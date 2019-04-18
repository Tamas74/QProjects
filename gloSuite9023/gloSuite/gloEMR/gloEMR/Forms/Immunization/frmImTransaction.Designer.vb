<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImTransaction
    Inherits gloEMR.frmBaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Dim dtpControls As DateTimePicker() = {dttransaction_date, dtTransactionTime, dtPatientDied, dtOnsetDate, dtDueDate, dtpublication_date, dtexpDate}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                Catch ex As Exception

                End Try
                

                If (IsNothing(tooltip) = False) Then
                    tooltip.Dispose()
                    tooltip = Nothing
                End If
                If (IsNothing(ToolTip1) = False) Then
                    ToolTip1.Dispose()
                    ToolTip1 = Nothing
                End If
                If (IsNothing(ofrmDiagnosisList) = False) Then
                    ofrmDiagnosisList.Dispose()
                    ofrmDiagnosisList = Nothing
                End If
                If (IsNothing(ofrmList) = False) Then
                    ofrmList.Dispose()
                    ofrmList = Nothing
                End If
                If (IsNothing(objfrmHistory) = False) Then
                    objfrmHistory.Dispose()
                    objfrmHistory = Nothing
                End If
                If (IsNothing(_lst) = False) Then
                    _lst.Dispose()
                    _lst = Nothing
                End If
                If (IsNothing(combo) = False) Then
                    combo.Dispose()
                    combo = Nothing
                End If
                If (IsNothing(hashtblItemName) = False) Then
                    hashtblItemName.Clear()
                    hashtblItemName = Nothing
                End If

                If (IsNothing(oCVXControl) = False) Then
                    oCVXControl.Dispose()
                    oCVXControl = Nothing
                End If
                If (IsNothing(oTradeNameControl) = False) Then
                    oTradeNameControl.Dispose()
                    oTradeNameControl = Nothing
                End If
                If (IsNothing(oMVXControl) = False) Then
                    oMVXControl.Dispose()
                    oMVXControl = Nothing
                End If
                If (IsNothing(oDiagnosisListControl) = False) Then
                    oDiagnosisListControl.Dispose()
                    oDiagnosisListControl = Nothing
                End If
                If (IsNothing(oListControl) = False) Then
                    oListControl.Dispose()
                    oListControl = Nothing
                End If

            End If
        Finally
            MyBase.Dispose(disposing)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImTransaction))
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmbAdministred = New System.Windows.Forms.ComboBox()
        Me.dttransaction_date = New System.Windows.Forms.DateTimePicker()
        Me.optRefused = New System.Windows.Forms.RadioButton()
        Me.optReported = New System.Windows.Forms.RadioButton()
        Me.optAdministered = New System.Windows.Forms.RadioButton()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.TabImmunization = New System.Windows.Forms.TabControl()
        Me.TabPageAdministratin = New System.Windows.Forms.TabPage()
        Me.publicityeffetiveDTP = New System.Windows.Forms.DateTimePicker()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.btnclrcqm = New System.Windows.Forms.Button()
        Me.btnbrwcqm = New System.Windows.Forms.Button()
        Me.txtcqm = New System.Windows.Forms.TextBox()
        Me.lblcqm = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optPartiallyAdministered = New System.Windows.Forms.RadioButton()
        Me.optNotAdministered = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblImmunizationFunding = New System.Windows.Forms.Label()
        Me.cmbImmunizationFunding = New System.Windows.Forms.ComboBox()
        Me.btnClearRefusalReason = New System.Windows.Forms.Button()
        Me.btnBrwRefusalReason = New System.Windows.Forms.Button()
        Me.txtRefusalReason = New System.Windows.Forms.TextBox()
        Me.lblOrdProvider = New System.Windows.Forms.Label()
        Me.btnClrRefProvider = New System.Windows.Forms.Button()
        Me.btnRefProvider = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.BtnUncertainCVX = New System.Windows.Forms.Button()
        Me.dtTransactionTime = New System.Windows.Forms.DateTimePicker()
        Me.btnAddPublicityCodeItem = New System.Windows.Forms.Button()
        Me.cmbPublicityCode = New System.Windows.Forms.ComboBox()
        Me.lblPublicityCode = New System.Windows.Forms.Label()
        Me.lblSnomedIdValue = New System.Windows.Forms.Label()
        Me.btnClearSnomed = New System.Windows.Forms.Button()
        Me.btnBrowseSnomed = New System.Windows.Forms.Button()
        Me.lblSnomed = New System.Windows.Forms.Label()
        Me.btnAddUnitsOfMeasureItem = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_units = New System.Windows.Forms.TextBox()
        Me.pnlTradeNameControl = New System.Windows.Forms.Panel()
        Me.BtnAddVaccineCategory = New System.Windows.Forms.Button()
        Me.BtnAddManufacturerCategory = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnAddCategory = New System.Windows.Forms.Button()
        Me.BtnAddTradeNameCategory = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.txt_refused_by = New System.Windows.Forms.TextBox()
        Me.pnlMvxControl = New System.Windows.Forms.Panel()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.pnlCvxControl = New System.Windows.Forms.Panel()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.btnClearCPT = New System.Windows.Forms.Button()
        Me.btnClearDiagnosis = New System.Windows.Forms.Button()
        Me.txt_notes = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNDCcode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblAdministeredTime = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnBrowsCPT = New System.Windows.Forms.Button()
        Me.btnBrowsDiagnosis = New System.Windows.Forms.Button()
        Me.lblProviderName = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbFunding = New System.Windows.Forms.ComboBox()
        Me.cmbIcd = New System.Windows.Forms.ComboBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.cmbCpt = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.dtDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.chkSetReminder = New System.Windows.Forms.CheckBox()
        Me.dtexpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.btnSearchsku = New System.Windows.Forms.Button()
        Me.lblFunding = New System.Windows.Forms.Label()
        Me.lblRefusedBy = New System.Windows.Forms.Label()
        Me.btnTradeName = New System.Windows.Forms.Button()
        Me.lblRefusalreason = New System.Windows.Forms.Label()
        Me.btnCvx = New System.Windows.Forms.Button()
        Me.cmbRefusalreason = New System.Windows.Forms.ComboBox()
        Me.btnMvx = New System.Windows.Forms.Button()
        Me.txt_refusal_comments = New System.Windows.Forms.TextBox()
        Me.cmbSKU = New System.Windows.Forms.ComboBox()
        Me.cmbLotNumber = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblTradeName = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.lblLotNumber = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtMvx = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCvx = New System.Windows.Forms.TextBox()
        Me.txt_TradeName = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txt_vis = New System.Windows.Forms.TextBox()
        Me.cmbRoute = New System.Windows.Forms.ComboBox()
        Me.dtpublication_date = New System.Windows.Forms.DateTimePicker()
        Me.cmbSite = New System.Windows.Forms.ComboBox()
        Me.txt_dosage_given = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txt_amount_given = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblDosesOnHand = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblDosesGiven = New System.Windows.Forms.Label()
        Me.cmbUnitOfMeasure = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabPageReaction = New System.Windows.Forms.TabPage()
        Me.grpReaction = New System.Windows.Forms.GroupBox()
        Me.dtOnsetDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnBrowseReaction = New System.Windows.Forms.Button()
        Me.lstReaction = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rdo_PatientRecoveredNo = New System.Windows.Forms.RadioButton()
        Me.rdo_PatientRecoveredUnknown = New System.Windows.Forms.RadioButton()
        Me.rdo_PatientRecoveredYes = New System.Windows.Forms.RadioButton()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.dtPatientDied = New System.Windows.Forms.DateTimePicker()
        Me.txtHospitalizationDays = New System.Windows.Forms.TextBox()
        Me.chk_NoneOfTheAbove = New System.Windows.Forms.CheckBox()
        Me.chk_ResultedInPermDisability = New System.Windows.Forms.CheckBox()
        Me.chk_RequiredEmergencyRoom = New System.Windows.Forms.CheckBox()
        Me.chk_ResultedInProlongation = New System.Windows.Forms.CheckBox()
        Me.chk_LifeThreateningIllness = New System.Windows.Forms.CheckBox()
        Me.chk_RequiredHospitalization = New System.Windows.Forms.CheckBox()
        Me.chkPatientDied = New System.Windows.Forms.CheckBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtAdverseEvent = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblNote = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.chkPatientHasReaction = New System.Windows.Forms.CheckBox()
        Me.TabVIS = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.c1VIS = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.chk_vis_given = New System.Windows.Forms.CheckBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tblStrip = New gloToolStrip.gloToolStrip()
        Me.tblbtn_PrintVis = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabImmunization.SuspendLayout()
        Me.TabPageAdministratin.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabPageReaction.SuspendLayout()
        Me.grpReaction.SuspendLayout()
        Me.TabVIS.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.c1VIS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tblStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(27, 62)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(103, 14)
        Me.Label30.TabIndex = 8
        Me.Label30.Text = "Entered by :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAdministred
        '
        Me.cmbAdministred.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAdministred.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAdministred.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAdministred.FormattingEnabled = True
        Me.cmbAdministred.Location = New System.Drawing.Point(133, 58)
        Me.cmbAdministred.Name = "cmbAdministred"
        Me.cmbAdministred.Size = New System.Drawing.Size(119, 22)
        Me.cmbAdministred.TabIndex = 9
        '
        'dttransaction_date
        '
        Me.dttransaction_date.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dttransaction_date.CalendarMonthBackground = System.Drawing.Color.White
        Me.dttransaction_date.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dttransaction_date.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dttransaction_date.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dttransaction_date.CustomFormat = "MM/dd/yyyy  "
        Me.dttransaction_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dttransaction_date.Location = New System.Drawing.Point(133, 26)
        Me.dttransaction_date.Name = "dttransaction_date"
        Me.dttransaction_date.Size = New System.Drawing.Size(119, 22)
        Me.dttransaction_date.TabIndex = 3
        '
        'optRefused
        '
        Me.optRefused.AutoSize = True
        Me.optRefused.Location = New System.Drawing.Point(178, 44)
        Me.optRefused.Name = "optRefused"
        Me.optRefused.Size = New System.Drawing.Size(69, 18)
        Me.optRefused.TabIndex = 7
        Me.optRefused.TabStop = True
        Me.optRefused.Text = "Refused"
        Me.optRefused.UseVisualStyleBackColor = True
        '
        'optReported
        '
        Me.optReported.AutoSize = True
        Me.optReported.Location = New System.Drawing.Point(178, 18)
        Me.optReported.Name = "optReported"
        Me.optReported.Size = New System.Drawing.Size(76, 18)
        Me.optReported.TabIndex = 6
        Me.optReported.TabStop = True
        Me.optReported.Text = "Reported"
        Me.optReported.UseVisualStyleBackColor = True
        '
        'optAdministered
        '
        Me.optAdministered.AutoSize = True
        Me.optAdministered.Location = New System.Drawing.Point(23, 18)
        Me.optAdministered.Name = "optAdministered"
        Me.optAdministered.Size = New System.Drawing.Size(96, 18)
        Me.optAdministered.TabIndex = 5
        Me.optAdministered.TabStop = True
        Me.optAdministered.Text = "Administered"
        Me.optAdministered.UseVisualStyleBackColor = True
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(89, 30)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(41, 14)
        Me.Label43.TabIndex = 2
        Me.Label43.Text = "Date :"
        '
        'TabImmunization
        '
        Me.TabImmunization.Controls.Add(Me.TabPageAdministratin)
        Me.TabImmunization.Controls.Add(Me.TabPageReaction)
        Me.TabImmunization.Controls.Add(Me.TabVIS)
        Me.TabImmunization.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabImmunization.Location = New System.Drawing.Point(0, 54)
        Me.TabImmunization.Name = "TabImmunization"
        Me.TabImmunization.SelectedIndex = 0
        Me.TabImmunization.Size = New System.Drawing.Size(1079, 696)
        Me.TabImmunization.TabIndex = 0
        '
        'TabPageAdministratin
        '
        Me.TabPageAdministratin.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPageAdministratin.Controls.Add(Me.publicityeffetiveDTP)
        Me.TabPageAdministratin.Controls.Add(Me.Label57)
        Me.TabPageAdministratin.Controls.Add(Me.Label56)
        Me.TabPageAdministratin.Controls.Add(Me.cmbStatus)
        Me.TabPageAdministratin.Controls.Add(Me.btnclrcqm)
        Me.TabPageAdministratin.Controls.Add(Me.btnbrwcqm)
        Me.TabPageAdministratin.Controls.Add(Me.txtcqm)
        Me.TabPageAdministratin.Controls.Add(Me.lblcqm)
        Me.TabPageAdministratin.Controls.Add(Me.GroupBox1)
        Me.TabPageAdministratin.Controls.Add(Me.lblImmunizationFunding)
        Me.TabPageAdministratin.Controls.Add(Me.cmbImmunizationFunding)
        Me.TabPageAdministratin.Controls.Add(Me.btnClearRefusalReason)
        Me.TabPageAdministratin.Controls.Add(Me.btnBrwRefusalReason)
        Me.TabPageAdministratin.Controls.Add(Me.txtRefusalReason)
        Me.TabPageAdministratin.Controls.Add(Me.lblOrdProvider)
        Me.TabPageAdministratin.Controls.Add(Me.btnClrRefProvider)
        Me.TabPageAdministratin.Controls.Add(Me.btnRefProvider)
        Me.TabPageAdministratin.Controls.Add(Me.Label13)
        Me.TabPageAdministratin.Controls.Add(Me.BtnUncertainCVX)
        Me.TabPageAdministratin.Controls.Add(Me.dtTransactionTime)
        Me.TabPageAdministratin.Controls.Add(Me.btnAddPublicityCodeItem)
        Me.TabPageAdministratin.Controls.Add(Me.cmbPublicityCode)
        Me.TabPageAdministratin.Controls.Add(Me.lblPublicityCode)
        Me.TabPageAdministratin.Controls.Add(Me.lblSnomedIdValue)
        Me.TabPageAdministratin.Controls.Add(Me.btnClearSnomed)
        Me.TabPageAdministratin.Controls.Add(Me.btnBrowseSnomed)
        Me.TabPageAdministratin.Controls.Add(Me.lblSnomed)
        Me.TabPageAdministratin.Controls.Add(Me.btnAddUnitsOfMeasureItem)
        Me.TabPageAdministratin.Controls.Add(Me.Label2)
        Me.TabPageAdministratin.Controls.Add(Me.txt_units)
        Me.TabPageAdministratin.Controls.Add(Me.pnlTradeNameControl)
        Me.TabPageAdministratin.Controls.Add(Me.BtnAddVaccineCategory)
        Me.TabPageAdministratin.Controls.Add(Me.BtnAddManufacturerCategory)
        Me.TabPageAdministratin.Controls.Add(Me.Label16)
        Me.TabPageAdministratin.Controls.Add(Me.btnAddCategory)
        Me.TabPageAdministratin.Controls.Add(Me.BtnAddTradeNameCategory)
        Me.TabPageAdministratin.Controls.Add(Me.Label15)
        Me.TabPageAdministratin.Controls.Add(Me.Label5)
        Me.TabPageAdministratin.Controls.Add(Me.Label14)
        Me.TabPageAdministratin.Controls.Add(Me.Label32)
        Me.TabPageAdministratin.Controls.Add(Me.Label11)
        Me.TabPageAdministratin.Controls.Add(Me.Panel2)
        Me.TabPageAdministratin.Controls.Add(Me.txt_refused_by)
        Me.TabPageAdministratin.Controls.Add(Me.pnlMvxControl)
        Me.TabPageAdministratin.Controls.Add(Me.Label47)
        Me.TabPageAdministratin.Controls.Add(Me.pnlCvxControl)
        Me.TabPageAdministratin.Controls.Add(Me.cmbCategory)
        Me.TabPageAdministratin.Controls.Add(Me.cmbLocation)
        Me.TabPageAdministratin.Controls.Add(Me.cmbProvider)
        Me.TabPageAdministratin.Controls.Add(Me.dttransaction_date)
        Me.TabPageAdministratin.Controls.Add(Me.btnClearCPT)
        Me.TabPageAdministratin.Controls.Add(Me.Label30)
        Me.TabPageAdministratin.Controls.Add(Me.btnClearDiagnosis)
        Me.TabPageAdministratin.Controls.Add(Me.txt_notes)
        Me.TabPageAdministratin.Controls.Add(Me.Label4)
        Me.TabPageAdministratin.Controls.Add(Me.txtNDCcode)
        Me.TabPageAdministratin.Controls.Add(Me.Label9)
        Me.TabPageAdministratin.Controls.Add(Me.lblAdministeredTime)
        Me.TabPageAdministratin.Controls.Add(Me.Label43)
        Me.TabPageAdministratin.Controls.Add(Me.Label24)
        Me.TabPageAdministratin.Controls.Add(Me.Label27)
        Me.TabPageAdministratin.Controls.Add(Me.cmbAdministred)
        Me.TabPageAdministratin.Controls.Add(Me.btnBrowsCPT)
        Me.TabPageAdministratin.Controls.Add(Me.btnBrowsDiagnosis)
        Me.TabPageAdministratin.Controls.Add(Me.lblProviderName)
        Me.TabPageAdministratin.Controls.Add(Me.lblLocation)
        Me.TabPageAdministratin.Controls.Add(Me.Label31)
        Me.TabPageAdministratin.Controls.Add(Me.Label10)
        Me.TabPageAdministratin.Controls.Add(Me.cmbFunding)
        Me.TabPageAdministratin.Controls.Add(Me.cmbIcd)
        Me.TabPageAdministratin.Controls.Add(Me.Label41)
        Me.TabPageAdministratin.Controls.Add(Me.cmbCpt)
        Me.TabPageAdministratin.Controls.Add(Me.Label39)
        Me.TabPageAdministratin.Controls.Add(Me.Label33)
        Me.TabPageAdministratin.Controls.Add(Me.Label36)
        Me.TabPageAdministratin.Controls.Add(Me.Label38)
        Me.TabPageAdministratin.Controls.Add(Me.Label34)
        Me.TabPageAdministratin.Controls.Add(Me.dtDueDate)
        Me.TabPageAdministratin.Controls.Add(Me.Label29)
        Me.TabPageAdministratin.Controls.Add(Me.chkSetReminder)
        Me.TabPageAdministratin.Controls.Add(Me.dtexpDate)
        Me.TabPageAdministratin.Controls.Add(Me.Label25)
        Me.TabPageAdministratin.Controls.Add(Me.btnSearchsku)
        Me.TabPageAdministratin.Controls.Add(Me.lblFunding)
        Me.TabPageAdministratin.Controls.Add(Me.lblRefusedBy)
        Me.TabPageAdministratin.Controls.Add(Me.btnTradeName)
        Me.TabPageAdministratin.Controls.Add(Me.lblRefusalreason)
        Me.TabPageAdministratin.Controls.Add(Me.btnCvx)
        Me.TabPageAdministratin.Controls.Add(Me.cmbRefusalreason)
        Me.TabPageAdministratin.Controls.Add(Me.btnMvx)
        Me.TabPageAdministratin.Controls.Add(Me.txt_refusal_comments)
        Me.TabPageAdministratin.Controls.Add(Me.cmbSKU)
        Me.TabPageAdministratin.Controls.Add(Me.cmbLotNumber)
        Me.TabPageAdministratin.Controls.Add(Me.Label28)
        Me.TabPageAdministratin.Controls.Add(Me.lblTradeName)
        Me.TabPageAdministratin.Controls.Add(Me.Label35)
        Me.TabPageAdministratin.Controls.Add(Me.lblLotNumber)
        Me.TabPageAdministratin.Controls.Add(Me.Label40)
        Me.TabPageAdministratin.Controls.Add(Me.Label26)
        Me.TabPageAdministratin.Controls.Add(Me.txtMvx)
        Me.TabPageAdministratin.Controls.Add(Me.Label1)
        Me.TabPageAdministratin.Controls.Add(Me.txtCvx)
        Me.TabPageAdministratin.Controls.Add(Me.txt_TradeName)
        Me.TabPageAdministratin.Controls.Add(Me.Label67)
        Me.TabPageAdministratin.Controls.Add(Me.txt_vis)
        Me.TabPageAdministratin.Controls.Add(Me.cmbRoute)
        Me.TabPageAdministratin.Controls.Add(Me.dtpublication_date)
        Me.TabPageAdministratin.Controls.Add(Me.cmbSite)
        Me.TabPageAdministratin.Controls.Add(Me.txt_dosage_given)
        Me.TabPageAdministratin.Controls.Add(Me.Label37)
        Me.TabPageAdministratin.Controls.Add(Me.txt_amount_given)
        Me.TabPageAdministratin.Controls.Add(Me.Label23)
        Me.TabPageAdministratin.Controls.Add(Me.lblDosesOnHand)
        Me.TabPageAdministratin.Controls.Add(Me.Label44)
        Me.TabPageAdministratin.Controls.Add(Me.lblDosesGiven)
        Me.TabPageAdministratin.Controls.Add(Me.cmbUnitOfMeasure)
        Me.TabPageAdministratin.Controls.Add(Me.Label6)
        Me.TabPageAdministratin.Controls.Add(Me.Label8)
        Me.TabPageAdministratin.Location = New System.Drawing.Point(4, 23)
        Me.TabPageAdministratin.Name = "TabPageAdministratin"
        Me.TabPageAdministratin.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAdministratin.Size = New System.Drawing.Size(1071, 669)
        Me.TabPageAdministratin.TabIndex = 0
        Me.TabPageAdministratin.Text = "Administration"
        '
        'publicityeffetiveDTP
        '
        Me.publicityeffetiveDTP.CalendarForeColor = System.Drawing.Color.Maroon
        Me.publicityeffetiveDTP.CalendarMonthBackground = System.Drawing.Color.White
        Me.publicityeffetiveDTP.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.publicityeffetiveDTP.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.publicityeffetiveDTP.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.publicityeffetiveDTP.CustomFormat = "MM/dd/yyyy  "
        Me.publicityeffetiveDTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.publicityeffetiveDTP.Location = New System.Drawing.Point(655, 595)
        Me.publicityeffetiveDTP.Name = "publicityeffetiveDTP"
        Me.publicityeffetiveDTP.Size = New System.Drawing.Size(324, 22)
        Me.publicityeffetiveDTP.TabIndex = 379
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(510, 599)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(141, 14)
        Me.Label57.TabIndex = 378
        Me.Label57.Text = "Publicity Effective Date :"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(77, 599)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(54, 14)
        Me.Label56.TabIndex = 377
        Me.Label56.Text = " Status :"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(134, 595)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(324, 22)
        Me.cmbStatus.TabIndex = 376
        '
        'btnclrcqm
        '
        Me.btnclrcqm.BackColor = System.Drawing.Color.Transparent
        Me.btnclrcqm.BackgroundImage = CType(resources.GetObject("btnclrcqm.BackgroundImage"), System.Drawing.Image)
        Me.btnclrcqm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclrcqm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclrcqm.Enabled = False
        Me.btnclrcqm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnclrcqm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclrcqm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclrcqm.Image = CType(resources.GetObject("btnclrcqm.Image"), System.Drawing.Image)
        Me.btnclrcqm.Location = New System.Drawing.Point(492, 343)
        Me.btnclrcqm.Name = "btnclrcqm"
        Me.btnclrcqm.Size = New System.Drawing.Size(23, 23)
        Me.btnclrcqm.TabIndex = 375
        Me.btnclrcqm.UseVisualStyleBackColor = False
        '
        'btnbrwcqm
        '
        Me.btnbrwcqm.BackgroundImage = CType(resources.GetObject("btnbrwcqm.BackgroundImage"), System.Drawing.Image)
        Me.btnbrwcqm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbrwcqm.Enabled = False
        Me.btnbrwcqm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrwcqm.Image = CType(resources.GetObject("btnbrwcqm.Image"), System.Drawing.Image)
        Me.btnbrwcqm.Location = New System.Drawing.Point(463, 344)
        Me.btnbrwcqm.Name = "btnbrwcqm"
        Me.btnbrwcqm.Size = New System.Drawing.Size(23, 23)
        Me.btnbrwcqm.TabIndex = 374
        Me.ToolTip2.SetToolTip(Me.btnbrwcqm, "Select Refusal Code")
        Me.btnbrwcqm.UseVisualStyleBackColor = True
        '
        'txtcqm
        '
        Me.txtcqm.Enabled = False
        Me.txtcqm.Location = New System.Drawing.Point(133, 345)
        Me.txtcqm.MaxLength = 1000
        Me.txtcqm.Name = "txtcqm"
        Me.txtcqm.Size = New System.Drawing.Size(324, 22)
        Me.txtcqm.TabIndex = 373
        '
        'lblcqm
        '
        Me.lblcqm.AutoSize = True
        Me.lblcqm.Location = New System.Drawing.Point(30, 349)
        Me.lblcqm.Name = "lblcqm"
        Me.lblcqm.Size = New System.Drawing.Size(101, 14)
        Me.lblcqm.TabIndex = 372
        Me.lblcqm.Text = "CQM Categories :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optPartiallyAdministered)
        Me.GroupBox1.Controls.Add(Me.optReported)
        Me.GroupBox1.Controls.Add(Me.optNotAdministered)
        Me.GroupBox1.Controls.Add(Me.optRefused)
        Me.GroupBox1.Controls.Add(Me.optAdministered)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(599, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(451, 69)
        Me.GroupBox1.TabIndex = 371
        Me.GroupBox1.TabStop = False
        '
        'optPartiallyAdministered
        '
        Me.optPartiallyAdministered.AutoSize = True
        Me.optPartiallyAdministered.Location = New System.Drawing.Point(291, 18)
        Me.optPartiallyAdministered.Name = "optPartiallyAdministered"
        Me.optPartiallyAdministered.Size = New System.Drawing.Size(140, 18)
        Me.optPartiallyAdministered.TabIndex = 370
        Me.optPartiallyAdministered.TabStop = True
        Me.optPartiallyAdministered.Text = "Partially Administered"
        Me.optPartiallyAdministered.UseVisualStyleBackColor = True
        '
        'optNotAdministered
        '
        Me.optNotAdministered.AutoSize = True
        Me.optNotAdministered.Location = New System.Drawing.Point(23, 44)
        Me.optNotAdministered.Name = "optNotAdministered"
        Me.optNotAdministered.Size = New System.Drawing.Size(120, 18)
        Me.optNotAdministered.TabIndex = 369
        Me.optNotAdministered.TabStop = True
        Me.optNotAdministered.Text = "Not Administered"
        Me.optNotAdministered.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(6, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 14)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "*"
        '
        'lblImmunizationFunding
        '
        Me.lblImmunizationFunding.AutoSize = True
        Me.lblImmunizationFunding.Location = New System.Drawing.Point(31, 566)
        Me.lblImmunizationFunding.Name = "lblImmunizationFunding"
        Me.lblImmunizationFunding.Size = New System.Drawing.Size(100, 14)
        Me.lblImmunizationFunding.TabIndex = 368
        Me.lblImmunizationFunding.Text = "Funding Source :"
        '
        'cmbImmunizationFunding
        '
        Me.cmbImmunizationFunding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbImmunizationFunding.FormattingEnabled = True
        Me.cmbImmunizationFunding.Location = New System.Drawing.Point(134, 562)
        Me.cmbImmunizationFunding.Name = "cmbImmunizationFunding"
        Me.cmbImmunizationFunding.Size = New System.Drawing.Size(324, 22)
        Me.cmbImmunizationFunding.TabIndex = 367
        '
        'btnClearRefusalReason
        '
        Me.btnClearRefusalReason.BackColor = System.Drawing.Color.Transparent
        Me.btnClearRefusalReason.BackgroundImage = CType(resources.GetObject("btnClearRefusalReason.BackgroundImage"), System.Drawing.Image)
        Me.btnClearRefusalReason.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearRefusalReason.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearRefusalReason.Enabled = False
        Me.btnClearRefusalReason.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearRefusalReason.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearRefusalReason.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearRefusalReason.Image = CType(resources.GetObject("btnClearRefusalReason.Image"), System.Drawing.Image)
        Me.btnClearRefusalReason.Location = New System.Drawing.Point(488, 282)
        Me.btnClearRefusalReason.Name = "btnClearRefusalReason"
        Me.btnClearRefusalReason.Size = New System.Drawing.Size(23, 23)
        Me.btnClearRefusalReason.TabIndex = 366
        Me.btnClearRefusalReason.UseVisualStyleBackColor = False
        '
        'btnBrwRefusalReason
        '
        Me.btnBrwRefusalReason.BackgroundImage = CType(resources.GetObject("btnBrwRefusalReason.BackgroundImage"), System.Drawing.Image)
        Me.btnBrwRefusalReason.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrwRefusalReason.Enabled = False
        Me.btnBrwRefusalReason.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrwRefusalReason.Image = CType(resources.GetObject("btnBrwRefusalReason.Image"), System.Drawing.Image)
        Me.btnBrwRefusalReason.Location = New System.Drawing.Point(461, 282)
        Me.btnBrwRefusalReason.Name = "btnBrwRefusalReason"
        Me.btnBrwRefusalReason.Size = New System.Drawing.Size(23, 23)
        Me.btnBrwRefusalReason.TabIndex = 365
        Me.ToolTip2.SetToolTip(Me.btnBrwRefusalReason, "Select Refusal Code")
        Me.btnBrwRefusalReason.UseVisualStyleBackColor = True
        '
        'txtRefusalReason
        '
        Me.txtRefusalReason.Enabled = False
        Me.txtRefusalReason.Location = New System.Drawing.Point(133, 282)
        Me.txtRefusalReason.MaxLength = 1000
        Me.txtRefusalReason.Name = "txtRefusalReason"
        Me.txtRefusalReason.Size = New System.Drawing.Size(324, 22)
        Me.txtRefusalReason.TabIndex = 364
        '
        'lblOrdProvider
        '
        Me.lblOrdProvider.AutoEllipsis = True
        Me.lblOrdProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lblOrdProvider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrdProvider.Location = New System.Drawing.Point(655, 122)
        Me.lblOrdProvider.Name = "lblOrdProvider"
        Me.lblOrdProvider.Size = New System.Drawing.Size(324, 22)
        Me.lblOrdProvider.TabIndex = 363
        Me.lblOrdProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClrRefProvider
        '
        Me.btnClrRefProvider.BackColor = System.Drawing.Color.Transparent
        Me.btnClrRefProvider.BackgroundImage = CType(resources.GetObject("btnClrRefProvider.BackgroundImage"), System.Drawing.Image)
        Me.btnClrRefProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClrRefProvider.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClrRefProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClrRefProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClrRefProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClrRefProvider.Image = CType(resources.GetObject("btnClrRefProvider.Image"), System.Drawing.Image)
        Me.btnClrRefProvider.Location = New System.Drawing.Point(1009, 122)
        Me.btnClrRefProvider.Name = "btnClrRefProvider"
        Me.btnClrRefProvider.Size = New System.Drawing.Size(23, 23)
        Me.btnClrRefProvider.TabIndex = 360
        Me.btnClrRefProvider.UseVisualStyleBackColor = False
        '
        'btnRefProvider
        '
        Me.btnRefProvider.BackgroundImage = CType(resources.GetObject("btnRefProvider.BackgroundImage"), System.Drawing.Image)
        Me.btnRefProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefProvider.Image = CType(resources.GetObject("btnRefProvider.Image"), System.Drawing.Image)
        Me.btnRefProvider.Location = New System.Drawing.Point(983, 122)
        Me.btnRefProvider.Name = "btnRefProvider"
        Me.btnRefProvider.Size = New System.Drawing.Size(23, 23)
        Me.btnRefProvider.TabIndex = 359
        Me.btnRefProvider.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(541, 126)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(110, 14)
        Me.Label13.TabIndex = 357
        Me.Label13.Text = "Ordering Provider :"
        '
        'BtnUncertainCVX
        '
        Me.BtnUncertainCVX.BackColor = System.Drawing.Color.Transparent
        Me.BtnUncertainCVX.BackgroundImage = CType(resources.GetObject("BtnUncertainCVX.BackgroundImage"), System.Drawing.Image)
        Me.BtnUncertainCVX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnUncertainCVX.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnUncertainCVX.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnUncertainCVX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUncertainCVX.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUncertainCVX.Image = CType(resources.GetObject("BtnUncertainCVX.Image"), System.Drawing.Image)
        Me.BtnUncertainCVX.Location = New System.Drawing.Point(983, 344)
        Me.BtnUncertainCVX.Name = "BtnUncertainCVX"
        Me.BtnUncertainCVX.Size = New System.Drawing.Size(23, 23)
        Me.BtnUncertainCVX.TabIndex = 342
        Me.BtnUncertainCVX.TabStop = False
        Me.BtnUncertainCVX.Text = "          "
        Me.BtnUncertainCVX.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip2.SetToolTip(Me.BtnUncertainCVX, "Add Uncertain Formulation Vaccination")
        Me.BtnUncertainCVX.UseVisualStyleBackColor = False
        '
        'dtTransactionTime
        '
        Me.dtTransactionTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTransactionTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTransactionTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTransactionTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTransactionTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTransactionTime.CustomFormat = "hh:mm:tt"
        Me.dtTransactionTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTransactionTime.Location = New System.Drawing.Point(338, 26)
        Me.dtTransactionTime.Name = "dtTransactionTime"
        Me.dtTransactionTime.ShowCheckBox = True
        Me.dtTransactionTime.ShowUpDown = True
        Me.dtTransactionTime.Size = New System.Drawing.Size(119, 22)
        Me.dtTransactionTime.TabIndex = 341
        '
        'btnAddPublicityCodeItem
        '
        Me.btnAddPublicityCodeItem.BackColor = System.Drawing.Color.Transparent
        Me.btnAddPublicityCodeItem.BackgroundImage = CType(resources.GetObject("btnAddPublicityCodeItem.BackgroundImage"), System.Drawing.Image)
        Me.btnAddPublicityCodeItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddPublicityCodeItem.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddPublicityCodeItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAddPublicityCodeItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddPublicityCodeItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddPublicityCodeItem.Image = CType(resources.GetObject("btnAddPublicityCodeItem.Image"), System.Drawing.Image)
        Me.btnAddPublicityCodeItem.Location = New System.Drawing.Point(983, 560)
        Me.btnAddPublicityCodeItem.Name = "btnAddPublicityCodeItem"
        Me.btnAddPublicityCodeItem.Size = New System.Drawing.Size(23, 23)
        Me.btnAddPublicityCodeItem.TabIndex = 338
        Me.btnAddPublicityCodeItem.TabStop = False
        Me.btnAddPublicityCodeItem.Text = "          "
        Me.btnAddPublicityCodeItem.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip2.SetToolTip(Me.btnAddPublicityCodeItem, "Add Publicity Code")
        Me.btnAddPublicityCodeItem.UseVisualStyleBackColor = False
        '
        'cmbPublicityCode
        '
        Me.cmbPublicityCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPublicityCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPublicityCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPublicityCode.FormattingEnabled = True
        Me.cmbPublicityCode.Location = New System.Drawing.Point(655, 560)
        Me.cmbPublicityCode.MaxLength = 1000
        Me.cmbPublicityCode.Name = "cmbPublicityCode"
        Me.cmbPublicityCode.Size = New System.Drawing.Size(324, 22)
        Me.cmbPublicityCode.TabIndex = 337
        '
        'lblPublicityCode
        '
        Me.lblPublicityCode.AutoSize = True
        Me.lblPublicityCode.Location = New System.Drawing.Point(560, 564)
        Me.lblPublicityCode.Name = "lblPublicityCode"
        Me.lblPublicityCode.Size = New System.Drawing.Size(91, 14)
        Me.lblPublicityCode.TabIndex = 336
        Me.lblPublicityCode.Text = "Publicity Code :"
        '
        'lblSnomedIdValue
        '
        Me.lblSnomedIdValue.AutoEllipsis = True
        Me.lblSnomedIdValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSnomedIdValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSnomedIdValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSnomedIdValue.Location = New System.Drawing.Point(655, 528)
        Me.lblSnomedIdValue.Name = "lblSnomedIdValue"
        Me.lblSnomedIdValue.Size = New System.Drawing.Size(324, 20)
        Me.lblSnomedIdValue.TabIndex = 335
        Me.lblSnomedIdValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClearSnomed
        '
        Me.btnClearSnomed.BackColor = System.Drawing.Color.Transparent
        Me.btnClearSnomed.BackgroundImage = CType(resources.GetObject("btnClearSnomed.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSnomed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSnomed.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSnomed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearSnomed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSnomed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearSnomed.Image = CType(resources.GetObject("btnClearSnomed.Image"), System.Drawing.Image)
        Me.btnClearSnomed.Location = New System.Drawing.Point(1009, 527)
        Me.btnClearSnomed.Name = "btnClearSnomed"
        Me.btnClearSnomed.Size = New System.Drawing.Size(23, 23)
        Me.btnClearSnomed.TabIndex = 334
        Me.ToolTip2.SetToolTip(Me.btnClearSnomed, "Clear selected Snomed ID")
        Me.btnClearSnomed.UseVisualStyleBackColor = False
        '
        'btnBrowseSnomed
        '
        Me.btnBrowseSnomed.BackgroundImage = CType(resources.GetObject("btnBrowseSnomed.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseSnomed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseSnomed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseSnomed.Image = CType(resources.GetObject("btnBrowseSnomed.Image"), System.Drawing.Image)
        Me.btnBrowseSnomed.Location = New System.Drawing.Point(983, 527)
        Me.btnBrowseSnomed.Name = "btnBrowseSnomed"
        Me.btnBrowseSnomed.Size = New System.Drawing.Size(23, 23)
        Me.btnBrowseSnomed.TabIndex = 333
        Me.ToolTip2.SetToolTip(Me.btnBrowseSnomed, "Select Snomed ID")
        Me.btnBrowseSnomed.UseVisualStyleBackColor = True
        '
        'lblSnomed
        '
        Me.lblSnomed.AutoSize = True
        Me.lblSnomed.Location = New System.Drawing.Point(575, 531)
        Me.lblSnomed.Name = "lblSnomed"
        Me.lblSnomed.Size = New System.Drawing.Size(76, 14)
        Me.lblSnomed.TabIndex = 331
        Me.lblSnomed.Text = "Snomed ID :"
        '
        'btnAddUnitsOfMeasureItem
        '
        Me.btnAddUnitsOfMeasureItem.BackColor = System.Drawing.Color.Transparent
        Me.btnAddUnitsOfMeasureItem.BackgroundImage = CType(resources.GetObject("btnAddUnitsOfMeasureItem.BackgroundImage"), System.Drawing.Image)
        Me.btnAddUnitsOfMeasureItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddUnitsOfMeasureItem.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddUnitsOfMeasureItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAddUnitsOfMeasureItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddUnitsOfMeasureItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddUnitsOfMeasureItem.Image = CType(resources.GetObject("btnAddUnitsOfMeasureItem.Image"), System.Drawing.Image)
        Me.btnAddUnitsOfMeasureItem.Location = New System.Drawing.Point(983, 282)
        Me.btnAddUnitsOfMeasureItem.Name = "btnAddUnitsOfMeasureItem"
        Me.btnAddUnitsOfMeasureItem.Size = New System.Drawing.Size(23, 23)
        Me.btnAddUnitsOfMeasureItem.TabIndex = 330
        Me.btnAddUnitsOfMeasureItem.TabStop = False
        Me.btnAddUnitsOfMeasureItem.Text = "          "
        Me.btnAddUnitsOfMeasureItem.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip2.SetToolTip(Me.btnAddUnitsOfMeasureItem, "Add Unit Of Measure Code")
        Me.btnAddUnitsOfMeasureItem.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(343, 684)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Units :"
        Me.Label2.Visible = False
        '
        'txt_units
        '
        Me.txt_units.Location = New System.Drawing.Point(388, 680)
        Me.txt_units.MaxLength = 12
        Me.txt_units.Name = "txt_units"
        Me.txt_units.Size = New System.Drawing.Size(97, 22)
        Me.txt_units.TabIndex = 47
        Me.txt_units.Visible = False
        '
        'pnlTradeNameControl
        '
        Me.pnlTradeNameControl.Location = New System.Drawing.Point(133, 144)
        Me.pnlTradeNameControl.Name = "pnlTradeNameControl"
        Me.pnlTradeNameControl.Size = New System.Drawing.Size(324, 132)
        Me.pnlTradeNameControl.TabIndex = 326
        Me.pnlTradeNameControl.Visible = False
        '
        'BtnAddVaccineCategory
        '
        Me.BtnAddVaccineCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddVaccineCategory.BackgroundImage = CType(resources.GetObject("BtnAddVaccineCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddVaccineCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddVaccineCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddVaccineCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddVaccineCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddVaccineCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddVaccineCategory.Image = CType(resources.GetObject("BtnAddVaccineCategory.Image"), System.Drawing.Image)
        Me.BtnAddVaccineCategory.Location = New System.Drawing.Point(1009, 152)
        Me.BtnAddVaccineCategory.Name = "BtnAddVaccineCategory"
        Me.BtnAddVaccineCategory.Size = New System.Drawing.Size(23, 23)
        Me.BtnAddVaccineCategory.TabIndex = 23
        Me.BtnAddVaccineCategory.TabStop = False
        Me.BtnAddVaccineCategory.Text = "          "
        Me.ToolTip2.SetToolTip(Me.BtnAddVaccineCategory, "Add Vaccine")
        Me.BtnAddVaccineCategory.UseVisualStyleBackColor = False
        '
        'BtnAddManufacturerCategory
        '
        Me.BtnAddManufacturerCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddManufacturerCategory.BackgroundImage = CType(resources.GetObject("BtnAddManufacturerCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddManufacturerCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddManufacturerCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddManufacturerCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddManufacturerCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddManufacturerCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddManufacturerCategory.Image = CType(resources.GetObject("BtnAddManufacturerCategory.Image"), System.Drawing.Image)
        Me.BtnAddManufacturerCategory.Location = New System.Drawing.Point(1009, 186)
        Me.BtnAddManufacturerCategory.Name = "BtnAddManufacturerCategory"
        Me.BtnAddManufacturerCategory.Size = New System.Drawing.Size(23, 23)
        Me.BtnAddManufacturerCategory.TabIndex = 32
        Me.BtnAddManufacturerCategory.TabStop = False
        Me.BtnAddManufacturerCategory.Text = "          "
        Me.ToolTip2.SetToolTip(Me.BtnAddManufacturerCategory, "Add Manufacturer")
        Me.BtnAddManufacturerCategory.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Location = New System.Drawing.Point(1067, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 661)
        Me.Label16.TabIndex = 17
        '
        'btnAddCategory
        '
        Me.btnAddCategory.BackColor = System.Drawing.Color.Transparent
        Me.btnAddCategory.BackgroundImage = CType(resources.GetObject("btnAddCategory.BackgroundImage"), System.Drawing.Image)
        Me.btnAddCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAddCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddCategory.Image = CType(resources.GetObject("btnAddCategory.Image"), System.Drawing.Image)
        Me.btnAddCategory.Location = New System.Drawing.Point(461, 186)
        Me.btnAddCategory.Name = "btnAddCategory"
        Me.btnAddCategory.Size = New System.Drawing.Size(23, 23)
        Me.btnAddCategory.TabIndex = 28
        Me.btnAddCategory.TabStop = False
        Me.btnAddCategory.Text = "          "
        Me.btnAddCategory.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip2.SetToolTip(Me.btnAddCategory, "Add Immunization Inventory Category")
        Me.btnAddCategory.UseVisualStyleBackColor = False
        '
        'BtnAddTradeNameCategory
        '
        Me.BtnAddTradeNameCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddTradeNameCategory.BackgroundImage = CType(resources.GetObject("BtnAddTradeNameCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddTradeNameCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddTradeNameCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddTradeNameCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddTradeNameCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddTradeNameCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddTradeNameCategory.Image = CType(resources.GetObject("BtnAddTradeNameCategory.Image"), System.Drawing.Image)
        Me.BtnAddTradeNameCategory.Location = New System.Drawing.Point(487, 122)
        Me.BtnAddTradeNameCategory.Name = "BtnAddTradeNameCategory"
        Me.BtnAddTradeNameCategory.Size = New System.Drawing.Size(23, 23)
        Me.BtnAddTradeNameCategory.TabIndex = 28
        Me.BtnAddTradeNameCategory.TabStop = False
        Me.BtnAddTradeNameCategory.Text = "          "
        Me.BtnAddTradeNameCategory.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip2.SetToolTip(Me.BtnAddTradeNameCategory, "Add Trade Name")
        Me.BtnAddTradeNameCategory.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Location = New System.Drawing.Point(3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 661)
        Me.Label15.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(581, 156)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "*"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Location = New System.Drawing.Point(3, 665)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1065, 1)
        Me.Label14.TabIndex = 8
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(541, 348)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(110, 14)
        Me.Label32.TabIndex = 55
        Me.Label32.Text = "Observation Date :"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Location = New System.Drawing.Point(3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1065, 1)
        Me.Label11.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnView)
        Me.Panel2.Controls.Add(Me.btnScan)
        Me.Panel2.Location = New System.Drawing.Point(397, 680)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(61, 24)
        Me.Panel2.TabIndex = 27
        Me.Panel2.Visible = False
        '
        'btnView
        '
        Me.btnView.BackgroundImage = CType(resources.GetObject("btnView.BackgroundImage"), System.Drawing.Image)
        Me.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnView.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.Image = CType(resources.GetObject("btnView.Image"), System.Drawing.Image)
        Me.btnView.Location = New System.Drawing.Point(24, 0)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(24, 24)
        Me.btnView.TabIndex = 1
        Me.btnView.UseVisualStyleBackColor = True
        Me.btnView.Visible = False
        '
        'btnScan
        '
        Me.btnScan.BackgroundImage = CType(resources.GetObject("btnScan.BackgroundImage"), System.Drawing.Image)
        Me.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnScan.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScan.Image = CType(resources.GetObject("btnScan.Image"), System.Drawing.Image)
        Me.btnScan.Location = New System.Drawing.Point(0, 0)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(24, 24)
        Me.btnScan.TabIndex = 0
        Me.btnScan.UseVisualStyleBackColor = True
        Me.btnScan.Visible = False
        '
        'txt_refused_by
        '
        Me.txt_refused_by.Location = New System.Drawing.Point(133, 314)
        Me.txt_refused_by.MaxLength = 50
        Me.txt_refused_by.Name = "txt_refused_by"
        Me.txt_refused_by.Size = New System.Drawing.Size(324, 22)
        Me.txt_refused_by.TabIndex = 64
        '
        'pnlMvxControl
        '
        Me.pnlMvxControl.Location = New System.Drawing.Point(655, 209)
        Me.pnlMvxControl.Name = "pnlMvxControl"
        Me.pnlMvxControl.Size = New System.Drawing.Size(324, 129)
        Me.pnlMvxControl.TabIndex = 328
        Me.pnlMvxControl.Visible = False
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(515, 94)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(136, 14)
        Me.Label47.TabIndex = 14
        Me.Label47.Text = "Administering Provider :"
        '
        'pnlCvxControl
        '
        Me.pnlCvxControl.Location = New System.Drawing.Point(655, 174)
        Me.pnlCvxControl.Name = "pnlCvxControl"
        Me.pnlCvxControl.Size = New System.Drawing.Size(324, 131)
        Me.pnlCvxControl.TabIndex = 31
        Me.pnlCvxControl.Visible = False
        '
        'cmbCategory
        '
        Me.cmbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(133, 186)
        Me.cmbCategory.MaxLength = 1000
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(324, 22)
        Me.cmbCategory.TabIndex = 34
        '
        'cmbLocation
        '
        Me.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(338, 58)
        Me.cmbLocation.MaxLength = 1000
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(119, 22)
        Me.cmbLocation.TabIndex = 12
        '
        'cmbProvider
        '
        Me.cmbProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(655, 90)
        Me.cmbProvider.MaxLength = 1000
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(324, 22)
        Me.cmbProvider.TabIndex = 15
        '
        'btnClearCPT
        '
        Me.btnClearCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.BackgroundImage = CType(resources.GetObject("btnClearCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCPT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearCPT.Image = CType(resources.GetObject("btnClearCPT.Image"), System.Drawing.Image)
        Me.btnClearCPT.Location = New System.Drawing.Point(1009, 468)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(23, 23)
        Me.btnClearCPT.TabIndex = 75
        Me.btnClearCPT.UseVisualStyleBackColor = False
        '
        'btnClearDiagnosis
        '
        Me.btnClearDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.btnClearDiagnosis.BackgroundImage = CType(resources.GetObject("btnClearDiagnosis.BackgroundImage"), System.Drawing.Image)
        Me.btnClearDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearDiagnosis.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearDiagnosis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearDiagnosis.Image = CType(resources.GetObject("btnClearDiagnosis.Image"), System.Drawing.Image)
        Me.btnClearDiagnosis.Location = New System.Drawing.Point(487, 407)
        Me.btnClearDiagnosis.Name = "btnClearDiagnosis"
        Me.btnClearDiagnosis.Size = New System.Drawing.Size(23, 23)
        Me.btnClearDiagnosis.TabIndex = 71
        Me.btnClearDiagnosis.UseVisualStyleBackColor = False
        '
        'txt_notes
        '
        Me.txt_notes.Location = New System.Drawing.Point(134, 470)
        Me.txt_notes.MaxLength = 1000
        Me.txt_notes.Multiline = True
        Me.txt_notes.Name = "txt_notes"
        Me.txt_notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_notes.Size = New System.Drawing.Size(324, 80)
        Me.txt_notes.TabIndex = 81
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(66, 190)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Category :"
        '
        'txtNDCcode
        '
        Me.txtNDCcode.Location = New System.Drawing.Point(134, 435)
        Me.txtNDCcode.MaxLength = 11
        Me.txtNDCcode.Name = "txtNDCcode"
        Me.txtNDCcode.Size = New System.Drawing.Size(324, 22)
        Me.txtNDCcode.TabIndex = 77
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(274, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 14)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Location :"
        '
        'lblAdministeredTime
        '
        Me.lblAdministeredTime.AutoSize = True
        Me.lblAdministeredTime.Location = New System.Drawing.Point(293, 30)
        Me.lblAdministeredTime.Name = "lblAdministeredTime"
        Me.lblAdministeredTime.Size = New System.Drawing.Size(42, 14)
        Me.lblAdministeredTime.TabIndex = 2
        Me.lblAdministeredTime.Text = "Time :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(58, 473)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(73, 14)
        Me.Label24.TabIndex = 80
        Me.Label24.Text = "Comments :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(561, 503)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(90, 14)
        Me.Label27.TabIndex = 78
        Me.Label27.Text = "Funding Type :"
        '
        'btnBrowsCPT
        '
        Me.btnBrowsCPT.BackgroundImage = CType(resources.GetObject("btnBrowsCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsCPT.Image = CType(resources.GetObject("btnBrowsCPT.Image"), System.Drawing.Image)
        Me.btnBrowsCPT.Location = New System.Drawing.Point(983, 468)
        Me.btnBrowsCPT.Name = "btnBrowsCPT"
        Me.btnBrowsCPT.Size = New System.Drawing.Size(23, 23)
        Me.btnBrowsCPT.TabIndex = 74
        Me.ToolTip2.SetToolTip(Me.btnBrowsCPT, "Select CPT")
        Me.btnBrowsCPT.UseVisualStyleBackColor = True
        '
        'btnBrowsDiagnosis
        '
        Me.btnBrowsDiagnosis.BackgroundImage = CType(resources.GetObject("btnBrowsDiagnosis.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsDiagnosis.Image = CType(resources.GetObject("btnBrowsDiagnosis.Image"), System.Drawing.Image)
        Me.btnBrowsDiagnosis.Location = New System.Drawing.Point(461, 407)
        Me.btnBrowsDiagnosis.Name = "btnBrowsDiagnosis"
        Me.btnBrowsDiagnosis.Size = New System.Drawing.Size(23, 23)
        Me.btnBrowsDiagnosis.TabIndex = 70
        Me.ToolTip2.SetToolTip(Me.btnBrowsDiagnosis, "Select Diagnosis")
        Me.btnBrowsDiagnosis.UseVisualStyleBackColor = True
        '
        'lblProviderName
        '
        Me.lblProviderName.AutoSize = True
        Me.lblProviderName.ForeColor = System.Drawing.Color.Red
        Me.lblProviderName.Location = New System.Drawing.Point(502, 94)
        Me.lblProviderName.Name = "lblProviderName"
        Me.lblProviderName.Size = New System.Drawing.Size(14, 14)
        Me.lblProviderName.TabIndex = 13
        Me.lblProviderName.Text = "*"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.ForeColor = System.Drawing.Color.Red
        Me.lblLocation.Location = New System.Drawing.Point(261, 62)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(14, 14)
        Me.lblLocation.TabIndex = 10
        Me.lblLocation.Text = "*"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(66, 411)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(64, 14)
        Me.Label31.TabIndex = 68
        Me.Label31.Text = "Diagnosis :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(71, 30)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(14, 14)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "*"
        '
        'cmbFunding
        '
        Me.cmbFunding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFunding.FormattingEnabled = True
        Me.cmbFunding.Location = New System.Drawing.Point(655, 499)
        Me.cmbFunding.Name = "cmbFunding"
        Me.cmbFunding.Size = New System.Drawing.Size(324, 22)
        Me.cmbFunding.TabIndex = 79
        '
        'cmbIcd
        '
        Me.cmbIcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIcd.FormattingEnabled = True
        Me.cmbIcd.Location = New System.Drawing.Point(133, 407)
        Me.cmbIcd.MaxLength = 1000
        Me.cmbIcd.Name = "cmbIcd"
        Me.cmbIcd.Size = New System.Drawing.Size(324, 22)
        Me.cmbIcd.TabIndex = 69
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(93, 94)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(37, 14)
        Me.Label41.TabIndex = 16
        Me.Label41.Text = "SKU :"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCpt
        '
        Me.cmbCpt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCpt.FormattingEnabled = True
        Me.cmbCpt.Location = New System.Drawing.Point(655, 468)
        Me.cmbCpt.MaxLength = 1000
        Me.cmbCpt.Name = "cmbCpt"
        Me.cmbCpt.Size = New System.Drawing.Size(324, 22)
        Me.cmbCpt.TabIndex = 73
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(565, 190)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(87, 14)
        Me.Label39.TabIndex = 29
        Me.Label39.Text = "Manufacturer :"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(93, 439)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(38, 14)
        Me.Label33.TabIndex = 76
        Me.Label33.Text = "NDC :"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(594, 156)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(57, 14)
        Me.Label36.TabIndex = 20
        Me.Label36.Text = "Vaccine :"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(614, 472)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(37, 14)
        Me.Label38.TabIndex = 72
        Me.Label38.Text = "CPT :"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(50, 156)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(80, 14)
        Me.Label34.TabIndex = 36
        Me.Label34.Text = "Lot Number :"
        '
        'dtDueDate
        '
        Me.dtDueDate.Checked = False
        Me.dtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDueDate.Location = New System.Drawing.Point(655, 438)
        Me.dtDueDate.Name = "dtDueDate"
        Me.dtDueDate.ShowCheckBox = True
        Me.dtDueDate.Size = New System.Drawing.Size(324, 22)
        Me.dtDueDate.TabIndex = 67
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(48, 126)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(82, 14)
        Me.Label29.TabIndex = 25
        Me.Label29.Text = "Trade Name :"
        '
        'chkSetReminder
        '
        Me.chkSetReminder.AutoSize = True
        Me.chkSetReminder.Location = New System.Drawing.Point(134, 376)
        Me.chkSetReminder.Name = "chkSetReminder"
        Me.chkSetReminder.Size = New System.Drawing.Size(108, 18)
        Me.chkSetReminder.TabIndex = 65
        Me.chkSetReminder.Text = "Set Reminder  "
        Me.chkSetReminder.UseVisualStyleBackColor = True
        '
        'dtexpDate
        '
        Me.dtexpDate.Checked = False
        Me.dtexpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtexpDate.Location = New System.Drawing.Point(655, 218)
        Me.dtexpDate.Name = "dtexpDate"
        Me.dtexpDate.ShowCheckBox = True
        Me.dtexpDate.Size = New System.Drawing.Size(324, 22)
        Me.dtexpDate.TabIndex = 39
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(584, 443)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(67, 14)
        Me.Label25.TabIndex = 66
        Me.Label25.Text = "Due Date :"
        '
        'btnSearchsku
        '
        Me.btnSearchsku.BackgroundImage = CType(resources.GetObject("btnSearchsku.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchsku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchsku.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchsku.Image = CType(resources.GetObject("btnSearchsku.Image"), System.Drawing.Image)
        Me.btnSearchsku.Location = New System.Drawing.Point(461, 90)
        Me.btnSearchsku.Name = "btnSearchsku"
        Me.btnSearchsku.Size = New System.Drawing.Size(23, 23)
        Me.btnSearchsku.TabIndex = 18
        Me.ToolTip2.SetToolTip(Me.btnSearchsku, "Select SKU")
        Me.btnSearchsku.UseVisualStyleBackColor = True
        '
        'lblFunding
        '
        Me.lblFunding.AutoSize = True
        Me.lblFunding.ForeColor = System.Drawing.Color.Red
        Me.lblFunding.Location = New System.Drawing.Point(593, 472)
        Me.lblFunding.Name = "lblFunding"
        Me.lblFunding.Size = New System.Drawing.Size(14, 14)
        Me.lblFunding.TabIndex = 62
        Me.lblFunding.Text = "*"
        '
        'lblRefusedBy
        '
        Me.lblRefusedBy.AutoSize = True
        Me.lblRefusedBy.ForeColor = System.Drawing.Color.Red
        Me.lblRefusedBy.Location = New System.Drawing.Point(42, 318)
        Me.lblRefusedBy.Name = "lblRefusedBy"
        Me.lblRefusedBy.Size = New System.Drawing.Size(14, 14)
        Me.lblRefusedBy.TabIndex = 62
        Me.lblRefusedBy.Text = "*"
        '
        'btnTradeName
        '
        Me.btnTradeName.BackgroundImage = CType(resources.GetObject("btnTradeName.BackgroundImage"), System.Drawing.Image)
        Me.btnTradeName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTradeName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTradeName.Image = CType(resources.GetObject("btnTradeName.Image"), System.Drawing.Image)
        Me.btnTradeName.Location = New System.Drawing.Point(461, 122)
        Me.btnTradeName.Name = "btnTradeName"
        Me.btnTradeName.Size = New System.Drawing.Size(23, 23)
        Me.btnTradeName.TabIndex = 27
        Me.ToolTip2.SetToolTip(Me.btnTradeName, "Select Trade Name")
        Me.btnTradeName.UseVisualStyleBackColor = True
        '
        'lblRefusalreason
        '
        Me.lblRefusalreason.AutoSize = True
        Me.lblRefusalreason.ForeColor = System.Drawing.Color.Red
        Me.lblRefusalreason.Location = New System.Drawing.Point(22, 286)
        Me.lblRefusalreason.Name = "lblRefusalreason"
        Me.lblRefusalreason.Size = New System.Drawing.Size(14, 14)
        Me.lblRefusalreason.TabIndex = 57
        Me.lblRefusalreason.Text = "*"
        '
        'btnCvx
        '
        Me.btnCvx.BackgroundImage = CType(resources.GetObject("btnCvx.BackgroundImage"), System.Drawing.Image)
        Me.btnCvx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCvx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCvx.Image = CType(resources.GetObject("btnCvx.Image"), System.Drawing.Image)
        Me.btnCvx.Location = New System.Drawing.Point(983, 152)
        Me.btnCvx.Name = "btnCvx"
        Me.btnCvx.Size = New System.Drawing.Size(23, 23)
        Me.btnCvx.TabIndex = 22
        Me.ToolTip2.SetToolTip(Me.btnCvx, "Select Vaccine")
        Me.btnCvx.UseVisualStyleBackColor = True
        '
        'cmbRefusalreason
        '
        Me.cmbRefusalreason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbRefusalreason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRefusalreason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRefusalreason.FormattingEnabled = True
        Me.cmbRefusalreason.Location = New System.Drawing.Point(133, 282)
        Me.cmbRefusalreason.MaxLength = 1000
        Me.cmbRefusalreason.Name = "cmbRefusalreason"
        Me.cmbRefusalreason.Size = New System.Drawing.Size(294, 22)
        Me.cmbRefusalreason.TabIndex = 59
        Me.cmbRefusalreason.Visible = False
        '
        'btnMvx
        '
        Me.btnMvx.BackgroundImage = CType(resources.GetObject("btnMvx.BackgroundImage"), System.Drawing.Image)
        Me.btnMvx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMvx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMvx.Image = CType(resources.GetObject("btnMvx.Image"), System.Drawing.Image)
        Me.btnMvx.Location = New System.Drawing.Point(983, 186)
        Me.btnMvx.Name = "btnMvx"
        Me.btnMvx.Size = New System.Drawing.Size(23, 23)
        Me.btnMvx.TabIndex = 31
        Me.ToolTip2.SetToolTip(Me.btnMvx, "Select Manufacturer")
        Me.btnMvx.UseVisualStyleBackColor = True
        '
        'txt_refusal_comments
        '
        Me.txt_refusal_comments.Location = New System.Drawing.Point(655, 377)
        Me.txt_refusal_comments.MaxLength = 1000
        Me.txt_refusal_comments.Multiline = True
        Me.txt_refusal_comments.Name = "txt_refusal_comments"
        Me.txt_refusal_comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_refusal_comments.Size = New System.Drawing.Size(324, 52)
        Me.txt_refusal_comments.TabIndex = 61
        '
        'cmbSKU
        '
        Me.cmbSKU.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSKU.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSKU.FormattingEnabled = True
        Me.cmbSKU.Location = New System.Drawing.Point(133, 90)
        Me.cmbSKU.MaxLength = 15
        Me.cmbSKU.Name = "cmbSKU"
        Me.cmbSKU.Size = New System.Drawing.Size(324, 22)
        Me.cmbSKU.TabIndex = 17
        '
        'cmbLotNumber
        '
        Me.cmbLotNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLotNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLotNumber.FormattingEnabled = True
        Me.cmbLotNumber.Location = New System.Drawing.Point(133, 152)
        Me.cmbLotNumber.MaxLength = 50
        Me.cmbLotNumber.Name = "cmbLotNumber"
        Me.cmbLotNumber.Size = New System.Drawing.Size(324, 22)
        Me.cmbLotNumber.TabIndex = 37
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(54, 318)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(76, 14)
        Me.Label28.TabIndex = 63
        Me.Label28.Text = "Refused by :"
        '
        'lblTradeName
        '
        Me.lblTradeName.AutoSize = True
        Me.lblTradeName.ForeColor = System.Drawing.Color.Red
        Me.lblTradeName.Location = New System.Drawing.Point(37, 126)
        Me.lblTradeName.Name = "lblTradeName"
        Me.lblTradeName.Size = New System.Drawing.Size(14, 14)
        Me.lblTradeName.TabIndex = 24
        Me.lblTradeName.Text = "*"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(536, 380)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(115, 14)
        Me.Label35.TabIndex = 60
        Me.Label35.Text = "Refusal Comments :"
        '
        'lblLotNumber
        '
        Me.lblLotNumber.AutoSize = True
        Me.lblLotNumber.ForeColor = System.Drawing.Color.Red
        Me.lblLotNumber.Location = New System.Drawing.Point(37, 156)
        Me.lblLotNumber.Name = "lblLotNumber"
        Me.lblLotNumber.Size = New System.Drawing.Size(14, 14)
        Me.lblLotNumber.TabIndex = 35
        Me.lblLotNumber.Text = "*"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(34, 286)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(96, 14)
        Me.Label40.TabIndex = 58
        Me.Label40.Text = "Refusal Reason :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(582, 222)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(69, 14)
        Me.Label26.TabIndex = 38
        Me.Label26.Text = "Exp. Date :"
        '
        'txtMvx
        '
        Me.txtMvx.Location = New System.Drawing.Point(655, 186)
        Me.txtMvx.MaxLength = 1000
        Me.txtMvx.Name = "txtMvx"
        Me.txtMvx.Size = New System.Drawing.Size(324, 22)
        Me.txtMvx.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(82, 254)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 14)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Route :"
        '
        'txtCvx
        '
        Me.txtCvx.Location = New System.Drawing.Point(655, 152)
        Me.txtCvx.MaxLength = 1000
        Me.txtCvx.Name = "txtCvx"
        Me.txtCvx.Size = New System.Drawing.Size(324, 22)
        Me.txtCvx.TabIndex = 21
        '
        'txt_TradeName
        '
        Me.txt_TradeName.Location = New System.Drawing.Point(133, 122)
        Me.txt_TradeName.MaxLength = 1000
        Me.txt_TradeName.Name = "txt_TradeName"
        Me.txt_TradeName.Size = New System.Drawing.Size(324, 22)
        Me.txt_TradeName.TabIndex = 26
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(615, 318)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(36, 14)
        Me.Label67.TabIndex = 50
        Me.Label67.Text = "Site :"
        '
        'txt_vis
        '
        Me.txt_vis.Enabled = False
        Me.txt_vis.Location = New System.Drawing.Point(97, 679)
        Me.txt_vis.MaxLength = 1000
        Me.txt_vis.Name = "txt_vis"
        Me.txt_vis.Size = New System.Drawing.Size(287, 22)
        Me.txt_vis.TabIndex = 54
        Me.txt_vis.Visible = False
        '
        'cmbRoute
        '
        Me.cmbRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRoute.FormattingEnabled = True
        Me.cmbRoute.Location = New System.Drawing.Point(133, 250)
        Me.cmbRoute.MaxLength = 100
        Me.cmbRoute.Name = "cmbRoute"
        Me.cmbRoute.Size = New System.Drawing.Size(324, 22)
        Me.cmbRoute.TabIndex = 49
        '
        'dtpublication_date
        '
        Me.dtpublication_date.Checked = False
        Me.dtpublication_date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpublication_date.Location = New System.Drawing.Point(655, 344)
        Me.dtpublication_date.Name = "dtpublication_date"
        Me.dtpublication_date.ShowCheckBox = True
        Me.dtpublication_date.Size = New System.Drawing.Size(324, 22)
        Me.dtpublication_date.TabIndex = 56
        '
        'cmbSite
        '
        Me.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSite.FormattingEnabled = True
        Me.cmbSite.Location = New System.Drawing.Point(655, 314)
        Me.cmbSite.MaxLength = 100
        Me.cmbSite.Name = "cmbSite"
        Me.cmbSite.Size = New System.Drawing.Size(324, 22)
        Me.cmbSite.TabIndex = 51
        '
        'txt_dosage_given
        '
        Me.txt_dosage_given.Location = New System.Drawing.Point(133, 218)
        Me.txt_dosage_given.MaxLength = 6
        Me.txt_dosage_given.Name = "txt_dosage_given"
        Me.txt_dosage_given.Size = New System.Drawing.Size(202, 22)
        Me.txt_dosage_given.TabIndex = 42
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(60, 681)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(34, 14)
        Me.Label37.TabIndex = 53
        Me.Label37.Text = "VIS :"
        Me.Label37.Visible = False
        '
        'txt_amount_given
        '
        Me.txt_amount_given.Location = New System.Drawing.Point(655, 282)
        Me.txt_amount_given.MaxLength = 6
        Me.txt_amount_given.Name = "txt_amount_given"
        Me.txt_amount_given.Size = New System.Drawing.Size(54, 22)
        Me.txt_amount_given.TabIndex = 45
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(49, 222)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(81, 14)
        Me.Label23.TabIndex = 41
        Me.Label23.Text = "Doses Given :"
        '
        'lblDosesOnHand
        '
        Me.lblDosesOnHand.AutoSize = True
        Me.lblDosesOnHand.Location = New System.Drawing.Point(338, 222)
        Me.lblDosesOnHand.Name = "lblDosesOnHand"
        Me.lblDosesOnHand.Size = New System.Drawing.Size(119, 14)
        Me.lblDosesOnHand.TabIndex = 43
        Me.lblDosesOnHand.Text = "XXXX doses on hand"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(558, 286)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(93, 14)
        Me.Label44.TabIndex = 44
        Me.Label44.Text = "Amount Given :"
        '
        'lblDosesGiven
        '
        Me.lblDosesGiven.AutoSize = True
        Me.lblDosesGiven.ForeColor = System.Drawing.Color.Red
        Me.lblDosesGiven.Location = New System.Drawing.Point(35, 222)
        Me.lblDosesGiven.Name = "lblDosesGiven"
        Me.lblDosesGiven.Size = New System.Drawing.Size(14, 14)
        Me.lblDosesGiven.TabIndex = 40
        Me.lblDosesGiven.Text = "*"
        '
        'cmbUnitOfMeasure
        '
        Me.cmbUnitOfMeasure.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbUnitOfMeasure.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbUnitOfMeasure.DropDownHeight = 110
        Me.cmbUnitOfMeasure.DropDownWidth = 208
        Me.cmbUnitOfMeasure.FormattingEnabled = True
        Me.cmbUnitOfMeasure.IntegralHeight = False
        Me.cmbUnitOfMeasure.Location = New System.Drawing.Point(721, 282)
        Me.cmbUnitOfMeasure.Name = "cmbUnitOfMeasure"
        Me.cmbUnitOfMeasure.Size = New System.Drawing.Size(259, 22)
        Me.cmbUnitOfMeasure.TabIndex = 329
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(709, 286)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(11, 14)
        Me.Label6.TabIndex = 339
        Me.Label6.Text = "-"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(810, 268)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 11)
        Me.Label8.TabIndex = 340
        Me.Label8.Text = "(Unit of Measure)"
        '
        'TabPageReaction
        '
        Me.TabPageReaction.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPageReaction.Controls.Add(Me.grpReaction)
        Me.TabPageReaction.Controls.Add(Me.lblNote)
        Me.TabPageReaction.Controls.Add(Me.Label20)
        Me.TabPageReaction.Controls.Add(Me.Label19)
        Me.TabPageReaction.Controls.Add(Me.Label18)
        Me.TabPageReaction.Controls.Add(Me.Label17)
        Me.TabPageReaction.Controls.Add(Me.chkPatientHasReaction)
        Me.TabPageReaction.Location = New System.Drawing.Point(4, 23)
        Me.TabPageReaction.Name = "TabPageReaction"
        Me.TabPageReaction.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageReaction.Size = New System.Drawing.Size(1071, 669)
        Me.TabPageReaction.TabIndex = 1
        Me.TabPageReaction.Text = "Reaction  "
        '
        'grpReaction
        '
        Me.grpReaction.Controls.Add(Me.dtOnsetDate)
        Me.grpReaction.Controls.Add(Me.Label12)
        Me.grpReaction.Controls.Add(Me.btnBrowseReaction)
        Me.grpReaction.Controls.Add(Me.lstReaction)
        Me.grpReaction.Controls.Add(Me.Label3)
        Me.grpReaction.Controls.Add(Me.rdo_PatientRecoveredNo)
        Me.grpReaction.Controls.Add(Me.rdo_PatientRecoveredUnknown)
        Me.grpReaction.Controls.Add(Me.rdo_PatientRecoveredYes)
        Me.grpReaction.Controls.Add(Me.Label46)
        Me.grpReaction.Controls.Add(Me.dtPatientDied)
        Me.grpReaction.Controls.Add(Me.txtHospitalizationDays)
        Me.grpReaction.Controls.Add(Me.chk_NoneOfTheAbove)
        Me.grpReaction.Controls.Add(Me.chk_ResultedInPermDisability)
        Me.grpReaction.Controls.Add(Me.chk_RequiredEmergencyRoom)
        Me.grpReaction.Controls.Add(Me.chk_ResultedInProlongation)
        Me.grpReaction.Controls.Add(Me.chk_LifeThreateningIllness)
        Me.grpReaction.Controls.Add(Me.chk_RequiredHospitalization)
        Me.grpReaction.Controls.Add(Me.chkPatientDied)
        Me.grpReaction.Controls.Add(Me.Label42)
        Me.grpReaction.Controls.Add(Me.Label45)
        Me.grpReaction.Controls.Add(Me.Label22)
        Me.grpReaction.Controls.Add(Me.txtAdverseEvent)
        Me.grpReaction.Controls.Add(Me.Label21)
        Me.grpReaction.Location = New System.Drawing.Point(23, 42)
        Me.grpReaction.Name = "grpReaction"
        Me.grpReaction.Size = New System.Drawing.Size(1024, 608)
        Me.grpReaction.TabIndex = 2
        Me.grpReaction.TabStop = False
        '
        'dtOnsetDate
        '
        Me.dtOnsetDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtOnsetDate.Location = New System.Drawing.Point(100, 19)
        Me.dtOnsetDate.Name = "dtOnsetDate"
        Me.dtOnsetDate.Size = New System.Drawing.Size(109, 22)
        Me.dtOnsetDate.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(19, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 14)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Onset Date :"
        '
        'btnBrowseReaction
        '
        Me.btnBrowseReaction.BackgroundImage = CType(resources.GetObject("btnBrowseReaction.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseReaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseReaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseReaction.Image = CType(resources.GetObject("btnBrowseReaction.Image"), System.Drawing.Image)
        Me.btnBrowseReaction.Location = New System.Drawing.Point(157, 417)
        Me.btnBrowseReaction.Name = "btnBrowseReaction"
        Me.btnBrowseReaction.Size = New System.Drawing.Size(123, 31)
        Me.btnBrowseReaction.TabIndex = 22
        Me.btnBrowseReaction.Text = " Add to History"
        Me.btnBrowseReaction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnBrowseReaction.UseVisualStyleBackColor = True
        '
        'lstReaction
        '
        Me.lstReaction.FormattingEnabled = True
        Me.lstReaction.ItemHeight = 14
        Me.lstReaction.Location = New System.Drawing.Point(19, 453)
        Me.lstReaction.Name = "lstReaction"
        Me.lstReaction.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstReaction.Size = New System.Drawing.Size(968, 74)
        Me.lstReaction.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 426)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 14)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Patient Allergy record :"
        '
        'rdo_PatientRecoveredNo
        '
        Me.rdo_PatientRecoveredNo.AutoSize = True
        Me.rdo_PatientRecoveredNo.Location = New System.Drawing.Point(200, 375)
        Me.rdo_PatientRecoveredNo.Name = "rdo_PatientRecoveredNo"
        Me.rdo_PatientRecoveredNo.Size = New System.Drawing.Size(40, 18)
        Me.rdo_PatientRecoveredNo.TabIndex = 18
        Me.rdo_PatientRecoveredNo.TabStop = True
        Me.rdo_PatientRecoveredNo.Text = "No"
        Me.rdo_PatientRecoveredNo.UseVisualStyleBackColor = True
        '
        'rdo_PatientRecoveredUnknown
        '
        Me.rdo_PatientRecoveredUnknown.AutoSize = True
        Me.rdo_PatientRecoveredUnknown.Location = New System.Drawing.Point(254, 375)
        Me.rdo_PatientRecoveredUnknown.Name = "rdo_PatientRecoveredUnknown"
        Me.rdo_PatientRecoveredUnknown.Size = New System.Drawing.Size(77, 18)
        Me.rdo_PatientRecoveredUnknown.TabIndex = 19
        Me.rdo_PatientRecoveredUnknown.TabStop = True
        Me.rdo_PatientRecoveredUnknown.Text = "Unknown"
        Me.rdo_PatientRecoveredUnknown.UseVisualStyleBackColor = True
        '
        'rdo_PatientRecoveredYes
        '
        Me.rdo_PatientRecoveredYes.AutoSize = True
        Me.rdo_PatientRecoveredYes.Location = New System.Drawing.Point(141, 375)
        Me.rdo_PatientRecoveredYes.Name = "rdo_PatientRecoveredYes"
        Me.rdo_PatientRecoveredYes.Size = New System.Drawing.Size(45, 18)
        Me.rdo_PatientRecoveredYes.TabIndex = 17
        Me.rdo_PatientRecoveredYes.TabStop = True
        Me.rdo_PatientRecoveredYes.Text = "Yes"
        Me.rdo_PatientRecoveredYes.UseVisualStyleBackColor = True
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(17, 377)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(113, 14)
        Me.Label46.TabIndex = 16
        Me.Label46.Text = "Patient recovered :"
        '
        'dtPatientDied
        '
        Me.dtPatientDied.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPatientDied.Location = New System.Drawing.Point(174, 199)
        Me.dtPatientDied.Name = "dtPatientDied"
        Me.dtPatientDied.Size = New System.Drawing.Size(109, 22)
        Me.dtPatientDied.TabIndex = 7
        '
        'txtHospitalizationDays
        '
        Me.txtHospitalizationDays.Location = New System.Drawing.Point(174, 265)
        Me.txtHospitalizationDays.MaxLength = 4
        Me.txtHospitalizationDays.Name = "txtHospitalizationDays"
        Me.txtHospitalizationDays.Size = New System.Drawing.Size(58, 22)
        Me.txtHospitalizationDays.TabIndex = 11
        '
        'chk_NoneOfTheAbove
        '
        Me.chk_NoneOfTheAbove.AutoSize = True
        Me.chk_NoneOfTheAbove.Location = New System.Drawing.Point(19, 333)
        Me.chk_NoneOfTheAbove.Name = "chk_NoneOfTheAbove"
        Me.chk_NoneOfTheAbove.Size = New System.Drawing.Size(130, 18)
        Me.chk_NoneOfTheAbove.TabIndex = 15
        Me.chk_NoneOfTheAbove.Text = "None of the above"
        Me.chk_NoneOfTheAbove.UseVisualStyleBackColor = True
        '
        'chk_ResultedInPermDisability
        '
        Me.chk_ResultedInPermDisability.AutoSize = True
        Me.chk_ResultedInPermDisability.Location = New System.Drawing.Point(19, 311)
        Me.chk_ResultedInPermDisability.Name = "chk_ResultedInPermDisability"
        Me.chk_ResultedInPermDisability.Size = New System.Drawing.Size(198, 18)
        Me.chk_ResultedInPermDisability.TabIndex = 14
        Me.chk_ResultedInPermDisability.Text = "Resulted in permanent disability"
        Me.chk_ResultedInPermDisability.UseVisualStyleBackColor = True
        '
        'chk_RequiredEmergencyRoom
        '
        Me.chk_RequiredEmergencyRoom.AutoSize = True
        Me.chk_RequiredEmergencyRoom.Location = New System.Drawing.Point(19, 245)
        Me.chk_RequiredEmergencyRoom.Name = "chk_RequiredEmergencyRoom"
        Me.chk_RequiredEmergencyRoom.Size = New System.Drawing.Size(236, 18)
        Me.chk_RequiredEmergencyRoom.TabIndex = 9
        Me.chk_RequiredEmergencyRoom.Text = "Required emergency room/doctor visit"
        Me.chk_RequiredEmergencyRoom.UseVisualStyleBackColor = True
        '
        'chk_ResultedInProlongation
        '
        Me.chk_ResultedInProlongation.AutoSize = True
        Me.chk_ResultedInProlongation.Location = New System.Drawing.Point(19, 289)
        Me.chk_ResultedInProlongation.Name = "chk_ResultedInProlongation"
        Me.chk_ResultedInProlongation.Size = New System.Drawing.Size(252, 18)
        Me.chk_ResultedInProlongation.TabIndex = 13
        Me.chk_ResultedInProlongation.Text = "Resulted in prolongation of hospitalization"
        Me.chk_ResultedInProlongation.UseVisualStyleBackColor = True
        '
        'chk_LifeThreateningIllness
        '
        Me.chk_LifeThreateningIllness.AutoSize = True
        Me.chk_LifeThreateningIllness.Location = New System.Drawing.Point(19, 223)
        Me.chk_LifeThreateningIllness.Name = "chk_LifeThreateningIllness"
        Me.chk_LifeThreateningIllness.Size = New System.Drawing.Size(147, 18)
        Me.chk_LifeThreateningIllness.TabIndex = 8
        Me.chk_LifeThreateningIllness.Text = "Life threatening illness"
        Me.chk_LifeThreateningIllness.UseVisualStyleBackColor = True
        '
        'chk_RequiredHospitalization
        '
        Me.chk_RequiredHospitalization.AutoSize = True
        Me.chk_RequiredHospitalization.Location = New System.Drawing.Point(19, 267)
        Me.chk_RequiredHospitalization.Name = "chk_RequiredHospitalization"
        Me.chk_RequiredHospitalization.Size = New System.Drawing.Size(153, 18)
        Me.chk_RequiredHospitalization.TabIndex = 10
        Me.chk_RequiredHospitalization.Text = "Required hospitalization"
        Me.chk_RequiredHospitalization.UseVisualStyleBackColor = True
        '
        'chkPatientDied
        '
        Me.chkPatientDied.AutoSize = True
        Me.chkPatientDied.Location = New System.Drawing.Point(19, 201)
        Me.chkPatientDied.Name = "chkPatientDied"
        Me.chkPatientDied.Size = New System.Drawing.Size(92, 18)
        Me.chkPatientDied.TabIndex = 5
        Me.chkPatientDied.Text = "Patient died"
        Me.chkPatientDied.UseVisualStyleBackColor = True
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(131, 203)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(41, 14)
        Me.Label42.TabIndex = 6
        Me.Label42.Text = "Date :"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(237, 269)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(32, 14)
        Me.Label45.TabIndex = 12
        Me.Label45.Text = "Days"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(16, 179)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(144, 14)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "Check all appropriate :"
        '
        'txtAdverseEvent
        '
        Me.txtAdverseEvent.Location = New System.Drawing.Point(13, 74)
        Me.txtAdverseEvent.MaxLength = 1000
        Me.txtAdverseEvent.Multiline = True
        Me.txtAdverseEvent.Name = "txtAdverseEvent"
        Me.txtAdverseEvent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAdverseEvent.Size = New System.Drawing.Size(968, 81)
        Me.txtAdverseEvent.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(13, 55)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(446, 14)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Describe adverse event(s) (symptoms, signs, time course) and treatment, if any"
        '
        'lblNote
        '
        Me.lblNote.AutoSize = True
        Me.lblNote.ForeColor = System.Drawing.Color.Red
        Me.lblNote.Location = New System.Drawing.Point(183, 18)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(218, 14)
        Me.lblNote.TabIndex = 36
        Me.lblNote.Text = "(Vaccine refusal cannot have reaction)"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Location = New System.Drawing.Point(4, 665)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1063, 1)
        Me.Label20.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Location = New System.Drawing.Point(4, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1063, 1)
        Me.Label19.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Location = New System.Drawing.Point(1067, 3)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 663)
        Me.Label18.TabIndex = 18
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(3, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 663)
        Me.Label17.TabIndex = 17
        '
        'chkPatientHasReaction
        '
        Me.chkPatientHasReaction.AutoSize = True
        Me.chkPatientHasReaction.Location = New System.Drawing.Point(29, 17)
        Me.chkPatientHasReaction.Name = "chkPatientHasReaction"
        Me.chkPatientHasReaction.Size = New System.Drawing.Size(150, 18)
        Me.chkPatientHasReaction.TabIndex = 1
        Me.chkPatientHasReaction.Text = "Patient had a Reaction"
        Me.chkPatientHasReaction.UseVisualStyleBackColor = True
        '
        'TabVIS
        '
        Me.TabVIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabVIS.Controls.Add(Me.Panel4)
        Me.TabVIS.Controls.Add(Me.Panel3)
        Me.TabVIS.Location = New System.Drawing.Point(4, 23)
        Me.TabVIS.Name = "TabVIS"
        Me.TabVIS.Size = New System.Drawing.Size(1071, 669)
        Me.TabVIS.TabIndex = 2
        Me.TabVIS.Text = "VIS"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label48)
        Me.Panel4.Controls.Add(Me.Label49)
        Me.Panel4.Controls.Add(Me.Label50)
        Me.Panel4.Controls.Add(Me.Label51)
        Me.Panel4.Controls.Add(Me.c1VIS)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 41)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel4.Size = New System.Drawing.Size(1071, 628)
        Me.Panel4.TabIndex = 47
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Location = New System.Drawing.Point(1067, 4)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(1, 620)
        Me.Label48.TabIndex = 48
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label49.Location = New System.Drawing.Point(3, 4)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 620)
        Me.Label49.TabIndex = 47
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label50.Location = New System.Drawing.Point(3, 624)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1065, 1)
        Me.Label50.TabIndex = 46
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label51.Location = New System.Drawing.Point(3, 3)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(1065, 1)
        Me.Label51.TabIndex = 45
        '
        'c1VIS
        '
        Me.c1VIS.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1VIS.AllowEditing = False
        Me.c1VIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1VIS.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1VIS.ColumnInfo = "9,0,0,0,0,105,Columns:"
        Me.c1VIS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1VIS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1VIS.Location = New System.Drawing.Point(3, 3)
        Me.c1VIS.Margin = New System.Windows.Forms.Padding(2)
        Me.c1VIS.Name = "c1VIS"
        Me.c1VIS.Rows.Count = 1
        Me.c1VIS.Rows.DefaultSize = 21
        Me.c1VIS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1VIS.Size = New System.Drawing.Size(1065, 622)
        Me.c1VIS.StyleInfo = resources.GetString("c1VIS.StyleInfo")
        Me.c1VIS.TabIndex = 44
        Me.c1VIS.Tag = "Print"
        Me.c1VIS.Tree.NodeImageExpanded = CType(resources.GetObject("c1VIS.Tree.NodeImageExpanded"), System.Drawing.Image)
        Me.c1VIS.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label52)
        Me.Panel3.Controls.Add(Me.chk_vis_given)
        Me.Panel3.Controls.Add(Me.Label53)
        Me.Panel3.Controls.Add(Me.Label54)
        Me.Panel3.Controls.Add(Me.Label55)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel3.Size = New System.Drawing.Size(1071, 41)
        Me.Panel3.TabIndex = 49
        Me.Panel3.Visible = False
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label52.Location = New System.Drawing.Point(1067, 4)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1, 36)
        Me.Label52.TabIndex = 48
        '
        'chk_vis_given
        '
        Me.chk_vis_given.AutoSize = True
        Me.chk_vis_given.Location = New System.Drawing.Point(20, 12)
        Me.chk_vis_given.Name = "chk_vis_given"
        Me.chk_vis_given.Size = New System.Drawing.Size(138, 18)
        Me.chk_vis_given.TabIndex = 53
        Me.chk_vis_given.Text = "VIS Given to Patient"
        Me.chk_vis_given.UseVisualStyleBackColor = True
        Me.chk_vis_given.Visible = False
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label53.Location = New System.Drawing.Point(3, 4)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 36)
        Me.Label53.TabIndex = 47
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label54.Location = New System.Drawing.Point(3, 40)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1065, 1)
        Me.Label54.TabIndex = 46
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label55.Location = New System.Drawing.Point(3, 3)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(1065, 1)
        Me.Label55.TabIndex = 45
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tblStrip)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1079, 54)
        Me.Panel1.TabIndex = 18
        '
        'tblStrip
        '
        Me.tblStrip.AddSeparatorsBetweenEachButton = False
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.ButtonsToHide = CType(resources.GetObject("tblStrip.ButtonsToHide"), System.Collections.ArrayList)
        Me.tblStrip.ConnectionString = Nothing
        Me.tblStrip.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_PrintVis, Me.tblbtn_Save, Me.tblbtn_Close})
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.ModuleName = Nothing
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(1079, 53)
        Me.tblStrip.TabIndex = 1
        Me.tblStrip.TabStop = True
        Me.tblStrip.Text = "ToolStrip1"
        Me.tblStrip.UserID = CType(0, Long)
        '
        'tblbtn_PrintVis
        '
        Me.tblbtn_PrintVis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_PrintVis.Image = CType(resources.GetObject("tblbtn_PrintVis.Image"), System.Drawing.Image)
        Me.tblbtn_PrintVis.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_PrintVis.Name = "tblbtn_PrintVis"
        Me.tblbtn_PrintVis.Size = New System.Drawing.Size(36, 50)
        Me.tblbtn_PrintVis.Tag = "Print Due"
        Me.tblbtn_PrintVis.Text = "&VIS"
        Me.tblbtn_PrintVis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_PrintVis.ToolTipText = "Vaccine Information Statement "
        '
        'tblbtn_Save
        '
        Me.tblbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save.Image = CType(resources.GetObject("tblbtn_Save.Image"), System.Drawing.Image)
        Me.tblbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save.Name = "tblbtn_Save"
        Me.tblbtn_Save.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Save.Tag = "Save and Close"
        Me.tblbtn_Save.Text = "&Save&&Cls"
        Me.tblbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save.ToolTipText = "Save and Close"
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Close.ToolTipText = "Close"
        '
        'frmImTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1079, 750)
        Me.Controls.Add(Me.TabImmunization)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImTransaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Immunization"
        Me.TabImmunization.ResumeLayout(False)
        Me.TabPageAdministratin.ResumeLayout(False)
        Me.TabPageAdministratin.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TabPageReaction.ResumeLayout(False)
        Me.TabPageReaction.PerformLayout()
        Me.grpReaction.ResumeLayout(False)
        Me.grpReaction.PerformLayout()
        Me.TabVIS.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.c1VIS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbAdministred As System.Windows.Forms.ComboBox
    Friend WithEvents dttransaction_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents optRefused As System.Windows.Forms.RadioButton
    Friend WithEvents optReported As System.Windows.Forms.RadioButton
    Friend WithEvents optAdministered As System.Windows.Forms.RadioButton
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents TabImmunization As System.Windows.Forms.TabControl
    Friend WithEvents TabPageAdministratin As System.Windows.Forms.TabPage
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabPageReaction As System.Windows.Forms.TabPage
    Friend WithEvents grpReaction As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtAdverseEvent As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents chkPatientHasReaction As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowseReaction As System.Windows.Forms.Button
    Friend WithEvents lstReaction As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdo_PatientRecoveredNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_PatientRecoveredUnknown As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_PatientRecoveredYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents dtPatientDied As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtHospitalizationDays As System.Windows.Forms.TextBox
    Friend WithEvents chk_NoneOfTheAbove As System.Windows.Forms.CheckBox
    Friend WithEvents chk_ResultedInPermDisability As System.Windows.Forms.CheckBox
    Friend WithEvents chk_RequiredEmergencyRoom As System.Windows.Forms.CheckBox
    Friend WithEvents chk_ResultedInProlongation As System.Windows.Forms.CheckBox
    Friend WithEvents chk_LifeThreateningIllness As System.Windows.Forms.CheckBox
    Friend WithEvents chk_RequiredHospitalization As System.Windows.Forms.CheckBox
    Friend WithEvents chkPatientDied As System.Windows.Forms.CheckBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dtOnsetDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblProviderName As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents btnClearCPT As System.Windows.Forms.Button
    Friend WithEvents btnClearDiagnosis As System.Windows.Forms.Button
    Friend WithEvents txt_notes As System.Windows.Forms.TextBox
    Friend WithEvents txtNDCcode As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnBrowsCPT As System.Windows.Forms.Button
    Friend WithEvents btnBrowsDiagnosis As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents cmbFunding As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIcd As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCpt As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents dtDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkSetReminder As System.Windows.Forms.CheckBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lblRefusedBy As System.Windows.Forms.Label
    Friend WithEvents lblRefusalreason As System.Windows.Forms.Label
    Friend WithEvents cmbRefusalreason As System.Windows.Forms.ComboBox
    Friend WithEvents txt_refusal_comments As System.Windows.Forms.TextBox
    Friend WithEvents txt_refused_by As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txt_vis As System.Windows.Forms.TextBox
    Friend WithEvents dtpublication_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblDosesGiven As System.Windows.Forms.Label
    Friend WithEvents txt_units As System.Windows.Forms.TextBox
    Friend WithEvents txt_amount_given As System.Windows.Forms.TextBox
    Friend WithEvents txt_dosage_given As System.Windows.Forms.TextBox
    Friend WithEvents cmbSite As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRoute As System.Windows.Forms.ComboBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblLotNumber As System.Windows.Forms.Label
    Friend WithEvents lblTradeName As System.Windows.Forms.Label
    Friend WithEvents cmbLotNumber As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSKU As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchsku As System.Windows.Forms.Button
    Friend WithEvents dtexpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txt_TradeName As System.Windows.Forms.TextBox
    Friend WithEvents btnTradeName As System.Windows.Forms.Button
    Friend WithEvents pnlTradeNameControl As System.Windows.Forms.Panel
    Friend WithEvents pnlCvxControl As System.Windows.Forms.Panel
    Friend WithEvents pnlMvxControl As System.Windows.Forms.Panel
    Friend WithEvents txtCvx As System.Windows.Forms.TextBox
    Friend WithEvents btnCvx As System.Windows.Forms.Button
    Friend WithEvents txtMvx As System.Windows.Forms.TextBox
    Friend WithEvents btnMvx As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnScan As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BtnAddTradeNameCategory As System.Windows.Forms.Button
    Friend WithEvents BtnAddManufacturerCategory As System.Windows.Forms.Button
    Friend WithEvents BtnAddVaccineCategory As System.Windows.Forms.Button
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents tblStrip As gloToolStrip.gloToolStrip
    Friend WithEvents tblbtn_PrintVis As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents lblDosesOnHand As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnAddCategory As System.Windows.Forms.Button
    Friend WithEvents lblFunding As System.Windows.Forms.Label
    Friend WithEvents cmbUnitOfMeasure As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddUnitsOfMeasureItem As System.Windows.Forms.Button
    Friend WithEvents lblSnomedIdValue As System.Windows.Forms.Label
    Friend WithEvents btnClearSnomed As System.Windows.Forms.Button
    Friend WithEvents btnBrowseSnomed As System.Windows.Forms.Button
    Friend WithEvents lblSnomed As System.Windows.Forms.Label
    Friend WithEvents cmbPublicityCode As System.Windows.Forms.ComboBox
    Friend WithEvents lblPublicityCode As System.Windows.Forms.Label
    Friend WithEvents btnAddPublicityCodeItem As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtTransactionTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAdministeredTime As System.Windows.Forms.Label
    Friend WithEvents BtnUncertainCVX As System.Windows.Forms.Button
    Friend WithEvents btnClrRefProvider As System.Windows.Forms.Button
    Friend WithEvents btnRefProvider As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblOrdProvider As System.Windows.Forms.Label
    Friend WithEvents btnClearRefusalReason As System.Windows.Forms.Button
    Friend WithEvents btnBrwRefusalReason As System.Windows.Forms.Button
    Friend WithEvents txtRefusalReason As System.Windows.Forms.TextBox
    Friend WithEvents TabVIS As System.Windows.Forms.TabPage
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents c1VIS As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents chk_vis_given As System.Windows.Forms.CheckBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents lblImmunizationFunding As System.Windows.Forms.Label
    Friend WithEvents cmbImmunizationFunding As System.Windows.Forms.ComboBox
    Friend WithEvents optPartiallyAdministered As System.Windows.Forms.RadioButton
    Friend WithEvents optNotAdministered As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtcqm As System.Windows.Forms.TextBox
    Friend WithEvents lblcqm As System.Windows.Forms.Label
    Friend WithEvents btnclrcqm As System.Windows.Forms.Button
    Friend WithEvents btnbrwcqm As System.Windows.Forms.Button
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents publicityeffetiveDTP As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label57 As System.Windows.Forms.Label
End Class
