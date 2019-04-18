<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrescription
    '' Inherits System.Windows.Forms.Form
    Inherits gloAUSLibrary.MasterForm

    'Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing AndAlso components IsNot Nothing Then
    '        components.Dispose()
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

#Region " TO Check the Multiple instances Of Form "
    Public Shared IsOpen As Boolean = False
    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmPrescription

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.
                Dispose_Global()
                If Not (components Is Nothing) Then
                    components.Dispose()

                End If
                'If (IsNothing(dvNext) = False) Then
                '    dvNext.Dispose()
                '    dvNext = Nothing
                'End If
                Try
                    If (IsNothing(PrintDialog1) = False) Then
                        PrintDialog1.Dispose()
                        PrintDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(ToList) = False) Then
                        ToList.Dispose()
                        ToList = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(_RxPatientStrip) = False) Then
                        _RxPatientStrip.Dispose()
                        _RxPatientStrip = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Dim dtpControls As DateTimePicker() = {dtpVisitDate}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                Catch ex As Exception

                End Try
                Dim dtpContextMenuStrip As ContextMenuStrip() = {cMnuStrp}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenuStrip)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpContextMenuStrip)
                Catch ex As Exception

                End Try
                
               
               
               
                
                'frm = Nothing
            End If
            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If
        'frm = Nothing
        Me.blnDisposed = True
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception

        End Try
    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance(ByVal ChangeRequest As gloGlobal.SS.RxChangeRequest) As frmPrescription
        '_mu.WaitOne()
        Try

            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmPrescription" Then
                    If CType(f, frmPrescription).nRxModulePatientID = ChangeRequest.PatientID Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmPrescription(ChangeRequest)
            End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

    Public Shared Function GetInstance(ByVal refRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefRequest) As frmPrescription
        '_mu.WaitOne()
        Try

            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmPrescription" Then
                    If CType(f, frmPrescription).nRxModulePatientID = refRequest.PatientID Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmPrescription(refRequest)
            End If

            'If frm Is Nothing Then
            '    frm = New frm_LM_Orders(VisitID, VisitDate, PatientID, OpenfromMainGrid, blnRecordLock)
            'End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function


    Public Shared Function GetInstance(ByVal m_visitid As Long, ByVal PatientID As Int64) As frmPrescription
        '_mu.WaitOne()
        Try

            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmPrescription" Then
                    If CType(f, frmPrescription).nRxModulePatientID = PatientID Then
                        IsOpen = True
                        frm = f
                    End If

                End If

            Next
            If (IsOpen = False) Then
                frm = New frmPrescription(m_visitid, PatientID)
            End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

    Public Shared Function GetInstance(ByVal PatientID As Long, Optional ByVal _IsOpenedFromPrescription As Boolean = False) As frmPrescription
        Try
            IsOpen = False
            For Each f As Form In Application.OpenForms
                If f.Name = "frmPrescription" Then
                    If CType(f, frmPrescription).nRxModulePatientID = PatientID Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmPrescription(PatientID, _IsOpenedFromPrescription)
            End If
        Finally
        End Try
        Return frm
    End Function

    Public Shared Function GetInstance(ByVal arrdrug As ArrayList, ByVal ProviderId As Int64, Optional ByVal m_Visitid As Int64 = 0, Optional ByVal m_Patientid As Int64 = 0, Optional ShowCarryForwardMessage As Boolean = True) As frmPrescription
        '_mu.WaitOne()
        Try

            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmPrescription" Then
                    If CType(f, frmPrescription).nRxModulePatientID = m_Patientid Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmPrescription(arrdrug, ProviderId, m_Visitid, m_Patientid, ShowCarryForwardMessage)
            End If

            'If frm Is Nothing Then
            '    frm = New frm_LM_Orders(VisitID, VisitDate, PatientID, OpenfromMainGrid, blnRecordLock)
            'End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function


#End Region

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrescription))
        Me.pnlCenter2 = New System.Windows.Forms.Panel()
        Me.pnlcentertop = New System.Windows.Forms.Panel()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.cMnuStrp = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAllergies = New System.Windows.Forms.Button()
        Me.pnlAllergiesAlerts = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.picBottom = New System.Windows.Forms.PictureBox()
        Me.lblAlert1 = New System.Windows.Forms.Label()
        Me.picAlertClose1 = New System.Windows.Forms.PictureBox()
        Me.picInfo = New System.Windows.Forms.PictureBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.picTop = New System.Windows.Forms.PictureBox()
        Me.picMiddle = New System.Windows.Forms.PictureBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.pnlPDMP = New System.Windows.Forms.Panel()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.elementHostPDMP = New System.Windows.Forms.Integration.ElementHost()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.lblPDMP = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.pnlDIProgress = New System.Windows.Forms.Panel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlWait = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.pnlElementHostCopay = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.elementHostCopay = New System.Windows.Forms.Integration.ElementHost()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.lblCoverageCopayHeading = New System.Windows.Forms.Label()
        Me.btnCloseCopayCoverage = New System.Windows.Forms.Button()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.pnlFormulary = New System.Windows.Forms.Panel()
        Me.elementHost = New System.Windows.Forms.Integration.ElementHost()
        Me.pnlFormularyDrugName = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.lblFormularyDrugName = New System.Windows.Forms.Label()
        Me.btnFormularyGridClose = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.pnlFormularyTransactionMessage = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblFormularyTransactionMessage = New System.Windows.Forms.Label()
        Me.pnlFormularyProgress = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.pnlFormularyCoverage = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rtfFormularyDescription = New gloEMR.gloRichtextbox(Me.components)
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pnlFormularyCoverageHeading = New System.Windows.Forms.Panel()
        Me.btnFormularyCovPnlClose = New System.Windows.Forms.Button()
        Me.lblAlternativeDrugName = New System.Windows.Forms.Label()
        Me.pnlViewMedicationHistory = New System.Windows.Forms.Panel()
        Me.pnlMedicationHistory = New System.Windows.Forms.Panel()
        Me.pnlMedicationHistoryHeader = New System.Windows.Forms.Panel()
        Me.btnViewMedicationHistoryClose = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.pnlMedicationHistoryFooter = New System.Windows.Forms.Panel()
        Me.pnlViewDocument = New System.Windows.Forms.Panel()
        Me.pnlDocument = New System.Windows.Forms.Panel()
        Me.pnlDMSHeader = New System.Windows.Forms.Panel()
        Me.btnViewDocumentClose = New System.Windows.Forms.Button()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.pnlViewDocumentFooter = New System.Windows.Forms.Panel()
        Me.pnlcenter = New System.Windows.Forms.Panel()
        Me.pnlFlexGrid = New System.Windows.Forms.Panel()
        Me.pnlRxMx = New System.Windows.Forms.Panel()
        Me.pnlMedicationGrid = New System.Windows.Forms.Panel()
        Me.pnlMedicationDetails = New System.Windows.Forms.Panel()
        Me.btnMedication = New System.Windows.Forms.Button()
        Me.pnlMxLabel = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblMedicationGridLabel = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnMxDown = New System.Windows.Forms.Button()
        Me.btnMxUp = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.sptRxMx = New System.Windows.Forms.Splitter()
        Me.pnlPrescriptionGrid = New System.Windows.Forms.Panel()
        Me.pnlPrescriptionDetails = New System.Windows.Forms.Panel()
        Me.pnlprescriptionbtn = New System.Windows.Forms.Panel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.btnPrescription = New System.Windows.Forms.Button()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.pnlRxLabel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblPrescriptionGridLabel = New System.Windows.Forms.Label()
        Me.btnRxDown = New System.Windows.Forms.Button()
        Me.btnRxUp = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlSupervisingProvider = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblSupervisingValidation = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cmbSupervisingProvider = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.sptRefill = New System.Windows.Forms.Splitter()
        Me.pnlRefill = New System.Windows.Forms.Panel()
        Me.pnlmonograph = New System.Windows.Forms.Panel()
        Me.pnlDIScreenResult = New System.Windows.Forms.Panel()
        Me.splleft = New System.Windows.Forms.Splitter()
        Me.pnlleft = New System.Windows.Forms.Panel()
        Me.splRight = New System.Windows.Forms.Splitter()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.pnlMxHistory = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlRxHistory = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pnltop = New System.Windows.Forms.Panel()
        Me.pnlFormularyToolBar = New System.Windows.Forms.Panel()
        Me.pnlDI = New System.Windows.Forms.Panel()
        Me.pnlmainToolBar = New System.Windows.Forms.Panel()
        Me.pnlAllergiesAlerts.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.picBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAlertClose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMiddle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPDMP.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.pnlDIProgress.SuspendLayout()
        Me.pnlWait.SuspendLayout()
        Me.pnlElementHostCopay.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.pnlFormulary.SuspendLayout()
        Me.pnlFormularyDrugName.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.pnlFormularyTransactionMessage.SuspendLayout()
        Me.pnlFormularyProgress.SuspendLayout()
        Me.pnlFormularyCoverage.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlFormularyCoverageHeading.SuspendLayout()
        Me.pnlViewMedicationHistory.SuspendLayout()
        Me.pnlMedicationHistoryHeader.SuspendLayout()
        Me.pnlViewDocument.SuspendLayout()
        Me.pnlDMSHeader.SuspendLayout()
        Me.pnlcenter.SuspendLayout()
        Me.pnlFlexGrid.SuspendLayout()
        Me.pnlRxMx.SuspendLayout()
        Me.pnlMedicationGrid.SuspendLayout()
        Me.pnlMxLabel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlPrescriptionGrid.SuspendLayout()
        Me.pnlprescriptionbtn.SuspendLayout()
        Me.pnlRxLabel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlSupervisingProvider.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.pnlMxHistory.SuspendLayout()
        Me.pnlRxHistory.SuspendLayout()
        Me.pnltop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlCenter2
        '
        Me.pnlCenter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCenter2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCenter2.Location = New System.Drawing.Point(0, 0)
        Me.pnlCenter2.Name = "pnlCenter2"
        Me.pnlCenter2.Size = New System.Drawing.Size(1024, 768)
        Me.pnlCenter2.TabIndex = 2
        '
        'pnlcentertop
        '
        Me.pnlcentertop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlcentertop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlcentertop.Location = New System.Drawing.Point(0, 3)
        Me.pnlcentertop.Name = "pnlcentertop"
        Me.pnlcentertop.Size = New System.Drawing.Size(602, 129)
        Me.pnlcentertop.TabIndex = 0
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'cMnuStrp
        '
        Me.cMnuStrp.Name = "cMnuStrp"
        Me.cMnuStrp.Size = New System.Drawing.Size(61, 4)
        '
        'btnAllergies
        '
        Me.btnAllergies.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnAllergies.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAllergies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAllergies.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAllergies.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAllergies.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAllergies.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAllergies.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllergies.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllergies.ForeColor = System.Drawing.Color.Black
        Me.btnAllergies.Image = Global.gloEMR.My.Resources.Resources.Allergies
        Me.btnAllergies.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAllergies.Location = New System.Drawing.Point(505, 0)
        Me.btnAllergies.Name = "btnAllergies"
        Me.btnAllergies.Size = New System.Drawing.Size(26, 24)
        Me.btnAllergies.TabIndex = 50
        Me.ToolTip1.SetToolTip(Me.btnAllergies, "View Allergies")
        Me.btnAllergies.UseVisualStyleBackColor = False
        '
        'pnlAllergiesAlerts
        '
        Me.pnlAllergiesAlerts.AutoSize = True
        Me.pnlAllergiesAlerts.BackColor = System.Drawing.Color.Transparent
        Me.pnlAllergiesAlerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlAllergiesAlerts.Controls.Add(Me.Panel8)
        Me.pnlAllergiesAlerts.Controls.Add(Me.Label40)
        Me.pnlAllergiesAlerts.Controls.Add(Me.pnlPDMP)
        Me.pnlAllergiesAlerts.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlAllergiesAlerts.Location = New System.Drawing.Point(381, 100)
        Me.pnlAllergiesAlerts.Name = "pnlAllergiesAlerts"
        Me.pnlAllergiesAlerts.Size = New System.Drawing.Size(320, 240)
        Me.pnlAllergiesAlerts.TabIndex = 57
        '
        'Panel8
        '
        Me.Panel8.AutoSize = True
        Me.Panel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = CType(resources.GetObject("Panel8.BackgroundImage"), System.Drawing.Image)
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.picBottom)
        Me.Panel8.Controls.Add(Me.lblAlert1)
        Me.Panel8.Controls.Add(Me.picAlertClose1)
        Me.Panel8.Controls.Add(Me.picInfo)
        Me.Panel8.Controls.Add(Me.Label15)
        Me.Panel8.Controls.Add(Me.picTop)
        Me.Panel8.Controls.Add(Me.picMiddle)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(320, 77)
        Me.Panel8.TabIndex = 68
        '
        'picBottom
        '
        Me.picBottom.BackColor = System.Drawing.Color.Transparent
        Me.picBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.picBottom.Image = CType(resources.GetObject("picBottom.Image"), System.Drawing.Image)
        Me.picBottom.Location = New System.Drawing.Point(0, 60)
        Me.picBottom.Name = "picBottom"
        Me.picBottom.Size = New System.Drawing.Size(320, 17)
        Me.picBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBottom.TabIndex = 62
        Me.picBottom.TabStop = False
        '
        'lblAlert1
        '
        Me.lblAlert1.AutoEllipsis = True
        Me.lblAlert1.AutoSize = True
        Me.lblAlert1.BackColor = System.Drawing.Color.Transparent
        Me.lblAlert1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlert1.Location = New System.Drawing.Point(70, 28)
        Me.lblAlert1.Name = "lblAlert1"
        Me.lblAlert1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 20)
        Me.lblAlert1.Size = New System.Drawing.Size(0, 34)
        Me.lblAlert1.TabIndex = 64
        '
        'picAlertClose1
        '
        Me.picAlertClose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picAlertClose1.BackColor = System.Drawing.Color.Transparent
        Me.picAlertClose1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picAlertClose1.Image = CType(resources.GetObject("picAlertClose1.Image"), System.Drawing.Image)
        Me.picAlertClose1.Location = New System.Drawing.Point(287, 7)
        Me.picAlertClose1.Name = "picAlertClose1"
        Me.picAlertClose1.Size = New System.Drawing.Size(20, 20)
        Me.picAlertClose1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picAlertClose1.TabIndex = 65
        Me.picAlertClose1.TabStop = False
        '
        'picInfo
        '
        Me.picInfo.BackColor = System.Drawing.Color.Transparent
        Me.picInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.picInfo.Image = CType(resources.GetObject("picInfo.Image"), System.Drawing.Image)
        Me.picInfo.Location = New System.Drawing.Point(26, 7)
        Me.picInfo.Name = "picInfo"
        Me.picInfo.Size = New System.Drawing.Size(32, 32)
        Me.picInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picInfo.TabIndex = 67
        Me.picInfo.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(70, 8)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 17)
        Me.Label15.TabIndex = 66
        Me.Label15.Text = "Allergies"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picTop
        '
        Me.picTop.BackColor = System.Drawing.Color.Transparent
        Me.picTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.picTop.Image = CType(resources.GetObject("picTop.Image"), System.Drawing.Image)
        Me.picTop.Location = New System.Drawing.Point(0, 0)
        Me.picTop.Name = "picTop"
        Me.picTop.Size = New System.Drawing.Size(320, 16)
        Me.picTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picTop.TabIndex = 61
        Me.picTop.TabStop = False
        '
        'picMiddle
        '
        Me.picMiddle.BackColor = System.Drawing.Color.Transparent
        Me.picMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picMiddle.Image = CType(resources.GetObject("picMiddle.Image"), System.Drawing.Image)
        Me.picMiddle.Location = New System.Drawing.Point(0, 0)
        Me.picMiddle.Name = "picMiddle"
        Me.picMiddle.Size = New System.Drawing.Size(320, 77)
        Me.picMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMiddle.TabIndex = 63
        Me.picMiddle.TabStop = False
        '
        'Label40
        '
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label40.Location = New System.Drawing.Point(0, 77)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(320, 3)
        Me.Label40.TabIndex = 68
        '
        'pnlPDMP
        '
        Me.pnlPDMP.BackColor = System.Drawing.Color.Transparent
        Me.pnlPDMP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPDMP.Controls.Add(Me.Label43)
        Me.pnlPDMP.Controls.Add(Me.Label42)
        Me.pnlPDMP.Controls.Add(Me.Label41)
        Me.pnlPDMP.Controls.Add(Me.elementHostPDMP)
        Me.pnlPDMP.Controls.Add(Me.Panel10)
        Me.pnlPDMP.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPDMP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPDMP.Location = New System.Drawing.Point(0, 80)
        Me.pnlPDMP.Name = "pnlPDMP"
        Me.pnlPDMP.Size = New System.Drawing.Size(320, 160)
        Me.pnlPDMP.TabIndex = 69
        Me.pnlPDMP.Visible = False
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(319, 27)
        Me.Label43.Name = "Label43"
        Me.Label43.Padding = New System.Windows.Forms.Padding(5)
        Me.Label43.Size = New System.Drawing.Size(1, 132)
        Me.Label43.TabIndex = 74
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label42.Location = New System.Drawing.Point(1, 159)
        Me.Label42.Name = "Label42"
        Me.Label42.Padding = New System.Windows.Forms.Padding(5)
        Me.Label42.Size = New System.Drawing.Size(319, 1)
        Me.Label42.TabIndex = 73
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.Location = New System.Drawing.Point(0, 27)
        Me.Label41.Name = "Label41"
        Me.Label41.Padding = New System.Windows.Forms.Padding(5)
        Me.Label41.Size = New System.Drawing.Size(1, 133)
        Me.Label41.TabIndex = 72
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'elementHostPDMP
        '
        Me.elementHostPDMP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.elementHostPDMP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.elementHostPDMP.Location = New System.Drawing.Point(0, 27)
        Me.elementHostPDMP.Name = "elementHostPDMP"
        Me.elementHostPDMP.Size = New System.Drawing.Size(320, 133)
        Me.elementHostPDMP.TabIndex = 71
        Me.elementHostPDMP.Text = "ElementHost1"
        Me.elementHostPDMP.Child = Nothing
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Transparent
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Panel11)
        Me.Panel10.Controls.Add(Me.Panel12)
        Me.Panel10.Controls.Add(Me.Panel17)
        Me.Panel10.Controls.Add(Me.Label39)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(320, 27)
        Me.Panel10.TabIndex = 70
        '
        'Panel11
        '
        Me.Panel11.BackgroundImage = CType(resources.GetObject("Panel11.BackgroundImage"), System.Drawing.Image)
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.lblPDMP)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(11, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(298, 26)
        Me.Panel11.TabIndex = 16
        '
        'lblPDMP
        '
        Me.lblPDMP.BackColor = System.Drawing.Color.Transparent
        Me.lblPDMP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPDMP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPDMP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPDMP.Location = New System.Drawing.Point(0, 0)
        Me.lblPDMP.Name = "lblPDMP"
        Me.lblPDMP.Padding = New System.Windows.Forms.Padding(5)
        Me.lblPDMP.Size = New System.Drawing.Size(298, 26)
        Me.lblPDMP.TabIndex = 10
        Me.lblPDMP.Text = "Prescription drug monitoring programs"
        Me.lblPDMP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel12
        '
        Me.Panel12.BackgroundImage = CType(resources.GetObject("Panel12.BackgroundImage"), System.Drawing.Image)
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel12.Location = New System.Drawing.Point(309, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(11, 26)
        Me.Panel12.TabIndex = 18
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Transparent
        Me.Panel17.BackgroundImage = CType(resources.GetObject("Panel17.BackgroundImage"), System.Drawing.Image)
        Me.Panel17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel17.Location = New System.Drawing.Point(0, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(11, 26)
        Me.Panel17.TabIndex = 17
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label39.Location = New System.Drawing.Point(0, 26)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(320, 1)
        Me.Label39.TabIndex = 67
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDIProgress
        '
        Me.pnlDIProgress.BackColor = System.Drawing.Color.White
        Me.pnlDIProgress.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlDIProgress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDIProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDIProgress.Controls.Add(Me.Label28)
        Me.pnlDIProgress.Controls.Add(Me.Label35)
        Me.pnlDIProgress.Location = New System.Drawing.Point(510, 303)
        Me.pnlDIProgress.Name = "pnlDIProgress"
        Me.pnlDIProgress.Size = New System.Drawing.Size(301, 80)
        Me.pnlDIProgress.TabIndex = 1
        Me.pnlDIProgress.Visible = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(20, 15)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(119, 19)
        Me.Label28.TabIndex = 61
        Me.Label28.Text = "Please wait..."
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label35.Location = New System.Drawing.Point(20, 46)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(279, 23)
        Me.Label35.TabIndex = 61
        Me.Label35.Text = "Performing Drug Interaction Screening… "
        '
        'pnlWait
        '
        Me.pnlWait.BackColor = System.Drawing.Color.Transparent
        Me.pnlWait.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlWait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlWait.Controls.Add(Me.Label12)
        Me.pnlWait.Controls.Add(Me.Label27)
        Me.pnlWait.Location = New System.Drawing.Point(335, 396)
        Me.pnlWait.Name = "pnlWait"
        Me.pnlWait.Size = New System.Drawing.Size(423, 80)
        Me.pnlWait.TabIndex = 66
        Me.pnlWait.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(20, 7)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(119, 19)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "Please wait..."
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label27.Location = New System.Drawing.Point(21, 33)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(177, 16)
        Me.Label27.TabIndex = 61
        Me.Label27.Text = "Updating Exam Template..."
        '
        'pnlElementHostCopay
        '
        Me.pnlElementHostCopay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlElementHostCopay.BackColor = System.Drawing.Color.Transparent
        Me.pnlElementHostCopay.Controls.Add(Me.Label11)
        Me.pnlElementHostCopay.Controls.Add(Me.Label10)
        Me.pnlElementHostCopay.Controls.Add(Me.Label6)
        Me.pnlElementHostCopay.Controls.Add(Me.elementHostCopay)
        Me.pnlElementHostCopay.Controls.Add(Me.Panel9)
        Me.pnlElementHostCopay.Location = New System.Drawing.Point(156, 295)
        Me.pnlElementHostCopay.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlElementHostCopay.Name = "pnlElementHostCopay"
        Me.pnlElementHostCopay.Size = New System.Drawing.Size(680, 237)
        Me.pnlElementHostCopay.TabIndex = 65
        Me.pnlElementHostCopay.Visible = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(1, 236)
        Me.Label11.Name = "Label11"
        Me.Label11.Padding = New System.Windows.Forms.Padding(5)
        Me.Label11.Size = New System.Drawing.Size(678, 1)
        Me.Label11.TabIndex = 71
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(679, 27)
        Me.Label10.Name = "Label10"
        Me.Label10.Padding = New System.Windows.Forms.Padding(5)
        Me.Label10.Size = New System.Drawing.Size(1, 210)
        Me.Label10.TabIndex = 70
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(0, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(5)
        Me.Label6.Size = New System.Drawing.Size(1, 210)
        Me.Label6.TabIndex = 69
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'elementHostCopay
        '
        Me.elementHostCopay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.elementHostCopay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.elementHostCopay.Location = New System.Drawing.Point(0, 27)
        Me.elementHostCopay.Name = "elementHostCopay"
        Me.elementHostCopay.Size = New System.Drawing.Size(680, 210)
        Me.elementHostCopay.TabIndex = 68
        Me.elementHostCopay.Text = "ElementHost1"
        Me.elementHostCopay.Child = Nothing
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Panel14)
        Me.Panel9.Controls.Add(Me.Panel16)
        Me.Panel9.Controls.Add(Me.Panel15)
        Me.Panel9.Controls.Add(Me.Label36)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(680, 27)
        Me.Panel9.TabIndex = 25
        '
        'Panel14
        '
        Me.Panel14.BackgroundImage = CType(resources.GetObject("Panel14.BackgroundImage"), System.Drawing.Image)
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.lblCoverageCopayHeading)
        Me.Panel14.Controls.Add(Me.btnCloseCopayCoverage)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel14.Location = New System.Drawing.Point(11, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(658, 26)
        Me.Panel14.TabIndex = 16
        '
        'lblCoverageCopayHeading
        '
        Me.lblCoverageCopayHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblCoverageCopayHeading.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCoverageCopayHeading.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCoverageCopayHeading.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCoverageCopayHeading.Location = New System.Drawing.Point(0, 0)
        Me.lblCoverageCopayHeading.Name = "lblCoverageCopayHeading"
        Me.lblCoverageCopayHeading.Padding = New System.Windows.Forms.Padding(5)
        Me.lblCoverageCopayHeading.Size = New System.Drawing.Size(637, 26)
        Me.lblCoverageCopayHeading.TabIndex = 10
        Me.lblCoverageCopayHeading.Text = "Copay Information"
        Me.lblCoverageCopayHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCloseCopayCoverage
        '
        Me.btnCloseCopayCoverage.BackColor = System.Drawing.Color.Transparent
        Me.btnCloseCopayCoverage.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCloseCopayCoverage.FlatAppearance.BorderSize = 0
        Me.btnCloseCopayCoverage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCloseCopayCoverage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCloseCopayCoverage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseCopayCoverage.Image = CType(resources.GetObject("btnCloseCopayCoverage.Image"), System.Drawing.Image)
        Me.btnCloseCopayCoverage.Location = New System.Drawing.Point(637, 0)
        Me.btnCloseCopayCoverage.Name = "btnCloseCopayCoverage"
        Me.btnCloseCopayCoverage.Size = New System.Drawing.Size(21, 26)
        Me.btnCloseCopayCoverage.TabIndex = 0
        Me.btnCloseCopayCoverage.UseVisualStyleBackColor = False
        '
        'Panel16
        '
        Me.Panel16.BackgroundImage = CType(resources.GetObject("Panel16.BackgroundImage"), System.Drawing.Image)
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel16.Location = New System.Drawing.Point(669, 0)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(11, 26)
        Me.Panel16.TabIndex = 18
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.Transparent
        Me.Panel15.BackgroundImage = CType(resources.GetObject("Panel15.BackgroundImage"), System.Drawing.Image)
        Me.Panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel15.Location = New System.Drawing.Point(0, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(11, 26)
        Me.Panel15.TabIndex = 17
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label36.Location = New System.Drawing.Point(0, 26)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(680, 1)
        Me.Label36.TabIndex = 67
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlFormulary
        '
        Me.pnlFormulary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFormulary.BackColor = System.Drawing.Color.Transparent
        Me.pnlFormulary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFormulary.Controls.Add(Me.elementHost)
        Me.pnlFormulary.Controls.Add(Me.pnlFormularyDrugName)
        Me.pnlFormulary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlFormulary.Location = New System.Drawing.Point(156, 100)
        Me.pnlFormulary.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlFormulary.Name = "pnlFormulary"
        Me.pnlFormulary.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlFormulary.Size = New System.Drawing.Size(680, 192)
        Me.pnlFormulary.TabIndex = 58
        Me.pnlFormulary.Visible = False
        '
        'elementHost
        '
        Me.elementHost.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.elementHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.elementHost.Location = New System.Drawing.Point(0, 27)
        Me.elementHost.Name = "elementHost"
        Me.elementHost.Size = New System.Drawing.Size(680, 162)
        Me.elementHost.TabIndex = 67
        Me.elementHost.Text = "ElementHost1"
        Me.elementHost.Child = Nothing
        '
        'pnlFormularyDrugName
        '
        Me.pnlFormularyDrugName.BackColor = System.Drawing.Color.Transparent
        Me.pnlFormularyDrugName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFormularyDrugName.Controls.Add(Me.Panel13)
        Me.pnlFormularyDrugName.Controls.Add(Me.Panel3)
        Me.pnlFormularyDrugName.Controls.Add(Me.Panel6)
        Me.pnlFormularyDrugName.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFormularyDrugName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlFormularyDrugName.Location = New System.Drawing.Point(0, 0)
        Me.pnlFormularyDrugName.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlFormularyDrugName.Name = "pnlFormularyDrugName"
        Me.pnlFormularyDrugName.Size = New System.Drawing.Size(680, 27)
        Me.pnlFormularyDrugName.TabIndex = 4
        '
        'Panel13
        '
        Me.Panel13.BackgroundImage = CType(resources.GetObject("Panel13.BackgroundImage"), System.Drawing.Image)
        Me.Panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel13.Controls.Add(Me.lblFormularyDrugName)
        Me.Panel13.Controls.Add(Me.btnFormularyGridClose)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(11, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(658, 27)
        Me.Panel13.TabIndex = 21
        '
        'lblFormularyDrugName
        '
        Me.lblFormularyDrugName.BackColor = System.Drawing.Color.Transparent
        Me.lblFormularyDrugName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblFormularyDrugName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormularyDrugName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFormularyDrugName.Location = New System.Drawing.Point(0, 0)
        Me.lblFormularyDrugName.Name = "lblFormularyDrugName"
        Me.lblFormularyDrugName.Padding = New System.Windows.Forms.Padding(5)
        Me.lblFormularyDrugName.Size = New System.Drawing.Size(628, 27)
        Me.lblFormularyDrugName.TabIndex = 9
        Me.lblFormularyDrugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFormularyGridClose
        '
        Me.btnFormularyGridClose.BackColor = System.Drawing.Color.Transparent
        Me.btnFormularyGridClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnFormularyGridClose.FlatAppearance.BorderSize = 0
        Me.btnFormularyGridClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFormularyGridClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFormularyGridClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFormularyGridClose.Image = CType(resources.GetObject("btnFormularyGridClose.Image"), System.Drawing.Image)
        Me.btnFormularyGridClose.Location = New System.Drawing.Point(628, 0)
        Me.btnFormularyGridClose.Name = "btnFormularyGridClose"
        Me.btnFormularyGridClose.Size = New System.Drawing.Size(30, 27)
        Me.btnFormularyGridClose.TabIndex = 10
        Me.btnFormularyGridClose.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(669, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(11, 27)
        Me.Panel3.TabIndex = 20
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.BackgroundImage = CType(resources.GetObject("Panel6.BackgroundImage"), System.Drawing.Image)
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(11, 27)
        Me.Panel6.TabIndex = 19
        '
        'pnlFormularyTransactionMessage
        '
        Me.pnlFormularyTransactionMessage.BackColor = System.Drawing.Color.Transparent
        Me.pnlFormularyTransactionMessage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlFormularyTransactionMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFormularyTransactionMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFormularyTransactionMessage.Controls.Add(Me.Label24)
        Me.pnlFormularyTransactionMessage.Controls.Add(Me.lblFormularyTransactionMessage)
        Me.pnlFormularyTransactionMessage.Location = New System.Drawing.Point(335, 306)
        Me.pnlFormularyTransactionMessage.Name = "pnlFormularyTransactionMessage"
        Me.pnlFormularyTransactionMessage.Size = New System.Drawing.Size(423, 80)
        Me.pnlFormularyTransactionMessage.TabIndex = 60
        Me.pnlFormularyTransactionMessage.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(20, 7)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(119, 19)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Please wait..."
        '
        'lblFormularyTransactionMessage
        '
        Me.lblFormularyTransactionMessage.AutoSize = True
        Me.lblFormularyTransactionMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblFormularyTransactionMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormularyTransactionMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFormularyTransactionMessage.Location = New System.Drawing.Point(21, 33)
        Me.lblFormularyTransactionMessage.Name = "lblFormularyTransactionMessage"
        Me.lblFormularyTransactionMessage.Size = New System.Drawing.Size(214, 16)
        Me.lblFormularyTransactionMessage.TabIndex = 61
        Me.lblFormularyTransactionMessage.Text = "Sending eligibility information… "
        '
        'pnlFormularyProgress
        '
        Me.pnlFormularyProgress.BackColor = System.Drawing.Color.White
        Me.pnlFormularyProgress.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlFormularyProgress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFormularyProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFormularyProgress.Controls.Add(Me.Label21)
        Me.pnlFormularyProgress.Controls.Add(Me.Label20)
        Me.pnlFormularyProgress.Location = New System.Drawing.Point(592, 303)
        Me.pnlFormularyProgress.Name = "pnlFormularyProgress"
        Me.pnlFormularyProgress.Size = New System.Drawing.Size(280, 80)
        Me.pnlFormularyProgress.TabIndex = 0
        Me.pnlFormularyProgress.Visible = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(20, 17)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(119, 19)
        Me.Label21.TabIndex = 61
        Me.Label21.Text = "Please wait..."
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(21, 52)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(237, 16)
        Me.Label20.TabIndex = 61
        Me.Label20.Text = "Retrieving formulary information… "
        '
        'pnlFormularyCoverage
        '
        Me.pnlFormularyCoverage.BackColor = System.Drawing.Color.Transparent
        Me.pnlFormularyCoverage.Controls.Add(Me.Panel7)
        Me.pnlFormularyCoverage.Controls.Add(Me.Panel5)
        Me.pnlFormularyCoverage.Controls.Add(Me.pnlFormularyCoverageHeading)
        Me.pnlFormularyCoverage.Location = New System.Drawing.Point(253, 300)
        Me.pnlFormularyCoverage.Name = "pnlFormularyCoverage"
        Me.pnlFormularyCoverage.Size = New System.Drawing.Size(402, 390)
        Me.pnlFormularyCoverage.TabIndex = 59
        Me.pnlFormularyCoverage.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImage = CType(resources.GetObject("Panel7.BackgroundImage"), System.Drawing.Image)
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 376)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(402, 14)
        Me.Panel7.TabIndex = 27
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.rtfFormularyDescription)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Controls.Add(Me.Label18)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(0, 28)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(402, 362)
        Me.Panel5.TabIndex = 26
        '
        'rtfFormularyDescription
        '
        Me.rtfFormularyDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.rtfFormularyDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtfFormularyDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtfFormularyDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtfFormularyDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rtfFormularyDescription.Location = New System.Drawing.Point(4, 5)
        Me.rtfFormularyDescription.Name = "rtfFormularyDescription"
        Me.rtfFormularyDescription.ReadOnly = True
        Me.rtfFormularyDescription.Size = New System.Drawing.Size(397, 356)
        Me.rtfFormularyDescription.TabIndex = 1
        Me.rtfFormularyDescription.Text = ""
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.White
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 5)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(3, 356)
        Me.Label19.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(1, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(400, 4)
        Me.Label13.TabIndex = 9
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 361)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(400, 1)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 361)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(401, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 361)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(402, 1)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "label1"
        '
        'pnlFormularyCoverageHeading
        '
        Me.pnlFormularyCoverageHeading.BackColor = System.Drawing.Color.Transparent
        Me.pnlFormularyCoverageHeading.BackgroundImage = CType(resources.GetObject("pnlFormularyCoverageHeading.BackgroundImage"), System.Drawing.Image)
        Me.pnlFormularyCoverageHeading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFormularyCoverageHeading.Controls.Add(Me.btnFormularyCovPnlClose)
        Me.pnlFormularyCoverageHeading.Controls.Add(Me.lblAlternativeDrugName)
        Me.pnlFormularyCoverageHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFormularyCoverageHeading.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlFormularyCoverageHeading.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlFormularyCoverageHeading.Location = New System.Drawing.Point(0, 0)
        Me.pnlFormularyCoverageHeading.Name = "pnlFormularyCoverageHeading"
        Me.pnlFormularyCoverageHeading.Size = New System.Drawing.Size(402, 28)
        Me.pnlFormularyCoverageHeading.TabIndex = 25
        '
        'btnFormularyCovPnlClose
        '
        Me.btnFormularyCovPnlClose.BackColor = System.Drawing.Color.Transparent
        Me.btnFormularyCovPnlClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnFormularyCovPnlClose.FlatAppearance.BorderSize = 0
        Me.btnFormularyCovPnlClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFormularyCovPnlClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFormularyCovPnlClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFormularyCovPnlClose.Image = CType(resources.GetObject("btnFormularyCovPnlClose.Image"), System.Drawing.Image)
        Me.btnFormularyCovPnlClose.Location = New System.Drawing.Point(358, 0)
        Me.btnFormularyCovPnlClose.Name = "btnFormularyCovPnlClose"
        Me.btnFormularyCovPnlClose.Size = New System.Drawing.Size(44, 28)
        Me.btnFormularyCovPnlClose.TabIndex = 0
        Me.btnFormularyCovPnlClose.UseVisualStyleBackColor = False
        '
        'lblAlternativeDrugName
        '
        Me.lblAlternativeDrugName.AutoSize = True
        Me.lblAlternativeDrugName.BackColor = System.Drawing.Color.Transparent
        Me.lblAlternativeDrugName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlternativeDrugName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAlternativeDrugName.Location = New System.Drawing.Point(24, 1)
        Me.lblAlternativeDrugName.Name = "lblAlternativeDrugName"
        Me.lblAlternativeDrugName.Padding = New System.Windows.Forms.Padding(5)
        Me.lblAlternativeDrugName.Size = New System.Drawing.Size(61, 24)
        Me.lblAlternativeDrugName.TabIndex = 10
        Me.lblAlternativeDrugName.Text = "           "
        Me.lblAlternativeDrugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlViewMedicationHistory
        '
        Me.pnlViewMedicationHistory.BackColor = System.Drawing.Color.Transparent
        Me.pnlViewMedicationHistory.Controls.Add(Me.pnlMedicationHistory)
        Me.pnlViewMedicationHistory.Controls.Add(Me.pnlMedicationHistoryHeader)
        Me.pnlViewMedicationHistory.Controls.Add(Me.pnlMedicationHistoryFooter)
        Me.pnlViewMedicationHistory.Location = New System.Drawing.Point(53, 117)
        Me.pnlViewMedicationHistory.Name = "pnlViewMedicationHistory"
        Me.pnlViewMedicationHistory.Size = New System.Drawing.Size(918, 534)
        Me.pnlViewMedicationHistory.TabIndex = 63
        '
        'pnlMedicationHistory
        '
        Me.pnlMedicationHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMedicationHistory.Location = New System.Drawing.Point(0, 28)
        Me.pnlMedicationHistory.Name = "pnlMedicationHistory"
        Me.pnlMedicationHistory.Size = New System.Drawing.Size(918, 492)
        Me.pnlMedicationHistory.TabIndex = 26
        '
        'pnlMedicationHistoryHeader
        '
        Me.pnlMedicationHistoryHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlMedicationHistoryHeader.BackgroundImage = CType(resources.GetObject("pnlMedicationHistoryHeader.BackgroundImage"), System.Drawing.Image)
        Me.pnlMedicationHistoryHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMedicationHistoryHeader.Controls.Add(Me.btnViewMedicationHistoryClose)
        Me.pnlMedicationHistoryHeader.Controls.Add(Me.Label25)
        Me.pnlMedicationHistoryHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMedicationHistoryHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMedicationHistoryHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMedicationHistoryHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlMedicationHistoryHeader.Name = "pnlMedicationHistoryHeader"
        Me.pnlMedicationHistoryHeader.Size = New System.Drawing.Size(918, 28)
        Me.pnlMedicationHistoryHeader.TabIndex = 25
        '
        'btnViewMedicationHistoryClose
        '
        Me.btnViewMedicationHistoryClose.BackColor = System.Drawing.Color.Transparent
        Me.btnViewMedicationHistoryClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnViewMedicationHistoryClose.FlatAppearance.BorderSize = 0
        Me.btnViewMedicationHistoryClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnViewMedicationHistoryClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnViewMedicationHistoryClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewMedicationHistoryClose.Image = CType(resources.GetObject("btnViewMedicationHistoryClose.Image"), System.Drawing.Image)
        Me.btnViewMedicationHistoryClose.Location = New System.Drawing.Point(874, 0)
        Me.btnViewMedicationHistoryClose.Name = "btnViewMedicationHistoryClose"
        Me.btnViewMedicationHistoryClose.Size = New System.Drawing.Size(44, 28)
        Me.btnViewMedicationHistoryClose.TabIndex = 0
        Me.btnViewMedicationHistoryClose.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(24, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Padding = New System.Windows.Forms.Padding(5)
        Me.Label25.Size = New System.Drawing.Size(176, 24)
        Me.Label25.TabIndex = 10
        Me.Label25.Text = "  Medication History         "
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMedicationHistoryFooter
        '
        Me.pnlMedicationHistoryFooter.BackColor = System.Drawing.Color.Transparent
        Me.pnlMedicationHistoryFooter.BackgroundImage = CType(resources.GetObject("pnlMedicationHistoryFooter.BackgroundImage"), System.Drawing.Image)
        Me.pnlMedicationHistoryFooter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMedicationHistoryFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlMedicationHistoryFooter.Location = New System.Drawing.Point(0, 520)
        Me.pnlMedicationHistoryFooter.Name = "pnlMedicationHistoryFooter"
        Me.pnlMedicationHistoryFooter.Size = New System.Drawing.Size(918, 14)
        Me.pnlMedicationHistoryFooter.TabIndex = 28
        '
        'pnlViewDocument
        '
        Me.pnlViewDocument.BackColor = System.Drawing.Color.Transparent
        Me.pnlViewDocument.Controls.Add(Me.pnlDocument)
        Me.pnlViewDocument.Controls.Add(Me.pnlDMSHeader)
        Me.pnlViewDocument.Controls.Add(Me.pnlViewDocumentFooter)
        Me.pnlViewDocument.Location = New System.Drawing.Point(124, 102)
        Me.pnlViewDocument.Name = "pnlViewDocument"
        Me.pnlViewDocument.Size = New System.Drawing.Size(776, 565)
        Me.pnlViewDocument.TabIndex = 64
        Me.pnlViewDocument.Visible = False
        '
        'pnlDocument
        '
        Me.pnlDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDocument.Location = New System.Drawing.Point(0, 28)
        Me.pnlDocument.Name = "pnlDocument"
        Me.pnlDocument.Size = New System.Drawing.Size(776, 523)
        Me.pnlDocument.TabIndex = 26
        '
        'pnlDMSHeader
        '
        Me.pnlDMSHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlDMSHeader.BackgroundImage = CType(resources.GetObject("pnlDMSHeader.BackgroundImage"), System.Drawing.Image)
        Me.pnlDMSHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDMSHeader.Controls.Add(Me.btnViewDocumentClose)
        Me.pnlDMSHeader.Controls.Add(Me.Label33)
        Me.pnlDMSHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDMSHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDMSHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlDMSHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlDMSHeader.Name = "pnlDMSHeader"
        Me.pnlDMSHeader.Size = New System.Drawing.Size(776, 28)
        Me.pnlDMSHeader.TabIndex = 25
        '
        'btnViewDocumentClose
        '
        Me.btnViewDocumentClose.BackColor = System.Drawing.Color.Transparent
        Me.btnViewDocumentClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnViewDocumentClose.FlatAppearance.BorderSize = 0
        Me.btnViewDocumentClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnViewDocumentClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnViewDocumentClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewDocumentClose.Image = CType(resources.GetObject("btnViewDocumentClose.Image"), System.Drawing.Image)
        Me.btnViewDocumentClose.Location = New System.Drawing.Point(732, 0)
        Me.btnViewDocumentClose.Name = "btnViewDocumentClose"
        Me.btnViewDocumentClose.Size = New System.Drawing.Size(44, 28)
        Me.btnViewDocumentClose.TabIndex = 0
        Me.btnViewDocumentClose.UseVisualStyleBackColor = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label33.Location = New System.Drawing.Point(24, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Padding = New System.Windows.Forms.Padding(5)
        Me.Label33.Size = New System.Drawing.Size(217, 24)
        Me.Label33.TabIndex = 10
        Me.Label33.Text = "    Medication Reconciliation       "
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlViewDocumentFooter
        '
        Me.pnlViewDocumentFooter.BackColor = System.Drawing.Color.Transparent
        Me.pnlViewDocumentFooter.BackgroundImage = CType(resources.GetObject("pnlViewDocumentFooter.BackgroundImage"), System.Drawing.Image)
        Me.pnlViewDocumentFooter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlViewDocumentFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlViewDocumentFooter.Location = New System.Drawing.Point(0, 551)
        Me.pnlViewDocumentFooter.Name = "pnlViewDocumentFooter"
        Me.pnlViewDocumentFooter.Size = New System.Drawing.Size(776, 14)
        Me.pnlViewDocumentFooter.TabIndex = 29
        '
        'pnlcenter
        '
        Me.pnlcenter.AutoScroll = True
        Me.pnlcenter.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlcenter.AutoSize = True
        Me.pnlcenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlcenter.Controls.Add(Me.pnlFlexGrid)
        Me.pnlcenter.Controls.Add(Me.pnlmonograph)
        Me.pnlcenter.Controls.Add(Me.pnlDIScreenResult)
        Me.pnlcenter.Controls.Add(Me.splleft)
        Me.pnlcenter.Controls.Add(Me.pnlleft)
        Me.pnlcenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlcenter.Location = New System.Drawing.Point(0, 55)
        Me.pnlcenter.Name = "pnlcenter"
        Me.pnlcenter.Size = New System.Drawing.Size(767, 687)
        Me.pnlcenter.TabIndex = 5
        '
        'pnlFlexGrid
        '
        Me.pnlFlexGrid.AutoScroll = True
        Me.pnlFlexGrid.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlFlexGrid.Controls.Add(Me.pnlRxMx)
        Me.pnlFlexGrid.Controls.Add(Me.pnlSupervisingProvider)
        Me.pnlFlexGrid.Controls.Add(Me.sptRefill)
        Me.pnlFlexGrid.Controls.Add(Me.pnlRefill)
        Me.pnlFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFlexGrid.Location = New System.Drawing.Point(236, 0)
        Me.pnlFlexGrid.Name = "pnlFlexGrid"
        Me.pnlFlexGrid.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlFlexGrid.Size = New System.Drawing.Size(531, 687)
        Me.pnlFlexGrid.TabIndex = 1
        '
        'pnlRxMx
        '
        Me.pnlRxMx.Controls.Add(Me.pnlMedicationGrid)
        Me.pnlRxMx.Controls.Add(Me.sptRxMx)
        Me.pnlRxMx.Controls.Add(Me.pnlPrescriptionGrid)
        Me.pnlRxMx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRxMx.Location = New System.Drawing.Point(0, 34)
        Me.pnlRxMx.Name = "pnlRxMx"
        Me.pnlRxMx.Size = New System.Drawing.Size(531, 327)
        Me.pnlRxMx.TabIndex = 9
        '
        'pnlMedicationGrid
        '
        Me.pnlMedicationGrid.Controls.Add(Me.pnlMedicationDetails)
        Me.pnlMedicationGrid.Controls.Add(Me.btnMedication)
        Me.pnlMedicationGrid.Controls.Add(Me.pnlMxLabel)
        Me.pnlMedicationGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMedicationGrid.Location = New System.Drawing.Point(0, 192)
        Me.pnlMedicationGrid.Name = "pnlMedicationGrid"
        Me.pnlMedicationGrid.Size = New System.Drawing.Size(531, 135)
        Me.pnlMedicationGrid.TabIndex = 7
        '
        'pnlMedicationDetails
        '
        Me.pnlMedicationDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMedicationDetails.Location = New System.Drawing.Point(0, 24)
        Me.pnlMedicationDetails.Name = "pnlMedicationDetails"
        Me.pnlMedicationDetails.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlMedicationDetails.Size = New System.Drawing.Size(531, 111)
        Me.pnlMedicationDetails.TabIndex = 5
        '
        'btnMedication
        '
        Me.btnMedication.BackColor = System.Drawing.Color.Transparent
        Me.btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMedication.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMedication.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnMedication.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMedication.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMedication.ForeColor = System.Drawing.Color.Black
        Me.btnMedication.Location = New System.Drawing.Point(0, 0)
        Me.btnMedication.Name = "btnMedication"
        Me.btnMedication.Size = New System.Drawing.Size(531, 24)
        Me.btnMedication.TabIndex = 18
        Me.btnMedication.Text = "  Medication History"
        Me.btnMedication.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMedication.UseVisualStyleBackColor = False
        '
        'pnlMxLabel
        '
        Me.pnlMxLabel.BackColor = System.Drawing.Color.Transparent
        Me.pnlMxLabel.Controls.Add(Me.Panel2)
        Me.pnlMxLabel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMxLabel.Location = New System.Drawing.Point(36, 93)
        Me.pnlMxLabel.Name = "pnlMxLabel"
        Me.pnlMxLabel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlMxLabel.Size = New System.Drawing.Size(19, 26)
        Me.pnlMxLabel.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lblMedicationGridLabel)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.btnMxDown)
        Me.Panel2.Controls.Add(Me.btnMxUp)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 23)
        Me.Panel2.TabIndex = 4
        '
        'lblMedicationGridLabel
        '
        Me.lblMedicationGridLabel.AutoSize = True
        Me.lblMedicationGridLabel.BackColor = System.Drawing.Color.Transparent
        Me.lblMedicationGridLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMedicationGridLabel.Location = New System.Drawing.Point(1, 1)
        Me.lblMedicationGridLabel.Name = "lblMedicationGridLabel"
        Me.lblMedicationGridLabel.Padding = New System.Windows.Forms.Padding(2)
        Me.lblMedicationGridLabel.Size = New System.Drawing.Size(86, 18)
        Me.lblMedicationGridLabel.TabIndex = 0
        Me.lblMedicationGridLabel.Text = "  Medication"
        Me.lblMedicationGridLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(0, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 21)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "label2"
        '
        'btnMxDown
        '
        Me.btnMxDown.BackColor = System.Drawing.Color.Transparent
        Me.btnMxDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMxDown.FlatAppearance.BorderSize = 0
        Me.btnMxDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMxDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnMxDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMxDown.Image = Global.gloEMR.My.Resources.Resources.Down
        Me.btnMxDown.Location = New System.Drawing.Point(-32, 1)
        Me.btnMxDown.Name = "btnMxDown"
        Me.btnMxDown.Size = New System.Drawing.Size(25, 21)
        Me.btnMxDown.TabIndex = 1
        Me.btnMxDown.UseVisualStyleBackColor = False
        Me.btnMxDown.Visible = False
        '
        'btnMxUp
        '
        Me.btnMxUp.BackColor = System.Drawing.Color.Transparent
        Me.btnMxUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMxUp.FlatAppearance.BorderSize = 0
        Me.btnMxUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMxUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnMxUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMxUp.Image = Global.gloEMR.My.Resources.Resources.UP
        Me.btnMxUp.Location = New System.Drawing.Point(-7, 1)
        Me.btnMxUp.Name = "btnMxUp"
        Me.btnMxUp.Size = New System.Drawing.Size(25, 21)
        Me.btnMxUp.TabIndex = 2
        Me.btnMxUp.UseVisualStyleBackColor = False
        Me.btnMxUp.Visible = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(18, 1)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(0, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(18, 1)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(18, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 23)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "label2"
        '
        'sptRxMx
        '
        Me.sptRxMx.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.sptRxMx.Dock = System.Windows.Forms.DockStyle.Top
        Me.sptRxMx.Location = New System.Drawing.Point(0, 189)
        Me.sptRxMx.Name = "sptRxMx"
        Me.sptRxMx.Size = New System.Drawing.Size(531, 3)
        Me.sptRxMx.TabIndex = 9
        Me.sptRxMx.TabStop = False
        '
        'pnlPrescriptionGrid
        '
        Me.pnlPrescriptionGrid.Controls.Add(Me.pnlPrescriptionDetails)
        Me.pnlPrescriptionGrid.Controls.Add(Me.pnlprescriptionbtn)
        Me.pnlPrescriptionGrid.Controls.Add(Me.pnlRxLabel)
        Me.pnlPrescriptionGrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPrescriptionGrid.Location = New System.Drawing.Point(0, 0)
        Me.pnlPrescriptionGrid.Name = "pnlPrescriptionGrid"
        Me.pnlPrescriptionGrid.Size = New System.Drawing.Size(531, 189)
        Me.pnlPrescriptionGrid.TabIndex = 8
        '
        'pnlPrescriptionDetails
        '
        Me.pnlPrescriptionDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPrescriptionDetails.Location = New System.Drawing.Point(0, 24)
        Me.pnlPrescriptionDetails.Name = "pnlPrescriptionDetails"
        Me.pnlPrescriptionDetails.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlPrescriptionDetails.Size = New System.Drawing.Size(531, 165)
        Me.pnlPrescriptionDetails.TabIndex = 4
        '
        'pnlprescriptionbtn
        '
        Me.pnlprescriptionbtn.Controls.Add(Me.Label38)
        Me.pnlprescriptionbtn.Controls.Add(Me.Label37)
        Me.pnlprescriptionbtn.Controls.Add(Me.btnPrescription)
        Me.pnlprescriptionbtn.Controls.Add(Me.Label34)
        Me.pnlprescriptionbtn.Controls.Add(Me.btnAllergies)
        Me.pnlprescriptionbtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlprescriptionbtn.Location = New System.Drawing.Point(0, 0)
        Me.pnlprescriptionbtn.Name = "pnlprescriptionbtn"
        Me.pnlprescriptionbtn.Size = New System.Drawing.Size(531, 24)
        Me.pnlprescriptionbtn.TabIndex = 50
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Location = New System.Drawing.Point(498, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 24)
        Me.Label38.TabIndex = 53
        Me.Label38.Text = "label4"
        '
        'Label37
        '
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label37.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Red
        Me.Label37.Location = New System.Drawing.Point(499, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(3, 24)
        Me.Label37.TabIndex = 52
        Me.Label37.Tag = ""
        '
        'btnPrescription
        '
        Me.btnPrescription.BackColor = System.Drawing.Color.Transparent
        Me.btnPrescription.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnPrescription.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPrescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPrescription.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPrescription.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrescription.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrescription.ForeColor = System.Drawing.Color.Black
        Me.btnPrescription.Location = New System.Drawing.Point(0, 0)
        Me.btnPrescription.Name = "btnPrescription"
        Me.btnPrescription.Size = New System.Drawing.Size(502, 24)
        Me.btnPrescription.TabIndex = 17
        Me.btnPrescription.Text = "  New Prescriptions"
        Me.btnPrescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrescription.UseVisualStyleBackColor = False
        '
        'Label34
        '
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Red
        Me.Label34.Location = New System.Drawing.Point(502, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(3, 24)
        Me.Label34.TabIndex = 51
        Me.Label34.Tag = ""
        '
        'pnlRxLabel
        '
        Me.pnlRxLabel.Controls.Add(Me.Panel1)
        Me.pnlRxLabel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlRxLabel.Location = New System.Drawing.Point(122, 115)
        Me.pnlRxLabel.Name = "pnlRxLabel"
        Me.pnlRxLabel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlRxLabel.Size = New System.Drawing.Size(30, 21)
        Me.pnlRxLabel.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lblPrescriptionGridLabel)
        Me.Panel1.Controls.Add(Me.btnRxDown)
        Me.Panel1.Controls.Add(Me.btnRxUp)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(30, 18)
        Me.Panel1.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 16)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "label2"
        '
        'lblPrescriptionGridLabel
        '
        Me.lblPrescriptionGridLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPrescriptionGridLabel.AutoSize = True
        Me.lblPrescriptionGridLabel.BackColor = System.Drawing.Color.Transparent
        Me.lblPrescriptionGridLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPrescriptionGridLabel.Location = New System.Drawing.Point(3, 1)
        Me.lblPrescriptionGridLabel.Name = "lblPrescriptionGridLabel"
        Me.lblPrescriptionGridLabel.Padding = New System.Windows.Forms.Padding(2)
        Me.lblPrescriptionGridLabel.Size = New System.Drawing.Size(92, 18)
        Me.lblPrescriptionGridLabel.TabIndex = 0
        Me.lblPrescriptionGridLabel.Text = "  Prescription"
        Me.lblPrescriptionGridLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRxDown
        '
        Me.btnRxDown.BackColor = System.Drawing.Color.Transparent
        Me.btnRxDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRxDown.FlatAppearance.BorderSize = 0
        Me.btnRxDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRxDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRxDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRxDown.Image = Global.gloEMR.My.Resources.Resources.Down
        Me.btnRxDown.Location = New System.Drawing.Point(-21, 1)
        Me.btnRxDown.Name = "btnRxDown"
        Me.btnRxDown.Size = New System.Drawing.Size(25, 16)
        Me.btnRxDown.TabIndex = 1
        Me.btnRxDown.UseVisualStyleBackColor = False
        Me.btnRxDown.Visible = False
        '
        'btnRxUp
        '
        Me.btnRxUp.BackColor = System.Drawing.Color.Transparent
        Me.btnRxUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRxUp.FlatAppearance.BorderSize = 0
        Me.btnRxUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRxUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRxUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRxUp.Image = Global.gloEMR.My.Resources.Resources.UP
        Me.btnRxUp.Location = New System.Drawing.Point(4, 1)
        Me.btnRxUp.Name = "btnRxUp"
        Me.btnRxUp.Size = New System.Drawing.Size(25, 16)
        Me.btnRxUp.TabIndex = 2
        Me.btnRxUp.UseVisualStyleBackColor = False
        Me.btnRxUp.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 1)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(0, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 1)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(29, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 18)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "label2"
        '
        'pnlSupervisingProvider
        '
        Me.pnlSupervisingProvider.Controls.Add(Me.Panel4)
        Me.pnlSupervisingProvider.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSupervisingProvider.Location = New System.Drawing.Point(0, 3)
        Me.pnlSupervisingProvider.Name = "pnlSupervisingProvider"
        Me.pnlSupervisingProvider.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSupervisingProvider.Size = New System.Drawing.Size(531, 31)
        Me.pnlSupervisingProvider.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.lblSupervisingValidation)
        Me.Panel4.Controls.Add(Me.Label32)
        Me.Panel4.Controls.Add(Me.Label31)
        Me.Panel4.Controls.Add(Me.Label30)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Controls.Add(Me.cmbSupervisingProvider)
        Me.Panel4.Controls.Add(Me.Label26)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(531, 28)
        Me.Panel4.TabIndex = 0
        '
        'lblSupervisingValidation
        '
        Me.lblSupervisingValidation.AutoSize = True
        Me.lblSupervisingValidation.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupervisingValidation.ForeColor = System.Drawing.Color.Red
        Me.lblSupervisingValidation.Location = New System.Drawing.Point(7, 8)
        Me.lblSupervisingValidation.Name = "lblSupervisingValidation"
        Me.lblSupervisingValidation.Size = New System.Drawing.Size(15, 13)
        Me.lblSupervisingValidation.TabIndex = 12
        Me.lblSupervisingValidation.Tag = ""
        Me.lblSupervisingValidation.Text = "*"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Location = New System.Drawing.Point(1, 27)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(529, 1)
        Me.Label32.TabIndex = 11
        Me.Label32.Text = "label4"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Location = New System.Drawing.Point(1, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(529, 1)
        Me.Label31.TabIndex = 10
        Me.Label31.Text = "label4"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Location = New System.Drawing.Point(530, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 28)
        Me.Label30.TabIndex = 9
        Me.Label30.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 28)
        Me.Label29.TabIndex = 8
        Me.Label29.Text = "label4"
        '
        'cmbSupervisingProvider
        '
        Me.cmbSupervisingProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupervisingProvider.FormattingEnabled = True
        Me.cmbSupervisingProvider.Location = New System.Drawing.Point(163, 3)
        Me.cmbSupervisingProvider.Name = "cmbSupervisingProvider"
        Me.cmbSupervisingProvider.Size = New System.Drawing.Size(218, 21)
        Me.cmbSupervisingProvider.TabIndex = 0
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(22, 7)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(141, 14)
        Me.Label26.TabIndex = 1
        Me.Label26.Text = "Supervising Provider :"
        '
        'sptRefill
        '
        Me.sptRefill.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.sptRefill.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.sptRefill.Enabled = False
        Me.sptRefill.Location = New System.Drawing.Point(0, 361)
        Me.sptRefill.Name = "sptRefill"
        Me.sptRefill.Size = New System.Drawing.Size(531, 3)
        Me.sptRefill.TabIndex = 10
        Me.sptRefill.TabStop = False
        '
        'pnlRefill
        '
        Me.pnlRefill.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlRefill.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlRefill.Location = New System.Drawing.Point(0, 364)
        Me.pnlRefill.Name = "pnlRefill"
        Me.pnlRefill.Size = New System.Drawing.Size(531, 323)
        Me.pnlRefill.TabIndex = 0
        '
        'pnlmonograph
        '
        Me.pnlmonograph.AutoScroll = True
        Me.pnlmonograph.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlmonograph.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlmonograph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlmonograph.Location = New System.Drawing.Point(236, 0)
        Me.pnlmonograph.Name = "pnlmonograph"
        Me.pnlmonograph.Size = New System.Drawing.Size(531, 687)
        Me.pnlmonograph.TabIndex = 1
        '
        'pnlDIScreenResult
        '
        Me.pnlDIScreenResult.AutoScroll = True
        Me.pnlDIScreenResult.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlDIScreenResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlDIScreenResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDIScreenResult.Location = New System.Drawing.Point(236, 0)
        Me.pnlDIScreenResult.Name = "pnlDIScreenResult"
        Me.pnlDIScreenResult.Size = New System.Drawing.Size(531, 687)
        Me.pnlDIScreenResult.TabIndex = 0
        '
        'splleft
        '
        Me.splleft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splleft.Location = New System.Drawing.Point(233, 0)
        Me.splleft.Name = "splleft"
        Me.splleft.Size = New System.Drawing.Size(3, 687)
        Me.splleft.TabIndex = 2
        Me.splleft.TabStop = False
        '
        'pnlleft
        '
        Me.pnlleft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlleft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlleft.Location = New System.Drawing.Point(0, 0)
        Me.pnlleft.Name = "pnlleft"
        Me.pnlleft.Size = New System.Drawing.Size(233, 687)
        Me.pnlleft.TabIndex = 1
        Me.pnlleft.Visible = False
        '
        'splRight
        '
        Me.splRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.splRight.Location = New System.Drawing.Point(767, 55)
        Me.splRight.Name = "splRight"
        Me.splRight.Size = New System.Drawing.Size(3, 687)
        Me.splRight.TabIndex = 4
        Me.splRight.TabStop = False
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlRight.Controls.Add(Me.pnlMxHistory)
        Me.pnlRight.Controls.Add(Me.Splitter1)
        Me.pnlRight.Controls.Add(Me.pnlRxHistory)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(770, 55)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlRight.Size = New System.Drawing.Size(254, 687)
        Me.pnlRight.TabIndex = 3
        '
        'pnlMxHistory
        '
        Me.pnlMxHistory.Controls.Add(Me.Label22)
        Me.pnlMxHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMxHistory.Location = New System.Drawing.Point(0, 289)
        Me.pnlMxHistory.Name = "pnlMxHistory"
        Me.pnlMxHistory.Size = New System.Drawing.Size(254, 395)
        Me.pnlMxHistory.TabIndex = 4
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(0, 394)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(254, 1)
        Me.Label22.TabIndex = 17
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 285)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(254, 4)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'pnlRxHistory
        '
        Me.pnlRxHistory.Controls.Add(Me.Label23)
        Me.pnlRxHistory.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRxHistory.Location = New System.Drawing.Point(0, 3)
        Me.pnlRxHistory.Name = "pnlRxHistory"
        Me.pnlRxHistory.Size = New System.Drawing.Size(254, 282)
        Me.pnlRxHistory.TabIndex = 5
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(0, 281)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(254, 1)
        Me.Label23.TabIndex = 18
        '
        'pnltop
        '
        Me.pnltop.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pnltop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnltop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltop.Controls.Add(Me.pnlFormularyToolBar)
        Me.pnltop.Controls.Add(Me.pnlDI)
        Me.pnltop.Controls.Add(Me.pnlmainToolBar)
        Me.pnltop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltop.Location = New System.Drawing.Point(0, 0)
        Me.pnltop.Name = "pnltop"
        Me.pnltop.Size = New System.Drawing.Size(1024, 55)
        Me.pnltop.TabIndex = 0
        '
        'pnlFormularyToolBar
        '
        Me.pnlFormularyToolBar.BackColor = System.Drawing.Color.Transparent
        Me.pnlFormularyToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFormularyToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFormularyToolBar.Location = New System.Drawing.Point(819, 0)
        Me.pnlFormularyToolBar.Name = "pnlFormularyToolBar"
        Me.pnlFormularyToolBar.Size = New System.Drawing.Size(205, 55)
        Me.pnlFormularyToolBar.TabIndex = 3
        '
        'pnlDI
        '
        Me.pnlDI.BackColor = System.Drawing.Color.Transparent
        Me.pnlDI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDI.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlDI.Location = New System.Drawing.Point(497, 0)
        Me.pnlDI.Name = "pnlDI"
        Me.pnlDI.Size = New System.Drawing.Size(322, 55)
        Me.pnlDI.TabIndex = 1
        '
        'pnlmainToolBar
        '
        Me.pnlmainToolBar.BackColor = System.Drawing.Color.Transparent
        Me.pnlmainToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlmainToolBar.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlmainToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlmainToolBar.Name = "pnlmainToolBar"
        Me.pnlmainToolBar.Size = New System.Drawing.Size(497, 55)
        Me.pnlmainToolBar.TabIndex = 0
        '
        'frmPrescription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1024, 742)
        Me.Controls.Add(Me.pnlAllergiesAlerts)
        Me.Controls.Add(Me.pnlDIProgress)
        Me.Controls.Add(Me.pnlWait)
        Me.Controls.Add(Me.pnlElementHostCopay)
        Me.Controls.Add(Me.pnlFormulary)
        Me.Controls.Add(Me.pnlFormularyTransactionMessage)
        Me.Controls.Add(Me.pnlFormularyProgress)
        Me.Controls.Add(Me.pnlFormularyCoverage)
        Me.Controls.Add(Me.pnlViewMedicationHistory)
        Me.Controls.Add(Me.pnlViewDocument)
        Me.Controls.Add(Me.pnlcenter)
        Me.Controls.Add(Me.splRight)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnltop)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPrescription"
        Me.ShowInTaskbar = False
        Me.Text = "Rx/Meds"
        Me.pnlAllergiesAlerts.ResumeLayout(False)
        Me.pnlAllergiesAlerts.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.picBottom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAlertClose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMiddle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPDMP.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.pnlDIProgress.ResumeLayout(False)
        Me.pnlDIProgress.PerformLayout()
        Me.pnlWait.ResumeLayout(False)
        Me.pnlWait.PerformLayout()
        Me.pnlElementHostCopay.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.pnlFormulary.ResumeLayout(False)
        Me.pnlFormularyDrugName.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.pnlFormularyTransactionMessage.ResumeLayout(False)
        Me.pnlFormularyTransactionMessage.PerformLayout()
        Me.pnlFormularyProgress.ResumeLayout(False)
        Me.pnlFormularyProgress.PerformLayout()
        Me.pnlFormularyCoverage.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlFormularyCoverageHeading.ResumeLayout(False)
        Me.pnlFormularyCoverageHeading.PerformLayout()
        Me.pnlViewMedicationHistory.ResumeLayout(False)
        Me.pnlMedicationHistoryHeader.ResumeLayout(False)
        Me.pnlMedicationHistoryHeader.PerformLayout()
        Me.pnlViewDocument.ResumeLayout(False)
        Me.pnlDMSHeader.ResumeLayout(False)
        Me.pnlDMSHeader.PerformLayout()
        Me.pnlcenter.ResumeLayout(False)
        Me.pnlFlexGrid.ResumeLayout(False)
        Me.pnlRxMx.ResumeLayout(False)
        Me.pnlMedicationGrid.ResumeLayout(False)
        Me.pnlMxLabel.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlPrescriptionGrid.ResumeLayout(False)
        Me.pnlprescriptionbtn.ResumeLayout(False)
        Me.pnlRxLabel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlSupervisingProvider.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pnlRight.ResumeLayout(False)
        Me.pnlMxHistory.ResumeLayout(False)
        Me.pnlRxHistory.ResumeLayout(False)
        Me.pnltop.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlcenter As System.Windows.Forms.Panel
    Friend WithEvents pnlcentertop As System.Windows.Forms.Panel
    Friend WithEvents splRight As System.Windows.Forms.Splitter
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents splleft As System.Windows.Forms.Splitter
    Friend WithEvents pnlleft As System.Windows.Forms.Panel
    Friend WithEvents pnltop As System.Windows.Forms.Panel
    Friend WithEvents pnlmainToolBar As System.Windows.Forms.Panel
    Friend WithEvents pnlDI As System.Windows.Forms.Panel
    Friend WithEvents pnlmonograph As System.Windows.Forms.Panel
    Friend WithEvents pnlDIScreenResult As System.Windows.Forms.Panel
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents pnlRefill As System.Windows.Forms.Panel
    Friend WithEvents pnlAllergiesAlerts As System.Windows.Forms.Panel
    Friend WithEvents lblAlert1 As System.Windows.Forms.Label
    Friend WithEvents picAlertClose1 As System.Windows.Forms.PictureBox
    Friend WithEvents picInfo As System.Windows.Forms.PictureBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents picMiddle As System.Windows.Forms.PictureBox
    Friend WithEvents picBottom As System.Windows.Forms.PictureBox
    Friend WithEvents picTop As System.Windows.Forms.PictureBox
    Friend WithEvents pnlFlexGrid As System.Windows.Forms.Panel
    Friend WithEvents pnlMxHistory As System.Windows.Forms.Panel
    Friend WithEvents pnlRxHistory As System.Windows.Forms.Panel
    Friend WithEvents pnlFormularyToolBar As System.Windows.Forms.Panel
    Friend WithEvents pnlMedicationGrid As System.Windows.Forms.Panel
    Friend WithEvents pnlMedicationDetails As System.Windows.Forms.Panel
    Friend WithEvents pnlMxLabel As System.Windows.Forms.Panel
    Friend WithEvents lblMedicationGridLabel As System.Windows.Forms.Label
    Friend WithEvents pnlPrescriptionGrid As System.Windows.Forms.Panel
    Friend WithEvents pnlprescriptionbtn As System.Windows.Forms.Panel
    Friend WithEvents pnlPrescriptionDetails As System.Windows.Forms.Panel
    Friend WithEvents pnlRxLabel As System.Windows.Forms.Panel
    Friend WithEvents lblPrescriptionGridLabel As System.Windows.Forms.Label
    Friend WithEvents btnRxDown As System.Windows.Forms.Button
    Friend WithEvents btnRxUp As System.Windows.Forms.Button
    Friend WithEvents pnlCenter2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnMxDown As System.Windows.Forms.Button
    Friend WithEvents btnMxUp As System.Windows.Forms.Button
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlFormulary As System.Windows.Forms.Panel
    Friend WithEvents pnlFormularyDrugName As System.Windows.Forms.Panel
    Friend WithEvents lblFormularyDrugName As System.Windows.Forms.Label
    Friend WithEvents pnlFormularyCoverage As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Protected Friend WithEvents rtfFormularyDescription As gloRichtextbox
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents pnlFormularyCoverageHeading As System.Windows.Forms.Panel
    Friend WithEvents btnFormularyGridClose As System.Windows.Forms.Button
    Friend WithEvents btnFormularyCovPnlClose As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents pnlFormularyProgress As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cMnuStrp As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents lblAlternativeDrugName As System.Windows.Forms.Label
    Friend WithEvents btnPrescription As System.Windows.Forms.Button
    Friend WithEvents btnAllergies As System.Windows.Forms.Button

    Friend WithEvents btnMedication As System.Windows.Forms.Button
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlFormularyTransactionMessage As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblFormularyTransactionMessage As System.Windows.Forms.Label
    Friend WithEvents sptRefill As System.Windows.Forms.Splitter
    Friend WithEvents pnlRxMx As System.Windows.Forms.Panel
    Friend WithEvents sptRxMx As System.Windows.Forms.Splitter
    Friend WithEvents pnlViewMedicationHistory As System.Windows.Forms.Panel
    Friend WithEvents pnlMedicationHistoryFooter As System.Windows.Forms.Panel
    Friend WithEvents pnlMedicationHistory As System.Windows.Forms.Panel
    Private WithEvents pnlMedicationHistoryHeader As System.Windows.Forms.Panel
    Friend WithEvents btnViewMedicationHistoryClose As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pnlViewDocument As System.Windows.Forms.Panel
    Friend WithEvents pnlViewDocumentFooter As System.Windows.Forms.Panel
    Friend WithEvents pnlDocument As System.Windows.Forms.Panel
    Private WithEvents pnlDMSHeader As System.Windows.Forms.Panel
    Friend WithEvents btnViewDocumentClose As System.Windows.Forms.Button
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents pnlSupervisingProvider As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cmbSupervisingProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblSupervisingValidation As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents elementHostCopay As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents elementHost As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents pnlElementHostCopay As System.Windows.Forms.Panel
    Private WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents lblCoverageCopayHeading As System.Windows.Forms.Label
    Friend WithEvents btnCloseCopayCoverage As System.Windows.Forms.Button
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pnlWait As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents pnlDIProgress As System.Windows.Forms.Panel
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents pnlPDMP As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents elementHostPDMP As System.Windows.Forms.Integration.ElementHost
    Private WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents lblPDMP As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label

End Class
