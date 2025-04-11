using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    private static readonly List<string> foundFiles = new List<string>();
    private static readonly object lockObject = new object();
    private static bool isSearching = true;
    private static string startDirectory = "./katalog";

    public static void Main()
    {
        Console.WriteLine("Podaj szukany podciąg w nazwie pliku:");
        string searchPattern = Console.ReadLine();

        // search thread
        Thread searchThread = new Thread(() => SearchFiles(startDirectory, searchPattern));
        searchThread.IsBackground = false;
        searchThread.Start();

        // main thread
        Thread printThread = new Thread(PrintAndRemoveFiles);
        printThread.IsBackground = false;
        printThread.Start();

        searchThread.Join();  // Czekaj na zakończenie wątku wyszukującego
        printThread.Join();   // Czekaj na zakończenie wątku wyświetlającego

        Console.WriteLine("Wyszukiwanie zakończone.");
    }

    private static void SearchFiles(string directory, string pattern)
    {
        try
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                if (Path.GetFileName(file).Contains(pattern))
                {
                    lock (lockObject)
                    {
                        foundFiles.Add(file);
                        Monitor.Pulse(lockObject);
                    }
                }
            }

            foreach (string subDir in Directory.GetDirectories(directory))
            {
                SearchFiles(subDir, pattern);
            }
        }
        catch (UnauthorizedAccessException)
        {}
        finally
        {
            if (directory == startDirectory)
            {
                isSearching = false;
                lock (lockObject)
                {
                    Monitor.Pulse(lockObject);
                }
            }
        }
    }

    private static void PrintAndRemoveFiles()
    {
        while (isSearching || foundFiles.Count > 0)
        {
            string fileToPrint = null;
            
            lock (lockObject)
            {
                if (foundFiles.Count == 0)
                {
                    Monitor.Wait(lockObject);
                    continue;
                }
                
                fileToPrint = foundFiles[0];
                foundFiles.RemoveAt(0);
            }

            if (fileToPrint != null)
            {
                Console.WriteLine($"Znaleziono: {fileToPrint}");
            }
        }
    }
}