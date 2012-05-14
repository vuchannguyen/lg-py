using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace QuanLyKinhDoanh
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
                MessageBox.Show("Chương trình Quản Lý Kinh Doanh đang được sử dụng!");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
        }
    }
}
