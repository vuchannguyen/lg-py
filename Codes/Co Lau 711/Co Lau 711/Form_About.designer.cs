namespace About
{
    partial class Form_About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_About));
            this.btOK = new System.Windows.Forms.Button();
            this.gbAbout = new System.Windows.Forms.GroupBox();
            this.lbARR = new System.Windows.Forms.Label();
            this.lbProductName = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.gbAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(68, 159);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(92, 30);
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
            this.gbAbout.Location = new System.Drawing.Point(14, 15);
            this.gbAbout.Name = "gbAbout";
            this.gbAbout.Size = new System.Drawing.Size(203, 134);
            this.gbAbout.TabIndex = 9;
            this.gbAbout.TabStop = false;
            this.gbAbout.Text = "Thông tin";
            // 
            // lbARR
            // 
            this.lbARR.AutoSize = true;
            this.lbARR.Location = new System.Drawing.Point(7, 109);
            this.lbARR.Name = "lbARR";
            this.lbARR.Size = new System.Drawing.Size(122, 16);
            this.lbARR.TabIndex = 4;
            this.lbARR.Text = "All rights reserved";
            // 
            // lbProductName
            // 
            this.lbProductName.AutoSize = true;
            this.lbProductName.Location = new System.Drawing.Point(7, 23);
            this.lbProductName.Name = "lbProductName";
            this.lbProductName.Size = new System.Drawing.Size(186, 16);
            this.lbProductName.TabIndex = 0;
            this.lbProductName.Text = "Tên phần mềm: Cờ Lau 711";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(7, 51);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(84, 16);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "Version: 1.0";
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Location = new System.Drawing.Point(7, 80);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(162, 16);
            this.lbCopyright.TabIndex = 3;
            this.lbCopyright.Text = "Copyright © 2011 by CD";
            // 
            // Form_About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 199);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.gbAbout);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_About";
            this.Padding = new System.Windows.Forms.Padding(10, 11, 10, 11);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.Form_About_Load);
            this.gbAbout.ResumeLayout(false);
            this.gbAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.GroupBox gbAbout;
        private System.Windows.Forms.Label lbARR;
        private System.Windows.Forms.Label lbProductName;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbCopyright;

    }
}
