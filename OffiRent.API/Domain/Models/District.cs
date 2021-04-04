using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DepartamentId { get; set; }
        public Departament Departament { get; set; }

        public List<Office> Offices { get; set; }

    }
}
