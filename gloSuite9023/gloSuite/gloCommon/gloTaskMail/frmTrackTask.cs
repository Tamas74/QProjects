using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloTaskMail
{
    public partial class frmTrackTask : Form, IDisposable 
    {

        //Sanjog - Optimize the COde
        int COL_TaskID = 0;
        int COL_DueDate = 1;
        int COL_Image = 2;
        int COL_Subject = 3;
        int COL_PatientID = 4;
        int COL_Patient_Name = 5;
        int COL_ProviderID = 6;
        int COL_Provider_Name = 7;
        int COL_AssignedToID = 8;
        int COL_Assigned_TO = 9;
        int COL_Status = 10;
        int COL_Description = 11;
        int COL_Count = 12; 

        //Sanjog - Optimize the COde
        private string _databaseconnnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _userId = 0;
        private Int64 _SelTaskId = 0;
        System.Drawing.Image imgHigh = global::gloTaskMail.Properties.Resources.High_PriorityRed;
        System.Drawing.Image imgLow = global::gloTaskMail.Properties.Resources.Low_Priority;

        C1.Win.C1FlexGrid.CellStyle csAcceptedTask = null;
        C1.Win.C1FlexGrid.CellStyle csRejectedTask = null;

        #region "IDisposable Interface"
        private bool disposed = false;

        public void Disposer()
        {
            Dispose(true);
            //GC.Collect();
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if (disposing)
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    // Dispose managed resources.
                    components.Dispose();
                }

                base.Dispose(disposing);
                _databaseconnnectionstring = null;
                _messageBoxCaption = null;

                if (imgHigh != null)
                {
                    imgHigh.Dispose();
                    imgHigh = null;
                }
                if (imgLow != null)
                {
                    imgLow.Dispose();
                    imgLow = null;
                }

                disposed = true;
            }
        }
        ~frmTrackTask()
     {
     	Dispose(false);
     }
        #endregion "IDisposable Interface"

     public frmTrackTask()
        {
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            _databaseconnnectionstring = appSettings["DataBaseConnectionString"].ToString();

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _userId = Convert.ToInt64(appSettings["UserID"]); }
                else { _userId = 0; }
            }
            else
            { _userId = 0; }

            //Added By Pramod Nair For Messagebox Caption 
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
            InitializeComponent(); 
        }

        private void frmTrackTask_Load(object sender, EventArgs e)
     {
       
            try
            {                
                gloC1FlexStyle.Style(c1TaskRequest, false);
          
                designC1TaskRequest_New();

               // csAcceptedTask = c1TaskRequest.Styles.Add("cs_AcceptedTask");
                try
                {
                    if (c1TaskRequest.Styles.Contains("cs_AcceptedTask"))
                    {
                        csAcceptedTask = c1TaskRequest.Styles["cs_AcceptedTask"];
                    }
                    else
                    {
                        csAcceptedTask = c1TaskRequest.Styles.Add("cs_AcceptedTask");

                    }

                }
                catch
                {
                    csAcceptedTask = c1TaskRequest.Styles.Add("cs_AcceptedTask");


                }
                csAcceptedTask.ForeColor = Color.Green;
              //  csRejectedTask = c1TaskRequest.Styles.Add("cs_RejectedTask");
                try
                {
                    if (c1TaskRequest.Styles.Contains("cs_RejectedTask"))
                    {
                        csRejectedTask = c1TaskRequest.Styles["cs_RejectedTask"];
                    }
                    else
                    {
                        csRejectedTask = c1TaskRequest.Styles.Add("cs_RejectedTask");

                    }

                }
                catch
                {
                    csRejectedTask = c1TaskRequest.Styles.Add("cs_RejectedTask");


                }
                csRejectedTask.ForeColor = Color.Red;

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }            
        }
        DataTable dtTaskRequest = null; //SLR: 'New is not needed
        private void designC1TaskRequest_New(long Seltaskid=0)
        {
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
           
            try
            {
                _SelTaskId = Seltaskid; 
                c1TaskRequest.BeginUpdate();
               // c1TaskRequest.Clear();
                c1TaskRequest.DataSource = null;
                c1TaskRequest.Cols.Count = COL_Count;
                c1TaskRequest.Rows.Count = 1;
                c1TaskRequest.Cols.Fixed = 0;                

                c1TaskRequest.SelectionMode = SelectionModeEnum.Row;                
                c1TaskRequest.AllowResizing = AllowResizingEnum.Columns;
                C1.Win.C1FlexGrid.CellStyle csAcceptedTask ;//= c1TaskRequest.Styles.Add("cs_AcceptedTask");
                try
                {
                    if (c1TaskRequest.Styles.Contains("cs_AcceptedTask"))
                    {
                        csAcceptedTask = c1TaskRequest.Styles["cs_AcceptedTask"];
                    }
                    else
                    {
                        csAcceptedTask = c1TaskRequest.Styles.Add("cs_AcceptedTask");

                    }

                }
                catch
                {
                    csAcceptedTask = c1TaskRequest.Styles.Add("cs_AcceptedTask");


                }
                csAcceptedTask.ForeColor = Color.Green;
                C1.Win.C1FlexGrid.CellStyle csRejectedTask;// = c1TaskRequest.Styles.Add("cs_RejectedTask");
                try
                {
                    if (c1TaskRequest.Styles.Contains("cs_RejectedTask"))
                    {
                        csRejectedTask = c1TaskRequest.Styles["cs_RejectedTask"];
                    }
                    else
                    {
                        csRejectedTask = c1TaskRequest.Styles.Add("cs_RejectedTask");

                    }

                }
                catch
                {
                    csRejectedTask = c1TaskRequest.Styles.Add("cs_RejectedTask");


                }
                csRejectedTask.ForeColor = Color.Red;
               
                c1TaskRequest.DrawMode = DrawModeEnum.Normal;

                dtTaskRequest = ogloTask.GetUserTrackTaskRequests_New(_userId);
               string userids= getSelectedUserIDs();
                if ((colName.Trim() != "") && (strSortOrder.Trim() != ""))
                {
                    DataView dv = dtTaskRequest.DefaultView;
                    if (userids.Trim() != "")
                    {
                        dv.RowFilter = "AssignedToID in(" + userids + ")";
                    }
                    dv.Sort = colName + " " + strSortOrder;
                    dtTaskRequest = dv.ToTable();
                    dv.Dispose();
                    dv = null;
                }

                if (colno == -1)
                {
                    DataView dv = dtTaskRequest.DefaultView;
                    if (userids.Trim() != "")
                    {
                        dv.RowFilter = "AssignedToID in(" + userids + ")";
                    }
                    dv.Sort = "[Due Date] Desc ,Subject Desc";
                    dtTaskRequest = dv.ToTable();
                    dv.Dispose();
                    dv = null;
                   // c1TaskRequest.Sort(SortFlags.Descending, COL_DueDate, COL_Subject);
                }
                c1TaskRequest.DataSource = dtTaskRequest;

                c1TaskRequest.SetData(0, COL_TaskID, "TaskID");
                c1TaskRequest.SetData(0, COL_DueDate, "Due Date");
                c1TaskRequest.SetData(0, COL_Image, "");
                c1TaskRequest.SetData(0, COL_Subject, "Subject");
                c1TaskRequest.SetData(0, COL_PatientID, "PatientID");
                c1TaskRequest.SetData(0, COL_Patient_Name, "Patient Name");
                c1TaskRequest.SetData(0, COL_ProviderID, "ProviderID");
                c1TaskRequest.SetData(0, COL_Provider_Name, "Provider Name");
                c1TaskRequest.SetData(0, COL_AssignedToID, "AssignedToID");
                c1TaskRequest.SetData(0, COL_Assigned_TO, "Assigned To");
                c1TaskRequest.SetData(0, COL_Status, "Status");
                c1TaskRequest.SetData(0, COL_Description, "Description");

                int nWidth;
                nWidth = pnlGridAssign.Width;                
                c1TaskRequest.Cols[COL_TaskID].Width = 0; // TASKID
                c1TaskRequest.Cols[COL_DueDate].Width = Convert.ToInt32(nWidth * 0.09);
                c1TaskRequest.Cols[COL_Image].Width = 16;
                c1TaskRequest.Cols[COL_Subject].Width = Convert.ToInt32(nWidth * 0.22);//Subject
                c1TaskRequest.Cols[COL_PatientID].Width = 0;//AssignedToID
                c1TaskRequest.Cols[COL_Patient_Name].Width = Convert.ToInt32(nWidth * 0.13);
                c1TaskRequest.Cols[COL_ProviderID].Width = 0;
                c1TaskRequest.Cols[COL_Provider_Name].Width = Convert.ToInt32(nWidth * 0.11);
                c1TaskRequest.Cols[COL_AssignedToID].Width = 0;//AssignedToID
                c1TaskRequest.Cols[COL_Assigned_TO].Width = Convert.ToInt32(nWidth * 0.09);
                c1TaskRequest.Cols[COL_Status].Width = Convert.ToInt32(nWidth * 0.12);//Status
                c1TaskRequest.Cols[COL_Description].Width = Convert.ToInt32(nWidth * 0.20);//Status

                 c1TaskRequest.AllowEditing = false;

                c1TaskRequest.EndUpdate();
                c1TaskRequest.DrawMode = DrawModeEnum.OwnerDraw;

                if (Seltaskid != 0)
                {
                    selectRecord(Seltaskid);
                }
               }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtTaskRequest != null)
                {
                    dtTaskRequest = null;
                }
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                    ogloTask = null;
                }
                EnableEditButton();
            }
        }
        //filter the data by selected users
        private void FilterBySelectedUsers(string struserids)
        {

            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            DataTable dtFilterData = null;
            try
            {
                //_SelTaskId = Seltaskid;
                c1TaskRequest.BeginUpdate();
                // c1TaskRequest.Clear();
                c1TaskRequest.DataSource = null;
                c1TaskRequest.Cols.Count = COL_Count;
                c1TaskRequest.Rows.Count = 1;
                c1TaskRequest.Cols.Fixed = 0;

                c1TaskRequest.SelectionMode = SelectionModeEnum.Row;
                c1TaskRequest.AllowResizing = AllowResizingEnum.Columns;
                C1.Win.C1FlexGrid.CellStyle csAcceptedTask;//= c1TaskRequest.Styles.Add("cs_AcceptedTask");
                try
                {
                    if (c1TaskRequest.Styles.Contains("cs_AcceptedTask"))
                    {
                        csAcceptedTask = c1TaskRequest.Styles["cs_AcceptedTask"];
                    }
                    else
                    {
                        csAcceptedTask = c1TaskRequest.Styles.Add("cs_AcceptedTask");

                    }

                }
                catch
                {
                    csAcceptedTask = c1TaskRequest.Styles.Add("cs_AcceptedTask");


                }
                csAcceptedTask.ForeColor = Color.Green;
                C1.Win.C1FlexGrid.CellStyle csRejectedTask;// = c1TaskRequest.Styles.Add("cs_RejectedTask");
                try
                {
                    if (c1TaskRequest.Styles.Contains("cs_RejectedTask"))
                    {
                        csRejectedTask = c1TaskRequest.Styles["cs_RejectedTask"];
                    }
                    else
                    {
                        csRejectedTask = c1TaskRequest.Styles.Add("cs_RejectedTask");

                    }

                }
                catch
                {
                    csRejectedTask = c1TaskRequest.Styles.Add("cs_RejectedTask");


                }
                csRejectedTask.ForeColor = Color.Red;

                c1TaskRequest.DrawMode = DrawModeEnum.Normal;
                if (dtTaskRequest == null)
                {
                    dtTaskRequest = ogloTask.GetUserTrackTaskRequests_New(_userId);
                }
                dtFilterData = dtTaskRequest;
               // string[] arruserids = { "Red", "Green", "Blue" };
               // var filteredRows = dtTaskRequest.AsEnumerable()
               //.Where(row => arruserids.Contains(row.Field<string>("Status")));
               
              
                DataView dv = dtFilterData.DefaultView;
                if ((colName.Trim() != "") && (strSortOrder.Trim() != ""))
                {
                    if (struserids.Trim() != "")  
                    dv.RowFilter = "AssignedToID in(" + struserids + ")"; 
                    dv.Sort = colName + " " + strSortOrder;
                    dtFilterData = dv.ToTable();
                    dv.Dispose();
                    dv = null;
                }

                if (colno == -1)
                {
                   // DataView dv = dtFilterData.DefaultView;
                 if(struserids.Trim()!="")  
                    dv.RowFilter = "AssignedToID in(" + struserids + ")"; 
                    dv.Sort = "[Due Date] Desc ,Subject Desc";
                    dtFilterData = dv.ToTable();
                    dv.Dispose();
                    dv = null;
                    // c1TaskRequest.Sort(SortFlags.Descending, COL_DueDate, COL_Subject);
                }
                c1TaskRequest.DataSource = dtFilterData;

                c1TaskRequest.SetData(0, COL_TaskID, "TaskID");
                c1TaskRequest.SetData(0, COL_DueDate, "Due Date");
                c1TaskRequest.SetData(0, COL_Image, "");
                c1TaskRequest.SetData(0, COL_Subject, "Subject");
                c1TaskRequest.SetData(0, COL_PatientID, "PatientID");
                c1TaskRequest.SetData(0, COL_Patient_Name, "Patient Name");
                c1TaskRequest.SetData(0, COL_ProviderID, "ProviderID");
                c1TaskRequest.SetData(0, COL_Provider_Name, "Provider Name");
                c1TaskRequest.SetData(0, COL_AssignedToID, "AssignedToID");
                c1TaskRequest.SetData(0, COL_Assigned_TO, "Assigned To");
                c1TaskRequest.SetData(0, COL_Status, "Status");
                c1TaskRequest.SetData(0, COL_Description, "Description");

                int nWidth;
                nWidth = pnlGridAssign.Width;
                c1TaskRequest.Cols[COL_TaskID].Width = 0; // TASKID
                c1TaskRequest.Cols[COL_DueDate].Width = Convert.ToInt32(nWidth * 0.09);
                c1TaskRequest.Cols[COL_Image].Width = 16;
                c1TaskRequest.Cols[COL_Subject].Width = Convert.ToInt32(nWidth * 0.22);//Subject
                c1TaskRequest.Cols[COL_PatientID].Width = 0;//AssignedToID
                c1TaskRequest.Cols[COL_Patient_Name].Width = Convert.ToInt32(nWidth * 0.13);
                c1TaskRequest.Cols[COL_ProviderID].Width = 0;
                c1TaskRequest.Cols[COL_Provider_Name].Width = Convert.ToInt32(nWidth * 0.11);
                c1TaskRequest.Cols[COL_AssignedToID].Width = 0;//AssignedToID
                c1TaskRequest.Cols[COL_Assigned_TO].Width = Convert.ToInt32(nWidth * 0.09);
                c1TaskRequest.Cols[COL_Status].Width = Convert.ToInt32(nWidth * 0.12);//Status
                c1TaskRequest.Cols[COL_Description].Width = Convert.ToInt32(nWidth * 0.20);//Status

                c1TaskRequest.AllowEditing = false;

                c1TaskRequest.EndUpdate();
                c1TaskRequest.DrawMode = DrawModeEnum.OwnerDraw;

                //if (Seltaskid != 0)
                //{
                //    selectRecord(Seltaskid);
                //}
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtTaskRequest != null)
                {
                    dtTaskRequest = null;
                }
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                    ogloTask = null;
                }
                EnableEditButton();
            }
        }
      
        private void selectRecord(Int64 SelTaskid)
        {
            DataTable dtdata =(DataTable ) c1TaskRequest.DataSource;
            if (dtdata != null)
            {
      DataRow []drr=      dtdata.Select("TaskID="+SelTaskid.ToString()+"");
      if (drr.Length > 0)
      {
        int ind=  dtdata.Rows.IndexOf(drr[0]);
        ind = ind + 1;
        if (c1TaskRequest.Rows.Count >= ind)
        {
            c1TaskRequest.Select( ind,0); 
        }
      }
            }
        }
        //SAnjog Bind Data from DataTable only
        private void EnableEditButton()
        {
            if (c1TaskRequest.RowSel >= 0)
            {
                if ((Convert.ToString(c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_Status)) != "Deleted") && (Convert.ToString(c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_Status)) != "Not Yet Accepted"))
                {
                    tsb_Modify.Enabled = true;
                  
                }
                else
                {
                    tsb_Modify.Enabled = false;
                   
                }
                ts_Commands.Refresh(); //added for bugid 87853
            }
        }
        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            Int64 tempTaskId = 0;
            Int64 assignToId = 0;
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            DataTable dt = null; //SLR: new is not needed
            c1TaskRequest.OwnerDrawCell -= c1TaskRequest_OwnerDrawCell;   
            try
            {
                //Added By MaheshB 
                if (c1TaskRequest.Rows.Count <= 1)
                {
                    return;
                }

                if (c1TaskRequest.RowSel > 0)
                {
                    if (Convert.ToString (c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_Status)) != "Deleted")
                    {

                    DialogResult dr = MessageBox.Show("Are you sure you want to delete task ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.Yes)
                    {
                        if (c1TaskRequest != null && c1TaskRequest.Rows.Count > 0)
                        {
                            if (c1TaskRequest.RowSel > 0)
                            {
                                tempTaskId = Convert.ToInt64(c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_TaskID));
                                assignToId = Convert.ToInt64(c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_AssignedToID));

                                if (ogloTask.CanDeleteTask(tempTaskId) == true)
                                {
                                    ogloTask.DeleteRequestedTask(tempTaskId, assignToId);
                                    dt = ogloTask.get_multiTask(tempTaskId);
                                    if (dt.Rows.Count == 0)
                                    {
                                        ogloTask.DeleteTask(tempTaskId);
                                    }
                                    designC1TaskRequest_New();
                                    
                                }
                                else
                                {
                                    MessageBox.Show("Cannot delete message . The message is assigned and is on hold", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }

                }

                else
                
                {
                    //28-Jan-15 Aniket: Resolving Bug #62669:
                    MessageBox.Show("Task with 'Deleted' Status cannot be deleted", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                }
               

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                c1TaskRequest.OwnerDrawCell -= c1TaskRequest_OwnerDrawCell;
                c1TaskRequest.OwnerDrawCell += c1TaskRequest_OwnerDrawCell;   
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                    ogloTask = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }


        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                designC1TaskRequest_New();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (c1TaskRequest != null && c1TaskRequest.Rows.Count > 0)
                {
                    if (c1TaskRequest.RowSel >= 0)
                    {
                       
                            ModifyTask();
                      
                        
                        //designC1TaskRequest_New();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void ModifyTask()
        {
            Int64 tempTaskId = 0;
            Int64 tempAssignToId = 0;
            Int64 _PatientId = 0;
            string users = "";
            int count;
            gloTask ogloTask = new gloTask(_databaseconnnectionstring);
            DataTable dt = null; //SLR: neew not neededd
            try
            {
                if (c1TaskRequest.Rows.Count <= 1)
                {
                    return;
                }
                else if (c1TaskRequest.RowSel <= 0)
                {
                    return;
                }

                tempTaskId = Convert.ToInt64(c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_TaskID));
                tempAssignToId = Convert.ToInt64(c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_AssignedToID));
                _PatientId = Convert.ToInt64(c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_PatientID));
                dt = ogloTask.get_multiTask(tempTaskId);
                count = 0;                
                if (dt != null || dt.Rows.Count > 0)
                {
                    while (count < dt.Rows.Count)
                    {
                        if (users == "")
                        {
                            users = dt.Rows[count][1].ToString();
                        }
                        else
                        {
                            users = users + "," + dt.Rows[count][1].ToString();
                        }
                        count++;
                    }

                }

                gloTaskMail.frmTask ofrmTask = new gloTaskMail.frmTask(_databaseconnnectionstring, tempTaskId, true, users);
                ofrmTask.TaskAssigntoID = Convert.ToInt64(c1TaskRequest.GetData(c1TaskRequest.RowSel, COL_AssignedToID));
                ofrmTask.IsEMREnable = false;
                ofrmTask.PatientID = _PatientId; 
                 ofrmTask.ShowDialog(this);
                 if (ofrmTask != null)
                 {
                     ofrmTask.Close();
                     ofrmTask.Dispose();
                 }
                    ofrmTask = null;

               designC1TaskRequest_New(tempTaskId);
               ts_Commands.Refresh();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
          
            }
            finally
            {

               if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }                
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                    ogloTask = null;
                }
            
           }
       }

        private void c1TaskRequest_DoubleClick(object sender, EventArgs e)
        {
            try
            {
             
             
         
                if (c1TaskRequest.RowSel >= 0)
                {
                    if (tsb_Modify.Enabled == true)
                    {
                      HitTestInfo htinfo=  c1TaskRequest.HitTest();
                      if (htinfo.Row > 0)
                      {
                          ModifyTask();
                      }
                     }
                     
                   
                }
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        // code to show tooltip  - pradeep 20100722

        private void c1TaskRequest_MouseMove(object sender, MouseEventArgs e)
        {
          
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
           
        }

        private void c1TaskRequest_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {            
            try
            {
                if (c1TaskRequest.GetCellImage(e.Row, COL_Image) != null && c1TaskRequest.GetCellStyle(e.Row, COL_Status) != null)
                //&& c1TaskRequest.GetCellImage(e.Row, COL_TaskID) != null)
                {
                    return;
                }

                if (e.Col == COL_Status)
                {
                    string reqStatus = c1TaskRequest.GetData(e.Row, COL_Status).ToString();
                    switch (reqStatus)
                    {
                        case "Not Started":
                            c1TaskRequest.SetCellStyle(e.Row, COL_Status, csAcceptedTask);
                            break;
                        case "In Progress":
                            c1TaskRequest.SetCellStyle(e.Row, COL_Status, csAcceptedTask);
                            break;
                        case "Completed":
                            c1TaskRequest.SetCellStyle(e.Row, COL_Status, csAcceptedTask);
                            break;
                        case "Deferred":
                            c1TaskRequest.SetCellStyle(e.Row, COL_Status, csAcceptedTask);
                            break;
                        case "On Hold":
                            c1TaskRequest.SetCellStyle(e.Row, COL_Status, csAcceptedTask);
                            break;
                        case "Declined":
                            c1TaskRequest.SetCellStyle(e.Row, COL_Status, csRejectedTask);
                            break;
                    }
                }

                if (e.Col == COL_Image)
                {
                    if (Convert.ToInt64(c1TaskRequest.GetData(e.Row, e.Col)) == 1)
                    {
                        c1TaskRequest.SetCellImage(e.Row, e.Col, imgHigh);
                    }
                    else if (Convert.ToInt64(c1TaskRequest.GetData(e.Row, e.Col)) == 2)
                    {
                        c1TaskRequest.SetData(e.Row, e.Col, "");
                    }
                    else if (Convert.ToInt64(c1TaskRequest.GetData(e.Row, e.Col)) == 3)
                    {
                        c1TaskRequest.SetCellImage(e.Row, e.Col, imgLow);
                    }
                    else
                    {
                        c1TaskRequest.SetData(e.Row, e.Col, "");
                    }

                }

                
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void c1TaskRequest_Click(object sender, EventArgs e)
        {
            EnableEditButton();
        }

        private void frmTrackTask_Shown(object sender, EventArgs e)
        {
           ts_Commands.Refresh(); //added for bugid 80341 toolbar not displaying
           panel1.Refresh();   //added for bugid 99078
            AdddefaultUser(); 
        }
        string strSortOrder = "";
        int colno = -1;
        string colName = "";
        private void c1TaskRequest_AfterSort(object sender, SortColEventArgs e)
        {
           if (e.Order == SortFlags.Ascending) 
           
               strSortOrder = "Asc";
           else
            strSortOrder = "Desc";
            colno = e.Col;
            switch (colno)
            {
                case 0: colName = "TaskID";
                break;
                case 1: colName = "Due Date";
                break;

                case 2: colName = "PriorityLevel";
                break;
                case 3: colName = "Subject";
                break;
                case 4: colName = "PatientID";
                break;
                case 5: colName = "Patient Name";
                break;
                case 6: colName = "ProviderID";
                break;
                case 7: colName = "Provider Name";
                break;
                case 8: colName = "AssignToID";
                break;
                case 9: colName = "Assigned To"; //bugid 98927 
                break;
                case 10: colName = "Status";
                break;
                case 11: colName = "Description";
                break;

            }
            EnableEditButton();
        }
        private gloListControl.gloListControl oListUsers;
        gloGeneralItem.gloItems ToList;
        private void btnToBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                ts_Commands.Enabled = false;
                panel1.Enabled = false;
                

                if (oListUsers != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListUsers.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                       
                    }
                   
                    try
                    {
                        oListUsers.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListUsers_ItemSelectedClick);
                        oListUsers.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListUsers_ItemClosedClick);

                    }
                    catch { }
                    oListUsers.Dispose();
                    oListUsers = null;
                }

                oListUsers = new gloListControl.gloListControl(_databaseconnnectionstring, gloListControl.gloListControlType.Users, true, this.Width);

                oListUsers.ControlHeader = "Users";
                oListUsers.IsgloCollectCustomer = true; // Sameer Added For User sorting based on non glocollect and glocollectusers 11/26/2014
                oListUsers.bchkIncludeAllUsers = false;
                oListUsers.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListUsers_ItemSelectedClick);
                oListUsers.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListUsers_ItemClosedClick);
                oListUsers.Dock = DockStyle.Fill;

                // to select already added users
                if (ToList != null)
                {
                    for (int i = 0; i < ToList.Count; i++)
                    {
                        oListUsers.SelectedItems.Add(ToList[i]);
                    }
                }
               
                

                this.Controls.Add(oListUsers);
                oListUsers.tsb_UserGroups.Visible = false;
                //

                //
                oListUsers.OpenControl();
                oListUsers.Dock = DockStyle.Fill;
                oListUsers.BringToFront();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            //finally
            //{
            //    ts_Commands.Enabled = true;
            //}
        }

        //Users List
        private void oListUsers_ItemClosedClick(object sender, EventArgs e)
        {
           
            //added for bugid 98909   
            ts_Commands.Enabled = true;

                panel1.Enabled = true;  

        }
        Boolean bIncludeAllUsers = false; 
        private void oListUsers_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                //cmb_To.Items.Clear(); 
                DataTable dtUsers = new DataTable();
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");
                Int64[] UserIDs = new Int64[2];
                dtUsers.Columns.Add(dcId);
                dtUsers.Columns.Add(dcDescription);


                ToList = new gloGeneralItem.gloItems();
                gloGeneralItem.gloItem ToItem;
                Array.Resize(ref UserIDs, oListUsers.SelectedItems.Count);
                if (oListUsers.SelectedItems.Count > 0)
                {

                    for (Int16 i = 0; i <= oListUsers.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtUsers.NewRow();
                        drTemp["ID"] = oListUsers.SelectedItems[i].ID;
                        drTemp["Description"] = oListUsers.SelectedItems[i].Description;

                        dtUsers.Rows.Add(drTemp);
                        UserIDs[i] = Convert.ToInt64(drTemp["ID"]);
                        //
                        ToItem = new gloGeneralItem.gloItem();

                        ToItem.ID = oListUsers.SelectedItems[i].ID;
                        ToItem.Description = oListUsers.SelectedItems[i].Description;


                        ToList.Add(ToItem);
                        ToItem.Dispose();
                        ToItem = null;

                        //
                    }
                }
                else
                {
                    //bugid 106944 adding bydefault all
                    dtUsers = (DataTable)cmbToUsers.DataSource;
                    if (dtUsers != null)
                    {
                        dtUsers.Rows.Clear();   
                    }
                     
                    AdddefaultUser();
                    return;
                }




                dtUsers.DefaultView.Sort = "Description";
                dtUsers = dtUsers.DefaultView.ToTable();

                //Int64 nDefaultUser = 0;
                //if (_defaultuserID > 0)
                //{
                //    nDefaultUser = _defaultuserID;
                //}
                //else
                //{
                //    nDefaultUser = _userID;

                //}
                //DataRow[] dr = dtUsers.Select("ID ='" + nDefaultUser + "'");
                //if (dr.Length > 0)
                //{
                //    DataRow newRow = dtUsers.NewRow();
                //    newRow.ItemArray = dr[0].ItemArray;
                //    dtUsers.Rows.Remove(dr[0]);
                //    dtUsers.Rows.InsertAt(newRow, 0);

                //}

                cmbToUsers.DataSource = dtUsers;
                cmbToUsers.ValueMember = dtUsers.Columns["ID"].ColumnName;
                cmbToUsers.DisplayMember = dtUsers.Columns["Description"].ColumnName;



                // oListUsers 
                oListUsers.IsgloCollectCustomer = false;
                bIncludeAllUsers = oListUsers.bchkIncludeAllUsers;
                string struserids = "";
                for (int i = 0; i < UserIDs.Length; i++)
                {
                    struserids += UserIDs[i].ToString() + ",";
                }
                if (struserids.Length > 0)
                {
                    struserids = struserids.Substring(0, struserids.Length - 1);
                    FilterBySelectedUsers(struserids);
                }
               

            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                //added for bugid 98909   
                ts_Commands.Enabled = true;

                panel1.Enabled = true;  
            }

        }
        private string getSelectedUserIDs()
        {
            string userids = "";

            DataTable dtusers =(DataTable)  cmbToUsers.DataSource;
            if(dtusers!=null)
              {
                  foreach (DataRow dr in dtusers.Rows)
                  {
                     if((Convert.ToString (dr["ID"]))!="-1")
                      userids += dr["ID"].ToString() + ",";
                  }
              }
            if (userids.Length > 0)
            {
                userids = userids.Substring(0, userids.Length - 1);     
            }
            return userids; 
        }
        //assignto filter added 
        private void AdddefaultUser()
        {
            DataTable dtUsers =(DataTable)cmbToUsers.DataSource;
            if (dtUsers == null)
            {
                dtUsers = new DataTable(); 
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");
                
                dtUsers.Columns.Add(dcId);
                dtUsers.Columns.Add(dcDescription);
            }
            DataRow dr = dtUsers.NewRow();
            dr["ID"] = -1;
            dr["Description"] = "All";
            dtUsers.Rows.Add(dr);
            dtUsers.AcceptChanges();
            cmbToUsers.SelectedIndexChanged -= cmbToUsers_SelectedIndexChanged;
            cmbToUsers.DataSource = dtUsers;
            cmbToUsers.DisplayMember = "Description";
            cmbToUsers.ValueMember = "ID";  
           
            cmbToUsers.SelectedIndexChanged += cmbToUsers_SelectedIndexChanged;
            cmbToUsers.Refresh();  
        }
        //added to delete the selected user from assignto dropdown
        private void btnToDelete_Click(object sender, EventArgs e)
        {

            Int64 _userId = 0;
            try
            {
                //Remove item from ToList

                _userId = Convert.ToInt64(cmbToUsers.SelectedValue);
                //SLR: Changed on 4/4/2014
                for (int i = ToList.Count - 1; i >= 0; i--)
                {
                    if (ToList[i].ID == _userId)
                    {
                        ToList.RemoveAt(i);
                    }
                }

                //
                if (_userId > 0)
                {
                    DataTable dtUsers = (DataTable)cmbToUsers.DataSource;
                    dtUsers.Rows.RemoveAt(cmbToUsers.SelectedIndex);
                    dtUsers.AcceptChanges();
                    cmbToUsers.DataSource = dtUsers;
                    cmbToUsers.Refresh();
                    //8060 Code Change to avoid exception
                    if (dtUsers.Rows.Count == 0)
                    {
                        AdddefaultUser(); 
                        //DataRow dr = dtUsers.NewRow();
                        //dr["ID"] = -1;
                        //dr["Description"] = "All";
                        //dtUsers.Rows.Add(dr);
                        //dtUsers.AcceptChanges();
                        //cmbToUsers.SelectedValue = -1;
                    }
                    if (dtUsers.Rows.Count > 0)
                    {
                        cmbToUsers.SelectedIndex = 0;
                    }
                    FilterBySelectedUsers(getSelectedUserIDs());
                }
                else
                {
                    DataTable dtUsers = (DataTable)cmbToUsers.DataSource;
                   dtUsers.Rows.Clear();
                   AdddefaultUser(); 
                    //DataRow dr=  dtUsers.NewRow();
                  //dr["ID"] = -1;
                  //dr["Description"] = "All";
                  //dtUsers.Rows.Add(dr);  
                  //  dtUsers.AcceptChanges();
                  // // cmbToUsers.Items.Add("All");
                  // // cmbToUsers.SelectedValue = 0;
                  //  cmbToUsers.SelectedValue = -1;  
                    FilterBySelectedUsers("");
                }
                cmbToUsers.Refresh();  
            
                }
catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }


        }

        private void cmbToUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            cmbToUsers.SelectedIndexChanged -= cmbToUsers_SelectedIndexChanged;
            if (cmbToUsers.Text.Trim() != "")
            {
                Int64 cmbuserId = Convert.ToInt64(cmbToUsers.SelectedValue);
                if (cmbuserId <= 0)
                {

                    FilterBySelectedUsers("");
                }
                //else
                //{
                //    FilterBySelectedUsers(getSelectedUserIDs() );
                //}
            }

            cmbToUsers.SelectedIndexChanged += cmbToUsers_SelectedIndexChanged;
         }

      
    }
}

