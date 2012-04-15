namespace WindowsApplication2
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.lbProductName = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gbAbout = new System.Windows.Forms.GroupBox();
            this.lbARR = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.picbCD = new System.Windows.Forms.PictureBox();
            this.picbN2 = new System.Windows.Forms.PictureBox();
            this.picbN2Click = new System.Windows.Forms.PictureBox();
            this.picbCDClick = new System.Windows.Forms.PictureBox();
            this.picbLogo = new System.Windows.Forms.PictureBox();
            this.gbAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbCD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbN2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbN2Click)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbCDClick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbProductName
            // 
            this.lbProductName.AutoSize = true;
            this.lbProductName.Location = new System.Drawing.Point(6, 19);
            this.lbProductName.Name = "lbProductName";
            this.lbProductName.Size = new System.Drawing.Size(127, 13);
            this.lbProductName.TabIndex = 0;
            this.lbProductName.Text = "Tên phần mềm: GWL 3.0";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(6, 42);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(63, 13);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "Version: 3.0";
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Location = new System.Drawing.Point(6, 65);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(129, 13);
            this.lbCopyright.TabIndex = 3;
            this.lbCopyright.Text = "Copyright © 2010 by STG";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(198, 116);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(185, 102);
            this.textBox1.TabIndex = 4;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "   Phần mềm hỗ trợ vẽ họa đồ Gilwell, dựa trên quyển Hướng đạo Hạng Nhất (Tác giả" +
                ": Trương Trọng Trác)\r\n\r\n   Mọi ý kiến đóng góp vui lòng gởi email về địa chỉ:\r\n " +
                "         stg.software@gmail.com";
            // 
            // gbAbout
            // 
            this.gbAbout.Controls.Add(this.lbARR);
            this.gbAbout.Controls.Add(this.lbProductName);
            this.gbAbout.Controls.Add(this.lbVersion);
            this.gbAbout.Controls.Add(this.lbCopyright);
            this.gbAbout.Location = new System.Drawing.Point(9, 110);
            this.gbAbout.Name = "gbAbout";
            this.gbAbout.Size = new System.Drawing.Size(174, 109);
            this.gbAbout.TabIndex = 5;
            this.gbAbout.TabStop = false;
            this.gbAbout.Text = "Thông tin phần mềm";
            // 
            // lbARR
            // 
            this.lbARR.AutoSize = true;
            this.lbARR.Location = new System.Drawing.Point(6, 88);
            this.lbARR.Name = "lbARR";
            this.lbARR.Size = new System.Drawing.Size(90, 13);
            this.lbARR.TabIndex = 4;
            this.lbARR.Text = "All rights reserved";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(152, 227);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(79, 24);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // picbCD
            // 
            this.picbCD.Image = global::Gilwell.Properties.Resources.conandog;
            this.picbCD.Location = new System.Drawing.Point(9, 12);
            this.picbCD.Name = "picbCD";
            this.picbCD.Size = new System.Drawing.Size(58, 64);
            this.picbCD.TabIndex = 11;
            this.picbCD.TabStop = false;
            this.picbCD.Visible = false;
            this.picbCD.Click += new System.EventHandler(this.picbCD_Click);
            // 
            // picbN2
            // 
            this.picbN2.Image = global::Gilwell.Properties.Resources.n2;
            this.picbN2.Location = new System.Drawing.Point(328, 12);
            this.picbN2.Name = "picbN2";
            this.picbN2.Size = new System.Drawing.Size(59, 64);
            this.picbN2.TabIndex = 10;
            this.picbN2.TabStop = false;
            this.picbN2.Visible = false;
            this.picbN2.Click += new System.EventHandler(this.picbN2_Click);
            // 
            // picbN2Click
            // 
            this.picbN2Click.Cursor = System.Windows.Forms.Cursors.Default;
            this.picbN2Click.Location = new System.Drawing.Point(240, 234);
            this.picbN2Click.Name = "picbN2Click";
            this.picbN2Click.Size = new System.Drawing.Size(8, 10);
            this.picbN2Click.TabIndex = 9;
            this.picbN2Click.TabStop = false;
            this.picbN2Click.Click += new System.EventHandler(this.picbN2Click_Click);
            // 
            // picbCDClick
            // 
            this.picbCDClick.Location = new System.Drawing.Point(189, 103);
            this.picbCDClick.Name = "picbCDClick";
            this.picbCDClick.Size = new System.Drawing.Size(7, 10);
            this.picbCDClick.TabIndex = 8;
            this.picbCDClick.TabStop = false;
            this.picbCDClick.Click += new System.EventHandler(this.picbCDClick_Click);
            // 
            // picbLogo
            // 
            this.picbLogo.Image = global::Gilwell.Properties.Resources.stgsoft;
            this.picbLogo.Location = new System.Drawing.Point(73, 12);
            this.picbLogo.Name = "picbLogo";
            this.picbLogo.Size = new System.Drawing.Size(250, 85);
            this.picbLogo.TabIndex = 7;
            this.picbLogo.TabStop = false;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 262);
            this.Controls.Add(this.picbCD);
            this.Controls.Add(this.picbN2);
            this.Controls.Add(this.picbN2Click);
            this.Controls.Add(this.picbCDClick);
            this.Controls.Add(this.picbLogo);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gbAbout);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.gbAbout.ResumeLayout(false);
            this.gbAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbCD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbN2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbN2Click)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbCDClick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbProductName;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox gbAbout;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.PictureBox picbLogo;
        private System.Windows.Forms.Label lbARR;
        private System.Windows.Forms.PictureBox picbCDClick;
        private System.Windows.Forms.PictureBox picbN2Click;
        private System.Windows.Forms.PictureBox picbN2;
        private System.Windows.Forms.PictureBox picbCD;

    }
}
