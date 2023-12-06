using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;
using Pinturería_Acuarela.Models.ViewModels.Home;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Pinturería_Acuarela.Controllers
{
    [Authorize]
    public class HomeController(IWorkContainer workContainer, IConfiguration config) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;
        private readonly IConfiguration _config = config;

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                int stocklessProducts = 0;
                int stockAlertProducts = 0;
                int pendingOrders = 0;
                ApplicationUser? user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                switch (user.BusinessID)
                {
                    case 1:
                        stocklessProducts = 0;// _workContainer.ProductBusiness.Where(p => p.stock == 0 && p.deleted_at.Equals(null) && p.Product.deleted_at.Equals(null)).Count();
                        stockAlertProducts = 0;//_workContainer.ProductBusiness.Where(p => p.stock < p.minimum_stock && p.deleted_at.Equals(null) && p.Product.deleted_at.Equals(null)).Count();
                        pendingOrders = 0;//_workContainer.Order.Where(o => o.status.Equals(false) && o.deleted_at.Equals(null)).Count();
                        break;
                    case 2:
                        stocklessProducts = 0;// _workContainer.ProductBusiness.Where(p => p.stock == 0 && p.deleted_at.Equals(null) && p.Product.deleted_at.Equals(null)).Count();
                        stockAlertProducts = 0;//_workContainer.ProductBusiness.Where(p => p.stock < p.minimum_stock && p.deleted_at.Equals(null) && p.Product.deleted_at.Equals(null)).Count();
                        pendingOrders = 0;//_workContainer.Order.Where(o => o.status.Equals(false) && o.deleted_at.Equals(null)).Count();
                        break;
                    default:
                        return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
                }
                IndexViewModel viewModel = new()
                {
                    StocklessProducts = stocklessProducts,
                    StockAlertProducts = stockAlertProducts,
                    PendingOrders = pendingOrders
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }
    }
}
