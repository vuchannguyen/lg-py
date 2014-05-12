namespace Weedon
{
    partial class UcNguyenLieuIndex
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
            this.lbDinhLuong = new System.Windows.Forms.Label();
            this.pbNguyenLieu = new System.Windows.Forms.PictureBox();
            this.pbDinhLuong = new System.Windows.Forms.PictureBox();
            this.lbNguyenLieu = new System.Windows.Forms.Label();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNguyenLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDinhLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.lbDinhLuong);
            this.pnSelect.Controls.Add(this.pbNguyenLieu);
            this.pnSelect.Controls.Add(this.pbDinhLuong);
            this.pnSelect.Controls.Add(this.lbNguyenLieu);
            this.pnSelect.Location = new System.Drawing.Point(145, 230);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(710, 140);
            this.pnSelect.TabIndex = 23;
            // 
            // lbDinhLuong
            // 
            this.lbDinhLuong.AutoSize = true;
            this.lbDinhLuong.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDinhLuong.ForeColor = System.Drawing.Color.Gray;
            this.lbDinhLuong.Location = new System.Drawing.Point(566, 120);
            this.lbDinhLuong.Name = "lbDinhLuong";
            this.lbDinhLuong.Size = new System.Drawing.Size(99, 16);
            this.lbDinhLuong.TabIndex = 12;
            this.lbDinhLuong.Text = "ĐỊNH LƯỢNG";
            // 
            // pbNguyenLieu
            // 
            this.pbNguyenLieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNguyenLieu.Location = new System.Drawing.Point(47, 10);
            this.pbNguyenLieu.Name = "pbNguyenLieu";
            this.pbNguyenLieu.Size = new System.Drawing.Size(100, 100);
            this.pbNguyenLieu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNguyenLieu.TabIndex = 11;
            this.pbNguyenLieu.TabStop = false;
            this.pbNguyenLieu.Click += new System.EventHandler(this.pbNguyenLieu_Click);
            this.pbNguyenLieu.MouseEnter += new System.EventHandler(this.pbNguyenLieu_MouseEnter);
            this.pbNguyenLieu.MouseLeave += new System.EventHandler(this.pbNguyenLieu_MouseLeave);
            // 
            // pbDinhLuong
            // 
            this.pbDinhLuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDinhLuong.Location = new System.Drawing.Point(566, 10);
            this.pbDinhLuong.Name = "pbDinhLuong";
            this.pbDinhLuong.Size = new System.Drawing.Size(100, 100);
            this.pbDinhLuong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDinhLuong.TabIndex = 11;
            this.pbDinhLuong.TabStop = false;
            this.pbDinhLuong.Click += new System.EventHandler(this.pbDinhLuong_Click);
            this.pbDinhLuong.MouseEnter += new System.EventHandler(this.pbDinhLuong_MouseEnter);
            this.pbDinhLuong.MouseLeave += new System.EventHandler(this.pbDinhLuong_MouseLeave);
            // 
            // lbNguyenLieu
            // 
            this.lbNguyenLieu.AutoSize = true;
            this.lbNguyenLieu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNguyenLieu.ForeColor = System.Drawing.Color.Gray;
            this.lbNguyenLieu.Location = new System.Drawing.Point(46, 120);
            this.lbNguyenLieu.Name = "lbNguyenLieu";
            this.lbNguyenLieu.Size = new System.Drawing.Size(103, 16);
            this.lbNguyenLieu.TabIndex = 12;
            this.lbNguyenLieu.Text = "NGUYÊN LIỆU";
            // 
            // UcNguyenLieuIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcNguyenLieuIndex";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcNguyenLieuIndex_Load);
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNguyenLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDinhLuong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbDinhLuong;
        private System.Windows.Forms.PictureBox pbNguyenLieu;
        private System.Windows.Forms.PictureBox pbDinhLuong;
        private System.Windows.Forms.Label lbNguyenLieu;

    }
}
