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
    public class BillDAL
    {
        private static BillDAL instance;
        public static BillDAL Instance 
        {
            get { if (instance == null) instance = new BillDAL(); return BillDAL.instance;  }
            private set => BillDAL.instance = value; 
        }
        private BillDAL() { }

        public int GetUncheckGetBillIDByTableID(int id)
        {
            DataTable data = DAL.DataProvider.Instance.ExecuteQuery
                ("SELECT * FROM dbo.Bill WHERE idTable = "+ id +" AND status = 0");
            if (data.Rows.Count > 0)
            {
                DTO.Bill bill = new DTO.Bill(data.Rows[0]);
                return bill.ID;
            }

            return -1;
        }
        public void CheckOut(int id, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET status = 1, " + " discount = " + discount + ", totalPrice = " + totalPrice + " WHERE id = " + id;
            DAL.DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            DAL.DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBill @idTable", new object[] {id});
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return DAL.DataProvider.Instance.ExecuteQuery("USP_GetListBillByDate @checkIn , @checkOut", new object[] {checkIn , checkOut});
        }
        public int GetMaxBill() 
        {
            try
            {
                string query = "SELECT MAX(id) FROM dbo.Bill";
                return (int)DAL.DataProvider.Instance.ExecuteScalar(query);
            }
            catch
            {
                return 1;
            }

        }
    }
}
