using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturer�a_Acuarela.Models;

namespace Pinturer�a_Acuarela.Data.Repository.IRepository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        IEnumerable<SelectListItem> GetDropDownList();

        void Update(Brand brand);
        bool IsDuplicated(Brand brand);
        void ChangeState(long id);
    }
}