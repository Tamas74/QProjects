using System;
using System.Collections.Generic;
using System.Text;
//using java.util;
//using java.util.zip;
//using java.io;
using Ionic.Zip;
#region "Old Logic"

//namespace UnZipFile
//{
//    public class clsExtractFile
//    {
//        public static string ExtractZipFile(string zipfilename)
//        {
//            String reqFile=null;
//            ZipFile zipfile = new ZipFile(zipfilename);
//            List<ZipEntry> entries = GetZippedItems(zipfile);
//            try
//            {
                
//                foreach (ZipEntry entry in entries)
//                {
//                    if (!entry.isDirectory())
//                    {
//                        InputStream s = zipfile.getInputStream(entry);
//                        try
//                        {
//                            string fname = System.IO.Path.GetFileName(entry.getName());
//                            // string tempFile = System.IO.Path.GetFileName(destination);
//                            //if (fname == tempFile)
//                            // {
//                            string dir = System.IO.Path.GetDirectoryName(entry.getName());
//                            string newpath = System.IO.Path.GetDirectoryName(zipfilename) + @"\" + dir;
//                            System.IO.Directory.CreateDirectory(newpath);
//                            reqFile = System.IO.Path.Combine(newpath, fname);
//                            FileOutputStream dest = new FileOutputStream(reqFile);
//                            try
//                            {
//                                CopyStream(s, dest);
                               
//                            }
//                            catch (Exception ex)
//                            {
//                                string msg = ex.Message;
//                                reqFile = null;
//                            }

//                            finally
//                            {
//                                dest.close();
//                            }
//                            //}

//                        }
//                        catch (Exception ex)
//                        {
//                            string msg = ex.Message;
//                            reqFile = null;
//                        }

//                        finally
//                        {
//                            s.close();
//                        }
//                    }
//                    else
//                    {
//                        reqFile = null;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                string msg = ex.Message;
//                reqFile = null;
//            }
//            finally
//            {
//                zipfile.close();
//            }
//            return reqFile;
//        }

//        private static void CopyStream(InputStream source, OutputStream destination)
//        {
//            sbyte[] buffer = new sbyte[8000];
//            int data;
//            while (true)
//            {
//                try
//                {
//                    data = source.read(buffer, 0, buffer.Length);
//                    if (data > 0)
//                    {
//                        destination.write(buffer, 0, data);
//                    }
//                    else
//                    {
//                        return;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    string msg = ex.Message;
//                }
//            }
//        }

//        private static List<ZipEntry> GetZippedItems(ZipFile file)
//        {
//            List<ZipEntry> entries = new List<ZipEntry>();
//            Enumeration e = file.entries();
//            while (true)
//            {
//                if (e.hasMoreElements())
//                {
//                    ZipEntry entry = (ZipEntry)e.nextElement();
//                    entries.Add(entry);
//                }
//                else { break; }
//            }
//            return entries;
//        }
//    }
//}
#endregion "End "

namespace UnZipFile
{
    public class clsExtractFile
    {
        public static string ExtractZipFile(string zipfilename)
        {
            String reqFile = null;
            using (ZipFile zipfile = new ZipFile(zipfilename))
            {
                List<ZipEntry> entries = GetZippedItems(zipfile);
                try
                {

                    foreach (ZipEntry entry in entries)
                    {
                        //if (!entry.isDirectory())
                        //{
                        //    InputStream s = zipfile.getInputStream(entry);
                        try
                        {
                            string fname = System.IO.Path.GetFileName(entry.FileName.ToString());
                            // string tempFile = System.IO.Path.GetFileName(destination);
                            //if (fname == tempFile)
                            // {
                            string dir = System.IO.Path.GetDirectoryName(entry.FileName.ToString());
                            string newpath = System.IO.Path.GetDirectoryName(zipfilename) + @"\" + dir;
                            System.IO.Directory.CreateDirectory(newpath);
                            reqFile = System.IO.Path.Combine(newpath, fname);
                            // FileOutputStream dest = new FileOutputStream(reqFile);
                            //try
                            //{
                            //    CopyStream(s, dest);

                            //}
                            //catch (Exception ex)
                            //{
                            //    string msg = ex.Message;
                            //    reqFile = null;
                            //}

                            //finally
                            //{
                            //    dest.close();
                            //}


                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                            reqFile = null;
                        }

                        //finally
                        //{
                        //    s.close();
                        //}
                        //}
                        //else
                        //{
                        //    reqFile = null;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    reqFile = null;
                }
                //finally
                //{
                //    zipfile.close();
                //}
            }
            return reqFile;
        }

      
        private static List<ZipEntry> GetZippedItems(ZipFile file)
        {
            List<ZipEntry> entries = new List<ZipEntry>();

            string ExistingZipFile = file.Name.ToString();// @"C:\ZIPFILES\RHDSetup.zip";
            //string dir = System.IO.Path.GetDirectoryName(entry.getName());
            using (ZipFile zip = ZipFile.Read(ExistingZipFile))
            {
                System.String TargetDirectory = System.IO.Path.GetDirectoryName(ExistingZipFile);
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