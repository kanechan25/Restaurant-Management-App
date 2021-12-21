using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantManagement.DAL
{
    public class DataProvider
    {
        private static DataProvider instance;

        private string connectionStr = @"Data Source=DESKTOP-JQN5FHE\KANE;Initial Catalog=RestaurantManagement;Integrated Security=True";

        public static DataProvider Instance 
        {
            get { if (instance == null) instance = new DataProvider();  return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }
        private DataProvider()
        { }

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {   //return a datatable containing data satisfy query execution 
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }
        public int  ExecuteNonQuery(string query, object[] parameter = null)
        {   //Only return number of lines satisfying the query conditions
            int dataLineNumber = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                dataLineNumber = command.ExecuteNonQuery();
                connection.Close();
            }
            return dataLineNumber;
        }
        public object ExecuteScalar(string query, object[] parameter = null)
        {   //using for "SELECT COUNT *  "query
            object dataObject = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                dataObject = command.ExecuteScalar();
                connection.Close();
            }
            return dataObject;
        }
    }

}
