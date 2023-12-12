using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models.ViewModels.Brands;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Controllers
{
    [Authorize(Roles = Constants.Admin)]
    public class BrandsController(IWorkContainer workContainer) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IndexViewModel viewModel = new()
                {
                    Brands = _workContainer.Brand.GetAll(),
                    CreateViewModel = new Brand()
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
        public IActionResult Create(IndexViewModel brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Brand.IsDuplicated(brand.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al agregar la marca",
                            message = "Ya existe otra con el mismo nombre",
                        });
                    }
                    _workContainer.Brand.Add(brand.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = brand.CreateViewModel,
                        message = "La marca se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar la marca",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar la marca",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Brand.IsDuplicated(brand.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al editar la marca",
                            message = "Ya existe otra con el mismo nombre",
                        });
                    }
                    _workContainer.Brand.Update(brand.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = brand.CreateViewModel,
                        message = "La marca se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar la marca",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar la marca",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeState(long id)
        {
            try
            {
                var brand = _workContainer.Brand.GetOne(id);
                if (brand != null)
                {
                    _workContainer.Brand.ChangeState(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = brand,
                        message = "La marca se dió de " + (brand.IsActive ? "alta" : "baja") + " correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "No se encontró la marca solicitada",
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "Intente nuevamente o comuníquese para soporte",
                    error = e.Message,
                });
            }
        }
    }
}
