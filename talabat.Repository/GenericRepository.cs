using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models;
using Talabat.Core.Spacifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _DbContext;

        public GenericRepository(StoreContext DbContext )
        {
            _DbContext = DbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _DbContext.Set<T>().ToListAsync();
        
        }

     

        public async Task<T?> GetAsync(int id)
        {
            return await _DbContext.Set<T>().FindAsync(id);
        }




        public  async Task<T?> GetWithSpacAsync(ISpacification<T> Spac)
        {
            return await ApplySpacification(Spac).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpacAsync(ISpacification<T> Spac)
        {
            return await ApplySpacification(Spac).ToListAsync();
        }
         
        private IQueryable<T> ApplySpacification( ISpacification<T> Spac)
        {  
          return   SpacificationEvaluator<T>.GetQuery(_DbContext.Set<T>(), Spac);
        }

        public async Task<int> GetCount(ISpacification<T> Spac)
        {
           return await ApplySpacification(Spac).CountAsync();
        }

        public async Task AddAcync(T entity)
        {
            await _DbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _DbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _DbContext.Remove(entity);  
        }
    }
}
