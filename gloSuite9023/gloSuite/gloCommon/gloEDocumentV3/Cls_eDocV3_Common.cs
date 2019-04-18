using System;
using System.Collections;
using gloEDocumentV3.Enumeration;
using gloSettings;

namespace gloEDocumentV3
{
    namespace Common
    {
        #region " NTAO "

        public partial class NTAO : IDisposable
        {
            #region "Constructor & Distructor"

            public NTAO()
            {
            }

            public NTAO(Int64 documentid,string documentname, Int64 containerid, Int32 containerpagenumber, Int32 documentpagenumber, string pagename, Int64 ntaoid, Int64 userid, string username, DateTime ntaodatetime, string ntaodescription, bool ispage, enum_NTAOType ntaotype, Int64 clinicid)
            {
                _DocumentID = documentid;
                _DocumentName = documentname;
                _ContainerID = containerid;
                _ContainerPageNumber = containerpagenumber;
                _DocumentPageNumber = documentpagenumber;
                _PageName = pagename;
                _NTAOID = ntaoid;
                _UserID = userid;
                _UserName = username;
                _NTAODateTime = ntaodatetime;
                _NTAODescription = ntaodescription;
                _IsPage = ispage;
                _NTAOType = ntaotype;
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

            ~NTAO()
            {
               
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            private Int64 _DocumentID = 0;
            private string _DocumentName = "";
            private Int64 _ContainerID = 0;
            private Int32 _ContainerPageNumber = 0;
            private Int32 _DocumentPageNumber = 0;
            private string _PageName = "";
            private Int64 _NTAOID = 0;
            private Int64 _UserID = 0;
            private string _UserName = "";
            private DateTime _NTAODateTime = DateTime.Now;
            private string _NTAODescription = "";
            private bool _IsPage = false;
            private enum_NTAOType _NTAOType = enum_NTAOType.None;
            private Int64 _ClinicID = 0;
            #endregion

            #region "Properties"

            public Int64 DocumentID
            {
                get { return _DocumentID; }
                set { _DocumentID = value; }
            }

            public string DocumentName
            {
                get { return _DocumentName; }
                set { _DocumentName = value; }
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

            public Int64 NTAOID
            {
                get { return _NTAOID; }
                set { _NTAOID = value; }
            }

            public Int64 UserID
            {
                get { return _UserID; }
                set { _UserID = value; }
            }

            public string UserName
            {
                get { return _UserName; }
                set { _UserName = value; }
            }

            public DateTime NTAODateTime
            {
                get { return _NTAODateTime; }
                set { _NTAODateTime = value; }
            }

            public string NTAODescription
            {
                get { return _NTAODescription; }
                set { _NTAODescription = value; }
            }

            public bool IsPage
            {
                get { return _IsPage; }
                set { _IsPage = value; }
            }

            public enum_NTAOType NTAOType
            {
                get { return _NTAOType; }
                set { _NTAOType = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            #endregion

            public override string ToString()
            {
                return _NTAODescription;
            }
        }

        public partial class NTAOs : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public NTAOs()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "NTAO";
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


            ~NTAOs()
            {
                
                Dispose(false);
            }
            #endregion

            public int Count
            {
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

            public void Add(NTAO item)
            {
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public void Add(Int64 documentid, string documentname, Int64 containerid, Int32 containerpagenumber, Int32 documentpagenumber, string pagename,Int64 ntaoid, Int64 userid, string username, DateTime ntaodatetime, string ntaodescription, bool ispage, enum_NTAOType ntaotype, Int64 clinicid)
            {
                if (_innerlist != null)
                {
                    using (NTAO _item = new NTAO(documentid, documentname, containerid, containerpagenumber, documentpagenumber, pagename, ntaoid, userid, username, ntaodatetime, ntaodescription, ispage, ntaotype, clinicid))
                    {

                        if (_item != null)
                        {
                            _innerlist.Add(_item);
                        }
                    }
                }
                
            }

            public bool Remove(NTAO item)
            {
                bool result = false;
                NTAO obj;
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new NTAO())
                        {
                            if (obj != null)
                            {
                                obj = (NTAO)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.NTAOID == item.NTAOID)
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
                    if (index < 0 ||  index >= _innerlist.Count )
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
                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public NTAO this[int index]
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
                            return (NTAO)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public bool Contains(NTAO item)
            {
                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(NTAO item)
            {
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(NTAO[] array, int index)
            {
                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }

            }
        } 

        #endregion

        #region "Category"

        public partial class Category : IDisposable
        {
            #region "Constructor & Distructor"

            public Category()
            {
            }

            public Category(Int32 CategoryID, string CategoryName)
            {
                _CategoryID = CategoryID;
                _CategoryName = CategoryName;
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

            ~Category()
            {
               
                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            private Int64 _CategoryID = 0;
            private string _CategoryName = "";
            #endregion

            #region "Properties"
            public Int64 CategoryID
            {
                get { return _CategoryID; }
                set { _CategoryID = value; }
            }

            public string CategoryName
            {
                get { return _CategoryName; }
                set { _CategoryName = value; }
            }
            #endregion

            public override string ToString()
            {
                return _CategoryName;
            }

        }

        public partial class Categories : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Categories()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "NTAO";
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


            ~Categories()
            {
                
                Dispose(false);
            }
            #endregion

            public int Count
            {
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

            public void Add(Category item)
            {
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public void Add(Int32 CategoryID, string CategoryName)
            {
                if (_innerlist != null)
                {
                    using (Category oItem = new Category(CategoryID, CategoryName))
                    {

                        if (oItem != null)
                        {
                            _innerlist.Add(oItem);
                        }
                    }
                }
                
            }

            public bool Remove(Category item)
            {
                bool result = false;
                Category obj;
                
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new Category())
                        {
                            if (obj != null)
                            {
                                obj = (Category)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.CategoryID == item.CategoryID)
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
                    if (index < 0 || index >= _innerlist.Count )
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

                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public Category this[int index]
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
                           return (Category)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }

              
            }

            public bool Contains(Category item)
            {
                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(Category item)
            {
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(Category[] array, int index)
            {
                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }

        #endregion

        #region "SubCategory"

        public partial class SubCategory : IDisposable
        {
            #region "Constructor & Distructor"

            public SubCategory()
            {
            }

            public SubCategory(string SubCategoryName)
            {
                _SubCategoryName = SubCategoryName;
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

            ~SubCategory()
            {

                Dispose(false);
            }

            #endregion

            #region "Private Variables"
            
            private string _SubCategoryName = "";
            #endregion

            #region "Properties"
            
            public string SubCategoryName
            {
                get { return _SubCategoryName; }
                set { _SubCategoryName = value; }
            }
            #endregion

            public override string ToString()
            {
                return _SubCategoryName;
            }

        }

        public partial class SubCategories : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public SubCategories()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "NTAO";
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


            ~SubCategories()
            {

                Dispose(false);
            }
            #endregion

            public int Count
            {
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

            public void Add(SubCategory item)
            {
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public void Add(string SubCategoryName)
            {
                if (_innerlist != null)
                {
                    using (SubCategory oItem = new SubCategory(SubCategoryName))
                    {

                        if (oItem != null)
                        {
                            _innerlist.Add(oItem);
                        }
                    }
                }

            }

            public bool Remove(SubCategory item)
            {
                bool result = false;
                SubCategory obj;

                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new SubCategory())
                        {
                            if (obj != null)
                            {
                                obj = (SubCategory)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.SubCategoryName == item.SubCategoryName)
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

                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public SubCategory this[int index]
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
                            return (SubCategory)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }


            }

            public bool Contains(SubCategory item)
            {
                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(SubCategory item)
            {
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(SubCategory[] array, int index)
            {
                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }

        #endregion

        namespace Delete
        {
            public partial class Document : IDisposable
            {
                #region "Constructor & Distructor"

                public Document()
                {
                    _Pages = new Pages();
                    if (_Pages == null)
                    {
                        string _ErrorMessage = "Insuffucient Memory . " + "Pages";
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                    }
                }

                public Document(Int64 documentid, string category, Int64 patientid, string year, string month, Int64 clinicid, Pages pages)
                {
                    _Pages = new Pages();
                    _DocumentID = documentid;
                    _Category = category;
                    _PatientID = patientid;
                    _Year = year;
                    _Month = month;
                    _ClinicID = clinicid;
                    _Pages = pages;
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
                            if (_Pages != null) 
                            {
                                _Pages.Dispose();
                                _Pages = null;
                            }
                        }
                    }
                    disposed = true;
                }

                ~Document()
                {
                    
                    
                    Dispose(false);
                }

                #endregion

                #region "Private Variables"
                private Int64 _DocumentID = 0;
                private string _Category = "";
                private Int64 _PatientID = 0;
                private string _Year = "";
                private string _Month = "";
                private Int64 _ClinicID = 0;
                private Pages _Pages = null;
                #endregion

                #region "Properties"
                public Int64 DocumentID
                {
                    get { return _DocumentID; }
                    set { _DocumentID = value; }
                }

                public string Category
                {
                    get { return _Category; }
                    set { _Category = value; }
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

                public Int64 ClinicID
                {
                    get { return _ClinicID; }
                    set { _ClinicID = value; }
                }

                public Pages DocumentPages
                {
                    get { return _Pages; }
                    set { _Pages = value; }
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
                        string _ErrorMessage = "Insuffucient Memory . " + "Document";
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

                public void Add(Document item)
                {
                    if (_innerlist != null)
                    {
                        _innerlist.Add(item);
                    }
                }

                public void Add(Int64 documentid, string category, Int64 patientid, string year, string month, Int64 clinicid, Pages pages)
                {
                    if (_innerlist != null)
                    {
                        using (Document _item = new Document(documentid, category, patientid, year, month, clinicid, pages))
                        {

                            if (_item != null)
                            {
                                _innerlist.Add(_item);
                            }
                        }
                    }
                    
                }

                public bool Remove(Document item)
                {
                    bool result = false;
                    Document obj;

                    if (_innerlist != null)
                    {
                        for (int i = _innerlist.Count - 1; i >= 0; i--)
                        {
                            //store current index being checked
                            using (obj = new Document())
                            {
                                if (obj != null)
                                {
                                    obj = (Document)_innerlist[i];
                                    if (obj != null)
                                    {
                                        if (obj.DocumentID == item.DocumentID)
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
                        if (index < 0 || index >= _innerlist.Count )
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
                    if (_innerlist != null)
                    {
                        _innerlist.Clear();
                    }
                }

                public Document this[int index]
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
                                return (Document)_innerlist[index];
                            }

                        }
                        else
                        {
                            return null;
                        }
                    }

                }

                public bool Contains(Document item)
                {
                    if (_innerlist != null)
                    {
                        return _innerlist.Contains(item);
                    }
                    else
                    {
                        return false;
                    }
                }

                public int IndexOf(Document item)
                {
                    if (_innerlist != null)
                    {
                        return _innerlist.IndexOf(item);
                    }
                    else
                    {
                        return -1;
                    }
                }

                public void CopyTo(Document[] array, int index)
                {
                    if (_innerlist != null)
                    {
                        _innerlist.CopyTo(array, index);
                    }
                }
            }

            public partial class Page : IDisposable
            {
                #region "Constructor & Distructor"

                public Page()
                {
                }

                public Page(Int32 containerpagenumber, Int32 documentpagenumber, string pagename, string bookmarktag, Int64 clinicid)
                {
                    _ContainerPageNumber = containerpagenumber;
                    _DocumentPageNumber = documentpagenumber;
                    _PageName = pagename;
                    _BookMarkTag = bookmarktag;
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

                ~Page()
                {
                    Dispose(false);
                }

                #endregion

                #region "Private Variables"
                private Int32 _ContainerPageNumber = 0;
                private Int32 _DocumentPageNumber = 0;
                private string _PageName = "";
                private string _BookMarkTag = "";
                private Int64 _ClinicID = 0;
                #endregion

                #region "Properties"
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
                    if (_innerlist != null)
                    {
                        _innerlist.Add(item);
                    }
                }

                public void Add(Int32 containerpagenumber, Int32 documentpagenumber, string pagename, string bookmarktag, Int64 clinicid)
                {
                    if (_innerlist != null)
                    {
                        using (Page _item = new Page(containerpagenumber, documentpagenumber, pagename, bookmarktag, clinicid))
                        {

                            if (_item != null)
                            {
                                _innerlist.Add(_item);
                            }
                        }
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
                                        if (obj.ContainerPageNumber == item.ContainerPageNumber && obj.DocumentPageNumber == item.DocumentPageNumber)
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
                    if (_innerlist != null)
                    {
                        _innerlist.CopyTo(array, index);
                    }
                }
            }
        }

        namespace Scanner
        {
            public static class ScanSide
            {
                public static string Single = "Single";
                public static string Duplex = "Duplex";
            }

            public static class ScanMode
            {
                public static string GrayScale = "Gray";
                public static string Color = "Color";
            }

            public static class ScanDPI
            {
                public static string DPI_100 = "100";
                public static string DPI_150 = "150";
                public static string DPI_200 = "200";
                public static string DPI_240 = "240";
                public static string DPI_300 = "300";
            }

            public static class ScanContrastBrightness
            {
                public static string CB_24 = "24";
                public static string CB_48 = "48";
                public static string CB_72 = "72";
                public static string CB_96 = "96";
                public static string CB_128 = "128";
            }
        }

        public class RemoteScanCommon
        {

            public string GetRegistryValue(string strRegistryKey)
            {
                string RegValue = null;

                try
                {
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                    {
                        //_ErrorMessage = "Unable to open registry. " + gloRegistrySetting.gstrSoftEMR;
                        //AuditLogErrorMessage(_ErrorMessage);
                        return RegValue;
                    }

                    object oSetting = gloRegistrySetting.GetRegistryValue(strRegistryKey);

                    if (oSetting != null)
                    {
                        RegValue = oSetting.ToString();
                    }
                    gloRegistrySetting.CloseRegistryKey();

                }
                catch 
                {
                    //_ErrorMessage = ex.ToString();
                    //AuditLogErrorMessage(_ErrorMessage);
                    RegValue = null;
                }

                return RegValue;
            }

            public Int32 GetScanSettingID(string sSettingType, string sCurrentName, Int32 iCurrentScanner, Int32 iCurrentMode)
            {
                Int32 indexi = -1;
                try
                {
                    if (string.IsNullOrEmpty(sCurrentName))
                    { return indexi; }

                    if (sSettingType == "RemoteScanner" || sSettingType == "TwainScanner")
                    {
                        if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner != null)
                        {
                            for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner.Length; i++)
                            {
                                if (sCurrentName == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[i].Name)
                                { indexi = Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[i].ScannerID); break; }
                            }
                        }
                    }
                    else
                    {
                        if ((iCurrentScanner >= 0) && (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner != null))
                        {
                            if (sSettingType == "ScanMode")
                            {
                                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanMode != null)
                                {
                                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanMode.Length; i++)
                                    {
                                        if (sCurrentName == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanMode[i].Name)
                                        { indexi = Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanMode[i].ScanModeID); break; }
                                    }
                                }
                            }
                            else if (sSettingType == "ScanDepth")
                            {
                                if ((iCurrentMode >= 0) && (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanMode[iCurrentMode].ScanDepth != null))
                                {
                                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanMode[iCurrentMode].ScanDepth.Length; i++)
                                    {
                                        if (sCurrentName == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanMode[iCurrentMode].ScanDepth[i].Name)
                                        { indexi = Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanMode[iCurrentMode].ScanDepth[i].ScanDepthId); break; }
                                    }
                                }
                            }
                            else if (sSettingType == "ScanSide")
                            {
                                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanSide != null)
                                {
                                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanSide.Length; i++)
                                    {
                                        if (sCurrentName == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanSide[i].Name)
                                        { indexi = Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].ScanSide[i].ScanSideID); break; }
                                    }
                                }
                            }
                            else if (sSettingType == "ScanResolution")
                            {
                                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Resolution != null)
                                {
                                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Resolution.Length; i++)
                                    {
                                        if (sCurrentName == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Resolution[i].Name)
                                        { indexi = Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Resolution[i].ResolutionID); break; }
                                    }
                                }
                            }
                            else if (sSettingType == "ScanBrightness")
                            {
                                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Brightness != null)
                                {
                                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Brightness.Length; i++)
                                    {
                                        if (sCurrentName == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Brightness[i].Name)
                                        { indexi = Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Brightness[i].BrightnessID); break; }
                                    }
                                }
                            }
                            else if (sSettingType == "ScanContrast")
                            {
                                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Contrast != null)
                                {
                                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Contrast.Length; i++)
                                    {
                                        if (sCurrentName == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Contrast[i].Name)
                                        { indexi = Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].Contrast[i].ContrastID); break; }
                                    }
                                }
                            }
                            else if (sSettingType == "ScanSupportedSize")
                            {
                                if (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize != null)
                                {
                                    for (int i = 0; i < gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize.Length; i++)
                                    {
                                        if (sCurrentName == gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize[i].Name)
                                        { indexi = Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize[i].SupportedSizeID); break; }
                                    }
                                }
                            }
                        }
                    }
                    //

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ActivityLog(ex.ToString());
                }


                return indexi;


            }

            public string GetXMLTagNameForMode(string sInpMode,bool bReverse=false)
            {
                if (string.IsNullOrEmpty(sInpMode)) { return sInpMode; }

                string thisString = sInpMode.ToUpper();
                string sOutMode = "";
                if (bReverse)
                {
                    
                    if (thisString == "BW")
                    {
                        sOutMode = "Black & White";
                    }
                    else if (thisString == "RGB")
                    {
                        sOutMode = "Color";
                    }
                    else if (thisString == "GRAY")
                    {
                        sOutMode = "GrayScale";
                    }
                    else
                    {
                        sOutMode = sInpMode;
                    }
                }
                else
                {
                    
                    if (thisString == "BLACK & WHITE")
                    {
                        sOutMode = "BW";
                    }
                    else if (thisString == "COLOR")
                    {
                        sOutMode = "RGB";
                    }
                    else if (thisString == "GRAYSCALE")
                    {
                        sOutMode = "Gray";
                    }
                    else
                    {
                        sOutMode = sInpMode;
                    }
                }
                return sOutMode;
            }

            public string SetRemoteScannerCurrentSettings(string ScanMode, string ScanSide, string ScanType, string ScanModule = null, string ScanDepth = null)
            {
                Int32 iCurrentScanner, iCurrentMode;
                Int32 iCurrentID = 0;
                string sRegVal = null;

                //Get Scanner ID
                sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteScanner);
                //iCurrentScanner = GetScanSettingID("RemoteScanner", sRegVal, 0, 0);

                if (gloGlobal.gloEliminatePegasus.bEliminatePegasus)
                {
                    iCurrentScanner = GetScanSettingID("TwainScanner", sRegVal, 0, 0);
                }
                else
                {
                    iCurrentScanner = GetScanSettingID("RemoteScanner", sRegVal, 0, 0);
                }

                if (iCurrentScanner < 0) { return "Local Scanner is not set."; }

                //if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting==null)
                //{ 
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting = new gloRemoteScanGeneral.ScannerCurrentSettingsScannerSettings(); 
                //}

                
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerID = Convert.ToString(iCurrentScanner);
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerName = sRegVal;

                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.Remark = "2"; // version number
                //Scan Mode ID
                if (!string.IsNullOrEmpty(ScanMode))
                {
                    string sMode=GetXMLTagNameForMode(ScanMode);

                    iCurrentMode = GetScanSettingID("ScanMode", sMode, iCurrentScanner, 0);
                    gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanModeName = sMode;

                    //iCurrentMode = iCurrentID;
                    gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanModeID = Convert.ToString(iCurrentMode);

                    //Scan Depth ID
                    if (!string.IsNullOrEmpty(ScanDepth))
                    {
                        iCurrentID = GetScanSettingID("ScanDepth", ScanDepth, iCurrentScanner, iCurrentMode);
                        gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanDepthID = Convert.ToString(iCurrentID);
                        gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanDepthName = ScanDepth;
                    }
                    else
                    {
                        sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteScanDepth);
                        iCurrentID = GetScanSettingID("ScanDepth", sRegVal, iCurrentScanner, iCurrentMode);
                        gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanDepthID = Convert.ToString(iCurrentID);
                        gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanDepthName = sRegVal;
                    }
                }
                else
                {
                    if (ScanType == "Scan Card")
                    {

                        string sMode = "";
                        Int32 iCurrentModeID,iCurrentDeptID;
                        string sRegMode= GetRegistryValue(gloRegistrySetting.gstrRemoteScanMode);

                        if (!string.IsNullOrEmpty(sRegMode))
                        {
                            sMode = GetXMLTagNameForMode(sRegMode);
                        }
                        else
                        {
                            sMode = GetXMLTagNameForMode("Black & White");
                        }

                        iCurrentModeID = GetScanSettingID("ScanMode", sMode, iCurrentScanner, 0);
                        gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanModeName = sMode;

                        string sRegDepth = GetRegistryValue(gloRegistrySetting.gstrRemoteScanDepth);

                        if (string.IsNullOrEmpty(sRegDepth))
                        {
                            sRegDepth = "1";
                        }
                      

                        iCurrentDeptID = GetScanSettingID("ScanDepth", sRegDepth, iCurrentScanner, iCurrentModeID);
                        if (iCurrentDeptID == -1)
                        {
                            iCurrentDeptID = 0;
                        }
                        gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanDepthID = Convert.ToString(iCurrentDeptID);
                        gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanDepthName = sRegDepth;
                    }

                }

                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanType = ScanModule; //ScanType;

                if (!string.IsNullOrEmpty(ScanSide))
                {
                    sRegVal = ScanSide;
                }
                else
                {
                    sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteScanSide); 

                    if (ScanType == "Scan Card")
                    {
                        if (string.IsNullOrEmpty(sRegVal))
                        {
                            sRegVal = "Front Side";
                        }
                    }

                    //Scan Side

                }
                iCurrentID = GetScanSettingID("ScanSide", sRegVal, iCurrentScanner, 0);
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanSideID = Convert.ToString(iCurrentID);
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanSideName = sRegVal;

                //read setting for feeder
                if (ScanType != "Scan Card" && sRegVal == "Duplex")
                {
                    gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanSideName += ";feeder";
                }
                else
                {
                    try
                    {
                        sRegVal = GetRegistryValue(gloRegistrySetting.gstrDMSScanFeeder);
                        if (!string.IsNullOrEmpty(sRegVal))
                        {
                            if (sRegVal.ToLower() == "feeder")
                            {
                                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanSideName += ";feeder";
                            }
                            else if (sRegVal.ToLower() == "flatbed")
                            {
                                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScanSideName += ";flatbed";
                            }
                        }
                    }
                    catch
                    { }
                }
                //

                //Get Resolution ID
                sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteScanResol);
                iCurrentID = GetScanSettingID("ScanResolution", sRegVal, iCurrentScanner, 0);
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ResolutionID = Convert.ToString(iCurrentID);
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ResolutionName = sRegVal;
                //

                //Get Brightness ID
                sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteScanBright);
                iCurrentID = GetScanSettingID("ScanBrightness", sRegVal, iCurrentScanner, 0);
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.BrightnessID = Convert.ToString(iCurrentID); ;
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.BrightnessName = sRegVal;
                //

                //Get Contrast ID
                sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast);
                iCurrentID = GetScanSettingID("ScanContrast", sRegVal, iCurrentScanner, 0);
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ContrastID = Convert.ToString(iCurrentID); ;
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ContrastName = sRegVal;
                //

                //Get Supported Size
                sRegVal = GetRegistryValue(gloRegistrySetting.gstrRemoteSupporedSize);
                iCurrentID = GetScanSettingID("ScanSupportedSize", sRegVal, iCurrentScanner, 0);
                Int32 iCurrentSupportedSizeID = iCurrentID;
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.SupportedSizeID = Convert.ToString(iCurrentID); ;
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.SupportedSizeName = sRegVal;

                
                string gstrRemoteCardLeftX = null;
                string gstrRemoteCardLength = null;
                string gstrRemoteCardTopY = null;
                string gstrRemoteCardWidth = null;
                float _CardWidth, _CardLength;

                if (ScanType == "Scan Card")
                {
                    gstrRemoteCardLeftX = GetRegistryValue(gloRegistrySetting.gstrRemoteCardLeftX);
                    gstrRemoteCardLength = GetRegistryValue(gloRegistrySetting.gstrRemoteCardLength);
                    gstrRemoteCardTopY = GetRegistryValue(gloRegistrySetting.gstrRemoteCardTopY);
                    gstrRemoteCardWidth = GetRegistryValue(gloRegistrySetting.gstrRemoteCardWidth);

                    if (string.IsNullOrEmpty(gstrRemoteCardLeftX))
                    {
                        gstrRemoteCardLeftX = "0";
                    }
                    if (string.IsNullOrEmpty(gstrRemoteCardLength))
                    {
                        gstrRemoteCardLength = "4.0";
                    }
                    if (string.IsNullOrEmpty(gstrRemoteCardTopY))
                    {
                        gstrRemoteCardTopY = "0";
                    }
                    if (string.IsNullOrEmpty(gstrRemoteCardWidth))
                    {
                        gstrRemoteCardWidth = "4.0";
                    }
                }
                else
                {
                    try
                    {
                        if ((iCurrentSupportedSizeID >=0) && (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize != null) && (gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize.Length > 0))
                        {
                            gstrRemoteCardLeftX = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize[iCurrentSupportedSizeID].Left;
                            gstrRemoteCardLength = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize[iCurrentSupportedSizeID].Length;
                            gstrRemoteCardTopY = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize[iCurrentSupportedSizeID].Top;
                            gstrRemoteCardWidth = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner[iCurrentScanner].SupportedSize[iCurrentSupportedSizeID].Width;
                        }
                    }
                    catch
                    {}

                    if (string.IsNullOrEmpty(gstrRemoteCardLeftX))
                    {
                        gstrRemoteCardLeftX = "0";
                    }
                    if (string.IsNullOrEmpty(gstrRemoteCardLength))
                    {
                        gstrRemoteCardLength = "11";
                    }
                    if (string.IsNullOrEmpty(gstrRemoteCardTopY))
                    {
                        gstrRemoteCardTopY = "0";
                    }
                    if (string.IsNullOrEmpty(gstrRemoteCardWidth))
                    {
                        gstrRemoteCardWidth = "8.5";
                    }
                }

                gloEDocumentV3.Forms.ScannerSettings objScannerSettings = new gloEDocumentV3.Forms.ScannerSettings();

                _CardWidth = (float)(Convert.ToDouble(gstrRemoteCardWidth));
                _CardLength = (float)(Convert.ToDouble(gstrRemoteCardLength));
                float X = (float)(Convert.ToDouble(gstrRemoteCardLeftX));
                float Y = (float)(Convert.ToDouble(gstrRemoteCardTopY));

                if (ScanType == "Scan Card")
                {
                    System.Drawing.RectangleF rectF = new System.Drawing.RectangleF(1f, 1f, 8.5f, 14f);

                    objScannerSettings.CardValidator(rectF, ref _CardWidth, ref _CardLength, true);
                }

                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardLeft = X;
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardLength = _CardLength;
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardTop = Y;
                gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardWidth = _CardWidth;

                try
                {
                    if (ScanType == "Scan Card")
                    {
                        if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerName.Contains("FUJITSU") || gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ScannerName.ToUpper().Contains("FUJITSU"))
                        {
                            if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardLength <= 4 && gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.CardWidth <= 4)
                            {
                                if (gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.SupportedSizeName.ToUpper() != "A6")
                                {
                                    iCurrentID = GetScanSettingID("ScanSupportedSize", "A6", iCurrentScanner, 0);
                                    gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.SupportedSizeID = Convert.ToString(iCurrentID); ;
                                    gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.SupportedSizeName = "A6";
                                }
                            }
                        }
                    }
                }
                catch
                { }

                if (objScannerSettings != null) { objScannerSettings.Dispose(); objScannerSettings = null; }

                // Scan Show UI
                sRegVal=GetRegistryValue(gloRegistrySetting.gstrRemoteShowScann);

                if (!string.IsNullOrEmpty(sRegVal))
                {
                    gloRemoteScanGeneral.RemoteScanSettings.oCurrentSetting.ShowUI = Convert.ToBoolean(sRegVal);
                }
                //

                return "";
            }
        }
    }
}
