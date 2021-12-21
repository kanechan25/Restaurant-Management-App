using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagement
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string passWord = txbPassWord.Text;
            if (Login(userName, passWord) == true)
            {
                DTO.Account loginAccount = DAL.AccountDAL.Instance.GetAccountByUserName(userName);
                fTableManager frm = new fTableManager(loginAccount);
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Login Name or Password is not correct! Please try again!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        bool Login(string userName, string passWord)
        {
            return RestaurantManagement.DAL.AccountDAL.Instance.Login(userName, passWord);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to exit program?", "Notice!!", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }

        }
    }

}
