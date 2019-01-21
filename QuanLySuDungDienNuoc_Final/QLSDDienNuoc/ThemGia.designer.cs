namespace QLSDDienNuoc
{
    partial class ThemGia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemGia));
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLuuLai = new System.Windows.Forms.Button();
            this.txtTenLoaiGia = new System.Windows.Forms.TextBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.erpThemGia = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtGiaDien = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGiaNuoc = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.erpThemGia)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(269, 263);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(85, 46);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnLuuLai
            // 
            this.btnLuuLai.Location = new System.Drawing.Point(179, 263);
            this.btnLuuLai.Name = "btnLuuLai";
            this.btnLuuLai.Size = new System.Drawing.Size(72, 46);
            this.btnLuuLai.TabIndex = 3;
            this.btnLuuLai.Text = "Lưu lại";
            this.btnLuuLai.UseVisualStyleBackColor = true;
            this.btnLuuLai.Click += new System.EventHandler(this.btnLuuLai_Click);
            // 
            // txtTenLoaiGia
            // 
            this.txtTenLoaiGia.Location = new System.Drawing.Point(130, 101);
            this.txtTenLoaiGia.Name = "txtTenLoaiGia";
            this.txtTenLoaiGia.Size = new System.Drawing.Size(225, 20);
            this.txtTenLoaiGia.TabIndex = 0;
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenDangNhap.Location = new System.Drawing.Point(12, 100);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(88, 19);
            this.lblTenDangNhap.TabIndex = 17;
            this.lblTenDangNhap.Text = "Tên loại giá:";
            // 
            // erpThemGia
            // 
            this.erpThemGia.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(12, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "Giá điện(đồng):";
            // 
            // txtGiaDien
            // 
            this.txtGiaDien.Location = new System.Drawing.Point(130, 145);
            this.txtGiaDien.Name = "txtGiaDien";
            this.txtGiaDien.Size = new System.Drawing.Size(225, 20);
            this.txtGiaDien.TabIndex = 1;
            this.txtGiaDien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGiaDien_KeyPress);
            this.txtGiaDien.Leave += new System.EventHandler(this.txtGiaDien_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 19);
            this.label2.TabIndex = 17;
            this.label2.Text = "Giá nước(đồng):";
            // 
            // txtGiaNuoc
            // 
            this.txtGiaNuoc.Location = new System.Drawing.Point(130, 189);
            this.txtGiaNuoc.Name = "txtGiaNuoc";
            this.txtGiaNuoc.Size = new System.Drawing.Size(225, 20);
            this.txtGiaNuoc.TabIndex = 2;
            this.txtGiaNuoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGiaNuoc_KeyPress);
            this.txtGiaNuoc.Leave += new System.EventHandler(this.txtGiaNuoc_Leave);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(77, 35);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(235, 29);
            this.label.TabIndex = 18;
            this.label.Text = "Điền thông tin đơn giá";
            // 
            // ThemGia
            // 
            this.AcceptButton = this.btnLuuLai;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 344);
            this.Controls.Add(this.label);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnLuuLai);
            this.Controls.Add(this.txtGiaNuoc);
            this.Controls.Add(this.txtGiaDien);
            this.Controls.Add(this.txtTenLoaiGia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTenDangNhap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm đơn giá";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThemGia_FormClosing);
            this.Load += new System.EventHandler(this.ThemGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erpThemGia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLuuLai;
        private System.Windows.Forms.TextBox txtTenLoaiGia;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.ErrorProvider erpThemGia;
        private System.Windows.Forms.TextBox txtGiaNuoc;
        private System.Windows.Forms.TextBox txtGiaDien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label;
    }
}