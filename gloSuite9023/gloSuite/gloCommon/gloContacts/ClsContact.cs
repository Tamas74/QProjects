using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
namespace gloContacts
{
    public enum COntactType

    {
        Insurance = 1,
        Physician = 2,
        Pharmacy = 3,
        Hospital = 4,
        Miscellaneous = 5,
        CollectionAgency=6
        
    }

    public static class ContactType
    {


        //Pharmacy

        private static string _Pharmacy = "Pharmacy";

        //Physician

        private static string _Physician = "Physician";

        //Insurance

        private static string _Insurance = "Insurance";

        //Others

        private static string _Others = "Others";

        //Hospital

        private static string _Hospital = "Hospital";

        public static string Pharmacy
        { get { return _Pharmacy; } }

        public static string Physician
        { get { return _Physician; } }

        public static string Insurance
        { get { return _Insurance; } }

        public static string Others
        { get { return _Others; } }

        public static string Hospital
        { get { return _Hospital; } }


    }

    public class gloContact
    {

        #region "Constructor & Distructor"



        //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //
        private string _MessageBoxCaption = String.Empty;

        private string _databaseconnectionstring = "";

        public gloContact(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //

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
            //Sandip Darade  20090428
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

        ~gloContact()
        {
            Dispose(false);
        }

        #endregion


        // Procedures For ADD ,Updates ,Delete, Bock, Fill, Select, IsExist, IsBlock

        public Int64 Add(Contact oContact)
        {
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            object _intresult = 0;
           try
            {
              

                oDBParameters.Add("@ContactID", oContact.ContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Name", oContact.Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ContactName", oContact.ContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Street", oContact.Street, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@City", oContact.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@State", oContact.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ZIP", oContact.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Phone", oContact.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Fax", oContact.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Mobile", oContact.Mobile, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Pager", oContact.Pager, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Email", oContact.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@URL", oContact.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Notes", oContact.Notes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@FirstName", oContact.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@MiddleName", oContact.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@LastName", oContact.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Gender", oContact.Gender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@SpecialtyID", oContact.SpecialtyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@InsuranceID", oContact.InsuranceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@HospitalAffiliation", oContact.HospitalAffiliation, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ContactType", oContact.ContactType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ExternalCode", oContact.ExternalCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Degree", oContact.Degree, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ClinicID", oContact.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDBParameters.Add("@Taxonomy", oContact.Taxonomy, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@TaxonomyDesc", oContact.TaxonomyDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@TaxID", oContact.TaxID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@UPIN", oContact.UPIN, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@NPI", oContact.NPI, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sInsuranceTypeCode", oContact.InsuranceTypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sInsuranceTypeDesc", oContact.InsuranceTypeDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsBlocked", oContact.IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                //_intresult = oDB.Execute("CO_INUP_Contacts_MST", oDBParameters, out  _intresult);
                int result = oDB.Execute("CO_INUP_Contacts_MST", oDBParameters, out  _intresult);
                //

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                _intresult = null;
            }
            return _result;
        }

        public Int64 AddDetails(ContactDetails ocdet)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {
                if (ocdet.Count > 0)
                    oDB.Execute_Query("DELETE FROM Contacts_Detail  WHERE nContactId = " + ocdet[0].ContactID);

                //@ContactsDetailID,@ContactId, @InsuranceCode, @InsuranceDecs, @Description, @Value, @Type
                for (int i = 0; i < ocdet.Count; i++)
                {
                    oDBParameters.Clear();
                    object _intresult = 0;
                    oDBParameters.Add("@ContactID", ocdet[i].ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ContactsDetailID", ocdet[i].ContactDetailID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@InsuranceCode", ocdet[i].InsuranceCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@InsuranceDesc", ocdet[i].InsuranceDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Description", ocdet[i].Description, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Value", ocdet[i].Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Type", ocdet[i].Type, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    int result = oDB.Execute("CO_INUP_CONTACTS_DETAIL", oDBParameters, out  _intresult);
                    //

                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = Convert.ToInt64(_intresult);
                            }
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }

        public bool IsExists(Int64 ContactId, string InsuranceDescription, string Hospital)
        {
            bool _result = false;

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //oDB.Connect(false);
            //try
            //{
            //    string _sqlQuery = "";
            //    if (ContactId == 0)
            //    {
            //        _sqlQuery = "SELECT Count(nContactID) FROM Contacts_Detail WHERE sInsuranceDecs = '" + InsuranceDescription + "'";
            //    }
            //    else
            //    {
            //        _sqlQuery = "SELECT Count(nLocationID) FROM Contacts_Detail WHERE sInsuranceDecs = '" + InsuranceDescription + "' ";

            //    }
            //    object _intresult = null;
            //    _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
            //    if (_intresult != null)
            //    {
            //        if (_intresult.ToString().Trim() != "")
            //        {
            //            if (Convert.ToInt64(_intresult) > 0)
            //            {
            //                _result = true;
            //            }
            //        }
            //    }
            //}
            //catch (gloDatabaseLayer.DBException DBErr)
            //{
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            //    oDB.Disconnect();
            //    oDB.Dispose();
            //}
            return _result;
        }


        public bool Modify(Int64 contactid, Contact oContact)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                int _intresult = 0;

                //nContactID, sName, sContact, sStreet, sCity, sState, sZIP, sPhone, sFax, sMobile, sPager, sEmail, sURL, 
                //sNotes, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, nInsuranceID, sHospitalAffiliation, 
                //sContactType, sExternalCode, sDegree, nClinicID, bIsBlocked

                oDBParameters.Add("@ContactID", oContact.ContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Name", oContact.Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ContactName", oContact.ContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Street", oContact.Street, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@City", oContact.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@State", oContact.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ZIP", oContact.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Phone", oContact.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Fax", oContact.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Mobile", oContact.Mobile, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Pager", oContact.Pager, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Email", oContact.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Notes", oContact.Notes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@FirstName", oContact.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@MiddleName", oContact.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@LastName", oContact.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Gender", oContact.Gender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@SpecialtyID", oContact.SpecialtyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@InsuranceID", oContact.InsuranceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@HospitalAffiliation", oContact.HospitalAffiliation, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ContactType", oContact.ContactType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ExternalCode", oContact.ExternalCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Degree", oContact.Degree, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ClinicID", oContact.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                oDBParameters.Add("@Taxonomy", oContact.Taxonomy, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@TaxID", oContact.TaxID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@UPIN", oContact.UPIN, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@NPI", oContact.NPI, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsBlocked", oContact.IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);


                _intresult = oDB.Execute("CO_INUP_Contacts_MST", oDBParameters);
                if (_intresult > 0)
                {
                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }

        public bool Block(Int64 contactid)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "UPDATE Contacts_MST SET bIsBlocked = 1 WHERE nContactId =" + contactid;
                int _intresult = 0;
                _intresult = oDB.Execute_Query(_sqlQuery);
                if (_intresult > 0)
                {
                 //   if (ContactType == COntactType.Physician.ToString())
                 //   {
                 //        _sqlQuery = "DELETE  Contacts_Physician_DTL  WHERE nContactId =" + contactid+" AND nClinicID = "+_ClinicID +" ";
                 //        _intresult = oDB.Execute_Query(_sqlQuery);

                 //        _sqlQuery = "DELETE  Contacts_Detail  WHERE nContactId =" + contactid + " ";
                 //        _intresult = oDB.Execute_Query(_sqlQuery);
                 //   }
                 //   if (ContactType == COntactType.Insurance.ToString())
                 //   {
                 //       _sqlQuery = "DELETE  Contacts_Insurance_DTL  WHERE nContactId =" + contactid + " AND nClinicID = " + _ClinicID + " ";
                 //       _intresult = oDB.Execute_Query(_sqlQuery);
                 //   }
                 //_intresult =  oDB.Execute_Query(_sqlQuery);
                 //if (_intresult > 0)
                 {
                     _result = true;
                 }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            return _result;
        }

        public bool Delete()
        {
            return true;
        }

        public System.Data.DataTable Fill()
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = null;
            oDB.Connect(false);
            try
            {
                //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
                //string _sqlQuery = "SELECT nContactID, sName, sContact, sStreet, sCity, sState, sZIP, sPhone, sFax, sMobile, sPager, sEmail, sURL, sNotes, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, nInsuranceID, sHospitalAffiliation, sContactType, sExternalCode, sDegree, nClinicID, bitIsBlocked FROM Contacts_MST WHERE bIsBlocked = 0 ";
                _sqlQuery = "SELECT nContactID, sName, sContact, sStreet, sCity, sState, sZIP, sPhone, sFax, sMobile, sPager, sEmail, sURL, sNotes, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, nInsuranceID, sHospitalAffiliation, sContactType, sExternalCode, sDegree, nClinicID, bitIsBlocked FROM Contacts_MST WHERE bIsBlocked = 0 AND nClinicID = " + ClinicID + "";
                //
                oDB.Retrive_Query(_sqlQuery, out  _result);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }
            return _result;
        }

        public Contact select(Int64 contactid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();
            odbparam.Add("@ContactID", contactid, ParameterDirection.Input, SqlDbType.BigInt);
            Contact oContact = new Contact();
            DataTable dt;
            try
            {
                oDB.Connect(false);
                oDB.Retrive("CO_Select_Contact", odbparam, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        //nContactID, sName, sContact, sStreet, sCity, sState, sZIP, sPhone, sFax, sMobile, 
                        //sPager, sEmail, sURL, sNotes, sFirstName, sMiddleName, sLastName, sGender, 
                        //nSpecialtyID, nInsuranceID, sHospitalAffiliation, sContactType, sExternalCode, 
                        //sDegree, nClinicID, bIsBlocked

                        oContact.Name = dt.Rows[0]["sName"].ToString();
                        oContact.ContactName = dt.Rows[0]["sContact"].ToString();
                        oContact.Street = dt.Rows[0]["sStreet"].ToString();
                        oContact.City = dt.Rows[0]["sCity"].ToString();
                        oContact.State = dt.Rows[0]["sState"].ToString();
                        oContact.ZIP = dt.Rows[0]["sZIP"].ToString();
                        oContact.Phone = dt.Rows[0]["sPhone"].ToString();
                        oContact.Fax = dt.Rows[0]["sFax"].ToString();
                        oContact.Mobile = dt.Rows[0]["sMobile"].ToString();
                        oContact.Pager = dt.Rows[0]["sPager"].ToString();
                        oContact.Email = dt.Rows[0]["sEmail"].ToString();
                        oContact.URL = dt.Rows[0]["sURL"].ToString();
                        oContact.Notes = dt.Rows[0]["sNotes"].ToString();
                        oContact.FirstName = dt.Rows[0]["sFirstName"].ToString();
                        oContact.MiddleName = dt.Rows[0]["sMiddleName"].ToString();
                        oContact.LastName = dt.Rows[0]["sLastName"].ToString();
                        oContact.Gender = dt.Rows[0]["sGender"].ToString();
                        if (dt.Rows[0]["nSpecialtyID"].ToString() != "" && dt.Rows[0]["nSpecialtyID"] != null)
                            oContact.SpecialtyID = Convert.ToInt32(dt.Rows[0]["nSpecialtyID"].ToString());
                        if (dt.Rows[0]["nInsuranceID"].ToString() != "" && dt.Rows[0]["nInsuranceID"] != null)
                            oContact.InsuranceID = Convert.ToInt32(dt.Rows[0]["nInsuranceID"].ToString());
                        oContact.HospitalAffiliation = dt.Rows[0]["sHospitalAffiliation"].ToString();
                        oContact.ContactType = dt.Rows[0]["sContactType"].ToString();
                        oContact.ExternalCode = dt.Rows[0]["sExternalCode"].ToString();
                        oContact.Degree = dt.Rows[0]["sDegree"].ToString();
                        if (dt.Rows[0]["nClinicID"].ToString() != "" && dt.Rows[0]["nClinicID"] != null)
                            oContact.ClinicID = Convert.ToInt32(dt.Rows[0]["nClinicID"].ToString());
                        if (dt.Rows[0]["bIsBlocked"].ToString() != "")
                            oContact.IsBlocked = Convert.ToBoolean(dt.Rows[0]["bIsBlocked"].ToString());

                        oContact.Taxonomy = dt.Rows[0]["Taxonomy"].ToString();
                        oContact.TaxonomyDesc = dt.Rows[0]["TaxonomyDesc"].ToString();
                        oContact.TaxID = dt.Rows[0]["TaxID"].ToString();
                        oContact.UPIN = dt.Rows[0]["UPIN"].ToString();
                        oContact.NPI = dt.Rows[0]["NPI"].ToString();
                        oContact.InsuranceTypeCode = dt.Rows[0]["sInsuranceTypeCode"].ToString();
                        oContact.InsuranceTypeDesc = dt.Rows[0]["sInsuranceTypeDesc"].ToString();
                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (odbparam != null) { odbparam.Dispose(); odbparam = null; }
            }
            return oContact;
        }

        public ContactDetails Selectdetails(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();
            string strSql = " SELECT  ISNULL(sInsuranceCode,'') AS sInsuranceCode , ISNULL(sInsuranceDecs,'') AS sInsuranceDecs, ISNULL(sDescription,'') AS sDescription ,ISNULL(sValue,'') AS  sValue,ISNULL( nType,0) AS nType  FROM Contacts_Detail  WHERE nContactId = " + ContactID + "";
            ContactDetail oCDetail = new ContactDetail();
            ContactDetails ocdet = new ContactDetails();
            DataTable dt;
            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query(strSql, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            oCDetail = new ContactDetail();
                            oCDetail.InsuranceCode = dt.Rows[i]["sInsuranceCode"].ToString();
                            oCDetail.InsuranceDesc = dt.Rows[i]["sInsuranceDecs"].ToString();
                            oCDetail.Description = dt.Rows[i]["sDescription"].ToString();
                            oCDetail.Value = dt.Rows[i]["sValue"].ToString();
                            oCDetail.Type = Convert.ToInt16(dt.Rows[i]["nType"]);
                            ocdet.Add(oCDetail);
                        }

                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                strSql = null;
                if (oCDetail != null) { oCDetail.Dispose(); oCDetail = null; }
            }
            return ocdet;
        }

        public bool IsExists(Int64 ContactId, string sLocation)
        {
            bool _result = false;

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //oDB.Connect(false);
            //try
            //{
            //    string _sqlQuery = "";
            //    if (ContactId == 0)
            //    {
            //        _sqlQuery = "SELECT Count(nContactID) FROM Contacts_MST WHERE sLocation = '" + sLocation + "'";
            //    }
            //    else
            //    {
            //        _sqlQuery = "SELECT Count(nLocationID) FROM AB_Location WHERE sLocation = '" + sLocation + "' AND nLocationID <> " + ContactId;
            //    }
            //    object _intresult = null;
            //    _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
            //    if (_intresult != null)
            //    {
            //        if (_intresult.ToString().Trim() != "")
            //        {
            //            if (Convert.ToInt64(_intresult) > 0)
            //            {
            //                _result = true;
            //            }
            //        }
            //    }
            //}
            //catch (gloDatabaseLayer.DBException DBErr)
            //{
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            //    oDB.Disconnect();
            //    oDB.Dispose();
            //}
            return _result;
        }

        public bool IsBlock(string Description)
        {
            bool _result = false;
            object _intresult = null;
            string _sqlQuery = null;
            // return true;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                _sqlQuery = "SELECT ncontactID FROM Contacts_MST WHERE bIsBlocked=" + true; //Check For Transaction Table
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
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _intresult = null;
                _sqlQuery = null;
            }
            return _result;
        }


        public void ShowContactDetails(Int64 ContactID)
        {
            //frmSetupContact ofrmSetupContact = new frmSetupContact(_databaseconnectionstring);
            //ofrmSetupContact.ContactID = ContactID;
            //ofrmSetupContact.ShowDialog();
        }

        public DataTable GetDirectPhysician(string sSearchText = "")
        {
            //Added method to resolve Resolved Catelogue searching issue
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = null;
            oDB.Connect(false);
            DataTable dt = null;
            try
            {
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSearchText", sSearchText, ParameterDirection.Input, SqlDbType.Text);
                oDB.Connect(false);
                oDB.Retrive("SurescriptGetDirectPhysician", oDBParameters, out dt);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                dt = null;
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                //if (dt != null) { dt.Dispose(); dt = null; }
            }
            return dt;
        }

        public DataTable GetContacts(string ContactType,string sSpecialityType="")
        {
            //gloAuditTrail.gloAuditTrail.UpdatePILog("Start GetContacts");  
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = null;
            oDB.Connect(false);
            DataTable dt = null;
            //string _strSQL = "";

            try
            {
                //code commented for optimization in 6020
                //switch (ContactType)
                //{
                //    case "Pharmacy":
                //        {
                //            //Query for Addressline1 and Addressline2
                //            ////_strSQL = "SELECT nContactID as ContactID, sName as  [Name],sContact as Contact, sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE  bIsBlocked = 0 AND sContactType= '" + ContactType + "' AND nClinicID =" + ClinicID + "";
                //            _strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ ISNULL(sLastName,'') AS [PhysicianName], " +
                //            "ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, ISNULL(sAddressLine2,'') AS AddressLine2, " +
                //            "ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            " FROM Contacts_MST WHERE  ISNULL(bIsBlocked,0) = 0 AND sContactType= '" + ContactType + "' AND ISNULL(sNCPDPID,'')=''  AND  ISNULL(nClinicID,1) =" + ClinicID + " ORDER BY sName";
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }
                //        break;
                //    //Shubhangi 20091104
                //    case "Insurance Company":
                //        {
                //            //Shubhangi 20100215
                //            //Retrive address details for Insurance company
                //            //_strSQL = "select nID,sCode,sDescription,nClinicID from Contacts_InsuranceCompany_MST";
                //            _strSQL = " SELECT ISNULL(nID,0) AS nID, ISNULL(sCode,'') AS sCode,ISNULL(sDescription,'') AS sDescription, ISNULL(sAddressLine1,'') AS sAddressLine1 , " +
                //                    " ISNULL(sAddressLine2,'') AS sAddressLine2, ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZip,'') AS sZip, ISNULL(nClinicID,1) AS nClinicID " +
                //                    " FROM Contacts_InsuranceCompany_MST ORDER BY sDescription";
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }
                //        break;
                //    // abhisekh pandey 10/02/2010 start----------------------------
                //    case "Insurance Reporting Category":
                //        {
                //            _strSQL = "select nID,sCode,sDescription,nClinicID from Contacts_InsuranceReportingCategory_MST ORDER BY sDescription";
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }
                //        break;
                //    // abhisekh pandey end  ----------------------------

                //    case "e-Pharmacy":
                //        {
                //            //Query for Addressline1 and Addressline2
                //            ////_strSQL = "SELECT nContactID as ContactID, sName as  [Name],sContact as Contact, sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE  bIsBlocked = 0 AND sContactType= '" + ContactType + "' AND nClinicID =" + ClinicID + "";
                //            _strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ ISNULL(sLastName,'') AS [PhysicianName], " +
                //            "ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, ISNULL(sAddressLine2,'') AS AddressLine2, " +
                //            "ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            " FROM Contacts_MST WHERE  ISNULL(bIsBlocked,0) = 0 AND sContactType= 'Pharmacy' AND (sNCPDPID is not null) AND (sNCPDPID <> '') AND ISNULL(nClinicID,1) =" + ClinicID + " ORDER BY sName";
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }

                //        break;
                //    case "New Rx":
                //        {
                //            //Query for Addressline1 and Addressline2
                //            ////_strSQL = "SELECT nContactID as ContactID, sName as  [Name],sContact as Contact, sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE  bIsBlocked = 0 AND sContactType= '" + ContactType + "' AND nClinicID =" + ClinicID + "";
                //            _strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ ISNULL(sLastName,'') AS [PhysicianName], " +
                //            "ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, ISNULL(sAddressLine2,'') AS AddressLine2, " +
                //            "ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            " FROM Contacts_MST WHERE  ISNULL(bIsBlocked,0) = 0 AND sContactType= 'Pharmacy' AND (sNCPDPID is not null) and (sNCPDPID <> '') AND sServiceLevel = '1' AND ISNULL(nClinicID,1) =" + ClinicID + " ORDER BY sName";
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }

                //        break;
                //    case "New Rx & Refill Request":
                //        {
                //            //Query for Addressline1 and Addressline2
                //            ////_strSQL = "SELECT nContactID as ContactID, sName as  [Name],sContact as Contact, sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE  bIsBlocked = 0 AND sContactType= '" + ContactType + "' AND nClinicID =" + ClinicID + "";
                //            _strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ ISNULL(sLastName,'') AS [PhysicianName], " +
                //            "ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, ISNULL(sAddressLine2,'') AS AddressLine2, " +
                //            "ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            " FROM Contacts_MST WHERE  ISNULL(bIsBlocked,0) = 0 AND sContactType= 'Pharmacy' AND (sNCPDPID is not null) AND (sNCPDPID <> '') and sServiceLevel = '3' AND ISNULL(nClinicID,1) =" + ClinicID + " ORDER BY sName";
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }

                //        break;
                //    case "Other":
                //        {
                //            //Query for Addressline1 and Addressline2
                //            ////_strSQL = "SELECT nContactID as ContactID, sName as  [Name],sContact as Contact, sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE  bIsBlocked = 0 AND sContactType= '" + ContactType + "' AND nClinicID =" + ClinicID + "";
                //            _strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ ISNULL(sLastName,'') AS [PhysicianName], " +
                //            "ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, ISNULL(sAddressLine2,'') AS AddressLine2, " +
                //            "ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            " FROM Contacts_MST WHERE  ISNULL(bIsBlocked,0) = 0 AND sContactType= 'Pharmacy' AND (sNCPDPID is not null) AND (sNCPDPID <> '') AND (sServiceLevel <> '1') AND (sServiceLevel <> '3') AND ISNULL(nClinicID,1) =" + ClinicID + " ORDER BY sName";
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }

                //        break;
                //    case "Insurance Plan":
                //        {
                //            //Query for Addressline1 and Addressline2

                //            ////_strSQL = "SELECT nContactID as ContactID, sName as  [Name], sContact as Contact, sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE   bIsBlocked = 0 AND  sContactType= '" + ContactType + "'AND nClinicID=" + ClinicID + "";
                //            //Query for street

                //            _strSQL = " SELECT   Contacts_MST.nContactID AS ContactID, ISNULL(Contacts_MST.sFirstName, '') + SPACE(1) + ISNULL(Contacts_MST.sMiddleName, '') + SPACE(1) " +
                //            " + ISNULL(Contacts_MST.sLastName, '') AS PhysicianName, ISNULL(Contacts_MST.sLastName, '') AS LastName, ISNULL(Contacts_MST.sName, '') AS sName,  " +
                //            " ISNULL(Contacts_MST.sContact, '') AS ContactName, ISNULL(Contacts_MST.sGender, '') AS Gender, ISNULL(Contacts_MST.sAddressLine1, '') AS AddressLine1,  " +
                //             " ISNULL(Contacts_MST.sAddressLine2, '') AS AddressLine2, ISNULL(Contacts_MST.sCity, '') AS City, ISNULL(Contacts_MST.sState, '') AS State, " +
                //            " ISNULL(Contacts_MST.sZIP, '') AS ZIP, ISNULL(Contacts_MST.sPhone, '') AS Phone, ISNULL(Contacts_MST.sFax, '') AS FAX, ISNULL(Contacts_MST.sMobile, '')  " +
                //             " AS Mobile, ISNULL(Contacts_MST.sEmail, '') AS Email,  ISNULL(Contacts_Insurance_DTL.sInsuranceTypeDesc,'') AS InsuranceTypeDesc , ISNULL(Contacts_Insurance_DTL.sInsuranceTypeCode,'') AS InsuranceTypeCode " +
                //             " FROM  Contacts_MST LEFT OUTER JOIN Contacts_Insurance_DTL ON Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID  " +
                //             " WHERE  ISNULL(Contacts_MST.bIsBlocked,0) = 0 AND Contacts_MST.sContactType= '" + ContactType + "' AND ISNULL(Contacts_MST.nClinicID,1) =" + ClinicID + " ORDER BY sName ";
                //            oDB.Retrive_Query(_strSQL, out dt);


                //        }
                //        break;
                //    case "Physician":
                //        {
                //            //Query for Addressline1 and Addressline2
                //            ////_strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ ISNULL(sLastName,'') AS [Name],ISNULL(sLastName,'') AS LastName,ISNULL(sGender,'') AS Gender,  sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity AS City, sState AS State, sZIP AS ZIP, sPhone AS Phone, sFax AS FAX, sMobile AS Mobile, sEmail AS Email FROM Contacts_MST WHERE   bIsBlocked = 0 AND  sContactType= '" + ContactType + "'AND nClinicID=" + ClinicID + "";
                //            //_strSQL = "SELECT nContactID as ContactID,   ISNULL(sFirstName,'')+' '+ISNULL(sMiddleName,'')+' '+ ISNULL(sLastName,'') AS [Name], sContact as Contact, sStreet as Street, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email, sHospitalAffiliation as [Hospital Affiliation], sDegree as Degree FROM Contacts_MST WHERE   bIsBlocked = 0 AND  sContactType= '" + ContactType + "'AND nClinicID=" + ClinicID + "";
                //            //FirstName first 
                //            //_strSQL = "SELECT nContactID as ContactID,   ISNULL(sFirstName,'')+' '+ISNULL(sMiddleName,'')+' '+ ISNULL(sLastName,'') AS [Name],sGender as Gender, sStreet as Street, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE   bIsBlocked = 0 AND  sContactType= '" + ContactType + "'AND nClinicID=" + ClinicID + "";
                //            //Lastname first 
                //            //Query for street
                //            //COMMENTED BY SHUBHANGI

                //            //_strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ CASE WHEN sMiddleName <>   '' THEN ISNULL(sMiddleName,'')+SPACE(1) WHEN sMiddleName <> Null THEN ISNULL(sMiddleName,'')+SPACE(1) ELSE '' END"+ 
                //            //"+ ISNULL(sLastName,'') AS [PhysicianName], " +
                //            //"ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, ISNULL(sAddressLine2,'') AS AddressLine2, " +
                //            //"ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            //" FROM Contacts_MST WHERE  ISNULL(bIsBlocked,0) = 0 AND sContactType= '" + ContactType + "' AND ISNULL(nClinicID,1) =" + ClinicID + ""; _strSQL += " ORDER BY PhysicianName";

                //            //ADDED BY SHUBHANGI COZ WE ARE DISPLAYIONG THE PREFIX & SUFFIX IF THEY PRESENT 20100608
                //            //_strSQL = "SELECT Contacts_MST.nContactID AS ContactID,CASE WHEN Contacts_Physician_DTL.sPrefix <>   '' THEN ISNULL(Contacts_Physician_DTL.sPrefix,'') +SPACE(1) " +
                //            //          "WHEN Contacts_Physician_DTL.sPrefix <>  Null THEN ISNULL(Contacts_Physician_DTL.sPrefix,'') +SPACE(1) ELSE '' END+ " +
                //            //          "ISNULL(sFirstName,'')+ SPACE(1)+ CASE WHEN sMiddleName <>   '' " +
                //            //          "THEN ISNULL(sMiddleName,'')+SPACE(1) WHEN sMiddleName <> Null THEN ISNULL(sMiddleName,'')+SPACE(1) ELSE '' END+ " +
                //            //          "ISNULL(sLastName,'')  +SPACE(1)+ CASE WHEN Contacts_Physician_DTL.sDegree <>   '' THEN ISNULL(Contacts_Physician_DTL.sDegree,'') +SPACE(1) " +
                //            //          "WHEN Contacts_Physician_DTL.sDegree <>  Null THEN ISNULL(Contacts_Physician_DTL.sDegree,'') ELSE '' END " +
                //            //          "AS [PhysicianName], ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName, " +
                //            //          "ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, " +
                //            //          "ISNULL(sAddressLine2,'') AS AddressLine2, ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, " +
                //            //          "ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            //          "FROM Contacts_MST left outer join Contacts_Physician_DTL " +
                //            //          "ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID WHERE  ISNULL(Contacts_MST.bIsBlocked,0) = 0 AND Contacts_MST.sContactType= 'Physician' AND ISNULL(Contacts_MST.nClinicID,1) =1 " +
                //            //          "ORDER BY PhysicianName";
                //            //ADEED BY SHUBHANGI 20100615 ADDED FIRST NAME COULMN FOR SEARCH (THERE IS LAST NAME COLUMN FOR THE SAME PURPOSE FIRST NAME COLUMN AFTER ADDING PREFIX & SUFFIX)
                //            _strSQL = "SELECT Contacts_MST.nContactID AS ContactID," +
                //                        "CASE WHEN Contacts_Physician_DTL.sPrefix <>   '' THEN ISNULL(Contacts_Physician_DTL.sPrefix,'') +SPACE(1) " +
                //                        " WHEN Contacts_Physician_DTL.sPrefix <>  Null THEN ISNULL(Contacts_Physician_DTL.sPrefix,'') +SPACE(1) " +
                //                        "ELSE '' END+ ISNULL(sFirstName,'')+ SPACE(1)+ " +
                //                        "CASE WHEN sMiddleName <>   '' THEN ISNULL(sMiddleName,'')+SPACE(1) WHEN sMiddleName <> Null THEN " +
                //                        "ISNULL(sMiddleName,'')+SPACE(1) ELSE '' END+ ISNULL(sLastName,'')  +SPACE(1)+ CASE " +
                //                        "WHEN Contacts_Physician_DTL.sDegree <>   '' THEN ISNULL(Contacts_Physician_DTL.sDegree,'') +SPACE(1)" +
                //                        " WHEN Contacts_Physician_DTL.sDegree <>  Null THEN ISNULL(Contacts_Physician_DTL.sDegree,'') ELSE '' END" +
                //                        " AS [PhysicianName], ISNULL(sFirstName,'') AS FirstName, ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,  " +
                //                        "ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, " +
                //                        "ISNULL(sAddressLine2,'') AS AddressLine2, ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State," +
                //                        " ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile," +
                //                        " ISNULL(sEmail,'') AS Email FROM Contacts_MST left outer join" +
                //                        " Contacts_Physician_DTL ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID WHERE " +
                //                        " ISNULL(Contacts_MST.bIsBlocked,0) = 0 AND Contacts_MST.sContactType= 'Physician' AND" +
                //                        " ISNULL(Contacts_MST.nClinicID,1) =1 ORDER BY PhysicianName";
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }
                //        break;
                //    case "Hospital":
                //        {
                //            //Query for Addressline1 and Addressline2
                //            ////_strSQL = "SELECT nContactID as ContactID,  sName as  [Name],  sContact as Contact, sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE   bIsBlocked = 0 AND  sContactType= '" + ContactType + "'AND nClinicID=" + ClinicID + "";
                //            //Query for street
                //            _strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ ISNULL(sLastName,'') AS [PhysicianName], " +
                //            "ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, ISNULL(sAddressLine2,'') AS AddressLine2, " +
                //            "ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            " FROM Contacts_MST WHERE  ISNULL(bIsBlocked,0) = 0 AND sContactType= '" + ContactType + "' AND ISNULL(nClinicID,1) =" + ClinicID + " ORDER BY sName ";                             //
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }

                //        break;
                //    case "Others":
                //        {
                //            //Query for Addressline1 and Addressline2
                //            ////_strSQL = "SELECT nContactID as ContactID,  sName as  [Name],  sContact as Contact, sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email FROM Contacts_MST WHERE   bIsBlocked = 0 AND  sContactType= '" + ContactType + "'AND nClinicID=" + ClinicID + "";
                //            //Query for street
                //            _strSQL = "SELECT nContactID AS ContactID,ISNULL(sFirstName,'')+ SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ ISNULL(sLastName,'') AS [PhysicianName], " +
                //            "ISNULL(sLastName,'') AS LastName,ISNULL(sName,'') AS sName,ISNULL(sContact,'') AS ContactName,ISNULL(sGender,'') AS Gender,ISNULL(sAddressLine1,'') AS AddressLine1, ISNULL(sAddressLine2,'') AS AddressLine2, " +
                //            "ISNULL(sCity,'') AS City, ISNULL(sState,'') AS State, ISNULL(sZIP,'')  AS ZIP, ISNULL(sPhone,'') AS Phone,ISNULL(sFax,'') AS FAX, ISNULL(sMobile,'') AS Mobile, ISNULL(sEmail,'') AS Email " +
                //            " FROM Contacts_MST WHERE  ISNULL(bIsBlocked,0) = 0 AND sContactType= '" + ContactType + "' AND ISNULL(nClinicID,1) =" + ClinicID + " ORDER BY sName ";                             //
                //            oDB.Retrive_Query(_strSQL, out dt);
                //        }
                //        break;
                //    default:
                //        {
                //            ////Query for Addressline1 and Addressline2
                //            //_strSQL = "SELECT nContactID as ContactID,  sName as  [Name], sFirstName as [First Name], sMiddleName as [Middle Name], sLastName as [Last Name], sContact as Contact,sAddressLine1 as AddressLine1, sAddressLine2 as AddressLine2, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email, sHospitalAffiliation as [Hospital Affiliation], sDegree as Degree FROM Contacts_MST WHERE   bIsBlocked = 0 AND  sContactType= '" + ContactType + "'AND nClinicID = " + ClinicID + "";
                //            ////Query for street
                //            //////_strSQL = "SELECT nContactID as ContactID,  sName as  [Name], sFirstName as [First Name], sMiddleName as [Middle Name], sLastName as [Last Name], sContact as Contact, sStreet as Street, sCity as City, sState as State, sZIP as ZIP, sPhone as Phone, sFax as FAX, sMobile as Mobile, sEmail as Email, sHospitalAffiliation as [Hospital Affiliation], sDegree as Degree FROM Contacts_MST WHERE   bIsBlocked = 0 AND  sContactType= '" + ContactType + "'AND nClinicID = " + ClinicID + "";
                //            ////
                //            //oDB.Retrive_Query(_strSQL, out dt);
                //        }
                //        break;
                //}
                //---------------------------------------------------
                //above query converted into stored procedure in 6020
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@sContactType", ContactType, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oDBParameters.Add("@sSpecialityType", sSpecialityType, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("gsp_GetContacts", oDBParameters, out dt);
                //gloAuditTrail.gloAuditTrail.UpdatePILog("END GetContacts");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
            return dt;
        }
        
        public DataTable GetContactTypes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt = null;
            string _strSQL = "";

            try
            {
                //_strSQL = "SELECT distinct sContactType FROM Contacts_MST ";
                _strSQL = "SELECT distinct sContactType FROM Contacts_MST WHERE ISNULL(nClinicID,1) = " + ClinicID + " ";
                //
                oDB.Retrive_Query(_strSQL, out dt);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _strSQL = null;
            }
            return dt;
        }

        public DataTable GetInsuranceCompanies()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtInsCompanies = null;
            string _sqlQuery = "";

            try
            {
                _sqlQuery = "SELECT ISNULL(nID,0) AS nID,ISNULL(sCode,'') AS sCode, " +
                 " ISNULL(sDescription,'') AS sDescription from Contacts_InsuranceCompany_MST  " +
                 " WHERE nClinicID = " + _ClinicID + " AND nID IS NOT NULL AND nID <> 0 ";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtInsCompanies);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            { 
                if (oDB != null) { oDB.Dispose(); }
                _sqlQuery = null;
            }

            return _dtInsCompanies;
        }
        //Shubhangi
        public DataTable GetReportingCategory()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtRptCatgegory = null;
            string _sqlQuery = "";

            try
            {
                _sqlQuery = "SELECT  ISNULL(nID,0) AS nID, ISNULL(sCode,'') AS sCode,ISNULL (sDescription,'') AS SDescription " +
                    " FROM Contacts_InsuranceReportingCategory_MST WHERE nClinicID = " + _ClinicID + " ORDER BY SDescription ";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtRptCatgegory);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            { 
                if (oDB != null) { oDB.Dispose(); }
                _sqlQuery = null;
            }

            return _dtRptCatgegory;
        }

        public DataTable GetFUAction()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtFUAction = null;
            string _sqlQuery = "";
            try
            {
                _sqlQuery = " SELECT DISTINCT sFollowUpActionCode, sFollowUpActionCode+'-'+sFollowUpActionDescription as sFollowUpDesc FROM CL_FollowUpAction_Mst where nFollowUpActionType=1";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtFUAction);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            { 
                if (oDB != null) { oDB.Dispose(); }
                _sqlQuery = null;
            }

            return _dtFUAction;
        }

        public DataTable GetInsuranceCompanyDetails()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtInsCompanies = null;
            string _sqlQuery = "";

            try
            {
                //_sqlQuery = "SELECT ISNULL(nID,0) AS nID,ISNULL (sCode,'') AS sCode,ISNULL (sDescription,'') AS sDescription," +
                //            " ISNULL (nReportCategoryID,0) AS nReportCategoryID, ISNULL(nInsuranceType,0) AS nInsuranceType," + 
                //            " ISNULL(nFeeScheduleID,0) AS nFeeScheduleID, ISNULL(sAddressLine1,'') AS sAddressLine1," + 
                //            " ISNULL(sAddressLine2,'') AS sAddressLine2, ISNULL(sCity,'') AS sCity, ISNULL(sState,'') AS sState," +
                //            " ISNULL(sZip,'') AS sZip, ISNULL(sPayerID,'') AS sPayerID, ISNULL(Contacts_InsuranceCompany_MST.nClinicID,0) AS nClinicID," +
                //            " ISNULL(CPT_Mapping_MST.sCPTMappingName,'') AS sCPTMappingName " +
                //            " FROM Contacts_InsuranceCompany_MST " +
                //            " LEFT OUTER JOIN " +
                //            " CPT_Mapping_MST " +
                //            " ON Contacts_InsuranceCompany_MST.nCPTMappingID = CPT_Mapping_MST.nCPTMappingID " +
                //            " WHERE Contacts_InsuranceCompany_MST.nClinicID = " + _ClinicID + " ORDER BY sDescription ";

                _sqlQuery = "SELECT DISTINCT ISNULL(nID,0) AS nID,ISNULL (sCode,'') AS sCode,ISNULL (sDescription,'') AS sDescription," +
                            " ISNULL (nReportCategoryID,0) AS nReportCategoryID, ISNULL(nInsuranceType,0) AS nInsuranceType," +
                            " ISNULL(nFeeScheduleID,0) AS nFeeScheduleID, ISNULL(sAddressLine1,'') AS sAddressLine1," +
                            " ISNULL(sAddressLine2,'') AS sAddressLine2, ISNULL(sCity,'') AS sCity, ISNULL(sState,'') AS sState," +
                            " ISNULL(sZip,'') AS sZip, ISNULL(sPayerID,'') AS sPayerID, ISNULL(Contacts_InsuranceCompany_MST.nClinicID,0) AS nClinicID," +
                            " ISNULL(CPT_Mapping_MST.sCPTMappingName,'') AS sCPTMappingName, " +
                            " ISNULL(CONVERT(VARCHAR,BL_ExpandedClaimSettings.nServiceLines) ,'') AS nServiceLines,ISNULL(CONVERT(VARCHAR,BL_ExpandedClaimSettings.nDiagnosis),'') AS nDiagnosis " +
                            " FROM   Contacts_InsuranceCompany_MST LEFT OUTER JOIN   dbo.BL_ExpandedClaimSettings ON dbo.Contacts_InsuranceCompany_MST.nID = dbo.BL_ExpandedClaimSettings.nCompanyID LEFT OUTER JOIN " +
                            " CPT_Mapping_MST ON Contacts_InsuranceCompany_MST.nCPTMappingID = CPT_Mapping_MST.nCPTMappingID " +
                            " WHERE Contacts_InsuranceCompany_MST.nClinicID = " + _ClinicID + " ORDER BY sDescription ";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtInsCompanies);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            { 
                if (oDB != null) { oDB.Dispose(); }
                _sqlQuery = null;
            }

            return _dtInsCompanies;
        }
        //End
        public DataTable GetInsuranceTypes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt = null;
            string _strSQL = "";

            try
            {
                _strSQL = "SELECT DISTINCT sInsuranceTypeCode,sInsuranceTypeDesc FROM InsuranceType_MST WHERE nClinicID = " + ClinicID + " ORDER BY sInsuranceTypeDesc ";
                oDB.Retrive_Query(_strSQL, out dt);

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _strSQL = null;
            }
            return dt;
        }

        public DataTable GetInsurancePlans()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameter = null;
            DataTable _dtInsurancePlan = null;
            try
            {
                oDBParameter = new gloDatabaseLayer.DBParameters();
                oDBParameter.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetInsurancePlans", oDBParameter, out _dtInsurancePlan);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameter != null) { oDBParameter.Dispose(); oDBParameter = null; }
            }
            return _dtInsurancePlan;
        }

        public DataTable GetInsurancePlans(Int64 nCompanyID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameter=null;
            DataTable _dtInsurancePlan = null;
            try
            {
                oDBParameter = new gloDatabaseLayer.DBParameters();
                if (nCompanyID > 0)
                    oDBParameter.Add("@nCompanyID", nCompanyID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameter.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                
                oDB.Connect(false);
                oDB.Retrive("CO_GetInsurancePlans", oDBParameter, out _dtInsurancePlan);
                oDB.Disconnect();
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameter != null) { oDBParameter.Dispose(); oDBParameter = null; }
            }
            return _dtInsurancePlan;
        }

        public DataTable GetInactiveContacts(Int64 nCompanyID, string sContactType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameter = null;
            DataTable _dtInactiveContacts = null;
            try
            {
                oDBParameter = new gloDatabaseLayer.DBParameters();
                
                oDBParameter.Add("@nCompanyID", nCompanyID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameter.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameter.Add("@sContactType", sContactType, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Connect(false);
                oDB.Retrive("gsp_GET_INACTIVE_INSURANCEPLANS", oDBParameter, out _dtInactiveContacts);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameter != null) { oDBParameter.Dispose(); oDBParameter = null; }
            }
            return _dtInactiveContacts;
        }

        public DataTable GetCategoryInsurancePlans(Int64 nReportingCategoryID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameter=null;
            DataTable _dtInsurancePlan = null;
            try
            {
                oDBParameter = new gloDatabaseLayer.DBParameters();
                if (nReportingCategoryID > 0)
                    oDBParameter.Add("@nCategoryID", nReportingCategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameter.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("CO_GetCategoryInsurancePlans", oDBParameter, out _dtInsurancePlan);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameter != null) { oDBParameter.Dispose(); oDBParameter = null; }
            }
            return _dtInsurancePlan;
        }

        public bool IsInsurancePlanUsed(Int64 nContactID)
        {
            bool _Result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            string _Query=null;
            Object oResult=null;
           try
            {
                _Query = "SELECT COUNT(*) FROM BL_Transaction_InsPlan WHERE nContactID = " + nContactID + " ";
                oDB.Connect(false);
                oResult = oDB.ExecuteScalar_Query(_Query);

                if (oResult != null && oResult.ToString() != "")
                    if (Convert.ToInt32(oResult) > 0)
                        _Result = true;

                // TO CHECK THIS INSURANCE PLAN IS IN TRANSACTION OR NOT //
                if (_Result == false)
                {
                    oResult = null;
                    _Query = " SELECT COUNT(*) FROM BL_EOBPayment_DTL WHERE nContactInsID = " + nContactID + " ";
                    oResult = oDB.ExecuteScalar_Query(_Query);

                    if (oResult != null && oResult.ToString() != "")
                        if (Convert.ToInt32(oResult) > 0)
                            _Result = true;
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
                _Query = null;
                oResult = null;
            }
            return _Result;
        }

        //Problem# 382 Integrated from 7022 - If insurance company is used for billing then connot delete that insurance company.
        public bool IsInsuranceCompanyUsed(Int64 nInsuranceCompanyID)
        {
            bool _Result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            string _Query = null;
            Object oResult = null;
            try
            {
                _Query = " SELECT  1 FROM Credits WHERE nPayerID = " + nInsuranceCompanyID + " AND nPayerType = 2 AND nEntryType = 4 ";
                oDB.Connect(false);
                oResult = oDB.ExecuteScalar_Query(_Query);

                if (oResult != null && oResult.ToString() != "")
                    if (Convert.ToInt32(oResult) > 0)
                        _Result = true;

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
                _Query = null;
                oResult = null;
            }
            return _Result;
        }
        // end 

        public bool IsInsuranceCompanyHasPlans(Int64 nInsuranceCompanyID)
        {
            bool _Result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            string _Query = null;
            Object oResult = null;
           try
            {
                _Query = " SELECT COUNT(*) FROM Contact_InsurancePlan_Association WHERE nCompanyId = " + nInsuranceCompanyID + " ";
                oDB.Connect(false);
                oResult = oDB.ExecuteScalar_Query(_Query);
                oDB.Disconnect();

                if (oResult != null && oResult.ToString() != "")
                    if (Convert.ToInt32(oResult) > 0)
                        _Result = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
                _Query = null;
                oResult = null;
            }
            return _Result;
        }

        public bool IsReportingCategoryHasPlans(Int64 nCategoryID)
        {
            bool _Result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            string _Query = null;
            Object oResult = null;
            try
            {
                _Query = " SELECT COUNT(*) FROM Contact_InsurancePlanReportingCat_Association WHERE nReportingCategoryId = " + nCategoryID + " ";
                oDB.Connect(false);
                oResult = oDB.ExecuteScalar_Query(_Query);
                oDB.Disconnect();

                if (oResult != null && oResult.ToString() != "")
                    if (Convert.ToInt32(oResult) > 0)
                        _Result = true;

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
                _Query = null;
                oResult = null;
            }
            return _Result;
        }

        public bool IsCountryBlocked(Int64 nCountryID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;
            string _strquery = null;
            try
            {
                oDB.Connect(false);
                _strquery = "SELECT ISNULL(bIsBlocked,0) FROM Contacts_Country_MST where nID=" + nCountryID;
                if (_strquery != "")
                {
                    _result = Convert.ToBoolean(oDB.ExecuteScalar_Query(_strquery));
                }
               
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _result = false;
            }

            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _strquery = null;
            }

            return _result;

        }

        public bool IsCountryInUse(string sCountryName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;
            string _strquery = null;
            try
            {
                oDB.Connect(false);
                _strquery = "SELECT 1 FROM Patient where UPPER(sCountry) = UPPER('" + sCountryName + "')" ;
                if (_strquery != "")
                {
                    _result = Convert.ToBoolean(oDB.ExecuteScalar_Query(_strquery));
                }

                
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _result = false;
            }

            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _strquery = null;
            }

            return _result;

        }


        public String getCountryCode(string sCountryName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String  _result = "";
            string _strquery = null;
            try
            {
                oDB.Connect(false);
                _strquery = "SELECT sCode FROM  Contacts_Country_MST where UPPER(sName) =UPPER('" + sCountryName.Replace("'","''") + "')";
                _result = Convert.ToString(oDB.ExecuteScalar_Query(_strquery));


            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);                
            }

            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _strquery = null;
            }

            return _result.ToUpper();

        }

        #region "Show UI"
        public void ShowContactView(System.Windows.Forms.Form oParentWindow)
        {
            //frmViewContacts oViewContact = new frmViewContacts(_databaseconnectionstring);
            //check for running instance bugzilla bugs no :4373
            frmViewContacts oViewContact = frmViewContacts.GetInstance();
            oViewContact.Databaseconnectionstring = _databaseconnectionstring;
            oViewContact.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            oViewContact.MdiParent = oParentWindow;
            oViewContact.BringToFront();
            oViewContact.Show();

        }
        #endregion

        #region "Methods For All Contact Types"

        #region " Physician Insurance,Pharmacy And Hospital"

        public Int64 AddPhysician(Physician oPhyisician)
        {
            Int64 _result = 0;
            //@ContactID,@Company,@AddressLine1 ,@AddressLine2 ,@City , @State , @ZIP , @Phone , @Fax , @Email ,
            //@URL ,@BMAddressLine1 ,@BMddressLine2 ,@BMCity , @BMState , @BMZIP , @BMPhone , @BMFax , @BMEmail , 
            //@BMURL , @PracAddressLine1 ,@PracddressLine2 ,@PracCity , @PracState, @PracZIP , @PracPhone , @PracFax ,
            //@PracEmail , @PracURL , @Mobile,@Pager, @Notes , @FirstName , @MiddleName , @LastName , @Gender ,
            // @SpecialtyID ,  @InsuranceID , @HospitalAffiliation , @ContactType, @ExternalCode ,@Degree , 
            //@ClinicID , @IsBlocked ,@Taxonomy ,@TaxonomyDesc,@TaxID ,@UPIN ,@NPI 
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            object _intresult = 0;
            try
            {


                oDBParameters.Add("@ContactID", oPhyisician.ContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Company", oPhyisician.Company, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@AddressLine1", oPhyisician.CompanyAddress.AddrressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@AddressLine2", oPhyisician.CompanyAddress.AddrressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@City", oPhyisician.CompanyAddress.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@State", oPhyisician.CompanyAddress.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ZIP", oPhyisician.CompanyAddress.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Phone", oPhyisician.CompanyAddress.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Fax", oPhyisician.CompanyAddress.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Email", oPhyisician.CompanyAddress.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@URL", oPhyisician.CompanyAddress.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMAddressLine1", oPhyisician.BusinessMailingAddress.AddrressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMAddressLine2", oPhyisician.BusinessMailingAddress.AddrressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMCity", oPhyisician.BusinessMailingAddress.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMState", oPhyisician.BusinessMailingAddress.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMZIP", oPhyisician.BusinessMailingAddress.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMPhone", oPhyisician.BusinessMailingAddress.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMFax", oPhyisician.BusinessMailingAddress.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMEmail", oPhyisician.BusinessMailingAddress.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMURL", oPhyisician.BusinessMailingAddress.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracAddressLine1", oPhyisician.PracticeLocationAddress.AddrressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracAddressLine2", oPhyisician.PracticeLocationAddress.AddrressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracCity", oPhyisician.PracticeLocationAddress.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracState", oPhyisician.PracticeLocationAddress.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracZIP", oPhyisician.PracticeLocationAddress.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracPhone", oPhyisician.PracticeLocationAddress.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracFax", oPhyisician.PracticeLocationAddress.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracEmail", oPhyisician.PracticeLocationAddress.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracURL", oPhyisician.PracticeLocationAddress.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Mobile", oPhyisician.Mobile, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Pager", oPhyisician.Pager, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Notes", oPhyisician.Notes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@FirstName", oPhyisician.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@MiddleName", oPhyisician.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@LastName", oPhyisician.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Gender", oPhyisician.Gender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@SpecialtyID", oPhyisician.SpecialtyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@InsuranceID", oPhyisician.InsuranceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@HospitalAffiliation", oPhyisician.HospitalAffiliation, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ContactType", COntactType.Physician.ToString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ExternalCode", oPhyisician.ExternalCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Degree", oPhyisician.Degree, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ClinicID", this._ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDBParameters.Add("@Taxonomy", oPhyisician.Taxonomy, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@TaxonomyDesc", oPhyisician.TaxonomyDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@TaxID", oPhyisician.TaxID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@UPIN", oPhyisician.UPIN, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@NPI", oPhyisician.NPI, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                //oDBParameters.Add("@sInsuranceTypeCode", oContact.InsuranceTypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                //oDBParameters.Add("@sInsuranceTypeDesc", oContact.InsuranceTypeDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsBlocked", oPhyisician.IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                //_intresult = oDB.Execute("CO_INUP_Contacts_MST", oDBParameters, out  _intresult);
                int result = oDB.Execute("[CO_INUP_Physician_MST]", oDBParameters, out  _intresult);
                //

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                _intresult = null;
            }
            return _result;
        }

        public Physician SelectPhysician(Int64 contactid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Physician oPhysician = new Physician();
            DataTable dt;
            string SqlQuery = null;
            try
            {
                oDB.Connect(false);

                SqlQuery = "SELECT sCompany, sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone,sFax, sEmail, sURL,sBMAddressLine1," +
                "sBMAddressLine2, sBMCity, sBMState, sBMZip, sBMPhone, sBMFax, sBMEail, sBMURL,sPracAddressLine1, sPracAddressLine2, sPracCity," +
                "sPracState, sPracZIP, sPracPhone, sPracFax,sPracEmail, sPracURL, sMobile, sPager,sNotes, sFirstName, sMiddleName, sLastName," +
                "sGender, nSpecialtyID, nInsuranceID,sHospitalAffiliation, sContactType, sExternalCode,sDegree ," +
                "sTaxonomy, sTaxonomyDesc, sTaxID,sUPIN, sNPI FROM Contacts_MST  WHERE nContactID='" + contactid + "' AND nClinicID=" + ClinicID + " AND bIsBlocked = 0 ";



                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        oPhysician.Mobile = dt.Rows[0]["sMobile"].ToString();
                        oPhysician.Pager = dt.Rows[0]["sPager"].ToString();
                        oPhysician.Notes = dt.Rows[0]["sNotes"].ToString();
                        oPhysician.FirstName = dt.Rows[0]["sFirstName"].ToString();
                        oPhysician.MiddleName = dt.Rows[0]["sMiddleName"].ToString();
                        oPhysician.LastName = dt.Rows[0]["sLastName"].ToString();
                        oPhysician.Gender = dt.Rows[0]["sGender"].ToString();
                        //oPhysician.SpecialtyID = Convert.ToInt64(dt.Rows[0]["nSpecialtyID"]);
                        //oPhysician.InsuranceID = Convert.ToInt64(dt.Rows[0]["nInsuranceID"]);
                        oPhysician.HospitalAffiliation = dt.Rows[0]["sHospitalAffiliation"].ToString();
                        oPhysician.ContactType = dt.Rows[0]["sContactType"].ToString();
                        oPhysician.ExternalCode = dt.Rows[0]["sExternalCode"].ToString();
                        oPhysician.Degree = dt.Rows[0]["sDegree"].ToString();
                        oPhysician.Taxonomy = dt.Rows[0]["sTaxonomy"].ToString();
                        oPhysician.TaxonomyDesc = dt.Rows[0]["sTaxonomyDesc"].ToString();
                        oPhysician.TaxID = dt.Rows[0]["sTaxID"].ToString();
                        oPhysician.UPIN = dt.Rows[0]["sUPIN"].ToString();
                        oPhysician.NPI = dt.Rows[0]["sNPI"].ToString();

                        oPhysician.Company = dt.Rows[0]["sCompany"].ToString();
                        oPhysician.CompanyAddress.AddrressLine1 = dt.Rows[0]["sAddressLine1"].ToString();
                        oPhysician.CompanyAddress.AddrressLine2 = dt.Rows[0]["sAddressLine2"].ToString();
                        oPhysician.CompanyAddress.City = dt.Rows[0]["sCity"].ToString();
                        oPhysician.CompanyAddress.State = dt.Rows[0]["sState"].ToString();
                        oPhysician.CompanyAddress.ZIP = dt.Rows[0]["sZip"].ToString();
                        oPhysician.CompanyAddress.Phone = dt.Rows[0]["sPhone"].ToString();
                        oPhysician.CompanyAddress.Fax = dt.Rows[0]["sFax"].ToString();
                        oPhysician.CompanyAddress.Email = dt.Rows[0]["sEmail"].ToString();
                        oPhysician.CompanyAddress.URL = dt.Rows[0]["sURL"].ToString();

                        oPhysician.BusinessMailingAddress.AddrressLine1 = dt.Rows[0]["sBMAddressLine1"].ToString();
                        oPhysician.BusinessMailingAddress.AddrressLine2 = dt.Rows[0]["sBMAddressLine2"].ToString();
                        oPhysician.BusinessMailingAddress.City = dt.Rows[0]["sBMCity"].ToString();
                        oPhysician.BusinessMailingAddress.State = dt.Rows[0]["sBMState"].ToString();
                        oPhysician.BusinessMailingAddress.ZIP = dt.Rows[0]["sBMZip"].ToString();
                        oPhysician.BusinessMailingAddress.Phone = dt.Rows[0]["sBMPhone"].ToString();
                        oPhysician.BusinessMailingAddress.Fax = dt.Rows[0]["sBMFax"].ToString();
                        oPhysician.BusinessMailingAddress.Email = dt.Rows[0]["sBMEail"].ToString();
                        oPhysician.BusinessMailingAddress.URL = dt.Rows[0]["sBMURL"].ToString();

                        oPhysician.PracticeLocationAddress.AddrressLine1 = dt.Rows[0]["sPracAddressLine1"].ToString();
                        oPhysician.PracticeLocationAddress.AddrressLine2 = dt.Rows[0]["sPracAddressLine2"].ToString();
                        oPhysician.PracticeLocationAddress.City = dt.Rows[0]["sPracCity"].ToString();
                        oPhysician.PracticeLocationAddress.State = dt.Rows[0]["sPracState"].ToString();
                        oPhysician.PracticeLocationAddress.ZIP = dt.Rows[0]["sPracZip"].ToString();
                        oPhysician.PracticeLocationAddress.Phone = dt.Rows[0]["sPracPhone"].ToString();
                        oPhysician.PracticeLocationAddress.Fax = dt.Rows[0]["sPracFax"].ToString();
                        oPhysician.PracticeLocationAddress.Email = dt.Rows[0]["sPracEmail"].ToString();
                        oPhysician.PracticeLocationAddress.URL = dt.Rows[0]["sPracURL"].ToString();


                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                SqlQuery = null;
            }
            return oPhysician;
        }

        public Int64 AddInsurance(Insurance oInsurance,Int64 contactID)
        {
            Int64 _result = 0;
            String strSql = "";
            Int64 _nContactID = contactID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                if (_nContactID == 0)
                {
                    strSql = "Select ISNULL(MAX(nContactID),0) +1 FROM Contacts_MST ";
                    _nContactID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));
                }
                if (_nContactID !=0)
                {
                    strSql = "delete  FROM Contacts_MST where nContactID = '" + _nContactID + "'";
                    oDB.Execute_Query(strSql);
                }
                strSql = "INSERT INTO Contacts_MST (nContactID,sName,sContact, sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone,sFax, sEmail, sURL,sMobile, sPager,sInsuranceTypeCode,sInsuranceTypeDesc,sContactType,nClinicID,bIsBlocked) " +
                       " VALUES (" + _nContactID + ",'" + oInsurance.Name + "','" + oInsurance.ContactName + "','" + oInsurance.Companyaddress.AddrressLine1 + "','" + oInsurance.Companyaddress.AddrressLine2 + "','" + oInsurance.Companyaddress.City + "',"+
                       "'" + oInsurance.Companyaddress.State + "','" + oInsurance.Companyaddress.ZIP + "','" + oInsurance.Companyaddress.Phone + "','" + oInsurance.Companyaddress.Fax + "','" + oInsurance.Companyaddress.Email + "','" + oInsurance.Companyaddress.URL + "','" + oInsurance.Mobile + "','" + oInsurance.Pager + "','" + oInsurance.InsuranceTypeCode + "','" + oInsurance.InsuranceTypeDesc + "','" + COntactType.Insurance.ToString() + "' ," + _ClinicID + ",0)";

                _result = oDB.Execute_Query(strSql);

                if (_result > 0)
                    return _nContactID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                strSql = null;
            }
           // return _result;
        }

        public Insurance SelectInsurance(Int64 contactid)
        {
            DataTable dt;
            String strSql = "";
            Insurance oInsurance = new Insurance();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                strSql = "SELECT sName,sContact,sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone," +
                         "sFax, sEmail,sURL,sMobile, sPager,isnull(sInsuranceTypeCode,'') as sInsuranceTypeCode,isnull(sInsuranceTypeDesc,'') as sInsuranceTypeDesc FROM Contacts_MST left outer join Contacts_Insurance_DTL ON Contacts_MST.nContactID =Contacts_Insurance_DTL.nContactID WHERE Contacts_MST.nContactID ='" + contactid + "' AND bIsBlocked = 0 AND  sContactType= '" + COntactType.Insurance.ToString() + "'AND Contacts_MST.nClinicID=" + ClinicID + "";
                oDB.Retrive_Query(strSql, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        oInsurance.Name = dt.Rows[0]["sName"].ToString();
                        oInsurance.ContactName = dt.Rows[0]["sContact"].ToString();
                        oInsurance.Companyaddress.AddrressLine1 = dt.Rows[0]["sAddressLine1"].ToString();
                        oInsurance.Companyaddress.AddrressLine2 = dt.Rows[0]["sAddressLine2"].ToString();
                        oInsurance.Companyaddress.City = dt.Rows[0]["sCity"].ToString();
                        oInsurance.Companyaddress.State = dt.Rows[0]["sState"].ToString();
                        oInsurance.Companyaddress.ZIP = dt.Rows[0]["sZIP"].ToString();
                        oInsurance.Companyaddress.Phone = dt.Rows[0]["sPhone"].ToString();
                        oInsurance.Companyaddress.Fax = dt.Rows[0]["sFax"].ToString();
                        oInsurance.Companyaddress.Email = dt.Rows[0]["sEmail"].ToString();
                        oInsurance.Companyaddress.URL = dt.Rows[0]["sURL"].ToString();
                        oInsurance.InsuranceTypeCode = dt.Rows[0]["sInsuranceTypeCode"].ToString();
                        oInsurance.InsuranceTypeDesc = dt.Rows[0]["sInsuranceTypeDesc"].ToString();

                        oInsurance.Mobile = dt.Rows[0]["sMobile"].ToString();
                        oInsurance.Pager = dt.Rows[0]["sPager"].ToString();
                    }
                }
                dt.Dispose();


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                strSql = null;
            }
            return oInsurance;
        }

        public Int64 AddPharmacyHospital(PharmacyHospitals oPharmacyHospitals, string ContactType,Int64 ContactID )
        {
            Int64 _result = 0;
            String strSql = "";
            Int64 _nContactID = ContactID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                if (_nContactID != 0)
                {
                    strSql = "delete  FROM Contacts_MST where nContactID = '" + _nContactID + "'";
                    oDB.Execute_Query(strSql);
                }
                if (_nContactID == 0)
                {
                    strSql = "Select ISNULL(MAX(nContactID),0) +1 FROM Contacts_MST ";
                    _nContactID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));
                }

                if (ContactType == Convert.ToString(COntactType.Pharmacy))
                {
                    strSql = "INSERT INTO Contacts_MST (nContactID,sName,sContact, sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone,sFax, sEmail, sURL,sMobile, sPager,sContactType,nClinicID,bIsBlocked) " +
                           " VALUES (" + _nContactID + ",'" + oPharmacyHospitals.Name + "','" + oPharmacyHospitals.ContactName + "','" + oPharmacyHospitals.Companyaddress.AddrressLine1 + "','" + oPharmacyHospitals.Companyaddress.AddrressLine2 + "','" + oPharmacyHospitals.Companyaddress.City + "','" + oPharmacyHospitals.Companyaddress.State + "','" + oPharmacyHospitals.Companyaddress.ZIP + "','" + oPharmacyHospitals.Companyaddress.Phone + "','" + oPharmacyHospitals.Companyaddress.Fax + "','" + oPharmacyHospitals.Companyaddress.Email + "','" + oPharmacyHospitals.Companyaddress.URL + "','" + oPharmacyHospitals.Mobile + "','" + oPharmacyHospitals.Pager + "','" + COntactType.Pharmacy.ToString() + "' ," + _ClinicID + ",0)";

                }
                if (ContactType == Convert.ToString(COntactType.Hospital))
                {
                    strSql = "INSERT INTO Contacts_MST (nContactID,sName,sContact, sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone,sFax,sEmail,sURL,sMobile, sPager,sContactType,nClinicID,bIsBlocked) " +
                                          " VALUES (" + _nContactID + ",'" + oPharmacyHospitals.Name + "','" + oPharmacyHospitals.ContactName + "','" + oPharmacyHospitals.Companyaddress.AddrressLine1 + "','" + oPharmacyHospitals.Companyaddress.AddrressLine2 + "','" + oPharmacyHospitals.Companyaddress.City + "','" + oPharmacyHospitals.Companyaddress.State + "','" + oPharmacyHospitals.Companyaddress.ZIP + "','" + oPharmacyHospitals.Companyaddress.Phone + "','" + oPharmacyHospitals.Companyaddress.Fax + "','" + oPharmacyHospitals.Companyaddress.Email + "','" + oPharmacyHospitals.Companyaddress.URL + "','" + oPharmacyHospitals.Mobile + "','" + oPharmacyHospitals.Pager + "','" + COntactType.Hospital.ToString() + "' ," + _ClinicID + ",0)";
                }
                _result = oDB.Execute_Query(strSql);

                if (_result > 0)
                    return _nContactID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                strSql = null;
            }
         //   return _result;
        }

        public PharmacyHospitals SelectPharmacyHospitals(Int64 contactid, string ContactType)
        {
            DataTable dt;
            String strSql = "";
            PharmacyHospitals oPharmacyHospitals = new PharmacyHospitals();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                if (ContactType == Convert.ToString(COntactType.Pharmacy))
                {
                    strSql = "SELECT sName,sContact,sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone," +
                             "sFax, sEmail,sURL,sMobile, sPager  FROM Contacts_MST WHERE nContactID ='" + contactid + "' AND bIsBlocked = 0 AND  sContactType= '" + COntactType.Pharmacy.ToString() + "' AND nClinicID=" + ClinicID + "";
                }
                if (ContactType == Convert.ToString(COntactType.Hospital))
                {
                    strSql = "SELECT sName,sContact,sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone," +
                            "sFax, sEmail,sURL, sMobile, sPager FROM Contacts_MST WHERE nContactID ='" + contactid + "'AND bIsBlocked = 0 AND  sContactType= '" + COntactType.Hospital.ToString() + "' AND nClinicID=" + ClinicID + "";
                }

                oDB.Retrive_Query(strSql, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        oPharmacyHospitals.Name = dt.Rows[0]["sName"].ToString();
                        oPharmacyHospitals.ContactName = dt.Rows[0]["sContact"].ToString();
                        oPharmacyHospitals.Companyaddress.AddrressLine1 = dt.Rows[0]["sAddressLine1"].ToString();
                        oPharmacyHospitals.Companyaddress.AddrressLine2 = dt.Rows[0]["sAddressLine2"].ToString();
                        oPharmacyHospitals.Companyaddress.City = dt.Rows[0]["sCity"].ToString();
                        oPharmacyHospitals.Companyaddress.State = dt.Rows[0]["sState"].ToString();
                        oPharmacyHospitals.Companyaddress.ZIP = dt.Rows[0]["sZIP"].ToString();
                        oPharmacyHospitals.Companyaddress.Phone = dt.Rows[0]["sPhone"].ToString();
                        oPharmacyHospitals.Companyaddress.Fax = dt.Rows[0]["sFax"].ToString();
                        oPharmacyHospitals.Companyaddress.Email = dt.Rows[0]["sEmail"].ToString() ;
                        oPharmacyHospitals.Companyaddress.URL = dt.Rows[0]["sURL"].ToString();
                        oPharmacyHospitals.Mobile = dt.Rows[0]["sMobile"].ToString();
                        oPharmacyHospitals.Pager = dt.Rows[0]["sPager"].ToString();
                    }
                }
                dt.Dispose();


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null ;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                strSql = null;
            }
            return oPharmacyHospitals ;
        }

        #endregion

        #endregion
    }

    public class Contact
    {
        #region "Constructor & Distructor"

        public Contact()
        {

            //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
           
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
        }

        //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        //

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

        ~Contact()
        {
            Dispose(false);
        }

        #endregion

        #region Property Variables

        //nContactID, 

        private Int64 _ContactID = 0;

        //sName, 

        private string _Name = "";

        //sContact, 

        private string _ContactName = "";

        //sStreet, 

        private string _Street = "";

        //sCity, 

        private string _City = "";

        //sState, 

        private string _State = "";

        //sZIP, 

        private string _Zip = "";

        //sPhone, 

        private string _Phone = "";

        //sFax, 

        private string _Fax = "";


        //sMobile, 

        private string _Mobile = "";

        //sPager, 

        private string _Pager = "";


        //sEmail, 

        private string _Email = "";

        //sURL, 

        private string _Url = "";

        //sNotes, 

        private string _Notes = "";

        //sFirstName, 

        private string _FirstName = "";

        //sMiddleName, 

        private string _MiddleName = "";

        //sLastName, 

        private string _LastName = "";

        //sGender, 

        private string _Gender = "";

        //nSpecialtyID, 

        private Int64 _SpecialtyID = 0;

        //nInsuranceID, 

        private Int64 _InsuranceID = 0;

        //sHospitalAffiliation, 

        private string _HospitalAffiliation = "";

        //sContactType, 

        private string _ContactType;

        public string ContactType
        {
            get { return _ContactType; }
            set { _ContactType = value; }
        }

        //sExternalCode, 

        private string _ExternalCode = "";

        //sDegree

        private string _Degree = "";

        //nClinicID

        private Int64 _ClinicID = 0;

        //bitIsBlocked

        private bool _bIsBlocked = false;

        private string _Taxonomy = "";

        private string _TaxonomyDesc = "";

        //sTaxonomy, 

        private string _TaxID = "";

        //sTaxID, 

        private string _UPIN = "";

        //sUPIN, 
        private string _NPI = "";

        private string _sInsuranceTypeCode = "";

        private string _sInsuranceTypeDesc = "";

        //sNPI,
        #region " Contacts_Detail Fields"
        ////nContactsDetailID, nContactId, sInsuranceCode, sInsuranceDecs, sDescription, sValue, nType
        //private Int64 _ContactDetailID = 0;

        ////nContactsDetailID, 

        //private string _InsuranceCode = "";

        ////sInsuranceCode, 

        //private string _InsuranceDesc = "";

        ////sInsuranceDesc, 

        //private string _Description = "";

        ////sDescription, 

        //private string _Value = "";

        ////sValue

        //private int _Type = 0;

        ////nType

        //nContactsDetailID, nContactId, sInsuranceCode, sInsuranceDecs, sDescription, sValue, nType 
        #endregion



        //nContactID, 
        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }


        //sName, 
        public string Name
        { get { return _Name; } set { _Name = value; } }

        //sContact, 

        public string ContactName
        { get { return _ContactName; } set { _ContactName = value; } }

        //sStreet, 

        public string Street
        { get { return _Street; } set { _Street = value; } }

        //sCity, 
        public string City
        { get { return _City; } set { _City = value; } }

        //sState, 
        public string State
        { get { return _State; } set { _State = value; } }

        //sZIP, 
        public string ZIP
        { get { return _Zip; } set { _Zip = value; } }

        //sPhone, 
        public string Phone
        { get { return _Phone; } set { _Phone = value; } }

        //sFax, 
        public string Fax
        { get { return _Fax; } set { _Fax = value; } }


        //sMobile, 
        public string Mobile
        { get { return _Mobile; } set { _Mobile = value; } }

        //sPager, 
        public string Pager
        { get { return _Pager; } set { _Pager = value; } }


        //sEmail, 
        public string Email
        { get { return _Email; } set { _Email = value; } }

        //sURL, 
        public string URL
        { get { return _Url; } set { _Url = value; } }

        //sNotes, 
        public string Notes
        { get { return _Notes; } set { _Notes = value; } }

        //sFirstName, 
        public string FirstName
        { get { return _FirstName; } set { _FirstName = value; } }

        //sMiddleName, 
        public string MiddleName
        { get { return _MiddleName; } set { _MiddleName = value; } }

        //sLastName, 
        public string LastName
        { get { return _LastName; } set { _LastName = value; } }

        //sGender, 
        public string Gender
        { get { return _Gender; } set { _Gender = value; } }

        //nSpecialtyID, 
        public Int64 SpecialtyID
        { get { return _SpecialtyID; } set { _SpecialtyID = value; } }

        //nInsuranceID, 
        public Int64 InsuranceID
        { get { return _InsuranceID; } set { _InsuranceID = value; } }

        //sHospitalAffiliation, 
        public string HospitalAffiliation
        { get { return _HospitalAffiliation; } set { _HospitalAffiliation = value; } }


        //sExternalCode, 
        public string ExternalCode
        { get { return _ExternalCode; } set { _ExternalCode = value; } }

        //sDegree
        public string Degree
        { get { return _Degree; } set { _Degree = value; } }

        //nClinicID
        public Int64 ClinicID
        { get { return _ClinicID; } set { _ClinicID = value; } }

        //bitIsBlocked
        public bool IsBlocked
        { get { return _bIsBlocked; } set { _bIsBlocked = value; } }



        //sTaxonomy, 
        public string Taxonomy
        { get { return _Taxonomy; } set { _Taxonomy = value; } }

        //sTaxonomyDesc
        public string TaxonomyDesc
        { get { return _TaxonomyDesc; } set { _TaxonomyDesc = value; } }

        //sTaxID, 
        public string TaxID
        { get { return _TaxID; } set { _TaxID = value; } }


        //sUPIN, 
        public string UPIN
        { get { return _UPIN; } set { _UPIN = value; } }

        //sNPI, 
        public string NPI
        { get { return _NPI; } set { _NPI = value; } }


        public string InsuranceTypeCode
        { get { return _sInsuranceTypeCode; } set { _sInsuranceTypeCode = value; } }

        public string InsuranceTypeDesc
        { get { return _sInsuranceTypeDesc; } set { _sInsuranceTypeDesc = value; } }


        #region " Contacts_Detail Fields Properties"

        //  public Int64 ContactDetailID 

        //   { get { return _ContactDetailID; } set { _ContactDetailID = value; } }


        //  public string InsuranceCode 
        //  { get { return _InsuranceCode; } set { _InsuranceCode = value; } }


        //  public string InsuranceDesc
        // { get { return _InsuranceDesc; } set { _InsuranceDesc = value; } }


        //  public string Description 
        //{ get { return _Description; } set { _Description = value; } }


        //  public string Value 
        // { get { return _Value; } set { _Value = value; } }


        //  public int Type 
        // { get { return _Type; } set { _Type = value; } }



        #endregion


        #endregion
    }

    public class ContactDetail
    {
        #region "Constructor & Distructor"

        public ContactDetail()
        {
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
            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //Sandip Darade  20090428
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


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _MessageBoxCaption = String.Empty;


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

        ~ContactDetail()
        {
            Dispose(false);
        }

        #endregion


        #region " Contacts_Detail Fields"
        //nContactsDetailID, nContactId, sInsuranceCode, sInsuranceDecs, sDescription, sValue, nType
        private Int64 _ClinicID = 0;

        private Int64 _ContactID = 0;

        private Int64 _ContactDetailID = 0;

        //nContactsDetailID, 

        private string _InsuranceCode = "";

        //sInsuranceCode, 

        private string _InsuranceDesc = "";

        //sInsuranceDesc, 

        private string _Description = "";

        //sDescription, 

        private string _Value = "";

        //sValue

        private int _Type = 0;

        //nType

        //nContactsDetailID, nContactId, sInsuranceCode, sInsuranceDecs, sDescription, sValue, nType 
        #endregion



        #region " Contacts_Detail Fields Properties"
        //nContactID, 
        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }

        public Int64 ClinicID
        { get { return _ClinicID; } set { _ClinicID = value; } }

        public Int64 ContactDetailID

        { get { return _ContactDetailID; } set { _ContactDetailID = value; } }


        public string InsuranceCode
        { get { return _InsuranceCode; } set { _InsuranceCode = value; } }


        public string InsuranceDesc
        { get { return _InsuranceDesc; } set { _InsuranceDesc = value; } }


        public string Description
        { get { return _Description; } set { _Description = value; } }


        public string Value
        { get { return _Value; } set { _Value = value; } }


        public int Type
        { get { return _Type; } set { _Type = value; } }



        #endregion


    }

    public class ContactDetails : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

       // private string _databaseconnectionstring = "";

        public ContactDetails()
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

        ~ContactDetails()
        {
            Dispose(false);
        }

        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(ContactDetail item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(ContactDetail item)
        {
            bool result = false;
            ContactDetail obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                obj = new ContactDetail();
                obj = (ContactDetail)_innerlist[i];
                if (obj.ContactDetailID == item.ContactDetailID)
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

        public ContactDetail this[int index]
        {
            get
            {
                return (ContactDetail)_innerlist[index];
            }
        }

        public bool Contains(ContactDetail item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(ContactDetail item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(ContactDetail[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }

    public class ContactAddress
    {
        #region "Constructor & Distructor"

        public ContactAddress()
        {

        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

        ~ContactAddress()
        {
            Dispose(false);
        }

        #endregion

        #region Property Variables

        #region " ContactAddress Fields"

        private Int64 _ContactID = 0;

        private string _AddressLine1 = "";

        private string _AddressLine2 = "";

        private string _City = "";

        private string _State = "";

        private string _Zip = "";

        private string _Phone = "";

        private string _Fax = "";

        private string _Email = "";

        private string _Url = "";

       
        #endregion


        #region " ContactsAddress Fields Properties"

        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }

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

        public string Phone
        { get { return _Phone; } set { _Phone = value; } }

        public string Fax
        { get { return _Fax; } set { _Fax = value; } }

        public string Email
        { get { return _Email; } set { _Email = value; } }

        public string URL
        { get { return _Url; } set { _Url = value; } }

     

        #endregion

        #endregion

    }

    public class Physician
    {
        #region "Constructor & Distructor"

        public Physician()
        {
            _BusinessMailingAddress = new ContactAddress();
            _CompanyAddress = new ContactAddress();
            _PracticeLocationAddress = new ContactAddress();
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

        ~Physician()
        {
            Dispose(false);
        }

        #endregion

        #region Property Variables

        private Int64 _ContactID = 0;

        private ContactAddress _BusinessMailingAddress = null;

        private ContactAddress _PracticeLocationAddress = null;

        private ContactAddress _CompanyAddress = null;

        private string _Company = "";

        private string _Notes = "";

        private string _FirstName = "";


        private string _MiddleName = "";


        private string _LastName = "";


        private string _Gender = "";


        private Int64 _SpecialtyID = 0;


        private Int64 _InsuranceID = 0;


        private string _HospitalAffiliation = "";


        private string _ContactType;


        private string _ExternalCode = "";


        private string _Degree = "";


        private bool _bIsBlocked = false;


        private string _Taxonomy = "";


        private string _TaxonomyDesc = "";


        private string _TaxID = "";


        private string _UPIN = "";


        private string _NPI = "";

        private string _Mobile = "";

        private string _Pager = "";
       

        private string _InsuranceTypeCode = "";


        private string _InsuranceTypeDesc = "";
        private string _County = "";

        #endregion

        #region Properties


        public ContactAddress BusinessMailingAddress
        { get { return _BusinessMailingAddress; } set { _BusinessMailingAddress = value; } }

        public ContactAddress PracticeLocationAddress
        { get { return _PracticeLocationAddress; } set { _PracticeLocationAddress = value; } }

        public ContactAddress CompanyAddress
        { get { return _CompanyAddress; } set { _CompanyAddress = value; } }


        public string Company
        { get { return _Company; } set { _Company = value; } }


        public string ContactType
        {
            get { return _ContactType; }
            set { _ContactType = value; }
        }

        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }


        public string Notes
        { get { return _Notes; } set { _Notes = value; } }


        public string FirstName
        { get { return _FirstName; } set { _FirstName = value; } }


        public string MiddleName
        { get { return _MiddleName; } set { _MiddleName = value; } }


        public string LastName
        { get { return _LastName; } set { _LastName = value; } }


        public string Gender
        { get { return _Gender; } set { _Gender = value; } }


        public Int64 SpecialtyID
        { get { return _SpecialtyID; } set { _SpecialtyID = value; } }


        public Int64 InsuranceID
        { get { return _InsuranceID; } set { _InsuranceID = value; } }


        public string HospitalAffiliation
        { get { return _HospitalAffiliation; } set { _HospitalAffiliation = value; } }


        public string ExternalCode
        { get { return _ExternalCode; } set { _ExternalCode = value; } }



        public string Degree
        { get { return _Degree; } set { _Degree = value; } }


        public bool IsBlocked
        { get { return _bIsBlocked; } set { _bIsBlocked = value; } }



        public string Taxonomy
        { get { return _Taxonomy; } set { _Taxonomy = value; } }


        public string TaxonomyDesc
        { get { return _TaxonomyDesc; } set { _TaxonomyDesc = value; } }


        public string TaxID
        { get { return _TaxID; } set { _TaxID = value; } }


        public string UPIN
        { get { return _UPIN; } set { _UPIN = value; } }


        public string NPI
        { get { return _NPI; } set { _NPI = value; } }

        public string Pager
        { get { return _Pager; } set { _Pager = value; } }

        public string Mobile
        { get { return _Mobile; } set { _Mobile = value; } }

        

        public string InsuranceTypeCode
        { get { return _InsuranceTypeCode; } set { _InsuranceTypeCode = value; } }


        public string InsuranceTypeDesc
        { get { return _InsuranceTypeDesc; } set { _InsuranceTypeDesc = value; } }

        public string County
        { get { return _County; } set { _County = value; } }

        #endregion


    }

    public class Insurance
    {
        #region "Constructor & Distructor"

        public Insurance()
        {
            _CompanyAddress = new ContactAddress();
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

        #region "Variables"

        private Int64 _ContactID = 0;

        private string _Name = "";

        private string _ContactName = "";

        private string _sInsuranceTypeCode = "";

        private string _sInsuranceTypeDesc = "";

        private ContactAddress _CompanyAddress = null;

        private string _Mobile = "";

        private string _Pager = "";

        #endregion

        #region    "  Properties  "

        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }

        public string Name
        { get { return _Name; } set { _Name = value; } }


        public string ContactName
        { get { return _ContactName; } set { _ContactName = value; } }


        public string InsuranceTypeCode
        { get { return _sInsuranceTypeCode; } set { _sInsuranceTypeCode = value; } }


        public string InsuranceTypeDesc
        { get { return _sInsuranceTypeDesc; } set { _sInsuranceTypeDesc = value; } }


        public ContactAddress Companyaddress

        { get { return _CompanyAddress; } set { _CompanyAddress = value; } }


        public string Pager
        { get { return _Pager; } set { _Pager = value; } }


        public string Mobile
        { get { return _Mobile; } set { _Mobile = value; } }


        #endregion

    }

    public class PharmacyHospitals
    {
        #region "Constructor & Distructor"

        public PharmacyHospitals()
        {
            _CompanyAddress = new ContactAddress();
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

        ~PharmacyHospitals()
        {
            Dispose(false);
        }

        #endregion

        #region "Variables"

        private Int64 _ContactID = 0;

        private string _Name = "";

        private string _ContactName = "";

        private ContactAddress _CompanyAddress = null;
        private string _Mobile = "";

        private string _Pager = "";

        #endregion

        #region "Properties"

        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }

        public string Name
        { get { return _Name; } set { _Name = value; } }


        public string ContactName
        { get { return _ContactName; } set { _ContactName = value; } }

        public ContactAddress Companyaddress

        { get { return _CompanyAddress; } set { _CompanyAddress = value; } }

        public string Pager
        { get { return _Pager; } set { _Pager = value; } }

        public string Mobile
        { get { return _Mobile; } set { _Mobile = value; } }


        #endregion


    }

    public class Hospitals
    {

        #region "Constructor & Distructor"

        public Hospitals()
        {
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

        ~Hospitals()
        {
            Dispose(false);
        }

        #endregion

        #region "Variables"

        private Int64 _ContactID = 0;

        private string _Name = "";

        private string _ContactName = "";

        private ContactAddress _CompanyAddress = null;

        #endregion

        #region    "  Properties  "

        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }

        public string Name
        { get { return _Name; } set { _Name = value; } }


        public string ContactName
        { get { return _ContactName; } set { _ContactName = value; } }


        public ContactAddress Companyaddress

        { get { return _CompanyAddress; } set { _CompanyAddress = value; } }

        #endregion

    }


    //public class ZipCode
    //{
    //        #region variables
    //            Int64 _ZipID = 0;
    //            string _code=String.Empty;
    //            string _state=String.Empty;
    //            string _country=String.Empty;
    //            string _AreaCode=String.Empty;
    //        #endregion

    //     #region "Constructor & Distructor"

    //    public ZipCode()
    //    {

    //    }

    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

    //    private bool disposed = false;

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this.disposed)
    //        {
    //            if (disposing)
    //            {

    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~ZipCode()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion

    //    #region Properties

    //    public string code
    //     { get { return _code; } set { _code = value; } }

    //      public string   city
    //           { get { return _city; } set { _city = value; } }

    //     public string   state
    //           { get { return _state; } set { _state = value; } }

        
    //     public string   country
    //           { get { return _country; } set { _country = value; } }

    //    public string AreaCode
    //     { get { return _AreaCode; } set { _AreaCode = value; } }

    //    public Int64 ZipID
    //    {
    //        get { return _ZipID; } set { _ZipID = value; } }
    


          
             
    //    #endregion
    //}

}

