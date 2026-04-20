using QuanLyNhaHang_EF.Model;
using System;

namespace QuanLyNhaHang_EF.Helpers
{
    public static class SessionHelper
    {
        // Lưu user đang đăng nhập, null = chưa đăng nhập
        public static NguoiDung CurrentUser { get; private set; } = null;

        public static void login(NguoiDung nguoiDung)
        {
            CurrentUser = nguoiDung;
        }

        public static void logout()
        {
            CurrentUser = null;
        }

        public static bool isLoggedIn()
        {
            return CurrentUser != null;
        }

        public static bool isAdmin()
        {
            return CurrentUser?.VaiTro == VaiTro.Admin.ToString();
        }

        public static bool isNhanVien()
        {
            return CurrentUser?.VaiTro == VaiTro.NhanVien.ToString();
        }

        public static bool isKhachHang()
        {
            return CurrentUser?.VaiTro == VaiTro.KhachHang.ToString();
        }
    }
}