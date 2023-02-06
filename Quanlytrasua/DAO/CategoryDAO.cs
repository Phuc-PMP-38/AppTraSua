using Quanlytrasua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlytrasua.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }
        private CategoryDAO() { }
        public List<Category> GetCategoryList()
        {
            List<Category> CategoryList = new List<Category>();
            string query = "select * from FoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                CategoryList.Add(category);
            }
            return CategoryList;
        }
        public Category GetCategoryByID(int id)
        {
            Category category = null;

            string query = "select * from FoodCategory where id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }

            return category;
        }
        public int KiemtraCategory(string name)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.FoodCategory WHERE categoryName like  N'" + name + "'");
            if (data.Rows.Count > 0)
            {
                return 1;
            }
            return -1;
        }
        public bool InsertFoodCategory(string name)
        {
            string query = string.Format("INSERT dbo.FoodCategory ( categoryName )VALUES  ( N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateFoodCategory(int idFoodCategory, string name)
        {
            string query = string.Format("UPDATE dbo.FoodCategory SET categoryName = N'{0}' WHERE id = {1}", name, idFoodCategory);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteFoodCategory(int idFoodCategory)
        {
            //BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFoodCategory);
            string query0 = string.Format("Delete BillInfo where idCategory = {0}", idFoodCategory);
            int result0 = DataProvider.Instance.ExecuteNonQuery(query0);
            string query1 = string.Format("Delete Food where idCategory = {0}", idFoodCategory);
            int result1 = DataProvider.Instance.ExecuteNonQuery(query1);
            string query = string.Format("Delete FoodCategory where id = {0}", idFoodCategory);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public List<Category> SearchCategoryByName(string name)
        {
            List<Category> listcategory = new List<Category>();

            string query = string.Format("SELECT * FROM dbo.FoodCategory WHERE dbo.fuConvertToUnsign1(categoryName) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listcategory.Add(category);
            }

            return listcategory;
        }
    }
}
