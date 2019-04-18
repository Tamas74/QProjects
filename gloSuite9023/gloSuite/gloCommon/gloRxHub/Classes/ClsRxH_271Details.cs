using System;
using System.Collections.Generic;
using System.Text;

namespace gloRxHub
{
    public class ClsRxH_271Details : IDisposable
    {

        //List of all fields
        #region "Private and Public variables"

        private string sIsSubscriberdemoChange = "";
        private string sMessageID = "";
        private string sSubscriberFirstName = "";
        private string sSubscriberMiddleName = "";
        private string sSubscriberLastName = "";
        private string sSubscriberSuffix = "";
        private string sSubscriberGender = "";
        private string sSubscriberDOB = "";
        private string sSubscriberSSN = "";
        private string sSubscriberAddress1 = "";
        private string sSubscriberAddress2 = "";
        private string sSubscriberCity = "";
        private string sSubscriberState = "";
        private string sSubscriberZip = "";

        private string  sSubscriberDemochgFirstName	= "";
        private string  sSubscriberDemoChgMiddleName	= "";
        private string sSubscriberDemoChgLastName	= "";
        private string sSubscriberDemoChgGender	= "";
        private string sSubscriberDemoChgDOB	= "";
        private string sSubscriberDemoChgSSN	= "";
        private string sSubscriberDemoChgAddress1	= "";
        private string sSubscriberDemoChgAddress2	= "";
        private string sSubscriberDemoChgCity	= "";
        private string sSubscriberDemoChgState	= "";
        private string sSubscriberDemoChgZip = "";


        private bool disposedValue = false;
        #endregion "Private and Public variables"
        // IDisposable 

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                // TODO: free managed resources when explicitly called 
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


        #region "Property procedures for Rxh_271Details"



        public string IsSubscriberdemoChange
        {
            get { return sIsSubscriberdemoChange; }
            set { sIsSubscriberdemoChange = value; }
        }

        public string MessageID
        {
            get { return sMessageID; }
            set { sMessageID = value; }
        }

        public string SubscriberFirstName
        {
            get { return sSubscriberFirstName; }
            set { sSubscriberFirstName = value; }

        }

        public string SubscriberMiddleName
        {
            get { return sSubscriberMiddleName; }
            set { sSubscriberMiddleName = value; }
        }

        public string SubscriberLastName
        {
            get { return sSubscriberLastName; }
            set { sSubscriberLastName = value; }
        }

        public string SubscriberSuffix
        {
            get { return sSubscriberSuffix; }
            set { sSubscriberSuffix = value; }
        }

        public string SubscriberGender
        {
            get { return sSubscriberGender; }
            set { sSubscriberGender = value; }
        }
        public string SubscriberDOB
        {
            get { return sSubscriberDOB; }
            set { sSubscriberDOB = value; }
        }
        public string SubscriberSSN
        {
            get { return sSubscriberSSN; }
            set { sSubscriberSSN = value; }
        }
        public string SubscriberAddress1
        {
            get { return sSubscriberAddress1; }
            set { sSubscriberAddress1 = value; }
        }
        public string SubscriberAddress2
        {
            get { return sSubscriberAddress2; }
            set { sSubscriberAddress2 = value; }
        }
        public string SubscriberCity
        {
            get { return sSubscriberCity; }
            set { sSubscriberCity = value; }
        }
        public string SubscriberState
        {
            get { return sSubscriberState; }
            set { sSubscriberState = value; }
        }
        public string SubscriberZip
        {
            get { return sSubscriberZip; }
            set { sSubscriberZip = value; }
        }

        //Subscriber demographic changed
        public string SubscriberDemochgFirstName
        {
            get { return sSubscriberDemochgFirstName; }
            set { sSubscriberDemochgFirstName = value; }
        }

          public string SubscriberDemoChgMiddleName
        {
            get { return sSubscriberDemoChgMiddleName; }
            set { sSubscriberDemoChgMiddleName = value; }
        }
        
          public string SubscriberDemoChgLastName
        {
            get { return sSubscriberDemoChgLastName; }
            set { sSubscriberDemoChgLastName = value; }
        }

        public string SubscriberDemoChgGender
        {
            get { return sSubscriberDemoChgGender; }
            set { sSubscriberDemoChgGender = value; }
        }

        public string SubscriberDemoChgDOB
        {
            get { return sSubscriberDemoChgDOB; }
            set { sSubscriberDemoChgDOB = value; }
        }

         public string SubscriberDemoChgSSN
        {
            get { return sSubscriberDemoChgSSN; }
            set { sSubscriberDemoChgSSN = value; }
        }

        public string SubscriberDemoChgAddress1
        {
            get { return sSubscriberDemoChgAddress1; }
            set { sSubscriberDemoChgAddress1 = value; }
        }

          public string SubscriberDemoChgAddress2
        {
            get { return sSubscriberDemoChgAddress2; }
            set { sSubscriberDemoChgAddress2 = value; }
        }

        public string SubscriberDemoChgCity
        {
            get { return sSubscriberDemoChgCity; }
            set { sSubscriberDemoChgCity = value; }
        }

        public string SubscriberDemoChgState
        {
            get { return sSubscriberDemoChgState; }
            set { sSubscriberDemoChgState = value; }
        }

         public string SubscriberDemoChgZip
        {
            get { return sSubscriberDemoChgZip; }
            set { sSubscriberDemoChgZip = value; }
        }
        //Subscriber demographic changed
     

        #endregion "Property procedures for Rxh_271Details"


    }
}
