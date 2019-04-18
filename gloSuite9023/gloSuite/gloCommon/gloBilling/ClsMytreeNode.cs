using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;  
namespace gloBilling
{

    public class myTreeNode : TreeNode, IDisposable
    {

        private long mykey;
        private string m_NodeName;
        private object Result;
        private System.DateTime _OrderTime;
        private bool _IsFinished;
        private Int16 m_IsNarcotics;
        private Int64 m_ddID;

        private object _ReferralLetter;
        private string m_DrugName = "";
        private string m_Dosage = "";
        private string m_OnsetDate = "";
        private string m_DateResolved = "";
        private string m_DrugForm = "";

        private string m_HistoryType = "";
        //29-Aug-13 Ashish: Addition of nAgeMin, nAgeMax,
        //bIsSnomed and sGender columns for frmEducationAssociation
        private int n_AgeMinimum = 0;
        private int n_AgeMaximum = 0;
        private string sGender = "";

        private bool bIsSnomedCode = false;
        //30-Aug-13 Ashish: Added bIsProvider and bIsPatient for
        //judging the context

        private bool bIsProviderReferenceMaterial = false;
        private bool bIsPatientEducationMaterial = false;

        private string sSnomedID = "";
        //3-Sep-13 Ashish: Added bIsICD, bIsMedication, bIsDrugs, bIsLabs
        private bool bIsICD9 = false;
        private bool bIsMedication = false;
    //    private bool bIsDrugs = false;
        private bool bIsLabs = false;

        private string sTemplateName = string.Empty;
        //24-Sep-13 Ashish: Added dLabResultValueOne, dLabResultValueTwo
        //nOperator and nIsProviderAdvancedMaterial

        private decimal dLabResultValueOne = 0;
        private decimal dLabResultValueTwo = 0;
        private int nOperator = 0;

        private int nIsProviderAdvancedMaterial = 0;
        //29-Mar-13 Aniket: Addition of source column on the History screen
        private string m_HistorySource = "";
        //'added for ICD10 implementation
        private int _nICDRevision = 0;

        private string m_CPT = "";
        private Int64 m_nHistoryID;

        private Int64 m_nRowOrder;
        private string m_Route = "";
        private string m_Frequency = "";
        private string m_Duration = "";
        private string m_NDCCode = "";
        private string m_ConceptCode = "";
        private string m_DrugQtyQualifier = "";

        private string m_FaxReferralName = "";
        private string m_Referralletter = "";
        private string m_TemplateName = "";

        private object m_Template = null;

      //  private string m_ICD9CPTCode = "";

    //    private string m_ICD9CPTName = "";
        private ArrayList m_arrRefferalDetails = null;
        private string _faxTo = "";
        private string _faxName = "";

        private string _faxCoverPage = "";
        public myTreeNode()
            : base("")
        {
            mykey = 0;
        }

        public myTreeNode(string strname, long key)
            : base(strname)
        {
            mykey = key;
            m_NodeName = strname;
        }

        public myTreeNode(string strname, long key, int ICDRev)
            : base(strname)
        {
            mykey = key;
            m_NodeName = strname;
            _nICDRevision = ICDRev;
        }

        public myTreeNode(string strname, long key, DateTime dtPrescriptiondate)
            : base(strname)
        {
            base.Tag = dtPrescriptiondate;
            mykey = key;
        }

        public myTreeNode(string strname, long key, string Drugname)
            : base(strname)
        {
            base.Tag = Drugname;
            mykey = key;
        }

        public myTreeNode(string strname, long key, string Drugname, Int64 DDID)
            : base(strname)
        {
            base.Tag = Drugname;
            mykey = key;
            m_ddID = DDID;
        }

        public myTreeNode(string strname, long key, string Drugname, string strname1)
            : base(strname)
        {
            base.Tag = Drugname;
            m_NodeName = strname1;
            mykey = key;
        }

        public myTreeNode(string strname, long key, long ID)
            : base(strname)
        {
            base.Tag = ID;
            mykey = key;
        }

        public myTreeNode(string strname, long key, string Drugname, string Dosage, string DrugForm, string Route, string Frequency, string NDCCode, Int16 IsNarcotics, string Duration,
        Int64 ddID, string DrugQtyQualifier)
            : base(strname)
        {
            base.Tag = Drugname;
            mykey = key;
            m_DrugName = Drugname;
            m_Dosage = Dosage;
            m_DrugForm = DrugForm;
            //Denormalization
            m_Route = Route;
            m_Frequency = Frequency;
            m_NDCCode = NDCCode;
            m_IsNarcotics = IsNarcotics;
            m_Duration = Duration;
            m_ddID = ddID;
            m_DrugQtyQualifier = DrugQtyQualifier;
            //Denormalization
        }

        public long Key
        {
            get { return mykey; }
            set { mykey = value; }
        }

        public string Name
        {
            get { return base.Text; }
            set { base.Text = Name; }
        }


        public string FaxTo
        {
            get { return _faxTo; }
            set { _faxTo = value; }
        }

        public string FaxName
        {
            get { return _faxName; }
            set { _faxName = value; }
        }

        public string FaxCoverPage
        {
            get { return _faxCoverPage; }
            set { _faxCoverPage = value; }
        }


        public System.DateTime OrderTime
        {
            get { return _OrderTime; }
            set { _OrderTime = value; }
        }


        //' for Order Status (Finished / Not-Finished)
        public bool IsFinished
        {
            get { return _IsFinished; }
            set { _IsFinished = value; }
        }

        public object TemplateResult
        {
            get { return Result; }
            set { Result = value; }
        }

        public object ReferralLetter
        {
            get { return _ReferralLetter; }
            set { _ReferralLetter = value; }
        }

        public Int16 IsNarcotics
        {
            get { return m_IsNarcotics; }
            set { m_IsNarcotics = value; }
        }

        public Int64 DDID
        {
            get { return m_ddID; }
            set { m_ddID = value; }
        }

        public string FaxReferralName
        {
            get { return m_FaxReferralName; }
            set { m_FaxReferralName = value; }
        }

        public string FaxReferralLetter
        {
            get { return m_Referralletter; }
            set { m_Referralletter = value; }
        }

        public string DrugName
        {
            get { return m_DrugName; }
            set { m_DrugName = value; }
        }


        public string DrugForm
        {
            get { return m_DrugForm; }
            set { m_DrugForm = value; }
        }


        public string HistoryType
        {
            get { return m_HistoryType; }
            set { m_HistoryType = value; }
        }


        //29-Mar-13 Aniket: Addition of source column on the History screen
        public string HistorySource
        {
            get { return m_HistorySource; }
            set { m_HistorySource = value; }
        }

        //added for ICD10 implementation
        public int nICDRevision
        {
            get { return _nICDRevision; }
            set { _nICDRevision = value; }
        }



        public string CPT
        {
            get { return m_CPT; }
            set { m_CPT = value; }
        }


        public string Dosage
        {
            get { return m_Dosage; }
            set { m_Dosage = value; }
        }

        public Int64 nHistoryID
        {
            get { return m_nHistoryID; }
            set { m_nHistoryID = value; }
        }

        public Int64 nRowOrder
        {
            get { return m_nRowOrder; }
            set { m_nRowOrder = value; }
        }

        public string OnsetDate
        {
            get { return m_OnsetDate; }
            set { m_OnsetDate = value; }
        }

        public string DateResolved
        {
            get { return m_DateResolved; }
            set { m_DateResolved = value; }
        }

        public string NodeName
        {
            get { return m_NodeName; }
            set { m_NodeName = value; }
        }

        public string Route
        {
            get { return m_Route; }
            set { m_Route = value; }
        }

        public string Frequency
        {
            get { return m_Frequency; }
            set { m_Frequency = value; }
        }

        public string Duration
        {
            get { return m_Duration; }
            set { m_Duration = value; }
        }

        public string NDCCode
        {
            get { return m_NDCCode; }
            set { m_NDCCode = value; }
        }

        public string ConceptCode
        {
            get { return m_ConceptCode; }
            set { m_ConceptCode = value; }
        }

        public string DrugQtyQualifier
        {
            get { return m_DrugQtyQualifier; }
            set { m_DrugQtyQualifier = value; }
        }

        public object DMTemplate
        {
            get { return m_Template; }
            set { m_Template = value; }
        }

        public string DMTemplateName
        {
            get { return m_TemplateName; }
            set { m_TemplateName = value; }
        }

        public ArrayList arrRefferalDetails
        {
            get { return m_arrRefferalDetails; }
            set { m_arrRefferalDetails = value; }
        }

        public int MinimumAge
        {
            get { return this.n_AgeMinimum; }
            set { this.n_AgeMinimum = value; }
        }

        public int MaximumAge
        {
            get { return this.n_AgeMaximum; }
            set { this.n_AgeMaximum = value; }
        }

        public string Gender
        {
            get { return this.sGender; }
            set { this.sGender = value; }
        }

        public bool IsSnomedCode
        {
            get { return this.bIsSnomedCode; }
            set { this.bIsSnomedCode = value; }
        }

        public bool IsPatientEducationMaterial
        {
            get { return this.bIsPatientEducationMaterial; }
            set { this.bIsPatientEducationMaterial = value; }
        }

        public bool IsProviderReferenceMaterial
        {
            get { return this.bIsProviderReferenceMaterial; }
            set { this.bIsProviderReferenceMaterial = value; }
        }

        public string SnomedID
        {
            get { return this.sSnomedID; }
            set { this.sSnomedID = value; }
        }

        public bool IsICD9
        {
            get { return this.bIsICD9; }
            set { this.bIsICD9 = value; }
        }

        public bool IsMedication
        {
            get { return this.bIsMedication; }
            set { this.bIsMedication = value; }
        }

        public bool IsLabs
        {
            get { return this.bIsLabs; }
            set { this.bIsLabs = value; }
        }

        public string TemplateName
        {
            get { return this.sTemplateName; }
            set { this.sTemplateName = value; }
        }

        public decimal LabResultValueOne
        {
            get { return this.dLabResultValueOne; }
            set { this.dLabResultValueOne = value; }
        }

        public decimal LabResultValueTwo
        {
            get { return this.dLabResultValueTwo; }
            set { this.dLabResultValueTwo = value; }
        }

        public int LabResultOperator
        {
            get { return this.nOperator; }
            set { this.nOperator = value; }
        }

        public int IsProviderAdvanceMaterial
        {
            get { return this.nIsProviderAdvancedMaterial; }
            set { this.nIsProviderAdvancedMaterial = value; }
        }
        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    // TODO: dispose managed state (managed objects).

                    //Free String Variables
                    //m_NodeName = Nothing
                    //m_DrugName = Nothing
                    //m_Dosage = Nothing
                    //m_OnsetDate = Nothing
                    //m_DateResolved = Nothing
                    //m_DrugForm = Nothing
                    //m_HistoryType = Nothing
                    //m_HistorySource = Nothing
                    //m_CPT = Nothing
                    //m_Route = Nothing
                    //m_Frequency = Nothing
                    //m_Duration = Nothing
                    //m_NDCCode = Nothing
                    //m_ConceptCode = Nothing
                    //m_DrugQtyQualifier = Nothing
                    //m_FaxReferralName = Nothing
                    //m_Referralletter = Nothing
                    //m_TemplateName = Nothing
                    //m_ICD9CPTCode = Nothing
                    //m_ICD9CPTName = Nothing
                    //_faxTo = Nothing
                    //_faxName = Nothing
                    //_faxCoverPage = Nothing

                    //'Free Object Variables
                    //Result = Nothing
                    //_ReferralLetter = Nothing
                    //m_Template = Nothing

                    //'Free other variables
                    //m_arrRefferalDetails = Nothing
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
