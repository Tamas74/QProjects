using System.Diagnostics;
using System;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;



namespace gloAUSLibrary
{
    public class clsEncryption : IDisposable
    {
        // Use DES CryptoService with Private key pair
        private byte[] key = new byte[] { }; // we are going to pass in the key portion in our method calls
        private byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #region Constructor and Destructor
       public clsEncryption()
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

            ~clsEncryption()
            {
                Dispose(false);
            }
        #endregion
     

        //////public string DecryptFromBase64String(string stringToDecrypt, string sEncryptionKey)
        //////{
        //////    byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        //////    // Note: The DES CryptoService only accepts certain key byte lengths
        //////    // We are going to make things easy by insisting on an 8 byte legal key length

        //////    try
        //////    {
        //////        key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
        //////        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        //////        {
        //////            // we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
        //////            inputByteArray = Convert.FromBase64String(stringToDecrypt);
        //////            // now decrypt the regular string
        //////            using (MemoryStream ms = new MemoryStream())
        //////            {
        //////                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write))
        //////                {
        //////                    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //////                    cs.FlushFinalBlock();
        //////                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        //////                    return encoding.GetString(ms.ToArray());
        //////                }
        //////            }
        //////        }
        //////    }
        //////    catch (Exception e)
        //////    {
        //////        return e.Message;
        //////    }
        //////}

        //////public string EncryptToBase64String(string stringToEncrypt, string SEncryptionKey)
        //////{
        //////    try
        //////    {
        //////        key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8));
        //////        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        //////        {
        //////            // convert our input string to a byte array
        //////            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
        //////            //now encrypt the bytearray
        //////            using (MemoryStream ms = new MemoryStream())
        //////            {
        //////                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write))
        //////                {
        //////                    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //////                    cs.FlushFinalBlock();
        //////                    // now return the byte array as a "safe for XMLDOM" Base64 String
        //////                    return Convert.ToBase64String(ms.ToArray());
        //////                }
        //////            }
        //////        }
        //////    }
        //////    catch (Exception e)
        //////    {
        //////        return e.Message;
        //////    }
        //////}

            public string EncryptToBase64String(string stringToEncrypt, string encryptionKey)
            {
                byte[] inputByteArray = null;
                try
                {
                    key = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 8));
                    //convert our input string to a byte array
                    inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                        {
                            // Encrypt the bytearray
                            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write))
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

                   // gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                   // ex = null;
                    return ex.ToString();
                }
                finally
                {
                    inputByteArray = null;
                }
            }

            public string DecryptFromBase64String(string stringToDecrypt, string decryptionKey)
            {
                byte[] inputByteArray = null;
                try
                {
                    inputByteArray = new byte[stringToDecrypt.Length];
                    key = Encoding.UTF8.GetBytes(decryptionKey.Substring(0, 8));
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write))
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
                   // gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                   // ex = null;
                    return ex.ToString();
                }
                finally
                { inputByteArray = null; }

            }
    }
}
