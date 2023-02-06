using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public class NhanVien
    {
        private int iD;
        private string name;
        private DateTime? ngaySinh;
        private string gioiTinh;
        private string diaChi;
        private string sdt;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public DateTime? NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string Sdt { get => sdt; set => sdt = value; }

        public NhanVien(int id, string name, DateTime? ngaysinh, string diachi, string gioitinh, string sdt )
        {
            this.ID = id;
            this.Name = name;
            this.NgaySinh = ngaysinh;
            this.GioiTinh = gioiTinh;
            this.DiaChi = diachi;
            this.Sdt = sdt;
        }
        public NhanVien(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.NgaySinh =  (DateTime?)row["NgaySinh"];
            this.GioiTinh = row["GioiTinh"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.Sdt =row["SDT"].ToString();
        }
    }
}
