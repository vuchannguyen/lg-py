namespace HR_STG_for_Client
{
    partial class Form_DangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_DangNhap));
            this.label2 = new System.Windows.Forms.Label();
            this.tbMatKhau = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTen = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lbError = new System.Windows.Forms.Label();
            this.tbIDS = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBrowse = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.pbHoanTat = new System.Windows.Forms.PictureBox();
            this.pbHuy = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(125, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 103;
            this.label2.Text = "Mật khẩu:";
            // 
            // tbMatKhau
            // 
            this.tbMatKhau.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMatKhau.Location = new System.Drawing.Point(201, 100);
            this.tbMatKhau.MaxLength = 30;
            this.tbMatKhau.Name = "tbMatKhau";
            this.tbMatKhau.Size = new System.Drawing.Size(171, 23);
            this.tbMatKhau.TabIndex = 98;
            this.tbMatKhau.UseSystemPasswordChar = true;
            this.tbMatKhau.TextChanged += new System.EventHandler(this.tbMatKhau_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(158, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 102;
            this.label1.Text = "Tên:";
            // 
            // tbTen
            // 
            this.tbTen.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTen.Location = new System.Drawing.Point(201, 60);
            this.tbTen.MaxLength = 30;
            this.tbTen.Name = "tbTen";
            this.tbTen.Size = new System.Drawing.Size(171, 23);
            this.tbTen.TabIndex = 97;
            this.tbTen.TextChanged += new System.EventHandler(this.tbTen_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(161, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 16);
            this.label10.TabIndex = 101;
            this.label10.Text = "IDS:";
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.BackColor = System.Drawing.Color.Transparent;
            this.lbError.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError.ForeColor = System.Drawing.Color.Red;
            this.lbError.Location = new System.Drawing.Point(185, 206);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(27, 15);
            this.lbError.TabIndex = 100;
            this.lbError.Text = " Lỗi";
            // 
            // tbIDS
            // 
            this.tbIDS.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIDS.Location = new System.Drawing.Point(201, 20);
            this.tbIDS.MaxLength = 6;
            this.tbIDS.Name = "tbIDS";
            this.tbIDS.ReadOnly = true;
            this.tbIDS.Size = new System.Drawing.Size(171, 23);
            this.tbIDS.TabIndex = 96;
            this.tbIDS.TextChanged += new System.EventHandler(this.tbIDS_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(178, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 16);
            this.label3.TabIndex = 104;
            this.label3.Text = "Chọn đường dẫn tập tin *.cfg:";
            // 
            // tbBrowse
            // 
            this.tbBrowse.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBrowse.Location = new System.Drawing.Point(31, 165);
            this.tbBrowse.Name = "tbBrowse";
            this.tbBrowse.ReadOnly = true;
            this.tbBrowse.Size = new System.Drawing.Size(260, 23);
            this.tbBrowse.TabIndex = 105;
            this.tbBrowse.TextChanged += new System.EventHandler(this.tbBrowse_TextChanged);
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(297, 165);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 106;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // pbHoanTat
            // 
            this.pbHoanTat.BackColor = System.Drawing.Color.Transparent;
            this.pbHoanTat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHoanTat.Enabled = false;
            this.pbHoanTat.Location = new System.Drawing.Point(322, 194);
            this.pbHoanTat.Name = "pbHoanTat";
            this.pbHoanTat.Size = new System.Drawing.Size(50, 39);
            this.pbHoanTat.TabIndex = 108;
            this.pbHoanTat.TabStop = false;
            this.pbHoanTat.Click += new System.EventHandler(this.pbHoanTat_Click);
            this.pbHoanTat.MouseEnter += new System.EventHandler(this.pbHoanTat_MouseEnter);
            this.pbHoanTat.MouseLeave += new System.EventHandler(this.pbHoanTat_MouseLeave);
            // 
            // pbHuy
            // 
            this.pbHuy.BackColor = System.Drawing.Color.Transparent;
            this.pbHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHuy.Location = new System.Drawing.Point(31, 194);
            this.pbHuy.Name = "pbHuy";
            this.pbHuy.Size = new System.Drawing.Size(50, 39);
            this.pbHuy.TabIndex = 107;
            this.pbHuy.TabStop = false;
            this.pbHuy.Click += new System.EventHandler(this.pbHuy_Click);
            this.pbHuy.MouseEnter += new System.EventHandler(this.pbHuy_MouseEnter);
            this.pbHuy.MouseLeave += new System.EventHandler(this.pbHuy_MouseLeave);
            // 
            // Form_DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 240);
            this.Controls.Add(this.pbHoanTat);
            this.Controls.Add(this.pbHuy);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.tbBrowse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMatKhau);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTen);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.tbIDS);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_DangNhap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_DangNhap";
            this.Load += new System.EventHandler(this.Form_DangNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMatKhau;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTen;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.TextBox tbIDS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBrowse;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.PictureBox pbHoanTat;
        private System.Windows.Forms.PictureBox pbHuy;
    }
}