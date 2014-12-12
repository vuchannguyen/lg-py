using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace CryptoFunction
{
    public class MyTripleDES
    {
        //  Call this function to remove the key from memory after use for security
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        // Function to Generate a 64 bits Key.
        public static String GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            TripleDESCryptoServiceProvider TrDesCrypto = (TripleDESCryptoServiceProvider)TripleDESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(TrDesCrypto.Key);
        }

        private static void SetMode(TripleDESCryptoServiceProvider TrDES, int iPadding, int iMode)
        {
            switch (iPadding)
            {
                case 0:
                    {
                        TrDES.Padding = PaddingMode.ANSIX923;
                        break;
                    }

                case 1:
                    {
                        TrDES.Padding = PaddingMode.PKCS7;
                        break;
                    }

                case 2:
                    {
                        TrDES.Padding = PaddingMode.ISO10126;
                        break;
                    }
                default:
                    {
                        TrDES.Padding = PaddingMode.PKCS7;
                        break;
                    }

            }

            switch (iMode)
            {
                case 0:
                    {
                        TrDES.Mode = CipherMode.ECB;
                        break;
                    }

                case 1:
                    {
                        TrDES.Mode = CipherMode.CBC;
                        break;
                    }

                case 2:
                    {
                        TrDES.Mode = CipherMode.CFB;
                        break;
                    }
                    default:
                    {
                        TrDES.Mode = CipherMode.ECB;
                        break;
                    }
            }
        }

        public static void EncryptFile(String sInputFilename, String sOutputFilename, String sKey, String sIV, int iPadding, int iMode)
        {

            FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write);

            TripleDESCryptoServiceProvider TrDES = new TripleDESCryptoServiceProvider();
            SetMode(TrDES, iPadding, iMode);

            TrDES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);

            if (iMode != 0)
            {
                TrDES.IV = ASCIIEncoding.ASCII.GetBytes(sIV);
            }
            ICryptoTransform desencrypt = TrDES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }

        public static void EncryptData(string sContent, String sOutputFilename, String sKey, String sIV, int iPadding, int iMode)
        {
            //FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write);

            TripleDESCryptoServiceProvider TrDES = new TripleDESCryptoServiceProvider();
            SetMode(TrDES, iPadding, iMode);

            TrDES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);

            if (iMode != 0)
            {
                TrDES.IV = ASCIIEncoding.ASCII.GetBytes(sIV);
            }
            ICryptoTransform desencrypt = TrDES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

            //byte[] bytearrayinput = new byte[stream_Encrypt.Length];
            //stream_Encrypt.Read(bytearrayinput, 0, bytearrayinput.Length);

            byte[] bytearrayinput = Encoding.UTF8.GetBytes(sContent);
            MemoryStream memoStream = new MemoryStream(bytearrayinput);

            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            memoStream.Close();
            fsEncrypted.Close();
        }

        public static void DecryptFile(String sInputFilename, String sOutputFilename, String sKey, String sIV, int iPadding, int iMode)
        {
            TripleDESCryptoServiceProvider TrDES = new TripleDESCryptoServiceProvider();
            SetMode(TrDES, iPadding, iMode);

            TrDES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            if (iMode != 0)
            {
                TrDES.IV = ASCIIEncoding.ASCII.GetBytes(sIV);
            }

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            //Create a DES decryptor from the TripleDES instance.
            ICryptoTransform desdecrypt = TrDES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //TripleDES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            StreamReader srReader = new StreamReader(cryptostreamDecr);

            fsDecrypted.Write(srReader.ReadToEnd());
            fsDecrypted.Flush();           
            fsDecrypted.Close();
            fsread.Close();

            cryptostreamDecr.Close();
            srReader.Close();
        }

        public static string DecryptData(byte[] bContent,  String sKey, String sIV, int iPadding, int iMode)
        {
            TripleDESCryptoServiceProvider TrDES = new TripleDESCryptoServiceProvider();
            SetMode(TrDES, iPadding, iMode);

            TrDES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            if (iMode != 0)
            {
                TrDES.IV = ASCIIEncoding.ASCII.GetBytes(sIV);
            }

            //Create a memory stream to read the content back.
            MemoryStream memoStream = new MemoryStream(bContent);

            //Create a DES decryptor from the TripleDES instance.
            ICryptoTransform desdecrypt = TrDES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //TripleDES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(memoStream, desdecrypt, CryptoStreamMode.Read);

            //Print the contents of the decrypted file.
            //StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            StreamReader srReader = new StreamReader(cryptostreamDecr);
            string sContent = srReader.ReadToEnd();

            cryptostreamDecr.Close();
            memoStream.Close();
            srReader.Close();

            return sContent;
        }



        #region Encrypt& decrypt private key (Not Working)
        private static string ChinhSua_sKey(string sKey)
        {
            string insertString = "motconvitxoerahaicaicanh";
            char[] theNewKey = new char[24];
            if (sKey.Length < 24)
            {
                for (int i = 0; i < sKey.Length; i++)
                {
                    theNewKey[i] = sKey[i];
                }

                for (int j = sKey.Length; j < 24; j++)
                {
                    theNewKey[j] = insertString[j];
                }
            }
            else
            {
                for (int i = 0; i < 24; i++)
                {
                    theNewKey[i] = sKey[i];
                }
            }
            string result = new string(theNewKey);
            return result;
        }

        public static string EncryptPrivateKey(String PrivateKey, String sKey)
        {
            byte[] byteChar = System.Text.Encoding.UTF8.GetBytes(PrivateKey);
            Stream inputStream = new MemoryStream(byteChar);
            Stream outputSTream = new MemoryStream();
            TripleDESCryptoServiceProvider TrDES = new TripleDESCryptoServiceProvider();
            SetMode(TrDES, 2, 0);

            ///////
            ///// TRiple DES: khóa = 192bit = 24 kí tự
            //!>>>>Quan trọng: về viết thêm 1 hàm thêm họăc bỏ bớt vào sau password 1 chuỗi kí tự xác định trước (ví dụ aaaaaaa);
            /////

            String theNewKey = ChinhSua_sKey(sKey);
            TrDES.Key = ASCIIEncoding.ASCII.GetBytes(theNewKey);


            ICryptoTransform desencrypt = TrDES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(outputSTream, desencrypt, CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[PrivateKey.Length];
            inputStream.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            inputStream.Close();
            outputSTream.Close();
            string EncryptedPrivatekey = Convert.ToBase64String(bytearrayinput);
            return EncryptedPrivatekey;
        }

        public static string DecryptPrivatekey(String EncryptedPrivateKey, String sKey)
        {
            // byte[] byteChar = Convert.FromBase64String(EncryptedPrivateKey);
            byte[] byteChar = Convert.FromBase64String(EncryptedPrivateKey);
            Stream inputStream = new MemoryStream();
            inputStream.Read(byteChar, 0, byteChar.Length);
            TripleDESCryptoServiceProvider TrDES = new TripleDESCryptoServiceProvider();
            SetMode(TrDES, 2, 0);
            //Chỉnh sửa Key
            String theNewKey = ChinhSua_sKey(sKey);
            TrDES.Key = ASCIIEncoding.ASCII.GetBytes(theNewKey);
            //Create a DES decryptor from the TripleDES instance.
            ICryptoTransform desdecrypt = TrDES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //TripleDES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(inputStream, desdecrypt, CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamReader srReader = new StreamReader(cryptostreamDecr);
            string DecryptedPrivateKey = srReader.ReadToEnd();
            return DecryptedPrivateKey;
        } 
        #endregion
    }
}
