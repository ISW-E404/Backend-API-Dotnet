using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services.Communications
{
    public class CountryCurrencyResponse : BaseResponse<CountryCurrency>
    {
        public CountryCurrencyResponse(CountryCurrency resource) : base(resource)
        {
        }

        public CountryCurrencyResponse(string message) : base(message)
        {
        }
    }
}
