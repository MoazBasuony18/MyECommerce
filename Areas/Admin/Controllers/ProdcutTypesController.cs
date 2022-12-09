using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyECommerce.Models;
using MyECommerce.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class ProdcutTypesController : Controller
    {
        private readonly IECommerceRepository<ProductTypes> productTypesRepository;

        public ProdcutTypesController(IECommerceRepository<ProductTypes> productTypesRepository)
        {
            this.productTypesRepository = productTypesRepository;
        }
        // GET: ProdcutTypesController
        public ActionResult Index()
        {
            var data=productTypesRepository.List();
            return View(data);
        }

        // GET: ProdcutTypesController/Details/5
        public ActionResult Details(int id)
        {
            var data = productTypesRepository.FindById(id);
            return View(data);
        }

        // GET: ProdcutTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProdcutTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductTypes productTypes)
        {
            try
            {
                productTypesRepository.Add(productTypes);
                TempData["save"] = "Product type has been saved successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdcutTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = productTypesRepository.FindById(id);
            return View(data);
        }

        // POST: ProdcutTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductTypes productTypes)
        {
            try
            {
                productTypesRepository.Update(id, productTypes);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdcutTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = productTypesRepository.FindById(id);
            return View(data);
        }

        // POST: ProdcutTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductTypes productTypes)
        {
            try
            {
                productTypesRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
