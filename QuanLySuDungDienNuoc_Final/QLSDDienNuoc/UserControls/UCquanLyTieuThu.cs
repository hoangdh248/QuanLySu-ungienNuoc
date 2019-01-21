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
using System.Globalization;
using Xceed.Words.NET;
using Commons;
using System.IO;
using System.Reflection;

namespace QLSDDienNuoc.UserControls
{
    public partial class UCquanLyTieuThu : baseUC
    {
        private User mUser;
        public UCcuaSoChinh mUCCuaSoChinh { get; set; }
        private frmMain frmSource;
        private int CustomerID;
        private Consume mConsume;
        private string thoiGian;
        private Price mPrice;
        private Users_Roles mUR;
        private int tempSua = 13;
        private int tempXoa = 13;
        private int tempTT = 13;
        private int tempTHD = 13;
        public UCquanLyTieuThu()
        {

        }
        public UCquanLyTieuThu(User oUser, int CustomerID, frmMain frm)
        {
            InitializeComponent();
            frmSource = frm;
            frmSource.currentUC = this;
            if (oUser != null)
            {
                mUser = oUser;
            }
            else
            {
                mUser = new User();
            }
            this.Dock = DockStyle.Fill;
            this.CustomerID = CustomerID;
            if (mUser.isAdmin == 2)
            {
                foreach (var item in new User_RoleLDM().GetElements(mUser.ID))
                {
                    if (item.Role.RoleName == "Quản lý tiêu thụ")
                    {
                        mUR = item;
                    }
                }
            }
            else
            {
                if(mUser.isAdmin == 3)
                {
                    mUR = new Users_Roles
                    {
                        isAdd = false,
                        isEdit = false,
                        isRemove = false
                    };
                    mUser.isPay = false;
                }
                
            }
        }


        
        private void UCquanLyTieuThu_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFromDate.CustomFormat = "dd/MM/yyyy";
                dtpToDate.CustomFormat = "dd/MM/yyyy";

                ReLoadDataTieuThu();
                dgvTieuThu.Columns["ChiSoDienCu"].HeaderText = "Chỉ số điện cũ(Kwh)";
                dgvTieuThu.Columns["ChiSoDienMoi"].HeaderText = "Chỉ số điện mới(Kwh)";
                dgvTieuThu.Columns["LuongDienTieuThu"].HeaderText = "Lượng điện tiêu thụ(Kwh)";
                dgvTieuThu.Columns["ChiSoNuocCu"].HeaderText = "Chỉ số nước cũ(khối)";
                dgvTieuThu.Columns["ChiSoNuocMoi"].HeaderText = "Chỉ số nước mới(khối)";
                dgvTieuThu.Columns["LuongNuocTieuThu"].HeaderText = "Lượng nước tiêu thụ(khối)";
                dgvTieuThu.Columns["ThoiGian"].HeaderText = "Thời gian";
                dgvTieuThu.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvTieuThu.Columns["ThemBoi"].HeaderText = "Thêm bởi";
                dgvTieuThu.Columns["SuaBoi"].HeaderText = "Sửa bởi";
                dgvTieuThu.Columns["ID"].Visible = false;
                // set headertext name

                DataGridViewTextBoxColumn stt = new DataGridViewTextBoxColumn();
                stt.Name = "STT";
                stt.HeaderText = "STT";
                dgvTieuThu.Columns.Insert(0, stt);

                int temp = 12;
                if(mUser.isAdmin == 1 || mUser.isPay.Value)
                {
                    DataGridViewButtonColumn ThanhToan = new DataGridViewButtonColumn();
                    ThanhToan.HeaderText = "Thanh toán";
                    ThanhToan.Name = "ThanhToan";
                    ThanhToan.Text = "Thanh toán";
                    ThanhToan.UseColumnTextForButtonValue = true;
                    dgvTieuThu.Columns.Insert(temp, ThanhToan);
                    dgvTieuThu.Columns["ThanhToan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvTieuThu.Columns["ThanhToan"].Width = 60;
                    temp++;
                    tempTT = temp;
                }
                

                DataGridViewButtonColumn TaihoaDon = new DataGridViewButtonColumn();
                TaihoaDon.HeaderText = "Tải hóa đơn";
                TaihoaDon.Name = "TaiHoaDon";
                TaihoaDon.Text = "Tải hóa đơn";
                TaihoaDon.UseColumnTextForButtonValue = true;
                dgvTieuThu.Columns.Insert(temp, TaihoaDon);
                temp++;
                tempTHD = temp;

                if(mUser.isAdmin == 1 || mUR.isEdit.Value)
                {
                    DataGridViewImageColumn Edit = new DataGridViewImageColumn();
                    Edit.HeaderText = "Sửa";
                    Edit.Name = "Edit";
                    Edit.Image = Properties.Resources.icons8_pencil_26;
                    dgvTieuThu.Columns.Insert(temp, Edit);
                    dgvTieuThu.Columns["Edit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvTieuThu.Columns["Edit"].Width = 30;
                    temp++;
                    tempSua = temp;
                }
                

                if(mUser.isAdmin == 1 || mUR.isRemove.Value)
                {
                    DataGridViewImageColumn Delete = new DataGridViewImageColumn();
                    Delete.HeaderText = "Xóa";
                    Delete.Name = "Delete";
                    Delete.Image = Properties.Resources.delete;
                    dgvTieuThu.Columns.Insert(temp, Delete);
                    dgvTieuThu.Columns["Delete"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvTieuThu.Columns["Delete"].Width = 30;
                    temp++;
                    tempXoa = temp;
                }

                



                for (int i = 0; i < dgvTieuThu.Columns.Count; i++)
                {
                    // set size display
                    dgvTieuThu.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    // set text color header
                    dgvTieuThu.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                    //set alignment middle
                    dgvTieuThu.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvTieuThu.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                dgvTieuThu.Columns["STT"].Width = 30;

                
                dgvTieuThu.Columns["TaiHoaDon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTieuThu.Columns["TaiHoaDon"].Width = 60;
                
                

                dgvTieuThu.Columns["ChiSoDienCu"].Width = 70;
                dgvTieuThu.Columns["ChiSoDienMoi"].Width = 70;
                dgvTieuThu.Columns["LuongDienTieuThu"].Width = 70;
                dgvTieuThu.Columns["ChiSoNuocCu"].Width = 70;
                dgvTieuThu.Columns["ChiSoNuocMoi"].Width = 70;
                dgvTieuThu.Columns["LuongNuocTieuThu"].Width = 70;



                //set backgd color header
                dgvTieuThu.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
                dgvTieuThu.EnableHeadersVisualStyles = false;
                if(mUser.isAdmin != 1 && mUR.isAdd.Value == false)
                {
                    btnThemTieuThu.Enabled = false;
                }
                if (mUser.isAdmin == 3)
                {
                    btnQuayLai.Enabled = false;
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
                frmSource.currentUC = mUCCuaSoChinh;
                this.Parent.Controls.Add(mUCCuaSoChinh);
                this.Parent.Controls.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void ReLoadDataTieuThu()
        {
            try
            {
                dgvTieuThu.DataSource = new TieuThuLDM().GetElements(CustomerID).Select(obj => new
                {
                    ID = obj.ID,
                    ChiSoDienCu = obj.OldElectricIndex,
                    ChiSoDienMoi = obj.NewElectricIndex,
                    LuongDienTieuThu = obj.ElectricConsume,
                    ChiSoNuocCu = obj.OldWaterIndex,
                    ChiSoNuocMoi = obj.NewWaterIndex,
                    LuongNuocTieuThu = obj.WaterConsume,
                    ThoiGian = obj.Time.Value.ToString("dd/MM/yyyy"),
                    TrangThai = obj.isPay.Value ? "ĐÃ THANH TOÁN" : "CHƯA THANH TOÁN",
                    ThemBoi = obj.CreatedByID == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value).DisplayName + " " + obj.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = obj.ModifiedByID == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value).DisplayName + " " + obj.ModifiedDate.Value.ToString("dd/MM/yyyy"),
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void ReLoadDataTieuThu(List<Consume> data)
        {
            try
            {
                dgvTieuThu.DataSource = data.Select(obj => new
                {
                    ID = obj.ID,
                    ChiSoDienCu = obj.OldElectricIndex,
                    ChiSoDienMoi = obj.NewElectricIndex,
                    LuongDienTieuThu = obj.ElectricConsume,
                    ChiSoNuocCu = obj.OldWaterIndex,
                    ChiSoNuocMoi = obj.NewWaterIndex,
                    LuongNuocTieuThu = obj.WaterConsume,
                    ThoiGian = obj.Time.Value.ToString("dd/MM/yyyy"),
                    TrangThai = obj.isPay.Value ? "ĐÃ THANH TOÁN" : "CHƯA THANH TOÁN",
                    ThemBoi = obj.CreatedByID == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(obj.CreatedByID.Value).DisplayName + " " + obj.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = obj.ModifiedByID == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(obj.ModifiedByID.Value).DisplayName + " " + obj.ModifiedDate.Value.ToString("dd/MM/yyyy"),
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void dgvTieuThu_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                dgvTieuThu.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnThemTieuThu_Click(object sender, EventArgs e)
        {
            try
            {
                ThemTieuThu mThemTieuThu = new ThemTieuThu(mUser, CustomerID);
                this.Parent.Parent.Enabled = false;
                mThemTieuThu.mUCQuanLyTieuThu = this;
                mThemTieuThu.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void dgvTieuThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((mUser.isAdmin == 1 || mUR.isRemove.Value) && e.ColumnIndex == (tempXoa - 12)) // click nut xóa
                {
                    if (dgvTieuThu["TrangThai", e.RowIndex].Value.ToString() == "CHƯA THANH TOÁN")
                    {
                        string fromStr = e.RowIndex > 0 ? dgvTieuThu["ThoiGian", e.RowIndex - 1].Value.ToString() : "BAN ĐẦU";
                        var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa tiêu thụ từ {fromStr} đến {dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString()}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            if (new TieuThuLDM().Delete(int.Parse(dgvTieuThu["ID", e.RowIndex].Value.ToString())))
                            {
                                MessageBox.Show("Xóa tiêu thụ thành công");
                                ReLoadDataTieuThu();
                            }
                            else
                            {
                                MessageBox.Show("Xóa tiêu thụ thất bại");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tiêu thụ đã thanh toán!");

                    }
                }
                if ((mUser.isAdmin == 1 || mUR.isEdit.Value) && e.ColumnIndex == (tempSua - 12)) // click nút sửa
                {
                    if (dgvTieuThu["TrangThai", e.RowIndex].Value.ToString() == "CHƯA THANH TOÁN")
                    {
                        Consume EditConsume = new Consume
                        {
                            ID = int.Parse(dgvTieuThu["ID", e.RowIndex].Value.ToString()),
                            NewElectricIndex = int.Parse(dgvTieuThu["ChiSoDienMoi", e.RowIndex].Value.ToString()),
                            NewWaterIndex = int.Parse(dgvTieuThu["ChiSoNuocMoi", e.RowIndex].Value.ToString()),
                            Time = DateTime.ParseExact(dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        };
                        ThemTieuThu mThemTieuThu = new ThemTieuThu(mUser, CustomerID, EditConsume);
                        this.Parent.Parent.Enabled = false;
                        mThemTieuThu.mUCQuanLyTieuThu = this;
                        mThemTieuThu.Show();
                    }
                    else
                    {
                        MessageBox.Show("Không thể sửa tiêu thụ đã thanh toán!");

                    }
                }
                if ((mUser.isAdmin == 1 || mUser.isPay.Value) && e.ColumnIndex == (tempTT - 12)) // click nut Thanh toan
                {
                    if (dgvTieuThu["TrangThai", e.RowIndex].Value.ToString() == "CHƯA THANH TOÁN")
                    {
                        Consume mConsume = new Consume
                        {
                            ID = int.Parse(dgvTieuThu["ID", e.RowIndex].Value.ToString()),
                            NewElectricIndex = int.Parse(dgvTieuThu["ChiSoDienMoi", e.RowIndex].Value.ToString()),
                            NewWaterIndex = int.Parse(dgvTieuThu["ChiSoNuocMoi", e.RowIndex].Value.ToString()),
                            OldElectricIndex = int.Parse(dgvTieuThu["ChiSoDienCu", e.RowIndex].Value.ToString()),
                            OldWaterIndex = int.Parse(dgvTieuThu["ChiSoNuocCu", e.RowIndex].Value.ToString()),
                            ElectricConsume = int.Parse(dgvTieuThu["LuongDienTieuThu", e.RowIndex].Value.ToString()),
                            WaterConsume = int.Parse(dgvTieuThu["LuongNuocTieuThu", e.RowIndex].Value.ToString()),
                            Time = DateTime.ParseExact(dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            CustomerID = CustomerID
                        };
                        string thoiGian = string.Empty;
                        if (e.RowIndex > 0)
                        {
                            thoiGian = "Từ ngày " + dgvTieuThu["ThoiGian", e.RowIndex - 1].Value.ToString() + " đến ngày " + dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString();
                        }
                        else
                        {
                            thoiGian = "Ban đầu" + " đến ngày " + dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString();
                        }
                        XacNhanThanhToan oFrm = new XacNhanThanhToan(mUser, mConsume, thoiGian);
                        oFrm.mUC = this;
                        this.Parent.Parent.Enabled = false;
                        oFrm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tiêu thụ này đã thanh toán rồi vui lòng chọn tiêu thụ khác!");
                    }
                }
                if (e.ColumnIndex == (tempTHD - 12))
                {
                    if (dgvTieuThu["TrangThai", e.RowIndex].Value.ToString() == "ĐÃ THANH TOÁN")
                    {
                        mConsume = new Consume
                        {
                            ID = int.Parse(dgvTieuThu["ID", e.RowIndex].Value.ToString()),
                            NewElectricIndex = int.Parse(dgvTieuThu["ChiSoDienMoi", e.RowIndex].Value.ToString()),
                            NewWaterIndex = int.Parse(dgvTieuThu["ChiSoNuocMoi", e.RowIndex].Value.ToString()),
                            OldElectricIndex = int.Parse(dgvTieuThu["ChiSoDienCu", e.RowIndex].Value.ToString()),
                            OldWaterIndex = int.Parse(dgvTieuThu["ChiSoNuocCu", e.RowIndex].Value.ToString()),
                            ElectricConsume = int.Parse(dgvTieuThu["LuongDienTieuThu", e.RowIndex].Value.ToString()),
                            WaterConsume = int.Parse(dgvTieuThu["LuongNuocTieuThu", e.RowIndex].Value.ToString()),
                            Time = DateTime.ParseExact(dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            CustomerID = CustomerID
                        };
                        thoiGian = string.Empty;
                        if (e.RowIndex > 0)
                        {
                            thoiGian = "Từ ngày " + dgvTieuThu["ThoiGian", e.RowIndex - 1].Value.ToString() + " đến ngày " + dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString();
                        }
                        else
                        {
                            thoiGian = "Ban đầu" + " đến ngày " + dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString();
                        }
                        mPrice = new PriceLDM().GetElement(new KhachHangLDM().GetElement(CustomerID).PriceID.Value);
                        saveFileDialog.FileName = $"Hóa đơn_{new KhachHangLDM().GetElement(CustomerID).User.DisplayName}_" + dgvTieuThu["ThoiGian", e.RowIndex].Value.ToString().Replace("/", "");
                        saveFileDialog.Title = "Save As";
                        saveFileDialog.Filter = "DocX|*.docx";
                        saveFileDialog.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng thanh toán trước khi tải hóa đơn!");
                    }
                }
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
                erpQLTieuThu.Clear();
                if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
                {
                    erpQLTieuThu.SetError(dtpToDate, "\"Đến ngày:\" phải lớn hơn thời gian \"Từ ngày:\"!");
                    dtpToDate.Focus();
                }
                else
                {
                    ReLoadDataTieuThu(new TieuThuLDM().SearchElements(dtpFromDate.Value.Date, dtpToDate.Value.Date, CustomerID));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
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
        public override void ReSizeDataGridView(DataGridViewAutoSizeColumnMode type)
        {
            for (int i = 0; i < dgvTieuThu.Columns.Count; i++)
            {
                // set size display
                dgvTieuThu.Columns[i].AutoSizeMode = type;
                // set text color header
                
            }
        }

        private void btnBieuDo_Click(object sender, EventArgs e)
        {
            try
            {
                UCBieuDo oUC = new UCBieuDo(mUser, CustomerID, this);
                this.Parent.Controls.Add(oUC);
                this.Parent.Controls.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
