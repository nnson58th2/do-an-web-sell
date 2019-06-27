﻿using DoAnWebSell.Common;
using DoAnWebSell.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebSell.Controllers
{
    public class userController : Controller
    {
        private DatabaseSellEntities db = new DatabaseSellEntities();

        // GET: Mã khách tự động
        string getKeyCustomer()
        {
            var maMax = db.KhachHang.ToList().Select(n => n.MaKH).Max();
            int maKH = int.Parse(maMax.Substring(2)) + 1;
            string KH = String.Concat("000", maKH.ToString());
            return "KH" + KH.Substring(maKH.ToString().Length - 1);
        }

        // GET: user
        [HttpGet]
        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var customerDao = new CustomersDao();
                var userDao = new UserDao();

                if (userDao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (userDao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    // Thêm thông tin đăng nhập vào bảng quản trị
                    var user = new QuanTri();
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.HoTen = model.Name;
                    user.DienThoai = model.Phone;
                    user.Email = model.Email;
                    user.DiaChi = model.Address;
                    user.Quyen = false;
                    user.TrangThai = true;

                    // Thêm thông tin đăng nhập vào bảng khách hàng
                    var customer = new KhachHang();
                    customer.MaKH = getKeyCustomer();
                    customer.HoTenKH = model.Name;
                    customer.GioiTinh = model.Gender;
                    customer.DiaChi = model.Address;
                    customer.SDT = model.Phone;
                    customer.Email = model.Email;

                    //Lấy mã tài khoản và chuyển đổi sáng cho mã tài khoản của khác hàng
                    long keyCustomer = user.Id;
                    customer.UserID = keyCustomer;

                    var resultUser = userDao.Insert(user);                 
                    var resultCustomer = customerDao.Insert(customer);

                    if (resultUser > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công!");
                    }

                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.LoginUser(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.Id;
                    userSession.Name = user.HoTen;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        //// Đăng nhập bằng facebook
        //public ActionResult LoginFacebook()
        //{
        //    var fb = new FacebookClient();
        //    var loginUrl = fb.GetLoginUrl(new
        //    {
        //        client_id = ConfigurationManager.AppSettings["FbAppId"],
        //        client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
        //        redirect_uri = RedirectUri.AbsoluteUri,
        //        response_type = "code",
        //        scope = "email",
        //    });

        //    return Redirect(loginUrl.AbsoluteUri);
        //}

        //public ActionResult FacebookCallback(string code)
        //{
        //    var fb = new FacebookClient();
        //    dynamic result = fb.Post("oauth/access_token", new
        //    {
        //        client_id = ConfigurationManager.AppSettings["FbAppId"],
        //        client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
        //        redirect_uri = RedirectUri.AbsoluteUri,
        //        code = code
        //    });


        //    var accessToken = result.access_token;
        //    if (!string.IsNullOrEmpty(accessToken))
        //    {
        //        fb.AccessToken = accessToken;
        //        // Get the user's information, like email, first name, middle name etc
        //        dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
        //        string email = me.email;
        //        string userName = me.email;
        //        string firstname = me.first_name;
        //        string middlename = me.middle_name;
        //        string lastname = me.last_name;

        //        var user = new User();
        //        user.Email = email;
        //        user.UserName = email;
        //        user.Status = true;
        //        user.Name = firstname + " " + middlename + " " + lastname;
        //        user.CreatedDate = DateTime.Now;
        //        var resultInsert = new UserDao().InsertForFacebook(user);
        //        if (resultInsert > 0)
        //        {
        //            var userSession = new UserLogin();
        //            userSession.UserName = user.UserName;
        //            userSession.UserID = user.ID;
        //            Session.Add(CommonConstants.USER_SESSION, userSession);
        //        }
        //    }
        //    return Redirect("/");
        //}
    }
}