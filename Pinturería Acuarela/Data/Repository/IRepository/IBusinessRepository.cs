using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturer�a_Acuarela.Models;

namespace Pinturer�a_Acuarela.Data.Repository.IRepository
{
    public interface IBusinessRepository : IRepository<Business>
    {
        IEnumerable<ProductBusiness> GetProducts(long id);
        IEnumerable<ProductBusiness> GetStocklessProducts(long businessID);
        IEnumerable<ProductBusiness> GetStockAlertProducts(long businessID);
        IEnumerable<Product> GetProductsNotAssociated(long businessID);
        int GetTotalProducts(long businessID);
        double GetTotalLiters(long businessID);
    }
}