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
    public partial class UCcuaSoChinh : baseUC
    {
        private User mUser;
        private frmMain frmSource;
        private Users_Roles mUR;
        private int tempSua = 15;
        private int tempXoa = 15;
        private int tempQLTT = 15;
        public UCcuaSoChinh(User oUser, frmMain frm)
        {
            InitializeComponent();
            frmSource = frm;
            frmSource.currentUC = this;
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
                    if (item.Role.RoleName == "Quản lý khách hàng")
                    {
                        mUR = item;
                    }
                }
            }

        }

        private void UCcuaSoChinh_Load(object sender, EventArgs e)
        {

            try
            {
                ReLoadDataKhachHang();
                dgvKhachHang.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
                dgvKhachHang.Columns["Gender"].HeaderText = "Giới tính";
                dgvKhachHang.Columns["DateOfBirth"].HeaderText = "Ngày sinh";
                dgvKhachHang.Columns["PassPortID"].HeaderText = "Số CMT";
                dgvKhachHang.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
                dgvKhachHang.Columns["EmailKH"].HeaderText = "Email";
                dgvKhachHang.Columns["AddressKH"].HeaderText = "Địa chỉ";
                dgvKhachHang.Columns["Price"].HeaderText = "Giá áp dụng";
                dgvKhachHang.Columns["ThemBoi"].HeaderText = "Thêm bởi";
                dgvKhachHang.Columns["SuaBoi"].HeaderText = "Sửa bởi";
                dgvKhachHang.Columns["MaKhachHang"].Visible = false;
                dgvKhachHang.Columns["PriceID"].Visible = false;
                dgvKhachHang.Columns["UserID"].Visible = false;
                // set headertext name

                DataGridViewTextBoxColumn stt = new DataGridViewTextBoxColumn();
                stt.Name = "STT";
                stt.HeaderText = "STT";
                dgvKhachHang.Columns.Insert(0, stt);
                dgvKhachHang.Columns["STT"].Width = 30;

                int temp = 14;
                if (mUser.isAdmin == 1 || new User_RoleLDM().GetElements(mUser.ID).Where(x => x.Role.RoleName == "Quản lý tiêu thụ").First().isView.Value)
                {
                    DataGridViewButtonColumn QLTT = new DataGridViewButtonColumn();
                    QLTT.HeaderText = "Quản lý tiêu thụ";
                    QLTT.Name = "QuanLyTieuThu";
                    QLTT.Text = "QL Tiêu thụ";
                    QLTT.UseColumnTextForButtonValue = true;
                    dgvKhachHang.Columns.Insert(temp, QLTT);
                    dgvKhachHang.Columns["QuanLyTieuThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvKhachHang.Columns["QuanLyTieuThu"].Width = 70;
                    temp++;
                    tempQLTT = temp;
                }

                if (mUser.isAdmin == 1 || mUR.isEdit.Value)
                {
                    DataGridViewImageColumn EditKH = new DataGridViewImageColumn();
                    EditKH.HeaderText = "Sửa";
                    EditKH.Name = "EditKH";
                    EditKH.Image = Properties.Resources.icons8_pencil_26;
                    dgvKhachHang.Columns.Insert(temp, EditKH);
                    dgvKhachHang.Columns["EditKH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvKhachHang.Columns["EditKH"].Width = 30;
                    temp++;
                    tempSua = temp;
                }

                if (mUser.isAdmin == 1 || mUR.isRemove.Value)
                {
                    DataGridViewImageColumn DeleteKH = new DataGridViewImageColumn();
                    DeleteKH.HeaderText = "Xóa";
                    DeleteKH.Name = "DeleteKH";
                    DeleteKH.Image = Properties.Resources.delete;
                    dgvKhachHang.Columns.Insert(temp, DeleteKH);
                    dgvKhachHang.Columns["DeleteKH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvKhachHang.Columns["DeleteKH"].Width = 30;
                    temp++;
                    tempXoa = temp;
                }





                for (int i = 0; i < dgvKhachHang.Columns.Count; i++)
                {
                    // set size display
                    dgvKhachHang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    // set text color header
                    dgvKhachHang.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                    //set alignment middle
                    dgvKhachHang.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvKhachHang.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                dgvKhachHang.Columns["Gender"].Width = 30;
                //set backgd color header
                dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
                dgvKhachHang.EnableHeadersVisualStyles = false;
                if(mUser.isAdmin == 2 && mUR.isAdd.Value == false)
                {
                    btnThemKhachHang.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void dgvKhachHang_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                dgvKhachHang.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((mUser.isAdmin == 1 || mUR.isRemove.Value) && e.ColumnIndex == (tempXoa - 14)) // click nut xoa
                {
                    var result = MessageBox.Show($" Dữ liệu về tài khoản ,tiêu thụ cũng sẽ bị xóa \n Bạn có muốn xóa khách hàng {dgvKhachHang["TenKhachHang", e.RowIndex].Value.ToString().ToUpper()}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (new KhachHangLDM().Delete(int.Parse(dgvKhachHang["MaKhachHang", e.RowIndex].Value.ToString())))
                        {
                            if (new UserLDM().Delete(int.Parse(dgvKhachHang["UserID", e.RowIndex].Value.ToString())))
                            {
                                var lstTieuThu = new TieuThuLDM().GetElements(int.Parse(dgvKhachHang["MaKhachHang", e.RowIndex].Value.ToString()));
                                var isvalid = true;
                                foreach (var item in lstTieuThu)
                                {

                                    if (new TieuThuLDM().Delete(item.ID) == false)
                                    {
                                        isvalid = false;
                                        break;
                                    }
                                }
                                if (isvalid == true)
                                {
                                    ReLoadDataKhachHang();
                                    MessageBox.Show("Xóa thành công!");
                                }
                                else
                                {
                                    MessageBox.Show("Xóa thất bại!");
                                }

                            }

                        }
                    }

                }
                if ((mUser.isAdmin == 1 || mUR.isEdit.Value) && e.ColumnIndex == (tempSua - 14) ) // click nut sua
                {
                    Customer objCustomer = new Customer();
                    objCustomer.User = new User();
                    objCustomer.UserID = int.Parse(dgvKhachHang["UserID", e.RowIndex].Value.ToString() == null ? "-1" : dgvKhachHang["UserID", e.RowIndex].Value.ToString());
                    objCustomer.PriceID = int.Parse(dgvKhachHang["PriceID", e.RowIndex].Value.ToString() == null ? "-1" : dgvKhachHang["PriceID", e.RowIndex].Value.ToString());
                    objCustomer.ID = int.Parse(dgvKhachHang["MaKhachHang", e.RowIndex].Value.ToString());
                    objCustomer.PassportID = dgvKhachHang["PassPortID", e.RowIndex].Value.ToString();
                    objCustomer.User.DisplayName = dgvKhachHang["TenKhachHang", e.RowIndex].Value.ToString();
                    objCustomer.User.Gender = dgvKhachHang["Gender", e.RowIndex].Value.ToString() == "Nam" ? true : false;
                    objCustomer.User.DateOfBirth = DateTime.ParseExact(dgvKhachHang["DateOfBirth", e.RowIndex].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objCustomer.User.Phone = dgvKhachHang["PhoneNumber", e.RowIndex].Value.ToString();
                    objCustomer.User.Email = dgvKhachHang["EmailKH", e.RowIndex].Value.ToString();
                    objCustomer.User.Address = dgvKhachHang["AddressKH", e.RowIndex].Value.ToString();
                    SuaKhachHang mSuaKhachHang = new SuaKhachHang(mUser, objCustomer);
                    this.Parent.Parent.Enabled = false;
                    mSuaKhachHang.UCCuaSoChinh = this;
                    mSuaKhachHang.Show();
                }
                if ((mUser.isAdmin == 1 || new User_RoleLDM().GetElements(mUser.ID).Where(x => x.Role.RoleName == "Quản lý tiêu thụ").First().isView.Value) && e.ColumnIndex == (tempQLTT - 14)) // click nut QL Tieu thu
                {
                    UCquanLyTieuThu mUCQuanLyTieuThu = new UCquanLyTieuThu(mUser, int.Parse(dgvKhachHang["MaKhachHang", e.RowIndex].Value.ToString()), frmSource);
                    mUCQuanLyTieuThu.mUCCuaSoChinh = this;
                    this.Parent.Controls.Add(mUCQuanLyTieuThu);
                    this.Parent.Controls.Remove(this);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void ReLoadDataKhachHang()
        {
            try
            {
                dgvKhachHang.DataSource = new KhachHangLDM().GetElements().Select(obj => new
                {
                    MaKhachHang = obj.ID,
                    TenKhachHang = obj.User.DisplayName,
                    Gender = obj.User.Gender == true ? "Nam" : "Nữ",
                    DateOfBirth = obj.User.DateOfBirth.Value.ToString("dd/MM/yyyy"),
                    PassPortID = obj.PassportID,
                    PhoneNumber = obj.User.Phone,
                    EmailKH = obj.User.Email,
                    AddressKH = obj.User.Address,
                    PriceID = obj.PriceID.HasValue ? obj.PriceID.Value : -1,
                    UserID = obj.UserID.HasValue ? obj.UserID.Value : -1,
                    Price = new PriceLDM().GetElement(obj.PriceID.HasValue ? obj.PriceID.Value : -1) != null ? new PriceLDM().GetElement(obj.PriceID.HasValue ? obj.PriceID.Value : -1).PriceName : "",
                    ThemBoi = obj.CreatedByID == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value).DisplayName + " " + obj.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = obj.ModifiedByID == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value).DisplayName + " " + obj.ModifiedDate.Value.ToString("dd/MM/yyyy")
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void ReLoadDataKhachHang(List<Customer> data)
        {
            try
            {
                dgvKhachHang.DataSource = data.Select(obj => new
                {
                    MaKhachHang = obj.ID,
                    TenKhachHang = obj.User.DisplayName,
                    Gender = obj.User.Gender == true ? "Nam" : "Nữ",
                    DateOfBirth = obj.User.DateOfBirth.Value.ToString("dd/MM/yyyy"),
                    PassPortID = obj.PassportID,
                    PhoneNumber = obj.User.Phone,
                    EmailKH = obj.User.Email,
                    AddressKH = obj.User.Address,
                    PriceID = obj.PriceID.HasValue ? obj.PriceID.Value : -1,
                    UserID = obj.UserID.HasValue ? obj.UserID.Value : -1,
                    Price = new PriceLDM().GetElement(obj.PriceID.HasValue ? obj.PriceID.Value : -1) != null ? new PriceLDM().GetElement(obj.PriceID.HasValue ? obj.PriceID.Value : -1).PriceName : "",
                    ThemBoi = obj.CreatedByID == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value).DisplayName + " " + obj.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = obj.ModifiedByID == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value).DisplayName + " " + obj.ModifiedDate.Value.ToString("dd/MM/yyyy"),
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void dgvKhachHang_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        }
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            try
            {
                ThemKhachHang mThemTaiKhoan = new ThemKhachHang(mUser);
                mThemTaiKhoan.UCCuaSoChinh = this;
                this.Parent.Parent.Enabled = false;
                mThemTaiKhoan.Show();
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
                    ReLoadDataKhachHang();
                }
                else
                {
                    ReLoadDataKhachHang(new KhachHangLDM().SearchElements(txtTimKiem.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public override void ReSizeDataGridView(DataGridViewAutoSizeColumnMode type)
        {
            try
            {
                for (int i = 0; i < dgvKhachHang.Columns.Count; i++)
                {
                    // set size display
                    dgvKhachHang.Columns[i].AutoSizeMode = type;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}

