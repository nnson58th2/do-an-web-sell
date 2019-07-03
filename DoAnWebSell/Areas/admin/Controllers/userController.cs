using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using DoAnWebSell.Common;
using System.Net;

namespace DoAnWebSell.Areas.admin.Controllers
{
    public class userController : BaseController
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        // GET: admin/user
        public ActionResult index(string search, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            //Tạo page sử dụng Pagedlist
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: admin/user/Details/5
        public ActionResult details(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
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
        public ActionResult delete(NhanVien useEmployesID, KhachHang useCustomerID, long id)
        {
            // Kiểm tra xem user cần xóa có năm trong user id của nhân viên hay không
            if (useEmployesID.UserID == id)
            {
                // Xóa nhân viên ra khỏi danh sách
                var employes = db.NhanVien.Single(x => x.UserID == id);
                db.NhanVien.Remove(employes);
                db.SaveChanges();
            }
            else
            {
                // Xóa khách hàng ra khỏi danh sách
                var customer = db.KhachHang.Single(x => x.UserID == id);
                db.KhachHang.Remove(customer);
                db.SaveChanges();
            }                       

            // Xóa user ra khỏi danh sách
            new UserDao().Delete(id);
            return RedirectToAction("index");
        }
    }
}