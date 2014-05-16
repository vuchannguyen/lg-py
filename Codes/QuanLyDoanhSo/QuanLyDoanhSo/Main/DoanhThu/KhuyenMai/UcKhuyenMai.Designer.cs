namespace Weedon
{
    partial class UcGiaChinhThuc
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
            this.pnTitle = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.pnSelect = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pbExcel = new System.Windows.Forms.PictureBox();
            this.pnSua = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.pbSua = new System.Windows.Forms.PictureBox();
            this.pnQuanLy = new System.Windows.Forms.Panel();
            this.dgvThongTin = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSanPhamKhuyenMai = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colSoLuongSanPhamKhuyenMai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViLamTron = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pnSelect.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).BeginInit();
            this.pnSua.SuspendLayout();
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
            this.lbTitle.Size = new System.Drawing.Size(130, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "KHUYẾN MÃI";
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
            this.pnSelect.Controls.Add(this.pnSua);
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
            this.panel1.Visible = false;
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
            // pnSua
            // 
            this.pnSua.Controls.Add(this.label30);
            this.pnSua.Controls.Add(this.pbSua);
            this.pnSua.Location = new System.Drawing.Point(6, 3);
            this.pnSua.Name = "pnSua";
            this.pnSua.Size = new System.Drawing.Size(70, 70);
            this.pnSua.TabIndex = 28;
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
            this.colIdSanPham,
            this.colSanPham,
            this.colSoLuongSanPham,
            this.colSanPhamKhuyenMai,
            this.colSoLuongSanPhamKhuyenMai,
            this.colDonViLamTron,
            this.colGhiChu});
            this.dgvThongTin.Location = new System.Drawing.Point(3, 3);
            this.dgvThongTin.Name = "dgvThongTin";
            this.dgvThongTin.Size = new System.Drawing.Size(894, 474);
            this.dgvThongTin.TabIndex = 0;
            this.dgvThongTin.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvThongTin_CellBeginEdit);
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
            this.colSanPham.HeaderText = "Sản phẩm";
            this.colSanPham.MaxInputLength = 50;
            this.colSanPham.Name = "colSanPham";
            this.colSanPham.ReadOnly = true;
            this.colSanPham.Width = 300;
            // 
            // colSoLuongSanPham
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.colSoLuongSanPham.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSoLuongSanPham.HeaderText = "SL";
            this.colSoLuongSanPham.MaxInputLength = 11;
            this.colSoLuongSanPham.Name = "colSoLuongSanPham";
            this.colSoLuongSanPham.Width = 70;
            // 
            // colSanPhamKhuyenMai
            // 
            this.colSanPhamKhuyenMai.HeaderText = "Sản phẩm khuyến mãi";
            this.colSanPhamKhuyenMai.Name = "colSanPhamKhuyenMai";
            this.colSanPhamKhuyenMai.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSanPhamKhuyenMai.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colSanPhamKhuyenMai.Width = 200;
            // 
            // colSoLuongSanPhamKhuyenMai
            // 
            this.colSoLuongSanPhamKhuyenMai.HeaderText = "SL KM";
            this.colSoLuongSanPhamKhuyenMai.Name = "colSoLuongSanPhamKhuyenMai";
            this.colSoLuongSanPhamKhuyenMai.Width = 70;
            // 
            // colDonViLamTron
            // 
            this.colDonViLamTron.HeaderText = "Làm tròn";
            this.colDonViLamTron.Name = "colDonViLamTron";
            this.colDonViLamTron.Width = 70;
            // 
            // colGhiChu
            // 
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.Name = "colGhiChu";
            // 
            // UcGiaChinhThuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.pnSelect);
            this.Controls.Add(this.pnQuanLy);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcGiaChinhThuc";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcGiaChinhThuc_Load);
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pnSelect.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).EndInit();
            this.pnSua.ResumeLayout(false);
            this.pnSua.PerformLayout();
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
        private System.Windows.Forms.Panel pnSua;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.PictureBox pbSua;
        private System.Windows.Forms.Panel pnQuanLy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbExcel;
        private System.Windows.Forms.DataGridView dgvThongTin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongSanPham;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSanPhamKhuyenMai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongSanPhamKhuyenMai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViLamTron;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
    }
}
