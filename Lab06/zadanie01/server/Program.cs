using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        const int port = 3000;
        const int bufferSize = 1024;

        try
        {
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Waiting for connection...");

            using (TcpClient client = server.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Client connected.");

                byte[] buffer = new byte[bufferSize];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received message: " + receivedMessage);

                string response = "Odpowiadam: " + receivedMessage;
                if (response.Length > bufferSize)
                {
                    response = response.Substring(0, bufferSize);
                }

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine("respond sent");
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