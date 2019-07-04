﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace DoAnWebSell.Controllers
{
    public class productController : Controller
    {
        // GET: product
        public ActionResult index()
        {
            return View();
        }

        public ActionResult productAll(int page = 1, int pageSize = 8)
        {
            var dao = new ProductDao();
            var model = dao.ListAllProduct(page, pageSize);
            return View(model);
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 2)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            double quantity = ((double)(totalRecord) / (pageSize));

            totalPage = (int)Math.Ceiling(quantity);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult productCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public JsonResult listName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult category(long id, int page = 1, int pageSize = 4)
        {
            var category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            var model = new ProductDao().ListByCategoryId(id, page, pageSize);
            return View(model);
        }

        public ActionResult detail(long id, int top = 4)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.DanhMucSanPhamID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(id, top);
            return View(product);
        }
    }
}