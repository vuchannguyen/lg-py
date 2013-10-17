using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Library
{
    public class Convert_Function
    {
        // This method uses the System.Drawing.Image.Save method to save the image
        // to a memorystream. The memory stream can then be used to return a byte
        // array using the ToArray() method in the MemoryStream class.
        public static byte[] ConvertImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        // This method uses the Image.FromStream method in the Image class to create a
        // method from a memorystream which has been created using a byte array. The 
        // image thus created is returned in this method.
        public static Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string ConvertByteArrayToString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);

            for (int i = 0; i < byteArray.Length; i++)       // <-- use for loop is faster than foreach   
                hex.Append(byteArray[i].ToString("X2"));   // <-- ToString is faster than AppendFormat   

            return hex.ToString();   
        }

        public static byte[] ConvertStringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}
