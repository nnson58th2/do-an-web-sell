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
    public class khachhangController : Controller
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        // GET: Mã khách tự động
        string LayMaKH()
        {
            var maMax = db.KhachHang.ToList().Select(n => n.MaKH).Max();
            int maKH = int.Parse(maMax.Substring(2)) + 1;
            string KH = String.Concat("000", maKH.ToString());
            return "KH" + KH.Substring(maKH.ToString().Length - 1);
        }

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

        // GET: admin/khachhang/Create
        public ActionResult create()
        {
            // Tạo mã khách hàng tự động
            ViewBag.MaKhachHang = LayMaKH();
            ViewBag.UserID = new SelectList(db.QuanTri, "Id", "UserName");
            return View();
        }

        // POST: admin/khachhang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "MaKH,HoKH,TenKH,GioiTinh,DiaChi,SDT,Email,UserID")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                khachHang.MaKH = LayMaKH();
                db.KhachHang.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            ViewBag.UserID = new SelectList(db.QuanTri, "Id", "UserName", khachHang.UserID);
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
        public ActionResult edit([Bind(Include = "MaKH,HoKH,TenKH,GioiTinh,DiaChi,SDT,Email,UserID")] KhachHang khachHang)
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
