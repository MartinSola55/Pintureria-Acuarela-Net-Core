using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturerķa_Acuarela.Models;

namespace Pinturerķa_Acuarela.Data.Repository.IRepository
{
    public interface IBusinessRepository : IRepository<Business>
    {
        int GetStocklessProducts(long businessID);
        int GetStockAlertProducts(long businessID);
        int GetTotalProducts(long businessID);
        double GetTotalLiters(long businessID);
    }
}