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
        public static StreamWriter writetext = File.AppendText("output.txt");

        static void Main(string[] args)
        {
            string file = "hw3_test.pcap";

            //четем файла
            var device = new CaptureFileReaderDevice(file);
            //отваря се комуникация
            device.Open();
            //слагаме събитие , при идване на пакети
            //какво да направим при наличие на пакети
            device.OnPacketArrival += new PacketArrivalEventHandler(dev_PacketArrival);
            //пускаме пакетите да идват
            device.Capture();
            //затваряме потока
            device.Close();

        }

        //как се обработва събитието , при налични пакети
        static void dev_PacketArrival(object sender, CaptureEventArgs e)
        {
            //проверяваме дали пакета е съвместим и е това което ни трябва
            if (e.Packet.LinkLayerType == PacketDotNet.LinkLayers.Ethernet)

            {
                //трябва да се cast-не от самото api на библиотеката
                var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                var ethernetPacket = (PacketDotNet.EthernetPacket)packet;

                string type = ethernetPacket.Type.ToString();

                //библиотеката изважда типът с думи IPv4, Arp,
                //и затова обръщам думите в числа
                if (type == "IPv4")
                {
                    type = "0x0800";
                }
                else if (type == "Arp")
                {
                    type = "0x8035";
                }
                else if (int.Parse(type) > 9 && int.Parse(type) < 100)
                {
                    type = "0x00" + type.ToString();
                }
                else if (int.Parse(type) > 0 && int.Parse(type) < 10)
                {
                    type = "0x000" + type.ToString();
                }
                else if (int.Parse(type) > 100 && int.Parse(type) < 1000)
                {
                    type = "0x0" + type.ToString();
                }
                else
                {
                    type = ethernetPacket.Type.ToString();
                }

                //пробразувайки ги в необходимия формат
                string sourceMac = FixMac(ethernetPacket.SourceHardwareAddress.ToString());
                string destinationMac = FixMac(ethernetPacket.DestinationHardwareAddress.ToString());

                Console.Write("Source: {0} -> Destination: {1} type: {2}",
                             sourceMac,
                             destinationMac,
                             type);
                writetext.Write("Source: {0} -> Destination: {1} type: {2}",
                             sourceMac,
                             destinationMac,
                             type);
                               

                //проверка за чексумата на ИП
                if (!CheckIp_Csum(packet))
                {
                    Console.WriteLine(" bad_IP_csum");
                    writetext.WriteLine(" bad_IP_csum");
                    return;
                }
                
                //извличане и cast-ване на пакета
                var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();
                if (tcpPacket != null)
                {
                    //обработка на пакета след като е извлечен
                    var ipPacket = (PacketDotNet.IPPacket)tcpPacket.ParentPacket;
                    System.Net.IPAddress srcIp = ipPacket.SourceAddress;
                    System.Net.IPAddress dstIp = ipPacket.DestinationAddress;

                                        
                    int srcPort = tcpPacket.SourcePort;
                    int dstPort = tcpPacket.DestinationPort;
                    //стандартния изходен резултат
                    string last = "NULL";

                    //променен след изискванията
                    if (tcpPacket.Push == true && tcpPacket.Finished == true && tcpPacket.Urgent == true)
                    {
                        last = "XMAS";
                    }
                    //празен ред допълван от следващия изход на конзолата
                    Console.WriteLine(" ");
                    writetext.WriteLine(" ");

                    //проверка за чексумата на TCP
                    if (!CheckTcp_Csum(packet))
                    {
                        Console.WriteLine(" {0} -> {1} {2} bad_TCP_CheckSum ",
                         srcIp, dstIp, ipPacket.Protocol);
                        writetext.WriteLine(" {0} -> {1} {2} bad_TCP_CheckSum ",
                         srcIp, dstIp, ipPacket.Protocol);
                        return;
                    }

                    Console.WriteLine(" {0}:{1} -> {2}:{3} {5}  {4} ",
                         srcIp, srcPort, dstIp, dstPort, last, ipPacket.Protocol);
                    writetext.WriteLine(" {0}:{1} -> {2}:{3} {5}  {4} ",
                         srcIp, srcPort, dstIp, dstPort, last, ipPacket.Protocol);
                }

            }

        }

        public static bool CheckIp_Csum(Packet packet)
        {
            var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();
            var ipPacket = (PacketDotNet.IPv4Packet)tcpPacket.ParentPacket;

            if (ipPacket.ValidIPChecksum)
            {
                return true;
            }
            
            return false;
        }

        public static bool CheckTcp_Csum(Packet packet)
        {
            var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();

            if (tcpPacket.ValidChecksum)
            {
                return true;
            }

            return false;
        }

        public static string FixMac(string sourceTemp)
        {
            string source = sourceTemp[0].ToString() + sourceTemp[1].ToString() + ":" +
               sourceTemp[2].ToString() + sourceTemp[3].ToString() + ":" +
               sourceTemp[4].ToString() + sourceTemp[5].ToString() + ":" +
               sourceTemp[6].ToString() + sourceTemp[7].ToString() + ":" +
               sourceTemp[8].ToString() + sourceTemp[9].ToString() + ":" +
               sourceTemp[10].ToString() + sourceTemp[11].ToString();
            return source;
        }
    }

}

