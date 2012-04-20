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

        float cmPerPix = 96 / (float)2.54;

        public FormPrintPrice()
        {
            InitializeComponent();
        }

        private void FormPrintPrice_Load(object sender, EventArgs e)
        {
            InitPrintDefault();

            CreateTextBox();
        }

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

        private void CreateTextBox()
        {
            listTexbox = new List<TextBox>();
            gbDecal.Controls.Clear();

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

                listTexbox.Add(tb);
                gbDecal.Controls.Add(tb);

                x++;
            }
        }

        private void printDocumentDecal_PrintPage(object sender, PrintPageEventArgs e)
        {
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

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgSetupDialog.ShowDialog();
        }
    }
}
