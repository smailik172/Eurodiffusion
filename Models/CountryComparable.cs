using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurodiffusion.Models
{
    class CountryComparable : IComparer<Country>
    {
        public int Compare(Country x, Country y)
        {
            if (x.Iterations != y.Iterations)
                return x.Iterations - y.Iterations;
            return x.CountryName.CompareTo(y.CountryName);
        }
    }
}
