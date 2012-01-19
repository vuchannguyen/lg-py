namespace PlayStationKD
{
    partial class TimesUp
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
            this.lbMay = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.lbTimesUp = new System.Windows.Forms.Label();
            this.timerRing = new System.Windows.Forms.Timer(this.components);
            this.pnTimesUp = new System.Windows.Forms.Panel();
            this.lbInfo = new System.Windows.Forms.Label();
            this.lbTimesIn = new System.Windows.Forms.Label();
            this.pnTimesUp.SuspendLayout();
            this.SuspendLayout();
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
            // btOk
            // 
            this.btOk.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.btOk.ForeColor = System.Drawing.Color.Red;
            this.btOk.Location = new System.Drawing.Point(58, 126);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(72, 30);
            this.btOk.TabIndex = 1;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // lbTimesUp
            // 
            this.lbTimesUp.AutoSize = true;
            this.lbTimesUp.Font = new System.Drawing.Font("Arial", 12F);
            this.lbTimesUp.ForeColor = System.Drawing.Color.Red;
            this.lbTimesUp.Location = new System.Drawing.Point(25, 95);
            this.lbTimesUp.Name = "lbTimesUp";
            this.lbTimesUp.Size = new System.Drawing.Size(72, 18);
            this.lbTimesUp.TabIndex = 2;
            this.lbTimesUp.Text = "Giờ nghỉ:";
            // 
            // timerRing
            // 
            this.timerRing.Interval = 1000;
            this.timerRing.Tick += new System.EventHandler(this.timerRing_Tick);
            // 
            // pnTimesUp
            // 
            this.pnTimesUp.Controls.Add(this.lbInfo);
            this.pnTimesUp.Controls.Add(this.lbTimesIn);
            this.pnTimesUp.Controls.Add(this.lbTimesUp);
            this.pnTimesUp.Controls.Add(this.btOk);
            this.pnTimesUp.Controls.Add(this.lbMay);
            this.pnTimesUp.Location = new System.Drawing.Point(0, 0);
            this.pnTimesUp.Name = "pnTimesUp";
            this.pnTimesUp.Size = new System.Drawing.Size(190, 170);
            this.pnTimesUp.TabIndex = 3;
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Arial", 12F);
            this.lbInfo.ForeColor = System.Drawing.Color.Red;
            this.lbInfo.Location = new System.Drawing.Point(25, 35);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(36, 18);
            this.lbInfo.TabIndex = 4;
            this.lbInfo.Text = "Info:";
            // 
            // lbTimesIn
            // 
            this.lbTimesIn.AutoSize = true;
            this.lbTimesIn.Font = new System.Drawing.Font("Arial", 12F);
            this.lbTimesIn.ForeColor = System.Drawing.Color.Red;
            this.lbTimesIn.Location = new System.Drawing.Point(25, 65);
            this.lbTimesIn.Name = "lbTimesIn";
            this.lbTimesIn.Size = new System.Drawing.Size(74, 18);
            this.lbTimesIn.TabIndex = 3;
            this.lbTimesIn.Text = "Giờ chơi:";
            // 
            // TimesUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnTimesUp);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TimesUp";
            this.Size = new System.Drawing.Size(190, 170);
            this.Load += new System.EventHandler(this.TimesUp_Load);
            this.pnTimesUp.ResumeLayout(false);
            this.pnTimesUp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbMay;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label lbTimesUp;
        private System.Windows.Forms.Timer timerRing;
        private System.Windows.Forms.Panel pnTimesUp;
        private System.Windows.Forms.Label lbTimesIn;
        private System.Windows.Forms.Label lbInfo;
    }
}
