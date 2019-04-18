using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace gloGeneralItem.ScreenResolution
{
    public static class gloScreenResolution
    {
        
        public static void BeforeControlLoad(Form ControlParentForm, out int myScreenWidth, out int myScreenHeight, out bool toChangeHeight, out bool toChangeWidth)
        {
            myScreenWidth = (int)Screen.PrimaryScreen.WorkingArea.Width - 10;
            myScreenHeight = (int)Screen.PrimaryScreen.WorkingArea.Height - 10;
            toChangeHeight = false;
            toChangeWidth = false;
            if (myScreenHeight < ControlParentForm.Height)
            {
                toChangeHeight = true;
            }
            else
            {
                myScreenHeight = ControlParentForm.Height;
            }
            if (myScreenWidth < ControlParentForm.Width)
            {
                toChangeWidth = true;
            }
            else
            {
                myScreenWidth = ControlParentForm.Width;
            }
        }

        public static void AfterControlLoad(int myScreenWidth, int myScreenHeight, bool toChangeHeight, bool toChangeWidth, Form ControlParentForm, out bool scrollToControl)
        {
            scrollToControl = false;

            if (toChangeHeight || toChangeWidth)
            {
                ControlParentForm.Size = new Size(myScreenWidth + (toChangeHeight ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0), myScreenHeight + (toChangeWidth ? System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight : 0));
                scrollToControl = true;
            }

        }

    }
}
