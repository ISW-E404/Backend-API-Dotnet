using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Extensions;
using OffiRent.API.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace OffiRent.API.Controllers
{
    [Route("api/Offices")]
    [ApiController]
    public class OfficesReservationsController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public OfficesReservationsController(IOfficeService officeService, IReservationService reservationService, IMapper mapper)
        {
            _officeService = officeService;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Reservations of an office",
            Description = "List all Reservations from an office given it's id",
            Tags = new[] { "Offices" }

           )]
        [SwaggerResponse(200, "List of Reservations", typeof(IEnumerable<ReservationResource>))]
        [ProducesResponseType(typeof(IEnumerable<ReservationResource>), 200)]
        [HttpGet("{officeId}/reservations")]
        public async Task<IEnumerable<ReservationResource>> GetAllReservationsByOfficeIdAsync(int officeId, [Optional][FromQuery(Name = "status")] string status)
        {
            var reservations = await _reservationService.ListByOfficeIdAsync(officeId, status);
            var resources = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResource>>(reservations);
            return resources;
        }


    }
}
