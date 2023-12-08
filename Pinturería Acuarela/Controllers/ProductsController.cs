using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;
using Pinturería_Acuarela.Models.ViewModels.Products;

namespace Pinturería_Acuarela.Controllers
{
    [Authorize(Roles = Constants.Admin)]
    public class ProductsController(IWorkContainer workContainer) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IndexViewModel viewModel = new()
                {
                    Products = _workContainer.Product.GetAll(includeProperties: "Brand, Category, Subcategory, Capacity, Color"),
                    Brands = _workContainer.Brand.GetDropDownList(),
                    Categories = _workContainer.Category.GetDropDownList(),
                    Subcategories = _workContainer.Subcategory.GetDropDownList(),
                    Capacities = _workContainer.Capacity.GetDropDownList(),
                    Colors = _workContainer.Color.GetDropDownList(),
                    CreateViewModel = new Product()
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IndexViewModel product)
        {
            ModelState.Remove("CreateViewModel.Brand");
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Product.IsDuplicated(product.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al crear el producto",
                            message = "Ya existe uno con la misma descripción o código interno",
                        });
                    }   
                    product.CreateViewModel.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    _workContainer.Product.Add(product.CreateViewModel);
                    _workContainer.Save();

                    Product newProduct = _workContainer.Product.GetFirstOrDefault(p => product.CreateViewModel.ID.Equals(p.ID), includeProperties: "Category, Brand");
                    return Json(new
                    {
                        success = true,
                        data = newProduct,
                        message = "El producto se creó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al crear el producto",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al crear el producto",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel product)
        {
            ModelState.Remove("CreateViewModel.Brand");
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Product.IsDuplicated(product.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al crear el producto",
                            message = "Ya existe uno con la misma descripción o código interno",
                        });
                    }
                    _workContainer.Product.Update(product.CreateViewModel);
                    _workContainer.Save();
                    Product editedProduct = _workContainer.Product.GetFirstOrDefault(p => product.CreateViewModel.ID.Equals(p.ID), includeProperties: "Category, Brand");
                    return Json(new
                    {
                        success = true,
                        data = editedProduct,
                        message = "El producto se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar el producto",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar el producto",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(long id)
        {
            try
            {
                Product product = _workContainer.Product.GetOne(id);
                if (product != null)
                {
                    _workContainer.Product.SoftDelete(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "El producto se eliminó correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar",
                    message = "No se encontró el producto solicitado",
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar",
                    message = "Intente nuevamente o comuníquese para soporte",
                    error = e.Message,
                });
            }
        }
    }
}
