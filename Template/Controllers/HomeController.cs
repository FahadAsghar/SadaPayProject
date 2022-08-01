using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Models;

namespace Template.Controllers
{
    public class HomeController : Controller
    {
        public static List<CartN> product = new List<CartN>();
        Database1Entities db = new Database1Entities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Detail(int id)
        {
            var product = db.Products.Where(a => a.Id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Detail(int id,string qtyn)
        {
            var pro = db.Products.Where(a => a.Id == id).FirstOrDefault();
            bool flag = false;
            int count = 0;
            if(product.Count > 0)
            {
                foreach (var item in product)
                {
                    if(item.Id == pro.Id)
                    {
                        flag = true;
                        break;
                    }
                    count++;
                }
                if (flag)
                {
                    product[count].qty = qtyn;
                    product[count].Total = (Convert.ToInt32(pro.Price) * Convert.ToInt32(qtyn)).ToString();
                }
                else
                {
                    var nPro = new CartN
                    {
                        Id = pro.Id,
                        Product_Name = pro.Product_Name,
                        Product_Brand = pro.Product_Brand,
                        Price = pro.Price,
                        Total = (Convert.ToInt32(pro.Price) * Convert.ToInt32(qtyn)).ToString(),
                        Image = pro.Image,
                        qty = qtyn
                    };
                    product.Add(nPro);
                }
            }
            else
            {
                var nPro = new CartN
                {
                    Id = pro.Id,
                    Product_Name = pro.Product_Name,
                    Product_Brand = pro.Product_Brand,
                    Price = pro.Price,
                    Total = (Convert.ToInt32(pro.Price) * Convert.ToInt32(qtyn)).ToString(),
                    Image = pro.Image,
                    qty = qtyn
                };
                product.Add(nPro);
            }
            
            return RedirectToAction("Cart");
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Cart()
        {
            int st = 0;
            ViewBag.tax = 0;
            if(product.Count > 0)
            {
                ViewBag.tax = 60;
                foreach (var item in product)
                {
                    st += Convert.ToInt32(item.Total);
                    ViewBag.subtotal = st;
                }
            }
            else
            {
                ViewBag.subtotal = 0;

            }

            ViewBag.Total = st + ViewBag.tax;
            return View(product);
        }
        public ActionResult Delete_cart(int id)
        {
            if(product.Count > 0)
            {
                foreach (var item in product)
                {
                    if (item.Id == id)
                    {
                        product.Remove(item);
                        break;
                    }
                }
            }
           
            return RedirectToAction("Cart", "Home");
        }
        public ActionResult Order()
        {
            if(Session["userId"] != null)
            {
                int id = Convert.ToInt32(Session["userId"]);
                List<Order> order = new List<Order>();
                foreach (var item in product)
                {
                    order.Add(new Order
                    {
                        user_id = id,
                        product_id = item.Id,
                        qty = Convert.ToInt32(item.qty),
                        date = DateTime.Now.ToString()                   
                    });
                }
                foreach (var item in order)
                {
                    db.Orders.Add(item);
                    db.SaveChanges();
                }
                product.Clear();
                return RedirectToAction("Cart", "Home");
            }
            return RedirectToAction("Login","Account");
        }
    }
}