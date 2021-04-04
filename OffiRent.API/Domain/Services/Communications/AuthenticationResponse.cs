using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services.Communications
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public bool IsPremium { get; set; }
        public string Email { get; set; }
        public string Identification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        
        public AuthenticationResponse(Account account, string token)
        {
            Id = account.Id;
            IsPremium = account.IsPremium;
            Email = account.Email;
            Identification = account.Identification;
            FirstName = account.FirstName;
            LastName = account.LastName;
            PhoneNumber = account.PhoneNumber;
            Token = token;
        }

    }
}
