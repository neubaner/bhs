using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bhs.Models
{
    public class Vehicle
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int YearOfManufacture { get; set; }
    }
}
