using Quanlytrasua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DAO
{
    public class AccountDao
    {
        private static AccountDao instance;

        public static AccountDao Instance
        {
            get { if (instance == null) instance = new AccountDao(); return instance; }
            private set { instance = value; }
        }
        private AccountDao() { }
        public bool login(string username,string password)
        {
            string query = "SELECT UserName, DisplayName, Type FROM dbo.Account WHERE Username= '" + username+"' AND Password='"+password+"'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count>0;
        }
        public Account GetAccountByUsername(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM dbo.Account WHERE Username = N'" + userName + "' ");
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, pass, newPass });

            return result > 0;
        }
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM dbo.Account");
        }
        public bool InsertAccount(string name, string displayName, int type)
        {
            string query = string.Format("INSERT dbo.Account ( UserName, DisplayName, Type )VALUES  ( N'{0}', N'{1}', {2})", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string name, string displayName, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string name)
        {
            string query = string.Format("Delete Account where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ResetPassword(string name)
        {
            string query = string.Format("update account set password = N'0' where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public List<SearchAccount> SearchAccountByName(string name)
        {
            List<SearchAccount> listAccount = new List<SearchAccount>();

            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC UDP_SreachAccount @nameAccount ",new object[] { name});

            foreach (DataRow item in data.Rows)
            {
                SearchAccount account = new SearchAccount(item);
                listAccount.Add(account);
            }

            return listAccount;
        }
    }
}
