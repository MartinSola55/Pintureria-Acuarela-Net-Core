using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;
using Pintureria_Acuarela.Models.ViewModels.Colors;

namespace Pintureria_Acuarela.Controllers
{
    [Authorize(Roles = Constants.Admin)]
    public class ColorsController(IWorkContainer workContainer) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IndexViewModel viewModel = new()
                {
                    Colors = _workContainer.Color.GetAll(),
                    CreateViewModel = new Color()
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
                    if (_workContainer.Color.IsDuplicated(brand.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al agregar el color",
                            message = "Ya existe otro con el mismo nombre",
                        });
                    }
                    _workContainer.Color.Add(brand.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = brand.CreateViewModel,
                        message = "El color se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar el color",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar el color",
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
                    if (_workContainer.Color.IsDuplicated(brand.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al editar el color",
                            message = "Ya existe otro con el mismo nombre",
                        });
                    }
                    _workContainer.Color.Update(brand.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = brand.CreateViewModel,
                        message = "El color se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar el color",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar el color",
                message = "Alguno de los campos ingresados no es válido",
            });
        }
    }
}
