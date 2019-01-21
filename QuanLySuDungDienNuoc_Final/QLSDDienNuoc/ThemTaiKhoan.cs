using LogicDataModels;
using LogicDataModels.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSDDienNuoc
{
    public partial class ThemTaiKhoan : Form
    {
        private User mUser;
        public ThemTaiKhoan(User oUser)
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            User newUser = new User();
            erpThemTaiKhoan.Clear();
            if (string.IsNullOrEmpty(txtTenDangNhap.Text.Trim()))
            {
                erpThemTaiKhoan.SetError(txtTenDangNhap, "Vui lòng điền trường này!");
                txtTenDangNhap.Focus();
            }
            else
            {
                newUser.UserName = txtTenDangNhap.Text;
                if (string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
                {
                    erpThemTaiKhoan.SetError(txtMatKhau, "Vui lòng điền trường này!");
                    txtMatKhau.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtNhapLaiMatKhau.Text.Trim()))
                    {
                        erpThemTaiKhoan.SetError(txtNhapLaiMatKhau, "Vui lòng điền trường này!");
                        txtNhapLaiMatKhau.Focus();
                    }
                    else
                    {
                        if (!txtMatKhau.Text.Equals(txtNhapLaiMatKhau.Text))
                        {
                            erpThemTaiKhoan.SetError(txtNhapLaiMatKhau, "Nhập lại mật khẩu không khớp!");
                            txtNhapLaiMatKhau.Focus();
                        }
                        else
                        {
                            newUser.Password = Commons.En_Decrypt.Encrypt(txtMatKhau.Text);
                            if (string.IsNullOrEmpty(txtTenHienThi.Text.Trim()))
                            {
                                erpThemTaiKhoan.SetError(txtTenHienThi, "Vui lòng điền trường này!");
                                txtTenHienThi.Focus();
                            }
                            else
                            {
                                newUser.DisplayName = txtTenHienThi.Text;
                                if (dtpNgaySinh.Value == null)
                                {
                                    erpThemTaiKhoan.SetError(dtpNgaySinh, "Vui lòng điền trường này!");
                                    dtpNgaySinh.Focus();
                                }
                                else
                                {
                                    newUser.DateOfBirth = dtpNgaySinh.Value;
                                    if (!rdoNam.Checked && !rdoNu.Checked)
                                    {
                                        erpThemTaiKhoan.SetError(rdoNam, "Vui lòng điền trường này!");
                                        erpThemTaiKhoan.SetError(rdoNu, "Vui lòng điền trường này!");
                                        rdoNam.Focus();
                                    }
                                    else
                                    {
                                        newUser.Gender = rdoNam.Checked;
                                        if (string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                                        {
                                            erpThemTaiKhoan.SetError(txtSoDienThoai, "Vui lòng điền trường này!");
                                            txtSoDienThoai.Focus();
                                        }
                                        else
                                        {
                                            newUser.Phone = txtSoDienThoai.Text;
                                            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                                            {
                                                erpThemTaiKhoan.SetError(txtEmail, "Vui lòng điền trường này!");
                                                txtEmail.Focus();


                                            }
                                            else
                                            {
                                                Regex mRegxExpression;
                                                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                                                if (!mRegxExpression.IsMatch(txtEmail.Text.Trim()))
                                                {
                                                    erpThemTaiKhoan.SetError(txtEmail, "Địa chỉ email không hợp lệ!");
                                                    txtEmail.Focus();

                                                }
                                                else
                                                {
                                                    newUser.Email = txtEmail.Text.Trim();
                                                    if (string.IsNullOrEmpty(txtDiaChi.Text.Trim()))
                                                    {
                                                        erpThemTaiKhoan.SetError(txtDiaChi, "Vui lòng điền trường này!");
                                                        txtDiaChi.Focus();
                                                    }
                                                    else
                                                    {
                                                        newUser.Address = txtDiaChi.Text.Trim();
                                                        newUser.CreatedByID = mUser.ID;
                                                        newUser.CreatedDate = DateTime.Now;
                                                        User result = new UserLDM().Insert(newUser);
                                                        if (result != null)
                                                        {
                                                            MessageBox.Show("Thêm tài khoản thành công!");
                                                            ThemKhachHang mThemKhachHang = new ThemKhachHang(mUser, result.ID);
                                                            mThemKhachHang.Show();
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Thêm tài khoản thất bại!");
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
