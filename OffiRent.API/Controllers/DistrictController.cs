using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services;
using OffiRent.API.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace OffiRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        private readonly IMapper _mapper;

        public DistrictController(IDistrictService districtService, IMapper mapper)
        {
            _districtService = districtService;
            _mapper = mapper;
        }


        [SwaggerOperation(
            Summary = "List of Districts",
            Description = "List of Districts",
            Tags = new[] { "Districts" }
        )]
        [SwaggerResponse(200, "List of  Districts", typeof(DistrictResource))]
        [HttpGet]
        public async Task<IEnumerable<DistrictResource>> GetAllAsync()
        {
            var districts = await _districtService.ListAsync();
            var resources = _mapper.Map<IEnumerable<District>, IEnumerable<DistrictResource>>(districts);
            return resources;

        }
    }
}
