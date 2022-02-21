using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace metjelentes
{
    class Program
    {
        class Adat
        {
            public string telepules;
            public DateTime dt;
            public TimeSpan ts;
            public string ora;
            public string perc;
            public string szelirany;
            public string szelerosseg;
            public int homerseklet;
            public static List<Adat> lista = new List<Adat>();
        }
        static void Main(string[] args)
        {
            string[] sorok = File.ReadAllLines("tavirathu13.txt"); // Itt eltároltuk a memóriában az egészet
            foreach (string sor in sorok)
            {
                string[] sortömb = sor.Split(' ');
                Adat a = new Adat();
                a.telepules = sortömb[0];
                a.ora = sortömb[1].Substring(0, 2);
                a.perc = sortömb[1].Substring(2, 2);
                // Különböző módok dátum beolvasására
                a.dt = new DateTime(1, 1, 1, int.Parse(a.ora), int.Parse(a.perc), 0);
                a.ts = new TimeSpan(int.Parse(a.ora), int.Parse(a.perc), 0); // Ha műveleteket is akarunk végezni a dátumokkal
                a.szelirany = sortömb[2].Substring(0, 3);
                a.szelerosseg = sortömb[2].Substring(3, 2);
                a.homerseklet = int.Parse(sortömb[3]);
                Adat.lista.Add(a);
            }
            Console.WriteLine(Adat.lista.Count);
        }
    }
}
