using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Resources
{
    public class SaveDepartamentResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
