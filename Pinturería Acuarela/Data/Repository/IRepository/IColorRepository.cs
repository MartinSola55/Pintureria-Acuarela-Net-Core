using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturer�a_Acuarela.Models;

namespace Pinturer�a_Acuarela.Data.Repository.IRepository
{
    public interface IColorRepository : IRepository<Color>
    {
        IEnumerable<SelectListItem> GetDropDownList();
        void Update(Color color);
        bool IsDuplicated(Color color);
    }
}