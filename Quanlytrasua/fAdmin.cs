using Microsoft.Reporting.WinForms;
using Quanlytrasua.DAO;
using Quanlytrasua.DataAccessLayer;
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
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using System.Windows.Forms.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace Quanlytrasua
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource CategoryList = new BindingSource();
        BindingSource TableList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource NhanVienList = new BindingSource();
        public DTO.Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();


            dtgFood.DataSource = foodList;
            dtgCategory.DataSource = CategoryList;
            dtgTable.DataSource = TableList;
            dtgAccount.DataSource = accountList;
            dtgNhanvien.DataSource = NhanVienList;
            LoadListBillByDate(DateIn.Value, DateOut.Value);
            LoadDateTimePickerView();
            //-Load DataGriview
            LoadListFood();
            LoadListFoodCategory();
            LoadAccount();
            LoadListTables();
            LoadListSUMBILL();
           loadChart();
            LoadNhanVien();

            LoadCategoryIntoCombobox(cbFoodCategory);
          //  AddFoodBinding();
        //    AddAccountBinding();
           // AddFoodCategory();
           // AddTableBinding();
        }
        
        private void tbTable_Click(object sender, EventArgs e)
        {

        }

        #region methods
        List<DTO.Food> SearchFoodByName(string name)
        {
            List<DTO.Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }
        List<Category> SearchCategoryByName(string name)
        {
            List<Category> listCategory = CategoryDAO.Instance.SearchCategoryByName(name);

            return listCategory;
        }
        List<DTO.SearchAccount> SearchAccountByName(string name)
        {
            List<DTO.SearchAccount> listAccount = AccountDao.Instance.SearchAccountByName(name);

            return listAccount;
        }
        List<DTO.NhanVien> SearchNhanVienByName(string name)
        {
            List<DTO.NhanVien> listNV = NhanVienDAO.Instance.SearchNVtByName(name);

            return listNV;
        }
        List<Table> SearchTableByName(string name)
        {
            List<Table> listTable = TableDAO.Instance.SearchTableByName(name);

            return listTable;
        }

        void LoadListBillByDate(DateTime checkIn,DateTime checkOut)
        {
            dtgTableDT.DataSource= BillDAO.Instance.getBillListByDate(checkIn, checkOut);
        }
        void LoadListSUMBILL()
        {
            dataGridView1.DataSource = SUMBILLDAO.Instance.GetListSUMBILL();
        }
        void loadChart()
        {
            chart1.DataSource = SUMBILLDAO.Instance.GetListSUMBILL(); ;
            double sum = SUMBILLDAO.Instance.GetNumBillListBySum();
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart1.Series["Total"].XValueMember = "DateSum";
           // set the member columns of the chart data source used to data bind to the X-values of the series  
            chart1.Series["Total"].YValueMembers = "Total";
           chart1.Titles.Add("Salary Chart");

            chart2.DataSource = SUMBILLDAO.Instance.GetListSUMBILL(); ;
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart2.Series["Total"].XValueMember ="DateSum";
            // set the member columns of the chart data source used to data bind to the X-values of the series  
            chart2.Series["Total"].YValueMembers = "Total";
            chart2.Titles.Add("Biểu đồ các tháng");
        }
        void LoadDateTimePickerView()
        {
            DateTime Today =  DateTime.Now;
            DateIn.Value = new DateTime(Today.Year,Today.Month,1);
            DateOut.Value = DateIn.Value.AddMonths(1).AddDays(-1);

        }
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Clear();
            txbFoodID.DataBindings.Clear();
            nmFoodPrice.DataBindings.Clear();
            cbFoodCategory.DataBindings.Clear();
            txbFoodName.DataBindings.Add(new Binding("Text", dtgFood.DataSource, "name"));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgFood.DataSource, "id"));
            cbFoodCategory.DataBindings.Add(new Binding("Text", dtgFood.DataSource, "CategoryName"));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgFood.DataSource, "price"));
            
        }
        void AddNhanVien()
        {
            txtIDNV.DataBindings.Clear();
            txtNameNV.DataBindings.Clear();
            DateTimeNV.DataBindings.Clear();
            cbbGioitinh.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtIDNV.DataBindings.Add(new Binding("Text", dtgNhanvien.DataSource, "id"));
            txtNameNV.DataBindings.Add(new Binding("Text", dtgNhanvien.DataSource, "name"));
            DateTimeNV.DataBindings.Add(new Binding("Value", dtgNhanvien.DataSource, "NgaySinh"));
            cbbGioitinh.DataBindings.Add(new Binding("Text", dtgNhanvien.DataSource, "GioiTinh"));
            txtDiaChi.DataBindings.Add(new Binding("Text", dtgNhanvien.DataSource, "DiaChi"));
            txtSDT.DataBindings.Add(new Binding("Text", dtgNhanvien.DataSource, "SDT"));

        }
        void AddTableBinding()
        {
            txtIdtable.DataBindings.Clear();
            txtTable.DataBindings.Clear();
            cbbTT.DataBindings.Clear();
            txtIdtable.DataBindings.Add(new Binding("Text", dtgTable.DataSource, "id"));
            txtTable.DataBindings.Add(new Binding("Text", dtgTable.DataSource, "name"));
            cbbTT.DataBindings.Add(new Binding("Text", dtgTable.DataSource, "status"));
        }
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Clear();
            txbDisplayName.DataBindings.Clear();
            numericUpDown1.DataBindings.Clear();
            txbUserName.DataBindings.Add(new Binding("Text", dtgAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void AddFoodCategory()
        {
            txtIDDM.DataBindings.Clear();
            txtNameDM.DataBindings.Clear();
            txtIDDM.DataBindings.Add(new Binding("Text", dtgCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtNameDM.DataBindings.Add(new Binding("Text", dtgCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        { 
            cb.DataSource = CategoryDAO.Instance.GetCategoryList();
            cb.DisplayMember = "Name";
           // cb.ValueMember = "categoryName";
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadListFoodCategory()
        {
            CategoryList.DataSource = CategoryDAO.Instance.GetCategoryList();
        }
        void LoadListTables()
        {
            TableList.DataSource = TableDAO.Instance.LoadTableList();
        }
        void LoadNhanVien()
        {
            NhanVienList.DataSource = NhanVienDAO.Instance.GetListNhanVien();
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDao.Instance.GetListAccount();
        }
        #endregion
        #region Events
        private void btnThongke_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(DateIn.Value,DateOut.Value);
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
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        private event EventHandler inserTable;
        public event EventHandler InserTable
        {
            add { inserTable += value; }
            remove { inserTable -= value; }
        }

        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }

        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDao.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }

            LoadAccount();
        }
        void AddNhanvien(string name, DateTime ngaysinh, string gioitinh, string diachi, string sdt)
        {
            if (NhanVienDAO.Instance.InsertNhanVien(name, ngaysinh, gioitinh, diachi, sdt))
            {
                MessageBox.Show("Thêm nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại");
            }

            LoadNhanVien();
        }
        void EditNhanVien(int id,string name, DateTime ngaysinh, string gioitinh, string diachi, string sdt)
        {
            if (NhanVienDAO.Instance.UpdateNhanVien(id,name, ngaysinh, gioitinh, diachi, sdt))
            {
                MessageBox.Show("Cập nhật nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật nhân viên thất bại");
            }

            LoadNhanVien();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDao.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xóa chính bạn chứ");
                return;
            }
            if (AccountDao.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }
        void DeleteNhanvien(int id)
        {
           /* if (loginAccount.UserName.Equals(id))
            {
                MessageBox.Show("Vui lòng đừng xóa chính bạn chứ");
                return;
            }*/
            if (NhanVienDAO.Instance.deleteNhanVien(id))
            {
                MessageBox.Show("Xóa nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Xóa nhân viên thất bại");
            }

            LoadNhanVien();
        }
        void ResetPass(string userName)
        {
            if (AccountDao.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }
        #endregion

        private void btnList_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (dtgFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
        }
        #region skFood
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice1.Value;
            if(FoodDAO.Instance.KiemtraFood(name) ==-1)
            {
                if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                {
                    MessageBox.Show("Thêm món thành công");
                    LoadListFood();
                    if (insertFood != null)
                        insertFood(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm thức ăn");
                }
            }
            else
            {
                MessageBox.Show("Ten thuc an da ton tai");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }

        private void dtgFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Enabled = false;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            txbFoodID.Enabled = false;
            AddFoodBinding();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txbFoodID.Enabled = true;
            LoadListFood();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }
        #endregion
        private void btnThemDM_Click(object sender, EventArgs e)
        {
            string name = txtNameDM.Text;
            if(CategoryDAO.Instance.KiemtraCategory(name) == -1)
            {
                if (CategoryDAO.Instance.InsertFoodCategory(name))
                {
                    MessageBox.Show("Thêm danh mục thành công");
                    LoadListFoodCategory();
                    if (insertCategory != null)
                        insertCategory(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm danh muc");
                }
            }
            else
            {
                MessageBox.Show("danh muc da ton tai");
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadAccount();
            txbUserName.Enabled = true;
            btnAddAccount.Enabled = true;
            btnSuaAccount.Enabled = false;
           
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPass(userName);
        }

        private void btnSuaAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            EditAccount(userName, displayName, type);
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnXoaAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }

        private void dtgAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAddAccount.Enabled = false;
            txbUserName.Enabled = false;
            btnSuaAccount.Enabled = true;
            AddAccountBinding();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        #region thongke
        private void btnSau_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(DateIn.Value, DateOut.Value);

            if (page < sumRecord)
                page++;

            txbPageBill.Text = page.ToString();
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);

            if (page > 1)
                page--;

            txbPageBill.Text = page.ToString();
        }

        private void btnFist_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(DateIn.Value, DateOut.Value);

            int lastPage = sumRecord / 10;

            if (sumRecord % 10 != 0)
                lastPage++;

            txbPageBill.Text = lastPage.ToString();
        }

        private void txbPageBill_TextChanged_1(object sender, EventArgs e)
        {
            dtgTableDT.DataSource = BillDAO.Instance.GetBillListByDateAndPage(DateIn.Value, DateOut.Value, Convert.ToInt32(txbPageBill.Text));
        }
#endregion
        #region danhmuc
        private void btnEditDM_Click(object sender, EventArgs e)
        {
            string name = txtNameDM.Text;
            int id = Convert.ToInt32(txtIDDM.Text);

            if (CategoryDAO.Instance.UpdateFoodCategory(id, name))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFoodCategory();
                if (updateCategory != null)
                    updateCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDeleteDM_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDDM.Text);

            if (CategoryDAO.Instance.DeleteFoodCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công");
                LoadListFoodCategory();
                if (deleteCategory != null)
                    deleteCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục");
            }
        }
        private void btnSeachDM_Click(object sender, EventArgs e)
        {
            CategoryList.DataSource = SearchCategoryByName(txbSearchCategory.Text);
        }

        private void btnShowDM_Click(object sender, EventArgs e)
        {
            btnEditDM.Enabled = false;
            btnDeleteDM.Enabled = false;
            btnThemDM.Enabled = true;
            LoadListFoodCategory();
        }
#endregion
        #region table
        private void btnSeachtable_Click(object sender, EventArgs e)
        {
            TableList.DataSource = SearchTableByName(txtSeachTable.Text);
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIdtable.Text);
            string status = cbbTT.SelectedItem.ToString();
            if(status == "Có người")
            {
                if (TableDAO.Instance.DeleteTable(id))
                {
                    MessageBox.Show("Xóa danh mục thành công");
                    LoadListTables();
                    if (deleteTable != null)
                        deleteTable(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa danh mục");
                }
            }
           
        }

        private void btnShowtable_Click(object sender, EventArgs e)
        {
            txtIdtable.Text = "";
            txtTable.Text="";
            btnDeleteTable.Enabled = false;
            btnUpdateTable.Enabled = false;
            button16.Enabled = true;
            LoadListTables();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string name = txtTable.Text;
            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm bàn thành công");
                LoadListTables();
                if (inserTable != null)
                    inserTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh muc");
            }
        }
        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            string name = txtTable.Text;
            int id = Convert.ToInt32(txtIdtable.Text);

            if (TableDAO.Instance.UpdateTable(id, name))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListTables();
                if (updateTable != null)
                    updateTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {
            this.rpvThongke.RefreshReport();
        }

        private void fAdmin_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QUANLYTRASUADataSet.USP_getTableList' table. You can move, or remove it, as needed.
            Hienthihoadon();
            btnDeleteDM.Enabled = false;
            btnEditDM.Enabled = false;
            btnDeleteTable.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnUpdateTable.Enabled = false;
            btnSuaAccount.Enabled = false;
        }

       private void Hienthihoadon()
        {
            using (var _Context = new Model1())
            {
                string query = "SELECT * FROM dbo.SUMBILLDATE";
                List<SUMBILL> danhsach = _Context.Database.SqlQuery<SUMBILL>(query).ToList();
                this.rpvThongke.LocalReport.ReportPath = "Report1.rdlc";
                var reportdataSourt = new ReportDataSource("DataSet1", danhsach);
                this.rpvThongke.LocalReport.DataSources.Clear(); 
                this.rpvThongke.LocalReport.DataSources.Add(reportdataSourt);
                this.rpvThongke.RefreshReport();

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void export2Excel(DataGridView g, string duongDan, string tenTap)
        {

        }
        #endregion
        #region Excel
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ToExcel(dtgAccount, saveFileDialog1.FileName);
            }
        }
        private void ToExcel(DataGridView dataGridView1, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;

                workbook = excel.Workbooks.Add(Type.Missing);

                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet.Name = "HOC LAP TRINH C#";

                // export header
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }

                // export content
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }

                // save workbook
                workbook.SaveAs(fileName);
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Export successful.!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
        private void btnExcelDM_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ToExcel(dtgCategory, saveFileDialog1.FileName);
            }
        }

        private void btnTimkiemAccount_Click(object sender, EventArgs e)
        {
            dtgAccount.DataSource = SearchAccountByName(txtSearchAccount.Text);
        }

        private void dtgCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThemDM.Enabled = false;
            btnEditDM.Enabled = true;
            btnDeleteDM.Enabled = true;
            AddFoodCategory();
        }

        private void dtgTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button16.Enabled = false;
            btnDeleteTable.Enabled = true;
            btnUpdateTable.Enabled = true;
            AddTableBinding();
        }
        #endregion
        #region nhanvien
        private void btnAddNV_Click(object sender, EventArgs e)
        {
            string name = txtNameNV.Text;
            string gioitinh = cbbGioitinh.Text;
            string diachi = txtDiaChi.Text;
            string sdt =txtSDT.Text;

            AddNhanvien(name, DateTimeNV.Value, gioitinh,diachi,sdt);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btnEditNV_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDNV.Text);
            string name = txtNameNV.Text;
            string gioitinh = cbbGioitinh.Text;
            string diachi = txtDiaChi.Text;
            string sdt = txtSDT.Text;

            EditNhanVien(id,name, DateTimeNV.Value, gioitinh, diachi, sdt);
        }

        private void btnDeleteNV_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDNV.Text);
            DeleteNhanvien(id);
        }

        private void dtgNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDeleteNV.Enabled = true;
            btnEditNV.Enabled = true;
            btnAddNV.Enabled = false;
            AddNhanVien();
        }

        private void btnSearchNV_Click(object sender, EventArgs e)
        {
            dtgNhanvien.DataSource = SearchNhanVienByName(txtSeachNV.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ToExcel(dtgNhanvien, saveFileDialog1.FileName);
            }
        }

        private void btnResetNV_Click(object sender, EventArgs e)
        {
            btnDeleteNV.Enabled = false;
            btnEditNV.Enabled = false;
            btnAddNV.Enabled = true;
        }
        #endregion
    }
}
