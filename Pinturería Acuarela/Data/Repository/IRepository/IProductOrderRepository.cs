using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturer�a_Acuarela.Models;

namespace Pinturer�a_Acuarela.Data.Repository.IRepository
{
    public interface IProductOrderRepository : IRepository<ProductOrder>
    {
        ProductOrder GetOne(long orderID, long productID);
    }
}