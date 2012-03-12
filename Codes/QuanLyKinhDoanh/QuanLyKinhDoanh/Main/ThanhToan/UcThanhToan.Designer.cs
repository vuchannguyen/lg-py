namespace QuanLyKinhDoanh
{
    partial class UcThanhToan
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
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFilter = new System.Windows.Forms.DateTimePicker();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.pnSelect = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.pbThem = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.pbSua = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.pbXoa = new System.Windows.Forms.PictureBox();
            this.pnQuanLy = new System.Windows.Forms.Panel();
            this.lvThongTin = new System.Windows.Forms.ListView();
            this.chMa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSanPham = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNgay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSoLuong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDonViTinh = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chThanhTien = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pnSelect.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThem)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSua)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbXoa)).BeginInit();
            this.pnQuanLy.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbFilter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpFilter);
            this.panel1.Location = new System.Drawing.Point(381, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 78);
            this.panel1.TabIndex = 58;
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "Ngày",
            "Tháng",
            "Năm"});
            this.cbFilter.Location = new System.Drawing.Point(76, 10);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(77, 24);
            this.cbFilter.TabIndex = 114;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Lọc theo:";
            // 
            // dtpFilter
            // 
            this.dtpFilter.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFilter.Location = new System.Drawing.Point(3, 40);
            this.dtpFilter.Name = "dtpFilter";
            this.dtpFilter.Size = new System.Drawing.Size(150, 23);
            this.dtpFilter.TabIndex = 3;
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.lbTitle);
            this.pnTitle.Controls.Add(this.pbTitle);
            this.pnTitle.Location = new System.Drawing.Point(591, 6);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(270, 38);
            this.pnTitle.TabIndex = 57;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Gray;
            this.lbTitle.Location = new System.Drawing.Point(52, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(134, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "THANH TOÁN";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pbTitle
            // 
            this.pbTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbTitle.Location = new System.Drawing.Point(10, 0);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(36, 36);
            this.pbTitle.TabIndex = 1;
            this.pbTitle.TabStop = false;
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.panel2);
            this.pnSelect.Controls.Add(this.panel3);
            this.pnSelect.Controls.Add(this.panel4);
            this.pnSelect.Location = new System.Drawing.Point(140, 3);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(235, 78);
            this.pnSelect.TabIndex = 56;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.pbThem);
            this.panel2.Location = new System.Drawing.Point(6, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(70, 70);
            this.panel2.TabIndex = 26;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(19, 53);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(33, 16);
            this.label24.TabIndex = 1;
            this.label24.Text = "Bán";
            // 
            // pbThem
            // 
            this.pbThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbThem.Location = new System.Drawing.Point(10, 0);
            this.pbThem.Name = "pbThem";
            this.pbThem.Size = new System.Drawing.Size(50, 50);
            this.pbThem.TabIndex = 1;
            this.pbThem.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.pbSua);
            this.panel3.Location = new System.Drawing.Point(158, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(70, 70);
            this.panel3.TabIndex = 28;
            this.panel3.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(18, 53);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(34, 16);
            this.label30.TabIndex = 1;
            this.label30.Text = "Sửa";
            // 
            // pbSua
            // 
            this.pbSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSua.Enabled = false;
            this.pbSua.Location = new System.Drawing.Point(10, 0);
            this.pbSua.Name = "pbSua";
            this.pbSua.Size = new System.Drawing.Size(50, 50);
            this.pbSua.TabIndex = 1;
            this.pbSua.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label28);
            this.panel4.Controls.Add(this.pbXoa);
            this.panel4.Location = new System.Drawing.Point(82, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(70, 70);
            this.panel4.TabIndex = 27;
            this.panel4.Visible = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(19, 53);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(32, 16);
            this.label28.TabIndex = 1;
            this.label28.Text = "Xóa";
            // 
            // pbXoa
            // 
            this.pbXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbXoa.Enabled = false;
            this.pbXoa.Location = new System.Drawing.Point(10, 0);
            this.pbXoa.Name = "pbXoa";
            this.pbXoa.Size = new System.Drawing.Size(50, 50);
            this.pbXoa.TabIndex = 1;
            this.pbXoa.TabStop = false;
            // 
            // pnQuanLy
            // 
            this.pnQuanLy.Controls.Add(this.lvThongTin);
            this.pnQuanLy.Location = new System.Drawing.Point(146, 87);
            this.pnQuanLy.Name = "pnQuanLy";
            this.pnQuanLy.Size = new System.Drawing.Size(710, 480);
            this.pnQuanLy.TabIndex = 55;
            // 
            // lvThongTin
            // 
            this.lvThongTin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMa,
            this.chSTT,
            this.chSanPham,
            this.chUser,
            this.chNgay,
            this.chSoLuong,
            this.chDonViTinh,
            this.chThanhTien});
            this.lvThongTin.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvThongTin.FullRowSelect = true;
            this.lvThongTin.GridLines = true;
            this.lvThongTin.Location = new System.Drawing.Point(3, 3);
            this.lvThongTin.Name = "lvThongTin";
            this.lvThongTin.Size = new System.Drawing.Size(700, 430);
            this.lvThongTin.TabIndex = 86;
            this.lvThongTin.UseCompatibleStateImageBehavior = false;
            this.lvThongTin.View = System.Windows.Forms.View.Details;
            // 
            // chMa
            // 
            this.chMa.Text = "Mã";
            this.chMa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chMa.Width = 0;
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
            this.chSanPham.Width = 146;
            // 
            // chUser
            // 
            this.chUser.Text = "Người nhập";
            this.chUser.Width = 147;
            // 
            // chNgay
            // 
            this.chNgay.Text = "Ngày";
            this.chNgay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chNgay.Width = 93;
            // 
            // chSoLuong
            // 
            this.chSoLuong.Text = "Số lượng";
            this.chSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chSoLuong.Width = 73;
            // 
            // chDonViTinh
            // 
            this.chDonViTinh.Text = "Đơn vị tính";
            this.chDonViTinh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chDonViTinh.Width = 82;
            // 
            // chThanhTien
            // 
            this.chThanhTien.Text = "Thành tiền";
            this.chThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chThanhTien.Width = 114;
            // 
            // UcThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.pnSelect);
            this.Controls.Add(this.pnQuanLy);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcThanhToan";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcThanhToan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pnSelect.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThem)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSua)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbXoa)).EndInit();
            this.pnQuanLy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFilter;
        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.PictureBox pbThem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.PictureBox pbSua;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.PictureBox pbXoa;
        private System.Windows.Forms.Panel pnQuanLy;
        private System.Windows.Forms.ListView lvThongTin;
        private System.Windows.Forms.ColumnHeader chMa;
        private System.Windows.Forms.ColumnHeader chSTT;
        private System.Windows.Forms.ColumnHeader chSanPham;
        private System.Windows.Forms.ColumnHeader chUser;
        private System.Windows.Forms.ColumnHeader chNgay;
        private System.Windows.Forms.ColumnHeader chSoLuong;
        private System.Windows.Forms.ColumnHeader chDonViTinh;
        private System.Windows.Forms.ColumnHeader chThanhTien;
    }
}
