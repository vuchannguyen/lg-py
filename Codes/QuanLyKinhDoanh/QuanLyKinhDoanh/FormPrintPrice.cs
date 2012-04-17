using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace QuanLyKinhDoanh
{
    public partial class FormPrintPrice : Form
    {
        private PageSetupDialog pgSetupDialog = new PageSetupDialog();
        private PageSettings pgSettings = new PageSettings();
        private PrinterSettings prtSettings = new PrinterSettings();
        private PrintPreviewDialog dlg = new PrintPreviewDialog();

        float cmPerPix = 96 / (float)2.54;

        public FormPrintPrice()
        {
            InitializeComponent();
        }

        private void FormPrintPrice_Load(object sender, EventArgs e)
        {

        }

        private void printDocumentDecal_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            int x = SystemInformation.WorkingArea.X;
            int y = SystemInformation.WorkingArea.Y;

            int width = this.Width;
            int height = this.Height;

            Rectangle bounds = new Rectangle(x, y, width, height);
            this.DrawToBitmap(bitmap, bounds);
            bounds = new Rectangle(15, 30, 620, 670);

            Bitmap bmpCrop = bitmap.Clone(bounds, bitmap.PixelFormat);

            if (pgSettings.Landscape)
            {
                e.Graphics.DrawImage(bmpCrop, 775, 620);
            }
            else
            {
                e.Graphics.DrawImage(bmpCrop, 620, 775);
            }
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
    }
}
