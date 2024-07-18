using System.ComponentModel.DataAnnotations.Schema;

namespace Order_Manag.Core.Entites
{
    public class OrderItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }//FK
        public Order Order { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }//FK
        public Product Product { get; set; }

    }
}