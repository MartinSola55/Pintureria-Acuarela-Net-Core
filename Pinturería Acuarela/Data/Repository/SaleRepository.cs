using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository
{
    public class SaleRepository(ApplicationDbContext db) : Repository<Sale>(db), ISaleRepository
    {
        private readonly ApplicationDbContext _db = db;

        public void SoftDelete(long id)
        {
            _db.Database.BeginTransaction();
            try
            {
                Sale dbObject = _db.Sales.Where(x => x.ID == id).First();
                ApplicationUser user = _db.User.Where(x => x.Id.Equals(dbObject.UserID)).First();
                if (dbObject != null)
                {

                    dbObject.DeletedAt = DateTime.UtcNow.AddHours(-3);
                    foreach (var product in _db.ProductsSale.Where(x => x.SaleID == dbObject.ID))
                    {
                        product.DeletedAt = DateTime.UtcNow.AddHours(-3);
                        _db.ProductsBusiness.Where(x => x.ProductID == product.ProductID && x.BusinessID == user.BusinessID).First().Stock += product.Quantity;
                    }
                    _db.SaveChanges();
                    _db.Database.CommitTransaction();
                }
                else
                {
                    _db.Database.RollbackTransaction();
                    throw new Exception("No se encontró la venta");
                }
            }
            catch (Exception)
            {
                _db.Database.RollbackTransaction();
                throw;
            }
        }

        public IEnumerable<SelectListItem> GetYears()
        {
            var years = this.GetAll()
            .Select(sale => sale.CreatedAt.Year)
            .Distinct()
            .OrderByDescending(year => year)
            .ToList();

            return years.Select(year => new SelectListItem
            {
                Text = year.ToString(),
                Value = year.ToString(),
                Selected = (year == DateTime.UtcNow.AddHours(-3).Year)
            });
        }
    }
}