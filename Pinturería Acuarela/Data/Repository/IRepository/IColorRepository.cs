using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository.IRepository
{
    public interface IColorRepository : IRepository<Color>
    {
        IEnumerable<SelectListItem> GetDropDownList();
        void Update(Color color);
        bool IsDuplicated(Color color);
    }
}