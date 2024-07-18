using Order_Manag.Core.Entites;

namespace Order_Management.DTOS
{
    public class OrderRequestDto
    {
        public decimal TotalAmount { get; set; }
        public string? PaymentIntentId { get; set; }
        public OrderStatus Status { get; set; }
        public int CustomerId { get; set; } //FK
        public List<OrderItemRequestDto> OrderItems { get; set; } = new ();
    }
    public class OrderItemRequestDto
    {
        public decimal UnitPrice { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }//FK
    }
}
