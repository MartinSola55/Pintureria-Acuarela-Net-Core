using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;
using Pintureria_Acuarela.Models.ViewModels.Businesses;
using System.Linq.Expressions;

namespace Pintureria_Acuarela.Controllers
{
    [Authorize]
    public class BusinessesController(IWorkContainer workContainer) : Controller
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

        [Authorize(Roles = Constants.Admin)]
        [HttpGet]
        public IActionResult Index()
        {
            return View(_workContainer.Business.GetAll());
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            try
            {
                DetailsViewModel viewModel = new()
                {
                    Business = _workContainer.Business.GetOne(id),
                    StocklessProducts = _workContainer.Business.GetStocklessProducts(id).Count(),
                    StockAlertProducts = _workContainer.Business.GetStockAlertProducts(id).Count(),
                    TotalProducts = _workContainer.Business.GetTotalProducts(id),
                    PendingOrders = _workContainer.Order.GetPendingOrders(id),
                    TotalLiters = _workContainer.Business.GetTotalLiters(id),
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult PendingOrders(long id)
        {
            try
            {
                ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                PendingOrdersViewModel viewModel = new();

                if (_workContainer.ApplicationUser.GetRole(user.Id).Name == Constants.Admin)
                {
                    Expression<Func<Order, bool>> filter = order => order.Status == false;
                    viewModel.PendingOrders =
                    [
                        .. _workContainer.Order.GetAll(filter, includeProperties: "User.Business").OrderByDescending(x => x.CreatedAt).ThenBy(x => x.Status),
                    ];
                    viewModel.Business = _workContainer.Business.GetOne(id);
                }
                else
                {
                    Expression<Func<Order, bool>> filter = order => order.Status == false && order.UserID.Equals(user.Id);
                    viewModel.PendingOrders =
                    [
                        .. _workContainer.Order.GetAll(filter, includeProperties: "User.Business").OrderByDescending(x => x.CreatedAt).ThenBy(x => x.Status),
                    ];
                    viewModel.Business = _workContainer.Business.GetOne(user.BusinessID);
                }
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult Stock(long id)
        {
            try
            {
                ProductsViewModel viewModel = new()
                {
                    Business = _workContainer.Business.GetOne(id),
                    Products = _workContainer.Business.GetProducts(id).ToList()
                };
                return View("Stock/Products", viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult StocklessProducts(long id)
        {
            try
            {
                ProductsViewModel viewModel = new()
                {
                    Business = _workContainer.Business.GetOne(id),
                    Products = _workContainer.Business.GetStocklessProducts(id).ToList()
                };
                return View("Stock/Stockless", viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult StockAlertProducts(long id)
        {
            try
            {
                ProductsViewModel viewModel = new()
                {
                    Business = _workContainer.Business.GetOne(id),
                    Products = _workContainer.Business.GetStockAlertProducts(id).ToList()
                };
                return View("Stock/StockAlert", viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStock(long businessID, long productID, int stock)
        {
            try
            {
                ProductBusiness productBusiness = _workContainer.ProductBusiness.GetOne(businessID, productID);
                if (productBusiness == null) return CustomBadRequest(title: "Error al actualizar el stock del producto", message: "El producto no existe");
                productBusiness.Stock = stock;
                _workContainer.ProductBusiness.Update(productBusiness);
                _workContainer.Save();
                return Json(new
                {
                    success = true,
                    data = productBusiness,
                    message = "El stock se actualizó correctamente",
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al actualizar el stock", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(long businessID, long productID)
        {
            try
            {
                ProductBusiness productBusiness = _workContainer.ProductBusiness.GetOne(businessID, productID);
                if (productBusiness == null) return CustomBadRequest(title: "Error al eliminar el producto", message: "El producto no existe");
                _workContainer.ProductBusiness.SoftDelete(businessID, productID);
                _workContainer.Save();
                return Json(new
                {
                    success = true,
                    data = productBusiness.ProductID,
                    message = "El producto se eliminó correctamente",
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al eliminar el producto", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [HttpGet]
        public IActionResult AssociateProducts(long id)
        {
            try
            {
                Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year == DateTime.UtcNow.AddHours(-3).Year;
                AssociateViewModel viewModel = new()
                {
                    Business = _workContainer.Business.GetOne(id),
                    Products = _workContainer.Business.GetProductsNotAssociated(id).ToList(),
                };
                return View("Stock/AssociateProducts", viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Associate(AssociateViewModel viewModel)
        {
            try
            {
                Product product = _workContainer.Product.GetOne(viewModel.ProductBusiness.ProductID);
                ProductBusiness associatedProduct = new()
                {
                    ProductID = viewModel.ProductBusiness.ProductID,
                    BusinessID = viewModel.ProductBusiness.BusinessID,
                    Stock = viewModel.ProductBusiness.Stock,
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
                };
                _workContainer.ProductBusiness.Add(associatedProduct);
                _workContainer.Save();
                return Json(new
                {
                    success = true,
                    data = viewModel.ProductBusiness.ProductID,
                    message = "El producto se asoció correctamente",
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al asociar el producto", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }
    }
}
