using System.Collections.Generic;
using QuanLyNhaHang_EF.Model;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class BanBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public List<Ban> getBanTrong()
        {
            List<Ban> ketQua = new List<Ban>();
            foreach (Ban ban in db.Bans)
            {
                if (ban.TrangThai == "Trống" || ban.TrangThai == "Trong")
                {
                    ketQua.Add(ban);
                }
            }
            return ketQua;
        }

        public List<Ban> getAll()
        {
            List<Ban> list = new List<Ban>();
            foreach (Ban ban in db.Bans)
            {
                list.Add(ban);
            }
            return list;
        }

        public bool updateTrangThai(int banId, string trangThai)
        {
            try
            {
                Ban target = db.Bans.Find(banId);
                if (target != null)
                {
                    target.TrangThai = trangThai;
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

        public bool insert(Ban ban)
        {
            if (string.IsNullOrEmpty(ban.SoBan))
                return false;

            if (ban.SoCho <= 0)
                return false;

            try
            {
                db.Bans.Add(ban);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool update(Ban ban)
        {
            if (string.IsNullOrEmpty(ban.SoBan))
                return false;

            if (ban.SoCho <= 0)
                return false;

            try
            {
                Ban target = db.Bans.Find(ban.Id);
                if (target != null)
                {
                    target.SoBan = ban.SoBan;
                    target.SoCho = ban.SoCho;
                    target.TrangThai = ban.TrangThai;

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
                Ban target = db.Bans.Find(id);
                if (target != null)
                {
                    db.Bans.Remove(target);
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