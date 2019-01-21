namespace QLSDDienNuoc
{
    partial class ThemTieuThu
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemTieuThu));
            this.lblChiSoDienMoi = new System.Windows.Forms.Label();
            this.txtChiSoDienMoi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpThoiGian = new System.Windows.Forms.DateTimePicker();
            this.btnLuuLai = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.erpThemTieuThu = new System.Windows.Forms.ErrorProvider(this.components);
            this.label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChiSoNuocMoi = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.erpThemTieuThu)).BeginInit();
            this.SuspendLayout();
            // 
            // lblChiSoDienMoi
            // 
            this.lblChiSoDienMoi.AutoSize = true;
            this.lblChiSoDienMoi.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblChiSoDienMoi.Location = new System.Drawing.Point(31, 98);
            this.lblChiSoDienMoi.Name = "lblChiSoDienMoi";
            this.lblChiSoDienMoi.Size = new System.Drawing.Size(151, 19);
            this.lblChiSoDienMoi.TabIndex = 0;
            this.lblChiSoDienMoi.Text = "Chỉ số điện mới(Kwh):";
            // 
            // txtChiSoDienMoi
            // 
            this.txtChiSoDienMoi.Location = new System.Drawing.Point(188, 98);
            this.txtChiSoDienMoi.Name = "txtChiSoDienMoi";
            this.txtChiSoDienMoi.Size = new System.Drawing.Size(170, 20);
            this.txtChiSoDienMoi.TabIndex = 0;
            this.txtChiSoDienMoi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChiSoDienMoi_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(31, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Thời gian:";
            // 
            // dtpThoiGian
            // 
            this.dtpThoiGian.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThoiGian.Location = new System.Drawing.Point(188, 174);
            this.dtpThoiGian.Name = "dtpThoiGian";
            this.dtpThoiGian.Size = new System.Drawing.Size(170, 20);
            this.dtpThoiGian.TabIndex = 2;
            this.dtpThoiGian.Value = new System.DateTime(2018, 12, 5, 16, 10, 19, 0);
            // 
            // btnLuuLai
            // 
            this.btnLuuLai.Location = new System.Drawing.Point(136, 247);
            this.btnLuuLai.Name = "btnLuuLai";
            this.btnLuuLai.Size = new System.Drawing.Size(97, 48);
            this.btnLuuLai.TabIndex = 3;
            this.btnLuuLai.Text = "Lưu lại";
            this.btnLuuLai.UseVisualStyleBackColor = true;
            this.btnLuuLai.Click += new System.EventHandler(this.btnLuuLai_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(261, 247);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(97, 48);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // erpThemTieuThu
            // 
            this.erpThemTieuThu.ContainerControl = this;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label.Location = new System.Drawing.Point(64, 18);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(237, 29);
            this.label.TabIndex = 12;
            this.label.Text = "Điền thông tin tiêu thụ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(31, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chỉ số nước mới(khối):";
            // 
            // txtChiSoNuocMoi
            // 
            this.txtChiSoNuocMoi.Location = new System.Drawing.Point(188, 135);
            this.txtChiSoNuocMoi.Name = "txtChiSoNuocMoi";
            this.txtChiSoNuocMoi.Size = new System.Drawing.Size(170, 20);
            this.txtChiSoNuocMoi.TabIndex = 1;
            this.txtChiSoNuocMoi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChiSoNuocMoi_KeyPress);
            // 
            // ThemTieuThu
            // 
            this.AcceptButton = this.btnLuuLai;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 316);
            this.Controls.Add(this.label);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnLuuLai);
            this.Controls.Add(this.dtpThoiGian);
            this.Controls.Add(this.txtChiSoNuocMoi);
            this.Controls.Add(this.txtChiSoDienMoi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblChiSoDienMoi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemTieuThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập tiêu thụ mới";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThemTieuThu_FormClosing);
            this.Load += new System.EventHandler(this.ThemTieuThu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erpThemTieuThu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChiSoDienMoi;
        private System.Windows.Forms.TextBox txtChiSoDienMoi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpThoiGian;
        private System.Windows.Forms.Button btnLuuLai;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ErrorProvider erpThemTieuThu;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox txtChiSoNuocMoi;
        private System.Windows.Forms.Label label1;
    }
}