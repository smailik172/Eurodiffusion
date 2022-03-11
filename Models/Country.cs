using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Eurodiffusion.Models
{
    class Country
    {
        public int Iterations { get; private set; }
        private Dictionary<MyVector2, City> Cities { get; set; }
        public string CountryName { get; }
        public Country(string countryName, int xl, int yl, int xh, int yh)
        {
            CountryName = countryName;
            Cities = new Dictionary<MyVector2, City>();
            for (int x=xl; x<=xh; x++)
                for (int y=yl; y<=yh; y++)
                {
                    AddCity(new City(countryName), new MyVector2(x, y));
                }
        }
        public void AddCity(City city, MyVector2 coord)
        {
            Cities.Add(coord, city);
            UpdateNeighbor(city, new MyVector2(coord.X + 1, coord.Y));
            UpdateNeighbor(city, new MyVector2(coord.X - 1, coord.Y));
            UpdateNeighbor(city, new MyVector2(coord.X, coord.Y + 1));
            UpdateNeighbor(city, new MyVector2(coord.X, coord.Y - 1));
        }

        public void CreateRelationshipBetweenCountry(Country country)
        {
            foreach (var city in country.Cities)
            {
                UpdateNeighbor(city.Value, new MyVector2(city.Key.X + 1, city.Key.Y));
                UpdateNeighbor(city.Value, new MyVector2(city.Key.X - 1, city.Key.Y));
                UpdateNeighbor(city.Value, new MyVector2(city.Key.X, city.Key.Y + 1));
                UpdateNeighbor(city.Value, new MyVector2(city.Key.X, city.Key.Y - 1));
            } 
        }

        public bool CountryIsComplete(int countCountry, int iteration)
        {
            if (Iterations > 0)
                return true;
            if (Cities.Count == Cities.Where(city => city.Value.IsComplete(countCountry)).Count())
            {
                Iterations = iteration;
                return true;
            }
            return false;
        }

        public void InitStartDay()
        {
            foreach (var city in Cities)
            {
                city.Value.InitRepresentative();
            }
        }
        public void SendCoins()
        {
            foreach (var city in Cities)
            {
                city.Value.SendCoinsToNeighbor();
            }
        }
        private void UpdateNeighbor(City city, MyVector2 coordNeighbor)
        {
            if (Cities.ContainsKey(coordNeighbor))
            {
                Cities[coordNeighbor].AddNeighbor(city);
                city.AddNeighbor(Cities[coordNeighbor]);
            }
        }
    }
}
