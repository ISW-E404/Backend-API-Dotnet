using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services.Communications;


namespace OffiRent.API.Domain.Services
{
    public interface IAccountPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
        Task<IEnumerable<PaymentMethod>> ListByProductIdAsync(int accountId);
        Task<IEnumerable<PaymentMethod>> ListByTagIdAsync(int paymentMethodId);
        Task<AccountPaymentMethodResponse> AssignPaymentMethodAsync(int accountId, int paymentMethodId);
        Task<AccountPaymentMethodResponse> UnassignPaymentMethodAsync(int accountId, int paymentMethodId);

    }
}
