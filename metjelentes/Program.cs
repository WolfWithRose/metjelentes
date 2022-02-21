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
            
            Console.Write("2. feladat \nAdja meg a település kódját! Település: ");
            string user_telepules = Console.ReadLine();

            // hagyományos megoldás:
            // keressük hátulról nézve az első rekordot, aminek a user_telepules a települése

            int i = Adat.lista.Count - 1;
            while (0<=i && !(Adat.lista[i].telepules == user_telepules))
            {
                i--;
            }
            if (i!=-1)
            {
                Console.WriteLine($"Az utolsó mérési adat a megadott településről {Adat.lista[i].ora}:{Adat.lista[i].perc}-kor érkezett.");
            }
            else
            {
                Console.WriteLine("Ilyen település nincsen!");
            }
            // 2. feladat: linq-kel
            {
                Adat az = Adat.lista.Last(a => a.telepules == user_telepules);
                //Console.WriteLine($"{az.ora}:{az.perc}");

                // 2. feladat DateTime-al
                //Console.WriteLine($"{az.dt.ToString(@"HH:mm")}");
            }

            // 3. feladat: MIN-MAX
            Console.WriteLine("3. feladat");
            int legnagyobb = Adat.lista.Max(a => a.homerseklet);
            int legkisebb = Adat.lista.Min(a => a.homerseklet);
            Adat b = Adat.lista.First(a => a.homerseklet == legkisebb);
            Adat c = Adat.lista.First(a => a.homerseklet == legnagyobb);
            Console.WriteLine($"A legalacsonyabb hőmérséklet: {b.telepules} {b.dt.ToString(@"HH:mm")} {b.homerseklet} fok.");
            Console.WriteLine($"A legnagyobb hőmérséklet: {c.telepules} {c.dt.ToString(@"HH:mm")} {c.homerseklet} fok.");

            // 3. feladat: ORDERBY
            {
                var rendezett = Adat.lista.OrderBy(a => a.homerseklet);
                //Console.WriteLine($"A legalacsonyabb hőmérséklet: {rendezett.First().telepules} {rendezett.First().dt.ToString(@"HH:mm")} {rendezett.First().homerseklet} fok.");
                //Console.WriteLine($"A legnagyobb hőmérséklet: {rendezett.Last().telepules} {rendezett.Last().dt.ToString(@"HH:mm")} {rendezett.Last().homerseklet} fok.");
            }

            Console.WriteLine("4. feladat");
            int db = 0;
            foreach (var item in Adat.lista.Where(a => a.szelirany == "000" && a.szelerosseg == "00"))
            {
                db++;
                Console.WriteLine($"{item.telepules} {item.dt.ToString(@"HH:mm")}");
            }
            if (db == 0)
            {
                Console.WriteLine("Nem volt szélcsend a mérések idején.");
            }

            // 5. feladat GROUPBY de Dictionary-vel!
        }
    }
}
