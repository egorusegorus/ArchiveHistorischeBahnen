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
        public HistorischeBahn(DateTime wannWarProduziert)
{
    WannWarProduziert = wannWarProduziert;

    DateTime heute = DateTime.Now;
    int h1 = heute.Year;
    int h2 = wannWarProduziert.Year;

    Alter = h1 - h2;
}

        public override string ToString() { return $"{BetriebArt}{WannWarProduziert.ToString()}{WoWarProduziert}{BisWannInBetrieb}{Alter.ToString()}"; }


    }

    }

