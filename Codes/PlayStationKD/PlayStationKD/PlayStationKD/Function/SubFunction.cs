﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Cryptography;

namespace Function
{
    class SubFunction
    {
        static bool bEnter = false;

        public static Point SetCenterLocation(Size bigSize, Size smallSize)
        {
            return (new Point((bigSize.Width - smallSize.Width) / 2, (bigSize.Height - smallSize.Height) / 2));
        }

        public static Point SetHeaderCenterForm(Size bigSize, Size smallSize)
        {
            return (new Point((bigSize.Width - smallSize.Width) / 2, 5));
        }

        public static Point SetWidthCenter(Size bigSize, Size smallSize, int iTop)
        {
            return (new Point((bigSize.Width - smallSize.Width) / 2, iTop));
        }

        public static void SetError(Label lb, int height, Size size, string sError)
        {
            lb.Text = sError;
            lb.Size = TextRenderer.MeasureText(sError, lb.Font);
            lb.Location = new Point((size.Width - lb.Size.Width) / 2, height);
        }

        public static bool TestNoneNumberInput(KeyEventArgs e)
        {
            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        return true;
                    }
                }
            }

            return false;
        }

        public static void MuteEnterPress(KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bEnter = false;
            }
        }

        public static void NextFocus(TextBox tb, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bEnter)
            {
                bEnter = true;
                tb.Focus();
                tb.SelectAll();
            }
        }

        public static bool tbPass(KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bEnter)
            {
                bEnter = true;
                return true;
            }

            return false;
        }

        public static String setSTT(int i)
        {
            if (i < 10)
            {
                return "0" + i.ToString();
            }
            else
            {
                if (i >= 10 && i <= 99)
                {
                    return i.ToString();
                }

                return i.ToString();
            }
        }

        public static void ClearlvItem(ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                lv.Items.Clear();
            }
        }

        public static string ThemMa3So(int iCount)
        {
            if (iCount < 9)
            {
                return ("00" + (iCount + 1).ToString());
            }

            if (iCount >= 9 && iCount < 99)
            {
                return ("0" + (iCount + 1).ToString());
            }

            if (iCount >= 99)
            {
                return ((iCount + 1).ToString());
            }

            return "00";
        }

        public static string ThemMa4So(int iCount)
        {
            if (iCount < 9)
            {
                return ("000" + (iCount + 1).ToString());
            }

            if (iCount >= 9 && iCount < 99)
            {
                return ("00" + (iCount + 1).ToString());
            }

            if (iCount >= 99 && iCount < 999)
            {
                return ("0" + (iCount + 1).ToString());
            }

            if (iCount >= 999)
            {
                return ((iCount + 1).ToString());
            }

            return "0000";
        }

        public static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static bool TestInt(string sInt)
        {
            int iTest;
            if (int.TryParse(sInt, out iTest))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
