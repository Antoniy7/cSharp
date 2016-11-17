using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                handleClient client = new handleClient();
                client.startClient(clientSocket, Convert.ToString(counter));
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadKey();
        }
    }

    public class handleClient
    {
        TcpClient clientSocket;
        string clientNumber;
        public void startClient(TcpClient inClientSocket, string clientNo)
        {
            this.clientSocket = inClientSocket;
            this.clientNumber = clientNo;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }
        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[1024];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> " + "From client-" + clientNumber + dataFromClient);

                    rCount = Convert.ToString(requestCount);
                    serverResponse = "Server to clinet(" + clientNumber + ") " + rCount;
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    }
}