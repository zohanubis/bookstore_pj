using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace QuanLySach.Controllers
{
    public class SachController : Controller
    {
        QLBSDataContext db = new QLBSDataContext();

        // GET: Sach
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SachPartial()
        {
            var listSach = db.Saches.OrderBy(s => s.TenSach).ToList();
            return View("SachPartial", listSach);
        }
        public ActionResult XemChiTiet(int ms)
        {
            ProduceBook sp = db.Saches.Single(s => s.MaSach == ms);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult SachTheoLoai(int MaChuDe)
        {
            var ListSanPham = db.Saches.Where(s => s.MaSach == MaChuDe).ToList();
            if (ListSanPham.Count == 0)
            {
                ViewBag.TB = "Không có sản phẩn nào thuộc loại này";
            }
            return View(ListSanPham);
        }
        public ActionResult insertSach()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult insertSach(HttpPostedFileBase Image, ProduceBook s)
        {

            if (Image != null)
            {
                string fileName = Image.FileName.ToString();
                string urlImage = Server.MapPath("~/Content/HinhAnhSP/" + fileName);
                Image.SaveAs(urlImage);
                s.AnhBia = fileName;
            }
            if (ModelState.IsValid)
            {
                db.Saches.InsertOnSubmit(s);
                db.SubmitChanges();
                return RedirectToAction("ListSach", "admin");
            }
            return View(s);
        }
        public ActionResult UpdateSach()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSach(int id, HttpPostedFileBase Image, ProduceBook s)
        {

            // Kiểm tra xem có sản phẩm nào có MaSach giống với id không
            ProduceBook produceBook = db.Saches.SingleOrDefault(b => b.MaSach == id);

            if (produceBook == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy sách với mã sách đã chọn.";
                return View(s);
            }

            if (Image != null)
            {
                string fileName = Image.FileName.ToString();
                string urlImage = Server.MapPath("~/Content/HinhAnhSP/" + fileName);
                Image.SaveAs(urlImage);
                produceBook.AnhBia = fileName;
            }

            if (produceBook.TenSach != s.TenSach)
                produceBook.TenSach = s.TenSach;

            if (produceBook.GiaBan != s.GiaBan)
                produceBook.GiaBan = s.GiaBan;

            if (produceBook.MoTa != s.MoTa)
                produceBook.MoTa = s.MoTa;

            if (produceBook.NgayCapNhat != s.NgayCapNhat)
                produceBook.NgayCapNhat = s.NgayCapNhat;

            if (produceBook.MaChuDe != s.MaChuDe)
                produceBook.MaChuDe = s.MaChuDe;
            if (produceBook.MaNXB != s.MaNXB)
                produceBook.MaNXB = s.MaNXB;
            db.SubmitChanges();
            return RedirectToAction("ListSach", "admin");
        }
        public ActionResult PhanTrang(int? page)
        {

            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in db.Saches
                         select l).OrderBy(x => x.MaSach);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 8;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(links.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DeleteSach(int id)
        {
            
                ProduceBook produceBook = db.Saches.SingleOrDefault(b => b.MaSach == id);

                if (produceBook == null)
                {
                    ViewBag.ErrorMessage = "Không tìm thấy sách với mã sách đã chọn.";
                    return View();
                }

                db.Saches.DeleteOnSubmit(produceBook);
                db.SubmitChanges();

                return RedirectToAction("ListSach", "admin");
                  
            
        }

    }
}