using System;
using System.Collections.Generic;
using QuanLyNhaHang_EF.Model;

namespace QuanLyNhaHang_EF.BL_layer
{
    public class BaoCaoBLL
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public decimal getDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            decimal tong = 0;
            foreach (ThanhToan tt in db.ThanhToans)
            {
                if (tt.NgayThanhToan >= tuNgay && tt.NgayThanhToan <= denNgay)
                {
                    tong += tt.TienThanhToan;
                }
            }
            return tong;
        }

        public List<ThanhToan> getBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            db = new QuanLyNhaHangEntities();
            List<ThanhToan> list = new List<ThanhToan>();
            foreach (ThanhToan tt in db.ThanhToans)
            {
                if (tt.NgayThanhToan >= tuNgay && tt.NgayThanhToan <= denNgay)
                {
                    list.Add(tt);
                }
            }
            return list;
        }

        // Món bán chạy 
        public List<MonAnBanChay> getMonBanChay(DateTime tuNgay, DateTime denNgay)
        {
            db = new QuanLyNhaHangEntities();
            Dictionary<int, int> dictSoLuong = new Dictionary<int, int>();
            Dictionary<int, decimal> dictTongTien = new Dictionary<int, decimal>();

            foreach (ThanhToan tt in db.ThanhToans)
            {
                if (tt.NgayThanhToan >= tuNgay && tt.NgayThanhToan <= denNgay)
                {
                    foreach (ChiTietDon ct in db.ChiTietDons)
                    {
                        if (ct.DonHangId == tt.DonHangId)
                        {
                            int idMon = ct.MonAnId;
                            if (idMon == 0) continue;

                            if (!dictSoLuong.ContainsKey(idMon))
                            {
                                dictSoLuong[idMon] = 0;
                                dictTongTien[idMon] = 0;
                            }
                            dictSoLuong[idMon] += (int)(ct.SoLuong);
                            dictTongTien[idMon] += ((ct.SoLuong) * (ct.DonGia));
                        }
                    }
                }
            }

            List<MonAnBanChay> result = new List<MonAnBanChay>();
            foreach (int monAnId in dictSoLuong.Keys)
            {
                MonAn mon = db.MonAns.Find(monAnId);
                if (mon != null)
                {
                    MonAnBanChay item = new MonAnBanChay();
                    item.MonAnId = monAnId;
                    item.TenMon = mon.TenMon;
                    item.TongSoLuong = dictSoLuong[monAnId];
                    item.TongDoanhThu = dictTongTien[monAnId];
                    result.Add(item);
                }
            }

            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = i + 1; j < result.Count; j++)
                {
                    if (result[i].TongSoLuong < result[j].TongSoLuong)
                    {
                        MonAnBanChay temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }

            return result;
        }
    }
}