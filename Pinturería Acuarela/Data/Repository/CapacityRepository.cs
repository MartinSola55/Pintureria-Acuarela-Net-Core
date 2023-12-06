using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturerķa_Acuarela.Data.Repository.IRepository;
using Pinturerķa_Acuarela.Models;

namespace Pinturerķa_Acuarela.Data.Repository
{
    public class CapacityRepository(ApplicationDbContext db) : Repository<Capacity>(db), ICapacityRepository
    {
        private readonly ApplicationDbContext _db = db;

        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> brands = new List<SelectListItem>
            {
                new() { Value = "", Text = "Seleccione una capacidad", Disabled = true, Selected = true }
            };
            return brands.Concat(_db.Capacities.OrderBy(x => x.Description).Select(i => new SelectListItem() {
                Text = i.Description,
                Value = i.ID.ToString(),
            }));
        }
    }
}