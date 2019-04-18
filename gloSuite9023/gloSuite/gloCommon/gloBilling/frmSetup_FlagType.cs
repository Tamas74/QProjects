using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmSetup_FlagType : Form
    {
        #region   "Declararions"
        private Int64 _flagtypeID = 0;
        private string _databaseconnectionstring;
      //  private Int64 _CPTCode;
        public string _MessageBoxCaption = String.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 flagtypeID
        {
            get { return _flagtypeID; }
            set { _flagtypeID = value; }
        } 
        #endregion

        #region "Constructors"
        public frmSetup_FlagType(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
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
        public frmSetup_FlagType(Int64 ID, string databaseconnectionstring)
        {
            InitializeComponent();
            _flagtypeID = ID;
            _databaseconnectionstring = databaseconnectionstring;
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
        #endregion  

        #region "Form Load Event"
       
        private void frmSetup_FlagType_Load(object sender, EventArgs e)
        {
            if (_flagtypeID != 0)
            {
                FlagType oFtype = new FlagType(_databaseconnectionstring);
                DataTable dtflagtype;
                dtflagtype = oFtype.GetFlagtype(_flagtypeID);

                if (dtflagtype != null)
                {
                    if (dtflagtype.Rows.Count != 0)
                    {
                        txtFlagtypeCode.Text = dtflagtype.Rows[0]["FlagtypeCode"].ToString();
                        txtFlagtypeDesc.Text = dtflagtype.Rows[0]["FlagtypeDesc"].ToString();
                        pictureBox1.BackColor = Color.FromArgb(Convert.ToInt32(dtflagtype.Rows[0]["ColorCode"]));
                    }
                }
                oFtype.Dispose();

            }

        }
        
        #endregion

        #region "Form  Control Events "
        #region 'Tool Strip Events'

        private void ts_Commands_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "SaveClose":
                    if (Save())
                    {
                        this.Close();
                    }

                    break;
                case "Save":
                    if (Save())
                    {
                        txtFlagtypeCode.Text = "";
                        txtFlagtypeDesc.Text = ""; 
                        pictureBox1.BackColor = Color.White;
                        _flagtypeID = 0;
                    }
                    break;
                case "Cancel":
                    this.Close();
                    break;
            }

        }

         #endregion

        private void btnBrowseAppColor_Click(object sender, EventArgs e)
        {
          //  ColorDialog clDlg = new ColorDialog();
            try
            {
                colorDialog1.CustomColors = gloGlobal.gloCustomColor.customColor;
            }
            catch
            {
            }
            DialogResult dResult = colorDialog1.ShowDialog(this);
            //Code Added by Mayuri:20091203
            //To fix issue:#Edit >Billing configuration > Flag type > while click on cancel still showing black flag 
            if (dResult == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
                try
                {
                    gloGlobal.gloCustomColor.customColor = colorDialog1.CustomColors;
                }
                catch
                {
                }
            }
          //  clDlg.Dispose();
          //  clDlg = null;

            //End code Added by Mayuri:20091203
        } 
        #endregion

        #region "Save Method"
        private bool Save()
        {
            if (txtFlagtypeCode.Text.Trim() == "")
            {
                MessageBox.Show("Enter a code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFlagtypeCode.Focus();
                return false;

            }
            if (txtFlagtypeDesc.Text.Trim() == "")
            {
                MessageBox.Show("Enter a description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFlagtypeDesc.Focus();
                return false;
            }
            try
            {
                FlagType oFtype = new FlagType(_databaseconnectionstring);
                oFtype.ClinicID = this.ClinicID;
                oFtype.flagtypeID = _flagtypeID;
                oFtype.flagtypeCode = Convert.ToString(txtFlagtypeCode.Text.Trim());
                oFtype.flagtypeDesc = Convert.ToString(txtFlagtypeDesc.Text.Trim());
                oFtype.ColorCode = pictureBox1.BackColor.ToArgb();
                if (oFtype.IsExistsFlagType(oFtype.flagtypeID, oFtype.flagtypeCode, oFtype.flagtypeDesc))
                {
                    MessageBox.Show(" Code is already in use by another entry.  Select a unique code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;

                }
                _flagtypeID = oFtype.Add();
                if (_flagtypeID > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.FlagType, ActivityType.Add, "Add Flag Type", 0, _flagtypeID, 0, ActivityOutCome.Success);
                    return true;
                }
                else 
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.FlagType, ActivityType.Add, "Add Flag Type", 0, _flagtypeID, 0, ActivityOutCome.Failure);
                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.FlagType, ActivityType.Add, "Add Flag Type", 0, _flagtypeID, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            return false;
        } 
        #endregion

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Yellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }
         
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}