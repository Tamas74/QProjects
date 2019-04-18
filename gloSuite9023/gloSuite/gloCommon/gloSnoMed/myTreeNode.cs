using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace gloSnoMed
{
    public class myTreeNode : TreeNode
    {
        
            private long mykey;
            private string m_NodeName;
            private object Result;
            private System.DateTime _OrderTime;
            private bool _IsFinished;
            private Int16 m_IsNarcotics;
            private Int32 m_mpid;
            private int _nICDRevision=9;       
            private string m_DrugName = "";
            private string m_Dosage = "";
            private string m_DrugForm = "";
            private string m_Route = "";
            private string m_Frequency = "";
            private string m_Duration = "";
            private string m_NDCCode = "";
            private string m_DrugQtyQualifier = "";
        
            private string m_FaxReferralName = "";

            private string m_Referralletter = "";

            private string m_TemplateName = "";
            private object m_Template = null;

            private string _ReasonConceptCode = "";
            private string  _ReasonConceptDesc = null;

            //private string m_ICD9CPTCode = "";
            //private string m_ICD9CPTName = "";

            private string _ConceptID = "";
            private string _DescriptionID = "";
            private string _SnoMedID = "";
            private string _RxNormID = "";
            private string _NDCCode = "";
            private string _ICD9 = "";
            private string _CPT = "";
            private string _HistoryType = "";
            private string _Comments = "";
            //private string _ICD9 = "";

            private string m_AllergyClassID = "";//added for 2015 certification
            private string m_CQMId= "";
                 private string m_CQMDesc = "";
                 private string _sProcStatus = "";

           
        
        public string CQMId
            {
                get { return m_CQMId ; }
                set { m_CQMId = value; }
            }
            public string CQMDesc
            {
                get { return m_CQMDesc  ; }
                set { m_CQMDesc = value; }
            }


            public string AllergyClassID
            {
                get { return m_AllergyClassID ; }
                set { m_AllergyClassID = value; }
            }
            public int nICDRevision
            {
                get { return _nICDRevision; }
                set { _nICDRevision = value; }
            }  
        
            public string ConceptID
            {
                get { return _ConceptID; }
                set { _ConceptID = value; }
            }
            public string ICD9
            {
                get { return _ICD9; }
                set { _ICD9 = value; }
            }
            public string CPT
            {
                get { return _CPT; }
                set { _CPT = value; }
            }
            public string HistoryType
            {
                get { return _HistoryType; }
                set { _HistoryType = value; }
            }
            public string Comments
            {
                get { return _Comments; }
                set { _Comments = value; }
            }

            public string DescriptionID
            {
                get { return _DescriptionID; }
                set { _DescriptionID = value; }
            }
            

            public string SnoMedID
            {
                get { return _SnoMedID; }
                set { _SnoMedID = value; }
            }
            public string RxNormID
            {
                get { return _RxNormID; }
                set { _RxNormID = value; }
            }
            public string NDCCod
            {
                get { return _NDCCode; }
                set { _NDCCode = value; }
            }

            public string LoincCode
            {
                get ;
               
                set ;
                
            }
            public string LoincDescription
            {
                get;

                set;

            }
            //public string ICD9
            //{
            //    get { return _ICD9; }
            //    set { _ICD9 = value; }
            //}

            public string sProcStatus
            {
                get { return _sProcStatus ; }
                set { _sProcStatus  = value; }
            }

            private ArrayList m_arrRefferalDetails = null;


            public myTreeNode()
                : base("")
            {
                mykey = 0;
            }

            public  myTreeNode(string strname, long key): base(strname)
            {
                mykey = key;
                m_NodeName = strname;
            }
            public myTreeNode(string strname, long key, DateTime dtPrescriptiondate): base(strname)
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
            public myTreeNode(string strname, long key, string Drugname, Int32 mpid): base(strname)
            {
                base.Tag = Drugname;
                mykey = key;
                m_mpid = mpid;
            }
            public myTreeNode(string strname, long key, string Drugname, string strname1): base(strname)
            {
                base.Tag = Drugname;
                m_NodeName = strname1;
                mykey = key;
            }
            public myTreeNode(string strname, long key, long ID): base(strname)
            {
                base.Tag = ID;
                mykey = key;
            }
            //For De-Normalization -20090127
            public myTreeNode(string strname, long key, string Drugname, string Dosage, string DrugForm, string Route, string Frequency, string NDCCode, Int16 IsNarcotics, string Duration,Int32 mpid, string DrugQtyQualifier): base(strname)
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
                m_mpid = mpid;
                //Denormalization
                m_DrugQtyQualifier = DrugQtyQualifier;
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

            //Public Property NodeName() As String
            // Get
            // Return m_NodeName
            // End Get
            // Set(ByVal Value As String)
            // m_NodeName = Value
            // End Set
            //End Property

            //' By Mahesh 
            //' for OrderDate
            public System.DateTime OrderTime
            {
                get { return _OrderTime; }
                set { _OrderTime = value; }
            }

            //' By Mahesh 
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
            public Int16 IsNarcotics
            {
                get { return m_IsNarcotics; }
                set { m_IsNarcotics = value; }
            }

            public Int32 mpid
            {
                get { return m_mpid; }
                set { m_mpid= value; }
            }

            //sarika Fax from Referrals 20081121

            public string FaxReferralName
            {
                get { return m_FaxReferralName; }
                set { m_FaxReferralName = value; }
            }

            //---

            //sarika Referral Letter 20081125

            public string FaxReferralLetter
            {
                get { return m_Referralletter; }
                set { m_Referralletter = value; }
            }

            //---
            //For de-Normalization - 20090127
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

            public string Dosage
            {
                get { return m_Dosage; }
                set { m_Dosage = value; }
            }
            //For de-Normalization
            public string NodeName
            {
                get { return m_NodeName; }
                set { m_NodeName = value; }
            }
            //For De-Normalization
            //// drugProvider form - Suraj 20090127
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

            public string DrugQtyQualifier
            {
                get { return m_DrugQtyQualifier; }
                set { m_DrugQtyQualifier = value; }
            }

            //sarika DM Denormalization
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
            //---
            public string ReasonConceptCode
            {
                get { return _ReasonConceptCode; }
                set { _ReasonConceptCode = value; }
            }
            public string ReasonConceptDesc
            {
                get { return _ReasonConceptDesc; }
                set { _ReasonConceptDesc = value; }
            }

            public string CVXCode
            {
                get;

                set;
            }

            public string CVXDesc
            {
                get;

                set;
            }


            ////
    //     Public Property ReasonConceptCode() As String
    //    Get
    //        Return m_ReasonConceptID
    //    End Get
    //    Set(ByVal value As String)
    //        m_ReasonConceptID = value
    //    End Set
    //End Property
    //Public Property ReasonConceptDesc() As String
    //    Get
    //        Return m_ReasonConceptDesc
    //    End Get
    //    Set(ByVal value As String)
    //        m_ReasonConceptDesc = value
    //    End Set
    //End Property


        }
    }

