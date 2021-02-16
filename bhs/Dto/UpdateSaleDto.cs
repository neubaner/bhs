using bhs.Models;
using System.ComponentModel.DataAnnotations;

namespace bhs.Dto
{
    public class UpdateSaleDto
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public SaleStatus? Status { get; set; }
    }
}
