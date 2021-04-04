using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OffiRent.API.Domain.Models;

namespace OffiRent.API.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> ListAsync();
        Task<Account> GetSingleByIdAsync(int accountId);
        Task<Account> GetByPhoneNumerAsync(string phoneNumber);
        Task<Account> GetByEmailAsync(string email);
        Task<Account> GetByEmailAndPasswordAsync(string email, string password);  // para token
        Task AddAsync(Account account);
        void Remove(Account account);
        void Update(Account account);
    }
}
