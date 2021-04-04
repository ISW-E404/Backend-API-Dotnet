using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Persistence.Contexts;
using OffiRent.API.Domain.Repositories;

namespace OffiRent.API.Persistence.Repositories
{
    public class CountryCurrencyRepository : BaseRepository, ICountryCurrencyRepository
    {
        public CountryCurrencyRepository(AppDbContext context) : base(context)
        {

        }
        public async Task AddAsync(CountryCurrency countryCurrency)
        {
            await _context.CountryCurrencies.AddAsync(countryCurrency);
        }
      
        public async Task AssignCountryCurrency(int countryId, int currencyId)
        {
            CountryCurrency countryCurrency = await FindByCountryIdAndCurrencyId(countryId, 
                currencyId);
            if (countryCurrency == null)
            {
                countryCurrency = new CountryCurrency
                {
                    CountryId = countryId,
                    CurrencyId = currencyId
                };
                await AddAsync(countryCurrency);
            }

        }

        public async Task<CountryCurrency> FindByCountryIdAndCurrencyId(int countryId, int currencyId)
        {
            return await _context.CountryCurrencies.FindAsync(countryId, currencyId);
        }

        public async Task<IEnumerable<CountryCurrency>> ListAsync()
        {
            return await _context.CountryCurrencies
                .Include(p => p.Currency)
                .Include(p => p.Country).ToListAsync();
        }

        public async Task<IEnumerable<CountryCurrency>> ListByCountryIdAsync(int countryId)
        {
            return await _context.CountryCurrencies
                .Where(p => p.CountryId == countryId)
                .Include(p => p.Country)
                .Include(p => p.Currency)
                .ToListAsync();
        }

        public async Task<IEnumerable<CountryCurrency>> ListByCurrencyIdAsync(int currencyId)
        {
            return await _context.CountryCurrencies
                .Where(p => p.CurrencyId == currencyId)
                .Include(p => p.Country)
                .Include(p => p.Currency)
                .ToListAsync();
        }

        public void Remove(CountryCurrency countrycurrency)
        {
            _context.CountryCurrencies.Remove(countrycurrency);
        }

        public async void UnassignCountryCurrency(int countryId, int currencyId)
        {
            CountryCurrency countryCurrency = await FindByCountryIdAndCurrencyId(countryId, currencyId);
            if (countryCurrency != null)
            {
                Remove(countryCurrency);
            }
        }
    }
}
