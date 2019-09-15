using System;
using SharpPcap;
using SharpPcap.LibPcap;
using System.IO;
using PacketDotNet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
//using PcapDotNet.Packets.Http;

namespace std
{
    class Pcap
    {
        /// <summary> In the file of reading the global position  </summary>
        static int GLOBAL_READING_POSITION_ = 0;
        /// <summary> Size before doing anything to the packet </summary>
        static int OFFICIAL_SIZE_PACKET_ = 0;
        public static string GLOBALFILENAME_;
        public static string EXTENSNION_;
        //public static bool header = false;
        public static BinaryWriter bw_;
        public static BinaryReader br_;
        static int TCP_FIXED = 0;
        static int UDP_FIXED = 0;


        struct packetHeaderZZ
        {
            /// <summary> epoch time </summary>
            public int ts_sec;         /* timestamp seconds */
            /// <summary> arrival time </summary>
            public int ts_usec;        /* timestamp microseconds */
            public int incl_len;       /* number of octets of packet saved in file */
            public int orig_len;       /* actual length of packet */
            public override string ToString()
            {
                return ts_sec.ToString() + " " + ts_usec.ToString() + " " +
                    incl_len.ToString() + " " + orig_len.ToString(); ;
            }
        }

        static void Main(string[] args)
        {
            //string file = "sflow.cap"; //http://packetlife.net/captures/sflow.cap 
            //string file = "sip.pcap"; //https://wiki.wireshark.org/SampleCaptures#SIP_and_RTP
            string file = "httpTest.pcap"; //https://wiki.wireshark.org/SampleCaptures#HyperText_Transport_Protocol_.28HTTP.29
            //string file = "RADIUS.cap"; //http://packetlife.net/captures/RADIUS.cap 
            //string file = "sipSecond.pcap";
            // Init();

            int offset = 4;
            if (file.Substring(file.Length - 4) == "pcap")
                offset = 5;

            GLOBALFILENAME_ = file.Substring(0, file.Length - offset);
            EXTENSNION_ = file.Substring(file.Length - offset);

            bw_ = new BinaryWriter(new FileStream("writingToFile.pcap", FileMode.Create));
            br_ = new BinaryReader(new FileStream(GLOBALFILENAME_ + " - Copy" + EXTENSNION_, FileMode.Open));

            //write file header
            WriteFileHeader();
            //четем файла
            var device = new CaptureFileReaderDevice(file);
            //отваря се комуникация
            device.Open();
            //event for arriving packets
            //and what to do with the coming packets
            device.OnPacketArrival += new PacketArrivalEventHandler(dev_PacketArrival);
            //start the flow of packets
            device.Capture();
            //close the stream
            device.Close();
            //stats for changed packets
            Statistic();

        }
        //handling of packets
        static void dev_PacketArrival(object sender, CaptureEventArgs e)
        {
            //check for compatability
            if (e.Packet.LinkLayerType == PacketDotNet.LinkLayers.Ethernet)
            {
                //cast it 
                var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

                OFFICIAL_SIZE_PACKET_ = packet.Bytes.Length;

                var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();

                if (tcpPacket != null)
                {
                    const int HTTP_PORT = 80;

                    if (tcpPacket.DestinationPort == HTTP_PORT || tcpPacket.SourcePort == HTTP_PORT)
                    {
                        TcpHttp(packet);
                        TCP_FIXED++;

                        return;
                    }
                    else
                    {
                        WritingPacket(packet, null);
                        return;
                    }
                }

                var udp = packet.Extract<PacketDotNet.UdpPacket>();

                if (udp != null)

                {
                    const int RADIUS_PORT = 1812;
                    const int SIP_PORT = 5060;

                    if (udp.DestinationPort == RADIUS_PORT || udp.SourcePort == RADIUS_PORT)
                    {

                        RadiusSession(packet);
                        UDP_FIXED++;
                        return;
                    }
                    else if (udp.DestinationPort == SIP_PORT)
                    {
                        SipSession(packet);
                        return;
                    }
                    else
                    {
                        WritingPacket(packet, null);
                        return;
                    }
                }

                else
                {
                    WritingPacket(packet, null);
                    return;
                }

            }
        }

        /// <summary>
        /// http Tcp protocol session
        /// reading from file fields that will be changed
        /// </summary>
        public static void TcpHttp(Packet packet)
        {

            //const string FIELD = "Accept-Encoding:";
            //const string new_field = "Accept-Encoding: google chrome, windows 10 some platform en-US";

            const string FILE_NAME = "command.txt";
            List<string> commands = ReadFromFile(FILE_NAME);


            for (int i = 0; i < commands.Count; i += 2)
            {
                string FIELD;
                string new_field;
                FIELD = commands[i];
                new_field = commands[i + 1];
                byte[] change_user_agent = Encoding.ASCII.GetBytes(new_field);
                byte[] payload = packet.PayloadPacket.PayloadPacket.PayloadData;


                if (payload.Length > 0)
                {
                    var penalthy_steps = FindPenalthySteps(payload, FIELD); //

                    if (penalthy_steps.Item1 > 0)
                    {
                        //testing
                        var new_payload = Session_Helper(payload, penalthy_steps, change_user_agent);

                        packet.PayloadPacket.PayloadPacket.PayloadData = new_payload;
                        //TCP_FIXED++;
                    }
                }
            }

            #region idea_of the variable size_tuple
            /*new size in the changed packet ! 
                since we write the size of the read file , once we change that
                we need to write a new size to the file
                which will eb the current size ! */
            #endregion
            Tuple<int, int> size_tuple = new Tuple<int, int>(packet.TotalPacketLength, 0);


            WritingPacket(packet, size_tuple);
        }


        /// <summary>
        /// UDP
        /// SIP session remove Expires/billing-credit-time/
        /// port 5060
        /// </summary>
        /// <param name="packet"></param>
        private static void SipSession(Packet packet)
        {
            const string METHOD = "REGISTER";

            //the field we want removed
            const string FIELD = "Expires";
            //const string FIELD = "CSeq";


            //the payload itself
            byte[] payload = packet.PayloadPacket.PayloadPacket.PayloadData;
            //the bytes needed to verify whether we should change the packet or not
            byte[] current_headerB = new byte[METHOD.Length];

            //transfer the required bytes to the new container
            Array.Copy(payload, 0, current_headerB, 0, METHOD.Length);

            //change the bytes header to string
            string current_headerC = Encoding.ASCII.GetString(current_headerB);

            if (current_headerC.Equals(METHOD))
            {
                //find penalthy and steps
                var penalthy_steps = FindPenalthySteps(payload, FIELD); //6 ; 15

                packet.PayloadPacket.PayloadPacket.PayloadData = Session_Helper(payload, penalthy_steps, null);

            }

            Tuple<int, int> size_tuple = new Tuple<int, int>(packet.TotalPacketLength, 0);
            WritingPacket(packet, size_tuple);
        }

        /// <summary>
        /// the number of steps needed to find the position
        /// of where we want to delete
        /// </summary>
        /// <param name="payload"></param>
        public static Tuple<int, int> FindPenalthySteps(byte[] payload, string string_to_match)
        {
            const byte kStopFirst = 0x0d;
            const byte kStopSecond = 0x0a;
            bool should_check = true;
            int num_stops_before_reaching = 0;
            int num_elements_to_remove = 0;
            for (int i = 0; i < payload.Length; ++i)
            {
                if (should_check)
                {
                    //generate second string to compare with
                    int size = string_to_match.Length;
                    byte[] comapre_to_origi = new byte[size];
                    if (payload.Length < i + size) { return new Tuple<int, int>(0, 0); }
                    Array.Copy(payload, i, comapre_to_origi, 0, size);
                    string current_field = Encoding.ASCII.GetString(comapre_to_origi);
                    if (string_to_match.Equals(current_field))  /*string matchs*/
                    {
                        //num_elements_to_remove += size;
                        while (payload[i++] != kStopFirst)
                        {
                            num_elements_to_remove++;
                        }

                        //last two ending eelements
                        num_elements_to_remove += 2;
                        break;
                    }
                    else
                    {
                        should_check = false;
                        i += size;
                        continue;
                    }
                }
                else if (payload[i] == kStopFirst && payload[i + 1] == kStopSecond)
                {
                    num_stops_before_reaching++;
                    i++; // to take the position of the second end symbol
                    should_check = true; // to check whether its the field we need
                    continue;

                }

            }

            return new Tuple<int, int>(num_stops_before_reaching, num_elements_to_remove);
        }

        /// <summary>
        /// removing the field form the payload
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static byte[] Session_Helper(byte[] payload, Tuple<int, int> pen_steps, byte[] data)
        {
            //when it reaches 0 it means we're at EXPIRES ! 
            int numberOfStops = pen_steps.Item1;
            const byte stopFirst = 0x0d;
            const byte stopSecond = 0x0a;

            List<byte> vector = new List<byte>();

            for (int i = 0; i < payload.Length; i++)
            {
                //current item in payload
                byte item = payload[i];

                //check if it matches our pattern
                if (item == stopFirst)
                {
                    //checking for the second elem
                    byte consec_item = payload[i + 1];
                    if (consec_item == stopSecond)
                    {
                        // it we're at the needed spot that we should 
                        //remove
                        if (numberOfStops == 0)
                        {
                            continue;
                        }
                        //we're still not there yet
                        else
                        {
                            numberOfStops--;
                            if (numberOfStops == 0)
                            {
                                vector.Add(payload[i]);
                                vector.Add(payload[i + 1]);

                                i++;
                                //place the new data in the array 
                                if (data != null)
                                {
                                    foreach (var elem in data)
                                    {
                                        vector.Add(elem);
                                    }
                                    vector.Add(stopFirst);
                                    vector.Add(stopSecond);
                                }
                                continue;
                            }
                        }
                    }
                }
                else if (numberOfStops == 0)
                {
                    //the place the index at the expected location
                    for (; numberOfStops >= 0; i++)
                    {

                        if (payload[i] == stopFirst)
                        {
                            // here it should be at stopSecond and by continuing 
                            // it should fall to the place it should be 
                            i++;
                            numberOfStops = -1;
                            
                            continue;
                        }
                    }
                    item = payload[i];
                }
                
                vector.Add(item);
            }

            return vector.ToArray();
            
        }


        /// <summary>
        /// UDP
        /// RADIUS destination port session port 1812 handling
        /// packets with this protocol
        /// Change nas-ip-address to
        /// </summary>
        /// <param name="packet"></param>
        public static void RadiusSession(Packet packet)
        {
            // new designated ip 
            const byte A = 21;
            const byte B = 21;
            const byte C = 21;
            const byte D = 21;
            //                            ip              udp         data
            byte[] ip_change = packet.PayloadPacket.PayloadPacket.PayloadData;
            int index_NAS_IP_Addr = 22;

            var udp = packet.Extract<PacketDotNet.UdpPacket>();
            if (udp.DestinationPort == 1812)
            {
                ip_change[index_NAS_IP_Addr++] = A;
                ip_change[index_NAS_IP_Addr++] = B;
                ip_change[index_NAS_IP_Addr++] = C;
                ip_change[index_NAS_IP_Addr++] = D;
                
            }

            packet.PayloadPacket.PayloadPacket.PayloadData = ip_change;

            //write the packet to the file
            WritingPacket(packet, null);
        }

        /// <summary>
        /// writing to file each individual packet with its packet header 
        /// </summary>
        /// <param name="packet"></param>
        private static void WritingPacket(Packet packet, Tuple<int, int> sizes)
        {
            const int PACKET_HEADER_SIZE = 16;

            // packet header
            packetHeaderZZ packetHeader = new packetHeaderZZ();
            packetHeader.ts_sec = br_.ReadInt32();
            packetHeader.ts_usec = br_.ReadInt32();
            if (sizes == null)
            {
                packetHeader.incl_len = br_.ReadInt32();
                packetHeader.orig_len = br_.ReadInt32();
            }
            else
            {
                packetHeader.incl_len = br_.ReadInt32();
                packetHeader.orig_len = br_.ReadInt32();
                packetHeader.incl_len = packetHeader.orig_len = sizes.Item1;

            }

            WritePacketHeader(packetHeader);


            GLOBAL_READING_POSITION_ += PACKET_HEADER_SIZE;

            //write the packet
            byte[] arr = br_.ReadBytes(OFFICIAL_SIZE_PACKET_);
            
            bw_.Write(packet.Bytes);

            //place it in position to continue reading
            GLOBAL_READING_POSITION_ += OFFICIAL_SIZE_PACKET_;

        }


        /// <summary>
        /// writing the packet header before the data of the packet themselves
        /// </summary>
        /// <param name="packetHeader"></param>
        private static void WritePacketHeader(packetHeaderZZ packetHeader)
        {
            byte[] ts_sec = BitConverter.GetBytes(packetHeader.ts_sec);
            byte[] ts_usec = BitConverter.GetBytes(packetHeader.ts_usec);
            byte[] incl_len = BitConverter.GetBytes(packetHeader.incl_len);
            byte[] orig_len = BitConverter.GetBytes(packetHeader.orig_len);
            bw_.Write(ts_sec); bw_.Write(ts_usec); bw_.Write(incl_len); bw_.Write(orig_len);
        }


        /// <summary>
        /// first 24 bytes which are the pcap file standart header 
        /// </summary>
        private static void WriteFileHeader()
        {
            const int PACKET_HEADER_SIZE = 16;
            const int HEADER_BEFORE_PACKETS = 24;
            
            {
                for (int i = 0, headerSize = HEADER_BEFORE_PACKETS; i < headerSize; ++i)
                {
                    byte a = br_.ReadByte();

                    GLOBAL_READING_POSITION_++; 
                    bw_.Write(a);
                }
                
            }

        }

        /// <summary>
        /// Summary of how many packets have been modified
        /// </summary>
        public static void Statistic()
        {
            Console.WriteLine("TCP = {0}\n UDP={1}", TCP_FIXED, UDP_FIXED);
        }
        /// <summary>
        /// reading instructions to modify in HTTP
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ReadFromFile(string fileName)
        {
            var list = new List<string>();
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.ASCII))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            return list;
        }


    }

}

