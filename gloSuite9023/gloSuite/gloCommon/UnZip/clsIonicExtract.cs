using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace UnZipFileIonic
{
    
        public class clsExtractFile
        {
            public static string ExtractZipFile(string zipfilename)
            {
                
                String newpath = String.Empty;
                newpath = System.IO.Path.GetDirectoryName(zipfilename);
                string FinalPath = newpath + "\\" + System.IO.Path.GetFileNameWithoutExtension(zipfilename);
                System.IO.Directory.CreateDirectory(FinalPath);
                using (ZipFile zipfile = new ZipFile(zipfilename))
                {
                    List<ZipEntry> entries = GetZippedItems(zipfile,FinalPath);
              
                   
                }
                return FinalPath;
            }


            private static List<ZipEntry> GetZippedItems(ZipFile file, string TargetDirectory)
            {

                List<ZipEntry> entries = new List<ZipEntry>();

                string ExistingZipFile = file.Name.ToString();// @"C:\ZIPFILES\RHDSetup.zip";

                //string dir = System.IO.Path.GetDirectoryName(entry.getName());
                using (ZipFile zip = ZipFile.Read(ExistingZipFile))
                {
                    //System.String TargetDirectory = System.IO.Path.GetDirectoryName(ExistingZipFile);
                    foreach (ZipEntry e in zip)
                    {
                        e.Extract(TargetDirectory, ExtractExistingFileAction.OverwriteSilently);
                        // string sFilename = e.FileName.ToString();  
                        entries.Add(e);
                    }
                }
                return entries;
            }
        }
    
}
