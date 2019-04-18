Imports System.Windows.Forms
Imports gloUserControlLibrary
Imports System.Drawing


Public Class frm_CQM_Detail_Report
    Shared ofrm_CQM_Detail_Report As frm_CQM_Detail_Report = Nothing
    Dim SPName As String
    Dim _databaseConnectionString As String
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim dsCQMDetailReport As DataSet

    Dim _StartDate As String
    Dim _EndDate As String
    Dim _ProviderID As DataTable
    Dim _ExtraParameter As Int16 = 0


    Dim _denName As String = String.Empty
    Dim _numName As String = String.Empty
    Dim _DenominatorExclusionsPatientID As String
    Dim _DenominatorExceptionsPatientID As String

    Dim _MeasureName As String
    Dim _MeasureID As String
    Public dtQRDA1Patientlist As DataTable = Nothing
    Public dsqrda1Criteria As DataSet = Nothing
    Private WithEvents ogloUC_generalsearch As gloUserControlLibrary.gloUCGeneralSearch
    Private WithEvents ogloUC_generalsearchDenominator As gloUserControlLibrary.gloUCGeneralSearch

    Private WithEvents ogloUC_DenominatorExclusionsSearch As gloUserControlLibrary.gloUCGeneralSearch
    Private WithEvents ogloUC_DenominatorExceptionsSearch As gloUserControlLibrary.gloUCGeneralSearch

    Dim isSearchadded As Boolean = False
    Dim _UserID As Int64 = 0
    Dim denoimagecolumn_0419 As Int16 = 0
    Dim numimagecolumn_0419 As Int16 = 0
    Private _strUsername As String = ""
    Private _PopulationNo As Int16 = 0
    Private _ReportingYear As String = ""
    'Public Delegate Sub DelShowHelp(ByVal obj As Object)
    'Public Event EvtShowHelp As DelShowHelp
    Public Property ReportingYear As String
        Get
            Return _ReportingYear
        End Get
        Set(ByVal value As String)
            _ReportingYear = value
        End Set
    End Property

    Public Property UserName As String
        Get
            Return _strUsername
        End Get
        Set(value As String)
            _strUsername = value
        End Set
    End Property
    Public Property UserID As Int64
        Get
            Return _UserID
        End Get
        Set(value As Int64)
            _UserID = value
        End Set
    End Property
    Public Property StoreProcedureName As String
        Get
            Return SPName
        End Get
        Set(ByVal value As String)
            SPName = value
        End Set
    End Property

    Public Property ProviderID As DataTable

        Get
            Return _ProviderID
        End Get
        Set(ByVal value As DataTable)
            _ProviderID = value
        End Set
    End Property

    Public Property StartDate As String

        Get
            Return _StartDate
        End Get
        Set(ByVal value As String)
            _StartDate = value
        End Set
    End Property

    Public Property EndDate As String
        Get
            Return _EndDate
        End Get
        Set(ByVal value As String)
            _EndDate = value
        End Set
    End Property

    Public Property ExtraParameter As Int16
        Get
            Return _ExtraParameter
        End Get
        Set(ByVal value As Int16)
            _ExtraParameter = value
        End Set
    End Property

    Public Property DenominatorIDS As String
        Get
            Return _denName
        End Get
        Set(ByVal value As String)
            _denName = value
        End Set
    End Property

    Public Property NemuratorIDS As String
        Get
            Return _numName
        End Get
        Set(ByVal value As String)
            _numName = value
        End Set
    End Property

    Public Property DenominatorExclusionsPatientIDs As String
        Get
            Return _DenominatorExclusionsPatientID
        End Get
        Set(ByVal value As String)
            _DenominatorExclusionsPatientID = value
        End Set
    End Property

    Public Property DenominatorExceptionsPatientIDs As String
        Get
            Return _DenominatorExceptionsPatientID
        End Get
        Set(ByVal value As String)
            _DenominatorExceptionsPatientID = value
        End Set
    End Property
    Public Property MeasureName As String
        Get
            Return _MeasureName
        End Get
        Set(value As String)
            _MeasureName = value
        End Set
    End Property
    Public Property MeasureID As String
        Get
            Return _MeasureID
        End Get
        Set(value As String)
            _MeasureID = value
        End Set
    End Property
    Public Property PopulationNo As Int16
        Get
            Return _PopulationNo
        End Get
        Set(value As Int16)
            _PopulationNo = value
        End Set
    End Property


    Private Sub frm_CQM_Detail_Report_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If Not IsNothing(dsCQMDetailReport) Then
            dsCQMDetailReport.Dispose()
            dsCQMDetailReport = Nothing
        End If

        ofrm_CQM_Detail_Report = Nothing
        ''added for bugid 86630 for searching issue
        If IsNothing(ogloUC_DenominatorExclusionsSearch) = False Then
            RemoveHandler ogloUC_DenominatorExclusionsSearch.AfterTextSearch, AddressOf oSearchProceduresExclusion_AfterTextSearch
        End If

        If IsNothing(ogloUC_DenominatorExceptionsSearch) = False Then
            RemoveHandler ogloUC_DenominatorExceptionsSearch.AfterTextSearch, AddressOf oSearchProceduresDenoException_AfterTextSearch
        End If

        If IsNothing(ogloUC_generalsearch) = False Then
            RemoveHandler ogloUC_generalsearch.AfterTextSearch, AddressOf oSearchProceduresCtl_AfterTextSearch
        End If

        If IsNothing(ogloUC_generalsearchDenominator) = False Then
            RemoveHandler ogloUC_generalsearchDenominator.AfterTextSearch, AddressOf oSearchProceduresDenominator_AfterTextSearch
        End If




    End Sub

    Public Shared Function GetInstance() As frm_CQM_Detail_Report

        Dim frm As frm_CQM_Detail_Report = Nothing
        Dim IsOpen As Boolean
        Try
            IsOpen = False
            ''If frm Is Nothing Then
            For Each f As Form In Application.OpenForms
                If f.Name = "frm_CQM_Detail_Report" Then
                    IsOpen = True
                    frm = f
                End If
            Next
            If (IsOpen = False) Then
                frm = New frm_CQM_Detail_Report()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

        End Try
        Return frm
    End Function
    Public Sub RefreshContent()
        Try
            Dim oBAL As New cls_BAL_MU_Detail_Report(_databaseConnectionString)

            dsCQMDetailReport = oBAL.GetDenAndNumListByIDS(DenominatorIDS, NemuratorIDS, DenominatorExclusionsPatientIDs, DenominatorExceptionsPatientIDs)
            'Ds = oBAL.GetDetailCQMReportingDataSet(SPName, ProviderID, StartDate, EndDate, ExtraParameter)
            If (IsNothing(dsCQMDetailReport)) Then

                C1Numerator.DataSource = Nothing

                C1Denominator.DataSource = Nothing

                C1Numerator.Rows.Count = 1
                C1Denominator.Rows.Count = 1
                C1Numerator.Cols.Count = 4
                C1Denominator.Cols.Count = 4
                C1Numerator.Cols(0).Visible = False
                C1Numerator.AllowEditing = False
                C1Denominator.Cols(0).Visible = False
                C1Denominator.AllowEditing = False
                C1Numerator.Cols(1).Width = 100
                C1Numerator.Cols(2).Width = 400
                C1Denominator.Cols(1).Width = 100
                C1Denominator.Cols(2).Width = 400
                C1Numerator.SetData(0, 1, "Patient Code")
                C1Numerator.SetData(0, 2, "Patient Name")
                C1Numerator.Cols(2).Width = 400
                C1Denominator.Cols(1).Width = 100
                C1Denominator.SetData(0, 1, "Patient Code")
                C1Denominator.SetData(0, 2, "Patient Name")
                C1Denominator.Cols(2).Width = 400
                C1Denominator.SetData(0, 3, "CQM Info")
                C1Denominator.Cols(3).DataType = GetType(System.Drawing.Image)
                C1Numerator.SetData(0, 3, "CQM Info")
                C1Numerator.Cols(3).DataType = GetType(System.Drawing.Image)

            Else

                dsCQMDetailReport.Tables(0).TableName = "Denominator"
                dsCQMDetailReport.Tables(1).TableName = "Numerator"
                dsCQMDetailReport.Tables(2).TableName = "DenominatorExclusions"
                dsCQMDetailReport.Tables(3).TableName = "DenominatorExceptions"

                C1Denominator.DataSource = dsCQMDetailReport.Tables("Denominator")
                C1Numerator.DataSource = dsCQMDetailReport.Tables("Numerator")
                SetDenoGridColumn()
                SetNumGridColumn()
                C1DenominatorExclusions.DataSource = dsCQMDetailReport.Tables("DenominatorExclusions")
                C1DenominatorExceptions.DataSource = dsCQMDetailReport.Tables("DenominatorExceptions")

                C1Numerator.Cols(0).Visible = False
                C1Numerator.AllowEditing = False
                C1Denominator.Cols(0).Visible = False
                C1Denominator.AllowEditing = False

                C1DenominatorExclusions.Cols(0).Visible = False
                C1DenominatorExclusions.AllowEditing = False
                C1DenominatorExceptions.Cols(0).Visible = False
                C1DenominatorExceptions.AllowEditing = False

                C1Numerator.Cols(1).Width = 100
                C1Numerator.Cols(2).Width = 450

                C1Denominator.Cols(1).Width = 100
                C1Denominator.Cols(2).Width = 520

                If (isSearchadded = False) Then
                    InitialiseSearchControl()
                    isSearchadded = True
                End If

                ogloUC_generalsearch.Visible = True
                ogloUC_generalsearch.IntialiseDatatable(dsCQMDetailReport.Tables("Numerator"))

                ogloUC_generalsearchDenominator.Visible = True
                ogloUC_generalsearchDenominator.IntialiseDatatable(dsCQMDetailReport.Tables("Denominator"))

                ogloUC_DenominatorExclusionsSearch.Visible = True
                ogloUC_DenominatorExclusionsSearch.IntialiseDatatable(dsCQMDetailReport.Tables("DenominatorExclusions"))

                ogloUC_DenominatorExceptionsSearch.Visible = True
                ogloUC_DenominatorExceptionsSearch.IntialiseDatatable(dsCQMDetailReport.Tables("DenominatorExceptions"))


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Function GetQRDA1Data(ByVal measureid As String, ByVal npatientid As Int64) As DataSet
        Try
            ' Dim resultSet As gloGlobal.DIB.CQMResult = Nothing
            Dim dtmeasurecriterialist As DataTable = New DataTable
            Dim dtselectedpatient As DataTable = Nothing

            Dim strDistinctColumns() As String = {"InitialPatientPopulation", "nVisitID"}
            'dtQRDAIMeasureWisePatientList = dtQRDAIPatientList.Copy
            Dim dtpatientlist As DataTable = dtQRDA1Patientlist.DefaultView.ToTable(True, strDistinctColumns)
            Dim dr As DataRow() = dtpatientlist.Select("Initialpatientpopulation = '" & npatientid & "'")
            dtselectedpatient = dr.CopyToDataTable()
            Dim resultSet As List(Of gloGlobal.DIB.QRDAResult) = Nothing
            Dim resultTVP1 As DataTable = Nothing
            Dim resultTVP2 As DataTable = Nothing
            Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                'resultSet = ogloGSHelper.GetQRDA1CodesList("MU2_GenerateQRDAI,nqf0002,CMSID22,NQF0068,NQF0062,NQF0028,NQF0033,NQF0418,NQF0421")
                Dim _cqmMeasure As String
                If _ReportingYear = "2017" Then
                    _cqmMeasure = "MU2_GenerateQRDAI_V2_2017"
                Else
                    _cqmMeasure = "MU2_GenerateQRDAI_V2"
                End If

                If measureid <> "" Then
                    _cqmMeasure = _cqmMeasure & "," & measureid
                    dtmeasurecriterialist.Columns.Add("MeasureName")
                    dtmeasurecriterialist.Rows.Add(measureid)
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
            oDB.Connect(False)
            Dim ds As New DataSet
            Dim dt As New DataTable
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MU2_CQMList"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtmeasurecriterialist
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MU2_InitialPatientPopulation"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtselectedpatient
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVP_Providers"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = ProviderID
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Date
            oParameter.Value = StartDate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Date
            oParameter.Value = EndDate
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
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ForCriteria"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Bit
            oParameter.Value = 1
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@CMSNUMBER"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Int
            oParameter.Value = _MeasureName
            oParameters.Add(oParameter)
            oParameter = Nothing

            If _ReportingYear = "2017" Then

                oDB.Retrive("MU2_GenerateQRDAI_V2_2017", oParameters, ds)
            Else
                oDB.Retrive("MU2_GenerateQRDAI_V2", oParameters, ds)
            End If


            oDB.Disconnect()
            Dim ocls_general As New cls_MU_General
            If _ReportingYear = "2017" Then
                ocls_general.UpdateLog("MU2_GenerateQRDAI_V2_2017")
            Else
                ocls_general.UpdateLog("MU2_GenerateQRDAI_V2")
            End If

            ocls_general = Nothing
            Return ds
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "GetQRDAData", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Sub RefreshContentVisitBased(ByVal dtDenominator As DataTable, ByVal dtNumerator As DataTable, ByVal MeasureName As String)
        Try

            Dim oBAL As New cls_BAL_MU_Detail_Report(_databaseConnectionString)

            dsCQMDetailReport = oBAL.GetDenAndNumListByIDS(DenominatorIDS, NemuratorIDS, DenominatorExclusionsPatientIDs, DenominatorExceptionsPatientIDs)

            If (Not IsNothing(dtDenominator)) Then


                Dim viewNumerator As DataView = dtNumerator.DefaultView
                viewNumerator.RowFilter = "MeasureName = '" & MeasureName & "'"


                Dim viewDenominator As DataView = dtDenominator.DefaultView
                viewDenominator.RowFilter = "MeasureName = '" & MeasureName & "'"


                C1Numerator.DataSource = viewNumerator
                C1Denominator.DataSource = viewDenominator
                If ReportingYear = 2019 Then
                    C1Numerator.Cols(5).Visible = False
                    C1Numerator.Cols.Count = 7
                    numimagecolumn_0419 = 6
                Else
                    C1Numerator.Cols.Count = 6
                    numimagecolumn_0419 = 5
                End If

                

                C1Numerator.SetData(0, numimagecolumn_0419, "")
                C1Numerator.Cols(numimagecolumn_0419).DataType = GetType(System.Drawing.Image)
                C1Numerator.Cols(numimagecolumn_0419).Width = 20
                'C1Numerator.Redraw = True
                C1Numerator.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                AddHandler C1Numerator.OwnerDrawCell, AddressOf C1Numerator_OwnerDrawCell

                C1Numerator.Cols(0).Visible = False
                C1Numerator.AllowEditing = False
                If ReportingYear = 2019 Then
                    C1Denominator.Cols(5).Visible = False
                    C1Denominator.Cols.Count = 7
                    denoimagecolumn_0419 = 6
                Else
                    C1Denominator.Cols.Count = 6
                    denoimagecolumn_0419 = 5
                End If

            
                C1Denominator.SetData(0, denoimagecolumn_0419, "")
                C1Denominator.Cols(denoimagecolumn_0419).DataType = GetType(System.Drawing.Image)
                C1Denominator.Cols(denoimagecolumn_0419).Width = 20
                'C1Denominator.Redraw = True
                C1Denominator.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                AddHandler C1Denominator.OwnerDrawCell, AddressOf C1Denominator_OwnerDrawCell

                C1Denominator.Cols(0).Visible = False
                C1Denominator.AllowEditing = False

                C1Numerator.Cols(1).Visible = False
                C1Denominator.Cols(1).Visible = False
                C1Denominator.Cols("MeasureName").Visible = False

                C1Numerator.Cols(2).Width = 100
                C1Numerator.Cols(3).Width = 350
                C1Numerator.Cols(4).Width = 100

                C1Denominator.Cols("Patient Code").Width = 100
                C1Denominator.Cols("Patient Name").Width = 420

                ''New
                dsCQMDetailReport.Tables(0).TableName = "Denominator"
                dsCQMDetailReport.Tables(1).TableName = "Numerator"
                dsCQMDetailReport.Tables(2).TableName = "DenominatorExclusions"
                dsCQMDetailReport.Tables(3).TableName = "DenominatorExceptions"


                C1DenominatorExclusions.DataSource = dsCQMDetailReport.Tables("DenominatorExclusions")
                C1DenominatorExceptions.DataSource = dsCQMDetailReport.Tables("DenominatorExceptions")

                C1DenominatorExclusions.Cols(0).Visible = False
                C1DenominatorExclusions.AllowEditing = False
                C1DenominatorExceptions.Cols(0).Visible = False
                C1DenominatorExceptions.AllowEditing = False

                If (isSearchadded = False) Then
                    InitialiseSearchControl()
                    isSearchadded = True
                End If

                'Dim dtNumSearch As DataTable = New DataTable
                'Dim dtDenoSearch As DataTable = New DataTable
                'dtNumSearch = dtNumerator.Copy()
                'dtDenoSearch = dtDenominator.Copy()
                'dtNumSearch.Columns.RemoveAt(0)
                'dtNumSearch.Columns.Remove("MeasureName")
                'dtDenoSearch.Columns.RemoveAt(0)
                'dtDenoSearch.Columns.Remove("MeasureName")

                ogloUC_generalsearch.Visible = True
                ogloUC_generalsearch.IntialiseDatatable(viewNumerator.ToTable())

                ogloUC_generalsearchDenominator.Visible = True
                ogloUC_generalsearchDenominator.IntialiseDatatable(viewDenominator.ToTable())

                ogloUC_DenominatorExclusionsSearch.Visible = True
                ogloUC_DenominatorExclusionsSearch.IntialiseDatatable(dsCQMDetailReport.Tables("DenominatorExclusions"))

                ogloUC_DenominatorExceptionsSearch.Visible = True
                ogloUC_DenominatorExceptionsSearch.IntialiseDatatable(dsCQMDetailReport.Tables("DenominatorExceptions"))
                ''New

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Dim CMSNO As String = ""
    Public Sub SetReportingParameters(ByVal MeasureString As String, ByVal ProviderName As String, ByVal StartdateVal As Date, ByVal EndDateVal As Date)
        Try
            lblProviderName.Text = ProviderName
            dtpicStartDate.Value = StartdateVal
            dtpicEndDate.Value = EndDateVal
            'Label10.Text = Reporting_Period_In_Progress
            Label33.Text = "Measure : " + MeasureString
            CMSNO = GetCMSNo(MeasureString)
            SetFormText()
            FillCQmDetails()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub SetFormText()
        If (CMSNO.Contains("122")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS122-NQF00592017"           
            Else
                Me.Text = "CMS122-NQF0059"
            End If



        ElseIf (CMSNO.Contains("117")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS117-NQF00382017"
            Else
                Me.Text = "CMS117-NQF0038"
            End If



        ElseIf (CMSNO.Contains("123")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS123-NQF00562017"            
            Else
                Me.Text = "CMS123-NQF0056"
            End If



        ElseIf (CMSNO.Contains("124")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS124-NQF00322017"            
            Else
                Me.Text = "CMS124-NQF0032"
            End If


        ElseIf (CMSNO.Contains("125")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS1252017"            
            Else
                Me.Text = "CMS125"
            End If




        ElseIf (CMSNO.Contains("127")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS127-NQF00432017"
            Else
                Me.Text = "CMS127-NQF0043"
            End If


        ElseIf (CMSNO.Contains("130")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS130-NQF00342017"
            Else
                Me.Text = "CMS130-NQF0034"
            End If



        ElseIf (CMSNO.Contains("131")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS131-NQF00552017"
            Else
                Me.Text = "CMS131-NQF0055"
            End If


        ElseIf (CMSNO.Contains("134")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS134-NQF00622017"
            Else
                Me.Text = "CMS134-NQF0062"
            End If



        ElseIf (CMSNO.Contains("138")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS138-NQF00282017"
            Else
                Me.Text = "CMS138-NQF0028"
            End If


        ElseIf (CMSNO.Contains("139")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS139-NQF01012017"
            Else
                Me.Text = "CMS139-NQF0101"

            End If



        ElseIf (CMSNO.Contains("146")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS146-NQF00022017"
            Else
                Me.Text = "CMS146-NQF0002"
            End If


        ElseIf (CMSNO.Contains("147")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS147-NQF00412017"
            Else
                Me.Text = "CMS147-NQF0041"
            End If



        ElseIf (CMSNO.Contains("153")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS153-NQF00332017"
            Else
                Me.Text = "CMS153-NQF0033"
            End If


        ElseIf (CMSNO.Contains("163")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS1632017"
            Else
                Me.Text = "CMS163"
            End If


        ElseIf (CMSNO.Contains("164")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS164-NQF00682017"
            Else
                Me.Text = "CMS164-NQF0068"
            End If



        ElseIf (CMSNO.Contains("165")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS165-NQF00182017"
            Else
                Me.Text = "CMS165-NQF0018"
            End If


        ElseIf (CMSNO.Contains("166")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS166-NQF00522017"
            Else
                Me.Text = "CMS166-NQF0052"
            End If


        ElseIf (CMSNO.Contains("66")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS662017"
            Else
                Me.Text = "CMS66"
            End If

        ElseIf (CMSNO.Contains("68")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS68-NQF04192017"            
            Else
                Me.Text = "CMS68-NQF0419"
            End If

        ElseIf (CMSNO.Contains("69")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS69-NQF04212017"
            Else
                Me.Text = "CMS69-NQF0421"
            End If




        ElseIf (CMSNO.Contains("90")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS902017"
            Else
                Me.Text = "CMS90"
            End If


        ElseIf (CMSNO.Contains("22")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS222017"
            Else
                Me.Text = "CMS22"
            End If

        ElseIf (CMSNO.Contains("56")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS562017"
            Else
                Me.Text = "CMS56"
            End If

        ElseIf (CMSNO.Contains("2")) Then
            If _ReportingYear = "2017" Then
                Me.Text = "CMS22017"
            Else
                Me.Text = "CMS2"
            End If
        End If

        If _ReportingYear = "2019" Then
            Me.Text = Me.Text + "-2019"
        End If

    End Sub
    Private Function GetCMSNo(ByVal CMSNO As String) As String
        Dim splstr As String()

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
        CMSNO = CMSNO.Replace(":", "").Replace(" ", "").Replace(",", "").Trim().Replace("CMS", "").Replace("NQF", "")
        Return CMSNO
    End Function
    Private Sub New()
        Try
            ' This call is required by the designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            '  FillCQMDetails()

            If appSettings("DataBaseConnectionString") IsNot Nothing Then
                If appSettings("DataBaseConnectionString") <> "" Then
                    _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Col_CQMCPTCode As Integer = 1
    Private Col_CQMDX As Integer = 2
    Private Col_CQMSnomed As Integer = 3
    Private Col_CQMAge As Integer = 4
    Private Col_CQMVitals As Integer = 5
    Private Col_CQMLionc As Integer = 6
    Private Col_CQMCVX As Integer = 7
    Private Col_CQMRXNorm As Integer = 8


    Private Sub FillCQmDetails()
        C1CQMDetail.Clear()
        C1CQMDetail.Rows.Count = 1
        C1CQMDetail.Cols.Count = 9
        C1CQMDetail.Rows.Add()
        C1CQMDetail.SetData(0, 0, "")
        C1CQMDetail.SetData(0, Col_CQMCPTCode, "CPT")
        C1CQMDetail.SetData(0, Col_CQMDX, "Dx")
        C1CQMDetail.SetData(0, Col_CQMSnomed, "Snomed")
        C1CQMDetail.SetData(0, Col_CQMAge, "Age")
        C1CQMDetail.SetData(0, Col_CQMVitals, "Vitals")
        C1CQMDetail.SetData(0, Col_CQMLionc, "LOINC")
        C1CQMDetail.SetData(0, Col_CQMRXNorm, "Drugs")
        C1CQMDetail.SetData(0, Col_CQMCVX, "CVX")
        C1CQMDetail.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1CQMDetail.Cols(Col_CQMCPTCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1CQMDetail.Cols(Col_CQMDX).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1CQMDetail.Cols(Col_CQMSnomed).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1CQMDetail.Cols(Col_CQMAge).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1CQMDetail.Cols(Col_CQMVitals).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1CQMDetail.Cols(Col_CQMLionc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1CQMDetail.Cols(Col_CQMCVX).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1CQMDetail.Cols(Col_CQMRXNorm).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        reterivedata()
    End Sub

    Private Sub reterivedata()
        clsCQMMeasure._databaseConnectionString = _databaseConnectionString
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oParameters.Add("@IntCQMCMSID", Convert.ToInt32(CMSNO), ParameterDirection.Input, SqlDbType.Int)
        oParameters.Add("@PopulationNo", _PopulationNo, ParameterDirection.Input, SqlDbType.Int)
        oParameters.Add("@ReportingYear", _ReportingYear, ParameterDirection.Input, SqlDbType.VarChar)
        Dim dsdata As DataSet = clsCQMMeasure.GetdataWithParamDataset(oParameters, "gsp_getCQMCriteriaDetails")
        Dim dtdata As DataTable = Nothing
        If (dsdata.Tables.Count > 0) Then
            dtdata = dsdata.Tables(0)
            If (dtdata.Rows.Count > 0) Then
                cnumStyle = C1CQMDetail.Styles.Add("LGreen")
                cnumStyle.BackColor = Color.LightGreen

                cdenStyle = C1CQMDetail.Styles.Add("LGray")
                cdenStyle.BackColor = Color.LightGray
                SetDatatoCQMGrid(dtdata)

                'If (C1CQMDetail.Rows.Count > 2) Then
                If (C1CQMDetail.Rows.Count < 3) Then
                    'While (C1CQMDetail.Rows.Count <> 3)
                    '    C1CQMDetail.Rows.Add()
                    'End While
                    SetNumDeno()
                Else
                    C1CQMDetail.SetData(C1CQMDetail.Rows.Count - 2, 0, "Num")
                    C1CQMDetail.SetData(C1CQMDetail.Rows.Count - 1, 0, "Denom")
                    Dim crnum As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(C1CQMDetail.Rows.Count - 2, 0)
                    crnum.Style = cnumStyle
                    Dim crden As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(C1CQMDetail.Rows.Count - 1, 0)
                    crden.Style = cdenStyle
                End If
                'C1CQMDetail.SetData(C1CQMDetail.Rows.Count - 2, 0, "Num")
                'C1CQMDetail.SetData(C1CQMDetail.Rows.Count - 1, 0, "Denom")
                'Dim crnum As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(C1CQMDetail.Rows.Count - 2, 0)
                'crnum.Style = cnumStyle
                'Dim crden As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(C1CQMDetail.Rows.Count - 1, 0)
                'crden.Style = cdenStyle
                'End If
            End If
            If (dsdata.Tables.Count > 1) Then
                If (C1CQMDetail.Rows.Count < 3) Then
                    SetNumDeno()
                End If
                SetDemographicsData(dsdata.Tables(1))
            End If
        End If
        'C1CQMDetail.Selection.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
    End Sub
    Private Sub SetNumDeno()
        While (C1CQMDetail.Rows.Count <> 3)
            C1CQMDetail.Rows.Add()
        End While
        cnumStyle = C1CQMDetail.Styles.Add("LGreen")
        cnumStyle.BackColor = Color.LightGreen

        cdenStyle = C1CQMDetail.Styles.Add("LGray")
        cdenStyle.BackColor = Color.LightGray
        C1CQMDetail.SetData(C1CQMDetail.Rows.Count - 2, 0, "Num")
        C1CQMDetail.SetData(C1CQMDetail.Rows.Count - 1, 0, "Denom")
        Dim crnum As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(C1CQMDetail.Rows.Count - 2, 0)
        crnum.Style = cnumStyle
        Dim crden As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(C1CQMDetail.Rows.Count - 1, 0)
        crden.Style = cdenStyle
    End Sub
    Private Sub SetDemographicsData(ByVal DtVitals As DataTable)
        Dim blnremoveloinc As Boolean = False

        If (DtVitals.Rows.Count > 0) Then
            Dim splagestr As String()
            Dim agestr As String = String.Empty
            splagestr = Convert.ToString(DtVitals.Rows(0)("MinAge")).Split(".")
            If (splagestr.Length > 0) Then
                agestr = splagestr(0) & " " & "yrs"

            End If
            If (splagestr.Length > 1) Then
                If (splagestr(1).Replace("0", "").Trim().Length > 0) Then
                    agestr = agestr & " " & splagestr(1) & " " & "mn"
                    'cmbAgeMinMnth.Items.Add(splagestr(1))
                    'cmbAgeMinMnth.SelectedIndex = 0
                End If
            End If
            Array.Resize(splagestr, 0)
            splagestr = Convert.ToString(DtVitals.Rows(0)("MaxAge")).Split(".")

            If (splagestr.Length > 0) Then
                If (splagestr(0).Replace("0", "").Trim().Length > 0) Then
                    agestr = agestr & " and " & splagestr(0) & " " & "yrs"
                End If
                'Dim cnt As Integer = Convert.ToInt32(splagestr(0))
                'If (cnt > 0) Then
                '    cmbAgeMax.Items.Add(splagestr(0))
                '   cmbAgeMax.SelectedIndex = 0

                'End If
            End If
            If (splagestr.Length > 1) Then
                If (splagestr(1).Replace("0", "").Trim().Length > 0) Then
                    agestr = agestr & " " & splagestr(1) & " " & "mn"
                    'cmbAgeMaxMnth.Items.Add(splagestr(1).Replace("0.00", ""))
                    'cmbAgeMaxMnth.SelectedIndex = 0
                End If
            End If
            If (agestr <> String.Empty) Then
                C1CQMDetail.SetData(1, Col_CQMAge, agestr)
                C1CQMDetail.Cols(Col_CQMAge).Width = 150
            End If
            Dim strvitals As String = String.Empty
            If (Convert.ToString(DtVitals.Rows(0)("MintoBPStanding")).Replace("-1.00", "").Trim() <> String.Empty) Then
                strvitals = Convert.ToString(DtVitals.Rows(0)("MintoBPStanding"))
            End If

            If (Convert.ToString(DtVitals.Rows(0)("MaxTOBPSitting")).Replace("-1.00", "").Trim() <> String.Empty) Then
                strvitals = strvitals & "-" & Convert.ToString(DtVitals.Rows(0)("MaxTOBPSitting")) & " / "
            End If


            If (Convert.ToString(DtVitals.Rows(0)("MaxBPSitting")).Replace("-1.00", "").Trim() <> String.Empty) Then
                strvitals = strvitals & Convert.ToString(DtVitals.Rows(0)("MaxBPSitting"))
            End If

            If (Convert.ToString(DtVitals.Rows(0)("MinBPSitting")).Replace("-1.00", "").Trim() <> String.Empty) Then
                strvitals = strvitals & "-" & Convert.ToString(DtVitals.Rows(0)("MinBPSitting"))
            End If

            If (strvitals <> String.Empty) Then
                C1CQMDetail.SetData(1, Col_CQMVitals, strvitals)
                blnremoveloinc = True
            End If
            Dim strBMI As String = String.Empty
            strBMI = ChangeDefaultValues(DtVitals.Rows(0)("MinBMI"))
            strBMI = strBMI & "-" & ChangeDefaultValues(DtVitals.Rows(0)("MaxBMI"))
            If (strBMI.Length > 1) Then
                C1CQMDetail.SetData(1, Col_CQMVitals, strBMI)
                blnremoveloinc = True
            End If
            ''txtBPsettingMax.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxBPSitting"))

            ''txtBPsettingMin.Text = ChangeDefaultValues(DtVitals.Rows(0)("MinBPSitting"))
            ''txtBPsettingMaxTo.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxTOBPSitting")).Replace("0.00", "")

            ''txtBPsettingMinTo.Text = ChangeDefaultValues(DtVitals.Rows(0)("MinTOBPSitting")).Replace("0.00", "")
            ''txtBPstandingMax.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxBPStanding"))
            ''txtBPstandingMin.Text = ChangeDefaultValues(DtVitals.Rows(0)("MinBPStanding"))

            ''txtBPstandingMaxTo.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxtoBPStanding")).Replace("0.00", "")
            ''txtBPstandingMinTo.Text = ChangeDefaultValues(DtVitals.Rows(0)("MintoBPStanding")).Replace("0.00", "")
            'txtBMImin.Text = ChangeDefaultValues(DtVitals.Rows(0)("MinBMI"))
            'txtBMImax.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxBMI"))

        End If
        ''code changes done for bugid 104257

        If (blnremoveloinc = True) Then
            For i As Integer = 1 To C1CQMDetail.Rows.Count - 1
                C1CQMDetail.SetData(i, Col_CQMLionc, "")
                Dim cr As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(i, Col_CQMLionc)

                cr.Style = Nothing
            Next
        End If
    End Sub
    Private Function ChangeDefaultValues(ByVal strvalue As Object) As String
        If (Convert.ToString(strvalue) = "-1.00") Then
            Return ""
        Else
            Return Convert.ToString(strvalue).Trim()
        End If
    End Function
    Private Sub SetDatatoCQMGrid(ByVal dtdata As DataTable)


        Dim row_cnt As Integer = 0
        Dim dxcnt As Integer = 1
        Dim snomedcnt As Integer = 1
        Dim ordercnt As Integer = 1
        Dim Rxnormcnt As Integer = 1
        Dim Hcpcscnt As Integer = 1
        Dim CVXcnt As Integer = 1
        For Each dr As DataRow In dtdata.Rows
            If IsDBNull(dr("CodeType")) = False Then
                If (Convert.ToInt32(dr("CodeType")) = gloListControl.gloListControlType.CQMICD9) Then

                    AssignCQMData(dxcnt, Col_CQMDX, Convert.ToString(dr("Code")), Convert.ToString(dr("PopCriteria")))
                End If
                If (Convert.ToInt32(dr("CodeType")) = gloListControl.gloListControlType.CQMICD10) Then

                    AssignCQMData(dxcnt, Col_CQMDX, Convert.ToString(dr("Code")), Convert.ToString(dr("PopCriteria")))
                End If
                If (Convert.ToInt32(dr("CodeType")) = gloListControl.gloListControlType.CQMSnomed) Then

                    AssignCQMData(snomedcnt, Col_CQMSnomed, Convert.ToString(dr("Code")), Convert.ToString(dr("PopCriteria")))
                End If
                If (Convert.ToInt32(dr("CodeType")) = gloListControl.gloListControlType.CQMOrders) Then

                    AssignCQMData(ordercnt, Col_CQMLionc, Convert.ToString(dr("Code")), Convert.ToString(dr("PopCriteria")))
                End If

                If (Convert.ToInt32(dr("CodeType")) = gloListControl.gloListControlType.CQMHCPCS) Then

                    AssignCQMData(Hcpcscnt, Col_CQMCPTCode, Convert.ToString(dr("Code")), Convert.ToString(dr("PopCriteria")))

                End If
                If (Convert.ToInt32(dr("CodeType")) = gloListControl.gloListControlType.CQMRxNorm) Then
                    ''rxnorm
                    AssignCQMData(Rxnormcnt, Col_CQMRXNorm, Convert.ToString(dr("CodeDescription")), Convert.ToString(dr("PopCriteria")))


                End If
                If (Convert.ToInt32(dr("CodeType")) = gloListControl.gloListControlType.CQMCVX) Then

                    AssignCQMData(CVXcnt, Col_CQMCVX, Convert.ToString(dr("Code")), Convert.ToString(dr("PopCriteria")))


                End If
            End If
        Next


    End Sub
    Dim cdenStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
    Dim cnumStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
    Private Sub AssignCQMData(ByRef rowno As Integer, ByVal ColumnName As Integer, value As String, strcriteria As String)
        If (rowno < 6) Then


            If (rowno >= C1CQMDetail.Rows.Count) Then
                C1CQMDetail.Rows.Add()

            End If
            C1CQMDetail.SetData(rowno, ColumnName, (value)) ''Loinc
            If (strcriteria = "den") Then


                ''  C1CQMDetail.Cols(ColumnName)(rowno)
                Dim cr As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(rowno, ColumnName)

                cr.Style = cdenStyle
            Else
                Dim cr As C1.Win.C1FlexGrid.CellRange = C1CQMDetail.GetCellRange(rowno, ColumnName)
                cr.Style = cnumStyle
            End If
            rowno += 1
        End If
    End Sub
    Public Sub InitialiseSearchControl() 'ByRef dt As DataTable

        Try

            ogloUC_generalsearch = New gloUCGeneralSearch()
            Panel1.Controls.Add(ogloUC_generalsearch)
            ogloUC_generalsearch.Dock = DockStyle.Left
            ogloUC_generalsearch.BringToFront()

            ogloUC_generalsearchDenominator = New gloUCGeneralSearch()
            Panel8.Controls.Add(ogloUC_generalsearchDenominator)
            ogloUC_generalsearchDenominator.Dock = DockStyle.Left
            ogloUC_generalsearchDenominator.BringToFront()


            ogloUC_DenominatorExclusionsSearch = New gloUCGeneralSearch()
            plnDenominatorExclusionSearch.Controls.Add(ogloUC_DenominatorExclusionsSearch)
            ogloUC_DenominatorExclusionsSearch.Dock = DockStyle.Left
            ogloUC_DenominatorExclusionsSearch.BringToFront()


            ogloUC_DenominatorExceptionsSearch = New gloUCGeneralSearch()
            plnDenominatorExceptionsSearch.Controls.Add(ogloUC_DenominatorExceptionsSearch)
            ogloUC_DenominatorExceptionsSearch.Dock = DockStyle.Left
            ogloUC_DenominatorExceptionsSearch.BringToFront()


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    ''added for bugid 86630 for searching issue
    Private Sub oSearchProceduresCtl_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles ogloUC_generalsearch.AfterTextSearch
        Try
            If Not IsNothing(dv) Then
                C1Numerator.DataSource = dv
                'C1Numerator.Cols(0).Visible = False
                'C1Numerator.AllowEditing = False
                'C1Numerator.Cols(1).Width = 100
                'C1Numerator.Cols(2).Width = 400
                SetNumGridColumn()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    ''added for bugid 86630 for searching issue
    Private Sub oSearchProceduresDenominator_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles ogloUC_generalsearchDenominator.AfterTextSearch
        Try
            If Not IsNothing(dv) Then
                C1Denominator.DataSource = Nothing
                C1Denominator.DataSource = dv
                SetDenoGridColumn()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    ''added for bugid 86630 for searching issue
    Private Sub oSearchProceduresExclusion_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles ogloUC_DenominatorExclusionsSearch.AfterTextSearch
        Try
            If Not IsNothing(dv) Then
                C1DenominatorExclusions.DataSource = dv
                C1DenominatorExclusions.Cols(0).Visible = False
                C1DenominatorExclusions.AllowEditing = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    ''added for bugid 86630 for searching issue
    Private Sub oSearchProceduresDenoException_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles ogloUC_DenominatorExceptionsSearch.AfterTextSearch
        Try
            If Not IsNothing(dv) Then
                C1DenominatorExceptions.Cols(0).Visible = False
                C1DenominatorExceptions.AllowEditing = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub





    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub



    Private Sub C1Numerator_DoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Numerator.DoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1Numerator.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                Dim PatientID As Int64 = -1
                PatientID = Convert.ToInt64(C1Numerator.GetData(C1Numerator.Row, 0).ToString())
                If PatientID > 0 Then
                    Try
                        CType(Me.ParentForm, Object).SetGnPatientID = PatientID
                        CType(Me.ParentForm, Object).mnu_DashBoard_Click(Nothing, Nothing)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1Denominator_DoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Denominator.DoubleClick

        Try

            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1Denominator.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                Dim PatientID As Int64 = -1
                PatientID = Convert.ToInt64(C1Denominator.GetData(C1Denominator.Row, 0).ToString())
                If PatientID > 0 Then
                    Try
                        CType(Me.ParentForm, Object).SetGnPatientID = PatientID
                        CType(Me.ParentForm, Object).mnu_DashBoard_Click(Nothing, Nothing)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub


    Private Sub C1DenominatorExceptions_DoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1DenominatorExceptions.DoubleClick

        Try

            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1DenominatorExceptions.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                Dim PatientID As Int64 = -1
                PatientID = Convert.ToInt64(C1DenominatorExceptions.GetData(C1DenominatorExceptions.Row, 0).ToString())
                If PatientID > 0 Then
                    Try
                        CType(Me.ParentForm, Object).SetGnPatientID = PatientID
                        CType(Me.ParentForm, Object).mnu_DashBoard_Click(Nothing, Nothing)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub C1DenominatorExclusions_DoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1DenominatorExclusions.DoubleClick

        Try

            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1DenominatorExclusions.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                Dim PatientID As Int64 = -1
                PatientID = Convert.ToInt64(C1DenominatorExclusions.GetData(C1DenominatorExclusions.Row, 0).ToString())
                If PatientID > 0 Then
                    Try
                        CType(Me.ParentForm, Object).SetGnPatientID = PatientID
                        CType(Me.ParentForm, Object).mnu_DashBoard_Click(Nothing, Nothing)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub ts_btndetail_Click(sender As Object, e As System.EventArgs) Handles ts_btndetail.Click
        Dim oDMSetup As New frmCQM_RulesSetup()
        ' oDMSetup.MdiParent = Me.MdiParent

        oDMSetup._databaseConnectionString = _databaseConnectionString
        oDMSetup.WindowState = FormWindowState.Maximized
        ' oDMSetup.StartPosition = FormStartPosition.CenterScreen
        oDMSetup.ShowInTaskbar = False
        oDMSetup.ReportingYear = _ReportingYear
        oDMSetup.UserID = gloGlobal.gloPMGlobal.UserID
        oDMSetup.UserName = gloGlobal.gloPMGlobal.UserName
        oDMSetup.CMSNO = Label33.Text
        oDMSetup.PopulationNo = _PopulationNo
        AddHandler oDMSetup.EvtShowHelp, AddressOf ShowHelp
        oDMSetup.StartPosition = FormStartPosition.CenterParent
        oDMSetup.Text = Me.Text
        Dim dr As DialogResult = oDMSetup.ShowDialog(IIf(IsNothing(oDMSetup.Parent), Me, oDMSetup.Parent))
        If dr = Windows.Forms.DialogResult.OK Then
            FillCQmDetails()
        End If

        Me.Cursor = Cursors.Arrow
        oDMSetup.Dispose()

    End Sub
    Private Sub ShowHelp()

        Dim frmobjDb As Object = Application.OpenForms("MainMenu")
        frmobjDb.ShowHelpobj(Me)
    End Sub
    Private Sub C1Numerator_DoubleClick(sender As System.Object, e As System.EventArgs) Handles C1Numerator.DoubleClick

    End Sub

    Private Sub lnkhelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkhelp.LinkClicked

        Dim frmobjDb As Object = Application.OpenForms("MainMenu")
        frmobjDb.ShowHelpobj(Me)

    End Sub

    Private Sub C1Numerator_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1Numerator.MouseClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1Numerator.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                If MeasureID = "NQF0419" Then
                    If htInfo.Column = numimagecolumn_0419 Then
                        dsqrda1Criteria = GetQRDA1Data(MeasureID, C1Numerator.Rows(C1Numerator.Row)("nPatientId"))
                        Dim frmCQMCritria As New frmViewCQMCriteriaReason(dsqrda1Criteria, C1Numerator.Rows(C1Numerator.Row)("nPatientId"), "Numerator", _MeasureName)
                        frmCQMCritria.ReportingYear = _ReportingYear
                        frmCQMCritria.ShowDialog(Me)
                    End If
                Else
                    If htInfo.Column = 3 Then
                        dsqrda1Criteria = GetQRDA1Data(MeasureID, C1Numerator.Rows(C1Numerator.Row)(0))
                        Dim frmCQMCritria As New frmViewCQMCriteriaReason(dsqrda1Criteria, C1Numerator.Rows(C1Numerator.Row)(0), "Numerator", _MeasureName)
                        frmCQMCritria.ReportingYear = _ReportingYear
                        frmCQMCritria.ShowDialog(Me)
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub C1Numerator_OwnerDrawCell(sender As Object, e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles C1Numerator.OwnerDrawCell
        If MeasureID = "NQF0419" Then
            If e.Col = numimagecolumn_0419 And e.Row > 0 Then
                e.Image = ImgFlag.Images(0)
            End If
        Else
            If e.Col = 3 And e.Row > 0 Then
                e.Image = ImgFlag.Images(0)
            End If
        End If
    End Sub
    Private Sub C1Numerator_MouseEnterCell(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Numerator.MouseEnterCell
        If MeasureID = "NQF0419" Then
            If e.Row > 0 Then
                If e.Col = numimagecolumn_0419 Then
                    Me.C1SuperTooltip1.SetToolTip(C1Numerator, "View CQM Details")
                Else
                    C1SuperTooltip1.Hide()
                End If
            Else
                C1SuperTooltip1.Hide()
            End If
        Else
            If e.Row > 0 Then
                If e.Col = 3 Then
                    Me.C1SuperTooltip1.SetToolTip(C1Numerator, "View CQM Details")
                Else
                    C1SuperTooltip1.Hide()
                End If
            Else
                C1SuperTooltip1.Hide()
            End If
        End If

    End Sub



    Private Sub C1Denominator_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1Denominator.MouseClick
        Try

            Me.Cursor = Cursors.WaitCursor
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1Denominator.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                Dim IsInExclusion As Boolean = False
                If MeasureID = "NQF0419" Then
                    If htInfo.Column = denoimagecolumn_0419 Then
                        Dim dr As DataRow() = dsCQMDetailReport.Tables("DenominatorExclusions").Select("PatientId = " & C1Denominator.Rows(C1Denominator.Row)("nPatientId") & "")
                        If dr.Length > 0 Then
                            IsInExclusion = True
                        End If
                        dsqrda1Criteria = GetQRDA1Data(MeasureID, C1Denominator.Rows(C1Denominator.Row)("nPatientId"))
                        Dim frmCQMCritria As New frmViewCQMCriteriaReason(dsqrda1Criteria, C1Denominator.Rows(C1Denominator.Row)("nPatientId"), "Denominator", _MeasureName, IsInExclusion)
                        frmCQMCritria.ReportingYear = _ReportingYear
                        frmCQMCritria.ShowDialog(Me)
                    End If
                Else
                    If htInfo.Column = 3 Then
                        Dim dr As DataRow() = dsCQMDetailReport.Tables("DenominatorExclusions").Select("PatientId = " & C1Denominator.Rows(C1Denominator.Row)(0) & "")
                        If dr.Length > 0 Then
                            IsInExclusion = True
                        End If
                        dsqrda1Criteria = GetQRDA1Data(MeasureID, C1Denominator.Rows(C1Denominator.Row)(0))
                        Dim frmCQMCritria As New frmViewCQMCriteriaReason(dsqrda1Criteria, C1Denominator.Rows(C1Denominator.Row)(0), "Denominator", _MeasureName, IsInExclusion)
                        frmCQMCritria.ReportingYear = _ReportingYear
                        frmCQMCritria.ShowDialog(Me)
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        'If C1Denominator.Col = 3 Then

        'End If

    End Sub
    Private Sub C1Denominator_MouseEnterCell(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Denominator.MouseEnterCell
        If MeasureID = "NQF0419" Then
            If e.Row > 0 Then
                If e.Col = denoimagecolumn_0419 Then
                    Me.C1SuperTooltip1.SetToolTip(C1Denominator, "View CQM Details")
                Else
                    C1SuperTooltip1.Hide()
                End If
            Else
                C1SuperTooltip1.Hide()
            End If
        Else
            If e.Row > 0 Then
                If e.Col = 3 Then
                    Me.C1SuperTooltip1.SetToolTip(C1Denominator, "View CQM Details")
                Else
                    C1SuperTooltip1.Hide()
                End If
            Else
                C1SuperTooltip1.Hide()
            End If
        End If
    End Sub

    Private Sub C1Denominator_OwnerDrawCell(sender As Object, e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles C1Denominator.OwnerDrawCell
        If MeasureID = "NQF0419" Then
            If e.Col = denoimagecolumn_0419 And e.Row > 0 Then
                e.Image = ImgFlag.Images(0)
            End If
        Else
            If e.Col = 3 And e.Row > 0 Then
                e.Image = ImgFlag.Images(0)
            End If
        End If
    End Sub
    Private Sub SetDenoGridColumn()
        Try
            If MeasureID = "NQF0419" Then
                C1Denominator.Cols.Count = 6
                denoimagecolumn_0419 = 5
                C1Denominator.SetData(0, denoimagecolumn_0419, "")
                C1Denominator.Cols(denoimagecolumn_0419).DataType = GetType(System.Drawing.Image)
                C1Denominator.Cols(denoimagecolumn_0419).Width = 20
                'C1Denominator.Redraw = True
                C1Denominator.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                AddHandler C1Denominator.OwnerDrawCell, AddressOf C1Denominator_OwnerDrawCell
                C1Denominator.Cols(0).Visible = False
                C1Denominator.Cols(1).Visible = False
                C1Denominator.Cols("MeasureName").Visible = False
                C1Denominator.Cols("Patient Code").Width = 100
                C1Denominator.Cols("Patient Name").Width = 420
            Else
                C1Denominator.Cols.Count = 4
                C1Denominator.Cols(1).Width = 100
                C1Denominator.Cols(2).Width = 520
                C1Denominator.Cols(0).Visible = False
                C1Denominator.AllowEditing = False
                C1Denominator.SetData(0, 3, "")
                C1Denominator.Cols(3).DataType = GetType(System.Drawing.Image)
                C1Denominator.Cols(3).Visible = True
                'C1Denominator.Redraw = True
                C1Denominator.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                AddHandler C1Denominator.OwnerDrawCell, AddressOf C1Denominator_OwnerDrawCell
                C1Denominator.Cols(3).Width = 20
            End If


        Catch ex As Exception

        End Try

    End Sub
    Private Sub SetNumGridColumn()
        If MeasureID = "NQF0419" Then
            'C1Numerator.Cols.Count = C1Numerator.Cols.Count + 1
            'numimagecolumn_0419 = C1Numerator.Cols.Count - 1
            C1Numerator.Cols.Count = 6
            numimagecolumn_0419 = 5
            C1Numerator.SetData(0, numimagecolumn_0419, "")
            C1Numerator.Cols(numimagecolumn_0419).DataType = GetType(System.Drawing.Image)
            C1Numerator.Cols(numimagecolumn_0419).Width = 20
            'C1Numerator.Redraw = True
            C1Numerator.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
            AddHandler C1Numerator.OwnerDrawCell, AddressOf C1Numerator_OwnerDrawCell
            C1Numerator.Cols(0).Visible = False
            C1Numerator.Cols(1).Visible = False
            C1Numerator.Cols(2).Width = 100
            C1Numerator.Cols(3).Width = 350
            C1Numerator.Cols(4).Width = 100
        Else
            C1Numerator.Cols.Count = 4
            C1Numerator.Cols(0).Visible = False
            C1Numerator.AllowEditing = False
            C1Numerator.Cols(1).Width = 100
            C1Numerator.Cols(2).Width = 450
            C1Numerator.SetData(0, 3, "")
            C1Numerator.Cols(3).DataType = GetType(System.Drawing.Image)
            C1Numerator.Cols(3).Visible = True
            'C1Numerator.Redraw = True
            C1Numerator.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
            AddHandler C1Numerator.OwnerDrawCell, AddressOf C1Numerator_OwnerDrawCell
            C1Numerator.Cols(3).Width = 20

        End If

    End Sub

    Private Sub frm_CQM_Detail_Report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Me.ReportingYear = "2017" Then
        '    Me.Name = "frm_CQM_Detail_Report_2017"
        'Else
        '    Me.Name = "frm_CQM_Detail_Report"



        'End If
    End Sub
End Class