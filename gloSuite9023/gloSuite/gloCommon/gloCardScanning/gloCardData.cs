using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloCardScanning
{
    class gloDrivingLicenceData
    {
        #region  " declare private variables "
        private String _sNameFirst;

        private String _sNameLast;

        private String _sNameMiddle;

        private String _sDateOfBirth;

        private String _sSex;

        private String _sCity;

        private String _sZip;

        private String _sState;

        private String _sAddress;
        #endregion

        #region  "declare Public property "
       public  String NameFirst 
        {
            get
            { return _sNameFirst; }
            set
            { this._sNameFirst = value; }
        }

        public String NameLast
        {
            get
            { return _sNameLast; }
            set
            { this._sNameLast = value; }
        }

        public String NameMiddle
        {
            get
            { return _sNameMiddle; }
            set
            { this._sNameMiddle = value; }
        }

        public String DateOfBirth
        {
            get
            { return _sDateOfBirth; }
            set
            { this._sDateOfBirth = value; }
        }

        public String Sex
        {
            get
            { return _sSex; }
            set
            { this._sSex = value; }
        }

       public  String City
        {
            get
            { return _sCity; }
            set
            { this._sCity = value; }
        }

        public String Zip
        {
            get
            { return _sZip; }
            set
            { this._sZip = value; }
        }

        public String Address
        {
            get
            { return _sAddress; }
            set
            { this._sAddress = value; }
        }

        public String State
        {
            get
            { return _sState; }
            set
            { this._sState = value; }
        }
        #endregion
    }

    class gloInsuranceCardData
    {
        #region  " declare private variables "
        private String _sInsMemberID;

        private String _sInsMemberName;

        private String _sInsGroupNo;

        private String _sPlanProvider;

        private String _sGroupNumber;

        #endregion

        #region  "declare Public property "

        public String InsMemberID
        {
            get
            { return _sInsMemberID; }
            set
            { this._sInsMemberID = value; }
        }

        public String InsMemberName
        {
            get
            { return _sInsMemberName; }
            set
            { this._sInsMemberName = value; }
        }

        public String InsGroupNo
        {
            get
            { return _sInsGroupNo; }
            set
            { this._sInsGroupNo = value; }
        }

        public String PlanProvider
        {
            get
            { return _sPlanProvider; }
            set
            { this._sPlanProvider = value; }
        }

        public String GroupNumber
        {
            get
            { return _sGroupNumber; }
            set
            { this._sGroupNumber = value; }
        }
        
        #endregion
    }



}
