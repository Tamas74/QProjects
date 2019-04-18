using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;  
using System.Collections; 
namespace gloCommunity.Classes
{
    class myTreeNode:TreeNode 
    {
    
private long mykey;
private string m_NodeName;
private object Result;
private System.DateTime _OrderTime;
private bool _IsFinished;
private Int16  m_IsNarcotics;

private Int64 m_ddID;
private Int32 m_mpid;
//For De-Normalization  -20090127
private string m_DrugName = "";
private string m_Dosage = "";
private string m_DrugForm = "";
//For De-Normalization

//// drugProvider form - Suraj 20090127
private string m_Route = "";
private string m_Frequency = "";
private string m_Duration = "";
private string m_NDCCode = "";
private string m_DrugQtyQualifier = "";
////

//sarika Fax from Referrals 20081121
private string m_FaxReferralName = "";
//---

//sarika Referral Letter 20081125
private string m_Referralletter = "";
//---------


//sarika DM Denormalization
private string m_TemplateName = "";

private object m_Template = null;

//ICD9, CPT 
//private string m_ICD9CPTCode = "";

//private string m_ICD9CPTName = "";
//---sarika DM Denormalization-------------------
private string m_GenericName = "";
        
   private string m_Quantity = "";  
         private bool m_PracticeFavorites = false;  //  Generic Name - A
           private string m_BeersList = "";
           private bool m_IsAllergicDrug = false;
//Quantity - A (sAmount)
//Practice Favorites - (bIsClinicalDrug)
//Beers List - (nDrugType)
//Is Allergic Drug


private ArrayList m_arrRefferalDetails = null;

public myTreeNode() : base("")
{
	mykey = 0;
}

public myTreeNode(string strname, long key) : base(strname)
{
	mykey = key;
	m_NodeName = strname;
}
public myTreeNode(string strname, long key, DateTime dtPrescriptiondate) : base(strname)
{
	base.Tag = dtPrescriptiondate;
	mykey = key;
}
public myTreeNode(string strname, long key, string Drugname) : base(strname)
{
	base.Tag = Drugname;
	mykey = key;
}
public myTreeNode(string strname, long key, string Drugname, Int64 DDID) : base(strname)
{
	base.Tag = Drugname;
	mykey = key;
	m_ddID = DDID;
}
public myTreeNode(string strname, long key, string Drugname, string strname1) : base(strname)
{
	base.Tag = Drugname;
	m_NodeName = strname1;
	mykey = key;
}
public myTreeNode(string strname, long key, long ID) : base(strname)
{
	base.Tag = ID;
	mykey = key;
}
//For De-Normalization -20090127
public myTreeNode(string strname, long key, string Drugname, string Dosage, string DrugForm, string Route, string Frequency, string NDCCode, Int16  IsNarcotics, string Duration,
Int64 ddID, string DrugQtyQualifier) : base(strname)
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
public Int16   IsNarcotics 
{
	get { return m_IsNarcotics; }
	set { m_IsNarcotics = value; }
}
public Int64  DDID 
{
	get { return m_ddID; }
	set { m_ddID = value; }
}

public Int32 mpid
{
    get { return m_mpid; }
    set { m_mpid = value; }
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
//For de-Normalization  - 20090127
public string DrugName 
{
	get { return m_DrugName; }
	set { m_DrugName = value; }
}

public string DrugForm {
	get { return m_DrugForm; }
	set { m_DrugForm = value; }
}

public string Dosage {
	get { return m_Dosage; }
	set { m_Dosage = value; }
}
//For de-Normalization
public string NodeName {
	get { return m_NodeName; }
	set { m_NodeName = value; }
}
//For De-Normalization
//// drugProvider form - Suraj 20090127
public string Route {
	get { return m_Route; }
	set { m_Route = value; }
}

public string Frequency {
	get { return m_Frequency; }
	set { m_Frequency = value; }
}

public string Duration {
	get { return m_Duration; }
	set { m_Duration = value; }
}

public string NDCCode {
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
	get 
    { 
        return m_TemplateName; 
    }
	set 
    { 
        m_TemplateName = value; 
    }
}

public ArrayList arrRefferalDetails 
{
	get 
    { 
        return m_arrRefferalDetails; 
    }
	set 
    {
        m_arrRefferalDetails = value; 
    }
}

//private string m_GenericName = "";


public string GenericName
{
    get
    {
        return m_GenericName;
    }
    set
    {
        m_GenericName = value;
    }
}

//private string m_Quantity = "";


public string Quantity
{
    get
    {
        return m_Quantity;
    }
    set
    {
        m_Quantity = value;
    }
}


public bool PracticeFavorites
{
    get
    {
        return m_PracticeFavorites;
    }
    set
    {
        m_PracticeFavorites = value;
    }
}

public string BeersList
{
    get
    {
        return m_BeersList;
    }
    set
    {
        m_BeersList = value;
    }
}

public bool IsAllergicDrug 
{
    get
    {
        return m_IsAllergicDrug;
    }
    set
    {
        m_IsAllergicDrug =value;
    }
}   
        //private string m_PracticeFavorites = "";  //  Generic Name - A
        //private string m_BeersList = "";
        //private bool m_IsAllergicDrug = false;

  
}
}
