using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Library
{
    class File_Function
    {
        public static string OpenDialog(string sNameOfFile, string sTypeOfFile)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = sNameOfFile + "|*." + sTypeOfFile;
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.DefaultExt = sTypeOfFile;
                //openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog1.FileName;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static string SaveDialog(string defaultName, string sNameOfFile, string sTypeOfFile)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.FileName = defaultName;
                saveFileDialog1.Filter = sNameOfFile + "(*." + sTypeOfFile + ")|*." + sTypeOfFile;
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.DefaultExt = sTypeOfFile;
                //saveFileDialog1.Title = "Where do you want to save the file?";
                //saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog1.FileName;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static string getFinalFolder(List<string> list_Folder)
        {
            string sNewFolder = "";

            for (int i = 0; i < list_Folder.Count; i++)
            {
                if (i > 0)
                {
                    sNewFolder = Path.Combine(list_Folder[i - 1], list_Folder[i]);
                }
                else
                {
                    sNewFolder = list_Folder[i];
                }
                if (!Directory.Exists(sNewFolder))
                {
                    Directory.CreateDirectory(sNewFolder);
                }
            }

            return sNewFolder;
        }

        public static bool savePic(String sFileName, Bitmap img)
        {
            try
            {
                if (File.Exists(sFileName))
                {
                    File.Delete(sFileName);
                }

                Image_Function.saveJpeg(sFileName, img, 100);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool savePic(List<string> list_Folder, String sFileName, Bitmap img)
        {
            try
            {
                sFileName = Path.Combine(getFinalFolder(list_Folder), sFileName);

                Image_Function.saveJpeg(sFileName, img, 50);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
