using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinturerķa_Acuarela.Data.Repository.IRepository
{
    public interface IWorkContainer : IDisposable
    {
        // Agregar los repositorios
        IApplicationUserRepository ApplicationUser { get; }
        IBrandRepository Brand { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        ISubcategoryRepository Subcategory { get; }
        ICapacityRepository Capacity { get; }
        IColorRepository Color { get; }
        IProductBusinessRepository ProductBusiness { get; }
        ISaleRepository Sale { get; }
        IProductSaleRepository ProductSale { get; }
        IBusinessRepository Business { get; }
        IOrderRepository Order { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}