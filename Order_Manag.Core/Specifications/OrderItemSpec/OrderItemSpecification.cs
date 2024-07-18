using Order_Manag.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Specifications.OrderItemSpec
{
    public class OrderItemSpecification :BaseSpecifications<OrderItem>
    {

        public OrderItemSpecification() : base()
        {
            Includes.Add(I => I.Order.Customer.Name);
            
        }

        public OrderItemSpecification(int id) : base(p => p.Id == id)
        {


           
            Includes.Add(I => I.Order.Customer.Name);





        }

    }
}
