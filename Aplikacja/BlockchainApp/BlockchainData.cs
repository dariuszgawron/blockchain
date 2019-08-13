using System;

namespace BlockchainApp
{
    [Serializable]
    public class BlockchainData
    {
        //Ogólne
        public string NumerInwentaryzacyjny { get; set; }
        public string Nazwa { get; set; }
        public string NumerFabryczny { get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public string SymbolKST { get; set; }
        public string Rodzaj { get; set; }

        //Daty
        public string DataZakupu { get; set; }
        public string DataPrzyjecia { get; set; }
        public string DataZbycia { get; set; }
        public string DataRozpoczeciaAmortyzacji { get; set; }
        public int RokProdukcji { get; set; }
        public string DataKoncaGwarancji { get; set; }
        
        //Lokalizacja
        public string MiejsceUzytkowania { get; set; }
        public string Dzial { get; set; }
        public string NumerPomieszczenia { get; set; }
        public string OsobaOdpowiedzialna { get; set; }

        //Amortyzacja
        public double WartoscPoczatkowa { get; set; }
        public double StawkaAmortyzacji { get; set; }
        public double WartoscZamortyzowana { get; set; }
        public string Status { get; set; }

        //Dodatkowe informacje
        public string Uwagi { get; set; }
        public string IdentyfikatorModyfikatora { get; set; }
        
        public BlockchainData(string NumerInwentaryzacyjny, string Nazwa, string NumerFabryczny = null, string Producent = null, string Model = null, string SymbolKST = null, string Rodzaj = null,
                              string DataZakupu = null, string DataPrzyjecia = null, string DataZbycia = null, string DataRozpoczeciaAmortyzacji = null, int RokProdukcji = 0, string DataKoncaGwarancji = null,
                              string MiejsceUzytkowania = null, string Dzial = null, string NumerPomieszczenia = null, string OsobaOdpowiedzialna = null,
                              double WartoscPoczatkowa = 0.0, double StawkaAmortyzacji = 0.0, double WartoscZamortyzowana = 0.0, string Status = null, 
                              string Uwagi = null, string IdentyfikatorModyfikatora = null)
        {
            //Ogólne
            this.NumerInwentaryzacyjny = NumerInwentaryzacyjny;
            this.Nazwa = Nazwa;
            this.NumerFabryczny = NumerFabryczny;
            this.Producent = Producent;
            this.Model = Model;
            this.SymbolKST = SymbolKST; 
            this.Rodzaj = Rodzaj;

            //Daty
            this.DataZakupu = DataZakupu;
            this.DataPrzyjecia = DataPrzyjecia;
            this.DataZbycia = DataZbycia;
            this.DataRozpoczeciaAmortyzacji = DataRozpoczeciaAmortyzacji;
            this.RokProdukcji = RokProdukcji;
            this.DataKoncaGwarancji = DataKoncaGwarancji;

            //Lokalizacja
            this.MiejsceUzytkowania = MiejsceUzytkowania;
            this.Dzial = Dzial;
            this.NumerPomieszczenia = NumerPomieszczenia;
            this.OsobaOdpowiedzialna = OsobaOdpowiedzialna;

            //Amortyzacja
            this.WartoscPoczatkowa = WartoscPoczatkowa;
            this.StawkaAmortyzacji = StawkaAmortyzacji;
            this.WartoscZamortyzowana = WartoscZamortyzowana;
            this.Status = Status;

            //Dodatkowe informacje
            this.Uwagi = Uwagi;
            this.IdentyfikatorModyfikatora = IdentyfikatorModyfikatora;
        }
        
        public BlockchainData() { }
    }
}
