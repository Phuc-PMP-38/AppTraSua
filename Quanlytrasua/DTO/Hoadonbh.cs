using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public  class Hoadonbh
    {
        public DateTime? dateCheckIn;
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public int totalPrice;
        public int TotalPrice { get => TotalPrice; set => TotalPrice = value; }
        public Hoadonbh(DateTime? dateCheckin, int totalPrice)
        {
            this.DateCheckIn = dateCheckin;
            this.TotalPrice = totalPrice;
        }
    }
}
