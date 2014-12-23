namespace QuanLyPhongTap
{
    partial class UcPhongTap
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
            this.tlAll = new System.Windows.Forms.TableLayoutPanel();
            this.lvThongTin = new System.Windows.Forms.ListView();
            this.chCheckBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNgayHetHan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSoXe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDTDD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGhiChu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlTop = new System.Windows.Forms.TableLayoutPanel();
            this.pnActions = new System.Windows.Forms.Panel();
            this.btSua = new System.Windows.Forms.Button();
            this.btXoa = new System.Windows.Forms.Button();
            this.btThem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBottom = new System.Windows.Forms.TableLayoutPanel();
            this.pnPage = new System.Windows.Forms.Panel();
            this.btNextPage = new System.Windows.Forms.Button();
            this.btBackPage = new System.Windows.Forms.Button();
            this.tbPage = new System.Windows.Forms.TextBox();
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbPage = new System.Windows.Forms.Label();
            this.pnSearch = new System.Windows.Forms.Panel();
            this.btSearch = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.tlAll.SuspendLayout();
            this.tlTop.SuspendLayout();
            this.pnActions.SuspendLayout();
            this.tbBottom.SuspendLayout();
            this.pnPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlAll
            // 
            this.tlAll.ColumnCount = 1;
            this.tlAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlAll.Controls.Add(this.lvThongTin, 0, 1);
            this.tlAll.Controls.Add(this.tlTop, 0, 0);
            this.tlAll.Controls.Add(this.tbBottom, 0, 2);
            this.tlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlAll.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlAll.Location = new System.Drawing.Point(0, 0);
            this.tlAll.Name = "tlAll";
            this.tlAll.RowCount = 3;
            this.tlAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlAll.Size = new System.Drawing.Size(800, 550);
            this.tlAll.TabIndex = 0;
            // 
            // lvThongTin
            // 
            this.lvThongTin.CheckBoxes = true;
            this.lvThongTin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCheckBox,
            this.chId,
            this.chSTT,
            this.chMa,
            this.chName,
            this.chNgayHetHan,
            this.chSoXe,
            this.chDTDD,
            this.chGhiChu});
            this.lvThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvThongTin.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvThongTin.FullRowSelect = true;
            this.lvThongTin.GridLines = true;
            this.lvThongTin.Location = new System.Drawing.Point(3, 63);
            this.lvThongTin.MultiSelect = false;
            this.lvThongTin.Name = "lvThongTin";
            this.lvThongTin.Size = new System.Drawing.Size(794, 444);
            this.lvThongTin.TabIndex = 87;
            this.lvThongTin.UseCompatibleStateImageBehavior = false;
            this.lvThongTin.View = System.Windows.Forms.View.Details;
            this.lvThongTin.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvThongTin_ColumnWidthChanging);
            this.lvThongTin.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvThongTin_ItemChecked);
            this.lvThongTin.SelectedIndexChanged += new System.EventHandler(this.lvThongTin_SelectedIndexChanged);
            this.lvThongTin.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvThongTin_MouseClick);
            // 
            // chCheckBox
            // 
            this.chCheckBox.Text = "All";
            this.chCheckBox.Width = 30;
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
            // chMa
            // 
            this.chMa.Text = "Mã";
            this.chMa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chMa.Width = 96;
            // 
            // chName
            // 
            this.chName.Text = "Họ và tên";
            this.chName.Width = 156;
            // 
            // chNgayHetHan
            // 
            this.chNgayHetHan.Text = "Ngày hết hạn";
            this.chNgayHetHan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chNgayHetHan.Width = 144;
            // 
            // chSoXe
            // 
            this.chSoXe.Text = "Số xe";
            this.chSoXe.Width = 76;
            // 
            // chDTDD
            // 
            this.chDTDD.Text = "ĐTDĐ";
            this.chDTDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chDTDD.Width = 100;
            // 
            // chGhiChu
            // 
            this.chGhiChu.Text = "Ghi chú";
            this.chGhiChu.Width = 143;
            // 
            // tlTop
            // 
            this.tlTop.ColumnCount = 2;
            this.tlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlTop.Controls.Add(this.pnActions, 0, 0);
            this.tlTop.Controls.Add(this.label1, 1, 0);
            this.tlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlTop.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlTop.Location = new System.Drawing.Point(3, 3);
            this.tlTop.Name = "tlTop";
            this.tlTop.RowCount = 1;
            this.tlTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlTop.Size = new System.Drawing.Size(794, 54);
            this.tlTop.TabIndex = 0;
            // 
            // pnActions
            // 
            this.pnActions.Controls.Add(this.btSua);
            this.pnActions.Controls.Add(this.btXoa);
            this.pnActions.Controls.Add(this.btThem);
            this.pnActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnActions.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnActions.Location = new System.Drawing.Point(3, 3);
            this.pnActions.Name = "pnActions";
            this.pnActions.Size = new System.Drawing.Size(391, 48);
            this.pnActions.TabIndex = 0;
            // 
            // btSua
            // 
            this.btSua.Location = new System.Drawing.Point(79, 3);
            this.btSua.Name = "btSua";
            this.btSua.Size = new System.Drawing.Size(70, 40);
            this.btSua.TabIndex = 2;
            this.btSua.Text = "Sửa";
            this.btSua.UseVisualStyleBackColor = true;
            // 
            // btXoa
            // 
            this.btXoa.Location = new System.Drawing.Point(155, 3);
            this.btXoa.Name = "btXoa";
            this.btXoa.Size = new System.Drawing.Size(70, 40);
            this.btXoa.TabIndex = 1;
            this.btXoa.Text = "Xóa";
            this.btXoa.UseVisualStyleBackColor = true;
            // 
            // btThem
            // 
            this.btThem.Location = new System.Drawing.Point(3, 3);
            this.btThem.Name = "btThem";
            this.btThem.Size = new System.Drawing.Size(70, 40);
            this.btThem.TabIndex = 0;
            this.btThem.Text = "Thêm";
            this.btThem.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(647, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "PHÒNG TẬP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbBottom
            // 
            this.tbBottom.ColumnCount = 2;
            this.tbBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbBottom.Controls.Add(this.pnPage, 1, 0);
            this.tbBottom.Controls.Add(this.pnSearch, 0, 0);
            this.tbBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbBottom.Location = new System.Drawing.Point(3, 513);
            this.tbBottom.Name = "tbBottom";
            this.tbBottom.RowCount = 1;
            this.tbBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbBottom.Size = new System.Drawing.Size(794, 34);
            this.tbBottom.TabIndex = 1;
            // 
            // pnPage
            // 
            this.pnPage.Controls.Add(this.btNextPage);
            this.pnPage.Controls.Add(this.btBackPage);
            this.pnPage.Controls.Add(this.lbTotalPage);
            this.pnPage.Controls.Add(this.panel1);
            this.pnPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPage.Location = new System.Drawing.Point(400, 3);
            this.pnPage.Name = "pnPage";
            this.pnPage.Size = new System.Drawing.Size(391, 28);
            this.pnPage.TabIndex = 1;
            // 
            // btNextPage
            // 
            this.btNextPage.Location = new System.Drawing.Point(368, 2);
            this.btNextPage.Name = "btNextPage";
            this.btNextPage.Size = new System.Drawing.Size(23, 23);
            this.btNextPage.TabIndex = 96;
            this.btNextPage.Text = ">";
            this.btNextPage.UseVisualStyleBackColor = true;
            this.btNextPage.Click += new System.EventHandler(this.btNextPage_Click);
            // 
            // btBackPage
            // 
            this.btBackPage.Location = new System.Drawing.Point(302, 2);
            this.btBackPage.Name = "btBackPage";
            this.btBackPage.Size = new System.Drawing.Size(23, 23);
            this.btBackPage.TabIndex = 95;
            this.btBackPage.Text = "<";
            this.btBackPage.UseVisualStyleBackColor = true;
            this.btBackPage.Click += new System.EventHandler(this.btBackPage_Click);
            // 
            // tbPage
            // 
            this.tbPage.Location = new System.Drawing.Point(3, 1);
            this.tbPage.MaxLength = 3;
            this.tbPage.Name = "tbPage";
            this.tbPage.Size = new System.Drawing.Size(40, 23);
            this.tbPage.TabIndex = 94;
            this.tbPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPage.Visible = false;
            this.tbPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPage_KeyPress);
            // 
            // lbTotalPage
            // 
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPage.ForeColor = System.Drawing.Color.Gray;
            this.lbTotalPage.Location = new System.Drawing.Point(209, 7);
            this.lbTotalPage.Name = "lbTotalPage";
            this.lbTotalPage.Size = new System.Drawing.Size(87, 19);
            this.lbTotalPage.TabIndex = 90;
            this.lbTotalPage.Text = "??? Trang";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbPage);
            this.panel1.Controls.Add(this.tbPage);
            this.panel1.Location = new System.Drawing.Point(324, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(45, 22);
            this.panel1.TabIndex = 91;
            // 
            // lbPage
            // 
            this.lbPage.AutoSize = true;
            this.lbPage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPage.ForeColor = System.Drawing.Color.Gray;
            this.lbPage.Location = new System.Drawing.Point(12, 3);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(18, 19);
            this.lbPage.TabIndex = 89;
            this.lbPage.Text = "1";
            this.lbPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPage.TextChanged += new System.EventHandler(this.lbPage_TextChanged);
            this.lbPage.Click += new System.EventHandler(this.lbPage_Click);
            // 
            // pnSearch
            // 
            this.pnSearch.Controls.Add(this.btSearch);
            this.pnSearch.Controls.Add(this.tbSearch);
            this.pnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSearch.Location = new System.Drawing.Point(3, 3);
            this.pnSearch.Name = "pnSearch";
            this.pnSearch.Size = new System.Drawing.Size(391, 28);
            this.pnSearch.TabIndex = 0;
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(3, 2);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 23);
            this.btSearch.TabIndex = 1;
            this.btSearch.Text = "Tìm";
            this.btSearch.UseVisualStyleBackColor = true;
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(84, 5);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(304, 23);
            this.tbSearch.TabIndex = 0;
            // 
            // UcPhongTap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlAll);
            this.Name = "UcPhongTap";
            this.Size = new System.Drawing.Size(800, 550);
            this.Load += new System.EventHandler(this.UcPhongTap_Load);
            this.tlAll.ResumeLayout(false);
            this.tlTop.ResumeLayout(false);
            this.tlTop.PerformLayout();
            this.pnActions.ResumeLayout(false);
            this.tbBottom.ResumeLayout(false);
            this.pnPage.ResumeLayout(false);
            this.pnPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnSearch.ResumeLayout(false);
            this.pnSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlAll;
        private System.Windows.Forms.TableLayoutPanel tlTop;
        private System.Windows.Forms.Panel pnActions;
        private System.Windows.Forms.Button btSua;
        private System.Windows.Forms.Button btXoa;
        private System.Windows.Forms.Button btThem;
        private System.Windows.Forms.TableLayoutPanel tbBottom;
        private System.Windows.Forms.Panel pnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnPage;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.TextBox tbPage;
        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbPage;
        private System.Windows.Forms.ListView lvThongTin;
        private System.Windows.Forms.ColumnHeader chCheckBox;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chSTT;
        private System.Windows.Forms.ColumnHeader chMa;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chNgayHetHan;
        private System.Windows.Forms.ColumnHeader chSoXe;
        private System.Windows.Forms.ColumnHeader chDTDD;
        private System.Windows.Forms.ColumnHeader chGhiChu;
        private System.Windows.Forms.Button btNextPage;
        private System.Windows.Forms.Button btBackPage;
    }
}
