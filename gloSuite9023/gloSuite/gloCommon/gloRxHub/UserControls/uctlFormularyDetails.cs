using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data.SqlClient ;
using System.Data;  
using System.Text;
using System.Windows.Forms;

namespace gloRxHub
{
    public partial class uctlFormularyDetails : UserControl
    {
        ClsFormularyCheck oFormularyCheck;
        public uctlFormularyDetails(ClsFormularyCheck objFormularyCheck)
        {
            InitializeComponent();
            oFormularyCheck = objFormularyCheck;
            LoadFormularyData();
        }
        public void LoadFormularyData()
        {
             
            SqlConnection sqlconn = new SqlConnection();
            SqlCommand sqlComm = new SqlCommand();
            string strQuery = "";
            SqlDataAdapter sqlAdpt = new SqlDataAdapter();
            DataTable dt = new DataTable(); 
            try
            {
                string CoPayId = "";
                string CoverageId = "";
                string FormularyId = "";
                string AlternativeId = "";

                //fill formulary Status Details 
                sqlconn.Open();
                
                CoPayId = oFormularyCheck.RxH_271MasterObject.CopayId;
                CoverageId = oFormularyCheck.RxH_271MasterObject.CoverageId;
                FormularyId = oFormularyCheck.RxH_271MasterObject.FormularyListId;
                AlternativeId = oFormularyCheck.RxH_271MasterObject.AlternativeListId; 

                strQuery = "Select isnull(sServiceID1,'') as oFormularyCheck.NDCCode,isnull(sServiceIDAlternative,'') as AlternativeNDCCode ,dbo.RxH_GetDrugName(isnull(sServiceIDAlternative,'')) as AlternativeDrugName,isnull(sPreferenceLevel,'') as PreferenceLevel from RxH_Alternative_DTL where sAlternativeID = " + "'" + AlternativeId + "'" + " and sServiceID1='" + oFormularyCheck.NDCCode + "'";
                sqlComm = new SqlCommand(strQuery, sqlconn);

                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1FormularyAlternativeList.DataSource = dt;
                strQuery = "";
                strQuery = "Select sCoPayID,sCoPayListID,isnull(sFormularyStatus,'') as Formularystatus,dbo.RxH_GetFormularyDescription(isnull(sFormularyStatus,'')) as FormularyDescription, dbo.RxH_GetProductType(isnull(sProductType,'')) as ProductDescription,dbo.RxH_GetPharmacyType(isnull(sPharmacyType,'')) as PharmacyDescription,isnull(sOutOfPocketRangeStart,'') as OutOfPocketRangeStart,isnull(sOutOfPocketRangeEnd,'') as OutOfPocketRangeEnd,isnull(sOutOfPocketRangeEnd,'') as OutOfPocketRangeEnd,isnull(sFlatCoPayAmount,'') as FlatCoPayAmount,isnull(sPercentCoPayRate,'') as PercentCoPayRate,isnull(sFirstCoPayTerm,'') as FirstCoPayTerm,isnull(sMinimumCoPay,'') as MinimumCoPay,isnull(sMaximumCoPay,'') as MaximumCoPay,isnull(sDaysSupplyPerCoPay,'') as DaysSupplyPerCoPay,isnull(sCoPayTier,'') as CoPayTier,isnull(sMaximumCoPayTier,'') as MaximumCoPayTier from RxH_CoPay_SL_DTL where sCoPayID = " + "'" + CoPayId + "'" + " ";
                sqlComm = new SqlCommand(strQuery, sqlconn);
                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1CoPaySummaryLevel.DataSource = dt;

                //fill Coverage Age Limit Details 
                strQuery = "";
                strQuery = "Select sCoverageID, isnull(sServiceID1,'') as oFormularyCheck.NDCCode,isnull(sMinimumAge,'') as MinimumAge,isnull(sMinimumAgeQualifier,'') as MinimumAgeQualifier, isnull(sMaximumAge,'') as MaximumAge, isnull(sMaximumAgeQualifier,'') as MaximumAgeQualifier from RxH_Coverage_AL_DTL where sCoverageID = " + "'" + CoverageId + "'" + " and sServiceID1='" + oFormularyCheck.NDCCode + "'";
                sqlComm = new SqlCommand(strQuery, sqlconn);
                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1CoverageAgeLimit.DataSource = dt;


                //fill Coverage Gender Limit 
                strQuery = "";
                strQuery = "Select sCoverageID, isnull(sServiceID1,'') as oFormularyCheck.NDCCode, dbo.RxH_GetGender(isnull(sGender,'')) as Gender from RxH_Coverage_GL_DTL where sCoverageID = " + "'" + CoverageId + "'" + " and sServiceID1='" + oFormularyCheck.NDCCode + "'";
                sqlComm = new SqlCommand(strQuery, sqlconn);
                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1CoverageGenderLimit.DataSource = dt;


                //fill Coverage Quantity Limit Details 
                strQuery = "";
                strQuery = "Select sCoverageID, isnull(sServiceID1,'') as oFormularyCheck.NDCCode, isnull(sMaximumAmountQualifier,'') as MaximumAmountQualifier, isnull(sMaximumAmountTime,'') as MaximumAmountTime , isnull(sMaximumAmountStartDate,'') as MaximumAmountStartDate, isnull(sMaximumAmountEndDate,'') as MaximumAmountEndDate , isnull(sMaximumAmountUnits,'') as MaximumAmountUnits, isnull(sMaximumAmount,'') as MaximumAmount from RxH_Coverage_QL_DTL where sCoverageID = " + "'" + CoverageId + "'" + " and sServiceID1='" + oFormularyCheck.NDCCode + "'";
                sqlComm = new SqlCommand(strQuery, sqlconn);
                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1CoverageQuntityLimit.DataSource = dt;


                //fill Coverage Step Medication Details 
                strQuery = "";
                strQuery = "Select sCoverageID , isnull(sServiceID1,'') as oFormularyCheck.NDCCode, isnull(sServiceID3,'') as StepNDCCode,dbo.RxH_GetDrugName(isnull(sServiceID3,'')) as StepMedication ,isnull(sNoOfDrug,'') as NoofDrug, isnull(sStepOrder,'') as StepOrder, isnull(sDiagnosisCode,'') as DiagnosisCode from RxH_Coverage_SM_DTL where sCoverageID = " + "'" + CoverageId + "'" + " and sServiceID1='" + oFormularyCheck.NDCCode + "'";
                sqlComm = new SqlCommand(strQuery, sqlconn);
                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1CoverageStepMedication.DataSource = dt;


                //fill Coverage Exclusion Details 
                //strQuery = "";
                //strQuery = "Select sCoverageID , isnull(sServiceID1,'') as oFormularyCheck.NDCCode from RxH_Coverage_DE_DTL where sCoverageID = " + "'" + CoverageId + "'" + " and sServiceID1='" + oFormularyCheck.NDCCode + "'";
                //sqlComm = new SqlCommand(strQuery, sqlconn);
                //sqlAdpt = new SqlDataAdapter(sqlComm);
                //dt = new DataTable();
                //sqlAdpt.Fill(dt);
                //C1CoverageExclusion.DataSource = dt;


                //fill Coverage Resource Link Summary Level Details 
                strQuery = "";
                strQuery = "Select sCoverageID, isnull(sURL,'') as URL, isnull(sResourceLinkType,'') as ResourceLinkType from RxH_Coverage_RS_DTL where sCoverageID = " + "'" + CoverageId + "'" + "";
                sqlComm = new SqlCommand(strQuery, sqlconn);
                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1CoverageRLSummaryLevel.DataSource = dt;


                //fill Coverage Resource Link Drug specific Details 
                strQuery = "";
                strQuery = "Select sCoverageID , isnull(sServiceID1,'') as oFormularyCheck.NDCCode,isnull(sResourceLinkType,'') as ResourceLinkType,isnull(sURL,'') as URL from RxH_Coverage_RD_DTL where sCoverageID = " + "'" + CoverageId + "'" + " and sServiceID1='" + oFormularyCheck.NDCCode + "'";
                sqlComm = new SqlCommand(strQuery, sqlconn);
                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1CoverageRLDrugSpecification.DataSource = dt;


                //fill Coverage Text Message Details 
                strQuery = "";
                strQuery = "Select sCoverageID , isnull(sServiceID1,'') as oFormularyCheck.NDCCode,isnull(sMessageShort,'') AS MessageShort, isnull(sMessageLong,'') AS MessageLong, isnull(sCoverageID,'') AS CoverageListID from RxH_Coverage_TM_DTL where sCoverageID = " + "'" + CoverageId + "'" + " and sServiceID1='" + oFormularyCheck.NDCCode + "'";
                sqlComm = new SqlCommand(strQuery, sqlconn);
                sqlAdpt = new SqlDataAdapter(sqlComm);
                dt = new DataTable();
                sqlAdpt.Fill(dt);
                C1CoverageTextMsg.DataSource = dt; 


            }
            finally
            {

            }
        }
    }
}
