using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using gloEDocumentV3.Enumeration;

namespace gloEDocumentV3
{
    namespace DocumentContextMenu
    {
        internal class eDocV3ContextMenuStrip : System.Windows.Forms.ContextMenuStrip
        {
            #region "Constructor & Distructor"
            public eDocV3ContextMenuStrip()
                : base()
            {
                _eSelectedDocuments = new eContextDocuments();
                if (_eSelectedDocuments == null)
                {
                    string _ErrorMessage = "selected document is null";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }

            protected override void Dispose(bool disposing)
            {
                //Dispose internal values
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                base.Dispose(disposing);
            }
            #endregion

            #region "Variable Declaration"

            private eContextDocuments _eSelectedDocuments = null;

            public eContextDocuments ESelectedDocuments
            {
                get { return _eSelectedDocuments; }
                set { _eSelectedDocuments = value; }
            }
            #endregion
        }

        internal class eDocV3ContextMenuItem : System.Windows.Forms.ToolStripMenuItem
        {
            public eDocV3ContextMenuItem()
                : base()
            {
                _eContextEventParameter = new eContextEventParameter();
                if (_eContextEventParameter == null)
                {
                    string _ErrorMessage = "Context event parameter is null";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }

            public eDocV3ContextMenuItem(Int64 patientid, Int64 containerid, Int64 documentid, Int64 categoryid, string category, string year, string month, string documentname, enum_DocumentEventType eventtype, Int64 clinicid, bool ispagemenu)
                : base()
            {
                _eContextEventParameter = new eContextEventParameter(patientid, containerid, documentid, categoryid, category,"", year, month, documentname, eventtype, clinicid, ispagemenu);
                if (_eContextEventParameter == null)
                {
                    string _ErrorMessage = "Context event parameter is null";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }

            protected override void Dispose(bool disposing)
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
              
                base.Dispose(disposing);
            }

            private eContextEventParameter _eContextEventParameter = null;

            public eContextEventParameter EContextEventParameter
            {
                get { return _eContextEventParameter; }
                set { _eContextEventParameter = value; }
            }
        }

        #region "Context Menu Supporting Classes"

        public class eContextDocument : IDisposable
        {
            #region "Constructor & Distructor"

            public eContextDocument()
            {
                Containers = new Common.eContextContainers();
                if (Containers == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "For Containers";
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

            ~eContextDocument()
            {
                Dispose(false);
            }

            #endregion

            public Int64 DocumentID = 0;
            public int PageCount = 0;
            public Common.eContextContainers Containers = new Common.eContextContainers();
            public Int64 PatientID = 0;
            public Int64 CategoryID = 0;
            public string Category = "";
            public string Year = "";
            public string Month = "";
            public string DocumentName = "";
            public bool IsAcknowledge = false;
            public Int64 ClinicID = 0;
        }

        public class eContextDocuments : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public eContextDocuments()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "eContextDocuments";
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


            ~eContextDocuments()
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
                //get { return _innerlist.Count; }
            }

            public void Add(eContextDocument item)
            {
                // _innerlist.Add(item);
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public bool Remove(eContextDocument item)
            {

                bool result = false;
                eContextDocument obj;

                for (int i = _innerlist.Count - 1; i >= 0; i--)
                {
                   // store current index being checked
                    using (obj = new eContextDocument())
                    {
                        if (obj != null)
                        {
                            obj = (eContextDocument)_innerlist[i];
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

            public eContextDocument this[int index]
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
                            return (eContextDocument)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(eContextDocument item)
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

            public int IndexOf(eContextDocument item)
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

            public void CopyTo(eContextDocument[] array, int index)
            {
               // _innerlist.CopyTo(array, index);
                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }
        }


        public class eContextEventParameter : IDisposable
        {
            #region "Constructor & Distructor"

            public eContextEventParameter()
            {
                
            }

            public eContextEventParameter(Int64 patientid, Int64 containerid, Int64 documentid, Int64 categoryid, string category,string subcategory, string year, string month, string documentname, enum_DocumentEventType eventtype, Int64 clinicid, bool ispagemenu)
            {
                PatientID = patientid;
                ContainerID = containerid;
                DocumentID = documentid;
                CategoryID = categoryid;
                Category = category;
                SubCategory = subcategory;
                Year = year;
                Month = month;
                DocumentName = documentname;
                EventType = eventtype;
                ClinicID = clinicid;
                IsPageMenu = ispagemenu;
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

            ~eContextEventParameter()
            {
                Dispose(false);
            }

            #endregion

            public Int64 PatientID = 0;
            public Int64 ContainerID = 0;
            public Int64 DocumentID = 0;
            public Int64 CategoryID = 0;
            public string Category = "";
            public string SubCategory = "";
            public string Year = "";
            public string Month = "";
            public string DocumentName = "";
            public enum_DocumentEventType EventType = enum_DocumentEventType.None;
            public Int64 ClinicID = 0;
            public bool IsPageMenu = false;
        }

        namespace Common
        {
            public class eContextPage : IDisposable
            {
                #region "Constructor & Distructor"

                public eContextPage()
                {
                }

                public eContextPage(int containerpagenumber, int documentpagenumber, string pagename, bool hasnotes)
                {
                    ContainerPageNumber = containerpagenumber;
                    DocumentPageNumber = documentpagenumber;
                    PageName = pagename;
                    HasNotes = hasnotes;
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

                ~eContextPage()
                {
                    Dispose(false);
                }

                #endregion

                public int ContainerPageNumber = 0;
                public int DocumentPageNumber = 0;
                public string PageName = "";
                public bool HasNotes = false;
            }

            public class eContextPages : IDisposable
            {
                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public eContextPages()
                {
                    _innerlist = new ArrayList();
                    if (_innerlist == null)
                    {
                        string _ErrorMessage = "Insuffucient Memory . " + "eContextPages";
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


                ~eContextPages()
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

                public void Add(eContextPage item)
                {
                    //_innerlist.Add(item);
                    if (_innerlist != null)
                    {
                        _innerlist.Add(item);
                    }
                }

                public void Add(int containerpagenumber, int documentpagenumber, string pagename, bool hasnotes)
                {
                    if (_innerlist != null)
                    {
                        using (eContextPage _item = new eContextPage(containerpagenumber, documentpagenumber, pagename, hasnotes))
                        {

                            if (_item != null)
                            {
                                _innerlist.Add(_item);
                            }
                        }
                    }
                }

                public bool Remove(eContextPage item)
                {
                    bool result = false;
                    eContextPage obj;

                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new eContextPage())
                        {
                            if (obj != null)
                            {
                                obj = (eContextPage)_innerlist[i];
                                if (obj != null)
                                {
                                    if (obj.DocumentPageNumber == item.DocumentPageNumber)
                                    {
                                        _innerlist.RemoveAt(i);
                                        result = true;
                                        break;
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

                public eContextPage this[int index]
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
                                return (eContextPage)_innerlist[index];
                            }

                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                public bool Contains(eContextPage item)
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

                public int IndexOf(eContextPage item)
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

                public void CopyTo(eContextPage[] array, int index)
                {
                    //_innerlist.CopyTo(array, index);
                    if (_innerlist != null)
                    {
                        _innerlist.CopyTo(array, index);
                    }
                }
            }

            public class eContextContainer : IDisposable
            {
                #region "Constructor & Distructor"

                public eContextContainer()
                {
                    Pages = new eContextPages();
                    if (Pages == null)
                    {
                        string _ErrorMessage = "Insuffucient Memory . " + "eContextContainer";
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

                ~eContextContainer()
                {
                    Dispose(false);
                }

                #endregion

                public Int64 ContainerID = 0;
                public int DocumentPageFrom = 0;
                public int DocumentPageTo = 0;
                public eContextPages Pages = new eContextPages();
            }

            public class eContextContainers : IDisposable
            {
                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public eContextContainers()
                {
                    _innerlist = new ArrayList();
                    if (_innerlist == null)
                    {
                        string _ErrorMessage = "Insuffucient Memory . " + "eContextContainers";
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


                ~eContextContainers()
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

                public void Add(eContextContainer item)
                {
                    //_innerlist.Add(item);
                    if (_innerlist != null)
                    {
                        _innerlist.Add(item);
                    }
                }

                public bool Remove(eContextContainer item)
                {

                    bool result = false;
                    eContextContainer obj;
                    if (_innerlist != null)
                    {
                        for (int i = _innerlist.Count; i > 0; i--)
                        {
                            //store current index being checked
                            using (obj = new eContextContainer())
                            {
                                if (obj != null)
                                {
                                    obj = (eContextContainer)_innerlist[i];
                                    if (obj != null)
                                    {
                                        if (obj.ContainerID == item.ContainerID)
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
                    //_innerlist.Clear();3

                    if (_innerlist != null)
                    {
                        _innerlist.Clear();
                    }
                }

                public eContextContainer this[int index]
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
                                return (eContextContainer)_innerlist[index];
                            }

                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                public bool Contains(eContextContainer item)
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

                public int IndexOf(eContextContainer item)
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

                public void CopyTo(eContextContainer[] array, int index)
                {
                    //_innerlist.CopyTo(array, index);
                    if (_innerlist != null)
                    {
                        _innerlist.CopyTo(array, index);
                    }
                }
            }
        }
        #endregion

        internal static class ContextMenuNames
        {
            internal static string gDocMenu_SendToNewFile = "Send to new document";
            internal static string gDocMenu_MergeInExisting = "Merge in existing document";
            internal static string gDocMenu_Print = "Print";
            internal static string gDocMenu_PrintDocument = "Print Document";
            internal static string gDocMenu_Fax = "Fax";
            internal static string gDocMenu_FaxDocument = "Fax Document";
            internal static string gDocMenu_Delete = "Delete";
            internal static string gDocMenu_MoveToPatient = "Send to patient";
            internal static string gDocMenu_Rename = "Rename";
            internal static string gDocMenu_Acknowledge = "Acknowledge";
            internal static string gDocMenu_UnAcknowledge = "Un-Acknowledge";
            internal static string gDocMenu_Reviwed = "Review";
            internal static string gDocMenu_AcknowledgeReview = "Acknowledge\\Review";
            internal static string gDocMenu_SelectAll = "Select All";
            internal static string gDocMenu_UnselectAll = "Unselect All";

            internal static string gDocMenu_View_View = "View";
            internal static string gDocMenu_View_LargeIcon = "Large Icons";
            internal static string gDocMenu_View_SmallIcon = "Small Icons";
            internal static string gDocMenu_View_List = "List";
            internal static string gDocMenu_View_Tiles = "Tiles";

            internal static string gDocMenu_SendToDMS = "Send To DMS";
        }
    }
}
