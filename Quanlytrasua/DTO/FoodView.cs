using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DTO
{
    public class FoodView
    {
        private int iD;
        private string name;
        private string categoryName;
        private float price;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public float Price { get => price; set => price = value; }

        public FoodView(int id, string name, string categoryName, float price)
        {
            this.ID = id;
            this.Name = name;
            this.CategoryName = categoryName;
            this.Price = price;
        }
        public FoodView(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.CategoryName = row["categoryName"].ToString();
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }
    }
}
