using Commons;
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
    public partial class PhanQuyen : Form
    {
        private User mUser;
        private int mUserID;
        public UCNguoiDung mUC { get; set; }
        public PhanQuyen(User oUser, int UserID)
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
            mUserID = UserID;
        }
        private void PhanQuyen_Load(object sender, EventArgs e)
        {
            try
            {
                lblTenNhanVien.Text = new UserLDM().GetElement(mUserID).DisplayName;
                dgvPhanQuyen.DataSource = Converts.ConvertToDatatable(new User_RoleLDM().GetElements(mUserID).Select(o => new
                {
                    TenQuyen = new QuyenLDM().GetElement(o.RoleID).RoleName,
                    qXem = o.isView,
                    qThem = o.isAdd,
                    qSua = o.isEdit,
                    qXoa = o.isRemove,
                    qID = o.RoleID,
                    ndID = o.UserID,
                    ThemBoi = o.CreatedByID == null ? " " : new UserLDM().GetElement(o.CreatedByID.Value) == null ? " " : new UserLDM().GetElement(o.CreatedByID.Value).DisplayName + " " + o.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    SuaBoi = o.ModifiedByID == null ? " " : new UserLDM().GetElement(o.ModifiedByID.Value) == null ? " " : new UserLDM().GetElement(o.ModifiedByID.Value).DisplayName + " " + o.ModifiedDate.Value.ToString("dd/MM/yyyy")
                }).ToList());

                dgvPhanQuyen.Columns["TenQuyen"].HeaderText = "Tên quyền";
                dgvPhanQuyen.Columns["TenQuyen"].ReadOnly = true;
                dgvPhanQuyen.Columns["qXem"].HeaderText = "Xem";
                dgvPhanQuyen.Columns["qSua"].HeaderText = "Sửa";
                dgvPhanQuyen.Columns["qSua"].ReadOnly = true;
                dgvPhanQuyen.Columns["qXoa"].HeaderText = "Xóa";
                dgvPhanQuyen.Columns["qXoa"].ReadOnly = true;
                dgvPhanQuyen.Columns["qThem"].HeaderText = "Thêm";
                dgvPhanQuyen.Columns["qThem"].ReadOnly = true;
                dgvPhanQuyen.Columns["qID"].Visible = false;
                dgvPhanQuyen.Columns["ndID"].Visible = false;
                dgvPhanQuyen.Columns["ThemBoi"].HeaderText = "Thêm bởi";
                dgvPhanQuyen.Columns["ThemBoi"].ReadOnly = true;
                dgvPhanQuyen.Columns["SuaBoi"].HeaderText = "Sửa bởi";
                dgvPhanQuyen.Columns["SuaBoi"].ReadOnly = true;
                for (int i = 0; i < dgvPhanQuyen.Columns.Count; i++)
                {
                    // set size display
                    dgvPhanQuyen.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    // set text color header
                    dgvPhanQuyen.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                    //set alignment middle
                    dgvPhanQuyen.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvPhanQuyen.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                dgvPhanQuyen.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
                dgvPhanQuyen.EnableHeadersVisualStyles = false;

                DataGridViewTextBoxColumn stt = new DataGridViewTextBoxColumn();
                stt.Name = "STT";
                stt.HeaderText = "STT";
                dgvPhanQuyen.Columns.Insert(0, stt);
                dgvPhanQuyen.Columns["STT"].Width = 30;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void PhanQuyen_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dgvPhanQuyen_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                dgvPhanQuyen.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
                if (bool.Parse(dgvPhanQuyen.Rows[e.RowIndex].Cells["qXem"].Value.ToString()))
                {
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qXoa"].ReadOnly = false;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qSua"].ReadOnly = false;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qThem"].ReadOnly = false;

                }
                else
                {
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qXoa"].ReadOnly = true;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qSua"].ReadOnly = true;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qThem"].ReadOnly = true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvPhanQuyen.Rows.Count; i++)
                {
                    var mUR = new Users_Roles
                    {
                        isAdd = bool.Parse(dgvPhanQuyen["qThem", i].Value.ToString()),
                        isEdit = bool.Parse(dgvPhanQuyen["qSua", i].Value.ToString()),
                        isRemove = bool.Parse(dgvPhanQuyen["qXoa", i].Value.ToString()),
                        isView = bool.Parse(dgvPhanQuyen["qXem", i].Value.ToString()),
                        ModifiedByID = mUser.ID,
                        ModifiedDate = DateTime.Now.Date,
                        RoleID = int.Parse(dgvPhanQuyen["qID", i].Value.ToString()),
                        UserID = int.Parse(dgvPhanQuyen["ndID", i].Value.ToString()),
                    };
                    new User_RoleLDM().Update(mUR);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgvPhanQuyen_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0) // qXem
            {
                if (bool.Parse(dgvPhanQuyen["qXem", e.RowIndex].Value.ToString()))
                {
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qXoa"].ReadOnly = false;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qSua"].ReadOnly = false;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qThem"].ReadOnly = false;

                }
                else
                {
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qXoa"].ReadOnly = true;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qSua"].ReadOnly = true;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qThem"].ReadOnly = true;

                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qXoa"].Value = false;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qSua"].Value = false;
                    dgvPhanQuyen.Rows[e.RowIndex].Cells["qThem"].Value = false;
                }
            }
        }
    }
}
