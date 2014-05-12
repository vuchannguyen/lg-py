namespace Weedon
{
    partial class UcSanPhamIndex
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
            this.pbSanPham = new System.Windows.Forms.PictureBox();
            this.pbNhomSanPham = new System.Windows.Forms.PictureBox();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNhomSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.lbNhomSanPham);
            this.pnSelect.Controls.Add(this.lbSanPham);
            this.pnSelect.Controls.Add(this.pbSanPham);
            this.pnSelect.Controls.Add(this.pbNhomSanPham);
            this.pnSelect.Location = new System.Drawing.Point(143, 228);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(710, 140);
            this.pnSelect.TabIndex = 5;
            // 
            // lbNhomSanPham
            // 
            this.lbNhomSanPham.AutoSize = true;
            this.lbNhomSanPham.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNhomSanPham.ForeColor = System.Drawing.Color.Gray;
            this.lbNhomSanPham.Location = new System.Drawing.Point(516, 120);
            this.lbNhomSanPham.Name = "lbNhomSanPham";
            this.lbNhomSanPham.Size = new System.Drawing.Size(129, 16);
            this.lbNhomSanPham.TabIndex = 6;
            this.lbNhomSanPham.Text = "NHÓM SẢN PHẨM";
            this.lbNhomSanPham.Visible = false;
            this.lbNhomSanPham.Click += new System.EventHandler(this.lbNhomSanPham_Click);
            // 
            // lbSanPham
            // 
            this.lbSanPham.AutoSize = true;
            this.lbSanPham.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSanPham.ForeColor = System.Drawing.Color.Gray;
            this.lbSanPham.Location = new System.Drawing.Point(310, 120);
            this.lbSanPham.Name = "lbSanPham";
            this.lbSanPham.Size = new System.Drawing.Size(81, 16);
            this.lbSanPham.TabIndex = 4;
            this.lbSanPham.Text = "SẢN PHẨM";
            // 
            // pbSanPham
            // 
            this.pbSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSanPham.Location = new System.Drawing.Point(300, 10);
            this.pbSanPham.Name = "pbSanPham";
            this.pbSanPham.Size = new System.Drawing.Size(100, 100);
            this.pbSanPham.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSanPham.TabIndex = 0;
            this.pbSanPham.TabStop = false;
            this.pbSanPham.Click += new System.EventHandler(this.pbSanPham_Click);
            this.pbSanPham.MouseEnter += new System.EventHandler(this.pbSanPham_MouseEnter);
            this.pbSanPham.MouseLeave += new System.EventHandler(this.pbSanPham_MouseLeave);
            // 
            // pbNhomSanPham
            // 
            this.pbNhomSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNhomSanPham.Location = new System.Drawing.Point(528, 10);
            this.pbNhomSanPham.Name = "pbNhomSanPham";
            this.pbNhomSanPham.Size = new System.Drawing.Size(100, 100);
            this.pbNhomSanPham.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNhomSanPham.TabIndex = 2;
            this.pbNhomSanPham.TabStop = false;
            this.pbNhomSanPham.Visible = false;
            this.pbNhomSanPham.Click += new System.EventHandler(this.pbNhomSanPham_Click);
            this.pbNhomSanPham.MouseEnter += new System.EventHandler(this.pbNhomSanPham_MouseEnter);
            this.pbNhomSanPham.MouseLeave += new System.EventHandler(this.pbNhomSanPham_MouseLeave);
            // 
            // UcSanPhamIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcSanPhamIndex";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcSanPhamIndex_Load);
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNhomSanPham)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbNhomSanPham;
        private System.Windows.Forms.Label lbSanPham;
        private System.Windows.Forms.PictureBox pbSanPham;
        private System.Windows.Forms.PictureBox pbNhomSanPham;
    }
}
