using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloAUSLibrary
{

    public partial class MasterForm : gloGlobal.Common.TriarqFormWithFocusListner
    {
        #region Variables   

        private Object[] objects;

     //   private string[] modules;

        #endregion Variables

        // Declare a Name property of type string:
        #region Property        

        public Object[] FormControls
        {
            get
            {
                return objects;
            }
            set
            {
                objects = value;
            }
        }
        public string strProviderID { get; set; }
        private bool _IsValidLicense { get; set; }
      
        #endregion Property

        #region Constructor

        public MasterForm()
        {
            InitializeComponent();           
        }      
              
        #endregion Constructor

        #region Function
       
        public void SetChildFormControls( )
        {
            if (FormControls  != null)
            {
                if (FormControls.Length > 0)
                {
                    _IsValidLicense = gloAUSLibrary.Class.clsgloLicenseParameters.IsLicenseValid;
                    CheckNonProvideruser(strProviderID);

                    for (int i = 0; i < FormControls.Length; i++)
                    {
                        Control _control = null;
                        ToolStripButton _toolstrip = null;
                        ContextMenuStrip _contextMenuStrip = null;
                        ToolStripMenuItem _toolStripMenuItem = null;

                        if (FormControls[i] != null)
                        {
                            if (FormControls[i] is Control)
                            {

                                _control = (Button)FormControls[i];
                                _control.Enabled = _IsValidLicense; //gloAUSLibrary.Class.clsgloLicenseParameters.IsLicenseValid;
                              //  clsGeneral.UpdateLog("MasterForm - in SetChildFormControls - control given - :" + _control.Name.ToString());
                            }
                            else
                                if (FormControls[i] is ToolStripButton)
                                {
                                    _toolstrip = (ToolStripButton)FormControls[i];
                                    _toolstrip.Enabled = _IsValidLicense; //gloAUSLibrary.Class.clsgloLicenseParameters.IsLicenseValid; ;
                                 //   clsGeneral.UpdateLog("MasterForm - in SetChildFormControls - toolstrip given - :" + _toolstrip.Name.ToString());
                                }
                                else
                                    if (FormControls[i] is ContextMenuStrip)
                                    {
                                        _contextMenuStrip = (ContextMenuStrip)FormControls[i];
                                        _contextMenuStrip.Enabled = _IsValidLicense; //gloAUSLibrary.Class.clsgloLicenseParameters.IsLicenseValid; ;
                                      //  clsGeneral.UpdateLog("MasterForm - in SetChildFormControls - contextMenuStrip given - :" + _contextMenuStrip.Name.ToString());
                                    }
                                    else
                                        if (FormControls[i] is ToolStripMenuItem)
                                        {
                                            _toolStripMenuItem = (ToolStripMenuItem)FormControls[i];
                                            _toolStripMenuItem.Enabled = _IsValidLicense; //gloAUSLibrary.Class.clsgloLicenseParameters.IsLicenseValid; ;                                           
                                        }
                        }
                        else
                        {
                           // clsGeneral.UpdateLog("MasterForm - in SetChildFormControls - object is null ");
                        }
                        
                    }
                }
            }
            else
            {
               // clsGeneral.UpdateLog("MasterForm - in SetChildFormControls - FormControls is null ");
            }          
        }

        private void CheckNonProvideruser(string _sProviderID)
        {
            try
            {
                if (!string.IsNullOrEmpty(_sProviderID))
                {
                    if (!string.IsNullOrEmpty(gloAUSLibrary.Class.clsgloLicenseParameters.sProviderLicenseIDs))
                    {
                        if (_sProviderID != "0" && _sProviderID != "")
                        {
                            if (gloAUSLibrary.Class.clsgloLicenseParameters.sProviderLicenseIDs.Contains(_sProviderID) == true)
                            {
                                //gloAUSLibrary.Class.clsgloLicenseParameters.IsLicenseValid = false;
                                _IsValidLicense = false;
                            }
                            else
                            {
                                _IsValidLicense = true;
                            }
                        }
                    }
                    else
                    {
                        _IsValidLicense = true;
                    }
                }
                else
                {
                    _IsValidLicense = gloAUSLibrary.Class.clsgloLicenseParameters.IsLicenseValid;
                }
            }
            catch (Exception)// ex)
            {
                // throw;
            }
        }

        public Boolean SetChildFormModules(string smodulename, string sDescription, string sProviderID)
        {
            Boolean bRestricted = false;
            if (smodulename != null)
            {
                CheckNonProvideruser(sProviderID);
                if (!_IsValidLicense)
                {
                    string result = "The selected provider has not yet obtained a valid license from TRIARQ Health." + Environment.NewLine + " " + sDescription + " is restricted.";
                    MessageBox.Show(result, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                  //  clsGeneral.UpdateLog("MasterForm - SetChildFormModules - message shown to user :" + result);
                    bRestricted = true;                   
                }
                else
                {
                  //  clsGeneral.UpdateLog("License is Valid ");
                }
            }
            return bRestricted;
        }

        public string ValidateLogin(Int64 nLoginProviderID, string sConstr)
        {
            string sResult = string.Empty;
            try 
	        {	        
		        gloAUSLibrary.Class.clsgloLicence oAUS=new Class.clsgloLicence("",sConstr );

                sResult = oAUS.IsValidProviderLicense(nLoginProviderID, out gloAUSLibrary.Class.clsgloLicenseParameters.sProviderLicenseIDs);
                
                oAUS.Dispose();                
	        }
	        catch (Exception)// ex)
	        {		
		       // throw;
	        }

            return sResult;
        }

        #endregion Function

    }
}
