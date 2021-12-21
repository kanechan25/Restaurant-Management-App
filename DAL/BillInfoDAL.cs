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
    public class BillInfoDAL
    {
        private static BillInfoDAL instance;
        public static BillInfoDAL Instance
        {
            get { if (instance == null) instance = new BillInfoDAL(); return BillInfoDAL.instance; }
            private set { BillInfoDAL.instance = value; }
        }

        private BillInfoDAL() { }
        public void DeleteBillInfoByFoodID(int id)
        {
            DAL.DataProvider.Instance.ExecuteQuery("DELETE dbo.BillInfo WHERE idFood = " + id);
        }
        public List<DTO.BillInfo> GetListBillInfo(int id)
        {
            List<DTO.BillInfo> listBillInfo = new List<DTO.BillInfo>();
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE idBill = " + id);
            foreach (DataRow item in data.Rows)
            {
                DTO.BillInfo info = new DTO.BillInfo(item);
                listBillInfo.Add(info);


            }
            return listBillInfo;
        }
        public void InsertBillInfo(int idBill, int idFood , int count)
        {
            DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBillInfo @idBill , @idFood , @count ",
                new object[] { idBill, idFood, count });
        }



    }
}
