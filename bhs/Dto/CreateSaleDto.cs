using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bhs.Dto
{
    public class CreateSaleDto
    {
        [Required]
        public int? SellerCode { get; set; }

        [Required]
        [MinLength(1)]
        public List<int> VehicleCodes { get; set; }
    }
}
