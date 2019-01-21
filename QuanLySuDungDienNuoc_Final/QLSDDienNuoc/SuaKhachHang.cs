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
    public partial class SuaKhachHang : Form
    {
        private User mUser;
        public UCcuaSoChinh UCCuaSoChinh { get; set; }
        private Customer mCustomer;
        private bool isUpdateProfile;
        public SuaKhachHang(User oUser, Customer customer, bool isUpdateProfile = false)
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
            mCustomer = customer;
            this.isUpdateProfile = isUpdateProfile;
            if (isUpdateProfile)
            {
                lblDoiMatKhau.Visible = true;
                lblGiaApDung.Visible = false;
                cboPrice.Visible = false;
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
                erpSuaKhachHang.Clear();
                if (string.IsNullOrEmpty(txtTenHienThi.Text.Trim()))
                {
                    erpSuaKhachHang.SetError(txtTenHienThi, "Vui lòng điền trường này!");
                    txtTenHienThi.Focus();
                }
                else
                {
                    newCustomer.User.DisplayName = txtTenHienThi.Text;
                    if (dtpNgaySinh.Value == null)
                    {
                        erpSuaKhachHang.SetError(dtpNgaySinh, "Vui lòng điền trường này!");
                        dtpNgaySinh.Focus();
                    }
                    else
                    {
                        newCustomer.User.DateOfBirth = dtpNgaySinh.Value.Date;
                        if (string.IsNullOrEmpty(txtSoCMT.Text.Trim()))
                        {
                            erpSuaKhachHang.SetError(txtSoCMT, "Vui lòng điền trường này!");
                            txtSoCMT.Focus();
                        }
                        else
                        {
                            newCustomer.PassportID = txtSoCMT.Text.Trim();
                            if (!rdoNam.Checked && !rdoNu.Checked)
                            {
                                erpSuaKhachHang.SetError(rdoNu, "Vui lòng điền trường này!");
                            }
                            else
                            {
                                newCustomer.User.Gender = rdoNam.Checked;
                                if (string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                                {
                                    erpSuaKhachHang.SetError(txtSoDienThoai, "Vui lòng điền trường này!");
                                    txtSoDienThoai.Focus();
                                }
                                else
                                {
                                    newCustomer.User.Phone = txtSoDienThoai.Text;
                                    if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                                    {
                                        erpSuaKhachHang.SetError(txtEmail, "Vui lòng điền trường này!");
                                        txtEmail.Focus();


                                    }
                                    else
                                    {
                                        Regex mRegxExpression;
                                        mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                                        if (!mRegxExpression.IsMatch(txtEmail.Text.Trim()))
                                        {
                                            erpSuaKhachHang.SetError(txtEmail, "Địa chỉ email không hợp lệ!");
                                            txtEmail.Focus();

                                        }
                                        else
                                        {
                                            newCustomer.User.Email = txtEmail.Text.Trim();
                                            if (string.IsNullOrEmpty(txtDiaChi.Text.Trim()))
                                            {
                                                erpSuaKhachHang.SetError(txtDiaChi, "Vui lòng điền trường này!");
                                                txtDiaChi.Focus();
                                            }
                                            else
                                            {
                                                newCustomer.User.Address = txtDiaChi.Text.Trim();
                                                if (isUpdateProfile == false && cboPrice.SelectedIndex == -1)
                                                {
                                                    erpSuaKhachHang.SetError(cboPrice, "Vui lòng điền trường này!");
                                                    cboPrice.Focus();
                                                }
                                                else
                                                {
                                                    newCustomer.PriceID = !isUpdateProfile ? int.Parse(cboPrice.SelectedValue.ToString()) : mCustomer.PriceID;
                                                    newCustomer.User.ModifiedByID = mUser.ID;
                                                    newCustomer.User.ModifiedDate = DateTime.Now.Date;
                                                    newCustomer.User.isPay = false;
                                                    newCustomer.ModifiedByID = mUser.ID;
                                                    newCustomer.ModifiedDate = DateTime.Now.Date;
                                                    newCustomer.ID = mCustomer.ID;
                                                    newCustomer.User.ID = mCustomer.UserID.Value;
                                                    newCustomer.User.isAdmin = 3;
                                                    newCustomer.UserID = mCustomer.UserID.Value;
                                                    if (new KhachHangLDM().Update(newCustomer))
                                                    {
                                                        MessageBox.Show("Sửa khách hàng thành công!");
                                                        if (!isUpdateProfile)
                                                        {
                                                            this.UCCuaSoChinh.ReLoadDataKhachHang();
                                                        }
                                                        else
                                                        {
                                                            mUser = new UserLDM().GetElement(mUser.ID);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Sửa khách hàng thất bại!");
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
        private void SuaKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (!isUpdateProfile)
                    UCCuaSoChinh.Parent.Parent.Enabled = true;
                else
                    this.Owner.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void SuaKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
                cboPrice.DataSource = new PriceLDM().GetElements();
                cboPrice.ValueMember = "ID";
                cboPrice.DisplayMember = "PriceName";
                cboPrice.SelectedIndex = -1;
                txtTenHienThi.Text = mCustomer.User.DisplayName;
                txtSoDienThoai.Text = mCustomer.User.Phone;
                txtSoCMT.Text = mCustomer.PassportID;
                txtEmail.Text = mCustomer.User.Email;
                txtDiaChi.Text = mCustomer.User.Address;
                rdoNam.Checked = mCustomer.User.Gender.Value ? true : false;
                rdoNu.Checked = mCustomer.User.Gender.Value == false ? true : false;
                cboPrice.SelectedValue = mCustomer.PriceID.Value;
                dtpNgaySinh.Value = mCustomer.User.DateOfBirth.Value;
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

        private void lblDoiMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (isUpdateProfile)
                {
                    DoiMatKhau obj = new DoiMatKhau(mUser);
                    obj.Owner = this;
                    this.Enabled = false;
                    obj.Show();
                }
                else
                {

                    DoiMatKhau obj = new DoiMatKhau(mUser, new UserLDM().GetElement(mCustomer.UserID.Value));
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
