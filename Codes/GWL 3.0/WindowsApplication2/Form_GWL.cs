using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsApplication2
{
    public partial class Form1 : Form
    {
        #region Variable
        float fBuocChan, X, Y;
        List<float> ArrayX = new List<float>();
        List<float> ArrayY = new List<float>();
        List<float> ArrayLength = new List<float>();
        List<int> ArrayCachTinh = new List<int>();
        List<float> ArrayRad = new List<float>();
        float fRatio = 1;

        float fTotalLength = 0;

        float fTyLeX = 0;
        float fTyLeY = 0;
        float fTyLe_Right = 0;
        float fTyLe_Top = 0;

        bool bTest = true;
        bool bStart = false;
        bool bDraw = true;
        bool bStartArrowDraw = false;

        bool bNoteDraw = false;
        bool bNoteErase = false;

        int n = 0;
        int xNote, yNote;
        int xNoteMove, yNoteMove;
        int iNote = 0;
        int iRedo = 0;
        int iStartX = 0;
        int iStartY = 0;
        int iNoteKind = 0;

        List<int> iColor = new List<int>();
        List<Pen> p = new List<Pen>();
        int tempColor = 0;
        Pen tempP = new Pen(Color.Black, 2);
        float fCmByPix = 72 / (float)2.54 / 3 * 4;
        PointF pStartArrow = new PointF();
        PointF pStartArrowLeft = new PointF();
        PointF pStartArrowRight = new PointF();

        Label lbStart = new Label();
        PictureBox picbStartArrow = new PictureBox();
        PictureBox picbNoteChoose = new PictureBox();
        List<PictureBox> picbNoteArray = new List<PictureBox>();
        List<int> iNoteArray = new List<int>();
        List<int> iNotePositionX = new List<int>();
        List<int> iNotePositionY = new List<int>();
        List<int> iNoteAngle = new List<int>();

        List<PictureBox> picbNoteKindArray = new List<PictureBox>();
        List<int> iNoteKindArray = new List<int>();
        List<Label> lbNoteArray = new List<Label>();
        List<String> sNoteKindArray = new List<String>();

        int ilbName = 0;
        int ilbPosition = 0;
        Label lbNameChoose = new Label();
        List<int> iNoteNamePositionX = new List<int>();
        List<int> iNoteNamePositionY = new List<int>();
        List<Label> lbNoteName = new List<Label>();
        List<String> sNoteName = new List<String>();

        String[] sToaDoNgang = { "1", "2", "3", "4", "5", "6", "7", "8", "9"};
        String[] sToaDoDoc = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K"};

        String sTenHoaDo = "";
        String sThucHien = "";

        private PageSetupDialog pgSetupDialog = new PageSetupDialog();
        private PageSettings pgSettings = new PageSettings();
        private PrinterSettings prtSettings = new PrinterSettings();
        private PrintPreviewDialog dlg = new PrintPreviewDialog();

        bool bSaved = true;
        int iOpen = 0;

        Gilwell.Startup A;
        #endregion

   

        #region Form
        public Form1()
        {
            InitializeComponent();

            if (Screen.PrimaryScreen.Bounds.Height < 750)
            {
                MessageBox.Show("                Không thể hiện thị ở độ phân giải này.\nVui lòng tăng độ phân giải màn hình để sử dụng chương trình.","Critical Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                A = new Gilwell.Startup(this);
                A.WaitStart += new Gilwell.Startup.TestEventHandler(A_WaitStart);
            }
        }

        private void LoadNote()
        {
            try
            {
                imageList.Images.Add(Gilwell.Properties.Resources.benhvien);
                imageList.Images.Add(Gilwell.Properties.Resources.cau);
                imageList.Images.Add(Gilwell.Properties.Resources.cautrenduong);
                imageList.Images.Add(Gilwell.Properties.Resources.cay);
                imageList.Images.Add(Gilwell.Properties.Resources.cayxang);
                imageList.Images.Add(Gilwell.Properties.Resources.cho);
                imageList.Images.Add(Gilwell.Properties.Resources.chua);
                imageList.Images.Add(Gilwell.Properties.Resources.nha);
                imageList.Images.Add(Gilwell.Properties.Resources.nhatho);
                imageList.Images.Add(Gilwell.Properties.Resources.rung);
                imageList.Images.Add(Gilwell.Properties.Resources.song);

                cbNote.Items.Add(new ImageComboItem("Bệnh viện", 0));
                cbNote.Items.Add(new ImageComboItem("Cầu", 1));
                cbNote.Items.Add(new ImageComboItem("Cầu trên đường", 2));
                cbNote.Items.Add(new ImageComboItem("Cây", 3));
                cbNote.Items.Add(new ImageComboItem("Cây xăng", 4));
                cbNote.Items.Add(new ImageComboItem("Chợ", 5));
                cbNote.Items.Add(new ImageComboItem("Chùa", 6));
                cbNote.Items.Add(new ImageComboItem("Nhà", 7));
                cbNote.Items.Add(new ImageComboItem("Nhà thờ", 8));
                cbNote.Items.Add(new ImageComboItem("Rừng", 9));
                cbNote.Items.Add(new ImageComboItem("Sông", 10));
                cbNote.SelectedIndex = -1;
                cbNote.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Không thể tải thông tin từ thư mục Resource!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picbStartup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            picbStartup.Dock = DockStyle.Fill;

            New(sender, e);

            A.Go();

            Point pLocation = new Point(3, (this.ClientRectangle.Height - tabcAll.Height) / 2);
            tabcAll.Location = pLocation;

            pgSettings.PaperSize = new PaperSize("A4", 827, 1169);

            pgSetupDialog.PageSettings = pgSettings;
            pgSetupDialog.PageSettings.PaperSize = pgSettings.PaperSize;
            pgSetupDialog.PageSettings.Landscape = true;
            pgSetupDialog.PrinterSettings = prtSettings;
            pgSetupDialog.AllowOrientation = true;
            pgSetupDialog.AllowMargins = false;

            picbGWL.AllowDrop = true;

            picbStartArrow.BackColor = Color.Transparent;
            picbStartArrow.Size = new Size(20, 20);

            LoadNote();
        }

        private void LoadCompleted()
        {
            tabcAll.Visible = true;
            menuStrip1.Enabled = true;
            this.MinimizeBox = true;
            picbStartup.Visible = false;
            picbNorthArrow.Visible = true;

            lbStart.Width = 150;
            lbStart.Text = "Đoạn đường khởi hành";
            lbStart.Location = new Point((int)fTyLe_Right + 50, (int)fTyLe_Top + 43);
            lbStart.Visible = false;
            this.Controls.Add(lbStart);

            tbDonvi.Focus();
        }

        private void picbStartup_Click(object sender, EventArgs e)
        {
            LoadCompleted();
        }

        private void tabcAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (picbStartup.Visible)
            {
                picbStartup_Click(sender, e);
            }
        }

        void A_WaitStart()
        {
            if (menuStrip1.Enabled == false)
            {
                LoadCompleted();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {        
            ArrayX = null;
            ArrayY = null;
            ArrayLength = null;
            ArrayRad = null;

            sToaDoNgang = null;
            sToaDoDoc = null;

            p = null;
            iColor = null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bSaved && n > iOpen)
            {
                DialogResult bClose = MessageBox.Show("Bạn có muốn lưu họa đồ hiện tại không?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (bClose == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (bClose == DialogResult.Yes)
                {
                    if (!Save(sender, e))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            ArrayX = null;
            ArrayY = null;
            ArrayLength = null;
            ArrayRad = null;

            sToaDoNgang = null;
            sToaDoDoc = null;

            p = null;
            iColor = null;
        }
        #endregion



        #region Fix_Form
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern Int32 EnableMenuItem(System.IntPtr hMenu, Int32 uIDEnableItem, Int32 uEnable);

        private const Int32 HTCAPTION = 0x00000002;

        private const Int32 MF_BYCOMMAND = 0x00000000;

        private const Int32 MF_ENABLED = 0x00000000;

        private const Int32 MF_GRAYED = 0x00000001;

        private const Int32 MF_DISABLED = 0x00000002;

        private const Int32 SC_MOVE = 0xF010;

        private const Int32 WM_NCLBUTTONDOWN = 0xA1;

        private const Int32 WM_SYSCOMMAND = 0x112;

        private const Int32 WM_INITMENUPOPUP = 0x117;

        Boolean Moveable = false;


        protected override void WndProc(ref System.Windows.Forms.Message m)
        {

            if (m.Msg == WM_INITMENUPOPUP)
            {

                //handles popup of system menu

                if ((m.LParam.ToInt32() / 65536) != 0) // 'divide by 65536 to get hiword
                {

                    Int32 AbleFlags = MF_ENABLED;

                    if (!Moveable)
                    {

                        AbleFlags = MF_DISABLED | MF_GRAYED; // disable the move

                    }

                    EnableMenuItem(m.WParam, SC_MOVE, MF_BYCOMMAND | AbleFlags);

                }

            } if (!Moveable)
            {

                if (m.Msg == WM_NCLBUTTONDOWN) //cancels the drag this is IMP
                {
                    if (m.WParam.ToInt32() == HTCAPTION) return;

                }

                if (m.Msg == WM_SYSCOMMAND) // Cancels any clicks on move menu
                {

                    if ((m.WParam.ToInt32() & 0xFFF0) == SC_MOVE) return;

                }

            }

            base.WndProc(ref m);
        }
        #endregion



        #region Paint
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (bStart == true)
            {
                menuStrip1.Enabled = true;
                this.MinimizeBox = true;
                picbStartup.Hide();
                picbStartup.WaitOnLoad = false;
                picbNorthArrow.Visible = true;

                bStart = false;
            }

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Clean(g);

            Font myFont = new Font("Helvetica", 12, FontStyle.Italic);
            Brush myBrush = new SolidBrush(Color.Black);

            g.DrawString("Họa đồ địa hình: ", myFont, myBrush, fTyLeX + 20, fTyLe_Top - 50);
            g.DrawString(sTenHoaDo, myFont, myBrush, fTyLeX + 150, fTyLe_Top - 50);
            g.DrawString("Thực hiện: ", myFont, myBrush, fTyLeX + 20, fTyLeY + 20);
            g.DrawString(sThucHien, myFont, myBrush, fTyLeX + 110, fTyLeY + 20);

            g.DrawString("Chú thích:", myFont, myBrush, fTyLe_Right + 25, fTyLe_Top + 10);
            if (n > 0 && !bStartArrowDraw)
            {
                g.DrawLine(p[0], fTyLe_Right + 25, fTyLe_Top + 50, fTyLe_Right + 45, fTyLe_Top + 50);
            }
            g.DrawString("Tỷ lệ xích: 1/" + tbRatio.Text, myFont, myBrush, fTyLe_Right + 25, fTyLeY - 20);

            myFont = new Font("Helvetica", 10, FontStyle.Regular);
            //g.Dispose();
        }

        private void picbGWL_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pSquare = new Pen(Color.LightGray);
            float fX = 0;
            float fY = 0;

            if (ArrayX.Count == 0)
            {
                ArrayX.Add(300);
                ArrayY.Add(240);
            }
            ArrayX[0] = 300 + (60 * iStartX);
            ArrayY[0] = 240 + (60 * iStartY);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    g.DrawRectangle(pSquare, fX, fY, 60, 60);
                    fX += 60;
                }
                fX = fX - 600;
                fY += 60;
            }

            g.DrawEllipse(new Pen(Color.Black, 2), ArrayX[0] - 5, ArrayY[0] - 5, 10, 10);

            fTotalLength = 0;
            if (n > 0)
            {
                Font myFont = new Font("Helvetica", 10, FontStyle.Regular);
                Brush myBrush = new SolidBrush(Color.Black);
                for (int i = 0; i < n; i++)
                {
                    g.DrawEllipse(new Pen(Color.Gray, 3), ArrayX[i + 1] - 2, ArrayY[i + 1] - 2, 4, 4);
                    g.DrawLine(p[i], ArrayX[i], ArrayY[i], ArrayX[i + 1], ArrayY[i + 1]);

                    if (ArrayCachTinh[i] == 0)
                    {
                        fTotalLength += ArrayLength[i] * fBuocChan;
                    }

                    if (ArrayCachTinh[i] == 1)
                    {
                        fTotalLength += ArrayLength[i];
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    if (chbNumber.Checked)
                    {
                        myFont = new Font("Arial", 11, FontStyle.Bold);
                        myBrush = new SolidBrush(Color.Goldenrod);
                        if (ArrayY[i + 1] - 20 <= 0)
                        {
                            g.DrawString((i + 1).ToString(), myFont, myBrush, ArrayX[i + 1] - 5, ArrayY[i + 1] + 10);
                        }
                        if (ArrayX[i + 1] + 5 >= 595)
                        {
                            if (i < 9)
                            {
                                g.DrawString((i + 1).ToString(), myFont, myBrush, ArrayX[i + 1] - 10, ArrayY[i + 1] - 20);
                            }
                            else
                            {
                                if (i < 99)
                                {
                                    g.DrawString((i + 1).ToString(), myFont, myBrush, ArrayX[i + 1] - 18, ArrayY[i + 1] - 20);
                                }
                                else
                                {
                                    g.DrawString((i + 1).ToString(), myFont, myBrush, ArrayX[i + 1] - 26, ArrayY[i + 1] - 20);
                                }
                            }
                        }
                        if (ArrayY[i + 1] - 20 > 0 && ArrayX[i + 1] + 5 < 595)
                        {
                            g.DrawString((i + 1).ToString(), myFont, myBrush, ArrayX[i + 1] - 5, ArrayY[i + 1] - 20);
                        }
                    }
                }        

                if (bStartArrowDraw)
                {
                    g.DrawLine(p[0], pStartArrow.X, pStartArrow.Y, pStartArrowRight.X, pStartArrowRight.Y);
                    g.DrawLine(p[0], pStartArrow.X, pStartArrow.Y, pStartArrowLeft.X, pStartArrowLeft.Y);
                }

                gbShow.Enabled = true;
                gbNote.Enabled = true;
                gbNoteName.Enabled = true;
            }
            else
            {
                gbShow.Enabled = false;
                gbNote.Enabled = false;
                gbNoteName.Enabled = false;
            }
        }

        private void Paint_DuongDi(float x, float y, float fLength, float fRad)
        {
            float x2, y2, dx, dy;
            x2 = y2 = 0;
            float tempLength = 0;

            if (cbCachTinh.SelectedIndex == 0)
            {
                tempLength = fBuocChan / 100 * fLength * fCmByPix * fRatio;
            }

            if (cbCachTinh.SelectedIndex == 1)
            {
                tempLength = fLength / 100 * fCmByPix * fRatio;
            }
            float tempRad = fRad * (float)Math.PI / 180;

            dx = tempLength * (float)Math.Sin(tempRad);
            x2 = x + dx;

            dy = tempLength * (float)Math.Cos(tempRad);
            y2 = y - dy;

            if (n == 0)
            {
                if (tempLength > 20)
                {
                    pStartArrow.X = x + (float)(20 * Math.Sin(tempRad));
                    pStartArrow.Y = y - (float)(20 * Math.Cos(tempRad));
                    tempRad = (fRad + 30) * (float)Math.PI / 180;
                    pStartArrowRight.X = x + 10 * (float)Math.Sin(tempRad);
                    pStartArrowRight.Y = y - 10 * (float)Math.Cos(tempRad);

                    tempRad = (fRad - 30) * (float)Math.PI / 180;
                    pStartArrowLeft.X = x + 10 * (float)Math.Sin(tempRad);
                    pStartArrowLeft.Y = y - 10 * (float)Math.Cos(tempRad);

                    bStartArrowDraw = true;
                }
                else
                {
                    MessageBox.Show("Đoạn đường quá ngắn không thể vẽ mũi tên khởi hành!");
                    cbColor.SelectedIndex = 0;
                    bStartArrowDraw = false;
                }
            }

            if (x2 < 0 || x2 > 600 || y2 < 0 || y2 > 480)
            {
                bDraw = false;
            }

            if (bDraw == true)
            {
                if (iRedo > 0)
                {
                    ArrayX[n + 1] = x2;
                    ArrayY[n + 1] = y2;
                }
                else
                {
                    ArrayX.Add(x2);
                    ArrayY.Add(y2);
                }

                if (cbCachTinh.SelectedIndex == 0)
                {
                    ArrayCachTinh.Add(0);
                }

                if (cbCachTinh.SelectedIndex == 1)
                {
                    ArrayCachTinh.Add(1);
                }
            }
        }

        private void Clean(Graphics g)
        {
            Rectangle rect = this.ClientRectangle;

            Color c = Color.Black;          

            X = 530;
            Y = 330;
       
            fTyLeX = X - 300;
            fTyLeY = fTyLe_Top = Y - 240;
            fTyLe_Right = X + 300;

            Font myFont = new Font("Helvetica", 10, FontStyle.Regular);
            Brush myBrush = new SolidBrush(Color.Gray);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0)
                    {
                        g.DrawString(sToaDoDoc[j], myFont, myBrush, fTyLeX - 5, fTyLeY - 20);
                    }
                    fTyLeX += 60;
                }
                fTyLeX = X - 300;
                g.DrawString(sToaDoNgang[i], myFont, myBrush, fTyLeX - 15, fTyLeY - 8);
                fTyLeY += 60;
            }

            g.DrawString(sToaDoNgang[8], myFont, myBrush, fTyLeX - 15, fTyLeY - 8);
            g.DrawString(sToaDoDoc[10], myFont, myBrush, fTyLe_Right - 5, fTyLe_Top - 20);

            g.DrawString("Phần mềm hỗ trợ vẽ họa đồ Gilwell - STG giữ toàn quyền.", myFont, myBrush, X - 90, fTyLeY + 85);
            g.DrawString("Copyright © 2010 by STG. All rights reserved.", myFont, myBrush, X - 55, fTyLeY + 105);
            //myFont = new Font("Helvetica", 16, FontStyle.Bold);
            //myBrush = new SolidBrush(Color.RoyalBlue);
            //g.DrawString("Phiên bản thử nghiệm", myFont, myBrush, X - 35, fTyLeY + 60);
        }
        #endregion



        #region tbTest
        private bool TestStartPointInput(String sStartInput)
        {
            if (sStartInput.Length != 0)
            {
                if (tbStartPoint.TextLength == 2)
                {
                    if (setStartPoint(sStartInput[0], sStartInput[1]))
                    {
                        return true;
                    }
                    else
                    {
                        bTest = true;
                        MessageBox.Show("Không có điểm khởi hành này trên họa đồ!\nVui lòng nhập lại.");
                        tbStartPoint.Focus();
                        tbStartPoint.SelectAll();
                        return false;
                    }
                }
                else
                {
                    bTest = true;
                    MessageBox.Show("Điểm khởi hành gồm 2 kí tự ứng với tọa độ họa đồ.");
                    tbStartPoint.Focus();
                    tbStartPoint.SelectAll();
                    return false;
                }
            }
            else
            {
                bTest = true;
                MessageBox.Show("Vui lòng nhập điểm khởi hành.");
                tbStartPoint.Focus();
                tbStartPoint.SelectAll();
                return false;
            }
        }

        private bool TestFootInput(String sFootInput)
        {
            if (sFootInput.Length != 0)
            {
                float fTestFoot;
                if (float.TryParse(sFootInput, out fTestFoot))
                {
                    if (fTestFoot < 0)
                    {
                        bTest = true;
                        MessageBox.Show("Độ dài 1 bước chân phải >0!\nVui lòng nhập lại.");
                        tbDonvi.Focus();
                        tbDonvi.SelectAll();
                        return false;
                    }
                    return true;
                }
                else
                {
                    bTest = true;
                    MessageBox.Show("Độ dài 1 bước chân không phù hợp!\nVui lòng nhập lại.");
                    tbDonvi.Focus();
                    tbDonvi.SelectAll();
                    return false;
                }
            }
            else
            {
                bTest = true;
                MessageBox.Show("Vui lòng nhập độ dài 1 bước chân.");
                tbDonvi.Focus();
                tbDonvi.SelectAll();
                return false;
            }
        }

        private bool TestRatioInput(String sRatioInput)
        {
            if (sRatioInput.Length != 0)
            {
                float fTestRatio;
                if (float.TryParse(sRatioInput, out fTestRatio))
                {
                    if (fTestRatio < 100 || fTestRatio > 100000)
                    {
                        if (fTestRatio < 100)
                        {
                            bTest = true;
                            MessageBox.Show("Tỷ lệ nhỏ nhất 1/100");
                            tbRatio.Text = "100";
                            tbRatio.Focus();
                            tbRatio.SelectAll();
                            return false;
                        }
                        if (fTestRatio > 100000)
                        {
                            bTest = true;
                            MessageBox.Show("Tỷ lệ lớn nhất 1/100000");
                            tbRatio.Text = "100000";
                            tbRatio.Focus();
                            tbRatio.SelectAll();
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    bTest = true;
                    MessageBox.Show("Tỷ lệ không hợp lệ.\n Vui lòng kiểm tra lại.");
                    tbRatio.Focus();
                    tbRatio.SelectAll();
                    return false;
                }
            }
            else
            {
                bTest = true;
                MessageBox.Show("Vui lòng nhập tỷ lệ.");
                tbRatio.Focus();
                tbRatio.SelectAll();
                return false;
            }
        }

        private bool TestLengthInput(String sLengthInput)
        {
            if (sLengthInput.Length != 0)
            {
                float fTestLength;
                if (float.TryParse(sLengthInput, out fTestLength))
                {
                    if (cbCachTinh.SelectedIndex == 0)
                    {
                        if (fTestLength < 1)
                        {
                            bTest = true;
                            MessageBox.Show("Số bước chân phải >= 1!\nVui lòng nhập lại.");
                            tbLength.Focus();
                            tbLength.SelectAll();
                            return false;
                        }

                        if ((int)fTestLength != fTestLength)
                        {
                            bTest = true;
                            MessageBox.Show("Số bước chân phải được làm tròn!\nVui lòng nhập lại.");
                            tbLength.Focus();
                            tbLength.SelectAll();
                            return false;
                        }
                    }

                    if (cbCachTinh.SelectedIndex == 1)
                    {
                        if (fTestLength < 0)
                        {
                            bTest = true;
                            MessageBox.Show("Khoảng cách phải > 0!\nVui lòng nhập lại.");
                            tbLength.Focus();
                            tbLength.SelectAll();
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    bTest = true;
                    MessageBox.Show("Số bước chân không hợp lệ!\nVui lòng nhập lại.");
                    tbLength.Focus();
                    tbLength.SelectAll();
                    return false;
                }
            }
            else
            {
                bTest = true;
                MessageBox.Show("Vui lòng nhập số bước chân.");
                tbLength.Focus();
                tbLength.SelectAll();
                return false;
            }
        }

        private bool TestRadInput(String sRadInput)
        {
            if (sRadInput.Length != 0)
            {
                float fTestRad;
                if (float.TryParse(sRadInput, out fTestRad))
                {
                    if (fTestRad < 0 || fTestRad > 360)
                    {
                        bTest = true;
                        MessageBox.Show("Góc phải nằm trong khoảng 0 -> 360 độ!");
                        tbRad.Focus();
                        tbRad.SelectAll();
                        return false;
                    }
                    return true;
                }
                else
                {
                    bTest = true;
                    MessageBox.Show("Góc không hợp lệ!\nVui lòng nhập lại.");
                    tbRad.Focus();
                    tbRad.SelectAll();
                    return false;
                }
            }
            else
            {
                bTest = true;
                MessageBox.Show("Vui lòng nhập góc.");
                tbRad.Focus();
                tbRad.SelectAll();
                return false;
            }
        }
        #endregion



        #region RotateImage
        private static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }

        public static Bitmap RotateImage(Image image, float angle)
        {
            return RotateImage(image, new PointF((float)image.Width / 2, (float)image.Height / 2), angle);
        }
        #endregion



        private bool setStartPoint(char cStartX, char cStartY)
        {
            int tempX = iStartX;
            int tempY = iStartY;
            bool bTestX = false;
            bool bTestY = false;
            String sStartX = new String(cStartX, 1);
            String sStartY = new String(cStartY, 1);

            #region SetStartPoint_event
            if (int.TryParse(sStartY, out tempY))
            {
                switch (cStartX)
                {
                    case 'A':
                        {
                            tempX = -5;
                            bTestX = true;
                            break;
                        }
                    case 'B':
                        {
                            tempX = -4;
                            bTestX = true;
                            break;
                        }
                    case 'C':
                        {
                            tempX = -3;
                            bTestX = true;
                            break;
                        }
                    case 'D':
                        {
                            tempX = -2;
                            bTestX = true;
                            break;
                        }
                    case 'E':
                        {
                            tempX = -1;
                            bTestX = true;
                            break;
                        }
                    case 'F':
                        {
                            tempX = 0;
                            bTestX = true;
                            break;
                        }
                    case 'G':
                        {
                            tempX = 1;
                            bTestX = true;
                            break;
                        }
                    case 'H':
                        {
                            tempX = 2;
                            bTestX = true;
                            break;
                        }
                    case 'I':
                        {
                            tempX = 3;
                            bTestX = true;
                            break;
                        }
                    case 'J':
                        {
                            tempX = 4;
                            bTestX = true;
                            break;
                        }
                    case 'K':
                        {
                            tempX = 5;
                            bTestX = true;
                            break;
                        }
                }

                tempY -= 5;
                bTestY = true;
                switch (cStartY)
                {
                    case '0':
                        {
                            bTestY = false;
                            break;
                        }
                }
            }
            else
            {
                if (int.TryParse(sStartX, out tempY))
                {
                    switch (cStartY)
                    {
                        case 'A':
                            {
                                tempX = -5;
                                bTestX = true;
                                break;
                            }
                        case 'B':
                            {
                                tempX = -4;
                                bTestX = true;
                                break;
                            }
                        case 'C':
                            {
                                tempX = -3;
                                bTestX = true;
                                break;
                            }
                        case 'D':
                            {
                                tempX = -2;
                                bTestX = true;
                                break;
                            }
                        case 'E':
                            {
                                tempX = -1;
                                bTestX = true;
                                break;
                            }
                        case 'F':
                            {
                                tempX = 0;
                                bTestX = true;
                                break;
                            }
                        case 'G':
                            {
                                tempX = 1;
                                bTestX = true;
                                break;
                            }
                        case 'H':
                            {
                                tempX = 2;
                                bTestX = true;
                                break;
                            }
                        case 'I':
                            {
                                tempX = 3;
                                bTestX = true;
                                break;
                            }
                        case 'J':
                            {
                                tempX = 4;
                                bTestX = true;
                                break;
                            }
                        case 'K':
                            {
                                tempX = 5;
                                bTestX = true;
                                break;
                            }
                    }

                    tempY -= 5;
                    bTestY = true;
                    switch (cStartX)
                    {
                        case '0':
                            {
                                bTestY = false;
                                break;
                            }
                    }
                }
                else
                {
                    return false;
                }
            }
            #endregion

            if (bTestX && bTestY)
            {
                iStartX = tempX;
                iStartY = tempY;
                return true;
            }

            return false;
        }

        private void New(object sender, EventArgs e)
        {
            gbDraw.Enabled = false;
            btBack.Visible = false;
            btQuyUoc.Visible = true;

            tbDonvi.Enabled = true;
            tbStartPoint.Enabled = true;
            tbRatio.Enabled = true;

            cbColor.SelectedIndex = 0;
            cbCachTinh.SelectedIndex = 0;
            tbLength.Text = "";
            tbRad.Text = "";

            tbTenHoaDo.Text = "";
            tbThucHien.Text = "";

            fTotalLength = 0;
            tbStartPoint.Text = "F5";
            setStartPoint('F', '5');
            tbDonvi.Text = "";
            tbRatio.Text = "10000";
            tabcAll.SelectedIndex = 0;
            tbDonvi.Focus();
            NoteUseEvent();
            Delete();
        }

        private void Delete()
        {
            ArrayX.Clear();
            ArrayY.Clear();
            ArrayLength.Clear();
            ArrayCachTinh.Clear();
            ArrayRad.Clear();

            n = 0;

            iColor.Clear();
            p.Clear();

            cbColor.SelectedIndex = 0;
            tbLength.Text = "";
            tbRad.Text = "";
            sTenHoaDo = "";
            sThucHien = "";

            tbTotalM.Text = "";
            tbAddName.Text = "";

            chbNumber.Checked = true;
            chbUseNote.Checked = true;

            bStartArrowDraw = false;
            picbGWL.Controls.Remove(picbStartArrow);

            for (int i = 0; i < iNote; i++)
            {
                picbGWL.Controls.Remove(picbNoteArray[i]);
            }

            for (int i = 0; i < iNoteKind; i++)
            {
                this.Controls.Remove(picbNoteKindArray[i]);
                this.Controls.Remove(lbNoteArray[i]);
            }

            for (int i = 0; i < ilbName; i++)
            {
                picbGWL.Controls.Remove(lbNoteName[i]);
            }
            picbNoteKindArray.Clear();
            lbNoteArray.Clear();
            picbNoteArray.Clear();
            iNoteArray.Clear();
            iNotePositionX.Clear();
            iNotePositionY.Clear();
            iNoteAngle.Clear();

            lbNoteName.Clear();
            iNoteNamePositionX.Clear();
            iNoteNamePositionY.Clear();
            sNoteName.Clear();

            lbStart.Visible = false;

            ilbName = 0;
            iNoteKind = 0;
            iNote = 0;
            iRedo = 0;
            iOpen = 0;

            bSaved = true;
            cbColor.Focus();
            this.Refresh();
        }



        #region LabelNote
        private void countLabelNote()
        {
            Label[] lbNoteKind = new Label[11];
            for (int i = 0; i < iNote; i++)
            {
                switch (iNoteArray[i])
                {
                    case 0:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Bệnh viện";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 1:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Cầu";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 2:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Cầu trên đường";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 3:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Cây";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 4:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Cây xăng";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 5:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Chợ";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 6:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Chùa";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 7:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Nhà";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 8:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Nhà thờ";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 9:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Rừng";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                    case 10:
                        {
                            if (lbNoteKind[iNoteArray[i]] == null)
                            {
                                lbNoteKind[iNoteArray[i]] = new Label();
                                lbNoteKind[iNoteArray[i]].Text = "Sông";
                                lbNoteKind[iNoteArray[i]].Name = iNoteArray[i].ToString();
                            }
                            break;
                        }
                }
            }

            for (int i = 0; i < 11; i++)
            {
                if (lbNoteKind[i] != null)
                {
                    sNoteKindArray.Add(lbNoteKind[i].Text);
                    iNoteKindArray.Add(i);
                    lbNoteArray.Add(lbNoteKind[i]);

                    PictureBox tempNote = new PictureBox();
                    tempNote.BackColor = Color.Transparent;
                    tempNote.Size = new Size(20, 20);
                    tempNote.Image = cbNote.ImageList.Images[i];

                    picbNoteKindArray.Add(tempNote);

                    iNoteKind++;
                }
            }

            lbNoteKind = null;
        }

        private void drawLabelNote()
        {
            int iNoteArrow = 1;
            if (n > 0 && !bStartArrowDraw)
            {
                iNoteArrow = 2;
            }
            for (int i = 0; i < iNoteKind; i++)
            {
                this.Controls.Remove(picbNoteKindArray[i]);
                this.Controls.Remove(lbNoteArray[i]);
            }
            sNoteKindArray.Clear();
            iNoteKindArray.Clear();
            picbNoteKindArray.Clear();
            lbNoteArray.Clear();

            iNoteKind = 0;
            countLabelNote();

            for (int i = 0; i < iNoteKind; i++)
            {
                int tempX = (int)fTyLe_Right + 50;
                int tempY = (int)fTyLe_Top + 20 + (i + iNoteArrow) * 30;
                lbNoteArray[i].Location = new Point(tempX, tempY);
                picbNoteKindArray[i].Location = new Point(tempX - 20, tempY);

                this.Controls.Add(picbNoteKindArray[i]);
                this.Controls.Add(lbNoteArray[i]);
            }
        }
        #endregion



        #region btClick
        private void btQuyUoc_Click(object sender, EventArgs e)
        {          
            if (TestStartPointInput(tbStartPoint.Text) && TestFootInput(tbDonvi.Text) && TestRatioInput(tbRatio.Text))
            {
                gbDraw.Enabled = true;
                cbColor.Focus();
                btBack.Visible = true;
                btQuyUoc.Visible = false;

                tbDonvi.Enabled = false;
                tbStartPoint.Enabled = false;
                tbRatio.Enabled = false;

                float tempRatio = (int)((float.Parse(tbRatio.Text) + 50) / 100) * 100;
                tbRatio.Text = tempRatio.ToString();

                fRatio = 10000 / tempRatio;
                int temp = (int)tempRatio / 100;
                tbTyLe.Text = "Tỷ lệ xích: 1/" + tbRatio.Text + " (1 centimet trên bản đồ bằng " + temp.ToString() + " met thực tế)";

                fBuocChan = float.Parse(tbDonvi.Text);
                this.Refresh();
            }
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            if (n > 0)
            {
                if (MessageBox.Show("Thay đổi quy ước sẽ xóa hết tác vụ đang thực hiện.\nBạn có chắc chắn không?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    New(sender, e);
                }
            }
            else
            {
                New(sender, e);
            }
        }

        private void btDraw_Click(object sender, EventArgs e)
        {
            if (TestLengthInput(tbLength.Text) && TestRadInput(tbRad.Text))
            {
                float tempLength = float.Parse(tbLength.Text);
                float tempRad = float.Parse(tbRad.Text);
                Paint_DuongDi(ArrayX[n], ArrayY[n], tempLength, tempRad);
                GetColor();

                if (bDraw == true)
                {
                    if (n == 0 && !bStartArrowDraw)
                    {
                        tempP = new Pen(Color.Green, 3);
                        tempColor = 3;

                        lbStart.Visible = true;
                    }
                    else
                    {
                        GetColor();
                    }

                    if (iRedo > 0)
                    {
                        p[n] = tempP;
                        iColor[n] = tempColor;
                        ArrayLength[n] = tempLength;
                        ArrayRad[n] = tempRad;
                    }
                    else
                    {
                        p.Add(tempP);
                        iColor.Add(tempColor);
                        ArrayLength.Add(tempLength);
                        ArrayRad.Add(tempRad);
                    }

                    n++;

                    if (iRedo > 0)
                    {
                        iRedo -= 1;
                    }

                    while (iRedo > 0)
                    {
                        iRedo -= 1;
                        ArrayX.RemoveAt(n + iRedo + 1);
                        ArrayY.RemoveAt(n + iRedo + 1);
                        ArrayLength.RemoveAt(n + iRedo);
                        ArrayCachTinh.RemoveAt(n + iRedo);
                        ArrayRad.RemoveAt(n + iRedo);

                        iColor.RemoveAt(n + iRedo);
                        p.RemoveAt(n + iRedo);
                    } 
   
                    cbColor.Focus();
                    bSaved = false;
                    this.Refresh();
                }
                else
                {
                    bDraw = true;
                    bTest = true;
                    MessageBox.Show("Không thể vẽ!\nĐường đi vượt quá giới hạn họa đồ.");

                    tbLength.Focus();
                    tbLength.SelectAll();
                }
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (!bSaved)
            {
                if (MessageBox.Show("Họa đồ đã thay đổi.\nBạn có chắc chắn xóa hết không?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Delete();      
                }
            }
            else
            {
                Delete(); 
            }
        }
        #endregion



        #region tb & cb event
        private void tbLength_TextChanged(object sender, EventArgs e)
        {
            if (tbLength.Text.Length > 0 && tbRad.Text.Length > 0)
            {
                btDraw.Enabled = true;
            }
            else
            {
                btDraw.Enabled = false;
            }
        }

        private void tbRad_TextChanged(object sender, EventArgs e)
        {
            if (tbLength.Text.Length > 0 && tbRad.Text.Length > 0)
            {
                btDraw.Enabled = true;
            }
            else
            {
                btDraw.Enabled = false;
            }
        }

        private void tbStartPoint_TextChanged(object sender, EventArgs e)
        {
            if (tbStartPoint.Text.Length > 0 && tbDonvi.Text.Length > 0 && tbRatio.Text.Length > 0)
            {
                btQuyUoc.Enabled = true;
            }
            else
            {
                btQuyUoc.Enabled = false;
            }
        }

        private void tbDonvi_TextChanged(object sender, EventArgs e)
        {
            if (tbStartPoint.Text.Length > 0 && tbDonvi.Text.Length > 0 && tbRatio.Text.Length > 0)
            {
                btQuyUoc.Enabled = true;
            }
            else
            {
                btQuyUoc.Enabled = false;
            }
        }

        private void tbRatio_TextChanged(object sender, EventArgs e)
        {
            if (tbStartPoint.Text.Length > 0 && tbDonvi.Text.Length > 0 && tbRatio.Text.Length > 0)
            {
                btQuyUoc.Enabled = true;
            }
            else
            {
                btQuyUoc.Enabled = false;
            }
        }

        private void GetColor()
        {
            if (cbColor.SelectedIndex == 0)
            {
                tempP = new Pen(Color.Black, 2);
                tempColor = 0;
            }
            if (cbColor.SelectedIndex == 1)
            {
                tempP = new Pen(Color.Blue, 2);
                tempColor = 1;
            }
            if (cbColor.SelectedIndex == 2)
            {
                tempP = new Pen(Color.Red, 2);
                tempColor = 2;
            }
        }

        private void cbColor_DropDownClosed(object sender, EventArgs e)
        {
            tbLength.Focus();
            tbLength.SelectAll();
        }

        private void cbCachTinh_DropDownClosed(object sender, EventArgs e)
        {
            tbRad.Focus();
            tbRad.SelectAll();
        }

        private void tbTenHoaDo_Leave(object sender, EventArgs e)
        {
            sTenHoaDo = tbTenHoaDo.Text;
            this.Refresh();
        }

        private void tbThucHien_Leave(object sender, EventArgs e)
        {
            sThucHien = tbThucHien.Text;
            this.Refresh();
        }
        #endregion



        #region Print
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (tbTenHoaDo.TextLength != 0 && tbThucHien.TextLength != 0)
            //{
                if (pgSetupDialog.PageSettings != null)
                {
                    pgSettings = pgSetupDialog.PageSettings;
                }
                else
                {
                    pgSettings.Landscape = true;
                }
                printDocument1.DefaultPageSettings = pgSettings;

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            //}
            //else
            //{
            //    if (tbTenHoaDo.TextLength == 0 && tbThucHien.TextLength == 0)
            //    {
            //        MessageBox.Show("Vui lòng nhập tên họa đồ và người thực hiện!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        tbTenHoaDo.Focus();
            //    }
            //    if (tbTenHoaDo.TextLength == 0 && tbThucHien.TextLength != 0)
            //    {
            //        MessageBox.Show("Vui lòng nhập tên họa đồ!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        tbTenHoaDo.Focus();
            //    }
            //    if (tbThucHien.TextLength == 0 && tbTenHoaDo.TextLength != 0)
            //    {
            //        MessageBox.Show("Vui lòng nhập tên người thực hiện!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        tbThucHien.Focus();
            //    }
            //}
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (tbTenHoaDo.TextLength != 0 && tbThucHien.TextLength != 0)
            //{
                if (pgSetupDialog.PageSettings != null)
                {
                    pgSettings = pgSetupDialog.PageSettings;
                }
                else
                {
                    pgSettings.Landscape = true;
                }

                printDocument1.DefaultPageSettings = pgSettings;
                dlg.Document = printDocument1;
                ((Form)dlg).WindowState = FormWindowState.Maximized;
                dlg.ShowDialog();
            //}
            //else
            //{
            //    if (tbTenHoaDo.TextLength == 0 && tbThucHien.TextLength == 0)
            //    {
            //        MessageBox.Show("Vui lòng nhập tên họa đồ và người thực hiện!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        tbTenHoaDo.Focus();
            //    }
            //    if (tbTenHoaDo.TextLength == 0 && tbThucHien.TextLength != 0)
            //    {
            //        MessageBox.Show("Vui lòng nhập tên họa đồ!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        tbTenHoaDo.Focus();
            //    }
            //    if (tbThucHien.TextLength == 0 && tbTenHoaDo.TextLength != 0)
            //    {
            //        MessageBox.Show("Vui lòng nhập tên người thực hiện!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        tbThucHien.Focus();
            //    }
            //}
        }
        
        private void pageSetupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgSetupDialog.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            int x = SystemInformation.WorkingArea.X;
            int y = SystemInformation.WorkingArea.Y;

            int width = this.Width;
            int height = this.Height;

            Rectangle bounds = new Rectangle(x, y, width, height);
            this.DrawToBitmap(bitmap, bounds);
            bounds = new Rectangle((int)fTyLeX - 20, (int)fTyLe_Top - 30, 810, 670);

            Bitmap bmpCrop = bitmap.Clone(bounds, bitmap.PixelFormat);

            if (pgSettings.Landscape)
            {
                e.Graphics.DrawImage(bmpCrop, (pgSettings.PaperSize.Height - 800) / 2, (pgSettings.PaperSize.Width - 660) / 2);
            }
            else
            {
                e.Graphics.DrawImage(bmpCrop, (pgSettings.PaperSize.Width - 800) / 2, (pgSettings.PaperSize.Height - 660) / 2);
            }
        }
        #endregion



        #region Save
        private bool Save(object sender, EventArgs e)
        {
            if (n > 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.FileName = sTenHoaDo;
                saveFileDialog1.Filter = "Gilwell file (*.gwl)|*.gwl";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.DefaultExt = "gwl";
                //saveFileDialog1.Title = "Where do you want to save the file?";
                saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String sNewFile = saveFileDialog1.FileName;
                    FileStream fs = File.Create(sNewFile);
                    fs.Close();

                    StreamWriter sw = new StreamWriter(sNewFile);
                    sw.WriteLine("Phần mềm GWL 3.0 - Copyright © 2010 by STG");
                    sw.WriteLine("3.0");
                    sw.WriteLine(sTenHoaDo);
                    sw.WriteLine(sThucHien);

                    sw.WriteLine(tbStartPoint.Text);
                    sw.WriteLine(fBuocChan.ToString());
                    sw.WriteLine(tbRatio.Text);
                    sw.WriteLine(n.ToString());

                    sw.WriteLine("Bắt đầu vẽ");
                    for (int i = 0; i < n; i++)
                    {
                        sw.WriteLine(iColor[i].ToString());
                        sw.WriteLine(ArrayX[i].ToString());
                        sw.WriteLine(ArrayY[i].ToString());
                        sw.WriteLine(ArrayLength[i].ToString());
                        sw.WriteLine(ArrayCachTinh[i].ToString());
                        sw.WriteLine(ArrayRad[i].ToString());
                    }
                    sw.WriteLine("Điểm kết thúc");
                    sw.WriteLine(ArrayX[n].ToString());
                    sw.WriteLine(ArrayY[n].ToString());

                    sw.WriteLine("Bắt đầu vẽ Note");
                    sw.WriteLine(iNote.ToString());
                    for (int i = 0; i < iNote; i++)
                    {
                        sw.WriteLine(iNoteArray[i].ToString());
                        sw.WriteLine(iNoteAngle[i].ToString());
                        sw.WriteLine(iNotePositionX[i].ToString());
                        sw.WriteLine(iNotePositionY[i].ToString());
                    }
                    sw.WriteLine("Kết thức vẽ Note");

                    sw.WriteLine("Bắt đầu vẽ NoteName");
                    sw.WriteLine(ilbName.ToString());
                    for (int i = 0; i < ilbName; i++)
                    {
                        sw.WriteLine(iNoteNamePositionX[i].ToString());
                        sw.WriteLine(iNoteNamePositionY[i].ToString());
                        sw.WriteLine(sNoteName[i]);
                    }
                    sw.WriteLine("Kết thúc vẽ NoteName");

                    sw.WriteLine("End");
                    sw.Close();
                }
            }
            else
            {
                MessageBox.Show("Không thể lưu! Họa đồ chưa được vẽ.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbNguoiVe.Focus();
            bSaved = Save(sender, e);
            if (bSaved)
            {
                iOpen = n;
            }
        }
        #endregion



        #region Open
        private void Open_Version1(object sender, EventArgs e, StreamReader sr)
        {
            sr.ReadLine();
            for (int i = 0; i < n; i++)
            {
                iColor.Add(int.Parse(sr.ReadLine()));
                ArrayX.Add(float.Parse(sr.ReadLine()) - 230);
                ArrayY.Add(float.Parse(sr.ReadLine()) - 90);
                ArrayLength.Add(float.Parse(sr.ReadLine()));
                ArrayCachTinh.Add(int.Parse(sr.ReadLine()));
                ArrayRad.Add(float.Parse(sr.ReadLine()));
                if (iColor[i] == 0)
                {
                    p.Add(new Pen(Color.Black, 2));
                }
                if (iColor[i] == 1)
                {
                    p.Add(new Pen(Color.Blue, 2));
                }
                if (iColor[i] == 2)
                {
                    p.Add(new Pen(Color.Red, 2));
                }
                if (iColor[i] == 3)
                {
                    p.Add(new Pen(Color.Green, 3));
                }
            }
            sr.ReadLine();
            ArrayX.Add(float.Parse(sr.ReadLine()) - 230);
            ArrayY.Add(float.Parse(sr.ReadLine()) - 90);
        }

        private void OpenNoteKind()
        {
            int iNoteArrow = 1;
            if (!bStartArrowDraw)
            {
                iNoteArrow = 2;
            }

            iNoteKind = 0;
            countLabelNote();

            for (int i = 0; i < iNoteKind; i++)
            {
                int tempX = (int)fTyLe_Right + 50;
                int tempY = (int)fTyLe_Top + 20 + (i + iNoteArrow) * 30;
                lbNoteArray[i].Location = new Point(tempX, tempY);
                picbNoteKindArray[i].Location = new Point(tempX - 20, tempY);

                this.Controls.Add(picbNoteKindArray[i]);
                this.Controls.Add(lbNoteArray[i]);
            }
        }

        private void Open_Version3(object sender, EventArgs e, StreamReader sr)
        {
            sr.ReadLine();
            iNote = int.Parse(sr.ReadLine());
            for (int i = 0; i < iNote; i++)
            {
                iNoteArray.Add(int.Parse(sr.ReadLine()));
                iNoteAngle.Add(int.Parse(sr.ReadLine()));
                iNotePositionX.Add(int.Parse(sr.ReadLine()));
                iNotePositionY.Add(int.Parse(sr.ReadLine()));
                AddNewPicbNote(cbNote.ImageList.Images[iNoteArray[i]], iNoteAngle[i], iNotePositionX[i], iNotePositionY[i], i, iNoteArray[i]);
            }
            sr.ReadLine();

            OpenNoteKind();

            sr.ReadLine();
            ilbName = int.Parse(sr.ReadLine());
            for (int i = 0; i < ilbName; i++)
            {
                iNoteNamePositionX.Add(int.Parse(sr.ReadLine()));
                iNoteNamePositionY.Add(int.Parse(sr.ReadLine()));
                sNoteName.Add(sr.ReadLine());
                AddName(sNoteName[i], i, iNoteNamePositionX[i], iNoteNamePositionY[i]);
            }
            sr.ReadLine();
        }

        private bool Open(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Gilwell file|*.gwl";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.DefaultExt = "gwl";
                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    sr.ReadLine();
                    String sVersion = sr.ReadLine();
                    if (sVersion == "1.0")
                    {
                        MessageBox.Show("Không phù hợp phiên bản!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    Delete();

                    sTenHoaDo = sr.ReadLine();
                    tbTenHoaDo.Text = sTenHoaDo;
                    sThucHien = sr.ReadLine();
                    tbThucHien.Text = sThucHien;

                    tbStartPoint.Text = sr.ReadLine();
                    fBuocChan = float.Parse(sr.ReadLine());
                    tbDonvi.Text = fBuocChan.ToString();
                    tbRatio.Text = sr.ReadLine();
                    fRatio = 10000 / float.Parse(tbRatio.Text);
                    n = int.Parse(sr.ReadLine());

                    ArrayX.Clear();
                    ArrayY.Clear();
                    sr.ReadLine();            
                    for (int i = 0; i < n; i++)
                    {
                        iColor.Add(int.Parse(sr.ReadLine()));
                        ArrayX.Add(float.Parse(sr.ReadLine()));
                        ArrayY.Add(float.Parse(sr.ReadLine()));
                        ArrayLength.Add(float.Parse(sr.ReadLine()));
                        ArrayCachTinh.Add(int.Parse(sr.ReadLine()));
                        ArrayRad.Add(float.Parse(sr.ReadLine()));

                        if (iColor[i] == 0)
                        {
                            p.Add(new Pen(Color.Black, 2));
                        }
                        if (iColor[i] == 1)
                        {
                            p.Add(new Pen(Color.Blue, 2));
                        }
                        if (iColor[i] == 2)
                        {
                            p.Add(new Pen(Color.Red, 2));
                        }
                        if (iColor[i] == 3)
                        {
                            p.Add(new Pen(Color.Green, 3));
                        }
                    }
                    sr.ReadLine();
                    ArrayX.Add(float.Parse(sr.ReadLine()));
                    ArrayY.Add(float.Parse(sr.ReadLine()));

                    float tempLength = fBuocChan / 100 * ArrayLength[0] * fCmByPix * fRatio;
                    float tempRad = ArrayRad[0] * (float)Math.PI / 180;

                    if (tempLength > 20)
                    {
                        pStartArrow.X = ArrayX[0] + (float)(20 * Math.Sin(tempRad));
                        pStartArrow.Y = ArrayY[0] - (float)(20 * Math.Cos(tempRad));
                        tempRad = (ArrayRad[0] + 30) * (float)Math.PI / 180;
                        pStartArrowRight.X = ArrayX[0] + 10 * (float)Math.Sin(tempRad);
                        pStartArrowRight.Y = ArrayY[0] - 10 * (float)Math.Cos(tempRad);

                        tempRad = (ArrayRad[0] - 30) * (float)Math.PI / 180;
                        pStartArrowLeft.X = ArrayX[0] + 10 * (float)Math.Sin(tempRad);
                        pStartArrowLeft.Y = ArrayY[0] - 10 * (float)Math.Cos(tempRad);

                        bStartArrowDraw = true;
                    }
                    else
                    {
                        lbStart.Visible = true;
                        bStartArrowDraw = false;
                    }

                    if (sVersion == "3.0")
                    {
                        Open_Version3(sender, e, sr);
                    }

                    cbCachTinh.SelectedIndex = ArrayCachTinh[n - 1];
                    btQuyUoc_Click(sender, e);
                    if (iColor[n - 1] == 3)
                    {
                        cbColor.SelectedIndex = 0;
                    }
                    else
                    {
                        cbColor.SelectedIndex = iColor[n - 1];
                    }
                    tabcAll_SelectedIndexChanged(sender, e);
                    sr.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Không thể mở họa đồ!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bSaved)
            {
                if (Open(sender, e))
                {
                    iOpen = n;

                    tbLength.Text = ArrayLength[n - 1].ToString();
                    tbRad.Text = ArrayRad[n - 1].ToString();
                    tbLength.Focus();
                    tbLength.SelectAll();
                }
            }
            else
            {
                DialogResult temp = MessageBox.Show("Họa đồ đã thay đổi!\nBạn có muốn lưu trước khi mở họa đồ khác không?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (temp == DialogResult.Yes)
                {
                    bSaved = Save(sender, e);
                    if (bSaved)
                    {
                        if (Open(sender, e))
                        {
                            iOpen = n;

                            tbLength.Text = ArrayLength[n - 1].ToString();
                            tbRad.Text = ArrayRad[n - 1].ToString();
                            tbLength.Focus();
                            tbLength.SelectAll();

                            return;
                        }
                    }
                }
                
                if (temp == DialogResult.No)
                {
                    if (Open(sender, e))
                    {
                        bSaved = true;
                        iOpen = n;

                        tbLength.Text = ArrayLength[n - 1].ToString();
                        tbRad.Text = ArrayRad[n - 1].ToString();
                        tbLength.Focus();
                        tbLength.SelectAll();

                        return;
                    }
                }
            }
        }
        #endregion



        #region Menu
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbNguoiVe.Focus();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbNguoiVe.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About frm = new About();
            frm.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!bSaved)
            {
                DialogResult temp = MessageBox.Show("Họa đồ đã thay đổi!\nBạn có muốn lưu trước khi mở họa đồ mới không?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (temp == DialogResult.Yes && Save(sender, e))
                {
                    New(sender, e);
                    iStartX = iStartY = 0;

                    return;
                }

                if (temp == DialogResult.No)
                {
                    New(sender, e);
                }
            }
            else
            {
                New(sender, e);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (n > iOpen && n > 1)
            {
                iRedo += 1;
                n -= 1;

                if (ArrayCachTinh[n] == 0)
                {
                    fTotalLength -= ArrayLength[n] * fBuocChan;
                }

                if (ArrayCachTinh[n] == 1)
                {
                    fTotalLength -= ArrayLength[n];
                }

                if (iColor[n - 1] == 3)
                {
                    cbColor.SelectedIndex = 0;
                }
                else
                {
                    cbColor.SelectedIndex = iColor[n - 1];
                }
                tbLength.Text = ArrayLength[n - 1].ToString();
                cbCachTinh.SelectedIndex = ArrayCachTinh[n - 1];
                tbRad.Text = ArrayRad[n - 1].ToString();
                tabcAll_SelectedIndexChanged(sender, e);

                tbLength.Focus();
                tbLength.SelectAll();
            }
            else
            {
                if (n == 1 && iOpen == 0)
                {
                    iRedo += 1;
                    n -= 1;

                    fTotalLength = 0;
                    cbColor.SelectedIndex = 0;
                    tbLength.Text = "";
                    cbCachTinh.SelectedIndex = 0;
                    tbRad.Text = "";

                    tbTotalM.Text = "";
                    tbAddName.Text = "";

                    for (int i = 0; i < iNote; i++)
                    {
                        picbNoteArray[i].Visible = false;
                    }

                    for (int i = 0; i < iNoteKind; i++)
                    {
                        picbNoteKindArray[i].Visible = false;
                        lbNoteArray[i].Visible = false;
                    }

                    for (int i = 0; i < ilbName; i++)
                    {
                        lbNoteName[i].Visible = false;
                    }
                    picbStartArrow.Visible = false;
                    lbStart.Visible = false;
                    tabcAll_SelectedIndexChanged(sender, e);

                    bSaved = true;
                    tbLength.Focus();
                }
            }

            this.Refresh();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (iRedo > 0)
            {
                iRedo -= 1;
                n += 1;

                if (ArrayCachTinh[n - 1] == 0)
                {
                    fTotalLength += ArrayLength[n - 1] * fBuocChan;
                }

                if (ArrayCachTinh[n - 1] == 1)
                {
                    fTotalLength += ArrayLength[n - 1];
                }

                if (iColor[n - 1] == 3)
                {
                    cbColor.SelectedIndex = 0;
                }
                else
                {
                    cbColor.SelectedIndex = iColor[n - 1];
                }
                tbLength.Text = ArrayLength[n - 1].ToString();
                cbCachTinh.SelectedIndex = ArrayCachTinh[n - 1];
                tbRad.Text = ArrayRad[n - 1].ToString();
                tbLength.Focus();
                tbLength.SelectAll();

                for (int i = 0; i < iNote; i++)
                {
                    picbNoteArray[i].Visible = true;
                }

                for (int i = 0; i < iNoteKind; i++)
                {
                    picbNoteKindArray[i].Visible = true;
                    lbNoteArray[i].Visible = true;
                }

                for (int i = 0; i < ilbName; i++)
                {
                    lbNoteName[i].Visible = true;
                }
                picbStartArrow.Visible = true;

                if (!bStartArrowDraw)
                {
                    lbStart.Visible = true;
                }
                tabcAll_SelectedIndexChanged(sender, e);

                bSaved = false;
                this.Refresh();
            }
        }
        #endregion



        #region CheckBox
        private void chbNumber_CheckedChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void chbUseNote_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUseNote.Checked)
            {
                for (int i = 0; i < iNote; i++)
                {
                    picbNoteArray[i].Visible = true;
                }

                for (int i = 0; i < iNoteKind; i++)
                {
                    picbNoteKindArray[i].Visible = true;
                    lbNoteArray[i].Visible = true;
                }

                for (int i = 0; i < ilbName; i++)
                {
                    lbNoteName[i].Visible = true;
                }
                picbStartArrow.Visible = true;

                if (!bStartArrowDraw)
                {
                    lbStart.Visible = true;
                }
            }
            else
            {
                cbNote.Enabled = false;
                picbNoteDraw.Image = Gilwell.Properties.Resources.d_d;
                bNoteDraw = false;
                cbNote.SelectedIndex = -1;

                for (int i = 0; i < iNote; i++)
                {
                    picbNoteArray[i].Visible = false;
                }

                for (int i = 0; i < iNoteKind; i++)
                {
                    picbNoteKindArray[i].Visible = false;
                    lbNoteArray[i].Visible = false;
                }

                for (int i = 0; i < ilbName; i++)
                {
                    lbNoteName[i].Visible = false;
                }
                picbStartArrow.Visible = false;
            }
        }

        private void NoteUseEvent()
        {
            if (bNoteDraw)
            {
                cbNote.Enabled = false;
                picbNoteDraw.Image = Gilwell.Properties.Resources.d_d;
                bNoteDraw = false;
                cbNote.SelectedIndex = -1;
            }

            if (bNoteErase)
            {
                picbNoteErase.Image = Gilwell.Properties.Resources.e_d;
                bNoteErase = false;
                picbGWL.Cursor = Cursors.Default;
            }

            //if (!bNoteDraw && !bNoteErase)
            //{
            //    picbGWL.Cursor = Cursors.Default;
            //}
        }  

        private void menuStrip1_Click(object sender, EventArgs e)
        {
            NoteUseEvent();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            NoteUseEvent();
        }
        #endregion



        #region Drag and Drop (khong su dung)
        private void picbGWL_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.Bitmap) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void picbGWL_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                try
                {
                    int xCurrentNote = e.X - ((Screen.PrimaryScreen.Bounds.Width - ClientRectangle.Width) / 2) - 240;
                    int yCurrentNote = e.Y - ((Screen.PrimaryScreen.Bounds.Height - ClientRectangle.Height) / 2) - 95;
                    //AddNewPicbNote(xCurrentNote, yCurrentNote);

                    iNote++;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }            
            }
        }
        #endregion



        #region AddNewPicbNote
        private void AddNewPicbNote(Image img, int iAngle, int xCurrent, int yCurrent, int iIndex, int iKindIndex)
        {
            try
            {
                PictureBox tempNote = new PictureBox();
                tempNote.Name = iIndex.ToString();
                tempNote.BackColor = Color.Transparent;
                tempNote.Size = new Size(20, 20);
                tempNote.Tag = iKindIndex;
                tempNote.Image = RotateImage(img, iAngle);
                tempNote.Location = new Point(xCurrent, yCurrent);
                tempNote.MouseEnter += new System.EventHandler(this.picbNote_MouseEnter);
                tempNote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picbNote_MouseDown);
                tempNote.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picbNote_MouseMove);
                tempNote.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picbNote_MouseUp);
                tempNote.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.picbNote_MouseWheel);
                tempNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picbNote_MouseClick);

                picbNoteArray.Add(tempNote);
                picbGWL.Controls.Add(picbNoteArray[iIndex]);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }   
        }

        private void picbNote_MouseEnter(object sender, EventArgs e)
        {
            if (bNoteErase)
            {
                picbNoteChoose = (PictureBox)sender;
                picbNoteChoose.Cursor = CreateCursor(Gilwell.Properties.Resources.erase_on, 1, 15);
            }
            else
            {
                picbNoteChoose = (PictureBox)sender;
                picbNoteChoose.Cursor = Cursors.NoMove2D;
            }
        }

        private void picbNote_MouseDown(object sender, MouseEventArgs e)
        {
            picbNoteChoose = (PictureBox)sender;
            xNote = e.X;
            yNote = e.Y;
            xNoteMove = picbNoteChoose.Left;
            yNoteMove = picbNoteChoose.Top;

            if (bNoteErase)
            {
                cbNote.Focus();
            }
            else
            {
                picbNoteChoose.Focus();
            }
        }

        private void picbNote_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bNoteErase)
            {
                if (e.Button == MouseButtons.Left)
                {
                    picbNoteChoose.Left += e.X - xNote;
                    picbNoteChoose.Top += e.Y - yNote;
                }
            }
        }

        private void picbNote_MouseUp(object sender, MouseEventArgs e)
        {
            if (picbNoteChoose.Left < 0 || picbNoteChoose.Right > 600 || picbNoteChoose.Top < 0 || picbNoteChoose.Bottom > 480)
            {
                picbNoteChoose.Left = xNoteMove;
                picbNoteChoose.Top = yNoteMove;
            }
            else
            {
                bSaved = false;
            }

            if (!bNoteErase)
            {
                iNotePositionX[int.Parse(picbNoteChoose.Name)] = picbNoteChoose.Left;
                iNotePositionY[int.Parse(picbNoteChoose.Name)] = picbNoteChoose.Top;
            }

            cbNote.Focus();
        }

        private void picbNote_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                picbNoteChoose = (PictureBox)sender;
                if (iNoteAngle[int.Parse(picbNoteChoose.Name)] >= 360)
                {
                    iNoteAngle[int.Parse(picbNoteChoose.Name)] = 0;
                }

                iNoteAngle[int.Parse(picbNoteChoose.Name)] += 5;
                picbNoteChoose.Image = RotateImage(cbNote.ImageList.Images[(int)picbNoteChoose.Tag], iNoteAngle[int.Parse(picbNoteChoose.Name)]);

                bSaved = false;
            }

            if (e.Delta < 0)
            {
                picbNoteChoose = (PictureBox)sender;
                if (iNoteAngle[int.Parse(picbNoteChoose.Name)] <= -360)
                {
                    iNoteAngle[int.Parse(picbNoteChoose.Name)] = 0;
                }

                iNoteAngle[int.Parse(picbNoteChoose.Name)] -= 5;
                picbNoteChoose.Image = RotateImage(cbNote.ImageList.Images[(int)picbNoteChoose.Tag], iNoteAngle[int.Parse(picbNoteChoose.Name)]);

                bSaved = false;
            }
        }

        private void picbNote_MouseClick(object sender, MouseEventArgs e)
        {
            if (bNoteErase)
            {
                picbNoteChoose = (PictureBox)sender;
                picbGWL.Controls.Remove(picbNoteChoose);
                picbNoteArray.RemoveAt(int.Parse(picbNoteChoose.Name));
                iNotePositionX.RemoveAt(int.Parse(picbNoteChoose.Name));
                iNotePositionY.RemoveAt(int.Parse(picbNoteChoose.Name));
                iNoteArray.RemoveAt(int.Parse(picbNoteChoose.Name));

                iNote--;
                drawLabelNote();
                for (int i = int.Parse(picbNoteChoose.Name); i < iNote; i++)
                {
                    picbNoteArray[i].Name = i.ToString();
                }

                bSaved = false;
            }
        }

        private void picbGWL_Click(object sender, EventArgs e)
        {
            if (cbNote.SelectedIndex != -1)
            {
                iNotePositionX.Add(MousePosition.X - ((Screen.PrimaryScreen.Bounds.Width - ClientRectangle.Width) / 2) - 238);
                iNotePositionY.Add(MousePosition.Y - ((Screen.PrimaryScreen.Bounds.Height - ClientRectangle.Height) / 2) - 94);
                iNoteArray.Add(cbNote.SelectedIndex);
                iNoteAngle.Add(0);
                AddNewPicbNote(cbNote.ImageList.Images[iNoteArray[iNote]], iNoteAngle[iNote], iNotePositionX[iNote], iNotePositionY[iNote], iNote, iNoteArray[iNote]);

                iNote++;
                drawLabelNote();

                bSaved = false;
            }
        }
        #endregion



        #region AddNewNoteName
        private void tbAddName_TextChanged(object sender, EventArgs e)
        {
            if (tbAddName.Text.Length > 0)
            {
                btAddName.Enabled = true;
            }
        }

        private void tbAddName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void tbAddName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                btAddName_Click(sender, e);
            }
        }

        private void AddName(String sName, int iIndex, int X, int Y)
        {
            Label lbNewName = new Label();
            lbNewName.Text = sName;
            lbNewName.Name = iIndex.ToString();
            lbNewName.Left = X;
            lbNewName.Top = Y;
            lbNewName.Size = TextRenderer.MeasureText(sName, lbNewName.Font);
            lbNewName.BackColor = Color.Transparent;
            lbNewName.MouseEnter += new System.EventHandler(this.lbNoteName_MouseEnter);
            lbNewName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbNoteName_MouseDown);
            lbNewName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbNoteName_MouseMove);
            lbNewName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbNoteName_MouseUp);
            lbNewName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbNoteName_MouseClick);

            lbNoteName.Add(lbNewName);
            picbGWL.Controls.Add(lbNoteName[iIndex]);
        }

        private void btAddName_Click(object sender, EventArgs e)
        {
            NoteUseEvent();
            if (tbAddName.Text.Length > 0)
            {
                sNoteName.Add(tbAddName.Text);

                int tempX, tempY;
                if (ilbName % 16 == 0)
                {
                    ilbPosition = 0;
                }

                if (ilbPosition < 8)
                {
                    tempX = 10;
                    tempY = 20 + (60 * ilbPosition);
                }
                else
                {
                    tempX = 600 - 10 - TextRenderer.MeasureText(sNoteName[ilbName], Label.DefaultFont).Width;
                    tempY = 20 + (60 * (ilbPosition - 8));
                }
                iNoteNamePositionX.Add(tempX);
                iNoteNamePositionY.Add(tempY);


                AddName(sNoteName[ilbName], ilbName, tempX, tempY);

                ilbName++;
                ilbPosition++;

                tbAddName.Text = "";
                tbAddName.Focus();
                bSaved = false;
            }
            else
            {
                bTest = true;
                MessageBox.Show("Vui lòng nhập thông tin chú thích tùy chọn!");
                tbAddName.Focus();
            }
        }

        private void lbNoteName_MouseEnter(object sender, EventArgs e)
        {
            if (bNoteErase)
            {
                lbNameChoose = (Label)sender;
                lbNameChoose.Cursor = CreateCursor(Gilwell.Properties.Resources.erase_on, 1, 15);
            }
            else
            {
                lbNameChoose = (Label)sender;
                lbNameChoose.Cursor = Cursors.NoMove2D;
            }
        }

        private void lbNoteName_MouseDown(object sender, MouseEventArgs e)
        {
            lbNameChoose = (Label)sender;
            xNote = e.X;
            yNote = e.Y;
            xNoteMove = lbNameChoose.Left;
            yNoteMove = lbNameChoose.Top;

            lbNameChoose.Focus();
        }

        private void lbNoteName_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bNoteErase)
            {
                if (e.Button == MouseButtons.Left)
                {
                    lbNameChoose.Left += e.X - xNote;
                    lbNameChoose.Top += e.Y - yNote;
                }
            }
        }

        private void lbNoteName_MouseUp(object sender, MouseEventArgs e)
        {
            if (lbNameChoose.Left < 0 || lbNameChoose.Right > 600 || lbNameChoose.Top < 0 || lbNameChoose.Bottom > 480)
            {
                lbNameChoose.Left = xNoteMove;
                lbNameChoose.Top = yNoteMove;
            }
            else
            {
                bSaved = false;
            }

            if (!bNoteErase)
            {
                iNoteNamePositionX[int.Parse(lbNameChoose.Name)] = lbNameChoose.Left;
                iNoteNamePositionY[int.Parse(lbNameChoose.Name)] = lbNameChoose.Top;
            }

            cbNote.Focus();
        }

        private void lbNoteName_MouseClick(object sender, MouseEventArgs e)
        {
            if (bNoteErase)
            {
                lbNameChoose = (Label)sender;
                picbGWL.Controls.Remove(lbNameChoose);
                sNoteName.RemoveAt(int.Parse(lbNameChoose.Name));
                iNoteNamePositionX.RemoveAt(int.Parse(lbNameChoose.Name));
                iNoteNamePositionY.RemoveAt(int.Parse(lbNameChoose.Name));
                lbNoteName.RemoveAt(int.Parse(lbNameChoose.Name));

                ilbName--;
                for (int i = int.Parse(lbNameChoose.Name); i < ilbName; i++)
                {
                    lbNoteName[i].Name = i.ToString();
                }

                bSaved = false;
            }
        }
        #endregion
        


        #region NewCursor
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }
        #endregion



        #region NoteDraw
        private void picbNoteDraw_Click(object sender, EventArgs e)
        {
            if (!bNoteDraw)
            {
                picbNoteErase.Image = Gilwell.Properties.Resources.e_d;
                bNoteErase = false;

                picbNoteDraw.Image = Gilwell.Properties.Resources.d_p;
                bNoteDraw = true;
                chbUseNote.Checked = true;
                cbNote.Enabled = true;
                cbNote.SelectedIndex = 0;
                picbGWL.Cursor = CreateCursor((Bitmap)cbNote.ImageList.Images[cbNote.SelectedIndex], 8, 9);
                picbNoteDraw.Focus();
                cbNote.Focus();
                cbNote.BackColor = Color.FromKnownColor(KnownColor.Highlight);
            }
            else
            {
                cbNote.BackColor = Color.FromKnownColor(KnownColor.Window);
                picbNoteDraw.Image = Gilwell.Properties.Resources.d_d;
                bNoteDraw = false;
                cbNote.Enabled = false;
                cbNote.SelectedIndex = -1;
            }           
        }

        private void picbNoteDraw_MouseLeave(object sender, EventArgs e)
        {
            cbNote.BackColor = Color.FromKnownColor(KnownColor.Window);
        }

        private void cbNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNote.SelectedIndex == -1)
            {
                picbGWL.Cursor = Cursors.Default;
            }
            else
            {
                picbGWL.Cursor = CreateCursor((Bitmap)cbNote.ImageList.Images[cbNote.SelectedIndex], 8, 9);
            }
        }
        #endregion



        #region NoteErase
        private void NoteErase(int iNoteErase)
        {
            picbNoteArray.RemoveAt(iNoteErase);
            for (int i = iNoteErase; i < iNote; i++)
            {
                picbNoteArray[i] = picbNoteArray[i + 1];
                picbNoteArray[i].Name = i.ToString();
            }

            iNote--;
        }

        private void picbNoteErase_Click(object sender, EventArgs e)
        {
            if (!bNoteErase)
            {
                picbNoteDraw.Image = Gilwell.Properties.Resources.d_d;
                bNoteDraw = false;
                cbNote.Enabled = false;
                cbNote.SelectedIndex = -1;

                picbNoteErase.Image = Gilwell.Properties.Resources.e_p;
                bNoteErase = true;
                picbGWL.Cursor = CreateCursor(Gilwell.Properties.Resources.erase, 1, 15);
            }
            else
            {
                picbNoteErase.Image = Gilwell.Properties.Resources.e_d;
                bNoteErase = false;
                picbGWL.Cursor = Cursors.Default;
            }
        }
        #endregion      



        private void tabcAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoteUseEvent();

            if (tabcAll.SelectedIndex == 0)
            {
                tbDonvi.Focus();
                tbDonvi.SelectAll();

                tbLength.Focus();
                tbLength.SelectAll();
            }

            if (tabcAll.SelectedIndex == 1)
            {
                tbAddName.Focus();
                tbAddName.SelectAll();
            }

            if (tabcAll.SelectedIndex == 2)
            {
                if (n <= 26)
                {
                    lvStatistics.Columns[2].Width = 50;
                }
                else
                {
                    lvStatistics.Columns[2].Width = 35;
                }

                tbTotalM.Text = String.Format("{0:0.##}", fTotalLength);
                tbTotalM.Focus();
                tbTotalM.SelectAll();

                if (this.lvStatistics.Items.Count > 0)
                {
                    this.lvStatistics.Items.Clear();
                }

                for (int i = 0; i < n; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (i + 1).ToString();
                    if (ArrayCachTinh[i] == 0)
                    {
                        lvi.SubItems.Add(String.Format("{0:0.##}", ArrayLength[i] * fBuocChan));
                    }

                    if (ArrayCachTinh[i] == 1)
                    {
                        lvi.SubItems.Add(String.Format("{0:0.##}", ArrayLength[i]));
                    }
                    lvi.SubItems.Add((ArrayRad[i]).ToString());
                    this.lvStatistics.Items.Add(lvi);
                }
            }
        }

        private void lvStatistics_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            if (e.ColumnIndex == 0)
            {
                e.NewWidth = 35;
            }
            if (e.ColumnIndex == 1)
            {
                e.NewWidth = 95;
            }
            if (e.ColumnIndex == 2)
            {
                if (n <= 22)
                {
                    e.NewWidth = 50;
                }
                else
                {
                    e.NewWidth = 35;
                }
            }
        }



        #region KeyPress
        private void tbStartPoint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void tbDonvi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void tbRatio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void cbColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void tbLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void cbCachTinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        } 

        private void tbRad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void tbTenHoaDo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void tbThucHien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bTest = false;
            }
        }

        private void tbStartPoint_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                tbDonvi.Focus();
                tbDonvi.SelectAll();
            }
        }

        private void tbDonvi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                tbRatio.Focus();
                tbRatio.SelectAll();
            }
        }

        private void tbRatio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                btQuyUoc_Click(sender, e);
            }
        }

        private void cbColor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                bTest = true;
                tbLength.Focus();
                tbLength.SelectAll();
            }
        }

        private void tbLength_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                tbRad.Focus();
                tbRad.SelectAll();
            }
        }

        private void tbRad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                btDraw_Click(sender, e);
            }
        }

        private void tbTenHoaDo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                tbThucHien.Focus();
                tbThucHien.SelectAll();
                this.Refresh();
            }
        }

        private void tbThucHien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                tbTenHoaDo.Focus();
                tbTenHoaDo.SelectAll();
                this.Refresh();
            }
        }
        #endregion 

        private void cbCachTinh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bTest)
            {
                bTest = true;
                tbRad.Focus();
                tbRad.SelectAll();
            }
        }
    }
}