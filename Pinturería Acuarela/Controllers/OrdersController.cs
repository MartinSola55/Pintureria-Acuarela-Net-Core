using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Controllers
{
    [Authorize]
    public class OrdersController(IWorkContainer workContainer) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;
        //public IActionResult Index()
        //{
        //    try
        //    {
        //        ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
        //        if (user != null)
        //        {
        //            return RedirectToAction("Details", "Businesses", new { id = user.BusinessID });
        //        }
        //        id = user.Rol.id != 1 ? user.id_business : id;
        //        if (id != null)
        //        {
        //            var orders = db.Order.Where(o => o.User.Business.id.Equals(id.Value) && o.deleted_at.Equals(null));
        //            return View(orders.ToList().OrderBy(o => o.User.Business.id).ThenBy(o => o.date).ThenBy(o => o.status));
        //        }
        //        var order = db.Order.Include(o => o.User).Where(o => o.deleted_at.Equals(null));
        //        return View(order.ToList().OrderBy(o => o.User.Business.id).ThenBy(o => o.date).ThenBy(o => o.status));
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
    }
}
