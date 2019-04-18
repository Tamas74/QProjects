using System;
using System.Data;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;

namespace gloEmdeonInterface.Forms
{
    public partial class frmAddSpirometryRace : Form
    {

        private long _CategoryID = 0;
        private string _gloDeviceConnString = string.Empty;
        private string _gloEMRConnectionString = string.Empty;
        private string _gstrMessageBoxCaption = string.Empty;
        private long _ClinicID = 0;
        private bool _isDataChanged = false;
        public bool IsDataChanged
        {
            get { return _isDataChanged; }
            set { _isDataChanged = value; }
        }
        #region "Constructor"

        public frmAddSpirometryRace(long CategoryID, string gloDeviceConnString, string gloEMRConnectionString)
        {

            InitializeComponent();
            _gloDeviceConnString = gloDeviceConnString;
            _gloEMRConnectionString = gloEMRConnectionString;
            _CategoryID = CategoryID;

            if (_CategoryID == 0)
            { txtSpiroRaceCode.ReadOnly = false; }
            else
            { txtSpiroRaceCode.ReadOnly = true; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            System.Collections.Specialized.NameValueCollection appSettings = null;
            try
            {
                appSettings = System.Configuration.ConfigurationManager.AppSettings;
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"].Length > 0)
                    {
                        _gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { _gstrMessageBoxCaption = "gloEMR"; }

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"].Length > 0)
                    {
                        _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                    }
                    else
                    {
                        _ClinicID = 1;
                    }
                }
                else
                { _ClinicID = 1; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error while reading values from AppSetting" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                appSettings = null;
            }
            #endregion
        }

        #endregion "Constructor"

        #region "Form Load & Methods"
        private void frmSpiroRace_New_Load(object sender, EventArgs e)
        {
            // add new spiro race
            if (_CategoryID == 0)
            {
                txtSpiroRaceCode.Tag = null;
                txtSpiroRaceCode.Text = string.Empty;
                txtSpiroRaceName.Text = string.Empty;
            }
            // modify Existing Spiro Race
            else
            {
                LoadSpiroDetail();
            }
        }

        private void LoadSpiroDetail()
        {
            String strSpiroRaceName = string.Empty;
            String strSpiroraceCode = String.Empty;
            clsCategoryMST objCatMst = null;
            try
            {
                objCatMst = new clsCategoryMST(_gloDeviceConnString);
                strSpiroraceCode = objCatMst.RetriveSpiroRace(out strSpiroRaceName,_CategoryID);
                txtSpiroRaceCode.Text = strSpiroraceCode;
                txtSpiroRaceName.Text = strSpiroRaceName;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmAddSpirometryRace.LoadSpiroDetail() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                if (objCatMst != null)
                {
                    objCatMst.Dispose();
                    objCatMst = null;
                }
                strSpiroRaceName = string.Empty;
                strSpiroraceCode = String.Empty;

            }
            //}
        }

        #endregion "Form Load & Methods"

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //Save Clicked
            if (tlbbtnPost.Name == e.ClickedItem.Name)
            {
                if (txtSpiroRaceCode.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Enter race code", _gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information  );
                    txtSpiroRaceCode.Focus();
                    return;
                }
                if (txtSpiroRaceName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Enter race name", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSpiroRaceName.Focus();
                    return;
                }
                // round race code if it having preceding 0
                Int32 RaceCode = 0;
                if (Int32.TryParse(txtSpiroRaceCode.Text.Trim(), out RaceCode) == true)
                {
                    txtSpiroRaceCode.Text = Convert.ToString(RaceCode);
                }
                gloEmdeonInterface.Classes.clsCategoryMST oclsCategoryMST = null;
                try
                {
                    oclsCategoryMST = new gloEmdeonInterface.Classes.clsCategoryMST(_gloDeviceConnString);
                    oclsCategoryMST.ConnectionString = _gloDeviceConnString;
                    if (_CategoryID == 0)
                    {
                        if (oclsCategoryMST.isExistsCategoryCode(txtSpiroRaceCode.Text.Trim()))
                        {
                            MessageBox.Show("Race code already present.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                    if (oclsCategoryMST.isExistsCategoryName(txtSpiroRaceName.Text.Trim(), _CategoryID))
                    {
                        MessageBox.Show("Race name already present.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_CategoryID == 0)
                    {
                        _CategoryID = gloEmdeonInterface.Classes.clsSpiroGeneralModule.GetSpUniqueID(_gloDeviceConnString);
                    }
                    oclsCategoryMST.CategoryCode = txtSpiroRaceCode.Text.Trim() ;
                    oclsCategoryMST.CategoryID = _CategoryID;
                    oclsCategoryMST.CategoryName = txtSpiroRaceName.Text.Trim() ;
                    oclsCategoryMST.CategoryType = "SPIRORACE";
                    oclsCategoryMST.ClinicID = 1;
                    if (oclsCategoryMST.Add(oclsCategoryMST) > 0)
                    {
                        IsDataChanged = true;
                        this.Close();
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmAddSpirometryRace.ts_LabMain_ItemClicked() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    ex = null;
                    IsDataChanged = false; 
                }
                finally
                {
                    if (oclsCategoryMST != null)
                    {
                        oclsCategoryMST.Dispose();
                        oclsCategoryMST = null;
                    }

                }
             
            }

           if (tlbbtnclose.Name == e.ClickedItem.Name)
            {
                IsDataChanged = false; 
                this.Close();

            }
        }
    
        private void txtSpiroRaceCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || (e.KeyChar == Convert.ToChar(8)))
            {
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
