using System;
using System.Collections.Generic;
using System.Text;

namespace gloGeneralNode
{
    public class gloGeneralNode : System.Windows.Forms.TreeNode
    {
         #region "Constructor & Distructor"

            public gloGeneralNode():base()
            {
            }

            public gloGeneralNode( string Code, string Description,Int64 Id) : base()
            {
                _id = Id;
                _code = Code;
                _description = Description;
                base.Tag = Id;
                base.Text = Code + " " + Description;
            }

            public gloGeneralNode( string Description, Int32 key): base()
            {
                _key = key;
                _code = "";
                _description = Description;
                base.Text = Code + " " + Description;
            }
            public gloGeneralNode(string Description, Int64 id): base()
        {
            _id= id;
            _code = "";
            _description = Description;
            base.Tag = id;
            base.Text = Code + " " + Description;
        }
            //private bool disposed = false;

            //public void Dispose()
            //{
            //    Dispose(true);
            //    GC.SuppressFinalize(this);
            //}

            //protected virtual void Dispose(bool disposing)
            //{
            //    if (!this.disposed)
            //    {
            //        if (disposing)
            //        {

            //        }
            //    }
            //    disposed = true;
            //}

            //~gloGeneralNode()
            //{
            //    Dispose(false);
            //}
    #endregion

    #region "Properties & Variables"
            private Int64 _id = 0;
            private string _code = "";
            private string _description = "";
            private Int32 _key = 0;

            public Int32 KEY
            {
                get { return _key; }
                set { _key = value; }
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
    #endregion
        }




}
