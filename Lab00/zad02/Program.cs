using System;

class Program
{
    static void Main(string[] args)
    {
        int count = 0;
        int sum = 0;
        while (true)
        {

            int liczba = int.Parse(Console.ReadLine());
            if (liczba == 0) break;
            // Console.WriteLine(liczba);
            sum += liczba;
            count++;
        }
        float avg = (float)sum/count;
        Console.WriteLine(sum);
        Console.WriteLine(count);

        StreamWriter sw = new StreamWriter("wynik.txt");
        sw.WriteLine(avg);
        sw.Close();
    }
}
