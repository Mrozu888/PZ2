using System;

namespace Lab02;

public class RachunekBankowy
{
    private string numer;
    private decimal stanRachunku;
    private bool czyDozwolonyDebet;
    private List<PosiadaczRachunku> _PosiadaczeRachunku = new List<PosiadaczRachunku>();
    private List<Transakcja> _Transakcje = new List<Transakcja>();


    public string Numer { get; set; }
    public decimal StanRachunku { get; set; }
    public bool CzyDozwolonyDebet { get; set; }
    public List<PosiadaczRachunku> PosiadaczeRachunku { get; }


    public RachunekBankowy(string numer, decimal stanRachunku, bool czyDozwolonyDebet, List<PosiadaczRachunku> posiadacze)
    {
        if (posiadacze == null || posiadacze.Count == 0)
        {
            throw new Exception("Rachunek musi zawierac co najmniej jedna pozycje.");
        }
        this.numer = numer;
        this.stanRachunku = stanRachunku;
        this.czyDozwolonyDebet = czyDozwolonyDebet;
        this._PosiadaczeRachunku = posiadacze;
    }

    public static void DokonajTransakcji(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, decimal kwota, string opis)
    {
        if (kwota <= 0)
        {
            throw new Exception("Kwota transakcji musi być dodatnia.");
        }
        if (rachunekZrodlowy == null && rachunekDocelowy == null)
        {
            throw new Exception("Oba rachunki nie mogą być null.");
        }
        if (rachunekZrodlowy != null && !rachunekZrodlowy.CzyDozwolonyDebet && rachunekZrodlowy.StanRachunku < kwota)
        {
            throw new Exception("Brak środków na rachunku źródłowym.");
        }

        Transakcja nowaTransakcja = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
        
        if (rachunekZrodlowy == null)
        {
            rachunekDocelowy.StanRachunku += kwota;
            rachunekDocelowy._Transakcje.Add(nowaTransakcja);
        }
        else if (rachunekDocelowy == null)
        {
            rachunekZrodlowy.StanRachunku -= kwota;
            rachunekZrodlowy._Transakcje.Add(nowaTransakcja);
        }
        else
        {
            rachunekZrodlowy.StanRachunku -= kwota;
            rachunekDocelowy.StanRachunku += kwota;
            rachunekZrodlowy._Transakcje.Add(nowaTransakcja);
            rachunekDocelowy._Transakcje.Add(nowaTransakcja);
        }
    }

    public static RachunekBankowy operator +(RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        if (rachunek._PosiadaczeRachunku.Contains(posiadacz))
            throw new Exception("Posiadacz juz istnieje.");

        rachunek._PosiadaczeRachunku.Add(posiadacz);
        return rachunek;

    }
    
    public static RachunekBankowy operator -(RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        if (!rachunek._PosiadaczeRachunku.Contains(posiadacz))
            throw new Exception("Posiadacz nie istnieje");
        if (rachunek._PosiadaczeRachunku.Count == 1)
            throw new Exception("Nie można usunąć jedynego posiadacza rachunku.");

        rachunek._PosiadaczeRachunku.Remove(posiadacz);
        return rachunek;

    }

    public override string ToString(){
        return @$"
            Numer rachunku: {Numer}
            Stan rachunku: {StanRachunku}
            Posiadacze rachunku: { string.Join(", ", _PosiadaczeRachunku)}
            Transakcje: {string.Join(", ", _Transakcje)}
        ";
    }
}
