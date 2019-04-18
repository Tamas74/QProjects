Imports System.Data.SqlClient
Imports System.IO
Partial Public Class frmGloService
    Inherits Form
#Region "Private Varibles"
    Private _databaseconnectionstring As String = ""
    Private _messageBoxCaption As String = ""

    ''Added ServicesDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table instead of Hardcoding
    ''Dim strgloServiceDatabaseName As String = "gloServices"
    ' Dim strgloServiceDatabaseName As String = gstrServicesDBName
  
    ''Added ServicesDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table instead of Hardcoding

    Private _CPTMappingId As Int64 = 0
    Private _ClinicID As Int64 = 0
    Private _UserID As Int64 = 0
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Dim oClsServiceConfiguration As New ClsServiceConfiguration
#End Region

#Region "Constructor and Form Load"
    Private _getService As DataTable
    Dim isSaveAndClose As Boolean

    Public Sub New()

        InitializeComponent()
        _databaseconnectionstring = mdlGeneral.GetConnectionString()

        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID") <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
            Else
                _ClinicID = 0
            End If
        Else
            _ClinicID = 0
        End If

        If appSettings("UserID") IsNot Nothing Then
            If appSettings("UserID") <> "" Then
                _UserID = Convert.ToInt64(appSettings("UserID"))
            Else
                _UserID = 1
            End If
        Else
            _UserID = 1
        End If

        '#Region " Retrieve MessageBoxCaption from AppSettings "

        If appSettings("MessageBOXCaption") IsNot Nothing Then
            If appSettings("MessageBOXCaption") <> "" Then
                _messageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _messageBoxCaption = "gloPM"
            End If
        Else
            _messageBoxCaption = "gloPM"

            '#End Region
        End If
    End Sub



    Private Sub frmGloService_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillServerCombo()
        FillServicesGrid()
        FillSettings()
        oClsServiceConfiguration.GetSettings()
        If oClsServiceConfiguration.sSendBatchTime = "" Then

            dtpApp_DateTime_StartTime.Value = CType(Now.Date & " 16:00:00", DateTime)
        Else
            '' dtpApp_DateTime_StartTime.Value = Convert.ToDateTime(oClsServiceConfiguration.sSendBatchTime)
            dtpApp_DateTime_StartTime.Value = (oClsServiceConfiguration.DateAsDateTime(TimeSpan.Parse(oClsServiceConfiguration.sSendBatchTime)))
        End If

        If oClsServiceConfiguration.sCheckResponseTime = "" Then
            cmbChkResponseDurTime.Text = "02:00"
        Else
            cmbChkResponseDurTime.Text = Convert.ToString(oClsServiceConfiguration.sCheckResponseTime)
        End If

        If oClsServiceConfiguration.sTerminateCheckResponseTime = "" Then

            dtTerminatAptAfterTime.Value = CType(Now.Date & " 09:00:00", DateTime)
        Else
            dtTerminatAptAfterTime.Value = (oClsServiceConfiguration.DateAsDateTime(TimeSpan.Parse(oClsServiceConfiguration.sTerminateCheckResponseTime)))
        End If

    End Sub
#End Region

#Region "Private Methods"
    Private Sub FillSettings()
        Dim _ds As New DataSet()
        Dim dtServicesDatabases As New DataTable()
        Dim sDataBaseName As String
        Dim sServiceName As String
        Dim bIsEnable As Boolean
        Dim sServerName As String = ""
        Dim nDbConnectionId As Integer
        _ds = getServicesDatabases()
        dtServicesDatabases = _ds.Tables(2)
        If dtServicesDatabases IsNot Nothing AndAlso dtServicesDatabases.Rows.Count > 0 Then
            For _FirstIndex As Integer = 1 To c1services.Rows.Count - 1
                For _FirstColumn As Integer = 1 To c1services.Cols.Count - 2
                    sDataBaseName = c1services.GetData(_FirstIndex, 0)
                    sServiceName = c1services.GetData(0, _FirstColumn)
                    sServiceName = c1services.Cols(_FirstColumn).Name
                    bIsEnable = c1services.GetData(_FirstIndex, _FirstColumn)
                    nDbConnectionId = c1services.GetData(_FirstIndex, c1services.Cols.Count - 1)

                    For dtServicesDatabasesrow As Integer = 0 To dtServicesDatabases.Rows.Count - 1
                        If dtServicesDatabases.Rows(dtServicesDatabasesrow)(1) = sServiceName AndAlso dtServicesDatabases.Rows(dtServicesDatabasesrow)(5) = sDataBaseName Then
                            c1services.SetData(_FirstIndex, sServiceName, True)
                        End If
                    Next
                Next

            Next
        Else
            'c1services.BeginUpdate()
            'c1services.Rows.Count = 1
            'c1services.EndUpdate()
        End If

    End Sub
    Private Sub FillServicesGrid()
       
        Try
            Dim objgloServicesDatabase As New ClsMultipleDb
            Dim dtDatabaseValue As New DataTable
            Dim dtservice As New DataTable
            dtDatabaseValue = objgloServicesDatabase.GetDistinctServiceDatabaseName(cmbdatabase.Text)
            dtservice = GetService()
            If dtservice IsNot Nothing AndAlso dtservice.Rows.Count > 0 Then
                Designe(dtservice)
            Else
                c1services.BeginUpdate()
                c1services.Rows.Count = 1
                c1services.EndUpdate()
                Return
            End If
            If dtDatabaseValue IsNot Nothing AndAlso dtDatabaseValue.Rows.Count > 0 Then
                For _FirstIndex As Integer = 0 To dtDatabaseValue.Rows.Count - 1
                    c1services.Rows.Add()
                    c1services.SetData(_FirstIndex + 1, 0, dtDatabaseValue.Rows(_FirstIndex)(0))
                    ''c1services.SetData(_FirstIndex + 1, c1services.Cols.Count - 1, dtDatabaseValue.Rows(_FirstIndex)(1))


                Next
            Else
                c1services.BeginUpdate()
                c1services.Rows.Count = 1
                c1services.EndUpdate()


            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Designe(ByVal dtService As DataTable)
        c1services.BeginUpdate()
        c1services.Rows.Count = 1
        c1services.EndUpdate()
        Dim a As Integer = c1services.Rows.Count
        c1services.ExtendLastCol = False
        c1services.Rows.Fixed = 1
        c1services.Cols.Fixed = 0
        '#Region "SET HEADER"
        For _FirstIndex As Integer = 0 To dtService.Rows.Count - 1
            c1services.Cols.Count = dtService.Rows.Count + 2
            c1services.SetData(0, 0, "Database/Service")
            c1services.Cols(0).AllowEditing = False
            c1services.Cols(0).DataType = GetType(String)
            c1services.SetData(0, _FirstIndex + 1, dtService.Rows(_FirstIndex)(4))
            c1services.Cols(_FirstIndex + 1).Name = dtService.Rows(_FirstIndex)(1).ToString()
            c1services.Cols(_FirstIndex + 1).DataType = GetType([Boolean])
            If (dtService.Rows(_FirstIndex)(1).ToString()) <> "gloBatchEligibility" Then
                c1services.Cols(_FirstIndex + 1).AllowEditing = True
                c1services.Cols(_FirstIndex + 1).Visible = dtService.Rows(_FirstIndex)(2).ToString()
                c1services.Cols(_FirstIndex + 1).AllowEditing = dtService.Rows(_FirstIndex)(3).ToString()
            End If
            c1services.SetData(0, dtService.Rows.Count + 1, "DbConnectionID")
            c1services.Cols(dtService.Rows.Count + 1).Visible = False
            c1services.Cols(0).Width = Convert.ToInt32(c1services.Width * 0.3)
            c1services.Cols(_FirstIndex + 1).Width = Convert.ToInt32(c1services.Width * 0.3)
            Tb_gloDMS.Hide()
            Tb_gloGenius.Hide()
            ServicesSettings.TabPages.Remove(Tb_gloDMS)
            ServicesSettings.TabPages.Remove(Tb_gloGenius)
        Next

    End Sub

    Private Sub fillServerCombo()
        RemoveHandler cmbdatabase.SelectedIndexChanged, AddressOf cmbdatabase_SelectedIndexChanged
        Dim _dtServerName As DataTable
        _dtServerName = GetserverName()
        If _dtServerName IsNot Nothing AndAlso _dtServerName.Rows.Count > 0 Then
            cmbdatabase.BeginUpdate()
            cmbdatabase.DataSource = _dtServerName
            cmbdatabase.DisplayMember = "sServerName"
            cmbdatabase.ValueMember = "sServerName"
            cmbdatabase.EndUpdate()
            cmbdatabase.Text = gstrSQLServerName
        End If
        AddHandler cmbdatabase.SelectedIndexChanged, AddressOf cmbdatabase_SelectedIndexChanged
    End Sub
    Private Function GetserverName() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim _strSQL As String = ""
        Dim dt As New DataTable()

        Try
            oDB.Connect(False)
            _strSQL = "select distinct sServerName from gloServices.dbo.DBSettings  Where sServiceName='gloEMR' OR sServiceName='BatchEligibility' order by sServerName "
            oDB.Retrive_Query(_strSQL, dt)
        Catch ex As Exception
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
        Return dt
    End Function

    Private Function GetService() As DataTable

        Dim con As New SqlConnection
        ''getting the connection string
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        Dim ad As SqlDataAdapter
        Dim dt As New DataTable
        Dim _query As String
        Dim dr As DataRow
        Try

            _query = "select nProductID , sProductName ,bIsProductVisible,bIsProductEnabled,sDisplayName from gloProducts"

            ad = New SqlDataAdapter(_query, con)
            ''filling the data into the datatable
            ad.Fill(dt)
            Return dt
        Catch ex As Exception

        End Try
    End Function

    Private Function getServicesDatabases() As DataSet
        Dim con As New SqlConnection
        Dim dtServicesDatabases As New DataSet()
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        Dim oDB As New gloDatabaseLayer.DBLayer(con.ConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Try

            oDB.Connect(False)
            oParameters.Add("@sServerName", cmbdatabase.Text, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Retrive("Get_gloServiceConfiguration", oParameters, dtServicesDatabases)
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
            If oParameters IsNot Nothing Then
                oParameters.Dispose()
            End If
        End Try
        Return dtServicesDatabases
    End Function


#End Region

#Region "Form Event"

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()

    End Sub

    Private Sub ts_btnSaveClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnSaveClose.Click
        Dim con As New SqlConnection
        Dim sDataBaseName As String
        Dim sServiceName As String
        Dim bIsEnable As Boolean
        Dim sServerName As String = ""
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        Dim oDB As New gloDatabaseLayer.DBLayer(con.ConnectionString)
        sServerName = cmbdatabase.Text
        If sServerName = "" Then
            isSaveAndClose = True
            Me.Close()
            Return
        End If
        For _FirstIndex As Integer = 1 To c1services.Rows.Count - 1
            For _FirstColumn As Integer = 1 To c1services.Cols.Count - 2
                sDataBaseName = c1services.GetData(_FirstIndex, 0)
                sServiceName = c1services.GetData(0, _FirstColumn)
                sServiceName = c1services.Cols(_FirstColumn).Name
                bIsEnable = c1services.GetData(_FirstIndex, _FirstColumn)
                Dim oParameters As New gloDatabaseLayer.DBParameters()
                Try
                    oDB.Connect(False)
                    oParameters.Add("@nDBId", 0, ParameterDirection.Input, SqlDbType.BigInt)
                    oParameters.Add("@sDatabaseName", sDataBaseName, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@sServerName", sServerName, ParameterDirection.Input, SqlDbType.VarChar)
                    ''oParameters.Add("@sSqlUserName", gstrSQLUserEMR, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@sServiceName", sServiceName, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@bEnabled", bIsEnable, ParameterDirection.Input, SqlDbType.Bit)
                    oParameters.Add("@bIsConnected", 1, ParameterDirection.Input, SqlDbType.Bit)

                    oDB.Execute("gsp_INUP_DbConnections", oParameters)
                    oDB.Disconnect()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                Finally
                    If oDB IsNot Nothing Then
                        oDB.Dispose()
                    End If
                    If oParameters IsNot Nothing Then
                        oParameters.Dispose()
                    End If
                End Try
            Next

        Next
        oClsServiceConfiguration.UpdateSettings("bSendBatchTime", Convert.ToString(dtpApp_DateTime_StartTime.Value.TimeOfDay))
        oClsServiceConfiguration.UpdateSettings("bCheckResponseTime", cmbChkResponseDurTime.Text)
        oClsServiceConfiguration.UpdateSettings("bTerminateCheckResponseTime", Convert.ToString(dtTerminatAptAfterTime.Value.TimeOfDay))


        isSaveAndClose = True
        Me.Close()
    End Sub

    Private Sub tsb_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_Save.Click
        Dim con As New SqlConnection
        Dim sDataBaseName As String
        Dim sServiceName As String
        Dim bIsEnable As Boolean
        Dim sServerName As String = ""
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        Dim oDB As New gloDatabaseLayer.DBLayer(con.ConnectionString)
        sServerName = cmbdatabase.Text
        If sServerName = "" Then
            isSaveAndClose = True
            Me.Close()
            Return
            Return
        End If
       For _FirstIndex As Integer = 1 To c1services.Rows.Count - 1
            For _FirstColumn As Integer = 1 To c1services.Cols.Count - 2
                sDataBaseName = c1services.GetData(_FirstIndex, 0)
                sServiceName = c1services.GetData(0, _FirstColumn)
                sServiceName = c1services.Cols(_FirstColumn).Name
                bIsEnable = c1services.GetData(_FirstIndex, _FirstColumn)
                Dim oParameters As New gloDatabaseLayer.DBParameters()
                Try
                  
                    oDB.Connect(False)
                    oParameters.Add("@nDBId", 0, ParameterDirection.Input, SqlDbType.BigInt)
                    oParameters.Add("@sDatabaseName", sDataBaseName, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@sServerName", sServerName, ParameterDirection.Input, SqlDbType.VarChar)
                    '' oParameters.Add("@sSqlUserName", gstrSQLUserEMR, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@sServiceName", sServiceName, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@bEnabled", bIsEnable, ParameterDirection.Input, SqlDbType.Bit)
                    oParameters.Add("@bIsConnected", 1, ParameterDirection.Input, SqlDbType.Bit)
                    oDB.Execute("gsp_INUP_DbConnections", oParameters)
                    oDB.Disconnect()
                    isSaveAndClose = True
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                Finally
                    If oDB IsNot Nothing Then
                        oDB.Dispose()
                    End If
                    If oParameters IsNot Nothing Then
                        oParameters.Dispose()
                    End If
                End Try
            Next

        Next
        oClsServiceConfiguration.UpdateSettings("bSendBatchTime", Convert.ToString(dtpApp_DateTime_StartTime.Value.TimeOfDay))
        oClsServiceConfiguration.UpdateSettings("bCheckResponseTime", cmbChkResponseDurTime.Text)
        oClsServiceConfiguration.UpdateSettings("bTerminateCheckResponseTime", Convert.ToString(dtTerminatAptAfterTime.Value.TimeOfDay))
    End Sub

    Private Sub cmbdatabase_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdatabase.SelectedIndexChanged
        FillServicesGrid()
        FillSettings()
    End Sub

    Private Sub c1services_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1services.MouseMove
        gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub

    Private Sub frmGloService_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If isSaveAndClose = False Then
            Dim res As DialogResult = MessageBox.Show("Do you want to save changes ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
            If res = DialogResult.Yes Then
                tsb_Save_Click(sender, e)
            End If
            If res = DialogResult.No Then

            End If
            If res = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If

    End Sub
#End Region

   

End Class