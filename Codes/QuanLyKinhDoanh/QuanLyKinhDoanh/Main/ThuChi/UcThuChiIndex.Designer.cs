namespace QuanLyKinhDoanh
{
    partial class UcThuChiIndex
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
            this.pnSelect = new System.Windows.Forms.Panel();
            this.lbChi = new System.Windows.Forms.Label();
            this.lbThu = new System.Windows.Forms.Label();
            this.pbThu = new System.Windows.Forms.PictureBox();
            this.pbChi = new System.Windows.Forms.PictureBox();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChi)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.lbChi);
            this.pnSelect.Controls.Add(this.lbThu);
            this.pnSelect.Controls.Add(this.pbThu);
            this.pnSelect.Controls.Add(this.pbChi);
            this.pnSelect.Location = new System.Drawing.Point(220, 230);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(560, 140);
            this.pnSelect.TabIndex = 6;
            // 
            // lbChi
            // 
            this.lbChi.AutoSize = true;
            this.lbChi.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChi.ForeColor = System.Drawing.Color.Gray;
            this.lbChi.Location = new System.Drawing.Point(473, 120);
            this.lbChi.Name = "lbChi";
            this.lbChi.Size = new System.Drawing.Size(32, 16);
            this.lbChi.TabIndex = 6;
            this.lbChi.Text = "CHI";
            // 
            // lbThu
            // 
            this.lbThu.AutoSize = true;
            this.lbThu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbThu.ForeColor = System.Drawing.Color.Gray;
            this.lbThu.Location = new System.Drawing.Point(46, 120);
            this.lbThu.Name = "lbThu";
            this.lbThu.Size = new System.Drawing.Size(36, 16);
            this.lbThu.TabIndex = 4;
            this.lbThu.Text = "THU";
            // 
            // pbThu
            // 
            this.pbThu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbThu.Location = new System.Drawing.Point(17, 10);
            this.pbThu.Name = "pbThu";
            this.pbThu.Size = new System.Drawing.Size(100, 100);
            this.pbThu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThu.TabIndex = 0;
            this.pbThu.TabStop = false;
            this.pbThu.Click += new System.EventHandler(this.pbThu_Click);
            this.pbThu.MouseEnter += new System.EventHandler(this.pbThu_MouseEnter);
            this.pbThu.MouseLeave += new System.EventHandler(this.pbThu_MouseLeave);
            // 
            // pbChi
            // 
            this.pbChi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbChi.Location = new System.Drawing.Point(439, 10);
            this.pbChi.Name = "pbChi";
            this.pbChi.Size = new System.Drawing.Size(100, 100);
            this.pbChi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbChi.TabIndex = 2;
            this.pbChi.TabStop = false;
            this.pbChi.Click += new System.EventHandler(this.pbChi_Click);
            this.pbChi.MouseEnter += new System.EventHandler(this.pbChi_MouseEnter);
            this.pbChi.MouseLeave += new System.EventHandler(this.pbChi_MouseLeave);
            // 
            // UcThuChiIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcThuChiIndex";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcThuChiIndex_Load);
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbChi;
        private System.Windows.Forms.Label lbThu;
        private System.Windows.Forms.PictureBox pbThu;
        private System.Windows.Forms.PictureBox pbChi;

    }
}
