using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClearGage
{
    public partial class frmCGWebBrowser : Form
    {
        public frmCGWebBrowser()
        {
            InitializeComponent();

        }

        public frmCGWebBrowser(ClearGage.SSO.SsoHelper oSSOHelper)
        {
            InitializeComponent();

            webBrowser1.ObjectForScripting = oSSOHelper;
            oSSOHelper.DefaultDialogHeight = webBrowser1.Width - (System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + System.Windows.Forms.SystemInformation.VerticalResizeBorderThickness);
            oSSOHelper.DefaultDialogHeight = webBrowser1.Width - (System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight + System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness);
        }

        public void LoadContent(string content)
        {
            webBrowser1.DocumentText = content;
        }
    }

    
}
