using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pintureria_Acuarela.Models.ViewModels.Sales
{
    public class IndexViewModel
    {
        public IEnumerable<Sale> Sales { get; set; } = new List<Sale>();
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
    }
}
