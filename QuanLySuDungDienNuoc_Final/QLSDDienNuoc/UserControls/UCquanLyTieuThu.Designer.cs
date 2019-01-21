namespace QLSDDienNuoc.UserControls
{
    partial class UCquanLyTieuThu
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCquanLyTieuThu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnBieuDo = new System.Windows.Forms.Button();
            this.btnThemTieuThu = new System.Windows.Forms.Button();
            this.dgvTieuThu = new System.Windows.Forms.DataGridView();
            this.erpQLTieuThu = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTieuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpQLTieuThu)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpToDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnQuayLai);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.btnBieuDo);
            this.panel1.Controls.Add(this.btnThemTieuThu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1273, 60);
            this.panel1.TabIndex = 1;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(882, 29);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(103, 20);
            this.dtpToDate.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(805, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Đến ngày:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(696, 31);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(103, 20);
            this.dtpFromDate.TabIndex = 7;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(626, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Từ ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(95, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 33);
            this.label1.TabIndex = 5;
            this.label1.Text = "QUẢN LÝ TIÊU THỤ";
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.BackColor = System.Drawing.Color.SteelBlue;
            this.btnQuayLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuayLai.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.Image = ((System.Drawing.Image)(resources.GetObject("btnQuayLai.Image")));
            this.btnQuayLai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuayLai.Location = new System.Drawing.Point(0, 19);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(90, 38);
            this.btnQuayLai.TabIndex = 3;
            this.btnQuayLai.Text = "Quay lại";
            this.btnQuayLai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTimKiem.Location = new System.Drawing.Point(997, 24);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 28);
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnBieuDo
            // 
            this.btnBieuDo.BackColor = System.Drawing.Color.SteelBlue;
            this.btnBieuDo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBieuDo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBieuDo.ForeColor = System.Drawing.Color.White;
            this.btnBieuDo.Image = global::QLSDDienNuoc.Properties.Resources.icons8_plus_math_16;
            this.btnBieuDo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBieuDo.Location = new System.Drawing.Point(379, 23);
            this.btnBieuDo.Name = "btnBieuDo";
            this.btnBieuDo.Size = new System.Drawing.Size(105, 28);
            this.btnBieuDo.TabIndex = 3;
            this.btnBieuDo.Text = "Biểu đồ TK";
            this.btnBieuDo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBieuDo.UseVisualStyleBackColor = false;
            this.btnBieuDo.Click += new System.EventHandler(this.btnBieuDo_Click);
            // 
            // btnThemTieuThu
            // 
            this.btnThemTieuThu.BackColor = System.Drawing.Color.SteelBlue;
            this.btnThemTieuThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemTieuThu.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThemTieuThu.ForeColor = System.Drawing.Color.White;
            this.btnThemTieuThu.Image = global::QLSDDienNuoc.Properties.Resources.icons8_plus_math_16;
            this.btnThemTieuThu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThemTieuThu.Location = new System.Drawing.Point(502, 23);
            this.btnThemTieuThu.Name = "btnThemTieuThu";
            this.btnThemTieuThu.Size = new System.Drawing.Size(104, 28);
            this.btnThemTieuThu.TabIndex = 3;
            this.btnThemTieuThu.Text = "Nhập mới";
            this.btnThemTieuThu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThemTieuThu.UseVisualStyleBackColor = false;
            this.btnThemTieuThu.Click += new System.EventHandler(this.btnThemTieuThu_Click);
            // 
            // dgvTieuThu
            // 
            this.dgvTieuThu.AllowUserToAddRows = false;
            this.dgvTieuThu.AllowUserToDeleteRows = false;
            this.dgvTieuThu.BackgroundColor = System.Drawing.Color.White;
            this.dgvTieuThu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvTieuThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTieuThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTieuThu.Location = new System.Drawing.Point(0, 60);
            this.dgvTieuThu.Name = "dgvTieuThu";
            this.dgvTieuThu.ReadOnly = true;
            this.dgvTieuThu.RowHeadersVisible = false;
            this.dgvTieuThu.Size = new System.Drawing.Size(1273, 616);
            this.dgvTieuThu.TabIndex = 2;
            this.dgvTieuThu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTieuThu_CellClick);
            this.dgvTieuThu.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvTieuThu_RowPostPaint);
            // 
            // erpQLTieuThu
            // 
            this.erpQLTieuThu.ContainerControl = this;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // UCquanLyTieuThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTieuThu);
            this.Controls.Add(this.panel1);
            this.Name = "UCquanLyTieuThu";
            this.Size = new System.Drawing.Size(1273, 676);
            this.Load += new System.EventHandler(this.UCquanLyTieuThu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTieuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpQLTieuThu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThemTieuThu;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvTieuThu;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.ErrorProvider erpQLTieuThu;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnBieuDo;
    }
}
