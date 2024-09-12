namespace ArchiveHistorischeBahnen
{
    [Serializable]
    public class HistorischeBahn
    {
        public string? BetriebArt;
        public DateTime WannWarProduziert;
        public string? WoWarProduziert;
        public string? BisWannInBetrieb;
        public int Alter;

        public HistorischeBahn(){}
        public HistorischeBahn( DateTime WannWarProduziert) {
            WannWarProduziert = this.WannWarProduziert;
        DateTime heute= DateTime.Now;
            int h1,h2;
            h1=Convert.ToInt32(heute.Year);
            h2=Convert.ToInt32(WannWarProduziert.Year);

            Alter = h1-h2;

        }
        public override string ToString() { return $"{BetriebArt}{WannWarProduziert.ToString()}{WoWarProduziert}{BisWannInBetrieb}{Alter.ToString()}"; }


    }

    }

