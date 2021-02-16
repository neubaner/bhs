using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bhs.Models
{
    public class Seller
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
