using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Program producent-konsument");
        Console.WriteLine("Podaj liczbę producentów:");
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj liczbę konsumentów:");
        int m = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj maksymalny czas opóźnienia (ms):");
        int maxDelay = int.Parse(Console.ReadLine());
        Console.WriteLine("Podaj minimalny czas opóźnienia (ms):");
        int minDelay = int.Parse(Console.ReadLine());

        var manager = new Manager(n, m, maxDelay);
        manager.Start();

        Console.WriteLine("Naciśnij 'q' aby zakończyć...");
        while (Console.ReadKey().Key != ConsoleKey.Q) { }

        manager.Stop();
        Console.WriteLine("Zatrzymywanie wątków...");
    }
}

