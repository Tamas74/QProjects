using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace gloSnoMed
{
   
    public class myList
    {
        private Int64 itemindex;
        private Int32 _Rowno;
        private string itemDescription = string.Empty;
        private bool itemType;
        private System.Int64 itemId;
        private System.DateTime itemVisitDate;
        private bool _IsFinished;
        //'swaraj 20100612 - to store bSendTask value in SmartConfig Table
        private bool _SendTask;
        private string _HistoryCategory = string.Empty;
        private string _HistoryItem = string.Empty;
        private string _Group = string.Empty;
        //variable added by sagar to store the medical condition id in the property procedure
        private long _MedicalConditionID;
        private string _Reaction = string.Empty;
        private long _ColorCode;
        ///'''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110
        private System.DateTime _ResolvedDate;
        private string _RComment = string.Empty;
        ///'''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110
        //Private _strItemname As String = ""

        private string _sHxDrugName;
        private string _sHxDrugDosage = string.Empty;
        private string _sHxNDCCode = string.Empty;
        private string _Status;

        private string _DOEOAllergy = string.Empty;

        private string _ConceptId = string.Empty;
        private string _DescId = string.Empty;
        private string _SnowMadeID = string.Empty;
        private string _Description = string.Empty;
        //Added by kanchan on 20100526
        private string _ICD9 = string.Empty;
        //Added by kanchan on 20100828
        private string _RxNorm = string.Empty;

        private string _Code;
        private string _ParameterName;
        //'Private _Operater As gloStream.gloCMS.Supporting.[Operator]
        private string _Value = string.Empty;

        private string _DMSID = string.Empty;
        //sarika 20090211 DICOM
        private string _DICOMID = "";
        //--


        private object Result;
        //added by sarika 13th nov 07 -- for 1 fax to multiple recipients
        private long _ContactID = 0;
        private string _ContactPersonName = "";
        private string _ContactPersonFaxNo = "";
        //-------------------------------------
        private Int64 _DisclosureAssociationID;

        private string _DisclosureType;
        //For De-Normalization
        private string itemDrugName = "";
        private string itemDosage = "";

        private string itemDrugForm = "";
        private string itemRoute = string.Empty;
        private string itemFrequency = string.Empty;
        private string itemNDCCode = string.Empty;
        private Int16 itemIsNarcotic;
        private string itemDuration = string.Empty;
        private Int32 itemnmpid;
        private string itemDrugQtyQualifier = string.Empty;
        //For De-Normalization

        //sarika referral letter 20081125

        private string _ReferralLetterName = "";

        //
        private string _sNPI = "";
        private string _sEmail = "";

        private int _nFlagType = 0;

        public enum ControlType
        {
            None = 0,
            CheckBox = 1,
            Text = 2
        }


        public enum CategoryType
        {
            None = 0,
            General = 1,
            Hitory = 2,
            Physical_Examination = 3,
            Medical_Decision_Making = 4,
            HPI = 5,
            Management_option = 6,
            Labs = 7,
            X_Ray_Radiology = 8,
            Other_Diagonsis_Tests = 9,
            ROS = 10,
            DB_History = 11
        }


        //--
   //     private ControlType _ControlType;
     //   private CategoryType _CategoryType;
        private string _AssociatedItem = string.Empty;
        private string _AssociatedCategory = string.Empty;

        private string _AssociatedProperty = string.Empty;

      //  private Collection _MyCollection = new Collection();
        private enumOrderComment _OrderComment;
        private int _ICD9Count;
        private int _CPTCount;
        private int _ModCount;
        //Shubhangi

        public enum enumExamControlType
        {
            None = 0,
            GeneralMultiSystem = 1,
            Cardiovascular = 2,
            EarsNoseThroat = 3,
            Eye = 4,
            Genitourinary = 5,
            HemaLymphImmuno = 6,
            Musculoskeletal = 7,
            Neurological = 8,
            Psychiatric = 9,
            Respiratory = 10,
            Skin = 11,
            Pre97Guidelines = 12
        }


    //    private enumExamControlType _ExamControlType;
        private int _nStatus;
        private int _Immediacy;
        private string _Provider;
        private string _Location;
        private DateTime _LastModified;
        private string _Comments;

        private string _ExamID;


        public enum enumOrderComment
        {
            //' SUDHIR 20090420 '' FOR SAVING ORDER TEST WITH TEMPLATE. ''
            None = 0,
            Assigned = 1,
            //' Template is Assigned for Test as comment
            UnAssigned = 2
            //' Template is Not Assigned for Test as comment
        }

        public myList(Int64 intindex, string strDescription)
        {
            itemindex = intindex;
            itemDescription = strDescription;
        }
        //'Sandip Darade 20090401
        //'to add name of item 
        public myList(string strDescription, string strItemName, Int64 intindex)
        {
            itemindex = intindex;
            itemDescription = strDescription;
            _ParameterName = strItemName;
        }

        public myList(Int64 UserID, string UserName, Int64 ProviderID)
        {
            itemindex = UserID;
            itemDescription = UserName;
            itemId = ProviderID;
        }

        public myList(Int64 intindex, string strDescription, bool blnType, System.Int64 intID)
        {
            itemindex = intindex;
            itemDescription = strDescription;
            itemType = blnType;
            itemId = intID;
        }

        //code added by sarika on 13th nov 07 --- for one fax to multiple recipients
        public myList(long ContactId, string strContactPersonName, string strContactFaxNo)
        {
            _ContactID = ContactId;
            _ContactPersonName = strContactPersonName;
            _ContactPersonFaxNo = strContactFaxNo;
        }
        //------------


        public myList()
        {
        }

        //For De-Normalization

        //Code Start-Added by kanchan on 20100828 for RxNorm
        public string RxNormID
        {
            get { return _RxNorm; }
            set { _RxNorm = value; }
        }
        //Code End-Added by kanchan on 20100828 for RxNorm

        //Code Start-Added by kanchan on 20100526 for ICD9
        public string ICD9
        {
            get { return _ICD9; }
            set { _ICD9 = value; }
        }
        //Code End-Added by kanchan on 20100526 for ICD9
        public string DrugName
        {
            get { return itemDrugName; }
            set { itemDrugName = value; }
        }
        public string Dosage
        {
            get { return itemDosage; }
            set { itemDosage = value; }
        }
        public string DrugForm
        {
            get { return itemDrugForm; }
            set { itemDrugForm = value; }
        }

        public string Route
        {
            get { return itemRoute; }
            set { itemRoute = value; }
        }

        public string Frequency
        {
            get { return itemFrequency; }
            set { itemFrequency = value; }
        }

        public string NDCCode
        {
            get { return itemNDCCode; }
            set { itemNDCCode = value; }
        }

        public Int16 IsNarcotic
        {
            get { return itemIsNarcotic; }
            set { itemIsNarcotic = value; }
        }

        public string Duration
        {
            get { return itemDuration; }
            set { itemDuration = value; }
        }

        public Int32 mpid
        {
            get { return itemnmpid; }
            set { itemnmpid = value; }
        }
      
        public string DrugQtyQualifier
        {
            get { return itemDrugQtyQualifier; }
            set { itemDrugQtyQualifier = value; }
        }
        //For De-Normalization

        public string ParameterName
        {
            get { return _ParameterName; }
            set { _ParameterName = value; }
        }

        //'Public Property Operater() As gloStream.gloCMS.Supporting.[Operator]
        //'    Get
        //'        Return _Operater
        //'    End Get
        //'    Set(ByVal Value As gloStream.gloCMS.Supporting.[Operator])
        //'        _Operater = Value
        //'    End Set
        //'End Property

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public object TemplateResult
        {
            get { return Result; }
            set { Result = value; }
        }

        public Int64 Index
        {
            get { return itemindex; }
            set { itemindex = value; }
        }

        public Int32 RowNo
        {
            get { return _Rowno; }
            set { _Rowno = value; }
        }


        //code added by sagar on 4 august 2007 to access the medical condition id in the history table
        public long MedicalConditionID
        {
            get { return _MedicalConditionID; }

            set { _MedicalConditionID = value; }
        }


        //for denormalization of History table
        public string HxDrugName
        {
            get { return _sHxDrugName; }
            set { _sHxDrugName = value; }
        }

        public string HxDrugDosage
        {
            get { return _sHxDrugDosage; }
            set { _sHxDrugDosage = value; }
        }

        public string HxNDCCode
        {
            get { return _sHxNDCCode; }
            set { _sHxNDCCode = value; }
        }

        public string DOEOAllergy
        {
            get { return _DOEOAllergy; }
            set { _DOEOAllergy = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        // added by chetan 
        public string ConceptId
        {
            get { return _ConceptId; }
            set { _ConceptId = value; }
        }


        public string DescId
        {
            get { return _DescId; }
            set { _DescId = value; }
        }

        public string SnowMadeID
        {
            get { return _SnowMadeID; }
            set { _SnowMadeID = value; }
        }

        public string SnoDescription
        {
            get { return _Description; }
            set { _Description = value; }
        }
        //for denormalization of History table

        public long ID
        {
            get { return itemId; }
            set { itemId = value; }
        }
        public bool Type
        {
            get { return itemType; }
            set { itemType = value; }
        }

        public string Description
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string HistoryCategory
        {
            get { return _HistoryCategory; }
            set { _HistoryCategory = value; }
        }

        public string HistoryItem
        {
            get { return _HistoryItem; }
            set { _HistoryItem = value; }
        }

        public string Group
        {
            get { return _Group; }
            set { _Group = value; }
        }

        //' Added By Mahesh 
        //' Used in Orders 
        public System.DateTime VisitDate
        {
            get { return itemVisitDate; }
            set { itemVisitDate = value; }
        }

        //' Added By Mahesh 
        //' Used in Orders to SetGet Order is Finished /NotFinised 

        ///'''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110
        public System.DateTime ResolvedDate
        {
            get { return _ResolvedDate; }
            set { _ResolvedDate = value; }
        }

        public string RComment
        {
            get { return _RComment; }
            set { _RComment = value; }
        }

        ///'''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110

        public bool IsFinished
        {
            get { return _IsFinished; }
            set { _IsFinished = value; }
        }
        //' swaraj 20100612 - To store bSendTask value in SmartConfig table
        public bool SendTask
        {
            get { return _SendTask; }
            set { _SendTask = value; }
        }

        public int nStatus
        {
            get { return _nStatus; }
            set { _nStatus = value; }
        }

        public int Immediacy
        {
            get { return _Immediacy; }
            set { _Immediacy = value; }
        }

        public string Provider
        {
            get { return _Provider; }
            set { _Provider = value; }
        }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public DateTime LastModified
        {
            get { return _LastModified; }
            set { _LastModified = value; }
        }


        public string ExamID
        {
            get { return _ExamID; }
            set { _ExamID = value; }
        }

        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        //'Added by Pramod
        #region "Commented By Pramod for DMSV2"
        //' Used to store DMSID in Radiology Flexgrid
        //Public Property DMSID() As Long
        //    Get
        //        Return _DMSID
        //    End Get
        //    Set(ByVal value As Long)
        //        _DMSID = value
        //    End Set
        //End Property
        #endregion
        public string DMSID
        {
            get { return _DMSID; }
            set { _DMSID = value; }
        }

        //sarika 20090211 DICOM
        public string DICOMID
        {
            get { return _DICOMID; }
            set { _DICOMID = value; }
        }
        //-----

        //added by sarika 13th nov 07 --- for one fax to multiple recipients
        public long ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }

        public string ContactPersonName
        {
            get { return _ContactPersonName; }
            set { _ContactPersonName = value; }
        }

        public string ContactPersonFaxNo
        {
            get { return _ContactPersonFaxNo; }
            set { _ContactPersonFaxNo = value; }
        }


        //-----------------


        public override string ToString()
        {
            return itemDescription;
        }

        public string Reaction
        {
            get { return _Reaction; }
            set { _Reaction = value; }
        }

        public long ColorCode
        {
            get { return _ColorCode; }
            set { _ColorCode = value; }
        }
        public Int64 DisclosureAssociationID
        {
            get { return _DisclosureAssociationID; }
            set { _DisclosureAssociationID = value; }
        }
        public string DisclosureType
        {
            get { return _DisclosureType; }
            set { _DisclosureType = value; }
        }

        //sarika Referral Letter 20081125
        public string ReferralLetterName
        {
            get { return _ReferralLetterName; }
            set { _ReferralLetterName = value; }
        }
        //---

        //Public Property ControlType() As [Enum]
        //    Get
        //        Return _ControlType
        //    End Get
        //    Set(ByVal value As [Enum])
        //        _ControlType = value
        //    End Set
        //End Property

        //Public Property CategoryType() As [Enum]
        //    Get
        //        Return _CategoryType
        //    End Get
        //    Set(ByVal value As [Enum])
        //        _CategoryType = value
        //    End Set
        //End Property
        public string AssociatedItem
        {
            get { return _AssociatedItem; }
            set { _AssociatedItem = value; }
        }
        public string AssociatedCategory
        {
            get { return _AssociatedCategory; }
            set { _AssociatedCategory = value; }
        }

        public string AssociatedProperty
        {
            get { return _AssociatedProperty; }
            set { _AssociatedProperty = value; }
        }

        //public Collection MyCollection
        //{
        //    get { return _MyCollection; }
        //    set { _MyCollection = value; }
        //}

        //sarika DM Denormalization 20090331
        private string _DMTemplateName = "";
        //Private _DMTemplate As Object = Nothing

        private object _DMTemplate = null;
        public string DMTemplateName
        {
            get { return _DMTemplateName; }
            set { _DMTemplateName = value; }
        }

        public object DMTemplate
        {

            get { return _DMTemplate; }
            set { _DMTemplate = value; }
        }

        public enumOrderComment OrderComment
        {
            get { return _OrderComment; }
            set { _OrderComment = value; }
        }

        public int ICD9Count
        {
            get { return _ICD9Count; }
            set { _ICD9Count = value; }
        }
        public int CPTCount
        {
            get { return _CPTCount; }
            set { _CPTCount = value; }
        }
        public int ModCount
        {
            get { return _ModCount; }
            set { _ModCount = value; }
        }
        //Shubhnagi
        //public Enum ExamControlType
        //{
        //    get { return _ExamControlType; }
        //    set { _ExamControlType = value; }
        //}
        //End

        public int FlagType
        {
            get { return _nFlagType; }
            set { _nFlagType = value; }
        }

        public string Email
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        public string NPI
        {
            get { return _sNPI; }
            set { _sNPI = value; }
        }




        #region "Contact Variable"
        private string _ContactName;
        private string _ContactFirstName;
        private string _ContactMiddleName;
        private string _ContactLastName;
        private string _ContactGender;
        private string _ContactDegree;
        private string _ContactAddressLine1;
        private string _ContactAddressLine2;
        private string _ContactCity;
        private string _ContactState;
        private string _ContactZip;
        private string _ContactPhone;
        private string _ContactFax;
        private string _ContactMobile;
        private string _ContactExternalCode;
        private string _ContactTemplateName;
        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }

        public string ContactFirstName
        {
            get { return _ContactFirstName; }
            set { _ContactFirstName = value; }
        }

        public string ContactMiddleName
        {
            get { return _ContactMiddleName; }
            set { _ContactMiddleName = value; }
        }

        public string ContactLastName
        {
            get { return _ContactLastName; }
            set { _ContactLastName = value; }
        }

        public string ContactGender
        {
            get { return _ContactGender; }
            set { _ContactGender = value; }
        }

        public string ContactDegree
        {
            get { return _ContactDegree; }
            set { _ContactDegree = value; }
        }

        public string ContactAddressLine1
        {
            get { return _ContactAddressLine1; }
            set { _ContactAddressLine1 = value; }
        }

        public string ContactAddressLine2
        {
            get { return _ContactAddressLine2; }
            set { _ContactAddressLine2 = value; }
        }

        public string ContactCity
        {
            get { return _ContactCity; }
            set { _ContactCity = value; }
        }

        public string ContactState
        {
            get { return _ContactState; }
            set { _ContactState = value; }
        }

        public string ContactZip
        {
            get { return _ContactZip; }
            set { _ContactZip = value; }
        }

        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; }
        }

        public string ContactFax
        {
            get { return _ContactFax; }
            set { _ContactFax = value; }
        }

        public string ContactMobile
        {
            get { return _ContactMobile; }
            set { _ContactMobile = value; }
        }

        public string ContactExternalCode
        {
            get { return _ContactExternalCode; }
            set { _ContactExternalCode = value; }
        }

        public string ContactTemplateName
        {
            get { return _ContactTemplateName; }
            set { _ContactTemplateName = value; }
        }
        #endregion

        //---
    }
}
