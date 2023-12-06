using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;
using Pinturería_Acuarela.Models.ViewModels.Sales;
using System.Linq.Expressions;
using System.Transactions;

namespace Pinturería_Acuarela.Controllers
{
    [Authorize]
    public class SalesController(IWorkContainer workContainer) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;

        private BadRequestObjectResult CustomBadRequest(string title, string message, string? error = null)
        {
            return BadRequest(new
            {
                success = false,
                title,
                message,
                error,
            });
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year == DateTime.UtcNow.AddHours(-3).Year;
                IndexViewModel viewModel = new()
                {
                    Sales = _workContainer.Sale.GetAll(filter, includeProperties: "Products, Products.Product").OrderByDescending(x => x.CreatedAt),
                    Years = _workContainer.Sale.GetYears()
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        // EMPLEADO
        [HttpGet]
        public IActionResult New()
        {
            try
            {
                ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                Expression<Func<ProductBusiness, bool>> filter = x => x.BusinessID == user.BusinessID;
                IEnumerable<ProductBusiness> products = _workContainer.ProductBusiness.GetAll(filter, includeProperties: "Product, Product.Brand, Product.Category, Product.Subcategory, Product.Capacity, Product.Color");
                if (!products.Any())
                {
                    return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "La sucursal no cuenta con ningún producto asociado", ErrorCode = 404 });
                }

                NewViewModel viewModel = new()
                {
                    Products = products.ToList(),
                    Brands = _workContainer.Brand.GetDropDownList(),
                    Categories = _workContainer.Category.GetDropDownList(),
                    Subcategories = _workContainer.Subcategory.GetDropDownList(),
                    Capacities = _workContainer.Capacity.GetDropDownList(),
                    Colors = _workContainer.Color.GetDropDownList(),
                    CreateViewModel = new Sale()
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        // EMPLEADO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewViewModel viewModel)
        {
            try
            {
                ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                Sale sale = viewModel.CreateViewModel;
                sale.UserID = user.Id;
                sale.CreatedAt = DateTime.UtcNow.AddHours(-3);

                _workContainer.BeginTransaction();
                _workContainer.Sale.Add(sale);
                _workContainer.Save();

                foreach (ProductSale product in sale.Products)
                {
                    ProductBusiness prod = _workContainer.ProductBusiness.GetOne(product.ProductID, user.BusinessID);

                    if (prod == null) return CustomBadRequest(title: "Error al crear la venta", message: "El producto ingresado no existe en los registros");
                    if (product.Quantity > prod.Stock) return CustomBadRequest(title: "Error al crear la venta", message: "La cantidad ingresada es mayor al stock");

                    prod.Stock -= product.Quantity;
                    product.SaleID = sale.ID;
                }
                _workContainer.Save();
                _workContainer.Commit();
                return Json(new
                {
                    success = true,
                    message = "La venta se creó correctamente",
                });
            }
            catch (FormatException)
            {
                _workContainer.Rollback();
                return CustomBadRequest(title: "Error al editar la venta", message: "Alguno de los campos ingresados no posee un formato válido");
            }
            catch (Exception e)
            {
                _workContainer.Rollback();
                return CustomBadRequest("Intente nuevamente o comuníquese para soporte", e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetSalesByYear(string year)
        {
            try
            {
                Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year.ToString() == year;
                var sales = _workContainer.Sale.GetAll(filter, includeProperties: "Products, Products.Product, Products.Product.Brand")
                   .Select(x => new
                   {
                       id = x.ID,
                       createdAt = x.CreatedAt,
                       products = x.Products.Select(p => new
                       {
                           quantity = p.Quantity,
                           product = new
                           {
                               description = p.Product.Description,
                               brand = p.Product.Brand.Name,
                           }
                       }),  
                   });
                return Json(new
                {
                    success = true,
                    data = sales
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al recuperar las ventas del " + year, message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(long id)
        {
            try
            {
                var sale = _workContainer.Sale.GetOne(id);
                if (sale != null)
                {
                    _workContainer.Sale.SoftDelete(id);

                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "La venta se eliminó correctamente",
                    });
                }
                return CustomBadRequest(title: "Error al eliminar", message: "No se encontró la venta solicitada");
            }
            catch (SqlException e)
            {
                return CustomBadRequest(title: "Error en la base de datos", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al eliminar la venta", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }
    }
}
