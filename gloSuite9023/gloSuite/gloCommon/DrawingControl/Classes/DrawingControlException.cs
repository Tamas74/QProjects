using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingControl
{
    public class DrawingControlException:ApplicationException 
    {
        public DrawingControlException(string sErrorMessage)
                : base(sErrorMessage)
            {

            }
        public DrawingControlException(string sErrorMessage, System.Exception inner)
                : base(sErrorMessage, inner)
            {

            }
    }
}
