namespace VNSC
{
    partial class Form_CongCuHoTro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CongCuHoTro));
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.lbExcel = new System.Windows.Forms.Label();
            this.pbExcel = new System.Windows.Forms.PictureBox();
            this.pnCongCuHoTro = new System.Windows.Forms.Panel();
            this.pnTopBar = new System.Windows.Forms.Panel();
            this.pbExit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).BeginInit();
            this.pnCongCuHoTro.SuspendLayout();
            this.pnTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTitle
            // 
            this.pbTitle.Location = new System.Drawing.Point(140, 30);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(210, 56);
            this.pbTitle.TabIndex = 0;
            this.pbTitle.TabStop = false;
            // 
            // lbExcel
            // 
            this.lbExcel.AutoSize = true;
            this.lbExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExcel.ForeColor = System.Drawing.Color.Gray;
            this.lbExcel.Location = new System.Drawing.Point(16, 77);
            this.lbExcel.Name = "lbExcel";
            this.lbExcel.Size = new System.Drawing.Size(44, 15);
            this.lbExcel.TabIndex = 6;
            this.lbExcel.Text = "EXCEL";
            // 
            // pbExcel
            // 
            this.pbExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExcel.Location = new System.Drawing.Point(10, 7);
            this.pbExcel.Name = "pbExcel";
            this.pbExcel.Size = new System.Drawing.Size(55, 57);
            this.pbExcel.TabIndex = 5;
            this.pbExcel.TabStop = false;
            this.pbExcel.Click += new System.EventHandler(this.pbExcel_Click);
            this.pbExcel.MouseEnter += new System.EventHandler(this.pbExcel_MouseEnter);
            this.pbExcel.MouseLeave += new System.EventHandler(this.pbExcel_MouseLeave);
            // 
            // pnCongCuHoTro
            // 
            this.pnCongCuHoTro.Controls.Add(this.lbExcel);
            this.pnCongCuHoTro.Controls.Add(this.pbExcel);
            this.pnCongCuHoTro.Location = new System.Drawing.Point(210, 100);
            this.pnCongCuHoTro.Name = "pnCongCuHoTro";
            this.pnCongCuHoTro.Size = new System.Drawing.Size(75, 100);
            this.pnCongCuHoTro.TabIndex = 7;
            // 
            // pnTopBar
            // 
            this.pnTopBar.BackColor = System.Drawing.Color.Transparent;
            this.pnTopBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnTopBar.Controls.Add(this.pbExit);
            this.pnTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnTopBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnTopBar.Name = "pnTopBar";
            this.pnTopBar.Size = new System.Drawing.Size(500, 26);
            this.pnTopBar.TabIndex = 8;
            // 
            // pbExit
            // 
            this.pbExit.BackColor = System.Drawing.Color.Transparent;
            this.pbExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbExit.Location = new System.Drawing.Point(475, 0);
            this.pbExit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(25, 26);
            this.pbExit.TabIndex = 6;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseEnter += new System.EventHandler(this.pbExit_MouseEnter);
            this.pbExit.MouseLeave += new System.EventHandler(this.pbExit_MouseLeave);
            // 
            // Form_CongCuHoTro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.pnTopBar);
            this.Controls.Add(this.pnCongCuHoTro);
            this.Controls.Add(this.pbTitle);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_CongCuHoTro";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form_CongCuHoTro";
            this.Load += new System.EventHandler(this.Form_CongCuHoTro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).EndInit();
            this.pnCongCuHoTro.ResumeLayout(false);
            this.pnCongCuHoTro.PerformLayout();
            this.pnTopBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Label lbExcel;
        private System.Windows.Forms.PictureBox pbExcel;
        private System.Windows.Forms.Panel pnCongCuHoTro;
        private System.Windows.Forms.Panel pnTopBar;
        private System.Windows.Forms.PictureBox pbExit;
    }
}