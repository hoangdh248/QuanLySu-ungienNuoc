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
    public partial class ThemTieuThu : Form
    {
        private User mUser;
        public UCquanLyTieuThu mUCQuanLyTieuThu { get; set; }
        private int mCustomerID;
        private Consume EditTieuThu = null;
        public ThemTieuThu(User oUser, int CustomerID)
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
            mCustomerID = CustomerID;
        }
        public ThemTieuThu(User oUser, int CustomerID, Consume EditTieuThu) // form sửa
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
            mCustomerID = CustomerID;
            this.EditTieuThu = EditTieuThu;
        }
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                Consume newConsume = new Consume();
                var lstTieuThu = new TieuThuLDM().GetElements(mCustomerID);
                erpThemTieuThu.Clear();
                if (EditTieuThu == null)
                {
                    if (string.IsNullOrEmpty(txtChiSoDienMoi.Text.Trim()))
                    {
                        erpThemTieuThu.SetError(txtChiSoDienMoi, "Vui lòng điền trường này!");
                        txtChiSoDienMoi.Focus();
                    }
                    else
                    {
                        if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) <= lstTieuThu.Last().NewElectricIndex)
                        {
                            erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải lớn hơn {lstTieuThu.Last().NewElectricIndex}!");
                            txtChiSoDienMoi.Focus();
                        }
                        else
                        {
                            newConsume.NewElectricIndex = int.Parse(txtChiSoDienMoi.Text.Trim());
                            if (string.IsNullOrEmpty(txtChiSoNuocMoi.Text.Trim()))
                            {
                                erpThemTieuThu.SetError(txtChiSoNuocMoi, "Vui lòng điền trường này!");
                                txtChiSoNuocMoi.Focus();
                            }
                            else
                            {
                                if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) <= lstTieuThu.Last().NewWaterIndex)
                                {
                                    erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải lớn hơn {lstTieuThu.Last().NewWaterIndex}!");
                                    txtChiSoNuocMoi.Focus();
                                }
                                else
                                {
                                    newConsume.NewWaterIndex = int.Parse(txtChiSoNuocMoi.Text.Trim());
                                    if (dtpThoiGian.Value == null)
                                    {
                                        erpThemTieuThu.SetError(dtpThoiGian, "Vui lòng điền trường này!");
                                        dtpThoiGian.Focus();
                                    }
                                    else
                                    {
                                        if ((lstTieuThu.Count > 0) && lstTieuThu.Last().Time.Value.Date == DateTime.Now.Date)
                                        {
                                            MessageBox.Show("Không thể thêm tiêu thụ\nBạn đã nhập tiêu thụ có thời gian là hôm nay rồi!");
                                            this.Close();
                                        }
                                        else
                                        {
                                            if (lstTieuThu.Count > 0 && dtpThoiGian.Value.Date <= lstTieuThu.Last().Time.Value.Date)
                                            {
                                                erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian phải sau ngày {lstTieuThu.Last().Time.Value.ToString("dd/MM/yyyy")}!");
                                                dtpThoiGian.Focus();
                                            }
                                            else
                                            {
                                                if (dtpThoiGian.Value.Date > DateTime.Now.Date)
                                                {
                                                    erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian phải trước ngày {DateTime.Now.ToString("dd/MM/yyyy")}!");
                                                    dtpThoiGian.Focus();
                                                }
                                                else
                                                {
                                                    newConsume.Time = dtpThoiGian.Value.Date;
                                                    newConsume.CreatedByID = mUser.ID;
                                                    newConsume.CreatedDate = DateTime.Now.Date;
                                                    if (lstTieuThu.Count > 0)
                                                    {
                                                        newConsume.OldElectricIndex = lstTieuThu.Last().NewElectricIndex;
                                                        newConsume.OldWaterIndex = lstTieuThu.Last().NewWaterIndex;
                                                    }
                                                    else
                                                    {
                                                        newConsume.OldElectricIndex = 0;
                                                        newConsume.OldWaterIndex = 0;
                                                    }
                                                    newConsume.CustomerID = mCustomerID;
                                                    newConsume.ElectricConsume = newConsume.NewElectricIndex - newConsume.OldElectricIndex;
                                                    newConsume.WaterConsume = newConsume.NewWaterIndex - newConsume.OldWaterIndex;
                                                    if (new TieuThuLDM().Insert(newConsume) != null)
                                                    {
                                                        MessageBox.Show("Nhập mới tiêu thụ thành công!");
                                                        mUCQuanLyTieuThu.ReLoadDataTieuThu();
                                                        mUCQuanLyTieuThu.Parent.Parent.Enabled = true;
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Nhập mới tiêu thụ thất bại!");
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
                else
                {
                    var current = lstTieuThu.Where(x => x.ID == EditTieuThu.ID).First();
                    int index = lstTieuThu.FindIndex(x => x.ID == EditTieuThu.ID);
                    Consume obj = new Consume();
                    if (lstTieuThu.Count > 2)
                    {
                        if (current == lstTieuThu.First())
                        {
                            var behind = lstTieuThu[index + 1];
                            if (string.IsNullOrEmpty(txtChiSoDienMoi.Text.Trim()))
                            {
                                erpThemTieuThu.SetError(txtChiSoDienMoi, "Vui lòng điền trường này!");
                                txtChiSoDienMoi.Focus();
                            }
                            else
                            {
                                if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) <= current.OldElectricIndex)
                                {
                                    erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải lớn hơn {current.OldElectricIndex}!");
                                    txtChiSoDienMoi.Focus();
                                }
                                else
                                {
                                    if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) >= behind.NewElectricIndex)
                                    {
                                        erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải nhỏ hơn {behind.NewElectricIndex}!");
                                        txtChiSoDienMoi.Focus();
                                    }
                                    else
                                    {
                                        obj.NewElectricIndex = int.Parse(txtChiSoDienMoi.Text.Trim());
                                        if (string.IsNullOrEmpty(txtChiSoNuocMoi.Text.Trim()))
                                        {
                                            erpThemTieuThu.SetError(txtChiSoNuocMoi, "Vui lòng điền trường này!");
                                            txtChiSoNuocMoi.Focus();
                                        }
                                        else
                                        {
                                            if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) <= current.OldWaterIndex)
                                            {
                                                erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải lớn hơn {current.OldWaterIndex}!");
                                                txtChiSoNuocMoi.Focus();
                                            }
                                            else
                                            {
                                                if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) >= behind.NewWaterIndex)
                                                {
                                                    erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải nhỏ hơn {behind.NewWaterIndex}!");
                                                    txtChiSoNuocMoi.Focus();
                                                }
                                                else
                                                {
                                                    obj.NewWaterIndex = int.Parse(txtChiSoNuocMoi.Text.Trim());
                                                    if (dtpThoiGian.Value.Date >= behind.Time.Value.Date)
                                                    {
                                                        erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian phải trước ngày {behind.Time.Value.Date.ToString("dd/MM/yyyy")}!");
                                                        dtpThoiGian.Focus();
                                                    }
                                                    else
                                                    {
                                                        obj.ElectricConsume = obj.NewElectricIndex - current.OldElectricIndex;
                                                        obj.ID = EditTieuThu.ID;
                                                        obj.ModifiedByID = mUser.ID;
                                                        obj.ModifiedDate = DateTime.Now.Date;
                                                        obj.Time = dtpThoiGian.Value;
                                                        obj.WaterConsume = obj.NewWaterIndex - current.OldWaterIndex;
                                                        if (new TieuThuLDM().Update(obj))
                                                        {
                                                            MessageBox.Show("Cập nhật tiêu thụ thành công!");
                                                            mUCQuanLyTieuThu.ReLoadDataTieuThu();
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Cập nhật tiêu thụ thất bại!");
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
                        else
                        {
                            if (current == lstTieuThu.Last())
                            {
                                var previous = lstTieuThu[index - 1];
                                if (string.IsNullOrEmpty(txtChiSoDienMoi.Text.Trim()))
                                {
                                    erpThemTieuThu.SetError(txtChiSoDienMoi, "Vui lòng điền trường này!");
                                    txtChiSoDienMoi.Focus();
                                }
                                else
                                {
                                    if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) <= previous.NewElectricIndex)
                                    {
                                        erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải lớn hơn {previous.NewElectricIndex}!");
                                        txtChiSoDienMoi.Focus();
                                    }
                                    else
                                    {
                                        obj.NewElectricIndex = int.Parse(txtChiSoDienMoi.Text.Trim());
                                        if (string.IsNullOrEmpty(txtChiSoNuocMoi.Text.Trim()))
                                        {
                                            erpThemTieuThu.SetError(txtChiSoNuocMoi, "Vui lòng điền trường này!");
                                            txtChiSoNuocMoi.Focus();
                                        }
                                        else
                                        {
                                            if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) <= previous.NewWaterIndex)
                                            {
                                                erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải lớn hơn {previous.NewWaterIndex}!");
                                                txtChiSoNuocMoi.Focus();
                                            }
                                            else
                                            {
                                                obj.NewWaterIndex = int.Parse(txtChiSoNuocMoi.Text.Trim());
                                                if (dtpThoiGian.Value.Date <= previous.Time.Value.Date)
                                                {
                                                    erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian phải sau ngày {previous.Time.Value.Date.ToString("dd/MM/yyyy")}!");
                                                    dtpThoiGian.Focus();
                                                }
                                                else
                                                {
                                                    if (dtpThoiGian.Value.Date > DateTime.Now.Date)
                                                    {
                                                        erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian không được vượt quá thời gian hiện tại!");
                                                        dtpThoiGian.Focus();
                                                    }
                                                    else
                                                    {
                                                        obj.ElectricConsume = obj.NewElectricIndex - current.OldElectricIndex;
                                                        obj.ID = EditTieuThu.ID;
                                                        obj.ModifiedByID = mUser.ID;
                                                        obj.ModifiedDate = DateTime.Now;
                                                        obj.Time = dtpThoiGian.Value;
                                                        obj.WaterConsume = obj.NewWaterIndex - current.OldWaterIndex;
                                                        if (new TieuThuLDM().Update(obj))
                                                        {
                                                            MessageBox.Show("Cập nhật tiêu thụ thành công!");
                                                            mUCQuanLyTieuThu.ReLoadDataTieuThu();
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Cập nhật tiêu thụ thất bại!");
                                                        }
                                                        this.Close();
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var behind = lstTieuThu[index + 1];
                                var previous = lstTieuThu[index - 1];
                                if (string.IsNullOrEmpty(txtChiSoDienMoi.Text.Trim()))
                                {
                                    erpThemTieuThu.SetError(txtChiSoDienMoi, "Vui lòng điền trường này!");
                                    txtChiSoDienMoi.Focus();
                                }
                                else
                                {
                                    if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) <= previous.NewElectricIndex)
                                    {
                                        erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải lớn hơn {previous.NewElectricIndex}!");
                                        txtChiSoDienMoi.Focus();
                                    }
                                    else
                                    {
                                        if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) >= behind.NewElectricIndex)
                                        {
                                            erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải nhỏ hơn {behind.NewElectricIndex}!");
                                            txtChiSoDienMoi.Focus();
                                        }
                                        else
                                        {
                                            obj.NewElectricIndex = int.Parse(txtChiSoDienMoi.Text.Trim());
                                            if (string.IsNullOrEmpty(txtChiSoNuocMoi.Text.Trim()))
                                            {
                                                erpThemTieuThu.SetError(txtChiSoNuocMoi, "Vui lòng điền trường này!");
                                                txtChiSoNuocMoi.Focus();
                                            }
                                            else
                                            {
                                                if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) <= previous.NewWaterIndex)
                                                {
                                                    erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải lớn hơn {previous.NewWaterIndex}!");
                                                    txtChiSoNuocMoi.Focus();
                                                }
                                                else
                                                {
                                                    if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) >= behind.NewWaterIndex)
                                                    {
                                                        erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải nhỏ hơn {behind.NewWaterIndex}!");
                                                        txtChiSoNuocMoi.Focus();
                                                    }
                                                    else
                                                    {
                                                        obj.NewWaterIndex = int.Parse(txtChiSoNuocMoi.Text.Trim());
                                                        if (dtpThoiGian.Value.Date <= previous.Time.Value.Date)
                                                        {
                                                            erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian phải sau ngày {previous.Time.Value.Date.ToString("dd/MM/yyyy")}!");
                                                            dtpThoiGian.Focus();
                                                        }
                                                        else
                                                        {
                                                            if (dtpThoiGian.Value.Date >= behind.Time.Value.Date)
                                                            {
                                                                erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian phải trước ngày {behind.Time.Value.Date.ToString("dd/MM/yyyy")}!");
                                                                dtpThoiGian.Focus();
                                                            }
                                                            else
                                                            {
                                                                obj.ElectricConsume = obj.NewElectricIndex - current.OldElectricIndex;
                                                                obj.ID = EditTieuThu.ID;
                                                                obj.ModifiedByID = mUser.ID;
                                                                obj.ModifiedDate = DateTime.Now.Date;
                                                                obj.Time = dtpThoiGian.Value;
                                                                obj.WaterConsume = obj.NewWaterIndex - current.OldWaterIndex;
                                                                if (new TieuThuLDM().Update(obj))
                                                                {
                                                                    MessageBox.Show("Cập nhật tiêu thụ thành công!");
                                                                    mUCQuanLyTieuThu.ReLoadDataTieuThu();
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Cập nhật tiêu thụ thất bại!");
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
                    else
                    {
                        if (lstTieuThu.Count > 1)
                        {
                            if (current == lstTieuThu.First())
                            {
                                var behind = lstTieuThu[index + 1];
                                if (string.IsNullOrEmpty(txtChiSoDienMoi.Text.Trim()))
                                {
                                    erpThemTieuThu.SetError(txtChiSoDienMoi, "Vui lòng điền trường này!");
                                    txtChiSoDienMoi.Focus();
                                }
                                else
                                {
                                    if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) <= current.OldElectricIndex)
                                    {
                                        erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải lớn hơn {current.OldElectricIndex}!");
                                        txtChiSoDienMoi.Focus();
                                    }
                                    else
                                    {
                                        if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) >= behind.NewElectricIndex)
                                        {
                                            erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải nhỏ hơn {behind.NewElectricIndex}!");
                                            txtChiSoDienMoi.Focus();
                                        }
                                        else
                                        {
                                            obj.NewElectricIndex = int.Parse(txtChiSoDienMoi.Text.Trim());
                                            if (string.IsNullOrEmpty(txtChiSoNuocMoi.Text.Trim()))
                                            {
                                                erpThemTieuThu.SetError(txtChiSoNuocMoi, "Vui lòng điền trường này!");
                                                txtChiSoNuocMoi.Focus();
                                            }
                                            else
                                            {
                                                if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) <= current.OldWaterIndex)
                                                {
                                                    erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải lớn hơn {current.OldWaterIndex}!");
                                                    txtChiSoNuocMoi.Focus();
                                                }
                                                else
                                                {
                                                    if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) >= behind.NewWaterIndex)
                                                    {
                                                        erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải nhỏ hơn {behind.NewWaterIndex}!");
                                                        txtChiSoNuocMoi.Focus();
                                                    }
                                                    else
                                                    {
                                                        obj.NewWaterIndex = int.Parse(txtChiSoNuocMoi.Text.Trim());
                                                        if (dtpThoiGian.Value.Date >= behind.Time.Value.Date)
                                                        {
                                                            erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian phải trước ngày {behind.Time.Value.Date.ToString("dd/MM/yyyy")}!");
                                                            dtpThoiGian.Focus();
                                                        }
                                                        else
                                                        {
                                                            obj.ElectricConsume = obj.NewElectricIndex - current.OldElectricIndex;
                                                            obj.ID = EditTieuThu.ID;
                                                            obj.ModifiedByID = mUser.ID;
                                                            obj.ModifiedDate = DateTime.Now.Date;
                                                            obj.Time = dtpThoiGian.Value;
                                                            obj.WaterConsume = obj.NewWaterIndex - current.OldWaterIndex;
                                                            if (new TieuThuLDM().Update(obj))
                                                            {
                                                                MessageBox.Show("Cập nhật tiêu thụ thành công!");
                                                                mUCQuanLyTieuThu.ReLoadDataTieuThu();
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Cập nhật tiêu thụ thất bại!");
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
                            else
                            {
                                if (current == lstTieuThu.Last())
                                {
                                    var previous = lstTieuThu[index - 1];
                                    if (string.IsNullOrEmpty(txtChiSoDienMoi.Text.Trim()))
                                    {
                                        erpThemTieuThu.SetError(txtChiSoDienMoi, "Vui lòng điền trường này!");
                                        txtChiSoDienMoi.Focus();
                                    }
                                    else
                                    {
                                        if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) <= previous.NewElectricIndex)
                                        {
                                            erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải lớn hơn {previous.NewElectricIndex}!");
                                            txtChiSoDienMoi.Focus();
                                        }
                                        else
                                        {
                                            obj.NewElectricIndex = int.Parse(txtChiSoDienMoi.Text.Trim());
                                            if (string.IsNullOrEmpty(txtChiSoNuocMoi.Text.Trim()))
                                            {
                                                erpThemTieuThu.SetError(txtChiSoNuocMoi, "Vui lòng điền trường này!");
                                                txtChiSoNuocMoi.Focus();
                                            }
                                            else
                                            {
                                                if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) <= previous.NewWaterIndex)
                                                {
                                                    erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải lớn hơn {previous.NewWaterIndex}!");
                                                    txtChiSoNuocMoi.Focus();
                                                }
                                                else
                                                {
                                                    obj.NewWaterIndex = int.Parse(txtChiSoNuocMoi.Text.Trim());
                                                    if (dtpThoiGian.Value.Date <= previous.Time.Value.Date)
                                                    {
                                                        erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian phải sau ngày {previous.Time.Value.Date.ToString("dd/MM/yyyy")}!");
                                                        dtpThoiGian.Focus();
                                                    }
                                                    else
                                                    {
                                                        if (dtpThoiGian.Value.Date > DateTime.Now.Date)
                                                        {
                                                            erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian không được vượt quá thời gian hiện tại!");
                                                            dtpThoiGian.Focus();
                                                        }
                                                        else
                                                        {
                                                            obj.ElectricConsume = obj.NewElectricIndex - current.OldElectricIndex;
                                                            obj.ID = EditTieuThu.ID;
                                                            obj.ModifiedByID = mUser.ID;
                                                            obj.ModifiedDate = DateTime.Now.Date;
                                                            obj.Time = dtpThoiGian.Value;
                                                            obj.WaterConsume = obj.NewWaterIndex - current.OldWaterIndex;
                                                            if (new TieuThuLDM().Update(obj))
                                                            {
                                                                MessageBox.Show("Cập nhật tiêu thụ thành công!");
                                                                mUCQuanLyTieuThu.ReLoadDataTieuThu();
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Cập nhật tiêu thụ thất bại!");
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
                        else
                        {
                            if (lstTieuThu.Count > 0)
                            {
                                if (current == lstTieuThu.First())
                                {
                                    if (string.IsNullOrEmpty(txtChiSoDienMoi.Text.Trim()))
                                    {
                                        erpThemTieuThu.SetError(txtChiSoDienMoi, "Vui lòng điền trường này!");
                                        txtChiSoDienMoi.Focus();
                                    }
                                    else
                                    {
                                        if (lstTieuThu.Count > 0 && int.Parse(txtChiSoDienMoi.Text.Trim()) <= current.OldElectricIndex)
                                        {
                                            erpThemTieuThu.SetError(txtChiSoDienMoi, $"Chỉ số phải lớn hơn {current.OldElectricIndex}!");
                                            txtChiSoDienMoi.Focus();
                                        }
                                        else
                                        {
                                            obj.NewElectricIndex = int.Parse(txtChiSoDienMoi.Text.Trim());
                                            if (string.IsNullOrEmpty(txtChiSoNuocMoi.Text.Trim()))
                                            {
                                                erpThemTieuThu.SetError(txtChiSoNuocMoi, "Vui lòng điền trường này!");
                                                txtChiSoNuocMoi.Focus();
                                            }
                                            else
                                            {
                                                if (lstTieuThu.Count > 0 && int.Parse(txtChiSoNuocMoi.Text.Trim()) <= current.OldWaterIndex)
                                                {
                                                    erpThemTieuThu.SetError(txtChiSoNuocMoi, $"Chỉ số phải lớn hơn {current.OldWaterIndex}!");
                                                    txtChiSoNuocMoi.Focus();
                                                }
                                                else
                                                {
                                                    obj.NewWaterIndex = int.Parse(txtChiSoNuocMoi.Text.Trim());
                                                    if (dtpThoiGian.Value.Date > DateTime.Now.Date)
                                                    {
                                                        erpThemTieuThu.SetError(dtpThoiGian, $"Thời gian không được vượt quá thời gian hiện tại!");
                                                        dtpThoiGian.Focus();
                                                    }
                                                    else
                                                    {
                                                        obj.ElectricConsume = obj.NewElectricIndex - current.OldElectricIndex;
                                                        obj.ID = EditTieuThu.ID;
                                                        obj.ModifiedByID = mUser.ID;
                                                        obj.ModifiedDate = DateTime.Now.Date;
                                                        obj.Time = dtpThoiGian.Value;
                                                        obj.WaterConsume = obj.NewWaterIndex - current.OldWaterIndex;
                                                        if (new TieuThuLDM().Update(obj))
                                                        {
                                                            MessageBox.Show("Cập nhật tiêu thụ thành công!");
                                                            mUCQuanLyTieuThu.ReLoadDataTieuThu();
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Cập nhật tiêu thụ thất bại!");
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
        private void ThemTieuThu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mUCQuanLyTieuThu.Parent.Parent.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void ThemTieuThu_Load(object sender, EventArgs e)
        {
            try
            {
                dtpThoiGian.CustomFormat = "dd/MM/yyyy";
                if (EditTieuThu == null)
                {
                    var lstTieuThu = new TieuThuLDM().GetElements(mCustomerID);
                    if (lstTieuThu.Count > 0 && EditTieuThu == null)
                    {
                        dtpThoiGian.Value = lstTieuThu.Last().Time.Value.AddMonths(1);
                    }
                }
                else
                {
                    txtChiSoDienMoi.Text = EditTieuThu.NewElectricIndex.ToString();
                    txtChiSoNuocMoi.Text = EditTieuThu.NewWaterIndex.ToString();
                    dtpThoiGian.Value = EditTieuThu.Time.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void txtChiSoDienMoi_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtChiSoNuocMoi_KeyPress(object sender, KeyPressEventArgs e)
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
