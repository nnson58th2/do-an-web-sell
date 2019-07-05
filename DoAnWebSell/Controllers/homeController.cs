using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using DoAnWebSell.Models;
using DoAnWebSell.Common;
using Model.EF;

namespace DoAnWebSell.Controllers
{
    public class homeController : Controller
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        // GET: home
        public ActionResult index()
        {
            ViewBag.Slides = new SlideDao();
            ViewBag.NewProduct = new ProductDao().ListNewProduct(8);
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

        [HttpGet]
        public ActionResult SearchByPriceBetween1_5(decimal priceMin = 100000, decimal priceMax = 500000)
        {
            ViewBag.NewProduct = new ProductDao().ListNewProduct(8);
            var prices = db.SanPham.SqlQuery("Price_Between'" + priceMin + "','" + priceMax  + "'");
            var model = prices.OrderBy(x => x.DonGia).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchByPriceBetween5_20(decimal priceMin = 500000, decimal priceMax = 2000000)
        {
            ViewBag.NewProduct = new ProductDao().ListNewProduct(8);
            var prices = db.SanPham.SqlQuery("Price_Between'" + priceMin + "','" + priceMax + "'");
            var model = prices.OrderBy(x => x.DonGia).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchByPriceBetween20_40(decimal priceMin = 2000000, decimal priceMax = 4000000)
        {
            ViewBag.NewProduct = new ProductDao().ListNewProduct(8);
            var prices = db.SanPham.SqlQuery("Price_Between'" + priceMin + "','" + priceMax + "'");
            var model = prices.OrderBy(x => x.DonGia).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchByPriceBetween40_60(decimal priceMin = 4000000, decimal priceMax = 6000000)
        {
            ViewBag.NewProduct = new ProductDao().ListNewProduct(8);
            var prices = db.SanPham.SqlQuery("Price_Between'" + priceMin + "','" + priceMax + "'");
            var model = prices.OrderBy(x => x.DonGia).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchByPriceBetween60_100(decimal priceMin = 6000000, decimal priceMax = 10000000)
        {
            ViewBag.NewProduct = new ProductDao().ListNewProduct(8);
            var prices = db.SanPham.SqlQuery("Price_Between'" + priceMin + "','" + priceMax + "'");
            var model = prices.OrderBy(x => x.DonGia).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchByPriceBetween100_150(decimal priceMin = 10000000, decimal priceMax = 15000000)
        {
            ViewBag.NewProduct = new ProductDao().ListNewProduct(8);
            var prices = db.SanPham.SqlQuery("Price_Between'" + priceMin + "','" + priceMax + "'");
            var model = prices.OrderBy(x => x.DonGia).ToList();
            return View(model);
        }
    }
}