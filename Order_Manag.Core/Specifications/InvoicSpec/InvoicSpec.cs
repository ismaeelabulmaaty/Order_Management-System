using Order_Manag.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Specifications.InvoicSpec
{
    public class InvoicSpec : BaseSpecifications<Invoice>
    {
        public InvoicSpec() : base()
        {
            Includes.Add(I => I.Order);
            
        }

        public InvoicSpec(int id) : base(p => p.Id == id)
        {

            
            Includes.Add(x => x.Order);
            Includes.Add(x => x.Order.Customer);





        }

    }
}
