using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pintureria_Acuarela.Models.ViewModels.Orders
{
    public class NewViewModel
    {
        public IEnumerable<ProductBusiness> Products { get; set; } = new List<ProductBusiness>();
        public Order CreateViewModel { get; set; } = new Order();
    }
}
