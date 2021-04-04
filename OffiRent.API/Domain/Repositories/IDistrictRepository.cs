using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<District>> ListAsync();
        Task<IEnumerable<District>> ListByDepartamentIdAsync(int departamentId);
        //Task<IEnumerable<District>> ListByCountryIdAsync(int countryId);
        Task AddAsync(District district);
        Task<District> FindById(int id);
        void Update(District district);
        void Remove(District district);
    }
}
