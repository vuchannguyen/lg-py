using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PlayStationKD
{
    public partial class BaoGioTheoThoiGian : UserControl
    {
        int hours = 0;
        int minutes = 0;
        int seconds = 0;
        //long iTime;
        String sMay, sGioChoi, sGioNghi;
        bool bEnter;
        TimesUp timesUp;

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

        public BaoGioTheoThoiGian()
        {
            InitializeComponent();
            timesUp = new TimesUp();
        }

        public BaoGioTheoThoiGian(String sMaMay, Point pLocation)
        {
            InitializeComponent();
            this.Location = pLocation;
            lbMay.Text = "Máy " + sMaMay;
            sMay = sMaMay;
            timesUp = new TimesUp();
            this.Visible = false;
            cbChuyenMay.SelectedIndex = 0;
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

        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            if ((minutes == 0) && (hours == 0) && (seconds == 0))
            {
                timerCountDown.Enabled = false;
                sGioNghi = DateTime.Now.ToShortTimeString();
                String sInfo = "TGian: " + tbHours.Text + "h " + tbMinutes.Text + "m " + tbSeconds.Text + "s";
                timesUp = new TimesUp(sMay, sInfo, sGioChoi, sGioNghi, true);
                this.Controls.Add(timesUp);
                pnBGTTG.SendToBack();

                timerCountDown.Enabled = false;
                NewBGTTG();
                lbTimesIn.Text = "Giờ nghỉ: " + DateTime.Now.ToShortTimeString();

                label1.Focus();
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

        private void NewBGTTG()
        {
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

            cbChuyenMay.Visible = false;
            cbChuyenMay.Items.Clear();
            cbChuyenMay.Items.Add("Chuyển Máy");
            cbChuyenMay.SelectedIndex = 0;

            bChuyenMay = false;
            bMayBan = false;
            TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = false;
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
                    if (hours == 0 && minutes == 0 && seconds == 0)
                    {
                        tbHours.Focus();
                        tbHours.SelectAll();
                        return;
                    }

                    setCountDownTime();

                    tbHours.Visible = false;
                    tbMinutes.Visible = false;
                    tbSeconds.Visible = false;
                    sGioChoi = DateTime.Now.ToShortTimeString();
                    lbTimesIn.Text = "Giờ chơi: " + sGioChoi;

                    btBatDau.Visible = false;
                    btDung.Visible = true;
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

                tbHours.Visible = false;
                tbMinutes.Visible = false;
                tbSeconds.Visible = false;
                timerCountDown.Enabled = true;

                bMayBan = true;
                cbChuyenMay.Visible = true;
                TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = true;
            }
            bEnter = true;
        }

        private void btDung_Click(object sender, EventArgs e)
        {
            timerCountDown.Enabled = false;

            NewBGTTG();
            lbTimesIn.Text = "Giờ nghỉ: " + DateTime.Now.ToShortTimeString();
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

        private void BaoGioTheoThoiGian_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false)
            {
                timesUp.Enabled = false;
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
                TinhTien.lrbBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].Checked = true;

                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].sGioChoi = sGioChoi;

                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].tbHours.Text = tbHours.Text;
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].tbMinutes.Text = tbMinutes.Text;
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].tbSeconds.Text = tbSeconds.Text;
                
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].hours = int.Parse(lbHr.Text);
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].minutes = int.Parse(lbMin.Text);
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].seconds = int.Parse(lbSec.Text);
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].lbSec.Text = lbSec.Text;
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].lbTimesIn.Text = lbTimesIn.Text;
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].bChuyenMay = true;
                TinhTien.lBGTTG[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].btBatDau_Click(sender, e);

                string sChuyenMay = cbChuyenMay.Text;

                timerCountDown.Enabled = false;
                NewBGTTG();
                lbTimesIn.Text = "Chuyển " + sChuyenMay + ": " + DateTime.Now.ToShortTimeString();
            }
        }
    }
}
