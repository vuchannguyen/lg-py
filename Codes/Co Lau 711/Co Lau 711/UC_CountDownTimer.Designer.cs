namespace Function
{
    partial class UC_CountDownTimer
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSec = new System.Windows.Forms.Label();
            this.lbMin = new System.Windows.Forms.Label();
            this.lbHr = new System.Windows.Forms.Label();
            this.timerCountDown = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(71, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = ":";
            // 
            // lbSec
            // 
            this.lbSec.AutoSize = true;
            this.lbSec.Font = new System.Drawing.Font("Arial", 16F);
            this.lbSec.Location = new System.Drawing.Point(80, 10);
            this.lbSec.Name = "lbSec";
            this.lbSec.Size = new System.Drawing.Size(36, 25);
            this.lbSec.TabIndex = 10;
            this.lbSec.Text = "00";
            // 
            // lbMin
            // 
            this.lbMin.AutoSize = true;
            this.lbMin.Font = new System.Drawing.Font("Arial", 16F);
            this.lbMin.Location = new System.Drawing.Point(41, 10);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(36, 25);
            this.lbMin.TabIndex = 9;
            this.lbMin.Text = "00";
            // 
            // lbHr
            // 
            this.lbHr.AutoSize = true;
            this.lbHr.Font = new System.Drawing.Font("Arial", 16F);
            this.lbHr.Location = new System.Drawing.Point(5, 10);
            this.lbHr.Name = "lbHr";
            this.lbHr.Size = new System.Drawing.Size(36, 25);
            this.lbHr.TabIndex = 8;
            this.lbHr.Text = "00";
            // 
            // timerCountDown
            // 
            this.timerCountDown.Interval = 1000;
            this.timerCountDown.Tick += new System.EventHandler(this.timerCountDown_Tick);
            // 
            // CountDownTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbSec);
            this.Controls.Add(this.lbMin);
            this.Controls.Add(this.lbHr);
            this.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "CountDownTimer";
            this.Size = new System.Drawing.Size(122, 44);
            this.Load += new System.EventHandler(this.UC_CountDownTimer_Load);
            this.EnabledChanged += new System.EventHandler(this.UC_CountDownTimer_EnabledChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSec;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.Label lbHr;
        private System.Windows.Forms.Timer timerCountDown;

    }
}
