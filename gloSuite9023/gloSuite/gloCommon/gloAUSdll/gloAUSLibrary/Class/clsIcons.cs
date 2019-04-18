using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace gloAUSLibrary
{
    public class clsIcons
    {
        [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHGetKnownFolderPath(ref Guid rfid, uint dwFlags, IntPtr hToken, out StringBuilder path);

        private const int MAX_PATH = 260;
        private const int CSIDL_COMMON_PROGRAMS = 0x0017;
        private const int CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019;
        //private const int CSIDL_PROGRAMS = 0x0002;
        private const int CSIDL_DESKTOP = 0x0000;
        private const int CSIDL_DESKTOPDIRECTORY = 0x0010;               // <user name>\Desktop
        private const int CSIDL_PROGRAMS = 0x0002;

        public static string GetuserDesktopDir()
        {
            StringBuilder sbPath = new StringBuilder(MAX_PATH);
            try
            {
                if (sbPath != null)
                {
                    SHGetFolderPath(

                    IntPtr.Zero, CSIDL_DESKTOPDIRECTORY, IntPtr.Zero, 0, sbPath);
                }
                else
                {
                    return "";
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }
            catch (System.Reflection.TargetException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {

            }
            return sbPath.ToString();

        }

        public static string GetallprogramsMenuPath()
        {
            StringBuilder sbPath = new StringBuilder(MAX_PATH);
            try
            {

                if (sbPath != null)
                {
                    SHGetFolderPath(

                    IntPtr.Zero, CSIDL_COMMON_PROGRAMS, IntPtr.Zero, 0, sbPath);
                }
                else
                {
                    return "";
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }
            catch (System.Reflection.TargetException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {

            }
            return sbPath.ToString();

        }

        public static string GetAllUsersDesktopFolderPath()
        {

            StringBuilder sbPath = new StringBuilder(MAX_PATH);
            try
            {
                if (sbPath != null)
                {
                    SHGetFolderPath(
                    IntPtr.Zero, CSIDL_COMMON_DESKTOPDIRECTORY, IntPtr.Zero, 0, sbPath);
                }
                else
                {
                    return "";
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }
            catch (System.Reflection.TargetException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {

            }
            return sbPath.ToString();

        }

        public static string GetDesktopDir()
        {
            // {B4BFCC3A-DB2C-424C-B029-7FE99A87C641}

            StringBuilder path = new StringBuilder(260);
            try
            {
                Guid FOLDERID_PublicDesktop = new Guid(0xB4BFCC3A, 0xDB2C, 0x424C, 0xB0, 0x29, 0x7F, 0xE9, 0x9A, 0x87, 0xC6, 0x41);
                if (path != null)
                {
                    uint retval = SHGetKnownFolderPath(ref FOLDERID_PublicDesktop, 0, IntPtr.Zero, out path);

                }
                else
                {
                    return "";
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }
            catch (System.Reflection.TargetException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {

            }
            return path.ToString();
        }
        
        public static string GetSharedDesktop()
        {
            StringBuilder path = new StringBuilder(260);
            try
            {
                Guid FOLDERID_PublicDesktop = new Guid(0xC4AA340D, 0xF20F, 0x4863, 0xAF, 0xEF, 0xF8, 0x7E, 0xF2, 0xE6, 0xBA, 0x25);

                // {C4AA340D-F20F-4863-AFEF-F87EF2E6BA25}


                if (path != null)
                {
                    uint retval = SHGetKnownFolderPath(ref FOLDERID_PublicDesktop, 0, IntPtr.Zero, out path);

                }
                else
                {
                    return "";
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }
            catch (System.Reflection.TargetException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {

            }
            return path.ToString();
        }

        //FOLDERID_Programs 
        public static string GetSharedProgramsPath()
        {


            StringBuilder path = new StringBuilder(260);
            try
            {
                Guid FOLDERID_Programs = new Guid(0xA77F5D77, 0x2E2B, 0x44C3, 0xA6, 0xA2, 0xAB, 0xA6, 0x01, 0x05, 0x4A, 0x51);
                //{A77F5D77-2E2B-44C3-A6A2-ABA601054A51}




                if (path != null)
                {
                    uint retval = SHGetKnownFolderPath(ref FOLDERID_Programs, 0, IntPtr.Zero, out path);

                }
                else
                {
                    return "";
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }
            catch (System.Reflection.TargetException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {

            }
            return path.ToString();

        }
        //FOLDERID_CommonPrograms
        public static string GetSharedAllProgramsPath()
        {

            StringBuilder path = new StringBuilder(260);
            try
            {

                Guid FOLDERID_Programs = new Guid(0x0139D44E, 0x6AFE, 0x49F2, 0x86, 0x90, 0x3D, 0xAF, 0xCA, 0xE6, 0xFF, 0xB8);
                //{0139D44E-6AFE-49F2-8690-3DAFCAE6FFB8}




                if (path != null)
                {
                    uint retval = SHGetKnownFolderPath(ref FOLDERID_Programs, 0, IntPtr.Zero, out path);

                }
                else
                {
                    return "";
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }
            catch (System.Reflection.TargetException)
            {
            }
            catch (System.ApplicationException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {

            }
            return path.ToString();


        }
    }


}
