using Eurodiffusion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Eurodiffusion
{
    class Program
    {
        public const int CountCoinInStartDay = 1000000;
        static void Main(string[] args)
        {
            List<Case> cases = new List<Case>();
            using(var file = new StreamReader("euro.in"))
            {
                var s = file.ReadLine();
                while(s != null)
                {
                    if (!int.TryParse(s, out int countCountry))
                    {
                        Console.WriteLine("Bad data");
                        return;
                    }
                    if (countCountry == 0)
                        break;
                    Case currCase = new Case();
                    for (int i=0; i < countCountry; i++)
                    {
                        var data = file.ReadLine().Split(' ').Where(str => str.Length > 0).ToList();
                        string name = data[0];
                        int xl = int.Parse(data[1]);
                        int yl = int.Parse(data[2]);
                        int xh = int.Parse(data[3]);
                        int yh = int.Parse(data[4]);
                        currCase.AddCountry(new Country(name, xl, yl, xh, yh));
                    }
                    cases.Add(currCase);
                    s = file.ReadLine();
                }
            }
            int iter = 1;
            foreach (var currCase in cases)
            {
                currCase.Start();
                var resultCountries = currCase.GetSortedList();
                Console.WriteLine($"Case Number {iter}");
                foreach (var country in resultCountries)
                {
                    Console.WriteLine($"    {country.CountryName} {country.Iterations}");
                }
            }
            Console.ReadKey();
        }
    }
}
