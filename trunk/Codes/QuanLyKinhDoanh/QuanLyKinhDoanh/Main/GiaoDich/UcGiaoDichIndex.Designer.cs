namespace QuanLyKinhDoanh
{
    partial class UcMuaBanIndex
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
            this.lbThanhToan = new System.Windows.Forms.Label();
            this.lbNhapKho = new System.Windows.Forms.Label();
            this.pbNhapKho = new System.Windows.Forms.PictureBox();
            this.pbThanhToan = new System.Windows.Forms.PictureBox();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNhapKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThanhToan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.lbThanhToan);
            this.pnSelect.Controls.Add(this.lbNhapKho);
            this.pnSelect.Controls.Add(this.pbNhapKho);
            this.pnSelect.Controls.Add(this.pbThanhToan);
            this.pnSelect.Location = new System.Drawing.Point(220, 230);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(560, 140);
            this.pnSelect.TabIndex = 7;
            // 
            // lbThanhToan
            // 
            this.lbThanhToan.AutoSize = true;
            this.lbThanhToan.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbThanhToan.ForeColor = System.Drawing.Color.Gray;
            this.lbThanhToan.Location = new System.Drawing.Point(440, 120);
            this.lbThanhToan.Name = "lbThanhToan";
            this.lbThanhToan.Size = new System.Drawing.Size(98, 16);
            this.lbThanhToan.TabIndex = 6;
            this.lbThanhToan.Text = "THANH TOÁN";
            // 
            // lbNhapKho
            // 
            this.lbNhapKho.AutoSize = true;
            this.lbNhapKho.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNhapKho.ForeColor = System.Drawing.Color.Gray;
            this.lbNhapKho.Location = new System.Drawing.Point(26, 120);
            this.lbNhapKho.Name = "lbNhapKho";
            this.lbNhapKho.Size = new System.Drawing.Size(81, 16);
            this.lbNhapKho.TabIndex = 4;
            this.lbNhapKho.Text = "NHẬP KHO";
            // 
            // pbNhapKho
            // 
            this.pbNhapKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNhapKho.Location = new System.Drawing.Point(17, 10);
            this.pbNhapKho.Name = "pbNhapKho";
            this.pbNhapKho.Size = new System.Drawing.Size(100, 100);
            this.pbNhapKho.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNhapKho.TabIndex = 0;
            this.pbNhapKho.TabStop = false;
            this.pbNhapKho.Click += new System.EventHandler(this.pbNhapKho_Click);
            this.pbNhapKho.MouseEnter += new System.EventHandler(this.pbNhapKho_MouseEnter);
            this.pbNhapKho.MouseLeave += new System.EventHandler(this.pbNhapKho_MouseLeave);
            // 
            // pbThanhToan
            // 
            this.pbThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbThanhToan.Location = new System.Drawing.Point(439, 10);
            this.pbThanhToan.Name = "pbThanhToan";
            this.pbThanhToan.Size = new System.Drawing.Size(100, 100);
            this.pbThanhToan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThanhToan.TabIndex = 2;
            this.pbThanhToan.TabStop = false;
            this.pbThanhToan.Click += new System.EventHandler(this.pbThanhToan_Click);
            this.pbThanhToan.MouseEnter += new System.EventHandler(this.pbThanhToan_MouseEnter);
            this.pbThanhToan.MouseLeave += new System.EventHandler(this.pbThanhToan_MouseLeave);
            // 
            // UcMuaBanIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcMuaBanIndex";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcGiaoDich_Load);
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNhapKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThanhToan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbThanhToan;
        private System.Windows.Forms.Label lbNhapKho;
        private System.Windows.Forms.PictureBox pbNhapKho;
        private System.Windows.Forms.PictureBox pbThanhToan;
    }
}
