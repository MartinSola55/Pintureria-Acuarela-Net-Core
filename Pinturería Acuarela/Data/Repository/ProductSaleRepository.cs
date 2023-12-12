using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository
{
    public class ProductSaleRepository(ApplicationDbContext db) : Repository<ProductSale>(db), IProductSaleRepository
    {
        private readonly ApplicationDbContext _db = db;

        public void SoftDelete(long saleID, long productID)
        {
            var dbObject = _db.ProductsSale.FirstOrDefault(x => x.SaleID == saleID && x.ProductID == productID);
            if (dbObject != null)
            {
                dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();
            }
        }

        public ProductSale GetOne(long saleID, long productID)
        {
            return _db.ProductsSale.Where(x => x.SaleID == saleID && x.ProductID == productID).First();
        }
    }
}