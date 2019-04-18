using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace gloSettings
{ 


    #region " Enumerations "

    public enum enumModuleName
    { 
        None = 0,
        Dashboard = 1,
        PatientDetails = 2
    }

    public enum enumToolStripButtons
    { 
        
    }

    #endregion " Enumerations "

    public partial class frmToolButtonSelection : Form
    {

        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _DatabaseConnectionString = "";
        //private string _messageBoxCaption = "gloPM";

        private string _messageBoxCaption = String.Empty;

        private Int64 _PatientID = 0;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";

        private ToolStrip _toolStrip = null;
        private enumModuleName _moduleName = enumModuleName.None;
        private ArrayList _selectedButtons = new ArrayList();
        private ToolStripSeparator _ToolSeperator = null;
        private ArrayList _DefaultToolStrip = new ArrayList();
        public static Microsoft.VisualBasic.Collection ToolButtons = null;
        private ToolTip oToolTip;

        #endregion " Variable Declarations "

        #region " Property Procedures "

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmToolButtonSelection(string databaseconnectionstring,ToolStrip Tls, enumModuleName ModuleName, ArrayList arrDefaultToolStrip)
        {

            InitializeComponent();

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion


            _DatabaseConnectionString = databaseconnectionstring;
            _toolStrip = Tls;
            _moduleName = ModuleName;
            _DefaultToolStrip = arrDefaultToolStrip;
            Fill_ToolButtons();
        }

        #endregion " Constructor "

        #region " Form Events "

        private void frmToolButtonSelection_Load(object sender, EventArgs e)
        {
          
            try
            {
                btnAdd.BackgroundImage = global::gloSettings.Properties.Resources.Forward;
                btnRemove.BackgroundImage = global::gloSettings.Properties.Resources.Rewind;
                btnUp.BackgroundImage = global::gloSettings.Properties.Resources.UP;
                btnDown.BackgroundImage = global::gloSettings.Properties.Resources.Down; 

                oToolTip = new ToolTip();
                oToolTip.SetToolTip(btnAdd, "Add Button");
                oToolTip.SetToolTip(btnRemove, "Remove Button");
                oToolTip.SetToolTip(btnUp, "Move Up");
                oToolTip.SetToolTip(btnDown, "Move Down");

                Fill_ToolButtons();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        
        private void frmToolButtonSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                oToolTip.RemoveAll();
                oToolTip.Dispose();
                oToolTip = null;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        #endregion " Form Events "

        #region " Public & Private Procedures "

        private void Fill_ToolButtons()
        {
            try
            {
                int i = 0;
                int j = 0;

                trvButtons.Nodes.Clear();
                trvSelectedButtons.Nodes.Clear();

                //' INSERT SEPERATOR IN TREE '' 
                if (_moduleName == enumModuleName.Dashboard)
                {
                    trvButtons.Nodes.Add("Separator");
                }
                //' '' 

                //' INITIALIZE IMAGELIST TO TREEVIEW '' 
                switch (_moduleName)
                {
                    case enumModuleName.Dashboard:
                        trvButtons.ImageList = imgDashBoard;
                        trvSelectedButtons.ImageList = imgDashBoard;
                        trvButtons.ItemHeight = 50;
                        trvSelectedButtons.ItemHeight = 50;
                        break;
                    case enumModuleName.PatientDetails:
                        trvButtons.ImageList = imgPatientDetails;
                        trvSelectedButtons.ImageList = imgPatientDetails;
                        trvButtons.ItemHeight = 20;
                        trvSelectedButtons.ItemHeight = 20;
                        break;
                }
                //' '' 

                for (i = 0; i <= _toolStrip.Items.Count - 1; i++)
                {

                    string buttonName1 = string.Empty;

                    if (_toolStrip.Items[i].Tag != null)
                    {
                        buttonName1 = _toolStrip.Items[i].Tag.ToString();
                    }

                    if (buttonName1 != "Scan Patient" && buttonName1 != "Remittance" && buttonName1 != "Balance"  && buttonName1 != "Payment")
                    {
                        //condition buttonName1 != "Exit" added for not showing Exit button in tree view trvSelectedButtons.
                        if (_moduleName == enumModuleName.Dashboard && buttonName1 != "Exit")
                        {
                            if (_toolStrip.Items[i].ToolTipText != null)
                            {
                                if (_toolStrip.Items[i].ToolTipText.Trim() != "")
                                {
                                    InsertNode(i);
                                }
                            }
                            else
                            {
                                if (_toolStrip.Items[i].Visible == true | _toolStrip.Items[i].IsOnOverflow == true)
                                {
                                    trvSelectedButtons.Nodes.Add("Separator");
                                }
                            }
                        }
                        else if (_moduleName == enumModuleName.PatientDetails)
                        {
                            if (_toolStrip.Items[i].ToolTipText != null)
                            {
                                if (_toolStrip.Items[i].ToolTipText.Trim() != "")
                                {
                                    InsertNode(i);
                                }
                            }
                        }
                    }
                    buttonName1 = null;
                }


                gloToolbarCustomization obtnSelection = new gloToolbarCustomization(_DatabaseConnectionString);
                DataTable dt = default(DataTable);
                dt = obtnSelection.GetGetButtonSelection(UserID, _moduleName);
                string[] arrButtons = null;
                string strButtons = null;
                //if (dt != null && dt.Rows.Count>0 && Convert.ToString(dt.Rows[0]["sButtons"]).Trim() != "")
                //{
                  if ((dt != null))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            strButtons = dt.Rows[0]["sButtons"].ToString();
                            arrButtons = strButtons.Split(',');
                            _selectedButtons.Clear();
                            for (j = 0; j <= arrButtons.Length - 1; j++)
                            {
                                _selectedButtons.Add(arrButtons[j]);
                            }
                        }
                        else
                        {
                            for (i = 0; i <= _toolStrip.Items.Count - 1; i++)
                            {
                                if ((_toolStrip.Items[i].Visible == true || _toolStrip.Items[i].IsOnOverflow == true) && (_toolStrip.Items[i].Tag != null))
                                {
                                    if (_moduleName == enumModuleName.Dashboard)
                                    {
                                        _selectedButtons.Add(_toolStrip.Items[i].Tag.ToString());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (i = 0; i <= _toolStrip.Items.Count - 1; i++)
                        {
                            if (_toolStrip.Items[i].Visible == true && (_toolStrip.Items[i].Tag != null))
                            {
                                _selectedButtons.Add(_toolStrip.Items[i].Tag.ToString().Trim());
                            }
                        }

                    }
                  if (obtnSelection != null) { obtnSelection.Dispose(); obtnSelection = null; }
                  if (dt != null) { dt.Dispose(); dt = null; }
                  arrButtons = null;
                  strButtons = null;
                }
                //else
                //{
                //    for (i = 0; i <= _toolStrip.Items.Count - 1; i++)
                //    {
                //        if (_toolStrip.Items[i].Tag != null)
                //        {
                //            _toolStrip.Items[i].Visible = true;
                //        }
                //        else
                //        {
 
                //        }
                        
                //    }
                //}
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void InsertNode(int btnIndex)
        {
            TreeNode oNode = null ;
            try
            {
                oNode = new TreeNode();
             

                //' SET NODE TEXT AS BUTTON TAG/TOOLTIP '' 
                if (_moduleName == enumModuleName.Dashboard)
                {
                    oNode.Text = _toolStrip.Items[btnIndex].ToolTipText.Trim();
                }
                else if (_moduleName == enumModuleName.PatientDetails)
                {
                    //Added By MaheshB
                    if (_toolStrip.Items[btnIndex].ToolTipText.Trim() != "Procedures")
                    {
                        oNode.Text = _toolStrip.Items[btnIndex].ToolTipText.Trim();
                    }
                }
                #region User Rights
                bool _showhidenode = false;
                gloUserRights.ClsgloUserRights Objuserrights = new gloUserRights.ClsgloUserRights(_DatabaseConnectionString);
                Objuserrights.CheckForUserRights(_UserName);
                switch (oNode.Text)
                {
                    case "Add New Patient":
                        if (Objuserrights.NewPatient == true) _showhidenode = true;
                        break;
                    case "Calendar":
                        if (Objuserrights.Calender == true) _showhidenode = true;
                        break;
                    case "Appointment":
                        if (Objuserrights.Appointment == true) _showhidenode = true;
                        break;
                    case "Charges":
                        if (Objuserrights.Appointment == true) _showhidenode = true;
                        break;
                    case "Batch":
                        if (Objuserrights.Batch == true) _showhidenode = true;
                        break;
                    //case "Payment":
                    //    if (Objuserrights.Payment == true) _showhidenode = true;//Commented By MaheshB For hiding Payment Button
                    //    break;
                    case "Balance":
                        if (Objuserrights.PatBalance == true) _showhidenode = true;
                        break;
                    case "Remittance":
                        if (Objuserrights.Remittance == true) _showhidenode = true;
                        break;
                    case "Ledger":
                        //if (Objuserrights.Ledger == true) _showhidenode = true;
                        _showhidenode = true;
                        break;
                    //case "Patient Ledger":
                    //    if (Objuserrights.Ledger == true) _showhidenode = true;
                    //    break;
                    case "Advance":
                        if (Objuserrights.Advance == true) _showhidenode = true;
                        break;
                    case "Modify Patient":
                        if (Objuserrights.ModifyPatient == true) _showhidenode = true;
                        break;
                    case "Scan Patient":
                        if (Objuserrights.ScanPatient == true) _showhidenode = true;
                        break;
                    case "Calculator":
                        _showhidenode = true;
                        break;
                    case "Exit":
                        _showhidenode = true;
                        break;
                    case "Lock Screen":
                        _showhidenode = true;
                        break;
                    case "Patient Payment":
                        _showhidenode = true;
                        break;
                    case "Insurance Payment":
                        _showhidenode = true;
                        break;
                    case "Show Dashboard":
                        _showhidenode = true;
                        break;
                    case "Patient Statement":
                        _showhidenode = true;
                        break;
                    case "Patient Account":
                        _showhidenode = true;
                        break;
                    case "Daily Close":
                        _showhidenode = true;
                        break;
                    case "ERA Payment":
                        _showhidenode = true;
                        break;
                    case "Revenue Cycle":
                           bool SettingsValue = false;
                            object oValue = null;
                            gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_DatabaseConnectionString);
                            ogloSettings.GetSetting("FOLLOWUP_FEATURE", 0, ClinicID, out oValue);
                            if (Convert.ToString(oValue).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValue).ToLower().Trim() == "False".ToLower())
                            {
                                SettingsValue = Convert.ToBoolean(oValue);
                            }
                            else if (Convert.ToString(oValue).Trim() == "1" || Convert.ToString(oValue).Trim() == "0")
                            {
                                SettingsValue = Convert.ToBoolean(Convert.ToString(oValue).Trim() == "1" ? "TRUE" : "FALSE");
                            }
                            if (ogloSettings != null) { ogloSettings.Dispose(); }
                            oValue = null;
                            if (SettingsValue)
                            {
                                _showhidenode = true;
                            }
                            else
                            {
                                _showhidenode = false;
                            }
                        break;
                    case "Scan Docs":
                        if (Objuserrights.ScanDocuments == true) _showhidenode = true;
                        break;
                    case "RCM Docs":
                        if (Objuserrights.RCMDocuments == true) _showhidenode = true;
                        break;
                    case "Cleargage Payment":
                       
                        object objCleargage = null;
                        gloSettings.GeneralSettings ogloSetting = new GeneralSettings(_DatabaseConnectionString);
                        try
                        {

                            ogloSetting.GetSetting("EnableCleargageFeature", out objCleargage);

                            if (!String.IsNullOrEmpty(Convert.ToString(objCleargage)))
                            {
                                gloGlobal.gloPMGlobal.IsCleargageEnable = Convert.ToBoolean(Convert.ToString(objCleargage).Trim());// == "1" ? "true" : "false");
                            }
                            else
                            {
                                gloGlobal.gloPMGlobal.IsCleargageEnable = false;
                            }
                            objCleargage = null;
                            if (gloGlobal.gloPMGlobal.IsCleargageEnable == true)
                            {
                                _showhidenode = true;
                            }
                            else
                            {
                                _showhidenode = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
             
                               
                        break;
                }
                try
                {
                    if (Objuserrights != null)
                    {
                        Objuserrights.Dispose();
                        Objuserrights = null;
                    }
                }
                catch
                {
                }
                #endregion
                //string _str=Enum.GetName(typeof(enumModuleName), enumModuleName.Dashboard);
                if (_showhidenode == true || _moduleName == enumModuleName.PatientDetails)
                {
                    //' COPY IMAGE TO PERTUCULAR IMAGELIST '' 
                    switch (_moduleName)
                    {
                        case enumModuleName.Dashboard:
                            imgDashBoard.Images.Add(_toolStrip.Items[btnIndex].Image);
                            oNode.ImageIndex = imgDashBoard.Images.Count - 1;
                            oNode.SelectedImageIndex = imgDashBoard.Images.Count - 1;
                            break;
                        case enumModuleName.PatientDetails:
                            imgPatientDetails.Images.Add(_toolStrip.Items[btnIndex].Image);
                            oNode.ImageIndex = imgPatientDetails.Images.Count - 1;
                            oNode.SelectedImageIndex = imgPatientDetails.Images.Count - 1;
                            break;
                    }


                    if (_toolStrip.Items[btnIndex].Visible == true || _toolStrip.Items[btnIndex].IsOnOverflow == true)
                    {
                        trvSelectedButtons.Nodes.Add(oNode);
                    }
                    else if (_toolStrip.Items[btnIndex].ToolTipText.Trim() != "Procedures")
                    {
                        trvButtons.Nodes.Add(oNode);

                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                oNode = null;
            }
        }

        private void InsertButton(int buttonIndex)
        {
            ToolStripItem _ToolButton = default(ToolStripItem);
            _ToolButton = _toolStrip.Items[buttonIndex];
            _toolStrip.Items.RemoveAt(buttonIndex);
            _ToolButton.Visible = true;
            _toolStrip.Items.Add(_ToolButton);

            //if (_moduleName == enumModuleName.Dashboard)
            //{
            //    _ToolSeperator = new ToolStripSeparator();
            //    _toolStrip.Items.Add(_ToolSeperator);
            //}
        }

        private void FindButton(string buttonName)
        {
           // ToolStripButton _toolButton = default(ToolStripButton);
            try
            {
                for (int i = 0; i <= _toolStrip.Items.Count - 1; i++)
                {
                    if (_moduleName == enumModuleName.Dashboard)
                    {
                        if (_toolStrip.Items[i].ToolTipText != null)
                        {
                            if (_toolStrip.Items[i].ToolTipText.Trim() == buttonName)
                            {
                                //' MOVING BUTTON '' 
                                InsertButton(i);
                                return;
                            }
                            //' MOVING BUTTON '' 
                        }

                    }
                    if (_moduleName == enumModuleName.PatientDetails)
                    {
                        if (_toolStrip.Items[i].ToolTipText != null)
                        {
                            if (_toolStrip.Items[i].ToolTipText.Trim() == buttonName)
                            {
                                //' MOVING BUTTON '' 
                                InsertButton(i);
                                return;
                            }
                            //' MOVING BUTTON '' 
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Button : " + buttonName + Environment.NewLine + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetSettings()
        {
            string[] strButton1=null;
            string buttonName1 = string.Empty;
            try
            {

                if (_DefaultToolStrip == null)
                { return; }
                else if (_DefaultToolStrip.Count == 0)
                 { return; }


                //MaheshB

                for (int i = _DefaultToolStrip.Count - 1; i >= 0; i--)
                {
                    strButton1 = _DefaultToolStrip[i].ToString().Split('.');
                    buttonName1 = strButton1[0];

                    if (buttonName1 == "Scan Patient" || buttonName1 == "Remittance" || buttonName1 == "Balance" ||  buttonName1 == "Daily Close" || buttonName1 == "Payment")
                    {
                        _DefaultToolStrip.RemoveAt(i);
                    }
                    if (buttonName1 == "Revenue Cycle")
                    {
                        bool SettingsValue = false;
                        object oValue = null;
                        gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_DatabaseConnectionString);
                        ogloSettings.GetSetting("FOLLOWUP_FEATURE", 0, ClinicID, out oValue);
                        if (Convert.ToString(oValue).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValue).ToLower().Trim() == "False".ToLower())
                        {
                           SettingsValue = Convert.ToBoolean(oValue);
                        }
                        else if (Convert.ToString(oValue).Trim() == "1" || Convert.ToString(oValue).Trim() == "0")
                        {
                            SettingsValue = Convert.ToBoolean(Convert.ToString(oValue).Trim() == "1" ? "TRUE" : "FALSE");
                        }
                        if (ogloSettings != null) { ogloSettings.Dispose(); }
                        if (!SettingsValue)
                        {
                            _DefaultToolStrip.RemoveAt(i);
                        }
                        oValue = null;
                    }
                    if (buttonName1 == "Cleargage Payment")
                    {
                        if (gloGlobal.gloPMGlobal.IsCleargageEnable == false)
                        {
                            _DefaultToolStrip.RemoveAt(i);
                        }
                    }
                   
                    
                }

                trvButtons.Nodes.Clear();
                trvSelectedButtons.Nodes.Clear();

                trvButtons.BeginUpdate();
                trvSelectedButtons.BeginUpdate();

                
                //' INSERT SEPERATOR IN TREE '' 
                if (_moduleName == enumModuleName.Dashboard)
                {
                    trvButtons.Nodes.Add("Separator");
                }
                //' '' 

                //' CLEAN PREVIOUS IMAGES FROM IMAGELIST '' 
                switch (_moduleName)
                {
                    case enumModuleName.Dashboard:
                        for (int i = imgDashBoard.Images.Count - 1; i >= 1; i += -1)
                        {
                            imgDashBoard.Images.RemoveAt(i);
                        }
                        break; 
                    case enumModuleName.PatientDetails:
                        for (int i = imgPatientDetails.Images.Count - 1; i >= 1; i += -1)
                        {
                            imgPatientDetails.Images.RemoveAt(i);
                        }
                        break;
                }
                //' '' 

                //' FILLING TREE WITH DEFAULT SETTINGS '' 
                string[] strButton = null;
                string buttonName = null;
                string buttonFlag = null;

                for (int i = 0; i <= _DefaultToolStrip.Count - 1; i++)
                {

                    //' SPLIT STRING '' 
                    strButton = _DefaultToolStrip[i].ToString().Split('.');
                    //strButton = Microsoft.VisualBasic.Strings.Split(_DefaultToolStrip(i), ".");
                    buttonName = strButton[0];
                    if (strButton.Length > 1)
                    {
                        buttonFlag = strButton[1];
                    }

                    if (_moduleName == enumModuleName.Dashboard)
                    {
                        if (buttonName == "|" || buttonName == "Separator")
                        { trvSelectedButtons.Nodes.Add("Separator");}
                        else
                        {InsertNodeAfterReset(buttonName, buttonFlag);}
                    }
                    if (_moduleName == enumModuleName.PatientDetails)
                    {
                        if (buttonName == "|" || buttonName == "Separator")
                        { trvSelectedButtons.Nodes.Add("Separator"); }
                        else if (buttonName.Trim() != "Procedures")
                        { InsertNodeAfterReset(buttonName, buttonFlag); }
                    }  
                    
                }
                strButton = null;
                buttonName = null;
                buttonFlag = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                strButton1 = null;
                buttonName1 = null;
                trvButtons.EndUpdate();
                trvSelectedButtons.EndUpdate();
            }
        }

        public ArrayList GetDefaultToolStripSetting()
        {
            ArrayList arrToolItems = new ArrayList();
            try
            {
                
                for (int i = 0; i <= _toolStrip.Items.Count - 1; i++)
                {
                    if (_toolStrip.Items[i].Tag != null)
                    {

                        if (Convert.ToString(_toolStrip.Items[i].Tag).Trim() == "")
                        {
                            continue;
                        }

                        if (_moduleName == enumModuleName.Dashboard)
                        {
                            if (_toolStrip.Items[i].Visible == true || _toolStrip.Items[i].IsOnOverflow == true)
                            {
                                arrToolItems.Add(_toolStrip.Items[i].Tag.ToString().Trim() + ".Visible");
                            }
                            else
                            {
                                arrToolItems.Add(_toolStrip.Items[i].Tag.ToString().Trim() + ".Invisible");
                            }
                        }
                        if (_moduleName == enumModuleName.PatientDetails)
                        {
                            if (_toolStrip.Items[i].Visible == true || _toolStrip.Items[i].IsOnOverflow == true)
                            {
                                arrToolItems.Add(_toolStrip.Items[i].Tag.ToString().Trim() + ".Visible");
                            }
                            else
                            {
                                arrToolItems.Add(_toolStrip.Items[i].Tag.ToString().Trim() + ".Invisible");
                            }
                        }
                    }
                    else
                    {
                        if (_toolStrip.Items[i].Visible == true & _moduleName == enumModuleName.Dashboard)
                        {
                            arrToolItems.Add("|");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return arrToolItems;
        }

        public void ShowButtonSelection()
        {
            gloToolbarCustomization obtnSelection = new gloToolbarCustomization(_DatabaseConnectionString);
            DataTable dt = null;
            string[] arrButtons = null;
            string strButtons = null;
            try
            {
                dt = obtnSelection.GetGetButtonSelection(UserID, _moduleName);

                if ((dt != null))
                {
                    if (dt.Rows.Count > 0)
                    {
                        strButtons = Convert.ToString(dt.Rows[0]["sButtons"]);
                    }
                    else
                    {
                        return;
                    }
                }
                arrButtons = strButtons.Split(',');
                _selectedButtons.Clear();

                for (int j = 0; j <= arrButtons.Length - 1; j++)
                {
                    _selectedButtons.Add(arrButtons[j].ToString().Trim());
                }
                //Added comment '&& Convert.ToString(dt.Rows[0]["sButtons"]) != ""' to allow empty value of sButtons from database.
                if (dt != null) //&& Convert.ToString(dt.Rows[0]["sButtons"]) != ""
                {
                    for (int i = _toolStrip.Items.Count - 1; i >= 0; i += -1)
                    {
                        if (_toolStrip.Items[i].Tag != null)
                        {
                            if (_moduleName == enumModuleName.Dashboard)
                            {
                                _toolStrip.Items[i].Visible = false;
                            }
                            if (_moduleName == enumModuleName.PatientDetails)
                            {
                                _toolStrip.Items[i].Visible = false;
                            }
                        }
                        else
                        {
                            _toolStrip.Items.RemoveAt(i);
                        }
                    }
                }

                for (int i = 0; i <= _selectedButtons.Count - 1; i++)
                {
                    if (_selectedButtons[i].ToString() == "|")
                    {
                        //' PIPE CHARACTER INDICATES SEPARATOR '' 
                        _ToolSeperator = new ToolStripSeparator();
                        _toolStrip.Items.Add(_ToolSeperator);
                    }
                    else if (_selectedButtons[i].ToString().Trim() != "")
                    {
                        FindButton(_selectedButtons[i].ToString().Trim());
                    }

                }
                //added to palce Default buttons on toolstrip.
                //Start
                if (_moduleName == enumModuleName.Dashboard)
                {
                    FindButton("Exit");
                }
                //End
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (obtnSelection != null) { obtnSelection.Dispose(); obtnSelection = null; }
                if (dt != null) { dt.Dispose(); dt = null; }
                arrButtons = null;
                strButtons = null;
            }
        }

        private void OKToolButtonSelection()
        {
            gloToolbarCustomization obtnSelection = null;
            try
            {
                int i = 0;
                if (_selectedButtons != null) { _selectedButtons.Clear(); }
                for (i = 0; i <= trvSelectedButtons.Nodes.Count - 1; i++)
                {
                    if (trvSelectedButtons.Nodes[i].Text == "Separator")
                    {
                        _selectedButtons.Add("|");
                    }
                    else
                    {
                        _selectedButtons.Add(trvSelectedButtons.Nodes[i].Text);
                    }
                }

                obtnSelection = new gloToolbarCustomization(_DatabaseConnectionString);
                if (obtnSelection.SaveButtonSelection(UserID, _moduleName, _selectedButtons) == true)
                {
                    ShowButtonSelection();
                }
                else
                {
                    MessageBox.Show("Error : Cannot save button selection ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (obtnSelection != null) { obtnSelection.Dispose(); obtnSelection = null; }
                this.Close();
            }
        }

        private void InsertNodeAfterReset(string buttonName, string buttonFlag)
        {
            TreeNode oNode = null;
            try
            {
                int foundIndex = 0;
                //' SEARCH FOR PERTICULAR TOOL BUTTON 
                for (int i = 0; i <= _toolStrip.Items.Count - 1; i++)
                {
                    if (_toolStrip.Items[i].Tag != null)
                    {
                        if (_moduleName == enumModuleName.Dashboard)
                        {
                            if (_toolStrip.Items[i].ToolTipText != null)
                            {
                                if (buttonName == _toolStrip.Items[i].ToolTipText.Trim())
                                {
                                    foundIndex = i;
                                    break; // TODO: might not be correct. Was : Exit For 
                                }
                            }
                        }
                        else
                        {
                            if (buttonName == _toolStrip.Items[i].Tag.ToString().Trim())
                            {
                                foundIndex = i;
                                break; // TODO: might not be correct. Was : Exit For 
                            }
                        }
                    }
                }
                //' SEARCH END '' 

                oNode = new TreeNode();

                //' SET NODE TEXT AS BUTTON NAME '' 
                oNode.Text = buttonName;

                //' COPY IMAGE TO PERTUCULAR IMAGELIST '' 
                switch (_moduleName)
                {
                    case enumModuleName.Dashboard:
                        imgDashBoard.Images.Add(_toolStrip.Items[foundIndex].Image);
                        oNode.ImageIndex = imgDashBoard.Images.Count - 1;
                        oNode.SelectedImageIndex = imgDashBoard.Images.Count - 1;
                        break;
                    case enumModuleName.PatientDetails:
                        imgPatientDetails.Images.Add(_toolStrip.Items[foundIndex].Image);
                        oNode.ImageIndex = imgPatientDetails.Images.Count - 1;
                        oNode.SelectedImageIndex = imgPatientDetails.Images.Count - 1;
                        break;
                }

                //' INSERT NODE '' 
                if (buttonFlag == "Visible")
                {
                    trvSelectedButtons.Nodes.Add(oNode);
                }
                else
                {
                    trvButtons.Nodes.Add(oNode);

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {oNode=null;}
        } 

        #endregion 

        #region " ToolStrip Button Click Event "

        private void ts_btnOk_Click(object sender, EventArgs e)
        {
            OKToolButtonSelection();
        }

        private void ts_btnReset_Click(object sender, EventArgs e)
        {
            ResetSettings();
        }

        private void ts_btnClearAll_Click(object sender, EventArgs e)
        {
            ts_btnClearAll.Visible = false;
            ts_btnSelectAll.Visible = true;
        }

        private void ts_btnSelectAll_Click(object sender, EventArgs e)
        {
            ts_btnSelectAll.Visible = false;
            ts_btnClearAll.Visible = true;
        }

        private void ts_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion " ToolStrip Button Click Event "

        #region  " Form Button Events "

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode oNode = new TreeNode();
                if ((trvButtons.SelectedNode == null) == false)
                {
                    oNode = ((TreeNode)trvButtons.SelectedNode.Clone());
                    if (oNode.Text != "Separator")
                    {
                        trvButtons.SelectedNode.Remove();
                    }
                    if ((trvSelectedButtons.SelectedNode == null) == false)
                    {
                        trvSelectedButtons.Nodes.Insert(trvSelectedButtons.SelectedNode.Index, oNode);
                    }
                    else
                    {
                        trvSelectedButtons.Nodes.Add(oNode);
                    }
                }
                oNode = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode oNode = default(TreeNode);
                if ((trvSelectedButtons.SelectedNode == null) == false)
                {
                    oNode = ((TreeNode)trvSelectedButtons.SelectedNode.Clone()); ;
                    trvSelectedButtons.SelectedNode.Remove();
                    if (oNode.Text != "Separator")
                    {
                        trvButtons.Nodes.Add(oNode);
                    }
                }
                oNode = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode oNode = default(TreeNode);
                int prevIndex = 0;
                if ((trvSelectedButtons.SelectedNode == null) == false)
                {
                    oNode = ((TreeNode)trvSelectedButtons.SelectedNode.Clone());
                    if ((oNode == null) == false)
                    {
                        if (trvSelectedButtons.SelectedNode.Index != 0)
                        {
                            prevIndex = trvSelectedButtons.SelectedNode.PrevNode.Index;
                            trvSelectedButtons.Nodes.Remove(trvSelectedButtons.SelectedNode);
                            trvSelectedButtons.Nodes.Insert(prevIndex, oNode);
                            trvSelectedButtons.SelectedNode = oNode;
                        }
                    }
                }
                oNode = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode oNode = default(TreeNode);
                int nextIndex = 0;
                if ((trvSelectedButtons.SelectedNode == null) == false)
                {
                    oNode =((TreeNode)trvSelectedButtons.SelectedNode.Clone());
                    if ((oNode == null) == false)
                    {
                        if (trvSelectedButtons.SelectedNode.Index != trvSelectedButtons.Nodes.Count - 1)
                        {
                            nextIndex = trvSelectedButtons.SelectedNode.NextNode.Index;
                            trvSelectedButtons.Nodes.Remove(trvSelectedButtons.SelectedNode);
                            trvSelectedButtons.Nodes.Insert(nextIndex, oNode);
                            trvSelectedButtons.SelectedNode = oNode;
                        }
                    }
                }
                oNode = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = global::gloSettings.Properties.Resources.ForwardHover; 
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = global::gloSettings.Properties.Resources.Forward; 
        }

        private void btnRemove_MouseHover(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloSettings.Properties.Resources.RewindHover; 
        }

        private void btnRemove_MouseLeave(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloSettings.Properties.Resources.Rewind; 
        }

        private void btnUp_MouseHover(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = global::gloSettings.Properties.Resources.UPHover; 
        }

        private void btnUp_MouseLeave(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = global::gloSettings.Properties.Resources.UP;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloSettings.Properties.Resources.Down;
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloSettings.Properties.Resources.DownHover;
        }

        #endregion  " Form Button Events "

        #region " Tree View Events "

        private void trvButtons_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if ((e.Node == null) == false)
                {
                    btnAdd_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void trvSelectedButtons_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if ((e.Node == null) == false)
                {
                    btnRemove_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        #endregion " Tree View Events "
        
    }
}