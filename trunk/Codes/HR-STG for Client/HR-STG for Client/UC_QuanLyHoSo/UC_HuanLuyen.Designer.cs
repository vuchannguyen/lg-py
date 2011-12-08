namespace HR_STG_for_Client
{
    partial class UC_HuanLuyen
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
            this.gbKhoa = new System.Windows.Forms.GroupBox();
            this.pnTinhTrang = new System.Windows.Forms.Panel();
            this.rbThamDu = new System.Windows.Forms.RadioButton();
            this.rbTrungCach = new System.Windows.Forms.RadioButton();
            this.lbTinhTrang = new System.Windows.Forms.Label();
            this.tbMHL = new System.Windows.Forms.TextBox();
            this.lbMHL = new System.Windows.Forms.Label();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.dtpNam = new System.Windows.Forms.DateTimePicker();
            this.rbKhac = new System.Windows.Forms.RadioButton();
            this.rbTrang = new System.Windows.Forms.RadioButton();
            this.rbKha = new System.Windows.Forms.RadioButton();
            this.rbThieu = new System.Windows.Forms.RadioButton();
            this.rbAu = new System.Windows.Forms.RadioButton();
            this.lbNganh = new System.Windows.Forms.Label();
            this.tbTenKhoa = new System.Windows.Forms.TextBox();
            this.tbKhoaTruong = new System.Windows.Forms.TextBox();
            this.lbTenKhoa = new System.Windows.Forms.Label();
            this.lbKhoaTruong = new System.Windows.Forms.Label();
            this.lbNam = new System.Windows.Forms.Label();
            this.lbKhoa = new System.Windows.Forms.Label();
            this.cbKhoa = new System.Windows.Forms.ComboBox();
            this.gbKhoa.SuspendLayout();
            this.pnTinhTrang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // gbKhoa
            // 
            this.gbKhoa.Controls.Add(this.pnTinhTrang);
            this.gbKhoa.Controls.Add(this.lbTinhTrang);
            this.gbKhoa.Controls.Add(this.tbMHL);
            this.gbKhoa.Controls.Add(this.lbMHL);
            this.gbKhoa.Controls.Add(this.pbDelete);
            this.gbKhoa.Controls.Add(this.dtpNam);
            this.gbKhoa.Controls.Add(this.rbKhac);
            this.gbKhoa.Controls.Add(this.rbTrang);
            this.gbKhoa.Controls.Add(this.rbKha);
            this.gbKhoa.Controls.Add(this.rbThieu);
            this.gbKhoa.Controls.Add(this.rbAu);
            this.gbKhoa.Controls.Add(this.lbNganh);
            this.gbKhoa.Controls.Add(this.tbTenKhoa);
            this.gbKhoa.Controls.Add(this.tbKhoaTruong);
            this.gbKhoa.Controls.Add(this.lbTenKhoa);
            this.gbKhoa.Controls.Add(this.lbKhoaTruong);
            this.gbKhoa.Controls.Add(this.lbNam);
            this.gbKhoa.Controls.Add(this.lbKhoa);
            this.gbKhoa.Controls.Add(this.cbKhoa);
            this.gbKhoa.ForeColor = System.Drawing.Color.Black;
            this.gbKhoa.Location = new System.Drawing.Point(3, 3);
            this.gbKhoa.Name = "gbKhoa";
            this.gbKhoa.Size = new System.Drawing.Size(497, 170);
            this.gbKhoa.TabIndex = 80;
            this.gbKhoa.TabStop = false;
            this.gbKhoa.Text = "Khóa";
            // 
            // pnTinhTrang
            // 
            this.pnTinhTrang.Controls.Add(this.rbThamDu);
            this.pnTinhTrang.Controls.Add(this.rbTrungCach);
            this.pnTinhTrang.Location = new System.Drawing.Point(258, 122);
            this.pnTinhTrang.Name = "pnTinhTrang";
            this.pnTinhTrang.Size = new System.Drawing.Size(197, 34);
            this.pnTinhTrang.TabIndex = 100;
            // 
            // rbThamDu
            // 
            this.rbThamDu.AutoSize = true;
            this.rbThamDu.Location = new System.Drawing.Point(5, 8);
            this.rbThamDu.Name = "rbThamDu";
            this.rbThamDu.Size = new System.Drawing.Size(83, 20);
            this.rbThamDu.TabIndex = 11;
            this.rbThamDu.Text = "Tham dự";
            this.rbThamDu.UseVisualStyleBackColor = true;
            this.rbThamDu.CheckedChanged += new System.EventHandler(this.rbThamDu_CheckedChanged);
            // 
            // rbTrungCach
            // 
            this.rbTrungCach.AutoSize = true;
            this.rbTrungCach.Checked = true;
            this.rbTrungCach.Location = new System.Drawing.Point(92, 8);
            this.rbTrungCach.Name = "rbTrungCach";
            this.rbTrungCach.Size = new System.Drawing.Size(98, 20);
            this.rbTrungCach.TabIndex = 12;
            this.rbTrungCach.TabStop = true;
            this.rbTrungCach.Text = "Trúng cách";
            this.rbTrungCach.UseVisualStyleBackColor = true;
            this.rbTrungCach.CheckedChanged += new System.EventHandler(this.rbTrungCach_CheckedChanged);
            // 
            // lbTinhTrang
            // 
            this.lbTinhTrang.AutoSize = true;
            this.lbTinhTrang.Location = new System.Drawing.Point(179, 132);
            this.lbTinhTrang.Name = "lbTinhTrang";
            this.lbTinhTrang.Size = new System.Drawing.Size(78, 16);
            this.lbTinhTrang.TabIndex = 97;
            this.lbTinhTrang.Text = "Tình trạng:";
            // 
            // tbMHL
            // 
            this.tbMHL.Location = new System.Drawing.Point(54, 129);
            this.tbMHL.MaxLength = 15;
            this.tbMHL.Name = "tbMHL";
            this.tbMHL.Size = new System.Drawing.Size(90, 23);
            this.tbMHL.TabIndex = 10;
            this.tbMHL.TextChanged += new System.EventHandler(this.tbMHL_TextChanged);
            // 
            // lbMHL
            // 
            this.lbMHL.AutoSize = true;
            this.lbMHL.Location = new System.Drawing.Point(11, 132);
            this.lbMHL.Name = "lbMHL";
            this.lbMHL.Size = new System.Drawing.Size(40, 16);
            this.lbMHL.TabIndex = 95;
            this.lbMHL.Text = "MHL:";
            // 
            // pbDelete
            // 
            this.pbDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDelete.Location = new System.Drawing.Point(465, 132);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(20, 20);
            this.pbDelete.TabIndex = 94;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            // 
            // dtpNam
            // 
            this.dtpNam.CustomFormat = "yyyy";
            this.dtpNam.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNam.Location = new System.Drawing.Point(54, 93);
            this.dtpNam.MaxDate = new System.DateTime(9998, 1, 1, 0, 0, 0, 0);
            this.dtpNam.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpNam.Name = "dtpNam";
            this.dtpNam.Size = new System.Drawing.Size(90, 20);
            this.dtpNam.TabIndex = 8;
            this.dtpNam.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpNam.ValueChanged += new System.EventHandler(this.dtpNam_ValueChanged);
            // 
            // rbKhac
            // 
            this.rbKhac.AutoSize = true;
            this.rbKhac.Location = new System.Drawing.Point(427, 18);
            this.rbKhac.Name = "rbKhac";
            this.rbKhac.Size = new System.Drawing.Size(58, 20);
            this.rbKhac.TabIndex = 5;
            this.rbKhac.Text = "Khác";
            this.rbKhac.UseVisualStyleBackColor = true;
            this.rbKhac.CheckedChanged += new System.EventHandler(this.rbKhac_CheckedChanged);
            // 
            // rbTrang
            // 
            this.rbTrang.AutoSize = true;
            this.rbTrang.Location = new System.Drawing.Point(324, 18);
            this.rbTrang.Name = "rbTrang";
            this.rbTrang.Size = new System.Drawing.Size(64, 20);
            this.rbTrang.TabIndex = 4;
            this.rbTrang.Text = "Tráng";
            this.rbTrang.UseVisualStyleBackColor = true;
            this.rbTrang.CheckedChanged += new System.EventHandler(this.rbTrang_CheckedChanged);
            // 
            // rbKha
            // 
            this.rbKha.AutoSize = true;
            this.rbKha.Location = new System.Drawing.Point(240, 18);
            this.rbKha.Name = "rbKha";
            this.rbKha.Size = new System.Drawing.Size(51, 20);
            this.rbKha.TabIndex = 3;
            this.rbKha.Text = "Kha";
            this.rbKha.UseVisualStyleBackColor = true;
            this.rbKha.CheckedChanged += new System.EventHandler(this.rbKha_CheckedChanged);
            // 
            // rbThieu
            // 
            this.rbThieu.AutoSize = true;
            this.rbThieu.Checked = true;
            this.rbThieu.Location = new System.Drawing.Point(151, 18);
            this.rbThieu.Name = "rbThieu";
            this.rbThieu.Size = new System.Drawing.Size(62, 20);
            this.rbThieu.TabIndex = 2;
            this.rbThieu.TabStop = true;
            this.rbThieu.Text = "Thiếu";
            this.rbThieu.UseVisualStyleBackColor = true;
            this.rbThieu.CheckedChanged += new System.EventHandler(this.rbThieu_CheckedChanged);
            // 
            // rbAu
            // 
            this.rbAu.AutoSize = true;
            this.rbAu.Location = new System.Drawing.Point(79, 18);
            this.rbAu.Name = "rbAu";
            this.rbAu.Size = new System.Drawing.Size(43, 20);
            this.rbAu.TabIndex = 1;
            this.rbAu.Text = "Ấu";
            this.rbAu.UseVisualStyleBackColor = true;
            this.rbAu.CheckedChanged += new System.EventHandler(this.rbAu_CheckedChanged);
            // 
            // lbNganh
            // 
            this.lbNganh.AutoSize = true;
            this.lbNganh.Location = new System.Drawing.Point(6, 20);
            this.lbNganh.Name = "lbNganh";
            this.lbNganh.Size = new System.Drawing.Size(53, 16);
            this.lbNganh.TabIndex = 87;
            this.lbNganh.Text = "Ngành:";
            // 
            // tbTenKhoa
            // 
            this.tbTenKhoa.Location = new System.Drawing.Point(262, 53);
            this.tbTenKhoa.MaxLength = 50;
            this.tbTenKhoa.Name = "tbTenKhoa";
            this.tbTenKhoa.Size = new System.Drawing.Size(223, 23);
            this.tbTenKhoa.TabIndex = 7;
            this.tbTenKhoa.TextChanged += new System.EventHandler(this.tbTenKhoa_TextChanged);
            // 
            // tbKhoaTruong
            // 
            this.tbKhoaTruong.Location = new System.Drawing.Point(263, 93);
            this.tbKhoaTruong.MaxLength = 30;
            this.tbKhoaTruong.Name = "tbKhoaTruong";
            this.tbKhoaTruong.Size = new System.Drawing.Size(222, 23);
            this.tbKhoaTruong.TabIndex = 9;
            this.tbKhoaTruong.TextChanged += new System.EventHandler(this.tbKhoaTruong_TextChanged);
            // 
            // lbTenKhoa
            // 
            this.lbTenKhoa.AutoSize = true;
            this.lbTenKhoa.Location = new System.Drawing.Point(185, 56);
            this.lbTenKhoa.Name = "lbTenKhoa";
            this.lbTenKhoa.Size = new System.Drawing.Size(72, 16);
            this.lbTenKhoa.TabIndex = 75;
            this.lbTenKhoa.Text = "Tên khóa:";
            // 
            // lbKhoaTruong
            // 
            this.lbKhoaTruong.AutoSize = true;
            this.lbKhoaTruong.Location = new System.Drawing.Point(165, 96);
            this.lbKhoaTruong.Name = "lbKhoaTruong";
            this.lbKhoaTruong.Size = new System.Drawing.Size(92, 16);
            this.lbKhoaTruong.TabIndex = 29;
            this.lbKhoaTruong.Text = "Khóa trưởng:";
            // 
            // lbNam
            // 
            this.lbNam.AutoSize = true;
            this.lbNam.Location = new System.Drawing.Point(11, 96);
            this.lbNam.Name = "lbNam";
            this.lbNam.Size = new System.Drawing.Size(40, 16);
            this.lbNam.TabIndex = 27;
            this.lbNam.Text = "Năm:";
            // 
            // lbKhoa
            // 
            this.lbKhoa.AutoSize = true;
            this.lbKhoa.Location = new System.Drawing.Point(6, 56);
            this.lbKhoa.Name = "lbKhoa";
            this.lbKhoa.Size = new System.Drawing.Size(45, 16);
            this.lbKhoa.TabIndex = 17;
            this.lbKhoa.Text = "Khóa:";
            // 
            // cbKhoa
            // 
            this.cbKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhoa.FormattingEnabled = true;
            this.cbKhoa.Items.AddRange(new object[] {
            "Cơ Bản",
            "Dự Bị",
            "Bạch Mã",
            "Bằng Rừng",
            "Khác"});
            this.cbKhoa.Location = new System.Drawing.Point(54, 53);
            this.cbKhoa.Name = "cbKhoa";
            this.cbKhoa.Size = new System.Drawing.Size(90, 24);
            this.cbKhoa.TabIndex = 6;
            this.cbKhoa.SelectedIndexChanged += new System.EventHandler(this.cbKhoa_SelectedIndexChanged);
            // 
            // UC_HuanLuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbKhoa);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_HuanLuyen";
            this.Size = new System.Drawing.Size(503, 174);
            this.Load += new System.EventHandler(this.UC_HuanLuyen_Load);
            this.gbKhoa.ResumeLayout(false);
            this.gbKhoa.PerformLayout();
            this.pnTinhTrang.ResumeLayout(false);
            this.pnTinhTrang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbKhoa;
        private System.Windows.Forms.Panel pnTinhTrang;
        private System.Windows.Forms.RadioButton rbThamDu;
        private System.Windows.Forms.RadioButton rbTrungCach;
        private System.Windows.Forms.Label lbTinhTrang;
        private System.Windows.Forms.TextBox tbMHL;
        private System.Windows.Forms.Label lbMHL;
        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.DateTimePicker dtpNam;
        private System.Windows.Forms.RadioButton rbKhac;
        private System.Windows.Forms.RadioButton rbTrang;
        private System.Windows.Forms.RadioButton rbKha;
        private System.Windows.Forms.RadioButton rbThieu;
        private System.Windows.Forms.RadioButton rbAu;
        private System.Windows.Forms.Label lbNganh;
        private System.Windows.Forms.TextBox tbTenKhoa;
        private System.Windows.Forms.TextBox tbKhoaTruong;
        private System.Windows.Forms.Label lbTenKhoa;
        private System.Windows.Forms.Label lbKhoaTruong;
        private System.Windows.Forms.Label lbNam;
        private System.Windows.Forms.Label lbKhoa;
        private System.Windows.Forms.ComboBox cbKhoa;

    }
}
