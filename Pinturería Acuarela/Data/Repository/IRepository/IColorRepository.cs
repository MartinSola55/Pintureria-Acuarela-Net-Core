using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository.IRepository
{
    public interface IColorRepository : IRepository<Color>
    {
        IEnumerable<SelectListItem> GetDropDownList();
        void Update(Color color);
        bool IsDuplicated(Color color);
    }
}