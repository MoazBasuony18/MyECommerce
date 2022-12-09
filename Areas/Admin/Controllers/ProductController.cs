using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyECommerce.Data;
using MyECommerce.Models;
using MyECommerce.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        ApplicationDbContext db;
        private readonly IECommerceRepository<Products> productsRepository;
        private readonly IHostingEnvironment he;
        private readonly IECommerceRepository<ProductTypes> productTypesRepository;
        private readonly IECommerceRepository<TagNames> tagNamesRepository;

        public ProductController(IECommerceRepository<Products> productsRepository,IHostingEnvironment he, 
            IECommerceRepository<ProductTypes> productTypesRepository, 
            IECommerceRepository<TagNames> tagNamesRepository, ApplicationDbContext db)
        {
            this.productsRepository = productsRepository;
            this.he = he;
            this.productTypesRepository = productTypesRepository;
            this.tagNamesRepository = tagNamesRepository;
            this.db = db;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var data = productsRepository.List();
            return View(data);
        }

        [HttpPost]
        public IActionResult Index(decimal? lowAmount,decimal? largeAmount)
        {
            var product = db.Products.Include(a => a.ProductTypes).Include(b => b.TagNames)
                .Where(c => c.Price >= lowAmount && c.Price >= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                 product = db.Products.Include(a => a.ProductTypes).Include(b => b.TagNames).ToList();
            }
            return View(product);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var data = productsRepository.FindById(id);
            if (data == null)
            {
                return View("NotFound");  
            }
            return View(data);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(productTypesRepository.List(), "Id", "ProductType");
            ViewData["tagNameId"] = new SelectList(tagNamesRepository.List(), "Id", "TagName");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products,IFormFile image)
        {
            try
            {
                var searchProduct = db.Products.FirstOrDefault(a => a.Name == products.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(productTypesRepository.List(), "Id", "ProductType");
                    ViewData["tagNameId"] = new SelectList(tagNamesRepository.List(), "Id", "TagName");
                    return View(products);
                }
                if (image != null)
                {
                    var name = Path.Combine(he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    image.CopyTo(new FileStream(name, FileMode.Create));
                    products.Image = "images/"+image.FileName;
                }
                if (image == null)
                {
                    products.Image = "Images/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg";
                }
                productsRepository.Add(products);
                TempData["save"] = "New product has been saved successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["productTypeId"] = new SelectList(productTypesRepository.List(), "Id", "ProductType");
            ViewData["tagNameId"] = new SelectList(tagNamesRepository.List(), "Id", "TagName");
            var data = productsRepository.FindById(id);
            return View(data);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Products products,IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    var name = Path.Combine(he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    image.CopyTo(new FileStream(name, FileMode.Create));
                    products.Image = "images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "Images/depositphotos_227725020-stock-illustration-image-available-icon-flat-vector.jpg";
                }
                productsRepository.Update(id, products);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = productsRepository.FindById(id);
            return View(data);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                productsRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string searchString)
        {
            var result =  productsRepository.Search(searchString);
            return View("Index", result);
        }
    }
}
