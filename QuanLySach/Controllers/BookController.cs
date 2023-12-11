using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuanLySach.Controllers
{
 
    public class BookController : ApiController
    {
        QLBSDataContext db = new QLBSDataContext();
        [HttpGet]
        public List<ProduceBook> GetSachList()
        {
            return db.Saches.ToList();
        }
        [HttpGet]
        public ProduceBook GetSach(int id)
        {
            return db.Saches.FirstOrDefault(s => s.MaSach == id);
        }
        [HttpPost]
        public int insertSach(string tenS, string moTa, string anhBia, int SLTon, int giaBan)
        {
            try
            {
                ProduceBook s = new ProduceBook();
                s.TenSach = tenS;
                s.MoTa = moTa;
                s.AnhBia = anhBia;
                s.SoLuongTon = SLTon;
                s.GiaBan = giaBan;
                db.Saches.InsertOnSubmit(s);
                db.SubmitChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        [HttpPut]
        public bool UpdateSach(int MaS, string tenS, string moTa, string anhBia, int SLTon, int giaBan)
        {
            try
            {
                ProduceBook sach = db.Saches.FirstOrDefault(s => s.MaSach == MaS);
                if(sach == null)
                {
                    return false;
                }
                sach.TenSach = tenS;
                sach.MoTa = moTa;
                sach.AnhBia= anhBia;
                sach.SoLuongTon= SLTon;
                sach.GiaBan = giaBan;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteSach(int Ma)
        {
            ProduceBook sach = db.Saches.FirstOrDefault(s => s.MaSach == Ma);
            if (sach == null)
            {
                return false;
            }
            db.Saches.DeleteOnSubmit(sach);
            db.SubmitChanges();
            return true;
        }
    }
}
