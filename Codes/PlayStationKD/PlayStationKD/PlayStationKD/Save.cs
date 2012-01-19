using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PlayStationKD
{
    public class Save
    {
        public static void Save2Text(String sFileName, String sWrite)
        {
            List<String> sContent = new List<String>();
            long lTongTien = 0;
            string sFolderYear = sFileName.Substring(4, 4);
            string sFolderMonth = Path.Combine(sFolderYear, sFileName.Substring(2, 2));
            sFileName = Path.Combine(sFolderMonth, sFileName);

            try
            {
                if (!Directory.Exists(sFolderYear))
                {
                    Directory.CreateDirectory(sFolderYear);
                }

                if (!Directory.Exists(sFolderMonth))
                {
                    Directory.CreateDirectory(sFolderMonth);
                }

                if (File.Exists(sFileName))
                {
                    StreamReader sr = new StreamReader(sFileName);
                    while (!sr.EndOfStream)
                    {
                        sContent.Add(sr.ReadLine());
                    }
                    sr.Close();

                    File.Delete(sFileName);

                    if (sContent.Count != 0)
                    {
                        lTongTien = long.Parse(sContent[0].Substring(11));
                    }
                    else
                    {
                        lTongTien = 0;
                    }
                }

                FileStream fs = File.Create(sFileName);
                fs.Close();

                if (sWrite.EndsWith("(CHI)"))
                {
                    lTongTien -= long.Parse(sWrite.Substring(10, 8));
                }
                else
                {
                    long lTemp = 0;
                    if (long.TryParse(sWrite.Substring(10, 8), out lTemp))
                    {
                        lTongTien += lTemp;
                    }
                }

                StreamWriter sw = new StreamWriter(sFileName, true);
                sw.WriteLine("Tổng cộng: " + lTongTien.ToString());
                for (int i = 1; i < sContent.Count; i++)
                {
                    sw.WriteLine(sContent[i]);
                }

                sw.WriteLine(sWrite);
                sw.Close();
            }
            catch
            {
                return;
            }
        }

        public static long getTongTien(String sFileName)
        {
            try
            {
                if (File.Exists(sFileName))
                {
                    StreamReader sr = new StreamReader(sFileName);
                    long lTemp = long.Parse(sr.ReadLine().Substring(11));
                    sr.Close();
                    return(lTemp);
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static List<String> GetContent(String sFileName)
        {
            List<String> sContent = new List<String>();
            try
            {
                if (File.Exists(sFileName))
                {
                    StreamReader sr = new StreamReader(sFileName);
                    while (!sr.EndOfStream)
                    {
                        sContent.Add(sr.ReadLine());
                    }
                    sr.Close();

                    return sContent;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
