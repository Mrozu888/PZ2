using System;

namespace Lab02;



public class OsobaFizyczna : PosiadaczRachunku
{
    private string imie;
    private string nazwisko;
    private string drugieImie;
    private string pesel;
    private string numerPaszportu;

    public string Imie{
        get {return imie;}
        set {imie = value;}
    }
    public string Nazwisko{
        get {return nazwisko;}
        set {nazwisko = value;}
    }
    public string DrugieImie{
        get {return drugieImie;}
        set {drugieImie = value;}
    }
    public string Pesel{
        get {return pesel;}
        set { if (!ValidatePesel(pesel)) throw new Exception("Zly pesel");
            pesel = value;}
    }
    public string NumerPaszportu{
        get {return NumerPaszportu;}
        set {NumerPaszportu = value;}
    }

    public OsobaFizyczna(string imie, string nazwisko, string drugieImie, string pesel, string numerPaszportu){
        if(string.IsNullOrEmpty(pesel) && string.IsNullOrEmpty(numerPaszportu)){
            throw new Exception("PESEL albo numer paszportu muszą być nie null");
        }
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.drugieImie = drugieImie;
        if (!ValidatePesel(pesel)) throw new Exception("Zly pesel");
        this.pesel = pesel;
        this.numerPaszportu = numerPaszportu;
    }

    public bool ValidatePesel (string pesel){
        if (string.IsNullOrEmpty(pesel)) return false;
        if (pesel.Length != 11) return false;
        return pesel.All(char.IsDigit);
    }

    public override string ToString()
    {
        return $"Osoba fizyczna: {imie} {nazwisko}";
    }

}