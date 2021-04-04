using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services.Communications
{
    public class ReservationResponse:BaseResponse<Reservation>
    {
        public ReservationResponse(Reservation resource) : base(resource)
        {

        }
        public ReservationResponse(string message) : base(message)
        {

        }
    }
}
