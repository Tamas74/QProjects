﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloReports
{
    public partial class frmInterfacesMessageQueueViewReport : Form
    {
        # region "Global Variables"
        Int32 iStartIndex = 0;
        Int32 iEndIndex = 0;
        Int32 iPageNumber = 1;
        Int32 iTotalPages = 1;
        Int32 iLogCount = 0;
        Int32 iPageSize = 0;
        String strConnectionString = String.Empty;
        String gstrMessageBoxCaption=String.Empty;
        DataTable dtMessageLog = null;
        //global variables used to designate columns in C1Flex grid for HL7 service
        int mColIndex_PateintId = 0;
        int mColIndex_DateTimeStamp = 1;
        int mColIndex_Hl7Message = 4;//2;
        int mColIndex_MachineName =5 ;//3;
        int mColIndex_PatientCode = 2;//4;
        int mColIndex_Patient = 3;//
        int mColIndex_Status = 6;
        int mColIndex_Date = 7;
        //int mCol_MsgID = 8;
        //global variables used to designate columns for other services
        int ColIndex_DateTimeStamp = 0;
        int ColIndex_MessageName = 1;
        int ColIndex_MachineName = 2;
        int ColIndex_PatientCode = 3;
        int ColIndex_Patient = 4;
        int ColIndex_Status =5;
       

        bool _isFormLoaded = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion "Global Variables"

        #region "Constructor & Distructor"
            public frmInterfacesMessageQueueViewReport(Int32 _intPagesize)
            {
                if (appSettings["DataBaseConnectionString"] != null)
                {
                    if (appSettings["DataBaseConnectionString"] != null)
                    {
                        strConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                    }
                }
                strConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { 
                    gstrMessageBoxCaption = "gloEMR"; 
                }
                InitializeComponent();
               // iPageSize = _intPagesize;
                iPageSize = getMessageQueuePageSize();
            }
            ~frmInterfacesMessageQueueViewReport()
            {
                this.Dispose();
            }
        #endregion "Constructor & Distructor"

        #region "Form Events"
            //Load Event

            private void frmHL7_MessageQueue_Load(object sender, EventArgs e)
            {
                _isFormLoaded = false;
                try
                {
                    LoadServiceName();
                    cmbServiceName.SelectedIndex = 0;
                    lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                    _isFormLoaded = true;
                }
                catch (Exception)
                {
                    _isFormLoaded = true;
                    
                }
            }
        #endregion "Form Events"

        #region "Form Control Events"
           
            //Previous Button Click
            private void btnPrevious_Click(object sender, EventArgs e)
            {
                try
                {
                    iPageNumber--;

                    if (iPageNumber < 1)
                    {
                        iPageNumber = 1;
                        return;
                    }

                    iStartIndex = iStartIndex - iPageSize;
                    iEndIndex = iEndIndex - iPageSize;

                    if (iStartIndex < 0)
                    {
                        iStartIndex = 0;
                    }

                    if (iEndIndex < iPageSize)
                    {
                        iEndIndex = iPageSize;
                    }

                    //PopulateDatagrid();
                    BindGrid();
                   
                    if (iLogCount > 0)
                    {
                        lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                    }
                    else
                    {
                        lblPage.Text = "";
                    }

                }
                catch //(Exception ex)
                {
                    // Interaction.MsgBox(Err().Description);
                }
            }
          
            //Next Button Click
            private void btnNext_Click(object sender, EventArgs e)
            {
                try
                {
                    iPageNumber++;

                    if (iPageNumber > iTotalPages)
                    {
                        iPageNumber = iTotalPages;
                        return;
                    }

                    iStartIndex = iStartIndex + iPageSize;
                    iEndIndex = iEndIndex + iPageSize;

                    if (iStartIndex > (iLogCount - iPageSize))
                    {
                        iStartIndex = iLogCount - iPageSize;
                    }

                    if (iEndIndex > iLogCount)
                    {
                        iEndIndex = iLogCount;
                    }

                    // PopulateDatagrid();
                    BindGrid();

                    if (iLogCount > 0)
                    {
                        lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                    }
                    else
                    {
                        lblPage.Text = "";
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                    //Interaction.MsgBox(Err().Description);
                }

            }
            
            //Refresh Button Click
            private void tlpRefresh_Click(object sender, EventArgs e)
            {
                txtSearch.Text = string.Empty;
                ProcessMessageLog(iPageSize, strConnectionString);
                BindGrid();
                lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
            }
            
            // Close Button Click
            private void tlpClose_Click(object sender, EventArgs e)
            {
                this.Close();
            }
            private void cmbServiceName_SelectedIndexChanged(object sender, EventArgs e)
            {
                ProcessMessageLog(iPageSize, strConnectionString);
                if (_isFormLoaded)
                {
                    BindGrid();
                    if (iLogCount <= 0)
                    {
                        MessageBox.Show("No message found.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;

            }
            private void txtPatientCode_TextChanged(object sender, EventArgs e)
            {
                ProcessMessageLog(iPageSize, strConnectionString);
                
                BindGrid();
                lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
            }




        #endregion "Form Control Events"
   
        #region "User Defined Functions"
           /// <summary>
           /// Function to retrieve message log records.
           /// </summary>
           /// <returns>Data Table</returns>
            private DataTable GetMessageLogRecords()
            {
                gloDatabaseLayer.DBLayer oDbLayer = null;
                string strQuery = String.Empty;
                DataTable dtHl7MessageLog = null;
                String sServiceName = String.Empty;
                String _strFilter = getFilterCriteria();
                if (_strFilter.Length > 0)
                    _strFilter = " where " + _strFilter;
                //if (_strFilter.Length > 0)
                //    strQuery += " AND  (" + _strFilter + ")";
                try
                {
                    if ((strConnectionString !=null )&&(strConnectionString !=String.Empty))
                    {
                       oDbLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                       oDbLayer.Connect(false);
                       sServiceName = cmbServiceName.SelectedItem.ToString();


                       if (sServiceName.ToLower() == "PatientPortal".ToLower())
                       {
                           strQuery = @"With tbl_PPGlMsgLog
                                        AS
                                        (
                                        SELECT  
			                                        ISNULL(dbo.Patient.nPatientID ,0)AS PatientId, 
			                                        ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],    
			                                        CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
			                                        , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
			                                        ISNULL(dbo.Patient.sPatientCode,'') AS [Patient Code], 
			                                        dbo.GET_NAME(ISNULL(dbo.Patient.sFirstName,''),ISNULL(dbo.Patient.sMiddleName,''), ISNULL(dbo.Patient.sLastName,'')) as [Patient Name],  
                                                    'No' AS [Is Representative],
			                                        CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as [Status],
			                                        CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
			                                        FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" + sServiceName + @"'
                                                    and dbo.Gl_Messagequeue.nPatientPortalAccessID is null 

                                        union all

                                        SELECT  
			                                        pr.npatientportalaccessid AS PatientId, 
			                                        ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],    
			                                        CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
			                                        , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
			                                        '' AS [Patient Code], 
                                                    (ISNULL(PR.sFirstName, '')+ ' '+
                                                    ISNULL(PR.sLastName, '')) AS [Patient Name],  
                                                    'Yes' AS [Is Representative],
			                                        CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as [Status],
			                                        CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
			                                        FROM dbo.Gl_Messagequeue inner JOIN 
                                                        (SELECT dbo.PatientPortalAccess.nPatientPortalAccessID,dbo.PatientRepresentative_Mst.sFirstName,dbo.PatientRepresentative_Mst.sLastName FROM 
                                                        dbo.PatientPortalaccess 
                                                        INNER JOIN dbo.PatientRepresentative_Mst ON dbo.PatientPortalAccess.nPatientID = dbo.PatientRepresentative_Mst.nPRID
                                                        ) PR 
                                                        ON dbo.Gl_Messagequeue.nPatientPortalAccessID = PR.nPatientPortalAccessID
                                                    Where sServiceName='" + sServiceName + @"'
                                        			
                                        ) 
                                      SELECT [Date Time],[Patient Code],[Patient Name],[Is Representative] ,[Message Type],[Machine Name],[Status] FROM(Select  ROW_NUMBER() over(order by [Date Time] DESC) AS ROWID, [Date Time],[Message Type],[Machine Name],[Patient Code],[Patient Name],[Is Representative],[Status] from tbl_PPGlMsgLog " + _strFilter + " )MyTable where RowId  >" + iStartIndex.ToString() + @" 
                                          And RowId <=" + iEndIndex.ToString() + "";// order by DateTimeStamp desc"; 
                       }
                       else if (sServiceName.ToLower() == "APIAccess".ToLower())
                       {
                           strQuery = @"With tbl_PPGlMsgLog
                                        AS
                                        (
                                            SELECT  
                                            ISNULL(dbo.Patient.nPatientID ,0)AS PatientId, 
                                            ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],   
                                            CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
                                            , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
                                            ISNULL(dbo.Patient.sPatientCode,'') AS [Patient Code], 
                                            dbo.GET_NAME(ISNULL(dbo.Patient.sFirstName,''),ISNULL(dbo.Patient.sMiddleName,''), ISNULL(dbo.Patient.sLastName,'')) as [Patient Name],  
                                            'No' AS [Is Representative],
                                            APIAccessRole.sRoleName,
                                            CASE dbo.Gl_Messagequeue.nStatus
                                            WHEN 1 THEN 'Queue' 
                                            WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error'
                                            ELSE 'Process Error' END as [Status],
                                            CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
                                            FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID
                                            Left join APIAccess on APIAccess.nAPIAccessUserID =  dbo.Patient.nPatientID
											left join APIAccessRole on APIAccessRole.nRoleID=APIAccess.nRoleId
                                            Where sServiceName='" + sServiceName + @"'
                                            union

                                            SELECT  
                                            ISNULL(dbo.PatientRepresentative_Mst.nPRID ,0)AS PatientId, 
                                            ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],  
                                            CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
                                            , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
                                            ''AS [Patient Code], 
                                            dbo.GET_NAME(ISNULL(dbo.PatientRepresentative_Mst.sFirstName,''), ' ', ISNULL(dbo.PatientRepresentative_Mst.sLastName,'')) as [Patient Name],  
                                            [Is Representative]= case when dbo.Gl_Messagequeue.nPatientID=dbo.PatientRepresentative_Mst.nPRID then 'Yes' else 'No' end
                                            ,APIAccessRole.sRoleName                                           
                                            ,CASE dbo.Gl_Messagequeue.nStatus
                                            WHEN 1 THEN 'Queue' 
                                            WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error'
                                            ELSE 'Process Error' END as [Status],
                                            CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
                                            FROM dbo.Gl_Messagequeue INNER JOIN dbo.PatientRepresentative_Mst 
                                            ON dbo.Gl_Messagequeue.nPatientID = dbo.PatientRepresentative_Mst.nprId
                                            Left join APIAccess on APIAccess.nAPIAccessUserID =  dbo.PatientRepresentative_Mst.nprId
											left join APIAccessRole on APIAccessRole.nRoleID=APIAccess.nRoleId
                                            Where sServiceName='" + sServiceName + @"'
                                            union

                                            SELECT  
                                            ISNULL(dbo.APIAccessUser.nAPIAccessUserId ,0)AS PatientId, 
                                            ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],   
                                            CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
                                            , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
                                            ''AS [Patient Code], 
                                            dbo.GET_NAME(ISNULL(dbo.APIAccessUser.sFirstName,''), ISNULL(dbo.APIAccessUser.sMiddleName,''), ISNULL(dbo.APIAccessUser.sLastName,'')) as [Patient Name],  
                                            [Is Representative]= case when dbo.Gl_Messagequeue.nPatientID=dbo.APIAccessUser.nAPIAccessUserId then 'No' else 'UNK' end
	                                        ,APIAccessRole.sRoleName
                                            ,CASE dbo.Gl_Messagequeue.nStatus
                                            WHEN 1 THEN 'Queue' 
                                            WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error'
                                            ELSE 'Process Error' END as [Status],
                                            CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
                                            FROM dbo.Gl_Messagequeue INNER JOIN dbo.APIAccessUser 
                                            ON dbo.Gl_Messagequeue.nPatientID = dbo.APIAccessUser.nAPIAccessUserId
                                            Left join APIAccess on APIAccess.nAPIAccessUserID =  dbo.APIAccessUser.nAPIAccessUserId
											left join APIAccessRole on APIAccessRole.nRoleID=APIAccess.nRoleId
                                            Where sServiceName='" + sServiceName + @"'
                                        			
                                        ) 
                                      SELECT [Date Time],[Patient Code],[Patient Name] as [User Name],[sRoleName] as Role ,[Message Type],[Machine Name],[Status] FROM(Select  ROW_NUMBER() over(order by [Date Time] DESC) AS ROWID, [Date Time],[Message Type],[Machine Name],[Patient Code],[Patient Name],[sRoleName],[Status] from tbl_PPGlMsgLog " + _strFilter + " )MyTable where RowId  >" + iStartIndex.ToString() + @" 
                                          And RowId <=" + iEndIndex.ToString() + "";// order by DateTimeStamp desc"; 
                       }

                       else
                       {
                           if (cmbServiceName.SelectedIndex == 0)
                           {
                               strQuery = @"With tbl_HL7MsgLog
                                    As
                                    (
                                    SELECT Patient.nPatientID as PatientID, HL7_MessageQueue.dtDateTimeStamp as DateTimeStamp , 
                                                  Case HL7_MessageQueue.sMessageName when 'A08' then 'Patient Modification' when 'A04' then 'Patient Registration'  
                                                  when 'O01' then 'Lab Order' when 'P03' then 'Charges' when 'S12' then 'Appointment Registration'  
                                                  when 'S13' then 'Appointment Modification' when 'S15' then 'Appointment Canceled/Deleted'  
                                                  when 'V04' then 'Immunization Update' when 'A28' then 'Patient Information' when 'V04IR' then 'Immunization Registry Update' when 'Q11' then 'Immunization History/Forecast Request'
                                                  When 'V03' then 'Immunization Query Response' when 'A03' then 'Patient Information' WHEN 'T02' THEN 'Visit Summary' end  as 'HL7Message', HL7_MessageQueue.sMachineName AS 'MachineName', 
                                                  isnull( Patient.sPatientCode,'') as  'PatientCode' , dbo.GET_NAME(Patient.sFirstName,Patient.sMiddleName,Patient.sLastName) AS 'Patient',  
                                                  case HL7_MessageQueue.nStatus when 1 then 'Queue'when 2 then 'InProcess' when 3 then 'Processed' when 4 then 'Error' when 5 then 'Failed' end as 'Status', 
                                                  convert(date,HL7_MessageQueue.dtDateTimeStamp,101) as Date  FROM Patient INNER JOIN HL7_MessageQueue  
                                                  ON Patient.nPatientID = HL7_MessageQueue.nPatientID 
                                    )
                                   SELECT PatientID,DateTimeStamp,PatientCode,Patient,HL7Message,MachineName,Status,Date FROM (Select ROW_NUMBER() over(order by DateTimeStamp DESC) AS ROWID, PatientID,DateTimeStamp,PatientCode,Patient,HL7Message,MachineName,Status,Date from tbl_Hl7MsgLog " + _strFilter + " ) MyTable where RowId  >" + iStartIndex.ToString() + @" 
                                      And RowId <=" + iEndIndex.ToString() + "";// order by DateTimeStamp desc";

                           }
                           else
                           {
                               //                           strQuery = @" SELECT  dbo.Patient.nPatientID AS PatientId, dbo.Gl_Messagequeue.dtDateTimeStamp AS DateTimeStamp,    
                               //                                       CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS 'MessageName', dbo.Gl_Messagequeue.sMachineName AS MachineName,    
                               //                                     ISNULL(dbo.Patient.sPatientCode,'') AS PatientCode, ISNULL(dbo.Patient.sFirstName,'')+' '+ISNULL(dbo.Patient.sMiddleName,'')+' '+ ISNULL(dbo.Patient.sLastName,'') as Patient,    
                               //                                     CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as 'Status',CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS Date   
                               //                                     FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='"+ sServiceName + "'";

                               strQuery = @"With tbl_GlMsgLog
                                        AS
                                        (
                                        SELECT  
			                                        ISNULL(dbo.Patient.nPatientID ,0)AS PatientId, 
			                                        ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS DateTimeStamp,    
			                                        CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS 'MessageName'
			                                        , dbo.Gl_Messagequeue.sMachineName AS MachineName,    
			                                        ISNULL(dbo.Patient.sPatientCode,'') AS PatientCode, 
			                                        dbo.GET_NAME(ISNULL(dbo.Patient.sFirstName,''),ISNULL(dbo.Patient.sMiddleName,''), ISNULL(dbo.Patient.sLastName,'')) as Patient,  

			                                        CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as [Status],
			                                        CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
			                                        FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" + sServiceName + @"'
                                   		
                                        ) 
                                      SELECT DateTimeStamp,PatientCode,Patient,MessageName,MachineName,[Status] FROM(Select  ROW_NUMBER() over(order by DateTimeStamp DESC) AS ROWID, DateTimeStamp,MessageName,MachineName,PatientCode,Patient,[Status] from tbl_GlMsgLog " + _strFilter + " )MyTable where RowId  >" + iStartIndex.ToString() + @" 
                                          And RowId <=" + iEndIndex.ToString() + "";// order by DateTimeStamp desc"; 
                           }
                       }
                     
                       //String _strSearhString = txtSearch.Text.Trim();
                     
                       }
                       oDbLayer.Retrive_Query(strQuery, out dtHl7MessageLog);
                       return dtHl7MessageLog;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                finally
                {
                    if (oDbLayer != null)
                    {
                        oDbLayer.Dispose();
                        oDbLayer = null;
                    }
                }

            }
            private string getFilterCriteria()
            {
                List<String> lstHL7ColumnList = new List<string>(){
                    "PatientID",
                    "DateTimeStamp",
                    "HL7Message",
                    "MachineName",
                    "PatientCode",
                    "Patient",
                    "Status",
                    "Date"
                };
                List<String> lstGeneralColumnList = new List<string>()
                {
                    "DateTimeStamp",
                    "MessageName",
                    "MachineName",
                    "PatientCode",
                    "Patient",
                    "Status"

                };

                List<String> lstPatientPortalColumnList = new List<string>()
                {
                    "[Date Time]",
                    "[Message Type]",
                    "[Machine Name]",
                    "[Patient Code]",
                    "[Patient Name]",
                    "[Status]",
                    "[Is Representative]"

                };

                List<String> lstAPIColumnList = new List<string>()
                {
                    "[Date Time]",
                    "[Message Type]",
                    "[Machine Name]",
                    "[Patient Code]",
                    "[Patient Name]",
                    "[Status]",
                    "[Is Representative]"

                };

                String _strSearhString = txtSearch.Text.Trim();
                String _strFilter = String.Empty;
                try
                {
                    if (_strSearhString.Length > 0)
                    {
                        _strSearhString = _strSearhString.Replace("'", "");
                        _strSearhString = _strSearhString.Replace("[", "");
                        _strSearhString = _strSearhString.Replace("*", "");
                        _strSearhString = _strSearhString.Replace("%", "");
                        _strSearhString = _strSearhString.Replace("]", "");

                        _strFilter = String.Empty;
                        if (_strSearhString.Length > 0)
                        {
                            String sServiceName = String.Empty;
                            sServiceName = cmbServiceName.SelectedItem.ToString();
                            if (sServiceName.ToLower() == "PatientPortal".ToLower())
                            {
                                foreach (String strCol in lstPatientPortalColumnList)
                                {
                                    if (_strFilter.Length == 0)
                                        _strFilter = strCol + " Like  '%" + _strSearhString + "%'";
                                    else
                                        _strFilter += " Or " + strCol + " Like  '%" + _strSearhString + "%'";

                                }
                            }
                            else if (sServiceName.ToLower() == "APIAccess".ToLower())
                            {
                                foreach (String strCol in lstAPIColumnList)
                                {
                                    if (_strFilter.Length == 0)
                                        _strFilter = strCol + " Like  '%" + _strSearhString + "%'";
                                    else
                                        _strFilter += " Or " + strCol + " Like  '%" + _strSearhString + "%'";

                                }
                            }
                            else
                            {
                                if (cmbServiceName.SelectedIndex == 0)
                                {
                                    foreach (String strCol in lstHL7ColumnList)
                                    {
                                        if (_strFilter.Length == 0)
                                            _strFilter = strCol + " Like  '%" + _strSearhString + "%'";
                                        else
                                            _strFilter += " Or " + strCol + " Like  '%" + _strSearhString + "%'";
                                    }
                                }
                                else
                                {
                                    foreach (String strCol in lstGeneralColumnList)
                                    {
                                        if (_strFilter.Length == 0)
                                            _strFilter = strCol + " Like  '%" + _strSearhString + "%'";
                                        else
                                            _strFilter += " Or " + strCol + " Like  '%" + _strSearhString + "%'";

                                    }
                                }
                            }

                            
                        }
                    }
                    return _strFilter;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return String.Empty;
                }
               
            }

            /// <summary>
            /// Function retrieves Count of HL7 message Queue records.
            /// </summary>
            /// <returns>Int32</returns>
            private Int32 GetHL7MessaLogCount()
            {
                gloDatabaseLayer.DBLayer _oDBLayer = null;
                string _strQuery = String.Empty;
                string sServiceName=String.Empty;
                Int32 _CntRecords = 0;
                try
                {
                    if ((strConnectionString != null) && (strConnectionString != String.Empty))
                    {
                        
                        _oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                        _oDBLayer.Connect(false);
                        sServiceName = cmbServiceName.SelectedItem.ToString();
                        if (sServiceName.ToLower() == "PatientPortal".ToLower())
                        {
                            _strQuery = @"With tbl_PPGlMsgLog
                                        AS
                                        (
                                        SELECT  
                                        			
			                                        ROW_NUMBER() over(order by dbo.Gl_Messagequeue.dtDateTimeStamp DESC) AS ROWID,
			                                        ISNULL(dbo.Patient.nPatientID ,0)AS PatientId, 
			                                        ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],    
			                                        CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
			                                        , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
			                                        ISNULL(dbo.Patient.sPatientCode,'') AS [Patient Code], 
			                                        dbo.GET_NAME(ISNULL(dbo.Patient.sFirstName,''),ISNULL(dbo.Patient.sMiddleName,''), ISNULL(dbo.Patient.sLastName,'')) as [Patient Name],  

			                                        CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as [Status],
			                                        CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'
                                                    ,'No' AS [Is Representative]   
			                                        FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" + sServiceName + @"'
                                                    and dbo.Gl_Messagequeue.nPatientPortalAccessID is null 
                                        union all

                                          SELECT  
                                        			
			                                        ROW_NUMBER() over(order by dbo.Gl_Messagequeue.dtDateTimeStamp DESC) AS ROWID,
			                                        ISNULL(pr.npatientportalaccessid  ,0)AS PatientId, 
			                                        ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS  [Date Time],    
			                                        CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
			                                        , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
			                                        '' AS [Patient Code], 
			                                        (ISNULL(PR.sFirstName, '')+ ' '+
                                                    ISNULL(PR.sLastName, ''))  as [Patient Name],  

			                                        CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as [Status],
			                                        CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date' 
                                                    ,'Yes' AS [Is Representative]
			                                        FROM dbo.Gl_Messagequeue inner JOIN 
                                                        (SELECT dbo.PatientPortalAccess.nPatientPortalAccessID,dbo.PatientRepresentative_Mst.sFirstName,dbo.PatientRepresentative_Mst.sLastName FROM 
                                                        dbo.PatientPortalaccess 
                                                        INNER JOIN dbo.PatientRepresentative_Mst ON dbo.PatientPortalAccess.nPatientID = dbo.PatientRepresentative_Mst.nPRID
                                                        ) PR 
                                                        ON dbo.Gl_Messagequeue.nPatientPortalAccessID = PR.nPatientPortalAccessID
                                                    Where sServiceName='" + sServiceName + @"'
                                        			
                                        )SELECT COUNT(*) from tbl_PPGlMsgLog ";
                        }
                        else if (sServiceName.ToLower() == "APIAccess".ToLower())
                        {
                            _strQuery = @"With tbl_PPGlMsgLog
                                        AS
                                        (
                                            SELECT  
                                            ISNULL(dbo.Patient.nPatientID ,0)AS PatientId, 
                                            ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],   
                                            CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
                                            , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
                                            ISNULL(dbo.Patient.sPatientCode,'') AS [Patient Code], 
                                            dbo.GET_NAME(ISNULL(dbo.Patient.sFirstName,''),ISNULL(dbo.Patient.sMiddleName,''), ISNULL(dbo.Patient.sLastName,'')) as [Patient Name],  
                                            'No' AS [Is Representative],
                                            APIAccessRole.sRoleName,
                                            CASE dbo.Gl_Messagequeue.nStatus
                                            WHEN 1 THEN 'Queue' 
                                            WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error'
                                            ELSE 'Process Error' END as [Status],
                                            CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
                                            FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID
                                            Left join APIAccess on APIAccess.nAPIAccessUserID =  dbo.Patient.nPatientID
											left join APIAccessRole on APIAccessRole.nRoleID=APIAccess.nRoleId
                                            Where sServiceName='" + sServiceName + @"'
                                            union

                                            SELECT  
                                            ISNULL(dbo.PatientRepresentative_Mst.nPRID ,0)AS PatientId, 
                                            ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],  
                                            CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
                                            , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
                                            ''AS [Patient Code], 
                                            dbo.GET_NAME(ISNULL(dbo.PatientRepresentative_Mst.sFirstName,''), ' ', ISNULL(dbo.PatientRepresentative_Mst.sLastName,'')) as [Patient Name],  
                                            [Is Representative]= case when dbo.Gl_Messagequeue.nPatientID=dbo.PatientRepresentative_Mst.nPRID then 'Yes' else 'No' end
                                            ,APIAccessRole.sRoleName                                           
                                            ,CASE dbo.Gl_Messagequeue.nStatus
                                            WHEN 1 THEN 'Queue' 
                                            WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error'
                                            ELSE 'Process Error' END as [Status],
                                            CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
                                            FROM dbo.Gl_Messagequeue INNER JOIN dbo.PatientRepresentative_Mst 
                                            ON dbo.Gl_Messagequeue.nPatientID = dbo.PatientRepresentative_Mst.nprId
                                            Left join APIAccess on APIAccess.nAPIAccessUserID =  dbo.PatientRepresentative_Mst.nprId
											left join APIAccessRole on APIAccessRole.nRoleID=APIAccess.nRoleId
                                            Where sServiceName='" + sServiceName + @"'
                                            union

                                            SELECT  
                                            ISNULL(dbo.APIAccessUser.nAPIAccessUserId ,0)AS PatientId, 
                                            ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS [Date Time],   
                                            CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS [Message Type]
                                            , dbo.Gl_Messagequeue.sMachineName AS [Machine Name],    
                                            ''AS [Patient Code], 
                                            dbo.GET_NAME(ISNULL(dbo.APIAccessUser.sFirstName,''), ISNULL(dbo.APIAccessUser.sMiddleName,''), ISNULL(dbo.APIAccessUser.sLastName,'')) as [Patient Name],  
                                            [Is Representative]= case when dbo.Gl_Messagequeue.nPatientID=dbo.APIAccessUser.nAPIAccessUserId then 'No' else 'UNK' end
	                                        ,APIAccessRole.sRoleName
                                            ,CASE dbo.Gl_Messagequeue.nStatus
                                            WHEN 1 THEN 'Queue' 
                                            WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error'
                                            ELSE 'Process Error' END as [Status],
                                            CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
                                            FROM dbo.Gl_Messagequeue INNER JOIN dbo.APIAccessUser 
                                            ON dbo.Gl_Messagequeue.nPatientID = dbo.APIAccessUser.nAPIAccessUserId
                                            Left join APIAccess on APIAccess.nAPIAccessUserID =  dbo.APIAccessUser.nAPIAccessUserId
											left join APIAccessRole on APIAccessRole.nRoleID=APIAccess.nRoleId
                                            Where sServiceName='" + sServiceName + @"'
                                        			
                                        ) 
                                     SELECT COUNT(*) from tbl_PPGlMsgLog "; 
                        }
                        else
                        {

                            if (cmbServiceName.SelectedIndex == 0)
                            {
                                _strQuery = @"With tbl_HL7MsgLog
                                    As
                                    (
                                    SELECT ROW_NUMBER() over(order by HL7_MessageQueue.dtDateTimeStamp  DESC) AS ROWID,  Patient.nPatientID as PatientID, HL7_MessageQueue.dtDateTimeStamp as DateTimeStamp , 
                                                  Case HL7_MessageQueue.sMessageName when 'A08' then 'Patient Modification' when 'A04' then 'Patient Registration'  
                                                  when 'O01' then 'Lab Order' when 'P03' then 'Charges' when 'S12' then 'Appointment Registration'  
                                                  when 'S13' then 'Appointment Modification' when 'S15' then 'Appointment Canceled/Deleted'  
                                                  when 'V04' then 'Immunization Update' when 'A28' then 'Patient Information' when 'V04IR' then 'Immunization Registry Update'
                                                  When 'V03' then 'Immunization Query Response' end as 'HL7Message', HL7_MessageQueue.sMachineName AS 'MachineName', 
                                                  isnull( Patient.sPatientCode,'') as  'PatientCode' , dbo.GET_NAME(Patient.sFirstName,Patient.sMiddleName,Patient.sLastName) AS 'Patient',  
                                                  case HL7_MessageQueue.nStatus when 1 then 'Queue'when 2 then 'InProcess' when 3 then 'Processed' when 4 then 'Error' when 5 then 'Failed' end as 'Status', 
                                                  convert(date,HL7_MessageQueue.dtDateTimeStamp,101) as Date FROM Patient INNER JOIN HL7_MessageQueue  
                                                  ON Patient.nPatientID = HL7_MessageQueue.nPatientID 
                                    )SELECT COUNT(ROWID) FROM tbl_HL7MsgLog ";

                                //_strQuery = "select COUNT(*) FROM Patient INNER JOIN HL7_MessageQueue ON Patient.nPatientID = HL7_MessageQueue.nPatientID";
                            }
                            else
                            {
                                _strQuery = @"With tbl_GlMsgLog
                                        AS
                                        (
                                        SELECT  
                                        			
			                                        ROW_NUMBER() over(order by dbo.Gl_Messagequeue.dtDateTimeStamp DESC) AS ROWID,
			                                        ISNULL(dbo.Patient.nPatientID ,0)AS PatientId, 
			                                        ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS DateTimeStamp,    
			                                        CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS 'MessageName'
			                                        , dbo.Gl_Messagequeue.sMachineName AS MachineName,    
			                                        ISNULL(dbo.Patient.sPatientCode,'') AS PatientCode, 
			                                        dbo.GET_NAME(ISNULL(dbo.Patient.sFirstName,''),ISNULL(dbo.Patient.sMiddleName,''), ISNULL(dbo.Patient.sLastName,'')) as Patient,  

			                                        CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as [Status],
			                                        CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
			                                        FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" + sServiceName + @"'

                                        			
                                        )SELECT COUNT(ROWID) from tbl_GlMsgLog ";
                                //_strQuery = "SELECT COUNT(*) FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" + sServiceName + "'";
                            }

                        }
                        
                        
                      
                        String _strFilter = getFilterCriteria();
                        if (_strFilter.Length > 0)
                            _strQuery += " Where " + _strFilter + "";

                        _CntRecords = Convert.ToInt32(_oDBLayer.ExecuteScalar_Query(_strQuery));
                        _oDBLayer.Disconnect();
                    }
                    return _CntRecords;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                finally
                {
                    if (_oDBLayer != null)
                    {
                        _oDBLayer.Dispose();
                        _oDBLayer = null;
                    }
                }
            }

            /// <summary>
            /// Function to initialize page count / log size
            /// </summary>
            /// <param name="nShowReport"></param>
            /// <param name="ConnectionString"></param>
            private void ProcessMessageLog(Int32 nShowReport, string ConnectionString)
            {
                // ClsDbCredentials _objDbCredentials = new ClsDbCredentials();
                string _strStatus = string.Empty;
                try
                {
                    iLogCount = 1;
                    iTotalPages = 1;
                    iLogCount = GetHL7MessaLogCount();
                   // dtMessageLog = GetMessageLogRecords();
                    //iLogCount = _objErrorLog.GetMessageLogCount(ConnectionString, dtFromDate.Text, dtToDate.Value.AddDays(1).ToShortDateString(), txtPatientCode.Text.Trim(), _strStatus);
                    //iLogCount = dtMessageLog.Rows.Count;
                    if (iLogCount > 0)
                    {
                        if (iLogCount > nShowReport)
                        {
                            iTotalPages = Convert.ToInt16(Math.Floor(Convert.ToDecimal(iLogCount / nShowReport)));
                            if (iLogCount > (nShowReport * iTotalPages))
                            {
                                Int32 iLogDifference = iLogCount - (nShowReport * iTotalPages);
                                iLogCount = iLogCount + (nShowReport - iLogDifference);
                                iTotalPages = iTotalPages + 1;
                            }
                        }

                    }
                    iPageNumber = 1;
                    iStartIndex = 0;
                    iEndIndex = iPageSize;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {

                    //_objErrorLog.Dispose();
                    //_objErrorLog = null;
                }
            }

        /// <summary>
        /// Function for binding data to grid
        /// </summary>
            private void BindGrid()
            {
              
                //DataTable _dtMessageLog = new DataTable();
                string _strStatus = string.Empty;
                try
                {
                    dtMessageLog = GetMessageLogRecords();
                   // _dtMessageLog = GetMessageLogRecords();
                    setC1FlexGridCol();   
                    if (dtMessageLog != null)
                    {
                       
                        C1Flex.DataSource = dtMessageLog;
                        if (cmbServiceName.SelectedIndex == 0)
                        {
                            int nWidth = pnlGrid.Width;
                            C1Flex.ExtendLastCol = true;
                            C1Flex.Cols[0].Width = Convert.ToInt32(nWidth * 0.12);
                            C1Flex.Cols[1].Width = Convert.ToInt32(nWidth * 0.12);
                            C1Flex.Cols[2].Width = Convert.ToInt32(nWidth * 0.12);
                            C1Flex.Cols[3].Width = Convert.ToInt32(nWidth * 0.20);
                            C1Flex.Cols[4].Width = Convert.ToInt32(nWidth * 0.20);
                            C1Flex.Cols[5].Width = Convert.ToInt32(nWidth * 0.12);
                            C1Flex.Cols[6].Width = Convert.ToInt32(nWidth * 0.08);
                            C1Flex.Cols[7].Width = Convert.ToInt32(nWidth * 0.08);


                            C1Flex.AllowEditing = false;
                            //Applying style to DateTimeStamp Column
                            if (dtMessageLog.Rows.Count > 0)
                            {
                                C1.Win.C1FlexGrid.CellStyle cs;// = C1Flex.Styles.Add("datetime");
                                try
                                {
                                    if (C1Flex.Styles.Contains("datetime"))
                                    {
                                        cs = C1Flex.Styles["datetime"];
                                    }
                                    else
                                    {
                                        cs = C1Flex.Styles.Add("datetime");

                                    }

                                }
                                catch
                                {
                                    cs = C1Flex.Styles.Add("datetime");


                                }
         
                                cs.DataType = typeof(DateTime);
                                cs.Format = "MM/dd/yyyy hh:mm tt";
                                // C1Flex.Cols[mColIndex_sDatetimeStamp].Style = cs;
                                C1.Win.C1FlexGrid.CellRange rg = C1Flex.GetCellRange(1, mColIndex_DateTimeStamp, C1Flex.Rows.Count - 1, mColIndex_DateTimeStamp);
                                rg.Style = C1Flex.Styles["datetime"];

                                //Applying Style to Date Column
                                C1.Win.C1FlexGrid.CellStyle cs1;// = C1Flex.Styles.Add("date");
                                try
                                {
                                    if (C1Flex.Styles.Contains("date"))
                                    {
                                        cs1 = C1Flex.Styles["date"];
                                    }
                                    else
                                    {
                                        cs1 = C1Flex.Styles.Add("date");

                                    }

                                }
                                catch
                                {
                                    cs1 = C1Flex.Styles.Add("date");


                                }
         
                                cs1.DataType = typeof(DateTime);
                                cs1.Format = "MM/dd/yyyy";
                                // C1Flex.Cols[mColIndex_sDatetimeStamp].Style = cs;
                                C1.Win.C1FlexGrid.CellRange rg1 = C1Flex.GetCellRange(1, mColIndex_Date, C1Flex.Rows.Count - 1, mColIndex_Date);
                                rg1.Style = C1Flex.Styles["date"];
                            }

                            //Added by Abhijeet on 20110527.doing patient id column visible false
                            C1Flex.Cols[0].Visible = false;
                            C1Flex.Cols[7].Visible = false;

                        }
                        else
                        {
                            int nWidth = pnlGrid.Width;
                            String sServiceName = String.Empty;
                            sServiceName = cmbServiceName.SelectedItem.ToString();
                            if (sServiceName.ToLower() == "PatientPortal".ToLower())
                            {
                                C1Flex.ExtendLastCol = true;
                                C1Flex.Cols[0].Width = Convert.ToInt32(nWidth * 0.13);
                                C1Flex.Cols[1].Width = Convert.ToInt32(nWidth * 0.10);
                                C1Flex.Cols[2].Width = Convert.ToInt32(nWidth * 0.20);
                                C1Flex.Cols[3].Width = Convert.ToInt32(nWidth * 0.11);
                                C1Flex.Cols[4].Width = Convert.ToInt32(nWidth * 0.17);
                                C1Flex.Cols[5].Width = Convert.ToInt32(nWidth * 0.13);
                            }
                            else
                            {
                                C1Flex.ExtendLastCol = true;
                                C1Flex.Cols[0].Width = Convert.ToInt32(nWidth * 0.20);
                                C1Flex.Cols[1].Width = Convert.ToInt32(nWidth * 0.20);
                                C1Flex.Cols[2].Width = Convert.ToInt32(nWidth * 0.20);
                                C1Flex.Cols[3].Width = Convert.ToInt32(nWidth * 0.20);
                                C1Flex.Cols[4].Width = Convert.ToInt32(nWidth * 0.10);
                                C1Flex.Cols[5].Width = Convert.ToInt32(nWidth * 0.09);
                            }

                            if (dtMessageLog.Rows.Count > 0)
                            {
                                C1Flex.AllowEditing = false;
                                //Applying style to DateTimeStamp Column
                                C1.Win.C1FlexGrid.CellStyle cs;// = C1Flex.Styles.Add("datetime");
                                try
                                {
                                    if (C1Flex.Styles.Contains("datetime"))
                                    {
                                        cs = C1Flex.Styles["datetime"];
                                    }
                                    else
                                    {
                                        cs = C1Flex.Styles.Add("datetime");

                                    }

                                }
                                catch
                                {
                                    cs = C1Flex.Styles.Add("datetime");


                                }
                                cs.DataType = typeof(DateTime);
                                cs.Format = "MM/dd/yyyy hh:mm tt";
                                // C1Flex.Cols[mColIndex_sDatetimeStamp].Style = cs;
                                C1.Win.C1FlexGrid.CellRange rg = C1Flex.GetCellRange(1, ColIndex_DateTimeStamp, C1Flex.Rows.Count - 1, ColIndex_DateTimeStamp);
                                rg.Style = C1Flex.Styles["datetime"];
                            }

                            //Applying Style to Date Column
                            //C1.Win.C1FlexGrid.CellStyle cs1 = C1Flex.Styles.Add("date");
                            //cs1.DataType = typeof(DateTime);
                            //cs1.Format = "dd-MM-yyyy";
                            //// C1Flex.Cols[mColIndex_sDatetimeStamp].Style = cs;
                            //C1.Win.C1FlexGrid.CellRange rg1 = C1Flex.GetCellRange(1, mColIndex_Date, C1Flex.Rows.Count - 1, mColIndex_Date);
                            //rg1.Style = C1Flex.Styles["date"];
                        }

                       
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(),gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

        /// <summary>
        /// Function for applying styles to grid.
        /// </summary>
            private void setC1FlexGridCol()
            {
                try
                {
                  //  C1Flex.Clear();
                    C1Flex.DataSource = null;
                    C1Flex.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                    //C1Flex.DataSource = null;
                    C1Flex.AllowDrop = true;
                    C1Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

                    C1Flex.Cols[0].Width = 30;

                    C1Flex.ExtendLastCol = true;
                   
                    //set properties of c1 grid
                    C1Flex.Rows.Count = 1;
                    C1Flex.Rows.Fixed = 1;
                    if (cmbServiceName.SelectedIndex == 0)
                    {
                        C1Flex.Cols.Count = 8;
                        C1Flex.Cols.Fixed = 0;
                        C1Flex.Rows[C1Flex.Rows.Count - 1].Height = 21;
                        C1Flex.Cols[mColIndex_DateTimeStamp].DataType = Type.GetType("System.DateTime");
                        C1Flex.Cols[mColIndex_Date].DataType = Type.GetType("System.DateTime");
                        C1Flex.Cols[mColIndex_PateintId].DataType = Type.GetType("System.String");
                        C1Flex.Cols[mColIndex_Hl7Message].DataType = Type.GetType("System.String");
                        C1Flex.Cols[mColIndex_MachineName].DataType = Type.GetType("System.String");
                        C1Flex.Cols[mColIndex_PatientCode].DataType = Type.GetType("System.String");
                        C1Flex.Cols[mColIndex_Patient].DataType = Type.GetType("System.String");
                        C1Flex.Cols[mColIndex_Status].DataType = Type.GetType("System.String");
                       // C1Flex.Cols[mCol_MsgID].DataType = Type.GetType("System.String");

                        //Set Column Headers
                        C1Flex.SetData(0, mColIndex_DateTimeStamp, "Date Time Stamp");
                        C1Flex.SetData(0, mColIndex_Date, "Date");
                        C1Flex.SetData(0, mColIndex_PateintId, "Patient Id");
                        C1Flex.SetData(0, mColIndex_Hl7Message, "HL7 Message");
                        C1Flex.SetData(0, mColIndex_MachineName, "Machine Name");
                        C1Flex.SetData(0, mColIndex_PatientCode, "Patient Code");
                        C1Flex.SetData(0, mColIndex_Patient, "Patient");
                        C1Flex.SetData(0, mColIndex_Status, "Status");
                        //C1Flex.SetData(0, mCol_MsgID, "MessageId");

                        //set column editing properties.
                        C1Flex.Cols[mColIndex_DateTimeStamp].AllowEditing = false;
                        C1Flex.Cols[mColIndex_Date].AllowEditing = false;
                        C1Flex.Cols[mColIndex_PateintId].AllowEditing = false;
                        C1Flex.Cols[mColIndex_Hl7Message].AllowEditing = false;
                        C1Flex.Cols[mColIndex_MachineName].AllowEditing = false;
                        C1Flex.Cols[mColIndex_PatientCode].AllowEditing = false;
                        C1Flex.Cols[mColIndex_Status].AllowEditing = false;
                       // C1Flex.Cols[mCol_MsgID].AllowEditing = false;
                       // C1Flex.Cols[mCol_MsgID].Width  = 0;
                        //C1Flex.Cols[mCol_MsgID].Visible = false;
                    }
                    else
                    {
                        C1Flex.Cols.Count = 6;
                        C1Flex.Cols.Fixed = 0;
                        C1Flex.Rows[C1Flex.Rows.Count - 1].Height = 21;
                 
                        C1Flex.Cols[ColIndex_DateTimeStamp].DataType = Type.GetType("System.DateTime");
                        C1Flex.Cols[ColIndex_MessageName].DataType = Type.GetType("System.String");
                        C1Flex.Cols[ColIndex_MachineName].DataType = Type.GetType("System.String");
                        C1Flex.Cols[ColIndex_PatientCode].DataType = Type.GetType("System.String");
                        C1Flex.Cols[ColIndex_Patient].DataType = Type.GetType("System.String");
                        C1Flex.Cols[ColIndex_Status].DataType = Type.GetType("System.String");
                       // C1Flex.Cols[Col_MsgID].DataType = Type.GetType("System.String");

                        C1Flex.SetData(0, ColIndex_DateTimeStamp, "Date Time Stamp");
                        C1Flex.SetData(0, ColIndex_MessageName, "Message Name");
                        C1Flex.SetData(0, ColIndex_MachineName, "Machine Name");
                        C1Flex.SetData(0, ColIndex_PatientCode, "Patient Code");
                        C1Flex.SetData(0, ColIndex_Patient, "Patient");
                        C1Flex.SetData(0, ColIndex_Status, "Status");
                     //   C1Flex.SetData(0, Col_MsgID, "MessageID");
                        //set column editing properties.
                        C1Flex.Cols[ColIndex_DateTimeStamp].AllowEditing = false;
                        C1Flex.Cols[ColIndex_MessageName].AllowEditing = false;
                        C1Flex.Cols[ColIndex_MachineName].AllowEditing = false;
                        C1Flex.Cols[ColIndex_PatientCode].AllowEditing = false;
                        C1Flex.Cols[ColIndex_Patient].AllowEditing = false;
                        C1Flex.Cols[ColIndex_Status].AllowEditing = false;
                      //  C1Flex.Cols[Col_MsgID].AllowEditing = false;
                      //  C1Flex.Cols[Col_MsgID].Width = 0;
                      //  C1Flex.Cols[Col_MsgID].Visible = false;
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        /// <summary>
        /// Function to retrieve service names from General Message Log
        /// </summary>
            private void LoadServiceName()
            {
                DataTable dtServiceNames = new DataTable();
                gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(strConnectionString );
                string _strSQL = string.Empty;

                try
                {
                    _strSQL = "SELECT Distinct sServiceName FROM Gl_Messagequeue";

                    objDbLayer.Connect(false);
                    objDbLayer.Retrive_Query(_strSQL,out dtServiceNames);


                    if ((dtServiceNames != null) & dtServiceNames.Rows.Count > 0)
                    {
                        //cmbServiceName.DataSource = dtServiceNames;
                        //cmbServiceName.DisplayMember = "sServiceName";
                        //cmbServiceName.ValueMember = "sServiceName";
                        //cmbServiceName.SelectedIndex = 0;
                        foreach (DataRow _dataRow in dtServiceNames.Rows)
                        {
                            cmbServiceName.Items.Add(Convert.ToString(_dataRow["sServiceName"]));
                        }
                    }

                    objDbLayer.Disconnect();

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if ((objDbLayer != null))
                    {
                        objDbLayer.Dispose();
                    }
                    _strSQL = string.Empty;
                }
            }

        /// <summary>
        /// Function to retrieve message queue page size from Settings
        /// </summary>
        /// <returns></returns>
            private Int32 getMessageQueuePageSize()
            {
                object  objPageSize;
                gloDatabaseLayer.DBLayer oDbLayer = null;
                try
                {
                    oDbLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                    oDbLayer.Connect(false);
                    objPageSize = oDbLayer.ExecuteScalar_Query("select ISNULL(sSettingsValue,'') from Settings where UPPER(sSettingsName) ='GENERALMESSAGELOGPAGESIZE'");
                    oDbLayer.Disconnect();
                    if(Convert.ToString(objPageSize).Length==0)
                    {
                        return 1000;
                    }
                    else
                    {
                        return Convert.ToInt32(objPageSize);
                    }
                }
                catch (Exception)
                {
                    return 1000;
                }
                finally
                {
                    if (oDbLayer != null)
                    {
                        oDbLayer.Dispose();
                        oDbLayer = null;
                    }
                }
            }
        #endregion "User Defined Functions"

            private void frmHL7_MessageQueue_Shown(object sender, EventArgs e)
            {
                BindGrid();
            }

            private void btnClear_Click(object sender, EventArgs e)
            {
                txtSearch.ResetText();
                txtSearch.Focus();
            }

            
           
            int ind = 0;
            private void C1Flex_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e) //added for bugid 72124: to solve selected row after sorting column issue
            {
            

                try
                {
                    if (ind > -1)
                    {
                        foreach (C1.Win.C1FlexGrid.Row rw in C1Flex.Rows)
                        {
                            CurrencyManager cm = (CurrencyManager)BindingContext[this.C1Flex.DataSource];
                            DataRowView dr = rw.DataSource as DataRowView;
                            if (dr != null)
                            {
                                int currIndex = dr.Row.Table.Rows.IndexOf(dr.Row);
                                if (currIndex == ind)
                                {
                                    CellRange cr = C1Flex.GetCellRange(rw.Index, 1);
                                    // to scroll the selected row in the visible area
                                    C1Flex.Select(cr, true);
                                    cr = C1Flex.GetCellRange(rw.Index, 0, rw.Index, C1Flex.Cols.Count - 1);
                                    C1Flex.Select(cr, false);
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(" InterfaceMessageQueue AfterSort " + ex.Message.ToString(), false);
                }
            }

        private void C1Flex_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if ((C1Flex.DataSource != null) && (C1Flex.Rows.Count > 0))
                {
                    CurrencyManager cm = (CurrencyManager)BindingContext[this.C1Flex.DataSource];
                    DataRowView dr = cm.Current as DataRowView;
                    ind = dr.Row.Table.Rows.IndexOf(dr.Row);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(" InterfaceMessageQueue MouseClick " + ex.Message.ToString(), false);
            }
             }

     

        

      
    
       }
}
