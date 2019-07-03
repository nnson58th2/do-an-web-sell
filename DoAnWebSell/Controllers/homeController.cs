using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using DoAnWebSell.Models;
using DoAnWebSell.Common;

namespace DoAnWebSell.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        public ActionResult index()
        {
            ViewBag.Slides = new SlideDao();
            ViewBag.NewProduct = new ProductDao().ListNewProduct(9);
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CART_SEDSSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}