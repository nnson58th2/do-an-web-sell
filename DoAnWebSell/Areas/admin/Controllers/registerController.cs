using DoAnWebSell.Common;
using DoAnWebSell.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebSell.Areas.admin.Controllers
{
    public class registerController : Controller
    {

        // GET: admin/register
        public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new QuanTri();
                    user.UserName = model.UserName;
                    //Mã hóa password trước khi thêm vào bản ghi
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.HoTen = model.Name;
                    user.DienThoai = model.Phone;
                    user.Email = model.Email;
                    user.DiaChi = model.Address;
                    user.Quyen = false;
                    user.TrangThai = true;

                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        return RedirectToAction("index", "login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tài khoản không thành công");
                    }
                }
            }
            return View("index");
        }
    }
}