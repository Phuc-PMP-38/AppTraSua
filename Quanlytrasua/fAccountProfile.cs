using Quanlytrasua.DAO;
using Quanlytrasua.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlytrasua
{
    public partial class fAccountProfile : Form
    {
        private Account loginaccount;

        public Account Loginaccount
        {
            get { return loginaccount; }
            set { loginaccount = value; }
        }
        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            this.loginaccount = acc;
            ChageAccount(loginaccount);
        }
        void ChageAccount(Account acc)
        {
            txtUsername.Text = loginaccount.UserName;
            txtUser.Text = loginaccount.DisplayName;
        }
        
       
        void UpdateAccountInfo()
        {
            string displayName = txtUser.Text;
            string password = txtPassword.Text;
            string newpass = txtPasswordNew.Text;
            string reenterPass = txtPasswordNew2.Text;
            string userName = txtUsername.Text;

            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới!");
            }
            else
            {
                if (AccountDao.Instance.UpdateAccount(userName, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công");
                    if (updateAccount != null)
                        updateAccount(this, new AccountEvent(AccountDao.Instance.GetAccountByUsername(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khấu");
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
