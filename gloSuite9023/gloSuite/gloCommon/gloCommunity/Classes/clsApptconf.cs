using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Windows.Forms;  
namespace gloCommunity.Classes
{
    class clsApptconf
    {
        DataTable getdata = null;
        public DataTable GetResource()
        {
            getdata = null;

            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string strQuery = " SELECT  'False' as [Select],ISNULL(sCode,'') AS Code " +
                             ", ISNULL(sDescription,'') AS [Resource Name] " +
                             ", ISNULL(nResourceTypeID,0) AS nResourceTypeID " +
                             ", ISNULL(bitIsBlocked,0) AS bitIsBlocked " +
                             ", ISNULL(sUserName,'') AS [User Name] " +
                             ",  '-1' as nAppointmentTypeFlag  ,''  as [Follow Up Name],'' as Duration,'-1' as Criteria,'' as sCriteria " + //added
                             ",'' as [Appointment Status],'' as [Record Type]"+
                             ",''  as [Problem Type],'' as [Color Codes],'-1' as nAppProcType"+
                             ",'' as Department,'-1' as nLocationID,'' as Location " +
                             ",'' as [Address Line1],'' as [Address Line2],'' as City,'' as State,'' as Zip,'' as County, '' as Country,'-1' as bisDefault " +
                             ",'' as [Appointment Type],'' as [Appointment Block Type],'Resource' as AppointmentCategory " +
                             " , '' as AppointmentTemplates,'' as ParentAppointmentTemplate,'' as StartTime,''  as EndTime,'' as ColorCode,'' as Description ,'' as ColorCode1 " +
                             " FROM  " +
                             " AB_Resource_MST " +
                             " WHERE  " +
                             " bitIsBlocked = '" + false + "' AND nClinicID = " + clsGeneral.gClinicID   + " ";


                getdata = oDB.GetDataTable_Query(strQuery);
                return getdata;

            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Resource For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
         
        }




        public DataTable GetSchemaForTemplate()
        {
            getdata = null;

            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string strQuery = " SELECT  top 0 'False' as [Select],' ' AS Code " +
                             ", ' ' AS [Resource Name] " +
                             ", '-1' AS nResourceTypeID " +
                             ",0 AS bitIsBlocked " +
                             ", ' ' AS [User Name] " +
                             ",  '-1' as nAppointmentTypeFlag  ,' '  as [Follow Up Name],' ' as Duration,'-1' as Criteria,' ' as sCriteria " + //added
                             ",' ' as [Appointment Status],' ' as [Record Type]" +
                             ",' '  as [Problem Type],' ' as [Color Codes],'-1' as nAppProcType" +
                             ",' ' as Department,'-1' as nLocationID,' ' as Location " +
                             ",' ' as [Address Line1],' ' as [Address Line2],' ' as City,' ' as State,' ' as Zip,' ' as County, ' ' as Country,'-1' as bisDefault " +
                             ",' ' as [Appointment Type],' ' as [Appointment Block Type],'Template' as AppointmentCategory " +
                             " , ' ' as AppointmentTemplates,' ' as ParentAppointmentTemplate,' ' as StartTime,' '  as EndTime,' ' as ColorCode,' ' as Description,'' as ColorCode1 " +
                             " FROM  " +
                             " AB_Resource_MST " +
                             " WHERE  " +
                             " bitIsBlocked = '" + false + "' AND nClinicID = " + clsGeneral.gClinicID + " ";


                getdata = oDB.GetDataTable_Query(strQuery);
                return getdata;

            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Schema For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
        
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }




        public DataTable GetFollowUp()
        {
            getdata = null;
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string strQuery = " SELECT 'False' as [Select], sFollowUpName as [Follow Up Name], convert(nchar,nDuration) AS Duration, nCriteria as Criteria," +
                               " case nCriteria WHEN  0  then 'Day' "+
				               " when  1 then 'Week' "+
				               " when  2 then 'Month' "+
                               " end AS sCriteria "+
                                " ,'-1' as nAppointmentTypeFlag ,'' as [Appointment Status],'' as [Record Type]" +                //added
                             ",''  as [Problem Type],'' as [Color Codes],'-1' as nAppProcType" +
                             ",'' as Department,'-1' as nLocationID,'' as Location " +
                             ",'' as [Address Line1],'' as [Address Line2],'' as City,'' as State,'' as Zip,'' as County,'' as Country,'-1' as bisDefault " +
                             ",'' as [Appointment Type],'' as [Appointment Block Type] " +
                             ", '' AS Code " +
                             ", '' AS [Resource Name] " +
                             ", '-1' AS nResourceTypeID " +
                             ", 0 AS bitIsBlocked " +
                             ", '' AS [User Name] ,'FollowUp' as AppointmentCategory " +
                             " , '' as AppointmentTemplates,'' as ParentAppointmentTemplate,'' as StartTime,''  as EndTime,'' as ColorCode,'' as Description,'' as ColorCode1 " +
                     
                             " FROM AB_FollowUp_MST";
            getdata = oDB.GetDataTable_Query(strQuery);
            return getdata;

            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Followup For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }   

        }


        public DataTable GetApptStatus()
        {
             getdata = null;

            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string strQuery = "SELECT  'False' as [Select], sAppointmentStatus as [Appointment Status],case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS [Record Type] "+
                               ", '-1' as nAppointmentTypeFlag ,''  as [Follow Up Name],'' as Duration,'-1' as Criteria,'' as sCriteria " +             //added
                             ",''  as [Problem Type],'' as [Color Codes],'-1' as nAppProcType" +
                             ",'' as Department,'-1' as nLocationID,'' as Location " +
                             ",'' as [Address Line1],'' as [Address Line2],'' as City,'' as State,'' as Zip,'' as County,'' as Country,'-1' as bisDefault " +
                             ",'' as [Appointment Type],'' as [Appointment Block Type] " +
                             ", '' AS Code " +
                             ", '' AS [Resource Name] " +
                             ", '-1' AS nResourceTypeID " +
                             ", 0 AS bitIsBlocked " +
                             ", '' AS [User Name],'AppointmentStatus' as AppointmentCategory " +
                             " , '' as AppointmentTemplates,'' as ParentAppointmentTemplate,'' as StartTime,''  as EndTime,'' as ColorCode,'' as Description ,'' as ColorCode1" +
                     
                    " FROM AB_AppointmentStatus WHERE bIsBlocked =0 And bIsSystem<> 1  AND nClinicID = " + clsGeneral.gClinicID + " ";
                getdata = oDB.GetDataTable_Query(strQuery);
                return getdata;
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Appointment Status For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }   
        
        }


        public DataTable Get_ResourceTypes()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                getdata = null;



                String strQuery = "SELECT  'False' as [Select], sResourceType as Description, nResourceType FROM AB_ResourceType_MST WHERE bitIsBlocked <> 1 AND nClinicID = " + clsGeneral.gClinicID + " ";
                getdata = oDB.GetDataTable_Query(strQuery);
                return getdata;

                //dgResource.DataSource = _dv;
                //dgResource.Columns[0].HeaderText = "ResourceTypeID";
                //dgResource.Columns[1].HeaderText = "Resource Type";
                //dgResource.Columns[2].HeaderText = "nResourceType";

                //dgResource.Columns[0].Visible = false;
                //dgResource.Columns[1].Visible = true;
                //dgResource.Columns[2].Visible = false;

             

            }
           
            catch (Exception ex)
            {

                //clsGeneral.UpdateLog("Error  while Getting ResourceTypes For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;   
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }


        public DataTable GetProblem()
        {
               DataBaseLayer oDB = new DataBaseLayer();
             try
             {
                 getdata = null;

                 string strQuery = "SELECT 'False' as [Select], sAppointmentType as [Problem Type],convert(nchar,nDuration) as Duration , sColorCode as [Color Codes],nAppProcType,nAppointmentTypeFlag " +
                        ",''  as [Follow Up Name],'' as Duration,'-1' as Criteria,'' as sCriteria " + 
                        ",'' as Department,'-1' as nLocationID,'' as Location " +
                             ",'' as [Address Line1],'' as [Address Line2],'' as City,'' as State,'' as Zip,'' as County, '' as Country,'-1' as bisDefault " +
                             ",'' as [Appointment Type],'' as [Appointment Block Type] " +
                             ", '' AS Code " +
                             ", '' AS [Resource Name] " +
                             ", '-1' AS nResourceTypeID " +
                             ", 0 AS bitIsBlocked " +
                             ", '' AS [User Name],'Problem' as AppointmentCategory " +
                             " , '' as AppointmentTemplates,'' as ParentAppointmentTemplate,'' as StartTime,''  as EndTime,'' as ColorCode,'' as Description,'' as ColorCode1 " +
                     
                     " FROM AB_AppointmentType WHERE (bIsBlocked = 0 AND nAppProcType = 2) AND nClinicID = " + clsGeneral.gClinicID + "";

          getdata = oDB.GetDataTable_Query(strQuery);
          return getdata;
             }
             catch (Exception ex)
             {
                 //clsGeneral.UpdateLog("Error  while Getting Problem For Appointment Configuration : " + ex.Message.ToString());
                 gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                 return null;
             }
             finally
             {
                 oDB.Dispose();
                 oDB = null;
             } 
        }


        public DataTable GetTemplate()
        {
             DataBaseLayer oDB = new DataBaseLayer();
             try
             {
                 getdata = null;


                 string strQuery = "SELECT  'False' as [Select],nAppointmentTemplateID, sAppointmentTemplates FROM  AB_AppointmentTemplate_MST WHERE nClinicID = " + clsGeneral.gClinicID + " ORDER BY sAppointmentTemplates ";

                 getdata = oDB.GetDataTable_Query(strQuery);
                 return getdata;
             }
             catch (Exception ex)
             {
                 //clsGeneral.UpdateLog("Error  while Getting Template For Appointment Configuration : " + ex.Message.ToString());  
                 gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                 return null;
             }
             finally
             {
                 oDB.Dispose();
                 oDB = null;
             } 
        }


        public AppointmentTemplate GetTemplate(Int64 TemplateID)
        {//new 
            AppointmentTemplate oTemplate = new AppointmentTemplate();
            DataTable dtTemplateMST = new DataTable();
            DataTable dtTemplateDTL = new DataTable();
            gloDatabaseLayer.DBLayer oDB = null;// new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
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
                //clsGeneral.UpdateLog("Error  while Getting Template For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oTemplate;
        }
       




        public DataTable GetDepartment()
        {

            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                getdata = null;

                string strQuery = " SELECT 'False' as [Select], AB_Department.sDepartment AS Department , AB_Department.nLocationID AS nLocationID , AB_Location.sLocation AS Location   " +

                       " , '-1' as nAppointmentTypeFlag  ,''  as [Follow Up Name],'' as Duration,'-1' as Criteria,'' as sCriteria " +             //added
                             ",''  as [Problem Type],'' as [Color Codes],'-1' as nAppProcType" +
                              ",'' as [Address Line1],'' as [Address Line2],'' as City,'' as State,'' as Zip,'' as County,'' as Country,'-1' as bisDefault " +
                             ",'' as [Appointment Type],'' as [Appointment Block Type] " +
                             ", '' AS Code " +
                             ", '' AS [Resource Name] " +
                             ", '-1' AS nResourceTypeID " +
                             ", 0 AS bitIsBlocked " +
                             ", '' AS [User Name] " +
                             ",'' as [Appointment Status],'' as [Record Type],'Department' as AppointmentCategory " +
                             " , '' as AppointmentTemplates,'' as ParentAppointmentTemplate,'' as StartTime,''  as EndTime,'' as ColorCode,'' as Description,'' as ColorCode1 " +
                     
                             " FROM AB_Department INNER JOIN AB_Location ON AB_Department.nLocationID = AB_Location.nLocationID WHERE (AB_Department.bIsBlocked = 0) AND AB_Department.nClinicID = " + clsGeneral.gClinicID +"";

                getdata = oDB.GetDataTable_Query(strQuery);
                return getdata;

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting ResourceTypes For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            } 
        
        
        }
        
        
        public DataTable GetLocation()
        {
             DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                getdata = null;


                string strQuery = "SELECT 'False' as [Select], sLocation as Location,sAddressLine1 as [Address Line1],sAddressLine2 as [Address Line2] ,sCity as City,sState as State ,sZIP as Zip,sCounty as County,sCountry as Country , bIsDefault "+
                      " ,'-1' as nAppointmentTypeFlag , '' as [Appointment Type],'' as [Appointment Block Type] " + //added
                             ", '' AS Code " +
                             ", '' AS [Resource Name] " +
                             ", '-1' AS nResourceTypeID " +
                             ", 0 AS bitIsBlocked " +
                             ", '' AS [User Name] " +
                            ",'' as [Appointment Status],'' as [Record Type]" +
                                ",''  as [Follow Up Name],'' as Duration,'-1' as Criteria,'' as sCriteria " +           
                             ",''  as [Problem Type],'' as [Color Codes],'-1' as nAppProcType" +
                             ",'' as Department,'-1' as nLocationID ,'Location' as AppointmentCategory " +
                           " , '' as AppointmentTemplates,'' as ParentAppointmentTemplate,'' as StartTime,''  as EndTime,'' as ColorCode,'' as Description,'' as ColorCode1 " +
                     
                             " FROM AB_Location WHERE bIsBlocked = 0 AND nClinicID = " + clsGeneral.gClinicID + "";
                getdata = oDB.GetDataTable_Query(strQuery);
                return getdata;
            }

            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Location For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            } 
        
        }





        public DataTable GetAppointmenttype()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                getdata = null;
                string strQuery = "SELECT 'False' as [Select], sAppointmentType as [Appointment Type],convert(nchar,nDuration) as Duration  , sColorCode as [Color Codes],nAppProcType,nAppointmentTypeFlag  " +
                       ",''  as [Follow Up Name],'-1' as Criteria,'' as sCriteria ,''  as [Problem Type] " +   //added
                          ",'' as Department,'-1' as nLocationID,'' as Location " +
                             ",'' as [Address Line1],'' as [Address Line2],'' as City,'' as State,'' as Zip,'' as County,'' as Country,'-1' as bisDefault " +
                             ",'' as [Appointment Type],'' as [Appointment Block Type] " +
                             ", '' AS Code " +
                             ", '' AS [Resource Name] " +
                             ", '-1' AS nResourceTypeID " +
                             ", 0 AS bitIsBlocked " +
                             ", '' AS [User Name] " +
                           ",'' as [Appointment Status],'' as [Record Type],'AppointmentType' as AppointmentCategory " +
                           " , '' as AppointmentTemplates,'' as ParentAppointmentTemplate,'' as StartTime,''  as EndTime,'' as ColorCode,'' as Description,sColorCode as ColorCode1" +
                     
                    " FROM AB_AppointmentType WHERE (bIsBlocked = 0 AND nAppProcType = 1) AND nClinicID = " + clsGeneral.gClinicID + "";
                getdata = oDB.GetDataTable_Query(strQuery);
                return getdata;

            }

            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Appointment Type  For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }


        public DataTable GetAppointmentBlk()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                getdata = null;


                string strQuery = "SELECT 'False' as [Select], sAppointmentBlockType as [Appointment Block Type] "+
                    ", '-1' as nAppointmentTypeFlag ,    ''  as [Follow Up Name],'' as Duration,'-1' as Criteria,'' as sCriteria " +             //added
                             ",''  as [Problem Type],'' as [Color Codes],'-1' as nAppProcType" +
                             ",'' as Department,'-1' as nLocationID,'' as Location " +
                             ",'' as [Address Line1],'' as [Address Line2],'' as City,'' as State,'' as Zip,'' as County,'' as Country,'-1' as bisDefault " +
                              ", '' AS Code " +
                             ", '' AS [Resource Name] " +
                             ", '-1' AS nResourceTypeID " +
                             ", 0 AS bitIsBlocked " +
                             ", '' AS [User Name] " +
                       ",'' as [Appointment Type],'' as [Appointment Status],'' as [Record Type],'AppointmentBlock' as AppointmentCategory " +
                       " , '' as AppointmentTemplates,'' as ParentAppointmentTemplate,'' as StartTime,''  as EndTime,'' as ColorCode,'' as Description ,'' as ColorCode1" +
                     
                       " FROM AB_AppointmentBlockType WHERE bIsBlocked ='" + false + "' AND nClinicID = " + clsGeneral.gClinicID + "";

                getdata = oDB.GetDataTable_Query(strQuery);
                return getdata;
             }

            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Appointment Block For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }




        public bool CompareXMlData(DataTable local, DataTable server, string FilePath)
        {
            bool changedata = false;
            //  string billingcategory = "";
            DialogResult Result;
            //  bool dataDownloaded = false; 
            foreach (DataRow dr in local.Rows)
            {

                DataRow[] drr = null;
                switch (dr["AppointmentCategory"].ToString())
                {
                    case "Resource":

                        drr = server.Select("[Resource Name]='" + dr["Resource Name"].ToString().Replace("'", "''") + "'");
                        if (drr.Length > 0)
                        {
                            Result = MessageBox.Show("For Resource'" + dr["Resource Name"].ToString() + "' '" + dr["Resource Name"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (Result == DialogResult.Yes)
                            {
                                server.Rows.Remove(drr[0]);
                                server.ImportRow(dr);
                                changedata = true; 
                            }

                        }
                        else
                        {
                            server.ImportRow(dr);
                            changedata = true; 
                        }
                        break;

                    case "FollowUp":

                        drr = server.Select("[Follow Up Name]='" + dr["Follow Up Name"].ToString().Replace("'", "''") + "'");
                        if (drr.Length > 0)
                        {
                            Result = MessageBox.Show("For Follow Up '" + dr["Follow Up Name"].ToString() + "' '" + dr["Follow Up Name"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (Result == DialogResult.Yes)
                            {
                                server.Rows.Remove(drr[0]);
                                server.ImportRow(dr);
                                changedata = true; 
                            }

                        }
                        else
                        {
                            server.ImportRow(dr);
                            changedata = true; 
                        }


                        break;

                    case "AppointmentStatus":

                        drr = server.Select("[Appointment Status]='" + dr["Appointment Status"].ToString().Replace("'", "''") + "'");
                        if (drr.Length > 0)
                        {
                            Result = MessageBox.Show("For Appointment Status '" + dr["Appointment Status"].ToString() + "'  Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (Result == DialogResult.Yes)
                            {
                                server.Rows.Remove(drr[0]);
                                server.ImportRow(dr);
                                changedata = true; 
                            }

                        }
                        else
                        {
                            server.ImportRow(dr);
                            changedata = true; 
                        }

                        break;
                    case "Problem":


                        drr = server.Select("[Problem Type]='" + dr["Problem Type"].ToString().Replace("'", "''") + "'");
                        if (drr.Length > 0)
                        {
                            Result = MessageBox.Show("For Problem Type '" + dr["Problem Type"].ToString() + "'Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (Result == DialogResult.Yes)
                            {
                                server.Rows.Remove(drr[0]);
                                server.ImportRow(dr);
                                changedata = true; 
                            }

                        }
                        else
                        {
                            server.ImportRow(dr);
                            changedata = true; 
                        }
                        break;


                    case "Department":

                        drr = server.Select("Department='" + dr["Department"].ToString().Replace("'", "''") + "' or Location= '" + dr["Location"].ToString().Replace("'", "''") + "'");
                        if (drr.Length > 0)
                        {
                            Result = MessageBox.Show("For Department '" + dr["Department"].ToString() + "' ,'" + dr["Location"].ToString() + "'  Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (Result == DialogResult.Yes)
                            {
                                server.Rows.Remove(drr[0]);
                                server.ImportRow(dr);
                                changedata = true; 
                            }

                        }
                        else
                        {
                            server.ImportRow(dr);
                            changedata = true; 
                        }
                        break;
                    case "Location":
                        //
                        drr = server.Select("Location='" + dr["Location"].ToString().Replace("'", "''") + "'");
                        if (drr.Length > 0)
                        {
                            Result = MessageBox.Show("For Location '" + dr["Location"].ToString() + "'  Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (Result == DialogResult.Yes)
                            {
                                server.Rows.Remove(drr[0]);
                                server.ImportRow(dr);
                                changedata = true; 
                            }

                        }
                        else
                        {
                            server.ImportRow(dr);
                            changedata = true; 
                        }

                        break;
                    case "AppointmentType":

                        drr = server.Select("[Appointment Type]= '" + dr["Appointment Type"].ToString().Replace("'", "''") + "'");
                        if (drr.Length > 0)
                        {
                            Result = MessageBox.Show("For Appointment Type '" + dr["Appointment Type"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (Result == DialogResult.Yes)
                            {
                                server.Rows.Remove(drr[0]);
                                server.ImportRow(dr);
                                changedata = true; 
                            }

                        }
                        else
                        {
                            server.ImportRow(dr);
                            changedata = true; 
                        }
                        break;

                    case "AppointmentBlock":


                        drr = server.Select("[Appointment Block Type]= '" + dr["Appointment Block Type"].ToString().Replace("'", "''") + "'");
                        if (drr.Length > 0)
                        {
                            Result = MessageBox.Show("For Appointment Block Type '" + dr["Appointment Block Type"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (Result == DialogResult.Yes)
                            {
                                server.Rows.Remove(drr[0]);
                                server.ImportRow(dr);
                                changedata = true; 
                            }

                        }
                        else
                        {
                            server.ImportRow(dr);
                            changedata = true; 
                        }
                        break;

                    case "Template":

                        if (dr["ParentAppointmentTemplate"].ToString().Trim().Length > 0)
                        {
                            drr = server.Select("ParentAppointmentTemplate = '" + dr["ParentAppointmentTemplate"].ToString().Replace("'", "''") + "' and  StartTime= '" + dr["StartTime"].ToString() + "' and  EndTime= '" + dr["EndTime"].ToString() + "' and  ColorCode= '" + dr["ColorCode"].ToString() + "'  and  Description= '" + dr["Description"].ToString().Replace("'", "''") + "'");
                            if (drr.Length > 0)
                            {
                                Result = MessageBox.Show("For Template  '" + dr["ParentAppointmentTemplate"].ToString() + "   " + dr["StartTime"].ToString() + "  " + dr["EndTime"].ToString() + "'   Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                if (Result == DialogResult.Yes)
                                {
                                    server.Rows.Remove(drr[0]);
                                    server.ImportRow(dr);
                                    changedata = true;
                                }

                            }
                            else
                            {
                                server.ImportRow(dr);
                                changedata = true;
                            }
                        }

                        else
                        {




                            drr = server.Select("AppointmentTemplates = '" + dr["AppointmentTemplates"].ToString().Replace("'", "''") + "'");
                            if (drr.Length > 0)
                            {
                                Result = MessageBox.Show("For Template  '" + dr["AppointmentTemplates"].ToString() +"'    Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                if (Result == DialogResult.Yes)
                                {
                                    server.Rows.Remove(drr[0]);
                                    server.ImportRow(dr);
                                    changedata = true;
                                }

                            }
                            else
                            {
                                server.ImportRow(dr);
                                changedata = true;
                            }
                  






                        }
                        break;

                  }

                }

            server.WriteXml(FilePath);
            return  changedata ; 
        }


        public string InsertProblem(string AppointmentType,int nDuration,string sColorCode,int nAppProcType,int nAppointmentTypeFlag)
        {
            string msg = "";

            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAppointmentType";
                oParamater.Value = AppointmentType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nDuration";
                oParamater.Value = nDuration;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sColorCode";
                oParamater.Value = sColorCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nAppProcType";
                oParamater.Value = nAppProcType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nAppointmentTypeFlag";
                oParamater.Value = nAppointmentTypeFlag;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "glocomm_ins_appconf_AB_AppointmentType";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;

            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting Problem For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }
        }



        public string InsertDepartment(string sDepartment, Int64 nLocationID)
        {
            string msg = "";
            
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDepartment";
                oParamater.Value = sDepartment;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
               
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nLocationID";
                oParamater.Value = nLocationID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
              
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "glocomm_ins_appconf_AB_Department";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;
            
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting Department  For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }
        
        
        
        }


        public string InsertLocation(string Location,string Addressline1,string Addressline2,string City,string State,string Zip,string Country,string County)
        {
        

     string msg = "";
            
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sLocation";
                oParamater.Value = Location;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
         
                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAddressLine1";
                oParamater.Value = Addressline1;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
         
                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAddressLine2";
                oParamater.Value = Addressline2 ;
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
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCity";
                oParamater.Value =City;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                   oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sState";
                oParamater.Value = State;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                     oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sZIP";
                oParamater.Value = Zip;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                     oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCounty";
                oParamater.Value = County ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                
                     oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCountry";
                oParamater.Value = Country;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit ;
                oParamater.Direction = ParameterDirection.Input ;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value =0 ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit ;
                oParamater.Direction = ParameterDirection.Input ;
                oParamater.Name = "@bIsDefault";
                oParamater.Value =0 ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

               string strQuery = "glocomm_ins_appconf_AB_Location";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting Location  For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }








        }

        public string InsertAppointmentType(string AppointmentType,string Duration,string sColorCode,int nAppProcType,int nAppointmentTypeFlag)
        {
            string msg = "";
            
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            string[] splstrduration = Duration.Split(':');
            int Min = 0;
            for (int len = 0; len < splstrduration.Length; len++)
            {
          switch (len)
          {
              case 0:
                  Min =Convert.ToInt32 (splstrduration[len]) * 60;
                  break;

              case 1:
                  Min += Convert.ToInt32(splstrduration[len]) ;
                  break;
              default:
                  break;
          }
          }
          
            try
            {

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAppointmentType";
                oParamater.Value = AppointmentType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nDuration";
                oParamater.Value = Min;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sColorCode";
                oParamater.Value = sColorCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nAppProcType";
                oParamater.Value =nAppProcType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value =clsGeneral.gClinicID ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input ;
                oParamater.Name = "@nAppointmentTypeFlag";
                oParamater.Value =nAppointmentTypeFlag ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                  oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit ;
                oParamater.Direction = ParameterDirection.Input ;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value =0 ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "glocomm_ins_appconf_AB_AppointmentType";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;
            
            }
            catch(Exception ex)
            {

                //clsGeneral.UpdateLog("Error  while Inserting AppointmentType For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }
            
            
        }


        public string InsertAppointmentBlock(string AppointmentBlockType)
        {
         string msg = "";
            
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAppointmentBlockType";
                oParamater.Value = AppointmentBlockType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
             
               string strQuery = "glocomm_ins_appconf_AB_AppointmentBlockType";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting AppointmentBlock For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }
            
            

            
          
        }


        public string InsertAppointmentstatus(string AppointmentStatus)
        {
            string msg = "";
            
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAppointmentStatus";
                oParamater.Value = AppointmentStatus;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
           
                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
           
                   
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsSystem";
                oParamater.Value =0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
           
           
               string strQuery = "glocomm_ins_appconfAppstat";
                msg = oDB.GetDataValue(strQuery).ToString();

               return msg;
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting Appointment Status For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }

        
        }











        public string  InsertResource(string Code,string Description,Int64 nRestypeID,bool bitisBlocked,string UserName,Int64 ClinicID)
        {

            string msg = "";
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCode";
                oParamater.Value = Code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDescription";
                oParamater.Value = Description;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nResourceTypeID";
                oParamater.Value = nRestypeID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bitIsBlocked";
                oParamater.Value = bitisBlocked;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sUserName";
                oParamater.Value = UserName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "glocomm_ins_appconfresource_mst";
                msg = oDB.GetDataValue(strQuery).ToString();

               return msg;
            }

            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting Resource For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return "";
            }


           
        
        }







        public string InsertFollowup(string FollowUpName,Int64  Duration,string sCriteria)
        {

            int Criteria = -1;

            string msg = "";
            
            switch (sCriteria.ToLower().Trim())
            {
            
                case "day":Criteria =  0 ;
                    break;
                       case "week":Criteria =  1;
                    break;
                       case "month":Criteria =  2;
                    break;
            }
         

    
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sFollowUpName";
                oParamater.Value = FollowUpName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
            
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nDuration";
                oParamater.Value = Duration;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
            
                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nCriteria";
                oParamater.Value = Criteria;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "glocomm_ins_appconf_Followup_mst";
                msg = oDB.GetDataValue(strQuery).ToString();

                return msg;
            
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting Followup For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return msg;
            }
        
        
        }


        public DataTable ProcessTemplates(TreeNode Treenode ,DataTable dt)
        {
            gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate();
            AppointmentTemplate oAppointmentTemplate = null;
         //   Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;

            Int64 _nTemplateID = Convert.ToInt64(GetTagElement(Treenode.Tag.ToString(), '~', 2));

        //    bool flag = false; 
            oAppointmentTemplate = ogloAppointmentTemplate.GetTemplate(_nTemplateID);
            if (oAppointmentTemplate.TemplateDetails.Count > 0)
            {
                DataRow drtemprow = dt.NewRow();
               
                drtemprow["AppointmentTemplates"]=Treenode.Text.ToString().Trim();
                drtemprow["AppointmentCategory"] = "Template";
                drtemprow["Select"] = "False";
                drtemprow["ParentAppointmentTemplate"] = "";
              
                drtemprow["Resource Name"] = "";

                drtemprow["nResourceTypeID"] = "-1";
                drtemprow["bitIsBlocked"] = false;
                drtemprow["User Name"] = "";
                drtemprow["nAppointmentTypeFlag"] = "-1";
                drtemprow["User Name"] = "";
                drtemprow["Follow Up Name"] = "";
                drtemprow["Duration"] = "";
                drtemprow["Criteria"] = "-1";
                drtemprow["sCriteria"] = "";
                drtemprow["Appointment Status"] = "";
                drtemprow["Record Type"] = "";
                drtemprow["Problem Type"] = "";
                drtemprow["Color Codes"] = "";
                drtemprow["nAppProcType"] = "-1";
                drtemprow["Department"] = " ";
                drtemprow["nLocationID"] = "-1";
                drtemprow["Location"] = "";
                drtemprow["Address Line1"] = "";
                drtemprow["Address Line2"] = "";

                drtemprow["City"] = "";
                drtemprow["State"] = " ";
                drtemprow["Zip"] = "-1";
                drtemprow["County"] = "";
                drtemprow["Country"] = "";
                drtemprow["bisDefault"] = "-1";

                drtemprow["Appointment Type"] = "-1";
                drtemprow["Appointment Block Type"] = "-1";
                drtemprow["ColorCode1"] = "";
                dt.Rows.Add(drtemprow);   
             
            }
            try
            {

                for (int i = 0; i < oAppointmentTemplate.TemplateDetails.Count; i++)
                {
                    DataRow drtemprow = dt.NewRow(); 
                    // oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                    drtemprow["Description"] = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeDesc;
                    drtemprow["Select"]="False" ; 
                    drtemprow["Code"]=""; 
                    
                    drtemprow["Resource Name"]=""; 

                 drtemprow["nResourceTypeID"]="-1";
                 drtemprow["bitIsBlocked"] = false;
                 drtemprow["User Name"]=""; 
                drtemprow["nAppointmentTypeFlag"]="-1"; 
                drtemprow["User Name"]=""; 
                drtemprow["Follow Up Name"]=""; 
                drtemprow["Duration"]=""; 
                drtemprow["Criteria"]="-1"; 
                drtemprow["sCriteria"]=""; 
                drtemprow["Appointment Status"]=""; 
                drtemprow["Record Type"]=""; 
                drtemprow["Problem Type"]=""; 
                drtemprow["Color Codes"]=""; 
                drtemprow["nAppProcType"]="-1";
                drtemprow["Department"] = oAppointmentTemplate.TemplateDetails[i].DepartmentName; //" "; 
                drtemprow["nLocationID"]="-1"; 
                drtemprow["Location"]=""; 
                drtemprow["Address Line1"]=""; 
                drtemprow["Address Line2"]=""; 
                drtemprow["City"]=""; 
                drtemprow["State"]=" "; 
                drtemprow["Zip"]="-1"; 
                drtemprow["County"]=""; 
                drtemprow["Country"]=""; 
                drtemprow["bisDefault"]="-1"; 
                drtemprow["Appointment Type"]="-1"; 
                drtemprow["Appointment Block Type"]="-1";
                drtemprow["ColorCode1"] = ""; 
   //   oJUC_Appointment.Description = "";
                  //  oJUC_Appointment.Prefix = "";
                   drtemprow["ColorCode"]=      oAppointmentTemplate.TemplateDetails[i].ColorCode.ToString() ;

                    bool _ErrorFound = false;
                    try
                    {
                       drtemprow["EndTime"] =  oAppointmentTemplate.TemplateDetails[i].EndTime.ToString() ;
                        drtemprow["StartTime"] = oAppointmentTemplate.TemplateDetails[i].StartTime.ToString() ;
                    }
                    catch { _ErrorFound = true; }

                    if (_ErrorFound == true)
                    {
                        try
                        {
                            drtemprow["StartTime"] = oAppointmentTemplate.TemplateDetails[i].StartTime.ToString() ;
                            drtemprow["EndTime"] =  oAppointmentTemplate.TemplateDetails[i].EndTime.ToString() ;
                        }
                        catch { }
                    }

                   drtemprow ["AppointmentTemplates"]=Treenode.Text.Trim().ToString();  
                   drtemprow ["ParentAppointmentTemplate"]=Treenode.Text.Trim().ToString();
                   drtemprow["AppointmentCategory"] = "Template"; 
                   dt.Rows.Add(drtemprow);  
                }


            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Executing ProcessTemplates  For Appointment Configuration : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }


            return dt;
        
        
        
        }


        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            string[] temp;
            if (TagContent.Contains(Delimeter.ToString()))
            {
                temp = TagContent.Split(Delimeter);
                return temp[Position - 1];
            }
            else
            {
                return TagContent;
            }
        }





        public long Add(AppointmentTemplate oTemplate)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //DataBaseLayer oDB = new DataBaseLayer();
       
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //oDB.Connect(false);


            //Object TemplateID;
            Int64 nTemplateID = 0;
            //Int64 _AppTempLineNumber = 0;
            //try
            //{

            //    oDBParameters.Add("@TemplateID", oTemplate.TemplateID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //    oDBParameters.Add("@TemplateName", oTemplate.TemplateName, ParameterDirection.Input, SqlDbType.VarChar);
            //    oDBParameters.Add("@nClinicID", clsGeneral.gClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            // //   oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

            //    oDB.Execute("AB_INUP_AppointmentTemplate", oDBParameters, out  TemplateID);

            //    if (TemplateID != null)
            //    {
            //        nTemplateID = Convert.ToInt64(TemplateID);
            //        if (nTemplateID != 0)
            //        {
            //            string SqlQuery = "DELETE FROM AB_AppointmentTemplate_DTL WHERE nAppointmentTemplateID = " + nTemplateID + "";
            //            oDB.Execute_Query(SqlQuery);


            //            for (int i = 0; i < oTemplate.TemplateDetails.Count; i++)
            //            {
            //                _AppTempLineNumber = _AppTempLineNumber + 1;
            //                oDBParameters.Clear();
            //                //nAppointmentTemplateID, nTemplateLineNo, nAppointmentTypeID, sAppointmentTypeCode, sAppointmentTypeDesc,
            //                //nStartTime, nEndTime, sColorCode, nLocationID, sLocationName, nDepartmentID, sDepartmentName, nClinicID
            //                oDBParameters.Add("@nAppointmentTemplateID", nTemplateID, ParameterDirection.Input, SqlDbType.BigInt);
            //                oDBParameters.Add("@nTemplateLineNo", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
            //                oDBParameters.Add("@nAppointmentTypeID", oTemplate.TemplateDetails[i].AppointmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
            //                oDBParameters.Add("@sAppointmentTypeCode", oTemplate.TemplateDetails[i].AppointmentTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
            //                oDBParameters.Add("@sAppointmentTypeDesc", oTemplate.TemplateDetails[i].AppointmentTypeDesc, ParameterDirection.Input, SqlDbType.VarChar);
            //                oDBParameters.Add("@nStartTime", oTemplate.TemplateDetails[i].StartTime, ParameterDirection.Input, SqlDbType.BigInt);
            //                oDBParameters.Add("@nEndTime", oTemplate.TemplateDetails[i].EndTime, ParameterDirection.Input, SqlDbType.VarChar);
            //                oDBParameters.Add("@sColorCode", oTemplate.TemplateDetails[i].ColorCode, ParameterDirection.Input, SqlDbType.VarChar);
            //                oDBParameters.Add("@nLocationID", oTemplate.TemplateDetails[i].LocationID, ParameterDirection.Input, SqlDbType.BigInt);
            //                oDBParameters.Add("@sLocationName", oTemplate.TemplateDetails[i].LocationName, ParameterDirection.Input, SqlDbType.VarChar);
            //                oDBParameters.Add("@nDepartmentID", oTemplate.TemplateDetails[i].DepartmentID, ParameterDirection.Input, SqlDbType.VarChar);
            //                oDBParameters.Add("@sDepartmentName", oTemplate.TemplateDetails[i].DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
            //                oDBParameters.Add("@nClinicID", clsGeneral.gClinicID  , ParameterDirection.Input, SqlDbType.BigInt);

            //                oDB.Execute("AB_INUP_AppointmentTemplate_DTL", oDBParameters);
            //            }


            //        }
            //    }

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
            //    oDBParameters.Dispose();
            //    oDB.Dispose();
            //}
            return nTemplateID;
        }

     








        //public  DataTable GetOccupation()
        //{
        //       DataBaseLayer oDB = new DataBaseLayer();
        //    try
        //    {
        //        getdata = null;

        //        string strQuery = "SELECT nOccupationID,isnull(sEmployerName,'') as Employer,isnull(sOccupation,'') as Occupation,isnull(sPlaceofEmployment,'') as [Place of Employment],isnull(sWorkAddress1,'') as sWorkAddress1,isnull(sWorkAddress2,'') as sWorkAddress2,isnull(sWorkCity,'') as City,isnull(sWorkState,'') as sWorkState,isnull(sWorkZip,'') as sWorkZip,isnull(sWorkCountry,'') as sWorkCountry,isnull(sWorkPhone,'') as Phone,isnull(sWorkMobile,'') as Mobile,isnull(sWorkFax,'') as sWorkFax,isnull(sWorkEmail,'') as sWorkEmail FROM AB_Occupation_MST WHERE nClinicID = " + clsGeneral.gClinicID +"";

        //    getdata = oDB.GetDataTable_Query(strQuery);
        //    return getdata;




        //    }

        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        oDB.Dispose();
        //        oDB = null;
        //    } 
          
        
        //}


    }
}
