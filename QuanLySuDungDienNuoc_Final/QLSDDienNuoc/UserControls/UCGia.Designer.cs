namespace QLSDDienNuoc.UserControls
{
    partial class UCGia
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnThemGia = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvGia = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGia)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.btnThemGia);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1040, 60);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(512, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Location = new System.Drawing.Point(596, 19);
            this.txtTimKiem.Multiline = true;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(235, 28);
            this.txtTimKiem.TabIndex = 11;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // btnThemGia
            // 
            this.btnThemGia.BackColor = System.Drawing.Color.SteelBlue;
            this.btnThemGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemGia.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThemGia.ForeColor = System.Drawing.Color.White;
            this.btnThemGia.Image = global::QLSDDienNuoc.Properties.Resources.icons8_plus_math_16;
            this.btnThemGia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThemGia.Location = new System.Drawing.Point(410, 19);
            this.btnThemGia.Name = "btnThemGia";
            this.btnThemGia.Size = new System.Drawing.Size(73, 28);
            this.btnThemGia.TabIndex = 10;
            this.btnThemGia.Text = "Thêm";
            this.btnThemGia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThemGia.UseVisualStyleBackColor = false;
            this.btnThemGia.Click += new System.EventHandler(this.btnThemGia_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 33);
            this.label1.TabIndex = 9;
            this.label1.Text = "QUẢN LÝ ĐƠN GIÁ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvGia);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1040, 404);
            this.panel2.TabIndex = 1;
            // 
            // dgvGia
            // 
            this.dgvGia.AllowUserToAddRows = false;
            this.dgvGia.AllowUserToDeleteRows = false;
            this.dgvGia.BackgroundColor = System.Drawing.Color.White;
            this.dgvGia.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGia.Location = new System.Drawing.Point(0, 0);
            this.dgvGia.Name = "dgvGia";
            this.dgvGia.ReadOnly = true;
            this.dgvGia.RowHeadersVisible = false;
            this.dgvGia.Size = new System.Drawing.Size(1040, 404);
            this.dgvGia.TabIndex = 2;
            this.dgvGia.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGia_CellClick);
            this.dgvGia.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvGia_RowPostPaint);
            // 
            // UCGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UCGia";
            this.Size = new System.Drawing.Size(1040, 464);
            this.Load += new System.EventHandler(this.UCGia_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnThemGia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvGia;
    }
}
