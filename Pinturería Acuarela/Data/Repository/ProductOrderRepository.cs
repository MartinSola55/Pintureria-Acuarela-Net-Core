using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository
{
    public class ProductOrderRepository(ApplicationDbContext db) : Repository<ProductOrder>(db), IProductOrderRepository
    {
        private readonly ApplicationDbContext _db = db;

        public ProductOrder GetOne(long orderID, long productID)
        {
            return _db.ProductsOrder.Where(x => x.OrderID == orderID && x.ProductID == productID).First();
        }
    }
}