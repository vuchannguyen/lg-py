﻿namespace Weedon
{
    partial class UcNhapHang
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
            this.pnTitle = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.pnSelect = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pbExcel = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.pbSua = new System.Windows.Forms.PictureBox();
            this.pnQuanLy = new System.Windows.Forms.Panel();
            this.dgvThongTin = new System.Windows.Forms.DataGridView();
            this.dtpFilter = new System.Windows.Forms.DateTimePicker();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdNguyenLieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguyenLieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTonDau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTonCuoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pnSelect.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSua)).BeginInit();
            this.pnQuanLy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongTin)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.lbTitle);
            this.pnTitle.Controls.Add(this.pbTitle);
            this.pnTitle.Location = new System.Drawing.Point(591, 6);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(310, 38);
            this.pnTitle.TabIndex = 50;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Gray;
            this.lbTitle.Location = new System.Drawing.Point(52, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(122, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "NHẬP HÀNG";
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
            this.pnSelect.Controls.Add(this.panel1);
            this.pnSelect.Controls.Add(this.panel3);
            this.pnSelect.Location = new System.Drawing.Point(140, 3);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(310, 78);
            this.pnSelect.TabIndex = 49;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pbExcel);
            this.panel1.Location = new System.Drawing.Point(82, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(70, 70);
            this.panel1.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(-1, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Xuất Excel";
            // 
            // pbExcel
            // 
            this.pbExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExcel.Location = new System.Drawing.Point(10, 0);
            this.pbExcel.Name = "pbExcel";
            this.pbExcel.Size = new System.Drawing.Size(50, 50);
            this.pbExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExcel.TabIndex = 1;
            this.pbExcel.TabStop = false;
            this.pbExcel.Click += new System.EventHandler(this.pbExcel_Click);
            this.pbExcel.MouseEnter += new System.EventHandler(this.pbExcel_MouseEnter);
            this.pbExcel.MouseLeave += new System.EventHandler(this.pbExcel_MouseLeave);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.pbSua);
            this.panel3.Location = new System.Drawing.Point(6, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(70, 70);
            this.panel3.TabIndex = 28;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(2, 53);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(66, 16);
            this.label30.TabIndex = 1;
            this.label30.Text = "Cập nhật";
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
            this.pbSua.Click += new System.EventHandler(this.pbSua_Click);
            this.pbSua.MouseEnter += new System.EventHandler(this.pbSua_MouseEnter);
            this.pbSua.MouseLeave += new System.EventHandler(this.pbSua_MouseLeave);
            // 
            // pnQuanLy
            // 
            this.pnQuanLy.Controls.Add(this.dgvThongTin);
            this.pnQuanLy.Location = new System.Drawing.Point(50, 87);
            this.pnQuanLy.Name = "pnQuanLy";
            this.pnQuanLy.Size = new System.Drawing.Size(900, 480);
            this.pnQuanLy.TabIndex = 48;
            // 
            // dgvThongTin
            // 
            this.dgvThongTin.AllowUserToAddRows = false;
            this.dgvThongTin.AllowUserToDeleteRows = false;
            this.dgvThongTin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongTin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colIdNguyenLieu,
            this.colNguyenLieu,
            this.colDVT,
            this.colTonDau,
            this.colNhap,
            this.colTonCuoi,
            this.colGhiChu});
            this.dgvThongTin.Location = new System.Drawing.Point(3, 3);
            this.dgvThongTin.MultiSelect = false;
            this.dgvThongTin.Name = "dgvThongTin";
            this.dgvThongTin.RowHeadersVisible = false;
            this.dgvThongTin.Size = new System.Drawing.Size(894, 474);
            this.dgvThongTin.TabIndex = 0;
            this.dgvThongTin.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThongTin_CellEndEdit);
            // 
            // dtpFilter
            // 
            this.dtpFilter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFilter.Location = new System.Drawing.Point(456, 54);
            this.dtpFilter.Name = "dtpFilter";
            this.dtpFilter.Size = new System.Drawing.Size(150, 23);
            this.dtpFilter.TabIndex = 51;
            this.dtpFilter.ValueChanged += new System.EventHandler(this.dtpFilter_ValueChanged);
            // 
            // colId
            // 
            this.colId.Frozen = true;
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            this.colId.Width = 5;
            // 
            // colIdNguyenLieu
            // 
            this.colIdNguyenLieu.Frozen = true;
            this.colIdNguyenLieu.HeaderText = "IdSanPham";
            this.colIdNguyenLieu.Name = "colIdNguyenLieu";
            this.colIdNguyenLieu.ReadOnly = true;
            this.colIdNguyenLieu.Visible = false;
            this.colIdNguyenLieu.Width = 5;
            // 
            // colNguyenLieu
            // 
            this.colNguyenLieu.Frozen = true;
            this.colNguyenLieu.HeaderText = "Sản phẩm";
            this.colNguyenLieu.MaxInputLength = 50;
            this.colNguyenLieu.MinimumWidth = 170;
            this.colNguyenLieu.Name = "colNguyenLieu";
            this.colNguyenLieu.ReadOnly = true;
            this.colNguyenLieu.Width = 170;
            // 
            // colDVT
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colDVT.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDVT.Frozen = true;
            this.colDVT.HeaderText = "ĐVT";
            this.colDVT.MaxInputLength = 10;
            this.colDVT.Name = "colDVT";
            this.colDVT.ReadOnly = true;
            this.colDVT.Width = 70;
            // 
            // colTonDau
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.colTonDau.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTonDau.Frozen = true;
            this.colTonDau.HeaderText = "Tồn đầu";
            this.colTonDau.MaxInputLength = 7;
            this.colTonDau.MinimumWidth = 90;
            this.colTonDau.Name = "colTonDau";
            this.colTonDau.Width = 90;
            // 
            // colNhap
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.colNhap.DefaultCellStyle = dataGridViewCellStyle3;
            this.colNhap.Frozen = true;
            this.colNhap.HeaderText = "Nhập";
            this.colNhap.MaxInputLength = 7;
            this.colNhap.Name = "colNhap";
            this.colNhap.Width = 90;
            // 
            // colTonCuoi
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.colTonCuoi.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTonCuoi.Frozen = true;
            this.colTonCuoi.HeaderText = "Tồn cuối";
            this.colTonCuoi.MaxInputLength = 7;
            this.colTonCuoi.MinimumWidth = 90;
            this.colTonCuoi.Name = "colTonCuoi";
            this.colTonCuoi.Width = 90;
            // 
            // colGhiChu
            // 
            this.colGhiChu.Frozen = true;
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.MinimumWidth = 110;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Width = 110;
            // 
            // UcNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpFilter);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.pnSelect);
            this.Controls.Add(this.pnQuanLy);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcNhapHang";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcNhapHang_Load);
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pnSelect.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSua)).EndInit();
            this.pnQuanLy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongTin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.PictureBox pbSua;
        private System.Windows.Forms.Panel pnQuanLy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbExcel;
        private System.Windows.Forms.DateTimePicker dtpFilter;
        private System.Windows.Forms.DataGridView dgvThongTin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdNguyenLieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguyenLieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDVT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTonDau;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTonCuoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
    }
}