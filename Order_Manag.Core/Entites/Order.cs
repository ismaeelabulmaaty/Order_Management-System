using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Entites
{
    public class Order : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClintSecret { get; set; }
        public OrderStatus Status { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; } //FK
        public Customer Customer { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();



    }
}
