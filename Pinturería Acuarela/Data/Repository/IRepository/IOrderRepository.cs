using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<SelectListItem> GetYears();
        void SoftDelete(long id);
        int GetPendingOrders(long businessID);
        int GetPendingOrders();
    }
}