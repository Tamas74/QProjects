Imports System.Windows.Forms
Imports gloUserControlLibrary
Imports System.Drawing

Public Class frm_MU_Detail_Report
    Shared ofrm_MU_Detail_Report As frm_MU_Detail_Report = Nothing
    Dim SPName As String
    Dim _databaseConnectionString As String
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim Ds As DataSet
    Dim _StartDate As String
    Dim _EndDate As String

    Dim _ProviderID As DataTable
    Private _SingleProvider As Int64
    Private _TIN As String
    Private _blnIsReportingYear As Boolean = True

    Private WithEvents ogloUC_generalsearch As gloUserControlLibrary.gloUCGeneralSearch
    Private WithEvents ogloUC_generalsearchDenominator As gloUserControlLibrary.gloUCGeneralSearch
    Dim isSearchadded As Boolean = False


    Public Property IsReportingYear As Boolean
        Get
            Return _blnIsReportingYear
        End Get
        Set(value As Boolean)
            _blnIsReportingYear = value
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

    Public Property SingleProvider As Int64

        Get
            Return _SingleProvider
        End Get
        Set(ByVal value As Int64)
            _SingleProvider = value
        End Set
    End Property

    Public Property TIN As String

        Get
            Return _TIN
        End Get
        Set(ByVal value As String)
            _TIN = value
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

    Private Sub frm_MU_Detail_Report_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ofrm_MU_Detail_Report = Nothing
    End Sub
    Private Sub frm_MU_Detail_Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Shared Function GetInstance() As frm_MU_Detail_Report

        Dim frm As frm_MU_Detail_Report = Nothing
        Dim IsOpen As Boolean
        Try
            IsOpen = False
            ''If frm Is Nothing Then
            For Each f As Form In Application.OpenForms
                If f.Name = "frm_MU_Detail_Report" Then
                    IsOpen = True
                    frm = f
                End If
            Next
            If (IsOpen = False) Then
                frm = New frm_MU_Detail_Report()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

        End Try
        Return frm
    End Function

    Public Sub RefreshContent()

        Try

            Dim oBAL As New cls_BAL_MU_Detail_Report(_databaseConnectionString)
            Ds = oBAL.GetDetailReportingDataSet(SPName, _ProviderID, _StartDate, _EndDate, _SingleProvider, _TIN, _blnIsReportingYear)
            C1Numerator.AutoResize = True
            C1Denominator.AutoResize = True

            C1Numerator.DataSource = Ds.Tables(0)
            C1Numerator.Cols(0).Visible = False
            C1Numerator.AllowEditing = False



            'If SPName = "MU_CPOE_MedicationOrders_MU2013" Then
            '    C1Numerator.Cols(1).Width = 70
            '    C1Numerator.Cols(2).Width = 150
            '    C1Numerator.Cols(3).Width = 70
            '    C1Numerator.Cols(4).Width = 200

            'Else
            '    C1Numerator.Cols(1).Width = 100
            '    C1Numerator.Cols(2).Width = 300
            '    If C1Numerator.Cols.Count > 3 Then
            '        C1Numerator.Cols(3).Width = 200
            '    End If
            'End If


            C1Denominator.AllowEditing = False
            C1Denominator.DataSource = Ds.Tables(1)
            C1Denominator.Cols(0).Visible = False
            C1Denominator.AllowEditing = False

            'C1Denominator.Cols(1).Width = 70

            'If SPName = "MU_CPOE_MedicationOrders_MU2013" Then
            '    lblNotSatisfied.Text = "Patients seen who do NOT meet objective criteria :"
            '    C1Denominator.Cols(1).Width = 70
            '    C1Denominator.Cols(2).Width = 150
            '    C1Denominator.Cols(3).Width = 70
            '    C1Denominator.Cols(4).Width = 200
            'Else
            '    C1Denominator.Cols(1).Width = 100
            '    C1Denominator.Cols(2).Width = 300
            '    If C1Denominator.Cols.Count > 3 Then
            '        C1Denominator.Cols(3).Width = 200
            '    End If
            'End If



            If (isSearchadded = False) Then
                InitiliseSearchControl(Ds.Tables(0))
                isSearchadded = True
            End If

            ogloUC_generalsearch.IntialiseDatatable(Ds.Tables(0))
            ogloUC_generalsearchDenominator.IntialiseDatatable(Ds.Tables(1))

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Public Sub SetReportingParameters(ByVal MeasureString As String, ByVal ProviderName As String, ByVal NPIValue As String, ByVal ReportingYear As String, ByVal ReportName As String, ByVal TaxIDVAlue As String, ByVal IsFirstYear As Boolean, ByVal StartdateVal As Date, ByVal EndDateVal As Date, ByVal Reporting_Period_In_Progress As String)
        Try
            lblProviderName.Text = ProviderName
            lblNPI.Text = NPIValue
            lblReportingYear.Text = ReportingYear
            lblReportName.Text = ReportName
            lbl_TaxIDValue.Text = TaxIDVAlue
            chk_FirstYear.Checked = IsFirstYear
            dtpicStartDate.Value = StartdateVal
            dtpicEndDate.Value = EndDateVal
            Label10.Text = Reporting_Period_In_Progress
            Label33.Text = "Measure : " + MeasureString
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub New()
        Try
            ' This call is required by the designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            If appSettings("DataBaseConnectionString") IsNot Nothing Then
                If appSettings("DataBaseConnectionString") <> "" Then
                    _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub InitiliseSearchControl(ByRef dt As DataTable)
        Try
            ogloUC_generalsearch = New gloUCGeneralSearch()
            Panel1.Controls.Add(ogloUC_generalsearch)
            ogloUC_generalsearch.Dock = DockStyle.Left
            ogloUC_generalsearch.BringToFront()

            ogloUC_generalsearchDenominator = New gloUCGeneralSearch()
            Panel8.Controls.Add(ogloUC_generalsearchDenominator)
            ogloUC_generalsearchDenominator.Dock = DockStyle.Left
            ogloUC_generalsearchDenominator.BringToFront()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

   
End Class























