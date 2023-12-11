using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySach.Models
{
    public class thongTinKH
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }

        public thongTinKH() { }

        public thongTinKH(int maKH, string hoTen, string email, string diaChi, string soDienThoai)
        {
            MaKH = maKH;
            HoTen = hoTen;
            Email = email;
            DiaChi = diaChi;
            SoDienThoai = soDienThoai;
        }
    }
}