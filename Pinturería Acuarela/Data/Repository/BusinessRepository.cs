using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository
{
    public class BusinessRepository(ApplicationDbContext db) : Repository<Business>(db), IBusinessRepository
    {
        private readonly ApplicationDbContext _db = db;
        public IEnumerable<ProductBusiness> GetStockAlertProducts(long businessID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(businessID) && x.Stock < x.MinimumStock).Include(x => x.Product).Include(x => x.Product.Brand);
        }
        
        public IEnumerable<ProductBusiness> GetStockAlertProducts()
        {
            return _db.ProductsBusiness.Where(x => x.Stock < x.MinimumStock).Include(x => x.Product).Include(x => x.Product.Brand);
        }

        public IEnumerable<ProductBusiness> GetStocklessProducts(long businessID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(businessID) && x.Stock == 0).Include(x => x.Product).Include(x => x.Product.Brand);
        }

        public IEnumerable<ProductBusiness> GetStocklessProducts()
        {
            return _db.ProductsBusiness.Where(x => x.Stock == 0).Include(x => x.Product).Include(x => x.Product.Brand);
        }

        public double GetTotalLiters(long businessID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(businessID)).Sum(x => x.Stock * (x.Product.CapacityID != null ? x.Product.Capacity.Volume : 0));
        }

        public int GetTotalProducts(long businessID)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(businessID)).Count();
        }

        public IEnumerable<ProductBusiness> GetProducts(long id)
        {
            return _db.ProductsBusiness.Where(x => x.BusinessID.Equals(id)).Include(x => x.Product).Include(x => x.Product.Brand).Include(x => x.Product.Category).Include(x => x.Product.Capacity);
        }

        public IEnumerable<Product> GetProductsNotAssociated(long id)
        {
            return _db.Products.Where(x => !_db.ProductsBusiness.Any(y => y.ProductID.Equals(x.ID) && y.BusinessID.Equals(id))).Include(x => x.Brand).Include(x => x.Category).Include(x => x.Capacity);
        }

        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> brands = new List<SelectListItem>
            {
                new() { Value = "", Text = "Todo el sistema", Selected = true }
            };
            return brands.Concat(_db.Businesses.OrderBy(x => x.Address).Select(i => new SelectListItem()
            {
                Text = i.Address,
                Value = i.ID.ToString(),
            }));
        }
    }
}