using System;
using Lab02;

class Program
{
    static void Main()
    {
        
        OsobaFizyczna osoba1 = new OsobaFizyczna("Jan", "Kowalski", "Piotr", "12345678901", null);
        OsobaPrawna firma1 = new OsobaPrawna("XYZ Sp. z o.o.", "Warszawa");

        RachunekBankowy rachunek1 = new RachunekBankowy("123456", 5000m, false, new List<PosiadaczRachunku> { osoba1 });
        RachunekBankowy rachunek2 = new RachunekBankowy("789012", 2000m, true, new List<PosiadaczRachunku> { firma1 });

        RachunekBankowy.DokonajTransakcji(rachunek1, rachunek2, 1500m, "Przelew wynagrodzenia");

        Console.WriteLine($"Stan rachunku 1: {rachunek1.StanRachunku}");
        Console.WriteLine($"Stan rachunku 2: {rachunek2.StanRachunku}");
    
    }
}
