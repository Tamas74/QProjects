using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace gloBilling
{
    public partial class frmSetupIDQualifierAssociation : Form
    {
        #region "Variable Declaration" 
        private String _databaseconnectionstring;
        private Int64 _QualifierAccociationID;
        private Int64 _QualifierMstID;
        private Int64 _ClinicID = 0;
        const int COL_ID = 0;
        const int COL_CODE = 1;
        const int COL_DESC = 2;
        const int COL_COUNT = 3;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = String.Empty;
       
        #endregion "Variable Declaration"

        #region "Form Constructor"
        public frmSetupIDQualifierAssociation(String _databaseconnectionstring, Int64 _QualifierAccociationID, Int64 _QualifierMstID)
        {
            InitializeComponent();

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
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }
            #endregion " Retrieve MessageBoxCaption from AppSettings "
            this._databaseconnectionstring = _databaseconnectionstring;
            this._QualifierAccociationID = _QualifierAccociationID;
            this._QualifierMstID = _QualifierMstID;
        }
        #endregion "Form Constructor"

        #region "Form Load Event"
        private void frmSetupIDQualifierAssociation_Load(object sender, EventArgs e)
        {
            try
            {
                DesignGrid();
                this.Width = 464;
                this.Height = 191;
                if (_QualifierAccociationID > 0)
                {
                    FillQualifierAssociation();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        #endregion "Form Load Event"

        #region "ToolStrip Events"
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        private void tsb_OK_Click(object sender, EventArgs e)
        {
            try
            {
                #region " Validate Data "

               
                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Qualifier Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return;
                }

                #endregion
                if (this._QualifierAccociationID == 0)
                {
                    String _strQuery = "Select count(*) from BL_IDQualifier_Association where sCode = '" + this.txtIDQualifierCode.Text.Replace("'","''") + "'  and sAdditionalDescription ='" + this.txtDescription.Text.Replace("'","''") + "'";
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    DataTable dtQualifierCode = new DataTable();
                    try
                    {
                        oDB.Connect(false);
                        object _intResult = null;
                        _intResult = oDB.ExecuteScalar_Query(_strQuery);
                        if (_intResult != null)
                        {
                            if (_intResult.ToString().Trim() != "")
                            {
                                if (Convert.ToInt64(_intResult) > 0)
                                {
                                    MessageBox.Show("Billing Id Qualifier already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                        oDB.Disconnect();
                    }
                    catch (gloDatabaseLayer.DBException dbex)
                    {
                        dbex.ERROR_Log(dbex.ToString());
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        dtQualifierCode.Dispose();

                    }
                }
                QualifierAssociation objQualifierAssociation = new QualifierAssociation(_databaseconnectionstring);
                objQualifierAssociation.QualifierCode = this.txtIDQualifierCode.Text;
                objQualifierAssociation.QualifierDescription = this.txtDescription.Text;
                objQualifierAssociation.ClinicID = this._ClinicID;
                objQualifierAssociation.QualifierMasterID = this._QualifierMstID;
                objQualifierAssociation.QualifierAssociationID = this._QualifierAccociationID;
                objQualifierAssociation.Add();
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        

        private void tsb_Save_Click(object sender, EventArgs e)
        {


            try
            {
                #region " Validate Data "


                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Qualifier Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return;
                }

                #endregion
                if (this._QualifierAccociationID == 0)
                {
                    String _strQuery = "Select count(*) from BL_IDQualifier_Association where sCode = '" + this.txtIDQualifierCode.Text.Replace("'", "''") + "'  and sAdditionalDescription ='" + this.txtDescription.Text.Replace("'", "''") + "'";
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    DataTable dtQualifierCode = new DataTable();
                    try
                    {
                        oDB.Connect(false);
                        object _intResult = null;
                        _intResult = oDB.ExecuteScalar_Query(_strQuery);
                        if (_intResult != null)
                        {
                            if (_intResult.ToString().Trim() != "")
                            {
                                if (Convert.ToInt64(_intResult) > 0)
                                {
                                    MessageBox.Show("Billing Id Qualifier already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                        oDB.Disconnect();
                    }
                    catch (gloDatabaseLayer.DBException dbex)
                    {
                        dbex.ERROR_Log(dbex.ToString());
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        dtQualifierCode.Dispose();

                    }
                }
                QualifierAssociation objQualifierAssociation = new QualifierAssociation(_databaseconnectionstring);
                objQualifierAssociation.QualifierCode = this.txtIDQualifierCode.Text;
                objQualifierAssociation.QualifierDescription = this.txtDescription.Text;
                objQualifierAssociation.ClinicID = this._ClinicID;
                objQualifierAssociation.QualifierMasterID = this._QualifierMstID;
                objQualifierAssociation.QualifierAssociationID = this._QualifierAccociationID;
                objQualifierAssociation.Add();
                this.txtDescription.Text = "";
                this.txtIDQualifierCode.Text = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion "ToolStrip Events"
        
        #region "C1 FlexGrid Events"
        private void c1IDQualifier_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (c1IDQualifier.RowSel > 0)
                {
                    txtIDQualifierCode.Text = Convert.ToString(c1IDQualifier.GetData(c1IDQualifier.RowSel, COL_CODE));
                    if (txtDescription.Text == "")
                    { txtDescription.Text = Convert.ToString(c1IDQualifier.GetData(c1IDQualifier.RowSel, COL_DESC)); }
                    _QualifierMstID = Convert.ToInt64(c1IDQualifier.GetData(c1IDQualifier.RowSel, COL_ID));
                }
                pnlInternalCodeControl.Visible = false;
                lblDescription.Select();
                lblDescription.Focus();
                this.Width = 464;
                this.Height = 191;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
        }

        private void c1IDQualifier_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter) && c1IDQualifier.RowSel > 0)
                {
                    _QualifierMstID = Convert.ToInt64(c1IDQualifier.GetData(c1IDQualifier.RowSel, COL_ID));
                    txtIDQualifierCode.Text = Convert.ToString(c1IDQualifier.GetData(c1IDQualifier.RowSel, COL_CODE));
                    pnlInternalCodeControl.Visible = false;
                    if (txtDescription.Text == "")
                    { txtDescription.Text = Convert.ToString(c1IDQualifier.GetData(c1IDQualifier.RowSel, COL_DESC)); }
                    lblDescription.Select();
                    lblDescription.Focus();
                    this.Width = 464;
                    this.Height = 191;
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                {
                    pnlInternalCodeControl.Visible = false;
                    lblDescription.Select();
                    lblDescription.Focus();
                    this.Width = 464;
                    this.Height = 191;
                    txtIDQualifierCode.Text = "";
                    txtDescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
        }

        private void c1IDQualifier_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (c1IDQualifier.RowSel > 0)
                {
                    txtIDQualifierCode.Text = Convert.ToString(c1IDQualifier.GetData(c1IDQualifier.RowSel, COL_CODE));
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
        }

        private void c1IDQualifier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (c1IDQualifier.RowSel > 0)
                {
                    txtIDQualifierCode.Text = Convert.ToString(c1IDQualifier.GetData(c1IDQualifier.RowSel, COL_CODE));
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
        }
        #endregion "C1 FlexGrid Events"

        #region "C1 FlexGrid Design"
        private void DesignGrid()
        {
            try
            {
                int _width = 0;
                #region " Qualifier Grid "
                c1IDQualifier.Rows.Count = 1;
                c1IDQualifier.Rows.Fixed = 1;
                c1IDQualifier.Cols.Count = COL_COUNT;
                c1IDQualifier.Cols.Fixed = 0;
                c1IDQualifier.SetData(0, COL_ID, "ID");
                c1IDQualifier.SetData(0, COL_CODE, "Code");
                c1IDQualifier.SetData(0, COL_DESC, "Description");

                c1IDQualifier.Cols[COL_ID].Visible = false;
                c1IDQualifier.Cols[COL_CODE].Visible = true;
                c1IDQualifier.Cols[COL_DESC].Visible = true;

                c1IDQualifier.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1IDQualifier.Cols[COL_DESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                _width = pnlInternalCodeControl.Width - 2;

                c1IDQualifier.Cols[COL_ID].Width = 0;
                c1IDQualifier.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.3);
                c1IDQualifier.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.65);



                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        #endregion "C1 FlexGrid Design"

        #region "Private Methods"
        private void FillQualifierAssociation()
        {

            QualifierAssociation qualifier = new QualifierAssociation(_databaseconnectionstring);
            DataTable dtQualifierCode = new DataTable();

            try
            {
                if (_QualifierAccociationID > 0)
                {
                    dtQualifierCode = qualifier.GetQualifierCode(_QualifierAccociationID);
                    if (dtQualifierCode != null && dtQualifierCode.Rows.Count > 0)
                    {
                        txtDescription.Text = dtQualifierCode.Rows[0]["sDescription"].ToString();
                        txtIDQualifierCode.Text = dtQualifierCode.Rows[0]["sCode"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                dtQualifierCode.Dispose();
            }
        }
        #endregion "Private Methods"

        #region "Form Button Click Events"
        private void btnSelectMod_Click(object sender, EventArgs e)
        {   
            this.Width = 466;
            this.Height = 310;
            pnlInternalCodeControl.Visible = true;
            DesignGrid();
            DataTable dtQualifier = new DataTable();
            String strQuery = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                if (txtIDQualifierCode.Text != "")
                { strQuery = "  Select nQualifierMstID,sCode,sDescription from BL_IDQualifier "; }
                else { strQuery = "  Select nQualifierMstID,sCode,sDescription from BL_IDQualifier "; }

                oDB.Connect(false);
                oDB.Retrive_Query(strQuery, out dtQualifier);
                if (dtQualifier.Rows.Count == 0)
                    pnlInternalCodeControl.Visible = false;
                if (dtQualifier != null)
                {
                    for (int i = 0; i < dtQualifier.Rows.Count; i++)
                    {
                        c1IDQualifier.Rows.Add();
                        int rowIndex = c1IDQualifier.Rows.Count - 1;
                        c1IDQualifier.SetData(rowIndex, COL_ID, dtQualifier.Rows[i]["nQualifierMstID"]);
                        c1IDQualifier.SetData(rowIndex, COL_CODE, dtQualifier.Rows[i]["sCode"]);
                        c1IDQualifier.SetData(rowIndex, COL_DESC, dtQualifier.Rows[i]["sDescription"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtQualifier != null)
                {
                    dtQualifier.Dispose();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pnlInternalCodeControl.Visible = false;                        
            lblDescription.Select();
            lblDescription.Focus();
            this.Width = 464;
            this.Height = 191;
            txtIDQualifierCode.Text = "";
            txtDescription.Text = "";
        }
        #endregion "Form Button Click Events"

        private void txtDescription_MouseHover(object sender, EventArgs e)
        {
            if(this.txtDescription.ToString().Length >45)
                C1SuperTooltip1.SetToolTip(this.txtDescription, Convert.ToString(txtDescription.Text));
        }

        private void c1IDQualifier_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

  

       

        
    }
}
