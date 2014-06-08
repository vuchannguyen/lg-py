namespace Weedon
{
    partial class UcNguonCungCapIndex
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
            this.lbNguonCungCap = new System.Windows.Forms.Label();
            this.pbNguonCungCap = new System.Windows.Forms.PictureBox();
            this.pbNguonCungCapGroup = new System.Windows.Forms.PictureBox();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNguonCungCap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNguonCungCapGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.lbNhomSanPham);
            this.pnSelect.Controls.Add(this.lbNguonCungCap);
            this.pnSelect.Controls.Add(this.pbNguonCungCap);
            this.pnSelect.Controls.Add(this.pbNguonCungCapGroup);
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
            this.lbNhomSanPham.Visible = false;
            // 
            // lbNguonCungCap
            // 
            this.lbNguonCungCap.AutoSize = true;
            this.lbNguonCungCap.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNguonCungCap.ForeColor = System.Drawing.Color.Gray;
            this.lbNguonCungCap.Location = new System.Drawing.Point(183, 120);
            this.lbNguonCungCap.Name = "lbNguonCungCap";
            this.lbNguonCungCap.Size = new System.Drawing.Size(137, 16);
            this.lbNguonCungCap.TabIndex = 4;
            this.lbNguonCungCap.Text = "NGUỒN CUNG CẤP";
            // 
            // pbNguonCungCap
            // 
            this.pbNguonCungCap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNguonCungCap.Location = new System.Drawing.Point(200, 10);
            this.pbNguonCungCap.Name = "pbNguonCungCap";
            this.pbNguonCungCap.Size = new System.Drawing.Size(100, 100);
            this.pbNguonCungCap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNguonCungCap.TabIndex = 0;
            this.pbNguonCungCap.TabStop = false;
            this.pbNguonCungCap.Click += new System.EventHandler(this.pbNguonCungCap_Click);
            this.pbNguonCungCap.MouseEnter += new System.EventHandler(this.pbNguonCungCap_MouseEnter);
            this.pbNguonCungCap.MouseLeave += new System.EventHandler(this.pbNguonCungCap_MouseLeave);
            // 
            // pbNguonCungCapGroup
            // 
            this.pbNguonCungCapGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNguonCungCapGroup.Location = new System.Drawing.Point(390, 10);
            this.pbNguonCungCapGroup.Name = "pbNguonCungCapGroup";
            this.pbNguonCungCapGroup.Size = new System.Drawing.Size(100, 100);
            this.pbNguonCungCapGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNguonCungCapGroup.TabIndex = 2;
            this.pbNguonCungCapGroup.TabStop = false;
            // 
            // UcNguonCungCapIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcNguonCungCapIndex";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcNguonCungCapIndex_Load);
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNguonCungCap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNguonCungCapGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbNhomSanPham;
        private System.Windows.Forms.Label lbNguonCungCap;
        private System.Windows.Forms.PictureBox pbNguonCungCap;
        private System.Windows.Forms.PictureBox pbNguonCungCapGroup;
    }
}
