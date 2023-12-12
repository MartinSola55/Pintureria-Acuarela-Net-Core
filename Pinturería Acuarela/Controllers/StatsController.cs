using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;
using Pintureria_Acuarela.Models.ViewModels.Stats;
using System.Dynamic;
using System.Linq.Expressions;

namespace Pintureria_Acuarela.Controllers
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
                    AnnualSales = this.GetAnnualSales(today.Year.ToString(), null),
                    MonthlySales = this.GetMonthlySales(today.Year.ToString(), today.Month.ToString(), null),
                    Years = _workContainer.Sale.GetYears(),
                    Businesses = _workContainer.Business.GetDropDownList(),
                    MostSoldProducts = this.GetMostSoldProducts(today.Year.ToString(), today.Month.ToString(), null),
                    MostOrderedProducts = this.GetMostOrderedProducts(today.Year.ToString(), today.Month.ToString(), null),
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public JsonResult GetAnnualSales(string yearString, long? businessID)
        {
            Expression<Func<Sale, bool>> filter;
            if (businessID == null)
            {
                filter = sale => sale.CreatedAt.Year.ToString() == yearString;
            } else
            {
                filter = sale => sale.CreatedAt.Year.ToString() == yearString && sale.User.BusinessID == businessID;
            }
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
        public JsonResult GetMonthlySales(string yearString, string monthString, long? businessID)
        {
            Expression<Func<Sale, bool>> filter;
            if (businessID == null)
            {
                filter = sale => sale.CreatedAt.Year.ToString() == yearString && sale.CreatedAt.Month.ToString() == monthString;
            } else
            {
                filter = sale => sale.CreatedAt.Year.ToString() == yearString && sale.CreatedAt.Month.ToString() == monthString && sale.User.BusinessID == businessID;
            }
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

        [HttpGet]
        public JsonResult GetMostSoldProducts(string yearString, string monthString, long? businessID)
        {
            Expression<Func<ProductSale, bool>> filter;
            if (businessID == null)
            {
                filter = sale => sale.Sale.CreatedAt.Year.ToString() == yearString && sale.Sale.CreatedAt.Month.ToString() == monthString;
            }
            else
            {
                filter = sale => sale.Sale.CreatedAt.Year.ToString() == yearString && sale.Sale.CreatedAt.Month.ToString() == monthString && sale.Sale.User.BusinessID == businessID;
            }
            IEnumerable<ProductSale> allSales = _workContainer.ProductSale.GetAll(filter, includeProperties: "Sale, Product");

            var mostSoldProducts = allSales
                .GroupBy(sale => sale.ProductID)
                .Select(group => new
                {
                    productID = group.Key,
                    product = group.First().Product.Description,
                    sold = group.Sum(x => x.Quantity)
                })
                .ToList();

            // Limitar la cantidad de productos a 10
            mostSoldProducts = mostSoldProducts
                .OrderByDescending(entry => entry.sold)
                .Take(10)
                .ToList();

            dynamic dataObject = new ExpandoObject();
            dataObject.period = yearString;

            foreach (var item in mostSoldProducts)
            {
                ((IDictionary<string, object>)dataObject)[item.product] = item.sold;
            }

            return Json(new
            {
                success = true,
                data = dataObject
            });
        }
        
        [HttpGet]
        public JsonResult GetMostOrderedProducts(string yearString, string monthString, long? businessID)
        {
            Expression<Func<ProductOrder, bool>> filter;
            if (businessID == null)
            {
                filter = order => order.Order.CreatedAt.Year.ToString() == yearString && order.Order.CreatedAt.Month.ToString() == monthString;
            }
            else
            {
                filter = order => order.Order.CreatedAt.Year.ToString() == yearString && order.Order.CreatedAt.Month.ToString() == monthString && order.Order.User.BusinessID == businessID;
            }
            IEnumerable<ProductOrder> allOrders = _workContainer.ProductOrder.GetAll(filter, includeProperties: "Order, Product");

            var mostSoldProducts = allOrders
                .GroupBy(order => order.ProductID)
                .Select(group => new
                {
                    productID = group.Key,
                    product = group.First().Product.Description,
                    sold = group.Sum(x => x.Quantity)
                })
                .ToList();

            // Limitar la cantidad de productos a 10
            mostSoldProducts = mostSoldProducts
                .OrderByDescending(entry => entry.sold)
                .Take(10)
                .ToList();

            dynamic dataObject = new ExpandoObject();
            dataObject.period = yearString;

            foreach (var item in mostSoldProducts)
            {
                ((IDictionary<string, object>)dataObject)[item.product] = item.sold;
            }

            return Json(new
            {
                success = true,
                data = dataObject
            });
        }
    }
}
