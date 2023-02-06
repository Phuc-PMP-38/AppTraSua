using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public class SearchAccount
    {

        private string userName;
        private string displayName;
        private int type;

        public string UserName { get => userName; set => userName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int Type { get => type; set => type = value; }

        public SearchAccount(string username, string displayname, int type)
        {
            this.UserName = username;
            this.DisplayName = displayname;
            this.Type = type;
        }
        public SearchAccount(DataRow row)
        {
            this.UserName = row["username"].ToString();
            this.DisplayName = row["displayname"].ToString();
            this.Type = (int)row["type"];
        }
    }
}
