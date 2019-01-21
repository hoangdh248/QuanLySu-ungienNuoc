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
using System.Runtime.InteropServices;
using QLSDDienNuoc.UserControls;
using LogicDataModels;

namespace QLSDDienNuoc
{
    public partial class frmMain : Form
    {
        private User mUsers;
        private List<Users_Roles> mUR;
        public baseUC currentUC { get; set; }
        public frmMain()
        {
            InitializeComponent();
            mUsers = new User() {
                ID = -1
            };
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Maximized;
                    btnMaxximum.Visible = false;
                    btnRes.Visible = true;
                }
                string DisplayRole = string.Empty;
                frmLoginFlat oFrmLogin = new frmLoginFlat(mUsers);
                oFrmLogin.ShowDialog();
                if(mUsers.ID == -1)
                {
                    Application.Exit();
                }
                mUR = new User_RoleLDM().GetElements(mUsers.ID);

                if (mUsers.isAdmin == 1)
                {
                    DisplayRole = "quản trị viên";
                }
                else
                    if (mUsers.isAdmin == 2)
                {
                    DisplayRole = "nhân viên";
                }
                else
                {
                    DisplayRole = "khách hàng";
                }
                label1.Text = $"Xin chào {DisplayRole} {mUsers.DisplayName}";



                if (mUsers.isAdmin == 3) // khach hang
                {
                    UCquanLyTieuThu mUC = new UCquanLyTieuThu(mUsers, new KhachHangLDM().GetElementByUserID(mUsers.ID).ID, this);
                    currentUC = mUC;
                    mUC.Dock = DockStyle.Fill;
                    pnlMain.Controls.Add(mUC);
                    btnCuaSoChinh.Enabled = false;
                    btnCuaSoChinhStatus.Visible = false;
                    btnQuanLyNguoiDung.Enabled = false;
                    btnQuanLyNguoiDungStatus.Visible = false;
                    btnQuanLyDonGia.Enabled = false;
                    btnQuanLyDonGiaStatus.Visible = false;
                }
                else
                {
                    if (mUsers.isAdmin == 2)
                    {
                        if (mUR.Where(x => x.Role.RoleName == "Quản lý khách hàng").First().isView.Value)
                        {
                            UCcuaSoChinh oCuaSoChinh = new UCcuaSoChinh(mUsers, this);
                            currentUC = oCuaSoChinh;
                            oCuaSoChinh.Dock = DockStyle.Fill;
                            pnlMain.Controls.Add(oCuaSoChinh);
                            btnCuaSoChinhStatus.Visible = true;
                        }
                        else
                        {
                            btnCuaSoChinh.Enabled = false;
                            btnCuaSoChinhStatus.Visible = false;
                        }

                        if (mUR.Where(x => x.Role.RoleName == "Quản lý nhân viên").First().isView.Value == false)
                        {
                            btnQuanLyNguoiDung.Enabled = false;
                            btnQuanLyNguoiDungStatus.Visible = false;
                        }

                        if (mUR.Where(x => x.Role.RoleName == "Quản lý đơn giá").First().isView.Value == false)
                        {
                            btnQuanLyDonGia.Enabled = false;
                            btnQuanLyDonGiaStatus.Visible = false;
                        }
                    }
                    else
                    {
                        if(mUsers.isAdmin == 1)
                        {
                            UCcuaSoChinh oCuaSoChinh = new UCcuaSoChinh(mUsers, this);
                            currentUC = oCuaSoChinh;
                            oCuaSoChinh.Dock = DockStyle.Fill;
                            pnlMain.Controls.Add(oCuaSoChinh);
                            btnCuaSoChinhStatus.Visible = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMaxximum_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaxximum.Visible = false;
                btnRes.Visible = true;
                if(currentUC != null)
                {
                    currentUC.ReSizeDataGridView(DataGridViewAutoSizeColumnMode.Fill);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Normal;
                if(currentUC != null)
                {
                    if (currentUC.GetType().ToString() == "QLSDDienNuoc.UserControls.UCquanLyTieuThu")
                    {
                        currentUC.ReSizeDataGridView(DataGridViewAutoSizeColumnMode.AllCells);
                    }
                    else
                    {
                        currentUC.ReSizeDataGridView(DataGridViewAutoSizeColumnMode.Fill);
                    }
                }
                btnRes.Visible = false;
                btnMaxximum.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnMinimum_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (btnRes.Visible == true)
                {
                    btnRes.Visible = false;
                    btnMaxximum.Visible = true;
                }
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

        private void btnQuanLyNguoiDung_Click(object sender, EventArgs e)
        {
            try
            {
                UCNguoiDung oUCNguoiDung = new UCNguoiDung(mUsers);
                currentUC = oUCNguoiDung;
                oUCNguoiDung.Dock = DockStyle.Fill;
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(oUCNguoiDung);
                btnCuaSoChinhStatus.Visible = false;
                btnQuanLyDonGiaStatus.Visible = false;
                btnQuanLyNguoiDungStatus.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        

        private void btnCuaSoChinh_Click(object sender, EventArgs e)
        {
            try
            {
                UCcuaSoChinh oUCcsc = new UCcuaSoChinh(mUsers, this);
                currentUC = oUCcsc;
                oUCcsc.Dock = DockStyle.Fill;
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(oUCcsc);
                btnCuaSoChinhStatus.Visible = true;
                btnQuanLyDonGiaStatus.Visible = false;
                btnQuanLyNguoiDungStatus.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnQuanLyDonGia_Click(object sender, EventArgs e)
        {
            try
            {
                UCGia oUCgia = new UCGia(mUsers);
                currentUC = oUCgia;
                oUCgia.Dock = DockStyle.Fill;
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(oUCgia);
                btnCuaSoChinhStatus.Visible = false;
                btnQuanLyDonGiaStatus.Visible = true;
                btnQuanLyNguoiDungStatus.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CapNhatThongTinCaNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (mUsers.isAdmin == 3)
                {
                    SuaKhachHang obj = new SuaKhachHang(mUsers, new KhachHangLDM().GetElementByUserID(mUsers.ID), true);
                    obj.Owner = this;
                    this.Enabled = false;
                    obj.Show();
                }
                else
                {
                    SuaNguoiDung obj = new SuaNguoiDung(mUsers, new UserLDM().GetElement(mUsers.ID), true);
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


        private void pnlMain_ControlAdded(object sender, ControlEventArgs e)
        {
            try
            {
                if (currentUC != null)
                {
                    if (WindowState == FormWindowState.Normal)
                    {
                        if (currentUC.GetType().ToString() == "QLSDDienNuoc.UserControls.UCquanLyTieuThu")
                        {
                            currentUC.ReSizeDataGridView(DataGridViewAutoSizeColumnMode.AllCells);
                        }
                        else
                        {
                            currentUC.ReSizeDataGridView(DataGridViewAutoSizeColumnMode.Fill);
                        }
                    }
                    else
                    {
                        currentUC.ReSizeDataGridView(DataGridViewAutoSizeColumnMode.Fill);
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
