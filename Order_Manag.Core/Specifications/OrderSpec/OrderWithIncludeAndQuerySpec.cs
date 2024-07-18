using Order_Manag.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Specifications.ProductsSpec
{
    public class OrderWithIncludeAndQuerySpec : BaseSpecifications<Order>
    {
        public OrderWithIncludeAndQuerySpec() : base()
        {

            AddOrderByDesc(p => p.OrderDate);
            Includes.Add(x => x.OrderItems);
            Includes.Add(x => x.Customer.Name);
        }
        public OrderWithIncludeAndQuerySpec(int id) : base(p => p.Id == id)
        {
            Includes.Add(x => x.OrderItems);
            Includes.Add(x => x.Customer.Name);
        }
    }
}
