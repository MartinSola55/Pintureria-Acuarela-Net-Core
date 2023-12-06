using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturer�a_Acuarela.Models;

namespace Pinturer�a_Acuarela.Data.Repository.IRepository
{
    public interface IProductSaleRepository : IRepository<ProductSale>
    {
        ProductSale GetOne(long saleID, long productID);
        void SoftDelete(long saleID, long productID);
    }
}