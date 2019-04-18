Public Class frmMSTDrugs
    Inherits System.Windows.Forms.Form

    '''' While Add, return this Paramerters to View-From so the Newly Added Records get Highlighted 
    Public _DrugName As String
    Public _Dosage As String
    Public _DrugCategory As Int16
    ''''
    Public _drugformtoshowinCombo As String = ""

    Private m_blnIsClinical As Boolean
    Private m_ID As Long
    Private m_AlternativeFormID As Int32
    Private m_sOldNDC As String = "" ''Used only in Modifing drugs mode, that drugs already present in DataBase 
    Private m_blnIsModifyDrug As Boolean = False
    Private m_Caption As String = "gloEMR"
    Dim objclsDrugs As New clsDrugs
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_MSTDrugs As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents lblNDCCOde As System.Windows.Forms.Label
    Friend WithEvents cmbDuration As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDoseUnit As System.Windows.Forms.TextBox
    Friend WithEvents cmbDrugsForm As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtNDCCode As System.Windows.Forms.TextBox
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Public CancelClick As Boolean
    '' flag to check whether Ok/Cancel button is click
    '' if Cancel, CancelClick =True  '' if Ok  CancelClick  = False
    Private _IsSystemAddedDrug As Boolean = False
    Friend WithEvents cmbDoseUnit As System.Windows.Forms.ComboBox 'flag aded to check the drug is user added or not
#Region " Windows Form Designer generated code "

    Dim i As Integer
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call


        cmbDrugsForm.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbDrugsForm.DrawItem, AddressOf ShowTooltipOnComboBox
        cmbDrugsForm.DropDownStyle = ComboBoxStyle.DropDownList

        i = Me.Width

    End Sub

    Private Sub ShowTooltipOnComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        Try

            'cmbDrugsForm = DirectCast(sender, ComboBox)
            If cmbDrugsForm.Items.Count > 0 AndAlso e.Index >= 0 Then

                e.DrawBackground()
                Using br As New SolidBrush(e.ForeColor)
                    e.Graphics.DrawString(cmbDrugsForm.GetItemText(cmbDrugsForm.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
                End Using

                If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                    If cmbDrugsForm.DroppedDown Then
                        'Me.Width = 388
                        If getWidthofListItems(cmbDrugsForm.GetItemText(cmbDrugsForm.Items(e.Index)).ToString(), cmbDrugsForm) >= cmbDrugsForm.DropDownWidth - 2 Then
                            If e.Bounds.Bottom < cmbDrugsForm.DropDownHeight + 10 And e.Bounds.Bottom > 15 Then
                                toolTip1.Show(cmbDrugsForm.GetItemText(cmbDrugsForm.Items(e.Index)), cmbDrugsForm, e.Bounds.Right - 100, e.Bounds.Bottom)
                            Else
                                toolTip1.Hide(cmbDrugsForm)
                            End If
                        End If
                    Else
                        toolTip1.Hide(cmbDrugsForm)
                    End If
                Else
                    toolTip1.Hide(cmbDrugsForm)
                End If
                e.DrawFocusRectangle()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer
        Try

            'Dim g As Graphics = CreateGraphics()
            'Dim s As SizeF = g.MeasureString(_text, comboBox1.Font)
            'Width = Convert.ToInt32(s.Width)
            Width = 442
            Return Width

        Catch ex As Exception
            Return Nothing
        Finally


            txtDoseUnit.Visible = False
            txtDoseUnit.SendToBack()
            cmbDrugsForm.Location = New Point(299, 210)
            cmbDrugsForm.Size = New Size(115, 22)

            cmbDoseUnit.Location = New Point(178, 211)
            cmbDoseUnit.Size = New Size(115, 22)

            cmbDoseUnit.Visible = True
            cmbDoseUnit.BringToFront()


        End Try
    End Function



    'Private Sub cmbDrugsForm_MouseMove_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbDrugsForm.MouseMove
    '    Try

    '        'cmbDrugsForm = DirectCast(sender, ComboBox)
    '        If cmbDrugsForm.SelectedItem IsNot Nothing Then
    '            'Me.Width = 388
    '            If getWidthofListItems(cmbDrugsForm.SelectedItem.ToString(), cmbDrugsForm) >= cmbDrugsForm.DropDownWidth Then
    '                i = Me.Width
    '                ''  toolTip1.Show(cmbDrugsForm.SelectedItem.ToString(), cmbDrugsForm, cmbDrugsForm.Right - 200, cmbDrugsForm.Bottom - 110)

    '            End If

    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub



    Public Sub New(ByVal ID As Long, ByVal AlternativeFormID As Long)
        MyBase.New()
        m_ID = ID
        m_AlternativeFormID = AlternativeFormID
        m_blnIsModifyDrug = True
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Property Caption() As String
        Get
            Return m_Caption
        End Get
        Set(ByVal Value As String)
            m_Caption = Value
        End Set
    End Property
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDrugName As System.Windows.Forms.TextBox
    Friend WithEvents txtGenericName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDosage As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRoute As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFrequency As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDuration As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkClinicDrug As System.Windows.Forms.CheckBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbNarcotics As System.Windows.Forms.ComboBox
    Friend WithEvents chkAllergicDrug As System.Windows.Forms.CheckBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTDrugs))
        Me.chkAllergicDrug = New System.Windows.Forms.CheckBox()
        Me.cmbNarcotics = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkClinicDrug = New System.Windows.Forms.CheckBox()
        Me.txtDuration = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFrequency = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRoute = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDosage = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtGenericName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDrugName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmbDoseUnit = New System.Windows.Forms.ComboBox()
        Me.txtNDCCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDoseUnit = New System.Windows.Forms.TextBox()
        Me.cmbDrugsForm = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbDuration = New System.Windows.Forms.ComboBox()
        Me.lblNDCCOde = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp_MSTDrugs = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_MSTDrugs.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkAllergicDrug
        '
        Me.chkAllergicDrug.AutoSize = True
        Me.chkAllergicDrug.Location = New System.Drawing.Point(130, 330)
        Me.chkAllergicDrug.Name = "chkAllergicDrug"
        Me.chkAllergicDrug.Size = New System.Drawing.Size(107, 18)
        Me.chkAllergicDrug.TabIndex = 13
        Me.chkAllergicDrug.Text = "Is Allergic Drug"
        '
        'cmbNarcotics
        '
        Me.cmbNarcotics.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbNarcotics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNarcotics.ForeColor = System.Drawing.Color.Black
        Me.cmbNarcotics.Location = New System.Drawing.Point(130, 274)
        Me.cmbNarcotics.Name = "cmbNarcotics"
        Me.cmbNarcotics.Size = New System.Drawing.Size(156, 22)
        Me.cmbNarcotics.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(60, 278)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 14)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "DEA Class :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAmount
        '
        Me.txtAmount.ForeColor = System.Drawing.Color.Black
        Me.txtAmount.Location = New System.Drawing.Point(130, 210)
        Me.txtAmount.MaxLength = 255
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ShortcutsEnabled = False
        Me.txtAmount.Size = New System.Drawing.Size(45, 22)
        Me.txtAmount.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(65, 214)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 14)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Quantity :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkClinicDrug
        '
        Me.chkClinicDrug.AutoSize = True
        Me.chkClinicDrug.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkClinicDrug.Location = New System.Drawing.Point(130, 306)
        Me.chkClinicDrug.Name = "chkClinicDrug"
        Me.chkClinicDrug.Size = New System.Drawing.Size(121, 18)
        Me.chkClinicDrug.TabIndex = 12
        Me.chkClinicDrug.Text = "Practice Favorites"
        Me.chkClinicDrug.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtDuration
        '
        Me.txtDuration.ForeColor = System.Drawing.Color.Black
        Me.txtDuration.Location = New System.Drawing.Point(130, 178)
        Me.txtDuration.MaxLength = 3
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.ShortcutsEnabled = False
        Me.txtDuration.Size = New System.Drawing.Size(163, 22)
        Me.txtDuration.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(66, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 14)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Duration :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFrequency
        '
        Me.txtFrequency.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFrequency.ForeColor = System.Drawing.Color.Black
        Me.txtFrequency.Location = New System.Drawing.Point(130, 146)
        Me.txtFrequency.MaxLength = 255
        Me.txtFrequency.Name = "txtFrequency"
        Me.txtFrequency.Size = New System.Drawing.Size(284, 22)
        Me.txtFrequency.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(55, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Frequency :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRoute
        '
        Me.txtRoute.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRoute.ForeColor = System.Drawing.Color.Black
        Me.txtRoute.Location = New System.Drawing.Point(130, 114)
        Me.txtRoute.MaxLength = 255
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(284, 22)
        Me.txtRoute.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(79, 118)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Route :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDosage
        '
        Me.txtDosage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDosage.ForeColor = System.Drawing.Color.Black
        Me.txtDosage.Location = New System.Drawing.Point(130, 82)
        Me.txtDosage.MaxLength = 255
        Me.txtDosage.Name = "txtDosage"
        Me.txtDosage.Size = New System.Drawing.Size(284, 22)
        Me.txtDosage.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(72, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Dosage :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtGenericName
        '
        Me.txtGenericName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGenericName.ForeColor = System.Drawing.Color.Black
        Me.txtGenericName.Location = New System.Drawing.Point(130, 50)
        Me.txtGenericName.MaxLength = 255
        Me.txtGenericName.Name = "txtGenericName"
        Me.txtGenericName.Size = New System.Drawing.Size(284, 22)
        Me.txtGenericName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Generic Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDrugName
        '
        Me.txtDrugName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDrugName.ForeColor = System.Drawing.Color.Black
        Me.txtDrugName.Location = New System.Drawing.Point(130, 18)
        Me.txtDrugName.MaxLength = 255
        Me.txtDrugName.Name = "txtDrugName"
        Me.txtDrugName.Size = New System.Drawing.Size(284, 22)
        Me.txtDrugName.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Drug Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.cmbDoseUnit)
        Me.Panel2.Controls.Add(Me.txtNDCCode)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.txtDoseUnit)
        Me.Panel2.Controls.Add(Me.cmbDrugsForm)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.cmbDuration)
        Me.Panel2.Controls.Add(Me.lblNDCCOde)
        Me.Panel2.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel2.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel2.Controls.Add(Me.lbl_pnlRight)
        Me.Panel2.Controls.Add(Me.lbl_pnlTop)
        Me.Panel2.Controls.Add(Me.chkAllergicDrug)
        Me.Panel2.Controls.Add(Me.txtGenericName)
        Me.Panel2.Controls.Add(Me.txtDrugName)
        Me.Panel2.Controls.Add(Me.chkClinicDrug)
        Me.Panel2.Controls.Add(Me.txtRoute)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.cmbNarcotics)
        Me.Panel2.Controls.Add(Me.txtDuration)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtFrequency)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.txtAmount)
        Me.Panel2.Controls.Add(Me.txtDosage)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(426, 365)
        Me.Panel2.TabIndex = 3
        '
        'cmbDoseUnit
        '
        Me.cmbDoseUnit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDoseUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbDoseUnit.FormattingEnabled = True
        Me.cmbDoseUnit.Location = New System.Drawing.Point(178, 211)
        Me.cmbDoseUnit.Name = "cmbDoseUnit"
        Me.cmbDoseUnit.Size = New System.Drawing.Size(115, 22)
        Me.cmbDoseUnit.TabIndex = 45
        '
        'txtNDCCode
        '
        Me.txtNDCCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNDCCode.ForeColor = System.Drawing.Color.Black
        Me.txtNDCCode.Location = New System.Drawing.Point(130, 242)
        Me.txtNDCCode.MaxLength = 11
        Me.txtNDCCode.Name = "txtNDCCode"
        Me.txtNDCCode.ShortcutsEnabled = False
        Me.txtNDCCode.Size = New System.Drawing.Size(138, 22)
        Me.txtNDCCode.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(44, 247)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(14, 14)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "*"
        '
        'txtDoseUnit
        '
        Me.txtDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDoseUnit.ForeColor = System.Drawing.Color.Black
        Me.txtDoseUnit.Location = New System.Drawing.Point(181, 210)
        Me.txtDoseUnit.MaxLength = 30
        Me.txtDoseUnit.Name = "txtDoseUnit"
        Me.txtDoseUnit.ShortcutsEnabled = False
        Me.txtDoseUnit.Size = New System.Drawing.Size(54, 22)
        Me.txtDoseUnit.TabIndex = 8
        '
        'cmbDrugsForm
        '
        Me.cmbDrugsForm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDrugsForm.DropDownHeight = 200
        Me.cmbDrugsForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDrugsForm.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbDrugsForm.FormattingEnabled = True
        Me.cmbDrugsForm.IntegralHeight = False
        Me.cmbDrugsForm.Location = New System.Drawing.Point(299, 210)
        Me.cmbDrugsForm.Name = "cmbDrugsForm"
        Me.cmbDrugsForm.Size = New System.Drawing.Size(115, 22)
        Me.cmbDrugsForm.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(39, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(14, 14)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "*"
        '
        'cmbDuration
        '
        Me.cmbDuration.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDuration.ForeColor = System.Drawing.Color.Black
        Me.cmbDuration.Location = New System.Drawing.Point(299, 179)
        Me.cmbDuration.Name = "cmbDuration"
        Me.cmbDuration.Size = New System.Drawing.Size(115, 22)
        Me.cmbDuration.TabIndex = 6
        '
        'lblNDCCOde
        '
        Me.lblNDCCOde.AutoSize = True
        Me.lblNDCCOde.Location = New System.Drawing.Point(57, 246)
        Me.lblNDCCOde.Name = "lblNDCCOde"
        Me.lblNDCCOde.Size = New System.Drawing.Size(70, 14)
        Me.lblNDCCOde.TabIndex = 19
        Me.lblNDCCOde.Text = "NDC Code :"
        Me.lblNDCCOde.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 361)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(418, 1)
        Me.lbl_pnlBottom.TabIndex = 17
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 358)
        Me.lbl_pnlLeft.TabIndex = 16
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(422, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 358)
        Me.lbl_pnlRight.TabIndex = 15
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(420, 1)
        Me.lbl_pnlTop.TabIndex = 14
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_MSTDrugs)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(426, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_MSTDrugs
        '
        Me.tlsp_MSTDrugs.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_MSTDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_MSTDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_MSTDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_MSTDrugs.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_MSTDrugs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_MSTDrugs.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_MSTDrugs.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_MSTDrugs.Name = "tlsp_MSTDrugs"
        Me.tlsp_MSTDrugs.Size = New System.Drawing.Size(426, 53)
        Me.tlsp_MSTDrugs.TabIndex = 0
        Me.tlsp_MSTDrugs.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
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
        'frmMSTDrugs
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(426, 418)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTDrugs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Drugs"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_MSTDrugs.ResumeLayout(False)
        Me.tlsp_MSTDrugs.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub SaveMSTDrugs()
        Dim DurationValue As String = ""
        Dim objfrmViewDrugs As New frmVWDrugs
        Dim sNDC As String = ""
        Dim sDrugname As String = ""
        Try
            ''DrugName is Cumpulsary Field
            If Trim(txtDrugName.Text) = "" Then
                ''if DrugName is Not entered then give messsage
                MessageBox.Show("Drug Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDrugName.Focus()
                Exit Sub
            End If
            If cmbDoseUnit.Text = String.Empty Then
                MessageBox.Show("Select Unit Of Measure. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbDoseUnit.Focus()
                Exit Sub
            End If

            ''\\Commented by suraj 20090128
            '' '' Check for duplicate Drug
            ''If objclsDrugs.CheckDuplicate(m_ID, Trim(txtDrugName.Text), Trim(txtDosage.Text)) = True Then
            ''    ''if DrugName is alredy exists then give messsage
            ''    MessageBox.Show("Drug Name Already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ''    txtDrugName.Focus()
            ''    Exit Sub
            ''End If

            '\\ Add Duration value + days\Weeks\Months  20090130
            If Trim(txtDuration.Text) <> "" Then
                DurationValue = Trim(txtDuration.Text) + " " + cmbDuration.SelectedItem.ToString
            Else
                DurationValue = ""
            End If

            Dim strDispense As String = String.Empty

            If txtAmount.Text.Trim <> "" Then
                If cmbDoseUnit.Text.Trim = "Unspecified" Then
                    strDispense = txtAmount.Text.Trim
                Else
                    strDispense = txtAmount.Text.Trim & " " & cmbDoseUnit.Text.Trim
                End If
            End If



            ''Check for NDC
            If txtNDCCode.Text.Trim.Length <> 0 Then

                If txtNDCCode.Text.Trim.Length = 11 Then

                    If m_blnIsModifyDrug = True Then ''it is open for modify drug, So check NDC code if changeed or not
                        ''Modify Drug
                        sNDC = txtNDCCode.Text.Trim.ToString
                        sDrugname = txtDrugName.Text.Trim.ToString

                        If sNDC.Contains("GLO") Then
                            If m_sOldNDC <> sNDC Then
                                MessageBox.Show("You can not modify a fake NDC Code", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                txtNDCCode.Text = m_sOldNDC
                                txtNDCCode.Focus()
                                Exit Sub
                            End If
                        End If

                        If objclsDrugs.IsNDCDuplicateforModify(sNDC, m_ID) Then  ''Check duplicate
                            If (MessageBox.Show("Duplicate NDC Code found. Do you want to generate a fake NDC Code?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                                sNDC = GenerateFakeNDC()
                            Else
                                txtNDCCode.Focus()
                                Exit Sub
                            End If
                        Else   ''check now old NDC is in used->user want to Replace it with New NDC [asked user]
                            If m_blnIsModifyDrug = True Then
                                If m_sOldNDC <> sNDC Then
                                    '' that means NDC code change by User, So check old NDC in used or not

                                    If objclsDrugs.CheckNDCinUsed(m_sOldNDC) Then  ''Check old NDC is in used for changing  with NEw NDC
                                        If (MessageBox.Show("The previous NDC Code is already used in transactions. Do you want to update all transactions with the new NDC Code?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                                            ''yes-> update oldNDC with newNDC in all transaction tables -> Drugs_MST, DrugProviderAssociation, Prescription, Medication
                                            ''m_sOldNDC is set at loading form for modifying drug mode 
                                            objclsDrugs.UpdateNDC(m_sOldNDC, sNDC)   ''this replace old with new NDC
                                        Else
                                            ''no-> don't update wth new NDC but update new NDC in Drug MST
                                            sNDC = txtNDCCode.Text.Trim.ToString
                                        End If
                                    Else
                                        ''update drug with new updated enteredNDC
                                        sNDC = txtNDCCode.Text.Trim.ToString
                                    End If
                                Else

                                    ''update drug with new updated enteredNDC
                                    sNDC = txtNDCCode.Text.Trim.ToString

                                End If
                            End If
                        End If

                    Else
                        ''Add New Drug
                        If (MessageBox.Show("Is this a valid NDC Code?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                            ''yes -> save NDC
                            ''check for duplicate -> msg
                            sNDC = txtNDCCode.Text.Trim.ToString
                            If objclsDrugs.IsNDCDuplicate(sNDC) Then
                                If (MessageBox.Show("Duplicate NDC Code found. Do you want to generate a fake NDC Code? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                                    sNDC = GenerateFakeNDC()

                                Else
                                    txtNDCCode.Focus()
                                    Exit Sub
                                End If
                            Else
                                sNDC = txtNDCCode.Text.Trim.ToString
                            End If

                        Else

                            ''No -> then add "GLO" prefix & save NDC 
                            If (MessageBox.Show("Do you want to generate a fake NDC Code? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                                sNDC = GenerateFakeNDC()
                            Else
                                txtNDCCode.Focus()
                                Exit Sub
                            End If
                        End If



                    End If

                Else
                    ''must be 11 digit
                    MessageBox.Show("Valid NDC Code format not found. NDC Code must be 11 digits.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtNDCCode.Focus()
                    Exit Sub
                End If
            Else
                ''MessageBox.Show("NDC Code must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question)
                ''txtNDCCode.Focus()
                ''Exit Sub

                ''Blank NDC -> then informed User & asked for fake NDC then if yes add "GLO" prefix & save NDC 
                If (MessageBox.Show("Found blank NDC Code. Do you want to generate a fake NDC Code?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    If m_blnIsModifyDrug = True Then
                        sNDC = m_sOldNDC  ''Don't  modify fake NDC wich is generated previously for this drug , give it previous generated fakeNDC again
                    Else
                        sNDC = GenerateFakeNDC()
                    End If
                Else
                    txtNDCCode.Focus()
                    Exit Sub
                End If
            End If

            '' Procedure to add new drug / Update Drug               '\\ Added NDCCode field 20090130 SURAJ & change in Duration value
            m_ID = objclsDrugs.AddNewDrug(m_ID, Trim(txtDrugName.Text), Trim(txtGenericName.Text), Trim(txtDosage.Text), Trim(txtRoute.Text), Trim(txtFrequency.Text), DurationValue, chkClinicDrug.CheckState, strDispense, cmbNarcotics.Text, chkAllergicDrug.CheckState, sNDC, cmbDrugsForm.Text, cmbDoseUnit.Text)
            _DrugName = Trim(txtDrugName.Text)
            _Dosage = Trim(txtDosage.Text)
            m_blnIsClinical = chkClinicDrug.CheckState


            If chkClinicDrug.CheckState = CheckState.Checked Then
                _DrugCategory = 2
            ElseIf chkClinicDrug.CheckState = CheckState.Unchecked Then
                _DrugCategory = 3
            End If

            CancelClick = False
            'objdrugsView.grdDrugs.DataSource = objclsDrugs.GetAllDrugs
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmViewDrugs = Nothing
        End Try
    End Sub
    ''Generate fakeNDC with GLO prefix
    Private Function GenerateFakeNDC() As String
        Dim prefix As String = "GLO"
        Dim sFakeNDC As String = ""
        Try
            ''Hr + min + Sec + miliSec ["GLO" + 8digit ]
            sFakeNDC = Format(DateTime.Now.Hour, "#00") & Format(DateTime.Now.Minute, "#00") & Format(DateTime.Now.Second, "#00") & Format(DateTime.Now.Millisecond, "#00") & ""

            If sFakeNDC.Length > 8 Then
                sFakeNDC = sFakeNDC.Substring(1, 8)
            ElseIf sFakeNDC.Length < 8 Then
                If sFakeNDC.Length = 7 Then
                    sFakeNDC = sFakeNDC & "0"
                ElseIf sFakeNDC.Length = 6 Then
                    sFakeNDC = sFakeNDC & "00"
                End If
            End If

            sFakeNDC = prefix & sFakeNDC
            Return sFakeNDC

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on generating Fake NDC Code " & sFakeNDC.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""

        End Try
    End Function
    Private Sub frmDrugs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim objdrugsView As New frmDrugsView()
         Try

            FillPotencyCode()
            txtDoseUnit.Visible = False
            txtDoseUnit.SendToBack()
            cmbDrugsForm.Location = New Point(299, 210)
            cmbDrugsForm.Size = New Size(115, 22)

            cmbDoseUnit.Location = New Point(178, 211)
            cmbDoseUnit.Size = New Size(115, 22)

            cmbDoseUnit.Visible = True
            cmbDoseUnit.BringToFront()

            '--Start --
            cmbNarcotics.Items.Add("Non-Scheduled")
            cmbNarcotics.Items.Add("Schedule II")
            cmbNarcotics.Items.Add("Schedule III")
            cmbNarcotics.Items.Add("Schedule IV")
            cmbNarcotics.Items.Add("Schedule V")

            'cmbNarcotics.Text = "C1"
            cmbNarcotics.Text = "Non-Scheduled"
            '--- End  of case  : GLO2010-0009973 i.e Narcotics flag in software doesn't equal narcotics flag set in database
            ''if m_ID <> 0  Open for Update
            txtDrugName.Text = _DrugName
            txtDosage.Text = _Dosage

            '\\Added by SURAJ 20090130
            cmbDuration.Items.Add("Days")
            cmbDuration.Items.Add("Weeks")
            cmbDuration.Items.Add("Months")
            cmbDuration.Text = cmbDuration.Items(0)

            If m_ID <> 0 Then
                '' procedure to get record of drugs for selected drug of ID m_ID
                '' m_ID = Selected DrugID
                Fill_Drug(m_ID)
            End If

            ''Load the Drugs Form Combo box
            'Rxhub

            Dim _dtDrugsForm As List(Of String) = Nothing
            _dtDrugsForm = GetDrugsForms()

            'clear the combo box
            cmbDrugsForm.Items.Clear()
            If Not IsNothing(_dtDrugsForm) Then
                For Each item As String In _dtDrugsForm
                    cmbDrugsForm.Items.Add(item)
                Next
            End If
            If _drugformtoshowinCombo <> "" Then    ''''which is initialized in Fill_Drug(m_ID)function
                cmbDrugsForm.Text = _drugformtoshowinCombo.ToString
            End If
            'Rxhub

            If m_blnIsModifyDrug = True Then
                m_sOldNDC = txtNDCCode.Text.Trim.ToString '' this oldNDC used only in modifying Drug mode that drugs already exist in database
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
          
        End Try
    End Sub
    '\\Added by suraj 20090130 for duration value spliting - days\weeks\months
    Private Function SplitDuration(ByVal _strDuration As String) As Array
        Try
            Dim _result As String()
            _result = _strDuration.Split(" ")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function

    Private Function GetDrugsForms() As List(Of String)
        Dim _dtDrugsForm As List(Of String) = Nothing
        Try
            Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gstrDIBServiceURL)
                _dtDrugsForm = oDIBGSHelper.GetDrugFormList()
            End Using
            Return _dtDrugsForm
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
        End Try
    End Function
   

    Public Sub Fill_Drug(ByVal DrugID As Long)
        Dim strCmbDuration As String = ""
        Dim strDuration As String = ""

        Dim dv As New DataView
        objclsDrugs.SelectDrug(DrugID)
        dv = objclsDrugs.GetDataview
        If dv IsNot Nothing AndAlso dv.Table.Rows.Count > 0 Then

            txtDrugName.Text = dv.Item(0)(0).ToString
            txtGenericName.Text = dv.Item(0)(1).ToString
            txtDosage.Text = dv.Item(0)(2).ToString
            txtRoute.Text = dv.Item(0)(3).ToString
            txtFrequency.Text = dv.Item(0)(4).ToString
            cmbDoseUnit.Text = Convert.ToString(dv.Item(0)("PotencyUnit"))

            '\\ Added 20090130 Suraj
            '\\ fetching Duration Value Days\weeks\months
            If Not IsNothing(dv.Item(0)(5)) Then
                Dim retval As String() = SplitDuration(dv.Item(0)(5).ToString) '\\split value with " "(blank space)
                If Not IsNothing(retval) Then
                    If retval.Length > 1 Then
                        strDuration = retval(0)
                        strCmbDuration = retval(retval.Length - 1)
                    Else
                        strDuration = dv.Item(0)(5).ToString
                    End If
                Else
                    strDuration = dv.Item(0)(5).ToString
                End If
            Else
                strDuration = ""
            End If

            If strCmbDuration <> "" Then
                If strCmbDuration = "Days" Then
                    cmbDuration.Text = cmbDuration.Items(0) '0th item is Days
                ElseIf strCmbDuration = "Weeks" Then
                    cmbDuration.Text = cmbDuration.Items(1) '1st item is Weeks
                Else
                    cmbDuration.Text = cmbDuration.Items(2) '2nd item is Months
                End If
            End If

            txtDuration.Text = strDuration.ToString
            '\\

            If dv.Item(0)(7).ToString <> "" Then 'fixed bug 5453
                Dim strDispense As String() = Split(dv.Item(0)(7).ToString, " ")
                If strDispense.Length >= 1 Then
                    txtAmount.Text = strDispense(0)
                    'cmbDoseUnit.Items.Add(strDispense(1).ToString)                       
                Else
                    txtAmount.Text = ""
                    'cmbDoseUnit.Text = ""
                End If
            Else
                txtAmount.Text = ""
                'cmbDoseUnit.Text = ""
            End If


            If dv.Item(0)("DrugForm").ToString <> "" Then
                'cmbDrugsForm.Text = dv.Item(0)("DrugForm").trim.ToString
                _drugformtoshowinCombo = dv.Item(0)("DrugForm").trim.ToString
            End If
            '\\ added on 20090130 SURAJ - for NDC code
            txtNDCCode.Text = dv.Item(0)(11).ToString

            Dim _nNarcoticValue As Int16 = dv.Item(0)(8)

            'Select Case _nNarcoticValue
            '    Case 0
            '        cmbNarcotics.Text = "C1"
            '    Case 1
            '        cmbNarcotics.Text = "C2"
            '    Case 2
            '        cmbNarcotics.Text = "C3"
            '    Case 3
            '        cmbNarcotics.Text = "C4"
            '    Case 4
            '        cmbNarcotics.Text = "C5"

            'End Select
            'Changes done for case :GLO2010-0009973 i.e Narcotics flag in software doesn't equal narcotics flag set in database
            ' -- Start --
            Select Case _nNarcoticValue
                Case 0
                    cmbNarcotics.Text = "Non-Scheduled"
                Case 2
                    cmbNarcotics.Text = "Schedule II"
                Case 3
                    cmbNarcotics.Text = "Schedule III"
                Case 4
                    cmbNarcotics.Text = "Schedule IV"
                Case 5
                    cmbNarcotics.Text = "Schedule V"
            End Select
            ' -- end ---
            'End of code change for resolving case :GLO2010-0009973

            'If dv.Item(0)(8) = 0 Then
            '    cmbNarcotics.Text = "C1"
            'ElseIf dv.Item(0)(8) = 1 Then
            '    cmbNarcotics.Text = "C2"
            'End If

            Dim Chk As Integer
            Chk = dv.Item(0)(6)
            If Chk = 0 Then
                chkClinicDrug.CheckState = CheckState.Unchecked
            Else
                chkClinicDrug.CheckState = CheckState.Checked
            End If

            'Dim Chk As Integer
            Chk = dv.Item(0)("bIsAllergicDrug")
            If Chk = 0 Then
                chkAllergicDrug.CheckState = CheckState.Unchecked
            Else
                chkAllergicDrug.CheckState = CheckState.Checked
            End If
            If dv.Item(0)("mpid") <> 0 OrElse objclsDrugs.CheckNDCinUsed(Convert.ToString(dv.Item(0)("NDCCode"))) Then
                DisableControls()
            End If
        Else
            Me.DialogResult = DialogResult.Cancel
        End If

    End Sub

    Private Sub CloseMSTDrugs()

        CancelClick = True
        Me.Close()
    End Sub

    Public Property DrugID() As Int64
        Get
            Return m_ID
        End Get
        Set(ByVal Value As Int64)
            m_ID = Value
        End Set
    End Property

    Public Property IsClinicalDrug() As Int64
        Get
            Return m_blnIsClinical
        End Get
        Set(ByVal Value As Int64)
            m_blnIsClinical = Value
        End Set
    End Property

    Private Sub frmMSTDrugs_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If chkClinicDrug.CheckState = CheckState.Checked Then
            _DrugCategory = 2
            'ElseIf chkClinicDrug.CheckState = CheckState.Unchecked Then
            'Shubhangi 20091203 Check for allergic drug also else set that for all drugs category.
        ElseIf chkAllergicDrug.CheckState = CheckState.Checked Then
            _DrugCategory = 4
            'Commented by Shubhangi 20091203 Bcoz Drug Category 3 is not in use now
            '_DrugCategory = 3 
        Else
            _DrugCategory = 1
        End If
    End Sub

    Private Sub tlsp_MSTDrugs_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_MSTDrugs.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    ''Dim sNDC As String = GenerateFakeNDC()
                    ''Exit Sub
                    SaveMSTDrugs()

                Case "Close"
                    CloseMSTDrugs()

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub txtDuration_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDuration.TextChanged
        Dim strLastVal As String = "" ''''''fixed bug 2876 
        Dim blnResettoLastval As Boolean = False
        Try
            If txtDuration.Text.Trim.Length > 0 Then
                If IsNumeric(txtDuration.Text.Trim) Then
                    Dim nDuration As Int64 = Convert.ToInt64(txtDuration.Text.Trim)
                    If nDuration > 999 Then
                        strLastVal = txtDuration.Text.Trim
                        '''''refer email dated by aniket on 10 nov 2009 sub:Issue 2876
                        MessageBox.Show("Maximum duration should not be more than 999", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtDuration.Text = ""
                        'blnResettoLastval = True
                    End If

                End If
            End If

            'If blnResettoLastval = True Then
            '    txtDuration.Text = strLastVal.Remove(strLastVal.Length - 1) ''''''fixed bug 2876
            '    Exit Sub
            'End If


        Catch ex As Exception
            'txtDuration.Text = strLastVal.Remove(strLastVal.Length - 1) ''''''fixed bug 2876
            MessageBox.Show("Maximum duration should not be more than 999", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDuration.Text = ""
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    '' issue: 5493 
    ''20091205 Added as per pravin sir discussion: Duration should be numeric value
    Private Sub txtDuration_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDuration.KeyPress
        Try
            If _IsSystemAddedDrug = True Then
                Exit Sub
            End If
            Dim chkNumeric As String = txtDuration.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub
            End If

            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                        MessageBox.Show("Enter valid Numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    Else
                        MessageBox.Show("Enter valid Numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    ''bugs no:5494 
    Private Sub txtAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            If _IsSystemAddedDrug = True Then
                Exit Sub
            End If
            Dim chkNumeric As String = txtAmount.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub

            End If

            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid Numeric or Decimal value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub txtNDCCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNDCCode.KeyPress
        Try
            If _IsSystemAddedDrug = True Then
                Exit Sub
            End If
            Dim chkNumeric As String = txtNDCCode.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub
            End If

            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then
                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then
                        MessageBox.Show("Enter valid Numeric number", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    Else
                        MessageBox.Show("Enter valid Numeric number", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'Developer:Pradeep/'Date:01/18/2012/'PER or Productbacklog/Reason: 'function added to lock editing of system added drugs
    Private Sub DisableControls()
        Try
            _IsSystemAddedDrug = True
            txtDrugName.ReadOnly = True
            txtGenericName.ReadOnly = True
            txtDosage.ReadOnly = True
            txtRoute.ReadOnly = True
            'txtFrequency.ReadOnly = True
            'txtDuration.ReadOnly = True
            'txtAmount.ReadOnly = True
            'txtDoseUnit.ReadOnly = True
            txtNDCCode.ReadOnly = True
            'cmbDuration.Enabled = False
            cmbDrugsForm.Enabled = False
            cmbNarcotics.Enabled = False

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub FillPotencyCode()
        Dim dtPotencyCode As New DataTable

        'Dim oDblayer As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(0)

        Try
            If cmbDoseUnit.Items.Count = 0 Then
                Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    dtPotencyCode = helper.GetPotencyCode()
                End Using
                If dtPotencyCode IsNot Nothing Then
                    If dtPotencyCode.Rows.Count > 0 Then

                        Dim dr As DataRow = dtPotencyCode.NewRow()
                        dr("sPotencycode") = "0"
                        dr("sDescription") = ""
                        dtPotencyCode.Rows.InsertAt(dr, 0)
                        dtPotencyCode.AcceptChanges()

                        cmbDoseUnit.DataSource = dtPotencyCode
                        cmbDoseUnit.ValueMember = dtPotencyCode.Columns("sPotencycode").ColumnName
                        cmbDoseUnit.DisplayMember = dtPotencyCode.Columns("sDescription").ColumnName

                    End If
                End If
            End If

            If m_AlternativeFormID = 23 Then '23 : ORAL LIQUID/ ML
                cmbDoseUnit.Enabled = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Not IsNothing(dtPotencyCode) Then
                dtPotencyCode.Dispose()
                dtPotencyCode = Nothing
            End If
            'If Not IsNothing(oDblayer) Then
            '    oDblayer.Dispose()
            '    oDblayer = Nothing
            'End If
        Finally
            'If Not IsNothing(oDblayer) Then
            '    oDblayer.Dispose()
            '    oDblayer = Nothing
            'End If
        End Try
    End Sub

End Class
