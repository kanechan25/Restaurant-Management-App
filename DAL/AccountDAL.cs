using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace RestaurantManagement.DAL
{
    public class AccountDAL
    {
        private static AccountDAL instance;

        public static AccountDAL Instance
        {
            get { if (instance == null) instance = new AccountDAL(); return instance; }
            private set { instance = value; }
        }

        private AccountDAL() { }

        public bool Login(string userName, string passWord)
        {
            /*byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord);
            byte[] hashData = new System.Security.Cryptography.HMACMD5().ComputeHash(temp);
            var list = hashData.ToString();
            list.Reverse();*/


            bool exist = false;
            /*string query = "SELECT * FROM dbo.Account WHERE UserName = N'"+userName+"' AND PassWord = N'"+passWord+"' "; 
            Cau query tren bi dinh loi SQL Injection => khong dc*/
            string query = "USP_Login @userName , @passWord"; //**Chu y tuyet doi dau cach (SPACE) giua 2 parameter
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {userName, passWord });
            if (result.Rows.Count > 0) exist = true;
            return exist;
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            string query = "USP_UpdateAccount @userName , @displayName , @passWord , @newPassword";
            int result = RestaurantManagement.DAL.DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, displayName, pass, newPass });
            return result > 0;
        }

        public DTO.Account GetAccountByUserName(string userName)
        {
            string query = "select * from account where userName = '" + userName + "'";
            DataTable data = RestaurantManagement.DAL.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                return new DTO.Account(item);

            }
            return null;
        }
        public bool InsertAccount(string name, string displayName, int type)
        {
            string query = string.Format("insert dbo.Account (UserName, DisplayName, Type) values (N'{0}', N'{1}' , {2})", name, displayName, type);
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateAccount(string name, string displayName, int type)
        {
            string query = string.Format("update dbo.Account set DisplayName = N'{1}', Type = {2} where UserName = N'{0}'", name, displayName, type);
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(string name)
        {
            string query = string.Format("DELETE dbo.Account where UserName = N'{0}'", name);
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool ResetPass(string name)
        {
            string query = string.Format("update dbo.Account set password = N'1' where UserName = N'{0}'", name);
            int result = DAL.DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
