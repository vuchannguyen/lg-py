using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CryptoFunction
{
    class MyRSAAlgorithm
    {
        private string _publicKey;

        public string PublicKey
        {
            get { return _publicKey; }
            set { _publicKey = value; }
        }

        private string _privateKey;

        public string PrivateKey
        {
            get { return _privateKey; }
            set { _privateKey = value; }
        }

        public RSACryptoServiceProvider theProvider;
        private int _keyLength = 1024;



        public MyRSAAlgorithm()
        {
            theProvider = new RSACryptoServiceProvider();
            this._privateKey = string.Empty;
            this._publicKey = string.Empty;
        }

        public void SetKeyLength(int iKeyLength)
        {
            _keyLength = iKeyLength;
            theProvider = new RSACryptoServiceProvider(_keyLength);
        }

        public string EncryptData(string data2Encrypt, String sKey)
        {
            theProvider.FromXmlString(sKey);

            byte[] plainbytes = System.Text.Encoding.UTF8.GetBytes(data2Encrypt);
            byte[] cipherbytes = theProvider.Encrypt(plainbytes, false);
            return Convert.ToBase64String(cipherbytes);
        }

        public string DecryptData(string data2Decrypt, String sKey)
        {

            byte[] byteData = Convert.FromBase64String(data2Decrypt);

            theProvider.FromXmlString(sKey);

            byte[] decryptedData = theProvider.Decrypt(byteData, false);
            return System.Text.Encoding.UTF8.GetString(decryptedData);
        }

        public string SavePublicKey(String sOutput)
        {
            try
            {
                FileStream file = new FileStream(sOutput, FileMode.Create);
                string lastKey = _keyLength + "\n" + _publicKey;
                byte[] buffer = Encoding.ASCII.GetBytes(lastKey);
                file.Write(buffer, 0, buffer.Length);
                file.Flush();
                file.Close();
                return _publicKey;
            }
            catch (System.Exception ex)
            {
                return "Cannot create Public key File, please try again!";
            }


        }

        public string SavePrivateKey(String sOutput)
        {
            try
            {
                FileStream file = new FileStream(sOutput, FileMode.Create);
                string lastKey = _keyLength + "\n" + _privateKey;
                byte[] buffer = Encoding.ASCII.GetBytes(lastKey);
                file.Write(buffer, 0, buffer.Length);
                file.Flush();
                file.Close();
                return _privateKey;
            }
            catch (System.Exception ex)
            {
                return "Cannot create Private key File, please try again!";
            }

        }
        public string GenerateKeyPair()
        {
            this._privateKey = string.Empty;
            this._publicKey = string.Empty;

            if (this._privateKey == string.Empty && this._publicKey == string.Empty)
            {
                this._privateKey = theProvider.ToXmlString(true);
                this._publicKey = theProvider.ToXmlString(false);
                return "Generating Key pair complete!";
            }
            return "Unable to generate key pair!";
        }
    }
}
