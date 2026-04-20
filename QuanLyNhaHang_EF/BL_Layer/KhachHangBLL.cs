using System;
using System.Collections.Generic;
using QuanLyNhaHang_EF.Model;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class KhachHangBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public KhachHang getByNguoiDungId(int nguoiDungId)
        {
            foreach (KhachHang kh in db.KhachHangs)
            {
                if (kh.NguoiDungId == nguoiDungId)
                {
                    return kh;
                }
            }
            return null;
        }

        public bool update(KhachHang khachHang)
        {
            if (string.IsNullOrEmpty(khachHang.HoTen) || string.IsNullOrEmpty(khachHang.SoDienThoai))
                return false;

            try
            {
                KhachHang target = db.KhachHangs.Find(khachHang.Id);
                if (target != null)
                {
                    target.HoTen = khachHang.HoTen;
                    target.SoDienThoai = khachHang.SoDienThoai;
                    target.DiaChi = khachHang.DiaChi;

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

        public KhachHang getBySoDienThoai(string soDienThoai)
        {
            foreach (KhachHang kh in db.KhachHangs)
            {
                if (kh.SoDienThoai == soDienThoai)
                {
                    return kh;
                }
            }
            return null;
        }

        public int insertVangLai(KhachHang khachHang)
        {
            if (string.IsNullOrEmpty(khachHang.HoTen) || string.IsNullOrEmpty(khachHang.SoDienThoai))
                return -1;

            try
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();

                return khachHang.Id;
            }
            catch
            {
                return -1;
            }
        }
    }
}