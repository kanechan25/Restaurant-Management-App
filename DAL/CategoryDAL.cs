using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagement.DAL
{
    public class CategoryDAL
    {
        private static CategoryDAL instance;
        public static CategoryDAL Instance
        {
            get { if (instance == null) instance = new CategoryDAL(); return CategoryDAL.instance; }
            private set => CategoryDAL.instance = value;
        }
        private CategoryDAL() { }

        public List<DTO.Category> GetListCategory()
        {
            List<DTO.Category> listCate = new List<DTO.Category>();
            string query = "SELECT * FROM FoodCategory";
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Category category = new DTO.Category(item);
                listCate.Add(category);
            }

            return listCate;
        }
        public DTO.Category GetCategoryByID(int id)
        {
            DTO.Category category = null;
            string query = "select * from dbo.FoodCategory where id = " + id;
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                category = new DTO.Category(item);
                return category;
            }
            return category;
        }
    }
}
