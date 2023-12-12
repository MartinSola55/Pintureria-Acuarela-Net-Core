using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository
{
    public class BrandRepository(ApplicationDbContext db) : Repository<Brand>(db), IBrandRepository
    {
        private readonly ApplicationDbContext _db = db;

        public IEnumerable<SelectListItem> GetDropDownList()
        {
            IEnumerable<SelectListItem> brands = new List<SelectListItem>
            {
                new() { Value = "", Text = "Seleccione una marca", Disabled = true, Selected = true }
            };
            return brands.Concat(_db.Brands.Where(x => x.IsActive == true).OrderBy(x => x.Name).Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.ID.ToString(),
            }));
        }

        public void Update(Brand brand)
        {
            var dbObject = _db.Brands.FirstOrDefault(x => x.ID == brand.ID);
            if (dbObject != null)
            {
                dbObject.Name = brand.Name;
                _db.SaveChanges();
            }
        }

        public bool IsDuplicated(Brand brand)
        {
            var dbObject = _db.Brands.FirstOrDefault(x => x.Name.ToLower() == brand.Name.ToLower() && x.ID != brand.ID);
            return dbObject != null;
        }

        public void ChangeState(long id)
        {
            var dbObject = _db.Brands.FirstOrDefault(x => x.ID == id);
            if (dbObject != null)
            {
                dbObject.IsActive = !dbObject.IsActive;
                _db.SaveChanges();
            }
        }
    }
}