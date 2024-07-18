using Order_Manag.Core.Entites;
using Order_Manag.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec) ;

        Task<T?> GetByIdWithSpecAsync(ISpecification<T> Spec);

        Task<T> AddAsync(T item);

        void Delete(T item);

        T Update(T item);

        Task<int> SaveChangesAsync();


    }
}
