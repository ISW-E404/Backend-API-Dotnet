using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Resources
{
    public class SaveDistrictResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
