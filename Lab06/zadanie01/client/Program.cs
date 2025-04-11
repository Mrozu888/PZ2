using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        const string serverAddress = "localhost";
        const int port = 3000;
        const int bufferSize = 1024;

        try
        {
            using (TcpClient client = new TcpClient(serverAddress, port))
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Server connected.");

                Console.Write("Enter message: ");
                string message = Console.ReadLine();

                if (message.Length > bufferSize)
                {
                    message = message.Substring(0, bufferSize);
                }

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                stream.Write(messageBytes, 0, messageBytes.Length);
                Console.WriteLine("Message sent.");

                byte[] buffer = new byte[bufferSize];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Response: " + response);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Client error: " + e.Message);
        }

        Console.WriteLine("Client exited.");
        Console.ReadKey();
    }
}