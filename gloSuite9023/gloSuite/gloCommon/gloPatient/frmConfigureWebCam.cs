using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloSettings;
using gloStreamWebCamera.Camera;
using System.Runtime.InteropServices;
namespace gloPatient
{
    public partial class frmConfigureWebCam : Form
    {
        private string _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public bool okPressed = false;
        public string sCameraName = "";
        public int iFPS = 15;
      
        


        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        static extern bool DestroyWindow(IntPtr hWnd);

        public frmConfigureWebCam()
        {
            InitializeComponent();
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }
        }
        
        private void buttonAdvancedConfig_Click(object sender, EventArgs e)
        {
            if (comboCameras.SelectedIndex >= 0)
            {
                IntPtr hwnd = IntPtr.Zero;

               
                hwnd = FindWindow(null, "Properties");

                //send WM_CLOSE system message
                if (hwnd != IntPtr.Zero)
                {
                    DestroyWindow(hwnd);
                }
                Camera c = (Camera)comboCameras.SelectedItem;
                CameraFrameSource thisFrameSource = new CameraFrameSource(c);
                thisFrameSource.Camera.ShowPropertiesDialog();
                
            }
        }

        private void frmConfigureWebCam_Load(object sender, EventArgs e)
        {
            sCameraName = getCameraName();
            iFPS = getCameraFPS();
            textFPS.Text = iFPS.ToString();
            comboCameras.Items.Clear();
         
            int myIndex = 0;
            int selectIndex = -1;
            foreach (Camera cam in CameraService.AvailableCameras)
            {
                comboCameras.Items.Add(cam);
                
                if (cam.Name == sCameraName)
                {
                    selectIndex = myIndex;
                }
                myIndex += 1;
            }
            if (selectIndex != -1)
            {
  
              comboCameras.SelectedIndex = selectIndex;
  
            }
            else
            {
                if (comboCameras.Items.Count > 0)
                {
                    comboCameras.SelectedIndex = comboCameras.Items.Count-1;
                }
            }
        }

        //private void changeHeightAsPerResolution()
        //{
        //    //this.Height = 
        //    Int32 myScreenHeight = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99);
        //    if (myScreenHeight < this.Height)
        //    {
        //        this.Height = myScreenHeight;
        //    }
        //    Int32 myScreenWidth = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 1.1);
        //    if (myScreenWidth > this.width)
        //    {
        //        this.width = myScreenWidth;
        //    }
        //}
	 
	


        private static string getCameraName()
        {
            string sCameraName = "";
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {



            }
            else
            {

                object oCameraName = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraName);

                if (oCameraName != null)
                {
                    sCameraName = oCameraName.ToString();
                }

                gloRegistrySetting.CloseRegistryKey();
            }
            return sCameraName;
        }
        private static void setCameraName(string sCameraName)
        {
            if (gloRegistrySetting.IsRegistryKeyExists(gloRegistrySetting.gstrSoftEMR) == false)
            {
                gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftEMR);
            }
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {
                return;
            }
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraName, sCameraName);
            gloRegistrySetting.CloseRegistryKey();

          
            return;
        }
        private static int getCameraFPS()
        {
            int iFPS = 15;
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {

                return iFPS;

            }
            else
            {

                object oCameraFPS = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraFPS);

                if (oCameraFPS != null)
                {
                    try
                    {
                         iFPS = Convert.ToInt32(oCameraFPS);
                        if (iFPS <= 0)
                        {
                            iFPS = 15;
                        }
                    }
                    catch
                    {
                        iFPS = 15;
                    }

                }
                else
                {
                    iFPS = 15;
                }

                gloRegistrySetting.CloseRegistryKey();
            }
            return iFPS;
        }
        private static void setCameraIFPS(int iFPS)
        {
            if (gloRegistrySetting.IsRegistryKeyExists(gloRegistrySetting.gstrSoftEMR) == false)
            {
                gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftEMR);
            }
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {
                return;
            }
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraFPS, iFPS.ToString());
            gloRegistrySetting.CloseRegistryKey();


            return;
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {

           
            int thisFPS=iFPS; 
            try
            {
                thisFPS = Convert.ToInt32(textFPS.Text);
                if( (thisFPS <= 0) || (thisFPS >1000) )
                {
                    MessageBox.Show("Frame per seconds should be numeric and should be between 1 and 1000", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                iFPS = thisFPS;
            }
            catch (Exception) 
            {
                MessageBox.Show("Wrong Entry!\r\nFrame per seconds entered should be numeric and should be between 1 and 1000" , _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            setCameraIFPS(iFPS);
            if (comboCameras.SelectedIndex >= 0)
            {
                Camera c = (Camera)comboCameras.SelectedItem;
                setCameraName(c.Name);
                sCameraName = c.Name;
            }
            okPressed = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            okPressed = false;
            this.Close();
        }
    }
}
