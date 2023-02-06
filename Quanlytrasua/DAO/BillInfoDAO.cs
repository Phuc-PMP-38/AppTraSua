using Quanlytrasua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DAO
{
    public  class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set { instance = value; }
        }
        private BillInfoDAO() { }
        public List<Billinfo> GetBillInfoList(int id)
        {
            List<Billinfo> ListBillinfo = new List<Billinfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE idBill ="+id+"");

            foreach (DataRow item in data.Rows)
            {
                Billinfo billinfo = new Billinfo(item);
                ListBillinfo.Add(billinfo);
            }
            return ListBillinfo;
        }
        public void InsertBillInfo(int idFood, int idBill, int count,int idCategory)
        { 
            DataProvider.Instance.ExecuteNonQuery("EXEC dbo.USP_insertBillInfo @idBill , @idFood , @count , @idCategory", new object[] { idBill, idFood, count,idCategory});
        }
        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.BillInfo where idFood=" + id + "");
        }
    }
}
