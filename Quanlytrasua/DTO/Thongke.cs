using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public class Thongke
    {
        private int dateSum;
        private double total;

        public int DateSum { get => dateSum; set => dateSum = value; }
        public double Total { get => total; set => total = value; }
        public Thongke(int dateSum, double total)
        {
            this.DateSum = dateSum;
            this.Total = total;
        }
        public Thongke(DataRow row)
        {
            this.DateSum = (int)row["DateSum"];
            this.Total = (double)row["Total"];
        }
    }
}
