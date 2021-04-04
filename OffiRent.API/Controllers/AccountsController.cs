using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Extensions;
using OffiRent.API.Resources.Account;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "Update Premium Status of Account",
           Tags = new[] { "Accounts" }


        )]
        [SwaggerResponse(200, "Change type of OffiProvider account to Premium", typeof(AccountResource))]
        [HttpPatch("{accountId}")]
        public async Task<IActionResult> UpdateAsync(int accountId)
        {
            var result = await _accountService.UpdatePremiumStatusAsync(accountId);

            if (!result.Success)
                return BadRequest(result.Message);
            var accountResource = _mapper.Map<Account, AccountResource>(result.Resource);
            return Ok(accountResource);
        }

        [SwaggerOperation(
            Summary = "List all accounts",
            Description = "Retrieve all the accounts registered.",
            OperationId = "ListAllAccounnts",
            Tags = new[] { "Accounts" }
            )]
        [SwaggerResponse(200, "List of registered accounts", typeof(IEnumerable<AccountResource>))]
        [ProducesResponseType(typeof(IEnumerable<AccountResource>), 200)]
        [HttpGet]
        public async Task<IEnumerable<AccountResource>> GetAllAsync()
        {
            var categories = await _accountService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Account>,
                IEnumerable<AccountResource>>(categories);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Details of an Account",
            Description = "Details of an Account for entered accountId",
            Tags = new[] { "Accounts" }
        )]
        [SwaggerResponse(200, "Details of an Account", typeof(AccountResource))]
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetByIdAsync(int accountId)
        {
            var result = await _accountService.GetBySingleIdAsync(accountId);

            if (!result.Success)
                return BadRequest(result.Message);
            var accountResource = _mapper.Map<Account, AccountResource>(result.Resource);
            return Ok(accountResource);
        }


        [SwaggerOperation(
           Summary = "Authenticate user session",
           Description = "Authenticate the login of an existing user when entering their credentials",
           Tags = new[] { "Accounts" }
        )]
        [SwaggerResponse(200, "Login credentials", typeof(AuthenticationResponse))]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticationRequest request)
        {
            var response = _accountService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Invalid Username Or Password" });

            return Ok(response);
        }

        [SwaggerOperation(
            Summary = "Register Account",
            Description = "Register a new account.",
            OperationId = "RegisterNewAccount",
            Tags = new[] { "Accounts" }
            )]
        [SwaggerResponse(200, "New account registered", typeof(IEnumerable<AccountResource>))]
        [ProducesResponseType(typeof(IEnumerable<AccountResource>), 200)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAccountResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var account = _mapper.Map<SaveAccountResource, Account>(resource);
            var result = await _accountService.SaveAsync(account);

            if (!result.Success)
                return BadRequest(result.Message);

            var accountResource = _mapper.Map<Account, AccountResource>(result.Resource);

            return Ok(accountResource);

        }

        [SwaggerOperation(
            Summary = "Edit account data",
            Description = "Edit a property from a account. Can handle: [FirstName, LastName, Identification, " +
            "Email, Password or PhoneNumber",
            OperationId = "ListAllCategories",
            Tags = new[] { "Accounts" }
            )]
        [SwaggerResponse(200, "Account updated", typeof(IEnumerable<AccountResource>))]
        [ProducesResponseType(typeof(IEnumerable<AccountResource>), 200)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAccountResource resource)
        {

            var account = _mapper.Map<SaveAccountResource, Account>(resource);
            var result = await _accountService.UpdateAsync(id, account);

            if (!result.Success)
                return BadRequest(result.Message);

            var accountResource = _mapper.Map<Account, AccountResource>(result.Resource);
            return Ok(accountResource);
        }

        [SwaggerOperation(
           Summary = "Delete a Account",
           Tags = new[] { "Accounts" })
        ]

        [SwaggerResponse(200, "Delete an account given it's id", typeof(AccountResource))]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _accountService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            var accountResource = _mapper.Map<Account, AccountResource>(result.Resource);
            return Ok(accountResource);
        }
    }
}
