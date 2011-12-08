using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace Co_Lau_711
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName(@"Co Lau 711").Length > 1)
            {
                MessageBox.Show("Chương trình đang chạy!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form_Main());
            }
        }
    }
}
