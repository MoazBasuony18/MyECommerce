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
    public class TagNamesController : Controller
    {
        private readonly IECommerceRepository<TagNames> tagNamesRepository;

        public TagNamesController(IECommerceRepository<TagNames> tagNamesRepository)
        {
            this.tagNamesRepository = tagNamesRepository;
        }
        // GET: TagNamesController
        public ActionResult Index()
        {
            var data = tagNamesRepository.List();
            return View(data);
        }

        // GET: TagNamesController/Details/5
        public ActionResult Details(int id)
        {
            var data = tagNamesRepository.FindById(id);
            return View(data);
        }

        // GET: TagNamesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagNamesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagNames tagNames)
        {
            try
            {
                tagNamesRepository.Add(tagNames);
                TempData["save"] = "New tag has been saved successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagNamesController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = tagNamesRepository.FindById(id);
            return View(data);
        }

        // POST: TagNamesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TagNames tagNames)
        {
            try
            {
                tagNamesRepository.Update(id, tagNames);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagNamesController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = tagNamesRepository.FindById(id);
            return View(data);
        }

        // POST: TagNamesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                tagNamesRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
