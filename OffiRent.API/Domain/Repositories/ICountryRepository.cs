using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> ListAsync();
        Task AddAsync(Country country);
        Task<Country> GetSingleByIdAsync(int id);
        void Remove(Country country);   
    }
}
