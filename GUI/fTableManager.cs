using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using RestaurantManagement.DTO;

namespace RestaurantManagement
{
    public partial class fTableManager : Form
    {
        private DTO.Account loginAccount;

        public Account LoginAccount 
        { 
            get => loginAccount;
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public fTableManager(DTO.Account acc)
        {

            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategory();
            LoadComboBoxTable(cbSwitchTable);
        }
        #region Methods
        public void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            settingAccountToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
            accountInfomationsToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        public void LoadCategory()
        {
            List<DTO.Category> listCategory = DAL.CategoryDAL.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "name";
        }
        public void LoadFoodListByCategoryID(int id)
        {
            List<DTO.Food> listFood = DAL.FoodDAL.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "name";
        }
        public void LoadTable()
        {
            flpTable.Controls.Clear();
            List<DTO.Table> tableList = DAL.TableDAL.Instance.LoadTableList();
            //Tao button tuong ung voi tung item trong tableList
            foreach (DTO.Table item in tableList)
            {
                Button btnTable = new Button()
                { 
                    Width = DAL.TableDAL.tableWidth, Height = DAL.TableDAL.tableHeigth,
                    Text = item.Name + Environment.NewLine + item.Status,
                    Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
                };
                btnTable.Click += BtnTable_Click;
                btnTable.Tag = item;
                //Doi mau button (table) khi thay doi status
                switch (item.Status)
                {
                    case "Empty":
                        btnTable.BackColor = Color.LightCyan;
                        break;
                    default:
                        btnTable.BackColor = Color.IndianRed;
                        break;
                }
                flpTable.Controls.Add(btnTable);
            }
        }
        public void ShowBill(int id)
        {
            /*listBill.Items.Clear();
            List<DTO.BillInfo> listBillInfo = DAL.BillInfoDAL.Instance.GetListBillInfo(
                DAL.BillDAL.Instance.GetUncheckGetBillIDByTableID(id));
            foreach (DTO.BillInfo item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodID.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                listBill.Items.Add(lsvItem);
            }
            //DataGridViewBand dau dinh dien listBill, nhung sau khong can,
            //quyet dinh lam 1 class trung gian la class Menu:*/

            listBill.Items.Clear();
            List<DTO.Menu> listBillInfo = DAL.MenuDAL.Instance.GetListMenuByTable(id);
            float amountTotal = 0;
            foreach (DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                amountTotal += item.TotalPrice;
                listBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentCulture = culture;
            txbTotalPrice.Text = amountTotal.ToString("N0", culture);
        }
        public void LoadComboBoxTable(ComboBox cbb)
        {
            cbb.DataSource = DAL.TableDAL.Instance.LoadTableList();
            cbb.DisplayMember = "Name";

        }


        #endregion

        #region Events
        private void BtnTable_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as DTO.Table).ID;
            listBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        private void settingAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile frm = new fAccountProfile(LoginAccount);
            frm.UpdateAccount += f_UpdateAccount;
            frm.ShowDialog();
        }
        void f_UpdateAccount(object sender, AccountEvent e)
        {
            accountInfomationsToolStripMenuItem.Text = "Account (" + e.Acc.DisplayName + ")";
            settingAccountToolStripMenuItem.Text = "Setting Account (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin frm = new fAdmin();
            frm.loginAccount = LoginAccount;
            frm.InsertFood += Frm_InsertFood;
            frm.UpdateFood += Frm_UpdateFood;
            frm.DeleteFood += Frm_DeleteFood;
            frm.ShowDialog();
        }

        private void Frm_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as DTO.Category).ID);
            if (listBill.Tag != null) ShowBill((listBill.Tag as Table).ID);
            LoadTable();
        }

        private void Frm_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as DTO.Category).ID);
            if (listBill.Tag != null) ShowBill((listBill.Tag as Table).ID);

        }

        private void Frm_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as DTO.Category).ID);
            if (listBill.Tag != null) ShowBill((listBill.Tag as Table).ID);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;
            DTO.Category selected = cb.SelectedItem as DTO.Category;
            id = selected.ID;
            LoadFoodListByCategoryID(id);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DTO.Table table = listBill.Tag as DTO.Table;
            if (table == null)
            {
                MessageBox.Show("Please choose a table!!");
                return;
            }


            //1. TH1. Co bill san roi, kich table vao mo ra o listview
            int idBill = DAL.BillDAL.Instance.GetUncheckGetBillIDByTableID(table.ID);
            int maxIDBill = DAL.BillDAL.Instance.GetMaxBill();
            int foodID = (cbFood.SelectedItem as DTO.Food).ID;
            int count = (int)nmFoodCount.Value;
            if (idBill == -1)
            {
                DAL.BillDAL.Instance.InsertBill(table.ID);
                DAL.BillInfoDAL.Instance.InsertBillInfo(maxIDBill , foodID , count);
            }
            else
            {
                DAL.BillInfoDAL.Instance.InsertBillInfo(idBill, foodID, count);
            }
            ShowBill(table.ID);
            LoadTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            DTO.Table table = listBill.Tag as DTO.Table;
            int idBill = DAL.BillDAL.Instance.GetUncheckGetBillIDByTableID(table.ID);
            int discount = (int)nmDiscount.Value;
            float totalPrice = float.Parse(txbTotalPrice.Text);
            float finalTotal = totalPrice - (totalPrice * discount) / 100;
            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Do you want to Check Out {0}\n Total after discount \n = {1} - ({1} x {2}) / 100 = {3}", table.Name, totalPrice, discount, finalTotal), "Notice",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    DAL.BillDAL.Instance.CheckOut(idBill, discount, finalTotal);
                    //listBill.Items.Clear();
                    ShowBill(table.ID);
                    LoadTable();
                }
            }

        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            try
            {
                string nameId1 = (listBill.Tag as DTO.Table).Name;
                string nameId2 = (cbSwitchTable.SelectedItem as DTO.Table).Name;
                if (MessageBox.Show(string.Format("Do you wanna switch the selected Table ({0}) with Table in ComboBox ({1})?", nameId1, nameId2),
                    "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int id1 = (listBill.Tag as DTO.Table).ID;
                    int id2 = (cbSwitchTable.SelectedItem as DTO.Table).ID;
                    DAL.TableDAL.Instance.SwitchTable(id1, id2);
                    LoadTable();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("This Function has been Error and has not fixed yet!!");
            }



        }
        #endregion

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAdd_Click(this, new EventArgs());
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCheckOut_Click(this, new EventArgs());
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }
    }
}
