using Quanlytrasua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;
        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return NhanVienDAO.instance; }
            private set { instance = value; }
        }
        private NhanVienDAO() { }
        public List<NhanVien> GetListNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();

            string query = "SELECT * FROM dbo.NhanVien";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NhanVien nhanvien = new NhanVien(item);
                list.Add(nhanvien);
            }

            return list;
        }

        public bool InsertNhanVien(string name,DateTime ngaysinh,string gioitinh,string diachi,string sdt)
        {
            string  query = string.Format("INSERT dbo.NhanVien (name,NgaySinh,GioiTinh,DiaChi,SDT)VALUES(N'{0}','{1}',N'{2}',N'{3}','{4}')", name, ngaysinh,gioitinh,diachi,sdt);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateNhanVien(int id,string name, DateTime ngaysinh, string gioitinh, string diachi, string sdt)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("UPDATE dbo.NhanVien SET name = N'"+name+"',NgaySinh='"+ngaysinh+"',GioiTinh=N'"+gioitinh+"',DiaChi=N'"+diachi+"',SDT = '"+sdt+"' WHERE id = '"+id+"'");
            return result > 0;
        }
        public bool deleteNhanVien(int id)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("DELETE FROM dbo.NhanVien WHERE id = '" + id + "'");
            return result > 0;
        }
        public List<NhanVien> SearchNVtByName(string name)
        {
            List<NhanVien> listNhanVien = new List<NhanVien>();
            string query = string.Format("SELECT * FROM dbo.NhanVien WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NhanVien account = new NhanVien(item);
                listNhanVien.Add(account);
            }

            return listNhanVien;
        }
    }
}
