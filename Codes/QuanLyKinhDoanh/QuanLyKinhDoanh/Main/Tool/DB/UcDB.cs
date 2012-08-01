using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using QuanLyKinhDoanh.SanPham;
using DTO;
using BUS;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;

namespace QuanLyKinhDoanh
{
    public partial class UcDB : UserControl
    {
        private UserControl uc;
        private const int row = Constant.DEFAULT_ROW;

        private ListViewEx lvEx;
        private int columnCount;

        private string sortColumn;
        private string sortOrder;
        private Image imgCheck;
        private Image imgWarning;

        private int soSP;

        public UcDB()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbBackup.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_BACKUP);
                pbRestore.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_RESTORE);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcDB_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            this.BringToFront();

            this.Visible = true;
        }



        #region Function
        private void BackupDB(string databaseName, string serverName, string path)
        {
            try
            {
                Backup sqlBackup = new Backup();

                sqlBackup.Action = BackupActionType.Database;
                sqlBackup.BackupSetDescription = "ArchiveDataBase:" + DateTime.Now.ToShortDateString();
                sqlBackup.BackupSetName = "Archive";

                sqlBackup.Database = databaseName;

                BackupDeviceItem deviceItem = new BackupDeviceItem(path, DeviceType.File);
                ServerConnection connection = new ServerConnection(serverName);
                Server sqlServer = new Server(connection);

                Database db = sqlServer.Databases[databaseName];

                sqlBackup.Initialize = true;
                sqlBackup.Checksum = true;
                sqlBackup.ContinueAfterError = true;

                sqlBackup.Devices.Add(deviceItem);
                sqlBackup.Incremental = false;

                //sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);
                sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;

                sqlBackup.FormatMedia = false;

                sqlBackup.SqlBackup(sqlServer);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_BACKUP + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_ERROR_BACKUP_PATH,
                    Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Restore()
        {
            try
            {
                // Create a new database restore operation
                Restore rstDatabase = new Restore();
                // Set the restore type to a database restore
                rstDatabase.Action = RestoreActionType.Database;
                // Set the database that we want to perform the restore on
                rstDatabase.Database = "QuanLyKinhDoanh";

                // Set the backup device from which we want to restore, to a file
                BackupDeviceItem bkpDevice = new BackupDeviceItem(@"C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\Backup\test.bak", DeviceType.File);
                // Add the backup device to the restore type
                rstDatabase.Devices.Add(bkpDevice);
                // If the database already exists, replace it
                rstDatabase.ReplaceDatabase = true;

                Server myServer = new Server("(local)");

                // Perform the restore
                rstDatabase.SqlRestore(myServer);
            }
            catch (Exception ex)
            { 
                //
            }
        }

        private void RS()
        {
            Restore sqlRestore = new Restore();

            BackupDeviceItem deviceItem = new BackupDeviceItem(@"D:\Bakup\QLKD.bak", DeviceType.File);
            sqlRestore.Devices.Add(deviceItem);
            sqlRestore.Database = "QuanLyKinhDoanh";

            ServerConnection connection = new ServerConnection(@".\SQLEXPRESS");

            Server sqlServer = new Server(connection);

            Database db = sqlServer.Databases["QuanLyKinhDoanh"];
            sqlRestore.Action = RestoreActionType.Database;
            String dataFileLocation = @"C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\QuanLyKinhDoanh.mdf";
            String logFileLocation = @"C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\QuanLyKinhDoanh_Log.ldf";
            db = sqlServer.Databases["QuanLyKinhDoanh"];
            RelocateFile rf = new RelocateFile("QuanLyKinhDoanh", dataFileLocation);

            //sqlRestore.RelocateFiles.Add(new RelocateFile("QuanLyKinhDoanh", dataFileLocation));
            //sqlRestore.RelocateFiles.Add(new RelocateFile("QuanLyKinhDoanh" + "_log", logFileLocation));

            System.Data.DataTable logicalRestoreFiles = sqlRestore.ReadFileList(sqlServer);
            sqlRestore.RelocateFiles.Add(new RelocateFile(logicalRestoreFiles.Rows[0][0].ToString(), dataFileLocation));
            sqlRestore.RelocateFiles.Add(new RelocateFile(logicalRestoreFiles.Rows[1][0].ToString(), logFileLocation));

            sqlRestore.ReplaceDatabase = true;
            //sqlRestore.Complete += new ServerMessageEventHandler(sqlRestore_Complete);
            sqlRestore.PercentCompleteNotification = 10;
            //sqlRestore.PercentComplete +=
               //new PercentCompleteEventHandler(sqlRestore_PercentComplete);

            //sqlServer.ConnectionContext.Disconnect();

            sqlRestore.SqlRestore(sqlServer);
            db = sqlServer.Databases["QuanLyKinhDoanh"];
            db.SetOnline();
            sqlServer.Refresh();
        }
        #endregion



        private void pbBackup_Click(object sender, EventArgs e)
        {
            string path = File_Function.SaveDialog("QLKD", "Backup SQL", "bak");

            if (path != null)
            {
                BackupDB(Constant.DEFAULT_DB_NAME, Constant.DEFAULT_SERVER, path);
            }
        }

        private void pbBackup_MouseEnter(object sender, EventArgs e)
        {
            pbBackup.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_BACKUP_MOUSEOVER);
            lbBackup.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbBackup_MouseLeave(object sender, EventArgs e)
        {
            pbBackup.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_BACKUP);
            lbBackup.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbRestore_Click(object sender, EventArgs e)
        {
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            SqlDataReader reader;

            sql = "USE [master] " +
                    "ALTER DATABASE [QuanLyKinhDoanh] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE " +
                    "USE [master] " +
                    "EXEC master.dbo.sp_detach_db @dbname = N'QuanLyKinhDoanh' ";

            cnn = new SqlConnection(@".\SQLEXPRESS");

            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }

            //Restore();
            RS();
        }

        private void pbRestore_MouseEnter(object sender, EventArgs e)
        {
            pbRestore.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_RESTORE_MOUSEOVER);
            lbRestore.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbRestore_MouseLeave(object sender, EventArgs e)
        {
            pbRestore.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_RESTORE);
            lbRestore.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
