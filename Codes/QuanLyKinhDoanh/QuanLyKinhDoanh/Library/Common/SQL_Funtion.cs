using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Library
{
    class SQL_Funtion
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
    }
}
