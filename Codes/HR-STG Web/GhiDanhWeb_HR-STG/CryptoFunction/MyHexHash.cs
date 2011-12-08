using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace CryptoFunction
{
    class MyHexHash
    {
        private static HashAlgorithm hash;

        public string CreateSalt()
        {
            byte[] saltData;
            // Define min and max salt sizes.
            int minSaltSize = 4;
            int maxSaltSize = 8;

            // Generate a random number for the size of the salt.
            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            // Allocate a byte array, which will hold the salt.
            saltData = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(saltData);

            string result = "";
            foreach (Byte b in saltData)
            {
                result += b.ToString("X"); // convert byte to hex
            }
            return result;
        }

        private void GetHashMode(String sMode)
        {
            switch (sMode)
            {
                case "MD5":
                    {
                        hash = new MD5CryptoServiceProvider();
                        break;
                    }
                case "SHA1":
                    {
                        hash = new SHA1CryptoServiceProvider();
                        break;
                    }
                //case "SHA256":
                //    {
                //        hash = new SHA256CryptoServiceProvider();
                //        break;
                //    }
                //case "SHA384":
                //    {
                //        hash = new SHA384CryptoServiceProvider();
                //        break;
                //    }
                //case "SHA512":
                //    {
                //        hash = new SHA512CryptoServiceProvider();
                //        break;
                //    }
            }
        }

        public String HashData(String data2Hash, String sMode, String Salt)
        {
            try
            {
                GetHashMode(sMode);
                byte[] plainbytes = System.Text.Encoding.UTF8.GetBytes(data2Hash);
                byte[] hashedbytes = hash.ComputeHash(plainbytes);

                byte[] saltData = System.Text.Encoding.UTF8.GetBytes(Salt);
                byte[] plainTextWithSaltBytes = new byte[hashedbytes.Length + saltData.Length];

                // Copy plain text bytes into resulting array.
                for (int i = 0; i < hashedbytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = hashedbytes[i];
                }

                // Append salt bytes to the resulting array.
                for (int i = 0; i < saltData.Length; i++)
                {
                    plainTextWithSaltBytes[hashedbytes.Length + i] = saltData[i];
                }

                string result = "";
                foreach (Byte b in plainTextWithSaltBytes)
                {
                    result += b.ToString("X"); // convert byte to hex
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
