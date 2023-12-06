using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository
{
    public class ColorRepository(ApplicationDbContext db) : Repository<Color>(db), IColorRepository
    {
        private readonly ApplicationDbContext _db = db;

        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> colors = new List<SelectListItem>
            {
                new() { Value = "", Text = "Seleccione un color", Disabled = true, Selected = true }
            };
            return colors.Concat(_db.Colors.OrderBy(x => x.Name).Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.ID.ToString(),
            }));
        }

        public void Update(Color color)
        {
            var dbObject = _db.Colors.FirstOrDefault(x => x.ID == color.ID);
            if (dbObject != null)
            {
                dbObject.Name = color.Name;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Color color)
        {
            var dbObject = _db.Colors.FirstOrDefault(x => x.Name.ToLower() == color.Name.ToLower() && x.ID != color.ID);
            return dbObject != null;
        }
    }
}