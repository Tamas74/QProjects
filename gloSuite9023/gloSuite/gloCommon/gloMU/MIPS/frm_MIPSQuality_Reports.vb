Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml
Imports System.Drawing
Imports gloCCDLibrary
Imports WPF = System.Windows.Forms.Integration

Public Class frm_MIPSQuality_Reports

    Dim mPatient As gloCCDLibrary.Patient = Nothing
    Dim dtStratum1 As DataTable
    Dim dtStratum2 As DataTable
    Dim dtCount As DataTable
    Dim dtCodes As DataTable
    Dim dtQRDAIPatientList As DataTable
    Dim dtQRDAIMeasureWisePatientList As DataTable

    Dim dtQRDA1Data As DataTable
    Dim dtMeasureList As DataTable

    Dim dtVistBasedDenominator As DataTable
    Dim dtVistBasedNumerator As DataTable

    ''Column 
    Private COL_Check As Integer = 0
    Private COL_CoreMeasure As Integer = 1
    Private COL_Button As Integer = 2
    Private COL_Numeratot As Integer = 4
    Private COL_Denomenator As Integer = 3
    Private COL_Exclusion As Integer = 5
    Private COL_Percent As Integer = 6
    Private COL_SP_Name As Integer = 7
    Private COL_Measure_Name As Integer = 8
    Private COL_DPatientID As Integer = 9
    Private COL_NPatientID As Integer = 10

    ''Column 
    Private COL_Check_2014 As Integer = 0
    Private COL_Domain_2014 As Integer = 1
    Private COL_CoreMeasure_2014 As Integer = 2
    Private COL_Button_2014 As Integer = 3
    Private COL_Quality_CAT As Integer = 4

    Private COL_IPP_2014 As Integer = 5
    Private COL_Denomenator_2014 As Integer = 6
    Private COL_Numeratot_2014 As Integer = 7

    Private COL_Exclusion_2014 As Integer = 8

    Private COL_Exception_2014 As Integer = 9
    Private COL_Percent_2014 As Integer = 10

    Private COL_SP_Name_2014 As Integer = 11
    Private COL_Measure_Name_2014 As Integer = 12
    Private COL_DPatientID_2014 As Integer = 13
    Private COL_NPatientID_2014 As Integer = 14


    Private COL_DenominatorExclusionsPatientID_2014 As Integer = 15
    Private COL_DenominatorExceptionsPatientID_2014 As Integer = 16
    Private Col_Icon_2014 As Integer = 17
    Private Col_MeasureID_2014 As Integer = 18
    ''Row
    Private ROW_NQF0002_2014 As Integer = 1
    Private ROW_NQF0018_2014 As Integer = 4
    Private ROW_NQF0028_2014 As Integer = 7
    Private ROW_NQF0031_2014 As Integer = 14 '10
    Private ROW_NQF0032_2014 As Integer = 17 '13
    Private ROW_NQF0033_2014 As Integer = 20 '13
    Private ROW_NQF0034_2014 As Integer = 23 '13
    Private ROW_NQF0038_2014 As Integer = 26 '7
    Private ROW_NQF0041_2014 As Integer = 29 '16
    Private ROW_NQF0043_2014 As Integer = 32 '19
    Private ROW_NQF0052_2014 As Integer = 35 '22
    Private ROW_NQF0055_2014 As Integer = 38 '25
    Private ROW_NQF0056_2014 As Integer = 41 '28
    Private ROW_NQF0059_2014 As Integer = 44 '31
    Private ROW_NQF0062_2014 As Integer = 47 '34
    Private ROW_NQF0064_2014 As Integer = 50 '37
    Private ROW_NQF0068_2014 As Integer = 53 '40
    Private ROW_NQF0101_2014 As Integer = 56 '43
    Private ROW_NQF0418_2014 As Integer = 59 '46
    Private ROW_NQF0419_2014 As Integer = 62 '49
    Private ROW_NQF0421_2014 As Integer = 65 '52 Multiple IPP

    Private ROW_NQF_CMSID22_2014 As Integer = 68 '66
    Private ROW_NQF_CMSID56_2014 As Integer = 71 '69
    Private ROW_NQF_CMSID66_2014 As Integer = 74 '72
    Private ROW_NQF_CMSID90_2014 As Integer = 77 '75
    Private ROW_NQF_CMSID125_2014 As Integer = 80 '78

    'Private ROW_NQF0018_2014 As Integer = 32

    Private ROW_CoreMeasure = 1
    Private ROW_NQF0421 As Integer = 2
    Private ROW_NQF0013 As Integer = 7
    Private ROW_NQF0028a As Integer = 10
    Private ROW_NQF0028b As Integer = 13
    Private ROW_AlternateCOreMeaseure = 16
    Private ROW_NQF0041 As Integer = 17
    Private ROW_NQF0024 As Integer = 20
    Private ROW_NQF0038 As Integer = 33
    Private ROW_MenuSetMeaseure = 47
    Private ROW_NQF0001 As Integer = 48
    Private ROW_NQF0002 As Integer = 51
    Private ROW_NQF0004 As Integer = 54
    Private ROW_NQF0012 As Integer = 64
    Private ROW_NQF0014 As Integer = 67
    Private ROW_NQF0018 As Integer = 70
    Private ROW_NQF0027 As Integer = 73

    Private ROW_NQF0031 As Integer = 77


    Private ROW_NQF0032 As Integer = 80


    Private ROW_NQF0033 As Integer = 83
    Private ROW_NQF0034 As Integer = 90
    Private ROW_NQF0036 As Integer = 93
    Private ROW_NQF0043 As Integer = 100


    Private ROW_NQF0047 As Integer = 103
    Private ROW_NQF0052 As Integer = 106

    Private ROW_NQF0055 As Integer = 109


    Private ROW_NQF0056 As Integer = 112


    Private ROW_NQF0059 As Integer = 115


    Private ROW_NQF0061 As Integer = 118
    Private ROW_NQF0062 As Integer = 121


    Private ROW_NQF0064 As Integer = 124


    Private ROW_NQF0067 As Integer = 128
    Private ROW_NQF0068 As Integer = 131


    Private ROW_NQF0070 As Integer = 134
    Private ROW_NQF0073 As Integer = 137
    Private ROW_NQF0074 As Integer = 140
    Private ROW_NQF0075 As Integer = 143


    Private ROW_NQF0081 As Integer = 147
    Private ROW_NQF0083 As Integer = 150
    Private ROW_NQF0084 As Integer = 153
    Private ROW_NQF0086 As Integer = 156
    Private ROW_NQF0088 As Integer = 159
    Private ROW_NQF0089 As Integer = 162
    Private ROW_NQF0105 As Integer = 165
    Private ROW_NQF0385 As Integer = 169
    Private ROW_NQF0387 As Integer = 172
    Private ROW_NQF0389 As Integer = 175
    Private ROW_NQF0575 As Integer = 178

    ''Sanjog-Added On 20101207 to show the Provider Name and the start and End Date of Dashboard
    Private PName As String = ""
    Private SDate As String = ""
    Private EDate As String = ""
    ''Sanjog-Added On 20101207 to show the Provider Name and the start and End Date of Dashboard

    Dim j As Integer = 0
    Private _databaseConnectionString As String = String.Empty
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    Dim Con As New SqlConnection(_databaseConnectionString)
    Dim ofrmList As frmViewListControl
    ' Private oListControl As gloListControl.gloListControl
    Dim oListhistoryItem As gloListControl.gloListControl
    Private HistoryItemList As gloGeneralItem.gloItems
    Dim _HistoryItemList As String = ""
    Private NQF0028bHistoryItemList As gloGeneralItem.gloItems

    Dim _NQF0028bHistoryItemList As String = ""
    Dim objPatient As New cls_MU_Measures
    Dim _ClinicID As Int64 = 0
    Dim _UserID As Int64 = 0
    Private _MessageBoxCaption As String = String.Empty
    Private dgCustomGrid As gloUserControlLibrary.CustomTask = Nothing
    Private dgCustomGridProv As gloUserControlLibrary.CustomTask = Nothing
    'By Pranit on 16 march 2012
    Private ProviderID As Int64 = 0
    Private StartDate As String
    Private Enddate As String
    Private PatientID As Int64 = 0
    Private strPatientDetails As String
    Private _strUsername As String
    Public Property UserName As String
        Get
            Return _strUsername
        End Get
        Set(value As String)
            _strUsername = value
        End Set
    End Property
    Dim lstRowVisibility As New List(Of RowVisibility)
    
    Private Sub frm_MIPSQuality_Reports_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        RemoveHandler chkCQM2014.CheckedChanged, AddressOf chkCQM2014_CheckedChanged
        If Not IsNothing(dtStratum1) Then
            dtStratum1.Dispose()
            dtStratum1.Dispose()
        End If
        If Not IsNothing(dtStratum2) Then
            dtStratum2.Dispose()
            dtStratum2.Dispose()
        End If


        If Not IsNothing(dtQRDAIPatientList) Then
            dtQRDAIPatientList.Dispose()
            dtQRDAIPatientList = Nothing
        End If

        If Not IsNothing(dtQRDAIMeasureWisePatientList) Then
            dtQRDAIMeasureWisePatientList.Dispose()
            dtQRDAIMeasureWisePatientList = Nothing
        End If

        If lstRowVisibility IsNot Nothing Then
            For Each element As RowVisibility In lstRowVisibility
                element.Dispose()
            Next
            lstRowVisibility.Clear()
            lstRowVisibility = Nothing
        End If

    End Sub

    'End By Pranit
    Private sDefaultQCheckpointMeasures As String = ""
    Private bHasQCheckpointMeasure As Boolean = False

    Private Sub frm_MIPSQuality_Reports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim oSettings As New gloSettings.GeneralSettings(_databaseConnectionString)
            pnlLoadingLable.SendToBack()
            '   FillProviders()

            '03-Jul-13 Aniket: Commented for CQM 2014
            Label6.Text = "Please select Provider && Measurement period"

            If PName <> "" Then
                cmbProviders.Text = PName
                dtpicStartDate.Value = SDate
                dtpicEndDate.Value = EDate
            End If

            '15-Apr-14 Aniket: Default the CQM to 2014+

            If (PatientID <> 0) Then
                oSettings.GetSetting("DefaultQCheckpointMeasures", sDefaultQCheckpointMeasures)
            End If
            FillGridCQM2014()
            LoadAddressControl()


            For row As Integer = 0 To C1QualityMeasures.Rows.Count - 1
                C1QualityMeasures.Rows(row).AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.None
                C1QualityMeasures.Rows(row).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            Next

            C1QualityMeasures.Cols(COL_CoreMeasure_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1QualityMeasures.Cols(COL_Domain_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1QualityMeasures.Cols(COL_Denomenator_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Numeratot_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Denomenator_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_DenominatorExceptionsPatientID_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_DenominatorExclusionsPatientID_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

            Dim dtFirstDate As Date
            Dim dtLastDate As Date

            GetReportingPeriod(cmbProviders.SelectedValue, dtFirstDate, dtLastDate)


            dtpicStartDate.Value = dtFirstDate
            dtpicEndDate.Value = dtLastDate

            If PatientID <> 0 Then

                Me.MinimizeBox = False

                lblPatientDetails.Text = "Showing Quality Dashboard data for the patient " & strPatientDetails & " and for the measures selected from admin"
                Me.Text = "Q Checkpoint" & " - " & strPatientDetails
                tlbbtn_Print.Enabled = False
                chkCQM2014.Enabled = False
                'SelectAll()
                GenerateReport_CQM2014()
            End If


            oSettings.Dispose()
            oSettings = Nothing

            AddHandler chkCQM2014.CheckedChanged, AddressOf chkCQM2014_CheckedChanged
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Dim oAddresscontrol = New gloAddress.gloAddressControl
    Private Sub LoadAddressControl()

        If oAddresscontrol IsNot Nothing Then
            If pnlProviderAddress.Controls.Contains(oAddresscontrol) Then
                pnlProviderAddress.Controls.Remove(oAddresscontrol)
            End If
            oAddresscontrol.Dispose()
            oAddresscontrol = Nothing
        End If
        oAddresscontrol = New gloAddress.gloAddressControl(_databaseConnectionString)
        Dim oSettings As New gloSettings.GeneralSettings(_databaseConnectionString)
        Dim oUseAreaCode As Object = Nothing
        oSettings.GetSetting("USEAREACODEFORPATIENT", oUseAreaCode)

        oAddresscontrol.UseAreaCodeForPatient = Convert.ToBoolean(Convert.ToInt16(oUseAreaCode))
        oAddresscontrol.SetAreaCode()
        oSettings.Dispose()
        oSettings = Nothing
        oAddresscontrol.Name = "DemographicsAddressControl"
        pnlProviderAddress.Controls.Add(oAddresscontrol)
    End Sub

    Private Sub GetReportingPeriod(ProviderID As Long, ByRef StartDate As Date, ByRef EndDate As Date)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dsMain As DataSet = Nothing



        Try
            oParameters = New gloDatabaseLayer.DBParameters

            oDB.Connect(False)

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Value = ProviderID
            oParameters.Add(oParameter)
            oParameter = Nothing

            'oParameter = New gloDatabaseLayer.DBParameter()
            'oParameter.ParameterName = "@ReportingYear"
            'oParameter.ParameterDirection = ParameterDirection.Input
            'oParameter.DataType = SqlDbType.Int
            'oParameter.Value = 2018
            'oParameters.Add(oParameter)
            'oParameter = Nothing


            oDB.Retrive("gsp_GetCQMReportingPeriod", oParameters, dsMain)
            oDB.Disconnect()

            If dsMain.Tables(0).Rows.Count = 0 Then
                StartDate = New Date(DateTime.Now.Year - 1, 1, 1)
                EndDate = New Date(DateTime.Now.Year - 1, 12, 31)
            Else
                StartDate = dsMain.Tables(0).Rows(0)("dtStartDate")
                EndDate = dsMain.Tables(0).Rows(0)("dtEndDate")
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

        Finally

            If IsNothing(dsMain) = False Then
                dsMain.Dispose()
                dsMain = Nothing
            End If

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Sub

    Private Sub SetReportingPeriod(ByVal dtProv As DataTable, StartDate As Date, EndDate As Date)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dsMain As DataSet = Nothing
        Dim dtProvider As DataTable = dtProv.Copy()


        Try
            oParameters = New gloDatabaseLayer.DBParameters

            oDB.Connect(False)

            If IsNothing(dtProvider) = False Then
                If (dtProvider.Columns.Contains("ProviderName")) Then
                    dtProvider.Columns.Remove("ProviderName")
                End If


                If IsNothing(dtProvider.Columns.Contains("nProviderID")) Then
                    dtProvider.Columns("nProviderID").ColumnName = "ProviderID"
                End If
            End If

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVP_Providers"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtProvider
            oParameters.Add(oParameter)
            oParameter = Nothing


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@StartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Date
            oParameter.Value = StartDate
            oParameters.Add(oParameter)
            oParameter = Nothing


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@EndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Date
            oParameter.Value = EndDate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Execute("gsp_SetCQMReportingPeriod", oParameters)
            oDB.Disconnect()


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

        Finally
            If Not IsNothing(dtProvider) Then
                dtProvider.Dispose()
                dtProvider = Nothing
            End If

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Sub

    Private Sub SetGridStyle()

        Try

            C1QualityMeasures.ShowCellLabels = True
            C1QualityMeasures.Cols.Count = 11
            C1QualityMeasures.Rows.Add()
            C1QualityMeasures.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1QualityMeasures.Cols(COL_Check).AllowEditing = True
            C1QualityMeasures.Cols(COL_Button).AllowEditing = True
            C1QualityMeasures.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            gloC1FlexStyle.Style(C1QualityMeasures)
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Check, "Select")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_CoreMeasure, "Measures Name")

            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Numeratot, "Numerator")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Denomenator, "Denominator")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Exclusion, "Exclusions")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Percent, "Percent")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_DPatientID, "DPatientID")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_NPatientID, "NPatientID")
            C1QualityMeasures.Cols(COL_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            C1QualityMeasures.Cols(COL_Denomenator).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Numeratot).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Exclusion).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Percent).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


            ''Visible
            C1QualityMeasures.Cols(COL_Check).Visible = True
            C1QualityMeasures.Cols(COL_CoreMeasure).Visible = True
            C1QualityMeasures.Cols(COL_Button).Visible = False
            C1QualityMeasures.Cols(COL_Numeratot).Visible = True
            C1QualityMeasures.Cols(COL_Denomenator).Visible = True
            C1QualityMeasures.Cols(COL_Exclusion).Visible = True
            C1QualityMeasures.Cols(COL_Percent).Visible = True

            '' By Pranit on 16 march 2012
            C1QualityMeasures.Cols(COL_SP_Name).Visible = False
            C1QualityMeasures.Cols(COL_Measure_Name).Visible = False
            C1QualityMeasures.Cols(COL_DPatientID).Visible = False
            C1QualityMeasures.Cols(COL_NPatientID).Visible = False

            C1QualityMeasures.Cols(COL_Check).Width = 47 ' Width * 0.05
            C1QualityMeasures.Cols(COL_CoreMeasure).Width = 800 'Width * 0.55

            C1QualityMeasures.Cols(COL_Numeratot).Width = 94 'Width * 0.1
            C1QualityMeasures.Cols(COL_Denomenator).Width = 94 'Width * 0.1
            C1QualityMeasures.Cols(COL_Exclusion).Width = 94 ' Width * 0.1
            C1QualityMeasures.Cols(COL_Percent).Width = 94 'Width * 0.1

            C1QualityMeasures.ExtendLastCol = False

            ''Editing
            C1QualityMeasures.Cols(COL_CoreMeasure).AllowEditing = False
            C1QualityMeasures.Cols(COL_Numeratot).AllowEditing = False
            C1QualityMeasures.Cols(COL_Denomenator).AllowEditing = False
            C1QualityMeasures.Cols(COL_Exclusion).AllowEditing = False
            C1QualityMeasures.Cols(COL_Percent).AllowEditing = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetGridStyle_2014()

        Try

            C1QualityMeasures.ShowCellLabels = True

            'Aniket: Increase the 2014 CQM column count here
            C1QualityMeasures.Cols.Count = 19
            C1QualityMeasures.Rows.Add()
            C1QualityMeasures.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1QualityMeasures.Cols(COL_Check_2014).AllowEditing = True


            C1QualityMeasures.Cols(COL_Button_2014).AllowEditing = True
            C1QualityMeasures.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

            gloC1FlexStyle.Style(C1QualityMeasures)
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Check_2014, "Select")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Domain_2014, "(Category) Domain")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_CoreMeasure_2014, "Measures Name")

            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Quality_CAT, "QUAL. CAT")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_IPP_2014, "IPP")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Denomenator_2014, "DEN")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Numeratot_2014, "NUM")

            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Exclusion_2014, "EXCL")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Exception_2014, "EXCP")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_Percent_2014, "Percent")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_DPatientID_2014, "DPatientID")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_NPatientID_2014, "NPatientID")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, Col_Icon_2014, "")

            C1QualityMeasures.Cols(COL_Quality_CAT).DataType = GetType(System.Drawing.Image)
            C1QualityMeasures.Cols(Col_Icon_2014).DataType = GetType(System.Drawing.Image)
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_DenominatorExclusionsPatientID_2014, "DenominatorExclusionsPatientID")
            C1QualityMeasures.SetData(ROW_CoreMeasure - 1, COL_DenominatorExceptionsPatientID_2014, "DenominatorExceptionsPatientID")

            C1QualityMeasures.Cols(COL_Check_2014).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_IPP_2014).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Denomenator_2014).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Numeratot_2014).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Exclusion_2014).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Exception_2014).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Percent_2014).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


            ''Visible
            C1QualityMeasures.Cols(COL_Check_2014).Visible = True
            C1QualityMeasures.Cols(COL_CoreMeasure_2014).Visible = True
            C1QualityMeasures.Cols(COL_Button_2014).Visible = False
            C1QualityMeasures.Cols(COL_Quality_CAT).Visible = True
            C1QualityMeasures.Cols(Col_Icon_2014).Visible = True



            C1QualityMeasures.Cols(COL_IPP_2014).Visible = True
            C1QualityMeasures.Cols(COL_Denomenator_2014).Visible = True
            C1QualityMeasures.Cols(COL_Numeratot_2014).Visible = True
            C1QualityMeasures.Cols(COL_Exclusion_2014).Visible = True
            C1QualityMeasures.Cols(COL_Exception_2014).Visible = True
            C1QualityMeasures.Cols(COL_Percent_2014).Visible = True

            '' By Pranit on 16 march 2012
            C1QualityMeasures.Cols(COL_SP_Name_2014).Visible = False
            C1QualityMeasures.Cols(COL_Measure_Name_2014).Visible = False
            C1QualityMeasures.Cols(COL_DPatientID_2014).Visible = False
            C1QualityMeasures.Cols(COL_NPatientID_2014).Visible = False

            C1QualityMeasures.Cols(COL_DenominatorExclusionsPatientID_2014).Visible = False
            C1QualityMeasures.Cols(COL_DenominatorExceptionsPatientID_2014).Visible = False
            C1QualityMeasures.Cols(Col_MeasureID_2014).Visible = False

            C1QualityMeasures.Cols(COL_Check_2014).Width = 47 ' Width * 0.05
            C1QualityMeasures.Cols(COL_Domain_2014).Width = 215 ' Width * 0.05
            C1QualityMeasures.Cols(COL_CoreMeasure_2014).Width = 470 'Width * 0.55


            C1QualityMeasures.Cols(COL_Quality_CAT).Width = 80
            C1QualityMeasures.Cols(Col_Icon_2014).Width = 40
            C1QualityMeasures.Cols(COL_IPP_2014).Width = 60 'Width * 0.1
            C1QualityMeasures.Cols(COL_Denomenator_2014).Width = 60 'Width * 0.1
            C1QualityMeasures.Cols(COL_Numeratot_2014).Width = 60 'Width * 0.1
            C1QualityMeasures.Cols(COL_Exclusion_2014).Width = 60 ' Width * 0.1
            C1QualityMeasures.Cols(COL_Exception_2014).Width = 60 ' Width * 0.1
            C1QualityMeasures.Cols(COL_Percent_2014).Width = 65 'Width * 0.1

            C1QualityMeasures.ExtendLastCol = False

            ''Editing
            C1QualityMeasures.Cols(COL_CoreMeasure_2014).AllowEditing = False
            C1QualityMeasures.Cols(COL_Numeratot_2014).AllowEditing = False
            C1QualityMeasures.Cols(COL_Denomenator_2014).AllowEditing = False
            C1QualityMeasures.Cols(COL_Exclusion_2014).AllowEditing = False
            C1QualityMeasures.Cols(COL_Percent_2014).AllowEditing = False
            C1QualityMeasures.Cols(COL_IPP_2014).AllowEditing = False
            C1QualityMeasures.Cols(Col_Icon_2014).AllowEditing = False
            C1QualityMeasures.Cols(COL_Quality_CAT).AllowEditing = False

            '29-Jan-13 Aniket: Resolved Bug #62723 
            C1QualityMeasures.Cols(COL_Exception_2014).AllowEditing = False

            '3-Jul-2013 Aniket: Resolving Bug #85722: EMR: Quality Mesaurements- (Category) Domain Column fields should not be editable
            C1QualityMeasures.Cols(COL_Domain_2014).AllowEditing = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillGridCQM2014()
        Dim rowVisibility As RowVisibility = Nothing        

        Dim dQCheckPointValue As New List(Of String)
        If (PatientID > 0) Then
            Dim sDefaultQCheckpointMeasuresArray As String() = Nothing
            Dim sQCheckPointValue As String() = Nothing
            ' Dim dQCheckPointValue As String() = Nothing

            If sDefaultQCheckpointMeasures IsNot Nothing Then
                sDefaultQCheckpointMeasuresArray = sDefaultQCheckpointMeasures.Split("|")

                For Each CMSID As String In sDefaultQCheckpointMeasuresArray
                    sQCheckPointValue = CMSID.Split("#")

                    If sQCheckPointValue.Length > 1 Then
                        If sQCheckPointValue(1).Trim() = "1" Then
                            dQCheckPointValue.Add(sQCheckPointValue(0))
                            bHasQCheckpointMeasure = True                            
                        End If
                    End If
                Next
            End If
        End If
        Dim dt As DataTable = New DataTable()

        C1QualityMeasures.Clear()
        C1QualityMeasures.Styles.Clear()
        C1QualityMeasures.Rows.Count = 1

        SetGridStyle_2014()

        C1QualityMeasures.Rows.Add(81) '77

        Dim i As Integer

        For i = 1 To 51
            C1QualityMeasures.Rows(i).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Next

        '  C1QualityMeasures.Rows(ROW_CoreMeasure).AllowMerging = True

        'NQF0002
        C1QualityMeasures.SetCellCheck(ROW_NQF0002_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS146"))
        C1QualityMeasures.Rows(ROW_NQF0002_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0002_2014, COL_Domain_2014, "(Core) Efficient Use of Healthcare Resources")
        C1QualityMeasures.SetData(ROW_NQF0002_2014, COL_CoreMeasure_2014, "CMS146/NQF 0002: Appropriate Testing for Children with Pharyngitis")
        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Children 3-18 years of age who had an outpatient or emergency department (ED) visit with a diagnosis of pharyngitis during the measurement period and an antibiotic ordered on or three days after the visit")
        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Children with a group A streptococcus test in the 7-day period from 3 days prior through 3 days after the diagnosis of pharyngitis")
        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_SP_Name_2014, "MU_NQF0002")
        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Measure_Name_2014, "CMS146/NQF 0002: Appropriate Testing for Children with Pharyngitis")
        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, Col_MeasureID_2014, "NQF0002")
        'Dim crange2 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0002_2014, COL_Quality_CAT, ROW_NQF0002_2014 + 2, COL_Quality_CAT)
        'crange2.Image = gloMU.My.Resources.HighPriorityNew

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0002_2014), C1QualityMeasures.Rows(ROW_NQF0002_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0002_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing


        Dim crange2 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0002_2014, COL_Quality_CAT, ROW_NQF0002_2014 + 2, COL_Quality_CAT)


        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        ' crange2.StyleNew.Clear()
        crange2.Data = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)
        crange2.StyleNew.BackColor = Color.FromArgb(199, 211, 235)
        C1QualityMeasures.Cols(COL_Quality_CAT).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0002_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0002_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0002_2014 + 2).AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly
        ' Dim crange As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0002_2014, COL_Quality_CAT, ROW_NQF0002_2014 + 2, COL_Quality_CAT)


        ' crange.Data = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)

        'crange2.IsSingleCell = Color.Magenta
        'FpSpread1.Sheets(0).Columns(1, 2).Width = 100
        'FpSpread1.Sheets(0).Rows(1, 1).Height = 50
        'FpSpread1.Sheets(0).Cells(1, 1, 2, 2).CellType = imgct
        'FpSpread1.Sheets(0).Cells(1, 1).Value = Image
        ' Dim crange As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0002_2014, COL_Quality_CAT, ROW_NQF0002_2014 + 2, COL_Quality_CAT)
        ' crange.UserData = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)

        'Dim crange As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(1, 5, 15, 5)
        ' crange.StyleDisplay.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
        'crange.StyleDisplay.Border.Width = 0


        'crange2.StyleDisplay.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Vertical
        'crange2.StyleDisplay.Border.Width = 1
        ''  crange.StyleDisplay.BackColor = Color.White
        '  C1QualityMeasures.Cols(COL_Quality_CAT + 1).StyleDisplay.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Vertical
        'C1QualityMeasures.Cols(COL_Quality_CAT + 1).StyleDisplay.Border.Width = 1
        'NQF0018
        C1QualityMeasures.SetCellCheck(ROW_NQF0018_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS165"))
        C1QualityMeasures.Rows(ROW_NQF0018_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0018_2014, COL_Domain_2014, "(Core) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0018_2014, COL_CoreMeasure_2014, "CMS165/NQF 0018: Controlling High Blood Pressure")
        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18-85 years of age who had a diagnosis of essential hypertension within the first six months of the measurement period or any time prior to the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients whose blood pressure at the most recent visit is adequately controlled (systolic blood pressure < 140 mmHg and diastolic blood pressure < 90 mmHg) during the measurement period.")
        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_SP_Name_2014, "MU_NQF0018")
        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Measure_Name_2014, "CMS165/NQF 0018: Controlling High Blood Pressure")
        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, Col_MeasureID_2014, "NQF0018")
        Dim crangeNQF0018 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0018_2014, COL_Quality_CAT, ROW_NQF0018_2014 + 2, COL_Quality_CAT)

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0018_2014), C1QualityMeasures.Rows(ROW_NQF0018_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0018_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        ' crangeNQF0018.StyleNew.Clear()
        crangeNQF0018.Data = CType(gloMU.My.Resources.Outcome_HighPriority_CrossCutting, Bitmap)
        crangeNQF0018.StyleNew.BackColor = Color.FromArgb(199, 211, 235)
        C1QualityMeasures.Rows(ROW_NQF0018_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0018_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0018_2014 + 2).AllowMerging = True

        'NQF0028
        C1QualityMeasures.SetCellCheck(ROW_NQF0028_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS138"))
        C1QualityMeasures.Rows(ROW_NQF0028_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0028_2014, COL_Domain_2014, "(Core) Population/ Public Health")
        C1QualityMeasures.SetData(ROW_NQF0028_2014, COL_CoreMeasure_2014, "CMS138/NQF 0028: Preventive Care and Screening: Tobacco Use: Screening and Cessation Intervention")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: All patients aged 18 years and older seen for at least two visits or at least one preventive visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients who were screened for tobacco use at least once within 24 months")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_SP_Name_2014, "MU_NQF0028")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Measure_Name_2014, "CMS138/NQF 0028: Preventive Care and Screening: Tobacco Use: Screening and Cessation Intervention")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, Col_MeasureID_2014, "NQF0028")
        Dim crangeNQF0028 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0028_2014, COL_Quality_CAT, ROW_NQF0028_2014 + 2, COL_Quality_CAT)

        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        ' crangeNQF0028.StyleNew.Clear()
        crangeNQF0028.Data = CType(gloMU.My.Resources.CrossCutting, Bitmap)
        crangeNQF0028.StyleNew.BackColor = Color.FromArgb(199, 211, 235)
        C1QualityMeasures.Rows(ROW_NQF0028_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0028_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0028_2014 + 2).AllowMerging = True

        'Add Population 2 
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 3, COL_CoreMeasure_2014, Space(3) & "Population 2: All patients aged 18 years and older seen for at least two visits or at least one preventive visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_CoreMeasure_2014, Space(5) & "Numerator 2: Patients who received tobacco cessation intervention")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_SP_Name_2014, "MU_NQF0028")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Measure_Name_2014, "CMS138/NQF 0028: Preventive Care and Screening: Tobacco Use: Screening and Cessation Intervention")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, Col_MeasureID_2014, "NQF0028")

        'Add Population 3
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 5, COL_CoreMeasure_2014, Space(3) & "Population 3: All patients aged 18 years and older seen for at least two visits or at least one preventive visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_CoreMeasure_2014, Space(5) & "Numerator 3: Patients who were screened for tobacco use at least once within 24 months AND who received tobacco cessation intervention if identified as a tobacco user")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_SP_Name_2014, "MU_NQF0028")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Measure_Name_2014, "CMS138/NQF 0028: Preventive Care and Screening: Tobacco Use: Screening and Cessation Intervention")
        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, Col_MeasureID_2014, "NQF0028")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0028_2014), C1QualityMeasures.Rows(ROW_NQF0028_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0028_2014 + 2), C1QualityMeasures.Rows(ROW_NQF0028_2014 + 3), C1QualityMeasures.Rows(ROW_NQF0028_2014 + 4), C1QualityMeasures.Rows(ROW_NQF0028_2014 + 5), C1QualityMeasures.Rows(ROW_NQF0028_2014 + 6)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ''NQF0031
        'C1QualityMeasures.SetCellCheck(ROW_NQF0031_2014, COL_Check_2014, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        'C1QualityMeasures.Rows(ROW_NQF0031_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        'C1QualityMeasures.SetData(ROW_NQF0031_2014, COL_Domain_2014, "Clinical Process/ Effectiveness")
        'C1QualityMeasures.SetData(ROW_NQF0031_2014, COL_CoreMeasure_2014, "NQF 0031: Breast Cancer Screening")
        'C1QualityMeasures.SetData(ROW_NQF0031_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Women 41–69 years of age with a visit during the measurement period")
        'C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Women with one or more mammograms during the measurement period or the year prior to the measurement period") 'Problem 00000272 : typo in CQM Dashboard 
        'C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_SP_Name_2014, "MU_NQF0031")
        'C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Measure_Name_2014, "NQF 0031, Population 1: Women 41–69 years of age with a visit during the measurement period") 'Problem 00000272 : typo in CQM Dashboard

        'Aniket: Do not show this measure as we are not certifying them
        C1QualityMeasures.Rows(ROW_NQF0031_2014).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0031_2014 + 1).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0031_2014 + 2).Visible = False

        'NQF0032
        C1QualityMeasures.SetCellCheck(ROW_NQF0032_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS124"))
        C1QualityMeasures.Rows(ROW_NQF0032_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0032_2014, COL_Domain_2014, "(General Practice Adult) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0032_2014, COL_CoreMeasure_2014, "CMS124/NQF 0032: Cervical Cancer Screening")
        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Women 23-64 years of age with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Women with one or more screenings for cervical cancer. Appropriate screenings are defined by any one of the following criteria:- Cervical cytology performed during the measurement period or the two years prior to the measurement period for women who are at least 21 years old at the time of the test- Cervical cytology/human papillomavirus (HPV) co-testing performed during the measurement period or the four years prior to the measurement period for women who are at least 30 years old at the time of the test") 'Problem 00000272 : typo in CQM Dashboard 
        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_SP_Name_2014, "MU_NQF0032")
        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Measure_Name_2014, "CMS124/NQF 0032: Cervical Cancer Screening")
        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, Col_MeasureID_2014, "NQF0032")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0032_2014), C1QualityMeasures.Rows(ROW_NQF0032_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0032_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0033
        C1QualityMeasures.SetCellCheck(ROW_NQF0033_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS153"))
        C1QualityMeasures.Rows(ROW_NQF0033_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0033_2014, COL_Domain_2014, "(Core) Population/ Public Health")
        C1QualityMeasures.SetData(ROW_NQF0033_2014, COL_CoreMeasure_2014, "CMS153/NQF 0033: Chlamydia Screening for Women")
        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Women 16 to 24 years of age who are sexually active and who had a visit in the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Women with at least one chlamydia test during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_SP_Name_2014, "MU_NQF0033")
        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Measure_Name_2014, "CMS153/NQF 0033: Chlamydia Screening for Women")
        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, Col_MeasureID_2014, "NQF0033")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0033_2014), C1QualityMeasures.Rows(ROW_NQF0033_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0033_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0034
        C1QualityMeasures.SetCellCheck(ROW_NQF0034_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS130"))
        C1QualityMeasures.Rows(ROW_NQF0034_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0034_2014, COL_Domain_2014, "(General Practice Adult) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0034_2014, COL_CoreMeasure_2014, "CMS130/NQF 0034: Colorectal Cancer Screening")
        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 50-75 years of age with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients with one or more screenings for colorectal cancer. Appropriate screenings are defined by any one of the following criteria below: - Fecal occult blood test (FOBT) during the measurement period - Flexible sigmoidoscopy during the measurement period or the four years prior to the measurement period - Colonoscopy during the measurement period or the nine years prior to the measurement period - FIT-DNA during the measurement period or the two years prior to the measurement period  - CT Colonography during the measurement period or the four years prior to the measurement period") 'Problem 00000272 : typo in CQM Dashboard 
        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_SP_Name_2014, "MU_NQF0034")
        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Measure_Name_2014, "CMS130/NQF 0034: Colorectal Cancer Screening") 'Problem 00000272 : typo in CQM Dashboard
        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, Col_MeasureID_2014, "NQF0034") 'Problem 00000272 : typo in CQM Dashboard

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0034_2014), C1QualityMeasures.Rows(ROW_NQF0034_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0034_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0038
        C1QualityMeasures.SetCellCheck(ROW_NQF0038_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS117"))
        C1QualityMeasures.Rows(ROW_NQF0038_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0038_2014, COL_Domain_2014, "(Core) Population/ Public Health")
        C1QualityMeasures.SetData(ROW_NQF0038_2014, COL_CoreMeasure_2014, "CMS117/NQF 0038: Childhood Immunization Status")
        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Children who turn 2 years of age during the measurement period and who have a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Children who have evidence showing they received recommended vaccines, had documented history of the illness, had a seropositive test result, or had an allergic reaction to the vaccine by their second birthday")
        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_SP_Name_2014, "MU_NQF0038")
        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Measure_Name_2014, "CMS117/NQF 0038: Childhood Immunization Status")
        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, Col_MeasureID_2014, "NQF0038")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0038_2014), C1QualityMeasures.Rows(ROW_NQF0038_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0038_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing


        'NQF0041
        C1QualityMeasures.SetCellCheck(ROW_NQF0041_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS147"))
        C1QualityMeasures.Rows(ROW_NQF0041_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0041_2014, COL_Domain_2014, "(General Practice Pediatric) Population/ Public Health")
        C1QualityMeasures.SetData(ROW_NQF0041_2014, COL_CoreMeasure_2014, "CMS147/NQF 0041: Preventive Care and Screening: Influenza Immunization")
        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: All patients aged 6 months and older seen for a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients who received an influenza immunization OR who reported previous receipt of an influenza immunization") 'Problem 00000272 : typo in CQM Dashboard 
        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_SP_Name_2014, "MU_NQF0041")
        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Measure_Name_2014, "CMS147/NQF 0041: Preventive Care and Screening: Influenza Immunization") 'Problem 00000272 : typo in CQM Dashboard
        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, Col_MeasureID_2014, "NQF0041")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0041_2014), C1QualityMeasures.Rows(ROW_NQF0041_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0041_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0043
        C1QualityMeasures.SetCellCheck(ROW_NQF0043_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS127"))
        C1QualityMeasures.Rows(ROW_NQF0043_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0043_2014, COL_Domain_2014, "(General Practice Adult) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0043_2014, COL_CoreMeasure_2014, "CMS127/NQF 0043: Pneumonia Vaccination Status for Older Adults")
        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 65 years of age and older with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients who have ever received a pneumococcal vaccination")
        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_SP_Name_2014, "MU_NQF0043_MU2")
        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Measure_Name_2014, "CMS127/NQF 0043: Pneumonia Vaccination Status for Older Adults")
        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, Col_MeasureID_2014, "NQF0043")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0043_2014), C1QualityMeasures.Rows(ROW_NQF0043_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0043_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0052
        C1QualityMeasures.SetCellCheck(ROW_NQF0052_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS166"))
        C1QualityMeasures.Rows(ROW_NQF0052_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0052_2014, COL_Domain_2014, "(Core) Efficient Use of Healthcare Resources")
        C1QualityMeasures.SetData(ROW_NQF0052_2014, COL_CoreMeasure_2014, "CMS166/NQF 0052: Use of Imaging Studies for Low Back Pain")
        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18-50 years of age with a diagnosis of Uncomplicated low back pain during a visit")
        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients without an imaging study conducted on the index episode start date or in the 28 days following the index episode start date")
        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_SP_Name_2014, "MU_NQF0052_MU2")
        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Measure_Name_2014, "CMS166/NQF 0052: Use of Imaging Studies for Low Back Pain")
        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, Col_MeasureID_2014, "NQF0052")
        Dim crangeNQF0052 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0052_2014, COL_Quality_CAT, ROW_NQF0052_2014 + 2, COL_Quality_CAT)

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0052_2014), C1QualityMeasures.Rows(ROW_NQF0052_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0052_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing


        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        'crangeNQF0052.StyleNew.Clear()
        crangeNQF0052.Data = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)
        crangeNQF0052.StyleNew.BackColor = Color.FromArgb(199, 211, 235)
        ' crangeNQF0052.StyleNew.BackColor = Color.FromArgb(253, 234, 196)
        C1QualityMeasures.Rows(ROW_NQF0052_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0052_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0052_2014 + 2).AllowMerging = True

        'NQF0055
        C1QualityMeasures.SetCellCheck(ROW_NQF0055_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS131"))
        C1QualityMeasures.Rows(ROW_NQF0055_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0055_2014, COL_Domain_2014, "(Eye) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0055_2014, COL_CoreMeasure_2014, "CMS131/NQF 0055: Diabetes: Eye Exam")
        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18-75 years of age with diabetes with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients with an eye screening for diabetic retinal disease. This includes diabetics who had one of the following: A retinal or dilated eye exam by an eye care professional in the measurement period or a negative retinal exam (no evidence of retinopathy) by an eye care professional in the year prior to the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_SP_Name_2014, "MU_NQF0055")
        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Measure_Name_2014, "CMS131/NQF 0055: Diabetes: Eye Exam")
        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, Col_MeasureID_2014, "NQF0055")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0055_2014), C1QualityMeasures.Rows(ROW_NQF0055_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0055_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0056
        C1QualityMeasures.SetCellCheck(ROW_NQF0056_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS123"))
        C1QualityMeasures.Rows(ROW_NQF0056_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0056_2014, COL_Domain_2014, "(Diabetes) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0056_2014, COL_CoreMeasure_2014, "CMS123/NQF 0056: Diabetes: Foot Exam")
        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18-75 years of age with diabetes with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients who received visual, pulse and sensory foot examinations during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_SP_Name_2014, "MU_NQF0056")
        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Measure_Name_2014, "CMS123/NQF 0056: Diabetes: Foot Exam")
        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, Col_MeasureID_2014, "NQF0056")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0056_2014), C1QualityMeasures.Rows(ROW_NQF0056_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0056_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0059
        C1QualityMeasures.SetCellCheck(ROW_NQF0059_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS122"))
        C1QualityMeasures.Rows(ROW_NQF0059_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0059_2014, COL_Domain_2014, "(Diabetes) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0059_2014, COL_CoreMeasure_2014, "CMS122/NQF 0059: Diabetes: Hemoglobin A1c (HbA1c) Poor Control (> 9%)")
        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18-75 years of age with diabetes with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients whose most recent HbA1c level (performed during the measurement period) is >9.0%")
        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_SP_Name_2014, "MU_NQF0059")
        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Measure_Name_2014, "CMS122/NQF 0059: Diabetes: Hemoglobin A1c (HbA1c) Poor Control (> 9%)")
        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, Col_MeasureID_2014, "NQF0059")
        Dim crangeNQF0059 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0059_2014, COL_Quality_CAT, ROW_NQF0059_2014 + 2, COL_Quality_CAT)

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0059_2014), C1QualityMeasures.Rows(ROW_NQF0059_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0059_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing


        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        'crangeNQF0059.StyleNew.Clear()
        crangeNQF0059.Data = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)
        crangeNQF0059.StyleNew.BackColor = Color.FromArgb(199, 211, 235)
        ' crangeNQF0059.StyleNew.BackColor = Color.FromArgb(253, 234, 196)

        C1QualityMeasures.Rows(ROW_NQF0059_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0059_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0059_2014 + 2).AllowMerging = True

        'NQF0062
        C1QualityMeasures.SetCellCheck(ROW_NQF0062_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS134"))
        C1QualityMeasures.Rows(ROW_NQF0062_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0062_2014, COL_Domain_2014, "(Diabetes) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0062_2014, COL_CoreMeasure_2014, "CMS134/NQF 0062: Diabetes: Medical Attention for Nephropathy ")
        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18-75 years of age with diabetes with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients with a screening for nephropathy or evidence of nephropathy during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_SP_Name_2014, "MU_NQF0062")
        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Measure_Name_2014, "CMS134/NQF 0062: Diabetes: Urine Protein Screening")
        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, Col_MeasureID_2014, "NQF0062")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0062_2014), C1QualityMeasures.Rows(ROW_NQF0062_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0062_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ''NQF0064
        'C1QualityMeasures.SetCellCheck(ROW_NQF0064_2014, COL_Check_2014, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        'C1QualityMeasures.Rows(ROW_NQF0064_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        'C1QualityMeasures.SetData(ROW_NQF0064_2014, COL_Domain_2014, "(Diabetes) Clinical Process/ Effectiveness")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014, COL_CoreMeasure_2014, "CMS163: Diabetes: Diabetes: Low Density Lipoprotein (LDL) Management")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18-75 years of age with diabetes with a visit during the measurement period")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator 1: Patients whose most recent LDL-C level performed during the measurement period is <100 mg/dL")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_SP_Name_2014, "MU_NQF0064_Numerator1")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Measure_Name_2014, "NQF 0064, Population 1, Numerator 1: Had LDL‐C lab test")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 3, COL_CoreMeasure_2014, Space(5) & "Numerator 2: Had LDL‐C <100mg/dL")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 3, COL_SP_Name_2014, "MU_NQF0064_Numerator2")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 3, COL_Measure_Name_2014, "CMS163: Diabetes: Diabetes: Low Density Lipoprotein (LDL) Management")
        'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, Col_MeasureID_2014, "NQF0064")
        'CMS 163 removed 
        C1QualityMeasures.Rows(ROW_NQF0064_2014).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0064_2014 + 1).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0064_2014 + 2).Visible = False

        'NQF0068
        C1QualityMeasures.SetCellCheck(ROW_NQF0068_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS164"))
        C1QualityMeasures.Rows(ROW_NQF0068_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0068_2014, COL_Domain_2014, "(Heart) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF0068_2014, COL_CoreMeasure_2014, "CMS164/NQF 0068: Ischemic Vascular Disease (IVD): Use of Aspirin or Another Antiplatelet")
        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18 years of age and older with a visit during the measurement period who had an AMI, CABG, or PCI during the 12 months prior to the measurement year or who had a diagnosis of IVD overlapping the measurement year ")
        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients who had an active medication of aspirin or another antiplatelet during the measurement year")
        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_SP_Name_2014, "MU_NQF0068")
        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Measure_Name_2014, "CMS164/NQF 0068: Ischemic Vascular Disease (IVD): Use of Aspirin or another Antithrombotic")
        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, Col_MeasureID_2014, "NQF0068")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0068_2014), C1QualityMeasures.Rows(ROW_NQF0068_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0068_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0101
        C1QualityMeasures.SetCellCheck(ROW_NQF0101_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS139"))
        C1QualityMeasures.Rows(ROW_NQF0101_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0101_2014, COL_Domain_2014, "(General Practice Adult) Patient Safety")
        C1QualityMeasures.SetData(ROW_NQF0101_2014, COL_CoreMeasure_2014, "CMS139/NQF 0101: Falls: Screening for Future Fall Risk")
        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients aged 65 years and older with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients who were screened for future fall risk at least once within the measurement period")
        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_SP_Name_2014, "MU_NQF0101")
        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Measure_Name_2014, "CMS139/NQF 0101: Falls: Screening for Future Fall Risk")
        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, Col_MeasureID_2014, "NQF0101")
        Dim crangeNQF0101 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0101_2014, COL_Quality_CAT, ROW_NQF0101_2014 + 2, COL_Quality_CAT)

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0101_2014), C1QualityMeasures.Rows(ROW_NQF0101_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0101_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        '  crangeNQF0101.StyleNew.Clear()
        crangeNQF0101.Data = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)
        crangeNQF0101.StyleNew.BackColor = Color.FromArgb(199, 211, 235)
        'crangeNQF0101.StyleNew.BackColor = Color.FromArgb(253, 234, 196)

        C1QualityMeasures.Rows(ROW_NQF0101_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0101_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0101_2014 + 2).AllowMerging = True


        'NQF0418
        C1QualityMeasures.SetCellCheck(ROW_NQF0418_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS2"))
        C1QualityMeasures.Rows(ROW_NQF0418_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0418_2014, COL_Domain_2014, "(Core) Population/ Public Health")
        C1QualityMeasures.SetData(ROW_NQF0418_2014, COL_CoreMeasure_2014, "CMS2/NQF 0418: Preventive Care and Screening: Screening for Clinical Depression and Follow-Up Plan")
        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: All patients aged 12 years and older before the beginning of the measurement period with at least one eligible encounter during the measurement period.")
        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients screened for depression on the date of the encounter  using an age appropriate standardized tool AND if positive, a follow-up plan is documented on the date of the positive screen")
        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_SP_Name_2014, "MU_NQF0418")
        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Measure_Name_2014, "CMS2/NQF 0418: Preventive Care and Screening: Screening for Clinical Depression and Follow-Up Plan")
        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, Col_MeasureID_2014, "NQF0418")

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0418_2014), C1QualityMeasures.Rows(ROW_NQF0418_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0418_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        'NQF0419
        C1QualityMeasures.SetCellCheck(ROW_NQF0419_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS68"))
        C1QualityMeasures.Rows(ROW_NQF0419_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0419_2014, COL_Domain_2014, "(Core) Patient Safety")
        C1QualityMeasures.SetData(ROW_NQF0419_2014, COL_CoreMeasure_2014, "CMS68/NQF 0419: Documentation of Current Medications in the Medical Record")
        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: All visits occurring during the 12 month measurement period for patients aged 18 years and older.")
        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Eligible professional or eligible clinician attests to documenting, updating or reviewing the patient's current medications using all immediate resources available on the date of the encounter. This list must include ALL known prescriptions, over-the-counters, herbals and vitamin/mineral/dietary (nutritional) supplements AND must contain the medications' name, dosages, frequency and route of administration")
        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_SP_Name_2014, "MU_NQF0419")
        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Measure_Name_2014, "CMS68/NQF 0419: Documentation of Current Medications in the Medical Record")
        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, Col_MeasureID_2014, "NQF0419")
        Dim crangeNQF0419 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0419_2014, COL_Quality_CAT, ROW_NQF0419_2014 + 2, COL_Quality_CAT)

        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0419_2014), C1QualityMeasures.Rows(ROW_NQF0419_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0419_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        '  crangeNQF0419.StyleNew.Clear()
        crangeNQF0419.Data = CType(gloMU.My.Resources.HighPriority__CrossCutting, Bitmap)
        crangeNQF0419.StyleNew.BackColor = Color.FromArgb(199, 211, 235)
        'crangeNQF0101.StyleNew.BackColor = Color.FromArgb(253, 234, 196)

        C1QualityMeasures.Rows(ROW_NQF0419_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0419_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0419_2014 + 2).AllowMerging = True


        'NQF0421
        C1QualityMeasures.SetCellCheck(ROW_NQF0421_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS69"))
        C1QualityMeasures.Rows(ROW_NQF0421_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF0421_2014, COL_Domain_2014, "(Core) Population/ Public Health")
        C1QualityMeasures.SetData(ROW_NQF0421_2014, COL_CoreMeasure_2014, "CMS69/NQF 0421: Preventive Care and Screening: Body Mass Index (BMI) Screening and Follow-Up Plan")


        'Population 1
        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: All patients 18 and older on the date of the encounter with at least one eligible encounter during the measurement period.")
        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator 1: Patients with a documented BMI during the encounter or during the previous twelve months, AND when the BMI is outside of normal parameters, a follow-up plan is documented during the encounter or during the previous twelve months of the current encounter.")
        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Measure_Name_2014, "CMS69/NQF 0421: Preventive Care and Screening: Body Mass Index (BMI) Screening and Follow-Up Plan")
        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, Col_MeasureID_2014, "NQF0421")

        'Population 2
        'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 3, COL_CoreMeasure_2014, Space(3) & "Population 2: All patients 65 years of age and older on the date of the encounter with at least one eligible encounter during the measurement period.")
        'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_CoreMeasure_2014, Space(5) & "Numerator 2: Patients with a documented BMI during the encounter or during the previous six months, AND when the BMI is outside of normal parameters, a follow-up plan is documented during the encounter or during the previous six months of the current encounter.")

        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_SP_Name_2014, "MU_NQF0421")
        'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Measure_Name_2014, "CMS69/NQF 0421: Preventive Care and Screening: Body Mass Index (BMI) Screening and Follow-Up Plan")
        'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, Col_MeasureID_2014, "NQF0421")

        Dim crangeNQF0421 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF0421_2014, COL_Quality_CAT, ROW_NQF0421_2014 + 4, COL_Quality_CAT)
        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF0421_2014), C1QualityMeasures.Rows(ROW_NQF0421_2014 + 1), C1QualityMeasures.Rows(ROW_NQF0421_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing


        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        '  crangeNQF0421.StyleNew.Clear()
        crangeNQF0421.Data = CType(gloMU.My.Resources.CrossCutting, Bitmap)
        crangeNQF0421.StyleNew.BackColor = Color.FromArgb(199, 211, 235)

        ' crangeNQF0421.StyleNew.BackColor = Color.FromArgb(253, 234, 196)
        C1QualityMeasures.Rows(ROW_NQF0421_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0421_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0421_2014 + 2).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0421_2014 + 3).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF0421_2014 + 4).AllowMerging = True


        'CMSID22
        C1QualityMeasures.SetCellCheck(ROW_NQF_CMSID22_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS22"))
        C1QualityMeasures.Rows(ROW_NQF_CMSID22_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014, COL_Domain_2014, "(General Practice Adult) Population/ Public Health")
        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014, COL_CoreMeasure_2014, "CMS22: Preventive Care and Screening: Screening for High Blood Pressure and Follow-Up Documented")
        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: All patients aged 18 years and older before the start of the measurement period with at least one eligible encounter during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients who were screened for high blood pressure AND have a recommended follow-up plan documented, as indicated if the blood pressure is pre-hypertensive or hypertensive")
        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_SP_Name_2014, "MU_CMSID22_MU2")
        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Measure_Name_2014, "CMS22: Preventive Care and Screening: Screening for High Blood Pressure and Follow-Up Documented")
        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, Col_MeasureID_2014, "CMSID22")
        Dim crangeCMSID22 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF_CMSID22_2014, COL_Quality_CAT, ROW_NQF_CMSID22_2014 + 2, COL_Quality_CAT)
        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF_CMSID22_2014), C1QualityMeasures.Rows(ROW_NQF_CMSID22_2014 + 1), C1QualityMeasures.Rows(ROW_NQF_CMSID22_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        ' crangeCMSID22.StyleNew.Clear()
        crangeCMSID22.Data = CType(gloMU.My.Resources.CrossCutting, Bitmap)

        crangeCMSID22.StyleNew.BackColor = Color.FromArgb(199, 211, 235)


        C1QualityMeasures.Rows(ROW_NQF_CMSID22_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF_CMSID22_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF_CMSID22_2014 + 2).AllowMerging = True


        'CMSID90
        C1QualityMeasures.SetCellCheck(ROW_NQF_CMSID90_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS90"))
        C1QualityMeasures.Rows(ROW_NQF_CMSID90_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014, COL_Domain_2014, "(Core) Patient and Family Engagement")
        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014, COL_CoreMeasure_2014, "CMS90: Functional Status Assessments for Congestive Heart Failure")
        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18 years of age and older who had two outpatient encounters during the measurement year and a diagnosis of congestive heart failure.")
        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients with patient-reported functional status assessment results (eg, VR-12; VR-36; MLHF-Q; KCCQ; PROMIS-10 Global Health, PROMIS-29) present in the EHR two weeks before or during the initial FSA encounter and results for the follow-up FSA at least 30 days but no more than 180 days after the initial functional status assessment")
        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_SP_Name_2014, "MU_CMSID90_MU2")
        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Measure_Name_2014, "CMS90: Functional Status Assessment for Complex Chronic Conditions")
        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, Col_MeasureID_2014, "CMSID90")
        Dim crangeCMSID90 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF_CMSID90_2014, COL_Quality_CAT, ROW_NQF_CMSID90_2014 + 2, COL_Quality_CAT)
        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF_CMSID90_2014), C1QualityMeasures.Rows(ROW_NQF_CMSID90_2014 + 1), C1QualityMeasures.Rows(ROW_NQF_CMSID90_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        ' crangeCMSID90.StyleNew.Clear()
        crangeCMSID90.Data = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)
        crangeCMSID90.StyleNew.BackColor = Color.FromArgb(199, 211, 235)

        C1QualityMeasures.Rows(ROW_NQF_CMSID90_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF_CMSID90_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF_CMSID90_2014 + 2).AllowMerging = True

        'CMSID56
        C1QualityMeasures.SetCellCheck(ROW_NQF_CMSID56_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS56"))
        C1QualityMeasures.Rows(ROW_NQF_CMSID56_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014, COL_Domain_2014, "(Orthopedics) Patient and Family Engagement")
        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014, COL_CoreMeasure_2014, "CMS56:  Functional Status Assessment for Total Hip Replacement")
        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18 years of age and older who had a primary total hip arthroplasty (THA) in the year prior to the measurement period and who had an outpatient encounter during the measurement period.")
        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients with patient-reported functional status assessment (i.e., VR-12, PROMIS-10-Global Health, HOOS) in the 90 days prior to or on the day of primary THA procedure, and 270 - 365 days after THA procedure.")
        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_SP_Name_2014, "MU_CMSID56_MU2")
        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Measure_Name_2014, "CMS56:  Functional Status Assessment for Hip Replacement")
        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, Col_MeasureID_2014, "CMSID56")
        Dim crangeCMSID56 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF_CMSID56_2014, COL_Quality_CAT, ROW_NQF_CMSID56_2014 + 2, COL_Quality_CAT)
        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF_CMSID56_2014), C1QualityMeasures.Rows(ROW_NQF_CMSID56_2014 + 1), C1QualityMeasures.Rows(ROW_NQF_CMSID56_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        ' crangeCMSID56.StyleNew.Clear()
        crangeCMSID56.Data = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)
        crangeCMSID56.StyleNew.BackColor = Color.FromArgb(199, 211, 235)

        C1QualityMeasures.Rows(ROW_NQF_CMSID56_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF_CMSID56_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF_CMSID56_2014 + 2).AllowMerging = True

        'CMSID66
        C1QualityMeasures.SetCellCheck(ROW_NQF_CMSID66_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS66"))
        C1QualityMeasures.Rows(ROW_NQF_CMSID66_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014, COL_Domain_2014, "(Orthopedics) Patient and Family Engagement")
        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014, COL_CoreMeasure_2014, "CMS66: Functional Status Assessment for Total Knee Replacement")
        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Patients 18 years of age and older who had a primary total knee arthroplasty (TKA) in the year prior to the measurement period and who had an outpatient encounter during the measurement period.")
        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Patients with patient-reported functional status assessment results (i.e., VR-12, PROMIS-10 Global Health, KOOS) in the 90 days prior to or on the day of primary TKA procedure, and 270 - 365 days after TKA procedure")
        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_SP_Name_2014, "MU_CMSID66_MU2")
        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Measure_Name_2014, "CMS66: Functional Status Assessment for Knee Replacement")
        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, Col_MeasureID_2014, "CMSID66")
        Dim crangeNQF_CMSID66 As C1.Win.C1FlexGrid.CellRange = C1QualityMeasures.GetCellRange(ROW_NQF_CMSID66_2014, COL_Quality_CAT, ROW_NQF_CMSID66_2014 + 2, COL_Quality_CAT)
        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF_CMSID66_2014), C1QualityMeasures.Rows(ROW_NQF_CMSID66_2014 + 1), C1QualityMeasures.Rows(ROW_NQF_CMSID66_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        ' crange2.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        ' crangeNQF_CMSID66.StyleNew.Clear()
        crangeNQF_CMSID66.Data = CType(gloMU.My.Resources.HighPriorityNew, Bitmap)

        crangeNQF_CMSID66.StyleNew.BackColor = Color.FromArgb(199, 211, 235)
        C1QualityMeasures.Rows(ROW_NQF_CMSID66_2014).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF_CMSID66_2014 + 1).AllowMerging = True
        C1QualityMeasures.Rows(ROW_NQF_CMSID66_2014 + 2).AllowMerging = True


        'CMSID125
        C1QualityMeasures.SetCellCheck(ROW_NQF_CMSID125_2014, COL_Check_2014, Me.GetCheckMark(dQCheckPointValue, PatientID, "CMS125"))
        C1QualityMeasures.Rows(ROW_NQF_CMSID125_2014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014, COL_Domain_2014, "(General Practice Adult) Clinical Process/ Effectiveness")
        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014, COL_CoreMeasure_2014, "CMS125: Breast Cancer Screening")
        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 1, COL_CoreMeasure_2014, Space(3) & "Population 1: Women 51-74 years of age with a visit during the measurement period")
        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_CoreMeasure_2014, Space(5) & "Numerator: Women with one or more mammograms during the measurement period or the 15 months prior to the measurement period")
        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_SP_Name_2014, "MU_CMSID125_MU2")
        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Measure_Name_2014, "CMS125: Breast Cancer Screening")
        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, Col_MeasureID_2014, "CMSID125")
        rowVisibility = New RowVisibility(C1QualityMeasures, New C1.Win.C1FlexGrid.Row() {C1QualityMeasures.Rows(ROW_NQF_CMSID125_2014), C1QualityMeasures.Rows(ROW_NQF_CMSID125_2014 + 1), C1QualityMeasures.Rows(ROW_NQF_CMSID125_2014 + 2)})
        lstRowVisibility.Add(rowVisibility)
        rowVisibility = Nothing

        If dQCheckPointValue IsNot Nothing Then
            dQCheckPointValue.Clear()
            dQCheckPointValue = Nothing
        End If

    End Sub

    Private Sub FillGrid()
        Try

            Dim dt As DataTable = New DataTable()

            C1QualityMeasures.Clear()
            C1QualityMeasures.Styles.Clear()
            C1QualityMeasures.Rows.Count = 1

            SetGridStyle()


            C1QualityMeasures.Rows.Add(180)

            Dim i As Integer

            For i = 1 To 180
                C1QualityMeasures.Rows(i).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            Next

            C1QualityMeasures.Rows(ROW_CoreMeasure).AllowMerging = True



            C1QualityMeasures.SetCellCheck(ROW_NQF0421, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.SetData(ROW_NQF0421, COL_CoreMeasure, "NQF 0421 - Adult Weight Screening and Follow-Up")
            C1QualityMeasures.Rows(ROW_NQF0421).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))


            C1QualityMeasures.SetData(ROW_NQF0421 + 1, COL_CoreMeasure, Space(3) & "Population 1: Age  65 years or older ")
            C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_CoreMeasure, Space(5) & "Numerator 1:  BMI calculated and a follow-up plan if BMI outside parameters")
            C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_SP_Name, "MU_NQF0421")
            C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Measure_Name, "NQF 0421, Population 1, Numerator 1: BMI calculated and a follow-up plan if BMI outside parameters")
            C1QualityMeasures.Rows(ROW_NQF0421 + 1).AllowEditing = False
            C1QualityMeasures.Rows(ROW_NQF0421 + 2).AllowEditing = False

            C1QualityMeasures.SetData(ROW_NQF0421 + 3, COL_CoreMeasure, Space(3) & "Population 2: Age 18 years to 64 years")
            C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_CoreMeasure, Space(5) & "Numerator 2:  BMI calculated and a follow-up plan if BMI outside parameters")
            C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_SP_Name, "MU_NQF0421_POP2")
            C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Measure_Name, "NQF 0421, Population 2, Numerator 2: BMI calculated and a follow-up plan if BMI outside parameters ")
            C1QualityMeasures.Rows(ROW_NQF0421 + 3).AllowEditing = False
            C1QualityMeasures.Rows(ROW_NQF0421 + 4).AllowEditing = False

            C1QualityMeasures.SetCellCheck(ROW_NQF0013, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0013).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0013, COL_CoreMeasure, "NQF 0013 - Hypertension: Blood Pressure Measurement")
            C1QualityMeasures.SetData(ROW_NQF0013 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years or older with a diagnosis of hypertension")
            C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Patients with BP (Systolic and Diastolic) recorded.")
            C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_SP_Name, "MU_NQF0013")
            C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Measure_Name, "NQF 0013,Population 1, Numerator: Patients with BP (Systolic and Diastolic) recorded")

            C1QualityMeasures.SetCellCheck(ROW_NQF0028a, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0028a).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0028a, COL_CoreMeasure, "NQF 0028a: Preventive Care and Screening Measure Pair: a.Tobacco Use Assessment")
            C1QualityMeasures.SetData(ROW_NQF0028a + 1, COL_CoreMeasure, Space(3) & "Population 1: Age 18 years or older")
            C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_CoreMeasure, Space(5) & "Numerator: Patients who were queried about tobacco use")
            C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_SP_Name, "MU_NQF0028a")
            C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Measure_Name, "NQF 0028a, Population 1,Numerator: Patients who were queried about tobacco use")


            C1QualityMeasures.SetCellCheck(ROW_NQF0028b, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0028b).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0028b, COL_CoreMeasure, "NQF 0028b: Preventive Care and Screening Measure Pair: b.Tobacco Cessation Intervention")
            C1QualityMeasures.SetData(ROW_NQF0028b + 1, COL_CoreMeasure, Space(3) & "Population 1: Tobacco users age 18 years or older")
            C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_CoreMeasure, Space(5) & "Numerator: Patients who received cessation intervention.")
            C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_SP_Name, "MU_NQF0028b")
            C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Measure_Name, "NQF 0028b, Population 1, Numerator: Patients who received cessation intervention")

            C1QualityMeasures.SetCellCheck(ROW_NQF0041, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0041).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0041, COL_CoreMeasure, "NQF 0041: Preventive Care and Screening: Influenza Immunization for Patients ≥ 50 Years Old")
            C1QualityMeasures.SetData(ROW_NQF0041 + 1, COL_CoreMeasure, Space(3) & "Population 1: Age 50 years or older")
            C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Patients who received influenza vaccination.")
            C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_SP_Name, "MU_NQF0041")
            C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Measure_Name, "NQF 0041, Population 1,Numerator: Patients who received influenza vaccination")

            C1QualityMeasures.SetCellCheck(ROW_NQF0024, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0024).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0024, COL_CoreMeasure, "NQF 0024: Weight Assessment and Counseling for Children and Adolescents")
            C1QualityMeasures.SetData(ROW_NQF0024 + 1, COL_CoreMeasure, Space(3) & "Population 1: Age 2 years to 16 years with an encounter and not pregnant")
            C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients with a BMI calculation.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_SP_Name, "MU_NQF0024_POP1#1")
            C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_Measure_Name, "NQF 0024, Population 1, Numerator 1: Patients with a BMI calculation")
            C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received nutrition counseling.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_SP_Name, "MU_NQF0024_POP1#2")
            C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_Measure_Name, "NQF 0024, Population 1, Numerator 2: Patients who received nutrition counseling")
            C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received counseling for physical activity.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_SP_Name, "MU_NQF0024_POP1#3")
            C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_Measure_Name, "NQF 0024, Population 1, Numerator 3: Patients who received counseling for physical activity")

            C1QualityMeasures.SetData(ROW_NQF0024 + 5, COL_CoreMeasure, Space(3) & "Population 2: Age 2 years to 10 years with an encounter and not pregnant")
            C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients with a BMI calculation.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_SP_Name, "MU_NQF0024_POP2#1")
            C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_Measure_Name, "NQF 0024, Population 2, Numerator 1: Patients with a BMI calculation")
            C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received nutrition counseling.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_SP_Name, "MU_NQF0024_POP2#2")
            C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_Measure_Name, "NQF 0024, Population 2, Numerator 2: Patients who received nutrition counseling")
            C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received counseling for physical activity.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_SP_Name, "MU_NQF0024_POP2#3")
            C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_Measure_Name, "NQF 0024, Population 2, Numerator 3: Patients who received counseling for physical activity")

            C1QualityMeasures.SetData(ROW_NQF0024 + 9, COL_CoreMeasure, Space(3) & "Population 3: Age 11 years to 16 years with an encounter and not pregnant")
            C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients with a BMI calculation.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_SP_Name, "MU_NQF0024_POP3#1")
            C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_Measure_Name, "NQF 0024, Population 3, Numerator 1: Patients with a BMI calculation")
            C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received nutrition counseling.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_SP_Name, "MU_NQF0024_POP3#2")
            C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_Measure_Name, "NQF 0024, Population 3, Numerator 2: Patients who received nutrition counseling")
            C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received counseling for physical activity.")
            C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_SP_Name, "MU_NQF0024_POP3#3")
            C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_Measure_Name, "NQF 0024, Population 3, Numerator 3: Patients who received counseling for physical activity")
            ''''''''

            C1QualityMeasures.SetCellCheck(ROW_NQF0038, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0038).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0038, COL_CoreMeasure, "NQF 0038: Childhood immunization Status")
            C1QualityMeasures.SetData(ROW_NQF0038 + 1, COL_CoreMeasure, Space(3) & "Population 1: Patients who reach age 2 within reporting period.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 2, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients who received 4 DTaP Vaccines.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 2, COL_SP_Name, "MU_NQF0038#1")
            C1QualityMeasures.SetData(ROW_NQF0038 + 2, COL_Measure_Name, "NQF 0038, Population 1, Numerator 1: Patients who received 4 DTaP Vaccines")
            C1QualityMeasures.SetData(ROW_NQF0038 + 3, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received 3 IPV Vaccines.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 3, COL_SP_Name, "MU_NQF0038#2")
            C1QualityMeasures.SetData(ROW_NQF0038 + 3, COL_Measure_Name, "NQF 0038, Population 1, Numerator 2: Patients who received 3 IPV Vaccines")
            C1QualityMeasures.SetData(ROW_NQF0038 + 4, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received 1 MMR Vaccine.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 4, COL_SP_Name, "MU_NQF0038#3")
            C1QualityMeasures.SetData(ROW_NQF0038 + 4, COL_Measure_Name, "NQF 0038, Population 1, Numerator 3: Patients who received 1 MMR Vaccine")
            C1QualityMeasures.SetData(ROW_NQF0038 + 5, COL_CoreMeasure, Space(5) & "Numerator 4:  Patients who received 2 HiB Vaccines.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 5, COL_SP_Name, "MU_NQF0038#4")
            C1QualityMeasures.SetData(ROW_NQF0038 + 5, COL_Measure_Name, "NQF 0038, Population 1, Numerator 4: Patients who received 2 HiB Vaccines")
            C1QualityMeasures.SetData(ROW_NQF0038 + 6, COL_CoreMeasure, Space(5) & "Numerator 5:  Patients who received 3 Hep B Vaccines.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 6, COL_SP_Name, "MU_NQF0038#5")
            C1QualityMeasures.SetData(ROW_NQF0038 + 6, COL_Measure_Name, "NQF 0038, Population 1, Numerator 5: Patients who received 3 Hep B Vaccines")
            C1QualityMeasures.SetData(ROW_NQF0038 + 7, COL_CoreMeasure, Space(5) & "Numerator 6:  Patients who received 1 VZV Vaccine.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 7, COL_SP_Name, "MU_NQF0038#6")
            C1QualityMeasures.SetData(ROW_NQF0038 + 7, COL_Measure_Name, "NQF 0038, Population 1, Numerator 6:  Patients who received 1 VZV Vaccine")
            C1QualityMeasures.SetData(ROW_NQF0038 + 8, COL_CoreMeasure, Space(5) & "Numerator 7:  Patients who received 4 PCV Vaccines.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 8, COL_SP_Name, "MU_NQF0038#7")
            C1QualityMeasures.SetData(ROW_NQF0038 + 8, COL_Measure_Name, "NQF 0038, Population 1, Numerator 7: Patients who received 4 PCV Vaccines")
            C1QualityMeasures.SetData(ROW_NQF0038 + 9, COL_CoreMeasure, Space(5) & "Numerator 8:  Patients who received 2 Hep A Vaccines.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 9, COL_SP_Name, "MU_NQF0038#8")
            C1QualityMeasures.SetData(ROW_NQF0038 + 9, COL_Measure_Name, "NQF 0038, Population 1, Numerator 8: Patients who received 2 Hep A Vaccines")
            C1QualityMeasures.SetData(ROW_NQF0038 + 10, COL_CoreMeasure, Space(5) & "Numerator 9:  Patients who received 2 RV Vaccines.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 10, COL_SP_Name, "MU_NQF0038#9")
            C1QualityMeasures.SetData(ROW_NQF0038 + 10, COL_Measure_Name, "NQF 0038, Population 1, Numerator 9: Patients who received 2 RV Vaccines")
            C1QualityMeasures.SetData(ROW_NQF0038 + 11, COL_CoreMeasure, Space(5) & "Numerator 10:  Patients who received 2 flu Vaccines.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 11, COL_SP_Name, "MU_NQF0038#10")
            C1QualityMeasures.SetData(ROW_NQF0038 + 11, COL_Measure_Name, "NQF 0038, Population 1, Numerator 10: Patients who received 2 flu Vaccines")
            C1QualityMeasures.SetData(ROW_NQF0038 + 12, COL_CoreMeasure, Space(5) & "Numerator 11:  Patients who received Vaccine set 1.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 12, COL_SP_Name, "MU_NQF0038#11")
            C1QualityMeasures.SetData(ROW_NQF0038 + 12, COL_Measure_Name, "NQF 0038, Population 1, Numerator 11: Patients who received Vaccine set 1")
            C1QualityMeasures.SetData(ROW_NQF0038 + 13, COL_CoreMeasure, Space(5) & "Numerator 12:  Patients who received Vaccine set 2.")
            C1QualityMeasures.SetData(ROW_NQF0038 + 13, COL_SP_Name, "MU_NQF0038#12")
            C1QualityMeasures.SetData(ROW_NQF0038 + 13, COL_Measure_Name, "NQF 0038, Population 1, Numerator 12: Patients who received Vaccine set 2")

            C1QualityMeasures.SetCellCheck(ROW_NQF0001, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0001).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0001, COL_CoreMeasure, "NQF 0001: Asthma Assessment")
            C1QualityMeasures.SetData(ROW_NQF0001 + 1, COL_CoreMeasure, Space(3) & "Population 1: Age 5 years to 50 years with diagnosis of asthma")
            C1QualityMeasures.SetData(ROW_NQF0001 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Daytime and nocturnal asthma symptoms assessed")

            C1QualityMeasures.SetCellCheck(ROW_NQF0002, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0002).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0002, COL_CoreMeasure, "NQF 0002: Appropriate Testing for Children with Pharyngitis")
            C1QualityMeasures.SetData(ROW_NQF0002 + 1, COL_CoreMeasure, Space(3) & "Population 1: Age 2 years to 18 years diagnosed with Pharyngitis and dispensed an antibiotic")
            C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Patients who received a group A streptococcus (strep) test for the episode.")
            C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_SP_Name, "MU_NQF0002")
            C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Measure_Name, "NQF 0002, Population 1, Numerator:  Patients who received a group A streptococcus (strep) test for the episode")

            C1QualityMeasures.SetCellCheck(ROW_NQF0004, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0004).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0004, COL_CoreMeasure, "NQF 0004: Initiation and Engagement of Alcohol and Other Drug Dependence Treatment: (a) Initiation, (b) Engagement")
            C1QualityMeasures.SetData(ROW_NQF0004 + 1, COL_CoreMeasure, Space(3) & "Population 1: Age 12 years to 16 years with a new episode of AOD dependence.")
            C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_CoreMeasure, Space(5) & "Numerator 1:  Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_SP_Name, "MU_NQF004_Pop1Num1#1")
            C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Measure_Name, "NQF 004, Population 1, Numerator 1: Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_CoreMeasure, Space(5) & "Numerator 2:  Two additional services with AOD diagnosis within 30 days")
            C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_SP_Name, "MU_NQF004_Pop1Num1#2")
            C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Measure_Name, "NQF 004, Population 1, Numerator 2: Two additional services with AOD diagnosis within 30 days")

            C1QualityMeasures.SetData(ROW_NQF0004 + 4, COL_CoreMeasure, Space(3) & "Population 2: Age 17 years or older with a new episode of AOD dependence.")
            C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_CoreMeasure, Space(5) & "Numerator 1:  Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_SP_Name, "MU_NQF004_Pop2Num1#1")
            C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Measure_Name, "NQF 004, Population 2, Numerator 1: Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_CoreMeasure, Space(5) & "Numerator 2:  Two additional services with an AOD Diagnosis within 30 days")
            C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_SP_Name, "MU_NQF004_Pop2Num1#2")
            C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Measure_Name, "NQF 004, Population 2, Numerator 2: Two additional services with an AOD Diagnosis within 30 days")

            C1QualityMeasures.SetData(ROW_NQF0004 + 7, COL_CoreMeasure, Space(3) & "Population 3: Age 12 years or older with a new episode of AOD dependence.")
            C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_CoreMeasure, Space(5) & "Numerator 1:  Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_SP_Name, "MU_NQF004_Pop3Num1#1")
            C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Measure_Name, "NQF 004, Population 3, Numerator 1: Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_CoreMeasure, Space(5) & "Numerator 2:  Two additional services with an AOD Diagnosis within 30 days")
            C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_SP_Name, "MU_NQF004_Pop3Num1#2")
            C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Measure_Name, "NQF 004, Population 3, Numerator 2: Two additional services with an AOD Diagnosis within 30 days")

            C1QualityMeasures.SetCellCheck(ROW_NQF0012, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0012).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0012, COL_CoreMeasure, "NQF 0012: Prenatal Care:  Screening for Human Immunodeficiency Virus (HIV)")
            C1QualityMeasures.SetData(ROW_NQF0012 + 1, COL_CoreMeasure, Space(3) & "Population 1: Delivered Live Birth and had Prenatal Visit")
            C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_CoreMeasure, Space(5) & "Numerator: HIV Screening Performed")
            C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_SP_Name, "MU_NQF0012")
            C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Measure_Name, "NQF 0012, Population 1, Numerator: HIV Screening Performed")

            C1QualityMeasures.SetCellCheck(ROW_NQF0014, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0014).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0014, COL_CoreMeasure, "NQF 0014: Prenatal Care:  Anti-D Immune Globulin")
            C1QualityMeasures.SetData(ROW_NQF0014 + 1, COL_CoreMeasure, Space(3) & "Population 1:   D (Rh) negative, unsensitized patients who delivered live birth")
            C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_CoreMeasure, Space(5) & "Numerator: Received anti-D immune globulin at 26-30 weeks gestation")
            C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_SP_Name, "MU_NQF0014")
1:          C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Measure_Name, "NQF 0014, Population 1, Numerator: Received anti-D immune globulin at 26-30 weeks gestation")


            C1QualityMeasures.SetCellCheck(ROW_NQF0031, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0031).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0031, COL_CoreMeasure, "NQF 0031:Breast Cancer Screening")
            C1QualityMeasures.SetData(ROW_NQF0031 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Women 40–69 years of age")
            C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Breast cancer screen performed")
            C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_SP_Name, "MU_NQF0031")
            C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Measure_Name, "NQF 0031, Population 1, Numerator: Breast cancer screen performed")

            C1QualityMeasures.SetCellCheck(ROW_NQF0032, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0032).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0032, COL_CoreMeasure, "NQF 0032:Cervical Cancer Screening")
            C1QualityMeasures.SetData(ROW_NQF0032 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Women 21-64 years of age")
            C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Pap test performed") 'Problem 00000272 : typo in CQM Dashboard 
            C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_SP_Name, "MU_NQF0032")
            C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Measure_Name, "NQF 0032, Population 1, Numerator: Pap test performed") 'Problem 00000272 : typo in CQM Dashboard

            C1QualityMeasures.SetCellCheck(ROW_NQF0033, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0033).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0033, COL_CoreMeasure, "NQF 0033:Chlamydia Screening for Women")
            C1QualityMeasures.SetData(ROW_NQF0033 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Women 15‐24 years of age identified as sexually active")
            C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Chlamydia screening performed")
            C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_SP_Name, "MU_NQF0033_POP1")
            C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Measure_Name, "NQF 0033, Population 1, Numerator: Chlamydia screening performed")
            C1QualityMeasures.SetData(ROW_NQF0033 + 3, COL_CoreMeasure, Space(3) & "Population 2:  Women 15‐20 years of age identified as sexually active")
            C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_CoreMeasure, Space(5) & "Numerator:  Chlamydia screening performed")
            C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_SP_Name, "MU_NQF0033_POP2")
            C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Measure_Name, "NQF 0033, Population 2, Numerator: Chlamydia screening performed")
            C1QualityMeasures.SetData(ROW_NQF0033 + 5, COL_CoreMeasure, Space(3) & "Population 3:  Women 21‐24 years of age identified as sexually active")
            C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_CoreMeasure, Space(5) & "Numerator:  Chlamydia screening performed")
            C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_SP_Name, "MU_NQF0033_POP3")
            C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Measure_Name, "NQF 0033, Population 3, Numerator: Chlamydia screening performed")


            C1QualityMeasures.SetCellCheck(ROW_NQF0055, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0055).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0055, COL_CoreMeasure, "NQF 0055:Diabetes: Eye Exam")
            C1QualityMeasures.SetData(ROW_NQF0055 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Had a retinal or dilated eye exam")
            C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_SP_Name, "MU_NQF0055")
            C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Measure_Name, "NQF 0055, Population 1, Numerator: Had a retinal or dilated eye exam")

            C1QualityMeasures.SetCellCheck(ROW_NQF0056, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0056).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0056, COL_CoreMeasure, "NQF 0056:Diabetes: Foot Exam")
            C1QualityMeasures.SetData(ROW_NQF0056 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Had a foot exam")
            C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_SP_Name, "MU_NQF0056")
            C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Measure_Name, "NQF 0056, Population 1, Numerator: Had a foot exam")

            C1QualityMeasures.SetCellCheck(ROW_NQF0059, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0059).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0059, COL_CoreMeasure, "NQF 0059:Diabetes:  HbA1c Poor Control")
            C1QualityMeasures.SetData(ROW_NQF0059 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Had HbA1c >9.0%.")
            C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_SP_Name, "MU_NQF0059")
            C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Measure_Name, "NQF 0059, Population 1, Numerator: Had HbA1c >9.0%")

            C1QualityMeasures.SetCellCheck(ROW_NQF0061, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0061).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0061, COL_CoreMeasure, "NQF 0061:Diabetes: Blood Pressure Management")
            C1QualityMeasures.SetData(ROW_NQF0061 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Had Blood Pressure <140/90 mmHg.")
            C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_SP_Name, "MU_NQF0061")
            C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Measure_Name, "NQF 0061, Population 1, Numerator: Had Blood Pressure <140/90 mmHg")

            C1QualityMeasures.SetCellCheck(ROW_NQF0062, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0062).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0062, COL_CoreMeasure, "NQF 0062:Diabetes: Urine Screening")
            C1QualityMeasures.SetData(ROW_NQF0062 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Had a nephropathy screening test or evidence of nephropathy")
            C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_SP_Name, "MU_NQF0062")
            C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Measure_Name, "NQF 0062, Population 1, Numerator: Had a nephropathy screening test or evidence of nephropathy")

            C1QualityMeasures.SetCellCheck(ROW_NQF0064, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0064).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0064, COL_CoreMeasure, "NQF 0064:Diabetes: LDL Management & Control")
            C1QualityMeasures.SetData(ROW_NQF0064 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_CoreMeasure, Space(5) & "Numerator 1:  Had LDL‐C lab test")
            C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_SP_Name, "MU_NQF0064_Numerator1")
            C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Measure_Name, "NQF 0064, Population 1, Numerator 1: Had LDL‐C lab test")
            C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_CoreMeasure, Space(5) & "Numerator 2:  Had LDL‐C <100mg/dL")
            C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_SP_Name, "MU_NQF0064_Numerator2")
            C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Measure_Name, "NQF 0064, Population 1, Numerator 2: Had LDL‐C <100mg/dL")

            C1QualityMeasures.SetCellCheck(ROW_NQF0067, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0067).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0067, COL_CoreMeasure, "NQF 0067:Coronary Artery Disease (CAD): Oral Antiplatelet Therapy Prescribed for Patients with CAD")
            C1QualityMeasures.SetData(ROW_NQF0067 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of CAD")
            C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed oral antiplatelet therapy")
            C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_SP_Name, "MU_NQF0067")
            C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Measure_Name, "NQF 0067, Population 1, Numerator: Prescribed oral antiplatelet therapy")

            C1QualityMeasures.SetCellCheck(ROW_NQF0068, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0068).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0068, COL_CoreMeasure, "NQF 0068:Ischemic Vascular Disease (IVD): Use of Aspirin or another Antithrombotic")
            C1QualityMeasures.SetData(ROW_NQF0068 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with AMI, CABG, or PTCA in prev year or IVD in current year")
            C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Use of aspirin or another antithrombotic")
            C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_SP_Name, "MU_NQF0068")
            C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Measure_Name, "NQF 0068, Population 1, Numerator: Use of aspirin or another antithrombotic")

            C1QualityMeasures.SetCellCheck(ROW_NQF0070, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0070).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0070, COL_CoreMeasure, "NQF 0070:Coronary Artery Disease (CAD): Beta-Blocker Therapy for CAD Patients with Prior Myocardial Infarction (MI)")
            C1QualityMeasures.SetData(ROW_NQF0070 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with diagnosis of CAD and prior MI")
            C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed beta‐blocker therapy")
            C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_SP_Name, "MU_NQF0070")
            C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Measure_Name, "NQF 0070, Population 1, Numerator: Prescribed beta‐blocker therapy")


            C1QualityMeasures.SetCellCheck(ROW_NQF0074, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0074).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0074, COL_CoreMeasure, "NQF 0074:Coronary Artery Disease (CAD): Drug Therapy for Lowering LDL-Cholesterol")
            C1QualityMeasures.SetData(ROW_NQF0074 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with diagnosis of CAD")
            C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed a lipid‐lowering therapy")
            C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_SP_Name, "MU_NQF0074")
            C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Measure_Name, "NQF 0074, Population 1, Numerator: Prescribed a lipid‐lowering therapy")

            C1QualityMeasures.SetCellCheck(ROW_NQF0075, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0075).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0075, COL_CoreMeasure, "NQF 0075:Ischemic Vascular Disease (IVD): Complete Lipid Panel and LDL Control")
            C1QualityMeasures.SetData(ROW_NQF0075 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with AMI, CABG, or PTCA in prev year or IVD in current year")
            C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_CoreMeasure, Space(5) & "Numerator 1:  Had LDL‐C lab test")
            C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_SP_Name, "MU_NQF0075_Numerator1")
            C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Measure_Name, "NQF 0075, Numerator 1: Had LDL‐C lab test")
            C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_CoreMeasure, Space(5) & "Numerator 2:  Had LDL‐C <100mg/dL")
            C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_SP_Name, "MU_NQF0075_Numerator2")
            C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Measure_Name, "NQF 0075, Population 1, Numerator 2: Had LDL‐C <100mg/dL")

            C1QualityMeasures.SetCellCheck(ROW_NQF0081, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0081).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0081, COL_CoreMeasure, "NQF 0081:Heart Failure (HF) : Angiotensin-Converting Enzyme (ACE) Inhibitor or Angiotensin Receptor Blocker (ARB) Therapy for Left Ventricular Systolic Dysfunction ")
            C1QualityMeasures.SetData(ROW_NQF0081 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of heart failure and LVSD (LVEF < 40%)")
            C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed ACE inhibitor or ARB therapy")
            C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_SP_Name, "MU_NQF0081")
            C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Measure_Name, "NQF 0081, Population 1, Numerator: Prescribed ACE inhibitor or ARB therapy")

            C1QualityMeasures.SetCellCheck(ROW_NQF0083, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0083).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0083, COL_CoreMeasure, "NQF 0083:Heart Failure (HF): Beta-Blocker Therapy for Left Ventricular Systolic Dysfunction (LVSD)")
            C1QualityMeasures.SetData(ROW_NQF0083 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of heart failure and LVSD (LVEF < 40%)")
            C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed beta‐blocker therapy")
            C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_SP_Name, "MU_NQF0083")
            C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Measure_Name, "NQF 0083, Population 1, Numerator: Prescribed beta‐blocker therapy")

            C1QualityMeasures.SetCellCheck(ROW_NQF0084, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0084).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0084, COL_CoreMeasure, "NQF 0084:Heart Failure (HF) : Warfarin Therapy Patients with Atrial Fibrillation")
            C1QualityMeasures.SetData(ROW_NQF0084 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of heart failure and paroxysmal or chronic atrial fibrillation")
            C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed warfarin therapy")
            C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_SP_Name, "MU_NQF0084")
            C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Measure_Name, "NQF 0084,Population 1, Numerator: Prescribed warfarin therapy")



            C1QualityMeasures.SetCellCheck(ROW_NQF0575, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(ROW_NQF0575).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(ROW_NQF0575, COL_CoreMeasure, "NQF 0575:Diabetes: HbA1c Control (<8%)")
            C1QualityMeasures.SetData(ROW_NQF0575 + 1, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_CoreMeasure, Space(5) & "Numerator:  Had HbA1c <8.0%")
            C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_SP_Name, "MU_NQF0575")
            '' C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_SP_Name, "NQF 0575, Population 1, Numerator: Had HbA1c <8.0%")
            C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Measure_Name, "NQF 0575, Population 1, Numerator: Had HbA1c <8.0%")

            Dim rg As C1.Win.C1FlexGrid.CellRange
            rg = C1QualityMeasures.GetCellRange(ROW_NQF0028a + 2, COL_Button, ROW_NQF0028a + 2, COL_Button)
            rg.StyleNew.ComboList = "..."
            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always


            C1QualityMeasures.Cols(COL_Check).AllowMerging = True
            C1QualityMeasures.Cols(COL_CoreMeasure).AllowMerging = True

            C1QualityMeasures.Cols(COL_Denomenator).AllowMerging = True
            C1QualityMeasures.Cols(COL_Numeratot).AllowMerging = True
            C1QualityMeasures.Cols(COL_Denomenator).AllowMerging = True
            C1QualityMeasures.Cols(COL_Percent).AllowMerging = True
            C1QualityMeasures.Cols(COL_Exclusion).AllowMerging = True





            C1QualityMeasures.Rows(ROW_CoreMeasure).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
            C1QualityMeasures.Rows(ROW_CoreMeasure).StyleNew.ForeColor = Color.White
            C1QualityMeasures.Rows(ROW_CoreMeasure).StyleNew.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            C1QualityMeasures.Rows(ROW_CoreMeasure).AllowEditing = False

            C1QualityMeasures.Rows(ROW_AlternateCOreMeaseure).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
            C1QualityMeasures.Rows(ROW_AlternateCOreMeaseure).StyleNew.ForeColor = Color.White
            C1QualityMeasures.Rows(ROW_AlternateCOreMeaseure).StyleNew.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            C1QualityMeasures.Rows(ROW_AlternateCOreMeaseure).AllowEditing = False

            C1QualityMeasures.Rows(ROW_MenuSetMeaseure).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
            C1QualityMeasures.Rows(ROW_MenuSetMeaseure).StyleNew.ForeColor = Color.White
            C1QualityMeasures.Rows(ROW_MenuSetMeaseure).StyleNew.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            C1QualityMeasures.Rows(ROW_MenuSetMeaseure).AllowEditing = False

            rg = C1QualityMeasures.GetCellRange(ROW_CoreMeasure, COL_Check, ROW_CoreMeasure, COL_Percent)
            C1QualityMeasures.Rows(ROW_CoreMeasure).AllowMerging = True
            C1QualityMeasures.SetData(ROW_CoreMeasure, COL_CoreMeasure, "Core Measures")
            'C1QualityMeasures.SetData(ROW_CoreMeasure, COL_Check, "Core Measures")
            C1QualityMeasures.Rows(ROW_AlternateCOreMeaseure).AllowMerging = True
            C1QualityMeasures.SetData(ROW_AlternateCOreMeaseure, COL_CoreMeasure, "Alternate Core Measures")
            C1QualityMeasures.Rows(ROW_MenuSetMeaseure).AllowMerging = True
            C1QualityMeasures.SetData(ROW_MenuSetMeaseure, COL_CoreMeasure, "Additional Measures")


            ''commenetd by Mayuri:20101028-As we are using snomed code insted of history item list
            'Dim rg1 As C1.Win.C1FlexGrid.CellRange
            'rg1 = C1QualityMeasures.GetCellRange(ROW_NQF0028b + 2, COL_Button, ROW_NQF0028b + 2, COL_Button)
            'rg1.StyleNew.ComboList = "..."
            'C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

            HideRows()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HideRows()

        ''''NQF 0001
        C1QualityMeasures.Rows(ROW_NQF0001).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0001 + 1).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0001 + 2).Visible = False

        ''''NQF 0002
        'C1QualityMeasures.Rows(ROW_NQF0002).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0002 + 1).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0002 + 2).Visible = False

        ''''NQF 0014
        'C1QualityMeasures.Rows(ROW_NQF0014).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0014 + 1).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0014 + 2).Visible = False

        ''''NQF 0018
        C1QualityMeasures.Rows(ROW_NQF0018).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0018 + 1).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0018 + 2).Visible = False

        ''''NQF 0027
        C1QualityMeasures.Rows(ROW_NQF0027).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0027 + 1).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0027 + 2).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0027 + 3).Visible = False

        ''''NQF 0031
        'C1QualityMeasures.Rows(ROW_NQF0031).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0031 + 1).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0031 + 2).Visible = False

        'C1QualityMeasures.Rows(ROW_NQF0033).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0033 + 1).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0033 + 2).Visible = False

        'C1QualityMeasures.Rows(ROW_NQF0033 + 3).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0033 + 4).Visible = False
        'C1QualityMeasures.Rows(ROW_NQF0033 + 5).Visible = False

        'C1QualityMeasures.Rows(ROW_NQF0033 + 6).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0034).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0034 + 1).Visible = False
        C1QualityMeasures.Rows(ROW_NQF0034 + 2).Visible = False
        ''''NQF 0036 to 0073
        Dim i As Integer
        'For i = ROW_NQF0036 To ROW_NQF0074 + 2
        For i = ROW_NQF0036 To ROW_NQF0052 + 2
            C1QualityMeasures.Rows(i).Visible = False
        Next




        For i = ROW_NQF0073 To ROW_NQF0073 + 2
            C1QualityMeasures.Rows(i).Visible = False
        Next

        For i = ROW_NQF0086 To ROW_NQF0389 + 2
            C1QualityMeasures.Rows(i).Visible = False
        Next

    End Sub

    Private Sub tlbbtn_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_SelectAll.Click

        SelectAll()

    End Sub

    Private Sub SetColours()

        Dim blnIsPatientInCQM As Boolean

        If PatientID <> 0 Then
            Dim blnHideNQF0041 As Boolean = False

            chkCQM2014.Enabled = False
            Dim styRed As C1.Win.C1FlexGrid.CellStyle
            styRed = C1QualityMeasures.Styles.Add("NotSatisfying")
            styRed.BackColor = Color.Tomato
            styRed.ForeColor = Color.White


            Dim styGreen As C1.Win.C1FlexGrid.CellStyle
            styGreen = C1QualityMeasures.Styles.Add("Satisfying")
            styGreen.BackColor = Color.Green
            styGreen.ForeColor = Color.White

            For intCQMCount As Integer = 0 To C1QualityMeasures.Rows.Count - 1

                Dim bytRowCount As Integer

                'NQF0421 which has multiple population
                If intCQMCount = ROW_NQF0421_2014 + 4 Then '65 Then
                    bytRowCount = 5
                Else
                    bytRowCount = 3
                End If


                'Satisfying
                If IsNumeric(C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014)) = True AndAlso IsNumeric(C1QualityMeasures.GetData(intCQMCount, COL_Numeratot_2014)) = True AndAlso C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014) >= 1 AndAlso C1QualityMeasures.GetData(intCQMCount, COL_Numeratot_2014) >= 1 Then
                    C1QualityMeasures.SetCellStyle(intCQMCount, COL_Numeratot_2014, "Satisfying")

                    blnIsPatientInCQM = True
                    If C1QualityMeasures.Rows(intCQMCount).UserData IsNot Nothing AndAlso TypeOf (C1QualityMeasures.Rows(intCQMCount).UserData) Is RowVisibility Then
                        DirectCast(C1QualityMeasures.Rows(intCQMCount).UserData, RowVisibility).TurnOnVisibility()
                    End If
                    'NotSatisfying
                ElseIf IsNumeric(C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014)) = True AndAlso IsNumeric(C1QualityMeasures.GetData(intCQMCount, COL_Numeratot_2014)) = True AndAlso C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014) >= 1 AndAlso C1QualityMeasures.GetData(intCQMCount, COL_Numeratot_2014) = 0 Then
                    C1QualityMeasures.SetCellStyle(intCQMCount, COL_Numeratot_2014, "NotSatisfying")

                    blnIsPatientInCQM = True
                    If C1QualityMeasures.Rows(intCQMCount).UserData IsNot Nothing AndAlso TypeOf (C1QualityMeasures.Rows(intCQMCount).UserData) Is RowVisibility Then
                        DirectCast(C1QualityMeasures.Rows(intCQMCount).UserData, RowVisibility).TurnOnVisibility()
                    End If

                    'Hide the measures for which the denominator count is 0
                ElseIf IsNumeric(C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014)) = True AndAlso C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014) = 0 Then

                    If intCQMCount = ROW_NQF0421_2014 + 4 Then '65 Then
                        If IsNumeric(C1QualityMeasures.GetData(intCQMCount - 2, COL_Denomenator_2014)) = True AndAlso C1QualityMeasures.GetData(intCQMCount - 2, COL_Denomenator_2014) <> 0 Then
                            blnIsPatientInCQM = True

                            If C1QualityMeasures.Rows(intCQMCount).UserData IsNot Nothing AndAlso TypeOf (C1QualityMeasures.Rows(intCQMCount).UserData) Is RowVisibility Then
                                DirectCast(C1QualityMeasures.Rows(intCQMCount).UserData, RowVisibility).TurnOnVisibility()
                            End If
                        Else
                            If IsNumeric(C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014)) = True AndAlso C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014) = 0 AndAlso C1QualityMeasures.GetData(intCQMCount - 2, COL_Denomenator_2014) = 0 Then
                                If C1QualityMeasures.Rows(intCQMCount).UserData IsNot Nothing AndAlso TypeOf (C1QualityMeasures.Rows(intCQMCount).UserData) Is RowVisibility Then
                                    DirectCast(C1QualityMeasures.Rows(intCQMCount).UserData, RowVisibility).TurnOffVisibility()
                                End If
                            End If
                        End If
                    End If

                    '63 65
                    If intCQMCount <> ROW_NQF0421_2014 + 2 AndAlso intCQMCount <> ROW_NQF0421_2014 + 4 Then '65 Then
                        If C1QualityMeasures.Rows(intCQMCount).UserData IsNot Nothing AndAlso TypeOf (C1QualityMeasures.Rows(intCQMCount).UserData) Is RowVisibility Then
                            DirectCast(C1QualityMeasures.Rows(intCQMCount).UserData, RowVisibility).TurnOffVisibility()
                        End If
                    End If

                ElseIf IsNumeric(C1QualityMeasures.GetData(intCQMCount, COL_Denomenator_2014)) = False Then
                    If C1QualityMeasures.Rows(intCQMCount).UserData IsNot Nothing AndAlso TypeOf (C1QualityMeasures.Rows(intCQMCount).UserData) Is RowVisibility Then
                        DirectCast(C1QualityMeasures.Rows(intCQMCount).UserData, RowVisibility).TurnOffVisibility()
                    End If
                Else
                    C1QualityMeasures.SetCellStyle(intCQMCount, COL_Numeratot_2014, "")
                    C1QualityMeasures.SetCellStyle(intCQMCount, COL_Numeratot_2014, "")

                End If

            Next
            styRed = Nothing
            styGreen = Nothing

            If blnIsPatientInCQM = False Then
                MsgBox("This patient provider combination does not satisfy for any CQM.", MsgBoxStyle.Information)
            End If

        End If

    End Sub

    Private Sub SelectAll()
        Dim checkval As C1.Win.C1FlexGrid.CheckEnum

        For intCQMCount As Integer = 0 To C1QualityMeasures.Rows.Count - 1

            If C1QualityMeasures.Rows(intCQMCount).Visible = True Then

                checkval = C1QualityMeasures.GetCellCheck(intCQMCount, 0)
                If checkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                    C1QualityMeasures.SetCellCheck(intCQMCount, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                End If

            End If
        Next
    End Sub

    Private Sub tlbbtn_DeelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_DeelectAll.Click

        Dim checkval As C1.Win.C1FlexGrid.CheckEnum

        For intCQMCount As Integer = 0 To C1QualityMeasures.Rows.Count - 1

            If C1QualityMeasures.Rows(intCQMCount).Visible = True Then

                checkval = C1QualityMeasures.GetCellCheck(intCQMCount, 0)
                If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    C1QualityMeasures.SetCellCheck(intCQMCount, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                End If

            End If

        Next
    End Sub

    Private Sub DeleteCQMFilters()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            oDB.Execute("gsp_DELETE_CQMFilters")
            oDB.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

        Finally

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try


    End Sub

    Private Sub tlbbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_Close.Click
        DeleteCQMFilters()
        Me.Close()
    End Sub

    Private Function CalculatePercent(ByVal SPName As String, Optional ByVal HistoryItemList As String = "") As DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dt As New DataTable

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, dt)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            Dim FinalDenominator As Int64


            If dt.Rows.Count > 0 Then


                dt.Columns.Add()

                If Convert.ToString(dt.Rows(0)(2)) = "N/A" Then
                    FinalDenominator = Convert.ToInt64(dt.Rows(0)(0))
                Else
                    FinalDenominator = Convert.ToInt64(dt.Rows(0)(0)) - Convert.ToInt64(dt.Rows(0)(2))
                    FinalDenominator = Math.Abs(FinalDenominator)
                End If
                dt.Rows(0)(0) = FinalDenominator
                If dt.Rows(0)(0).ToString() = "0" Then
                    dt.Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
                    dt.Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If


            End If
            ''Sanjog -Added on 20101101 for insert Update log
            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing
            ''Sanjog -Added on 20101101 for insert Update log
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing

        Finally

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Private Function CalculatePercent_ds(ByVal SPName As String, Optional ByVal HistoryItemList As String = "", Optional ByVal CQM2014 As Boolean = False) As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim ds As New DataSet
            Dim dt As New DataTable
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, ds)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            Dim FinalDenominator As Int64
            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Denominator"
                ds.Tables(2).TableName = "Numerator"

            End If
            dt = ds.Tables("Count")
            If dt.Rows.Count > 0 Then


                dt.Columns.Add("CQMPercentage")

                If CQM2014 = False Then

                    If Convert.ToString(dt.Rows(0)(2)) = "N/A" Then
                        FinalDenominator = Convert.ToInt64(dt.Rows(0)(0))
                    Else
                        FinalDenominator = Convert.ToInt64(dt.Rows(0)(0)) - Convert.ToInt64(dt.Rows(0)(2))
                        FinalDenominator = Math.Abs(FinalDenominator)
                    End If
                    dt.Rows(0)(0) = FinalDenominator

                End If

                If dt.Rows(0)(0).ToString() = "0" Then
                    dt.Rows(0)("CQMPercentage") = "N/A"
                Else
                    Percent = Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
                    dt.Rows(0)("CQMPercentage") = FormatNumber(Percent, 2, TriState.True)

                End If


            End If
            ''Sanjog -Added on 20101101 for insert Update log
            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing
            ''Sanjog -Added on 20101101 for insert Update log
            Return ds


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing

        Finally

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Private Function CalculatePercent_ds_ORDAIII(ByVal SPName As String, Optional ByVal HistoryItemList As String = "", Optional ByVal CQM2014 As Boolean = False, Optional ByVal MultipleNumerator As Boolean = False, Optional ByVal ResultTVP1 As DataTable = Nothing, Optional ByVal ResultTVP2 As DataTable = Nothing, Optional PatientID As Int64 = 0) As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters
        Dim dtProviders As DataTable

        Try

            oDB.Connect(False)
            Dim ds As New DataSet
            Dim dt As New DataTable

            dtProviders = CType(cmbProviders.DataSource, DataTable).Copy

            If IsNothing(dtProviders.Columns("ProviderName")) = False Then
                dtProviders.Columns.Remove("ProviderName")
            End If

            If IsNothing(dtProviders.Columns("nProviderID")) = False Then
                dtProviders.Columns("nProviderID").ColumnName = "ProviderID"
            End If

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVP_Providers"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtProviders
            oParameters.Add(oParameter)
            oParameter = Nothing

         

            If Not IsNothing(ResultTVP1) Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ResultTVP1"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Structured
                oParameter.Value = ResultTVP1
                oParameters.Add(oParameter)
                oParameter = Nothing
            End If
            If Not IsNothing(ResultTVP2) Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ResultTVP2"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Structured
                oParameter.Value = ResultTVP2
                oParameters.Add(oParameter)
                oParameter = Nothing
            End If

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@PatientID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = PatientID
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, ds)

            oDB.Disconnect()
            Dim Percent As Single
            Dim FinalDenominator As Int64

            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Denominator"
                ds.Tables(2).TableName = "Numerator"
                ds.Tables(3).TableName = "QRDAIIICount"
                ds.Tables(4).TableName = "QRDAIIICodes"
                ds.Tables(5).TableName = "QRDAICodes"
            End If



            If ds.Tables.Count >= 8 Then
                If IsNothing(ds.Tables(6)) = False Then
                    ds.Tables(6).TableName = "DenominatorExclusionsPatientID"
                End If

                If IsNothing(ds.Tables(7)) = False Then
                    ds.Tables(7).TableName = "DenominatorExceptionsPatientID"
                End If
            End If

            If ds.Tables.Count = 10 Then
                If IsNothing(ds.Tables(8)) = False Then
                    ds.Tables(8).TableName = "DenoPatients"
                End If
                If IsNothing(ds.Tables(9)) = False Then
                    ds.Tables(9).TableName = "NumPatients"
                End If
            End If



            dt = ds.Tables("Count")
            If dt.Rows.Count > 0 Then


                dt.Columns.Add("CQMPercentage")

                If CQM2014 = False Then

                    If Convert.ToString(dt.Rows(0)(2)) = "N/A" Then
                        FinalDenominator = Convert.ToInt64(dt.Rows(0)(0))
                    Else
                        FinalDenominator = Convert.ToInt64(dt.Rows(0)(0)) - Convert.ToInt64(dt.Rows(0)(2))
                        FinalDenominator = Math.Abs(FinalDenominator)
                    End If
                    dt.Rows(0)(0) = FinalDenominator

                End If

                If MultipleNumerator = True Then

                    dt.Columns.Add("CQMPercentage2")

                    If dt.Rows(0)("Denominator").ToString() = "0" Then
                        dt.Rows(0)("CQMPercentage") = "N/A"
                        dt.Rows(0)("CQMPercentage2") = "N/A"
                    Else
                        If CQM2014 = True Then  ''denominator -exclusion
                            Percent = Single.Parse(dt.Rows(0)("Numerator1").ToString()) / Single.Parse(Convert.ToInt64(dt.Rows(0)("Denominator")) - Convert.ToInt64(dt.Rows(0)("Exception")) - Convert.ToInt64(dt.Rows(0)("Exclusion"))) * 100
                        Else
                            Percent = Single.Parse(dt.Rows(0)("Numerator1").ToString()) / Single.Parse(dt.Rows(0)("Denominator").ToString()) * 100

                        End If
                        dt.Rows(0)("CQMPercentage") = FormatNumber(Percent, 2, TriState.True)

                        If CQM2014 = True Then  ''denominator -exclusion

                            Percent = Single.Parse(dt.Rows(0)("Numerator2").ToString()) / Single.Parse(Convert.ToInt64(dt.Rows(0)("Denominator")) - Convert.ToInt64(dt.Rows(0)("Exception")) - Convert.ToInt64(dt.Rows(0)("Exclusion"))) * 100
                        Else
                            Percent = Single.Parse(dt.Rows(0)("Numerator2").ToString()) / Single.Parse(dt.Rows(0)("Denominator").ToString()) * 100
                        End If
                        dt.Rows(0)("CQMPercentage2") = FormatNumber(Percent, 2, TriState.True)
                    End If

                Else
                    Dim deno As Int64 = 0
                    deno = Convert.ToInt64(dt.Rows(0)("Denominator"))
                    If CQM2014 = True Then
                        deno = Convert.ToInt64(dt.Rows(0)("Denominator")) - Convert.ToInt64(dt.Rows(0)("Exception")) - Convert.ToInt64(dt.Rows(0)("Exclusion"))  ''denominator-exclusion
                    End If
                    If deno = 0 Then
                        dt.Rows(0)("CQMPercentage") = "N/A"
                    Else
                        Percent = Single.Parse(dt.Rows(0)("Numerator").ToString()) / Single.Parse(deno) * 100
                        dt.Rows(0)("CQMPercentage") = FormatNumber(Percent, 2, TriState.True)
                    End If
                End If



            End If

            Return ds
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

    Private Function CalculatePercent_0004(ByVal SPName As String, Optional ByVal HistoryItemList As String = "") As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, ds)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            Dim FinalDenominator As Int64

            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Count1"
                ds.Tables(2).TableName = "Denominator1"
                ds.Tables(3).TableName = "Numerator1"
                ds.Tables(4).TableName = "Denominator2"
                ds.Tables(5).TableName = "Numerator2"
            End If

            If ds.Tables(0).Rows.Count > 0 Then


                ds.Tables(0).Columns.Add()

                If Convert.ToString(ds.Tables(0).Rows(0)(2)) = "N/A" Then
                    FinalDenominator = Convert.ToInt64(ds.Tables(0).Rows(0)(0))
                Else
                    FinalDenominator = Convert.ToInt64(ds.Tables(0).Rows(0)(0)) - Convert.ToInt64(ds.Tables(0).Rows(0)(2))
                    FinalDenominator = Math.Abs(FinalDenominator)
                End If
                ds.Tables(0).Rows(0)(0) = FinalDenominator
                If ds.Tables(0).Rows(0)(0).ToString() = "0" Then
                    ds.Tables(0).Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(ds.Tables(0).Rows(0)(1).ToString()) / Single.Parse(ds.Tables(0).Rows(0)(0).ToString()) * 100
                    ds.Tables(0).Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If


            End If
            If ds.Tables(1).Rows.Count > 0 Then


                ds.Tables(1).Columns.Add()

                If Convert.ToString(ds.Tables(1).Rows(0)(2)) = "N/A" Then
                    FinalDenominator = Convert.ToInt64(ds.Tables(1).Rows(0)(0))
                Else
                    FinalDenominator = Convert.ToInt64(ds.Tables(1).Rows(0)(0)) - Convert.ToInt64(ds.Tables(1).Rows(0)(2))
                    FinalDenominator = Math.Abs(FinalDenominator)
                End If
                ds.Tables(1).Rows(0)(0) = FinalDenominator
                If ds.Tables(1).Rows(0)(0).ToString() = "0" Then
                    ds.Tables(1).Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(ds.Tables(1).Rows(0)(1).ToString()) / Single.Parse(ds.Tables(1).Rows(0)(0).ToString()) * 100
                    ds.Tables(1).Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If


            End If

            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing

            Return ds

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function
    ''function Added by Mayuri:20101028
#Region "RetriveDataFromMultipleServer"
    ''' <summary>
    ''' function to retirive data from servers like Rxnorm and snomed:-Added by Mayuri:20101028
    ''' </summary>
    ''' <param name="SPName"></param>
    ''' <param name="HistoryItemList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function RetriveDataFromMultipleServer(ByVal SPName As String, Optional ByVal HistoryItemList As String = "") As DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dt As New DataTable

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@Snomed_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBServerName
            ''gstrSMDBServerName()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@Snomed_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Retrive("" + SPName + "", oParameters, dt)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            Dim FinalDenominator As Int64
            If dt.Rows.Count > 0 Then
                dt.Columns.Add()
                If Convert.ToString(dt.Rows(0)(2)) = "N/A" Then
                    FinalDenominator = Convert.ToInt64(dt.Rows(0)(0))
                Else
                    FinalDenominator = Convert.ToInt64(dt.Rows(0)(0)) - Convert.ToInt64(dt.Rows(0)(2))
                    FinalDenominator = Math.Abs(FinalDenominator)
                End If
                dt.Rows(0)(0) = FinalDenominator
                If Convert.ToString(dt.Rows(0)(0)) = "0" Then
                    dt.Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
                    dt.Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)
                End If
            End If


            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

    Public Function RetriveData_NQF0038(ByVal SPName As String, ByVal _selectedvalue As String, Optional ByVal HistoryItemList As String = "") As DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dt As New DataTable

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = _selectedvalue
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DIB_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DIB_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, dt)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            Dim FinalDenominator As Int64
            If dt.Rows.Count > 0 Then
                dt.Columns.Add()
                For k = 0 To dt.Rows.Count - 1


                    If Convert.ToString(dt.Rows(k)(2)) = "N/A" Then
                        FinalDenominator = Convert.ToInt64(dt.Rows(k)(0))
                    Else
                        FinalDenominator = Convert.ToInt64(dt.Rows(k)(0)) - Convert.ToInt64(dt.Rows(k)(2))
                        FinalDenominator = Math.Abs(FinalDenominator)
                    End If
                    dt.Rows(k)(0) = FinalDenominator
                    If Convert.ToString(dt.Rows(k)(0)) = "0" Then
                        dt.Rows(k)(3) = "N/A"
                    Else
                        Percent = Single.Parse(dt.Rows(k)(1).ToString()) / Single.Parse(dt.Rows(k)(0).ToString()) * 100
                        dt.Rows(k)(3) = FormatNumber(Percent, 2, TriState.True)
                    End If
                Next
            End If
            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing
            ''Sanjog -Added on 20101101 for insert Update log
            Return dt


        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try


    End Function


    Public Function RetriveData_NQF0038_ds(ByVal SPName As String, ByVal _selectedvalue As String, Optional ByVal HistoryItemList As String = "") As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dt As New DataTable
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = _selectedvalue
            oParameters.Add(oParameter)
            oParameter = Nothing



            oDB.Retrive("" + SPName + "", oParameters, ds)
            ''
            oDB.Disconnect()


            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Denominator1"
                ds.Tables(2).TableName = "Numerator1"
                ds.Tables(3).TableName = "Denominator2"
                ds.Tables(4).TableName = "Numerator2"
                ds.Tables(5).TableName = "Denominator3"
                ds.Tables(6).TableName = "Numerator3"
                ds.Tables(7).TableName = "Denominator4"
                ds.Tables(8).TableName = "Numerator4"
                ds.Tables(9).TableName = "Denominator5"
                ds.Tables(10).TableName = "Numerator5"
                ds.Tables(11).TableName = "Denominator6"
                ds.Tables(12).TableName = "Numerator6"
                ds.Tables(13).TableName = "Denominator7"
                ds.Tables(14).TableName = "Numerator7"
                ds.Tables(15).TableName = "Denominator8"
                ds.Tables(16).TableName = "Numerator8"
                ds.Tables(17).TableName = "Denominator9"
                ds.Tables(18).TableName = "Numerator9"
                ds.Tables(19).TableName = "Denominator10"
                ds.Tables(20).TableName = "Numerator10"
                ds.Tables(21).TableName = "Denominator11"
                ds.Tables(22).TableName = "Numerator11"
                ds.Tables(23).TableName = "Denominator12"
                ds.Tables(24).TableName = "Numerator12"
            End If
            dt = ds.Tables("Count")
            Dim Percent As Single
            Dim FinalDenominator As Int64


            If dt.Rows.Count > 0 Then
                dt.Columns.Add()
                For k = 0 To dt.Rows.Count - 1


                    If Convert.ToString(dt.Rows(k)(2)) = "N/A" Then
                        FinalDenominator = Convert.ToInt64(dt.Rows(k)(0))
                    Else
                        FinalDenominator = Convert.ToInt64(dt.Rows(k)(0)) - Convert.ToInt64(dt.Rows(k)(2))
                        FinalDenominator = Math.Abs(FinalDenominator)
                    End If
                    dt.Rows(k)(0) = FinalDenominator
                    If Convert.ToString(dt.Rows(k)(0)) = "0" Then
                        dt.Rows(k)(3) = "N/A"
                    Else
                        Percent = Single.Parse(dt.Rows(k)(1).ToString()) / Single.Parse(dt.Rows(k)(0).ToString()) * 100
                        dt.Rows(k)(3) = FormatNumber(Percent, 2, TriState.True)
                    End If
                Next
            End If
            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing
            ''Sanjog -Added on 20101101 for insert Update log
            Return ds


        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

    Public Sub CallCQMFilterData()


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dtGender As New DataTable
            dtGender.Columns.Add("Gender")
            ''gender
            FillCQMFilterDatatoDT(cmbGender, dtGender, "gender")
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVP_CQMGenderFilter"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtGender
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Execute("gsp_CQMFilters", oParameters)
            ''
            oDB.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

        Finally

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Sub

    Public Sub FillCQMFilterDatatoDT(ByRef cmb As ComboBox, ByRef dtdata As DataTable, ByVal typedata As String)
        If cmb.Items.Count > 0 Then
            For i As Integer = 0 To cmb.Items.Count - 1
                dtdata.Rows.Add(cmb.Items(i))
            Next

        Else

            Select Case typedata
                Case "gender"
                    dtdata.Dispose()
                    dtdata = Nothing
                    dtdata = FillGender()
            End Select
        End If

    End Sub


    Public Function RetriveData_RxNormDIB_QRDAIII(ByVal SPName As String, Optional ByVal HistoryItemList As String = "", Optional ByVal CQM2014 As Boolean = False, Optional ByVal ResultTVP1 As DataTable = Nothing, Optional PatientID As Int64 = 0, Optional ByVal populationcount As Int16 = 1) As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters
        Dim dtProviders As DataTable

        Try

            oDB.Connect(False)

            Dim dt As New DataTable
            Dim ds As New DataSet

            dtProviders = CType(cmbProviders.DataSource, DataTable).Copy

            If IsNothing(dtProviders) = False Then
                If (dtProviders.Columns.Contains("ProviderName")) Then
                    dtProviders.Columns.Remove("ProviderName")
                End If


                If dtProviders.Columns.Contains("nProviderId") Then
                    dtProviders.Columns("nProviderId").ColumnName = "ProviderID"
                End If
            End If

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVP_Providers"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtProviders
            oParameters.Add(oParameter)
            oParameter = Nothing


            If Not IsNothing(ResultTVP1) Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ResultTVP1"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Structured
                oParameter.Value = ResultTVP1
                oParameters.Add(oParameter)
                oParameter = Nothing
            End If

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@PatientID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = PatientID
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, ds)

            oDB.Disconnect()


            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Denominator"
                ds.Tables(2).TableName = "Numerator"
                ds.Tables(3).TableName = "QRDAIIICount"
                ds.Tables(4).TableName = "QRDAIIICodes"
                ds.Tables(5).TableName = "QRDAICodes"

                If ds.Tables.Count >= 8 Then
                    If IsNothing(ds.Tables(6)) = False Then
                        ds.Tables(6).TableName = "DenominatorExclusionsPatientID"
                    End If

                    If IsNothing(ds.Tables(7)) = False Then
                        ds.Tables(7).TableName = "DenominatorExceptionsPatientID"
                    End If
                End If
                If ds.Tables.Count > 9 Then
                    ds.Tables(8).TableName = "Pop2Count"
                    ds.Tables(9).TableName = "DenominatorPOP2"
                    ds.Tables(10).TableName = "NumeratorPOP2"
                    '  ds.Tables(11).TableName = "QRDAIIICountPOP2"
                    '  ds.Tables(11).TableName = "QRDAIIICodesPOP2"
                    ds.Tables(11).TableName = "QRDAICodesPOP2"
                    If ds.Tables.Count > 14 Then
                        If Not IsNothing(ds.Tables(12)) Then
                            ds.Tables(12).TableName = "DenominatorExclusionsPatientIDPOP2"
                        End If
                        If Not IsNothing(ds.Tables(13)) Then
                            ds.Tables(13).TableName = "DenominatorExceptionsPatientIDPOP2"
                        End If
                    End If

                    ds.Tables(14).TableName = "Pop3Count"
                    ds.Tables(15).TableName = "DenominatorPOP3"
                    ds.Tables(16).TableName = "NumeratorPOP3"
                    '  ds.Tables(19).TableName = "QRDAIIICountPOP3"
                    '  ds.Tables(20).TableName = "QRDAIIICodesPOP3"
                    ds.Tables(17).TableName = "QRDAICodesPOP3"
                    If ds.Tables.Count = 20 Then
                        If Not IsNothing(ds.Tables(18)) Then
                            ds.Tables(18).TableName = "DenominatorExclusionsPatientIDPOP3"
                        End If
                        If Not IsNothing(ds.Tables(19)) Then
                            ds.Tables(19).TableName = "DenominatorExceptionsPatientIDPOP3"
                        End If
                    End If
                End If



            End If
            Dim Percent As Single
            Dim FinalDenominator As Int64
            'Dim populationCount As Int16 = 0
            'If SPName = "MU_NQF0028_MU2" Then
            '    populationcount = 3
            'Else
            '    populationcount = 1
            'End If


            For PopIndex = 0 To populationcount - 1
                If PopIndex = 0 Then
                    dt = ds.Tables("Count")
                ElseIf PopIndex = 1 Then
                    If Not IsNothing(dt) Then
                        dt = Nothing
                    End If
                    If Not IsNothing(ds.Tables("Pop2Count")) Then
                        dt = ds.Tables("Pop2Count")
                    End If
                Else
                    If Not IsNothing(dt) Then
                        dt = Nothing
                    End If
                    If Not IsNothing(ds.Tables("Pop3Count")) Then
                        dt = ds.Tables("Pop3Count")
                    End If
                End If
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        dt.Columns.Add("CQMPercentage")
                        For k = 0 To dt.Rows.Count - 1

                            If CQM2014 = False Then



                                If Convert.ToString(dt.Rows(k)(2)) = "N/A" Then
                                    FinalDenominator = Convert.ToInt64(dt.Rows(k)(0))
                                Else
                                    FinalDenominator = Convert.ToInt64(dt.Rows(k)(0)) - Convert.ToInt64(dt.Rows(k)(2))
                                    FinalDenominator = Math.Abs(FinalDenominator)
                                End If

                                dt.Rows(k)(0) = FinalDenominator

                            End If

                            If Convert.ToString(dt.Rows(k)("Denominator")) = "0" Then
                                dt.Rows(k)("CQMPercentage") = "N/A"
                            Else
                                'Percent = Single.Parse(dt.Rows(k)("Numerator").ToString()) / Single.Parse(dt.Rows(k)("Denominator").ToString()) * 100
                                'dt.Rows(k)("CQMPercentage") = FormatNumber(Percent, 2, TriState.True)

                                Dim deno As Int64 = 0
                                deno = Convert.ToInt64(dt.Rows(0)(0))
                                If CQM2014 = True Then
                                    deno = Convert.ToInt64(dt.Rows(0)(0)) - Convert.ToInt64(dt.Rows(0)(2)) - Convert.ToInt64(dt.Rows(0)(3))  ''denominator-exclusion
                                End If
                                If deno = 0 Then
                                    dt.Rows(0)("CQMPercentage") = "N/A"
                                Else
                                    Percent = (Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(deno)) * 100
                                    dt.Rows(0)("CQMPercentage") = FormatNumber(Percent, 2, TriState.True)
                                End If

                            End If
                        Next
                    End If
                End If
            Next


            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing

            Return ds


        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try


    End Function



    Public Function RetriveData_RxNormDIB(ByVal SPName As String, Optional ByVal HistoryItemList As String = "", Optional ByVal CQM2014 As Boolean = False) As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dt As New DataTable
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing



            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DIB_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DIB_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, ds)
            ''
            oDB.Disconnect()


            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Denominator"
                ds.Tables(2).TableName = "Numerator"

            End If
            dt = ds.Tables("Count")



            Dim Percent As Single
            Dim FinalDenominator As Int64


            If dt.Rows.Count > 0 Then
                dt.Columns.Add("CQMPercentage")
                For k = 0 To dt.Rows.Count - 1

                    If CQM2014 = False Then



                        If Convert.ToString(dt.Rows(k)(2)) = "N/A" Then
                            FinalDenominator = Convert.ToInt64(dt.Rows(k)(0))
                        Else
                            FinalDenominator = Convert.ToInt64(dt.Rows(k)(0)) - Convert.ToInt64(dt.Rows(k)(2))
                            FinalDenominator = Math.Abs(FinalDenominator)
                        End If

                        dt.Rows(k)(0) = FinalDenominator

                    End If

                    If Convert.ToString(dt.Rows(k)(0)) = "0" Then
                        dt.Rows(k)(3) = "N/A"
                    Else
                        Percent = Single.Parse(dt.Rows(k)(1).ToString()) / Single.Parse(dt.Rows(k)(0).ToString()) * 100
                        dt.Rows(k)(3) = FormatNumber(Percent, 2, TriState.True)
                    End If
                Next
            End If
            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing
            ''Sanjog -Added on 20101101 for insert Update log
            Return ds


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing

        Finally

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try


    End Function

    Public Function RetriveData_NQF0041_ds(ByVal SPName As String, ByVal _selectedvalue As String, Optional ByVal HistoryItemList As String = "", Optional ByVal dtInfluenzavaccineSnoMed As DataTable = Nothing) As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dt As New DataTable
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = _selectedvalue
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing



            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DIB_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DIB_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing

            ''-- Added TVP paraMeter from DIB Service Call Input
            If Not IsNothing(dtInfluenzavaccineSnoMed) Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@Influenza_vaccine_SnoMed"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Structured
                oParameter.Value = dtInfluenzavaccineSnoMed
                oParameters.Add(oParameter)
                oParameter = Nothing
            End If


            oDB.Retrive("" + SPName + "", oParameters, ds)
            ''
            oDB.Disconnect()


            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Denominator"
                ds.Tables(2).TableName = "Numerator"

            End If
            dt = ds.Tables("Count")



            Dim Percent As Single
            Dim FinalDenominator As Int64


            If dt.Rows.Count > 0 Then
                dt.Columns.Add()
                For k = 0 To dt.Rows.Count - 1


                    If Convert.ToString(dt.Rows(k)(2)) = "N/A" Then
                        FinalDenominator = Convert.ToInt64(dt.Rows(k)(0))
                    Else
                        FinalDenominator = Convert.ToInt64(dt.Rows(k)(0)) - Convert.ToInt64(dt.Rows(k)(2))
                        FinalDenominator = Math.Abs(FinalDenominator)
                    End If
                    dt.Rows(k)(0) = FinalDenominator
                    If Convert.ToString(dt.Rows(k)(0)) = "0" Then
                        dt.Rows(k)(3) = "N/A"
                    Else
                        Percent = Single.Parse(dt.Rows(k)(1).ToString()) / Single.Parse(dt.Rows(k)(0).ToString()) * 100
                        dt.Rows(k)(3) = FormatNumber(Percent, 2, TriState.True)
                    End If
                Next
            End If
            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing

            Return ds

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

    Private Function RetriveDataFromMultipleServer_ds(ByVal SPName As String, Optional ByVal HistoryItemList As String = "") As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dt As New DataTable
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@Snomed_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBServerName
            ''gstrSMDBServerName()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@Snomed_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Retrive("" + SPName + "", oParameters, ds)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            Dim FinalDenominator As Int64

            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Denominator"
                ds.Tables(2).TableName = "Numerator"
            End If

            dt = ds.Tables("Count")

            If dt.Rows.Count > 0 Then
                dt.Columns.Add()
                If Convert.ToString(dt.Rows(0)(2)) = "N/A" Then
                    FinalDenominator = Convert.ToInt64(dt.Rows(0)(0))
                Else
                    FinalDenominator = Convert.ToInt64(dt.Rows(0)(0)) - Convert.ToInt64(dt.Rows(0)(2))
                    FinalDenominator = Math.Abs(FinalDenominator)
                End If
                dt.Rows(0)(0) = FinalDenominator
                If Convert.ToString(dt.Rows(0)(0)) = "0" Then
                    dt.Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
                    dt.Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)
                End If
            End If



            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog(SPName)
            ocls_general = Nothing

            Return ds
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function



#End Region
    ''End function Added by Mayuri:20101028
    Private Sub FillProviders()
        Try
            Dim dtProvider As New DataTable
            'Dim dr As DataRow
            'Dim i As Int16
            '  dtProvider = objPatient.GetProviders(_ClinicID)
            dtProvider.Columns.Add("nProviderID", GetType(System.Int64))
            dtProvider.Columns.Add("ProviderName", GetType(String))
            If Not IsNothing(dtProvider) Then
                Dim dr As DataRow
                dr = dtProvider.NewRow
                dr("nProviderID") = 0
                dr("ProviderName") = "All"
                dtProvider.Rows.InsertAt(dr, 0)
                dtProvider.AcceptChanges()

                If dtProvider.Rows.Count > 0 Then
                    cmbProviders.DataSource = dtProvider.Copy()
                    cmbProviders.ValueMember = dtProvider.Columns("nProviderID").ColumnName
                    cmbProviders.DisplayMember = dtProvider.Columns("ProviderName").ColumnName
                    cmbProviders.Refresh()

                    If ProviderID = 0 Then
                        cmbProviders.SelectedIndex = 0
                    Else
                        cmbProviders.SelectedValue = ProviderID
                    End If

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "CalculatePercentforMorePopulation"
    Private Function CalculatePercentforMorePopulation(ByVal SPName As String, Optional ByVal HistoryItemList As String = "") As DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim dt As New DataTable

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, dt)
            ''
            oDB.Disconnect()
            Dim Percent As Single

            Dim i As Integer
            Dim resultdt As New DataTable
            resultdt.Columns.Add()
            resultdt.Columns.Add()
            resultdt.Columns.Add()
            resultdt.Columns.Add()
            If dt.Columns.Count > 3 Then
                For i = 1 To dt.Columns.Count - 2
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0)(0).ToString() = "0" Then
                            resultdt.Rows.Add()
                            resultdt.Rows(i - 1)(0) = dt.Rows(0)(0).ToString()
                            resultdt.Rows(i - 1)(1) = dt.Rows(0)(i).ToString()
                            resultdt.Rows(i - 1)(2) = dt.Rows(0)(4).ToString()
                            resultdt.Rows(i - 1)(3) = "N/A"
                        Else
                            Percent = Single.Parse(dt.Rows(0)(i).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
                            Percent = FormatNumber(Percent, 2, TriState.True)
                            resultdt.Rows.Add()
                            resultdt.Rows(i - 1)(0) = dt.Rows(0)(0).ToString()
                            resultdt.Rows(i - 1)(1) = dt.Rows(0)(i).ToString()
                            resultdt.Rows(i - 1)(2) = dt.Rows(0)(4).ToString()
                            resultdt.Rows(i - 1)(3) = Percent
                        End If

                    End If
                Next

            End If

            Return resultdt

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function


    Private Function CalculatePercentforMorePopulation_ds(ByVal SPName As String, Optional ByVal HistoryItemList As String = "") As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Dim dt As New DataTable
        Dim ds As New DataSet

        Try
            oDB.Connect(False)


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, ds)
            ''
            oDB.Disconnect()

            If IsNothing(ds) = False Then
                ds.Tables(0).TableName = "Count"
                ds.Tables(1).TableName = "Count1"
                ds.Tables(2).TableName = "Count2"
                ds.Tables(3).TableName = "Denominator1"
                ds.Tables(4).TableName = "Numerator1"
                ds.Tables(5).TableName = "Denominator2"
                ds.Tables(6).TableName = "Numerator2"
                ds.Tables(7).TableName = "Denominator3"
                ds.Tables(8).TableName = "Numerator3"
            End If


            If ds.Tables("Count").Rows.Count > 0 Then


                ds.Tables("Count").Columns.Add()
                Dim Percent As Single

                If ds.Tables("Count").Rows(0)(0).ToString() = "0" Then
                    ds.Tables("Count").Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(ds.Tables("Count").Rows(0)(1).ToString()) / Single.Parse(ds.Tables("Count").Rows(0)(0).ToString()) * 100
                    ds.Tables("Count").Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If


            End If
            If ds.Tables("Count1").Rows.Count > 0 Then


                ds.Tables("Count1").Columns.Add()
                Dim Percent As Single

                If ds.Tables("Count1").Rows(0)(0).ToString() = "0" Then
                    ds.Tables("Count1").Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(ds.Tables("Count1").Rows(0)(1).ToString()) / Single.Parse(ds.Tables("Count1").Rows(0)(0).ToString()) * 100
                    ds.Tables("Count1").Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If


            End If
            If ds.Tables("Count2").Rows.Count > 0 Then


                ds.Tables("Count2").Columns.Add()
                Dim Percent As Single

                If ds.Tables("Count2").Rows(0)(0).ToString() = "0" Then
                    ds.Tables("Count2").Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(ds.Tables("Count2").Rows(0)(1).ToString()) / Single.Parse(ds.Tables("Count2").Rows(0)(0).ToString()) * 100
                    ds.Tables("Count2").Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If


            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

        Return ds

    End Function

#End Region

    Private Function RetriveDataWithHistoryItems(ByVal SPName As String, Optional ByVal HistoryItemList As String = "") As DataTable

        Dim dt As New DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@HistoryItemList"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = _HistoryItemList
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing



            oDB.Retrive("" + SPName + "", oParameters, dt)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            Dim FinalDenominator As Int64
            If dt.Rows.Count > 0 Then
                dt.Columns.Add()
                If Convert.ToString(dt.Rows(0)(2)) = "N/A" Then
                    FinalDenominator = Convert.ToInt64(dt.Rows(0)(0))
                Else
                    FinalDenominator = Convert.ToInt64(dt.Rows(0)(0)) - Convert.ToInt64(dt.Rows(0)(2))
                    FinalDenominator = Math.Abs(FinalDenominator)
                End If
                dt.Rows(0)(0) = FinalDenominator
                If dt.Rows(0)(0).ToString() = "0" Then
                    dt.Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
                    dt.Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If

            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing

        Finally
            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function
    ''Added by Mayuri:20100901-To retrieve tobacco related history items
#Region "Cell Button Click Event"
    ''Sanjog -Added on 20101029 to make checkbox column non editable
    Private Sub C1QualityMeasures_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.BeforeEdit
        If e.Col = COL_Check Then
            Dim checkval As C1.Win.C1FlexGrid.CheckEnum
            checkval = C1QualityMeasures.GetCellCheck(e.Row, e.Col)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked Or checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

            Else
                e.Cancel = True
            End If
        End If
    End Sub
    ''Sanjog -Added on 20101029 to make checkbox column non editable

    Private Sub C1QualityMeasures_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.CellButtonClick
        If C1QualityMeasures.ColSel = COL_Button Then
            If C1QualityMeasures.Row = 14 Then
                Try
                    ofrmList = New frmViewListControl
                    ofrmList.Text = "History Items"

                    oListhistoryItem = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.HistoryItem, True, ofrmList.Width)
                    oListhistoryItem.ControlHeader = "History Items"


                    AddHandler oListhistoryItem.ItemSelectedClick, AddressOf oListhistoryItem_ItemSelectedClick
                    AddHandler oListhistoryItem.ItemClosedClick, AddressOf oListhistoryItem_ItemClosedClick
                    ''To get previously selected items
                    If IsNothing(NQF0028bHistoryItemList) = False Then
                        For i As Integer = 0 To NQF0028bHistoryItemList.Count - 1
                            oListhistoryItem.SelectedItems.Add(NQF0028bHistoryItemList(i))
                        Next
                    End If


                    ofrmList.Controls.Add(oListhistoryItem)
                    oListhistoryItem.Dock = DockStyle.Fill
                    oListhistoryItem.BringToFront()
                    oListhistoryItem.OpenControl()
                    oListhistoryItem.ShowHeaderPanel(False)
                    ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

                    If IsNothing(ofrmList) = False Then
                        RemoveHandler oListhistoryItem.ItemSelectedClick, AddressOf oListhistoryItem_ItemSelectedClick
                        RemoveHandler oListhistoryItem.ItemClosedClick, AddressOf oListhistoryItem_ItemClosedClick
                        ofrmList.Controls.Remove(oListhistoryItem)
                        oListhistoryItem.Dispose()
                        oListhistoryItem = Nothing
                        ofrmList.Dispose()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            ElseIf C1QualityMeasures.Row = 11 Then
                Try
                    ofrmList = New frmViewListControl
                    ofrmList.Text = "History Items"

                    oListhistoryItem = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.HistoryItem, True, ofrmList.Width)
                    oListhistoryItem.ControlHeader = "History Items"


                    AddHandler oListhistoryItem.ItemSelectedClick, AddressOf oListhistoryItem_ItemSelectedClick
                    AddHandler oListhistoryItem.ItemClosedClick, AddressOf oListhistoryItem_ItemClosedClick
                    ''To get previously selected items
                    If IsNothing(HistoryItemList) = False Then
                        For i As Integer = 0 To HistoryItemList.Count - 1
                            oListhistoryItem.SelectedItems.Add(HistoryItemList(i))
                        Next
                    End If


                    ofrmList.Controls.Add(oListhistoryItem)
                    oListhistoryItem.Dock = DockStyle.Fill
                    oListhistoryItem.BringToFront()
                    oListhistoryItem.OpenControl()
                    oListhistoryItem.ShowHeaderPanel(False)
                    ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

                    If IsNothing(ofrmList) = False Then
                        RemoveHandler oListhistoryItem.ItemSelectedClick, AddressOf oListhistoryItem_ItemSelectedClick
                        RemoveHandler oListhistoryItem.ItemClosedClick, AddressOf oListhistoryItem_ItemClosedClick
                        ofrmList.Controls.Remove(oListhistoryItem)
                        oListhistoryItem.Dispose()
                        oListhistoryItem = Nothing
                        ofrmList.Dispose()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
            'Dim frmHistory As New frmHistory(gnVisitID, Date.Now.ToShortDateString(), False)
            '' Dim frmHistory As New frmHistory()
            ''frmHistory.MdiParent = Me
            'frmHistory.Show()

        End If
    End Sub

#End Region
    Dim dtpat_data As DataTable = Nothing

    Private Sub oListhistoryItem_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try



            If C1QualityMeasures.Row = 11 Then


                HistoryItemList = New gloGeneralItem.gloItems()
                Dim HistoryItem As gloGeneralItem.gloItem
                If oListhistoryItem.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListhistoryItem.SelectedItems.Count - 1

                        HistoryItem = New gloGeneralItem.gloItem()
                        HistoryItem.ID = oListhistoryItem.SelectedItems(i).ID
                        HistoryItem.Description = oListhistoryItem.SelectedItems(i).Description
                        HistoryItemList.Add(HistoryItem)
                        HistoryItem.Dispose()
                        ' HistoryItem = Nothing

                    Next
                End If

            ElseIf C1QualityMeasures.Row = 14 Then
                NQF0028bHistoryItemList = New gloGeneralItem.gloItems()
                Dim NQF0028bHistoryItem As gloGeneralItem.gloItem
                If oListhistoryItem.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListhistoryItem.SelectedItems.Count - 1

                        NQF0028bHistoryItem = New gloGeneralItem.gloItem()
                        NQF0028bHistoryItem.ID = oListhistoryItem.SelectedItems(i).ID
                        NQF0028bHistoryItem.Description = oListhistoryItem.SelectedItems(i).Description
                        NQF0028bHistoryItemList.Add(NQF0028bHistoryItem)
                        NQF0028bHistoryItem.Dispose()
                        ' HistoryItem = Nothing

                    Next
                End If

            End If




            ofrmList.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''Added by Mayuri:20100901-To retrieve tobacco related history items and close form 
    Private Sub oListhistoryItem_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Public Sub New(nPatientID As Int64, nProviderID As Int64, PatientName As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        PatientID = nPatientID
        ProviderID = nProviderID
        fillcombo(ProviderID)
        strPatientDetails = PatientName
        pnlPatientDetails.Visible = True

        ' Add any initialization after the InitializeComponent() call.
        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID").ToString() <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
                ' cmbProviders.SelectedValue = _ClinicID
            End If
        End If
        If appSettings("UserID") IsNot Nothing Then
            If appSettings("UserID").ToString() <> "" Then
                _UserID = Convert.ToInt64(appSettings("UserID"))
                ' cmbProviders.SelectedValue = _UserID
            End If
        End If
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then
                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If




    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID").ToString() <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
                ' cmbProviders.SelectedValue = _ClinicID
            End If
        End If
        If appSettings("UserID") IsNot Nothing Then
            If appSettings("UserID").ToString() <> "" Then
                _UserID = Convert.ToInt64(appSettings("UserID"))
                ' cmbProviders.SelectedValue = _UserID
            End If
        End If
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then
                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If

        PatientID = 0
        ProviderID = 0

    End Sub

    ''Sanjog-Added On 20101207 to show the Providername and the start and End Date of Dashboard
    Public Sub New(ByVal providerName As String, ByVal startDate As String, ByVal endDate As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID").ToString() <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
                fillcombo(_ClinicID)
                If (Not IsNothing(cmbProviders.DataSource)) Then
                    cmbProviders.SelectedValue = _ClinicID
                End If
            End If
        End If
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then
                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If

        PName = providerName
        SDate = startDate
        EDate = endDate
    End Sub
    Private Sub fillcombo(ByVal provid As Long)
        Dim dt As DataTable = FillCustomGridProvider(provid)

        If (Not IsNothing(dt)) Then
            Dim dr As DataRow() = dt.Select("nProviderid=" & provid & "")
            If (dr.Length > 0) Then
                Dim dtpr As DataTable = dt.Clone()

                dtpr.ImportRow(dr(0))
                cmbProviders.DataSource = dtpr
                cmbProviders.ValueMember = "nProviderId"
                cmbProviders.DisplayMember = "ProviderName"
            End If
            dt.Dispose()
            dt = Nothing
        End If

    End Sub

    ''Sanjog-Added On 20101207 to show the Providername and the start and End Date of Dashboard
    Private Sub C1QualityMeasures_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1QualityMeasures.EnterCell
        'C1QualityMeasures.Row = C1QualityMeasures.Row
        'If C1QualityMeasures.RowSel = 25 AndAlso C1QualityMeasures.RowSel = 26 Then
        '    C1QualityMeasures.Cols(COL_Check).AllowEditing = False
        'Else
        '    C1QualityMeasures.Cols(COL_Check).AllowEditing = True
        'End If
    End Sub

    Private Sub tlbbtn_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If chkCQM2014.Checked = True Then
                GenerateReport_CQM2014()
                For row As Integer = 0 To C1QualityMeasures.Rows.Count - 1
                    C1QualityMeasures.Rows(row).AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.None
                    C1QualityMeasures.Rows(row).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                Next
                C1QualityMeasures.Cols(COL_CoreMeasure_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1QualityMeasures.Cols(COL_Domain_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1QualityMeasures.Cols(COL_Denomenator_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                C1QualityMeasures.Cols(COL_Numeratot_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                C1QualityMeasures.Cols(COL_Denomenator_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                C1QualityMeasures.Cols(COL_DenominatorExceptionsPatientID_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                C1QualityMeasures.Cols(COL_DenominatorExclusionsPatientID_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

            Else
                GenerateReport()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GenerateReport_CQM2014()

        If (cmbProviders.DataSource Is Nothing) OrElse cmbProviders.Items.Count = 0 Then

            MessageBox.Show("Please Select Provider", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If



        If (GenerateMessagefor2014()) Then
            Dim count_Measure As Integer = 0

            pnlLoadingLable.Refresh()
            Me.WindowState = FormWindowState.Maximized 'Resolved Bug #109434:  While clicking on CQM show reports the window is moving in background 
            tlbbtn_DeelectAll.Enabled = False
            tlbbtn_Export.Enabled = False
            tlbbtn_SelectAll.Enabled = False
            tlbbtn_Close.Enabled = False
            pnlLoadingLable.Visible = True
            pnlLoadingLable.BringToFront()

            Me.Label6.Text = "Calculating measures... Please wait..."
            Me.Cursor = Cursors.WaitCursor

            SetReportingPeriod(cmbProviders.DataSource, dtpicStartDate.Value, dtpicEndDate.Value)

            Dim checkval As C1.Win.C1FlexGrid.CheckEnum
            Dim dsData As DataSet
            dtVistBasedDenominator = New DataTable
            dtVistBasedNumerator = New DataTable
            ''Mayuri
            dtStratum1 = New DataTable
            dtStratum2 = New DataTable
            dtCount = New DataTable
            dtCodes = New DataTable
            dtQRDAIPatientList = New DataTable

            dtQRDA1Data = New DataTable
            dtMeasureList = New DataTable
            'dtMeasureList.Columns.Add("ID")
            dtMeasureList.Columns.Add("MeasureName")
            dtQRDAIPatientList.Columns.Add("InitialPatientPopulation")
            dtQRDAIPatientList.Columns.Add("nVisitID")
            dtQRDAIPatientList.Columns.Add("MeasureNo")

            dtQRDAIPatientList.Columns(0).DataType = System.Type.GetType("System.Int64")
            dtQRDAIPatientList.Columns(1).DataType = System.Type.GetType("System.Int64")
            dtQRDAIPatientList.Columns(2).DataType = System.Type.GetType("System.String")

            'NQF0002
            CallCQMFilterData()
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0002_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0002"
                Application.DoEvents()
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_NQF0002_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If
                    End If
                End Using

                dsData = RetriveData_RxNormDIB_QRDAIII("MU_NQF0002_MU2", "", True, resultTVP1, PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0002"
                End If



                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0002_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))

                    C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_DPatientID_2014, Nothing)


                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If



            'NQF0018
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0018_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0018"
                Application.DoEvents()

                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0018_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0018"
                End If

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0018_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If



            Else
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_DPatientID_2014, Nothing)



                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0018_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If



            'NQF0028
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0028_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0028"
                Application.DoEvents()
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_NQF0028_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If
                    End If
                End Using
                dsData = RetriveData_RxNormDIB_QRDAIII("MU_NQF0028_MU2", "", True, resultTVP1, PatientID, 3)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)

                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0028"
                End If

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0028_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())

                    'C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    If IsNothing(dsData.Tables("Count").Columns("Exception")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exception").ToString())
                    End If

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                'dtCount.Merge(dsData.Tables("QRDAIIICountPOP2"))
                'dtCodes.Merge(dsData.Tables("QRDAIIICodesPOP2").Copy())
                'dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodesPOP2").Copy(), True, MissingSchemaAction.Ignore)


                'Set Data for population 2
                If Not IsNothing(dsData.Tables("Pop2Count")) Then
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_IPP_2014, dsData.Tables("Pop2Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Denomenator_2014, dsData.Tables("Pop2Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0028_2014 + 4, Convert.ToInt32(dsData.Tables("Pop2Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Numeratot_2014, dsData.Tables("Pop2Count").Rows(0)("Numerator").ToString())

                    'C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    If IsNothing(dsData.Tables("Pop2Count").Columns("Exception")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Exception_2014, dsData.Tables("Pop2Count").Rows(0)("Exception").ToString())
                    End If

                    If dsData.Tables("Pop2Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Percent_2014, dsData.Tables("Pop2Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Percent_2014, dsData.Tables("Pop2Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("DenominatorPOP2")) = False Then
                    If dsData.Tables("DenominatorPOP2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_DPatientID_2014, dsData.Tables("DenominatorPOP2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("NumeratorPOP2")) = False Then
                    If dsData.Tables("NumeratorPOP2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_NPatientID_2014, dsData.Tables("NumeratorPOP2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientIDPOP2")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientIDPOP2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientIDPOP2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientIDPOP2")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientIDPOP2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientIDPOP2").Rows(0)(0).ToString())
                    End If
                End If

                'dtCount.Merge(dsData.Tables("QRDAIIICountPOP3"))
                'dtCodes.Merge(dsData.Tables("QRDAIIICodesPOP3").Copy())
                'dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodesPOP3").Copy(), True, MissingSchemaAction.Ignore)

                'Set Data for population 3
                If Not IsNothing(dsData.Tables("Pop3Count")) Then
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_IPP_2014, dsData.Tables("Pop3Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Denomenator_2014, dsData.Tables("Pop3Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0028_2014 + 6, Convert.ToInt32(dsData.Tables("Pop3Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Numeratot_2014, dsData.Tables("Pop3Count").Rows(0)("Numerator").ToString())

                    'C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    If IsNothing(dsData.Tables("Pop3Count").Columns("Exception")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Exception_2014, dsData.Tables("Pop3Count").Rows(0)("Exception").ToString())
                    End If

                    If dsData.Tables("Pop3Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Percent_2014, dsData.Tables("Pop3Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Percent_2014, dsData.Tables("Pop3Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorPOP3")) = False Then
                    If dsData.Tables("DenominatorPOP3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_DPatientID_2014, dsData.Tables("DenominatorPOP3").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("NumeratorPOP3")) = False Then
                    If dsData.Tables("NumeratorPOP3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_NPatientID_2014, dsData.Tables("NumeratorPOP3").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientIDPOP3")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientIDPOP3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientIDPOP3").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientIDPOP3")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientIDPOP3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientIDPOP3").Rows(0)(0).ToString())
                    End If
                End If
            Else
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_DPatientID_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_DPatientID_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 4, COL_DenominatorExceptionsPatientID_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_DPatientID_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028_2014 + 6, COL_DenominatorExceptionsPatientID_2014, Nothing)
            End If



            'NQF0031
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0031_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0031"
                Application.DoEvents()

                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0031_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0031"
                End If

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0031_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031_2014 + 2, COL_DPatientID_2014, Nothing)
            End If



            'NQF0032
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0032_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0032"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0032_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0032"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0032_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032_2014 + 2, COL_DPatientID_2014, Nothing)
            End If


            'NQF0033
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0033_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0033"
                Application.DoEvents()
                dsData = Nothing
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Dim resultTVP2 As DataTable = Nothing
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_NQF0033_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If
                        If Not IsNothing(resultSet.MarketedProductIdSet2) Then
                            resultTVP2 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet2)
                        End If

                    End If
                End Using

                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0033_MU2", "", True, False, resultTVP1, resultTVP2, PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then

                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0033"
                End If
                dtStratum1.Merge(dsData.Tables(9).Copy())
                dtStratum2.Merge(dsData.Tables(12).Copy())


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0033_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033_2014 + 2, COL_DPatientID_2014, Nothing)
            End If



            'NQF0034
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0034_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0034"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0034_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0034"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0034_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0034_2014 + 2, COL_DPatientID_2014, Nothing)
            End If

            'NQF0038
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0038_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0038"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0038_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0038"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0038_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    'Exclusion is not present for this measure
                    'C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0038_2014 + 2, COL_DPatientID_2014, Nothing)
            End If


            'NQF0041
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0041_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0041"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0041_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0041"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0041_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    'C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())


                    C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exception").ToString())


                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_DPatientID_2014, Nothing)


                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If


            'CMSID22
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID22_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure CMS 22"
                Application.DoEvents()
                dsData = Nothing
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_CMSID22_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If
                    End If
                End Using
                dsData = CalculatePercent_ds_ORDAIII("MU_CMSID22_MU2", "", True, False, resultTVP1, , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "CMSID22"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF_CMSID22_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exception").ToString())

                    If IsNothing(dsData.Tables("Count").Columns("Exceptions")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exceptions").ToString())
                    End If

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_DPatientID_2014, Nothing)


                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID22_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If
            'NQF0043
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0043_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0043"
                Application.DoEvents()
                dsData = Nothing

                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0043_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0043"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0043_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0043_2014 + 2, COL_DPatientID_2014, Nothing)
            End If



            'NQF0052
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0052_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0052"
                Application.DoEvents()
                dsData = Nothing
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_NQF0052_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If
                    End If
                End Using
                ' dsData = CalculatePercent_ds_ORDAIII("MU_NQF0052_MU2", "", True, , , , PatientID)
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0052_MU2", "", True, False, resultTVP1, , PatientID)
                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0052"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0052_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If



            Else
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_DPatientID_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0052_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If


            'NQF0055
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0055_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Me.Label6.Text = "Calculating " & "Measure NQF0055"
                Application.DoEvents()


                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0055_MU2", "", True, , , , PatientID)


                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0055"
                End If

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0055_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If



            Else
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_DPatientID_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)
            End If



            'NQF0056
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0056_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0056"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0056_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0056"
                End If

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0056_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_DPatientID_2014, Nothing)


                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If



            'NQF0059
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0059_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0059"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0059_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0059"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0059_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If



            Else
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_DPatientID_2014, Nothing)


                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If



            'NQF0062
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0062_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0062"
                Application.DoEvents()
                dsData = Nothing
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_NQF0062_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If
                    End If
                End Using
                dsData = RetriveData_RxNormDIB_QRDAIII("MU_NQF0062_MU2", "", True, resultTVP1, PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0062"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0062_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())


                    C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If



            Else
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_DPatientID_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If



            'NQF0064
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0064_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure CMSID 163"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0064_MU2", "", True, , , , PatientID)

                'dtCount.Merge(dsData.Tables("QRDAIIICount"))
                'dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())

                'dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                'If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                '    dtMeasureList.Rows.Add()
                '    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0064"
                'End If

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0064_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    'C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_DPatientID_2014, Nothing)


                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If



            'NQF0068
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0068_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0068"
                Application.DoEvents()
                dsData = Nothing
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Dim resultTVP2 As DataTable = Nothing

                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_NQF0068_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If

                        If Not IsNothing(resultSet.MarketedProductIdSet2) Then
                            resultTVP2 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet2)
                        End If

                    End If
                End Using
                'dsData = RetriveData_RxNormDIB_QRDAIII("MU_NQF0068_MU2", "", True, resultTVP1, PatientID)
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0068_MU2", "", True, False, resultTVP1, resultTVP2, PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0068"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0068_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Exception_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068_2014 + 2, COL_DPatientID_2014, Nothing)
            End If


            'NQF0101
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0101_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0101"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0101_MU2", "", True, , , , PatientID)

                'dtVistBasedDenominator.Merge(dsData.Tables("Denominator"))
                'dtVistBasedNumerator.Merge(dsData.Tables("Numerator"))

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes"))
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes"), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0101"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0101_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())


                    C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    'Exception is not present for this measure 2018 change 
                    '  C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exception").ToString())

                    'If IsNothing(dsData.Tables("Count").Columns("Exceptions")) = False Then
                    '    C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exceptions").ToString())
                    'End If

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_DPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0101_2014 + 2, COL_Exception_2014, Nothing)
            End If


            'NQF-0421
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0421_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0421"
                Application.DoEvents()
                dsData = Nothing
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Dim resultTVP2 As DataTable = Nothing
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_NQF0421_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If
                        If Not IsNothing(resultSet.MarketedProductIdSet2) Then
                            resultTVP2 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet2)
                        End If

                    End If
                End Using

                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0421_MU2", "", True, False, resultTVP1, resultTVP2, PatientID)

                'dtVistBasedDenominator.Merge(dsData.Tables("Denominator"))
                'dtVistBasedNumerator.Merge(dsData.Tables("Numerator"))

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes"))
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes"), True, MissingSchemaAction.Ignore)

                'dtQRDAIPatientList.Merge(dsData.Tables("Table11"), True, MissingSchemaAction.Ignore)

                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0421"
                End If

                'If dsData.Tables.Count >= 14 Then
                '    dsData.Tables(8).TableName = "Count2"
                '    dsData.Tables(9).TableName = "Denominator2"
                '    dsData.Tables(10).TableName = "Numerator2"
                '    '27-Sep-16 Aniket: Resolving Bug #100471 ( Modified): CMS69 : gloEMR :Application increases count of Deno Excep. instead of Deno Exclu.
                '    dsData.Tables(12).TableName = "DenominatorExclusionsPatientID2"
                '    dsData.Tables(13).TableName = "DenominatorExceptionsPatientID2"
                'End If

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0421_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    'Exceptions is not present for this measure
                    If IsNothing(dsData.Tables("Count").Columns("Exception")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exception").ToString())
                    End If

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If



                If IsNothing(dsData.Tables("Count2")) = False Then

                    Dim Percent As Single

                    dsData.Tables("Count2").Columns.Add("CQMPercentage")

                    If dsData.Tables("Count2").Rows(0)("Denominator").ToString() = "0" Then
                        dsData.Tables("Count2").Rows(0)("CQMPercentage") = "N/A"
                    Else
                        Percent = Single.Parse(dsData.Tables("Count2").Rows(0)("Numerator").ToString()) / Single.Parse(dsData.Tables("Count2").Rows(0)("Denominator").ToString()) * 100
                        dsData.Tables("Count2").Rows(0)("CQMPercentage") = FormatNumber(Percent, 2, TriState.True)
                    End If

                    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_IPP_2014, dsData.Tables("Count2").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Denomenator_2014, dsData.Tables("Count2").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0421_2014 + 4, Convert.ToInt32(dsData.Tables("Count2").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Numeratot_2014, dsData.Tables("Count2").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Exclusion_2014, dsData.Tables("Count2").Rows(0)("Exclusion").ToString())

                    'If IsNothing(dsData.Tables("Count2").Columns("Exceptions")) = False Then
                    '    C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Exception_2014, dsData.Tables("Count2").Rows(0)("Exceptions").ToString())
                    'End If

                    If IsNothing(dsData.Tables("Count2").Columns("CQMPercentage")) = False Then
                        If dsData.Tables("Count2").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                            C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Percent_2014, dsData.Tables("Count2").Rows(0)("CQMPercentage").ToString())
                        Else
                            C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Percent_2014, dsData.Tables("Count2").Rows(0)("CQMPercentage").ToString() & "%")
                        End If
                    End If

                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                ''Initial Patient Population 2
                'If IsNothing(dsData.Tables("Denominator2")) = False Then
                '    If dsData.Tables("Denominator2").Rows.Count > 0 Then
                '        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_DPatientID_2014, dsData.Tables("Denominator2").Rows(0)(0).ToString())
                '    End If
                'End If

                'If IsNothing(dsData.Tables("Numerator2")) = False Then
                '    If dsData.Tables("Numerator2").Rows.Count > 0 Then
                '        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_NPatientID_2014, dsData.Tables("Numerator2").Rows(0)(0).ToString())
                '    End If
                'End If

                'If IsNothing(dsData.Tables("DenominatorExclusionsPatientID2")) = False Then
                '    If dsData.Tables("DenominatorExclusionsPatientID2").Rows.Count > 0 Then
                '        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID2").Rows(0)(0).ToString())
                '    End If
                'End If

                'If IsNothing(dsData.Tables("DenominatorExclusionsPatientID2")) = False Then
                '    If dsData.Tables("DenominatorExclusionsPatientID2").Rows.Count > 0 Then
                '        C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID2").Rows(0)(0).ToString())
                '    End If
                'End If

            Else

                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_IPP_2014, Nothing) 'Population 1
                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_DPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421_2014 + 2, COL_Exception_2014, Nothing)

                ''Population 2
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_IPP_2014, Nothing)
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Denomenator_2014, Nothing)
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, Col_Icon_2014, Nothing)
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Numeratot_2014, Nothing)
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Exclusion_2014, Nothing)
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Percent_2014, Nothing)
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_NPatientID_2014, Nothing)
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_DPatientID_2014, Nothing)
                'C1QualityMeasures.SetData(ROW_NQF0421_2014 + 4, COL_Exception_2014, Nothing)
            End If



            'CMSID125
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID125_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure CMS 125"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_CMSID125_MU2", "", True, , , , PatientID)

                'dtVistBasedDenominator.Merge(dsData.Tables("Denominator"))
                'dtVistBasedNumerator.Merge(dsData.Tables("Numerator"))

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes"))
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes"), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "CMSID125"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF_CMSID125_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    If IsNothing(dsData.Tables("Count").Columns("Exceptions")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exceptions").ToString())
                    End If

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_DPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID125_2014 + 2, COL_Exception_2014, Nothing)
            End If


            'CMSID66
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID66_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure CMS 66"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_CMSID66_MU2", "", True, , , , PatientID)

                'dtVistBasedDenominator.Merge(dsData.Tables("Denominator"))
                'dtVistBasedNumerator.Merge(dsData.Tables("Numerator"))

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes"))
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes"), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "CMSID66"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF_CMSID66_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    If IsNothing(dsData.Tables("Count").Columns("Exceptions")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exceptions").ToString())
                    End If

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_DPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID66_2014 + 2, COL_Exception_2014, Nothing)
            End If

            'CMSID56
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID56_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure CMS 56"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_CMSID56_MU2", "", True, , , , PatientID)

                'dtVistBasedDenominator.Merge(dsData.Tables("Denominator"))
                'dtVistBasedNumerator.Merge(dsData.Tables("Numerator"))

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes"))
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes"), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "CMSID56"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF_CMSID56_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())

                    If IsNothing(dsData.Tables("Count").Columns("Exceptions")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exceptions").ToString())
                    End If

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_DPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID56_2014 + 2, COL_Exception_2014, Nothing)
            End If

            'NQF0418
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0418_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0418"
                Application.DoEvents()
                dsData = Nothing
                Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
                Dim resultTVP1 As DataTable = Nothing
                Dim resultTVP2 As DataTable = Nothing
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    resultSet = ogloGSHelper.CQMServiceCallGSDD("MU_NQF00418_MU2")
                    If Not IsNothing(resultSet) Then
                        If Not IsNothing(resultSet.MarketedProductIdSet1) Then
                            resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet1)
                        End If
                        If Not IsNothing(resultSet.MarketedProductIdSet2) Then
                            resultTVP2 = cls_DAL_MU_Detail_Report.ConvertToDataTable(resultSet.MarketedProductIdSet2)
                        End If

                    End If
                End Using
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF00418_MU2", "", True, False, resultTVP1, resultTVP2, PatientID)

                'dtVistBasedDenominator.Merge(dsData.Tables("Denominator"))
                'dtVistBasedNumerator.Merge(dsData.Tables("Numerator"))

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes"))
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes"), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0418"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0418_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exception").ToString())

                    If IsNothing(dsData.Tables("Count").Columns("Exceptions")) = False Then
                        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exceptions").ToString())
                    End If

                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_DPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0418_2014 + 2, COL_Exception_2014, Nothing)
            End If


            'NQF0419
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0419_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0419"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_NQF0419_MU2", "", True, , , , PatientID)

                'Commented the below code as it does not contain list of patients to merge
                dtVistBasedDenominator.Merge(dsData.Tables("Denominator"))
                dtVistBasedNumerator.Merge(dsData.Tables("Numerator"))

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes"))
                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes"), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "NQF0419"
                End If


                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF0419_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    'C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())


                    C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Exception_2014, dsData.Tables("Count").Rows(0)("Exception").ToString())


                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("DenoPatients")) = False Then
                    If dsData.Tables("DenoPatients").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_DPatientID_2014, dsData.Tables("DenoPatients").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("NumPatients")) = False Then
                    If dsData.Tables("NumPatients").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_NPatientID_2014, dsData.Tables("NumPatients").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_DPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0419_2014 + 2, COL_Exception_2014, Nothing)
            End If


            'CMSID90
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID90_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure CMSID90"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds_ORDAIII("MU_CMSID90_MU2", "", True, , , , PatientID)

                dtCount.Merge(dsData.Tables("QRDAIIICount"))
                dtCodes.Merge(dsData.Tables("QRDAIIICodes").Copy())

                dtQRDAIPatientList.Merge(dsData.Tables("QRDAICodes").Copy(), True, MissingSchemaAction.Ignore)
                If dsData.Tables("QRDAICodes").Copy().Rows.Count > 0 Then
                    dtMeasureList.Rows.Add()
                    dtMeasureList.Rows(dtMeasureList.Rows.Count - 1)("MeasureName") = "CMSID90"
                End If

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_IPP_2014, dsData.Tables("Count").Rows(0)("InitialPatientPopulation").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Denomenator_2014, dsData.Tables("Count").Rows(0)("Denominator").ToString())
                    SetIconImage(ROW_NQF_CMSID90_2014 + 2, Convert.ToInt32(dsData.Tables("Count").Rows(0)("Denominator")))
                    C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Numeratot_2014, dsData.Tables("Count").Rows(0)("Numerator").ToString())
                    C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Exclusion_2014, dsData.Tables("Count").Rows(0)("Exclusion").ToString())
                    If dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Percent_2014, dsData.Tables("Count").Rows(0)("CQMPercentage").ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_DPatientID_2014, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_NPatientID_2014, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExclusionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExclusionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_DenominatorExclusionsPatientID_2014, dsData.Tables("DenominatorExclusionsPatientID").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("DenominatorExceptionsPatientID")) = False Then
                    If dsData.Tables("DenominatorExceptionsPatientID").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_DenominatorExceptionsPatientID_2014, dsData.Tables("DenominatorExceptionsPatientID").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_IPP_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Denomenator_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, Col_Icon_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Numeratot_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Exclusion_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Percent_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_NPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_DPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_Exception_2014, Nothing)

                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_DenominatorExclusionsPatientID_2014, Nothing)
                C1QualityMeasures.SetData(ROW_NQF_CMSID90_2014 + 2, COL_DenominatorExceptionsPatientID_2014, Nothing)

            End If



            tlbbtn_Close.Enabled = True
            tlbbtn_DeelectAll.Enabled = True
            tlbbtn_Export.Enabled = True
            tlbbtn_SelectAll.Enabled = True

            SetColours()

            Me.Cursor = Cursors.Default

            pnlLoadingLable.Visible = False


        End If

    End Sub

    Private Sub SetIconImage(ByVal rowno As Integer, ByVal Value As Integer)
        If (Value < 20) Then
            C1QualityMeasures.SetData(rowno, Col_Icon_2014, CType(gloMU.My.Resources.Warning, Icon))

            C1QualityMeasures.SetUserData(rowno, Col_Icon_2014, "The minimum patients required for a measure is 20. Select another measure whose denominator count is at least 20.")
        Else
            C1QualityMeasures.SetData(rowno, Col_Icon_2014, Nothing)
        End If
    End Sub
    Private Function CheckOneMeasure() As Boolean
        Dim checkval As C1.Win.C1FlexGrid.CheckEnum
        For Len As Integer = 0 To C1QualityMeasures.Rows.Count - 1
            checkval = C1QualityMeasures.GetCellCheck(Len, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Return True
                Exit For
            End If
        Next
        Return False
    End Function
    Private Function GenerateMessagefor2014() As Boolean
        If bHasQCheckpointMeasure Then
            Return True
        End If

        Dim cnt As Integer = 0
        Dim blnhigh As Boolean = False
        Dim blncross As Boolean = False
        Dim blnout As Boolean = False
        Dim checkval As C1.Win.C1FlexGrid.CheckEnum
        If CheckOneMeasure() Then
            checkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked
            'NQF0002
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0002_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True
                cnt += 1
            End If
            'CMSID90
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID90_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True
                cnt += 1

            End If
            'NQF0419
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0419_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True
                blncross = True
                cnt += 1

            End If
            'NQF0418
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0418_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'CMSID56
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID56_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True

                cnt += 1
            End If
            'CMSID66
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID66_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True
                cnt += 1
            End If
            'CMSID125
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID125_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF-0421
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0421_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                blncross = True
                cnt += 1
            End If
            'NQF0101
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0101_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True

                cnt += 1
            End If
            'NQF0068
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0068_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0064
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0064_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0062
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0062_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0059
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0059_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True

                cnt += 1
            End If
            'NQF0056
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0056_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0055
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0055_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0052
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0052_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True
                cnt += 1
            End If
            'NQF0043
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0043_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'CMSID22
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF_CMSID22_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                blncross = True
                cnt += 1
            End If
            'NQF0041
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0041_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0038
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0038_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0034
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0034_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0033
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0033_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0032
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0032_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0031
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0031_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                cnt += 1
            End If
            'NQF0028
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0028_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                blncross = True
                cnt += 1
            End If
            'NQF0018
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0018_2014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                blnhigh = True
                blncross = True
                blnout = True
                cnt += 1
            End If
            If (cnt >= 6) AndAlso ((blnout = True) Or (blnhigh = True)) Then
                Return True
            Else

                Dim strMessage As String = "You must report 6 measures, including 1 outcome measure, or another high priority measure if outcome measure is unavailable.  A bonus point is given for additional High Priority measures. "

                If cnt < 6 Then
                    strMessage += "Please select at least 6 total measures. Do you want to continue?"

                    Me.WindowState = FormWindowState.Maximized  'Resolved Bug #109434:  While clicking on CQM show reports the window is moving in background 
                    If MessageBox.Show(Me, strMessage, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Return True
                    End If
                Else

                    'If (blnout = False) AndAlso (blnhigh = False) Then
                    '    strMessage += "Please select additional measures to meet these requirements. Do you want to continue?"
                    '    If MessageBox.Show(strMessage, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '        Return True
                    '    End If

                    'ElseIf (blncross = False) AndAlso ((blnhigh = True) Or (blncross = True)) Then
                    '    If MessageBox.Show("No cross-cutting measure selected. Do you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '        Return True
                    '    End If

                    If (blnout = False) AndAlso (blnhigh = False) Then
                        strMessage += "Please select additional measures to meet these requirements. Do you want to continue?"
                        If MessageBox.Show(strMessage, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            Return True
                        End If
                    End If
                End If
            End If
        End If

        Return False

    End Function
    Private Sub GenerateReport()
        pnlLoadingLable.Refresh()
        tlbbtn_DeelectAll.Enabled = False
        tlbbtn_Export.Enabled = False
        tlbbtn_SelectAll.Enabled = False
        tlbbtn_Close.Enabled = False
        tsp_QRDAIII.Enabled = False
        pnlLoadingLable.Visible = True
        pnlLoadingLable.BringToFront()
        Me.Label6.Text = "Calculating measures... Please wait..."


        Try
            ''Added by Mayuri:
            Dim checkval As C1.Win.C1FlexGrid.CheckEnum

            Dim dt As DataTable = New DataTable()
            Dim ds As New DataSet
            Dim dsData As New DataSet



            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0002, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0002"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0002")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0002 + 2, COL_DPatientID, Nothing)
            End If
            'Me.Label6.Text = "Calculating NQF 004 Population 1, Numerator 1"
            'Application.DoEvents()
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0004, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0004 Population-1"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_0004("MU_NQF004_Pop1Num1")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Count1")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Denomenator, dsData.Tables("Count1").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Numeratot, dsData.Tables("Count1").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Exclusion, dsData.Tables("Count1").Rows(0)(2).ToString())
                    If dsData.Tables("Count1").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator1")) = False Then
                    If dsData.Tables("Denominator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_DPatientID, dsData.Tables("Denominator1").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator1")) = False Then
                    If dsData.Tables("Numerator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_NPatientID, dsData.Tables("Numerator1").Rows(0)(0).ToString())
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator2")) = False Then
                    If dsData.Tables("Denominator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_DPatientID, dsData.Tables("Denominator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator2")) = False Then
                    If dsData.Tables("Numerator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_NPatientID, dsData.Tables("Numerator2").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_DPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_DPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 3, COL_NPatientID, Nothing)
            End If



            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0004, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0004 Population-2 Numerator-1"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_0004("MU_NQF004_Pop2Num1")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    ' C1QualityMeasures.SetData(56, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Count1")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Denomenator, dsData.Tables("Count1").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Numeratot, dsData.Tables("Count1").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Exclusion, dsData.Tables("Count1").Rows(0)(2).ToString())
                    If dsData.Tables("Count1").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator1")) = False Then
                    If dsData.Tables("Denominator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_DPatientID, dsData.Tables("Denominator1").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator1")) = False Then
                    If dsData.Tables("Numerator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_NPatientID, dsData.Tables("Numerator1").Rows(0)(0).ToString())
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator2")) = False Then
                    If dsData.Tables("Denominator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_DPatientID, dsData.Tables("Denominator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator2")) = False Then
                    If dsData.Tables("Numerator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_NPatientID, dsData.Tables("Numerator2").Rows(0)(0).ToString())
                    End If
                End If
            Else
                C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_DPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 5, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_DPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 6, COL_NPatientID, Nothing)
            End If





            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0004, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0004 Population-3 Numerator-1"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_0004("MU_NQF004_Pop3Num1")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    ' C1QualityMeasures.SetData(56, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Count1")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Denomenator, dsData.Tables("Count1").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Numeratot, dsData.Tables("Count1").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Exclusion, dsData.Tables("Count1").Rows(0)(2).ToString())
                    If dsData.Tables("Count1").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator1")) = False Then
                    If dsData.Tables("Denominator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_DPatientID, dsData.Tables("Denominator1").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator1")) = False Then
                    If dsData.Tables("Numerator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_NPatientID, dsData.Tables("Numerator1").Rows(0)(0).ToString())
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator2")) = False Then
                    If dsData.Tables("Denominator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_DPatientID, dsData.Tables("Denominator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator2")) = False Then
                    If dsData.Tables("Numerator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_NPatientID, dsData.Tables("Numerator2").Rows(0)(0).ToString())
                    End If
                End If
            Else
                C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_DPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 8, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_DPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0004 + 9, COL_NPatientID, Nothing)
            End If


            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0012, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0012"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0012")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If
            Else
                C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0012 + 2, COL_DPatientID, Nothing)
            End If


            'Me.Label6.Text = "Calculating NQF 0013"
            'Application.DoEvents()
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0013, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0013"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0013")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If

                End If
                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If
            Else
                C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0013 + 2, COL_DPatientID, Nothing)
            End If


            'Me.Label6.Text = "Calculating NQF 0014"
            'Application.DoEvents()
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0014, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0014"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0014")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0014 + 2, COL_DPatientID, Nothing)
            End If



            'Me.Label6.Text = "Calculating NQF 0024 Population 1"
            'Application.DoEvents()
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0024, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0024 Population-1"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercentforMorePopulation_ds("MU_NQF0024_POP1")

                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Count1")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_Denomenator, dsData.Tables("Count1").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_Numeratot, dsData.Tables("Count1").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_Exclusion, dsData.Tables("Count1").Rows(0)(2).ToString())
                    If dsData.Tables("Count1").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Count2")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_Denomenator, dsData.Tables("Count2").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_Numeratot, dsData.Tables("Count2").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_Exclusion, dsData.Tables("Count2").Rows(0)(2).ToString())
                    If dsData.Tables("Count2").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_Percent, dsData.Tables("Count2").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_Percent, dsData.Tables("Count2").Rows(0)(3).ToString() & "%")
                    End If
                End If


                If IsNothing(dsData.Tables("Denominator1")) = False Then
                    If dsData.Tables("Denominator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_DPatientID, dsData.Tables("Denominator1").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator1")) = False Then
                    If dsData.Tables("Numerator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 2, COL_NPatientID, dsData.Tables("Numerator1").Rows(0)(0).ToString())
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator2")) = False Then
                    If dsData.Tables("Denominator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_DPatientID, dsData.Tables("Denominator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator2")) = False Then
                    If dsData.Tables("Numerator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 3, COL_NPatientID, dsData.Tables("Numerator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator3")) = False Then
                    If dsData.Tables("Denominator3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_DPatientID, dsData.Tables("Denominator3").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator3")) = False Then
                    If dsData.Tables("Numerator3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 4, COL_NPatientID, dsData.Tables("Numerator3").Rows(0)(0).ToString())
                    End If
                End If

            Else
                For i = 0 To 2
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2 + i, COL_Denomenator, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2 + i, COL_Numeratot, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2 + i, COL_Exclusion, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2 + i, COL_Percent, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2 + i, COL_NPatientID, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 2 + i, COL_DPatientID, Nothing)
                Next

            End If

            'Me.Label6.Text = "Calculating NQF 0024 Population 2"
            'Application.DoEvents()
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0024, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0024 Population-2"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercentforMorePopulation_ds("MU_NQF0024_POP2")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Count1")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_Denomenator, dsData.Tables("Count1").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_Numeratot, dsData.Tables("Count1").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_Exclusion, dsData.Tables("Count1").Rows(0)(2).ToString())
                    If dsData.Tables("Count1").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Count2")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_Denomenator, dsData.Tables("Count2").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_Numeratot, dsData.Tables("Count2").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_Exclusion, dsData.Tables("Count2").Rows(0)(2).ToString())
                    If dsData.Tables("Count2").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_Percent, dsData.Tables("Count2").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_Percent, dsData.Tables("Count2").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator1")) = False Then
                    If dsData.Tables("Denominator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_DPatientID, dsData.Tables("Denominator1").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator1")) = False Then
                    If dsData.Tables("Numerator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 6, COL_NPatientID, dsData.Tables("Numerator1").Rows(0)(0).ToString())
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator2")) = False Then
                    If dsData.Tables("Denominator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_DPatientID, dsData.Tables("Denominator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator2")) = False Then
                    If dsData.Tables("Numerator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_NPatientID, dsData.Tables("Numerator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator3")) = False Then
                    If dsData.Tables("Denominator3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_DPatientID, dsData.Tables("Denominator3").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator3")) = False Then
                    If dsData.Tables("Numerator3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 8, COL_NPatientID, dsData.Tables("Numerator3").Rows(0)(0).ToString())
                    End If
                End If



            Else
                For i = 0 To 2
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6 + i, COL_Denomenator, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6 + i, COL_Numeratot, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6 + i, COL_Exclusion, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6 + i, COL_Percent, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6 + i, COL_NPatientID, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 6 + i, COL_DPatientID, Nothing)
                Next
            End If

            'Me.Label6.Text = "Calculating NQF 0024 Population 3"
            'Application.DoEvents()
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0024, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0024 Population-3"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercentforMorePopulation_ds("MU_NQF0024_POP3")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Count1")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_Denomenator, dsData.Tables("Count1").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_Numeratot, dsData.Tables("Count1").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_Exclusion, dsData.Tables("Count1").Rows(0)(2).ToString())
                    If dsData.Tables("Count1").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 7, COL_Percent, dsData.Tables("Count1").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Count2")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_Denomenator, dsData.Tables("Count2").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_Numeratot, dsData.Tables("Count2").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_Exclusion, dsData.Tables("Count2").Rows(0)(2).ToString())
                    If dsData.Tables("Count2").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_Percent, dsData.Tables("Count2").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_Percent, dsData.Tables("Count2").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator1")) = False Then
                    If dsData.Tables("Denominator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_DPatientID, dsData.Tables("Denominator1").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator1")) = False Then
                    If dsData.Tables("Numerator1").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 10, COL_NPatientID, dsData.Tables("Numerator1").Rows(0)(0).ToString())
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator2")) = False Then
                    If dsData.Tables("Denominator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_DPatientID, dsData.Tables("Denominator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator2")) = False Then
                    If dsData.Tables("Numerator2").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 11, COL_NPatientID, dsData.Tables("Numerator2").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator3")) = False Then
                    If dsData.Tables("Denominator3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_DPatientID, dsData.Tables("Denominator3").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator3")) = False Then
                    If dsData.Tables("Numerator3").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0024 + 12, COL_NPatientID, dsData.Tables("Numerator3").Rows(0)(0).ToString())
                    End If
                End If
            Else
                For i = 0 To 2
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10 + i, COL_Denomenator, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10 + i, COL_Numeratot, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10 + i, COL_Exclusion, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10 + i, COL_Percent, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10 + i, COL_NPatientID, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0024 + 10 + i, COL_DPatientID, Nothing)
                Next
            End If


            'Me.Label6.Text = "Calculating NQF 0028a"
            'Application.DoEvents()
            '''''Added by Mayuri:
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0028a, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0028a"
                Application.DoEvents()
                dsData = Nothing
                ''_HistoryItemList = ""
                ''_HistoryItemList = GetHistoryItems()
                dsData = CalculatePercent_ds("MU_NQF0028a")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If (dsData.Tables("Count").Rows(0)(3).ToString() = "N/A") Then
                        C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028a + 2, COL_DPatientID, Nothing)
            End If


            'Me.Label6.Text = "Calculating NQF 0028b"
            'Application.DoEvents()
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0028b, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0028b"
                Application.DoEvents()
                dsData = Nothing
                ''_NQF0028bHistoryItemList = ""
                ''_NQF0028bHistoryItemList = GetNQF0028bHistoryItems()
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0028b")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If (dsData.Tables("Count").Rows(0)(3).ToString() = "N/A") Then
                        C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0028b + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0031, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0031"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0031")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0031 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0032, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0032"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0032")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0033, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0033 Population-1"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0033_POP1")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 2, COL_DPatientID, Nothing)
            End If


            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0033, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0033 Population-2"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0033_POP2")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
            Else
                C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 4, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 4, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 4, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0033, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0033 Population-3"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0033_POP3")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If



            Else
                C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0033 + 6, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 6, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0032 + 6, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0038, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0038"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveData_NQF0038_ds("MU_NQF0038", cmbProviders.SelectedValue.ToString())
                Dim k As Int16
                Dim _rowcnt As Int16
                dt = Nothing
                dt = dsData.Tables("Count")
                If IsNothing(dt) = False Then
                    If dt.Rows.Count > 0 Then


                        k = 2
                        For _rowcnt = 0 To dt.Rows.Count - 1



                            C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Denomenator, dt.Rows(_rowcnt)(0).ToString())
                            C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Numeratot, dt.Rows(_rowcnt)(1).ToString())
                            C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Exclusion, dt.Rows(_rowcnt)(2).ToString())
                            If dt.Rows(_rowcnt)(3).ToString() = "N/A" Then
                                C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Percent, dt.Rows(_rowcnt)(3).ToString())
                            Else
                                C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Percent, dt.Rows(_rowcnt)(3).ToString() & "%")
                            End If
                            k = k + 1

                        Next
                    End If
                End If


                C1QualityMeasures.SetData(ROW_NQF0038 + 2, COL_DPatientID, dsData.Tables("Denominator1").Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_NQF0038 + 2, COL_NPatientID, dsData.Tables("Numerator1").Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_NQF0038 + 3, COL_DPatientID, dsData.Tables("Denominator2").Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_NQF0038 + 3, COL_NPatientID, dsData.Tables("Numerator2").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 4, COL_DPatientID, dsData.Tables("Denominator3").Rows(0)(0).ToString())



                C1QualityMeasures.SetData(ROW_NQF0038 + 4, COL_NPatientID, dsData.Tables("Numerator3").Rows(0)(0).ToString())


                C1QualityMeasures.SetData(ROW_NQF0038 + 5, COL_DPatientID, dsData.Tables("Denominator4").Rows(0)(0).ToString())



                C1QualityMeasures.SetData(ROW_NQF0038 + 5, COL_NPatientID, dsData.Tables("Numerator4").Rows(0)(0).ToString())


                C1QualityMeasures.SetData(ROW_NQF0038 + 6, COL_DPatientID, dsData.Tables("Denominator5").Rows(0)(0).ToString())



                C1QualityMeasures.SetData(ROW_NQF0038 + 6, COL_NPatientID, dsData.Tables("Numerator5").Rows(0)(0).ToString())


                C1QualityMeasures.SetData(ROW_NQF0038 + 7, COL_DPatientID, dsData.Tables("Denominator6").Rows(0)(0).ToString())



                C1QualityMeasures.SetData(ROW_NQF0038 + 7, COL_NPatientID, dsData.Tables("Numerator6").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 8, COL_DPatientID, dsData.Tables("Denominator7").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 8, COL_NPatientID, dsData.Tables("Numerator7").Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_NQF0038 + 9, COL_DPatientID, dsData.Tables("Denominator8").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 9, COL_NPatientID, dsData.Tables("Numerator8").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 10, COL_DPatientID, dsData.Tables("Denominator9").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 10, COL_NPatientID, dsData.Tables("Numerator9").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 11, COL_DPatientID, dsData.Tables("Denominator10").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 11, COL_NPatientID, dsData.Tables("Numerator10").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 12, COL_DPatientID, dsData.Tables("Denominator11").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 12, COL_NPatientID, dsData.Tables("Numerator11").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 13, COL_DPatientID, dsData.Tables("Denominator12").Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_NQF0038 + 13, COL_NPatientID, dsData.Tables("Numerator12").Rows(0)(0).ToString())

            Else
                For k = 2 To 13


                    C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Denomenator, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Numeratot, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Exclusion, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_Percent, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_NPatientID, Nothing)
                    C1QualityMeasures.SetData(ROW_NQF0038 + k, COL_DPatientID, Nothing)
                Next
            End If



            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0041, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0041"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveData_NQF0041_ds("MU_NQF0041", cmbProviders.SelectedValue.ToString())
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_DPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0041 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0055, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0055"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0055")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0055 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0056, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0056"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0056")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0056 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0059, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0059"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0059")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0059 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0061, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0061"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0061")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0061 + 2, COL_DPatientID, Nothing)
            End If

            'shubhangi
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0062, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0062"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0062")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0062 + 2, COL_DPatientID, Nothing)
            End If

            ' SHUBHANGI 20101013 INTEGRATE 70
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0064, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0064 Numerator-1"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0064_Numerator1")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 2, COL_DPatientID, Nothing)

            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0064, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0064 Numerator-2"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0064_Numerator2")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0064 + 3, COL_DPatientID, Nothing)

            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0067, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0067"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0067")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0067 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0068, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0068"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0068")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0068 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0070, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0070"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0070")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0070 + 2, COL_DPatientID, Nothing)
            End If

            'Me.Label6.Text = "Calculating " & "Measure NQF0073"
            'Application.DoEvents()
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF0073")
            'If IsNothing(dt) = False Then
            '    C1QualityMeasures.SetData(ROW_NQF0073 + 2, COL_Denomenator, dt.Rows(0)(0).ToString())
            '    C1QualityMeasures.SetData(ROW_NQF0073 + 2, COL_Numeratot, dt.Rows(0)(1).ToString())
            '    C1QualityMeasures.SetData(ROW_NQF0073 + 2, COL_Exclusion, dt.Rows(0)(2).ToString())
            '    If dt.Rows(0)(3).ToString() = "N/A" Then
            '        C1QualityMeasures.SetData(ROW_NQF0073 + 2, COL_Percent, dt.Rows(0)(3).ToString())
            '    Else
            '        C1QualityMeasures.SetData(ROW_NQF0073 + 2, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            '    End If
            'End If

            ' SHUBHANGI 20101014 TO INTEGRATE 74
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0074, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0074"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0074")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0074 + 2, COL_DPatientID, Nothing)
            End If


            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0075, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0075 Numerator-1"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0075_Numerator1")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0075, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0075 Numerator-2"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0075_Numerator2")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0075 + 3, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0081, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0081"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0081")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If


            Else
                C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0083, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0083"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0083")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dt.Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0083 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0081 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0084, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0084"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0084")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0084 + 2, COL_DPatientID, Nothing)
            End If

            'Me.Label6.Text = "Calculating " & "Measure NQF0086"
            'Application.DoEvents()
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF0086")
            'If IsNothing(dt) = False Then
            '    C1QualityMeasures.SetData(ROW_NQF0086 + 2, COL_Denomenator, dt.Rows(0)(0).ToString())
            '    C1QualityMeasures.SetData(ROW_NQF0086 + 2, COL_Numeratot, dt.Rows(0)(1).ToString())
            '    C1QualityMeasures.SetData(ROW_NQF0086 + 2, COL_Exclusion, dt.Rows(0)(2).ToString())
            '    If dt.Rows(0)(3).ToString() = "N/A" Then
            '        C1QualityMeasures.SetData(ROW_NQF0086 + 2, COL_Percent, dt.Rows(0)(3).ToString())
            '    Else
            '        C1QualityMeasures.SetData(ROW_NQF0086 + 2, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            '    End If
            'End If

            'Me.Label6.Text = "Calculating " & "Measure NQF0088"
            'Application.DoEvents()
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF0088")
            'If IsNothing(dt) = False Then
            '    C1QualityMeasures.SetData(ROW_NQF0088 + 2, COL_Denomenator, dt.Rows(0)(0).ToString())
            '    C1QualityMeasures.SetData(ROW_NQF0088 + 2, COL_Numeratot, dt.Rows(0)(1).ToString())
            '    C1QualityMeasures.SetData(ROW_NQF0088 + 2, COL_Exclusion, dt.Rows(0)(2).ToString())
            '    If dt.Rows(0)(3).ToString() = "N/A" Then
            '        C1QualityMeasures.SetData(ROW_NQF0088 + 2, COL_Percent, dt.Rows(0)(3).ToString())
            '    Else
            '        C1QualityMeasures.SetData(ROW_NQF0088 + 2, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            '    End If
            'End If
            dsData = Nothing
            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0421, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0421 Population-1"
                Application.DoEvents()
                dsData = CalculatePercent_ds("MU_NQF0421")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If
                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If

                End If
                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If

                End If
            Else
                C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 2, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0421, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0421 Population-2"
                Application.DoEvents()
                dsData = Nothing
                dsData = CalculatePercent_ds("MU_NQF0421_POP2")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If



            Else
                C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0421 + 4, COL_DPatientID, Nothing)
            End If

            checkval = C1QualityMeasures.GetCellCheck(ROW_NQF0575, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Me.Label6.Text = "Calculating " & "Measure NQF0575"
                Application.DoEvents()
                dsData = Nothing
                dsData = RetriveDataFromMultipleServer_ds("MU_NQF0575")
                If IsNothing(dsData.Tables("Count")) = False Then
                    C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Denomenator, dsData.Tables("Count").Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Numeratot, dsData.Tables("Count").Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Exclusion, dsData.Tables("Count").Rows(0)(2).ToString())
                    If dsData.Tables("Count").Rows(0)(3).ToString() = "N/A" Then
                        C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString())
                    Else
                        C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Percent, dsData.Tables("Count").Rows(0)(3).ToString() & "%")
                    End If
                End If

                If IsNothing(dsData.Tables("Denominator")) = False Then
                    If dsData.Tables("Denominator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_DPatientID, dsData.Tables("Denominator").Rows(0)(0).ToString())
                    End If
                End If

                If IsNothing(dsData.Tables("Numerator")) = False Then
                    If dsData.Tables("Numerator").Rows.Count > 0 Then
                        C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_NPatientID, dsData.Tables("Numerator").Rows(0)(0).ToString())
                    End If
                End If

            Else
                C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Denomenator, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Numeratot, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Exclusion, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_Percent, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_NPatientID, Nothing)
                C1QualityMeasures.SetData(ROW_NQF0575 + 2, COL_DPatientID, Nothing)
            End If

            tlbbtn_Close.Enabled = True
            tlbbtn_DeelectAll.Enabled = True
            tlbbtn_Export.Enabled = True
            tlbbtn_SelectAll.Enabled = True
            tsp_QRDAIII.Enabled = True
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportReport, "Report Exported", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''added for Bug 110311 
        Catch ex As Exception
            tlbbtn_Close.Enabled = True
            tlbbtn_DeelectAll.Enabled = True
            tlbbtn_Export.Enabled = True
            tlbbtn_SelectAll.Enabled = True
            tsp_QRDAIII.Enabled = True
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        pnlLoadingLable.Visible = False
        ''pnlLoadingLable.BringToFront()
    End Sub


    Private Function RetriveSessionCQMFilters() As DataTable

        Dim dt As New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)

        Try

            oDB.Connect(False)
            oDB.Retrive("gsp_GET_CQMFilters", dt)
            oDB.Disconnect()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing

        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

    Private Sub SetCQMFilters()

        Dim dtFilters As New DataTable()
        dtFilters = RetriveSessionCQMFilters()

        If Not IsNothing(dtFilters) And dtFilters.Rows.Count > 0 Then
            dtFilters.Rows.Clear()
        End If

        If Not IsNothing(dtFilters) Then
          
            If txtNPI.Text <> "" Then
                Dim dRow As DataRow
                dRow = dtFilters.NewRow()
                dRow("FilterType") = enum_CQMFilters.NPI
                dRow("FilterValue") = Convert.ToString(txtNPI.Text)
                dtFilters.Rows.Add(dRow)
                dRow = Nothing
            End If

            If txtTIN.Text <> "" Then
                Dim dRow As DataRow
                dRow = dtFilters.NewRow()
                dRow("FilterType") = enum_CQMFilters.TIN
                dRow("FilterValue") = Convert.ToString(txtTIN.Text)
                dtFilters.Rows.Add(dRow)
                dRow = Nothing
            End If

            If cmbRace.Items.Count > 0 Then
                For i As Integer = 0 To cmbRace.Items.Count - 1 Step 1
                    Dim dRow As DataRow
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.Race
                    dRow("FilterValue") = Convert.ToString(cmbRace.Items(i))
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                Next
            End If

            If cmbethnicity.Items.Count > 0 Then
                For i As Integer = 0 To cmbethnicity.Items.Count - 1 Step 1
                    Dim dRow As DataRow
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.Ethnicity
                    dRow("FilterValue") = Convert.ToString(cmbethnicity.Items(i))
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                Next
            End If

            If cmbGender.Items.Count > 0 Then
                For i As Integer = 0 To cmbGender.Items.Count - 1 Step 1
                    Dim dRow As DataRow
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.Gender
                    dRow("FilterValue") = Convert.ToString(cmbGender.Items(i))
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                Next
            End If

            If cmbPayers.Items.Count > 0 Then
                For i As Integer = 0 To cmbPayers.Items.Count - 1 Step 1
                    Dim dRow As DataRow
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.Payer
                    dRow("FilterValue") = Convert.ToString(cmbPayers.Items(i))
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                Next
            End If

            If cmbProblems.Items.Count > 0 Then
                For i As Integer = 0 To cmbProblems.Items.Count - 1 Step 1
                    Dim dRow As DataRow
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.Problem
                    dRow("FilterValue") = Convert.ToString(cmbProblems.Items(i))
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                Next
            End If
            If CmbProviderTaxanomy.Items.Count > 0 Then
                For i As Integer = 0 To CmbProviderTaxanomy.Items.Count - 1 Step 1
                    Dim dRow As DataRow
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.ProviderTaxonomy
                    dRow("FilterValue") = Convert.ToString(CmbProviderTaxanomy.Items(i))
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                Next
            End If
            If chkAge.Checked = True Then
                Dim dRow As DataRow
                dRow = dtFilters.NewRow()
                dRow("FilterType") = enum_CQMFilters.Age
                Dim AgeCriteria As String
                If cmbAgeCriteria.SelectedItem = "Maximum" Then
                    AgeCriteria = "<="
                ElseIf cmbAgeCriteria.SelectedItem = "Minimum" Then
                    AgeCriteria = ">="
                Else
                    AgeCriteria = "="
                End If
                Dim ageFilter As String = dtpicAge.Value.Date.ToString("MM/dd/yyyy") + "~" + AgeCriteria + "~" + cmbAge.SelectedItem
                dRow("FilterValue") = ageFilter
                dtFilters.Rows.Add(dRow)
                dRow = Nothing
            End If

            If chkPracticeAddress.Checked = True Then
                Dim dRow As DataRow
                If oAddresscontrol.txtAddress1.Text.Trim() <> "" Then
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.PracticeAddressLine1
                    dRow("FilterValue") = oAddresscontrol.txtAddress1.Text.Trim()
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                End If

                If oAddresscontrol.txtAddress2.Text.Trim() <> "" Then
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.PracticeAddressLine2
                    dRow("FilterValue") = oAddresscontrol.txtAddress2.Text.Trim()
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                End If

                If oAddresscontrol.txtCity.Text.Trim() <> "" Then
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.PracticeCity
                    dRow("FilterValue") = oAddresscontrol.txtCity.Text.Trim()
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                End If

                If oAddresscontrol.cmbState.Text.Trim() <> "" Then
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.PracticeState
                    dRow("FilterValue") = oAddresscontrol.cmbState.Text.Trim()
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                End If

                If oAddresscontrol.txtZip.Text.Trim() <> "" Then
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.PracticeZIP
                    dRow("FilterValue") = oAddresscontrol.txtZip.Text.Trim()
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                End If

                If oAddresscontrol.cmbCountry.Text.Trim() <> "" Then
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.PracticeCountry
                    dRow("FilterValue") = oAddresscontrol.cmbCountry.Text.Trim()
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                End If

                If oAddresscontrol.txtCounty.Text.Trim() <> "" Then
                    dRow = dtFilters.NewRow()
                    dRow("FilterType") = enum_CQMFilters.PracticeCounty
                    dRow("FilterValue") = oAddresscontrol.txtCounty.Text.Trim()
                    dtFilters.Rows.Add(dRow)
                    dRow = Nothing
                End If
            End If
        End If


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        oDB.Connect(False)

        Dim dt As New DataTable
        Dim ds As New DataSet

        If Not IsNothing(dtFilters) Then
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@FilterTVP"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtFilters
            oParameters.Add(oParameter)
            oParameter = Nothing
        End If

        oDB.Execute("gsp_INUP_CQMFilters", oParameters)
        oDB.Disconnect()



        If IsNothing(oParameter) = False Then
            oParameter.Dispose()
            oParameter = Nothing
        End If

        If IsNothing(oParameters) = False Then
            oParameters.Dispose()
            oParameters = Nothing
        End If

        If IsNothing(oDB) = False Then
            oDB.Dispose()
            oDB = Nothing
        End If

    End Sub

    Private Function validateCQMFilters() As Boolean
        Dim result As Boolean = True
        If chkAge.Checked = True Then
            If cmbAgeCriteria.Text = "" Then
                MessageBox.Show("Please select age criteria.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeCriteria.Focus()
                Return False
            End If

            If cmbAge.Text = "" Then
                MessageBox.Show("Please Select age.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAge.Focus()
                Return False
            End If
        End If
        Return result
    End Function

    Private Sub tlsbtnShowReportList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnShowReportList.Click

        Try
            Me.Enabled = False
            cmbProviders.Enabled = False
            chkCQM2014.Enabled = False
            dtpicStartDate.Enabled = False
            dtpicEndDate.Enabled = False
            tlsbtnShowReportList.Enabled = False
            tsp_ExportQRDAI.Enabled = False
            tsp_QRDAIII.Enabled = False
            tlbbtn_Print.Enabled = False

            If validateCQMFilters() = False Then
                Exit Sub
            End If

            SetCQMFilters()

            If chkCQM2014.Checked = True Then
                If (CheckGridCount()) Then
                    GenerateReport_CQM2014()
                End If
                tlbbtn_Export.Enabled = False

                If PatientID = 0 Then
                    tlbbtn_Print.Enabled = True
                End If

            Else
                GenerateReport()
                tlbbtn_Export.Enabled = True
                If PatientID = 0 Then
                    tlbbtn_Print.Enabled = True
                End If
            End If
        Catch ex1 As System.Net.WebException
            MessageBox.Show("Unable to connect to GSSD server.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If PatientID = 0 Then
                chkCQM2014.Enabled = True
            End If

            cmbProviders.Enabled = True
            dtpicStartDate.Enabled = True
            dtpicEndDate.Enabled = True
            tlsbtnShowReportList.Enabled = True
            tsp_ExportQRDAI.Enabled = True
            tsp_QRDAIII.Enabled = True
            If PatientID = 0 Then
                tlbbtn_Print.Enabled = True
            End If
            Me.WindowState = FormWindowState.Maximized
            Me.Enabled = True


        End Try
    End Sub

    Private Function CheckGridCount() As Integer
        Dim checkval As C1.Win.C1FlexGrid.CheckEnum
        For cnt As Integer = 0 To C1QualityMeasures.Rows.Count - 1
            checkval = C1QualityMeasures.GetCellCheck(cnt, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Return True
                Exit For

            End If
        Next

        Return False
    End Function

    Private Sub tlbbtn_Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Export.Click
        GenerateXML()
    End Sub

    Public Sub GenerateXML()

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim ds As New DataSet
        Dim dt As New DataTable

        Try

            If IsReportGenerated() Then



                Dim oClsCQM As New cls_MU_CQMSubmission
                SaveFileDialogXML.FileName = Nothing
                SaveFileDialogXML.Filter = "XML Files (*.xml)|*.xml|XSL Files (*.xsl)|*.xsl|All files(*.*)|*.*"
                If SaveFileDialogXML.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    oClsCQM.FilePath = SaveFileDialogXML.FileName
                Else
                    'MessageBox.Show("Select the file path", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                If File.Exists(Application.StartupPath & "\CQMXMLDefaultValue.xml") Then
                    ds.ReadXml(Application.StartupPath & "\CQMXMLDefaultValue.xml")
                End If


                If ds.Tables.Count > 0 Then

                    oClsCQM.CreateBy = ds.Tables(0).Rows(0)(0).ToString()
                    oClsCQM.Version = ds.Tables(0).Rows(0)(1).ToString()
                    oClsCQM.FileNumber = ds.Tables(0).Rows(0)(2).ToString()
                    oClsCQM.NumberOfFiles = ds.Tables(0).Rows(0)(3).ToString()
                    oClsCQM.RegistryName = ds.Tables(1).Rows(0)(0).ToString()
                    oClsCQM.RegistryID = ds.Tables(1).Rows(0)(1).ToString()
                    oClsCQM.SubmissionMethod = ds.Tables(1).Rows(0)(2).ToString()
                    oClsCQM.MeasureGroupID = ds.Tables(2).Rows(0)(1).ToString()
                    oClsCQM.WaiverSign = ds.Tables(3).Rows(0)(0).ToString()
                Else
                    oClsCQM.CreateBy = "gloEMR"
                    oClsCQM.Version = "1.0"
                    oClsCQM.FileNumber = 1
                    oClsCQM.NumberOfFiles = 1
                    oClsCQM.RegistryName = "gloEMR"
                    oClsCQM.RegistryID = 0
                    oClsCQM.SubmissionMethod = "A"
                    oClsCQM.MeasureGroupID = "X"
                    oClsCQM.WaiverSign = "y"
                End If
                oClsCQM.CreateDate = Today()
                oClsCQM.CreateTime = TimeOfDay()

                'dt = ds.Tables(1)


                oDB.Connect(False)
                oDB.Retrive_Query("select CASE sNPI WHEN sNPI THEN sNPI ELSE '' END from Provider_mst where nProviderID=" & cmbProviders.SelectedValue, dt)
                oDB.Disconnect()
                If dt.Rows.Count > 0 Then
                    oClsCQM.NPI = dt.Rows(0)(0)
                Else
                    oClsCQM.NPI = "000000000"
                End If
                dt = Nothing
                oDB.Connect(False)
                ''Sanjog 2011 Jan 14
                oDB.Retrive_Query("select CASE WHEN len(sTAXID)>0 THEN sTAXID ELSE '0' END from clinic_mst where nclinicId=" & _ClinicID, dt)
                ''Sanjog 2011 Jan 14
                oDB.Disconnect()
                If dt.Rows.Count > 0 Then
                    oClsCQM.TIN = dt.Rows(0)(0)
                Else
                    oClsCQM.TIN = 0
                End If

                oClsCQM.EnCounterFromDate = dtpicStartDate.Value
                oClsCQM.EnCounterToDate = dtpicEndDate.Value

                Dim oMeasures As New Measures
                Dim j As Integer
                Dim k As Integer
                Dim strCQM As String
                Dim cnt As Int64 = 0
                Dim cntrcrd As Int64 = 0
                For i = 1 To C1QualityMeasures.Rows.Count - 1
                    Dim checkval As C1.Win.C1FlexGrid.CheckEnum
                    checkval = C1QualityMeasures.GetCellCheck(i, 0)
                    If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        cnt = cnt + 1
                        k = 0
                        For j = i + 1 To C1QualityMeasures.Rows.Count - 1
                            checkval = C1QualityMeasures.GetCellCheck(j, 0)
                            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Or checkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                                k = j
                                Exit For
                            End If
                        Next
                        If k = 0 Then
                            k = C1QualityMeasures.Rows.Count
                        End If
                        For j = i To k - 1
                            If Convert.ToString(C1QualityMeasures.GetData(j, COL_Denomenator)) <> "" Then
                                cntrcrd = cntrcrd + 1
                                Dim oMeasure As New Measure
                                strCQM = Convert.ToString(Convert.ToString(C1QualityMeasures.GetData(i, COL_CoreMeasure)).Substring(0, 8))

                                oDB.Connect(False)
                                oDB.Retrive_Query("select PQRI_NUMBER from PQRImapping where NQF_Number='" & strCQM & "'", dt)
                                oDB.Disconnect()
                                If dt.Rows.Count > 0 Then
                                    oMeasure.PQRIMEasureNumber = Convert.ToUInt64(Convert.ToString(dt.Rows(0)(0)).Trim())
                                Else
                                    oMeasure.PQRIMEasureNumber = 0
                                End If
                                Dim Percent As Single
                                oMeasure.EligibleInstances = Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Denomenator))
                                oMeasure.MeetPerformanceInstances = Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Numeratot))
                                If Convert.ToString(C1QualityMeasures.GetData(j, COL_Exclusion)) = "N/A" Then
                                    oMeasure.PerformanceExclusionInstances = 0
                                    oMeasure.PerformanceNotMetInstances = Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Denomenator)) - Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Numeratot))
                                    If Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Denomenator)) = 0 Then
                                        oMeasure.ReportingRate = 0
                                        oMeasure.PerformanceRate = 0
                                    Else
                                        Percent = (Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Numeratot) + oMeasure.PerformanceNotMetInstances) / Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Denomenator))) * 100
                                        oMeasure.ReportingRate = FormatNumber(Percent, 2, TriState.True)
                                        'Percent = (oMeasure.MeetPerformanceInstances / (oMeasure.MeetPerformanceInstances - oMeasure.PerformanceExclusionInstances)) * 100
                                        Percent = (oMeasure.MeetPerformanceInstances / (oMeasure.EligibleInstances)) * 100
                                        oMeasure.PerformanceRate = FormatNumber(Percent, 2, TriState.True)
                                        If oMeasure.PerformanceRate.ToString() = "NaN" Or oMeasure.PerformanceRate.ToString() = "Infinity" Or oMeasure.PerformanceRate.ToString() = "0.00" Then
                                            oMeasure.PerformanceRate = 0
                                        End If
                                    End If
                                Else
                                    oMeasure.PerformanceExclusionInstances = Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Exclusion))
                                    oMeasure.PerformanceNotMetInstances = Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Denomenator)) - (Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Numeratot)) + Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Exclusion)))
                                    If Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Denomenator)) = 0 Then
                                        oMeasure.ReportingRate = 0
                                        oMeasure.PerformanceRate = 0
                                    Else
                                        Percent = (Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Numeratot) + oMeasure.PerformanceNotMetInstances + Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Exclusion))) / Convert.ToInt64(C1QualityMeasures.GetData(j, COL_Denomenator))) * 100
                                        oMeasure.ReportingRate = FormatNumber(Percent, 2, TriState.True)
                                        Percent = (oMeasure.MeetPerformanceInstances / (oMeasure.EligibleInstances)) * 100
                                        oMeasure.PerformanceRate = FormatNumber(Percent, 2, TriState.True)
                                        If oMeasure.PerformanceRate.ToString() = "NaN" Or oMeasure.PerformanceRate.ToString() = "Infinity" Or oMeasure.PerformanceRate.ToString() = "0.00" Then
                                            oMeasure.PerformanceRate = 0
                                        End If
                                        ''oMeasure.ReportingRate = 
                                    End If
                                End If
                                'oMeasure.PerformanceRate = "10"
                                oMeasures.Add(oMeasure)
                            End If
                        Next
                        oClsCQM.Measures = oMeasures
                    Else

                    End If
                Next
                If cnt = 0 Then
                    MessageBox.Show("Select at least one report", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                ElseIf cntrcrd = 0 Then
                    MessageBox.Show("There is no record to save", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                Else
                    Dim ocls_MU_Measures As New cls_MU_Measures
                    ocls_MU_Measures.GenerateXML(oClsCQM)
                End If

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportReport, "Report Exported", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

        Finally

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Sub

    Private Function IsReportGenerated() As Boolean
        Try
            Dim i As Int32
            Dim j As Int32
            Dim K As Int32
            Dim chkCnt As Int32 = 0
            Dim rptCnt As Int32 = 0
            Dim checkval As C1.Win.C1FlexGrid.CheckEnum
            For i = 0 To C1QualityMeasures.Rows.Count - 1
                checkval = C1QualityMeasures.GetCellCheck(i, 0)
                If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked And C1QualityMeasures.Rows(i).Visible = True Then
                    chkCnt = chkCnt + 1
                    For j = i + 1 To C1QualityMeasures.Rows.Count - 1
                        checkval = C1QualityMeasures.GetCellCheck(j, 0)
                        If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Or checkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                            K = j
                            Exit For
                        End If
                        If j = C1QualityMeasures.Rows.Count - 1 Then
                            K = C1QualityMeasures.Rows.Count - 1
                        End If
                    Next

                    For j = i To K
                        If chkCQM2014.Checked Then
                            If C1QualityMeasures.GetData(j, COL_Denomenator_2014) <> "" Then
                                rptCnt = rptCnt + 1
                                Exit For
                            End If
                        Else
                            If C1QualityMeasures.GetData(j, COL_Denomenator) <> "" Then
                                rptCnt = rptCnt + 1
                                Exit For
                            End If
                        End If

                    Next
                End If
            Next
            If chkCnt = 0 Then
                MessageBox.Show("Select At least One Report", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            Else
                If chkCnt = rptCnt Then
                    ''report is generated
                Else
                    ''some report is Not generated 
                    MessageBox.Show("Some of the selected measures are not calculated.  Please Calculate all measures before Exporting.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub C1QualityMeasures_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1QualityMeasures.MouseMove

        Try
            If (chkCQM2014.Checked = True) Then
                If C1QualityMeasures.MouseCol = Col_Icon_2014 Then
                    If Not IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.MouseRow, Col_Icon_2014)) Then
                        C1SuperTooltip1.SetToolTip(C1QualityMeasures, "The minimum patients required for a measure is 20. Select another measure whose denominator count is at least 20.")
                    End If
                End If
            End If

            If C1QualityMeasures.MouseCol = COL_CoreMeasure Or C1QualityMeasures.MouseCol = COL_CoreMeasure_2014 Then
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
            End If
        Catch ex As Exception
            gloC1FlexStyle.ShowToolTip(Nothing, sender, e.Location)
        End Try
       
    End Sub

    Private Sub GetValues()
        ProviderID = cmbProviders.SelectedValue.ToString()

        'If chk_Firs.Checked = True Then
        StartDate = dtpicStartDate.Value.ToShortDateString()
        Enddate = dtpicEndDate.Value.ToShortDateString
        'Else
        '    If cmb_RptYear.Text <> "" Then
        '        StartDate = New DateTime(cmb_RptYear.Text, 1, 1).ToShortDateString()
        '        Enddate = New DateTime(cmb_RptYear.Text, 12, 31).ToShortDateString()
        '    Else
        'StartDate = New DateTime(Date.Now.Year, 1, 1).ToShortDateString()
        'Enddate = New DateTime(Date.Now.Year, 12, 31).ToShortDateString()
        '  End If
        'End If
    End Sub

    Private Sub C1QualityMeasures_DoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1QualityMeasures.DoubleClick

        Try
            Dim dvQRDA1Patientlist As DataView = Nothing
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1QualityMeasures.HitTest(ptPoint)
            Dim spName As String = String.Empty
            Dim measureName As String = String.Empty
            Dim denominatorName As String = String.Empty
            Dim numeratorName As String = String.Empty

            Dim strDenominatorExclusionsPatientID As String = String.Empty
            Dim strDenominatorExceptionsPatientID As String = String.Empty

            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then

                Dim ofrm As frm_CQM_Detail_Report
                ofrm = frm_CQM_Detail_Report.GetInstance()
                ofrm.UserID = _UserID  ''change
                ofrm.UserName = UserName   ''change
                Dim dsQRDA1Data As DataSet = New DataSet
                If chkCQM2014.Checked = False Then

                    If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Denomenator)) = False Then

                        Dim dtProviders As DataTable

                        measureName = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Measure_Name).ToString()
                        If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_DPatientID)) = False Then
                            denominatorName = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_DPatientID).ToString()
                        End If
                        If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_NPatientID)) = False Then
                            numeratorName = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_NPatientID).ToString()
                        End If


                        GetValues()


                        dtProviders = CType(cmbProviders.DataSource, DataTable).Copy

                        If IsNothing(dtProviders.Columns("ProviderName")) = False Then
                            dtProviders.Columns.Remove("ProviderName")
                        End If

                        If IsNothing(dtProviders.Columns("nProviderID")) = False Then
                            dtProviders.Columns("nProviderID").ColumnName = "ProviderID"
                        End If

                        ofrm.DenominatorIDS = denominatorName
                        ofrm.NemuratorIDS = numeratorName

                        ofrm.pnlExcl_Excp.Visible = False
                        ofrm.pnlMain.Dock = DockStyle.Fill

                        ofrm.ProviderID = dtProviders
                        ofrm.StartDate = StartDate
                        ofrm.EndDate = Enddate
                        ofrm.MeasureName = GetCMSNo(measureName)
                        ofrm.MdiParent = Me.MdiParent
                        ofrm.Show(dtQRDA1Data)
                        ofrm.SetReportingParameters(measureName, cmbProviders.Text, dtpicStartDate.Value, dtpicEndDate.Value)
                        ofrm.RefreshContent()

                        If PatientID = 0 Then
                            ofrm.MdiParent = Me.MdiParent
                            ofrm.Show()
                            ofrm.BringToFront()
                        Else
                            ofrm.StartPosition = FormStartPosition.CenterParent
                            ofrm.WindowState = FormWindowState.Maximized
                            ofrm.ShowDialog(Me)
                        End If

                    End If

                Else

                    If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Denomenator_2014)) = False Then
                        measureName = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Measure_Name_2014).ToString()
                        If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_DPatientID_2014)) = False Then
                            denominatorName = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_DPatientID_2014).ToString()
                        End If
                        If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_NPatientID_2014)) = False Then
                            numeratorName = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_NPatientID_2014).ToString()
                        End If

                        If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_DenominatorExclusionsPatientID_2014)) = False Then
                            strDenominatorExclusionsPatientID = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_DenominatorExclusionsPatientID_2014).ToString()
                        End If

                        If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_DenominatorExceptionsPatientID_2014)) = False Then
                            strDenominatorExceptionsPatientID = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_DenominatorExceptionsPatientID_2014).ToString()
                        End If
                        If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row, Col_MeasureID_2014)) = False Then
                            ofrm.MeasureID = C1QualityMeasures.GetData(C1QualityMeasures.Row, Col_MeasureID_2014)

                            If ofrm.MeasureID = "NQF0419" Then
                                dvQRDA1Patientlist = New DataView
                                dvQRDA1Patientlist = dtQRDAIPatientList.DefaultView
                                dvQRDA1Patientlist.RowFilter = "measureno is NULL"
                                ofrm.dtQRDA1Patientlist = dvQRDA1Patientlist.ToTable()
                            Else
                                dvQRDA1Patientlist = New DataView
                                dvQRDA1Patientlist = dtQRDAIPatientList.DefaultView
                                dvQRDA1Patientlist.RowFilter = "measureno = '" & ofrm.MeasureID & "'"
                                ofrm.dtQRDA1Patientlist = dvQRDA1Patientlist.ToTable()
                            End If


                            'dsQRDA1Data = GetQRDA1Data(C1QualityMeasures.GetData(C1QualityMeasures.Row, Col_MeasureID_2014))
                        End If
                        If IsNothing(C1QualityMeasures.GetData(C1QualityMeasures.Row - 1, COL_CoreMeasure_2014)) = False Then
                            If C1QualityMeasures.GetData(C1QualityMeasures.Row - 1, COL_CoreMeasure_2014).ToString.Contains("Population 1") Then
                                ofrm.PopulationNo = 1
                            ElseIf C1QualityMeasures.GetData(C1QualityMeasures.Row - 1, COL_CoreMeasure_2014).ToString.Contains("Population 2") Then
                                ofrm.PopulationNo = 2
                            End If

                        End If

                        GetValues()
                        ofrm.DenominatorIDS = denominatorName
                        ofrm.NemuratorIDS = numeratorName

                        ofrm.DenominatorExclusionsPatientIDs = strDenominatorExclusionsPatientID
                        ofrm.DenominatorExceptionsPatientIDs = strDenominatorExceptionsPatientID

                        Dim dtProviders As DataTable

                        dtProviders = CType(cmbProviders.DataSource, DataTable).Copy

                        If IsNothing(dtProviders.Columns("ProviderName")) = False Then
                            dtProviders.Columns.Remove("ProviderName")
                        End If

                        If IsNothing(dtProviders.Columns("nProviderID")) = False Then
                            dtProviders.Columns("nProviderID").ColumnName = "ProviderID"
                        End If

                        ofrm.ProviderID = dtProviders
                        ofrm.StartDate = StartDate
                        ofrm.EndDate = Enddate
                        ofrm.MeasureName = GetCMSNo(measureName)

                        Dim arrMeasureName As String()
                        arrMeasureName = measureName.Split(",")
                        ofrm.MdiParent = Me.MdiParent
                        ofrm.Show()
                        ofrm.SetReportingParameters(measureName, cmbProviders.Text, dtpicStartDate.Value, dtpicEndDate.Value)

                        If arrMeasureName(0).ToUpper() = "CMS68/NQF 0419: DOCUMENTATION OF CURRENT MEDICATIONS IN THE MEDICAL RECORD" Then
                            ofrm.RefreshContentVisitBased(dtVistBasedDenominator, dtVistBasedNumerator, "NQF0419")
                        Else
                            ofrm.RefreshContent()
                        End If


                        If PatientID = 0 Then
                            ofrm.MdiParent = Me.MdiParent
                            ofrm.Show()
                            ofrm.BringToFront()
                        Else
                            ofrm.StartPosition = FormStartPosition.CenterParent
                            ofrm.WindowState = FormWindowState.Maximized
                            ofrm.ShowDialog(Me)
                        End If



                    End If

                End If


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tlbbtn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_Print.Click
        If chkCQM2014.Checked = True Then
            PrintMUReport2014()
        Else
            PrintMUReport()
        End If
    End Sub

    Private Sub PrintMUReport2014()
        Dim RowNum As Integer = 1
        Dim checkval As C1.Win.C1FlexGrid.CheckEnum
        Dim blnChecked As Boolean = False

        Dim CategoryID As Integer = 0
        Dim Category As String = ""
        Dim MeasureName As String = ""

        Dim Denominator As String = ""
        Dim Numerator As String = ""
        Dim Exclusions As String = ""
        Dim Exception As String = ""
        Dim Percent As String = ""

        'Category,  MeasureName,  Denominator,  Numerator,  Exclusions,  Exception,  Percnt

        Try
            'deteting old record
            InsertMUReport2014Record("Delete")

            For RowNum = 1 To C1QualityMeasures.Rows.Count - 1

                If C1QualityMeasures.GetCellCheck(RowNum, 0).ToString = "Checked" Or C1QualityMeasures.GetCellCheck(RowNum, 0).ToString = "Unchecked" Then
                    checkval = C1QualityMeasures.GetCellCheck(RowNum, 0)
                    If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked And C1QualityMeasures.Rows(RowNum).Visible = True Then
                        blnChecked = True

                        If Not C1QualityMeasures.GetData(RowNum, 1) = Nothing Then
                            Category = C1QualityMeasures.GetData(RowNum, 1).ToString()
                        End If
                    ElseIf checkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                        blnChecked = False
                    End If
                End If

                If (blnChecked = True) Then

                    If Not IsNothing(C1QualityMeasures.GetData(RowNum, 1)) Then
                        Category = C1QualityMeasures.GetData(RowNum, 1).ToString()
                        CategoryID = RowNum
                    End If

                    If Not IsNothing(C1QualityMeasures.GetData(RowNum, COL_CoreMeasure_2014)) Then
                        MeasureName = C1QualityMeasures.GetData(RowNum, COL_CoreMeasure_2014).ToString()
                    Else
                        MeasureName = ""
                    End If

                    If Not IsNothing(C1QualityMeasures.GetData(RowNum, COL_Denomenator_2014)) Then
                        Denominator = C1QualityMeasures.GetData(RowNum, COL_Denomenator_2014).ToString()
                    Else
                        Denominator = ""
                    End If

                    If Not IsNothing(C1QualityMeasures.GetData(RowNum, COL_Numeratot_2014)) Then
                        Numerator = C1QualityMeasures.GetData(RowNum, COL_Numeratot_2014).ToString()
                    Else
                        Numerator = ""
                    End If

                    If Not IsNothing(C1QualityMeasures.GetData(RowNum, COL_Exclusion_2014)) Then
                        Exclusions = C1QualityMeasures.GetData(RowNum, COL_Exclusion_2014).ToString()
                    Else
                        Exclusions = ""
                    End If

                    If Not IsNothing(C1QualityMeasures.GetData(RowNum, COL_Exception_2014)) Then
                        Exception = C1QualityMeasures.GetData(RowNum, COL_Exception_2014).ToString()
                    Else
                        Exception = ""
                    End If

                    If Not IsNothing(C1QualityMeasures.GetData(RowNum, COL_Percent_2014)) Then
                        Percent = C1QualityMeasures.GetData(RowNum, COL_Percent_2014).ToString()
                    Else
                        Percent = ""
                    End If

                    'inserting new record for MU report
                    If (MeasureName.Trim() <> "") Then
                        InsertMUReport2014Record(CategoryID, Category, MeasureName, Denominator, Numerator, Exclusions, Exception, Percent, "Insert")
                    End If
                End If
            Next
            PrintReport()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Function GetCMSNo(ByVal CMSNO As String) As String
        Dim splstr As String()
        'splstr = CMSNO.Split("/")
        'If (splstr.Length = 1) Then
        '    splstr = CMSNO.Split(",")
        'End If
        'If (splstr.Length = 1) Then
        '    splstr = CMSNO.Split(":")
        'End If
        'If (splstr.Length > 1) Then
        '    CMSNO = splstr(0).Replace("Measure", "").Replace(":", "").Replace(" ", "").Trim()
        '    If (CMSNO.Contains(" ")) Then
        '        CMSNO = CMSNO.Substring(0, CMSNO.IndexOf(" "))
        '    End If

        'End If
        If CMSNO.Contains("0064") Then
            CMSNO = 163
        Else
            Dim indcms As Integer = CMSNO.IndexOf("CMS")
            If (indcms = -1) Then
                indcms = CMSNO.IndexOf("NQF")
            End If
            CMSNO = CMSNO.Substring(indcms, CMSNO.Length - indcms)
            splstr = CMSNO.Split("/")
            If (splstr.Length = 1) Then
                splstr = CMSNO.Split(",")
            End If
            If (splstr.Length = 1) Then
                splstr = CMSNO.Split(":")
            End If
            If (splstr.Length > 1) Then
                If ((splstr(0).ToUpper() = "CMS") Or (splstr(0).ToUpper() = "NQF")) Then
                    CMSNO = splstr(0) + splstr(1)
                Else
                    CMSNO = splstr(0)
                End If
            End If
            CMSNO = CMSNO.Replace(":", "").Replace(" ", "").Replace(",", "").Replace("CMS", "").Trim()

        End If

       
        Return CMSNO
    End Function

    Private Sub PrintMUReport()
        Dim RowNum As Integer = 2
        Dim checkval As C1.Win.C1FlexGrid.CheckEnum
        Dim blnChecked As Boolean = False
        Dim MeasureName As String = ""
        Dim subMeasureName As String = ""
        Dim DetailMeasureName As String = ""
        Dim Denominator As String = ""
        Dim Numerator As String = ""
        Dim Exclusions As String = ""
        Dim Percent As String = ""

        Try
            'deteting old record
            InsertMUReportRecord("Delete")

            For RowNum = 2 To C1QualityMeasures.Rows.Count - 1

                checkval = C1QualityMeasures.GetCellCheck(RowNum, 0)
                If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked And C1QualityMeasures.Rows(RowNum).Visible = True Then
                    blnChecked = True

                    If Not C1QualityMeasures.GetData(RowNum, 1) = Nothing Then
                        MeasureName = C1QualityMeasures.GetData(RowNum, 1).ToString()
                    End If
                ElseIf checkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                    blnChecked = False
                End If

                If (blnChecked = True) Then
                    If Not C1QualityMeasures.GetData(RowNum, 1) = Nothing Then
                        If C1QualityMeasures.GetData(RowNum, 1).ToString().Contains("Numerator") Then

                            If Not IsNothing(C1QualityMeasures.GetData(RowNum, 1)) Then
                                DetailMeasureName = C1QualityMeasures.GetData(RowNum, 1).ToString()
                            End If


                            If Not IsNothing(C1QualityMeasures.GetData(RowNum, 3)) Then
                                Denominator = C1QualityMeasures.GetData(RowNum, 3).ToString()
                            End If

                            If Not IsNothing(C1QualityMeasures.GetData(RowNum, 4)) Then
                                Numerator = C1QualityMeasures.GetData(RowNum, 4).ToString()
                            End If

                            If Not IsNothing(C1QualityMeasures.GetData(RowNum, 5)) Then
                                Exclusions = C1QualityMeasures.GetData(RowNum, 5).ToString()
                            End If

                            If Not IsNothing(C1QualityMeasures.GetData(RowNum, 6)) Then
                                Percent = C1QualityMeasures.GetData(RowNum, 6).ToString()
                            End If

                            'inserting new record for MU report
                            InsertMUReportRecord(MeasureName, subMeasureName, DetailMeasureName, Denominator, Numerator, Exclusions, Percent, "Insert")
                        Else
                            subMeasureName = C1QualityMeasures.GetData(RowNum, 1).ToString()
                        End If
                    End If
                End If
            Next
            PrintReport()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub InsertMUReportRecord(ByVal Flag As String)
        Try
            Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()
            oDB.Connect(False)
            oDBParameters.Add("@Flag", Flag, ParameterDirection.Input, SqlDbType.Text)
            oDB.Execute("MUReportInsertDelete", oDBParameters)
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDBParameters = Nothing
            oDB.Dispose()
            oDB = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    

    Private Sub InsertMUReport2014Record(ByVal Flag As String)
        Try
            Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()
            oDB.Connect(False)
            oDBParameters.Add("@Flag", Flag, ParameterDirection.Input, SqlDbType.Text)
            oDB.Execute("MUReport2014InsertDelete", oDBParameters)
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDBParameters = Nothing
            oDB.Dispose()
            oDB = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub InsertMUReportRecord(ByVal MeasureName As String, ByVal subMeasureName As String, ByVal DetailMeasureName As String, ByVal Denominator As String, ByVal Numerator As String, ByVal Exclusions As String, ByVal Percent As String, ByVal Flag As String)
        Try
            Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()
            oDB.Connect(False)
            oDBParameters.Add("@MeasureName", MeasureName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@subMeasureName", subMeasureName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@DetailMeasureName", DetailMeasureName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Denominator", Denominator, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Numerator", Numerator, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Exclusions", Exclusions, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Percnt", Percent, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Flag", Flag, ParameterDirection.Input, SqlDbType.Text)
            oDB.Execute("MUReportInsertDelete", oDBParameters)
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDBParameters = Nothing
            oDB.Dispose()
            oDB = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub InsertMUReport2014Record(CategoryID As Integer, ByVal Category As String, ByVal MeasureName As String, ByVal Denominator As String, ByVal Numerator As String, ByVal Exclusions As String, ByVal Exception As String, ByVal Percent As String, ByVal Flag As String)
        Try
            Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()
            oDB.Connect(False)
            oDBParameters.Add("@CategoryID", CategoryID, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@Category", Category, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@MeasureName", MeasureName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Denominator", Denominator, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Numerator", Numerator, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Exclusions", Exclusions, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Exception", Exception, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Percnt", Percent, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Flag", Flag, ParameterDirection.Input, SqlDbType.Text)
            oDB.Execute("MUReport2014InsertDelete", oDBParameters)
            oDB.Disconnect()
            oDBParameters.Dispose() : oDBParameters = Nothing
            oDB.Dispose() : oDB = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub PrintReport()
        Dim clsPrntRpt As gloSSRSApplication.clsPrintReport
        Dim _MessageBoxCaption As String = String.Empty
        Dim _databaseConnectionString As String = String.Empty
        Dim _LoginName As String = String.Empty
        Dim gstrSQLServerName As String = String.Empty
        Dim gstrDatabaseName As String = String.Empty
        Dim gblnSQLAuthentication As String = String.Empty
        Dim gstrSQLUserEMR As String = String.Empty
        Dim gstrSQLPasswordEMR As String = String.Empty
        Dim gblnDefaultPrinter As Boolean = False

        Try

            If appSettings("DataBaseConnectionString") IsNot Nothing Then
                If appSettings("DataBaseConnectionString") <> "" Then
                    _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                    Con = New SqlConnection(_databaseConnectionString)
                End If
            End If

            If appSettings("UserName") IsNot Nothing Then
                If appSettings("UserName") <> "" Then
                    _LoginName = Convert.ToString(appSettings("UserName"))
                End If
            End If

            If appSettings("SQLServerName") IsNot Nothing Then
                If appSettings("SQLServerName") <> "" Then
                    gstrSQLServerName = Convert.ToString(appSettings("SQLServerName"))
                End If
            End If

            If appSettings("DatabaseName") IsNot Nothing Then
                If appSettings("DatabaseName") <> "" Then
                    gstrDatabaseName = Convert.ToString(appSettings("DatabaseName"))
                End If
            End If

            If appSettings("SQLLoginName") IsNot Nothing Then
                If appSettings("SQLLoginName") <> "" Then
                    gstrSQLUserEMR = Convert.ToString(appSettings("SQLLoginName"))
                End If
            End If

            If appSettings("SQLPassword") IsNot Nothing Then
                If appSettings("SQLPassword") <> "" Then
                    gstrSQLPasswordEMR = Convert.ToString(appSettings("SQLPassword"))
                End If
            End If

            If appSettings("DefaultPrinter") IsNot Nothing Then
                If appSettings("DefaultPrinter") <> "" Then
                    gblnDefaultPrinter = Not Convert.ToBoolean(appSettings("DefaultPrinter"))
                End If
            End If

            If appSettings("WindowAuthentication") IsNot Nothing Then
                If appSettings("WindowAuthentication") <> "" Then
                    gblnSQLAuthentication = Not Convert.ToBoolean(appSettings("WindowAuthentication"))
                End If
            End If


            Dim ParameterValue As String
            Dim provname As String = String.Empty
            Try

            
            Dim dt As DataTable = cmbProviders.DataSource
            If (Not IsNothing(dt)) Then
                For Each dr As DataRow In dt.Rows
                    provname = provname & dr("ProviderName") & ": "
                Next
                If (provname.Trim().Length > 2) Then
                    provname = provname.Substring(0, provname.Length - 2)
                End If
            End If
            Catch ex As Exception

            End Try
            If (provname.Trim() = "") Then
                MessageBox.Show("Atleast one provider must be selected ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            ParameterValue = provname & "," & dtpicStartDate.Value.ToShortDateString() & "," & dtpicEndDate.Value.ToShortDateString() & "," & _LoginName

            Dim ParameterName As String
            ParameterName = "Provider,FromDate,ToDate,User"


            clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
            If chkCQM2014.Checked = True Then
                clsPrntRpt.PrintReport("rptMU2014Report", ParameterName, ParameterValue, gblnDefaultPrinter, "")
            Else
                clsPrntRpt.PrintReport("rptMUReport", ParameterName, ParameterValue, gblnDefaultPrinter, "")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkCQM2014_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If chkCQM2014.Checked = True Then
            FillGridCQM2014()
            tlbbtn_Export.Enabled = False
            If PatientID = 0 Then
                tlbbtn_Print.Enabled = True
            End If
            
            AlignDataCenterfor2014()
        Else
            FillGrid()
            tlbbtn_Export.Enabled = True
            If PatientID = 0 Then
                tlbbtn_Print.Enabled = True
            End If
        End If



    End Sub

    Private Function GenerateQRDAIII(ByVal FilePath As String) As Boolean
        Dim bReturned As Boolean = True

        Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = Nothing
        Dim mStrXMLfile As String = ""
        Dim objgloCDAWriter As gloCDAWriter = Nothing
        Dim oPatient As gloCCDLibrary.Patient = Nothing
        Dim dsClinicnProvider As DataSet = Nothing
        Dim strFilePath As String = ""
        Try
            strFilePath = FilePath
            oPatient = New gloCCDLibrary.Patient
            mPatient = New gloCCDLibrary.Patient
            mPatient = oPatient
            '  lblQRDAMsg.Visible = False

            If IsNothing(dtCount) = False AndAlso dtCount.Rows.Count > 0 Then
                If strFilePath <> "" Then

                    oCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()
                    objgloCDAWriter = New gloCDAWriter
                    objgloCDAWriter.nQRDAFileType = gloCCDSchema.CDAFileTypeEnum.QRDA3
                    objgloCDAWriter.MeasurementStartDate = dtpicStartDate.Value.ToString()
                    objgloCDAWriter.MeasurementEndDate = dtpicEndDate.Value.ToString()
                    objgloCDAWriter.dtMeasures = dtCount
                    If Not IsNothing(dtCodes) Then
                        If dtCodes.Rows.Count > 0 Then
                            objgloCDAWriter.dtCodes = dtCodes
                        End If
                    End If
                    If Not IsNothing(dtStratum1) Then
                        If dtStratum1.Rows.Count > 0 Then
                            objgloCDAWriter.dtStratum1 = dtStratum1
                        End If
                    End If
                    If Not IsNothing(dtStratum2) Then
                        If dtStratum2.Rows.Count > 0 Then
                            objgloCDAWriter.dtStratum2 = dtStratum2
                        End If
                    End If

                    Dim dtProviders As DataTable

                    dtProviders = CType(cmbProviders.DataSource, DataTable).Copy

                    If IsNothing(dtProviders.Columns("ProviderName")) = False Then
                        dtProviders.Columns.Remove("ProviderName")
                    End If

                    If IsNothing(dtProviders.Columns("nProviderID")) = False Then
                        dtProviders.Columns("nProviderID").ColumnName = "ProviderID"
                    End If

                    dsClinicnProvider = oCDADataExtraction.GetQRDAIIIInformation(dtProviders)

                    mPatient.Clinic = oCDADataExtraction.GetClinicInfo(dsClinicnProvider.Tables("Clinic"))
                    mPatient.PatientProviders = oCDADataExtraction.GetPatientProviderInfo(dsClinicnProvider.Tables("Provider"))
                    objgloCDAWriter.mPatient = mPatient
                    If Not IsNothing(dtProviders) Then
                        If dtProviders.Rows.Count > 1 Then
                            objgloCDAWriter.IsGroupReporting = True
                        Else
                            objgloCDAWriter.IsGroupReporting = False
                        End If
                    Else
                        objgloCDAWriter.IsGroupReporting = False
                    End If

                    If Not IsNothing(mPatient.PatientProviders) AndAlso mPatient.PatientProviders.Count > 0 Then
                        If DirectCast(mPatient.PatientProviders.Item(0), gloCCDLibrary.PatientProvider).ProvTaxID = "" Then
                            MessageBox.Show("Please select Tax Identifier Number.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            bReturned = False
                        End If
                    End If

                    mStrXMLfile = objgloCDAWriter.GenerateQRDAIII(strFilePath, "")
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportReport, "QRDA Category III file exported successfully", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            Else
                MessageBox.Show("Select and generate At least One Measure", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                bReturned = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtStratum1) Then
                dtStratum1.Dispose()
                dtStratum1.Dispose()
            End If
            If Not IsNothing(dtStratum2) Then
                dtStratum2.Dispose()
                dtStratum2.Dispose()
            End If
            If Not IsNothing(oCDADataExtraction) Then
                oCDADataExtraction.Dispose()
                oCDADataExtraction = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If
            If Not IsNothing(oPatient) Then
                oPatient.Dispose()
                oPatient = Nothing
            End If
            If Not IsNothing(dsClinicnProvider) Then
                dsClinicnProvider.Dispose()
                dsClinicnProvider = Nothing
            End If
            If Not IsNothing(mPatient) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If
        End Try

        Return bReturned
    End Function

    Private Sub tsp_QRDAIII_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsp_QRDAIII.Click
        Dim strFilePath As String = ""

        Try
            If dtpicStartDate.Value.Year = 2018 AndAlso dtpicEndDate.Value.Year = 2018 Then
                If IsReportGenerated() Then
                    SaveFileDialogXML.FileName = Nothing
                    SaveFileDialogXML.Filter = "XML Files (*.xml)|*.xml|XSL Files (*.xsl)|*.xsl|All files(*.*)|*.*"
                    SaveFileDialogXML.OverwritePrompt = True
                    If SaveFileDialogXML.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                        strFilePath = SaveFileDialogXML.FileName
                        If Me.GenerateQRDAIII(strFilePath) Then
                            lblQRDAMsg.Text = "QRDA Category III file" & " ‘" & strFilePath & "’ " & "Generated successfully. "
                            MessageBox.Show("QRDA Category III file exported successfully. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            Else
                MessageBox.Show("Please select the 2018 Reporting Period.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tsp_ExportQRDAI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsp_ExportQRDAI.Click

        Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = Nothing
        Dim mStrXMLfile As String = ""
        Dim objgloCDAWriter As gloCDAWriter = Nothing
        Dim dsClinicnProvider As DataSet = Nothing
        Dim strFilePath As String = ""

        Dim dtDistinct As New DataTable
        Dim dvQRDA1Data As New DataView
        Dim dvQRDA1MeasureWisePatients As New DataView
        Dim dtQRDA1EntryData As New DataTable
        Dim blnexpqrda As Boolean = True
        Try

            '  GenerateReport_CQM2014()
            If IsReportGenerated() Then



                If IsNothing(dtMeasureList) = False AndAlso dtMeasureList.Rows.Count > 0 Then

                    Dim strDistinctColumns() As String = {"InitialPatientPopulation", "nVisitID"}

                    dtQRDAIMeasureWisePatientList = dtQRDAIPatientList.Copy
                   
                    blnexpqrda = OpenListControl(dtQRDAIMeasureWisePatientList)
                    If (blnexpqrda = False) Then
                        Exit Sub
                    End If

                    Dim tempdtQRDAIPatientList As New DataTable
                    tempdtQRDAIPatientList = dtQRDAIMeasureWisePatientList.Clone()
                    For Each dr As DataRow In dtpat_data.Rows
                        Dim drr() As DataRow = dtQRDAIMeasureWisePatientList.Select("InitialPatientPopulation=" & dr(0).ToString() & "")
                        If (drr.Length > 0) Then
                            For Len As Integer = 0 To drr.Length - 1
                                tempdtQRDAIPatientList.ImportRow(drr(Len))
                            Next
                        End If
                    Next

                    If Not IsNothing(dtCount) AndAlso dtCount.Rows.Count > 0 Then
                        If Not IsNothing(tempdtQRDAIPatientList) AndAlso tempdtQRDAIPatientList.Rows.Count > 0 Then
                            dtQRDA1Data = GetQRDAIData(dtMeasureList, tempdtQRDAIPatientList.DefaultView.ToTable(True, strDistinctColumns), dtpicStartDate.Value, dtpicEndDate.Value)
                        End If
                    End If
                    If IsNothing(dtQRDA1Data) = False AndAlso dtQRDA1Data.Rows.Count > 0 Then

                        Dim dv As New DataView

                        dv = dtQRDA1Data.Copy().DefaultView


                        dtDistinct = dv.ToTable(True, "nPatientID")

                        If FolderBrowserDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                            strFilePath = FolderBrowserDialog1.SelectedPath & "\"
                        End If

                        If strFilePath <> "" Then

                            oCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()
                            objgloCDAWriter = New gloCDAWriter


                            If Not IsNothing(dtDistinct) AndAlso dtDistinct.Rows.Count > 0 Then
                                For i As Integer = 0 To dtDistinct.Rows.Count - 1


                                    dvQRDA1Data = dtQRDA1Data.Copy().DefaultView
                                    dvQRDA1Data.RowFilter = "nPatientID = '" & dtDistinct.Rows(i)("nPatientID") & "' "

                                    mStrXMLfile = oCDADataExtraction.GenerateQRDAIInformation(dtDistinct.Rows(i)("nPatientID"), _UserID, dtpicStartDate.Value.ToString(), dtpicEndDate.Value.ToString(), dtCount, dvQRDA1Data.ToTable, strFilePath)

                                    dvQRDA1MeasureWisePatients = tempdtQRDAIPatientList.DefaultView
                                    dvQRDA1MeasureWisePatients.RowFilter = "InitialPatientPopulation = '" & dtDistinct.Rows(i)("nPatientID") & "' AND MeasureNo <> ''"

                                    For intCount As Integer = 0 To dvQRDA1MeasureWisePatients.Count - 1

                                        If Directory.Exists(strFilePath & "\" & dvQRDA1MeasureWisePatients.Item(intCount)("MeasureNo")) = False Then
                                            Directory.CreateDirectory(strFilePath & dvQRDA1MeasureWisePatients.Item(intCount)("MeasureNo"))
                                        End If

                                        If File.Exists(mStrXMLfile) = True Then
                                            File.Copy(mStrXMLfile, strFilePath & dvQRDA1MeasureWisePatients.Item(intCount)("MeasureNo") & "\" & Path.GetFileName(mStrXMLfile))
                                        Else
                                            MessageBox.Show("Cannot access the file " & mStrXMLfile & "." & vbCrLf & "Please try again.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        End If

                                    Next



                                Next
                            End If


                            MessageBox.Show("QRDA Category I file exported successfully. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportReport, "QRDA Category I file exported successfully", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''added for Bug 110311 


                        End If
                    End If
                Else
                    MessageBox.Show("No patients found to generate QRDA Category I Report.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oCDADataExtraction) Then
                oCDADataExtraction.Dispose()
                oCDADataExtraction = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If

            If Not IsNothing(dsClinicnProvider) Then
                dsClinicnProvider.Dispose()
                dsClinicnProvider = Nothing
            End If
            If Not IsNothing(mPatient) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If

            If Not IsNothing(dtDistinct) Then
                dtDistinct.Dispose()
                dtDistinct = Nothing
            End If


            If Not IsNothing(dvQRDA1Data) Then
                dvQRDA1Data.Dispose()
                dvQRDA1Data = Nothing
            End If

            If Not IsNothing(dvQRDA1MeasureWisePatients) Then
                dvQRDA1MeasureWisePatients.Dispose()
                dvQRDA1MeasureWisePatients = Nothing
            End If

            If Not IsNothing(dtQRDA1EntryData) Then
                dtQRDA1EntryData.Dispose()
                dtQRDA1EntryData = Nothing
            End If



        End Try
    End Sub
   
    Private Function OpenListControl(ByVal dtPatdata As DataTable) As Boolean
        Try

            Dim dttemppat As DataTable = dtPatdata.Copy()
            dttemppat.Columns.RemoveAt(1)
            dttemppat.Columns.RemoveAt(1)

            Dim objfrmsrchpat As New frmSrchQrdaPatient()
            objfrmsrchpat._databaseConnectionString = _databaseConnectionString
            objfrmsrchpat.dtpatID = dttemppat
            objfrmsrchpat.ShowDialog(Me)
            If (objfrmsrchpat._Saveflag = True) Then
                dtpat_data = objfrmsrchpat.dtpatID
                Return True
            Else
                dtpat_data = dttemppat
                Return False
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function GetQRDAIData(ByVal dtMeasureList As DataTable, ByVal dtPatientList As DataTable, dtMeasurementStartDate As Date, dtMeasurementEndDate As Date) As DataTable

        Try

            Dim resultSet As List(Of gloGlobal.DIB.QRDAResult) = Nothing
            Dim resultTVP1 As DataTable = Nothing
            Dim resultTVP2 As DataTable = Nothing

            Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)

                Dim _cqmMeasure As String = "MU2_GenerateQRDAI_V2"

                If Not IsNothing(dtMeasureList) Then
                    If dtMeasureList.Rows.Count > 0 Then
                        For _measureindex As Integer = 0 To dtMeasureList.Rows.Count - 1

                            _cqmMeasure = _cqmMeasure & "," & Convert.ToString(dtMeasureList.Rows(_measureindex)(0))

                        Next
                    End If
                End If

                resultSet = ogloGSHelper.GetQRDA1CodesList(_cqmMeasure)
                If Not IsNothing(resultSet) Then
                    resultTVP1 = cls_DAL_MU_Detail_Report.ConvertToDataTable1(resultSet)

                    '28-Oct-16 Aniket: Resolving Bug #101019: NQF0033 > Error while Exporting QRDA I file for NQF0033 measure
                    resultTVP1.Columns("MPID").SetOrdinal(0)
                    resultTVP1.Columns("RxNormCodes").SetOrdinal(1)
                    resultTVP1.Columns("CQMMeasure").SetOrdinal(2)

                End If
            End Using

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            Dim dtProviders As DataTable

            dtProviders = CType(cmbProviders.DataSource, DataTable).Copy

            If IsNothing(dtProviders.Columns("ProviderName")) = False Then
                dtProviders.Columns.Remove("ProviderName")
            End If

            If IsNothing(dtProviders.Columns("nProviderID")) = False Then
                dtProviders.Columns("nProviderID").ColumnName = "ProviderID"
            End If

            oDB.Connect(False)
            Dim ds As New DataSet
            Dim dt As New DataTable
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MU2_CQMList"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtMeasureList
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MU2_InitialPatientPopulation"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtPatientList
            oParameters.Add(oParameter)
            oParameter = Nothing

           

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVP_Providers"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtProviders
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Date
            oParameter.Value = dtMeasurementStartDate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Date
            oParameter.Value = dtMeasurementEndDate
            oParameters.Add(oParameter)
            oParameter = Nothing

            If Not IsNothing(resultTVP1) Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ResultTVP1"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Structured
                oParameter.Value = resultTVP1
                oParameters.Add(oParameter)
                oParameter = Nothing
            End If
            If Not IsNothing(resultTVP2) Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ResultTVP2"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Structured
                oParameter.Value = resultTVP2
                oParameters.Add(oParameter)
                oParameter = Nothing
            End If

            oDB.Retrive("MU2_GenerateQRDAI_V2", oParameters, dt)

            oDB.Disconnect()



            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog("MU2_GenerateQRDAI_V2")
            ocls_general = Nothing

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Function GetQRDAIData() As DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)
            Dim ds As New DataSet
            Dim dt As New DataTable

            oDB.Retrive("MU2_GetQRDAICategoryEntries", oParameters, dt)

            oDB.Disconnect()



            Dim ocls_general As New cls_MU_General
            ocls_general.UpdateLog("MU2_GetQRDAICategoryEntries")
            ocls_general = Nothing

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

           
            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Private Function GetPatientDetailsData() As DataSet


        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters
        Try

            oDB.Connect(False)
            Dim dsdemographics As New DataSet

            oDB.Retrive("getPatientDetailsQualityDashboard", dsdemographics)

            oDB.Disconnect()


            Return dsdemographics

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
           
            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function


  

#Region "CQM Filters"

    Dim dsdemographics As DataSet = Nothing
    Dim _reportName As String = String.Empty
    Dim dt As DataTable = Nothing
    Dim strLst As String = String.Empty

    Private Sub Tblbtn_More_Click(sender As System.Object, e As System.EventArgs) Handles Tblbtn_More.Click
        pnlDemoFilter.Visible = True
        pnlMeasurementPeriod.SendToBack()
        pnl_tls_PtICD9CPTReport.SendToBack()
        Tblbtn_More.Visible = False
        Tblbtn_Hide.Visible = True
        If Not IsNothing(dsdemographics) Then
            dsdemographics.Tables.Clear()
            dsdemographics.Dispose()
            dsdemographics = Nothing
        End If
        dsdemographics = GetPatientDetailsData()
    End Sub

    Private Sub Tblbtn_Hide_Click(sender As System.Object, e As System.EventArgs) Handles Tblbtn_Hide.Click
        pnlDemoFilter.Visible = False
        Tblbtn_More.Visible = True
        Tblbtn_Hide.Visible = False
    End Sub

    Private Sub AddControlProv()
        If (dgCustomGridProv IsNot Nothing) Then
            RemoveControl()
        End If

        dgCustomGridProv = New gloUserControlLibrary.CustomTask()
        dgCustomGridProv.Dock = DockStyle.Fill
        dgCustomGridProv.Visible = True
        AddHandler dgCustomGridProv.OKClick, AddressOf dgCustomGridProv_OKClick
        AddHandler dgCustomGridProv.CloseClick, AddressOf dgCustomGridProv_CloseClick
        AddHandler dgCustomGridProv.SearchChanged, AddressOf dgCustomGridProv_SearchChanged
        AddHandler dgCustomGridProv.MouseMoveClick, AddressOf dgCustomGridProv_MouseMove
        pnlcustomTask.Controls.Add(dgCustomGridProv)

        Dim y As Integer = 0
        Dim x As Integer = 0


        x = cmbProviders.Location.X
        y = cmbProviders.Location.Y
        dgCustomGridProv.SetSearchTextWidth = 250

        pnlcustomTask.Location = New Point(x, y)
        pnlcustomTask.Visible = True
        pnlcustomTask.BringToFront()
        dgCustomGridProv.BringToFront()
    End Sub
    Private Sub dgCustomGridProv_MouseMove(sender As Object, e As MouseEventArgs)
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub
    Private Sub dgCustomGridProv_CloseClick(sender As Object, e As EventArgs)
        dgCustomGridProv.Visible = False
        pnlcustomTask.Visible = False
    End Sub
    Private Sub dgCustomGridProv_SearchChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView = DirectCast(dgCustomGridProv.GridDatasource, DataView)
            If dvPatient Is Nothing Then
                Me.Cursor = Cursors.[Default]
                Return
            End If

            Dim strPatientSearchDetails As String = ""
            If dgCustomGridProv.SearchText.ToString().Trim() <> "" Then
                strPatientSearchDetails = dgCustomGridProv.SearchText.ToString().Replace("'", "''")
                strPatientSearchDetails = strPatientSearchDetails.Replace("[", "") + ""
                strPatientSearchDetails = ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If


            dvPatient.RowFilter = (Convert.ToString("[" + dvPatient.Table.Columns(1).ColumnName + "]" + " Like '") & strPatientSearchDetails) + "%'  "


            dgCustomGridProv.Enabled = False
            dgCustomGridProv.datasource(dvPatient)
            dgCustomGridProv.Enabled = True
            Me.Cursor = Cursors.[Default]


            'dgCustomGrid.C1Grid.Cols(1).Width = 0
            'Else
            If dgCustomGridProv.C1Task.Cols.Count > 2 Then
                dgCustomGridProv.C1Task.Cols(1).Visible = False
            End If
            ' End If
            dgCustomGridProv.Selectsearch(gloUserControlLibrary.CustomTask.enmcontrol.Search)
        Catch objErr As Exception
            Me.Cursor = Cursors.[Default]
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.[Select], objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString(), _MessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub AddControl()
        If (dgCustomGrid IsNot Nothing) Then
            RemoveControl()
        End If

        dgCustomGrid = New gloUserControlLibrary.CustomTask()
        dgCustomGrid.Dock = DockStyle.Fill
        dgCustomGrid.Visible = True
        AddHandler dgCustomGrid.OKClick, AddressOf dgCustomGrid_OKClick
        AddHandler dgCustomGrid.CloseClick, AddressOf dgCustomGrid_CloseClick
        AddHandler dgCustomGrid.SearchChanged, AddressOf dgCustomGrid_SearchChanged
        AddHandler dgCustomGrid.MouseMoveClick, AddressOf dgCustomGrid_MouseMove
        pnlcustomTask.Controls.Add(dgCustomGrid)

        Dim y As Integer = 0
        Dim x As Integer = 0

        If strLst = "npi" Then
            x = txtNPI.Location.X()
            y = txtNPI.Location.Y + 120
            dgCustomGrid.SetSearchTextWidth = 250
        ElseIf strLst = "tin" Then
            x = txtTIN.Location.X
            y = txtTIN.Location.Y + 120
            dgCustomGrid.SetSearchTextWidth = 250
        ElseIf strLst = "race" Then
            x = cmbRace.Location.X
            y = cmbRace.Location.Y + 120
            dgCustomGrid.SetSearchTextWidth = 250
        ElseIf strLst = "insplan" Then
            x = cmbPayers.Location.X
            y = cmbPayers.Location.Y + 120
            dgCustomGrid.SetSearchTextWidth = 250
        ElseIf strLst = "ethnicity" Then
            x = cmbethnicity.Location.X
            y = cmbethnicity.Location.Y + 120
            dgCustomGrid.SetSearchTextWidth = 250
        ElseIf strLst = "diag" Then
            x = cmbProblems.Location.X
            y = cmbProblems.Location.Y + 120
            dgCustomGrid.SetSearchTextWidth = 250
        ElseIf strLst = "gender" Then
            x = cmbGender.Location.X
            y = cmbGender.Location.Y + 120
            dgCustomGrid.SetSearchTextWidth = 100
        ElseIf strLst = "Provider Type" Then
            x = CmbProviderTaxanomy.Location.X
            y = CmbProviderTaxanomy.Location.Y + 120
            dgCustomGrid.SetSearchTextWidth = 250
        End If
        pnlcustomTask.Location = New Point(x, y)
        pnlcustomTask.Visible = True
        pnlcustomTask.BringToFront()
        dgCustomGrid.BringToFront()
    End Sub

    Private Sub RemoveControl()
        If (dgCustomGrid IsNot Nothing) Then
            If pnlcustomTask.Controls.Contains(dgCustomGrid) Then
                pnlcustomTask.Controls.Remove(dgCustomGrid)
            End If
            dgCustomGrid.Visible = False
            Try
                RemoveHandler dgCustomGrid.OKClick, AddressOf dgCustomGrid_OKClick
                RemoveHandler dgCustomGrid.CloseClick, AddressOf dgCustomGrid_CloseClick
                RemoveHandler dgCustomGrid.SearchChanged, AddressOf dgCustomGrid_SearchChanged
                RemoveHandler dgCustomGrid.MouseMoveClick, AddressOf dgCustomGrid_MouseMove
                dgCustomGrid.Dispose()
                dgCustomGrid = Nothing
            Catch
            End Try
            dgCustomGrid = Nothing
        End If
    End Sub

    Private Sub HideAllControls()
        If dgCustomGrid IsNot Nothing Then
            dgCustomGrid.Visible = False
        End If
        pnlcustomTask.Visible = False
    End Sub

    Private Sub LoadUserGrid()
        Try
            AddControl()
            If (dgCustomGrid IsNot Nothing) Then
                dgCustomGrid.Visible = True
                dgCustomGrid.SetSelectAllVisible = False
                dgCustomGrid.Width = pnlcustomTask.Width
                dgCustomGrid.Height = pnlcustomTask.Height
                dgCustomGrid.C1Grid.AllowEditing = False
                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Visible = True
                dgCustomGrid.Selectsearch(gloUserControlLibrary.CustomTask.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Private Function FillCustomGridProvider(Optional ByVal ProviderID As Int64 = 0) As DataTable

        Try

            ' cmb_Provider.Items.Clear()

            Dim clsPat As New cls_MU_Measures
            Dim dt As New DataTable

            dt = clsPat.GetProviders(Convert.ToInt64(appSettings("ClinicID")), ProviderID)
            clsPat = Nothing



            Return dt

        Catch ex As Exception
            Return Nothing
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function
    Private Sub BindUserGridProv()

        Try

            Dim dt As DataTable = FillCustomGridProvider()
            Dim col As New DataColumn()

            CustomDrugsProvGridStyle()

            col.ColumnName = "Select"
            col.DataType = System.Type.[GetType]("System.Boolean")
            col.DefaultValue = False

            If Not (dt.Columns.Contains(col.ColumnName)) Then
                dt.Columns.Add(col)
            End If

            Dim dvData As New DataView(dt)

            If (dt IsNot Nothing) Then
                dgCustomGridProv.datasource(dvData)
            End If

            'RESET THE GRID
            Dim _TotalWidth As Single = dgCustomGridProv.Gridwidth - 5
            dgCustomGridProv.C1Grid.Cols.Move(dgCustomGridProv.C1Grid.Cols.Count - 1, 0)
            dgCustomGridProv.C1Grid.AllowEditing = True

            If dgCustomGridProv.C1Grid.Cols.Count > 0 Then
                dgCustomGridProv.C1Grid.Cols(0).AllowSorting = False
            End If

            dgCustomGridProv.C1Grid.Cols("ProviderName").AllowEditing = False

            dgCustomGridProv.C1Grid.Cols(1).Width = 0
            dgCustomGridProv.C1Grid.Cols(1).Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Private Sub BindUserGrid()
        Try
            Select Case strLst.ToLower()
                Case "diag"
                    If dsdemographics IsNot Nothing AndAlso dsdemographics.Tables(2) IsNot Nothing Then
                        dt = Nothing
                        dt = dsdemographics.Tables(2)
                    End If
                    Exit Select
                Case "gender"
                    If True Then
                        dt = FillGender()
                        Exit Select
                    End If
                Case "race"
                    If True Then
                        If dsdemographics IsNot Nothing AndAlso dsdemographics.Tables(1) IsNot Nothing Then
                            dt = Nothing
                            dt = dsdemographics.Tables(1)
                        End If
                        Exit Select
                    End If
                Case "ethnicity"
                    If True Then
                        If dsdemographics IsNot Nothing AndAlso dsdemographics.Tables(0) IsNot Nothing Then
                            dt = Nothing
                            dt = dsdemographics.Tables(0)
                        End If
                        Exit Select
                    End If
                Case "insplan"
                    If True Then
                        dt = FillInsurancePlan()
                        Exit Select
                    End If
                Case "npi"
                    If True Then
                        If (dsdemographics.Tables.Count > 3) Then
                            dt = Nothing
                            dt = dsdemographics.Tables(3)
                        End If
                        Exit Select
                    End If
                Case "provider type"
                    If True Then
                        If (dsdemographics.Tables.Count > 4) Then
                            dt = Nothing
                            dt = dsdemographics.Tables(4)
                        End If
                        Exit Select
                    End If
            End Select

            CustomDrugsGridStyle()
            If strLst.ToLower() <> "patientlabresults" Then
                Dim col As New DataColumn()
                col.ColumnName = "Select Data"
                col.DataType = System.Type.[GetType]("System.Boolean")
                col.DefaultValue = False

                If Not (dt.Columns.Contains(col.ColumnName)) Then
                    dt.Columns.Add(col)
                End If
            End If
            Dim dvData As New DataView(dt)

            If (dt IsNot Nothing) Then
                dgCustomGrid.datasource(dvData)
            End If

            'RESET THE GRID
            Dim _TotalWidth As Single = dgCustomGrid.Gridwidth - 5
            dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0)
            dgCustomGrid.C1Grid.AllowEditing = True

            If dgCustomGrid.C1Grid.Cols.Count > 0 Then
                dgCustomGrid.C1Grid.Cols(0).AllowSorting = False
            End If
            If strLst.ToLower() <> "patientlabresults" Then
                dgCustomGrid.C1Grid.Cols(1).AllowEditing = False
            End If

            If (strLst.ToLower()) = "insplan" Then
                dgCustomGrid.C1Grid.Cols(1).Width = 20
            Else
                If dgCustomGrid.C1Grid.Cols.Count > 2 Then
                    dgCustomGrid.C1Grid.Cols(2).Width = 20
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Public Sub CustomDrugsGridStyle()
        Dim _TotalWidth As Single = dgCustomGrid.C1Grid.Width - 5
        dgCustomGrid.C1Grid.Cols.Fixed = 0
        dgCustomGrid.C1Grid.Rows.Fixed = 1
        dgCustomGrid.C1Grid.Cols.Count = 4
        dgCustomGrid.C1Grid.AllowEditing = True
        dgCustomGrid.C1Grid.SetData(0, COL_Check, "Select")
        dgCustomGrid.C1Grid.SetData(0, 1, "Name")
        dgCustomGrid.C1Grid.SetData(0, 2, "NDCCode")
        dgCustomGrid.C1Grid.Cols(3).Width = 0
        dgCustomGrid.Height = 400
        dgCustomGrid.Width = 500
    End Sub

    Public Sub CustomDrugsProvGridStyle()
        Dim _TotalWidth As Single = dgCustomGridProv.C1Grid.Width - 5
        dgCustomGridProv.C1Grid.Cols.Fixed = 0
        dgCustomGridProv.C1Grid.Rows.Fixed = 1
        dgCustomGridProv.C1Grid.Cols.Count = 4
        dgCustomGridProv.C1Grid.AllowEditing = True
        dgCustomGridProv.C1Grid.SetData(0, COL_Check, "Select")
        dgCustomGridProv.C1Grid.SetData(0, 1, "Name")
        dgCustomGridProv.C1Grid.SetData(0, 2, "NDCCode")
        dgCustomGridProv.C1Grid.Cols(3).Width = 0
        dgCustomGridProv.Height = 400
        dgCustomGridProv.Width = 500
    End Sub
    Public Function FillInsurancePlan() As DataTable
        Dim dtDefaultTypeCode As New DataTable()

        '   dtDefaultTypeCode.Columns.Add("sCode")
        dtDefaultTypeCode.Columns.Add("Payer")

        Dim dr As DataRow = dtDefaultTypeCode.NewRow()
        ' dr("sCode") = "AP"
        dr("Payer") = "Auto Insurance Policy"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        ' dr("sCode") = "C1"
        dr("Payer") = "Commercial"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '  dr("sCode") = "CP"
        dr("Payer") = "Medicare Conditionally Primary"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        ' dr("sCode") = "GP"
        dr("Payer") = "Group Policy"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '  dr("sCode") = "HM"
        dr("Payer") = "Health Maintenance Organization (HMO)"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '  dr("sCode") = "IP"
        dr("Payer") = "Individual Policy"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '  dr("sCode") = "LD"
        dr("Payer") = "Long Term Policy"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '   dr("sCode") = "LT"
        dr("Payer") = "Litigation"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        dr("Payer") = "Medicare"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '  dr("sCode") = "MB"
        dr("Payer") = "Medicare Part B"
        dtDefaultTypeCode.Rows.Add(dr)

        'Added this on 20100330 By Sagar GHodke
        dr = dtDefaultTypeCode.NewRow()
        '  dr("sCode") = "MA"
        dr("Payer") = "Medicare Part A"
        dtDefaultTypeCode.Rows.Add(dr)
        'End add 20100330,Sagar Ghodke

        dr = dtDefaultTypeCode.NewRow()
        ' dr("sCode") = "MC"
        dr("Payer") = "Medicaid"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '   dr("sCode") = "MI"
        dr("Payer") = "Medigap Part B"
        dtDefaultTypeCode.Rows.Add(dr)


        dr = dtDefaultTypeCode.NewRow()
        '  dr("sCode") = "OT"
        dr("Payer") = "Other"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '   dr("sCode") = "PP"
        dr("Payer") = "Personal Payment (Cash - No Insurance)"
        dtDefaultTypeCode.Rows.Add(dr)

        dr = dtDefaultTypeCode.NewRow()
        '   dr("sCode") = "SP"
        dr("Payer") = "Supplemental Policy"
        dtDefaultTypeCode.Rows.Add(dr)


        dtDefaultTypeCode.AcceptChanges()
        Dim _dv As DataView = dtDefaultTypeCode.DefaultView
        _dv.Sort = "Payer"
        dtDefaultTypeCode = _dv.ToTable()
        Return dtDefaultTypeCode

    End Function

    Public Function FillGender() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Gender")
        Dim dr As DataRow = dt.NewRow()
        'Adding the new row int the Datatable
        Try
            dr("Gender") = "Male"
            dt.Rows.InsertAt(dr, 0)
            'Need to be inserted in the speicfied location
            dr = dt.NewRow()
            'Adding the new row int the Datatable
            dr("Gender") = "Female"
            dt.Rows.InsertAt(dr, 1)
            'Need to be inserted in the speicfied location
            dr = dt.NewRow()
            'Adding the new row int the Datatable
            dr("Gender") = "Other"
            dt.Rows.InsertAt(dr, 2)
            'Need to be inserted in the speicfied location
            dt.AcceptChanges()
            'After adding the row in the datatable accept the changes
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            If dr IsNot Nothing Then
                dr = Nothing
            End If
        End Try

    End Function


    Private Sub btnBrowseNPIs_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseNPIs.Click
        strLst = "npi"
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 373
        LoadUserGrid()
        SetCheckValues(txtNPI.Text)
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub btnBrowseTINs_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseTINs.Click
        strLst = "tin"
    End Sub

    Private Sub btnBrowseRace_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseRace.Click
        strLst = "race"
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 373
        LoadUserGrid()
        SetCheckValues(cmbRace)
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub btnBrowsePayers_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowsePayers.Click
        strLst = "insplan"
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 373
        LoadUserGrid()
        SetCheckValues(cmbPayers)
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub btnBrowseEthnicity_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseEthnicity.Click
        strLst = "ethnicity"
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 330
        LoadUserGrid()
        SetCheckValues(cmbethnicity)
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub btnBrowseProblems_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseProblems.Click
        strLst = "diag"
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 330
        LoadUserGrid()
        SetCheckValues(cmbProblems)
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub btnBrowseGender_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseGender.Click
        strLst = "gender"
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 200
        LoadUserGrid()
        SetCheckValues(cmbGender)
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub btnBrowseProviderType_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseProviderType.Click
        strLst = "Provider Type"
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 373
        LoadUserGrid()
        SetCheckValues(CmbProviderTaxanomy)
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub btnCleatTaxonomy_Click(sender As System.Object, e As System.EventArgs) Handles btnClearProviderType.Click
        ClearSelectedItem(CmbProviderTaxanomy)
    End Sub

    Private Sub btnclearethnicity_Click(sender As Object, e As System.EventArgs) Handles btnclearethnicity.Click
        ClearSelectedItem(cmbethnicity)
    End Sub

    Private Sub btncleargender_Click(sender As Object, e As System.EventArgs) Handles btncleargender.Click
        ClearSelectedItem(cmbGender)
    End Sub

    Private Sub btnClearNPIs_Click(sender As Object, e As System.EventArgs) Handles btnClearNPIs.Click
        'ClearSelectedItem(cmbNPIs)
        txtNPI.Text = ""
    End Sub

    Private Sub btnClearPayers_Click(sender As Object, e As System.EventArgs) Handles btnClearPayers.Click
        ClearSelectedItem(cmbPayers)
    End Sub

    Private Sub btnClearProblems_Click(sender As Object, e As System.EventArgs) Handles btnClearProblems.Click
        ClearSelectedItem(cmbProblems)
    End Sub

    Private Sub btnClearTINs_Click(sender As Object, e As System.EventArgs) Handles btnClearTINs.Click
        'ClearSelectedItem(cmbTINs)
        txtTIN.Text = ""
    End Sub

    Private Sub btnCleaseRace_Click(sender As Object, e As System.EventArgs) Handles btnCleaseRace.Click
        ClearSelectedItem(cmbRace)
    End Sub

    Private Sub chkAge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAge.CheckedChanged
        If chkAge.Checked Then
            pnlAge.Enabled = True
            cmbAgeCriteria.Enabled = True
        Else
            pnlAge.Enabled = False
            cmbAgeCriteria.Enabled = False
        End If
    End Sub


    Private Sub SetCheckValues(lst As ComboBox)
        Dim cnt As Integer = 0
        Dim lstitem As Integer = 0

        If lst.Items.Count = 0 Then
            For cnt = 1 To dgCustomGrid.C1Task.Rows.Count - 1
                dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            Next
        End If

        For lstitem = 0 To lst.Items.Count - 1
            For cnt = 1 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.GetItem(cnt, 1).ToString().Trim() = lst.Items(lstitem).ToString().Trim() Then
                    dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub SetCheckValues(filterValue As String)
        Dim cnt As Integer = 0
        Dim lstitem As Integer = 0

        If filterValue = "" Then
            For cnt = 1 To dgCustomGrid.C1Task.Rows.Count - 1
                dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            Next
        End If

        For cnt = 1 To dgCustomGrid.C1Task.Rows.Count - 1
            If dgCustomGrid.GetItem(cnt, 1).ToString().Trim() = filterValue.Trim() Then
                dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                Exit For
            End If
        Next

    End Sub


    Private Sub ClearSelectedItem(ByRef cmbdropdown As ComboBox)
        cmbdropdown.Items.Clear()
    End Sub

    Public Function ReplaceSpecialCharacters(strSpecialChar As String) As String
        Try
            strSpecialChar = strSpecialChar.Replace("#", "[#]") + ""
            strSpecialChar = strSpecialChar.Replace("$", "[$]") + ""
            strSpecialChar = strSpecialChar.Replace("%", "[%]") + ""
            strSpecialChar = strSpecialChar.Replace("^", "[^]") + ""
            strSpecialChar = strSpecialChar.Replace("&", "[&]") + ""
            strSpecialChar = strSpecialChar.Replace("~", "[~]") + ""
            strSpecialChar = strSpecialChar.Replace("!", "[!]") + ""
            strSpecialChar = strSpecialChar.Replace("*", "[*]") + ""
            strSpecialChar = strSpecialChar.Replace(";", "[;]") + ""
            strSpecialChar = strSpecialChar.Replace("/", "[/]") + ""
            strSpecialChar = strSpecialChar.Replace("?", "[?]") + ""
            strSpecialChar = strSpecialChar.Replace(">", "[>]") + ""
            strSpecialChar = strSpecialChar.Replace("<", "[<]") + ""
            strSpecialChar = strSpecialChar.Replace("\", "[\]") + ""
            strSpecialChar = strSpecialChar.Replace("|", "[|]") + ""
            strSpecialChar = strSpecialChar.Replace("{", "[{]") + ""
            strSpecialChar = strSpecialChar.Replace("}", "[}]") + ""
            strSpecialChar = strSpecialChar.Replace("-", "[-]") + ""
            strSpecialChar = strSpecialChar.Replace("_", "[_]") + ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
        Return strSpecialChar
    End Function


    Private Sub dgCustomGrid_SearchChanged(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView = DirectCast(dgCustomGrid.GridDatasource, DataView)
            If dvPatient Is Nothing Then
                Me.Cursor = Cursors.[Default]
                Return
            End If

            Dim strPatientSearchDetails As String = ""
            If dgCustomGrid.SearchText.ToString().Trim() <> "" Then
                strPatientSearchDetails = dgCustomGrid.SearchText.ToString().Replace("'", "''")
                strPatientSearchDetails = strPatientSearchDetails.Replace("[", "") + ""
                strPatientSearchDetails = ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            If strLst = "drugs" Then
                dvPatient.RowFilter = (Convert.ToString("[" + dvPatient.Table.Columns(0).ColumnName + "]" + " Like '") & strPatientSearchDetails) + "%'  "
            ElseIf strLst = "InsPlan" Then
                dvPatient.RowFilter = (Convert.ToString("[" + dvPatient.Table.Columns(1).ColumnName + "]" + " Like '") & strPatientSearchDetails) + "%'  "
            Else
                dvPatient.RowFilter = (Convert.ToString("[" + dvPatient.Table.Columns(0).ColumnName + "]" + " Like '%") & strPatientSearchDetails) + "%' "
            End If

            dgCustomGrid.Enabled = False
            dgCustomGrid.datasource(dvPatient)
            dgCustomGrid.Enabled = True
            Me.Cursor = Cursors.[Default]

            If (strLst.ToLower()) = "insplan" Then
                'dgCustomGrid.C1Grid.Cols(1).Width = 0
            Else
                If dgCustomGrid.C1Task.Cols.Count > 2 Then
                    dgCustomGrid.C1Task.Cols(2).Visible = False
                End If
            End If
            dgCustomGrid.Selectsearch(gloUserControlLibrary.CustomTask.enmcontrol.Search)
        Catch objErr As Exception
            Me.Cursor = Cursors.[Default]
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.[Select], objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString(), _MessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub dgCustomGrid_CloseClick(sender As Object, e As EventArgs)
        dgCustomGrid.Visible = False
        pnlcustomTask.Visible = False
    End Sub

    Private Sub dgCustomGrid_OKClick(sender As Object, e As EventArgs)
        Try
            dgCustomGrid.SearchText = ""

            If dgCustomGrid.C1Task.Rows.Count > 1 Then
                Select Case strLst
                    Case "gender"
                        cmbGender.Items.Clear()
                        Exit Select
                    Case "race"
                        cmbRace.Items.Clear()
                        Exit Select
                    Case "ethnicity"
                        cmbethnicity.Items.Clear()
                        Exit Select
                    Case "npi"
                        txtNPI.Text = ""
                        Exit Select
                    Case "diag"
                        cmbProblems.Items.Clear()
                        Exit Select
                    Case "insplan"
                        cmbPayers.Items.Clear()
                        Exit Select
                    Case "Provider Type"
                        CmbProviderTaxanomy.Items.Clear()
                        Exit Select
                End Select

                If strLst <> "PatientLabResults" Then
                    If strLst = "npi" Then
                        Dim j As Int32 = 0
                        Dim rowcnt As Integer = dgCustomGrid.GetTotalRows
                        For j = 1 To rowcnt - 1
                            If (Convert.ToBoolean(dgCustomGrid.GetItem(j, 0))) Then
                                txtNPI.Text = dgCustomGrid.GetItem(j, 1).ToString()
                            End If
                        Next
                    ElseIf strLst = "diag" Then
                        Dim j As Int32 = 0
                        Dim rowcnt As Integer = dgCustomGrid.GetTotalRows
                        For j = 1 To rowcnt - 1
                            If (Convert.ToBoolean(dgCustomGrid.GetItem(j, 0))) Then
                                If (cmbProblems.Items.Contains(dgCustomGrid.GetItem(j, 1).ToString()) = False) Then
                                    cmbProblems.Items.Add(dgCustomGrid.GetItem(j, 1).ToString())
                                    If cmbProblems.SelectedIndex = -1 Then
                                        cmbProblems.SelectedIndex = 0
                                    End If
                                End If
                            End If
                        Next
                    ElseIf strLst = "gender" Then
                        Dim j As Int32 = 0
                        Dim rowcnt As Integer = dgCustomGrid.GetTotalRows
                        For j = 1 To rowcnt - 1
                            If (Convert.ToBoolean(dgCustomGrid.GetItem(j, 0))) Then
                                If (cmbGender.Items.Contains(dgCustomGrid.GetItem(j, 1).ToString()) = False) Then
                                    cmbGender.Items.Add(dgCustomGrid.GetItem(j, 1).ToString())
                                    If cmbGender.SelectedIndex = -1 Then
                                        cmbGender.SelectedIndex = 0
                                    End If
                                End If
                            End If
                        Next
                    ElseIf strLst = "insplan" Then
                        Dim j As Int32 = 0
                        Dim rowcnt As Integer = dgCustomGrid.GetTotalRows
                        For j = 1 To rowcnt - 1
                            If (Convert.ToBoolean(dgCustomGrid.GetItem(j, 0))) Then
                                If (cmbPayers.Items.Contains(dgCustomGrid.GetItem(j, 1).ToString()) = False) Then
                                    cmbPayers.Items.Add(dgCustomGrid.GetItem(j, 1).ToString())
                                    If cmbPayers.SelectedIndex = -1 Then
                                        cmbPayers.SelectedIndex = 0
                                    End If
                                End If
                            End If
                        Next
                    ElseIf strLst = "race" Then
                        Dim j As Int32 = 0
                        Dim rowcnt As Integer = dgCustomGrid.GetTotalRows
                        For j = 1 To rowcnt - 1
                            If (Convert.ToBoolean(dgCustomGrid.GetItem(j, 0))) Then
                                If (cmbRace.Items.Contains(dgCustomGrid.GetItem(j, 1).ToString()) = False) Then
                                    cmbRace.Items.Add(dgCustomGrid.GetItem(j, 1).ToString())
                                    If cmbRace.SelectedIndex = -1 Then
                                        cmbRace.SelectedIndex = 0
                                    End If
                                End If
                            End If
                        Next

                    ElseIf strLst = "ethnicity" Then
                        Dim j As Int32 = 0
                        Dim rowcnt As Integer = dgCustomGrid.GetTotalRows
                        For j = 1 To rowcnt - 1
                            If (Convert.ToBoolean(dgCustomGrid.GetItem(j, 0))) Then
                                If (cmbethnicity.Items.Contains(dgCustomGrid.GetItem(j, 1).ToString()) = False) Then
                                    cmbethnicity.Items.Add(dgCustomGrid.GetItem(j, 1).ToString())
                                    If cmbethnicity.SelectedIndex = -1 Then
                                        cmbethnicity.SelectedIndex = 0
                                    End If
                                End If
                            End If
                        Next
                    ElseIf strLst = "Provider Type" Then
                        Dim j As Int32 = 0
                        Dim rowcnt As Integer = dgCustomGrid.GetTotalRows
                        For j = 1 To rowcnt - 1
                            If (Convert.ToBoolean(dgCustomGrid.GetItem(j, 0))) Then
                                If (CmbProviderTaxanomy.Items.Contains(dgCustomGrid.GetItem(j, 1).ToString()) = False) Then
                                    CmbProviderTaxanomy.Items.Add(dgCustomGrid.GetItem(j, 1).ToString())
                                    If CmbProviderTaxanomy.SelectedIndex = -1 Then
                                        CmbProviderTaxanomy.SelectedIndex = 0
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            pnlcustomTask.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            dgCustomGrid.Visible = False
        End Try

    End Sub

    Private Sub dgCustomGrid_MouseMove(sender As Object, e As MouseEventArgs)
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub

    Private Sub chkPracticeAddress_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPracticeAddress.CheckedChanged
        If chkPracticeAddress.Checked = True Then
            pnlProviderAddress.Enabled = True
        Else
            pnlProviderAddress.Enabled = False
        End If
    End Sub
#End Region


    Private Sub btnClearFilters_Click(sender As System.Object, e As System.EventArgs) Handles btnClearFilters.Click
        cmbAge.SelectedItem = Nothing
        cmbAgeCriteria.SelectedItem = Nothing
        cmbethnicity.Items.Clear()
        cmbGender.Items.Clear()
        cmbProblems.Items.Clear()
        cmbPayers.Items.Clear()
        cmbRace.Items.Clear()
        CmbProviderTaxanomy.Items.Clear()
        txtNPI.Text = ""
        txtTIN.Text = ""
        chkAge.Checked = False
        chkPracticeAddress.Checked = False
        oAddresscontrol.ClearaddressControl()
    End Sub

    Private Sub Combo_DropDownClosed(sender As Object, e As EventArgs) Handles cmbRace.DropDownClosed, cmbProblems.DropDownClosed, cmbPayers.DropDownClosed, cmbGender.DropDownClosed, cmbethnicity.DropDownClosed, CmbProviderTaxanomy.DropDownClosed
        ToolTip1.Hide(sender)
    End Sub

    Private Sub Combo_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbRace.DrawItem, cmbProblems.DrawItem, cmbPayers.DrawItem, cmbGender.DrawItem, cmbethnicity.DrawItem, CmbProviderTaxanomy.DrawItem
        Try
            If e.Index < 0 Then
                Return
            End If
            Dim text As String = sender.GetItemText(sender.Items(e.Index))
            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(text, e.Font, br, e.Bounds)
            End Using
            If sender.DroppedDown = True Then
                If getWidthofListItems(text, sender) >= sender.DropDownWidth - 20 Then
                    If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                        ToolTip1.Show(text, sender, e.Bounds.Right, e.Bounds.Bottom + 5)
                    End If
                Else
                    ToolTip1.Hide(sender)
                End If
            End If
            e.DrawFocusRectangle()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Private Function getWidthofListItems(_text As String, combo As ComboBox) As Integer
        Try
            Dim g As Graphics = Me.CreateGraphics()
            If g IsNot Nothing Then
                Dim s As SizeF = g.MeasureString(_text, combo.Font)
                Width = s.Width
                g.Dispose()
                Return s.Width
            End If

            Return Width
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        End Try
    End Function

    Private Sub AlignDataCenterfor2014()
        For row As Integer = 0 To C1QualityMeasures.Rows.Count - 1
            C1QualityMeasures.Rows(row).AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.None
            C1QualityMeasures.Rows(row).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        Next
        C1QualityMeasures.Cols(COL_CoreMeasure_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1QualityMeasures.Cols(COL_Domain_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1QualityMeasures.Cols(COL_Denomenator_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_Numeratot_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_Denomenator_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_DenominatorExceptionsPatientID_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_DenominatorExclusionsPatientID_2014).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

    End Sub

    Private Sub btnbrprov_Click(sender As System.Object, e As System.EventArgs) Handles btnbrprov.Click
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 330
        LoadUserGridProv()

        SetCheckValuesProv(CType(cmbProviders.DataSource, DataTable))
        pnlcustomTask.BringToFront()
    End Sub
    Private Sub LoadUserGridProv()
        Try
            AddControlProv()
            If (dgCustomGridProv IsNot Nothing) Then
                dgCustomGridProv.Visible = True
                dgCustomGridProv.SetSelectAllVisible = False
                dgCustomGridProv.Width = pnlcustomTask.Width
                dgCustomGridProv.Height = pnlcustomTask.Height
                dgCustomGridProv.C1Grid.AllowEditing = False
                dgCustomGridProv.BringToFront()
                dgCustomGridProv.SetVisible = False
                BindUserGridProv()
                dgCustomGridProv.Visible = True
                dgCustomGridProv.Selectsearch(gloUserControlLibrary.CustomTask.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Dim dtprov As DataTable = Nothing
    Private Sub dgCustomGridProv_OKClick(sender As Object, e As EventArgs)
        Try
            dgCustomGridProv.SearchText = ""

            If dgCustomGridProv.C1Task.Rows.Count > 1 Then
                If (Not dtprov Is Nothing) Then
                    dtprov = CType(cmbProviders.DataSource, DataTable)
                Else
                    dtprov = New DataTable
                    dtprov.Columns.Clear()
                    dtprov.Columns.Add("nProviderId")
                    dtprov.Columns.Add("ProviderName")
                End If

                dtprov.Rows.Clear()



                Dim rowcnt As Integer = dgCustomGridProv.GetTotalRows
                For j As Integer = 1 To rowcnt - 1
                    If (Convert.ToBoolean(dgCustomGridProv.GetItem(j, 0))) Then
                        Dim dr As DataRow = dtprov.NewRow()
                        dr("nProviderId") = dgCustomGridProv.GetItem(j, 1).ToString()
                        dr("ProviderName") = dgCustomGridProv.GetItem(j, 2).ToString()

                        dtprov.Rows.Add(dr)
                    End If
                Next
                If (Not dtprov Is Nothing) Then
                    cmbProviders.DataSource = dtprov
                    cmbProviders.ValueMember = "nProviderId"
                    cmbProviders.DisplayMember = "ProviderName"
                End If

                If cmbProviders.SelectedIndex = -1 AndAlso cmbProviders.Items.Count > 0 Then
                    cmbProviders.SelectedIndex = 0
                End If

            End If


            pnlcustomTask.Visible = False
            dgCustomGridProv.Visible = False
            'ShowTaxID(cmb_Provider.DataSource)
            'GetValues()
            'FillData()

        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally

        End Try
    End Sub

    Private Sub SetCheckValuesProv(dtdata As DataTable)
        Dim cnt As Integer = 0
        Dim lstitem As Integer = 0

        If IsNothing(dtdata) OrElse dtdata.Rows.Count = 0 Then

            For cnt = 1 To dgCustomGridProv.C1Task.Rows.Count - 1
                dgCustomGridProv.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            Next


        Else
            ' For lstitem = 0 To lst.Items.Count - 1
            For cnt = 1 To dgCustomGridProv.C1Task.Rows.Count - 1
                Try


                    Dim drdata As DataRow() = dtdata.Select("nProviderid='" & dgCustomGridProv.GetItem(cnt, 1).ToString().Trim() & "'")
                    If (drdata.Length > 0) Then
                        dgCustomGridProv.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
            ' Next
        End If
    End Sub

    Private Sub btnClearProvider_Click(sender As System.Object, e As System.EventArgs) Handles btnClearProvider.Click
        Try


            If (Not IsNothing(cmbProviders.DataSource)) Then
                Dim dt As DataTable = cmbProviders.DataSource
                Dim dr As DataRow() = dt.Select("nProviderID='" & cmbProviders.SelectedValue & "'")
                If (dr.Length > 0) Then                    
                    dt.Rows.Remove(dr(0))
                    cmbProviders.DataSource = dt
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Function GetCheckMark(dQCheckPointValue As List(Of String), PatientID As Long, CMSID As String) As C1.Win.C1FlexGrid.CheckEnum
        Dim chkReturned As C1.Win.C1FlexGrid.CheckEnum = C1.Win.C1FlexGrid.CheckEnum.Unchecked
        Try
            If PatientID <> 0 Then
                If dQCheckPointValue.Count = 0 OrElse (dQCheckPointValue.Contains(CMSID)) Then
                    chkReturned = C1.Win.C1FlexGrid.CheckEnum.Checked
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return chkReturned
    End Function

    Private Class RowVisibility
        Implements IDisposable

        Public Property Rows As List(Of C1.Win.C1FlexGrid.Row)
        Public Property C1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid = Nothing

        Public Sub New(ByVal c1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal Rows As C1.Win.C1FlexGrid.Row())
            Me.C1FlexGrid = c1FlexGrid

            For Each row As C1.Win.C1FlexGrid.Row In Rows
                row.UserData = Me
            Next

            Me.Rows = New List(Of C1.Win.C1FlexGrid.Row)
            Me.Rows.AddRange(Rows)

        End Sub

        Public Sub TurnOffVisibility()
            For Each row As C1.Win.C1FlexGrid.Row In Me.Rows.AsEnumerable().Where(Function(p) p.Visible = True)
                Me.C1FlexGrid.Rows(row.Index).Visible = False
            Next
        End Sub

        Public Sub TurnOnVisibility()
            For Each row As C1.Win.C1FlexGrid.Row In Me.Rows.AsEnumerable().Where(Function(p) p.Visible = False)
                Me.C1FlexGrid.Rows(row.Index).Visible = True
            Next
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    For Each row As C1.Win.C1FlexGrid.Row In Rows.AsEnumerable().Where(Function(p) p.UserData IsNot Nothing AndAlso TypeOf (p.UserData) Is RowVisibility AndAlso DirectCast(p.UserData, RowVisibility) Is Me)
                        row.UserData = Nothing
                    Next

                    If Me.Rows IsNot Nothing Then
                        Me.Rows.Clear()
                        Me.Rows = Nothing
                    End If
                    Me.C1FlexGrid = Nothing
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Private sClinicCode As String
    Public Property ClinicCode() As String
        Get
            Return sClinicCode
        End Get
        Set(ByVal value As String)
            sClinicCode = value
        End Set
    End Property
End Class


