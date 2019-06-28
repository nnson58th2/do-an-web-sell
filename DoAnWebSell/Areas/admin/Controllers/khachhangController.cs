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
    public class khachhangController : BaseController
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        public ActionResult index(string search, int page = 1, int pageSize = 5)
        {
            var dao = new CustomersDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: admin/khachhang/Details/5
        public ActionResult details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHang.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: admin/khachhang/Edit/5
        public ActionResult edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHang.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.QuanTri, "Id", "UserName", khachHang.UserID);
            return View(khachHang);
        }

        // POST: admin/khachhang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "MaKH,HoTenKH,GioiTinh,DiaChi,SDT,Email,UserID")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.UserID = new SelectList(db.QuanTri, "Id", "UserName", khachHang.UserID);
            return View(khachHang);
        }

        // GET: admin/khachhang/Delete/5
        [HttpDelete]
        public ActionResult delete(string id)
        {
            new BaseDao().DeleteCustomer(id);
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
