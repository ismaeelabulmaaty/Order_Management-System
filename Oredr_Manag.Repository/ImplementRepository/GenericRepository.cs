using Microsoft.EntityFrameworkCore;
using Order_Manag.Core.Entites;
using Order_Manag.Core.Repository.Contract;
using Order_Manag.Core.Specifications;
using Oredr_Manag.Repository.Data;
using Oredr_Manag.Repository.SpecificationEvalutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredr_Manag.Repository.ImplementRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ManagDbContext _dbContext;

        public GenericRepository(ManagDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

        public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> Spec)
        {
            return  await ApplySpecification(Spec).FirstOrDefaultAsync();
        }



        public async Task<T> AddAsync(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
            return item;
        }
        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
        public T Update(T item)
        {
            _dbContext.Set<T>().Update(item);
            return item;
        }

        public void Delete(T item)
        => _dbContext.Set<T>().Remove(item);



        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }

    }
}
