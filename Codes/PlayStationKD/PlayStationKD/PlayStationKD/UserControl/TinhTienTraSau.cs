using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PlayStationKD
{
    public partial class TinhTienTraSau : UserControl
    {
        int hours, minutes, seconds;
        String sMay, sGioChoi, sGioNghi;
        long lTien;
        bool bEnter = false;



        private bool bMayBan; //May dang duoc su dung ko the chuyen may

        public bool BMayBan
        {
            get { return bMayBan; }
            set { bMayBan = value; }
        }

        private bool bChuyenMay; //Neu la chuyen tu may khac qua thi khong ghi tien vao file

        public bool BChuyenMay
        {
            get { return bChuyenMay; }
            set { bChuyenMay = value; }
        }

        public TinhTienTraSau()
        {
            InitializeComponent();
        }

        public TinhTienTraSau(String sMaMay, Point pLocation)
        {
            InitializeComponent();
            this.Location = pLocation;
            lbMay.Text = "Máy " + sMaMay;
            sMay = sMaMay;
            NewTTTS();
        }

        private void MuteEnterPress(KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bEnter = false;
            }
        }

        private void NextFocus(TextBox tb, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bEnter)
            {
                bEnter = true;
                tb.Focus();
                tb.SelectAll();
            }
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

        private bool TestHours(TextBox tb)
        {
            if (tb.Text.Length == 0)
            {
                tb.Text = "00";
                return true;
            }

            int iTemp = 0;
            if (int.TryParse(tb.Text, out iTemp))
            {
                if (iTemp > 23)
                {
                    MessageBox.Show("Chỉ có thể chơi trong 24 tiếng!\nKhông thể thêm thời gian nữa!");
                    tb.Focus();
                    tb.SelectAll();
                    return false;
                }
            }
            else
            {
                tb.Focus();
                tb.SelectAll();
                return false;
            }

            return true;
        }

        private bool TestMinSec(TextBox tb)
        {
            if (tb.Text.Length == 0)
            {
                tb.Text = "00";
                return true;
            }

            int iTemp = 0;
            if (int.TryParse(tb.Text, out iTemp))
            {
                if (iTemp < 0 || iTemp > 59)
                {
                    tb.Focus();
                    tb.SelectAll();
                    return false;
                }
            }
            else
            {
                tb.Focus();
                tb.SelectAll();
                return false;
            }

            return true;
        }

        private void NewTTTS()
        {
            lTien = 0;

            cbChuyenMay.SelectedIndex = 0;
            tbTien.Text = "0";
            lbHr.Text = "00";
            lbMin.Text = "00";
            lbSec.Text = "00";
            tbHours.Text = "00";
            tbMinutes.Text = "00";
            tbSeconds.Text = "00";

            tbHours.Visible = true;
            tbMinutes.Visible = true;
            tbSeconds.Visible = true;

            btBatDau.Visible = true;
            btDung.Visible = false;
        }

        private void btBatDau_Click(object sender, EventArgs e)
        {
            if (!bChuyenMay)
            {
                if (TestHours(tbHours) && TestMinSec(tbMinutes) && TestMinSec(tbSeconds))
                {
                    hours = int.Parse(tbHours.Text);
                    minutes = int.Parse(tbMinutes.Text);
                    seconds = int.Parse(tbSeconds.Text);
                    setCountDownTime();

                    btBatDau.Visible = false;
                    btDung.Visible = true;

                    TimeSpan ts = new TimeSpan(hours, minutes, seconds);
                    sGioChoi = DateTime.Now.Subtract(ts).ToShortTimeString();
                    lbTimesIn.Text = "Giờ chơi: " + sGioChoi;

                    setTime(hours, minutes);
                    tbHours.Visible = false;
                    tbMinutes.Visible = false;
                    tbSeconds.Visible = false;
                    timerCountDown.Enabled = true;

                    bMayBan = true;
                    cbChuyenMay.Visible = true;
                    TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = true;
                }
            }
            else
            {
                btBatDau.Visible = false;
                btDung.Visible = true;

                setCountDownTime();

                setTime(hours, minutes);
                tbHours.Visible = false;
                tbMinutes.Visible = false;
                tbSeconds.Visible = false;
                timerCountDown.Enabled = true;

                bMayBan = true;
                cbChuyenMay.Visible = true;
                TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = true;
            }
        }

        private void btDung_Click(object sender, EventArgs e)
        {
            timerCountDown.Enabled = false;

            sGioNghi = DateTime.Now.ToShortTimeString();
            String sInfo = "Tiền: " + tbTien.Text;
            this.Controls.Add(new TimesUp(sMay, sInfo, sGioChoi, sGioNghi, false));
            pnTTTS.SendToBack();
            lbTimesIn.Text = "Giờ nghỉ: " + sGioNghi;

            if (int.Parse(tbTien.Text) > 0)
            {
                String sSave = DateTime.Now.ToShortTimeString() + "   " + tbTien.Text + "        ";
                Save.Save2Text(DateTime.Now.ToString("ddMMyyyy") + ".txt", sSave);
            }

            cbChuyenMay.Visible = false;
            cbChuyenMay.Items.Clear();
            cbChuyenMay.Items.Add("Chuyển Máy");
            cbChuyenMay.SelectedIndex = 0;

            bChuyenMay = false;
            bMayBan = false;
            TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = false;

            NewTTTS();
        }

        private void setTime(int iHours, int iMinutes)
        {
            //8k tieng
            int totalTime = (iHours * 60) + iMinutes;

            lTien = ((iHours * 60) + iMinutes) * 8000 / 60; //1 phut ~ 133d

            long iTemp = lTien;
            if (iTemp % 1000 >= 300)
            {
                iTemp = lTien - (lTien % 1000) + 1000;
            }
            else
            {
                iTemp = lTien - (lTien % 1000);
            }

            tbTien.Text = iTemp.ToString();



            ////6k tieng
            ////lTien = ((iHours * 60) + iMinutes) / 3 * 200 + 200; //3 phut = 200d
            //lTien = ((iHours * 60) + iMinutes) * 100; //1 phut = 100d
            //long iTemp = lTien;
            //if (iTemp % 500 > 250)
            //{
            //    iTemp = lTien - (lTien % 500) + 500;
            //}
            //else
            //{
            //    iTemp = lTien - (lTien % 500);
            //}

            //tbTien.Text = iTemp.ToString();
        }

        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            if (hours < 24)
            {
                if (seconds > 58)
                {
                    seconds = 0;

                    if (minutes > 58)
                    {
                        minutes = 0;
                        hours++;
                    }
                    else
                    {
                        minutes++;
                    }

                    setTime(hours, minutes);
                }
                else
                {
                    seconds++;
                }

                setCountDownTime();
            }
            else
            {
                timerCountDown.Enabled = false;
                MessageBox.Show("Thời gian chơi quá lâu!");
            }
        }

        private void tbSeconds_MouseClick(object sender, MouseEventArgs e)
        {
            tbSeconds.SelectAll();
        }

        private void tbMinutes_MouseClick(object sender, MouseEventArgs e)
        {
            tbMinutes.SelectAll();
        }

        private void tbHours_MouseClick(object sender, MouseEventArgs e)
        {
            tbHours.SelectAll();
        }

        private void tbHours_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbHours_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbMinutes, e);
        }

        private void tbMinutes_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbMinutes_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbSeconds, e);
        }

        private void tbSeconds_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbSeconds_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bEnter)
            {
                btBatDau_Click(sender, e);
            }
        }

        private void cbChuyenMay_DropDown(object sender, EventArgs e)
        {
            cbChuyenMay.Items.Clear();
            for (int i = 0; i < TinhTien.listMayBan.Count; i++)
            {
                if (!TinhTien.listMayBan[i])
                {
                    cbChuyenMay.Items.Add("Máy " + (i + 1).ToString());
                }
            }
        }

        private void cbChuyenMay_DropDownClosed(object sender, EventArgs e)
        {
            if (cbChuyenMay.SelectedIndex == -1)
            {
                cbChuyenMay.Items.Clear();
                cbChuyenMay.Items.Add("Chuyển Máy");
                cbChuyenMay.SelectedIndex = 0;
            }
            else
            {
                TinhTien.lrbTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].Checked = true;

                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].sGioChoi = sGioChoi;

                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].tbTien.Text = tbTien.Text;
                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].hours = int.Parse(lbHr.Text);
                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].minutes = int.Parse(lbMin.Text);
                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].seconds = int.Parse(lbSec.Text);
                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].lbSec.Text = lbSec.Text;
                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].lbTimesIn.Text = lbTimesIn.Text;
                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].bChuyenMay = true;
                TinhTien.lTTTS[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].btBatDau_Click(sender, e);

                string sChuyenMay = cbChuyenMay.Text;

                cbChuyenMay.Visible = false;
                cbChuyenMay.Items.Clear();
                cbChuyenMay.Items.Add("Chuyển Máy");
                cbChuyenMay.SelectedIndex = 0;

                bChuyenMay = false;
                bMayBan = false;
                TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = false;

                timerCountDown.Enabled = false;
                NewTTTS();
                lbTimesIn.Text = "Chuyển " + sChuyenMay + ": " + DateTime.Now.ToShortTimeString();
            }
        }
    }
}
