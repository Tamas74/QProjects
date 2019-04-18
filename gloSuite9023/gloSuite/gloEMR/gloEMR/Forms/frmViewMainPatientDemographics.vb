Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmViewMainPatientDemographics
    Inherits System.Windows.Forms.Form
    Implements IPatientContext

    Dim orpt As ReportDocument
    Dim PatientID As Int64
    Dim Fromdate As DateTime
    Dim ToDate As DateTime
    Dim m_HistoryFlag As Boolean
    Dim m_MedicationFlag As Boolean
    Dim m_PrescriptionFlag As Boolean
    Private m_ProblemListFlag As Boolean
    Private m_DiagnosisFlag As Boolean
    Private m_TreatmentFlag As Boolean
    Private m_ROSFlag As Boolean
    Private m_PatientEducationFlag As Boolean
    Private m_MessagesFlag As Boolean
    Private m_VitalsFlag As Boolean
    Private m_ExamFlag As Boolean
    Private m_DemographicsFlag As Boolean
    Private m_InsuranceFlag As Boolean
    Private m_ConsentFlag As Boolean
    Private m_FlowSheetFlag As Boolean
    Private m_OrdersFlag As Boolean


    ''----------------------------------------Added by Anil on 20071205
    Dim m_strProviderIDs As String
    Dim m_Fromdate As DateTime
    Dim m_ToDate As DateTime
    'Public mycaller As frmVWAppointment
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Public FormName As String = ""


    Public Property ProviderIDs() As String
        Get
            Return m_strProviderIDs
        End Get
        Set(ByVal Value As String)
            m_strProviderIDs = Value
        End Set
    End Property

    Public Property AppFromDate() As Date
        Get
            Return m_Fromdate
        End Get
        Set(ByVal Value As Date)
            m_Fromdate = Value
        End Set
    End Property

    Public Property AppToDate() As Date
        Get
            Return m_ToDate
        End Get
        Set(ByVal Value As Date)
            m_ToDate = Value
        End Set
    End Property

    ''-------------------------------------------------------------------

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal m_PatientId As Int64)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        PatientID = m_PatientId
        'Add any initialization after the InitializeComponent() call
    End Sub
    Public Sub New(ByVal m_PatientId As Int64, ByVal m_fromdate As DateTime, ByVal m_todate As DateTime)
        MyBase.New()
        PatientID = m_PatientId
        Fromdate = m_fromdate
        ToDate = m_todate
        'This call is required by the Windows Form Designer.
        InitializeComponent()

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
    Friend WithEvents pnlbottom As System.Windows.Forms.Panel
    Friend WithEvents pnlFill As System.Windows.Forms.Panel
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewMainPatientDemographics))
        Me.pnlbottom = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.pnlFill = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.pnlbottom.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlFill.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlbottom
        '
        Me.pnlbottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbottom.Controls.Add(Me.ToolStrip1)
        Me.pnlbottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlbottom.Name = "pnlbottom"
        Me.pnlbottom.Size = New System.Drawing.Size(1028, 53)
        Me.pnlbottom.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1028, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton1.Text = "&Close"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlFill
        '
        Me.pnlFill.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlFill.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlFill.Controls.Add(Me.lbl_RightBrd)
        Me.pnlFill.Controls.Add(Me.lbl_TopBrd)
        Me.pnlFill.Controls.Add(Me.CrystalReportViewer1)
        Me.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFill.Location = New System.Drawing.Point(0, 53)
        Me.pnlFill.Name = "pnlFill"
        Me.pnlFill.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlFill.Size = New System.Drawing.Size(1028, 693)
        Me.pnlFill.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 689)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(1020, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 686)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(1024, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 686)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(1022, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.EnableDrillDown = False
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(3, 3)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.ShowGroupTreeButton = False
        Me.CrystalReportViewer1.ShowRefreshButton = False
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1022, 687)
        Me.CrystalReportViewer1.TabIndex = 2
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'frmViewMainPatientDemographics
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.pnlFill)
        Me.Controls.Add(Me.pnlbottom)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewMainPatientDemographics"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Patient Demographics"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlbottom.ResumeLayout(False)
        Me.pnlbottom.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlFill.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub PrintReport()

        Try

      
            orpt = New ReportDocument
            orpt.Load(Application.StartupPath & "\Reports\rptDemographics.rpt")

            MapDatabaseInfo(orpt)

            Dim prm As ParameterValues
            Dim discreteval As ParameterDiscreteValue

            prm = orpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_HistoryFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_HistoryFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(2).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_PrescriptionFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(2).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(3).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_ROSFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(3).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(4).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_PatientEducationFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(4).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(5).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_MessagesFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(5).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(6).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_ExamFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(6).ApplyCurrentValues(prm)


            prm = orpt.DataDefinition.ParameterFields.Item(7).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_VitalsFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(7).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(8).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_InsuranceFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(8).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(9).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_DemographicsFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(9).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(10).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_ProblemListFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(10).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(11).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = Fromdate
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(11).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(12).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = ToDate
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(12).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(13).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = PatientID.ToString
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(13).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(14).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_ConsentFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(14).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(15).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_FlowSheetFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(15).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(16).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_OrdersFlag
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(16).ApplyCurrentValues(prm)

            CrystalReportViewer1.ReportSource = orpt

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub MapDatabaseInfo(ByVal rpt As ReportDocument)

        Dim crConnectionInfo As New ConnectionInfo
        Try

     
            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName
                .IntegratedSecurity = True

                '.UserID = "Your User ID"
                '.Password = "Your Password"
            End With

            SetDBLogonForReport(crConnectionInfo, rpt)
            SetDBLogonForSubreports(crConnectionInfo, rpt)

            'MapTableInfo(crConnectionInfo, rpt)
            'Dim objsubrpt As SubreportObject
            'Dim objrpt As ReportDocument

            'objsubrpt = rpt.ReportDefinition.Sections.Item(2).ReportObjects(0)
            'objrpt = New ReportDocument
            'objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            'MapTableInfo(crConnectionInfo, objrpt)

            'objsubrpt = rpt.ReportDefinition.Sections.Item(3).ReportObjects(0)
            'objrpt = New ReportDocument
            'objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            'MapTableInfo(crConnectionInfo, objrpt)

            'Dim i As Integer
            'For i = 5 To 12
            '    objsubrpt = rpt.ReportDefinition.Sections.Item(i).ReportObjects(0)

            '    objrpt = New ReportDocument
            '    objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            '    MapTableInfo(crConnectionInfo, objrpt)
            'Next
            'objsubrpt = rpt.ReportDefinition.Sections.Item(14).ReportObjects(0)
            'objrpt = New ReportDocument
            'objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            'MapTableInfo(crConnectionInfo, objrpt)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub SetDBLogonForReport(ByVal connectionInfo As ConnectionInfo, ByVal reportDocument As ReportDocument)
        Dim tables As Tables = reportDocument.Database.Tables

        Try

    
            For Each table As CrystalDecisions.CrystalReports.Engine.Table In tables
                Dim tableLogonInfo As TableLogOnInfo = table.LogOnInfo
                tableLogonInfo.ConnectionInfo = connectionInfo
                tableLogonInfo.TableName = table.Name
                ' Added to try and make other databases work. 
                table.ApplyLogOnInfo(tableLogonInfo)
                table.Location = table.Name
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetDBLogonForSubreports(ByVal connectionInfo As ConnectionInfo, ByVal reportDocument As ReportDocument)
        Dim sections As Sections = reportDocument.ReportDefinition.Sections


        Try

     
            For Each section As Section In sections
                Dim reportObjects As ReportObjects = section.ReportObjects
                For Each reportObject As ReportObject In reportObjects
                    If reportObject.Kind = ReportObjectKind.SubreportObject Then
                        Dim subreportObject As SubreportObject = DirectCast(reportObject, SubreportObject)
                        Dim subreportDocument As ReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName)
                        SetDBLogonForReport(connectionInfo, subreportDocument)
                    End If
                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MapTableInfo(ByVal crConnectionInfo As ConnectionInfo, ByVal rpt As ReportDocument)
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo

        Dim CrTables As Tables
        Dim CrTable As Table
        '  Dim TableCounter
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
            Throw ex
        End Try
    End Sub

    Private Sub frmViewMainPatientDemographics_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmViewMainPatientDemographics_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CrystalReportViewer1.ReportSource = Nothing
        CrystalReportViewer1.Dispose()
    End Sub

    Private Sub frmViewMainPatientDemographics_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            ' ''---------------------------Added by Anil on 20071205
            If FormName = "Appointments" Then
                'PrintAppointmentReport()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            ' ''------------------------------------------------
            'Sanjog - Added on 2011 May 17 for Patient Safety
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Sanjog - Added on 2011 May 17 for Patient Safety
            PrintReport()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to load Report", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    ''--------------------------------------Added by Anil on 20071205
    'Public Sub PrintAppointmentReport(ByVal ShowPreview As Boolean)
    '    Try
    '        orpt = New ReportDocument
    '        orpt.Load(Application.StartupPath & "\Reports\rptAppointment.rpt")

    '        Dim crConnectionInfo As New ConnectionInfo

    '        With crConnectionInfo
    '            .ServerName = gstrSQLServerName
    '            .DatabaseName = gstrDatabaseName
    '            .IntegratedSecurity = True

    '        End With
    '        MapTableInfo(crConnectionInfo, orpt)

    '        Dim prm As ParameterValues
    '        Dim discreteval As ParameterDiscreteValue
    '        prm = orpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
    '        prm.Clear()

    '        For i As Int32 = 1 To mycaller.clProviders.Count

    '            discreteval = New ParameterDiscreteValue

    '            discreteval.Value = mycaller.GetProviderID(mycaller.clProviders.Item(i)).ToString()
    '            prm.Add(discreteval)
    '            discreteval = Nothing
    '        Next
    '        orpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)
    '        orpt.SetParameterValue(1, AppFromDate)
    '        orpt.SetParameterValue(2, AppToDate)
    '        If ShowPreview = False Then
    '            orpt.PrintToPrinter(1, False, 0, 0)
    '        Else
    '            CrystalReportViewer1.ReportSource = orpt
    '        End If

    '    Catch ex As Exception

    '        MessageBox.Show("Error : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Throw ex
    '    End Try
    'End Sub
    ''-------------------------------------------------------------------------------------

    Public Property HistoryFlag() As Boolean
        Get
            Return m_HistoryFlag
        End Get
        Set(ByVal Value As Boolean)
            m_HistoryFlag = Value
        End Set
    End Property
    Public Property ProblemListFlag() As Boolean
        Get
            Return m_ProblemListFlag
        End Get
        Set(ByVal Value As Boolean)
            m_ProblemListFlag = Value
        End Set
    End Property
    Public Property MedicationFlag() As Boolean
        Get
            Return m_MedicationFlag
        End Get
        Set(ByVal Value As Boolean)
            m_MedicationFlag = Value
        End Set
    End Property

    Public Property PrescriptionFlag() As Boolean
        Get
            Return m_PrescriptionFlag
        End Get
        Set(ByVal Value As Boolean)
            m_PrescriptionFlag = Value
        End Set
    End Property
    Public Property DiagnosisFlag() As Boolean
        Get
            Return m_DiagnosisFlag
        End Get
        Set(ByVal Value As Boolean)
            m_DiagnosisFlag = Value
        End Set
    End Property
    Public Property TreatmentFlag() As Boolean
        Get
            Return m_TreatmentFlag
        End Get
        Set(ByVal Value As Boolean)
            m_TreatmentFlag = Value
        End Set
    End Property
    Public Property ROSFlag() As Boolean
        Get
            Return m_ROSFlag
        End Get
        Set(ByVal Value As Boolean)
            m_ROSFlag = Value
        End Set
    End Property
    Public Property PatientEducationFlag() As Boolean
        Get
            Return m_PatientEducationFlag
        End Get
        Set(ByVal Value As Boolean)
            m_PatientEducationFlag = Value
        End Set
    End Property
    Public Property MessagesFlag() As Boolean
        Get
            Return m_MessagesFlag
        End Get
        Set(ByVal Value As Boolean)
            m_MessagesFlag = Value
        End Set
    End Property
    Public Property ExamFlag() As Boolean
        Get
            Return m_ExamFlag
        End Get
        Set(ByVal Value As Boolean)
            m_ExamFlag = Value
        End Set
    End Property
    Public Property VitalFlag() As Boolean
        Get
            Return m_VitalsFlag
        End Get
        Set(ByVal Value As Boolean)
            m_VitalsFlag = Value
        End Set
    End Property
    Public Property DemographicsFlag() As Boolean
        Get
            Return m_DemographicsFlag
        End Get
        Set(ByVal Value As Boolean)
            m_DemographicsFlag = Value
        End Set
    End Property
    Public Property InsuranceFlag() As Boolean
        Get
            Return m_InsuranceFlag
        End Get
        Set(ByVal Value As Boolean)
            m_InsuranceFlag = Value
        End Set
    End Property

    Public Property ConsentFlag() As Boolean
        Get
            Return m_ConsentFlag
        End Get
        Set(ByVal Value As Boolean)
            m_ConsentFlag = Value
        End Set
    End Property

    Public Property FlowSheetFlag() As Boolean
        Get
            Return m_FlowSheetFlag
        End Get
        Set(ByVal Value As Boolean)
            m_FlowSheetFlag = Value
        End Set
    End Property

    Public Property OrdersFlag() As Boolean
        Get
            Return m_OrdersFlag
        End Get
        Set(ByVal Value As Boolean)
            m_OrdersFlag = Value
        End Set
    End Property



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
       
        Me.Close()
    End Sub

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

End Class

