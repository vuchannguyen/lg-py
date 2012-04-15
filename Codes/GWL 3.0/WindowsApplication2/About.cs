using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsApplication2
{
    partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            //this.Text = String.Format("About {0}", AssemblyTitle);
            //this.labelProductName.Text = AssemblyProduct;
            //this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        //private void okButton_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            //Bitmap bitmap = new Bitmap(this.Width, this.Height);

            //int x = SystemInformation.WorkingArea.X;
            //int y = SystemInformation.WorkingArea.Y;

            //int width = this.Width;
            //int height = this.Height;

            //Rectangle bounds = new Rectangle(x, y, width, height);
            //this.DrawToBitmap(bitmap, bounds);

            //bounds = new Rectangle(225, 89, 7, 10);
            //Bitmap bmpCropCDClick = bitmap.Clone(bounds, bitmap.PixelFormat);
            //picbCDClick.Image = bmpCropCDClick;

            //bounds = new Rectangle(207, 169, 8, 10);
            //Bitmap bmpCropN2Click = bitmap.Clone(bounds, bitmap.PixelFormat);
            //picbN2Click.Image = bmpCropN2Click;
        }

        private void picbCDClick_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                picbCD.Dock = DockStyle.Fill;
                picbCD.Visible = true;

                //MessageBox.Show("Trứng phục sinh CD!\nCó tất cả 2 trứng.", "Congratulation :)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picbCD_Click(object sender, EventArgs e)
        {
            picbCD.Visible = false;
        }

        private void picbN2Click_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                picbN2.Dock = DockStyle.Fill;
                picbN2.Visible = true;

                //MessageBox.Show("Trứng phục sinh 2N!\nCó tất cả 2 trứng.", "Congratulation :)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picbN2_Click(object sender, EventArgs e)
        {
            picbN2.Visible = false;
        }
    }
}
