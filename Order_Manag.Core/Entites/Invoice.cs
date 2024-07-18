using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Entites
{
    public class Invoice : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.Now;
        [ForeignKey("Order")]
        public int OrderId { get; set; } //FK
        public Order Order { get; set; }

    }
}
