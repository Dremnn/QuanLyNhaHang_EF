using System.Collections.Generic;
using System.Linq;
using QuanLyNhaHang_EF.Model;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class DanhMucBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public List<DanhMuc> getAll()
        {
            return db.DanhMucs.ToList();
        }

        public bool insert(DanhMuc dm)
        {
            try { db.DanhMucs.Add(dm); db.SaveChanges(); return true; } catch { return false; }
        }

        public bool update(DanhMuc dm)
        {
            try
            {
                var target = db.DanhMucs.Find(dm.Id);
                if (target != null) { target.TenDanhMuc = dm.TenDanhMuc; db.SaveChanges(); return true; }
                return false;
            }
            catch { return false; }
        }

        public bool delete(int id)
        {
            try { var target = db.DanhMucs.Find(id); if (target != null) { db.DanhMucs.Remove(target); db.SaveChanges(); return true; } return false; } catch { return false; }
        }
    }
}