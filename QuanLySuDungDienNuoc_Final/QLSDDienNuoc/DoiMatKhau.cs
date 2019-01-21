using LogicDataModels;
using LogicDataModels.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSDDienNuoc
{
    public partial class DoiMatKhau : Form
    {
        private User mUser;
        private User PassUser = null;
        public DoiMatKhau(User oUser, User obj)
        {
            InitializeComponent();
            if (oUser == null)
            {
                mUser = new User();
            }
            else
            {
                mUser = oUser;
            }
            PassUser = obj;

            lblMatKhauCu.Enabled = false;
            txtMatKhauCu.Enabled = false;

        }
        public DoiMatKhau(User oUser)
        {
            InitializeComponent();
            if (oUser == null)
            {
                mUser = new User();
            }
            else
            {
                mUser = oUser;
            }
        }

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                erpDoiMatKhau.Clear();
                User obj = new User();
                obj.Password = PassUser == null ? mUser.Password : PassUser.Password;
                if ((PassUser == null) && string.IsNullOrEmpty(txtMatKhauCu.Text))
                {
                    erpDoiMatKhau.SetError(txtMatKhauCu, "Vui lòng điền trường này!");
                    txtMatKhauCu.Focus();
                }
                else
                {
                    if ((PassUser == null) && Commons.En_Decrypt.Encrypt(txtMatKhauCu.Text) != obj.Password)
                    {
                        erpDoiMatKhau.SetError(txtMatKhauCu, "Mật khẩu không chính xác!");
                        txtMatKhauCu.Focus();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtMatKhauMoi.Text))
                        {
                            erpDoiMatKhau.SetError(txtMatKhauMoi, "Vui lòng điền trường này!");
                            txtMatKhauMoi.Focus();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtNhapLaiMatKhauMoi.Text))
                            {
                                erpDoiMatKhau.SetError(txtNhapLaiMatKhauMoi, "Vui lòng điền trường này!");
                                txtNhapLaiMatKhauMoi.Focus();
                            }
                            else
                            {
                                if (!txtMatKhauMoi.Text.Equals(txtNhapLaiMatKhauMoi.Text))
                                {
                                    erpDoiMatKhau.SetError(txtNhapLaiMatKhauMoi, "Nhập lại mật khẩu không khớp!");
                                    txtNhapLaiMatKhauMoi.Focus();
                                }
                                else
                                {
                                    obj.Password = Commons.En_Decrypt.Encrypt(txtNhapLaiMatKhauMoi.Text);

                                    if (PassUser == null)
                                    {
                                        obj.ID = mUser.ID;
                                    }
                                    else
                                    {
                                        obj.ID = PassUser.ID;
                                    }
                                    if (new UserLDM().DoiMatKhau(obj))
                                    {
                                        if (PassUser == null)
                                        {
                                            mUser.Password = obj.Password;
                                        }
                                        
                                        MessageBox.Show("Đổi mật khẩu thành công");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Đổi mật khẩu thất bại");
                                    }
                                    this.Close();
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

        private void DoiMatKhau_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                Owner.Enabled = true;
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
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
