using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace gloGallery
{
    public class myTreeNode : TreeNode
    {
        private long mykey;
        private string m_NodeName;
        private object Result;
        private System.DateTime _OrderTime;
        private bool _IsFinished;

        private Int16 m_IsNarcotics;
        //To get the Formulary status of the drug for the selected drug

        private string m_FormularyStatus;
        //To get the RxType of the drug for the selected drug

        private string m_DrugRxType;
        
        //'used for drug provider association
        private long m_SIGId;
        //' SUDHIR 20090513 ''
        private Int64 _ID;
        private string _Code;
        //GLO2010-0005444'sIndicator
        private string _sIndicator;

        //' GLO2011-0010684
        //' Variable used to hold the ROS Comments

        private string _sComment;
        private string _Description;
        private string _Unit;

        private string _ConceptID;
        //'For De-Normalization  -20090127
        //Private m_DrugName As String = ""
        //Private m_Dosage As String = ""
        //Private m_DrugForm As String = ""
        //'For De-Normalization

        //// drugProvider form - Suraj 20090127
        private string m_DrugForm = "";
        private string m_Route = "";
        private string m_Frequency = "";
        private string m_Duration = "";
        private string m_NDCCode = "";
        private string m_DrugQtyQualifier = "";
        ////

        //'sarika Fax from Referrals 20081121
        //Private m_FaxReferralName As String = ""
        //'---

        //'sarika Referral Letter 20081125
        //Private m_Referralletter As String = ""
        //'---------


        //'sarika DM Denormalization
        //Private m_TemplateName As String = ""
        //Private m_Template As Object = Nothing


        //'ICD9, CPT 
        //Private m_ICD9CPTCode As String = ""
        //Private m_ICD9CPTName As String = ""

        //'---sarika DM Denormalization-------------------
        //Private m_arrRefferalDetails As ArrayList = Nothing


        public myTreeNode(): base("")
        {
            mykey = 0;
            m_SIGId = 0;
            ///''used for drugProvider association
        }

        //Sub New(ByVal strname As String, ByVal key As Int64)
        //    MyBase.New(strname)
        //    mykey = key
        //End Sub
        public myTreeNode(string strname, long key): base(strname)
        {
            mykey = key;
            m_NodeName = strname;
        }

        public myTreeNode(string strname, long key, DateTime dtPrescriptiondate): base(strname)
        {
            base.Tag = dtPrescriptiondate;
            mykey = key;
        }

        public myTreeNode(string strname, long key, string Drugname): base(strname)
        {
            base.Tag = Drugname;
            mykey = key;
        }

        //'used for DrugProviderassociation, 
        public myTreeNode(string strname, long key, string Drugname, string NDCCODE, long SIGID): base(strname)
        {
            base.Tag = Drugname;
            mykey = key;
            m_NDCCode = NDCCODE;
            m_SIGId = SIGID;
        }

        public myTreeNode(string strname, long key, long ID): base(strname)
        {
            base.Tag = ID;
            mykey = key;
        }

        //'for drugproviderasscoiation
        public long SIGID
        {
            get { return m_SIGId; }
            set { m_SIGId = value; }
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


        //For de-Normalization
        public string NodeName
        {
            get { return m_NodeName; }
            set { m_NodeName = value; }
        }

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

        //Formulary Status
        public string FormularyStatus
        {
            get { return m_FormularyStatus; }
            set { m_FormularyStatus = value; }
        }

        //Drug RxType
        public string DrugRxType
        {
            get { return m_DrugRxType; }
            set { m_DrugRxType = value; }
        }

        public Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string ConceptID
        {
            get { return _ConceptID; }
            set { _ConceptID = value; }
        }
        public string Indicator
        {
            //GLO2010-0005444:'sIndicator
            get { return _sIndicator; }
            set { _sIndicator = value; }
        }

        //' GLO2011-0010684
        //' Property used to hold the ROS Comments
        public string Comments
        {
            get { return _sComment; }
            set { _sComment = value; }
        }

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }

        //For De-Normalization
        //// drugProvider form - Suraj 20090127
        public string DrugForm
        {
            get { return m_DrugForm; }
            set { m_DrugForm = value; }
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

        public string DrugQtyQualifier
        {
            get { return m_DrugQtyQualifier; }
            set { m_DrugQtyQualifier = value; }
        }

        public DateTime CPTDeactivationDate { get; set; }
        
        public DateTime CPTActivationDate { get; set; }

    }
}
