using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace gloGeneralItem
{
    public class gloItem : IDisposable
    {

        #region "Constructor & Distructor"

            public gloItem()
            {
                _SubItems = new gloSubItems();
            }

            public gloItem(Int64 Id, string Code, string Description)
            {
                _id = Id;
                _code = Code;
                _description = Description;
                _SubItems = new gloSubItems();
            }

            public gloItem(Int64 Id, string Description)
            {
                _id = Id;
                _code = "";
                _description = Description;
            _SubItems = new gloSubItems();
            }



            public gloItem(string Code, string Description)
            {
                _code = Code;
                _description = Description;
                _SubItems = new gloSubItems();
            }

        //SHUBHANGI 20110103
          public gloItem(Int64 Id, string Description,Int64 Default)
          {
                _id = Id;
                _code = Code;
                _description = Description;
                _default = Default;
                _SubItems = new gloSubItems();
          }
        //END
          
          public gloItem(Int64 Id, string Description,DateTime? MuDate,bool MuCheckBox )
          {
              _id = Id;
              _code = Code;
              _description = Description;
              _muDate = MuDate;
              _muCheckBox = MuCheckBox;
              _SubItems = new gloSubItems();
          }
          

          //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
          public gloItem(Int64 Id, string Code, string Description, string SSN)
          {
              _id = Id;
              _code = Code;
              _description = Description;
              _ssn = SSN;
              _SubItems = new gloSubItems();
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
                        if (!bAssignedfromOut)
                        {
                            _SubItems.Clear();
                            _SubItems.Dispose();
                            _SubItems = null;
                        }
                    }
                }
                disposed = true;
            }

            ~gloItem()
            {
                Dispose(false);
            }

    #endregion

        private Int64 _id = 0;
        private string _code = "";
        private string _description = "";
        private Int64 _default = 0;
        private string _status = "";

        //14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
        private bool _IsSnoMedOneToOneMapping = false;

        private DateTime? _muDate=null;
        private bool _muCheckBox = false;

        private gloSubItems _SubItems = null;

        //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd).
        private string _ssn = string.Empty;

        private bool _bGloCollect = false;

        //14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
        public bool IsSnoMedOneToOneMapping
        {
            get { return _IsSnoMedOneToOneMapping; }
            set { _IsSnoMedOneToOneMapping = value; }
        }

        public Int64 ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Int64 Default
        {
            get { return _default; }
            set { _default = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private bool bAssignedfromOut = false;
        public gloSubItems SubItems
        {
            get 
            {
                if (_SubItems == null)
                {
                    _SubItems = new gloSubItems();
                }
                return _SubItems; 
            }
            set 
            {
                if (_SubItems != null)
                {
                    _SubItems.Clear();
                    _SubItems.Dispose();
                    _SubItems = null;
                }
                _SubItems = value;
                bAssignedfromOut = true;
            }
        }

        //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
        public string SSN
        {
            get { return _ssn; }
            set { _ssn = value; }
        }

        public DateTime? MuDate
        {
            get { return _muDate; }
            set { _muDate = value; }
        }

        public bool MuCheckBox
        {
            get { return _muCheckBox; }
            set { _muCheckBox = value; }
        }

        public bool bGloCollect
        {
            get { return _bGloCollect; }
            set { _bGloCollect = value; }
        }


    }

    public class gloItems : IDisposable, IEnumerable 
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

                public gloItems()
                {
                    _innerlist = new ArrayList();
                }

                private bool disposed = false;
                private bool oktoClear = false;
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
                            if (oktoClear)
                            {
                                Clear();
                            }
                        }
                    }
                    disposed = true;
                }


                ~gloItems()
                {
                    Dispose(false);
                }
        #endregion

        
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(gloItem item)
        {
            _innerlist.Add(item);            
            
            
        }
        public System.Collections.IEnumerator  GetEnumerator()
        {
            return _innerlist.GetEnumerator();
        }

        public int Add(Int64 Id, string Code, string Description )
        {
            gloItem item = new gloItem(Id, Code, Description);
            try
            {
                return _innerlist.Add(item);
            }
            catch
            {
                return -1;
            }
            finally
            {
                item.Dispose();
                item = null;
            }
        }
        public int Add(string Code, string Description)
        {
            gloItem item = new gloItem(Code, Description);
            try
            {
                return _innerlist.Add(item);
            }
            catch
            {
                return -1;
            }
            finally
            {
                item.Dispose();
                item = null;
            }
        }
        public int Add(Int64 Id, string Description)
        {
            gloItem item = new gloItem(Id,Description);
            try
            {
                return _innerlist.Add(item);
            }
            catch
            {
                return -1;
            }
            finally
            {
                item.Dispose();
                item = null;
            }
        }
        //SHUBHANGI 20110103
        public int Add(Int64 Id, string Description,Int64 Default)
        {
            gloItem item = new gloItem(Id, Description,Default);
            try
            {
                return _innerlist.Add(item);
            }
            catch
            {
                return -1;
            }
            finally
            {
                item.Dispose();
                item = null;
            }
        }
        //END

       // Added MuDate And MuCheckBox
        public int Add(Int64 Id, string Description,DateTime? MuDate,bool MuCheckBox) 
        {
            gloItem item = new gloItem(Id, Description,MuDate,MuCheckBox);
            try
            {
                return _innerlist.Add(item);
            }
            catch
            {
                return -1;
            }
            finally
            {
                item.Dispose();
                item = null;
            }
        }

        public void Insert(int index, gloItem item)
        {
            _innerlist.Insert(index,item);
        }

        public bool Remove(gloItem item)
        {
            bool result = false;
            gloItem obj; 

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
             //   obj = new gloItem();
                obj = (gloItem)_innerlist[i];
                if (obj.ID == item.ID && obj.Description == item.Description)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                try
                {
                    if (obj != null)
                    {
                        obj.Dispose();
                        obj = null;
                    }
                }
                catch
                {
                }
            }

            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            try
            {
                if (index < _innerlist.Count)
                {
                    RemoveAtIndex(index);
                    result = true;
                }
                
            }
            catch
            {
            }
           
            return result;
        }

        private void RemoveAtIndex(int index)
        {
            gloItem obj = (gloItem)_innerlist[index];
            _innerlist.RemoveAt(index);
            try
            {
                if (obj != null)
                {
                    obj.Dispose();
                    obj = null;
                }
            }
            catch
            {
            }
        }

        public void Clear()
        {
            for (int i = _innerlist.Count - 1; i >= 0; i--)
            {
                RemoveAtIndex(i);
            }
            _innerlist.Clear();
        }

        public gloItem this[int index]
        {
            get 
            {
                return (gloItem)_innerlist[index];
            }
        }

        public bool Contains(gloItem item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(gloItem item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(gloItem[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }

    public class gloSubItem : IDisposable
    {

        #region "Constructor & Distructor"

            public gloSubItem()
            {

            }

            public gloSubItem(Int64 Id, string Code, string Description)
            {
                _id = Id;
                _code = Code;
                _description = Description;
            }

            public gloSubItem(Int64 Id, string Code, string Description,string CloseDate)
            {
                _id = Id;
                _code = Code;
                _description = Description;
                _CloseDate = CloseDate;
            }

            public gloSubItem(Int64 Id, string Description)
            {
                _id = Id;
                _code = "";
                _description = Description;
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

            ~gloSubItem()
            {
                Dispose(false);
            }

        #endregion

        private Int64 _id = 0;
        private string _code = "";
        private string _description = "";
        private string _CloseDate = "";

        //14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
        private bool _IsSnoMedOneToOneMapping = false;

        //14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
        public bool IsSnoMedOneToOneMapping
        {
            get { return _IsSnoMedOneToOneMapping; }
            set { _IsSnoMedOneToOneMapping = value; }
        }

        public Int64 ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string CloseDate
        {
            get { return _CloseDate; }
            set { _CloseDate = value; }
        }
    }

    public class gloSubItems : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

            public gloSubItems()
            {
                _innerlist = new ArrayList();
            }

            private bool disposed = false;
            private bool oktoClear = false;
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
                        if (oktoClear)
                        {
                            Clear();
                        }
                    }
                }
                disposed = true;
            }


            ~gloSubItems()
            {
                Dispose(false);
            }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(gloSubItem item)
        {
            _innerlist.Add(item);
        }

        public int Add(Int64 Id, string Code, string Description)
        {
            gloSubItem item = new gloSubItem(Id, Code, Description);
            try
            {
                return _innerlist.Add(item);
            }
            catch
            {
                return -1;
            }
            finally
            {
                item.Dispose();
                item = null;
            }
        }

        public int Add(Int64 Id, string Code, string Description,string CloseDate)
        {
            gloSubItem item = new gloSubItem(Id, Code, Description, CloseDate);
            try
            {
                return _innerlist.Add(item);
            }
            catch
            {
                return -1;
            }
            finally
            {
                item.Dispose();
                item = null;
            }
        }

        public int Add(Int64 Id, string Description)
        {
            gloSubItem item = new gloSubItem(Id, Description);
            try
            {
                return _innerlist.Add(item);
            }
            catch
            {
                return -1;
            }
            finally
            {
                item.Dispose();
                item = null;
            }
        }

        public bool Remove(gloSubItem item)
        {
            bool result = false;
            gloSubItem obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
             //   obj = new gloSubItem();
                obj = (gloSubItem)_innerlist[i];
                if (obj.ID == item.ID && obj.Description == item.Description)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                try
                {
                    if (obj != null)
                    {
                        obj.Dispose();
                        obj = null;
                    }
                }
                catch
                {
                }
            }

            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            try
            {
                if (index < _innerlist.Count)
                {
                    RemoveAtIndex(index);
                    result = true;
                }

            }
            catch
            {
            }

            return result;
        }

        private void RemoveAtIndex(int index)
        {
            gloSubItem obj = (gloSubItem)_innerlist[index];
            _innerlist.RemoveAt(index);
            try
            {
                if (obj != null)
                {
                    obj.Dispose();
                    obj = null;
                }
            }
            catch
            {
            }
        }

        public void Clear()
        {
            for (int i = _innerlist.Count-1; i >=0; i--)
            {
                RemoveAtIndex(i);
            }
            _innerlist.Clear();
        }

        public gloSubItem this[int index]
        {
            get
            {
                return (gloSubItem)_innerlist[index];
            }
        }

        public bool Contains(gloSubItem item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(gloSubItem item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(gloSubItem[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }
}
