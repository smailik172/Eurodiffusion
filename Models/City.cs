using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Eurodiffusion.Models
{
    class City
    {
        /// <summary>
        /// Key - country of coin, value - count
        /// </summary>
        private Dictionary<string, int> _coins { get; set; }
        private Dictionary<string, int> _startDayRepresentative { get; set; }
        private List<City> _neighbor { get; set; }
        public City(string country)
        {
            _coins = new Dictionary<string, int> { [country] = Program.CountCoinInStartDay };
            _startDayRepresentative = new Dictionary<string, int>();
            _neighbor = new List<City>();
        }
        public void InitRepresentative()
        {
            foreach (var coin in _coins)
            {
                if (_startDayRepresentative.ContainsKey(coin.Key))
                    _startDayRepresentative[coin.Key] = (coin.Value / 1000);
                else
                    _startDayRepresentative.Add(coin.Key, (coin.Value / 1000));
            }
        }

        public void SendCoinsToNeighbor()
        {
            foreach (var neighbor in _neighbor)
            {
                neighbor.GetCoinsFromNeighbor(_startDayRepresentative);
            }
            foreach (var coin in _startDayRepresentative)
            {
                _coins[coin.Key] -= _neighbor.Count * coin.Value;
            }
        }
        public void GetCoinsFromNeighbor(IDictionary<string, int> coins)
        {
            foreach (var coin in coins)
            {
                if (_coins.ContainsKey(coin.Key))
                    _coins[coin.Key] += coin.Value;
                else
                    _coins.Add(coin.Key, coin.Value);
            }
        }

        public void AddNeighbor(City city)
        {
            _neighbor.Add(city);
        }

        public bool IsComplete(int country)
        {
            return _coins.Where(coin => coin.Value > 0).Count() == country;
        }
    }
}
