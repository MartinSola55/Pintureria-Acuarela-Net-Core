using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturerķa_Acuarela.Models;

namespace Pinturerķa_Acuarela.Data.Repository.IRepository
{
    public interface IColorRepository : IRepository<Color>
    {
        IEnumerable<SelectListItem> GetDropDownList();
        void Update(Color color);
        bool IsDuplicated(Color color);
    }
}