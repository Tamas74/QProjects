Imports System.Data.SqlClient
Imports gloEMR.gloStream

Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Drawing.Color
Imports System.Drawing.Printing

Namespace ReportDiagnosis

    Public Class frmPatientDiagnosis
        Inherits System.Windows.Forms.Form

        Private _PatientID As Long
        Private _IsExempted As Boolean
        Private _PatientCode As String
        Private _PatientName As String
        Private _PatientDOB As String
        Private _PatientAge As String
        Private _PatientGender As String
        Private _PatientSSN As String

        Private _VisitDate As Date
        Private _Count As Integer
        Private _FlowSheetID As Long
        Private _FlowSheetName As String
        Private _FlowSheetRecID As Long
        Private _VisitID As Long

        Private _dtOrderLabs As DataTable

        Private blnModify As Boolean
        Private storedPageSettings As PageSettings
        Private PrintPreviewDialog1 As New PrintPreviewDialog
        Private PrintDialog1 As New PrintDialog
        Private GridPrinter As DataGridPrinter
        Private dtgrid As DataTable

        Private ocls_LM_LabResult As New cls_LM_LabResult
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
        Private WithEvents tlsp_PatientDiagnosis As gloGlobal.gloToolStripIgnoreFocus
        Private WithEvents ts_btnShowReport As System.Windows.Forms.ToolStripButton
        Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
        Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
        Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
        Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
        Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
        Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip


        Private WithEvents dgCustomGrid As CustomDataGrid

        Enum enmAlign
            LeftCenter
            CenterCenter
            RightCenter
        End Enum


#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

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
                Try

                    If (IsNothing(dgCustomGrid) = False) Then
                        ' dgCustomGrid.TableStyles.Clear()
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dgCustomGrid)
                        dgCustomGrid.Dispose()
                        dgCustomGrid = Nothing
                    End If
                Catch ex As Exception

                End Try
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                Try
                    If (IsNothing(PrintDialog1) = False) Then
                        PrintDialog1.Dispose()
                        PrintDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(PrintPreviewDialog1) = False) Then
                        PrintPreviewDialog1.Dispose()
                        PrintPreviewDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents chklstbx As System.Windows.Forms.CheckedListBox
        Friend WithEvents lblCheckDiag As System.Windows.Forms.Label
        Friend WithEvents lblLabResult As System.Windows.Forms.Label
        Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
        Friend WithEvents chkLabResult As System.Windows.Forms.CheckedListBox
        Friend WithEvents txtSearchDia As System.Windows.Forms.TextBox
        Friend WithEvents txtLabResult As System.Windows.Forms.TextBox
        Friend WithEvents lblSearcgDia As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents rbDesc As System.Windows.Forms.RadioButton
        Friend WithEvents rbICD9Code As System.Windows.Forms.RadioButton
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientDiagnosis))
            Me.chklstbx = New System.Windows.Forms.CheckedListBox
            Me.lblCheckDiag = New System.Windows.Forms.Label
            Me.lblLabResult = New System.Windows.Forms.Label
            Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid
            Me.chkLabResult = New System.Windows.Forms.CheckedListBox
            Me.txtSearchDia = New System.Windows.Forms.TextBox
            Me.txtLabResult = New System.Windows.Forms.TextBox
            Me.lblSearcgDia = New System.Windows.Forms.Label
            Me.Label1 = New System.Windows.Forms.Label
            Me.rbDesc = New System.Windows.Forms.RadioButton
            Me.rbICD9Code = New System.Windows.Forms.RadioButton
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.lbl_BottomBrd = New System.Windows.Forms.Label
            Me.lbl_LeftBrd = New System.Windows.Forms.Label
            Me.lbl_RightBrd = New System.Windows.Forms.Label
            Me.lbl_TopBrd = New System.Windows.Forms.Label
            Me.pnl_tlsp = New System.Windows.Forms.Panel
            Me.tlsp_PatientDiagnosis = New gloGlobal.gloToolStripIgnoreFocus
            Me.ts_btnShowReport = New System.Windows.Forms.ToolStripButton
            Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
            Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
            CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            Me.pnl_tlsp.SuspendLayout()
            Me.tlsp_PatientDiagnosis.SuspendLayout()
            Me.SuspendLayout()
            '
            'chklstbx
            '
            Me.chklstbx.CheckOnClick = True
            Me.chklstbx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.chklstbx.ForeColor = System.Drawing.Color.Black
            Me.chklstbx.Location = New System.Drawing.Point(123, 77)
            Me.chklstbx.Name = "chklstbx"
            Me.chklstbx.Size = New System.Drawing.Size(392, 123)
            Me.chklstbx.TabIndex = 3
            '
            'lblCheckDiag
            '
            Me.lblCheckDiag.AutoSize = True
            Me.lblCheckDiag.BackColor = System.Drawing.Color.Transparent
            Me.lblCheckDiag.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCheckDiag.Location = New System.Drawing.Point(52, 80)
            Me.lblCheckDiag.Name = "lblCheckDiag"
            Me.lblCheckDiag.Size = New System.Drawing.Size(68, 14)
            Me.lblCheckDiag.TabIndex = 9
            Me.lblCheckDiag.Text = " Diagnosis :"
            Me.lblCheckDiag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblLabResult
            '
            Me.lblLabResult.AutoSize = True
            Me.lblLabResult.BackColor = System.Drawing.Color.Transparent
            Me.lblLabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblLabResult.Location = New System.Drawing.Point(49, 248)
            Me.lblLabResult.Name = "lblLabResult"
            Me.lblLabResult.Size = New System.Drawing.Size(71, 14)
            Me.lblLabResult.TabIndex = 10
            Me.lblLabResult.Text = "Lab Result :"
            Me.lblLabResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'C1FlexGrid1
            '
            Me.C1FlexGrid1.AllowAddNew = True
            Me.C1FlexGrid1.AllowDelete = True
            Me.C1FlexGrid1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both
            Me.C1FlexGrid1.BackColor = System.Drawing.Color.GhostWhite
            Me.C1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
            Me.C1FlexGrid1.ColumnInfo = "8,1,0,0,0,95,Columns:0{Width:34;}" & Global.Microsoft.VisualBasic.ChrW(9)
            Me.C1FlexGrid1.ExtendLastCol = True
            Me.C1FlexGrid1.ForeColor = System.Drawing.Color.Black
            Me.C1FlexGrid1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
            Me.C1FlexGrid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
            Me.C1FlexGrid1.Location = New System.Drawing.Point(203, 84)
            Me.C1FlexGrid1.Name = "C1FlexGrid1"
            Me.C1FlexGrid1.Rows.Count = 16
            Me.C1FlexGrid1.Rows.DefaultSize = 19
            Me.C1FlexGrid1.Size = New System.Drawing.Size(144, 104)
            Me.C1FlexGrid1.StyleInfo = resources.GetString("C1FlexGrid1.StyleInfo")
            Me.C1FlexGrid1.TabIndex = 15
            Me.C1FlexGrid1.Visible = False
            '
            'chkLabResult
            '
            Me.chkLabResult.CheckOnClick = True
            Me.chkLabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.chkLabResult.ForeColor = System.Drawing.Color.Black
            Me.chkLabResult.Location = New System.Drawing.Point(124, 244)
            Me.chkLabResult.Name = "chkLabResult"
            Me.chkLabResult.Size = New System.Drawing.Size(392, 123)
            Me.chkLabResult.TabIndex = 5
            '
            'txtSearchDia
            '
            Me.txtSearchDia.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtSearchDia.ForeColor = System.Drawing.Color.Black
            Me.txtSearchDia.Location = New System.Drawing.Point(123, 45)
            Me.txtSearchDia.Name = "txtSearchDia"
            Me.txtSearchDia.Size = New System.Drawing.Size(144, 22)
            Me.txtSearchDia.TabIndex = 2
            '
            'txtLabResult
            '
            Me.txtLabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtLabResult.ForeColor = System.Drawing.Color.Black
            Me.txtLabResult.Location = New System.Drawing.Point(123, 211)
            Me.txtLabResult.Name = "txtLabResult"
            Me.txtLabResult.Size = New System.Drawing.Size(144, 22)
            Me.txtLabResult.TabIndex = 4
            '
            'lblSearcgDia
            '
            Me.lblSearcgDia.AutoSize = True
            Me.lblSearcgDia.BackColor = System.Drawing.Color.Transparent
            Me.lblSearcgDia.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSearcgDia.Location = New System.Drawing.Point(15, 49)
            Me.lblSearcgDia.Name = "lblSearcgDia"
            Me.lblSearcgDia.Size = New System.Drawing.Size(105, 14)
            Me.lblSearcgDia.TabIndex = 20
            Me.lblSearcgDia.Text = "Search Diagnosis :"
            Me.lblSearcgDia.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(8, 215)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(112, 14)
            Me.Label1.TabIndex = 21
            Me.Label1.Text = "Search Lab Result :"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'rbDesc
            '
            Me.rbDesc.AutoSize = True
            Me.rbDesc.BackColor = System.Drawing.Color.Transparent
            Me.rbDesc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.rbDesc.Location = New System.Drawing.Point(228, 18)
            Me.rbDesc.Name = "rbDesc"
            Me.rbDesc.Size = New System.Drawing.Size(85, 18)
            Me.rbDesc.TabIndex = 1
            Me.rbDesc.Text = "Description"
            Me.rbDesc.UseVisualStyleBackColor = False
            '
            'rbICD9Code
            '
            Me.rbICD9Code.AutoSize = True
            Me.rbICD9Code.BackColor = System.Drawing.Color.Transparent
            Me.rbICD9Code.Checked = True
            Me.rbICD9Code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.rbICD9Code.Location = New System.Drawing.Point(123, 18)
            Me.rbICD9Code.Name = "rbICD9Code"
            Me.rbICD9Code.Size = New System.Drawing.Size(90, 18)
            Me.rbICD9Code.TabIndex = 0
            Me.rbICD9Code.TabStop = True
            Me.rbICD9Code.Text = "ICD9 Code"
            Me.rbICD9Code.UseVisualStyleBackColor = False
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.Color.Transparent
            Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
            Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
            Me.Panel1.Controls.Add(Me.lbl_RightBrd)
            Me.Panel1.Controls.Add(Me.lbl_TopBrd)
            Me.Panel1.Controls.Add(Me.rbDesc)
            Me.Panel1.Controls.Add(Me.lblCheckDiag)
            Me.Panel1.Controls.Add(Me.rbICD9Code)
            Me.Panel1.Controls.Add(Me.chklstbx)
            Me.Panel1.Controls.Add(Me.txtLabResult)
            Me.Panel1.Controls.Add(Me.lblLabResult)
            Me.Panel1.Controls.Add(Me.txtSearchDia)
            Me.Panel1.Controls.Add(Me.chkLabResult)
            Me.Panel1.Controls.Add(Me.C1FlexGrid1)
            Me.Panel1.Controls.Add(Me.lblSearcgDia)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 53)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
            Me.Panel1.Size = New System.Drawing.Size(534, 382)
            Me.Panel1.TabIndex = 1
            '
            'lbl_BottomBrd
            '
            Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
            Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
            Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 378)
            Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
            Me.lbl_BottomBrd.Size = New System.Drawing.Size(526, 1)
            Me.lbl_BottomBrd.TabIndex = 25
            Me.lbl_BottomBrd.Text = "label2"
            '
            'lbl_LeftBrd
            '
            Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
            Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
            Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
            Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
            Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 375)
            Me.lbl_LeftBrd.TabIndex = 24
            Me.lbl_LeftBrd.Text = "label4"
            '
            'lbl_RightBrd
            '
            Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
            Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
            Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
            Me.lbl_RightBrd.Location = New System.Drawing.Point(530, 4)
            Me.lbl_RightBrd.Name = "lbl_RightBrd"
            Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 375)
            Me.lbl_RightBrd.TabIndex = 23
            Me.lbl_RightBrd.Text = "label3"
            '
            'lbl_TopBrd
            '
            Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
            Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
            Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
            Me.lbl_TopBrd.Name = "lbl_TopBrd"
            Me.lbl_TopBrd.Size = New System.Drawing.Size(528, 1)
            Me.lbl_TopBrd.TabIndex = 22
            Me.lbl_TopBrd.Text = "label1"
            '
            'pnl_tlsp
            '
            Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
            Me.pnl_tlsp.Controls.Add(Me.tlsp_PatientDiagnosis)
            Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
            Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
            Me.pnl_tlsp.Name = "pnl_tlsp"
            Me.pnl_tlsp.Size = New System.Drawing.Size(534, 53)
            Me.pnl_tlsp.TabIndex = 0
            '
            'tlsp_PatientDiagnosis
            '
            Me.tlsp_PatientDiagnosis.BackColor = System.Drawing.Color.Transparent
            Me.tlsp_PatientDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
            Me.tlsp_PatientDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.tlsp_PatientDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.tlsp_PatientDiagnosis.ImageScalingSize = New System.Drawing.Size(32, 32)
            Me.tlsp_PatientDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnShowReport, Me.ts_btnClose})
            Me.tlsp_PatientDiagnosis.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
            Me.tlsp_PatientDiagnosis.Location = New System.Drawing.Point(0, 0)
            Me.tlsp_PatientDiagnosis.Name = "tlsp_PatientDiagnosis"
            Me.tlsp_PatientDiagnosis.Size = New System.Drawing.Size(534, 53)
            Me.tlsp_PatientDiagnosis.TabIndex = 0
            Me.tlsp_PatientDiagnosis.Text = "toolStrip1"
            '
            'ts_btnShowReport
            '
            Me.ts_btnShowReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ts_btnShowReport.Image = CType(resources.GetObject("ts_btnShowReport.Image"), System.Drawing.Image)
            Me.ts_btnShowReport.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ts_btnShowReport.Name = "ts_btnShowReport"
            Me.ts_btnShowReport.Size = New System.Drawing.Size(93, 50)
            Me.ts_btnShowReport.Tag = "Show Report"
            Me.ts_btnShowReport.Text = "&Show Report"
            Me.ts_btnShowReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
            '
            'ts_btnClose
            '
            Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
            Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ts_btnClose.Name = "ts_btnClose"
            Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
            Me.ts_btnClose.Tag = "Close"
            Me.ts_btnClose.Text = "&Close"
            Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
            '
            'C1SuperTooltip1
            '
            Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
            Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
            '
            'frmPatientDiagnosis
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ClientSize = New System.Drawing.Size(534, 435)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.pnl_tlsp)
            Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmPatientDiagnosis"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Patient Diagnosis Report"
            CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.pnl_tlsp.ResumeLayout(False)
            Me.pnl_tlsp.PerformLayout()
            Me.tlsp_PatientDiagnosis.ResumeLayout(False)
            Me.tlsp_PatientDiagnosis.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Sub frmPatientDiagnosis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            gloC1FlexStyle.Style(C1FlexGrid1)

            'Dim ofrm As New frmPatientDiagnosis
            Dim odb As New gloStream.gloDataBase.gloDataBase
            'Dim i As Integer


            Dim odt As DataTable = Nothing
            Try
                odb.Connect(GetConnectionString)
                odt = odb.ReadData("gLM_ViewLabResult_MST")

                With chkLabResult
                    .DataSource = odt
                    .DisplayMember = odt.Columns(1).ColumnName.ToString
                    .ValueMember = odt.Columns("nFlowSheetID").ColumnName
                    ''''this IF condition is added by Anil on 31/10/2007, since it was giving error for blank DB.
                    If .Items.Count > 0 Then
                        .SelectedIndex = 0
                    End If
                End With
                odb.Disconnect()

                Dim _strSQL As String = ""
                _strSQL = " select nICD9ID AS nICD9ID ,sICD9code+'-'+sDescription AS sICDDesc , sDescription AS sDescription, sICD9code AS ICD9code " _
                & " from ICD9 order by sICD9code "

                '' FIll Diagnosis
                odb.Connect(GetConnectionString)
                odt = odb.ReadQueryDataTable(_strSQL)

                With chklstbx
                    .DataSource = odt
                    .DisplayMember = odt.Columns("sICDDesc").ColumnName.ToString 'ICD9 code description
                    .ValueMember = odt.Columns("nICD9ID").ColumnName
                    ''''this IF condition is added by Anil on 31/10/2007, since it was giving error for blank DB.
                    If .Items.Count > 0 Then
                        .SelectedIndex = 0
                    End If
                End With

                odb.Disconnect()
                'Dim objAudit As New clsAudit
                'objAudit.CreateLog(clsAudit.enmActivityType.Other, "Patient Diagnosis Lab Results Opened", gstrLoginName, gstrClientMachineName)
                'objAudit = Nothing
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "Patient Diagnosis Lab Results Opened", gloAuditTrail.ActivityOutCome.Success)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MsgBox(ex.Message)
            Finally
                odb.Dispose()
                odb = Nothing
            End Try

        End Sub


        Private Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Call GetPrint(False)
        End Sub

        Private Sub GetPrint(ByVal ISPrint As Boolean)
            'Dim oPatients As New Collection
            'Dim oPatientInfo As PatientInfo
            'Dim oLabOrders As New Collection

            Try
                Dim strDia As String = ""
                Dim strLabResult As String = ""

                ' Dim LabResultID As Long
                Dim i As Integer
                Dim j As Integer
                '    Dim dt As New DataTable
                '   Dim dtPatientDetails As New DataTable
                With chklstbx
                    If .CheckedItems.Count = 0 Then
                        MessageBox.Show("Please select atleast one Diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtSearchDia.Focus()
                        Exit Sub
                    End If

                    For i = 0 To .CheckedItems.Count - 1
                        .SelectedItem = .CheckedItems(i)   '
                        If i = 0 Then
                            strDia = .SelectedValue
                        Else
                            strDia = strDia & "," & .SelectedValue

                            'Label1.Text = Split(str, "'").ToString
                        End If
                    Next
                End With

                ' strDia = "928.11-KNEE CRUSH INJURY" & "," & "726.19-SHOULDER IMPINGEMENT SYNDROME/BURSITIS" & "," & "808.8-CLOSED FRACTURE OF PELVIS"

                With chkLabResult
                    If .CheckedItems.Count = 0 Then
                        MessageBox.Show("Please select atleast one Lab Test", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtLabResult.Focus()
                        Exit Sub
                    End If
                    Dim oLabOrders As New Collection
                    For i = 0 To .CheckedItems.Count - 1
                        .SelectedItem = .CheckedItems(i)   '
                        oLabOrders.Add(.SelectedValue)
                        If i = 0 Then
                            strLabResult = .SelectedValue
                        Else
                            strLabResult = strLabResult & "," & .SelectedValue

                            'Label1.Text = Split(str, "'").ToString
                        End If
                    Next
                    oLabOrders.Clear()
                    oLabOrders = Nothing
                End With

                '_FlowSheetID = cmbLabResult.SelectedValue
                '_FlowSheetName = cmbLabResult.Text

                Dim oDB As New gloDataBase.gloDataBase
                With oDB
                    Dim _strSQL As String = ""
                    Dim _strPatientDetails As String = ""

                    ''<< By Vinayak for Exempt from report >>
                    '_strSQL = " SELECT DISTINCT Diagnosis.nPatientID, Isnull(Patient.nExemptFromReport,0) AS nExemptFromReport " _
                    '        & " FROM Diagnosis INNER JOIN " _
                    '        & " ICD9 ON Diagnosis.sICD9Code = ICD9.sICD9Code AND Diagnosis.sICD9Description = ICD9.sDescription INNER JOIN Patient ON Diagnosis.nPatientID = Patient.nPatientID CROSS JOIN LM_LabResult " _
                    '        & " WHERE (LM_LabResult.nFlowSheetID = " & _FlowSheetID & " ) AND (ICD9.nICD9ID IN (" & strDia & ")) "
                    ''& "  AND (Patient.nExemptFromReport = 0 OR Patient.nExemptFromReport IS NULL)"

                    '_strSQL = " SELECT DISTINCT Diagnosis.nPatientID, Patient.sFirstName, Patient.sLastName, Patient.sPatientCode, Patient.dtDOB, Patient.sGender, Patient.nSSN , Isnull(Patient.nExemptFromReport,0) AS nExemptFromReport ,LM_LabResult_MST.nFlowSheetID " _
                    '        & " FROM Diagnosis INNER JOIN " _
                    '        & " ICD9 ON Diagnosis.sICD9Code = ICD9.sICD9Code AND Diagnosis.sICD9Description = ICD9.sDescription INNER JOIN Patient ON Diagnosis.nPatientID = Patient.nPatientID CROSS JOIN LM_LabResult " _
                    '        & " WHERE (LM_LabResult.nFlowSheetID IN (" & strLabResult & ")) AND (ICD9.nICD9ID IN (" & strDia & ")) " _
                    '        & " AND (Patient.nExemptFromReport = 0 OR Patient.nExemptFromReport IS NULL)"

                    '_strSQL = " SELECT DISTINCT Diagnosis.nPatientID, Isnull(Patient.nExemptFromReport,0) AS nExemptFromReport ,LM_LabResult.nFlowSheetID " _
                    '        & " FROM Diagnosis INNER JOIN " _
                    '        & " ICD9 ON Diagnosis.sICD9Code = ICD9.sICD9Code AND Diagnosis.sICD9Description = ICD9.sDescription INNER JOIN Patient ON Diagnosis.nPatientID = Patient.nPatientID CROSS JOIN LM_LabResult " _
                    '        & " WHERE (LM_LabResult.nFlowSheetID IN (" & strLabResult & ")) AND (ICD9.nICD9ID IN (" & strDia & ")) " _
                    '        & " AND (Patient.nExemptFromReport = 0 OR Patient.nExemptFromReport IS NULL)"

                    _strSQL = " SELECT DISTINCT ExamICD9CPT.nPatientID, Patient.sFirstName, Patient.sLastName, Patient.sPatientCode, Patient.dtDOB, Patient.sGender,isnull(Patient.nSSN,'') as nSSN " _
                            & " FROM ExamICD9CPT INNER JOIN " _
                            & " ICD9 ON ExamICD9CPT.sICD9Code = ICD9.sICD9Code AND ExamICD9CPT.sICD9Description = ICD9.sDescription INNER JOIN Patient ON ExamICD9CPT.nPatientID = Patient.nPatientID " _
                            & " WHERE (ICD9.nICD9ID IN (" & strDia & ")) " _
                            & " AND ( Patient.nExemptFromReport = 0 OR Patient.nExemptFromReport IS NULL ) "

                    .Connect(GetConnectionString)
                    Dim dt As DataTable = Nothing
                    '   Dim dtPatientDetails As New DataTable

                    dt = .ReadQueryDataTable(_strSQL)
                    .Disconnect()

                    If IsNothing(dt) = False Then

                        If dt.Rows.Count > 0 Then
                            Dim oPatients As New Collection
                            Dim oPatientInfo As PatientInfo
                            For i = 0 To dt.Rows.Count - 1

                                _PatientID = dt.Rows(i)("nPatientID")

                                '_IsExempted = dt.Rows(i)("nExemptFromReport")
                                'oPatientInfo.IsExempted = _IsExempted
                                '_FlowSheetID = dt.Rows(i)("nFlowSheetID")
                                'oPatientInfo.FlowSheetID = _FlowSheetID
                                _PatientName = dt.Rows(i)("sFirstName") & " " & dt.Rows(i)("sLastName")
                                'oPatientInfo.PatientName = _PatientName

                                _PatientCode = dt.Rows(i)("sPatientCode")
                                'oPatientInfo.PatientCode = _PatientCode

                                _PatientDOB = dt.Rows(i)("dtDOB")
                                'oPatientInfo.PatientDOB = _PatientDOB

                                Dim nMonths As Int16
                                nMonths = DateDiff(DateInterval.Month, CType(_PatientDOB, Date), Date.Now.Date)
                                _PatientAge = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"

                                _PatientGender = dt.Rows(i)("sGender")
                                'oPatientInfo.PatientGender = _PatientGender

                                _PatientSSN = dt.Rows(i)("nSSN")
                                'oPatientInfo.PatientSSN = _PatientSSN

                                '''' Find Patient Details
                                ''_strSQL = " SELECT DISTINCT Patient.sFirstName, Patient.sLastName, Patient.sPatientCode, Patient.dtDOB, Patient.sGender, Patient.nSSN, LM_LabResult.dtVisitDate, " _
                                '_strSQL = " SELECT DISTINCT  LM_LabResult.dtVisitDate, " _
                                '        & " LM_LabResult.nFlowSheetRecordID, LM_LabResult_MST.sFlowSheetName FROM Patient INNER JOIN LM_LabResult ON Patient.nPatientID = LM_LabResult.nPatientID RIGHT OUTER JOIN " _
                                '        & " LM_LabResult_MST ON LM_LabResult.nFlowSheetID = LM_LabResult_MST.nFlowSheetID " _
                                '        & " WHERE LM_LabResult.dtVisitDate = (SELECT MAX(LM_LabResult.dtVisitDate) FROM LM_LabResult WHERE LM_LabResult.nPatientID = " & _PatientID & ") " _
                                '        & " AND Patient.nPatientID = " & _PatientID & " AND LM_LabResult.nFlowSheetID = " & _FlowSheetID & ""

                                'for l as Int16 = labres
                                '_strSQL = " SELECT DISTINCT  LM_LabResult.dtVisitDate, LM_LabResult.nFlowSheetRecordID, LM_LabResult_MST.nFlowSheetID ,LM_LabResult_MST.sFlowSheetName FROM Patient INNER JOIN LM_LabResult ON Patient.nPatientID = LM_LabResult.nPatientID RIGHT OUTER JOIN LM_LabResult_MST ON LM_LabResult.nFlowSheetID = LM_LabResult_MST.nFlowSheetID " _
                                '        & " WHERE LM_LabResult.dtVisitDate = (SELECT MAX(LM_LabResult.dtVisitDate) FROM LM_LabResult WHERE LM_LabResult.nPatientID = " & _PatientID & ") " _
                                '        & " AND Patient.nPatientID = " & _PatientID & " AND (LM_LabResult.nFlowSheetID IN (" & strLabResult & ")) " _
                                '        & " UNION " _
                                '        & " SELECT DISTINCT  LM_LabResult.dtVisitDate, LM_LabResult.nFlowSheetRecordID, LM_LabResult_MST.nFlowSheetID , LM_LabResult_MST.sFlowSheetName FROM Patient INNER JOIN LM_LabResult ON Patient.nPatientID = LM_LabResult.nPatientID RIGHT OUTER JOIN LM_LabResult_MST ON LM_LabResult.nFlowSheetID = LM_LabResult_MST.nFlowSheetID " _
                                '        & " WHERE LM_LabResult.dtVisitDate = (SELECT MAX(LM_LabResult.dtVisitDate) FROM LM_LabResult WHERE LM_LabResult.nPatientID = " & _PatientID & ") " _
                                '        & " AND Patient.nPatientID = " & _PatientID & " AND (LM_LabResult_MST.nFlowSheetID IN (" & strLabResult & "))"

                                '_strSQL = " SELECT DISTINCT  LM_LabResult.dtVisitDate, LM_LabResult.nFlowSheetRecordID, " _
                                '& " LM_LabResult_MST.nFlowSheetID , LM_LabResult_MST.sFlowSheetName FROM " _
                                '& " LM_LabResult_MST   left OUTER JOIN LM_LabResult ON " _
                                '& " LM_LabResult_MST.nFlowSheetID in (select LM_LabResult.nFlowSheetID from LM_LabResult WHERE LM_LabResult.nPatientID = " & _PatientID & ") " _
                                '& " WHERE LM_LabResult_MST.nFlowSheetID in (" & strLabResult & ") AND " _
                                '& " LM_LabResult.dtVisitDate = (SELECT MAX(LM_LabResult.dtVisitDate) FROM LM_LabResult WHERE LM_LabResult.nPatientID = " & _PatientID & ") "

                                '_strSQL = " SELECT DISTINCT  LM_LabResult.dtVisitDate, LM_LabResult.nFlowSheetRecordID, " _
                                '& " LM_LabResult_MST.nFlowSheetID , LM_LabResult_MST.sFlowSheetName FROM " _
                                '& " LM_LabResult_MST   left OUTER JOIN LM_LabResult ON " _
                                '& " LM_LabResult_MST.nFlowSheetID in (select LM_LabResult.nFlowSheetID from LM_LabResult WHERE LM_LabResult.nPatientID = " & _PatientID & ") " _
                                '& " where LM_LabResult_MST.nFlowSheetID in (" & strLabResult & ") "

                                _strSQL = "SELECT DISTINCT  lm.dtVisitDate, lm.nFlowSheetRecordID, " _
                                & " lmm.nFlowSheetID , lmm.sFlowSheetName FROM  LM_LabResult_MST lmm " _
                                & " left OUTER JOIN LM_LabResult lm ON  lmm.nFlowSheetID in " _
                                & " (select lm.nFlowSheetID from LM_LabResult WHERE lm.nPatientID = " & _PatientID & " " _
                                & " and lm.dtvisitdate =(select max(dtvisitdate) from LM_LabResult inner join " _
                                & " LM_LabResult_MST on  LM_LabResult.nFlowSheetID =LM_LabResult_MST.nFlowSheetID  WHERE LM_LabResult.nPatientID = " & _PatientID & ")) " _
                                & " where lmm.nFlowSheetID in (" & strLabResult & ") "


                                .Connect(GetConnectionString)
                                '    Dim dt As New DataTable
                                Dim dtPatientDetails As DataTable = Nothing

                                dtPatientDetails = .ReadQueryDataTable(_strSQL)
                                ' = .ReadQueryDataTable(_strPatientDetails)
                                .Disconnect()
                                If IsNothing(dtPatientDetails) = False Then
                                    If dtPatientDetails.Rows.Count > 0 Then
                                        For j = 0 To dtPatientDetails.Rows.Count - 1
                                            oPatientInfo = New PatientInfo

                                            '_PatientID = dt.Rows(i)("nPatientID")
                                            '_PatientName = dt.Rows(i)("sFirstName") & " " & dt.Rows(i)("sLastName")
                                            oPatientInfo.PatientId = _PatientID
                                            oPatientInfo.PatientCode = _PatientCode
                                            oPatientInfo.PatientName = _PatientName
                                            oPatientInfo.PatientSSN = _PatientSSN
                                            oPatientInfo.PatientDOB = _PatientDOB
                                            oPatientInfo.PatientAge = _PatientAge
                                            oPatientInfo.PatientGender = _PatientGender

                                            If IsDBNull(dtPatientDetails.Rows(j)("nFlowSheetRecordID")) = False Then
                                                _FlowSheetRecID = dtPatientDetails.Rows(j)("nFlowSheetRecordID")
                                            Else
                                                _FlowSheetRecID = 0
                                            End If
                                            oPatientInfo.FlowSheetRecID = _FlowSheetRecID

                                            If IsDBNull(dtPatientDetails.Rows(j)("sFlowSheetName")) = False Then
                                                _FlowSheetName = dtPatientDetails.Rows(j)("sFlowSheetName")
                                            Else
                                                _FlowSheetName = ""
                                            End If

                                            oPatientInfo.FlowSheetName = _FlowSheetName

                                            If IsDBNull(dtPatientDetails.Rows(j)("dtVisitDate")) = False Then
                                                _VisitDate = Format(dtPatientDetails.Rows(j)("dtVisitDate"), "MM/dd/yyyy")
                                            Else
                                                _VisitDate = Format(Now.Date, "MM/dd/yyyy")
                                            End If
                                            oPatientInfo.VisitDate = _VisitDate

                                            If IsDBNull(dtPatientDetails.Rows(j)("nFlowSheetID")) = False Then
                                                _FlowSheetID = dtPatientDetails.Rows(j)("nFlowSheetID")
                                            Else
                                                _FlowSheetID = 0
                                            End If
                                            oPatientInfo.FlowSheetID = _FlowSheetID


                                            oPatientInfo.Result = Fill_OrderLabResult(_FlowSheetID, _FlowSheetRecID)

                                            oPatients.Add(oPatientInfo)

                                            'Call PrintFlowSheet(ISPrint, oPatientInfo)

                                        Next 'for j loop
                                        'Else
                                        'MessageBox.Show("There is no Patient(s) found with this Diagnosis and Labs", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        'Exit Sub
                                    End If  '' If dtPatientDetails.Rows.Count > 0 Then
                                    'Else
                                    '    MessageBox.Show("There is no Patient(s) found with this Diagnosis and Labs", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    'Exit Sub
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If  '' If IsNothing(dtPatientDetails) = False Then

                            Next 'for i loop
                            If oPatients.Count > 0 Then
                                Call SaveReportData(oPatients)

                                oPatients.Clear()
                                oPatients = Nothing

                                Dim frm As New frmrptViewLabResult
                                With frm
                                    .ShowInTaskbar = False
                                    .WindowState = FormWindowState.Maximized
                                    .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                    .Dispose()
                                End With
                            Else
                                oPatients.Clear()
                                oPatients = Nothing
                                MessageBox.Show("There are no Patient(s) found with these Diagnosis and Labs", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                If (IsNothing(dt) = False) Then
                                    dt.Dispose()
                                    dt = Nothing
                                End If
                                If (IsNothing(oDB) = False) Then
                                    oDB.Dispose()
                                    oDB = Nothing
                                End If
                                Exit Sub
                            End If

                        Else
                            'MessageBox.Show("There is no Patient(s) found with this Diagnosis and Labs", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Sub
                        End If
                        If (IsNothing(dt) = False) Then
                            dt.Dispose()
                            dt = Nothing
                        End If
                     

                    End If ' for dt
                    If (IsNothing(oDB) = False) Then
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                  
                End With

                'If oPatients.Count > 0 Then
                '    Dim frm As New frmRpt_PatientDiagnosis(oPatients, _FlowSheetName)
                '    With frm
                '        .ShowInTaskbar = False
                '        .MdiParent = CType(Me.Owner, MainMenu)
                '        .WindowState = FormWindowState.Maximized
                '        Me.Hide()
                '        .Show()
                '    End With
                'Else
                '    MessageBox.Show("There is no Patient(s) found with this Diagnosis and Labs", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ShowReportPatientDiagnosis()
            Call GetPrint(True)
            'Dim oPatients As New Collection
            'Dim oPatientInfo As PatientInfo
            'Try
            '    Dim strDia As String = ""
            '    Dim strLabResult As String = ""

            '    ' Dim LabResultID As Long
            '    Dim i As Integer
            '    Dim j As Integer
            '    Dim dt As New DataTable
            '    Dim dtPatientDetails As New DataTable
            '    With chklstbx
            '        For i = 0 To .CheckedItems.Count - 1
            '            .SelectedItem = .CheckedItems(i)   '
            '            If i = 0 Then
            '                strDia = .SelectedValue
            '            Else
            '                strDia = strDia & "," & .SelectedValue

            '                'Label1.Text = Split(str, "'").ToString
            '            End If
            '        Next
            '    End With

            '    ' strDia = "928.11-KNEE CRUSH INJURY" & "," & "726.19-SHOULDER IMPINGEMENT SYNDROME/BURSITIS" & "," & "808.8-CLOSED FRACTURE OF PELVIS"

            '    With chkLabResult
            '        For i = 0 To .CheckedItems.Count - 1
            '            .SelectedItem = .CheckedItems(i)   '
            '            If i = 0 Then
            '                strLabResult = .SelectedValue
            '            Else
            '                strLabResult = strLabResult & "," & .SelectedValue

            '                'Label1.Text = Split(str, "'").ToString
            '            End If
            '        Next
            '    End With

            '    '_FlowSheetID = cmbLabResult.SelectedValue
            '    '_FlowSheetName = cmbLabResult.Text

            '    Dim oDB As New gloDataBase.gloDataBase
            '    With oDB
            '        Dim _strSQL As String = ""
            '        Dim _strPatientDetails As String = ""

            '        ''<< By Vinayak for Exempt from report >>
            '        '_strSQL = " SELECT DISTINCT Diagnosis.nPatientID, Isnull(Patient.nExemptFromReport,0) AS nExemptFromReport " _
            '        '        & " FROM Diagnosis INNER JOIN " _
            '        '        & " ICD9 ON Diagnosis.sICD9Code = ICD9.sICD9Code AND Diagnosis.sICD9Description = ICD9.sDescription INNER JOIN Patient ON Diagnosis.nPatientID = Patient.nPatientID CROSS JOIN LM_LabResult " _
            '        '        & " WHERE (LM_LabResult.nFlowSheetID = " & _FlowSheetID & " ) AND (ICD9.nICD9ID IN (" & strDia & ")) "
            '        ''& "  AND (Patient.nExemptFromReport = 0 OR Patient.nExemptFromReport IS NULL)"

            '        _strSQL = " SELECT DISTINCT Diagnosis.nPatientID, Isnull(Patient.nExemptFromReport,0) AS nExemptFromReport ,LM_LabResult.nFlowSheetID " _
            '                & " FROM Diagnosis INNER JOIN " _
            '                & " ICD9 ON Diagnosis.sICD9Code = ICD9.sICD9Code AND Diagnosis.sICD9Description = ICD9.sDescription INNER JOIN Patient ON Diagnosis.nPatientID = Patient.nPatientID CROSS JOIN LM_LabResult " _
            '                & " WHERE (LM_LabResult.nFlowSheetID IN (" & strLabResult & ")) AND (ICD9.nICD9ID IN (" & strDia & ")) "

            '        .Connect(GetConnectionString)
            '        dt = .ReadQueryDataTable(_strSQL)
            '        .Disconnect()

            '        If IsNothing(dt) = False Then
            '            If dt.Rows.Count > 0 Then
            '                For i = 0 To dt.Rows.Count - 1
            '                    oPatientInfo = New PatientInfo

            '                    _PatientID = dt.Rows(i)("nPatientID")
            '                    oPatientInfo.PatientId = _PatientID
            '                    _IsExempted = dt.Rows(i)("nExemptFromReport")
            '                    oPatientInfo.IsExempted = _IsExempted
            '                    _FlowSheetID = dt.Rows(i)("nFlowSheetID")
            '                    oPatientInfo.FlowSheetID = _FlowSheetID

            '                    '' Find Patient Details
            '                    _strSQL = "SELECT DISTINCT  Patient.sFirstName , Patient.sLastName ,Patient.sPatientCode, Patient.dtDOB , Patient.sGender , Patient.nSSN, LM_LabResult.dtVisitDate , LM_LabResult.nFlowSheetRecordID, " _
            '                            & " LM_LabResult_MST.sFlowSheetName FROM Patient INNER JOIN " _
            '                            & " LM_LabResult ON Patient.nPatientID = LM_LabResult.nPatientID INNER JOIN " _
            '                            & " LM_LabResult_MST ON LM_LabResult.nFlowSheetID = LM_LabResult_MST.nFlowSheetID " _
            '                            & " WHERE LM_LabResult.dtVisitDate = (SELECT MAX(LM_LabResult.dtVisitDate) FROM LM_LabResult WHERE LM_LabResult.nPatientID = " & _PatientID & ") " _
            '                            & " and Patient.nPatientID = " & _PatientID & " AND LM_LabResult.nFlowSheetID = " & _FlowSheetID & ""

            '                    .Connect(GetConnectionString)
            '                    dtPatientDetails = .ReadQueryDataTable(_strSQL)
            '                    ' = .ReadQueryDataTable(_strPatientDetails)
            '                    .Disconnect()
            '                    If IsNothing(dtPatientDetails) = False Then
            '                        If dtPatientDetails.Rows.Count > 0 Then
            '                            For j = 0 To dtPatientDetails.Rows.Count - 1
            '                                '_PatientID = dt.Rows(i)("nPatientID")
            '                                '_PatientName = dt.Rows(i)("sFirstName") & " " & dt.Rows(i)("sLastName")
            '                                _PatientName = dtPatientDetails.Rows(j)("sFirstName") & " " & dtPatientDetails.Rows(j)("sLastName")
            '                                oPatientInfo.PatientName = _PatientName

            '                                _PatientCode = dtPatientDetails.Rows(j)("sPatientCode")
            '                                oPatientInfo.PatientCode = _PatientCode

            '                                _PatientDOB = dtPatientDetails.Rows(j)("dtDOB")
            '                                oPatientInfo.PatientDOB = _PatientDOB

            '                                _PatientGender = dtPatientDetails.Rows(j)("sGender")
            '                                oPatientInfo.PatientGender = _PatientGender

            '                                _PatientSSN = dtPatientDetails.Rows(j)("nSSN")
            '                                oPatientInfo.PatientSSN = _PatientSSN

            '                                _FlowSheetRecID = dtPatientDetails.Rows(j)("nFlowSheetRecordID")
            '                                oPatientInfo.FlowSheetRecID = _FlowSheetRecID

            '                                _FlowSheetName = dtPatientDetails.Rows(j)("sFlowSheetName")
            '                                oPatientInfo.FlowSheetName = _FlowSheetName

            '                                _VisitDate = dtPatientDetails.Rows(j)("dtVisitDate")
            '                                oPatientInfo.VisitDate = _VisitDate

            '                                oPatientInfo.Flex = Fill_OrderLabResult()

            '                                oPatients.Add(oPatientInfo)

            '                                Call PrintFlowSheet(True, oPatientInfo)

            '                            Next 'for j loop
            '                        Else
            '                            MessageBox.Show("There is no Patient(s) found with this Diagnosis and Labs", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                            Exit Sub
            '                        End If

            '                    End If ' for dtPatientDetails

            '                Next 'for i loop
            '            Else
            '                MessageBox.Show("There is no Patient(s) found with this Diagnosis and Labs", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                Exit Sub
            '            End If
            '        End If ' for dt
            '    End With


            '    'If oPatients.Count > 0 Then
            '    '    Dim frm As New frmRpt_PatientDiagnosis(oPatients, _FlowSheetName)
            '    '    With frm
            '    '        .ShowInTaskbar = False
            '    '        .MdiParent = CType(Me.Owner, MainMenu)
            '    '        .WindowState = FormWindowState.Maximized
            '    '        Me.Hide()
            '    '        .Show()
            '    '    End With
            '    'Else
            '    '    MessageBox.Show("There is no Patient(s) found with this Diagnosis and Labs", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    'End If
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End Try
        End Sub

        Private Function GetPatient() As Collection
            Dim strDia As String = ""
            ' Dim LabResultID As Long
            Dim i As Integer
            'Dim j As Integer

            ' Dim dtPatientDetails As New DataTable
            Dim oDB As New gloDataBase.gloDataBase
            Try


                With oDB


                    Dim _strSQL As String = ""
                    _strSQL = " SELECT DISTINCT ExamICD9CPT.nPatientID " _
                                       & " FROM ExamICD9CPT INNER JOIN " _
                                    & " ICD9 ON ExamICD9CPT.sICD9Code = ICD9.sICD9Code AND ExamICD9CPT.sICD9Description = ICD9.sDescription INNER JOIN Patient ON ExamICD9CPT.nPatientID = Patient.nPatientID CROSS JOIN LM_LabResult " _
                                    & " WHERE (LM_LabResult.nFlowSheetID = " & _FlowSheetID & " ) AND (ICD9.nICD9ID IN (" & strDia & "))" ' AND " _
                    '& " (Patient.nExemptFromReport = 0 OR Patient.nExemptFromReport IS NULL)"

                    .Connect(GetConnectionString)
                    Dim dt As DataTable = Nothing
                    dt = .ReadQueryDataTable(_strSQL)
                    .Disconnect()
                    If IsNothing(dt) = False Then
                        Try


                            Dim patient As New Collection
                            For i = 0 To dt.Rows.Count - 1
                                patient.Add(dt.Rows(i))
                            Next
                            Return patient

                        Catch ex As Exception
                        Finally
                            oDB.Dispose()
                            oDB = Nothing

                            dt.Dispose()
                            dt = Nothing
                        End Try
                    End If

                    ' dtPatientDetails = .ReadQueryDataTable(_strPatientDetails)


                    Return Nothing
                End With
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally
                If (IsNothing(oDB) = False) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
               
            End Try

        End Function

        'Private Function Fill_OrderLabResult_(ByVal FlowSheetID As Long, ByVal FlowSheetRecID As Long) As C1.Win.C1FlexGrid.C1FlexGrid

        '    Dim dtResult As New DataTable
        '    If FlowSheetRecID > 0 Then
        '        dtResult = ocls_LM_LabResult.SelectLabResult(_PatientID, _FlowSheetRecID)
        '    Else
        '        C1FlexGrid1.Clear(ClearFlags.All)
        '        setGridStyle(_FlowSheetID)
        '        Return C1FlexGrid1
        '    End If

        '    '' dtResult Contains  Columns 
        '    '' nFlowSheetRecordID,sResult

        '    If IsNothing(dtResult) = False Then
        '        If dtResult.Rows.Count > 0 Then
        '            '' IF Lab Result Exist the Fill it
        '            Dim mstream As ADODB.Stream
        '            Dim strFileName As String
        '            mstream = New ADODB.Stream
        '            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        '            mstream.Open()

        '            mstream.Write(dtResult.Rows(0)("sResult")) '' Flow Sheet Image

        '            '''' FLoWSheet Record ID
        '            ' _FlowSheetRecID = dtResult.Rows(0)("nFlowSheetRecordID")
        '            ''

        '            strFileName = Application.StartupPath & "\Temp\TempLabsResult.txt"
        '            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
        '            mstream.Close()

        '            'C1FlexGrid1.Clear(ClearFlags.Style)
        '            C1FlexGrid1.Clear(ClearFlags.All)

        '            ' Dim tmp As Long = dtResult.Rows(0)(0) 
        '            '' FlowsheetMSTID

        '            Call setGridStyle(_FlowSheetID)

        '            C1FlexGrid1.LoadGrid(strFileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
        '            ''C1FlexGrid1.WriteXml(Application.StartupPath & "\Temp\TempLabsResult.XML")
        '            C1FlexGrid1.Row = C1FlexGrid1.Rows.Count - 1

        '            If C1FlexGrid1.Row = 0 Then
        '                C1FlexGrid1.Row = 1 '' C1FlexGrid1.Rows.Count
        '            End If

        '            If C1FlexGrid1.Row <> 1 Then
        '                Dim i As Integer
        '                For i = 1 To C1FlexGrid1.Cols.Count - 1
        '                    If C1FlexGrid1.Cols(i).DataType Is GetType(System.DateTime) Then
        '                        C1FlexGrid1.SetData(C1FlexGrid1.Row, i, Now.Date)
        '                    End If
        '                Next
        '            End If

        '            blnModify = True

        '            ''''' Fill Tasks Details
        '            'Call Load_users()
        '            ''
        '        Else
        '            C1FlexGrid1.Clear(ClearFlags.All)
        '            setGridStyle(_FlowSheetID)
        '            _FlowSheetRecID = 0
        '        End If
        '    Else
        '        C1FlexGrid1.Clear(ClearFlags.All)
        '        setGridStyle(_FlowSheetID)
        '        _FlowSheetRecID = 0
        '    End If
        '    Return C1FlexGrid1
        'End Function


        Private Function Fill_OrderLabResult(ByVal FlowSheetID As Long, ByVal FlowSheetRecID As Long) As String

            Dim dtResult As DataTable = Nothing
            Dim strFileName As String = gloSettings.FolderSettings.AppTempFolderPath & Guid.NewGuid().ToString() & "TempLabsResult.txt"

            If FlowSheetRecID > 0 Then
                dtResult = ocls_LM_LabResult.SelectLabResult(_PatientID, _FlowSheetRecID)
            Else
                C1FlexGrid1.Clear(ClearFlags.All)
                setGridStyle(_FlowSheetID)
                'If File.Exists(strFileName) = True Then
                '    File.Delete(strFileName)
                'End If
                C1FlexGrid1.SaveGrid(strFileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)

                'Return C1FlexGrid1
            End If

            '' dtResult Contains  Columns 
            '' nFlowSheetRecordID,sResult

            If IsNothing(dtResult) = False Then
                If dtResult.Rows.Count > 0 Then
                    '' IF Lab Result Exist the Fill it
                    Dim mstream As ADODB.Stream

                    mstream = New ADODB.Stream
                    mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                    mstream.Open()

                    mstream.Write(dtResult.Rows(0)("sResult")) '' Flow Sheet Image

                    '''' FLoWSheet Record ID
                    ' _FlowSheetRecID = dtResult.Rows(0)("nFlowSheetRecordID")
                    ''


                    mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                    mstream.Flush()
                    mstream.Close()

                    mstream = Nothing


                    ''C1FlexGrid1.Clear(ClearFlags.Style)
                    'C1FlexGrid1.Clear(ClearFlags.All)

                    '' Dim tmp As Long = dtResult.Rows(0)(0) 
                    '''' FlowsheetMSTID

                    'Call setGridStyle(_FlowSheetID)

                    'C1FlexGrid1.LoadGrid(strFileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                    ''''C1FlexGrid1.WriteXml(Application.StartupPath & "\Temp\TempLabsResult.XML")
                    'C1FlexGrid1.Row = C1FlexGrid1.Rows.Count - 1

                    'If C1FlexGrid1.Row = 0 Then
                    '    C1FlexGrid1.Row = 1 '' C1FlexGrid1.Rows.Count
                    'End If

                    'If C1FlexGrid1.Row <> 1 Then
                    '    Dim i As Integer
                    '    For i = 1 To C1FlexGrid1.Cols.Count - 1
                    '        If C1FlexGrid1.Cols(i).DataType Is GetType(System.DateTime) Then
                    '            C1FlexGrid1.SetData(C1FlexGrid1.Row, i, Now.Date)
                    '        End If
                    '    Next
                    'End If

                    'blnModify = True

                    '''''' Fill Tasks Details
                    ''Call Load_users()
                    ''''
                Else
                    C1FlexGrid1.Clear(ClearFlags.All)
                    setGridStyle(_FlowSheetID)
                    _FlowSheetRecID = 0
                End If
                dtResult.Dispose()
                dtResult = Nothing
            Else
                C1FlexGrid1.Clear(ClearFlags.All)
                setGridStyle(_FlowSheetID)
                _FlowSheetRecID = 0
            End If
            ''''''''''''''''''''''''''''''''
            'Dim oDS As New DataSet
            ' Dim strFields As String
            Dim strData As String = ""
            'Dim oTable As New DataTable
            '  Dim oRows As DataRow
            Dim intCounter As Int32 = 0
            '  Dim oRow As DataRow()
            Dim strResult As New System.Text.StringBuilder

            Dim oSR As New StreamReader(strFileName)
            'Go to the top of the file
            oSR.BaseStream.Seek(0, SeekOrigin.Begin)
            ''Add in the Header Columns
            'For Each strFields In oSR.ReadLine().Split(vbTab)
            '    oDS.Tables(0).Columns.Add(strFields)
            'Next

            ''Now add in the Rows

            'oTable = oDS.Tables(0)
            While (oSR.Peek() > -1)
                'oRows = oTable.NewRow()
                ' For Each strFields In oSR.ReadLine() ' .Split(vbTab)
                'strData = strData & oSR.ReadLine & vbCrLf
                strResult.Append(oSR.ReadLine & vbCrLf)
                'oRows(intCounter) = strFields
                intCounter = intCounter + 1
                ' Next
                'intCounter = 0
                'oTable.Rows.Add(oRows)
            End While
            oSR.Close()
            oSR.Dispose()
            oSR = Nothing

            If intCounter = 1 Then
                strResult.Remove(0, strResult.Length)
                strResult.Append("No Lab Results")
            End If
            ''''''''''''''''''''''''''''''''
            Return strResult.ToString
            'Return C1FlexGrid1
        End Function


        Private Sub SaveReportData(ByVal oPatient As Collection)

            Dim oDB As New gloDataBase.gloDataBase
            Dim Patient As PatientInfo
            Dim _strSQL As String = ""
            Dim i As Integer

            _strSQL = "DELETE FROM tmpRptDiagnosisLabResult"
            oDB.Connect(GetConnectionString)
            oDB.ExecuteNonSQLQuery(_strSQL)

            For i = 1 To oPatient.Count
                'Patient = New PatientInfo
                Patient = CType(oPatient(i), PatientInfo)
                With Patient
                    Try
                        _strSQL = " Insert Into tmpRptDiagnosisLabResult ( nPatientID, sPatientCode, sPatientName, sPatientDOB, sPatientAge, sPatienGender, dtReportDate, sLabResultName, sResult) " _
                                & " VALUES( '" & .PatientId & "' , '" & .PatientCode & "' , '" & .PatientName & "' , '" & .PatientDOB & "' ,  '" & .PatientAge & "' ,  '" & .PatientGender & "' , '" & .VisitDate & "' , '" & .FlowSheetName & "' , '" & .Result & "')"

                        oDB.ExecuteNonSQLQuery(_strSQL)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                End With
            Next
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

        End Sub

        Private Sub InvokePrint(ByRef frm As Form, ByVal dggrid As DataGrid)
            'Instantiate GridPrinter class
            If GridPrinter Is Nothing Then
                GridPrinter = New DataGridPrinter(dggrid)
            End If

            'Invoke PrintDialog
            With Me.PrintDialog1
                .Document = GridPrinter.PrintDocument
                Dim objReportHeadercol As New Collection
                Dim objPageHeadercol As New Collection
                Dim objDetailscol As New Collection
                Dim objPageFootercol As New Collection
                Dim objReportFootercol As New Collection

                Dim objcol1 As New ArrayList


                'Fill Temporary sections collection 
                Dim objcontrol As Control
                For Each objcontrol In frm.Controls
                    If Not objcontrol.Tag Is Nothing Then
                        If objcontrol.Tag.substring(0, 1) = "1" Then
                            objReportHeadercol.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "2" Then
                            objPageHeadercol.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "3" Then
                            objcol1.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "4" Then
                            objPageFootercol.Add(objcontrol)
                        End If
                    End If

                Next
                'Populate Sections Collection
                GridPrinter.SetHeaderControls(objReportHeadercol, 1)
                GridPrinter.SetHeaderControls(objPageHeadercol, 2)
                GridPrinter.SetHeaderControls(objPageFootercol, 4)
                GridPrinter.SetHeaderControls(objReportFootercol, 5)

                If Not (storedPageSettings Is Nothing) Then
                    .Document.DefaultPageSettings = storedPageSettings
                End If

                'Invoke the Print Method of GridPrinter
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    GridPrinter.Print()
                End If
                objReportHeadercol.Clear()
                objPageHeadercol.Clear()
                objDetailscol.Clear()
                objPageFootercol.Clear()
                objReportFootercol.Clear()
                objcol1.Clear()
            End With
            PrintDialog1.Document = Nothing
            If (IsNothing(GridPrinter) = False) Then
                GridPrinter.Dispose()
                GridPrinter = Nothing
            End If

          
            
        End Sub

        Private Sub InvokePrintPreview(ByRef frm As Form, ByVal dggrid As DataGrid)
            'Instantiate GridPrinter class
            If GridPrinter Is Nothing Then
                GridPrinter = New DataGridPrinter(dggrid)
            End If

            'Invoke PrintDialog
            With Me.PrintPreviewDialog1
                .Document = GridPrinter.PrintDocument
                Dim objReportHeadercol As New Collection
                Dim objPageHeadercol As New Collection
                Dim objDetailscol As New Collection
                Dim objPageFootercol As New Collection
                Dim objReportFootercol As New Collection

                Dim objcol1 As New ArrayList


                'Fill Temporary sections collection 
                Dim objcontrol As Control
                For Each objcontrol In frm.Controls
                    If Not objcontrol.Tag Is Nothing Then
                        If objcontrol.Tag.substring(0, 1) = "1" Then
                            objReportHeadercol.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "2" Then
                            objPageHeadercol.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "3" Then
                            objcol1.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "4" Then
                            objPageFootercol.Add(objcontrol)
                        End If
                    End If

                Next
                'Populate Sections Collection
                GridPrinter.SetHeaderControls(objReportHeadercol, 1)
                GridPrinter.SetHeaderControls(objPageHeadercol, 2)
                GridPrinter.SetHeaderControls(objPageFootercol, 4)
                GridPrinter.SetHeaderControls(objReportFootercol, 5)

                If Not (storedPageSettings Is Nothing) Then
                    .Document.DefaultPageSettings = storedPageSettings
                End If


                'Invoke the Print Method of GridPrinter
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    GridPrinter.Print()
                End If
                objReportHeadercol.Clear()
                objPageHeadercol.Clear()
                objDetailscol.Clear()
                objPageFootercol.Clear()
                objReportFootercol.Clear()
                objcol1.Clear()
            End With
            PrintPreviewDialog1.Document = Nothing

            If (IsNothing(GridPrinter) = False) Then
                GridPrinter.Dispose()
                GridPrinter = Nothing
            End If
             
        End Sub

        Private Sub HideColumn(ByVal dggrid As DataGrid)
            Dim ts As New DataGridTableStyle

            Dim dgID As DataGridTextBoxColumn
            Dim i As Int16
            For i = 1 To C1FlexGrid1.Cols.Count - 1
                dgID = New DataGridTextBoxColumn
                With dgID

                    '.Alignment = HorizontalAlignment.Center
                    '.NullText = ""
                    .Width = C1FlexGrid1.Cols.Item(i).Width
                    .MappingName = C1FlexGrid1.Cols.Item(i).Caption
                    .HeaderText = C1FlexGrid1.Cols.Item(i).Caption
                End With
                ts.GridColumnStyles.Add(dgID)
                dgID = Nothing
            Next
            dggrid.TableStyles.Clear()
            dggrid.TableStyles.Add(ts)

        End Sub

        Private Sub PageSettings()
            Try
                Dim psDlg As New PageSetupDialog

                If (storedPageSettings Is Nothing) Then
                    storedPageSettings = New PageSettings
                End If

                psDlg.PageSettings = storedPageSettings
                psDlg.ShowDialog(System.Windows.Forms.Form.ActiveForm)
                psDlg.Dispose()
                psDlg = Nothing
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show("An error occurred - " + ex.Message)
            End Try

        End Sub

        'Private Sub PrintFlowSheet(ByVal blnPrint As Boolean, ByVal oPatientInfo As PatientInfo)
        '    Try
        '        Dim frm As New Form
        '        Dim objdatagrid As New DataGrid
        '        Dim objlabel As Label
        '        Dim fntTitle As New Font(New FontFamily("Arial"), 16, FontStyle.Bold)
        '        Dim fntTitle2 As Font
        '        Dim fntTitle3 As Font
        '        Dim objPrintFlowsheet As New ClsPrintFlowSheet
        '        'Dim PT As Point

        '        Dim objstruct As ClinicDetails
        '        objstruct = objPrintFlowsheet.ScanClinicInfo

        '        objlabel = New Label
        '        objlabel.Text = "Lab Result " & oPatientInfo.FlowSheetName
        '        objlabel.AutoSize = True
        '        objlabel.Tag = 1 & "Title"
        '        objlabel.Font = fntTitle
        '        frm.Controls.Add(objlabel)

        '        objlabel = New Label
        '        objlabel.Text = objstruct.m_Clinicname
        '        objlabel.AutoSize = True
        '        objlabel.Tag = 1 & "Clinicname"
        '        fntTitle2 = New Font(New FontFamily("Arial"), 13, FontStyle.Bold)
        '        objlabel.Font = fntTitle2
        '        frm.Controls.Add(objlabel)

        '        Dim fnt As New Font(New FontFamily("Arial"), 9)
        '        objlabel = New Label
        '        objlabel.AutoSize = True
        '        objlabel.Text = "Clinic Address : " & Replace(Trim(objstruct.m_ClinicAddress1), vbCrLf, "") '& " " & objstruct.m_ClincAddress2
        '        objlabel.Tag = 1 & "ClinicAddress1"

        '        objlabel.Font = fnt
        '        frm.Controls.Add(objlabel)

        '        objlabel = New Label
        '        objlabel.AutoSize = True
        '        objlabel.Font = fnt
        '        objlabel.Text = Replace(Trim(objstruct.m_ClincAddress2), vbCrLf, "")
        '        objlabel.Tag = 1 & "ClinicAddress2"
        '        frm.Controls.Add(objlabel)

        '        objlabel = New Label
        '        objlabel.AutoSize = True
        '        objlabel.Text = "Phone No : " & Replace(Trim(objstruct.m_PhoneNo), vbCrLf, "")
        '        objlabel.Tag = 1 & "PhoneNo"
        '        objlabel.Font = fnt
        '        frm.Controls.Add(objlabel)

        '        objlabel = New Label
        '        objlabel.AutoSize = True
        '        objlabel.Text = "Patient Name: " & oPatientInfo.PatientName & vbCrLf & "Patient Code : " & oPatientInfo.PatientCode   ''& gstrPatientFirstName & " " & gstrPatientLastName
        '        objlabel.Tag = 2 & "PatientName"
        '        fntTitle3 = New Font(New FontFamily("Arial"), 10, FontStyle.Bold)
        '        objlabel.Font = fntTitle3
        '        frm.Controls.Add(objlabel)


        '        objlabel = New Label
        '        objlabel.AutoSize = True
        '        objlabel.Text = "Date of Birth : " & oPatientInfo.PatientDOB & vbCrLf & "Gender : " & oPatientInfo.PatientGender   '' gstrPatientDOB
        '        objlabel.Tag = 2 & "PatientDOB"
        '        objlabel.Font = fntTitle
        '        frm.Controls.Add(objlabel)

        '        'to add gender of the patient
        '        'objlabel = New Label
        '        'With objlabel
        '        '    .AutoSize = True
        '        '    .Text = "Patient Gender: " & _PatientGender
        '        '    .Tag = 2 & "PatientGender"
        '        '    .Top = 200
        '        '    '.Location.Y = 200
        '        '    fntTitle = New Font(New FontFamily("Arial"), 11, FontStyle.Bold)
        '        '    .Font = fntTitle
        '        'End With

        '        frm.Controls.Add(objlabel)


        '        objlabel = New Label
        '        objlabel.AutoSize = True
        '        objlabel.Text = ""
        '        objlabel.Tag = 4 & "PageNo"
        '        objlabel.Font = fnt
        '        frm.Controls.Add(objlabel)

        '        objlabel = New Label
        '        objlabel.AutoSize = True
        '        objlabel.Text = ""
        '        objlabel.Tag = 4 & "PrintDate"
        '        objlabel.Font = fnt
        '        frm.Controls.Add(objlabel)

        '        objdatagrid.Font = fnt
        '        frm.Controls.Add(objdatagrid)

        '        BindToGrid(objdatagrid)
        '        frm.WindowState = FormWindowState.Maximized

        '        If blnPrint Then
        '            InvokePrint(frm, objdatagrid)
        '        Else
        '            InvokePrintPreview(frm, objdatagrid)
        '        End If
        '        Try

        '            frm.Dispose()
        '            frm = Nothing
        '            fnt.Dispose()
        '            fnt = Nothing
        '            fntTitle.Dispose()
        '            fntTitle = Nothing
        '            fntTitle2.Dispose()
        '            fntTitle2 = Nothing
        '            fntTitle3.Dispose()
        '            fntTitle3 = Nothing
        '            objdatagrid.TableStyles.Clear()
        '            objdatagrid.Dispose()
        '            objdatagrid = Nothing
        '            objlabel.Dispose()
        '            objlabel = Nothing
        '            objPrintFlowsheet = Nothing
        '        Catch ex As Exception

        '        End Try
        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '        MessageBox.Show("Error Printing Report - " & ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try

        'End Sub

        Private Sub BindToGrid(ByVal objdatagrid As DataGrid)
            Dim i As Int16
            Dim j As Int16

            dtgrid = New DataTable
            Dim objcol As DataColumn
            Dim objrow As DataRow
            'C1FlexGrid1.Styles.ClearUnused()

            For i = 1 To C1FlexGrid1.Cols.Count - 1
                objcol = New DataColumn(C1FlexGrid1.Cols.Item(i).Caption, GetType(System.String))
                'objcol = New DataColumn(C1FlexGrid1.Cols.Item(i).Caption, C1FlexGrid1.Cols(i).DataType)
                dtgrid.Columns.Add(objcol)
                objcol = Nothing
            Next

            'C1FlexGrid1.Styles.ClearUnused()
            If C1FlexGrid1.Rows.Count - 1 > 1 Then
                '' Remove Last Row '' Which is always Empty
                C1FlexGrid1.Rows.Remove(C1FlexGrid1.Rows.Count - 1)
            End If

            C1FlexGrid1.Styles.ClearUnused()

            For i = 1 To C1FlexGrid1.Rows.Count - 1
                objrow = dtgrid.NewRow
                For j = 1 To C1FlexGrid1.Cols.Count - 1
                    If IsNothing(C1FlexGrid1.GetData(i, j)) = False Then
                        Try
                            If C1FlexGrid1.Cols(j).EditMask.Trim = "" Then
                                If C1FlexGrid1.Cols(j).Format.Trim = "General" Then
                                    objrow.Item(j - 1) = C1FlexGrid1.GetData(i, j)
                                Else
                                    objrow.Item(j - 1) = Format(C1FlexGrid1.GetData(i, j), C1FlexGrid1.Cols(j).Format)
                                End If

                            Else
                                objrow.Item(j - 1) = C1FlexGrid1.GetData(i, j)
                            End If

                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        End Try
                        'Else
                        '    objrow.Item(j - 1) = ctype( "", GetDataType( C1FlexGrid1.Cols(j).DataType ))' gettype( C1FlexGrid1.Cols(j).DataType )' Format("", C1FlexGrid1.Cols(j).DataType)
                    End If
                Next
                dtgrid.Rows.Add(objrow)
                objrow = Nothing
            Next
            objdatagrid.DataSource = dtgrid
            HideColumn(objdatagrid)
        End Sub

        Private Sub setGridStyle(ByVal Id As Long)
            Dim dt As DataTable

            ocls_LM_LabResult.SelectFlowSheet(Id)
            dt = ocls_LM_LabResult.GetDataview.Table

            If IsNothing(dt) Then
                Exit Sub
            End If

            C1FlexGrid1.Styles.ClearUnused()
            C1FlexGrid1.Rows.Count = 1

            Dim i As Integer
            '' 0-sFlowSheetName ,1-nCols ,2-nColNumber ,3-sColumnName ,4-sFormat ,5-dWidth ,6-sFontName ,7-nFontSize ,
            '' 8-nForeColor, 9-bIsBold, 10-bIsItalic, 11-bIsUnderline, 12-sAlignment, 13-nBackColor
            If dt.Rows.Count > 0 Then
                C1FlexGrid1.Cols.Count = dt.Rows(i)(1) + 1
                C1FlexGrid1.Cols(0).Width = 25

                For i = 0 To dt.Rows.Count - 1
                    With C1FlexGrid1
                        Dim j As Integer
                        j = dt.Rows(i)(2)
                        '' to Set FixedRow's Alingnment to Center
                        .Cols(j).TextAlignFixed = TextAlignEnum.CenterCenter

                        .Cols(j).Caption = CType(dt.Rows(i)(3), String) ''Column Name
                        C1FlexGrid1.Cols(j).DataType = GetDataType(dt.Rows(i)(4)) ''Format
                        If InStr((dt.Rows(i)(4)), "#") Then
                            C1FlexGrid1.Cols(j).EditMask = GetMask(dt.Rows(i)(4))
                        Else
                            C1FlexGrid1.Cols(j).Format = GetMask(dt.Rows(i)(4))
                        End If
                        .Cols(j).Width = CType(dt.Rows(i)(5), Integer) ''Col width
                        ' .Cols(j).Style.Font = CType(dt.Rows(i)(6), Font)  ''Font Name
                        '.Cols(j).Style.Font = CType(dt.Rows(i)(7), Single)  ''Font Size
                        .Cols(j).Style.ForeColor = Color.FromArgb(dt.Rows(i)(8))  ''Fore Color
                        .Cols(j).Style.BackColor = Color.FromArgb(dt.Rows(i)(13)) ''Back Color
                        .Cols(j).Style.TextAlign = GetTextAlign(dt.Rows(i)(12))
                        .Cols(j).Style.WordWrap = True
                        '' Text Alignment 
                        '.Font = CType(dt.Rows(i)(6), Object)
                        '.FontHeight = dt.Rows(i)(7)
                    End With
                Next
            End If
            C1FlexGrid1.Styles.ClearUnused()

        End Sub

        Private Function GetDataType(ByVal StrType As String) As Type

            Select Case StrType
                Case "General"
                    Return GetType(System.String)
                    ''cmbFormat.Items.Add("1234") ''Int64
                Case "1234"
                    Return GetType(System.Int64)
                Case "1234.00"
                    Return GetType(System.Double)
                Case "-1,234.00"
                    Return GetType(System.Double)
                Case "(1,234.00)"
                    Return GetType(System.Double)
                Case "(1234.00)"
                    Return GetType(System.Double)
                Case "-1234.00"
                    Return GetType(System.Double)
                Case "-$1234.00"
                    Return GetType(System.Double)
                Case "$1,234.00"
                    Return GetType(System.Double)

                Case "Percentage"
                    Return GetType(System.Double)

                Case "MM/DD/YYYY"
                    Return GetType(System.DateTime)
                Case "DD/MM/YYYY"
                    Return GetType(System.DateTime)

                Case "DD/MMMM/YYYY"
                    Return GetType(System.DateTime)

                Case "MMMM DD/YYYY"
                    Return GetType(System.DateTime)

                Case "HH:MM:ss"
                    Return GetType(System.DateTime)
                Case "HH:MM"
                    Return GetType(System.DateTime)
                Case "HH:MM:ss PM"
                    Return GetType(System.DateTime)
                Case "HH:MM PM"
                    Return GetType(System.DateTime)
                Case "Masked"
                    Return GetType(System.String)
                Case Else
                    Return GetType(System.String)
            End Select

        End Function

        Private Function GetMask(ByVal StrType As String) As String
            Select Case StrType

                Case "1234"
                    Return "####"
                Case "1234.00"
                    Return "####.##"
                Case "-1,234.00"
                    Return "-#,###.##"
                Case "(1,234.00)"
                    Return "(#,###.##)"
                Case "(1234.00)"
                    Return "(####.##)"

                Case "-1234.00"
                    Return "-####.##"
                Case "-$1234.00"
                    Return "-$####.##"
                Case "$1,234.00"
                    Return "$#,###.##"

                Case "Percentage"
                    Return "0%"

                Case "MM/DD/YYYY"
                    Return "MM/dd/yyyy"
                Case "DD/MM/YYYY"
                    Return "dd/MM/yyyy"
                Case "MMMM DD/YYYY"
                    Return "MMMM dd/yyyy"
                Case "DD/MMMM/YYYY"
                    Return "dd/MMMM/yyyy"

                Case "HH:MM:ss"
                    Return "hh:mm:ss"
                Case "HH:MM"
                    Return "hh:mm"
                Case "HH:MM:ss PM"
                    Return "hh:mm:ss tt"
                Case "HH:MM PM"
                    Return "hh:mm tt"
                Case StrType
                    Return StrType
                Case Else
                    Return StrType
            End Select
        End Function

        'Private Function GetTextAlign(ByVal StrAlign As String) As [Enum]
        Private Function GetTextAlign(ByVal StrAlign As String) As TextAlignEnum
            '  Dim obje1 As enmAlign
            Select Case StrAlign
                Case "Left"
                    Return TextAlignEnum.LeftCenter
                Case "Center"
                    Return TextAlignEnum.CenterCenter
                Case "Right"
                    Return TextAlignEnum.RightCenter
                Case Else
                    Return TextAlignEnum.GeneralCenter
            End Select
        End Function

        'Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        '    Try
        '        Dim strDia As String = ""
        '        ' Dim LabResultID As Long
        '        Dim i As Integer
        '        Dim j As Integer
        '        Dim dt As New DataTable
        '        Dim dtPatientDetails As New DataTable
        '        With chklstbx
        '            For i = 0 To .CheckedItems.Count - 1
        '                .SelectedItem = .CheckedItems(i)   '
        '                If i = 0 Then
        '                    strDia = chklstbx.SelectedValue
        '                Else
        '                    strDia = strDia & "," & chklstbx.SelectedValue

        '                    'Label1.Text = Split(str, "'").ToString
        '                End If
        '            Next
        '            ' strDia = "928.11-KNEE CRUSH INJURY" & "," & "726.19-SHOULDER IMPINGEMENT SYNDROME/BURSITIS" & "," & "808.8-CLOSED FRACTURE OF PELVIS"

        '            _FlowSheetID = cmbLabResult.SelectedValue
        '            _FlowSheetName = cmbLabResult.Text

        '            Dim oDB As New gloDataBase.gloDataBase
        '            With oDB
        '                Dim _strSQL As String = ""
        '                Dim _strPatientDetails As String = ""

        '                '_strSQL = " SELECT DISTINCT LM_Orders.lm_Patient_ID, LM_LabResult.nFlowSheetRecordID  " _
        '                '        & " FROM         LM_LabResult INNER JOIN " _
        '                '        & " LM_Orders ON LM_LabResult.nPatientID = LM_Orders.lm_Patient_ID INNER JOIN " _
        '                '        & " Diagnosis ON LM_LabResult.nPatientID = Diagnosis.nPatientID " _
        '                '        & " WHERE     ((Diagnosis.sICD9Code & " - " & Diagnosis.sICD9Description) IN ('" & strDia & "')) AND (LM_LabResult.nFlowSheetID = " & LabResultID & " )"

        '                '_strSQL = " SELECT DISTINCT LM_Orders.lm_Patient_ID, Patient.sPatientCode, Patient.sFirstName, Patient.sMiddleName, Patient.sLastName, Patient.nSSN, Patient.dtDOB, Patient.sGender, " _
        '                '        & " LM_LabResult.nFlowSheetRecordID,LM_LabResult.dtVisitDate " _
        '                '        & " FROM         LM_LabResult INNER JOIN & " _
        '                '        & " LM_Orders ON LM_LabResult.nPatientID = LM_Orders.lm_Patient_ID INNER JOIN " _
        '                '        & " Diagnosis ON LM_LabResult.nPatientID = Diagnosis.nPatientID INNER JOIN " _
        '                '        & " Patient ON LM_LabResult.nPatientID = Patient.nPatientID " _
        '                '        & " WHERE ((Diagnosis.sICD9Code '+ " - " +' Diagnosis.sICD9Description) IN ('" & strDia & "')) AND (LM_LabResult.nFlowSheetID = " & _FlowSheetID & " )" _
        '                '        & " AND (LM_LabResult.dtVisitDate = (SELECT MAX(LM_LabResult.dtVisitDate) FROM LM_LabResult)) "

        '                _strSQL = " SELECT DISTINCT Diagnosis.nPatientID " _
        '                        & " FROM Diagnosis INNER JOIN " _
        '                        & " ICD9 ON Diagnosis.sICD9Code = ICD9.sICD9Code AND Diagnosis.sICD9Description = ICD9.sDescription CROSS JOIN LM_LabResult " _
        '                        & " WHERE (LM_LabResult.nFlowSheetID = " & _FlowSheetID & " )AND (ICD9.nICD9ID IN (" & strDia & "))"

        '                '_strPatientDetails = "SELECT  Patient.nPatientID , Patient.sFirstName , Patient.sLastName , Patient.dtDOB , Patient.sGender , Patient.nSSN, LM_LabResult.dtVisitDate , LM_LabResult.nFlowSheetID " _
        '                '                                       & " FROM Patient INNER JOIN " _
        '                '                                       & " LM_LabResult ON Patient.nPatientID = LM_LabResult.nPatientID " _
        '                '                                       & " WHERE LM_LabResult.dtVisitDate = (SELECT MAX(LM_LabResult.dtVisitDate) FROM LM_LabResult)"





        '                .Connect(GetConnectionString)

        '                '.DBParameters.Add("@Diagnosis", strDia, ParameterDirection.Input, SqlDbType.VarChar, 1000)
        '                '.DBParameters.Add("@FlowSheetID", _FlowSheetID, ParameterDirection.Input, SqlDbType.BigInt)
        '                'dt = .ReadData("gLM_GetLabResultReport")   ''_strSQL)
        '                dt = .ReadQueryDataTable(_strSQL)
        '                ' dtPatientDetails = .ReadQueryDataTable(_strPatientDetails)
        '                .Disconnect()

        '                If IsNothing(dt) = False Then
        '                    For i = 0 To dt.Rows.Count - 1

        '                        _PatientID = dt.Rows(i)("nPatientID")

        '                        _strSQL = "SELECT Patient.sFirstName , Patient.sLastName ,Patient.sPatientCode, Patient.dtDOB , Patient.sGender , Patient.nSSN, LM_LabResult.dtVisitDate , LM_LabResult.nFlowSheetRecordID " _
        '                                & " FROM Patient INNER JOIN " _
        '                                & " LM_LabResult ON Patient.nPatientID = LM_LabResult.nPatientID " _
        '                                & " WHERE LM_LabResult.dtVisitDate = (SELECT MAX(LM_LabResult.dtVisitDate) FROM LM_LabResult WHERE LM_LabResult.nPatientID = " & _PatientID & ") " _
        '                                & " and Patient.nPatientID = " & _PatientID & " "
        '                        .Connect(GetConnectionString)
        '                        dtPatientDetails = .ReadQueryDataTable(_strSQL)
        '                        ' = .ReadQueryDataTable(_strPatientDetails)
        '                        .Disconnect()
        '                        If IsNothing(dtPatientDetails) = False Then
        '                            If dtPatientDetails.Rows.Count > 0 Then
        '                                For j = 0 To dtPatientDetails.Rows.Count - 1
        '                                    '_PatientID = dt.Rows(i)("nPatientID")
        '                                    '_PatientName = dt.Rows(i)("sFirstName") & " " & dt.Rows(i)("sLastName")
        '                                    _PatientName = dtPatientDetails.Rows(j)("sFirstName") & " " & dtPatientDetails.Rows(j)("sLastName")

        '                                    _PatientCode = dtPatientDetails.Rows(j)("sPatientCode")

        '                                    _PatientDOB = dtPatientDetails.Rows(j)("dtDOB")
        '                                    _PatientGender = dtPatientDetails.Rows(j)("sGender")
        '                                    _PatientSSN = dtPatientDetails.Rows(j)("nSSN")
        '                                    _FlowSheetRecID = dtPatientDetails.Rows(j)("nFlowSheetRecordID")
        '                                    _VisitDate = dtPatientDetails.Rows(j)("dtVisitDate")


        '                                    Call Fill_OrderLabResult()

        '                                    Call PrintFlowSheet(True)

        '                                Next 'for j loop
        '                            End If

        '                        End If ' for dtPatientDetails


        '                    Next 'for i loop
        '                End If ' for dt



        '            End With
        '        End With

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try
        'End Sub


        Private Sub txtSearchDia_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchDia.TextChanged
            Try
                For i As Integer = 0 To chklstbx.Items.Count - 1
                    With chklstbx
                        Dim str As String
                        Dim drv As DataRowView
                        drv = CType(.Items(i), DataRowView)
                        'str = UCase(Trim(.Items(i).Text))
                        If rbICD9Code.Checked = True Then
                            '' sDescription, ICD9code 
                            str = UCase(drv("ICD9code"))
                        Else
                            str = UCase(drv("sDescription"))
                        End If

                        If Mid(str, 1, Len(Trim(txtSearchDia.Text))) = UCase(Trim(txtSearchDia.Text)) Then
                            chklstbx.SelectedItem = chklstbx.Items(i)
                            txtSearchDia.Focus()
                            Exit Sub
                        End If
                    End With
                Next
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub txtLabResult_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLabResult.TextChanged
            Try
                For i As Integer = 0 To chkLabResult.Items.Count - 1
                    With chkLabResult
                        Dim str As String
                        Dim drv As DataRowView
                        drv = CType(.Items(i), DataRowView)
                        'str = UCase(Trim(.Items(i).Text))
                        str = UCase(drv(1))
                        If Mid(str, 1, Len(Trim(txtLabResult.Text))) = UCase(Trim(txtLabResult.Text)) Then
                            chkLabResult.SelectedItem = chkLabResult.Items(i)
                            txtLabResult.Focus()
                            Exit Sub
                        End If
                    End With
                Next
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub txtSearchDia_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchDia.KeyPress
            Try
                If (e.KeyChar = ChrW(13)) Then
                    chklstbx.Select()
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End Sub

        Private Sub txtLabResult_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLabResult.KeyPress
            Try
                If (e.KeyChar = ChrW(13)) Then
                    chkLabResult.Select()
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End Sub

        Private Sub frmPatientDiagnosis_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
            Try
                'Dim objAudit As New clsAudit
                'objAudit.CreateLog(clsAudit.enmActivityType.Other, "Patient Diagnosis Lab Results Closed", gstrLoginName, gstrClientMachineName)
                'objAudit = Nothing
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Close, "Patient Diagnosis Lab Results Closed", gloAuditTrail.ActivityOutCome.Success)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End Sub

        Private Sub tlsp_PatientDiagnosis_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PatientDiagnosis.ItemClicked
            Try
                Select Case e.ClickedItem.Tag
                    Case "Show Report"
                        ShowReportPatientDiagnosis()

                    Case "Close"
                        Me.Close()

                End Select

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
            End Try
        End Sub

        Private Sub rbICD9Code_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbICD9Code.CheckedChanged
            If rbICD9Code.Checked = True Then
                rbICD9Code.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            Else
                rbICD9Code.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            End If
        End Sub

        Private Sub rbDesc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDesc.CheckedChanged
            If rbICD9Code.Checked = True Then
                rbICD9Code.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            Else
                rbICD9Code.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            End If
        End Sub

        Private Sub C1FlexGrid1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1FlexGrid1.MouseMove
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        End Sub
    End Class

    Public Class PatientInfo
        Dim _PatientID As Long
        Dim _IsExempted As Boolean
        Dim _PatientName As String
        Dim _PatientCode As String
        Dim _PatientDOB As String
        Dim _PatientAge As String
        Dim _PatientGender As String
        Dim _PatientSSN As String
        Dim _FlowSheetRecID As Long
        Dim _FlowSheetID As Long
        Dim _FlowSheetName As String
        Dim _VisitDate As String
        Dim _Result As String
        Dim _Flex As C1.Win.C1FlexGrid.C1FlexGrid

        Public Property PatientId() As Long
            Get
                Return _PatientID
            End Get
            Set(ByVal Value As Long)
                _PatientID = Value
            End Set
        End Property

        Public Property IsExempted() As Boolean
            Get
                Return _IsExempted
            End Get
            Set(ByVal Value As Boolean)
                _IsExempted = Value
            End Set
        End Property

        Public Property PatientName() As String
            Get
                Return _PatientName
            End Get
            Set(ByVal Value As String)
                _PatientName = Value
            End Set
        End Property

        Public Property PatientCode() As String
            Get
                Return _PatientCode
            End Get
            Set(ByVal Value As String)
                _PatientCode = Value
            End Set
        End Property

        Public Property PatientDOB() As String
            Get
                Return _PatientDOB
            End Get
            Set(ByVal Value As String)
                _PatientDOB = Value
            End Set
        End Property

        Public Property PatientAge() As String
            Get
                Return _PatientAge
            End Get
            Set(ByVal Value As String)
                _PatientAge = Value
            End Set
        End Property

        Public Property PatientGender() As String
            Get
                Return _PatientGender
            End Get
            Set(ByVal Value As String)
                _PatientGender = Value
            End Set
        End Property

        Public Property PatientSSN() As String
            Get
                Return _PatientSSN
            End Get
            Set(ByVal Value As String)
                _PatientSSN = Value
            End Set
        End Property

        Public Property FlowSheetRecID() As Long
            Get
                Return _FlowSheetRecID
            End Get
            Set(ByVal Value As Long)
                _FlowSheetRecID = Value
            End Set
        End Property

        Public Property FlowSheetID() As Long
            Get
                Return _FlowSheetID
            End Get
            Set(ByVal Value As Long)
                _FlowSheetID = Value
            End Set
        End Property

        Public Property FlowSheetName() As String
            Get
                Return _FlowSheetName
            End Get
            Set(ByVal Value As String)
                _FlowSheetName = Value
            End Set
        End Property


        Public Property VisitDate() As String
            Get
                Return _VisitDate
            End Get
            Set(ByVal Value As String)
                _VisitDate = Value
            End Set
        End Property

        Public Property Flex() As C1.Win.C1FlexGrid.C1FlexGrid
            Get
                Return _Flex
            End Get
            Set(ByVal Value As C1.Win.C1FlexGrid.C1FlexGrid)
                _Flex = Value
            End Set
        End Property

        Public Property Result() As String
            Get
                Return _Result
            End Get
            Set(ByVal Value As String)
                _Result = Value
            End Set
        End Property
    End Class

End Namespace