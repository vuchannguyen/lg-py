using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Weedon
{
    public partial class AboutSoftware : Form
    {
        public AboutSoftware()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                //pbHeader.Image = Image.FromFile(ConstantResource.MAIN_LOGO);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void AboutSoftware_Load(object sender, EventArgs e)
        {
            LoadResource();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
