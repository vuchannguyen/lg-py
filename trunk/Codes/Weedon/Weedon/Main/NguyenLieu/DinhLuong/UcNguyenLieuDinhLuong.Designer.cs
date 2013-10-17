namespace Weedon.DinhLuong
{
    partial class UcNguyenLieuDinhLuong
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
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.tbDonVi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSoLuong = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbGhiChu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTen = new System.Windows.Forms.ComboBox();
            this.tbMa = new System.Windows.Forms.TextBox();
            this.lbMa = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.pnInfo.SuspendLayout();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbDelete
            // 
            this.pbDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDelete.Location = new System.Drawing.Point(8, 30);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(20, 20);
            this.pbDelete.TabIndex = 95;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            // 
            // pnInfo
            // 
            this.pnInfo.Controls.Add(this.gbInfo);
            this.pnInfo.ForeColor = System.Drawing.Color.Black;
            this.pnInfo.Location = new System.Drawing.Point(3, 3);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(490, 160);
            this.pnInfo.TabIndex = 96;
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.tbDonVi);
            this.gbInfo.Controls.Add(this.label4);
            this.gbInfo.Controls.Add(this.tbSoLuong);
            this.gbInfo.Controls.Add(this.pbDelete);
            this.gbInfo.Controls.Add(this.label6);
            this.gbInfo.Controls.Add(this.tbGhiChu);
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Controls.Add(this.cbTen);
            this.gbInfo.Controls.Add(this.tbMa);
            this.gbInfo.Controls.Add(this.lbMa);
            this.gbInfo.Controls.Add(this.label2);
            this.gbInfo.ForeColor = System.Drawing.Color.Orange;
            this.gbInfo.Location = new System.Drawing.Point(7, 3);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(480, 150);
            this.gbInfo.TabIndex = 2;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Nguyên Liệu";
            // 
            // tbDonVi
            // 
            this.tbDonVi.Location = new System.Drawing.Point(272, 70);
            this.tbDonVi.MaxLength = 10;
            this.tbDonVi.Name = "tbDonVi";
            this.tbDonVi.ReadOnly = true;
            this.tbDonVi.Size = new System.Drawing.Size(100, 23);
            this.tbDonVi.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(213, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 122;
            this.label4.Text = "Đơn vị:";
            // 
            // tbSoLuong
            // 
            this.tbSoLuong.Location = new System.Drawing.Point(96, 70);
            this.tbSoLuong.MaxLength = 6;
            this.tbSoLuong.Name = "tbSoLuong";
            this.tbSoLuong.Size = new System.Drawing.Size(60, 23);
            this.tbSoLuong.TabIndex = 4;
            this.tbSoLuong.Leave += new System.EventHandler(this.tbSoLuong_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(4, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 16);
            this.label6.TabIndex = 118;
            this.label6.Text = "*Ước lượng:";
            // 
            // tbGhiChu
            // 
            this.tbGhiChu.Location = new System.Drawing.Point(96, 110);
            this.tbGhiChu.MaxLength = 50;
            this.tbGhiChu.Name = "tbGhiChu";
            this.tbGhiChu.Size = new System.Drawing.Size(371, 23);
            this.tbGhiChu.TabIndex = 10;
            this.tbGhiChu.TextChanged += new System.EventHandler(this.tbGhiChu_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(224, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 100;
            this.label1.Text = "*Tên:";
            // 
            // cbTen
            // 
            this.cbTen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTen.FormattingEnabled = true;
            this.cbTen.Location = new System.Drawing.Point(272, 30);
            this.cbTen.Name = "cbTen";
            this.cbTen.Size = new System.Drawing.Size(195, 24);
            this.cbTen.TabIndex = 2;
            this.cbTen.SelectedIndexChanged += new System.EventHandler(this.cbTen_SelectedIndexChanged);
            // 
            // tbMa
            // 
            this.tbMa.Location = new System.Drawing.Point(96, 30);
            this.tbMa.MaxLength = 15;
            this.tbMa.Name = "tbMa";
            this.tbMa.ReadOnly = true;
            this.tbMa.Size = new System.Drawing.Size(84, 23);
            this.tbMa.TabIndex = 0;
            // 
            // lbMa
            // 
            this.lbMa.AutoSize = true;
            this.lbMa.ForeColor = System.Drawing.Color.Black;
            this.lbMa.Location = new System.Drawing.Point(59, 33);
            this.lbMa.Name = "lbMa";
            this.lbMa.Size = new System.Drawing.Size(31, 16);
            this.lbMa.TabIndex = 90;
            this.lbMa.Text = "Mã:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(29, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Ghi chú:";
            // 
            // UcNguyenLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnInfo);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcNguyenLieu";
            this.Size = new System.Drawing.Size(500, 170);
            this.Load += new System.EventHandler(this.UcNguyenLieuDinhLuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.pnInfo.ResumeLayout(false);
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbGhiChu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTen;
        private System.Windows.Forms.TextBox tbMa;
        private System.Windows.Forms.Label lbMa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDonVi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSoLuong;
    }
}
