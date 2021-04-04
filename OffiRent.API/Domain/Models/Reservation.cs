using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Models
{
    public class Reservation
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
        public int OfficeId { get; set; }
        public Account Account {get;set;}
        public Office Office { get; set; }
    }
}
