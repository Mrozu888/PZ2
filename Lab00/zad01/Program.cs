using System;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Not enough args!");
            return;
        }

        else
        {
            try
            {
                int n = int.Parse(args[^1]);
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine(string.Join(" ", args[..^1]));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error:" + e);
            }
        }
    }
}
