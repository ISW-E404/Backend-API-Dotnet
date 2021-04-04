using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface IAccountPaymentMethodRepository
    {
        Task<IEnumerable<AccountPaymentMethod>> ListAsync();
        Task<IEnumerable<AccountPaymentMethod>> ListByAccountIdAsync(int accountId);
        Task<IEnumerable<AccountPaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId);
        Task<AccountPaymentMethod> FindByAccountIdAndPaymentMethodId(int accountId, int paymentMethodId);
        Task AddAsync(AccountPaymentMethod accountId);
        void Remove(AccountPaymentMethod accountId);
        Task AssignAccountPaymentMethod(int accountId, int paymentMethodId);
        void UnassignAccountPaymentMethod(int accountId, int paymentMethodId);
    }
}
