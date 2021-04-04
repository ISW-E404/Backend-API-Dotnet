using System;
using System.Collections.Generic;


namespace OffiRent.API.Domain.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char Symbol { get; set; }

        public List<CountryCurrency> CountryCurrencies { get; set; } = new List <CountryCurrency> ();
    }
}
