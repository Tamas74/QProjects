using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace gloICDAnalysis.ClassLib
{
    
    public class ClsEncryption : IDisposable
    {
        private byte[] _key;
        private byte[] _iv = { 18, 52, 86, 120, 144, 171, 205, 239 };

        #region Constructor and Destructor
        public ClsEncryption()
             {
                    
             }
        
            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

        ~ClsEncryption()
            {
                Dispose(false);
            }
        #endregion

         /// <summary>
        /// This method accepts the string to encrypt and encryption key
        /// and returns corresponding Encrypted string 
        /// </summary>
        /// <param name="stringToEncrypt"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public string EncryptToBase64String(string stringToEncrypt, string encryptionKey)
        {
            try
            {
                _key = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 8));
                //convert our input string to a byte array
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                    {
                        // Encrypt the bytearray
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();

                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
               
            }
        }

        public string DecryptFromBase64String(string stringToDecrypt, string decryptionKey)
        {
            try
            {
                 
                _key = Encoding.UTF8.GetBytes(decryptionKey.Substring(0, 8));
                byte[] inputByteArray = inputByteArray = Convert.FromBase64String(stringToDecrypt);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(_key, _iv), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();

                            return Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                
        }
  


    }
}
