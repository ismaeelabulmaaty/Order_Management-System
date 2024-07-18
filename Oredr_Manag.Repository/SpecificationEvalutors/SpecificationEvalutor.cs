using Microsoft.EntityFrameworkCore;
using Order_Manag.Core.Entites;
using Order_Manag.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Oredr_Manag.Repository.SpecificationEvalutors
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery ,ISpecification<T> spec)
        {
            var Query = inputQuery;  //DbContext.Set<T>();

            if(spec.Criteria is not null) //p=>p.id == id
            {
                Query = Query.Where(spec.Criteria);//DbContext.Set<T>().where(p=>p.id == id);
            }

            if(spec.OrderBy is not null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);
            }
            Query = spec.Includes.Aggregate(Query, (CurrentQuery, includeExprition) => CurrentQuery.Include(includeExprition));
            return Query;
        }


    }
}
