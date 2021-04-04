using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface IOfficeRepository
    {
        Task<IEnumerable<Office>> ListAsync();
        Task<IEnumerable<Office>> ListByProviderIdAsync(int providerId);
        Task<IEnumerable<Office>> ListByInactiveStatus(int providerId);
        Task<IEnumerable<Office>> ListAccountOfficesAsync(int accountId);
        Task<IEnumerable<Office>> ListByDistrictIdAsync(int districtId);
        Task<IEnumerable<Office>> ListByPriceEqualOrLowerThanAsync(int price);
        Task AddAsync(Office office);
        Task<Office> FindById(int id);
        void Update(Office office);
        void Remove(Office office);
    }
}
