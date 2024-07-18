using Order_Manag.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Specifications.NewFolder
{
    public class CustomerSpec :BaseSpecifications<Customer>
    {
        public CustomerSpec() : base()
        {
           
        }

        public CustomerSpec(int id) : base(p => p.Id == id)
        {
            Includes.Add(x => x.Orders);
            
        }
    }
}
