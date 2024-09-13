using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ArchiveHistorischeBahnen
{
    internal class Program
    {
        static List<HistorischeBahn> lstHB = new List<HistorischeBahn>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                int whal = Menu();
                WasJetzt(whal);
                Console.ReadLine();
            }
        }

        private static void ZeigListe()
        {
            if (File.Exists("HB.xml"))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HistorischeBahn>));

                    using (FileStream fileStream = new FileStream("HB.xml", FileMode.Open))
                    {
                        List<HistorischeBahn> deserializedList = (List<HistorischeBahn>)xmlSerializer.Deserialize(fileStream);

                        Console.WriteLine("\nInhalt der XML-Datei:");
                        Console.WriteLine("");


                        foreach (var bahn in deserializedList)
                        {
                            Console.WriteLine($"Betriebsart:         {bahn.BetriebArt} ");
                            Console.WriteLine($"Produktionsdatum:    {bahn.WannWarProduziert:yyyy-MM-dd}");
                            Console.WriteLine($"Produktionsland:     {bahn.WoWarProduziert}");
                            Console.WriteLine($"Bis wann in betrieb: {bahn.BisWannInBetrieb} ");
                            Console.WriteLine($"Alter:               {bahn.Alter}");
                            Console.WriteLine("");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim Lesen der XML-Datei: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Die XML-Datei existiert nicht.");
                return;
            }
            EntfernenEintrag();
        }

        private static void EntfernenEintrag()
        {
            Console.WriteLine("");
            Console.WriteLine("Möchten Sie ein eintrag löschen?");
            string? ja = DatenValidator.ValidiereString("Bitte  J oder N eintippen: ");
            string? ja1 = ja.ToUpper();
            if (ja1 == "JA" || ja1 == "J")
            {
                string? löschenTeil = DatenValidator.ValidiereString("Bitte geben Sie bis wann in betrieb war (MM-DD-YYYY) ein: ");
                löschenTeil = löschenTeil + " 00:00:00";
                XDocument xmlDoc = XDocument.Load("HB.xml");

                var bahnZumLöschen = xmlDoc.Descendants("HistorischeBahn")
                                   .Where(p => p.Element("BisWannInBetrieb") != null)
                                   .FirstOrDefault();
                if (bahnZumLöschen != null)
                {
                    bahnZumLöschen.Remove();
                    xmlDoc.Save("HB.xml");

                    Console.WriteLine($"Eintrag mit Produktionsdatum {löschenTeil} wurde erfolgreich gelöscht.");
                }
                else
                {
                    Console.WriteLine("Kein Eintrag mit dem angegebenen Produktionsdatum gefunden.");
                }
            }
            else
            {
                return;
            }
        }


        private static void NeueElementEinfügen(int whal)
        {
            /* HistorischeBahn hb = new HistorischeBahn();
    Console.WriteLine("Bitte die Betriebsart eingeben:");
    hb.Betriebsart = Console.ReadLine();

    Console.WriteLine("Bitte Produktionsdatum (yyyy-MM-dd) eingeben:");
    hb.WannWarProduziert = Convert.ToDateTime(Console.ReadLine());

    Console.WriteLine("Bitte Produktionsland eingeben:");
    hb.WoWarProduziert = Console.ReadLine();

    Console.WriteLine("Bitte Datum eingeben, bis wann in Betrieb war:");
    hb.BisWannInBetrieb = Convert.ToDateTime(Console.ReadLine());

    HistorischeBahn hb1 = new HistorischeBahn(hb.WannWarProduziert);
    hb.Alter = hb1.Alter; */

            HistorischeBahn hb = new HistorischeBahn();

            hb.BetriebArt = DatenValidator.ValidiereString("Bitte den Betriebart eingeben:");

            hb.WannWarProduziert = DatenValidator.ValidiereDatum("Bitte Produtiondatum (yyyy-MM-dd) eingeben:");

            hb.WoWarProduziert = DatenValidator.ValidiereString("Bitte Produktionsland eingeben:");

            DateTime bisDatum = DatenValidator.ValidiereDatum("Bitte Datum Bis wann in betrieb war eingeben:");

            hb.BisWannInBetrieb = Convert.ToString(bisDatum);

            HistorischeBahn hb1 = new HistorischeBahn(hb.WannWarProduziert);
            hb.Alter = hb1.Alter;

            lstHB.Add(hb);

            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create("HB.xml"))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HistorischeBahn>));
                    xmlSerializer.Serialize(xmlWriter, lstHB);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Daten: {ex.Message}");
            }

            Console.WriteLine("Das Element wurde erfolgreich hinzugefügt.");

            whal = -1;

        }
        private static void WasJetzt(int whal)
        {
            if (whal == 3)
            { Environment.Exit(0); }
            else if (whal == 1) { ZeigListe(); }
            else { NeueElementEinfügen(whal); }
        }

        private static int Menu()
        {
            int wahl = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    Console.WriteLine("____                                                                              ____");
                    Console.WriteLine("|DD|____T_                                                                        |DD|____T_ ");
                    Console.WriteLine("|_ |_____|<                                                                       |_ |_____|<");
                    Console.WriteLine("  @-@-@-oo\\__________Herzlich willkommen_im_Archiv_der_Historischen_Bahnen__________@-@-@-oo");
                    Console.WriteLine(" ");
                    Console.WriteLine("Was möchten Sie tun?");
                    Console.WriteLine("1. Zeig Liste");
                    Console.WriteLine("2. Neuen Element einfügen");
                    Console.WriteLine("3. Ausgang");


                    wahl = int.Parse(Console.ReadLine());


                    if (wahl < 1 || wahl > 3)
                    {
                        Console.WriteLine("Ungültige Auswahl. Bitte wählen Sie eine Zahl zwischen 1 und 3.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (FormatException)
                {

                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine Zahl ein.");
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                }
            }
            return wahl;
        }
    }



}