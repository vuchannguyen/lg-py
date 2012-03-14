namespace QuanLyKinhDoanh
{
    partial class UcKhoHang
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
            this.pnTitle = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.pnFind = new System.Windows.Forms.Panel();
            this.cbTinhTrang = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbNhom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbHoTen = new System.Windows.Forms.TextBox();
            this.lbMa = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.pbFind = new System.Windows.Forms.PictureBox();
            this.pnQuanLy = new System.Windows.Forms.Panel();
            this.lvThongTin = new System.Windows.Forms.ListView();
            this.chSTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSanPham = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNhom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTinhTrang = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSoLuong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDVT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnTraCuu = new System.Windows.Forms.Panel();
            this.tbPage = new System.Windows.Forms.TextBox();
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.pbNextPage = new System.Windows.Forms.PictureBox();
            this.pbBackPage = new System.Windows.Forms.PictureBox();
            this.pbTotalPage = new System.Windows.Forms.PictureBox();
            this.pnPage = new System.Windows.Forms.Panel();
            this.lbPage = new System.Windows.Forms.Label();
            this.chThanhTien = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pnFind.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFind)).BeginInit();
            this.pnQuanLy.SuspendLayout();
            this.pnTraCuu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNextPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTotalPage)).BeginInit();
            this.pnPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.lbTitle);
            this.pnTitle.Controls.Add(this.pbTitle);
            this.pnTitle.Location = new System.Drawing.Point(591, 6);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(270, 38);
            this.pnTitle.TabIndex = 53;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Gray;
            this.lbTitle.Location = new System.Drawing.Point(52, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(111, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "KHO HÀNG";
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
            // pnFind
            // 
            this.pnFind.Controls.Add(this.cbTinhTrang);
            this.pnFind.Controls.Add(this.label3);
            this.pnFind.Controls.Add(this.cbNhom);
            this.pnFind.Controls.Add(this.label2);
            this.pnFind.Controls.Add(this.textBox1);
            this.pnFind.Controls.Add(this.label1);
            this.pnFind.Controls.Add(this.tbHoTen);
            this.pnFind.Controls.Add(this.lbMa);
            this.pnFind.Controls.Add(this.panel2);
            this.pnFind.Location = new System.Drawing.Point(140, 3);
            this.pnFind.Name = "pnFind";
            this.pnFind.Size = new System.Drawing.Size(445, 78);
            this.pnFind.TabIndex = 52;
            // 
            // cbTinhTrang
            // 
            this.cbTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTinhTrang.FormattingEnabled = true;
            this.cbTinhTrang.Items.AddRange(new object[] {
            "Tất cả",
            "Còn",
            "Hết"});
            this.cbTinhTrang.Location = new System.Drawing.Point(325, 10);
            this.cbTinhTrang.Name = "cbTinhTrang";
            this.cbTinhTrang.Size = new System.Drawing.Size(110, 24);
            this.cbTinhTrang.TabIndex = 102;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(241, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 101;
            this.label3.Text = "Tình trạng:";
            // 
            // cbNhom
            // 
            this.cbNhom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNhom.FormattingEnabled = true;
            this.cbNhom.Items.AddRange(new object[] {
            "Tất cả",
            "Vd1: Giày",
            "Vd2: Dép",
            "Vd3: Nón"});
            this.cbNhom.Location = new System.Drawing.Point(295, 40);
            this.cbNhom.Name = "cbNhom";
            this.cbNhom.Size = new System.Drawing.Size(140, 24);
            this.cbNhom.TabIndex = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(241, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 99;
            this.label2.Text = "Nhóm:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 40);
            this.textBox1.MaxLength = 50;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(107, 23);
            this.textBox1.TabIndex = 93;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(82, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 94;
            this.label1.Text = "Tên:";
            // 
            // tbHoTen
            // 
            this.tbHoTen.Location = new System.Drawing.Point(125, 10);
            this.tbHoTen.MaxLength = 6;
            this.tbHoTen.Name = "tbHoTen";
            this.tbHoTen.Size = new System.Drawing.Size(107, 23);
            this.tbHoTen.TabIndex = 91;
            // 
            // lbMa
            // 
            this.lbMa.AutoSize = true;
            this.lbMa.ForeColor = System.Drawing.Color.Black;
            this.lbMa.Location = new System.Drawing.Point(88, 13);
            this.lbMa.Name = "lbMa";
            this.lbMa.Size = new System.Drawing.Size(31, 16);
            this.lbMa.TabIndex = 92;
            this.lbMa.Text = "Mã:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.pbFind);
            this.panel2.Location = new System.Drawing.Point(6, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(70, 70);
            this.panel2.TabIndex = 27;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(6, 53);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(58, 16);
            this.label24.TabIndex = 1;
            this.label24.Text = "Tra cứu";
            // 
            // pbFind
            // 
            this.pbFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbFind.Location = new System.Drawing.Point(10, 0);
            this.pbFind.Name = "pbFind";
            this.pbFind.Size = new System.Drawing.Size(50, 50);
            this.pbFind.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFind.TabIndex = 1;
            this.pbFind.TabStop = false;
            this.pbFind.Click += new System.EventHandler(this.pbFind_Click);
            this.pbFind.MouseEnter += new System.EventHandler(this.pbFind_MouseEnter);
            this.pbFind.MouseLeave += new System.EventHandler(this.pbFind_MouseLeave);
            // 
            // pnQuanLy
            // 
            this.pnQuanLy.Controls.Add(this.lvThongTin);
            this.pnQuanLy.Controls.Add(this.pnTraCuu);
            this.pnQuanLy.Location = new System.Drawing.Point(146, 87);
            this.pnQuanLy.Name = "pnQuanLy";
            this.pnQuanLy.Size = new System.Drawing.Size(810, 480);
            this.pnQuanLy.TabIndex = 51;
            // 
            // lvThongTin
            // 
            this.lvThongTin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSTT,
            this.chMa,
            this.chSanPham,
            this.chNhom,
            this.chTinhTrang,
            this.chSoLuong,
            this.chDVT,
            this.chGia,
            this.chThanhTien});
            this.lvThongTin.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvThongTin.FullRowSelect = true;
            this.lvThongTin.GridLines = true;
            this.lvThongTin.Location = new System.Drawing.Point(3, 3);
            this.lvThongTin.Name = "lvThongTin";
            this.lvThongTin.Size = new System.Drawing.Size(800, 430);
            this.lvThongTin.TabIndex = 86;
            this.lvThongTin.UseCompatibleStateImageBehavior = false;
            this.lvThongTin.View = System.Windows.Forms.View.Details;
            // 
            // chSTT
            // 
            this.chSTT.Text = "STT";
            this.chSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chSTT.Width = 39;
            // 
            // chMa
            // 
            this.chMa.Text = "Mã";
            this.chMa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chMa.Width = 77;
            // 
            // chSanPham
            // 
            this.chSanPham.Text = "Sản phẩm";
            this.chSanPham.Width = 174;
            // 
            // chNhom
            // 
            this.chNhom.Text = "Nhóm";
            this.chNhom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chNhom.Width = 110;
            // 
            // chTinhTrang
            // 
            this.chTinhTrang.Text = "Tình trạng";
            this.chTinhTrang.Width = 69;
            // 
            // chSoLuong
            // 
            this.chSoLuong.Text = "Số lượng";
            this.chSoLuong.Width = 68;
            // 
            // chDVT
            // 
            this.chDVT.Text = "ĐVT";
            this.chDVT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chDVT.Width = 63;
            // 
            // chGia
            // 
            this.chGia.Text = "Giá";
            this.chGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chGia.Width = 94;
            // 
            // pnTraCuu
            // 
            this.pnTraCuu.Controls.Add(this.tbPage);
            this.pnTraCuu.Controls.Add(this.lbTotalPage);
            this.pnTraCuu.Controls.Add(this.pbNextPage);
            this.pnTraCuu.Controls.Add(this.pbBackPage);
            this.pnTraCuu.Controls.Add(this.pbTotalPage);
            this.pnTraCuu.Controls.Add(this.pnPage);
            this.pnTraCuu.Location = new System.Drawing.Point(3, 439);
            this.pnTraCuu.Name = "pnTraCuu";
            this.pnTraCuu.Size = new System.Drawing.Size(800, 33);
            this.pnTraCuu.TabIndex = 85;
            // 
            // tbPage
            // 
            this.tbPage.Location = new System.Drawing.Point(518, 6);
            this.tbPage.MaxLength = 3;
            this.tbPage.Name = "tbPage";
            this.tbPage.Size = new System.Drawing.Size(40, 23);
            this.tbPage.TabIndex = 89;
            this.tbPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPage.Visible = false;
            // 
            // lbTotalPage
            // 
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPage.ForeColor = System.Drawing.Color.Gray;
            this.lbTotalPage.Location = new System.Drawing.Point(612, 6);
            this.lbTotalPage.Name = "lbTotalPage";
            this.lbTotalPage.Size = new System.Drawing.Size(88, 19);
            this.lbTotalPage.TabIndex = 2;
            this.lbTotalPage.Text = "??? Trang";
            // 
            // pbNextPage
            // 
            this.pbNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNextPage.Location = new System.Drawing.Point(772, 0);
            this.pbNextPage.Name = "pbNextPage";
            this.pbNextPage.Size = new System.Drawing.Size(25, 32);
            this.pbNextPage.TabIndex = 88;
            this.pbNextPage.TabStop = false;
            // 
            // pbBackPage
            // 
            this.pbBackPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBackPage.Location = new System.Drawing.Point(707, 0);
            this.pbBackPage.Name = "pbBackPage";
            this.pbBackPage.Size = new System.Drawing.Size(25, 32);
            this.pbBackPage.TabIndex = 87;
            this.pbBackPage.TabStop = false;
            // 
            // pbTotalPage
            // 
            this.pbTotalPage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbTotalPage.Location = new System.Drawing.Point(574, 0);
            this.pbTotalPage.Name = "pbTotalPage";
            this.pbTotalPage.Size = new System.Drawing.Size(32, 33);
            this.pbTotalPage.TabIndex = 86;
            this.pbTotalPage.TabStop = false;
            // 
            // pnPage
            // 
            this.pnPage.Controls.Add(this.lbPage);
            this.pnPage.Location = new System.Drawing.Point(730, 5);
            this.pnPage.Name = "pnPage";
            this.pnPage.Size = new System.Drawing.Size(45, 22);
            this.pnPage.TabIndex = 86;
            // 
            // lbPage
            // 
            this.lbPage.AutoSize = true;
            this.lbPage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPage.ForeColor = System.Drawing.Color.Gray;
            this.lbPage.Location = new System.Drawing.Point(3, 1);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(39, 19);
            this.lbPage.TabIndex = 89;
            this.lbPage.Text = "???";
            this.lbPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chThanhTien
            // 
            this.chThanhTien.Text = "Thành tiền";
            this.chThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chThanhTien.Width = 100;
            // 
            // UcKhoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.pnFind);
            this.Controls.Add(this.pnQuanLy);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcKhoHang";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcKhoHang_Load);
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pnFind.ResumeLayout(false);
            this.pnFind.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFind)).EndInit();
            this.pnQuanLy.ResumeLayout(false);
            this.pnTraCuu.ResumeLayout(false);
            this.pnTraCuu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNextPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTotalPage)).EndInit();
            this.pnPage.ResumeLayout(false);
            this.pnPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Panel pnFind;
        private System.Windows.Forms.Panel pnQuanLy;
        private System.Windows.Forms.ListView lvThongTin;
        private System.Windows.Forms.ColumnHeader chMa;
        private System.Windows.Forms.ColumnHeader chSTT;
        private System.Windows.Forms.ColumnHeader chSanPham;
        private System.Windows.Forms.ColumnHeader chNhom;
        private System.Windows.Forms.ColumnHeader chTinhTrang;
        private System.Windows.Forms.ColumnHeader chDVT;
        private System.Windows.Forms.ColumnHeader chGia;
        private System.Windows.Forms.Panel pnTraCuu;
        private System.Windows.Forms.TextBox tbPage;
        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.PictureBox pbNextPage;
        private System.Windows.Forms.PictureBox pbBackPage;
        private System.Windows.Forms.PictureBox pbTotalPage;
        private System.Windows.Forms.Panel pnPage;
        private System.Windows.Forms.Label lbPage;
        private System.Windows.Forms.ColumnHeader chSoLuong;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.PictureBox pbFind;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHoTen;
        private System.Windows.Forms.Label lbMa;
        private System.Windows.Forms.ComboBox cbTinhTrang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbNhom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader chThanhTien;

    }
}
