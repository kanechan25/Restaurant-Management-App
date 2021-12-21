using RestaurantManagement.DTO;
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
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount 
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public fAccountProfile(DTO.Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }
        public void ChangeAccount(DTO.Account acc)
        {
            txbUserName.Text = LoginAccount.UserName;
            txbDisplayName.Text = LoginAccount.DisplayName;
        }
        public void UpdateAccountInfo()
        {
            string displayName = txbDisplayName.Text;
            string password = txbPassWord.Text;
            string newpass = txbNewPass.Text;
            string reenterPass = txbReEnterPass.Text;
            string userName = txbUserName.Text;
            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("You Reentered Password Uncorrectly!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (DAL.AccountDAL.Instance.UpdateAccount(userName, displayName, password, newpass))
                {
                    MessageBox.Show("You Updated Successfully !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (updateAccount != null)
                    {
                        updateAccount(this, new AccountEvent(DAL.AccountDAL.Instance.GetAccountByUserName(userName)));
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the Password correctly !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private event EventHandler<AccountEvent> updateAccount;

        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

    }
    public class AccountEvent:EventArgs
    {
        private Account acc;

        public Account Acc { get => acc; set => acc = value; }
        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }

    }
}
