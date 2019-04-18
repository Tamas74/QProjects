using System;
using System.Collections.Generic;
using System.Text;
using  Microsoft.Win32;
using  System.IO;
using  System.Threading;

namespace gloSettings
{
   public static  class gloRegistrySetting
    {
        #region
        #endregion
      //public   gloRegistrySetting()
      //  {
      //  }

      public static bool IsServerOS = false ;
      private static RegistryKey regkey=null ;

      public static string  gstrSoft="Software\\";
      public static string gstrSoftEMR = "Software\\gloEMR";
      public static string gstrSqlServ="SQLServer";
      public static string gstrDB="Database";
      public static string gstrSqlAuthen="IsSQLAuthentication";
      public static string gstrSqlUsrEMR="SQLUserEMR";
      public static string gstrSqlPwdEMR="SQLPasswordEMR";
      public static string gstrDefPatCode = "DefaultPatientCode";
      public static string gstrServpth = "ServerPath";
      public static string gstrEMR = "gloEMR";
      public static string gstrPenwdth = "PenWidth";

      public static string gstrEliminatePegasus = "EliminatePegasus";  
      public static string gstrEnableRemoteScan = "EnableRemoteScan";

      public static string gstrZipScannerSettings = "ZipScannerSettings";

      public static string gstrScanImageFormat = "ScanImageFormat";

      public static string gstrDMSScan = "DMSScanner";
      public static string gstrRemoteScanner = "DMSScanner";  //"RemoteScanner";

      public static string gstrDMSResol = "DMSResolution";
      public static string gstrRemoteScanResol = "DMSResolution"; //"RemoteScanResolution";

      public static string gstrDMSBright = "DMSBrightness";
      public static string gstrRemoteScanBright = "DMSBrightness"; //"RemoteScanBrightness";
      public static string gstrPegasusBright = "DMSPegasusBrightness"; 

      public static string gstrSoftPM = "Software\\gloPM";
      public static string gstrDMSContrast = "DMSContrast";
      public static string gstrRemoteScanContrast = "DMSContrast";  //"RemoteScanContrast";
      public static string gstrPegasusContrast = "DMSPegasusContrast";  

      public static string gstrDMSScanMode = "DMSScanMode";
      public static string gstrRemoteScanMode = "DMSScanMode";  //"RemoteScanMode";

      public static string gstrDMSScanSide = "DMSScanSide";
      public static string gstrRemoteScanSide = "DMSScanSide";  //"RemoteScanSide";

      public static string gstrDMSScanFeeder = "DMSScanFeeder";

      public static string gstrDMSScanDepth = "DMSScanDepth";//
      public static string gstrRemoteScanDepth = "DMSScanDepth";  //"RemoteScanDepth";//

      public static string gstrDMSCardWidth = "CardWidth";//
      public static string gstrRemoteCardWidth = "CardWidth";  //"RemoteCardWidth";//

      public static string gstrDMSCardLength = "CardLength";//
      public static string gstrRemoteCardLength = "CardLength"; //"RemoteCardLength";//

      public static string gstrDMSCardLeftX = "CardLeftX";//
      public static string gstrDMSCardTopY = "CardTopY";//

      public static string gstrRemoteCardLeftX = "CardLeftX"; //"RemoteCardLeftX";//
      public static string gstrRemoteCardTopY = "CardTopY";  //"RemoteCardTopY";//

       public static string gstrDMSupporedSize = "DMSPageDesiredSize";//
       public static string gstrRemoteSupporedSize = "DMSPageDesiredSize"; //"RemotePageDesiredSize";//


      public static string gstrDMSShowScann = "DMSShowScanner";
      public static string gstrRemoteShowScann = "DMSShowScanner"; //"RemoteShowScanner";

      public static string gstrDMSBuffSz = "DMSBufferSize";
      public static string gstrShwresBox = "ShowResultBox";
      public static string gstrTrue = "True";
      public static string gstrFalse = "False";

    public static string gstrResBoxPos="ResultBoxPosition";

    public static string gstrtoplft="TopLeft";
    public static string gstrtoprght=  "TopRight";
      public static bool gblnAutoLockEnable = false;


    public static string gstrbtmlft="BottomLeft";
    public static string gstrbtmrght=  "BottomRight";

    //GLO2010-0007047 [BJMC]: Webcam image too small
    public static string gCameraLocation = "CameraLocation";
    public static string gCameraZoom = "CameraZoom";
    public static string gCameraRotation = "CameraRotation";
    public static string gCameraName = "CameraName";
    public static string gCameraVersion = "CameraVersion";
    public static string gCameraFPS = "CameraFramePerSecond";
    public static string gZoomVersion = "ZoomVersion"; //It is set to resolve the case GLO2011-0015477 while importing the data from eThomas.

       public static  Boolean  IsRegistryKeyExists(string SubKey)
         {
             ////IsServerOS = true;
           //Commented for windows 8 - from 7020 regestry value access from Current user on 20121220
             //if (IsServerOS == true)
             //{
           //Added by SLR on 3/20/2014
                 RegistryKey checkRegkey= Registry.CurrentUser.OpenSubKey(SubKey) ;

                 if (checkRegkey == null)
                 {
                     return false;
                     //if ((Registry.LocalMachine.OpenSubKey(SubKey) == null))
                     //{
                     //    return false;
                     //}
                     //else
                     //    return true;

                 }
                 else
                 {
                     try
                     {
                         checkRegkey.Close();
                         checkRegkey.Dispose();
                     }
                     catch
                     {
                     }
                     return true;
                 }
             //}
             //else 
             //{
             //    if ((Registry.LocalMachine.OpenSubKey(SubKey) == null))
             //    {
             //        return false;
             //    }
             //    else
             //        return true;
             
             //}
          }

        public static void  OpenSubKey(string SubKey, Boolean flag)
        {
            //IsServerOS = true;
            //if (IsServerOS == true)
            //{
            //Added by SLR on 3/20/2014
            
            if (regkey != null)
            {
                CloseRegistryKey();
            }
                regkey = Registry.CurrentUser.OpenSubKey(SubKey, flag);
                if (regkey == null)
                {   
                    RegistryKey lregkey = Registry.LocalMachine.OpenSubKey(SubKey, false);
                    if (lregkey != null)
                    {
                        regkey = Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                        if (regkey != null)
                        {
                            string[] keyNames = lregkey.GetValueNames();
                            foreach (string keyName in keyNames)
                            {
                                regkey.SetValue(keyName, lregkey.GetValue(keyName));
                            }
                        }
                        lregkey.Close();
                        lregkey.Dispose();
                    }
                    //Bug #42386: PM-Registry Changes-Application does not save registry under HKEY_CURRENT_USER
                    else
                    {
                        regkey = Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);                        
                    }
                }
            //}
            //else
            //{
            //    regkey = Registry.LocalMachine.OpenSubKey(SubKey, flag);
            //}
          
        
        
        
        
        
        
        }

        public static Boolean  OpenSubKey(string SubKey, Boolean flag,string none)
        {
            //IsServerOS = true;
            //if (IsServerOS == true)
            //{
            //Added by SLR on 3/20/2014

            if (regkey != null)
            {
                CloseRegistryKey();
            }

            regkey = Registry.CurrentUser.OpenSubKey(SubKey, flag);
                if (regkey == null)
                {
                    RegistryKey lregkey = Registry.LocalMachine.OpenSubKey(SubKey, flag);
                    if (lregkey != null)
                    {
                        regkey = Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                        if (regkey != null)
                        {
                            string[] keyNames = lregkey.GetValueNames();
                            foreach (string keyName in keyNames)
                            {
                                regkey.SetValue(keyName, lregkey.GetValue(keyName));
                            }
                        }
                        lregkey.Close();
                        lregkey.Dispose();
                    }
                }
            //}
            //else
            //{
            //    regkey = Registry.LocalMachine.OpenSubKey(SubKey, flag);
            //}




            return (regkey != null);


        }


        public static Boolean   OpenSubKey(string SubKey)
        {
            try
            {
                //IsServerOS = true;
                //if (IsServerOS == true)
                //{
                //Added by SLR on 3/20/2014

                if (regkey != null)
                {
                    CloseRegistryKey();
                }

                    regkey = Registry.CurrentUser.OpenSubKey(SubKey);

                    if (regkey == null)
                    {
                        RegistryKey lregkey = Registry.LocalMachine.OpenSubKey(SubKey);
                        if (lregkey != null)
                        {
                            regkey = Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                            if (regkey != null)
                            {
                                string[] keyNames = lregkey.GetValueNames();
                                foreach (string keyName in keyNames)
                                {
                                    regkey.SetValue(keyName, lregkey.GetValue(keyName));
                                }
                            }
                            lregkey.Close();
                            lregkey.Dispose();
                          
                        }
                    }

                //}
                //else
                //{
                //    regkey = Registry.LocalMachine.OpenSubKey(SubKey);
                //}
                return (regkey != null);
            }
            catch 
            {
                
                return false ;
            }

        }

        public static Boolean OpenRemoteBaseKey()
        {
            try
            {
                //Bug #42386: PM-Registry Changes-Application does not save registry under HKEY_CURRENT_USER
                //Changes missing while doing changes of windows 8

                // IsServerOS = true;
                //if (IsServerOS == true)
                //{
                //Added by SLR on 3/20/2014

                if (regkey != null)
                {
                    CloseRegistryKey();
                }

                regkey = RegistryKey.OpenRemoteBaseKey(RegistryHive.CurrentUser, System.Windows.Forms.SystemInformation.ComputerName);

                if (regkey == null)
                {
                    //RegistryKey lregkey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, System.Windows.Forms.SystemInformation.ComputerName);
                    //if (lregkey != null)
                    //{

                    //        Registry.CurrentUser.CreateSubKey(SubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                    //    if (regkey != null)
                    //    {
                    //        regkey.SetValue(SubKey, lregkey.GetValue(SubKey));
                    //    }
                    //    lregkey.Close();
                    //}
                }

                //}
                //else
                //{
                //    regkey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, System.Windows.Forms.SystemInformation.ComputerName);

                //}
                return (regkey != null);
            }
            catch
            {
                return false;
            }

        }
        public static  object   GetRegistryValue(string KeyName)
        {
            //RegistryKey regkey;


            //if (regkey.GetValue(KeyName) == null)
              //  return null;
            //else

            if (regkey != null)
            {
                return regkey.GetValue(KeyName);
            }
            else
            {
                return null;
            }
           
        }

        public static  void  SetRegistryValue( string Name, object Reg)
        {
            if (regkey != null)
            {
                regkey.SetValue(Name, Reg);
            }

        }
        public static void CreateSubKey( string SubKeyName)
        {
            if (regkey != null)
            {
                RegistryKey thisKey = regkey.CreateSubKey(SubKeyName);
                CloseRegistryKey();
                regkey = thisKey;
            }
        }


        public static Boolean  CreateSubKey(string SubKeyName,string none)
        {
            try
            {
                if (regkey != null)
                {
                    RegistryKey thisKey = regkey.CreateSubKey(SubKeyName);
                    CloseRegistryKey();
                    regkey = thisKey;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public static void CloseRegistryKey()
        {
            try
            {
                if (regkey != null)
                {
                    regkey.Close();
                    //Adde by SLR on 3/20/2014
                    regkey.Dispose();
                    regkey = null;
                }
            }
            catch
            {
                regkey = null;
            }
           
           }




     
    }
}


