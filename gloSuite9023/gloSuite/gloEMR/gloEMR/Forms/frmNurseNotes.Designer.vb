<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNurseNotes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            MyBase.Dispose(disposing)
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
                Try
                    If (IsNothing(_PatientStrip) = False) Then
                        _PatientStrip.Dispose()
                        _PatientStrip = Nothing
                    End If
                Catch ex As Exception

                End Try
                Dim dtpControls(0) As System.Windows.Forms.DateTimePicker
                dtpControls(0) = dtLetterDate
                If Not IsNothing(dtpControls) Then
                    If dtpControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                        gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                    End If
                End If
              

                Try
                    If (IsNothing(GloUC_TemplateTreeControl_NursesNotes) = False) Then
                        If (IsNothing(GloUC_TemplateTreeControl_NursesNotes.DocCriteria) = False) Then
                            DirectCast(GloUC_TemplateTreeControl_NursesNotes.DocCriteria, gloEMR.gloEMRWord.DocCriteria).Dispose()
                            GloUC_TemplateTreeControl_NursesNotes.DocCriteria = Nothing
                        End If
                    End If
                Catch

                End Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    Try
                        DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, gloEMR.gloEMRWord.DocCriteria).Dispose()

                    Catch

                    End Try
                    GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                End If
                Try
                    If (IsNothing(GloUC_PastWordNotes1) = False) Then
                        If (IsNothing(GloUC_PastWordNotes1.OBJCRITERIAs) = False) Then
                            DirectCast(GloUC_PastWordNotes1.OBJCRITERIAs, gloEMR.gloEMRWord.DocCriteria).Dispose()
                            GloUC_PastWordNotes1.OBJCRITERIAs = Nothing
                        End If
                    End If
                Catch

                End Try
            End If
        Finally

        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNurseNotes))
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.lblNurseDt = New System.Windows.Forms.Label()
        Me.dtLetterDate = New System.Windows.Forms.DateTimePicker()
        Me.lblSelectTem = New System.Windows.Forms.Label()
        Me.pnlwdNurseNotes = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.wdNurseNotes = New AxDSOFramer.AxFramerControl()
        Me.pnlCombo = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlWordToolStrip = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlGloUC_PastWordNotes = New System.Windows.Forms.Panel()
        Me.GloUC_PastWordNotes1 = New gloUserControlLibrary.gloUC_PastWordNotes()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlGloUC_TemplateTreeControl = New System.Windows.Forms.Panel()
        Me.GloUC_TemplateTreeControl_NursesNotes = New gloUserControlLibrary.gloUC_TemplateTreeControl()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnPastExams = New System.Windows.Forms.Button()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlgloUC_Addendum = New System.Windows.Forms.Panel()
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.pnlwdNurseNotes.SuspendLayout()
        CType(Me.wdNurseNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCombo.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlWordToolStrip.SuspendLayout()
        Me.pnlGloUC_PastWordNotes.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlGloUC_TemplateTreeControl.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplate.Location = New System.Drawing.Point(127, 2)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(200, 22)
        Me.cmbTemplate.TabIndex = 30
        '
        'lblNurseDt
        '
        Me.lblNurseDt.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblNurseDt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNurseDt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.lblNurseDt.Location = New System.Drawing.Point(520, 1)
        Me.lblNurseDt.Name = "lblNurseDt"
        Me.lblNurseDt.Size = New System.Drawing.Size(100, 24)
        Me.lblNurseDt.TabIndex = 32
        Me.lblNurseDt.Text = "  Nurses Date :"
        Me.lblNurseDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtLetterDate
        '
        Me.dtLetterDate.CalendarFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtLetterDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtLetterDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtLetterDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtLetterDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtLetterDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtLetterDate.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtLetterDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtLetterDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtLetterDate.Location = New System.Drawing.Point(620, 1)
        Me.dtLetterDate.Name = "dtLetterDate"
        Me.dtLetterDate.Size = New System.Drawing.Size(162, 22)
        Me.dtLetterDate.TabIndex = 31
        '
        'lblSelectTem
        '
        Me.lblSelectTem.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSelectTem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectTem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.lblSelectTem.Location = New System.Drawing.Point(1, 1)
        Me.lblSelectTem.Name = "lblSelectTem"
        Me.lblSelectTem.Size = New System.Drawing.Size(126, 24)
        Me.lblSelectTem.TabIndex = 29
        Me.lblSelectTem.Text = "  Select Template :"
        Me.lblSelectTem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlwdNurseNotes
        '
        Me.pnlwdNurseNotes.Controls.Add(Me.Label2)
        Me.pnlwdNurseNotes.Controls.Add(Me.Label3)
        Me.pnlwdNurseNotes.Controls.Add(Me.Label4)
        Me.pnlwdNurseNotes.Controls.Add(Me.Label9)
        Me.pnlwdNurseNotes.Controls.Add(Me.wdNurseNotes)
        Me.pnlwdNurseNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlwdNurseNotes.Location = New System.Drawing.Point(0, 29)
        Me.pnlwdNurseNotes.Name = "pnlwdNurseNotes"
        Me.pnlwdNurseNotes.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlwdNurseNotes.Size = New System.Drawing.Size(833, 531)
        Me.pnlwdNurseNotes.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 530)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(831, 1)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 527)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(832, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 527)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(833, 1)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "label1"
        '
        'wdNurseNotes
        '
        Me.wdNurseNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdNurseNotes.Enabled = True
        Me.wdNurseNotes.Location = New System.Drawing.Point(0, 3)
        Me.wdNurseNotes.Name = "wdNurseNotes"
        Me.wdNurseNotes.OcxState = CType(resources.GetObject("wdNurseNotes.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdNurseNotes.Size = New System.Drawing.Size(833, 528)
        Me.wdNurseNotes.TabIndex = 5
        '
        'pnlCombo
        '
        Me.pnlCombo.BackColor = System.Drawing.Color.Transparent
        Me.pnlCombo.Controls.Add(Me.Panel2)
        Me.pnlCombo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCombo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlCombo.Location = New System.Drawing.Point(0, 0)
        Me.pnlCombo.Name = "pnlCombo"
        Me.pnlCombo.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlCombo.Size = New System.Drawing.Size(833, 29)
        Me.pnlCombo.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.Panel2.Controls.Add(Me.cmbTemplate)
        Me.Panel2.Controls.Add(Me.lblNurseDt)
        Me.Panel2.Controls.Add(Me.dtLetterDate)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.lblSelectTem)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(833, 26)
        Me.Panel2.TabIndex = 0
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(336, 3)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(48, 20)
        Me.GloUC_AddRefreshDic1.TabIndex = 34
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'Label11
        '
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(782, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 24)
        Me.Label11.TabIndex = 33
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(127, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 24)
        Me.Label1.TabIndex = 32
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(831, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 25)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(832, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 25)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(833, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'pnlWordToolStrip
        '
        Me.pnlWordToolStrip.Controls.Add(Me.Label10)
        Me.pnlWordToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlWordToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlWordToolStrip.Name = "pnlWordToolStrip"
        Me.pnlWordToolStrip.Size = New System.Drawing.Size(1276, 22)
        Me.pnlWordToolStrip.TabIndex = 13
        '
        'Label10
        '
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(170, 22)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Word ToolStrip panel :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label10.Visible = False
        '
        'pnlGloUC_PastWordNotes
        '
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.GloUC_PastWordNotes1)
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.Panel4)
        Me.pnlGloUC_PastWordNotes.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_PastWordNotes.Location = New System.Drawing.Point(0, 22)
        Me.pnlGloUC_PastWordNotes.Name = "pnlGloUC_PastWordNotes"
        Me.pnlGloUC_PastWordNotes.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlGloUC_PastWordNotes.Size = New System.Drawing.Size(217, 563)
        Me.pnlGloUC_PastWordNotes.TabIndex = 14
        '
        'GloUC_PastWordNotes1
        '
        Me.GloUC_PastWordNotes1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GloUC_PastWordNotes1.CLSPATIENTEXAMSs = Nothing
        Me.GloUC_PastWordNotes1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_PastWordNotes1.EXAMNEWDOCUMENTNAMEs = Nothing
        Me.GloUC_PastWordNotes1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GloUC_PastWordNotes1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GloUC_PastWordNotes1.FromForms = Nothing
        Me.GloUC_PastWordNotes1.Location = New System.Drawing.Point(3, 26)
        Me.GloUC_PastWordNotes1.M_LETTERIDs = CType(0, Long)
        Me.GloUC_PastWordNotes1.M_VISITIDs = CType(0, Long)
        Me.GloUC_PastWordNotes1.Name = "GloUC_PastWordNotes1"
        Me.GloUC_PastWordNotes1.OBJCLSDISCLOSUREs = Nothing
        Me.GloUC_PastWordNotes1.OBJCLSNOTESs = Nothing
        Me.GloUC_PastWordNotes1.OBJCLSPATIENTCONSENTs = Nothing
        Me.GloUC_PastWordNotes1.OBJCLSPATIENTLETTERSs = Nothing
        Me.GloUC_PastWordNotes1.OBJCLSPTPROTOCOLs = Nothing
        Me.GloUC_PastWordNotes1.OBJCRITERIAs = Nothing
        Me.GloUC_PastWordNotes1.OBJWORDs = Nothing
        Me.GloUC_PastWordNotes1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.GloUC_PastWordNotes1.PATIENTIDs = CType(0, Long)
        Me.GloUC_PastWordNotes1.SHOWPASTs = False
        Me.GloUC_PastWordNotes1.Size = New System.Drawing.Size(214, 534)
        Me.GloUC_PastWordNotes1.STRFORMNAMEs = "0"
        Me.GloUC_PastWordNotes1.TabIndex = 0
        Me.GloUC_PastWordNotes1.TLSMESSAGESs = Nothing
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(3, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(214, 26)
        Me.Panel4.TabIndex = 5
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.Label15)
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(214, 26)
        Me.Panel5.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(1, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(169, 24)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "  Past Nurses Notes"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 25)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(212, 1)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 25)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(213, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 25)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(214, 1)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(217, 22)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 563)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'pnlGloUC_TemplateTreeControl
        '
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.GloUC_TemplateTreeControl_NursesNotes)
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.Panel6)
        Me.pnlGloUC_TemplateTreeControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_TemplateTreeControl.Location = New System.Drawing.Point(220, 22)
        Me.pnlGloUC_TemplateTreeControl.Name = "pnlGloUC_TemplateTreeControl"
        Me.pnlGloUC_TemplateTreeControl.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGloUC_TemplateTreeControl.Size = New System.Drawing.Size(217, 563)
        Me.pnlGloUC_TemplateTreeControl.TabIndex = 16
        '
        'GloUC_TemplateTreeControl_NursesNotes
        '
        Me.GloUC_TemplateTreeControl_NursesNotes.DocCriteria = Nothing
        Me.GloUC_TemplateTreeControl_NursesNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TemplateTreeControl_NursesNotes.Location = New System.Drawing.Point(0, 26)
        Me.GloUC_TemplateTreeControl_NursesNotes.Name = "GloUC_TemplateTreeControl_NursesNotes"
        Me.GloUC_TemplateTreeControl_NursesNotes.ObjClsWord = Nothing
        Me.GloUC_TemplateTreeControl_NursesNotes.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.GloUC_TemplateTreeControl_NursesNotes.ProviderId = CType(0, Long)
        Me.GloUC_TemplateTreeControl_NursesNotes.Size = New System.Drawing.Size(217, 534)
        Me.GloUC_TemplateTreeControl_NursesNotes.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnPastExams)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(217, 26)
        Me.Panel6.TabIndex = 1
        '
        'btnPastExams
        '
        Me.btnPastExams.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnPastExams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPastExams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPastExams.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPastExams.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPastExams.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPastExams.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPastExams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPastExams.Location = New System.Drawing.Point(0, 0)
        Me.btnPastExams.Name = "btnPastExams"
        Me.btnPastExams.Size = New System.Drawing.Size(217, 26)
        Me.btnPastExams.TabIndex = 0
        Me.btnPastExams.Text = "Show Past Nurses Notes"
        Me.btnPastExams.UseVisualStyleBackColor = True
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(437, 22)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 563)
        Me.Splitter2.TabIndex = 17
        Me.Splitter2.TabStop = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlgloUC_Addendum)
        Me.pnlMain.Controls.Add(Me.pnlwdNurseNotes)
        Me.pnlMain.Controls.Add(Me.pnlCombo)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(440, 22)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(836, 563)
        Me.pnlMain.TabIndex = 18
        '
        'pnlgloUC_Addendum
        '
        Me.pnlgloUC_Addendum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlgloUC_Addendum.Location = New System.Drawing.Point(0, 29)
        Me.pnlgloUC_Addendum.Name = "pnlgloUC_Addendum"
        Me.pnlgloUC_Addendum.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlgloUC_Addendum.Size = New System.Drawing.Size(833, 531)
        Me.pnlgloUC_Addendum.TabIndex = 5
        '
        'tmrDocProtect
        '
        '
        'frmNurseNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1276, 585)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.pnlGloUC_TemplateTreeControl)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlGloUC_PastWordNotes)
        Me.Controls.Add(Me.pnlWordToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmNurseNotes"
        Me.Text = "Nurses Notes"
        Me.pnlwdNurseNotes.ResumeLayout(False)
        CType(Me.wdNurseNotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCombo.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlWordToolStrip.ResumeLayout(False)
        Me.pnlGloUC_PastWordNotes.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlGloUC_TemplateTreeControl.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents lblNurseDt As System.Windows.Forms.Label
    Friend WithEvents dtLetterDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSelectTem As System.Windows.Forms.Label
    Friend WithEvents wdNurseNotes As AxDSOFramer.AxFramerControl
    'Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    Friend WithEvents pnlwdNurseNotes As System.Windows.Forms.Panel
    Friend WithEvents pnlCombo As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlWordToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlGloUC_PastWordNotes As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlGloUC_TemplateTreeControl As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GloUC_PastWordNotes1 As gloUserControlLibrary.gloUC_PastWordNotes
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GloUC_TemplateTreeControl_NursesNotes As gloUserControlLibrary.gloUC_TemplateTreeControl
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnPastExams As System.Windows.Forms.Button
    Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    Friend WithEvents pnlgloUC_Addendum As System.Windows.Forms.Panel
End Class
