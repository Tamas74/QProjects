using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;

namespace gloCommunity.Classes
{
  
    public class myList
    {
        private Int64 itemindex;
        private string itemDescription = string.Empty;
        private bool itemType;
        private System.Int64 itemId;
        private System.DateTime itemVisitDate;
        private bool _IsFinished;
        private string _HistoryCategory = string.Empty;
        private string _HistoryItem = string.Empty;
        private string _Group = string.Empty;
        //variable added by sagar to store the medical condition id in the property procedure
        private long _MedicalConditionID;
        private string _Reaction = string.Empty;
        private long _ColorCode;
        //'Added on 20101004
        private string _DOEOAllergy = string.Empty;
        private string _ConceptId = string.Empty;
        private string _DescId = string.Empty;
        private string _SnowMadeID = string.Empty;
        private string _Description = string.Empty;
        private string _ICD9 = string.Empty;
        private string _RxNorm = string.Empty;
        private string _RComment = string.Empty;
        //'
        //Private _strItemname As String = ""

        //'Variables added in History table sDrugName/sDosage/sNDCCode/nddid
        private string _sHxDrugName = string.Empty;
        private string _sHxDrugDosage = string.Empty;
        private string _sHxNDCCode = string.Empty;

        private long _nHxddid;


        private string _Code = string.Empty;
        private string _ParameterName = string.Empty;
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

        private string _DisclosureType = string.Empty;
        //For De-Normalization
        private string itemDrugName = "";
        private string itemDosage = "";

        private string itemDrugForm = "";
        private string itemRoute = string.Empty;
        private string itemFrequency = string.Empty;
        private string itemNDCCode = string.Empty;
        private Int16 itemIsNarcotic;
        private string itemDuration = string.Empty;
        private Int64 itemnDDid;
        private Int32 itemmpid;
        private string itemDrugQtyQualifier = string.Empty;
        private bool _ItemChecked = false;
        //' sUserID
        private string _sUserID = string.Empty;

        //For De-Normalization

        //sarika referral letter 20081125

        private string _ReferralLetterName = "";

        


       

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

       
        private gloCommunity.Classes.clsGeneral.ControlType _ControlType;
        private gloCommunity.Classes.clsGeneral.CategoryType _CategoryType;
        private string _AssociatedItem = string.Empty;
        private string _AssociatedCategory = string.Empty;

        private string _AssociatedProperty = string.Empty;

        ////private Collection _MyCollection = new Collection();
        private enumOrderComment _OrderComment;
        private int _ICD9Count;
        private int _CPTCount;
        private int _ModCount;
        //Shubhangi

    //    private enumExamControlType _ExamControlType;
        private System.DateTime _ResolvedDate;
        private string _Status = string.Empty;
        //' chetan assign it to empty on 28-oct-2010 
        private string _Comments = string.Empty;
        private int _Immediacy;
        private string _Provider = string.Empty;
        private string _Location = string.Empty;
        private DateTime _LastModified;
        private string _ExamID = string.Empty;
        //'Added Rahul for LOINC Code,TextComment on 20101021
        private string _TextComment = string.Empty;
        private string _LOINC_Code = string.Empty;
        //'End

        private bool _SendTask;
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
        public myList(string strDescription, string strItemName, Int64 intindex, bool blnchecked = false)
        {
            itemindex = intindex;
            itemDescription = strDescription;
            _ParameterName = strItemName;
            ItemChecked = blnchecked;
        }


        //For De-Normalization
        public myList(Int64 intindex, string strDescription, string strDrugName, string strDosage, string strDrugForm, string strRoute, string strFrequency, string strNDCCode, Int16 nIsNarcotic, string strDuration,
        Int64 nDDid, string strDrugQtyQualifier, bool blnchecked = false)
        {
            itemindex = intindex;
            itemDescription = strDescription;
            //For De-Normalization
            itemDrugName = strDrugName;
            itemDosage = strDosage;
            itemDrugForm = strDrugForm;
            itemRoute = strRoute;
            itemFrequency = strFrequency;
            itemNDCCode = strNDCCode;
            itemIsNarcotic = nIsNarcotic;
            itemDuration = strDuration;
            itemnDDid = nDDid;
            itemDrugQtyQualifier = strDrugQtyQualifier;
           ItemChecked = blnchecked;
       }
        //For De-Normalization

        //By mahesh for Messages

        public myList(Int64 UserID, string UserName, Int64 ProviderID)
        {
            itemindex = UserID;
            itemDescription = UserName;
            itemId = ProviderID;
        }

        public myList(Int64 intindex, string strDescription, bool blnType, System.Int64 intID, string StrComment = "")
        {
            itemindex = intindex;
            itemDescription = strDescription;
            itemType = blnType;
            itemId = intID;
            Comments = StrComment;
        }

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
            get { return itemmpid; }
            set { itemmpid = value; }
        }
        public Int64 DDid
        {
            get { return itemnDDid; }
            set { itemnDDid = value; }
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

        public string UserID
        {
            get { return _sUserID; }
            set { _sUserID = value; }
        }

        public bool ItemChecked
        {
            get { return _ItemChecked; }
            set { _ItemChecked = value; }
        }

      

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

        public long HxDDID
        {
            get { return _nHxddid; }
            set { _nHxddid = value; }
        }

        //'Added on 20101004
        public string DOEOAllergy
        {
            get { return _DOEOAllergy; }
            set { _DOEOAllergy = value; }
        }

        public bool SendTask
        {
            get { return _SendTask; }
            set { _SendTask = value; }
        }

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

        public string RxNormID
        {
            get { return _RxNorm; }
            set { _RxNorm = value; }
        }

        public string ICD9
        {
            get { return _ICD9; }
            set { _ICD9 = value; }
        }
        //'

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
        public bool IsFinished
        {
            get { return _IsFinished; }
            set { _IsFinished = value; }
        }
       
      
        public string DMSID
        {
            get { return _DMSID; }
            set { _DMSID = value; }
        }

       
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

        public gloCommunity.Classes.clsGeneral.ControlType ControlType
        {
            get { return _ControlType; }
            set { _ControlType = value ; }
        }

        public gloCommunity.Classes.clsGeneral.CategoryType CategoryType
        {
            get { return _CategoryType; }
            set { _CategoryType = value; }
        }
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
       

        ///Problem List variables
        public System.DateTime ResolvedDate
        {
            get { return _ResolvedDate; }
            set { _ResolvedDate = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
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
        public string RComment
        {
            get { return _RComment; }
            set { _RComment = value; }
        }

        //'Added Rahul for LOINC Code & Text Comment on 20101021
        public string LoincCode
        {
            get { return _LOINC_Code; }
            set { _LOINC_Code = value; }
        }

        public string TextComment
        {
            get { return _TextComment; }
            set { _TextComment = value; }
        }
        //'End

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
            get
            {
                string functionReturnValue = null;
                return functionReturnValue;                
            }
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
