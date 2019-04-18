using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace gloAppointmentBook
{


        public enum Modules
    {
        Appointment = 1,
        Schedule = 2,
        Billing = 3,
        AppointmentBook = 4,
        BillingBook = 5
    }

        public enum Activities
    {
        Add = 1,
        Modify = 2,
        Delete = 3,
        View = 4,
        SendExchange = 5,
        RetriveExchange = 6
    }

        public class Appointment
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public Appointment()
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

        ~Appointment()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variable"
        private bool _Add;
        private bool _Modify;
        private bool _Delete;
        private bool _View;
        private bool _SendToExchange;
        private bool _RetriveFromExchange;
        #endregion


        public bool Add
        {
            get { return _Add; }

            set
            {
                _Add = value;

                // Add / Remove the Acivity form the list of Module 
                if (_Add == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Add) == false)
                        _innerlist.Add(Activities.Add);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Add) == true)
                        _innerlist.Remove(Activities.Add);
                }
                //
            }
        }
        public bool Modify
        {
            get { return _Modify; }
            set
            {
                _Modify = value;

                // Add / Remove the Acivity form the list of Module 
                if (_Modify == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Modify) == false)
                        _innerlist.Add(Activities.Modify);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Modify) == true)
                        _innerlist.Remove(Activities.Modify);
                }
                //
            }
        }
        public bool Delete
        {
            get { return _Delete; }
            set
            {
                _Delete = value;

                // Add / Remove the Acivity form the list of Module 
                if (_Delete == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Delete) == false)
                        _innerlist.Add(Activities.Delete);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Delete) == true)
                        _innerlist.Remove(Activities.Delete);
                }
            }
        }
        public bool View
        {
            get { return _View; }
            set
            {
                _View = value;
                // Add / Remove the Acivity form the list of Module 
                if (_View == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.View) == false)
                        _innerlist.Add(Activities.View);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.View) == true)
                        _innerlist.Remove(Activities.View);
                }
            }
        }
        public bool SendToExchange
        {
            get { return _SendToExchange; }
            set
            {
                _SendToExchange = value;

                // Add / Remove the Acivity form the list of Module 
                if (_SendToExchange == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.SendExchange) == false)
                        _innerlist.Add(Activities.SendExchange);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.SendExchange) == true)
                        _innerlist.Remove(Activities.SendExchange);
                }
            }
        }
        public bool RetriveFromExchange
        {
            get { return _RetriveFromExchange; }
            set
            {
                _RetriveFromExchange = value;

                // Add / Remove the Acivity form the list of Module 
                if (_RetriveFromExchange == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.RetriveExchange) == false)
                        _innerlist.Add(Activities.RetriveExchange);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.RetriveExchange) == true)
                        _innerlist.Remove(Activities.RetriveExchange);
                }
            }
        }


        public void Clear()
        {
            _innerlist.Clear();
        }

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public Activities this[int index]
        {
            get
            {
                return (Activities)_innerlist[index];
            }
        }

        public int IndexOf(Activities item)
        {
            return _innerlist.IndexOf(item);
        }

    }

        public class Schedule
    {
        #region "Constructor & Distructor"


        public Schedule()
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

        ~Schedule()
        {
            Dispose(false);
        }

        #endregion

        private ArrayList _innerlist;
        private bool _Add;
        private bool _Modify;
        private bool _Delete;
        private bool _View;

        public bool Add
        {
            get { return _Add; }
            set
            {
                _Add = value;

                // Add / Remove the Acivity form the list of Module 
                if (_Add == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Add) == false)
                        _innerlist.Add(Activities.Add);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Add) == true)
                        _innerlist.Remove(Activities.Add);
                }
                //
            }
        }
        public bool Modify
        {
            get { return _Modify; }
            set
            {
                _Modify = value;
                // Add / Remove the Acivity form the list of Module 
                if (_Modify == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Modify) == false)
                        _innerlist.Add(Activities.Modify);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Modify) == true)
                        _innerlist.Remove(Activities.Modify);
                }
            }
        }
        public bool Delete
        {
            get { return _Delete; }
            set
            {
                _Delete = value;  // Add / Remove the Acivity form the list of Module 

                if (_Delete == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Delete) == false)
                        _innerlist.Add(Activities.Delete);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Delete) == true)
                        _innerlist.Remove(Activities.Delete);
                }
            }
        }
        public bool View
        {
            get { return _View; }
            set
            {
                _View = value; // Add / Remove the Acivity form the list of Module 
                if (_View == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.View) == false)
                        _innerlist.Add(Activities.View);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.View) == true)
                        _innerlist.Remove(Activities.View);
                }
            }
        }

        public void Clear()
        {
            _innerlist.Clear();
        }
        public int Count
        {
            get { return _innerlist.Count; }
        }
        public Activities this[int index]
        {
            get
            {
                return (Activities)_innerlist[index];
            }
        }
        public int IndexOf(Activities item)
        {
            return _innerlist.IndexOf(item);
        }
    }

        public class Billing
    {
        #region "Constructor & Distructor"


        public Billing()
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

        ~Billing()
        {
            Dispose(false);
        }

        #endregion

        private ArrayList _innerlist;
        private bool _Add;
        private bool _Modify;
        private bool _Delete;
        private bool _View;

        public bool Add
        {
            get { return _Add; }
            set
            {
                _Add = value;

                // Add / Remove the Acivity form the list of Module 
                if (_Add == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Add) == false)
                        _innerlist.Add(Activities.Add);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Add) == true)
                        _innerlist.Remove(Activities.Add);
                }
                //
            }
        }
        public bool Modify
        {
            get { return _Modify; }
            set
            {
                _Modify = value;
                // Add / Remove the Acivity form the list of Module 
                if (_Modify == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Modify) == false)
                        _innerlist.Add(Activities.Modify);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Modify) == true)
                        _innerlist.Remove(Activities.Modify);
                }
            }
        }
        public bool Delete
        {
            get { return _Delete; }
            set
            {
                _Delete = value;  // Add / Remove the Acivity form the list of Module 

                if (_Delete == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Delete) == false)
                        _innerlist.Add(Activities.Delete);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Delete) == true)
                        _innerlist.Remove(Activities.Delete);
                }
            }
        }
        public bool View
        {
            get { return _View; }
            set
            {
                _View = value; // Add / Remove the Acivity form the list of Module 
                if (_View == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.View) == false)
                        _innerlist.Add(Activities.View);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.View) == true)
                        _innerlist.Remove(Activities.View);
                }
            }
        }

        public void Clear()
        {
            _innerlist.Clear();
        }
        public int Count
        {
            get { return _innerlist.Count; }
        }
        public Activities this[int index]
        {
            get
            {
                return (Activities)_innerlist[index];
            }
        }
        public int IndexOf(Activities item)
        {
            return _innerlist.IndexOf(item);
        }
    }

        public class AppointmentBook
    {
        #region "Constructor & Distructor"


        public AppointmentBook()
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

        ~AppointmentBook()
        {
            Dispose(false);
        }

        #endregion

        private ArrayList _innerlist;
        private bool _Add;
        private bool _Modify;
        private bool _Delete;
        private bool _View;

        public bool Add
        {
            get { return _Add; }
            set
            {
                _Add = value;

                // Add / Remove the Acivity form the list of Module 
                if (_Add == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Add) == false)
                        _innerlist.Add(Activities.Add);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Add) == true)
                        _innerlist.Remove(Activities.Add);
                }
                //
            }
        }
        public bool Modify
        {
            get { return _Modify; }
            set
            {
                _Modify = value;
                // Add / Remove the Acivity form the list of Module 
                if (_Modify == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Modify) == false)
                        _innerlist.Add(Activities.Modify);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Modify) == true)
                        _innerlist.Remove(Activities.Modify);
                }
            }
        }
        public bool Delete
        {
            get { return _Delete; }
            set
            {
                _Delete = value;  // Add / Remove the Acivity form the list of Module 

                if (_Delete == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Delete) == false)
                        _innerlist.Add(Activities.Delete);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Delete) == true)
                        _innerlist.Remove(Activities.Delete);
                }
            }
        }
        public bool View
        {
            get { return _View; }
            set
            {
                _View = value; // Add / Remove the Acivity form the list of Module 
                if (_View == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.View) == false)
                        _innerlist.Add(Activities.View);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.View) == true)
                        _innerlist.Remove(Activities.View);
                }
            }
        }

        public void Clear()
        {
            _innerlist.Clear();
        }
        public int Count
        {
            get { return _innerlist.Count; }
        }
        public Activities this[int index]
        {
            get
            {
                return (Activities)_innerlist[index];
            }
        }
        public int IndexOf(Activities item)
        {
            return _innerlist.IndexOf(item);
        }

    }

        public class BillingBook
    {
        #region "Constructor & Distructor"

        public BillingBook()
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

        ~BillingBook()
        {
            Dispose(false);
        }

        #endregion

        private ArrayList _innerlist;
        private bool _Add;
        private bool _Modify;
        private bool _Delete;
        private bool _View;

        public bool Add
        {
            get { return _Add; }
            set
            {
                _Add = value;

                // Add / Remove the Acivity form the list of Module 
                if (_Add == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Add) == false)
                        _innerlist.Add(Activities.Add);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Add) == true)
                        _innerlist.Remove(Activities.Add);
                }
                //
            }
        }
        public bool Modify
        {
            get { return _Modify; }
            set
            {
                _Modify = value;
                // Add / Remove the Acivity form the list of Module 
                if (_Modify == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Modify) == false)
                        _innerlist.Add(Activities.Modify);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Modify) == true)
                        _innerlist.Remove(Activities.Modify);
                }
            }
        }
        public bool Delete
        {
            get { return _Delete; }
            set
            {
                _Delete = value;  // Add / Remove the Acivity form the list of Module 

                if (_Delete == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.Delete) == false)
                        _innerlist.Add(Activities.Delete);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.Delete) == true)
                        _innerlist.Remove(Activities.Delete);
                }
            }
        }
        public bool View
        {
            get { return _View; }
            set
            {
                _View = value; // Add / Remove the Acivity form the list of Module 
                if (_View == true)
                {
                    // If Acivityis Set then Check if it is already in the List of Module
                    if (_innerlist.Contains(Activities.View) == false)
                        _innerlist.Add(Activities.View);
                }
                else
                // if Acivity is not set the Remove it from the List of Module
                {
                    if (_innerlist.Contains(Activities.View) == true)
                        _innerlist.Remove(Activities.View);
                }
            }
        }

        public void Clear()
        {
            _innerlist.Clear();
        }
        public int Count
        {
            get { return _innerlist.Count; }
        }
        public Activities this[int index]
        {
            get
            {
                return (Activities)_innerlist[index];
            }
        }
        public int IndexOf(Activities item)
        {
            return _innerlist.IndexOf(item);
        }

    }

        public class gloUser : IDisposable
        {
            private string _databaseConnectionString = "";

            #region "Constructor & Destructor"

            public gloUser(string databaseConnectionString)
            {
                _databaseConnectionString = databaseConnectionString;
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

            ~gloUser()
            {
                Dispose(false);
            }

            #endregion

            public bool CheckUserExists(string loginName,Int64 Userid)
            {
                int count;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "SELECT Count(*) FROM User_MST  WITH(NOLOCK) WHERE sLoginName = '" + loginName + "' AND nUserID <> " + Userid + "";
                    count = (Int32)oDB.ExecuteScalar_Query(strQuery);
                    strQuery = null;
                    if (count > 0)
                    {
                        //Login name exists
                        return true;
                    }
                    else
                    {
                        //Login name does not exists
                        return false;
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;   
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
                return true;
            }

            public Int64 Add(User oUser)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                object Result = new object();
                try
                {
                    oDB.Connect(false);

                    oDBParameters.Add("@UserID", oUser.UserID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@LoginName", oUser.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Password", oUser.Password, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@NickName", oUser.NickName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@FirstName", oUser.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@MiddleName", oUser.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@LastName", oUser.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@SSNNo", oUser.SSNno, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@DOB", oUser.DateOfBirth, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                    oDBParameters.Add("@Gender", oUser.Gender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@MaritalStatus", oUser.MaritalStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Address", oUser.Address1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Address2", oUser.Address2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Street", oUser.Street, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@City", oUser.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@State", oUser.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@ZIP", oUser.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@PhoneNo", oUser.PhoneNo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@MobileNo", oUser.MobileNo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@FAX", oUser.FAX, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Email", oUser.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Administrator", oUser.IsAdministrator, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ProviderID", oUser.ProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@IsAuditTrail", oUser.IsAuditTrail, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@nAccessDenied", oUser.IsAccessDenied, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@bCoSign", oUser.IsCoSign, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@IsPasswordSettings", oUser.IsPasswordSettings, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                    oDBParameters.Add("@IsExchangeUser", oUser.IsExchangeUser, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@ExchangeLogin", oUser.ExchangeLogin, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@ExchangePassword", oUser.ExchangePassword, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                    //for storing image into database
                    //convert image into memory stream
                    //then get byte array of the image & send the array to databse
                    if (oUser.Signature != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        oUser.Signature.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        try
                        {
                            ms.Flush();
                        }
                        catch
                        {
                        }
                        Byte[] arrImage = ms.ToArray();
                        
                        oDBParameters.Add("@Signature", arrImage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                        try
                        {
                            ms.Close();
                            ms.Dispose();
                            ms = null;
                        }
                        catch
                        {
                        }
                        arrImage = null;
                    }
                    else
                    {
                        oDBParameters.Add("@Signature", null, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                    }


                    oDB.Execute("gsp_InUpUser", oDBParameters, out Result);
                    return (Int64)Result;

                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;   
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
                }
            }

            public DataTable GetAllProviders()
            {
                DataTable dtProviders = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "SELECT nProviderID, (sFirstName+' '+sMiddleName+' '+sLastName) as ProviderName FROM AB_Resource_Provider  WITH(NOLOCK) ORDER BY ProviderName";
                    oDB.Retrive_Query(strQuery, out dtProviders);
                    strQuery = null;
                    return dtProviders;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;   
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
                    oDB.Dispose();
                }
            }

            public DataTable GetUser(Int64 UserId)
            {
                DataTable dtUser = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                 //   String strQuery = "SELECT * FROM User_MST WHERE nUserID = " + UserId + ""; //Removed Select *
                      String strQuery = "SELECT  nUserID, sLoginName, sPassword, sFirstName, sMiddleName, sLastName, sSSNNo, dtDOB, sGender, sMaritalStatus, sAddress, sStreet, sCity, sState, " +
                                       " sZIP, sPhoneNo, sMobileNo, sFAX, sEmail, nBlockStatus, nAdministrator, nProviderID, sNickName, imgSignature, IsPasswordReset, IsAuditTrail,  " +
                                        " nAccessDenied, bCoSign, bIsSecurityUser, nClinicID, IsExchangeUser, sExchangeLogin, sExchangePassword, sAddress2, IsPasswordSettings,  " +
                                       " sCountry, sCounty, IsPatientChartAccess, sAccessPassword, dtValidupto, sSpeciality  " +
                                       " FROM User_MST  WITH(NOLOCK) WHERE nUserID = " + UserId + "";
                    oDB.Retrive_Query(strQuery, out dtUser);
                    strQuery = null;
                    return dtUser;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;   
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
                    oDB.Dispose();
                }
            }

            public DataTable getAllUsers()
            {
                DataTable dtUsers = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST  WITH(NOLOCK) ";
                    oDB.Retrive_Query(strQuery, out dtUsers);
                    strQuery = null;
                    return dtUsers;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;   
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
                    oDB.Dispose();
                }
            }

            public DataTable getActiveUsers()
            {
                DataTable dtUsers = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST  WITH(NOLOCK) WHERE nBlockStatus = 0 OR nBlockStatus is null";
                    oDB.Retrive_Query(strQuery, out dtUsers);
                    strQuery = null;
                    return dtUsers;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;   
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
                    oDB.Dispose();
                }
            }

            public DataTable getBlockedUsers()
            {
                DataTable dtUsers = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST  WITH(NOLOCK) WHERE nBlockStatus = 1";
                    oDB.Retrive_Query(strQuery, out dtUsers);
                    strQuery = null;
                    return dtUsers;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;   
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
                    oDB.Dispose();
                }
            }

            public void BlockUser(Int64 UserId, bool Block)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "UPDATE User_MST SET nBlockStatus = '" + Block + "' WHERE nUserID = " + UserId + "";
                    oDB.Execute_Query(strQuery);
                    strQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;   
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
            }

            //public void ShowViewUsers(System.Windows.Forms.Form oParentWindow2)
            //{
            //    //frmViewUsers oViewUser = new frmViewUsers(_databaseConnectionString);
            //    frmViewUsers oViewUser = frmViewUsers.GetInstance(_databaseConnectionString);
            //    oViewUser.MdiParent = oParentWindow2;
            //    oViewUser.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //    oViewUser.BringToFront();
            //    oViewUser.Show();
            //}
        }

        public class User : IDisposable
        {
            #region Declaration

            private Int64 _nUserID;
            private string _sUserName;
            private string _sPassword;
            private string _sNickName;
            private string _sFirstName;
            private string _sMiddleName;
            private string _sLastName;
            private string _sSSNno;
            private DateTime _dtDOB;
            private string _sGender;
            private string _sMaritalStatus;
            private string _sAddress1;
            private string _sAddress2;
            private string _sStreet;
            private string _sCity;
            private string _sState;
            private string _sZIP;
            private string _sPhoneNo;
            private string _sMobileNo;
            private string _sFAX;
            private string _sEmail;
            private byte _nBlockStatus;
            private bool _blnAdministrator;
            private Int64 _nProviderID = 0;
            private string _sProviderName;
            private bool _isPasswordReset = false;
            private bool _isAuditTrail;
            private bool _blnAccessDenied = false;
            private bool _blnCoSign;
            private bool _isPasswordSettings = false;
            private bool _isExchangeUser = false;
            private string _exchangeLogin = "";
            private string _exchangePassword = "";
             private Image _signature;


            #endregion

            #region Properties

            public Int64 UserID
            {
                get { return _nUserID; }
                set { _nUserID = value; }
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

            public string NickName
            {
                get { return _sNickName; }
                set { _sNickName = value; }
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

            public string SSNno
            {
                get { return _sSSNno; }
                set { _sSSNno = value; }
            }

            public DateTime DateOfBirth
            {
                get { return _dtDOB; }
                set { _dtDOB = value; }
            }

            public string Gender
            {
                get { return _sGender; }
                set { _sGender = value; }
            }

            public string MaritalStatus
            {
                get { return _sMaritalStatus; }
                set { _sMaritalStatus = value; }
            }

            public string Address1
            {
                get { return _sAddress1; }
                set { _sAddress1 = value; }
            }

            public string Address2
            {
                get { return _sAddress2; }
                set { _sAddress2 = value; }
            }

            public string Street
            {
                get { return _sStreet; }
                set { _sStreet = value; }
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

            public string PhoneNo
            {
                get { return _sPhoneNo; }
                set { _sPhoneNo = value; }
            }

            public string MobileNo
            {
                get { return _sMobileNo; }
                set { _sMobileNo = value; }
            }

            public string FAX
            {
                get { return _sFAX; }
                set { _sFAX = value; }
            }

            public string Email
            {
                get { return _sEmail; }
                set { _sEmail = value; }
            }

            public byte BlockStatus
            {
                get { return _nBlockStatus; }
                set { _nBlockStatus = value; }
            }

            public bool IsAdministrator
            {
                get { return _blnAdministrator; }
                set { _blnAdministrator = value; }
            }

            public Int64 ProviderID
            {
                get { return _nProviderID; }
                set { _nProviderID = value; }
            }

            public string ProviderName
            {
                get { return _sProviderName; }
                set { _sProviderName = value; }
            }

            public bool IsPasswordReset
            {
                get { return _isPasswordReset; }
                set { _isPasswordReset = value; }
            }

            public bool IsAuditTrail
            {
                get { return _isAuditTrail; }
                set { _isAuditTrail = value; }
            }

            public bool IsAccessDenied
            {
                get { return _blnAccessDenied; }
                set { _blnAccessDenied = value; }
            }

            public bool IsCoSign
            {
                get { return _blnCoSign; }
                set { _blnCoSign = value; }
            }

            public bool IsPasswordSettings
            {
                get { return _isPasswordSettings; }
                set { _isPasswordSettings = value; }
            }

            public bool IsExchangeUser
            {
                get { return _isExchangeUser; }
                set { _isExchangeUser = value; }
            }

            public string ExchangeLogin
            {
                get { return _exchangeLogin; }
                set { _exchangeLogin = value; }
            }

            public string ExchangePassword
            {
                get { return _exchangePassword; }
                set { _exchangePassword = value; }
            }

            public Image Signature
            {
                get { return _signature; }
                set { _signature = value; }
            }

            #endregion

            #region "Constructor & Destructor"


            public User()
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

            ~User()
            {
                Dispose(false);
            }

            #endregion

        }

        public class Users : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Users()
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


            ~Users()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(User item)
            {
                _innerlist.Add(item);
            }


            public bool Remove(User item)
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

            public User this[int index]
            {
                get
                { return (User)_innerlist[index]; }
            }

            public bool Contains(User item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(User item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(User[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class gloValidateUser : IDisposable
        {

            #region "Constructor & Destructor"

            public gloValidateUser(string databaseConnectionString)
            {
                _databaseconnectionstring = databaseConnectionString;
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

            ~gloValidateUser()
            {
                Dispose(false);
            }

            #endregion


            #region "Declarations"

            private string _databaseconnectionstring = "";
            private bool _blnIsAcessDenied = false;
            private int _nUserID;

            public int nUserID
            {
                get { return _nUserID; }
                set { _nUserID = value; }
            }
            #endregion "Declarations"


            /// <summary>
            /// Function to check whether the User trying to login is Administrator.
            /// </summary>
            /// <param name="nUserID"></param>
            /// <returns>
            /// If Administrator -> Return TRUE
            /// ELSE -> Return FALSE
            /// </returns>
            public bool chkIsAdminAccess()
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable result;
                try
                {
                    oDB.Connect(false);
                    oParameters.Add("@nUserID", _nUserID, ParameterDirection.Input, SqlDbType.Int);
                    oDB.Retrive("gsp_IsAdministrator", oParameters, out result);
                    if (result != null)
                    {
                        if (!(result.Rows[0]["nAdministrator"] == Convert.DBNull))
                        {
                            return Convert.ToBoolean(result.Rows[0]["nAdministrator"]);
                        }
                        else
                        { return false; }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                    return false;
                }
                finally 
                {
                    oDB.Disconnect();
                    oParameters.Dispose();
                    oDB.Dispose();
                }

            }


            /// <summary>
            /// Checking user's Access if access is denied then return true
            ///  else return false.
            /// </summary>
            /// <returns>
            /// AccessDenied -> True
            /// ELSE -> False
            ///</returns>
            public bool chkIsAccessDenied(string sLoginName)
            {


                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                DataTable dt=null;


                try
                {
                    //string strQuery = "select nUserID,nAccessDenied from User_MST where sLoginName =   '" + txtUserName.Text.Trim() + "' ";
                    string strQuery = "select nUserID,nAccessDenied from User_MST WITH(NOLOCK)  where sLoginName='" + sLoginName + "'";
                    oDB.Retrive_Query(strQuery, out dt);

                    strQuery = null;
                    //Check for Data Table Null  
                    if (dt != null)
                    {
                        if (!(dt.Rows[0]["nAccessDenied"] == Convert.DBNull))
                        {
                            _blnIsAcessDenied = Convert.ToBoolean(dt.Rows[0]["nAccessDenied"]);

                            //Setting the UserID.
                            _nUserID = Convert.ToInt32(dt.Rows[0]["nUserID"]);
                            return _blnIsAcessDenied;


                        }
                        else
                        {
                            return _blnIsAcessDenied = false;
                        }
                    }
                    return _blnIsAcessDenied = false;

                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());

                    return _blnIsAcessDenied = false;

                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    if (dt != null) { dt.Dispose(); dt = null; }
                }


            }


            /// <summary>
            /// Checking whether the User with the UserName & Password  exists or not 
            /// If YES the return true else return false.
            /// </summary>
            /// <param name="sLoginName"></param>
            /// <param name="sPassword"></param>
            /// <returns></returns>
            public bool chkLogin(string sLoginName, string sPassword)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable dt=null;
                try
                {
                    oDB.Connect(false);
                    oParameters.Add("@LoginName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@Password", sPassword, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Retrive("gsp_CheckLogin", oParameters, out dt);

                    if (dt != null)
                    {
                        int i = Convert.ToInt32(dt.Rows[0][0]);
                        if (i != 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    return false;
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());

                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oParameters.Dispose();
                    oDB.Dispose();
                    if (dt != null) { dt.Dispose(); dt = null; }
                }


            }


            /// <summary>
            /// Function to store the Login status and time of logged user
            /// LoginName,DateTime of Login,Machine Name.
            /// </summary>
            /// <param name="sLoginName">Login Name of User</param>
            /// <param name="loginStatus"></param>
            /// <param name="sMachineName"></param>
            public void updateLoginStatus(string sLoginName, bool loginStatus, string sMachineName)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                DataTable result;

                try
                {
                    if (loginStatus)
                    {
                        oDB.Connect(false);
                        oParameters.Add("@sLoginName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sMachineName", sMachineName, ParameterDirection.Input, SqlDbType.VarChar);
                        oDB.Retrive("gsp_InsertLoginUsers", oParameters, out result);

                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null; 
                }
                finally
                {
                    oDB.Disconnect();
                    oParameters.Dispose();
                    oDB.Dispose();
                }

            }



        }

        public class Rights
        {
            #region "Constructor & Distructor"


            public Rights()
            {
                _Appointment = new Appointment();
                _AppointmentBook = new AppointmentBook();
                _Billing = new Billing();
                _BillingBook = new BillingBook();
                _Schedule = new Schedule();
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

            ~Rights()
            {
                Dispose(false);
            }

            #endregion

            private Int64 _UserId;
            private Appointment _Appointment;
            private Schedule _Schedule;
            private Billing _Billing;
            private AppointmentBook _AppointmentBook;
            private BillingBook _BillingBook;

            public Int64 UserId
            {
                get { return _UserId; }
                set { _UserId = value; }
            }
            public Appointment Appointment
            {
                get { return _Appointment; }
                set { _Appointment = value; }
            }
            public Schedule Schedule
            {
                get { return _Schedule; }
                set { _Schedule = value; }
            }
            public Billing Billing
            {
                get { return _Billing; }
                set { _Billing = value; }
            }
            public AppointmentBook AppointmentBook
            {
                get { return _AppointmentBook; }
                set { _AppointmentBook = value; }
            }
            public BillingBook BillingBook
            {
                get { return _BillingBook; }
                set { _BillingBook = value; }
            }
        }

        public class gloRights
        {
            #region "Constructor & Distructor"


            private string _databaseConnectionString = "";
            public gloRights(string databaseConnectionString)
            {
                _databaseConnectionString = databaseConnectionString;
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

            ~gloRights()
            {
                Dispose(false);
            }

            #endregion

            public bool Add(Rights oRights)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                try
                {
                    oDB.Connect(false);
                    oDB.Execute_Query("DELETE FROM User_Rights WHERE nUserID = " + oRights.UserId + "");

                    for (int i = 0; i < oRights.Appointment.Count; i++)
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nUserID", oRights.UserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nModuleID", Modules.Appointment, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nActivityId", oRights.Appointment[i], System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDB.Execute("gsp_InUpUserRights", oDBParameters);
                    }

                    for (int i = 0; i < oRights.Schedule.Count; i++)
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nUserID", oRights.UserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nModuleID", Modules.Schedule, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nActivityId", oRights.Schedule[i], System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDB.Execute("gsp_InUpUserRights", oDBParameters);
                    }

                    for (int i = 0; i < oRights.Billing.Count; i++)
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nUserID", oRights.UserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nModuleID", Modules.Billing, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nActivityId", oRights.Billing[i], System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDB.Execute("gsp_InUpUserRights", oDBParameters);
                    }

                    for (int i = 0; i < oRights.AppointmentBook.Count; i++)
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nUserID", oRights.UserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nModuleID", Modules.AppointmentBook, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nActivityId", oRights.AppointmentBook[i], System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDB.Execute("gsp_InUpUserRights", oDBParameters);
                    }

                    for (int i = 0; i < oRights.BillingBook.Count; i++)
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nUserID", oRights.UserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nModuleID", Modules.BillingBook, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nActivityId", oRights.BillingBook[i], System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDB.Execute("gsp_InUpUserRights", oDBParameters);
                    }

                    return true;

                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());

                    return false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }
            }

            public DataTable GetModuleNames()
            {
                DataTable dtRights = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "SELECT distinct nModuleID FROM  Rights_MST  WITH(NOLOCK) order by nModuleID";
                    oDB.Retrive_Query(strQuery, out dtRights);
                    strQuery = null;
                    return dtRights;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
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
                    oDB.Dispose();
                }
            }

            public DataTable GetActivityNames(int ModuleId)
            {
                DataTable dtRights = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "SELECT distinct nActivityID FROM  Rights_MST  WITH(NOLOCK) WHERE nModuleID = " + ModuleId + " order by nActivityID";
                    oDB.Retrive_Query(strQuery, out dtRights);
                    strQuery = null;
                    return dtRights;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
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
                    oDB.Dispose();
                }
            }

            public DataTable GetUserRights(Int64 UserId, Int32 ModuleId)
            {
                DataTable dtRights = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    // String strQuery = "SELECT nActivityID FROM User_Rights WHERE nUserID = " + UserId + " AND nModuleID = " + ModuleId + " order by nActivityID";

                    string strQuery = "SELECT  distinct Rights_MST.nActivityID FROM User_Rights  WITH(NOLOCK) INNER JOIN Rights_MST  WITH(NOLOCK) " +
                                      "ON User_Rights.nModuleID = Rights_MST.nModuleID " +
                                      "WHERE (User_Rights.nUserID = " + UserId + ") AND (User_Rights.nModuleID = " + ModuleId + ") ORDER BY nActivityID";
                    oDB.Retrive_Query(strQuery, out dtRights);
                    strQuery = null;
                    return dtRights;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
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
                    oDB.Dispose();
                }
            }

            //Delete all records from Rights_Master 
            public bool DeleteRights()
            {
                //DataTable dtRights = new DataTable();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    String strQuery = "DELETE Rights_MST";
                    oDB.Execute_Query(strQuery);
                    strQuery = null;
                    return true;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                    return false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            //Add records to Rights_Master  
            public bool AddRight(int ModuleId, int ActivityID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    oDB.Connect(false);
                    string strQuery = "INSERT INTO Rights_MST VALUES(" + ModuleId + "," + ActivityID + ")";
                    oDB.Execute_Query(strQuery);
                    strQuery = null;
                    return true;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                    return false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }
            }

            public DataTable GetMasterRights()
            {
                DataTable dtRights = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    string strQuery = "SELECT nModuleID,nActivityID FROM Rights_MST  WITH(NOLOCK) ";

                    oDB.Retrive_Query(strQuery, out dtRights);
                    strQuery = null;
                    return dtRights;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
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
                    oDB.Dispose();
                }
            }
        }

}

