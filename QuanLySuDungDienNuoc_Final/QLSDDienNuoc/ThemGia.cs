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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSDDienNuoc
{
    public partial class ThemGia : Form
    {
        private User mUser;
        public UCGia mUCGia { get; set; }
        private Price mPrice = null;
        public ThemGia(User oUser)
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
        public ThemGia(User oUser, Price EditPrice)
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
            mPrice = EditPrice;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            try
            {
                mUCGia.Parent.Parent.Enabled = true;
                this.Close();

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
                Price newPrice = new Price();
                if (string.IsNullOrEmpty(txtTenLoaiGia.Text.Trim()))
                {
                    erpThemGia.SetError(txtTenLoaiGia, "Vui lòng điền trường này!");
                    txtTenLoaiGia.Focus();
                }
                else
                {
                    newPrice.PriceName = txtTenLoaiGia.Text.Trim();
                    if (string.IsNullOrEmpty(txtGiaDien.Text.Trim()))
                    {
                        erpThemGia.SetError(txtGiaDien, "Vui lòng điền trường này!");
                        txtGiaDien.Focus();
                    }
                    else
                    {
                        newPrice.ElectricPrice = decimal.Parse(txtGiaDien.Text.Trim().Replace(".", ""));
                        if (string.IsNullOrEmpty(txtGiaNuoc.Text.Trim()))
                        {
                            erpThemGia.SetError(txtGiaNuoc, "Vui lòng điền trường này!");
                            txtGiaNuoc.Focus();
                        }
                        else
                        {
                            newPrice.WaterPrice = decimal.Parse(txtGiaNuoc.Text.Trim().Replace(".", ""));
                            if (mPrice == null)
                            {
                                newPrice.CreatedDate = DateTime.Now.Date;
                                newPrice.CreatedByID = mUser.ID;
                                Price Result = new PriceLDM().Insert(newPrice);
                                if (Result != null)
                                {
                                    MessageBox.Show("Thêm đơn giá thành công!");
                                    this.mUCGia.ReloadDataGia();
                                }
                                else
                                {
                                    MessageBox.Show("Thêm đơn giá thất bại");
                                }
                            }
                            else
                            {
                                newPrice.ModifiedByID = mUser.ID;
                                newPrice.ModifiedDate = DateTime.Now.Date;
                                newPrice.ID = mPrice.ID;
                                if (new PriceLDM().Update(newPrice))
                                {
                                    MessageBox.Show("Cập nhật đơn giá thành công!");
                                    this.mUCGia.ReloadDataGia();
                                }
                                else
                                {
                                    MessageBox.Show("Thêm đơn giá thất bại");
                                }
                            }
                            this.Close();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void ThemGia_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mUCGia.Parent.Parent.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtGiaDien_KeyPress(object sender, KeyPressEventArgs e)
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


        private void txtGiaDien_Leave(object sender, EventArgs e)
        {
            try
            {
                double value;
                if (double.TryParse(txtGiaDien.Text, out value))
                {
                    txtGiaDien.Text = (value.ToString("#,###"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtGiaNuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtGiaNuoc_Leave(object sender, EventArgs e)
        {
            try
            {
                double value;
                if (double.TryParse(txtGiaNuoc.Text, out value))
                {
                    txtGiaNuoc.Text = (value.ToString("#,###"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void ThemGia_Load(object sender, EventArgs e)
        {
            try
            {
                if (mPrice != null)
                {
                    this.Text = "Cập nhật thông tin đơn giá";
                    label.Text = "Cập nhật thông tin đơn giá";
                    txtTenLoaiGia.Text = mPrice.PriceName;
                    txtGiaDien.Text = mPrice.ElectricPrice.Value.ToString("#,###");
                    txtGiaNuoc.Text = mPrice.WaterPrice.Value.ToString("#,###");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
