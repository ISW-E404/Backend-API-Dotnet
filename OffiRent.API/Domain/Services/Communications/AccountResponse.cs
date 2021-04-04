using System;
using OffiRent.API.Domain.Models;

namespace OffiRent.API.Domain.Services.Communications
{
    public class AccountResponse : BaseResponse<Account>
    {
        public AccountResponse(Account resource) : base(resource)
        {
        }

        public AccountResponse(string message) : base(message)
        {
        }
    }
}
