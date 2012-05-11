namespace QuanLyKinhDoanh
{
    partial class FormBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBill));
            this.lvThongTin = new System.Windows.Forms.ListView();
            this.chCheckBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSanPham = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSoLuong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDonGia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chChietKhau = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chThanhTien = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvThongTin
            // 
            this.lvThongTin.CheckBoxes = true;
            this.lvThongTin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCheckBox,
            this.chId,
            this.chSTT,
            this.chSanPham,
            this.chSoLuong,
            this.chDonGia,
            this.chChietKhau,
            this.chThanhTien});
            this.lvThongTin.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvThongTin.FullRowSelect = true;
            this.lvThongTin.Location = new System.Drawing.Point(0, 0);
            this.lvThongTin.Name = "lvThongTin";
            this.lvThongTin.Size = new System.Drawing.Size(583, 366);
            this.lvThongTin.TabIndex = 88;
            this.lvThongTin.UseCompatibleStateImageBehavior = false;
            this.lvThongTin.View = System.Windows.Forms.View.Details;
            // 
            // chCheckBox
            // 
            this.chCheckBox.Text = "All";
            this.chCheckBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chCheckBox.Width = 0;
            // 
            // chId
            // 
            this.chId.Text = "Id";
            this.chId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chId.Width = 0;
            // 
            // chSTT
            // 
            this.chSTT.Text = "STT";
            this.chSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chSTT.Width = 39;
            // 
            // chSanPham
            // 
            this.chSanPham.Text = "Sản phẩm";
            this.chSanPham.Width = 186;
            // 
            // chSoLuong
            // 
            this.chSoLuong.Text = "SL";
            this.chSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chSoLuong.Width = 58;
            // 
            // chDonGia
            // 
            this.chDonGia.Text = "Đơn giá";
            this.chDonGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chDonGia.Width = 101;
            // 
            // chChietKhau
            // 
            this.chChietKhau.Text = "Giảm";
            this.chChietKhau.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chChietKhau.Width = 50;
            // 
            // chThanhTien
            // 
            this.chThanhTien.Text = "Thành tiền";
            this.chThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chThanhTien.Width = 80;
            // 
            // FormBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 479);
            this.Controls.Add(this.lvThongTin);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormBill";
            this.Load += new System.EventHandler(this.FormBill_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvThongTin;
        private System.Windows.Forms.ColumnHeader chCheckBox;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chSTT;
        private System.Windows.Forms.ColumnHeader chSanPham;
        private System.Windows.Forms.ColumnHeader chSoLuong;
        private System.Windows.Forms.ColumnHeader chDonGia;
        private System.Windows.Forms.ColumnHeader chChietKhau;
        private System.Windows.Forms.ColumnHeader chThanhTien;

    }
}