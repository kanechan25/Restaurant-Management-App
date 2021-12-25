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

namespace RestaurantManagement
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public DTO.Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            LoadAll();
        }
        #region METHODs: just coding to understand more about get datatable from SQL server but it's not true for our three-layer model.
        List<DTO.Food> SearchFoodByName(string name)
        {
            List<DTO.Food> listFood = DAL.FoodDAL.Instance.SearchFoodByName(name);
            return listFood;
        }

        public void LoadAll()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            LoadAccountList();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadDateTimePickerBill();
            LoadFoodList();
            LoadFoodCategoryList();
            LoadTableList();
            AddFoodBinding();
            LoadCateIntoComboBox(cbCategory);
            AddAccountBinding();
        }

        public void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            cbAccountType.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if (DAL.AccountDAL.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Add a new Account successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Add a new Account Unsuccessful!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadAccountList();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (DAL.AccountDAL.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Update a new Account successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Update a new Account Unsuccessful!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadAccountList();
        }
        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("You are deleting your own account??", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DAL.AccountDAL.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Delete a new Account successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Delete a new Account Unsuccessful!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadAccountList();

        }
        void ResetPass(string name)
        {
            if (DAL.AccountDAL.Instance.ResetPass(name))
            {
                MessageBox.Show("Reset a new password successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Reset a new password Unsuccessful!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadAccountList();
        }
        public void LoadAccountList()
        {
            //string query = "SELECT * FROM dbo.Account WHERE UserName = N'linhvien' AND PassWord = N'linhlinh' ";
            /* Cach 1: 
            RestaurantManagement.DAL.DataProvider provider = new RestaurantManagement.DAL.DataProvider();
            dtgvAccount.DataSource = provider.ExecuteQuery(query); =>Ngoai query co the truyen them cac parameter[i] khac - cac keyword de truy van
            Nhung cach nay cu moi lan goi 1 data thi lai phai goi 1 lan, neu goi 10 datatable thi phai goi lan => khong on
            Vi the ap dung "static" cho cach 2 nhu sau: */
            //Cach 2:
            string query = "SELECT  * FROM dbo.Account";
            dtgvAccount.DataSource = RestaurantManagement.DAL.DataProvider.Instance.ExecuteQuery(query);
            //Tuong tu muon lay table Food, Category... ra chi can thay doi cau query
        }
        public void LoadFoodList()
        {
            string query = "SELECT  * FROM dbo.Food";
            dtgvFood.DataSource = RestaurantManagement.DAL.DataProvider.Instance.ExecuteQuery(query);
        }
        public void LoadFoodCategoryList()
        {
            string query = "SELECT  * FROM dbo.FoodCategory";
            dtgvCategory.DataSource = RestaurantManagement.DAL.DataProvider.Instance.ExecuteQuery(query);
        }
        public void LoadTableList()
        {
            string query = "SELECT  * FROM dbo.TableFood";
            dtgvTable.DataSource = RestaurantManagement.DAL.DataProvider.Instance.ExecuteQuery(query);
        }
        public void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = DAL.BillDAL.Instance.GetBillListByDate(checkIn, checkOut);
        }
        public void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
        }
        public void LoadListFood()
        {
            foodList.DataSource = DAL.FoodDAL.Instance.GetListFood();
        }
        public void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbFoodPrice.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        public void LoadCateIntoComboBox(ComboBox cb)
        {
            cb.DataSource = DAL.CategoryDAL.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        #endregion

        #region EVENTs

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["idCategory"].Value;
             
                    DTO.Category category = DAL.CategoryDAL.Instance.GetCategoryByID(id);
                    cbCategory.SelectedItem = category;
                    int index = -1;
                    int i = 0;
                    foreach (DTO.Category item in cbCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbCategory.SelectedIndex = index;
                 
                }
            }
            catch (Exception)
            {

            }
            
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbCategory.SelectedItem as DTO.Category).ID;
            float price = float.Parse(txbFoodPrice.Text);
            if (DAL.FoodDAL.Instance.InsertFood(name, categoryID, price))
            {
                LoadFoodList();

                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
                MessageBox.Show("Add a new Food successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error occurred!!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbCategory.SelectedItem as DTO.Category).ID;
            float price = float.Parse(txbFoodPrice.Text);
            int id = Convert.ToInt32(txbFoodID.Text);
            int index = dtgvFood.CurrentCell.RowIndex;
            if (DAL.FoodDAL.Instance.UpdateFood(id, name , categoryID, price))
            {
                LoadFoodList();
                dtgvFood.Rows[index].Selected = true;
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
                MessageBox.Show("Update a new Food successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error occurred!!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            int index = dtgvFood.CurrentCell.RowIndex;
            if (DAL.FoodDAL.Instance.DeleteFood(id))
            {
                LoadFoodList();
                dtgvFood.Rows[index].Selected = true;
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
                MessageBox.Show("Deleted a new Food successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error occurred!!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
           dtgvFood.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }
        private void txbSearchFoodName_TextChanged(object sender, EventArgs e)
        {
            dtgvFood.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = Convert.ToInt32(cbAccountType.Text);
            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = Convert.ToInt32(cbAccountType.Text);
            EditAccount(userName, displayName, type);
        }
        #endregion

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccountList();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            ResetPass(userName);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }

        private void cbCategory_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }

}
