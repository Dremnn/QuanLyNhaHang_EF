using QuanLyNhaHang_EF.Model;
using System;

namespace QuanLyNhaHang_EF.Model 
{
    public partial class MonAn : IMonAnComponent
    {
        public string TenDanhMuc => this.DanhMuc?.TenDanhMuc;

        public string LaysTen()
        {
            return this.TenMon;
        }

        public decimal TinhGia()
        {

            return this.GiaBan;
        }
    }
}