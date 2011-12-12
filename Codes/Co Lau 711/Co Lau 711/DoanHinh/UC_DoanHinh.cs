using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Function;

namespace Co_Lau_711
{
    public partial class UC_DoanHinh : UserControl
    {
        private int iPlayingTime;
        private int iShowTime;
        private int iTime;

        private int iCount;
        private int iPics;
        private List<int> list_Pics;

        private PictureBox pbBegin;
        private PictureBox pbEnd;
        private Image imgBackGround;
        private Image imgBackGround_mouseover;

        private bool bClick;
        private bool bCorrect;

        private UC_CountDownTimer uc_cdt;

        public UC_DoanHinh()
        {
            InitializeComponent();
        }

        private void UC_DoanHinh_Load(object sender, EventArgs e)
        {
            InitMain();

            newGameToolStripMenuItem_Click(sender, e);
        }

        private void InitMain()
        {
            lbError_Play.Text = "";

            this.Size = new Size(710, 540);

            pnMain.Size = new Size(700, 500);
            pnMain.Location = SubFunction.SetWidthCenter(this.Size, pnMain.Size, pnMain.Location.Y);

            pnPics.Size = new Size(480, 480);
            //pnPics.Location = SubFunction.SetCenterLocation(this.Size, pnPics.Size);

            gbSettings.Size = new Size(360, 180);
            gbSettings.Location = SubFunction.SetCenterLocation(this.Size, gbSettings.Size);

            lbContent.Location = SubFunction.SetCenterLocation(pnPics.Size, lbContent.Size);

            cbNumberOfPics.SelectedIndex = 2;
            cbPlayingTime.SelectedIndex = 1;
            cbShowTime.SelectedIndex = 0;

            pnMain.Visible = false;
            gbTime.Visible = false;
            gbSettings.Visible = true;
        }

        private void NewGame()
        {
            if (cbPlayingTime.Text == "none")
            {
                iPlayingTime = -1;
            }
            else
            {
                iPlayingTime = int.Parse(cbPlayingTime.Text.ToString());
            }

            if (cbShowTime.Text == "none")
            {

            }
            else
            {
                iShowTime = int.Parse(cbShowTime.Text.ToString());
            }

            iTime = 1;

            iCount = 0;
            iPics = int.Parse(cbNumberOfPics.Text.ToString());
            list_Pics = new List<int>();

            imgBackGround = Image.FromFile(@"Resources\background.jpg");
            imgBackGround_mouseover = Image.FromFile(@"Resources\background_mouseover.jpg");

            pbBegin = new PictureBox();
            pbBegin.Image = imgBackGround;

            pbEnd = new PictureBox();
            pbEnd.Image = imgBackGround;

            bCorrect = false;
            bClick = false;

            lbError_Play.Text = "";
        }

        private bool TestRandomPics(int iRandom, List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (iRandom == list[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void CreatePics()
        {
            int iRowAndColumn = (int)Math.Sqrt(iPics);

            int iWidth = pnPics.Width / iRowAndColumn;
            int iHeight = pnPics.Height / iRowAndColumn;

            pnPics.Controls.Clear();
            pnPics.Controls.Add(lbContent);

            for (int i = 0; i < iPics; i++)
            {
                int n = 0;

                do
                {
                    n = SubFunction.RandomIntNumber(0, iPics);
                }
                while (!TestRandomPics(n, list_Pics));

                list_Pics.Add(n);

                PictureBox pb = new PictureBox();
                pb.Name = "pb" + i.ToString();

                int x = iWidth * (n % iRowAndColumn);
                int y = iHeight * (n / iRowAndColumn);

                pb.Location = new Point(x, y);

                pb.Size = new Size(iWidth, iHeight);
                pb.Cursor = Cursors.Hand;

                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Image = imgBackGround;

                pb.MouseEnter += new EventHandler(pb_MouseEnter);
                pb.MouseLeave += new EventHandler(pb_MouseLeave);
                pb.Click += new EventHandler(pb_Click);

                pnPics.Controls.Add(pb);
            }

            lbContent.SendToBack();
        }



        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnMain.Visible = true;
            pnPics.Enabled = true;

            gbTime.Visible = true;
            gbTime.Enabled = true;

            gbSettings.Visible = false;

            NewGame();
            CreatePics();

            if (iPlayingTime != -1)
            {
                CreateTimer();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnMain.Visible = false;
            gbTime.Visible = false;
            gbSettings.Visible = true;
        }

        private void pb_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            pb.Image = imgBackGround_mouseover;
        }

        private void pb_MouseLeave(object sender, EventArgs e)
        {
            if (!bClick)
            {
                PictureBox pb = (PictureBox)sender;

                pb.Image = imgBackGround;
            }
            else
            {
                bClick = false;
            }
        }

        private void pb_Click(object sender, EventArgs e)
        {
            bClick = true;

            PictureBox pb = (PictureBox)sender;
            string sName = pb.Name;
            int iPic = int.Parse(sName.Substring(2)) / 2;

            Image img = pbBegin.Image;
            pb.Image = Image.FromFile(@"Resources\" + iPic.ToString() + ".jpg");

            if (img == imgBackGround || img == imgBackGround_mouseover)
            {
                pbBegin = pb;
                pb.Enabled = false;

                return;
            }

            string s1 = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(pbBegin.Image));
            string s2 = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(pb.Image));

            if (s1 == s2)
            {
                bCorrect = true;
            }
            else
            {
                bCorrect = false;
            }

            pbEnd = pb;

            pnPics.Enabled = false;

            timer_ShowTime.Start();
        }

        private void timer_ShowTime_Tick(object sender, EventArgs e)
        {
            if (bCorrect)
            {
                iCount++;

                if (iCount == iPics / 2)
                {
                    SubFunction.SetError(lbError_Play, lbError_Play.Location.Y, gbTime.Size, "Xin chúc mừng!");

                    gbTime.Enabled = false;
                    pnPics.Enabled = false;

                    //this.Dispose();
                }

                pbBegin.Dispose();
                pbEnd.Dispose();

                pbBegin.Image = imgBackGround;
                pbEnd.Image = imgBackGround;

                timer_ShowTime.Stop();

                if (gbTime.Enabled)
                {
                    pnPics.Enabled = true;
                }             
            }

            if (iTime == iShowTime)
            {
                pbBegin.Image = imgBackGround;
                pbEnd.Image = imgBackGround;

                pbBegin.Enabled = true;
                pbEnd.Enabled = true;

                iTime = 1;

                timer_ShowTime.Stop();

                if (gbTime.Enabled)
                {
                    pnPics.Enabled = true;
                }

                return;
            }

            iTime++;
        }

        private void CreateTimer()
        {
            gbTime.Controls.Clear();
            uc_cdt = new UC_CountDownTimer(0, iPlayingTime, 0);
            uc_cdt.EnabledChanged += new EventHandler(uc_cdt_EnabledChanged);
            uc_cdt.Location = SubFunction.SetCenterLocation(gbTime.Size, uc_cdt.Size);
            gbTime.Controls.Add(uc_cdt);
        }

        private void uc_cdt_EnabledChanged(object sender, EventArgs e)
        {
            if (!uc_cdt.BTimeCounting)
            {
                SubFunction.SetError(lbError_Play, lbError_Play.Location.Y, gbTime.Size, "Hết thời gian!");

                gbTime.Enabled = false;
                pnPics.Enabled = false;
            }
        }
    }
}
