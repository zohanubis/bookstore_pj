﻿using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySach.Controllers
{
    public class GioHangController : Controller
    {
        QLBSDataContext db = new QLBSDataContext();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThanhToanHoaDon()
        {
            return View();
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstGioHang);
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int ms, string str)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanPham = lstGioHang.Find(sp => sp.MaSach == ms);
            if (sanPham == null)
            {
                sanPham = new GioHang(ms);
                lstGioHang.Add(sanPham);
                return Redirect(str);
            }
            else
            {
                sanPham.SoLuong++;
                return Redirect(str);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(sp => sp.SoLuong);
            }
            return tsl;
        }

        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                ttt = lstGioHang.Sum(sp => sp.ThanhTien);
            }
            return ttt;
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("SachPartial", "Sach");
            }
            List<GioHang> lst = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lst);
        }
        public ActionResult XoaGioHang(int ms)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(s => s.MaSach == ms);
            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.MaSach == ms);
                return RedirectToAction("GioHang", "GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang", "GioHang");
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DonHang donHang = new DonHang();
            taiKhoan kh = new taiKhoan(Convert.ToInt32(Session["id"]));
            List<GioHang> gh = LayGioHang();
            donHang.MaKH = kh.MaKH;
            donHang.NgayDat = DateTime.Now;
            var ngayGiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            donHang.NgayGiao = DateTime.Parse(ngayGiao);
            donHang.TinhTrangGiaoHang = 0;
            donHang.DaThanhToan = "Chua";
            db.DonHangs.InsertOnSubmit(donHang);
            db.SubmitChanges();
            foreach (var item in gh)
            {
                ChiTietDonHang CTDH = new ChiTietDonHang();
                CTDH.MaDonHang = donHang.MaDonHang;
                CTDH.MaSach = item.MaSach;
                CTDH.SoLuong = item.SoLuong;
                CTDH.DonGia = (decimal)item.DonGia;
                db.ChiTietDonHangs.InsertOnSubmit(CTDH);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            return View();
        }
        [HttpPost]
        public ActionResult ThanhToanND(string hoTen, string dienThoai, string email, string diaChi)
        {

            thongTinKH kh = new thongTinKH();
            kh.MaKH = Convert.ToInt32(Session["id"]);
            kh.HoTen = hoTen;
            kh.Email = email;
            kh.DiaChi = diaChi;
            kh.SoDienThoai = dienThoai;

            KhachHang KH = new KhachHang();
            KH.MaKH = kh.MaKH;
            KH.HoTen = kh.HoTen;
            KH.Email = kh.Email;
            KH.DiaChi = kh.DiaChi;
            KH.DienThoai = kh.SoDienThoai;

            db.KhachHangs.InsertOnSubmit(KH);
            db.SubmitChanges();
            return RedirectToAction("DatHang", "GioHang");
        }

        public ActionResult ThanhToan()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("PhanTrang", "Sach");
            }


            return View();
        }


    }
}