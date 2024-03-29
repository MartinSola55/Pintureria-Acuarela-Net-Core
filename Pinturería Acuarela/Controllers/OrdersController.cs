﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Mono.TextTemplating;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;
using Pintureria_Acuarela.Models.ViewModels.Orders;
using System.Linq.Expressions;
using System.Net;

namespace Pintureria_Acuarela.Controllers
{
    [Authorize]
    public class OrdersController(IWorkContainer workContainer) : Controller
    {
        private readonly IWorkContainer _workContainer = workContainer;

        private BadRequestObjectResult CustomBadRequest(string title, string message, string? error = null)
        {
            return BadRequest(new
            {
                success = false,
                title,
                message,
                error,
            });
        }
        public IActionResult Index()
        {
            try
            {
                ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                IndexViewModel viewModel = new()
                {
                    Years = _workContainer.Order.GetYears(),
                };

                if (_workContainer.ApplicationUser.GetRole(user.Id).Name == Constants.Admin)
                {
                    Expression<Func<Order, bool>> filter = order => order.CreatedAt.Year == DateTime.UtcNow.AddHours(-3).Year;
                    viewModel.Orders = _workContainer.Order.GetAll(filter, includeProperties: "User.Business").OrderByDescending(x => x.CreatedAt).ThenBy(x => x.Status);
                    return View(viewModel);
                }
                else
                {
                    Expression<Func<Order, bool>> filter = order => order.CreatedAt.Year == DateTime.UtcNow.AddHours(-3).Year && order.UserID.Equals(user.Id);
                    viewModel.Orders = _workContainer.Order.GetAll(filter, includeProperties: "User.Business").OrderByDescending(x => x.CreatedAt).ThenBy(x => x.Status);
                    return View(viewModel);
                }
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult New()
        {
            try
            {
                ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                Expression<Func<ProductBusiness, bool>> filter = x => x.BusinessID == user.BusinessID;
                IEnumerable<ProductBusiness> products = _workContainer.ProductBusiness.GetAll(filter, includeProperties: "Product, Product.Brand, Product.Category, Product.Subcategory, Product.Capacity, Product.Color");
                if (!products.Any())
                {
                    return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "La sucursal no cuenta con ningún producto asociado", ErrorCode = 404 });
                }

                NewViewModel viewModel = new()
                {
                    Products = products.ToList(),
                    CreateViewModel = new Order()
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            try
            {
                Expression<Func<Order, bool>> filter = order => order.ID == id;
                Order order = _workContainer.Order.GetFirstOrDefault(filter, includeProperties: "Products, Products.Product, Products.Product.Brand, Products.Product.Capacity");
                if (order == null)
                {
                    return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "La orden no existe", ErrorCode = 404 });
                }

                return View(order);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        // EMPLEADO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewViewModel viewModel)
        {
            try
            {
                ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                Order order = viewModel.CreateViewModel;
                order.UserID = user.Id;
                order.CreatedAt = DateTime.UtcNow.AddHours(-3);
                order.Status = false;

                _workContainer.BeginTransaction();
                _workContainer.Order.Add(order);
                _workContainer.Save();

                foreach (ProductOrder product in order.Products)
                {
                    ProductBusiness prod = _workContainer.ProductBusiness.GetOne(user.BusinessID, product.ProductID);

                    if (prod == null) return CustomBadRequest(title: "Error al crear la orden", message: "El producto ingresado no existe en los registros");

                    product.OrderID = order.ID;
                    product.QuantitySend = 0;
                }
                _workContainer.Save();
                _workContainer.Commit();
                return Json(new
                {
                    success = true,
                    message = "La orden se creó correctamente",
                });
            }
            catch (FormatException)
            {
                _workContainer.Rollback();
                return CustomBadRequest(title: "Error al editar la orden", message: "Alguno de los campos ingresados no posee un formato válido");
            }
            catch (Exception e)
            {
                _workContainer.Rollback();
                return CustomBadRequest(title: "Error al editar la orden", message: "Intente nuevamente o comuníquese para soporte", e.Message);
            }
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(long id)
        {
            try
            {
                var order = _workContainer.Order.GetOne(id);
                if (order != null)
                {
                    _workContainer.Order.SoftDelete(id);

                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "La orden se eliminó correctamente",
                    });
                }
                return CustomBadRequest(title: "Error al eliminar", message: "No se encontró la orden solicitada");
            }
            catch (SqlException e)
            {
                return CustomBadRequest(title: "Error en la base de datos", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al eliminar la orden", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetOrdersByYear(string year)
        {
            try
            {
                ApplicationUser user = _workContainer.ApplicationUser.GetFirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                Expression<Func<Order, bool>> filter;
                if (_workContainer.ApplicationUser.GetRole(user.Id).Name == Constants.Admin)
                {
                    filter = order => order.CreatedAt.Year.ToString() == year;
                } else
                {
                    filter = order => order.CreatedAt.Year.ToString() == year && order.UserID.Equals(user.Id);
                }

                var orders = _workContainer.Order.GetAll(filter, includeProperties: "User.Business").OrderByDescending(x => x.CreatedAt).ThenBy(x => x.Status)
                   .Select(x => new
                   {
                       id = x.ID,
                       createdAt = x.CreatedAt,
                       status = x.Status == false ? "En espera" : "Cerrada",
                       address = x.User.Business.Address,
                   });
                return Json(new
                {
                    success = true,
                    data = orders
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al recuperar las ordenes del " + year, message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetBusinessesAvailable(long productID, long orderID)
        {
            try
            {
                Expression<Func<Order, bool>> filterOrder = order => order.ID == orderID;
                Order order = _workContainer.Order.GetFirstOrDefault(filterOrder, includeProperties: "User");
                Expression<Func<ProductBusiness, bool>> filter = prod => prod.ProductID == productID && prod.BusinessID != order.User.BusinessID && prod.Stock > 0;
                var businesses = _workContainer.ProductBusiness.GetAll(filter, includeProperties: "Business").ToList();
                return Json(new
                {
                    success = true,
                    data = businesses
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al recuperar las sucursales", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmProduct(ProductOrder productOrder)
        {
            try
            {
                Order order = _workContainer.Order.GetFirstOrDefault(x => x.ID == productOrder.OrderID, includeProperties: "User");
                ProductOrder product = _workContainer.ProductOrder.GetOne(productOrder.OrderID, productOrder.ProductID);
                if (product == null) return CustomBadRequest(title: "Error al confirmar el producto", message: "El producto ingresado no existe en los registros");
                ProductBusiness productBusiness = _workContainer.ProductBusiness.GetOne(productOrder.BusinessID.Value, productOrder.ProductID);
                if (productBusiness == null) return CustomBadRequest(title: "Error al confirmar el producto", message: "La sucursal ingresada no existe en los registros");
                if (product.QuantitySend > productBusiness.Stock) return CustomBadRequest(title: "Error al confirmar el producto", message: "La sucursal no cuenta con stock suficiente");

                _workContainer.BeginTransaction();

                product.Status = true;
                product.QuantitySend = productOrder.QuantitySend;
                product.BusinessID = productOrder.BusinessID;
                productBusiness.Stock -= productOrder.QuantitySend;
                _workContainer.ProductBusiness.GetFirstOrDefault(x => x.ProductID == productOrder.ProductID && x.BusinessID == order.User.BusinessID).Stock += productOrder.QuantitySend;

                _workContainer.Save();
                _workContainer.Commit();
                return Json(new
                {
                    success = true,
                    message = "El producto se confirmó correctamente",
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al confirmar el producto", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UnconfirmProduct(ProductOrder productOrder)
        {
            try
            {
                Order order = _workContainer.Order.GetFirstOrDefault(x => x.ID == productOrder.OrderID, includeProperties: "User");
                ProductOrder product = _workContainer.ProductOrder.GetOne(productOrder.OrderID, productOrder.ProductID);
                if (product == null) return CustomBadRequest(title: "Error al cancelar la confirmación del producto", message: "El producto ingresado no existe en los registros");
                ProductBusiness productBusiness = _workContainer.ProductBusiness.GetOne(product.BusinessID.Value, productOrder.ProductID);
                if (productBusiness == null) return CustomBadRequest(title: "Error al cancelar la confirmación del producto", message: "La sucursal ingresada no existe en los registros");

                _workContainer.BeginTransaction();

                _workContainer.ProductBusiness.GetFirstOrDefault(x => x.ProductID == productOrder.ProductID && x.BusinessID == order.User.BusinessID).Stock -= product.QuantitySend;
                productBusiness.Stock += product.QuantitySend;
                product.Status = false;
                product.QuantitySend = 0;
                product.BusinessID = null;

                _workContainer.Save();
                _workContainer.Commit();
                return Json(new
                {
                    success = true,
                    message = "Se canceló la confirmación del producto correctamente",
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al cancelar la confirmación del producto", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmOrder(long id)
        {
            try
            {
                Order order = _workContainer.Order.GetFirstOrDefault(x => x.ID == id, includeProperties: "Products");
                if (order == null) return CustomBadRequest(title: "Error al cerrar la orden", message: "La orden ingresada no existe en los registros");

                _workContainer.BeginTransaction();

                order.Status = true;

                foreach(ProductOrder product in order.Products)
                {
                    product.Status = true;
                }

                _workContainer.Save();
                _workContainer.Commit();
                return Json(new
                {
                    success = true,
                    message = "La orden se cerró correctamente",
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al cerrar la orden", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UnconfirmOrder(long id)
        {
            try
            {
                Order order = _workContainer.Order.GetFirstOrDefault(x => x.ID == id, includeProperties: "Products, User");
                if (order == null) return CustomBadRequest(title: "Error al cancelar la confirmación de la orden", message: "La orden ingresada no existe en los registros");

                _workContainer.BeginTransaction();

                order.Status = false;

                foreach (ProductOrder product in order.Products)
                {
                    if (product.BusinessID != null)
                    {
                        ProductBusiness businessSender = _workContainer.ProductBusiness.GetOne(product.BusinessID.Value, product.ProductID);
                        businessSender.Stock += product.QuantitySend;
                        ProductBusiness businessReceiver = _workContainer.ProductBusiness.GetOne(order.User.BusinessID, product.ProductID);
                        businessReceiver.Stock -= product.QuantitySend;
                    }
                    product.Status = false;
                    product.QuantitySend = 0;
                    product.BusinessID = null;
                }

                _workContainer.Save();
                _workContainer.Commit();
                return Json(new
                {
                    success = true,
                    message = "Se canceló la confirmación de la orden correctamente",
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al confirmar la orden", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }
    }
}
