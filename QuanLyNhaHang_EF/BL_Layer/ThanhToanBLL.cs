using System;
using System.Collections.Generic;
using QuanLyNhaHang_EF.Model;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class ThanhToanBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        // Giữ nguyên tham số truyền vào như code cũ của bạn
        public bool thanhToan(int donHangId, decimal tienGiam, object phuongThuc, string ghiChu)
        {
            foreach (ThanhToan tt in db.ThanhToans)
            {
                if (tt.DonHangId == donHangId)
                {
                    return false;
                }
            }

            decimal tongTien = 0;
            foreach (ChiTietDon ct in db.ChiTietDons)
            {
                if (ct.DonHangId == donHangId)
                {
                    tongTien += ct.DonGia * ct.SoLuong;
                }
            }

            if (tienGiam < 0 || tienGiam > tongTien)
                return false;

            try
            {
                ThanhToan thanhToan = new ThanhToan();
                thanhToan.DonHangId = donHangId;
                thanhToan.TongTien = tongTien;
                thanhToan.TienGiam = tienGiam;
                thanhToan.TienThanhToan = tongTien - tienGiam;
                thanhToan.NgayThanhToan = DateTime.Now;

                // Ép ra chuỗi để tránh lỗi lệch kiểu Enum của Entity Framework
                if (phuongThuc != null) thanhToan.PhuongThuc = phuongThuc.ToString();

                thanhToan.GhiChu = ghiChu;
                thanhToan.TrangThai = "ThanhCong"; // Gán cứng chuỗi trạng thái

                db.ThanhToans.Add(thanhToan);

                DonHang dh = db.DonHangs.Find(donHangId);
                if (dh != null)
                {
                    dh.TrangThai = "DaThanhToan";

                    int banId = Convert.ToInt32(dh.BanId);
                    Ban ban = db.Bans.Find(banId);
                    if (ban != null)
                    {
                        ban.TrangThai = "Trong";
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ThanhToan getByDonHangId(int donHangId)
        {
            foreach (ThanhToan tt in db.ThanhToans)
            {
                if (tt.DonHangId == donHangId)
                {
                    return tt;
                }
            }
            return null;
        }
    }
}