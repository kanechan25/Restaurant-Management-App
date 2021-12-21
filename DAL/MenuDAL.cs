using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagement.DAL
{
    public class MenuDAL
    {
        private static MenuDAL instance;
        public static MenuDAL Instance
        {
            get { if (instance == null) instance = new MenuDAL(); return MenuDAL.instance; }
            private set => MenuDAL.instance = value;
        }
        private MenuDAL() { }

        public List<DTO.Menu> GetListMenuByTable(int id)
        {
            List<DTO.Menu> listMenu = new List<DTO.Menu>();
            string query = "SELECT f.name, bi.count, f.price, f.price*bi.count AS [totalPrice] " +
                "FROM dbo.Bill AS b, dbo.BillInfo AS bi, dbo.Food AS f " +
                "WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable = " + id ;
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Menu menu = new DTO.Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }
    }
}
