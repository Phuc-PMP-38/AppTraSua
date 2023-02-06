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
using System.Globalization;
using System.Threading;

namespace Quanlytrasua
{
    public partial class Hethong : Form
    {
        // Lay tai khoan dang nhap vao
        private Account loginaccount;

        public Account Loginaccount {
            get { return loginaccount; } 
            set { loginaccount = value; } 
        }

        public Hethong(Account acc)
        {
            InitializeComponent();

            this.loginaccount = acc;
            ChageAccount(loginaccount.Type);
            loadTable();
            LoadCategory();
            LoadComboboxTable(cbbTable);
        }
        #region Method
        // kiem tra tai khoan va in ra ten tai khoan
        void ChageAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text +=" ( "+loginaccount.DisplayName+ " ) ";
        }
        // load table ban an
        void loadTable()
        {
            //xoa du lieu control ban an
            flpTable.Controls.Clear();
            // day du lieu ra list
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            // add button len man hinh
            foreach(Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.Tablewidth,Height=TableDAO.Tableheight};
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        // load combobox table
        void LoadComboboxTable(ComboBox cb)
        {
            cbbTable.DataSource = TableDAO.Instance.LoadTableList();
            cbbTable.DisplayMember = "Name";
        }
        // load combobox Category
        void LoadCategory() {
            List<Category> listCategory = CategoryDAO.Instance.GetCategoryList();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        //Load combobox food
        void LoadFoodListCategory(int id)
        {
            List<FoodView> listFood = FoodDAO.Instance.GetFoodList(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        //Show hoa don
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<Quanlytrasua.DTO.Menu> listMenu = MenuDAO.Instance.GetMenuList(id);
            float totalPrice = 0;
            foreach(Quanlytrasua.DTO.Menu item in listMenu)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Pirce.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            //Thread.CurrentThread.CurrentCulture = culture;
            txtTotal.Text = totalPrice.ToString("c",culture);
            
        }
        #endregion

        #region events

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = loginaccount;

            //food
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;

            //category
            f.InsertCategory += f_InsertCategoryFood;
            f.DeleteCategory += f_DeleteCategoryFood;
            f.UpdateCategory += f_UpdateCategoryFood;
            //table
            f.InserTable += f_InsertTable;
            f.DeleteTable += f_DeleteTable;
            f.UpdateTable += f_UpdateTable;
            f.ShowDialog();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(loginaccount);
            f.UpdateAccount  += f_UpdateAccount;
            f.ShowDialog();
        }
        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" +e.Acc.DisplayName+ ")";
        }
        void f_InsertTable(object sender, EventArgs e)
        {
            loadTable();
            // LoadFoodListCategory((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        void f_UpdateTable(object sender, EventArgs e)
        {
            loadTable();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        void f_DeleteTable(object sender, EventArgs e)
        {
            loadTable();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
        void f_UpdateCategoryFood(object sender, EventArgs e)
        {
            LoadCategory();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
        void f_DeleteCategoryFood(object sender, EventArgs e)
        {
            LoadCategory();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            loadTable();
        }
        
        void f_InsertCategoryFood(object sender, EventArgs e)
        {
            LoadCategory();
           // LoadFoodListCategory((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
       
        void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListCategory((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListCategory((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            loadTable();
        }

        void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListCategory((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadFoodListCategory(id);
        }
        private void btn_Click(object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableId);
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table !=null)
            {
                int idBill = BillDAO.Instance.GetUncheckIDBillbyIDTable(table.ID);
                int idCategory = (cbCategory.SelectedItem as Category).ID;
                int idfood = (cbFood.SelectedItem as FoodView).ID;
                int count = (int)cmFoodCount.Value;
                if (idBill == -1)
                {
                    BillDAO.Instance.InsertBill(table.ID);
                    BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), idfood, count, idCategory);
                }
                else
                {
                    BillInfoDAO.Instance.InsertBillInfo(idBill, idfood, count, idCategory);
                }
                ShowBill(table.ID);
                loadTable();
            }
            else
            {
                MessageBox.Show("moi chon ban");
            }
           
        }
        private void Hethong_Load(object sender, EventArgs e)
        {

        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckIDBillbyIDTable(table.ID);
            int discount = (int)nmDiscount.Value;

            double totalprice =Convert.ToDouble(txtTotal.Text.Split(',')[0].Replace(".", ""));
            double finaltotal = totalprice - (totalprice / 100) * discount;
            if (idBill != -1)
            {
                if(MessageBox.Show(String.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0} \n Tổng tiền - (Tổng tiền / 100)*Giảm giá \n=> {1}-({1}/100)*{2} = {3} " ,table.Name,totalprice,discount,finaltotal),"Thông báo ", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.checkOut(idBill,discount, (float)finaltotal);
                    ShowBill(table.ID);
                    loadTable();
                }
            }

        }
        private void btnSwithTable_Click(object sender, EventArgs e)
        {
            
            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbbTable.SelectedItem as Table).ID;
            if (MessageBox.Show(String.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1} không ? ", (lsvBill.Tag as Table).Name, (cbbTable.SelectedItem as Table).Name),"Thông báo ",MessageBoxButtons.OKCancel)== System.Windows.Forms.DialogResult.OK)
            { 
                TableDAO.Instance.SwithTable(id1, id2); 
            }
            loadTable();
        }

        #endregion

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnThanhtoan_Click(this,new  EventArgs ());
        }

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }
    }
}
