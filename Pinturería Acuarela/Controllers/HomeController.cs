using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class HomeController(IWorkContainer workContainer, IConfiguration config, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                int stocklessProducts = 0;
                int stockAlertProducts = 0;
                int pendingOrders = 0;
                ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                string role = _signInManager.UserManager.GetRolesAsync(user).Result.FirstOrDefault();
                switch (role)
                {
                    case Constants.Admin:
                        stocklessProducts = _workContainer.Business.GetStocklessProducts().Count();
                        stockAlertProducts = _workContainer.Business.GetStockAlertProducts().Count();
                        pendingOrders = _workContainer.Order.GetPendingOrders();
                        break;
                    case Constants.Employee:
                        stocklessProducts = _workContainer.Business.GetStocklessProducts(user.BusinessID).Count();
                        stockAlertProducts = _workContainer.Business.GetStockAlertProducts(user.BusinessID).Count();
                        pendingOrders = _workContainer.Order.GetPendingOrders(user.BusinessID);
                        break;
                    default:
                        return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
                }
                IndexViewModel viewModel = new()
                {
                    User = user,
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
