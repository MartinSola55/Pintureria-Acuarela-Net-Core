using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturerķa_Acuarela.Models;

namespace Pinturerķa_Acuarela.Data.Repository.IRepository
{
    public interface IProductBusinessRepository : IRepository<ProductBusiness>
    {
        ProductBusiness GetOne(long businessID, long productID);
        void Update(ProductBusiness product);
        bool IsDuplicated(ProductBusiness product);
        void SoftDelete(long businessID, long productID);
    }
}