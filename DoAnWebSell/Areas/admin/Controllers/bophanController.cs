using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace DoAnWebSell.Areas.admin.Controllers
{
    public class bophanController : BaseController
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        // GET: admin/bophan
        public ActionResult index()
        {
            return View(db.BoPhan.ToList());
        }

        // GET: admin/bophan/Create
        public ActionResult create()
        {
            return View();
        }

        // POST: admin/bophan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "MaBP,TenBP")] BoPhan boPhan)
        {
            if (ModelState.IsValid)
            {
                db.BoPhan.Add(boPhan);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(boPhan);
        }

        // GET: admin/bophan/Edit/5
        public ActionResult edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoPhan boPhan = db.BoPhan.Find(id);
            if (boPhan == null)
            {
                return HttpNotFound();
            }
            return View(boPhan);
        }

        // POST: admin/bophan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "MaBP,TenBP")] BoPhan boPhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boPhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(boPhan);
        }

        [HttpDelete]
        public ActionResult delete(string id)
        {
            var parts = db.BoPhan.Find(id);
            db.BoPhan.Remove(parts);
            db.SaveChanges();
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
