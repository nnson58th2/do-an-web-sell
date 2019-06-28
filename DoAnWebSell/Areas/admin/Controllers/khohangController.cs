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
    public class khohangController : BaseController
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        string LayMaKho()
        {
            var maMax = db.KhoHang.ToList().Select(n => n.MaKho).Max();
            int maKho = int.Parse(maMax.Substring(2)) + 1;
            string result = String.Concat("0", maKho.ToString());
            return "MK" + result.Substring(maKho.ToString().Length - 1);
        }

        // GET: admin/khohang
        public ActionResult index(string search, int page = 1, int pageSize = 5)
        {
            var dao = new WarehouseDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: admin/khohang/Details/5
        public ActionResult details(string id)
        {
            var chiTietKho = new WarehouseDao().ListOrder(id);
            return View(chiTietKho);
        }

        // GET: admin/khohang/Create
        public ActionResult create()
        {
            ViewBag.MaKho = LayMaKho();
            return View();
        }

        // POST: admin/khohang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "MaKho,TenKho")] KhoHang khoHang)
        {
            if (ModelState.IsValid)
            {
                khoHang.MaKho = LayMaKho();
                db.KhoHang.Add(khoHang);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(khoHang);
        }

        // GET: admin/khohang/Edit/5
        public ActionResult edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoHang khoHang = db.KhoHang.Find(id);
            if (khoHang == null)
            {
                return HttpNotFound();
            }

            return View(khoHang);
        }

        // POST: admin/khohang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "MaKho,TenKho")] KhoHang khoHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khoHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(khoHang);
        }

        // GET: admin/khohang/Delete/5
        [HttpDelete]
        public ActionResult delete(string id)
        {
            new BaseDao().DeleteWareHouse(id);
            return RedirectToAction("index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
