using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Zpracovanie_vykazov
{
    class Program
    {
        static void Main(string[] args)
        {
            SpravaVykazov spravaVykazov = new SpravaVykazov();
            spravaVykazov.ZpracovatVykazy();
            string pathD = "C:\\Users\\kocak\\Desktop\\Programming\\programovanie v C#\\Aplikacia2\\ConsoleSave";
            Directory.SetCurrentDirectory(pathD);
            Console.Clear();

            int volba = 0;
            while(volba != 7)
            {
                Console.Clear();
                volba = ZobrazVyberMenu("***HLAVNE MENU***", new string[] {"Projekty - celkove odpracovane hodiny", "Zamestnanci - celkove odpracovane hodiny", "Hodiny celkom",
                "Projekt - detail", "Zamestnanec - detail", "Vypis vsetko","Ukoncit program"});
                Console.Clear();
                if (volba == 7) { break; }
                string vypis = spravaVykazov.KonzolaSubor();
                Console.Clear();
                StreamWriter zapisovac = null;
                switch (volba)
                {
                    case 1:
                        while (vypis != null)
                        {
                            if (vypis == "konzola")
                            {
                                spravaVykazov.ReportCelkoveHodinyProjektu();
                                Console.WriteLine("Stlacte lubovolnu klavesu pre pokračovanie");
                                Console.ReadKey();
                                Console.Clear();
                                vypis = null;
                            }
                            else if (vypis == "subor")
                            {
                                Console.WriteLine("Zadajte nazov suboru: ");
                                string nazovMiesta = Console.ReadLine();
                                string nazovSuboru = nazovMiesta + ".txt";
                                Console.Clear();
                                zapisovac = new StreamWriter(nazovSuboru);
                                spravaVykazov.ZapisReportCelkoveHodinyProjektu(zapisovac);
                                zapisovac.Close();
                                Console.Clear();
                                UspesneZapisanie();
                                vypis = null;
                            }
                        }
                        break;
                    case 2:
                        while (vypis != null)
                        {
                            if (vypis == "konzola")
                            {
                                spravaVykazov.ReportCelkoveHodinyZamestnanca();
                                Console.WriteLine("Stlacte lubovolnu klavesu pre pokračovanie");
                                Console.ReadKey();
                                Console.Clear();
                                vypis = null;
                            }
                            else if (vypis == "subor")
                            {
                                Console.WriteLine("Zadajte nazov suboru: ");
                                string nazovMiesta = Console.ReadLine();
                                string nazovSuboru = nazovMiesta + ".txt";
                                Console.Clear();
                                zapisovac = new StreamWriter(nazovSuboru);
                                spravaVykazov.ZapisReportCelkoveHodinyZamestnanca(zapisovac);
                                zapisovac.Close();
                                Console.Clear();
                                UspesneZapisanie();
                                vypis = null;
                            }
                        }
                        break;
                    case 3:
                        while (vypis != null)
                        {
                            if (vypis == "konzola")
                            {
                                spravaVykazov.ReportCelkomHodin();
                                Console.WriteLine("Stlacte lubovolnu klavesu pre pokračovanie");
                                Console.ReadKey();
                                Console.Clear();
                                vypis = null;
                            }
                            else if (vypis == "subor")
                            {
                                Console.WriteLine("Zadajte nazov suboru: ");
                                string nazovMiesta = Console.ReadLine();
                                string nazovSuboru = nazovMiesta + ".txt";
                                Console.Clear();
                                zapisovac = new StreamWriter(nazovSuboru);
                                spravaVykazov.ZapisReportCelkomHodin(zapisovac);
                                zapisovac.Close();
                                Console.Clear();
                                UspesneZapisanie();
                                vypis = null; 
                            }
                        }
                        break;
                    case 4:
                        while (vypis != null)
                        {
                            if (vypis == "konzola")
                            {
                                spravaVykazov.ReportProjektu();
                                Console.WriteLine("Stlacte lubovolnu klavesu pre pokračovanie");
                                Console.ReadKey();
                                Console.Clear();
                                vypis = null;
                            }
                            else if (vypis == "subor")
                            {
                                Console.WriteLine("Zadajte nazov suboru: ");
                                string nazovMiesta = Console.ReadLine();
                                string nazovSuboru = nazovMiesta + ".txt";
                                Console.Clear();
                                zapisovac = new StreamWriter(nazovSuboru);
                                spravaVykazov.ZapisReportProjektu(zapisovac);
                                zapisovac.Close();
                                Console.Clear();
                                UspesneZapisanie();
                                vypis = null;
                            }
                        }
                        break;
                    case 5:
                        while (vypis != null)
                        {
                            if (vypis == "konzola")
                            {
                                spravaVykazov.ReportZamestnanca();
                                Console.WriteLine("Stlacte lubovolnu klavesu pre pokračovanie");
                                Console.ReadKey();
                                Console.Clear();
                                vypis = null;
                            }
                            else if (vypis == "subor")
                            {
                                Console.WriteLine("Zadajte nazov suboru: ");
                                string nazovMiesta = Console.ReadLine();
                                string nazovSuboru = nazovMiesta + ".txt";
                                Console.Clear();
                                zapisovac = new StreamWriter(nazovSuboru);
                                spravaVykazov.ZapisReportZamestnanca(zapisovac);
                                zapisovac.Close();
                                Console.Clear();
                                UspesneZapisanie();
                                vypis = null;
                            }
                        }
                        break;
                    case 6:
                        while (vypis != null)
                        {
                            if (vypis == "konzola")
                            {
                                spravaVykazov.ReportVsetkeho();
                                Console.WriteLine("Stlacte lubovolnu klavesu pre pokračovanie");
                                Console.ReadKey();
                                Console.Clear();
                                vypis = null;
                            }
                            else if (vypis == "subor")
                            {
                                Console.WriteLine("Zadajte nazov suboru: ");
                                string nazovMiesta = Console.ReadLine();
                                string nazovSuboru = nazovMiesta + ".txt";
                                Console.Clear();
                                zapisovac = new StreamWriter(nazovSuboru);
                                spravaVykazov.ZapisReportVsetkeho(zapisovac);
                                zapisovac.Close();
                                Console.Clear();
                                UspesneZapisanie();
                                vypis = null;
                            }
                        }
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("PROGRAM SKONCIL, STISKNUTIM LUBOVOLNEJ KLAVESY ZAVRIETE PROGRAM");
            Console.ReadKey();
        }

        static int ZobrazVyberMenu(string nadpis, string[] polozkyMenu)
        {
            int volba = 0;
            Console.WriteLine("* MENU - {0} *", nadpis);
            while (volba <= 0 || volba > polozkyMenu.Length + 1)
            {
                for(int i = 0; i < polozkyMenu.Length; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, polozkyMenu[i]);
                }
                Console.Write("Vasa volba: ");
                volba = Int32.Parse(Console.ReadLine());
                Console.Clear();

            }
            return volba;
        }

        static void UspesneZapisanie()
        {
            
            for (int i=0; i<30000; i++)
            {
              Console.CursorTop = 0;
              Console.CursorLeft = 0;
              Console.Write("Vypis bol Uspesne zapisany do zvoleneho suboru!!!");
                
                i += 1;
            }
            Console.Clear();
        }
    }
}
