using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyECommerce.Data;
using MyECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext db;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(db.ApplicationUsers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    TempData["Save"] = "User has been created successfully";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var erorr in result.Errors)
                {
                    ModelState.AddModelError("", erorr.Description);
                }
            }
            return View();
        }


        public IActionResult Edit(string id)
        {
            var user = db.ApplicationUsers.FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var userInfo = db.ApplicationUsers.SingleOrDefault(a => a.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            var result = await userManager.UpdateAsync(userInfo);
            if (result.Succeeded)
            {
                TempData["Save"] = "User has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }
        public IActionResult Details(string id)
        {
            var user = db.ApplicationUsers.SingleOrDefault(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        public IActionResult Locout(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = db.ApplicationUsers.SingleOrDefault(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Locout(ApplicationUser user)
        {
            var userInfo = db.ApplicationUsers.SingleOrDefault(a => a.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.LockoutEnd = DateTime.Now.AddYears(100);
            int rowEffected = db.SaveChanges();
            if (rowEffected > 0)
            {
                TempData["Save"] = "User has been locout successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }
        public IActionResult Active(string id)
        {
            var user = db.ApplicationUsers.SingleOrDefault(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Active(ApplicationUser user)
        {
            var userInfo = db.ApplicationUsers.SingleOrDefault(a => a.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.LockoutEnd = DateTime.Now.AddDays(-1);
            int rowEffected = db.SaveChanges();
            if (rowEffected > 0)
            {
                TempData["Save"] = "User has been Active successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = db.ApplicationUsers.SingleOrDefault(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            var userInfo = db.ApplicationUsers.SingleOrDefault(a => a.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            db.ApplicationUsers.Remove(userInfo);
            int rowEffected = db.SaveChanges();
            if (rowEffected > 0)
            {
                TempData["Save"] = "User has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }
    }
}
