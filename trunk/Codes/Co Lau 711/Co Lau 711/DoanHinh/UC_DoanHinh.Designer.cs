namespace Co_Lau_711
{
    partial class UC_DoanHinh
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
            this.pnMain = new System.Windows.Forms.Panel();
            this.gbTime = new System.Windows.Forms.GroupBox();
            this.lbError_Play = new System.Windows.Forms.Label();
            this.pnPics = new System.Windows.Forms.Panel();
            this.lbContent = new System.Windows.Forms.Label();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbShowTime = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPlayingTime = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbNumberOfPics = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_ShowTime = new System.Windows.Forms.Timer(this.components);
            this.lbTroChoi = new System.Windows.Forms.Label();
            this.uC_HelpDoanHinh1 = new Co_Lau_711.UC_HelpDoanHinh();
            this.pnMain.SuspendLayout();
            this.pnPics.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.uC_HelpDoanHinh1);
            this.pnMain.Controls.Add(this.gbTime);
            this.pnMain.Controls.Add(this.lbError_Play);
            this.pnMain.Controls.Add(this.pnPics);
            this.pnMain.Location = new System.Drawing.Point(3, 33);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(666, 422);
            this.pnMain.TabIndex = 10;
            // 
            // gbTime
            // 
            this.gbTime.Location = new System.Drawing.Point(15, 3);
            this.gbTime.Name = "gbTime";
            this.gbTime.Size = new System.Drawing.Size(135, 150);
            this.gbTime.TabIndex = 11;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "Thời gian còn lại";
            // 
            // lbError_Play
            // 
            this.lbError_Play.AutoSize = true;
            this.lbError_Play.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError_Play.ForeColor = System.Drawing.Color.Red;
            this.lbError_Play.Location = new System.Drawing.Point(52, 156);
            this.lbError_Play.Name = "lbError_Play";
            this.lbError_Play.Size = new System.Drawing.Size(27, 15);
            this.lbError_Play.TabIndex = 68;
            this.lbError_Play.Text = " Lỗi";
            // 
            // pnPics
            // 
            this.pnPics.Controls.Add(this.lbContent);
            this.pnPics.Location = new System.Drawing.Point(200, 10);
            this.pnPics.Name = "pnPics";
            this.pnPics.Size = new System.Drawing.Size(480, 480);
            this.pnPics.TabIndex = 2;
            // 
            // lbContent
            // 
            this.lbContent.AutoSize = true;
            this.lbContent.Font = new System.Drawing.Font("Arial", 40F);
            this.lbContent.Location = new System.Drawing.Point(39, 19);
            this.lbContent.Name = "lbContent";
            this.lbContent.Size = new System.Drawing.Size(396, 427);
            this.lbContent.TabIndex = 0;
            this.lbContent.Text = "TROF\r\n\r\n            CHOWI\r\n\r\nDDOANS\r\n\r\n       HINHF";
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.label5);
            this.gbSettings.Controls.Add(this.label4);
            this.gbSettings.Controls.Add(this.cbShowTime);
            this.gbSettings.Controls.Add(this.label3);
            this.gbSettings.Controls.Add(this.cbPlayingTime);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.cbNumberOfPics);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.ForeColor = System.Drawing.Color.Blue;
            this.gbSettings.Location = new System.Drawing.Point(644, 33);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(369, 171);
            this.gbSettings.TabIndex = 9;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(277, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "seconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(277, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "minutes";
            // 
            // cbShowTime
            // 
            this.cbShowTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShowTime.ForeColor = System.Drawing.Color.Black;
            this.cbShowTime.FormattingEnabled = true;
            this.cbShowTime.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbShowTime.Location = new System.Drawing.Point(150, 130);
            this.cbShowTime.Name = "cbShowTime";
            this.cbShowTime.Size = new System.Drawing.Size(121, 24);
            this.cbShowTime.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(57, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Show Time:";
            // 
            // cbPlayingTime
            // 
            this.cbPlayingTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayingTime.ForeColor = System.Drawing.Color.Black;
            this.cbPlayingTime.FormattingEnabled = true;
            this.cbPlayingTime.Items.AddRange(new object[] {
            "3",
            "5",
            "10",
            "15",
            "none"});
            this.cbPlayingTime.Location = new System.Drawing.Point(150, 80);
            this.cbPlayingTime.Name = "cbPlayingTime";
            this.cbPlayingTime.Size = new System.Drawing.Size(121, 24);
            this.cbPlayingTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(45, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Playing Time:";
            // 
            // cbNumberOfPics
            // 
            this.cbNumberOfPics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNumberOfPics.ForeColor = System.Drawing.Color.Black;
            this.cbNumberOfPics.FormattingEnabled = true;
            this.cbNumberOfPics.Items.AddRange(new object[] {
            "4",
            "16",
            "36",
            "64"});
            this.cbNumberOfPics.Location = new System.Drawing.Point(150, 30);
            this.cbNumberOfPics.Name = "cbNumberOfPics";
            this.cbNumberOfPics.Size = new System.Drawing.Size(121, 24);
            this.cbNumberOfPics.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(31, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Pics:";
            // 
            // timer_ShowTime
            // 
            this.timer_ShowTime.Interval = 1000;
            this.timer_ShowTime.Tick += new System.EventHandler(this.timer_ShowTime_Tick);
            // 
            // lbTroChoi
            // 
            this.lbTroChoi.AutoSize = true;
            this.lbTroChoi.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTroChoi.Location = new System.Drawing.Point(300, 3);
            this.lbTroChoi.Name = "lbTroChoi";
            this.lbTroChoi.Size = new System.Drawing.Size(117, 27);
            this.lbTroChoi.TabIndex = 18;
            this.lbTroChoi.Text = "TINH MẮT";
            // 
            // uC_HelpDoanHinh1
            // 
            this.uC_HelpDoanHinh1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_HelpDoanHinh1.Location = new System.Drawing.Point(4, 198);
            this.uC_HelpDoanHinh1.Margin = new System.Windows.Forms.Padding(4);
            this.uC_HelpDoanHinh1.Name = "uC_HelpDoanHinh1";
            this.uC_HelpDoanHinh1.Size = new System.Drawing.Size(155, 190);
            this.uC_HelpDoanHinh1.TabIndex = 69;
            // 
            // UC_DoanHinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTroChoi);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.gbSettings);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_DoanHinh";
            this.Size = new System.Drawing.Size(710, 540);
            this.Load += new System.EventHandler(this.UC_DoanHinh_Load);
            this.pnMain.ResumeLayout(false);
            this.pnMain.PerformLayout();
            this.pnPics.ResumeLayout(false);
            this.pnPics.PerformLayout();
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.GroupBox gbTime;
        private System.Windows.Forms.Label lbError_Play;
        private System.Windows.Forms.Panel pnPics;
        private System.Windows.Forms.Label lbContent;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbShowTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPlayingTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbNumberOfPics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer_ShowTime;
        private System.Windows.Forms.Label lbTroChoi;
        private UC_HelpDoanHinh uC_HelpDoanHinh1;
    }
}
