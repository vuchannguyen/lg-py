namespace Weedon
{
    partial class FormConnection
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rbSQLServerAu = new System.Windows.Forms.RadioButton();
            this.rbWindowsAu = new System.Windows.Forms.RadioButton();
            this.lbError = new System.Windows.Forms.Label();
            this.pbOk = new System.Windows.Forms.PictureBox();
            this.tbServerName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbOk)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(63, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 105;
            this.label2.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Enabled = false;
            this.tbPassword.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(146, 171);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(207, 23);
            this.tbPassword.TabIndex = 98;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(55, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 104;
            this.label1.Text = "User Name:";
            // 
            // tbUserName
            // 
            this.tbUserName.Enabled = false;
            this.tbUserName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUserName.Location = new System.Drawing.Point(146, 131);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(207, 23);
            this.tbUserName.TabIndex = 97;
            this.tbUserName.TextChanged += new System.EventHandler(this.tbUserName_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(42, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 16);
            this.label10.TabIndex = 103;
            this.label10.Text = "Server Name:";
            // 
            // rbSQLServerAu
            // 
            this.rbSQLServerAu.AutoSize = true;
            this.rbSQLServerAu.BackColor = System.Drawing.Color.Transparent;
            this.rbSQLServerAu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSQLServerAu.Location = new System.Drawing.Point(259, 47);
            this.rbSQLServerAu.Name = "rbSQLServerAu";
            this.rbSQLServerAu.Size = new System.Drawing.Size(48, 19);
            this.rbSQLServerAu.TabIndex = 102;
            this.rbSQLServerAu.Text = "SSA";
            this.rbSQLServerAu.UseVisualStyleBackColor = false;
            this.rbSQLServerAu.CheckedChanged += new System.EventHandler(this.rbSQLServerAu_CheckedChanged);
            // 
            // rbWindowsAu
            // 
            this.rbWindowsAu.AutoSize = true;
            this.rbWindowsAu.BackColor = System.Drawing.Color.Transparent;
            this.rbWindowsAu.Checked = true;
            this.rbWindowsAu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbWindowsAu.Location = new System.Drawing.Point(93, 47);
            this.rbWindowsAu.Name = "rbWindowsAu";
            this.rbWindowsAu.Size = new System.Drawing.Size(43, 19);
            this.rbWindowsAu.TabIndex = 101;
            this.rbWindowsAu.TabStop = true;
            this.rbWindowsAu.Text = "WA";
            this.rbWindowsAu.UseVisualStyleBackColor = false;
            this.rbWindowsAu.CheckedChanged += new System.EventHandler(this.rbWindowsAu_CheckedChanged);
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.BackColor = System.Drawing.Color.Transparent;
            this.lbError.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError.ForeColor = System.Drawing.Color.Red;
            this.lbError.Location = new System.Drawing.Point(179, 70);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(27, 15);
            this.lbError.TabIndex = 100;
            this.lbError.Text = " Lỗi";
            // 
            // pbOk
            // 
            this.pbOk.BackColor = System.Drawing.Color.Transparent;
            this.pbOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbOk.Enabled = false;
            this.pbOk.Location = new System.Drawing.Point(189, 206);
            this.pbOk.Name = "pbOk";
            this.pbOk.Size = new System.Drawing.Size(26, 21);
            this.pbOk.TabIndex = 99;
            this.pbOk.TabStop = false;
            this.pbOk.Click += new System.EventHandler(this.pbOk_Click);
            this.pbOk.MouseEnter += new System.EventHandler(this.pbOk_MouseEnter);
            this.pbOk.MouseLeave += new System.EventHandler(this.pbOk_MouseLeave);
            // 
            // tbServerName
            // 
            this.tbServerName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServerName.Location = new System.Drawing.Point(146, 91);
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.Size = new System.Drawing.Size(207, 23);
            this.tbServerName.TabIndex = 96;
            this.tbServerName.TextChanged += new System.EventHandler(this.tbServerName_TextChanged);
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 274);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rbSQLServerAu);
            this.Controls.Add(this.rbWindowsAu);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.pbOk);
            this.Controls.Add(this.tbServerName);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.FormConnection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbSQLServerAu;
        private System.Windows.Forms.RadioButton rbWindowsAu;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.PictureBox pbOk;
        private System.Windows.Forms.TextBox tbServerName;
    }
}