namespace Weedon
{
    partial class UcToolIndex
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
            this.lbSanPham = new System.Windows.Forms.Label();
            this.pbDB = new System.Windows.Forms.PictureBox();
            this.pnSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDB)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSelect
            // 
            this.pnSelect.Controls.Add(this.lbSanPham);
            this.pnSelect.Controls.Add(this.pbDB);
            this.pnSelect.Location = new System.Drawing.Point(143, 228);
            this.pnSelect.Name = "pnSelect";
            this.pnSelect.Size = new System.Drawing.Size(710, 140);
            this.pnSelect.TabIndex = 5;
            // 
            // lbSanPham
            // 
            this.lbSanPham.AutoSize = true;
            this.lbSanPham.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSanPham.ForeColor = System.Drawing.Color.Gray;
            this.lbSanPham.Location = new System.Drawing.Point(325, 120);
            this.lbSanPham.Name = "lbSanPham";
            this.lbSanPham.Size = new System.Drawing.Size(65, 16);
            this.lbSanPham.TabIndex = 4;
            this.lbSanPham.Text = "DỮ LIỆU";
            // 
            // pbDB
            // 
            this.pbDB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDB.Location = new System.Drawing.Point(306, 10);
            this.pbDB.Name = "pbDB";
            this.pbDB.Size = new System.Drawing.Size(100, 100);
            this.pbDB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDB.TabIndex = 0;
            this.pbDB.TabStop = false;
            this.pbDB.Click += new System.EventHandler(this.pbDB_Click);
            this.pbDB.MouseEnter += new System.EventHandler(this.pbDB_MouseEnter);
            this.pbDB.MouseLeave += new System.EventHandler(this.pbDB_MouseLeave);
            // 
            // UcToolIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSelect);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcToolIndex";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UcToolIndex_Load);
            this.pnSelect.ResumeLayout(false);
            this.pnSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnSelect;
        private System.Windows.Forms.Label lbSanPham;
        private System.Windows.Forms.PictureBox pbDB;
    }
}
