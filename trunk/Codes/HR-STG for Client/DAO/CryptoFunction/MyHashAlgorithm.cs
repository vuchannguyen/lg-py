using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CryptoFunction
{
    class MyHashAlgorithm
    {
        public string ComputeHash(byte[] OriginalData,string hashAlgorithm,byte[] saltData)
        {
            // If salt is not specified, generate it on the fly.
            if (saltData == null)
            {
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
            }

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[OriginalData.Length + saltData.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < OriginalData.Length; i++)
                plainTextWithSaltBytes[i] = OriginalData[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltData.Length; i++)
                plainTextWithSaltBytes[OriginalData.Length + i] = saltData[i];

            HashAlgorithm hash;

            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Initialize appropriate hashing algorithm class.
            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;
                case "MD5":
                    hash = new MD5CryptoServiceProvider();
                    break;
                default:
                    hash = new SHA1Managed();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltData.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltData.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltData[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public bool VerifyHash(byte[] OriginalData, string hashAlgorithm, byte[] hashWithSaltBytes)
        {            
            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;
            
            // Make sure that hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";
            
            // Size of hash is based on the specified algorithm.
            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                case "MD5":
                    hashSizeInBits = 128;
                    break;
                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - 
                                        hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i=0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString = 
                        ComputeHash(OriginalData, hashAlgorithm, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);
            return (hashValue == expectedHashString);
        }
        public string GenerateSaltString()
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
            string salt = Convert.ToBase64String(saltData);
            return salt;
        }

        /*
        public string VerifyHashFile(string ioriginalFile,string ihashedFile, string hashAlgorithm)
        {
            if (ioriginalFile == string.Empty || hashAlgorithm == string.Empty)
            {
                return "Error Verifying Hash File!\n Input File or Hash Algorithms is Empty!";
            }
            else
            {
                //Read the original file and read it to a Byte[] data
                FileStream reader = new FileStream(ioriginalFile, FileMode.Open);
                byte[] hashOriginalData = new byte[reader.Length];
                reader.Read(hashOriginalData, 0, hashOriginalData.Length);
                reader.Close();

                //read the hash string in the Hashed File to a Byte[] data
                FileStream hashedReader = new FileStream(ihashedFile, FileMode.Open);
                byte[] hashedDatatoTest = new byte[hashedReader.Length];
                hashedReader.Read(hashedDatatoTest, 0, hashedDatatoTest.Length);
                hashedReader.Close();

                try
                {
                    bool Result = VerifyHash(hashOriginalData, hashAlgorithm, hashedDatatoTest);
                    if(Result)
                    {
                        return "These Files are matched!";
                    }
                    else if (!Result)
                    {
                        return "These Files are NOT matched!";
                    }
                }
                catch (System.Exception ex)
                {
                    return "Error Verifying Hash File, Exception occurred!";
                }
                return "Unknown Error!";
            }

        }
        public bool HashandSaveFile(string iFile, string hashAlgorithm)
        {
            if (iFile == string.Empty || hashAlgorithm == string.Empty)
            {
                return false;
            }
            else
            {
                //Read the file and read it to a Byte[] data
                FileStream reader = new FileStream(iFile, FileMode.Open);
                byte[] hashOriginalData = new byte[reader.Length];
                reader.Read(hashOriginalData, 0, hashOriginalData.Length);
                reader.Close();
                try
                {
                    //Do the hashing
                    string hashedData = ComputeHash(hashOriginalData, hashAlgorithm, null);

                    //outFileName:                
                    //do the Writing to filenam.algorithms in the same folder
                    string oFile = GetValue(iFile, hashAlgorithm);
                    FileStream writer;
                    writer = new FileStream(oFile, FileMode.Create);
                    byte[] encrypted = Convert.FromBase64String(hashedData);
                    writer.Write(encrypted, 0, encrypted.Length);
                    writer.Flush();
                    writer.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        private string GetValue(string iFile,string hashAlgorithm)
        {
            //Hash values
            string path = iFile;
            for (int i = iFile.Length - 1; i > 0; i--)
            {
                if (iFile[i] == '\\' || iFile[i] == '/')
                {
                    path = iFile.Substring(0, i + 1);
                    break;
                }
            }
            string fileName = iFile.Replace(path, "");
            string oFile = iFile + "." + hashAlgorithm.ToLower();
            return oFile;
        }
         */
    }
}
