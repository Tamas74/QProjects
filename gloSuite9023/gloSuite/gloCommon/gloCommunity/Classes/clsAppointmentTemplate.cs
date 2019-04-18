using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Windows.Forms;
namespace gloCommunity.Classes
{
    //class clsAppointmentTemplate
    //{
    //}




    public class AppointmentTemplateLines : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        //  private string _databaseconnectionstring = "";


        public AppointmentTemplateLines()
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

                }
            }
            disposed = true;
        }

        ~AppointmentTemplateLines()
        {
            Dispose(false);
        }

        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(AppointmentTemplateLine item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(AppointmentTemplateLine item)
        {
            bool result = false;
            AppointmentTemplateLine obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                obj = new AppointmentTemplateLine();
                obj = (AppointmentTemplateLine)_innerlist[i];
                if (obj.AppointmentTypeID == item.AppointmentTypeID)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                obj = null;
            }

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

        public AppointmentTemplateLine this[int index]
        {
            get
            {
                return (AppointmentTemplateLine)_innerlist[index];
            }
        }

        public bool Contains(AppointmentTemplateLine item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(AppointmentTemplateLine item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(AppointmentTemplateLine[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }


    public class AppointmentTemplate : IDisposable
    {

        #region " Declarations "

        private Int64 _TemplateID = 0;
        private string _TemplateName = "";
        private AppointmentTemplateLines _TemplateDetails;

        #endregion " Declarations "

        #region "Constructor & Distructor"
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = "";

        public AppointmentTemplate()
        {
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            _TemplateDetails = new AppointmentTemplateLines();


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

        ~AppointmentTemplate()
        {
            Dispose(false);
        }

        #endregion

        #region "Property Procedures"

        public Int64 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        public string TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }

        public AppointmentTemplateLines TemplateDetails
        {
            get { return _TemplateDetails; }
            set { _TemplateDetails = value; }
        }

        #endregion

    }

    public class AppointmentTemplateLine : IDisposable
    {

        #region " Declarations "

        private Int64 _nTemplateID = 0;
        private Int64 _nTemplateLineNo = 0;
        private Int64 _nAppointmentTypeID = 0;
        private string _sAppointmentTypeCode = "";
        private string _sAppointmentTypeDesc = "";
        private Int32 _nStartTime = 0;
        private Int32 _endTime = 0;
        private Int32 _nColorCode = 0;
        private Int64 _nLocationID = 0;
        private string _sLocationName = "";
        private Int64 _nDepartmentID = 0;
        private string _sDepartmentName = "";

        #endregion " Declarations "

        #region "Constructor & Distructor"

        // private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = "";

        public AppointmentTemplateLine()
        {

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

        ~AppointmentTemplateLine()
        {
            Dispose(false);
        }

        #endregion

        #region "Property Procedures"

        public Int64 TemplateID
        {
            get { return _nTemplateID; }
            set { _nTemplateID = value; }
        }

        public Int64 TemplateLineNo
        {
            get { return _nTemplateLineNo; }
            set { _nTemplateLineNo = value; }
        }

        public Int64 AppointmentTypeID
        {
            get { return _nAppointmentTypeID; }
            set { _nAppointmentTypeID = value; }
        }

        public string AppointmentTypeCode
        {
            get { return _sAppointmentTypeCode; }
            set { _sAppointmentTypeCode = value; }
        }

        public string AppointmentTypeDesc
        {
            get { return _sAppointmentTypeDesc; }
            set { _sAppointmentTypeDesc = value; }
        }

        public Int32 StartTime
        {
            get { return _nStartTime; }
            set { _nStartTime = value; }
        }

        public Int32 EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public Int32 ColorCode
        {
            get { return _nColorCode; }
            set { _nColorCode = value; }
        }

        public Int64 LocationID
        {
            get { return _nLocationID; }
            set { _nLocationID = value; }
        }

        public string LocationName
        {
            get { return _sLocationName; }
            set { _sLocationName = value; }
        }

        public Int64 DepartmentID
        {
            get { return _nDepartmentID; }
            set { _nDepartmentID = value; }
        }

        public string DepartmentName
        {
            get { return _sDepartmentName; }
            set { _sDepartmentName = value; }
        }

        #endregion
    }






    public class gloAppointmentTemplate : IDisposable
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion "Declarations "

        #region " Property Procedure "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedure "

        #region "Constructor & Distructor"

        public gloAppointmentTemplate()
        {
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
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

        public gloAppointmentTemplate(string DatabaseConnectionString)
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

        ~gloAppointmentTemplate()
        {
            Dispose(false);
        }

        #endregion

        #region Private & Public Methods

        public void Add(AppointmentTemplate oTemplate)
        {
          //  gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
           // gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
          //  oDB.Connect(false);


            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            string msg = "";
            try
            {

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@TemplateName";
                oParamater.Value =  oTemplate.TemplateName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@MachineID";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



                string strQuery = "gloComm_AB_INUP_AppointmentTemplate";
                msg = oDB.GetDataValue(strQuery).ToString();
                //return msg;
                if (msg.Contains("Template") == true)
                  //  MessageBox.Show("Tempelate Already Exist do u like to replace it");
                {
                      DialogResult Result;
                      ////Fixed Bug id : 45786 on 20130221
                      Result = MessageBox.Show("Template '" + oTemplate.TemplateName + "' already exist. Do you want to overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                      ////End        
                    if (Result == DialogResult.Yes)
                            {
                             string ID=msg.Substring(msg.IndexOf(",")+1,msg.Length-(msg.IndexOf(",")+1));    
                                AddTemplateDetail(oTemplate,ID, oTemplate.TemplateName);
                            }
                            //else
                            //{
                            
                            //}
                            //if ((msg.Contains("Template") == false))
                            //{
                            //}
                }

                else  //New Template Inserted
                {
                    AddTemplateDetail(oTemplate, msg, oTemplate.TemplateName);
                }
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
             //   return 0;
            }
            catch (Exception ex)
            {
              //  gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //clsGeneral.UpdateLog("Error  while Adding Appointment Template : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                // return 0;
            }
            finally
            {
                //oDB.Disconnect();
                //oDBParameters.Dispose();
                oDB.Dispose(); 
            }
            //return nTemplateID;
        }

        public void AddTemplateDetail(AppointmentTemplate oTemplate, string ID, string TemplateName)
        {

            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
           // bool result = false;

            try
            {
                Int64 TemplateID = 0;
                Int64 nTemplateID = 0;
                Int64 _AppTempLineNumber = 0;
                TemplateID = Convert.ToInt64(ID);
                //if (TemplateID != null)
                {
                    nTemplateID = Convert.ToInt64(TemplateID);
                    if (nTemplateID != 0)
                    {
                        string SqlQuery = "DELETE FROM AB_AppointmentTemplate_DTL WHERE nAppointmentTemplateID = " + nTemplateID + "";
                        //    oDB.Execute_Query(SqlQuery);
                        oDB.Delete_Query(SqlQuery);

                        oDB.DBParametersCol.Clear();
                        for (int i = 0; i < oTemplate.TemplateDetails.Count; i++)
                        {
                            oDB.DBParametersCol.Clear();
                            _AppTempLineNumber = _AppTempLineNumber + 1;

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.BigInt;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@nAppointmentTemplateID";
                            oParamater.Value = nTemplateID;
                            oDB.DBParametersCol.Add(oParamater);


                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.BigInt;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@nTemplateLineNo";
                            oParamater.Value = _AppTempLineNumber;
                            oDB.DBParametersCol.Add(oParamater);

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.BigInt;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@nAppointmentTypeID";
                            oParamater.Value = 0; //oTemplate.TemplateDetails[i].AppointmentTypeID;
                            oDB.DBParametersCol.Add(oParamater);


                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.VarChar;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@sAppointmentTypeCode";
                            oParamater.Value = oTemplate.TemplateDetails[i].AppointmentTypeCode;
                            oDB.DBParametersCol.Add(oParamater);

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.VarChar;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@sAppointmentTypeDesc";
                            oParamater.Value = oTemplate.TemplateDetails[i].AppointmentTypeDesc;
                            oDB.DBParametersCol.Add(oParamater);

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.BigInt;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@nStartTime";
                            oParamater.Value = oTemplate.TemplateDetails[i].StartTime;
                            oDB.DBParametersCol.Add(oParamater);

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.BigInt;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@nEndTime";
                            oParamater.Value = oTemplate.TemplateDetails[i].EndTime;
                            oDB.DBParametersCol.Add(oParamater);

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.VarChar;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@sColorCode";
                            oParamater.Value = oTemplate.TemplateDetails[i].ColorCode;
                            oDB.DBParametersCol.Add(oParamater);

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.BigInt;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@nLocationID";
                            oParamater.Value = 0;//oTemplate.TemplateDetails[i].LocationID;
                            oDB.DBParametersCol.Add(oParamater);


                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.VarChar;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@sLocationName";
                            oParamater.Value = ""; //oTemplate.TemplateDetails[i].LocationName;
                            oDB.DBParametersCol.Add(oParamater);

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.BigInt;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@nDepartmentID";
                            oParamater.Value = 0;//oTemplate.TemplateDetails[i].DepartmentID;
                            oDB.DBParametersCol.Add(oParamater);

                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.VarChar;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@sDepartmentName";
                            oParamater.Value = "";//oTemplate.TemplateDetails[i].DepartmentName;
                            oDB.DBParametersCol.Add(oParamater);


                            oParamater = new DBParameter();
                            oParamater.DataType = SqlDbType.BigInt;
                            oParamater.Direction = ParameterDirection.Input;
                            oParamater.Name = "@nClinicID";
                            oParamater.Value = clsGeneral.gClinicID;
                            oDB.DBParametersCol.Add(oParamater);


                            string strQuery = "gloComm_AB_INUP_AppointmentTemplate_DTL";
                            oDB.ExecuteNon_Query(strQuery);
                          //  result = true;

                        }

                        if (oTemplate.TemplateDetails.Count > 0)
                            MessageBox.Show("Details for Template '" + TemplateName + "' Downloaded Successfully ", clsGeneral.gstrMessageBoxCaption);
                        //MessageBox.Show()  
                    }
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting Appointment Template Detail : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

            finally
            {
                oDB.Dispose();
            }
        }




        public DataTable GetTemplates()
        {

            DataTable dtTemplates = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //string SqlQuery = "SELECT  nAppointmentTemplateID, sAppointmentTemplates FROM  AB_AppointmentTemplate_MST WHERE bISBlocked = 0";
                string SqlQuery = "SELECT  nAppointmentTemplateID, sAppointmentTemplates FROM  AB_AppointmentTemplate_MST WHERE nClinicID = " + clsGeneral.gClinicID + " ORDER BY sAppointmentTemplates ";
                //
                oDB.Retrive_Query(SqlQuery, out  dtTemplates);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //clsGeneral.UpdateLog("Error  while Getting Templates : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dtTemplates;
        }
        public DataTable GetAllocatedTemplates(Int64 ProviderId)
        {
            DataTable dtTemplates = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //string SqlQuery = "SELECT  nAppointmentTemplateID, sAppointmentTemplates FROM  AB_AppointmentTemplate_MST WHERE bISBlocked = 0";
                string SqlQuery = "SELECT   DISTINCT   sTemplateName FROM  AB_AppointmentTemplate_Allocation  WHERE nClinicID = " + this.ClinicID + " AND nASBaseId= " + ProviderId + " ";
                //
                oDB.Retrive_Query(SqlQuery, out  dtTemplates);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //clsGeneral.UpdateLog("Error  while Getting Allocated Template :" + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dtTemplates;
        }
        public DataTable GetProviders(Int64 providerID)
        {
            DataTable dtProviders = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //string SqlQuery = "SELECT  nAppointmentTemplateID, sAppointmentTemplates FROM  AB_AppointmentTemplate_MST WHERE bISBlocked = 0";
                string SqlQuery = "SELECT   DISTINCT   sASBaseDesc FROM  AB_AppointmentTemplate_Allocation  WHERE nClinicID = " + this.ClinicID + " AND nASBaseID =" + providerID + " ";
                //
                oDB.Retrive_Query(SqlQuery, out  dtProviders);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //clsGeneral.UpdateLog("Error  while Getting Providers for Appointment Template  Detail : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dtProviders;
        }
        public AppointmentTemplate GetTemplate(Int64 TemplateID)
        {//new 
            AppointmentTemplate oTemplate = new AppointmentTemplate();
            DataTable dtTemplateMST = new DataTable();
            DataTable dtTemplateDTL = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string SqlQuery = "SELECT nAppointmentTemplateID, sAppointmentTemplates FROM AB_AppointmentTemplate_MST WHERE nAppointmentTemplateID = " + TemplateID + " AND bIsBlocked = 0";

                oDB.Retrive_Query(SqlQuery, out  dtTemplateMST);

                if (dtTemplateMST.Rows.Count > 0)
                {
                    oTemplate = new AppointmentTemplate();

                    oTemplate.TemplateID = TemplateID;
                    oTemplate.TemplateName = Convert.ToString(dtTemplateMST.Rows[0]["sAppointmentTemplates"]);


                    //SqlQuery = "SELECT nAppointmentTemplateID, nTemplateLineNo, nAppointmentTypeID, sAppointmentTypeCode, sAppointmentTypeDesc, " +
                    //           " nStartTime, nEndTime, sColorCode, nLocationID, sLocationName, nDepartmentID, sDepartmentName, nClinicID " +
                    //           " FROM AB_AppointmentTemplate_DTL " +
                    //           " WHERE nAppointmentTemplateID = " + TemplateID + " ORDER BY nStartTime";

                    //Modified Vinayak for take master color change effect - 13 oct 2008
                    SqlQuery = "SELECT AB_AppointmentTemplate_DTL.nAppointmentTemplateID, AB_AppointmentTemplate_DTL.nTemplateLineNo, " +
                    " AB_AppointmentTemplate_DTL.nAppointmentTypeID, AB_AppointmentTemplate_DTL.sAppointmentTypeCode, " +
                    " AB_AppointmentTemplate_DTL.sAppointmentTypeDesc, AB_AppointmentTemplate_DTL.nStartTime, AB_AppointmentTemplate_DTL.nEndTime,  " +
                    " AB_AppointmentTemplate_DTL.sColorCode, AB_AppointmentTemplate_DTL.nLocationID, AB_AppointmentTemplate_DTL.sLocationName, " +
                    " AB_AppointmentTemplate_DTL.nDepartmentID, AB_AppointmentTemplate_DTL.sDepartmentName, AB_AppointmentTemplate_DTL.nClinicID, " +
                    " AB_AppointmentType.sColorCode AS MstColorCode " +
                    " FROM AB_AppointmentTemplate_DTL INNER JOIN AB_AppointmentType ON AB_AppointmentTemplate_DTL.nAppointmentTypeID = AB_AppointmentType.nAppointmentTypeID " +
                    " WHERE (AB_AppointmentTemplate_DTL.nAppointmentTemplateID = " + TemplateID + ") ORDER BY AB_AppointmentTemplate_DTL.nStartTime";

                    oDB.Retrive_Query(SqlQuery, out  dtTemplateDTL);
                    if (dtTemplateDTL.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTemplateDTL.Rows.Count; i++)
                        {
                            AppointmentTemplateLine oAppointmentType = new AppointmentTemplateLine();

                            oAppointmentType.TemplateID = Convert.ToInt64(dtTemplateDTL.Rows[i]["nAppointmentTemplateID"]);
                            oAppointmentType.TemplateLineNo = Convert.ToInt64(dtTemplateDTL.Rows[i]["nTemplateLineNo"]);
                            oAppointmentType.AppointmentTypeID = Convert.ToInt64(dtTemplateDTL.Rows[i]["nAppointmentTypeID"]);
                            oAppointmentType.AppointmentTypeCode = Convert.ToString(dtTemplateDTL.Rows[i]["sAppointmentTypeCode"]);
                            oAppointmentType.AppointmentTypeDesc = Convert.ToString(dtTemplateDTL.Rows[i]["sAppointmentTypeDesc"]);
                            oAppointmentType.StartTime = Convert.ToInt32(dtTemplateDTL.Rows[i]["nStartTime"]);
                            oAppointmentType.EndTime = Convert.ToInt32(dtTemplateDTL.Rows[i]["nEndTime"]);
                            if (dtTemplateDTL.Rows[i]["MstColorCode"].GetType() != null)
                            {
                                oAppointmentType.ColorCode = Convert.ToInt32(dtTemplateDTL.Rows[i]["MstColorCode"]);
                            }
                            else
                            {
                                oAppointmentType.ColorCode = Convert.ToInt32(dtTemplateDTL.Rows[i]["sColorCode"]);
                            }

                            if (dtTemplateDTL.Rows[i]["nLocationID"] != DBNull.Value)
                            {
                                oAppointmentType.LocationID = Convert.ToInt64(dtTemplateDTL.Rows[i]["nLocationID"]);
                            }
                            oAppointmentType.LocationName = Convert.ToString(dtTemplateDTL.Rows[i]["sLocationName"]);
                            if (dtTemplateDTL.Rows[i]["nDepartmentID"] != DBNull.Value)
                            {
                                oAppointmentType.DepartmentID = Convert.ToInt64(dtTemplateDTL.Rows[i]["nDepartmentID"]);
                            }
                            oAppointmentType.DepartmentName = Convert.ToString(dtTemplateDTL.Rows[i]["sDepartmentName"]);

                            oTemplate.TemplateDetails.Add(oAppointmentType);
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //clsGeneral.UpdateLog("Error  while Getting Appointment Template : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oTemplate;
        }
        public DataTable GetTemplateNames()
        {
            DataTable dtTemplateNames = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //string SqlQuery = "SELECT  nAppointmentTemplateID, sAppointmentTemplates FROM  AB_AppointmentTemplate_MST WHERE bISBlocked = 0";
                string SqlQuery = "select sAppointmentTemplates from AB_AppointmentTemplate_MST";
                //
                oDB.Retrive_Query(SqlQuery, out  dtTemplateNames);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dtTemplateNames;

        }
        public bool IsTemplateNamePresent(Int64 TemplateId, string TemplateName)
        {
            {
                // DataTable dtResult = new DataTable();

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string SqlQuery = "SELECT  COUNT (nAppointmentTemplateID) FROM AB_AppointmentTemplate_MST WHERE sAppointmentTemplates = '" + TemplateName.Replace("'", "''") + "' AND nAppointmentTemplateID <>  " + TemplateId + " ";


                    Object Count = oDB.ExecuteScalar_Query(SqlQuery);
                    if (Convert.ToInt64(Count) > 0)
                        return true;

                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    //clsGeneral.UpdateLog("Error  while Checking Template Name from  Appointment Template MST  Detail : " + ex.Message.ToString());
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                return false;
            }

        }

        public bool AddTemplateAllocation(AppointmentTemplateAllocation oAllocation)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //oDB.Connect(false);

            ////declare a variable for getting the resourceid for the inserted record.
            //Object AllocationID;
            ////Int64 nTemplateID = 0;
            //try
            //{
            //    //first insert into the resource_mst table
            //    oDBParameters.Add("@TemplateAllocationID", oAllocation.TemplateAllocationID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //    oDBParameters.Add("@ProviderID", oAllocation.ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@TemplateID", oAllocation.TemplateID, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(oAllocation.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oAllocation.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
            //    oDBParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

            //    oDB.Execute("AB_INUP_AppointmentTemplateAllocation", oDBParameters, out  AllocationID);

            //    if (AllocationID != null)
            //    {
            //        if (Convert.ToInt64(AllocationID) > 0)
            //            return true;
            //    }

            //}
            //catch (gloDatabaseLayer.DBException dbex)
            //{
            //    dbex.ERROR_Log(dbex.ToString());
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS");
            //    return false;
            //}
            //finally
            //{
            //    oDB.Disconnect();
            //    oDBParameters.Dispose();
            //    oDB.Dispose();
            //}
            return true;
        }

        public bool AddTemplateAllocations(AppointmentTemplateAllocations oAllocations)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
      //      gloDatabaseLayer.DBParameters oDBParameters;
            //Object AllocationID;
            Object MasterTemplateID = null;
            Int64 _AppTempLineNumber = 0;
            string _sqlQuery = "";
            object _IsMasterIDExist = null;
            try
            {
                oDB.Connect(false);

                //1.Delete if saving for modify
                if (oAllocations[0].TemplateAllocationMasterID > 0)
                {
                    _sqlQuery = "DELETE FROM AB_AppointmentTemplate_Allocation where nTemplateAllocationMasterID = " + oAllocations[0].TemplateAllocationMasterID;
                    oDB.ExecuteScalar_Query(_sqlQuery);
                }

                //2.Get the Max Template Master Id
                _sqlQuery = "";
                _sqlQuery = "SELECT ISNULL(nTemplateAllocationMasterID,0) AS nTemplateAllocationMasterID FROM AB_AppointmentTemplate_Allocation WHERE convert(varchar(18),nTemplateAllocationMasterID) Like convert(varchar(18)," + oDB.GetPrefixTransactionID(0) + ")+ '%'";
                _IsMasterIDExist = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_IsMasterIDExist != null && Convert.ToString(_IsMasterIDExist) != "")
                {
                    _sqlQuery = "";
                    _sqlQuery = "SELECT isnull(max(nTemplateAllocationMasterID),0)+1 FROM AB_AppointmentTemplate_Allocation where convert(varchar(18),nTemplateAllocationMasterID) Like convert(varchar(18),@MachineID)+ '%'";

                    MasterTemplateID = oDB.ExecuteScalar_Query(_sqlQuery);
                }
                else
                {
                    _sqlQuery = "";
                    _sqlQuery = "SELECT convert(numeric(18,0), convert(varchar(18)," + oDB.GetPrefixTransactionID(0) + ") + '001')";

                    MasterTemplateID = oDB.ExecuteScalar_Query(_sqlQuery);
                }


                if (MasterTemplateID == null)
                {
                    return false;
                }


                //nTemplateAllocationID, nLineNumber, sTemplateName, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag, nAppointmentTypeID,
                //sAppointmentTypeCode, sAppointmentTypeDesc, dtStartDate, dtStartTime, dtEndDate, dtEndTime, nColorCode, nLocationID,
                //sLocationName, nDepartmentID, sDepartmentName, nClinicID
                if (oAllocations != null)
                {

                    for (int i = 0; i < oAllocations.Count; i++)
                    {
                        object _lineresult = new object();
                        //  _lineresult = oDB.ExecuteScalar_Query("SELECT ISNULL(MAX(nLineNumber),0) + 1 FROM AB_AppointmentTemplate_Allocation WHERE nASBaseID = " + oAllocations[i].ASBaseID + " AND nASBaseFlag  = " + oAllocations[i].ASBaseFlag.GetHashCode() + " AND dtStartDate = " + gloDate.DateAsNumber(oAllocations[i].StartDate.ToString()) + " AND nClinicID = " + oAllocations[i].ClinicID + "");
                        if (_lineresult != null)
                        {
                            if (_lineresult.ToString() != "")
                            {
                                _AppTempLineNumber = Convert.ToInt64(_lineresult.ToString());
                            }
                        }
                        _lineresult = null;

                        //oDBParameters = new DBParameters();
                        //oDBParameters.Add("@nTemplateAllocationMasterID", Convert.ToInt64(MasterTemplateID), ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@nTemplateAllocationID", oAllocations[i].TemplateAllocationID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@nLineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@sTemplateName", oAllocations[i].TemplateName, ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@nASBaseID", oAllocations[i].ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@sASBaseCode", oAllocations[i].ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@sASBaseDesc", oAllocations[i].ASBaseDesc, ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@nASBaseFlag", oAllocations[i].ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        //oDBParameters.Add("@nAppointmentTypeID", oAllocations[i].AppointmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@sAppointmentTypeCode", oAllocations[i].AppointmentTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@sAppointmentTypeDesc", oAllocations[i].AppointmentTypeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oAllocations[i].StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oAllocations[i].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oAllocations[i].EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oAllocations[i].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@nColorCode", oAllocations[i].ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@nLocationID", oAllocations[i].LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@sLocationName", oAllocations[i].LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@nDepartmentID", oAllocations[i].DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@sDepartmentName", oAllocations[i].DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                        //oDBParameters.Add("@nClinicID", oAllocations[i].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(1), ParameterDirection.Input, SqlDbType.BigInt);

                     //   int retVal = 0;// oDB.Execute("AB_INUP_AppointmentTemplateAllocations", oDBParameters);
                    }

                }




            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //clsGeneral.UpdateLog("Error  while Adding  Appointment Template Allocation : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return true;
        }



        public static class gloTime
        {
            public static Int32 TimeAsNumber(string timevalue)
            {
                Int32 _result = 0;
                timevalue = string.Format(Convert.ToDateTime(timevalue).ToShortTimeString(), "hh:mm tt");
                try
                {
                    if (timevalue.Length <= 8)
                    {
                        if (timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim() == "AM" || timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim() == "PM")
                        {
                            string _internalresult = "";
                            string _AmPm = timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim();
                            string _Hour = timevalue.Substring(0, timevalue.IndexOf(":", 0)).ToUpper().Trim();
                            string _Minutes = timevalue.Substring(timevalue.IndexOf(":", 0) + 1, 2).ToUpper().Trim();
                            string _HourNo = "";
                            string _MinuteNo = "";

                            if (_AmPm == "PM")
                            {
                                if (_Hour != "12")
                                {
                                    _HourNo = Convert.ToString(Convert.ToInt16(_Hour) + 12);
                                }
                                else
                                {
                                    _HourNo = _Hour;
                                }
                            }
                            else
                            {
                                if (_Hour == "12")
                                {
                                    _HourNo = "";
                                }
                                else
                                {
                                    _HourNo = _Hour;
                                }

                            }
                            _MinuteNo = _Minutes;

                            _internalresult = _HourNo + _MinuteNo;
                            _result = Convert.ToInt32(_internalresult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    //clsGeneral.UpdateLog("Error  while Formatting Time string in Appointment Template : " + ex.Message.ToString());
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                return _result;
            }

            //public static Int32 TimeAsNumber(DateTime timevalue)
            //{
            //    Int32 _result = 0;
            //    try
            //    {

            //    }
            //    catch
            //    {
            //    }
            //    return _result;
            //}

            //public static string TimeAsString(Int32 timevalue)
            //{
            //    string _result = "";
            //    try
            //    {

            //    }
            //    catch
            //    {
            //    }
            //    return _result;
            //}



            //public DataTable GetTemplateAllocations(Int64 ProviderID, DateTime FromDate, DateTime ToDate)
            //{

            //    DataTable dtAllocations = new DataTable();

            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    oDB.Connect(false);
            //    try
            //    {
            //        string SqlQuery = "";
            //        //string SqlQuery = "SELECT AB_AppointmentTemplate_Allocation.nTemplateAllocationID, AB_AppointmentTemplate_MST.sAppointmentTemplates,AB_AppointmentTemplate_Allocation.nStartDate, AB_AppointmentTemplate_Allocation.nEndDate " +
            //        //                  " FROM AB_AppointmentTemplate_Allocation INNER JOIN AB_AppointmentTemplate_MST ON AB_AppointmentTemplate_Allocation.nTemplateID = AB_AppointmentTemplate_MST.nAppointmentTemplateID " +
            //        //                  " WHERE AB_AppointmentTemplate_Allocation.nProviderID = " + ProviderID;

            //        //string SqlQuery = "SELECT AB_AppointmentTemplate_Allocation.nTemplateAllocationID, AB_AppointmentTemplate_MST.sAppointmentTemplates, " +
            //        //                    " (substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),5,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),7,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),1,4) ) As StartDate, " +
            //        //                    " (substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),5,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),7,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),1,4) ) As EndDate " +
            //        //                    " FROM AB_AppointmentTemplate_Allocation INNER JOIN AB_AppointmentTemplate_MST ON AB_AppointmentTemplate_Allocation.nTemplateID = AB_AppointmentTemplate_MST.nAppointmentTemplateID " +
            //        //                    " WHERE AB_AppointmentTemplate_Allocation.nProviderID =" + ProviderID;

            //        // string SqlQuery = "SELECT AB_AppointmentTemplate_Allocation.nTemplateAllocationID, AB_AppointmentTemplate_MST.sAppointmentTemplates, " +
            //        //" (substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),5,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),7,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),1,4) ) As StartDate, " +
            //        //" (substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),5,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),7,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),1,4) ) As EndDate " +
            //        //" FROM AB_AppointmentTemplate_Allocation INNER JOIN AB_AppointmentTemplate_MST ON AB_AppointmentTemplate_Allocation.nTemplateID = AB_AppointmentTemplate_MST.nAppointmentTemplateID " +
            //        //" WHERE AB_AppointmentTemplate_Allocation.nProviderID =" + ProviderID + " AND AB_AppointmentTemplate_Allocation.nClinicID = " + this.ClinicID + " ";
            //        SqlQuery = "SELECT  nTemplateAllocationMasterID,nTemplateAllocationID,sTemplateName,nASBaseID,nColorCode," +
            //                          "sASBaseDesc,sAppointmentTypeDesc,dtStartDate,dtStartTime,dtEndDate," +
            //                          "dtEndTime,sDepartmentName FROM AB_AppointmentTemplate_Allocation WHERE (dtStartDate >= " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()) + " ) AND (dtStartDate <= " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()) + " )" +
            //                          "AND (dtEndDate >= " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()) + " ) AND (dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()) + " ) AND  nClinicID = " + this.ClinicID + " AND nASBaseID = " + ProviderID + " ORDER BY nLineNumber";


            //        oDB.Retrive_Query(SqlQuery, out  dtAllocations);

            //    }
            //    catch (gloDatabaseLayer.DBException DBErr)
            //    {
            //        DBErr.ERROR_Log(DBErr.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDB.Dispose();
            //    }
            //    return dtAllocations;
            //}

            //public DataTable GetTemplateAllocation(Int64 AllocationID)
            //{
            //    DataTable dtAllocation = new DataTable();

            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    oDB.Connect(false);
            //    try
            //    {
            //        //string SqlQuery = "SELECT AB_AppointmentTemplate_Allocation.nTemplateAllocationID, AB_AppointmentTemplate_Allocation.nStartDate, AB_AppointmentTemplate_Allocation.nEndDate, AB_AppointmentTemplate_Allocation.nTemplateID, AB_AppointmentTemplate_Allocation.nProviderID, AB_AppointmentTemplate_MST.sAppointmentTemplates " +
            //        //                  " FROM AB_AppointmentTemplate_Allocation INNER JOIN AB_AppointmentTemplate_MST ON AB_AppointmentTemplate_Allocation.nTemplateID = AB_AppointmentTemplate_MST.nAppointmentTemplateID " +
            //        //                  " WHERE AB_AppointmentTemplate_Allocation.nTemplateAllocationID = " + AllocationID;


            //        string SqlQuery = "SELECT AB_AppointmentTemplate_Allocation.nTemplateAllocationID,AB_AppointmentTemplate_Allocation.nTemplateID, AB_AppointmentTemplate_Allocation.nProviderID, AB_AppointmentTemplate_MST.sAppointmentTemplates, " +
            //                            " (substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),5,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),7,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nStartDate),1,4) ) As StartDate, " +
            //                            " (substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),5,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),7,2) + '/' + substring(convert(varchar,AB_AppointmentTemplate_Allocation.nEndDate),1,4) ) As EndDate " +
            //                            " FROM AB_AppointmentTemplate_Allocation INNER JOIN AB_AppointmentTemplate_MST ON AB_AppointmentTemplate_Allocation.nTemplateID = AB_AppointmentTemplate_MST.nAppointmentTemplateID " +
            //                            " WHERE AB_AppointmentTemplate_Allocation.nTemplateAllocationID = " + AllocationID;

            //        oDB.Retrive_Query(SqlQuery, out  dtAllocation);

            //    }
            //    catch (gloDatabaseLayer.DBException DBErr)
            //    {
            //        DBErr.ERROR_Log(DBErr.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDB.Dispose();
            //    }
            //    return dtAllocation;
            //}

            //public void DeleteTemplateAllocation(Int64 AllocationID)
            //{
            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //    oDB.Connect(false);
            //    try
            //    {

            //        string SqlQuery = "DELETE FROM AB_AppointmentTemplate_Allocation WHERE nTemplateAllocationID = " + AllocationID;
            //        oDB.Execute_Query(SqlQuery);

            //    }
            //    catch (gloDatabaseLayer.DBException dbex)
            //    {
            //        dbex.ERROR_Log(dbex.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDBParameters.Dispose();
            //        oDB.Dispose();
            //    }
            //}
            //public bool DeleteTemplateAllocation(Int64 ProviderID, string TemplateName, DateTime Startdate, DateTime Enddate)
            //{
            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //    oDB.Connect(false);
            //    try
            //    {

            //        string SqlQuery = "DELETE FROM AB_AppointmentTemplate_Allocation WHERE (dtStartDate >= " + gloDateMaster.gloDate.DateAsNumber(Startdate.ToString()) + " ) AND (dtStartDate <= " + gloDateMaster.gloDate.DateAsNumber(Enddate.ToString()) + " )" +
            //                        "AND (dtEndDate >= " + gloDateMaster.gloDate.DateAsNumber(Startdate.ToString()) + " ) AND (dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(Enddate.ToString()) + " )  AND nASBaseID = " + ProviderID + " AND sTemplateName = '" + TemplateName.ToString().Replace("'", "''") + "' ";
            //        int i = oDB.Execute_Query(SqlQuery);
            //        if (i >= 1)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }

            //    }
            //    catch (gloDatabaseLayer.DBException dbex)
            //    {
            //        dbex.ERROR_Log(dbex.ToString());
            //        return false;
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //        return false;
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDBParameters.Dispose();
            //        oDB.Dispose();
            //    }


            //}

            //public void DeleteTemplate(Int64 TemplateId)
            //{
            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //    oDB.Connect(false);
            //    try
            //    {
            //        //string SqlQuery = "UPDATE AB_AppointmentTemplate_DTL SET bISBlocked = 1 WHERE nAppointmentTemplateID = " + TemplateId;
            //        string SqlQuery = "delete from  AB_AppointmentTemplate_DTL where  nAppointmentTemplateID = " + TemplateId;
            //        oDB.Execute_Query(SqlQuery);

            //        SqlQuery = "delete from  AB_AppointmentTemplate_MST  WHERE nAppointmentTemplateID = " + TemplateId;
            //        oDB.Execute_Query(SqlQuery);

            //    }
            //    catch (gloDatabaseLayer.DBException dbex)
            //    {
            //        dbex.ERROR_Log(dbex.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDBParameters.Dispose();
            //        oDB.Dispose();
            //    }
            //}

            ////Method returns True is Template is allocated for between Start & End Date
            //public bool IsTemplateAllocated(Int64 ProviderID, Int64 AllocationID, DateTime StartDate, DateTime EndDate)
            //{
            //    DataTable dtResult = new DataTable();

            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    oDB.Connect(false);
            //    try
            //    {
            //        //string SqlQuery = "SELECT  nTemplateAllocationID FROM AB_AppointmentTemplate_Allocation WHERE nProviderID = " + ProviderID +
            //        //                  " AND nStartDate <= " + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(StartDate)) + " AND nEndDate >= " + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(StartDate)) +
            //        //                  " AND nTemplateAllocationID != " + AllocationID;
            //        //
            //        string SqlQuery = "SELECT  nTemplateAllocationID FROM AB_AppointmentTemplate_Allocation WHERE nProviderID = " + ProviderID +
            //                          " AND nStartDate <= " + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(StartDate)) + " AND nEndDate >= " + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(StartDate)) +
            //                          " AND nTemplateAllocationID != " + AllocationID + " AND nClinicID = " + this.ClinicID + " ";
            //        //


            //        oDB.Retrive_Query(SqlQuery, out  dtResult);
            //        if (dtResult.Rows.Count > 0)
            //            return true;

            //        //SqlQuery = "SELECT  nTemplateAllocationID FROM AB_AppointmentTemplate_Allocation WHERE nProviderID = " + ProviderID +
            //        //            " AND nStartDate <= " + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(EndDate)) + " AND nEndDate >= " + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(EndDate)) +
            //        //            " AND nTemplateAllocationID != " + AllocationID;
            //        //
            //        //
            //        SqlQuery = "SELECT  nTemplateAllocationID FROM AB_AppointmentTemplate_Allocation WHERE nProviderID = " + ProviderID +
            //                    " AND nStartDate <= " + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(EndDate)) + " AND nEndDate >= " + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(EndDate)) +
            //                    " AND nTemplateAllocationID != " + AllocationID + " AND nClinicID = " + this.ClinicID + " ";
            //        //

            //        oDB.Retrive_Query(SqlQuery, out  dtResult);
            //        if (dtResult.Rows.Count > 0)
            //            return true;

            //    }
            //    catch (gloDatabaseLayer.DBException DBErr)
            //    {
            //        DBErr.ERROR_Log(DBErr.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDB.Dispose();
            //    }

            //    return false;
            //}

            //public bool IsTemplateAllocated(Int64 TemplateID)
            //{
            //    DataTable dtResult = new DataTable();

            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    oDB.Connect(false);
            //    try
            //    {
            //        string SqlQuery = "SELECT  nTemplateAllocationID FROM AB_AppointmentTemplate_Allocation WHERE nTemplateAllocationID = " + TemplateID;

            //        oDB.Retrive_Query(SqlQuery, out  dtResult);
            //        if (dtResult.Rows.Count > 0)
            //            return true;

            //    }
            //    catch (gloDatabaseLayer.DBException DBErr)
            //    {
            //        DBErr.ERROR_Log(DBErr.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    }
            //    finally
            //    {
            //        oDB.Disconnect();
            //        oDB.Dispose();
            //    }

            //    return false;
            //}

        #endregion

        }


        public class AppointmentTemplateAllocation : IDisposable
        {
            #region " Declarations "

            private Int64 _nTemplateAllocationMasterID = 0;
            private Int64 _nTemplateAllocationID = 0;
            private Int64 _nLineNumber = 0;
            private string _sTemplateName = "";
            private Int64 _nASBaseID = 0;
            private string _sASBaseCode = "";
            private string _sASBaseDesc = "";
            private int _nASBaseFlag;
            private Int64 _nAppointmentTypeID = 0;
            private string _sAppointmentTypeCode = "";
            private string _sAppointmentTypeDesc = "";
            private DateTime _dtStartDate;
            private DateTime _dtStartTime;
            private DateTime _dtEndDate;
            private DateTime _dtEndTime;
            private Int32 _nColorCode = 0;
            private Int64 _nLocationID = 0;
            private string _sLocationName = "";
            private Int64 _nDepartmentID = 0;
            private string _sDepartmentName = "";
            private Int64 _nClinicID = 0;
            private string _MessageBoxCaption = string.Empty;

            #endregion " Declarations "

            #region "Constructor & Distructor"

            public AppointmentTemplateAllocation()
            {
                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

            ~AppointmentTemplateAllocation()
            {
                Dispose(false);
            }

            #endregion

            #region "Property Procedures"

            public Int64 TemplateAllocationMasterID
            {
                get { return _nTemplateAllocationMasterID; }
                set { _nTemplateAllocationMasterID = value; }
            }

            public Int64 TemplateAllocationID
            {
                get { return _nTemplateAllocationID; }
                set { _nTemplateAllocationID = value; }

            }

            public Int64 LineNumber
            {
                get { return _nLineNumber; }
                set { _nLineNumber = value; }

            }

            public string TemplateName
            {
                get { return _sTemplateName; }
                set { _sTemplateName = value; }
            }

            public Int64 ASBaseID
            {
                get { return _nASBaseID; }
                set { _nASBaseID = value; }
            }

            public string ASBaseCode
            {
                get { return _sASBaseCode; }
                set { _sASBaseCode = value; }
            }

            public string ASBaseDesc
            {
                get { return _sASBaseDesc; }
                set { _sASBaseDesc = value; }
            }

            public int ASBaseFlag
            {
                get { return _nASBaseFlag; }
                set { _nASBaseFlag = value; }
            }

            public Int64 AppointmentTypeID
            {
                get { return _nAppointmentTypeID; }
                set { _nAppointmentTypeID = value; }
            }

            public string AppointmentTypeCode
            {
                get { return _sAppointmentTypeCode; }
                set { _sAppointmentTypeCode = value; }
            }

            public string AppointmentTypeDesc
            {
                get { return _sAppointmentTypeDesc; }
                set { _sAppointmentTypeDesc = value; }
            }

            public DateTime StartDate
            {
                get { return _dtStartDate; }
                set { _dtStartDate = value; }
            }

            public DateTime StartTime
            {
                get { return _dtStartTime; }
                set { _dtStartTime = value; }
            }

            public DateTime EndDate
            {
                get { return _dtEndDate; }
                set { _dtEndDate = value; }
            }

            public DateTime EndTime
            {
                get { return _dtEndTime; }
                set { _dtEndTime = value; }
            }

            public Int32 ColorCode
            {
                get { return _nColorCode; }
                set { _nColorCode = value; }
            }

            public Int64 LocationID
            {
                get { return _nLocationID; }
                set { _nLocationID = value; }
            }

            public string LocationName
            {
                get { return _sLocationName; }
                set { _sLocationName = value; }
            }

            public Int64 DepartmentID
            {
                get { return _nDepartmentID; }
                set { _nDepartmentID = value; }
            }

            public string DepartmentName
            {
                get { return _sDepartmentName; }
                set { _sDepartmentName = value; }
            }

            public Int64 ClinicID
            {
                get { return _nClinicID; }
                set { _nClinicID = value; }
            }


            #endregion

        }

        public class AppointmentTemplateAllocations : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Distructor"

            public AppointmentTemplateAllocations()
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

                    }
                }
                disposed = true;
            }

            ~AppointmentTemplateAllocations()
            {
                Dispose(false);
            }

            #endregion

            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(AppointmentTemplateAllocation item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(AppointmentTemplateAllocation item)
            {
                bool result = false;
                AppointmentTemplateAllocation obj;

                for (int i = 0; i < _innerlist.Count; i++)
                {
                    //store current index being checked
                    obj = new AppointmentTemplateAllocation();
                    obj = (AppointmentTemplateAllocation)_innerlist[i];
                    if (obj.TemplateAllocationID == item.TemplateAllocationID)
                    {
                        _innerlist.RemoveAt(i);
                        result = true;
                        break;
                    }
                    obj = null;
                }

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

            public AppointmentTemplateAllocation this[int index]
            {
                get
                {
                    return (AppointmentTemplateAllocation)_innerlist[index];
                }
            }

            public bool Contains(AppointmentTemplateAllocation item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(AppointmentTemplateAllocation item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(AppointmentTemplateAllocation[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }
        }



    }



    static class gloTime
    {



        public static Int32 TimeAsNumber(string timevalue)
        {
            Int32 _result = 0;
            timevalue = string.Format(Convert.ToDateTime(timevalue).ToShortTimeString(), "hh:mm tt");
            try
            {
                if (timevalue.Length <= 8)
                {
                    if (timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim() == "AM" || timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim() == "PM")
                    {
                        string _internalresult = "";
                        string _AmPm = timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim();
                        string _Hour = timevalue.Substring(0, timevalue.IndexOf(":", 0)).ToUpper().Trim();
                        string _Minutes = timevalue.Substring(timevalue.IndexOf(":", 0) + 1, 2).ToUpper().Trim();
                        string _HourNo = "";
                        string _MinuteNo = "";

                        if (_AmPm == "PM")
                        {
                            if (_Hour != "12")
                            {
                                _HourNo = Convert.ToString(Convert.ToInt16(_Hour) + 12);
                            }
                            else
                            {
                                _HourNo = _Hour;
                            }
                        }
                        else
                        {
                            if (_Hour == "12")
                            {
                                _HourNo = "";
                            }
                            else
                            {
                                _HourNo = _Hour;
                            }

                        }
                        _MinuteNo = _Minutes;

                        _internalresult = _HourNo + _MinuteNo;
                        _result = Convert.ToInt32(_internalresult);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //clsGeneral.UpdateLog("Error  while Formatting Time in Appointment Template Detail : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            return _result;
        }







        public static DateTime TimeAsDateTime(DateTime SupportingDate, Int32 timevalue)
        {
            DateTime _result = DateTime.Now;
            try
            {
                string _timeValue = timevalue.ToString();

                if (_timeValue.Length > 2 && _timeValue.Length <= 4)
                {
                    string _AmPm = "";
                    int _Hour = 0;// Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim());
                    int _Minutes = 0;// Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim());

                    if (timevalue > 0)
                    { _Hour = Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim()); }

                    if (timevalue > 0)
                    { _Minutes = Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim()); }

                    string _internalresult;

                    if (_Hour < 12)
                    {
                        _AmPm = "AM";
                    }
                    else if (_Hour >= 12)
                    {
                        _AmPm = "PM";
                    }

                    _internalresult = _Hour.ToString() + ":" + _Minutes.ToString() + " " + _AmPm;

                    _result = Convert.ToDateTime(SupportingDate.Date.ToString("MM/dd/yyyy") + " " + _internalresult);
                }
                else if (_timeValue.Length <= 2)
                {
                    string _AmPm = "";
                    int _Hour = 0;// Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim());
                    int _Minutes = 0;// Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim());

                    _Hour = 0;

                    _Minutes = Convert.ToInt16(_timeValue);

                    string _internalresult;

                    if (_Hour < 12)
                    {
                        _AmPm = "AM";
                    }
                    else if (_Hour >= 12)
                    {
                        _AmPm = "PM";
                    }

                    _internalresult = _Hour.ToString() + ":" + _Minutes.ToString() + " " + _AmPm;

                    _result = Convert.ToDateTime(SupportingDate.Date.ToString("MM/dd/yyyy") + " " + _internalresult);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //clsGeneral.UpdateLog("Error  while Formatting Time in  Appointment Template Detail : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            return _result;
        }

        public static bool IsValidTime(string timevalue)
        {
            bool _result = false;
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            return _result;
        }

        public static Int64 GetMinutes(Int64 nMinutes)
        {
            nMinutes = System.Math.Abs(nMinutes);
            Int64 nHrs = 0;
            Int64 nMins = 0;

            nHrs = (nMinutes / 100);
            nMins = (nMinutes % 100);
            return (nHrs * 60) + nMins;
        }

        public static Int64 GetMinutes(Int64 nMinute1, Int64 nMinute2)
        {
            Int64 nMinutes;
            Int64 nHrs = 0;
            Int64 nMins = 0;

            nMinutes = System.Math.Abs(nMinute1 - nMinute2);

            nHrs = (nMinutes / 100);
            nMins = (nMinutes % 100);
            return (nHrs * 60) + nMins;
        }


    }

}
