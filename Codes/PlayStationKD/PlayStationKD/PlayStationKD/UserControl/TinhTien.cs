using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DTO;
using BUS;

namespace PlayStationKD
{
    public partial class TinhTien : UserControl
    {
        public static BaoGioTheoTien BGTT = new BaoGioTheoTien();
        public static BaoGioTheoThoiGian BGTTG = new BaoGioTheoThoiGian();
        public static TinhTienTraSau TTTS = new TinhTienTraSau();

        public static List<RadioButton> lrbBGTT = new List<RadioButton>();
        public static List<RadioButton> lrbBGTTG = new List<RadioButton>();
        public static List<RadioButton> lrbTTTS = new List<RadioButton>();

        public static List<Panel> lpnBaoGio = new List<Panel>();
        public static List<BaoGioTheoTien> lBGTT = new List<BaoGioTheoTien>();
        public static List<BaoGioTheoThoiGian> lBGTTG = new List<BaoGioTheoThoiGian>();
        public static List<TinhTienTraSau> lTTTS = new List<TinhTienTraSau>();
        Panel pn = new Panel();

        int iMay = 0;



        public static List<bool> listMayBan; //Danh sach cac may co the chuyen

        public List<bool> ListMayBan
        {
            get { return listMayBan; }
            set { listMayBan = value; }
        }

        public static void setListMayBan(int iMay)
        {
            listMayBan[iMay - 1] = true;
        }

        public TinhTien()
        {
            InitializeComponent();

            //pn.Size = new Size(150, 130);
            //pn.Location = new Point(0, 20);

            LoadForm();
            //timerChuyenMay.Enabled = true;
            this.AutoScroll = true;
        }

        private void LoadForm()
        {
            this.Controls.Clear();
            lpnBaoGio.Clear();
            lBGTT.Clear();
            lBGTTG.Clear();
            List<May_DTO> lMay = May_BUS.SelectMay();
            iMay = lMay.Count;
            int x = 0;
            int y = 0;
            listMayBan = new List<bool>();

            Font newFont = new Font("Arial", 12F);

            for (int i = 0; i < iMay; i++)
            {
                if (i > 0 && i % 3 == 0)
                {
                    x = 0;
                    y++;
                }

                RadioButton rbBGTT = new RadioButton();
                rbBGTT.Name = i.ToString();
                rbBGTT.Text = "Tiền";
                rbBGTT.Checked = true;
                rbBGTT.Location = new Point(2, 0);
                rbBGTT.Size = new Size(58, 24);
                rbBGTT.Font = newFont;
                rbBGTT.CheckedChanged += new EventHandler(rbBGTT_CheckedChanged);

                rbBGTT.ForeColor = Color.Red;

                RadioButton rbBGTTG = new RadioButton();
                rbBGTTG.Name = i.ToString();
                rbBGTTG.Text = "TGian";
                rbBGTTG.Location = new Point(60, 0);
                rbBGTTG.Size = new Size(68, 24);
                rbBGTTG.Font = newFont;
                rbBGTTG.CheckedChanged += new EventHandler(rbBGTTG_CheckedChanged);

                RadioButton rbTTTS = new RadioButton();
                rbTTTS.Name = i.ToString();
                rbTTTS.Text = "Trả sau";
                rbTTTS.Location = new Point(130, 0);
                rbTTTS.Size = new Size(82, 24);
                rbTTTS.Font = newFont;
                rbTTTS.CheckedChanged += new EventHandler(rbTTTS_CheckedChanged);

                Panel pnTemp = new Panel();
                pnTemp.Size = new Size(215, 205);
                pnTemp.Location = new Point(x * 235 + 20, y * 220);
                lpnBaoGio.Add(pnTemp);

                lrbBGTT.Add(rbBGTT);
                lrbBGTTG.Add(rbBGTTG);
                lrbTTTS.Add(rbTTTS);

                lBGTT.Add(new BaoGioTheoTien(lMay[i].Ma, new Point(0, 20)));
                lBGTTG.Add(new BaoGioTheoThoiGian(lMay[i].Ma, new Point(0, 20)));
                lTTTS.Add(new TinhTienTraSau(lMay[i].Ma, new Point(0, 20)));

                lpnBaoGio[i].Controls.Add(lrbBGTT[i]);
                lpnBaoGio[i].Controls.Add(lrbBGTTG[i]);
                lpnBaoGio[i].Controls.Add(lrbTTTS[i]);

                lpnBaoGio[i].Controls.Add(lBGTT[i]);
                lpnBaoGio[i].Controls.Add(lBGTTG[i]);
                lpnBaoGio[i].Controls.Add(lTTTS[i]);

                this.Controls.Add(lpnBaoGio[i]);

                listMayBan.Add(false);

                x++;
            }
        }

        private void rbBGTT_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton temp = (RadioButton)sender;
            int i = int.Parse(temp.Name);

            if (temp.Checked)
            {
                temp.ForeColor = Color.Red;
            }
            else
            {
                temp.ForeColor = Color.Black;
            }

            lBGTT[i].Enabled = true;
            lBGTT[i].Visible = true;
            lBGTTG[i].Enabled = false;
            lBGTTG[i].Visible = false;
            lTTTS[i].Enabled = false;
            lTTTS[i].Visible = false;
        }

        private void rbBGTTG_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton temp = (RadioButton)sender;
            int i = int.Parse(temp.Name);

            if (temp.Checked)
            {
                temp.ForeColor = Color.Red;
            }
            else
            {
                temp.ForeColor = Color.Black;
            }

            lBGTT[i].Enabled = false;
            lBGTT[i].Visible = false;
            lBGTTG[i].Enabled = true;
            lBGTTG[i].Visible = true;
            lTTTS[i].Enabled = false;
            lTTTS[i].Visible = false;
        }

        private void rbTTTS_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton temp = (RadioButton)sender;
            int i = int.Parse(temp.Name);

            if (temp.Checked)
            {
                temp.ForeColor = Color.Red;
            }
            else
            {
                temp.ForeColor = Color.Black;
            }

            lBGTT[i].Enabled = false;
            lBGTT[i].Visible = false;
            lBGTTG[i].Enabled = false;
            lBGTTG[i].Visible = false;
            lTTTS[i].Enabled = true;
            lTTTS[i].Visible = true;
        }        
    }
}
