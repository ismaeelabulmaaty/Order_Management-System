using Order_Manag.Core.Entites;

namespace Order_Management.DTOS
{
    public class PaymentDto 
    {

        public string Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }

    }
}
