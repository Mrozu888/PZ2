using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string[] gama = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "B", "H" };
        int[] dur = { 2, 2, 1, 2, 2, 2, 1 };

        Console.Write("Podaj dźwięk początkowy: ");
        string start = Console.ReadLine()?.Trim();

        if (!gama.Contains(start))
        {
            Console.WriteLine("Niepoprawny dźwięk. Podaj jeden z: C, C#, D, D#, E, F, F#, G, G#, A, B, H.");
            return;
        }

        int index = Array.IndexOf(gama, start);
        Console.Write(start + " "); // Wypisujemy pierwszy dźwięk

        foreach (int i in dur)
        {
            index = (index + i) % gama.Length; // Przechodzimy po gamie, uwzględniając cykliczność
            Console.Write(gama[index] + " ");
        }

        Console.WriteLine(); // Nowa linia na końcu
    }
}
