using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;

namespace DAO
{
    public class SQLDBConnection
    {
        public static SqlConnection CreateSQlConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                //connection.ConnectionString = @"Data Source=CONAN-PC\SQLEXPRESS;Initial Catalog=DienTu;Integrated Security=True";
                connection.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DienTu;Integrated Security=True";
                connection.Open();
                return connection;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối CSDL!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
