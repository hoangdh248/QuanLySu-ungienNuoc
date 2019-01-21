using LogicDataModels;
using LogicDataModels.DataModels;
using QLSDDienNuoc.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSDDienNuoc
{
    public partial class SuaNguoiDung : Form
    {
        private User mUser;
        public UCNguoiDung UCNguoiDung { get; set; }
        private User mEditUser;
        private bool isUpdateProfile;
        public SuaNguoiDung(User oUser, User editUser, bool isUpdateProfile = false)
        {
            InitializeComponent();
            if (oUser != null)
            {
                mUser = oUser;
            }
            else
            {
                mUser = new User();
            }
            mEditUser = editUser;
            this.isUpdateProfile = isUpdateProfile;
            if (isUpdateProfile)
            {
                chkThanhToan.Visible = false;
            }
        }
        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                User newUser = new User();
                erpSuaNguoiDung.Clear();
                if (string.IsNullOrEmpty(txtTenHienThi.Text.Trim()))
                {
                    erpSuaNguoiDung.SetError(txtTenHienThi, "Vui lòng điền trường hợp này!");
                    txtTenHienThi.Focus();
                }
                else
                {
                    newUser.DisplayName = txtTenHienThi.Text.Trim();
                    if (dtpNgaySinh.Value == null)
                    {
                        erpSuaNguoiDung.SetError(dtpNgaySinh, "Vui lòng điền trường này!");
                        dtpNgaySinh.Focus();
                    }
                    else
                    {
                        newUser.DateOfBirth = dtpNgaySinh.Value;
                        if (!rdoNam.Checked && !rdoNu.Checked)
                        {
                            erpSuaNguoiDung.SetError(rdoNu, "Vui lòng điền trường này!");
                        }
                        else
                        {
                            newUser.Gender = rdoNam.Checked;
                            if (string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                            {
                                erpSuaNguoiDung.SetError(txtSoDienThoai, "Vui lòng điền trường này!");
                                txtSoDienThoai.Focus();
                            }
                            else
                            {
                                newUser.Phone = txtSoDienThoai.Text.Trim();
                                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                                {
                                    erpSuaNguoiDung.SetError(txtEmail, "Vui lòng điền trường này!");
                                    txtEmail.Focus();
                                }
                                else
                                {
                                    Regex mRegxExpression;
                                    mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                                    if (!mRegxExpression.IsMatch(txtEmail.Text.Trim()))
                                    {
                                        erpSuaNguoiDung.SetError(txtEmail, "Địa chỉ email không hợp lệ!");
                                        txtEmail.Focus();
                                    }
                                    else
                                    {
                                        newUser.Email = txtEmail.Text.Trim();
                                        if (string.IsNullOrEmpty(txtDiaChi.Text.Trim()))
                                        {
                                            erpSuaNguoiDung.SetError(txtDiaChi, "Vui lòng điền trường này!");
                                            txtDiaChi.Focus();
                                        }
                                        else
                                        {
                                            newUser.Address = txtDiaChi.Text.Trim();
                                            newUser.ModifiedByID = mUser.ID;
                                            newUser.ID = mEditUser.ID;
                                            newUser.ModifiedDate = DateTime.Now.Date;
                                            newUser.isPay = chkThanhToan.Checked;
                                            newUser.isAdmin = isUpdateProfile ? mUser.isAdmin : 2;
                                            if (new UserLDM().Update(newUser))
                                            {
                                                MessageBox.Show("Cập nhật thông tin người dùng thành công!");
                                                if (!isUpdateProfile)
                                                    this.UCNguoiDung.ReloadDataNguoiDung();
                                                else
                                                {
                                                    mUser = new UserLDM().GetElement(mUser.ID);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Cập nhật thông tin người dùng thất bại!");
                                            }
                                            this.Close();
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SuaNguoiDung_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!isUpdateProfile)
                    UCNguoiDung.Parent.Parent.Enabled = true;
                else
                    Owner.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SuaNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                txtTenHienThi.Text = mEditUser.DisplayName;
                dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
                dtpNgaySinh.Value = mEditUser.DateOfBirth == null ? DateTime.Now.Date : mEditUser.DateOfBirth.Value;
                if(mEditUser.Gender != null)
                {
                    rdoNam.Checked = mEditUser.Gender.Value ? true : false;
                    rdoNu.Checked = mEditUser.Gender.Value == false ? true : false;
                }
                txtSoDienThoai.Text = mEditUser.Phone ?? "";
                txtEmail.Text = mEditUser.Email ?? "";
                txtDiaChi.Text = mEditUser.Address ?? "";
                if(mEditUser.isPay != null)
                {
                    chkThanhToan.Checked = mEditUser.isPay.Value ? true : false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblDoiMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if(isUpdateProfile)
                {
                    DoiMatKhau obj = new DoiMatKhau(mUser);
                    obj.Owner = this;
                    this.Enabled = false;
                    obj.Show();
                }
                else
                {
                    DoiMatKhau obj = new DoiMatKhau(mUser, new UserLDM().GetElement(mEditUser.ID));
                    obj.Owner = this;
                    this.Enabled = false;
                    obj.Show();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
