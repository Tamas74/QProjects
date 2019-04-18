using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace gloGlobal.ChangePassword
{
    class clsEncryption:IDisposable
    {
        private byte[] key = {
	
	// we are going to pass in the key portion in our method calls
};
        private byte[] IV = {
	0x12,
	0x34,
	0x56,
	0x78,
	0x90,
	0xab,
	0xcd,
	0xef

};
        public string DecryptFromBase64String(string stringToDecrypt, string sEncryptionKey)
        {
            // Note: The DES CryptoService only accepts certain key byte lengths
            // We are going to make things easy by insisting on an 8 byte legal key length
            MemoryStream ms = null;
            CryptoStream cs = null;
            DESCryptoServiceProvider des = null;

            try
            {

                if (!string.IsNullOrEmpty(stringToDecrypt))
                {
                    key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    des = new DESCryptoServiceProvider();
                    // we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
                    byte[] inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    // now decrypt the regular string
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());

                }
                else
                {
                    return "";
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (((ms == null) == false))
                {
                    ms.Dispose();
                    ms = null;
                }
                if (((cs == null) == false))
                {
                    cs.Dispose();
                    cs = null;
                }
                if (((des == null) == false))
                {
                    des.Dispose();
                    des = null;
                }
            }
        }

        public string EncryptToBase64String(string stringToEncrypt, string SEncryptionKey)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            DESCryptoServiceProvider des = null;

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8));
                des = new DESCryptoServiceProvider();
                // convert our input string to a byte array
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                //now encrypt the bytearray
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                // now return the byte array as a "safe for XMLDOM" Base64 String
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (((ms == null) == false))
                {
                    ms.Dispose();
                    ms = null;
                }
                if (((cs == null) == false))
                {
                    cs.Dispose();
                    cs = null;
                }
                if (((des == null) == false))
                {
                    des.Dispose();
                    des = null;
                }
            }
        }


        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if ((key != null))
                    {
                        if (key.Length > 0)
                        {
                            Array.Clear(key, 0, key.Length);
                        }
                    }
                    key = null;
                    if ((IV != null))
                    {
                        if (IV.Length > 0)
                        {
                            Array.Clear(IV, 0, IV.Length);
                        }
                    }
                    IV = null;
                }
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            // TODO: set large fields to null.

            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
