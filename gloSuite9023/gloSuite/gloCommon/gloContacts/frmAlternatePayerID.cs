using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Collections;

namespace gloContacts
{
    public partial class frmAlternatePayerID : Form
    {
        #region   "Declararions"
        private Int64 _ContactID = 0;
        private Int64 _AlternateUniqueID = 0;
        private string _databaseconnectionstring;
        public string _MessageBoxCaption = String.Empty;
        public BindingList<AlternatePayerID> ObjAlternatePayerIDs;
        public bool isEdit =false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        int indexofSelectedRow=-1;
        private bool _IsModified = false;
        public bool IsModified
        {
            get { return _IsModified; }
            set { _IsModified = value; }
        }
        #endregion

        #region "Constructors"
        public frmAlternatePayerID(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

        }
        public frmAlternatePayerID(string databaseconnectionstring,Int64 ContactID,Int64 AlternateUniqueID)
        {
            InitializeComponent();
            _ContactID = ContactID;
            _AlternateUniqueID = AlternateUniqueID; 
            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

        }

        public frmAlternatePayerID(string databaseconnectionstring, Int64 ContactID, Int64 AlternateUniqueID,bool isEdit,int indexofSelectedRow)
        {
            InitializeComponent();
            _ContactID = ContactID;
            _AlternateUniqueID = AlternateUniqueID;
            _databaseconnectionstring = databaseconnectionstring;
            this.isEdit = isEdit;
             this.indexofSelectedRow  =indexofSelectedRow;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

        }
        #endregion  

        #region "Form Load Event"
        private void frmAlternatePayerID_Load(object sender, EventArgs e)
        {
          
            //if (_AlternateUniqueID != 0)
            //{
            //    DataTable _dt = new DataTable();
            //    _dt = GetAlternatePayerID(_ContactID, _AlternateUniqueID);
            //    txtName.Text = _dt.Rows[0]["Name"].ToString();
            //    txtPayerID.Text = _dt.Rows[0]["AlternatePayerID"].ToString();
            //    txtdescription.Text = _dt.Rows[0]["Description"].ToString();

            //}
            //else
            //{
            AlternatePayerID _obAlternatePayerID = null;
            try
            {
                if (indexofSelectedRow >= 0)
                {
                    _obAlternatePayerID = ObjAlternatePayerIDs[indexofSelectedRow];
                    txtName.Text = _obAlternatePayerID.Name;
                    txtPayerID.Text = _obAlternatePayerID.AlternatePayerId;
                    txtdescription.Text = _obAlternatePayerID.Desc;
                }
                else
                {
                    txtName.Text = "";
                    txtPayerID.Text = "";
                    txtdescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                _obAlternatePayerID = null;
            }
            //}

        }
        
        
        #endregion

        #region "Form  Control Events "
        #region 'Tool Strip Events'

        private void ts_Commands_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            AlternatePayerID _objObjAlternatePayerID = null;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "SaveClose":
                        if (Validatedata())
                        {
                            bool DuplicatePayerId = false;
                            for (int i = 0; i < ObjAlternatePayerIDs.Count; i++)
                            {
                                _objObjAlternatePayerID = ObjAlternatePayerIDs[i];
                                if (_objObjAlternatePayerID.AlternatePayerId.ToLower() == txtPayerID.Text.ToString().Trim().ToLower() && i != indexofSelectedRow)
                                {

                                    DuplicatePayerId = true;
                                    MessageBox.Show("PayerID " + txtPayerID.Text.ToString().Trim() + " is already exist.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;

                                }


                            }
                            if (!DuplicatePayerId)
                            {
                                save(_ContactID, txtName.Text.ToString().Trim(), txtPayerID.Text.ToString().Trim(), txtdescription.Text.ToString().Trim());
                                this.Close();
                            }
                            /*if (save(_ContactID, txtName.Text.ToString().Trim(), txtPayerID.Text.ToString().Trim(), txtdescription.Text.ToString().Trim()) > 0)
                                this.Close();
                            else
                                MessageBox.Show("PayerID "+txtPayerID.Text.ToString().Trim()+ " is already exist.",_MessageBoxCaption ,MessageBoxButtons.OK ,MessageBoxIcon.Information);  
                          */
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                _objObjAlternatePayerID = null; 
            }

        }

         #endregion

             #endregion

        # region "Private Method"
        private DataTable GetAlternatePayerID(Int64 contactID, Int64 AlternateUniqueID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dt = null;
            string _Query = "";
            try
            {
                _Query = "Select TOP 1  ISNULL(sName,'') AS Name , ISNULL(sAlternatePayerId,'') AS AlternatePayerID, ISNULL(sDescription,'') AS Description from ERA_AlternatePayerID WHERE  nContactID=" + _ContactID + "AND nAlternateID=" + AlternateUniqueID + "";
                oDB.Connect(false);
                oDB.Retrive_Query(_Query, out _dt);
                return _dt;
                
              
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                
                dbEx.ERROR_Log(dbEx.ToString());
                return null; 

            }
            catch (Exception ex)
            {
                
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null; 

            }
            finally
            {
                _Query = null;
                oDB.Disconnect();               
                oDB.Dispose();
            }
        }
        
        private void  save(Int64 ContactID, String Name ,String PayerId,String Descriptionn)
        {
            bool SamePayerID = false;
            for (int i = 0; i < ObjAlternatePayerIDs.Count; i++)
            {
                AlternatePayerID _ObjAlternatePayerID = ObjAlternatePayerIDs[i];
                if (indexofSelectedRow == i && this.isEdit == true)
                {
                    if (ObjAlternatePayerIDs[i].Name == Name && ObjAlternatePayerIDs[i].AlternatePayerId == PayerId && ObjAlternatePayerIDs[i].Desc == Descriptionn  && !_IsModified)
                        _IsModified = false  ;
                    else
                        _IsModified = true  ;  
                   
                    ObjAlternatePayerIDs[i].Name = Name;
                    ObjAlternatePayerIDs[i].AlternatePayerId = PayerId;
                    ObjAlternatePayerIDs[i].Desc = Descriptionn;
                    SamePayerID = true;
                      
                }
            }
            if (!SamePayerID)
            {
                AlternatePayerID _objAlternatePayerID = new AlternatePayerID();
                _objAlternatePayerID.Name = Name;
                _objAlternatePayerID.AlternatePayerId = PayerId;
                _objAlternatePayerID.Desc = Descriptionn;
                ObjAlternatePayerIDs.Add(_objAlternatePayerID);
                _objAlternatePayerID = null;
                _IsModified = true;  
            }
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            //Object _oResult = new object();
            //int Count = 0;
            //try
            //{
            //    //Pass 0 to Add
            //    oDB.Connect(false);
            //    oParameters.Add("@ID", _AlternateUniqueID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oParameters.Add("@ContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oParameters.Add("@sName", Name, ParameterDirection.Input, SqlDbType.VarChar, 100);
            //    oParameters.Add("@sPayerID", PayerId, ParameterDirection.Input, SqlDbType.VarChar, 100);
            //    oParameters.Add("@sDescription", Descriptionn, ParameterDirection.Input, SqlDbType.VarChar, 100);
            //    oParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                
            //    Count = oDB.Execute("ERA_IN_AlternatePayerID", oParameters, out _oResult);
                 
            //    return Count;

            //}
            //catch (gloDatabaseLayer.DBException dbex)
            //{
            //    dbex.ERROR_Log(dbex.ToString());
            //    return 0;
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    return 0;
            //}
            //finally
            //{
            //    oDB.Disconnect();
            //    oDB.Dispose();
            //    oParameters.Dispose();
            //   _oResult = null;
            //}
        }

        private bool  Validatedata()
        {
            if (txtPayerID.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Enter Alternate PayerID. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPayerID.Focus();
                return false;
            }
            else
                return true;
        }
        # endregion

    }


    public class AlternatePayerID 
    {
        Int64 _nId;
        string _sName;
        string _Desc;
        string _sAlternatePayerId;
        

        public Int64 ID
        {
            set {
                this._nId = value;
            }
            get 
            {
                return _nId;
            }
        }

     

        public string Name
        {
            set
            {
                this._sName = value;
            }
            get
            {
                return _sName;
            }
        }


        public string AlternatePayerId
        {
            set
            {
                this._sAlternatePayerId = value;
            }
            get
            {
                return _sAlternatePayerId;
            }
        }
        public string Desc
        {
            set
            {
                this._Desc = value;
            }
            get
            {
                return _Desc;
            }
        }
    }

    public  class AlternatePayerIDs : IDisposable
    {
        protected System.Collections.ArrayList _innerlist;

        #region " Constructor & Distructor "

        public AlternatePayerIDs()
        {
            _innerlist = new ArrayList();

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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~AlternatePayerIDs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(AlternatePayerID item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(AlternatePayerID item)
        {
            bool result = false;


            return result;
        }
        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }
        public void Clear()
        {
            _innerlist.Clear();
        }
        public AlternatePayerID this[int index]
        {
            get
            { return (AlternatePayerID)_innerlist[index]; }
        }
        public bool Contains(AlternatePayerID item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(AlternatePayerID item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(AlternatePayerID[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }
}