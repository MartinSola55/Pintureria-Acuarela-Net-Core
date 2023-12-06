using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pinturería_Acuarela.Models.ViewModels.Products
{
    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Subcategories { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Capacities { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Colors { get; set; } = new List<SelectListItem>();
        public Product CreateViewModel { get; set; } = new Product();
    }
}
