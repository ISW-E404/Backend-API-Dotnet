using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OffiRent.API.Domain.Models;
using OffiRent.API.Resources.Account;

namespace OffiRent.API.Resources
{
    public class ReservationResource
    {
        public int Id { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinishDate { get; set; }
        //public int InitialDate { get; set; }
        //public int EndDate { get; set; }
        //public int InitialHour { get; set; }
        //public int EndHour { get; set; }
        public string Status { get; set; }
        public int AccountId { get; set; }
        //public int OfficeId { get; set; }
        public OfficeServiceResource Office { get; set; }
        public AccountResource Account { get; set; }
    }
}
