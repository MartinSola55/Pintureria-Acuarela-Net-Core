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
    public class OrderRepository(ApplicationDbContext db) : Repository<Order>(db), IOrderRepository
    {
        private readonly ApplicationDbContext _db = db;

        public void SoftDelete(long id)
        {
            try
            {
                Order? order = _db.Orders.Where(x => x.ID == id).Include(x => x.User).Include(x => x.Products).First() ?? throw new Exception("No se encontró la orden especificada");
                //_db.Database.BeginTransaction();
                //order.Status = false;
                order.DeletedAt = DateTime.UtcNow.AddHours(-3);
                _db.SaveChanges();

                //foreach (ProductOrder product in order.Products)
                //{
                //    if (product.BusinessID != null)
                //    {
                //        ProductBusiness businessSender = _db.ProductsBusiness.Where(x => x.BusinessID == product.BusinessID.Value && x.ProductID == product.ProductID).First();
                //        businessSender.Stock += product.QuantitySend;
                //        ProductBusiness businessReceiver = _db.ProductsBusiness.Where(x => x.BusinessID == order.User.BusinessID && x.ProductID == product.ProductID).First();
                //        businessReceiver.Stock -= product.QuantitySend;
                //    }
                //    product.Status = false;
                //    product.QuantitySend = 0;
                //    product.BusinessID = null;
                //}

                //_db.SaveChanges();
                //_db.Database.CommitTransaction();
            }
            catch (Exception)
            {
                //_db.Database.RollbackTransaction();
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

        public int GetPendingOrders(long businessID)
        {
            return _db.Orders.Where(x => x.ID == businessID && x.Status.Equals(false)).Count();
        }
    }
}