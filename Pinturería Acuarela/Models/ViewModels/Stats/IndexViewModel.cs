using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pinturería_Acuarela.Models.ViewModels.Stats
{
    public class IndexViewModel
    {
        public required JsonResult AnnualSales { get; set; }
        public required JsonResult MonthlySales { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
    }
}
