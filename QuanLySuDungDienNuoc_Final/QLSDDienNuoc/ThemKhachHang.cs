using LogicDataModels;
using LogicDataModels.DataModels;
using QLSDDienNuoc.UserControls;
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
    public partial class ThemKhachHang : Form
    {
        private User mUser;
        public UCcuaSoChinh UCCuaSoChinh { get; set; }
        public ThemKhachHang(User oUser)
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
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                Customer newCustomer = new Customer();
                newCustomer.User = new User();
                erpThemKhachHang.Clear();
                if (string.IsNullOrEmpty(txtTenDangNhap.Text.Trim()))
                {
                    erpThemKhachHang.SetError(txtTenDangNhap, "Vui lòng điền trường này!");
                    txtTenDangNhap.Focus();
                }
                else
                {
                    if (new UserLDM().isDuplicateUserName(txtTenDangNhap.Text.Trim()))
                    {
                        erpThemKhachHang.SetError(txtTenDangNhap, "Tên đăng nhập đã tồn tại!");
                        txtTenDangNhap.Focus();
                    }
                    else
                    {
                        newCustomer.User.UserName = txtTenDangNhap.Text;
                        if (string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
                        {
                            erpThemKhachHang.SetError(txtMatKhau, "Vui lòng điền trường này!");
                            txtMatKhau.Focus();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtNhapLaiMatKhau.Text.Trim()))
                            {
                                erpThemKhachHang.SetError(txtNhapLaiMatKhau, "Vui lòng điền trường này!");
                                txtNhapLaiMatKhau.Focus();
                            }
                            else
                            {
                                if (!txtMatKhau.Text.Equals(txtNhapLaiMatKhau.Text))
                                {
                                    erpThemKhachHang.SetError(txtNhapLaiMatKhau, "Nhập lại mật khẩu không khớp!");
                                    txtNhapLaiMatKhau.Focus();
                                }
                                else
                                {
                                    newCustomer.User.Password = Commons.En_Decrypt.Encrypt(txtMatKhau.Text);
                                    if (string.IsNullOrEmpty(txtTenHienThi.Text.Trim()))
                                    {
                                        erpThemKhachHang.SetError(txtTenHienThi, "Vui lòng điền trường này!");
                                        txtTenHienThi.Focus();
                                    }
                                    else
                                    {
                                        newCustomer.User.DisplayName = txtTenHienThi.Text;
                                        if (dtpNgaySinh.Value == null)
                                        {
                                            erpThemKhachHang.SetError(dtpNgaySinh, "Vui lòng điền trường này!");
                                            dtpNgaySinh.Focus();
                                        }
                                        else
                                        {
                                            newCustomer.User.DateOfBirth = dtpNgaySinh.Value;
                                            if (string.IsNullOrEmpty(txtSoCMT.Text.Trim()))
                                            {
                                                erpThemKhachHang.SetError(txtSoCMT, "Vui lòng điền trường này!");
                                                txtSoCMT.Focus();
                                            }
                                            else
                                            {
                                                newCustomer.PassportID = txtSoCMT.Text.Trim();
                                                if (!rdoNam.Checked && !rdoNu.Checked)
                                                {
                                                    erpThemKhachHang.SetError(rdoNu, "Vui lòng điền trường này!");
                                                }
                                                else
                                                {
                                                    newCustomer.User.Gender = rdoNam.Checked;
                                                    if (string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                                                    {
                                                        erpThemKhachHang.SetError(txtSoDienThoai, "Vui lòng điền trường này!");
                                                        txtSoDienThoai.Focus();
                                                    }
                                                    else
                                                    {
                                                        newCustomer.User.Phone = txtSoDienThoai.Text;
                                                        if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                                                        {
                                                            erpThemKhachHang.SetError(txtEmail, "Vui lòng điền trường này!");
                                                            txtEmail.Focus();


                                                        }
                                                        else
                                                        {
                                                            Regex mRegxExpression;
                                                            mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                                                            if (!mRegxExpression.IsMatch(txtEmail.Text.Trim()))
                                                            {
                                                                erpThemKhachHang.SetError(txtEmail, "Địa chỉ email không hợp lệ!");
                                                                txtEmail.Focus();

                                                            }
                                                            else
                                                            {
                                                                newCustomer.User.Email = txtEmail.Text.Trim();
                                                                if (string.IsNullOrEmpty(txtDiaChi.Text.Trim()))
                                                                {
                                                                    erpThemKhachHang.SetError(txtDiaChi, "Vui lòng điền trường này!");
                                                                    txtDiaChi.Focus();
                                                                }
                                                                else
                                                                {
                                                                    newCustomer.User.Address = txtDiaChi.Text.Trim();
                                                                    if (cboPrice.SelectedIndex == -1)
                                                                    {
                                                                        erpThemKhachHang.SetError(cboPrice, "Vui lòng điền trường này!");
                                                                        cboPrice.Focus();
                                                                    }
                                                                    else
                                                                    {
                                                                        newCustomer.PriceID = int.Parse(cboPrice.SelectedValue.ToString());
                                                                        newCustomer.User.isPay = false;
                                                                        newCustomer.User.CreatedByID = mUser.ID;
                                                                        newCustomer.User.CreatedDate = DateTime.Now.Date;
                                                                        newCustomer.CreatedByID = mUser.ID;
                                                                        newCustomer.CreatedDate = DateTime.Now.Date;
                                                                        newCustomer.User.isDelete = false;
                                                                        newCustomer.User.isAdmin = 3; // khách hàng
                                                                        Customer CustomerResult = new KhachHangLDM().Insert(newCustomer);
                                                                        if (CustomerResult != null)
                                                                        {
                                                                            MessageBox.Show("Thêm khách hàng thành công!");
                                                                            this.UCCuaSoChinh.ReLoadDataKhachHang();
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Thêm khách hàng thất bại!");
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
        private void ThemKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                UCCuaSoChinh.Parent.Parent.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ThemKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
                cboPrice.DataSource = new PriceLDM().GetElements();
                cboPrice.ValueMember = "ID";
                cboPrice.DisplayMember = "PriceName";
                cboPrice.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void txtSoCMT_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
