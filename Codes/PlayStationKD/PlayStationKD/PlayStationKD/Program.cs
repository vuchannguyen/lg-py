using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace PlayStationKD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string currentProcess = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcessesByName(currentProcess).Length > 1)
            {
                MessageBox.Show("Chương trình PlayStationKD đang chạy!");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PlayStationKD());
            }
        }
    }
}
