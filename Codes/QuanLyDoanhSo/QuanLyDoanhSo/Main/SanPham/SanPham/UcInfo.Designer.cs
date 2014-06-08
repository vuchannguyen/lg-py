namespace Weedon.SanPham
{
    partial class UcInfo
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
            this.lbSelect = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.cbDVT = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbGia = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rbTamNgung = new System.Windows.Forms.RadioButton();
            this.rbBan = new System.Windows.Forms.RadioButton();
            this.tbGhiChu = new System.Windows.Forms.TextBox();
            this.tbTen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.pbHoanTat = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label39 = new System.Windows.Forms.Label();
            this.pbHuy = new System.Windows.Forms.PictureBox();
            this.tbMa = new System.Windows.Forms.TextBox();
            this.lbMa = new System.Windows.Forms.Label();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pnInfo.SuspendLayout();
            this.gbInfo.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.lbSelect);
            this.pnTitle.Controls.Add(this.lbTitle);
            this.pnTitle.Controls.Add(this.pbTitle);
            this.pnTitle.Location = new System.Drawing.Point(346, 3);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(278, 38);
            this.pnTitle.TabIndex = 50;
            // 
            // lbSelect
            // 
            this.lbSelect.AutoSize = true;
            this.lbSelect.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelect.ForeColor = System.Drawing.Color.Orange;
            this.lbSelect.Location = new System.Drawing.Point(50, 8);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.Size = new System.Drawing.Size(66, 22);
            this.lbSelect.TabIndex = 1;
            this.lbSelect.Text = "THÊM";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Gray;
            this.lbTitle.Location = new System.Drawing.Point(122, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(110, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "SẢN PHẨM";
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
            // pnInfo
            // 
            this.pnInfo.Controls.Add(this.gbInfo);
            this.pnInfo.ForeColor = System.Drawing.Color.Black;
            this.pnInfo.Location = new System.Drawing.Point(145, 119);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(710, 350);
            this.pnInfo.TabIndex = 49;
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.cbDVT);
            this.gbInfo.Controls.Add(this.label5);
            this.gbInfo.Controls.Add(this.tbGia);
            this.gbInfo.Controls.Add(this.label6);
            this.gbInfo.Controls.Add(this.label3);
            this.gbInfo.Controls.Add(this.rbTamNgung);
            this.gbInfo.Controls.Add(this.rbBan);
            this.gbInfo.Controls.Add(this.tbGhiChu);
            this.gbInfo.Controls.Add(this.tbTen);
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Controls.Add(this.label4);
            this.gbInfo.Controls.Add(this.panel12);
            this.gbInfo.Controls.Add(this.panel8);
            this.gbInfo.Controls.Add(this.tbMa);
            this.gbInfo.Controls.Add(this.lbMa);
            this.gbInfo.ForeColor = System.Drawing.Color.Orange;
            this.gbInfo.Location = new System.Drawing.Point(7, 3);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(700, 340);
            this.gbInfo.TabIndex = 2;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Thông tin";
            // 
            // cbDVT
            // 
            this.cbDVT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDVT.DisplayMember = "1";
            this.cbDVT.FormattingEnabled = true;
            this.cbDVT.Items.AddRange(new object[] {
            "Bao",
            "Cái"});
            this.cbDVT.Location = new System.Drawing.Point(420, 70);
            this.cbDVT.MaxLength = 20;
            this.cbDVT.Name = "cbDVT";
            this.cbDVT.Size = new System.Drawing.Size(150, 24);
            this.cbDVT.TabIndex = 126;
            this.cbDVT.SelectedIndexChanged += new System.EventHandler(this.cbDVT_SelectedIndexChanged);
            this.cbDVT.TextChanged += new System.EventHandler(this.cbDVT_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(328, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 16);
            this.label5.TabIndex = 125;
            this.label5.Text = "*Đơn vị tính:";
            // 
            // tbGia
            // 
            this.tbGia.Location = new System.Drawing.Point(110, 70);
            this.tbGia.MaxLength = 11;
            this.tbGia.Name = "tbGia";
            this.tbGia.Size = new System.Drawing.Size(100, 23);
            this.tbGia.TabIndex = 122;
            this.tbGia.TextChanged += new System.EventHandler(this.tbGia_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(65, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 16);
            this.label6.TabIndex = 124;
            this.label6.Text = "*Giá:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(26, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 121;
            this.label3.Text = "Tình trạng:";
            // 
            // rbTamNgung
            // 
            this.rbTamNgung.AutoSize = true;
            this.rbTamNgung.ForeColor = System.Drawing.Color.Black;
            this.rbTamNgung.Location = new System.Drawing.Point(110, 140);
            this.rbTamNgung.Name = "rbTamNgung";
            this.rbTamNgung.Size = new System.Drawing.Size(100, 20);
            this.rbTamNgung.TabIndex = 120;
            this.rbTamNgung.TabStop = true;
            this.rbTamNgung.Text = "Tạm Ngưng";
            this.rbTamNgung.UseVisualStyleBackColor = true;
            // 
            // rbBan
            // 
            this.rbBan.AutoSize = true;
            this.rbBan.Checked = true;
            this.rbBan.ForeColor = System.Drawing.Color.Black;
            this.rbBan.Location = new System.Drawing.Point(110, 110);
            this.rbBan.Name = "rbBan";
            this.rbBan.Size = new System.Drawing.Size(51, 20);
            this.rbBan.TabIndex = 119;
            this.rbBan.TabStop = true;
            this.rbBan.Text = "Bán";
            this.rbBan.UseVisualStyleBackColor = true;
            // 
            // tbGhiChu
            // 
            this.tbGhiChu.Location = new System.Drawing.Point(420, 110);
            this.tbGhiChu.MaxLength = 200;
            this.tbGhiChu.Multiline = true;
            this.tbGhiChu.Name = "tbGhiChu";
            this.tbGhiChu.Size = new System.Drawing.Size(227, 64);
            this.tbGhiChu.TabIndex = 9;
            // 
            // tbTen
            // 
            this.tbTen.Location = new System.Drawing.Point(420, 30);
            this.tbTen.MaxLength = 50;
            this.tbTen.Name = "tbTen";
            this.tbTen.Size = new System.Drawing.Size(210, 23);
            this.tbTen.TabIndex = 1;
            this.tbTen.TextChanged += new System.EventHandler(this.tbTen_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(372, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 100;
            this.label1.Text = "*Tên:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(353, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 97;
            this.label4.Text = "Ghi chú:";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label47);
            this.panel12.Controls.Add(this.pbHoanTat);
            this.panel12.Location = new System.Drawing.Point(499, 260);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(70, 70);
            this.panel12.TabIndex = 93;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(7, 68);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(0, 16);
            this.label47.TabIndex = 2;
            // 
            // pbHoanTat
            // 
            this.pbHoanTat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHoanTat.Location = new System.Drawing.Point(10, 10);
            this.pbHoanTat.Name = "pbHoanTat";
            this.pbHoanTat.Size = new System.Drawing.Size(50, 39);
            this.pbHoanTat.TabIndex = 1;
            this.pbHoanTat.TabStop = false;
            this.pbHoanTat.Click += new System.EventHandler(this.pbHoanTat_Click);
            this.pbHoanTat.MouseEnter += new System.EventHandler(this.pbHoanTat_MouseEnter);
            this.pbHoanTat.MouseLeave += new System.EventHandler(this.pbHoanTat_MouseLeave);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label39);
            this.panel8.Controls.Add(this.pbHuy);
            this.panel8.Location = new System.Drawing.Point(128, 260);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(70, 70);
            this.panel8.TabIndex = 92;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(7, 68);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(0, 16);
            this.label39.TabIndex = 2;
            // 
            // pbHuy
            // 
            this.pbHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHuy.Location = new System.Drawing.Point(10, 10);
            this.pbHuy.Name = "pbHuy";
            this.pbHuy.Size = new System.Drawing.Size(50, 39);
            this.pbHuy.TabIndex = 1;
            this.pbHuy.TabStop = false;
            this.pbHuy.Click += new System.EventHandler(this.pbHuy_Click);
            this.pbHuy.MouseEnter += new System.EventHandler(this.pbHuy_MouseEnter);
            this.pbHuy.MouseLeave += new System.EventHandler(this.pbHuy_MouseLeave);
            // 
            // tbMa
            // 
            this.tbMa.Location = new System.Drawing.Point(110, 30);
            this.tbMa.MaxLength = 10;
            this.tbMa.Name = "tbMa";
            this.tbMa.ReadOnly = true;
            this.tbMa.Size = new System.Drawing.Size(100, 23);
            this.tbMa.TabIndex = 0;
            this.tbMa.TextChanged += new System.EventHandler(this.tbMa_TextChanged);
            // 
            // lbMa
            // 
            this.lbMa.AutoSize = true;
            this.lbMa.ForeColor = System.Drawing.Color.Black;
            this.lbMa.Location = new System.Drawing.Point(68, 33);
            this.lbMa.Name = "lbMa";
            this.lbMa.Size = new System.Drawing.Size(36, 16);
            this.lbMa.TabIndex = 90;
            this.lbMa.Text = "*Mã:";
            // 
            // UcInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.pnInfo);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcInfo";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcInfo_Load);
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pnInfo.ResumeLayout(false);
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label lbSelect;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.TextBox tbTen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.PictureBox pbHoanTat;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.PictureBox pbHuy;
        private System.Windows.Forms.TextBox tbMa;
        private System.Windows.Forms.Label lbMa;
        private System.Windows.Forms.TextBox tbGhiChu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbTamNgung;
        private System.Windows.Forms.RadioButton rbBan;
        private System.Windows.Forms.ComboBox cbDVT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbGia;
        private System.Windows.Forms.Label label6;
    }
}
