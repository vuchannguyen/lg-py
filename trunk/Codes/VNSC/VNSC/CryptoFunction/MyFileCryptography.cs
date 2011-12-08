using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CryptoFunction
{
    class MyFileCryptography
    {
        #region Members & Properties
        private string m_SecretKey; //Secret key dùng làm pass khi mã hoá File
        private string m_EncryptedSecretKey;

        public string SecretKey
        {
            get { return m_SecretKey; }
            set { m_SecretKey = value; }
        }

        private string m_ChosenAlgorithms;

        public string ChosenAlgorithms
        {
            get { return m_ChosenAlgorithms; }
            set { m_ChosenAlgorithms = value; }
        }

        private string m_IV;

        public string IV
        {
            get { return m_IV; }
            set { m_IV = value; }
        }

        #endregion



        #region Common Functions
        //Set algorithms
        public void SetAlgorithms(string theAlgo)
        {
            ChosenAlgorithms = theAlgo;
        }

        //Generate ra secretey
        public void GenerateSecretKey()
        {
            //Generate secretkey va gan m_scretkey = key moi generate
            byte[] secretkeyBytes;
            int keySize = 0;
            switch (ChosenAlgorithms)
            {

                //cac case cua cac phep ma hoa o day

                //Ma hoa KEy
                //case "RC2": //128 bit nên keysize = 12
                //    keySize = 12; //=12 thì khi generate sẽ ra key = 128 bit
                //    break;
                case "Triple DES":
                    keySize = 16; //=16 thì generate sẽ ra 192bit
                    break;
                default:
                    keySize = 16;
                    break;
            }
            // Allocate a byte array, which will hold the salt.
            secretkeyBytes = new byte[keySize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(secretkeyBytes);
            string theKey = Convert.ToBase64String(secretkeyBytes);
            m_SecretKey = theKey;
        }

        #endregion



        #region Mã hóa File và Secret Key

        //Mã hóa File = secret key vừa generate
        public void EncryptFilewithSecretKey(String sInputFilename, String sOutputFilename, int iPadding, int iMode)
        {
            //
            switch (ChosenAlgorithms)
            {

                //cac case cua cac phep ma hoa o day

                //Ma hoa KEy
                //case "RC2":
                //    MyRC2Algorithm.EncryptFile(sInputFilename, sOutputFilename, m_SecretKey, m_IV, iPadding, iMode);
                //    break;
                case "Triple DES":
                    MyTripleDES.EncryptFile(sInputFilename, sOutputFilename, m_SecretKey, m_IV, iPadding, iMode);
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        //Mã hóa Data = secret key vừa generate
        public void EncryptDatawithSecretKey(String sContent, String sOutputFilename, int iPadding, int iMode)
        {
            //
            switch (ChosenAlgorithms)
            {

                //cac case cua cac phep ma hoa o day

                //Ma hoa KEy
                case "Triple DES":
                    MyTripleDES.EncryptData(sContent, sOutputFilename, m_SecretKey, m_IV, iPadding, iMode);
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        //Mã hóa bất đối xứng secretkey = public key 
        public void EncryptSecretKeywithASAlgorithm(String inputPublicFile)
        {
            //Tạo RSA ra, set chiều dài, đọc file Input Public, gán vào và mã hóa, trả về
            StreamReader srReader = new StreamReader(inputPublicFile);
            int KeyLength = int.Parse(srReader.ReadLine());
            string ReadKey = srReader.ReadLine();
            srReader.Close();
            MyRSAAlgorithm myRSA = new MyRSAAlgorithm();
            myRSA.SetKeyLength(KeyLength);
            m_EncryptedSecretKey = myRSA.EncryptData(m_SecretKey, ReadKey);
        }

        //Mã hóa đối xứng Secretkey = Public Key truyền vào trực tiếp ko đọc File
        public void EncryptSecretKeyWithRSAAlgo_DirectPublicKey(string publicKey, int KeySize)
        {
            MyRSAAlgorithm myRSA = new MyRSAAlgorithm();
            myRSA.SetKeyLength(KeySize);
            m_EncryptedSecretKey = myRSA.EncryptData(m_SecretKey, publicKey);
        }

        public void AddSecretKeyToFile_RC2(string inputFile)
        {
            if (File.Exists(inputFile))
            {
                Encoding theEncoding = System.Text.Encoding.UTF32;

                //StreamReader sr = new StreamReader(inputFile, theEncoding);
                byte[] content = File.ReadAllBytes(inputFile);
                //string content = sr.ReadToEnd();
                //sr.Close();
                StreamWriter sw = new StreamWriter(inputFile, false, theEncoding);
                try
                {
                    sw.Write(m_EncryptedSecretKey + "\r\n");
                    sw.Write(theEncoding.GetChars(content));
                    sw.Close();
                }
                catch
                {
                    sw.Close();
                }
            }
        }

        //Đưa SecretKey vào File của thuật toán 3DES
        public void AddSecretKeytoFile(string inputFile)
        {
            //Add SecretKey to File;
            if (File.Exists(inputFile))
            {
                FileStream fsOpen = new FileStream(inputFile, FileMode.Open, FileAccess.ReadWrite);
                try
                {
                    byte[] content = new byte[fsOpen.Length];
                    fsOpen.Read(content, 0, content.Length);
                    //string sContent = System.Text.Encoding.UTF8.GetString(content); //noi dung bang string

                    //byte[] plainbytesCheck = System.Text.Encoding.UTF8.GetBytes(sContent);
                    byte[] plainbytesKey = new UTF8Encoding(true).GetBytes(m_EncryptedSecretKey + "\r\n");

                    fsOpen.Seek(0, SeekOrigin.Begin);   
                    fsOpen.Write(plainbytesKey, 0, plainbytesKey.Length);
                    fsOpen.Flush();
                    fsOpen.Write(content, 0, content.Length);
                    fsOpen.Flush();
                    fsOpen.Close();
                }
                catch
                {
                    fsOpen.Close();
                }
            }
        }
        #endregion



        #region 3 hàm cho việc đọc và tạo File temp cho việc Giải mã Triple DES

        //Đọc và đưa nội dung mã hoá vào 1 file Temp
        public string ReadandCreateTempCryptoFile(string inputFile)
        {
            StreamReader srReader = new StreamReader(inputFile);
            m_EncryptedSecretKey = srReader.ReadLine();
            srReader.Close();

            List<byte> lContent = new List<byte>();
            FileStream fsReadContent = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
            long lContentLength = fsReadContent.Length - m_EncryptedSecretKey.Length - 2; //lay do dai content

            byte[] bContent = new byte[lContentLength];
            fsReadContent.Seek(m_EncryptedSecretKey.Length + 2, SeekOrigin.Begin); //chuyen den vi tri ket thuc key + 2 byte xuong dong
            fsReadContent.Read(bContent, 0, bContent.Length); //lay noi dung de giai ma
            fsReadContent.Close();

            FileInfo fi = new FileInfo(inputFile);
            string sTempFile = fi.DirectoryName + "temp.txt";
            FileStream fsWrite = new FileStream(sTempFile, FileMode.Create, FileAccess.Write);
            fsWrite.Write(bContent, 0, bContent.Length); //ghi tam ra file temp de read
            fsWrite.Flush();
            fsWrite.Close();
            return sTempFile;
        }

        //Xoá file Temp đã tạo
        public void DeleteTempCryptoFile(string iInput)
        {
            File.Delete(iInput);
        }

        //Đọc SecretKey từ File ra
        public void ReadSecretKeyfromFile(string inputFile)
        {
            StreamReader srReader = new StreamReader(inputFile);
            m_EncryptedSecretKey = srReader.ReadLine();
            srReader.Close();
        }
        #endregion



        #region Việc đọc stream cho phần mã hoá RC2
        private Stream ReadRC2CryptoContent(string iInputFile)
        {
            Encoding theEncoding = System.Text.Encoding.Unicode;

            StreamReader srReader = new StreamReader(iInputFile, theEncoding);
            m_EncryptedSecretKey = srReader.ReadLine();
            string CryptoContent = srReader.ReadToEnd();
            srReader.Close();
            return new MemoryStream(theEncoding.GetBytes(CryptoContent));
        }
        #endregion



        //Giả mã Secret key dùng thuật toán RSA
        public void DecryptSecretKeywithRSAAlgorithm(string PrivateKey, int KeySize)
        {
            MyRSAAlgorithm myRSA = new MyRSAAlgorithm();
            myRSA.SetKeyLength(KeySize);
            string DecryptedSecretKey = myRSA.DecryptData(m_EncryptedSecretKey, PrivateKey);
            m_SecretKey = DecryptedSecretKey;//gán Key đã mã hoá vào m_SecretKey
        }

        public void DecryptFilewithSecretKey(String sInputFilename, String sOutputFilename, int iPadding, int iMode)
        {
            switch (ChosenAlgorithms)
            {
                //cac case cua cac phep ma hoa o day

                //Ma hoa KEy
                //case "RC2":
                //    MyRC2Algorithm.DecryptFile(sInputFilename, sOutputFilename, m_SecretKey, m_IV, iPadding, iMode); 
                //    break;
                case "Triple DES":
                    MyTripleDES.DecryptFile(sInputFilename, sOutputFilename, m_SecretKey, m_IV, iPadding, iMode);
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        public string DecryptDatawithSecretKey(byte[] bContent, int iPadding, int iMode)
        {
            switch (ChosenAlgorithms)
            {
                //cac case cua cac phep ma hoa o day

                //Ma hoa KEy
                //case "RC2":
                //    MyRC2Algorithm.DecryptFile(sInputFilename, sOutputFilename, m_SecretKey, m_IV, iPadding, iMode); 
                //    break;
                case "Triple DES":
                    return MyTripleDES.DecryptData(bContent, m_SecretKey, m_IV, iPadding, iMode);

                default:
                    //do nothing
                    return null;
            }
        }
    }
}
