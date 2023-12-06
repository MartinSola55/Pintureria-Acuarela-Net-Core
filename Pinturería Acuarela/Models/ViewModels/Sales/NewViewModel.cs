using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pinturería_Acuarela.Models.ViewModels.Sales
{
    public class NewViewModel
    {
        public IEnumerable<ProductBusiness> Products { get; set; } = new List<ProductBusiness>();
        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Subcategories { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Capacities { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Colors { get; set; } = new List<SelectListItem>();
        public Sale CreateViewModel { get; set; } = new Sale();
    }
}
