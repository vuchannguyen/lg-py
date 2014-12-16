namespace QuanLyPhongTap
{
    partial class FormMain
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTimKiem = new System.Windows.Forms.TabPage();
            this.tabThongKe = new System.Windows.Forms.TabPage();
            this.tabNhaTro = new System.Windows.Forms.TabPage();
            this.tabPhongTap = new System.Windows.Forms.TabPage();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.menuStripMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(984, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "&Menu";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // tabTimKiem
            // 
            this.tabTimKiem.Location = new System.Drawing.Point(4, 25);
            this.tabTimKiem.Name = "tabTimKiem";
            this.tabTimKiem.Padding = new System.Windows.Forms.Padding(3);
            this.tabTimKiem.Size = new System.Drawing.Size(976, 608);
            this.tabTimKiem.TabIndex = 3;
            this.tabTimKiem.Text = "Tìm kiếm";
            this.tabTimKiem.UseVisualStyleBackColor = true;
            // 
            // tabThongKe
            // 
            this.tabThongKe.Location = new System.Drawing.Point(4, 25);
            this.tabThongKe.Name = "tabThongKe";
            this.tabThongKe.Padding = new System.Windows.Forms.Padding(3);
            this.tabThongKe.Size = new System.Drawing.Size(976, 608);
            this.tabThongKe.TabIndex = 2;
            this.tabThongKe.Text = "Thống kê";
            this.tabThongKe.UseVisualStyleBackColor = true;
            // 
            // tabNhaTro
            // 
            this.tabNhaTro.Location = new System.Drawing.Point(4, 25);
            this.tabNhaTro.Name = "tabNhaTro";
            this.tabNhaTro.Padding = new System.Windows.Forms.Padding(3);
            this.tabNhaTro.Size = new System.Drawing.Size(976, 608);
            this.tabNhaTro.TabIndex = 1;
            this.tabNhaTro.Text = "Nhà trọ";
            this.tabNhaTro.UseVisualStyleBackColor = true;
            // 
            // tabPhongTap
            // 
            this.tabPhongTap.Location = new System.Drawing.Point(4, 25);
            this.tabPhongTap.Name = "tabPhongTap";
            this.tabPhongTap.Padding = new System.Windows.Forms.Padding(3);
            this.tabPhongTap.Size = new System.Drawing.Size(976, 608);
            this.tabPhongTap.TabIndex = 0;
            this.tabPhongTap.Text = "Phòng tập";
            this.tabPhongTap.UseVisualStyleBackColor = true;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPhongTap);
            this.tabMain.Controls.Add(this.tabNhaTro);
            this.tabMain.Controls.Add(this.tabThongKe);
            this.tabMain.Controls.Add(this.tabTimKiem);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 24);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(984, 637);
            this.tabMain.TabIndex = 1;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLB LAGI";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabPage tabTimKiem;
        private System.Windows.Forms.TabPage tabThongKe;
        private System.Windows.Forms.TabPage tabNhaTro;
        private System.Windows.Forms.TabPage tabPhongTap;
        private System.Windows.Forms.TabControl tabMain;
    }
}