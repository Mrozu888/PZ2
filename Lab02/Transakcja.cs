using System;

namespace Lab02;

public class Transakcja
{
    private RachunekBankowy? rachunekZrodlowy;
    private RachunekBankowy? rachunekDocelowy;
    private decimal kwota;
    private string opis;

    public RachunekBankowy RachunekZrodlowy { get; }
    public RachunekBankowy RachunekDocelowy { get;}
    public decimal Kwota { get => kwota; }
    public string Opis { get => opis; }

    public Transakcja(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, decimal kwota, string opis)
    {
        if (rachunekZrodlowy == null || rachunekDocelowy == null)
        {
            throw new Exception("Brak rachunkow.");
        }
        this.rachunekZrodlowy = rachunekZrodlowy;
        this.rachunekDocelowy = rachunekDocelowy;
        this.kwota = kwota;
        this.opis = opis;
    }

    public override string ToString()
    {
        return @$"Rachunek zrodlowy: {rachunekZrodlowy?.Numer ?? "Brak"}
        Rachunek docelowy: {rachunekDocelowy?.Numer ?? "Brak"},
        Kwota: {kwota}, 
        Opis: {opis}";
    }
}