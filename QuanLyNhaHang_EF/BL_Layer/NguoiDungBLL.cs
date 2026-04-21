using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyNhaHang_EF.Model;
using QuanLyNhaHang_EF.Helpers;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class NguoiDungBLL
    {

        public NguoiDung login(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
                return null;

            using (var db = new QuanLyNhaHangEntities())
            {
                NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(nd => nd.TenDangNhap == tenDangNhap);

                if (nguoiDung == null || nguoiDung.HoatDong != true)
                    return null;

                if (!PasswordHelper.verifyPassword(matKhau, nguoiDung.MatKhau))
                    return null;

                return nguoiDung;
            }
        }

        public bool dangKy(string tenDangNhap, string matKhau, string hoTen, string soDienThoai)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau) ||
                string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(soDienThoai))
                return false;

            using (var db = new QuanLyNhaHangEntities())
            {
                if (db.NguoiDungs.Any(nd => nd.TenDangNhap == tenDangNhap)) return false;
                if (db.KhachHangs.Any(kh => kh.SoDienThoai == soDienThoai)) return false;

                try
                {
                    NguoiDung nguoiDung = new NguoiDung();
                    nguoiDung.TenDangNhap = tenDangNhap;
                    nguoiDung.MatKhau = PasswordHelper.hashPassword(matKhau);
                    nguoiDung.VaiTro = VaiTro.KhachHang.ToString();
                    nguoiDung.HoatDong = true;
                    nguoiDung.NgayTao = DateTime.Now; 

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
        }

        public List<NguoiDung> getAll()
        {
            using (var db = new QuanLyNhaHangEntities())
            {
                return db.NguoiDungs.OrderBy(nd => nd.VaiTro).ThenBy(nd => nd.TenDangNhap).ToList();
            }
        }

        public bool updateHoatDong(int id, bool hoatDong)
        {
            using (var db = new QuanLyNhaHangEntities())
            {
                var target = db.NguoiDungs.Find(id);
                if (target != null)
                {
                    target.HoatDong = hoatDong;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool updateVaiTro(int id, string vaiTro)
        {
            if (id == SessionHelper.CurrentUser.Id)
                return false;

            using (var db = new QuanLyNhaHangEntities())
            {
                var target = db.NguoiDungs.Find(id);
                if (target != null)
                {
                    target.VaiTro = vaiTro;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool delete(int id)
        {
            if (id == SessionHelper.CurrentUser.Id)
                return false;

            using (var db = new QuanLyNhaHangEntities())
            {
                try
                {
                    var target = db.NguoiDungs.Find(id);
                    if (target != null)
                    {
                        var kh = db.KhachHangs.FirstOrDefault(k => k.NguoiDungId == id);
                        if (kh != null)
                        {
                            db.KhachHangs.Remove(kh);
                        }

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

        public bool xoaTaiKhoan(int id)
        {
            return delete(id);
        }
    }
}