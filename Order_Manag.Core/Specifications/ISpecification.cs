using Order_Manag.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {


        public Expression<Func<T , bool>> Criteria { get; set; } //where(x=>x.id == id)

        public List<Expression<Func<T, object>>> Includes { get; set; }

        public Expression<Func<T , object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDesc { get; set; }
    }
}
