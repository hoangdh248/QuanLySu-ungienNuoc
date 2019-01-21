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
    public partial class ThemNguoiDung : Form
    {
        private User mUser;
        public UCNguoiDung UCNguoiDung { get; set; }
        public ThemNguoiDung(User oUser)
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            try
            {
                UCNguoiDung.Enabled = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ThemNguoiDung_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                UCNguoiDung.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ThemNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
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
                erpThemNguoiDung.Clear();
                if (string.IsNullOrEmpty(txtTenDangNhap.Text.Trim()))
                {
                    erpThemNguoiDung.SetError(txtTenDangNhap, "Vui lòng điền trường này!");
                    txtTenDangNhap.Focus();
                }
                else
                {
                    if (new UserLDM().isDuplicateUserName(txtTenDangNhap.Text.Trim()))
                    {
                        erpThemNguoiDung.SetError(txtTenDangNhap, "Tên đăng nhập đã tồn tại!");
                        txtTenDangNhap.Focus();
                    }
                    else
                    {
                        {
                            newUser.UserName = txtTenDangNhap.Text.Trim();
                            if (string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
                            {
                                erpThemNguoiDung.SetError(txtMatKhau, "Vui lòng điền trường này!");
                                txtMatKhau.Focus();
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(txtNhapLaiMatKhau.Text.Trim()))
                                {
                                    erpThemNguoiDung.SetError(txtNhapLaiMatKhau, "Vui lòng điền trường này!");
                                    txtNhapLaiMatKhau.Focus();
                                }
                                else
                                {
                                    if (!txtMatKhau.Text.Trim().Equals(txtNhapLaiMatKhau.Text.Trim()))
                                    {
                                        erpThemNguoiDung.SetError(txtNhapLaiMatKhau, "Nhập lại mật khẩu không khớp!");
                                        txtNhapLaiMatKhau.Focus();
                                    }
                                    else
                                    {
                                        newUser.Password = Commons.En_Decrypt.Encrypt(txtMatKhau.Text.Trim());
                                        if (string.IsNullOrEmpty(txtTenHienThi.Text.Trim()))
                                        {
                                            erpThemNguoiDung.SetError(txtTenHienThi, "Vui lòng điền trường này!");
                                            txtTenHienThi.Focus();
                                        }
                                        else
                                        {
                                            newUser.DisplayName = txtTenHienThi.Text.Trim();
                                            if (dtpNgaySinh.Value == null)
                                            {
                                                erpThemNguoiDung.SetError(dtpNgaySinh, "Vui lòng điền trường này!");
                                                dtpNgaySinh.Focus();
                                            }
                                            else
                                            {
                                                newUser.DateOfBirth = dtpNgaySinh.Value;
                                                if (!rdoNam.Checked && !rdoNu.Checked)
                                                {
                                                    erpThemNguoiDung.SetError(rdoNu, "Vui lòng điền trường này!");
                                                }
                                                else
                                                {
                                                    newUser.Gender = rdoNam.Checked;
                                                    if (string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                                                    {
                                                        erpThemNguoiDung.SetError(txtSoDienThoai, "Vui lòng điền trường này!");
                                                        txtSoDienThoai.Focus();
                                                    }
                                                    else
                                                    {
                                                        newUser.Phone = txtSoDienThoai.Text.Trim();
                                                        if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                                                        {
                                                            erpThemNguoiDung.SetError(txtEmail, "Vui lòng điền trường này!");
                                                            txtEmail.Focus();
                                                        }
                                                        else
                                                        {
                                                            Regex mRegxExpression;
                                                            mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                                                            if (!mRegxExpression.IsMatch(txtEmail.Text.Trim()))
                                                            {
                                                                erpThemNguoiDung.SetError(txtEmail, "Địa chỉ email không hợp lệ!");
                                                                txtEmail.Focus();

                                                            }
                                                            else
                                                            {
                                                                newUser.Email = txtEmail.Text.Trim();
                                                                if (string.IsNullOrEmpty(txtDiaChi.Text.Trim()))
                                                                {
                                                                    erpThemNguoiDung.SetError(txtDiaChi, "Vui lòng nhập trường này!");
                                                                    txtDiaChi.Focus();
                                                                }
                                                                else
                                                                {
                                                                    newUser.Address = txtDiaChi.Text.Trim();
                                                                    newUser.CreatedByID = mUser.ID;
                                                                    newUser.CreatedDate = DateTime.Now.Date;
                                                                    newUser.isAdmin = 2;
                                                                    newUser.isDelete = false;
                                                                    newUser.isPay = chkThanhToan.Checked;
                                                                    User UserResult = new UserLDM().Insert(newUser);
                                                                    if (UserResult != null)
                                                                    {
                                                                        MessageBox.Show($"Thêm người dùng thành công!");
                                                                        this.UCNguoiDung.ReloadDataNguoiDung();
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Thêm người dùng thất bại!");
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
    }
}
