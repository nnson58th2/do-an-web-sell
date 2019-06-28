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
    public class menuController : BaseController
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        // GET: admin/menu
        public ActionResult index()
        {
            var menu = db.Menu.Include(m => m.MenuType);
            return View(menu.ToList());
        }

        // GET: admin/menu/Create
        public ActionResult create()
        {
            long maMax = db.Menu.ToList().Select(n => n.Id).Max();
            ViewBag.Id = maMax + 1;
            ViewBag.TypeID = new SelectList(db.MenuType, "Id", "Name");
            return View();
        }

        // POST: admin/menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "Id,Text,Link,Target,TypeID")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                long maMax = db.Menu.ToList().Select(n => n.Id).Max();
                menu.Id = maMax + 1;
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            ViewBag.TypeID = new SelectList(db.MenuType, "Id", "Name", menu.TypeID);
            return View(menu);
        }

        // GET: admin/menu/Edit/5
        public ActionResult edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.MenuType, "Id", "Name", menu.TypeID);
            return View(menu);
        }

        // POST: admin/menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "Id,Text,Link,Target,TypeID")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeID = new SelectList(db.MenuType, "Id", "Name", menu.TypeID);
            return View(menu);
        }

        // GET: admin/menu/Delete/5
        [HttpDelete]
        public ActionResult delete(int id)
        {
            new BaseDao().DeleteMenu(id);
            return RedirectToAction("index");
        }
    }
}
