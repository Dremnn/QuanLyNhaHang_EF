using System;
using System.Collections.Generic;
using QuanLyNhaHang_EF.Model;
using QuanLyNhaHang_EF.Helpers;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class NguoiDungBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public NguoiDung login(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
                return null;

            NguoiDung nguoiDung = null;
            foreach (NguoiDung nd in db.NguoiDungs)
            {
                if (nd.TenDangNhap == tenDangNhap)
                {
                    nguoiDung = nd;
                    break;
                }
            }

            if (nguoiDung == null || nguoiDung.HoatDong != true)
                return null;

            if (!PasswordHelper.verifyPassword(matKhau, nguoiDung.MatKhau))
                return null;

            return nguoiDung;
        }

        public bool dangKy(string tenDangNhap, string matKhau, string hoTen, string soDienThoai)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau) ||
                string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(soDienThoai))
                return false;

            foreach (NguoiDung nd in db.NguoiDungs)
            {
                if (nd.TenDangNhap == tenDangNhap) return false;
            }

            foreach (KhachHang kh in db.KhachHangs)
            {
                if (kh.SoDienThoai == soDienThoai) return false;
            }

            try
            {
                NguoiDung nguoiDung = new NguoiDung();
                nguoiDung.TenDangNhap = tenDangNhap;
                nguoiDung.MatKhau = PasswordHelper.hashPassword(matKhau);
                nguoiDung.VaiTro = "KhachHang";
                nguoiDung.HoatDong = true;

                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();

                KhachHang khachHang = new KhachHang();
                khachHang.HoTen = hoTen;
                khachHang.SoDienThoai = soDienThoai;
                khachHang.NguoiDungId = nguoiDung.Id;

                db.KhachHangs.Add(khachHang);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<NguoiDung> getAll()
        {
            List<NguoiDung> list = new List<NguoiDung>();
            foreach (NguoiDung nd in db.NguoiDungs)
            {
                list.Add(nd);
            }
            return list;
        }

        public bool updateHoatDong(int id, bool hoatDong)
        {
            try
            {
                NguoiDung target = db.NguoiDungs.Find(id);
                if (target != null)
                {
                    target.HoatDong = hoatDong;
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

        public bool updateVaiTro(int id, string vaiTro)
        {
            if (id == SessionHelper.CurrentUser.Id)
                return false;

            try
            {
                NguoiDung target = db.NguoiDungs.Find(id);
                if (target != null)
                {
                    target.VaiTro = vaiTro;
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

        public bool delete(int id)
        {
            if (id == SessionHelper.CurrentUser.Id)
                return false;

            try
            {
                NguoiDung target = db.NguoiDungs.Find(id);
                if (target != null)
                {
                    db.NguoiDungs.Remove(target);
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