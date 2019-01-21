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
    public partial class frmLoginFlat : Form
    {
        private User mUser;
        public frmLoginFlat()
        {
            InitializeComponent();
        }
        public frmLoginFlat(User oUser)
        {
            InitializeComponent();
            if (oUser == null)
            {
                mUser = new User();
            }
            else
                mUser = oUser;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                errorProviderLogin.Clear();
                panelError.Visible = false;
                if (string.IsNullOrEmpty(txtTenDangNhap.Text))
                {
                    errorProviderLogin.SetError(txtTenDangNhap, "Vui lòng nhập Tên đăng nhập!");
                    txtTenDangNhap.Focus();
                }
                else
                if (string.IsNullOrEmpty(txtMatKhau.Text))
                {

                    errorProviderLogin.SetError(txtMatKhau, "Vui lòng nhập Mật khẩu!");
                    txtMatKhau.Focus();
                }
                else
                {
                    User LoginUser = new UserLDM().Login(txtTenDangNhap.Text, txtMatKhau.Text);
                    if (LoginUser != null)
                    {
                        mUser.UserName = LoginUser.UserName;
                        mUser.Address = LoginUser.Address;
                        mUser.CreatedByID = LoginUser.CreatedByID;
                        mUser.CreatedDate = LoginUser.CreatedDate;
                        mUser.DateOfBirth = LoginUser.DateOfBirth;
                        mUser.Customers = LoginUser.Customers;
                        mUser.DisplayName = LoginUser.DisplayName;
                        mUser.Email = LoginUser.Email;
                        mUser.Gender = LoginUser.Gender;
                        mUser.ID = LoginUser.ID;
                        mUser.isAdmin = LoginUser.isAdmin;
                        mUser.isDelete = LoginUser.isDelete;
                        mUser.ModifiedByID = LoginUser.ModifiedByID;
                        mUser.ModifiedDate = LoginUser.ModifiedDate;
                        mUser.Password = LoginUser.Password;
                        mUser.Phone = LoginUser.Phone;
                        mUser.Users_Roles = LoginUser.Users_Roles;
                        mUser.isPay = LoginUser.isPay;
                        this.Close();
                    }
                    else
                    {
                        mUser.ID = -1;
                        panelError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void frmLoginFlat_Load(object sender, EventArgs e)
        {
            try
            {
                txtMatKhau.Text = "123a@";
                txtTenDangNhap.Text = "admin";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
