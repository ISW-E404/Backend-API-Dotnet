using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services
{
    public interface IOfficeService
    {
        Task<IEnumerable<Office>> ListAsync();
        Task<IEnumerable<Office>> ListByDistrictIdAsync(int districtId);
        Task<IEnumerable<Office>> ListByPriceEqualOrLowerThanAsync(int price);
        Task<IEnumerable<Office>> ListByProviderIdAsync(int providerId);
        Task<IEnumerable<Office>> ListByInactiveStatus(int providerId);
        Task<OfficeResponse> GetByIdAsync(int id);
        Task<OfficeResponse> ActiveOffice(int providerId, int id);
        Task<OfficeResponse> SaveStatusAsync(Office Office);
        Task<OfficeResponse> SaveAsync(int accountId, Office Office);
        //Task<OfficeResponse> ActiveOffice(int providerId, int id);
        Task<OfficeResponse> SaveAsyncPrev(Office Office);
        Task<OfficeResponse> UpdateAsync(int id, Office Office);
        Task<OfficeResponse> UpdateScoreAsync(int accountId, int officeId, Office office);
        Task<OfficeResponse> DeleteAsync(int id);
        Task<bool> AccountHasMoreThanXPosts(int x, Account account);
    }
}
