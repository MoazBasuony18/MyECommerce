using MyECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models.Repositories
{
    public class ProductTypesRepository : IECommerceRepository<ProductTypes>
    {
        private ApplicationDbContext db;
        public ProductTypesRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(ProductTypes entity)
        {
            db.ProductTypes.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var productTypes = FindById(id);
            db.ProductTypes.Remove(productTypes);
            db.SaveChanges();
        }

        public ProductTypes FindById(int id)
        {
            var productTypes = db.ProductTypes.SingleOrDefault(b => b.Id == id);
            return productTypes;
        }

        public IList<ProductTypes> List()
        {
            return db.ProductTypes.ToList();
        }

        public List<ProductTypes> Search(string searchString)
        {
            return db.ProductTypes.Where(b => b.ProductType.Contains(searchString)).ToList();
        }

        public void Update(int id, ProductTypes newProduct)
        {
            db.Update(newProduct);
            db.SaveChanges();
        }
    }
}
