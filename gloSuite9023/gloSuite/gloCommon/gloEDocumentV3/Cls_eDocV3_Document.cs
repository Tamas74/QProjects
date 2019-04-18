using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using gloEDocumentV3.Enumeration;

namespace gloEDocumentV3
{
    namespace Document
    {
        #region "Document Container"

        public partial class eBaseContainer : IDisposable
        {
            #region "Constructor & Distructor"

            public eBaseContainer()
            {
            }

            public eBaseContainer(Int64 edocumentid, Int64 econtainerid, int pagefrom, int pageto, string documentextension, Int64 clinicid)
            {
                _eDocumentID = edocumentid;
                _eContainerID = econtainerid;
                _PageFrom = pagefrom;
                _PageTo = pageto;
                _DocumentExtension = documentextension;
                _ClinicID = clinicid;
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

            ~eBaseContainer()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            private Int64 _eDocumentID = 0;
            private string _DocumentExtension = "";
            private Int64 _ClinicID = 0;
            private int _PageTo = 0;
            private int _PageFrom = 0;
            private Int64 _eContainerID = 0;
            #endregion

            #region "Property Procedures "

            public Int64 EDocumentID
            {
                get { return _eDocumentID; }
                set { _eDocumentID = value; }
            }

            public Int64 EContainerID
            {
                get { return _eContainerID; }
                set { _eContainerID = value; }
            }


            public int PageFrom
            {
                get { return _PageFrom; }
                set { _PageFrom = value; }
            }

            public int PageTo
            {
                get { return _PageTo; }
                set { _PageTo = value; }
            }

            public string DocumentExtension
            {
                get { return _DocumentExtension; }
                set { _DocumentExtension = value; }
            }


            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }


            #endregion

        }

        public partial class eBaseContainers : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public eBaseContainers()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "eBaseContainers";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
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


            ~eBaseContainers()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
               // get { return _innerlist.Count; }
                get
                {
                    if (_innerlist != null)
                    {

                        return _innerlist.Count;

                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            public void Add(eBaseContainer item)
            {
               // _innerlist.Add(item);
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public void Add(Int64 edocumentid, Int64 econtainerid, int pagefrom, int pageto, string documentextension, Int64 clinicid)
            {
               
                if (_innerlist != null)
                {
                    using (eBaseContainer _item = new eBaseContainer(edocumentid, econtainerid, pagefrom, pageto, documentextension, clinicid))
                    {

                        if (_item != null)
                        {
                            _innerlist.Add(_item);
                        }
                    }
                }
            }

            public bool Remove(eBaseContainer item)
            {

                bool result = false;
                eBaseContainer obj;
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new eBaseContainer())
                        {
                            if (obj != null)
                            {
                                obj = (eBaseContainer)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.EContainerID == item.EContainerID)
                                    {
                                        _innerlist.RemoveAt(i);
                                        result = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //obj = null;
                }

                return result;

            }

            public bool RemoveAt(int index)
            {

                bool result = false;
                if (_innerlist != null)
                {
                    if (index < 0 || index >= _innerlist.Count)
                    {
                        result = false;
                    }
                    else
                    {
                        _innerlist.RemoveAt(index);
                        result = true;
                    }
                }


                return result;
            }

            public void Clear()
            {
                //_innerlist.Clear();

                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public eBaseContainer this[int index]
            {

                get
                {
                    if (_innerlist != null)
                    {
                        if (index < 0 || index >= _innerlist.Count)
                        {
                            return null;
                        }
                        else
                        {
                            return (eBaseContainer)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(eBaseContainer item)
            {
                //return _innerlist.Contains(item);

                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(eBaseContainer item)
            {
               // return _innerlist.IndexOf(item);
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(eBaseContainer[] array, int index)
            {
                //_innerlist.CopyTo(array, index);

                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }

        public partial class eContainer : eBaseContainer, IDisposable
        {
            #region "Constructor & Distructor"

            public eContainer(): base()
            {
            }

            private bool disposed = false;

            public void Disposer()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected override void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
                base.Dispose(disposing);
            }

            ~eContainer()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"

            private bool _IsModified = false;
            private string _MachineID = "";
            private string _SourceMachine = "";
            private enum_DocumentSourceBin _SourceBin = 0;

            #endregion

            #region "Properties"

            public bool IsModified
            {
                get { return _IsModified; }
                set { _IsModified = value; }
            }

            public enum_DocumentSourceBin SourceBin
            {
                get { return _SourceBin; }
                set { _SourceBin = value; }
            }

            public string SourceMachine
            {
                get { return _SourceMachine; }
                set { _SourceMachine = value; }
            }

            public string MachineID
            {
                get { return _MachineID; }
                set { _MachineID = value; }
            }

            #endregion
        }

        public partial class ExportDocument : IDisposable
        {
            #region "Constructor & Distructor"

            public ExportDocument()
            {
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

            ~ExportDocument()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            private string _DocumentPath = "";
            private string _Documentname = "";
            private string _DocumentSelect = "";
            private string _DocumentType = "";
            private string _DocumentHiddenPath = "";
            private string _DocumentHiddenPageNo = "";
            private string _PageName = "";


            #endregion

            #region "Properties"
            public string DocumentPath
            {
                get { return _DocumentPath; }
                set { _DocumentPath = value; }
            }

            public string Documentname
            {
                get { return _Documentname; }
                set { _Documentname = value; }
            }

            public string DocumentSelect
            {
                get { return _DocumentSelect; }
                set { _DocumentSelect = value; }
            }

            public string DocumentType
            {
                get { return _DocumentType; }
                set { _DocumentType = value; }
            }

            public string DocumentHiddenPath
            {
                get { return _DocumentHiddenPath; }
                set { _DocumentHiddenPath = value; }
            }

            public string DocumentHiddenPageNo
            {
                get { return _DocumentHiddenPageNo; }
                set { _DocumentHiddenPageNo = value; }
            }
            public string PageName
            {
                get { return _PageName; }
                set { _PageName = value; }
            }
            #endregion

        }
        public partial class Documents : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Documents()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "Documents";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
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


            ~Documents()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                //get { return _innerlist.Count; }

                get
                {
                    if (_innerlist != null)
                    {

                        return _innerlist.Count;

                    }
                    else
                    {
                        return -1;
                    }
                }

            }

            public void Add(ExportDocument item)
            {
               // _innerlist.Add(item);
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }


            public bool Remove(ExportDocument item)
            {
                
                bool result = false;
                ExportDocument obj;
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new ExportDocument())
                        {
                            if (obj != null)
                            {
                                obj = (ExportDocument)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.Documentname == item.Documentname)
                                    {
                                        _innerlist.RemoveAt(i);
                                        result = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                return result;

            }

            public bool RemoveAt(int index)
            {

                bool result = false;
                if (_innerlist != null)
                {
                    if (index < 0 || index >= _innerlist.Count)
                    {
                        result = false;
                    }
                    else
                    {
                        _innerlist.RemoveAt(index);
                        result = true;
                    }
                }
                return result;
            }

            public void Clear()
            {
                //_innerlist.Clear();
                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public ExportDocument this[int index]
            {
                get
                {
                    if (_innerlist != null)
                    {
                        if (index < 0 || index >= _innerlist.Count)
                        {
                            return null;
                        }
                        else
                        {
                            return (ExportDocument)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(ExportDocument item)
            {
                //return _innerlist.Contains(item);
                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(ExportDocument item)
            {
              // return _innerlist.IndexOf(item);
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(ExportDocument[] array, int index)
            {
               // _innerlist.CopyTo(array, index);

                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }



        #endregion

        #region "Base Document"

        public partial class BaseDocument : IDisposable
        {
            #region "Constructor & Distructor"

            public BaseDocument()
            {
                _eContainers = new eBaseContainers();
                if (_eContainers == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "BaseDocument";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
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

            ~BaseDocument()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            private Int64 _eDocumentID = 0;
            private eBaseContainers _eContainers = null;
            private string _DocumentName = "";
            private int _CategoryID = 0;
            private string _Category = "";
            private string _SubCategory = "";
            private Int64 _PatientID = 0;
            private string _Year = "";
            private string _Month = "";
            private int _PageCounts = 0;
            private DateTime _CreatedDateTime = DateTime.Now;
            private DateTime _ModifiedDateTime = DateTime.Now;
            private bool _IsAcknowledge = false;
            private bool _HasNote = false;
            private bool _IsCompressed = false;
            private Int64 _ClinicID = 0;
            #endregion

            #region " Property Procedures "

            public Int64 EDocumentID
            {
                get { return _eDocumentID; }
                set { _eDocumentID = value; }
            }

            public eBaseContainers EContainers
            {
                get { return _eContainers; }
                set { _eContainers = value; }
            }

            public string DocumentName
            {
                get { return _DocumentName; }
                set { _DocumentName = value; }
            }

            public int CategoryID
            {
                get { return _CategoryID; }
                set { _CategoryID = value; }
            }

            public string Category
            {
                get { return _Category; }
                set { _Category = value; }
            }

            public string SubCategory
            {
                get { return _SubCategory; }
                set { _SubCategory = value; }
            }

            public Int64 PatientID
            {
                get { return _PatientID; }
                set { _PatientID = value; }
            }

            public string Year
            {
                get { return _Year; }
                set { _Year = value; }
            }

            public string Month
            {
                get { return _Month; }
                set { _Month = value; }
            }

            public int PageCounts
            {
                get { return _PageCounts; }
                set { _PageCounts = value; }
            }
            
            public DateTime CreatedDateTime
            {
                get { return _CreatedDateTime; }
                set { _CreatedDateTime = value; }
            }
            
            public DateTime ModifiedDateTime
            {
                get { return _ModifiedDateTime; }
                set { _ModifiedDateTime = value; }
            }
            
            public bool IsAcknowledge
            {
                get { return _IsAcknowledge; }
                set { _IsAcknowledge = value; }
            }
            
            public bool HasNote
            {
                get { return _HasNote; }
                set { _HasNote = value; }
            }
            
            public bool IsCompressed
            {
                get { return _IsCompressed; }
                set { _IsCompressed = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            #endregion

            public override string ToString()
            {
                return _DocumentName;
            }
        }

        public partial class BaseDocuments : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public BaseDocuments()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "BaseDocuments";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }

            private bool disposed = false;

            public void Dispose()
            {
                if (_innerlist != null)
                {

                    _innerlist = null;
                }
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


            ~BaseDocuments()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
              //  get { return _innerlist.Count; }
                get
                {
                    if (_innerlist != null)
                    {

                        return _innerlist.Count;

                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            public void Add(BaseDocument item)
            {
               // _innerlist.Add(item);
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }

            }

            public bool Remove(BaseDocument item)
            {

                bool result = false;
                BaseDocument obj;
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new BaseDocument())
                        {
                            if (obj != null)
                            {
                                
                                obj = (BaseDocument)_innerlist[i];

                                if (obj != null)
                                {
                                    if (obj.EDocumentID == item.EDocumentID)
                                    {
                                        _innerlist.RemoveAt(i);
                                        result = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }

                return result;
            }

            public bool RemoveAt(int index)
            {

                bool result = false;
                if (_innerlist != null)
                {
                    if (index < 0 || index >= _innerlist.Count)
                    {
                        result = false;
                    }
                    else
                    {
                        _innerlist.RemoveAt(index);
                        result = true;
                    }
                }
                return result;
            }

            public void Clear()
            {
               // _innerlist.Clear();
                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }

            }

            public BaseDocument this[int index]
            {

                get
                {
                    if (_innerlist != null)
                    {
                        if (index < 0 || index >= _innerlist.Count)
                        {
                            return null;
                        }
                        else
                        {
                            return (BaseDocument)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(BaseDocument item)
            {
               // return _innerlist.Contains(item);

                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(BaseDocument item)
            {
               // return _innerlist.IndexOf(item);
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(BaseDocument[] array, int index)
            {
               // _innerlist.CopyTo(array, index);

                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }

        #endregion

        #region "e Document"
        public partial class eDocument : BaseDocument, IDisposable
        {

            #region "Constructor & Distructor"

            public eDocument()
                : base()
            {
                _Acknowledges  = new gloEDocumentV3.Common.NTAOs();
                _UserTags = new gloEDocumentV3.Common.NTAOs();
                _Pages = new Pages();
                if (_Acknowledges == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "eDocument";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
                if (_UserTags == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "eDocument";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
                if (_Pages == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "eDocument";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }

            private bool disposed = false;

            public void Disposer()
            {
                
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected override void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                       
                    }
                }
                disposed = true;
                base.Dispose(disposing);
            }

            ~eDocument()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            private Int64 _ExternalID = 0;
            private string _ExternalCode = "";
            private string _ExternalDescription = "";
            private bool _UsedStatus = false;
            private string _UsedMachine = "";
            private Int64 _ArchiveID = 0;
            private bool _ArchiveStatus = false;
            private string _ArchiveDescription = "";
            private Int64 _DMSV1ID = 0;
            private Int64 _DMSV2ContainerID = 0;
            private Int64 _DMSV2DocumentID = 0;
            private Common.NTAOs _Acknowledges = null;
            private Common.NTAOs _UserTags = null;
            private Pages _Pages = null;
            #endregion 

            #region "Property Procedures"

            public Int64 ExternalID
            {
                get { return _ExternalID; }
                set { _ExternalID = value; }
            }
            
            public string ExternalCode
            {
                get { return _ExternalCode; }
                set { _ExternalCode = value; }
            }
            
            public string ExternalDescription
            {
                get { return _ExternalDescription; }
                set { _ExternalDescription = value; }
            }
            
            public bool UsedStatus
            {
                get { return _UsedStatus; }
                set { _UsedStatus = value; }
            }
            
            public string UsedMachine
            {
                get { return _UsedMachine; }
                set { _UsedMachine = value; }
            }
            
            public Int64 ArchiveID
            {
                get { return _ArchiveID; }
                set { _ArchiveID = value; }
            }
            
            public bool ArchiveStatus
            {
                get { return _ArchiveStatus; }
                set { _ArchiveStatus = value; }
            }
            
            public string ArchiveDescription
            {
                get { return _ArchiveDescription; }
                set { _ArchiveDescription = value; }
            }

            
            public Int64 DMSV1ID
            {
                get { return _DMSV1ID; }
                set { _DMSV1ID = value; }
            }
            
            public Int64 DMSV2ContainerID
            {
                get { return _DMSV2ContainerID; }
                set { _DMSV2ContainerID = value; }
            }
            
            public Int64 DMSV2DocumentID
            {
                get { return _DMSV2DocumentID; }
                set { _DMSV2DocumentID = value; }
            }

            
            public Common.NTAOs Acknowledges
            {
                get { return _Acknowledges; }
                set { _Acknowledges = value; }
            }
            
            public Common.NTAOs UserTags
            {
                get { return _UserTags; }
                set { _UserTags = value; }
            }
         
            public Pages Pages
            {
                get { return _Pages; }
                set { _Pages = value; }
            }
            #endregion

         
        }

        public partial class eDocuments : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public eDocuments()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "eDocuments";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }

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


            ~eDocuments()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                //get { return _innerlist.Count; }
                get
                {
                    if (_innerlist != null)
                    {
                        return _innerlist.Count;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            public void Add(eDocument item)
            {
                //_innerlist.Add(item);
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public bool Remove(eDocument item)
            {

                bool result = false;
                eDocument obj;
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new eDocument())
                        {
                            if (obj != null)
                            {
                                obj = (eDocument)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.EDocumentID == item.EDocumentID)
                                    {
                                        _innerlist.RemoveAt(i);
                                        result = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }

                return result;
            }

            public bool RemoveAt(int index)
            {
               
                bool result = false;
                if (_innerlist != null)
                {
                    if (index < 0 || index >= _innerlist.Count)
                    {
                        result = false;
                    }
                    else
                    {
                        _innerlist.RemoveAt(index);
                        result = true;
                    }
                }

                return result;
            }

            public void Clear()
            {
                //_innerlist.Clear();
                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public eDocument this[int index]
            {
               
                get
                {
                    if (_innerlist != null)
                    {
                        if (index < 0 || index >= _innerlist.Count)
                        {
                            return null;
                        }
                        else
                        {
                            return (eDocument)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(eDocument item)
            {
               // return _innerlist.Contains(item);
                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }

                else
                {
                    return false;
                }
            }

            public int IndexOf(eDocument item)
            {
                //return _innerlist.IndexOf(item);
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }

            }

            public void CopyTo(eDocument[] array, int index)
            {
               // _innerlist.CopyTo(array, index);
                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }
        #endregion

        #region "Document Page"

        public partial class BasePage : IDisposable
        {
            #region "Constructor & Distructor"

            public BasePage()
            {
            }

            public BasePage(Int64 documentid,Int64 containerid, Int32 containerpagenumber, Int32 documentpagenumber, string pagename, string bookmarktag, bool hasnotes, Int64 clinicid)
            {
                _DocumentID = documentid;
                _ContainerID = containerid;
                _ContainerPageNumber = containerpagenumber;
                _DocumentPageNumber = documentpagenumber;
                _PageName = pagename;
                _BookMarkTag = bookmarktag;
                _HasNotes = hasnotes;
                _ClinicID = clinicid;
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

            ~BasePage()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            private Int64 _DocumentID = 0;
            private Int64 _ContainerID = 0;
            private Int32 _ContainerPageNumber = 0;
            private Int32 _DocumentPageNumber = 0;
            private string _PageName = "";
            private string _BookMarkTag = "";
            private bool _HasNotes = false;
            private Int64 _ClinicID = 0;
            #endregion

            #region "Properties"
            public Int64 DocumentID
            {
                get { return _DocumentID; }
                set { _DocumentID = value; }
            }

            public Int64 ContainerID
            {
                get { return _ContainerID; }
                set { _ContainerID = value; }
            }

            public Int32 ContainerPageNumber
            {
                get { return _ContainerPageNumber; }
                set { _ContainerPageNumber = value; }
            }

            public Int32 DocumentPageNumber
            {
                get { return _DocumentPageNumber; }
                set { _DocumentPageNumber = value; }
            }

            public string PageName
            {
                get { return _PageName; }
                set { _PageName = value; }
            }

            public string BookMarkTag
            {
                get { return _BookMarkTag; }
                set { _BookMarkTag = value; }
            }

            public bool HasNotes
            {
                get { return _HasNotes; }
                set { _HasNotes = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }
            #endregion

            public override string ToString()
            {
                return _PageName;
            }
        }

        public partial class BasePages : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public BasePages()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "BasePages";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
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


            ~BasePages()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                //get { return _innerlist.Count; }
                get
                {
                    if (_innerlist != null)
                    {

                        return _innerlist.Count;

                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            public void Add(BasePage item)
            {
               // _innerlist.Add(item);
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public void Add(Int64 documentid,Int64 containerid, Int32 containerpagenumber, Int32 documentpagenumber, string pagename, string bookmarktag, bool hasnotes, Int64 clinicid)
            {
                
                if (_innerlist != null)
                {
                    using (BasePage _item = new BasePage(documentid, containerid, containerpagenumber, documentpagenumber, pagename, bookmarktag, hasnotes, clinicid))
                    {

                        if (_item != null)
                        {
                            _innerlist.Add(_item);
                        }
                    }
                }
            }

            public bool Remove(BasePage item)
            {
                
                bool result = false;
                BasePage obj;
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new BasePage())
                        {
                            if (obj != null)
                            {
                                obj = (BasePage)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.ContainerID == item.ContainerID && obj.DocumentPageNumber == item.DocumentPageNumber)
                                    {
                                        _innerlist.RemoveAt(i);
                                        result = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }

                return result;

            }

            public bool RemoveAt(int index)
            {
               
                bool result = false;
                if (_innerlist != null)
                {
                    if (index < 0 || index >= _innerlist.Count)
                    {
                        result = false;
                    }
                    else
                    {
                        _innerlist.RemoveAt(index);
                        result = true;
                    }
                }

                return result;
            }

            public void Clear()
            {
              //  _innerlist.Clear();
                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public BasePage this[int index]
            {
               
                get
                {
                    if (_innerlist != null)
                    {
                        if (index < 0 || index >= _innerlist.Count)
                        {
                            return null;
                        }
                        else
                        {
                            return (BasePage)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(BasePage item)
            {
                //return _innerlist.Contains(item);
                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(BasePage item)
            {
                //return _innerlist.IndexOf(item);
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(BasePage[] array, int index)
            {
                //_innerlist.CopyTo(array, index);
                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }

        public partial class Page : BasePage, IDisposable
        {
            #region "Constructor & Distructor"

            public Page()
                : base()
            {
                _Notes = new gloEDocumentV3.Common.NTAOs();
                if (_Notes == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "Page";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
                _UserTags = new gloEDocumentV3.Common.NTAOs();
                if (_UserTags == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "Page";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }

            private bool disposed = false;

            public void Disposer()
            {
              
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected override void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        
                    }
                }
                disposed = true;
                base.Dispose(disposing);
            }

            ~Page()
            {
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            private Common.NTAOs _Notes = null;
            private Common.NTAOs _UserTags = null;
            #endregion

            #region "Properties"

          
            public Common.NTAOs Notes
            {
                get { return _Notes; }
                set { _Notes = value; }
            }

            public Common.NTAOs UserTags
            {
                get { return _UserTags; }
                set { _UserTags = value; }
            }
            #endregion

            public override string ToString()
            {
                return base.ToString();

            }
        }

        public partial class Pages : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Pages()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "Pages";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
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


            ~Pages()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
               // get { return _innerlist.Count; }

                get
                {
                    if (_innerlist != null)
                    {

                        return _innerlist.Count;

                    }
                    else
                    {
                        return -1;
                    }
                }

            }

            public void Add(Page item)
            {
               // _innerlist.Add(item);
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public bool Remove(Page item)
            {

                bool result = false;
                Page obj;
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new Page())
                        {
                            if (obj != null)
                            {
                                obj = (Page)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.ContainerID == item.ContainerID && obj.ContainerPageNumber == item.ContainerPageNumber)
                                    {
                                        _innerlist.RemoveAt(i);
                                        result = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }

                return result;
            }

            public bool RemoveAt(int index)
            {
                
                bool result = false;
                if (_innerlist != null)
                {
                    if (index < 0 || index >= _innerlist.Count)
                    {
                        result = false;
                    }
                    else
                    {
                        _innerlist.RemoveAt(index);
                        result = true;
                    }
                }

                return result;
            }

            public void Clear()
            {
                //_innerlist.Clear();
                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public Page this[int index]
            {
               
                get
                {
                    if (_innerlist != null)
                    {
                        if (index < 0 || index >= _innerlist.Count)
                        {
                            return null;
                        }
                        else
                        {
                            return (Page)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
               
            }

            public bool Contains(Page item)
            {
                //return _innerlist.Contains(item);
                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(Page item)
            {
                //return _innerlist.IndexOf(item);
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(Page[] array, int index)
            {
               // _innerlist.CopyTo(array, index);
                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }

        #endregion
   }
}
