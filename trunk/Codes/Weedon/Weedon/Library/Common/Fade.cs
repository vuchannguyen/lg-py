using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Library
{
    class Fade
    {
        public static Image Lighter(string sImage, int level, int nRed, int nGreen, int nBlue)
        {
            Image imgLight = GetImage(sImage);

            Graphics graphics = Graphics.FromImage(imgLight); //convert image to graphics object
            int conversion = (5 * (level - 50)); //calculate new alpha value
            Pen pLight = new Pen(Color.FromArgb(conversion, nRed, nGreen, nBlue), imgLight.Width * 2); //create mask with blended alpha value and chosen color as pen 
            graphics.DrawLine(pLight, -1, -1, imgLight.Width, imgLight.Height); //apply created mask to graphics object
            graphics.Save(); //save created graphics object and modify image object by that
            graphics.Dispose(); //dispose graphics object
            return imgLight; //return modified image
        }

        public static Image Lighter(Image img, int level, int nRed, int nGreen, int nBlue)
        {
            Graphics graphics = Graphics.FromImage(img); //convert image to graphics object
            int conversion = (5 * (level - 50)); //calculate new alpha value
            Pen pLight = new Pen(Color.FromArgb(conversion, nRed, nGreen, nBlue), img.Width * 2); //create mask with blended alpha value and chosen color as pen 
            graphics.DrawLine(pLight, -1, -1, img.Width, img.Height); //apply created mask to graphics object
            graphics.Save(); //save created graphics object and modify image object by that
            graphics.Dispose(); //dispose graphics object
            return img; //return modified image
        }

        public string Back = "Back";
        public string Forward = "Forward";
        public string icon_add = "icon_add";
        public string icon_cancel = "icon_cancel";
        public string icon_delete = "icon_delete";
        public string icon_edit = "icon_edit";
        public string icon_ok = "icon_ok";
        public string icon_thaydoi_themmoi = "icon_thaydoi_themmoi";
        public string icon_thaydoi_themthanhvien = "icon_thaydoi_themthanhvien";

        public static Image GetImage(string sImage)
        {
            switch (sImage)
            {
                default:
                    {
                        return null;
                    }

                //case "Back":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.Back;
                //    }

                //case "Forward":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.Forward;
                //    }

                //case "icon_add":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.icon_add;
                //    }

                //case "icon_cancel":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.icon_cancel;
                //    }

                //case "icon_delete":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.icon_delete;
                //    }

                //case "icon_edit":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.icon_edit;
                //    }

                //case "icon_ok":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.icon_ok;
                //    }

                //case "icon_thaydoi_themmoi":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.icon_thaydoi_themmoi;
                //    }

                //case "icon_thaydoi_themthanhvien":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.icon_thaydoi_themthanhvien;
                //    }

                //case "NewFolder":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.NewFolder;
                //    }

                //case "Paste":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.Paste;
                //    }

                //case "Refresh":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.Refresh;
                //    }

                //case "Rename":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.Rename;
                //    }

                //case "Sign":
                //    {
                //        return PTTK_0712306_0712451.Properties.Resources.Sign;
                //    }
            }
        }
    }
}
