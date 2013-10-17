namespace Weedon
{
    partial class UcKhachHangIndex
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
            this.pnSelect = new System.Windows.Forms.Panel();
            this.lbNhomSanPham = new System.Windows.Forms.Label();
            this.lbSanPham = new System.Windows.Forms.Label();
            this.pbKhachHang = new System.Windows.Forms.PictureBox();
            this.pbKhachHangGroup = new System.Windows.Forms.PictureBox();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbKhachHangGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.lbNhomSanPham);
            this.pnSelect.Controls.Add(this.lbSanPham);
            this.pnSelect.Controls.Add(this.pbKhachHang);
            this.pnSelect.Controls.Add(this.pbKhachHangGroup);
            this.pnSelect.Location = new System.Drawing.Point(230, 228);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(520, 140);
            this.pnSelect.TabIndex = 5;
            // 
            // lbNhomSanPham
            // 
            this.lbNhomSanPham.AutoSize = true;
            this.lbNhomSanPham.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNhomSanPham.ForeColor = System.Drawing.Color.Gray;
            this.lbNhomSanPham.Location = new System.Drawing.Point(366, 120);
            this.lbNhomSanPham.Name = "lbNhomSanPham";
            this.lbNhomSanPham.Size = new System.Drawing.Size(151, 16);
            this.lbNhomSanPham.TabIndex = 6;
            this.lbNhomSanPham.Text = "NHÓM KHÁCH HÀNG";
            // 
            // lbSanPham
            // 
            this.lbSanPham.AutoSize = true;
            this.lbSanPham.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSanPham.ForeColor = System.Drawing.Color.Gray;
            this.lbSanPham.Location = new System.Drawing.Point(29, 120);
            this.lbSanPham.Name = "lbSanPham";
            this.lbSanPham.Size = new System.Drawing.Size(103, 16);
            this.lbSanPham.TabIndex = 4;
            this.lbSanPham.Text = "KHÁCH HÀNG";
            // 
            // pbKhachHang
            // 
            this.pbKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbKhachHang.Location = new System.Drawing.Point(30, 10);
            this.pbKhachHang.Name = "pbKhachHang";
            this.pbKhachHang.Size = new System.Drawing.Size(100, 100);
            this.pbKhachHang.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbKhachHang.TabIndex = 0;
            this.pbKhachHang.TabStop = false;
            this.pbKhachHang.Click += new System.EventHandler(this.pbKhachHang_Click);
            this.pbKhachHang.MouseEnter += new System.EventHandler(this.pbKhachHang_MouseEnter);
            this.pbKhachHang.MouseLeave += new System.EventHandler(this.pbKhachHang_MouseLeave);
            // 
            // pbKhachHangGroup
            // 
            this.pbKhachHangGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbKhachHangGroup.Location = new System.Drawing.Point(390, 10);
            this.pbKhachHangGroup.Name = "pbKhachHangGroup";
            this.pbKhachHangGroup.Size = new System.Drawing.Size(100, 100);
            this.pbKhachHangGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbKhachHangGroup.TabIndex = 2;
            this.pbKhachHangGroup.TabStop = false;
            this.pbKhachHangGroup.Click += new System.EventHandler(this.pbKhachHangGroup_Click);
            this.pbKhachHangGroup.MouseEnter += new System.EventHandler(this.pbKhachHangGroup_MouseEnter);
            this.pbKhachHangGroup.MouseLeave += new System.EventHandler(this.pbKhachHangGroup_MouseLeave);
            // 
            // UcKhachHangIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcKhachHangIndex";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcKhachHangIndex_Load);
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbKhachHangGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbNhomSanPham;
        private System.Windows.Forms.Label lbSanPham;
        private System.Windows.Forms.PictureBox pbKhachHang;
        private System.Windows.Forms.PictureBox pbKhachHangGroup;
    }
}
