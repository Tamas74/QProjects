using System;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized; 

namespace gloEmdeonInterface.Classes
{
    
   
    public class clsInsertEmdeonData
    {  
        // This is class is created by Abhijeet
        // purpose : it used to store an gloLab order details into EMR data.
        // It contains function like creating order id,test id,transaction id,visit id,Icd master entry etc.

   #region class level variable declaration

        private string _ConnectionString;
        private GloLabOrder_Msts _objEmdeOrdon_Msts;
        
        private string _ExternalCode;
        private string _gloLabOrderId;
        private long _PatientCode;
        private DateTime _TransDate;
        private string _LabTestName;
        private string _labcode;
        private long _ProviderId;
        private Int32 _IntPatientAgeyear;
        private Int32 _IntPatientAgeMonth;
        private Int32 _IntPatientAgeDay;
        private long _UserId = 1;
        private string _UserName = " ";
        //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order
        private Int32 Count = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        // By Abhijeet On Date 20100222 & 20100330
        private string _gloLabICDCode;
        private string _gloLabDiagDesc;
      //  private string _gloLabOrderStatus;
      //  private string _gloLabOrderBillingType;
        private clsGeneral.OrderStatus _gloLabeOrderStatus;
        private clsGeneral.OrderBillingType _gloLabeOrderBillingType;


        // By Abhijeet on date 20100318
        //defines the sql connection variable to maintain transaction during saving order details in EMR database. 

        private SqlConnection mySqlConn;
        private SqlTransaction mySqlTransaction; // = new SqlTransaction();
        private SqlCommand mySqlCommand;// = new SqlCommand();


        #endregion class level variable declaration
       //Added by madan on 20100612        
        private Int64 _ModifiedOrderId = 0;

        public Int64 ModifiedOrderId
        {
            get { return _ModifiedOrderId; }
            set { _ModifiedOrderId = value; }
        }

        //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order
        private Int64 _MySelectedOrderID = 0;
        public Int64 GetMySelectedOrderId
        {
            get { return _MySelectedOrderID; }
            set { _MySelectedOrderID = value; }
        }
        private Boolean _IsSplitOrder = false;
        public Boolean IsSplitOrder
        {
            get { return _IsSplitOrder; }
            set { _IsSplitOrder = value; }
        }
        private string _ModifiedOrderRefreanceID = string.Empty;

        public string ModifiedOrderRefreanceID
        {
            get { return _ModifiedOrderRefreanceID; }
            set { _ModifiedOrderRefreanceID = value; }
        }

        //end madan.

        //Added by Amit 28/05/2013 7031
        private Boolean _OrderNotCPOE = false;
        public Boolean OrderNotCPOE
        {
            get { return _OrderNotCPOE; }
            set { _OrderNotCPOE = value; }
        }

   #region Constructor declarion

        // DO NOT CREATE DEFAULT CONSTRUCTOR,CONSTRUCTOR WITHOUT CONNECTION STRING  AND ORDER COLLECTION OBJECT
      
        public clsInsertEmdeonData(String sDataBaseConnectionString, GloLabOrder_Msts objEmdeOrdon_Msts)
        {
            _ConnectionString = sDataBaseConnectionString;
            _objEmdeOrdon_Msts = objEmdeOrdon_Msts;
        }
        public clsInsertEmdeonData(String sDataBaseConnectionString, GloLabOrder_Msts objEmdeOrdon_Msts, long LngUserId, string StrUserName)
        {
            _ConnectionString = sDataBaseConnectionString;
            _objEmdeOrdon_Msts = objEmdeOrdon_Msts;
            _UserId = LngUserId;
            _UserName = StrUserName;
        }

  #endregion for Constructor declarion


   #region methods defination

        private void BeginMyTransaction()
        {
            try
            {
                if (mySqlConn != null)
                {
                    if (mySqlConn.State == ConnectionState.Open)
                        mySqlConn.Close();
                    mySqlConn.Dispose();
                    mySqlConn = null;
                }
                mySqlConn = new SqlConnection();
                mySqlConn.ConnectionString = _ConnectionString;
                mySqlConn.Open();
                mySqlTransaction = mySqlConn.BeginTransaction();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void EndMyTransaction()
        {
            try
            {
                mySqlTransaction.Commit();
                if(mySqlConn != null)
                {
                    if (mySqlConn.State == ConnectionState.Open)
                        mySqlConn.Close();
                    mySqlConn.Dispose();
                    mySqlConn = null;
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RollbackMyTransaction()
        {
            try
            {

                if (mySqlTransaction != null)
                {
                    mySqlTransaction.Rollback(); 
                }
                
                if (mySqlConn != null)
                {
                    if (mySqlConn.State == ConnectionState.Open)
                        mySqlConn.Close();
                    mySqlConn.Dispose();
                    mySqlConn = null;
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetMyCommand(string strQuery)
        {
            try
            {
                if (mySqlCommand != null)
                {
                    mySqlCommand.Dispose();
                    mySqlCommand = null;
                }
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConn;
                mySqlCommand.Transaction = mySqlTransaction;
                mySqlCommand.CommandType= CommandType.Text;
                mySqlCommand.CommandText= strQuery;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public bool SaveOrdTestDtl()
        {   // function used to Inserting  order Test details from object of class type EmdeonOrder_Msts     

            Boolean BoolStatusFlg = false;

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);

            try
            {
                Int32 _LineNo = 0;

                for (int i = 0; i < _objEmdeOrdon_Msts.Count; i++) // loop for order id
                {
                    _ExternalCode = Convert.ToString(_objEmdeOrdon_Msts[i].GloLabOrderNumber); //Ordrer numer
                    _gloLabOrderId = Convert.ToString(_objEmdeOrdon_Msts[i].GloLabOrderID);  
                    _PatientCode = _objEmdeOrdon_Msts[i].PatientID;
                    _TransDate = _objEmdeOrdon_Msts[i].TransactionDate;
                    _ProviderId = _objEmdeOrdon_Msts[i].PatientProviderID;

                    //code start By Abhijeet on date 20100415 to find patient age
                    //_IntPatientAgeyear = Convert.ToInt32(_objEmdeOrdon_Msts[i].PatientAgeYear);
                    //_IntPatientAgeMonth = Convert.ToInt32(_objEmdeOrdon_Msts[i].PatientAgeMonth);
                    //_IntPatientAgeDay = Convert.ToInt32(_objEmdeOrdon_Msts[i].PatientAgeDays);
                    
                    if (_objEmdeOrdon_Msts[i].PatientAgeYear > 0 && _objEmdeOrdon_Msts[i].PatientAgeMonth > 0 && _objEmdeOrdon_Msts[i].PatientAgeDays > 0)
                    {
                        //TimeSpan Tp = new TimeSpan();-- Commneted by madan to fix the warnings on 20100520
                        //Tp= DateTime.Now.Subtract(


                        //oTS = new TimeSpan();
                        //oTS = _CurrentDate.Subtract(_BaseDate);
                        //strID1 = oTS.Days.ToString().Replace("-", "");

                        //Convert.ToDateTime("

                         DateTime dt = Convert.ToDateTime(_objEmdeOrdon_Msts[i].PatientAgeMonth.ToString().Trim() +"/" +  _objEmdeOrdon_Msts[i].PatientAgeDays.ToString().Trim() + "/"+_objEmdeOrdon_Msts[i].PatientAgeYear.ToString().Trim());
                        int year = 0;
                        int month = 0;
                        int day = 0;
                        clsGeneral oclsGeneral = new clsGeneral();
                        oclsGeneral.formatAge(dt, out day, out month, out year);
                        oclsGeneral.Dispose();

                        _IntPatientAgeyear = year;
                        _IntPatientAgeMonth = month;
                        _IntPatientAgeDay = day;
                    }
                    else
                    {
                        _IntPatientAgeyear = 0;
                        _IntPatientAgeMonth = 0;
                        _IntPatientAgeDay =0;
                    }
                    // End of changes By Abhijeet on date 20100415 to find patient age
                    _LineNo = 0;

                    
                    // Added by Abhijeet on Date 20100330                  
                    //_gloLabOrderStatus =  _objEmdeOrdon_Msts[i].OrderStatus;
                   // _gloLabOrderBillingType = _objEmdeOrdon_Msts[i].BillingType;

                    clsGeneral objclsGeneral = new clsGeneral();
                    _gloLabeOrderStatus =objclsGeneral.GetOrderStatusEnum(_objEmdeOrdon_Msts[i].OrderStatus);
                    _gloLabeOrderBillingType =objclsGeneral.GetBillingTypeEnum(_objEmdeOrdon_Msts[i].BillingType);
                    objclsGeneral.Dispose();
                    // End of changes by Abhijeet for storing order status & billing type
                    try
                    {
                        BeginMyTransaction();  

                        //Added by madan on 20100612
                        long lngOrdid = 0;
                        if (ModifiedOrderId > 0 && ModifiedOrderRefreanceID !="")
                        {
                            lngOrdid = ModifiedOrderId;
                            EditEmdeonOrder(ModifiedOrderId);
                        }
                        else
                        {
                            // Insert - Update order MST 
                            lngOrdid = GetLabOrderID(_ExternalCode);
                            //** Insert - Update order MST
                            
                            
                        }

                        //end madan----

                        for (int j = 0; j < _objEmdeOrdon_Msts[i].EmdeonTests.Count; j++) // loop for test name under above order id
                        {
                            _LabTestName = _objEmdeOrdon_Msts[i].EmdeonTests[j].GloLabTestName;
                            _labcode = _objEmdeOrdon_Msts[i].EmdeonTests[j].GloLabTestCode;
                            _LineNo = j + 1;
                            
                            // Insert - Update test MST 
                            long lngTestid = getLabTestID(_LabTestName);
                            //** Insert - Update test MST 

                            // Insert - Update tests details 
                            if (lngOrdid != 0 && lngTestid != 0)
                            {
                                // Code For Inserting Order test details for new one or Update for existing one.

                                string strquery = "select count(*) from Lab_Order_TestDtl where labotd_OrderID=" + lngOrdid.ToString() + " and labotd_TestID=" + lngTestid.ToString();

                                Int32 IntRowCnt = 0;
                             
                                SetMyCommand(strquery);
                                IntRowCnt = Convert.ToInt32(mySqlCommand.ExecuteScalar());
                                mySqlCommand.Dispose();
                                mySqlCommand = null;


                                if (IntRowCnt > 0) // if available then update
                                {
                                    //commented by Abhijeet on date 20100327 .
                                   // strquery = "update Lab_Order_TestDtl set labotd_DateTime='" + DateTime.Now.Date + "',labotd_TestName='" + _LabTestName + "' where labotd_OrderID=" + lngOrdid.ToString() + " and labotd_TestID=" + lngTestid.ToString();
                                }
                                else  // if not available then insert
                                {
                                    // by Abhijeet on date 20100327. Initialy user id is taking from app setting
                                    // but as like in EMR and less numeric filed on table now it is storing 1.
                                    Int32 intUserID = 1;

                                    //Problem No: 00000429: Lab Orders
                                    //Description: Query change Replace function added for _LabTestName variable.
                                    strquery = "insert into  Lab_Order_TestDtl(labotd_OrderID, labotd_TestID," +
                                     "labotd_LineNo, labotd_SpecimenID ,labotd_CollectionID,labotd_StorageID," +
                                     "labotd_DateTime,labotd_TestName,labotd_Note,labotd_LOINCCode," +
                                     "labotd_Instruction,labotd_Precaution,labotd_Comment,labotd_DMSID," +
                                     "labotd_SpecimenName,labotd_CollectionName,labotd_StorageName,labotd_UserID) values(" +
                                     lngOrdid.ToString() + "," + lngTestid + "," + _LineNo + ",0,0,0,'" +
                                     Convert.ToString(_TransDate) + "','" + _LabTestName.Replace("'", "''") +
                                     "',' ',' ',' ',' ',' ',0,' ',' ',' '," + intUserID.ToString() + ")";

                                    //  End of changes by abhijeet

                                    SetMyCommand(strquery);
                                    mySqlCommand.ExecuteNonQuery();
                                    mySqlCommand.Dispose();
                                    mySqlCommand = null;


                                    // End of code for saving order test details
                                    int nICDRevision = 0;
                                    for (int k = 0; k < _objEmdeOrdon_Msts[i].EmdeonTests[j].GloLabTestsDiagnoses.Count; k++)
                                    {
                                        _gloLabICDCode = _objEmdeOrdon_Msts[i].EmdeonTests[j].GloLabTestsDiagnoses[k].GloLabICD9Code;
                                        _gloLabDiagDesc = _objEmdeOrdon_Msts[i].EmdeonTests[j].GloLabTestsDiagnoses[k].GloLabDiagDescription;
                                        //added for icd10 feature
                                        nICDRevision = _objEmdeOrdon_Msts[i].EmdeonTests[j].GloLabTestsDiagnoses[k].nICDRevision ;
                                        if (_gloLabICDCode.Trim() != "" && _gloLabDiagDesc.Trim() != "")
                                        {
                                            // Code for saving Diagnosis details for Lab order
                                            //  code for checking diagnosis is available or not in Matser table
                                            //Problem No: 00000429: Lab Orders
                                            //Description: Query change Replace function added for _gloLabDiagDesc variable.

                                            //Problem No: Bug #61728: 00000597: The lab orders show up on Emdeon's site but not in the orders tab in gloEMR 
                                            //Commented line and added new line with only sICD9Code as discription may be different. To avoid duplication of ICDCode.
                                            //strquery = "select count(*) from ICD9 where sICD9Code='" + _gloLabICDCode.Trim() + "' and sDescription='" + _gloLabDiagDesc.Trim().Replace("'", "''") + "'";
                                            strquery = "select count(*) from ICD9 where sICD9Code='" + _gloLabICDCode.Trim() + "'";

                                            SetMyCommand(strquery);
                                            IntRowCnt = 0;
                                            IntRowCnt = Convert.ToInt32(mySqlCommand.ExecuteScalar());
                                            mySqlCommand.Dispose();
                                            mySqlCommand = null;

                                            long lngIcdID = 0;
                                            if (!(IntRowCnt > 0))
                                            {
                                                // Code for generating Unique ICD ID
                                                lngIcdID = GenerateUnique_IcdID(_PatientCode);
                                                //Problem No: 00000429: Lab Orders
                                                //Description: Query change Replace function added for _gloLabDiagDesc variable.
                                                strquery = "insert into ICD9 (nICD9ID,sICD9Code,sDescription,nSpecialtyID,nClinicID,bIsBlocked,bInActive)" +
                                                    " values (" + lngIcdID.ToString() + ",'" + _gloLabICDCode + "','" + _gloLabDiagDesc.Replace("'", "''") + "',1,1,'False','False')";


                                                SetMyCommand(strquery);
                                                mySqlCommand.ExecuteNonQuery();
                                                mySqlCommand.Dispose();
                                                mySqlCommand = null;
                                            }

                                            // Inserting Diadnosis deatils into database
                                            
                                            //Problem No: 00000429: Lab Orders
                                            //Description: Query change Replace function added for _LabTestName and _gloLabDiagDesc variable.
                                            //nicdrevision added for icd10 feature
                                            //Updated by Mayuri: 20160824-update detail type to 1 for all diagnosis against test
                                            strquery = "insert into Lab_Order_TestDtl_DiagCPT(labodtl_OrderID,labodtl_TestID,labodtl_DiagCPTID," +
                                                "labodtl_Code,labodtl_Description,labodtl_Type,labodtl_TestName,nICDRevision) values(" + lngOrdid.ToString() + "," +
                                                lngTestid.ToString() + ",1,'" + _gloLabICDCode + "','" + _gloLabDiagDesc.Replace("'", "''") + "',1,'" + _LabTestName.Replace("'", "''") + "',"+nICDRevision.ToString() +")";

                                          
                                            SetMyCommand(strquery);
                                            mySqlCommand.ExecuteNonQuery();
                                            mySqlCommand.Dispose();
                                            mySqlCommand = null;

                                        }

                                    }

                                    //by Abhijeet on 20100512                
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Successfully added external lab order ", _PatientCode, lngOrdid, _ProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                }
                             
                            }
                            else
                            {
                                RollbackMyTransaction();
                                break;
                            }                           
                            //** Insert - Update tests details 
                        } // end of loop for test name  
                        EndMyTransaction();                       
                    }
                    catch (SqlException e)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("GL16 - " + e.ToString(), false);
                        //clsGeneral objclsGen = new clsGeneral();
                        //objclsGen.UpdateLog("GL16 - Insert Emdeon Data: " + e.ToString());
                        BoolStatusFlg = false;
                        RollbackMyTransaction();
                    }
                    catch (Exception e)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("GL17 - " + e.ToString(), false);
                        //clsGeneral objclsGen = new clsGeneral();
                        //objclsGen.UpdateLog("GL17 - Insert Emdeon Data: " + e.ToString());
                        BoolStatusFlg = false;
                        RollbackMyTransaction();
                    }                  

                } // end of loop for order id

               // UpdateOrderStatusBillingType() ; // by Abhijeet on 20100331
                BoolStatusFlg = true;
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL18 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL18 - Insert Emdeon Data: " + ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL19 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL19 - Insert Emdeon Data: " + ex.ToString());
                return false;
            }
            finally
            {
               
            }
            return BoolStatusFlg;
        } // end of function SaveOrdTestDtl()
        //Added by madan on 20100612
        private bool EditEmdeonOrder(Int64 _OrderId)
        {
            bool _blnStatus = false;
            string strQuery = string.Empty;
            string strMachineName=string.Empty;
            long lngVisitID = 0;
            int _Result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            try
            {
               
                lngVisitID = GenerateVisitID(DateTime.Now);
                strMachineName=Environment.MachineName;

                oDB.Connect(false);

                //29-May-13 Aniket: Orders PRD: Change status from New to Sent
                if (_gloLabeOrderStatus.GetHashCode().ToString()=="12")
                {
                    strQuery = "UPDATE Lab_Order_MST SET OrderStatusNumber=1005 Where OrderStatusNumber=1001 AND labom_OrderID='" + _OrderId + "' AND labom_dgloLabOrderID=" + ModifiedOrderRefreanceID;
                    oDB.Execute_Query(strQuery);
                }

                

                strQuery = "UPDATE Lab_Order_MST SET Labom_TransactionDate='" +DateTime.Now.ToString() + "' ,labom_PatientAgeYear='" + _IntPatientAgeyear.ToString() +
                           "' ,labom_patientAgeMonth= '" + _IntPatientAgeMonth.ToString() + "',labom_patientAgeday='" + _IntPatientAgeDay.ToString() +
                           "' ,labom_providerID='" + _ProviderId.ToString() + "' ,labom_Externalcode='" + _ExternalCode + "' ,labom_VisitID=' " + lngVisitID.ToString() +
                           "' ,labom_MachineName='' ,labom_UserName=''" +
                           " ,labom_gloLabOrderBillingType='" + _gloLabeOrderBillingType.GetHashCode().ToString() +
                           "' ,labom_gloLabOrderStatus=' " + _gloLabeOrderStatus.GetHashCode().ToString() + "',labom_OrderDate='" + _TransDate.ToString() + "', blnOrderNotCPOE ='" + _OrderNotCPOE.ToString()  + "' Where labom_OrderID='" + _OrderId + "' AND labom_dgloLabOrderID=" + ModifiedOrderRefreanceID;

                _Result = oDB.Execute_Query(strQuery);
                if (_Result < 0)
                    _blnStatus = false;
                else if (_Result >= 0)
                    _blnStatus = true;
                else
                    _blnStatus = false;

                if (_blnStatus)
                {
                    strQuery = "DELETE FROM Lab_Order_TestDtl WHERE labotd_OrderID = " + _OrderId;
                    if (DeleteTable(strQuery))
                    {
                        strQuery = string.Empty;
                        strQuery = "DELETE FROM Lab_Order_TestDtl_DiagCPT WHERE labodtl_OrderID =" + _OrderId;
                        if (DeleteTable(strQuery))
                        {
                            strQuery = string.Empty;
                            strQuery = "DELETE FROM Lab_Order_Test_Result where labotr_OrderID = " + _OrderId;
                            if (DeleteTable(strQuery))
                            {
                                strQuery = string.Empty;
                                strQuery = "DELETE FROM Lab_Order_Test_ResultDtl where labotrd_OrderID = " + _OrderId;
                                if (DeleteTable(strQuery))
                                {
                                    _blnStatus = true;
                                }
                                else
                                    return false;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in query"+ strQuery.ToString()+" " + ex.ToString(), false);
            }
            finally
            {
                if (oDB !=null)
                {
                    oDB.Dispose();                    
                }
                strQuery = string.Empty;
                lngVisitID = 0;
                _Result = 0;
                strMachineName = string.Empty;
            }
            return _blnStatus;
        }
        //Added by madan on 20100612
        private bool DeleteTable(string strQuery)
        {
            bool _blnResult = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            int _result = 0;
            try
            {
                oDB.Connect(false);
                if (strQuery !="")
                {
                    _result = oDB.Execute_Query(strQuery);
                    if (_result < 0)
                        _blnResult = false;
                    else if (_result >= 0)
                        _blnResult = true;
                    else
                        _blnResult = false;
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL19 - " + ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                strQuery = string.Empty;
            }
            return _blnResult;

        }

        private void UpdateOrderStatusBillingType()
        { // function added by Abhijeet on Date 20100330 for updating order status & order billing type
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            try
            {oDB.Connect(false);
                // retrieving all order having status Entered 'E' Or null                
                string strGetOrders = "select labom_OrderID,labom_OrderNoID,labom_ExternalCode," +
                    "labom_dgloLabOrderID,labom_gloLabOrderStatus,labom_gloLabOrderBillingType from " +
                    "Lab_Order_MST where (labom_gloLabOrderStatus=4 or labom_gloLabOrderStatus is null) and labom_dgloLabOrderID is not null";
                DataTable dtOrders = null;
                
                oDB.Retrive_Query(strGetOrders, out dtOrders);
                if (dtOrders != null && dtOrders.Rows.Count > 0)
                {
                    for (Int16 cnt = 0; cnt < dtOrders.Rows.Count; cnt++)
                    {
                        Boolean boolIsUpdated = false;

                        if (Convert.ToDouble(dtOrders.Rows[cnt]["labom_dgloLabOrderID"])>0)
                        {
                            // boolIsUpdated  = OrderStatusUpdate(dtOrders.Rows[cnt]["labom_dgloLabOrderID"],clsGeneral.OrderStatus.ReadyToTransmit)               

                            if (boolIsUpdated) 
                            {
                                // code for updating order status in EMR data
                                string strUpdateQry = "update Lab_order_mst set labom_gloLabOrderStatus=12 " +
                                    " where labom_OrderID=" + dtOrders.Rows[cnt]["labom_OrderID"].ToString();
                                oDB.Execute_Query(strUpdateQry);
                            }
                        }                       
                    }
                }
                if (dtOrders != null)
                {
                    dtOrders.Dispose();
                    dtOrders = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
               // objclsGen.UpdateLog("Error at Updating Order Status & Billing Type: " + ex.ToString());
            }
            finally
            {
                if (oDB != null)
                {      oDB.Disconnect();             
                    oDB.Dispose();
                }
            }
        }
       
        private long GetLabOrderID(string EOrdId)
        {    // Function used to get EMR order id against the Emdeon order ID
            // It first search the emdeon order id is available or not , if not then it generate the 
            // order id and then return it.    

          // gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            try
            {               
                long intOrdID = 0;
                Int32 intOrdNoID = 0;
                string strQuery = "";
                // checking lab order id is available or not                              
                string QryStr = "select labom_orderid from Lab_order_mst where labom_externalcode='" + _ExternalCode.Trim() + "' and labom_dgloLabOrderID IS NOT NULL";       //_gloLabOrderId.Trim() + "'";          
             
                SetMyCommand(QryStr);
                object Tmpobj = mySqlCommand.ExecuteScalar();
                mySqlCommand.Dispose();
                mySqlCommand = null;

                if (Tmpobj == null)
                    intOrdID = 0;
                else
                    intOrdID = Convert.ToInt64(Tmpobj); 
                
                            
                if (intOrdID == 0)  // Lab order ID is not available
                {
                    // code for Creation of new lab order id

                    // generating Unique order id as per glostream standard
                   
                        intOrdID = GenerateUnique_OrderID(_PatientCode);
                        //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order
                    if (!_IsSplitOrder)
                    {
                        if (_MySelectedOrderID !=0)
                        {
                            if (Count != 1)
                            {
                                string QryStrc = "select labom_OrderNoID  from Lab_order_mst WHERE labom_OrderID='" + _MySelectedOrderID + "'";
                                SetMyCommand(QryStrc);
                                object tempobj = mySqlCommand.ExecuteScalar();
                                mySqlCommand.Dispose();
                                mySqlCommand = null;
                                intOrdNoID = Convert.ToInt32(tempobj);
                                Count = 1;
                            }
                        }
                    }  
                   
                   
                    

                    if (intOrdID == 0)
                    {
                        clsGeneral objclsGen = new clsGeneral();
                        objclsGen.UpdateLog("GL1 - Insert Emdeon Data: " + " Error in generating unique order id");
                        objclsGen.Dispose();
                        return 0;   
                    }

                    // creating new Order Number ID

                    if (intOrdNoID == 0)
                    {
                        QryStr = "select isnull(max(labom_OrderNoID),0)+1 from Lab_Order_MST";
                        SetMyCommand(QryStr);
                        intOrdNoID = Convert.ToInt32(mySqlCommand.ExecuteScalar());
                        mySqlCommand.Dispose();
                        mySqlCommand = null;
                    }

                    long lngVisitID = 0;
                   // lngVisitID = GenerateVisitID(_TransDate);
                    lngVisitID = GenerateVisitID(DateTime.Now);
                    if (lngVisitID == 0)
                    {
                        clsGeneral objclsGen = new clsGeneral();
                        objclsGen.UpdateLog("GL2 - Insert Emdeon Data: " + " Error in generating Visit id");
                        objclsGen.Dispose();
                        return 0;
                    }

                    String StrMachineName = Environment.MachineName;
                    Int32 intOrderStatusNumber;

                    //04-Jun-13 Aniket: Resolving Bug 51736
                    if (_gloLabeOrderStatus.GetHashCode().ToString() == "12")
                    {
                        intOrderStatusNumber = 1005; //Sent

                    }
                    else 
                    {
                        intOrderStatusNumber = 1001; //New
                    }

                    // Inserting the newly generated Lab order into Lab order master                   

                    //29-Sep-2014 Aniket: Insert the Order Creator User for MU Stage 2 Report
                    strQuery = "INSERT INTO Lab_order_Mst(Labom_orderid,LabOm_OrderNoPrefix,Labom_OrderNoID," +
                        "Labom_TransactionDate,Labom_patientID,labom_PatientAgeYear,labom_patientAgeMonth," +
                        "labom_patientAgeday,labom_providerID,labom_Externalcode,labom_PreferredLabID," +
                        "labom_SampledByID,labom_ReferredByID,labom_DMSID,labom_PreferredLabName," +
                        "labom_SampledByName,labom_ReferredByFName,labom_ReferredByMName,labom_ReferredByLName," +
                        "nClinicID,labom_VisitID,labom_MachineName,labom_UserName,labom_dgloLabOrderID,labom_gloLabOrderStatus,labom_gloLabOrderBillingType,labom_OrderDate, blnOrderNotCPOE, OrderStatusNumber, sOrderCreaterUser) values(" +
                        intOrdID.ToString() + ",'" + "ORD" + "'" + "," + intOrdNoID + ",'" + _TransDate.ToString() +
                        "'," + _PatientCode.ToString() + "," + _IntPatientAgeyear.ToString() + "," +
                        _IntPatientAgeMonth.ToString() + "," + _IntPatientAgeDay.ToString() + "," +
                        _ProviderId.ToString() + ",'" + _ExternalCode + "',0,0,0,0,' ',' ',' ',' ',' ',1," +
                        lngVisitID.ToString() + ",'',''," + Convert.ToDecimal(_gloLabOrderId) + "," + _gloLabeOrderStatus.GetHashCode().ToString() + ",'" + _gloLabeOrderBillingType.GetHashCode().ToString() + "','" + _TransDate.ToString() + "','" + _OrderNotCPOE.ToString() + "'," + intOrderStatusNumber + ",'" + appSettings["UserName"] + "')";
                                    
                    SetMyCommand(strQuery);
                    mySqlCommand.ExecuteNonQuery();
                    mySqlCommand.Dispose();
                    mySqlCommand = null;
               
                    return intOrdID;
                }
                else  // lab order ID is already exist
                {
                    //case No: Case 16553 / Incident 1104 (For 7004 Version) 
                    //Developer Name : Mitesh Patel

                    //Bug #53611: 00000492 : Lab Orders
                    //Update Query changed to resolve issue.
                    strQuery = "UPDATE Lab_Order_MST SET labom_gloLabOrderStatus='" + _gloLabeOrderStatus.GetHashCode().ToString() + "', labom_gloLabOrderBillingType ='" + _gloLabeOrderBillingType.GetHashCode().ToString() + "', labom_dgloLabOrderID= " + Convert.ToDecimal(_gloLabOrderId) + " Where labom_OrderID='" + intOrdID + "'";
                    SetMyCommand(strQuery);
                    mySqlCommand.ExecuteNonQuery();
                    mySqlCommand.Dispose();
                    mySqlCommand = null;
                    
                    //29-May-13 Aniket: Orders PRD: Change status from New to Sent
                    if (_gloLabeOrderStatus.GetHashCode().ToString() == "12")
                    {
                        strQuery = "UPDATE Lab_Order_MST SET OrderStatusNumber=1005 Where OrderStatusNumber=1001 AND labom_OrderID='" + intOrdID + "' AND labom_dgloLabOrderID=" + Convert.ToDecimal(_gloLabOrderId);
                        SetMyCommand(strQuery);
                        mySqlCommand.ExecuteNonQuery();
                        mySqlCommand.Dispose();
                        mySqlCommand = null;
                       
                    }
                    
                    return intOrdID;
                }

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL2 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL2 - Insert Emdeon Data: " + ex.ToString());

                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL4 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL4 - Insert Emdeon Data: " + ex.ToString());

                return 0;
            }
                   
        } // End of  function GetLabOrderID(string EOrdId)

        // Function Used to Create the Lab Order ID for a Patient as per glostream standard and it return generated Lab order id
        private long GenerateUnique_OrderID(long LngPatientCode)
        {
           // gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            try
            {
                              
                long MachineId = 0;
                Int64 _OrderID = 0;
                string strquery = "";

                // getting the Prefix Transaction ID
                MachineId = GetPrefixTransactionID(_PatientCode);

                strquery = "SELECT labom_OrderID FROM Lab_Order_MST WHERE convert(varchar(18),labom_OrderID) Like convert(varchar(18)," + MachineId + " )+ '%'";
                             
                SetMyCommand(strquery);
                object Tmpobj = mySqlCommand.ExecuteScalar();
                mySqlCommand.Dispose();
                mySqlCommand = null;


                if (Tmpobj == null)
                    _OrderID = 0;
                else
                _OrderID = Convert.ToInt64(Tmpobj);
                            

                if (_OrderID == 0)
                {
                    strquery = "select convert(numeric(18,0), convert(varchar(18)," + MachineId + ") + '01')";
                }
                else
                {
                    strquery = "select isnull(max(labom_OrderID),0)+1 from Lab_Order_MST where convert(varchar(18),labom_OrderID) Like convert(varchar(18)," + MachineId + " )+ '%'";
                }
                              
                SetMyCommand(strquery);
                Tmpobj = mySqlCommand.ExecuteScalar();
                mySqlCommand.Dispose();
                mySqlCommand = null;

                _OrderID = Convert.ToInt64(Tmpobj);

                return _OrderID;
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL5 - " + ex.ToString(), false);
                // clsGeneral objclsGen = new clsGeneral();
               // objclsGen.UpdateLog("GL5 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL6 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL6 - Insert Emdeon Data: : " + ex.ToString());
                return 0;
            }
                                                           
        }  // end of function GenerateUnique_OrderID()

        // end of function GenerateUnique_OrderID(long LngPatientCode)
        private Int64 GetPrefixTransactionID(Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = "";

           // gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);

            try
            {
                if (PatientID > 0)
                {
                    _strSQL = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientID + "";
                                    
                    SetMyCommand(_strSQL);
                    _internalresult = mySqlCommand.ExecuteScalar();
                    mySqlCommand.Dispose();
                    mySqlCommand = null;

                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                }

                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

                  return _Result;

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL7 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL7 - Insert Emdeon Data: " + ex.ToString());
               
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL8 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL8 - Insert Emdeon Data: " + ex.ToString());

                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }                    
        }
      
        private long getLabTestID(string StrLabName)
        {    // Function used to get Lab test id against the lab test Name
            // It first search the labe test name is available or not , if not then it generate the 
            // new lab test id and then return it.      

           // gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);

            try
            {
                long IntLabTestID = 0;

                // checking lab Test  is available or not                              
                string QryStr = "select labtm_ID from Lab_Test_Mst where labtm_Name='" + StrLabName.Trim().Replace("'", "''") + "'";              
                            
                SetMyCommand(QryStr);
                object Tmpobj = mySqlCommand.ExecuteScalar();
                mySqlCommand.Dispose();
                mySqlCommand = null;

                if (Tmpobj == null)
                    IntLabTestID = 0;
                else
                    IntLabTestID = Convert.ToInt64(Tmpobj);
            
                if (IntLabTestID == 0)  // Lab test is not available
                {
                    // generate unique lab test id as per glostream standard
                    IntLabTestID = GenerateUnique_TestMSTID(_PatientCode);

                    if (IntLabTestID == 0)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("GL9 - " + " Error in generating lab test id", false);
                        //clsGeneral objclsGen = new clsGeneral();
                        //objclsGen.UpdateLog("GL9 - Insert Emdeon Data: " + " Error in generating lab test id");
                        return 0;
                    }
                    //Problem No: 00000429: Lab Orders
                    //Description: Query change Replace function added for _LabTestName and _labcode variable.
                    string strQry = "insert into Lab_test_MST(labtm_id,labtm_code,labtm_Ordarable,labtm_name," +
                                    "labtm_deprtcatID,labtm_testHeadID,labtm_resultType,nClinicID,labtm_SpecimenID," +
                                    "labtm_CollectionID,labtm_StorageID,labtm_Instruction,labtm_Precuation,labtm_LOINCId," +
                                    "labtm_SpecimenName,labtm_CollectionName,labtm_StorageName,dtupdateddate) values(" + IntLabTestID.ToString() +
                                    ",'" + _labcode.Replace("'", "''") + "','" + "True" + "','" + _LabTestName.Replace("'", "''") + "',0,0,1,1,0,0,0,' ',' ',' ',' ',' ',' ',dbo.gloGetDate())";
                    //dtupdateddate column added 
                    SetMyCommand(strQry);
                    mySqlCommand.ExecuteNonQuery();
                    mySqlCommand.Dispose();
                    mySqlCommand = null;
                    
                    return IntLabTestID;
                }
                else  // Lab test is available
                {
                    return IntLabTestID;
                }
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL10 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL10 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL11 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL11 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
                                               
        }
       
        private long GenerateUnique_TestMSTID(long PatientID)
        {  // function used to generate unique lab test id as per glostream standard

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            try
            {
                long MachineId = 0;
                long _testID = 0;
                string strquery = "";

                MachineId = GetPrefixTransactionID(PatientID);           

                strquery = "SELECT labtm_ID FROM Lab_Test_Mst WHERE convert(varchar(18),labtm_ID) Like convert(varchar(18)," + MachineId + " )+ '%'";
                       
                SetMyCommand(strquery);
                object Tmpobj = mySqlCommand.ExecuteScalar();
                mySqlCommand.Dispose();
                mySqlCommand = null;

                if (Tmpobj == null)
                    _testID = 0;
                else
                    _testID = Convert.ToInt64(Tmpobj);
  
                  
                if (_testID == 0)
                {
                    strquery = "select convert(numeric(18,0), convert(varchar(18)," + MachineId + ") + '01')";
                }
                else
                {
                    strquery = "select isnull(max(labtm_ID),0)+1 from Lab_Test_Mst where convert(varchar(18),labtm_ID) Like convert(varchar(18)," + MachineId + " )+ '%'";
                }
         
              
                SetMyCommand(strquery);
                Tmpobj = mySqlCommand.ExecuteScalar();
                mySqlCommand.Dispose();
                mySqlCommand = null;
                
                _testID = Convert.ToInt64(Tmpobj);
            
                return _testID;
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL12 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL12 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL13 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL13 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }                                              
        }      

        private long GenerateUnique_IcdID(long patientCode)
        {
           // gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);

            try
            {
                long MachineId = 0;
                Int64 IcdID = 0;
                string strquery = "";

                // getting the Prefix Transaction ID
                MachineId = GetPrefixTransactionID(_PatientCode);

                strquery = "SELECT nICD9ID FROM icd9 WHERE convert(varchar(18),nICD9ID) Like convert(varchar(18)," + MachineId + " )+ '%'";
                           
                SetMyCommand(strquery);
                object Tmpobj = mySqlCommand.ExecuteScalar();
                mySqlCommand.Dispose();
                mySqlCommand = null;


                if(Tmpobj == null)
                    IcdID=0;
                else
                   IcdID = Convert.ToInt64(Tmpobj);                             

                if (IcdID == 0)
                {
                    strquery = "select convert(numeric(18,0), convert(varchar(18)," + MachineId + ") + '01')";
                }
                else
                {
                    strquery = "select isnull(max(nICD9ID),0)+1 from icd9 where convert(varchar(18),nICD9ID) Like convert(varchar(18)," + MachineId + " )+ '%'";
                }
                       
                SetMyCommand(strquery);
                Tmpobj = mySqlCommand.ExecuteScalar();
                mySqlCommand.Dispose();
                mySqlCommand = null;

                IcdID = Convert.ToInt64(Tmpobj);
             
                return IcdID;
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL14 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL14 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL15 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL15 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }            
        }
             
            
        private long GenerateVisitID(DateTime DtTransDate)
        {    // function used to generate the Visit ID for order.

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();       
            
            try
            {             
                oDBParameters.Add("@nPatientID", _PatientCode, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                oDBParameters.Add("@dtVisitdate", DtTransDate, ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                long lngAppointID = 0;
                oDBParameters.Add("@AppointmentID", lngAppointID, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                long lngTransId = 0;
                lngTransId = GetPrefixTransactionID(_PatientCode);
                oDBParameters.Add("@MachineID", lngTransId, ParameterDirection.Input, System.Data.SqlDbType.BigInt, 18);
                oDBParameters.Add("@VisitID", 0, ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt, 18);
                
                oDBParameters.Add("@flag", 0, ParameterDirection.Output, System.Data.SqlDbType.Int);

                oDB.Connect(false);
                long lngVisitId = 0;                               
                object tmpVisitId;
                object tmpFlag;
                oDB.Execute("gsp_InsertVisits", oDBParameters,out tmpVisitId,out tmpFlag);
                oDB.Disconnect();

                if (tmpVisitId.ToString() == "" || tmpVisitId == null)
                    lngVisitId = 0;
                else
                    lngVisitId = Convert.ToInt64(tmpVisitId);

                return lngVisitId;
                          
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL19 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL19 - Insert Emdeon Data: " + ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL20 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL20 - Insert Emdeon Data: " + ex.ToString());
                return  0;  
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }                                        
            }

        }

        #endregion methods defination

    } // end of class clsInsertEmdeonData


    public class clsGetGloLabData

    {
        // This is class is created by Abhijeet
        // purpose : it is used to access all latest placed order details into emdeon 

        #region enum definations

        public enum RetrievePatientOrdersType
        {   // Used to retrieve order
            // for All retrieve all order for patient 
            // for Duration retrieve (as per specified in XML call) orders for patient.
            All = 0, // Called for refresh button on view gloLab form
            Duration = 1,  // Called for close/save button on gloLab interface form
            Selected=2 //Added by madan on 20100612
        }
        
        #endregion for definations of enum
        
        private RetrievePatientOrdersType _RetrieveType = RetrievePatientOrdersType.Duration;
        private string _DBConnectionString = "";
        private long _PatientId = 0;
        

        long _UserID = 0;
        string _UserName = "";
        //Below code is commeted by madan on 20100520
        //private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        //Added by madan on 20100612
        private Int64 _ModifiyOrderId = 0;

        public Int64 ModifiyOrderId
        {
            get { return _ModifiyOrderId; }
            set { _ModifiyOrderId = value; }
        }
        private string _ModifiyOrderReferenceId = string.Empty;

        public string ModifiyOrderReferenceId
        {
            get { return _ModifiyOrderReferenceId; }
            set { _ModifiyOrderReferenceId = value; }
        }
        //End madan Changes.

        //Added by Amit 28/05/2013 7031
        private Boolean  _OrderNotCPOE = false ;
        public Boolean OrderNotCPOE
        {
            get { return _OrderNotCPOE; }
            set { _OrderNotCPOE = value; }
        }
        //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order
        private Int64 _MySelectedOrderID = 0;
        public Int64 GetMySelectedOrderId
        {
            get { return _MySelectedOrderID; }
            set { _MySelectedOrderID = value; }
        }
        private Boolean _IsSplitOrder = false;
        public Boolean IsSplitOrder
        {
            get { return _IsSplitOrder; }
            set { _IsSplitOrder = value; }
        }
        
        private Int64 _ProviderID = 0;
        public Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }
        //// NOTE : Don't define gany constructor without PatiendID & connection string parameters
        public clsGetGloLabData()
        {
            if (appSettings != null)
            {
                _UserID = Convert.ToInt64(appSettings["UserID"]);
                _UserName = Convert.ToString(appSettings["UserName"]);
            }                       
        }

        public void GetAllLatestOrder(long PatiendId,  string DBConnectionString, RetrievePatientOrdersType RetrieveType)
        {
            _RetrieveType = RetrieveType;
            GetAllLatestOrder(PatiendId,DBConnectionString);
        }
        
        public void GetAllLatestOrder(long PatiendId,string DBConnectionString)
        {
            
            _PatientId = PatiendId;
            _DBConnectionString = DBConnectionString;
            GloLabOrder_Msts oEmdeonOrder_Msts = new GloLabOrder_Msts();
            Dictionary<string,Int64>  dishProvID=new Dictionary<string,Int64 >() ;
            //// By Abhijeet Farkande on Date 20100227
            //// made the object of gloLab patient
            gloPatient.Patient objpatient = new gloPatient.Patient();
            gloPatient.gloPatient objgloPatient = new gloPatient.gloPatient(_DBConnectionString);
            objpatient = objgloPatient.GetPatient(_PatientId);
            objgloPatient.Dispose();
            //// Code By Abhijeet Farkande on Date 20100227

            string strFileName = string.Empty;
            clsEmdeonLabLayer objclsEmdeonLabLayer = new clsEmdeonLabLayer(_PatientId);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);

            DataTable _dtLabsForPatient = null;
            DataSet dsAllOrders = new DataSet();
            DataSet dsOrderTest = null;
            try
            {
                //strFileName = objclsEmdeonLabLayer.ProcessRequest("order_search_by_order_info", "");

                //by Abhijeet on 20100512                
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.LabOrderRequest, "Making request for retrieving external lab orders for a patient", objpatient.PatientID, 0, _ProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                // Assign global connection string variable before calling process request with following parameters 
                // order_search_by_patient_info_saveclose or order_search_by_patient_info_refresh
                clsEmdeonGeneral.sConnectionString = _DBConnectionString;
                if (_RetrieveType == RetrievePatientOrdersType.Duration)
                {
                    strFileName = objclsEmdeonLabLayer.ProcessRequest("order_search_by_patient_info_saveclose", "");
                }
                else if (_RetrieveType == RetrievePatientOrdersType.All)
                {
                    strFileName = objclsEmdeonLabLayer.ProcessRequest("order_search_by_patient_info_refresh", "");
                }
                else if (_RetrieveType == RetrievePatientOrdersType.Selected)
                {
                    objclsEmdeonLabLayer.OrderRefreanceID = ModifiyOrderReferenceId;
                    strFileName = objclsEmdeonLabLayer.ProcessRequest("retrieve_selected_order", "");
                }


                dsAllOrders.ReadXml(strFileName);


                bool _foundExistingLab = false;
                if (dsAllOrders != null && dsAllOrders.Tables.Count > 1)
                {
                    //Read All Database Orders 
                    oDB.Connect(false);
                    if (ModifiyOrderId == 0)
                    {
                        //Read All Database Orders                 
                        dsAllOrders.Tables[1].DefaultView.RowFilter = "patient_id='" + objpatient.DemographicsDetail.PatientCode + "'";

                        ////Read All Database Orders 
                        //oDB.Connect(false);

                        // Added by Abhijeet on date 20100330
                        //oDB.Retrive_Query("select labom_ExternalCode,labom_dgloLabOrderID from  dbo.Lab_Order_MST where  labom_PatientID ='" + _PatientId + "' ", out _dtLabsForPatient);
                        oDB.Retrive_Query("select labom_ExternalCode,labom_dgloLabOrderID from  dbo.Lab_Order_MST where  labom_dgloLabOrderID IS NOT NULL and  labom_PatientID ='" + _PatientId + "' ", out _dtLabsForPatient);
                        // end of changes by Abhijeet
                    }
                    if (dsAllOrders.Tables["OBJECT"] != null && dsAllOrders.Tables["OBJECT"].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsAllOrders.Tables["OBJECT"].DefaultView.Count; i++)
                        {
                            _foundExistingLab = false;

                            //if already present in system don't need to reinsert.
                            //So do not pull order details and insert
                            if (_dtLabsForPatient != null && _dtLabsForPatient.Rows.Count > 0)
                            {
                                for (int j = 0; j < _dtLabsForPatient.Rows.Count; j++)
                                {   // by Abhijeet on 20100330 ,change the condition for order number & ID
                                    if (_dtLabsForPatient.Rows[j]["labom_dgloLabOrderID"] != null && Convert.ToString(_dtLabsForPatient.Rows[j]["labom_dgloLabOrderID"]).Trim() != ""
                                        && _dtLabsForPatient.Rows[j]["labom_ExternalCode"] != null && Convert.ToString(_dtLabsForPatient.Rows[j]["labom_ExternalCode"]).Trim() != ""
                                        && Convert.ToString(_dtLabsForPatient.Rows[j]["labom_dgloLabOrderID"]) == Convert.ToString(dsAllOrders.Tables["OBJECT"].DefaultView[i]["placer_order_number"])
                                        && Convert.ToString(_dtLabsForPatient.Rows[j]["labom_ExternalCode"]) == Convert.ToString(dsAllOrders.Tables["OBJECT"].DefaultView[i]["order"]))
                                    {
                                        _foundExistingLab = true;
                                    }
                                }
                                if (_foundExistingLab)
                                {
                                    // by Abhijeet on date 20100330
                                    // comment the break statement & use continue statment
                                    break;
                                    //continue;
                                    // End of changes by Abhijeet
                                }
                                // Found patient labs in database.                                
                            }
                            else
                            {
                                // Did not found labs in gloEMR , So pull order details and insert
                            }

                            // This is order ID recieved from emdeon
                            string _EmdeonOrderID = dsAllOrders.Tables[1].DefaultView[i]["order"].ToString();

                            //3. Call API to get "Lab test Information" for those filtered and save back to gloEMR
                            strFileName = objclsEmdeonLabLayer.ProcessRequest("search_diag_code", _EmdeonOrderID);
                            dsOrderTest = new DataSet();
                            dsOrderTest.ReadXml(strFileName);
                            if (dsOrderTest != null && dsOrderTest.Tables.Count > 1)
                            {

                                if (dsOrderTest.Tables["OBJECT"] != null && dsOrderTest.Tables["OBJECT"].Rows.Count > 0)
                                {

                                    DataTable objdt =  SelectDistinct("OrderCode", dsOrderTest.Tables["OBJECT"], "order_code");

                                    GloLabOrder_tests objEmdeonOrder_tests = new GloLabOrder_tests();
                                    if (objdt != null)
                                    {
                                        //for (int k = 0; k < dsOrderTest.Tables["OBJECT"].Rows.Count; k++)
                                        for (int k = 0; k < objdt.Rows.Count; k++)
                                        {
                                            GloLabOrder_test objEmdeonOrder_test = new GloLabOrder_test();

                                            DataTable DTOrderTest = null;
                                            DTOrderTest = dsOrderTest.Tables["OBJECT"];
                                            DTOrderTest.DefaultView.RowFilter = "order_code='" + Convert.ToString(objdt.Rows[k]["order_code"]) + "'";
                                            DataTable DTFilterOrderTest = null;
                                            DTFilterOrderTest = DTOrderTest.DefaultView.ToTable();

                                            //string _EmdeonTestCode = Convert.ToString(dsOrderTest.Tables["OBJECT"].Rows[k]["order_code"]);
                                            //string _EmdeonTestName = Convert.ToString(dsOrderTest.Tables["OBJECT"].Rows[k]["orderable_description"]);

                                            string _EmdeonTestCode = Convert.ToString(DTFilterOrderTest.Rows[0]["order_code"]);
                                            string _EmdeonTestName = Convert.ToString(DTFilterOrderTest.Rows[0]["orderable_description"]);


                                            //// By Abhijeet Farkande On Date 20100222
                                            // // changes : access value of ICD code and diagnosis description into variables
                                            // string _EmdeonICDCode = Convert.ToString(dsOrderTest.Tables["OBJECT"].Rows[k]["icd_9_cm_code"]);
                                            // string _EmdeonDiagDesc = Convert.ToString(dsOrderTest.Tables["OBJECT"].Rows[k]["order_diag_description"]);
                                            // // End of changes by Abhijeet Farkande

                                            //Add Recieved Data -- Tests

                                            objEmdeonOrder_test.GloLabTestName = _EmdeonTestName;
                                            objEmdeonOrder_test.GloLabTestCode = _EmdeonTestCode;


                                            //// By Abhijeet Farkande On Date 20100222
                                            //// changes : adding value of ICD code and diagnosis description into test object
                                            //objEmdeonOrder_test.GloLabICD9Code = _EmdeonICDCode;
                                            //objEmdeonOrder_test.GloLabDiagDescription = _EmdeonDiagDesc;
                                            //// End of changes by Abhijeet Farkande

                                            // code for getting collection of Diagnosis
                                            GloLabOrder_test_diagnoses oGloLabTestDiagnoses = new GloLabOrder_test_diagnoses();
                                            for (int j = 0; j < DTFilterOrderTest.Rows.Count; j++)
                                            {
                                                GloLabOrder_test_diagnosis oGloLabTestDiagnosis = new GloLabOrder_test_diagnosis();
                                                //string _EmdeonICDCode = Convert.ToString(DTFilterOrderTest.Rows[j]["icd_9_cm_code"]);
                                                //string _EmdeonDiagDesc = Convert.ToString(DTFilterOrderTest.Rows[j]["order_diag_description"]);

                                                string _EmdeonICDCode = string.Empty;
                                                string _EmdeonDiagDesc = string.Empty;

                                                if ((Convert.ToString(DTFilterOrderTest.Rows[j]["icd_9_cm_code"]).Trim() != ""))
                                                {
                                                    oGloLabTestDiagnosis.nICDRevision = 9;
                                                    _EmdeonICDCode = Convert.ToString(DTFilterOrderTest.Rows[j]["icd_9_cm_code"]);
                                                    _EmdeonDiagDesc = Convert.ToString(DTFilterOrderTest.Rows[j]["order_diag_description"]);
                                                }
                                                else //added for icd10 feature
                                                {
                                                    oGloLabTestDiagnosis.nICDRevision = 10;
                                                    _EmdeonICDCode = Convert.ToString(DTFilterOrderTest.Rows[j]["icd_10_cm_code"]);
                                                    _EmdeonDiagDesc = Convert.ToString(DTFilterOrderTest.Rows[j]["order_diag_description"]);

                                                }
                                                oGloLabTestDiagnosis.GloLabICD9Code = _EmdeonICDCode;
                                                oGloLabTestDiagnosis.GloLabDiagDescription = _EmdeonDiagDesc;
                                                oGloLabTestDiagnoses.Add(oGloLabTestDiagnosis);
                                            }
                                            DTFilterOrderTest.Dispose();
                                            DTFilterOrderTest = null;
                                            objEmdeonOrder_test.GloLabTestsDiagnoses = oGloLabTestDiagnoses;
                                            // end of code for getting collection of Diagnosis

                                            objEmdeonOrder_tests.Add(objEmdeonOrder_test);
                                            //**Add Recieved Data -- Tests                                      

                                        }
                                        objdt.Dispose();
                                        objdt = null;
                                    }
                                    oEmdeonOrder_Msts.Add(Convert.ToDouble(_EmdeonOrderID), objEmdeonOrder_tests);
                                    oEmdeonOrder_Msts[i].PatientAgeYear = objpatient.DemographicsDetail.PatientDOB.Year;/// gloUC_PatientStrip1.PatientAge.Years;
                                    oEmdeonOrder_Msts[i].PatientAgeMonth = objpatient.DemographicsDetail.PatientDOB.Month;/// gloUC_PatientStrip1.PatientAge.Months;
                                    oEmdeonOrder_Msts[i].PatientAgeDays = objpatient.DemographicsDetail.PatientDOB.Day; // gloUC_PatientStrip1.PatientAge.Days;
                                    oEmdeonOrder_Msts[i].GloLabOrderID = Convert.ToDouble(_EmdeonOrderID);
                                    oEmdeonOrder_Msts[i].PatientID = _PatientId;

                                    //11-Sep-14 Aniket: Fixing Incident #00036876: Provider ID getting 0 saved while reloading labs
                                    if (_ProviderID == 0)
                                    {
                                        oEmdeonOrder_Msts[i].PatientProviderID = objpatient.DemographicsDetail.PatientProviderID;
                                    }
                                    else
                                    {
                                        oEmdeonOrder_Msts[i].PatientProviderID = _ProviderID;
                                    }       //gloUC_PatientStrip1.ProviderID; }

                                    if(dsAllOrders.Tables["OBJECT"].Columns.Contains("referringcaregiver"))     //functionality added for CR00000376 ,bugid 82945 if emdeon provider exist then it gets assigned if found in DB
                                        {
                                        if (!string.IsNullOrEmpty(Convert.ToString(dsAllOrders.Tables["OBJECT"].DefaultView[i]["referringcaregiver"])))
                                        {
                                        Int64 TempProvId = 0;
                                        if (!dishProvID.ContainsKey(Convert.ToString(dsAllOrders.Tables["OBJECT"].DefaultView[i]["referringcaregiver"])))
                                        {
                                            TempProvId = checkProviderExist(Convert.ToString(dsAllOrders.Tables["OBJECT"].DefaultView[i]["referringcaregiver"]));
                                            dishProvID.Add(Convert.ToString(dsAllOrders.Tables["OBJECT"].DefaultView[i]["referringcaregiver"]),TempProvId); 
                                        }
                                        else
                                        {
                                            TempProvId = dishProvID[Convert.ToString(dsAllOrders.Tables["OBJECT"].DefaultView[i]["referringcaregiver"])];
                                        }
                                           if(TempProvId!=0)
                                        oEmdeonOrder_Msts[i].PatientProviderID=TempProvId; 
                                       }
                                       }

                                    if (dsAllOrders.Tables["OBJECT"].Columns.Contains("collection_datetime"))
                                    {

                                        if (string.IsNullOrEmpty(Convert.ToString(dsAllOrders.Tables["OBJECT"].DefaultView[i]["collection_datetime"])))
                                        {
                                            oEmdeonOrder_Msts[i].TransactionDate = DateTime.Now;
                                        }
                                        else
                                        {
                                            oEmdeonOrder_Msts[i].TransactionDate = Convert.ToDateTime(dsAllOrders.Tables["OBJECT"].DefaultView[i]["collection_datetime"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        oEmdeonOrder_Msts[i].TransactionDate = DateTime.Now;
                                    }


                                    //if (dsAllOrders.Tables["OBJECT"].DefaultView[i]["order_type"].ToString().ToUpper() == "PSC")
                                    //{
                                    //    oEmdeonOrder_Msts[i].TransactionDate = DateTime.Now;
                                    //}
                                    //else
                                    //{
                                    //    oEmdeonOrder_Msts[i].TransactionDate = Convert.ToDateTime(dsAllOrders.Tables["OBJECT"].DefaultView[i]["collection_datetime"].ToString());
                                    //}          
                                    oEmdeonOrder_Msts[i].GloLabOrderNumber = dsAllOrders.Tables["OBJECT"].DefaultView[i]["placer_order_number"].ToString();                                    
                                    // Added by Abhijeet on date 20100330
                                    // saving the Order status & billing type
                                    oEmdeonOrder_Msts[i].BillingType = dsAllOrders.Tables["OBJECT"].DefaultView[i]["bill_type"].ToString();
                                    oEmdeonOrder_Msts[i].OrderStatus = dsAllOrders.Tables["OBJECT"].DefaultView[i]["order_status"].ToString();
                                    // End of changes by Abhijeet
                                }
                            }
                            if (dsOrderTest != null)
                            {
                                dsOrderTest.Dispose();
                                dsOrderTest = null;
                            }
                        }
                    }                    
                }
                if (dsAllOrders != null)
                {
                    dsAllOrders.Dispose();
                    dsAllOrders = null;
                }
                //by Abhijeet on 20100512 
                 if (oEmdeonOrder_Msts != null)
                     gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RetrivedAllLabOrders, "Successfully retrieved all external lab orders of a patient from lab service", objpatient.PatientID, 0, _ProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                 
                
                SaveEmdeonLabOrders(oEmdeonOrder_Msts);
                if (oEmdeonOrder_Msts != null)
                {
                    oEmdeonOrder_Msts.Dispose();
                    oEmdeonOrder_Msts = null;
                }
                if (dishProvID != null)
                {
                    dishProvID.Clear();
                    dishProvID = null; 
                }

            }
            catch (System.Security.SecurityException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL21 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL21 - Insert Emdeon Data - Exception During Reading XML :" + ex.ToString());                
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL22 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL22 - Insert Emdeon Data :" + ex.ToString());                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL23 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL23 - Insert Emdeon Data :" + ex.ToString());             
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (_dtLabsForPatient != null)
                {
                    _dtLabsForPatient.Dispose();
                    _dtLabsForPatient = null;
                }
                if (objpatient != null)
                {
                    objpatient.Dispose();
                    objpatient = null;
                }
                if (objclsEmdeonLabLayer != null)
                {
                    objclsEmdeonLabLayer = null;
                }
            }
        }
        
        public Int64 checkProviderExist(string gloLabId)  // added for CR00000376 ,bugid 82945 if emdeon provider exist then it gets assigned if found in DB
        {
            Int64 LabProviderID = 0;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParams = null;
            gloDatabaseLayer.DBParameter oParamater = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_DBConnectionString);
                oDB.Connect(false);
                oParams=new gloDatabaseLayer.DBParameters();  
                oParamater = new gloDatabaseLayer.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.ParameterDirection  = ParameterDirection.Input;
                oParamater.ParameterName  = "@sgloLabId";
                oParamater.Value = gloLabId;
                oParams.Add(oParamater);

               string  ProvId = Convert.ToString(oDB.ExecuteScalar ("gsp_UpdateEmdeonProvider", oParams));
               if (ProvId.Trim().Length > 0 && ProvId != "-1")
                   LabProviderID = Convert.ToInt64(ProvId);  
            }
            catch
            {
                LabProviderID = 0;
            }

            finally
            {
                if (oParams != null)
                {
                    oParams.Clear();
                    oParams.Dispose();
                    oParams = null; 
                }
                if (oParamater != null)
                {
                    oParamater.Dispose();
                    oParamater = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
               
            }
           
            return LabProviderID; 
        }

        public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
        {
            try
            {
                DataTable dt = new DataTable(TableName);
                dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

                object LastValue = null;
                foreach (DataRow dr in SourceTable.Select("", FieldName))
                {
                    if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                    {
                        LastValue = dr[FieldName];
                        dt.Rows.Add(new object[] { LastValue });
                    }
                }
                return dt;
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL24 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL24 - Insert Emdeon Data :" + ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL25 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL25 - Insert Emdeon Data :" + ex.ToString());
                return null;
            }                      
        }

        private bool ColumnEqual(object A, object B)
        {

            try
            {
                if (A == DBNull.Value && B == DBNull.Value)
                    return true;
                if (A == DBNull.Value || B == DBNull.Value)
                    return false;
                return (A.Equals(B));

            }
            catch (Exception)
            {
                throw;
            }           
        }

        private void SaveEmdeonLabOrders(GloLabOrder_Msts oEmdeonOrder_Msts)
        {
            try
            {
                if (oEmdeonOrder_Msts != null)
                {
                   
                    //Code Stared By : Abhijeet Farkande on date 20100201
                    // changes : called the object of class clsInsertEmdeonData to save the test order details obtained from Emdeon
                    clsInsertEmdeonData oInsrtEmdeonData = new clsInsertEmdeonData(_DBConnectionString, oEmdeonOrder_Msts, _UserID, _UserName);

                    //Added for Bug #87928: CR00000369 : Lab orders coming back into glo are not matching up to the pending/sent order
                    oInsrtEmdeonData.GetMySelectedOrderId = _MySelectedOrderID;
                    oInsrtEmdeonData.IsSplitOrder = _IsSplitOrder;
                    //Added by madan on 20100612
                    if (ModifiyOrderId >0 && ModifiyOrderReferenceId !="")
                    {
                        oInsrtEmdeonData.ModifiedOrderRefreanceID = ModifiyOrderReferenceId;
                        oInsrtEmdeonData.ModifiedOrderId = ModifiyOrderId;
                    }
                    //end Madan
                    oInsrtEmdeonData.OrderNotCPOE = OrderNotCPOE;
                    
                    oInsrtEmdeonData.SaveOrdTestDtl();
                    oInsrtEmdeonData = null;
                    // End of changes By Abhijeet Farkande
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL26 - " + ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("GL26 - Insert Emdeon Data :" + ex.ToString());              
            }                                  
        }
        
    }//End of class clsGetGloLabData

   
   

} // end of namespace

