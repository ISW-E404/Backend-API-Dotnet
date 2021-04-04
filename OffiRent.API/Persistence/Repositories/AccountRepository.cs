using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Persistence.Contexts;
using OffiRent.API.Domain.Repositories;

namespace OffiRent.API.Persistence.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }



        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public async Task<Account> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Account> GetByPhoneNumerAsync(string phoneNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task<Account> GetSingleByIdAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<IEnumerable<Account>> ListAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public void Remove(Account account)
        {
            _context.Remove(account);
        }
        public void Update(Account account)
        {
            _context.Accounts.Update(account);
        }
    }
}
