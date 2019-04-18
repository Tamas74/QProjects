using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;

using pdftron;
using pdftron.PDF;
using pdftron.SDF;
using pdftron.Filters;
using pdftron.Common;

using System.IO;
using pdftron.PDF.Annots;
using System.Drawing;
using gloGlobal;

namespace gloBilling
{

    public enum WorkersCompFormTypes
    {
        C4 = 1,
        C42 = 2,
        C4Auth = 3,
        MG2 = 4,
        MG21 = 5
    }


    public class clsWorkerCompData
    {
        gloDatabaseLayer.DBLayer oDB = null;
        C4 obj_C4 = null;
        C4DoctorInfo objProviderInfo = null;

        //--PDF Tron Ini/Terminate
        public bool gIsPDFTronConnected = false;
        public bool gIsPDFTronExpired = false;

        gloEDocumentV3.eDocManager.eDocManager objSign=null;
        private static System.Drawing.Printing.PrinterSettings myPrinterSetting = new System.Drawing.Printing.PrinterSettings();

        // pdftron.PDF.PDFDraw pdfdraw = null;

        public static void Print(PDFDoc pdfPrintDoc, string sDBConnectionString, Control Parent)
        {
            PDFDoc doc = null;
            try
            {
                using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
                {
                    oDialog.ConnectionString = sDBConnectionString;
                    oDialog.TopMost = true;
                    oDialog.AllowSomePages = true;
                    oDialog.ModuleName = "PrintWorkersCompForms";

                    oDialog.RegistryModuleName = "NYWCForms";


                    //System.Drawing.Printing.StandardPrintController oPrintController = new System.Drawing.Printing.StandardPrintController();
                    //printDocument1.PrintController = oPrintController;

                    ////if (gloEDocV3Admin.blnUseDefaultPrinterDialog == false)
                    ////{

                    //PrintDialog1 = new PrintDialog();
                    if (oDialog != null)
                    {
                        //PrintDialog1.AllowCurrentPage = true;
                        //PrintDialog1.AllowSelection = true;
                        doc = pdfPrintDoc; //_pdfview.GetDoc();
                        doc.Lock();
                        int maxPage = doc.GetPageCount();

                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            oDialog.PrinterSettings = myPrinterSetting;//printDocument1.PrinterSettings;
                            oDialog.PrinterSettings.Copies = 1;
                            oDialog.ShowPrinterProfileDialog = true;

                            oDialog.AllowSomePages = true;
                            oDialog.PrinterSettings.ToPage = maxPage;
                            oDialog.PrinterSettings.FromPage = 1;
                            oDialog.PrinterSettings.MaximumPage = maxPage;
                            oDialog.PrinterSettings.MinimumPage = 1;
                        }
                        //PrintDialog1.AllowSomePages = true;
                        //PrintDialog1.PrinterSettings.ToPage = maxPage;
                        //PrintDialog1.PrinterSettings.FromPage = 1;
                        //PrintDialog1.PrinterSettings.MaximumPage = maxPage;
                        //PrintDialog1.PrinterSettings.MinimumPage = 1;

                        if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //if (PrintDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            //{
                            //  myPrinterSetting = PrintDialog1.PrinterSettings;
                            ////try
                            ////{
                            //printDocument1.PrinterSettings = oDialog.PrinterSettings;
                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {
                                myPrinterSetting = oDialog.PrinterSettings;
                            }
                            ////}
                            ////catch
                            ////{
                            ////    printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
                            ////}

                            //Lines Added By Dipak 20090909 
                            //oDialogResultIsOK = true;
                            //PopulatePrinterPageArray(maxPage, printDocument1.PrinterSettings);
                            //pdfdraw = new pdftron.PDF.PDFDraw();
                            //if (rect != null)
                            //{
                            //    rect.Dispose();
                            //    rect = null;
                            //}
                            ////printDocument1.DocumentName = strFileName;
                            //printDocument1.Print();
                            //if (pdfdraw != null)
                            //{
                            //    pdfdraw.Dispose();
                            //    pdfdraw = null;
                            //}
                            //if (rect != null)
                            //{
                            //    rect.Dispose();
                            //    rect = null;
                            //}

                            gloPrintDialog.gloPrintProgressController ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController
                                (doc, doc.GetFileName(), oDialog.PrinterSettings,
                                oDialog.CustomPrinterExtendedSettings);
                            //ogloPrintProgressController.AutoLandscape = false;
                            ogloPrintProgressController.ShowProgress(Parent);
                            //if (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint)
                            //{
                            //    if (oDialog.CustomPrinterExtendedSettings.IsShowProgress)
                            //    {
                            //        ogloPrintProgressController.Show();
                            //    }
                            //    else
                            //    {
                            //        ogloPrintProgressController.Show();
                            //    }
                            //}
                            //else
                            //{
                            //    ogloPrintProgressController.TopMost = true;
                            //    ogloPrintProgressController.ShowInTaskbar = false;

                            //    ogloPrintProgressController.ShowDialog(this);
                            //    if (ogloPrintProgressController != null)
                            //    {
                            //        ogloPrintProgressController.Dispose();
                            //    }
                            //    ogloPrintProgressController = null;
                            //}


                        }//if
                        doc.Unlock();
                        //   PrintDialog1.Dispose();
                        //   PrintDialog1 = null;
                    }
                    else
                    {
                        string _ErrorMessage = "Error in Showing Print Dialog";

                        if (_ErrorMessage.Trim() != "")
                        {
                            string _MessageString = "Date Time : " + DateTime.Now.Ticks.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                        }


                        MessageBox.Show(_ErrorMessage, gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ////}
                    ////else
                    ////{
                    ////    // printDocument1.DefaultPageSettings.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName;
                    ////    if (myPrinterSetting == null)
                    ////    {
                    ////        myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
                    ////    }

                    ////    if (myPrinterSetting != null)
                    ////    {
                    ////        try
                    ////        {
                    ////            printDocument1.PrinterSettings = myPrinterSetting;
                    ////        }
                    ////        catch
                    ////        {
                    ////            printDocument1.PrinterSettings.PrinterName = myPrinterSetting.PrinterName;
                    ////        }
                    ////    }


                    ////oDialogResultIsOK = true;
                }
                //   pdfdraw = new pdftron.PDF.PDFDraw();
                //if (rect != null)
                //{
                //    rect.Dispose();
                //    rect = null;
                //}
                ////  printDocument1.DocumentName = strFileName;
                //printDocument1.Print();
                //   myPrinterSetting = null;
                //if (pdfdraw != null)
                //{
                //    pdfdraw.Dispose();
                //    pdfdraw = null;
                //}
                //if (rect != null)
                //{
                //    rect.Dispose();
                //    rect = null;
                //}


            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                string _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.Ticks.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show(ex.Message, gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {
                //if (pdfdraw != null)
                //{
                //    pdfdraw.Dispose();
                //    pdfdraw = null;
                //}
                //if (rect != null)
                //{
                //    rect.Dispose();
                //    rect = null;
                //}
                //if (PrintDialog1 != null)
                //{
                //   PrintDialog1.Dispose();
                //   PrintDialog1 = null;
                //}
                //PageArray.Clear();
            }

        }

        public string _AppTempFolderPathNYWC
        {
            get
            {
                string directory = gloSettings.FolderSettings.AppTempFolderPath + "NYWC\\";
                    try
                    {
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);

                        }
                        return directory;
                    }
                    catch
                    {
                        return directory;
                    }

            }
        }

        public int nServiceLinesCnt { get; set; }

        public clsWorkerCompData(string _databaseconnectionstring)
        {
            oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            obj_C4 = new C4();
            objProviderInfo = new C4DoctorInfo();
            objSign = new gloEDocumentV3.eDocManager.eDocManager();
        }

        public void Dispose()
        {
            if (oDB != null)
            {
                oDB.Dispose();
                oDB = null;
            }
            obj_C4 = null;
            objProviderInfo = null;
        }

        public void CheckLockStatusForWorkerCompForm(Int64 FormID, bool flgLock, string Username, string MachineName, ref object retUserName, ref object retMachineName)
        {
            gloDatabaseLayer.DBParameters objDbParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                objDbParameters.Add("@nFormID", FormID, ParameterDirection.Input, SqlDbType.BigInt);
                objDbParameters.Add("@sLockFlag", flgLock, ParameterDirection.Input, SqlDbType.Bit);
                objDbParameters.Add("@sUserName", Username, ParameterDirection.InputOutput, SqlDbType.VarChar, 50);
                objDbParameters.Add("@sMachineName", MachineName, ParameterDirection.InputOutput, SqlDbType.VarChar, 50);

                oDB.Execute("gsp_LockUnlock_WorkersCompForm", objDbParameters, out retUserName, out retMachineName);


                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (objDbParameters != null)
                {
                    objDbParameters.Dispose();
                    objDbParameters = null;
                }
            }

        }

        public DataSet GetWorkersCompPredefinedInfo(Int64 Patientid, Int64 TransactionID, Int64 TransactionMasterID, Int64 VisitId, Int64 ClinicId)
        {
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet ds = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for dashboard.
                oParameters.Add("@PatientID", Patientid, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@VisitID", VisitId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@ClinicID", ClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                
                oDB.Retrive("gsp_GetDataForWorkersCompForms", oParameters, out ds);

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), "ERROR !!! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                //if (oDB != null) { oDB.Dispose(); }
            }

            return ds;
        }

        public void SetC4PrdefinedInfo(DataSet ds, ref C4 objC4, bool blnSetBillingInfo, bool blnSetPatientDemographics)
        {
            DataTable dtPatientDemographics = null;
            DataTable dtEmployer = null;
            DataTable dtProvider = null;
            DataTable dtClinicInfo = null;
            DataTable dtInsurance = null;
            DataTable dtICD9 = null;
            DataTable dtBillingTable = null;
            DataTable dtBillingIDs = null;
            string sPatientFullName = null;
            ArrayList arrlstDOS = null;

            try
            {
                if (blnSetPatientDemographics)
                {
                    dtPatientDemographics = ds.Tables[0];

                    if (dtPatientDemographics != null && dtPatientDemographics.Rows.Count > 0)
                    {
                        //return object;

                        sPatientFullName = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientLastName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientFirstName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientMiddleName"]);

                        objC4.C4HeaderInformation.PatientNamePage2 = sPatientFullName;
                        objC4.C4HeaderInformation.PatientNamePage3 = sPatientFullName;
                        objC4.C4HeaderInformation.PatientNamePage4 = sPatientFullName;

                        if ((string.Compare(System.Convert.ToString(dtPatientDemographics.Rows[0]["sGender"]), "Male", true)) == 0)
                        {
                            objC4.C4PatientInformation.IsFemale = false;
                            objC4.C4PatientInformation.IsMale = true;
                        }
                        else if ((string.Compare(System.Convert.ToString(dtPatientDemographics.Rows[0]["sGender"]), "Female", true)) == 0)
                        {
                            objC4.C4PatientInformation.IsFemale = true;
                            objC4.C4PatientInformation.IsMale = false;
                        }

                        objC4.C4PatientInformation.PatientFullName = sPatientFullName;
                        objC4.C4PatientInformation.PatientSSN = System.Convert.ToString(dtPatientDemographics.Rows[0]["nSSN_Formatted"]);
                        objC4.C4PatientInformation.PatientPhoneCode = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPhone_Part1"]);
                        objC4.C4PatientInformation.PatientPhone = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPhone_Part2"]);
                        objC4.C4PatientInformation.PatientStreetAddress = System.Convert.ToString(dtPatientDemographics.Rows[0]["sAddressLine1"]) + "  " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sAddressLine2"]);
                        objC4.C4PatientInformation.PatientCity = System.Convert.ToString(dtPatientDemographics.Rows[0]["sCity"]);
                        objC4.C4PatientInformation.PatientState = System.Convert.ToString(dtPatientDemographics.Rows[0]["sState"]);
                        objC4.C4PatientInformation.PatientZip = System.Convert.ToString(dtPatientDemographics.Rows[0]["sZIP"]);
                        objC4.C4PatientInformation.DateOfBirthDD = System.Convert.ToString(dtPatientDemographics.Rows[0]["dtDOB_Day"]);
                        objC4.C4PatientInformation.DateOfBirthMM = System.Convert.ToString(dtPatientDemographics.Rows[0]["dtDOB_Month"]);
                        objC4.C4PatientInformation.DateOfBirthYY = System.Convert.ToString(dtPatientDemographics.Rows[0]["dtDOB_YEAR"]);
                        objC4.C4PatientInformation.JobOnInjuryDate = System.Convert.ToString(dtPatientDemographics.Rows[0]["sOccupation"]);
                        objC4.C4PatientInformation.PatientAccount = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientAccountNo"]);
                    }

                    dtEmployer = ds.Tables[1];

                    if (dtEmployer != null && dtEmployer.Rows.Count > 0)
                    {
                        //return object;

                        objC4.C4EmployerInformation.EmployerWhenInjuryOccurred = System.Convert.ToString(dtEmployer.Rows[0]["sEmployerName"]);
                        objC4.C4EmployerInformation.EmployeePhoneCode = System.Convert.ToString(dtEmployer.Rows[0]["sWorkPhone_Part1"]);
                        objC4.C4EmployerInformation.EmployeePhone = System.Convert.ToString(dtEmployer.Rows[0]["sWorkPhone_Part2"]);
                        objC4.C4EmployerInformation.EmployeeAddress = System.Convert.ToString(dtEmployer.Rows[0]["sWorkAddressLine1"]) + ' ' + System.Convert.ToString(dtEmployer.Rows[0]["sWorkAddressLine2"]);
                        objC4.C4EmployerInformation.EmployeeCity = System.Convert.ToString(dtEmployer.Rows[0]["sWorkCity"]);
                        objC4.C4EmployerInformation.EmployeeState = System.Convert.ToString(dtEmployer.Rows[0]["sWorkState"]);
                        objC4.C4EmployerInformation.EmployeeZip = System.Convert.ToString(dtEmployer.Rows[0]["sWorkZIP"]);
                    }
                   
                }

                if (blnSetBillingInfo)
                {
                   
                    
                    objC4.C4DoctorInformation = new C4DoctorInfo();

                    dtProvider = ds.Tables[2];

                    if (dtProvider != null && dtProvider.Rows.Count > 0)
                    {
                       

                        objC4.C4DoctorInformation.ProviderID = System.Convert.ToInt64(dtProvider.Rows[0]["nProviderID"]);
                        objC4.C4DoctorInformation.ProviderSignImage = (byte[])(dtProvider.Rows[0]["imgSignature"]);

                        objC4.C4DoctorInformation.ProviderFullName = System.Convert.ToString(dtProvider.Rows[0]["sProviderName"]);
                        
                        //objC4.C4DoctorInformation.WCBRatingCode = "";

                        objC4.C4DoctorInformation.BillingGrp = System.Convert.ToString(dtProvider.Rows[0]["BillingGrp"]);

                        objC4.C4DoctorInformation.BillingAddressNumberStreet = System.Convert.ToString(dtProvider.Rows[0]["sAddress"]) + "  " + System.Convert.ToString(dtProvider.Rows[0]["sStreet"]);
                        objC4.C4DoctorInformation.BillingCity = System.Convert.ToString(dtProvider.Rows[0]["sCity"]);
                        objC4.C4DoctorInformation.BillingState = System.Convert.ToString(dtProvider.Rows[0]["sState"]);
                        objC4.C4DoctorInformation.BillingZip = System.Convert.ToString(dtProvider.Rows[0]["sZIP"]);
                        objC4.C4DoctorInformation.BillingPhoneCode = System.Convert.ToString(dtProvider.Rows[0]["sPhoneNo_Part1"]);
                        objC4.C4DoctorInformation.BillingPhone = System.Convert.ToString(dtProvider.Rows[0]["sPhoneNo_Part2"]);

                        objC4.C4DoctorInformation.ProviderNPI = System.Convert.ToString(dtProvider.Rows[0]["sNPI"]);


                        if (System.Convert.ToString(dtProvider.Rows[0]["ProviderEmpID"]).Trim() != "")
                        {
                            objC4.C4DoctorInformation.FederalTaxID = System.Convert.ToString(dtProvider.Rows[0]["ProviderEmpID"]).Trim();
                            objC4.C4DoctorInformation.blnIsEIN = true;
                            objC4.C4DoctorInformation.blnIsSSN = false;
                        }
                        else if (System.Convert.ToString(dtProvider.Rows[0]["ProviderEmpSSN"]).Trim() != "")
                        {
                            objC4.C4DoctorInformation.FederalTaxID = System.Convert.ToString(dtProvider.Rows[0]["ProviderEmpSSN"]).Trim();
                            objC4.C4DoctorInformation.blnIsEIN = false;
                            objC4.C4DoctorInformation.blnIsSSN = true;
                        }
                        else
                        {
                            objC4.C4DoctorInformation.FederalTaxID = "";
                            objC4.C4DoctorInformation.blnIsEIN = false;
                            objC4.C4DoctorInformation.blnIsSSN = false;
                        }


                        //objC4.C4Footer.Providersame = System.Convert.ToString(dtProvider.Rows[0]["sProviderName"]);
                        //objC4.C4Footer.ProviderSpeciality = System.Convert.ToString(dtProvider.Rows[0]["ProviderSpecialty"]);
                        //objC4.C4Footer.AuthorizedProviderName = System.Convert.ToString(dtProvider.Rows[0]["sProviderName"]);
                        //objC4.C4Footer.AuthorizedProviderSpeciality = System.Convert.ToString(dtProvider.Rows[0]["ProviderSpecialty"]);


                        dtClinicInfo = ds.Tables[6];

                        if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                        {
                            objC4.C4DoctorInformation.OfficeAddressNumberStreet = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicAddress"]);
                            objC4.C4DoctorInformation.OfficeCity = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicCity"]);
                            objC4.C4DoctorInformation.OfficeState = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicState"]);
                            objC4.C4DoctorInformation.OfficeZip = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicZip"]);

                            objC4.C4DoctorInformation.OfficePhoneCode = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicPhone_Part1"]);
                            objC4.C4DoctorInformation.OfficePhone = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicPhone_Part2"]);
                        }
                    }

                    objC4.C4BillingInformation = new C4BillingInfo();

                    dtInsurance = ds.Tables[3];

                    if (dtInsurance != null && dtInsurance.Rows.Count > 0)
                    {
                        objC4.C4BillingInformation.InsuranceCarrier = System.Convert.ToString(dtInsurance.Rows[0]["InsuranceCarrierName"]);
                        objC4.C4BillingInformation.CarrierCode = System.Convert.ToString(dtInsurance.Rows[0]["CarrierCode"]);
                        objC4.C4BillingInformation.CarrierAddressNumberStreet = System.Convert.ToString(dtInsurance.Rows[0]["PayerFullAddress"]);
                        objC4.C4BillingInformation.CarrierCity = System.Convert.ToString(dtInsurance.Rows[0]["PayerCity"]);
                        objC4.C4BillingInformation.CarrierState = System.Convert.ToString(dtInsurance.Rows[0]["PayerState"]);
                        objC4.C4BillingInformation.CarrierZip = System.Convert.ToString(dtInsurance.Rows[0]["PayerZip"]);
                    }

                    dtICD9 = ds.Tables[4];

                    if (dtICD9 != null && dtICD9.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtICD9.Rows.Count - 1; i++)
                        {
                            if (i == 0)
                            {
                                objC4.C4BillingInformation.ICD9Code1 = System.Convert.ToString(dtICD9.Rows[i]["sDxCode"]);
                                objC4.C4BillingInformation.ICD9Description1 = System.Convert.ToString(dtICD9.Rows[i]["sDxDescription"]);
                            }
                            else if (i == 1)
                            {
                                objC4.C4BillingInformation.ICD9Code2 = System.Convert.ToString(dtICD9.Rows[i]["sDxCode"]);
                                objC4.C4BillingInformation.ICD9Description2 = System.Convert.ToString(dtICD9.Rows[i]["sDxDescription"]);
                            }
                            else if (i == 2)
                            {
                                objC4.C4BillingInformation.ICD9Code3 = System.Convert.ToString(dtICD9.Rows[i]["sDxCode"]);
                                objC4.C4BillingInformation.ICD9Description3 = System.Convert.ToString(dtICD9.Rows[i]["sDxDescription"]);
                            }
                            else if (i == 3)
                            {
                                objC4.C4BillingInformation.ICD9Code4 = System.Convert.ToString(dtICD9.Rows[i]["sDxCode"]);
                                objC4.C4BillingInformation.ICD9Description4 = System.Convert.ToString(dtICD9.Rows[i]["sDxDescription"]);
                            }
                        }
                    }


                    dtBillingTable = ds.Tables[5];

                    if (dtBillingTable != null && dtBillingTable.Rows.Count > 0)
                    {
                        //return object;            
                        nServiceLinesCnt = dtBillingTable.Rows.Count;

                        arrlstDOS = new ArrayList();

                        for (int i = 0; i <= dtBillingTable.Rows.Count - 1; i++)
                        {

                            if (i == 0)
                            {
                                objC4.C4BillingInformation.DateOfServiceFromDD1 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceFromMM1 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceFromYY1 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (! arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"])) 
                                { 
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC4.C4BillingInformation.DateOfServiceToDD1 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceToMM1 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceToYY1 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC4.C4BillingInformation.PlaceOfService1 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC4.C4BillingInformation.CPT_HCPCS1 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }
                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }


                                objC4.C4BillingInformation.DiagnosisCode1 = sDxCodes;
                                objC4.C4BillingInformation.ChargesAmt1 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC4.C4BillingInformation.Days_Units1 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC4.C4BillingInformation.ModifierA1 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC4.C4BillingInformation.ModifierB1 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod2Code"]);
                                objC4.C4BillingInformation.ZipCode1 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);
                            }
                            if (i == 1)
                            {
                                objC4.C4BillingInformation.DateOfServiceFromDD2 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceFromMM2 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceFromYY2 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC4.C4BillingInformation.DateOfServiceToDD2 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceToMM2 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceToYY2 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC4.C4BillingInformation.PlaceOfService2 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC4.C4BillingInformation.CPT_HCPCS2 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC4.C4BillingInformation.DiagnosisCode2 = sDxCodes;
                                objC4.C4BillingInformation.ChargesAmt2 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC4.C4BillingInformation.Days_Units2 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC4.C4BillingInformation.ModifierA2 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC4.C4BillingInformation.ModifierB2 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod2Code"]);
                                objC4.C4BillingInformation.ZipCode2 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);
                            }
                            if (i == 2)
                            {
                                objC4.C4BillingInformation.DateOfServiceFromDD3 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceFromMM3 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceFromYY3 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC4.C4BillingInformation.DateOfServiceToDD3 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceToMM3 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceToYY3 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC4.C4BillingInformation.PlaceOfService3 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC4.C4BillingInformation.CPT_HCPCS3 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC4.C4BillingInformation.DiagnosisCode3 = sDxCodes;
                                objC4.C4BillingInformation.ChargesAmt3 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC4.C4BillingInformation.Days_Units3 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC4.C4BillingInformation.ModifierA3 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC4.C4BillingInformation.ModifierB3 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod2Code"]);
                                objC4.C4BillingInformation.ZipCode3 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);

                            }
                            if (i == 3)
                            {
                                objC4.C4BillingInformation.DateOfServiceFromDD4 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceFromMM4 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceFromYY4 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC4.C4BillingInformation.DateOfServiceToDD4 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceToMM4 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceToYY4 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC4.C4BillingInformation.PlaceOfService4 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC4.C4BillingInformation.CPT_HCPCS4 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC4.C4BillingInformation.DiagnosisCode4 = sDxCodes;
                                objC4.C4BillingInformation.ChargesAmt4 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC4.C4BillingInformation.Days_Units4 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC4.C4BillingInformation.ModifierA4 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC4.C4BillingInformation.ModifierB4 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod2Code"]);
                                objC4.C4BillingInformation.ZipCode4 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);
                            }
                            if (i == 4)
                            {
                                objC4.C4BillingInformation.DateOfServiceFromDD5 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceFromMM5 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceFromYY5 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC4.C4BillingInformation.DateOfServiceToDD5 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceToMM5 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceToYY5 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC4.C4BillingInformation.PlaceOfService5 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC4.C4BillingInformation.CPT_HCPCS5 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC4.C4BillingInformation.DiagnosisCode5 = sDxCodes;
                                objC4.C4BillingInformation.ChargesAmt5 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC4.C4BillingInformation.Days_Units5 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC4.C4BillingInformation.ModifierA5 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC4.C4BillingInformation.ModifierB5 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod2Code"]);
                                objC4.C4BillingInformation.ZipCode5 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);
                            }
                            if (i == 5)
                            {
                                objC4.C4BillingInformation.DateOfServiceFromDD6 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceFromMM6 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceFromYY6 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC4.C4BillingInformation.DateOfServiceToDD6 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC4.C4BillingInformation.DateOfServiceToMM6 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC4.C4BillingInformation.DateOfServiceToYY6 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC4.C4BillingInformation.PlaceOfService6 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC4.C4BillingInformation.CPT_HCPCS6 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC4.C4BillingInformation.DiagnosisCode6 = sDxCodes;
                                objC4.C4BillingInformation.ChargesAmt6 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC4.C4BillingInformation.Days_Units6 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC4.C4BillingInformation.ModifierA6 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC4.C4BillingInformation.ModifierB6 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod2Code"]);
                                objC4.C4BillingInformation.ZipCode6 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);
                            }

                            if (arrlstDOS.Count > 0)
                            {
                                arrlstDOS.Sort();
                                arrlstDOS.Reverse();

                                string strDOS = null;

                                for (int j = 0; j <= arrlstDOS.Count - 1; j++)
                                {
                                    strDOS = arrlstDOS[j].ToString().Substring(4, 2) + "/" + arrlstDOS[j].ToString().Substring(6, 2) + "/" + arrlstDOS[j].ToString().Substring(0, 4) + ", " + strDOS;
                                }

                                strDOS = strDOS.Trim().TrimEnd(',').Trim();
                                objC4.C4ExamInformation.DateOfExam = strDOS;
                                strDOS = null;
                            }

                            objC4.C4BillingInformation.TotalCharges = System.Convert.ToString(dtBillingTable.Compute("SUM(dTotal)", string.Empty));

                            objC4.C4PatientInformation.DateOfInjuryDD = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateDD"]);
                            objC4.C4PatientInformation.DateOfInjuryMM = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateMM"]);
                            objC4.C4PatientInformation.DateOfInjuryYY = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateYYYY"]);


                            objC4.C4HeaderInformation.DateOfInjuryDDPage2 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateDD"]);
                            objC4.C4HeaderInformation.DateOfInjuryMMPage2 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateMM"]);
                            objC4.C4HeaderInformation.DateOfInjuryYYYYPage2 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateYYYY"]);

                            objC4.C4HeaderInformation.DateOfInjuryDDPage3 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateDD"]);
                            objC4.C4HeaderInformation.DateOfInjuryMMPage3 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateMM"]);
                            objC4.C4HeaderInformation.DateOfInjuryYYYYPage3 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateYYYY"]);

                            objC4.C4HeaderInformation.DateOfInjuryDDPage4 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateDD"]);
                            objC4.C4HeaderInformation.DateOfInjuryMMPage4 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateMM"]);
                            objC4.C4HeaderInformation.DateOfInjuryYYYYPage4 = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateYYYY"]);

                            objC4.C4PatientInformation.Carrier_CaseNo = System.Convert.ToString(dtBillingTable.Rows[0]["WorkersCompNo"]); //CaseName
                            objC4.C4PatientInformation.WCB_CaseNo = System.Convert.ToString(dtBillingTable.Rows[0]["WCBCaseNo"]);
                        }

                            objC4.C4Footer.AuthorizedProviderName = objC4.C4DoctorInformation.ProviderFullName;

                    }

                    dtBillingIDs = ds.Tables[7];

                    if (dtBillingIDs != null && dtBillingIDs.Rows.Count > 0)
                    {
                        objC4.C4DoctorInformation.WCBAuthNo = (from s in dtBillingIDs.AsEnumerable()
                                                               where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form WCB Authorization #", true) == 0
                                                               select s.Field<string>("sValue")).FirstOrDefault();
                        objC4.C4DoctorInformation.WCBRatingCode = (from s in dtBillingIDs.AsEnumerable()
                                                                   where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form WCB Rating Code", true) == 0
                                                                   select s.Field<string>("sValue")).FirstOrDefault();
                        

                        if (!string.IsNullOrEmpty(objC4.C4Footer.AuthorizedProviderName))
                        {
                            objC4.C4Footer.AuthorizedProviderSpeciality = (from s in dtBillingIDs.AsEnumerable()
                                                                           where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form Provider Specialty", true) == 0
                                                                           select s.Field<string>("sValue")).FirstOrDefault();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtPatientDemographics != null)
                { dtPatientDemographics.Dispose(); dtPatientDemographics = null; }

                if (dtEmployer != null)
                { dtEmployer.Dispose(); dtEmployer = null; }

                if (dtProvider != null)
                { dtProvider.Dispose(); dtProvider = null; }

                if (dtClinicInfo != null)
                { dtClinicInfo.Dispose(); dtClinicInfo = null; }

                if (dtInsurance != null)
                { dtInsurance.Dispose(); dtInsurance = null; }

                if (dtICD9 != null)
                { dtICD9.Dispose(); dtICD9 = null; }

                if (dtBillingTable != null)
                { dtBillingTable.Dispose(); dtBillingTable = null; }

                if (dtBillingIDs != null)
                { dtBillingIDs.Dispose(); dtBillingIDs = null; }

                sPatientFullName = null;
                arrlstDOS = null;
            }
            return ;

        }

        public void SetC42PrdefinedInfo(DataSet ds, ref C4_2ProgressReport objC42, bool blnSetBillingInfo, bool blnSetPatientDemographics)
        {
            DataTable dtPatientDemographics = null;
            DataTable dtDoctorsInfo = null;
            DataTable dtClinicInfo = null;
            DataTable dtInsurance = null;
            DataTable dtICD9 = null;
            DataTable dtBillingTable = null;
            DataTable dtBillingIDs = null;
            string sPatientFullName = null;
            ArrayList arrlstDOS = null;

            try
            {
                if (blnSetPatientDemographics)
                {
                    dtPatientDemographics = ds.Tables[0];

                    if (dtPatientDemographics != null && dtPatientDemographics.Rows.Count > 0)
                    {

                        sPatientFullName = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientLastName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientFirstName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientMiddleName"]);

                        objC42.C4_2_PatientInfo.DateOfExam = "";
                        objC42.C4_2_PatientInfo.WCBCaseNumber = "";
                        objC42.C4_2_PatientInfo.CarrierCaseNumber = "";

                        objC42.C4_2_Header.PatientNameHeader = sPatientFullName;

                        objC42.C4_2_PatientInfo.PatientFullName = sPatientFullName;

                        if (!string.IsNullOrEmpty(System.Convert.ToString(dtPatientDemographics.Rows[0]["nSSN_Formatted"]).Trim()))
                        {
                            string strSSN = System.Convert.ToString(dtPatientDemographics.Rows[0]["nSSN_Formatted"]);
                            strSSN = strSSN.Replace(" ", "");

                            objC42.C4_2_PatientInfo.PatientSSN1 = strSSN.Substring(0, 3);
                            objC42.C4_2_PatientInfo.PatientSSN2 = strSSN.Substring(3, 3);
                            objC42.C4_2_PatientInfo.PatientSSN3 = strSSN.Substring(6, 3);
                        }

                        objC42.C4_2_PatientInfo.PatientStreetAddress = System.Convert.ToString(dtPatientDemographics.Rows[0]["sAddressLine1"]) + "  " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sAddressLine2"]);
                        objC42.C4_2_PatientInfo.PatientCity = System.Convert.ToString(dtPatientDemographics.Rows[0]["sCity"]);
                        objC42.C4_2_PatientInfo.PatientState = System.Convert.ToString(dtPatientDemographics.Rows[0]["sState"]);
                        objC42.C4_2_PatientInfo.PatientZip = System.Convert.ToString(dtPatientDemographics.Rows[0]["sZIP"]);

                        objC42.C4_2_PatientInfo.PatientAccount = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientAccountNo"]);
                    }

                   

                   
                }
                //
                if (blnSetBillingInfo)
                {
                    objC42.C4_2_BillingInfo = new C4_2BillingInfo();
                    objC42.C4_2_DoctorsInfo = new C4_2DoctorsInfo();
                    
                    dtDoctorsInfo = ds.Tables[2];

                    if (dtDoctorsInfo != null && dtDoctorsInfo.Rows.Count > 0)
                    {
                        objC42.C4_2_DoctorsInfo.ProviderID = System.Convert.ToInt64(dtDoctorsInfo.Rows[0]["nProviderID"]);
                        objC42.C4_2_DoctorsInfo.ProviderSignImage = (byte[])(dtDoctorsInfo.Rows[0]["imgSignature"]);

                        objC42.C4_2_DoctorsInfo.ProviderFullName = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sProviderName"]);
                        //objC42.C4_2_DoctorsInfo.WCBRatingCode = "";//System.Convert.ToString(dtPatientDemographics.Rows[0]["sProviderName"]);
                        objC42.C4_2_DoctorsInfo.FederalTaxID = "";//System.Convert.ToString(dtPatientDemographics.Rows[0]["sProviderName"]);
                        objC42.C4_2_DoctorsInfo.blnIsEIN = false;
                        objC42.C4_2_DoctorsInfo.blnIsSSN = false;
                        objC42.C4_2_DoctorsInfo.BillingGrp = System.Convert.ToString(dtDoctorsInfo.Rows[0]["BillingGrp"]);
                        objC42.C4_2_DoctorsInfo.BillingAddressNumberStreet = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sAddress"]) + " " + System.Convert.ToString(dtDoctorsInfo.Rows[0]["sStreet"]);
                        objC42.C4_2_DoctorsInfo.BillingCity = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sCity"]);
                        objC42.C4_2_DoctorsInfo.BillingState = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sState"]);
                        objC42.C4_2_DoctorsInfo.BillingZip = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sZIP"]);
                        objC42.C4_2_DoctorsInfo.BillingPhoneCode = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sPhoneNo_Part1"]);
                        objC42.C4_2_DoctorsInfo.BillingPhone = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sPhoneNo_Part2"]);
                        //objC42.C4_2_DoctorsInfo.BillingPhoneCode = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sPhoneNo_Part1"]);
                        //objC42.C4_2_DoctorsInfo.BillingPhone = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sPhoneNo_Part2"]);
                        objC42.C4_2_DoctorsInfo.ProviderNPI = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sNPI"]);


                        if (System.Convert.ToString(dtDoctorsInfo.Rows[0]["ProviderEmpID"]).Trim() != "")
                        {
                            objC42.C4_2_DoctorsInfo.FederalTaxID = System.Convert.ToString(dtDoctorsInfo.Rows[0]["ProviderEmpID"]).Trim();
                            objC42.C4_2_DoctorsInfo.blnIsEIN = true;
                        }
                        else if (System.Convert.ToString(dtDoctorsInfo.Rows[0]["ProviderEmpSSN"]).Trim() != "")
                        {
                            objC42.C4_2_DoctorsInfo.FederalTaxID = System.Convert.ToString(dtDoctorsInfo.Rows[0]["ProviderEmpSSN"]).Trim();
                            objC42.C4_2_DoctorsInfo.blnIsSSN = true;
                        }
                        else
                        {
                            objC42.C4_2_DoctorsInfo.blnIsSSN = false;
                            objC42.C4_2_DoctorsInfo.blnIsEIN = false;
                            objC42.C4_2_DoctorsInfo.FederalTaxID = "";
                        }

                        dtClinicInfo = ds.Tables[6];

                        if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                        {
                            objC42.C4_2_DoctorsInfo.OfficeAddressNumberStreet = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicAddress"]);
                            objC42.C4_2_DoctorsInfo.OfficeCity = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicCity"]);
                            objC42.C4_2_DoctorsInfo.OfficeState = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicState"]);
                            objC42.C4_2_DoctorsInfo.OfficeZip = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicZip"]);
                            objC42.C4_2_DoctorsInfo.OfficePhoneCode = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicPhone_Part1"]);
                            objC42.C4_2_DoctorsInfo.OfficePhone = System.Convert.ToString(dtClinicInfo.Rows[0]["ClinicPhone_Part2"]);
                        }
                    }

                    dtInsurance = ds.Tables[3];

                    if (dtInsurance != null && dtInsurance.Rows.Count > 0)
                    {

                        objC42.C4_2_BillingInfo.InsuranceCarrier = System.Convert.ToString(dtInsurance.Rows[0]["InsuranceCarrierName"]);
                        objC42.C4_2_BillingInfo.CarrierCode = System.Convert.ToString(dtInsurance.Rows[0]["CarrierCode"]);
                        objC42.C4_2_BillingInfo.CarrierAddressNumberStreet = System.Convert.ToString(dtInsurance.Rows[0]["PayerFullAddress"]);
                        objC42.C4_2_BillingInfo.CarrierCity = System.Convert.ToString(dtInsurance.Rows[0]["PayerCity"]);
                        objC42.C4_2_BillingInfo.CarrierState = System.Convert.ToString(dtInsurance.Rows[0]["PayerState"]);
                        objC42.C4_2_BillingInfo.CarrierZip = System.Convert.ToString(dtInsurance.Rows[0]["PayerZip"]);
                    }

                    dtICD9 = ds.Tables[4];

                    if (dtICD9 != null && dtICD9.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtICD9.Rows.Count - 1; i++)
                        {
                            if (i == 0)
                            {
                                objC42.C4_2_BillingInfo.ICD9Code1 = System.Convert.ToString(dtICD9.Rows[i]["sDxCode"]);
                                objC42.C4_2_BillingInfo.ICD9Description1 = System.Convert.ToString(dtICD9.Rows[i]["sDxDescription"]);
                            }
                            else if (i == 1)
                            {
                                objC42.C4_2_BillingInfo.ICD9Code2 = System.Convert.ToString(dtICD9.Rows[i]["sDxCode"]);
                                objC42.C4_2_BillingInfo.ICD9Description2 = System.Convert.ToString(dtICD9.Rows[i]["sDxDescription"]);
                            }
                            else if (i == 2)
                            {
                                objC42.C4_2_BillingInfo.ICD9Code3 = System.Convert.ToString(dtICD9.Rows[i]["sDxCode"]);
                                objC42.C4_2_BillingInfo.ICD9Description3 = System.Convert.ToString(dtICD9.Rows[i]["sDxDescription"]);
                            }
                            else if (i == 3)
                            {
                                objC42.C4_2_BillingInfo.ICD9Code4 = System.Convert.ToString(dtICD9.Rows[i]["sDxCode"]);
                                objC42.C4_2_BillingInfo.ICD9Description4 = System.Convert.ToString(dtICD9.Rows[i]["sDxDescription"]);
                            }
                        }
                    }

                    

                    dtBillingTable = ds.Tables[5];
                    double dTotalCharges = 0;
                    if (dtBillingTable != null && dtBillingTable.Rows.Count > 0)
                    {
                        nServiceLinesCnt = dtBillingTable.Rows.Count;

                        arrlstDOS = new ArrayList();

                        for (int i = 0; i <= dtBillingTable.Rows.Count - 1; i++)
                        {

                            if (i == 0)
                            {
                                objC42.C4_2_BillingInfo.DateOfServiceFromDD1 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromMM1 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromYY1 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC42.C4_2_BillingInfo.DateOfServiceToDD1 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToMM1 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToYY1 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC42.C4_2_BillingInfo.PlaceOfService1 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC42.C4_2_BillingInfo.CPT_HCPCS1 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }
                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }


                                objC42.C4_2_BillingInfo.DiagnosisCode1 = sDxCodes;
                                objC42.C4_2_BillingInfo.ChargesAmt1 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC42.C4_2_BillingInfo.Days_Units1 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC42.C4_2_BillingInfo.Modifier1 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC42.C4_2_BillingInfo.ZipCode1 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);

                                dTotalCharges = dTotalCharges + System.Convert.ToDouble(dtBillingTable.Rows[i]["dTotal"]);
                            }
                            if (i == 1)
                            {
                                objC42.C4_2_BillingInfo.DateOfServiceFromDD2 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromMM2 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromYY2 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC42.C4_2_BillingInfo.DateOfServiceToDD2 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToMM2 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToYY2 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC42.C4_2_BillingInfo.PlaceOfService2 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC42.C4_2_BillingInfo.CPT_HCPCS2 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC42.C4_2_BillingInfo.DiagnosisCode2 = sDxCodes;
                                objC42.C4_2_BillingInfo.ChargesAmt2 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC42.C4_2_BillingInfo.Days_Units2 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC42.C4_2_BillingInfo.Modifier2 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC42.C4_2_BillingInfo.ZipCode2 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);

                                dTotalCharges = dTotalCharges + System.Convert.ToDouble(dtBillingTable.Rows[i]["dTotal"]);

                            }
                            if (i == 2)
                            {
                                objC42.C4_2_BillingInfo.DateOfServiceFromDD3 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromMM3 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromYY3 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC42.C4_2_BillingInfo.DateOfServiceToDD3 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToMM3 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToYY3 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC42.C4_2_BillingInfo.PlaceOfService3 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC42.C4_2_BillingInfo.CPT_HCPCS3 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC42.C4_2_BillingInfo.DiagnosisCode3 = sDxCodes;
                                objC42.C4_2_BillingInfo.ChargesAmt3 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC42.C4_2_BillingInfo.Days_Units3 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC42.C4_2_BillingInfo.ModifierA3 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC42.C4_2_BillingInfo.ZipCode3 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);

                                dTotalCharges = dTotalCharges + System.Convert.ToDouble(dtBillingTable.Rows[i]["dTotal"]);

                            }
                            if (i == 3)
                            {
                                objC42.C4_2_BillingInfo.DateOfServiceFromDD4 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromMM4 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromYY4 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC42.C4_2_BillingInfo.DateOfServiceToDD4 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToMM4 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToYY4 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC42.C4_2_BillingInfo.PlaceOfService4 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC42.C4_2_BillingInfo.CPT_HCPCS4 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC42.C4_2_BillingInfo.DiagnosisCode4 = sDxCodes;
                                objC42.C4_2_BillingInfo.ChargesAmt4 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC42.C4_2_BillingInfo.Days_Units4 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC42.C4_2_BillingInfo.ModifierA4 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC42.C4_2_BillingInfo.ZipCode4 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);

                                dTotalCharges = dTotalCharges + System.Convert.ToDouble(dtBillingTable.Rows[i]["dTotal"]);

                            }
                            if (i == 4)
                            {
                                objC42.C4_2_BillingInfo.DateOfServiceFromDD5 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromMM5 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromYY5 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC42.C4_2_BillingInfo.DateOfServiceToDD5 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToMM5 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToYY5 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC42.C4_2_BillingInfo.PlaceOfService5 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC42.C4_2_BillingInfo.CPT_HCPCS5 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC42.C4_2_BillingInfo.DiagnosisCode5 = sDxCodes;
                                objC42.C4_2_BillingInfo.ChargesAmt5 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC42.C4_2_BillingInfo.Days_Units5 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC42.C4_2_BillingInfo.ModifierA5 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC42.C4_2_BillingInfo.ZipCode5 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);

                                dTotalCharges = dTotalCharges + System.Convert.ToDouble(dtBillingTable.Rows[i]["dTotal"]);

                            }
                            if (i == 5)
                            {
                                objC42.C4_2_BillingInfo.DateOfServiceFromDD6 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromMM6 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceFromYY6 = System.Convert.ToString(dtBillingTable.Rows[i]["FromDateYY"]);

                                if (!arrlstDOS.Contains(dtBillingTable.Rows[i]["nFromDate"]))
                                {
                                    arrlstDOS.Add(dtBillingTable.Rows[i]["nFromDate"]);
                                }

                                objC42.C4_2_BillingInfo.DateOfServiceToDD6 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateDD"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToMM6 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateMM"]);
                                objC42.C4_2_BillingInfo.DateOfServiceToYY6 = System.Convert.ToString(dtBillingTable.Rows[i]["ToDateYY"]);

                                objC42.C4_2_BillingInfo.PlaceOfService6 = System.Convert.ToString(dtBillingTable.Rows[i]["sPOSCode"]);

                                objC42.C4_2_BillingInfo.CPT_HCPCS6 = System.Convert.ToString(dtBillingTable.Rows[i]["sCPTCode"]);

                                string sDxCodes = null;
                                char[] arr = new char[] { ',', ' ' }; // Trim these characters.

                                for (int j = 0; j <= dtICD9.Rows.Count - 1; j++)
                                {
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx1Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx2Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx3Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                    if (System.Convert.ToString(dtBillingTable.Rows[i]["sDx4Code"]) == System.Convert.ToString(dtICD9.Rows[j]["sDxCode"]))
                                    {
                                        sDxCodes = sDxCodes + ", " + System.Convert.ToString(dtICD9.Rows[j]["iRowID"]);
                                    }
                                }

                                if (sDxCodes != null)
                                {
                                    sDxCodes = sDxCodes.TrimStart(arr).TrimEnd(arr);
                                }

                                objC42.C4_2_BillingInfo.DiagnosisCode6 = sDxCodes;
                                objC42.C4_2_BillingInfo.ChargesAmt6 = System.Convert.ToString(dtBillingTable.Rows[i]["dTotal"]);
                                objC42.C4_2_BillingInfo.Days_Units6 = System.Convert.ToString(dtBillingTable.Rows[i]["dUnit"]);
                                objC42.C4_2_BillingInfo.ModifierA6 = System.Convert.ToString(dtBillingTable.Rows[i]["sMod1Code"]);
                                objC42.C4_2_BillingInfo.ZipCode6 = System.Convert.ToString(dtBillingTable.Rows[i]["sFacilityZip"]);

                                dTotalCharges = dTotalCharges + System.Convert.ToDouble(dtBillingTable.Rows[i]["dTotal"]);
                            }

                            objC42.C4_2_BillingInfo.TotalCharges =System.Convert.ToString(dTotalCharges);//Convert.ToString(dtBillingTable.Compute("SUM(dTotal)", string.Empty));

                            objC42.C4_2_Header.DateOfInjuryHeaderDD = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateDD"]);
                            objC42.C4_2_Header.DateOfInjuryHeaderMM = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateMM"]);
                            objC42.C4_2_Header.DateOfInjuryHeaderYYYY = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateYYYY"]);

                            objC42.C4_2_PatientInfo.DateOfInjuryMM = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateMM"]);
                            objC42.C4_2_PatientInfo.DateOfInjuryDD = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateDD"]);
                            objC42.C4_2_PatientInfo.DateOfInjuryYY = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDateYYYY"]);

                            objC42.C4_2_PatientInfo.CarrierCaseNumber = System.Convert.ToString(dtBillingTable.Rows[0]["WorkersCompNo"]);
                            objC42.C4_2_PatientInfo.WCBCaseNumber = System.Convert.ToString(dtBillingTable.Rows[0]["WCBCaseNo"]);
                        }

                        if (arrlstDOS.Count > 0)
                        {
                            arrlstDOS.Sort();

                            string strDOS = null;

                            for (int j = 0; j <= arrlstDOS.Count - 1; j++)
                            {
                                strDOS = strDOS + ", " + arrlstDOS[j].ToString().Substring(4, 2) + "/" + arrlstDOS[j].ToString().Substring(6, 2) + "/" + arrlstDOS[j].ToString().Substring(0, 4);
                            }

                            strDOS = strDOS.Trim().TrimStart(',').Trim();
                            objC42.C4_2_PatientInfo.DateOfExam = strDOS;
                            strDOS = null;
                        }

                        objC42.C4_2_Footer.AuthorizedProviderName = objC42.C4_2_DoctorsInfo.ProviderFullName;

                    }

                    dtBillingIDs = ds.Tables[7];

                    if (dtBillingIDs != null && dtBillingIDs.Rows.Count > 0)
                    {
                        objC42.C4_2_DoctorsInfo.WCBAuthNo =  (from s in dtBillingIDs.AsEnumerable()
                                                               where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form WCB Authorization #", true) == 0
                                                               select s.Field<string>("sValue")).FirstOrDefault();
                        objC42.C4_2_DoctorsInfo.WCBRatingCode =  (from s in dtBillingIDs.AsEnumerable()
                                                              where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form WCB Rating Code", true) == 0
                                        select s.Field<string>("sValue")).FirstOrDefault();

                        

                        if (!string.IsNullOrEmpty(objC42.C4_2_Footer.AuthorizedProviderName))
                        { 
                            objC42.C4_2_Footer.AuthorizedProviderSpeciality = (from s in dtBillingIDs.AsEnumerable()
                                                                       where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form Provider Specialty", true) == 0
                                                                       select s.Field<string>("sValue")).FirstOrDefault();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (dtPatientDemographics != null)
                { dtPatientDemographics.Dispose(); dtPatientDemographics = null; }

                if (dtDoctorsInfo != null)
                { dtDoctorsInfo.Dispose(); dtDoctorsInfo = null; }

                if (dtClinicInfo != null)
                { dtClinicInfo.Dispose(); dtClinicInfo = null; }

                if (dtInsurance != null)
                { dtInsurance.Dispose(); dtInsurance = null; }

                if (dtICD9 != null)
                { dtICD9.Dispose(); dtICD9 = null; }

                if (dtBillingTable != null)
                { dtBillingTable.Dispose(); dtBillingTable = null; }

                if (dtBillingIDs != null)
                { dtBillingIDs.Dispose(); dtBillingIDs = null; }

                sPatientFullName = null;
                arrlstDOS = null;
            }

            return ;
        }

        public void SetC4AuthorizationPrdefinedInfo(DataSet ds, ref C4Authorization objC4Auth, bool blnSetBillingInfo, bool blnSetPatientDemographics)
        {
            DataTable dtPatientDemographics = null;
            DataTable dtDoctorsInfo = null;
            DataTable dtEmpInfo = null;
            DataTable dtInsurance = null;
            DataTable dtBillingTable = null;
            DataTable dtBillingIDs = null;

            try
            {
                if (blnSetPatientDemographics)
                {
                    dtPatientDemographics = ds.Tables[0];

                    if (dtPatientDemographics != null && dtPatientDemographics.Rows.Count > 0)
                    {

                        objC4Auth.PatientName = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientFirstName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientMiddleName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientLastName"]);

                        objC4Auth.SSN = System.Convert.ToString(dtPatientDemographics.Rows[0]["nSSN"]).Trim();

                        objC4Auth.PatientFullAddress =  System.Convert.ToString(dtPatientDemographics.Rows[0]["sAddressLine1"]) + " " + 
                                                        System.Convert.ToString(dtPatientDemographics.Rows[0]["sAddressLine2"]) + "    " +
                                                        System.Convert.ToString(dtPatientDemographics.Rows[0]["sCity"]) + "    "+
                                                        System.Convert.ToString(dtPatientDemographics.Rows[0]["sState"]) + "    " +
                                                        System.Convert.ToString(dtPatientDemographics.Rows[0]["sZIP"]);
                    }

                    dtEmpInfo = ds.Tables[1];

                    if (dtEmpInfo != null && dtEmpInfo.Rows.Count > 0)
                    {
                        objC4Auth.EmployerName = System.Convert.ToString(dtEmpInfo.Rows[0]["sEmployerName"]);
                        objC4Auth.EmployerFullAddress = System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkAddressLine1"]) + " " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkAddressLine2"]) + "    " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkCity"]) + "    " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkState"]) + "    " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkZIP"]);

                    }

                   

                }
                if (blnSetBillingInfo)
                {
                    objC4Auth.InsuranceCarrierName = "";
                    objC4Auth.InsuranceCarrierFullAddress = "";
                    objC4Auth.WCBCaseNo = "";
                    objC4Auth.CarrierCaseNo = "";
                    objC4Auth.DateOfInjury = "";

                    objC4Auth.AttendingDoctorsName = "";
                    objC4Auth.AttendingDoctorsFullAddress = "";
                    objC4Auth.ProviderAuthorizationNo = "";
                    objC4Auth.ProviderPhoneNo = "";
                    objC4Auth.ProviderFaxNo = "";

                    dtDoctorsInfo = ds.Tables[2];

                    if (dtDoctorsInfo != null && dtDoctorsInfo.Rows.Count > 0)
                    {
                        objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderID = System.Convert.ToInt64(dtDoctorsInfo.Rows[0]["nProviderID"]);
                        objC4Auth.C4_Auth_AuthorizationRequest.C4_Auth_StatementOfMedicalNecessity.ProviderSignImage = (byte[])(dtDoctorsInfo.Rows[0]["imgSignature"]);

                        objC4Auth.AttendingDoctorsName = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sProviderName"]);

                        objC4Auth.AttendingDoctorsFullAddress = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sAddress"]) + " " + System.Convert.ToString(dtDoctorsInfo.Rows[0]["sStreet"]) + "    " +
                                                                System.Convert.ToString(dtDoctorsInfo.Rows[0]["sCity"]) + "    " +
                                                                System.Convert.ToString(dtDoctorsInfo.Rows[0]["sState"]) + "    " +
                                                                System.Convert.ToString(dtDoctorsInfo.Rows[0]["sZIP"]);
                        objC4Auth.ProviderPhoneNo = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sPhoneNo_Part1"]) + "-" +
                                                    System.Convert.ToString(dtDoctorsInfo.Rows[0]["sPhoneNo_Part2"]);
                        objC4Auth.ProviderPhoneNo = objC4Auth.ProviderPhoneNo.Trim().Trim('-');

                        objC4Auth.ProviderFaxNo = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sFAX"]);
                    }

                    dtInsurance = ds.Tables[3];

                    if (dtInsurance != null && dtInsurance.Rows.Count > 0)
                    {
                        objC4Auth.InsuranceCarrierName=System.Convert.ToString(dtInsurance.Rows[0]["InsuranceCarrierName"]);
                        objC4Auth.InsuranceCarrierFullAddress=  System.Convert.ToString(dtInsurance.Rows[0]["PayerFullAddress"]) + "   " +
                                                                System.Convert.ToString(dtInsurance.Rows[0]["PayerCity"])+ "   " +
                                                                System.Convert.ToString(dtInsurance.Rows[0]["PayerState"])+ "   " +
                                                                System.Convert.ToString(dtInsurance.Rows[0]["PayerZip"]);
                    }

                    dtBillingIDs = ds.Tables[7];

                    if (dtBillingIDs != null && dtBillingIDs.Rows.Count > 0)
                    {
                        objC4Auth.ProviderAuthorizationNo = (from s in dtBillingIDs.AsEnumerable()
                                                             where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form WCB Authorization #", true) == 0
                                                             select s.Field<string>("sValue")).FirstOrDefault();
                    }

                    dtBillingTable = ds.Tables[5];

                    if (dtBillingTable != null && dtBillingTable.Rows.Count > 0)
                    {
                        objC4Auth.WCBCaseNo = System.Convert.ToString(dtBillingTable.Rows[0]["WCBCaseNo"]);
                        objC4Auth.CarrierCaseNo = System.Convert.ToString(dtBillingTable.Rows[0]["WorkersCompNo"]);//CaseName
                        objC4Auth.DateOfInjury = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (dtPatientDemographics != null)
                { dtPatientDemographics.Dispose(); dtPatientDemographics = null; }

                if (dtDoctorsInfo != null)
                { dtDoctorsInfo.Dispose(); dtDoctorsInfo = null; }

                if (dtEmpInfo != null)
                { dtEmpInfo.Dispose(); dtEmpInfo = null; }

                if (dtInsurance != null)
                { dtInsurance.Dispose(); dtInsurance = null; }

                if (dtBillingTable != null)
                { dtBillingTable.Dispose(); dtBillingTable = null; }

                if (dtBillingIDs!= null)
                { dtBillingIDs.Dispose(); dtBillingIDs = null; }
            }

            return ;
        }

        public void SetMG2PrdefinedInfo(DataSet ds, ref MG2 objMG2, bool blnSetBillingInfo, bool blnSetPatientDemographics)
        {
            DataTable dtPatientDemographics = null;
            DataTable dtDoctorsInfo = null;
            DataTable dtEmpInfo = null;
            DataTable dtInsurance = null;
            DataTable dtBillingTable = null;
            DataTable dtBillingIDs = null;

            try
            {
                if (blnSetPatientDemographics)
                {
                    dtPatientDemographics = ds.Tables[0];

                    if (dtPatientDemographics != null && dtPatientDemographics.Rows.Count > 0)
                    {

                        objMG2.MG2_HeaderInfo.PatientsName_MG2HeaderInfo = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientFirstName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientMiddleName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientLastName"]);
                        objMG2.MG2_PatientDetails.PatientsFullName = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientFirstName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientMiddleName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientLastName"]);
                        objMG2.MG2_PatientDetails.PatientSSN = System.Convert.ToString(dtPatientDemographics.Rows[0]["nSSN"]).Trim();

                        objMG2.MG2_PatientDetails.PatientsFullAddress = System.Convert.ToString(dtPatientDemographics.Rows[0]["sAddressLine1"]) + " " +
                                                        System.Convert.ToString(dtPatientDemographics.Rows[0]["sAddressLine2"]) + "    " +
                                                        System.Convert.ToString(dtPatientDemographics.Rows[0]["sCity"]) + "    " +
                                                        System.Convert.ToString(dtPatientDemographics.Rows[0]["sState"]) + "    " +
                                                        System.Convert.ToString(dtPatientDemographics.Rows[0]["sZIP"]);
                    }

                    dtEmpInfo = ds.Tables[1];

                    if (dtEmpInfo != null && dtEmpInfo.Rows.Count > 0)
                    {
                        objMG2.MG2_PatientDetails.EmployerNameAndAddress = System.Convert.ToString(dtEmpInfo.Rows[0]["sEmployerName"]) + "    " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkAddressLine1"]) + " " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkAddressLine2"]) + "    " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkCity"]) + "    " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkState"]) + "    " +
                                                        System.Convert.ToString(dtEmpInfo.Rows[0]["sWorkZIP"]);

                    }

                   

                }
                if (blnSetBillingInfo)
                {
                    objMG2.MG2_PatientDetails.InsuranceCarrierNameAndAddress = "";
                    
                    objMG2.MG2_HeaderInfo.WCBCaseNumber_MG2HeaderInfo = "";
                    objMG2.MG2_CaseDetails.WCBCaseNumber_CaseDetails = "";
                    
                    objMG2.MG2_HeaderInfo.DateOfInjury_MG2HeaderInfo = "";
                    objMG2.MG2_CaseDetails.DateOfInjury_CaseDetails = "";

                    objMG2.MG2_AttendingDoctorInformation.AttendingDoctorsNameAndAddress = "";
                    objMG2.MG2_AttendingDoctorInformation.TelephoneNumber = "";
                    objMG2.MG2_AttendingDoctorInformation.FaxNumber = "";

                    objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber1 = "";
                    objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber2 = "";
                    objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber3 = "";
                    objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber4 = "";
                    objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber5 = "";
                    objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber6 = "";
                    objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber7 = "";
                    objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber8 = "";

                    objMG2.MG2_ApprovalRequestVarianceFor.ProviderID = 0;
                    objMG2.MG2_ApprovalRequestVarianceFor.ProviderSignImage = null;

                    dtDoctorsInfo = ds.Tables[2];

                    if (dtDoctorsInfo != null && dtDoctorsInfo.Rows.Count > 0)
                    {
                        objMG2.MG2_ApprovalRequestVarianceFor.ProviderID = System.Convert.ToInt64(dtDoctorsInfo.Rows[0]["nProviderID"]);
                        objMG2.MG2_ApprovalRequestVarianceFor.ProviderSignImage = (byte[])(dtDoctorsInfo.Rows[0]["imgSignature"]);

                        objMG2.MG2_AttendingDoctorInformation.AttendingDoctorsNameAndAddress = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sProviderName"]) + "    " +
                                                                System.Convert.ToString(dtDoctorsInfo.Rows[0]["sAddress"]) + " " + System.Convert.ToString(dtDoctorsInfo.Rows[0]["sStreet"]) + "    " +
                                                                System.Convert.ToString(dtDoctorsInfo.Rows[0]["sCity"]) + "    " +
                                                                System.Convert.ToString(dtDoctorsInfo.Rows[0]["sState"]) + "    " +
                                                                System.Convert.ToString(dtDoctorsInfo.Rows[0]["sZIP"]);
                        objMG2.MG2_AttendingDoctorInformation.TelephoneNumber = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sPhoneNo_Part1"]) + "-" +
                                                    System.Convert.ToString(dtDoctorsInfo.Rows[0]["sPhoneNo_Part2"]);

                        objMG2.MG2_AttendingDoctorInformation.TelephoneNumber = objMG2.MG2_AttendingDoctorInformation.TelephoneNumber.Trim().Trim('-');

                        objMG2.MG2_AttendingDoctorInformation.FaxNumber = System.Convert.ToString(dtDoctorsInfo.Rows[0]["sFAX"]);

                        // Provider's WCB Authorization No needs to show

                        //objMG2.ProviderAuthorizationNo = "";
                    }

                    dtBillingIDs = ds.Tables[7];

                    if (dtBillingIDs != null && dtBillingIDs.Rows.Count > 0)
                    {

                        string WCBAuthNo = (from s in dtBillingIDs.AsEnumerable()
                                                             where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form WCB Authorization #", true) == 0
                                                             select s.Field<string>("sValue")).FirstOrDefault();
                        if (!string.IsNullOrEmpty(WCBAuthNo))
                        {

                            objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber1 = WCBAuthNo.Substring(0, 1);
                            objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber2 = (WCBAuthNo.Length > 1) ? WCBAuthNo.Substring(1, 1) : "";
                            objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber3 = (WCBAuthNo.Length > 2) ? WCBAuthNo.Substring(2, 1) : "";
                            objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber4 = (WCBAuthNo.Length > 3) ? WCBAuthNo.Substring(3, 1) : "";
                            objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber5 = (WCBAuthNo.Length > 4) ? WCBAuthNo.Substring(4, 1) : "";
                            objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber6 = (WCBAuthNo.Length > 5) ? WCBAuthNo.Substring(5, 1) : "";
                            objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber7 = (WCBAuthNo.Length > 6) ? WCBAuthNo.Substring(6, 1) : "";
                            objMG2.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber8 = (WCBAuthNo.Length > 7) ? WCBAuthNo.Substring(7, 1) : "";
                        }
                    }

                    dtInsurance = ds.Tables[3];

                    if (dtInsurance != null && dtInsurance.Rows.Count > 0)
                    {
                        objMG2.MG2_PatientDetails.InsuranceCarrierNameAndAddress = System.Convert.ToString(dtInsurance.Rows[0]["InsuranceCarrierName"]) + "   " +
                                                                System.Convert.ToString(dtInsurance.Rows[0]["PayerFullAddress"]) + "   " +
                                                                System.Convert.ToString(dtInsurance.Rows[0]["PayerCity"]) + "   " +
                                                                System.Convert.ToString(dtInsurance.Rows[0]["PayerState"]) + "   " +
                                                                System.Convert.ToString(dtInsurance.Rows[0]["PayerZip"]);
                    }

                    dtBillingTable = ds.Tables[5];

                    if (dtBillingTable != null && dtBillingTable.Rows.Count > 0)
                    {
                        objMG2.MG2_HeaderInfo.WCBCaseNumber_MG2HeaderInfo = System.Convert.ToString(dtBillingTable.Rows[0]["WCBCaseNo"]);
                        objMG2.MG2_HeaderInfo.DateOfInjury_MG2HeaderInfo = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDate"]);

                        objMG2.MG2_CaseDetails.WCBCaseNumber_CaseDetails= System.Convert.ToString(dtBillingTable.Rows[0]["WCBCaseNo"]);
                        objMG2.MG2_CaseDetails.CarrierCaseNumber_CaseDetails = System.Convert.ToString(dtBillingTable.Rows[0]["WorkersCompNo"]);//CaseName
                        objMG2.MG2_CaseDetails.DateOfInjury_CaseDetails = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (dtPatientDemographics != null)
                { dtPatientDemographics.Dispose(); dtPatientDemographics = null; }

                if (dtDoctorsInfo != null)
                { dtDoctorsInfo.Dispose(); dtDoctorsInfo = null; }

                if (dtEmpInfo != null)
                { dtEmpInfo.Dispose(); dtEmpInfo = null; }

                if (dtInsurance != null)
                { dtInsurance.Dispose(); dtInsurance = null; }

                if (dtBillingTable != null)
                { dtBillingTable.Dispose(); dtBillingTable = null; }
            }

            return ;
        }

        public void SetMG21PrdefinedInfo(DataSet ds, ref MG2_1 objMG21, bool blnSetBillingInfo, bool blnSetPatientDemographics)
        {
            DataTable dtPatientDemographics = null;
            DataTable dtDoctorsInfo = null;
            DataTable dtEmpInfo = null;
            DataTable dtInsurance = null;
            DataTable dtBillingTable = null;
            DataTable dtBillingIDs = null;

            try
            {
                if (blnSetPatientDemographics)
                {
                    dtPatientDemographics = ds.Tables[0];

                    if (dtPatientDemographics != null && dtPatientDemographics.Rows.Count > 0)
                    {
                        objMG21.MG2_1_HeaderInfo.PatientsName_MG2_1HeaderInfo = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientFirstName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientMiddleName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientLastName"]);
                        objMG21.MG2_1_PatientDetails.PatientsFullName = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientFirstName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientMiddleName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientLastName"]);
                        objMG21.MG2_1_PatientDetails.PatientSSN = System.Convert.ToString(dtPatientDemographics.Rows[0]["nSSN"]).Trim();
                    }

                    dtEmpInfo = ds.Tables[1];

                    if (dtEmpInfo!=null && dtEmpInfo.Rows.Count>0)
                    {
                        
                    }
                    //dtEmpInfo = ds.Tables[1];

                    //if (dtEmpInfo != null && dtEmpInfo.Rows.Count > 0)
                    //{
                    //    objMG21.MG2_1_AttendingDoctorInformation.DoctorsName = System.Convert.ToString(dtEmpInfo.Rows[0]["sEmployerName"]);
                    //}



                }
                if (blnSetBillingInfo)
                {
                    //Page 2 Header Info
                    objMG21.MG2_1_HeaderInfo.WCBCaseNumber_MG2_1HeaderInfo = "";
                    objMG21.MG2_1_HeaderInfo.DateOfInjury_MG2_1HeaderInfo = "";
                    //

                    objMG21.MG2_1_CaseDetails.WCBCaseNumber_CaseDetails = "";
                    objMG21.MG2_1_CaseDetails.CarrierCaseNumber_CaseDetails = "";
                    objMG21.MG2_1_CaseDetails.DateOfInjury_CaseDetails = "";

                    objMG21.MG2_1_AttendingDoctorInformation.DoctorsWCBAuthorizationNumber = "";

                    objMG21.MG2_1_HealthProviderCertification.ProviderID = 0;
                    objMG21.MG2_1_HealthProviderCertification.ProviderSignImage = null;


                    //objMG21.MG2_1_HeaderInfo.PatientsName_MG2_1HeaderInfo = System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientFirstName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientMiddleName"]) + " " + System.Convert.ToString(dtPatientDemographics.Rows[0]["sPatientLastName"]);


                    dtDoctorsInfo = ds.Tables[2];

                    if (dtDoctorsInfo != null && dtDoctorsInfo.Rows.Count > 0)
                    {
                        objMG21.MG2_1_HealthProviderCertification.ProviderID = System.Convert.ToInt64(dtDoctorsInfo.Rows[0]["nProviderID"]);
                        objMG21.MG2_1_HealthProviderCertification.ProviderSignImage =(byte[])( dtDoctorsInfo.Rows[0]["imgSignature"]);

                        objMG21.MG2_1_AttendingDoctorInformation.DoctorsName= System.Convert.ToString(dtDoctorsInfo.Rows[0]["sProviderName"]);

                    }

                    dtBillingIDs = ds.Tables[7];

                    if (dtBillingIDs != null && dtBillingIDs.Rows.Count > 0)
                    {

                        string WCBAuthNo = (from s in dtBillingIDs.AsEnumerable()
                                            where string.Compare(s.Field<string>("sAdditionalDescription"), "NYWC Form WCB Authorization #", true) == 0
                                            select s.Field<string>("sValue")).FirstOrDefault();
                        if (!string.IsNullOrEmpty(WCBAuthNo))
                        {

                            objMG21.MG2_1_AttendingDoctorInformation.DoctorsWCBAuthorizationNumber = WCBAuthNo;
                            //objMG21.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber2 = (WCBAuthNo.Length > 1) ? WCBAuthNo.Substring(1, 1) : "";
                            //objMG21.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber3 = (WCBAuthNo.Length > 2) ? WCBAuthNo.Substring(2, 1) : "";
                            //objMG21.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber4 = (WCBAuthNo.Length > 3) ? WCBAuthNo.Substring(3, 1) : "";
                            //objMG21.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber5 = (WCBAuthNo.Length > 4) ? WCBAuthNo.Substring(4, 1) : "";
                            //objMG21.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber6 = (WCBAuthNo.Length > 5) ? WCBAuthNo.Substring(5, 1) : "";
                            //objMG21.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber7 = (WCBAuthNo.Length > 6) ? WCBAuthNo.Substring(6, 1) : "";
                            //objMG21.MG2_AttendingDoctorInformation.IndividualWCBAuthorizationNumber8 = (WCBAuthNo.Length > 7) ? WCBAuthNo.Substring(7, 1) : "";
                        }
                    }

                    dtInsurance = ds.Tables[3];

                    if (dtInsurance != null && dtInsurance.Rows.Count > 0)
                    {
                        //objMG21.MG2_PatientDetails.InsuranceCarrierNameAndAddress = System.Convert.ToString(dtInsurance.Rows[0]["InsuranceCarrierName"]) + "   " +
                        //                                        System.Convert.ToString(dtInsurance.Rows[0]["PayerFullAddress"]) + "   " +
                        //                                        System.Convert.ToString(dtInsurance.Rows[0]["PayerCity"]) + "   " +
                        //                                        System.Convert.ToString(dtInsurance.Rows[0]["PayerState"]) + "   " +
                        //                                        System.Convert.ToString(dtInsurance.Rows[0]["PayerZip"]);
                    }

                    dtBillingTable = ds.Tables[5];

                    if (dtBillingTable != null && dtBillingTable.Rows.Count > 0)
                    {
                        objMG21.MG2_1_HeaderInfo.WCBCaseNumber_MG2_1HeaderInfo = System.Convert.ToString(dtBillingTable.Rows[0]["WCBCaseNo"]);

                    //    objMG21.MG2_1_HeaderInfo.PatientsName_MG2_1HeaderInfo = System.Convert.ToString(dtBillingTable.Rows[0]["WCBCaseNo"]);
                        objMG21.MG2_1_HeaderInfo.DateOfInjury_MG2_1HeaderInfo = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDate"]);

                        objMG21.MG2_1_CaseDetails.WCBCaseNumber_CaseDetails = System.Convert.ToString(dtBillingTable.Rows[0]["WCBCaseNo"]);
                        objMG21.MG2_1_CaseDetails.CarrierCaseNumber_CaseDetails = System.Convert.ToString(dtBillingTable.Rows[0]["WorkersCompNo"]);//CaseName
                        objMG21.MG2_1_CaseDetails.DateOfInjury_CaseDetails = System.Convert.ToString(dtBillingTable.Rows[0]["InjuryDate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (dtPatientDemographics != null)
                { dtPatientDemographics.Dispose(); dtPatientDemographics = null; }

                if (dtDoctorsInfo != null)
                { dtDoctorsInfo.Dispose(); dtDoctorsInfo = null; }

                if (dtEmpInfo != null)
                { dtEmpInfo.Dispose(); dtEmpInfo = null; }

                if (dtInsurance != null)
                { dtInsurance.Dispose(); dtInsurance = null; }

                if (dtBillingTable != null)
                { dtBillingTable.Dispose(); dtBillingTable = null; }
            }

            return;
        }

        public void ConnectToPDFTron()
        {
            if (gIsPDFTronConnected == true)
            {
                DisconnectToPDFTron();
            }

            try
            {
                //changed license key 
                pdftron.PDFNet.Initialize("gloStream, Inc.(glostream.com):OEM:gloEMR::W:AMC(20130603):4DE63118A4FA49B931EDEC194A2640E528387DE495B2C9112BD15C49D07AF0FA");

                string strResourcePath = Application.StartupPath + "\\pdfnet.res";
                pdftron.PDFNet.SetResourcesPath(strResourcePath);

                gIsPDFTronConnected = true;
            }
            catch (pdftron.Common.PDFNetException ex)
            {
                gIsPDFTronExpired = true;
                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.Message;
                //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                gloAuditTrail.gloAuditTrail.ExceptionLog(_MessageString, false);
            }

        }

        public void DisconnectToPDFTron()
        {
            try
            {
                pdftron.PDFNet.Terminate();
                gIsPDFTronConnected = false;
            }
            catch (pdftron.Common.PDFNetException ex)
            {
                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.Message;
                //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                gloAuditTrail.gloAuditTrail.ExceptionLog(_MessageString, false);
            }
        }

        public DataTable GetUniqueIDs(Int64 iCount, string sDBConnectionString, string _messageBoxCaption)
        {
            DataTable _dtUniqueIDs = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sDBConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oParameters.Clear();
                oDB.Connect(false);
                oParameters.Add("@IDCount", iCount, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@SingleRow", 1, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Retrive("gsp_GetUniqueIDS", oParameters, out _dtUniqueIDs);
                oDB.Disconnect();
                return _dtUniqueIDs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return _dtUniqueIDs;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (_dtUniqueIDs != null) { _dtUniqueIDs.Dispose(); _dtUniqueIDs = null; }
            }

        }

        public long GetUniqueID(string sDBConnectionString, string _messageBoxCaption)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sDBConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object oResult = new object();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@ID", "0", ParameterDirection.Output, SqlDbType.BigInt);
                oDB.Execute("gsp_GetUniqueID", oParameters, out oResult);
                oDB.Disconnect();

                return System.Convert.ToInt64(oResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                oResult = null;

                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if ((oParameters != null))
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }

        }

        public string SignPDFImage(ref PDFDoc doc, byte[] bytProviderSignImg, string FieldDisplayName, Int16 PageNo)
        {
            Field sigField = null;
            Widget widgetAnnot = null;
            Widget a = null;
            ElementWriter apWriter = null;
            ElementBuilder apBuilder = null;
            pdftron.PDF.Image sigImg = null;
            Element apElement = null;
            Obj apObj = null;
            Page page = null;
            gloPictureBox.gloPictureBox pictbxSign = null;
            string fname = string.Empty;

            try
            {
                page = doc.GetPage(PageNo);

                int num_annots = page.GetNumAnnots();

                for (int i = 0; i < num_annots; ++i)
                {
                    Annot annot = page.GetAnnot(i);
                    if (annot.IsValid() == false) continue;

                    widgetAnnot = new Widget(annot);
                    sigField = widgetAnnot.GetField();


                    string widgetName = sigField.GetName();

                    if (widgetName == FieldDisplayName)
                    {
                        Rect bbox = annot.GetRect();

                        //arltSign = objSign.GetSignatureFormat(nProviderId, 2, 0);

                        if (bytProviderSignImg.Length > 0)
                        {
                            a = widgetAnnot;
                            annot.RemoveAppearance();

                            pictbxSign = new gloPictureBox.gloPictureBox();
                            pictbxSign.byteImage = bytProviderSignImg;

                            apWriter = new ElementWriter();
                            apBuilder = new ElementBuilder();

                            sigImg = pdftron.PDF.Image.Create(doc.GetSDFDoc(), (System.Drawing.Bitmap)pictbxSign.Image);
                            apWriter.Begin(doc.GetSDFDoc());

                            double w = sigImg.GetImageWidth();
                            double h = sigImg.GetImageHeight();

                            apElement = apBuilder.CreateImage(sigImg, 0, 0, w, h);
                            apWriter.WritePlacedElement(apElement);

                            apObj = apWriter.End();

                            apObj.PutRect("BBox", 0, 0, w, h);
                            apObj.PutName("Subtype", "Form");
                            apObj.PutName("Type", "XObject");

                            a.SetAppearance(apObj, Annot.AnnotationState.e_normal);
                            a.SetFlag(Annot.Flag.e_print, true);
                            page.AnnotPushBack(a);

                        }
                        else
                        {
                            a = widgetAnnot;
                            annot.RemoveAppearance();

                            System.Drawing.Bitmap myBmp = new System.Drawing.Bitmap((int)bbox.Width(), (int)bbox.Height());
                            System.Drawing.Graphics grp = System.Drawing.Graphics.FromImage(myBmp);
                            System.Drawing.SolidBrush brsh = new System.Drawing.SolidBrush(System.Drawing.Color.White);

                            grp.FillRectangle(brsh, 0, 0, myBmp.Width, myBmp.Height);

                            apWriter = new ElementWriter();
                            apBuilder = new ElementBuilder();
                            sigImg = pdftron.PDF.Image.Create(doc.GetSDFDoc(), myBmp);
                            apWriter.Begin(doc.GetSDFDoc());



                            double w = sigImg.GetImageWidth();
                            double h = sigImg.GetImageHeight();

                            brsh.Dispose();
                            grp.Dispose();
                            myBmp.Dispose();

                            apElement = apBuilder.CreateImage(sigImg, 0, 0, w, h);
                            apWriter.WritePlacedElement(apElement);

                            apObj = apWriter.End();

                            apObj.PutRect("BBox", 0, 0, w, h);
                            apObj.PutName("Subtype", "Form");
                            apObj.PutName("Type", "XObject");

                            a.SetAppearance(apObj, Annot.AnnotationState.e_normal);
                            a.SetFlag(Annot.Flag.e_print, true);

                            page.AnnotPushBack(a);

                        }

                        fname = _AppTempFolderPathNYWC + "InsertSign_" + gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") + "_" + Guid.NewGuid().ToString() + ".pdf";
                        doc.Save(fname, SDFDoc.SaveOptions.e_compatibility);

                        break;

                    }
                    widgetAnnot.Dispose();
                    widgetAnnot = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (pictbxSign != null) { pictbxSign.Dispose(); pictbxSign = null; }
                if (apWriter != null) { apWriter.Dispose(); apWriter = null; }
                if (apBuilder != null) { apBuilder.Dispose(); apBuilder = null; }
                if (sigImg != null) { sigImg.Dispose(); sigImg = null; }
                if (apObj != null) { apObj.Dispose(); apObj = null; }
                if (widgetAnnot != null) { widgetAnnot.Dispose(); widgetAnnot = null; }
                if (a != null) { a.Dispose(); a = null; }
                if (objSign != null) { objSign.Dispose(); objSign = null; }

                sigField = null;
                apElement = null;
                page = null;

            }

            return (fname);
        }

        public byte[] GetProviderSignImage(Field field, ref PDFDoc FormPdfDoc, Int16 Pageno, string _messageBoxCaption)
        {
         System.Drawing.Bitmap ProviderSignatureBmp = null;

         try
         {
             bool ImageFound = false;

             if (field.IsAnnot())
             {
                 Page page = FormPdfDoc.GetPage(Pageno);
                 string widgetName = null;
                 Annot annot = null;

                 try
                 {
                     int num_annots = page.GetNumAnnots();

                     widgetName = field.GetName();

                     for (int i = 0; i < num_annots; ++i)
                     {
                         annot = page.GetAnnot(i);

                         if (annot.IsValid() == false) continue;
                         Widget widgetAnnot = new Widget(annot);
                         Field WidgetField = widgetAnnot.GetField();

                         if (WidgetField != null)
                         {
                             if (widgetName == WidgetField.GetName())
                             {
                                 ImageFound = true;

                                 if (widgetAnnot != null)
                                 {
                                     widgetAnnot.Dispose();
                                     widgetAnnot = null;
                                 }
                                 break;
                             }
                             WidgetField = null;
                         }


                         if (widgetAnnot != null)
                         {
                             widgetAnnot.Dispose();
                             widgetAnnot = null;
                         }
                     }
                 }
                 catch
                 {
                 }
                 finally
                 {
                     page = null;
                     widgetName = null;

                     if (annot != null)
                     {
                         annot.Dispose();
                         annot = null;
                     }
                 }

                 Annot ButtonAnnot = new Annot(field.GetSDFObj());

                 if (ImageFound)
                 {
                     Obj app_stm = ButtonAnnot.GetAppearance();
                     if (app_stm != null)
                     {

                         ElementReader reader = new ElementReader();
                         Element element;
                         reader.Begin(app_stm);
                         while ((element = reader.Next()) != null)  // Read page contents
                         {
                             if (element.GetType() == Element.Type.e_image)
                             {

                                 ProviderSignatureBmp = element.GetBitmap();
                                 break;
                             }
                         }
                         reader.End();
                         reader.Dispose();
                         reader = null;
                         element = null;
                         app_stm.Dispose();
                         app_stm = null;

                     }
                 }
                 if (ButtonAnnot != null)
                 {
                     ButtonAnnot.Dispose();
                     ButtonAnnot = null;
                 }

             }
         }

         catch (Exception ex)
         {
             MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }

         if (ProviderSignatureBmp == null) return null;
         else
         {
             ImageConverter imgByte = new ImageConverter();
             try
             {
                 byte[] ImgByteArr = (byte[])imgByte.ConvertTo(ProviderSignatureBmp, System.Type.GetType("System.Byte[]"));
                 return ImgByteArr;
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
             }

             imgByte = null;
             return null;
         }
     }

        public DataTable GetClaimsForPatient(Int64 _nPatientID, string _databaseconnectionstring)
     {
         gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
         gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
         DataTable dtClaims = null;
         try
         {
             oDB.Connect(false);
             oDBParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
             oDB.Retrive("BL_SELECT_ClaimsForPatient", oDBParameters, out dtClaims);
             oDB.Disconnect();

         }
         catch (gloDatabaseLayer.DBException dbEx)
         {
             dbEx.ERROR_Log(dbEx.ToString());
         }
         catch (Exception ex)
         {
             gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
         }

         finally
         {
             if (oDB != null) { oDB.Dispose(); }
         }
         return dtClaims;
     }

    }

}
