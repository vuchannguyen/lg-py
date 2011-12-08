using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace HR_STG_for_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName(@"HR-STG for Client").Length > 1)
            {
                Form_Notice frm = new Form_Notice("Chương trình HR-STG đang chạy!", false);
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
