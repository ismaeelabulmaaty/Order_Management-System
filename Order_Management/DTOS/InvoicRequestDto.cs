using Order_Manag.Core.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order_Management.DTOS
{
    public class InvoicRequestDto:BaseEntity
    {

        public decimal TotalAmount { get; set; }
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.Now;
        public int OrderId { get; set; }

    }
}
