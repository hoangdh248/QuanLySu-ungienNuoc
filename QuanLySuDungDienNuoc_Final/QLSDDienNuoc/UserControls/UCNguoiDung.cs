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
using System.Globalization;

namespace QLSDDienNuoc.UserControls
{
    public partial class UCNguoiDung : baseUC
    {
        private User mUser;
        private Users_Roles mUR;
        private int tempSua = 12;
        private int tempXoa = 12;
        private int tempPQ = 12;
        public UCNguoiDung(User oUser)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if (oUser != null)
            {
                mUser = oUser;
            }
            else
            {
                mUser = new User();
            }
            if (mUser.isAdmin == 2)
            {
                foreach (var item in new User_RoleLDM().GetElements(mUser.ID))
                {
                    if (item.Role.RoleName == "Quản lý nhân viên")
                    {
                        mUR = item;
                    }
                }
            }
        }
        public void ReloadDataNguoiDung()
        {
            try
            {
                dgvNguoiDung.DataSource = new UserLDM().GetElements().Select(obj => new
                {
                    ID = obj.ID,
                    DisplayName = obj.DisplayName,
                    DateOfBirth = obj.DateOfBirth.Value.ToString("dd/MM/yyyy"),
                    Gender = obj.Gender == true ? "Nam" : "Nữ",
                    Phone = obj.Phone,
                    Email = obj.Email,
                    Address = obj.Address,
                    QuyenThanhToan = obj.isPay.Value ? "Có" : "Không",
                    ThemBoi = obj.CreatedByID == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value).DisplayName + " " + obj.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = obj.ModifiedByID == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value).DisplayName + " " + obj.ModifiedDate.Value.ToString("dd/MM/yyyy"),
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void ReloadDataNguoiDung(List<User> data)
        {
            try
            {
                dgvNguoiDung.DataSource = data.Select(obj => new
                {
                    ID = obj.ID,
                    DisplayName = obj.DisplayName,
                    DateOfBirth = obj.DateOfBirth.Value.ToString("dd/MM/yyyy"),
                    Gender = obj.Gender == true ? "Nam" : "Nữ",
                    Phone = obj.Phone,
                    Email = obj.Email,
                    Address = obj.Address,
                    QuyenThanhToan = obj.isPay.Value ? "Có" : "Không",
                    ThemBoi = obj.CreatedByID == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value).DisplayName + " " + obj.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = obj.ModifiedByID == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value).DisplayName + " " + obj.ModifiedDate.Value.ToString("dd/MM/yyyy"),
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void UCNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                ReloadDataNguoiDung();
                dgvNguoiDung.Columns["ID"].Visible = false;
                dgvNguoiDung.Columns["DisplayName"].HeaderText = "Tên người dùng";
                dgvNguoiDung.Columns["Gender"].HeaderText = "Giới tính";
                dgvNguoiDung.Columns["Phone"].HeaderText = "Số điện thoại";
                dgvNguoiDung.Columns["Email"].HeaderText = "Email";
                dgvNguoiDung.Columns["DateOfBirth"].HeaderText = "Ngày sinh";
                dgvNguoiDung.Columns["Address"].HeaderText = "Địa chỉ";
                dgvNguoiDung.Columns["ThemBoi"].HeaderText = "Thêm bởi";
                dgvNguoiDung.Columns["SuaBoi"].HeaderText = "Sửa bởi";
                dgvNguoiDung.Columns["QuyenThanhToan"].HeaderText = "Được phép thanh toán tiêu thụ";
                ///

                // thêm cột mới ở đầu
                DataGridViewTextBoxColumn stt = new DataGridViewTextBoxColumn();
                stt.Name = "STT";
                stt.HeaderText = "STT";
                dgvNguoiDung.Columns.Insert(0, stt);

                int temp = 11;
                if(mUser.isAdmin == 1)
                {
                    DataGridViewButtonColumn PhanQuyen = new DataGridViewButtonColumn();
                    PhanQuyen.HeaderText = "Phân quyền";
                    PhanQuyen.Name = "PhanQuyen";
                    PhanQuyen.Text = "Phân quyền";
                    PhanQuyen.UseColumnTextForButtonValue = true;
                    dgvNguoiDung.Columns.Insert(temp, PhanQuyen);
                    dgvNguoiDung.Columns["PhanQuyen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvNguoiDung.Columns["PhanQuyen"].Width = 30;
                    temp++;
                    tempPQ = temp;
                }

                

                // thêm cột và hình ảnh cho từng cột 
                if(mUser.isAdmin == 1 || mUR.isEdit.Value)
                {
                    DataGridViewImageColumn EditUser = new DataGridViewImageColumn();
                    EditUser.Name = "EditUser";
                    EditUser.HeaderText = "Sửa";
                    EditUser.Image = Properties.Resources.icons8_pencil_26;
                    dgvNguoiDung.Columns.Insert(temp, EditUser);
                    dgvNguoiDung.Columns["EditUser"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvNguoiDung.Columns["EditUser"].Width = 30;
                    temp++;
                    tempSua = temp;
                }
                

                //
                if(mUser.isAdmin == 1 || mUR.isRemove.Value)
                {
                    DataGridViewImageColumn DeleteUser = new DataGridViewImageColumn();
                    DeleteUser.Name = "DeleteUser";
                    DeleteUser.HeaderText = "Xóa";
                    DeleteUser.Image = Properties.Resources.delete;
                    dgvNguoiDung.Columns.Insert(temp, DeleteUser);
                    dgvNguoiDung.Columns["DeleteUser"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvNguoiDung.Columns["DeleteUser"].Width = 30;
                    temp++;
                    tempXoa = temp;
                }
                


                for (int i = 0; i < dgvNguoiDung.Columns.Count; i++)
                {
                    // auto fill columns
                    dgvNguoiDung.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    // set text color 
                    dgvNguoiDung.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                    dgvNguoiDung.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvNguoiDung.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                dgvNguoiDung.Columns["STT"].Width = 30;
                
                
               

                // set color dgv
                dgvNguoiDung.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
                dgvNguoiDung.EnableHeadersVisualStyles = false;
                if(mUser.isAdmin == 2 && mUR.isAdd.Value == false)
                {
                    btnThemNguoiDung.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void dgvNguoiDung_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                dgvNguoiDung.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((mUser.isAdmin == 1 || mUR.isEdit.Value) && e.ColumnIndex == (tempSua - 11)) // click nut sua
                {
                    User objUser = new User();
                    objUser.ID = int.Parse(dgvNguoiDung["ID", e.RowIndex].Value.ToString());
                    objUser.DisplayName = dgvNguoiDung["DisplayName", e.RowIndex].Value.ToString();
                    objUser.Gender = dgvNguoiDung["Gender", e.RowIndex].Value.ToString() == "Nam" ? true : false;
                    objUser.isPay = dgvNguoiDung["QuyenThanhToan", e.RowIndex].Value.ToString() == "Có" ? true : false;
                    objUser.Phone = dgvNguoiDung["Phone", e.RowIndex].Value.ToString();
                    objUser.Email = dgvNguoiDung["Email", e.RowIndex].Value.ToString();
                    objUser.DateOfBirth = DateTime.ParseExact(dgvNguoiDung["DateOfBirth", e.RowIndex].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objUser.Address = dgvNguoiDung["Address", e.RowIndex].Value.ToString();
                    SuaNguoiDung mSuaNguoiDung = new SuaNguoiDung(mUser, objUser);
                    this.Parent.Parent.Enabled = false;
                    mSuaNguoiDung.UCNguoiDung = this;
                    mSuaNguoiDung.Show();
                }
                if ((mUser.isAdmin == 1 || mUR.isRemove.Value) && e.ColumnIndex == (tempXoa - 11))
                {
                    var result = MessageBox.Show($"Bạn có muốn xóa{dgvNguoiDung["DisplayName", e.RowIndex].Value.ToString().ToUpper()}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (new UserLDM().Delete(int.Parse(dgvNguoiDung["ID", e.RowIndex].Value.ToString())))
                        {
                            ReloadDataNguoiDung();
                            MessageBox.Show("Xóa thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!");
                        }
                    }
                }
                if ((mUser.isAdmin == 1) && e.ColumnIndex == (tempPQ - 11))
                {
                    PhanQuyen mPhanQuyen = new PhanQuyen(mUser, int.Parse(dgvNguoiDung["ID", e.RowIndex].Value.ToString()));
                    mPhanQuyen.mUC = this;
                    this.Parent.Parent.Enabled = false;
                    mPhanQuyen.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnThemNguoiDung_Click(object sender, EventArgs e)
        {
            try
            {
                ThemNguoiDung mThemNguoiDung = new ThemNguoiDung(mUser);
                mThemNguoiDung.UCNguoiDung = this;
                this.Parent.Parent.Enabled = true;
                mThemNguoiDung.Show();
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
                    ReloadDataNguoiDung();
                else
                {
                    ReloadDataNguoiDung(new UserLDM().SearchElements(txtTimKiem.Text.Trim()));
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
                for (int i = 0; i < dgvNguoiDung.Columns.Count; i++)
                {
                    // auto fill columns
                    dgvNguoiDung.Columns[i].AutoSizeMode = type;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
