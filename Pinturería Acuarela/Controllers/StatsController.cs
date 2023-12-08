using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;
using Pinturería_Acuarela.Models.ViewModels.Stats;
using System.Dynamic;
using System.Linq.Expressions;

namespace Pinturería_Acuarela.Controllers
{
    [Authorize(Roles = Constants.Admin)]
    public class StatsController(IWorkContainer workContainer) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;

        private readonly DateTime today = DateTime.UtcNow.AddHours(-3);

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IndexViewModel viewModel = new()
                {
                    AnnualSales = this.GetAnnualSales(today.Year.ToString()),
                    MonthlySales = this.GetMonthlySales(today.Year.ToString(), today.Month.ToString()),
                    Years = _workContainer.Sale.GetYears()
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public JsonResult GetAnnualSales(string yearString)
        {
            Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year.ToString() == yearString;
            IEnumerable<Sale> allSales = _workContainer.Sale.GetAll(filter);

            // Agrupar las ventas por mes y calcular la suma de Amount
            var salesByMonth = allSales
                .GroupBy(sale => new { sale.CreatedAt.Year, sale.CreatedAt.Month })
                .Select(group => new
                {
                    Period = $"{group.Key.Year}-{group.Key.Month.ToString().PadLeft(2, '0')}",
                    Sold = group.Count(),
                })
                .OrderBy(entry => entry.Period)
                .ToList();

            // Construir el arreglo AnnualSales con los datos agrupados
            List<object> annualSales = [];

            for (int month = 1; month <= 12; month++)
            {
                string monthPadded = month.ToString().PadLeft(2, '0');
                string period = $"{yearString}-{monthPadded}";

                // Buscar la información de ventas por mes en el arreglo salesByMonth
                var salesEntry = salesByMonth.FirstOrDefault(entry => entry.Period == period);

                // Sumar el total de SimpleSales al total de ventas por mes
                decimal sold = (salesEntry?.Sold ?? 0);

                // Agregar la información combinada al arreglo annualSales
                annualSales.Add(new { period, sold });
            }

            return Json(new
            {
                success = true,
                data = annualSales,
            });
        }


        [HttpGet]
        public JsonResult GetMonthlySales(string yearString, string monthString)
        {
            Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year.ToString() == yearString && sale.CreatedAt.Month.ToString() == monthString;
            IEnumerable<Sale> allSales = _workContainer.Sale.GetAll(filter);

            // Convertir los parámetros de cadena a valores numéricos
            int year = int.Parse(yearString);
            int month = int.Parse(monthString);

            // Agrupar las ventas por día y calcular la suma de Amount
            var salesByDay = allSales
                .GroupBy(sale => sale.CreatedAt.Day)
                .Select(group => new
                {
                    Day = group.Key,
                    Sold = group.Count()
                })
                .OrderBy(entry => entry.Day)
                .ToList();

            // Construir el arreglo monthlySales con los datos agrupados
            List<object> monthlySales = new();
            for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                // Buscar la información de ventas por dia en el arreglo salesByDay
                var sale = salesByDay.FirstOrDefault(s => s.Day == day);

                // Sumar el total de recetas y ventas
                decimal sold = (sale?.Sold ?? 0);

                string monthPadded = month.ToString().PadLeft(2, '0');
                string dayPadded = day.ToString().PadLeft(2, '0');
                string period = $"{yearString}-{monthPadded}-{dayPadded}";

                var dailySaleObject = new { period, sold };

                monthlySales.Add(dailySaleObject);
            }

            return Json(new
            {
                success = true,
                data = monthlySales,
            });
        }
    }
}
