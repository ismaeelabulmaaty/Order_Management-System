using Order_Manag.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Specifications.ProductsSpec
{
    public class ProductWithIncludeAndQuerySpec : BaseSpecifications<Product>
    {
        public ProductWithIncludeAndQuerySpec(string Sort) : base()
        {

            if (!string.IsNullOrEmpty(Sort))
            {
                switch (Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }


            }

        }
        public ProductWithIncludeAndQuerySpec(int id) : base(p => p.Id == id)
        {





        }
    }
}
