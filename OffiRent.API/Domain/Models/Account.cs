using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Models
{
    public class Account 
    {
        public int Id { get; set; }
        public bool IsPremium { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Identification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Reservation Reservation { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public List<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();
        public List<Office> Offices { get; set; } = new List<Office>();
        //public int DiscountId { get; set; }
        //public Discount Discount { get; set; }

        [JsonIgnore]
        public string Token { get; set; }
    }
}