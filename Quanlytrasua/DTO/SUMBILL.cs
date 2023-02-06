using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public class SUMBILL
    {
        private int iD;
        private DateTime? dateSum;
        private double total;
        public int ID { get => iD; set => iD = value; }

        public DateTime? DateSum { get => dateSum; set => dateSum = value; }
        public double Total { get => total; set => total = value; }
    }
}
