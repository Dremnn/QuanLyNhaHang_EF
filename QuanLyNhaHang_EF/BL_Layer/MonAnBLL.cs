using QuanLyNhaHang.Model;
using QuanLyNhaHang_EF.Model;
using System;
using System.Collections.Generic;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class MonAnBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public List<MonAn> getAll()
        {
            List<MonAn> list = new List<MonAn>();
            foreach (MonAn mon in db.MonAns)
            {
                list.Add(mon);
            }
            return list;
        }

        public List<MonAn> getByDanhMuc(int danhMucId)
        {
            List<MonAn> ketQua = new List<MonAn>();
            foreach (MonAn mon in db.MonAns)
            {
                if (mon.DanhMucId == danhMucId)
                {
                    ketQua.Add(mon);
                }
            }
            return ketQua;
        }

        public bool insert(MonAn monAn)
        {
            if (string.IsNullOrEmpty(monAn.TenMon))
                return false;

            if (monAn.DanhMucId != 8 && monAn.GiaBan <= 0)
                return false;

            try
            {
                db.MonAns.Add(monAn);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool update(MonAn monAn)
        {
            if (string.IsNullOrEmpty(monAn.TenMon))
                return false;

            if (monAn.DanhMucId != 8 && monAn.GiaBan <= 0)
                return false;

            try
            {
                var target = db.MonAns.Find(monAn.Id);

                if (target != null)
                {
                    target.TenMon = monAn.TenMon;
                    target.GiaBan = monAn.GiaBan;
                    target.DanhMucId = monAn.DanhMucId;
                    target.MoTa = monAn.MoTa;
                    target.ConHang = monAn.ConHang;
                    target.AnhUrl = monAn.AnhUrl;

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
            try
            {
                var target = db.MonAns.Find(id);

                if (target != null)
                {
                    db.MonAns.Remove(target);
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

        public bool updateConHang(int id, bool conHang)
        {
            try
            {
                var target = db.MonAns.Find(id);

                if (target != null)
                {
                    target.ConHang = conHang;
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

        private MonAn TimMonTheoId(List<MonAn> danhSach, int id)
        {
            foreach (MonAn mon in danhSach)
            {
                if (mon.Id == id)
                {
                    return mon;
                }
            }
            return null; // Trả về null nếu không tìm thấy
        }

        public decimal TinhGiaThucTe(MonAn mon)
        {
            if (mon.DanhMucId != 8)
                return mon.GiaBan;

            List<MonAn> all = getAll(); // Lấy tất cả danh sách bằng hàm getAll() ở trên
            ComboMonAn comboPattern = null;

            if (mon.Id == 28)
            {
                comboPattern = new ComboMonAn("Combo Sinh Viên", 0.1m);
                comboPattern.ThemMon(TimMonTheoId(all, 1));
                comboPattern.ThemMon(TimMonTheoId(all, 12));
            }
            else if (mon.Id == 29)
            {
                comboPattern = new ComboMonAn("Combo BestSeller", 0.1m);
                comboPattern.ThemMon(TimMonTheoId(all, 4));
                comboPattern.ThemMon(TimMonTheoId(all, 12));
                comboPattern.ThemMon(TimMonTheoId(all, 22));
            }
            else if (mon.Id == 30)
            {
                comboPattern = new ComboMonAn("Combo FullToping", 0.15m);
                comboPattern.ThemMon(TimMonTheoId(all, 4));
                comboPattern.ThemMon(TimMonTheoId(all, 19));
                comboPattern.ThemMon(TimMonTheoId(all, 18));
                comboPattern.ThemMon(TimMonTheoId(all, 12));
                comboPattern.ThemMon(TimMonTheoId(all, 22));
            }

            if (comboPattern != null)
                return comboPattern.TinhGia();

            return mon.GiaBan;
        }
    }
}