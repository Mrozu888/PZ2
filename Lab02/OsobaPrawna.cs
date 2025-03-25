using System;

namespace Lab02;

public class OsobaPrawna : PosiadaczRachunku
{
    private String nazwa;
    private String siedziba;

    public string Nazwa{
        get {return nazwa;}
    }
    public string Siedziba{
        get {return siedziba;}
    }

    public OsobaPrawna(string nazwa, string siedziba)
    {
        this.nazwa = nazwa;
        this.siedziba = siedziba;
    }

    public override string ToString()
    {
        return $"Osoba prawna: {nazwa}, siedziba: {siedziba}";
    }
}