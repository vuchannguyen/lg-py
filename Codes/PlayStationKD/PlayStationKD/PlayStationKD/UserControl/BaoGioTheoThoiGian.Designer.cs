namespace PlayStationKD
{
    partial class BaoGioTheoThoiGian
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
            this.lbTimesIn = new System.Windows.Forms.Label();
            this.btDung = new System.Windows.Forms.Button();
            this.lbSec = new System.Windows.Forms.Label();
            this.lbMin = new System.Windows.Forms.Label();
            this.lbMay = new System.Windows.Forms.Label();
            this.lbHr = new System.Windows.Forms.Label();
            this.btBatDau = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.timerCountDown = new System.Windows.Forms.Timer(this.components);
            this.pnBGTTG = new System.Windows.Forms.Panel();
            this.cbChuyenMay = new System.Windows.Forms.ComboBox();
            this.tbSeconds = new System.Windows.Forms.TextBox();
            this.tbMinutes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnBGTTG.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTimesIn
            // 
            this.lbTimesIn.AutoSize = true;
            this.lbTimesIn.Font = new System.Drawing.Font("Arial", 12F);
            this.lbTimesIn.Location = new System.Drawing.Point(10, 35);
            this.lbTimesIn.Name = "lbTimesIn";
            this.lbTimesIn.Size = new System.Drawing.Size(74, 18);
            this.lbTimesIn.TabIndex = 18;
            this.lbTimesIn.Text = "Giờ chơi:";
            // 
            // btDung
            // 
            this.btDung.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btDung.ForeColor = System.Drawing.Color.Red;
            this.btDung.Location = new System.Drawing.Point(60, 60);
            this.btDung.Name = "btDung";
            this.btDung.Size = new System.Drawing.Size(70, 28);
            this.btDung.TabIndex = 17;
            this.btDung.Text = "Dừng";
            this.btDung.UseVisualStyleBackColor = true;
            this.btDung.Visible = false;
            this.btDung.Click += new System.EventHandler(this.btDung_Click);
            // 
            // lbSec
            // 
            this.lbSec.AutoSize = true;
            this.lbSec.Font = new System.Drawing.Font("Arial", 16F);
            this.lbSec.Location = new System.Drawing.Point(114, 113);
            this.lbSec.Name = "lbSec";
            this.lbSec.Size = new System.Drawing.Size(36, 25);
            this.lbSec.TabIndex = 14;
            this.lbSec.Text = "00";
            // 
            // lbMin
            // 
            this.lbMin.AutoSize = true;
            this.lbMin.Font = new System.Drawing.Font("Arial", 16F);
            this.lbMin.Location = new System.Drawing.Point(76, 113);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(36, 25);
            this.lbMin.TabIndex = 13;
            this.lbMin.Text = "00";
            // 
            // lbMay
            // 
            this.lbMay.AutoSize = true;
            this.lbMay.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lbMay.Location = new System.Drawing.Point(60, 5);
            this.lbMay.Name = "lbMay";
            this.lbMay.Size = new System.Drawing.Size(52, 26);
            this.lbMay.TabIndex = 10;
            this.lbMay.Text = "Máy";
            // 
            // lbHr
            // 
            this.lbHr.AutoSize = true;
            this.lbHr.Font = new System.Drawing.Font("Arial", 16F);
            this.lbHr.Location = new System.Drawing.Point(40, 113);
            this.lbHr.Name = "lbHr";
            this.lbHr.Size = new System.Drawing.Size(36, 25);
            this.lbHr.TabIndex = 12;
            this.lbHr.Text = "00";
            // 
            // btBatDau
            // 
            this.btBatDau.Font = new System.Drawing.Font("Arial", 12F);
            this.btBatDau.Location = new System.Drawing.Point(60, 60);
            this.btBatDau.Name = "btBatDau";
            this.btBatDau.Size = new System.Drawing.Size(70, 28);
            this.btBatDau.TabIndex = 19;
            this.btBatDau.Text = "Bắt đầu";
            this.btBatDau.UseVisualStyleBackColor = true;
            this.btBatDau.Click += new System.EventHandler(this.btBatDau_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F);
            this.label3.Location = new System.Drawing.Point(40, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "Nhập thời gian";
            // 
            // tbHours
            // 
            this.tbHours.Font = new System.Drawing.Font("Arial", 12F);
            this.tbHours.Location = new System.Drawing.Point(45, 113);
            this.tbHours.MaxLength = 2;
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(24, 26);
            this.tbHours.TabIndex = 21;
            this.tbHours.Text = "00";
            this.tbHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbHours.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbHours_MouseClick);
            this.tbHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbHours_KeyDown);
            this.tbHours.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbHours_KeyUp);
            // 
            // timerCountDown
            // 
            this.timerCountDown.Interval = 1000;
            this.timerCountDown.Tick += new System.EventHandler(this.timerCountDown_Tick);
            // 
            // pnBGTTG
            // 
            this.pnBGTTG.Controls.Add(this.btDung);
            this.pnBGTTG.Controls.Add(this.cbChuyenMay);
            this.pnBGTTG.Controls.Add(this.tbSeconds);
            this.pnBGTTG.Controls.Add(this.tbMinutes);
            this.pnBGTTG.Controls.Add(this.label2);
            this.pnBGTTG.Controls.Add(this.label1);
            this.pnBGTTG.Controls.Add(this.btBatDau);
            this.pnBGTTG.Controls.Add(this.tbHours);
            this.pnBGTTG.Controls.Add(this.label3);
            this.pnBGTTG.Controls.Add(this.lbTimesIn);
            this.pnBGTTG.Controls.Add(this.lbSec);
            this.pnBGTTG.Controls.Add(this.lbMin);
            this.pnBGTTG.Controls.Add(this.lbMay);
            this.pnBGTTG.Controls.Add(this.lbHr);
            this.pnBGTTG.Location = new System.Drawing.Point(0, 0);
            this.pnBGTTG.Name = "pnBGTTG";
            this.pnBGTTG.Size = new System.Drawing.Size(190, 170);
            this.pnBGTTG.TabIndex = 24;
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
            // tbSeconds
            // 
            this.tbSeconds.Font = new System.Drawing.Font("Arial", 12F);
            this.tbSeconds.Location = new System.Drawing.Point(120, 113);
            this.tbSeconds.MaxLength = 2;
            this.tbSeconds.Name = "tbSeconds";
            this.tbSeconds.Size = new System.Drawing.Size(24, 26);
            this.tbSeconds.TabIndex = 27;
            this.tbSeconds.Text = "00";
            this.tbSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSeconds.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbSeconds_MouseClick);
            this.tbSeconds.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSeconds_KeyDown);
            this.tbSeconds.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSeconds_KeyUp);
            // 
            // tbMinutes
            // 
            this.tbMinutes.Font = new System.Drawing.Font("Arial", 12F);
            this.tbMinutes.Location = new System.Drawing.Point(81, 113);
            this.tbMinutes.MaxLength = 2;
            this.tbMinutes.Name = "tbMinutes";
            this.tbMinutes.Size = new System.Drawing.Size(24, 26);
            this.tbMinutes.TabIndex = 26;
            this.tbMinutes.Text = "00";
            this.tbMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbMinutes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbMinutes_MouseClick);
            this.tbMinutes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMinutes_KeyDown);
            this.tbMinutes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbMinutes_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = ":";
            // 
            // BaoGioTheoThoiGian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnBGTTG);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BaoGioTheoThoiGian";
            this.Size = new System.Drawing.Size(190, 170);
            this.EnabledChanged += new System.EventHandler(this.BaoGioTheoThoiGian_EnabledChanged);
            this.pnBGTTG.ResumeLayout(false);
            this.pnBGTTG.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTimesIn;
        private System.Windows.Forms.Button btDung;
        private System.Windows.Forms.Label lbSec;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.Label lbMay;
        private System.Windows.Forms.Label lbHr;
        private System.Windows.Forms.Button btBatDau;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Timer timerCountDown;
        private System.Windows.Forms.Panel pnBGTTG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMinutes;
        private System.Windows.Forms.TextBox tbSeconds;
        private System.Windows.Forms.ComboBox cbChuyenMay;

    }
}
