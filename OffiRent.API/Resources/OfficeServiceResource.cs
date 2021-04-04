using OffiRent.API.Domain.Models;
using OffiRent.API.Resources.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Resources
{
    public class OfficeServiceResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public bool AllowResource { get; set; }
        public float Score { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public int AccountId { get; set; }

        public List<ServiceResource> Services { get; set; }

        //public District District { get; set; }
        //public Publication Publication { get; set; }
    }
}
