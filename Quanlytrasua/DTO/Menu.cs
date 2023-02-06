using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public class Menu
    {
        private string foodName;
        private int count;
        private float pirce;
        private float totalPrice;


        public string FoodName { get => foodName; set => foodName = value; }
        public int Count { get => count; set => count = value; }
        public float Pirce { get => pirce; set => pirce = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public Menu(string foodName,int count, float price, float totalPrice = 0)
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Pirce = price;
            this.TotalPrice = totalPrice;
        }
        public Menu(DataRow row)
        {
            this.FoodName = row["Name"].ToString();
            this.Count = (int)row["count"];
            this.Pirce = (float)Convert.ToDouble(row["price"]);
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"])
                ;
        }
    }
}
