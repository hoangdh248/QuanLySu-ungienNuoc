using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicDataModels.DataModels;
using LogicDataModels;

namespace QLSDDienNuoc.UserControls
{
    public partial class UCBieuDo : UserControl
    {
        private User mUser;
        private int CustomerID;
        private baseUC mUC;
        List<Consume> lstTieuThu;
        public UCBieuDo(User oUser, int cusID, baseUC oUC)
        {
            InitializeComponent();
            mUC = oUC;
            if (oUser == null)
            {
                mUser = new User();
            }
            else
            {
                mUser = oUser;
            }
            CustomerID = cusID;
            this.Dock = DockStyle.Fill;
            lstTieuThu = lstTieuThu = new TieuThuLDM().SearchElements(DateTime.Now.AddYears(-1).Date, DateTime.Now.Date, CustomerID);
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            lblTenKhachHang.Text = new KhachHangLDM().GetElement(CustomerID).User.DisplayName;
        }



        private void UCBieuDo_Load(object sender, EventArgs e)
        {
            try
            {
                chartDien.DataSource = lstTieuThu;
                chartDien.Series["Kwh"].Points.Clear();
                chartNuoc.Series["m3"].Points.Clear();
                foreach (var item in lstTieuThu)
                {
                    chartDien.Series["Kwh"].Points.AddXY(item.Time.Value.ToString("dd/MM/yyyy"), item.ElectricConsume);
                }
                chartNuoc.DataSource = lstTieuThu;
                foreach (var item in lstTieuThu)
                {
                    chartNuoc.Series["m3"].Points.AddXY(item.Time.Value.ToString("dd/MM/yyyy"), item.WaterConsume);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            try
            {
                this.Parent.Controls.Add(this.mUC);
                this.Parent.Controls.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                erpBieuDo.Clear();
                if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
                {
                    erpBieuDo.SetError(dtpToDate, "\"Đến ngày:\" phải lớn hơn thời gian \"Từ ngày:\"!");
                    dtpToDate.Focus();
                }
                else
                {
                    lstTieuThu = new TieuThuLDM().SearchElements(dtpFromDate.Value.Date, dtpToDate.Value.Date, CustomerID);
                    UCBieuDo_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
