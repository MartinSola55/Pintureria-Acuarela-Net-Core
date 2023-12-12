using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pintureria_Acuarela.Models.ViewModels.Orders
{
    public class IndexViewModel
    {
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
    }
}
