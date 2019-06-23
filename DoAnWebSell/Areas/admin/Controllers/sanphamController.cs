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
    public class sanphamController : Controller
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        // GET: admin/sanpham
        public ActionResult index(string search, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: admin/sanpham/Details/5
        public ActionResult details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: admin/sanpham/Create
        public ActionResult create()
        {
            ViewBag.DanhMucSanPhamID = new SelectList(db.DanhMucSanPham, "Id", "Name");
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC");
            return View();
        }

        // POST: admin/sanpham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "MaSP,TenSP,MetaTitle,SoLuong,DonGia,GiaKhuyenMai,HinhAnh,MoTa,ChiTiet,ThoiHanBaoHanh,NgaySanXuat,TrangThai,NgayTao,TaoBoi,DanhMucSanPhamID,MaNCC")] SanPham sanPham)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgSP = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgSP.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/assets/admin/images/" + postedFileName);
            imgSP.SaveAs(path);

            if (ModelState.IsValid)
            {
                sanPham.HinhAnh = postedFileName;
                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            ViewBag.DanhMucSanPhamID = new SelectList(db.DanhMucSanPham, "Id", "Name", sanPham.DanhMucSanPhamID);
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // GET: admin/sanpham/Edit/5
        public ActionResult edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.DanhMucSanPhamID = new SelectList(db.DanhMucSanPham, "Id", "Name", sanPham.DanhMucSanPhamID);
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // POST: admin/sanpham/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "MaSP,TenSP,MetaTitle,SoLuong,DonGia,GiaKhuyenMai,HinhAnh,MoTa,ChiTiet,ThoiHanBaoHanh,NgaySanXuat,TrangThai,NgayTao,TaoBoi,DanhMucSanPhamID,MaNCC")] SanPham sanPham)
        {
            var imgSP = Request.Files["Avatar"];

            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgSP.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/assets/admin/images/" + postedFileName);
                imgSP.SaveAs(path);
            }
            catch
            { }

            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.DanhMucSanPhamID = new SelectList(db.DanhMucSanPham, "Id", "Name", sanPham.DanhMucSanPhamID);
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // GET: admin/sanpham/Delete/5
        [HttpDelete]
        public ActionResult delete(long id)
        {
            new BaseDao().DeleteProduct(id);
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
