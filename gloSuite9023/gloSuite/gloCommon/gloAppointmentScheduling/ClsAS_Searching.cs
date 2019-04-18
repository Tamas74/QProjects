using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using gloGeneralItem;
using System.Collections;
using System.Windows.Forms;

namespace gloAppointmentScheduling
{
    namespace gloSearching
    {

        public class gloSearching : IDisposable
        {
            #region "Constructor & Distructor"

            private string _databaseconnectionstring = "";
            private string _MessageBoxCaption = string.Empty;
            private Int64 _ClinicID = 0;
           // gloGeneralItem.gloItems oAppointmentItems = new gloGeneralItem.gloItems();//added sandip darade 30072008 
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
       

            public gloSearching(string DatabaseConnectionString)
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

            ~gloSearching()
            {
                Dispose(false);
            }

            #endregion

            public DataTable SearchAppointments(AppointmentSearchCriteria oCriteria)
            {
                DataTable dtTemp = new DataTable();

                String strQuery = "";
                String strResourcesIDs = "";
                String strProviderIDs = "";
                String strProcedureIDs = "";

                gloItems oProviderItems = new gloItems();

                ArrayList _FindDates = new ArrayList();

                DateTime dtClinicStartTime = oCriteria.ClinicStartTime;
                DateTime dtClinicEndTime = oCriteria.ClinicEndTime;
                
                try
                {
                    // Add Columns to the DataTable to which we need to apply the Filte               
                    dtTemp.Columns.Add("Select");
                    dtTemp.Columns.Add("ProviderID");
                    dtTemp.Columns.Add("Provider Name");
                    dtTemp.Columns.Add("Date");
                    dtTemp.Columns.Add("Start Time");
                    dtTemp.Columns.Add("End Time");
                    dtTemp.Columns.Add("ProblemID");
                    dtTemp.Columns.Add("ProblemType");

                    #region " Get The Filter Criteria For Appointment "

                    //Provider ID's
                    for (int i = 0; i < oProviderItems.Count; i++)
                    {
                        if (strProviderIDs.Trim() == "")
                            strProviderIDs = Convert.ToString(oProviderItems[i].ID);
                        else
                            strProviderIDs = strProviderIDs + "," + oProviderItems[i].ID;

                    }
                    oProviderItems = oCriteria.Providers;

                   
                    //Resource ID's
                    for (int i = 0; i < oCriteria.Resources.Count; i++)
                    {
                        if (strResourcesIDs.Trim() == "")
                            strResourcesIDs = Convert.ToString(oCriteria.Resources[i].ID);
                        else
                            strResourcesIDs = strResourcesIDs + "," + oCriteria.Resources[i].ID;
                    }

                    //Problem Type ID's
                    for (int i = 0; i < oCriteria.ProblemTypes.Count; i++)
                    {
                        if (strProcedureIDs.Trim() == "")
                            strProcedureIDs = Convert.ToString(oCriteria.ProblemTypes[i].ID);
                        else
                            strProcedureIDs = strProcedureIDs + "," + oCriteria.ProblemTypes[i].ID;
                    }

                    //Dates                    
                    _FindDates = oCriteria.Dates;
                  

                    #endregion

                    
                    for (int l = 0; l < oProviderItems.Count; l++)
                    {
                        for (int m = 0; m <= _FindDates.Count - 1; m++)
                        {
                            Int64 nProviderID = Convert.ToInt64(oProviderItems[l].ID);

                            #region Get Schedules for Provider
                            //// Search Schedule
                            //// for the Given Date i.e for _FindDates[m] & The Selected Provider , Problem type & Resources
                            //string strScheduleQuery = "";
                            //strScheduleQuery = "SELECT dtStartDate,dtStartTime,dtEndDate,dtEndTime,sASBaseCode,sASBaseDesc,nASBaseFlag,nASBaseID FROM  AS_Schedule_DTL" +
                            //                  " WHERE (dtStartDate=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + "  )"+
                            //                  "AND (dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ")AND(nRefID IN (" + oProviderItems[l].ID + "))AND(nRefFlag =" + gloAppointmentScheduling.AppointmentScheduleFlag.ProviderSchedule.GetHashCode() + ") AND "; // 20080819

                            //if (strProcedureIDs.Trim() == "")
                            //{
                            //    if (strProviderIDs.Trim() != "")
                            //    {
                            //        //20080811  // strScheduleQuery = strScheduleQuery + " (AB_Resource_Provider.nProviderID IN (" + oProviderItems[l].ID + "))";
                            //        //strScheduleQuery = strScheduleQuery + " (nPRUFlag  IN (" + oProviderItems[l].ID + "))";
                            //        strScheduleQuery = strScheduleQuery + "nASBaseFlag  IN (" + ASBaseType.ProblemType.GetHashCode() + ")";
                            //    }
                                

                            //}
                            //else
                            //{
                            //    if (strProviderIDs.Trim() != "")
                            //    {
                            //        //20080811    // strScheduleQuery = strScheduleQuery + " (AB_Resource_Provider.nProviderID IN (" + oProviderItems[l].ID + ")) AND";
                            //       // strScheduleQuery = strScheduleQuery + "nPRUFlag  IN (" + SchedulePRUType.ProblemType.GetHashCode() + ")";
                            //        strScheduleQuery = strScheduleQuery + "nASBaseFlag  IN (" + ASBaseType.ProblemType.GetHashCode() + ")";
                            //    }
                            //    if (strProcedureIDs.Trim() != "")
                            //    {
                            //        //strScheduleQuery = strScheduleQuery + " (AS_Schedule_DTL_Procedures.nProcedureID IN (" + strProcedureIDs + "))";
                            //        //strScheduleQuery = strScheduleQuery + "  AND (nPRUID IN (" + strProcedureIDs + "))";
                            //        strScheduleQuery = strScheduleQuery + "  AND (nASBaseID IN (" + strProcedureIDs + "))";

                            //    }
                               

                            //}
                            ////strScheduleQuery = strScheduleQuery.Trim();
                            ////strScheduleQuery = strScheduleQuery.Remove((strScheduleQuery.Trim().Length - 3), 3);

                            ////20080811 // strScheduleQuery = strScheduleQuery + " ORDER BY AS_Schedule_DTL.StartTime, AS_Schedule_DTL.nEndTime ";
                            //strScheduleQuery = strScheduleQuery + " ORDER BY dtStartTime,dtEndTime ";

                            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            oDB.Connect(false);
                            DataTable dtSchedule = new DataTable();
                            //oDB.Retrive_Query(strScheduleQuery, out dtSchedule);
                            #endregion

                            #region "Get Providers Appointments "
                            
                            //---------------Providers Appointments and Blocks
                            strQuery = " SELECT nASBaseID AS nProviderID , dtStartDate, dtStartTime, dtEndDate, dtEndTime FROM AS_Appointment_DTL  WITH(NOLOCK) " +
                                       " WHERE (dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") " + 
                                       " AND (dtEndDate = " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") " + 
                                       " AND  (nASBaseID = " + oProviderItems[l].ID + ") AND (nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") " +                                      
                                       " UNION " +
                                       " SELECT nASBaseID AS nProviderID , dtStartDate, dtStartTime, dtEndDate, dtEndTime FROM AS_Schedule_DTL  WITH(NOLOCK) " +
                                       " WHERE (dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") " +
                                       " AND (dtEndDate = " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") " +
                                       " AND  (nASBaseID = " + oProviderItems[l].ID + ") AND (nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") " +
                                       " ORDER BY dtStartDate, dtStartTime,dtEndTime ";
                         
                            oDB.Connect(false);
                            DataTable dt;
                            oDB.Retrive_Query(strQuery, out dt);
                            
                            gloGeneralItem.gloItems oAppointmentItems = new gloGeneralItem.gloItems();
                            if (dt != null)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    gloGeneralItem.gloItem oAppointmentItem = new gloGeneralItem.gloItem();
                                    oAppointmentItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["dtStartTime"])).ToShortTimeString();
                                    oAppointmentItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["dtEndTime"])).ToShortTimeString();
                                  
                                    oAppointmentItems.Add(oAppointmentItem);
                                    oAppointmentItem.Dispose();
                                    oAppointmentItem = null;

                                }
                            }
                            //-----------------

                            #endregion
                          
                            #region Search Free Slots Without Problem Type

                            if (strProcedureIDs.Trim() == "")  //Problem Type Not Selected
                            {
                                gloGeneralItem.gloItems oFreeSlotItems = SplitDuration(dtClinicStartTime, dtClinicEndTime, Convert.ToInt64(oCriteria.Duration), oAppointmentItems);//for _AMPm                                

                                #region Attach ScheduleName/ProblemType to FreeSlot

                                //Add Schedule name & ID to free slots which have same time as schedule
                                for (int i = 0; i < dtSchedule.Rows.Count; i++)
                                {
                                    DateTime StartTime = dtClinicStartTime;
                                    DateTime EndTime = dtClinicEndTime;
                                    string ScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["dtStartTime"])).ToShortTimeString();
                                    string ScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["dtEndTime"])).ToShortTimeString();

                                    // If Schedule is not between Clinic Start & End Time 
                                    // then do not attach Schedule name to any free slot continue with next schedule 
                                    if (Convert.ToDateTime(ScheduleStartTime) < dtClinicStartTime && Convert.ToDateTime(ScheduleEndTime) <= dtClinicStartTime)
                                    {
                                        continue;
                                    }
                                    if (Convert.ToDateTime(ScheduleStartTime) >= dtClinicEndTime && Convert.ToDateTime(ScheduleEndTime) > dtClinicEndTime)
                                    {
                                        continue;
                                    }

                                    if (oFreeSlotItems != null)
                                    {
                                        for (int x = 0; x < oFreeSlotItems.Count; x++)
                                        {
                                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) <= Convert.ToDateTime(ScheduleStartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) > Convert.ToDateTime(ScheduleStartTime))
                                            {
                                                StartTime = Convert.ToDateTime(oFreeSlotItems[x].Code);
                                            }

                                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) < Convert.ToDateTime(ScheduleEndTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) >= Convert.ToDateTime(ScheduleEndTime))
                                            {
                                                EndTime = Convert.ToDateTime(oFreeSlotItems[x].Description);
                                            }
                                        }
                                        if (StartTime <= dtClinicStartTime)
                                        {
                                            StartTime = Convert.ToDateTime(ScheduleStartTime);
                                        }

                                        if (EndTime >= dtClinicEndTime)
                                        {
                                            EndTime = Convert.ToDateTime(ScheduleEndTime);
                                        }

                                        for (int x = oFreeSlotItems.Count - 1; x >= 0; x--)
                                        {

                                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) >= Convert.ToDateTime(StartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) <= Convert.ToDateTime(EndTime))
                                            {
                                                gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();
                                                oSubItem.Code = Convert.ToString(dtSchedule.Rows[i]["sASBaseCode"]);
                                                oSubItem.Description = Convert.ToString(dtSchedule.Rows[i]["sASBaseDesc"]);
                                                oFreeSlotItems[x].SubItems.Add(oSubItem);
                                                oSubItem.Dispose();
                                                oSubItem = null;
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #region Code To Display Appointment Slot Comented
                                //for (int k = 0; k < oFreeSlotItems.Count; k++)
                                //{
                                //    gloGeneralItem.gloItem oItemTime = new gloGeneralItem.gloItem();
                                //    oItemTime = oFreeSlotItems[k];
                                //    // if Appointment is  before first freeslot 
                                //    if (k == 0)
                                //    {
                                //        if (Convert.ToDateTime(oFreeSlotItems[k].Code) > oCriteria.ClinicStartTime && Convert.ToDateTime(oFreeSlotItems[k].Description) <= Convert.ToDateTime(oFreeSlotItems[k + 1].Code))
                                //        {
                                //            DataRow Fslot;
                                //            Fslot = dtTemp.NewRow();

                                //            Fslot["Select"] = Convert.ToBoolean(1);
                                //            Fslot["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                //            Fslot["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                //            Fslot["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
                                //            Fslot["Start Time"] = oCriteria.ClinicStartTime.ToString("hh:mm tt");
                                //            Fslot["End Time"] = oFreeSlotItems[k].Code;
                                //            dtTemp.Rows.Add(Fslot);
                                //        }
                                //    }

                                //    DataRow r;
                                //    r = dtTemp.NewRow();
                                //    //Column[0]= "ProviderID"
                                //    //Column[1]= "Provider Name"
                                //    //Column[2]= "Date"
                                //    //Column[3]= "Start Time"
                                //    //Column[4]= "End Time"
                                //    r["Select"] = Convert.ToBoolean(0);
                                //    r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                //    r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                //    r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
                                //    r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
                                //    r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
                                //    for (int i = 0; i < oItemTime.SubItems.Count; i++)
                                //    {
                                //        r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
                                //        r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
                                //    }
                                //    // Remove last coma ","
                                //    if (Convert.ToString(r["ProblemType"]).Length > 0)
                                //    {
                                //        r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
                                //    }
                                //    dtTemp.Rows.Add(r);

                                //    if (k < oFreeSlotItems.Count - 2)
                                //    {

                                //        if (Convert.ToDateTime(oFreeSlotItems[k].Description) != Convert.ToDateTime(oFreeSlotItems[k + 1].Code))
                                //        {

                                //            DataRow rBlocked;
                                //            rBlocked = dtTemp.NewRow();

                                //            rBlocked["Select"] = Convert.ToBoolean(1);
                                //            rBlocked["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                //            rBlocked["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                //            rBlocked["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
                                //            rBlocked["Start Time"] = oFreeSlotItems[k].Description;
                                //            rBlocked["End Time"] = oFreeSlotItems[k + 1].Code;
                                //            dtTemp.Rows.Add(rBlocked);

                                //        }
                                //    }

                                //} 
                                #endregion
                                if (oFreeSlotItems != null)
                                {
                                    for (int k = 0; k < oFreeSlotItems.Count; k++)
                                    {
                                        gloGeneralItem.gloItem oItemTime = oFreeSlotItems[k]; //new gloGeneralItem.gloItem();
                                        //oItemTime = oFreeSlotItems[k];

                                        DataRow r;
                                        r = dtTemp.NewRow();
                                        //Column[0]= "ProviderID"
                                        //Column[1]= "Provider Name"
                                        //Column[2]= "Date"
                                        //Column[3]= "Start Time"
                                        //Column[4]= "End Time"
                                        r["Select"] = Convert.ToBoolean(0);
                                        r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                        r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                        r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
                                        r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
                                        r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
                                        for (int i = 0; i < oItemTime.SubItems.Count; i++)
                                        {
                                            r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
                                            r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
                                        }
                                        // Remove last coma ","
                                        if (Convert.ToString(r["ProblemType"]).Length > 0)
                                        {
                                            r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
                                        }

                                        dtTemp.Rows.Add(r);
                                    } //  for (int k = 0; k < oFreeSlotItems.Count; k++)
                                }
                                if (oFreeSlotItems != null)
                                {
                                    oFreeSlotItems.Clear();
                                    oFreeSlotItems.Dispose();
                                    oFreeSlotItems = null;
                                }
                            }
                            #endregion Search Free Slots Without Problem Type

                            #region Search Free Slots in Problem Type
                            //If Shedule Present for selected problem type
                            //change clinic start & end time by schedule start and end time
                            //i.e : Search only in Schedule time 
                            if (strProcedureIDs.Trim() != "")
                            {

                                #region Find The time slots within which we have to search


                                gloGeneralItem.gloItems oScheduleItems = new gloGeneralItem.gloItems();
                                Int32 nSTime = gloDateMaster.gloTime.TimeAsNumber(dtClinicStartTime.ToShortTimeString());
                                Int32 nETime = gloDateMaster.gloTime.TimeAsNumber(dtClinicEndTime.ToShortTimeString());

                                for (int i = 0; i < dtSchedule.Rows.Count; i++)
                                {
                                    nSTime = Convert.ToInt32(dtSchedule.Rows[i]["dtStartTime"]);
                                    nETime = Convert.ToInt32(dtSchedule.Rows[i]["dtEndTime"]);

                                    for (int k = i; k < dtSchedule.Rows.Count; k++)
                                    {
                                        Int32 nTempSTime = Convert.ToInt32(dtSchedule.Rows[k]["dtStartTime"]);
                                        Int32 nTempETime = Convert.ToInt32(dtSchedule.Rows[k]["dtEndTime"]);
                                        if (nSTime <= nTempSTime && nETime >= nTempSTime)
                                        {
                                            if (nETime <= nTempETime)
                                            {
                                                nETime = nTempETime;
                                                i = k;
                                            }
                                            else
                                            {
                                                i = k;
                                            }
                                        }
                                    }

                                    gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
                                    oItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, nSTime).ToShortTimeString();
                                    oItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, nETime).ToShortTimeString();

                                    oScheduleItems.Add(oItem);
                                    oItem.Dispose();
                                    oItem = null;
                                }

                                #endregion
                               
                                for (int n = 0; n < oScheduleItems.Count; n++)
                                {                                 
                                    //DateTime dtScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[n]["dtStartTime"]));
                                    //DateTime dtScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[n]["dtEndTime"]));

                                    DateTime dtScheduleStartTime = Convert.ToDateTime(oScheduleItems[n].Code);
                                    DateTime dtScheduleEndTime = Convert.ToDateTime(oScheduleItems[n].Description);
                                   

                                    if (dtScheduleStartTime < dtClinicStartTime && dtScheduleEndTime <= dtClinicStartTime)
                                    {
                                        continue;
                                    }
                                    if (dtScheduleStartTime >= dtClinicEndTime && dtScheduleEndTime > dtClinicEndTime)
                                    {
                                        continue;
                                    }

                                    if (dtScheduleStartTime < dtClinicStartTime)
                                        dtScheduleStartTime = dtClinicStartTime;
                                    if (dtScheduleEndTime > dtClinicEndTime)
                                        dtScheduleEndTime = dtClinicEndTime;

                                    gloGeneralItem.gloItems oFreeSlotItems = SplitDuration(dtScheduleStartTime, dtScheduleEndTime, Convert.ToInt64(oCriteria.Duration), oAppointmentItems);
                                    
                                    #region Attach ScheduleName/ProblemType to FreeSlot

                                    //Add Schedule name & ID to free slots which has same time as schedule
                                    for (int i = 0; i < dtSchedule.Rows.Count; i++)
                                    {
                                        DateTime StartTime = dtClinicStartTime;
                                        DateTime EndTime = dtClinicEndTime;
                                        string ScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["dtStartTime"])).ToShortTimeString();
                                        string ScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["dtEndTime"])).ToShortTimeString();

                                        // If Schedule is not between Clinic Start & End Time 
                                        // then do not attach Schedule name to any free slot continue with next schedule 
                                        if (Convert.ToDateTime(ScheduleStartTime) < dtClinicStartTime && Convert.ToDateTime(ScheduleEndTime) <= dtClinicStartTime)
                                        {
                                            continue;
                                        }
                                        if (Convert.ToDateTime(ScheduleStartTime) >= dtClinicEndTime && Convert.ToDateTime(ScheduleEndTime) > dtClinicEndTime)
                                        {
                                            continue;
                                        }
                                        if (oFreeSlotItems != null)
                                        {
                                            for (int x = 0; x < oFreeSlotItems.Count; x++)
                                            {
                                                if (Convert.ToDateTime(oFreeSlotItems[x].Code) <= Convert.ToDateTime(ScheduleStartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) > Convert.ToDateTime(ScheduleStartTime))
                                                {
                                                    StartTime = Convert.ToDateTime(oFreeSlotItems[x].Code);
                                                }

                                                if (Convert.ToDateTime(oFreeSlotItems[x].Code) < Convert.ToDateTime(ScheduleEndTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) >= Convert.ToDateTime(ScheduleEndTime))
                                                {
                                                    EndTime = Convert.ToDateTime(oFreeSlotItems[x].Description);
                                                }
                                            }

                                            if (StartTime <= dtClinicStartTime)
                                            {
                                                StartTime = Convert.ToDateTime(ScheduleStartTime);
                                            }

                                            if (EndTime >= dtClinicEndTime)
                                            {
                                                EndTime = Convert.ToDateTime(ScheduleEndTime);
                                            }

                                            for (int x = oFreeSlotItems.Count - 1; x >= 0; x--)
                                            {
                                                if (Convert.ToDateTime(oFreeSlotItems[x].Code) >= Convert.ToDateTime(StartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) <= Convert.ToDateTime(EndTime))
                                                {
                                                    gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();
                                                    oSubItem.Code = Convert.ToString(dtSchedule.Rows[i]["sASBaseCode"]);
                                                    oSubItem.Description = Convert.ToString(dtSchedule.Rows[i]["sASBaseDesc"]);

                                                    // If Problem Type is alredy attached to freeslot do not attach again 
                                                    bool _IsProblemTypeAttached = false;
                                                    for (int y = 0; y < oFreeSlotItems[x].SubItems.Count; y++)
                                                    {
                                                        if (oSubItem.Code == oFreeSlotItems[x].SubItems[y].Code)
                                                            _IsProblemTypeAttached = true;
                                                    }
                                                    if (_IsProblemTypeAttached == false)
                                                        oFreeSlotItems[x].SubItems.Add(oSubItem);
                                                    oSubItem.Dispose();
                                                    oSubItem = null;
                                                    //----------------------------------------------------------------

                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Code To Display Appointment Slot Comented
                                    //for (int k = 0; k < oFreeSlotItems.Count; k++)
                                    //{
                                    //    gloGeneralItem.gloItem oItemTime = new gloGeneralItem.gloItem();
                                    //    oItemTime = oFreeSlotItems[k];
                                    //    // if Appointment is  before first freeslot 
                                    //    if (k == 0)
                                    //    {
                                    //        if (Convert.ToDateTime(oFreeSlotItems[k].Code) == oCriteria.ClinicStartTime && Convert.ToDateTime(oFreeSlotItems[k].Description) <= Convert.ToDateTime(oFreeSlotItems[k + 1].Code))
                                    //        {
                                    //            DataRow Fslot;
                                    //            Fslot = dtTemp.NewRow();

                                    //            Fslot["Select"] = Convert.ToBoolean(1);
                                    //            Fslot["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                    //            Fslot["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                    //            Fslot["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
                                    //            Fslot["Start Time"] = oCriteria.ClinicStartTime.ToString("hh:mm tt");
                                    //            Fslot["End Time"] = oFreeSlotItems[k].Code;
                                    //            dtTemp.Rows.Add(Fslot);
                                    //        }
                                    //    }

                                    //    DataRow r;
                                    //    r = dtTemp.NewRow();
                                    //    //Column[0]= "ProviderID"
                                    //    //Column[1]= "Provider Name"
                                    //    //Column[2]= "Date"
                                    //    //Column[3]= "Start Time"
                                    //    //Column[4]= "End Time"
                                    //    r["Select"] = Convert.ToBoolean(0);
                                    //    r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                    //    r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                    //    r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
                                    //    r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
                                    //    r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
                                    //    for (int i = 0; i < oItemTime.SubItems.Count; i++)
                                    //    {
                                    //        r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
                                    //        r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
                                    //    }
                                    //    // Remove last coma ","
                                    //    if (Convert.ToString(r["ProblemType"]).Length > 0)
                                    //    {
                                    //        r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
                                    //    }
                                    //    dtTemp.Rows.Add(r);

                                    //    if (k < oFreeSlotItems.Count - 2)
                                    //    {

                                    //        if (Convert.ToDateTime(oFreeSlotItems[k].Description) != Convert.ToDateTime(oFreeSlotItems[k + 1].Code))
                                    //        {

                                    //            DataRow rBlocked;
                                    //            rBlocked = dtTemp.NewRow();

                                    //            rBlocked["Select"] = Convert.ToBoolean(1);
                                    //            rBlocked["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                    //            rBlocked["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                    //            rBlocked["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
                                    //            rBlocked["Start Time"] = oFreeSlotItems[k].Description;
                                    //            rBlocked["End Time"] = oFreeSlotItems[k + 1].Code;
                                    //            dtTemp.Rows.Add(rBlocked);

                                    //        }
                                    //    }

                                    //}
                                    #endregion

                                    if (oFreeSlotItems != null)
                                    {
                                        for (int k = 0; k < oFreeSlotItems.Count; k++)
                                        {
                                            gloGeneralItem.gloItem oItemTime = oFreeSlotItems[k];  //new gloGeneralItem.gloItem();
                                            //oItemTime = oFreeSlotItems[k];

                                            DataRow r;
                                            r = dtTemp.NewRow();
                                            //Column[0]= "ProviderID"
                                            //Column[1]= "Provider Name"
                                            //Column[2]= "Date"
                                            //Column[3]= "Start Time"
                                            //Column[4]= "End Time"
                                            r["Select"] = Convert.ToBoolean(0);
                                            r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                            r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                            r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
                                            r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
                                            r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
                                            for (int i = 0; i < oItemTime.SubItems.Count; i++)
                                            {
                                                r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
                                                r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
                                            }
                                            // Remove last coma ","
                                            if (Convert.ToString(r["ProblemType"]).Length > 0)
                                            {
                                                r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
                                            }

                                            dtTemp.Rows.Add(r);
                                            //nTempStartTime = gloDateMaster.gloTime.TimeAsNumber(oItemTime.Description.ToString());
                                        } //  for (int k = 0; k < oFreeSlotItems.Count; k++)
                                    }
                                    if (oFreeSlotItems != null)
                                    {
                                        oFreeSlotItems.Clear();
                                        oFreeSlotItems.Dispose();
                                        oFreeSlotItems = null;
                                    }   
                                }//for (int n = 0; n < dtSchedule.Rows.Count; n++)
                                if (oScheduleItems != null)
                                {
                                    oScheduleItems.Clear();
                                    oScheduleItems.Dispose();
                                    oScheduleItems = null;
                                }
                            }// if (strProcedureIDs.Trim() != "")
                            #endregion Search Free Slots in Problem Type
                            if (oAppointmentItems != null)
                            {
                                oAppointmentItems.Clear();
                                oAppointmentItems.Dispose();
                                oAppointmentItems = null;
                            }
                        } //     for (int l = 0; l < oProviderItems.Count; l++)
                    } // for (int m = 0; m <= _FindDates.Count - 1; m++)

                } // try
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;   
                    return null;
                }

                return dtTemp;

            }

            public DataTable GetAppointments(AppointmentSearchCriteria oCriteria)
            {
                DataTable dtTemp = new DataTable();

                String strQuery = "";
                String strResourcesIDs = "";
                String strProviderIDs = "";
                String strProcedureIDs = "";
                String strFindDates = "";

                try
                {
                    #region " Get The Filter Criteria For Appointment "

                    //Provider ID's
                    for (int i = 0; i < oCriteria.Providers.Count; i++)
                    {
                        if (strProviderIDs.Trim() == "")
                            strProviderIDs = Convert.ToString(oCriteria.Providers[i].ID);
                        else
                            strProviderIDs = strProviderIDs + "," + oCriteria.Providers[i].ID;

                    }

                    if (strProviderIDs == "")
                    {
                        strProviderIDs = "0";
                    }

                    //Resource ID's
                    for (int i = 0; i < oCriteria.Resources.Count; i++)
                    {
                        if (strResourcesIDs.Trim() == "")
                            strResourcesIDs = Convert.ToString(oCriteria.Resources[i].ID);
                        else
                            strResourcesIDs = strResourcesIDs + "," + oCriteria.Resources[i].ID;
                    }

                    //Problem Type ID's
                    for (int i = 0; i < oCriteria.ProblemTypes.Count; i++)
                    {
                        if (strProcedureIDs.Trim() == "")
                            strProcedureIDs = Convert.ToString(oCriteria.ProblemTypes[i].ID);
                        else
                            strProcedureIDs = strProcedureIDs + "," + oCriteria.ProblemTypes[i].ID;
                    }

                    //Find Dates
                    for (int i = 0; i < oCriteria.Dates.Count; i++)
                    {
                        if (strFindDates.Trim() == "")
                            strFindDates = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[i])).ToString();
                        else
                            strFindDates = strFindDates + "," + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[i])).ToString();
                    }

                    #endregion

                    string FromDate;
                    string ToDate;

                    FromDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[0])).ToString();
                    ToDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[oCriteria.Dates.Count - 1])).ToString();


                    //Changed query by Amit to show all blocked appointment time and excluding delete, no show and canceled appointment
                    strQuery = "  SELECT nASBaseID AS ProviderID ,sASBaseDesc As ProviderName, nASBaseFlag, dtStartDate, dtStartTime, dtEndDate, dtEndTime,ISNULL(nUsedStatus,0) AS nUsedStatus  FROM AS_Appointment_DTL  WITH(NOLOCK)  " +
                             " WHERE " +
                             " (dtStartDate between " + FromDate + " and  " + ToDate + " or dtEndDate between " + FromDate + " and " + ToDate + " ) " +
                             " AND nASBaseID IN (" + strProviderIDs + ") AND nASBaseFlag =  " + ASBaseType.Provider.GetHashCode() + " AND nUsedStatus not in (5,6,7) " +
                             " UNION  " +
                             " SELECT nASBaseID AS ProviderID ,sASBaseDesc As ProviderName, nASBaseFlag ,dtStartDate, dtStartTime, dtEndDate, dtEndTime,11 AS nUsedStatus FROM AS_Schedule_DTL  WITH(NOLOCK)  " +
                             " WHERE " +
                             " (dtStartDate between " + FromDate + " and  " + ToDate + " or dtEndDate between " + FromDate + " and " + ToDate + " ) " +

                             " AND nASBaseID IN (" + strProviderIDs + ") " +
                             " AND nASBaseFlag =  " + ASBaseType.Provider.GetHashCode() + " ";

                    if (oCriteria.Location != "")
                    {
                        strQuery += " AND sLocationName in ('" + oCriteria.Location + "','<All Locations>')";
                    }
                    if (oCriteria.IsAdvanceSearch == true && strResourcesIDs.Length > 0)
                    {
                        strQuery += " UNION SELECT nASBaseID AS ProviderID ,sASBaseDesc As ProviderName, nASBaseFlag ,dtStartDate, dtStartTime, dtEndDate,dtEndTime,ISNULL(nUsedStatus,0) AS nUsedStatus FROM AS_Appointment_DTL WITH(NOLOCK)  " +
                                  " WHERE dtStartDate IN (" + strFindDates + ")  AND dtEndDate IN (" + strFindDates + ") " +
                                  " AND nASBaseID IN (" + strResourcesIDs + ") AND nASBaseFlag =  " + ASBaseType.Resource.GetHashCode() + "  AND nUsedStatus not in (5,6,7) " +
                                  " UNION " +
                                  " SELECT nASBaseID AS ProviderID ,sASBaseDesc As ProviderName, nASBaseFlag,dtStartDate, dtStartTime, dtEndDate, dtEndTime, 11 AS nUsedStatus FROM AS_Schedule_DTL  WITH(NOLOCK) " +
                                  " WHERE dtStartDate IN (" + strFindDates + ")  AND dtEndDate IN (" + strFindDates + ") AND nASBaseID IN (" + strResourcesIDs + ") AND nASBaseFlag =  " + ASBaseType.Resource.GetHashCode() + " ";
                    }

                    strQuery += " ORDER BY dtStartDate, dtStartTime,dtEndTime ";

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    oDB.Retrive_Query(strQuery, out dtTemp);

                } // try
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    return null;
                }
                finally
                {
                }

                return dtTemp;
            }


            public DataTable GetAppointmentsUsingProviderID(AppointmentSearchCriteria oCriteria, long providerID)
            {
                DataTable dtTemp = new DataTable();

                String strQuery = "";
                String strResourcesIDs = "";
                String strProviderIDs = "";
                String strProcedureIDs = "";
                String strFindDates = "";

                try
                {
                    #region " Get The Filter Criteria For Appointment "

                    //Provider ID's

                    //for (int i = 0; i < oCriteria.Providers.Count; i++)
                    //{
                    //    if (strProviderIDs.Trim() == "")
                    //        strProviderIDs = Convert.ToString(oCriteria.Providers[i].ID);
                    //    else
                    //        strProviderIDs = strProviderIDs + "," + oCriteria.Providers[i].ID;

                    //}

                    strProviderIDs = Convert.ToString(providerID);


                    //Resource ID's
                    for (int i = 0; i < oCriteria.Resources.Count; i++)
                    {
                        if (strResourcesIDs.Trim() == "")
                            strResourcesIDs = Convert.ToString(oCriteria.Resources[i].ID);
                        else
                            strResourcesIDs = strResourcesIDs + "," + oCriteria.Resources[i].ID;
                    }

                    //Problem Type ID's
                    for (int i = 0; i < oCriteria.ProblemTypes.Count; i++)
                    {
                        if (strProcedureIDs.Trim() == "")
                            strProcedureIDs = Convert.ToString(oCriteria.ProblemTypes[i].ID);
                        else
                            strProcedureIDs = strProcedureIDs + "," + oCriteria.ProblemTypes[i].ID;
                    }

                    //Find Dates
                    for (int i = 0; i < oCriteria.Dates.Count; i++)
                    {
                        if (strFindDates.Trim() == "")
                            strFindDates = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[i])).ToString();
                        else
                            strFindDates = strFindDates + "," + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[i])).ToString();
                    }

                    #endregion

                    string FromDate;
                    string ToDate;

                    FromDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[0])).ToString();
                    ToDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[oCriteria.Dates.Count - 1])).ToString();

                    //Changed query by Amit to show all blocked appointment time and excluding delete, no show and canceled appointment
                    strQuery = "  SELECT nASBaseID AS ProviderID ,sASBaseDesc As ProviderName, nASBaseFlag, dtStartDate, dtStartTime, dtEndDate, dtEndTime,ISNULL(nUsedStatus,0) AS nUsedStatus  FROM AS_Appointment_DTL   WITH(NOLOCK) " +
                             " WHERE " +
                             " (dtStartDate between " + FromDate + " and  " + ToDate + " or dtEndDate between " + FromDate + " and " + ToDate + " ) " +
                             " AND nASBaseID IN (" + strProviderIDs + ") AND nASBaseFlag =  " + ASBaseType.Provider.GetHashCode() + " AND nUsedStatus not in (5,6,7) " +
                             " UNION  " +
                             " SELECT nASBaseID AS ProviderID ,sASBaseDesc As ProviderName, nASBaseFlag ,dtStartDate, dtStartTime, dtEndDate, dtEndTime,11 AS nUsedStatus FROM AS_Schedule_DTL  WITH(NOLOCK)  " +
                             " WHERE dtStartDate IN (" + strFindDates + ")  AND dtEndDate IN (" + strFindDates + ") AND nASBaseID IN (" + strProviderIDs + ") AND nASBaseFlag =  " + ASBaseType.Provider.GetHashCode() + " ";




                    if (oCriteria.IsAdvanceSearch == true && strResourcesIDs.Length > 0)
                    {
                        strQuery += " UNION SELECT nASBaseID AS ProviderID ,sASBaseDesc As ProviderName, nASBaseFlag ,dtStartDate, dtStartTime, dtEndDate,dtEndTime,ISNULL(nUsedStatus,0) AS nUsedStatus FROM AS_Appointment_DTL  WITH(NOLOCK) " +
                                  " WHERE dtStartDate IN (" + strFindDates + ")  AND dtEndDate IN (" + strFindDates + ") AND nASBaseID IN (" + strResourcesIDs + ") AND nASBaseFlag =  " + ASBaseType.Resource.GetHashCode() + "  AND nUsedStatus not in (5,6,7) " +
                                  " UNION " +
                                  " SELECT nASBaseID AS ProviderID ,sASBaseDesc As ProviderName, nASBaseFlag,dtStartDate, dtStartTime, dtEndDate, dtEndTime, 11 AS nUsedStatus FROM AS_Schedule_DTL WITH(NOLOCK)  " +
                                  " WHERE dtStartDate IN (" + strFindDates + ")  AND dtEndDate IN (" + strFindDates + ") AND nASBaseID IN (" + strResourcesIDs + ") AND nASBaseFlag =  " + ASBaseType.Resource.GetHashCode() + " ";
                    }


                    //if (oCriteria.Location.Trim() != "")
                    //{
                    //    strQuery += " AND UPPER(ProblemType.sLocationName)= '" + oCriteria.Location.ToUpper() + "' ";
                    //}
                    //if (oCriteria.Department.Trim() != "")
                    //{
                    //    strQuery += " AND UPPER(ProblemType.sDepartmentName) = '" + oCriteria.Department.ToUpper() + "'";
                    //}



                    strQuery += " ORDER BY dtStartDate, dtStartTime,dtEndTime ";

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    oDB.Retrive_Query(strQuery, out dtTemp);

                } // try
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    return null;
                }
                finally
                {
                }

                return dtTemp;
            }

            //public DataTable GetSchedules(ArrayList _ProviderIDs, ArrayList _FindDates, ArrayList _AppointmentTypes, String _sLocation, String _sDepartment)
            //{
            //    DataTable dtSchedules = new DataTable();
            //    String strProviderIDs = "";
            //    String strAppointmentTypes = "";
            //    String _strFindDates = "";
            //    String strFlag = "";

            //    try
            //    {

            //        for (int i = 0; i < _ProviderIDs.Count; i++)
            //        {
            //            if (strProviderIDs.Trim() == "")
            //                strProviderIDs = Convert.ToString(_ProviderIDs[i]);
            //            else
            //                strProviderIDs = strProviderIDs + "," + Convert.ToString(_ProviderIDs[i]);
            //        }


            //        for (int i = 0; i < _FindDates.Count; i++)
            //        {
            //            if (_strFindDates.Trim() == "")
            //                _strFindDates = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(_FindDates[i])).ToString();
            //            else
            //                _strFindDates = _strFindDates + "," + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(_FindDates[i])).ToString();
            //        }


            //        for (int i = 0; i < _AppointmentTypes.Count; i++)
            //        {
            //            if (strAppointmentTypes.Trim() == "")
            //                strAppointmentTypes = "" + Convert.ToString(_AppointmentTypes[i]).Replace("'", "''") + "";
            //            else
            //                strAppointmentTypes = strAppointmentTypes + "," + Convert.ToString(_AppointmentTypes[i]).Replace("'", "''") + "";
            //        }

            //        if (_AppointmentTypes.Count > 0)
            //        {
            //            strFlag = "Template";
            //        }

            //        if (strProviderIDs.Length > 0)
            //        {
            //            dtSchedules = GetCalendarSearchOpenSlot(_sLocation, _sDepartment, strProviderIDs, strAppointmentTypes, _strFindDates, strFlag);
            //        }

            //        return dtSchedules;
            //    }
            //    catch (gloDatabaseLayer.DBException dbEx)
            //    {
            //        dbEx.ERROR_Log(dbEx.ToString());
            //        return null;
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //        ex = null;
            //        return null;
            //    }
            //    finally
            //    {
            //        dtSchedules.Dispose();
            //        dtSchedules = null;
            //        strProviderIDs = null;
            //        strAppointmentTypes = null;
            //        _strFindDates = null;
            //        strFlag = null;
            //    }
            //}

            public DataTable GetSchedules(AppointmentSearchCriteria oCriteria, long ProviderID)
            {

                DataTable dtTemp = new DataTable();
                String strQuery = "";
                String strProblemTypeIDs = "";
                String strResourceIDs = "";
                String strFindDates = "";

                try
                {

                    #region " Get The Filter Criteria For Appointment "

                    //Problem Type ID's
                    for (int i = 0; i < oCriteria.ProblemTypes.Count; i++)
                    {
                        if (strProblemTypeIDs.Trim() == "")
                            strProblemTypeIDs = Convert.ToString(oCriteria.ProblemTypes[i].ID);
                        else
                            strProblemTypeIDs = strProblemTypeIDs + "," + oCriteria.ProblemTypes[i].ID;
                    }

                    //Resource ID's
                    for (int i = 0; i < oCriteria.Resources.Count; i++)
                    {
                        if (strResourceIDs.Trim() == "")
                            strResourceIDs = Convert.ToString(oCriteria.Resources[i].ID);
                        else
                            strResourceIDs = strResourceIDs + "," + oCriteria.Resources[i].ID;
                    }


                    //Find Dates
                    for (int i = 0; i < oCriteria.Dates.Count; i++)
                    {
                        if (strFindDates.Trim() == "")
                            strFindDates = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[i])).ToString();
                        else
                            strFindDates = strFindDates + "," + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oCriteria.Dates[i])).ToString();
                    }

                    #endregion

                    //---------------Providers Appointments and Blocks
                    if (strProblemTypeIDs.Length > 0)
                    {
                        strQuery = " SELECT Provider.nASBaseID AS ProviderID, Provider.sASBaseDesc AS ProviderName, "
                                  + " ProblemType.nASBaseID AS PRoblemTypeID,ProblemType.sASBaseDesc AS ProlemType, "
                                  + " ProblemType.sLocationName, ProblemType.sDepartmentName,"
                                  + " ProblemType.dtStartDate, ProblemType.dtStartTime, ProblemType.dtEndDate, ProblemType.dtEndTime "
                                  + " FROM   AS_Schedule_DTL AS ProblemType  WITH(NOLOCK) INNER JOIN AS_Schedule_DTL AS Provider  WITH(NOLOCK) "
                                  + " ON ProblemType.nMSTScheduleID = Provider.nMSTScheduleID AND ProblemType.nRefID = Provider.nDTLScheduleID ";
                        if (oCriteria.IsProblemTypeSearch == true)
                        {
                            strQuery += " WHERE (ProblemType.nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " ) AND ProblemType.nASBaseID IN (" + strProblemTypeIDs + ") AND "
                                      + " ProblemType.dtStartDate IN (" + strFindDates + ") AND ProblemType.dtEndDate IN (" + strFindDates + ")"
                                      + " AND (Provider.nASBaseID = " + ProviderID + " ) ";
                        }
                        else
                        {
                            strQuery += " WHERE (ProblemType.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " ) AND ProblemType.nASBaseID IN (" + strResourceIDs + ") AND "
                                    + " ProblemType.dtStartDate IN (" + strFindDates + ") AND ProblemType.dtEndDate IN (" + strFindDates + ")"
                                    + " AND (Provider.nASBaseID = " + ProviderID + " ) ";
                        }

                        if (oCriteria.Location.Trim() != "")
                        {
                            strQuery += " AND UPPER(ProblemType.sLocationName)= '" + oCriteria.Location.ToUpper() + "' ";
                        }
                        if (oCriteria.Department.Trim() != "")
                        {
                            strQuery += " AND UPPER(ProblemType.sDepartmentName) = '" + oCriteria.Department.ToUpper() + "'";
                        }

                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        oDB.Connect(false);
                        oDB.Retrive_Query(strQuery, out dtTemp);
                    }

                } // try
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    return null;
                }
                finally
                {
                }

                return dtTemp;
            }

            public DataTable GetAppointments_Working(AppointmentSearchCriteria oCriteria)
            {
                DataTable dtTemp = new DataTable();

                String strQuery = "";
                String strResourcesIDs = "";
                String strProviderIDs = "";
                String strProcedureIDs = "";

                gloItems oProviderItems = new gloItems();

                ArrayList _FindDates = new ArrayList();

                DateTime dtClinicStartTime = oCriteria.ClinicStartTime;
                DateTime dtClinicEndTime = oCriteria.ClinicEndTime;

                try
                {
                    // Add Columns to the DataTable to which we need to apply the Filte               
                    dtTemp.Columns.Add("ProviderID");
                    dtTemp.Columns.Add("Provider Name");
                    dtTemp.Columns.Add("Date");
                    dtTemp.Columns.Add("Start Time");
                    dtTemp.Columns.Add("End Time");

                    #region " Get The Filter Criteria For Appointment "

                    //Provider ID's
                    for (int i = 0; i < oProviderItems.Count; i++)
                    {
                        if (strProviderIDs.Trim() == "")
                            strProviderIDs = Convert.ToString(oProviderItems[i].ID);
                        else
                            strProviderIDs = strProviderIDs + "," + oProviderItems[i].ID;

                    }
                    oProviderItems = oCriteria.Providers;


                    //Resource ID's
                    for (int i = 0; i < oCriteria.Resources.Count; i++)
                    {
                        if (strResourcesIDs.Trim() == "")
                            strResourcesIDs = Convert.ToString(oCriteria.Resources[i].ID);
                        else
                            strResourcesIDs = strResourcesIDs + "," + oCriteria.Resources[i].ID;
                    }

                    //Problem Type ID's
                    for (int i = 0; i < oCriteria.ProblemTypes.Count; i++)
                    {
                        if (strProcedureIDs.Trim() == "")
                            strProcedureIDs = Convert.ToString(oCriteria.ProblemTypes[i].ID);
                        else
                            strProcedureIDs = strProcedureIDs + "," + oCriteria.ProblemTypes[i].ID;
                    }

                    //Dates                    
                    _FindDates = oCriteria.Dates;


                    #endregion


                    for (int l = 0; l < oProviderItems.Count; l++)
                    {
                        for (int m = 0; m <= _FindDates.Count - 1; m++)
                        {
                            Int64 nProviderID = Convert.ToInt64(oProviderItems[l].ID);

                            #region "Get Providers Appointments "

                            //---------------Providers Appointments and Blocks
                            strQuery = " SELECT nASBaseID AS nProviderID , dtStartDate, dtStartTime, dtEndDate, dtEndTime FROM AS_Appointment_DTL  WITH(NOLOCK) " +
                                       " WHERE (dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") " +
                                       " AND (dtEndDate = " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") " +
                                       " AND  (nASBaseID = " + oProviderItems[l].ID + ") AND (nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") " +
                                       " UNION " +
                                       " SELECT nASBaseID AS nProviderID , dtStartDate, dtStartTime, dtEndDate, dtEndTime FROM AS_Schedule_DTL  WITH(NOLOCK) " +
                                       " WHERE (dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") " +
                                       " AND (dtEndDate = " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") " +
                                       " AND  (nASBaseID = " + oProviderItems[l].ID + ") AND (nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") " +
                                       " ORDER BY dtStartDate, dtStartTime,dtEndTime ";


                            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            oDB.Connect(false);
                            DataTable dt;
                            oDB.Retrive_Query(strQuery, out dt);

                            if (strProcedureIDs.Trim() == "")  //Problem Type Not Selected
                            {
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    DataRow r;
                                    r = dtTemp.NewRow();

                                    r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
                                    r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
                                    r["Date"] = Convert.ToDateTime(_FindDates[m]);
                                    //r["Start Time"] = Convert.ToDateTime(Convert.ToDateTime(_FindDates[m]).ToShortDateString() + " " + Convert.ToDateTime(gloDateMaster.gloTime.  ).ToShortTimeString());
                                    //r["End Time"] = Convert.ToDateTime(Convert.ToDateTime(_FindDates[m]).ToShortDateString() + " " + Convert.ToDateTime(oAppointmentItems[k].Description).ToShortTimeString());

                                    r["Start Time"] = gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(_FindDates[m]), Convert.ToInt32(dt.Rows[k]["dtStartTime"]));
                                    r["End Time"] = gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(_FindDates[m]), Convert.ToInt32(dt.Rows[k]["dtEndTime"]));

                                    dtTemp.Rows.Add(r);
                                }
                            }

                            #endregion

                        } // for (int l = 0; l < oProviderItems.Count; l++)
                    } // for (int m = 0; m <= _FindDates.Count - 1; m++)

                } // try
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;   
                    return null;
                }

                return dtTemp;
            }

            public DataTable GetTemplateAppointments(String _ProviderIDs, ArrayList _FindDates, string _AppointmentTypes, String _sLocation, String _sDepartment)
            {
                DataTable dtAppointment = new DataTable();
                String _strFindDates = "";
                String strFlag = "Template";
               
                try
                {

                    for (int i = 0; i < _FindDates.Count; i++)
                    {
                        if (_strFindDates.Trim() == "")
                            _strFindDates = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(_FindDates[i])).ToString();
                        else
                            _strFindDates = _strFindDates + "," + gloDateMaster.gloDate.DateAsNumber(Convert.ToString(_FindDates[i])).ToString();
                    }

                     
                    if (_ProviderIDs != "")
                    {
                        dtAppointment = GetCalendarSearchOpenSlot(_sLocation, _sDepartment, _ProviderIDs, _AppointmentTypes, _strFindDates, strFlag);
                    }

                    return dtAppointment;
                } 
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;   
                    return null;
                }
                finally
                {
                    dtAppointment.Dispose ();
                    dtAppointment = null;
                    _strFindDates= null;
                    strFlag = null;
                }
            }


            private DataTable GetCalendarSearchOpenSlot(String _sLocation, String _sDepartment, String _strProviderIDs, String _strAppointmentTypes, String _strFindDates, String _strFlag)
            {
                DataTable dtAppointment = new DataTable();

                try
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                    oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@LocationName", _sLocation, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@DepartmentName", _sDepartment, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ProviderID", _strProviderIDs, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@AppointmentTypeDesc", _strAppointmentTypes, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@dtStartDate", _strFindDates, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@dtEndDate", _strFindDates, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@flag", _strFlag, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Connect(false);
                    oDB.Retrive("GetCalendarSearchOpenSlot", oDBParameters, out dtAppointment);

                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDBParameters = null;

                    oDB.Dispose();
                    oDB = null;

                    return dtAppointment;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    return null;
                }
                finally
                {
                    dtAppointment.Dispose();
                    dtAppointment = null;
                }
            }

            private gloItems SplitDuration(DateTime sTime, DateTime eTime, Int64 duration, gloItems oAppointmentItems)
            {
                gloGeneralItem.gloItems oItems = new gloGeneralItem.gloItems();                
                try
                {
                    //Converting the duration into ticks.
                    TimeSpan tsDuration = new TimeSpan(duration);
                    DateTime dtTemSTime = sTime;
                    DateTime dtTemETime = eTime;
                                       

                    for (int i = 0; dtTemETime <= eTime; i++)
                    {
                        gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();

                        //Make the 'Temp slot' of given duration
                        dtTemETime = dtTemSTime.AddMinutes(duration);
                        oItem.Code = dtTemSTime.ToString("hh:mm tt");
                        oItem.Description = dtTemETime.ToString("hh:mm tt");

                        for (int k = 0; k < oAppointmentItems.Count; k++)
                        {
                            if (dtTemSTime <= Convert.ToDateTime(oAppointmentItems[k].Code) && dtTemETime > Convert.ToDateTime(oAppointmentItems[k].Code))
                            {
                                oItem.Description = Convert.ToDateTime(oAppointmentItems[k].Code).ToString("hh:mm tt");
                                dtTemETime = Convert.ToDateTime(oAppointmentItems[k].Description);
                                //break;
                            }                           
                        }


                       
                        
                        if (oItem.Code != oItem.Description)
                        {
                            oItems.Add(oItem);
                        }
                        oItem.Dispose();
                        oItem = null;
                        // Initialize the Date Time for the Next Interval
                        dtTemSTime = dtTemETime;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return oItems;
            }

            #region UI Methods

            public void ShowSearchAppointment(System.Windows.Forms.Form oParentWindow)
            {
                frmSearchAppointment oSearchAppointment = new frmSearchAppointment();
                oSearchAppointment.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                oSearchAppointment.MdiParent = oParentWindow;
                oSearchAppointment.Show();
            }

            public void ShowDialogSearchAppointment()
            {
                frmSearchAppointment oSearchAppointment = new frmSearchAppointment();
                //oSearchAppointment.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                //oSearchAppointment.MdiParent = oParentWindow;
                oSearchAppointment.ShowInTaskbar = false;
                oSearchAppointment.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                oSearchAppointment.ShowDialog(oSearchAppointment.Parent);
                oSearchAppointment.Dispose();
                oSearchAppointment = null;
            }

            #endregion

            
        }


        public class AppointmentSearchCriteria : IDisposable
        {
            #region "Constructor & Distructor"

            
            public AppointmentSearchCriteria()
            {
                //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

            ~AppointmentSearchCriteria()
            {
                Dispose(false);
            }

            #endregion

            gloItems oProviders;
            gloItems oProblemTypes;
            gloItems oResources;
            string _sLocation = "";
            string _sDepartment = "";
            DateTime _dtStartDate;
            DateTime _dtEndDate;
            Int64 _nDuration = 0;
            Int64 _ClinicID = 0;
            ArrayList _Dates;
            DateTime _nClinicStartTime;
            DateTime _nClinicEndTime;
            bool _IsAdvanceSearch;
            bool _IsProblemTypeSearch;  //Problem Type == true, Resource == False 
            bool _IncludeRB;
            bool _ExcludeRB;
            bool _WholeRB;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _MessageBoxCaption = string.Empty;

            public bool  IncludeRBuuton
            {
                get { return _IncludeRB; }
                set { _IncludeRB = value; }
            }
            public bool ExcludeRBuuton
            {
                get { return _ExcludeRB; }
                set { _ExcludeRB = value; }
            }
            public bool WholeRBuuton
            {
                get { return _WholeRB; }
                set { _WholeRB = value; }
            }
            public bool IsAdvanceSearch
            {
                get { return _IsAdvanceSearch; }
                set { _IsAdvanceSearch = value; }
            }


            public bool IsProblemTypeSearch
            {
                get { return _IsProblemTypeSearch; }
                set { _IsProblemTypeSearch = value; }
            }

            public DateTime ClinicStartTime
            {
                get { return _nClinicStartTime; }
                set { _nClinicStartTime = value; }
            }
            public DateTime ClinicEndTime
            {
                get { return _nClinicEndTime; }
                set { _nClinicEndTime = value; }
            }
            public gloItems Providers
            {
                get { return oProviders; }
                set { oProviders = value; }
            }

            public gloItems ProblemTypes
            {
                get { return oProblemTypes; }
                set { oProblemTypes = value; }
            }

            public gloItems Resources
            {
                get { return oResources; }
                set { oResources = value; }
            }

            public string  Location
            {
                get { return _sLocation; }
                set { _sLocation  = value; }
            }

            public string Department
            {
                get { return _sDepartment; }
                set { _sDepartment = value; }
            }

            public DateTime StartDate
            {
                get { return _dtStartDate; }
                set { _dtStartDate = value; }
            }

            public DateTime EndDate
            {
                get { return _dtEndDate; }
                set { _dtEndDate = value; }
            }

            public Int64 Duration
            {
                get { return _nDuration; }
                set { _nDuration = value; }
            }

            public Int64 ClinicId
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            public ArrayList Dates
            {
                get { return _Dates; }
                set { _Dates = value; }
            }


        }

        //public class gloSearching_Old : IDisposable
        //{
        //    #region "Constructor & Distructor"

        //    private string _databaseconnectionstring = "";
            
        //    gloGeneralItem.gloItems oAppointmentItems = new gloGeneralItem.gloItems();//added sandip darade 30072008 

        //    public gloSearching_Old(string DatabaseConnectionString)
        //    {
        //        _databaseconnectionstring = DatabaseConnectionString;
        //    }

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
        //                if (oAppointmentItems != null)
        //                {
        //                    oAppointmentItems.Clear();
        //                    oAppointmentItems.Dispose();
        //                    oAppointmentItems = null;
        //                }
        //            }
        //        }
        //        disposed = true;
        //    }

        //    ~gloSearching_Old()
        //    {
        //        Dispose(false);
        //    }

        //    #endregion


        //    public DataTable SearchAppointments(AppointmentSearchCriteria oCriteria)
        //    {
        //        DataTable dtTemp = new DataTable();
        //        try
        //        {

        //            String strQuery = "";
        //            String strResourcesIDs = "";
        //            String strProviderIDs = "";
        //            String strProcedureIDs = "";

        //            DateTime dtClinicStartTime = oCriteria.ClinicStartTime;
        //            DateTime dtClinicEndTime = oCriteria.ClinicEndTime;

        //            // Add Columns to the DataTable to which we need to apply the Filte               

        //            dtTemp.Columns.Add("Select");
        //            dtTemp.Columns.Add("ProviderID");
        //            dtTemp.Columns.Add("Provider Name");
        //            dtTemp.Columns.Add("Date");
        //            dtTemp.Columns.Add("Start Time");
        //            dtTemp.Columns.Add("End Time");
        //            dtTemp.Columns.Add("ProblemID");
        //            dtTemp.Columns.Add("ProblemType");

        //            // Clear The Calender Control 
        //            //juc_Calender.Appointments.Clear();

        //            #region " Get The Filter Criteria For Appointment "

        //            // Add Checked ResourceIDs in String 

        //            for (int i = 0; i < oCriteria.Resources.Count; i++)
        //            {
        //                if (strResourcesIDs.Trim() == "")
        //                    strResourcesIDs = Convert.ToString(oCriteria.Resources[i].ID);
        //                else
        //                    strResourcesIDs = strResourcesIDs + "," + oCriteria.Resources[i].ID;
        //            }

        //            for (int i = 0; i < oCriteria.ProblemTypes.Count; i++)
        //            {
        //                if (strProcedureIDs.Trim() == "")
        //                    strProcedureIDs = Convert.ToString(oCriteria.ProblemTypes[i].ID);
        //                else
        //                    strProcedureIDs = strProcedureIDs + "," + oCriteria.ProblemTypes[i].ID;
        //            }



        //            // Set The Date Criteria
        //            // Fill the Dates in Array List 

        //            ArrayList _FindDates = new ArrayList();
        //            //Int32 dNoofDays;
        //            //TimeSpan oTimeSpan = oCriteria.EndDate.Subtract(oCriteria.StartDate);

        //            //dNoofDays = Convert.ToInt32(oTimeSpan.TotalDays);
        //            //for (int k = 0; k <= dNoofDays; k++)
        //            //{
        //            //    _FindDates.Add(oCriteria.StartDate.AddDays(k));
        //            //}
        //            //dNoofDays = 0;

        //            _FindDates = oCriteria.Dates;


        //            gloItems oProviderItems = oCriteria.Providers;

        //            if (oProviderItems.Count > 0)
        //            {
        //                for (int i = 0; i < oProviderItems.Count; i++)
        //                {
        //                    if (strProviderIDs.Trim() == "")
        //                        strProviderIDs = Convert.ToString(oProviderItems[i].ID);
        //                    else
        //                        strProviderIDs = strProviderIDs + "," + oProviderItems[i].ID;

        //                }
        //            }
        //            else
        //            {
        //                // If Providers are not Selected in ComboBox then Select all Providers
        //                gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //                DataTable dt = oResource.GetProviders();
        //                oResource.Dispose();
        //                oResource = null;
        //                //nProviderID, sFirstName, sMiddleName, sLastName, sGender, sDEA, sAddress, sStreet, sCity, sState, sZIP, sPhoneNo, sFAX, sMobileNo, sPagerNo, sEmail, sURL, imgSignature, nProviderType, sNPI, sUPIN, sMedicalLicenseNo, sPrefix, sExternalCode , ProviderName
        //                if (dt != null)
        //                {
        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        gloGeneralItem.gloItem oProviderItem = new gloGeneralItem.gloItem();

        //                        oProviderItem.ID = Convert.ToInt64(dt.Rows[i]["nProviderID"]);
        //                        oProviderItem.Description = dt.Rows[i]["ProviderName"].ToString(); // Provider Name
        //                        oProviderItems.Add(oProviderItem);

        //                        if (strProviderIDs.Trim() == "")
        //                            strProviderIDs = Convert.ToString(oProviderItem.ID);
        //                        else
        //                            strProviderIDs = strProviderIDs + "," + oProviderItem.ID;
        //                        oProviderItem.Dispose();
        //                        oProviderItem = null;
        //                    }
        //                    dt.Dispose();
        //                    dt = null;
        //                }

        //            }

        //            #endregion

        //            //for (int m = 0; m <= _FindDates.Count - 1; m++)
        //            for (int l = 0; l < oProviderItems.Count; l++)
        //            {
        //                //for (int l = 0; l < oProviderItems.Count; l++)
        //                for (int m = 0; m <= _FindDates.Count - 1; m++)
        //                {
        //                    Int64 nProviderID = Convert.ToInt64(oProviderItems[l].ID);

        //                    #region Get Schedules for Provider
        //                    // Search Schedule
        //                    // for the Given Date i.e for _FindDates[m] & The Selected Provider , Problem type & Resources
        //                    string strScheduleQuery = "";
        //                    //20080811 //strScheduleQuery = " SELECT DISTINCT  AS_Schedule_DTL.nStartDate, AS_Schedule_DTL.nStartTime, AS_Schedule_DTL.nEndDate, AS_Schedule_DTL.nEndTime,  AS_Schedule_DTL_Procedures.nProcedureID, AB_AppointmentType.sAppointmentType " +
        //                    //               "FROM         AS_Schedule_DTL_Resources RIGHT OUTER JOIN AS_Schedule_DTL INNER JOIN " +
        //                    //               " AB_Location ON AS_Schedule_DTL.nLocationID = AB_Location.nLocationID LEFT OUTER JOIN " +
        //                    //               "AB_Resource_Provider INNER JOIN AS_Schedule_DTL_Providers ON AB_Resource_Provider.nProviderID = AS_Schedule_DTL_Providers.nProviderID ON " +
        //                    //               "AS_Schedule_DTL.nMasterScheduleID = AS_Schedule_DTL_Providers.nMasterScheduleID ON  " +
        //                    //               "AS_Schedule_DTL_Resources.nMasterScheduleID = AS_Schedule_DTL.nMasterScheduleID LEFT OUTER JOIN " +
        //                    //               "AB_AppointmentType INNER JOIN  " +
        //                    //               "AS_Schedule_DTL_Procedures ON AB_AppointmentType.nAppointmentTypeID = AS_Schedule_DTL_Procedures.nProcedureID ON  " +
        //                    //               "AS_Schedule_DTL.nMasterScheduleID = AS_Schedule_DTL_Procedures.nMasterScheduleID  " +
        //                    //               " WHERE     (AS_Schedule_DTL.nStartDate >= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") AND (AS_Schedule_DTL.nEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") AND ";
        //                    //strScheduleQuery = " SELECT nPRUID,nPRUFlag,dtStartDate,dtStartTime,dtEndDate,dtEndTime,sASBaseCode,sASBaseDesc FROM AS_Schedule_DTL" +
        //                    //                " WHERE (dtStartDate=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + "  )AND (dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ")AND(nbasePRUID IN (" + oProviderItems[l].ID + "))AND(nBasePRUFlag =" + ScheduleBlockType.ProviderSchedule.GetHashCode() + ") AND ";
        //                    strScheduleQuery = "SELECT dtStartDate,dtStartTime,dtEndDate,dtEndTime,sASBaseCode,sASBaseDesc,nASBaseFlag,nASBaseID FROM  AS_Schedule_DTL WITH(NOLOCK) " +
        //                                      " WHERE (dtStartDate=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + "  )" +
        //                                      "AND (dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ")AND(nRefID IN (" + oProviderItems[l].ID + "))AND(nRefFlag =" + gloAppointmentScheduling.AppointmentScheduleFlag.ProviderSchedule.GetHashCode() + ") AND "; // 20080819
        //                    if (strProcedureIDs.Trim() == "")
        //                    {
        //                        if (strProviderIDs.Trim() != "")
        //                        {
        //                            //20080811  // strScheduleQuery = strScheduleQuery + " (AB_Resource_Provider.nProviderID IN (" + oProviderItems[l].ID + "))";
        //                            //strScheduleQuery = strScheduleQuery + " (nPRUFlag  IN (" + oProviderItems[l].ID + "))";
        //                            strScheduleQuery = strScheduleQuery + "nASBaseFlag  IN (" + ASBaseType.ProblemType.GetHashCode() + ")";
        //                        }


        //                    }
        //                    else
        //                    {
        //                        if (strProviderIDs.Trim() != "")
        //                        {
        //                            //20080811    // strScheduleQuery = strScheduleQuery + " (AB_Resource_Provider.nProviderID IN (" + oProviderItems[l].ID + ")) AND";
        //                            // strScheduleQuery = strScheduleQuery + "nPRUFlag  IN (" + SchedulePRUType.ProblemType.GetHashCode() + ")";
        //                            strScheduleQuery = strScheduleQuery + "nASBaseFlag  IN (" + ASBaseType.ProblemType.GetHashCode() + ")";
        //                        }
        //                        if (strProcedureIDs.Trim() != "")
        //                        {
        //                            //strScheduleQuery = strScheduleQuery + " (AS_Schedule_DTL_Procedures.nProcedureID IN (" + strProcedureIDs + "))";
        //                            //strScheduleQuery = strScheduleQuery + "  AND (nPRUID IN (" + strProcedureIDs + "))";
        //                            strScheduleQuery = strScheduleQuery + "  AND (nASBaseID IN (" + strProcedureIDs + "))";

        //                        }


        //                    }
        //                    //strScheduleQuery = strScheduleQuery.Trim();
        //                    //strScheduleQuery = strScheduleQuery.Remove((strScheduleQuery.Trim().Length - 3), 3);

        //                    //20080811 // strScheduleQuery = strScheduleQuery + " ORDER BY AS_Schedule_DTL.StartTime, AS_Schedule_DTL.nEndTime ";
        //                    strScheduleQuery = strScheduleQuery + " ORDER BY dtStartTime,dtEndTime ";

        //                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //                    oDB.Connect(false);
        //                    DataTable dtSchedule;
        //                    oDB.Retrive_Query(strScheduleQuery, out dtSchedule);
        //                    #endregion
        //                    #region Retrievimg Resourceschedule 20080811
        //                    string strResourceQuery = "";
        //                    strResourceQuery = "SELECT dtStartDate,dtStartTime,dtEndDate,dtEndTime,sASBaseCode,sASBaseDesc,nASBaseFlag,nASBaseID FROM  AS_Schedule_DTL  WITH(NOLOCK) " +
        //                                      " WHERE (dtStartDate=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + "  )" +
        //                                      "AND (dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") AND (nASBaseFlag= " + ASBaseType.ProblemType.GetHashCode() + ")"; // 20080819
        //                    if (strResourcesIDs.Trim() != "")//  if resource added 20080818 
        //                    {

        //                        strResourceQuery = strResourceQuery + "  AND (nASBaseID IN (" + strResourcesIDs + "))";

        //                    }
        //                    strResourceQuery = strResourceQuery + " ORDER BY dtStartTime,dtEndTime ";
        //                    oDB.Connect(false);
        //                    DataTable dtResource;
        //                    oDB.Retrive_Query(strResourceQuery, out dtResource);
        //                    #endregion
        //                    #region "Get Providers Appointments "
        //                    //strQuery = " SELECT DISTINCT AS_Appointment_DTL.nStartDate, AS_Appointment_DTL.nStartTime, AS_Appointment_DTL.nEndDate, AS_Appointment_DTL.nEndTime,  "
        //                    //+ " AS_Appointment_DTL.sNotes, AB_Location.sLocation, AB_Department.sDepartment, AS_Appointment_DTL_Providers.nProviderID AS nProviderID, (ISNULL(AB_Resource_Provider.sFirstName,'') +SPACE(1)+ ISNULL(AB_Resource_Provider.sMiddleName,'') +SPACE(1)+ ISNULL(AB_Resource_Provider.sLastName,'')) ProviderName "
        //                    //+ " FROM	AS_Appointment_DTL_Procedures RIGHT OUTER JOIN AS_Appointment_DTL_Resources RIGHT OUTER JOIN AB_Department "
        //                    //+ " INNER JOIN AS_Appointment_DTL ON AB_Department.nDepartmentID = AS_Appointment_DTL.nDepartmentID "
        //                    //+ " INNER JOIN AB_Location ON AB_Department.nLocationID = AB_Location.nLocationID ON AS_Appointment_DTL_Resources.nMasterAppointmentID = AS_Appointment_DTL.nMasterAppointmentID AND AS_Appointment_DTL_Resources.nAppointmentID = AS_Appointment_DTL.nAppointmentID "
        //                    //+ " LEFT OUTER JOIN AS_Appointment_DTL_Providers "
        //                    //+ " RIGHT OUTER JOIN AB_Resource_Provider ON AS_Appointment_DTL_Providers.nProviderID = AB_Resource_Provider.nProviderID ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Providers.nMasterAppointmentID AND "
        //                    //+ " AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Providers.nAppointmentID "
        //                    //+ " LEFT OUTER JOIN AS_Appointment_DTL_RefDoctors ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_RefDoctors.nMasterAppointmentID AND "
        //                    //+ " AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_RefDoctors.nAppointmentID ON AS_Appointment_DTL_Procedures.nMasterAppointmentID = AS_Appointment_DTL.nMasterAppointmentID AND "
        //                    //+ " AS_Appointment_DTL_Procedures.nAppointmentID = AS_Appointment_DTL.nAppointmentID "
        //                    //+ " LEFT OUTER JOIN AS_Appointment_DTL_Coverages ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Coverages.nMasterAppointmentID AND AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Coverages.nAppointmentID "
        //                    //+ " LEFT OUTER JOIN  AB_AppointmentType ON AS_Appointment_DTL.nAppointmentTypeID = AB_AppointmentType.nAppointmentTypeID "
        //                    //    //  + " WHERE AS_Appointment_DTL.nStartDate >=" + gloDateMaster.gloDate.DateAsNumber(tempStartDate.Date.ToString()) + " AND  AS_Appointment_DTL.nEndDate <= " + gloDateMaster.gloDate.DateAsNumber(tempEndDate.Date.ToString()) + " AND "
        //                    //+ " WHERE AS_Appointment_DTL.nStartDate >=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + " AND   AS_Appointment_DTL.nEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + " AND "
        //                    //+ "(AS_Appointment_DTL_Providers.nProviderID = " + nProviderID + " OR";

        //                    strQuery = "select dtStartDate,dtEndDate,dtStartTime,dtEndTime,nAppointmentFlag,nASBaseFlag,sASBaseDesc,sASBaseCode from AS_Appointment_DTL  WITH(NOLOCK) " +
        //                        " where dtStartDate>=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + " and dtEndDate=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + " and (nRefID IN (" + oProviderItems[l].ID + "))  ";
        //                    // If Resources are Selected then Apply filter for Resources
        //                    if (strResourcesIDs.Trim() != "")
        //                    {
        //                        //strQuery = strQuery + " AS_Appointment_DTL_Resources.nResourceID IN( " + strResourcesIDs + "))";
        //                        strQuery = strQuery + " AND  nASBaseID IN (" + strResourcesIDs + ")";
        //                    }
        //                    else
        //                    {
        //                        strQuery = strQuery.Trim();
        //                        //strQuery = strQuery.Remove((strQuery.Trim().Length - 3), 3);
        //                        //strQuery += ")";
        //                    }

        //               //     strQuery = strQuery;
        //                    strQuery = strQuery + " ORDER BY dtStartDate, dtStartTime,dtEndTime";


        //                    // 
        //                    //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //                    oDB.Connect(false);
        //                    DataTable dt;
        //                    oDB.Retrive_Query(strQuery, out dt);

        //                    gloGeneralItem.gloItems oAppointmentItems = new gloGeneralItem.gloItems();
        //                    if (dt != null)
        //                    {
        //                        if (dt.Rows.Count > 0)
        //                        {
        //                            for (int i = 0; i < dt.Rows.Count; i++)
        //                            {
        //                                gloGeneralItem.gloItem oAppointmentItem = new gloGeneralItem.gloItem();
        //                                oAppointmentItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nStartTime"])).ToShortTimeString();
        //                                oAppointmentItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nEndTime"])).ToShortTimeString();

        //                                oAppointmentItems.Add(oAppointmentItem);
        //                                oAppointmentItem.Dispose();
        //                                oAppointmentItem = null;

        //                            } // for (int i = 0; i < dt.Rows.Count; i++)
        //                        }// if (dt.Rows.Count > 0)
        //                        dt.Dispose();
        //                        dt = null;
        //                    } //  if (dt != null)
        //                    #endregion
        //                    #region Considering Resources
        //                    //To exclude Resource Schedule from the search 
        //                    if (strResourcesIDs.Trim() != "")
        //                    {
        //                        if (oCriteria.ExcludeRBuuton == true)
        //                        {
        //                            if (dtResource != null)
        //                            {
        //                                if (dtResource.Rows.Count > 0)//if resource schedule present 
        //                                {
        //                                    for (int j = 0; j < dtResource.Rows.Count; j++)
        //                                    {
        //                                        gloGeneralItem.gloItem oAppointmentItem = new gloGeneralItem.gloItem();
        //                                        oAppointmentItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtResource.Rows[j]["dtStartTime"])).ToShortTimeString();
        //                                        oAppointmentItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtResource.Rows[j]["dtEndTime"])).ToShortTimeString();
        //                                        oAppointmentItems.Add(oAppointmentItem);
        //                                        oAppointmentItem.Dispose();
        //                                        oAppointmentItem = null;

        //                                    }

        //                                }
        //                            }
        //                        }



        //                        // Find The time slots within which we have to search

        //                        if (oCriteria.IncludeRBuuton == true)
        //                        {
        //                            gloGeneralItem.gloItems oScheduleItems = new gloGeneralItem.gloItems();
        //                            Int32 nSTime = gloDateMaster.gloTime.TimeAsNumber(dtClinicStartTime.ToShortTimeString());
        //                            Int32 nETime = gloDateMaster.gloTime.TimeAsNumber(dtClinicEndTime.ToShortTimeString());

        //                            for (int i = 0; i < dtResource.Rows.Count; i++)
        //                            {
        //                                nSTime = Convert.ToInt32(dtResource.Rows[i]["dtStartTime"]);
        //                                nETime = Convert.ToInt32(dtResource.Rows[i]["dtEndTime"]);

        //                                for (int k = i; k < dtResource.Rows.Count; k++)
        //                                {
        //                                    Int32 nTempSTime = Convert.ToInt32(dtResource.Rows[k]["dtStartTime"]);
        //                                    Int32 nTempETime = Convert.ToInt32(dtResource.Rows[k]["dtEndTime"]);
        //                                    if (nSTime <= nTempSTime && nETime >= nTempSTime)
        //                                    {
        //                                        if (nETime <= nTempETime)
        //                                        {
        //                                            nETime = nTempETime;
        //                                            i = k;
        //                                        }
        //                                        else
        //                                        {
        //                                            i = k;
        //                                        }
        //                                    }
        //                                }

        //                                gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
        //                                oItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, nSTime).ToShortTimeString();
        //                                oItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, nETime).ToShortTimeString();

        //                                oScheduleItems.Add(oItem);
        //                                oItem.Dispose();
        //                                oItem = null;
        //                            }



        //                            for (int n = 0; n < oScheduleItems.Count; n++)
        //                            {
        //                                //DateTime dtScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[n]["dtStartTime"]));
        //                                //DateTime dtScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[n]["dtEndTime"]));

        //                                DateTime dtScheduleStartTime = Convert.ToDateTime(oScheduleItems[n].Code);
        //                                DateTime dtScheduleEndTime = Convert.ToDateTime(oScheduleItems[n].Description);


        //                                if (dtScheduleStartTime < dtClinicStartTime && dtScheduleEndTime <= dtClinicStartTime)
        //                                {
        //                                    continue;
        //                                }
        //                                if (dtScheduleStartTime >= dtClinicEndTime && dtScheduleEndTime > dtClinicEndTime)
        //                                {
        //                                    continue;
        //                                }

        //                                if (dtScheduleStartTime < dtClinicStartTime)
        //                                {
        //                                    dtScheduleStartTime = dtClinicStartTime;
        //                                }
        //                                if (dtScheduleEndTime > dtClinicEndTime)
        //                                {
        //                                    dtScheduleEndTime = dtClinicEndTime;
        //                                }

        //                                gloGeneralItem.gloItems oFreeSlotItems = new gloGeneralItem.gloItems();
        //                                oFreeSlotItems = SplitDuration(dtScheduleStartTime, dtScheduleEndTime, Convert.ToInt64(oCriteria.Duration), oAppointmentItems);


        //                            }
        //                        }


        //                    }
        //                    #endregion

        //                    #region Search Free Slots Without Problem Type

        //                    if (strProcedureIDs.Trim() == "")  //Problem Type Not Selected
        //                    {
        //                        gloGeneralItem.gloItems oFreeSlotItems = new gloGeneralItem.gloItems();
        //                        oFreeSlotItems = SplitDuration(dtClinicStartTime, dtClinicEndTime, Convert.ToInt64(oCriteria.Duration), oAppointmentItems);//for _AMPm                                

        //                        #region Attach ScheduleName/ProblemType to FreeSlot

        //                        //Add Schedule name & ID to free slots which have same time as schedule
        //                        for (int i = 0; i < dtSchedule.Rows.Count; i++)
        //                        {
        //                            DateTime StartTime = dtClinicStartTime;
        //                            DateTime EndTime = dtClinicEndTime;
        //                            string ScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["dtStartTime"])).ToShortTimeString();
        //                            string ScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["dtEndTime"])).ToShortTimeString();

        //                            // If Schedule is not between Clinic Start & End Time 
        //                            // then do not attach Schedule name to any free slot continue with next schedule 
        //                            if (Convert.ToDateTime(ScheduleStartTime) < dtClinicStartTime && Convert.ToDateTime(ScheduleEndTime) <= dtClinicStartTime)
        //                            {
        //                                continue;
        //                            }
        //                            if (Convert.ToDateTime(ScheduleStartTime) >= dtClinicEndTime && Convert.ToDateTime(ScheduleEndTime) > dtClinicEndTime)
        //                            {
        //                                continue;
        //                            }


        //                            for (int x = 0; x < oFreeSlotItems.Count; x++)
        //                            {
        //                                if (Convert.ToDateTime(oFreeSlotItems[x].Code) <= Convert.ToDateTime(ScheduleStartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) > Convert.ToDateTime(ScheduleStartTime))
        //                                {
        //                                    StartTime = Convert.ToDateTime(oFreeSlotItems[x].Code);
        //                                }

        //                                if (Convert.ToDateTime(oFreeSlotItems[x].Code) < Convert.ToDateTime(ScheduleEndTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) >= Convert.ToDateTime(ScheduleEndTime))
        //                                {
        //                                    EndTime = Convert.ToDateTime(oFreeSlotItems[x].Description);
        //                                }
        //                            }
        //                            if (StartTime <= dtClinicStartTime)
        //                            {
        //                                StartTime = Convert.ToDateTime(ScheduleStartTime);
        //                            }

        //                            if (EndTime >= dtClinicEndTime)
        //                            {
        //                                EndTime = Convert.ToDateTime(ScheduleEndTime);
        //                            }

        //                            for (int x = oFreeSlotItems.Count - 1; x >= 0; x--)
        //                            {

        //                                if (Convert.ToDateTime(oFreeSlotItems[x].Code) >= Convert.ToDateTime(StartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) <= Convert.ToDateTime(EndTime))
        //                                {
        //                                    gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();
        //                                    oSubItem.Code = Convert.ToString(dtSchedule.Rows[i]["sASBaseCode"]);
        //                                    oSubItem.Description = Convert.ToString(dtSchedule.Rows[i]["sASBaseDesc"]);
        //                                    oFreeSlotItems[x].SubItems.Add(oSubItem);
        //                                    oSubItem.Dispose();
        //                                    oSubItem = null;
        //                                }
        //                            }

        //                        }

        //                        #endregion

        //                        #region Code To Display Appointment Slot Comented
        //                        //for (int k = 0; k < oFreeSlotItems.Count; k++)
        //                        //{
        //                        //    gloGeneralItem.gloItem oItemTime = new gloGeneralItem.gloItem();
        //                        //    oItemTime = oFreeSlotItems[k];
        //                        //    // if Appointment is  before first freeslot 
        //                        //    if (k == 0)
        //                        //    {
        //                        //        if (Convert.ToDateTime(oFreeSlotItems[k].Code) > oCriteria.ClinicStartTime && Convert.ToDateTime(oFreeSlotItems[k].Description) <= Convert.ToDateTime(oFreeSlotItems[k + 1].Code))
        //                        //        {
        //                        //            DataRow Fslot;
        //                        //            Fslot = dtTemp.NewRow();

        //                        //            Fslot["Select"] = Convert.ToBoolean(1);
        //                        //            Fslot["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //                        //            Fslot["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //                        //            Fslot["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //                        //            Fslot["Start Time"] = oCriteria.ClinicStartTime.ToString("hh:mm tt");
        //                        //            Fslot["End Time"] = oFreeSlotItems[k].Code;
        //                        //            dtTemp.Rows.Add(Fslot);
        //                        //        }
        //                        //    }

        //                        //    DataRow r;
        //                        //    r = dtTemp.NewRow();
        //                        //    //Column[0]= "ProviderID"
        //                        //    //Column[1]= "Provider Name"
        //                        //    //Column[2]= "Date"
        //                        //    //Column[3]= "Start Time"
        //                        //    //Column[4]= "End Time"
        //                        //    r["Select"] = Convert.ToBoolean(0);
        //                        //    r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //                        //    r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //                        //    r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //                        //    r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
        //                        //    r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
        //                        //    for (int i = 0; i < oItemTime.SubItems.Count; i++)
        //                        //    {
        //                        //        r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
        //                        //        r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
        //                        //    }
        //                        //    // Remove last coma ","
        //                        //    if (Convert.ToString(r["ProblemType"]).Length > 0)
        //                        //    {
        //                        //        r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
        //                        //    }
        //                        //    dtTemp.Rows.Add(r);

        //                        //    if (k < oFreeSlotItems.Count - 2)
        //                        //    {

        //                        //        if (Convert.ToDateTime(oFreeSlotItems[k].Description) != Convert.ToDateTime(oFreeSlotItems[k + 1].Code))
        //                        //        {

        //                        //            DataRow rBlocked;
        //                        //            rBlocked = dtTemp.NewRow();

        //                        //            rBlocked["Select"] = Convert.ToBoolean(1);
        //                        //            rBlocked["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //                        //            rBlocked["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //                        //            rBlocked["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //                        //            rBlocked["Start Time"] = oFreeSlotItems[k].Description;
        //                        //            rBlocked["End Time"] = oFreeSlotItems[k + 1].Code;
        //                        //            dtTemp.Rows.Add(rBlocked);

        //                        //        }
        //                        //    }

        //                        //} 
        //                        #endregion

        //                        for (int k = 0; k < oFreeSlotItems.Count; k++)
        //                        {
        //                            gloGeneralItem.gloItem oItemTime = oFreeSlotItems[k];

        //                            DataRow r;
        //                            r = dtTemp.NewRow();
        //                            //Column[0]= "ProviderID"
        //                            //Column[1]= "Provider Name"
        //                            //Column[2]= "Date"
        //                            //Column[3]= "Start Time"
        //                            //Column[4]= "End Time"
        //                            r["Select"] = Convert.ToBoolean(0);
        //                            r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //                            r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //                            r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //                            r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
        //                            r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
        //                            for (int i = 0; i < oItemTime.SubItems.Count; i++)
        //                            {
        //                                r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
        //                                r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
        //                            }
        //                            // Remove last coma ","
        //                            if (Convert.ToString(r["ProblemType"]).Length > 0)
        //                            {
        //                                r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
        //                            }

        //                            dtTemp.Rows.Add(r);
        //                        } //  for (int k = 0; k < oFreeSlotItems.Count; k++)

        //                    }
        //                    #endregion Search Free Slots Without Problem Type

        //                    #region Search Free Slots in Problem Type
        //                    //If Shedule Present for selected problem type
        //                    //change clinic start & end time by schedule start and end time
        //                    //i.e : Search only in Schedule time 
        //                    if (strProcedureIDs.Trim() != "")
        //                    {

        //                        #region Find The time slots within which we have to search


        //                        gloGeneralItem.gloItems oScheduleItems = new gloGeneralItem.gloItems();
        //                        Int32 nSTime = gloDateMaster.gloTime.TimeAsNumber(dtClinicStartTime.ToShortTimeString());
        //                        Int32 nETime = gloDateMaster.gloTime.TimeAsNumber(dtClinicEndTime.ToShortTimeString());

        //                        for (int i = 0; i < dtSchedule.Rows.Count; i++)
        //                        {
        //                            nSTime = Convert.ToInt32(dtSchedule.Rows[i]["dtStartTime"]);
        //                            nETime = Convert.ToInt32(dtSchedule.Rows[i]["dtEndTime"]);

        //                            for (int k = i; k < dtSchedule.Rows.Count; k++)
        //                            {
        //                                Int32 nTempSTime = Convert.ToInt32(dtSchedule.Rows[k]["dtStartTime"]);
        //                                Int32 nTempETime = Convert.ToInt32(dtSchedule.Rows[k]["dtEndTime"]);
        //                                if (nSTime <= nTempSTime && nETime >= nTempSTime)
        //                                {
        //                                    if (nETime <= nTempETime)
        //                                    {
        //                                        nETime = nTempETime;
        //                                        i = k;
        //                                    }
        //                                    else
        //                                    {
        //                                        i = k;
        //                                    }
        //                                }
        //                            }

        //                            gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();
        //                            oItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, nSTime).ToShortTimeString();
        //                            oItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, nETime).ToShortTimeString();

        //                            oScheduleItems.Add(oItem);
        //                            oItem.Dispose();
        //                            oItem = null;
        //                        }

        //                        #endregion

        //                        for (int n = 0; n < oScheduleItems.Count; n++)
        //                        {
        //                            //DateTime dtScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[n]["dtStartTime"]));
        //                            //DateTime dtScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[n]["dtEndTime"]));

        //                            DateTime dtScheduleStartTime = Convert.ToDateTime(oScheduleItems[n].Code);
        //                            DateTime dtScheduleEndTime = Convert.ToDateTime(oScheduleItems[n].Description);


        //                            if (dtScheduleStartTime < dtClinicStartTime && dtScheduleEndTime <= dtClinicStartTime)
        //                            {
        //                                continue;
        //                            }
        //                            if (dtScheduleStartTime >= dtClinicEndTime && dtScheduleEndTime > dtClinicEndTime)
        //                            {
        //                                continue;
        //                            }

        //                            if (dtScheduleStartTime < dtClinicStartTime)
        //                                dtScheduleStartTime = dtClinicStartTime;
        //                            if (dtScheduleEndTime > dtClinicEndTime)
        //                                dtScheduleEndTime = dtClinicEndTime;

        //                            gloGeneralItem.gloItems oFreeSlotItems = new gloGeneralItem.gloItems();
        //                            oFreeSlotItems = SplitDuration(dtScheduleStartTime, dtScheduleEndTime, Convert.ToInt64(oCriteria.Duration), oAppointmentItems);

        //                            #region Attach ScheduleName/ProblemType to FreeSlot

        //                            //Add Schedule name & ID to free slots which has same time as schedule
        //                            for (int i = 0; i < dtSchedule.Rows.Count; i++)
        //                            {
        //                                DateTime StartTime = dtClinicStartTime;
        //                                DateTime EndTime = dtClinicEndTime;
        //                                string ScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["dtStartTime"])).ToShortTimeString();
        //                                string ScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["dtEndTime"])).ToShortTimeString();

        //                                // If Schedule is not between Clinic Start & End Time 
        //                                // then do not attach Schedule name to any free slot continue with next schedule 
        //                                if (Convert.ToDateTime(ScheduleStartTime) < dtClinicStartTime && Convert.ToDateTime(ScheduleEndTime) <= dtClinicStartTime)
        //                                {
        //                                    continue;
        //                                }
        //                                if (Convert.ToDateTime(ScheduleStartTime) >= dtClinicEndTime && Convert.ToDateTime(ScheduleEndTime) > dtClinicEndTime)
        //                                {
        //                                    continue;
        //                                }

        //                                for (int x = 0; x < oFreeSlotItems.Count; x++)
        //                                {
        //                                    if (Convert.ToDateTime(oFreeSlotItems[x].Code) <= Convert.ToDateTime(ScheduleStartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) > Convert.ToDateTime(ScheduleStartTime))
        //                                    {
        //                                        StartTime = Convert.ToDateTime(oFreeSlotItems[x].Code);
        //                                    }

        //                                    if (Convert.ToDateTime(oFreeSlotItems[x].Code) < Convert.ToDateTime(ScheduleEndTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) >= Convert.ToDateTime(ScheduleEndTime))
        //                                    {
        //                                        EndTime = Convert.ToDateTime(oFreeSlotItems[x].Description);
        //                                    }
        //                                }
        //                                if (StartTime <= dtClinicStartTime)
        //                                {
        //                                    StartTime = Convert.ToDateTime(ScheduleStartTime);
        //                                }

        //                                if (EndTime >= dtClinicEndTime)
        //                                {
        //                                    EndTime = Convert.ToDateTime(ScheduleEndTime);
        //                                }

        //                                for (int x = oFreeSlotItems.Count - 1; x >= 0; x--)
        //                                {
        //                                    if (Convert.ToDateTime(oFreeSlotItems[x].Code) >= Convert.ToDateTime(StartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) <= Convert.ToDateTime(EndTime))
        //                                    {
        //                                        gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();
        //                                        oSubItem.Code = Convert.ToString(dtSchedule.Rows[i]["sASBaseCode"]);
        //                                        oSubItem.Description = Convert.ToString(dtSchedule.Rows[i]["sASBaseDesc"]);

        //                                        // If Problem Type is alredy attached to freeslot do not attach again 
        //                                        bool _IsProblemTypeAttached = false;
        //                                        for (int y = 0; y < oFreeSlotItems[x].SubItems.Count; y++)
        //                                        {
        //                                            if (oSubItem.Code == oFreeSlotItems[x].SubItems[y].Code)
        //                                                _IsProblemTypeAttached = true;
        //                                        }
        //                                        if (_IsProblemTypeAttached == false)
        //                                            oFreeSlotItems[x].SubItems.Add(oSubItem);
        //                                        oSubItem.Dispose();
        //                                        oSubItem = null;
        //                                        //----------------------------------------------------------------

        //                                    }
        //                                }

        //                            }
        //                            #endregion

        //                            #region Code To Display Appointment Slot Comented
        //                            //for (int k = 0; k < oFreeSlotItems.Count; k++)
        //                            //{
        //                            //    gloGeneralItem.gloItem oItemTime = new gloGeneralItem.gloItem();
        //                            //    oItemTime = oFreeSlotItems[k];
        //                            //    // if Appointment is  before first freeslot 
        //                            //    if (k == 0)
        //                            //    {
        //                            //        if (Convert.ToDateTime(oFreeSlotItems[k].Code) == oCriteria.ClinicStartTime && Convert.ToDateTime(oFreeSlotItems[k].Description) <= Convert.ToDateTime(oFreeSlotItems[k + 1].Code))
        //                            //        {
        //                            //            DataRow Fslot;
        //                            //            Fslot = dtTemp.NewRow();

        //                            //            Fslot["Select"] = Convert.ToBoolean(1);
        //                            //            Fslot["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //                            //            Fslot["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //                            //            Fslot["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //                            //            Fslot["Start Time"] = oCriteria.ClinicStartTime.ToString("hh:mm tt");
        //                            //            Fslot["End Time"] = oFreeSlotItems[k].Code;
        //                            //            dtTemp.Rows.Add(Fslot);
        //                            //        }
        //                            //    }

        //                            //    DataRow r;
        //                            //    r = dtTemp.NewRow();
        //                            //    //Column[0]= "ProviderID"
        //                            //    //Column[1]= "Provider Name"
        //                            //    //Column[2]= "Date"
        //                            //    //Column[3]= "Start Time"
        //                            //    //Column[4]= "End Time"
        //                            //    r["Select"] = Convert.ToBoolean(0);
        //                            //    r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //                            //    r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //                            //    r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //                            //    r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
        //                            //    r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
        //                            //    for (int i = 0; i < oItemTime.SubItems.Count; i++)
        //                            //    {
        //                            //        r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
        //                            //        r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
        //                            //    }
        //                            //    // Remove last coma ","
        //                            //    if (Convert.ToString(r["ProblemType"]).Length > 0)
        //                            //    {
        //                            //        r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
        //                            //    }
        //                            //    dtTemp.Rows.Add(r);

        //                            //    if (k < oFreeSlotItems.Count - 2)
        //                            //    {

        //                            //        if (Convert.ToDateTime(oFreeSlotItems[k].Description) != Convert.ToDateTime(oFreeSlotItems[k + 1].Code))
        //                            //        {

        //                            //            DataRow rBlocked;
        //                            //            rBlocked = dtTemp.NewRow();

        //                            //            rBlocked["Select"] = Convert.ToBoolean(1);
        //                            //            rBlocked["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //                            //            rBlocked["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //                            //            rBlocked["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //                            //            rBlocked["Start Time"] = oFreeSlotItems[k].Description;
        //                            //            rBlocked["End Time"] = oFreeSlotItems[k + 1].Code;
        //                            //            dtTemp.Rows.Add(rBlocked);

        //                            //        }
        //                            //    }

        //                            //}
        //                            #endregion

        //                            for (int k = 0; k < oFreeSlotItems.Count; k++)
        //                            {
        //                                gloGeneralItem.gloItem oItemTime  = oFreeSlotItems[k];

        //                                DataRow r;
        //                                r = dtTemp.NewRow();
        //                                //Column[0]= "ProviderID"
        //                                //Column[1]= "Provider Name"
        //                                //Column[2]= "Date"
        //                                //Column[3]= "Start Time"
        //                                //Column[4]= "End Time"
        //                                r["Select"] = Convert.ToBoolean(0);
        //                                r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //                                r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //                                r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //                                r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
        //                                r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
        //                                for (int i = 0; i < oItemTime.SubItems.Count; i++)
        //                                {
        //                                    r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
        //                                    r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
        //                                }
        //                                // Remove last coma ","
        //                                if (Convert.ToString(r["ProblemType"]).Length > 0)
        //                                {
        //                                    r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
        //                                }

        //                                dtTemp.Rows.Add(r);
        //                                //nTempStartTime = gloDateMaster.gloTime.TimeAsNumber(oItemTime.Description.ToString());
        //                            } //  for (int k = 0; k < oFreeSlotItems.Count; k++)
        //                        }//for (int n = 0; n < dtSchedule.Rows.Count; n++)
        //                    }// if (strProcedureIDs.Trim() != "")
        //                    #endregion Search Free Slots in Problem Type

        //                } //     for (int l = 0; l < oProviderItems.Count; l++)
        //            } // for (int m = 0; m <= _FindDates.Count - 1; m++)

        //        } // try
        //        catch (Exception ex)
        //        {
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //            ex = null;   
        //            return null;
        //        }

        //        return dtTemp;

        //    }

        //    private gloItems SplitDuration(DateTime sTime, DateTime eTime, Int64 duration, gloItems oAppointmentItems)
        //    {
        //        gloGeneralItem.gloItems oItems = new gloGeneralItem.gloItems();
        //        // gloGeneralItem.gloItem oAppointmentItem = new gloGeneralItem.gloItem();
        //        try
        //        {
        //            //Converting the duration into ticks.
        //            TimeSpan tsDuration = new TimeSpan(duration);
        //            DateTime dtTemSTime = sTime;
        //            DateTime dtTemETime = eTime;
        //            //  
        //            //if appointment starts before noon(12:00)  & ends after noon(12:00)  
        //            for (int x = 0; x < oAppointmentItems.Count; x++)
        //            {
        //                if (dtTemSTime >= Convert.ToDateTime(oAppointmentItems[x].Code) && dtTemSTime <= Convert.ToDateTime(oAppointmentItems[x].Description))
        //                {
        //                    if (dtTemETime > Convert.ToDateTime(oAppointmentItems[x].Description))
        //                        dtTemSTime = Convert.ToDateTime(oAppointmentItems[x].Description);
        //                }
        //                if (dtTemETime >= Convert.ToDateTime(oAppointmentItems[x].Code) && dtTemETime <= Convert.ToDateTime(oAppointmentItems[x].Description))
        //                {
        //                    if (dtTemSTime < Convert.ToDateTime(oAppointmentItems[x].Code))
        //                        dtTemETime = Convert.ToDateTime(oAppointmentItems[x].Code);
        //                }
        //            }
        //            eTime = dtTemETime;
        //            TimeSpan ts;
        //            //Calculating the time span between the start & end time.
        //            ts = eTime.Subtract(dtTemSTime);

        //            //for (int k = 0; ts.TotalMinutes >= tsDuration.Ticks; k++)
        //            for (int k = 0; dtTemETime <= eTime; k++)
        //            {
        //                gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();

        //                //Make the 'Temp slot' of given duration
        //                dtTemETime = dtTemSTime.AddMinutes(duration);
        //                oItem.Code = dtTemSTime.ToString("hh:mm tt");
        //                oItem.Description = dtTemETime.ToString("hh:mm tt");

        //                //if any appointment starts between 'Temp slot' then 
        //                //change the 'End time' of 'Temp slot' by 'Start time' of appointment   
        //                for (int y = 0; y < oAppointmentItems.Count; y++)
        //                {
        //                    if (dtTemSTime <= Convert.ToDateTime(oAppointmentItems[y].Code) && dtTemETime > Convert.ToDateTime(oAppointmentItems[y].Code))
        //                    {
        //                        oItem.Description = Convert.ToDateTime(oAppointmentItems[y].Code).ToString("hh:mm tt");
        //                        dtTemETime = Convert.ToDateTime(oAppointmentItems[y].Description);
        //                        break;
        //                    }

        //                    //****
        //                    //if any appointment starts before 'Temp slot' end time and ends after 'Temp slot' then 
        //                    //change the 'End time' of 'Temp slot' by 'Start time' of appointment   

        //                    if (dtTemETime >= Convert.ToDateTime(oAppointmentItems[y].Code) && dtTemETime <= Convert.ToDateTime(oAppointmentItems[y].Description))
        //                    {
        //                        dtTemETime = Convert.ToDateTime(oAppointmentItems[y].Description);
        //                    }

        //                }

        //                if (dtTemETime > eTime)
        //                {
        //                    oItem.Description = eTime.ToString("hh:mm tt");
        //                }
        //                for (int h = 0; h < oAppointmentItems.Count; h++)
        //                //*** for appointments overlapped 
        //                {
        //                    if (h < oAppointmentItems.Count - 1)
        //                    {
        //                        if (Convert.ToDateTime(oItem.Description) == Convert.ToDateTime(oAppointmentItems[h].Code) && Convert.ToDateTime(oAppointmentItems[h].Description) > Convert.ToDateTime(oAppointmentItems[h + 1].Code))
        //                        {
        //                            // if starttime for appointments is  same 

        //                            if (oAppointmentItems[h].Code == oAppointmentItems[h + 1].Code)
        //                            {
        //                                if (Convert.ToDateTime(oAppointmentItems[h].Description) > Convert.ToDateTime(oAppointmentItems[h + 1].Description))
        //                                {
        //                                    dtTemETime = Convert.ToDateTime(oAppointmentItems[h].Description);
        //                                    break;
        //                                }
        //                                else
        //                                {
        //                                    dtTemETime = Convert.ToDateTime(oAppointmentItems[h + 1].Description);
        //                                    break;
        //                                }

        //                            }
        //                            //
        //                            if (Convert.ToDateTime(oAppointmentItems[h].Description) < Convert.ToDateTime(oAppointmentItems[h + 1].Description))
        //                            {
        //                                dtTemETime = Convert.ToDateTime(oAppointmentItems[h + 1].Description);
        //                                break;
        //                            }

        //                        }
        //                    }
        //                }
        //                //***
        //                if (oItem.Code != oItem.Description)
        //                {
        //                    oItems.Add(oItem);
        //                }
        //                oItem.Dispose();
        //                oItem = null;
        //                // Initialize the Date Time for the Next Interval
        //                dtTemSTime = dtTemETime;
        //                ts = eTime.Subtract(dtTemSTime);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //        return oItems;
        //    }

        //    /// <summary>
        //    /// Old Search Appointment  "Working with old logic"
        //    /// Use "private gloItems SplitDuration_1(DateTime sTime, DateTime eTime, Int64 duration)"
        //    /// 
        //    /// </summary>
        //    /// <param name="oCriteria"></param>
        //    /// <returns></returns>
        //    public DataTable SearchAppointments_1(AppointmentSearchCriteria oCriteria)
        //    {
        //        //DataTable dtTemp = new DataTable();
        //        //try
        //        //{
        //        //    Int32 nClinicStartTime = 0900;
        //        //    Int32 nClinicEndTime = 1800;
        //        //    String strQuery = "";
        //        //    String strResourcesIDs = "";
        //        //    String strProviderIDs = "";
        //        //    String strProcedureIDs = "";

        //        //    DateTime dtClinicStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nClinicStartTime));
        //        //    DateTime dtClinicEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nClinicEndTime));

        //        //    // Add Columns to the DataTable to which we need to apply the Filte               

        //        //    dtTemp.Columns.Add("Select");
        //        //    dtTemp.Columns.Add("ProviderID");
        //        //    dtTemp.Columns.Add("Provider Name");
        //        //    dtTemp.Columns.Add("Date");
        //        //    dtTemp.Columns.Add("Start Time");
        //        //    dtTemp.Columns.Add("End Time");
        //        //    dtTemp.Columns.Add("ProblemID");
        //        //    dtTemp.Columns.Add("ProblemType");

        //        //    // Clear The Calender Control 
        //        //    //juc_Calender.Appointments.Clear();

        //        //    #region " Get The Filter Criteria For Appointment "

        //        //    // Add Checked ResourceIDs in String 

        //        //    for (int i = 0; i < oCriteria.Resources.Count; i++)
        //        //    {
        //        //        if (strResourcesIDs.Trim() == "")
        //        //            strResourcesIDs = Convert.ToString(oCriteria.Resources[i].ID);
        //        //        else
        //        //            strResourcesIDs = strResourcesIDs + "," + oCriteria.Resources[i].ID;
        //        //    }

        //        //    for (int i = 0; i < oCriteria.ProblemTypes.Count; i++)
        //        //    {
        //        //        if (strProcedureIDs.Trim() == "")
        //        //            strProcedureIDs = Convert.ToString(oCriteria.ProblemTypes[i].ID);
        //        //        else
        //        //            strProcedureIDs = strProcedureIDs + "," + oCriteria.ProblemTypes[i].ID;
        //        //    }



        //        //    // Set The Date Criteria
        //        //    // Fill the Dates in Array List 

        //        //    ArrayList _FindDates = new ArrayList();
        //        //    //Int32 dNoofDays;
        //        //    //TimeSpan oTimeSpan = oCriteria.EndDate.Subtract(oCriteria.StartDate);

        //        //    //dNoofDays = Convert.ToInt32(oTimeSpan.TotalDays);
        //        //    //for (int k = 0; k <= dNoofDays; k++)
        //        //    //{
        //        //    //    _FindDates.Add(oCriteria.StartDate.AddDays(k));
        //        //    //}
        //        //    //dNoofDays = 0;

        //        //    _FindDates = oCriteria.Dates;


        //        //    gloItems oProviderItems = oCriteria.Providers;

        //        //    if (oProviderItems.Count > 0)
        //        //    {
        //        //        for (int i = 0; i < oProviderItems.Count; i++)
        //        //        {
        //        //            if (strProviderIDs.Trim() == "")
        //        //                strProviderIDs = Convert.ToString(oProviderItems[i].ID);
        //        //            else
        //        //                strProviderIDs = strProviderIDs + "," + oProviderItems[i].ID;

        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        // If Providers are not Selected in ComboBox then Select al Providers
        //        //        gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
        //        //        DataTable dt = new DataTable();
        //        //        dt = oResource.GetProviders();
        //        //        oResource.Dispose();
        //        //        oResource = null;
        //        //        //nProviderID, sFirstName, sMiddleName, sLastName, sGender, sDEA, sAddress, sStreet, sCity, sState, sZIP, sPhoneNo, sFAX, sMobileNo, sPagerNo, sEmail, sURL, imgSignature, nProviderType, sNPI, sUPIN, sMedicalLicenseNo, sPrefix, sExternalCode , ProviderName
        //        //        if (dt != null)
        //        //        {
        //        //            for (int i = 0; i < dt.Rows.Count; i++)
        //        //            {
        //        //                gloGeneralItem.gloItem oProviderItem = new gloGeneralItem.gloItem();

        //        //                oProviderItem.ID = Convert.ToInt64(dt.Rows[i]["nProviderID"]);
        //        //                oProviderItem.Description = dt.Rows[i]["ProviderName"].ToString(); // Provider Name
        //        //                oProviderItems.Add(oProviderItem);

        //        //                if (strProviderIDs.Trim() == "")
        //        //                    strProviderIDs = Convert.ToString(oProviderItem.ID);
        //        //                else
        //        //                    strProviderIDs = strProviderIDs + "," + oProviderItem.ID;
        //        //            }
        //        //        }
        //        //    }

        //        //    #endregion

        //        //    for (int m = 0; m <= _FindDates.Count - 1; m++)
        //        //    {
        //        //        for (int l = 0; l < oProviderItems.Count; l++)
        //        //        {
        //        //            Int64 nProviderID = Convert.ToInt64(oProviderItems[l].ID);

        //        //            #region Get Schedules for Provider
        //        //            // Search Schedule
        //        //            // for the Given Date i.e for _FindDates[m] & The Selected Provider , Problem type & Resources
        //        //            string strScheduleQuery = "";
        //        //            strScheduleQuery = " SELECT DISTINCT  AS_Schedule_DTL.nStartDate, AS_Schedule_DTL.nStartTime, AS_Schedule_DTL.nEndDate, AS_Schedule_DTL.nEndTime,  AS_Schedule_DTL_Procedures.nProcedureID, AB_AppointmentType.sAppointmentType " +
        //        //                           "FROM         AS_Schedule_DTL_Resources RIGHT OUTER JOIN AS_Schedule_DTL INNER JOIN " +
        //        //                           " AB_Location ON AS_Schedule_DTL.nLocationID = AB_Location.nLocationID LEFT OUTER JOIN " +
        //        //                           "AB_Resource_Provider INNER JOIN AS_Schedule_DTL_Providers ON AB_Resource_Provider.nProviderID = AS_Schedule_DTL_Providers.nProviderID ON " +
        //        //                           "AS_Schedule_DTL.nMasterScheduleID = AS_Schedule_DTL_Providers.nMasterScheduleID ON  " +
        //        //                           "AS_Schedule_DTL_Resources.nMasterScheduleID = AS_Schedule_DTL.nMasterScheduleID LEFT OUTER JOIN " +
        //        //                           "AB_AppointmentType INNER JOIN  " +
        //        //                           "AS_Schedule_DTL_Procedures ON AB_AppointmentType.nAppointmentTypeID = AS_Schedule_DTL_Procedures.nProcedureID ON  " +
        //        //                           "AS_Schedule_DTL.nMasterScheduleID = AS_Schedule_DTL_Procedures.nMasterScheduleID  " +
        //        //                           " WHERE     (AS_Schedule_DTL.nStartDate >= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") AND (AS_Schedule_DTL.nEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + ") AND ";

        //        //            if (strProcedureIDs.Trim() == "")
        //        //            {
        //        //                if (strProviderIDs.Trim() != "")
        //        //                {
        //        //                    strScheduleQuery = strScheduleQuery + " (AB_Resource_Provider.nProviderID IN (" + oProviderItems[l].ID + "))";
        //        //                }
        //        //            }
        //        //            else
        //        //            {
        //        //                if (strProviderIDs.Trim() != "")
        //        //                {
        //        //                    strScheduleQuery = strScheduleQuery + " (AB_Resource_Provider.nProviderID IN (" + oProviderItems[l].ID + ")) AND";
        //        //                }
        //        //                if (strProcedureIDs.Trim() != "")
        //        //                {
        //        //                    strScheduleQuery = strScheduleQuery + " (AS_Schedule_DTL_Procedures.nProcedureID IN (" + strProcedureIDs + "))";
        //        //                }

        //        //            }
        //        //            //strScheduleQuery = strScheduleQuery.Trim();
        //        //            //strScheduleQuery = strScheduleQuery.Remove((strScheduleQuery.Trim().Length - 3), 3);

        //        //            strScheduleQuery = strScheduleQuery + " ORDER BY AS_Schedule_DTL.nStartTime, AS_Schedule_DTL.nEndTime ";


        //        //            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        //            oDB.Connect(false);
        //        //            DataTable dtSchedule;
        //        //            oDB.Retrive_Query(strScheduleQuery, out dtSchedule);
        //        //            #endregion

        //        //            #region Search Free Slots Without Problem Type

        //        //            if (strProcedureIDs.Trim() == "")  //Problem Type Not Selected
        //        //            {
        //        //                gloGeneralItem.gloItems oFreeSlotItems = new gloGeneralItem.gloItems();
        //        //                oFreeSlotItems = SplitDuration_1(gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nClinicStartTime)), gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nClinicEndTime)), Convert.ToInt64(oCriteria.Duration));


        //        //                #region Attach ScheduleName/ProblemType to FreeSlot

        //        //                //Add Schedule name & ID to free slots which has same time as schedule
        //        //                for (int i = 0; i < dtSchedule.Rows.Count; i++)
        //        //                {
        //        //                    DateTime StartTime = Convert.ToDateTime("9:00 AM");
        //        //                    DateTime EndTime = Convert.ToDateTime("6:00 PM");
        //        //                    string ScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["nStartTime"])).ToShortTimeString();
        //        //                    string ScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["nEndTime"])).ToShortTimeString();

        //        //                    for (int x = 0; x < oFreeSlotItems.Count; x++)
        //        //                    {
        //        //                        if (Convert.ToDateTime(oFreeSlotItems[x].Code) <= Convert.ToDateTime(ScheduleStartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) > Convert.ToDateTime(ScheduleStartTime))
        //        //                        {
        //        //                            StartTime = Convert.ToDateTime(oFreeSlotItems[x].Code);
        //        //                        }

        //        //                        if (Convert.ToDateTime(oFreeSlotItems[x].Code) < Convert.ToDateTime(ScheduleEndTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) >= Convert.ToDateTime(ScheduleEndTime))
        //        //                        {
        //        //                            EndTime = Convert.ToDateTime(oFreeSlotItems[x].Description);
        //        //                        }
        //        //                    }
        //        //                    if (StartTime < dtClinicStartTime)
        //        //                    {
        //        //                        StartTime = Convert.ToDateTime(ScheduleEndTime);
        //        //                    }

        //        //                    if (EndTime > dtClinicEndTime)
        //        //                    {
        //        //                        EndTime = Convert.ToDateTime(ScheduleEndTime);
        //        //                    }

        //        //                    for (int x = oFreeSlotItems.Count - 1; x >= 0; x--)
        //        //                    {

        //        //                        if (Convert.ToDateTime(oFreeSlotItems[x].Code) >= Convert.ToDateTime(StartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) <= Convert.ToDateTime(EndTime))
        //        //                        {
        //        //                            gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();
        //        //                            oSubItem.Code = Convert.ToString(dtSchedule.Rows[i]["nProcedureID"]);
        //        //                            oSubItem.Description = Convert.ToString(dtSchedule.Rows[i]["sAppointmentType"]);
        //        //                            oFreeSlotItems[x].SubItems.Add(oSubItem);
        //        //                        }
        //        //                    }

        //        //                }

        //        //                #endregion

        //        //                #region "Get Providers Appointments "

        //        //                strQuery = " SELECT DISTINCT AS_Appointment_DTL.nStartDate, AS_Appointment_DTL.nStartTime, AS_Appointment_DTL.nEndDate, AS_Appointment_DTL.nEndTime,  "
        //        //                    + " AS_Appointment_DTL.sNotes, AB_Location.sLocation, AB_Department.sDepartment, AS_Appointment_DTL_Providers.nProviderID AS nProviderID, (ISNULL(AB_Resource_Provider.sFirstName,'') +SPACE(1)+ ISNULL(AB_Resource_Provider.sMiddleName,'') +SPACE(1)+ ISNULL(AB_Resource_Provider.sLastName,'')) ProviderName "
        //        //                    + " FROM	AS_Appointment_DTL_Procedures RIGHT OUTER JOIN AS_Appointment_DTL_Resources RIGHT OUTER JOIN AB_Department "
        //        //                    + " INNER JOIN AS_Appointment_DTL ON AB_Department.nDepartmentID = AS_Appointment_DTL.nDepartmentID "
        //        //                    + " INNER JOIN AB_Location ON AB_Department.nLocationID = AB_Location.nLocationID ON AS_Appointment_DTL_Resources.nMasterAppointmentID = AS_Appointment_DTL.nMasterAppointmentID AND AS_Appointment_DTL_Resources.nAppointmentID = AS_Appointment_DTL.nAppointmentID "
        //        //                    + " LEFT OUTER JOIN AS_Appointment_DTL_Providers "
        //        //                    + " RIGHT OUTER JOIN AB_Resource_Provider ON AS_Appointment_DTL_Providers.nProviderID = AB_Resource_Provider.nProviderID ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Providers.nMasterAppointmentID AND "
        //        //                    + " AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Providers.nAppointmentID "
        //        //                    + " LEFT OUTER JOIN AS_Appointment_DTL_RefDoctors ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_RefDoctors.nMasterAppointmentID AND "
        //        //                    + " AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_RefDoctors.nAppointmentID ON AS_Appointment_DTL_Procedures.nMasterAppointmentID = AS_Appointment_DTL.nMasterAppointmentID AND "
        //        //                    + " AS_Appointment_DTL_Procedures.nAppointmentID = AS_Appointment_DTL.nAppointmentID "
        //        //                    + " LEFT OUTER JOIN AS_Appointment_DTL_Coverages ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Coverages.nMasterAppointmentID AND AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Coverages.nAppointmentID "
        //        //                    + " LEFT OUTER JOIN  AB_AppointmentType ON AS_Appointment_DTL.nAppointmentTypeID = AB_AppointmentType.nAppointmentTypeID "
        //        //                    //  + " WHERE AS_Appointment_DTL.nStartDate >=" + gloDateMaster.gloDate.DateAsNumber(tempStartDate.Date.ToString()) + " AND  AS_Appointment_DTL.nEndDate <= " + gloDateMaster.gloDate.DateAsNumber(tempEndDate.Date.ToString()) + " AND "
        //        //                    + " WHERE AS_Appointment_DTL.nStartDate >=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + " AND   AS_Appointment_DTL.nEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + " AND "
        //        //                    + " (AS_Appointment_DTL_Providers.nProviderID = " + nProviderID + " OR";


        //        //                // If Resources are Selected then Apply filter for Resources
        //        //                if (strResourcesIDs.Trim() != "")
        //        //                {
        //        //                    strQuery = strQuery + " AS_Appointment_DTL_Resources.nResourceID IN( " + strResourcesIDs + "))";
        //        //                }
        //        //                else
        //        //                {
        //        //                    strQuery = strQuery.Trim();
        //        //                    strQuery = strQuery.Remove((strQuery.Trim().Length - 2), 2);
        //        //                    strQuery += ")";
        //        //                }


        //        //                strQuery = strQuery + " ORDER BY AS_Appointment_DTL.nStartDate, AS_Appointment_DTL.nStartTime, AS_Appointment_DTL.nEndTime";

        //        //                // 
        //        //                //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        //                oDB.Connect(false);
        //        //                DataTable dt;
        //        //                oDB.Retrive_Query(strQuery, out dt);

        //        //                gloGeneralItem.gloItems oAppointmentItems = new gloGeneralItem.gloItems();
        //        //                if (dt != null)
        //        //                {
        //        //                    if (dt.Rows.Count > 0)
        //        //                    {
        //        //                        for (int i = 0; i < dt.Rows.Count; i++)
        //        //                        {
        //        //                            gloGeneralItem.gloItem oAppointmentItem = new gloGeneralItem.gloItem();
        //        //                            oAppointmentItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nStartTime"])).ToShortTimeString();
        //        //                            oAppointmentItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nEndTime"])).ToShortTimeString();

        //        //                            oAppointmentItems.Add(oAppointmentItem);
        //        //                            oAppointmentItem = null;

        //        //                        } // for (int i = 0; i < dt.Rows.Count; i++)
        //        //                    }// if (dt.Rows.Count > 0)
        //        //                } //  if (dt != null)



        //        //                // To Find the Block Schedule
        //        //               // gloScheduling.gloSchedule oFindSchedules = new gloAppointmentScheduling.gloScheduling.gloSchedule(_databaseconnectionstring);
        //        //                gloScheduling.Common.ViewSchedules oViewSchedules = new gloAppointmentScheduling.gloScheduling.Common.ViewSchedules();
        //        //                ArrayList arrProvder = new ArrayList();
        //        //                arrProvder.Add(nProviderID);

        //        //                // Get The Blocked Schedules
        //        //                oViewSchedules = oFindSchedules.GetSchedules(Convert.ToDateTime(_FindDates[m]), Convert.ToDateTime(_FindDates[m]), arrProvder, oCriteria.ClinicId, true);

        //        //                for (int i = 0; i < oViewSchedules.Count; i++)
        //        //                {
        //        //                    gloGeneralItem.gloItem oBlockItem = new gloGeneralItem.gloItem();
        //        //                    oBlockItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(oViewSchedules[i].StartTime)).ToShortTimeString(); // gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nStartTime"])).ToShortTimeString();
        //        //                    oBlockItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(oViewSchedules[i].EndTime)).ToShortTimeString();  // gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nEndTime"])).ToShortTimeString();

        //        //                    oAppointmentItems.Add(oBlockItem);
        //        //                    oBlockItem = null;
        //        //                }


        //        //                #endregion

        //        //                #region Finding the FreeSlots According to Appointments

        //        //                gloGeneralItem.gloItems oExtraSlots = new gloGeneralItem.gloItems();
        //        //                // For Finding the Free Slots According to Appointments Data
        //        //                for (int y = 0; y < oAppointmentItems.Count; y++)
        //        //                {
        //        //                    Int32 nStartTime = 0800;
        //        //                    Int32 nEndTime = 1900;
        //        //                    DateTime StartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nStartTime));
        //        //                    DateTime EndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nEndTime));

        //        //                    for (int x = 0; x < oFreeSlotItems.Count; x++)
        //        //                    {

        //        //                        if (Convert.ToDateTime(oFreeSlotItems[x].Code) <= Convert.ToDateTime(oAppointmentItems[y].Code) && Convert.ToDateTime(oFreeSlotItems[x].Description) > Convert.ToDateTime(oAppointmentItems[y].Code))
        //        //                        {
        //        //                            StartTime = Convert.ToDateTime(oFreeSlotItems[x].Code);

        //        //                            TimeSpan ts = new TimeSpan();
        //        //                            ts = Convert.ToDateTime(oAppointmentItems[y].Code).Subtract(Convert.ToDateTime(oFreeSlotItems[x].Code));
        //        //                            if (ts.Minutes >= 1)
        //        //                            {
        //        //                                oExtraSlots.Add(0, oFreeSlotItems[x].Code, oAppointmentItems[y].Code);

        //        //                                for (int i = 0; i < oFreeSlotItems[x].SubItems.Count; i++)
        //        //                                {
        //        //                                    oExtraSlots[oExtraSlots.Count - 1].SubItems.Add(oFreeSlotItems[x].SubItems[i]);
        //        //                                }
        //        //                            }
        //        //                        }

        //        //                        if (Convert.ToDateTime(oFreeSlotItems[x].Code) < Convert.ToDateTime(oAppointmentItems[y].Description) && Convert.ToDateTime(oFreeSlotItems[x].Description) >= Convert.ToDateTime(oAppointmentItems[y].Description))
        //        //                        {
        //        //                            EndTime = Convert.ToDateTime(oFreeSlotItems[x].Description);

        //        //                            TimeSpan ts = new TimeSpan();
        //        //                            ts = Convert.ToDateTime(oFreeSlotItems[x].Description).Subtract(Convert.ToDateTime(oAppointmentItems[y].Description));
        //        //                            if (ts.Minutes >= 1)
        //        //                            {
        //        //                                oExtraSlots.Add(0, oAppointmentItems[y].Description, oFreeSlotItems[x].Description);
        //        //                                for (int i = 0; i < oFreeSlotItems[x].SubItems.Count; i++)
        //        //                                {
        //        //                                    oExtraSlots[oExtraSlots.Count - 1].SubItems.Add(oFreeSlotItems[x].SubItems[i]);
        //        //                                }

        //        //                            }
        //        //                        }
        //        //                    }

        //        //                    if (StartTime <= dtClinicStartTime)
        //        //                    {
        //        //                        StartTime = Convert.ToDateTime(oAppointmentItems[y].Code);
        //        //                    }

        //        //                    if (EndTime >= dtClinicEndTime)
        //        //                    {
        //        //                        EndTime = Convert.ToDateTime(oAppointmentItems[y].Description);
        //        //                    }

        //        //                    for (int x = oFreeSlotItems.Count - 1; x >= 0; x--)
        //        //                    {

        //        //                        if (Convert.ToDateTime(oFreeSlotItems[x].Code) >= Convert.ToDateTime(StartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) <= Convert.ToDateTime(EndTime))
        //        //                        {
        //        //                            oFreeSlotItems.RemoveAt(x);
        //        //                        }
        //        //                    }
        //        //                }
        //        //                #endregion

        //        //                #region "Insert Extra free slots in FreeSlot colection"

        //        //                //Insert Extra free slots in free slot colection
        //        //                //Extra Slot --> Slot between two appointments   
        //        //                for (int k = 0; k < oFreeSlotItems.Count; k++)
        //        //                {
        //        //                    for (int i = 0; i < oExtraSlots.Count; i++)
        //        //                    {
        //        //                        //if extra slot is before first slot
        //        //                        if (k == 0)
        //        //                        {
        //        //                            if (Convert.ToDateTime(oFreeSlotItems[k].Code) > Convert.ToDateTime(oExtraSlots[i].Code))
        //        //                            {
        //        //                                oFreeSlotItems.Insert(k, oExtraSlots[i]);
        //        //                                break;
        //        //                            }
        //        //                        }

        //        //                        //if extra slot is after last slot
        //        //                        if (k == oFreeSlotItems.Count - 1)
        //        //                        {
        //        //                            if (Convert.ToDateTime(oFreeSlotItems[k].Description) < Convert.ToDateTime(oExtraSlots[i].Code))
        //        //                            {
        //        //                                oFreeSlotItems.Add(oExtraSlots[i]);
        //        //                                break;
        //        //                            }
        //        //                        }

        //        //                        //extra slot is between two slots
        //        //                        if (Convert.ToDateTime(oFreeSlotItems[k].Code) > Convert.ToDateTime(oExtraSlots[i].Code)
        //        //                            && Convert.ToDateTime(oFreeSlotItems[k - 1].Description) <= Convert.ToDateTime(oExtraSlots[i].Code))
        //        //                        {
        //        //                            oFreeSlotItems.Insert(k, oExtraSlots[i]);
        //        //                        }
        //        //                    }
        //        //                }

        //        //                oExtraSlots.Clear();

        //        //                #endregion


        //        //                for (int k = 0; k < oFreeSlotItems.Count; k++)
        //        //                {
        //        //                    gloGeneralItem.gloItem oItemTime = new gloGeneralItem.gloItem();
        //        //                    oItemTime = oFreeSlotItems[k];

        //        //                    DataRow r;
        //        //                    r = dtTemp.NewRow();
        //        //                    //Column[0]= "ProviderID"
        //        //                    //Column[1]= "Provider Name"
        //        //                    //Column[2]= "Date"
        //        //                    //Column[3]= "Start Time"
        //        //                    //Column[4]= "End Time"
        //        //                    r["Select"] = Convert.ToBoolean(0);
        //        //                    r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //        //                    r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //        //                    r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //        //                    r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
        //        //                    r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
        //        //                    for (int i = 0; i < oItemTime.SubItems.Count; i++)
        //        //                    {
        //        //                        r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
        //        //                        r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
        //        //                    }
        //        //                    // Remove last coma ","
        //        //                    if (Convert.ToString(r["ProblemType"]).Length > 0)
        //        //                    {
        //        //                        r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
        //        //                    }
        //        //                    dtTemp.Rows.Add(r);
        //        //                } //  for (int k = 0; k < oFreeSlotItems.Count; k++)
        //        //            }
        //        //            #endregion

        //        //            #region Search Free Slots in Problem Type
        //        //            //If Shedule Present for selected problem type
        //        //            //change clinic start & end time by schedule start and end time
        //        //            //i.e : Search only in Schedule time 
        //        //            if (strProcedureIDs.Trim() != "")
        //        //            {
        //        //                if (dtSchedule.Rows.Count == 0) // No schedule present i.e : Don't search
        //        //                {
        //        //                    nClinicStartTime = 0900;
        //        //                    nClinicEndTime = 0900;
        //        //                }
        //        //                // Search in schedule time only
        //        //                for (int n = 0; n < dtSchedule.Rows.Count; n++)
        //        //                {
        //        //                    nClinicStartTime = Convert.ToInt32(dtSchedule.Rows[n]["nStartTime"]);
        //        //                    nClinicEndTime = Convert.ToInt32(dtSchedule.Rows[n]["nEndTime"]);

        //        //                    dtClinicStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nClinicStartTime));
        //        //                    dtClinicEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nClinicEndTime));


        //        //                    gloGeneralItem.gloItems oFreeSlotItems = new gloGeneralItem.gloItems();
        //        //                    oFreeSlotItems = SplitDuration_1(gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nClinicStartTime)), gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(nClinicEndTime)), Convert.ToInt64(oCriteria.Duration));


        //        //                    #region Attach ScheduleName/ProblemType to FreeSlot

        //        //                    //Add Schedule name & ID to free slots which has same time as schedule
        //        //                    for (int i = 0; i < dtSchedule.Rows.Count; i++)
        //        //                    {
        //        //                        DateTime StartTime = Convert.ToDateTime("8:00 AM");  //keep  less than actual clinic strart time
        //        //                        DateTime EndTime = Convert.ToDateTime("7:00 PM");  //keep more than actual clinic strart time
        //        //                        string ScheduleStartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["nStartTime"])).ToShortTimeString();
        //        //                        string ScheduleEndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtSchedule.Rows[i]["nEndTime"])).ToShortTimeString();
        //        //                        for (int x = 0; x < oFreeSlotItems.Count; x++)
        //        //                        {
        //        //                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) <= Convert.ToDateTime(ScheduleStartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) > Convert.ToDateTime(ScheduleStartTime))
        //        //                            {
        //        //                                StartTime = Convert.ToDateTime(oFreeSlotItems[x].Code);
        //        //                            }

        //        //                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) < Convert.ToDateTime(ScheduleEndTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) >= Convert.ToDateTime(ScheduleEndTime))
        //        //                            {
        //        //                                EndTime = Convert.ToDateTime(oFreeSlotItems[x].Description);
        //        //                            }
        //        //                        }
        //        //                        if (StartTime < dtClinicStartTime)
        //        //                        {
        //        //                            StartTime = Convert.ToDateTime(ScheduleEndTime);
        //        //                        }

        //        //                        if (EndTime > dtClinicEndTime)
        //        //                        {
        //        //                            EndTime = Convert.ToDateTime(ScheduleEndTime);
        //        //                        }

        //        //                        for (int x = oFreeSlotItems.Count - 1; x >= 0; x--)
        //        //                        {

        //        //                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) >= Convert.ToDateTime(StartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) <= Convert.ToDateTime(EndTime))
        //        //                            {
        //        //                                gloGeneralItem.gloSubItem oSubItem = new gloGeneralItem.gloSubItem();
        //        //                                oSubItem.Code = Convert.ToString(dtSchedule.Rows[i]["nProcedureID"]);
        //        //                                oSubItem.Description = Convert.ToString(dtSchedule.Rows[i]["sAppointmentType"]);
        //        //                                oFreeSlotItems[x].SubItems.Add(oSubItem);
        //        //                            }
        //        //                        }

        //        //                    }

        //        //                    #endregion

        //        //                    #region "Get Providers Appointments "

        //        //                    strQuery = " SELECT DISTINCT AS_Appointment_DTL.nStartDate, AS_Appointment_DTL.nStartTime, AS_Appointment_DTL.nEndDate, AS_Appointment_DTL.nEndTime,  "
        //        //                        + " AS_Appointment_DTL.sNotes, AB_Location.sLocation, AB_Department.sDepartment, AS_Appointment_DTL_Providers.nProviderID AS nProviderID, (ISNULL(AB_Resource_Provider.sFirstName,'') +SPACE(1)+ ISNULL(AB_Resource_Provider.sMiddleName,'') +SPACE(1)+ ISNULL(AB_Resource_Provider.sLastName,'')) ProviderName "
        //        //                        + " FROM	AS_Appointment_DTL_Procedures RIGHT OUTER JOIN AS_Appointment_DTL_Resources RIGHT OUTER JOIN AB_Department "
        //        //                        + " INNER JOIN AS_Appointment_DTL ON AB_Department.nDepartmentID = AS_Appointment_DTL.nDepartmentID "
        //        //                        + " INNER JOIN AB_Location ON AB_Department.nLocationID = AB_Location.nLocationID ON AS_Appointment_DTL_Resources.nMasterAppointmentID = AS_Appointment_DTL.nMasterAppointmentID AND AS_Appointment_DTL_Resources.nAppointmentID = AS_Appointment_DTL.nAppointmentID "
        //        //                        + " LEFT OUTER JOIN AS_Appointment_DTL_Providers "
        //        //                        + " RIGHT OUTER JOIN AB_Resource_Provider ON AS_Appointment_DTL_Providers.nProviderID = AB_Resource_Provider.nProviderID ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Providers.nMasterAppointmentID AND "
        //        //                        + " AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Providers.nAppointmentID "
        //        //                        + " LEFT OUTER JOIN AS_Appointment_DTL_RefDoctors ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_RefDoctors.nMasterAppointmentID AND "
        //        //                        + " AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_RefDoctors.nAppointmentID ON AS_Appointment_DTL_Procedures.nMasterAppointmentID = AS_Appointment_DTL.nMasterAppointmentID AND "
        //        //                        + " AS_Appointment_DTL_Procedures.nAppointmentID = AS_Appointment_DTL.nAppointmentID "
        //        //                        + " LEFT OUTER JOIN AS_Appointment_DTL_Coverages ON AS_Appointment_DTL.nMasterAppointmentID = AS_Appointment_DTL_Coverages.nMasterAppointmentID AND AS_Appointment_DTL.nAppointmentID = AS_Appointment_DTL_Coverages.nAppointmentID "
        //        //                        + " LEFT OUTER JOIN  AB_AppointmentType ON AS_Appointment_DTL.nAppointmentTypeID = AB_AppointmentType.nAppointmentTypeID "
        //        //                        //  + " WHERE AS_Appointment_DTL.nStartDate >=" + gloDateMaster.gloDate.DateAsNumber(tempStartDate.Date.ToString()) + " AND  AS_Appointment_DTL.nEndDate <= " + gloDateMaster.gloDate.DateAsNumber(tempEndDate.Date.ToString()) + " AND "
        //        //                        + " WHERE AS_Appointment_DTL.nStartDate >=" + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + " AND   AS_Appointment_DTL.nEndDate <= " + gloDateMaster.gloDate.DateAsNumber(_FindDates[m].ToString()) + " AND "
        //        //                        + " (AS_Appointment_DTL_Providers.nProviderID = " + nProviderID + " OR ";


        //        //                    // If Resources are Selected then Apply filter for Resources
        //        //                    if (strResourcesIDs.Trim() != "")
        //        //                    {
        //        //                        strQuery = strQuery + " AS_Appointment_DTL_Resources.nResourceID IN( " + strResourcesIDs + "))";
        //        //                    }
        //        //                    else
        //        //                    {
        //        //                        strQuery = strQuery.Trim();
        //        //                        strQuery = strQuery.Remove((strQuery.Trim().Length - 2), 2);
        //        //                        strQuery += ")";
        //        //                    }

        //        //                    strQuery = strQuery + " ORDER BY AS_Appointment_DTL.nStartDate, AS_Appointment_DTL.nStartTime, AS_Appointment_DTL.nEndTime";

        //        //                    // 
        //        //                    //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        //                    oDB.Connect(false);
        //        //                    DataTable dt;
        //        //                    oDB.Retrive_Query(strQuery, out dt);


        //        //                    gloGeneralItem.gloItems oAppointmentItems = new gloGeneralItem.gloItems();
        //        //                    if (dt != null)
        //        //                    {
        //        //                        if (dt.Rows.Count > 0)
        //        //                        {

        //        //                            for (int i = 0; i < dt.Rows.Count; i++)
        //        //                            {
        //        //                                gloGeneralItem.gloItem oAppointmentItem = new gloGeneralItem.gloItem();
        //        //                                oAppointmentItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nStartTime"])).ToShortTimeString();
        //        //                                oAppointmentItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nEndTime"])).ToShortTimeString();

        //        //                                oAppointmentItems.Add(oAppointmentItem);
        //        //                                oAppointmentItem = null;

        //        //                            } // for (int i = 0; i < dt.Rows.Count; i++)

        //        //                        }// if (dt.Rows.Count > 0)
        //        //                    } //  if (dt != null)



        //        //                    // To Find the Block Schedule
        //        //                    gloScheduling.gloSchedule oFindSchedules = new gloAppointmentScheduling.gloScheduling.gloSchedule(_databaseconnectionstring);
        //        //                    gloScheduling.Common.ViewSchedules oViewSchedules = new gloAppointmentScheduling.gloScheduling.Common.ViewSchedules();
        //        //                    ArrayList arrProvder = new ArrayList();
        //        //                    arrProvder.Add(nProviderID);

        //        //                    // Get The Blocked Schedules
        //        //                    oViewSchedules = oFindSchedules.GetSchedules(Convert.ToDateTime(_FindDates[m]), Convert.ToDateTime(_FindDates[m]), arrProvder, oCriteria.ClinicId, true);

        //        //                    for (int i = 0; i < oViewSchedules.Count; i++)
        //        //                    {
        //        //                        gloGeneralItem.gloItem oBlockItem = new gloGeneralItem.gloItem();
        //        //                        oBlockItem.Code = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(oViewSchedules[i].StartTime)).ToShortTimeString(); // gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nStartTime"])).ToShortTimeString();
        //        //                        oBlockItem.Description = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(oViewSchedules[i].EndTime)).ToShortTimeString();  // gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dt.Rows[i]["nEndTime"])).ToShortTimeString();

        //        //                        oAppointmentItems.Add(oBlockItem);
        //        //                        oBlockItem = null;
        //        //                    }

        //        //                    #endregion " // Provider with Data"

        //        //                    #region Finding the FreeSlots According to Appointments

        //        //                    gloGeneralItem.gloItems oExtraSlots = new gloGeneralItem.gloItems();
        //        //                    // For Finding the Free Slots According to Appointments Data
        //        //                    for (int y = 0; y < oAppointmentItems.Count; y++)
        //        //                    {
        //        //                        DateTime StartTime = Convert.ToDateTime("9:00 AM");
        //        //                        DateTime EndTime = Convert.ToDateTime("6:00 PM");
        //        //                        for (int x = 0; x < oFreeSlotItems.Count; x++)
        //        //                        {

        //        //                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) <= Convert.ToDateTime(oAppointmentItems[y].Code) && Convert.ToDateTime(oFreeSlotItems[x].Description) > Convert.ToDateTime(oAppointmentItems[y].Code))
        //        //                            {
        //        //                                StartTime = Convert.ToDateTime(oFreeSlotItems[x].Code);

        //        //                                TimeSpan ts = new TimeSpan();
        //        //                                ts = Convert.ToDateTime(oAppointmentItems[y].Code).Subtract(Convert.ToDateTime(oFreeSlotItems[x].Code));
        //        //                                if (ts.Minutes >= 1)
        //        //                                {
        //        //                                    oExtraSlots.Add(0, oFreeSlotItems[x].Code, oAppointmentItems[y].Code);
        //        //                                    for (int i = 0; i < oFreeSlotItems[x].SubItems.Count; i++)
        //        //                                    {
        //        //                                        oExtraSlots[oExtraSlots.Count - 1].SubItems.Add(oFreeSlotItems[x].SubItems[i]);
        //        //                                    }
        //        //                                }
        //        //                            }

        //        //                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) < Convert.ToDateTime(oAppointmentItems[y].Description) && Convert.ToDateTime(oFreeSlotItems[x].Description) >= Convert.ToDateTime(oAppointmentItems[y].Description))
        //        //                            {
        //        //                                EndTime = Convert.ToDateTime(oFreeSlotItems[x].Description);

        //        //                                TimeSpan ts = new TimeSpan();
        //        //                                ts = Convert.ToDateTime(oFreeSlotItems[x].Description).Subtract(Convert.ToDateTime(oAppointmentItems[y].Description));
        //        //                                if (ts.Minutes >= 1)
        //        //                                {
        //        //                                    oExtraSlots.Add(0, oAppointmentItems[y].Description, oFreeSlotItems[x].Description);
        //        //                                    for (int i = 0; i < oFreeSlotItems[x].SubItems.Count; i++)
        //        //                                    {
        //        //                                        oExtraSlots[oExtraSlots.Count - 1].SubItems.Add(oFreeSlotItems[x].SubItems[i]);
        //        //                                    }

        //        //                                }
        //        //                            }
        //        //                        }

        //        //                        if (StartTime <= dtClinicStartTime)
        //        //                        {
        //        //                            StartTime = Convert.ToDateTime(oAppointmentItems[y].Code);
        //        //                        }

        //        //                        if (EndTime >= dtClinicEndTime)
        //        //                        {
        //        //                            EndTime = Convert.ToDateTime(oAppointmentItems[y].Description);
        //        //                        }

        //        //                        for (int x = oFreeSlotItems.Count - 1; x >= 0; x--)
        //        //                        {

        //        //                            if (Convert.ToDateTime(oFreeSlotItems[x].Code) >= Convert.ToDateTime(StartTime) && Convert.ToDateTime(oFreeSlotItems[x].Description) <= Convert.ToDateTime(EndTime))
        //        //                            {
        //        //                                oFreeSlotItems.RemoveAt(x);
        //        //                            }
        //        //                        }

        //        //                    }

        //        //                    #endregion

        //        //                    #region Insert Extra free slots in FreeSlot colection

        //        //                    //Insert Extra free slots in free slot colection
        //        //                    //Extra Slot --> Slot between two appointments   
        //        //                    for (int k = 0; k < oFreeSlotItems.Count; k++)
        //        //                    {
        //        //                        for (int i = 0; i < oExtraSlots.Count; i++)
        //        //                        {
        //        //                            //if extra slot is before first slot
        //        //                            if (k == 0)
        //        //                            {
        //        //                                if (Convert.ToDateTime(oFreeSlotItems[k].Code) > Convert.ToDateTime(oExtraSlots[i].Code))
        //        //                                {
        //        //                                    oFreeSlotItems.Insert(k, oExtraSlots[i]);
        //        //                                    break;
        //        //                                }
        //        //                            }

        //        //                            //if extra slot is after last slot
        //        //                            if (k == oFreeSlotItems.Count - 1)
        //        //                            {
        //        //                                if (Convert.ToDateTime(oFreeSlotItems[k].Description) < Convert.ToDateTime(oExtraSlots[i].Code))
        //        //                                {
        //        //                                    oFreeSlotItems.Add(oExtraSlots[i]);
        //        //                                    break;
        //        //                                }
        //        //                            }

        //        //                            //extra slot is between two slots
        //        //                            if (Convert.ToDateTime(oFreeSlotItems[k].Code) > Convert.ToDateTime(oExtraSlots[i].Code)
        //        //                                && Convert.ToDateTime(oFreeSlotItems[k - 1].Description) <= Convert.ToDateTime(oExtraSlots[i].Code))
        //        //                            {
        //        //                                oFreeSlotItems.Insert(k, oExtraSlots[i]);
        //        //                            }
        //        //                        }
        //        //                    }
        //        //                    oExtraSlots.Clear();

        //        //                    #endregion

        //        //                    for (int k = 0; k < oFreeSlotItems.Count; k++)
        //        //                    {
        //        //                        gloGeneralItem.gloItem oItemTime = new gloGeneralItem.gloItem();
        //        //                        oItemTime = oFreeSlotItems[k];

        //        //                        DataRow r;
        //        //                        r = dtTemp.NewRow();
        //        //                        //Column[0]= "ProviderID"
        //        //                        //Column[1]= "Provider Name"
        //        //                        //Column[2]= "Date"
        //        //                        //Column[3]= "Start Time"
        //        //                        //Column[4]= "End Time"
        //        //                        r["Select"] = Convert.ToBoolean(0);
        //        //                        r["ProviderID"] = Convert.ToInt64(oProviderItems[l].ID);
        //        //                        r["Provider Name"] = Convert.ToString(oProviderItems[l].Description);
        //        //                        r["Date"] = Convert.ToDateTime(_FindDates[m]).ToShortDateString();
        //        //                        r["Start Time"] = oItemTime.Code; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nEndTime)).ToShortTimeString();
        //        //                        r["End Time"] = oItemTime.Description; // gloDateMaster.gloTime.TimeAsDateTime(Convert.ToDateTime(dtStartDate.AddDays(j)), Convert.ToInt32(nClinicEndTime)).ToShortTimeString();
        //        //                        for (int i = 0; i < oItemTime.SubItems.Count; i++)
        //        //                        {
        //        //                            r["ProblemID"] = Convert.ToString(r["ProblemID"]) + oItemTime.SubItems[i].Code + ", ";
        //        //                            r["ProblemType"] = Convert.ToString(r["ProblemType"]) + oItemTime.SubItems[i].Description + ", ";
        //        //                        }
        //        //                        // Remove last coma ","
        //        //                        if (Convert.ToString(r["ProblemType"]).Length > 0)
        //        //                        {
        //        //                            r["ProblemType"] = Convert.ToString(r["ProblemType"]).Substring(0, Convert.ToString(r["ProblemType"]).Length - 2);
        //        //                        }
        //        //                        dtTemp.Rows.Add(r);
        //        //                        //nTempStartTime = gloDateMaster.gloTime.TimeAsNumber(oItemTime.Description.ToString());
        //        //                    } //  for (int k = 0; k < oFreeSlotItems.Count; k++)
        //        //                }//for (int n = 0; n < dtSchedule.Rows.Count; n++)
        //        //            }// if (strProcedureIDs.Trim() != "")
        //        //            #endregion

        //        //        } //     for (int l = 0; l < oProviderItems.Count; l++)
        //        //    } // for (int m = 0; m <= _FindDates.Count - 1; m++)




        //        //} // try

        //        //catch (Exception ex)
        //        //{
        //        //    return null;
        //        //}
        //        //return dtTemp;
        //        return null;
        //    }

        //    /// <summary>
        //    ///     Function to calculate the time intervals between the given StartTime and EndTime 
        //    ///     depending on the given interval duration.
        //    /// </summary>

        //    /// <returns>
        //    ///     A gloGeneralItem.gloItems value...
        //    /// </returns>
        //    /// 
        //    //
        //    private gloItems SplitDuration_1(DateTime sTime, DateTime eTime, Int64 duration)
        //    {

        //        gloGeneralItem.gloItems oItems = new gloGeneralItem.gloItems();

        //        //Converting the duration into ticks.
        //        TimeSpan tsDuration = new TimeSpan(duration);
        //        DateTime dtTemSTime = sTime;
        //        DateTime dtTemETime = eTime;
        //        TimeSpan ts;
        //        //Calculating the time span between the start & end time.
        //        ts = eTime.Subtract(dtTemSTime);

        //        for (int i = 0; ts.TotalMinutes >= tsDuration.Ticks; i++)
        //        {
        //            gloGeneralItem.gloItem oItem = new gloGeneralItem.gloItem();

        //        //    dtTemSTime = dtTemSTime;
        //            dtTemETime = dtTemSTime.AddMinutes(duration);

        //            oItem.Code = dtTemSTime.ToString("hh:mm tt");
        //            oItem.Description = dtTemETime.ToString("hh:mm tt");

        //            oItems.Add(oItem);
        //            oItem.Dispose();
        //            oItem = null;
        //            // Initialize the Date Time for the Next Interval
        //            dtTemSTime = dtTemETime;
        //            dtTemETime = dtTemSTime.AddMinutes(duration);

        //            ts = eTime.Subtract(dtTemSTime);
        //        }

        //        return oItems;
        //    }

        //    //#endregion

        //    #region UI Methods

        //    public void ShowSearchAppointment(System.Windows.Forms.Form oParentWindow)
        //    {
        //        frmSearchAppointment oSearchAppointment = new frmSearchAppointment();
        //        oSearchAppointment.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        //        oSearchAppointment.MdiParent = oParentWindow;
        //        oSearchAppointment.Show();
        //    }

        //    public void ShowDialogSearchAppointment()
        //    {
        //        frmSearchAppointment oSearchAppointment = new frmSearchAppointment();
        //        //oSearchAppointment.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        //        //oSearchAppointment.MdiParent = oParentWindow;
        //        oSearchAppointment.ShowInTaskbar = false;
        //        oSearchAppointment.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        //        oSearchAppointment.ShowDialog(oSearchAppointment.Parent);
        //        oSearchAppointment.Dispose();
        //        oSearchAppointment = null;
        //    }

        //    public void ShowSearchSchedule(System.Windows.Forms.Form oParentWindow)
        //    {
        //        //frmSearchSchedule oSearchSchedule = new frmSearchSchedule(_databaseconnectionstring);

        //        //oSearchSchedule.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        //        //oSearchSchedule.MdiParent = oParentWindow;

        //        //oSearchSchedule.Show();
        //    }

        //    public void ShowDialogSearchSchedule()
        //    {
        //        //frmSearchSchedule oSearchSchedule = new frmSearchSchedule(_databaseconnectionstring);

        //        //oSearchSchedule.ShowInTaskbar = false;
        //        //oSearchSchedule.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        //        //oSearchSchedule.ShowDialog();
        //    }

        //    public void ShowSearchAppointmentSchedule(System.Windows.Forms.Form oParentWindow)
        //    {
        //        //frmSearchAppointmentSchedule oSearchAppointmentSchedule = new frmSearchAppointmentSchedule(_databaseconnectionstring);
        //        //oSearchAppointmentSchedule.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        //        //oSearchAppointmentSchedule.MdiParent = oParentWindow;
        //        //oSearchAppointmentSchedule.Show();
        //    }

        //    #endregion
        //}
    }
}
