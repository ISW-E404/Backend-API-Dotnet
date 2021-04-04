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
using OffiRent.API.Resources.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace OffiRent.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountOfficesController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;

        public AccountOfficesController(IAccountService accountService, IOfficeService officeService, IMapper mapper)
        {
            _accountService = accountService;
            _officeService = officeService;
            _mapper = mapper;
        }

        [SwaggerOperation(
             Summary = "Details of the Office",
             Description = "Details of the Office for entered officeId",
             Tags = new[] { "Accounts" }
         )]
        [SwaggerResponse(200, "Details of the Office", typeof(OfficeServiceResource))]
        [HttpGet("account/offices/{officeId}")]
        public async Task<IActionResult> GetByIdAsync(int officeId)
        {
            var result = await _officeService.GetByIdAsync(officeId);

            if (!result.Success)
                return BadRequest(result.Message);
            var officeResource = _mapper.Map<Office, OfficeServiceResource>(result.Resource);
            return Ok(officeResource);
        }

        [SwaggerOperation(
             Summary = "List offices by provider",
             Description = "List offices given the provider id",
             Tags = new[] { "Accounts" }
         )]
        [SwaggerResponse(200, "Office list of account", typeof(OfficeServiceResource))]
        [HttpGet("account/{accountId}/offices")]
        public async Task<IActionResult> ListByAccountIdAsync(int accountId)
        {
            var result = await _officeService.ListByProviderIdAsync(accountId);

            var officeResource = _mapper.Map<IEnumerable<Office>, IEnumerable<OfficeServiceResource>>(result);
            return Ok(officeResource);
        }
    }
}
