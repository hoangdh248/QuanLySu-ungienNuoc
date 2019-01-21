using Commons;
using LogicDataModels;
using LogicDataModels.DataModels;
using QLSDDienNuoc.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Words.NET;

namespace QLSDDienNuoc
{
    public partial class XacNhanThanhToan : Form
    {
        private User mUser;
        private string thoiGian;
        private Price mPrice;
        private Consume mConsume;
        public UCquanLyTieuThu mUC { get; set; }
        public XacNhanThanhToan(User oUser, Consume oConsume, string thoiGian)
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
            this.thoiGian = thoiGian;
            mPrice = new PriceLDM().GetElement(new KhachHangLDM().GetElement(oConsume.CustomerID.Value).PriceID.Value);
            mConsume = oConsume;

        }

        private void lblLuongDienTieuThu_Click(object sender, EventArgs e)
        {

        }

        private void XacNhanThanhToan_Load(object sender, EventArgs e)
        {
            try
            {
                lblChiSoDienCu.Text = mConsume.OldElectricIndex.ToString() + "(Kwh)";
                lblChiSoDienMoi.Text = mConsume.NewElectricIndex.ToString() + "(Kwh)";
                lblChiSoNuocCu.Text = mConsume.OldWaterIndex.ToString() + "(khối)";
                lblChiSoNuocMoi.Text = mConsume.NewWaterIndex.ToString() + "(khối)";
                lblDonGiaDien.Text = mPrice.ElectricPrice.Value.ToString("#,###") + " (đồng)";
                lblDonGiaNuoc.Text = mPrice.WaterPrice.Value.ToString("#,###") + " (đồng)";
                lblLuongDienTieuThu.Text = mConsume.ElectricConsume.ToString() + "(Kwh)";
                lblLuongNuocTieuThu.Text = mConsume.WaterConsume.ToString() + "(Kwh)";
                lblThanhTienDien.Text = (mConsume.ElectricConsume.Value * mPrice.ElectricPrice.Value).ToString("#,###") + " (đồng)";
                lblThanhTienNuoc.Text = (mConsume.WaterConsume.Value * mPrice.WaterPrice.Value).ToString("#,###") + " (đồng)";
                lblThoiGian.Text = thoiGian;
                lblTongCong.Text = ((mConsume.WaterConsume.Value * mPrice.WaterPrice.Value) + (mConsume.ElectricConsume.Value * mPrice.ElectricPrice.Value)).ToString("#,###") + " (đồng)";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void XacNhanThanhToan_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mUC.Parent.Parent.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (new TieuThuLDM().ThanhToan(mConsume))
                {

                    saveFileDialog.FileName = $"Hóa đơn_{new KhachHangLDM().GetElement(mConsume.CustomerID.Value).User.DisplayName}_" + mConsume.Time.Value.ToString("dd/MM/yyyy").Replace("/", "");
                    saveFileDialog.Title = "Save As";
                    saveFileDialog.Filter = "DocX|*.docx";
                    saveFileDialog.ShowDialog();
                    mUC.ReLoadDataTieuThu();

                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại!");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                Customer mCus = new KhachHangLDM().GetElement(mConsume.CustomerID.Value);
                var doc = DocX.Load(@"..\..\doc\HoaDon.docx");
                doc.ReplaceText("<MaSo>", (MyRandom.RandomString(9) + mConsume.ID));
                doc.ReplaceText("<TenKhachHang>", mCus.User.DisplayName);
                doc.ReplaceText("<SoCMT>", mCus.PassportID);
                doc.ReplaceText("<DiaChi>", mCus.User.Address);
                doc.ReplaceText("<ThoiGian>", thoiGian);
                doc.ReplaceText("<LuongDienTieuThu>", mConsume.ElectricConsume.ToString() + " (Kwh)");
                doc.ReplaceText("<LuongNuocTieuThu>", mConsume.WaterConsume.ToString() + " (khối)");
                doc.ReplaceText("<TongTienDien>", (mConsume.ElectricConsume.Value * mPrice.ElectricPrice.Value).ToString("#,###"));
                doc.ReplaceText("<TongTienNuoc>", (mConsume.WaterConsume.Value * mPrice.WaterPrice.Value).ToString("#,###"));
                doc.ReplaceText("<TongCong>", ((mConsume.WaterConsume.Value * mPrice.WaterPrice.Value) + (mConsume.ElectricConsume.Value * mPrice.ElectricPrice.Value)).ToString("#,###"));
                doc.ReplaceText("<Ngay>", DateTime.Now.Day.ToString());
                doc.ReplaceText("<Thang>", DateTime.Now.Month.ToString());
                doc.ReplaceText("<Nam>", DateTime.Now.Year.ToString());
                Stream stream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(stream);
                doc.SaveAs(stream);
                sw.Close();
                stream.Close();
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
    }
}
