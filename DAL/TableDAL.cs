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
    public class TableDAL
    {
        private static TableDAL instance;
        public static int tableWidth = 132;
        public static int tableHeigth = 132;
        public static TableDAL Instance
        {
            get { if (instance == null) instance = new TableDAL(); return TableDAL.instance; }
            private set { TableDAL.instance = value; }
        }

        private TableDAL() { }

        public void SwitchTable(int id1, int id2)
        {
            DAL.DataProvider.Instance.ExecuteQuery("USP_SwitchTable @idTable1, @idTable2", new object[] {id1, id2 });
        }
        public List<DTO.Table> LoadTableList()
        {
            List<DTO.Table> tableList = new List<DTO.Table>();
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery("USP_GetTableList");
            foreach (DataRow item in data.Rows)
            {
                DTO.Table table = new DTO.Table(item);
                tableList.Add(table);
            }
            return tableList;
        }


    }
}
