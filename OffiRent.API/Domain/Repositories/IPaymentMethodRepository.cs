using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OffiRent.API.Domain.Models;

namespace OffiRent.API.Domain.Repositories
{
    public interface IPaymentMethodRepository
    {
        // Maybe: get by due date, or get by account, or add payment method from account
        Task<IEnumerable<PaymentMethod>> ListAsync();
        Task<PaymentMethod> GetSingleByIdAsync(int id);
        void Remove(PaymentMethod paymentMethod);
    }
}
