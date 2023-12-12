using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository.IRepository
{
    public interface IProductBusinessRepository : IRepository<ProductBusiness>
    {
        ProductBusiness GetOne(long businessID, long productID);
        void Update(ProductBusiness product);
        bool IsDuplicated(ProductBusiness product);
        void SoftDelete(long businessID, long productID);
    }
}