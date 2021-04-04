using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services.Communications
{
    public class DepartamentResponse : BaseResponse<Departament>
    {
        public DepartamentResponse(Departament resource) : base(resource)
        {
        }

        public DepartamentResponse(string message) : base(message)
        {
        }
    }
}
