using System;
using System.Collections.Generic;
using QuanLyNhaHang_EF.Model;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class DonHangBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        private BanBLL banBLL = new BanBLL();

        public List<DonHang> getByKhachHangId(int khachHangId)
        {
            List<DonHang> ketQua = new List<DonHang>();
            foreach (DonHang dh in db.DonHangs)
            {
                if (dh.KhachHangId == khachHangId)
                {
                    ketQua.Add(dh);
                }
            }
            return ketQua;
        }

        public List<DonHang> getAllDangMo()
        {
            List<DonHang> ketQua = new List<DonHang>();
            foreach (DonHang dh in db.DonHangs)
            {
                if (dh.TrangThai != "Đã thanh toán" && dh.TrangThai != "Hủy" && dh.TrangThai != "Đã hủy")
                {
                    ketQua.Add(dh);
                }
            }
            return ketQua;
        }

        public bool datBan(int banId, int? khachHangId)
        {
            List<Ban> danhSachTrong = banBLL.getBanTrong();
            bool banConTrong = false;

            foreach (Ban b in danhSachTrong)
            {
                if (b.Id == banId)
                {
                    banConTrong = true;
                    break;
                }
            }

            if (!banConTrong)
                return false;

            try
            {
                DonHang donHang = new DonHang();
                donHang.BanId = banId;
                donHang.KhachHangId = khachHangId;
                donHang.NguoiDungId = null;
                donHang.GhiChu = null;
                donHang.TrangThai = "Mở";

                db.DonHangs.Add(donHang);
                db.SaveChanges();

                banBLL.updateTrangThai(banId, "CoKhach");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool huyDatBan(int donHangId)
        {
            try
            {
                DonHang target = db.DonHangs.Find(donHangId);
                if (target != null)
                {
                    int banId = Convert.ToInt32(target.BanId);
                    target.TrangThai = "Hủy";
                    db.SaveChanges();

                    banBLL.updateTrangThai(banId, "Trong");
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool updateTrangThai(int donHangId, string trangThai)
        {
            try
            {
                DonHang target = db.DonHangs.Find(donHangId);
                if (target != null)
                {
                    target.TrangThai = trangThai;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}