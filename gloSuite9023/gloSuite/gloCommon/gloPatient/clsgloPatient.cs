using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;
using gloPatientPortalCommon;
namespace gloPatient
{

    #region "Enum Declarations"

    public enum PatientOtherContactType
    {
        None = 0,
        Guarantor = 1,
        Patient = 2,
        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        Mother = 3,
        Father = 4,
        SameAsPatient = 5,
        OtherGuardian = 6
    }
    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
    public enum GuarantorType
    {
        Personal = 0,
        Commercial = 1
    }

    public enum TypeOfBilling
    {
        None = 0,
        Electronic = 1,
        Paper = 2
    }

    public enum StudentStatus
    {
        FullTime = 1,
        PartTime = 2
    }

    public enum PatientContactType
    {
        None = 0,
        Pharmacy = 1,
        PrimaryCarePhysician = 2,
        Referral = 3,
        CareTeam = 4,
    }

    public enum WorkersComp
    {
        None = 0,
        WokersComp = 1,
        Autoclaim = 2,
    }

    public enum ModifyPatientDetailType
    {
        None = 0,
        Insurance = 1,
        Guarantor = 2,
        Guardian = 3,
        Occupation = 4,
        OtherInfo = 5,
        Referral = 6
    }

    #endregion

    public class gloPatient : IDisposable
    {

        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = "gloPM";
        private String _username = "";
        public string _result = "12345";
        public Int32 _UseSitePrefix = 0;
        public gloPatient(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }


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

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _username = Convert.ToString(appSettings["UserName"]);
                }
            }
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

        ~gloPatient()
        {
            Dispose(false);
        }

        #endregion

        Object patientID;
        public Int64 Id = 0;
        public Boolean ShowSaveAsCopyButton { get; set; }
        public static Boolean SendHL7Details = false;
        private Boolean bBadDebtStatus { get; set; }
        private Form frmParentForm;

        //17-Nov-15 Aniket: Resolving Bug #90191: gloEMR: OB vitals- View OB vitals winow looses focus and does not allow to perform any operation, user has to kill application
        public Form ParentForm
        {
            get { return frmParentForm; }
            set { frmParentForm = value; }
        }

        //Added Rahul for get Guardian Information on 20101116
        public DataTable GetGuardianInformation(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for Quick Review Panel.
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GuardianInfo", oParameters, out dt);
                return dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                // dt.Dispose();
            }
        }


        public DataTable GetMUPatHosp(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for Quick Review Panel.
                oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_getMU_Pat_HOSP", oParameters, out dt);
                return dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters.Dispose();
                    oParameters = null;
                }
                // dt.Dispose();
            }
        }


        public DataTable GetRecentPatient(Int64 UserID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for Quick Review Panel.
                oParameters.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_getRecentPatientUserwise", oParameters, out dt);
                return dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters.Dispose();
                    oParameters = null;
                }
                // dt.Dispose();
            }
        }



        public void SaveMUPatHosp(Int64 PatientID,DataTable dtPatHosp)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for Quick Review Panel.
          
                oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@LoginProviderID",gloGlobal.gloPMGlobal.LoginProviderID, ParameterDirection.Input, SqlDbType.BigInt);  
                oParameters.Add("@TVP_MU_Pat_Hosp", dtPatHosp, ParameterDirection.Input, SqlDbType.Structured );
              
                oDB.Execute("gsp_UPMU_Pat_HOSP", oParameters);
              

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Clear();
                    oParameters.Dispose();
                    oParameters = null; 
                }
                    // dt.Dispose();
            }
        }


        //
        public Int64 Add(Patient oPatient)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBParameter oDBParameter;

          
            try
            {
                oDB.Connect(false);

                // PatientID
                oDBParameters.Add("@nPatientID", oPatient.DemographicsDetail.PatientID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                //@sPatientCode
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sPatientCode";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientCode;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //
                // //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;

                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@nClinicID";
                oDBParameter.Value = oPatient.ClinicID;
                oDBParameter.DataType = SqlDbType.BigInt;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //


                //@sFirstName
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFirstName";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientFirstName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMiddleName
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMiddleName";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientMiddleName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sLastName
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sLastName";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientLastName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@nSSN
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@nSSN";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientSSN;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@dtDOB
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@dtDOB";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientDOB;
                oDBParameter.DataType = System.Data.SqlDbType.DateTime;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //, @sGender
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGender";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientGender;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMaritalStatus
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMaritalStatus";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientMaritalStatus;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sAddressLine1
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sAddressLine1";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientAddress1;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sAddressLine2
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sAddressLine2";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientAddress2;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sCity
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sCity";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientCity;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sState
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sState";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientState;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sZIP
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sZIP";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientZip;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sCounty
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sCounty";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientCounty;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sPhone
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sPhone";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientPhone;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //, @sMobile
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMobile";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientMobile;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //, @sEmail,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmail";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientEmail;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sFAX,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFAX";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientFax;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sOccupation, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sOccupation";
                oDBParameter.Value = oPatient.OccupationDetail.Occupation;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sEmploymentStatus, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmploymentStatus";
                oDBParameter.Value = oPatient.OccupationDetail.PatientEmploymentStatus;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sPlaceofEmployment, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sPlaceofEmployment";
                oDBParameter.Value = oPatient.OccupationDetail.PatientPlaceofEmployment;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);


                //Code added on 14th May 2008 by Sagar Ghodke

                //Occupation Details - @sEmployerName
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmployerName";
                oDBParameter.Value = oPatient.OccupationDetail.EmployerName;
                oDBParameter.DataType = SqlDbType.VarChar;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //Occupation Details - @dtRetirementDate
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@dtRetirementDate";
                //if (oPatient.OccupationDetail.RetirementDate != DateTime.MinValue)
                //{
                //    oDBParameter.Value = oPatient.OccupationDetail.RetirementDate;
                //}
                if (oPatient.OccupationDetail.RetirementDate != null && oPatient.OccupationDetail.RetirementDate != DateTime.MinValue)
                {
                    oDBParameter.Value = oPatient.OccupationDetail.RetirementDate;
                }
                else
                {
                    oDBParameter.Value = DBNull.Value;
                }
                oDBParameter.DataType = SqlDbType.DateTime;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //Occupation Details - @sEmploymentType
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmploymentType";
                oDBParameter.Value = oPatient.OccupationDetail.EmploymentType;
                oDBParameter.DataType = SqlDbType.VarChar;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //


                //@sWorkAddressLine1,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkAddressLine1";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkAddress1;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sWorkAddressLine2, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkAddressLine2";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkAddress2;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sWorkCity,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkCity";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkCity;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sWorkState, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkState";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkState;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sWorkZIP, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkZIP";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkZip;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sWorkPhone,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkPhone";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkPhone;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sWorkFAX, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkFAX";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkFax;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sWorkCounty, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkCounty";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkCounty;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sWorkMobile,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkMobile";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkMobile;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sWorkEmail, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sWorkEmail";
                oDBParameter.Value = oPatient.OccupationDetail.PatientWorkEmail;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);




                //@sChiefComplaints,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sChiefComplaints";
                oDBParameter.Value = "";
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @nProviderID, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@nProviderID";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientProviderID;
                oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                ////@nPCPId,
                //oDBParameter = new gloDatabaseLayer.DBParameter();
                //oDBParameter.ParameterName = "@nPCPId";
                //oDBParameter.Value = oPatient.DemographicsDetail.PatientPCPId;
                //oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                //oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                //oDBParameters.Add(oDBParameter);

                // @sGuarantor,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuarantor";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientGuarantor;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //// @nPharmacyID,
                //oDBParameter = new gloDatabaseLayer.DBParameter();
                //oDBParameter.ParameterName = "@nPharmacyID";
                //oDBParameter.Value = oPatient.DemographicsDetail.PatientPharmacyID;
                //oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                //oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                //oDBParameters.Add(oDBParameter);

                // @sSpouseName, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sSpouseName";
                oDBParameter.Value = "";
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sSpousePhone,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sSpousePhone";
                oDBParameter.Value = "";
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sRace, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sRace";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientRace;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);
                // @sLang paratemeter added to patient table to store patient language, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sLang";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientLanguage;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sCommunicationPreference ,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sCommunicationPreference";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientCommunicationPrefence;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);


                // @sEthn, paratemeter added to patient table to store PatientEthnicities
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEthn";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientEthnicities;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sEthn, paratemeter added to patient table to store PatientEthnicities
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sPrefix";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientPrefix;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sPatientStatus,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sPatientStatus";
                if (oPatient.PatientDemographicOtherInfo != null)
                {
                    oDBParameter.Value = oPatient.PatientDemographicOtherInfo.Status;
                }
                else
                {
                    oDBParameter.Value = "";
                }
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);


                //Start/Old Commented Code
                //// @iPhoto,
                //if (oPatient.DemographicsDetail.PatientPhoto != null)
                //{
                //    oDBParameter = new gloDatabaseLayer.DBParameter();
                //    oDBParameter.ParameterName = "@iPhoto";
                //    System.Drawing.Image ilogo = oPatient.DemographicsDetail.PatientPhoto;
                //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //    ilogo.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //    Byte[] arrImage = ms.GetBuffer();
                //    oDBParameter.Value = arrImage;
                //    oDBParameter.DataType = System.Data.SqlDbType.Image;
                //    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                //    oDBParameters.Add(oDBParameter);
                //    ms.Close();
                //}
                //End/Old Commented Code


                //if (oPatient.DemographicsDetail.PatientPhoto != null)
                //{
                    //try
                    //{
                    //    oDBParameter = new gloDatabaseLayer.DBParameter();
                    //    if (oDBParameter != null)
                    //    {
                    //        oDBParameter.ParameterName = "@iPhoto";
                    //        oDBParameter.DataType = System.Data.SqlDbType.Image;
                    //        oDBParameter.Value = oPatient.DemographicsDetail.MyPictureBoxControl;//Start'GLO2010-0007047[BJMC]: Webcam image too small
                    //        oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    //        oDBParameters.Add(oDBParameter);
                    //        oDBParameter.Dispose();
                    //        oDBParameter = null;
                    //    }
                    //}

                    //catch (Exception ex)
                    //{
                    //    if (oDBParameter != null)
                    //    {
                    //        oDBParameter.Dispose();
                    //        oDBParameter = null;
                    //    }
                    //    string _ErrorMessage = ex.ToString();

                    //}
                //}


                ////Added by Anil on 20090713
                //Patient Signature
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@iSignature";
                oDBParameter.Value = null;
                oDBParameter.DataType = System.Data.SqlDbType.Image;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //Emergency RelatioshipCode
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmergencyRelationshipCode";
                oDBParameter.Value = oPatient.DemographicsDetail.EmergencyRelationshipCode;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //Emergency Relatioship description
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmergencyRelationshipDesc";
                oDBParameter.Value = oPatient.DemographicsDetail.EmergencyRelationshipDesc;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                ///////

                // @dtRegistrationDate,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@dtRegistrationDate";
                oDBParameter.Value = DateTime.Now;
                oDBParameter.DataType = System.Data.SqlDbType.DateTime;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);


                //@dtInjuryDate, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@dtInjuryDate";
                oDBParameter.Value = DateTime.Now;
                oDBParameter.DataType = System.Data.SqlDbType.DateTime;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@dtSurgeryDate, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@dtSurgeryDate";
                oDBParameter.Value = DateTime.Now;
                oDBParameter.DataType = System.Data.SqlDbType.DateTime;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sHandDominance,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sHandDominance";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientHandDominance;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sLocation,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sLocation";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientLocation;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_fName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_fName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherFirstName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_mName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_mName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMiddleName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_lName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_lName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherLastName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_maidenfName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_maidenfName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMaidenFirstName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_maidenmName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_maidenmName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMaidenMiddleName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_maidenlName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_maidenlName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMaidenLastName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_Address1, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_Address1";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherAddress1;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sMother_Address2, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_Address2";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherAddress2;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sMother_City,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_City";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherCity;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_State,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_State";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherState;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_ZIP,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_ZIP";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherZip;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_County, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_County";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherCounty;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sMother_Phone,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_Phone";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherPhone;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_Mobile, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_Mobile";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMobile;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sMother_FAX,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_FAX";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherFAX;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMother_Email,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMother_Email";
                oDBParameter.Value = oPatient.GuardianDetail.PatientMotherEmail;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sFather_fName, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_fName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherFirstName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sFather_mName, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_mName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherMiddleName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sFather_lName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_lName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherLastName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sFather_Address1, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_Address1";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherAddress1;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sFather_Address2,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_Address2";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherAddress2;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sFather_City,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_City";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherCity;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sFather_State,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_State";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherState;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sFather_ZIP,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_ZIP";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherZip;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sFather_County, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_County";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherCounty;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sFather_Phone, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_Phone";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherPhone;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sFather_Mobile,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_Mobile";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherMobile;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sFather_FAX,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_FAX";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherFAX;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sFather_Email, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sFather_Email";
                oDBParameter.Value = oPatient.GuardianDetail.PatientFatherEmail;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sGuardian_fName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_fName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianFirstName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sGuardian_mName, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_mName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianMiddleName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sGuardian_lName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_lName";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianLastName;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sGuardian_Address1, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_Address1";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianAddress1;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sGuardian_Address2,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_Address2";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianAddress2;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sGuardian_City,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_City";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianCity;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sGuardian_State,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_State";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianState;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sGuardian_ZIP,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_ZIP";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianZip;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sGuardian_County,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_County";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianCounty;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sGuardian_Phone, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_Phone";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianPhone;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sGuardian_Mobile, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_Mobile";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianMobile;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sGuardian_FAX, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_FAX";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianFAX;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sGuardian_Email, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_Email";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianEmail;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);


                //Start :: Guardian Relationship
                //sGuardian_RelationshipCD
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_RelationshipCD";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianRelationCD;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //sGuardian_RelationshipDS
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sGuardian_RelationshipDS";
                oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianRelationDS;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);
                //End :: Guardian Relationship

                //@nPatientDirective,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@nPatientDirective";
                oDBParameter.Value = oPatient.DemographicsDetail.Directive;
                oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@nExemptFromReport,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@nExemptFromReport";
                oDBParameter.Value = oPatient.DemographicsDetail.ExemptFromReport;
                oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sExternalCode,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sExternalCode";
                oDBParameter.Value = "";
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sUserName,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sUserName";
                oDBParameter.Value = _username;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                // @sMachineName, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sMachineName";
                oDBParameter.Value = Convert.ToString(oDB.GetPrefixTransactionID(oPatient.DemographicsDetail.PatientID));
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sInsuranceNotes, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sInsuranceNotes";
                oDBParameter.Value = oPatient.DemographicsDetail.InsuranceNotes;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@bitIsGuarantor, 
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@bitIsGuarantor";
                oDBParameter.Value = oPatient.DemographicsDetail.IsGuarantor;
                oDBParameter.DataType = System.Data.SqlDbType.Bit;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);


                // @nGuarantorID,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@nGuarantorID";
                oDBParameter.Value = oPatient.DemographicsDetail.PatientGuarantorID;
                oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);



                //@bitIsBlock
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@bitIsBlock";
                oDBParameter.Value = false;
                oDBParameter.DataType = System.Data.SqlDbType.Bit;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@IsNOTRegDate Bit,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@IsNOTRegDate";
                oDBParameter.Value = "true";
                oDBParameter.DataType = System.Data.SqlDbType.Bit;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@IsNOTInjuryDate Bit,
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@IsNOTInjuryDate";
                oDBParameter.Value = "true";
                oDBParameter.DataType = System.Data.SqlDbType.Bit;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@IsNOTSurgeryDate Bit
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@IsNOTSurgeryDate";
                oDBParameter.Value = "true";
                oDBParameter.DataType = System.Data.SqlDbType.Bit;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);


                //@IsWorkersComp Bit
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@IsWorkersComp";
                oDBParameter.Value = "false";
                oDBParameter.DataType = System.Data.SqlDbType.Bit;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@WorkersCompClaimNo varchar
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@WorkersCompClaimNo";
                oDBParameter.Value = "";
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@IsWorkersComp Bit
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@IsAutoClaim";
                oDBParameter.Value = "false";
                oDBParameter.DataType = System.Data.SqlDbType.Bit;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@WorkersCompClaimNo varchar
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@AutoClaimNo";
                oDBParameter.Value = "";
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sEmergencyContact varchar
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmergencyContact";
                oDBParameter.Value = oPatient.DemographicsDetail.EmergencyContact;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sEmergencyPhone varchar
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmergencyPhone";
                oDBParameter.Value = oPatient.DemographicsDetail.EmergencyPhone;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //@sEmergencyMobile varchar
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@sEmergencyMobile";
                oDBParameter.Value = oPatient.DemographicsDetail.EmergencyMobile;
                oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                //bSetToCollection Bit
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@bSetToCollection";
                oDBParameter.Value = oPatient.DemographicsDetail.SetToCollection;
                oDBParameter.DataType = System.Data.SqlDbType.Bit;
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                oDBParameters.Add("sCountry", oPatient.DemographicsDetail.PatientCountry, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("sWorkCountry", oPatient.OccupationDetail.PatientWorkCountry, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("sMother_Country", oPatient.GuardianDetail.PatientMotherCountry, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("sFather_Country", oPatient.GuardianDetail.PatientFatherCountry, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("sGuardian_Country", oPatient.GuardianDetail.PatientGuardianCountry, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sBirthTime", oPatient.DemographicsDetail.BirthTime, ParameterDirection.Input, SqlDbType.VarChar);//dtpBirthTime Settings
                oDBParameters.Add("@bIsYesLab", oPatient.DemographicsDetail.IsYesNoLab, ParameterDirection.Input, SqlDbType.Bit); //''YES/No Labs
                //7022Items: Home Billing- Added to save area code for patient
                oDBParameters.Add("@sAreaCode", oPatient.DemographicsDetail.AreaCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sSuffix", oPatient.DemographicsDetail.PatientSuffix, ParameterDirection.Input, SqlDbType.VarChar);




                oDB.Execute("PAT_INUP_Patient", oDBParameters, out  patientID);

                if (patientID != null)
                {
                    PatientPhoto oPatientPhoto = null;
                    try
                    {
                        oPatientPhoto = new PatientPhoto(_databaseconnectionstring);
                        byte[] patientPhoto = null;
                        if (oPatient.DemographicsDetail.PatientPhoto != null)
                        {
                            patientPhoto = oPatient.DemographicsDetail.MyPictureBoxControl;
                        }
                        else
                        {
                            patientPhoto = null;
                        }
                        oPatientPhoto.InsertPhoto(Convert.ToInt64(patientID), patientPhoto);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                    }
                    finally
                    {
                        if (oPatientPhoto != null)
                        {
                            oPatientPhoto.Dispose();
                            oPatientPhoto = null;
                        }
                    }
                }

                gloDatabaseLayer.DBParameters odbParams;


                //For Referrals
                //Case GLO2011-0011794 :Still running delete statements when updating patient records
                //Removed the Delete Insert Logic on saving.                
                               
                //string strSqlStatement = "";
                //string nTempId = "";
              
                //if (oPatient.PatientReferrals.Count > 0)
                //{
                //    for (int i = 0; i < oPatient.PatientReferrals.Count; i++)
                //    {
                //        if (nTempId != "")
                //        {
                //            nTempId = nTempId + ',' + Convert.ToString(oPatient.PatientReferrals[i].ContactId);
                //        }
                //        else
                //        {
                //            nTempId = Convert.ToString(oPatient.PatientReferrals[i].ContactId);
                //        }
                //    }
                //    DataTable dtRowCount = null;
                //    strSqlStatement = "Select Count(nPatientDetailID) as count from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactId  not in ( " + nTempId + " ) and nContactFlag = " + PatientContactType.Referral.GetHashCode();
                //    oDB.Retrive_Query(strSqlStatement, out dtRowCount);
                //    if (dtRowCount != null)
                //    {
                //        if (Convert.ToInt16(dtRowCount.Rows[0]["Count"]) > 0)
                //        {
                //         //   strSqlStatement = "Delete from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactId  not in ( " + nTempId + " ) and nContactFlag = " + PatientContactType.Referral.GetHashCode();
                //          //  oDB.Execute_Query(strSqlStatement);
                //        }
                //        dtRowCount.Dispose();
                //        dtRowCount = null;
                //    }
                //}
                //else
                //{
                //    strSqlStatement = "Delete from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactFlag = " + PatientContactType.Referral.GetHashCode();
                //    oDB.Execute_Query(strSqlStatement);
                //}
               

                //---Add referrals
                //for (int i = 0; i < oPatient.PatientReferrals.Count; i++)
                //{
                //    odbParams = new gloDatabaseLayer.DBParameters();

                //    odbParams.Add("@nPatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nPatientDetailID", oPatient.PatientReferrals[i].PatientDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nContactId", oPatient.PatientReferrals[i].ContactId, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@sName", oPatient.PatientReferrals[i].Name, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sContact", oPatient.PatientReferrals[i].Contact, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine1", oPatient.PatientReferrals[i].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine2", oPatient.PatientReferrals[i].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCity", oPatient.PatientReferrals[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sState", oPatient.PatientReferrals[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sZIP", oPatient.PatientReferrals[i].ZIP, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCounty", oPatient.PatientReferrals[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCountry", oPatient.PatientReferrals[i].Country, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPhone", oPatient.PatientReferrals[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFax", oPatient.PatientReferrals[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sEmail", oPatient.PatientReferrals[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sURL", oPatient.PatientReferrals[i].URL, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMobile", oPatient.PatientReferrals[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPager", oPatient.PatientReferrals[i].Pager, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNotes", oPatient.PatientReferrals[i].Notes, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFirstName", oPatient.PatientReferrals[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMiddleName", oPatient.PatientReferrals[i].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sLastName", oPatient.PatientReferrals[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sGender", oPatient.PatientReferrals[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxonomy", oPatient.PatientReferrals[i].Taxonomy, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxonomyDesc", oPatient.PatientReferrals[i].TaxonomyDesc, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxID", oPatient.PatientReferrals[i].TaxID, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sUPIN", oPatient.PatientReferrals[i].UPIN, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNPI", oPatient.PatientReferrals[i].NPI, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sHospitalAffiliation", oPatient.PatientReferrals[i].HospitalAffiliation, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sExternalCode", oPatient.PatientReferrals[i].ExternalCode, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sDegree", oPatient.PatientReferrals[i].Degree, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@nContactFlag", PatientContactType.Referral.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                //    odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@sPrefix", oPatient.PatientReferrals[i].Prefix, ParameterDirection.Input, SqlDbType.VarChar); //Case 4426 ''Sandip Darade
                //    if (oPatient.PatientReferrals[i].ActiveStartTime == null)
                //        odbParams.Add("@ActiveStartTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@ActiveStartTime", oPatient.PatientReferrals[i].ActiveStartTime, ParameterDirection.Input, SqlDbType.DateTime);

                //    if (oPatient.PatientReferrals[i].ActiveEndTime == null)
                //        odbParams.Add("@ActiveEndTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@ActiveEndTime", oPatient.PatientReferrals[i].ActiveEndTime, ParameterDirection.Input, SqlDbType.DateTime);

                //    if (oPatient.PatientReferrals[i].MUTransactionDate == null)
                //        odbParams.Add("@MuTransactionDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@MuTransactionDate", oPatient.PatientReferrals[i].MUTransactionDate, ParameterDirection.Input, SqlDbType.DateTime);

                //    odbParams.Add("@MuCheckBox", oPatient.PatientReferrals[i].MUCheckBox, ParameterDirection.Input, SqlDbType.Bit);

                //    odbParams.Add("@sServiceLevel", oPatient.PatientReferrals[i].ServiceLevel, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPharmacyStatus", oPatient.PatientReferrals[i].PharmacyStatus, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNCPDPID", oPatient.PatientReferrals[i].NCPDPID, ParameterDirection.Input, SqlDbType.VarChar);
                //    //odbParams.Add("@nContactStatus", oPatient.PatientPharmacies[i].ContactStatus, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nContactStatus", oPatient.PatientReferrals[i].ContactStatus, ParameterDirection.Input, SqlDbType.BigInt);

                //    oDB.Execute("PA_Fill_Referrals_MU", odbParams);
                //    odbParams.Dispose();
                //    odbParams = null;
                //}

                //Added on 28/07/2016 by Juily : Start
                //This all code in comment becasue Add N Remove referrals logic added in SP through TVP
                //Add or Remove referrals through TVP
                DataTable DtPatientDTL = new DataTable();
                SqlConnection con = new SqlConnection(_databaseconnectionstring);
                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();
                SqlParameter param1 = new SqlParameter();
                SqlParameter param2 = new SqlParameter();
                DataTable DTInsurance = new DataTable();
                DataTable DtGuarantor = new DataTable();
                                
                DtPatientDTL.Columns.Add("nPatientID", typeof(long  ));
                DtPatientDTL.Columns.Add("nContactId", typeof(long ));
                DtPatientDTL.Columns.Add("nPatientDetailID", typeof(long ));
                DtPatientDTL.Columns.Add("sName", typeof(string));
                DtPatientDTL.Columns.Add("sContact", typeof(string));
                DtPatientDTL.Columns.Add("sAddressLine1", typeof(string));
                DtPatientDTL.Columns.Add("sAddressLine2", typeof(string));
                DtPatientDTL.Columns.Add("sCity", typeof(string));
                DtPatientDTL.Columns.Add("sState", typeof(string));
                DtPatientDTL.Columns.Add("sZIP", typeof(string));
                DtPatientDTL.Columns.Add("sPhone", typeof(string));
                DtPatientDTL.Columns.Add("sFax", typeof(string));
                DtPatientDTL.Columns.Add("sEmail", typeof(string));
                DtPatientDTL.Columns.Add("sURL", typeof(string));
                DtPatientDTL.Columns.Add("sMobile", typeof(string));
                DtPatientDTL.Columns.Add("sPager", typeof(string));
                DtPatientDTL.Columns.Add("sNotes", typeof(string));
                DtPatientDTL.Columns.Add("sFirstName", typeof(string));
                DtPatientDTL.Columns.Add("sMiddleName", typeof(string));
                DtPatientDTL.Columns.Add("sLastName", typeof(string));
                DtPatientDTL.Columns.Add("sGender", typeof(string));
                DtPatientDTL.Columns.Add("sTaxonomy", typeof(string));
                DtPatientDTL.Columns.Add("sTaxonomyDesc", typeof(string));
                DtPatientDTL.Columns.Add("sTaxID", typeof(string));
                DtPatientDTL.Columns.Add("sUPIN", typeof(string));
                DtPatientDTL.Columns.Add("sNPI", typeof(string));
                DtPatientDTL.Columns.Add("sHospitalAffiliation", typeof(string));
                DtPatientDTL.Columns.Add("sExternalCode", typeof(string));
                DtPatientDTL.Columns.Add("sDegree", typeof(string));
                DtPatientDTL.Columns.Add("nContactFlag", typeof(int));
                DtPatientDTL.Columns.Add("nClinicID", typeof(long));
                DtPatientDTL.Columns.Add("ActiveStartTime", typeof(DateTime));
                DtPatientDTL.Columns.Add("ActiveEndTime", typeof(DateTime));
                DtPatientDTL.Columns.Add("sServiceLevel", typeof(string));
                DtPatientDTL.Columns.Add("sPharmacyStatus", typeof(string));
                DtPatientDTL.Columns.Add("sNCPDPID", typeof(string));
                DtPatientDTL.Columns.Add("sCounty", typeof(string));
                DtPatientDTL.Columns.Add("sCountry", typeof(string));
                DtPatientDTL.Columns.Add("sPrefix", typeof(string));
                DtPatientDTL.Columns.Add("nContactStatus", typeof(Int64 ));
                DtPatientDTL.Columns.Add("dtMuTransactionDate", typeof(DateTime ));
                DtPatientDTL.Columns.Add("bMuCheckBox", typeof(Boolean ));                            

                DtPatientDTL.Rows.Clear();
                for (int i = 0; i < oPatient.PatientReferrals.Count; i++)
                {                  
                   DtPatientDTL.Rows.Add(new object[] { patientID, oPatient.PatientReferrals[i].ContactId, oPatient.PatientReferrals[i].PatientDetailID, oPatient.PatientReferrals[i].Name, oPatient.PatientReferrals[i].Contact
                    ,oPatient.PatientReferrals[i].AddressLine1 ,  oPatient.PatientReferrals[i].AddressLine2 ,oPatient.PatientReferrals[i].City,oPatient.PatientReferrals[i].State,
                    oPatient.PatientReferrals[i].ZIP, oPatient.PatientReferrals[i].Phone, oPatient.PatientReferrals[i].Fax , oPatient.PatientReferrals[i].Email  ,
                    oPatient.PatientReferrals[i].URL ,   oPatient.PatientReferrals[i].Mobile ,   oPatient.PatientReferrals[i].Pager ,   oPatient.PatientReferrals[i].Notes,
                    oPatient.PatientReferrals[i].FirstName , oPatient.PatientReferrals[i].MiddleName , oPatient.PatientReferrals[i].LastName , oPatient.PatientReferrals[i].Gender , oPatient.PatientReferrals[i].Taxonomy ,
                    oPatient.PatientReferrals[i].TaxonomyDesc , oPatient.PatientReferrals[i].TaxID, oPatient.PatientReferrals[i].UPIN , oPatient.PatientReferrals[i].NPI , oPatient.PatientReferrals[i].HospitalAffiliation ,
                    oPatient.PatientReferrals[i].ExternalCode , oPatient.PatientReferrals[i].Degree, PatientContactType.Referral.GetHashCode(), _ClinicID , oPatient.PatientReferrals[i].ActiveStartTime , oPatient.PatientReferrals[i].ActiveEndTime ,
                    oPatient.PatientReferrals[i].ServiceLevel , oPatient.PatientReferrals[i].PharmacyStatus , oPatient.PatientReferrals[i].NCPDPID , 
                    oPatient.PatientReferrals[i].County  , oPatient.PatientReferrals[i].Country , oPatient.PatientReferrals[i].Prefix , oPatient.PatientReferrals[i].ContactStatus ,
                    oPatient.PatientReferrals[i].MUTransactionDate  , oPatient.PatientReferrals[i].MUCheckBox });
                  
                }                
                //------------------------------------------------------------------End

                //----Delete removed Care Team Members
                //strSqlStatement = "";
                //nTempId = "";
                
                //if (oPatient.PatientCareTeam.Count > 0)
                //{
                                       
                //    for (int i = 0; i < oPatient.PatientCareTeam.Count; i++)
                //    {
                //        if (nTempId != "")
                //        {
                //            nTempId = nTempId + ',' + Convert.ToString(oPatient.PatientCareTeam[i].ContactId);
                //        }
                //        else
                //        {
                //            nTempId = Convert.ToString(oPatient.PatientCareTeam[i].ContactId);
                //        }
                //    }
                //    DataTable dtRowCount = null;
                //    strSqlStatement = "Select Count(nPatientDetailID) as count from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactId  not in ( " + nTempId + " ) and nContactFlag = " + PatientContactType.CareTeam.GetHashCode();
                //    oDB.Retrive_Query(strSqlStatement, out dtRowCount);
                //    if (dtRowCount != null)
                //    {
                //        if (Convert.ToInt16(dtRowCount.Rows[0]["Count"]) > 0)
                //        {
                //            strSqlStatement = "Delete from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactId  not in ( " + nTempId + " ) and nContactFlag = " + PatientContactType.CareTeam.GetHashCode();
                //            oDB.Execute_Query(strSqlStatement);
                //        }
                //        dtRowCount.Dispose();
                //        dtRowCount = null;
                //    }
                //}
                //else
                //{
                //    strSqlStatement = "Delete from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactFlag = " + PatientContactType.CareTeam.GetHashCode();
                //    oDB.Execute_Query(strSqlStatement);
                //}
                //---Add Care Team
                //for (int i = 0; i < oPatient.PatientCareTeam.Count; i++)
                //{
                //    odbParams = new gloDatabaseLayer.DBParameters();

                //    odbParams.Add("@nPatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nPatientDetailID", oPatient.PatientCareTeam[i].PatientDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nContactId", oPatient.PatientCareTeam[i].ContactId, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@sName", oPatient.PatientCareTeam[i].Name, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sContact", oPatient.PatientCareTeam[i].Contact, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine1", oPatient.PatientCareTeam[i].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine2", oPatient.PatientCareTeam[i].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCity", oPatient.PatientCareTeam[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sState", oPatient.PatientCareTeam[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sZIP", oPatient.PatientCareTeam[i].ZIP, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCounty", oPatient.PatientCareTeam[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCountry", oPatient.PatientCareTeam[i].Country, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPhone", oPatient.PatientCareTeam[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFax", oPatient.PatientCareTeam[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sEmail", oPatient.PatientCareTeam[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sURL", oPatient.PatientCareTeam[i].URL, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMobile", oPatient.PatientCareTeam[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPager", oPatient.PatientCareTeam[i].Pager, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNotes", oPatient.PatientCareTeam[i].Notes, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFirstName", oPatient.PatientCareTeam[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMiddleName", oPatient.PatientCareTeam[i].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sLastName", oPatient.PatientCareTeam[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sGender", oPatient.PatientCareTeam[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxonomy", oPatient.PatientCareTeam[i].Taxonomy, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxonomyDesc", oPatient.PatientCareTeam[i].TaxonomyDesc, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxID", oPatient.PatientCareTeam[i].TaxID, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sUPIN", oPatient.PatientCareTeam[i].UPIN, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNPI", oPatient.PatientCareTeam[i].NPI, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sHospitalAffiliation", oPatient.PatientCareTeam[i].HospitalAffiliation, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sExternalCode", oPatient.PatientCareTeam[i].ExternalCode, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sDegree", oPatient.PatientCareTeam[i].Degree, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@nContactFlag", PatientContactType.CareTeam.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                //    odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@sPrefix", oPatient.PatientCareTeam[i].Prefix, ParameterDirection.Input, SqlDbType.VarChar); 
                //    if (oPatient.PatientCareTeam[i].ActiveStartTime == null)
                //        odbParams.Add("@ActiveStartTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@ActiveStartTime", oPatient.PatientCareTeam[i].ActiveStartTime, ParameterDirection.Input, SqlDbType.DateTime);

                //    if (oPatient.PatientCareTeam[i].ActiveEndTime == null)
                //        odbParams.Add("@ActiveEndTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@ActiveEndTime", oPatient.PatientCareTeam[i].ActiveEndTime, ParameterDirection.Input, SqlDbType.DateTime);

                //    odbParams.Add("@sServiceLevel", oPatient.PatientCareTeam[i].ServiceLevel, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPharmacyStatus", oPatient.PatientCareTeam[i].PharmacyStatus, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNCPDPID", oPatient.PatientCareTeam[i].NCPDPID, ParameterDirection.Input, SqlDbType.VarChar);
                //    //odbParams.Add("@nContactStatus", oPatient.PatientPharmacies[i].ContactStatus, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nContactStatus", oPatient.PatientCareTeam[i].ContactStatus, ParameterDirection.Input, SqlDbType.BigInt);

                //    oDB.Execute("PA_Fill_Referrals", odbParams);
                //    odbParams.Dispose();
                //    odbParams = null;

                //}

                //Added on 28/07/2016 by Juily : Start
                //This all code in comment becasue Add N Remove Care Team logic added in SP through TVP
                //---Add Care Team through TVP
               for (int i = 0; i < oPatient.PatientCareTeam.Count; i++)
                {
                    DtPatientDTL.Rows.Add(new object[] { patientID, oPatient.PatientCareTeam [i].ContactId, oPatient.PatientCareTeam[i].PatientDetailID, oPatient.PatientCareTeam[i].Name ,
                    oPatient.PatientCareTeam[i].Contact, oPatient.PatientCareTeam[i].AddressLine1, oPatient.PatientCareTeam[i].AddressLine2, oPatient.PatientCareTeam[i].City, 
                    oPatient.PatientCareTeam[i].State, oPatient.PatientCareTeam[i].ZIP, oPatient.PatientCareTeam[i].Phone ,oPatient.PatientCareTeam[i].Fax ,
                    oPatient.PatientCareTeam[i].Email, oPatient.PatientCareTeam[i].URL , oPatient.PatientCareTeam[i].Mobile , oPatient.PatientCareTeam[i].Pager ,
                    oPatient.PatientCareTeam[i].Notes , oPatient.PatientCareTeam[i].FirstName, oPatient.PatientCareTeam[i].MiddleName, oPatient.PatientCareTeam[i].LastName ,
                    oPatient.PatientCareTeam[i].Gender , oPatient.PatientCareTeam[i].Taxonomy , oPatient.PatientCareTeam[i].TaxonomyDesc , oPatient.PatientCareTeam[i].TaxID ,
                    oPatient.PatientCareTeam[i].UPIN , oPatient.PatientCareTeam[i].NPI , oPatient.PatientCareTeam[i].HospitalAffiliation , oPatient.PatientCareTeam[i].ExternalCode ,
                    oPatient.PatientCareTeam[i].Degree , PatientContactType.CareTeam.GetHashCode() , _ClinicID , oPatient.PatientCareTeam[i].ActiveStartTime ,
                    oPatient.PatientCareTeam[i].ActiveEndTime , oPatient.PatientCareTeam[i].ServiceLevel , oPatient.PatientCareTeam[i].PharmacyStatus , oPatient.PatientCareTeam[i].NCPDPID ,
                    oPatient.PatientCareTeam[i].County , oPatient.PatientCareTeam[i].Country , oPatient.PatientCareTeam[i].Prefix , oPatient.PatientCareTeam[i].ContactStatus ,
                    null , null });
                }
                //---------------------------End
                
                //For Pharmacies
                //Case GLO2011-0011794 :Still running delete statements when updating patient records
                //Removed the Delete Insert Logic on saving.
                //nTempId = "";
                //if (oPatient.PatientPharmacies.Count > 0)
                //{
                //    for (int i = 0; i < oPatient.PatientPharmacies.Count; i++)
                //    {
                //        if (nTempId != "")
                //        {
                //            nTempId = nTempId + ',' + Convert.ToString(oPatient.PatientPharmacies[i].ContactId);
                //        }
                //        else
                //        {
                //            nTempId = Convert.ToString(oPatient.PatientPharmacies[i].ContactId);
                //        }
                //    }
                //    DataTable dtRowCount = null;

                //    strSqlStatement = "Select Count(nPatientDetailID) as count from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactId  not in ( " + nTempId + " ) and nContactFlag = " + PatientContactType.Pharmacy.GetHashCode();
                //    oDB.Retrive_Query(strSqlStatement, out dtRowCount);
                //    if (dtRowCount != null)
                //    {
                //        if (Convert.ToInt16(dtRowCount.Rows[0]["Count"]) > 0)
                //        {
                //            strSqlStatement = "Delete from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactId  not in ( " + nTempId + " ) and nContactFlag = " + PatientContactType.Pharmacy.GetHashCode();
                //            oDB.Execute_Query(strSqlStatement);
                //        }
                //        dtRowCount.Dispose();
                //        dtRowCount = null;
                //    }
                //}
                //else
                //{
                //    strSqlStatement = "Delete from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + "  and nContactFlag = " + PatientContactType.Pharmacy.GetHashCode();
                //    oDB.Execute_Query(strSqlStatement);
                //}


                //---Add Pharmacies
                //for (int i = 0; i < oPatient.PatientPharmacies.Count; i++)
                //{
                //    odbParams = new gloDatabaseLayer.DBParameters();

                //    odbParams.Add("@nPatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nPatientDetailID", oPatient.PatientPharmacies[i].PatientDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nContactId", oPatient.PatientPharmacies[i].ContactId, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@sName", oPatient.PatientPharmacies[i].Name, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sContact", oPatient.PatientPharmacies[i].Contact, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine1", oPatient.PatientPharmacies[i].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine2", oPatient.PatientPharmacies[i].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCity", oPatient.PatientPharmacies[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sState", oPatient.PatientPharmacies[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sZIP", oPatient.PatientPharmacies[i].ZIP, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCounty", oPatient.PatientPharmacies[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCountry", oPatient.PatientPharmacies[i].Country, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPhone", oPatient.PatientPharmacies[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFax", oPatient.PatientPharmacies[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sEmail", oPatient.PatientPharmacies[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sURL", oPatient.PatientPharmacies[i].URL, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMobile", oPatient.PatientPharmacies[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPager", oPatient.PatientPharmacies[i].Pager, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNotes", oPatient.PatientPharmacies[i].Notes, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFirstName", oPatient.PatientPharmacies[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMiddleName", oPatient.PatientPharmacies[i].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sLastName", oPatient.PatientPharmacies[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sGender", oPatient.PatientPharmacies[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxonomy", oPatient.PatientPharmacies[i].Taxonomy, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxonomyDesc", oPatient.PatientPharmacies[i].TaxonomyDesc, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxID", oPatient.PatientPharmacies[i].TaxID, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sUPIN", oPatient.PatientPharmacies[i].UPIN, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNPI", oPatient.PatientPharmacies[i].NPI, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sHospitalAffiliation", oPatient.PatientPharmacies[i].HospitalAffiliation, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sExternalCode", oPatient.PatientPharmacies[i].ExternalCode, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sDegree", oPatient.PatientPharmacies[i].Degree, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@nContactFlag", PatientContactType.Pharmacy.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                //    odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                //    if (oPatient.PatientPharmacies[i].ActiveStartTime == null)
                //        odbParams.Add("@ActiveStartTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@ActiveStartTime", oPatient.PatientPharmacies[i].ActiveStartTime, ParameterDirection.Input, SqlDbType.DateTime);

                //    if (oPatient.PatientPharmacies[i].ActiveEndTime == null)
                //        odbParams.Add("@ActiveEndTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@ActiveEndTime", oPatient.PatientPharmacies[i].ActiveEndTime, ParameterDirection.Input, SqlDbType.DateTime);

                //    odbParams.Add("@sServiceLevel", oPatient.PatientPharmacies[i].ServiceLevel, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPharmacyStatus", oPatient.PatientPharmacies[i].PharmacyStatus, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNCPDPID", oPatient.PatientPharmacies[i].NCPDPID, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@nContactStatus", oPatient.PatientPharmacies[i].ContactStatus, ParameterDirection.Input, SqlDbType.BigInt);


                //    oDB.Execute("PA_Fill_Referrals", odbParams);
                //    odbParams.Dispose();
                //    odbParams = null;

                //}

               //Added on 28/07/2016 by Juily : Start
                //This all code in comment becasue Add N Remove Pharmacies logic added in SP through TVP
                //---Add Pharmacies through TVP               
                for (int i = 0; i < oPatient.PatientPharmacies.Count; i++)
                {
                    DtPatientDTL.Rows.Add(new object[] { patientID, oPatient.PatientPharmacies [i].ContactId, oPatient.PatientPharmacies[i].PatientDetailID, oPatient.PatientPharmacies[i].Name ,
                    oPatient.PatientPharmacies[i].Contact, oPatient.PatientPharmacies[i].AddressLine1, oPatient.PatientPharmacies[i].AddressLine2, oPatient.PatientPharmacies[i].City, 
                    oPatient.PatientPharmacies[i].State, oPatient.PatientPharmacies[i].ZIP, oPatient.PatientPharmacies[i].Phone ,oPatient.PatientPharmacies[i].Fax ,
                    oPatient.PatientPharmacies[i].Email, oPatient.PatientPharmacies[i].URL , oPatient.PatientPharmacies[i].Mobile , oPatient.PatientPharmacies[i].Pager ,
                    oPatient.PatientPharmacies[i].Notes , oPatient.PatientPharmacies[i].FirstName, oPatient.PatientPharmacies[i].MiddleName, oPatient.PatientPharmacies[i].LastName ,
                    oPatient.PatientPharmacies[i].Gender , oPatient.PatientPharmacies[i].Taxonomy , oPatient.PatientPharmacies[i].TaxonomyDesc , oPatient.PatientPharmacies[i].TaxID ,
                    oPatient.PatientPharmacies[i].UPIN , oPatient.PatientPharmacies[i].NPI , oPatient.PatientPharmacies[i].HospitalAffiliation , oPatient.PatientPharmacies[i].ExternalCode ,
                    oPatient.PatientPharmacies[i].Degree , PatientContactType.Pharmacy.GetHashCode() , _ClinicID , oPatient.PatientPharmacies[i].ActiveStartTime ,
                    oPatient.PatientPharmacies[i].ActiveEndTime , oPatient.PatientPharmacies[i].ServiceLevel , oPatient.PatientPharmacies[i].PharmacyStatus , oPatient.PatientPharmacies[i].NCPDPID ,
                    oPatient.PatientPharmacies[i].County , oPatient.PatientPharmacies[i].Country , oPatient.PatientPharmacies[i].Prefix , oPatient.PatientPharmacies[i].ContactStatus ,
                    null , null });
                }                
                //-------------------------------End

                //For PrimaryCarePhysicians
                //Case GLO2011-0011794 :Still running delete statements when updating patient records
                //Removed the Delete Insert Logic on saving.
                //nTempId = "";
                //if (oPatient.PrimaryCarePhysicians.Count > 0)
                //{
                //    for (int i = 0; i < oPatient.PrimaryCarePhysicians.Count; i++)
                //    {
                //        if (nTempId != "")
                //        {
                //            nTempId = nTempId + ',' + Convert.ToString(oPatient.PrimaryCarePhysicians[i].ContactId);
                //        }
                //        else
                //        {
                //            nTempId = Convert.ToString(oPatient.PrimaryCarePhysicians[i].ContactId);
                //        }
                //    }
                //    DataTable dtRowCount = null;
                //    strSqlStatement = "Select Count(nPatientDetailID) as count from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactId  not in ( " + nTempId + " ) and nContactFlag = " + PatientContactType.PrimaryCarePhysician.GetHashCode();
                //    oDB.Retrive_Query(strSqlStatement, out dtRowCount);
                //    if (dtRowCount != null)
                //    {
                //        if (Convert.ToInt16(dtRowCount.Rows[0]["Count"]) > 0)
                //        {
                //            strSqlStatement = "Delete from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactId  not in ( " + nTempId + " ) and nContactFlag = " + PatientContactType.PrimaryCarePhysician.GetHashCode();
                //            oDB.Execute_Query(strSqlStatement);
                //        }
                //        dtRowCount.Dispose();
                //        dtRowCount = null;
                //    }
                //}
                //else
                //{
                //    strSqlStatement = "Delete from Patient_DTL where nPatientID = '" + patientID + "' AND nClinicID = " + _ClinicID + " and nContactFlag = " + PatientContactType.PrimaryCarePhysician.GetHashCode();
                //    oDB.Execute_Query(strSqlStatement);
                //}

                //--Add Primary care Physician
                //for (int i = 0; i < oPatient.PrimaryCarePhysicians.Count; i++)
                //{
                //    odbParams = new gloDatabaseLayer.DBParameters();

                //    odbParams.Add("@nPatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nPatientDetailID", oPatient.PrimaryCarePhysicians[i].PatientDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nContactId", oPatient.PrimaryCarePhysicians[i].ContactId, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@sName", oPatient.PrimaryCarePhysicians[i].Name, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sContact", oPatient.PrimaryCarePhysicians[i].Contact, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine1", oPatient.PrimaryCarePhysicians[i].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine2", oPatient.PrimaryCarePhysicians[i].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCity", oPatient.PrimaryCarePhysicians[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sState", oPatient.PrimaryCarePhysicians[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sZIP", oPatient.PrimaryCarePhysicians[i].ZIP, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCounty", oPatient.PrimaryCarePhysicians[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCountry", oPatient.PrimaryCarePhysicians[i].Country, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPhone", oPatient.PrimaryCarePhysicians[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFax", oPatient.PrimaryCarePhysicians[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sEmail", oPatient.PrimaryCarePhysicians[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sURL", oPatient.PrimaryCarePhysicians[i].URL, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMobile", oPatient.PrimaryCarePhysicians[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPager", oPatient.PrimaryCarePhysicians[i].Pager, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNotes", oPatient.PrimaryCarePhysicians[i].Notes, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFirstName", oPatient.PrimaryCarePhysicians[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMiddleName", oPatient.PrimaryCarePhysicians[i].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sLastName", oPatient.PrimaryCarePhysicians[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sGender", oPatient.PrimaryCarePhysicians[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxonomy", oPatient.PrimaryCarePhysicians[i].Taxonomy, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxonomyDesc", oPatient.PrimaryCarePhysicians[i].TaxonomyDesc, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sTaxID", oPatient.PrimaryCarePhysicians[i].TaxID, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sUPIN", oPatient.PrimaryCarePhysicians[i].UPIN, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNPI", oPatient.PrimaryCarePhysicians[i].NPI, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sHospitalAffiliation", oPatient.PrimaryCarePhysicians[i].HospitalAffiliation, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sExternalCode", oPatient.PrimaryCarePhysicians[i].ExternalCode, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sDegree", oPatient.PrimaryCarePhysicians[i].Degree, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@nContactFlag", PatientContactType.PrimaryCarePhysician.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                //    odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@sPrefix", oPatient.PrimaryCarePhysicians[i].Prefix, ParameterDirection.Input, SqlDbType.VarChar); //Case 4426 ''Sandip Darade

                //    if (oPatient.PrimaryCarePhysicians[i].ActiveStartTime == null)
                //        odbParams.Add("@ActiveStartTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@ActiveStartTime", oPatient.PrimaryCarePhysicians[i].ActiveStartTime, ParameterDirection.Input, SqlDbType.DateTime);

                //    if (oPatient.PrimaryCarePhysicians[i].ActiveEndTime == null)
                //        odbParams.Add("@ActiveEndTime", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    else
                //        odbParams.Add("@ActiveEndTime", oPatient.PrimaryCarePhysicians[i].ActiveEndTime, ParameterDirection.Input, SqlDbType.DateTime);

                //    //if (oPatient.PrimaryCarePhysicians[i].MUTransactionDate == null)
                //    //    odbParams.Add("@MuTransactionDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //    //else
                //    //    odbParams.Add("@MuTransactionDate", oPatient.PrimaryCarePhysicians[i].MUTransactionDate, ParameterDirection.Input, SqlDbType.DateTime);

                //    //odbParams.Add("@MuCheckBox", oPatient.PrimaryCarePhysicians[i].MUCheckBox, ParameterDirection.Input, SqlDbType.Bit);

                //    odbParams.Add("@sServiceLevel", oPatient.PrimaryCarePhysicians[i].ServiceLevel, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPharmacyStatus", oPatient.PrimaryCarePhysicians[i].PharmacyStatus, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sNCPDPID", oPatient.PrimaryCarePhysicians[i].NCPDPID, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@nContactStatus", oPatient.PrimaryCarePhysicians[i].ContactStatus, ParameterDirection.Input, SqlDbType.BigInt);

                //    oDB.Execute("PA_Fill_Referrals", odbParams);
                //    odbParams.Dispose();
                //    odbParams = null;

                //}

                //Added on 28/07/2016 by Juily : Start
                //This all code in comment becasue Add N Remove Primary care Physician logic added in SP through TVP
                //--Add Primary care Physician through TVP               
                for (int i = 0; i < oPatient.PrimaryCarePhysicians.Count; i++)
                {
                    DtPatientDTL.Rows.Add(new object[] { patientID, oPatient.PrimaryCarePhysicians[i].ContactId, oPatient.PrimaryCarePhysicians[i].PatientDetailID, oPatient.PrimaryCarePhysicians[i].Name ,
                    oPatient.PrimaryCarePhysicians[i].Contact, oPatient.PrimaryCarePhysicians[i].AddressLine1, oPatient.PrimaryCarePhysicians[i].AddressLine2, oPatient.PrimaryCarePhysicians[i].City, 
                    oPatient.PrimaryCarePhysicians[i].State, oPatient.PrimaryCarePhysicians[i].ZIP, oPatient.PrimaryCarePhysicians[i].Phone ,oPatient.PrimaryCarePhysicians[i].Fax ,
                    oPatient.PrimaryCarePhysicians[i].Email, oPatient.PrimaryCarePhysicians[i].URL , oPatient.PrimaryCarePhysicians[i].Mobile , oPatient.PrimaryCarePhysicians[i].Pager ,
                    oPatient.PrimaryCarePhysicians[i].Notes , oPatient.PrimaryCarePhysicians[i].FirstName, oPatient.PrimaryCarePhysicians[i].MiddleName, oPatient.PrimaryCarePhysicians[i].LastName ,
                    oPatient.PrimaryCarePhysicians[i].Gender , oPatient.PrimaryCarePhysicians[i].Taxonomy , oPatient.PrimaryCarePhysicians[i].TaxonomyDesc , oPatient.PrimaryCarePhysicians[i].TaxID ,
                    oPatient.PrimaryCarePhysicians[i].UPIN , oPatient.PrimaryCarePhysicians[i].NPI , oPatient.PrimaryCarePhysicians[i].HospitalAffiliation , oPatient.PrimaryCarePhysicians[i].ExternalCode ,
                    oPatient.PrimaryCarePhysicians[i].Degree , PatientContactType.PrimaryCarePhysician.GetHashCode() , _ClinicID , oPatient.PrimaryCarePhysicians[i].ActiveStartTime ,
                    oPatient.PrimaryCarePhysicians[i].ActiveEndTime , oPatient.PrimaryCarePhysicians[i].ServiceLevel , oPatient.PrimaryCarePhysicians[i].PharmacyStatus , oPatient.PrimaryCarePhysicians[i].NCPDPID ,
                    oPatient.PrimaryCarePhysicians[i].County , oPatient.PrimaryCarePhysicians[i].Country , oPatient.PrimaryCarePhysicians[i].Prefix , oPatient.PrimaryCarePhysicians[i].ContactStatus ,
                    null , null });
                }
                //-------------------------End

                //For Insurances
                //Delete if any already referrals 
                //code comment start by nilesh on 20110110 for case#GLO2010-0007999
                //oDB.Execute_Query("Delete from PatientInsurance_DTL where nPatientID='" + patientID + "'");
                //code comment end by nilesh on 20110110 for case#GLO2010-0007999

                //code start by pankaj for case#GLO2010-0007999
                //delete the insurances from patientinsurance_dtl table
                //For Resolving BUG ID : 8277
                //if (oPatient.DeletedInsurances != null)
                //{
                //    if (oPatient.DeletedInsurances.Count > 0)
                //    {
                //        foreach (Int64 insuranceID in oPatient.DeletedInsurances)
                //        {
                //            oDB.Execute_Query("Delete from PatientInsurance_DTL where nInsuranceID='" + insuranceID + "' And nPatientID=" + patientID);
                //        }
                //        oPatient.DeletedInsurances.Clear();
                //    }
                //}
                //code end by pankaj for case#GLO2010-0007999

                // If condition added to avoid the unnecessary updates (ref #GLO2010-0007999)
                //if (oPatient.IsInsuranceModified)
                //{
                //    for (int i = 0; i <= oPatient.InsuranceDetails.InsurancesDetails.Count - 1; i++)
                //    {
                //        odbParams = new gloDatabaseLayer.DBParameters();
                //        //nPatientID, nInsuranceID, sSubscriberName, sSubscriberPolicy#, sSubscriberID, sGroup, sEmployer,
                //        //sPhone, dtDOB, bPrimaryFlag
                //        odbParams.Add("@PatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);

                //        // Added for Resolving Bug #8034 ( Modified): Patient Registration >> Print >> Patient NameChange not getting Updated on Insurance details when click on Yes ont he Patient Record Save message when printing
                //        // After resolving above issue, found that insurances were getting added multiple times when user prints multiple times w/o save. Resolved the issue. 
                //        // Done SP changes, made Insurance ID as InputOutput parameter & updated the Insurance ID into the collection.
                //        odbParams.Add("@InsuranceID", oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceID, ParameterDirection.InputOutput, SqlDbType.BigInt);

                //        // odbParams.Add("@SubscriberName", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberName, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@SubscriberPolicy#", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberPolicy, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@SubscriberID", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberID, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@Group", oPatient.InsuranceDetails.InsurancesDetails[i].Group, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@Employer", oPatient.InsuranceDetails.InsurancesDetails[i].Employer, ParameterDirection.Input, SqlDbType.VarChar);

                //        odbParams.Add("@InsuranceFlag", oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceFlag, ParameterDirection.Input, SqlDbType.Int);
                //        odbParams.Add("@IsNotDOB", oPatient.InsuranceDetails.InsurancesDetails[i].IsNotDOB, ParameterDirection.Input, SqlDbType.Bit);
                //        //odbParams.Add("@InsuranceID", oPatient.Referrals[i].ReferralID, ParameterDirection.Input, SqlDbType.BigInt);

                //        if (oPatient.InsuranceDetails.InsurancesDetails[i].IsSameAsPatient)
                //        {
                //            //@SubFName Varchar(50),
                //            odbParams.Add("@SubFName", oPatient.DemographicsDetail.PatientFirstName, ParameterDirection.Input, SqlDbType.VarChar);
                //            //@SubMName Varchar(50),
                //            odbParams.Add("@SubMName", oPatient.DemographicsDetail.PatientMiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                //            //@SubLName Varchar(50), 
                //            odbParams.Add("@SubLName", oPatient.DemographicsDetail.PatientLastName, ParameterDirection.Input, SqlDbType.VarChar);
                //            //@RelationShipID numeric(18,0), 

                //            //Fields added on 20081030
                //            odbParams.Add("@sSubscriberAddr1", oPatient.DemographicsDetail.PatientAddress1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //            odbParams.Add("@sSubscriberAddr2", oPatient.DemographicsDetail.PatientAddress2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //            odbParams.Add("@sSubscriberState", oPatient.DemographicsDetail.PatientState, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //            odbParams.Add("@sSubscriberCity", oPatient.DemographicsDetail.PatientCity, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //            odbParams.Add("@sSubscriberZip", oPatient.DemographicsDetail.PatientZip, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //            //
                //            odbParams.Add("@sSubscriberGender", oPatient.DemographicsDetail.PatientGender, ParameterDirection.Input, SqlDbType.VarChar);
                //            odbParams.Add("@RelationShip", oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipName, ParameterDirection.Input, SqlDbType.VarChar);
                //            odbParams.Add("@Phone", oPatient.DemographicsDetail.PatientPhone, ParameterDirection.Input, SqlDbType.VarChar);
                //            odbParams.Add("@DOB", oPatient.DemographicsDetail.PatientDOB, ParameterDirection.Input, SqlDbType.DateTime);
                //            odbParams.Add("@sSubscriberCounty", oPatient.DemographicsDetail.PatientCounty, ParameterDirection.Input, SqlDbType.VarChar);
                //            odbParams.Add("@sSubscriberCountry", oPatient.DemographicsDetail.PatientCountry, ParameterDirection.Input, SqlDbType.VarChar);
                //        }
                //        else
                //        {
                //            //@SubFName Varchar(50),
                //            odbParams.Add("@SubFName", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberFName, ParameterDirection.Input, SqlDbType.VarChar);
                //            //@SubMName Varchar(50),
                //            odbParams.Add("@SubMName", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberMName, ParameterDirection.Input, SqlDbType.VarChar);
                //            //@SubLName Varchar(50), 
                //            odbParams.Add("@SubLName", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberLName, ParameterDirection.Input, SqlDbType.VarChar);
                //            //@RelationShipID numeric(18,0), 
                //            //CODE ADDED BY DIPAK  20100309
                //            //IF ELSE CONDITION ADD FOR IMPLEMENT FUNCTIONALITY ADDRESS SAME AS PATIENT
                //            if (oPatient.InsuranceDetails.InsurancesDetails[i].IsAddressSameAsPatient)
                //            {
                //                //Fields added on 20081030
                //                odbParams.Add("@sSubscriberAddr1", oPatient.DemographicsDetail.PatientAddress1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                odbParams.Add("@sSubscriberAddr2", oPatient.DemographicsDetail.PatientAddress2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                odbParams.Add("@sSubscriberState", oPatient.DemographicsDetail.PatientState, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                odbParams.Add("@sSubscriberCity", oPatient.DemographicsDetail.PatientCity, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                odbParams.Add("@sSubscriberZip", oPatient.DemographicsDetail.PatientZip, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                //
                //            }
                //            else
                //            {
                //                //Fields added on 20081030
                //                odbParams.Add("@sSubscriberAddr1", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberAddr1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                odbParams.Add("@sSubscriberAddr2", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberAddr2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                odbParams.Add("@sSubscriberState", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberState, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                odbParams.Add("@sSubscriberCity", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCity, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //                odbParams.Add("@sSubscriberZip", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberZip, ParameterDirection.Input, SqlDbType.VarChar, 255);
                //            }//
                //            //END CODE ADDED BY DIPAK  20100309

                //            odbParams.Add("@sSubscriberGender", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberGender, ParameterDirection.Input, SqlDbType.VarChar);
                //            odbParams.Add("@RelationShip", oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipName, ParameterDirection.Input, SqlDbType.VarChar);
                //            odbParams.Add("@Phone", oPatient.InsuranceDetails.InsurancesDetails[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                //            odbParams.Add("@DOB", oPatient.InsuranceDetails.InsurancesDetails[i].DOB, ParameterDirection.Input, SqlDbType.DateTime);
                //            odbParams.Add("@sSubscriberCounty", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCounty, ParameterDirection.Input, SqlDbType.VarChar);
                //            odbParams.Add("@sSubscriberCountry", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCountry, ParameterDirection.Input, SqlDbType.VarChar);


                //        }
                //        odbParams.Add("@RelationShipID", oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipID, ParameterDirection.Input, SqlDbType.BigInt);
                //        //@RelationShip Varchar(50),

                //        //@Deductableamount Decimal(18,0), 
                //        odbParams.Add("@Deductableamount", oPatient.InsuranceDetails.InsurancesDetails[i].DeductableAmount, ParameterDirection.Input, SqlDbType.Decimal);
                //        //@CoveragePercent Decimal(18,0), 
                //        odbParams.Add("@CoveragePercent", oPatient.InsuranceDetails.InsurancesDetails[i].CoveragePercent, ParameterDirection.Input, SqlDbType.Decimal);
                //        //@CoPay Decimal(18,0),
                //        odbParams.Add("@CoPay", oPatient.InsuranceDetails.InsurancesDetails[i].CoPay, ParameterDirection.Input, SqlDbType.Decimal);
                //        //@AssignmentofBenifit Bit,
                //        odbParams.Add("@AssignmentofBenifit", oPatient.InsuranceDetails.InsurancesDetails[i].AssignmentofBenefit, ParameterDirection.Input, SqlDbType.Bit);
                //        //@IsNotStartDate   bit,
                //        odbParams.Add("@IsNotStartDate", oPatient.InsuranceDetails.InsurancesDetails[i].IsNotStartDate, ParameterDirection.Input, SqlDbType.Bit);
                //        //@dtStartDate DateTime, 
                //        odbParams.Add("@dtStartDate", oPatient.InsuranceDetails.InsurancesDetails[i].StartDate, ParameterDirection.Input, SqlDbType.DateTime);
                //        //@IsNotEndDate   bit,
                //        odbParams.Add("@IsNotEndDate", oPatient.InsuranceDetails.InsurancesDetails[i].IsNotEndDate, ParameterDirection.Input, SqlDbType.Bit);
                //        //@dtEndDate DateTime
                //        odbParams.Add("@dtEndDate", oPatient.InsuranceDetails.InsurancesDetails[i].EndDate, ParameterDirection.Input, SqlDbType.DateTime);
                //        //@dtEndDate DateTime


                //        //Insurance Details
                //        odbParams.Add("@sInsuranceName", oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceName, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sPayerID", oPatient.InsuranceDetails.InsurancesDetails[i].PayerID, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sAddressLine1", oPatient.InsuranceDetails.InsurancesDetails[i].AddrressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sAddressLine2", oPatient.InsuranceDetails.InsurancesDetails[i].AddrressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sCity", oPatient.InsuranceDetails.InsurancesDetails[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sState", oPatient.InsuranceDetails.InsurancesDetails[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sZIP", oPatient.InsuranceDetails.InsurancesDetails[i].ZIP, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sInsurancePhone", oPatient.InsuranceDetails.InsurancesDetails[i].InsurancePhone, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sFax", oPatient.InsuranceDetails.InsurancesDetails[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sEmail", oPatient.InsuranceDetails.InsurancesDetails[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sURL", oPatient.InsuranceDetails.InsurancesDetails[i].URL, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sInsuranceTypeCode", oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sInsuranceTypeDesc", oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceTypeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@bAccessAssignment", oPatient.InsuranceDetails.InsurancesDetails[i].bAccessAssignment, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bStatementToPatient", oPatient.InsuranceDetails.InsurancesDetails[i].bStatementToPatient, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bMedigap", oPatient.InsuranceDetails.InsurancesDetails[i].bMedigap, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bReferringIDInBox19", oPatient.InsuranceDetails.InsurancesDetails[i].bReferringIDInBox19, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bNameOfacilityinBox33", oPatient.InsuranceDetails.InsurancesDetails[i].bNameOfacilityinBox33, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bDoNotPrintFacility", oPatient.InsuranceDetails.InsurancesDetails[i].bDoNotPrintFacility, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@b1stPointer", oPatient.InsuranceDetails.InsurancesDetails[i].b1stPointer, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bBox31Blank", oPatient.InsuranceDetails.InsurancesDetails[i].bBox31Blank, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bShowPayment", oPatient.InsuranceDetails.InsurancesDetails[i].bShowPayment, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@nTypeOBilling", oPatient.InsuranceDetails.InsurancesDetails[i].nTypeOBilling, ParameterDirection.Input, SqlDbType.Int);
                //        odbParams.Add("@nClearingHouse", oPatient.InsuranceDetails.InsurancesDetails[i].nClearingHouse, ParameterDirection.Input, SqlDbType.BigInt);
                //        odbParams.Add("@bIsClaims", oPatient.InsuranceDetails.InsurancesDetails[i].bIsClaims, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bIsRemittanceAdvice", oPatient.InsuranceDetails.InsurancesDetails[i].bIsRemittanceAdvice, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bIsRealTimeEligibility", oPatient.InsuranceDetails.InsurancesDetails[i].bIsRealTimeEligibility, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bIsElectronicCOB", oPatient.InsuranceDetails.InsurancesDetails[i].bIsElectronicCOB, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bIsRealTimeClaimStatus", oPatient.InsuranceDetails.InsurancesDetails[i].bIsRealTimeClaimStatus, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@bIsEnrollmentRequired", oPatient.InsuranceDetails.InsurancesDetails[i].bIsEnrollmentRequired, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@sPayerPhone", oPatient.InsuranceDetails.InsurancesDetails[i].sPayerPhone, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sWebsite", oPatient.InsuranceDetails.InsurancesDetails[i].sWebsite, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sServicingState", oPatient.InsuranceDetails.InsurancesDetails[i].sServicingState, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sComments", oPatient.InsuranceDetails.InsurancesDetails[i].sComments, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sPayerPhoneExtn", oPatient.InsuranceDetails.InsurancesDetails[i].sPayerPhoneExtn, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@nContactID", oPatient.InsuranceDetails.InsurancesDetails[i].ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                //        odbParams.Add("@bNotesInBox19", oPatient.InsuranceDetails.InsurancesDetails[i].bNotesInBox19, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@sOfficeID", oPatient.InsuranceDetails.InsurancesDetails[i].OfficeID, ParameterDirection.Input, SqlDbType.VarChar);


                //        odbParams.Add("@sCounty", oPatient.InsuranceDetails.InsurancesDetails[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sCountry", oPatient.InsuranceDetails.InsurancesDetails[i].Country, ParameterDirection.Input, SqlDbType.VarChar);

                //        odbParams.Add("@bworkerscomp", oPatient.InsuranceDetails.InsurancesDetails[i].Isworkerscomp, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@bautoclaim", oPatient.InsuranceDetails.InsurancesDetails[i].Isautoclaim, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@bIsSameAsPatient", oPatient.InsuranceDetails.InsurancesDetails[i].IsSameAsPatient, ParameterDirection.Input, SqlDbType.Bit);
                //        //@bIsAddressSameAsPatient PARAMETER ADDED BY DIPAK 20100309 TO REMEMBER DATABASE STATE OF ADDRESS SAME AS PATIENT SETTING
                //        odbParams.Add("@bIsAddressSameAsPatient", oPatient.InsuranceDetails.InsurancesDetails[i].IsAddressSameAsPatient, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@sInsTypeCodeDefault", oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeCodeDefault, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sInsTypeDescriptionDefault", oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeDescriptionDefault, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sInsTypeCodeMedicare", oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeCodeMedicare, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sInsTypeDescriptionMedicare", oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeDescriptionMedicare, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sEligibilityInsuranceNote", oPatient.InsuranceDetails.InsurancesDetails[i].sEligibiltyInsuranceNotes, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@sUserName", _username, ParameterDirection.Input, SqlDbType.VarChar);
                //        odbParams.Add("@bIsCompany", oPatient.InsuranceDetails.InsurancesDetails[i].IsCompnay, ParameterDirection.Input, SqlDbType.Bit);
                //        odbParams.Add("@sSubscriberCompanyName", oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCompanyLName, ParameterDirection.Input, SqlDbType.VarChar);


                //        oDB.Execute("PA_Insert_Insurances", odbParams);

                //        // Added for Resolving Bug #8034 ( Modified): Patient Registration >> Print >> Patient NameChange not getting Updated on Insurance details when click on Yes ont he Patient Record Save message when printing
                //        // After resolving above issue, found that insurances were getting added multiple times when user prints multiple times w/o save. Resolved the issue. 
                //        // Done SP changes, made Insurance ID as InputOutput parameter & updated the Insurance ID into the collection.
                //        if (odbParams["@InsuranceID"].Value != null)
                //        {
                //            oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceID = Convert.ToInt64(odbParams["@InsuranceID"].Value);
                //        }
                //        odbParams.Dispose();
                //        odbParams = null;

                //    }

                //}
                //For Insurances

                //Added on 28/07/2016 by Juily : Start
                //This all code in comment becasue Add Insurances logic added in SP through TVP
                //For Insurances through TVP  
                DTInsurance.Columns.Add("nPatientID", typeof(long));
                DTInsurance.Columns.Add("nInsuranceID", typeof(long));
                DTInsurance.Columns.Add("sSubscriberName", typeof(string));
                DTInsurance.Columns.Add("sSubscriberPolicy#", typeof(string));
                DTInsurance.Columns.Add("sSubscriberID", typeof(string));
                DTInsurance.Columns.Add("sGroup", typeof(string));
                DTInsurance.Columns.Add("sEmployer", typeof(string));
                DTInsurance.Columns.Add("sPhone", typeof(string));
                DTInsurance.Columns.Add("dtDOB", typeof(DateTime));
                DTInsurance.Columns.Add("bPrimaryFlag", typeof(Boolean));
                DTInsurance.Columns.Add("sPayerID", typeof(string));
                DTInsurance.Columns.Add("CopayER", typeof(string));
                DTInsurance.Columns.Add("CopayOV", typeof(string));
                DTInsurance.Columns.Add("CopaySP", typeof(string));
                DTInsurance.Columns.Add("CopayUC", typeof(string));
                DTInsurance.Columns.Add("dtEffectiveDate", typeof(string));
                DTInsurance.Columns.Add("dtExpiryDate", typeof(string));
                DTInsurance.Columns.Add("sSubFName", typeof(string));
                DTInsurance.Columns.Add("sSubMName", typeof(string));
                DTInsurance.Columns.Add("sSubLName", typeof(string));
                DTInsurance.Columns.Add("nRelationShipID", typeof(long));
                DTInsurance.Columns.Add("sRelationShip", typeof(string));
                DTInsurance.Columns.Add("nDeductableamount", typeof(Decimal));
                DTInsurance.Columns.Add("nCoveragePercent", typeof(Decimal));
                DTInsurance.Columns.Add("nCoPay", typeof(Decimal));
                DTInsurance.Columns.Add("bAssignmentofBenifit", typeof(Boolean));
                DTInsurance.Columns.Add("dtStartDate", typeof(DateTime));
                DTInsurance.Columns.Add("dtEndDate", typeof(DateTime));
                DTInsurance.Columns.Add("nInsuranceFlag", typeof(long));
                DTInsurance.Columns.Add("sSubscriberGender", typeof(string));
                DTInsurance.Columns.Add("sSubscriberAddr1", typeof(string));
                DTInsurance.Columns.Add("sSubscriberAddr2", typeof(string));
                DTInsurance.Columns.Add("sSubscriberState", typeof(string));
                DTInsurance.Columns.Add("sSubscriberCity", typeof(string));
                DTInsurance.Columns.Add("sSubscriberZip", typeof(string));
                DTInsurance.Columns.Add("nContactID", typeof(long));
                DTInsurance.Columns.Add("sInsuranceName", typeof(string));
                DTInsurance.Columns.Add("sAddressLine1", typeof(string));
                DTInsurance.Columns.Add("sAddressLine2", typeof(string));
                DTInsurance.Columns.Add("sCity", typeof(string));
                DTInsurance.Columns.Add("sState", typeof(string));
                DTInsurance.Columns.Add("sZIP", typeof(string));
                DTInsurance.Columns.Add("sInsurancePhone", typeof(string));
                DTInsurance.Columns.Add("sFax", typeof(string));
                DTInsurance.Columns.Add("sEmail", typeof(string));
                DTInsurance.Columns.Add("sURL", typeof(string));
                DTInsurance.Columns.Add("sInsuranceTypeCode", typeof(string));
                DTInsurance.Columns.Add("sInsuranceTypeDesc", typeof(string));
                DTInsurance.Columns.Add("bAccessAssignment", typeof(Boolean));
                DTInsurance.Columns.Add("bStatementToPatient", typeof(Boolean));
                DTInsurance.Columns.Add("bMedigap", typeof(Boolean));
                DTInsurance.Columns.Add("bReferringIDInBox19", typeof(Boolean));
                DTInsurance.Columns.Add("bNameOfacilityinBox33", typeof(Boolean));
                DTInsurance.Columns.Add("bDoNotPrintFacility", typeof(Boolean));
                DTInsurance.Columns.Add("b1stPointer", typeof(Boolean));
                DTInsurance.Columns.Add("bBox31Blank", typeof(Boolean));
                DTInsurance.Columns.Add("bShowPayment", typeof(Boolean));
                DTInsurance.Columns.Add("nTypeOBilling", typeof(int));
                DTInsurance.Columns.Add("nClearingHouse", typeof(long));
                DTInsurance.Columns.Add("bIsClaims", typeof(Boolean));
                DTInsurance.Columns.Add("bIsRemittanceAdvice", typeof(Boolean));
                DTInsurance.Columns.Add("bIsRealTimeEligibility", typeof(Boolean));
                DTInsurance.Columns.Add("bIsElectronicCOB", typeof(Boolean));
                DTInsurance.Columns.Add("bIsRealTimeClaimStatus", typeof(Boolean));
                DTInsurance.Columns.Add("bIsEnrollmentRequired", typeof(Boolean));
                DTInsurance.Columns.Add("sPayerPhone", typeof(string));
                DTInsurance.Columns.Add("sWebsite", typeof(string));
                DTInsurance.Columns.Add("sServicingState", typeof(string));
                DTInsurance.Columns.Add("sComments", typeof(string));
                DTInsurance.Columns.Add("sPayerPhoneExtn", typeof(string));
                DTInsurance.Columns.Add("bNotesInBox19", typeof(Boolean));
                DTInsurance.Columns.Add("sOfficeID", typeof(string));
                DTInsurance.Columns.Add("sSubscriberCounty", typeof(string));
                DTInsurance.Columns.Add("sSubscriberCountry", typeof(string));
                DTInsurance.Columns.Add("sCounty", typeof(string));
                DTInsurance.Columns.Add("sCountry", typeof(string));
                DTInsurance.Columns.Add("sExternalCode", typeof(string));
                DTInsurance.Columns.Add("bworkerscomp", typeof(Boolean));
                DTInsurance.Columns.Add("bautoclaim", typeof(Boolean));
                DTInsurance.Columns.Add("bIsSameAsPatient", typeof(Boolean));
                DTInsurance.Columns.Add("bIsAddressSameAsPatient", typeof(Boolean));
                DTInsurance.Columns.Add("sInsTypeCodeDefault", typeof(string));
                DTInsurance.Columns.Add("sInsTypeDescriptionDefault", typeof(string));
                DTInsurance.Columns.Add("sInsTypeCodeMedicare", typeof(string));
                DTInsurance.Columns.Add("sInsTypeDescriptionMedicare", typeof(string));
                DTInsurance.Columns.Add("bUsedForBilling", typeof(Boolean));
                DTInsurance.Columns.Add("sEligibiltyInsuranceNote", typeof(string));
                DTInsurance.Columns.Add("sUserName", typeof(string));
                DTInsurance.Columns.Add("dtReviewedDateTime", typeof(DateTime));
                DTInsurance.Columns.Add("bIsCompnay", typeof(Boolean));
                DTInsurance.Columns.Add("sCompanyName", typeof(string));
                DTInsurance.Columns.Add("IsNotStartDate", typeof(Boolean));
                DTInsurance.Columns.Add("IsNotEndDate", typeof(Boolean));
                DTInsurance.Columns.Add("IsNotDOB", typeof(Boolean));
                              
               // if (oPatient.IsInsuranceModified)
              //  {                    
                    DTInsurance.Rows.Clear();                 
                    for (int i = 0; i <= oPatient.InsuranceDetails.InsurancesDetails.Count - 1; i++)
                    {
                        if (oPatient.InsuranceDetails.InsurancesDetails[i].IsSameAsPatient)
                        {
                            //if (oPatient.InsuranceDetails.InsurancesDetails[i].IsNotStartDate)
                            //{
                            //    oPatient.InsuranceDetails.InsurancesDetails[i].StartDate  =convert.todateDBNull.Value  ;
                            //}
                            DTInsurance.Rows.Add(new object[] { patientID, oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceID, null, oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberPolicy, oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberID ,
                            oPatient.InsuranceDetails.InsurancesDetails[i].Group , oPatient.InsuranceDetails.InsurancesDetails[i].Employer , oPatient.DemographicsDetail.PatientPhone , oPatient.DemographicsDetail.PatientDOB , null 
                            , oPatient.InsuranceDetails.InsurancesDetails[i].PayerID ,
                            null , null , null , null , null , null , oPatient.DemographicsDetail.PatientFirstName , oPatient.DemographicsDetail.PatientMiddleName , oPatient.DemographicsDetail.PatientLastName , oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipID ,
                            oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipName , oPatient.InsuranceDetails.InsurancesDetails[i].DeductableAmount , oPatient.InsuranceDetails.InsurancesDetails[i].CoveragePercent , oPatient.InsuranceDetails.InsurancesDetails[i].CoPay ,
                            oPatient.InsuranceDetails.InsurancesDetails[i].AssignmentofBenefit , oPatient.InsuranceDetails.InsurancesDetails[i].StartDate , oPatient.InsuranceDetails.InsurancesDetails[i].EndDate , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceFlag,
                            oPatient.DemographicsDetail.PatientGender , oPatient.DemographicsDetail.PatientAddress1 , oPatient.DemographicsDetail.PatientAddress2 , oPatient.DemographicsDetail.PatientState , oPatient.DemographicsDetail.PatientCity , oPatient.DemographicsDetail.PatientZip , 
                            oPatient.InsuranceDetails.InsurancesDetails[i].ContactID , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceName ,oPatient.InsuranceDetails.InsurancesDetails[i].AddrressLine1 , oPatient.InsuranceDetails.InsurancesDetails[i].AddrressLine2 , 
                            oPatient.InsuranceDetails.InsurancesDetails[i].City , oPatient.InsuranceDetails.InsurancesDetails[i].State , oPatient.InsuranceDetails.InsurancesDetails[i].ZIP , oPatient.InsuranceDetails.InsurancesDetails[i].InsurancePhone , oPatient.InsuranceDetails.InsurancesDetails[i].Fax , 
                            oPatient.InsuranceDetails.InsurancesDetails[i].Email , oPatient.InsuranceDetails.InsurancesDetails[i].URL , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceTypeCode , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceTypeDesc , 
                            oPatient.InsuranceDetails.InsurancesDetails[i].bAccessAssignment , oPatient.InsuranceDetails.InsurancesDetails[i].bStatementToPatient, oPatient.InsuranceDetails.InsurancesDetails[i].bMedigap, oPatient.InsuranceDetails.InsurancesDetails[i].bReferringIDInBox19, 
                            oPatient.InsuranceDetails.InsurancesDetails[i].bNameOfacilityinBox33 , oPatient.InsuranceDetails.InsurancesDetails[i].bDoNotPrintFacility, oPatient.InsuranceDetails.InsurancesDetails[i].b1stPointer, oPatient.InsuranceDetails.InsurancesDetails[i].bBox31Blank , oPatient.InsuranceDetails.InsurancesDetails[i].bShowPayment , 
                            oPatient.InsuranceDetails.InsurancesDetails[i].nTypeOBilling, oPatient.InsuranceDetails.InsurancesDetails[i].nClearingHouse, oPatient.InsuranceDetails.InsurancesDetails[i].bIsClaims, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRemittanceAdvice, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRealTimeEligibility,
                            oPatient.InsuranceDetails.InsurancesDetails[i].bIsElectronicCOB, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRealTimeClaimStatus , oPatient.InsuranceDetails.InsurancesDetails[i].bIsEnrollmentRequired , oPatient.InsuranceDetails.InsurancesDetails[i].sPayerPhone, oPatient.InsuranceDetails.InsurancesDetails[i].sWebsite,
                            oPatient.InsuranceDetails.InsurancesDetails[i].sServicingState, oPatient.InsuranceDetails.InsurancesDetails[i].sComments, oPatient.InsuranceDetails.InsurancesDetails[i].sPayerPhoneExtn, oPatient.InsuranceDetails.InsurancesDetails[i].bNotesInBox19, oPatient.InsuranceDetails.InsurancesDetails[i].OfficeID, 
                            oPatient.DemographicsDetail.PatientCounty , oPatient.DemographicsDetail.PatientCountry, oPatient.InsuranceDetails.InsurancesDetails[i].County, oPatient.InsuranceDetails.InsurancesDetails[i].Country, null , oPatient.InsuranceDetails.InsurancesDetails[i].Isworkerscomp , oPatient.InsuranceDetails.InsurancesDetails[i].Isautoclaim,
                            oPatient.InsuranceDetails.InsurancesDetails[i].IsSameAsPatient , oPatient.InsuranceDetails.InsurancesDetails[i].IsAddressSameAsPatient, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeCodeDefault, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeDescriptionDefault, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeCodeMedicare, 
                            oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeDescriptionMedicare , null , oPatient.InsuranceDetails.InsurancesDetails[i].sEligibiltyInsuranceNotes , _username, null, oPatient.InsuranceDetails.InsurancesDetails[i].IsCompnay,
                            oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCompanyLName
                            , oPatient.InsuranceDetails.InsurancesDetails[i].IsNotStartDate , oPatient.InsuranceDetails.InsurancesDetails[i].IsNotEndDate, oPatient.InsuranceDetails.InsurancesDetails[i].IsNotDOB 
                            });
                        }
                        else
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[i].IsAddressSameAsPatient)
                            {
                                DTInsurance.Rows.Add(new object[] { patientID, oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceID, null, oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberPolicy, oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberID ,
                                oPatient.InsuranceDetails.InsurancesDetails[i].Group , oPatient.InsuranceDetails.InsurancesDetails[i].Employer , oPatient.InsuranceDetails.InsurancesDetails[i].Phone , oPatient.InsuranceDetails.InsurancesDetails[i].DOB , null , oPatient.InsuranceDetails.InsurancesDetails[i].PayerID ,
                                null , null , null , null , null , null , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberFName ,  oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberMName , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberLName , oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipID ,
                               oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipName , oPatient.InsuranceDetails.InsurancesDetails[i].DeductableAmount , oPatient.InsuranceDetails.InsurancesDetails[i].CoveragePercent , oPatient.InsuranceDetails.InsurancesDetails[i].CoPay ,
                                oPatient.InsuranceDetails.InsurancesDetails[i].AssignmentofBenefit , oPatient.InsuranceDetails.InsurancesDetails[i].StartDate , oPatient.InsuranceDetails.InsurancesDetails[i].EndDate , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceFlag,
                                oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberGender , oPatient.DemographicsDetail.PatientAddress1 , oPatient.DemographicsDetail.PatientAddress2 , oPatient.DemographicsDetail.PatientState , oPatient.DemographicsDetail.PatientCity , oPatient.DemographicsDetail.PatientZip , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].ContactID , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceName ,oPatient.InsuranceDetails.InsurancesDetails[i].AddrressLine1 , oPatient.InsuranceDetails.InsurancesDetails[i].AddrressLine2 , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].City , oPatient.InsuranceDetails.InsurancesDetails[i].State , oPatient.InsuranceDetails.InsurancesDetails[i].ZIP , oPatient.InsuranceDetails.InsurancesDetails[i].InsurancePhone , oPatient.InsuranceDetails.InsurancesDetails[i].Fax , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].Email , oPatient.InsuranceDetails.InsurancesDetails[i].URL , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceTypeCode , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceTypeDesc , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].bAccessAssignment , oPatient.InsuranceDetails.InsurancesDetails[i].bStatementToPatient, oPatient.InsuranceDetails.InsurancesDetails[i].bMedigap, oPatient.InsuranceDetails.InsurancesDetails[i].bReferringIDInBox19, 
                                oPatient.InsuranceDetails.InsurancesDetails[i].bNameOfacilityinBox33 , oPatient.InsuranceDetails.InsurancesDetails[i].bDoNotPrintFacility, oPatient.InsuranceDetails.InsurancesDetails[i].b1stPointer, oPatient.InsuranceDetails.InsurancesDetails[i].bBox31Blank , oPatient.InsuranceDetails.InsurancesDetails[i].bShowPayment , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].nTypeOBilling, oPatient.InsuranceDetails.InsurancesDetails[i].nClearingHouse, oPatient.InsuranceDetails.InsurancesDetails[i].bIsClaims, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRemittanceAdvice, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRealTimeEligibility,
                                oPatient.InsuranceDetails.InsurancesDetails[i].bIsElectronicCOB, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRealTimeClaimStatus , oPatient.InsuranceDetails.InsurancesDetails[i].bIsEnrollmentRequired , oPatient.InsuranceDetails.InsurancesDetails[i].sPayerPhone, oPatient.InsuranceDetails.InsurancesDetails[i].sWebsite,
                                oPatient.InsuranceDetails.InsurancesDetails[i].sServicingState, oPatient.InsuranceDetails.InsurancesDetails[i].sComments, oPatient.InsuranceDetails.InsurancesDetails[i].sPayerPhoneExtn, oPatient.InsuranceDetails.InsurancesDetails[i].bNotesInBox19, oPatient.InsuranceDetails.InsurancesDetails[i].OfficeID, 
                                oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCounty , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCountry, oPatient.InsuranceDetails.InsurancesDetails[i].County, oPatient.InsuranceDetails.InsurancesDetails[i].Country, null , oPatient.InsuranceDetails.InsurancesDetails[i].Isworkerscomp , oPatient.InsuranceDetails.InsurancesDetails[i].Isautoclaim,
                                oPatient.InsuranceDetails.InsurancesDetails[i].IsSameAsPatient , oPatient.InsuranceDetails.InsurancesDetails[i].IsAddressSameAsPatient, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeCodeDefault, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeDescriptionDefault, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeCodeMedicare, 
                                oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeDescriptionMedicare , null , oPatient.InsuranceDetails.InsurancesDetails[i].sEligibiltyInsuranceNotes , _username, null, oPatient.InsuranceDetails.InsurancesDetails[i].IsCompnay,
                                oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCompanyLName 
                                , oPatient.InsuranceDetails.InsurancesDetails[i].IsNotStartDate , oPatient.InsuranceDetails.InsurancesDetails[i].IsNotEndDate, oPatient.InsuranceDetails.InsurancesDetails[i].IsNotDOB });
                            }
                            else
                            {
                                DTInsurance.Rows.Add(new object[] { patientID, oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceID, null, oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberPolicy, oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberID ,
                                oPatient.InsuranceDetails.InsurancesDetails[i].Group , oPatient.InsuranceDetails.InsurancesDetails[i].Employer , oPatient.InsuranceDetails.InsurancesDetails[i].Phone , oPatient.InsuranceDetails.InsurancesDetails[i].DOB , null , oPatient.InsuranceDetails.InsurancesDetails[i].PayerID ,
                                null , null , null , null , null , null , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberFName ,  oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberMName , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberLName , oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipID ,
                                oPatient.InsuranceDetails.InsurancesDetails[i].RelationshipName , oPatient.InsuranceDetails.InsurancesDetails[i].DeductableAmount , oPatient.InsuranceDetails.InsurancesDetails[i].CoveragePercent , oPatient.InsuranceDetails.InsurancesDetails[i].CoPay ,
                                oPatient.InsuranceDetails.InsurancesDetails[i].AssignmentofBenefit , oPatient.InsuranceDetails.InsurancesDetails[i].StartDate , oPatient.InsuranceDetails.InsurancesDetails[i].EndDate , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceFlag,
                                oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberGender , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberAddr1 , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberAddr2 , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberState , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCity , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberZip , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].ContactID , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceName ,oPatient.InsuranceDetails.InsurancesDetails[i].AddrressLine1 , oPatient.InsuranceDetails.InsurancesDetails[i].AddrressLine2 , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].City , oPatient.InsuranceDetails.InsurancesDetails[i].State , oPatient.InsuranceDetails.InsurancesDetails[i].ZIP , oPatient.InsuranceDetails.InsurancesDetails[i].InsurancePhone , oPatient.InsuranceDetails.InsurancesDetails[i].Fax , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].Email , oPatient.InsuranceDetails.InsurancesDetails[i].URL , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceTypeCode , oPatient.InsuranceDetails.InsurancesDetails[i].InsuranceTypeDesc , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].bAccessAssignment , oPatient.InsuranceDetails.InsurancesDetails[i].bStatementToPatient, oPatient.InsuranceDetails.InsurancesDetails[i].bMedigap, oPatient.InsuranceDetails.InsurancesDetails[i].bReferringIDInBox19, 
                                oPatient.InsuranceDetails.InsurancesDetails[i].bNameOfacilityinBox33 , oPatient.InsuranceDetails.InsurancesDetails[i].bDoNotPrintFacility, oPatient.InsuranceDetails.InsurancesDetails[i].b1stPointer, oPatient.InsuranceDetails.InsurancesDetails[i].bBox31Blank , oPatient.InsuranceDetails.InsurancesDetails[i].bShowPayment , 
                                oPatient.InsuranceDetails.InsurancesDetails[i].nTypeOBilling, oPatient.InsuranceDetails.InsurancesDetails[i].nClearingHouse, oPatient.InsuranceDetails.InsurancesDetails[i].bIsClaims, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRemittanceAdvice, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRealTimeEligibility,
                                oPatient.InsuranceDetails.InsurancesDetails[i].bIsElectronicCOB, oPatient.InsuranceDetails.InsurancesDetails[i].bIsRealTimeClaimStatus , oPatient.InsuranceDetails.InsurancesDetails[i].bIsEnrollmentRequired , oPatient.InsuranceDetails.InsurancesDetails[i].sPayerPhone, oPatient.InsuranceDetails.InsurancesDetails[i].sWebsite,
                                oPatient.InsuranceDetails.InsurancesDetails[i].sServicingState, oPatient.InsuranceDetails.InsurancesDetails[i].sComments, oPatient.InsuranceDetails.InsurancesDetails[i].sPayerPhoneExtn, oPatient.InsuranceDetails.InsurancesDetails[i].bNotesInBox19, oPatient.InsuranceDetails.InsurancesDetails[i].OfficeID, 
                                oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCounty , oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCountry, oPatient.InsuranceDetails.InsurancesDetails[i].County, oPatient.InsuranceDetails.InsurancesDetails[i].Country, null , oPatient.InsuranceDetails.InsurancesDetails[i].Isworkerscomp , oPatient.InsuranceDetails.InsurancesDetails[i].Isautoclaim,
                                oPatient.InsuranceDetails.InsurancesDetails[i].IsSameAsPatient , oPatient.InsuranceDetails.InsurancesDetails[i].IsAddressSameAsPatient, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeCodeDefault, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeDescriptionDefault, oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeCodeMedicare, 
                                oPatient.InsuranceDetails.InsurancesDetails[i].InsTypeDescriptionMedicare , null , oPatient.InsuranceDetails.InsurancesDetails[i].sEligibiltyInsuranceNotes , _username, null, oPatient.InsuranceDetails.InsurancesDetails[i].IsCompnay,
                                oPatient.InsuranceDetails.InsurancesDetails[i].SubscriberCompanyLName
                                , oPatient.InsuranceDetails.InsurancesDetails[i].IsNotStartDate , oPatient.InsuranceDetails.InsurancesDetails[i].IsNotEndDate, oPatient.InsuranceDetails.InsurancesDetails[i].IsNotDOB });
                            }
                        }
                    }                   
             //   }
                //--------------------------------------------------------------------------------------------End 

                //For Other Details  Add


                if (oPatient.PatientDemographicOtherInfo != null)
                {
                    try
                    {


                        if (IsTableExists("Patient_OtherDetails"))
                        {
                            //delete existing record if any
                            //For Other Details
                            //Case GLO2011-0011794 :Still running delete statements when updating patient records
                            //Removed the Delete Insert Logic on saving.
                            //oDB.Execute_Query("delete from Patient_OtherDetails where nPatientID = '" + Convert.ToInt64(patientID) + "' ");

                            odbParams = new gloDatabaseLayer.DBParameters();

                            //add Record 
                            odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sSpouseName", oPatient.PatientDemographicOtherInfo.SpouseName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sSpousePhone", oPatient.PatientDemographicOtherInfo.SpousePhone, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nRegistrationDate", gloDateMaster.gloDate.DateAsNumber(oPatient.PatientDemographicOtherInfo.RegistrationDate.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                            odbParams.Add("@sPatientLawyer", oPatient.PatientDemographicOtherInfo.PatientLawyer, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@bSignatureOnFile", oPatient.PatientDemographicOtherInfo.SOF, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@bReminderDeclined", oPatient.PatientDemographicOtherInfo.Reminders, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@nSexualOrientationCategoryID", oPatient.PatientDemographicOtherInfo.SexualOrientationID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sSexualOrientationCode", oPatient.PatientDemographicOtherInfo.SexualOrientationCode, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sSexualOrientationDesc", oPatient.PatientDemographicOtherInfo.SexualOrientationDesc, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sSexualOrientationOtherSpecification", oPatient.PatientDemographicOtherInfo.SexualOrientationOtherSpecification, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nGenderIdentityCateroryID", oPatient.PatientDemographicOtherInfo.GenderIdentityID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sGenderIdentityCode", oPatient.PatientDemographicOtherInfo.GenderIdentityCode, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sGenderIdentityDesc", oPatient.PatientDemographicOtherInfo.GenderIdentityDesc, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sGenderIdentityOtherSpecification", oPatient.PatientDemographicOtherInfo.GenderIdentityOtherSpecification, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sPatientPrevFname", oPatient.PatientDemographicOtherInfo.sPatientPrevFName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sPatientPrevMname", oPatient.PatientDemographicOtherInfo.sPatientPrevMName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sPatientPrevLname", oPatient.PatientDemographicOtherInfo.sPatientPrevLName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sMultipleBirthIndicator", oPatient.PatientDemographicOtherInfo.sMultipleBirthIndicator, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nBirthOrder", Convert.ToInt64(oPatient.PatientDemographicOtherInfo.BirthOrder), ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sBirthSex", oPatient.PatientDemographicOtherInfo.PatientBirthSex, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sImmunizationRegistryStatus", oPatient.PatientDemographicOtherInfo.ImmunizationRegistryStatus, ParameterDirection.Input, SqlDbType.VarChar);

                            oDB.Execute("PA_IN_Patient_OtherDetails", odbParams);
                            odbParams.Dispose();
                            odbParams = null;


                        }

                        if (IsTableExists("Patient_MedicalCategory"))
                        {
                            DataSet dsMedCat = null;
                            DataTable dtMedCat = new DataTable();
                            DataColumn dcPatId = new DataColumn("nPatientID");
                            DataColumn dcMedCatID = new DataColumn("nMedicalCategoryId");
                            dtMedCat.Columns.Add(dcPatId);
                            dtMedCat.Columns.Add(dcMedCatID);
                            foreach (long CatId in oPatient.PatientDemographicOtherInfo.MedicalConditions)
                            {
                                DataRow dtR = dtMedCat.NewRow();
                                dtR["nPatientID"] = Convert.ToInt64(patientID);
                                dtR["nMedicalCategoryId"] = Convert.ToInt64(CatId);
                                dtMedCat.Rows.Add(dtR);
                            }
                            if (dtMedCat.Rows.Count == 0)
                            {
                                DataRow dtR = dtMedCat.NewRow();
                                dtR["nPatientID"] = Convert.ToInt64(patientID);
                                dtR["nMedicalCategoryId"] = 0;
                                dtMedCat.Rows.Add(dtR);
                            }

                            odbParams = new gloDatabaseLayer.DBParameters();
                            odbParams.Add("@tvpMedicalCategory", dtMedCat, ParameterDirection.Input, SqlDbType.Structured);
                            odbParams.Add("@PatientId", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);


                            oDB.Retrive("gsp_GetPatientMedicalCategoryColor", odbParams, out dsMedCat);//changes made for bugid 83503
                            odbParams.Dispose();
                            odbParams = null;

                            Int64 Caseid = 0;
                            if (dsMedCat != null)
                            {
                                if (dsMedCat.Tables.Count > 1)
                                {
                                    if (dsMedCat.Tables[1].Rows.Count > 0)
                                    {
                                        Caseid = Convert.ToInt64(dsMedCat.Tables[1].Rows[0][0]);
                                        if (Caseid > 0)
                                        {
                                            DialogResult dg = MessageBox.Show("There is an active pregnancy case available for this patient. Would you like to close this case?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (dg == DialogResult.Yes)
                                                OpenSetupCases(Caseid);
                                        }
                                    }
                                }

                            }

                            if (dtMedCat != null)
                            {
                                dtMedCat.Dispose();
                                dtMedCat = null;
                            }
                            if (dsMedCat != null)
                            {
                                dsMedCat.Tables.Clear();
                                dsMedCat.Dispose();
                                dsMedCat = null;
                            }
                        }

                        //Save Bad Debt Patient

                        if (IsTableExists("Patient_BadDebt"))
                        {
                            odbParams = new gloDatabaseLayer.DBParameters();

                            odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@dtCreatedDateTime", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);

                            if (oPatient.PatientDemographicOtherInfo.isBadDebtPatient)
                            {
                                odbParams.Add("@flag", 1, ParameterDirection.Input, SqlDbType.Int);
                            }
                            else
                            {
                                odbParams.Add("@flag", 2, ParameterDirection.Input, SqlDbType.Int);
                            }

                            oDB.Execute("PA_IN_Patient_BadDebt", odbParams);
                            odbParams.Dispose();
                            odbParams = null;

                            if (bBadDebtStatus != oPatient.PatientDemographicOtherInfo.isBadDebtPatient)
                            {
                                if (oPatient.PatientDemographicOtherInfo.isBadDebtPatient)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.BadDebtStatus, gloAuditTrail.ActivityType.Checked, "Bad Debt status checked ", Convert.ToInt64(patientID), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                }
                                else
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.BadDebtStatus, gloAuditTrail.ActivityType.Unchecked, "Bad Debt status unchecked", Convert.ToInt64(patientID), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {

                    }


                }

                #region "Workers comp"
                if (oPatient.PatientWorkersComp != null)
                {
                    try
                    {
                        string sqlquery = "delete from Patient_WorkersComp where nPatientID = '" + Convert.ToInt64(patientID) + "' ";
                        //delete existing record if any
                        oDB.Execute_Query(sqlquery);
                        //nPatientId, sClaimno, sContractno, dtValidFrom, dtValidTo, sOtherinfo, nType, nInsuranceID
                        //     odbParams = new gloDatabaseLayer.DBParameters();
                        for (int i = 0; i < oPatient.PatientWorkersComp.Count; i++)
                        {
                            // @nPatientID,@sClaimNo,@sContractno,@dtValidFrom,@dtValidTo,@sOtherinfo,@nType,@nInsuranceID
                            odbParams = new gloDatabaseLayer.DBParameters();
                            odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.InputOutput, SqlDbType.BigInt);
                            odbParams.Add("@sClaimNo", oPatient.PatientWorkersComp[i].Claimno, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sContractno", oPatient.PatientWorkersComp[i].Contractno, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@dtValidFrom", gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oPatient.PatientWorkersComp[i].ValidFrom)), ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@dtValidTo", gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oPatient.PatientWorkersComp[i].ValidTo)), ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sOtherinfo", oPatient.PatientWorkersComp[i].Otherinfo, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nType", Convert.ToInt64(oPatient.PatientWorkersComp[i].Type), ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@nInsuranceID", Convert.ToInt64(oPatient.PatientWorkersComp[i].InsuranceID), ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Execute("PA_IN_WorkerscomInfo", odbParams);
                            odbParams.Dispose();
                            odbParams = null;

                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion


                #region "Patient Account Guarantors and Account Saving"

                odbParams = new gloDatabaseLayer.DBParameters();
                object accountId = 0;
                if (oPatient.Account != null)
                {
                    if (oPatient.IsPatientAccountFeature == false && oPatient.Account.PAccountID != 0)
                    {
                        #region "Account Saving"
                        if (oPatient.Account.IsDataModified == true)
                        {
                            odbParams.Clear();
                            odbParams.Add("@nPAccountID", oPatient.Account.PAccountID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            odbParams.Add("@sAccountNo", oPatient.Account.AccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAccountDesc", oPatient.Account.AccountDesc, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nGuarantorID", oPatient.Account.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sGuarantorCode", oPatient.Account.GuarantorCode, ParameterDirection.Input, SqlDbType.VarChar);
                            if (oPatient.Account.Active == false)
                            {
                                odbParams.Add("@dtAccountClosedDate", DateTime.Today, ParameterDirection.Input, SqlDbType.DateTime);
                            }
                            else
                            {
                                odbParams.Add("@dtAccountClosedDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                            }
                            odbParams.Add("@sFirstName", oPatient.Account.FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sMiddleName", oPatient.Account.MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sLastName", oPatient.Account.LastName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nEntityType", oPatient.Account.EntityType, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sAddressLine1", oPatient.Account.AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAddressLine2", oPatient.Account.AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCity", oPatient.Account.City, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sState", oPatient.Account.State, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sZip", oPatient.Account.Zip, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCountry", oPatient.Account.Country, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCounty", oPatient.Account.County, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAreaCode", oPatient.Account.AreaCode, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@bIsExcludeStatement", oPatient.Account.ExcludeStatement, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@bIsSentToCollection", oPatient.Account.SentToCollection, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@nClinicID", oPatient.Account.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@nSiteID", oPatient.Account.SiteID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sMachineName", oPatient.Account.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nUserID", oPatient.Account.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@dtRecordDate", oPatient.Account.RecordDate, ParameterDirection.Input, SqlDbType.DateTime);
                            odbParams.Add("@bIsActive", oPatient.Account.Active, ParameterDirection.Input, SqlDbType.Bit);

                            odbParams.Add("@nBusinessCenterID", oPatient.Account.nBusinessCenterID, ParameterDirection.Input, SqlDbType.BigInt);


                            oDB.Execute("PA_InUp_Accounts", odbParams, out accountId);

                        }

                        #endregion

                        #region "PatientAccountGuarantors Saving"
                        //AccountGuarantor Saving when  Account Featue off.
                        if (oPatient.IsPatientAccountFeature == false)
                        {
                            //select account guarnator as patient otherguarantor then delete that guarnator  in  patient otherguarantors
                            if (oPatient.PatientOtherGuarantors != null && oPatient.PatientOtherGuarantors.Count > 0)
                            {
                                for (int i = 0; i < oPatient.PatientOtherGuarantors.Count; i++)
                                {
                                    if (oPatient.PatientOtherGuarantors[i].PatientContactID != 0)
                                    {
                                        if (oPatient.PatientOtherGuarantors[i].PatientContactID == oPatient.PatientGuarantors[0].PatientContactID)
                                        {
                                            oPatient.PatientOtherGuarantors.RemoveAt(i);
                                            break;
                                        }

                                    }
                                }
                            }
                            //when account guarantor changed then Insert chnaged guarantor into history table and delete guarantor in table and insert new guarantor 
                            if ((oPatient.nGuarantorId != oPatient.PatientGuarantors[0].PatientContactID) || (oPatient.PatientGuarantors[0].PatientContactID != 0 && oPatient.PatientGuarantors[0].IsDataModified == true))
                            {
                                //Insert deleted guarnator in history table.
                                odbParams.Clear();
                                odbParams.Add("@nPatientContactID", oPatient.nGuarantorId, ParameterDirection.Input, SqlDbType.BigInt);
                                odbParams.Add("@nUserID", Convert.ToInt64(appSettings["UserID"].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                odbParams.Add("@sHstMachineName", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDB.Execute("PA_IN_Patient_OtherContacts_Hst", odbParams);

                                string sqlquery = "delete from Patient_OtherContacts where nPatientID = '" + Convert.ToInt64(patientID) + "' and nPAccountID =" + Convert.ToInt64(oPatient.nPAccountId);
                                oDB.Execute_Query(sqlquery);
                                for (int i = 0; i < oPatient.PatientGuarantors.Count; i++)
                                {
                                    Object GuarantorId;
                                    odbParams.Clear();
                                    //If it is SameAsPatient assign patientID to GuarantorAsPatientID
                                    if (oPatient.PatientGuarantors[i].OtherConatctType.GetHashCode() == PatientOtherContactType.SameAsPatient.GetHashCode())
                                    {
                                        oPatient.PatientGuarantors[i].GuarantorAsPatientID = Convert.ToInt64(patientID);
                                    }

                                    odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactID", oPatient.PatientGuarantors[i].PatientContactID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                    odbParams.Add("@nLineNumber", i, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactType", oPatient.PatientGuarantors[i].OtherConatctType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@sFirstName", oPatient.PatientGuarantors[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sMiddleName", oPatient.PatientGuarantors[i].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sLastName", oPatient.PatientGuarantors[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                                    if (oPatient.PatientGuarantors[i].DOB != null && oPatient.PatientGuarantors[i].DOB != DateTime.MinValue)
                                    {
                                        odbParams.Add("@nDOB", gloDateMaster.gloDate.DateAsNumber(oPatient.PatientGuarantors[i].DOB.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                                    }
                                    else
                                    {
                                        odbParams.Add("@nDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                                    }
                                    odbParams.Add("@sSSN", oPatient.PatientGuarantors[i].SSN, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sGender", oPatient.PatientGuarantors[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sRelation", oPatient.PatientGuarantors[i].Relation, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sAddressLine1", oPatient.PatientGuarantors[i].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sAddressLine2", oPatient.PatientGuarantors[i].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sCity", oPatient.PatientGuarantors[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sState", oPatient.PatientGuarantors[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sZIP", oPatient.PatientGuarantors[i].Zip, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sPhone", oPatient.PatientGuarantors[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sMobile", oPatient.PatientGuarantors[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sFax", oPatient.PatientGuarantors[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sEmail", oPatient.PatientGuarantors[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@bIsActive", oPatient.PatientGuarantors[i].IsActive, ParameterDirection.Input, SqlDbType.Bit);
                                    odbParams.Add("@nVisitID", oPatient.PatientGuarantors[i].VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nAppointmentID", oPatient.PatientGuarantors[i].AppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nGuarantorAsPatientID", oPatient.PatientGuarantors[i].GuarantorAsPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactTypeFlag", oPatient.PatientGuarantors[i].nGuarantorTypeFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@sCounty", oPatient.PatientGuarantors[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sCountry", oPatient.PatientGuarantors[i].Country, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nGuarantorType", oPatient.PatientGuarantors[i].GurantorType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@nPAccountID", oPatient.nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@bIsAccountGuarantor", oPatient.PatientGuarantors[i].IsAccountGuarantor, ParameterDirection.Input, SqlDbType.Bit);
                                    oDB.Execute("PA_INUP_PatientGuarantor", odbParams, out GuarantorId);

                                }
                            }
                        }
                        #endregion "PatientGuarantors Saving"
                    }
                    else
                    {
                        //Indicates that new account.
                        if (oPatient.Account.IsExistingAccount == false)
                        {

                            #region "Account Saving"

                            odbParams.Clear();

                            odbParams.Add("@nPAccountID", oPatient.Account.PAccountID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            odbParams.Add("@sAccountNo", oPatient.Account.AccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAccountDesc", oPatient.Account.AccountDesc, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nGuarantorID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sGuarantorCode", oPatient.Account.GuarantorCode, ParameterDirection.Input, SqlDbType.VarChar);
                            if (oPatient.Account.AccountClosedDate != null && oPatient.Account.AccountClosedDate != DateTime.MinValue)
                            {
                                odbParams.Add("@dtAccountClosedDate", oPatient.Account.AccountClosedDate, ParameterDirection.Input, SqlDbType.DateTime);
                            }
                            else
                            {
                                odbParams.Add("@dtAccountClosedDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                            }
                            odbParams.Add("@sFirstName", oPatient.Account.FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sMiddleName", oPatient.Account.MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sLastName", oPatient.Account.LastName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nEntityType", oPatient.Account.EntityType, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sAddressLine1", oPatient.Account.AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAddressLine2", oPatient.Account.AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCity", oPatient.Account.City, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sState", oPatient.Account.State, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sZip", oPatient.Account.Zip, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCountry", oPatient.Account.Country, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCounty", oPatient.Account.County, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAreaCode", oPatient.Account.AreaCode, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@bIsExcludeStatement", oPatient.Account.ExcludeStatement, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@bIsSentToCollection", oPatient.Account.SentToCollection, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@nClinicID", oPatient.Account.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@nSiteID", oPatient.Account.SiteID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sMachineName", oPatient.Account.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nUserID", oPatient.Account.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@dtRecordDate", oPatient.Account.RecordDate, ParameterDirection.Input, SqlDbType.DateTime);
                            odbParams.Add("@bIsActive", oPatient.Account.Active, ParameterDirection.Input, SqlDbType.Bit);

                            odbParams.Add("@nBusinessCenterID", oPatient.Account.nBusinessCenterID, ParameterDirection.Input, SqlDbType.BigInt);


                            oDB.Execute("PA_InUp_Accounts", odbParams, out accountId);

                            #endregion "Account Saving"

                            #region "PatientAccountGuarantors Saving"

                            //string sqlquery = "delete from Patient_OtherContacts where nPatientID = '" + Convert.ToInt64(patientID) + "' ";
                            //oDB.Execute_Query(sqlquery);

                            if (oPatient.PatientGuarantors != null)
                            {
                                for (int i = 0; i < oPatient.PatientGuarantors.Count; i++)
                                {
                                    Object GuarantorId;
                                    odbParams.Clear();
                                    //If it is SameAsPatient assign patientID to GuarantorAsPatientID
                                    if (oPatient.PatientGuarantors[i].OtherConatctType.GetHashCode() == PatientOtherContactType.SameAsPatient.GetHashCode())
                                    {
                                        oPatient.PatientGuarantors[i].GuarantorAsPatientID = Convert.ToInt64(patientID);
                                    }

                                    odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactID", oPatient.PatientGuarantors[i].PatientContactID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                    odbParams.Add("@nLineNumber", i, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactType", oPatient.PatientGuarantors[i].OtherConatctType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@sFirstName", oPatient.PatientGuarantors[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sMiddleName", oPatient.PatientGuarantors[i].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sLastName", oPatient.PatientGuarantors[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                                    if (oPatient.PatientGuarantors[i].DOB != null && oPatient.PatientGuarantors[i].DOB != DateTime.MinValue)
                                    {
                                        odbParams.Add("@nDOB", gloDateMaster.gloDate.DateAsNumber(oPatient.PatientGuarantors[i].DOB.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                                    }
                                    else
                                    {
                                        odbParams.Add("@nDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                                    }
                                    odbParams.Add("@sSSN", oPatient.PatientGuarantors[i].SSN, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sGender", oPatient.PatientGuarantors[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sRelation", oPatient.PatientGuarantors[i].Relation, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sAddressLine1", oPatient.PatientGuarantors[i].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sAddressLine2", oPatient.PatientGuarantors[i].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sCity", oPatient.PatientGuarantors[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sState", oPatient.PatientGuarantors[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sZIP", oPatient.PatientGuarantors[i].Zip, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sPhone", oPatient.PatientGuarantors[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sMobile", oPatient.PatientGuarantors[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sFax", oPatient.PatientGuarantors[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sEmail", oPatient.PatientGuarantors[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@bIsActive", oPatient.PatientGuarantors[i].IsActive, ParameterDirection.Input, SqlDbType.Bit);
                                    odbParams.Add("@nVisitID", oPatient.PatientGuarantors[i].VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nAppointmentID", oPatient.PatientGuarantors[i].AppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nGuarantorAsPatientID", oPatient.PatientGuarantors[i].GuarantorAsPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactTypeFlag", oPatient.PatientGuarantors[i].nGuarantorTypeFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@sCounty", oPatient.PatientGuarantors[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sCountry", oPatient.PatientGuarantors[i].Country, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nGuarantorType", oPatient.PatientGuarantors[i].GurantorType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@bIsAccountGuarantor", oPatient.PatientGuarantors[i].IsAccountGuarantor, ParameterDirection.Input, SqlDbType.Bit);
                                    oDB.Execute("PA_INUP_Patient_Guarantor_OtherContacts", odbParams, out GuarantorId);

                                }
                            }
                            #endregion "PatientGuarantors Saving"

                        }
                        //Indicates Existing account
                        else
                        {
                            accountId = oPatient.Account.PAccountID;
                        }

                        #region"PatientAccount Saving"

                        odbParams.Clear();
                        odbParams.Add("@nAccountPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nPatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@sAccountNo", oPatient.PatientAccount.AccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sPatientCode", oPatient.PatientAccount.PatientCode, ParameterDirection.Input, SqlDbType.VarChar);
                        if (oPatient.Account.AccountClosedDate != null && oPatient.PatientAccount.AccountClosedDate != DateTime.MinValue)
                        {
                            odbParams.Add("@dtAccountClosedDate", oPatient.PatientAccount.AccountClosedDate, ParameterDirection.Input, SqlDbType.DateTime);
                        }
                        else
                        {
                            odbParams.Add("@dtAccountClosedDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                        }
                        odbParams.Add("@nClinicID", oPatient.PatientAccount.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nSiteID", oPatient.PatientAccount.SiteID, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@sMachineName", oPatient.PatientAccount.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@nUserID", oPatient.PatientAccount.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@dtRecordDate", oPatient.PatientAccount.RecordDate, ParameterDirection.Input, SqlDbType.DateTime);
                        odbParams.Add("@bIsActive", oPatient.PatientAccount.Active, ParameterDirection.Input, SqlDbType.Bit);
                        odbParams.Add("@bIsOwnAccount", oPatient.PatientAccount.OwnAccount, ParameterDirection.Input, SqlDbType.Bit);

                        oDB.Execute("PA_InUp_Accounts_Patients", odbParams);

                        #endregion "PatientAccount Saving"
                    }
                }


                //#region "PatientOtherGuarantors Saving"



                //Patient Other Guarantors saving
                //string _sqlquery = "";
                    //= "delete from Patient_OtherContacts where nPatientID = '" + Convert.ToInt64(patientID) + "' and nPAccountID = 0";
                //oDB.Execute_Query(_sqlquery);
                //for (int i = 0; i < oPatient.PatientOtherGuarantors.Count; i++)
                //{
                //    Object GuarantorId;
                //    odbParams.Clear();
                //    //If it is SameAsPatient assign patientID to GuarantorAsPatientID
                //    if (oPatient.PatientOtherGuarantors[i].OtherConatctType.GetHashCode() == PatientOtherContactType.SameAsPatient.GetHashCode())
                //    {
                //        oPatient.PatientOtherGuarantors[i].GuarantorAsPatientID = Convert.ToInt64(patientID);
                //    }

                //    odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nPatientContactID", oPatient.PatientOtherGuarantors[i].PatientContactID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                //    odbParams.Add("@nLineNumber", i, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nPatientContactType", oPatient.PatientOtherGuarantors[i].OtherConatctType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                //    odbParams.Add("@sFirstName", oPatient.PatientOtherGuarantors[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMiddleName", oPatient.PatientOtherGuarantors[i].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sLastName", oPatient.PatientOtherGuarantors[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                //    if (oPatient.PatientOtherGuarantors[i].DOB != null && oPatient.PatientOtherGuarantors[i].DOB != DateTime.MinValue)
                //    {
                //        odbParams.Add("@nDOB", gloDateMaster.gloDate.DateAsNumber(oPatient.PatientOtherGuarantors[i].DOB.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                //    }
                //    else
                //    {
                //        odbParams.Add("@nDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                //    }
                //    odbParams.Add("@sSSN", oPatient.PatientOtherGuarantors[i].SSN, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sGender", oPatient.PatientOtherGuarantors[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sRelation", oPatient.PatientOtherGuarantors[i].Relation, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine1", oPatient.PatientOtherGuarantors[i].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sAddressLine2", oPatient.PatientOtherGuarantors[i].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCity", oPatient.PatientOtherGuarantors[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sState", oPatient.PatientOtherGuarantors[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sZIP", oPatient.PatientOtherGuarantors[i].Zip, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sPhone", oPatient.PatientOtherGuarantors[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sMobile", oPatient.PatientOtherGuarantors[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sFax", oPatient.PatientOtherGuarantors[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sEmail", oPatient.PatientOtherGuarantors[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@bIsActive", oPatient.PatientOtherGuarantors[i].IsActive, ParameterDirection.Input, SqlDbType.Bit);
                //    odbParams.Add("@nVisitID", oPatient.PatientOtherGuarantors[i].VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nAppointmentID", oPatient.PatientOtherGuarantors[i].AppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nGuarantorAsPatientID", oPatient.PatientOtherGuarantors[i].GuarantorAsPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nPatientContactTypeFlag", oPatient.PatientOtherGuarantors[i].nGuarantorTypeFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                //    odbParams.Add("@sCounty", oPatient.PatientOtherGuarantors[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@sCountry", oPatient.PatientOtherGuarantors[i].Country, ParameterDirection.Input, SqlDbType.VarChar);
                //    odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@nGuarantorType", oPatient.PatientOtherGuarantors[i].GurantorType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                //    odbParams.Add("@nPAccountID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                //    odbParams.Add("@bIsAccountGuarantor", 0, ParameterDirection.Input, SqlDbType.Bit);
                //  //  oDB.Execute("PA_INUP_Patient_Guarantor_OtherContacts", odbParams, out GuarantorId);

                //}

               // #endregion "PatientOtherGuarantors Saving"

                //Added on 28/07/2016 by Juily : Start
                //This all code in comment becasue Add N Remove Other Guarantors logic added in SP through TVP
                //Patient Other Guarantors saving through TVP
                DtGuarantor.Columns.Add("nPatientID", typeof(long));
                DtGuarantor.Columns.Add("nPatientContactID", typeof(long));
                DtGuarantor.Columns.Add("nLineNumber", typeof(long));
                DtGuarantor.Columns.Add("nPatientContactType", typeof(int));
                DtGuarantor.Columns.Add("sFirstName", typeof(string));
                DtGuarantor.Columns.Add("sMiddleName", typeof(string));
                DtGuarantor.Columns.Add("sLastName", typeof(string));
                DtGuarantor.Columns.Add("nDOB", typeof(int));
                DtGuarantor.Columns.Add("sSSN", typeof(string));
                DtGuarantor.Columns.Add("sGender", typeof(string));
                DtGuarantor.Columns.Add("sRelation", typeof(string));
                DtGuarantor.Columns.Add("sAddressLine1", typeof(string));
                DtGuarantor.Columns.Add("sAddressLine2", typeof(string));
                DtGuarantor.Columns.Add("sCity", typeof(string));
                DtGuarantor.Columns.Add("sState", typeof(string));
                DtGuarantor.Columns.Add("sZIP", typeof(string));
                DtGuarantor.Columns.Add("sPhone", typeof(string));
                DtGuarantor.Columns.Add("Mobile", typeof(string));
                DtGuarantor.Columns.Add("sFax", typeof(string));
                DtGuarantor.Columns.Add("sEmail", typeof(string));
                DtGuarantor.Columns.Add("bIsActive", typeof(Boolean));
                DtGuarantor.Columns.Add("nVisitID", typeof(long));
                DtGuarantor.Columns.Add("nAppointmentID", typeof(long));
                DtGuarantor.Columns.Add("nGuarantorAsPatientID", typeof(long));
                //REsolved case-CAS-04906-S2B0S5 CRM:0022362
                DtGuarantor.Columns.Add("nPatientContactTypeFlag", typeof(int));
                DtGuarantor.Columns.Add("sCounty", typeof(string));
                DtGuarantor.Columns.Add("sCountry", typeof(string));
                DtGuarantor.Columns.Add("nClinicID", typeof(long));
                DtGuarantor.Columns.Add("nGuarantorType", typeof(int));
                DtGuarantor.Columns.Add("nPAccountId", typeof(long));
                DtGuarantor.Columns.Add("bIsAccountGuarantor", typeof(Boolean ));

                object nDOB = DBNull.Value;

                for (int i = 0; i < oPatient.PatientOtherGuarantors.Count; i++)
                {
                    if (oPatient.PatientOtherGuarantors[i].DOB != null && oPatient.PatientOtherGuarantors[i].DOB != DateTime.MinValue)
                    {
                        nDOB = gloDateMaster.gloDate.DateAsNumber(oPatient.PatientOtherGuarantors[i].DOB.ToShortDateString());
                    }
                    else
                    {
                        nDOB = DBNull.Value;
                    }

                    DtGuarantor.Rows.Add(new object[] { Convert.ToInt64(patientID) ,  oPatient.PatientOtherGuarantors[i].PatientContactID , i, oPatient.PatientOtherGuarantors[i].OtherConatctType.GetHashCode() , 
                    oPatient.PatientOtherGuarantors[i].FirstName , oPatient.PatientOtherGuarantors[i].MiddleName  , oPatient.PatientOtherGuarantors[i].LastName , nDOB , 
                    oPatient.PatientOtherGuarantors[i].SSN , oPatient.PatientOtherGuarantors[i].Gender , oPatient.PatientOtherGuarantors[i].Relation , oPatient.PatientOtherGuarantors[i].AddressLine1 , oPatient.PatientOtherGuarantors[i].AddressLine2 , 
                    oPatient.PatientOtherGuarantors[i].City , oPatient.PatientOtherGuarantors[i].State , oPatient.PatientOtherGuarantors[i].Zip , oPatient.PatientOtherGuarantors[i].Phone , oPatient.PatientOtherGuarantors[i].Mobile , 
                    oPatient.PatientOtherGuarantors[i].Fax , oPatient.PatientOtherGuarantors[i].Email , oPatient.PatientOtherGuarantors[i].IsActive , oPatient.PatientOtherGuarantors[i].VisitID , oPatient.PatientOtherGuarantors[i].AppointmentID ,
                    oPatient.PatientOtherGuarantors[i].GuarantorAsPatientID , oPatient.PatientOtherGuarantors[i].nGuarantorTypeFlag.GetHashCode() , oPatient.PatientOtherGuarantors[i].County , oPatient.PatientOtherGuarantors[i].Country ,
                    _ClinicID , oPatient.PatientOtherGuarantors[i].GurantorType.GetHashCode() , 0 , 0 });
                }                

                //Added on 28/07/2016 by Juily
                //after all information store in data table call final SP
                cmd = new SqlCommand("SP_PA_PATIENT_DTL_Modify", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tvpparameters", DtPatientDTL);
                cmd.Parameters.AddWithValue("@User_Name", Convert.ToString(appSettings["UserName"]));  //_username
                cmd.Parameters.AddWithValue("@nPatientId", patientID);
                cmd.Parameters.AddWithValue("@nclinicID", _ClinicID);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "dbo.TVP_PATIENT_DTL_Modify";
                cmd.Parameters.AddWithValue("@tvpGuaran", DtGuarantor);
                param1.SqlDbType = SqlDbType.Structured;
                param1.TypeName = "dbo.TVP_PATIENT_DTL_Guarantors";
                cmd.Parameters.AddWithValue("@tvpParaInsu", DTInsurance);
                param2.SqlDbType = SqlDbType.Structured;
                param2.TypeName = "dbo.TVP_PATIENT_DTL_Insurance";
                cmd.Parameters.AddWithValue("@nProviderID", gloGlobal.gloPMGlobal.LoginProviderID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                DtPatientDTL.Rows.Clear();
                DtPatientDTL.Dispose();
                DTInsurance.Rows.Clear();
                DTInsurance.Dispose();
                DtGuarantor.Rows.Clear();
                DtGuarantor.Dispose();
                cmd.Dispose();
                con.Dispose();
                //-----------------------------------------------------End

                #region "PatientRepresentative Saving"


                ////_sqlquery = "delete from PatientRepresentative_mst  ";
                ////_sqlquery += " where  nprid in (select nprid from (select nprid,count(nprid)cnt from (select * from PatientRepresentative_dtl where nprid in (select nprid from PatientRepresentative_dtl prdtl where prdtl.nPatientID = " + Convert.ToInt64(patientID) + ")) t group by nprid,npatientid ) t1 where cnt > 1 )";
                ////oDB.Execute_Query(_sqlquery);
                ////_sqlquery = "delete from PatientPortalAccess  ";
                ////_sqlquery += " where  nPatientID in (select nprid from PatientRepresentative_dtl prdtl where prdtl.nPatientID = " + Convert.ToInt64(patientID) + ") and nPatientID not in (select nprid from PatientRepresentative_mst)";
                ////oDB.Execute_Query(_sqlquery);
                ////_sqlquery = "delete from PatientRepresentative_dtl    ";
                ////_sqlquery += " where  nPatientID = " + Convert.ToInt64(patientID) + "";
                ////oDB.Execute_Query(_sqlquery);
                odbParams.Clear();
                object _removedPRID = "";
                odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@nModuleID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("gsp_DeAssociate_PatientRepresentative", odbParams, out _removedPRID);


                for (int i = 0; i < oPatient.PatientRepresentatives.Count; i++)
                {
                    Object PatientRepresentativeId;
                    odbParams.Clear();

                    odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@sFirstName", oPatient.PatientRepresentatives[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sLastName", oPatient.PatientRepresentatives[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@dtDOB", oPatient.PatientRepresentatives[i].DOB, ParameterDirection.Input, SqlDbType.DateTime);
                    odbParams.Add("@sGender", oPatient.PatientRepresentatives[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sEmail", oPatient.PatientRepresentatives[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sPhone", oPatient.PatientRepresentatives[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sUserName", oPatient.PatientRepresentatives[i].UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sPassword", oPatient.PatientRepresentatives[i].Password, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@nvPRId", oPatient.PatientRepresentatives[i].PRId, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@sSecurityQuestion", oPatient.PatientRepresentatives[i].SecurityQuestion, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sSecurityAnswer", oPatient.PatientRepresentatives[i].SecurityAnswer, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Execute("gsp_INUP_PatientRepresentative", odbParams, out PatientRepresentativeId);

                }

                #endregion "PatientRepresentative Saving"
                #region "APIRepresentative Saving"


                ////_sqlquery = "delete from PatientRepresentative_mst  ";
                ////_sqlquery += " where  nprid in (select nprid from (select nprid,count(nprid)cnt from (select * from PatientRepresentative_dtl where nprid in (select nprid from PatientRepresentative_dtl prdtl where prdtl.nPatientID = " + Convert.ToInt64(patientID) + ")) t group by nprid,npatientid ) t1 where cnt > 1 )";
                ////oDB.Execute_Query(_sqlquery);
                ////////_sqlquery = "delete from PatientPortalAccess  ";
                ////////_sqlquery += " where  nPatientID in (select nprid from PatientRepresentative_dtl prdtl where prdtl.nPatientID = " + Convert.ToInt64(patientID) + ") and nPatientID not in (select nprid from PatientRepresentative_mst)";
                ////////oDB.Execute_Query(_sqlquery);
                ////_sqlquery = "delete from PatientRepresentative_dtl    ";
                ////_sqlquery += " where  nPatientID = " + Convert.ToInt64(patientID) + "";
                ////oDB.Execute_Query(_sqlquery);
                odbParams.Clear();
                object _removedAPIPRID = "";
                odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@nModuleID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("gsp_DeAssociate_PatientRepresentative", odbParams, out _removedAPIPRID);

                for (int i = 0; i < oPatient.APIRepresentatives.Count; i++)
                {
                    Object PatientAPIRepresentativeId;
                    odbParams.Clear();
                    int bIsInsert = 0;
                    Object processResult=-1;
                    if (oPatient.APIRepresentatives[i].PRId != 0)
                    {

                        bIsInsert = 1;
                    }
                    odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@sFirstName", oPatient.APIRepresentatives[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sLastName", oPatient.APIRepresentatives[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@dtDOB", oPatient.APIRepresentatives[i].DOB, ParameterDirection.Input, SqlDbType.DateTime);
                    odbParams.Add("@sGender", oPatient.APIRepresentatives[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sEmail", oPatient.APIRepresentatives[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sPhone", oPatient.APIRepresentatives[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sUserName", oPatient.APIRepresentatives[i].UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sPassword", oPatient.APIRepresentatives[i].Password, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@nvPRId", oPatient.APIRepresentatives[i].PRId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    odbParams.Add("@sSecurityQuestion", oPatient.APIRepresentatives[i].SecurityQuestion, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sSecurityAnswer", oPatient.APIRepresentatives[i].SecurityAnswer, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@ProcessResult", processResult, ParameterDirection.InputOutput, SqlDbType.VarChar);
                    oDB.Execute("gsp_INUP_PatientAPIRepresentative", odbParams, out PatientAPIRepresentativeId, out processResult);

                   

                    if (Convert.ToInt32(processResult)==1)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientRepresentative, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.Add, "Patient Representative " + oPatient.APIRepresentatives[i].FirstName + " " + oPatient.APIRepresentatives[i].LastName + " added", Convert.ToInt64(PatientAPIRepresentativeId), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        
                    }
                    else if (Convert.ToInt32(processResult) ==2)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientRepresentative, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.Modify, "Patient Representative " + oPatient.APIRepresentatives[i].FirstName + " " + oPatient.APIRepresentatives[i].LastName + " modified", Convert.ToInt64(PatientAPIRepresentativeId), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    }
                    else if (Convert.ToInt32(processResult) == 3)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientRepresentative, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.Add, "Patient Representative " + oPatient.APIRepresentatives[i].FirstName + " " + oPatient.APIRepresentatives[i].LastName + " added", Convert.ToInt64(PatientAPIRepresentativeId), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientRepresentative, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.AssociatedWithPatient, "Patient Representative " + oPatient.APIRepresentatives[i].FirstName + " " + oPatient.APIRepresentatives[i].LastName + " associated with patient", Convert.ToInt64(patientID), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                       
                    }
                    else if (Convert.ToInt32(processResult) == 4)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientRepresentative, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.AssociatedWithPatient, "Patient Representative " + oPatient.APIRepresentatives[i].FirstName + " " + oPatient.APIRepresentatives[i].LastName + " associated with patient", Convert.ToInt64(patientID), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    }
                    APIAccess[] arrAPIAccess = new APIAccess[1];

                    APIAccess objAPIAccess = new APIAccess();
                    objAPIAccess.APIUserID = Convert.ToInt64(PatientAPIRepresentativeId);
                    objAPIAccess.UserName = oPatient.APIRepresentatives[i].UserName;
                    objAPIAccess.Password = oPatient.APIRepresentatives[i].Password;

                    arrAPIAccess[0] = objAPIAccess;

                    clsAPIAcceess objclsAPIAcceess = new clsAPIAcceess();
                    Int64 _result = -1;
                    _result= objclsAPIAcceess.APIAccessProceess(_databaseconnectionstring, arrAPIAccess, 1, 2, "", DateTime.Now,bEncryptPassword:false);
                    objclsAPIAcceess = null;

                    if (bIsInsert == 0)
                    {
                     //   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientRepresentative, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.Add, "Patient Representative added", Convert.ToInt64(PatientAPIRepresentativeId), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.Activate, "API activated for Patient Representative " + oPatient.APIRepresentatives[i].FirstName + " " + oPatient.APIRepresentatives[i].LastName , Convert.ToInt64(PatientAPIRepresentativeId), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                }

                #endregion "APIRepresentative Saving"
                #region PatientPortalAccount

                if (oPatient.PatientPortalAccount != null)
                {
                    odbParams.Clear();
                    odbParams.Add("@nPatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);

                    odbParams.Add("@dtDateofTraining", oPatient.PatientPortalAccount.DateOfTraining, ParameterDirection.Input, SqlDbType.DateTime);


                    odbParams.Add("@bIsTrainingProvided", oPatient.PatientPortalAccount.IsTrainingProvided, ParameterDirection.Input, SqlDbType.Bit);

                    oDB.Execute("gsp_UP_PatientPortalAccountsUpdate", odbParams);
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
                #endregion
                #region APIAccount

                if (oPatient.APIAccount != null)
                {
                    odbParams = new gloDatabaseLayer.DBParameters();
                    odbParams.Clear();
                    odbParams.Add("@nPatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);

                    odbParams.Add("@dtDateofTraining", oPatient.APIAccount.DateOfTraining, ParameterDirection.Input, SqlDbType.DateTime);


                    odbParams.Add("@bIsTrainingProvided", oPatient.APIAccount.IsTrainingProvided, ParameterDirection.Input, SqlDbType.Bit);

                    oDB.Execute("gsp_UP_PatientAPIAccountsUpdate", odbParams);
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
                #endregion
                #region "UpdateAccount Address When Patient Address changed"

                //Update account address when patient address changes.
                //Sagar Ghodke 20110822 - Removing the isPatientDataModified condition and updating all the time
                //This is coz the mask control data need to check for is changes done
                //if (oPatient._IsPatientDataModified == true)

                {
                    odbParams = new gloDatabaseLayer.DBParameters();
                    odbParams.Add("@nPatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@sFirstName", oPatient.DemographicsDetail.PatientFirstName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sMiddleName", oPatient.DemographicsDetail.PatientMiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sLastName", oPatient.DemographicsDetail.PatientLastName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sAddress1", oPatient.DemographicsDetail.PatientAddress1, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sAddress2", oPatient.DemographicsDetail.PatientAddress2, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sCity", oPatient.DemographicsDetail.PatientCity, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sState", oPatient.DemographicsDetail.PatientState, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sCountry", oPatient.DemographicsDetail.PatientCountry, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sCounty", oPatient.DemographicsDetail.PatientCounty, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@szip", oPatient.DemographicsDetail.PatientZip, ParameterDirection.Input, SqlDbType.VarChar);

                    odbParams.Add("@sPhone", oPatient.DemographicsDetail.PatientPhone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sMobile", oPatient.DemographicsDetail.PatientMobile, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sFax", oPatient.DemographicsDetail.PatientFax, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sEmail", oPatient.DemographicsDetail.PatientEmail, ParameterDirection.Input, SqlDbType.VarChar);
                    if (oPatient.DemographicsDetail.PatientDOB != null && oPatient.DemographicsDetail.PatientDOB != DateTime.MinValue)
                    {
                        odbParams.Add("@nDOB", gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                    }
                    else
                    {
                        odbParams.Add("@nDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                    }
                    odbParams.Add("@sSSN", oPatient.DemographicsDetail.PatientSSN, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sGender", oPatient.DemographicsDetail.PatientGender, ParameterDirection.Input, SqlDbType.VarChar);

                    oDB.Execute("PA_UpdateAccountAddress", odbParams);
                }

                #endregion "UpdateAccount Address When Patient Address changed"

                #region "Update PatientCode in PA_Accounts_Patients Table When PatientCode changed"

                if (oPatient._IsPatientCodeModified == true)
                {
                    string _SqlQuery = "Update PA_Accounts_Patients Set sPatientCode = '" + oPatient.DemographicsDetail.PatientCode + "' Where nPatientID =" + patientID;
                    oDB.Execute_Query(_SqlQuery);
                }
                #endregion"Update PatientCode in PA_Accounts_Patients Table When PatientCode changed"

                #endregion "Patient Guarantors and Account Saving"

                //Added by Mukesh 20090829
                //Add additional Patient Information in Patient Setting Table
                #region "Patient Setting"

                //if (oPatient.DemographicsDetail != null)
                //{
                //    if (IsTableExists("PatientSettings"))
                //    {
                //        #region "Exclude from Statement"
                //        //Exclude from Statement
                //        odbParams = new gloDatabaseLayer.DBParameters();
                //        odbParams.Add("@PatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                //        odbParams.Add("@SettingName", "Exclude from Statement", ParameterDirection.Input, SqlDbType.VarChar);
                //        if (oPatient.DemographicsDetail.ExcludeFromStatement == true)
                //        {
                //            odbParams.Add("@SettingValue", "1", ParameterDirection.Input, SqlDbType.VarChar);
                //        }
                //        else
                //        {
                //            odbParams.Add("@SettingValue", "0", ParameterDirection.Input, SqlDbType.VarChar);
                //        }
                //        odbParams.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //        oDB.Execute("gsp_InUpPatientSettings", odbParams);

                //        #endregion

                //    }
                //}
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
                if (oPatient.PatientDemographicOtherInfo != null)
                {
                    if (IsTableExists("PatientSettings"))
                    {
                        #region "SOF Date"
                        //Signature on File Date
                        odbParams = new gloDatabaseLayer.DBParameters();
                        odbParams.Add("@PatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@SettingName", "Signature on File Date", ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@SettingValue", gloDateMaster.gloDate.DateAsNumber(oPatient.PatientDemographicOtherInfo.SOFDate.ToShortDateString()), ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Execute("gsp_InUpPatientSettings", odbParams);
                        odbParams.Dispose();
                        odbParams = null;

                        #endregion

                        #region "CMS1500 Box13"
                        //CMS1500 Box13
                        odbParams = new gloDatabaseLayer.DBParameters();
                        odbParams.Add("@PatientID", Convert.ToInt64(patientID), ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@SettingName", "CMS1500 Box13", ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@SettingValue", oPatient.PatientDemographicOtherInfo.CMS1500Box13, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Execute("gsp_InUpPatientSettings", odbParams);
                        odbParams.Dispose();
                        odbParams = null;

                        #endregion


                    }
                }
                #endregion "Patinet Setting"

                if (patientID != null)
                {
                    oPatient.PatientID = Convert.ToInt64(patientID);
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                throw new gloDatabaseLayer.DBException("Error in database execution - ", ex.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDB.Dispose();
                oDBParameters.Dispose();
            }

            return Convert.ToInt64(patientID);
        }

        //OpenSetupCases function added  made for bugid 83503 
        private void OpenSetupCases(Int64 Caseid)
        {


            //Assembly myAssembly = null;
            //Type myType = null;
            //ConstructorInfo magicConstructor = null;
            //object magicClassObject = null;
            //MethodInfo Exampleb = null;
            //object[] obj = new object[3];


            try
            {

                //myAssembly = System.Reflection.Assembly.LoadFrom("gloBilling.dll");
                //myType = myAssembly.GetType("gloBilling.clsCaseSetup");
                //magicConstructor = myType.GetConstructor(Type.EmptyTypes);
                //magicClassObject = magicConstructor.Invoke(new object[] { });

                //Exampleb = myType.GetMethod("OpenSetupCases");
                //obj[0] = patientID;
                //obj[1] = Caseid;

                ////17-Nov-15 Aniket: Resolving Bug #90191: gloEMR: OB vitals- View OB vitals winow looses focus and does not allow to perform any operation, user has to kill application
                //obj[2] = frmParentForm;


                //Exampleb.Invoke(magicClassObject, obj);
                gloGlobal.LoadFromAssembly.OpenSetupCases((long)patientID, Caseid, frmParentForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                //if ((Exampleb != null))
                //{
                //    Exampleb = null;
                //}

                //if ((magicClassObject != null))
                //{
                //    magicClassObject = null;
                //}

                //if ((magicConstructor != null))
                //{
                //    magicConstructor = null;
                //}

                //if ((myType != null))
                //{
                //    myType = null;
                //}

                //if ((myAssembly != null))
                //{
                //    myAssembly = null;
                //}




            }
        }
        public PatientRepresentative GetPatientRepresentativesById(long nPRId ,int IsForAPImode=0)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPR = null;
            PatientRepresentative oPatientRepresentative = new PatientRepresentative();
            try
            {
                string _strSqlQuery = "";
                oDB.Connect(false);
               
                if (IsForAPImode == 0)
                {
                     _strSqlQuery = "Select nprid, isnull(sFirstName,'')sFirstName,isnull(sLastName,'')sLastName,isnull(dtDOB,'')dtDOB,isnull(sGender,'')sGender,isnull(sEmail,'')sEmail,isnull(sPhone,'')sPhone ,isnull(sloginname,'') sloginname,isnull(sportalpassword ,'') sportalpassword ,patientportalaccess.SecurityQuestion,patientportalaccess.SecurityAnswer " +
                             " From PatientRepresentative_mst prmst left join (select isnull(sloginname,'')  sloginname,isnull(sportalpassword ,'') sportalpassword,npatientid,isnull(sSecurityQuestion,'') SecurityQuestion,isnull(sSecurityAnswer,'') SecurityAnswer   from patientportalaccess where sapptype='PatientPortal') patientportalaccess on prmst.nprid = patientportalaccess.npatientid " +
                             " Where nPRId = " + nPRId;
                }
                else
                {
                    _strSqlQuery = "Select nprid, isnull(sFirstName,'')sFirstName,isnull(sLastName,'')sLastName,isnull(dtDOB,'')dtDOB,isnull(sGender,'')sGender,isnull(sEmail,'')sEmail,isnull(sPhone,'')sPhone ,isnull(sloginname,'') sloginname,isnull(sportalpassword ,'') sportalpassword ,'' as SecurityQuestion,'' as SecurityAnswer " +
                           " From PatientRepresentative_mst prmst left join (select isnull(sloginname,'')  sloginname,isnull(sportalpassword ,'') sportalpassword,napiaccessuserid   from ApiAccess where sapptype='APIAccess') ApiAccess on prmst.nprid = ApiAccess.napiaccessuserid " +
                           " Where nPRId = " + nPRId;
                }
                oDB.Retrive_Query(_strSqlQuery, out dtPR);

                //if (dt != null)
                if (dtPR != null && dtPR.Rows.Count > 0)
                {
                    if (dtPR.Rows.Count > 0)
                    {
                        oPatientRepresentative.PRId = Convert.ToInt64(dtPR.Rows[0]["nprid"]);
                        oPatientRepresentative.FirstName = dtPR.Rows[0]["sFirstName"].ToString();
                        oPatientRepresentative.LastName = dtPR.Rows[0]["sLastName"].ToString();
                        oPatientRepresentative.DOB = (DateTime)dtPR.Rows[0]["dtDOB"];
                        oPatientRepresentative.Gender = dtPR.Rows[0]["sGender"].ToString();
                        oPatientRepresentative.Email = dtPR.Rows[0]["sEmail"].ToString();
                        oPatientRepresentative.Phone = dtPR.Rows[0]["sPhone"].ToString();
                        oPatientRepresentative.UserName = dtPR.Rows[0]["sLoginName"].ToString();
                        oPatientRepresentative.Password = dtPR.Rows[0]["sportalpassword"].ToString();
                        oPatientRepresentative.SecurityQuestion = dtPR.Rows[0]["SecurityQuestion"].ToString();
                        oPatientRepresentative.SecurityAnswer = dtPR.Rows[0]["SecurityAnswer"].ToString();
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dtPR != null)
                {
                    dtPR.Dispose();
                    dtPR = null;
                }
            }
            return oPatientRepresentative;
        }

        public void Add_ReferraltoPatient(PatientDetails PatientReferrals)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams;
            oDB.Connect(false);
            for (int i = 0; i < PatientReferrals.Count; i++)
            {
                odbParams = new gloDatabaseLayer.DBParameters();

                odbParams.Add("@nPatientID", PatientReferrals[i].PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@nPatientDetailID", PatientReferrals[i].PatientDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@nContactId", PatientReferrals[i].ContactId, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@sName", PatientReferrals[i].Name, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sContact", PatientReferrals[i].Contact, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sAddressLine1", PatientReferrals[i].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sAddressLine2", PatientReferrals[i].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sCity", PatientReferrals[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sState", PatientReferrals[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sZIP", PatientReferrals[i].ZIP, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sPhone", PatientReferrals[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sFax", PatientReferrals[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sEmail", PatientReferrals[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sURL", PatientReferrals[i].URL, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sMobile", PatientReferrals[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sPager", PatientReferrals[i].Pager, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sNotes", PatientReferrals[i].Notes, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sFirstName", PatientReferrals[i].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sMiddleName", PatientReferrals[i].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sLastName", PatientReferrals[i].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sGender", PatientReferrals[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sTaxonomy", PatientReferrals[i].Taxonomy, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sTaxonomyDesc", PatientReferrals[i].TaxonomyDesc, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sTaxID", PatientReferrals[i].TaxID, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sUPIN", PatientReferrals[i].UPIN, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sNPI", PatientReferrals[i].NPI, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sHospitalAffiliation", PatientReferrals[i].HospitalAffiliation, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sExternalCode", PatientReferrals[i].ExternalCode, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sDegree", PatientReferrals[i].Degree, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@nContactFlag", PatientContactType.Referral.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("PA_Fill_Referrals", odbParams);
                odbParams.Dispose();
                odbParams = null;
            }
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public void Add_InsurancePlan(PatientInsuranceOther oInsuranceDetails, Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                //For Insurances
                //Delete if any already referrals 
                //code comment start by nilesh on 20110110 for case#GLO2010-0007999
                //oDB.Execute_Query("Delete from PatientInsurance_DTL where nPatientID='" + PatientID + "'");
                //code comment end by nilesh on 20110110 for case#GLO2010-0007999

                for (int i = 0; i <= oInsuranceDetails.InsurancesDetails.Count - 1; i++)
                {
                    odbParams = new gloDatabaseLayer.DBParameters();
                    //nPatientID, nInsuranceID, sSubscriberName, sSubscriberPolicy#, sSubscriberID, sGroup, sEmployer,
                    //sPhone, dtDOB, bPrimaryFlag
                    odbParams.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@InsuranceID", oInsuranceDetails.InsurancesDetails[i].InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                    // odbParams.Add("@SubscriberName", oInsuranceDetails.InsurancesDetails[i].SubscriberName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@SubscriberPolicy#", oInsuranceDetails.InsurancesDetails[i].SubscriberPolicy, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@SubscriberID", oInsuranceDetails.InsurancesDetails[i].SubscriberID, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@Group", oInsuranceDetails.InsurancesDetails[i].Group, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@Employer", oInsuranceDetails.InsurancesDetails[i].Employer, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@Phone", oInsuranceDetails.InsurancesDetails[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@DOB", oInsuranceDetails.InsurancesDetails[i].DOB, ParameterDirection.Input, SqlDbType.DateTime);
                    odbParams.Add("@InsuranceFlag", oInsuranceDetails.InsurancesDetails[i].InsuranceFlag, ParameterDirection.Input, SqlDbType.Int);
                    odbParams.Add("@IsNotDOB", oInsuranceDetails.InsurancesDetails[i].IsNotDOB, ParameterDirection.Input, SqlDbType.Bit);
                    //odbParams.Add("@InsuranceID", oPatient.Referrals[i].ReferralID, ParameterDirection.Input, SqlDbType.BigInt);

                    //@SubFName Varchar(50),
                    odbParams.Add("@SubFName", oInsuranceDetails.InsurancesDetails[i].SubscriberFName, ParameterDirection.Input, SqlDbType.VarChar);
                    //@SubMName Varchar(50),
                    odbParams.Add("@SubMName", oInsuranceDetails.InsurancesDetails[i].SubscriberMName, ParameterDirection.Input, SqlDbType.VarChar);
                    //@SubLName Varchar(50), 
                    odbParams.Add("@SubLName", oInsuranceDetails.InsurancesDetails[i].SubscriberLName, ParameterDirection.Input, SqlDbType.VarChar);
                    //@RelationShipID numeric(18,0), 

                    //Fields added on 20081030
                    odbParams.Add("@sSubscriberAddr1", oInsuranceDetails.InsurancesDetails[i].SubscriberAddr1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                    odbParams.Add("@sSubscriberAddr2", oInsuranceDetails.InsurancesDetails[i].SubscriberAddr2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                    odbParams.Add("@sSubscriberState", oInsuranceDetails.InsurancesDetails[i].SubscriberState, ParameterDirection.Input, SqlDbType.VarChar, 255);
                    odbParams.Add("@sSubscriberCity", oInsuranceDetails.InsurancesDetails[i].SubscriberCity, ParameterDirection.Input, SqlDbType.VarChar, 255);
                    odbParams.Add("@sSubscriberZip", oInsuranceDetails.InsurancesDetails[i].SubscriberZip, ParameterDirection.Input, SqlDbType.VarChar, 255);
                    //

                    odbParams.Add("@RelationShipID", oInsuranceDetails.InsurancesDetails[i].RelationshipID, ParameterDirection.Input, SqlDbType.BigInt);
                    //@RelationShip Varchar(50),
                    odbParams.Add("@RelationShip", oInsuranceDetails.InsurancesDetails[i].RelationshipName, ParameterDirection.Input, SqlDbType.VarChar);
                    //@Deductableamount Decimal(18,0), 
                    odbParams.Add("@Deductableamount", oInsuranceDetails.InsurancesDetails[i].DeductableAmount, ParameterDirection.Input, SqlDbType.Decimal);
                    //@CoveragePercent Decimal(18,0), 
                    odbParams.Add("@CoveragePercent", oInsuranceDetails.InsurancesDetails[i].CoveragePercent, ParameterDirection.Input, SqlDbType.Decimal);
                    //@CoPay Decimal(18,0),
                    odbParams.Add("@CoPay", oInsuranceDetails.InsurancesDetails[i].CoPay, ParameterDirection.Input, SqlDbType.Decimal);
                    //@AssignmentofBenifit Bit,
                    odbParams.Add("@AssignmentofBenifit", oInsuranceDetails.InsurancesDetails[i].AssignmentofBenefit, ParameterDirection.Input, SqlDbType.Bit);
                    //@IsNotStartDate   bit,
                    odbParams.Add("@IsNotStartDate", oInsuranceDetails.InsurancesDetails[i].IsNotStartDate, ParameterDirection.Input, SqlDbType.Bit);
                    //@dtStartDate DateTime, 
                    odbParams.Add("@dtStartDate", oInsuranceDetails.InsurancesDetails[i].StartDate, ParameterDirection.Input, SqlDbType.DateTime);
                    //@IsNotEndDate   bit,
                    odbParams.Add("@IsNotEndDate", oInsuranceDetails.InsurancesDetails[i].IsNotEndDate, ParameterDirection.Input, SqlDbType.Bit);
                    //@dtEndDate DateTime
                    odbParams.Add("@dtEndDate", oInsuranceDetails.InsurancesDetails[i].EndDate, ParameterDirection.Input, SqlDbType.DateTime);
                    //@dtEndDate DateTime
                    odbParams.Add("@sSubscriberGender", oInsuranceDetails.InsurancesDetails[i].SubscriberGender, ParameterDirection.Input, SqlDbType.VarChar);

                    //Insurance Details
                    odbParams.Add("@sInsuranceName", oInsuranceDetails.InsurancesDetails[i].InsuranceName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sPayerID", oInsuranceDetails.InsurancesDetails[i].PayerID, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sAddressLine1", oInsuranceDetails.InsurancesDetails[i].AddrressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sAddressLine2", oInsuranceDetails.InsurancesDetails[i].AddrressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sCity", oInsuranceDetails.InsurancesDetails[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sState", oInsuranceDetails.InsurancesDetails[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sZIP", oInsuranceDetails.InsurancesDetails[i].ZIP, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsurancePhone", oInsuranceDetails.InsurancesDetails[i].InsurancePhone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sFax", oInsuranceDetails.InsurancesDetails[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sEmail", oInsuranceDetails.InsurancesDetails[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sURL", oInsuranceDetails.InsurancesDetails[i].URL, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsuranceTypeCode", oInsuranceDetails.InsurancesDetails[i].InsuranceTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsuranceTypeDesc", oInsuranceDetails.InsurancesDetails[i].InsuranceTypeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@bAccessAssignment", oInsuranceDetails.InsurancesDetails[i].bAccessAssignment, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bStatementToPatient", oInsuranceDetails.InsurancesDetails[i].bStatementToPatient, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bMedigap", oInsuranceDetails.InsurancesDetails[i].bMedigap, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bReferringIDInBox19", oInsuranceDetails.InsurancesDetails[i].bReferringIDInBox19, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bNameOfacilityinBox33", oInsuranceDetails.InsurancesDetails[i].bNameOfacilityinBox33, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bDoNotPrintFacility", oInsuranceDetails.InsurancesDetails[i].bDoNotPrintFacility, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@b1stPointer", oInsuranceDetails.InsurancesDetails[i].b1stPointer, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bBox31Blank", oInsuranceDetails.InsurancesDetails[i].bBox31Blank, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bShowPayment", oInsuranceDetails.InsurancesDetails[i].bShowPayment, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@nTypeOBilling", oInsuranceDetails.InsurancesDetails[i].nTypeOBilling, ParameterDirection.Input, SqlDbType.Int);
                    odbParams.Add("@nClearingHouse", oInsuranceDetails.InsurancesDetails[i].nClearingHouse, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@bIsClaims", oInsuranceDetails.InsurancesDetails[i].bIsClaims, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsRemittanceAdvice", oInsuranceDetails.InsurancesDetails[i].bIsRemittanceAdvice, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsRealTimeEligibility", oInsuranceDetails.InsurancesDetails[i].bIsRealTimeEligibility, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsElectronicCOB", oInsuranceDetails.InsurancesDetails[i].bIsElectronicCOB, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsRealTimeClaimStatus", oInsuranceDetails.InsurancesDetails[i].bIsRealTimeClaimStatus, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsEnrollmentRequired", oInsuranceDetails.InsurancesDetails[i].bIsEnrollmentRequired, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@sPayerPhone", oInsuranceDetails.InsurancesDetails[i].sPayerPhone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sWebsite", oInsuranceDetails.InsurancesDetails[i].sWebsite, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sServicingState", oInsuranceDetails.InsurancesDetails[i].sServicingState, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sComments", oInsuranceDetails.InsurancesDetails[i].sComments, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sPayerPhoneExtn", oInsuranceDetails.InsurancesDetails[i].sPayerPhoneExtn, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@nContactID", oInsuranceDetails.InsurancesDetails[i].ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@bNotesInBox19", oInsuranceDetails.InsurancesDetails[i].bNotesInBox19, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@sOfficeID", oInsuranceDetails.InsurancesDetails[i].OfficeID, ParameterDirection.Input, SqlDbType.VarChar);

                    odbParams.Add("@sSubscriberCounty", oInsuranceDetails.InsurancesDetails[i].SubscriberCounty, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sSubscriberCountry", oInsuranceDetails.InsurancesDetails[i].SubscriberCountry, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sCounty", oInsuranceDetails.InsurancesDetails[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sCountry", oInsuranceDetails.InsurancesDetails[i].Country, ParameterDirection.Input, SqlDbType.VarChar);

                    odbParams.Add("@bworkerscomp", oInsuranceDetails.InsurancesDetails[i].Isworkerscomp, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@bautoclaim", oInsuranceDetails.InsurancesDetails[i].Isautoclaim, ParameterDirection.Input, SqlDbType.VarChar);


                    oDB.Execute("PA_Insert_Insurances", odbParams);
                    if (odbParams != null)
                    {
                        odbParams.Dispose();
                        odbParams = null;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }
        }

        public bool IsTableExists(string strTableName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@sTableName", strTableName, ParameterDirection.InputOutput, SqlDbType.VarChar);
                object _oresult = new object();
                oDB.Execute("TABLEEXISTS", oParameters, out _oresult);
                if (Convert.ToInt64(_oresult) > 0 && _oresult != null)
                {
                    return true;
                }
                else
                {
                    //MessageBox.Show("Table Not Exists", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                //MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();

            }

        }

        public Int64 Modify(Int64 patientid, Patient oPatient)
        {
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                gloDatabaseLayer.DBParameter oDBParameter;

                try
                {
                    oDB.Connect(false);

                    // PatientID
                    oDBParameters.Add("@nPatientID", patientid, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    //@sPatientCode
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sPatientCode";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientCode;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
                    //
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@nClinicID";
                    oDBParameter.Value = oPatient.ClinicID;
                    oDBParameter.DataType = SqlDbType.BigInt;
                    oDBParameter.ParameterDirection = ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);
                    //

                    //@sFirstName
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFirstName";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientFirstName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMiddleName
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMiddleName";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientMiddleName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sLastName
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sLastName";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientLastName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@nSSN
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@nSSN";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientSSN;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@dtDOB
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@dtDOB";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientDOB;
                    oDBParameter.DataType = System.Data.SqlDbType.DateTime;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //, @sGender
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGender";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientGender;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMaritalStatus
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMaritalStatus";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientMaritalStatus;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sAddressLine1
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sAddressLine1";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientAddress1;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sAddressLine2
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sAddressLine2";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientAddress2;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sCity
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sCity";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientCity;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sState
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sState";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientState;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sZIP
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sZIP";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientZip;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sCounty
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sCounty";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientCounty;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sPhone
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sPhone";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientPhone;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //, @sMobile
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMobile";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientMobile;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //, @sEmail,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sEmail";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientEmail;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sFAX,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFAX";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientFax;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sOccupation, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sOccupation";
                    oDBParameter.Value = oPatient.OccupationDetail.Occupation;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sEmploymentStatus, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sEmploymentStatus";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientEmploymentStatus;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sPlaceofEmployment, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sPlaceofEmployment";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientPlaceofEmployment;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //Code added on 14th May 2008 by Sagar Ghodke

                    //Occupation Details - @sEmployerName
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sEmployerName";
                    oDBParameter.Value = oPatient.OccupationDetail.EmployerName;
                    oDBParameter.DataType = SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //Occupation Details - @dtRetirementDate
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@dtRetirementDate";
                    oDBParameter.Value = oPatient.OccupationDetail.RetirementDate;
                    oDBParameter.DataType = SqlDbType.DateTime;
                    oDBParameter.ParameterDirection = ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);


                    //

                    ////Added by Anil on 20090713
                    //Patient Signature
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@iSignature";
                    oDBParameter.Value = null;
                    oDBParameter.DataType = System.Data.SqlDbType.Image;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //Emergency RelatioshipCode
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sEmergencyRelationshipCode";
                    oDBParameter.Value = oPatient.DemographicsDetail.EmergencyRelationshipCode;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //Emergency Relatioship description
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sEmergencyRelationshipDesc";
                    oDBParameter.Value = oPatient.DemographicsDetail.EmergencyRelationshipDesc;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    ///////

                    //@sWorkAddressLine1,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sWorkAddressLine1";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientWorkAddress1;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sWorkAddressLine2, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sWorkAddressLine2";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientWorkAddress2;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sWorkCity,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sWorkCity";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientWorkCity;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sWorkState, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sWorkState";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientWorkState;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sWorkZIP, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sWorkZIP";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientWorkZip;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sWorkPhone,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sWorkPhone";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientWorkPhone;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sWorkFAX, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sWorkFAX";
                    oDBParameter.Value = oPatient.OccupationDetail.PatientWorkFax;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sChiefComplaints,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sChiefComplaints";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @nProviderID, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@nProviderID";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientProviderID;
                    oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@nPCPId,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@nPCPId";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientPCPId;
                    oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sGuarantor,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuarantor";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientGuarantor;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @nPharmacyID,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@nPharmacyID";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientPharmacyID;
                    oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sSpouseName, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sSpouseName";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sSpousePhone,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sSpousePhone";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sRace, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sRace";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientRace;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sLang, paratemeter added to patient table to store PatientLanguage
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sLang";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientLanguage;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sEthn, // @sEthn, paratemeter added to patient table to store PatientEthnicities
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sEthn";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientEthnicities;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sEthn, // @sEthn, paratemeter added to patient table to store PatientEthnicities
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sPrefix";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientPrefix;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sPatientStatus,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sPatientStatus";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //Start :: ''YES/No Labs
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@bIsYesLab";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);
                    //End :: ''YES/No Labs


                    //// @iPhoto,
                    //if (oPatient.DemographicsDetail.PatientPhoto != null)
                    //{
                    //    oDBParameter = new gloDatabaseLayer.DBParameter();
                    //    oDBParameter.ParameterName = "@iPhoto";
                    //    //string fname = "c:\\iphoto.jpg";
                    //    //oPatient.PatientPhoto.Save(fname);
                    //    //Image iname ;
                    //    //iname = Bitmap.FromFile(fname);
                    //    //SLR: Commented to have myPictureBoxControl as arrbyteimage
                    //    //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    //    ////  iname.Save(ms, iname.RawFormat);

                    //    //oPatient.DemographicsDetail.PatientPhoto.Save(ms, oPatient.DemographicsDetail.PatientPhoto.RawFormat);
                    //    //Byte[] arrByteImage = ms.ToArray();
                    //    //oDBParameter.Value = arrByteImage;
                    //    oDBParameter.Value = oPatient.DemographicsDetail.MyPictureBoxControl;
                    //    oDBParameter.DataType = System.Data.SqlDbType.Image;
                    //    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    //    oDBParameters.Add(oDBParameter);
                    //    //ms.Close();
                    //    //ms.Dispose();
                    //}
                    /*Image to Ssl type
                        Syste.Drawing.Image= ilogo
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        ilogo.Save(ms, ilogo.RawFormat);
                        Byte[] arrImage = ms.GetBuffer();
                        ms.Close();
                    */



                    // @dtRegistrationDate,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@dtRegistrationDate";
                    oDBParameter.Value = DateTime.Now;
                    oDBParameter.DataType = System.Data.SqlDbType.DateTime;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@dtInjuryDate, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@dtInjuryDate";
                    oDBParameter.Value = DateTime.Now;
                    oDBParameter.DataType = System.Data.SqlDbType.DateTime;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@dtSurgeryDate, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@dtSurgeryDate";
                    oDBParameter.Value = DateTime.Now;
                    oDBParameter.DataType = System.Data.SqlDbType.DateTime;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sHandDominance,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sHandDominance";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientHandDominance;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sLocation,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sLocation";
                    oDBParameter.Value = oPatient.DemographicsDetail.PatientLocation;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_fName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_fName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherFirstName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_mName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_mName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMiddleName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_lName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_lName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherLastName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_maidenfName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_maidenfName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMaidenFirstName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_maidenmName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_maidenmName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMaidenMiddleName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_maidenlName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_maidenlName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMaidenLastName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_Address1, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_Address1";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherAddress1;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sMother_Address2, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_Address2";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherAddress2;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sMother_City,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_City";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherCity;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_State,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_State";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherState;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_ZIP,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_ZIP";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherZip;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_County, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_County";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherCountry;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sMother_Phone,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_Phone";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherPhone;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_Mobile, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_Mobile";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherMobile;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sMother_FAX,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_FAX";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherFAX;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMother_Email,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMother_Email";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientMotherEmail;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sFather_fName, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_fName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherFirstName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sFather_mName, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_mName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherMiddleName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sFather_lName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_lName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherLastName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sFather_Address1, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_Address1";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherAddress1;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sFather_Address2,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_Address2";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherAddress2;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sFather_City,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_City";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherCity;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sFather_State,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_State";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherState;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sFather_ZIP,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_ZIP";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherZip;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sFather_County, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_County";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherCountry;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sFather_Phone, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_Phone";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherPhone;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sFather_Mobile,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_Mobile";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherMobile;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sFather_FAX,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_FAX";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherFAX;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sFather_Email, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sFather_Email";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientFatherEmail;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sGuardian_fName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_fName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianFirstName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sGuardian_mName, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_mName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianMiddleName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sGuardian_lName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_lName";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianLastName;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sGuardian_Address1, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_Address1";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianAddress1;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sGuardian_Address2,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_Address2";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianAddress2;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sGuardian_City,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_City";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianCity;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sGuardian_State,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_State";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianState;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sGuardian_ZIP,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_ZIP";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianZip;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sGuardian_County,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_County";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianCountry;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sGuardian_Phone, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_Phone";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianPhone;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sGuardian_Mobile, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_Mobile";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianMobile;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sGuardian_FAX, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_FAX";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianFAX;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sGuardian_Email, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sGuardian_Email";
                    oDBParameter.Value = oPatient.GuardianDetail.PatientGuardianEmail;
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@nPatientDirective,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@nPatientDirective";
                    oDBParameter.Value = oPatient.DemographicsDetail.Directive;
                    oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@nExemptFromReport,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@nExemptFromReport";
                    oDBParameter.Value = oPatient.DemographicsDetail.ExemptFromReport;
                    oDBParameter.DataType = System.Data.SqlDbType.BigInt;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sExternalCode,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sExternalCode";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sUserName,
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sUserName";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    // @sMachineName, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sMachineName";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@sInsuranceNotes, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@sInsuranceNotes";
                    oDBParameter.Value = "";
                    oDBParameter.DataType = System.Data.SqlDbType.VarChar;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@IsNOTRegDate
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@IsNOTRegDate";
                    oDBParameter.Value = "true";
                    oDBParameter.DataType = System.Data.SqlDbType.Bit;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@@IsNOTInjuryDate
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@IsNOTInjuryDate";
                    oDBParameter.Value = "true";
                    oDBParameter.DataType = System.Data.SqlDbType.Bit;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@IsNOTSurgeryDate
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@IsNOTSurgeryDate";
                    oDBParameter.Value = "true";
                    oDBParameter.DataType = System.Data.SqlDbType.Bit;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);


                    //@bitIsGuarantor, 
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@bitIsGuarantor";
                    oDBParameter.Value = false;
                    oDBParameter.DataType = System.Data.SqlDbType.Bit;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);

                    //@bitIsBlock
                    oDBParameter = new gloDatabaseLayer.DBParameter();
                    oDBParameter.ParameterName = "@bitIsBlock";
                    oDBParameter.Value = false;
                    oDBParameter.DataType = System.Data.SqlDbType.Bit;
                    oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input;
                    oDBParameters.Add(oDBParameter);




                    Object patientID;
                    oDB.Execute("PAT_INUP_Patient", oDBParameters, out  patientID);
                    if (patientID != null)
                    {
                        oPatient.PatientID = Convert.ToInt64(patientID);
                        
                        if (oPatient.DemographicsDetail.PatientPhoto != null)
                        {
                            PatientPhoto oPatientPhoto = null;
                            try
                            {
                                oPatientPhoto = new PatientPhoto(_databaseconnectionstring);
                                oPatientPhoto.InsertPhoto(oPatient.PatientID, oPatient.DemographicsDetail.MyPictureBoxControl);
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                            }
                            finally
                            {
                                if (oPatientPhoto != null)
                                {
                                    oPatientPhoto.Dispose();
                                    oPatientPhoto = null;
                                }
                            }
                        }
                    }
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    throw new gloDatabaseLayer.DBException("Error in database execution", ex.ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                    oDBParameters.Dispose();
                }

                return 1;
            }
        }

        public Patient GetPatientDemo(Int64 patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Patient oPatient = new Patient();
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for Quick Review Panel.
                oParameters.Add("@PatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_SELECT_PatientDEMO", oParameters, out dt);

                //if (dt != null)
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        oPatient.DemographicsDetail.PatientCode = dt.Rows[0]["sPatientCode"].ToString();
                        oPatient.DemographicsDetail.PatientFirstName = dt.Rows[0]["sFirstName"].ToString();
                        oPatient.DemographicsDetail.PatientMiddleName = dt.Rows[0]["sMiddleName"].ToString();
                        oPatient.DemographicsDetail.PatientLastName = dt.Rows[0]["sLastName"].ToString();
                        if (dt.Rows[0]["nSSN"] != null && Convert.ToString(dt.Rows[0]["nSSN"]) != "")
                        {
                            oPatient.DemographicsDetail.PatientSSN = dt.Rows[0]["nSSN"].ToString();
                        }
                        if (dt.Rows[0]["dtDOB"] != null && Convert.ToString(dt.Rows[0]["dtDOB"]) != "")
                        {
                            oPatient.DemographicsDetail.PatientDOB = Convert.ToDateTime(dt.Rows[0]["dtDOB"]);
                        }
                        oPatient.DemographicsDetail.PatientMaritalStatus = dt.Rows[0]["sMaritalStatus"].ToString();
                        oPatient.DemographicsDetail.PatientGender = dt.Rows[0]["sGender"].ToString();
                        oPatient.DemographicsDetail.PatientAddress1 = dt.Rows[0]["sAddressLine1"].ToString();
                        oPatient.DemographicsDetail.PatientAddress2 = dt.Rows[0]["sAddressLine2"].ToString();
                        oPatient.DemographicsDetail.PatientCity = dt.Rows[0]["sCity"].ToString();
                        oPatient.DemographicsDetail.PatientState = dt.Rows[0]["sState"].ToString();
                        oPatient.DemographicsDetail.PatientZip = dt.Rows[0]["sZIP"].ToString();
                        oPatient.DemographicsDetail.PatientCounty = dt.Rows[0]["sCounty"].ToString();
                        oPatient.DemographicsDetail.PatientCountry = dt.Rows[0]["sCountry"].ToString();

                        oPatient.DemographicsDetail.PatientPhone = dt.Rows[0]["sPhone"].ToString();
                        oPatient.DemographicsDetail.PatientMobile = dt.Rows[0]["sMobile"].ToString();
                        oPatient.DemographicsDetail.PatientEmail = dt.Rows[0]["sEmail"].ToString();
                        oPatient.DemographicsDetail.PatientFax = dt.Rows[0]["sFAX"].ToString();

                        oPatient.DemographicsDetail.BirthTime = Convert.ToString(dt.Rows[0]["sBirthTime"]); //dtpBirthTime Settings

                        oPatient.DemographicsDetail.PharmacyName = "";
                        oPatient.DemographicsDetail.PrimaryCarePhysicianName = "";
                        oPatient.DemographicsDetail.PatientPCPId = 0;
                        oPatient.DemographicsDetail.PatientPharmacyID = 0;
                        oPatient.DemographicsDetail.PatientCounty = "";
                        oPatient.DemographicsDetail.PatientGuarantor = "";
                        oPatient.DemographicsDetail.PatientGuarantorID = 0;
                        oPatient.DemographicsDetail.PatientHandDominance = "";
                        oPatient.DemographicsDetail.PatientLocation = "";
                        oPatient.DemographicsDetail.PatientOccupation = "";
                        oPatient.DemographicsDetail.PatientRace = "";

                        oPatient.DemographicsDetail.ProvideName = "";
                        oPatient.DemographicsDetail.PatientEthnicities = dt.Rows[0]["sEthn"].ToString();
                        oPatient.DemographicsDetail.PatientLanguage = dt.Rows[0]["sLang"].ToString();
                        oPatient.DemographicsDetail.PatientPrefix = dt.Rows[0]["sPrefix"].ToString();
                        // EMERGENCY CONTACT 
                        oPatient.DemographicsDetail.EmergencyContact = dt.Rows[0]["sEmergencyContact"].ToString();
                        oPatient.DemographicsDetail.EmergencyPhone = dt.Rows[0]["sEmergencyPhone"].ToString();
                        oPatient.DemographicsDetail.EmergencyMobile = dt.Rows[0]["sEmergencyMobile"].ToString();
                        //Added by Anil 20090713
                        oPatient.DemographicsDetail.EmergencyRelationshipCode = dt.Rows[0]["sEmergencyRelationshipCode"].ToString();
                        oPatient.DemographicsDetail.EmergencyRelationshipDesc = dt.Rows[0]["sEmergencyRelationshipDesc"].ToString();
                        //
                        // Get Patient Provider Name
                        oPatient.DemographicsDetail.PatientProviderID = Convert.ToInt64(dt.Rows[0]["nProviderID"]);
                        oPatient.DemographicsDetail.ProvideName = dt.Rows[0]["sProviderName"].ToString();


                        // Get Patient PharmacyName
                        DataTable dtContact = null;
                        String strQuery = "SELECT ISNULL(sName,'') AS PharmacyName FROM Patient_DTL "
                        + " WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + " AND nContactFlag = " + PatientContactType.Pharmacy.GetHashCode() + "";

                        oDB.Retrive_Query(strQuery, out dtContact);
                        if (dtContact != null && dtContact.Rows.Count > 0)
                        {
                            oPatient.DemographicsDetail.PharmacyName = dtContact.Rows[0]["PharmacyName"].ToString();
                        }
                        if (dtContact != null)
                        {
                            dtContact.Dispose();
                            dtContact = null;
                        }

                        // Get Patient PrimaryCarePhysicianName
                        dtContact = null;
                        strQuery = "SELECT ISNULL(sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS sPCPName  FROM Patient_DTL "
                        + " WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + " AND nContactFlag = " + PatientContactType.PrimaryCarePhysician.GetHashCode() + "";

                        oDB.Retrive_Query(strQuery, out dtContact);
                        if (dtContact != null && dtContact.Rows.Count > 0)
                        {
                            oPatient.DemographicsDetail.PrimaryCarePhysicianName = dtContact.Rows[0]["sPCPName"].ToString();
                        }
                        if (dtContact != null)
                        {
                            dtContact.Dispose();
                            dtContact = null;
                        }
                        //Start/ Commented for the new one
                        //if ((dt.rows[0]["iphoto"] != dbnull.value))
                        //{
                        //    system.drawing.image ilogo;
                        //    byte[] arrpicture = (byte[])dt.rows[0]["iphoto"];
                        //    system.io.memorystream ms = new system.io.memorystream(arrpicture);
                        //    ilogo = image.fromstream(ms);
                        //    opatient.demographicsdetail.patientphoto = ilogo;
                        //}
                        //End/ Commented for the new one

                        //Start/New Code
                        //Start'GLO2010-0007047[BJMC]: Webcam image too small
                        if ((dt.Rows[0]["iPhoto"] != DBNull.Value))
                        {
                            //System.Drawing.Image ilogo;
                            oPatient.DemographicsDetail.MyPictureBoxControl = (Byte[])dt.Rows[0]["iPhoto"];
                            //SLR: following not needed as it is part of set mypictureboxcontrol
                            //gloPictureBox.gloPictureBox myPixBx = new gloPictureBox.gloPictureBox();
                            //myPixBx.byteImage = oPatient.DemographicsDetail.MyPictureBoxControl;
                            //oPatient.DemographicsDetail.PatientPhoto = myPixBx.copyFrame(true);
                            //myPixBx.Dispose();
                            //myPixBx = null;


                        }
                        else
                        {
                            //SLR: following not needed as it is part of set mypictureboxcontrol
                            // oPatient.DemographicsDetail.PatientPhoto = null;
                            oPatient.DemographicsDetail.MyPictureBoxControl = null;

                        }
                        //End'GLO2010-0007047[BJMC]: Webcam image too small
                        //End/New Code


                        oPatient.DemographicsDetail.PatientPCPId = 0;
                        oPatient.DemographicsDetail.PatientPharmacyID = 0;
                        oPatient.DemographicsDetail.PharmacyName = "";
                        oPatient.DemographicsDetail.PrimaryCarePhysicianName = "";

                        //Retrieve referral Information
                        DataTable dtDetails = null;
                        string strSQlDetails = "";

                        //strSQlReff = "Select (ISNULL(Patient_DTL.sFirstName,'')+ ' ' +ISNULL(Patient_DTL.sMiddleName,'')+ ' '+ISNULL(Patient_DTL.sLastName,'')) AS sName , Contacts_MST.nContactID as nContactID from Contacts_MST  inner join Patient_DTL on Contacts_MST.nContactID = Patient_DTL.nContactId where Patient_DTL.npatientid='" + patientid + "'";
                        //COMMENTED BY SHUBHANGI 20100910
                        //strSQlDetails = "SELECT ISNULL(nContactId,0) AS nContactId,ISNULL(nPatientDetailID,0) AS nPatientDetailID, ISNULL(sName,'') AS sName, ISNULL(sContact,'') AS  sContact,ISNULL(sAddressLine1,'') AS  sAddressLine1, ISNULL(sAddressLine2,'') AS sAddressLine2 , "
                        //+ " ISNULL(sCity,'') AS  sCity, ISNULL(sState,'') AS sState , ISNULL(sZIP,'') AS sZIP , ISNULL(sPhone,'') AS sPhone , ISNULL(sFax,'') AS  sFax, ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                        //+ " ISNULL(sEmail,'') AS  sEmail, ISNULL(sURL,'') AS  sURL, ISNULL(sMobile,'') AS  sMobile, ISNULL(sPager,'') AS  sPager, "
                        //+ " ISNULL(sNotes,'') AS  sNotes, ISNULL(sFirstName,'') AS sFirstName , ISNULL(sMiddleName,'') AS sMiddleName , ISNULL(sLastName,'') AS sLastName , "
                        //+ " ISNULL(sGender,'') AS  sGender, ISNULL(sTaxonomy,'') AS  sTaxonomy, ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc , ISNULL(sTaxID,'') AS  sTaxID, "
                        //+ " ISNULL(sUPIN,'') AS  sUPIN, ISNULL(sNPI,'') AS  sNPI, ISNULL(sHospitalAffiliation,'') AS sHospitalAffiliation , ISNULL(sExternalCode,'') AS sExternalCode , "
                        //+ " ISNULL(sDegree,'') AS  sDegree, ISNULL(nContactFlag,0) AS  nContactFlag, "
                        //+ " ISNULL(sNCPDPID,'') AS sNCPDPID,ISNULL(sPharmacyStatus,'') AS sPharmacyStatus,ISNULL(sServiceLevel,'') AS sServiceLevel,ActiveStartTime,ActiveEndTime"
                        //+ " FROM Patient_DTL WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + "";


                        //ADDED BY SHUBHANGI TO ADD PREFIX FIELD
                        strSQlDetails = "SELECT ISNULL(nContactId,0) AS nContactId,ISNULL(nPatientDetailID,0) AS nPatientDetailID, ISNULL(sName,'') AS sName, ISNULL(sContact,'') AS  sContact,ISNULL(sAddressLine1,'') AS  sAddressLine1, ISNULL(sAddressLine2,'') AS sAddressLine2 , "
                       + " ISNULL(sCity,'') AS  sCity, ISNULL(sState,'') AS sState , ISNULL(sZIP,'') AS sZIP , dbo.FormatPhone(ISNULL(sPhone,''),0) AS sPhone , ISNULL(sFax,'') AS  sFax, ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                       + " ISNULL(sEmail,'') AS  sEmail, ISNULL(sURL,'') AS  sURL, dbo.FormatPhone(ISNULL(sMobile, ''),0) AS  sMobile, ISNULL(sPager,'') AS  sPager, "
                       + " ISNULL(sNotes,'') AS  sNotes, ISNULL(sPrefix,'') AS sPrefix , ISNULL(sFirstName,'') AS sFirstName , ISNULL(sMiddleName,'') AS sMiddleName , ISNULL(sLastName,'') AS sLastName , "
                       + " ISNULL(sGender,'') AS  sGender, ISNULL(sTaxonomy,'') AS  sTaxonomy, ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc , ISNULL(sTaxID,'') AS  sTaxID, "
                       + " ISNULL(sUPIN,'') AS  sUPIN, ISNULL(sNPI,'') AS  sNPI, ISNULL(sHospitalAffiliation,'') AS sHospitalAffiliation , ISNULL(sExternalCode,'') AS sExternalCode , "
                       + " ISNULL(sDegree,'') AS  sDegree, ISNULL(nContactFlag,0) AS  nContactFlag, "
                       + " ISNULL(sNCPDPID,'') AS sNCPDPID,ISNULL(sPharmacyStatus,'') AS sPharmacyStatus,ISNULL(nContactStatus,0) AS  nContactStatus,ISNULL(sServiceLevel,'') AS sServiceLevel,ActiveStartTime,ActiveEndTime"
                       + " FROM Patient_DTL WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + " ";

                        oDB.Retrive_Query(strSQlDetails, out dtDetails);

                        if (dtDetails != null && dtDetails.Rows.Count > 0)
                        {
                            PatientDetail oPatientDetail = null;
                            for (int i = 0; i < dtDetails.Rows.Count; i++)
                            {
                                oPatientDetail = new PatientDetail();

                                oPatientDetail.PatientID = patientid;
                                oPatientDetail.PatientDetailID = Convert.ToInt64(dtDetails.Rows[i]["nPatientDetailID"]);
                                oPatientDetail.ContactId = Convert.ToInt64(dtDetails.Rows[i]["nContactId"]);
                                oPatientDetail.Name = Convert.ToString(dtDetails.Rows[i]["sName"]);
                                oPatientDetail.Contact = Convert.ToString(dtDetails.Rows[i]["sContact"]);
                                oPatientDetail.AddressLine1 = Convert.ToString(dtDetails.Rows[i]["sAddressLine1"]);
                                oPatientDetail.AddressLine2 = Convert.ToString(dtDetails.Rows[i]["sAddressLine2"]);
                                oPatientDetail.City = Convert.ToString(dtDetails.Rows[i]["sCity"]);
                                oPatientDetail.State = Convert.ToString(dtDetails.Rows[i]["sState"]);
                                oPatientDetail.ZIP = Convert.ToString(dtDetails.Rows[i]["sZIP"]);

                                oPatientDetail.County = Convert.ToString(dtDetails.Rows[i]["sCounty"]);
                                oPatientDetail.Country = Convert.ToString(dtDetails.Rows[i]["sCountry"]);

                                oPatientDetail.Phone = Convert.ToString(dtDetails.Rows[i]["sPhone"]);

                                oPatientDetail.Fax = Convert.ToString(dtDetails.Rows[i]["sFax"]);
                                oPatientDetail.Email = Convert.ToString(dtDetails.Rows[i]["sEmail"]);
                                oPatientDetail.URL = Convert.ToString(dtDetails.Rows[i]["sURL"]);
                                oPatientDetail.Mobile = Convert.ToString(dtDetails.Rows[i]["sMobile"]);
                                oPatientDetail.Pager = Convert.ToString(dtDetails.Rows[i]["sPager"]);
                                oPatientDetail.Notes = Convert.ToString(dtDetails.Rows[i]["sNotes"]);
                                oPatientDetail.Prefix = Convert.ToString(dtDetails.Rows[i]["sPrefix"]);
                                oPatientDetail.FirstName = Convert.ToString(dtDetails.Rows[i]["sFirstName"]);
                                oPatientDetail.MiddleName = Convert.ToString(dtDetails.Rows[i]["sMiddleName"]);
                                oPatientDetail.LastName = Convert.ToString(dtDetails.Rows[i]["sLastName"]);
                                oPatientDetail.Gender = Convert.ToString(dtDetails.Rows[i]["sGender"]);
                                oPatientDetail.Taxonomy = Convert.ToString(dtDetails.Rows[i]["sTaxonomy"]);
                                oPatientDetail.TaxonomyDesc = Convert.ToString(dtDetails.Rows[i]["sTaxonomyDesc"]);
                                oPatientDetail.TaxID = Convert.ToString(dtDetails.Rows[i]["sTaxID"]);
                                oPatientDetail.UPIN = Convert.ToString(dtDetails.Rows[i]["sUPIN"]);
                                oPatientDetail.NPI = Convert.ToString(dtDetails.Rows[i]["sNPI"]);
                                oPatientDetail.HospitalAffiliation = Convert.ToString(dtDetails.Rows[i]["sHospitalAffiliation"]);
                                oPatientDetail.ExternalCode = Convert.ToString(dtDetails.Rows[i]["sExternalCode"]);
                                oPatientDetail.Degree = Convert.ToString(dtDetails.Rows[i]["sDegree"]);

                                oPatientDetail.NCPDPID = Convert.ToString(dtDetails.Rows[i]["sNCPDPID"]);
                                oPatientDetail.ServiceLevel = Convert.ToString(dtDetails.Rows[i]["sServiceLevel"]);
                                oPatientDetail.PharmacyStatus = Convert.ToString(dtDetails.Rows[i]["sPharmacyStatus"]);
                                oPatientDetail.ContactStatus = Convert.ToInt64(dtDetails.Rows[i]["nContactStatus"]);

                                if (dtDetails.Rows[i]["ActiveStartTime"] != DBNull.Value)
                                    oPatientDetail.ActiveStartTime = Convert.ToDateTime(dtDetails.Rows[i]["ActiveStartTime"]);
                                if (dtDetails.Rows[i]["ActiveEndTime"] != DBNull.Value)
                                    oPatientDetail.ActiveEndTime = Convert.ToDateTime(dtDetails.Rows[i]["ActiveEndTime"]);


                                oPatientDetail.ContactFlag = (PatientContactType)Convert.ToInt32(dtDetails.Rows[i]["nContactFlag"]);
                                oPatientDetail.ClinicID = _ClinicID;

                                switch (oPatientDetail.ContactFlag)
                                {
                                    case PatientContactType.Pharmacy:
                                        oPatient.PatientPharmacies.Add(oPatientDetail);
                                        if (oPatientDetail.ContactStatus == 1)
                                            oPatient.DemographicsDetail.PharmacyName = oPatientDetail.Name;
                                        break;
                                    case PatientContactType.PrimaryCarePhysician:
                                        oPatient.PrimaryCarePhysicians.Add(oPatientDetail);
                                        // oPatient.DemographicsDetail.PrimaryCarePhysicianName = oPatientDetail.FirstName + " " + oPatientDetail.MiddleName + " " + oPatientDetail.LastName;
                                        //ADDED BY SHUBHANGI TO CHECK & IF PRESENT THEN DISPLAY THE PREFIX & SUFFIX FIELDS
                                        if (oPatientDetail.Prefix != "" && oPatientDetail.Degree != "")
                                        {
                                            oPatient.DemographicsDetail.PrimaryCarePhysicianName = oPatientDetail.Prefix + " " + oPatientDetail.FirstName + " " + oPatientDetail.MiddleName + " " + oPatientDetail.LastName + " " + oPatientDetail.Degree;
                                        }
                                        else if (oPatientDetail.Prefix == "" && oPatientDetail.Degree != "")
                                        {
                                            oPatient.DemographicsDetail.PrimaryCarePhysicianName = oPatientDetail.FirstName + " " + oPatientDetail.MiddleName + " " + oPatientDetail.LastName + " " + oPatientDetail.Degree;
                                        }
                                        else if (oPatientDetail.Prefix != "" && oPatientDetail.Degree == "")
                                        {
                                            oPatient.DemographicsDetail.PrimaryCarePhysicianName = oPatientDetail.Prefix + " " + oPatientDetail.FirstName + " " + oPatientDetail.MiddleName + " " + oPatientDetail.LastName;
                                        }
                                        else
                                        {
                                            oPatient.DemographicsDetail.PrimaryCarePhysicianName = oPatientDetail.FirstName + " " + oPatientDetail.MiddleName + " " + oPatientDetail.LastName;
                                        }
                                        break;
                                    case PatientContactType.Referral:
                                        oPatient.PatientReferrals.Add(oPatientDetail);
                                        break;
                                    case PatientContactType.CareTeam:
                                        oPatient.PatientCareTeam.Add(oPatientDetail);
                                        break;
                                    default:
                                        break;
                                }
                                oPatientDetail.Dispose();
                                oPatientDetail = null;
                            }
                        }
                        if (dtDetails != null)
                        {
                            dtDetails.Dispose();
                            dtDetails = null;
                        }
                        //Added by Mukesh 20090829
                        #region "Patient Settings"

                        if (IsTableExists("PatientSettings"))
                        {
                            DataTable dtPatientSettings = null;
                            strQuery = "select isNull(sName,'') as sName, isnull(sValue,'') as sValue FROM PatientSettings WHERE sName in ('Exclude from Statement','Signature on File Date','CMS1500 Box13') AND nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + "";

                            oDB.Retrive_Query(strQuery, out dtPatientSettings);
                            if (dtPatientSettings != null)
                            {
                                foreach (DataRow dr in dtPatientSettings.Rows)
                                {
                                    switch (dr["sName"].ToString())
                                    {
                                        case "Exclude from Statement":
                                            if (Convert.ToString(dr["sValue"]) == "1")
                                            {
                                                oPatient.DemographicsDetail.ExcludeFromStatement = true;
                                            }
                                            else
                                            {
                                                oPatient.DemographicsDetail.ExcludeFromStatement = false;
                                            }
                                            break;
                                        case "Signature on File Date":
                                            oPatient.PatientDemographicOtherInfo.SOFDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dr["sValue"]));
                                            break;
                                        case "CMS1500 Box13":
                                            oPatient.PatientDemographicOtherInfo.CMS1500Box13 = Convert.ToString(dr["sValue"]);
                                            break;
                                    }
                                }
                                dtPatientSettings.Dispose();
                            }
                            dtPatientSettings = null;
                        }

                        #endregion


                    }
                }
                return oPatient;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                if (dt != null)
                {
                    dt.Dispose();
                }
                //oPatient.Dispose();
            }

        }
        //
        public DataTable GetPatientDemographics(Int64 patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for dashboard.
                oParameters.Add("@PatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_SELECT_PatientDEMO_Dashboard", oParameters, out dt);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); }
                if (oDB != null) { oDB.Dispose(); }
                oParameters.Dispose();
            }
            return dt;

        }
        //
        public bool IsWorkerComp(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _Query = "";
            object Result = null;
            _Query = "SELECT ISNULL(bIsWorkerComp,0) AS bIsWorkerComp FROM dbo.Contacts_Insurance_DTL WHERE nContactID=" + ContactID;
            try
            {
                oDB.Connect(false);
                Result = oDB.ExecuteScalar_Query(_Query);
                if (Result.ToString() != "" && Result != null)
                    return Convert.ToBoolean(Result);
                else
                    return false;

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
        public Patient GetPatient(Int64 patientid)
        {
            //Function to Get the patient information from Database

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();

            Patient opatient = new Patient();
            DataTable dt;

            try
            {
                oDB.Connect(false);
                odbparam.Add("@PatienID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                odbparam.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Select_Patient", odbparam, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //nPatientID, sPatientCode, sFirstName, sMiddleName, sLastName, nSSN, dtDOB, sGender, sMaritalStatus, 
                        opatient.DemographicsDetail.PatientCode = dt.Rows[0]["sPatientCode"].ToString(); // Resourse Description
                        opatient.DemographicsDetail.PatientFirstName = dt.Rows[0]["sFirstName"].ToString(); // ResourceType
                        opatient.DemographicsDetail.PatientMiddleName = dt.Rows[0]["sMiddleName"].ToString();
                        opatient.DemographicsDetail.PatientLastName = dt.Rows[0]["sLastName"].ToString();

                        opatient.DemographicsDetail.PatientSuffix = dt.Rows[0]["sSuffix"].ToString();

                        // Change Done while Changing the DataType of nSSN from Numeric to Varchar
                        opatient.DemographicsDetail.PatientSSN = Convert.ToString(dt.Rows[0]["nSSN"]);
                        //
                        //opatient.DemographicsDetail.PatientSSN = Convert.ToInt64(dt.Rows[0]["nSSN"]);
                        if (dt.Rows[0]["dtDOB"].ToString() != "" && dt.Rows[0]["dtDOB"] != null)
                        { opatient.DemographicsDetail.PatientDOB = Convert.ToDateTime(dt.Rows[0]["dtDOB"]); }
                        opatient.DemographicsDetail.PatientGender = dt.Rows[0]["sGender"].ToString();
                        opatient.DemographicsDetail.PatientMaritalStatus = dt.Rows[0]["sMaritalStatus"].ToString();
                        opatient.DemographicsDetail.BirthTime = Convert.ToString(dt.Rows[0]["sBirthTime"]); //dtpBirthTime Settings

                        //sAddressLine1, sAddressLine2, sCity, sState, sZIP, sCounty, sPhone, sMobile, sEmail, 
                        opatient.DemographicsDetail.PatientAddress1 = dt.Rows[0]["sAddressLine1"].ToString();
                        opatient.DemographicsDetail.PatientAddress2 = dt.Rows[0]["sAddressLine2"].ToString();
                        opatient.DemographicsDetail.PatientCity = dt.Rows[0]["sCity"].ToString();
                        opatient.DemographicsDetail.PatientState = dt.Rows[0]["sState"].ToString();
                        opatient.DemographicsDetail.PatientZip = dt.Rows[0]["sZIP"].ToString();
                        opatient.DemographicsDetail.PatientCounty = dt.Rows[0]["sCounty"].ToString();
                        opatient.DemographicsDetail.PatientCountry = dt.Rows[0]["sCountry"].ToString();
                        opatient.DemographicsDetail.PatientPhone = dt.Rows[0]["sPhone"].ToString();
                        opatient.DemographicsDetail.PatientMobile = dt.Rows[0]["sMobile"].ToString();
                        opatient.DemographicsDetail.PatientEmail = dt.Rows[0]["sEmail"].ToString();
                        opatient.DemographicsDetail.PatientFax = dt.Rows[0]["sFAX"].ToString();
                        opatient.DemographicsDetail.InsuranceNotes = dt.Rows[0]["sInsuranceNotes"].ToString();

                        //Start :: YES/No Labs
                        if (dt.Rows[0]["bIsYesLab"].ToString().ToLower() == "true")
                        {
                            opatient.DemographicsDetail.IsYesNoLab = true;
                        }
                        else
                        {
                            opatient.DemographicsDetail.IsYesNoLab = false;
                        }
                        //End :: YES/No Labs

                        //7022Items: Home Billing
                        //Get Area Code from database and assign to AreaCode property of opatient.DemographicsDetail.
                        opatient.DemographicsDetail.AreaCode = dt.Rows[0]["sAreaCode"].ToString();

                        //Emergency contact
                        opatient.DemographicsDetail.EmergencyContact = dt.Rows[0]["sEmergencyContact"].ToString();
                        opatient.DemographicsDetail.EmergencyPhone = dt.Rows[0]["sEmergencyPhone"].ToString();
                        opatient.DemographicsDetail.EmergencyMobile = dt.Rows[0]["sEmergencyMobile"].ToString();
                        //Added by Anil 20090713
                        opatient.DemographicsDetail.EmergencyRelationshipCode = dt.Rows[0]["sEmergencyRelationshipCode"].ToString();
                        opatient.DemographicsDetail.EmergencyRelationshipDesc = dt.Rows[0]["sEmergencyRelationshipDesc"].ToString();
                        //
                        //sOccupation, sEmploymentStatus, sPlaceofEmployment, sWorkAddressLine1, 
                        opatient.OccupationDetail.Occupation = dt.Rows[0]["sOccupation"].ToString();
                        opatient.OccupationDetail.PatientEmploymentStatus = dt.Rows[0]["sEmploymentStatus"].ToString();
                        opatient.OccupationDetail.PatientPlaceofEmployment = dt.Rows[0]["sPlaceofEmployment"].ToString();
                        opatient.OccupationDetail.PatientWorkAddress1 = dt.Rows[0]["sWorkAddressLine1"].ToString();

                        //sWorkAddressLine2, sWorkCity, sWorkState, sWorkZIP, sWorkPhone, sWorkFAX, 
                        opatient.OccupationDetail.PatientWorkAddress2 = dt.Rows[0]["sWorkAddressLine2"].ToString();
                        opatient.OccupationDetail.PatientWorkCity = dt.Rows[0]["sWorkCity"].ToString();
                        opatient.OccupationDetail.PatientWorkState = dt.Rows[0]["sWorkState"].ToString();
                        opatient.OccupationDetail.PatientWorkZip = dt.Rows[0]["sWorkZIP"].ToString();
                        opatient.OccupationDetail.PatientWorkPhone = dt.Rows[0]["sWorkPhone"].ToString();
                        opatient.OccupationDetail.PatientWorkFax = dt.Rows[0]["sWorkFAX"].ToString();


                        opatient.OccupationDetail.PatientWorkCounty = dt.Rows[0]["sWorkCounty"].ToString();
                        opatient.OccupationDetail.PatientWorkCountry = dt.Rows[0]["sWorkCountry"].ToString();
                        opatient.OccupationDetail.PatientWorkMobile = dt.Rows[0]["sWorkMobile"].ToString();
                        opatient.OccupationDetail.PatientWorkEmail = dt.Rows[0]["sWorkEmail"].ToString();

                        //Code added on 14 May 2008 
                        opatient.OccupationDetail.EmployerName = dt.Rows[0]["sEmployerName"].ToString();
                        if (dt.Rows[0]["dtRetirementDate"].ToString() != "" && dt.Rows[0]["dtRetirementDate"] != DBNull.Value)
                        {
                            opatient.OccupationDetail.RetirementDate = Convert.ToDateTime(dt.Rows[0]["dtRetirementDate"]);
                        }
                        //Comented Sandip Darade 20090406
                        //else
                        //{
                        //    opatient.OccupationDetail.RetirementDate = DateTime.Now.Date;
                        //}
                        opatient.OccupationDetail.EmploymentType = dt.Rows[0]["sEmploymentType"].ToString();
                        //

                        //Guarantor
                        opatient.DemographicsDetail.PatientGuarantorID = Convert.ToInt64(dt.Rows[0]["nGuarantorID"]);
                        opatient.DemographicsDetail.PatientGuarantor = dt.Rows[0]["sGuarantor"].ToString();


                        //sChiefComplaints, nProviderID, nPCPId, sGuarantor, nPharmacyID, sSpouseName, 
                        // opatient.PatientChiefComplaints = dt.Rows[0]["sChiefComplaints"].ToString();
                        opatient.DemographicsDetail.PatientProviderID = Convert.ToInt64(dt.Rows[0]["nProviderID"]);


                        opatient.DemographicsDetail.PatientGuarantor = GetPatientName(Convert.ToInt64(dt.Rows[0]["nGuarantorID"]));// dt.Rows[0]["sGuarantor"].ToString();

                        opatient.DemographicsDetail.PatientRace = dt.Rows[0]["sRace"].ToString();
                        opatient.DemographicsDetail.PatientLanguage = dt.Rows[0]["sLang"].ToString();
                        opatient.DemographicsDetail.PatientCommunicationPrefence = dt.Rows[0]["sCommunicationPreference"].ToString();
                        opatient.DemographicsDetail.PatientEthnicities = dt.Rows[0]["sEthn"].ToString();
                        opatient.PatientDemographicOtherInfo.Status = dt.Rows[0]["sPatientStatus"].ToString();
                        opatient.DemographicsDetail.PatientPrefix = dt.Rows[0]["sPrefix"].ToString();


                        //opatient.PatientPhoto = System.Drawing.Image(dt.Rows[0]["iPhoto"]);


                        //Start/ Commeted Code
                        //if ((dt.Rows[0]["iPhoto"] != DBNull.Value))
                        //{
                        //    System.Drawing.Image ilogo;
                        //    Byte[] arrPicture = (Byte[])dt.Rows[0]["iPhoto"];
                        //    System.IO.MemoryStream ms = new System.IO.MemoryStream(arrPicture);
                        //    ilogo = Image.FromStream(ms);
                        //    opatient.DemographicsDetail.PatientPhoto = ilogo;
                        //}
                        //End/ Commented Code

                        if ((dt.Rows[0]["iPhoto"] != DBNull.Value))
                        {
                            opatient.DemographicsDetail.MyPictureBoxControl = (Byte[])dt.Rows[0]["iPhoto"];
                            //SLR: following not needed as it is part of set mypictureboxcontrol
                            //gloPictureBox.gloPictureBox myPicBx = new gloPictureBox.gloPictureBox();
                            //myPicBx.byteImage = opatient.DemographicsDetail.MyPictureBoxControl;
                            //opatient.DemographicsDetail.PatientPhoto = myPicBx.copyFrame(true);
                            //myPicBx.Dispose();
                        }
                        else
                        {
                            //SLR: following not needed as it is part of set mypictureboxcontrol
                            //opatient.DemographicsDetail.PatientPhoto = null;
                            opatient.DemographicsDetail.MyPictureBoxControl = null;
                        }



                        //// Fields Moved to Other details
                        //opatient.InsuranceDetails.SpouseName = dt.Rows[0]["sSpouseName"].ToString();
                        //opatient.InsuranceDetails.SpousePhone = dt.Rows[0]["sSpousePhone"].ToString();
                        //opatient.InsuranceDetails.Status = dt.Rows[0]["sPatientStatus"].ToString();
                        //if (dt.Rows[0]["dtRegistrationDate"].ToString() != "" && dt.Rows[0]["dtRegistrationDate"] != null)
                        //opatient.InsuranceDetails.RegistrationDate = Convert.ToDateTime(dt.Rows[0]["dtRegistrationDate"]);
                        //if (dt.Rows[0]["dtInjuryDate"].ToString() != "" && dt.Rows[0]["dtInjuryDate"] != null)
                        //opatient.InsuranceDetails.InjuryDate = Convert.ToDateTime(dt.Rows[0]["dtInjuryDate"]);
                        //if (dt.Rows[0]["dtSurgeryDate"].ToString() != "" && dt.Rows[0]["dtSurgeryDate"] != null)
                        //opatient.InsuranceDetails.SurgeryDate = Convert.ToDateTime(dt.Rows[0]["dtSurgeryDate"]);
                        //opatient.InsuranceDetails.IsWorkersComp = Convert.ToBoolean(dt.Rows[0]["bIsWorkersComp"]);
                        //opatient.InsuranceDetails.WorkersCompClaimNo = Convert.ToString(dt.Rows[0]["sWorkersCompClaimNo"]);
                        //opatient.InsuranceDetails.IsAutoClaim = Convert.ToBoolean(dt.Rows[0]["bIsAutoClaim"]);
                        //opatient.InsuranceDetails.AutoClaimNo = Convert.ToString(dt.Rows[0]["sAutoClaimNo"]);
                        //opatient.InsuranceDetails.InsuranceNotes = dt.Rows[0]["sInsuranceNotes"].ToString();

                        opatient.DemographicsDetail.PatientHandDominance = dt.Rows[0]["sHandDominance"].ToString();
                        opatient.DemographicsDetail.PatientLocation = dt.Rows[0]["sLocation"].ToString();
                        opatient.GuardianDetail.PatientMotherFirstName = dt.Rows[0]["sMother_fName"].ToString();
                        opatient.GuardianDetail.PatientMotherMiddleName = dt.Rows[0]["sMother_mName"].ToString();
                        opatient.GuardianDetail.PatientMotherLastName = dt.Rows[0]["sMother_lName"].ToString();

                        opatient.GuardianDetail.PatientMotherMaidenFirstName = dt.Rows[0]["sMother_maidenfName"].ToString();
                        opatient.GuardianDetail.PatientMotherMaidenMiddleName = dt.Rows[0]["sMother_maidenmName"].ToString();
                        opatient.GuardianDetail.PatientMotherMaidenLastName = dt.Rows[0]["sMother_maidenlName"].ToString();


                        //sMother_Address1, sMother_Address2, sMother_City, sMother_State, sMother_ZIP, 
                        opatient.GuardianDetail.PatientMotherAddress1 = dt.Rows[0]["sMother_Address1"].ToString();
                        opatient.GuardianDetail.PatientMotherAddress2 = dt.Rows[0]["sMother_Address2"].ToString();
                        opatient.GuardianDetail.PatientMotherCity = dt.Rows[0]["sMother_City"].ToString();
                        opatient.GuardianDetail.PatientMotherState = dt.Rows[0]["sMother_State"].ToString();
                        opatient.GuardianDetail.PatientMotherZip = dt.Rows[0]["sMother_ZIP"].ToString();

                        //sMother_County, sMother_Phone, sMother_Mobile, sMother_FAX, sMother_Email, sFather_fName,
                        opatient.GuardianDetail.PatientMotherCounty = dt.Rows[0]["sMother_County"].ToString();
                        opatient.GuardianDetail.PatientMotherCountry = dt.Rows[0]["sMother_Country"].ToString();
                        opatient.GuardianDetail.PatientMotherPhone = dt.Rows[0]["sMother_Phone"].ToString();
                        opatient.GuardianDetail.PatientMotherMobile = dt.Rows[0]["sMother_Mobile"].ToString();
                        opatient.GuardianDetail.PatientMotherFAX = dt.Rows[0]["sMother_FAX"].ToString();
                        opatient.GuardianDetail.PatientMotherEmail = dt.Rows[0]["sMother_Email"].ToString();
                        opatient.GuardianDetail.PatientFatherFirstName = dt.Rows[0]["sFather_fName"].ToString();

                        //sFather_mName, sFather_lName, sFather_Address1, sFather_Address2, sFather_City, 
                        opatient.GuardianDetail.PatientFatherMiddleName = dt.Rows[0]["sFather_mName"].ToString();
                        opatient.GuardianDetail.PatientFatherLastName = dt.Rows[0]["sFather_lName"].ToString();
                        opatient.GuardianDetail.PatientFatherAddress1 = dt.Rows[0]["sFather_Address1"].ToString();
                        opatient.GuardianDetail.PatientFatherAddress2 = dt.Rows[0]["sFather_Address2"].ToString();
                        opatient.GuardianDetail.PatientFatherCity = dt.Rows[0]["sFather_City"].ToString();

                        //sFather_State, sFather_ZIP, sFather_County, sFather_Phone, sFather_Mobile, sFather_FAX, 
                        opatient.GuardianDetail.PatientFatherState = dt.Rows[0]["sFather_State"].ToString();
                        opatient.GuardianDetail.PatientFatherZip = dt.Rows[0]["sFather_ZIP"].ToString();
                        opatient.GuardianDetail.PatientFatherCounty = dt.Rows[0]["sFather_County"].ToString();
                        opatient.GuardianDetail.PatientFatherCountry = dt.Rows[0]["sFather_Country"].ToString();
                        opatient.GuardianDetail.PatientFatherPhone = dt.Rows[0]["sFather_Phone"].ToString();
                        opatient.GuardianDetail.PatientFatherMobile = dt.Rows[0]["sFather_Mobile"].ToString();
                        opatient.GuardianDetail.PatientFatherFAX = dt.Rows[0]["sFather_FAX"].ToString();

                        //sFather_Email, sGuardian_fName, sGuardian_mName, sGuardian_lName, sGuardian_Address1, 
                        opatient.GuardianDetail.PatientFatherEmail = dt.Rows[0]["sFather_Email"].ToString();
                        opatient.GuardianDetail.PatientGuardianFirstName = dt.Rows[0]["sGuardian_fName"].ToString();
                        opatient.GuardianDetail.PatientGuardianMiddleName = dt.Rows[0]["sGuardian_mName"].ToString();
                        opatient.GuardianDetail.PatientGuardianLastName = dt.Rows[0]["sGuardian_lName"].ToString();
                        opatient.GuardianDetail.PatientGuardianAddress1 = dt.Rows[0]["sGuardian_Address1"].ToString();

                        //sGuardian_Address2, sGuardian_City, sGuardian_State, sGuardian_ZIP, sGuardian_County, 
                        opatient.GuardianDetail.PatientGuardianAddress2 = dt.Rows[0]["sGuardian_Address2"].ToString();
                        opatient.GuardianDetail.PatientGuardianCity = dt.Rows[0]["sGuardian_City"].ToString();
                        opatient.GuardianDetail.PatientGuardianState = dt.Rows[0]["sGuardian_State"].ToString();
                        opatient.GuardianDetail.PatientGuardianZip = dt.Rows[0]["sGuardian_ZIP"].ToString();
                        opatient.GuardianDetail.PatientGuardianCounty = dt.Rows[0]["sGuardian_County"].ToString();
                        opatient.GuardianDetail.PatientGuardianCountry = dt.Rows[0]["sGuardian_Country"].ToString();

                        //sGuardian_Phone, sGuardian_Mobile, sGuardian_FAX, sGuardian_Email, nPatientDirective, 
                        opatient.GuardianDetail.PatientGuardianPhone = dt.Rows[0]["sGuardian_Phone"].ToString();
                        opatient.GuardianDetail.PatientGuardianMobile = dt.Rows[0]["sGuardian_Mobile"].ToString();
                        opatient.GuardianDetail.PatientGuardianFAX = dt.Rows[0]["sGuardian_FAX"].ToString();
                        opatient.GuardianDetail.PatientGuardianEmail = dt.Rows[0]["sGuardian_Email"].ToString();


                        //start ::Patient Guardian relationship
                        opatient.GuardianDetail.PatientGuardianRelationCD = dt.Rows[0]["sGuardian_RelationshipCD"].ToString();
                        opatient.GuardianDetail.PatientGuardianRelationDS = dt.Rows[0]["sGuardian_RelationshipDS"].ToString();
                        //End ::Patient Guardian relationship

                        if (dt.Rows[0]["nPatientDirective"].ToString() != "" && dt.Rows[0]["nPatientDirective"] != null)
                            opatient.DemographicsDetail.Directive = Convert.ToBoolean(dt.Rows[0]["nPatientDirective"]);

                        //nExemptFromReport, sExternalCode, sUserName, sMachineName, sInsuranceNotes, 
                        if (dt.Rows[0]["nExemptFromReport"].ToString() != "" && dt.Rows[0]["nExemptFromReport"] != null)
                            opatient.DemographicsDetail.ExemptFromReport = Convert.ToBoolean(dt.Rows[0]["nExemptFromReport"]);
                        //opatient.PatientExternalCode = dt.Rows[0]["sExternalCode"].ToString();
                        //opatient.PatientUserName = dt.Rows[0]["sUserName"].ToString();
                        //opatient.PatientMachineName = dt.Rows[0]["sMachineName"].ToString();



                        //bitIsGuarantor, bitIsBlock
                        //if (dt.Rows[0]["bitIsGuarantor"] != "")
                        //    opatient.PatientHasGuarantor = Convert.ToBoolean(dt.Rows[0]["bitIsGuarantor"]);
                        //if (dt.Rows[0]["bitIsBlock"] != "")
                        //    opatient.PatientIsBlock = Convert.ToBoolean(dt.Rows[0]["bitIsBlock"]);



                        opatient.DemographicsDetail.SetToCollection = Convert.ToBoolean(dt.Rows[0]["bSetToCollection"]);
                    }
                    dt.Dispose();
                }



                opatient.DemographicsDetail.PatientPCPId = 0;
                opatient.DemographicsDetail.PatientPharmacyID = 0;
                opatient.DemographicsDetail.PharmacyName = "";
                opatient.DemographicsDetail.PrimaryCarePhysicianName = "";

                //Retrieve referral Information
                DataTable dtDetails = null;
                string strSQlDetails = "";

                //strSQlReff = "Select (ISNULL(Patient_DTL.sFirstName,'')+ ' ' +ISNULL(Patient_DTL.sMiddleName,'')+ ' '+ISNULL(Patient_DTL.sLastName,'')) AS sName , Contacts_MST.nContactID as nContactID from Contacts_MST  inner join Patient_DTL on Contacts_MST.nContactID = Patient_DTL.nContactId where Patient_DTL.npatientid='" + patientid + "'";
                //COMMENTED BY SHUBHANGI 20100609 
                //strSQlDetails = "SELECT ISNULL(nContactId,0) AS nContactId,ISNULL(nPatientDetailID,0) AS nPatientDetailID, ISNULL(sName,'') AS sName, ISNULL(sContact,'') AS  sContact,ISNULL(sAddressLine1,'') AS  sAddressLine1, ISNULL(sAddressLine2,'') AS sAddressLine2 , "
                //+ " ISNULL(sCity,'') AS  sCity, ISNULL(sState,'') AS sState , ISNULL(sZIP,'') AS sZIP ,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry, ISNULL(sPhone,'') AS sPhone , ISNULL(sFax,'') AS  sFax, "
                //+ " ISNULL(sEmail,'') AS  sEmail, ISNULL(sURL,'') AS  sURL, ISNULL(sMobile,'') AS  sMobile, ISNULL(sPager,'') AS  sPager, "
                //+ " ISNULL(sNotes,'') AS  sNotes, ISNULL(sFirstName,'') AS sFirstName , ISNULL(sMiddleName,'') AS sMiddleName , ISNULL(sLastName,'') AS sLastName , "
                //+ " ISNULL(sGender,'') AS  sGender, ISNULL(sTaxonomy,'') AS  sTaxonomy, ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc , ISNULL(sTaxID,'') AS  sTaxID, "
                //+ " ISNULL(sUPIN,'') AS  sUPIN, ISNULL(sNPI,'') AS  sNPI, ISNULL(sHospitalAffiliation,'') AS sHospitalAffiliation , ISNULL(sExternalCode,'') AS sExternalCode , "
                //+ " ISNULL(sDegree,'') AS  sDegree, ISNULL(nContactFlag,0) AS  nContactFlag, "
                //+ " ISNULL(sNCPDPID,'') AS sNCPDPID,ISNULL(sPharmacyStatus,'') AS sPharmacyStatus,ISNULL(sServiceLevel,'') AS sServiceLevel,ActiveStartTime,ActiveEndTime"
                //+ " FROM Patient_DTL WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + "";

                //ADDED PREFIX COLUMN BY SHUBHANGI 20100609
                strSQlDetails = "SELECT ISNULL(nContactId,0) AS nContactId,ISNULL(nPatientDetailID,0) AS nPatientDetailID, ISNULL(sName,'') AS sName, ISNULL(sContact,'') AS  sContact,ISNULL(sAddressLine1,'') AS  sAddressLine1, ISNULL(sAddressLine2,'') AS sAddressLine2 , "
               + " ISNULL(sCity,'') AS  sCity, ISNULL(sState,'') AS sState , ISNULL(sZIP,'') AS sZIP ,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry, ISNULL(sPhone,'') AS sPhone , ISNULL(sFax,'') AS  sFax, "
               + " ISNULL(sEmail,'') AS  sEmail, ISNULL(sURL,'') AS  sURL, ISNULL(sMobile,'') AS  sMobile, ISNULL(sPager,'') AS  sPager, "
               + " ISNULL(sNotes,'') AS  sNotes, ISNULL(sPrefix,'') AS sPrefix ,ISNULL(sFirstName,'') AS sFirstName , ISNULL(sMiddleName,'') AS sMiddleName , ISNULL(sLastName,'') AS sLastName , "
               + " ISNULL(sGender,'') AS  sGender, ISNULL(sTaxonomy,'') AS  sTaxonomy, ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc , ISNULL(sTaxID,'') AS  sTaxID, "
               + " ISNULL(sUPIN,'') AS  sUPIN, ISNULL(sNPI,'') AS  sNPI, ISNULL(sHospitalAffiliation,'') AS sHospitalAffiliation , ISNULL(sExternalCode,'') AS sExternalCode , "
               + " ISNULL(sDegree,'') AS  sDegree, ISNULL(nContactFlag,0) AS  nContactFlag, "
               + " ISNULL(sNCPDPID,'') AS sNCPDPID,ISNULL(sPharmacyStatus,'') AS sPharmacyStatus,ISNULL(nContactStatus,0) AS  nContactStatus,ISNULL(sServiceLevel,'') AS sServiceLevel,ActiveStartTime,ActiveEndTime , "
               + " dtMuTransactionDate AS  MuDate,ISNULL(bMuCheckBox,0) AS MuCheckBox "
               + " FROM Patient_DTL  WITH (NOLOCK)  WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + "";


                oDB.Retrive_Query(strSQlDetails, out dtDetails);

                if (dtDetails != null && dtDetails.Rows.Count > 0)
                {
                    PatientDetail oPatientDetail = null;
                    for (int i = 0; i < dtDetails.Rows.Count; i++)
                    {
                        oPatientDetail = new PatientDetail();

                        oPatientDetail.PatientID = patientid;
                        oPatientDetail.PatientDetailID = Convert.ToInt64(dtDetails.Rows[i]["nPatientDetailID"]);
                        oPatientDetail.ContactId = Convert.ToInt64(dtDetails.Rows[i]["nContactId"]);
                        oPatientDetail.Name = Convert.ToString(dtDetails.Rows[i]["sName"]);
                        oPatientDetail.Contact = Convert.ToString(dtDetails.Rows[i]["sContact"]);
                        oPatientDetail.AddressLine1 = Convert.ToString(dtDetails.Rows[i]["sAddressLine1"]);
                        oPatientDetail.AddressLine2 = Convert.ToString(dtDetails.Rows[i]["sAddressLine2"]);
                        oPatientDetail.City = Convert.ToString(dtDetails.Rows[i]["sCity"]);
                        oPatientDetail.State = Convert.ToString(dtDetails.Rows[i]["sState"]);
                        oPatientDetail.ZIP = Convert.ToString(dtDetails.Rows[i]["sZIP"]);
                        oPatientDetail.County = Convert.ToString(dtDetails.Rows[i]["sCounty"]);
                        oPatientDetail.Country = Convert.ToString(dtDetails.Rows[i]["sCountry"]);
                        oPatientDetail.Phone = Convert.ToString(dtDetails.Rows[i]["sPhone"]);
                        oPatientDetail.Fax = Convert.ToString(dtDetails.Rows[i]["sFax"]);
                        oPatientDetail.Email = Convert.ToString(dtDetails.Rows[i]["sEmail"]);
                        oPatientDetail.URL = Convert.ToString(dtDetails.Rows[i]["sURL"]);
                        oPatientDetail.Mobile = Convert.ToString(dtDetails.Rows[i]["sMobile"]);
                        oPatientDetail.Pager = Convert.ToString(dtDetails.Rows[i]["sPager"]);
                        oPatientDetail.Notes = Convert.ToString(dtDetails.Rows[i]["sNotes"]);
                        oPatientDetail.Prefix = Convert.ToString(dtDetails.Rows[i]["sPrefix"]);
                        oPatientDetail.FirstName = Convert.ToString(dtDetails.Rows[i]["sFirstName"]);
                        oPatientDetail.MiddleName = Convert.ToString(dtDetails.Rows[i]["sMiddleName"]);
                        oPatientDetail.LastName = Convert.ToString(dtDetails.Rows[i]["sLastName"]);
                        oPatientDetail.Gender = Convert.ToString(dtDetails.Rows[i]["sGender"]);
                        oPatientDetail.Taxonomy = Convert.ToString(dtDetails.Rows[i]["sTaxonomy"]);
                        oPatientDetail.TaxonomyDesc = Convert.ToString(dtDetails.Rows[i]["sTaxonomyDesc"]);
                        oPatientDetail.TaxID = Convert.ToString(dtDetails.Rows[i]["sTaxID"]);
                        oPatientDetail.UPIN = Convert.ToString(dtDetails.Rows[i]["sUPIN"]);
                        oPatientDetail.NPI = Convert.ToString(dtDetails.Rows[i]["sNPI"]);
                        oPatientDetail.HospitalAffiliation = Convert.ToString(dtDetails.Rows[i]["sHospitalAffiliation"]);
                        oPatientDetail.ExternalCode = Convert.ToString(dtDetails.Rows[i]["sExternalCode"]);
                        oPatientDetail.Degree = Convert.ToString(dtDetails.Rows[i]["sDegree"]);

                        oPatientDetail.NCPDPID = Convert.ToString(dtDetails.Rows[i]["sNCPDPID"]);
                        oPatientDetail.ServiceLevel = Convert.ToString(dtDetails.Rows[i]["sServiceLevel"]);
                        oPatientDetail.PharmacyStatus = Convert.ToString(dtDetails.Rows[i]["sPharmacyStatus"]);
                        oPatientDetail.ContactStatus = Convert.ToInt64(dtDetails.Rows[i]["nContactStatus"]); //**

                        if (dtDetails.Rows[i]["ActiveStartTime"] != DBNull.Value)
                            oPatientDetail.ActiveStartTime = Convert.ToDateTime(dtDetails.Rows[i]["ActiveStartTime"]);
                        if (dtDetails.Rows[i]["ActiveEndTime"] != DBNull.Value)
                            oPatientDetail.ActiveEndTime = Convert.ToDateTime(dtDetails.Rows[i]["ActiveEndTime"]);

                        if (dtDetails.Rows[i]["MuDate"] != DBNull.Value)
                            oPatientDetail.MUTransactionDate = Convert.ToDateTime(dtDetails.Rows[i]["MuDate"]);
                        else
                            oPatientDetail.MUTransactionDate = null;

                        oPatientDetail.MUCheckBox = Convert.ToBoolean(dtDetails.Rows[i]["MuCheckBox"]);

                        oPatientDetail.ContactFlag = (PatientContactType)Convert.ToInt32(dtDetails.Rows[i]["nContactFlag"]);
                        oPatientDetail.ClinicID = _ClinicID;

                        switch (oPatientDetail.ContactFlag)
                        {
                            case PatientContactType.Pharmacy:
                                opatient.PatientPharmacies.Add(oPatientDetail);
                                opatient.DemographicsDetail.PharmacyName = oPatientDetail.Name;
                                break;
                            case PatientContactType.PrimaryCarePhysician:
                                opatient.PrimaryCarePhysicians.Add(oPatientDetail);
                                //COMMENTED BY SHUBHANGI 
                                //opatient.DemographicsDetail.PrimaryCarePhysicianName = oPatientDetail.FirstName + " " + oPatientDetail.MiddleName + " " + oPatientDetail.LastName;
                                //ADDED PREFIX & SUFFIX COLUMN BY SHUBHANGI 20100609
                                opatient.DemographicsDetail.PrimaryCarePhysicianName = oPatientDetail.Prefix + " " + oPatientDetail.FirstName + " " + oPatientDetail.MiddleName + " " + oPatientDetail.LastName + " " + oPatientDetail.Degree;
                                break;
                            case PatientContactType.Referral:
                                opatient.PatientReferrals.Add(oPatientDetail);
                                break;
                            case PatientContactType.CareTeam:
                                opatient.PatientCareTeam.Add(oPatientDetail);
                                break;
                            default:
                                break;
                        }
                        oPatientDetail.Dispose();
                        oPatientDetail = null;
                    }
                }
                if (dtDetails != null)
                {
                    dtDetails.Dispose();
                    dtDetails = null;
                }
                ////nContactID, sName
                //if (dt_ref != null)
                //{
                //    if (dt_ref.Rows.Count > 0)
                //    {
                //        for (int i = 0; i <= dt_ref.Rows.Count - 1; i++)
                //        {
                //            opatient.DemographicsDetail.Referrals.Add(Convert.ToInt64(dt_ref.Rows[i]["nContactID"]), Convert.ToString(dt_ref.Rows[i]["sName"]));
                //        }
                //    }
                //}
                //dt_ref.Dispose();

                //Retrieve Insurance Information
                DataTable dt_ins;

                //string _strQuery = "SELECT nPatientID,isnull(sInsuranceName,'')as InsuranceName ,"
                //    + " PatientInsurance_DTL.nInsuranceID,ISNULL(sSubscriberID,'') AS sSubscriberID, "
                //    + " (isnull(sSubFName,'') + Space(1) + ISNULL(sSubMName,'') + Space(1) + ISNULL(sSubLName,'')) AS sSubscriberName, "
                //    + " ISNULL(sSubscriberPolicy#,'') As sSubscriberPolicy#, ISNULL(sGroup,'') as sGroup,  PatientInsurance_DTL.sPhone ,"
                //    + " Isnull(nInsuranceFlag,0) as nInsuranceFlag, PatientInsurance_DTL.dtDOB,PatientInsurance_DTL.dtEffectiveDate,"
                //    + " PatientInsurance_DTL.dtExpiryDate,PatientInsurance_DTL.sSubscriberGender,  "
                //    + " isnull(sSubFName,'') As SubFName, ISNULL(sSubMName,'') AS SubMName, ISNULL(sSubLName,'') as SubLName, "
                //    + " ISNULL(nRelationShipID,0) as RelationShipID , ISNULL(sRelationShip,'') AS RelationShip, "
                //    + " ISNULL(nDeductableamount,0) as Deductableamount, ISNULL(nCoveragePercent,0) as CoveragePercent, "
                //    + " ISNULL(nCoPay,0) as CoPay, ISNULL(bAssignmentofBenifit,0) as AssignmentofBenifit, "
                //    + " dtStartDate, dtEndDate, ISNULL(sEmployer,'') AS sEmployer,  ISNULL(sSubscriberAddr1,'') AS sSubscriberAddr1,  "
                //    + " ISNULL(sSubscriberAddr2,'') AS sSubscriberAddr2,  ISNULL(sSubscriberState,'') AS sSubscriberState,  "
                //    + " ISNULL(sSubscriberCity,'') AS sSubscriberCity,  ISNULL(sSubscriberZip,'') AS sSubscriberZip  "
                //    + " FROM  PatientInsurance_DTL where nPatientID=" + patientid + "";

                string _strQuery = "SELECT  nPatientID, ISNULL(sInsuranceName, '') AS InsuranceName, nInsuranceID, ISNULL(sSubscriberID, '') AS sSubscriberID, "
                    + " ISNULL(sSubFName, '') + SPACE(1) + ISNULL(sSubMName, '') + SPACE(1) + ISNULL(sSubLName, '') AS sSubscriberName, "
                    + " ISNULL(sSubscriberPolicy#, '') AS sSubscriberPolicy#, ISNULL(sGroup, '') AS sGroup, sPhone, "
                    + " ISNULL(nInsuranceFlag, 0) AS nInsuranceFlag, dtDOB, dtEffectiveDate, dtExpiryDate, sSubscriberGender, "
                    + " ISNULL(sSubFName, '') AS SubFName, ISNULL(sSubMName, '') AS SubMName, ISNULL(sSubLName, '') AS SubLName, "
                    + " ISNULL(nRelationShipID, 0) AS RelationShipID, ISNULL(sRelationShip, '') AS RelationShip, ISNULL(nDeductableamount, 0) AS Deductableamount, "
                    + " ISNULL(nCoveragePercent, 0) AS CoveragePercent, ISNULL(nCoPay, 0) AS CoPay, ISNULL(bAssignmentofBenifit, 0) AS AssignmentofBenifit, "
                    + " dtStartDate, dtEndDate, ISNULL(sEmployer, '') AS sEmployer, ISNULL(sSubscriberAddr1, '') AS sSubscriberAddr1, "
                    + " ISNULL(sSubscriberAddr2, '') AS sSubscriberAddr2, ISNULL(sSubscriberState, '') AS sSubscriberState, "
                    + " ISNULL(sSubscriberCity, '') AS sSubscriberCity, ISNULL(sSubscriberZip, '') AS sSubscriberZip, "
                    + " ISNULL(sInsuranceName, '') AS sInsuranceName, ISNULL(sAddressLine1, '') AS  sAddressLine1,ISNULL(sAddressLine2, '') AS  sAddressLine2,"
                    + " ISNULL(sCity, '') AS  sCity,ISNULL(sState, '') AS  sState,ISNULL(sZIP, '') AS  sZIP,ISNULL(sInsurancePhone, '') AS  sInsurancePhone, ISNULL(sPayerID,'') AS sPayerID,"
                    + " ISNULL(sFax, '') AS  sFax,ISNULL(sEmail, '') AS sEmail,ISNULL(sURL, '') AS  sURL,ISNULL(sInsuranceTypeCode, '') AS  sInsuranceTypeCode,"
                    + " ISNULL(sInsuranceTypeDesc, '') AS  sInsuranceTypeDesc,ISNULL(bAccessAssignment, 'false') AS  bAccessAssignment,"
                    + " ISNULL(bStatementToPatient,  'false') AS bStatementToPatient, ISNULL(bMedigap, 'false') AS  bMedigap,ISNULL(bReferringIDInBox19, 'false') AS bReferringIDInBox19 ,"
                    + " ISNULL(bNameOfacilityinBox33, 'false') AS bNameOfacilityinBox33, ISNULL(bDoNotPrintFacility, 'false') AS  bDoNotPrintFacility,"
                    + " ISNULL(b1stPointer, 'false') AS  b1stPointer,ISNULL(bBox31Blank, 'false') AS  bBox31Blank,ISNULL(bShowPayment, 'false') AS  bShowPayment,"
                    + " ISNULL(nTypeOBilling, 0) AS  nTypeOBilling,ISNULL(nClearingHouse, 0) AS  nClearingHouse,ISNULL(bIsClaims, 'false') AS  bIsClaims,"
                    + " ISNULL(bIsRemittanceAdvice, 'false') AS  bIsRemittanceAdvice,ISNULL(bIsElectronicCOB, 'false') AS  bIsElectronicCOB,"
                    + " ISNULL(bIsRealTimeEligibility, 'false') AS  bIsRealTimeEligibility,ISNULL(bIsRealTimeClaimStatus, 'false') AS  bIsRealTimeClaimStatus,"
                    + " ISNULL(bIsEnrollmentRequired, 'false') AS  bIsEnrollmentRequired,ISNULL(sPayerPhone, '') AS  sPayerPhone,ISNULL(sServicingState, '') AS  sServicingState,"
                    + " ISNULL(sWebsite, '') AS sWebsite ,ISNULL(sComments, '') AS  sComments,ISNULL(sPayerPhoneExtn, '') AS  sPayerPhoneExtn,ISNULL(nContactID, 0) AS nContactID,"
                    + " ISNULL(sSubscriberCounty,'') As sSubscriberCounty, "
                    + " ISNULL(sSubscriberCountry,'') As sSubscriberCountry, "
                    + " ISNULL(sCounty,'') As sCounty, "
                    + " ISNULL(sCountry,'') As sCountry, "
                    + " ISNULL(bworkerscomp,0) As bworkerscomp, "
                    + " ISNULL(bautoclaim,0) As bautoclaim, "
                    //+ " CASE ISNULL(nInsuranceFlag, 0) WHEN 0 THEN 4 WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 3 END As SortOrder"
                    //LINE COMMENTED AND MODIFIED BY DIPAK 20100309 TO ADD NEW COLUMN bIsAddressSameAsPatient IN SELECT LIST
                    //+ " CASE ISNULL(nInsuranceFlag, 0) WHEN 0 THEN 4 WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 3 END As SortOrder,ISNULL(bIsSameAsPatient,0)as bIsSameAsPatient"
                    //
                    + " CASE ISNULL(nInsuranceFlag, 0) WHEN 0 THEN 4 WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 3 END As SortOrder,ISNULL(bIsSameAsPatient,0)as bIsSameAsPatient,ISNULL(bIsAddressSameAsPatient,0)as bIsAddressSameAsPatient, "
                    + " ISNULL(sInsTypeCodeDefault,'') AS sInsTypeCodeDefault,ISNULL(sInsTypeDescriptionDefault,'') AS sInsTypeDescriptionDefault,ISNULL(sInsTypeCodeMedicare,'') AS sInsTypeCodeMedicare,ISNULL(sInsTypeDescriptionMedicare,'') AS sInsTypeDescriptionMedicare,"
                    + "ISNULL(sEligibiltyInsuranceNote,'') AS EligibilityInsuranceNote, ISNULL(bIsCompnay,0) AS bIsCompnay,ISNULL(sCompanyName,'') AS sCompanyName"
                    + " FROM PatientInsurance_DTL WITH (NOLOCK) "
                    + " WHERE nPatientID = " + patientid + " ORDER BY SortOrder ";


                oDB.Retrive_Query(_strQuery, out dt_ins);

                //nContactID, sName
                if (dt_ins != null)
                {
                    if (dt_ins.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt_ins.Rows.Count - 1; i++)
                        {
                            Insurance oInsurance = new Insurance();
                            oInsurance.PatientID = Convert.ToInt64(dt_ins.Rows[i]["nPatientID"]);
                            oInsurance.InsuranceID = Convert.ToInt64(dt_ins.Rows[i]["nInsuranceID"]);
                            oInsurance.InsuranceName = Convert.ToString(dt_ins.Rows[i]["sInsuranceName"]);
                            oInsurance.SubscriberID = Convert.ToString(dt_ins.Rows[i]["sSubscriberID"]);
                            oInsurance.SubscriberName = Convert.ToString(dt_ins.Rows[i]["sSubscriberName"]);
                            oInsurance.SubscriberFName = Convert.ToString(dt_ins.Rows[i]["SubFName"]);
                            oInsurance.SubscriberMName = Convert.ToString(dt_ins.Rows[i]["SubMName"]);
                            oInsurance.SubscriberLName = Convert.ToString(dt_ins.Rows[i]["SubLName"]);

                            //Code added on 20081030
                            oInsurance.SubscriberAddr1 = Convert.ToString(dt_ins.Rows[i]["sSubscriberAddr1"]);
                            oInsurance.SubscriberAddr2 = Convert.ToString(dt_ins.Rows[i]["sSubscriberAddr2"]);
                            oInsurance.SubscriberState = Convert.ToString(dt_ins.Rows[i]["sSubscriberState"]);
                            oInsurance.SubscriberCity = Convert.ToString(dt_ins.Rows[i]["sSubscriberCity"]);
                            oInsurance.SubscriberZip = Convert.ToString(dt_ins.Rows[i]["sSubscriberZip"]);
                            oInsurance.SubscriberCounty = Convert.ToString(dt_ins.Rows[i]["sSubscriberCounty"]);
                            oInsurance.SubscriberCountry = Convert.ToString(dt_ins.Rows[i]["sSubscriberCountry"]);
                            //

                            oInsurance.SubscriberGender = Convert.ToString(dt_ins.Rows[i]["sSubscriberGender"]);
                            oInsurance.SubscriberPolicy = Convert.ToString(dt_ins.Rows[i]["sSubscriberPolicy#"]);
                            oInsurance.Group = Convert.ToString(dt_ins.Rows[i]["sGroup"]);
                            oInsurance.Employer = Convert.ToString(dt_ins.Rows[i]["sEmployer"]);
                            oInsurance.Phone = Convert.ToString(dt_ins.Rows[i]["sPhone"]);
                            oInsurance.InsuranceFlag = Convert.ToInt64(dt_ins.Rows[i]["nInsuranceFlag"]);
                            oInsurance.IsSameAsPatient = Convert.ToBoolean(dt_ins.Rows[i]["bIsSameAsPatient"]);
                            oInsurance.IsAddressSameAsPatient = Convert.ToBoolean(dt_ins.Rows[i]["bIsAddressSameAsPatient"]);
                            oInsurance.InsTypeCodeDefault = Convert.ToString(dt_ins.Rows[i]["sInsTypeCodeDefault"]);
                            oInsurance.InsTypeDescriptionDefault = Convert.ToString(dt_ins.Rows[i]["sInsTypeDescriptionDefault"]);
                            oInsurance.InsTypeCodeMedicare = Convert.ToString(dt_ins.Rows[i]["sInsTypeCodeMedicare"]);
                            oInsurance.InsTypeDescriptionMedicare = Convert.ToString(dt_ins.Rows[i]["sInsTypeDescriptionMedicare"]);


                            if (dt_ins.Rows[i]["dtDOB"].ToString().Trim() == "")
                            {
                                oInsurance.IsNotDOB = true;
                                oInsurance.DOB = DateTime.Now; ;
                            }
                            else
                            {
                                oInsurance.DOB = Convert.ToDateTime(dt_ins.Rows[i]["dtDOB"]);
                            }

                            oInsurance.EffectiveDate = Convert.ToString(dt_ins.Rows[i]["dtEffectiveDate"]);
                            oInsurance.ExpiryDate = Convert.ToString(dt_ins.Rows[i]["dtExpiryDate"]);
                            oInsurance.RelationshipID = Convert.ToInt64(dt_ins.Rows[i]["RelationShipID"]);
                            oInsurance.RelationshipName = Convert.ToString(dt_ins.Rows[i]["RelationShip"]);
                            oInsurance.DeductableAmount = Convert.ToDecimal(dt_ins.Rows[i]["Deductableamount"]);
                            oInsurance.sEligibiltyInsuranceNotes = Convert.ToString(dt_ins.Rows[i]["EligibilityInsuranceNote"]);
                            oInsurance.CoveragePercent = Convert.ToDecimal(dt_ins.Rows[i]["CoveragePercent"]);
                            oInsurance.CoPay = Convert.ToDecimal(dt_ins.Rows[i]["CoPay"]);
                            oInsurance.AssignmentofBenefit = Convert.ToBoolean(dt_ins.Rows[i]["AssignmentofBenifit"]);
                            if (dt_ins.Rows[i]["dtStartDate"].ToString().Trim() == "")
                            {
                                oInsurance.IsNotStartDate = true;
                                oInsurance.StartDate = DateTime.Now;
                            }
                            else
                            {
                                oInsurance.StartDate = Convert.ToDateTime(dt_ins.Rows[i]["dtStartDate"]);
                            }

                            if (dt_ins.Rows[i]["dtEndDate"].ToString().Trim() == "")
                            {
                                oInsurance.IsNotEndDate = true;
                                oInsurance.EndDate = DateTime.Now;
                            }
                            else
                            {
                                oInsurance.EndDate = Convert.ToDateTime(dt_ins.Rows[i]["dtEndDate"]);
                            }


                            oInsurance.ContactID = Convert.ToInt64(dt_ins.Rows[i]["nContactID"]);
                            oInsurance.PayerID = Convert.ToString(dt_ins.Rows[i]["sPayerID"]);
                            oInsurance.AddrressLine1 = Convert.ToString(dt_ins.Rows[i]["sAddressLine1"]);
                            oInsurance.AddrressLine2 = Convert.ToString(dt_ins.Rows[i]["sAddressLine2"]);
                            oInsurance.City = Convert.ToString(dt_ins.Rows[i]["sCity"]);
                            oInsurance.State = Convert.ToString(dt_ins.Rows[i]["sState"]);
                            oInsurance.ZIP = Convert.ToString(dt_ins.Rows[i]["sZIP"]);
                            oInsurance.County = Convert.ToString(dt_ins.Rows[i]["sCounty"]);
                            oInsurance.Country = Convert.ToString(dt_ins.Rows[i]["sCountry"]);

                            oInsurance.InsurancePhone = Convert.ToString(dt_ins.Rows[i]["sInsurancePhone"]);
                            oInsurance.Fax = Convert.ToString(dt_ins.Rows[i]["sFax"]);
                            oInsurance.Email = Convert.ToString(dt_ins.Rows[i]["sEmail"]);
                            oInsurance.URL = Convert.ToString(dt_ins.Rows[i]["sURL"]);
                            oInsurance.InsuranceTypeCode = Convert.ToString(dt_ins.Rows[i]["sInsuranceTypeCode"]);
                            oInsurance.InsuranceTypeDesc = Convert.ToString(dt_ins.Rows[i]["sInsuranceTypeDesc"]);
                            oInsurance.bAccessAssignment = Convert.ToBoolean(dt_ins.Rows[i]["bAccessAssignment"]);
                            oInsurance.bStatementToPatient = Convert.ToBoolean(dt_ins.Rows[i]["bStatementToPatient"]);
                            oInsurance.bMedigap = Convert.ToBoolean(dt_ins.Rows[i]["bMedigap"]);
                            oInsurance.bReferringIDInBox19 = Convert.ToBoolean(dt_ins.Rows[i]["bReferringIDInBox19"]);
                            oInsurance.bNameOfacilityinBox33 = Convert.ToBoolean(dt_ins.Rows[i]["bNameOfacilityinBox33"]);
                            oInsurance.bDoNotPrintFacility = Convert.ToBoolean(dt_ins.Rows[i]["bDoNotPrintFacility"]);
                            oInsurance.b1stPointer = Convert.ToBoolean(dt_ins.Rows[i]["b1stPointer"]);
                            oInsurance.bBox31Blank = Convert.ToBoolean(dt_ins.Rows[i]["bBox31Blank"]);
                            oInsurance.bShowPayment = Convert.ToBoolean(dt_ins.Rows[i]["bShowPayment"]);
                            oInsurance.nTypeOBilling = (TypeOfBilling)Convert.ToInt32((dt_ins.Rows[i]["nTypeOBilling"]));
                            oInsurance.nClearingHouse = Convert.ToInt64(dt_ins.Rows[i]["nClearingHouse"]);
                            oInsurance.bIsClaims = Convert.ToBoolean(dt_ins.Rows[i]["bIsClaims"]);
                            oInsurance.bIsRemittanceAdvice = Convert.ToBoolean(dt_ins.Rows[i]["bIsRemittanceAdvice"]);
                            oInsurance.bIsRealTimeEligibility = Convert.ToBoolean(dt_ins.Rows[i]["bIsRealTimeEligibility"]);
                            oInsurance.bIsElectronicCOB = Convert.ToBoolean(dt_ins.Rows[i]["bIsElectronicCOB"]);
                            oInsurance.bIsRealTimeClaimStatus = Convert.ToBoolean(dt_ins.Rows[i]["bIsRealTimeClaimStatus"]);
                            oInsurance.bIsEnrollmentRequired = Convert.ToBoolean(dt_ins.Rows[i]["bIsEnrollmentRequired"]);
                            oInsurance.sPayerPhone = Convert.ToString(dt_ins.Rows[i]["sPayerPhone"]);
                            oInsurance.sWebsite = Convert.ToString(dt_ins.Rows[i]["sWebsite"]);
                            oInsurance.sServicingState = Convert.ToString(dt_ins.Rows[i]["sServicingState"]);
                            oInsurance.sComments = Convert.ToString(dt_ins.Rows[i]["sComments"]);
                            oInsurance.sPayerPhoneExtn = Convert.ToString(dt_ins.Rows[i]["sPayerPhoneExtn"]);

                            oInsurance.Isworkerscomp = Convert.ToBoolean(dt_ins.Rows[i]["bworkerscomp"]);
                            oInsurance.Isautoclaim = Convert.ToBoolean(dt_ins.Rows[i]["bautoclaim"]);
                            oInsurance.IsCompnay = Convert.ToBoolean(dt_ins.Rows[i]["bIsCompnay"]);
                            oInsurance.SubscriberCompanyLName = Convert.ToString(dt_ins.Rows[i]["sCompanyName"]);
                            //Deductableamount, CoveragePercent, CoPay, AssignmentofBenifit, dtStartDate, dtEndDate
                            opatient.InsuranceDetails.InsurancesDetails.Add(oInsurance);

                            //opatient.InsuranceDetails.InsurancesDetails.Add(Convert.ToInt64(dt_ins.Rows[i]["nInsuranceID"]), Convert.ToString(dt_ins.Rows[i]["InsuranceName"]), Convert.ToString(dt_ins.Rows[i]["sSubscriberName"]), Convert.ToString(dt_ins.Rows[i]["sSubscriberPolicy#"]), Convert.ToString(dt_ins.Rows[i]["sSubscriberID"]), Convert.ToString(dt_ins.Rows[i]["sGroup"]), Convert.ToString(dt_ins.Rows[i]["sEmployer"]), Convert.ToString(dt_ins.Rows[i]["sPhone"]), Convert.ToDateTime(dt_ins.Rows[i]["dtDOB"]), Convert.ToBoolean(dt_ins.Rows[i]["bPrimaryFlag"]), Convert.ToBoolean(dt_ins.Rows[i]["bPrimaryFlag"]));
                        }
                    }
                    dt_ins.Dispose();
                }




                if (IsTableExists("Patient_OtherDetails"))
                {
                    //Demographic Other Details
                    DataTable dtDemoOther;
                    gloDatabaseLayer.DBParameters oParam = default(gloDatabaseLayer.DBParameters);

                    oDB.Connect(false);
                    oParam = new gloDatabaseLayer.DBParameters();
                    oParam.Add("@nPatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("gsp_GetPatientOtherDetails", oParam, out dtDemoOther);
                    oDB.Disconnect();

                    //string _strQueryDemoOther = "SELECT nPatientID , ISNULL(sSpouseName , '') AS sSpouseName , ISNULL(sSpousePhone , '') AS sSpousePhone , ISNULL(nRegistrationDate , 0) AS nRegistrationDate , ISNULL(sPatientLawyer , '') AS sPatientLawyer , ISNULL(bSignatureOnFile , 'false') AS bSignatureOnFile , ISNULL(nClinicID , 0) AS nClinicID , ISNULL(bReminderDeclined , 'false') AS bReminderDeclined , ISNULL(nSexualOrientationCategoryID , 0) AS nSexualOrientationCategoryID , ISNULL(sSexualOrientationCode , '') AS sSexualOrientationCode , ISNULL(sSexualOrientationDesc , '') AS sSexualOrientationDesc , ISNULL(nGenderIdentityCateroryID , 0) AS nGenderIdentityCateroryID , ISNULL(sGenderIdentityCode , '') AS sGenderIdentityCode , ISNULL(sGenderIdentityDesc , '') AS sGenderIdentityDesc FROM Patient_OtherDetails WITH (NOLOCK) WHERE nPatientID ='" + patientid + "' ";
                    //oDB.Retrive_Query(_strQueryDemoOther, out dtDemoOther);

                    if (dtDemoOther != null && dtDemoOther.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDemoOther.Rows.Count; i++)
                        {
                            opatient.PatientDemographicOtherInfo.PatientID = Convert.ToInt64(dtDemoOther.Rows[i]["nPatientID"]);
                            opatient.PatientDemographicOtherInfo.SpouseName = Convert.ToString(dtDemoOther.Rows[i]["sSpouseName"]);
                            opatient.PatientDemographicOtherInfo.SpousePhone = Convert.ToString(dtDemoOther.Rows[i]["sSpousePhone"]);
                            opatient.PatientDemographicOtherInfo.RegistrationDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtDemoOther.Rows[i]["nRegistrationDate"]));
                            opatient.PatientDemographicOtherInfo.PatientLawyer = dtDemoOther.Rows[i]["sPatientLawyer"].ToString();
                            opatient.PatientDemographicOtherInfo.SOF = Convert.ToBoolean(dtDemoOther.Rows[i]["bSignatureOnFile"]);
                            opatient.PatientDemographicOtherInfo.Reminders = Convert.ToBoolean(dtDemoOther.Rows[i]["bReminderDeclined"]);
                            opatient.PatientDemographicOtherInfo.SexualOrientationID = Convert.ToInt64(dtDemoOther.Rows[i]["nSexualOrientationCategoryID"]);
                            opatient.PatientDemographicOtherInfo.SexualOrientationCode = Convert.ToString(dtDemoOther.Rows[i]["sSexualOrientationCode"]);
                            opatient.PatientDemographicOtherInfo.SexualOrientationDesc = Convert.ToString(dtDemoOther.Rows[i]["sSexualOrientationDesc"]);
                            opatient.PatientDemographicOtherInfo.SexualOrientationOtherSpecification = Convert.ToString(dtDemoOther.Rows[i]["sSexualOrientationOtherSpecification"]); 
                            opatient.PatientDemographicOtherInfo.GenderIdentityID = Convert.ToInt64(dtDemoOther.Rows[i]["nGenderIdentityCateroryID"]);
                            opatient.PatientDemographicOtherInfo.GenderIdentityCode = Convert.ToString(dtDemoOther.Rows[i]["sGenderIdentityCode"]);
                            opatient.PatientDemographicOtherInfo.GenderIdentityDesc = Convert.ToString(dtDemoOther.Rows[i]["sGenderIdentityDesc"]);
                            opatient.PatientDemographicOtherInfo.GenderIdentityOtherSpecification = Convert.ToString(dtDemoOther.Rows[i]["sGenderIdentityOtherSpecification"]);
                            opatient.PatientDemographicOtherInfo.sPatientPrevFName = Convert.ToString(dtDemoOther.Rows[i]["sPatientPrevFname"]);
                            opatient.PatientDemographicOtherInfo.sPatientPrevMName = Convert.ToString(dtDemoOther.Rows[i]["sPatientPrevMname"]);
                            opatient.PatientDemographicOtherInfo.sPatientPrevLName = Convert.ToString(dtDemoOther.Rows[i]["sPatientPrevLname"]);
                            opatient.PatientDemographicOtherInfo.sMultipleBirthIndicator = Convert.ToString(dtDemoOther.Rows[i]["sMultipleBirthIndicator"]);
                            opatient.PatientDemographicOtherInfo.BirthOrder = Convert.ToInt64(dtDemoOther.Rows[i]["nBirthOrder"]);
                            opatient.PatientDemographicOtherInfo.PatientBirthSex = Convert.ToString(dtDemoOther.Rows[i]["sBirthSex"]);
                            opatient.PatientDemographicOtherInfo.ImmunizationRegistryStatus = Convert.ToString(dtDemoOther.Rows[i]["sImmunizationRegistryStatus"]);
                        }
                    }
                    if (dtDemoOther != null)
                    {
                        dtDemoOther.Dispose();
                        dtDemoOther = null;
                    }
                    if (oParam != null)
                    {
                        oParam.Dispose();
                        oParam = null;
                    }

                }

                if (IsTableExists("Patient_BadDebt"))
                {
                    DataTable dtDemoOther;
                    string _strQueryDemoOther = "SELECT 1 FROM Patient_Baddebt WHERE nPatientID='" + patientid + "' ";
                    oDB.Retrive_Query(_strQueryDemoOther, out dtDemoOther);
                    if (dtDemoOther != null && dtDemoOther.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDemoOther.Rows.Count; i++)
                        {
                            opatient.PatientDemographicOtherInfo.isBadDebtPatient = true;
                            //opatient
                        }
                    }
                    else
                    {
                        opatient.PatientDemographicOtherInfo.isBadDebtPatient = false;
                    }
                    if (dtDemoOther != null)
                    {
                        dtDemoOther.Dispose();
                        dtDemoOther = null;
                    }
                    bBadDebtStatus = opatient.PatientDemographicOtherInfo.isBadDebtPatient;
                }



                #region "Get Workers comp info"

                DataTable dtWorkerscompinfo = null;

                {
                    string _strInsOther = "Select ISNULL(nType,0) AS Type,ISNULL(sClaimno,'') AS Claimno,dtValidFrom,dtValidTo,isnull(sOtherinfo,'') as sOtherinfo from Patient_WorkersComp WITH (NOLOCK) where nPatientID='" + patientid + "' ";

                    oDB.Retrive_Query(_strInsOther, out dtWorkerscompinfo);
                    if (dtWorkerscompinfo != null && dtWorkerscompinfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtWorkerscompinfo.Rows.Count; i++)
                        {
                            PatientWorkersComp _PatientWorkersComp = new PatientWorkersComp();

                            _PatientWorkersComp.Claimno = Convert.ToString(dtWorkerscompinfo.Rows[i]["Claimno"]);
                            _PatientWorkersComp.Type = Convert.ToInt64(dtWorkerscompinfo.Rows[i]["Type"]);
                            _PatientWorkersComp.ValidFrom = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtWorkerscompinfo.Rows[i]["dtValidFrom"]));
                            if (Convert.ToString(dtWorkerscompinfo.Rows[i]["dtValidTo"]) != "10101")
                            {
                                _PatientWorkersComp.ValidTo = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtWorkerscompinfo.Rows[i]["dtValidTo"]));
                            }
                            else
                            {
                                _PatientWorkersComp.ValidTo = DateTime.MinValue;
                            }

                            _PatientWorkersComp.Otherinfo = (Convert.ToString(dtWorkerscompinfo.Rows[i]["sOtherinfo"]));

                            opatient.PatientWorkersComp.Add(_PatientWorkersComp);
                            _PatientWorkersComp = null;

                        }
                    }
                }
                if (dtWorkerscompinfo != null)
                {
                    dtWorkerscompinfo.Dispose();
                    dtWorkerscompinfo = null;
                }
                #endregion


                //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                #region "PatientAccounts"
                Int64 PatAcnt = 0;

                if (IsTableExists("PA_Accounts_Patients"))
                {
                    DataTable dtPatientAccounts;
                    string _strSqlQuery = "Select nAccountPatientId, nPAccountID, nPatientID," +
                         " (sAccountNo + ' - ' + (Select LTrim(RTrim(sFirstName + ' ' + sLastName)) from Patient_OtherContacts " +
                                 " where nPatientContactID = (Select nGuarantorId from PA_Accounts " +
                                        " where nPAccountId = PA_Accounts_Patients.nPAccountId))) as sAccountNo, " +
                           " sPatientCode, dtAccountClosedDate," +
                           " nClinicID, nSiteID, sMachineName, nUserID, dtRecordDate, bIsActive,bIsOwnAccount" +
                           " From PA_Accounts_Patients" +
                           " Where nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID;

                    oDB.Retrive_Query(_strSqlQuery, out dtPatientAccounts);

                    if (dtPatientAccounts != null && dtPatientAccounts.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPatientAccounts.Rows.Count; i++)
                        {
                            PatientAccount oPatientAccount = new PatientAccount();

                            oPatientAccount.AccountPatientID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nAccountPatientID"].ToString());
                            oPatientAccount.PAccountID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nPAccountID"].ToString());
                            oPatientAccount.AccountNo = dtPatientAccounts.Rows[i]["sAccountNo"].ToString();
                            oPatientAccount.PatientID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nPatientID"].ToString());

                            oPatientAccount.PatientCode = dtPatientAccounts.Rows[i]["sPatientCode"].ToString();
                            oPatientAccount.ClinicID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nClinicID"].ToString());
                            oPatientAccount.SiteID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nSiteID"].ToString());
                            oPatientAccount.MachineName = dtPatientAccounts.Rows[i]["sMachineName"].ToString();
                            oPatientAccount.UserID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nUserID"].ToString());
                            oPatientAccount.RecordDate = Convert.ToDateTime(dtPatientAccounts.Rows[i]["dtRecordDate"].ToString());
                            oPatientAccount.Active = Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsActive"].ToString());
                            oPatientAccount.OwnAccount = Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsOwnAccount"].ToString());
                            if (oPatientAccount.OwnAccount == true)
                            {
                                PatAcnt = oPatientAccount.PAccountID;
                            }
                            opatient.PatientAccounts.Add(oPatientAccount);
                        }
                    }
                    if (dtPatientAccounts != null)
                    {
                        dtPatientAccounts.Dispose();
                        dtPatientAccounts = null;
                    }
                }
                #endregion

                #region "PatientAccount Guarantor and PatientOtherGuarantors"


                if (IsTableExists("Patient_OtherContacts") && opatient.PatientAccounts.Count > 0)
                {
                    DataTable dtGuarantors;
                    string _strSqlQuery = "";
                    if (PatAcnt == 0)
                    {
                        _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                        + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                        + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                        + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                        + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                        + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID,ISNULL(nGuarantorType,1) as nGuarantorType,bIsAccountGuarantor"
                        + " FROM Patient_OtherContacts WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + " AND nPAccountId =" + opatient.PatientAccounts[0].PAccountID + " and bIsAccountGuarantor = 1 ORDER BY nPatientContactTypeFlag";
                    }
                    else
                    {
                        _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                        + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                        + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                        + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                        + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                        + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID,ISNULL(nGuarantorType,1) as nGuarantorType,bIsAccountGuarantor"
                        + " FROM Patient_OtherContacts WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + " AND nPAccountId =" + PatAcnt + " and bIsAccountGuarantor = 1 ORDER BY nPatientContactTypeFlag";

                    }
                    oDB.Retrive_Query(_strSqlQuery, out dtGuarantors);
                    if (dtGuarantors != null && dtGuarantors.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtGuarantors.Rows.Count; i++)
                        {
                            PatientOtherContact oGuarantor = new PatientOtherContact();

                            //Account guarantor
                            oGuarantor.PatientID = patientid;
                            oGuarantor.PatientContactID = Convert.ToInt64(dtGuarantors.Rows[i]["nPatientContactID"]);
                            oGuarantor.FirstName = Convert.ToString(dtGuarantors.Rows[i]["sFirstName"]);
                            oGuarantor.MiddleName = Convert.ToString(dtGuarantors.Rows[i]["sMiddleName"]);
                            oGuarantor.LastName = Convert.ToString(dtGuarantors.Rows[i]["sLastName"]);
                            if (dtGuarantors.Rows[i]["nDOB"] != null && dtGuarantors.Rows[i]["nDOB"].ToString() != "")
                            {
                                oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGuarantors.Rows[i]["nDOB"]));
                            }
                            oGuarantor.SSN = Convert.ToString(dtGuarantors.Rows[i]["sSSN"]);
                            oGuarantor.Relation = Convert.ToString(dtGuarantors.Rows[i]["sRelation"]);
                            oGuarantor.Gender = Convert.ToString(dtGuarantors.Rows[i]["sGender"]);
                            oGuarantor.AddressLine1 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine1"]);
                            oGuarantor.AddressLine2 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine2"]);
                            oGuarantor.City = Convert.ToString(dtGuarantors.Rows[i]["sCity"]);
                            oGuarantor.State = Convert.ToString(dtGuarantors.Rows[i]["sState"]);

                            oGuarantor.County = Convert.ToString(dtGuarantors.Rows[i]["sCounty"]);
                            oGuarantor.Country = Convert.ToString(dtGuarantors.Rows[i]["sCountry"]);

                            oGuarantor.Zip = Convert.ToString(dtGuarantors.Rows[i]["sZIP"]);
                            oGuarantor.Phone = Convert.ToString(dtGuarantors.Rows[i]["sPhone"]);
                            oGuarantor.Mobile = Convert.ToString(dtGuarantors.Rows[i]["sMobile"]);
                            oGuarantor.Email = Convert.ToString(dtGuarantors.Rows[i]["sEmail"]);
                            oGuarantor.Fax = Convert.ToString(dtGuarantors.Rows[i]["sFax"]);
                            oGuarantor.IsActive = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsActive"]);
                            oGuarantor.VisitID = Convert.ToInt64(dtGuarantors.Rows[i]["nVisitID"]);
                            oGuarantor.AppointmentID = Convert.ToInt64(dtGuarantors.Rows[i]["nAppointmentID"]);
                            oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtGuarantors.Rows[i]["nGuarantorAsPatientID"]);
                            oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactTypeFlag"]);
                            oGuarantor.OtherConatctType = (PatientOtherContactType)Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactType"]);
                            oGuarantor.PAccountID = Convert.ToInt64(opatient.PatientAccounts[0].PAccountID);
                            oGuarantor.GurantorType = (GuarantorType)Convert.ToInt32(dtGuarantors.Rows[i]["nGuarantorType"]);
                            oGuarantor.IsAccountGuarantor = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsAccountGuarantor"]);
                            opatient.PatientGuarantors.Add(oGuarantor);
                        }
                    }
                    if (dtGuarantors != null)
                    {
                        dtGuarantors.Dispose();
                        dtGuarantors = null;
                    }
                }
                else
                {
                    MessageBox.Show("Patient is not associated with an Account.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //get PatientOtherGuarantors

                if (IsTableExists("Patient_OtherContacts"))
                {
                    DataTable dtOtherGuarantors;
                    string _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                    + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                    + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                    + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                    + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                    + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID,ISNULL(nGuarantorType,1) as nGuarantorType,bIsAccountGuarantor"
                    + " FROM Patient_OtherContacts WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + " AND nPAccountId = 0 ORDER BY nPatientContactTypeFlag";

                    oDB.Retrive_Query(_strSqlQuery, out dtOtherGuarantors);
                    if (dtOtherGuarantors != null && dtOtherGuarantors.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtOtherGuarantors.Rows.Count; i++)
                        {
                            PatientOtherContact oGuarantor = new PatientOtherContact();
                            oGuarantor.PatientID = patientid;
                            oGuarantor.PatientContactID = Convert.ToInt64(dtOtherGuarantors.Rows[i]["nPatientContactID"]);
                            oGuarantor.FirstName = Convert.ToString(dtOtherGuarantors.Rows[i]["sFirstName"]);
                            oGuarantor.MiddleName = Convert.ToString(dtOtherGuarantors.Rows[i]["sMiddleName"]);
                            oGuarantor.LastName = Convert.ToString(dtOtherGuarantors.Rows[i]["sLastName"]);
                            if (dtOtherGuarantors.Rows[i]["nDOB"] != null && dtOtherGuarantors.Rows[i]["nDOB"].ToString() != "")
                            {
                                oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtOtherGuarantors.Rows[i]["nDOB"]));
                            }
                            oGuarantor.SSN = Convert.ToString(dtOtherGuarantors.Rows[i]["sSSN"]);
                            oGuarantor.Relation = Convert.ToString(dtOtherGuarantors.Rows[i]["sRelation"]);
                            oGuarantor.Gender = Convert.ToString(dtOtherGuarantors.Rows[i]["sGender"]);
                            oGuarantor.AddressLine1 = Convert.ToString(dtOtherGuarantors.Rows[i]["sAddressLine1"]);
                            oGuarantor.AddressLine2 = Convert.ToString(dtOtherGuarantors.Rows[i]["sAddressLine2"]);
                            oGuarantor.City = Convert.ToString(dtOtherGuarantors.Rows[i]["sCity"]);
                            oGuarantor.State = Convert.ToString(dtOtherGuarantors.Rows[i]["sState"]);

                            oGuarantor.County = Convert.ToString(dtOtherGuarantors.Rows[i]["sCounty"]);
                            oGuarantor.Country = Convert.ToString(dtOtherGuarantors.Rows[i]["sCountry"]);

                            oGuarantor.Zip = Convert.ToString(dtOtherGuarantors.Rows[i]["sZIP"]);
                            oGuarantor.Phone = Convert.ToString(dtOtherGuarantors.Rows[i]["sPhone"]);
                            oGuarantor.Mobile = Convert.ToString(dtOtherGuarantors.Rows[i]["sMobile"]);
                            oGuarantor.Email = Convert.ToString(dtOtherGuarantors.Rows[i]["sEmail"]);
                            oGuarantor.Fax = Convert.ToString(dtOtherGuarantors.Rows[i]["sFax"]);
                            oGuarantor.IsActive = Convert.ToBoolean(dtOtherGuarantors.Rows[i]["bIsActive"]);
                            oGuarantor.VisitID = Convert.ToInt64(dtOtherGuarantors.Rows[i]["nVisitID"]);
                            oGuarantor.AppointmentID = Convert.ToInt64(dtOtherGuarantors.Rows[i]["nAppointmentID"]);
                            oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtOtherGuarantors.Rows[i]["nGuarantorAsPatientID"]);
                            oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtOtherGuarantors.Rows[i]["nPatientContactTypeFlag"]);
                            oGuarantor.OtherConatctType = (PatientOtherContactType)Convert.ToInt32(dtOtherGuarantors.Rows[i]["nPatientContactType"]);

                            oGuarantor.PAccountID = 0;
                            oGuarantor.GurantorType = (GuarantorType)Convert.ToInt32(dtOtherGuarantors.Rows[i]["nGuarantorType"]);
                            oGuarantor.IsAccountGuarantor = Convert.ToBoolean(dtOtherGuarantors.Rows[i]["bIsAccountGuarantor"]);
                            opatient.PatientOtherGuarantors.Add(oGuarantor);
                        }
                    }
                    if (dtOtherGuarantors != null)
                    {
                        dtOtherGuarantors.Dispose();
                        dtOtherGuarantors = null;
                    }
                }

                #endregion "PatientAccount Guarantors "


                #region "Patient Representative"


                if (IsTableExists("PatientRepresentative_Mst") && IsTableExists("PatientRepresentative_Dtl"))
                {
                    DataTable dtPatientRepresentative;
                    string _strSqlQuery = "SELECT prdtl.nPRID , isnull(sFirstName,'') sFirstName , isnull(sLastName,'') sLastName ,  isnull(dtDOB,null) dtDOB ,  isnull(sGender,'') sGender ,    isnull(sEmail,'') sEmail ,";
                    _strSqlQuery += " isnull(sPhone,'') sPhone,isnull(PatientPortalAccess.sLoginName,'') UserName,isnull(PatientPortalAccess.sPortalPassword,'') Password,isnull(PatientPortalAccess.sSecurityQuestion,'') SecurityQuestion,isnull(PatientPortalAccess.sSecurityAnswer,'') SecurityAnswer FROM PatientRepresentative_Mst prmst  inner join PatientRepresentative_dtl prdtl on  prmst.nprid = prdtl.nprid ";
                    _strSqlQuery += "      inner join PatientPortalAccess   on  prmst.nprid = PatientPortalAccess.nPatientID ";
                    _strSqlQuery += " where prdtl.nPatientID = " + Convert.ToInt64(patientid) + "";

                    oDB.Retrive_Query(_strSqlQuery, out dtPatientRepresentative);
                    if (dtPatientRepresentative != null && dtPatientRepresentative.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPatientRepresentative.Rows.Count; i++)
                        {
                            PatientRepresentative oPatientRepresentative = new PatientRepresentative();
                            oPatientRepresentative.PRId = Convert.ToInt64(dtPatientRepresentative.Rows[i]["nPRID"]);
                            oPatientRepresentative.FirstName = Convert.ToString(dtPatientRepresentative.Rows[i]["sFirstName"]);
                            oPatientRepresentative.LastName = Convert.ToString(dtPatientRepresentative.Rows[i]["sLastName"]);
                            if (dtPatientRepresentative.Rows[i]["dtDOB"] != null && dtPatientRepresentative.Rows[i]["dtDOB"].ToString() != "")
                            {
                                oPatientRepresentative.DOB = Convert.ToDateTime(dtPatientRepresentative.Rows[i]["dtDOB"]);
                            }
                            oPatientRepresentative.Gender = Convert.ToString(dtPatientRepresentative.Rows[i]["sGender"]);
                            oPatientRepresentative.Phone = Convert.ToString(dtPatientRepresentative.Rows[i]["sPhone"]);
                            oPatientRepresentative.Email = Convert.ToString(dtPatientRepresentative.Rows[i]["sEmail"]);
                            oPatientRepresentative.UserName = Convert.ToString(dtPatientRepresentative.Rows[i]["UserName"]);
                            oPatientRepresentative.Password = Convert.ToString(dtPatientRepresentative.Rows[i]["Password"]);
                            oPatientRepresentative.SecurityQuestion = Convert.ToString(dtPatientRepresentative.Rows[i]["SecurityQuestion"]);
                            oPatientRepresentative.SecurityAnswer = Convert.ToString(dtPatientRepresentative.Rows[i]["SecurityAnswer"]);
                            opatient.PatientRepresentatives.Add(oPatientRepresentative);
                        }
                    }
                    if (dtPatientRepresentative != null)
                    {
                        dtPatientRepresentative.Dispose();
                        dtPatientRepresentative = null;
                    }
                }


                #endregion "Patient Representative"

                #region "Patient Portal Account


                odbparam.Clear();
                odbparam.Add("@PatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                DataTable dtt = null;
                oDB.Connect(false);
                oDB.Retrive("Portal_SelectPortalAccount", odbparam, out  dtt);
                PatientPortalAccount oPatientPortalAccount = null;
                if (dtt != null)
                {
                    if (dtt.Rows.Count > 0)
                    {
                        oPatientPortalAccount = new PatientPortalAccount();
                        oPatientPortalAccount.DateOfTraining = Convert.ToDateTime(dtt.Rows[0]["dtDateofPortaltraining"].ToString());
                        oPatientPortalAccount.IsTrainingProvided = Convert.ToBoolean(dtt.Rows[0]["bIsPortalTrainingProvided"].ToString());
                        opatient.PatientPortalAccount = oPatientPortalAccount;
                    }
                    dtt.Dispose();
                    dtt = null;
                }



                #endregion
                #region "API Account


                odbparam.Clear();
                odbparam.Add("@PatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                DataTable dtAPITraining = null;
                oDB.Connect(false);
                oDB.Retrive("API_SelectAPIAccount", odbparam, out  dtAPITraining);
                PatientPortalAccount oPatientAPIAccount = null;
                if (dtAPITraining != null)
                {
                    if (dtAPITraining.Rows.Count > 0)
                    {
                        oPatientAPIAccount = new PatientPortalAccount();
                        oPatientAPIAccount.DateOfTraining = Convert.ToDateTime(dtAPITraining.Rows[0]["dtDateOfAPITraining"].ToString());
                        oPatientAPIAccount.IsTrainingProvided = Convert.ToBoolean(dtAPITraining.Rows[0]["bIsAPITrainingProvided"].ToString());
                        opatient.APIAccount = oPatientAPIAccount;
                    }
                    dtAPITraining.Dispose();
                    dtAPITraining = null;
                }



                #endregion
                #region "API Representative"


                if (IsTableExists("PatientRepresentative_Mst") && IsTableExists("PatientAPIRepresentative_Dtl"))
                {
                    DataTable dtPatientRepresentative;
                    string _strSqlQuery = "SELECT prdtl.nPRID , isnull(sFirstName,'') sFirstName , isnull(sLastName,'') sLastName ,  isnull(dtDOB,null) dtDOB ,  isnull(sGender,'') sGender ,    isnull(sEmail,'') sEmail ,";
                    _strSqlQuery += " isnull(sPhone,'') sPhone,isnull(APIAccess.sLoginName,'') UserName,isnull(APIAccess.sPortalPassword,'') Password FROM PatientRepresentative_Mst prmst  inner join PatientAPIRepresentative_dtl prdtl on  prmst.nprid = prdtl.nprid ";
                    _strSqlQuery += "      inner join APIAccess   on  prmst.nprid = APIAccess.nAPIAccessUserID ";
                    _strSqlQuery += " where prdtl.nPatientID = " + Convert.ToInt64(patientid) + "";

                    oDB.Retrive_Query(_strSqlQuery, out dtPatientRepresentative);
                    if (dtPatientRepresentative != null && dtPatientRepresentative.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPatientRepresentative.Rows.Count; i++)
                        {
                            PatientRepresentative oPatientRepresentative = new PatientRepresentative();
                            oPatientRepresentative.PRId = Convert.ToInt64(dtPatientRepresentative.Rows[i]["nPRID"]);
                            oPatientRepresentative.FirstName = Convert.ToString(dtPatientRepresentative.Rows[i]["sFirstName"]);
                            oPatientRepresentative.LastName = Convert.ToString(dtPatientRepresentative.Rows[i]["sLastName"]);
                            if (dtPatientRepresentative.Rows[i]["dtDOB"] != null && dtPatientRepresentative.Rows[i]["dtDOB"].ToString() != "")
                            {
                                oPatientRepresentative.DOB = Convert.ToDateTime(dtPatientRepresentative.Rows[i]["dtDOB"]);
                            }
                            oPatientRepresentative.Gender = Convert.ToString(dtPatientRepresentative.Rows[i]["sGender"]);
                            oPatientRepresentative.Phone = Convert.ToString(dtPatientRepresentative.Rows[i]["sPhone"]);
                            oPatientRepresentative.Email = Convert.ToString(dtPatientRepresentative.Rows[i]["sEmail"]);
                            oPatientRepresentative.UserName = Convert.ToString(dtPatientRepresentative.Rows[i]["UserName"]);
                            //string _encryptionKey = "12345678";
                            //gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                            // oPatientRepresentative.Password =oClsEncryption.DecryptFromBase64String(Convert.ToString(dtPatientRepresentative.Rows[i]["Password"]), _encryptionKey);
                            oPatientRepresentative.Password = Convert.ToString(dtPatientRepresentative.Rows[i]["Password"]);
                            oPatientRepresentative.SecurityQuestion = "";// Convert.ToString(dtPatientRepresentative.Rows[i]["SecurityQuestion"]);
                            oPatientRepresentative.SecurityAnswer = "";// Convert.ToString(dtPatientRepresentative.Rows[i]["SecurityAnswer"]);
                            opatient.APIRepresentatives.Add(oPatientRepresentative);



                        }
                    }
                    if (dtPatientRepresentative != null)
                    {
                        dtPatientRepresentative.Dispose();
                        dtPatientRepresentative = null;
                    }
                }


                #endregion "API Representative"

                //Added by Mukesh 20090829
                //#region "Patient Settings"

                //if (IsTableExists("PatientSettings"))
                //{
                //    DataTable dtPatientSettings;
                //    string strQuery = "select isnull(sValue,'') as sValue FROM PatientSettings WHERE sName = 'Exclude from Statement' AND nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + "";

                //    oDB.Retrive_Query(strQuery, out dtPatientSettings);
                //    if (dtPatientSettings != null && dtPatientSettings.Rows.Count > 0)
                //    {
                //        if (Convert.ToString(dtPatientSettings.Rows[0]["sValue"]) == "1")
                //        {
                //            opatient.DemographicsDetail.ExcludeFromStatement = true;
                //        }
                //        else
                //        {
                //            opatient.DemographicsDetail.ExcludeFromStatement = false;
                //        }
                //    }
                //    dtPatientSettings = null;
                //}

                //#endregion                
                #region "Patient Settings"

                if (IsTableExists("PatientSettings"))
                {
                    DataTable dtPatientSettings;
                    string strQuery = "select isNull(sName,'') as sName, isnull(sValue,'') as sValue FROM PatientSettings WHERE sName in ('Exclude from Statement','Signature on File Date','CMS1500 Box13') AND nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + "";

                    oDB.Retrive_Query(strQuery, out dtPatientSettings);
                    if (dtPatientSettings != null)
                    {
                        foreach (DataRow dr in dtPatientSettings.Rows)
                        {
                            switch (dr["sName"].ToString())
                            {
                                case "Exclude from Statement":
                                    if (Convert.ToString(dr["sValue"]) == "1")
                                    {
                                        opatient.DemographicsDetail.ExcludeFromStatement = true;
                                    }
                                    else
                                    {
                                        opatient.DemographicsDetail.ExcludeFromStatement = false;
                                    }
                                    break;
                                case "Signature on File Date":
                                    opatient.PatientDemographicOtherInfo.SOFDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dr["sValue"]));
                                    break;
                                case "CMS1500 Box13":
                                    opatient.PatientDemographicOtherInfo.CMS1500Box13 = Convert.ToString(dr["sValue"]);
                                    break;
                            }
                        }
                        dtPatientSettings.Dispose();
                    }

                    dtPatientSettings = null;
                }

                #endregion

                //Added to Get Patient Medical Conditions
                #region Patient Medical Condition
                DataTable dtMedCat;
                string strMedCat = "";
                strMedCat = "select nMedicalCategoryID  from Patient_MedicalCategory  WHERE nPatientID = " + patientid + "";
                oDB.Retrive_Query(strMedCat, out dtMedCat);
                if (dtMedCat != null)
                {
                    if (dtMedCat.Rows.Count > 0)
                    {
                        for (int m = 0; m < dtMedCat.Rows.Count; m++)
                        {
                            opatient.PatientDemographicOtherInfo.MedicalConditions.Add(Convert.ToInt64(dtMedCat.Rows[m]["nMedicalCategoryID"]));
                        }
                    }
                    dtMedCat.Dispose();
                    dtMedCat = null;
                }
                #endregion

                return opatient;

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                throw new Exception("Error while retrieving patient.");
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                odbparam.Dispose();
                odbparam = null;
            }
            //   return opatient;
        }

        public PatientCards GetPatientCards(Int64 patientid)
        {
            //Function to Get the patient information from Database

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();

            odbparam.Add("@PatienID", patientid, ParameterDirection.Input, SqlDbType.BigInt);

            PatientCards opatientcards = new PatientCards();
            PatientCard opatientcard = null;
            PatientCard oPatientCardBack;

            DataTable dt;

            try
            {
                oDB.Connect(false);
                oDB.Retrive("PA_Select_Patient_Cards", odbparam, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            //
                            //opatient.PatientPhoto = System.Drawing.Image(dt.Rows[0]["iPhoto"]);
                            if ((dt.Rows[i]["iCard"] != DBNull.Value))
                            {
                                opatientcard = new PatientCard();

                                opatientcard.CardNumber = Convert.ToInt64(dt.Rows[i]["nPatientCardNo"].ToString());
                                if (dt.Rows[i]["dtScanDateTime"].ToString() != "" && dt.Rows[i]["dtScanDateTime"] != null)
                                { opatientcard.ScannedDateTime = Convert.ToDateTime(dt.Rows[i]["dtScanDateTime"]); }
                                opatientcard.CardTypeID = Convert.ToInt64(dt.Rows[i]["nCardTypeID"].ToString());
                                opatientcard.CardData = dt.Rows[i]["sScannedData"].ToString();
                                //Added by Mitesh 
                                opatientcard.Username = dt.Rows[i]["sUserName"].ToString();
                                //System.Drawing.Image ilogo;
                                //Byte[] arrPicture = (Byte[])dt.Rows[i]["iCard"];
                                //System.IO.MemoryStream ms = new System.IO.MemoryStream(arrPicture);
                                //ilogo = Image.FromStream(ms);
                                //opatientcard.CardImage = ilogo;

                                //***************************************************************
                                //Handled Exception while BLOB is invalid or Corrupted
                                //***************************************************************


                                System.Drawing.Image ilogo = null;
                                Byte[] arrPicture = (Byte[])dt.Rows[i]["iCard"];
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(arrPicture);

                                try
                                {
                                    ilogo = Image.FromStream(ms);
                                    opatientcard.CardImage = ilogo;
                                    opatientcards.Add(opatientcard);
                                }
                                catch (ArgumentException ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                }

                                //***************************************************************
                                try
                                {
                                    ms.Flush();
                                    ms.Close();
                                    ms.Dispose();
                                }
                                catch
                                {
                                }
                                arrPicture = null;

                            }

                            //Code added on 29th March 2008 - for Back Side of Card to show
                            if ((dt.Rows[i]["iCardBack"] != DBNull.Value))
                            {
                                oPatientCardBack = new PatientCard();
                                //System.Drawing.Image ilogoBack;
                                //Byte[] arrPictureBack = (Byte[])dt.Rows[i]["iCardBack"];
                                //System.IO.MemoryStream msBack = new System.IO.MemoryStream(arrPictureBack);
                                //ilogoBack = Image.FromStream(msBack);
                                //oPatientCardBack.CardImage = ilogoBack;

                                //***************************************************************
                                //Handled Exception while BLOB is invalid or Corrupted
                                //***************************************************************

                                System.Drawing.Image ilogoBack = null;
                                Byte[] arrPictureBack = (Byte[])dt.Rows[i]["iCardBack"];
                                System.IO.MemoryStream msBack = new System.IO.MemoryStream(arrPictureBack);

                                try
                                {
                                    ilogoBack = Image.FromStream(msBack);
                                    oPatientCardBack.CardImage = ilogoBack;
                                    if (dt.Rows[i]["dtScanDateTime"].ToString() != "" && dt.Rows[i]["dtScanDateTime"] != null)
                                    { oPatientCardBack.ScannedDateTime = Convert.ToDateTime(dt.Rows[i]["dtScanDateTime"]); }
                                    oPatientCardBack.Username = dt.Rows[i]["sUserName"].ToString();
                                    opatientcards.Add(oPatientCardBack);
                                }
                                catch (ArgumentException ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                }

                                //***************************************************************
                                try
                                {
                                    msBack.Flush();
                                    msBack.Close();
                                    msBack.Dispose();
                                }
                                catch
                                {
                                }
                                arrPictureBack = null;

                            }


                        }
                    }
                }
                dt.Dispose();
                dt = null;
                return opatientcards;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                //if (opatientcards != null)
                //{
                //    opatientcards.Dispose();
                //    opatientcards = null;
                //}
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbparam != null)
                {
                    odbparam.Dispose();
                    odbparam = null;
                }
            }

        }

        private class CardPages
        {
            private int _mstartImg;
            public int startImg
            {
                get { return _mstartImg; }
                set { _mstartImg = value; }
            }

            private int _mendImg;
            public int endImg
            {
                get { return _mendImg; }
                set { _mendImg = value; }
            }

            private float _mdpiX;
            public float dpiX
            {
                get { return _mdpiX; }
                set { _mdpiX = value; }
            }

            private float _mdpiY;
            public float dpiY
            {
                get { return _mdpiY; }
                set { _mdpiY = value; }
            }

        }
        bool toCreateEMF = gloGlobal.gloTSPrint.UseEMFForImages;
        private int CreateEMFPatientCards(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        {
            try
            {
                thisGraphics.Clear(Color.White);
                PlaceMulitpleImagesInOnePage(thisGraphics);
                return 0;
            }
            catch
            {
                return 1;
            }

        }
        public bool UniformSize =false;
        public bool readCardPrintSetting()
        {
            bool result = false;
            using (gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting())
            {
                try
                {
                    if (oSettings.ReadSettings_XML("CardScannerSettings", "UniformCardPrinting") != "")
                    {
                        string _uniFormPrinting = oSettings.ReadSettings_XML("CardScannerSettings", "UniformCardPrinting");
                        if (_uniFormPrinting == "1")
                        {
                           result= true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oSettings != null) { oSettings.Dispose(); }
                }
            }
            return result;
        }

        public Dictionary<String, Byte[]> PrintToBitmapsWithDPi(float pageWidth, float pageHeight, float DpiX, float DpiY, PatientCards oPatientCards)
        {
            Dictionary<String, Byte[]> myList = new Dictionary<String, Byte[]>();
            _pageDPI = new List<CardPages>();
            _oPatientCards = oPatientCards;
            try
            {
                UniformSize = readCardPrintSetting();
                //Dim exitfor As Boolean = False
                RectangleF MyBounds = new RectangleF(0, 0, pageWidth * DpiX, pageHeight * DpiY);

                //Dim MySize As New Size(DpiX, DpiY)

                RectangleF MarginRect = new RectangleF(15, 15, 87, 18);
                //RectangleF MarginRect = new RectangleF(2, 2, 108, 72);
                //Changed margin width to increase card size
                if (UniformSize)
                {
                    MarginRect.Width = 36;
                }

                MarginRect.Y = (MarginRect.Y * DpiY / 72);
                MarginRect.X = (MarginRect.X * DpiX / 72);
                MarginRect.Height = (MarginRect.Height * DpiY / 72);
                MarginRect.Width = (MarginRect.Width * DpiX / 72);

                float NewTop = MarginRect.Top;
                float NewHeight = MyBounds.Height - (2 * MarginRect.Height);
                float NewLeft = MarginRect.Left;
                float NewWidth = MyBounds.Width - (2 * MarginRect.Width);
                bool hasMorePages = true;
                int iCount = 0;

                while (hasMorePages)
                {
                    CardPages CardPagesObj = new CardPages();
                    CardPagesObj.startImg = iCount;
                    CardPagesObj.dpiX = 96;
                    CardPagesObj.dpiY = 96;
                    hasMorePages = false;
                    //Dim NewBitmap As Bitmap = New Bitmap(MyBounds.Width, MyBounds.Height)
                    //NewBitmap.SetResolution(DpiX, DpiY)
                    //Dim NewGraphics As Graphics = Graphics.FromImage(NewBitmap)

                    float y = NewTop;

                    if ((_oPatientCards != null))
                    {
                        int jCount = -1;
                        for (Int32 i = iCount; i <= _oPatientCards.Count - 1; i++)
                        {
                            if (_oPatientCards[i].CardImage != null)
                            {
                                jCount++;
                                System.Drawing.Image thisImage = _oPatientCards[i].CardImage;
                                //Dim thisImageBound As RectangleF = New RectangleF(0, 0, thisImage.Width, thisImage.Height) 'thisImage.GetBounds(NewGraphics.PageUnit)

                                //currDpix = thisImageBound.Width / pageWidth
                                //If currDpix > CardPagesObj.dpiX Then
                                //    CardPagesObj.dpiX = currDpix
                                //End If
                                //currDpiy = thisImageBound.Height / pageHeight
                                //If currDpiy > CardPagesObj.dpiY Then
                                //    CardPagesObj.dpiY = currDpiy
                                //End If


                                float cardHeight = thisImage.Height * DpiY / thisImage.VerticalResolution;
                                float cardWidth = thisImage.Width * DpiX / thisImage.HorizontalResolution;

                                float ScaleFactor = 1;
                                if ((cardWidth > NewWidth) || (cardHeight > NewHeight))
                                {
                                    float ScaleX = NewWidth / cardWidth;
                                    float ScaleY = NewHeight / cardHeight;
                                    if ((ScaleX > ScaleY))
                                    {
                                        cardHeight = NewHeight;
                                        cardWidth = cardWidth * ScaleY;
                                        ScaleFactor = ScaleY;
                                    }
                                    else
                                    {
                                        cardWidth = NewWidth;
                                        cardHeight = cardHeight * ScaleX;
                                        ScaleFactor = ScaleX;
                                    }
                                }
                                CardPagesObj.dpiX = Math.Max(CardPagesObj.dpiX, thisImage.HorizontalResolution / ScaleFactor);
                                CardPagesObj.dpiY = Math.Max(CardPagesObj.dpiY, thisImage.VerticalResolution / ScaleFactor);
                                if (UniformSize)
                                {
                                    if (jCount == 8)
                                    {



                                        hasMorePages = true;
                                        iCount = i;
                                        //exitfor = True
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }
                                else
                                {
                                    if (((cardHeight + y) >= NewHeight))
                                    {
                                        if ((y == NewTop))
                                        {
                                        }
                                        else
                                        {
                                            hasMorePages = true;
                                            iCount = i;
                                            //exitfor = True
                                            break; // TODO: might not be correct. Was : Exit For
                                        }

                                    }
                                }

                                //NewGraphics.DrawImage(thisImage, NewLeft, y, Convert.ToInt64(cardWidth), Convert.ToInt64(cardHeight))
                                y += (long)cardHeight + NewTop;
                            }
                        }

                    }
                    if ((hasMorePages == false))
                    {
                        //hasMorePages = False
                        CardPagesObj.endImg = oPatientCards.Count - 1;
                    }
                    else
                    {
                        CardPagesObj.endImg = iCount - 1;
                    }
                    CardPagesObj.dpiX = Math.Min(CardPagesObj.dpiX, DpiX);
                    CardPagesObj.dpiY = Math.Min(CardPagesObj.dpiY, DpiY);
                    _pageDPI.Add(CardPagesObj);

                    //Dim ms As New MemoryStream()
                    //NewBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                    //myList.Add((pCount).ToString(), ms.ToArray())

                    //If (IsNothing(NewGraphics) = False) Then
                    //    NewGraphics.Dispose()
                    //    NewGraphics = Nothing
                    //End If
                    //If (IsNothing(NewBitmap) = False) Then
                    //    NewBitmap.Dispose()
                    //    NewBitmap = Nothing
                    //End If
                }

                // Create Byte dictionary as per page DIP
                myList = getPageDictionary(pageWidth, pageHeight);


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                _pageDPI.Clear();
            }
            return myList;
        }

        private Dictionary<String, Byte[]> getPageDictionary(float pageWidth, float pageHeight)
        {
            Dictionary<String, Byte[]> myList = new Dictionary<String, Byte[]>();
            try
            {
                UniformSize = readCardPrintSetting();
                for (_jthPosition = 0; _jthPosition < _pageDPI.Count; _jthPosition++)
                {
                    float DpiX_Page = _pageDPI[_jthPosition].dpiX;
                    float DpiY_Page = _pageDPI[_jthPosition].dpiY;

                    RectangleF MyBounds_Page = new RectangleF(0, 0, pageWidth * DpiX_Page, pageHeight * DpiY_Page);
                    //Dim MySize_Page As New Size(DpiX_Page, DpiY_Page)

                    RectangleF MarginRect_Page = new RectangleF(15, 15, 87, 18);
                    //RectangleF MarginRect_Page = new RectangleF(2, 2, 108, 72);
                    //Changed margin width to increase card size
                    if (UniformSize)
                    {
                        MarginRect_Page.Width = 36;
                    }

                    MarginRect_Page.Y = (MarginRect_Page.Y * DpiY_Page / 72);
                    MarginRect_Page.X = (MarginRect_Page.X * DpiX_Page / 72);
                    MarginRect_Page.Height = (MarginRect_Page.Height * DpiY_Page / 72);
                    MarginRect_Page.Width = (MarginRect_Page.Width * DpiX_Page / 72);

                    _NewTop_Page = MarginRect_Page.Top;
                    _NewHeight_Page = MyBounds_Page.Height - (2 * MarginRect_Page.Height);
                    _NewLeft_Page = MarginRect_Page.Left;
                    _NewWidth_Page = MyBounds_Page.Width - (2 * MarginRect_Page.Width);
                    if (UniformSize)
                    {
                        _NewHorzDivision = (_NewWidth_Page - MarginRect_Page.X) / 2;
                        _NewVertDivision = (_NewHeight_Page - (3 * MarginRect_Page.Y)) / 4;
                    }
                    else
                    {
                        _NewHorzDivision = 0;
                        _NewVertDivision = 0;
                    }
                    Bitmap NewBitmap_Page = new Bitmap((int)MyBounds_Page.Width, (int)MyBounds_Page.Height);
                    NewBitmap_Page.SetResolution(DpiX_Page, DpiY_Page);
                    byte[] emfBytes = null;
                    bool gotEmfBytes = false;
                    try
                    {
                        if (toCreateEMF)
                        {
                            emfBytes = gloGlobal.CreateEMF.GetEMFBytes((float)NewBitmap_Page.Width / NewBitmap_Page.HorizontalResolution, (float)NewBitmap_Page.Height / NewBitmap_Page.VerticalResolution, NewBitmap_Page.Width, NewBitmap_Page.Height, CreateEMFPatientCards);
                            gotEmfBytes = true;
                        }
                    }
                    catch
                    {

                    }
                    if (!gotEmfBytes)
                    {
                        toCreateEMF = false;
                        using (Graphics NewGraphics_Page = Graphics.FromImage(NewBitmap_Page))
                        {

                            PlaceMulitpleImagesInOnePage(NewGraphics_Page);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                NewBitmap_Page.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                try
                                {
                                    ms.Flush();

                                }
                                catch
                                {
                                }

                                myList.Add((_jthPosition + 1).ToString(), ms.ToArray());
                                try
                                {
                                    ms.Close();


                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        myList.Add((_jthPosition + 1).ToString(), emfBytes);
                    }

                    if (NewBitmap_Page != null)
                    {
                        NewBitmap_Page.Dispose();
                        NewBitmap_Page = null;
                    }
                }

            }
            catch
            {
            }

            return myList;
        }

        private static List<CardPages> _pageDPI = null;
        private static PatientCards _oPatientCards = null;
        private static int _jthPosition = 0;
        private static float _NewTop_Page = 0;
        private static float _NewHeight_Page = 0;
        private static float _NewLeft_Page = 0;
        private static float _NewWidth_Page = 0;
        private static float _NewHorzDivision = 0;
        private static float _NewVertDivision = 0;

        private static void PlaceMulitpleImagesInOnePage(Graphics NewGraphics_Page)
        {
            float y_Page = _NewTop_Page;

            int jCount = -1;
            for (Int32 k = _pageDPI[_jthPosition].startImg; k <= _pageDPI[_jthPosition].endImg; k++)
            {
                if (_oPatientCards[k].CardImage != null)
                {
                    jCount++;
                    //GraphicsUnit gp = NewGraphics_Page.PageUnit;
                    System.Drawing.Image thisImage_Page = _oPatientCards[k].CardImage;
                    //RectangleF thisImageBound_Page = thisImage_Page.GetBounds(ref gp);
                    //NewGraphics_Page.PageUnit = gp;

                    float _cardHeight_Page = thisImage_Page.Height * _pageDPI[_jthPosition].dpiY / thisImage_Page.VerticalResolution;
                    float _cardWidth_Page = thisImage_Page.Width * _pageDPI[_jthPosition].dpiX / thisImage_Page.HorizontalResolution;
                    float x_Page = _NewLeft_Page;
                    float y_addition = _NewVertDivision;
                    if (_NewVertDivision != 0)
                    {
                        float ScaleX = _NewHorzDivision / _cardWidth_Page;
                        float ScaleY = _NewVertDivision / _cardHeight_Page;
                        if ((ScaleX > ScaleY))
                        {
                            _cardHeight_Page = _NewVertDivision;
                            _cardWidth_Page = _cardWidth_Page * ScaleY;
                        }
                        else
                        {
                            _cardWidth_Page = _NewHorzDivision;
                            _cardHeight_Page = _cardHeight_Page * ScaleX;
                        }

                        if ((jCount % 2) == 1)
                        {
                            x_Page += _NewLeft_Page + _NewHorzDivision;
                        }
                        else
                        {
                            y_addition = -_NewTop_Page;
                        }
                    }
                    else
                    {
                        if ((_cardWidth_Page > _NewWidth_Page) || (_cardHeight_Page > _NewHeight_Page))
                        {
                            float ScaleX = _NewWidth_Page / _cardWidth_Page;
                            float ScaleY = _NewHeight_Page / _cardHeight_Page;
                            if ((ScaleX > ScaleY))
                            {
                                _cardHeight_Page = _NewHeight_Page;
                                _cardWidth_Page = _cardWidth_Page * ScaleY;
                            }
                            else
                            {
                                _cardWidth_Page = _NewWidth_Page;
                                _cardHeight_Page = _cardHeight_Page * ScaleX;
                            }
                        }
                        y_addition = _cardHeight_Page;
                    }
                    //If ((cardHeight_Page + y_Page) >= NewHeight_Page) Then
                    //    If (y_Page = NewTop_Page) Then
                    //    Else
                    //        hasMorePages = True
                    //        iCount = i
                    //        exitfor = True
                    //        Exit For
                    //    End If

                    //End If

                    NewGraphics_Page.DrawImage(thisImage_Page, x_Page, y_Page, _cardWidth_Page, _cardHeight_Page);
                    y_Page += (y_addition + _NewTop_Page);
                }
            }
        }


        public PatientCards GetSinglePatientCard(Int64 patientid)
        {
            //Function to Get the patient information from Database

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();

            odbparam.Add("@PatienID", patientid, ParameterDirection.Input, SqlDbType.BigInt);

            PatientCards opatientcards = new PatientCards();
            PatientCard opatientcard = null;
            //PatientCard oPatientCardBack;

            DataTable dt;

            try
            {
                oDB.Connect(false);
                oDB.Retrive("PA_Single_Patient_Card", odbparam, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        //{

                        //
                        //opatient.PatientPhoto = System.Drawing.Image(dt.Rows[0]["iPhoto"]);
                        if ((dt.Rows[0]["iCard"] != DBNull.Value))
                        {
                            opatientcard = new PatientCard();

                            opatientcard.CardNumber = Convert.ToInt64(dt.Rows[0]["nPatientCardNo"].ToString());
                            if (dt.Rows[0]["dtScanDateTime"].ToString() != "" && dt.Rows[0]["dtScanDateTime"] != null)
                            { opatientcard.ScannedDateTime = Convert.ToDateTime(dt.Rows[0]["dtScanDateTime"]); }
                            opatientcard.CardTypeID = Convert.ToInt64(dt.Rows[0]["nCardTypeID"].ToString());
                            opatientcard.CardData = dt.Rows[0]["sScannedData"].ToString();

                            opatientcard.Username = dt.Rows[0]["sUserName"].ToString();

                            gloPictureBox.gloPictureBox myPicture = new gloPictureBox.gloPictureBox();
                            myPicture.sZoomVersion = "7X";
                            myPicture.byteImage = (Byte[])dt.Rows[0]["iCard"];

                            try
                            {
                                opatientcard.CardImage = (Image)myPicture.Image.Clone();
                                opatientcards.Add(opatientcard);
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            }
                            myPicture.Dispose();
                            myPicture = null;

                            //System.Drawing.Image ilogo = null;
                            //Byte[] arrPicture = (Byte[])dt.Rows[0]["iCard"];
                            //System.IO.MemoryStream ms = new System.IO.MemoryStream(arrPicture);

                            //try
                            //{
                            //    ilogo = Image.FromStream(ms);
                            //    opatientcard.CardImage = ilogo;
                            //    opatientcards.Add(opatientcard);
                            //}
                            //catch (ArgumentException ex)
                            //{
                            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            //}

                            //***************************************************************

                            //ms.Flush();
                            //ms.Close();
                            //ms.Dispose();
                            //arrPicture = null;

                        }

                        ////Code added on 29th March 2008 - for Back Side of Card to show
                        //if ((dt.Rows[i]["iCardBack"] != DBNull.Value))
                        //{
                        //    oPatientCardBack = new PatientCard();


                        //    System.Drawing.Image ilogoBack = null;
                        //    Byte[] arrPictureBack = (Byte[])dt.Rows[i]["iCardBack"];
                        //    System.IO.MemoryStream msBack = new System.IO.MemoryStream(arrPictureBack);

                        //    try
                        //    {
                        //        ilogoBack = Image.FromStream(msBack);
                        //        oPatientCardBack.CardImage = ilogoBack;
                        //        if (dt.Rows[i]["dtScanDateTime"].ToString() != "" && dt.Rows[i]["dtScanDateTime"] != null)
                        //        { oPatientCardBack.ScannedDateTime = Convert.ToDateTime(dt.Rows[i]["dtScanDateTime"]); }
                        //        oPatientCardBack.Username = dt.Rows[i]["sUserName"].ToString();
                        //        opatientcards.Add(oPatientCardBack);
                        //    }
                        //    catch (ArgumentException ex)
                        //    {
                        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        //    }

                        //    //***************************************************************

                        //    msBack.Flush();
                        //    msBack.Close();
                        //    msBack.Dispose();
                        //    arrPictureBack = null;

                        //}


                        // }
                    }
                }
                dt.Dispose();
                dt = null;
                return opatientcards;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                //if (opatientcards != null)
                //{
                //    opatientcards.Dispose();
                //    opatientcards = null;
                //}
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbparam != null)
                {
                    odbparam.Dispose();
                    odbparam = null;
                }

            }

        }
        public string GetPatientName(Int64 patientID)
        {

            DataTable dtPatient = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            string _result = "";
            string firstName = "", midName = "", lastName = "";
            try
            {
                oDB.Connect(false);

                //get the provider details in the datatable -- dtProvider
                _strSQL = "SELECT sFirstName, sMiddleName, sLastName FROM Patient WHERE nPatientID = " + patientID;
                oDB.Retrive_Query(_strSQL, out dtPatient);

                if (dtPatient.Rows.Count > 0 && dtPatient != null)
                {

                    //  oProvider._nProviderID = dtProvider.Rows[0]["nProviderID"];
                    if (dtPatient.Rows[0]["sFirstName"] != null)
                        firstName = dtPatient.Rows[0]["sFirstName"].ToString();

                    if (dtPatient.Rows[0]["sMiddleName"] != null)
                        midName = dtPatient.Rows[0]["sMiddleName"].ToString();

                    if (dtPatient.Rows[0]["sLastName"] != null)
                        lastName = dtPatient.Rows[0]["sLastName"].ToString();
                }
                _result = firstName + " " + midName + " " + lastName;


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {

                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {

                oDB.Disconnect();
                if (oDB != null)
                    oDB.Dispose();
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }
            }

            return _result;

        }

        //Added By Pramod Nair For Fetching The Garuntor Details

        public PatientOtherContacts GetGaruntorDetails(Int64 patientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            PatientOtherContacts oGaruntors = new PatientOtherContacts();

            try
            {
                Int64 patientid = patientID;

                // String _strSQL = "";
                if (IsTableExists("Patient_OtherContacts"))
                {
                    DataTable dtGuarantors;
                    oDB.Connect(false);

                    string _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                    + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                    + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                    + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                    + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                    + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID "
                    + " FROM Patient_OtherContacts WHERE nPatientID = " + patientid + " AND ISNULL(nClinicID,1) = " + _ClinicID + " ORDER BY nPatientContactTypeFlag";

                    oDB.Retrive_Query(_strSqlQuery, out dtGuarantors);
                    if (dtGuarantors != null && dtGuarantors.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtGuarantors.Rows.Count; i++)
                        {
                            PatientOtherContact oGuarantor = new PatientOtherContact();

                            oGuarantor.PatientID = patientid;

                            oGuarantor.FirstName = Convert.ToString(dtGuarantors.Rows[i]["sFirstName"]);
                            oGuarantor.MiddleName = Convert.ToString(dtGuarantors.Rows[i]["sMiddleName"]);
                            oGuarantor.LastName = Convert.ToString(dtGuarantors.Rows[i]["sLastName"]);
                            oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGuarantors.Rows[i]["nDOB"]));
                            oGuarantor.SSN = Convert.ToString(dtGuarantors.Rows[i]["sSSN"]);
                            oGuarantor.Relation = Convert.ToString(dtGuarantors.Rows[i]["sRelation"]);
                            oGuarantor.Gender = Convert.ToString(dtGuarantors.Rows[i]["sGender"]);
                            oGuarantor.AddressLine1 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine1"]);
                            oGuarantor.AddressLine2 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine2"]);
                            oGuarantor.City = Convert.ToString(dtGuarantors.Rows[i]["sCity"]);
                            oGuarantor.State = Convert.ToString(dtGuarantors.Rows[i]["sState"]);

                            oGuarantor.County = Convert.ToString(dtGuarantors.Rows[i]["sCounty"]);
                            oGuarantor.Country = Convert.ToString(dtGuarantors.Rows[i]["sCountry"]);

                            oGuarantor.Zip = Convert.ToString(dtGuarantors.Rows[i]["sZIP"]);
                            oGuarantor.Phone = Convert.ToString(dtGuarantors.Rows[i]["sPhone"]);
                            oGuarantor.Mobile = Convert.ToString(dtGuarantors.Rows[i]["sMobile"]);
                            oGuarantor.Email = Convert.ToString(dtGuarantors.Rows[i]["sEmail"]);
                            oGuarantor.Fax = Convert.ToString(dtGuarantors.Rows[i]["sFax"]);
                            oGuarantor.IsActive = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsActive"]);
                            oGuarantor.VisitID = Convert.ToInt64(dtGuarantors.Rows[i]["nVisitID"]);
                            oGuarantor.AppointmentID = Convert.ToInt64(dtGuarantors.Rows[i]["nAppointmentID"]);
                            oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtGuarantors.Rows[i]["nGuarantorAsPatientID"]);
                            oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactTypeFlag"]);
                            oGuarantor.OtherConatctType = (PatientOtherContactType)Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactType"]);

                            oGaruntors.Add(oGuarantor);
                        }
                    }
                    if (dtGuarantors != null)
                    {
                        dtGuarantors.Dispose();
                    }
                }
            }
            catch (gloDatabaseLayer.DBException)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

            return oGaruntors;
        }


        public bool Block(Int64 patientid)
        {
            return false;
        }

        public bool ChkExistPatientID(string patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object flgUserExist = 0;
            try
            {

                oDB.Connect(false);

                flgUserExist = oDB.ExecuteScalar_Query("Select count(*) from Patient where sPatientCode='" + patientid.ToString() + "'");

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Dispose();
                oDBParameters.Dispose();
            }
            if (Convert.ToInt64(flgUserExist) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ChkExistPatientIDUpdate(string patientcode, Int64 patientid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object flgUserExist = 0;
            try
            {

                oDB.Connect(false);

                flgUserExist = oDB.ExecuteScalar_Query("Select count(*) from Patient where sPatientCode='" + patientcode.Replace("'", "''") + "' and nPatientID<>'" + patientid + "'");

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Dispose();
                oDBParameters.Dispose();
            }
            if (Convert.ToInt64(flgUserExist) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ShowPatientRegistration(Int64 PatientID, out Int64 CurrentPatientId)
        {
            //frmPatientRegistration oPatientRegistration = new frmPatientRegistration(_databaseconnectionstring);
            frmSetupPatient oPatientRegistration = new frmSetupPatient(PatientID, _databaseconnectionstring);
            //  oPatientRegistration._EditID = PatientID;
            oPatientRegistration.ShowDialog(oPatientRegistration.Parent);
            CurrentPatientId = oPatientRegistration.ReturnPatientID;

            //Change made to solve memory Leak and word crash issue
            oPatientRegistration.Close();
            oPatientRegistration.Dispose();
            oPatientRegistration = null;
        }

        public void ShowPatientRegistration(Int64 PatientID, out Int64 CurrentPatientId, out bool ReturnIsClose, Form oParentForm)
        {
            //frmPatientRegistration oPatientRegistration = new frmPatientRegistration(_databaseconnectionstring);
            frmSetupPatient oPatientRegistration = new frmSetupPatient(PatientID, _databaseconnectionstring);
            //  oPatientRegistration._EditID = PatientID;
            oPatientRegistration.ShowSaveAsCopyButton = true;
            oPatientRegistration.ShowDialog(oParentForm);
            CurrentPatientId = oPatientRegistration.ReturnPatientID;
            ReturnIsClose = oPatientRegistration.ReturnIsClose;
            //Change made to solve memory Leak and word crash issue
            oPatientRegistration.Close();
            oPatientRegistration.Dispose();
            oPatientRegistration = null;
        }

        public void ShowPatientRegistration(Int64 PatientID, ModifyPatientDetailType oModificationDetail, out Int64 CurrentPatientId, Form oParentForm)
        {

            //frmPatientRegistration oPatientRegistration = new frmPatientRegistration(_databaseconnectionstring);
            frmSetupPatient oPatientRegistration = new frmSetupPatient(PatientID, oModificationDetail, _databaseconnectionstring);
            SendHL7Details = false;
            oPatientRegistration.EvntSaveandClose += new frmSetupPatient.SaveandCloseHandler(oPatientRegistration_EvntSaveandClose);
            //  oPatientRegistration._EditID = PatientID;
            oPatientRegistration.ShowSaveAsCopyButton = true;

            oPatientRegistration.ShowDialog(oParentForm);

            CurrentPatientId = oPatientRegistration.ReturnPatientID;
            //Change made to solve memory Leak and word crash issue
            oPatientRegistration.Close();
            oPatientRegistration.EvntSaveandClose -= new frmSetupPatient.SaveandCloseHandler(oPatientRegistration_EvntSaveandClose);

            oPatientRegistration.Dispose();
            oPatientRegistration = null;
        }


        public void ShowPatientRegistration(Int64 PatientID, ModifyPatientDetailType oModificationDetail, out Int64 CurrentPatientId, out bool ReturnIsClose, Form oParent)
        {

            //frmPatientRegistration oPatientRegistration = new frmPatientRegistration(_databaseconnectionstring);
            frmSetupPatient oPatientRegistration = new frmSetupPatient(PatientID, oModificationDetail, _databaseconnectionstring);
            SendHL7Details = false;
            oPatientRegistration.EvntSaveandClose += new frmSetupPatient.SaveandCloseHandler(oPatientRegistration_EvntSaveandClose);
            //  oPatientRegistration._EditID = PatientID;
            oPatientRegistration.ShowSaveAsCopyButton = true;

            oPatientRegistration.ShowDialog(oParent);

            CurrentPatientId = oPatientRegistration.ReturnPatientID;
            ReturnIsClose = oPatientRegistration.ReturnIsClose;
            //Change made to solve memory Leak and word crash issue
            oPatientRegistration.Close();
            oPatientRegistration.EvntSaveandClose -= new frmSetupPatient.SaveandCloseHandler(oPatientRegistration_EvntSaveandClose);

            oPatientRegistration.Dispose();
            oPatientRegistration = null;
        }

        private void oPatientRegistration_EvntSaveandClose(long PatientID)
        {
            SendHL7Details = true;
        }
        //Added by Mayuri:20101006-Added Save As Copy functionality-to open form on save as copy menu click
        public void PatientRegistration(Int64 PatientID, out Int64 CurrentPatientId, out bool IsSaveAsCopy, string PatientFirstName, string PatientLastName, Form oParent)
        {

            frmSetupPatient oPatientRegistration = new frmSetupPatient(PatientID, _databaseconnectionstring, true);
            oPatientRegistration.Text = "New Patient : copy of " + PatientFirstName + " " + PatientLastName;
            oPatientRegistration.ShowDialog(oParent);
            CurrentPatientId = oPatientRegistration.ReturnPatientID;
            IsSaveAsCopy = oPatientRegistration._IsSaveAsCopy;
            //Change made to solve memory Leak and word crash issue
            oPatientRegistration.Close();
            oPatientRegistration.Dispose();
            oPatientRegistration = null;
        }

        public void ShowQuickPatientRegistration(out Int64 CurrentPatientId)
        {
            frmSetupQuickPatient ofrmSetupQuickPatient = new frmSetupQuickPatient(_databaseconnectionstring);
            ofrmSetupQuickPatient.ShowDialog(ofrmSetupQuickPatient.Parent);
            CurrentPatientId = ofrmSetupQuickPatient.ReturnPatientID;
            //Change made to solve memory Leak and word crash issue
            ofrmSetupQuickPatient.Close();
            ofrmSetupQuickPatient.Dispose();
            ofrmSetupQuickPatient = null;
        }

        /// <summary>
        ///     Function AddPatientInsrance 
        ///     TO Add Only Patient Insurance to The PatientInsrance_DTl Table
        /// </summary>
        /// <param name="PatientID" type="long">
        ///     <para>
        ///         PatientID Against whome the Insurances has to Add
        ///     </para>
        /// </param>
        /// <param name="oInsurances" type="gloPatient.Insurances">
        ///     <para>
        ///         Object of Insurances Which Contacns Insurance Information
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public Boolean AddPatientInsrance(Int64 PatientID, Insurances oInsurances)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = null;
            //    gloDatabaseLayer.DBParameter odbParam;
            try
            {
                oDB.Connect(false);
                for (int i = 0; i <= oInsurances.Count - 1; i++)
                {


                    odbParams = new gloDatabaseLayer.DBParameters();
                    //nPatientID, nInsuranceID, sSubscriberName, sSubscriberPolicy#, sSubscriberID, sGroup, sEmployer,
                    //sPhone, dtDOB, bPrimaryFlag
                    odbParams.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@InsuranceID", oInsurances[i].InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@SubscriberName", oInsurances[i].SubscriberName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@SubscriberPolicy#", oInsurances[i].SubscriberPolicy, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@SubscriberID", oInsurances[i].SubscriberID, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@Group", oInsurances[i].Group, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@Employer", oInsurances[i].Employer, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@Phone", oInsurances[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@DOB", oInsurances[i].DOB, ParameterDirection.Input, SqlDbType.DateTime);
                    odbParams.Add("@PrimaryFlag", oInsurances[i].PrimaryFlag, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@IsNotDOB", oInsurances[i].IsNotDOB, ParameterDirection.Input, SqlDbType.Bit);
                    //odbParams.Add("@InsuranceID", oPatient.Referrals[i].ReferralID, ParameterDirection.Input, SqlDbType.BigInt);
                    if (oDB.Execute("PA_Insert_Insurances", odbParams) == 0)
                    {
                        // If Patient Insurance is Not Added Then Return flase
                        oDB.Disconnect();
                        if (odbParams != null)
                        {
                            odbParams.Dispose();
                            odbParams = null;
                        }

                        return false;
                    }
                    if (odbParams != null)
                    {
                        odbParams.Dispose();
                        odbParams = null;
                    }

                }
                oDB.Disconnect();
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }
        }
        public void AddQRDAPatientInsurance(Int64 PatientID, Insurances oInsurances, Patient oPatient)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = null;
            //    gloDatabaseLayer.DBParameter odbParam;
            try
            {
                oDB.Connect(false);
                for (int i = 0; i <= oInsurances.Count - 1; i++)
                {


                    odbParams = new gloDatabaseLayer.DBParameters();
                    //nPatientID, nInsuranceID, sSubscriberName, sSubscriberPolicy#, sSubscriberID, sGroup, sEmployer,
                    //sPhone, dtDOB, bPrimaryFlag
                    odbParams.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@InsuranceID", oInsurances[i].InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@SubscriberPolicy#", oInsurances[i].SubscriberPolicy, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@SubscriberID", oInsurances[i].SubscriberID, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@Group", oInsurances[i].Group, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@Employer", oInsurances[i].Employer, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@InsuranceFlag", oInsurances[i].InsuranceFlag, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@sServicingState", oInsurances[i].sServicingState, ParameterDirection.Input, SqlDbType.NVarChar);
                    //odbParams.Add("@SubscriberName", oInsurances[i].SubscriberName, ParameterDirection.Input, SqlDbType.VarChar);


                    //odbParams.Add("@PrimaryFlag", oInsurances[i].PrimaryFlag, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@IsNotDOB", oInsurances[i].IsNotDOB, ParameterDirection.Input, SqlDbType.Bit);
                    if (oInsurances[i].IsSameAsPatient)
                    {
                        //@SubFName Varchar(50),
                        odbParams.Add("@SubFName", oPatient.DemographicsDetail.PatientFirstName, ParameterDirection.Input, SqlDbType.VarChar);
                        //@SubMName Varchar(50),
                        odbParams.Add("@SubMName", oPatient.DemographicsDetail.PatientMiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                        //@SubLName Varchar(50), 
                        odbParams.Add("@SubLName", oPatient.DemographicsDetail.PatientLastName, ParameterDirection.Input, SqlDbType.VarChar);
                        //@RelationShipID numeric(18,0), 

                        //Fields added on 20081030
                        odbParams.Add("@sSubscriberAddr1", oPatient.DemographicsDetail.PatientAddress1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        odbParams.Add("@sSubscriberAddr2", oPatient.DemographicsDetail.PatientAddress2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        odbParams.Add("@sSubscriberState", oPatient.DemographicsDetail.PatientState, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        odbParams.Add("@sSubscriberCity", oPatient.DemographicsDetail.PatientCity, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        odbParams.Add("@sSubscriberZip", oPatient.DemographicsDetail.PatientZip, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        //
                        odbParams.Add("@sSubscriberGender", oPatient.DemographicsDetail.PatientGender, ParameterDirection.Input, SqlDbType.VarChar);

                        odbParams.Add("@Phone", oPatient.DemographicsDetail.PatientPhone, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@DOB", oPatient.DemographicsDetail.PatientDOB, ParameterDirection.Input, SqlDbType.DateTime);
                        odbParams.Add("@sSubscriberCounty", oPatient.DemographicsDetail.PatientCounty, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sSubscriberCountry", oPatient.DemographicsDetail.PatientCountry, ParameterDirection.Input, SqlDbType.VarChar);
                    }
                    odbParams.Add("@RelationShip", oInsurances[i].RelationshipName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@RelationShipID", oInsurances[i].RelationshipID, ParameterDirection.Input, SqlDbType.BigInt);
                    //@RelationShip Varchar(50),

                    //@Deductableamount Decimal(18,0), 
                    odbParams.Add("@Deductableamount", oInsurances[i].DeductableAmount, ParameterDirection.Input, SqlDbType.Decimal);
                    //@CoveragePercent Decimal(18,0), 
                    odbParams.Add("@CoveragePercent", oInsurances[i].CoveragePercent, ParameterDirection.Input, SqlDbType.Decimal);
                    //@CoPay Decimal(18,0),
                    odbParams.Add("@CoPay", oInsurances[i].CoPay, ParameterDirection.Input, SqlDbType.Decimal);
                    //@AssignmentofBenifit Bit,
                    odbParams.Add("@AssignmentofBenifit", oInsurances[i].AssignmentofBenefit, ParameterDirection.Input, SqlDbType.Bit);
                    //@IsNotStartDate   bit,
                    odbParams.Add("@IsNotStartDate", oInsurances[i].IsNotStartDate, ParameterDirection.Input, SqlDbType.Bit);
                    //@dtStartDate DateTime, 
                    odbParams.Add("@dtStartDate", oInsurances[i].StartDate, ParameterDirection.Input, SqlDbType.DateTime);
                    //@IsNotEndDate   bit,
                    odbParams.Add("@IsNotEndDate", oInsurances[i].IsNotEndDate, ParameterDirection.Input, SqlDbType.Bit);
                    //@dtEndDate DateTime
                    odbParams.Add("@dtEndDate", oInsurances[i].EndDate, ParameterDirection.Input, SqlDbType.DateTime);
                    //@dtEndDate DateTime


                    //Insurance Details
                    odbParams.Add("@sInsuranceName", oInsurances[i].InsuranceName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sPayerID", oInsurances[i].PayerID, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sAddressLine1", oInsurances[i].AddrressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sAddressLine2", oInsurances[i].AddrressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sCity", oInsurances[i].City, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sState", oInsurances[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sZIP", oInsurances[i].ZIP, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsurancePhone", oInsurances[i].InsurancePhone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sFax", oInsurances[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sEmail", oInsurances[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sURL", oInsurances[i].URL, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsuranceTypeCode", oInsurances[i].InsuranceTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsuranceTypeDesc", oInsurances[i].InsuranceTypeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@bAccessAssignment", oInsurances[i].bAccessAssignment, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bStatementToPatient", oInsurances[i].bStatementToPatient, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bMedigap", oInsurances[i].bMedigap, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bReferringIDInBox19", oInsurances[i].bReferringIDInBox19, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bNameOfacilityinBox33", oInsurances[i].bNameOfacilityinBox33, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bDoNotPrintFacility", oInsurances[i].bDoNotPrintFacility, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@b1stPointer", oInsurances[i].b1stPointer, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bBox31Blank", oInsurances[i].bBox31Blank, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bShowPayment", oInsurances[i].bShowPayment, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@nTypeOBilling", oInsurances[i].nTypeOBilling, ParameterDirection.Input, SqlDbType.Int);
                    odbParams.Add("@nClearingHouse", oInsurances[i].nClearingHouse, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@bIsClaims", oInsurances[i].bIsClaims, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsRemittanceAdvice", oInsurances[i].bIsRemittanceAdvice, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsRealTimeEligibility", oInsurances[i].bIsRealTimeEligibility, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsElectronicCOB", oInsurances[i].bIsElectronicCOB, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsRealTimeClaimStatus", oInsurances[i].bIsRealTimeClaimStatus, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@bIsEnrollmentRequired", oInsurances[i].bIsEnrollmentRequired, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@sPayerPhone", oInsurances[i].sPayerPhone, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sWebsite", oInsurances[i].sWebsite, ParameterDirection.Input, SqlDbType.VarChar);
                    //odbParams.Add("@sServicingState", oInsurances[i].sServicingState, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sComments", oInsurances[i].sComments, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sPayerPhoneExtn", oInsurances[i].sPayerPhoneExtn, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@nContactID", oInsurances[i].ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@bNotesInBox19", oInsurances[i].bNotesInBox19, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@sOfficeID", oInsurances[i].OfficeID, ParameterDirection.Input, SqlDbType.VarChar);


                    odbParams.Add("@sCounty", oInsurances[i].County, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sCountry", oInsurances[i].Country, ParameterDirection.Input, SqlDbType.VarChar);

                    odbParams.Add("@bworkerscomp", oInsurances[i].Isworkerscomp, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@bautoclaim", oInsurances[i].Isautoclaim, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@bIsSameAsPatient", oInsurances[i].IsSameAsPatient, ParameterDirection.Input, SqlDbType.Bit);
                    //@bIsAddressSameAsPatient PARAMETER ADDED BY DIPAK 20100309 TO REMEMBER DATABASE STATE OF ADDRESS SAME AS PATIENT SETTING
                    odbParams.Add("@bIsAddressSameAsPatient", oInsurances[i].IsAddressSameAsPatient, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@sInsTypeCodeDefault", oInsurances[i].InsTypeCodeDefault, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsTypeDescriptionDefault", oInsurances[i].InsTypeDescriptionDefault, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsTypeCodeMedicare", oInsurances[i].InsTypeCodeMedicare, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sInsTypeDescriptionMedicare", oInsurances[i].InsTypeDescriptionMedicare, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sEligibilityInsuranceNote", oInsurances[i].sEligibiltyInsuranceNotes, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@sUserName", _username, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@bIsCompany", oInsurances[i].IsCompnay, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@sSubscriberCompanyName", oInsurances[i].SubscriberCompanyLName, ParameterDirection.Input, SqlDbType.VarChar);


                    //odbParams.Add("@InsuranceID", oPatient.Referrals[i].ReferralID, ParameterDirection.Input, SqlDbType.BigInt);
                    if (oDB.Execute("PA_Insert_Insurances", odbParams) == 0)
                    {
                        // If Patient Insurance is Not Added Then Return flase
                        oDB.Disconnect();
                        if (odbParams != null)
                        {
                            odbParams.Dispose();
                            odbParams = null;
                        }


                    }
                    if (odbParams != null)
                    {
                        odbParams.Dispose();
                        odbParams = null;
                    }

                }
                oDB.Disconnect();
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }
        }


        #region "Methods to get Patients Insurances,Appointments,Reffrals,Procedure & Billing"

        /// <summary>
        /// Method to get Reffrals of Particular Patient
        /// </summary>
        /// <param name="PatientID">ID of Patient of whose reffrals are to be fetched. </param>
        /// <returns>DataTable containing Reffral information of requested Patient. </returns>
        public DataTable getPatientReffrals(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            DataTable dt_Reffrals = null;
            try
            {
                //ADD PREFIX & SUFFIX IF THE EXIST 20100619
                //string strSQlDetails = "SELECT ISNULL(sPrefix,'') AS sPrefix ,ISNULL(sFirstName,'') AS sFirstName , ISNULL(sMiddleName,'') AS sMiddleName , ISNULL(sLastName,'') AS sLastName ,ISNULL(sDegree,'') AS sDegree, ISNULL(sAddressLine1,'') AS  sAddressLine1, ISNULL(sAddressLine2,'') AS sAddressLine2 , "
                //+ " ISNULL(sCity,'') AS  sCity, ISNULL(sState,'') AS sState , ISNULL(sZIP,'') AS sZIP , dbo.formatPhone(ISNULL(sPhone,''),0) AS sPhone , dbo.formatFax(ISNULL(sFax,'')) AS  sFax, "
                //+ " ISNULL(sEmail,'') AS  sEmail, ISNULL(sURL,'') AS  sURL, dbo.formatPhone(ISNULL(sMobile,''),0) AS  sMobile, "
                //+ " ISNULL(sTaxonomy,'') AS  sTaxonomy, ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc , ISNULL(sTaxID,'') AS  sTaxID, "
                //+ " ISNULL(sUPIN,'') AS  sUPIN, ISNULL(sNPI,'') AS  sNPI, ISNULL(sHospitalAffiliation,'') AS sHospitalAffiliation , ISNULL(sExternalCode,'') AS sExternalCode , "
                //+ " ISNULL(sDegree,'') AS  sDegree,"
                //+ " ISNULL(nPatientDetailID,0) AS nPatientDetailID, ISNULL(nContactId,0) AS nContactId"
                //+ " FROM Patient_DTL WHERE nPatientID = " + PatientID + " AND nContactFlag = " + PatientContactType.Referral.GetHashCode() + " AND ISNULL(nClinicID,1) = " + _ClinicID + "";

                //  odbParams = new gloDatabaseLayer.DBParameters();
                //nPatientID, nInsuranceID, sSubscriberName, sSubscriberPolicy#, sSubscriberID, sGroup, sEmployer,
                //sPhone, dtDOB, bPrimaryFlag
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nContactFlag", PatientContactType.Referral.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);


                oDB.Retrive("gsp_GetPatientReferral", oParameters, out dt_Reffrals);
                oParameters.Clear();
                oParameters.Dispose();
                oParameters = null;
                if (dt_Reffrals != null)
                {

                    return dt_Reffrals;

                }
                return null;


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }



        //sEligibiltyInsuranceNote added for Problem 00000213: 8020V
        public DataTable getPatientInsurancesNew(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();

            oDB.Connect(false);
            DataTable dtInsurance = null;
            try
            {
                //string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                //                    " CASE WHEN ISNULL(PatientInsurance_DTL.bISCompnay, 0) = 0 THEN ISNULL(PatientInsurance_DTL.sSubFName, '') + ' ' + ISNULL(PatientInsurance_DTL.sSubMName, '') + ' ' + ISNULL(PatientInsurance_DTL.sSubLName, '') ELSE PatientInsurance_DTL.sCompanyName END AS sSubscriberName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                //                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                //                    " PatientInsurance_DTL.sPhone, " +
                //                    " PatientInsurance_DTL.dtDOB,  " +
                //                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                //                    " PatientInsurance_DTL.dtExpiryDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                //                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                //                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                //                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                //                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                //                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                //                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                //                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                //                    " PatientInsurance_DTL.sSubscriberGender,  " +
                //                    " PatientInsurance_DTL.sPayerID,  " +
                //                    " ISNULL(Patient.sCity, '') AS sCity, " +
                //                    " ISNULL(Patient.sState, '') AS sState,  " +
                //                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                //                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                //                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                //                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                //                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                //                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                //                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                //                    " ELSE '' END  AS sInsuranceFlag,  " +

                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 4 " +
                //                    " ELSE nInsuranceFlag END  AS SortOrder,  " +
                //                    " dbo.formatPhone(PatientInsurance_DTL.sInsurancePhone,0), " +
                //                    " ISNULL(PatientInsurance_DTL.bworkerscomp,0) AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim,0) AS bAutoClaim , " +
                //                     "   ISNULL(PatientInsurance_DTL.sEligibiltyInsuranceNote,'') as sEligibiltyInsuranceNote " +
                //                    " FROM PatientInsurance_DTL  " +
                //                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                //                    " LEFT OUTER JOIN PatientRelationship ON  " +
                //                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                //                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY SortOrder ";


                odbParams.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                //oDB.Retrive("gsp_GetPatientInsurances", dtInsurance );

                //02-Dec-14 Aniket: Patient switching optimization 
                oDB.Retrive("gsp_GetPatientInsurances_Dashboard", odbParams, out dtInsurance);//sp name change
                //chetan added new sp for gettingInsurance
                //oDB.Retrive_Query(_strQuery, out dtInsurance);
                if (dtInsurance != null)
                {
                    return dtInsurance;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }

        }







        /// <summary>
        /// Method to get Insurance Information of Particular Patient.
        /// </summary>
        /// <param name="PatientID">ID of Patient of whose Insurance Info is to be fetched are to be fetched.</param>
        /// <returns></returns>
        public DataTable getPatientInsurances(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtInsurance = null;
            try
            {
                //string _strQuery = "SELECT PatientInsurance_DTL.nInsuranceID,isnull(Contacts_MST.sName,'')as InsuranceName ,ISNULL(sSubscriberID,'') AS sSubscriberID, ISNULL(sSubscriberName,'') AS sSubscriberName,ISNULL(sSubscriberPolicy#,'') As sSubscriberPolicy#, ISNULL(sGroup,'') as sGroup,PatientInsurance_DTL.sPhone ,bPrimaryFlag,PatientInsurance_DTL.dtDOB,PatientInsurance_DTL.dtEffectiveDate,PatientInsurance_DTL.dtExpiryDate,PatientInsurance_DTL.sSubscriberID FROM   Contacts_MST INNER JOIN PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nInsuranceID where nPatientID='" + PatientID + "'";
                // Changed on  20080926 

                //*********************************************************************************************
                //Code Changes on - 20081011 By - Sagar Ghodke
                //Below Commented Code is previous logic
                //Code Changed for not loading the Insurances which are inactive

                #region " Previous Code "
                //string _strQuery = "SELECT PatientInsurance_DTL.nInsuranceID,isnull(Contacts_MST.sName,'')as InsuranceName ,ISNULL(sSubscriberID,'') AS sSubscriberID, (isnull(sSubFName,'') + Space(1) + ISNULL(sSubMName,'') + Space(1) + ISNULL(sSubLName,'')) AS sSubscriberName, ISNULL(sSubscriberPolicy#,'') As sSubscriberPolicy#, ISNULL(sGroup,'') as sGroup, " +
                //    " PatientInsurance_DTL.sPhone ,bPrimaryFlag,PatientInsurance_DTL.dtDOB,PatientInsurance_DTL.dtEffectiveDate,PatientInsurance_DTL.dtExpiryDate,PatientInsurance_DTL.sSubscriberID, " +
                //    " isnull(sSubFName,'') As SubFName, ISNULL(sSubMName,'') AS SubMName, ISNULL(sSubLName,'') as SubLName, ISNULL(nRelationShipID,0) as RelationShipID , ISNULL(sRelationShip,'') AS RelationShip, ISNULL(nDeductableamount,0) as Deductableamount, ISNULL(nCoveragePercent,0) as CoveragePercent, ISNULL(nCoPay,0) as CoPay, ISNULL(bAssignmentofBenifit,0) as AssignmentofBenifit, dtStartDate, dtEndDate " +
                //    " FROM   Contacts_MST INNER JOIN PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nInsuranceID where nPatientID='" + PatientID + "'";
                #endregion " Previous Code "

                //string _strQuery = "SELECT PatientInsurance_DTL.nInsuranceID, ISNULL(Contacts_MST.sName, '') AS InsuranceName, ISNULL(PatientInsurance_DTL.sSubscriberID, '')  "+
                //      " AS sSubscriberID, ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) + ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1) "+
                //      " + ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName, ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, "+
                //      " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup, PatientInsurance_DTL.sPhone, PatientInsurance_DTL.bPrimaryFlag, "+
                //      " PatientInsurance_DTL.dtDOB, PatientInsurance_DTL.dtEffectiveDate, PatientInsurance_DTL.dtExpiryDate, "+
                //      " PatientInsurance_DTL.sSubscriberID AS Expr1, ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName, "+
                //      " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName, "+
                //      " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID, ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip, "+
                //      " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent, "+
                //      " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay, ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit, "+
                //      " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate, CASE isnull(PatientInsurance_DTL.nInsuranceFlag, 0)  "+
                //      " WHEN 0 THEN 4 ELSE PatientInsurance_DTL.nInsuranceFlag END AS nInsuranceFlag,  "+
                //      " PatientInsurance_DTL.sSubscriberGender, PatientInsurance_DTL.sPayerID ,ISNULL(Contacts_MST.sPayerID,'') AS PayerID, ISNULL(Patient.sCity,'') AS sCity, "+ 
                //      " ISNULL(Patient.sState,'') AS sState, ISNULL(Patient.sZIP,'') AS sZIP, ISNULL(Patient.sAddressLine1,'') AS sAddress1, ISNULL(Patient.sAddressLine2,'') AS sAddress2," +
                //      " ISNULL(Contacts_MST.sInsuranceTypeCode,'') AS InsuranceTypeCode, ISNULL(PatientRelationship.sRelationshipCode,'') AS RelationshipCode "+
                //      " FROM Contacts_MST INNER JOIN " +
                //      " PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nInsuranceID INNER JOIN "+
                //      " Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID INNER JOIN "+
                //      " PatientRelationship ON PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID where PatientInsurance_DTL.nPatientID='" + PatientID + "'  ORDER BY nInsuranceFlag";

                //string _strQuery = "SELECT  PatientInsurance_DTL.nInsuranceID, ISNULL(Contacts_MST.sName, '') AS InsuranceName, ISNULL(PatientInsurance_DTL.sSubscriberID, '') "
                //+ " AS sSubscriberID, ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) + ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)  "
                //+ " + ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName, ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#,  "
                //+ " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup, PatientInsurance_DTL.sPhone,PatientInsurance_DTL.bPrimaryFlag,  "
                //+ " PatientInsurance_DTL.dtDOB, PatientInsurance_DTL.dtEffectiveDate, PatientInsurance_DTL.dtExpiryDate,  "
                //+ " PatientInsurance_DTL.sSubscriberID AS Expr1, ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  "
                //+ " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,  "
                //+ " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID, ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  "
                //+ " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent, "
                //+ " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay, ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  "
                //+ " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate, CASE isnull(PatientInsurance_DTL.nInsuranceFlag, 0)  "
                //+ " WHEN 0 THEN 4 ELSE PatientInsurance_DTL.nInsuranceFlag END AS nInsuranceFlag, PatientInsurance_DTL.sSubscriberGender,  "
                //+ " PatientInsurance_DTL.sPayerID, ISNULL(Patient.sCity, '') AS sCity, ISNULL(Patient.sState, '') AS sState, ISNULL(Patient.sZIP, '') AS sZIP,  "
                //+ " ISNULL(Patient.sAddressLine1, '') AS sAddress1, ISNULL(Patient.sAddressLine2, '') AS sAddress2, ISNULL(PatientRelationship.sRelationshipCode, '')  "
                //+ " AS RelationshipCode, Contacts_MST.nContactID, ISNULL(Contacts_Insurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode,  "
                //+ " ISNULL(Contacts_Insurance_DTL.sPayerId, '') AS PayerID, "
                //+ " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'  "
                //+ " ELSE '' END  AS sInsuranceFlag "
                //+ " FROM Contacts_MST INNER JOIN PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nInsuranceID  "
                //+ " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  "
                //+ " INNER JOIN PatientRelationship ON PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  "
                //+ " LEFT OUTER JOIN Contacts_Insurance_DTL ON Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID" +
                //" where PatientInsurance_DTL.nPatientID='" + PatientID + "'  ORDER BY nInsuranceFlag";

                //string _strQuery =  " SELECT PatientInsurance_DTL.nInsuranceID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                //                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                //                    " PatientInsurance_DTL.sPhone, " +
                //                    " PatientInsurance_DTL.dtDOB,  " +
                //                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                //                    " PatientInsurance_DTL.dtExpiryDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                //                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                //                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                //                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                //                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                //                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                //                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                //                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                //                    " PatientInsurance_DTL.sSubscriberGender,  " +
                //                    " PatientInsurance_DTL.sPayerID,  " +
                //                    " ISNULL(Patient.sCity, '') AS sCity, " +
                //                    " ISNULL(Patient.sState, '') AS sState,  " +
                //                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                //                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                //                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                //                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                //                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                //                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                //                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                //                    " ELSE '' END  AS sInsuranceFlag  " +
                //                    " FROM PatientInsurance_DTL  " +
                //                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                //                    " INNER JOIN PatientRelationship ON  " +
                //                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                //                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY nInsuranceFlag ";

                //string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                //                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                //                    " PatientInsurance_DTL.sPhone, " +
                //                    " PatientInsurance_DTL.dtDOB,  " +
                //                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                //                    " PatientInsurance_DTL.dtExpiryDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                //                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                //                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                //                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                //                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                //                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                //                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                //                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                //                    " PatientInsurance_DTL.sSubscriberGender,  " +
                //                    " PatientInsurance_DTL.sPayerID,  " +
                //                    " ISNULL(Patient.sCity, '') AS sCity, " +
                //                    " ISNULL(Patient.sState, '') AS sState,  " +
                //                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                //                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                //                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                //                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                //                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                //                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                //                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                //                    " ELSE '' END  AS sInsuranceFlag,  " +

                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 4 " +
                //                    " ELSE nInsuranceFlag END  AS SortOrder  " +

                //                    " FROM PatientInsurance_DTL  " +
                //                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                //                    " LEFT OUTER JOIN PatientRelationship ON  " +
                //                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                //                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY SortOrder ";
                //Problem : 00000249
                //Reason : The insurance tab (in EMR) at the bottom of the dashboard no longer displays the Subscriber Name information.
                //Description : change made is ISNULL() is check in case statement as below at line number 5648 of _strQuery.
                //CASE WHEN ISNULL(PatientInsurance_DTL.bISCompnay, 0) = 0 THEN ISNULL(PatientInsurance_DTL.sSubFName, '') + ' ' + ISNULL(PatientInsurance_DTL.sSubMName, '') + ' ' + ISNULL(PatientInsurance_DTL.sSubLName, '') ELSE PatientInsurance_DTL.sCompanyName END AS sSubscriberName, 
                string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                                    " CASE WHEN ISNULL(PatientInsurance_DTL.bISCompnay, 0) = 0 THEN ISNULL(PatientInsurance_DTL.sSubFName, '') + ' ' + ISNULL(PatientInsurance_DTL.sSubMName, '') + ' ' + ISNULL(PatientInsurance_DTL.sSubLName, '') ELSE PatientInsurance_DTL.sCompanyName END AS sSubscriberName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                                    " PatientInsurance_DTL.sPhone, " +
                                    " PatientInsurance_DTL.dtDOB,  " +
                                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                                    " PatientInsurance_DTL.dtExpiryDate,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                                    " PatientInsurance_DTL.sSubscriberGender,  " +
                                    " PatientInsurance_DTL.sPayerID,  " +
                                    " ISNULL(Patient.sCity, '') AS sCity, " +
                                    " ISNULL(Patient.sState, '') AS sState,  " +
                                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                                    " ELSE '' END  AS sInsuranceFlag,  " +

                                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                                    " WHEN 0 THEN 4 " +
                                    " ELSE nInsuranceFlag END  AS SortOrder,  " +
                                    " dbo.formatPhone(PatientInsurance_DTL.sInsurancePhone,0), " +
                                    " ISNULL(PatientInsurance_DTL.bworkerscomp,0) AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim,0) AS bAutoClaim " +
                                    " FROM PatientInsurance_DTL  " +
                                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                                    " LEFT OUTER JOIN PatientRelationship ON  " +
                                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY SortOrder ";


                //End Code Changes - 20081011 By - Sagar Ghodke
                //*********************************************************************************************

                oDB.Retrive_Query(_strQuery, out dtInsurance);
                if (dtInsurance != null)
                {
                    return dtInsurance;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public DataTable getPatientInsurances(Int64 PatientID, int Primary_1_Secondary_2_Tertiary_3)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtInsurance = null;
            try
            {
                string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                                    " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " +
                                    " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " +
                                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                                    " PatientInsurance_DTL.sPhone, " +
                                    " PatientInsurance_DTL.dtDOB,  " +
                                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                                    " PatientInsurance_DTL.dtExpiryDate,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                                    " PatientInsurance_DTL.sSubscriberGender,  " +
                                    " PatientInsurance_DTL.sPayerID,  " +
                                    " ISNULL(Patient.sCity, '') AS sCity, " +
                                    " ISNULL(Patient.sState, '') AS sState,  " +
                                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                                    " ELSE '' END  AS sInsuranceFlag,  " +
                                    " ISNULL(nTypeOBilling,0) AS nTypeOBilling " +
                                    " FROM PatientInsurance_DTL  " +
                                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                                    " LEFT OUTER JOIN PatientRelationship ON  " +
                                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "' AND nInsuranceFlag = " + Primary_1_Secondary_2_Tertiary_3 + " ORDER BY nInsuranceFlag ";


                //End Code Changes - 20081011 By - Sagar Ghodke
                //*********************************************************************************************

                oDB.Retrive_Query(_strQuery, out dtInsurance);
                if (dtInsurance != null)
                {
                    return dtInsurance;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public DataTable GetPatientPriorAuthorization(Int64 PatientID, Int64 ClinicID, Int64 AsOfDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for Quick Review Panel.
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@AsOfDate", AsOfDate, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("GET_PriorAuthorization_Revised", oParameters, out dt);
                return dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                //   dt.Dispose();
            }
        }

        public DataTable GetPatientCases(Int64 PatientID, Int64 ClinicID, Int64 AsOfDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtCase = null;
            //query replace by storedprocedure for dashboard performance issue
            try
            {
                //String _strQuery = "SELECT Patient_Cases_MST.nCaseId, sCaseName,"+
                //                    " Case when Patient_Cases_MST.nAccidentType =1 then 'Work' "+
                //                    " when Patient_Cases_MST.nAccidentType =2 then 'Auto' "+
                //                    " when Patient_Cases_MST.nAccidentType =3 then 'Other' "+
                //                    " else 'None'  end  AccidentType,"+
                //                    "sWCNumber,dtStartdate,dtEnddate," +
                //                    " dbo.GetCaesDiagnoses(Patient_Cases_MST.nCaseId) as Diag,dbo.GetCasesFacility(Patient_Cases_MST.nCaseId) as sFacilityDescription,(Select sPriorAuthorizationNo FROM PriorAuthorization_Mst WHERE nPriorAuthorizationID =Patient_Cases_MST.nAuthorizationID AND bIsActive=1) AS sAuthorizationNumber, " +
                //                    " (SELECT "+       
                //                    "ISNULL(Contacts_MST.sFirstName,'')  +space(1)+"+        
                //                    "CASE ISNULL(Contacts_MST.sMiddleName,'') WHEN '' THEN '' WHEN Contacts_MST.sMiddleName THEN Contacts_MST.sMiddleName +SPACE(1) END"+
                //                    "+ISNULL(Contacts_MST.sLastName,'') AS sReferralName " +
                //                    "FROM Contacts_MST WHERE nContactID=Patient_Cases_MST.nReferralID) as Referring , " +
                //                   "  Case WHEN Cases_ReportingCategory.bIsblocked=1 THEN '' ELSE Cases_ReportingCategory.sCode END, " +
                //                    "sNote  " +
                //                    " FROM Patient_Cases_MST LEFT OUTER JOIN Cases_ReportingCategory "+
                //                    " on Patient_Cases_MST.nReportCategoryId =Cases_ReportingCategory.nID "+
                //                   " where  Patient_Cases_MST.nPatientId= " + PatientID + "order by dtStartdate  desc";
                oDB.Connect(false);
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_getCases_Dashboard", oParameters, out dtCase);
                oParameters.Clear();


                return dtCase;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oDB = null;
                oParameters = null;
                //if (dtCase != null)
                //{
                //    dtCase.Dispose();
                //    dtCase = null; 
                //}
            }
        }



        //MaheshB 20091112
        public DataTable getTransactionInsurances(Int64 _nTransID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtInsurance = null;
            try
            {
                string _strQuery = "SELECT DISTINCT BL_Transaction_InsPlan.nResponsibilityNo, " +
                      " PatientInsurance_DTL.nInsuranceID AS Expr1, ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberID, '') AS sSubscriberID, ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) " +
                      " + ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1) + ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup, " +
                      " PatientInsurance_DTL.sPhone, PatientInsurance_DTL.dtDOB, PatientInsurance_DTL.dtEffectiveDate, PatientInsurance_DTL.dtExpiryDate, " +
                      " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName, ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                      " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName, ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID, " +
                      " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip, ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                      " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent, ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay, " +
                      "ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit, PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate, " +
                      " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, PatientInsurance_DTL.sSubscriberGender, PatientInsurance_DTL.sPayerID, " +
                      " ISNULL(Patient.sCity, '') AS sCity, ISNULL(Patient.sState, '') AS sState, ISNULL(Patient.sZIP, '') AS sZIP, ISNULL(Patient.sAddressLine1, '') " +
                      " AS sAddress1, ISNULL(Patient.sAddressLine2, '') AS sAddress2, ISNULL(PatientRelationship.sRelationshipCode, '') AS RelationshipCode, " +
                      " ISNULL(PatientInsurance_DTL.nContactID, 0) AS nContactID, ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                      " ISNULL(PatientInsurance_DTL.sPayerID, '') AS PayerID, ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2, ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState, ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip, " +
                      " ISNULL(PatientInsurance_DTL.sZIP, '') AS PayerZip, ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity, ISNULL(PatientInsurance_DTL.sState, '') " +
                      " AS PayerState, ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1,PatientInsurance_DTL.sInsurancePhone, ISNULL(PatientInsurance_DTL.bworkerscomp, 0) " +
                      " AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim, 0) AS bAutoClaim, BL_Transaction_InsPlan.nTransactionID, " +
                      " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, CASE ISNULL(BL_Transaction_InsPlan.nResponsibilityNo, 0) " +
                      " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary' ELSE '' END AS sInsuranceFlag," +
                      " BL_Transaction_InsPlan.nInsuranceID,IsNull(PatientInsurance_DTL.bAssignmentofBenifit,0) as bAssignmentofBenifit,IsNull(PatientInsurance_DTL.sInsTypeCodeDefault,0) as InsTypeCodeDefault," +
                      " IsNull(PatientInsurance_DTL.sInsTypeCodeMedicare,0) as InsTypeCodeMedicare" +
                      " FROM                  PatientInsurance_DTL INNER JOIN " +
                      " Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID INNER JOIN " +
                     "  BL_Transaction_InsPlan ON PatientInsurance_DTL.nInsuranceID = BL_Transaction_InsPlan.nInsuranceID " +
                     " And PatientInsurance_DTL.nPatientID = BL_Transaction_InsPlan.nPatientID LEFT OUTER JOIN " +
                     "  PatientRelationship ON PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID " +
                      "   WHERE                 (BL_Transaction_InsPlan.nTransactionID = '" + _nTransID + "' ) and  nResponsibilityNo<>0 and BL_Transaction_InsPlan.nInsuranceID <>0 order by BL_Transaction_InsPlan.nResponsibilityNo";

                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtInsurance);
                if (dtInsurance != null)
                {
                    return dtInsurance;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }


        public DataTable getTransactionInsurances(Int64 _nTransMasterID, Int16 _nResponsibilityNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtInsurance = null;
            try
            {
                string _strQuery = "SELECT DISTINCT BL_Transaction_InsPlan.nResponsibilityNo, " +
                      " PatientInsurance_DTL.nInsuranceID AS Expr1, ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberID, '') AS sSubscriberID, ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) " +
                      " + ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1) + ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup, " +
                      " PatientInsurance_DTL.sPhone, PatientInsurance_DTL.dtDOB, PatientInsurance_DTL.dtEffectiveDate, PatientInsurance_DTL.dtExpiryDate, " +
                      " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName, ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                      " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName, ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID, " +
                      " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip, ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                      " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent, ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay, " +
                      "ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit, PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate, " +
                      " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, PatientInsurance_DTL.sSubscriberGender, PatientInsurance_DTL.sPayerID, " +
                      " ISNULL(Patient.sCity, '') AS sCity, ISNULL(Patient.sState, '') AS sState, ISNULL(Patient.sZIP, '') AS sZIP, ISNULL(Patient.sAddressLine1, '') " +
                      " AS sAddress1, ISNULL(Patient.sAddressLine2, '') AS sAddress2, ISNULL(PatientRelationship.sRelationshipCode, '') AS RelationshipCode, " +
                      " ISNULL(PatientInsurance_DTL.nContactID, 0) AS nContactID, ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                      " ISNULL(PatientInsurance_DTL.sPayerID, '') AS PayerID, ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2, ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity, " +
                      " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState, ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,case upper(sSubscriberCountry) when 'CANADA' then 'CA' else '' end as SubscriberCountryCode, " +
                      " ISNULL(PatientInsurance_DTL.sZIP, '') AS PayerZip, ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity, ISNULL(PatientInsurance_DTL.sState, '') " +
                      " AS PayerState, ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1,PatientInsurance_DTL.sInsurancePhone, ISNULL(PatientInsurance_DTL.bworkerscomp, 0) " +
                      " AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim, 0) AS bAutoClaim, BL_Transaction_InsPlan.nTransactionID, " +
                      " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, CASE ISNULL(BL_Transaction_InsPlan.nResponsibilityNo, 0) " +
                      " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary' ELSE '' END AS sInsuranceFlag," +
                      " BL_Transaction_InsPlan.nInsuranceID,IsNull(PatientInsurance_DTL.bAssignmentofBenifit,0) as bAssignmentofBenifit,IsNull(PatientInsurance_DTL.sInsTypeCodeDefault,0) as InsTypeCodeDefault," +
                      " IsNull(PatientInsurance_DTL.sInsTypeCodeMedicare,0) as InsTypeCodeMedicare," +
                      "(case when BL_Transaction_InsPlan.nResponsibilityNo = " + _nResponsibilityNo + " then 0 else nResponsibilityNo end) as ResponsibilityNo," +
                    //"IsNull(BL_Transaction_InsPlan.sClaimRemittanceRefNo,'') as sClaimRemittanceRefNo "+
                      " dbo.GET_ClaimRemittanceRef(BL_Transaction_InsPlan.nTransactionID,BL_Transaction_InsPlan.nContactID,BL_Transaction_InsPlan.nInsuranceID) as sClaimRemittanceRefNo, " +
                      " ISNULL(Contacts_Insurance_DTL.bAccessAssignment,0) AS bAccessAssignment, ISNULL(Contacts_Insurance_DTL.sClaimOfficeNumber,'') AS sClaimOfficeNumber, ISNULL(Contacts_Insurance_DTL.bIncludeSubscriberAddress,0) as bIncludeSubscriberAddress ," +
                       " ISNULL(Contacts_Insurance_DTL.bIncludePlanname,0) AS bIncludePlanname " +
                      " FROM                  PatientInsurance_DTL INNER JOIN " +
                      " Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID INNER JOIN " +
                     "  BL_Transaction_InsPlan ON PatientInsurance_DTL.nInsuranceID = BL_Transaction_InsPlan.nInsuranceID " +
                     " And PatientInsurance_DTL.nPatientID = BL_Transaction_InsPlan.nPatientID LEFT OUTER JOIN " +
                     "  PatientRelationship ON PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID " +
                     " LEFT OUTER JOIN dbo.Contacts_Insurance_DTL ON PatientInsurance_DTL.nContactID=Contacts_Insurance_DTL.nContactID " +
                      "   WHERE                 (BL_Transaction_InsPlan.nTransactionID = '" + _nTransMasterID + "' ) and  nResponsibilityNo<>0 and BL_Transaction_InsPlan.nInsuranceID <>0 order by ResponsibilityNo,BL_Transaction_InsPlan.nResponsibilityNo";

                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtInsurance);
                if (dtInsurance != null)
                {
                    return dtInsurance;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {

                MessageBox.Show("Error - " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        /// <summary>
        /// Method to get Appointments of requested Patient.
        /// </summary>
        /// <param name="PatientID">PatientID of the Patient whose Appointments are to be fetched. </param>
        /// <returns>DataTable containing the Appointments information </returns>
        public DataTable getPatientAppointments(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            DataTable dt_Appointment = null;

            try
            {
                oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetPatientAppointments", oParameters, out dt_Appointment);
                if (dt_Appointment != null)
                {
                    return dt_Appointment;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("ERROR - " + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }

        }

        public DataTable getPatientUpComingAppointments(Int64 PatientID, Int64 FromDate, Int64 ProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "";
                if (ProviderID > 0)
                {
                    _strSQL = "SELECT DISTINCT	ISNULL(AS_Appointment_DTL.nAppointmentID, 0) AS AppointmentID, " +
                          " ISNULL(Provider_MST.sFirstName, '') + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS ProviderName, " +
                          " ISNULL(AB_Location.sLocation, '') AS ApptLocation, ISNULL(AB_Department.sDepartment, '') AS ApptDept, " +
                          " ISNULL(AS_Appointment_DTL.nStartDate, 0) AS StartDate, ISNULL(AS_Appointment_DTL.nStartTime, 0) AS StartTime, " +
                          " ISNULL(AS_Appointment_DTL.nEndTime, 0) AS EndTime, ISNULL(AS_Appointment_DTL.sNotes, '') AS Notes " +
                          " FROM AB_Location INNER JOIN AS_Appointment_DTL INNER JOIN Patient ON AS_Appointment_DTL.nPatientID = Patient.nPatientID ON AB_Location.nLocationID = AS_Appointment_DTL.nLocationID INNER JOIN Provider_MST INNER JOIN AS_Appointment_DTL_Providers ON Provider_MST.nProviderID = AS_Appointment_DTL_Providers.nProviderID ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Providers.nMasterAppointmentID AND AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Providers.nAppointmentID LEFT OUTER JOIN AB_Department ON AS_Appointment_DTL.nDepartmentID = AB_Department.nDepartmentID " +
                          " WHERE Patient.nExemptFromReport = 0  AND Patient.nPatientID  = " + PatientID + " AND AS_Appointment_DTL.nStartDate >= " + FromDate + " AND AS_Appointment_DTL_Providers.nProviderID = " + ProviderID + "";
                }
                else
                {
                    _strSQL = "SELECT DISTINCT	ISNULL(AS_Appointment_DTL.nAppointmentID, 0) AS AppointmentID, " +
                          " ISNULL(Provider_MST.sFirstName, '') + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS ProviderName, " +
                          " ISNULL(AB_Location.sLocation, '') AS ApptLocation, ISNULL(AB_Department.sDepartment, '') AS ApptDept, " +
                          " ISNULL(AS_Appointment_DTL.nStartDate, 0) AS StartDate, ISNULL(AS_Appointment_DTL.nStartTime, 0) AS StartTime, " +
                          " ISNULL(AS_Appointment_DTL.nEndTime, 0) AS EndTime, ISNULL(AS_Appointment_DTL.sNotes, '') AS Notes " +
                          " FROM AB_Location INNER JOIN AS_Appointment_DTL INNER JOIN Patient ON AS_Appointment_DTL.nPatientID = Patient.nPatientID ON AB_Location.nLocationID = AS_Appointment_DTL.nLocationID INNER JOIN Provider_MST INNER JOIN AS_Appointment_DTL_Providers ON Provider_MST.nProviderID = AS_Appointment_DTL_Providers.nProviderID ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Providers.nMasterAppointmentID AND AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Providers.nAppointmentID LEFT OUTER JOIN AB_Department ON AS_Appointment_DTL.nDepartmentID = AB_Department.nDepartmentID " +
                          " WHERE Patient.nExemptFromReport = 0  AND Patient.nPatientID  = " + PatientID + " AND AS_Appointment_DTL.nStartDate >= " + FromDate + " ";
                }

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public DataTable getPatientUpComingAppointment_Providers(Int64 PatientID, Int64 FromDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "SELECT DISTINCT	ISNULL(Provider_MST.nProviderID,0) AS nProviderID, " +
                                 " ISNULL(Provider_MST.sFirstName, '') + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS ProviderName " +
                                 " FROM AB_Location INNER JOIN AS_Appointment_DTL INNER JOIN Patient ON AS_Appointment_DTL.nPatientID = Patient.nPatientID ON AB_Location.nLocationID = AS_Appointment_DTL.nLocationID INNER JOIN Provider_MST INNER JOIN AS_Appointment_DTL_Providers ON Provider_MST.nProviderID = AS_Appointment_DTL_Providers.nProviderID ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Providers.nMasterAppointmentID AND AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Providers.nAppointmentID LEFT OUTER JOIN AB_Department ON AS_Appointment_DTL.nDepartmentID = AB_Department.nDepartmentID " +
                                 " WHERE dbo.Patient.nExemptFromReport = 0  AND dbo.Patient.nPatientID = " + PatientID + "  AND AS_Appointment_DTL.nStartDate >= " + FromDate + " AND Provider_MST.nProviderID <> 0";

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public DataTable getPatientAppointmentStatus(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "";

                if (PatientID > 0)
                {
                    _strSQL = "SELECT nAppointmentStatusID,sAppointmentStatus FROM AB_AppointmentStatus WHERE bIsBlocked = 0";
                }
                else
                {
                    _strSQL = "SELECT nAppointmentStatusID,sAppointmentStatus FROM AB_AppointmentStatus WHERE bIsBlocked = 0";
                }


                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public DataTable getPatientProcedure_Lines(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "";
                _strSQL = "SELECT BL_Transaction.nPatientID, BL_TransactionLines.nLineNo, BL_TransactionLines.nTransactionID," +
                          " SUBSTRING(CONVERT(varchar,BL_TransactionLines.nStartDate), 5, 2) + '/' + SUBSTRING(CONVERT(varchar, BL_TransactionLines.nStartDate), 7, 2) + '/' + SUBSTRING(CONVERT(varchar, BL_TransactionLines.nStartDate), 1, 4) AS StartDate, " +
                          " SUBSTRING(CONVERT(varchar,BL_TransactionLines.nEndDate), 5, 2) + '/' + SUBSTRING(CONVERT(varchar, BL_TransactionLines.nEndDate), 7, 2) + '/' + SUBSTRING(CONVERT(varchar, BL_TransactionLines.nEndDate), 1, 4) AS EndDate, " +
                          " BL_TransactionLines.nBillingProviderID,BL_TransactionLines.nLocationID, " +
                          " BL_TransactionLines.nIsSendToBill, BL_TransactionLines.nIsBilled, BL_TransactionLines.nClinicID, " +
                          " ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') AS ProviderName, AB_Location.sLocation " +
                          " FROM BL_Transaction INNER JOIN BL_TransactionLines ON BL_Transaction.nTransactionID = BL_TransactionLines.nTransactionID INNER JOIN Provider_MST ON BL_TransactionLines.nBillingProviderID = Provider_MST.nProviderID INNER JOIN AB_Location ON BL_TransactionLines.nLocationID = AB_Location.nLocationID " +
                          " WHERE (BL_Transaction.nPatientID = " + PatientID + ") ORDER BY BL_TransactionLines.nStartDate, BL_TransactionLines.nLineNo";

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public DataTable getPatientProcedure_Lines_CPTs(Int64 TransactionID, Int64 TransactionLineID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "";
                _strSQL = "SELECT nTransactionID, nLineNo, nColumnNo, sProcedureCode, sProcedureDescription, nClinicID " +
                          " FROM BL_TransactionLine_CPT WHERE (nTransactionID = " + TransactionID + ") AND (nLineNo  = " + TransactionLineID + ")";

                oDB.Retrive_Query(_strSQL, out  dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public DataTable getPatientProcedure_Lines_CPT_ICD9s(Int64 TransactionID, Int64 TransactionLineID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "";
                _strSQL = "SELECT  nTransactionID, nLineNo, nColumnNo, sICD9Code, sICD9Description, nClinicID " +
                          " FROM BL_TransactionLine_CPT_ICD9s WHERE (nTransactionID = " + TransactionID + ") AND (nLineNo = " + TransactionLineID + ")";

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public DataTable getPatientProcedure_Lines_CPT_Modifiers(Int64 TransactionID, Int64 TransactionLineID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "";
                _strSQL = "SELECT nTransactionID, nLineNo, nColumnNo, sModifierCode, sModifierDescription, nClinicID " +
                          " FROM BL_TransactionLine_CPT_Modifiers WHERE (nTransactionID = " + TransactionID + ") AND (nLineNo = " + TransactionLineID + ")";

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public DataTable getPatientProcedure_Lines_CPT_Payment(Int64 TransactionID, Int64 TransactionLineID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "";
                _strSQL = "SELECT nTransactionID, nLineNo, nColumnNo, dRateAmount, dUnitQuantity, dSubTotalAmount, dTotalAmount, nClinicID " +
                          " FROM BL_TransactionLine_Payment WHERE nTransactionID = " + TransactionID + " AND nLineNo = " + TransactionLineID + "";

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public DataTable getPatientProcedure_Lines_CPT_Payment_Taxes(Int64 TransactionID, Int64 TransactionLineID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_Data = null;
            try
            {
                string _strSQL = "";
                _strSQL = "SELECT nTransactionID, nLineNo, nColumnNo, dTaxID, nTaxType, dTaxName, dTaxPercentage, dTaxAmount, nClinicID " +
                          " FROM BL_TransactionLine_Payment_Taxes WHERE nTransactionID = " + TransactionID + " AND nLineNo = " + TransactionLineID + "";

                oDB.Retrive_Query(_strSQL, out dt_Data);

                if (dt_Data != null)
                {
                    return dt_Data;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; ;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }


        #endregion "Methods to get Patients Insurances,Appointments & Reffrals"

        //Patient Related Enhancement for Billing
        public DataTable GetPatientGroups()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dtPatientGroups = null;

            try
            {
                oDB.Connect(false);
                // strQuery = "select * from PatientGroup_BL_MST ";
                strQuery = "select  nPatientGroupID, sDescription  from PatientGroup_BL_MST ";
                oDB.Retrive_Query(strQuery, out dtPatientGroups);
                return dtPatientGroups;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                //  dtPatientGroups.Dispose();
            }
        }


        public void AddPatientChangeHistory(PatientDemographics oPatientHistory, bool IsDOBModified,string sPatientInboundHospital="",string sPatientInboundTranCare="")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                //@PatientID, @dtChangeDateTime, @FirstName, @MiddleName, @LastName, @dtDOB, @Gender, @AddressLine1, @AddressLine2,
                //@City, @State, @ZIP, @County, @Phone , @sLoginName

                odbParams.Add("@PatientID", oPatientHistory.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@dtChangeDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                odbParams.Add("@FirstName", oPatientHistory.PatientFirstName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@MiddleName", oPatientHistory.PatientMiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@LastName", oPatientHistory.PatientLastName, ParameterDirection.Input, SqlDbType.VarChar);

                if (IsDOBModified == true)
                    odbParams.Add("@dtDOB", oPatientHistory.PatientDOB, ParameterDirection.Input, SqlDbType.DateTime);
                else
                    odbParams.Add("@dtDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);

                odbParams.Add("@Gender", oPatientHistory.PatientGender, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@AddressLine1", oPatientHistory.PatientAddress1, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@AddressLine2", oPatientHistory.PatientAddress2, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@City", oPatientHistory.PatientCity, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@State", oPatientHistory.PatientState, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@ZIP", oPatientHistory.PatientZip, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@County", oPatientHistory.PatientCounty, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@Phone", oPatientHistory.PatientPhone, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sLoginName", _username, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sLanguage", oPatientHistory.PatientLanguage, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sInboundHospital", sPatientInboundHospital, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sInboundtranCare", sPatientInboundTranCare, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Execute("gsp_Insert_PatientChangeHistory", odbParams);

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }
        }

        public DataTable GetPatientChangeHistory(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for Quick Review Panel.
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_viewPatientHistory", oParameters, out dt);
                return dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                //  dt.Dispose();
            }
        }
        public DataTable GetPrefix()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dtPrefix = null;

            try
            {
                oDB.Connect(false);
                String strSQLServerName = appSettings["SQLServerName"].ToString();
                String strDatabaseName = appSettings["DatabaseName"].ToString();

                strQuery = "select nPrefixID,sServer,sDatabase,sPrefix from Prefix where sServer='" + strSQLServerName.Replace("'", "''").ToString() + "' and " + "sDatabase='" + strDatabaseName.Replace("'", "''") + "'";
                oDB.Retrive_Query(strQuery, out dtPrefix);
                return dtPrefix;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                //  dtPrefix.Dispose();
            }
        }
        public string GeneratePatientCode()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = new object();
            _result = "12345";
            string _Prefix = "";
            Int32 _Increment = 1;
            try
            {
                //code commented by Mayuri:20091229-PatientCodePrefix setting is now removed from Admin-Issue ID:#4837
                //so, if AutoGeneratecode setting is true then autoincremented code is displayed
                //ogloSettings.GetSetting("PatientCodePrefix", out value);
                //if (value != null)
                //{
                //    _Prefix = Convert.ToString(value);
                //}
                //value = null;
                //End code commented by Mayuri:20091229
                object oResult;
                //gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                ogloSettings.GetSetting("UseSitePrefix", out oResult);
                _UseSitePrefix = 0;
                if (oResult != null && oResult.ToString() != "")
                {
                    _UseSitePrefix = Convert.ToInt32(oResult);
                }
                if (_UseSitePrefix != 0)
                {
                    if (appSettings["PatientPrefix"] != null)
                    {
                        if (appSettings["PatientPrefix"] != "")
                        {
                            _Prefix = Convert.ToString(appSettings["PatientPrefix"]);
                            _result = _Prefix + "12345";
                        }
                    }
                    if (_Prefix.Trim() == "")
                    {
                        DataTable dtPrefix = null;
                        dtPrefix = GetPrefix();
                        if (dtPrefix != null)
                        {
                            if (dtPrefix.Rows.Count > 0)
                            {
                                _Prefix = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);
                                _result = _Prefix + "12345";
                                appSettings["PatientPrefix"] = Convert.ToString(dtPrefix.Rows[0]["sPreFix"]);

                            }
                        }
                        if (dtPrefix != null)
                        {
                            dtPrefix.Dispose();
                        }
                    }
                }
                else
                {
                    _Prefix = "";
                    _result = _Prefix + "12345";
                }


                ogloSettings.GetSetting("PatientCodeIncrement", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _Increment = Convert.ToInt32(value);
                }
                value = null;
                //value = 
                ogloSettings.GetSetting("SequentialPatientCode", out value);

                if (value != null && Convert.ToString(value) != "")
                {
                    long SequentialPatientCode = _Increment + Convert.ToInt64(value);
                    ogloSettings.AddSetting("SequentialPatientCode", SequentialPatientCode.ToString(), _ClinicID, 0, gloSettings.SettingFlag.Clinic);

                }
                else
                {
                    oDB.Connect(false);


                    string strQuery = "SELECT ISNULL(MAX(convert(numeric,substring(sPatientCode, " + (_Prefix.Length + 1) + ",len(sPatientCode)- " + _Prefix.Length + "))),0) AS PatientCodeValue "
                                    + " FROM Patient WHERE substring(sPatientCode,1," + _Prefix.Length + ") = '" + _Prefix + "' AND  isnumeric(substring(sPatientCode, " + (_Prefix.Length + 1) + ",len(sPatientCode)- " + _Prefix.Length + ")) > 0 AND ISNULL(nClinicID,1) = " + _ClinicID + " AND  sPatientCode <> '.'";
                    //string strQuery = "SELECT ISNULL(MAX(convert(numeric,substring(dbo.Left_ParseNumber(sPatientCode), " + (_Prefix.Length + 1) + ",len(dbo.Left_ParseNumber(sPatientCode))- " + _Prefix.Length + "))),0) AS PatientCodeValue "
                    //                + " FROM Patient WHERE substring(dbo.Left_ParseNumber(sPatientCode),1," + _Prefix.Length + ") = '" + _Prefix + "' AND  isnumeric(substring(dbo.Left_ParseNumber(sPatientCode), " + (_Prefix.Length + 1) + ",len(dbo.Left_ParseNumber(sPatientCode))- " + _Prefix.Length + ")) > 0 AND ISNULL(nClinicID,1) = " + _ClinicID + " AND  dbo.Left_ParseNumber(sPatientCode) <> '.'";
                    string sqlQueryError = "";
                    value = oDB.ExecuteScalar_Query(strQuery, out sqlQueryError);
                    //value = Convert.ToInt64(value);
                    if (Convert.ToString(value).Trim() == "0")
                    {
                        value = 1000;
                    }
                    else
                    {
                        value = Convert.ToInt64(value) + 1;
                    }
                    long SequentialPatientCode = _Increment + Convert.ToInt64(value);
                    ogloSettings.AddSetting("SequentialPatientCode", SequentialPatientCode.ToString(), _ClinicID, 0, gloSettings.SettingFlag.Clinic);


                }


                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _result = _Prefix + Convert.ToString(Convert.ToInt64(value));

                }
                else
                {
                    _result = _Prefix + "12345";
                }
            }
            catch (gloDatabaseLayer.DBException)// dbEx)
            {
                //dbEx.ToString();
                //dbEx = null;
                _result = _Prefix + "12345";
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                _result = _Prefix + "12345";
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (ogloSettings != null)
                {
                    ogloSettings.Dispose();
                    ogloSettings = null;
                }
            }
            return _result;

        }

        public static void GetWindowTitle(Form oForm, Int64 nPatientID, String _databaseconnectionstring, String _messageBoxCaption)
        {
            String PatientCode = "";

            String PatientName = "";
            DataTable dtPatient = null;
            String PatientDOB = "";
            //    String PatientAge = "";

            String PatientGender = "";
            String PatientMaritalStatus = "";

            try
            {
                dtPatient = GetPatientInfo(nPatientID, _databaseconnectionstring, _messageBoxCaption);
                if ((dtPatient == null) == false)
                {
                    if (dtPatient.Rows.Count > 0)
                    {
                        PatientCode = Convert.ToString(dtPatient.Rows[0]["sPatientCode"]);
                        PatientName = Convert.ToString(dtPatient.Rows[0]["PatientName"]);
                        PatientDOB = Convert.ToString(dtPatient.Rows[0]["dtDOB"]);
                        PatientGender = Convert.ToString(dtPatient.Rows[0]["sGender"]);
                        PatientMaritalStatus = Convert.ToString(dtPatient.Rows[0]["sMaritalStatus"]);

                        oForm.Text = oForm.Text + " - " + PatientName + "( " + PatientCode + " )";

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if ((dtPatient == null) == false)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }

                PatientCode = null;
                PatientName = null;
                PatientDOB = null;
                PatientGender = null;
                PatientMaritalStatus = null;
            }


        }

        public static DataTable GetPatientInfo(Int64 patientid, String _databaseconnectionstring, String _messageBoxCaption)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                //Get the Patient Demographic Details for dashboard.
                oParameters.Add("@PatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_getPatientName", oParameters, out dt);
                return dt;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                //Change made to solve memory Leak and word crash issue
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                //if (dt != null)
                //{
                //    dt.Dispose();
                //    dt = null;
                //}

            }


        }

        public DataTable GetPatientProvider(Int64 PatientId, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                if (PatientId > 0 && ClinicId > 0)  //Warning Removed at the time of Change made to solve memory Leak and word crash issue
                {
                    //Bug #93369: 00001074: setup Appointment
                    _sqlQuery = " SELECT ISNULL(Patient.nProviderID, 0) AS nProviderID, " +
                                " (ISNULL(Provider_Mst.sFirstName,'') + space(1)  + CASE Provider_MST.sMiddleName   WHEN  '' THEN '' When Provider_MST.sMiddleName then  Provider_MST. sMiddleName + SPACE(1) END +ISNULL(Provider_MST.sLastName,'')) AS ProviderName" +
                                " FROM Patient LEFT OUTER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID " +
                                " WHERE (Patient.nPatientID = " + PatientId + ") AND  (Patient.nClinicID = " + ClinicId + " ) ";
                    oDB.Retrive_Query(_sqlQuery, out dt);
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }

            }
            return dt;
        }

        public DataTable GetPatientProvider(Int64 PatientId, Int64 ClinicId, Int64 EMRProviderId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                if (EMRProviderId > 0)
                {
                    if (PatientId > 0 && ClinicId > 0) //Warning Removed at the time of Change made to solve memory Leak and word crash issue
                    {
                        _sqlQuery = " SELECT ISNULL(Provider_MST.nProviderID, 0) AS nProviderID, " +
                                    " (ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +ISNULL(Provider_MST.sMiddleName,'')+ " +
                                    " SPACE(1) +ISNULL(Provider_MST.sLastName,'')) AS ProviderName  " +
                                    " FROM Provider_MST " +
                                    " WHERE Provider_MST.nProviderID = " + EMRProviderId + "";
                        oDB.Retrive_Query(_sqlQuery, out dt);
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }

            }
            return dt;
        }


        public DataTable GetDuplicatePatients()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;

            try
            {
                oDB.Connect(false);

                string _sqlQuery = " Select nPatientID as PatientID, "
                + " sPatientCode + ' - ' + isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'') as PatientName from Patient "
                + " where isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'') in  "
                + " (select isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'') "
                + " from Patient group by sFirstName,sMiddleName,sLastName   "
                + " having count(isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'') )>1)  "
                + " Order By PatientName ";

                oDB.Retrive_Query(_sqlQuery, out dt);
                return dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                // dt.Dispose();
            }
        }

        public DataTable GetPatientsToMerge()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;

            try
            {
                oDB.Connect(false);

                string _sqlQuery = " Select nPatientID as PatientID, "
                + " sPatientCode + ' - ' + isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'') as PatientName from Patient "
                + " Order By PatientName ";

                oDB.Retrive_Query(_sqlQuery, out dt);
                return dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                //dt.Dispose();
            }
        }

        internal DataTable GetDuplicatePatients(long PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;

            try
            {
                oDB.Connect(false);

                string _sqlQuery = "Select distinct nPatientID as PatientID, "
                + " sPatientCode + ' - ' + isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'') as PatientName "
                + " from Patient "
                + " where isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'')  = "
                + " 		("
                + " 			SELECT isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'') "
                + " 			FROM Patient "
                + " 			WHERE nPatientID = " + PatientID + ""
                + " 		)"
                + " Order By PatientName ";

                oDB.Retrive_Query(_sqlQuery, out dt);
                return dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                //dt.Dispose();
            }
        }

        public bool Merge_Patients(Int64 PatientId, string PatientCode, Int64 MergeInPatientID, string MergeInPatientCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            bool _result = false;
            try
            {
                oDB.Connect(false);

                odbParams.Add("@ActivityCategory", "Merge", ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@Description", "", ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@PatientID", PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@PatientCode", PatientCode, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@MergeInPatientID", MergeInPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@MergeInPatientCode", MergeInPatientCode, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@UserName", _username, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@MachineName", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Execute("gsp_MergePatient", odbParams);
                _result = true;

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }

            return _result;

            //return true;
        }
        //Added by Mayuri:20100206-To delet merge patient
        public bool DeleteData(Int64 PatientId, Int64 DestPatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            bool _result = false;
            try
            {
                oDB.Connect(false);
                odbParams.Add("@nPatientId", PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@nDestPatientId", DestPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("gsp_DeletePatient", odbParams);
                _result = true;

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }
            return _result;

            //return true;
        }

        //

        internal bool IsPatientExists(string FirstName, string LastName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;
            try
            {
                string _sqlQuery = "select count(*) from patient where RTRIM(LTRIM(UPPER(sfirstname))) = '" + FirstName.Trim().ToUpper() + "' AND RTRIM(LTRIM(UPPER(slastname))) = '" + LastName.Trim().ToUpper() + "' ";

                oDB.Connect(false);
                object oInnerResult = new object();
                oInnerResult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (oInnerResult != null && Convert.ToString(oInnerResult) != "")
                {
                    if (Convert.ToInt32(oInnerResult) > 0)
                    {
                        _result = true;
                    }
                }

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result;
        }
        ////Added on 20100203-To check whether patient code alresdy exists or not
        internal bool IsPatientCodeExists(string PatientCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;
            try
            {
                string _sqlQuery = "select count(sPatientCode) from patient where RTRIM(LTRIM(UPPER(sPatientCode))) = '" + PatientCode.Trim().ToUpper() + "'";

                oDB.Connect(false);
                object oInnerResult = new object();
                oInnerResult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (oInnerResult != null && Convert.ToString(oInnerResult) != "")
                {
                    if (Convert.ToInt32(oInnerResult) > 0)
                    {
                        _result = true;
                    }
                }

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result;
        }
        //


        ////Added on 20100224-To check "AUTO-GENERATE PATIENT CODE" setting is turn on
        internal bool IsAutoGeneratePatientCode()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;
            try
            {
                string _sqlQuery = "select ISNULL(sSettingsValue,0) as sSettingsValue from Settings where UPPER(sSettingsName) = 'AUTO-GENERATE PATIENT CODE'";

                oDB.Connect(false);
                object oInnerResult = new object();
                oInnerResult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (oInnerResult != null && Convert.ToString(oInnerResult) != "")
                {
                    if (Convert.ToInt32(oInnerResult) > 0)
                    {
                        _result = true;
                    }
                }

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result;
        }
        //


        //for checking same name and date of birth...
        internal bool IsPatientExists(string FirstName, string LastName, DateTime DOB, Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;
            try
            {
                string _sqlQuery = "select count(*) from patient where RTRIM(LTRIM(UPPER(sfirstname))) = '" + FirstName.Replace("'", "''").Trim().ToUpper() + "' AND RTRIM(LTRIM(UPPER(slastname))) = '" + LastName.Replace("'", "''").Trim().ToUpper() + "' AND dtdob =  '" + DOB + "'AND nPatientID<>" + PatientID + " ";


                oDB.Connect(false);
                object oInnerResult = new object();
                oInnerResult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (oInnerResult != null && Convert.ToString(oInnerResult) != "")
                {
                    if (Convert.ToInt32(oInnerResult) > 0)
                    {
                        _result = true;
                    }
                }

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result;
        }

        //for checking same SSN no...
        internal bool IsPatientSSNExists(string SSN, Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;
            try
            {
                string _sqlQuery = "select count(*) from patient where RTRIM(LTRIM(nSSN)) = '" + SSN.Trim() + "' AND nPatientID<>" + PatientID + " ";


                oDB.Connect(false);
                object oInnerResult = new object();
                oInnerResult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (oInnerResult != null && Convert.ToString(oInnerResult) != "")
                {
                    if (Convert.ToInt32(oInnerResult) > 0)
                    {
                        _result = true;
                    }
                }

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result;
        }

        //Validate if Insurance is used for billing
        public bool IsInsuranceUsed(long InsuranceID)
        {
            bool Result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtUsed = null;
            try
            {

                if (InsuranceID == 0)
                {
                    return Result;
                }

                string _sqlQuery = "";

                oDB.Connect(false);

                _sqlQuery = " SELECT bUsedForBilling FROM PatientInsurance_DTL WHERE nInsuranceID = " + InsuranceID + " ";
                oDB.Retrive_Query(_sqlQuery, out dtUsed);

                oDB.Disconnect();

                if (dtUsed != null && dtUsed.Rows.Count > 0)
                {
                    if (dtUsed.Rows[0]["bUsedForBilling"] != DBNull.Value)
                    {
                        if (Convert.ToBoolean(dtUsed.Rows[0]["bUsedForBilling"]) == true)
                        {
                            Result = true;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtUsed != null) { dtUsed.Dispose(); }
            }
            return Result;
        }

        // GLO2011-0011970 
        // Created a funtion to get the patient status is Legal Pending or not
        public string GetPatientStatus(long PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            //DataTable dtstatus =  null;
            string strStatus = "";

            try
            {
                oDB.Connect(false);
                oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                strStatus = Convert.ToString(oDB.ExecuteScalar("gsp_GetPatientStatus", oParameters));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

            return strStatus;
        }

        // GLO2011-0011970 
        // Created a function to check the patient status is Legal Pending or not
        public bool IsLegalPending(Int64 PatientID)
        {
            try
            {
                using (gloPatient ogloPatient = new gloPatient(_databaseconnectionstring))
                {
                    if (ogloPatient.GetPatientStatus(PatientID).ToString() == "Legal Pending")
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            return false;
        }

        public string CheckStaus(Int64 PatientID)
        {
            string _Msg = "";
            try
            {
                using (gloPatient ogloPatient = new gloPatient(_databaseconnectionstring))
                {
                    //if (string.Compare(_messageBoxCaption, "gloEMR", true) == 0)
                    //{
                    string _sStatus = ogloPatient.GetPatientStatus(PatientID).ToString();
                    if ((string.Compare(_sStatus, "Legal Pending", true) == 0) || (string.Compare(_sStatus, "Deceased", true) == 0))
                    {
                        _Msg = "The status of the patient is '" + _sStatus + "'." + Environment.NewLine + "Only Administrator can modify this Patient's information.";
                    }
                    else
                    {
                        _Msg = "";
                    }
                    //}

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return _Msg;
            }
            return _Msg;

        }

    }

    #region "Patient Class and Sub Classes"

    public class Patient : IDisposable
    {

        #region "Constructor & Destructor"

        public Patient()
        {
            _Referrals = new Referrals();
            _Insurances = new Insurances();

            _PatientDemographics = new PatientDemographics();
            _PatientGuardian = new PatientGuardian(_databaseConnectionString);
            _PatientOccupation = new PatientOccupation();
            _PatientInsurance = new PatientInsuranceOther();
            _deletedInsurances = new List<Int64>(); //added by panklaj on 20110113 for deleted insurances

            //Other Details
            _PatientDemographicOtherInfo = new PatientDemographicOtherInfo();
            _PatientWorkersComp = new PatientWorkersComps();

            _PatientPharmacies = new PatientDetails();
            _PatientReferrals = new PatientDetails();
            _PatientCareTeam = new PatientDetails();
            _PrimaryCarePhysicians = new PatientDetails();

            _PatientGuarantors = new PatientOtherContacts();

            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
            _PatientOtherGuarantors = new PatientOtherContacts();
            _PatientRepresentatives = new PatientRepresentatives();
            APIRepresentatives = new PatientRepresentatives();
            _PatientAccounts = new PatientAccounts();



            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            //

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


                    if (_Referrals != null)
                    {
                        _Referrals.Clear();
                        _Referrals.Dispose();
                        _Referrals = null;
                    }
                    if (_Insurances != null)
                    {
                        _Insurances.Clear();
                        _Insurances.Dispose();
                        _Insurances = null;
                    }
                    if (_PatientDemographics != null)
                    {
                        _PatientDemographics.MyPictureBoxControl = null;
                        _PatientDemographics.Dispose();
                        _PatientDemographics = null;
                    }
                    if (_PatientGuardian != null)
                    {
                        _PatientGuardian.Dispose();
                        _PatientGuardian = null;
                    }

                    if (_PatientOccupation != null)
                    {
                        _PatientOccupation.Dispose();
                        _PatientOccupation = null;
                    }

                    if (_PatientInsurance != null)
                    {
                        _PatientInsurance.Dispose();
                        _PatientInsurance = null;
                    }


                    if (_PatientDemographicOtherInfo != null)
                    {
                        _PatientDemographicOtherInfo.Dispose();
                        _PatientDemographicOtherInfo = null;
                    }
                    if (_PatientWorkersComp != null)
                    {
                        _PatientWorkersComp.Clear();
                        _PatientWorkersComp.Dispose();
                        _PatientWorkersComp = null;
                    }
                    if (_PatientPharmacies != null)
                    {
                        _PatientPharmacies.Clear();
                        _PatientPharmacies.Dispose();
                        _PatientPharmacies = null;
                    }
                    if (_PatientReferrals != null)
                    {
                        _PatientReferrals.Clear();
                        _PatientReferrals.Dispose();
                        _PatientReferrals = null;
                    }
                    if (_PatientCareTeam != null)
                    {
                        _PatientCareTeam.Clear();
                        _PatientCareTeam.Dispose();
                        _PatientCareTeam = null;
                    }
                    if (_PrimaryCarePhysicians != null)
                    {
                        _PrimaryCarePhysicians.Clear();
                        _PrimaryCarePhysicians.Dispose();
                        _PrimaryCarePhysicians = null;

                    }
                    if (_PatientGuarantors != null)
                    {
                        _PatientGuarantors.Clear();
                        _PatientGuarantors.Dispose();
                        _PatientGuarantors = null;
                    }
                    if (_PatientOtherGuarantors != null)
                    {
                        _PatientOtherGuarantors.Clear();
                        _PatientOtherGuarantors.Dispose();
                        _PatientOtherGuarantors = null;
                    }
                    if (_PatientRepresentatives != null)
                    {
                        _PatientRepresentatives.Clear();
                        _PatientRepresentatives.Dispose();
                        _PatientRepresentatives = null;
                    }
                    if (_PatientAccounts != null)
                    {
                        _PatientAccounts.Clear();
                        _PatientAccounts.Dispose();
                        _PatientAccounts = null;
                    }
                    try
                    {
                        if (oPatientPortalAccount != null)
                        {
                            oPatientPortalAccount.Dispose();
                            oPatientPortalAccount = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (_Account != null)
                        {
                            _Account.Dispose();
                            _Account = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (_PatientAccount != null)
                        {
                            _PatientAccount.Dispose();
                            _PatientAccount = null;
                        }
                    }
                    catch
                    {
                    }
                    // OLD disposals.
                    //_PatientDemographics.Dispose();
                    //_PatientGuardian.Dispose();
                    //_PatientOccupation.Dispose();
                    //_PatientInsurance.Dispose(); 
                    //_PatientGuarantors.Dispose();
                    //_PatientInsurance.Dispose();
                    //_Insurances.Dispose();
                    //_PatientPharmacies.Dispose();
                    //_Referrals.Dispose();
                    //_PatientReferrals.Dispose();
                    //_PrimaryCarePhysicians.Dispose();
                }
            }
            disposed = true;
        }

        ~Patient()
        {

            Dispose(false);
        }

        #endregion

        #region "Private Variables"

        private String _databaseConnectionString = "";

        //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        private Int64 _nPatientID = 0;
        private bool _bitIsBlock = false;
        private Insurances _Insurances;
        private Referrals _Referrals;

        private PatientDemographics _PatientDemographics = null;

        private PatientGuardian _PatientGuardian = null;
        private PatientOccupation _PatientOccupation = null;
        private PatientInsuranceOther _PatientInsurance = null;
        private List<Int64> _deletedInsurances = new List<Int64>(); //collection defined by pankaj on date 20110113 for deleted insurances

        private PatientWorkersComps _PatientWorkersComp = null;

        //Billing Related Enhacement Classes
        private PatientDemographicOtherInfo _PatientDemographicOtherInfo = null;
        //

        //
        private PatientDetails _PatientPharmacies = null;
        private PatientDetails _PatientReferrals = null;
        private PatientDetails _PatientCareTeam = null;
        private PatientDetails _PrimaryCarePhysicians = null;

        //
        private PatientOtherContacts _PatientGuarantors = null;

        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        private Account _Account = null;
        private PatientAccount _PatientAccount = null;
        private PatientAccounts _PatientAccounts = null;
        private PatientOtherContacts _PatientOtherGuarantors = null;
        private PatientRepresentatives _PatientRepresentatives = null;
        private PatientPortalAccount oPatientPortalAccount = null;

        //API
        PatientRepresentatives oAPIRepresentatives = null;
        PatientPortalAccount oAPIAccount = null;
        //API
        public bool _IsPatientAccountFeature = false;
        public bool _IsPatientDataModified = false;
        public Int64 nPAccountId;
        public Int64 nGuarantorId;
        public bool _IsPatientCodeModified = false;

        #endregion

        #region "Property Procedures"

        //property defined by pankaj on 20110113 for deleted insurances
        public List<Int64> DeletedInsurances
        {
            get { return _deletedInsurances; }
            set { _deletedInsurances = value; }
        }

        //Property added to check whether any insurance related updates are done or not (ref #GLO2010-0007999)
        bool _isInsuranceModified = false;
        public bool IsInsuranceModified
        {
            get { return _isInsuranceModified; }
            set { _isInsuranceModified = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseConnectionString; }
            set { _databaseConnectionString = value; }
        }


        //Billing Related Enhacement Classes


        public PatientDemographicOtherInfo PatientDemographicOtherInfo
        {
            get { return _PatientDemographicOtherInfo; }
            set { _PatientDemographicOtherInfo = value; }
        }

        public PatientDemographics DemographicsDetail
        {
            get { return _PatientDemographics; }
            set { _PatientDemographics = value; }
        }

        public PatientGuardian GuardianDetail
        {
            get { return _PatientGuardian; }
            set { _PatientGuardian = value; }
        }

        public PatientOccupation OccupationDetail
        {
            get { return _PatientOccupation; }
            set { _PatientOccupation = value; }
        }

        public PatientInsuranceOther InsuranceDetails
        {
            get { return _PatientInsurance; }
            set { _PatientInsurance = value; }
        }
        public PatientWorkersComps PatientWorkersComp
        {
            get { return _PatientWorkersComp; }
            set { _PatientWorkersComp = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        //Patient _bitIsBlock
        public bool PatientIsBlock
        {
            get { return _bitIsBlock; }
            set { _bitIsBlock = value; }
        }

        //Insurance  Information
        public Insurances Insurances
        {
            get { return _Insurances; }
            set { _Insurances = value; }
        }

        //Referrals Information
        public Referrals Referrals
        {
            get
            { return _Referrals; }
            set
            { _Referrals = value; }
        }

        public PatientDetails PatientPharmacies
        {
            get
            { return _PatientPharmacies; }
            set
            { _PatientPharmacies = value; }
        }

        public PatientDetails PatientReferrals
        {
            get
            { return _PatientReferrals; }
            set
            { _PatientReferrals = value; }
        }

        public PatientDetails PatientCareTeam
        {
            get
            { return _PatientCareTeam; }
            set
            { _PatientCareTeam = value; }
        }

        public PatientDetails PrimaryCarePhysicians
        {
            get
            { return _PrimaryCarePhysicians; }
            set
            { _PrimaryCarePhysicians = value; }
        }

        public PatientOtherContacts PatientGuarantors
        {
            get
            { return _PatientGuarantors; }
            set
            { _PatientGuarantors = value; }
        }

        //Added by Sai Krishna for PAF 2011-05-09(yyyy-mm-dd)
        public Account Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

        public PatientAccount PatientAccount
        {
            get { return _PatientAccount; }
            set { _PatientAccount = value; }
        }

        public PatientAccounts PatientAccounts
        {
            get { return _PatientAccounts; }
            set { _PatientAccounts = value; }
        }

        public bool IsPatientAccountFeature
        {
            get { return _IsPatientAccountFeature; }
            set { _IsPatientAccountFeature = value; }
        }

        public PatientOtherContacts PatientOtherGuarantors
        {
            get { return _PatientOtherGuarantors; }
            set { _PatientOtherGuarantors = value; }
        }
        public PatientRepresentatives PatientRepresentatives
        {
            get { return _PatientRepresentatives; }
            set { _PatientRepresentatives = value; }
        }

        public PatientPortalAccount PatientPortalAccount
        {
            get
            { return oPatientPortalAccount; }
            set
            { oPatientPortalAccount = value; }
        }
        //API
        public PatientRepresentatives APIRepresentatives
        {
            get
            { return oAPIRepresentatives; }
            set
            { oAPIRepresentatives = value; }
        }

        public PatientPortalAccount APIAccount
        {
            get
            { return oAPIAccount; }
            set
            { oAPIAccount = value; }
        }
        //API

        #endregion

    }

    public class Patients : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public Patients()
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


        ~Patients()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(Patient item)
        {
            _innerlist.Add(item);
        }

        //public int Add(string parametername, ParameterDirection parameterdirection, DbType datatype, object value)
        //{
        //    DBParameter item = new DBParameter(parametername, parameterdirection, datatype, value);
        //    return _innerlist.Add(item);
        //}

        public bool Remove(Patient item)
        //Remark - Work Remining for comparision
        {
            bool result = false;
            //DBParameter obj;

            //for (int i = 0; i < _innerlist.Count; i++)
            //{
            //    //store current index being checked
            //    obj = new DBParameter();
            //    obj = (DBParameter)_innerlist[i];
            //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            //    {
            //        _innerlist.RemoveAt(i);
            //        result = true;
            //        break;
            //    }
            //    obj = null;
            //}

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

        public Patient this[int index]
        {
            get
            { return (Patient)_innerlist[index]; }
        }

        public bool Contains(Patient item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(Patient item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(Patient[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class PatientDemographics : IDisposable
    {

        #region "Constructor & Distructor"

        public PatientDemographics()
        {
            //_Referrals = new Referrals();
            //_Insurances = new Insurances();
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
                    if (_Pharmacy != null)
                    {
                        _Pharmacy.Dispose();
                    }
                    if (_Referrals != null)
                    {
                        _Referrals.Dispose();
                    }

                }
            }
            disposed = true;
        }

        ~PatientDemographics()
        {

            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        private bool _blnYesNoLab = false; //''YES/No Labs
        private Int64 _nPatientID;
        private string _sPatientCode = "";
        private string _sFirstName = "";
        private string _sMiddleName = "";
        private string _sLastName = "";
        private String _nSSN = "";
        private DateTime _dtDOB;
        private string _sGender = "";
        private string _sMaritalStatus = "";
        private string _sAddress1 = "";
        private string _sAddress2 = "";
        private string _sCity = "";
        private string _sState = "";
        private string _sZip = "";
        private string _sCounty = "";
        private string _sCountry = "";
        private string _sPhone = "";
        private string _sMobile = "";
        private string _sEmail = "";
        private string _sFax = "";
        private string _sOccupation = "";
        private Int64 _nProviderID = 0;
        private Int64 _nPCPId = 0;
        private string _sPrimaryCarePhysicianName = "";
        private string _sGuarantor = "";
        private Int64 _nGuarantorID = 0;
        private bool _IsGuarantor;
        private Int64 _nPharmacyID = 0;
        private string _sPharmacyName = "";
        private string _sRace = "";
        private string _sLang = "";
        private string _sInbound = "";
         private string _sInboundTransactionOfCare = "";
        private string _CommPref = "";//Communication Preference
        private string _sEthn = "";
        private string _sPrefix = "";
        private System.Drawing.Image _iPhoto = null;

        private bool _exemptFromReport;
        private bool _directive;
        private string _sHandDominance = "";
        private string _sLocation = "";
        private string _ProviderName = "";
        //private Insurances _Insurances;
        private Referrals _Referrals = new Referrals();
        //shubhangi
        private Pharmacy _Pharmacy = new Pharmacy();

        private string _sEmergencyContact = "";
        private string _sEmergencyPhone = "";
        private string _sEmergencyMobile = "";
        private bool _bSetToCollection = false;

        //Added by Anil 20090713
        private string _sEmergencyRelationshipCode = "";
        private string _sEmergencyRelationshipDesc = "";
        private System.Drawing.Image _iSignature = null;

        //Added by Mukesh 2009-08-29
        private bool _excludeFromStatement = false;
        private string _sInsuranceNotes = "";
        private string sBirthTime = "";
        private string sExternalCode = ""; //Added by kanchan on 20101113
        private byte[] _myPictureBoxControl = null; //gloPicutreBoxControl ''Start'GLO2010-0007047[BJMC]: Webcam image too small
        //7022Items: Home Billing- Added to save area code for patient
        private string _sAreaCode = "";
        private string _sSuffix = "";

        #endregion

        #region "Property Procedures"
        //start/ gloPicutreBoxControl ''Start'GLO2010-0007047[BJMC]: Webcam image too small
        public byte[] MyPictureBoxControl
        {
            get { return _myPictureBoxControl; }
            set
            {
                _myPictureBoxControl = value;
                if (PatientPhoto != null)
                {
                    PatientPhoto.Dispose();
                    PatientPhoto = null;
                }
                if (_myPictureBoxControl != null)
                {
                    PatientPhoto = gloPictureBox.gloImage.GetImage(_myPictureBoxControl, true);
                }

            }
        }
        //end/ gloPicutreBoxControl ''End'GLO2010-0007047[BJMC]: Webcam image too small


        //1. Patient ID
        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        //2. Patient Code
        public string PatientCode
        {
            get { return _sPatientCode; }
            set { _sPatientCode = value; }
        }

        public string ProvideName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }

        //3. Patient First name
        public string PatientFirstName
        {
            get { return _sFirstName; }
            set { _sFirstName = value; }
        }

        //4. Patient Middle name
        public string PatientMiddleName
        {
            get { return _sMiddleName; }
            set { _sMiddleName = value; }
        }

        //5. Patient Last name
        public string PatientLastName
        {
            get { return _sLastName; }
            set { _sLastName = value; }
        }

        //6. Patient SSN
        // By Mahesh - 20080402
        // Change Done while Changing the DataType of nSSN from Numeric to Varchar
        public String PatientSSN
        {
            get { return _nSSN; }
            set { _nSSN = value; }
        }

        //7. Patient DOB
        public DateTime PatientDOB
        {
            get { return _dtDOB; }
            set { _dtDOB = value; }
        }

        //8. Patient Gender
        public string PatientGender
        {
            get { return _sGender; }
            set { _sGender = value; }
        }

        //9. Patient _sMaritalStatus
        public string PatientMaritalStatus
        {
            get { return _sMaritalStatus; }
            set { _sMaritalStatus = value; }
        }

        //10. Patient Address1
        public string PatientAddress1
        {
            get { return _sAddress1; }
            set { _sAddress1 = value; }
        }

        //11.Patient Address2
        public string PatientAddress2
        {
            get { return _sAddress2; }
            set { _sAddress2 = value; }
        }


        //12.Patient City
        public string PatientCity
        {
            get { return _sCity; }
            set { _sCity = value; }
        }

        //13.Patient State
        public string PatientState
        {
            get { return _sState; }
            set { _sState = value; }
        }

        //14.Patient Zip
        public string PatientZip
        {
            get { return _sZip; }
            set { _sZip = value; }
        }

        //15.Patient Country
        public string PatientCounty
        {
            get { return _sCounty; }
            set { _sCounty = value; }
        }

        //15.Patient Country
        public string PatientCountry
        {
            get { return _sCountry; }
            set { _sCountry = value; }
        }

        //16.Patient Phone
        public string PatientPhone
        {
            get { return _sPhone; }
            set { _sPhone = value; }
        }

        //17.Patient Mobile
        public string PatientMobile
        {
            get { return _sMobile; }
            set { _sMobile = value; }
        }

        //18.Patient Address2
        public string PatientEmail
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        //19.Patient Fax
        public string PatientFax
        {
            get { return _sFax; }
            set { _sFax = value; }
        }


        //20.Patient Occupation
        public string PatientOccupation
        {
            get { return _sOccupation; }
            set { _sOccupation = value; }
        }

        //21.Patient EmploymentStatus

        //31.Patient ProviderID
        public Int64 PatientProviderID
        {
            get { return _nProviderID; }
            set { _nProviderID = value; }
        }

        //Primary Care Physician Name
        public string PrimaryCarePhysicianName
        {
            get { return _sPrimaryCarePhysicianName; }
            set { _sPrimaryCarePhysicianName = value; }
        }

        //32.Patient PCPId
        public Int64 PatientPCPId
        {
            get { return _nPCPId; }
            set { _nPCPId = value; }
        }

        //33.Patient Guarantor
        public string PatientGuarantor
        {
            get { return _sGuarantor; }
            set { _sGuarantor = value; }
        }

        //Patient Guarantor ID
        public Int64 PatientGuarantorID
        {
            get { return _nGuarantorID; }
            set { _nGuarantorID = value; }
        }


        //Patient Guarantor ID
        public bool IsGuarantor
        {
            get { return _IsGuarantor; }
            set { _IsGuarantor = value; }
        }


        //34.Patient PharmacyID
        public Int64 PatientPharmacyID
        {
            get { return _nPharmacyID; }
            set { _nPharmacyID = value; }
        }

        //shubhangi
        // Pharmacy Name
        public string PharmacyName
        {
            get { return _sPharmacyName; }
            set { _sPharmacyName = value; }
        }

        //SHUBHANGI
        public Pharmacy Pharmacy
        {
            get
            { return _Pharmacy; }
            set
            { _Pharmacy = value; }
        }



        //36.Patient Race
        public string PatientRace
        {
            get { return _sRace; }
            set { _sRace = value; }
        }

        //36Patient Communication Preference
        public string PatientCommunicationPrefence
        {
            get { return _CommPref; }
            set { _CommPref = value; }
        }


        public string PatientLanguage
        {
            get { return _sLang; }
            set { _sLang = value; }
        }
         //36.Patient Ethnicities     
        public string PatientEthnicities
        {
            get { return _sEthn; }
            set { _sEthn = value; }
        }
        public string PatientPrefix
        {
            get { return _sPrefix; }
            set { _sPrefix = value; }
        }
        public string BirthTime
        {
            get
            {
                return sBirthTime;
            }
            set
            {
                sBirthTime = value;
            }
        }
        //38.Patient Photo
        public System.Drawing.Image PatientPhoto
        {
            get { return _iPhoto; }
            set { _iPhoto = value; }
        }


        //42.Patient HandDominance
        public string PatientHandDominance
        {
            get { return _sHandDominance; }
            set { _sHandDominance = value; }
        }

        //42.Patient _sLocation
        public string PatientLocation
        {
            get { return _sLocation; }
            set { _sLocation = value; }
        }


        public bool ExemptFromReport
        {
            get { return _exemptFromReport; }
            set { _exemptFromReport = value; }
        }


        public bool Directive
        {
            get { return _directive; }
            set { _directive = value; }
        }

        //Referrals Information
        public Referrals Referrals
        {
            get
            { return _Referrals; }
            set
            { _Referrals = value; }
        }

        public string EmergencyContact
        {
            get { return _sEmergencyContact; }
            set { _sEmergencyContact = value; }
        }

        public string EmergencyPhone
        {
            get { return _sEmergencyPhone; }
            set { _sEmergencyPhone = value; }
        }

        public string EmergencyMobile
        {
            get { return _sEmergencyMobile; }
            set { _sEmergencyMobile = value; }
        }

        public bool SetToCollection
        {
            get { return _bSetToCollection; }
            set { _bSetToCollection = value; }
        }

        ////Added by Anil 20090713
        public string EmergencyRelationshipCode
        {
            get { return _sEmergencyRelationshipCode; }
            set { _sEmergencyRelationshipCode = value; }
        }

        public string EmergencyRelationshipDesc
        {
            get { return _sEmergencyRelationshipDesc; }
            set { _sEmergencyRelationshipDesc = value; }
        }

        public System.Drawing.Image Signature
        {
            get { return _iSignature; }
            set { _iSignature = value; }
        }
        ////


        //Added by Mukesh 20090829
        public bool ExcludeFromStatement
        {
            get { return _excludeFromStatement; }
            set { _excludeFromStatement = value; }
        }

        public string InsuranceNotes
        {
            get { return _sInsuranceNotes; }
            set { _sInsuranceNotes = value; }
        }




        //Start ''YES/No Labs
        public bool IsYesNoLab
        {
            get { return _blnYesNoLab; }
            set { _blnYesNoLab = value; }
        }
        //End ''YES/No Labs

        //Added by kanchan on 20101113
        public string PatientExternalCode
        {
            get { return sExternalCode; }
            set { sExternalCode = value; }
        }
        //7022Items: Home Billing- Added to save area code for patient
        public string AreaCode
        {
            get { return _sAreaCode; }
            set { _sAreaCode = value; }
        }


        public string PatientSuffix
        {
            get { return _sSuffix; }
            set { _sSuffix = value; }
        }
        #endregion

    }

    public class PatientGuardian : IDisposable
    {
        #region "Contructors & Destructors"

        private string _databaseconnectionstring = "";

        public PatientGuardian(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
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


        ~PatientGuardian()
        {
            Dispose(false);
        }

        #endregion

        #region Private Variables

        private string _sMother_fname = "";
        private string _sMother_mname = "";
        private string _sMother_lname = "";
        private string _sMother_maiden_fname = "";
        private string _sMother_maiden_mname = "";
        private string _sMother_maiden_lname = "";
        private string _sMother_Address1 = "";
        private string _sMother_Address2 = "";
        private string _sMother_City = "";
        private string _sMother_State = "";
        private string _sMother_Zip = "";
        private string _sMother_County = "";
        private string _sMother_Country = "";
        private string _sMother_Phone = "";
        private string _sMother_Mobile = "";
        private string _sMother_FAX = "";
        private string _sMother_Email = "";
        private string _sFather_fname = "";
        private string _sFather_mname = "";
        private string _sFather_lname = "";
        private string _sFather_Address1 = "";
        private string _sFather_Address2 = "";
        private string _sFather_City = "";
        private string _sFather_State = "";
        private string _sFather_Zip = "";
        private string _sFather_County = "";
        private string _sFather_Country = "";
        private string _sFather_Phone = "";
        private string _sFather_Mobile = "";
        private string _sFather_FAX = "";
        private string _sFather_Email = "";
        private string _sGuardian_fname = "";
        private string _sGuardian_mname = "";
        private string _sGuardian_lname = "";
        private string _sGuardian_Address1 = "";
        private string _sGuardian_Address2 = "";
        private string _sGuardian_City = "";
        private string _sGuardian_State = "";
        private string _sGuardian_Zip = "";
        private string _sGuardian_County = "";
        private string _sGuardian_Country = "";
        private string _sGuardian_Phone = "";
        private string _sGuardian_Mobile = "";
        private string _sGuardian_FAX = "";
        private string _sGuardian_Email = "";
        //start :: Patient Guardian ReletionShip
        private string _sGuardian_RelationCD = "";
        private string _sGuardian_RelationDS = "";
        //End :: Patient Guardian ReletionShip
        #endregion

        #region "Property Procedures"

        //1.Patient Mother_fname
        public string PatientMotherFirstName
        {
            get { return _sMother_fname; }
            set { _sMother_fname = value; }
        }

        //2.Patient Mother_mname
        public string PatientMotherMiddleName
        {
            get { return _sMother_mname; }
            set { _sMother_mname = value; }
        }

        //3.Patient Mother_lname
        public string PatientMotherLastName
        {
            get { return _sMother_lname; }
            set { _sMother_lname = value; }
        }

        //4.Patient Mother_Address1
        public string PatientMotherAddress1
        {
            get { return _sMother_Address1; }
            set { _sMother_Address1 = value; }
        }

        //5.Patient Mother_Address2
        public string PatientMotherAddress2
        {
            get { return _sMother_Address2; }
            set { _sMother_Address2 = value; }
        }

        //6.Patient Mother_City
        public string PatientMotherCity
        {
            get { return _sMother_City; }
            set { _sMother_City = value; }
        }

        //7.Patient _sMother_State
        public string PatientMotherState
        {
            get { return _sMother_State; }
            set { _sMother_State = value; }
        }

        //8.Patient Mother_Zip
        public string PatientMotherZip
        {
            get { return _sMother_Zip; }
            set { _sMother_Zip = value; }
        }

        //9.Patient Mother_Country
        public string PatientMotherCountry
        {
            get { return _sMother_Country; }
            set { _sMother_Country = value; }
        }

        //9.Patient Mother_County
        public string PatientMotherCounty
        {
            get { return _sMother_County; }
            set { _sMother_County = value; }
        }

        //10.Patient _sMother_Phone
        public string PatientMotherPhone
        {
            get { return _sMother_Phone; }
            set { _sMother_Phone = value; }
        }

        //11.Patient Mother_Mobile
        public string PatientMotherMobile
        {
            get { return _sMother_Mobile; }
            set { _sMother_Mobile = value; }
        }

        //12.Patient Mother_FAX
        public string PatientMotherFAX
        {
            get { return _sMother_FAX; }
            set { _sMother_FAX = value; }
        }

        //13.Patient Mother_Email
        public string PatientMotherEmail
        {
            get { return _sMother_Email; }
            set { _sMother_Email = value; }
        }

        //14.Patient Father_fname
        public string PatientFatherFirstName
        {
            get { return _sFather_fname; }
            set { _sFather_fname = value; }
        }

        //15.Patient Father_mname
        public string PatientFatherMiddleName
        {
            get { return _sFather_mname; }
            set { _sFather_mname = value; }
        }

        //16.Patient Father_lname
        public string PatientFatherLastName
        {
            get { return _sFather_lname; }
            set { _sFather_lname = value; }
        }

        //17.Patient Father_Address1
        public string PatientFatherAddress1
        {
            get { return _sFather_Address1; }
            set { _sFather_Address1 = value; }
        }

        //18.Patient Father_Address2
        public string PatientFatherAddress2
        {
            get { return _sFather_Address2; }
            set { _sFather_Address2 = value; }
        }

        //19.Patient Father_City
        public string PatientFatherCity
        {
            get { return _sFather_City; }
            set { _sFather_City = value; }
        }

        //20.Patient Father_State
        public string PatientFatherState
        {
            get { return _sFather_State; }
            set { _sFather_State = value; }
        }

        //21.Patient Father_Zip
        public string PatientFatherZip
        {
            get { return _sFather_Zip; }
            set { _sFather_Zip = value; }
        }

        //22.Patient Father_Country
        public string PatientFatherCounty
        {
            get { return _sFather_County; }
            set { _sFather_County = value; }
        }

        //22.Patient Father_Country
        public string PatientFatherCountry
        {
            get { return _sFather_Country; }
            set { _sFather_Country = value; }
        }

        //23.Patient _sFather_Phone
        public string PatientFatherPhone
        {
            get { return _sFather_Phone; }
            set { _sFather_Phone = value; }
        }

        //24.Patient Father_Mobile
        public string PatientFatherMobile
        {
            get { return _sFather_Mobile; }
            set { _sFather_Mobile = value; }
        }

        //25.Patient Father_FAX
        public string PatientFatherFAX
        {
            get { return _sFather_FAX; }
            set { _sFather_FAX = value; }
        }

        //26.Patient Father_Email
        public string PatientFatherEmail
        {
            get { return _sFather_Email; }
            set { _sFather_Email = value; }
        }

        //27.Patient Guardian_fname
        public string PatientGuardianFirstName
        {
            get { return _sGuardian_fname; }
            set { _sGuardian_fname = value; }
        }

        //28.Patient Guardian_mname
        public string PatientGuardianMiddleName
        {
            get { return _sGuardian_mname; }
            set { _sGuardian_mname = value; }
        }

        //29.Patient Guardian_lname
        public string PatientGuardianLastName
        {
            get { return _sGuardian_lname; }
            set { _sGuardian_lname = value; }
        }

        //30.Patient _sGuardian_Address1
        public string PatientGuardianAddress1
        {
            get { return _sGuardian_Address1; }
            set { _sGuardian_Address1 = value; }
        }

        //31Patient _sGuardian_Address2
        public string PatientGuardianAddress2
        {
            get { return _sGuardian_Address2; }
            set { _sGuardian_Address2 = value; }
        }

        //32.Patient _sGuardian_City
        public string PatientGuardianCity
        {
            get { return _sGuardian_City; }
            set { _sGuardian_City = value; }
        }

        //33.Patient _sGuardian_State
        public string PatientGuardianState
        {
            get { return _sGuardian_State; }
            set { _sGuardian_State = value; }
        }

        //34.Patient _sGuardian_Zip
        public string PatientGuardianZip
        {
            get { return _sGuardian_Zip; }
            set { _sGuardian_Zip = value; }
        }

        //35.Patient _sGuardian_County
        public string PatientGuardianCounty
        {
            get { return _sGuardian_County; }
            set { _sGuardian_County = value; }
        }

        //35.Patient _sGuardian_Country
        public string PatientGuardianCountry
        {
            get { return _sGuardian_Country; }
            set { _sGuardian_Country = value; }
        }

        //36.Patient _sGuardian_Phone
        public string PatientGuardianPhone
        {
            get { return _sGuardian_Phone; }
            set { _sGuardian_Phone = value; }
        }

        //37.Patient _sGuardian_Mobile
        public string PatientGuardianMobile
        {
            get { return _sGuardian_Mobile; }
            set { _sGuardian_Mobile = value; }
        }

        //38.Patient _sGuardian_FAX
        public string PatientGuardianFAX
        {
            get { return _sGuardian_FAX; }
            set { _sGuardian_FAX = value; }
        }

        //39.Patient _sGuardian_Email
        public string PatientGuardianEmail
        {
            get { return _sGuardian_Email; }
            set { _sGuardian_Email = value; }
        }

        //Start :: Patient GuardianReletionShip
        public string PatientGuardianRelationCD
        {
            get { return _sGuardian_RelationCD; }
            set { _sGuardian_RelationCD = value; }
        }

        //41.Patient _sGuardian_Relation
        public string PatientGuardianRelationDS
        {
            get { return _sGuardian_RelationDS; }
            set { _sGuardian_RelationDS = value; }
        }

        //42.Patient Mother_Maidenfname
        public string PatientMotherMaidenFirstName
        {
            get { return _sMother_maiden_fname; }
            set { _sMother_maiden_fname = value; }
        }

        //43.Patient Mother_Maidenmname
        public string PatientMotherMaidenMiddleName
        {
            get { return _sMother_maiden_mname; }
            set { _sMother_maiden_mname = value; }
        }

        //44.Patient Mother_Maidenlname
        public string PatientMotherMaidenLastName
        {
            get { return _sMother_maiden_lname; }
            set { _sMother_maiden_lname = value; }
        }

        //End :: Patient GuardianReletionShip

        #endregion
    }

    public class PatientOccupation : IDisposable
    {

        #region "Constructor & Distructor"

        public PatientOccupation()
        {
            //_Referrals = new Referrals();
            //_Insurances = new Insurances();
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

        ~PatientOccupation()
        {

            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        private string _sOccupation = "";
        private string _sEmploymentStatus = "";
        private string _sPlaceofEmployment = "";
        private string _sWorkAddress1 = "";
        private string _sWorkAddress2 = "";
        private string _sWorkCity = "";
        private string _sWorkState = "";
        private string _sWorkZip = "";
        private string _sWorkCounty = "";
        private string _sWorkCountry = "";
        private string _sWorkPhone = "";
        private string _sWorkMobile = "";
        private string _sWorkEmail = "";
        private string _sWorkFax = "";


        //Code added on 14 May 2008 by Sagar Ghodke
        private string _sEmployerName = "";
        private DateTime? _dtRetirementDate = null;
        private string _sEmploymentType = "";
        //
        #endregion

        #region "Property Procedures"


        //20.Patient Occupation
        public string Occupation
        {
            get { return _sOccupation; }
            set { _sOccupation = value; }
        }

        //21.Patient EmploymentStatus
        public string PatientEmploymentStatus
        {
            get { return _sEmploymentStatus; }
            set { _sEmploymentStatus = value; }
        }

        //22.Patient PlaceofEmployment
        public string PatientPlaceofEmployment
        {
            get { return _sPlaceofEmployment; }
            set { _sPlaceofEmployment = value; }
        }

        //23.Patient WorkAddress1
        public string PatientWorkAddress1
        {
            get { return _sWorkAddress1; }
            set { _sWorkAddress1 = value; }
        }

        //24.Patient WorkAddress2
        public string PatientWorkAddress2
        {
            get { return _sWorkAddress2; }
            set { _sWorkAddress2 = value; }
        }


        //25.Patient WorkCity
        public string PatientWorkCity
        {
            get { return _sWorkCity; }
            set { _sWorkCity = value; }
        }

        //26.Patient WorkState
        public string PatientWorkState
        {
            get { return _sWorkState; }
            set { _sWorkState = value; }
        }


        //27.Patient WorkZip
        public string PatientWorkZip
        {
            get { return _sWorkZip; }
            set { _sWorkZip = value; }
        }

        //28.Patient WorkPhone
        public string PatientWorkPhone
        {
            get { return _sWorkPhone; }
            set { _sWorkPhone = value; }
        }

        //28.Patient Workcountry
        public string PatientWorkCountry
        {
            get { return _sWorkCountry; }
            set { _sWorkCountry = value; }
        }

        //28.Patient Workcounty
        public string PatientWorkCounty
        {
            get { return _sWorkCounty; }
            set { _sWorkCounty = value; }
        }

        //29.Patient WorkFax
        public string PatientWorkFax
        {
            get { return _sWorkFax; }
            set { _sWorkFax = value; }
        }


        //29.Patient WorkMobile
        public string PatientWorkMobile
        {
            get { return _sWorkMobile; }
            set { _sWorkMobile = value; }
        }

        //29.Patient WorkMobile
        public string PatientWorkEmail
        {
            get { return _sWorkEmail; }
            set { _sWorkEmail = value; }
        }

        //Code added on 14 May 2008 by Sagar Ghodke
        public string EmployerName
        {
            get { return _sEmployerName; }
            set { _sEmployerName = value; }
        }
        public DateTime? RetirementDate
        {
            get { return _dtRetirementDate; }
            set { _dtRetirementDate = value; }
        }
        public string EmploymentType
        {
            get { return _sEmploymentType; }
            set { _sEmploymentType = value; }
        }

        //

        #endregion

    }

    //Patients Other details
    public class PatientDemographicOtherInfo
    {

        #region "Constructor & Destructor"

        public PatientDemographicOtherInfo()
        {

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

        ~PatientDemographicOtherInfo()
        {
            Dispose(false);
        }

        #endregion

        #region " Declarations "

        private Int64 _PatientID = 0;
        private string _PatientLawyer = "";
        private string _sSpouseName = "";
        private string _sSpousePhone = "";
        private DateTime _dtRegistrationDate = DateTime.Now;
        private string _sPatientStatus = "";
        private bool _bSignatureOnFile = false;
        private DateTime _dtSignatureOnFileDate = DateTime.Now;
        private string _sCMS1500Box13 = "";
        private bool _bReminderDeclined = false;
        private bool _isBadDebtPatient = false;
        private ArrayList oMedicalConditions = new ArrayList();
        private Int64 _nSexualOrientationID = 0;
        private string _sSexualOrientationCode = "";
        private string _sSexualOrientationDesc = "";
        private string _sSexualOrientationOtherSpecification = "";
        private Int64 _nGenderIdentityID = 0;
        private string _sGenderIdentityCode = "";
        private string _sGenderIdentityDesc = "";
        private string _sGenderIdentityOtherSpecification = "";
        private string _sMultipleBirthIndicator = "1";
        private Int64 _BirthOrder = 1;

        private string _sPatientPrevFName = "";
        private string _sPatientPrevMName = "";
        private string _sPatientPrevLName = "";
        private string _PatientBirthSex = "";
        private string _ImmunizationRegistryStatus = "Active";

        #endregion " Declarations "

        #region " Property Procedures "

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }

        }

        public string PatientLawyer
        {
            get { return _PatientLawyer; }
            set { _PatientLawyer = value; }

        }

        public string SpouseName
        {
            get { return _sSpouseName; }
            set { _sSpouseName = value; }
        }

        public string SpousePhone
        {
            get { return _sSpousePhone; }
            set { _sSpousePhone = value; }
        }

        public string Status
        {
            get { return _sPatientStatus; }
            set { _sPatientStatus = value; }
        }

        public DateTime RegistrationDate
        {
            get { return _dtRegistrationDate; }
            set { _dtRegistrationDate = value; }
        }

        public bool SOF
        {
            get { return _bSignatureOnFile; }
            set { _bSignatureOnFile = value; }
        }

        public DateTime SOFDate
        {
            get { return _dtSignatureOnFileDate; }
            set { _dtSignatureOnFileDate = value; }
        }

        public string CMS1500Box13
        {
            get { return _sCMS1500Box13; }
            set { _sCMS1500Box13 = value; }
        }

        public bool Reminders
        {
            get { return _bReminderDeclined; }
            set { _bReminderDeclined = value; }
        }

        public ArrayList MedicalConditions
        {
            get { return oMedicalConditions; }
            set { oMedicalConditions = value; }
        }

        public bool isBadDebtPatient
        {
            get { return _isBadDebtPatient; }
            set { _isBadDebtPatient = value; }
        }

        public Int64 SexualOrientationID
        {
            get { return _nSexualOrientationID; }
            set { _nSexualOrientationID = value; }
        }

        public string SexualOrientationCode
        {
            get { return _sSexualOrientationCode; }
            set { _sSexualOrientationCode = value; }
        }

        public string SexualOrientationDesc
        {
            get { return _sSexualOrientationDesc; }
            set { _sSexualOrientationDesc = value; }
        }

        public string SexualOrientationOtherSpecification
        {
            get { return _sSexualOrientationOtherSpecification; }
            set { _sSexualOrientationOtherSpecification = value; }
        }

        public Int64 GenderIdentityID
        {
            get { return _nGenderIdentityID; }
            set { _nGenderIdentityID = value; }
        }

        public string GenderIdentityCode
        {
            get { return _sGenderIdentityCode; }
            set { _sGenderIdentityCode = value; }
        }

        public string GenderIdentityDesc
        {
            get { return _sGenderIdentityDesc; }
            set { _sGenderIdentityDesc = value; }
        }

        public string GenderIdentityOtherSpecification
        {
            get { return _sGenderIdentityOtherSpecification; }
            set { _sGenderIdentityOtherSpecification = value; }
        }

        public string sPatientPrevFName
        {
            get { return _sPatientPrevFName; }
            set { _sPatientPrevFName = value; }
        }

        public string sPatientPrevMName
        {
            get { return _sPatientPrevMName; }
            set { _sPatientPrevMName = value; }
        }

        public string sPatientPrevLName
        {
            get { return _sPatientPrevLName; }
            set { _sPatientPrevLName = value; }
        }

        public string sMultipleBirthIndicator
        {
            get { return _sMultipleBirthIndicator; }
            set { _sMultipleBirthIndicator = value; }
        }

        public Int64 BirthOrder
        {
            get { return _BirthOrder; }
            set { _BirthOrder = value; }

        }


        public string PatientBirthSex
        {
            get { return _PatientBirthSex; }
            set { _PatientBirthSex = value; }
        }

        public string ImmunizationRegistryStatus
        {
            get { return _ImmunizationRegistryStatus; }
            set { _ImmunizationRegistryStatus = value; }
        }
        #endregion " Property Procedures "

    }

    //Patients Other details
    public class PatientWorkersComp : IDisposable
    {

        #region "Constructor & Distructor"

        public PatientWorkersComp()
        {

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

        ~PatientWorkersComp()
        {
            Dispose(false);
        }

        #endregion "Constructor & Distructor"

        #region "Private variables"

        private Int64 _nPatientID = 0;
        private string _sClaimno = "";
        private string _sContractno = "";
        private DateTime _dtValidFrom = DateTime.Now;
        private DateTime _dtValidTo = DateTime.Now;
        private string _sOtherinfo = "";
        private Int64 _nType = 0;
        private Int64 _nInsuranceID = 0;

        #endregion "Private variables"

        #region "Property Procedures"

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }



        public string Claimno
        {
            get { return _sClaimno; }
            set { _sClaimno = value; }
        }

        public string Contractno
        {
            get { return _sContractno; }
            set { _sContractno = value; }
        }

        public DateTime ValidFrom
        {
            get { return _dtValidFrom; }
            set { _dtValidFrom = value; }
        }

        public DateTime ValidTo
        {
            get { return _dtValidTo; }
            set { _dtValidTo = value; }
        }


        public string Otherinfo
        {
            get { return _sOtherinfo; }
            set { _sOtherinfo = value; }
        }

        public Int64 Type
        {
            get { return _nType; }
            set { _nType = value; }
        }

        public Int64 InsuranceID
        {
            get { return _nInsuranceID; }
            set { _nInsuranceID = value; }
        }

        #endregion "Property Procedures"

    }

    public class PatientWorkersComps : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public PatientWorkersComps()
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


        ~PatientWorkersComps()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(PatientWorkersComp item)
        {
            _innerlist.Add(item);
        }



        public bool Remove(PatientWorkersComp item)
        //Remark - Work Remining for comparision
        {
            bool result = false;
            //DBParameter obj;

            //for (int i = 0; i < _innerlist.Count; i++)
            //{
            //    //store current index being checked
            //    obj = new DBParameter();
            //    obj = (DBParameter)_innerlist[i];
            //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            //    {
            //        _innerlist.RemoveAt(i);
            //        result = true;
            //        break;
            //    }
            //    obj = null;
            //}

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

        public PatientWorkersComp this[int index]
        {
            get
            { return (PatientWorkersComp)_innerlist[index]; }
        }

        public bool Contains(PatientWorkersComp item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(PatientWorkersComp item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(PatientWorkersComp[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    //This Class is used for PCP/Pharmacys/refferals
    public class PatientDetail : IDisposable
    {

        #region "Constructor & Distructor"

        public PatientDetail()
        {

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

        ~PatientDetail()
        {
            Dispose(false);
        }

        #endregion "Constructor & Distructor"

        #region "Private variables"

        private Int64 _nPatientID = 0;
        private Int64 _nPatientDetailID = 0;
        private Int64 _nContactId = 0;
        private string _sName = "";
        private string _sContact = "";
        private string _sAddressLine1 = "";
        private string _sAddressLine2 = "";
        private string _sCity = "";
        private string _sState = "";
        private string _sZIP = "";
        private string _sCounty = "";
        private string _sCountry = "";
        private string _sPhone = "";
        private string _sFax = "";
        private string _sEmail = "";
        private string _sURL = "";
        private string _sMobile = "";
        private string _sPager = "";
        private string _sNotes = "";
        private string _sFirstName = "";
        private string _sMiddleName = "";
        private string _sLastName = "";
        private string _sGender = "";
        private string _sTaxonomy = "";
        private string _sTaxonomyDesc = "";
        private string _sTaxID = "";
        private string _sUPIN = "";
        private string _sNPI = "";
        private string _sHospitalAffiliation = "";
        private string _sExternalCode = "";
        private string _sDegree = ""; //Degree will be used as prefix
        private string _sPrefix = "";

        private string _sNCPDPID = "";
        private DateTime? _dtActiveStartTime = null;
        private DateTime? _dtActiveEndTime = null;
        private string _sServiceLevel = "";
        private string _sPharmacyStatus = "";
        private Int64 _nContactStatus;

        private PatientContactType _nContactFlag = PatientContactType.None;
        private Int64 _nClinicID = 0;


        private DateTime? _dtMUTransactionDate = null;
        private bool _bMUCheckBox = false;

        #endregion "Private variables"

        #region "Property Procedures"

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 PatientDetailID
        {
            get { return _nPatientDetailID; }
            set { _nPatientDetailID = value; }
        }

        public Int64 ContactId
        {
            get { return _nContactId; }
            set { _nContactId = value; }
        }

        public string Name
        {
            get { return _sName; }
            set { _sName = value; }
        }

        public string Contact
        {
            get { return _sContact; }
            set { _sContact = value; }
        }

        public string AddressLine1
        {
            get { return _sAddressLine1; }
            set { _sAddressLine1 = value; }
        }

        public string AddressLine2
        {
            get { return _sAddressLine2; }
            set { _sAddressLine2 = value; }
        }

        public string City
        {
            get { return _sCity; }
            set { _sCity = value; }
        }

        public string State
        {
            get { return _sState; }
            set { _sState = value; }
        }

        public string ZIP
        {
            get { return _sZIP; }
            set { _sZIP = value; }
        }


        public string Country
        {
            get { return _sCountry; }
            set { _sCountry = value; }
        }

        public string County
        {
            get { return _sCounty; }
            set { _sCounty = value; }
        }

        public string Phone
        {
            get { return _sPhone; }
            set { _sPhone = value; }
        }

        public string Fax
        {
            get { return _sFax; }
            set { _sFax = value; }
        }

        public string Email
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        public string URL
        {
            get { return _sURL; }
            set { _sURL = value; }
        }

        public string Mobile
        {
            get { return _sMobile; }
            set { _sMobile = value; }
        }

        public string Pager
        {
            get { return _sPager; }
            set { _sPager = value; }
        }

        public string Notes
        {
            get { return _sNotes; }
            set { _sNotes = value; }
        }


        public string FirstName
        {
            get { return _sFirstName; }
            set { _sFirstName = value; }
        }

        public string MiddleName
        {
            get { return _sMiddleName; }
            set { _sMiddleName = value; }
        }

        public string LastName
        {
            get { return _sLastName; }
            set { _sLastName = value; }
        }

        public string Gender
        {
            get { return _sGender; }
            set { _sGender = value; }
        }

        public string Taxonomy
        {
            get { return _sTaxonomy; }
            set { _sTaxonomy = value; }
        }

        public string TaxonomyDesc
        {
            get { return _sTaxonomyDesc; }
            set { _sTaxonomyDesc = value; }
        }

        public string TaxID
        {
            get { return _sTaxID; }
            set { _sTaxID = value; }
        }

        public string UPIN
        {
            get { return _sUPIN; }
            set { _sUPIN = value; }
        }

        public string NPI
        {
            get { return _sNPI; }
            set { _sNPI = value; }
        }

        public string HospitalAffiliation
        {
            get { return _sHospitalAffiliation; }
            set { _sHospitalAffiliation = value; }
        }

        public string ExternalCode
        {
            get { return _sExternalCode; }
            set { _sExternalCode = value; }
        }

        public string Degree
        {
            get { return _sDegree; }
            set { _sDegree = value; }
        }
        public string Prefix
        {
            get { return _sPrefix; }
            set { _sPrefix = value; }
        }

        public PatientContactType ContactFlag
        {
            get { return _nContactFlag; }
            set { _nContactFlag = value; }
        }

        public Int64 ClinicID
        {
            get { return _nClinicID; }
            set { _nClinicID = value; }
        }

        public string NCPDPID
        {
            get { return _sNCPDPID; }
            set { _sNCPDPID = value; }
        }

        public DateTime? ActiveStartTime
        {
            get { return _dtActiveStartTime; }
            set { _dtActiveStartTime = value; }
        }

        public DateTime? ActiveEndTime
        {
            get { return _dtActiveEndTime; }
            set { _dtActiveEndTime = value; }
        }

        public string ServiceLevel
        {
            get { return _sServiceLevel; }
            set { _sServiceLevel = value; }
        }

        public string PharmacyStatus
        {
            get { return _sPharmacyStatus; }
            set { _sPharmacyStatus = value; }
        }

        public Int64 ContactStatus
        {
            get { return _nContactStatus; }
            set { _nContactStatus = value; }
        }

        public DateTime? MUTransactionDate
        {
            get { return _dtMUTransactionDate; }
            set { _dtMUTransactionDate = value; }
        }

        public bool MUCheckBox
        {
            get { return _bMUCheckBox; }
            set { _bMUCheckBox = value; }
        }

        #endregion "Property Procedures"

    }

    public class PatientDetails : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public PatientDetails()
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


        ~PatientDetails()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(PatientDetail item)
        {
            _innerlist.Add(item);
        }



        public bool Remove(PatientDetail item)
        //Remark - Work Remining for comparision
        {
            bool result = false;
            //DBParameter obj;

            //for (int i = 0; i < _innerlist.Count; i++)
            //{
            //    //store current index being checked
            //    obj = new DBParameter();
            //    obj = (DBParameter)_innerlist[i];
            //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            //    {
            //        _innerlist.RemoveAt(i);
            //        result = true;
            //        break;
            //    }
            //    obj = null;
            //}

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

        public PatientDetail this[int index]
        {
            get
            { return (PatientDetail)_innerlist[index]; }
        }

        public bool Contains(PatientDetail item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(PatientDetail item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(PatientDetail[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    //This Class is used for patient Guarantors
    public class PatientOtherContact : IDisposable
    {
        public enum GuarantorTypeFlag
        {
            None = 0,
            Primary = 1,
            Secondary = 2,
            Tertiary = 3,
            Inactive = 4
        }

        #region "Constructor & Destructor"


        public PatientOtherContact()
        {
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

        ~PatientOtherContact()
        {

            Dispose(false);
        }

        #endregion

        #region "Private Variables"

        private Int64 _nPatientID = 0;
        private string _sFirstName = "";
        private string _sMiddleName = "";
        private string _sLastName = "";
        private DateTime _dtDOB;
        private string _sSSN = "";
        private string _sRelation = "";
        private string _sGender = "";
        private string _sAddress1 = "";
        private string _sAddress2 = "";
        private string _sCity = "";
        private string _sState = "";
        private string _sZip = "";
        private string _sCounty = "";
        private string _sCountry = "";
        private string _sPhone = "";
        private string _sMobile = "";
        private string _sEmail = "";
        private string _sFax = "";
        private Int64 _nVisitID = 0;
        private Int64 _nAppointmentID = 0;

        private Int64 _nGuarantorAsPatientID = 0;
        private bool _bIsActive = false;
        private Int32 _nGuarantorTypeFlag = 4; // Inactive

        private PatientOtherContactType _ConatctType = PatientOtherContactType.None;

        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        private Int64 _nPatientContactID;
        private GuarantorType _nGuarantorType = GuarantorType.Personal; //personal Guarantor
        private Int64 _nPAccountID;
        //private string _sGuID;
        private bool _bIsAccountGuarantor;
        private bool _IsDataModified;

        #endregion

        #region "Property Procedures"

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public string FirstName
        {
            get { return _sFirstName; }
            set { _sFirstName = value; }
        }

        public string MiddleName
        {
            get { return _sMiddleName; }
            set { _sMiddleName = value; }
        }

        public string LastName
        {
            get { return _sLastName; }
            set { _sLastName = value; }
        }

        public DateTime DOB
        {
            get { return _dtDOB; }
            set { _dtDOB = value; }
        }

        public string SSN
        {
            get { return _sSSN; }
            set { _sSSN = value; }
        }

        public string Gender
        {
            get { return _sGender; }
            set { _sGender = value; }
        }

        public string Relation
        {
            get { return _sRelation; }
            set { _sRelation = value; }
        }

        public string AddressLine1
        {
            get { return _sAddress1; }
            set { _sAddress1 = value; }
        }

        public string AddressLine2
        {
            get { return _sAddress2; }
            set { _sAddress2 = value; }
        }

        public string City
        {
            get { return _sCity; }
            set { _sCity = value; }
        }

        public string State
        {
            get { return _sState; }
            set { _sState = value; }
        }

        public string Zip
        {
            get { return _sZip; }
            set { _sZip = value; }
        }

        public string Country
        {
            get { return _sCountry; }
            set { _sCountry = value; }
        }

        public string County
        {
            get { return _sCounty; }
            set { _sCounty = value; }
        }

        public string Phone
        {
            get { return _sPhone; }
            set { _sPhone = value; }
        }

        public string Mobile
        {
            get { return _sMobile; }
            set { _sMobile = value; }
        }

        public string Email
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        public string Fax
        {
            get { return _sFax; }
            set { _sFax = value; }
        }

        public Int64 VisitID
        {
            get { return _nVisitID; }
            set { _nVisitID = value; }
        }

        public Int64 AppointmentID
        {
            get { return _nAppointmentID; }
            set { _nAppointmentID = value; }
        }

        public PatientOtherContactType OtherConatctType
        {
            get { return _ConatctType; }
            set { _ConatctType = value; }
        }

        public Int64 GuarantorAsPatientID
        {
            get { return _nGuarantorAsPatientID; }
            set { _nGuarantorAsPatientID = value; }
        }

        public bool IsActive
        {
            get { return _bIsActive; }
            set { _bIsActive = value; }
        }

        public Int32 nGuarantorTypeFlag
        {
            get { return _nGuarantorTypeFlag; }
            set { _nGuarantorTypeFlag = value; }
        }

        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        public Int64 PatientContactID
        {
            get { return _nPatientContactID; }
            set { _nPatientContactID = value; }
        }

        public GuarantorType GurantorType
        {
            get { return _nGuarantorType; }
            set { _nGuarantorType = value; }
        }

        public Int64 PAccountID
        {
            get { return _nPAccountID; }
            set { _nPAccountID = value; }
        }

        public bool IsAccountGuarantor
        {
            get { return _bIsAccountGuarantor; }
            set { _bIsAccountGuarantor = value; }
        }

        public bool IsDataModified
        {
            get { return _IsDataModified; }
            set { _IsDataModified = value; }
        }

        #endregion
    }

    public class PatientOtherContacts : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public PatientOtherContacts()
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


        ~PatientOtherContacts()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(PatientOtherContact item)
        {
            _innerlist.Add(item);
        }



        public bool Remove(PatientOtherContact item)
        //Remark - Work Remining for comparision
        {
            bool result = false;
            //DBParameter obj;

            //for (int i = 0; i < _innerlist.Count; i++)
            //{
            //    //store current index being checked
            //    obj = new DBParameter();
            //    obj = (DBParameter)_innerlist[i];
            //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            //    {
            //        _innerlist.RemoveAt(i);
            //        result = true;
            //        break;
            //    }
            //    obj = null;
            //}

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

        public PatientOtherContact this[int index]
        {
            get
            { return (PatientOtherContact)_innerlist[index]; }
        }

        public bool Contains(PatientOtherContact item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(PatientOtherContact item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(PatientOtherContact[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }


    public class PatientPortalAccount : IDisposable
    {

        #region "Constructor & Destructor"


        public PatientPortalAccount()
        {
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

        ~PatientPortalAccount()
        {

            Dispose(false);
        }

        #endregion

        #region "Private Variables"

        private DateTime? _dtDateOfTraining;
        private bool _IsTrainingProvided = false;

        #endregion

        #region "Property Procedures"

        public DateTime? DateOfTraining
        {
            get { return _dtDateOfTraining; }
            set { _dtDateOfTraining = value; }
        }

        public bool IsTrainingProvided
        {
            get { return _IsTrainingProvided; }
            set { _IsTrainingProvided = value; }
        }

        #endregion
    }

    public class PatientRepresentative : IDisposable
    {

        #region "Constructor & Destructor"


        public PatientRepresentative()
        {
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

        ~PatientRepresentative()
        {

            Dispose(false);
        }

        #endregion

        #region "Private Variables"

        private long _nPRId = 0;
        private string _sFirstName = "";
        private string _sLastName = "";
        private DateTime _dtDOB;
        private string _sEmail = "";
        private string _sGender = "";
        private string _sPhone = "";
        private string _sUserName = "";
        private string _sPassword = "";
        private bool _IsDataModified;
        private string _sSecurityQuestion = "";
        private string _sSecurityAnswer = "";

        #endregion

        #region "Property Procedures"

        public long PRId
        {
            get { return _nPRId; }
            set { _nPRId = value; }
        }

        public string FirstName
        {
            get { return _sFirstName; }
            set { _sFirstName = value; }
        }

        public string LastName
        {
            get { return _sLastName; }
            set { _sLastName = value; }
        }

        public DateTime DOB
        {
            get { return _dtDOB; }
            set { _dtDOB = value; }
        }

        public string Email
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        public string Gender
        {
            get { return _sGender; }
            set { _sGender = value; }
        }

        public string Phone
        {
            get { return _sPhone; }
            set { _sPhone = value; }
        }

        public string UserName
        {
            get { return _sUserName; }
            set { _sUserName = value; }
        }

        public string Password
        {
            get { return _sPassword; }
            set { _sPassword = value; }
        }

        public bool IsDataModified
        {
            get { return _IsDataModified; }
            set { _IsDataModified = value; }
        }
        public string SecurityQuestion
        {
            get { return _sSecurityQuestion; }
            set { _sSecurityQuestion = value; }
        }

        public string SecurityAnswer
        {
            get { return _sSecurityAnswer; }
            set { _sSecurityAnswer = value; }
        }


        #endregion
    }

    public class PatientRepresentatives : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public PatientRepresentatives()
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


        ~PatientRepresentatives()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(PatientRepresentative item)
        {
            _innerlist.Add(item);
        }



        public bool Remove(PatientRepresentative item)
        //Remark - Work Remining for comparision
        {
            bool result = false;
            //DBParameter obj;

            //for (int i = 0; i < _innerlist.Count; i++)
            //{
            //    //store current index being checked
            //    obj = new DBParameter();
            //    obj = (DBParameter)_innerlist[i];
            //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            //    {
            //        _innerlist.RemoveAt(i);
            //        result = true;
            //        break;
            //    }
            //    obj = null;
            //}

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

        public PatientRepresentative this[int index]
        {
            get
            { return (PatientRepresentative)_innerlist[index]; }
        }

        public bool Contains(PatientRepresentative item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(PatientRepresentative item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(PatientRepresentative[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class PatientInsuranceOther
    {

        #region "Declaration"
        // private string _MessageBoxCaption = "gloPM";
        //  private string _databaseconnectionstring = "";

        private Insurances _oPInsurancesDetails;

        #endregion "Declartion End"


        #region  "Constructor"


        public PatientInsuranceOther()
        {
            _oPInsurancesDetails = new Insurances();
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
                    _oPInsurancesDetails.Dispose();
                }
            }
            disposed = true;
        }


        ~PatientInsuranceOther()
        {
        }

        #endregion "Constructor"

        #region "Public Properties"

        public Insurances InsurancesDetails
        {
            get { return _oPInsurancesDetails; }
            set { _oPInsurancesDetails = value; }
        }

        #endregion "Public Properties"



    }

    public class Insurance : IDisposable
    {
        public enum InsuranceTypeFlag
        {
            None = 0,
            Primary = 1,
            Secondary = 2,
            Tertiary = 3
        }

        #region "Constructor & Distructor"

        public Insurance()
        {

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

        ~Insurance()
        {
            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        //Name, 
        //private string _IName = "";

        //nPatientID, 
        private Int64 _PatientID = 0;
        //nInsuranceID,
        private Int64 _InsuranceID = 0;
        //sSubscriberName,
        private string _SubscriberName = "";
        //sSubscriberGender,
        private string _SubscriberGender = "";
        //sSubscriberPolicy, 
        private string _SubscriberPolicy = "";
        //sSubscriberID,
        private string _SubscriberID = "";
        //sGroup, 
        private string _Group = "";
        //sEmployer, 
        private string _Employer = "";
        //sPhone, 
        private string _Phone = "";
        //dtDOB, 
        private DateTime _DOB;
        //bPrimaryFlag,
        private Boolean _PrimaryFlag;
        //nInsuranceFlag,
        private Int64 _InsuranceFlag;
        //bDOB, 
        private Boolean _IsNotDOB;
        private Boolean _bIsSameAsPatient;
        //VARIABLE ADDED FOR VALUE OF CHECKBOX ADDRESS SAME AS PATIENT
        private Boolean _bIsAddressSameAsPatient;
        private string _sInsTypeCodeDefault = "";
        private string _sInsTypeDescriptionDefault = "";
        private string _sInsTypeCodeMedicare = "";
        private string _sInsTypeDescriptionMedicare = "";

        //Insurance other details for Scan Cards 
        private string _PayerID = "";
        private string _CopayER = "";
        private string _CopayOV = "";
        private string _CopaySP = "";
        private string _CopayUC = "";
        private string _dtEffectiveDate = "";
        private string _dtExpiryDate = "";
        //

        //20080926
        private string _SubscriberFName = "";
        private string _SubscriberMName = "";
        private string _SubscriberLName = "";
        private string _SubscriberCompanyLName = "";
        private Int64 _nRelationshipID;
        private string _sRelationshipName = "";
        private Boolean _IsNotStartDate;
        private Boolean _IsNotEndDate;
        private DateTime _dtStartDate;
        private DateTime _dtEndDate;
        private Decimal _DeductableAmount;
        private Decimal _CoveragePercent;
        private Decimal _CoPay;
        private Boolean _AssignmentofBenefit;
        private String _sEligibiltyNote = "";
        //

        private string _SubscriberAddr1 = "";
        private string _SubscriberAddr2 = "";
        private string _SubscriberState = "";
        private string _SubscriberCity = "";
        private string _SubscriberZip = "";

        private string _SubscriberCounty = "";
        private string _SubscriberCountry = "";


        // Insurance Detail Fields
        private Int64 _ContactID = 0;
        private string _InsuranceName = "";
        //SHUBHANGI
        private Int32 _index = -1;
        //END
        private string _AddressLine1 = "";
        private string _AddressLine2 = "";
        private string _City = "";
        private string _State = "";
        private string _Zip = "";
        private string _County = "";
        private string _Country = "";
        private string _InsurancePhone = "";
        private string _Fax = "";
        private string _Email = "";
        private string _Url = "";
        private string _sInsuranceTypeCode = "";
        private string _sInsuranceTypeDesc = "";
        private bool _bAccessAssignment = false;
        private bool _bStatementToPatient = false;
        private bool _bMedigap = false;
        private bool _bReferringIDInBox19 = false;
        private bool _bNameOfacilityinBox33 = false;
        private bool _bDoNotPrintFacility = false;
        private bool _b1stPointer = false;
        private bool _bBox31Blank = false;
        private bool _bShowPayment = false;
        private TypeOfBilling _nTypeOBilling = TypeOfBilling.None;
        private Int64 _nClearingHouse = 0;
        private bool _bIsClaims = false;
        private bool _bIsRemittanceAdvice = false;
        private bool _bIsRealTimeEligibility = false;
        private bool _bIsElectronicCOB = false;
        private bool _bIsRealTimeClaimStatus = false;
        private bool _bIsEnrollmentRequired = false;
        private string _sPayerPhone = "";
        private string _sWebsite = "";
        private string _sServicingState = "";
        private string _sComments = "";
        private string _sPayerPhoneExtn = "";
        //Added by Anil on 20090714
        private bool _bNotesInBox19 = false;
        private string _sOfficeID = "";

        //Added By MaheshB
        private bool _bworkerscomp = false;
        private bool _bCompnay = false;
        private bool _bautoclaim = false;

        #endregion

        #region "Property Procedures"

        //nPatientID, 
        //public string InsuranceName
        //{ get { return _IName; } set { _IName = value; } }

        //nPatientID, 
        public Int64 PatientID
        { get { return _PatientID; } set { _PatientID = value; } }

        //nInsuranceID,
        public Int64 InsuranceID
        { get { return _InsuranceID; } set { _InsuranceID = value; } }

        //sSubscriberName,
        public string SubscriberName
        { get { return _SubscriberName; } set { _SubscriberName = value; } }

        //sSubscribergender,
        public string SubscriberGender
        {
            get { return _SubscriberGender; }
            set { _SubscriberGender = value; }
        }

        //sSubscriberPolicy, 
        public string SubscriberPolicy
        { get { return _SubscriberPolicy; } set { _SubscriberPolicy = value; } }

        //sSubscriberID,
        public string SubscriberID
        { get { return _SubscriberID; } set { _SubscriberID = value; } }

        //sGroup, 
        public string Group
        { get { return _Group; } set { _Group = value; } }

        //sEmployer, 
        public string Employer
        { get { return _Employer; } set { _Employer = value; } }

        //sPhone, 
        public string Phone
        { get { return _Phone; } set { _Phone = value; } }

        //dtDOB, 
        public DateTime DOB
        { get { return _DOB; } set { _DOB = value; } }

        //bInsuranceFlag
        public Boolean PrimaryFlag
        { get { return _PrimaryFlag; } set { _PrimaryFlag = value; } }

        //nInsuranceFlag
        public Int64 InsuranceFlag
        { get { return _InsuranceFlag; } set { _InsuranceFlag = value; } }

        //bDOB, 
        public Boolean IsNotDOB
        { get { return _IsNotDOB; } set { _IsNotDOB = value; } }

        public Boolean IsSameAsPatient
        { get { return _bIsSameAsPatient; } set { _bIsSameAsPatient = value; } }

        public Boolean IsAddressSameAsPatient
        { get { return _bIsAddressSameAsPatient; } set { _bIsAddressSameAsPatient = value; } }

        public string InsTypeCodeDefault
        { get { return _sInsTypeCodeDefault; } set { _sInsTypeCodeDefault = value; } }

        public string InsTypeDescriptionDefault
        { get { return _sInsTypeDescriptionDefault; } set { _sInsTypeDescriptionDefault = value; } }

        public string InsTypeCodeMedicare
        { get { return _sInsTypeCodeMedicare; } set { _sInsTypeCodeMedicare = value; } }

        public string InsTypeDescriptionMedicare
        { get { return _sInsTypeDescriptionMedicare; } set { _sInsTypeDescriptionMedicare = value; } }

        //Insurance other details for Scan Cards 
        public string PayerID
        {
            get { return _PayerID; }
            set { _PayerID = value; }
        }
        public string CopayER
        {
            get { return _CopayER; }
            set { _CopayER = value; }
        }
        public string CopayOV
        {
            get { return _CopayOV; }
            set { _CopayOV = value; }
        }
        public string CopaySP
        {
            get { return _CopaySP; }
            set { _CopaySP = value; }
        }
        public string CopayUC
        {
            get { return _CopayUC; }
            set { _CopayUC = value; }
        }
        public string EffectiveDate
        {
            get { return _dtEffectiveDate; }
            set { _dtEffectiveDate = value; }
        }
        public string ExpiryDate
        {
            get { return _dtExpiryDate; }
            set { _dtExpiryDate = value; }
        }
        //

        // Added 20080926
        public string SubscriberFName
        {
            get { return _SubscriberFName; }
            set { _SubscriberFName = value; }
        }
        public string SubscriberMName
        {
            get { return _SubscriberMName; }
            set { _SubscriberMName = value; }
        }
        public string SubscriberLName
        {
            get { return _SubscriberLName; }
            set { _SubscriberLName = value; }
        }
        public string SubscriberCompanyLName
        {
            get { return _SubscriberCompanyLName; }
            set { _SubscriberCompanyLName = value; }
        }
        public Int64 RelationshipID
        {
            get { return _nRelationshipID; }
            set { _nRelationshipID = value; }
        }

        public string RelationshipName
        {
            get { return _sRelationshipName; }
            set { _sRelationshipName = value; }
        }

        public Boolean IsNotStartDate
        { get { return _IsNotStartDate; } set { _IsNotStartDate = value; } }

        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }

        public Boolean IsNotEndDate
        { get { return _IsNotEndDate; } set { _IsNotEndDate = value; } }

        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }
        public Decimal DeductableAmount
        {
            get { return _DeductableAmount; }
            set { _DeductableAmount = value; }
        }
        public Decimal CoveragePercent
        {
            get { return _CoveragePercent; }
            set { _CoveragePercent = value; }
        }
        public Decimal CoPay
        {
            get { return _CoPay; }
            set { _CoPay = value; }
        }
        public Boolean AssignmentofBenefit
        {
            get { return _AssignmentofBenefit; }
            set { _AssignmentofBenefit = value; }
        }

        public string SubscriberAddr1
        {
            get { return _SubscriberAddr1; }
            set { _SubscriberAddr1 = value; }
        }
        public string SubscriberAddr2
        {
            get { return _SubscriberAddr2; }
            set { _SubscriberAddr2 = value; }
        }
        public string SubscriberState
        {
            get { return _SubscriberState; }
            set { _SubscriberState = value; }
        }
        public string SubscriberCity
        {
            get { return _SubscriberCity; }
            set { _SubscriberCity = value; }

        }
        public string SubscriberZip
        {
            get { return _SubscriberZip; }
            set { _SubscriberZip = value; }

        }
        public string SubscriberCountry
        {
            get { return _SubscriberCountry; }
            set { _SubscriberCountry = value; }

        }
        public string SubscriberCounty
        {
            get { return _SubscriberCounty; }
            set { _SubscriberCounty = value; }

        }
        public string sEligibiltyInsuranceNotes
        {
            get { return _sEligibiltyNote; }
            set { _sEligibiltyNote = value; }
        }
        // Insurance Detail Fields
        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }

        //SHUBHANGI INSURANCE NODE INDEX 
        public Int32 Index
        { get { return _index; } set { _index = value; } }
        //END

        public string InsuranceName
        { get { return _InsuranceName; } set { _InsuranceName = value; } }

        public string AddrressLine1
        { get { return _AddressLine1; } set { _AddressLine1 = value; } }

        public string AddrressLine2
        { get { return _AddressLine2; } set { _AddressLine2 = value; } }

        public string City
        { get { return _City; } set { _City = value; } }

        public string State
        { get { return _State; } set { _State = value; } }

        public string ZIP
        { get { return _Zip; } set { _Zip = value; } }

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }

        }
        public string County
        {
            get { return _County; }
            set { _County = value; }

        }


        public string InsurancePhone
        { get { return _InsurancePhone; } set { _InsurancePhone = value; } }

        public string Fax
        { get { return _Fax; } set { _Fax = value; } }

        public string Email
        { get { return _Email; } set { _Email = value; } }

        public string URL
        { get { return _Url; } set { _Url = value; } }

        public string InsuranceTypeCode
        { get { return _sInsuranceTypeCode; } set { _sInsuranceTypeCode = value; } }

        public string InsuranceTypeDesc
        { get { return _sInsuranceTypeDesc; } set { _sInsuranceTypeDesc = value; } }

        public bool bAccessAssignment
        { get { return _bAccessAssignment; } set { _bAccessAssignment = value; } }

        public bool bStatementToPatient
        { get { return _bStatementToPatient; } set { _bStatementToPatient = value; } }

        public bool bMedigap
        { get { return _bMedigap; } set { _bMedigap = value; } }

        public bool bReferringIDInBox19
        { get { return _bReferringIDInBox19; } set { _bReferringIDInBox19 = value; } }

        public bool bNameOfacilityinBox33
        { get { return _bNameOfacilityinBox33; } set { _bNameOfacilityinBox33 = value; } }

        public bool bDoNotPrintFacility
        { get { return _bDoNotPrintFacility; } set { _bDoNotPrintFacility = value; } }

        public bool b1stPointer
        { get { return _b1stPointer; } set { _b1stPointer = value; } }

        public bool bBox31Blank
        { get { return _bBox31Blank; } set { _bBox31Blank = value; } }

        public bool bShowPayment
        { get { return _bShowPayment; } set { _bShowPayment = value; } }

        public TypeOfBilling nTypeOBilling
        { get { return _nTypeOBilling; } set { _nTypeOBilling = value; } }

        public Int64 nClearingHouse
        { get { return _nClearingHouse; } set { _nClearingHouse = value; } }

        public bool bIsClaims
        { get { return _bIsClaims; } set { _bIsClaims = value; } }

        public bool bIsRemittanceAdvice
        { get { return _bIsRemittanceAdvice; } set { _bIsRemittanceAdvice = value; } }

        public bool bIsRealTimeEligibility
        { get { return _bIsRealTimeEligibility; } set { _bIsRealTimeEligibility = value; } }

        public bool bIsElectronicCOB
        { get { return _bIsElectronicCOB; } set { _bIsElectronicCOB = value; } }

        public bool bIsRealTimeClaimStatus
        { get { return _bIsRealTimeClaimStatus; } set { _bIsRealTimeClaimStatus = value; } }

        public bool bIsEnrollmentRequired
        { get { return _bIsEnrollmentRequired; } set { _bIsEnrollmentRequired = value; } }

        public string sPayerPhone
        { get { return _sPayerPhone; } set { _sPayerPhone = value; } }

        public string sWebsite
        { get { return _sWebsite; } set { _sWebsite = value; } }

        public string sServicingState
        { get { return _sServicingState; } set { _sServicingState = value; } }

        public string sComments
        { get { return _sComments; } set { _sComments = value; } }

        public string sPayerPhoneExtn
        { get { return _sPayerPhoneExtn; } set { _sPayerPhoneExtn = value; } }

        //Added by Anil on 20090714
        public bool bNotesInBox19
        { get { return _bNotesInBox19; } set { _bNotesInBox19 = value; } }

        public string OfficeID
        { get { return _sOfficeID; } set { _sOfficeID = value; } }

        //Added By MaheshB
        public bool Isworkerscomp
        { get { return _bworkerscomp; } set { _bworkerscomp = value; } }

        public bool IsCompnay
        { get { return _bCompnay; } set { _bCompnay = value; } }

        public bool Isautoclaim
        { get { return _bautoclaim; } set { _bautoclaim = value; } }

        #endregion
    }

    public class Insurances : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public Insurances()
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


        ~Insurances()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(Insurance item)
        {
            _innerlist.Add(item);
        }

        public void Add(Int64 InsuranceID, string InsuranceName, string SubscriberName, string SubscriberPolicy, string SubscriberID, string Group, string Employer, string Phone, DateTime DOB, Boolean PrimaryFlag, Boolean IsNotDOB)
        {
            Insurance item = new Insurance();
            item.InsuranceID = InsuranceID;
            item.InsuranceName = InsuranceName;
            item.SubscriberName = SubscriberName;
            item.SubscriberPolicy = SubscriberPolicy;
            item.SubscriberID = SubscriberID;
            item.Group = Group;
            item.Employer = Employer;
            item.Phone = Phone;
            item.DOB = DOB;
            item.PrimaryFlag = PrimaryFlag;
            item.IsNotDOB = IsNotDOB;
            _innerlist.Add(item);

        }
        public void Add(Int64 InsuranceID, string InsuranceName, string SubscriberName, string SubscriberPolicy, string SubscriberID, string Group, string Employer, string Phone, DateTime DOB, Boolean PrimaryFlag)
        {
            Insurance item = new Insurance();
            item.InsuranceID = InsuranceID;
            item.InsuranceName = InsuranceName;
            item.SubscriberName = SubscriberName;
            item.SubscriberPolicy = SubscriberPolicy;
            item.SubscriberID = SubscriberID;
            item.Group = Group;
            item.Employer = Employer;
            item.Phone = Phone;
            item.DOB = DOB;
            item.PrimaryFlag = PrimaryFlag;
            //item.IsNotDOB = IsNotDOB;
            _innerlist.Add(item);
        }


        public bool Remove(Insurance item)
        //Remark - Work Remining for comparision
        {
            bool result = false;
            //DBParameter obj;

            //for (int i = 0; i < _innerlist.Count; i++)
            //{
            //    //store current index being checked
            //    obj = new DBParameter();
            //    obj = (DBParameter)_innerlist[i];
            //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            //    {
            //        _innerlist.RemoveAt(i);
            //        result = true;
            //        break;
            //    }
            //    obj = null;
            //}

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

        public Insurance this[int index]
        {
            get
            { return (Insurance)_innerlist[index]; }
        }

        public bool Contains(Insurance item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(Insurance item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(Insurance[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    #endregion

    public class gloInsurance
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        //Code added on 18/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messageBoxCaption = "gloPM";
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        public gloInsurance(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 18/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            //Sandip Darade 20090428
            //read messageboxcaption from  application settings
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
            //

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

        ~gloInsurance()
        {
            Dispose(false);
        }

        #endregion

        //public System.Data.DataTable GetInsurances()
        //{
        //    System.Data.DataTable _result = new System.Data.DataTable();

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    oDB.Connect(false);
        //    try
        //    {
        //        //string _sqlQuery = "Select nContactID, Isnull(sName,'') as InsuranceName, Isnull(sContact,'') as Contact, Isnull(sStreet,'')as Street , Isnull(sCity,'') as City, Isnull(sState,'') as State, Isnull(sZIP,'') as Zip, Isnull(sPhone,'') as Phone, Isnull(sFax,'') as Fax,Isnull(sMobile,'') as Mobile, Isnull(sPager,'') as Pager, Isnull(sEmail,'') as Email, Isnull(sURL,'') as URL , Isnull(sNotes,'') as Notes, nSpecialtyID, nInsuranceID, Isnull(sHospitalAffiliation,'') as HospitalAffiliation, Isnull(sContactType,'') as ContactType, Isnull(sExternalCode,'') as ExternalCode, Isnull(sDegree,'') as Degree, nClinicID, bIsBlocked from Contacts_MST where bIsBlocked= 0 and sContactType='Insurance'";
        //        //
        //        string _sqlQuery = "Select nContactID, Isnull(sName,'') as InsuranceName, Isnull(sContact,'') as Contact, Isnull(sStreet,'')as Street , Isnull(sCity,'') as City, Isnull(sState,'') as State, Isnull(sZIP,'') as Zip, Isnull(sPhone,'') as Phone, Isnull(sFax,'') as Fax,Isnull(sMobile,'') as Mobile, Isnull(sPager,'') as Pager, Isnull(sEmail,'') as Email, Isnull(sURL,'') as URL , Isnull(sNotes,'') as Notes, nSpecialtyID, nInsuranceID, Isnull(sHospitalAffiliation,'') as HospitalAffiliation, Isnull(sContactType,'') as ContactType, Isnull(sExternalCode,'') as ExternalCode, Isnull(sDegree,'') as Degree, nClinicID, bIsBlocked from Contacts_MST where (bIsBlocked= 0 and sContactType='Insurance') AND nClinicID=" + this.ClinicID + " ";
        //        //
        //        oDB.Retrive_Query(_sqlQuery, out _result);
        //    }
        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //    return _result;
        //}

        //public System.Data.DataTable GetPatientInsurances(Int64 PatientID)
        //{
        //    System.Data.DataTable _result = new System.Data.DataTable();

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    oDB.Connect(false);
        //    try
        //    {
        //        string _sqlQuery = "SELECT Contacts_MST.nContactID, Isnull(Contacts_MST.sName,'') as InsuranceName, Isnull(Contacts_MST.sContact,'') as Contact, Isnull(Contacts_MST.sStreet,'') as Street, Contacts_MST.nSpecialtyID as nSpecialtyID, Contacts_MST.nInsuranceID as nInsuranceID, Isnull(Contacts_MST.sHospitalAffiliation,'') as HospitalAffiliation, Isnull(Contacts_MST.sContactType,'') as ContactType, Isnull(Contacts_MST.sExternalCode,'') as ExternalCode, Isnull(Contacts_MST.sDegree,'') as Degree, Contacts_MST.nClinicID, Contacts_MST.bIsBlocked, Isnull(Contacts_MST.sCity,'') as City, Isnull(Contacts_MST.sState,'') as State, Isnull(Contacts_MST.sZIP,'') as Zip, Isnull(Contacts_MST.sPhone,'') as Phone, Isnull(Contacts_MST.sFax,'') as Fax, Isnull(Contacts_MST.sMobile,'') as Mobile, Isnull(Contacts_MST.sPager,'') as Pager, Isnull(Contacts_MST.sEmail,'')as Email, Isnull(Contacts_MST.sURL,'')as URL, Isnull(Contacts_MST.sNotes,'') as Notes FROM PatientInsurance_DTL INNER JOIN Contacts_MST ON PatientInsurance_DTL.nInsuranceID = Contacts_MST.nContactID where bIsBlocked= 0 and sContactType='Insurance' And PatientInsurance_DTL.nPatientID='" + PatientID + "'";
        //        oDB.Retrive_Query(_sqlQuery, out _result);
        //    }
        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //    return _result;
        //}

        public System.Data.DataTable GetInsurance(Int64 InsuranceID)
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //string _sqlQuery = "SELECT nContactID, Isnull(sContact,'') as Contact, Isnull(sName,'')as Name, Isnull(sStreet,'')as Street, Isnull(sCity,'') as City, Isnull(sState,'') as State, Isnull(sZIP,'') as Zip, Isnull(sPhone,'') as Phone, Isnull(sFax,'') as Fax, Isnull(sMobile,'') as Mobile, Isnull(sPager,'') as Pager, Isnull(sEmail,'') as Email, Isnull(sURL,'') as URL, Isnull(sNotes,'') as Notes, nSpecialtyID, nInsuranceID, Isnull(sHospitalAffiliation,'') as HospitalAffiliation, Isnull(sContactType,'') as ContactType, Isnull(sExternalCode,'') as ExternalCode, Isnull(sDegree,'') as Degree, nClinicID, bIsBlocked FROM Contacts_MST where bIsBlocked=0 and sContactType='Insurance' and  nContactID='" + InsuranceID.ToString() + "'";

                string _sqlQuery = " SELECT nInsuranceID, " +
                    " ISNULL(sInsuranceName,'') AS Name, " +
                    " ISNULL(sAddressLine1,'') AS sAddressLine1, " +
                    " ISNULL(sAddressLine2,'') AS sAddressLine2, " +
                    " Isnull(sCity,'') as City,  " +
                    " Isnull(sState,'') as State, " +
                    " Isnull(sZIP,'') as Zip, " +
                    " Isnull(sPhone,'') as Phone,  " +
                    " Isnull(sFax,'') as Fax, " +
                    " Isnull(sEmail,'') as Email, " +
                    " Isnull(sURL,'') as URL,  " +
                    " ISNULL(nInsuranceFlag,0) AS nInsuranceFlag " +
                    " FROM  " +
                    " PatientInsurance_DTL " +
                    " WHERE nInsuranceID = " + InsuranceID.ToString() + " ";

                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException)// DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public System.Data.DataTable GetInsurance(Int64 InsuranceID, Int64 PatientID)
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //string _sqlQuery = "SELECT nContactID, Isnull(sContact,'') as Contact, Isnull(sName,'')as Name, Isnull(sStreet,'')as Street, Isnull(sCity,'') as City, Isnull(sState,'') as State, Isnull(sZIP,'') as Zip, Isnull(sPhone,'') as Phone, Isnull(sFax,'') as Fax, Isnull(sMobile,'') as Mobile, Isnull(sPager,'') as Pager, Isnull(sEmail,'') as Email, Isnull(sURL,'') as URL, Isnull(sNotes,'') as Notes, nSpecialtyID, nInsuranceID, Isnull(sHospitalAffiliation,'') as HospitalAffiliation, Isnull(sContactType,'') as ContactType, Isnull(sExternalCode,'') as ExternalCode, Isnull(sDegree,'') as Degree, nClinicID, bIsBlocked FROM Contacts_MST where bIsBlocked=0 and sContactType='Insurance' and  nContactID='" + InsuranceID.ToString() + "'";

                string _sqlQuery = " SELECT nInsuranceID, " +
                    " ISNULL(sInsuranceName,'') AS Name, " +
                    " ISNULL(sAddressLine1,'') AS sAddressLine1, " +
                    " ISNULL(sAddressLine2,'') AS sAddressLine2, " +
                    " Isnull(sCity,'') as City,  " +
                    " Isnull(sState,'') as State, " +
                    " Isnull(sZIP,'') as Zip, " +
                    " Isnull(sPhone,'') as Phone,  " +
                    " Isnull(sFax,'') as Fax, " +
                    " Isnull(sEmail,'') as Email, " +
                    " Isnull(sURL,'') as URL,  " +
                    " ISNULL(nInsuranceFlag,0) AS nInsuranceFlag " +
                    " FROM  " +
                    " PatientInsurance_DTL " +
                    " WHERE nInsuranceID = " + InsuranceID.ToString() + " " +
                    " AND nPatientID = " + PatientID + "";

                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        //public System.Data.DataTable GetInsurance(Int64 InsuranceID)
        //{
        //    System.Data.DataTable _result = new System.Data.DataTable();

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    oDB.Connect(false);
        //    try
        //    {
        //        string _sqlQuery = "SELECT nContactID, Isnull(sContact,'') as Contact, Isnull(sName,'')as Name, Isnull(sStreet,'')as Street, Isnull(sCity,'') as City, Isnull(sState,'') as State, Isnull(sZIP,'') as Zip, Isnull(sPhone,'') as Phone, Isnull(sFax,'') as Fax, Isnull(sMobile,'') as Mobile, Isnull(sPager,'') as Pager, Isnull(sEmail,'') as Email, Isnull(sURL,'') as URL, Isnull(sNotes,'') as Notes, nSpecialtyID, nInsuranceID, Isnull(sHospitalAffiliation,'') as HospitalAffiliation, Isnull(sContactType,'') as ContactType, Isnull(sExternalCode,'') as ExternalCode, Isnull(sDegree,'') as Degree, nClinicID, bIsBlocked FROM Contacts_MST where bIsBlocked=0 and sContactType='Insurance' and  nContactID='" + InsuranceID.ToString() + "'";
        //        oDB.Retrive_Query(_sqlQuery, out _result);
        //    }
        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //    return _result;
        //}

        public bool AddCardInsurance(Insurance oInsurance)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = null;

            //Add Insurance to Contact Master Table.
            try
            {
                oDB.Connect(false);

                odbParams = new gloDatabaseLayer.DBParameters();
                //nPatientID, nInsuranceID, sSubscriberName, sSubscriberPolicy#, sSubscriberID, sGroup, sEmployer,
                //sPhone, dtDOB, bPrimaryFlag
                odbParams.Add("@PatientID", oInsurance.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@InsuranceID", oInsurance.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@SubscriberName", oInsurance.SubscriberName, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@SubscriberPolicy#", oInsurance.SubscriberPolicy, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@SubscriberID", oInsurance.SubscriberID, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@Employer", oInsurance.Employer, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@Group", oInsurance.Group, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@DOB", oInsurance.DOB, ParameterDirection.Input, SqlDbType.DateTime);
                odbParams.Add("@Phone", oInsurance.Phone, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@PrimaryFlag", oInsurance.PrimaryFlag, ParameterDirection.Input, SqlDbType.Bit);
                odbParams.Add("@IsNotDOB", oInsurance.IsNotDOB, ParameterDirection.Input, SqlDbType.Bit);
                odbParams.Add("@PayerID", oInsurance.PayerID, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@CopayER", oInsurance.CopayER, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@CopayOV", oInsurance.CopayOV, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@CopaySP", oInsurance.CopaySP, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@CopayUC", oInsurance.CopayUC, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@EffectiveDate", oInsurance.EffectiveDate, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@ExpiryDate", oInsurance.ExpiryDate, ParameterDirection.Input, SqlDbType.VarChar);

                int result = oDB.Execute("PA_IN_InsuranceDTL", odbParams);
                return true;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }

        }
    }

    public class Referral : IDisposable
    {
        #region "Constructor & Distructor"

        public Referral()
        {

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

        ~Referral()
        {
            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        private string _Name;
        private Int64 _ReferralID;
        //Shubhangi
        private Int64 _PharmacyID;

        private DateTime? _muDate = null;
        private bool _muCheckBox = false;

        #endregion

        #region "Property Procedures"
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public Int64 ReferralID
        {
            get { return _ReferralID; }
            set { _ReferralID = value; }
        }
        //shubhangi
        public Int64 PharmacyID
        {
            get { return _PharmacyID; }
            set { _PharmacyID = value; }
        }

        public DateTime? MuDate
        {
            get { return _muDate; }
            set { _muDate = value; }
        }

        public bool MuCheckbox
        {
            get { return _muCheckBox; }
            set { _muCheckBox = value; }
        }

        #endregion
    }

    public class Referrals : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public Referrals()
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


        ~Referrals()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(Referral item)
        {
            _innerlist.Add(item.ReferralID);
        }

        public void Add(Int64 ReferralID)
        {
            Referral item = new Referral();
            item.ReferralID = ReferralID;
            item.Name = "";
            _innerlist.Add(item);

        }

        public void Add(Int64 ReferralID, string RefferalName)
        {
            Referral item = new Referral();
            item.ReferralID = ReferralID;
            item.Name = RefferalName;
            _innerlist.Add(item);

        }



        public void Add(Int64 ReferralID, string RefferalName, DateTime? MuDate, bool MuCheckbox)
        {
            Referral item = new Referral();
            item.ReferralID = ReferralID;
            item.Name = RefferalName;
            item.MuDate = MuDate;
            item.MuCheckbox = MuCheckbox;
            _innerlist.Add(item);

        }


        public bool Remove(Referral item)
        //Remark - Work Remining for comparision
        {
            bool result = false;
            //DBParameter obj;

            //for (int i = 0; i < _innerlist.Count; i++)
            //{
            //    //store current index being checked
            //    obj = new DBParameter();
            //    obj = (DBParameter)_innerlist[i];
            //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            //    {
            //        _innerlist.RemoveAt(i);
            //        result = true;
            //        break;
            //    }
            //    obj = null;
            //}

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

        public Referral this[int index]
        {
            get
            { return (Referral)_innerlist[index]; }
        }

        public bool Contains(Referral item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(Referral item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(Referral[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }
    #region "Shubhangi"
    public class Pharmacy : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public Pharmacy()
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

        //SHUBHANGI
        ~Pharmacy()
        {
            Dispose(false);
        }
        #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        //public void Add(Pharmacy item)
        //{
        //    _innerlist.Add(item.PharmacyID);
        //}

        //public void Add(Int64 PharmacyID)
        //{
        //    Referral item = new Referral();
        //    item.ReferralID = ReferralID;
        //    item.Name = "";
        //    _innerlist.Add(item);
        //}

        public void Add(Int64 ReferralID, string RefferalName)
        {
            Referral item = new Referral();
            item.ReferralID = ReferralID;
            item.Name = RefferalName;
            _innerlist.Add(item);
        }

        public bool Remove(Referral item)
        //Remark - Work Remining for comparision
        {
            bool result = false;
            //DBParameter obj;

            //for (int i = 0; i < _innerlist.Count; i++)
            //{
            //    //store current index being checked
            //    obj = new DBParameter();
            //    obj = (DBParameter)_innerlist[i];
            //    if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            //    {
            //        _innerlist.RemoveAt(i);
            //        result = true;
            //        break;
            //    }
            //    obj = null;
            //}

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

        //public Pharmacy this[int index]
        //{
        //    get
        //    { return (Pharmacy)_innerlist[index]; }
        //}

        //public bool Contains(Pharmacy item)
        //{
        //    return _innerlist.Contains(item);
        //}

        //public int IndexOf(Pharmacy item)
        //{
        //    return _innerlist.IndexOf(item);
        //}

        //public void CopyTo(Pharmacy[] array, int index)
        //{
        //    _innerlist.CopyTo(array, index);
        //}
    }
    #endregion

    public class RelationShip : IDisposable
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "gloPM";
        public RelationShip(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            //
            //Sandip Darade 20090428
            //read messageboxcaption from  application settings
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

        ~RelationShip()
        {
            Dispose(false);
        }

        #endregion


        #region "Property Procedures"

        //nPatientRelID, sRelationship, bIsBlock, nClinicID
        private Int64 _PatientRelID = 0;
        private string _Relationship = "";
        //Code added on 01/10/2008 for Setup Patient Relationship form
        private string _RelationshipCode = "";
        private string _RelationshipDesc = "";
        private bool _IsBlock = false;
        private Int64 _ClinicID = 0;

        //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //

        public Int64 PatientRelID
        {
            get { return _PatientRelID; }
            set { _PatientRelID = value; }
        }

        public string Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; }
        }
        public string RelationshipCode
        {
            get { return _RelationshipCode; }
            set { _RelationshipCode = value; }
        }

        public string RelationshipDesc
        {
            get { return _RelationshipDesc; }
            set { _RelationshipDesc = value; }
        }

        public bool IsBlock
        {
            get { return _IsBlock; }
            set { _IsBlock = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        #endregion

        public Int64 Add()
        {
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                object _intresult = 0;
                ////nPatientRelID, sRelationshipDesc, sRelationshipCode, bIsBlock, nClinicID, bIsSystem
                oDBParameters.Add("@PatientRelID", _PatientRelID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Relationship", _Relationship, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsBlock", _IsBlock, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                _intresult = oDB.Execute("AB_INUP_PatientRelationtshipType", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException)// DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }

        public bool Modify()
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                //nPatientRelID, sRelationshipDesc, sRelationshipCode, bIsBlock, nClinicID, bIsSystem
                int _intresult = 0;
                //@AppointmentTypeID, @AppointmentType, @Duration, @ColorCode, @IsBlocked, @ClinicID
                //nPatientRelID, sRelationship, bIsBlock, nClinicID
                oDBParameters.Add("@PatientRelID", _PatientRelID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Relationship", _Relationship, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                //oDBParameters.Add("@sRelationshipDesc", _RelationshipDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                //oDBParameters.Add("@sRelationshipCode", _RelationshipCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsBlock", _IsBlock, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                _intresult = oDB.Execute("AB_INUP_PatientRelationtshipType", oDBParameters);
                if (_intresult > 0)
                {
                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }

        public Int64 AddPR()
        {
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                //nPatientRelID, sRelationshipDesc, sRelationshipCode, bIsBlock, nClinicID, bIsSystem
                object _intresult = 0;

                oDBParameters.Add("@PatientRelID", _PatientRelID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@RelationshipDesc", _RelationshipDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@RelationshipCode", _RelationshipCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsBlock", _IsBlock, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("AB_INUP_PatientRelationtship", oDBParameters, out _intresult);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;

        }

        public bool Block()
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "UPDATE PatientRelationship SET bIsBlock = 1 WHERE nPatientRelID = " + _PatientRelID;
                int _intresult = 0;
                _intresult = oDB.Execute_Query(_sqlQuery);
                if (_intresult > 0)
                {
                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from PatientRelationship where nPatientRelID =" + ID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public System.Data.DataTable GetList()
        {
            System.Data.DataTable _result = new System.Data.DataTable();
            System.Data.DataTable _result1 = null;
            System.Data.DataTable _result2 = null;
            System.Data.DataTable _result3 = null;
            System.Data.DataTable _result4 = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
                string _sqlQuery = "SELECT nPatientRelID, sRelationshipCode,sRelationshipDesc, bIsBlock, nClinicID,bIsSystem, case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS isSystem FROM patientrelationship WHERE bIsBlock = 0 and sRelationshipCode='18'";//Self
                oDB.Retrive_Query(_sqlQuery, out _result1);
                _sqlQuery = "SELECT nPatientRelID, sRelationshipCode,sRelationshipDesc, bIsBlock, nClinicID,bIsSystem, case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS isSystem FROM patientrelationship WHERE bIsBlock = 0 and sRelationshipCode='01'";//Spouse
                oDB.Retrive_Query(_sqlQuery, out _result2);
                _sqlQuery = "SELECT nPatientRelID, sRelationshipCode,sRelationshipDesc, bIsBlock, nClinicID,bIsSystem, case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS isSystem FROM patientrelationship WHERE bIsBlock = 0 and sRelationshipCode='19'";//Child
                oDB.Retrive_Query(_sqlQuery, out _result3);
                _sqlQuery = "SELECT nPatientRelID, sRelationshipCode,sRelationshipDesc, bIsBlock, nClinicID,bIsSystem, case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS isSystem FROM patientrelationship WHERE bIsBlock = 0 and sRelationshipCode<>'18' and sRelationshipCode<>'01' and sRelationshipCode<>'19' Order by sRelationshipDesc";
                oDB.Retrive_Query(_sqlQuery, out _result4);
                //string _sqlQuery = "SELECT nPatientRelID, sRelationship, bIsBlock, nClinicID FROM patientrelationship WHERE bIsBlock = 0 and nClinicID = " + ClinicID + " ";//added by Sandip Darade
                //
                if (_result1 != null)
                {
                    if (_result1.Rows.Count > 0)
                        _result.Merge(_result1);
                }
                if (_result2 != null)
                {
                    if (_result2.Rows.Count > 0)
                        _result.Merge(_result2);
                }
                if (_result3 != null)
                {
                    if (_result3.Rows.Count > 0)
                        _result.Merge(_result3);
                }
                if (_result4 != null)
                {
                    if (_result4.Rows.Count > 0)
                        _result.Merge(_result4);
                }


            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        //Start :: Releationship in the Patient Guaidain
        public System.Data.DataTable GetList(String SQry)
        {
            System.Data.DataTable _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                if (oDB != null)
                {
                    if (oDB.Connect(false))
                        try
                        {
                            string _sqlQuery = SQry;
                            oDB.Retrive_Query(_sqlQuery, out _result);

                        }
                        catch (gloDatabaseLayer.DBException)
                        {
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                        }

                }//oDB NUull
            }
            catch
            {
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }
        //End :: Releationship in the Patient Guaidain

        public System.Data.DataTable GetRelationShip(Int64 relTypeID)
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //query added by sandip Darade
                string _sqlQuery = "SELECT nPatientRelID, sRelationshipCode,sRelationshipDesc, bIsBlock, nClinicID  FROM patientrelationship where nPatientRelID='" + relTypeID + "'";
                //string _sqlQuery = "SELECT nPatientRelID, sRelationship, bIsBlock, nClinicID FROM patientrelationship where nPatientRelID='" + relTypeID + "'";
                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public bool IsExists(Int64 relTypeID, string RelCode)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                // Queries added by Sandip Darade
                string _sqlQuery = "";
                if (relTypeID == 0)
                {
                    _sqlQuery = "SELECT Count(nPatientRelID) FROM PatientRelationship WHERE sRelationshipCode ='" + RelCode.Replace("'", "''") + "'";
                    //_sqlQuery = "SELECT Count(nPatientRelID) FROM PatientRelationship WHERE sRelationship ='" + relTypeName + "'";
                }
                else
                {
                    _sqlQuery = "SELECT Count(nPatientRelID) FROM PatientRelationship WHERE sRelationshipCode ='" + RelCode.Replace("'", "''") + "' AND nPatientRelID <> " + relTypeID;
                    //_sqlQuery = "SELECT Count(nPatientRelID) FROM PatientRelationship WHERE sRelationship ='" + relTypeName + "' AND nPatientRelID <> " + relTypeID;
                }

                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        //Shubhangi 20100317
        public Int32 IsExists(Int64 relTypeID, string RelCode, string RelDescription)
        {
            Int32 _Result = 0;
            object oResult;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //For Code
                string _sqlQuery = "";
                if (relTypeID == 0) //CHECK AT THE TIME OF MODIFICATION
                {
                    _sqlQuery = "SELECT COUNT(nPatientRelID) FROM PatientRelationship WHERE sRelationshipCode ='" + RelCode.Replace("'", "''") + "'";
                }
                else
                {
                    _sqlQuery = "SELECT COUNT(nPatientRelID) FROM PatientRelationship WHERE sRelationshipCode ='" + RelCode.Replace("'", "''") + "' AND nPatientRelID <> " + relTypeID;
                }

                //object _intresult = null;
                oResult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (oResult != null && oResult.ToString() != "")
                {
                    if (Convert.ToInt64(oResult) > 0)
                    {
                        return 1;//RETURN 1 IF THE CODE IS ALREADY EXIST
                    }
                }

                // For Description
                if (relTypeID == 0) //CHECK AT THE TIME OF MODIFICATION
                {
                    _sqlQuery = "SELECT COUNT(nPatientRelID) FROM PatientRelationship WHERE sRelationshipDesc ='" + RelDescription.Replace("'", "''") + "'";
                }
                else
                {
                    _sqlQuery = "SELECT COUNT(nPatientRelID) FROM PatientRelationship WHERE sRelationshipDesc ='" + RelDescription.Replace("'", "''") + "' AND nPatientRelID <> " + relTypeID;
                }
                oResult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (oResult != null && oResult.ToString().Trim() != "")
                {
                    if (Convert.ToInt64(oResult) > 0)
                    {
                        return 2;//RETURN 2 IF THE DESCRIPTION IS ALREADY EXIST.
                    }
                }

            }
            catch { }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _Result;
        }
        //End
        //Added by Anil 20090713
        public string GetRelationshipCode(Int64 _RelationshipID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            string _result = "";
            object objResult = null;
            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT sRelationshipCode FROM PatientRelationship WHERE nPatientRelID=" + _RelationshipID + " and bIsBlock='false' and bIsBlock is null";
                objResult = oDB.ExecuteScalar_Query(_strSQL);
                if (objResult != null && Convert.ToString(objResult) != null)
                {
                    _result = Convert.ToString(objResult);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _result;
        }

        /*     public bool IsBlock(string Description)
            {
                bool _result = false;

                return true;
                //nPatientRelID, sRelationship, bIsBlock, nClinicID
                //PatientRelationship
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT ID FROM TABLE WHERE FIELD"; //Check For Transaction Table
                    object _intresult = null;
                    _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = true;
                            }
                        }
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }
        */
    }

    public class PatientCard : IDisposable
    {

        #region "Constructor & Destructor"

        public PatientCard()
        {

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

        ~PatientCard()
        {

            Dispose(false);
        }

        #endregion

        #region "Private Variables"

        private Int64 _nPatientID = 0;
        private Int64 _nCardNumber = 0;
        private Image _iCard = null;
        private DateTime _ScanDateTime = DateTime.Now;
        private Int64 _CardTypeID = 0;
        private string _CardData = "";
        private string _Username = string.Empty;
        #endregion

        #region "Property Procedures"

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 CardNumber
        {
            get { return _nCardNumber; }
            set { _nCardNumber = value; }
        }

        public Image CardImage
        {
            get { return _iCard; }
            set { _iCard = value; }
        }

        public DateTime ScannedDateTime
        {
            get { return _ScanDateTime; }
            set { _ScanDateTime = value; }
        }

        public Int64 CardTypeID
        {
            get { return _CardTypeID; }
            set { _CardTypeID = value; }
        }

        public string CardData
        {
            get { return _CardData; }
            set { _CardData = value; }
        }
        //Added by mitesh
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        #endregion

    }

    //Transaction - Line Tax(s)
    public class PatientCards
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"



        public PatientCards()
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


        ~PatientCards()
        {
            Dispose(false);
        }
        #endregion


        // Methods Add, Remove, Count , Item of TransactionLineTax
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(PatientCard item)
        {
            _innerlist.Add(item);
        }


        public bool Remove(PatientCard item)
        //Remark - Work Remining for comparision
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

        public PatientCard this[int index]
        {
            get
            { return (PatientCard)_innerlist[index]; }
        }

        public bool Contains(PatientCard item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(PatientCard item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(PatientCard[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class ScanedPatient : IDisposable
    {

        #region " Declarations "

        //Primary Information
        private string _PatientCode = "";
        string _SocialSecurity = "";
        string _Name = "";
        string _FirstName = "";
        string _MiddleName = "";
        string _LastName = "";
        string _NameSuffix = "";
        string _DOB = "";
        string _DOB4 = "";
        string _Address1 = "";
        string _City = "";
        string _Zip = "";
        string _State = "";
        string _County = "";
        string _CountryShort = "";
        //

        // ID/DL Information

        string _ID = "";
        string _License = "";
        string _Type = "";
        string _IssueDate = "";
        string _IssueDate4 = "";
        string _ExpirationDate = "";
        string _ExpirationDate4 = "";
        string _sClass = "";

        //


        // ID/DL Other Information
        string _Sex = "";
        string _Eyes = "";
        string _Hair = "";
        string _Height = "";
        string _Weight = "";
        string _SigNum = "";
        string _Audit = "";
        string _Original = "";
        string _Restriction = "";
        string _Endorsments = "";
        string _CSC = "";
        string _Fee = "";
        string _Address2 = "";
        string _Address3 = "";
        string _Address4 = "";
        string _Address5 = "";
        string _Address6 = "";
        string _DocType = "";
        string _Text1 = "";
        string _Text2 = "";
        string _Text3 = "";
        string _Duplicate = "";
        //

        private string _ProviderName = "";
        private Int64 _ProviderId = 0;



        #endregion " Declarations "


        #region "Property Procedures"

        public string PatientCode
        {
            get { return _PatientCode; }
            set { _PatientCode = value; }
        }
        public string SocialSecurity
        {
            get { return _SocialSecurity; }
            set { _SocialSecurity = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string NameSuffix
        {
            get { return _NameSuffix; }
            set { _NameSuffix = value; }
        }
        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string DOB4
        {
            get { return _DOB4; }
            set { _DOB4 = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public string County
        {
            get { return _County; }
            set { _County = value; }
        }
        public string CountryShort
        {
            get { return _CountryShort; }
            set { _CountryShort = value; }
        }



        //-------

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string License
        {
            get { return _License; }
            set { _License = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        public string IssueDate4
        {
            get { return _IssueDate4; }
            set { _IssueDate4 = value; }
        }
        public string ExpirationDate
        {
            get { return _ExpirationDate; }
            set { _ExpirationDate = value; }
        }
        public string ExpirationDate4
        {
            get { return _ExpirationDate4; }
            set { _ExpirationDate4 = value; }
        }
        public string sClass
        {
            get { return _sClass; }
            set { _sClass = value; }
        }

        //----------------

        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }
        public string Eyes
        {
            get { return _Eyes; }
            set { _Eyes = value; }
        }
        public string Hair
        {
            get { return _Hair; }
            set { _Hair = value; }
        }
        public string Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        public string Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
        public string SigNum
        {
            get { return _SigNum; }
            set { _SigNum = value; }
        }
        public string Audit
        {
            get { return _Audit; }
            set { _Audit = value; }
        }
        public string Original
        {
            get { return _Original; }
            set { _Original = value; }
        }
        public string Restriction
        {
            get { return _Restriction; }
            set { _Restriction = value; }
        }
        public string Endorsments
        {
            get { return _Endorsments; }
            set { _Endorsments = value; }
        }
        public string CSC
        {
            get { return _CSC; }
            set { _CSC = value; }
        }
        public string Fee
        {
            get { return _Fee; }
            set { _Fee = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Address3
        {
            get { return _Address3; }
            set { _Address3 = value; }
        }
        public string Address4
        {
            get { return _Address4; }
            set { _Address4 = value; }
        }
        public string Address5
        {
            get { return _Address5; }
            set { _Address5 = value; }
        }
        public string Address6
        {
            get { return _Address6; }
            set { _Address6 = value; }
        }
        public string DocType
        {
            get { return _DocType; }
            set { _DocType = value; }
        }
        public string Text1
        {
            get { return _Text1; }
            set { _Text1 = value; }
        }
        public string Text2
        {
            get { return _Text2; }
            set { _Text2 = value; }
        }
        public string Text3
        {
            get { return _Text3; }
            set { _Text3 = value; }
        }
        public string Duplicate
        {
            get { return _Duplicate; }
            set { _Duplicate = value; }
        }

        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }
        public Int64 ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }

        #endregion "Property Procedures"



        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";

        public ScanedPatient(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
        }
        public ScanedPatient()
        {

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

        ~ScanedPatient()
        {
            Dispose(false);
        }

        #endregion

    }

    public class ScanedInsurance : IDisposable
    {

        #region "Declarations"

        string _MemberID = "";
        string _MemberName = "";
        string _PlanProvider = "";
        string _GroupNumber = "";
        string _PayerID = "";
        string _EffectiveDate = "";
        string _DOB = "";
        string _ExpiryDate = "";
        string _CopayOV = "";
        string _CopayUC = "";
        string _CopaySP = "";
        string _CopayER = "";
        decimal _Copay = 0;
        string _TelList = "";
        string _EmailList = "";
        string _WebList = "";
        string _AddressList = "";
        private string _ProviderName = "";
        private Int64 _ProviderId = 0;
        private string _SocialSecurityNo = "";
        private string _PatientCode = "";
        private string _PatientFirstName = "";
        private string _PatientMiddleName = "";
        private string _PatientLastName = "";
        private string _Sex = "";
        private string _Address1 = "";
        private string _City = "";
        private string _Zip = "";
        private string _State = "";
        private string _County = "";

        #endregion "Declartions"


        #region "Property Procedures"


        public string MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        public string PlanProvider
        {
            get { return _PlanProvider; }
            set { _PlanProvider = value; }
        }
        public string GroupNumber
        {
            get { return _GroupNumber; }
            set { _GroupNumber = value; }
        }
        public string PayerID
        {
            get { return _PayerID; }
            set { _PayerID = value; }
        }
        public string EffectiveDate
        {
            get { return _EffectiveDate; }
            set { _EffectiveDate = value; }
        }
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string CopayOV
        {
            get { return _CopayOV; }
            set { _CopayOV = value; }
        }
        public string CopayUC
        {
            get { return _CopayUC; }
            set { _CopayUC = value; }
        }
        public string CopaySP
        {
            get { return _CopaySP; }
            set { _CopaySP = value; }
        }
        public string CopayER
        {
            get { return _CopayER; }
            set { _CopayER = value; }
        }
        public decimal Copay
        {
            get { return _Copay; }
            set { _Copay = value; }
        }
        public string TelList
        {
            get { return _TelList; }
            set { _TelList = value; }
        }
        public string EmailList
        {
            get { return _EmailList; }
            set { _EmailList = value; }
        }
        public string WebList
        {
            get { return _WebList; }
            set { _WebList = value; }
        }
        public string AddressList
        {
            get { return _AddressList; }
            set { _AddressList = value; }
        }

        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }
        public Int64 ProviderId
        {
            get { return _ProviderId; }
            set { _ProviderId = value; }
        }
        public string SSN
        {
            get { return _SocialSecurityNo; }
            set { _SocialSecurityNo = value; }
        }
        public string PatientCode
        {
            get { return _PatientCode; }
            set { _PatientCode = value; }
        }
        public string PatientFirstName
        {
            get { return _PatientFirstName; }
            set { _PatientFirstName = value; }
        }
        public string PatientMiddleName
        {
            get { return _PatientMiddleName; }
            set { _PatientMiddleName = value; }
        }
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }
        public string PatientLastName
        {
            get { return _PatientLastName; }
            set { _PatientLastName = value; }
        }
        public string Address
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public string County
        {
            get { return _County; }
            set { _County = value; }
        }


        #endregion "Property Procedures"


        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";

        public ScanedInsurance(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
        }

        public ScanedInsurance()
        {

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

        ~ScanedInsurance()
        {
            Dispose(false);
        }

        #endregion

    }


}

