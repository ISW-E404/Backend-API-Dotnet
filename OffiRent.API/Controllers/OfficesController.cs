using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public OfficesController(IOfficeService officeService, IAccountService accountService, IMapper mapper)
        {
            _officeService = officeService;
            _accountService = accountService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List of Offices",
            Description = "List of Offices",
            Tags = new[] { "Offices" }
        )]
        [SwaggerResponse(200, "List of  Offices", typeof(OfficeServiceResource))]
        [HttpGet]
        public async Task<IEnumerable<OfficeServiceResource>> GetAllAsync()
        {
            var offices = await _officeService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Office>, IEnumerable<OfficeServiceResource>>(offices);
            return resources;
        }



        [SwaggerOperation(
            Summary = "Details of the Office",
            Description = "Details of the Office for entered officeId",
            Tags = new[] { "Offices" }
        )]
        [SwaggerResponse(200, "Details of the Office", typeof(OfficeServiceResource))]
        [HttpGet("{officeId}")]
        public async Task<IActionResult> GetByIdAsync(int officeId)
        {
            var result = await _officeService.GetByIdAsync(officeId);

            if (!result.Success)
                return BadRequest(result.Message);
            var officeResource = _mapper.Map<Office, OfficeServiceResource>(result.Resource);
            return Ok(officeResource);
        }

        //[SwaggerOperation(
        //    Summary = "List of Offices by price",
        //    Description = "List of Offices by price",
        //    Tags = new[] { "Offices" }
        //)]
        //[SwaggerResponse(200, "List of  Offices with price equal or lower than entered price", typeof(OfficeResource))]
        //[HttpGet("price")]
        //public async Task<IEnumerable<OfficeResource>> ListByPriceEqualOrLowerThanAsync(int price)
        //{
        //    var offices = await _officeService.ListByPriceEqualOrLowerThanAsync(price);
        //    var resources = _mapper.Map<IEnumerable<Office>, IEnumerable<OfficeResource>>(offices);
        //    return resources;
        //}

        [SwaggerOperation(
            Summary = "Add an office",
            Description = "Add an office given its properties. Non-premium accounts cannot have more than one office at the same time. But" +
            "premium accounts can have up to 15 offices.")]
        [SwaggerResponse(200, "Delete an office by its id", typeof(OfficeServiceResource))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveOfficeServiceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            var office = _mapper.Map<SaveOfficeServiceResource, Office>(resource);
            var result = await _officeService.SaveAsyncPrev(office);

            if (!result.Success)
                return BadRequest(result.Message);

            var officeResource = _mapper.Map<Office, OfficeServiceResource>(result.Resource);

            return Ok(officeResource);
        }



        [SwaggerOperation(
            Summary = "Delete a reservation",
            Description = "Remove a reservation given its id")]
        [SwaggerResponse(200, "Delete an office by its id", typeof(OfficeServiceResource))]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _officeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            var officeResource = _mapper.Map<Office, OfficeServiceResource>(result.Resource);
            return Ok(officeResource);
        }

        [SwaggerOperation(
            Summary = "Edit office data",
            Description = "Edit a property from an office",
            OperationId = "EditOffice",
            Tags = new[] { "Offices" }
            )]

        [SwaggerResponse(200, "List of Offices", typeof(IEnumerable<OfficeServiceResource>))]
        [ProducesResponseType(typeof(IEnumerable<OfficeServiceResource>), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOfficeServiceResource resource)
        {

            var office = _mapper.Map<SaveOfficeServiceResource, Office>(resource);
            var result = await _officeService.UpdateAsync(id, office);

            if (!result.Success)
                return BadRequest(result.Message);

            var officeResource = _mapper.Map<Office, OfficeServiceResource>(result.Resource);
            return Ok(officeResource);
        }


        [SwaggerOperation(
            Summary = "Active office status",
            Description = "Change the status from a office a active",
            OperationId = "ActiveOffice",
            Tags = new[] { "Offices" }
            )]
        [SwaggerResponse(200, "Status from a Office changed", typeof(IEnumerable<OfficeServiceResource>))]
        [ProducesResponseType(typeof(IEnumerable<OfficeServiceResource>), 200)]
        [HttpPatch("{providerId}/{id}")]
        public async Task<IActionResult> PutActiveOfficeAsync(int providerid, int id)
        {
            var result = await _officeService.ActiveOffice(providerid, id);

            if (!result.Success)
                return BadRequest(result.Message);

            var officeResource = _mapper.Map<Office, OfficeServiceResource>(result.Resource);
            return Ok(officeResource);
        }

        [SwaggerOperation(
            Summary = "Rate an office given a score",
            Description = "Rate an office giving a score between 1 to 5",
            Tags = new[] { "Offices" }
            )]
        [SwaggerResponse(200, "List of Categories", typeof(IEnumerable<OfficeServiceResource>))]
        [ProducesResponseType(typeof(IEnumerable<OfficeServiceResource>), 200)]
        [HttpPatch("{officeId}")]
        public async Task<IActionResult> PutScoreAsync(int accountId, int officeId, [FromBody] SaveOfficeServiceResource resource)
        {

            var office = _mapper.Map<SaveOfficeServiceResource, Office>(resource);
            var result = await _officeService.UpdateScoreAsync(accountId, officeId, office);


            if (!result.Success)
                return BadRequest(result.Message);

            var officeResource = _mapper.Map<Office, OfficeServiceResource>(result.Resource);
            return Ok(officeResource);
        }

    }
}
