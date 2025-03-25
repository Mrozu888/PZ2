using System;

class Program
{
    static void Main(string[] args)
    {
        
        //czytanie z pliku tekstowego linijka po linijce aż do końca pliku
        StreamReader sr = new StreamReader(args[0]);
        int max = 0;
        int inc = 1;
        int savedInc = 1;
        while (!sr.EndOfStream)
        {
            String napis = sr.ReadLine();
            int val = int.Parse(napis);
            // Console.WriteLine(napis);
            if (val>max){
                max = val;
                savedInc = inc;
            }
            inc++;
        }
        sr.Close();
        Console.WriteLine(max + ", linijka: " + savedInc);
    }
}
