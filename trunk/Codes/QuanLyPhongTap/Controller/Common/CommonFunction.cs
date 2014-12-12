using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Model;
using System.IO;

namespace Controller.Common
{
    public class CommonFunction
    {
        public static string GetFilterText(string text)
        {
            string filter = string.Empty;

            if (!string.IsNullOrEmpty(text))
            {
                filter = text.Replace("%", "[%]");
                filter = filter.Replace("[", "[[]");
                filter = filter.Replace("_", "[_]");
                filter = "%" + Regex.Replace(filter.Trim(), @"\s+", "%") + "%";
            }

            return filter;
        }

        public static int ConvertStringToInt(string content)
        {
            int result = 0;
            int.TryParse(content, out result);
            return result;
        }

        public static bool AutoConnect()
        {
            bool res = false;

            if (File.Exists(CommonConstants.DATABASE_CONFIG_NAME))
            {
                StreamReader sr = null;

                try
                {
                    sr = new StreamReader(CommonConstants.DATABASE_CONFIG_NAME);

                    if (sr.ReadLine() == "WA")
                    {
                        SqlConnection.WindowsAuthentication = true;
                        SqlConnection.ServerName = sr.ReadLine();
                    }
                    else
                    {
                        SqlConnection.WindowsAuthentication = false;
                        SqlConnection.ServerName = sr.ReadLine();
                        SqlConnection.UserName = sr.ReadLine();
                        SqlConnection.Password = CryptoFunction.Crypto.DecryptDBPassText(sr.ReadLine());
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                }

                if (SqlConnection.NewConnection())
                {
                    res = true;
                }
            }
            else
            {
                //do nothing
            }

            return res;
        }

        public static void WriteConfiguration(bool isWA, string server, string username, string password)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(CommonConstants.DATABASE_CONFIG_NAME);

                if (isWA)
                {
                    sw.WriteLine("WA");
                }
                else
                {
                    sw.WriteLine("SSA");
                }

                sw.WriteLine(server);
                sw.WriteLine(username);
                sw.WriteLine(CryptoFunction.Crypto.EncryptDBPassText(password));
                sw.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}
