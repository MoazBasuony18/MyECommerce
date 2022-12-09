using Microsoft.AspNetCore.Mvc;
using MyECommerce.Data;
using MyECommerce.Models;
using MyECommerce.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext db;
        public OrderController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order anOrder)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }

            anOrder.OrderNo = GetOrderNo();
            db.Orders.Add(anOrder);
            await db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }


    public string GetOrderNo()
        {
            int rowCount = db.Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
