using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;

namespace DoAnWebSell.Areas.admin.Controllers
{
    public class danhmucsanphamController : Controller
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        public ActionResult index(string search, int page = 1, int pageSize = 5)
        {
            var dao = new ProductCategoryDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "Id,Name,MetaTitle,ParentID,CreateBy")] DanhMucSanPham danhMucSanPham)
        {
            if (ModelState.IsValid)
            {
                db.DanhMucSanPham.Add(danhMucSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhMucSanPham);
        }

        public ActionResult edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucSanPham danhMucSanPham = db.DanhMucSanPham.Find(id);
            if (danhMucSanPham == null)
            {
                return HttpNotFound();
            }
            return View(danhMucSanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "Id,Name,MetaTitle,ParentID,CreateBy")] DanhMucSanPham danhMucSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMucSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhMucSanPham);
        }

        [HttpDelete]
        public ActionResult delete(long id)
        {
            var productCategory = db.DanhMucSanPham.Find(id);
            db.DanhMucSanPham.Remove(productCategory);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
