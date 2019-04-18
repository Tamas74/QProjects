using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace gloSecurity
{
    public static class gloDataHashing
    {
        private static byte[] ConvertStringToByteArray(string data)
        {
            return (new System.Text.UnicodeEncoding()).GetBytes(data);
        }

        private static System.IO.FileStream GetFileStream(string pathName)
        {
            return (new System.IO.FileStream(pathName, System.IO.FileMode.Open,
                      System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
        }

        public static string GetSHA1Hash(string pathName,out string AlgorithmType)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue = null ;

            try
            {
                System.IO.FileStream oFileStream = null;
                SHA256Managed oSHA256 = new SHA256Managed();
                oFileStream = GetFileStream(pathName);
                arrbytHashValue = oSHA256.ComputeHash(oFileStream);
                oFileStream.Close();

                strHashData = System.BitConverter.ToString(arrbytHashValue);
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch (System.Exception)
            {
                strResult = null;
            }
            finally
            {
                strHashData = null;
                arrbytHashValue = null;
            }

            AlgorithmType = "SHA256Managed";
            return strResult;
        }

        public static string GetMD5Hash(string pathName)
        {
            string strResult = "";
            string strHashData = "";

            byte[] arrbytHashValue;
            System.IO.FileStream oFileStream = null;

            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                       new System.Security.Cryptography.MD5CryptoServiceProvider();

            try
            {
                oFileStream = GetFileStream(pathName);
                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
                oFileStream.Close();

                strHashData = System.BitConverter.ToString(arrbytHashValue);
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch //(System.Exception ex)
            {
                strResult = null;
            }
            finally
            {
                arrbytHashValue = null;
                oFileStream = null;
                if (oMD5Hasher != null) { oMD5Hasher.Dispose(); oMD5Hasher = null; }
            }

            return strResult;
        }

    }
}
