using System;
using OffiRent.API.Domain.Models;

namespace OffiRent.API.Domain.Services.Communications
{
    public class AccountPaymentMethodResponse : BaseResponse<AccountPaymentMethod>
    {
        public AccountPaymentMethodResponse(AccountPaymentMethod resource) : base(resource)
        {
        }

        public AccountPaymentMethodResponse(string message) : base(message)
        {
        }
    }
}
