using Quanlytrasua.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public  class Account
    {
        private string userName;
        private string displayName;
        private string passWord;
        private int type;

        public string UserName { get => userName; set => userName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public int Type { get => type; set => type = value; }

        public Account(string username,string displayname,int type,string password=null)
        {
            this.UserName = username;
            this.DisplayName = displayname;
            this.PassWord = password;
            this.Type = type;
        }
        public Account(DataRow row)
        {
            this.UserName = row["username"].ToString();
            this.DisplayName = row["displayname"].ToString();
            this.Type = (int)row["type"];
        }
        
    }
}
