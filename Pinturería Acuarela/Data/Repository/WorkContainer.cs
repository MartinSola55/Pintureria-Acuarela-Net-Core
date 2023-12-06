using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pinturería_Acuarela.Data.Repository.IRepository;

namespace Pinturería_Acuarela.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {
        private readonly ApplicationDbContext _db;

        public WorkContainer(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Brand = new BrandRepository(_db);
            Product = new ProductRepository(_db);
            Category = new CategoryRepository(_db);
            Subcategory = new SubcategoryRepository(_db);
            Capacity = new CapacityRepository(_db);
            Color = new ColorRepository(_db);
            ProductBusiness = new ProductBusinessRepository(_db);
            Sale = new SaleRepository(_db);
            ProductSale = new ProductSaleRepository(_db);
            Business = new BusinessRepository(_db);
            Order = new OrderRepository(_db);
            ProductOrder = new ProductOrderRepository(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IBrandRepository Brand { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICategoryRepository Category { get; private set; }


        public ISubcategoryRepository Subcategory { get; private set; }


        public ICapacityRepository Capacity { get; private set; }


        public IColorRepository Color { get; private set; }

        public IProductBusinessRepository ProductBusiness { get; private set; }
        public ISaleRepository Sale { get; private set; }
        public IProductSaleRepository ProductSale { get; private set; }
        public IBusinessRepository Business { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IProductOrderRepository ProductOrder { get; private set; }


        public void BeginTransaction()
        {
            _db.Database.BeginTransaction();
        }

        public void Commit()
        {
            _db.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _db.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}