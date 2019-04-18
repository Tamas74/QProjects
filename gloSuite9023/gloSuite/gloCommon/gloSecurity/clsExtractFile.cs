using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using Ionic.Zip;
using Ionic.Zlib;
using gloAuditTrail;

namespace gloSecurity
{

    public static class gloZip
    {
        /// <summary>
        /// The function takes a file path and the extracting file name as input
        /// and creates a zip file of the inputted file at the given path.
        /// </summary>
        /// <param name="SourceFilePath">The file path which is to be zipped.</param>
        /// <param name="DestinationFileName">The output file name.</param>
        /// <returns>
        /// Returns void
        /// </returns>
        /// <exception cref="Ionic.Zip.ZipException">
        /// When performing zip to the file if any problem accours the method will
        /// throw zip exception.
        /// </exception>
        public static void ZipMyFile(string SourceFilePath, string DestinationFileName)
        {
            try
            {
                FileInfo _fileInfo = new FileInfo(DestinationFileName);

                using (ZipFile zipFile = new ZipFile())
                {
                    zipFile.CompressionLevel = CompressionLevel.None;
                    zipFile.AddFile(SourceFilePath,"");
                    zipFile.Save(_fileInfo.FullName);
                }
                _fileInfo = null;
            }
            catch (ZipException zipEx)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(zipEx.ToString(), true); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }
    }

}
