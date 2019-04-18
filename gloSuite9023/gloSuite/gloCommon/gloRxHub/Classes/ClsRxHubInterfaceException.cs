using System;
using System.Collections.Generic;
using System.Text;

namespace gloRxHub
{
    public class ClsRxHubInterfaceException :ApplicationException  
    {

        public ClsRxHubInterfaceException(string sErrorMessage)
                : base(sErrorMessage)
            {

            }
    }
}
