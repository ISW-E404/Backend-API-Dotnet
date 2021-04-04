using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> ListAsync();
        Task<IEnumerable<District>> ListByDepartamentIdAsync(int departamentId);
        Task<DistrictResponse> GetByIdAsync(int id);
        Task<DistrictResponse> SaveAsync(District district);
        Task<DistrictResponse> UpdateAsync(int id, District district);
        Task<DistrictResponse> DeleteAsync(int id);
    }
}
