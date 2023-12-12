using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
        bool IsDuplicated(Product product);
        void SoftDelete(long id);
    }
}