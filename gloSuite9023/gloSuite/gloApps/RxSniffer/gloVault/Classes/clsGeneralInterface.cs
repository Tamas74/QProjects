using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace gloVault.Classes
{
    public class clsGeneralInterface
    {

        public static void UpdateLog(string strLogMessage)
        {
            try
            {
                System.IO.StreamWriter objFile = new System.IO.StreamWriter(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "gloVaultLog.txt"), true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "\t" + strLogMessage);
                objFile.Close();
                objFile = null;
            }
            catch (Exception)
            {

            }
        }

        public static string GetFileName(string strAppPath)
        {
            string _NewDocumentName = "";

            string _Extension = ".xml";
            DateTime _dtCurrentDateTime = System.DateTime.Now;

            int i = 0;

            _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + _Extension;
            while (File.Exists(strAppPath + "\\" + _NewDocumentName) == true)
            {
                i = i + 1;
                _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + "-" + i + _Extension;
            }
            return strAppPath + _NewDocumentName;
        }
       

        public static byte[] ConvertFiletoBinary(string strFileName)
        {
            //const int CHUNK_SIZE = 1024 * 8; //8K write buffer.

            if (File.Exists(strFileName))
            {
                FileStream oFile;
                BinaryReader oReader;

                //'Please uncomment the following line of code to read the file, even the file is in use by same or another process 
                // oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous);

                //'To read the file only when it is not in use by any process 
                oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read);

                using (oReader = new BinaryReader(oFile))
                {

                    byte[] bytesRead = new byte[oReader.BaseStream.Length];
                    oReader.Read(bytesRead, 0, bytesRead.Length);
                    oFile.Close();
                    oReader.Close();
                    return bytesRead;
                }
            }
            else
            {
                return null;
            }
        }

        public static String ConvertBinarytoFile(byte[] cntFromDB, String strFileName)
        {
            if (cntFromDB != null)
            {
                MemoryStream stream = new MemoryStream(cntFromDB);
                FileStream oFile = new FileStream(strFileName, System.IO.FileMode.Create);
                stream.WriteTo(oFile);
                oFile.Close();
                return strFileName;

            }
            else
            {
                return "";
            }

        }

        public static void DeleteFile(string strPath)
        {
            if (File.Exists(strPath))
            {
                File.Delete(strPath);
            }
        }

    }
}
