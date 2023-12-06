using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository
{
    public class ProductBusinessRepository(ApplicationDbContext db) : Repository<ProductBusiness>(db), IProductBusinessRepository
    {
        private readonly ApplicationDbContext _db = db;

        public void Update(ProductBusiness product)
        {
            var dbObject = _db.ProductsBusiness.FirstOrDefault(x => x.BusinessID == product.BusinessID && x.ProductID == product.ProductID);
            if (dbObject != null)
            {
                dbObject.MinimumStock = product.MinimumStock;
                dbObject.Stock = product.Stock;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(ProductBusiness product)
        {
            var dbObject = _db.ProductsBusiness.FirstOrDefault(x => x.BusinessID == product.BusinessID && x.ProductID == product.ProductID);
            return dbObject != null;
        }

        public void SoftDelete(long businessID, long productID)
        {
            var dbObject = _db.ProductsBusiness.FirstOrDefault(x => x.BusinessID == businessID && x.ProductID == productID);
            if (dbObject != null)
            {
                dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }

        public ProductBusiness GetOne(long businessID, long productID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID == businessID && x.ProductID == productID).First();
        }
    }
}