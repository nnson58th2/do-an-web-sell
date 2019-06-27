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
    public class nhanvienController : BaseController
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        // GET: Mã nhân viên tự động
        string LayMaNV()
        {
            var maMax = db.NhanVien.ToList().Select(n => n.MaNV).Max();
            int maNV = int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("00", maNV.ToString());
            return "NV" + NV.Substring(maNV.ToString().Length - 1);
        }

        public ActionResult index(string search, int page = 1, int pageSize = 5)
        {
            var dao = new EmployeesDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: admin/nhanvien/Details/5
        public ActionResult details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: admin/nhanvien/Create
        public ActionResult create()
        {
            // Tạo mã nhân viên tự động
            ViewBag.MaNhanVien = LayMaNV();
            ViewBag.MaBP = new SelectList(db.BoPhan, "MaBP", "TenBP");
            return View();
        }

        // POST: admin/nhanvien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,Luong,AnhNV,SDT,DiaChi,Email,MaBP,UserID")] NhanVien nhanVien)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgNV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/assets/admin/images/" + postedFileName);
            imgNV.SaveAs(path);

            if (ModelState.IsValid)
            {
                nhanVien.MaNV = LayMaNV();
                nhanVien.AnhNV = postedFileName;
                db.NhanVien.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            ViewBag.MaBP = new SelectList(db.BoPhan, "MaBP", "TenBP", nhanVien.MaBP);
            return View(nhanVien);
        }

        // GET: admin/nhanvien/Edit/5
        public ActionResult edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBP = new SelectList(db.BoPhan, "MaBP", "TenBP", nhanVien.MaBP);
            return View(nhanVien);
        }

        // POST: admin/nhanvien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,Luong,AnhNV,SDT,DiaChi,Email,MaBP,UserID")] NhanVien nhanVien)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgNV = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/assets/admin/images/" + postedFileName);
                imgNV.SaveAs(path);
            }
            catch
            { }

            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.MaBP = new SelectList(db.BoPhan, "MaBP", "TenBP", nhanVien.MaBP);
            return View(nhanVien);
        }

        [HttpDelete]
        public ActionResult delete(string id)
        {
            new BaseDao().DeleteEmployees(id);
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
