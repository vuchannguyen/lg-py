namespace Weedon
{
    partial class FormExportExcel
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnInfo = new System.Windows.Forms.Panel();
            this.pnSelect = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.pbHuy = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.pbHoanTat = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSelect = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.lbTip = new System.Windows.Forms.Label();
            this.pnSelect.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // pnInfo
            // 
            this.pnInfo.ForeColor = System.Drawing.Color.White;
            this.pnInfo.Location = new System.Drawing.Point(67, 97);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(770, 485);
            this.pnInfo.TabIndex = 44;
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.panel1);
            this.pnSelect.Controls.Add(this.panel3);
            this.pnSelect.Location = new System.Drawing.Point(584, 13);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(235, 78);
            this.pnSelect.TabIndex = 43;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.pbHuy);
            this.panel1.Location = new System.Drawing.Point(6, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(70, 70);
            this.panel1.TabIndex = 26;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(19, 53);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(32, 16);
            this.label24.TabIndex = 1;
            this.label24.Text = "Hủy";
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
            // panel3
            // 
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.pbHoanTat);
            this.panel3.Location = new System.Drawing.Point(158, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(70, 70);
            this.panel3.TabIndex = 28;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(19, 53);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(33, 16);
            this.label30.TabIndex = 1;
            this.label30.Text = "Lưu";
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
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.lbSelect);
            this.panel4.Controls.Add(this.pbTitle);
            this.panel4.Location = new System.Drawing.Point(57, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(371, 53);
            this.panel4.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(52, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 16);
            this.label1.TabIndex = 42;
            this.label1.Text = "Dữ liệu xuất ra dưới dạng MSExcel (.xlsx)";
            // 
            // lbSelect
            // 
            this.lbSelect.AutoSize = true;
            this.lbSelect.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelect.ForeColor = System.Drawing.Color.Orange;
            this.lbSelect.Location = new System.Drawing.Point(50, 8);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.Size = new System.Drawing.Size(145, 22);
            this.lbSelect.TabIndex = 1;
            this.lbSelect.Text = "XUẤT DỮ LIỆU";
            // 
            // pbTitle
            // 
            this.pbTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbTitle.Location = new System.Drawing.Point(10, 0);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(36, 36);
            this.pbTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTitle.TabIndex = 1;
            this.pbTitle.TabStop = false;
            // 
            // lbTip
            // 
            this.lbTip.AutoSize = true;
            this.lbTip.ForeColor = System.Drawing.Color.Red;
            this.lbTip.Location = new System.Drawing.Point(64, 75);
            this.lbTip.Name = "lbTip";
            this.lbTip.Size = new System.Drawing.Size(272, 16);
            this.lbTip.TabIndex = 45;
            this.lbTip.Text = "Click chuột phải để hiển thị thêm thông tin";
            this.lbTip.Visible = false;
            // 
            // FormExportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 594);
            this.Controls.Add(this.lbTip);
            this.Controls.Add(this.pnInfo);
            this.Controls.Add(this.pnSelect);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormExportExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EXPORT EXCEL";
            this.Load += new System.EventHandler(this.FormExportExcel_Load);
            this.pnSelect.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.PictureBox pbHuy;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.PictureBox pbHoanTat;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSelect;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Label lbTip;
    }
}