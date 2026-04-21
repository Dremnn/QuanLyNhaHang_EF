using System.Collections.Generic;
using QuanLyNhaHang_EF.Model;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class ChiTietDonBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public List<ChiTietDon> getByDonHangId(int donHangId)
        {
            db = new QuanLyNhaHangEntities();  
            List<ChiTietDon> ketQua = new List<ChiTietDon>();
            foreach (ChiTietDon ct in db.ChiTietDons)
            {
                if (ct.DonHangId == donHangId)
                    ketQua.Add(ct);
            }
            return ketQua;
        }

        public bool themMon(int donHangId, int monAnId, int soLuong, string ghiChu)
        {
            if (soLuong <= 0) return false;

            MonAn monGoc = db.MonAns.Find(monAnId);

            if (monGoc == null || monGoc.ConHang != true) return false;

            decimal giaChot = monGoc.GiaBan;
            string ghiChuMoi = ghiChu;

            if (monGoc.DanhMucId == 8)
            {
                MonAnBLL bll = new MonAnBLL();

                giaChot = bll.TinhGiaThucTe(monGoc);

                if (monGoc.Id == 28)
                    ghiChuMoi += " (Cơm Sườn, Canh Chua)";
                else if (monGoc.Id == 29)
                    ghiChuMoi += " (Cơm Sườn Bì Chả, Canh Chua, Sting)";
                else if (monGoc.Id == 30)
                    ghiChuMoi += " (Cơm Sườn Bì Chả, Tốp Mỡ, Trứng Ốp La, Canh Chua, Sting)";
            }

            try
            {
                ChiTietDon chiTiet = new ChiTietDon();
                chiTiet.DonHangId = donHangId;
                chiTiet.MonAnId = monAnId;
                chiTiet.SoLuong = (byte)soLuong;
                chiTiet.DonGia = giaChot;
                chiTiet.GhiChu = ghiChuMoi;

                db.ChiTietDons.Add(chiTiet);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool xoaMon(int chiTietId)
        {
            db = new QuanLyNhaHangEntities();
            try
            {
                ChiTietDon target = db.ChiTietDons.Find(chiTietId);
                if (target != null)
                {
                    db.ChiTietDons.Remove(target);
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

        public bool capNhatSoLuong(int chiTietId, int soLuong)
        {
            if (soLuong <= 0) return false;

            try
            {
                ChiTietDon target = db.ChiTietDons.Find(chiTietId);
                if (target != null)
                {
                    target.SoLuong = (byte)soLuong;
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

        public decimal tinhTongTien(int donHangId)
        {
            db = new QuanLyNhaHangEntities();
            decimal tong = 0;
            foreach (ChiTietDon ct in db.ChiTietDons)
            {
                if (ct.DonHangId == donHangId)
                {
                    tong += ct.DonGia * ct.SoLuong;
                }
            }
            return tong;
        }
    }
}