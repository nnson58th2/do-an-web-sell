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
    public class nhacungcapController : BaseController
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        string LayMaNCC()
        {
            var maMax = db.NhaCungCap.ToList().Select(n => n.MaNCC).Max();
            int maNCC = int.Parse(maMax.Substring(3)) + 1;
            string result = String.Concat("0", maNCC.ToString());
            return "NCC" + result.Substring(maNCC.ToString().Length - 1);
        }

        // GET: admin/nhacungcap
        public ActionResult index(string search, int page = 1, int pageSize = 5)
        {
            var dao = new SupplierDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: admin/nhacungcap/Create
        public ActionResult create()
        {
            ViewBag.MaNCC = LayMaNCC();
            return View();
        }

        // POST: admin/nhacungcap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "MaNCC,TenNCC,DiaChi,SoDienThoai")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                nhaCungCap.MaNCC = LayMaNCC();
                db.NhaCungCap.Add(nhaCungCap);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(nhaCungCap);
        }

        // GET: admin/nhacungcap/Edit/5
        public ActionResult edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCap.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // POST: admin/nhacungcap/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "MaNCC,TenNCC,DiaChi,SoDienThoai")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaCungCap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(nhaCungCap);
        }

        // GET: admin/nhacungcap/Delete/5
        [HttpDelete]
        public ActionResult delete(string id)
        {
            new BaseDao().DeleteSuppliere(id);
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
