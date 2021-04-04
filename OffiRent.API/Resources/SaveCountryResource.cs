using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Resources
{
    public class SaveCountryResource
    {
        [Required]
        [MaxLength(30)]
        public char Name { get; set; }
    }
}
