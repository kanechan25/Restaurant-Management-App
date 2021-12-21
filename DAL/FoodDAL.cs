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
    public class FoodDAL
    {
        private static FoodDAL instance;
        public static FoodDAL Instance
        {
            get { if (instance == null) instance = new FoodDAL(); return FoodDAL.instance; }
            private set => FoodDAL.instance = value;
        }
        private FoodDAL() { }

        public List<DTO.Food> GetFoodByCategoryID(int id)
        {
            List<DTO.Food> listFood = new List<DTO.Food>();
            string query = "SELECT * FROM   dbo.Food WHERE idCategory = " + id;
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Food food = new DTO.Food(item);
                listFood.Add(food);
            }

            return listFood;
        }

        public List<DTO.Food> GetListFood()
        {
            List<DTO.Food> listFood = new List<DTO.Food>();
            string query = "SELECT * FROM   dbo.Food ";
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Food food = new DTO.Food(item);
                listFood.Add(food);
            }

            return listFood;
        }

        public List<DTO.Food> SearchFoodByName(string name)
        {
            List<DTO.Food> listFood = new List<DTO.Food>();
            string query = string.Format("SELECT * FROM dbo.Food where name like N'%{0}%'", name);
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Food food = new DTO.Food(item);
                listFood.Add(food);
            }

            return listFood;
        }

        public bool InsertFood(string name, int id, float price)
        {
            string query = string.Format("insert dbo.Food (name, idCategory, price) values (N'{0}', {1} , {2})", name, id, price);
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateFood(int idFood, string name, int id, float price)
        {
            string query = string.Format("update dbo.Food set name = N'{0}', idCategory = {1}, price = {2} where id = {3}", name , id , price , idFood);
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            DAL.BillInfoDAL.Instance.DeleteBillInfoByFoodID(idFood);
            string query = string.Format("DELETE dbo.Food where id = {0}", idFood);
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

    }
}
