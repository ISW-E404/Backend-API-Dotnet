using Microsoft.EntityFrameworkCore;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Persistence.Contexts;
using OffiRent.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Persistence.Repositories
{
    public class AccountPaymentMethodRepository : BaseRepository, IAccountPaymentMethodRepository
    {
        public AccountPaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(AccountPaymentMethod accountPaymentMethod)
        {
            await _context.AccountPaymentMethods.AddAsync(accountPaymentMethod);
        }

        public async Task AssignAccountPaymentMethod(int accountId, int paymentMethodId)
        {
            AccountPaymentMethod accountPaymentMethod = await FindByAccountIdAndPaymentMethodId(
                accountId, paymentMethodId
                );
            if (accountPaymentMethod == null)
            {
                accountPaymentMethod = new AccountPaymentMethod
                {
                    AccountId = accountId,
                    PaymentMethodId = paymentMethodId
                };
                await AddAsync(accountPaymentMethod);
            }
        }

        public async Task<AccountPaymentMethod> FindByAccountIdAndPaymentMethodId(int accountId, int paymentMethodId)
        {
            return await _context.AccountPaymentMethods.FindAsync(accountId, paymentMethodId);
        }

        public async Task<IEnumerable<AccountPaymentMethod>> ListAsync()
        {
            return await _context.AccountPaymentMethods
                .Include(ap => ap.Account)
                .Include(ap => ap.PaymentMethod)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccountPaymentMethod>> ListByAccountIdAsync(int accountId)
        {
            return await _context.AccountPaymentMethods
                .Where(ap => ap.AccountId == accountId)
                .Include(ap => ap.Account)
                .Include(ap => ap.PaymentMethod)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccountPaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId)
        {
            return await _context.AccountPaymentMethods
                .Where(ap => ap.PaymentMethodId == paymentMethodId)
                .Include(ap => ap.Account)
                .Include(ap => ap.PaymentMethod)
                .ToListAsync();
        }

        public void Remove(AccountPaymentMethod paymentMethodId)
        {
            _context.AccountPaymentMethods.Remove(paymentMethodId);
        }

        public async void UnassignAccountPaymentMethod(int accountId, int paymentMethodId)
        {
            AccountPaymentMethod accountPaymentMethod = await FindByAccountIdAndPaymentMethodId(accountId, paymentMethodId);
            if (accountPaymentMethod != null)
            {
                Remove(accountPaymentMethod);
            }
        }
    }
}
