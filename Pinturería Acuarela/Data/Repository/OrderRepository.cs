using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository
{
    public class OrderRepository(ApplicationDbContext db) : Repository<Order>(db), IOrderRepository
    {
        private readonly ApplicationDbContext _db = db;

        public void SoftDelete(long id)
        {
            
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