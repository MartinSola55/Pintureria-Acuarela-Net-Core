using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository
{
    public class ProductRepository(ApplicationDbContext db) : Repository<Product>(db), IProductRepository
    {
        private readonly ApplicationDbContext _db = db;

        public void Update(Product product)
        {
            var dbObject = _db.Products.FirstOrDefault(x => x.ID == product.ID);
            if (dbObject != null)
            {
                dbObject.Description = product.Description;
                dbObject.InternalCode = product.InternalCode;
                dbObject.BrandID = product.BrandID;
                dbObject.CategoryID = product.CategoryID;
                dbObject.SubcategoryID = product.SubcategoryID;
                dbObject.CapacityID = product.CapacityID;
                dbObject.ColorID = product.ColorID;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Product product)
        {
            var dbObject = _db.Products.FirstOrDefault(x => 
            (x.InternalCode.ToLower() == product.InternalCode.ToLower() || 
            x.Description.ToLower() == product.Description.ToLower())
            && x.ID != product.ID);
            return dbObject != null;
        }

        public void SoftDelete(long id)
        {
            var dbObject = _db.Products.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }
    }
}