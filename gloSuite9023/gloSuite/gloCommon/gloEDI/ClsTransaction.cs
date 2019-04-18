using System;
using System.Collections.Generic;
using System.Text;
    public class ClsTransaction
    {
        public ClsTransaction():base()
        {
            
        }
        private string sTransactionID="";
        private string sActionCode = "";
        private string sCodeSet = "";
        private string sSearchTerms = "";
        private ClsPatient oPatient = null;
        private List<ClsServiceInfo> oServices;

        public List<ClsServiceInfo> Services
        {
            get
            {
                if (oServices == null)
                {
                    oServices = new List<ClsServiceInfo>();
                }
                return oServices;

            }
            set
            {
                oServices = value;
            }

        }

        public ClsPatient Patient
        {
            get
            {
                if (oPatient == null)
                {
                    oPatient = new ClsPatient();
                }
                return oPatient;
              
            }
            set
            {
                oPatient = value;
            }

        }
        public string CodeSet
        {
            get
            {
                return sCodeSet;
            }
            set
            {
                sCodeSet = value;
            }

        }
        public string SearchTerms
        {
            get
            {
                return sSearchTerms;
            }
            set
            {
                sSearchTerms = value;
            }

        }
        public string TransactionID
        {
            get 
            {
                return sTransactionID;
            }
            set
            {
                sTransactionID = value;
            }

        }
        public string ActionCode
        {
            get
            {
                return sActionCode;
            }
            set
            {
                sActionCode = value;
            }

        }

    }
public class ClsPatient
{
    public ClsPatient()
        : base()
    {
    }
        private string sMonth="";
        private string sDay = "";
        private string sYear = "";
        private string sGender = "";

        public string Gender
        {
            get
            {
                return sGender;
            }
            set
            {
                sGender = value;
            }

        }


        public string Month
        {
            get
            {
                return sMonth;
            }
            set
            {
                sMonth = value;
            }

        }

        public string Day
        {
            get
            {
                return sDay;
            }
            set
            {
                sDay = value;
            }

        }
        public string Year
        {
            get
            {
                return sYear;
            }
            set
            {
                sYear = value;
            }

        }
        

    }
public class ClsServiceInfo
{
    public ClsServiceInfo()
        : base()
    {
    }
    private string sPOS;
    private string sFromMonth;
    private string sFromDay;
    private string sFromYear;

    private string sToMonth;
    private string sToDay;
    private string sToYear;

    private string sProcCode;
    private List<string> sModifierList;
    private List<string> sDiagnosisList;

    public List<string> DiagnosisList
    {
        get
        {
            if (sDiagnosisList == null)
            {
                sDiagnosisList = new List<string>();

            }

            return sDiagnosisList;
        }
        set
        {
            sDiagnosisList = value;
        }
    }
    

    public List<string> ModifierList
    {
        get
        {
            if (sModifierList == null)
            {
                sModifierList = new List<string>();

            }

            return sModifierList;
        }
        set
        {
            sModifierList = value;
        }
    }
    public string ProcCode
    {
        get
        {
            return sProcCode;
        }
        set
        {
            sProcCode = value;
        }

    }

    public string POS
    {
        get
        {
            return sPOS;
        }
        set
        {
            sPOS = value;
        }

    }
    public string FromMonth
    {
        get
        {
            return sFromMonth;
        }
        set
        {
            sFromMonth = value;
        }

    }
    public string FromDay
    {
        get
        {
            return sFromDay;
        }
        set
        {
            sFromDay = value;
        }

    }
    public string FromYear
    {
        get
        {
            return sFromYear;
        }
        set
        {
            sFromYear = value;
        }

    }
    public string ToMonth
    {
        get
        {
            return sToMonth;
        }
        set
        {
            sToMonth = value;
        }

    }
    public string ToDay
    {
        get
        {
            return sToDay;
        }
        set
        {
            sToDay = value;
        }

    }
    public string ToYear
    {
        get
        {
            return sToYear;
        }
        set
        {
            sToYear = value;
        }

    }
}

public class ClsValidationResponse
{
    public ClsValidationResponse()
        : base()
    {
    }
    private string sCode="";
    private string sMedicalNecessity="";
    private string sCCI = "";
    private string sUsage = "";
    private string sModifiers = "";

    public string Code
    {
        get
        {
            return sCode;
        }
        set
        {
            sCode = value;
        }

    }
    public string MedicalNecessity
    {
        get
        {
            return sMedicalNecessity;
        }
        set
        {
            sMedicalNecessity = value;
        }
    }
    public string CCI
    {
        get
        {
            return sCCI;
        }
        set
        {
            sCCI = value;
        }
    }
    public string Usage
    {
        get
        {
            return sUsage;
        }
        set
        {
            sUsage = value;
        }
    }
    public string Modifiers
    {
        get
        {
            return sModifiers;
        }
        set
        {
            sModifiers = value;
        }
    }
}

public class ClsSearchResponse
{
    public ClsSearchResponse()
        : base()
    {
    }
    private string sCode = "";
    private string sDescription = "";

    public string Code
    {
        get
        {
            return sCode;
        }
        set
        {
            sCode = value;
        }

    }
    public string Description
    {
        get
        {
            return sDescription;
        }
        set
        {
            sDescription = value;
        }
    }
    
    
}