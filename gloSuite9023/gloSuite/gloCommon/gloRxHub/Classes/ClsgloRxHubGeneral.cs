using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gloRxHub
{
   public  class ClsgloRxHubGeneral
    {
       public static bool gblnIsRxhubStagingServer;
       public static string gstrRxHubLoginUser;////whenever we save MedHx we need to save who was the log in user info in the medication grid.
       public static bool gblnSend270UsingDEA = false;
       
     public long gnVisitID ;
     // public long gnPatientID;
       public static bool moreDrugAvailableFlag = false;
       public static string errorMessage = "";
       public static string DeniedReason = "";
    
       public static string ConnectionString = "";
       public static Int64 gnPatientId;
       public static Int64 gnInsuranceId;
       public static string gnstrApplicationFilePath = gloSettings.FolderSettings.AppTempFolderPath;

       public static void UpdateLog(string strLogMessage)
        {
            try
            {
                System.IO.StreamWriter objFile = new System.IO.StreamWriter(Application.StartupPath + "\\gloRxHub.log", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "   " + strLogMessage);
                objFile.Close();
                //SLR: objFile.dispose and then
                objFile.Dispose();
                objFile = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

       public static Int64 GetPrefixTransactionID(Int64 PatientID)
       {
           Int64 _Result = 0;
           string _result = "";
           DateTime _PatientDOB = DateTime.Now;
           DateTime _CurrentDate = DateTime.Now;
           DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

           string strID1 = "";
           string strID2 = "";
           string strID3 = "";

           TimeSpan oTS;

           object _internalresult = null;
           string _strSQL = "";
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);

           try
           {
               if (PatientID > 0)
               {
                   oDB.Connect(false);
                   _strSQL = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientID + "";
                   _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                   if (_internalresult != null)
                   {
                       if (_internalresult.ToString() != null)
                       {
                           if (_internalresult.GetType() != typeof(System.DBNull))
                           {
                               if (_internalresult.ToString() != "")
                               {
                                   _PatientDOB = Convert.ToDateTime(_internalresult);
                               }
                           }
                       }
                   }
                   oDB.Disconnect();
               }

               _result = "";

               //oTS = new TimeSpan(); //SLR: new is not needed
               oTS = _CurrentDate.Subtract(_BaseDate);
               strID1 = oTS.Days.ToString().Replace("-", "");

               //oTS = new TimeSpan(); //SLR: new is not needed
               oTS = _CurrentDate.Subtract(_CurrentDate.Date);
               strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

               //oTS = new TimeSpan(); //SLR: new is not needed
               oTS = _PatientDOB.Subtract(_BaseDate);
               strID3 = oTS.Days.ToString().Replace("-", "");

               _result = strID1 + strID2 + strID3;

               _Result = Convert.ToInt64(_result);
           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
               return 0;
           }
           finally
           {               
               _internalresult = null;
               //SLR: odb.dispose
               if (oDB != null)
               {
                   oDB.Disconnect();
                   oDB.Dispose();
               }
           }
           return _Result;
       }
       public static string getRejecttionDescription(string RejectionCode, string LoopName, string SegmentName)
       {
           string _sRejectionDescription = "";
           try
           {
               #region "AAA Information Source level Request Validation (HL)"

               if (LoopName == "Reciever" && SegmentName == "HL")
               {
                   switch (RejectionCode)
                   {
                       case "04":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Authorized Quantity Exceeded";
                           break;
                       case "41":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Authorization/Access Restrictions";
                           break;
                       case "42":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Unable to Respond at Current Time";
                           break;
                       case "79":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid Participant Identification";
                           break;
                       case "80":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": No Response received — Transaction Terminated";
                           break;
                       case "T4":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Payer Name or Identifier Missing";
                           break;

                   }
               }
               #endregion "AAA Information Source level Request Validation (HL)"

               #region "AAA Information Receiver Request Validation (NM1)"

               else if (LoopName == "Reciever" && SegmentName == "NM1")
               {
                   switch (RejectionCode)
                   {
                       case "15":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Required application data missing";
                           break;
                       case "41":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Authorization/Access Restrictions";
                           break;
                       case "43":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Identification (Surescripts recommends this for NPI error.)";
                           break;
                       case "44":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Name";
                           break;
                       case "45":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Specialty";
                           break;
                       case "46":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Phone Number";
                           break;
                       case "47":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider State";
                           break;
                       case "48":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Referring Provider Identification Number";
                           break;
                       case "50":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Provider Ineligible for Inquiries";
                           break;
                       case "51":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Provider Not on File";
                           break;
                       case "79":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid Participant Identification";
                           break;
                       case "97":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid or Missing Provider Address";
                           break;
                       case "T4":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Payer Name or Identifier Missing";
                           break;

                   }
               }
               #endregion "AAA Information Receiver Request Validation (NM1)"

               #region "AAA Subscriber Request Validation (NM1)"

               else if (LoopName == "Subscriber" && SegmentName == "NM1")
               {
                   switch (RejectionCode)
                   {
                       case "15":
                           //*At Surescripts — Not enough infomiation for Surescripts to identify patient.
                           //*At PBM — PBM wants more info than what was supplied.
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Required application data missing";
                           break;
                       case "35":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Out of Network";
                           break;
                       case "42":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Unable to Respond at Current Time";
                           break;
                       case "43":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Identification";
                           break;
                       case "45":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Specialty";
                           break;
                       case "47":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider State";
                           break;
                       case "48":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Referring Provider Identification Number";
                           break;
                       case "49":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Provider is Not Primary Care Physician";
                           break;
                       case "51":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Provider Not on File";
                           break;
                       case "52":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Service Dates Not Within Provider Plan Enrollment";
                           break;
                       case "56":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inappropriate Date";
                           break;
                       case "57":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Date(s) of Service";
                           break;
                       case "58":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Date-of-Birth";
                           break;
                       case "60":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Birth Follows Date(s) of Service";
                           break;
                       case "61":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Death Precedes Date(s) of Service";
                           break;
                       case "62":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Service Not Within Allowable Inquiry Period";
                           break;
                       case "63":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Service in Future";
                           break;
                       case "71":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Patient Birth Date Does Not Match That for the Patient on the Database";
                           break;
                       case "72":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Subscriber/Insured ID";
                           break;
                       case "73":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Subscriber/Insured Name";
                           break;
                       case "74":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Subscriber/Insured Gender Code";
                           break;
                       case "75":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Subscriber/Insured Not Found";
                           break;
                       case "76":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Duplicate Subscriber/Insured ID Number";
                           break;
                       case "78":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Subscriber/Insured Not in Group/Plan Identified";
                           break;

                   }
               }
               #endregion "AAA Subscriber Request Validation (NM1)"

               #region "AAA Subscriber Request Validation (EB)"
               else if (LoopName == "Subscriber" && SegmentName == "EB")
               {
                   switch (RejectionCode)
                   {
                       case "15":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Required application data missing";
                           break;
                       case "33":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Input Errors";
                           break;
                       case "52":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Service Dates Not Within Provider Plan Enrollment";
                           break;
                       case "54":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inappropriate Product/Service ID Qualifier";
                           break;
                       case "55":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inappropriate Product/Service ID";
                           break;
                       case "56":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inappropriate Date";
                           break;
                       case "57":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Date(s) of Service";
                           break;
                       case "60":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Birth Follows Date(s) of Service";
                           break;
                       case "61":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Death Precedes Date(s) of Service";
                           break;
                       case "62":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Service Not Within Allowable Inquiry Period";
                           break;
                       case "63":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Service in Future";
                           break;
                       case "69":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inconsistent with Patient’s Age";
                           break;
                       case "70":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inconsistent with Patient’s Gender";
                           break;
                   }
               }
               #endregion "AAA Subscriber Request Validation (EB)"

               #region "All combined"
               else if (LoopName == "" && SegmentName == "")
               {
                   switch (RejectionCode)
                   {
                       //"AAA Information Source level Request Validation (HL)"
                       case "04":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Authorized Quantity Exceeded";
                           break;
                       case "41":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Authorization/Access Restrictions";
                           break;
                       case "42":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Unable to Respond at Current Time";
                           break;
                       case "79":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid Participant Identification";
                           break;
                       case "80":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": No Response received — Transaction Terminated";
                           break;
                       case "T4":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Payer Name or Identifier Missing";
                           break;

                       //"AAA Information Receiver Request Validation (NM1)"
                       case "15":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Required application data missing";
                           break;
                       //case "41":
                       //    _sRejectionDescription = "Authorization/Access Restrictions";
                       //    break;
                       case "43":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Identification (Surescripts recommends this for NPI error.)";
                           break;
                       case "44":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Name";
                           break;
                       case "45":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Specialty";
                           break;
                       case "46":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider Phone Number";
                           break;
                       case "47":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Provider State";
                           break;
                       case "48":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Referring Provider Identification Number";
                           break;
                       case "50":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Provider Ineligible for Inquiries";
                           break;
                       case "51":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Provider Not on File";
                           break;
                       //case "79":
                       //    _sRejectionDescription = "Invalid Participant Identification";
                       //    break;
                       case "97":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid or Missing Provider Address";
                           break;
                       //case "T4":
                       //    _sRejectionDescription = "Payer Name or Identifier Missing";
                       //    break;

                       // #endregion "AAA Subscriber Request Validation (NM1)"
                       //case "15":
                       //    //*At Surescripts — Not enough infomiation for Surescripts to identify patient.
                       //    //*At PBM — PBM wants more info than what was supplied.
                       //    _sRejectionDescription = "Required application data missing";
                       //    break;
                       case "35":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Out of Network";
                           break;
                       //case "42":
                       //    _sRejectionDescription = "Unable to Respond at Current Time";
                       //    break;
                       //case "43":
                       //    _sRejectionDescription = "Invalid/Missing Provider Identification";
                       //    break;
                       //case "45":
                       //    _sRejectionDescription = "Invalid/Missing Provider Specialty";
                       //    break;
                       //case "47":
                       //    _sRejectionDescription = "Invalid/Missing Provider State";
                       //    break;
                       //case "48":
                       //    _sRejectionDescription = "Invalid/Missing Referring Provider Identification Number";
                       //    break;
                       case "49":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Provider is Not Primary Care Physician";
                           break;
                       //case "51":
                       //    _sRejectionDescription = "Provider Not on File";
                       //    break;
                       case "52":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Service Dates Not Within Provider Plan Enrollment";
                           break;
                       case "56":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inappropriate Date";
                           break;
                       case "57":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Date(s) of Service";
                           break;
                       case "58":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Date-of-Birth";
                           break;
                       case "60":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Birth Follows Date(s) of Service";
                           break;
                       case "61":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Death Precedes Date(s) of Service";
                           break;
                       case "62":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Service Not Within Allowable Inquiry Period";
                           break;
                       case "63":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Date of Service in Future";
                           break;
                       case "71":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Patient Birth Date Does Not Match That for the Patient on the Database";
                           break;
                       case "72":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Subscriber/Insured ID";
                           break;
                       case "73":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Subscriber/Insured Name";
                           break;
                       case "74":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Invalid/Missing Subscriber/Insured Gender Code";
                           break;
                       case "75":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Subscriber/Insured Not Found";
                           break;
                       case "76":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Duplicate Subscriber/Insured ID Number";
                           break;
                       case "78":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Subscriber/Insured Not in Group/Plan Identified";
                           break;

                       //"AAA Subscriber Request Validation (EB)"
                       //case "15":
                       //    _sRejectionDescription = "Required application data missing";
                       //    break;
                       case "33":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Input Errors";
                           break;
                       //case "52":
                       //    _sRejectionDescription = "Service Dates Not Within Provider Plan Enrollment";
                       //    break;
                       case "54":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inappropriate Product/Service ID Qualifier";
                           break;
                       case "55":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inappropriate Product/Service ID";
                           break;
                       //case "56":
                       //    _sRejectionDescription = "Inappropriate Date";
                       //    break;
                       //case "57":
                       //    _sRejectionDescription = "Invalid/Missing Date(s) of Service";
                       //    break;
                       //case "60":
                       //    _sRejectionDescription = "Date of Birth Follows Date(s) of Service";
                       //    break;
                       //case "61":
                       //    _sRejectionDescription = "Date of Death Precedes Date(s) of Service";
                       //    break;
                       //case "62":
                       //    _sRejectionDescription = "Date of Service Not Within Allowable Inquiry Period";
                       //    break;
                       //case "63":
                       //    _sRejectionDescription = "Date of Service in Future";
                       //break;
                       case "69":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inconsistent with Patient’s Age";
                           break;
                       case "70":
                           _sRejectionDescription = "Error Code AAA" + RejectionCode + ": Inconsistent with Patient’s Gender";
                           break;
                   }
               }

               #endregion "All Combined"

               return _sRejectionDescription;

           }
           catch (Exception ex)
           {
               //MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
               return "";
           }
           finally
           {

           }
       }

      }    

}
