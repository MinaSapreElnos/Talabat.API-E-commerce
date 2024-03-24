using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Spacifications;

namespace Talabat.Core.IGenericRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<T?> GetAsync(int id); 

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllWithSpacAsync(ISpacification<T> Spac);

        Task<T?> GetWithSpacAsync(ISpacification<T> Spac);


        Task<int> GetCount(ISpacification<T> Spac);


        Task AddAcync( T entity);

        void Update( T entity );

        void Delete( T entity );



    }
}
