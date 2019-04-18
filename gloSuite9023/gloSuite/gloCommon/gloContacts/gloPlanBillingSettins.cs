using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using gloSettings;
using System.Collections;
using System.Windows.Forms;

namespace gloContacts
{
    partial class frmSetupInsurance
    {

        #region " Billing Settings - Alternate ID "

        private void SaveAlternateIDSettings()
        {
            GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strsql = "";

            try
            {               
                oDb.Connect(false);
                _strsql = "Delete from BL_AlternateID_Settings where nContactID = " + _ContactID + " and nClinicID = " + _ClinicID;

                oDb.Execute_Query(_strsql);

                if (cmbPaperRendering.SelectedIndex > 0)
                {
                    oSettings.SaveBillingQualifierIDSettings("Paper Rendering Provider ID Type", Convert.ToInt64(cmbPaperRendering.SelectedValue), cmbPaperRendering.Text, false, "", false, AlternateIDSettingLevel.InsurancePlan, _ContactID, 0, _ClinicID);
                }
                if (cmbElectronicRendering.SelectedIndex > 0)
                {
                    oSettings.SaveBillingQualifierIDSettings("Electronic Rendering Provider ID Type", Convert.ToInt64(cmbElectronicRendering.SelectedValue), cmbElectronicRendering.Text, false, "", false, AlternateIDSettingLevel.InsurancePlan, _ContactID, 0, _ClinicID);
                }
                                                
                if (cmbReferringProviderOtherIDType.SelectedIndex > 0)
                {
                    oSettings.SaveBillingQualifierIDSettings("Referring Provider Other ID Type", 0, "", true, cmbReferringProviderOtherIDType.SelectedValue.ToString(), false, AlternateIDSettingLevel.InsurancePlan, _ContactID, 0, _ClinicID, cmbReferringProviderOtherIDType.Text);
                  
                }

                //save UB04 Box5 
                if (cmbFedTaxNoBox5.SelectedIndex >= 0)
                {
                    oSettings.SaveBillingQualifierIDSettings("Box 5 - FED TAX NO", Convert.ToInt64(cmbFedTaxNoBox5.SelectedValue), cmbFedTaxNoBox5.Text, false, "", false, AlternateIDSettingLevel.InsurancePlan, _ContactID, 0, _ClinicID);

                }
                //END save UB04 Box5 cmbUBBlngprvdraltID

                //save UB04 Box51 
                if (cmbUBBlngprvdraltID.SelectedIndex > 0)
                {
                    oSettings.SaveBillingQualifierIDSettings("Box 51 - Health Plan ID", Convert.ToInt64(cmbUBBlngprvdraltID.SelectedValue), cmbUBBlngprvdraltID.Text, false, "", false, AlternateIDSettingLevel.InsurancePlan, _ContactID, 0, _ClinicID);

                }
                //END save UB04 Box51 


                if (cmbServiceFacilitySource.SelectedIndex > 0 || chkServiceFacOtherID.Checked)
                {
                    oSettings.SaveBillingQualifierIDSettings("Service Facility Source", Convert.ToInt64(cmbServiceFacilitySource.SelectedValue), cmbServiceFacilitySource.Text, chkServiceFacOtherID.Checked, (chkServiceFacOtherID.Checked ? Convert.ToString(cmbServiceFacilityOtherIDType.SelectedValue) : ""), (chkServiceFacOtherID.Checked ? chkServiceFacilitySwap.Checked : false), AlternateIDSettingLevel.InsurancePlan, _ContactID, 0, _ClinicID,cmbServiceFacilityOtherIDType.Text);
                }
                if (cmbBillingProviderSource.SelectedIndex > 0 || chkBillingProviderOtherID.Checked)
                {
                    oSettings.SaveBillingQualifierIDSettings("Billing Provider Source", Convert.ToInt64(cmbBillingProviderSource.SelectedValue), cmbBillingProviderSource.Text, chkBillingProviderOtherID.Checked, (chkBillingProviderOtherID.Checked ? Convert.ToString(cmbBillingProviderSourceOtherIDType.SelectedValue) : ""), (chkBillingProviderOtherID.Checked ? chkBillingProviderSwap.Checked : false), AlternateIDSettingLevel.InsurancePlan, _ContactID, 0, _ClinicID, cmbBillingProviderSourceOtherIDType.Text);
                }
                
                oSettings.SaveBillingQualifierIDSettings("IsOtherIDForEDIFacility", 0, "", chkOtherIDonEDI.Checked, "0", (chkBillingProviderOtherID.Checked ? chkBillingProviderSwap.Checked : false), AlternateIDSettingLevel.InsurancePlan, _ContactID, 0, _ClinicID);

               
                for (int iGridCount = 1; iGridCount <= c1ProviderSettings.Rows.Count - 1; iGridCount++)
                {
                    if (c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.ServiceFacilitySource) != null && Convert.ToString(c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.ServiceFacilitySource)).Trim() != "")
                    {
                        oSettings.SaveBillingQualifierIDSettings("Service Facility Source", Convert.ToInt64(htSourceSettings[c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.ServiceFacilitySource)]), Convert.ToString(c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.ServiceFacilitySource)), false, "", false, AlternateIDSettingLevel.InsurancePlanProvider, _ContactID, Convert.ToInt64(c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.ProviderID)), _ClinicID);
                    }

                    if (c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.BillingProviderSource) != null && Convert.ToString(c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.BillingProviderSource)).Trim() != "")
                    {
                        oSettings.SaveBillingQualifierIDSettings("Billing Provider Source", Convert.ToInt64(htSourceSettings[c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.BillingProviderSource)]), Convert.ToString(c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.BillingProviderSource)), false, "", false, AlternateIDSettingLevel.InsurancePlanProvider, _ContactID, Convert.ToInt64(c1ProviderSettings.GetData(iGridCount, (int)BillingGridColumn.ProviderID)), _ClinicID);
                    }
                }
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); }
                if (oDb != null) { oDb.Disconnect(); oDb.Dispose(); }
                _strsql = null;
            }
        }

        private void FillAlternateIDSettingsData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtAlternateIDSettings = null;
                      
            string _sqlQuery = string.Empty;
            int iRow = 0;
            try
            {                
                oDB.Connect(false);

                _sqlQuery = " select sSettingName,sValue,bIsOtherID,nOtherID,bIsSwapIDs,nLevel,nContactID,nProviderID, "
                            + "nClinicID,nQualifierID,nOtherIDProviderCompanyIndex,sOtherIDDesc from BL_AlternateID_Settings WHERE nContactID = " + _ContactID + " AND nLevel IN(" 
                            + (int)AlternateIDSettingLevel.InsurancePlan + "," + (int)AlternateIDSettingLevel.InsurancePlanProvider + ")";

                oDB.Retrive_Query(_sqlQuery, out dtAlternateIDSettings);

                if (dtAlternateIDSettings != null && dtAlternateIDSettings.Rows.Count > 0)
                {
                    DataRow[] oRow = dtAlternateIDSettings.Select("sSettingName = 'Paper Rendering Provider ID Type' AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlan);

                    if (oRow.Length > 0)
                    {
                        int _itemIndex = -1;
                        cmbPaperRendering.SelectedIndex = _itemIndex;
                        _itemIndex = cmbPaperRendering.FindStringExact(oRow[0]["svalue"].ToString());
                        cmbPaperRendering.SelectedIndex = _itemIndex;

                        if (_itemIndex == -1 || _itemIndex == 0)
                        {
                            cmbPaperRendering.SelectedValue = oRow[0]["nQualifierID"];
                        }
                        oRow = null;
                       
                    }

                   
                    oRow = dtAlternateIDSettings.Select("sSettingName = 'Electronic Rendering Provider ID Type'AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlan);
                    if (oRow.Length>0)
                    {  
                            int _itemIndex = -1;
                            cmbElectronicRendering.SelectedIndex = _itemIndex;
                            _itemIndex = cmbElectronicRendering.FindStringExact(oRow[0]["svalue"].ToString());
                            cmbElectronicRendering.SelectedIndex = _itemIndex;

                            if (_itemIndex == -1 || _itemIndex == 0)
                            {
                                cmbElectronicRendering.SelectedValue = oRow[0]["nQualifierID"];
                            }
                            oRow = null;
                       
                    }
                    oRow = dtAlternateIDSettings.Select("sSettingName = 'Referring Provider Other ID Type' AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlan);
                    if (oRow.Length > 0)
                    {
                        int _itemIndex = -1;
                        cmbReferringProviderOtherIDType.SelectedIndex = _itemIndex;
                        _itemIndex = cmbReferringProviderOtherIDType.FindStringExact(oRow[0]["sOtherIDDesc"].ToString());
                        cmbReferringProviderOtherIDType.SelectedIndex = _itemIndex;
                        if (_itemIndex == -1 || _itemIndex == 0)
                        {
                            cmbReferringProviderOtherIDType.SelectedValue = oRow[0]["nOtherID"];
                        }
                        oRow = null;                       
                    }
                    //Set Box 5
                    oRow = dtAlternateIDSettings.Select("sSettingName = 'Box 5 - FED TAX NO' AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlan);
                    if (oRow.Length > 0)
                    {
                        int _itemIndex = -1;
                        cmbFedTaxNoBox5.SelectedIndex = _itemIndex;
                        _itemIndex = cmbFedTaxNoBox5.FindStringExact(oRow[0]["svalue"].ToString());
                        cmbFedTaxNoBox5.SelectedIndex = _itemIndex;
                        if (_itemIndex == -1 || _itemIndex == 0)
                        {
                            cmbFedTaxNoBox5.SelectedValue = oRow[0]["nQualifierID"];
                        }
                        oRow = null;
                    }
                    else
                    {
                        cmbFedTaxNoBox5.SelectedIndex =1;
                    }
                    //End Set Box5

                    //Set Box 51
                    oRow = dtAlternateIDSettings.Select("sSettingName = 'Box 51 - Health Plan ID' AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlan);
                    if (oRow.Length > 0)
                    {
                        int _itemIndex = -1;
                        cmbUBBlngprvdraltID.SelectedIndex = _itemIndex;
                        _itemIndex = cmbUBBlngprvdraltID.FindStringExact(oRow[0]["svalue"].ToString());
                        cmbUBBlngprvdraltID.SelectedIndex = _itemIndex;
                        
                        if (_itemIndex == -1 || _itemIndex == 0)
                        {
                            cmbUBBlngprvdraltID.SelectedValue = oRow[0]["nQualifierID"];
                        }
                        oRow = null;                       
                       
                    }
                    //End Set Box51
                    oRow = dtAlternateIDSettings.Select("sSettingName = 'Service Facility Source' AND nProviderID = 0 AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlan);
                    if (oRow.Length > 0)
                    {
                        int _itemIndex = -1;
                        cmbServiceFacilitySource.SelectedIndex = _itemIndex;
                        _itemIndex = cmbServiceFacilitySource.FindStringExact(oRow[0]["svalue"].ToString());
                        cmbServiceFacilitySource.SelectedIndex = _itemIndex;
                        chkServiceFacOtherID.Checked = Convert.ToBoolean(oRow[0]["bIsOtherID"]);

                        if (Convert.ToBoolean(oRow[0]["bIsOtherID"]))
                        { pnlServiceFacilityOther.Enabled = true; }
                        else
                        { pnlServiceFacilityOther.Enabled = false; }

                       // cmbServiceFacilityOtherIDType.Text = oRow[0]["sOtherIDDesc"].ToString();
                        _itemIndex = -1;
                        cmbServiceFacilityOtherIDType.SelectedIndex = _itemIndex;
                        _itemIndex = cmbServiceFacilityOtherIDType.FindStringExact(oRow[0]["sOtherIDDesc"].ToString());
                        cmbServiceFacilityOtherIDType.SelectedIndex = _itemIndex;

                        if (_itemIndex == -1 || _itemIndex == 0)
                        {
                            cmbServiceFacilityOtherIDType.SelectedValue = oRow[0]["nOtherID"];
                        }


                        chkServiceFacilitySwap.Checked = Convert.ToBoolean(oRow[0]["bIsSwapIDs"]);
                        oRow = null;

                    }

                    oRow = dtAlternateIDSettings.Select("sSettingName = 'Billing Provider Source' AND nProviderID = 0 AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlan);

                    if (oRow.Length > 0)
                    {
                        int _itemIndex = -1;
                        cmbBillingProviderSource.SelectedIndex = _itemIndex;
                        _itemIndex = cmbBillingProviderSource.FindStringExact(oRow[0]["svalue"].ToString());
                        cmbBillingProviderSource.SelectedIndex = _itemIndex;
                       
                       
                          chkBillingProviderOtherID.Checked = Convert.ToBoolean(oRow[0]["bIsOtherID"]);

                        if (Convert.ToBoolean(oRow[0]["bIsOtherID"]))
                        { pnlBillingSourceOther.Enabled = true; }
                        else
                        { pnlBillingSourceOther.Enabled = false; }

                        //cmbBillingProviderSourceOtherIDType.Text = oRow[0]["sOtherIDDesc"].ToString();
                        _itemIndex = -1;
                        cmbBillingProviderSourceOtherIDType.SelectedIndex = _itemIndex;
                        _itemIndex = cmbBillingProviderSourceOtherIDType.FindStringExact(oRow[0]["sOtherIDDesc"].ToString());
                        cmbBillingProviderSourceOtherIDType.SelectedIndex = _itemIndex;

                        if (_itemIndex == -1 || _itemIndex == 0)
                        {
                            cmbBillingProviderSourceOtherIDType.SelectedValue = oRow[0]["nOtherID"];
                        }

                        chkBillingProviderSwap.Checked = Convert.ToBoolean(oRow[0]["bIsSwapIDs"]);
                        oRow = null;
                    }

                    oRow = dtAlternateIDSettings.Select("sSettingName = 'IsOtherIDForEDIFacility' AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlan);

                    if (oRow.Length > 0)
                    {
                        chkOtherIDonEDI.Checked = Convert.ToBoolean(oRow[0]["bIsOtherID"]);
                        oRow = null;
                    }

                    oRow = dtAlternateIDSettings.Select("nProviderID <> 0 AND nLevel = " + (int)AlternateIDSettingLevel.InsurancePlanProvider);

                    foreach (DataRow dr in oRow)
                    {
                        iRow = c1ProviderSettings.FindRow(Convert.ToString(dr["nProviderID"]), 0, (int)BillingGridColumn.ProviderID, true);

                        if (iRow > 0)
                        {
                            if (Convert.ToString(dr["sSettingName"]) == "Billing Provider Source")
                            {
                                c1ProviderSettings.SetData(iRow, (int)BillingGridColumn.BillingProviderSource, Convert.ToString(dr["sValue"]));
                            }
                            else if (Convert.ToString(dr["sSettingName"]) == "Service Facility Source")
                            {
                                c1ProviderSettings.SetData(iRow, (int)BillingGridColumn.ServiceFacilitySource, Convert.ToString(dr["sValue"]));
                            }
                        }

                    }
                    oRow = null;
                }
               

                if (pnlServiceFacilityOther.Enabled)
                { cmbServiceFacilityOtherIDType.DrawMode = DrawMode.OwnerDrawFixed; }
                else
                { cmbServiceFacilityOtherIDType.DrawMode = DrawMode.Normal; }

                if (pnlBillingSourceOther.Enabled)
                { cmbBillingProviderSourceOtherIDType.DrawMode = DrawMode.OwnerDrawFixed; }
                else
                { cmbBillingProviderSourceOtherIDType.DrawMode = DrawMode.Normal; }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                if (dtAlternateIDSettings != null)
                {
                    dtAlternateIDSettings.Dispose();
                }
                _sqlQuery = null;
            }


        }

        private void FillBillingTabFields()
        {
            GeneralSettings oSettings = null;
            DataTable dtQualifiersAssociation = null;
            DataTable dtQualifiersAssociationwithCompanyProvider = null;
            DataTable dtSources = null;
            
            try
            {
                Int16 nProviderCount = 1;
                Object _oProviderCount = null;
                oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                oSettings.GetSetting("NoOfProviderCompany", out _oProviderCount);
                if (_oProviderCount != null )
                {
                    nProviderCount = Convert.ToInt16(_oProviderCount);
                }
                dtQualifiersAssociation = oSettings.getIDQualifiersAssociation(false, true);
                if (nProviderCount > 1)
                {
                    dtQualifiersAssociationwithCompanyProvider = oSettings.getIDQualifiersAssociation(false, false); 
                }
                else
                {
                    dtQualifiersAssociationwithCompanyProvider = dtQualifiersAssociation.Copy();
                }

                if (dtQualifiersAssociation != null && dtQualifiersAssociation.Rows.Count > 0)
                {
                    cmbPaperRendering.DataSource = dtQualifiersAssociationwithCompanyProvider.Copy();
                    cmbPaperRendering.DisplayMember = "sAdditionalDescription";
                    cmbPaperRendering.ValueMember = "nQualifierID";
                    //cmbPaperRendering.SelectedIndex = -1;
                    cmbPaperRendering.Update();
                    cmbPaperRendering.Refresh();

                    cmbElectronicRendering.DataSource = dtQualifiersAssociationwithCompanyProvider.Copy();
                    cmbElectronicRendering.DisplayMember = "sAdditionalDescription";
                    cmbElectronicRendering.ValueMember = "nQualifierID";
                    //cmbElectronicRendering.SelectedIndex = -1;
                    cmbElectronicRendering.Update();
                    cmbElectronicRendering.Refresh();


                    cmbServiceFacilityOtherIDType.DataSource = dtQualifiersAssociationwithCompanyProvider.Copy();
                    cmbServiceFacilityOtherIDType.DisplayMember = "sAdditionalDescription";
                    cmbServiceFacilityOtherIDType.ValueMember = "nQualifierID";
                    //cmbServiceFacilityOtherIDType.SelectedIndex = -1;
                    cmbServiceFacilityOtherIDType.Update();
                    cmbServiceFacilityOtherIDType.Refresh();


                    cmbBillingProviderSourceOtherIDType.DataSource = dtQualifiersAssociationwithCompanyProvider.Copy();
                    cmbBillingProviderSourceOtherIDType.DisplayMember = "sAdditionalDescription";
                    cmbBillingProviderSourceOtherIDType.ValueMember = "nQualifierID";
                    //cmbBillingProviderSourceOtherIDType.SelectedIndex = -1;
                    cmbBillingProviderSourceOtherIDType.Update();
                    cmbBillingProviderSourceOtherIDType.Refresh();

                    cmbReferringProviderOtherIDType.DataSource = dtQualifiersAssociationwithCompanyProvider.Copy();
                    cmbReferringProviderOtherIDType.DisplayMember = "sAdditionalDescription";
                    cmbReferringProviderOtherIDType.ValueMember = "nQualifierID";
                    cmbReferringProviderOtherIDType.Update();
                    cmbReferringProviderOtherIDType.Refresh();

                }

                dtSources = oSettings.getSources(true);

                if (dtSources != null && dtSources.Rows.Count > 0)
                {
                    cmbServiceFacilitySource.DataSource = dtSources.Copy();
                    cmbServiceFacilitySource.DisplayMember = "sDescription";
                    cmbServiceFacilitySource.ValueMember = "nID";
                    //cmbServiceFacilitySource.SelectedIndex = -1;
                    cmbServiceFacilitySource.Update();
                    cmbServiceFacilitySource.Refresh();

                    cmbBillingProviderSource.DataSource = dtSources.Copy();
                    cmbBillingProviderSource.DisplayMember = "sDescription";
                    cmbBillingProviderSource.ValueMember = "nID";
                    //cmbBillingProviderSource.SelectedIndex = -1;
                    cmbBillingProviderSource.Update();
                    cmbBillingProviderSource.Refresh();

                    htSourceSettings = new Hashtable();

                    for (int iCount = 0; iCount <= dtSources.Rows.Count - 1; iCount++)
                    {
                        htSourceSettings.Add(Convert.ToString(dtSources.Rows[iCount]["sDescription"]), Convert.ToString(dtSources.Rows[iCount]["nID"]));

                        if (sSourceComboString != string.Empty)
                        {
                            sSourceComboString += "||" + Convert.ToString(dtSources.Rows[iCount]["sDescription"]);
                        }
                        else
                        {
                            sSourceComboString = " " + "||" + Convert.ToString(dtSources.Rows[iCount]["sDescription"]);
                        }
                    }

                }
                FillBillingGrid();
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); }
                if (dtQualifiersAssociation != null) { dtQualifiersAssociation.Dispose(); }
                if (dtSources != null) { dtSources.Dispose(); }
            }
            
        }

        private void FillBillingGrid()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //DataTable dtMidLevelSettings = new DataTable();
            DataTable dtProvider = null;
            //string sComboString = string.Empty;
            string _sqlQuery = string.Empty;

            try
            {
                c1ProviderSettings.ExtendLastCol = true;
                c1ProviderSettings.Cols[(int)BillingGridColumn.ProviderID].AllowEditing = false;
                c1ProviderSettings.Cols[(int)BillingGridColumn.ProviderName].AllowEditing = false;
                c1ProviderSettings.Cols[(int)BillingGridColumn.ServiceFacilitySource].Width = 210;
                c1ProviderSettings.Cols[(int)BillingGridColumn.BillingProviderSource].Width = 210;

                oDB.Connect(false);
                _sqlQuery = "SELECT PM.nProviderID, PM.sFirstName + ' '+ PM.sMiddleName + ' '+ PM.sLastName AS sName,PM.nProviderType, PTM.sProviderType FROM Provider_MST PM,ProviderType_MST PTM WHERE  PM.nProviderType = PTM.nProviderTypeID ORDER BY sName";
                oDB.Retrive_Query(_sqlQuery, out dtProvider);

                if (dtProvider != null && dtProvider.Rows.Count > 0)
                {
                    c1ProviderSettings.Rows.Count = 1;
                    foreach (DataRow dr in dtProvider.Rows)
                    {
                        c1ProviderSettings.Rows.Add();
                        c1ProviderSettings.SetData(c1ProviderSettings.Rows.Count - 1, (int)BillingGridColumn.ProviderID, Convert.ToString(dr["nProviderID"]));
                        c1ProviderSettings.SetData(c1ProviderSettings.Rows.Count - 1, (int)BillingGridColumn.ProviderName, Convert.ToString(dr["sName"]));

                    }

                    c1ProviderSettings.Cols[(int)BillingGridColumn.ServiceFacilitySource].ComboList = sSourceComboString;
                    c1ProviderSettings.Cols[(int)BillingGridColumn.BillingProviderSource].ComboList = sSourceComboString;
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtProvider != null) { dtProvider.Dispose(); dtProvider = null; }
                _sqlQuery = null;
            }


        }
        public Int64 AddUB04ExtendSettings()
        {
            Int64 _result = 0;
            object _intresult = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {

                oDBParameters.Add("@nContactID", _ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@bInlcudeEstAmtDueUB04", Convert.ToBoolean(chkIncludeEstimatedAmtDue.Checked), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@bIncludeAttendingTaxonomyonElectonic", Convert.ToBoolean(ChkIncludeAttendingPrvTaxonomy.Checked), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@nIncludeExtendedZip", Convert.ToInt16(cmbExtendedZipCode.SelectedIndex), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@sBox81CCa_Qual", txt81AQual.Text.ToString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sBox81CCb_Qual", txt81BQual.Text.ToString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sBox81CCc_Qual", txt81CQual.Text.ToString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sBox81CCd_Qual", txt81DQual.Text.ToString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@nBox81CCa", Convert.ToInt16(cmb81AValue.SelectedIndex), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nBox81CCb", Convert.ToInt16(cmb81BValue.SelectedIndex), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nBox81CCc", Convert.ToInt16(cmb81CValue.SelectedIndex), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nBox81CCd", Convert.ToInt16(cmb81DValue.SelectedIndex), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                int result = oDB.Execute("BL_INUP_UB04_ExtendedSettings", oDBParameters, out  _intresult);


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;

        }
        private void FillUB04ExtendedSettings()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPayerExtendedSettings = null;           
            string _sqlQuery = string.Empty;

            try
            {
                
                oDB.Connect(false);
                _sqlQuery = "  SELECT nID, nContactID, nIncludeExtendedZip, bInlcudeEstAmtDueUB04, bIncludeAttendingTaxonomyonElectonic, sBox81CCa_Qual, nBox81CCa, sBox81CCb_Qual, nBox81CCb, sBox81CCc_Qual, nBox81CCc, sBox81CCd_Qual, nBox81CCd" +
                            " FROM  dbo.BL_UB04_ExtendedSettings WITH(NOLOCK) WHERE dbo.BL_UB04_ExtendedSettings.nContactID=" + _ContactID;               
                oDB.Retrive_Query(_sqlQuery, out dtPayerExtendedSettings);

                if (dtPayerExtendedSettings != null && dtPayerExtendedSettings.Rows.Count > 0)
                {
                    chkIncludeEstimatedAmtDue.Checked = Convert.ToBoolean(dtPayerExtendedSettings.Rows[0]["bInlcudeEstAmtDueUB04"]);
                    ChkIncludeAttendingPrvTaxonomy.Checked = Convert.ToBoolean(dtPayerExtendedSettings.Rows[0]["bIncludeAttendingTaxonomyonElectonic"]);
                    cmbExtendedZipCode.SelectedIndex = Convert.ToInt16(dtPayerExtendedSettings.Rows[0]["nIncludeExtendedZip"]);
                    txt81AQual.Text = Convert.ToString(dtPayerExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                    txt81BQual.Text = Convert.ToString(dtPayerExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                    txt81CQual.Text = Convert.ToString(dtPayerExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                    txt81DQual.Text = Convert.ToString(dtPayerExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                    cmb81AValue.SelectedIndex = Convert.ToInt16(dtPayerExtendedSettings.Rows[0]["nBox81CCa"]);
                    cmb81BValue.SelectedIndex = Convert.ToInt16(dtPayerExtendedSettings.Rows[0]["nBox81CCb"]);
                    cmb81CValue.SelectedIndex = Convert.ToInt16(dtPayerExtendedSettings.Rows[0]["nBox81CCc"]);
                    cmb81DValue.SelectedIndex = Convert.ToInt16(dtPayerExtendedSettings.Rows[0]["nBox81CCd"]);
                }

              

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtPayerExtendedSettings != null) { dtPayerExtendedSettings.Dispose(); dtPayerExtendedSettings = null; }
                _sqlQuery = null;
            }
        }

        private void tbInsuranceSetup_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                //code start added by kanchan on 20130613 to avoid multiple messages
                mtxtPhone.removeLeaveEvent();
                txtFax.removeLeaveEvent();
                mskeligibiltyPhone.removeLeaveEvent();
                //code end added by kanchan on 20130613 to avoid multiple messages

                ts_btnNewAlternateID.Visible = false;
                ts_btnEditAlternateID.Visible = false;
                ts_btnDeleteAlternateID.Visible = false;
                if (e.TabPage == tbp_InsurancePlan)
                {
                    this.Height = 450;
                    if (_oPlanHold != null)
                    {
                        if (_oPlanHold.IsHold == true)
                        {
                            tls_Hold.Visible = false;
                            tls_Release.Visible = true;
                        }
                        else
                        {
                            tls_Hold.Visible = true;
                            tls_Release.Visible = false;
                        }
                    }

                }
                else if (e.TabPage == tbp_MidLevel || e.TabPage == tbp_BillingTaxon)
                {
                    //this.Height = 635;
                    this.Height = 750;
                    tls_Hold.Visible = false;
                    tls_Release.Visible = false;
                }
                else if (e.TabPage == tbp_BillingSettings)
                {
                    // this.Height = 635;
                    this.Height = 798;
                    tls_Hold.Visible = false;
                    tls_Release.Visible = false;
                    //pnlBillingSourceOther.Enabled = false;
                    //pnlServiceFacilityOther.Enabled = false;
                }
                else if (e.TabPage == tbp_Eligibility)
                {
                    this.Height = 400;
                    tls_Hold.Visible = false;
                    tls_Release.Visible = false;
                }
                else if (e.TabPage == tbp_5010Transition)
                {
                    this.Height = 385;
                    tls_Hold.Visible = false;
                    tls_Release.Visible = false;
                }
                else if (e.TabPage == tbp_Institutional)
                {
                    this.Height = 620;
                    tls_Hold.Visible = false;
                    tls_Release.Visible = false;
                }
                else if (e.TabPage == tbp_AlternatePayerID)
                {
                    this.Height = 450;
                    tls_Hold.Visible = false;
                    ts_btnNewAlternateID.Visible = true;
                    ts_btnEditAlternateID.Visible = true;
                    ts_btnDeleteAlternateID.Visible = true;
                }
                else if (e.TabPage == tbp_EPSDT)
                {
                    this.Height = 462;
                    tls_Hold.Visible = false;
                    tls_Release.Visible = false;
                }
                else if (e.TabPage == tpAnesthesia)
                {
                    this.Height = 300;
                    tls_Hold.Visible = false;
                    tls_Release.Visible = false;
                }
                //pnlBillingSourceOther.Enabled = false;
                //pnlServiceFacilityOther.Enabled = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw;
            }
            finally
            {
                //code start added by kanchan on 20130613 to avoid multiple messages
                mtxtPhone.addLeaveEvent();
                txtFax.addLeaveEvent();
                mskeligibiltyPhone.addLeaveEvent();
                //code end added by kanchan on 20130613 to avoid multiple messages
            }
        }

        private void chkServiceFacOtherID_CheckedChanged(object sender, EventArgs e)
        {
            if (chkServiceFacOtherID.Checked == true)
            { pnlServiceFacilityOther.Enabled = true; }
            else
            { pnlServiceFacilityOther.Enabled = false; }

            if (pnlServiceFacilityOther.Enabled)
            { cmbServiceFacilityOtherIDType.DrawMode = DrawMode.OwnerDrawFixed; }
            else
            { cmbServiceFacilityOtherIDType.DrawMode = DrawMode.Normal; }

        }

        private void chkBillingProviderOtherID_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBillingProviderOtherID.Checked == true)
            { pnlBillingSourceOther.Enabled = true; }
            else
            { pnlBillingSourceOther.Enabled = false; }

            if (pnlBillingSourceOther.Enabled)
            { cmbBillingProviderSourceOtherIDType.DrawMode = DrawMode.OwnerDrawFixed; }
            else
            { cmbBillingProviderSourceOtherIDType.DrawMode = DrawMode.Normal; }
        }

        private void lblServiceFacChkSwap_Click(object sender, EventArgs e)
        {
            if (chkServiceFacilitySwap.CheckState == CheckState.Checked)
            { chkServiceFacilitySwap.Checked = false; }
            else
            { chkServiceFacilitySwap.Checked = true; }
        }

        private void lblProviderSourceChkSwap_Click(object sender, EventArgs e)
        {
            if (chkBillingProviderSwap.CheckState == CheckState.Checked)
            { chkBillingProviderSwap.Checked = false; }
            else
            { chkBillingProviderSwap.Checked = true; }
        }

        //private void cmbPaperRendering_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbPaperRendering.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPaperRendering.Items[cmbPaperRendering.SelectedIndex])["sAdditionalDescription"]), cmbPaperRendering) >= cmbPaperRendering.DropDownWidth - 20)
        //        {
        //            this.toolTip1.SetToolTip(cmbPaperRendering, Convert.ToString(((DataRowView)cmbPaperRendering.Items[cmbPaperRendering.SelectedIndex])["sAdditionalDescription"]));
        //        }
        //        else
        //        {
        //            this.toolTip1.Hide(combo);
        //        }
        //    }
        //}

        //private void cmbElectronicRendering_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbElectronicRendering.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbElectronicRendering.Items[cmbElectronicRendering.SelectedIndex])["sAdditionalDescription"]), cmbElectronicRendering) >= cmbElectronicRendering.DropDownWidth - 20)
        //        {
        //            this.toolTip1.SetToolTip(cmbElectronicRendering, Convert.ToString(((DataRowView)cmbElectronicRendering.Items[cmbElectronicRendering.SelectedIndex])["sAdditionalDescription"]));
        //        }
        //        else
        //        {
        //            this.toolTip1.Hide(combo);
        //        }
        //    }
        //}

        //private void cmbServiceFacilitySource_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbServiceFacilitySource.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbServiceFacilitySource.Items[cmbServiceFacilitySource.SelectedIndex])["sDescription"]), cmbServiceFacilitySource) >= cmbServiceFacilitySource.DropDownWidth - 20)
        //        {
        //            this.toolTip1.SetToolTip(cmbServiceFacilitySource, Convert.ToString(((DataRowView)cmbServiceFacilitySource.Items[cmbServiceFacilitySource.SelectedIndex])["sDescription"]));
        //        }
        //        else
        //        {
        //            this.toolTip1.Hide(combo);
        //        }
        //    }
        //}

        //private void cmbServiceFacilityOtherIDType_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbServiceFacilityOtherIDType.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbServiceFacilityOtherIDType.Items[cmbServiceFacilityOtherIDType.SelectedIndex])["sAdditionalDescription"]), cmbServiceFacilityOtherIDType) >= cmbServiceFacilityOtherIDType.DropDownWidth - 20)
        //        {
        //            this.toolTip1.SetToolTip(cmbServiceFacilityOtherIDType, Convert.ToString(((DataRowView)cmbServiceFacilityOtherIDType.Items[cmbServiceFacilityOtherIDType.SelectedIndex])["sAdditionalDescription"]));
        //        }
        //        else
        //        {
        //            this.toolTip1.Hide(combo);
        //        }
        //    }
        //}

        //private void cmbBillingProviderSource_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbBillingProviderSource.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBillingProviderSource.Items[cmbBillingProviderSource.SelectedIndex])["sDescription"]), cmbBillingProviderSource) >= cmbBillingProviderSource.DropDownWidth - 20)
        //        {
        //            this.toolTip1.SetToolTip(cmbBillingProviderSource, Convert.ToString(((DataRowView)cmbBillingProviderSource.Items[cmbBillingProviderSource.SelectedIndex])["sDescription"]));
        //        }
        //        else
        //        {
        //            this.toolTip1.Hide(combo);
        //        }
        //    }
        //}

        //private void cmbBillingProviderSourceOtherIDType_MouseMove(object sender, MouseEventArgs e)
        //{
        //    combo = (ComboBox)sender;
        //    if (cmbBillingProviderSourceOtherIDType.SelectedItem != null)
        //    {
        //        if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBillingProviderSourceOtherIDType.Items[cmbBillingProviderSourceOtherIDType.SelectedIndex])["sAdditionalDescription"]), cmbBillingProviderSourceOtherIDType) >= cmbBillingProviderSourceOtherIDType.DropDownWidth - 20)
        //        {
        //            this.toolTip1.SetToolTip(cmbBillingProviderSourceOtherIDType, Convert.ToString(((DataRowView)cmbBillingProviderSourceOtherIDType.Items[cmbBillingProviderSourceOtherIDType.SelectedIndex])["sAdditionalDescription"]));
        //        }
        //        else
        //        {
        //            this.toolTip1.Hide(combo);
        //        }
        //    }
        //}

        ////Function For Calculating the Lenghth of the Items in the combo box
        //private int getWidthofListItems(string _text, ComboBox combo)
        //{

        //    Graphics g = this.CreateGraphics();
        //    SizeF s = g.MeasureString(_text, combo.Font);
        //    int width = Convert.ToInt32(s.Width);
        //    return width;
        //}

        private void cmbServiceFacilityOtherIDType_MouseEnter(object sender, EventArgs e)
        {
            if (cmbServiceFacilityOtherIDType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbServiceFacilityOtherIDType.Items[cmbServiceFacilityOtherIDType.SelectedIndex])["sAdditionalDescription"]), cmbServiceFacilityOtherIDType) >= cmbServiceFacilityOtherIDType.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbServiceFacilityOtherIDType, Convert.ToString(((DataRowView)cmbServiceFacilityOtherIDType.Items[cmbServiceFacilityOtherIDType.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbServiceFacilityOtherIDType, "");
                }
            }
        }
        
        private void cmbServiceFacilitySource_MouseEnter(object sender, EventArgs e)
        {
            if (cmbServiceFacilitySource.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbServiceFacilitySource.Items[cmbServiceFacilitySource.SelectedIndex])["sDescription"]), cmbServiceFacilitySource) >= cmbServiceFacilitySource.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbServiceFacilitySource, Convert.ToString(((DataRowView)cmbServiceFacilitySource.Items[cmbServiceFacilitySource.SelectedIndex])["sDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbServiceFacilitySource, "");
                }
            }
        }

        private void cmbElectronicRendering_MouseEnter(object sender, EventArgs e)
        {
            if (cmbElectronicRendering.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbElectronicRendering.Items[cmbElectronicRendering.SelectedIndex])["sAdditionalDescription"]), cmbElectronicRendering) >= cmbElectronicRendering.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbElectronicRendering, Convert.ToString(((DataRowView)cmbElectronicRendering.Items[cmbElectronicRendering.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbElectronicRendering, "");
                }
            }
        }

        private void cmbPaperRendering_MouseEnter(object sender, EventArgs e)
        {
            if (cmbPaperRendering.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPaperRendering.Items[cmbPaperRendering.SelectedIndex])["sAdditionalDescription"]), cmbPaperRendering) >= cmbPaperRendering.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbPaperRendering, Convert.ToString(((DataRowView)cmbPaperRendering.Items[cmbPaperRendering.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbPaperRendering, "");
                }
            }
        }

        private void cmbBillingProviderSourceOtherIDType_MouseEnter(object sender, EventArgs e)
        {
            if (cmbBillingProviderSourceOtherIDType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBillingProviderSourceOtherIDType.Items[cmbBillingProviderSourceOtherIDType.SelectedIndex])["sAdditionalDescription"]), cmbBillingProviderSourceOtherIDType) >= cmbBillingProviderSourceOtherIDType.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbBillingProviderSourceOtherIDType, Convert.ToString(((DataRowView)cmbBillingProviderSourceOtherIDType.Items[cmbBillingProviderSourceOtherIDType.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbBillingProviderSourceOtherIDType, "");
                }
            }
        }

        private void cmbBillingProviderSource_MouseEnter(object sender, EventArgs e)
        {
            if (cmbBillingProviderSource.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBillingProviderSource.Items[cmbBillingProviderSource.SelectedIndex])["sDescription"]), cmbBillingProviderSource) >= cmbBillingProviderSource.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbBillingProviderSource, Convert.ToString(((DataRowView)cmbBillingProviderSource.Items[cmbBillingProviderSource.SelectedIndex])["sDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbBillingProviderSource, "");
                }
            }
        }

        private void cmbReferringProviderOtherIDType_MouseEnter(object sender, EventArgs e)
        {
            if (cmbReferringProviderOtherIDType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbReferringProviderOtherIDType.Items[cmbReferringProviderOtherIDType.SelectedIndex])["sAdditionalDescription"]), cmbReferringProviderOtherIDType) >= cmbReferringProviderOtherIDType.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbReferringProviderOtherIDType, Convert.ToString(((DataRowView)cmbReferringProviderOtherIDType.Items[cmbReferringProviderOtherIDType.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbReferringProviderOtherIDType, "");
                }
            }
        }

        private void cmbInsClmStartFUAction_MouseEnter(object sender, EventArgs e)
        {
            if (cmbInsClmStartFUAction.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsClmStartFUAction.Items[cmbInsClmStartFUAction.SelectedIndex])["sFollowUpDesc"]), cmbInsClmStartFUAction) >= cmbInsClmStartFUAction.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbInsClmStartFUAction, Convert.ToString(((DataRowView)cmbInsClmStartFUAction.Items[cmbInsClmStartFUAction.SelectedIndex])["sFollowUpDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbInsClmStartFUAction, "");
                }
            }
        }

        private void cmbInsClmRebillFUAction_MouseEnter(object sender, EventArgs e)
        {
            if (cmbInsClmRebillFUAction.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsClmRebillFUAction.Items[cmbInsClmRebillFUAction.SelectedIndex])["sFollowUpDesc"]), cmbInsClmRebillFUAction) >= cmbInsClmRebillFUAction.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbInsClmRebillFUAction, Convert.ToString(((DataRowView)cmbInsClmRebillFUAction.Items[cmbInsClmRebillFUAction.SelectedIndex])["sFollowUpDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbInsClmRebillFUAction, "");
                }
            }
        }

        private void cmbInsClmStartFUAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInsClmStartFUAction.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsClmStartFUAction.Items[cmbInsClmStartFUAction.SelectedIndex])["sFollowUpDesc"]), cmbInsClmStartFUAction) >= cmbInsClmStartFUAction.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbInsClmStartFUAction, Convert.ToString(((DataRowView)cmbInsClmStartFUAction.Items[cmbInsClmStartFUAction.SelectedIndex])["sFollowUpDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbInsClmStartFUAction, "");
                }
            }
        }

        private void cmbInsClmRebillFUAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInsClmRebillFUAction.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsClmRebillFUAction.Items[cmbInsClmRebillFUAction.SelectedIndex])["sFollowUpDesc"]), cmbInsClmRebillFUAction) >= cmbInsClmRebillFUAction.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbInsClmRebillFUAction, Convert.ToString(((DataRowView)cmbInsClmRebillFUAction.Items[cmbInsClmRebillFUAction.SelectedIndex])["sFollowUpDesc"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbInsClmRebillFUAction, "");
                }
            }
        }

        private void cmbElectronicRendering_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbElectronicRendering.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbElectronicRendering.Items[cmbElectronicRendering.SelectedIndex])["sAdditionalDescription"]), cmbElectronicRendering) >= cmbElectronicRendering.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbElectronicRendering, Convert.ToString(((DataRowView)cmbElectronicRendering.Items[cmbElectronicRendering.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbElectronicRendering, "");
                }
            }
        }

        private void cmbPaperRendering_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaperRendering.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPaperRendering.Items[cmbPaperRendering.SelectedIndex])["sAdditionalDescription"]), cmbPaperRendering) >= cmbPaperRendering.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbPaperRendering, Convert.ToString(((DataRowView)cmbPaperRendering.Items[cmbPaperRendering.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbPaperRendering, "");
                }
            }
        }

        private void cmbServiceFacilitySource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServiceFacilitySource.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbServiceFacilitySource.Items[cmbServiceFacilitySource.SelectedIndex])["sDescription"]), cmbServiceFacilitySource) >= cmbServiceFacilitySource.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbServiceFacilitySource, Convert.ToString(((DataRowView)cmbServiceFacilitySource.Items[cmbServiceFacilitySource.SelectedIndex])["sDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbServiceFacilitySource, "");
                }
            }
        }

        private void cmbServiceFacilityOtherIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServiceFacilityOtherIDType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbServiceFacilityOtherIDType.Items[cmbServiceFacilityOtherIDType.SelectedIndex])["sAdditionalDescription"]), cmbServiceFacilityOtherIDType) >= cmbServiceFacilityOtherIDType.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbServiceFacilityOtherIDType, Convert.ToString(((DataRowView)cmbServiceFacilityOtherIDType.Items[cmbServiceFacilityOtherIDType.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbServiceFacilityOtherIDType, "");
                }
            }
        }

        private void cmbBillingProviderSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBillingProviderSource.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBillingProviderSource.Items[cmbBillingProviderSource.SelectedIndex])["sDescription"]), cmbBillingProviderSource) >= cmbBillingProviderSource.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbBillingProviderSource, Convert.ToString(((DataRowView)cmbBillingProviderSource.Items[cmbBillingProviderSource.SelectedIndex])["sDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbBillingProviderSource, "");
                }
            }
        }

        private void cmbBillingProviderSourceOtherIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBillingProviderSourceOtherIDType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBillingProviderSourceOtherIDType.Items[cmbBillingProviderSourceOtherIDType.SelectedIndex])["sAdditionalDescription"]), cmbBillingProviderSourceOtherIDType) >= cmbBillingProviderSourceOtherIDType.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbBillingProviderSourceOtherIDType, Convert.ToString(((DataRowView)cmbBillingProviderSourceOtherIDType.Items[cmbBillingProviderSourceOtherIDType.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbBillingProviderSourceOtherIDType, "");
                }
            }
        }


        private void cmbReferringProviderOtherIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReferringProviderOtherIDType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbReferringProviderOtherIDType.Items[cmbReferringProviderOtherIDType.SelectedIndex])["sAdditionalDescription"]), cmbReferringProviderOtherIDType) >= cmbReferringProviderOtherIDType.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbReferringProviderOtherIDType, Convert.ToString(((DataRowView)cmbReferringProviderOtherIDType.Items[cmbReferringProviderOtherIDType.SelectedIndex])["sAdditionalDescription"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbReferringProviderOtherIDType, "");
                }
            }
        }

        
        #endregion " Billing Settings - Alternate ID "

        #region " Billing Taxonomy "

        private void c1BillingTaxonomy_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {               
                OpenInternalControl(gloContactsGridListControlType.Taxonomy, "Taxonomy", false, e.Row, e.Col, "");

                string _SearchText = "";
                if (c1BillingTaxonomy != null && c1BillingTaxonomy.Rows.Count > 0)
                {
                    _SearchText = Convert.ToString(c1BillingTaxonomy.GetData(e.Row, e.Col));
                    if (_SearchText != "" && ogloGridListControl != null)
                    {
                        ogloGridListControl.FillControl(_SearchText);
                    }
                }
                _SearchText = null;
                ogloGridListControl.Focus();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl.SelectedItems.Count > 0)
                {
                    c1BillingTaxonomy.SetData(c1BillingTaxonomy.RowSel, c1BillingTaxonomy.ColSel, Convert.ToString(ogloGridListControl.SelectedItems[0].Code));
                    c1BillingTaxonomy.SetData(c1BillingTaxonomy.RowSel, c1BillingTaxonomy.ColSel - 1, Convert.ToString(ogloGridListControl.SelectedItems[0].Description));
                    c1BillingTaxonomy.Focus();
                    c1BillingTaxonomy.Select(c1BillingTaxonomy.RowSel, c1BillingTaxonomy.ColSel);
                    if (ogloGridListControl != null)
                    {
                        CloseInternalControl();
                    }
                }
                else
                {
                    c1BillingTaxonomy.SetData(c1BillingTaxonomy.RowSel, c1BillingTaxonomy.ColSel, "");
                    c1BillingTaxonomy.SetData(c1BillingTaxonomy.RowSel, c1BillingTaxonomy.ColSel - 1, "");

                    CloseInternalControl();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                c1BillingTaxonomy.Focus();
                c1BillingTaxonomy.Select(c1BillingTaxonomy.RowSel, c1BillingTaxonomy.ColSel);
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool OpenInternalControl(gloContactsGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }


                ogloGridListControl = new gloGridListControl(gloContactsGridListControlType.Taxonomy, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;

                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, gloContactsSearchColumn.Code);
                }
                ogloGridListControl.Show();

                int _x = c1BillingTaxonomy.Cols[ColIndex].Left;

                int _y = c1BillingTaxonomy.Rows[RowIndex].Bottom;

                pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                pnlInternalControl.Width = c1BillingTaxonomy.Cols[ColIndex].Width;
                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
                pnlInternalControl.Focus();
                ogloGridListControl.Select();
                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                RePositionInternalControl();
            }
            return _result;
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changd on 4/4/2014
                for (int i = pnlInternalControl.Controls.Count-1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null) 
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);

                    }
                    catch { }
                    ogloGridListControl.Dispose(); 
                    ogloGridListControl = null;
                }
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
          
            return _result;
        }

        private void RePositionInternalControl()
        {
            try
            {
                if (pnlInternalControl.Visible == true && ogloGridListControl != null)
                {
                    if (c1BillingTaxonomy.Rows[c1BillingTaxonomy.RowSel].Bottom + c1BillingTaxonomy.ScrollPosition.Y > 220)
                    {
                        pnlInternalControl.SetBounds((c1BillingTaxonomy.Cols[c1BillingTaxonomy.ColSel].Left + c1BillingTaxonomy.ScrollPosition.X), (c1BillingTaxonomy.Rows[c1BillingTaxonomy.RowSel].Bottom + c1BillingTaxonomy.ScrollPosition.Y - 230), 0, 0, BoundsSpecified.Location);
                    }
                    else
                    {
                        pnlInternalControl.SetBounds((c1BillingTaxonomy.Cols[c1BillingTaxonomy.ColSel].Left + c1BillingTaxonomy.ScrollPosition.X), (c1BillingTaxonomy.Rows[c1BillingTaxonomy.RowSel].Bottom + c1BillingTaxonomy.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);
                    }

                    //if ((this.Bottom - c1Transaction.Rows[CurrentTransactionLine].Bottom) - c1Transaction.ScrollPosition.Y > (c1Transaction.Rows[CurrentTransactionLine].Top - this.Top) + c1Transaction.ScrollPosition.Y)
                    //{
                    //    if ((this.Bottom - c1Transaction.Rows[CurrentTransactionLine].Bottom) - c1Transaction.ScrollPosition.Y < pnlInternalControl.Height) { pnlInternalControl.Height = (this.Bottom - c1Transaction.Rows[CurrentTransactionLine].Bottom) - c1Transaction.ScrollPosition.Y; }
                    //    //pnlInternalControl.Height = (this.Bottom - c1Transaction.Rows[CurrentTransactionLine].Bottom) - c1Transaction.ScrollPosition.Y;
                    //    pnlInternalControl.SetBounds((c1Transaction.Cols[CurrentColumn].Left + c1Transaction.ScrollPosition.X), (c1Transaction.Rows[CurrentTransactionLine].Bottom + c1Transaction.ScrollPosition.Y), 0, 0, BoundsSpecified.Location);

                    //}
                    //else
                    //{
                    //    if ((c1Transaction.Rows[CurrentTransactionLine].Top - this.Top) + c1Transaction.ScrollPosition.Y < pnlInternalControl.Height) { pnlInternalControl.Height = (c1Transaction.Rows[CurrentTransactionLine].Top - this.Top) + c1Transaction.ScrollPosition.Y; }
                    //    //pnlInternalControl.Height = (c1Transaction.Rows[CurrentTransactionLine].Top - this.Top) - c1Transaction.ScrollPosition.Y;
                    //    pnlInternalControl.SetBounds((c1Transaction.Cols[CurrentColumn].Left + c1Transaction.ScrollPosition.X), (c1Transaction.Rows[CurrentTransactionLine].Top - pnlInternalControl.Height) + c1Transaction.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                    //}

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillBillingTaxonomyGrid()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //DataTable dtMidLevelSettings = new DataTable();
            DataTable dtProvider = null;
            //string sComboString = string.Empty;
            string _sqlQuery = string.Empty;

            try
            {                

                oDB.Connect(false);
                //_sqlQuery = "SELECT PM.nProviderID, PM.sFirstName + ' '+ PM.sMiddleName + ' '+ PM.sLastName AS sName,PM.nProviderType,PM.sTaxonomy, PTM.sProviderType FROM Provider_MST PM,ProviderType_MST PTM WHERE  PM.nProviderType = PTM.nProviderTypeID ORDER BY sName";

                _sqlQuery = "SELECT PM.nProviderID, PM.sFirstName + ' '+ PM.sMiddleName + ' '+ PM.sLastName AS sName, PM.sTaxonomy,PM.sTaxonomyDesc FROM Provider_MST PM ORDER BY sName";

                oDB.Retrive_Query(_sqlQuery, out dtProvider);

                if (dtProvider != null && dtProvider.Rows.Count > 0)
                {
                    c1BillingTaxonomy.Rows.Count = 1;
                    foreach (DataRow dr in dtProvider.Rows)
                    {
                        c1BillingTaxonomy.Rows.Add();
                        c1BillingTaxonomy.SetData(c1BillingTaxonomy.Rows.Count - 1, (int)BillingTaxonomy.ProviderID, Convert.ToString(dr["nProviderID"]));
                        c1BillingTaxonomy.SetData(c1BillingTaxonomy.Rows.Count - 1, (int)BillingTaxonomy.ProviderName, Convert.ToString(dr["sName"]));
                        c1BillingTaxonomy.SetData(c1BillingTaxonomy.Rows.Count - 1, (int)BillingTaxonomy.DefaultTaxonomyDesc, Convert.ToString(dr["sTaxonomyDesc"]));
                        c1BillingTaxonomy.SetData(c1BillingTaxonomy.Rows.Count - 1, (int)BillingTaxonomy.DefaultTaxonomy, Convert.ToString(dr["sTaxonomy"]));


                        C1.Win.C1FlexGrid.CellStyle csNonEdit;// = c1BillingTaxonomy.Styles.Add("cs_NonEdit");
                        try
                        {
                            if (c1BillingTaxonomy.Styles.Contains("cs_NonEdit"))
                            {
                                csNonEdit = c1BillingTaxonomy.Styles["cs_NonEdit"];
                            }
                            else
                            {
                                csNonEdit = c1BillingTaxonomy.Styles.Add("cs_NonEdit");
 
                            }

                        }
                        catch
                        {
                            csNonEdit = c1BillingTaxonomy.Styles.Add("cs_NonEdit");
 

                        }
                        csNonEdit.BackColor = System.Drawing.Color.White;
                        c1BillingTaxonomy.SetCellStyle(c1BillingTaxonomy.Rows.Count - 1, (int)BillingTaxonomy.PlanOverride, csNonEdit);

                    }
                                      
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (dtProvider != null) { dtProvider.Dispose(); dtProvider = null; }
                _sqlQuery = null;
            }


        }

        private void c1BillingTaxonomy_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            try
            {
                _strSearchString = c1BillingTaxonomy.Editor.Text;
                ogloGridListControl.FillControl(_strSearchString);
                //ogloGridListControl.Focus();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            { _strSearchString = null; }
        }

        private void c1BillingTaxonomy_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                e.SuppressKeyPress = true;
                
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;


                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                            if (_IsItemSelected)
                            {

                                //If Item is Selected Move to nextcell
                                //MoveNext();

                                //********* Code Commented shifted to ogloGridListControl_ItemSelected
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
                {                   
                    CloseInternalControl();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    #region "Down Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion

                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (c1BillingTaxonomy.ColSel == (int)BillingTaxonomy.PlanOverride)
                    {
                        c1BillingTaxonomy.SetData(c1BillingTaxonomy.RowSel, (int)BillingTaxonomy.PlanOverride, "");
                        c1BillingTaxonomy.SetData(c1BillingTaxonomy.RowSel, (int)BillingTaxonomy.PlanOverrideDesc, "");
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BillingTaxonomy_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
                {
                    CloseInternalControl();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BillingTaxonomy_Click(object sender, EventArgs e)
        {
            CloseInternalControl();
        }

        private void c1BillingTaxonomy_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.Blue;
                //C1SuperTooltip1.RoundedCorners = true;
                C1SuperTooltip1.IsBalloon = false;

                if (c1BillingTaxonomy.HitTest(e.X, e.Y).Column == (int)BillingTaxonomy.PlanOverride || c1BillingTaxonomy.HitTest(e.X, e.Y).Column == (int)BillingTaxonomy.DefaultTaxonomy)
                {
                    gloC1FlexStyle.ShowToolTipForBillingServiceLine(C1SuperTooltip1, (C1.Win.C1FlexGrid.C1FlexGrid)sender, e.Location, true);
                }
                else
                {
                    C1SuperTooltip1.Hide();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
          
        private void c1BillingTaxonomy_KeyDownEdit(object sender, C1.Win.C1FlexGrid.KeyEditEventArgs e)
        {
            try
            {
                if (pnlInternalControl.Visible == true && e.KeyCode == Keys.Down)
                {
                    e.Handled = true;
                    ogloGridListControl.Select();
                    ogloGridListControl.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BillingTaxonomy_LeaveEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                c1BillingTaxonomy.ChangeEdit -= new System.EventHandler(this.c1BillingTaxonomy_ChangeEdit);
                c1BillingTaxonomy.Editor.Text = "";
                c1BillingTaxonomy.ChangeEdit += new System.EventHandler(this.c1BillingTaxonomy_ChangeEdit);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void c1BillingTaxonomy_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (c1BillingTaxonomy.GetData(c1BillingTaxonomy.RowSel, (int)BillingTaxonomy.PlanOverride) != null)
                {
                    if (Convert.ToString(c1BillingTaxonomy.GetData(c1BillingTaxonomy.RowSel, (int)BillingTaxonomy.PlanOverride)) == "")
                    {
                        c1BillingTaxonomy.SetData(c1BillingTaxonomy.RowSel, (int)BillingTaxonomy.PlanOverrideDesc, "");
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1BillingTaxonomy_AfterScroll(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                RePositionInternalControl();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

         #endregion " Billing Settings - Alternate ID "
    }
}
