using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//test1
namespace gloPatient
{
    public partial class frmQuickNotes : Form
    {

        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        Int64 _ClinicID = 0;
        string _nNote;
        gloListControl.gloListControl oListControl = null;
        int _NoteType = 0;
        public string Note
        {
            get { return _nNote; }
            set { _nNote = value; }
        }


        public frmQuickNotes(int Notetype)
        {
            InitializeComponent();
            _NoteType = Notetype;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #region " Retrieve MessageBoxCaption from AppSettings "

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

            #endregion

        }


        private void frmQuickNotes_Load(object sender, EventArgs e)
        {
           if (_NoteType == 5)
            {
                oListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.RemarkCode, true, this.Width);
                oListControl.ControlHeader = "Remark Code";
            }
           else if (_NoteType == 6)
           {
               oListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.ReasonCode, true, this.Width);
               oListControl.ControlHeader = "Reason Code";
           }
           else
           {
               oListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.BillingQuickNotes, false, this.Width);
               oListControl.ControlHeader = " Quick Notes";
           }

            oListControl.ClinicID = _ClinicID;
            
            oListControl.QuickNoteTypes = _NoteType;
            oListControl.CloseOnDoubleClick = false;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            this.Controls.Add(oListControl);

            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
        }

        private void removeListControl()
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch
                {
                }

                oListControl.Dispose();
                oListControl = null;
            }
        }

        #region " List Control Events "

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            if (oListControl.SelectedItems.Count > 0)
            {

                if (oListControl.ControlHeader == "Reason Code" || oListControl.ControlHeader == "Remark Code")
                {
                    foreach (gloGeneralItem.gloItem oItem in oListControl.SelectedItems)
                    {
                        string a = oListControl.ControlHeader;
                        if (_nNote != "" && _nNote != null)
                        {
                            if (oItem.Description == "")
                            {
                                _nNote = _nNote + ";" + oItem.Code;
                            }
                            else
                            {
                                _nNote = _nNote + ";" + oItem.Code + " - " + oItem.Description;
                            }
                        }
                        else
                        {
                            if (oItem.Description == "")
                            {
                                _nNote = oItem.Code;
                            }
                            else
                            {
                                _nNote = oItem.Code + " - " + oItem.Description;
                            }
                        }

                    }
                }
                else
                {
                    _nNote = oListControl.SelectedItems[0].Code;
                }
               
            }
            oListControl_ItemClosedClick(null, null);
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            removeListControl();
            this.Close();
        }
        #endregion " List Control Events "
    }
}
