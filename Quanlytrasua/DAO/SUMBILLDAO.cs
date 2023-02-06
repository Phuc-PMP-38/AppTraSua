using Quanlytrasua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DAO
{
    public class SUMBILLDAO
    {
        private static SUMBILLDAO instance;

        public static SUMBILLDAO Instance
        {
            get { if (instance == null) instance = new SUMBILLDAO(); return SUMBILLDAO.instance; }
            private set { SUMBILLDAO.instance = value; }
        }
        private SUMBILLDAO() { }
        public List <Thongke> GetListSUMBILL()
        {
            List<Thongke> list = new List<Thongke>();

            string query = "SELECT * FROM thongke";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Thongke sum = new Thongke(item);
                list.Add(sum);
            }

            return list;
        }
        public double GetNumBillListBySum()
        {
            return (double)DataProvider.Instance.ExecuteScalar("SELECT SUM(Total) FROM thongke");
        }
    }
}
