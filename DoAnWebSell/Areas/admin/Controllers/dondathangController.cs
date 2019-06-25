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
    public class dondathangController : BaseController
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        public ActionResult index(string search, int page = 1, int pageSize = 5)
        {
            var dao = new OrderDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: admin/dondathang/Details/5
        public ActionResult details(long id)
        {
            var chiTietDatHang = new OrderDetailDao().ListOrder(id);
            return View(chiTietDatHang);
        }

        // GET: admin/dondathang/Edit/5
        public ActionResult edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHang.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "HoKH", donDatHang.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "HoNV", donDatHang.MaNV);
            return View(donDatHang);
        }

        // POST: admin/dondathang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "MaDon,NgayDat,TenNguoiDat,DiaChiGiaoHang,SoDienThoai,Email,MaNV,MaKH,TrangThai")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "HoKH", donDatHang.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "HoNV", donDatHang.MaNV);
            return View(donDatHang);
        }

        // GET: admin/dondathang/Delete/5

        //public ActionResult delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DonDatHang donDatHang = db.DonDatHang.Find(id);
        //    if (donDatHang == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(donDatHang);
        //}

        //// POST: admin/dondathang/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    DonDatHang donDatHang = db.DonDatHang.Find(id);
        //    db.DonDatHang.Remove(donDatHang);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpDelete]
        public ActionResult delete(int id)
        {
            new OrderDao().Delete(id);
            return RedirectToAction("index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
