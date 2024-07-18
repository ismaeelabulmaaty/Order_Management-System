using Order_Manag.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Specifications.OrderSpec
{
    public class OrderWithPaymentSpec : BaseSpecifications<Order>
    {


       
        

            public OrderWithPaymentSpec(string PaymentId) : base(O => O.PaymentIntentId == PaymentId)
            {

            }

        


    }
}
