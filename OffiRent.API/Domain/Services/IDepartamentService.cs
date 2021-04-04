using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services
{
    public interface IDepartamentService
    {
        Task<IEnumerable<Departament>> ListAsync();
        Task<IEnumerable<Departament>> ListByCountryIdAsync(int countryId);
        Task<DepartamentResponse> GetByIdAsync(int id);
        Task<DepartamentResponse> SaveAsync(Departament departament);
        Task<DepartamentResponse> UpdateAsync(int id, Departament departament);
        Task<DepartamentResponse> DeleteAsync(int id);

    }
}
