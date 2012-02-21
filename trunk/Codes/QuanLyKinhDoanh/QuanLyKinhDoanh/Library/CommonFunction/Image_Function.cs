using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CommonFunction
{
    class Image_Function
    {
        public static Bitmap PrintScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(bmp as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bmp.Size);

            return bmp;
        }

        public static Image cloneBitMap(Bitmap bmp)
        {
            // Clone a portion of the Bitmap object.
            Rectangle cloneRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            PixelFormat format = bmp.PixelFormat;
            return bmp.Clone(cloneRect, format);
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
            }
            else
            {
                nPercent = nPercentW;
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)bmp;
        }

        public static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        private static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
            {
                if (codecs[i].MimeType == mimeType)
                {
                    return codecs[i];
                }
            }
            return null;
        }

        public static void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        public static Image ZoomImage(Image imgZoom, Rectangle zoomArea, int iZoom, Rectangle sourceArea)
        {
            Bitmap newBmp = new Bitmap(sourceArea.Width, sourceArea.Height);
            zoomArea.Width /= iZoom;
            zoomArea.Height /= iZoom;

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                //high interpolation
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgZoom, sourceArea, zoomArea, GraphicsUnit.Pixel);
            }

            return newBmp;
        }

        public static Image CropImage(Image imgZoom, Rectangle zoomArea, Rectangle sourceArea)
        {
            Bitmap newBmp = new Bitmap(sourceArea.Width, sourceArea.Height);

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                //high interpolation
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgZoom, sourceArea, zoomArea, GraphicsUnit.Pixel);
            }

            return newBmp;
        }

        public static Image ZoomCropImage(Image imgZoom, Rectangle zoomArea, Size sizeZoom, Rectangle sourceArea)
        {
            Bitmap newBmp = new Bitmap(sourceArea.Width, sourceArea.Height);
            zoomArea.Width = sizeZoom.Width;
            zoomArea.Height = sizeZoom.Height;

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                //high interpolation
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgZoom, sourceArea, zoomArea, GraphicsUnit.Pixel);
            }

            return newBmp;
        }

        public static Point setPicBound(Point point_PicBound, Size size_Pic, Size size_PicRec)
        {
            if (point_PicBound.X < 0 || point_PicBound.X > size_Pic.Width - size_PicRec.Width || point_PicBound.Y < 0 || point_PicBound.Y > size_Pic.Height - size_PicRec.Height)
            {
                if (point_PicBound.X < 0)
                {
                    point_PicBound.X = 0;

                    if (point_PicBound.Y < 0)
                    {
                        point_PicBound.Y = 0;
                    }

                    if (point_PicBound.Y > size_Pic.Height - size_PicRec.Height)
                    {
                        point_PicBound.Y = size_Pic.Height - size_PicRec.Height;
                    }
                }

                if (point_PicBound.X > size_Pic.Width - size_PicRec.Width)
                {
                    point_PicBound.X = size_Pic.Width - size_PicRec.Width;

                    if (point_PicBound.Y < 0)
                    {
                        point_PicBound.Y = 0;
                    }

                    if (point_PicBound.Y > size_Pic.Height - size_PicRec.Height)
                    {
                        point_PicBound.Y = size_Pic.Height - size_PicRec.Height;
                    }
                }

                if (point_PicBound.Y < 0)
                {
                    point_PicBound.Y = 0;

                    if (point_PicBound.X < 0)
                    {
                        point_PicBound.X = 0;
                    }

                    if (point_PicBound.X > size_Pic.Width - size_PicRec.Width)
                    {
                        point_PicBound.X = size_Pic.Width - size_PicRec.Width;
                    }
                }

                if (point_PicBound.Y > size_Pic.Height - size_PicRec.Height)
                {
                    point_PicBound.Y = size_Pic.Height - size_PicRec.Height;

                    if (point_PicBound.X < 0)
                    {
                        point_PicBound.X = 0;
                    }

                    if (point_PicBound.X > size_Pic.Width - size_PicRec.Width)
                    {
                        point_PicBound.X = size_Pic.Width - size_PicRec.Width;
                    }
                }
            }

            return new Point(point_PicBound.X, point_PicBound.Y);
        }
    }
}
