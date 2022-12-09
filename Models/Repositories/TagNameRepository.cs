using MyECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models.Repositories
{
    public class TagNameRepository : IECommerceRepository<TagNames>
    {
        ApplicationDbContext db;
        public TagNameRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(TagNames entity)
        {
            db.TagNames.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var tagNames = FindById(id);
            db.TagNames.Remove(tagNames);
            db.SaveChanges();
        }

        public TagNames FindById(int id)
        {
            var tagNames = db.TagNames.SingleOrDefault(b => b.Id == id);
            return tagNames;
        }

        public IList<TagNames> List()
        {
            return db.TagNames.ToList();
        }

        public List<TagNames> Search(string searchString)
        {
            return db.TagNames.Where(b => b.TagName.Contains(searchString)).ToList();
        }

        public void Update(int id, TagNames newTagName)
        {
            db.Update(newTagName);
            db.SaveChanges();
        }
    }
}
