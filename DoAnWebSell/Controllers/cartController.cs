using DoAnWebSell.Common;
using DoAnWebSell.Models;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using System.Web.Script.Serialization;
using System.Configuration;
using System.IO;

namespace DoAnWebSell.Controllers
{
    public class cartController : Controller
    {
        // GET: cart
        public ActionResult index()
        {
            var cart = Session[CommonConstants.CART_SEDSSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult addItem(long productID, int quantity)
        {
            var product = new ProductDao().ViewDetail(productID);
            var cart = Session[CommonConstants.CART_SEDSSION];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.MaSP == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.MaSP == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }               
                else
                {
                    // Tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                // Gán vào session
                Session[CommonConstants.CART_SEDSSION] = list;
            }
            else
            {
                // Tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);

                // Gán vào session
                Session[CommonConstants.CART_SEDSSION] = list;
            }
            return RedirectToAction("index");
        }

        public JsonResult deleteAll()
        {
            Session[CommonConstants.CART_SEDSSION] = null;

            return Json(new
            {
                status = true
            });
        }

        public JsonResult delete(long id)
        {
            // Lấy ra danh sách giỏ hàng
            var sessionCart = (List<CartItem>)Session[CommonConstants.CART_SEDSSION];
            sessionCart.RemoveAll(x => x.Product.MaSP == id);
            Session[CommonConstants.CART_SEDSSION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CART_SEDSSION];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.MaSP == item.Product.MaSP);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }

            }
            Session[CommonConstants.CART_SEDSSION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult payment()
        {
            var cart = Session[CommonConstants.CART_SEDSSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult payment(string shipName, string mobile, string address, string email)
        {
            var order = new DonDatHang();
            order.NgayDat = DateTime.Now;
            order.DiaChiGiaoHang = address;
            order.SoDienThoai = mobile;
            order.TenNguoiDat = shipName;
            order.Email = email;
            order.TrangThai = 1;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CommonConstants.CART_SEDSSION];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new ChiTietDatHang();
                    orderDetail.MaDon = id;
                    orderDetail.MaSP = item.Product.MaSP;
                    
                    if (item.Product.GiaKhuyenMai != null)
                    {
                        orderDetail.DonGia = item.Product.GiaKhuyenMai;
                    }
                    else
                    {
                        orderDetail.DonGia = item.Product.DonGia;
                    }

                    orderDetail.SoLuong = item.Quantity;
                    detailDao.Insert(orderDetail);

                    //total += (item.Product.DonGia.GetValueOrDefault(0) * item.Quantity);
                }
                Session[CommonConstants.CART_SEDSSION] = null;
                //string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                //content = content.Replace("{{CustomerName}}", shipName);
                //content = content.Replace("{{Phone}}", mobile);
                //content = content.Replace("{{Email}}", email);
                //content = content.Replace("{{Address}}", address);
                //content = content.Replace("{{Total}}", total.ToString("N0"));
                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(email, "Đơn hàng mới từ Shop", content);
                //new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Shop", content);
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult success()
        {
            return View();
        }
    }
}