using System;
using System.Collections.Generic;
using System.Text;

namespace gloRxHub
{
    public class ClsDrugAlerts:IDisposable

    {
        #region "Public and Private variable"

        //List<ClsFormularyStatusDetails> objFormularyStatusDetails =new List<ClsFormularyStatusDetails>();//Commented for ANSI 5010

        private bool disposedValue = false;
        #endregion "Public and Private variable"

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                // TODO: free managed resources when explicitly called 
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region "Property procedures"
        ////Commented for ANSI 5010
        //public List<ClsFormularyStatusDetails> FormularyStatusDetails
        //{
        //    get 
        //    {
        //        if (objFormularyStatusDetails == null)
        //        {
        //            objFormularyStatusDetails = new List<ClsFormularyStatusDetails>();
        //        }
        //        return objFormularyStatusDetails;
        //    }
        //    set { objFormularyStatusDetails = value; }        
        //}

        #endregion "Property procedures"

    }
}
