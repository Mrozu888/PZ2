using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    private static string my_dir;

    static void Main()
    {
        my_dir = Directory.GetCurrentDirectory();
        const int port = 3000;

        try
        {
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine($"Server running in dir: {my_dir}");
            Console.WriteLine("Waiting for connection...");

            using (TcpClient client = server.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Client connected.");

                while (true)
                {
                    byte[] lengthBytes = new byte[4];
                    stream.Read(lengthBytes, 0, 4);
                    int messageLength = BitConverter.ToInt32(lengthBytes, 0);

                    byte[] messageBytes = new byte[messageLength];
                    int bytesRead = 0;
                    while (bytesRead < messageLength)
                    {
                        bytesRead += stream.Read(messageBytes, bytesRead, messageLength - bytesRead);
                    }
                    string message = Encoding.UTF8.GetString(messageBytes);
                    Console.WriteLine($"Otrzymano: {message}");


                    string response;
                    if (message == "!end")
                    {
                        response = "Closing server...";
                        SendResponse(stream, response);
                        break;
                    }
                    else if (message == "list")
                    {
                        response = GetDirectoryContents();
                    }
                    else if (message.StartsWith("in "))
                    {
                        string dirName = message.Substring(3);
                        response = ChangeDirectory(dirName);
                    }
                    else
                    {
                        response = "Invalid command";
                    }

                    SendResponse(stream, response);
                }
            }

            server.Stop();
            Console.WriteLine("Server exited.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Server error: {e.Message}");
        }
    }

    private static void SendResponse(NetworkStream stream, string response)
    {
        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
        byte[] lengthBytes = BitConverter.GetBytes(responseBytes.Length);
        stream.Write(lengthBytes, 0, 4);
        stream.Write(responseBytes, 0, responseBytes.Length);
    }

    private static string GetDirectoryContents()
    {
        try
        {
            var directories = Directory.GetDirectories(my_dir);
            var files = Directory.GetFiles(my_dir);
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dirs:");
            foreach (var dir in directories)
            {
                sb.AppendLine($"  {Path.GetFileName(dir)}");
            }
            
            sb.AppendLine("files:");
            foreach (var file in files)
            {
                sb.AppendLine($"  {Path.GetFileName(file)}");
            }
            
            return sb.ToString();
        }
        catch (Exception e)
        {
            return $"Error reading directory: {e.Message}";
        }
    }

    private static string ChangeDirectory(string dirName)
    {
        try
        {
            string newPath;
            if (dirName == "..")
            {
                newPath = Directory.GetParent(my_dir)?.FullName;
                if (newPath == null)
                    return "You can't go there, you are in root directory";
            }
            else
            {
                newPath = Path.Combine(my_dir, dirName);
            }

            if (Directory.Exists(newPath))
            {
                my_dir = newPath;
                return GetDirectoryContents();
            }
            else
            {
                return "Directory not found";
            }
        }
        catch (Exception e)
        {
            return $"Error changing directory: {e.Message}";
        }
    }
}