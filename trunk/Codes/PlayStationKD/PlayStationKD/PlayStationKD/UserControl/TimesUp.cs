using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using Function;

namespace PlayStationKD
{
    public partial class TimesUp : UserControl
    {
        private int n = 0;
        private double iTime = 0;
        private string Sound;

        private SoundPlayer RingTone;

        public TimesUp()
        {
            InitializeComponent();
        }

        public TimesUp(String sMaMay, String sInfo, String sTimesIn, String sTimesUp, bool bRing)
        {
            InitializeComponent();
            lbMay.Text = "Máy " + sMaMay;
            lbInfo.Text = sInfo;
            lbTimesIn.Text = "Giờ chơi: " + sTimesIn;
            lbTimesUp.Text = "Giờ nghỉ: " + sTimesUp;

            setSound();

            if (bRing)
            {
                RingTone = new SoundPlayer(Sound);
                RingTone.PlayLooping();

                timerRing.Enabled = true;
            }
        }

        private void setSound()
        {
            string sSound = @"Sound\Sound.sou";

            if (File.Exists(sSound))
            {
                StreamReader sr = new StreamReader(sSound);
                Sound = sr.ReadLine();
                sr.Close();
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            RingTone.Stop();
            this.Dispose();
        }

        private void TimesUp_Load(object sender, EventArgs e)
        {
            //timerRing.Enabled = true;

            //btOk.Focus();
        }

        private void timerRing_Tick(object sender, EventArgs e)
        {
            if (n >= 60 || !this.Enabled)
            {
                RingTone.Stop();
                this.Dispose();
            }
            else
            {
                //if (n < 60 && n % 3 == 0)
                //{
                //    try
                //    {
                //        SoundPlayer RingTone = new SoundPlayer(Sound);
                //        RingTone.PlayLooping();
                //    }
                //    catch
                //    { 
                //        //
                //    }
                //}
            }

            n++;
        }
    }
}