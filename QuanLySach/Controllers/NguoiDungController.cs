using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySach.Controllers
{
    public class NguoiDungController : Controller
    {
        QLBSDataContext db = new QLBSDataContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang kh, FormCollection f)
        {
            var hoTen = f["HoTenKH"];
            
            var matKhau = f["MatKhau"];
            var reMatKhau = f["ReMatKhau"];
            if (String.IsNullOrEmpty(hoTen))
            {
                ViewBag.Loi1 = "Họ Tên Không Được Bỏ Trống";
            }

            if (String.IsNullOrEmpty(matKhau))
            {
                ViewBag.Loi3 = "Vui Lòng Nhập Mật Khẩu";
            }

            if (matKhau != reMatKhau)
            {
                ViewBag.Loi6 = "Vui Lòng Nhập Lại Mật Khẩu";
            }
            kh.HoTen = "Zohan";
            kh.TaiKhoan = hoTen;
            kh.MatKhau = matKhau;
            
           
            
            db.KhachHangs.InsertOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("DangNhap", "NguoiDung");
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhapDN(string taiKhoan, string MatKhau)
        {
           
            if (ModelState.IsValid)
            {
         
                var data = db.KhachHangs.Where(s => s.TaiKhoan.Equals(taiKhoan) && s.MatKhau.Equals(MatKhau)).ToList();
                
                if (taiKhoan == "zohan" && MatKhau == "123")
                {
                    Session["admin"] = data.FirstOrDefault().HoTen;
                    Session["HoTenKH"] = data.FirstOrDefault().HoTen;
                    Session["id"] = data.FirstOrDefault().MaKH;
                    return RedirectToAction("Index", "admin");
                }
                else
                {
                    if (data.Count > 0)
                    {
                        Session["HoTenKH"] = data.FirstOrDefault().HoTen;
                        Session["id"] = data.FirstOrDefault().MaKH;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.error = "Dăng Nhập Thất Bại";
                        return RedirectToAction("DangNhap", "NguoiDung");
                    }
                }

            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

    }
}