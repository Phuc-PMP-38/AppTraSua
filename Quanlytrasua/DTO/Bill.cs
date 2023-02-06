using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public class Bill
    {
        private int iD;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int status;
        private int discount;
        public int ID { get => iD; set => iD = value; }

        public int Status { get => status; set => status = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public int Discount { get => discount; set => discount = value; }

        public Bill(int id,DateTime? dateCheckin,DateTime? dateCheckOut,int status,int dicount)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckin;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = dicount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckin"];
            var DateCheckOuttemp = row["dateCheckOut"];
            if (DateCheckOuttemp.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)DateCheckOuttemp;
            }
            this.Status = (int)row["status"];

            if(row["discount"].ToString() != "")
                this.Discount = (int)row["discount"];
        }
    }
}
