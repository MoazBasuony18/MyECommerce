using Microsoft.EntityFrameworkCore;
using MyECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models.Repositories
{
    public class ProductRepository : IECommerceRepository<Products>
    {
        ApplicationDbContext db;
        public ProductRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(Products entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var products = FindById(id);
            db.Products.Remove(products);
            db.SaveChanges();
        }

        public Products FindById(int id)
        {
            var products = db.Products.Include(b => b.ProductTypes).Include(a => a.TagNames).SingleOrDefault(c=>c.Id == id);
            return products;
        }

        public IList<Products> List()
        {
            return db.Products.Include(b => b.ProductTypes).Include(a => a.TagNames).ToList();
        }

        public List<Products> Search(string searchString)
        {
            var result = db.Products.Include(b => b.ProductTypes).Include(a => a.TagNames).Where(c => c.Name.Contains(searchString)
                  || c.ProductColor.Contains(searchString) || c.Price.ToString().Contains(searchString)
                  || c.ProductTypes.ProductType.Contains(searchString) || c.TagNames.TagName.Contains(searchString)).ToList();
            return result;
        }

        public void Update(int id, Products newProduct)
        {
            db.Update(newProduct);
            db.SaveChanges();
        }
    }
}
