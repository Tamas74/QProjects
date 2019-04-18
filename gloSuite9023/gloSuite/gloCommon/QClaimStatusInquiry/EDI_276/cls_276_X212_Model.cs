using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace TriArqEDIRealTimeClaimStatus.EDI_276
{
    class cls_276_X212_Model
    {
    }


    public class Cls_276X212_ISA
    {
        public Cls_276X212_ISA()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _ISAID;
        public long ISAID
        {
            get { return _ISAID; }
            set { _ISAID = value; }
        }

        string _ISA01_AuthorInfoQual;
        public string ISA01_AuthorInfoQual
        {
            get { return _ISA01_AuthorInfoQual; }
            set { _ISA01_AuthorInfoQual = value; }
        }

        string _ISA02_AuthorInfo;
        public string ISA02_AuthorInfo
        {
            get { return _ISA02_AuthorInfo; }
            set { _ISA02_AuthorInfo = value; }
        }

        string _ISA03_SecurityInfoQual;
        public string ISA03_SecurityInfoQual
        {
            get { return _ISA03_SecurityInfoQual; }
            set { _ISA03_SecurityInfoQual = value; }
        }

        string _ISA04_SecurityInfo;
        public string ISA04_SecurityInfo
        {
            get { return _ISA04_SecurityInfo; }
            set { _ISA04_SecurityInfo = value; }
        }

        string _ISA05_IntrChngIDQual;
        public string ISA05_IntrChngIDQual
        {
            get { return _ISA05_IntrChngIDQual; }
            set { _ISA05_IntrChngIDQual = value; }
        }

        string _ISA06_IntrChngSenderID;
        public string ISA06_IntrChngSenderID
        {
            get { return _ISA06_IntrChngSenderID; }
            set { _ISA06_IntrChngSenderID = value; }
        }

        string _ISA07_IntrChngIDQual;
        public string ISA07_IntrChngIDQual
        {
            get { return _ISA07_IntrChngIDQual; }
            set { _ISA07_IntrChngIDQual = value; }
        }

        string _ISA08_IntrChngReceiverID;
        public string ISA08_IntrChngReceiverID
        {
            get { return _ISA08_IntrChngReceiverID; }
            set { _ISA08_IntrChngReceiverID = value; }
        }

        string _ISA09_IntrChngDate;
        public string ISA09_IntrChngDate
        {
            get { return _ISA09_IntrChngDate; }
            set { _ISA09_IntrChngDate = value; }
        }

        string _ISA10_IntrChngTime;
        public string ISA10_IntrChngTime
        {
            get { return _ISA10_IntrChngTime; }
            set { _ISA10_IntrChngTime = value; }
        }

        string _ISA11_IntrChngRepetitionSeperator;
        public string ISA11_IntrChngRepetitionSeperator
        {
            get { return _ISA11_IntrChngRepetitionSeperator; }
            set { _ISA11_IntrChngRepetitionSeperator = value; }
        }

        string _ISA12_IntrChngControlVersionNo;
        public string ISA12_IntrChngControlVersionNo
        {
            get { return _ISA12_IntrChngControlVersionNo; }
            set { _ISA12_IntrChngControlVersionNo = value; }
        }

        string _ISA13_IntrChngControlNo;
        public string ISA13_IntrChngControlNo
        {
            get { return _ISA13_IntrChngControlNo; }
            set { _ISA13_IntrChngControlNo = value; }
        }

        string _ISA14_AckwRequested;
        public string ISA14_AckwRequested
        {
            get { return _ISA14_AckwRequested; }
            set { _ISA14_AckwRequested = value; }
        }

        string _ISA15_UsageIndicator;
        public string ISA15_UsageIndicator
        {
            get { return _ISA15_UsageIndicator; }
            set { _ISA15_UsageIndicator = value; }
        }

        string _ISA16_ComponentElementSeparator;
        public string ISA16_ComponentElementSeparator
        {
            get { return _ISA16_ComponentElementSeparator; }
            set { _ISA16_ComponentElementSeparator = value; }
        }

        Cls_276X212_GS _ISA_GS;
        public Cls_276X212_GS ISA_GS
        {
            get { return _ISA_GS; }
            set { _ISA_GS = value; }
        }

        List<Cls_276X212_ST> _ISA_ST;
        public List<Cls_276X212_ST> ISA_ST
        {
            get { return _ISA_ST; }
            set { _ISA_ST = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_ISAs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_ISAs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_ISAs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_ISA item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_ISA item)
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
        public Cls_276X212_ISA this[int index]
        {
            get
            { return (Cls_276X212_ISA)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_ISA item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_ISA item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_ISA[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_276X212_GS
    {
        public Cls_276X212_GS()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _GS_ISAID;
        public long GS_ISAID
        {
            get { return _GS_ISAID; }
            set { _GS_ISAID = value; }
        }

        long _GSID;
        public long GSID
        {
            get { return _GSID; }
            set { _GSID = value; }
        }

        string _GS01_StatusNotification;
        public string GS01_StatusNotification
        {
            get { return _GS01_StatusNotification; }
            set { _GS01_StatusNotification = value; }
        }

        string _GS02_SenderID;
        public string GS02_SenderID
        {
            get { return _GS02_SenderID; }
            set { _GS02_SenderID = value; }
        }

        string _GS03_ReceiverID;
        public string GS03_ReceiverID
        {
            get { return _GS03_ReceiverID; }
            set { _GS03_ReceiverID = value; }
        }

        string _GS04_FunctionalGroupDate;
        public string GS04_FunctionalGroupDate
        {
            get { return _GS04_FunctionalGroupDate; }
            set { _GS04_FunctionalGroupDate = value; }
        }

        string _GS05_FunctionalGroupTime;
        public string GS05_FunctionalGroupTime
        {
            get { return _GS05_FunctionalGroupTime; }
            set { _GS05_FunctionalGroupTime = value; }
        }

        string _GS06_GroupControlNumber;
        public string GS06_GroupControlNumber
        {
            get { return _GS06_GroupControlNumber; }
            set { _GS06_GroupControlNumber = value; }
        }

        string _GS07_ResponsibleAgencyCode;
        public string GS07_ResponsibleAgencyCode
        {
            get { return _GS07_ResponsibleAgencyCode; }
            set { _GS07_ResponsibleAgencyCode = value; }
        }

        string _GS08_VersionORIndustryIdentifier;
        public string GS08_VersionORIndustryIdentifier
        {
            get { return _GS08_VersionORIndustryIdentifier; }
            set { _GS08_VersionORIndustryIdentifier = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_GSs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_GSs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_GSs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_GS item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_GS item)
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
        public Cls_276X212_GS this[int index]
        {
            get
            { return (Cls_276X212_GS)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_GS item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_GS item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_GS[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_276X212_ST
    {

        public Cls_276X212_ST()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _ST_ISAID;
        public long ST_ISAID
        {
            get { return _ST_ISAID; }
            set { _ST_ISAID = value; }
        }

        long _STID;
        public long STID
        {
            get { return _STID; }
            set { _STID = value; }
        }

        string _ST01_IdentifierCode;
        public string ST01_IdentifierCode
        {
            get { return _ST01_IdentifierCode; }
            set { _ST01_IdentifierCode = value; }
        }

        string _ST02_TransactioNSetControlNumber;
        public string ST02_TransactioNSetControlNumber
        {
            get { return _ST02_TransactioNSetControlNumber; }
            set { _ST02_TransactioNSetControlNumber = value; }
        }

        string _ST03_ConventionReference;
        public string ST03_ConventionReference
        {
            get { return _ST03_ConventionReference; }
            set { _ST03_ConventionReference = value; }
        }

        Cls_276X212_BHT _ST_BHT;
        public Cls_276X212_BHT ST_BHT
        {
            get { return _ST_BHT; }
            set { _ST_BHT = value; }
        }

        List<Cls_276X212_HL> _ST_HL;
        public List<Cls_276X212_HL> ST_HL
        {
            get { return _ST_HL; }
            set { _ST_HL = value; }
        }
        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_STs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_STs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_STs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_ST item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_ST item)
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
        public Cls_276X212_ST this[int index]
        {
            get
            { return (Cls_276X212_ST)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_ST item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_ST item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_ST[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_276X212_BHT
    {

        public Cls_276X212_BHT()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _BHT_ISAID;
        public long BHT_ISAID
        {
            get { return _BHT_ISAID; }
            set { _BHT_ISAID = value; }
        }

        long _BHT_STID;
        public long BHT_STID
        {
            get { return _BHT_STID; }
            set { _BHT_STID = value; }
        }

        long _BHTID;
        public long BHTID
        {
            get { return _BHTID; }
            set { _BHTID = value; }
        }

        string _BHT01_StructureCode;
        public string BHT01_StructureCode
        {
            get { return _BHT01_StructureCode; }
            set { _BHT01_StructureCode = value; }
        }

        string _BHT02_PurposeCode;
        public string BHT02_PurposeCode
        {
            get { return _BHT02_PurposeCode; }
            set { _BHT02_PurposeCode = value; }
        }

        string _BHT03_ReferenceIdentification;
        public string BHT03_ReferenceIdentification
        {
            get { return _BHT03_ReferenceIdentification; }
            set { _BHT03_ReferenceIdentification = value; }
        }

        string _BHT04_TransactionDate;
        public string BHT04_TransactionDate
        {
            get { return _BHT04_TransactionDate; }
            set { _BHT04_TransactionDate = value; }
        }

        string _BHT05_TransactionTime;
        public string BHT05_TransactionTime
        {
            get { return _BHT05_TransactionTime; }
            set { _BHT05_TransactionTime = value; }
        }

        string _BHT06_TransactionTypeCode;
        public string BHT06_TransactionTypeCode
        {
            get { return _BHT06_TransactionTypeCode; }
            set { _BHT06_TransactionTypeCode = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_BHTs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_BHTs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_BHTs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_BHT item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_BHT item)
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
        public Cls_276X212_BHT this[int index]
        {
            get
            { return (Cls_276X212_BHT)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_BHT item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_BHT item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_BHT[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_276X212_HL
    {
        public Cls_276X212_HL()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _HL_ISAID;
        public long HL_ISAID
        {
            get { return _HL_ISAID; }
            set { _HL_ISAID = value; }
        }

        long _HL_STID;
        public long HL_STID
        {
            get { return _HL_STID; }
            set { _HL_STID = value; }
        }

        long _ParentHLID;
        public long ParentHLID
        {
            get { return _ParentHLID; }
            set { _ParentHLID = value; }
        }

        long _HLID;
        public long HLID
        {
            get { return _HLID; }
            set { _HLID = value; }
        }

        string _HL01_HLSegmentId;
        public string HL01_HLSegmentId
        {
            get { return _HL01_HLSegmentId; }
            set { _HL01_HLSegmentId = value; }
        }

        string _HL02_HLParentId;
        public string HL02_HLParentId
        {
            get { return _HL02_HLParentId; }
            set { _HL02_HLParentId = value; }
        }

        string _HL03_LevelCode;
        public string HL03_LevelCode
        {
            get { return _HL03_LevelCode; }
            set { _HL03_LevelCode = value; }
        }

        string _HL04_ChildCode;
        public string HL04_ChildCode
        {
            get { return _HL04_ChildCode; }
            set { _HL04_ChildCode = value; }
        }

        Cls_276X212_DMG _HL_DMG;
        public Cls_276X212_DMG HL_DMG
        {
            get { return _HL_DMG; }
            set { _HL_DMG = value; }
        }

        Cls_276X212_NM _HL_NM;
        public Cls_276X212_NM HL_NM
        {
            get { return _HL_NM; }
            set { _HL_NM = value; }
        }

        List<Cls_276X212_TRN> _HL_TRN;
        public List<Cls_276X212_TRN> HL_TRN
        {
            get { return _HL_TRN; }
            set { _HL_TRN = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_HLs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_HLs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_HLs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_HL item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_HL item)
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
        public Cls_276X212_HL this[int index]
        {
            get
            { return (Cls_276X212_HL)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_HL item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_HL item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_HL[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_276X212_NM
    {

        public Cls_276X212_NM()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _NM_ISAID;
        public long NM_ISAID
        {
            get { return _NM_ISAID; }
            set { _NM_ISAID = value; }
        }

        long _NM_STID;
        public long NM_STID
        {
            get { return _NM_STID; }
            set { _NM_STID = value; }
        }

        long _NM_HLID;
        public long NM_HLID
        {
            get { return _NM_HLID; }
            set { _NM_HLID = value; }
        }

        long _NMID;
        public long NMID
        {
            get { return _NMID; }
            set { _NMID = value; }
        }

        string _NM101_EntityIdCode;
        public string NM101_EntityIdCode
        {
            get { return _NM101_EntityIdCode; }
            set { _NM101_EntityIdCode = value; }
        }

        string _NM102_EntityTypeQualifier;
        public string NM102_EntityTypeQualifier
        {
            get { return _NM102_EntityTypeQualifier; }
            set { _NM102_EntityTypeQualifier = value; }
        }

        string _NM103_LastName;
        public string NM103_LastName
        {
            get { return _NM103_LastName; }
            set { _NM103_LastName = value; }
        }

        string _NM104_FirstName;
        public string NM104_FirstName
        {
            get { return _NM104_FirstName; }
            set { _NM104_FirstName = value; }
        }

        string _NM105_MiddleName;
        public string NM105_MiddleName
        {
            get { return _NM105_MiddleName; }
            set { _NM105_MiddleName = value; }
        }

        string _NM106_Prefix;
        public string NM106_Prefix
        {
            get { return _NM106_Prefix; }
            set { _NM106_Prefix = value; }
        }

        string _NM107_Suffix;
        public string NM107_Suffix
        {
            get { return _NM107_Suffix; }
            set { _NM107_Suffix = value; }
        }

        string _NM108_IdentificationCodeQualifier;
        public string NM108_IdentificationCodeQualifier
        {
            get { return _NM108_IdentificationCodeQualifier; }
            set { _NM108_IdentificationCodeQualifier = value; }
        }

        string _NM109_IdentificationCode;
        public string NM109_IdentificationCode
        {
            get { return _NM109_IdentificationCode; }
            set { _NM109_IdentificationCode = value; }
        }

        string _NM110_EntityRelationShipCode;
        public string NM110_EntityRelationShipCode
        {
            get { return _NM110_EntityRelationShipCode; }
            set { _NM110_EntityRelationShipCode = value; }
        }

        string _NM111_EntityIdentifierCode;
        public string NM111_EntityIdentifierCode
        {
            get { return _NM111_EntityIdentifierCode; }
            set { _NM111_EntityIdentifierCode = value; }
        }

        string _NM112_NameLastOROrganizatioName;
        public string NM112_NameLastOROrganizatioName
        {
            get { return _NM112_NameLastOROrganizatioName; }
            set { _NM112_NameLastOROrganizatioName = value; }
        }
        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_NMs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_NMs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_NMs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_NM item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_NM item)
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
        public Cls_276X212_NM this[int index]
        {
            get
            { return (Cls_276X212_NM)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_NM item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_NM item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_NM[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_276X212_DMG
    {
        public Cls_276X212_DMG()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _DMG_ISAID;
        public long DMG_ISAID
        {
            get { return _DMG_ISAID; }
            set { _DMG_ISAID = value; }
        }

        long _DMG_STID;
        public long DMG_STID
        {
            get { return _DMG_STID; }
            set { _DMG_STID = value; }
        }

        long _DMG_HLID;
        public long DMG_HLID
        {
            get { return _DMG_HLID; }
            set { _DMG_HLID = value; }
        }

        long _DMGID;
        public long DMGID
        {
            get { return _DMGID; }
            set { _DMGID = value; }
        }

        string _DMG01_DateTimePeriodFormatQualifier;
        public string DMG01_DateTimePeriodFormatQualifier
        {
            get { return _DMG01_DateTimePeriodFormatQualifier; }
            set { _DMG01_DateTimePeriodFormatQualifier = value; }
        }

        string _DMG02_DateTimePeriod;
        public string DMG02_DateTimePeriod
        {
            get { return _DMG02_DateTimePeriod; }
            set { _DMG02_DateTimePeriod = value; }
        }

        string _DMG03_GenderCode;
        public string DMG03_GenderCode
        {
            get { return _DMG03_GenderCode; }
            set { _DMG03_GenderCode = value; }
        }

        string _DMG04_MaritalStatusCode;
        public string DMG04_MaritalStatusCode
        {
            get { return _DMG04_MaritalStatusCode; }
            set { _DMG04_MaritalStatusCode = value; }
        }

        string _DMG05_01_RaceorEthnicityCode;
        public string DMG05_01_RaceorEthnicityCode
        {
            get { return _DMG05_01_RaceorEthnicityCode; }
            set { _DMG05_01_RaceorEthnicityCode = value; }
        }

        string _DMG05_02_QualifierCode;
        public string DMG05_02_QualifierCode
        {
            get { return _DMG05_02_QualifierCode; }
            set { _DMG05_02_QualifierCode = value; }
        }

        string _DMG05_03_IndustryCode;
        public string DMG05_03_IndustryCode
        {
            get { return _DMG05_03_IndustryCode; }
            set { _DMG05_03_IndustryCode = value; }
        }

        string _DMG06_CitizenShipStatusCode;
        public string DMG06_CitizenShipStatusCode
        {
            get { return _DMG06_CitizenShipStatusCode; }
            set { _DMG06_CitizenShipStatusCode = value; }
        }

        string _DMG07_CountryCode;
        public string DMG07_CountryCode
        {
            get { return _DMG07_CountryCode; }
            set { _DMG07_CountryCode = value; }
        }

        string _DMG08_VerificationCode;
        public string DMG08_VerificationCode
        {
            get { return _DMG08_VerificationCode; }
            set { _DMG08_VerificationCode = value; }
        }

        string _DMG09_Quantity;
        public string DMG09_Quantity
        {
            get { return _DMG09_Quantity; }
            set { _DMG09_Quantity = value; }
        }

        string _DMG010_CodeListQualifierCode;
        public string DMG010_CodeListQualifierCode
        {
            get { return _DMG010_CodeListQualifierCode; }
            set { _DMG010_CodeListQualifierCode = value; }
        }

        string _DMG011_IndustryCode;
        public string DMG011_IndustryCode
        {
            get { return _DMG011_IndustryCode; }
            set { _DMG011_IndustryCode = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_DMGs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_DMGs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_DMGs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_DMG item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_DMG item)
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
        public Cls_276X212_DMG this[int index]
        {
            get
            { return (Cls_276X212_DMG)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_DMG item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_DMG item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_DMG[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion
    }


    public class Cls_276X212_TRN
    {
        public Cls_276X212_TRN()
        {

        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _TRN_ISAID;
        public long TRN_ISAID
        {
            get { return _TRN_ISAID; }
            set { _TRN_ISAID = value; }
        }

        long _TRN_STID;
        public long TRN_STID
        {
            get { return _TRN_STID; }
            set { _TRN_STID = value; }
        }

        long _TRN_HLID;
        public long TRN_HLID
        {
            get { return _TRN_HLID; }
            set { _TRN_HLID = value; }
        }

        long _TRNID;
        public long TRNID
        {
            get { return _TRNID; }
            set { _TRNID = value; }
        }

        string _TRN01_TraceTypeID;
        public string TRN01_TraceTypeID
        {
            get { return _TRN01_TraceTypeID; }
            set { _TRN01_TraceTypeID = value; }
        }

        string _TRN02_ReferenceIdentification;
        public string TRN02_ReferenceIdentification
        {
            get { return _TRN02_ReferenceIdentification; }
            set { _TRN02_ReferenceIdentification = value; }
        }

        string _TRN03_OriginatingCompanyIdentifier;
        public string TRN03_OriginatingCompanyIdentifier
        {
            get { return _TRN03_OriginatingCompanyIdentifier; }
            set { _TRN03_OriginatingCompanyIdentifier = value; }
        }

        string _TRN04_ReferenceIdentification;
        public string TRN04_ReferenceIdentification
        {
            get { return _TRN04_ReferenceIdentification; }
            set { _TRN04_ReferenceIdentification = value; }
        }

        List<Cls_276X212_REF> _TRN_REF;
        public List<Cls_276X212_REF> TRN_REF
        {
            get { return _TRN_REF; }
            set { _TRN_REF = value; }
        }

        Cls_276X212_AMT _TRN_AMT;
        public Cls_276X212_AMT TRN_AMT
        {
            get { return _TRN_AMT; }
            set { _TRN_AMT = value; }
        }

        Cls_276X212_DTP _TRN_DTP;
        public Cls_276X212_DTP TRN_DTP
        {
            get { return _TRN_DTP; }
            set { _TRN_DTP = value; }
        }

        List<Cls_276X212_SVC> _TRN_SVC;
        public List<Cls_276X212_SVC> TRN_SVC
        {
            get { return _TRN_SVC; }
            set { _TRN_SVC = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_TRNs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_TRNs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_TRNs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_TRN item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_TRN item)
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
        public Cls_276X212_TRN this[int index]
        {
            get
            { return (Cls_276X212_TRN)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_TRN item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_TRN item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_TRN[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion
    }


    public class Cls_276X212_REF
    {
        public Cls_276X212_REF()
        {

        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _REF_ISAID;
        public long REF_ISAID
        {
            get { return _REF_ISAID; }
            set { _REF_ISAID = value; }
        }

        long _REF_STID;
        public long REF_STID
        {
            get { return _REF_STID; }
            set { _REF_STID = value; }
        }

        long _REF_HLID;
        public long REF_HLID
        {
            get { return _REF_HLID; }
            set { _REF_HLID = value; }
        }

        long _REF_TRNID;
        public long REF_TRNID
        {
            get { return _REF_TRNID; }
            set { _REF_TRNID = value; }
        }

        long _REF_SVCID;
        public long REF_SVCID
        {
            get { return _REF_SVCID; }
            set { _REF_SVCID = value; }
        }

        long _REFID;
        public long REFID
        {
            get { return _REFID; }
            set { _REFID = value; }
        }

        string _REF01_ReferenceIdentificationQualifier;
        public string REF01_ReferenceIdentificationQualifier
        {
            get { return _REF01_ReferenceIdentificationQualifier; }
            set { _REF01_ReferenceIdentificationQualifier = value; }
        }

        string _REF02_ReferenceIdentification;
        public string REF02_ReferenceIdentification
        {
            get { return _REF02_ReferenceIdentification; }
            set { _REF02_ReferenceIdentification = value; }
        }

        string _REF03_Description;
        public string REF03_Description
        {
            get { return _REF03_Description; }
            set { _REF03_Description = value; }
        }

        string _REF04_01_ReferenceIdentificationQualifier;
        public string REF04_01_ReferenceIdentificationQualifier
        {
            get { return _REF04_01_ReferenceIdentificationQualifier; }
            set { _REF04_01_ReferenceIdentificationQualifier = value; }
        }

        string _REF04_02_ReferenceIdentification;
        public string REF04_02_ReferenceIdentification
        {
            get { return _REF04_02_ReferenceIdentification; }
            set { _REF04_02_ReferenceIdentification = value; }
        }

        string _REF04_03_ReferenceIdentificationQualifier;
        public string REF04_03_ReferenceIdentificationQualifier
        {
            get { return _REF04_03_ReferenceIdentificationQualifier; }
            set { _REF04_03_ReferenceIdentificationQualifier = value; }
        }

        string _REF04_04_ReferenceIdentification;
        public string REF04_04_ReferenceIdentification
        {
            get { return _REF04_04_ReferenceIdentification; }
            set { _REF04_04_ReferenceIdentification = value; }
        }

        string _REF04_05_ReferenceIdentificationQualifier;
        public string REF04_05_ReferenceIdentificationQualifier
        {
            get { return _REF04_05_ReferenceIdentificationQualifier; }
            set { _REF04_05_ReferenceIdentificationQualifier = value; }
        }

        string _REF04_06_ReferenceIdentification;
        public string REF04_06_ReferenceIdentification
        {
            get { return _REF04_06_ReferenceIdentification; }
            set { _REF04_06_ReferenceIdentification = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_REFs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_REFs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_REFs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_REF item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_REF item)
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
        public Cls_276X212_REF this[int index]
        {
            get
            { return (Cls_276X212_REF)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_REF item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_REF item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_REF[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion
    }


    public class Cls_276X212_AMT
    {
        public Cls_276X212_AMT()
        {

        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _AMT_ISAID;
        public long AMT_ISAID
        {
            get { return _AMT_ISAID; }
            set { _AMT_ISAID = value; }
        }

        long _AMT_STID;
        public long AMT_STID
        {
            get { return _AMT_STID; }
            set { _AMT_STID = value; }
        }

        long _AMT_HLID;
        public long AMT_HLID
        {
            get { return _AMT_HLID; }
            set { _AMT_HLID = value; }
        }

        long _AMT_TRNID;
        public long AMT_TRNID
        {
            get { return _AMT_TRNID; }
            set { _AMT_TRNID = value; }
        }

        long _AMTID;
        public long AMTID
        {
            get { return _AMTID; }
            set { _AMTID = value; }
        }

        string _AMT01_AmountQualifierCode;
        public string AMT01_AmountQualifierCode
        {
            get { return _AMT01_AmountQualifierCode; }
            set { _AMT01_AmountQualifierCode = value; }
        }

        string _AMT02_MonetaryAmount;
        public string AMT02_MonetaryAmount
        {
            get { return _AMT02_MonetaryAmount; }
            set { _AMT02_MonetaryAmount = value; }
        }

        string _AMT03_CreditDebitFlagCode;
        public string AMT03_CreditDebitFlagCode
        {
            get { return _AMT03_CreditDebitFlagCode; }
            set { _AMT03_CreditDebitFlagCode = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_AMTs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_AMTs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_AMTs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_AMT item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_AMT item)
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
        public Cls_276X212_AMT this[int index]
        {
            get
            { return (Cls_276X212_AMT)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_AMT item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_AMT item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_AMT[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion
    }


    public class Cls_276X212_DTP
    {
        public Cls_276X212_DTP()
        {

        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _DTP_ISAID;
        public long DTP_ISAID
        {
            get { return _DTP_ISAID; }
            set { _DTP_ISAID = value; }
        }

        long _DTP_STID;
        public long DTP_STID
        {
            get { return _DTP_STID; }
            set { _DTP_STID = value; }
        }

        long _DTP_HLID;
        public long DTP_HLID
        {
            get { return _DTP_HLID; }
            set { _DTP_HLID = value; }
        }

        long _DTP_TRNID;
        public long DTP_TRNID
        {
            get { return _DTP_TRNID; }
            set { _DTP_TRNID = value; }
        }

        long _DTP_SVCID;
        public long DTP_SVCID
        {
            get { return _DTP_SVCID; }
            set { _DTP_SVCID = value; }
        }

        long _DTPID;
        public long DTPID
        {
            get { return _DTPID; }
            set { _DTPID = value; }
        }

        string _DTP01_DateTimeQualifier;
        public string DTP01_DateTimeQualifier
        {
            get { return _DTP01_DateTimeQualifier; }
            set { _DTP01_DateTimeQualifier = value; }
        }

        string _DTP02_DateTimePeriodFormatQualifier;
        public string DTP02_DateTimePeriodFormatQualifier
        {
            get { return _DTP02_DateTimePeriodFormatQualifier; }
            set { _DTP02_DateTimePeriodFormatQualifier = value; }
        }

        string _DTP03_DateTimePeriod;
        public string DTP03_DateTimePeriod
        {
            get { return _DTP03_DateTimePeriod; }
            set { _DTP03_DateTimePeriod = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_DTPs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_DTPs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_DTPs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_DTP item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_DTP item)
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
        public Cls_276X212_DTP this[int index]
        {
            get
            { return (Cls_276X212_DTP)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_DTP item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_DTP item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_DTP[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion
    }


    public class Cls_276X212_SVC
    {
        public Cls_276X212_SVC()
        {

        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _SVC_ISAID;
        public long SVC_ISAID
        {
            get { return _SVC_ISAID; }
            set { _SVC_ISAID = value; }
        }

        long _SVC_STID;
        public long SVC_STID
        {
            get { return _SVC_STID; }
            set { _SVC_STID = value; }
        }

        long _SVC_HLID;
        public long SVC_HLID
        {
            get { return _SVC_HLID; }
            set { _SVC_HLID = value; }
        }

        long _SVC_TRNID;
        public long SVC_TRNID
        {
            get { return _SVC_TRNID; }
            set { _SVC_TRNID = value; }
        }

        long _SVCID;
        public long SVCID
        {
            get { return _SVCID; }
            set { _SVCID = value; }
        }

        string _SVC01_01_ProductORServiceIDQualifier;
        public string SVC01_01_ProductORServiceIDQualifier
        {
            get { return _SVC01_01_ProductORServiceIDQualifier; }
            set { _SVC01_01_ProductORServiceIDQualifier = value; }
        }

        string _SVC01_02_ProductORServiceID;
        public string SVC01_02_ProductORServiceID
        {
            get { return _SVC01_02_ProductORServiceID; }
            set { _SVC01_02_ProductORServiceID = value; }
        }

        string _SVC01_03_ProcedureModifier;
        public string SVC01_03_ProcedureModifier
        {
            get { return _SVC01_03_ProcedureModifier; }
            set { _SVC01_03_ProcedureModifier = value; }
        }

        string _SVC01_04_ProcedureModifier;
        public string SVC01_04_ProcedureModifier
        {
            get { return _SVC01_04_ProcedureModifier; }
            set { _SVC01_04_ProcedureModifier = value; }
        }

        string _SVC01_05_ProcedureModifier;
        public string SVC01_05_ProcedureModifier
        {
            get { return _SVC01_05_ProcedureModifier; }
            set { _SVC01_05_ProcedureModifier = value; }
        }

        string _SVC01_06_ProcedureModifier;
        public string SVC01_06_ProcedureModifier
        {
            get { return _SVC01_06_ProcedureModifier; }
            set { _SVC01_06_ProcedureModifier = value; }
        }

        string _SVC01_07_Description;
        public string SVC01_07_Description
        {
            get { return _SVC01_07_Description; }
            set { _SVC01_07_Description = value; }
        }

        string _SVC01_08_ProductORServiceID;
        public string SVC01_08_ProductORServiceID
        {
            get { return _SVC01_08_ProductORServiceID; }
            set { _SVC01_08_ProductORServiceID = value; }
        }

        string _SVC02_MonetaryAmount;
        public string SVC02_MonetaryAmount
        {
            get { return _SVC02_MonetaryAmount; }
            set { _SVC02_MonetaryAmount = value; }
        }

        string _SVC03_MonetaryAmount;
        public string SVC03_MonetaryAmount
        {
            get { return _SVC03_MonetaryAmount; }
            set { _SVC03_MonetaryAmount = value; }
        }

        string _SVC04_ProductORServiceID;
        public string SVC04_ProductORServiceID
        {
            get { return _SVC04_ProductORServiceID; }
            set { _SVC04_ProductORServiceID = value; }
        }

        string _SVC05_Quantity;
        public string SVC05_Quantity
        {
            get { return _SVC05_Quantity; }
            set { _SVC05_Quantity = value; }
        }

        string _SVC06_01_ProductORServiceIDQualifier;
        public string SVC06_01_ProductORServiceIDQualifier
        {
            get { return _SVC06_01_ProductORServiceIDQualifier; }
            set { _SVC06_01_ProductORServiceIDQualifier = value; }
        }

        string _SVC06_02_ProductORServiceID;
        public string SVC06_02_ProductORServiceID
        {
            get { return _SVC06_02_ProductORServiceID; }
            set { _SVC06_02_ProductORServiceID = value; }
        }

        string _SVC06_03_ProcedureModifier;
        public string SVC06_03_ProcedureModifier
        {
            get { return _SVC06_03_ProcedureModifier; }
            set { _SVC06_03_ProcedureModifier = value; }
        }

        string _SVC06_04_ProcedureModifier;
        public string SVC06_04_ProcedureModifier
        {
            get { return _SVC06_04_ProcedureModifier; }
            set { _SVC06_04_ProcedureModifier = value; }
        }

        string _SVC06_05_ProcedureModifier;
        public string SVC06_05_ProcedureModifier
        {
            get { return _SVC06_05_ProcedureModifier; }
            set { _SVC06_05_ProcedureModifier = value; }
        }

        string _SVC06_06_ProcedureModifier;
        public string SVC06_06_ProcedureModifier
        {
            get { return _SVC06_06_ProcedureModifier; }
            set { _SVC06_06_ProcedureModifier = value; }
        }

        string _SVC06_07_Description;
        public string SVC06_07_Description
        {
            get { return _SVC06_07_Description; }
            set { _SVC06_07_Description = value; }
        }

        string _SVC06_08_ProductORServiceID;
        public string SVC06_08_ProductORServiceID
        {
            get { return _SVC06_08_ProductORServiceID; }
            set { _SVC06_08_ProductORServiceID = value; }
        }

        string _SVC07_Quantity;
        public string SVC07_Quantity
        {
            get { return _SVC07_Quantity; }
            set { _SVC07_Quantity = value; }
        }

        Cls_276X212_REF _SVC_REF;
        public Cls_276X212_REF SVC_REF
        {
            get { return _SVC_REF; }
            set { _SVC_REF = value; }
        }

        Cls_276X212_DTP _SVC_DTP;
        public Cls_276X212_DTP SVC_DTP
        {
            get { return _SVC_DTP; }
            set { _SVC_DTP = value; }
        }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Cls_276X212_SVCs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_276X212_SVCs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~Cls_276X212_SVCs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_276X212_SVC item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_276X212_SVC item)
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
        public Cls_276X212_SVC this[int index]
        {
            get
            { return (Cls_276X212_SVC)_innerlist[index]; }
        }
        public bool Contains(Cls_276X212_SVC item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_276X212_SVC item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_276X212_SVC[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion
    }
}
