using Quanlytrasua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Quanlytrasua.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO();return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }
        private TableDAO() { }
        public List<Table> LoadTableList()
        {
            List<Table> TableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC dbo.USP_getTableList");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                TableList.Add(table);
            }
            return TableList;
        }
        public static int Tablewidth = 80;
        public static int Tableheight = 80;
        public void SwithTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("EXEC dbo.USP_SwichTable @idTable1 , @idTable2", new object[] {id1,id2});
        }
        public Table GetTableByID(int id)
        {
            Table table = null;

            string query = "select * from TableFood where id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                table = new Table(item);
                return table;
            }

            return table;
        }
        public bool InsertTable(string name)
        {
            string query = string.Format("INSERT dbo.TableFood ( name , Status )VALUES  ( N'{0}' , N'Trống')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateTable(int idTable, string name)
        {
            string query = string.Format("UPDATE dbo.TableFood SET name = N'{0}',status = N'Trống' WHERE id = {1}", name, idTable);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteTable(int idTable)
        {
             int result = DataProvider.Instance.ExecuteNonQuery("EXEC dbo.UDP_SelectBillTable @idTalbe ",new object[] {idTable});
             return result > 0;
        }
        public List<Table> SearchTableByName(string name)
        {
            List<Table> listtable= new List<Table>();

            string query = string.Format("SELECT * FROM dbo.TableFood WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                listtable.Add(table);
            }

            return listtable;
        }
    }
}
