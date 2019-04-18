using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace gloEDocumentV3
{
    class clsHotKeys
    {
    }

    public interface IHotKey
    {
        void Navigate(string strstring);
    }

    public class HotKeyPressedEventArgs : EventArgs
    {

        private HotKey m_hotKey;
        public object HotKey
        {
            get { return m_hotKey; }
        }

        internal HotKeyPressedEventArgs(HotKey hotKey)
        {
            m_hotKey = hotKey;
        }

    }

    public class HotKey
    {
        //[Flags]
        public enum HotKeyModifiers : int
        {
            MOD_NONE = 0x0,
            // Added new enaum on 09/18/2006 - Pravin
            MOD_ALT = 0x1,
            MOD_CONTROL = 0x2,
            MOD_SHIFT = 0x4,
            MOD_WIN = 0x8,
            MOD_ALT_SHIFT = 0x1 + 0x4
            //'For the alt and shift key (Modifier).''dhruv 2010232010
        }
        private string m_name;
        private string m_atomName;
        private IntPtr m_atomId;
        private Keys m_keyCode;

        private HotKeyModifiers m_modifiers;
        internal IntPtr AtomId
        {
            get { return m_atomId; }
            set { m_atomId = value; }
        }

        internal string AtomName
        {
            get { return m_atomName; }
            set { m_atomName = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public Keys KeyCode
        {
            get { return m_keyCode; }
            set { m_keyCode = value; }
        }

        public HotKeyModifiers Modifiers
        {
            get { return m_modifiers; }
            set { m_modifiers = value; }
        }

        public void Validate()
        {
            string msg = "";
            //If (Name Is Null) Then
            //msg = "Name parameter cannot be null"
            //End If
            if ((m_name.Trim().Length == 0))
            {
                msg = "Name parameter cannot be zero length";
            }
            if (((KeyCode == Keys.Alt) | (KeyCode == Keys.Control) | (KeyCode == Keys.Shift) | (KeyCode == Keys.ShiftKey) | (KeyCode == Keys.ControlKey)))
            {
                msg = "KeyCode cannot be set to a modifier key";
            }
            if ((msg.Length > 0))
            {
                throw new ArgumentException(msg);
            }
        }


        public HotKey()
        {
        }

        public HotKey(string name, Keys keyCode, HotKeyModifiers modifiers)
        {
            m_name = name;
            m_keyCode = keyCode;
            m_modifiers = modifiers;
        }

    }

    //public class HotKeyCollection : System.Collections.CollectionBase
    //{

    //    private System.Windows.Forms.Form ownerForm;

    //    [DllImport("kernel32.dll")]
    //    private static extern bool ProcessIdToSessionId(uint dwProcessId, ref uint pSessionId);
    //    protected override void OnClear()
    //{
    //    HotKey htk = default(HotKey);
    //    foreach ( htk in this.InnerList) {
    //        RemoveHotKey(htk);
    //    }
    //    base.OnClear();
    //}

    //    protected override void OnInsert(int index, object item)
    //    {
    //        // validate item is a hot key:
    //        HotKey htk = new HotKey();
    //        if ((item.GetType().IsInstanceOfType(htk)))
    //        {
    //            // check if the name, keycode and modifiers have been set up:
    //            htk = item;
    //            // throws ArgumentException if there is a problem:
    //            htk.Validate();
    //            // throws Unable to add HotKeyException:
    //            AddHotKey(htk);
    //            // ok
    //            base.OnInsert(index, item);
    //        }
    //        else
    //        {
    //            throw new InvalidCastException("Invalid object.");
    //        }

    //    }
    //    protected override void OnRemove(int index, object item)
    //    {
    //        // get the item to be removed:
    //        HotKey htk = item;
    //        RemoveHotKey(htk);
    //        base.OnRemove(index, item);
    //    }

    //    protected override void OnSet(int index, object oldItem, object newItem)
    //    {
    //        // remove old hot key:
    //        HotKey htk = oldItem;
    //        RemoveHotKey(htk);

    //        // add new hotkey:
    //        htk = newItem;
    //        AddHotKey(htk);

    //        base.OnSet(index, oldItem, newItem);
    //    }

    //    protected override void OnValidate(object item)
    //    {
    //        HotKey htk = item;
    //        htk.Validate();
    //    }

    //    public void Add(HotKey hotKey)
    //    {
    //        // throws argument exception:
    //        hotKey.Validate();
    //        // throws unable to add hot key exception:
    //        AddHotKey(hotKey);
    //        // assuming all is well:
    //        this.InnerList.Add(hotKey);
    //    }

    //    public int this[int index]
    //    {
    //        get { return this.InnerList[index]; }
    //    }

    //    private void RemoveHotKey(HotKey hotKey)
    //    {
    //        //// remove the hot key:
    //        bool ret = UnmanagedMethods.UnregisterHotKey(ownerForm.Handle, hotKey.AtomId.ToInt32());
    //        int myerror = Marshal.GetLastWin32Error();
    //        if (ret != 0)
    //        {
    //            UnmanagedMethods.GlobalDeleteAtom(hotKey.AtomId);
    //        }
    //        //// unregister the atom:

    //    }

    //    private bool HotKeyFound(string atomName, ref IntPtr myfindAtom)
    //    {
    //        bool _isHotKeyRegistered = false;
    //        myfindAtom = UnmanagedMethods.GlobalFindAtom(atomName);
    //        if (myfindAtom.Equals(IntPtr.Zero))
    //        {
    //            _isHotKeyRegistered = false;
    //        }
    //        else
    //        {
    //            _isHotKeyRegistered = true;
    //        }
    //        return _isHotKeyRegistered;
    //    }

    //    private void getProcessIdToSessionId(ref uint myApplicationProcessId, ref uint myApplicationSessionId, ref string myApplicationName)
    //    {
    //        System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
    //        myApplicationProcessId = Convert.ToUInt32(currentProcess.Id);
    //        myApplicationName = System.IO.Path.GetFileName(Application.ExecutablePath).Replace(".EXE", "");
    //        ProcessIdToSessionId(myApplicationProcessId, ref myApplicationSessionId);
    //    }

    //    private void AddHotKey(HotKey hotKey)
    //    {

    //        //'Variable decalaration 
    //        bool _isAlreadyExists = false;
    //        string myApplicationName = "";
    //        uint myApplicationProcessId = 0;
    //        uint myApplicationSessionId = 0;
    //        getProcessIdToSessionId(ref myApplicationSessionId, ref myApplicationSessionId, ref myApplicationName);
    //        string myAtomName = myApplicationSessionId.ToString() + "_" + myApplicationProcessId.ToString() + "_" + myApplicationName + "_" + hotKey.Name + "_";
    //        int myKeyCode = hotKey.KeyCode;
    //        int myKeyModifier = hotKey.Modifiers;
    //        IntPtr id = 0;

    //        string atomName = myAtomName + "_" + myKeyCode.ToString() + "_" + myKeyModifier.ToString();
    //        if ((atomName.Length > 255))
    //        {
    //            atomName = atomName.Substring(0, 255);
    //        }
    //        if ((HotKeyFound(atomName, ref id)))
    //        {
    //            _isAlreadyExists = true;
    //        }
    //        else
    //        {
    //            id = UnmanagedMethods.GlobalAddAtom(atomName);
    //        }
    //        if ((id.Equals(IntPtr.Zero)))
    //        {
    //            // failed
    //            throw new HotKeyAddException("Failed to add GlobalAtom for HotKey");
    //        }
    //        else
    //        {
    //            // succeeded:
    //            bool ret = UnmanagedMethods.RegisterHotKey(ownerForm.Handle, id.ToInt32(), hotKey.Modifiers, hotKey.KeyCode);
    //            int myerror = Marshal.GetLastWin32Error();
    //            if (!_isAlreadyExists)
    //            {
    //                if (!(ret))
    //                {
    //                    // Remove the atom:
    //                    UnmanagedMethods.GlobalDeleteAtom(id);
    //                    // failed
    //                    throw new HotKeyAddException("Failed to register HotKey : " + myerror.ToString());

    //                }
    //                else
    //                {
    //                    hotKey.AtomName = atomName;
    //                    hotKey.AtomId = id;
    //                }
    //            }
    //            else
    //            {
    //                hotKey.AtomName = atomName;
    //                hotKey.AtomId = id;
    //            }

    //        }
    //    }


    //    public HotKeyCollection(System.Windows.Forms.Form ownerForm)
    //    {
    //        this.ownerForm = ownerForm;
    //    }

    //}

    internal class UnmanagedMethods
    {

        ///* SHIFT-PRINTSCRN  */
        internal const int IDHOT_SNAPWINDOW = -1;
        ///* PRINTSCRN        */
        internal const int IDHOT_SNAPDESKTOP = -2;

        internal const int WM_HOTKEY = 0x312;
        internal const int WM_ACTIVATEAPP = 0x1c;
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]

        public static extern IntPtr GlobalFindAtom(string lpString);
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]

        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GlobalAddAtom(string lpString);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GlobalDeleteAtom(IntPtr nAtom);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetTickCount();
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);
        internal const int WM_SYSCOMMAND = 0x112;

        internal const int SC_RESTORE = 0xf120;
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        internal const int SW_SHOW = 5;



    }



}
