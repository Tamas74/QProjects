using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace gloEmdeonInterface.Classes
{
    public class clsgloLabOrder
    {

        private void emdAddorder()
        {
            //EmdeonOrder_Mst oemdOrder = new EmdeonOrder_Mst();
            //EmdeonOrder_Msts oemdOrders = new EmdeonOrder_Msts();

            //EmdeonOrder_test oemdtest = new EmdeonOrder_test();
            //EmdeonOrder_tests oemdoemdtests = new EmdeonOrder_tests();

            //oemdOrder = new EmdeonOrder_Mst();
            //oemdOrder.EmdeonOrderID = 34495;

            //oemdoemdtests = new EmdeonOrder_tests();

            //oemdtest = new EmdeonOrder_test();
            //oemdtest.EmdeonTestCode = "test1";
            //oemdoemdtests.Add(oemdtest);

            //oemdtest = new EmdeonOrder_test();
            //oemdtest.EmdeonTestCode = "test2";
            //oemdoemdtests.Add(oemdtest);

            //oemdtest = new EmdeonOrder_test();
            //oemdtest.EmdeonTestCode = "test3";
            //oemdoemdtests.Add(oemdtest);

            //oemdOrder.EmdeonTests = oemdoemdtests;
            //oemdOrders.Add(oemdOrder);


            //oemdOrder = new EmdeonOrder_Mst();
            //oemdOrder.EmdeonOrderID = 34496;
            //oemdoemdtests = new EmdeonOrder_tests();

            //oemdtest = new EmdeonOrder_test();
            //oemdtest.EmdeonTestCode = "test21";
            //oemdoemdtests.Add(oemdtest);

            //oemdtest = new EmdeonOrder_test();
            //oemdtest.EmdeonTestCode = "test22";
            //oemdoemdtests.Add(oemdtest);

            //oemdOrder.EmdeonTests = oemdoemdtests;
            //oemdOrders.Add(oemdOrder);

            //oemdOrders[1].EmdeonTests[1].EmdeonTestCode;


        }

    }

    public class GloLabOrder_Mst : IDisposable
    {
        #region "Private Variables"

        private Double _gloLabOrderID;
        private GloLabOrder_tests _gloLabTests;
        private DateTime _transactionDate;
        private Int64 _patientID;
        private Int64 _patientAgeYear;
        private Int64 _patientAgeDays;
        private Int64 _patientAgeMonth;
        private Int64 _patientProviderID;
        private String _gloLabOrderNumber;

        //Added by Abhijeet On Date 20100330
        // defines variables for saving order status abd billing type of order
        private string _orderStatus;
        private string _billingType;
        // End of code by Abhijeet 


        #endregion "Private Variables"

        #region "Public properties"

        public Double GloLabOrderID
        {
            get { return _gloLabOrderID; }
            set { _gloLabOrderID = value; }
        }

        public String GloLabOrderNumber
        {
            get { return _gloLabOrderNumber; }
            set { _gloLabOrderNumber = value; }
        }

        public GloLabOrder_tests EmdeonTests
        {
            get { return _gloLabTests; }
            set { _gloLabTests = value; }
        }

        //private DateTime _transactionDate;
        public DateTime TransactionDate
        {
            get { return _transactionDate; }
            set { _transactionDate = value; }
        }
        
        //private Int64 _patientID;
        public Int64 PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        //private Int64 _patientAgeYear;
        public Int64 PatientAgeYear
        {
            get { return _patientAgeYear; }
            set { _patientAgeYear = value; }
        }

        //private Int64 _patientAgeDays;
        public Int64 PatientAgeDays
        {
            get { return _patientAgeDays; }
            set { _patientAgeDays = value; }
        }

        //private Int64 _patientAgeMonth;
        public Int64 PatientAgeMonth
        {
            get { return _patientAgeMonth; }
            set { _patientAgeMonth = value; }
        }

        //private Int64 _patientProviderID;
        public Int64 PatientProviderID
        {
            get { return _patientProviderID; }
            set { _patientProviderID = value; }
        }

        //Added by Abhijeet On Date 20100330
        // defines properties for saving order status abd billing type of order
        public string OrderStatus
        {
            get {return _orderStatus ;}
            set {_orderStatus= value ;}
        }
        public string BillingType
        {
            get {return _billingType ;}
            set {_billingType =value ;}
        }        
        // End of code by Abhijeet 

        #endregion "Public properties"

        #region "Constructor & Distructor"

        public GloLabOrder_Mst()
        {

        }

        public GloLabOrder_Mst(Double EmdeonOrderID, GloLabOrder_tests GloLabTests)
        {
            _gloLabOrderID = GloLabOrderID;
            _gloLabTests = GloLabTests;
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~GloLabOrder_Mst()
        {
            Dispose(false);
        }

        #endregion

    }

    public class GloLabOrder_Msts : IDisposable
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public GloLabOrder_Msts()
        {
            _innerlist = new ArrayList();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }


        ~GloLabOrder_Msts()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(GloLabOrder_Mst item)
        {
            _innerlist.Add(item);
        }

        public void Add(double GloLabOrderID, GloLabOrder_tests GloLabTests)
        {
            //GloLabOrder_tests oGloLabTests = new GloLabOrder_tests(GloLabOrderID, GloLabTests);

            GloLabOrder_Mst oGloLabOrder_Mst = new GloLabOrder_Mst(GloLabOrderID, GloLabTests);
            _innerlist.Add(oGloLabOrder_Mst);

        }

        public void Add(double GloLabOrderID, GloLabOrder_test GloLabTest)
        {
            GloLabOrder_tests GloLabTests = new GloLabOrder_tests();
            GloLabTests.Add(GloLabTest);

            GloLabOrder_Mst oGloLabOrder_Mst = new GloLabOrder_Mst(GloLabOrderID, GloLabTests);
            _innerlist.Add(oGloLabOrder_Mst);

        }
        //Remark - Work Remining for comparision
        public bool Remove(GloLabOrder_Mst item)
        {
            bool result = false;


            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public GloLabOrder_Mst this[int index]
        {
            get
            { return (GloLabOrder_Mst)_innerlist[index]; }
        }

        public bool Contains(GloLabOrder_Mst item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(GloLabOrder_Mst item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(GloLabOrder_Mst[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    
    public class GloLabOrder_test : IDisposable
    {

        #region "Private Variables"

        private string _sGloLabTestCode = string.Empty;
        private string _sGloLabTestName = string.Empty;

        //// By Abhijeet Farkande on Date 20100224
        private GloLabOrder_test_diagnoses _gloLabTestsDiagnoses;

        //// End of code By Abhijeet Farkande on Date 20100224

      
        #endregion

        #region "Properties"
        //_sGloLabTestCode
        public string GloLabTestCode
        {
            get { return _sGloLabTestCode; }
            set { _sGloLabTestCode = value; }
        }
        //_sGloLabTestName
        public string GloLabTestName 
        {
            get { return _sGloLabTestName; }
            set { _sGloLabTestName = value; }
        }

        //// By Abhijeet Farkande on Date 20100224
        public GloLabOrder_test_diagnoses GloLabTestsDiagnoses
        {
            get { return _gloLabTestsDiagnoses;}
            set { _gloLabTestsDiagnoses=value;}
        }
        //// End of code By Abhijeet Farkande on Date 20100224
       

        #endregion "Properties"

        #region "Constructor & Distructor"

        public GloLabOrder_test()
        {

        }

        public GloLabOrder_test(string GloLabTestCode)
        {
            _sGloLabTestCode = GloLabTestCode;
        }
        public GloLabOrder_test(string GloLabTestCode, string GloLabTestName)
        {
            _sGloLabTestCode = GloLabTestCode;
            _sGloLabTestName = GloLabTestName;
        }
        public GloLabOrder_test(string GLOLabTestCode, string GloLabTestName, GloLabOrder_test_diagnoses GloLabTestsDiadnoses)
        {
            _sGloLabTestCode = GloLabTestCode;
            _sGloLabTestName = GloLabTestName;
            _gloLabTestsDiagnoses = GloLabTestsDiadnoses;
        }

      
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~GloLabOrder_test()
        {
            Dispose(false);
        }

        #endregion


    }

    public class GloLabOrder_tests : IDisposable
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public GloLabOrder_tests()
        {
            _innerlist = new ArrayList();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }


        ~GloLabOrder_tests()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(GloLabOrder_test item)
        {
            _innerlist.Add(item);
        }
        public void Add(string GloLabTestCode)
        {
            GloLabOrder_test oGloLabOrder_test = new GloLabOrder_test(GloLabTestCode);
            _innerlist.Add(oGloLabOrder_test);
        }
        public void Add(string GloLabTestCode, string GloLabTestName)
        {
            GloLabOrder_test oGloLabOrder_test = new GloLabOrder_test(GloLabTestCode, GloLabTestName);
            _innerlist.Add(oGloLabOrder_test);
        }

        //// By Abhijeet Farkande on Date 20100224
        public void Add(string GloLabTestCode, string GloLabTestName, GloLabOrder_test_diagnoses GloLabTestDiagnoses)
        {
            GloLabOrder_test oGloLabOrder_test = new GloLabOrder_test(GloLabTestCode, GloLabTestName, GloLabTestDiagnoses);
            _innerlist.Add(oGloLabOrder_test);
        }
        //// End of code By Abhijeet Farkande on Date 20100224

        
        //Remark - Work Remining for comparision

        public bool Remove(GloLabOrder_test item)
        {
            bool result = false;


            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public GloLabOrder_test this[int index]
        {
            get
            { return (GloLabOrder_test)_innerlist[index]; }
        }

        public bool Contains(GloLabOrder_test item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(GloLabOrder_test item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(GloLabOrder_test[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }


    //// By Abhijeet Farkande On date 20100224
    //// Declaration & defination of collection GloLabOrder_test_diagnosis,GloLabOrder_test_diagnoses for diagnosis
      public class GloLabOrder_test_diagnosis : IDisposable
    {

        #region "Private Variables"

        private string _sGloLabDiagDescription = string.Empty;
        private string _sGloLabICD9Code = string.Empty;
        private int _nICDRevision = 0;
        #endregion "Private Variables"

        #region "Properties"


        //_sGloLabDiagDescription
        public string GloLabDiagDescription
        {
            get {return _sGloLabDiagDescription ;}
            set { _sGloLabDiagDescription=value;}
        }
        public string GloLabICD9Code
        {
            get { return _sGloLabICD9Code; }
            set { _sGloLabICD9Code = value; }
        }
      //added for icd10 feature fetch it from emdeon
        public int  nICDRevision
        {
            get { return _nICDRevision; }
            set { _nICDRevision = value; }
        }
       
       
        #endregion "Properties"

        #region "Constructor & Distructor"

        public GloLabOrder_test_diagnosis()
        {

        }
        // nICdRevision added for icd10 feature fetch it from emdeon
        public GloLabOrder_test_diagnosis(string GloLabICD9Code,int nICdRevision=9)
        {
           _sGloLabICD9Code = GloLabICD9Code;
           _nICDRevision = nICdRevision;
        }
        // nICdRevision added for icd10 feature fetch it from emdeon
        public GloLabOrder_test_diagnosis(string GloLabICD9Code, string GloLabDiagDescription, int nICdRevision = 9)
        {
            _sGloLabICD9Code = GloLabICD9Code;
           _sGloLabDiagDescription = GloLabDiagDescription;
           _nICDRevision = nICdRevision;
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~GloLabOrder_test_diagnosis()
        {
            Dispose(false);
        }

        #endregion "Constructor & Distructor"

    }

    public class GloLabOrder_test_diagnoses : IDisposable
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public GloLabOrder_test_diagnoses()
        {
            _innerlist = new ArrayList();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }
        ~GloLabOrder_test_diagnoses()
        {
            Dispose(false);
        }
        #endregion "Constructor & Destructor"

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(GloLabOrder_test_diagnosis item)
        {
            _innerlist.Add(item);
        }
        public void Add(string GloLabICD9Code)
        {
            GloLabOrder_test_diagnosis oGloLabOrder_test_diagnosis = new GloLabOrder_test_diagnosis(GloLabICD9Code);
            _innerlist.Add(oGloLabOrder_test_diagnosis);
        }
        public void Add(string GloLabICD9Code, string GloLabDiagDescription)
        {
            GloLabOrder_test_diagnosis oGloLabOrder_test_diagnosis = new GloLabOrder_test_diagnosis(GloLabICD9Code, GloLabDiagDescription);
            _innerlist.Add(oGloLabOrder_test_diagnosis);
        }

       
        //Remark - Work Remining for comparision

        public bool Remove(GloLabOrder_test_diagnosis item)
        {
            bool result = false;


            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public GloLabOrder_test_diagnosis this[int index]
        {
            get
            { return (GloLabOrder_test_diagnosis)_innerlist[index]; }
        }

        public bool Contains(GloLabOrder_test_diagnosis item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(GloLabOrder_test_diagnosis item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(GloLabOrder_test_diagnosis[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }
    //// End of collection for Diadnosis By Abhijeet


}

