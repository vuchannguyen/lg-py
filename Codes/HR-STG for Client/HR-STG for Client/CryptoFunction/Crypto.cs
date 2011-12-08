using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CryptoFunction
{
    class Crypto
    {
        private static string sPublicKey_Default = "<RSAKeyValue><Modulus>uLbsZhYb2qxCo1zKWu86XjLREhyipppw4wrJXHyo5PGZnvBXMvdpXVOsVFpLy76/uJRy2CP0opHpY0bzWWoSwvGIPYJ9J5uDGABdTdbEd68+Ho0LwGxUcSTVLCVzgThs2t+T0LjH+xGcx7+wbBWvHdnRPcqhe4Nzxx7f6IN3mAk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static int iPublicKeyLength_Default = 1024;

        private static string sPrivateKey_Default = "<RSAKeyValue><Modulus>uLbsZhYb2qxCo1zKWu86XjLREhyipppw4wrJXHyo5PGZnvBXMvdpXVOsVFpLy76/uJRy2CP0opHpY0bzWWoSwvGIPYJ9J5uDGABdTdbEd68+Ho0LwGxUcSTVLCVzgThs2t+T0LjH+xGcx7+wbBWvHdnRPcqhe4Nzxx7f6IN3mAk=</Modulus><Exponent>AQAB</Exponent><P>6VfYshhQgue3nWPBh3iApZBqMjXzB5tF2a2k5RYrQW1/k8H0w66hCyobQRuL/rmFvXrmaSAQu/tTNrRf8DHgdQ==</P><Q>yqZT31QalViDJUOICcENvwpSXzoay1s3N37H/nYecm52gn1kqP45qAmCybs5YlmrTaMpIrFmJhDhJJ3xriKmxQ==</Q><DP>bXA++KBAunSVAGM19CSHKZ+Gvw/gcNPZOdOf/7WbCe+iIxmXg2NWspbH+4xA53H5kLmgcPOSBahFCeYlDNFHtQ==</DP><DQ>FmyZU1knJ+eHe5QhdZLbRoyJ2OfBF0ecsJNi5hGEBP2cN4xQmzKmhqWmx1PkYbGydwDbCG/A8e1kPH0NxUsoUQ==</DQ><InverseQ>T+YqPodPVA8Zcx2T2YeBTLYa7+0MCigYWslRSvkyOLiEmQH3BV3NVh405h7k9k+KFTVQTxYtHig2B04UdqDRmQ==</InverseQ><D>kvvMhTeWlp14sXIG+/FdatPZFiZ/Lz+6EJDmxpfT9cpiydzKJp5F06Pff4gSxGBXQ1OtR1zlL/AX3Y352u7TWb3UfcK15Kw2qsKOcju8XAOumyuSK75qyuxR3U9d5AmzGrsyYXPIKiu2Vx+4CjWx1fBtGKGW+fhN6LV/l9Fh4+E=</D></RSAKeyValue>";

        //Hàm mã hoá File
        public static bool EncryptFile(string sInputFilename, string sOutputFilename)
        {
            MyFileCryptography MyFileCrypto = new MyFileCryptography();
            //Set Algorithms
            MyFileCrypto.SetAlgorithms("Triple DES");
            //Generate key
            MyFileCrypto.GenerateSecretKey();

            //Encrypt File;
            try
            {
                MyFileCrypto.EncryptFilewithSecretKey(sInputFilename, sOutputFilename, 1, 0); //1,0 = ECB mode;
            }
            catch (System.Exception ex)
            {
                return false;
            }

            try
            {
                MyFileCrypto.EncryptSecretKeyWithRSAAlgo_DirectPublicKey(sPublicKey_Default, iPublicKeyLength_Default);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            try
            {
                //Add key vào File đã mã hoá
                MyFileCrypto.AddSecretKeytoFile(sOutputFilename);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        //Hàm mã hoá Data
        public static bool EncryptData(string sContent, string sOutputFilename)
        {
            MyFileCryptography MyFileCrypto = new MyFileCryptography();
            //Set Algorithms
            MyFileCrypto.SetAlgorithms("Triple DES");
            //Generate key
            MyFileCrypto.GenerateSecretKey();

            //Encrypt File;
            try
            {
                MyFileCrypto.EncryptDatawithSecretKey(sContent, sOutputFilename, 1, 0); //1,0 = ECB mode;
            }
            catch (System.Exception ex)
            {
                return false;
            }

            try
            {
                MyFileCrypto.EncryptSecretKeyWithRSAAlgo_DirectPublicKey(sPublicKey_Default, iPublicKeyLength_Default);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            try
            {
                //Add key vào File đã mã hoá
                MyFileCrypto.AddSecretKeytoFile(sOutputFilename);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        

        //Hàm giải mã File
        public static bool DecryptFile(string sInputFilename, string sOutputFilename)
        {
            MyFileCryptography MyFileCrypto = new MyFileCryptography();
            //Set Algorithms
            MyFileCrypto.SetAlgorithms("Triple DES");

            try
            {
                //Đọc Key từ File ra và gán vào trong object MyFileCrypto.Secretkey
                MyFileCrypto.ReadSecretKeyfromFile(sInputFilename);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            try
            {
                //Giải mã Secret key với Private Key
                MyFileCrypto.DecryptSecretKeywithRSAAlgorithm(sPrivateKey_Default, 1024);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            try
            {
                //Tạo 1 file Temp chứ nội dung mã hoá, tạo 1 string chứa đường dẫn File Temp này
                string tempInput = MyFileCrypto.ReadandCreateTempCryptoFile(sInputFilename);

                //Giải mã File với key vừa đc giải mã và Input là file Temp ở trên
                MyFileCrypto.DecryptFilewithSecretKey(tempInput, sOutputFilename, 1, 0); //0 = ECB Mode

                //Sau khi giải mã thì Delete file Temp này đi
                MyFileCrypto.DeleteTempCryptoFile(tempInput);
            }
            catch (System.Exception ex)
            {
                return false;

            }

            return true;
        }

        //Hàm giải mã Data
        public static string DecryptData(string sInputFilename)
        {
            MyFileCryptography MyFileCrypto = new MyFileCryptography();
            //Set Algorithms
            MyFileCrypto.SetAlgorithms("Triple DES");

            try
            {
                //Đọc Key từ File ra và gán vào trong object MyFileCrypto.Secretkey
                MyFileCrypto.ReadSecretKeyfromFile(sInputFilename);
            }
            catch (System.Exception ex)
            {
                return null;
            }

            try
            {
                //Giải mã Secret key với Private Key
                MyFileCrypto.DecryptSecretKeywithRSAAlgorithm(sPrivateKey_Default, 1024);
            }
            catch (System.Exception ex)
            {
                return null;
            }

            try
            {
                StreamReader srReader = new StreamReader(sInputFilename);
                string sKey = srReader.ReadLine();
                srReader.Close();

                List<byte> lContent = new List<byte>();
                FileStream fsReadContent = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
                long lContentLength = fsReadContent.Length - sKey.Length - 2; //lay do dai content

                byte[] bContent = new byte[lContentLength];
                fsReadContent.Seek(sKey.Length + 2, SeekOrigin.Begin); //chuyen den vi tri ket thuc key + 2 byte xuong dong
                fsReadContent.Read(bContent, 0, bContent.Length); //lay noi dung de giai ma
                fsReadContent.Close();

                //Giải mã File với key vừa đc giải mã và Input là file Temp ở trên
                return MyFileCrypto.DecryptDatawithSecretKey(bContent,  1, 0); //0 = ECB Mode
            }
            catch (System.Exception ex)
            {
                return null;

            }

            return null;
        }
    }
}
