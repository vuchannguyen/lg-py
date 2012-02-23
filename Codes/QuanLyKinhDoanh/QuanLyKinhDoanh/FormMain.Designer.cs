namespace QuanLyKinhDoanh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnAbout = new System.Windows.Forms.Panel();
            this.lbAbout = new System.Windows.Forms.Label();
            this.pnTopBar = new System.Windows.Forms.Panel();
            this.pbMinimize = new System.Windows.Forms.PictureBox();
            this.pnHello = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pnHeaderAndMainMenu = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pbTimKiem = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pbThuChi = new System.Windows.Forms.PictureBox();
            this.pbHeader = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pbHorizonline = new System.Windows.Forms.PictureBox();
            this.pbMuaHang = new System.Windows.Forms.PictureBox();
            this.pbQuanLyUser = new System.Windows.Forms.PictureBox();
            this.pbBanHang = new System.Windows.Forms.PictureBox();
            this.pnMain.SuspendLayout();
            this.pnAbout.SuspendLayout();
            this.pnTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimize)).BeginInit();
            this.pnHello.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.pnHeaderAndMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTimKiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThuChi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHorizonline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMuaHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQuanLyUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanHang)).BeginInit();
            this.SuspendLayout();
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.pnAbout);
            this.pnMain.Controls.Add(this.pnTopBar);
            this.pnMain.Controls.Add(this.pnHeaderAndMainMenu);
            this.pnMain.Location = new System.Drawing.Point(12, 12);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(811, 498);
            this.pnMain.TabIndex = 11;
            this.pnMain.Visible = false;
            // 
            // pnAbout
            // 
            this.pnAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnAbout.Controls.Add(this.lbAbout);
            this.pnAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnAbout.Location = new System.Drawing.Point(0, 478);
            this.pnAbout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnAbout.Name = "pnAbout";
            this.pnAbout.Size = new System.Drawing.Size(811, 20);
            this.pnAbout.TabIndex = 9;
            // 
            // lbAbout
            // 
            this.lbAbout.AutoSize = true;
            this.lbAbout.BackColor = System.Drawing.Color.Transparent;
            this.lbAbout.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAbout.ForeColor = System.Drawing.Color.Gray;
            this.lbAbout.Location = new System.Drawing.Point(237, 2);
            this.lbAbout.Name = "lbAbout";
            this.lbAbout.Size = new System.Drawing.Size(401, 14);
            this.lbAbout.TabIndex = 8;
            this.lbAbout.Text = "Phần mềm Quản Lý Nhân Sự - STG giữ toàn quyền. | About Software | About STG";
            // 
            // pnTopBar
            // 
            this.pnTopBar.BackColor = System.Drawing.Color.Transparent;
            this.pnTopBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnTopBar.Controls.Add(this.pbMinimize);
            this.pnTopBar.Controls.Add(this.pnHello);
            this.pnTopBar.Controls.Add(this.pbExit);
            this.pnTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnTopBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnTopBar.Name = "pnTopBar";
            this.pnTopBar.Size = new System.Drawing.Size(811, 26);
            this.pnTopBar.TabIndex = 0;
            // 
            // pbMinimize
            // 
            this.pbMinimize.BackColor = System.Drawing.Color.Transparent;
            this.pbMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbMinimize.Location = new System.Drawing.Point(765, 0);
            this.pbMinimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pbMinimize.Name = "pbMinimize";
            this.pbMinimize.Size = new System.Drawing.Size(21, 26);
            this.pbMinimize.TabIndex = 9;
            this.pbMinimize.TabStop = false;
            this.pbMinimize.Click += new System.EventHandler(this.pbMinimize_Click);
            this.pbMinimize.MouseEnter += new System.EventHandler(this.pbMinimize_MouseEnter);
            this.pbMinimize.MouseLeave += new System.EventHandler(this.pbMinimize_MouseLeave);
            // 
            // pnHello
            // 
            this.pnHello.BackColor = System.Drawing.Color.Transparent;
            this.pnHello.Controls.Add(this.label2);
            this.pnHello.Controls.Add(this.label1);
            this.pnHello.Location = new System.Drawing.Point(261, 4);
            this.pnHello.Name = "pnHello";
            this.pnHello.Size = new System.Drawing.Size(230, 18);
            this.pnHello.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(29, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Admin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chào Admin";
            // 
            // pbExit
            // 
            this.pbExit.BackColor = System.Drawing.Color.Transparent;
            this.pbExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbExit.Location = new System.Drawing.Point(786, 0);
            this.pbExit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(25, 26);
            this.pbExit.TabIndex = 6;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseEnter += new System.EventHandler(this.pbExit_MouseEnter);
            this.pbExit.MouseLeave += new System.EventHandler(this.pbExit_MouseLeave);
            // 
            // pnHeaderAndMainMenu
            // 
            this.pnHeaderAndMainMenu.BackColor = System.Drawing.Color.Transparent;
            this.pnHeaderAndMainMenu.Controls.Add(this.label7);
            this.pnHeaderAndMainMenu.Controls.Add(this.pbTimKiem);
            this.pnHeaderAndMainMenu.Controls.Add(this.label6);
            this.pnHeaderAndMainMenu.Controls.Add(this.pbThuChi);
            this.pnHeaderAndMainMenu.Controls.Add(this.pbHeader);
            this.pnHeaderAndMainMenu.Controls.Add(this.label5);
            this.pnHeaderAndMainMenu.Controls.Add(this.label4);
            this.pnHeaderAndMainMenu.Controls.Add(this.label24);
            this.pnHeaderAndMainMenu.Controls.Add(this.pbHorizonline);
            this.pnHeaderAndMainMenu.Controls.Add(this.pbMuaHang);
            this.pnHeaderAndMainMenu.Controls.Add(this.pbQuanLyUser);
            this.pnHeaderAndMainMenu.Controls.Add(this.pbBanHang);
            this.pnHeaderAndMainMenu.Location = new System.Drawing.Point(12, 55);
            this.pnHeaderAndMainMenu.Name = "pnHeaderAndMainMenu";
            this.pnHeaderAndMainMenu.Size = new System.Drawing.Size(794, 118);
            this.pnHeaderAndMainMenu.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(373, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 14);
            this.label7.TabIndex = 22;
            this.label7.Text = "Tìm kiếm";
            // 
            // pbTimKiem
            // 
            this.pbTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbTimKiem.Location = new System.Drawing.Point(364, 3);
            this.pbTimKiem.Name = "pbTimKiem";
            this.pbTimKiem.Size = new System.Drawing.Size(65, 67);
            this.pbTimKiem.TabIndex = 21;
            this.pbTimKiem.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(281, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 20;
            this.label6.Text = "Thu chi";
            // 
            // pbThuChi
            // 
            this.pbThuChi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbThuChi.Location = new System.Drawing.Point(274, 3);
            this.pbThuChi.Name = "pbThuChi";
            this.pbThuChi.Size = new System.Drawing.Size(65, 67);
            this.pbThuChi.TabIndex = 19;
            this.pbThuChi.TabStop = false;
            // 
            // pbHeader
            // 
            this.pbHeader.BackColor = System.Drawing.Color.Transparent;
            this.pbHeader.Location = new System.Drawing.Point(470, 3);
            this.pbHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pbHeader.Name = "pbHeader";
            this.pbHeader.Size = new System.Drawing.Size(318, 69);
            this.pbHeader.TabIndex = 7;
            this.pbHeader.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(190, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.TabIndex = 18;
            this.label5.Text = "Mua hàng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(100, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 14);
            this.label4.TabIndex = 17;
            this.label4.Text = "Bán hàng";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Gray;
            this.label24.Location = new System.Drawing.Point(2, 68);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(70, 14);
            this.label24.TabIndex = 16;
            this.label24.Text = "Quản lý User";
            // 
            // pbHorizonline
            // 
            this.pbHorizonline.BackColor = System.Drawing.Color.Transparent;
            this.pbHorizonline.Location = new System.Drawing.Point(4, 83);
            this.pbHorizonline.Margin = new System.Windows.Forms.Padding(4);
            this.pbHorizonline.Name = "pbHorizonline";
            this.pbHorizonline.Size = new System.Drawing.Size(784, 31);
            this.pbHorizonline.TabIndex = 8;
            this.pbHorizonline.TabStop = false;
            // 
            // pbMuaHang
            // 
            this.pbMuaHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMuaHang.Location = new System.Drawing.Point(184, 3);
            this.pbMuaHang.Name = "pbMuaHang";
            this.pbMuaHang.Size = new System.Drawing.Size(65, 67);
            this.pbMuaHang.TabIndex = 14;
            this.pbMuaHang.TabStop = false;
            // 
            // pbQuanLyUser
            // 
            this.pbQuanLyUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbQuanLyUser.Location = new System.Drawing.Point(4, 3);
            this.pbQuanLyUser.Name = "pbQuanLyUser";
            this.pbQuanLyUser.Size = new System.Drawing.Size(65, 67);
            this.pbQuanLyUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbQuanLyUser.TabIndex = 10;
            this.pbQuanLyUser.TabStop = false;
            this.pbQuanLyUser.Click += new System.EventHandler(this.pbQuanLyUser_Click);
            this.pbQuanLyUser.MouseEnter += new System.EventHandler(this.pbQuanLyUser_MouseEnter);
            this.pbQuanLyUser.MouseLeave += new System.EventHandler(this.pbQuanLyUser_MouseLeave);
            // 
            // pbBanHang
            // 
            this.pbBanHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBanHang.Location = new System.Drawing.Point(94, 3);
            this.pbBanHang.Name = "pbBanHang";
            this.pbBanHang.Size = new System.Drawing.Size(65, 67);
            this.pbBanHang.TabIndex = 12;
            this.pbBanHang.TabStop = false;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.pnMain);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ BÁN HÀNG 1.0";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.pnMain.ResumeLayout(false);
            this.pnAbout.ResumeLayout(false);
            this.pnAbout.PerformLayout();
            this.pnTopBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimize)).EndInit();
            this.pnHello.ResumeLayout(false);
            this.pnHello.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.pnHeaderAndMainMenu.ResumeLayout(false);
            this.pnHeaderAndMainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTimKiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThuChi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHorizonline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMuaHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQuanLyUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnAbout;
        private System.Windows.Forms.Label lbAbout;
        private System.Windows.Forms.Panel pnTopBar;
        private System.Windows.Forms.PictureBox pbMinimize;
        private System.Windows.Forms.Panel pnHello;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.Panel pnHeaderAndMainMenu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbTimKiem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbThuChi;
        private System.Windows.Forms.PictureBox pbHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.PictureBox pbHorizonline;
        private System.Windows.Forms.PictureBox pbMuaHang;
        private System.Windows.Forms.PictureBox pbQuanLyUser;
        private System.Windows.Forms.PictureBox pbBanHang;
    }
}

