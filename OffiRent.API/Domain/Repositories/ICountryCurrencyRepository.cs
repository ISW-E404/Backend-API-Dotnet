using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface ICountryCurrencyRepository
    {
        Task<IEnumerable<CountryCurrency>> ListAsync();
        Task<IEnumerable<CountryCurrency>> ListByCountryIdAsync(int countryId);
        Task<IEnumerable<CountryCurrency>> ListByCurrencyIdAsync(int currencyId);
        Task<CountryCurrency> FindByCountryIdAndCurrencyId(int countryId, int currencyId);
        Task AddAsync(CountryCurrency countryCurrencyId);
        void Remove(CountryCurrency countryCurrency);
        Task AssignCountryCurrency(int countryId, int currencyId);
        void UnassignCountryCurrency(int countryId, int currencyId);
    }
}
