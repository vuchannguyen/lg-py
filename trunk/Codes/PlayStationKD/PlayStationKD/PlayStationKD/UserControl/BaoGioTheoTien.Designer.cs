namespace PlayStationKD
{
    partial class BaoGioTheoTien
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
            this.components = new System.ComponentModel.Container();
            this.timerCountDown = new System.Windows.Forms.Timer(this.components);
            this.lbHr = new System.Windows.Forms.Label();
            this.lbMay = new System.Windows.Forms.Label();
            this.lbMin = new System.Windows.Forms.Label();
            this.lbSec = new System.Windows.Forms.Label();
            this.tbTien = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btBatDau = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btDung = new System.Windows.Forms.Button();
            this.lbTimesIn = new System.Windows.Forms.Label();
            this.lbTien = new System.Windows.Forms.Label();
            this.pnBGTT = new System.Windows.Forms.Panel();
            this.cbChuyenMay = new System.Windows.Forms.ComboBox();
            this.pnBGTT.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerCountDown
            // 
            this.timerCountDown.Interval = 1000;
            this.timerCountDown.Tick += new System.EventHandler(this.timerDienTu_Tick);
            // 
            // lbHr
            // 
            this.lbHr.AutoSize = true;
            this.lbHr.Font = new System.Drawing.Font("Arial", 16F);
            this.lbHr.Location = new System.Drawing.Point(40, 113);
            this.lbHr.Name = "lbHr";
            this.lbHr.Size = new System.Drawing.Size(36, 25);
            this.lbHr.TabIndex = 2;
            this.lbHr.Text = "00";
            // 
            // lbMay
            // 
            this.lbMay.AutoSize = true;
            this.lbMay.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lbMay.Location = new System.Drawing.Point(60, 5);
            this.lbMay.Name = "lbMay";
            this.lbMay.Size = new System.Drawing.Size(52, 26);
            this.lbMay.TabIndex = 0;
            this.lbMay.Text = "Máy";
            // 
            // lbMin
            // 
            this.lbMin.AutoSize = true;
            this.lbMin.Font = new System.Drawing.Font("Arial", 16F);
            this.lbMin.Location = new System.Drawing.Point(76, 113);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(36, 25);
            this.lbMin.TabIndex = 4;
            this.lbMin.Text = "00";
            // 
            // lbSec
            // 
            this.lbSec.AutoSize = true;
            this.lbSec.Font = new System.Drawing.Font("Arial", 16F);
            this.lbSec.Location = new System.Drawing.Point(115, 113);
            this.lbSec.Name = "lbSec";
            this.lbSec.Size = new System.Drawing.Size(36, 25);
            this.lbSec.TabIndex = 5;
            this.lbSec.Text = "00";
            // 
            // tbTien
            // 
            this.tbTien.Font = new System.Drawing.Font("Arial", 12F);
            this.tbTien.Location = new System.Drawing.Point(60, 32);
            this.tbTien.MaxLength = 6;
            this.tbTien.Name = "tbTien";
            this.tbTien.Size = new System.Drawing.Size(120, 26);
            this.tbTien.TabIndex = 1;
            this.tbTien.TextChanged += new System.EventHandler(this.tbTien_TextChanged);
            this.tbTien.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTien_KeyDown);
            this.tbTien.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbTien_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = ":";
            // 
            // btBatDau
            // 
            this.btBatDau.Enabled = false;
            this.btBatDau.Font = new System.Drawing.Font("Arial", 12F);
            this.btBatDau.Location = new System.Drawing.Point(60, 82);
            this.btBatDau.Name = "btBatDau";
            this.btBatDau.Size = new System.Drawing.Size(70, 28);
            this.btBatDau.TabIndex = 3;
            this.btBatDau.Text = "Bắt đầu";
            this.btBatDau.UseVisualStyleBackColor = true;
            this.btBatDau.Click += new System.EventHandler(this.btBatDau_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = ":";
            // 
            // btDung
            // 
            this.btDung.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btDung.ForeColor = System.Drawing.Color.Red;
            this.btDung.Location = new System.Drawing.Point(60, 82);
            this.btDung.Name = "btDung";
            this.btDung.Size = new System.Drawing.Size(70, 28);
            this.btDung.TabIndex = 8;
            this.btDung.Text = "Dừng";
            this.btDung.UseVisualStyleBackColor = true;
            this.btDung.Visible = false;
            this.btDung.Click += new System.EventHandler(this.btDung_Click);
            // 
            // lbTimesIn
            // 
            this.lbTimesIn.AutoSize = true;
            this.lbTimesIn.Font = new System.Drawing.Font("Arial", 12F);
            this.lbTimesIn.Location = new System.Drawing.Point(10, 62);
            this.lbTimesIn.Name = "lbTimesIn";
            this.lbTimesIn.Size = new System.Drawing.Size(74, 18);
            this.lbTimesIn.TabIndex = 9;
            this.lbTimesIn.Text = "Giờ chơi:";
            // 
            // lbTien
            // 
            this.lbTien.AutoSize = true;
            this.lbTien.Font = new System.Drawing.Font("Arial", 12F);
            this.lbTien.Location = new System.Drawing.Point(10, 35);
            this.lbTien.Name = "lbTien";
            this.lbTien.Size = new System.Drawing.Size(42, 18);
            this.lbTien.TabIndex = 10;
            this.lbTien.Text = "Tiền:";
            // 
            // pnBGTT
            // 
            this.pnBGTT.Controls.Add(this.cbChuyenMay);
            this.pnBGTT.Controls.Add(this.lbTien);
            this.pnBGTT.Controls.Add(this.lbTimesIn);
            this.pnBGTT.Controls.Add(this.btDung);
            this.pnBGTT.Controls.Add(this.label2);
            this.pnBGTT.Controls.Add(this.btBatDau);
            this.pnBGTT.Controls.Add(this.label1);
            this.pnBGTT.Controls.Add(this.tbTien);
            this.pnBGTT.Controls.Add(this.lbSec);
            this.pnBGTT.Controls.Add(this.lbMin);
            this.pnBGTT.Controls.Add(this.lbMay);
            this.pnBGTT.Controls.Add(this.lbHr);
            this.pnBGTT.Location = new System.Drawing.Point(0, 0);
            this.pnBGTT.Name = "pnBGTT";
            this.pnBGTT.Size = new System.Drawing.Size(190, 170);
            this.pnBGTT.TabIndex = 9;
            // 
            // cbChuyenMay
            // 
            this.cbChuyenMay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChuyenMay.Font = new System.Drawing.Font("Arial", 12F);
            this.cbChuyenMay.FormattingEnabled = true;
            this.cbChuyenMay.Items.AddRange(new object[] {
            "Chuyển Máy"});
            this.cbChuyenMay.Location = new System.Drawing.Point(40, 140);
            this.cbChuyenMay.Name = "cbChuyenMay";
            this.cbChuyenMay.Size = new System.Drawing.Size(110, 26);
            this.cbChuyenMay.TabIndex = 32;
            this.cbChuyenMay.Visible = false;
            this.cbChuyenMay.DropDown += new System.EventHandler(this.cbChuyenMay_DropDown);
            this.cbChuyenMay.DropDownClosed += new System.EventHandler(this.cbChuyenMay_DropDownClosed);
            // 
            // BaoGioTheoTien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnBGTT);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BaoGioTheoTien";
            this.Size = new System.Drawing.Size(190, 170);
            this.Load += new System.EventHandler(this.BaoGioTheoTien_Load);
            this.EnabledChanged += new System.EventHandler(this.BaoGioTheoTien_EnabledChanged);
            this.pnBGTT.ResumeLayout(false);
            this.pnBGTT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerCountDown;
        private System.Windows.Forms.Label lbHr;
        private System.Windows.Forms.Label lbMay;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.Label lbSec;
        private System.Windows.Forms.TextBox tbTien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btBatDau;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btDung;
        private System.Windows.Forms.Label lbTimesIn;
        private System.Windows.Forms.Label lbTien;
        private System.Windows.Forms.Panel pnBGTT;
        private System.Windows.Forms.ComboBox cbChuyenMay;
    }
}
