using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> ListAsync();
        Task<Currency> GetSingleByIdAsync(int id);
        void Remove(Currency currency);
    }
}
