using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zpracovanie_vykazov
{
    public class Projekt
    {
        public string Nazov { get; set; }
        public string Kod { get; set; }
        public int Priorita { get; set; }

        public Dictionary<string, Zamestnanec> Zamestnanci { get; set; }
        public List<PolozkaVykazu> PolozkyVykazov { get; set; }

        public Projekt(string nazov, string kod, int priorita,
            Dictionary<string, Zamestnanec> zamestnanci, List<PolozkaVykazu> polozkyVykazu)
        {
            Nazov = nazov;
            Kod = kod;
            Priorita = priorita;
            Zamestnanci = zamestnanci;
            PolozkyVykazov = polozkyVykazu;
        }
    }
    
    public class Zamestnanec
    {
        public string Meno { get; set; }
        public string Priezvisko { get; set; }
        public string Kod { get; set; }
        public string Pozicia { get; set; }

        public List<Projekt> Projekty { get; set; }
        public List<PolozkaVykazu> PolozkyVykazov { get; set; }

        public Zamestnanec(string meno, string priezvisko, string kod, string pozicia,
            List<Projekt> projekty, List<PolozkaVykazu> polozkyVykazu)
        {
            Meno = meno;
            Priezvisko = priezvisko;
            Kod = kod;
            Pozicia = pozicia;
            Projekty = projekty;
            PolozkyVykazov = polozkyVykazu;
        }
    }

    public class PolozkaVykazu
    {
        public Zamestnanec Zamestnanec { get; set; }
        public Projekt Projekt { get; set; }
        public DateTime Datum { get; set; }
        public int PocetHodin { get; set; }

        public PolozkaVykazu(Zamestnanec zamestnanec, Projekt projekt, DateTime datum, int pocetHodin)
        {
            Zamestnanec = zamestnanec;
            Projekt = projekt;
            Datum = datum;
            PocetHodin = pocetHodin;
        }
    }

    public class SpravaVykazov
    {
        private const string CestaSuboruProjekty = "C:\\Users\\kocak\\Desktop\\Programming\\programovanie v C#\\Aplikacia2\\Zpracovanie_vykazov\\projekty.txt";
        private const string CestaSuboruZamestnanci = "C:\\Users\\kocak\\Desktop\\Programming\\programovanie v C#\\Aplikacia2\\Zpracovanie_vykazov\\zamestnanci.txt";
        private const string CestaSuboruVykazy = "C:\\Users\\kocak\\Desktop\\Programming\\programovanie v C#\\Aplikacia2\\Zpracovanie_vykazov\\vykazy.txt";

        public Dictionary<string, Projekt> Projekty   { get; set; }
        public Dictionary<string, Zamestnanec> Zamestnanci { get; set; }
        public List<PolozkaVykazu> PolozkyVykazov { get; set; }
        public SpravaVykazov()
        {
            Projekty = new Dictionary<string, Projekt>();
            Zamestnanci = new Dictionary<string, Zamestnanec>();
            PolozkyVykazov = new List<PolozkaVykazu>();
        }

        public void ZpracovatVykazy()
        {
            Console.WriteLine("1. Nacitanie projektov...");
            NacitajProjekty();
            Console.WriteLine("1. projekty nacitane!");

            Console.WriteLine("2. Nacitanie zamestnancov...");
            NacitajZamestnancov();
            Console.WriteLine("2. zamestnanci nacitany!");

            Console.WriteLine("3. Nacitanie vykazov...");
            NacitajVykazy();
            Console.WriteLine("3. vykazy nacitane!");

            Console.WriteLine("VSETKO BOLO USPESNE NACITANE!!");
        }

        private void NacitajProjekty()
        {
            StreamReader citac = new StreamReader(CestaSuboruProjekty);
            string riadok = citac.ReadLine();
            while((riadok = citac.ReadLine()) != null)
            {
                var hodnotyRiadok = riadok.Split(';');
                string kod = hodnotyRiadok[0];
                string nazov = hodnotyRiadok[1];
                int priorita = Int32.Parse(hodnotyRiadok[2]);
                Projekt projekt = new Projekt(nazov, kod, priorita, new Dictionary<string, Zamestnanec>(), new List<PolozkaVykazu>());
                Projekty.Add(kod, projekt);
            }
            citac.Close();
        }

        private void NacitajZamestnancov()
        {
            StreamReader citac = new StreamReader(CestaSuboruZamestnanci);
            string riadok = citac.ReadLine();
            while ((riadok = citac.ReadLine()) != null)
            {
                var hodnotyRiadok = riadok.Split(';');
                string kod = hodnotyRiadok[0];
                string meno = hodnotyRiadok[1];
                string priezvisko = hodnotyRiadok[2];
                string pozicia = hodnotyRiadok[3];

                Zamestnanec zamestnanec = new Zamestnanec(meno, priezvisko, kod, pozicia, new List<Projekt>(), new List<PolozkaVykazu>());
                Zamestnanci.Add(kod, zamestnanec);
            }
            citac.Close();
        }

        private void NacitajVykazy()
        {
            StreamReader citac = new StreamReader(CestaSuboruVykazy);

            string riadok = citac.ReadLine();
            while((riadok = citac.ReadLine()) != null)
            {
                var hodnotyRiadok = riadok.Split(';');
                string kodZamestnanec = hodnotyRiadok[0];
                string kodProjekt = hodnotyRiadok[1];

                var strDatumVykazu = hodnotyRiadok[2].Split('.');
                int den = Int32.Parse(strDatumVykazu[0]);
                int mesiac = Int32.Parse(strDatumVykazu[1]);
                int rok = Int32.Parse(strDatumVykazu[2]);
                DateTime datumVykazu = new DateTime(rok, mesiac, den);
                int pocetHodin = Int32.Parse(hodnotyRiadok[3]);

                Zamestnanec zamestnanec = Zamestnanci[kodZamestnanec];
                Projekt projekt = Projekty[kodProjekt];
                PolozkaVykazu polozkaVykazu = new PolozkaVykazu(zamestnanec, projekt, datumVykazu, pocetHodin);
                if (pocetHodin != 0)
                {
                    PolozkyVykazov.Add(polozkaVykazu);
                    projekt.PolozkyVykazov.Add(polozkaVykazu);
                    zamestnanec.PolozkyVykazov.Add(polozkaVykazu);

                    if (!projekt.Zamestnanci.ContainsKey(kodZamestnanec))
                    {
                        projekt.Zamestnanci.Add(kodZamestnanec, zamestnanec);
                    }

                    if (!zamestnanec.Projekty.Contains(projekt))
                    {
                        zamestnanec.Projekty.Add(projekt);
                    }
                }
            }
            citac.Close();
        }

        public void ReportCelkomHodin()
        {
            Console.WriteLine("*** Report - Hodiny celkom ***");
            int sucetHodin = 0;
            foreach(PolozkaVykazu polozkyV in PolozkyVykazov)
            {
                sucetHodin += polozkyV.PocetHodin;
            }
            if(sucetHodin != 0)
            {
                Console.WriteLine("Celkovy pocet hodin: {0}", sucetHodin);
            }
            else if(sucetHodin == 0)
            {
                Console.WriteLine("Ani 1 hodina nebola vypracovana");

            }
        }

        public void ZapisReportCelkomHodin(StreamWriter zapisovac)
        {
            zapisovac.WriteLine("*** Report - Hodiny celkom ***");
            int sucetHodin = 0;
            foreach (PolozkaVykazu polozkyV in PolozkyVykazov)
            {
                sucetHodin += polozkyV.PocetHodin;
            }
            if (sucetHodin != 0)
            {
                zapisovac.WriteLine("Celkovy pocet hodin: {0}", sucetHodin);
            }
            else if (sucetHodin == 0)
            {
                zapisovac.WriteLine("Ani 1 hodina nebola vypracovana");
            }
        }

        public void ReportCelkoveHodinyProjektu()
        {
            int priorita = ZobrazVyberPriority("*** Report - Projekty - celkove odpracovane hodiny ***", Projekty);
            foreach(Projekt projekt in Projekty.Values)
            {

                int sucetHodin = 0;
                foreach(PolozkaVykazu polozkaV in projekt.PolozkyVykazov)
                {
                    sucetHodin += polozkaV.PocetHodin;
                }
                if(priorita == projekt.Priorita)
                {
                    if(sucetHodin == 0)
                    {
                        Console.WriteLine("\tProjekt: {0}({1}) = Na tomto projekte este nikto nepracoval!", projekt.Nazov, projekt.Kod);
                    }
                    else if(sucetHodin != 0)
                    {
                        Console.WriteLine("\tProjekt: {0}({1}) Pocet hodin: {2}", projekt.Nazov, projekt.Kod, sucetHodin);
                    }
                }
            }
        }

        public void ZapisReportCelkoveHodinyProjektu(StreamWriter zapisovac)
        {
            int priorita = ZobrazVyberPriority("*** Report - Projekty - celkove odpracovane hodiny ***", Projekty);
            foreach (Projekt projekt in Projekty.Values)
            {

                int sucetHodin = 0;
                foreach (PolozkaVykazu polozkaV in projekt.PolozkyVykazov)
                {
                    sucetHodin += polozkaV.PocetHodin;
                }
                if (priorita == projekt.Priorita)
                {
                    if (sucetHodin == 0)
                    {
                        zapisovac.WriteLine("\tProjekt: {0}({1}) = Na tomto projekte este nikto nepracoval!", projekt.Nazov, projekt.Kod);
                    }
                    else if (sucetHodin != 0)
                    {
                        zapisovac.WriteLine("\tProjekt: {0}({1}) Pocet hodin: {2}", projekt.Nazov, projekt.Kod, sucetHodin);
                    }
                }
            }
        }

        static int ZobrazVyberPriority(string nadpis, Dictionary<string, Projekt> polozkyMenu)
        {
            int volba = 0;
            Console.WriteLine("* MENU - {0} *", nadpis);
            List<int> hodnotyL = new List<int>();
            var hodnotyN = polozkyMenu.Values.ToArray();

            for(int i = 0; i< hodnotyN.Length; i++)
            {
              hodnotyL.Add(hodnotyN[i].Priorita);
            }
            hodnotyL.Sort();
            hodnotyL.Reverse();

            for(int i=1; i<hodnotyL.Count(); i++)
            {
                if(hodnotyL[i] == hodnotyL[i-1])
                {
                    hodnotyL.RemoveAt(i);
                }    
            }

            var hodnoty = hodnotyL.ToArray();
            while (volba <= 0 || volba > hodnoty.Count() + 1)
            {
                for (int i = 0; i < hodnoty.Count(); i++)
                {
                Console.WriteLine("{0}.Priorita: {1}", i + 1, hodnoty[i]);
                }
                Console.Write("Vasa volba: ");
                volba = Int32.Parse(Console.ReadLine());
                Console.Clear();

            }
            for (int i = 0; i < hodnoty.Count(); i++)
            {
                if(volba-1 == i)
                {
                    Console.Write(hodnoty[i]);
                    volba = hodnoty[i];
                    return volba;
                }
            }
            return volba;
        }

        public void ReportCelkoveHodinyZamestnanca()
        {
            Console.WriteLine("*** Report - Zamestnanci - celkove odpracovane hodiny ***");
            foreach(Zamestnanec zamestnanec in Zamestnanci.Values)
            {
                int sucetHodin = 0;
                foreach(PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                {
                    sucetHodin += polozkaV.PocetHodin;
                }
                if (sucetHodin > 0)
                {
                    Console.WriteLine("\tZamestnanec: {0} {1}({2}) Poziciou: {3} odpracoval hodin: {4}", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, zamestnanec.Pozicia, sucetHodin);
                }
                else
                {
                    Console.WriteLine("\tZamestnanec: {0} {1}({2}) Poziciou: {3} neodpracoval vobec nic. Vyhodte ho!!!", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, zamestnanec.Pozicia);
                }
            }
        }

        public void ZapisReportCelkoveHodinyZamestnanca(StreamWriter zapisovac)
        {
            foreach (Zamestnanec zamestnanec in Zamestnanci.Values)
            {
                int sucetHodin = 0;
                foreach (PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                {
                    sucetHodin += polozkaV.PocetHodin;
                }
                if (sucetHodin > 0)
                {
                    zapisovac.WriteLine("\tZamestnanec: {0} {1}({2}) Poziciou: {3} odpracoval hodin: {4}", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, zamestnanec.Pozicia, sucetHodin);
                }
                else
                {
                    zapisovac.WriteLine("\tZamestnanec: {0} {1}({2}) Poziciou: {3} neodpracoval vobec nic. Vyhodte ho!!!", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, zamestnanec.Pozicia);
                }
            }
        }

        public void ReportZamestnanca()
        {
            Console.WriteLine("*** Report - Zamestnanec - detail ***");
            Console.WriteLine("Vyberte zamestnanca: ");

            List<Zamestnanec> zamestnanci = Zamestnanci.Values.ToList();
            for(int i = 0; i < zamestnanci.Count; i++)
            {
                Console.WriteLine("\t{0}. {1} {2} ({3}) [{4}]", i + 1, zamestnanci[i].Meno, zamestnanci[i].Priezvisko, zamestnanci[i].Kod, zamestnanci[i].Pozicia);
            }

            Console.Write("Vasa volba: ");
            int volba = Int32.Parse(Console.ReadLine()) - 1;
            Zamestnanec zamestnanec = zamestnanci[volba];
            Console.Clear();

            Console.WriteLine("\nReport - {0} {1} ({2}) \nPROJEKTY: ", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod);
            
            foreach(Projekt projekt in zamestnanec.Projekty)
            {
                
                int sucet = 0;
                foreach(PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                {
                    if(polozkaV.Projekt == projekt)
                    {
                        sucet += polozkaV.PocetHodin;
                    }
                }
               Console.WriteLine("\t{0} ({1}): {2}", projekt.Nazov, projekt.Kod, sucet);
            }
            if(zamestnanec.Projekty.Count == 0)
            {
                Console.WriteLine("\tZiadne projekty!");
            }
        }

        public void ZapisReportZamestnanca(StreamWriter zapisovac)
        {
            zapisovac.WriteLine("*** Report - Zamestnanec - detail ***");
            Console.WriteLine("Vyberte zamestnanca: ");

            List<Zamestnanec> zamestnanci = Zamestnanci.Values.ToList();
            for (int i = 0; i < zamestnanci.Count; i++)
            {
                Console.WriteLine("\t{0}. {1} {2} ({3}) [{4}]", i + 1, zamestnanci[i].Meno, zamestnanci[i].Priezvisko, zamestnanci[i].Kod, zamestnanci[i].Pozicia);
            }

            Console.Write("Vasa volba: ");
            int volba = Int32.Parse(Console.ReadLine()) - 1;
            Zamestnanec zamestnanec = zamestnanci[volba];
            zapisovac.WriteLine("\nReport - {0} {1} ({2}) \nPROJEKTY: ", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod);
            foreach (Projekt projekt in zamestnanec.Projekty)
            {
                
                int sucet = 0;
                foreach (PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                {
                    if (polozkaV.Projekt == projekt)
                    {
                        sucet += polozkaV.PocetHodin;
                    }
                }
               
                zapisovac.WriteLine("\t{0} ({1}): {2}", projekt.Nazov, projekt.Kod, sucet);
            }
            if (zamestnanec.Projekty.Count == 0)
            {
                zapisovac.WriteLine("\tTento zamestnanec nema ziadne projekty!");
            }
            zapisovac.Close();
        }

        public void ReportProjektu()
        {
            Console.WriteLine("*** Report - Projekt - detail ***");
            Console.WriteLine("Vyberte projekt: ");

            List<Projekt> projekty = Projekty.Values.ToList();
            for (int i = 0; i < projekty.Count; i++)
            {
                Console.WriteLine("\t{0}. {1} {2}", i + 1, projekty[i].Nazov, projekty[i].Kod);
            }

            Console.Write("Vasa volba: ");
            int volba = Int32.Parse(Console.ReadLine()) - 1;
            Projekt projekt = projekty[volba];
            Console.Clear();

            Console.WriteLine("\nReport - {0}({1}) \nZAMESTNANCI: ", projekt.Nazov, projekt.Kod);
            foreach (Zamestnanec zamestnanec in projekt.Zamestnanci.Values)
            {
                int sucet = 0;
                foreach (PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                {
                    if (polozkaV.Projekt == projekt)
                    {
                        sucet += polozkaV.PocetHodin;
                    }
                }
                Console.WriteLine("\t{0} {1} ({2}): {3}", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, sucet);
            }
            if(projekt.Zamestnanci.Values.Count == 0)
            {
                Console.WriteLine("\tNa tomto projekte nikto nepracoval");
            }
        }

        public void ZapisReportProjektu(StreamWriter zapisovac)
        {
            zapisovac.WriteLine("*** Report - Projekt - detail ***");
            Console.WriteLine("Vyberte projekt: ");

            List<Projekt> projekty = Projekty.Values.ToList();
            for (int i = 0; i < projekty.Count; i++)
            {
                Console.WriteLine("\t{0}. {1} {2}", i + 1, projekty[i].Nazov, projekty[i].Kod);
            }

            Console.Write("Vasa volba: ");
            int volba = Int32.Parse(Console.ReadLine()) - 1;
            Projekt projekt = projekty[volba];

            zapisovac.WriteLine("\nReport - {0}({1}) \nZAMESTNANCI: ", projekt.Nazov, projekt.Kod);
            foreach (Zamestnanec zamestnanec in projekt.Zamestnanci.Values)
            {
                int sucet = 0;
                foreach (PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                {
                    if (polozkaV.Projekt == projekt)
                    {
                        sucet += polozkaV.PocetHodin;
                    }
                }
                
                zapisovac.WriteLine("\t{0} ({1}): {2}", projekt.Nazov, projekt.Kod, sucet);
  
            }
            if (projekt.Zamestnanci.Values.Count == 0)
            {
                zapisovac.WriteLine("\tNa tomto projekte nikto nepracoval!");
            }
            if (zapisovac != null)
            {
                Console.WriteLine("Vypis dokonceny");
                zapisovac.Close();
            }
            }

        public void ReportVsetkeho()
        {
            Console.WriteLine("*** Report - Hodiny celkom ***");
            int sucetHodin = 0;
            foreach (PolozkaVykazu polozkyV in PolozkyVykazov)
            {
                sucetHodin += polozkyV.PocetHodin;

            }
            Console.WriteLine("Celkovy pocet odpracovanych hodin: {0}", sucetHodin);
            Console.Write("\n*****************************************************************************************\n");
            Console.WriteLine("Z toho odpracoval: ");

            List<Zamestnanec> zams = Zamestnanci.Values.ToList();
            Console.WriteLine("Pracovalo sa na projektoch: ");
            for (int a = 0; a < zams.Count; a++)
            {
                List<Projekt> projkt = zams[a].Projekty.ToList();
                int totalHodin = 0;
                for (int b = 0; b < PolozkyVykazov.Count; b++)
                {
                    if (PolozkyVykazov[b].Zamestnanec == zams[a])
                    {
                        totalHodin += PolozkyVykazov[b].PocetHodin;
                    }
                }
                if (totalHodin > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} {1} ({2}) [{3}] {4}", zams[a].Meno, zams[a].Priezvisko, zams[a].Kod, zams[a].Pozicia, totalHodin);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Tento zamestnanec pracoval na tychto projektoch:");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                for (int c = 0; c < projkt.Count(); c++)
                {
                    int totalHodProj = 0;
                    for (int d = 0; d < PolozkyVykazov.Count; d++)
                    {
                        if (PolozkyVykazov[d].Projekt == projkt[c] && PolozkyVykazov[d].Zamestnanec == zams[a])
                        {
                            totalHodProj += PolozkyVykazov[d].PocetHodin;
                        }
                    }
                    Console.WriteLine("{0} {1} (Priorita = {2}) Celkovy pocet hodin: {3}", projkt[c].Nazov, projkt[c].Kod, projkt[c].Priorita, totalHodProj);
                }
            }
            Console.Write("\n*****************************************************************************************\n");

            List<Projekt> proj = Projekty.Values.ToList();
            Console.WriteLine("Pracovalo sa na projektoch: ");
            for (int i = 0; i < proj.Count; i++)
            {
                List<Zamestnanec> zam = proj[i].Zamestnanci.Values.ToList();
                int totalHod = 0;
                for (int y = 0; y < PolozkyVykazov.Count; y++)
                {
                    if(PolozkyVykazov[y].Projekt == proj[i])
                    {
                        totalHod += PolozkyVykazov[y].PocetHodin;
                    }
                }
                if(totalHod >0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} {1} (Priorita = {2}) Celkovy pocet hodin: {3}", proj[i].Nazov, proj[i].Kod, proj[i].Priorita, totalHod);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Na tomto projekte pracovali:");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                for (int x = 0; x < proj[i].Zamestnanci.Count; x++)
                {
                    int totalHodZam = 0;
                    for (int z = 0; z < PolozkyVykazov.Count; z++)
                    {
                        if (PolozkyVykazov[z].Zamestnanec == zam[x] && PolozkyVykazov[z].Projekt == proj[i])
                        {
                            totalHodZam += PolozkyVykazov[z].PocetHodin;
                        }
                    }
                    Console.WriteLine("{0} {1} ({2}) [{3}] {4}", zam[x].Meno, zam[x].Priezvisko, zam[x].Kod, zam[x].Pozicia, totalHodZam);
                }
            }
            Console.Write("\n*****************************************************************************************\n");

            int projekty = 0;
            foreach (Projekt projekt in Projekty.Values)
            {
                projekty += 1;
                int sucetHodin1 = 0;
                foreach (PolozkaVykazu polozkaV in projekt.PolozkyVykazov)
                {
                    sucetHodin1 += polozkaV.PocetHodin;
                }
               Console.WriteLine("\t{3}.Projekt: {0}({1}) Pocet hodin: {2}", projekt.Nazov, projekt.Kod, sucetHodin1, projekty);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n*****************************************************************************************\n");

            List<Zamestnanec> zamestnanci = Zamestnanci.Values.ToList();
            foreach (Zamestnanec zamestnanec in zamestnanci)
            {
                Console.WriteLine("\nReport - {0} {1} ({2}) \nPROJEKTY: ", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod);
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (Projekt projekt in zamestnanec.Projekty)
                {
                    int sucet = 0;
                    foreach (PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                    {
                        if (polozkaV.Projekt == projekt)
                        {
                            sucet += polozkaV.PocetHodin;
                        }
                    }
                    Console.WriteLine("\t{0} ({1}): {2} hodin", projekt.Nazov, projekt.Kod, sucet);
                }
                if(zamestnanec.Projekty.Count == 0)
                {
                    Console.WriteLine("\tZAMESTNANEC NEMA ZIADNE PROJEKTY!!!");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write("\n*****************************************************************************************\n");

            Console.WriteLine("*** Report - Zamestnanci - celkove odpracovane hodiny ***");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Zamestnanec zamestnanec in Zamestnanci.Values)
            {
                int suc = 0;
                foreach (PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                {
                    suc += polozkaV.PocetHodin;
                }
                if (suc > 0)
                {
                    Console.WriteLine("\tZamestnanec: {0} {1}({2}) Poziciou: {3} odpracoval hodin: {4}", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, zamestnanec.Pozicia, suc);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tZamestnanec: {0} {1}({2}) Poziciou: {3} neodpracoval vobec nic. Vyhodte ho!!!", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, zamestnanec.Pozicia);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n*****************************************************************************************\n");

        }

        public void ZapisReportVsetkeho(StreamWriter zapisovac)
        {
            zapisovac.WriteLine("*** Report - Hodiny celkom ***");
            int sucetHodin = 0;
            foreach (PolozkaVykazu polozkyV in PolozkyVykazov)
            {
                sucetHodin += polozkyV.PocetHodin;

            }
            zapisovac.WriteLine("Celkovy pocet odpracovanych hodin: {0}", sucetHodin);

            zapisovac.Write("\n*****************************************************************************************\n");
            zapisovac.WriteLine("Z toho odpracoval: ");
            List<Zamestnanec> zams = Zamestnanci.Values.ToList();
            zapisovac.WriteLine("Pracovalo sa na projektoch: ");
            for (int a = 0; a < zams.Count; a++)
            {
                List<Projekt> projkt = zams[a].Projekty.ToList();
                int totalHodin = 0;
                for (int b = 0; b < PolozkyVykazov.Count; b++)
                {
                    if (PolozkyVykazov[b].Zamestnanec == zams[a])
                    {
                        totalHodin += PolozkyVykazov[b].PocetHodin;
                    }
                }
                if (totalHodin > 0)
                {
                    zapisovac.WriteLine("{0} {1} ({2}) [{3}] {4}", zams[a].Meno, zams[a].Priezvisko, zams[a].Kod, zams[a].Pozicia, totalHodin);
                    zapisovac.WriteLine("Tento zamestnanec pracoval na tychto projektoch:");
                }

                for (int c = 0; c < projkt.Count(); c++)
                {
                    int totalHodProj = 0;
                    for (int d = 0; d < PolozkyVykazov.Count; d++)
                    {
                        if (PolozkyVykazov[d].Projekt == projkt[c] && PolozkyVykazov[d].Zamestnanec == zams[a])
                        {
                            totalHodProj += PolozkyVykazov[d].PocetHodin;
                        }
                    }
                    zapisovac.WriteLine("{0} {1} (Priorita = {2}) Celkovy pocet hodin: {3}", projkt[c].Nazov, projkt[c].Kod, projkt[c].Priorita, totalHodProj);
                }
            }
            zapisovac.Write("\n*****************************************************************************************\n");

            List<Projekt> proj = Projekty.Values.ToList();

            zapisovac.WriteLine("Pracovalo sa na projektoch: ");
            for (int i = 0; i < proj.Count; i++)
            {
                List<Zamestnanec> zam = proj[i].Zamestnanci.Values.ToList();
                int totalHod = 0;
                for (int y = 0; y < PolozkyVykazov.Count; y++)
                {
                    if (PolozkyVykazov[y].Projekt == proj[i])
                    {
                        totalHod += PolozkyVykazov[y].PocetHodin;
                    }
                }
                if (totalHod > 0)
                {
                    zapisovac.WriteLine("{0} {1} (Priorita = {2}) Celkovy pocet hodin: {3}", proj[i].Nazov, proj[i].Kod, proj[i].Priorita, totalHod);
                    zapisovac.WriteLine("Na tomto projekte pracovali:");
                }

                for (int x = 0; x < proj[i].Zamestnanci.Count; x++)
                {
                    int totalHodZam = 0;
                    for (int z = 0; z < PolozkyVykazov.Count; z++)
                    {
                        if (PolozkyVykazov[z].Zamestnanec == zam[x] && PolozkyVykazov[z].Projekt == proj[i])
                        {
                            totalHodZam += PolozkyVykazov[z].PocetHodin;
                        }
                    }
                    zapisovac.WriteLine("{0} {1} ({2}) [{3}] {4}", zam[x].Meno, zam[x].Priezvisko, zam[x].Kod, zam[x].Pozicia, totalHodZam);
                }
            }
            zapisovac.Write("\n*****************************************************************************************\n");

            int projekty = 0;
            foreach (Projekt projekt in Projekty.Values)
            {
                projekty += 1;
                int sucetHodin1 = 0;
                foreach (PolozkaVykazu polozkaV in projekt.PolozkyVykazov)
                {
                    sucetHodin1 += polozkaV.PocetHodin;
                }
                zapisovac.WriteLine("\t{3}.Projekt: {0}({1}) Pocet hodin: {2}", projekt.Nazov, projekt.Kod, sucetHodin1, projekty);
            }
            zapisovac.Write("\n*****************************************************************************************\n");

            List<Zamestnanec> zamestnanci = Zamestnanci.Values.ToList();
            foreach (Zamestnanec zamestnanec in zamestnanci)
            {
                zapisovac.WriteLine("\nReport - {0} {1} ({2}) \nPROJEKTY: ", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod);
                foreach (Projekt projekt in zamestnanec.Projekty)
                {
                    int sucet = 0;
                    foreach (PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                    {
                        if (polozkaV.Projekt == projekt)
                        {
                            sucet += polozkaV.PocetHodin;
                        }
                    }
                    zapisovac.WriteLine("\t{0} ({1}): {2} hodin", projekt.Nazov, projekt.Kod, sucet);
                }
                if (zamestnanec.Projekty.Count == 0)
                {
                    zapisovac.WriteLine("\tZAMESTNANEC NEMA ZIADNE PROJEKTY!!!");
                }
            }
            zapisovac.Write("\n*****************************************************************************************\n");

            zapisovac.WriteLine("*** Report - Zamestnanci - celkove odpracovane hodiny ***");
            foreach (Zamestnanec zamestnanec in Zamestnanci.Values)
            {
                int suc = 0;
                foreach (PolozkaVykazu polozkaV in zamestnanec.PolozkyVykazov)
                {
                    suc += polozkaV.PocetHodin;
                }
                if (suc > 0)
                {
                    zapisovac.WriteLine("\tZamestnanec: {0} {1}({2}) Poziciou: {3} odpracoval hodin: {4}", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, zamestnanec.Pozicia, suc);
                }
                else
                {
                    zapisovac.WriteLine("\tZamestnanec: {0} {1}({2}) Poziciou: {3} neodpracoval vobec nic. Vyhodte ho!!!", zamestnanec.Meno, zamestnanec.Priezvisko, zamestnanec.Kod, zamestnanec.Pozicia);
                }
            }
            zapisovac.Write("\n*****************************************************************************************\n");
        }

        public string KonzolaSubor()
        {
            Console.Clear();
            Console.WriteLine("Chcete vypisat report na konzolu alebo ulozit do suboru?");
            string[] konzolaSubor = { "konzola", "subor", "zpet" };
            int volba = 0;

            while (volba <= 0 || volba > konzolaSubor.Length + 2)
            {
                for (int i = 0; i < konzolaSubor.Length; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, konzolaSubor[i]);
                }
                Console.Write("Vasa volba: ");
                volba = Int32.Parse(Console.ReadLine());
                Console.Clear();
            }

            if (volba == 1)
            {
                string vypisanie = "konzola";
                return vypisanie;
            }
            else if(volba == 2)
            {
                string vypisanie = "subor";
                return vypisanie;
            }
            else
            {
                return null;
            }
        }
    }
}
