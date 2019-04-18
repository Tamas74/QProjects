Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports gloEMRReports
Public Class frmHCFAViewer
    Inherits System.Windows.Forms.Form
    Private m_fromdate As String
    Private m_ToDate As String
    Private m_PatientId As Int64
    Private ArrDate As New ArrayList
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Dim oRpt As ReportDocument
    Dim _oCPT As rpt_CptDriven
    Dim _oICD9 As Rpt_HCFA_ICD9Driven
#Region " Windows Form Designer generated code "

    Public Sub New(ByVal m_ArrDate As ArrayList)
        MyBase.New()
        ArrDate = m_ArrDate
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal m_ArrDate As ArrayList, ByVal oICD9 As Rpt_HCFA_ICD9Driven)
        MyBase.New()
        ArrDate = m_ArrDate
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _oICD9 = oICD9
        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal m_ArrDate As ArrayList, ByVal oCPT As rpt_CptDriven)
        MyBase.New()
        ArrDate = m_ArrDate
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _oCPT = oCPT
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHCFAViewer))
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnl_Base.SuspendLayout()
        Me.SuspendLayout()
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.EnableDrillDown = False
        Me.CrystalReportViewer1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(3, 3)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.ShowRefreshButton = False
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1022, 740)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lbl_pnlBottom)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlLeft)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlRight)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlTop)
        Me.pnl_Base.Controls.Add(Me.CrystalReportViewer1)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 0)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(1028, 746)
        Me.pnl_Base.TabIndex = 1
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 742)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(1020, 1)
        Me.lbl_pnlBottom.TabIndex = 4
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 739)
        Me.lbl_pnlLeft.TabIndex = 3
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(1024, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 739)
        Me.lbl_pnlRight.TabIndex = 2
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(1022, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'frmHCFAViewer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.pnl_Base)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHCFAViewer"
        Me.ShowInTaskbar = False
        Me.Text = "HCFA Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnl_Base.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmHCFAViewer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CrystalReportViewer1.ReportSource = Nothing
        CrystalReportViewer1.Dispose()

    End Sub

    Private Sub frmHCFAViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim _ExamID As String = ""

        If ArrDate.Count > 0 Then
            For i As Integer = 0 To ArrDate.Count - 1
                _ExamID = _ExamID & "'" & ArrDate(i) & "',"
            Next
        End If


        'Added By Shweta 20091205 
        'To print the report as per the setting in admin 
        If gblnICD9Driven = True Then

            'Commented by shweta 20091231
            'Try
            '    Dim oRpt As ReportDocument
            '    Dim _strPath As String = gstrgloEMRStartupPath & "\Reports\rptHCFA.rpt"
            '    oRpt = New ReportDocument
            '    oRpt.Load(_strPath)
            '    Dim crtableLogoninfos As New TableLogOnInfos
            '    Dim crtableLogoninfo As New TableLogOnInfo
            '    Dim crConnectionInfo As New ConnectionInfo
            '    Dim CrTables As Tables
            '    Dim CrTable As Table
            '    Dim TableCounter

            '    With crConnectionInfo
            '        .AllowCustomConnection = True
            '        .ServerName = gstrSQLServerName
            '        'If you are connecting to Oracle there is no 
            '        'DatabaseName. Use an empty string. 
            '        'For example, .DatabaseName = "" 
            '        .DatabaseName = gstrDatabaseName
            '        .UserID = gstrSQLUserEMR
            '        .Password = gstrSQLPasswordEMR
            '        .IntegratedSecurity = Not gblnSQLAuthentication
            '    End With

            '    'This code works for both user tables and stored 
            '    'procedures. Set the CrTables to the Tables collection 
            '    'of the report 

            '    CrTables = oRpt.Database.Tables

            '    'Loop through each table in the report and apply the 
            '    'LogonInfo information 

            '    For Each CrTable In CrTables
            '        crtableLogoninfo = CrTable.LogOnInfo
            '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '        CrTable.ApplyLogOnInfo(crtableLogoninfo)

            '        'If your DatabaseName is changing at runtime, specify 
            '        'the table location. 
            '        'For example, when you are reporting off of a 
            '        'Northwind database on SQL server you 
            '        'should have the following line of code: 

            '        CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name

            '    Next

            '    'Try
            '    '    'AddParameters()
            '    '    Me.Cursor = Cursors.WaitCursor
            '    '    oRpt = New ReportDocument
            '    '    oRpt.Load(Application.StartupPath & "\Reports\rptHIPAA.rpt")
            '    '    MapDatabaseInfo(oRpt)

            '    '    'CrystalReportViewer1.RefreshReport()

            '    '    Dim prm As ParameterValues
            '    '    Dim discreteval As ParameterDiscreteValue

            '    '    prm = oRpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
            '    '    prm.Clear()
            '    '    discreteval = New ParameterDiscreteValue
            '    '    discreteval.Value = m_PatientId
            '    '    prm.Add(discreteval)
            '    '    oRpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)

            '    '    prm = oRpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
            '    '    prm.Clear()
            '    '    discreteval = New ParameterDiscreteValue
            '    '    discreteval.Value = m_fromdate
            '    '    prm.Add(discreteval)
            '    '    oRpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)

            '    '    prm = oRpt.DataDefinition.ParameterFields.Item(2).CurrentValues()
            '    '    prm.Clear()
            '    '    discreteval = New ParameterDiscreteValue
            '    '    discreteval.Value = m_ToDate
            '    '    prm.Add(discreteval)
            '    '    oRpt.DataDefinition.ParameterFields.Item(2).ApplyCurrentValues(prm)

            '    ''{sp_HCFAReport;1.ExamID}
            '    'If ArrDate.Count > 0 Then
            '    'Dim selectFormula As String = ""
            '    ''Dim i As Int16
            '    'For i = 0 To ArrDate.Count - 1
            '    'selectFormula = selectFormula & "{sp_RptHIPAA;1.ExamID} = " & CType(ArrDate.Item(i), Int64) & " or "
            '    'If i = ArrDate.Count - 1 Then
            '    'oRpt.DataDefinition.GroupSelectionFormula = selectFormula.Substring(0, selectFormula.Length - 4)
            '    'End If
            '    'Next
            '    'End If

            '    ''Selection formula at runtime to select records for a report
            '    Dim selectFormula As String = ""
            '    selectFormula = selectFormula & "{HIPPAExamReport.PatientID} = '" & m_PatientId.ToString & "' and " & "{HIPPAExamReport.DateofService} >= CDate ('" & m_fromdate.ToString & "') and " & "{HIPPAExamReport.DateofService}<= CDate ('" & m_ToDate.ToString & "')"
            '    oRpt.RecordSelectionFormula = selectFormula
            '    ''View report
            '    CrystalReportViewer1.ReportSource = oRpt
            '    ''
            '    Me.Cursor = Cursors.Default
            'Catch ex As Exception
            '    Me.Cursor = Cursors.Default
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End Try
            'End Commenting

            'Added by Shweta 20091231
            'To view ICD9 Driven report
            Try
                'Commented by Shweta 20100106
                'against the bug id: 5693 
                'Dim oICD9 As Rpt_HCFA_ICD9Driven = New Rpt_HCFA_ICD9Driven()
                'Dim objClsHCFAReport As ClsHCFAReport = New ClsHCFAReport

                '_ExamID = _ExamID.Substring(0, _ExamID.Length - 1)
                'If _ExamID <> "" Then

                '    oICD9 = objClsHCFAReport.CreateICD9Report(_ExamID)
                'End Commenting
                'Changed by Shweta 20100106
                'against the bug id: 5693 
                If _oICD9 IsNot Nothing Then
                    CrystalReportViewer1.ReportSource = _oICD9
                End If
                'End Coding 20100106
                '  End If
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            'End Code 20091231

        Else
            'Added by Shweta 20091207
            'To view  cpt driven report 
            Try
                'Commented by Shweta 20100106
                'against the bug id: 5693 
                'Dim oCpt As rpt_CptDriven = New rpt_CptDriven()
                'Dim objClsHCFAReport As ClsHCFAReport = New ClsHCFAReport
                '_ExamID = _ExamID.Substring(0, _ExamID.Length - 1)
                'If _ExamID <> "" Then
                '    'objClsHCFAReport.CreateReport(_ExamID, oCpt)
                '    oCpt = objClsHCFAReport.CreateReport(_ExamID)
                'End Commenting
                'Changed by Shweta 20100106
                'against the bug id: 5693 
                If _oCPT IsNot Nothing Then
                    CrystalReportViewer1.ReportSource = _oCPT
                End If
                'End Coding 20100106
                'End If
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
    End Sub
    Public WriteOnly Property FromDate() As String

        Set(ByVal Value As String)
            m_fromdate = Value
        End Set
    End Property
    Public WriteOnly Property ToDate() As String
        Set(ByVal Value As String)
            m_ToDate = Value
        End Set
    End Property
    Public WriteOnly Property PatientID() As Int64
        Set(ByVal Value As Int64)
            m_PatientId = Value
        End Set
    End Property
    Private Sub MapDatabaseInfo(ByVal rpt As ReportDocument)

        Dim crConnectionInfo As New ConnectionInfo
        Try

    
            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                '.UserID = "Your User ID"
                '.Password = "Your Password"

                .IntegratedSecurity = True
            End With
            MapTableInfo(crConnectionInfo, rpt)
            Dim objsubrpt As SubreportObject
            Dim objrpt As ReportDocument

            objsubrpt = rpt.ReportDefinition.Sections.Item(3).ReportObjects(0)
            objrpt = New ReportDocument
            objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            MapTableInfo(crConnectionInfo, objrpt)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub MapTableInfo(ByVal crConnectionInfo As ConnectionInfo, ByVal rpt As ReportDocument)
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo

        Dim CrTables As Tables
        Dim CrTable As Table
        'Dim TableCounter
        'This code works for both user tables and stored 
        'procedures. Set the CrTables to the Tables collection 
        'of the report 
        Try

      
            CrTables = rpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub
    'Private Sub AddParameters()
    '    ' Declare variables needed to pass the parameters
    '    ' to the viewer control.
    '    Dim paramFields As New ParameterFields
    '    Dim paramField As New ParameterField
    '    Dim discreteVal As New ParameterDiscreteValue

    '    ' The first parameter is a discrete parameter with multiple values.

    '    ' Set the name of the parameter field, this must match a 
    '    ' parameter in the report.
    '    paramField.ParameterFieldName = "@fromdate"
    '    paramField.ParameterType = ParameterType.StoreProcedureParameter
    '    paramField.ParameterValueKind = ParameterValueKind.StringParameter
    '    ' Set the first discrete value and pass it to the parameter
    '    discreteVal.Value = m_fromdate
    '    paramField.CurrentValues.Add(discreteVal)

    '    ' Add the parameter to the parameter fields collection.
    '    paramFields.Add(paramField)

    '    ' The second parameter is a range value. The paramField variable
    '    ' is set to new so the previous settings will not be overwritten.
    '    paramField = New ParameterField

    '    ' Set the name of the parameter field, this must match a
    '    ' parameter in the report.
    '    paramField.ParameterFieldName = "@Todate"
    '    paramField.ParameterType = ParameterType.StoreProcedureParameter
    '    paramField.ParameterValueKind = ParameterValueKind.StringParameter

    '    discreteVal = New ParameterDiscreteValue
    '    discreteVal.Value = m_ToDate
    '    paramField.CurrentValues.Add(discreteVal)

    '    ' Add the second parameter to the parameter fields collection.
    '    paramFields.Add(paramField)


    '    ' The second parameter is a range value. The paramField variable
    '    ' is set to new so the previous settings will not be overwritten.
    '    paramField = New ParameterField

    '    ' Set the name of the parameter field, this must match a
    '    ' parameter in the report.
    '    paramField.ParameterFieldName = "@PatientID"
    '    paramField.ParameterType = ParameterType.StoreProcedureParameter
    '    paramField.ParameterValueKind = ParameterValueKind.NumberParameter

    '    discreteVal = New ParameterDiscreteValue
    '    discreteVal.Value = m_PatientId
    '    paramField.CurrentValues.Add(discreteVal)

    '    ' Add the second parameter to the parameter fields collection.
    '    paramFields.Add(paramField)

    '    ' Set the parameter fields collection into the viewer control.
    '    CrystalReportViewer1.ParameterFieldInfo = paramFields


    'End Sub
End Class
