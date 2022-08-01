using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Models;

namespace Template.Controllers
{
    public class AdminController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult Detail_product()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Add_Product()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Product(Product pro, HttpPostedFileBase img)
        {
            if (img.ContentLength > 0)
            {
                img.SaveAs(Server.MapPath("~/Content/images/" + img.FileName));
                pro.Image = img.FileName;
            }
            db.Products.Add(pro);
            db.SaveChanges();
            return RedirectToAction("Detail_product");
        }
        public ActionResult Edit_Product(int id)
        {
            var edit = db.Products.Where(a => a.Id == id).FirstOrDefault();

            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit_Product(Product pro, HttpPostedFileBase img,int id)
        {
            var edit = db.Products.Where(a => a.Id == id).FirstOrDefault();
            if(img != null)
            {
                if (img.ContentLength > 0)
                {
                    img.SaveAs(Server.MapPath("~/Content/images/" + img.FileName));
                    edit.Image = img.FileName;
                }
               
            }
            else
            {
                edit.Image = pro.Image;
            }
            edit.Product_Name = pro.Product_Name;
            edit.Product_Brand = pro.Product_Brand;
            edit.Price = pro.Price;
            edit.Quantity = pro.Quantity;


            db.SaveChanges();
            return RedirectToAction("Detail_product");
        }
        public ActionResult Delete_Product(int id)
        {
            var edit = db.Products.Where(a => a.Id == id).FirstOrDefault();
            db.Products.Remove(edit);
            db.SaveChanges();
            return RedirectToAction("Detail_product");
        }
        public ActionResult Detail_order(){
            return View(db.Orders.ToList());
        }
    }
}