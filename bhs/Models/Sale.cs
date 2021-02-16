using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bhs.Models
{
    public class Sale
    {
        public enum SaleStatus
        {
            WaitingPayment,
            PaymentApproved,
            InTransit,
            Delivered,
            Canceled
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public SaleStatus Status { get; set; }

        [Required]
        public Saler Saler { get; set; }

        [Required]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        
    }
}
