namespace QuanLyNhaHang_EF.Model
{
    public partial class DonHang
    {
        public string SoBan => this.Ban?.SoBan;
        public string TenKhachHang => this.KhachHang?.HoTen;
    }
}