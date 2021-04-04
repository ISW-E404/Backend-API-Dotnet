using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services.Communications;

namespace OffiRent.API.Domain.Services
{
    public interface IAccountService 
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        Task<IEnumerable<Account>> ListAsync();
        Task<AccountResponse> GetBySingleIdAsync(int id);
        Task<AccountResponse> GetBySinglePhoneNumberAsync(string phoneNumber);
        Task<AccountResponse> GetBySingleEmailAsync(string email);
        Task<AccountResponse> SaveAsync(Account account);
        Task<AccountResponse> DeleteAsync(int id);
        Task<AccountResponse> UpdatePremiumStatusAsync(int id);
        Task<AccountResponse> UpdateAsync(int id, Account account);
    }
}
