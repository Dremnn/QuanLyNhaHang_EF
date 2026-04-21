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
            db = new QuanLyNhaHangEntities();
            List<DonHang> ketQua = new List<DonHang>();
            foreach (DonHang dh in db.DonHangs.Include("Ban"))
            {
                if (dh.KhachHangId == khachHangId)
                    ketQua.Add(dh);
            }
            return ketQua;
        }

        public List<DonHang> getAllDangMo()
        {
            db = new QuanLyNhaHangEntities();
            List<DonHang> ketQua = new List<DonHang>();
            foreach (DonHang dh in db.DonHangs.Include("Ban").Include("KhachHang"))
            {
                if (dh.TrangThai != TrangThaiDonHang.DaThanhToan.ToString() &&
                    dh.TrangThai != TrangThaiDonHang.Huy.ToString())
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

            if (!banConTrong) return false;

            try
            {
                DonHang donHang = new DonHang();
                donHang.BanId = banId;
                donHang.KhachHangId = khachHangId;
                donHang.NguoiDungId = null;
                donHang.GhiChu = null;
                donHang.TrangThai = TrangThaiDonHang.ChoDuyet.ToString();

                donHang.NgayTao = DateTime.Now;

                db.DonHangs.Add(donHang);
                db.SaveChanges();

                banBLL.updateTrangThai(banId, TrangThaiBan.CoKhach.ToString());
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
                    target.TrangThai = TrangThaiDonHang.Huy.ToString();
                    db.SaveChanges();

                    banBLL.updateTrangThai(banId, TrangThaiBan.Trong.ToString());
                    return true;
                }
                return false;
            }
            catch { return false; }
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
            catch { return false; }
        }
    }
}