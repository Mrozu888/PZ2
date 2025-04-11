using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        const string serverAddress = "localhost";
        const int port = 3000;

        try
        {
            using (TcpClient client = new TcpClient(serverAddress, port))
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Connected to the server.");

                Console.Write("Enter message: ");
                string message = Console.ReadLine();

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] lengthBytes = BitConverter.GetBytes(messageBytes.Length);
                stream.Write(lengthBytes, 0, 4);

                stream.Write(messageBytes, 0, messageBytes.Length);
                Console.WriteLine("Message sent.");

                byte[] responseLengthBytes = new byte[4];
                stream.Read(responseLengthBytes, 0, 4);
                int responseLength = BitConverter.ToInt32(responseLengthBytes, 0);

                byte[] responseBytes = new byte[responseLength];
                int bytesRead = 0;
                while (bytesRead < responseLength)
                {
                    bytesRead += stream.Read(responseBytes, bytesRead, responseLength - bytesRead);
                }
                string response = Encoding.UTF8.GetString(responseBytes);
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