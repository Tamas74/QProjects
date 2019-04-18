using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Security.Permissions;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace gloCommunity.Forms
{
    public partial class FrmgloCommunityDashboard : Form
    {
        public string sSiteURL;
        bool _IsSignOut = false; //Added by kanchan on 20120910
        public FrmgloCommunityDashboard(string _strShowSite)
        {
            InitializeComponent();

            string fqdnname = Dns.GetHostEntry(Environment.MachineName.ToString()).HostName.ToLower();// Dns.GetHostByName(Environment.MachineName.ToString()).HostName.ToLower();
            int cnt = Environment.MachineName.Length + 1;
            if (fqdnname.Contains(Environment.MachineName.ToLower() + "."))
            {
                string actdomainname = fqdnname.Substring(cnt);
                clsGeneral.gstrDomainName1 = actdomainname;
            }

            if (_strShowSite == "MySite")
            {
               
            //    sSiteURL = "http://dev110/my/person.aspx";
                string[] strCommunitySrv = clsGeneral.gstrSharepointSrvNm.Split(':');
              //  sSiteURL = "http://" + strCommunitySrv[0].Trim() + "/my/person.aspx";
               // sSiteURL = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/SitePages/Home.aspx?ClinicName=" + clsGeneral.gstrClinicName + "&DomainName=" + clsGeneral.gstrDomainName1 + "";
                sSiteURL = clsGeneral.gstrSharepointSrvNm + "/sites/MySite/person.aspx?ClinicName=" + clsGeneral.gstrClinicName + "&DomainName=" + clsGeneral.gstrDomainName1 + "&authtype=" + clsGeneral.gstrgloCommunityAuthentication.ToLower() + "";
                                
                this.Text = "My Community";
            }
            else
            {

              
                //Code Added by kanchan on 20111111 to link practice master to clinical repository
                //sSiteURL = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/SitePages/Home.aspx";
                //sSiteURL = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/SitePages/Home.aspx?ClinicName="+ clsGeneral.gstrClinicName  +"";
                //commented by kanchan on 20120120
                //sSiteURL = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/SitePages/Home.aspx?ClinicName=" + clsGeneral.gstrClinicName + "&?DomainName=" + clsGeneral.gstrDomainName1 + "";
                
                //Code commented & Added by kanchan on 201208002 for Form authentication changes
                //sSiteURL = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/SitePages/Home.aspx?ClinicName=" + clsGeneral.gstrClinicName + "&DomainName=" + clsGeneral.gstrDomainName1 + "";
                sSiteURL = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/SitePages/Home.aspx?ClinicName=" + clsGeneral.gstrClinicName + "&DomainName=" + clsGeneral.gstrDomainName1 + "&authtype=" + clsGeneral.gstrgloCommunityAuthentication.ToLower() + "";

                this.Text = "Community Connect";
           }

            if (clsGeneral.gstrgloCommunityAuthentication != "" && clsGeneral.gstrgloCommunityAuthentication.Trim().ToUpper() == "FORM")
            {
                //Code added by kanchan on 20120910 for password encryption
                byte[] toEncodeasBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(clsGeneral.gstrGCPassword);
                string _password = System.Convert.ToBase64String(toEncodeasBytes);
                //Code changes -done by kanchan on 20121010 for backward compatibility issue
                //sSiteURL = sSiteURL + "&uid=" + clsGeneral.gstrGCUserName + "&pwd=" + _password + "";
                sSiteURL = sSiteURL + "&uid=" + clsGeneral.gstrGCUserName + "&enpwd=" + _password + "";
                //condition for password is added by kanchan on 20120807 to handle special character
                //sSiteURL = sSiteURL + "&uid=" + clsGeneral.gstrGCUserName + "&pwd=" + checkforSpecialCharinURL(clsGeneral.gstrGCPassword) + "";
            }

            WbgloCommunity.CanGoBackChanged += new EventHandler(WbgloCommunity_CanGoBackChanged);
            WbgloCommunity.CanGoForwardChanged += new EventHandler(WbgloCommunity_CanGoForwardChanged);
            
            // Load the gloCommunity Home page.
            //WbgloCommunity.GoHome();
            //Code start Added by kanchan on 20120201 for process Message & cursor change
            this.Cursor = Cursors.WaitCursor;
            pnlProcess.Visible = true;
            WbgloCommunity.Navigate(sSiteURL);
          

        }

        //added by kanchan on 20120807 to handle special character
        private string checkforSpecialCharinURL(string _pwd)
        {
            if (_pwd == null || _pwd == "")
                return "";
            _pwd = _pwd.Replace("%", "%25");
            _pwd = _pwd.Replace("+", "%2B").Replace("/", "%2F").Replace("?", "%3F").Replace("#", "%23").Replace("&", "%26");
            return _pwd;
        }

        // Disables the Back button at the beginning of the navigation history.
        private void WbgloCommunity_CanGoBackChanged(object sender, EventArgs e)
        {
           backButton.Enabled = WbgloCommunity.CanGoBack;
        }

        // Disables the Forward button at the end of navigation history.
        private void WbgloCommunity_CanGoForwardChanged(object sender, EventArgs e)
        {
            forwardButton.Enabled = WbgloCommunity.CanGoForward;
        }


        private void FrmgloCommunityDashboard_Load(object sender, EventArgs e)
        {
            WbgloCommunity.Navigate(sSiteURL);
            ///////////
            backButton.Enabled = false;
            forwardButton.Enabled = false;
            ///////////
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            //Code Start-Added by kanchan on 20120910
            string _URL = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/_layouts/signOut.aspx";
            _IsSignOut = true;
            WbgloCommunity.Navigate(_URL);
            //Code End-Added by kanchan on 20120910
            StopKeyListening();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            //Code start Added by kanchan on 20120201 for cursor change
            try
            {
                //Code start Added by kanchan on 20120201 for process Message & cursor change
                this.Cursor = Cursors.WaitCursor;
                pnlProcess.Visible = true;
                
                WbgloCommunity.GoHome();
                WbgloCommunity.Navigate(sSiteURL);
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            // Skip refresh if about:blank is loaded to avoid removing
            // content specified by the DocumentText property.
            //Code start Added by kanchan on 20120201 for cursor change
            try
            {
                //Code start Added by kanchan on 20120201 for process Message & cursor change
                this.Cursor = Cursors.WaitCursor;
                pnlProcess.Visible = true;
                string _currentURL = WbgloCommunity.Url.ToString();
                WbgloCommunity.Navigate(_currentURL);
                //if (!WbgloCommunity.Url.Equals("about:blank"))
                //{
                //    WbgloCommunity.Refresh();
                //}
                
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
                       
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            //Code start Added by kanchan on 20120201 for cursor change
            try
            {
                //Code start Added by kanchan on 20120201 for process Message & cursor change
                this.Cursor = Cursors.WaitCursor;
                pnlProcess.Visible = true;
                WbgloCommunity.GoBack();
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            //Code start Added by kanchan on 20120201 for cursor change
            try
            {
                //Code start Added by kanchan on 20120201 for process Message & cursor change
                this.Cursor = Cursors.WaitCursor;
                pnlProcess.Visible = true;
                WbgloCommunity.GoForward();
            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        #region "Tushar for Esc Key"
        private readonly gloCommunity.Utility.KeyBordHook _keyBordHook = new gloCommunity.Utility.KeyBordHook();
        private void InitKeyHook()
        {
            _keyBordHook.OnKeyPressEvent += new KeyPressEventHandler(_KeyBordHook_OnKeyPressEvent);
            _keyBordHook.Start();
        }

        void _KeyBordHook_OnKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                //WindowHelper.CloseWindowIfActive("Flash");
                WindowHelper.CloseWindowIfActive("Microsoft SilverLight");

            }
        }
        public void StartKeyListening2()
        {
            if (WbgloCommunity.Url != null)
            {
                if (WbgloCommunity.Url.AbsoluteUri.ToString() != "")
                {
                    if (WbgloCommunity.Url.AbsoluteUri.ToString().ToLower().Trim().Contains("SitePages/gloTV.aspx".ToLower()) || WbgloCommunity.Url.AbsoluteUri.ToString().ToLower().Trim().Contains("SitePages\\gloTV.aspx".ToLower()))
                    {
                        StartKeyListening();
                    }
                    else
                    {
                        StopKeyListening();
                    }
                }
                else
                {
                    StopKeyListening();
                }
            }
            else
            {
                StopKeyListening();
            }
        }

        Boolean keyslistenerstarted = false;
        public void StartKeyListening()
        {
            InitKeyHook();
            keyslistenerstarted = true;
        }

        public void StopKeyListening()
        {
            if (keyslistenerstarted)
            {
                _keyBordHook.Stop();
                keyslistenerstarted = false;
            }
        }

        private void FrmgloCommunityDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopKeyListening();
        }

        private void FrmgloCommunityDashboard_Deactivate(object sender, EventArgs e)
        {
            StopKeyListening();
        }


        private void FrmgloCommunityDashboard_Activated(object sender, EventArgs e)
        {
            StartKeyListening2();
            
        }

        #endregion"Tushar"

        private void WbgloCommunity_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            StartKeyListening2();
            //Code start Added by kanchan on 20100207 to redirect to home page with clinic name after adfs process
            string _URL = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/SitePages/Home.aspx";
            if (WbgloCommunity.Url.ToString().ToLower() == _URL.ToLower())
            {
                Uri _testURL = new Uri(_URL + "?ClinicName=" + clsGeneral.gstrClinicName);
                WbgloCommunity.Url = _testURL;
            }

            //Code start Added by kanchan on 20120201 for process Message & cursor change
            //pnlProcess.Visible = false;
            //this.Cursor = Cursors.Default;
        }

        private void WbgloCommunity_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //Code start Added by kanchan on 20120201 for process Message & cursor change
            //pnlProcess.Visible = true;
          
        }

        private void WbgloCommunity_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
                //Code start Added by kanchan on 20120201 for process Message & cursor change
                pnlProcess.Visible = false;
                this.Cursor = Cursors.Default;
                //Code Start-Added by kanchan on 20121026 for cookies clear
                if (_IsSignOut == true && WbgloCommunity.ReadyState == WebBrowserReadyState.Complete)
                {
                    this.Close();
                }
                //Code end-Added by kanchan on 20121026
        }

        private void FrmgloCommunityDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        

    }

    #region "Tushar for Esc Key"
    public sealed class WindowHelper
    {
        [DllImport("USER32.dll", SetLastError = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("USER32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        static uint WM_CLOSE = 0x10;
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("USER32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private static bool CloseWindow(IntPtr hWnd)
        {
            bool returnValue = PostMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            if (!returnValue)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return true;
        }

        public static void CloseWindowIfActive(string windowTitle)
        {
            const int nChars = 256;
            StringBuilder buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, buff, nChars) <= 0) return;

            if (buff.ToString().ToLower().IndexOf(windowTitle.ToLower()) > -1)
                CloseWindow(handle);
        }
    }
    #endregion ""
}
#region "Tushar for Esc Key"
namespace gloCommunity.Utility
{
    public class KeyBordHook
    {
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;

        //Global event 
        public event KeyEventHandler OnKeyDownEvent;
        public event KeyEventHandler OnKeyUpEvent;
        public event KeyPressEventHandler OnKeyPressEvent;

        private static int hKeyboardHook = 0;

        private const int WH_KEYBOARD_LL = 13; //keyboard hook constant

        private HookProc KeyboardHookProcedure; // declare keyhook event type

        //declare keyhook struct 
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        [DllImport("user32")]
        private static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        private List<Keys> preKeys = new List<Keys>();


        public KeyBordHook()
        {
            Start();
        }

        ~KeyBordHook()
        {
            Stop();
        }

        public void Start()
        {
            //install keyboard hook 
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                //hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                Process curProcess = Process.GetCurrentProcess();
                ProcessModule curModule = curProcess.MainModule;

                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(curModule.ModuleName), 0);

                if (hKeyboardHook == 0)
                {
                    Stop();
                    throw new Exception("SetWindowsHookEx ist failed.");
                }
            }
        }

        public void Stop()
        {
            bool retKeyboard = true;

            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }
            //if unhook failed 
            if (!(retKeyboard)) throw new Exception("UnhookWindowsHookEx failed.");
        }

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {

            if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    if (IsCtrlAltShiftKeys(keyData) && preKeys.IndexOf(keyData) == -1)
                    {
                        preKeys.Add(keyData);
                    }
                }

                if (OnKeyDownEvent != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));

                    OnKeyDownEvent(this, e);
                }

                if (OnKeyPressEvent != null && wParam == WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    GetKeyboardState(keyState);

                    byte[] inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.vkCode,
                    MyKeyboardHookStruct.scanCode,
                    keyState,
                    inBuffer,
                    MyKeyboardHookStruct.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        OnKeyPressEvent(this, e);
                    }
                }

                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    if (IsCtrlAltShiftKeys(keyData))
                    {

                        for (int i = preKeys.Count - 1; i >= 0; i--)
                        {
                            if (preKeys[i] == keyData)
                            {
                                preKeys.RemoveAt(i);
                            }
                        }

                    }
                }

                if (OnKeyUpEvent != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));
                    OnKeyUpEvent(this, e);
                }
            }
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        private Keys GetDownKeys(Keys key)
        {
            Keys rtnKey = Keys.None;
            foreach (Keys keyTemp in preKeys)
            {
                switch (keyTemp)
                {
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                        rtnKey = rtnKey | Keys.Control;
                        break;
                    case Keys.LMenu:
                    case Keys.RMenu:
                        rtnKey = rtnKey | Keys.Alt;
                        break;
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                        rtnKey = rtnKey | Keys.Shift;
                        break;
                    default:
                        break;
                }
            }
            rtnKey = rtnKey | key;

            return rtnKey;
        }

        private Boolean IsCtrlAltShiftKeys(Keys key)
        {

            switch (key)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    return true;
                default:
                    return false;
            }
        }
    }
}
    #endregion ""
