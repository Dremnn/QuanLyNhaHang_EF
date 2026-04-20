namespace QuanLyNhaHang_EF.Model
{
    public partial class ChiTietDon
    {
        public string TenMon => this.MonAn?.TenMon;
        public decimal ThanhTien => (this.DonGia) * (this.SoLuong);
    }
}