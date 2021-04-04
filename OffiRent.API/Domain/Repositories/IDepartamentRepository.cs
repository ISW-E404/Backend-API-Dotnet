using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface IDepartamentRepository
    {
        Task<IEnumerable<Departament>> ListAsync();
        Task<IEnumerable<Departament>> ListByCountryIdAsync(int countryId);
        Task AddAsync(Departament departament);
        Task<Departament> FindById(int id);
        void Update(Departament departament);
        void Remove(Departament departament);
    }
}
