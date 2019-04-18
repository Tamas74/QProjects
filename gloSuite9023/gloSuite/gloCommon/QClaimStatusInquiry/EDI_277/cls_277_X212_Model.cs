using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TriArqEDIRealTimeClaimStatus.EDI_277
{
    class cls_277_X212_Model
    {
    }



    public class Cls_277X212_ISA
    {
        public Cls_277X212_ISA()
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

        string _ISA11_IntrChngRepetitionSeparator;
        public string ISA11_IntrChngRepetitionSeparator
        {
            get { return _ISA11_IntrChngRepetitionSeparator; }
            set { _ISA11_IntrChngRepetitionSeparator = value; }
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

        Cls_277X212_GS _ISA_GS;
        public Cls_277X212_GS ISA_GS
        {
            get { return _ISA_GS; }
            set { _ISA_GS = value; }
        }

        List<Cls_277X212_ST> _ISA_ST;
        public List<Cls_277X212_ST> ISA_ST
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

    public class Cls_277X212_ISAs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_ISAs()
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
        ~Cls_277X212_ISAs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_ISA item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_ISA item)
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
        public Cls_277X212_ISA this[int index]
        {
            get
            { return (Cls_277X212_ISA)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_ISA item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_ISA item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_ISA[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_GS
    {
        public Cls_277X212_GS()
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

        string _GS01_FunctionalIdCode;
        public string GS01_FunctionalIdCode
        {
            get { return _GS01_FunctionalIdCode; }
            set { _GS01_FunctionalIdCode = value; }
        }

        string _GS02_SenderCode;
        public string GS02_SenderCode
        {
            get { return _GS02_SenderCode; }
            set { _GS02_SenderCode = value; }
        }

        string _GS03_ReceiverCode;
        public string GS03_ReceiverCode
        {
            get { return _GS03_ReceiverCode; }
            set { _GS03_ReceiverCode = value; }
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

        string _GS08_VersionIDCode;
        public string GS08_VersionIDCode
        {
            get { return _GS08_VersionIDCode; }
            set { _GS08_VersionIDCode = value; }
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

    public class Cls_277X212_GSs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_GSs()
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
        ~Cls_277X212_GSs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_GS item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_GS item)
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
        public Cls_277X212_GS this[int index]
        {
            get
            { return (Cls_277X212_GS)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_GS item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_GS item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_GS[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_ST
    {

        public Cls_277X212_ST()
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

        Cls_277X212_BHT _ST_BHT;
        public Cls_277X212_BHT ST_BHT
        {
            get { return _ST_BHT; }
            set { _ST_BHT = value; }
        }

        List<Cls_277X212_HL> _ST_HL;
        public List<Cls_277X212_HL> ST_HL
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

    public class Cls_277X212_STs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_STs()
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
        ~Cls_277X212_STs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_ST item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_ST item)
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
        public Cls_277X212_ST this[int index]
        {
            get
            { return (Cls_277X212_ST)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_ST item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_ST item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_ST[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_BHT
    {

        public Cls_277X212_BHT()
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

        string _BHT06_transactionTypeCode;
        public string BHT06_transactionTypeCode
        {
            get { return _BHT06_transactionTypeCode; }
            set { _BHT06_transactionTypeCode = value; }
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

    public class Cls_277X212_BHTs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_BHTs()
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
        ~Cls_277X212_BHTs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_BHT item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_BHT item)
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
        public Cls_277X212_BHT this[int index]
        {
            get
            { return (Cls_277X212_BHT)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_BHT item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_BHT item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_BHT[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_HL
    {

        public Cls_277X212_HL()
        {
        }

        public enum Level_Code
        {
            InformationSource = 1,
            InformationReceiver = 2,
            ProviderofService = 3,
            Patient = 4
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

        Cls_277X212_NM _HL_NM;
        public Cls_277X212_NM HL_NM
        {
            get { return _HL_NM; }
            set { _HL_NM = value; }
        }

        Cls_277X212_PER _HL_PER;
        public Cls_277X212_PER HL_PER
        {
            get { return _HL_PER; }
            set { _HL_PER = value; }
        }

        List<Cls_277X212_TRN> _HL_TRN;
        public List<Cls_277X212_TRN> HL_TRN
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

    public class Cls_277X212_HLs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_HLs()
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
        ~Cls_277X212_HLs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_HL item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_HL item)
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
        public Cls_277X212_HL this[int index]
        {
            get
            { return (Cls_277X212_HL)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_HL item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_HL item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_HL[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_NM
    {

        public Cls_277X212_NM()
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

        string _NM112_SecondLastName;
        public string NM112_SecondLastName
        {
            get { return _NM112_SecondLastName; }
            set { _NM112_SecondLastName = value; }
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

    public class Cls_277X212_NMs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_NMs()
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
        ~Cls_277X212_NMs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_NM item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_NM item)
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
        public Cls_277X212_NM this[int index]
        {
            get
            { return (Cls_277X212_NM)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_NM item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_NM item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_NM[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_PER
    {
        public Cls_277X212_PER()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _PER_ISAID;
        public long PER_ISAID
        {
            get { return _PER_ISAID; }
            set { _PER_ISAID = value; }
        }

        long _PER_STID;
        public long PER_STID
        {
            get { return _PER_STID; }
            set { _PER_STID = value; }
        }

        long _PER_HLID;
        public long PER_HLID
        {
            get { return _PER_HLID; }
            set { _PER_HLID = value; }
        }

        long _PERID;
        public long PERID
        {
            get { return _PERID; }
            set { _PERID = value; }
        }

        string _PER01_ContactFunctionCode;
        public string PER01_ContactFunctionCode
        {
            get { return _PER01_ContactFunctionCode; }
            set { _PER01_ContactFunctionCode = value; }
        }

        string _PER02_Name;
        public string PER02_Name
        {
            get { return _PER02_Name; }
            set { _PER02_Name = value; }
        }

        string _PER03_CommNumberQualifier;
        public string PER03_CommNumberQualifier
        {
            get { return _PER03_CommNumberQualifier; }
            set { _PER03_CommNumberQualifier = value; }
        }

        string _PER04_CommNumber;
        public string PER04_CommNumber
        {
            get { return _PER04_CommNumber; }
            set { _PER04_CommNumber = value; }
        }

        string _PER05_CommNumberQualifier;
        public string PER05_CommNumberQualifier
        {
            get { return _PER05_CommNumberQualifier; }
            set { _PER05_CommNumberQualifier = value; }
        }

        string _PER06_CommNumber;
        public string PER06_CommNumber
        {
            get { return _PER06_CommNumber; }
            set { _PER06_CommNumber = value; }
        }

        string _PER07_CommNumberQualifier;
        public string PER07_CommNumberQualifier
        {
            get { return _PER07_CommNumberQualifier; }
            set { _PER07_CommNumberQualifier = value; }
        }

        string _PER08_CommNumber;
        public string PER08_CommNumber
        {
            get { return _PER08_CommNumber; }
            set { _PER08_CommNumber = value; }
        }

        string _PER09_ContactInquiryReference;
        public string PER09_ContactInquiryReference
        {
            get { return _PER09_ContactInquiryReference; }
            set { _PER09_ContactInquiryReference = value; }
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

    public class Cls_277X212_PERs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_PERs()
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
        ~Cls_277X212_PERs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_PER item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_PER item)
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
        public Cls_277X212_PER this[int index]
        {
            get
            { return (Cls_277X212_PER)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_PER item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_PER item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_PER[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_TRN
    {

        public Cls_277X212_TRN()
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

        string _TRN01_TraceTypeCode;
        public string TRN01_TraceTypeCode
        {
            get { return _TRN01_TraceTypeCode; }
            set { _TRN01_TraceTypeCode = value; }
        }

        string _TRN02_ReferenceIdentification;
        public string TRN02_ReferenceIdentification
        {
            get { return _TRN02_ReferenceIdentification; }
            set { _TRN02_ReferenceIdentification = value; }
        }

        string _TRN03_OriginatingCompanyId;
        public string TRN03_OriginatingCompanyId
        {
            get { return _TRN03_OriginatingCompanyId; }
            set { _TRN03_OriginatingCompanyId = value; }
        }

        string _TRN04_ReferenceID;
        public string TRN04_ReferenceID
        {
            get { return _TRN04_ReferenceID; }
            set { _TRN04_ReferenceID = value; }
        }

        List<Cls_277X212_STC> _TRN_STC;
        public List<Cls_277X212_STC> TRN_STC
        {
            get { return _TRN_STC; }
            set { _TRN_STC = value; }
        }

        List<Cls_277X212_REF> _TRN_REF;
        public List<Cls_277X212_REF> TRN_REF
        {
            get { return _TRN_REF; }
            set { _TRN_REF = value; }
        }

        Cls_277X212_DTP _TRN_DTP;
        public Cls_277X212_DTP TRN_DTP
        {
            get { return _TRN_DTP; }
            set { _TRN_DTP = value; }
        }

        List<Cls_277X212_SVC> _TRN_SVC;
        public List<Cls_277X212_SVC> TRN_SVC
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

    public class Cls_277X212_TRNs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_TRNs()
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
        ~Cls_277X212_TRNs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_TRN item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_TRN item)
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
        public Cls_277X212_TRN this[int index]
        {
            get
            { return (Cls_277X212_TRN)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_TRN item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_TRN item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_TRN[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_SVC
    {
        public Cls_277X212_SVC()
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

        string _SVC01_1_ServiceIDQualifier;
        public string SVC01_1_ServiceIDQualifier
        {
            get { return _SVC01_1_ServiceIDQualifier; }
            set { _SVC01_1_ServiceIDQualifier = value; }
        }

        string _SVC01_2_ServiceID;
        public string SVC01_2_ServiceID
        {
            get { return _SVC01_2_ServiceID; }
            set { _SVC01_2_ServiceID = value; }
        }

        string _SVC01_3_Modifier;
        public string SVC01_3_Modifier
        {
            get { return _SVC01_3_Modifier; }
            set { _SVC01_3_Modifier = value; }
        }

        string _SVC01_4_Modifier;
        public string SVC01_4_Modifier
        {
            get { return _SVC01_4_Modifier; }
            set { _SVC01_4_Modifier = value; }
        }

        string _SVC01_5_Modifier;
        public string SVC01_5_Modifier
        {
            get { return _SVC01_5_Modifier; }
            set { _SVC01_5_Modifier = value; }
        }

        string _SVC01_6_Modifier;
        public string SVC01_6_Modifier
        {
            get { return _SVC01_6_Modifier; }
            set { _SVC01_6_Modifier = value; }
        }

        string _SVC01_7_Description;
        public string SVC01_7_Description
        {
            get { return _SVC01_7_Description; }
            set { _SVC01_7_Description = value; }
        }

        string _SVC01_8_ServiceId;
        public string SVC01_8_ServiceId
        {
            get { return _SVC01_8_ServiceId; }
            set { _SVC01_8_ServiceId = value; }
        }

        string _SVC02_Amount;
        public string SVC02_Amount
        {
            get { return _SVC02_Amount; }
            set { _SVC02_Amount = value; }
        }

        string _SVC03_Amount;
        public string SVC03_Amount
        {
            get { return _SVC03_Amount; }
            set { _SVC03_Amount = value; }
        }

        string _SVC04_ProductID;
        public string SVC04_ProductID
        {
            get { return _SVC04_ProductID; }
            set { _SVC04_ProductID = value; }
        }

        string _SVC05_Quantity;
        public string SVC05_Quantity
        {
            get { return _SVC05_Quantity; }
            set { _SVC05_Quantity = value; }
        }

        string _SVC06_1_ServiceIDQualifier;
        public string SVC06_1_ServiceIDQualifier
        {
            get { return _SVC06_1_ServiceIDQualifier; }
            set { _SVC06_1_ServiceIDQualifier = value; }
        }

        string _SVC06_2_ServiceID;
        public string SVC06_2_ServiceID
        {
            get { return _SVC06_2_ServiceID; }
            set { _SVC06_2_ServiceID = value; }
        }

        string _SVC06_3_Modifier;
        public string SVC06_3_Modifier
        {
            get { return _SVC06_3_Modifier; }
            set { _SVC06_3_Modifier = value; }
        }

        string _SVC06_4_Modifier;
        public string SVC06_4_Modifier
        {
            get { return _SVC06_4_Modifier; }
            set { _SVC06_4_Modifier = value; }
        }

        string _SVC06_5_Modifier;
        public string SVC06_5_Modifier
        {
            get { return _SVC06_5_Modifier; }
            set { _SVC06_5_Modifier = value; }
        }

        string _SVC06_6_Modifier;
        public string SVC06_6_Modifier
        {
            get { return _SVC06_6_Modifier; }
            set { _SVC06_6_Modifier = value; }
        }

        string _SVC06_7_Description;
        public string SVC06_7_Description
        {
            get { return _SVC06_7_Description; }
            set { _SVC06_7_Description = value; }
        }

        string _SVC06_8_ServiceId;
        public string SVC06_8_ServiceId
        {
            get { return _SVC06_8_ServiceId; }
            set { _SVC06_8_ServiceId = value; }
        }

        string _SVC07_Quantity;
        public string SVC07_Quantity
        {
            get { return _SVC07_Quantity; }
            set { _SVC07_Quantity = value; }
        }

        List<Cls_277X212_STC> _SVC_STC;
        public List<Cls_277X212_STC> SVC_STC
        {
            get { return _SVC_STC; }
            set { _SVC_STC = value; }
        }

        Cls_277X212_REF _SVC_REF;
        public Cls_277X212_REF SVC_REF
        {
            get { return _SVC_REF; }
            set { _SVC_REF = value; }
        }

        Cls_277X212_DTP _SVC_DTP;
        public Cls_277X212_DTP SVC_DTP
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

    public class Cls_277X212_SVCs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_SVCs()
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
        ~Cls_277X212_SVCs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_SVC item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_SVC item)
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
        public Cls_277X212_SVC this[int index]
        {
            get
            { return (Cls_277X212_SVC)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_SVC item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_SVC item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_SVC[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_STC
    {
        public Cls_277X212_STC()
        {
        }

        long _ERAFileID;
        public long ERAFileID
        {
            get { return _ERAFileID; }
            set { _ERAFileID = value; }
        }

        long _STC_ISAID;
        public long STC_ISAID
        {
            get { return _STC_ISAID; }
            set { _STC_ISAID = value; }
        }

        long _STC_STID;
        public long STC_STID
        {
            get { return _STC_STID; }
            set { _STC_STID = value; }
        }

        long _STC_HLID;
        public long STC_HLID
        {
            get { return _STC_HLID; }
            set { _STC_HLID = value; }
        }

        long _STC_TRNID;
        public long STC_TRNID
        {
            get { return _STC_TRNID; }
            set { _STC_TRNID = value; }
        }

        long _STC_SVCID;
        public long STC_SVCID
        {
            get { return _STC_SVCID; }
            set { _STC_SVCID = value; }
        }

        long _STCID;
        public long STCID
        {
            get { return _STCID; }
            set { _STCID = value; }
        }


        string _STC01_1_StatusCategoryCode;
        public string STC01_1_StatusCategoryCode
        {
            get { return _STC01_1_StatusCategoryCode; }
            set { _STC01_1_StatusCategoryCode = value; }
        }

        string _STC01_2_StatusCode;
        public string STC01_2_StatusCode
        {
            get { return _STC01_2_StatusCode; }
            set { _STC01_2_StatusCode = value; }
        }

        string _STC01_3_EntityIdentifierCode;
        public string STC01_3_EntityIdentifierCode
        {
            get { return _STC01_3_EntityIdentifierCode; }
            set { _STC01_3_EntityIdentifierCode = value; }
        }

        string _STC01_4_CodeListQualifier;
        public string STC01_4_CodeListQualifier
        {
            get { return _STC01_4_CodeListQualifier; }
            set { _STC01_4_CodeListQualifier = value; }
        }

        string _STC02_StatusInfoEffecticeDate;
        public string STC02_StatusInfoEffecticeDate
        {
            get { return _STC02_StatusInfoEffecticeDate; }
            set { _STC02_StatusInfoEffecticeDate = value; }
        }

        string _STC03_StatusInfoActionCode;
        public string STC03_StatusInfoActionCode
        {
            get { return _STC03_StatusInfoActionCode; }
            set { _STC03_StatusInfoActionCode = value; }
        }

        string _STC04_TotalClaimChargeAmount;
        public string STC04_TotalClaimChargeAmount
        {
            get { return _STC04_TotalClaimChargeAmount; }
            set { _STC04_TotalClaimChargeAmount = value; }
        }

        string _STC05_TotalClaimChargeAmount;
        public string STC05_TotalClaimChargeAmount
        {
            get { return _STC05_TotalClaimChargeAmount; }
            set { _STC05_TotalClaimChargeAmount = value; }
        }

        string _STC06_Date;
        public string STC06_Date
        {
            get { return _STC06_Date; }
            set { _STC06_Date = value; }
        }

        string _STC07_PaymentMethodCode;
        public string STC07_PaymentMethodCode
        {
            get { return _STC07_PaymentMethodCode; }
            set { _STC07_PaymentMethodCode = value; }
        }

        string _STC08_Date;
        public string STC08_Date
        {
            get { return _STC08_Date; }
            set { _STC08_Date = value; }
        }

        string _STC09_CheckNumber;
        public string STC09_CheckNumber
        {
            get { return _STC09_CheckNumber; }
            set { _STC09_CheckNumber = value; }
        }

        string _STC10_1_IndustryCode;
        public string STC10_1_IndustryCode
        {
            get { return _STC10_1_IndustryCode; }
            set { _STC10_1_IndustryCode = value; }
        }

        string _STC10_2_IndustryCode;
        public string STC10_2_IndustryCode
        {
            get { return _STC10_2_IndustryCode; }
            set { _STC10_2_IndustryCode = value; }
        }

        string _STC10_3_EntityIDCode;
        public string STC10_3_EntityIDCode
        {
            get { return _STC10_3_EntityIDCode; }
            set { _STC10_3_EntityIDCode = value; }
        }

        string _STC10_4_CodeListQualifierCode;
        public string STC10_4_CodeListQualifierCode
        {
            get { return _STC10_4_CodeListQualifierCode; }
            set { _STC10_4_CodeListQualifierCode = value; }
        }

        string _STC11_1_IndustryCode;
        public string STC11_1_IndustryCode
        {
            get { return _STC11_1_IndustryCode; }
            set { _STC11_1_IndustryCode = value; }
        }

        string _STC11_2_IndustryCode;
        public string STC11_2_IndustryCode
        {
            get { return _STC11_2_IndustryCode; }
            set { _STC11_2_IndustryCode = value; }
        }

        string _STC11_3_EntityIDCode;
        public string STC11_3_EntityIDCode
        {
            get { return _STC11_3_EntityIDCode; }
            set { _STC11_3_EntityIDCode = value; }
        }

        string _STC11_4_CodeListQualifierCode;
        public string STC11_4_CodeListQualifierCode
        {
            get { return _STC11_4_CodeListQualifierCode; }
            set { _STC11_4_CodeListQualifierCode = value; }
        }

        string _STC12_Message;
        public string STC12_Message
        {
            get { return _STC12_Message; }
            set { _STC12_Message = value; }
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

    public class Cls_277X212_STCs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_STCs()
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
        ~Cls_277X212_STCs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_STC item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_STC item)
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
        public Cls_277X212_STC this[int index]
        {
            get
            { return (Cls_277X212_STC)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_STC item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_STC item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_STC[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_REF
    {

        public Cls_277X212_REF()
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

        string _REF01_TypeCode;
        public string REF01_TypeCode
        {
            get { return _REF01_TypeCode; }
            set { _REF01_TypeCode = value; }
        }

        string _REF02_ID;
        public string REF02_ID
        {
            get { return _REF02_ID; }
            set { _REF02_ID = value; }
        }

        string _REF03_Description;
        public string REF03_Description
        {
            get { return _REF03_Description; }
            set { _REF03_Description = value; }
        }

        string _REF04_1_ReferenceIDQualifier;
        public string REF04_1_ReferenceIDQualifier
        {
            get { return _REF04_1_ReferenceIDQualifier; }
            set { _REF04_1_ReferenceIDQualifier = value; }
        }

        string _REF04_2_ReferenceID;
        public string REF04_2_ReferenceID
        {
            get { return _REF04_2_ReferenceID; }
            set { _REF04_2_ReferenceID = value; }
        }

        string _REF04_3_ReferenceIDQualifier;
        public string REF04_3_ReferenceIDQualifier
        {
            get { return _REF04_3_ReferenceIDQualifier; }
            set { _REF04_3_ReferenceIDQualifier = value; }
        }

        string _REF04_4_ReferenceID;
        public string REF04_4_ReferenceID
        {
            get { return _REF04_4_ReferenceID; }
            set { _REF04_4_ReferenceID = value; }
        }

        string _REF04_5_ReferenceIDQualifier;
        public string REF04_5_ReferenceIDQualifier
        {
            get { return _REF04_5_ReferenceIDQualifier; }
            set { _REF04_5_ReferenceIDQualifier = value; }
        }

        string _REF04_6_ReferenceID;
        public string REF04_6_ReferenceID
        {
            get { return _REF04_6_ReferenceID; }
            set { _REF04_6_ReferenceID = value; }
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

    public class Cls_277X212_REFs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_REFs()
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
        ~Cls_277X212_REFs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_REF item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_REF item)
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
        public Cls_277X212_REF this[int index]
        {
            get
            { return (Cls_277X212_REF)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_REF item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_REF item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_REF[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }


    public class Cls_277X212_DTP
    {
        public Cls_277X212_DTP()
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

        string _DTP01_DateQualifier;
        public string DTP01_DateQualifier
        {
            get { return _DTP01_DateQualifier; }
            set { _DTP01_DateQualifier = value; }
        }

        string _DTP02_DateFormatQualifier;
        public string DTP02_DateFormatQualifier
        {
            get { return _DTP02_DateFormatQualifier; }
            set { _DTP02_DateFormatQualifier = value; }
        }

        string _DTP03_Date;
        public string DTP03_Date
        {
            get { return _DTP03_Date; }
            set { _DTP03_Date = value; }
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

    public class Cls_277X212_DTPs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public Cls_277X212_DTPs()
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
        ~Cls_277X212_DTPs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Cls_277X212_DTP item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Cls_277X212_DTP item)
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
        public Cls_277X212_DTP this[int index]
        {
            get
            { return (Cls_277X212_DTP)_innerlist[index]; }
        }
        public bool Contains(Cls_277X212_DTP item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Cls_277X212_DTP item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Cls_277X212_DTP[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }
}
