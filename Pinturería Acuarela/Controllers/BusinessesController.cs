using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;
using Pinturería_Acuarela.Models.ViewModels.Businesses;

namespace Pinturería_Acuarela.Controllers
{
    [Authorize]
    public class BusinessesController(IWorkContainer workContainer) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;
        public IActionResult Index()
        {
            return View(_workContainer.Business.GetAll());
        }

        public IActionResult Details(long id)
        {
            try
            {
                DetailsViewModel viewModel = new()
                {
                    Business = _workContainer.Business.GetOne(id),
                    StocklessProducts = _workContainer.Business.GetStocklessProducts(id),
                    StockAlertProducts = _workContainer.Business.GetStockAlertProducts(id),
                    TotalProducts = _workContainer.Business.GetTotalProducts(id),
                    PendingOrders = _workContainer.Order.GetPendingOrders(id),
                    TotalLiters = _workContainer.Business.GetTotalLiters(id)
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
