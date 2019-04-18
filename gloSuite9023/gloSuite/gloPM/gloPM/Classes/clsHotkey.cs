
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace gloPM.Classes
{
    static class mdlHotkey
    {
        public interface IHotKey
        {
            void Navigate(string strstring);
        }
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

    public class HotKeyCollection : System.Collections.CollectionBase
    {
        [DllImport("kernel32.dll")]
        private static extern bool ProcessIdToSessionId(uint dwProcessId, ref uint pSessionId);

        private System.Windows.Forms.Form ownerForm;
        protected override void OnClear()
        {
            HotKey htk = null;
            foreach (HotKey htk_loopVariable in this.InnerList)
            {
                htk = htk_loopVariable;
                RemoveHotKey(htk);
            }
            base.OnClear();
        }

        protected override void OnInsert(int index, object item)
        {
            // validate item is a hot key:
            HotKey htk = new HotKey();
            if ((item.GetType().IsInstanceOfType(htk)))
            {
                // check if the name, keycode and modifiers have been set up:
                htk = (HotKey)item;
                // throws ArgumentException if there is a problem:
                htk.Validate();
                // throws Unable to add HotKeyException:
                AddHotKey(htk);
                // ok
                base.OnInsert(index, item);
            }
            else
            {
                throw new InvalidCastException("Invalid object.");
            }

        }
        protected override void OnRemove(int index, object item)
        {
            // get the item to be removed:
            HotKey htk = (HotKey)item;
            RemoveHotKey(htk);
            base.OnRemove(index, item);
        }

        protected override void OnSet(int index, object oldItem, object newItem)
        {
            // remove old hot key:
            HotKey htk = (HotKey)oldItem;
            RemoveHotKey(htk);

            // add new hotkey:
            htk = (HotKey)newItem;
            AddHotKey(htk);

            base.OnSet(index, oldItem, newItem);
        }

        protected override void OnValidate(object item)
        {
            HotKey htk = (HotKey)item;
            htk.Validate();
        }

        public void Add(HotKey hotKey)
        {
            // throws argument exception:
            hotKey.Validate();
            // throws unable to add hot key exception:
            AddHotKey(hotKey);
            // assuming all is well:
            this.InnerList.Add(hotKey);
        }

        public int this[int index]
        {
            get { return (int)this.InnerList[index]; }
        }

        private void RemoveHotKey(HotKey hotKey)
        {
            //// remove the hot key:

            bool ret = UnmanagedMethods.UnregisterHotKey(ownerForm.Handle, hotKey.AtomId.ToInt32());
            int myerror = Marshal.GetLastWin32Error();
            if (ret != false)
            {
                UnmanagedMethods.GlobalDeleteAtom(hotKey.AtomId);
            }

            //// unregister the atom:


        }


        //private void AddHotKey(HotKey hotKey)
        //{
        //    // generate the id:
        //    string atomName = hotKey.Name + "_" + UnmanagedMethods.GetTickCount().ToString();
        //    if ((atomName.Length > 255))
        //    {
        //        atomName = atomName.Substring(0, 255);
        //    }
        //    // Create a new atom:
        //    IntPtr id = UnmanagedMethods.GlobalAddAtom(atomName);
        //    if ((id.Equals(IntPtr.Zero)))
        //    {
        //        // failed
        //        throw new HotKeyAddException("Failed to add GlobalAtom for HotKey");
        //    }
        //    else
        //    {
        //        // succeeded:
        //        bool ret = UnmanagedMethods.RegisterHotKey(ownerForm.Handle, id.ToInt32(), hotKey.Modifiers.GetHashCode(), hotKey.KeyCode.GetHashCode());
        //        if (!(ret))
        //        {
        //            // Remove the atom:
        //            UnmanagedMethods.GlobalDeleteAtom(id);
        //            // failed
        //            throw new HotKeyAddException("Failed to register HotKey");
        //        }
        //        else
        //        {
        //            hotKey.AtomName = atomName;
        //            hotKey.AtomId = id;
        //        }
        //    }
        //}

        private void getProcessIdToSessionId(ref uint myApplicationProcessId, ref uint myApplicationSessionId, ref string myApplicationName)
        {
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            myApplicationProcessId = Convert.ToUInt32(currentProcess.Id);
            myApplicationName = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath).Replace(".EXE", "");
            ProcessIdToSessionId(myApplicationProcessId,ref myApplicationSessionId);
        }
        private bool HotKeyFound(string atomName, ref IntPtr myfindAtom)
        {
            bool _isHotKeyRegistered = false;
            myfindAtom = UnmanagedMethods.GlobalFindAtom(atomName);
            if (myfindAtom.Equals(IntPtr.Zero))
            {
                _isHotKeyRegistered = false;
            }
            else
            {
                _isHotKeyRegistered = true;
            }
            return _isHotKeyRegistered;
        }
        private void AddHotKey(HotKey hotKey)
        {
            // generate the id:
            //Dim atomName As String = hotKey.Name + "_" + UnmanagedMethods.GetTickCount().ToString()

            //'Variable decalaration 
            bool _isAlreadyExists = false;
            string myApplicationName = "";
            uint myApplicationProcessId = 0;
            uint myApplicationSessionId = 0;
            getProcessIdToSessionId(ref myApplicationSessionId,ref myApplicationSessionId,ref myApplicationName);
            string myAtomName = myApplicationSessionId.ToString() + "_" + myApplicationProcessId.ToString() + "_" + myApplicationName + "_" + hotKey.Name + "_";
            //    Dim myTickCount As UInteger = UnmanagedMethods.GetTickCount()
            int myKeyCode = Convert.ToInt32(hotKey.KeyCode);
            int myKeyModifier = Convert.ToInt32(hotKey.Modifiers);
            IntPtr id = IntPtr.Zero;

            string atomName = myAtomName + "_" + myKeyCode.ToString() + "_" + myKeyModifier.ToString();
            if ((atomName.Length > 255))
            {
                atomName = atomName.Substring(0, 255);
            }
            if ((HotKeyFound(atomName,ref id)))
            {
                _isAlreadyExists = true;
            }
            else
            {
                id = UnmanagedMethods.GlobalAddAtom(atomName);
            }
            //Dim orgTickCount As UInteger = myTickCount


            //'code for
            // '' to find the registered hotkeys
            //Try
            //    Do While (True)
            //        If (atomName.Length > 255) Then
            //            atomName = atomName.Substring(0, 255)
            //        End If
            //        If (HotKeyFound(atomName)) Then
            //            If (myTickCount = UInteger.MaxValue) Then
            //                myTickCount = UInteger.MinValue
            //            Else
            //                myTickCount = myTickCount + 1
            //            End If

            //            If (myTickCount = orgTickCount) Then
            //                Exit Do
            //            End If
            //            atomName = myAtomName + myTickCount.ToString()
            //        Else
            //            Exit Do
            //        End If
            //    Loop
            //Catch
            //    Throw New HotKeyAddException("Failed to Find GlobalAtom for HotKey")
            //End Try

            // Create a new atom:
            //'id = UnmanagedMethods.GlobalAddAtom(atomName)
            if ((id.Equals(IntPtr.Zero)))
            {
                // failed
                throw new HotKeyAddException("Failed to add GlobalAtom for HotKey");
            }
            else
            {
                // succeeded:
                bool ret = UnmanagedMethods.RegisterHotKey(ownerForm.Handle, id.ToInt32(), hotKey.Modifiers.GetHashCode(), hotKey.KeyCode.GetHashCode());
                int myerror = Marshal.GetLastWin32Error();
                if (!_isAlreadyExists)
                {
                    if (!(ret))
                    {
                        // Remove the atom:
                        UnmanagedMethods.GlobalDeleteAtom(id);
                        // failed
                        throw new HotKeyAddException("Failed to register HotKey : " + myerror.ToString());

                    }
                    else
                    {
                        hotKey.AtomName = atomName;
                        hotKey.AtomId = id;
                    }
                }
                else
                {
                    hotKey.AtomName = atomName;
                    hotKey.AtomId = id;
                }

            }
        }



        public HotKeyCollection(System.Windows.Forms.Form ownerForm)
        {
            this.ownerForm = ownerForm;
        }

    }

    public class HotKeyAddException : System.Exception
    {

        public HotKeyAddException()
            : base()
        {
        }

        public HotKeyAddException(string message)
            : base(message)
        {
        }

        public HotKeyAddException(string message, System.Exception innerException)
            : base(message, innerException)
        {
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
        private System.Windows.Forms.Keys m_keyCode;

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

        public System.Windows.Forms.Keys KeyCode
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
            if (((KeyCode == System.Windows.Forms.Keys.Alt) | (KeyCode == System.Windows.Forms.Keys.Control) | (KeyCode == System.Windows.Forms.Keys.Shift) | (KeyCode == System.Windows.Forms.Keys.ShiftKey) | (KeyCode == System.Windows.Forms.Keys.ControlKey)))
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

        public HotKey(string name, System.Windows.Forms.Keys keyCode, HotKeyModifiers modifiers)
        {
            m_name = name;
            m_keyCode = keyCode;
            m_modifiers = modifiers;
        }

    }
    internal class UnmanagedMethods
    {

        ///* SHIFT-PRINTSCRN  */
        internal const int IDHOT_SNAPWINDOW = -1;
        ///* PRINTSCRN        */
        internal const int IDHOT_SNAPDESKTOP = -2;
        internal const int WM_HOTKEY = 0x312;
        /// <summary>
        /// By Mahesh On 20071128
        /// TO Add The Constant for  WM_ACTIVATEAPP 
        /// </summary>
        internal const int WM_ACTIVATEAPP = 0x1c;

        /// <remarks>
        /// TO Capture Activate & InActivation of Application
        /// </remarks>

        [DllImport("kernel32")]
        public static extern IntPtr GlobalFindAtom(string lpString);
        [DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("kernel32")]
        public static extern IntPtr GlobalAddAtom(string lpString);
        [DllImport("kernel32")]
        public static extern IntPtr GlobalDeleteAtom(IntPtr nAtom);
        [DllImport("kernel32")]
        public static extern int GetTickCount();
        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);
        internal const int WM_SYSCOMMAND = 0x112;

        internal const int SC_RESTORE = 0xf120;
        [DllImport("user32")]
        public static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32")]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        internal const int SW_SHOW = 5;

        /// 

    }
}