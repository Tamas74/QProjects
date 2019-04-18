using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using gloRxPatientSaving;
using System.Data; 

namespace gloPatientSavingMessage
{
    public class clsProcessLayer
    {
        public delegate void OpenPrescription(ArrayList arrdrug, Int64 ProviderId, Int64 m_Visitid = 0, Int64 m_Patientid = 0, Boolean ShowCarryForwardMessage = true);
        public event OpenPrescription EntOpenPrescription;

        public UC_RxSaving ShowPatientSavingMessageForm(Int64 _PatientID)
        {
            PatSavGeneral.objDataContext = new PatSavDataClassDataContext(PatSavGeneral.ConnectionString);
            //IQueryable<PatSav_Mst> objPatSav_Mst = (from result in PatSavGeneral.objDataContext.PatSav_Msts where result.nPatientID == _PatientID select result);
            IQueryable<PatSav_Mst> objPatSav_Mst = (from result in PatSavGeneral.objDataContext.PatSav_Msts 
                                                    join Opp in PatSavGeneral.objDataContext.PatSavOpportunity_Msts
                                                    on result.PSID equals Opp.PSID 
                                                    where result.nPatientID == _PatientID 
                                                    && Opp.DispositionFile == null
                                                    select result).Distinct();
            if (objPatSav_Mst != null && objPatSav_Mst.Count() != 0)
            {
                objPatSav_Mst = (from r in objPatSav_Mst
                                 join Que in PatSavGeneral.objDataContext.PatSav_Queues
                                 on r.nPatSavQID equals Que.nPatSavQID
                                 orderby Que.dtSavedDate descending
                                 select r);
                string _PatientCode = getPatientCode(_PatientID);
                UC_RxSaving objWindow = new UC_RxSaving(objPatSav_Mst, _PatientCode);

                objWindow.EntOpensavePrescription = ShowPrescription;

                return objWindow;
            }
            return null;
            
        }

        public void ShowPrescription(ArrayList arrdrug, Int64 ProviderId, Int64 m_Visitid = 0, Int64 m_Patientid = 0, Boolean ShowCarryForwardMessage = true)
        {
            try
            {
                EntOpenPrescription(arrdrug, ProviderId, m_Visitid, m_Patientid, ShowCarryForwardMessage);
            }
            catch //(Exception ex)
            {
                return;
            }
        }

        public bool ISPatientSavingMessageExist(Int64 _PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();

            //string strQuery = "";
            bool _response = false;
            try
            {
                if (_PatientID > 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(PatSavGeneral.ConnectionString);
                    odbParams.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                    //strQuery = "select COUNT(*) from PatSavOpportunity_Mst where PSID in (SELECT psid FROM PatSav_Mst where nPatientID ="+ _PatientID +") and DispositionFile is NULL";
                    oDB.Connect(false);

                    //02-Dec-14 Aniket: Patient switching optimization
                    object _result = oDB.ExecuteScalar("gsp_ISPatientSavingMessageExist", odbParams);
                    //object _result = oDB.ExecuteScalar_Query(strQuery);
                    oDB.Disconnect();
                    if (_result != null)
                    {
                        if (Convert.ToInt64(_result) > 0)
                            _response = true;
                    }
                }
            }
            catch //(Exception ex)
            {
                _response = false;
            }

            finally 
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                  
                }
                oDB = null;
                odbParams.Clear();
                odbParams.Dispose();
                odbParams = null;
             }
            return _response;
        }

        public string getCodeDesc(string _CodeType,string _Code)
        {
            string _Codedesc = string.Empty;

            try
            {
                _Codedesc = (from result in PatSavGeneral.objDataContext.PatSav_Codes where result.sCodeType == _CodeType && result.sCodeValue == _Code select result.sCodeDesc).FirstOrDefault() ;
                return _Codedesc;
            }
            catch //(Exception ex)
            {
                return "";
            }
        }

        public string getPatientCode(Int64 _PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string strQuery = "";
            string  _responseCode = string.Empty ;
            try
            {
                if (_PatientID > 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(PatSavGeneral.ConnectionString);
                    strQuery = "select sPatientCode from Patient where nPatientID =" + _PatientID;
                    oDB.Connect(false);
                    object _result = oDB.ExecuteScalar_Query(strQuery);
                    oDB.Disconnect();
                    if (_result != null)
                    {
                        _responseCode = _result.ToString();
                    }
                }
            }
            catch //(Exception ex)
            {
                _responseCode = string.Empty;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
                oDB = null;
              
            }
            return _responseCode;
        }
    }
}
