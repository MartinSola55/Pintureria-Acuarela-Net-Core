using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository.IRepository
{
    public interface ISaleRepository : IRepository<Sale>
    {
        IEnumerable<SelectListItem> GetYears();
        void SoftDelete(long id);
    }
}