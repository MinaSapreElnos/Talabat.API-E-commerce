using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models;
using Talabat.Core.Models.OrderAggragation;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly StoreContext storeContext;

        //private Dictionary<string, GenericRepository<BaseEntity>> _Repositores; 

        private Hashtable _Repositores;

        public UnitOfWork( StoreContext storeContext)
        {
            this.storeContext = storeContext;

            _Repositores = new Hashtable(); 
        }


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Key = typeof(TEntity).Name; //Product 

            if (!_Repositores.ContainsKey(Key))
            {
                var Repository = new GenericRepository<TEntity>(storeContext);

                //as GenericRepository<BaseEntity>//

                _Repositores.Add(Key, Repository);
            }

            return _Repositores[Key] as IGenericRepository<TEntity>; 

        }



        public async Task<int> CompleteAcync()
        {
          return  await storeContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            storeContext.Dispose();
        }

        
    }
}
 