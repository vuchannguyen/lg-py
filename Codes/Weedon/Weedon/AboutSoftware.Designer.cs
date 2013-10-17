namespace Weedon
{
    partial class AboutSoftware
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutSoftware));
            this.btOK = new System.Windows.Forms.Button();
            this.gbAbout = new System.Windows.Forms.GroupBox();
            this.lbARR = new System.Windows.Forms.Label();
            this.lbProductName = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.pbHeader = new System.Windows.Forms.PictureBox();
            this.gbAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(85, 260);
            this.btOK.Margin = new System.Windows.Forms.Padding(4);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(118, 33);
            this.btOK.TabIndex = 10;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // gbAbout
            // 
            this.gbAbout.Controls.Add(this.lbARR);
            this.gbAbout.Controls.Add(this.lbProductName);
            this.gbAbout.Controls.Add(this.lbVersion);
            this.gbAbout.Controls.Add(this.lbCopyright);
            this.gbAbout.Location = new System.Drawing.Point(15, 100);
            this.gbAbout.Margin = new System.Windows.Forms.Padding(4);
            this.gbAbout.Name = "gbAbout";
            this.gbAbout.Padding = new System.Windows.Forms.Padding(4);
            this.gbAbout.Size = new System.Drawing.Size(260, 150);
            this.gbAbout.TabIndex = 9;
            this.gbAbout.TabStop = false;
            this.gbAbout.Text = "Thông tin phần mềm";
            // 
            // lbARR
            // 
            this.lbARR.AutoSize = true;
            this.lbARR.Location = new System.Drawing.Point(9, 122);
            this.lbARR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbARR.Name = "lbARR";
            this.lbARR.Size = new System.Drawing.Size(122, 16);
            this.lbARR.TabIndex = 4;
            this.lbARR.Text = "All rights reserved";
            // 
            // lbProductName
            // 
            this.lbProductName.AutoSize = true;
            this.lbProductName.Location = new System.Drawing.Point(9, 26);
            this.lbProductName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbProductName.Name = "lbProductName";
            this.lbProductName.Size = new System.Drawing.Size(213, 16);
            this.lbProductName.TabIndex = 0;
            this.lbProductName.Text = "Tên phần mềm:Quản lý Weedon";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(9, 58);
            this.lbVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(83, 16);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "Version: 1.0";
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Location = new System.Drawing.Point(9, 90);
            this.lbCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(162, 16);
            this.lbCopyright.TabIndex = 3;
            this.lbCopyright.Text = "Copyright © 2012 by CD";
            // 
            // pbHeader
            // 
            this.pbHeader.BackColor = System.Drawing.Color.Transparent;
            this.pbHeader.Location = new System.Drawing.Point(50, 15);
            this.pbHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pbHeader.Name = "pbHeader";
            this.pbHeader.Size = new System.Drawing.Size(194, 80);
            this.pbHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHeader.TabIndex = 11;
            this.pbHeader.TabStop = false;
            // 
            // AboutSoftware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 304);
            this.Controls.Add(this.pbHeader);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gbAbout);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AboutSoftware";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Software";
            this.Load += new System.EventHandler(this.AboutSoftware_Load);
            this.gbAbout.ResumeLayout(false);
            this.gbAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.GroupBox gbAbout;
        private System.Windows.Forms.Label lbARR;
        private System.Windows.Forms.Label lbProductName;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.PictureBox pbHeader;
    }
}