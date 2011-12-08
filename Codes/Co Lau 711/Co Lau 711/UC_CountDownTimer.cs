using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Function
{
    public partial class UC_CountDownTimer : UserControl
    {
        private bool bTimeCounting;

        public bool BTimeCounting
        {
            get { return bTimeCounting; }
            set { bTimeCounting = value; }
        }

        int hours, minutes, seconds;

        public UC_CountDownTimer()
        {
            InitializeComponent();
        }

        public UC_CountDownTimer(int iHours, int iMinutes, int iSeconds)
        {
            InitializeComponent();

            hours = iHours;
            minutes = iMinutes;
            seconds = iSeconds;
        }

        private void UC_CountDownTimer_Load(object sender, EventArgs e)
        {
            InitUC();

            timerCountDown.Enabled = true;
        }

        private void InitUC()
        {
            bTimeCounting = true;

            //hours = 0;
            //minutes = 5;
            //seconds = 0;

            setCountDownTime();
        }

        private void setCountDownTime()
        {
            if (hours.ToString().Length == 1)
            {
                lbHr.Text = "0" + hours.ToString();
            }
            else
            {
                lbHr.Text = hours.ToString();
            }

            if (minutes.ToString().Length == 1)
            {
                lbMin.Text = "0" + minutes.ToString();
            }
            else
            {
                lbMin.Text = minutes.ToString();
            }

            if (seconds.ToString().Length == 1)
            {
                lbSec.Text = "0" + seconds.ToString();
            }
            else
            {
                lbSec.Text = seconds.ToString();
            }
        }

        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            if (minutes == 0 && seconds <= 30)
            {
                this.ForeColor = Color.Red;
            }

            if ((hours == 0) && (minutes == 0) && (seconds == 0))
            {
                timerCountDown.Enabled = false;

                bTimeCounting = false;
                this.Enabled = false;
            }
            else
            {
                if (seconds < 1)
                {
                    seconds = 59;

                    if (minutes == 0)
                    {
                        minutes = 59;
                        if (hours != 0)
                        {
                            hours--;
                        }
                    }
                    else
                    {
                        minutes--;
                    }
                }
                else
                {
                    seconds--;
                }

                setCountDownTime();
            }
        }

        private void UC_CountDownTimer_EnabledChanged(object sender, EventArgs e)
        {
            if (!this.Enabled)
            {
                timerCountDown.Enabled = false;
            }
        }
    }
}
