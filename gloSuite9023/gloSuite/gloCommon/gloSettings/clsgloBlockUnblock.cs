using System;
using System.Collections.Generic;
using System.Text;

namespace gloSettings
{
    public class gloBlockUnblock
    {
         #region " Declarations "

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region "Constructor & Distructor"

        public gloBlockUnblock(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);

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

        ~gloBlockUnblock()
        {
            Dispose(false);
        }

#endregion

        #region "Show UI"
            public void ShowViewBlockedItems(System.Windows.Forms.Form oParentWindow)
            {
                
                //frmViewBlockedItems ofrmViewBlockedItems = new frmViewBlockedItems(_databaseconnectionstring);
                //ofrmViewBlockedItems.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                //ofrmViewBlockedItems.MdiParent = oParentWindow;
                //ofrmViewBlockedItems.Show();
                
            }
        #endregion
        

        
    }
}
