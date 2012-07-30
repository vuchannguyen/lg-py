namespace QuanLyKinhDoanh
{
    partial class UcDB
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
            this.pnTitle = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.pnSelect = new System.Windows.Forms.Panel();
            this.lbRestore = new System.Windows.Forms.Label();
            this.pbRestore = new System.Windows.Forms.PictureBox();
            this.lbBackup = new System.Windows.Forms.Label();
            this.pbBackup = new System.Windows.Forms.PictureBox();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRestore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackup)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.lbTitle);
            this.pnTitle.Controls.Add(this.pbTitle);
            this.pnTitle.Location = new System.Drawing.Point(591, 6);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(270, 38);
            this.pnTitle.TabIndex = 50;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Gray;
            this.lbTitle.Location = new System.Drawing.Point(52, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(88, 22);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "DỮ LIỆU";
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
            this.pnSelect.Controls.Add(this.lbRestore);
            this.pnSelect.Controls.Add(this.pbRestore);
            this.pnSelect.Controls.Add(this.lbBackup);
            this.pnSelect.Controls.Add(this.pbBackup);
            this.pnSelect.Location = new System.Drawing.Point(143, 228);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(710, 140);
            this.pnSelect.TabIndex = 51;
            // 
            // lbRestore
            // 
            this.lbRestore.AutoSize = true;
            this.lbRestore.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRestore.ForeColor = System.Drawing.Color.Gray;
            this.lbRestore.Location = new System.Drawing.Point(583, 120);
            this.lbRestore.Name = "lbRestore";
            this.lbRestore.Size = new System.Drawing.Size(76, 16);
            this.lbRestore.TabIndex = 8;
            this.lbRestore.Text = "PHỤC HỒI";
            // 
            // pbRestore
            // 
            this.pbRestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRestore.Location = new System.Drawing.Point(570, 10);
            this.pbRestore.Name = "pbRestore";
            this.pbRestore.Size = new System.Drawing.Size(100, 100);
            this.pbRestore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRestore.TabIndex = 7;
            this.pbRestore.TabStop = false;
            this.pbRestore.Click += new System.EventHandler(this.pbRestore_Click);
            this.pbRestore.MouseEnter += new System.EventHandler(this.pbRestore_MouseEnter);
            this.pbRestore.MouseLeave += new System.EventHandler(this.pbRestore_MouseLeave);
            // 
            // lbBackup
            // 
            this.lbBackup.AutoSize = true;
            this.lbBackup.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBackup.ForeColor = System.Drawing.Color.Gray;
            this.lbBackup.Location = new System.Drawing.Point(54, 120);
            this.lbBackup.Name = "lbBackup";
            this.lbBackup.Size = new System.Drawing.Size(71, 16);
            this.lbBackup.TabIndex = 4;
            this.lbBackup.Text = "SAO LƯU";
            // 
            // pbBackup
            // 
            this.pbBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBackup.Location = new System.Drawing.Point(40, 10);
            this.pbBackup.Name = "pbBackup";
            this.pbBackup.Size = new System.Drawing.Size(100, 100);
            this.pbBackup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBackup.TabIndex = 0;
            this.pbBackup.TabStop = false;
            this.pbBackup.Click += new System.EventHandler(this.pbBackup_Click);
            this.pbBackup.MouseEnter += new System.EventHandler(this.pbBackup_MouseEnter);
            this.pbBackup.MouseLeave += new System.EventHandler(this.pbBackup_MouseLeave);
            // 
            // UcDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Controls.Add(this.pnTitle);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcDB";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcDB_Load);
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRestore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbBackup;
        private System.Windows.Forms.PictureBox pbBackup;
        private System.Windows.Forms.Label lbRestore;
        private System.Windows.Forms.PictureBox pbRestore;
    }
}
