using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository.IRepository
{
    public interface ICapacityRepository : IRepository<Capacity>
    {
        IEnumerable<SelectListItem> GetDropDownList();
    }
}