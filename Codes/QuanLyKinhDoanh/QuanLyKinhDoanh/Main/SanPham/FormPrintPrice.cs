using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Library;
using DTO;
using BUS;

namespace QuanLyKinhDoanh
{
    public partial class FormPrintPrice : Form
    {
        private PageSetupDialog pgSetupDialog;
        private PageSettings pgSettings;
        private PrinterSettings prtSettings;
        private PrintPreviewDialog dlg;

        private List<TextBox> listTexbox;

        private int currentTextboxDecalId;

        float cmPerPix = 96 / (float)2.54;

        public FormPrintPrice()
        {
            InitializeComponent();

            if (Init())
            {
                RefreshData();
            }
            else
            {
                this.Visible = false;
            }
        }

        private void LoadResource()
        {
            try
            {
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
                pbRefresh.Image = Image.FromFile(ConstantResource.CHUC_NANG_REFRESH);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void FormPrintPrice_Load(object sender, EventArgs e)
        {
            LoadResource();

            InitPrintDefault();

            CreateTextBox();

            ValidateInput();
        }



        #region Function
        private void InitPrintDefault()
        {
            pgSetupDialog = new PageSetupDialog();
            pgSettings = new PageSettings();
            prtSettings = new PrinterSettings();
            dlg = new PrintPreviewDialog();

            pgSettings.PaperSize = new PaperSize("Custom", 630, 750); //16 x 20 cm
            pgSettings.Margins = new Margins(0, 0, 0, 0);
            pgSetupDialog.PageSettings = pgSettings;
            pgSetupDialog.PageSettings.PaperSize = pgSettings.PaperSize;
            pgSetupDialog.PageSettings.Landscape = false;
            pgSetupDialog.PrinterSettings = prtSettings;
            pgSetupDialog.AllowOrientation = true;
            pgSetupDialog.AllowMargins = false;
        }

        private bool Init()
        {
            if (!GetListGroupSP())
            {
                return false;
            }

            return true;
        }

        private void RefreshData()
        {
            tbSoLuong.Text = string.Empty;

            cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
            cbTen.SelectedIndex = cbTen.Items.Count > 0 ? 0 : -1;

            tbSoLuong.Focus();
        }

        private bool GetListGroupSP()
        {
            List<DTO.SanPhamGroup> listData = SanPhamGroupBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Nhóm sản Phẩm"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            cbGroup.Items.Clear();
            cbGroup.Items.Add(new CommonComboBoxItems(Constant.DEFAULT_FIRST_VALUE_COMBOBOX, 0));

            foreach (DTO.SanPhamGroup data in listData)
            {
                cbGroup.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private void GetListSP()
        {
            int idGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
            List<DTO.SanPham> listData = SanPhamBus.GetList(string.Empty, idGroup, true, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
                string.Empty, string.Empty, 0, 0);

            cbTen.Text = string.Empty;
            cbTen.Items.Clear();

            foreach (DTO.SanPham data in listData)
            {
                cbTen.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            if (listData.Count > 0)
            {
                cbTen.SelectedIndex = 0;
            }
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbSoLuong.Text) &&
                !string.IsNullOrEmpty(cbTen.Text)
                )
            {
                pbHoanTat.Enabled = true;
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
            else
            {
                pbHoanTat.Enabled = false;
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
            }
        }

        private void CreateTextBox()
        {
            listTexbox = new List<TextBox>();
            gbDecal.Controls.Clear();

            currentTextboxDecalId = 0;

            int x = 0;
            int y = 0;

            for (int i = 0; i < Constant.DEFAULT_TOTAL_DECAL; i++)
            {
                if (i > 0 && i % 7 == 0)
                {
                    x = 0;
                    y++;
                }

                TextBox tb = new TextBox();
                tb.Name = "tbDecal" + i.ToString();
                tb.Size = Constant.DEFAULT_SIZE_DECAL;
                tb.Location = new Point(x * Constant.DEFAULT_SIZE_DECAL.Width + (x == 0 ? 0 : x * Constant.DEFAULT_SPACE_WIDTH) + Constant.DEFAULT_LEFT_FIRST_DECAL,
                    y * Constant.DEFAULT_SIZE_DECAL.Height + (y == 0 ? 0 : y * Constant.DEFAULT_SPACE_HEIGHT) + Constant.DEFAULT_TOP_FIRST_DECAL);
                tb.Multiline = true;
                tb.MaxLength = 20;
                tb.TextAlign = HorizontalAlignment.Center;
                tb.BorderStyle = BorderStyle.None;

                tb.Tag = i;
                tb.Enter += new EventHandler(tbDecal_Enter);

                listTexbox.Add(tb);
                gbDecal.Controls.Add(tb);

                x++;
            }
        }

        private void ClearAllPrice()
        {
            for (int i = 0; i < Constant.DEFAULT_TOTAL_DECAL; i++)
            {
                listTexbox[i].Text = string.Empty;
            }

            listTexbox[0].Focus();
        }
        #endregion



        #region Controls
        private void printDocumentDecal_PrintPage(object sender, PrintPageEventArgs e)
        {
            listTexbox[currentTextboxDecalId].BackColor = Color.White;
            this.BackColor = Color.White;

            Bitmap bitmap = new Bitmap(gbDecal.Width, gbDecal.Height);

            int width = gbDecal.Width;
            int height = gbDecal.Height;

            Rectangle bounds = new Rectangle(0, 0, gbDecal.Width, gbDecal.Height);
            gbDecal.DrawToBitmap(bitmap, bounds);
            bounds = new Rectangle(8, 14, gbDecal.Width - 8, gbDecal.Height - 14);

            Bitmap bmpCrop = bitmap.Clone(bounds, bitmap.PixelFormat);
            bitmap.Dispose();
            if (pgSettings.Landscape)
            {
                e.Graphics.DrawImage(bmpCrop, 0, 0);
            }
            else
            {
                e.Graphics.DrawImage(bmpCrop, 0, 0);
            }

            listTexbox[currentTextboxDecalId].BackColor = Constant.COLOR_CHOOSEN_PRICE;
            this.BackColor = SystemColors.Control;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pgSetupDialog.PageSettings != null)
            {
                pgSettings = pgSetupDialog.PageSettings;
            }
            else
            {
                pgSettings.Landscape = true;
            }

            printDocumentDecal.DefaultPageSettings = pgSettings;

            if (printDialogDecal.ShowDialog() == DialogResult.OK)
            {
                printDocumentDecal.Print();
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pgSetupDialog.PageSettings != null)
            {
                pgSettings = pgSetupDialog.PageSettings;
            }
            else
            {
                pgSettings.Landscape = false;
            }

            printDocumentDecal.DefaultPageSettings = pgSettings;
            dlg.Document = printDocumentDecal;

            ((Form)dlg).WindowState = FormWindowState.Maximized;
            dlg.ShowDialog();
        }

        private void PageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgSetupDialog.ShowDialog();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_CONFIRM_DELETE_ALL_PRICE, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                ClearAllPrice();
            }
        }

        private void tbDecal_Enter(object sender, EventArgs e)
        {
            if (currentTextboxDecalId >= 0)
            {
                listTexbox[ConvertUtil.ConvertToInt(currentTextboxDecalId)].BackColor = Color.White;
            }

            TextBox tb = (TextBox)sender;
            currentTextboxDecalId = ConvertUtil.ConvertToInt(tb.Tag);
            listTexbox[currentTextboxDecalId].BackColor = Constant.COLOR_CHOOSEN_PRICE;
        }

        private void tbSoLuong_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            try
            {
                DTO.SanPham data = SanPhamBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbTen.SelectedItem).Value));

                if (data.GiaBan == 0)
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_MONEY, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                int count = ConvertUtil.ConvertToInt(tbSoLuong.Text);

                for (int i = 0; i < count; i++)
                {
                    if (currentTextboxDecalId + i < Constant.DEFAULT_TOTAL_DECAL)
                    {
                        listTexbox[currentTextboxDecalId + i].Text = data.MaSanPham + Constant.MESSAGE_NEW_LINE + data.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY) + Constant.DEFAULT_MONEY_SUBFIX;
                    }
                }

                listTexbox[currentTextboxDecalId + count].Focus();
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_NULL_DATA, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            if (Init())
            {
                RefreshData();
            }
            else
            {
                this.Visible = false;
            }
        }

        private void pbRefresh_MouseEnter(object sender, EventArgs e)
        {
            pbRefresh.Image = Image.FromFile(ConstantResource.CHUC_NANG_REFRESH_MOUSEOVER);
        }

        private void pbRefresh_MouseLeave(object sender, EventArgs e)
        {
            pbRefresh.Image = Image.FromFile(ConstantResource.CHUC_NANG_REFRESH);
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_MOUSEOVER);
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetListSP();
        }

        private void tbSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void cbTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
        #endregion
    }
}
