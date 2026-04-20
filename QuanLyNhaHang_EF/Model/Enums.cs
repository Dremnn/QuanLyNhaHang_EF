namespace QuanLyNhaHang_EF.Model
{
    // 1. Dữ liệu từ cột VaiTro
    public enum VaiTro
    {
        Admin,
        KhachHang,
        NhanVien
    }

    // 2. Dữ liệu từ cột TrangThai của bảng Ban
    public enum TrangThaiBan
    {
        CoKhach,
        Trong
    }

    // 3. Dữ liệu từ cột TrangThai của bảng DonHang
    public enum TrangThaiDonHang
    {
        ChoDuyet,
        DaThanhToan,
        Huy
    }

    // 4. Dữ liệu từ cột PhuongThuc của bảng ThanhToan
    public enum PhuongThucThanhToan
    {
        TienMat
    }

    // 5. Dữ liệu từ cột TrangThai của bảng ThanhToan
    public enum TrangThaiThanhToan
    {
        ThanhCong
    }
}