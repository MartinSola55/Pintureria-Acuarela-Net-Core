using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository
{
    public class BusinessRepository(ApplicationDbContext db) : Repository<Business>(db), IBusinessRepository
    {
        private readonly ApplicationDbContext _db = db;
        public int GetStockAlertProducts(long businessID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(businessID) && x.Stock < x.MinimumStock).Count();
        }

        public int GetStocklessProducts(long businessID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(businessID) && x.Stock == 0).Count();
        }

        public double GetTotalLiters(long businessID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(businessID)).Sum(x => x.Stock * (x.Product.CapacityID != null ? x.Product.Capacity.Volume : 0));
        }

        public int GetTotalProducts(long businessID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(businessID)).Count();
        }
    }
}