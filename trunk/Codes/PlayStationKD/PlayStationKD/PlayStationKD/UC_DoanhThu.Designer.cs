namespace PlayStationKD
{
    partial class UC_DoanhThu
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
            this.pnDoanhThu_Ngay = new System.Windows.Forms.Panel();
            this.dateTimePickerDT = new System.Windows.Forms.DateTimePicker();
            this.rbNam = new System.Windows.Forms.RadioButton();
            this.lbTongTienDT = new System.Windows.Forms.Label();
            this.rbThang = new System.Windows.Forms.RadioButton();
            this.lvThuChiDT = new System.Windows.Forms.ListView();
            this.clhSTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhGiờ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhThu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhChi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rbNgay = new System.Windows.Forms.RadioButton();
            this.pnDoanhThu_Ngay.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnDoanhThu_Ngay
            // 
            this.pnDoanhThu_Ngay.Controls.Add(this.dateTimePickerDT);
            this.pnDoanhThu_Ngay.Controls.Add(this.rbNam);
            this.pnDoanhThu_Ngay.Controls.Add(this.lbTongTienDT);
            this.pnDoanhThu_Ngay.Controls.Add(this.rbThang);
            this.pnDoanhThu_Ngay.Controls.Add(this.lvThuChiDT);
            this.pnDoanhThu_Ngay.Controls.Add(this.rbNgay);
            this.pnDoanhThu_Ngay.Location = new System.Drawing.Point(3, 3);
            this.pnDoanhThu_Ngay.Name = "pnDoanhThu_Ngay";
            this.pnDoanhThu_Ngay.Size = new System.Drawing.Size(677, 590);
            this.pnDoanhThu_Ngay.TabIndex = 4;
            // 
            // dateTimePickerDT
            // 
            this.dateTimePickerDT.Font = new System.Drawing.Font("Arial", 12F);
            this.dateTimePickerDT.Location = new System.Drawing.Point(170, 35);
            this.dateTimePickerDT.MinDate = new System.DateTime(2010, 11, 1, 0, 0, 0, 0);
            this.dateTimePickerDT.Name = "dateTimePickerDT";
            this.dateTimePickerDT.Size = new System.Drawing.Size(306, 26);
            this.dateTimePickerDT.TabIndex = 25;
            this.dateTimePickerDT.ValueChanged += new System.EventHandler(this.dateTimePickerDT_ValueChanged);
            // 
            // rbNam
            // 
            this.rbNam.AutoSize = true;
            this.rbNam.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNam.Location = new System.Drawing.Point(417, 7);
            this.rbNam.Name = "rbNam";
            this.rbNam.Size = new System.Drawing.Size(59, 22);
            this.rbNam.TabIndex = 7;
            this.rbNam.TabStop = true;
            this.rbNam.Text = "Năm";
            this.rbNam.UseVisualStyleBackColor = true;
            this.rbNam.CheckedChanged += new System.EventHandler(this.rbNam_CheckedChanged);
            // 
            // lbTongTienDT
            // 
            this.lbTongTienDT.AutoSize = true;
            this.lbTongTienDT.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lbTongTienDT.Location = new System.Drawing.Point(13, 70);
            this.lbTongTienDT.Name = "lbTongTienDT";
            this.lbTongTienDT.Size = new System.Drawing.Size(110, 22);
            this.lbTongTienDT.TabIndex = 21;
            this.lbTongTienDT.Text = "Tổng tiền: ";
            // 
            // rbThang
            // 
            this.rbThang.AutoSize = true;
            this.rbThang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbThang.Location = new System.Drawing.Point(290, 7);
            this.rbThang.Name = "rbThang";
            this.rbThang.Size = new System.Drawing.Size(69, 22);
            this.rbThang.TabIndex = 6;
            this.rbThang.TabStop = true;
            this.rbThang.Text = "Tháng";
            this.rbThang.UseVisualStyleBackColor = true;
            this.rbThang.CheckedChanged += new System.EventHandler(this.rbThang_CheckedChanged);
            // 
            // lvThuChiDT
            // 
            this.lvThuChiDT.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhSTT,
            this.clhGiờ,
            this.clhThu,
            this.clhChi});
            this.lvThuChiDT.Font = new System.Drawing.Font("Arial", 12F);
            this.lvThuChiDT.FullRowSelect = true;
            this.lvThuChiDT.GridLines = true;
            this.lvThuChiDT.Location = new System.Drawing.Point(3, 100);
            this.lvThuChiDT.MultiSelect = false;
            this.lvThuChiDT.Name = "lvThuChiDT";
            this.lvThuChiDT.Size = new System.Drawing.Size(671, 487);
            this.lvThuChiDT.TabIndex = 12;
            this.lvThuChiDT.UseCompatibleStateImageBehavior = false;
            this.lvThuChiDT.View = System.Windows.Forms.View.Details;
            this.lvThuChiDT.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvThuChiDT_ColumnClick);
            // 
            // clhSTT
            // 
            this.clhSTT.Text = "STT";
            this.clhSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clhSTT.Width = 41;
            // 
            // clhGiờ
            // 
            this.clhGiờ.Text = "Giờ";
            this.clhGiờ.Width = 103;
            // 
            // clhThu
            // 
            this.clhThu.Text = "Thu";
            this.clhThu.Width = 261;
            // 
            // clhChi
            // 
            this.clhChi.Text = "Chi";
            this.clhChi.Width = 260;
            // 
            // rbNgay
            // 
            this.rbNgay.AutoSize = true;
            this.rbNgay.Checked = true;
            this.rbNgay.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNgay.Location = new System.Drawing.Point(170, 7);
            this.rbNgay.Name = "rbNgay";
            this.rbNgay.Size = new System.Drawing.Size(62, 22);
            this.rbNgay.TabIndex = 5;
            this.rbNgay.TabStop = true;
            this.rbNgay.Text = "Ngày";
            this.rbNgay.UseVisualStyleBackColor = true;
            this.rbNgay.CheckedChanged += new System.EventHandler(this.rbNgay_CheckedChanged);
            // 
            // UC_DoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnDoanhThu_Ngay);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_DoanhThu";
            this.Size = new System.Drawing.Size(839, 618);
            this.Load += new System.EventHandler(this.UC_DoanhThu_Load);
            this.VisibleChanged += new System.EventHandler(this.UC_DoanhThu_VisibleChanged);
            this.pnDoanhThu_Ngay.ResumeLayout(false);
            this.pnDoanhThu_Ngay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnDoanhThu_Ngay;
        private System.Windows.Forms.DateTimePicker dateTimePickerDT;
        private System.Windows.Forms.Label lbTongTienDT;
        private System.Windows.Forms.ListView lvThuChiDT;
        private System.Windows.Forms.ColumnHeader clhSTT;
        private System.Windows.Forms.ColumnHeader clhGiờ;
        private System.Windows.Forms.ColumnHeader clhThu;
        private System.Windows.Forms.ColumnHeader clhChi;
        private System.Windows.Forms.RadioButton rbNgay;
        private System.Windows.Forms.RadioButton rbThang;
        private System.Windows.Forms.RadioButton rbNam;
    }
}
