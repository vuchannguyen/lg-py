namespace QuanLyKinhDoanh
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbLogin = new System.Windows.Forms.PictureBox();
            this.lbError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTitle
            // 
            this.pbTitle.Location = new System.Drawing.Point(30, 25);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(100, 100);
            this.pbTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTitle.TabIndex = 0;
            this.pbTitle.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(270, 27);
            this.tbUserName.MaxLength = 20;
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(200, 26);
            this.tbUserName.TabIndex = 2;
            this.tbUserName.TextChanged += new System.EventHandler(this.tbUserName_TextChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(270, 70);
            this.tbPassword.MaxLength = 20;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(200, 26);
            this.tbPassword.TabIndex = 4;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mật khẩu:";
            // 
            // pbLogin
            // 
            this.pbLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLogin.Location = new System.Drawing.Point(400, 95);
            this.pbLogin.Name = "pbLogin";
            this.pbLogin.Size = new System.Drawing.Size(50, 50);
            this.pbLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogin.TabIndex = 5;
            this.pbLogin.TabStop = false;
            this.pbLogin.Click += new System.EventHandler(this.pbLogin_Click);
            this.pbLogin.MouseEnter += new System.EventHandler(this.pbLogin_MouseEnter);
            this.pbLogin.MouseLeave += new System.EventHandler(this.pbLogin_MouseLeave);
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError.ForeColor = System.Drawing.Color.Red;
            this.lbError.Location = new System.Drawing.Point(170, 115);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(178, 16);
            this.lbError.TabIndex = 6;
            this.lbError.Text = "Mật khẩu không chính xác!";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 160);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbTitle);
            this.Controls.Add(this.pbLogin);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DANG NHAP";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbLogin;
        private System.Windows.Forms.Label lbError;
    }
}