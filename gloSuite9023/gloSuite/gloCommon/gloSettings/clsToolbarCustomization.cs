using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace gloSettings
{
    class gloToolbarCustomization
    {
        #region " Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
      //  private string _emrdatabaseconnectionstring = "";
        //private string _messageBoxCaption = "gloPMS";
        private string _messageBoxCaption = String.Empty;

        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region "Constructor & Distructor"

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        


        public gloToolbarCustomization(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
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

        ~gloToolbarCustomization()
        {
            Dispose(false);
        }

        #endregion

        #region " Private & Public Methods "

        public bool SaveButtonSelection(Int64 userId, enumModuleName ModuleName, ArrayList _selectedButtons)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            string strButtons = "";

            try
            {
                oDB.Connect(false);

                ///Delete previous ToolBarButton for current Users 
                //strSQL = "Delete ToolBarButtons where nUserID = " + UserID + " AND nModule = " + ModuleName.GetHashCode();
                strSQL = " DELETE FROM SET_Customization_ToolBarButtons WHERE nUserID = " + userId + " AND nModule =  "+ModuleName.GetHashCode()+" AND nClinicID = "+this.ClinicID+" ";
                oDB.Execute_Query(strSQL);

                for (int i = 0; i <= _selectedButtons.Count - 1; i++)
                { strButtons += "," + _selectedButtons[i].ToString(); }

                if (strButtons.Length > 0)
                { strButtons = strButtons.Substring(1, strButtons.Length - 1); }

                //strSQL = " INSERT INTO ToolBarButtons(nUserID, nModule, sButtons) " + " VALUES (" + userId + ", " + ModuleName.GetHashCode() + ", '" + strButtons + "')";
                strSQL = " INSERT INTO  SET_Customization_ToolBarButtons " +
                " (nUserID, nModule, sButtons, nClinicID) " +
                " VALUES (" + userId + "," + ModuleName.GetHashCode() + ",'" + strButtons + "'," + this.ClinicID + ") ";


                oDB.Execute_Query(strSQL);

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { 
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                strSQL = null;
                strButtons = null;
            }

            return true; 
        }

        public DataTable GetGetButtonSelection(Int64 userId,enumModuleName moduleName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            string strSQL = "";
           // string strButtons = "";

            try
            {
                oDB.Connect(false);
                ///Delete previous ToolBarButton for current Users 
                ///nUserID,nModule,sButtons,nClinicID
                
                //strSQL = "SELECT sButtons, nUserId, nModule FROM ToolBarButtons WHERE nUserId = " + userId + " AND nModule = " + moduleName.GetHashCode();
                strSQL = " SELECT ISNULL(nUserID,0) AS nUserID,ISNULL(nModule,0) AS nModule,ISNULL(sButtons,'') AS sButtons,nClinicID " +
                " FROM SET_Customization_ToolBarButtons WHERE nUserID = " + userId + " AND nModule = " + moduleName.GetHashCode() + " "+
                " AND nClinicID = "+this.ClinicID+" ";


                oDB.Retrive_Query(strSQL,out dt);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { 
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                strSQL = null;
            }

            return dt;
        }

        #endregion " Private & Public Methods "

    }

}
