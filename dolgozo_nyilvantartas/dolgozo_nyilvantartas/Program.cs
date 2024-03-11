using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dolgozo_nyilvantartas
{
    internal class Program
    {      
        private static async Task Main(string[] args)
        {
            List<Dolgozok> dolgozok = new List<Dolgozok>();
            dolgozok = await dolgozo();
            int count = CountElements(dolgozok);
            Console.WriteLine($"Az összes elemek száma :{ count}");

            var legmagasabbFizetesuDolgozo = dolgozok.OrderByDescending(d => d.DolgozokSalary).FirstOrDefault();
            Dolgozok legjobbanKereso = dolgozok.Find(a => a.DolgozokSalary == dolgozok.Max(d => d.DolgozokSalary));
            if (legmagasabbFizetesuDolgozo != null)
            {
                Console.WriteLine($"A legmagasabb fizetéssel rendelkező dolgozó neve: {legjobbanKereso.DolgozokName}");
                Console.WriteLine($"A fizetése:{legmagasabbFizetesuDolgozo.Salary}");
            }
            else
            {
                Console.WriteLine("Nincs dolgozó az adatok között.");
            }

            Console.ReadKey();
        }

        private static async Task<List<Dolgozok>> dolgozo()
        {
            List<Dolgozok> dolgozok = new List<Dolgozok>();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://retoolapi.dev/Kc6xuH/data");
            if (response.IsSuccessStatusCode)
            {               
                string jsonString = await response.Content.ReadAsStringAsync();
                dolgozok.AddRange(Dolgozok.FromJson(jsonString));
            }
            return dolgozok;
        }
        static int CountElements<T>(List<T> list)
        {
            return list.Count;
        }
       

    }
}
