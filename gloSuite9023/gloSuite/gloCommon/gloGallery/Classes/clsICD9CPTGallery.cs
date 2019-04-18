using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace gloGallery
{
    public class ICD9CPT : IDisposable
    {

        // To detect redundant calls
        private bool disposedValue = false;
        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: free managed resources when explicitly called
                }

                // TODO: free shared unmanaged resources
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        ~ICD9CPT()
        {
            Dispose(false);
        }

        private string _ICD9Code;
        private string _ICD9Indicator;
        private string _ICD9CodeStatus;
        private string _ICD9DescriptionShort;
        private string _ICD9DescriptionMedium;

        private string _ICD9DescriptionLong;
        private string _CPTCode;
        private string _CPTDescription;
        public string ICD9Code
        {
            get { return _ICD9Code; }
            set { _ICD9Code = value; }
        }
        public string ICD9Indicator
        {
            get { return _ICD9Indicator; }
            set { _ICD9Indicator = value; }
        }
        public string ICD9CodeStatus
        {
            get { return _ICD9CodeStatus; }
            set { _ICD9CodeStatus = value; }
        }
        public string ICD9DescriptionShort
        {
            get { return _ICD9DescriptionShort; }
            set { _ICD9DescriptionShort = value; }
        }

        public string ICD9DescriptionMedium
        {
            get { return _ICD9DescriptionMedium; }
            set { _ICD9DescriptionMedium = value; }
        }

        public string ICD9DescriptionLong
        {
            get { return _ICD9DescriptionLong; }
            set { _ICD9DescriptionLong = value; }
        }
        public string CPTCode
        {
            get { return _CPTCode; }
            set { _CPTCode = value; }
        }
        public string CPTDescription
        {
            get { return _CPTDescription; }
            set { _CPTDescription = value; }
        }
    }


    public class CollICD9CPT : CollectionBase, IDisposable
    {

        // To detect redundant calls
        private bool disposedValue = false;

        //Remove Item at specified index  

        public void Remove(int index)
        {
            // Check to see if there is a widget at the supplied index.
            if (index > Count - 1 || index < 0)
            {
                // If no object exists, a messagebox is shown and the operation is 
                // cancelled.
                //System.Windows.Forms.MessageBox.Show("Index not valid!")
            }
            else
            {
                // Invokes the RemoveAt method of the List object.
                List.RemoveAt(index);
            }
        }
        // This line declares the Item property as ReadOnly, and 
        // declares that it will return a SentFax object.
        public ICD9CPT Item(int index)
        {
            // The appropriate item is retrieved from the List object and 
            // explicitly cast to the SentFax type, then returned to the 
            // caller.
            return (ICD9CPT)List[index]; 
        }
        // Restricts to SentFax types, items that can be added to the collection.
        public void Add(ICD9CPT oConditionfield)
        {
            // Invokes Add method of the List object to add a SentFax.
            List.Add(oConditionfield);
        }
        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: free managed resources when explicitly called
                }

                // TODO: free shared unmanaged resources
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        ~CollICD9CPT()
        {
            Dispose(false);
        }
    }
    public class DBICD9CPT
    {

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _messageBoxCaption = "";
        private Int64 _clinicID;
        private string _databaseConnectionString;
 
        private bool disposedValue = false;
        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: free managed resources when explicitly called
                }

                // TODO: free shared unmanaged resources
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DBICD9CPT()
        {
            Dispose();
        }

        #endregion

        public DBICD9CPT()
        {
        }

        public DBICD9CPT(string sConnectionString)
        {
            _databaseConnectionString = sConnectionString;

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }

            #endregion
        }

        public bool FillICD9Gallery(CollICD9CPT oICD9Coll, ProgressBar oProgressBar, bool Deleteprev)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            try
            {
                string ICD9Code = "";
                string ICD9Indicator = "";
                string ICD9CodeStatus = "";
                string ICD9DescriptionShort = "";
                string ICD9DescriptionMedium = "";
                string ICD9DescriptionLong = "";
                oDB.Connect(false);
                if (Deleteprev == true)
                {
                    string strSelectGallery = " Select nICD9ID from ICD9Gallery";
                    oDB.Retrive_Query(strSelectGallery, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are You Sure to Delete Previous ICD9", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Application.DoEvents();
                            string strTrucateQry = "Truncate Table ICD9Gallery";
                            oDB.Execute_Query(strTrucateQry);
                        }
                    }
                }

                oProgressBar.Enabled = true;
                oProgressBar.Visible = true;
                oProgressBar.Minimum = 0;
                oProgressBar.Maximum = oICD9Coll.Count;
                oProgressBar.Value = oProgressBar.Minimum;

                for (int i = 0; i <= oICD9Coll.Count - 1; i++)
                {
                    ICD9Code = oICD9Coll.Item(i).ICD9Code;
                    ICD9Indicator = oICD9Coll.Item(i).ICD9Indicator;
                    ICD9CodeStatus = oICD9Coll.Item(i).ICD9CodeStatus;
                    ICD9DescriptionShort = oICD9Coll.Item(i).ICD9DescriptionShort;
                    ICD9DescriptionMedium = oICD9Coll.Item(i).ICD9DescriptionMedium;
                    ICD9DescriptionLong = oICD9Coll.Item(i).ICD9DescriptionLong;

                    string strSelectIDQry = "Select Max(nICD9ID)+1 from ICD9Gallery";
                    Int64 ICD9ID = 0;
                    ICD9ID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSelectIDQry));
                    if (ICD9ID == 0)
                    {
                        ICD9ID = 1;
                    }

                    string strInsertQry = "INSERT INTO ICD9Gallery(nICD9ID, sICD9Code, sIndicator, sCodeStatus, sDescriptionShort, sDescriptionMedium, sDescriptionLong) Values(" + ICD9ID + ",'" + ICD9Code + "','" + ICD9Indicator + "','" + ICD9CodeStatus + "','" + ICD9DescriptionShort.Replace("'", "''") + "','" + ICD9DescriptionMedium.Replace("'", "''") + "','" + ICD9DescriptionLong.Replace("'", "''") + "')";
                    oDB.Execute_Query(strInsertQry);
                    oProgressBar.Value = oProgressBar.Value + oProgressBar.Step;
                }
                oDB.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                oProgressBar.Enabled = false;
                oProgressBar.Visible = false;
            }
        }

        public bool FillCPTGallery(CollICD9CPT oCPTColl, ProgressBar oProgressBar, bool Deleteprev)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            try
            {
                string CPTCode = "";
                string CPTDescription = "";
                oDB.Connect(false);
                if (Deleteprev == true)
                {
                    string _strSQL = "Select nCPTID from CPTGallery";
                    oDB.Retrive_Query(_strSQL, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are You Sure to Delete Previous ICD9", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Application.DoEvents();
                            string strTrucateQry = "Truncate Table CPTGallery";
                            oDB.Execute_Query(strTrucateQry);
                        }
                    }
                }
                oProgressBar.Enabled = true;
                oProgressBar.Visible = true;
                oProgressBar.Minimum = 0;
                oProgressBar.Maximum = oCPTColl.Count;
                oProgressBar.Value = oProgressBar.Minimum;
                for (int i = 0; i <= oCPTColl.Count - 1; i++)
                {
                    CPTCode = oCPTColl.Item(i).CPTCode;
                    CPTDescription = oCPTColl.Item(i).CPTDescription;
                    string strSelectIDQry = "Select Max(nCPTID)+1 from CPTGallery ";
                    Int64 CPTID = 0;
                    CPTID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSelectIDQry));
                    if (CPTID==0)
                    {
                        CPTID = 1;
                    }
                    string strInsertQry = "INSERT INTO CPTGallery(nCPTID, sCPTCode, sDescription) Values(" + CPTID + ",'" + CPTCode + "','" + CPTDescription.Replace("'", "''") + "')";
                    oDB.Execute_Query(strInsertQry);
                    oProgressBar.Value = oProgressBar.Value + oProgressBar.Step;   
                }
                oDB.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                oProgressBar.Enabled = false;
                oProgressBar.Visible = false;
            }  
            
        }
     
        public void DeleteCPT(long ID, bool _isSelectedCPTGallery)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                string _strSQL = "";
                if (_isSelectedCPTGallery == true)
                {
                    _strSQL = "delete CPTGallery where nCPTID=" + ID + "";
                }
                else
                {
                    _strSQL = "delete CPT_MST where nCPTID=" + ID + "";
                }
                oDB.Connect(false);
                oDB.Execute_Query(_strSQL);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag">0 for ICD9Code and 1 for ICD9Description</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataTable GetICD9Gallery(Int16 flag, string strIndicator)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dtdrugs = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@strIndicator", strIndicator, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@flag", flag, ParameterDirection.Input, SqlDbType.SmallInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetICD9Gallery", oParameters, out dtdrugs);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dtdrugs;

        }
        public DataTable GetCPTGallery(Int16 flag)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dtdrugs = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@flag", flag, ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetCPTGallery", oParameters, out dtdrugs);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtdrugs = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dtdrugs;

        }

        public int GetMaxICDorCPTCount(gloGallery.clsGallery.GalleryType galleryType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            int _result = 200;
            try
            {
                string _strSQL = "";
                if (galleryType == clsGallery.GalleryType.ICD9)
                {
                    _strSQL = "SELECT sSettingsValue FROM settings where sSettingsName='ICD9TOPCount' ";
                }
                else if (galleryType == clsGallery.GalleryType.ICD10)
                {
                    _strSQL = "SELECT sSettingsValue FROM settings where sSettingsName='ICD10TOPCount' ";
                }
                else if (galleryType == clsGallery.GalleryType.CPT)
                {
                    _strSQL = "SELECT sSettingsValue FROM settings where sSettingsName='CPTTOPCount' ";
                }
                oDB.Connect(false);
                object obj = oDB.ExecuteScalar_Query(_strSQL);
                if (obj != null)
                    int.TryParse(obj.ToString(), out _result);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result ;
        }
    }
}
