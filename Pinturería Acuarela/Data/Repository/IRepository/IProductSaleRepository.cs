using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository.IRepository
{
    public interface IProductSaleRepository : IRepository<ProductSale>
    {
        ProductSale GetOne(long saleID, long productID);
        void SoftDelete(long saleID, long productID);
    }
}