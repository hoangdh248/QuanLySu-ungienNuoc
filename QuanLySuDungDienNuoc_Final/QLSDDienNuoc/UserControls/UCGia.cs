using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicDataModels.DataModels;
using LogicDataModels;

namespace QLSDDienNuoc.UserControls
{
    public partial class UCGia : baseUC
    {
        private User mUser;
        private Users_Roles mUR;
        private int tempSua = 8;
        private int tempXoa = 8;
        public UCGia(User oUser)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if (oUser == null)
            {
                mUser = new User();
            }
            else
            {
                mUser = oUser;
            }
            if (mUser.isAdmin == 2)
            {
                foreach (var item in new User_RoleLDM().GetElements(mUser.ID))
                {
                    if (item.Role.RoleName == "Quản lý đơn giá")
                    {
                        mUR = item;
                    }
                }
            }
        }
        public void ReloadDataGia()
        {
            try
            {
                dgvGia.DataSource = new PriceLDM().GetElements().Select(obj => new
                {
                    ID = obj.ID,
                    PriceName = obj.PriceName,
                    ElectricPrice = obj.ElectricPrice.Value.ToString("#,###"),
                    WaterPrice = obj.WaterPrice.Value.ToString("#,###"),
                    ThemBoi = obj.CreatedByID == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value).DisplayName + " " + obj.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = obj.ModifiedByID == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value).DisplayName + " " + obj.ModifiedDate.Value.ToString("dd/MM/yyyy")
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ReloadDataGia(List<Price> data)
        {
            try
            {
                dgvGia.DataSource = data.Select(obj => new
                {
                    ID = obj.ID,
                    PriceName = obj.PriceName,
                    ElectricPrice = obj.ElectricPrice.Value.ToString("#,###"),
                    WaterPrice = obj.WaterPrice.Value.ToString("#,###"),
                    ThemBoi = obj.CreatedByID == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value).DisplayName + " " + obj.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = obj.ModifiedByID == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value).DisplayName + " " + obj.ModifiedDate.Value.ToString("dd/MM/yyyy")
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void UCGia_Load(object sender, EventArgs e)
        {
            try
            {
                ReloadDataGia();
                dgvGia.Columns["ID"].Visible = false;
                dgvGia.Columns["PriceName"].HeaderText = "Giá áp dụng";
                dgvGia.Columns["ElectricPrice"].HeaderText = "Giá điện(đồng)";
                dgvGia.Columns["WaterPrice"].HeaderText = "Giá nước(đồng)";
                dgvGia.Columns["ThemBoi"].HeaderText = "Thêm bởi";
                dgvGia.Columns["SuaBoi"].HeaderText = "Sửa bởi";
                DataGridViewTextBoxColumn stt = new DataGridViewTextBoxColumn();
                stt.Name = "STT";
                stt.HeaderText = "STT";
                dgvGia.Columns.Insert(0, stt);
                int temp = 7;
                if (mUser.isAdmin == 1 || mUR.isEdit.Value)
                {
                    DataGridViewImageColumn EditPrice = new DataGridViewImageColumn();
                    EditPrice.Name = "EditPrice";
                    EditPrice.HeaderText = "Sửa";
                    EditPrice.Image = Properties.Resources.icons8_pencil_26;
                    dgvGia.Columns.Insert(temp, EditPrice);
                    dgvGia.Columns["EditPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvGia.Columns["EditPrice"].Width = 30;
                    temp++;
                    tempSua = temp;
                }
                if (mUser.isAdmin == 1 || mUR.isRemove.Value)
                {
                    DataGridViewImageColumn DeletePrice = new DataGridViewImageColumn();
                    DeletePrice.Name = "DeletePrice";
                    DeletePrice.HeaderText = "Xóa";
                    DeletePrice.Image = Properties.Resources.delete;
                    dgvGia.Columns.Insert(temp, DeletePrice);
                    dgvGia.Columns["DeletePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvGia.Columns["DeletePrice"].Width = 30;
                    temp++;
                    tempXoa = temp;
                }
                for (int i = 0; i < dgvGia.ColumnCount; i++)
                {

                    dgvGia.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvGia.Columns[i].HeaderCell.Style.ForeColor = Color.White;

                    dgvGia.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvGia.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                dgvGia.Columns["STT"].Width = 30;
                dgvGia.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
                dgvGia.EnableHeadersVisualStyles = false;
                if (mUser.isAdmin == 2 && mUR.isAdd.Value == false)
                {
                    btnThemGia.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvGia_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                dgvGia.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((mUser.isAdmin == 1 || mUR.isEdit.Value) && e.ColumnIndex == (tempSua - 7)) // nut sua
                {
                    Price EditPrice = new Price();
                    EditPrice.ID = int.Parse(dgvGia["ID", e.RowIndex].Value.ToString());
                    EditPrice.PriceName = dgvGia["PriceName", e.RowIndex].Value.ToString();
                    EditPrice.ElectricPrice = decimal.Parse(dgvGia["ElectricPrice", e.RowIndex].Value.ToString().Replace(".", ""));
                    EditPrice.WaterPrice = decimal.Parse(dgvGia["WaterPrice", e.RowIndex].Value.ToString().Replace(".", ""));
                    ThemGia mThemGia = new ThemGia(mUser, EditPrice);
                    mThemGia.mUCGia = this;
                    this.Parent.Parent.Enabled = false;
                    mThemGia.Show();
                }
                if ((mUser.isAdmin == 1 || mUR.isRemove.Value) && e.ColumnIndex == (tempXoa - 7)) // click nút xóa
                {
                    var result = MessageBox.Show($"Bạn có muốn xóa {dgvGia["PriceName", e.RowIndex].Value.ToString().ToUpper()}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (new PriceLDM().Delete(int.Parse(dgvGia["ID", e.RowIndex].Value.ToString())))
                        {
                            ReloadDataGia();
                            MessageBox.Show("Xóa thành công");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnThemGia_Click(object sender, EventArgs e)
        {
            try
            {
                ThemGia mThemGia = new ThemGia(mUser);
                mThemGia.mUCGia = this;
                this.Parent.Parent.Enabled = false;
                mThemGia.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text.Trim() == "")
                {
                    ReloadDataGia();
                }
                else
                {
                    ReloadDataGia(new PriceLDM().SearchElements(txtTimKiem.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public override void ReSizeDataGridView(DataGridViewAutoSizeColumnMode type)
        {
            try
            {
                for (int i = 0; i < dgvGia.ColumnCount; i++)
                {
                    //auto fill column
                    dgvGia.Columns[i].AutoSizeMode = type;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
