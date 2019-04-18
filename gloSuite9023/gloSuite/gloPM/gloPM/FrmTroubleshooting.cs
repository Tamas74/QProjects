using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloPM
{
    public partial class FrmTroubleshooting : Form
    {

        #region "Declarations"

        private string _MessageBoxCaption = string.Empty;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings; 

        #endregion "Declarations"

        #region "Constructor"
        
            public FrmTroubleshooting()
            {
                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _MessageBoxCaption = "";
                    }
                }
                else
                { _MessageBoxCaption = ""; }

                #endregion
                InitializeComponent();

            }

        #endregion "Constructor"


            

       
    }
}