using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bhs.Models
{
    public enum SaleStatus
    {
        WaitingPayment,
        PaymentApproved,
        InTransit,
        Delivered,
        Canceled
    }

    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public SaleStatus Status { get; set; }

        [Required]
        public Seller Seller { get; set; }

        [Required]
        public int SellerCode { get; set; }

        [Required]
        public ICollection<Vehicle> Vehicles { get; set; }
        
    }
}
