using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using IWshRuntimeLibrary;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Management;

namespace gloAUSLibrary
{
    public class clsValidations
    {
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        static extern int GetDiskFreeSpaceEx(
         string lpDirectoryName,
         out ulong lpFreeBytesAvailable,
         out ulong lpTotalNumberOfBytes,
         out ulong lpTotalNumberOfFreeBytes);
        public enum GLOSTATUS { SUCCESS = 1, FAILED = 0, EXISTS = 2, BEGINNING = 3, RESTORE = 4, PARTIALLYSUCCESS = 5, NORECORDS = 6 , SKIP = 7 };
        public enum MACHINESTATUS { BIT32 = 1, BIT64 = 2 };
        public enum INSTALLATION
        {
            STARTINSTALLATION = 0,
            BASEDATABASE = 1, ARCHIVEDATABASE = 2, SERVICESDATABASE = 3, CODEWIZARDDATABASE = 4,DMS=5,MEDISPAN=6,SNOWMED=7,RXNORM=8,HL7=9,DEVICE=10, EMRADMIN = 11, PMADMIN = 12, CM = 13,
            VISUALjSHARP = 14, ARCHIVEDBSCRIPTS = 15, APPLICATIONDBSCRIPTS = 16, SSRSREPROTS = 17, SERVICES = 18,DMSMiGRATION=19,BILLINGMIGRATION,DATABASEINDEXING,POSTAPPLICATIONDBSCRIPTS,
            ABORTINSTALLATION = 99
        };
        public static string strDomainName = Environment.UserDomainName.ToString();
        
        public static  string Removeprerequiistes(string Path)
        {
            try
            {
                
                while (Path.EndsWith("\\"))
                { Path = Path.TrimEnd('\\'); }

                if (Path.ToLower().EndsWith("prerequisites"))
                {
                    Path = Path.Substring(0, Path.Length - 14);
                }
                else
                {
                }
            }
            catch (Exception)
            {

            }
            finally
            {
            }
            return Path.ToString();
           

        }
        
        public static bool CreateFolder(string strPath)
        {
            bool success = false;
            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                    
                    success = true;
                }
                else
                {
                    success = true;
                }
            }
            catch (System.AccessViolationException)
            {

            }
            catch (System.ArgumentException)
            {

            }
            catch (System.Exception)
            {

            }
            return success;
        }
        
        public static  string TrimPathVariables(string strPath)
        {
            while (strPath.EndsWith("\\"))
            { strPath = strPath.TrimEnd('\\'); }
            return strPath;
        }
        
        //Services Changes

        public static int GetOsVersion()
        {
            int osVer;
            OperatingSystem os = Environment.OSVersion;
            osVer = os.Version.Major;
            if (osVer < 6)
            {
                osVer = 0; //windows xp
            }
            else
            {
                osVer = 1;//windows 7 and windows vista
            }

            return osVer;
        }
        
        public static int CheckMachineStatus()
        {
            int _type = 0;
            string strProcArchi = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            bool Proc64running32 = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            //MessageBox.Show(strProcArchi);
            if ((strProcArchi.IndexOf("64") > 0) || (!Proc64running32))
            {
                
                _type = 1; //64 bit machine
                //  MessageBox.Show("64bit machine");
            }
            else
            {
                _type = 0; //32 bit machine
                //MessageBox.Show("32bit machine");
            }
            return _type;
        }
        
        public static string GetProgramFilesPath()
        {
            int _type = CheckMachineStatus();
            string strPathtoProgramFiles = "";
            if (_type == 1) //64 bit machine
            {
                strPathtoProgramFiles = Environment.GetEnvironmentVariable("PROGRAMFILES(x86)");
            }
            else if (_type == 0) //32 bit machine
            {
                strPathtoProgramFiles = Environment.GetEnvironmentVariable("PROGRAMFILES");
            }
            return strPathtoProgramFiles;
        }
        
        public static void CreateDesktoIcons(string IconName, string IconTargetPath, string IconLocation, int type,string startinpath,string strProductIconToolTip ="")
        {
            #region "old Code"
            //try
            //{
            //    WshShellClass WshShell;
            //    // Create a new instance of WshShellClass
            //    WshShell = new WshShellClass();
            //    // Create the shortcut
            //    IWshRuntimeLibrary.IWshShortcut MyShortcut;
            //    string strAllUsersPath = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
            //    //strAllUsersPath = strAllUsersPath + "\\Desktop\\gloPM.lnk";
            //    if (System.IO.File.Exists(IconLocation))
            //    {
            //        if (type == 0)
            //        {
            //            strAllUsersPath = strAllUsersPath + "\\Desktop\\"+IconName+"";
            //        }
            //        else
            //        {
            //            strAllUsersPath = strAllUsersPath + "\\Start Menu\\Programs\\"+IconName+"";
            //        }
            //        // Choose the path for the shortcut
            //        //MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(@"C:\MyShortcut.lnk");
            //        MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(strAllUsersPath);
            //        // Where the shortcut should point to
            //        MyShortcut.TargetPath = IconTargetPath;
            //        // Description for the shortcut
            //        MyShortcut.WorkingDirectory = startinpath;
            //        MyShortcut.Description = "Launch";
            //        // Location for the shortcut's icon
            //        //MyShortcut.IconLocation = Application.StartupPath + @"\gloPMXP.ico";
            //        MyShortcut.IconLocation = IconLocation;
            //        // Create the shortcut at the given path
            //        MyShortcut.Save();
            //    }
            //}
            //catch (System.ArgumentException)
            //{
            //}
            //catch (System.ApplicationException)
            //{
            //}
            //catch (Exception)
            //{
            //}
            #endregion "old Code"
            try
            {
                WshShellClass WshShell;
                // Create a new instance of WshShellClass
                WshShell = new WshShellClass();
                // Create the shortcut
                IWshRuntimeLibrary.IWshShortcut MyShortcut;
                //  string strAllUsersPath = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
                //strAllUsersPath = strAllUsersPath + "\\Desktop\\gloPM.lnk";
                int osVer;
                string IconPathLoaction = "";
                OperatingSystem os = Environment.OSVersion;

                osVer = os.Version.Major;

                if (System.IO.File.Exists(IconLocation))
                {
                    if (osVer < 6)
                    {
                        if (type == 0)//desktop path
                        {
                            IconPathLoaction = clsIcons.GetAllUsersDesktopFolderPath();
                        }
                        else ///all programs path
                        {
                            //MessageBox.Show(strAlluserDesktoppath.ToString());
                            IconPathLoaction = clsIcons.GetallprogramsMenuPath();
                            // MessageBox.Show(strallprogramsPath.ToString());
                        }

                    }
                    else
                    {
                        if (type == 0)//desktop path
                        {
                            IconPathLoaction = clsIcons.GetSharedDesktop();
                        }
                        else  ///all programs path
                        {
                            //MessageBox.Show(strSharedDesktoppath.ToString());
                            //GetSharedAllProgramsPath
                            // IconPathLoaction = gloSuiteClient.Classes.clsIcons.GetSharedProgramsPath();
                            IconPathLoaction = clsIcons.GetSharedAllProgramsPath();
                        }

                    }
                    if (!String.IsNullOrEmpty(IconPathLoaction))
                    {
                        IconPathLoaction = IconPathLoaction + "\\" + IconName + "";
                    }

                    if (System.IO.File.Exists(IconPathLoaction))
                    {
                        // Choose the path for the shortcut
                        //MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(@"C:\MyShortcut.lnk");
                        //MessageBox.Show("alredy icon exists");
                    }
                    else
                    {
                        //MessageBox.Show("icon is creating");
                        MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(IconPathLoaction);
                        MyShortcut.TargetPath = IconTargetPath;
                        MyShortcut.WorkingDirectory = startinpath;
                        // Description for the shortcut
                        //MyShortcut.Description = "Location:gloEMRAdmin ("+startinpath+")";
                        MyShortcut.Description = strProductIconToolTip;
                        // Location for the shortcut's icon
                        //MyShortcut.IconLocation = Application.StartupPath + @"\gloPMXP.ico";
                        MyShortcut.IconLocation = IconLocation;
                        // Create the shortcut at the given path
                        MyShortcut.Save();

                    }
                    // Where the shortcut should point to

                }
            }
            catch (System.ArgumentException)
            {
            }
            catch (System.ApplicationException)
            {
            }

            catch (Exception)
            {
            }

        }

        public static void Deleteshortcut(string IconName, int _type)
        {
            //find the machine status
            try
            {
                OperatingSystem os = Environment.OSVersion;
                int osVer;
                osVer = os.Version.Major;
                string IconPathLoaction = string.Empty;
                if (osVer < 6) //windows xp
                {
                    if (_type == 0)//desktop
                    {
                        IconPathLoaction = clsIcons.GetAllUsersDesktopFolderPath();
                    }
                    else //all programs
                    {
                        IconPathLoaction = clsIcons.GetallprogramsMenuPath();
                    }
                }
                else //windows 7 vista or any server os
                {
                    if (_type == 0)//desktop
                    {
                        IconPathLoaction = clsIcons.GetSharedDesktop();

                    }
                    else  //all programs
                    {
                        IconPathLoaction = clsIcons.GetSharedAllProgramsPath();
                    }
                }
                //MessageBox.Show(IconPathLoaction);
                if (!String.IsNullOrEmpty(IconPathLoaction))
                {
                    IconPathLoaction = IconPathLoaction + "\\" + IconName + "";
                }
                if (System.IO.File.Exists(IconPathLoaction))
                {
                    System.IO.File.Delete(IconPathLoaction);
                }

            }
            catch (System.IO.FileNotFoundException)
            {
            }
            catch (System.Security.SecurityException)
            {
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        //Added New function for Getting Operation System Name.
        public static string GetOSFriendlyName()
        {
            string result = string.Empty;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem"))
            {
                foreach (ManagementObject os in searcher.Get())
                {
                    result = os["Caption"].ToString();
                    break;
                }
            }
            return result;
        } 
    }
}
