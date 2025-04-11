using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main()
    {
        const int port = 3000;

        try
        {
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Waiting for connection...");

            using (TcpClient client = server.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Clinet connected.");

                byte[] lengthBytes = new byte[4];
                stream.Read(lengthBytes, 0, 4);
                int messageLength = BitConverter.ToInt32(lengthBytes, 0);

                byte[] messageBytes = new byte[messageLength];
                int bytesRead = 0;
                while (bytesRead < messageLength)
                {
                    bytesRead += stream.Read(messageBytes, bytesRead, messageLength - bytesRead);
                }
                string receivedMessage = System.Text.Encoding.UTF8.GetString(messageBytes);
                Console.WriteLine("Received response: " + receivedMessage);

                string response = "received: " + receivedMessage;
                byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(response);

                byte[] responseLengthBytes = BitConverter.GetBytes(responseBytes.Length);
                stream.Write(responseLengthBytes, 0, 4);

                stream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine("Repsonse sent.");
            }

            server.Stop();
        }
        catch (Exception e)
        {
            Console.WriteLine("Server error: " + e.Message);
        }

        Console.WriteLine("Server exited.");
    }
}