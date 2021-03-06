﻿namespace Weedon.HoaDon
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.dgvThongTin = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lvThongTin = new System.Windows.Forms.ListView();
            this.chCheckBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSanPham = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.tbGhiChu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbNgayGio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.pbHoanTat = new System.Windows.Forms.PictureBox();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.lbSelect = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.pnDetail = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbMaHD = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label39 = new System.Windows.Forms.Label();
            this.pbHuy = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbTongHoaDon = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ttDetail = new System.Windows.Forms.ToolTip(this.components);
            this.dtpFilter = new System.Windows.Forms.DateTimePicker();
            this.pnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongTin)).BeginInit();
            this.gbInfo.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).BeginInit();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pnDetail.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).BeginInit();
            this.SuspendLayout();
            // 
            // pnInfo
            // 
            this.pnInfo.Controls.Add(this.dgvThongTin);
            this.pnInfo.ForeColor = System.Drawing.Color.Black;
            this.pnInfo.Location = new System.Drawing.Point(20, 40);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(950, 170);
            this.pnInfo.TabIndex = 56;
            // 
            // dgvThongTin
            // 
            this.dgvThongTin.AllowUserToAddRows = false;
            this.dgvThongTin.AllowUserToDeleteRows = false;
            this.dgvThongTin.AllowUserToResizeRows = false;
            this.dgvThongTin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongTin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colIdSanPham,
            this.colSanPham,
            this.colGia,
            this.colSoLuong,
            this.colThanhTien,
            this.colGhiChu,
            this.colRemove});
            this.dgvThongTin.Location = new System.Drawing.Point(3, 3);
            this.dgvThongTin.MultiSelect = false;
            this.dgvThongTin.Name = "dgvThongTin";
            this.dgvThongTin.RowHeadersVisible = false;
            this.dgvThongTin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThongTin.ShowCellToolTips = false;
            this.dgvThongTin.Size = new System.Drawing.Size(944, 163);
            this.dgvThongTin.TabIndex = 1;
            this.dgvThongTin.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThongTin_CellContentClick);
            this.dgvThongTin.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThongTin_CellEndEdit);
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
            // colIdSanPham
            // 
            this.colIdSanPham.Frozen = true;
            this.colIdSanPham.HeaderText = "IdSanPham";
            this.colIdSanPham.Name = "colIdSanPham";
            this.colIdSanPham.ReadOnly = true;
            this.colIdSanPham.Visible = false;
            this.colIdSanPham.Width = 5;
            // 
            // colSanPham
            // 
            this.colSanPham.Frozen = true;
            this.colSanPham.HeaderText = "Sản phẩm";
            this.colSanPham.MaxInputLength = 50;
            this.colSanPham.MinimumWidth = 300;
            this.colSanPham.Name = "colSanPham";
            this.colSanPham.ReadOnly = true;
            this.colSanPham.Width = 300;
            // 
            // colGia
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.colGia.DefaultCellStyle = dataGridViewCellStyle1;
            this.colGia.HeaderText = "Giá";
            this.colGia.MaxInputLength = 11;
            this.colGia.MinimumWidth = 120;
            this.colGia.Name = "colGia";
            this.colGia.ReadOnly = true;
            this.colGia.Width = 120;
            // 
            // colSoLuong
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.MaxInputLength = 3;
            this.colSoLuong.MinimumWidth = 100;
            this.colSoLuong.Name = "colSoLuong";
            // 
            // colThanhTien
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = "0";
            this.colThanhTien.DefaultCellStyle = dataGridViewCellStyle3;
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.MaxInputLength = 11;
            this.colThanhTien.MinimumWidth = 120;
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 120;
            // 
            // colGhiChu
            // 
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.MaxInputLength = 200;
            this.colGhiChu.MinimumWidth = 200;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Width = 200;
            // 
            // colRemove
            // 
            this.colRemove.HeaderText = "";
            this.colRemove.MinimumWidth = 30;
            this.colRemove.Name = "colRemove";
            this.colRemove.Text = "X";
            this.colRemove.UseColumnTextForButtonValue = true;
            this.colRemove.Width = 30;
            // 
            // lvThongTin
            // 
            this.lvThongTin.CheckBoxes = true;
            this.lvThongTin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCheckBox,
            this.chId,
            this.chSTT,
            this.chSanPham,
            this.chGia});
            this.lvThongTin.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvThongTin.FullRowSelect = true;
            this.lvThongTin.GridLines = true;
            this.lvThongTin.Location = new System.Drawing.Point(6, 22);
            this.lvThongTin.MultiSelect = false;
            this.lvThongTin.Name = "lvThongTin";
            this.lvThongTin.Size = new System.Drawing.Size(635, 302);
            this.lvThongTin.TabIndex = 87;
            this.lvThongTin.UseCompatibleStateImageBehavior = false;
            this.lvThongTin.View = System.Windows.Forms.View.Details;
            this.lvThongTin.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvThongTin_ColumnClick);
            this.lvThongTin.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvThongTin_ColumnWidthChanging);
            this.lvThongTin.SelectedIndexChanged += new System.EventHandler(this.lvThongTin_SelectedIndexChanged);
            this.lvThongTin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvThongTin_KeyPress);
            this.lvThongTin.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvThongTin_MouseDoubleClick);
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
            this.chSTT.Width = 45;
            // 
            // chSanPham
            // 
            this.chSanPham.Text = "Sản phẩm";
            this.chSanPham.Width = 450;
            // 
            // chGia
            // 
            this.chGia.Text = "Giá";
            this.chGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chGia.Width = 100;
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.lvThongTin);
            this.gbInfo.ForeColor = System.Drawing.Color.Orange;
            this.gbInfo.Location = new System.Drawing.Point(298, 3);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(647, 330);
            this.gbInfo.TabIndex = 20;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Danh sách sản phẩm";
            // 
            // tbGhiChu
            // 
            this.tbGhiChu.Location = new System.Drawing.Point(88, 110);
            this.tbGhiChu.MaxLength = 200;
            this.tbGhiChu.Multiline = true;
            this.tbGhiChu.Name = "tbGhiChu";
            this.tbGhiChu.Size = new System.Drawing.Size(169, 148);
            this.tbGhiChu.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(21, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 97;
            this.label4.Text = "Ghi chú:";
            // 
            // lbNgayGio
            // 
            this.lbNgayGio.AutoSize = true;
            this.lbNgayGio.ForeColor = System.Drawing.Color.Black;
            this.lbNgayGio.Location = new System.Drawing.Point(102, 18);
            this.lbNgayGio.Name = "lbNgayGio";
            this.lbNgayGio.Size = new System.Drawing.Size(137, 16);
            this.lbNgayGio.TabIndex = 140;
            this.lbNgayGio.Text = "dd/MM/yyyy - hh:mm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(36, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 122;
            this.label1.Text = "Ngày giờ:";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label47);
            this.panel12.Controls.Add(this.pbHoanTat);
            this.panel12.Location = new System.Drawing.Point(209, 264);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(70, 60);
            this.panel12.TabIndex = 128;
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
            this.pbHoanTat.Enabled = false;
            this.pbHoanTat.Location = new System.Drawing.Point(10, 10);
            this.pbHoanTat.Name = "pbHoanTat";
            this.pbHoanTat.Size = new System.Drawing.Size(50, 39);
            this.pbHoanTat.TabIndex = 1;
            this.pbHoanTat.TabStop = false;
            this.pbHoanTat.Click += new System.EventHandler(this.pbHoanTat_Click);
            this.pbHoanTat.MouseEnter += new System.EventHandler(this.pbHoanTat_MouseEnter);
            this.pbHoanTat.MouseLeave += new System.EventHandler(this.pbHoanTat_MouseLeave);
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.lbSelect);
            this.pnTitle.Controls.Add(this.lbTitle);
            this.pnTitle.Controls.Add(this.pbTitle);
            this.pnTitle.Location = new System.Drawing.Point(381, 3);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(249, 38);
            this.pnTitle.TabIndex = 55;
            // 
            // lbSelect
            // 
            this.lbSelect.AutoSize = true;
            this.lbSelect.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelect.ForeColor = System.Drawing.Color.Orange;
            this.lbSelect.Location = new System.Drawing.Point(50, 8);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.Size = new System.Drawing.Size(98, 22);
            this.lbSelect.TabIndex = 1;
            this.lbSelect.Text = "HÓA ĐƠN";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Gray;
            this.lbTitle.Location = new System.Drawing.Point(118, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(111, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "KHO HÀNG";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbTitle.Visible = false;
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
            // pnDetail
            // 
            this.pnDetail.Controls.Add(this.gbInfo);
            this.pnDetail.Controls.Add(this.groupBox1);
            this.pnDetail.ForeColor = System.Drawing.Color.Black;
            this.pnDetail.Location = new System.Drawing.Point(20, 209);
            this.pnDetail.Name = "pnDetail";
            this.pnDetail.Size = new System.Drawing.Size(950, 350);
            this.pnDetail.TabIndex = 57;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbMaHD);
            this.groupBox1.Controls.Add(this.panel8);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tbTongHoaDon);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tbGhiChu);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.panel12);
            this.groupBox1.ForeColor = System.Drawing.Color.Orange;
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 330);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chi tiết";
            // 
            // tbMaHD
            // 
            this.tbMaHD.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMaHD.Location = new System.Drawing.Point(87, 30);
            this.tbMaHD.MaxLength = 10;
            this.tbMaHD.Name = "tbMaHD";
            this.tbMaHD.ReadOnly = true;
            this.tbMaHD.Size = new System.Drawing.Size(73, 26);
            this.tbMaHD.TabIndex = 4;
            this.tbMaHD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label39);
            this.panel8.Controls.Add(this.pbHuy);
            this.panel8.Location = new System.Drawing.Point(6, 264);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(70, 60);
            this.panel8.TabIndex = 141;
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
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(27, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 16);
            this.label14.TabIndex = 4;
            this.label14.Text = "Mã HĐ:";
            // 
            // tbTongHoaDon
            // 
            this.tbTongHoaDon.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTongHoaDon.Location = new System.Drawing.Point(87, 70);
            this.tbTongHoaDon.MaxLength = 11;
            this.tbTongHoaDon.Name = "tbTongHoaDon";
            this.tbTongHoaDon.ReadOnly = true;
            this.tbTongHoaDon.Size = new System.Drawing.Size(127, 26);
            this.tbTongHoaDon.TabIndex = 6;
            this.tbTongHoaDon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(13, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 16);
            this.label10.TabIndex = 106;
            this.label10.Text = "Tổng HĐ:";
            // 
            // ttDetail
            // 
            this.ttDetail.AutoPopDelay = 10000;
            this.ttDetail.InitialDelay = 500;
            this.ttDetail.ReshowDelay = 100;
            // 
            // dtpFilter
            // 
            this.dtpFilter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFilter.Location = new System.Drawing.Point(236, 13);
            this.dtpFilter.Name = "dtpFilter";
            this.dtpFilter.Size = new System.Drawing.Size(150, 23);
            this.dtpFilter.TabIndex = 141;
            this.dtpFilter.Visible = false;
            // 
            // UcInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpFilter);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.lbNgayGio);
            this.Controls.Add(this.pnInfo);
            this.Controls.Add(this.pnDetail);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcInfo";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcInfo_Load);
            this.pnInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongTin)).EndInit();
            this.gbInfo.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).EndInit();
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pnDetail.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.TextBox tbGhiChu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label lbSelect;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnDetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListView lvThongTin;
        private System.Windows.Forms.ColumnHeader chSTT;
        private System.Windows.Forms.ColumnHeader chSanPham;
        private System.Windows.Forms.ColumnHeader chGia;
        private System.Windows.Forms.TextBox tbTongHoaDon;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.PictureBox pbHoanTat;
        private System.Windows.Forms.ColumnHeader chCheckBox;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.Label lbNgayGio;
        private System.Windows.Forms.ToolTip ttDetail;
        private System.Windows.Forms.DataGridView dgvThongTin;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.PictureBox pbHuy;
        private System.Windows.Forms.TextBox tbMaHD;
        private System.Windows.Forms.DateTimePicker dtpFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.DataGridViewButtonColumn colRemove;

    }
}
