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
    public partial class UC_DoanSo : UserControl
    {
        private string sSo;
        private int iLength;
        private int iMaxSteps;
        private int iSteps;

        private int ilbStep_Position;
        //private List<Label> list_Step;

        private UC_CountDownTimer uc_cdt;

        public UC_DoanSo()
        {
            InitializeComponent();
        }

        private void UC_DoanSo_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(750, 470);
            gbSettings.Location = SubFunction.SetCenterLocation(this.Size, gbSettings.Size);

            //gbThongDiep.Location = SubFunction.SetCenterLocation(this.Size, gbThongDiep.Size);
            gbThongDiep.Location = new Point(465, 110);

            InitForm();

            btPlay_Click(sender, e);
        }

        

        #region Form
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iLength = (int)nUD_Length.Value;
            iMaxSteps = (int)nUD_Steps.Value;

            tbNumber.Enabled = false;
            //tbNumber.Size = TextRenderer.MeasureText(new string('9', iLength), tbNumber.Font);
            tbNumber.Text = "";
            tbNumber.MaxLength = iLength;
            //tbNumber.Location = SubFunction.SetWidthCenter(gbCreate.Size, tbNumber.Size, tbNumber.Top);

            lbLength.Text = iLength.ToString() + " digits";
            lbLength.Left = tbNumber.Right + 10;

            iSteps = 0;
            lbRemain.Text = "Số lần còn lại: " + (iMaxSteps - iSteps).ToString();

            //tbGuess.Size = tbNumber.Size;
            tbGuess.MaxLength = iLength;
            tbGuess.Text = "";

            lbError_Create.Text = "";
            lbError_Play.Text = "";

            pnSteps.Controls.Clear();

            pnGame.Visible = true;
            gbCreate.Enabled = true;
            gbPlay.Enabled = false;
            gbSettings.Visible = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnGame.Visible = false;
            gbSettings.Visible = true;
        }
        #endregion



        private bool TestSo(string sSo)
        {
            decimal iTest;
            if (decimal.TryParse(sSo, out iTest))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TestSoHopLe(string sSo)
        {
            int iTemp = 0;
            for (int i = 0; i < iLength; i++)
            {
                iTemp = 0;
                for (int j = 0; j < iLength; j++)
                {
                    if (sSo[i] == sSo[j])
                    {
                        iTemp++;
                        if (iTemp > 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }



        #region Create
        private int RandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private void btAuto_Click(object sender, EventArgs e)
        {
            do
            {
                sSo = RandomInt((int)Math.Pow(10, iLength - 1), (int)Math.Pow(10, iLength) - 1).ToString();
            }
            while (!TestSoHopLe(sSo));

            tbNumber.Enabled = false;
            tbNumber.UseSystemPasswordChar = true;
            tbNumber.Text = sSo;
        }

        private void btCustom_Click(object sender, EventArgs e)
        {
            tbNumber.Text = "";
            tbNumber.UseSystemPasswordChar = false;
            tbNumber.Enabled = true;
        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            lbError_Create.Text = "";

            if (tbNumber.Text.Length > 0 && !TestSo(tbNumber.Text))
            {
                SubFunction.SetError(lbError_Create, lbError_Create.Location.Y, gbCreate.Size, "Tắt bộ gõ dấu tiếng Việt!");
            }

            if (tbNumber.Text.Length == iLength)
            {
                btPlay.Enabled = true;
            }
            else
            {
                btPlay.Enabled = false;
            }
        }

        private void tbNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (SubFunction.TestNoneNumberInput(e))
            {
                e.SuppressKeyPress = true;
            }

            SubFunction.MuteEnterPress(e);
        }

        private void tbNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                if (tbNumber.Text.Length == iLength)
                {
                    btPlay_Click(sender, e);
                }
                else
                {
                    SubFunction.SetError(lbError_Create, lbError_Create.Location.Y, gbCreate.Size, "Not enough length!");
                }
            }
        }

        private void btPlay_Click(object sender, EventArgs e)
        {
            newGameToolStripMenuItem_Click(sender, e);
            btAuto_Click(sender, e);

            if (TestSo(tbNumber.Text))
            {
                if (TestSoHopLe(tbNumber.Text))
                {
                    gbCreate.Enabled = false;
                    tbNumber.UseSystemPasswordChar = true;

                    gbTime.Controls.Clear();
                    uc_cdt = new UC_CountDownTimer(0, 5, 0);
                    uc_cdt.EnabledChanged += new EventHandler(uc_cdt_EnabledChanged);
                    uc_cdt.Location = SubFunction.SetCenterLocation(gbTime.Size, uc_cdt.Size);
                    gbTime.Controls.Add(uc_cdt);

                    gbPlay.Enabled = true;
                    tbGuess.Focus();
                }
                else
                {
                    SubFunction.SetError(lbError_Create, lbError_Create.Location.Y, gbCreate.Size, "Các chữ số phải khác nhau!");
                }
            }
            else
            {
                SubFunction.SetError(lbError_Create, lbError_Create.Location.Y, gbCreate.Size, "Tắt bộ gõ dấu tiếng Việt");
            }
        }
        #endregion



        #region Guess
        private void tbGuess_TextChanged(object sender, EventArgs e)
        {
            lbError_Play.Text = "";

            if (tbGuess.Text.Length > 0 && !TestSo(tbGuess.Text))
            {
                SubFunction.SetError(lbError_Play, lbError_Play.Location.Y, new Size(gbPlay.Width / 2, gbPlay.Height), "Tắt bộ gõ dấu tiếng Việt");
            }

            if (tbGuess.Text.Length == iLength)
            {
                btGuess.Enabled = true;
            }
            else
            {
                btGuess.Enabled = false;
            }
        }

        private void tbGuess_KeyDown(object sender, KeyEventArgs e)
        {
            if (SubFunction.TestNoneNumberInput(e))
            {
                e.SuppressKeyPress = true;
            }

            SubFunction.MuteEnterPress(e);
        }

        private void tbGuess_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                if (tbGuess.Text.Length == iLength)
                {
                    btGuess_Click(sender, e);
                }
            }
        }

        private string getResult(string sNumber, string sGuess)
        {
            int B = 0;
            int W = 0;

            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < iLength; j++)
                {
                    if (sNumber[i] == sGuess[j])
                    {
                        if (i == j)
                        {
                            B++;
                        }
                        else
                        {
                            W++;
                        }
                    }
                }
            }

            if (B == iLength)
            {
                return "WIN";
            }
            else
            {
                return "Lần " + (iSteps + 1).ToString() + ": " + sGuess + " " + W.ToString() + "T" + " - " + B.ToString() + "Đ";
            }
        }

        private void AddStep(String sResult, int X, int Y)
        {
            Label lbNewStep = new Label();
            lbNewStep.Text = sResult;
            lbNewStep.Left = X;
            lbNewStep.Top = Y;
            lbNewStep.Size = TextRenderer.MeasureText(sResult, this.Font);

            pnSteps.Controls.Add(lbNewStep);
        }

        private void btGuess_Click(object sender, EventArgs e)
        {
            if (TestSo(tbGuess.Text))
            {
                if (TestSoHopLe(tbGuess.Text))
                {
                    string sResult = getResult(tbNumber.Text, tbGuess.Text);

                    int tempX, tempY;
                    if (iSteps % 10 == 0)
                    {
                        ilbStep_Position = 0;
                    }

                    if (iSteps < 10)
                    {
                        tempX = 20;
                        tempY = 20 * ilbStep_Position;
                    }
                    else
                    {
                        tempX = pnGame.Width - 30 - TextRenderer.MeasureText(sResult, this.Font).Width;
                        tempY = 20 * ilbStep_Position;
                    }

                    iSteps++;
                    ilbStep_Position++;

                    if (sResult == "WIN")
                    {
                        //SubFunction.SetError(lbError_Play, lbError_Play.Location.Y, gbPlay.Size, "YOU ARE WINNER :)");
                        uc_cdt.Enabled = false;

                        tbNumber.UseSystemPasswordChar = false;
                        gbPlay.Enabled = false;

                        //uc_HuongDan.Visible = false;
                        //gbThongDiep.Visible = true;

                        Form_ThongDiep frm = new Form_ThongDiep();
                        frm.ShowDialog();

                        //return;
                        this.Dispose();
                    }
                    else
                    {
                        AddStep(sResult, tempX, tempY);
                        lbRemain.Text = "Số lần còn lại: " + (iMaxSteps - iSteps).ToString();

                        tbGuess.Text = "";
                        tbGuess.Focus();
                    }

                    if (iSteps == iMaxSteps)
                    {
                        SubFunction.SetError(lbError_Play, lbError_Play.Location.Y, gbPlay.Size, "Hết lượt đoán. Vui lòng thử lại!");

                        tbNumber.UseSystemPasswordChar = false;
                        gbPlay.Enabled = false;

                        gbCreate.Enabled = true;
                        uc_cdt.Enabled = false;

                        btPlay.Enabled = true;
                    }
                }
                else
                {
                    SubFunction.SetError(lbError_Play, lbError_Play.Location.Y, new Size(gbPlay.Width, gbPlay.Height), "Các chữ số phải khác nhau!");
                }
            }
            else
            {
                SubFunction.SetError(lbError_Play, lbError_Play.Location.Y, new Size(gbPlay.Width, gbPlay.Height), "Tắt bộ gõ dấu tiếng Việt!");
            }
        }
        #endregion

        private void uc_cdt_EnabledChanged(object sender, EventArgs e)
        {
            if (!uc_cdt.BTimeCounting)
            {
                SubFunction.SetError(lbError_Play, lbError_Play.Location.Y, gbPlay.Size, "Thời gian đã hết. Vui lòng thử lại!");
                tbNumber.UseSystemPasswordChar = false;
                gbPlay.Enabled = false;

                gbCreate.Enabled = true;
                uc_cdt.Enabled = false;

                btPlay.Enabled = true;
            }
        }

        private void btThongDiep_Click(object sender, EventArgs e)
        {
            Form_ThongDiep frm = new Form_ThongDiep();
            frm.ShowDialog();

            //InitForm();
            this.Dispose();
        }

        private void InitForm()
        {
            pnSteps.Controls.Clear();
            lbRemain.Text = "Số lần còn lại";

            gbCreate.Enabled = true;
            gbPlay.Enabled = false;

            uc_HuongDan.Visible = true;
            gbThongDiep.Visible = false;

            gbTime.Controls.Clear();
            uc_cdt = new UC_CountDownTimer(0, 5, 0);
            uc_cdt.Location = SubFunction.SetCenterLocation(gbTime.Size, uc_cdt.Size);
            gbTime.Controls.Add(uc_cdt);
            uc_cdt.Enabled = false;

            tbNumber.Text = "";
            tbGuess.Text = ""; 

            lbError_Create.Text = "";
            lbError_Play.Text = "";

            btPlay.Enabled = true;
        }

        private void btCheat_Click(object sender, EventArgs e)
        {
            tbNumber.UseSystemPasswordChar = false;
        }

        private void Form_GuessNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult bClose = MessageBox.Show("Bạn có thật sự muốn thoát?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (bClose == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
