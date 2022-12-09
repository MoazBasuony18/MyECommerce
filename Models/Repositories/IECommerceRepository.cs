using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models.Repositories
{
    public interface IECommerceRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity FindById(int id);
        void Delete(int id);
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        List<TEntity> Search(string searchString);
    }
}
