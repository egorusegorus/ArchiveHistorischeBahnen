using System.Threading.Channels;

namespace ArchiveHistorischeBahnen
{
    internal class Program
    {
        static List<HistorischeBahn> lstHB=new List<HistorischeBahn>();
        static void Main(string[] args)
        {
            

            
            int whal = Menu();
            WasJetzt(whal);

    Console.ReadLine();

        }

        private static void ZeigListe() {
            Console.WriteLine("Betriebart |"+" "+ "Wann war produziert |" + " "+ "Wo war produziert |" + " "+ "Bis wann in betrieb |" + " "+ "Alter |");

        }
        private static void NeueElementEinfügen() {
            HistorischeBahn hb = new HistorischeBahn( );
            Console.WriteLine("Bitte den Betriebart eingeben:");
            hb.BetriebArt=Console.ReadLine();
            
            Console.WriteLine("Bitte Produtiondatum (yyyy-MM-dd) eingeben:");
            hb.WannWarProduziert = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Bitte Produktionsland eingeben:");
            hb.WoWarProduziert = Console.ReadLine();

            Console.WriteLine("Bitte Datum Bis wann in betrieb war eingeben:");
            hb.WoWarProduziert = Console.ReadLine();
            HistorischeBahn hb1 =new HistorischeBahn(hb.WannWarProduziert);
            hb.Alter=hb1.Alter;

            lstHB.Add( hb );
            

        }
        private static void WasJetzt(int wahl) {
            if (wahl == 3)
            { Environment.Exit(0);}
               else if (wahl == 1) { ZeigListe(); }
            else { NeueElementEinfügen(); }
        } 

        private static int Menu() {
            int wahl = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    Console.WriteLine("____                                                                               _____");
                    Console.WriteLine("|DD|____T_                                                                        |DD|____T_ ");
                    Console.WriteLine("|_ |_____|<                                                                       |_ |_____|<");
                    Console.WriteLine("  @-@-@-oo\\__________Herzlich willkommen in Archiv von Historische Bahnen__________@-@-@-oo");
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

