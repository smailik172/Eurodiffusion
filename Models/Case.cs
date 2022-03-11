using System;
using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion.Models
{
    class Case
    {
        private List<Country> _countries { get; set; }
        private bool isComplete = false;
        public Case()
        {
            _countries = new List<Country>();
        }

        public void AddCountry(Country country)
        {
            foreach (var oldCountry in _countries)
            {
                oldCountry.CreateRelationshipBetweenCountry(country);
            }
            _countries.Add(country);
        }
        public void Start()
        {
            if (isComplete)
                throw new Exception();
            isComplete = true;
            int iterations = 0;
            bool allCountryComplete = false;
            while (!allCountryComplete)
            {
                allCountryComplete = true;
                //Проверяем, что все страны завершены
                foreach (var country in _countries)
                {
                    allCountryComplete &= country.CountryIsComplete(_countries.Count, iterations);
                }
                if (allCountryComplete)
                    break;
                //Инициализируем монеты, которые будем передавать
                foreach (var country in _countries)
                {
                    country.InitStartDay();
                }
                //передаем монеты
                foreach (var country in _countries)
                {
                    country.SendCoins();
                }
                iterations++;
            }
        }

        public IList<Country> GetSortedList()
        {
            CountryComparable countryComparable = new CountryComparable();
            List<Country> countries = _countries.ToList();
            countries.Sort(countryComparable);
            return countries;
        }
    }
}
