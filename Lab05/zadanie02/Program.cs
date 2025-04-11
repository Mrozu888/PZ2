using System;
using System.IO;
using System.Threading;

class Program
{
    private static bool isRunning = true;
    private static string[] previousFiles;

    public static void Main()
    {
        string directoryPath = "./katalog";

        previousFiles = Directory.GetFiles(directoryPath);

        Thread monitorThread = new Thread(() => MonitorDirectory(directoryPath));
        monitorThread.IsBackground = false;
        monitorThread.Start();

        Console.WriteLine("Monitorowanie rozpoczęte. Naciśnij 'q' aby zakończyć");

        while (Console.ReadKey().Key != ConsoleKey.Q) { }

        isRunning = false;
        monitorThread.Join();
        Console.WriteLine("Monitorowanie zakończone.");
    }

    private static void MonitorDirectory(string path)
    {
        while (isRunning)
        {
            Thread.Sleep(1000);

            string[] currentFiles = Directory.GetFiles(path);

            // dodane pliki
            foreach (string file in currentFiles)
            {
                if (Array.IndexOf(previousFiles, file) < 0)
                {
                    Console.WriteLine($"dodano plik {Path.GetFileName(file)}");
                }
            }

            // usuniete pliki
            foreach (string file in previousFiles)
            {
                if (Array.IndexOf(currentFiles, file) < 0)
                {
                    Console.WriteLine($"usunięto plik {Path.GetFileName(file)}");
                }
            }

            previousFiles = currentFiles;
        }
    }
}