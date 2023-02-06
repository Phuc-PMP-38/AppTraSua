using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public  class Billinfo
    {
        private int iD;
        private int billID;
        private int foodID;
        private int count;

        public int ID { get => iD; set => iD = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int Count { get => count; set => count = value; }

        public Billinfo(int id,int billid,int foodid,int count)
        {
            this.ID = id;
            this.BillID = billid;
            this.FoodID = foodid;
            this.Count = count;
        }
        public Billinfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["idbill"];
            this.FoodID = (int)row["idfood"];
            this.Count = (int)row["count"];
        }
    }
}
