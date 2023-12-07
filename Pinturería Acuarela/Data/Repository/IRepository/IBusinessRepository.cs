using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository.IRepository
{
    public interface IBusinessRepository : IRepository<Business>
    {
        int GetStocklessProducts(long businessID);
        int GetStockAlertProducts(long businessID);
        int GetTotalProducts(long businessID);
        double GetTotalLiters(long businessID);
        IEnumerable<ProductBusiness> GetProducts(long id);
    }
}