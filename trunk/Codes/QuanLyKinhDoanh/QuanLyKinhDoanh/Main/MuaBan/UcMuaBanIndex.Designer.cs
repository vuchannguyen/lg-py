namespace QuanLyKinhDoanh
{
    partial class UcMuaBanIndex
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
            this.lbBan = new System.Windows.Forms.Label();
            this.lbMua = new System.Windows.Forms.Label();
            this.pbMua = new System.Windows.Forms.PictureBox();
            this.pbBan = new System.Windows.Forms.PictureBox();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.lbBan);
            this.pnSelect.Controls.Add(this.lbMua);
            this.pnSelect.Controls.Add(this.pbMua);
            this.pnSelect.Controls.Add(this.pbBan);
            this.pnSelect.Location = new System.Drawing.Point(220, 230);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(560, 140);
            this.pnSelect.TabIndex = 7;
            // 
            // lbBan
            // 
            this.lbBan.AutoSize = true;
            this.lbBan.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBan.ForeColor = System.Drawing.Color.Gray;
            this.lbBan.Location = new System.Drawing.Point(471, 120);
            this.lbBan.Name = "lbBan";
            this.lbBan.Size = new System.Drawing.Size(38, 16);
            this.lbBan.TabIndex = 6;
            this.lbBan.Text = "BÁN";
            // 
            // lbMua
            // 
            this.lbMua.AutoSize = true;
            this.lbMua.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMua.ForeColor = System.Drawing.Color.Gray;
            this.lbMua.Location = new System.Drawing.Point(46, 120);
            this.lbMua.Name = "lbMua";
            this.lbMua.Size = new System.Drawing.Size(40, 16);
            this.lbMua.TabIndex = 4;
            this.lbMua.Text = "MUA";
            // 
            // pbMua
            // 
            this.pbMua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMua.Location = new System.Drawing.Point(17, 10);
            this.pbMua.Name = "pbMua";
            this.pbMua.Size = new System.Drawing.Size(100, 100);
            this.pbMua.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMua.TabIndex = 0;
            this.pbMua.TabStop = false;
            this.pbMua.Click += new System.EventHandler(this.pbMua_Click);
            this.pbMua.MouseEnter += new System.EventHandler(this.pbMua_MouseEnter);
            this.pbMua.MouseLeave += new System.EventHandler(this.pbMua_MouseLeave);
            // 
            // pbBan
            // 
            this.pbBan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBan.Location = new System.Drawing.Point(439, 10);
            this.pbBan.Name = "pbBan";
            this.pbBan.Size = new System.Drawing.Size(100, 100);
            this.pbBan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBan.TabIndex = 2;
            this.pbBan.TabStop = false;
            this.pbBan.Click += new System.EventHandler(this.pbBan_Click);
            this.pbBan.MouseEnter += new System.EventHandler(this.pbBan_MouseEnter);
            this.pbBan.MouseLeave += new System.EventHandler(this.pbBan_MouseLeave);
            // 
            // UcMuaBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcMuaBan";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcMuaBan_Load);
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbBan;
        private System.Windows.Forms.Label lbMua;
        private System.Windows.Forms.PictureBox pbMua;
        private System.Windows.Forms.PictureBox pbBan;
    }
}
