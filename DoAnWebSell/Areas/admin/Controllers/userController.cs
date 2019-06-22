using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using DoAnWebSell.Common;

namespace DoAnWebSell.Areas.admin.Controllers
{
    public class userController : BaseController
    {
        // GET: admin/user
        public ActionResult index(string search, int page = 1, int pageSize = 3)
        {
            var dao = new UserDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(QuanTri user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //Mã hóa password trước khi thêm vào bản ghi
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;

                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm user thành công", "success"); 
                    return RedirectToAction("index", "user");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("index");
        }

        public ActionResult edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult edit(QuanTri user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //Kiểm tra người dùng có nhập password vào không
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                }

                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("index", "user");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập user không thành công");
                }
            }
            return View("index");
        }

        [HttpDelete]
        public ActionResult delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("index");
        }
    }
}