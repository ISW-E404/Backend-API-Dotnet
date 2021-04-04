using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services.Communications
{
    public class OfficeResponse : BaseResponse<Office>
    {
        public OfficeResponse(Office resource) : base(resource)
        {
        }

        public OfficeResponse(string message) : base(message)
        {
        }
    }
}
