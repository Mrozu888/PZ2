using System;
using System.Threading;

class Program
{
    private static readonly object lockObj = new object();
    private static int startedThreads = 0;
    private static bool exit = false;
    private static int threadCount = 10;

    public static void Main()
    {
        for (int i = 0; i < threadCount; i++)
        {
            int threadId = i;
            Thread t = new Thread(() => WorkerThread(threadId));
            t.Start();
        }

        WaitForAllThreadsToStart();

        Console.WriteLine("Wszystkie wątki rozpoczęły działanie. Inicjowanie zamknięcia...");

        lock (lockObj)
        {
            exit = true;
            Monitor.PulseAll(lockObj);
        }

        Console.WriteLine("Wszystkie wątki otrzymały sygnał zakończenia. Program główny kończy działanie.");
    }

    private static void WaitForAllThreadsToStart()
    {
        lock (lockObj)
        {
            while (startedThreads < threadCount)
            {
                Monitor.Wait(lockObj);
            }
        }
    }

    private static void WorkerThread(int id)
    {
        lock (lockObj)
        {
            startedThreads++;
            Console.WriteLine($"Wątek {id} rozpoczął działanie");
            Monitor.PulseAll(lockObj);
        }

        while (true)
        {
            lock (lockObj)
            {
                if (exit)
                {
                    return;
                }
                
                Thread.Sleep(200);
            }
        }
    }
}