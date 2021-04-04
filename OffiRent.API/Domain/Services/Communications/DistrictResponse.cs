using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services.Communications
{
    public class DistrictResponse : BaseResponse<District>
    {
        public DistrictResponse(District resource) : base(resource)
        {
        }

        public DistrictResponse(string message) : base(message)
        {
        }
    }
}
