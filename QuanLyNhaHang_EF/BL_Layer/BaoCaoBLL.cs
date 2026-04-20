using System;
using System.Collections.Generic;
using QuanLyNhaHang_EF.Model;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class BaoCaoBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public decimal getDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            decimal tong = 0;
            foreach (ThanhToan tt in db.ThanhToans)
            {
                if (tt.NgayThanhToan >= tuNgay && tt.NgayThanhToan <= denNgay)
                {
                    tong += tt.TienThanhToan;
                }
            }
            return tong;
        }

        public List<ThanhToan> getBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            List<ThanhToan> list = new List<ThanhToan>();
            foreach (ThanhToan tt in db.ThanhToans)
            {
                if (tt.NgayThanhToan >= tuNgay && tt.NgayThanhToan <= denNgay)
                {
                    list.Add(tt);
                }
            }
            return list;
        }
    }
}