namespace Weedon.DinhLuong
{
    partial class UcDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.gbNguyenLieu = new System.Windows.Forms.GroupBox();
            this.pn_gbNguyenLieu = new System.Windows.Forms.Panel();
            this.dgvThongTin = new System.Windows.Forms.DataGridView();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.pbHoanTat = new System.Windows.Forms.PictureBox();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbTamNgung = new System.Windows.Forms.RadioButton();
            this.rbBan = new System.Windows.Forms.RadioButton();
            this.tbMoTa = new System.Windows.Forms.TextBox();
            this.tbTen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMa = new System.Windows.Forms.TextBox();
            this.lbMa = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbSelect = new System.Windows.Forms.Label();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdNL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTen = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colUocLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonVi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnInfo.SuspendLayout();
            this.gbNguyenLieu.SuspendLayout();
            this.pn_gbNguyenLieu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongTin)).BeginInit();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).BeginInit();
            this.gbInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pnTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnInfo
            // 
            this.pnInfo.Controls.Add(this.gbNguyenLieu);
            this.pnInfo.Controls.Add(this.gbInfo);
            this.pnInfo.ForeColor = System.Drawing.Color.Black;
            this.pnInfo.Location = new System.Drawing.Point(100, 47);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(800, 510);
            this.pnInfo.TabIndex = 49;
            // 
            // gbNguyenLieu
            // 
            this.gbNguyenLieu.Controls.Add(this.pn_gbNguyenLieu);
            this.gbNguyenLieu.Controls.Add(this.panel12);
            this.gbNguyenLieu.ForeColor = System.Drawing.Color.Orange;
            this.gbNguyenLieu.Location = new System.Drawing.Point(3, 169);
            this.gbNguyenLieu.Name = "gbNguyenLieu";
            this.gbNguyenLieu.Size = new System.Drawing.Size(794, 338);
            this.gbNguyenLieu.TabIndex = 57;
            this.gbNguyenLieu.TabStop = false;
            this.gbNguyenLieu.Text = "Nguyên liệu";
            // 
            // pn_gbNguyenLieu
            // 
            this.pn_gbNguyenLieu.AutoScroll = true;
            this.pn_gbNguyenLieu.Controls.Add(this.dgvThongTin);
            this.pn_gbNguyenLieu.Location = new System.Drawing.Point(6, 22);
            this.pn_gbNguyenLieu.Name = "pn_gbNguyenLieu";
            this.pn_gbNguyenLieu.Size = new System.Drawing.Size(782, 234);
            this.pn_gbNguyenLieu.TabIndex = 81;
            // 
            // dgvThongTin
            // 
            this.dgvThongTin.AllowUserToAddRows = false;
            this.dgvThongTin.AllowUserToDeleteRows = false;
            this.dgvThongTin.AllowUserToResizeRows = false;
            this.dgvThongTin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongTin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colIdNL,
            this.colMa,
            this.colTen,
            this.colUocLuong,
            this.colDonVi,
            this.colGhiChu});
            this.dgvThongTin.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvThongTin.Location = new System.Drawing.Point(3, 3);
            this.dgvThongTin.MultiSelect = false;
            this.dgvThongTin.Name = "dgvThongTin";
            this.dgvThongTin.ReadOnly = true;
            this.dgvThongTin.RowHeadersVisible = false;
            this.dgvThongTin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThongTin.ShowCellToolTips = false;
            this.dgvThongTin.Size = new System.Drawing.Size(776, 231);
            this.dgvThongTin.TabIndex = 2;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label47);
            this.panel12.Controls.Add(this.pbHoanTat);
            this.panel12.Location = new System.Drawing.Point(362, 262);
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
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.label3);
            this.gbInfo.Controls.Add(this.rbTamNgung);
            this.gbInfo.Controls.Add(this.rbBan);
            this.gbInfo.Controls.Add(this.tbMoTa);
            this.gbInfo.Controls.Add(this.tbTen);
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Controls.Add(this.cbGroup);
            this.gbInfo.Controls.Add(this.label4);
            this.gbInfo.Controls.Add(this.tbMa);
            this.gbInfo.Controls.Add(this.lbMa);
            this.gbInfo.Controls.Add(this.label2);
            this.gbInfo.ForeColor = System.Drawing.Color.Orange;
            this.gbInfo.Location = new System.Drawing.Point(3, 3);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(794, 160);
            this.gbInfo.TabIndex = 2;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Thông tin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(45, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 121;
            this.label3.Text = "Tình trạng:";
            // 
            // rbTamNgung
            // 
            this.rbTamNgung.AutoSize = true;
            this.rbTamNgung.Enabled = false;
            this.rbTamNgung.ForeColor = System.Drawing.Color.Black;
            this.rbTamNgung.Location = new System.Drawing.Point(240, 124);
            this.rbTamNgung.Name = "rbTamNgung";
            this.rbTamNgung.Size = new System.Drawing.Size(99, 20);
            this.rbTamNgung.TabIndex = 120;
            this.rbTamNgung.TabStop = true;
            this.rbTamNgung.Text = "Tạm ngưng";
            this.rbTamNgung.UseVisualStyleBackColor = true;
            // 
            // rbBan
            // 
            this.rbBan.AutoSize = true;
            this.rbBan.Checked = true;
            this.rbBan.Enabled = false;
            this.rbBan.ForeColor = System.Drawing.Color.Black;
            this.rbBan.Location = new System.Drawing.Point(129, 124);
            this.rbBan.Name = "rbBan";
            this.rbBan.Size = new System.Drawing.Size(51, 20);
            this.rbBan.TabIndex = 119;
            this.rbBan.TabStop = true;
            this.rbBan.Text = "Bán";
            this.rbBan.UseVisualStyleBackColor = true;
            // 
            // tbMoTa
            // 
            this.tbMoTa.Location = new System.Drawing.Point(485, 78);
            this.tbMoTa.MaxLength = 200;
            this.tbMoTa.Multiline = true;
            this.tbMoTa.Name = "tbMoTa";
            this.tbMoTa.ReadOnly = true;
            this.tbMoTa.Size = new System.Drawing.Size(250, 66);
            this.tbMoTa.TabIndex = 9;
            // 
            // tbTen
            // 
            this.tbTen.Location = new System.Drawing.Point(129, 78);
            this.tbTen.MaxLength = 50;
            this.tbTen.Name = "tbTen";
            this.tbTen.ReadOnly = true;
            this.tbTen.Size = new System.Drawing.Size(210, 23);
            this.tbTen.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(81, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 100;
            this.label1.Text = "*Tên:";
            // 
            // cbGroup
            // 
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.Enabled = false;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(485, 30);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(250, 24);
            this.cbGroup.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(432, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 97;
            this.label4.Text = "Mô tả:";
            // 
            // tbMa
            // 
            this.tbMa.Location = new System.Drawing.Point(129, 30);
            this.tbMa.MaxLength = 15;
            this.tbMa.Name = "tbMa";
            this.tbMa.ReadOnly = true;
            this.tbMa.Size = new System.Drawing.Size(100, 23);
            this.tbMa.TabIndex = 0;
            // 
            // lbMa
            // 
            this.lbMa.AutoSize = true;
            this.lbMa.ForeColor = System.Drawing.Color.Black;
            this.lbMa.Location = new System.Drawing.Point(87, 33);
            this.lbMa.Name = "lbMa";
            this.lbMa.Size = new System.Drawing.Size(36, 16);
            this.lbMa.TabIndex = 90;
            this.lbMa.Text = "*Mã:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(426, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "*Nhóm:";
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
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Gray;
            this.lbTitle.Location = new System.Drawing.Point(147, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(132, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "ĐỊNH LƯỢNG";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbSelect
            // 
            this.lbSelect.AutoSize = true;
            this.lbSelect.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelect.ForeColor = System.Drawing.Color.Orange;
            this.lbSelect.Location = new System.Drawing.Point(50, 8);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.Size = new System.Drawing.Size(91, 22);
            this.lbSelect.TabIndex = 1;
            this.lbSelect.Text = "CHI TIẾT";
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.lbSelect);
            this.pnTitle.Controls.Add(this.lbTitle);
            this.pnTitle.Controls.Add(this.pbTitle);
            this.pnTitle.Location = new System.Drawing.Point(343, 3);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(308, 38);
            this.pnTitle.TabIndex = 50;
            // 
            // colId
            // 
            this.colId.Frozen = true;
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colIdNL
            // 
            this.colIdNL.Frozen = true;
            this.colIdNL.HeaderText = "IdNL";
            this.colIdNL.Name = "colIdNL";
            this.colIdNL.ReadOnly = true;
            this.colIdNL.Visible = false;
            this.colIdNL.Width = 5;
            // 
            // colMa
            // 
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.colMa.DefaultCellStyle = dataGridViewCellStyle1;
            this.colMa.Frozen = true;
            this.colMa.HeaderText = "Mã";
            this.colMa.MaxInputLength = 50;
            this.colMa.MinimumWidth = 100;
            this.colMa.Name = "colMa";
            this.colMa.ReadOnly = true;
            // 
            // colTen
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.colTen.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTen.Frozen = true;
            this.colTen.HeaderText = "Tên";
            this.colTen.MaxDropDownItems = 10;
            this.colTen.MinimumWidth = 250;
            this.colTen.Name = "colTen";
            this.colTen.ReadOnly = true;
            this.colTen.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colTen.Width = 250;
            // 
            // colUocLuong
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.colUocLuong.DefaultCellStyle = dataGridViewCellStyle3;
            this.colUocLuong.HeaderText = "Ước lượng";
            this.colUocLuong.MaxInputLength = 6;
            this.colUocLuong.MinimumWidth = 100;
            this.colUocLuong.Name = "colUocLuong";
            this.colUocLuong.ReadOnly = true;
            // 
            // colDonVi
            // 
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.colDonVi.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDonVi.HeaderText = "Đơn vị";
            this.colDonVi.MaxInputLength = 11;
            this.colDonVi.MinimumWidth = 100;
            this.colDonVi.Name = "colDonVi";
            this.colDonVi.ReadOnly = true;
            // 
            // colGhiChu
            // 
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.colGhiChu.DefaultCellStyle = dataGridViewCellStyle5;
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.MaxInputLength = 200;
            this.colGhiChu.MinimumWidth = 200;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            this.colGhiChu.Width = 200;
            // 
            // UcDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.pnInfo);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcDetail";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcDetail_Load);
            this.pnInfo.ResumeLayout(false);
            this.gbNguyenLieu.ResumeLayout(false);
            this.pn_gbNguyenLieu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongTin)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).EndInit();
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.TextBox tbTen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.PictureBox pbHoanTat;
        private System.Windows.Forms.TextBox tbMa;
        private System.Windows.Forms.Label lbMa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMoTa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbTamNgung;
        private System.Windows.Forms.RadioButton rbBan;
        private System.Windows.Forms.GroupBox gbNguyenLieu;
        private System.Windows.Forms.Panel pn_gbNguyenLieu;
        private System.Windows.Forms.DataGridView dgvThongTin;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbSelect;
        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdNL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMa;
        private System.Windows.Forms.DataGridViewComboBoxColumn colTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUocLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonVi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
    }
}
