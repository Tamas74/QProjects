'Imports Microsoft.Office.Core 
Imports Wd = Microsoft.Office.Interop.Word
'Imports gloEMR.gloEMRWord
Imports System.IO
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports gloEmdeonCommon
Imports System.Drawing
Imports gloUserControlLibrary


Public Class frmSelectContactFAXWithFAXCoverPage
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByRef pFAXContactsCollection As Collection)

        MyBase.New()
        If (IsNothing(pFAXContactsCollection) = True) Then
            pFAXContactsCollection = New Collection
        End If
        FAXContactsCollection = pFAXContactsCollection

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                Try
                    components.Dispose()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Try

                    Dim cntmnuControls As System.Windows.Forms.ContextMenu() = {cmnuAddFaxTo, cmnuDeleteFaxTo}

                    If Not IsNothing(cntmnuControls) Then

                        If (cntmnuControls.Length > 0) Then

                            gloGlobal.cEventHelper.RemoveAllEventHandlers(cntmnuControls)

                        End If
                    End If
                    gloGlobal.cEventHelper.DisposeContextMenu(cntmnuControls)

                Catch ex As Exception

                End Try




            End If
            '22-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(dtFillTemplates) = False Then
                dtFillTemplates.Dispose()
                dtFillTemplates = Nothing    'Change made to solve memory Leak and word crash issue
            End If

            'If IsNothing(fntHighPriority) = False Then
            '    fntHighPriority.Dispose()
            '    fntHighPriority = Nothing
            'End If

            'If IsNothing(fntNormalPriority) = False Then
            '    fntNormalPriority.Dispose()
            '    fntNormalPriority = Nothing
            'End If

            '24-Apr-13 Aniket: Cannot dispose this Collection as it is used after the form closes.
            'If IsNothing(FAXContactsCollection) = False Then
            '    FAXContactsCollection.Clear()
            '    FAXContactsCollection = Nothing
            'End If

            If IsNothing(ContactDBLayer) = False Then
                ContactDBLayer.Dispose()
                ContactDBLayer = Nothing
            End If

            If IsNothing(_dtBatchRefFaxInfo) = False Then
                _dtBatchRefFaxInfo.Dispose()
                _dtBatchRefFaxInfo = Nothing
            End If


        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlCommand As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUpdateFAXNo1 As System.Windows.Forms.Button
    Friend WithEvents txtFAXNo As System.Windows.Forms.TextBox
    Friend WithEvents lblFAXNo As System.Windows.Forms.Label
    Friend WithEvents lblContactPerson As System.Windows.Forms.Label
    Friend WithEvents pnlCoverPage As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents trvContact As System.Windows.Forms.TreeView
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents optNormalPriority As System.Windows.Forms.RadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dsoFAXPreview As AxDSOFramer.AxFramerControl
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmnuAddFaxTo As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuFaxTo As System.Windows.Forms.MenuItem
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents trvFaxTo As System.Windows.Forms.TreeView
    Friend WithEvents cmnuDeleteFaxTo As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_SelectContactFAXWithFAXCoverPage As gloGlobal.gloToolStripIgnoreFocus
    Public WithEvents ts_btnShowGrid As System.Windows.Forms.ToolStripButton
    Public WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Public WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Public WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Public WithEvents ts_btnHideGrid As System.Windows.Forms.ToolStripButton
    Public WithEvents ts_btnHidePreView As System.Windows.Forms.ToolStripButton
    Private WithEvents pnltrvFaxTo As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents pnltrvContact As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Splitter4 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pnlFaxCoverPg As System.Windows.Forms.Panel
    Friend WithEvents pnlSelectFAXNo As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp1 As System.Windows.Forms.Button
    Friend WithEvents btnDown1 As System.Windows.Forms.Button
    Friend WithEvents C1Contacts As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Public WithEvents btnUpdateFAXNo As System.Windows.Forms.ToolStripButton
    Friend WithEvents mskFaxNo As gloMaskControl.gloMaskBox
    Public WithEvents tsb_batchRefFax As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnl_txtSearch As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtClearSearch As System.Windows.Forms.Button
    Private WithEvents Label63 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents optHighPriority As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectContactFAXWithFAXCoverPage))
        Me.pnlCommand = New System.Windows.Forms.Panel()
        Me.lblContactPerson = New System.Windows.Forms.Label()
        Me.lblFAXNo = New System.Windows.Forms.Label()
        Me.mskFaxNo = New gloMaskControl.gloMaskBox()
        Me.txtFAXNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnDown1 = New System.Windows.Forms.Button()
        Me.btnUp1 = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnUpdateFAXNo1 = New System.Windows.Forms.Button()
        Me.optHighPriority = New System.Windows.Forms.RadioButton()
        Me.optNormalPriority = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlCoverPage = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.dsoFAXPreview = New AxDSOFramer.AxFramerControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.trvFaxTo = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnl_txtSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtClearSearch = New System.Windows.Forms.Button()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnltrvFaxTo = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Splitter4 = New System.Windows.Forms.Splitter()
        Me.pnltrvContact = New System.Windows.Forms.Panel()
        Me.trvContact = New System.Windows.Forms.TreeView()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.cmnuAddFaxTo = New System.Windows.Forms.ContextMenu()
        Me.mnuFaxTo = New System.Windows.Forms.MenuItem()
        Me.cmnuDeleteFaxTo = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.pnlFaxCoverPg = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.pnlSelectFAXNo = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.C1Contacts = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tlsp_SelectContactFAXWithFAXCoverPage = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnShowGrid = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnHideGrid = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnHidePreView = New System.Windows.Forms.ToolStripButton()
        Me.btnUpdateFAXNo = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.tsb_batchRefFax = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlCommand.SuspendLayout()
        Me.pnlCoverPage.SuspendLayout()
        CType(Me.dsoFAXPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnl_txtSearch.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnltrvFaxTo.SuspendLayout()
        Me.pnltrvContact.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlFaxCoverPg.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlSelectFAXNo.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.C1Contacts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsp_SelectContactFAXWithFAXCoverPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlCommand
        '
        Me.pnlCommand.BackColor = System.Drawing.Color.Transparent
        Me.pnlCommand.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Blue2007
        Me.pnlCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCommand.Controls.Add(Me.lblContactPerson)
        Me.pnlCommand.Controls.Add(Me.lblFAXNo)
        Me.pnlCommand.Controls.Add(Me.mskFaxNo)
        Me.pnlCommand.Controls.Add(Me.txtFAXNo)
        Me.pnlCommand.Controls.Add(Me.Label2)
        Me.pnlCommand.Controls.Add(Me.Label18)
        Me.pnlCommand.Controls.Add(Me.btnDown1)
        Me.pnlCommand.Controls.Add(Me.btnUp1)
        Me.pnlCommand.Controls.Add(Me.Label20)
        Me.pnlCommand.Controls.Add(Me.Label17)
        Me.pnlCommand.Controls.Add(Me.Label19)
        Me.pnlCommand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCommand.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlCommand.Location = New System.Drawing.Point(0, 3)
        Me.pnlCommand.Name = "pnlCommand"
        Me.pnlCommand.Size = New System.Drawing.Size(761, 25)
        Me.pnlCommand.TabIndex = 1
        '
        'lblContactPerson
        '
        Me.lblContactPerson.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblContactPerson.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactPerson.ForeColor = System.Drawing.Color.White
        Me.lblContactPerson.Location = New System.Drawing.Point(119, 1)
        Me.lblContactPerson.Name = "lblContactPerson"
        Me.lblContactPerson.Size = New System.Drawing.Size(426, 23)
        Me.lblContactPerson.TabIndex = 2
        Me.lblContactPerson.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFAXNo
        '
        Me.lblFAXNo.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXNo.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblFAXNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFAXNo.ForeColor = System.Drawing.Color.White
        Me.lblFAXNo.Location = New System.Drawing.Point(545, 1)
        Me.lblFAXNo.Name = "lblFAXNo"
        Me.lblFAXNo.Size = New System.Drawing.Size(67, 23)
        Me.lblFAXNo.TabIndex = 3
        Me.lblFAXNo.Text = "Fax No.: "
        Me.lblFAXNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mskFaxNo
        '
        Me.mskFaxNo.AllowValidate = True
        Me.mskFaxNo.Dock = System.Windows.Forms.DockStyle.Right
        Me.mskFaxNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskFaxNo.IncludeLiteralsAndPrompts = False
        Me.mskFaxNo.Location = New System.Drawing.Point(612, 1)
        Me.mskFaxNo.MaskType = gloMaskControl.gloMaskType.Fax
        Me.mskFaxNo.Name = "mskFaxNo"
        Me.mskFaxNo.ReadOnly = False
        Me.mskFaxNo.Size = New System.Drawing.Size(100, 23)
        Me.mskFaxNo.TabIndex = 13
        '
        'txtFAXNo
        '
        Me.txtFAXNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFAXNo.ForeColor = System.Drawing.Color.Black
        Me.txtFAXNo.Location = New System.Drawing.Point(545, 1)
        Me.txtFAXNo.Name = "txtFAXNo"
        Me.txtFAXNo.Size = New System.Drawing.Size(167, 22)
        Me.txtFAXNo.TabIndex = 4
        Me.txtFAXNo.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 23)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Contact Person: "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 23)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "label4"
        '
        'btnDown1
        '
        Me.btnDown1.BackColor = System.Drawing.Color.Transparent
        Me.btnDown1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDown1.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDown1.FlatAppearance.BorderSize = 0
        Me.btnDown1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown1.Location = New System.Drawing.Point(712, 1)
        Me.btnDown1.Name = "btnDown1"
        Me.btnDown1.Size = New System.Drawing.Size(24, 23)
        Me.btnDown1.TabIndex = 12
        Me.btnDown1.UseVisualStyleBackColor = False
        '
        'btnUp1
        '
        Me.btnUp1.BackColor = System.Drawing.Color.Transparent
        Me.btnUp1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnUp1.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp1.FlatAppearance.BorderSize = 0
        Me.btnUp1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp1.Location = New System.Drawing.Point(736, 1)
        Me.btnUp1.Name = "btnUp1"
        Me.btnUp1.Size = New System.Drawing.Size(24, 23)
        Me.btnUp1.TabIndex = 11
        Me.btnUp1.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(760, 1)
        Me.Label20.TabIndex = 7
        Me.Label20.Text = "label1"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(0, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(760, 1)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(760, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 25)
        Me.Label19.TabIndex = 8
        Me.Label19.Text = "label3"
        '
        'btnUpdateFAXNo1
        '
        Me.btnUpdateFAXNo1.BackColor = System.Drawing.Color.Transparent
        Me.btnUpdateFAXNo1.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Button
        Me.btnUpdateFAXNo1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUpdateFAXNo1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUpdateFAXNo1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUpdateFAXNo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateFAXNo1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateFAXNo1.Location = New System.Drawing.Point(850, 12)
        Me.btnUpdateFAXNo1.Name = "btnUpdateFAXNo1"
        Me.btnUpdateFAXNo1.Size = New System.Drawing.Size(69, 23)
        Me.btnUpdateFAXNo1.TabIndex = 5
        Me.btnUpdateFAXNo1.Text = "Update FAX No"
        Me.btnUpdateFAXNo1.UseVisualStyleBackColor = False
        Me.btnUpdateFAXNo1.Visible = False
        '
        'optHighPriority
        '
        Me.optHighPriority.BackColor = System.Drawing.Color.Transparent
        Me.optHighPriority.Dock = System.Windows.Forms.DockStyle.Left
        Me.optHighPriority.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optHighPriority.Location = New System.Drawing.Point(220, 1)
        Me.optHighPriority.Name = "optHighPriority"
        Me.optHighPriority.Size = New System.Drawing.Size(156, 22)
        Me.optHighPriority.TabIndex = 1
        Me.optHighPriority.Text = "Send Immediately"
        Me.optHighPriority.UseVisualStyleBackColor = False
        '
        'optNormalPriority
        '
        Me.optNormalPriority.BackColor = System.Drawing.Color.Transparent
        Me.optNormalPriority.Checked = True
        Me.optNormalPriority.Dock = System.Windows.Forms.DockStyle.Left
        Me.optNormalPriority.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optNormalPriority.Location = New System.Drawing.Point(89, 1)
        Me.optNormalPriority.Name = "optNormalPriority"
        Me.optNormalPriority.Size = New System.Drawing.Size(131, 22)
        Me.optNormalPriority.TabIndex = 0
        Me.optNormalPriority.TabStop = True
        Me.optNormalPriority.Text = "Normal Priority"
        Me.optNormalPriority.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 22)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "   Fax Priority :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCoverPage
        '
        Me.pnlCoverPage.Controls.Add(Me.Label25)
        Me.pnlCoverPage.Controls.Add(Me.Label26)
        Me.pnlCoverPage.Controls.Add(Me.Label27)
        Me.pnlCoverPage.Controls.Add(Me.Label28)
        Me.pnlCoverPage.Controls.Add(Me.dsoFAXPreview)
        Me.pnlCoverPage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCoverPage.Location = New System.Drawing.Point(0, 27)
        Me.pnlCoverPage.Name = "pnlCoverPage"
        Me.pnlCoverPage.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlCoverPage.Size = New System.Drawing.Size(764, 401)
        Me.pnlCoverPage.TabIndex = 1
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(1, 397)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(759, 1)
        Me.Label25.TabIndex = 8
        Me.Label25.Text = "label2"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 397)
        Me.Label26.TabIndex = 7
        Me.Label26.Text = "label4"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(760, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 397)
        Me.Label27.TabIndex = 6
        Me.Label27.Text = "label3"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(0, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(761, 1)
        Me.Label28.TabIndex = 5
        Me.Label28.Text = "label1"
        '
        'dsoFAXPreview
        '
        Me.dsoFAXPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dsoFAXPreview.Enabled = True
        Me.dsoFAXPreview.Location = New System.Drawing.Point(0, 0)
        Me.dsoFAXPreview.Name = "dsoFAXPreview"
        Me.dsoFAXPreview.OcxState = CType(resources.GetObject("dsoFAXPreview.OcxState"), System.Windows.Forms.AxHost.State)
        Me.dsoFAXPreview.Size = New System.Drawing.Size(761, 398)
        Me.dsoFAXPreview.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.optHighPriority)
        Me.Panel2.Controls.Add(Me.optNormalPriority)
        Me.Panel2.Controls.Add(Me.cmbTemplate)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(761, 24)
        Me.Panel2.TabIndex = 0
        Me.Panel2.TabStop = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(376, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(2, 5, 2, 2)
        Me.Label5.Size = New System.Drawing.Size(218, 22)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Select Template :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTemplate.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplate.Location = New System.Drawing.Point(594, 1)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(166, 22)
        Me.cmbTemplate.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(1, 23)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(759, 1)
        Me.Label21.TabIndex = 14
        Me.Label21.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 23)
        Me.Label22.TabIndex = 13
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(760, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 23)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(761, 1)
        Me.Label24.TabIndex = 11
        Me.Label24.Text = "label1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(761, 22)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "    Fax cover page preview"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'trvFaxTo
        '
        Me.trvFaxTo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvFaxTo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvFaxTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvFaxTo.ForeColor = System.Drawing.Color.Black
        Me.trvFaxTo.HideSelection = False
        Me.trvFaxTo.ImageIndex = 0
        Me.trvFaxTo.ImageList = Me.ImageList1
        Me.trvFaxTo.ItemHeight = 19
        Me.trvFaxTo.Location = New System.Drawing.Point(4, 5)
        Me.trvFaxTo.Name = "trvFaxTo"
        Me.trvFaxTo.SelectedImageIndex = 0
        Me.trvFaxTo.Size = New System.Drawing.Size(222, 329)
        Me.trvFaxTo.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Insurance.ico")
        Me.ImageList1.Images.SetKeyName(1, "Other.ico")
        Me.ImageList1.Images.SetKeyName(2, "Pharmacy.ico")
        Me.ImageList1.Images.SetKeyName(3, "Physician.ico")
        Me.ImageList1.Images.SetKeyName(4, "Contact_03.ico")
        Me.ImageList1.Images.SetKeyName(5, "Hospital_05.ico")
        Me.ImageList1.Images.SetKeyName(6, "Pharmacy.ico")
        Me.ImageList1.Images.SetKeyName(7, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(8, "epharmacy.ico")
        Me.ImageList1.Images.SetKeyName(9, "Other.ico")
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Button
        Me.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearch.Controls.Add(Me.pnl_txtSearch)
        Me.pnlSearch.Controls.Add(Me.lblSearch)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Controls.Add(Me.Label10)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearch.Location = New System.Drawing.Point(0, 3)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(761, 24)
        Me.pnlSearch.TabIndex = 0
        '
        'pnl_txtSearch
        '
        Me.pnl_txtSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnl_txtSearch.Controls.Add(Me.txtSearch)
        Me.pnl_txtSearch.Controls.Add(Me.Label77)
        Me.pnl_txtSearch.Controls.Add(Me.Label61)
        Me.pnl_txtSearch.Controls.Add(Me.Label62)
        Me.pnl_txtSearch.Controls.Add(Me.txtClearSearch)
        Me.pnl_txtSearch.Controls.Add(Me.Label63)
        Me.pnl_txtSearch.Controls.Add(Me.Label64)
        Me.pnl_txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_txtSearch.ForeColor = System.Drawing.Color.Black
        Me.pnl_txtSearch.Location = New System.Drawing.Point(64, 1)
        Me.pnl_txtSearch.Name = "pnl_txtSearch"
        Me.pnl_txtSearch.Size = New System.Drawing.Size(241, 22)
        Me.pnl_txtSearch.TabIndex = 231
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.MaxLength = 50
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(212, 15)
        Me.txtSearch.TabIndex = 0
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 17)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(212, 5)
        Me.Label77.TabIndex = 43
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.White
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label61.Location = New System.Drawing.Point(5, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(212, 3)
        Me.Label61.TabIndex = 37
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.White
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label62.Location = New System.Drawing.Point(1, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(4, 22)
        Me.Label62.TabIndex = 38
        '
        'txtClearSearch
        '
        Me.txtClearSearch.BackgroundImage = CType(resources.GetObject("txtClearSearch.BackgroundImage"), System.Drawing.Image)
        Me.txtClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.txtClearSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.txtClearSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtClearSearch.FlatAppearance.BorderSize = 0
        Me.txtClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.txtClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.txtClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.txtClearSearch.Image = CType(resources.GetObject("txtClearSearch.Image"), System.Drawing.Image)
        Me.txtClearSearch.Location = New System.Drawing.Point(217, 0)
        Me.txtClearSearch.Name = "txtClearSearch"
        Me.txtClearSearch.Size = New System.Drawing.Size(23, 22)
        Me.txtClearSearch.TabIndex = 41
        Me.txtClearSearch.UseVisualStyleBackColor = True
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label63.Location = New System.Drawing.Point(0, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 22)
        Me.Label63.TabIndex = 39
        Me.Label63.Text = "label4"
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label64.Location = New System.Drawing.Point(240, 0)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 22)
        Me.Label64.TabIndex = 40
        Me.Label64.Text = "label4"
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(63, 22)
        Me.lblSearch.TabIndex = 4
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(1, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(759, 1)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 23)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(760, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 23)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(761, 1)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "label1"
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.pnltrvFaxTo)
        Me.pnlLeft.Controls.Add(Me.Splitter4)
        Me.pnlLeft.Controls.Add(Me.pnltrvContact)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(227, 659)
        Me.pnlLeft.TabIndex = 1
        '
        'pnltrvFaxTo
        '
        Me.pnltrvFaxTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrvFaxTo.Controls.Add(Me.trvFaxTo)
        Me.pnltrvFaxTo.Controls.Add(Me.Label30)
        Me.pnltrvFaxTo.Controls.Add(Me.Label1)
        Me.pnltrvFaxTo.Controls.Add(Me.Label6)
        Me.pnltrvFaxTo.Controls.Add(Me.Label7)
        Me.pnltrvFaxTo.Controls.Add(Me.Label8)
        Me.pnltrvFaxTo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvFaxTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvFaxTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnltrvFaxTo.Location = New System.Drawing.Point(0, 321)
        Me.pnltrvFaxTo.Name = "pnltrvFaxTo"
        Me.pnltrvFaxTo.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrvFaxTo.Size = New System.Drawing.Size(227, 338)
        Me.pnltrvFaxTo.TabIndex = 0
        Me.pnltrvFaxTo.TabStop = True
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.White
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Location = New System.Drawing.Point(4, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(222, 4)
        Me.Label30.TabIndex = 39
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 334)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(222, 1)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 334)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(226, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 334)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(224, 1)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "label1"
        '
        'Splitter4
        '
        Me.Splitter4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter4.Location = New System.Drawing.Point(0, 317)
        Me.Splitter4.Name = "Splitter4"
        Me.Splitter4.Size = New System.Drawing.Size(227, 4)
        Me.Splitter4.TabIndex = 11
        Me.Splitter4.TabStop = False
        '
        'pnltrvContact
        '
        Me.pnltrvContact.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrvContact.Controls.Add(Me.trvContact)
        Me.pnltrvContact.Controls.Add(Me.Label29)
        Me.pnltrvContact.Controls.Add(Me.lbl_BottomBrd)
        Me.pnltrvContact.Controls.Add(Me.lbl_LeftBrd)
        Me.pnltrvContact.Controls.Add(Me.lbl_RightBrd)
        Me.pnltrvContact.Controls.Add(Me.lbl_TopBrd)
        Me.pnltrvContact.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltrvContact.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvContact.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnltrvContact.Location = New System.Drawing.Point(0, 0)
        Me.pnltrvContact.Name = "pnltrvContact"
        Me.pnltrvContact.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnltrvContact.Size = New System.Drawing.Size(227, 317)
        Me.pnltrvContact.TabIndex = 1
        Me.pnltrvContact.TabStop = True
        '
        'trvContact
        '
        Me.trvContact.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvContact.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvContact.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvContact.ForeColor = System.Drawing.Color.Black
        Me.trvContact.HideSelection = False
        Me.trvContact.ImageIndex = 0
        Me.trvContact.ImageList = Me.ImageList1
        Me.trvContact.ItemHeight = 20
        Me.trvContact.Location = New System.Drawing.Point(4, 8)
        Me.trvContact.Name = "trvContact"
        Me.trvContact.SelectedImageIndex = 0
        Me.trvContact.Size = New System.Drawing.Size(222, 308)
        Me.trvContact.TabIndex = 0
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.White
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Location = New System.Drawing.Point(4, 4)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(222, 4)
        Me.Label29.TabIndex = 38
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 316)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(222, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 313)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(226, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 313)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(224, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'cmnuAddFaxTo
        '
        Me.cmnuAddFaxTo.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFaxTo})
        '
        'mnuFaxTo
        '
        Me.mnuFaxTo.Index = 0
        Me.mnuFaxTo.Text = "Add To Selected Contacts"
        '
        'cmnuDeleteFaxTo
        '
        Me.cmnuDeleteFaxTo.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Delete Selected Contact"
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.pnlFaxCoverPg)
        Me.pnlBottom.Controls.Add(Me.Panel4)
        Me.pnlBottom.Controls.Add(Me.pnlSelectFAXNo)
        Me.pnlBottom.Controls.Add(Me.Panel7)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(764, 659)
        Me.pnlBottom.TabIndex = 0
        '
        'pnlFaxCoverPg
        '
        Me.pnlFaxCoverPg.Controls.Add(Me.pnlCoverPage)
        Me.pnlFaxCoverPg.Controls.Add(Me.Panel6)
        Me.pnlFaxCoverPg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFaxCoverPg.Location = New System.Drawing.Point(0, 231)
        Me.pnlFaxCoverPg.Name = "pnlFaxCoverPg"
        Me.pnlFaxCoverPg.Size = New System.Drawing.Size(764, 428)
        Me.pnlFaxCoverPg.TabIndex = 1
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel6.Size = New System.Drawing.Size(764, 27)
        Me.Panel6.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 203)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(764, 28)
        Me.Panel4.TabIndex = 210
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), System.Drawing.Image)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.btnUp)
        Me.Panel5.Controls.Add(Me.btnDown)
        Me.Panel5.Controls.Add(Me.Label31)
        Me.Panel5.Controls.Add(Me.Label32)
        Me.Panel5.Controls.Add(Me.Label33)
        Me.Panel5.Controls.Add(Me.Label34)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(0, 3)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(761, 22)
        Me.Panel5.TabIndex = 19
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(712, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(24, 20)
        Me.btnUp.TabIndex = 9
        Me.btnUp.UseVisualStyleBackColor = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(736, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(24, 20)
        Me.btnDown.TabIndex = 10
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(1, 21)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(759, 1)
        Me.Label31.TabIndex = 8
        Me.Label31.Text = "label2"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(0, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1, 21)
        Me.Label32.TabIndex = 7
        Me.Label32.Text = "label4"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(760, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 21)
        Me.Label33.TabIndex = 6
        Me.Label33.Text = "label3"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(0, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(761, 1)
        Me.Label34.TabIndex = 5
        Me.Label34.Text = "label1"
        '
        'pnlSelectFAXNo
        '
        Me.pnlSelectFAXNo.Controls.Add(Me.Panel8)
        Me.pnlSelectFAXNo.Controls.Add(Me.Panel3)
        Me.pnlSelectFAXNo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectFAXNo.Location = New System.Drawing.Point(0, 28)
        Me.pnlSelectFAXNo.Name = "pnlSelectFAXNo"
        Me.pnlSelectFAXNo.Size = New System.Drawing.Size(764, 175)
        Me.pnlSelectFAXNo.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.C1Contacts)
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Controls.Add(Me.Label16)
        Me.Panel8.Controls.Add(Me.Label15)
        Me.Panel8.Controls.Add(Me.Label13)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.ForeColor = System.Drawing.Color.Black
        Me.Panel8.Location = New System.Drawing.Point(0, 30)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel8.Size = New System.Drawing.Size(764, 145)
        Me.Panel8.TabIndex = 1
        Me.Panel8.TabStop = True
        '
        'C1Contacts
        '
        Me.C1Contacts.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Contacts.AllowEditing = False
        Me.C1Contacts.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1Contacts.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Contacts.ColumnInfo = "10,0,0,0,0,105,Columns:"
        Me.C1Contacts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Contacts.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1Contacts.Location = New System.Drawing.Point(1, 1)
        Me.C1Contacts.Name = "C1Contacts"
        Me.C1Contacts.Rows.DefaultSize = 21
        Me.C1Contacts.Size = New System.Drawing.Size(759, 143)
        Me.C1Contacts.StyleInfo = resources.GetString("C1Contacts.StyleInfo")
        Me.C1Contacts.TabIndex = 0
        Me.C1Contacts.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 143)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(760, 1)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "label1"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(760, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 144)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(0, 144)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(761, 1)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "label2"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlSearch)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(764, 30)
        Me.Panel3.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.pnlCommand)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 3, 3, 0)
        Me.Panel7.Size = New System.Drawing.Size(764, 28)
        Me.Panel7.TabIndex = 500
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.pnlRight)
        Me.pnlTop.Controls.Add(Me.Splitter2)
        Me.pnlTop.Controls.Add(Me.pnlLeft)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 54)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(994, 659)
        Me.pnlTop.TabIndex = 0
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.pnlBottom)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(230, 0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(764, 659)
        Me.pnlRight.TabIndex = 0
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Location = New System.Drawing.Point(227, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 659)
        Me.Splitter2.TabIndex = 10
        Me.Splitter2.TabStop = False
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.btnUpdateFAXNo1)
        Me.pnl_tlspTOP.Controls.Add(Me.tlsp_SelectContactFAXWithFAXCoverPage)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(994, 54)
        Me.pnl_tlspTOP.TabIndex = 1
        '
        'tlsp_SelectContactFAXWithFAXCoverPage
        '
        Me.tlsp_SelectContactFAXWithFAXCoverPage.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_SelectContactFAXWithFAXCoverPage.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_SelectContactFAXWithFAXCoverPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_SelectContactFAXWithFAXCoverPage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_SelectContactFAXWithFAXCoverPage.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_SelectContactFAXWithFAXCoverPage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnShowGrid, Me.ts_btnHideGrid, Me.ts_btnHidePreView, Me.btnUpdateFAXNo, Me.ts_btnRefresh, Me.ts_btnOk, Me.tsb_batchRefFax, Me.ts_btnCancel})
        Me.tlsp_SelectContactFAXWithFAXCoverPage.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_SelectContactFAXWithFAXCoverPage.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_SelectContactFAXWithFAXCoverPage.Name = "tlsp_SelectContactFAXWithFAXCoverPage"
        Me.tlsp_SelectContactFAXWithFAXCoverPage.Size = New System.Drawing.Size(994, 53)
        Me.tlsp_SelectContactFAXWithFAXCoverPage.TabIndex = 0
        Me.tlsp_SelectContactFAXWithFAXCoverPage.Text = "toolStrip1"
        '
        'ts_btnShowGrid
        '
        Me.ts_btnShowGrid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnShowGrid.Image = CType(resources.GetObject("ts_btnShowGrid.Image"), System.Drawing.Image)
        Me.ts_btnShowGrid.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnShowGrid.Name = "ts_btnShowGrid"
        Me.ts_btnShowGrid.Size = New System.Drawing.Size(74, 50)
        Me.ts_btnShowGrid.Tag = "ShowGrid"
        Me.ts_btnShowGrid.Text = "&Show Grid"
        Me.ts_btnShowGrid.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnShowGrid.Visible = False
        '
        'ts_btnHideGrid
        '
        Me.ts_btnHideGrid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnHideGrid.Image = CType(resources.GetObject("ts_btnHideGrid.Image"), System.Drawing.Image)
        Me.ts_btnHideGrid.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnHideGrid.Name = "ts_btnHideGrid"
        Me.ts_btnHideGrid.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnHideGrid.Tag = "HideGrid"
        Me.ts_btnHideGrid.Text = "&Hide Grid"
        Me.ts_btnHideGrid.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnHideGrid.Visible = False
        '
        'ts_btnHidePreView
        '
        Me.ts_btnHidePreView.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnHidePreView.Image = CType(resources.GetObject("ts_btnHidePreView.Image"), System.Drawing.Image)
        Me.ts_btnHidePreView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnHidePreView.Name = "ts_btnHidePreView"
        Me.ts_btnHidePreView.Size = New System.Drawing.Size(90, 50)
        Me.ts_btnHidePreView.Tag = "HidePreview"
        Me.ts_btnHidePreView.Text = "&Hide Preview"
        Me.ts_btnHidePreView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnHidePreView.Visible = False
        '
        'btnUpdateFAXNo
        '
        Me.btnUpdateFAXNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateFAXNo.Image = CType(resources.GetObject("btnUpdateFAXNo.Image"), System.Drawing.Image)
        Me.btnUpdateFAXNo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdateFAXNo.Name = "btnUpdateFAXNo"
        Me.btnUpdateFAXNo.Size = New System.Drawing.Size(99, 50)
        Me.btnUpdateFAXNo.Tag = "Update"
        Me.btnUpdateFAXNo.Text = "&Update Fax No"
        Me.btnUpdateFAXNo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnUpdateFAXNo.ToolTipText = "Update Fax Number"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(57, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Fax&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Fax and Close"
        '
        'tsb_batchRefFax
        '
        Me.tsb_batchRefFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_batchRefFax.Image = CType(resources.GetObject("tsb_batchRefFax.Image"), System.Drawing.Image)
        Me.tsb_batchRefFax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_batchRefFax.Name = "tsb_batchRefFax"
        Me.tsb_batchRefFax.Size = New System.Drawing.Size(57, 50)
        Me.tsb_batchRefFax.Tag = "BatchRefFax"
        Me.tsb_batchRefFax.Text = "&Fax&&Cls"
        Me.tsb_batchRefFax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_batchRefFax.ToolTipText = "Fax and Close"
        Me.tsb_batchRefFax.Visible = False
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 713)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(994, 2)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmSelectContactFAXWithFAXCoverPage
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(994, 715)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Controls.Add(Me.Splitter1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSelectContactFAXWithFAXCoverPage"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Contact"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlCommand.ResumeLayout(False)
        Me.pnlCommand.PerformLayout()
        Me.pnlCoverPage.ResumeLayout(False)
        CType(Me.dsoFAXPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnl_txtSearch.ResumeLayout(False)
        Me.pnl_txtSearch.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnltrvFaxTo.ResumeLayout(False)
        Me.pnltrvContact.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlFaxCoverPg.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlSelectFAXNo.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        CType(Me.C1Contacts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlRight.ResumeLayout(False)
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsp_SelectContactFAXWithFAXCoverPage.ResumeLayout(False)
        Me.tlsp_SelectContactFAXWithFAXCoverPage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public strFAXCoverPage As String
    Public PatientID As Long
    Public VisitId As Long
    Public ExamId As Long
    Public ReferralId As Long
    Public strFAXType As String = ""
    Private FAXContactsCollection As Collection = Nothing

    Public Shared strPrintLogFile As String
    Public Shared strBatchRefTemplate As String
    Public Shared _blnInsertExamNotes As Boolean
    Public Shared _blnInsertSelectedExamNotes As Boolean

    Private selContactID As Long = 0
    Private strcontacttype As String
    Private SelectedNodekey As Int64
    Private ContactDBLayer As New ClsContactDBLayer(True)
    Private strAarryOfFields() As Int16
    Private objWord As gloEMRWord.clsWordDocument
    Private objCriteria As gloEMRWord.DocCriteria
    Public oCurDoc As Wd.Document
    Private oWordApp As Wd.Application
    Private _Selectedcontentcontrol As Wd.ContentControl
    Private nRow As Integer = 0
    Private _dtBatchRefFaxInfo As DataTable
    Private _blnIsFromBatchreferral As Boolean
    Private _IsRefresh As Boolean = False
    Private _IsInternetFax As Boolean = False
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _IsMsgShown As Boolean = False
    Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Private WithEvents wdPrint As AxDSOFramer.AxFramerControl
    Private r As Wd.Range
    Private WithEvents oTempDoc As Wd.Document

    '22-Apr-13 Aniket: Resolving Memory Leak Issues
    Private dtFillTemplates As DataTable
    Private fntNormalPriority As Font = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)
    Private fntHighPriority As Font = gloGlobal.clsgloFont.gFont  'New Font("Tahoma", 9, FontStyle.Regular)
    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing

    Public Property dtBatchRefFaxInfo() As DataTable
        Get
            Return _dtBatchRefFaxInfo
        End Get
        Set(ByVal value As DataTable)
            _dtBatchRefFaxInfo = value
        End Set
    End Property

    Public Property blnIsFromBatchreferral() As Boolean
        Get
            Return _blnIsFromBatchreferral
        End Get
        Set(ByVal value As Boolean)
            _blnIsFromBatchreferral = value
        End Set
    End Property

#Region "Liquid Data Variable"
    Dim strValue() As String
    Dim m_elementId As Int64
    Dim m_Required As Boolean
    Dim m_DataType As String
    Dim strTag() As String

    Dim LineStyleDouble As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
    Dim LineStyleSingle As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    Dim TextureNone As Wd.WdTextureIndex = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
    Dim ColorGray10 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
    Dim _IsOpenedFromExam As Boolean = False
    Public Property IsOpenedFromExam() As Boolean
        Get
            Return _IsOpenedFromExam
        End Get
        Set(ByVal value As Boolean)
            _IsOpenedFromExam = value
        End Set
    End Property
#End Region

    Private Sub frmSelectContactFAXWithFAXCoverPage_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        '22-Apr-13 Aniket: Resolving Memory Leak Issues
        If IsNothing(dtFillTemplates) = False Then
            dtFillTemplates.Dispose()
            dtFillTemplates = Nothing    'Change made to solve memory Leak and word crash issue
        End If

        'If IsNothing(fntHighPriority) = False Then
        '    fntHighPriority.Dispose()
        '    fntHighPriority = Nothing
        'End If

        'If IsNothing(fntNormalPriority) = False Then
        '    fntNormalPriority.Dispose()
        '    fntNormalPriority = Nothing
        'End If

        '24-Apr-13 Aniket: Cannot dispose this Collection as it is used after the form closes.
        'If IsNothing(FAXContactsCollection) = False Then
        '    FAXContactsCollection.Clear()
        '    FAXContactsCollection = Nothing
        'End If

        If IsNothing(ContactDBLayer) = False Then
            ContactDBLayer.Dispose()
            ContactDBLayer = Nothing
        End If

        If IsNothing(_dtBatchRefFaxInfo) = False Then
            _dtBatchRefFaxInfo.Dispose()
            _dtBatchRefFaxInfo = Nothing
        End If

    End Sub

    Private Sub frmSelectContactFAXWithFAXCoverPage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        btnUp1.Visible = True
        btnUp1.BackgroundImage = My.Resources.Resources.UP
        btnUp1.BackgroundImageLayout = ImageLayout.Center

        btnDown1.Visible = False

        txtSearch.Select()
        gloC1FlexStyle.Style(C1Contacts)
        Try
            btnUp.Visible = True
            btnUp.BackgroundImage = My.Resources.Resources.UP
            btnUp.BackgroundImage = My.Resources.Resources.Down
            btnUp.BackgroundImageLayout = ImageLayout.Center

            btnDown.Visible = False
            btnDown.BackgroundImage = My.Resources.Resources.UP
            btnDown.BackgroundImageLayout = ImageLayout.Center

            gstrFAXContactPerson = ""
            gstrFAXContactPersonFAXNo = ""


            'For Resloving FaxTo Going Blank even if eFax is Off.
            'Retrieve Internet fax setting from AppSettings "

            'code start by nilesh on 20110328 for case GLO2010-0008730
            'Retrieve Internet fax setting from AppSettings "
            _IsInternetFax = False

            If appSettings("Internet Fax") <> Nothing Then
                If appSettings("Internet Fax") <> "" Then
                    _IsInternetFax = Convert.ToBoolean(appSettings("Internet Fax"))
                End If
            End If
            If _IsInternetFax = False Then
                mskFaxNo.MaskType = gloMaskControl.gloMaskType.Other
            End If
            'code end by nilesh on 20110328 for case GLO2010-0008730


            'sarika 13th nov 07
            ''Bug #57310: 00000547 : UNABLE TO FAX LAB ORDER via CONTACT
            ''Change gstrFAXContacts by gstrfaxCollection
            If Not IsNothing(gstrfaxCollection) Then
                gstrfaxCollection.Clear()
            End If
            If blnIsFromBatchreferral = False Then
                loadPatientStrip()
            End If
            multipleRecipients = False
            '------------

            FillTreeView()       'Fill trvContact tree view 
            'Commented by Mayuri:20090930 in order to make consistency between Contacts form and FAX form
            ' trvContact.ExpandAll()

            ' '' GLO2011-0012907 :  When selecting save and close on notes getting an error
            ' '' If Cover Page is turn on then Fill template in Combo Box.
            If gblnFAXCoverPage = True Then
                FillFaxTemplates()      'Fill combo box with template
            End If

            ts_btnShowGrid.Visible = False
            pnlCoverPage.Visible = True

            'code added by sarika 26th nov 07
            If trvFaxTo.Nodes.Count = 0 Then
                'add the root node
                Dim rootnode As New myTreeNode

                rootnode.Text = "Selected Contacts"
                '    rootnode.Tag = 0
                trvFaxTo.Nodes.Add(rootnode)
                trvFaxTo.Nodes(0).ImageIndex = 4
                trvFaxTo.Nodes(0).SelectedImageIndex = 4

                '22-Apr-13 Aniket: Resolving Memory Leak Issues
                '   rootnode.Dispose()
                rootnode = Nothing  'Change made to solve memory Leak and word crash issue
            End If
            '------

            trvFaxTo.ExpandAll()

            Dim mynode As myTreeNode
            If trvContact.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvContact.Nodes.Item(0).Nodes.Item(0)
                trvContact.SelectedNode = mynode

                trvContact_AfterSelect(Nothing, Nothing)

                '22-Apr-13 Aniket: Resolving Memory Leak Issues
                '  mynode.Dispose()
                mynode = Nothing 'Change made to solve memory Leak and word crash issue
            End If

            Select Case CurrentSendingFAXPriority
                Case mdlFAX.enmFAXPriority.NormalPriority
                    optNormalPriority.Checked = True
                Case mdlFAX.enmFAXPriority.SendImmediately
                    optHighPriority.Checked = True
            End Select

            '' GLO2011-0012907 :  When selecting save and close on notes getting an error
            '' If Cover Page is turn on then Fill template in Combo Box.
            If gblnFAXCoverPage = True Then
                cmbTemplate_SelectionChangeCommitted(Nothing, Nothing)
            End If

            If (_IsOpenedFromExam = True) Then
                lblContactPerson.Text = ""
                mskFaxNo.Text = ""
                gstrFAXContactPerson = ""
                gstrFAXContactPersonFAXNo = ""
            End If

            If (_blnIsFromBatchreferral = True) Then
                tsb_batchRefFax.Visible = True
                ts_btnOk.Visible = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub loadPatientStrip()
        ''slr free previous memory
        If Not IsNothing(_PatientStrip) Then
            pnlTop.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose()
            _PatientStrip = Nothing
        End If
        _PatientStrip = New gloUC_PatientStrip()
        _PatientStrip.ShowDetail(PatientID, gloUC_PatientStrip.enumFormName.Fax)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(3, 0, 3, 0)
        pnlTop.Controls.Add(_PatientStrip)

        'If m_IsReadOnly Then
        '    If (IsNothing(_PatientStrip) = False) Then
        '        _PatientStrip.DTPEnabled = False
        '    End If

        'End If
    End Sub
    Private Sub FillFaxTemplates()

        Try

            objWord = New gloEMRWord.clsWordDocument
            '22-Apr-13 Aniket: Resolving Memory Leak Issues
            dtFillTemplates = objWord.FillTemplates(gloEMRWord.enumTemplateFlag.Fax)

            If Not IsNothing(dtFillTemplates) Then
                If dtFillTemplates.Rows.Count > 0 Then
                    cmbTemplate.DataSource = dtFillTemplates
                    cmbTemplate.DisplayMember = dtFillTemplates.Columns(1).ColumnName
                    cmbTemplate.ValueMember = dtFillTemplates.Columns(0).ColumnName
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub BindGrid()

        '22-Apr-13 Aniket: Resolving Memory Leak Issues
        Dim dt As DataTable = Nothing
        Dim dtReferral As DataTable = Nothing

        C1Contacts.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never
        C1Contacts.Redraw = False


        Try
            Getcontacts()
            C1Contacts.SetDataBinding(ContactDBLayer.DsDataview, "")

            C1Contacts.ScrollBars = ScrollBars.None
            C1Contacts.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            C1Contacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns

            If SelectedNodekey = 1 Or SelectedNodekey = 2 Or SelectedNodekey = 3 Or SelectedNodekey = 4 Or SelectedNodekey = 5 Or SelectedNodekey = 40 Or SelectedNodekey = 41 Or SelectedNodekey = 42 Then    'Check which tree node is selected Using tag stored in Key variable
                ContactDBLayer.SortDataview(ContactDBLayer.DsDataview.Table.Columns("Name").ColumnName)
                HideColumn()
            Else
                If SelectedNodekey = 0 Or SelectedNodekey = 6 Or SelectedNodekey = 43 Or SelectedNodekey = 44 Or SelectedNodekey = 45 Then
                    ContactDBLayer.SortDataview(ContactDBLayer.DsDataview.Table.Columns("LastName").ColumnName)
                    C1Contacts.ScrollBars = ScrollBars.None
                    HideColumn()
                End If
            End If

            If (strcontacttype = "Physician" Or strcontacttype = "Patient Contacts" Or strcontacttype = "Referrals" Or strcontacttype = "Primary Care Physician" Or strcontacttype = "Other Care Team") Then

                Dim RowIndex As Int16 = C1Contacts.FindRow(Convert.ToString(ReferralId), 0, 0, False, True, False)

                If ((String.IsNullOrEmpty(txtSearch.Text) = False) And (ReferralId <> 0)) Then

                Else
                    If (RowIndex < 0) Then
                        If (ReferralId <> 0) Then



                            dt = ContactDBLayer.DsDataview.Table()

                            dtReferral = dt.Clone()
                            Dim sFilter As String = "AND  Contacts_Mst.nContactID = " + Convert.ToString(ReferralId) + " "

                            ContactDBLayer.FetchContacts(strcontacttype, sFilter, PatientID)
                            dtReferral = ContactDBLayer.DsDataview.Table()
                            dt.Merge(dtReferral, True)
                            ContactDBLayer.DsDataview = dt.DefaultView
                            C1Contacts.SetDataBinding(ContactDBLayer.DsDataview, "")
                            RowIndex = C1Contacts.FindRow(Convert.ToString(ReferralId), 0, 0, False, True, False)

                        End If
                    End If
                    C1Contacts.Row = RowIndex
                End If

            End If


        Catch ex As Exception
            Throw ex

        Finally

            C1Contacts.Cols(C1Contacts.Cols.IndexOf("nContactID")).Visible = False
            C1Contacts.Redraw = True
            C1Contacts.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Always
            C1Contacts.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            '22-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

            If IsNothing(dtReferral) = False Then
                dtReferral.Dispose()
                dtReferral = Nothing
            End If

        End Try

        C1Contacts.ScrollBars = ScrollBars.Both

    End Sub

    Private Sub CancelCoverPage()
        Try
            ContactDBLayer = Nothing
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            Me.DialogResult = DialogResult.Cancel
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HideColumn()
        'For C1Flex grid Instead of Datagrid
        'SHUBHANGI 20090917
        'To hide column for 
        If SelectedNodekey = 1 Or SelectedNodekey = 2 Or SelectedNodekey = 3 Or SelectedNodekey = 4 Or SelectedNodekey = 5 Or SelectedNodekey = 40 Or SelectedNodekey = 41 Or SelectedNodekey = 42 Then
            Dim nContactId As Byte = 0
            Dim nPatientName As Byte = 1
            Dim nPatientContact As Byte = 2
            Dim nPhone As Byte = 3
            Dim nMobile As Byte = 4
            Dim nEmail As Byte = 5
            Dim URL As Byte = 6
            Dim Fax As Byte = 7

            'Make Unwated column visible false
            C1Contacts.Cols(nContactId).Width = 0
            ' C1Contacts.Cols(1).Width = 0
            C1Contacts.Cols(nMobile).Width = 0
            C1Contacts.Cols(nEmail).Width = 0
            C1Contacts.Cols(URL).Width = 0

            'Set Name for columns
            nContactId = C1Contacts.Cols.IndexOf("nContactID")
            nPatientName = C1Contacts.Cols.IndexOf("Name")
            nPatientContact = C1Contacts.Cols.IndexOf("Contact")
            nPhone = C1Contacts.Cols.IndexOf("Phone")
            nMobile = C1Contacts.Cols.IndexOf("Mobile")
            nEmail = C1Contacts.Cols.IndexOf("Email")
            URL = C1Contacts.Cols.IndexOf("URL")
            Fax = C1Contacts.Cols.IndexOf("Fax")


            'Set the size of columns
            Dim nWidth As Integer = pnlSelectFAXNo.Width - 10
            C1Contacts.Cols(nPatientName).Width = nWidth / 4
            C1Contacts.Cols(nPatientContact).Width = nWidth / 4
            C1Contacts.Cols(nPhone).Width = nWidth / 4
            C1Contacts.Cols(Fax).Width = nWidth / 4

            ''Start'GLO2011-0012282 gloEMR 6.0.2.2 - FAX REFERRAL & COMPLETE GOES TO WRONG CONTACT
            C1Contacts.Cols(nContactId).Visible = False
        Else

            'Hide column for physician
            'Declare variable for column name
            Dim nContactId As Byte = 0
            Dim nPrefix As Byte = 1
            Dim nPatinetFirstName As Byte = 2
            Dim nPatientMiddleName As Byte = 3
            Dim nPatientLastName As Byte = 4
            Dim nSuffix As Byte = 5
            Dim nPatientPhoneNo As Byte = 6
            Dim nPatientMobileNo As Byte = 7
            Dim nPatientEmail As Byte = 8
            Dim nPatientURL As Byte = 9
            Dim nPatientFaxNo As Byte = 10
            Dim nPatientDegree As Byte = 11

            'Set Name for columns
            nContactId = C1Contacts.Cols.IndexOf("nContactID")
            nPrefix = C1Contacts.Cols.IndexOf("Prefix")
            nPatinetFirstName = C1Contacts.Cols.IndexOf("FirstName")
            nPatientMiddleName = C1Contacts.Cols.IndexOf("MiddleName")
            nPatientLastName = C1Contacts.Cols.IndexOf("LastName")
            nSuffix = C1Contacts.Cols.IndexOf("Suffix")
            nPatientPhoneNo = C1Contacts.Cols.IndexOf("Phone")
            nPatientMobileNo = C1Contacts.Cols.IndexOf("Mobile")
            nPatientEmail = C1Contacts.Cols.IndexOf("Email")
            nPatientURL = C1Contacts.Cols.IndexOf("URL")
            nPatientFaxNo = C1Contacts.Cols.IndexOf("Fax")
            nPatientDegree = C1Contacts.Cols.IndexOf("Degree")

            'Set caption for Column
            C1Contacts.Cols(1).Caption = "Prefix"
            C1Contacts.Cols(2).Caption = "First Name"
            C1Contacts.Cols(3).Caption = "Middle Name"
            C1Contacts.Cols(4).Caption = "Last Name"
            C1Contacts.Cols(5).Caption = "Suffix"

            'Make Unwated column visible false
            C1Contacts.Cols(nContactId).Width = 0
            ' C1Contacts.Cols(1).Width = 0
            C1Contacts.Cols(nPatientMobileNo).Width = 0
            C1Contacts.Cols(nPatientEmail).Width = 0
            C1Contacts.Cols(nPatientURL).Width = 0
            C1Contacts.Cols(nPatientDegree).Width = 0

            'Set the size of columns
            Dim nWidth As Integer = pnlSelectFAXNo.Width - 10
            C1Contacts.Cols(nPrefix).Width = nWidth / 9
            C1Contacts.Cols(nPatinetFirstName).Width = nWidth / 6
            C1Contacts.Cols(nPatientMiddleName).Width = nWidth / 9
            C1Contacts.Cols(nPatientLastName).Width = nWidth / 6
            C1Contacts.Cols(nSuffix).Width = nWidth / 9
            C1Contacts.Cols(nPatientPhoneNo).Width = nWidth / 7.5
            C1Contacts.Cols(nPatientFaxNo).Width = nWidth / 5

            ''Start'GLO2011-0012282 gloEMR 6.0.2.2 - FAX REFERRAL & COMPLETE GOES TO WRONG CONTACT
            C1Contacts.Cols(nContactId).Visible = False
        End If

        'End Shubhangi
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        BindGrid()
        lblContactPerson.Text = ""
        mskFaxNo.Clear()
    End Sub

    Private Sub FillTreeView()


        Dim mynode As myTreeNode
        Dim myRequestNode As myTreeNode
        Dim mychildnode As myTreeNode

        mynode = New myTreeNode("Contacts", -1)
        mynode.ImageIndex = 4
        mynode.SelectedImageIndex = 4
        trvContact.Nodes.Add(mynode)


        mychildnode = New myTreeNode("Physician", 0)
        mychildnode.ImageIndex = 3
        mychildnode.SelectedImageIndex = 3
        mynode.Nodes.Add(mychildnode)
        '  mychildnode.Dispose()
        mychildnode = Nothing 'Change made to solve memory Leak and word crash issue

        mychildnode = New myTreeNode("Hospital", 1)
        mychildnode.ImageIndex = 5
        mychildnode.SelectedImageIndex = 5
        mynode.Nodes.Add(mychildnode)
        '   mychildnode.Dispose()
        mychildnode = Nothing 'Change made to solve memory Leak and word crash issue

        mychildnode = New myTreeNode("Insurance", 2)
        mychildnode.ImageIndex = 0
        mychildnode.SelectedImageIndex = 0
        mynode.Nodes.Add(mychildnode)
        '  mychildnode.Dispose()
        mychildnode = Nothing 'Change made to solve memory Leak and word crash issue

        mychildnode = New myTreeNode("Pharmacy", 3)
        mychildnode.ImageIndex = 2
        mychildnode.SelectedImageIndex = 2
        mynode.Nodes.Add(mychildnode)
        '    mychildnode.Dispose()
        mychildnode = Nothing 'Change made to solve memory Leak and word crash issue

        'Code Added by Mayuri:20090929
        'To add e Pharmacy,New Rx,New Rx &Refill Request,Other Node in treeview
        mychildnode = New myTreeNode(" e Pharmacy", 4)
        mychildnode.ImageIndex = 8
        mychildnode.SelectedImageIndex = 8
        mynode.Nodes.Add(mychildnode)

        myRequestNode = New myTreeNode("New Rx", 40)
        myRequestNode.ImageIndex = 7
        myRequestNode.SelectedImageIndex = 7
        mychildnode.Nodes.Add(myRequestNode)
        '  myRequestNode.Dispose()
        myRequestNode = Nothing 'Change made to solve memory Leak and word crash issue

        myRequestNode = New myTreeNode("New Rx & Refill Request", 41)
        myRequestNode.ImageIndex = 7
        myRequestNode.SelectedImageIndex = 7
        mychildnode.Nodes.Add(myRequestNode)
        '  myRequestNode.Dispose()
        myRequestNode = Nothing 'Change made to solve memory Leak and word crash issue

        myRequestNode = New myTreeNode("Other", 42)
        myRequestNode.ImageIndex = 7
        myRequestNode.SelectedImageIndex = 7
        mychildnode.Nodes.Add(myRequestNode)
        '   myRequestNode.Dispose()
        myRequestNode = Nothing 'Change made to solve memory Leak and word crash issue

        'end code by Mayuri:20090929
        mychildnode = New myTreeNode("Others", 5)
        mychildnode.ImageIndex = 9
        mychildnode.SelectedImageIndex = 9
        mynode.Nodes.Add(mychildnode)
        '    mychildnode.Dispose()
        mychildnode = Nothing   'Change made to solve memory Leak and word crash issue

        '    mynode.Dispose()

        mychildnode = New myTreeNode("Patient Contacts", 6)
        mychildnode.ImageIndex = 4
        mychildnode.SelectedImageIndex = 4
        mynode.Nodes.Add(mychildnode)
        ' mychildnode = Nothing

        myRequestNode = New myTreeNode("Referrals", 43)
        myRequestNode.ImageIndex = 7
        myRequestNode.SelectedImageIndex = 7
        mychildnode.Nodes.Add(myRequestNode)
        myRequestNode = Nothing


        myRequestNode = New myTreeNode("Primary Care Physician", 44)
        myRequestNode.ImageIndex = 7
        myRequestNode.SelectedImageIndex = 7
        mychildnode.Nodes.Add(myRequestNode)
        myRequestNode = Nothing


        myRequestNode = New myTreeNode("Other Care Team", 45)
        myRequestNode.ImageIndex = 7
        myRequestNode.SelectedImageIndex = 7
        mychildnode.Nodes.Add(myRequestNode)
        myRequestNode = Nothing


        mynode = Nothing    'Change made to solve memory Leak and word crash issue
    End Sub

    Private Sub trvContact_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvContact.AfterSelect
        Try
            txtSearch.Text = ""
            Dim mynode As myTreeNode
            'Try
            '    If (IsNothing(Me.C1Contacts.ContextMenu) = False) Then
            '        Me.C1Contacts.ContextMenu.Dispose()
            '        Me.C1Contacts.ContextMenu = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
            C1Contacts.ContextMenu = Nothing
            ''If Not IsNothing(e.Node) Then
            If Not IsNothing(trvContact.SelectedNode) Then
                mynode = CType(trvContact.SelectedNode, myTreeNode)

                SelectedNodekey = mynode.Key
                If SelectedNodekey <> -1 Then
                    Select Case mynode.Key
                        Case 0
                            strcontacttype = "Physician"
                            ContactDBLayer.ContactType = True
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False
                        Case 1
                            strcontacttype = "Hospital"
                            ContactDBLayer.ContactType = False
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False
                        Case 2
                            strcontacttype = "Insurance"
                            ContactDBLayer.ContactType = False
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False
                        Case 3
                            strcontacttype = "Pharmacy"
                            ContactDBLayer.ContactType = False
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False
                            'Code Added by Mayuri:20090929
                            'To add e Pharmacy,New Rx,New Rx &Refill Request,Other Node in treeview
                        Case 4
                            strcontacttype = "e Pharmacy"
                            ContactDBLayer.ContactType = False
                            btnUpdateFAXNo.Visible = False
                            mskFaxNo.ReadOnly = True
                        Case 40
                            strcontacttype = "New Rx"
                            ContactDBLayer.ContactType = False
                            btnUpdateFAXNo.Visible = False
                            mskFaxNo.ReadOnly = True
                        Case 41
                            strcontacttype = "New Rx & Refill Request"
                            ContactDBLayer.ContactType = False
                            btnUpdateFAXNo.Visible = False
                            mskFaxNo.ReadOnly = True
                        Case 42
                            strcontacttype = "Other Service Level"
                            ContactDBLayer.ContactType = False
                            btnUpdateFAXNo.Visible = False
                            mskFaxNo.ReadOnly = True
                            'End code by Mayuri:20090929
                        Case 5
                            strcontacttype = "Others"
                            ContactDBLayer.ContactType = False
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False
                            'End code by Shweta 
                        Case 6
                            strcontacttype = "Patient Contacts"
                            ContactDBLayer.ContactType = True
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False

                        Case 43
                            strcontacttype = "Referrals"
                            ContactDBLayer.ContactType = True
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False

                        Case 44
                            strcontacttype = "Primary Care Physician"
                            ContactDBLayer.ContactType = True
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False

                        Case 45
                            strcontacttype = "Other Care Team"
                            ContactDBLayer.ContactType = True
                            btnUpdateFAXNo.Visible = True
                            mskFaxNo.ReadOnly = False
                    End Select
                    BindGrid()
                Else

                    C1Contacts.DataSource = Nothing
                    C1Contacts.Rows.Count = 1
                    C1Contacts.AllowEditing = False

                End If
                ''Sandp Darade 20100212
                If (mynode.Key <> 0 And mynode.Key <> 6 And mynode.Key <> 43 And mynode.Key <> 44 And mynode.Key <> 45) Then ''if contact type  is not physician 
                    lblContactPerson.Text = ""
                    mskFaxNo.Text = ""
                    gstrFAXContactPerson = ""
                    gstrFAXContactPersonFAXNo = ""
                    C1Contacts.Row = 0
                Else
                    If (ReferralId = 0) Then ''if the contact type is physician  but no referral passed from the calling window 
                        C1Contacts.Row = 0
                        mskFaxNo.Text = ""
                        gstrFAXContactPerson = ""
                        gstrFAXContactPersonFAXNo = ""
                        lblContactPerson.Text = gstrFAXContactPerson
                    End If
                End If
                'Change made to solve memory Leak and word crash issue
                If Not mynode Is Nothing Then
                    '    mynode.Dispose()
                    mynode = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            'If (e.KeyChar = ChrW(13)) Then
            '    If C1Contacts.RowSel >= 0 Then
            '        C1Contacts.Select()
            '        C1Contacts.RowSel = 0
            '    End If
            'End If
            'If (e.KeyChar = ChrW(Keys.Space)) Then
            '    e.Handled = True
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Getcontacts()

        Try

            Dim strsearch As String = txtSearch.Text.Trim()

            Dim strSearchArray As String() = Nothing
            Dim sFilter As String = ""

            strsearch = strsearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]")

            If strsearch.StartsWith("*") = True Then
                strsearch = strsearch.Replace("*", "%")
            End If

            strsearch = strsearch.Replace("*", "[*]")

            If strsearch.Length > 1 Then
                Dim str As String = strsearch.Substring(1)
                strsearch = strsearch.Substring(0, 1) + str
            End If
            If strsearch.Trim() <> "" Then
                strSearchArray = strsearch.Split(",")
            End If

            If (strsearch <> "") Then

                If (strcontacttype = "Physician" Or strcontacttype = "Patient Contacts" Or strcontacttype = "Referrals" Or strcontacttype = "Primary Care Physician" Or strcontacttype = "Other Care Team") Then
                    If strSearchArray.Length = 1 Then
                        sFilter = "AND   ( Contacts_Physician_DTL.sPrefix like ('" & strsearch & "%')OR Contacts_Mst.sFirstName like ('" & strsearch & "%')OR Contacts_Mst.sMiddleName like ('" & strsearch & "%') OR Contacts_Mst.sLastName like ('" & strsearch & "%')OR Contacts_Physician_DTL.sDegree like ('" & strsearch & "%') OR Contacts_Mst.sPhone like ('" & strsearch & "%') OR Contacts_Mst.sFax like ('" & strsearch & "%') )"
                    Else
                        'For Comma separated value search
                        For i As Integer = 0 To strSearchArray.Length - 1
                            strsearch = strSearchArray(i).Trim()

                            If i = 0 Then
                                sFilter = sFilter + " AND "
                                'sFilter = sFilter + " ( " & " sFirstName like ('" & strsearch & "%')OR  sMiddleName like ('" & strsearch & "%') OR sLastName like ('" & strsearch & "%') OR sPhone like ('" & strsearch & "%') OR sFax like ('" & strsearch & "%') "
                                sFilter = sFilter + " ( " & " Contacts_Physician_DTL.sPrefix like ('" & strsearch & "%')OR Contacts_Mst.sFirstName like ('" & strsearch & "%')OR Contacts_Mst.sMiddleName like ('" & strsearch & "%') OR Contacts_Mst.sLastName like ('" & strsearch & "%')OR Contacts_Physician_DTL.sDegree like ('" & strsearch & "%') OR Contacts_Mst.sPhone like ('" & strsearch & "%') OR Contacts_Mst.sFax like ('" & strsearch & "%') "
                                sFilter = sFilter + " ) "
                            Else
                                If sFilter <> "" Then

                                    sFilter = sFilter + " AND "

                                    'sFilter = sFilter + " (" & "  sFirstName like ('" & strsearch & "%')OR sMiddleName like ('" & strsearch & "%')OR  sLastName like ('" & strsearch & "%') OR sPhone like ('" & strsearch & "%') OR sFax like ('" & strsearch & "%') "
                                    sFilter = sFilter + " (" & " Contacts_Physician_DTL.sPrefix like ('" & strsearch & "%')OR Contacts_Mst.sFirstName like ('" & strsearch & "%')OR Contacts_Mst.sMiddleName like ('" & strsearch & "%') OR Contacts_Mst.sLastName like ('" & strsearch & "%')OR Contacts_Physician_DTL.sDegree like ('" & strsearch & "%') OR Contacts_Mst.sPhone like ('" & strsearch & "%') OR Contacts_Mst.sFax like ('" & strsearch & "%')"
                                    sFilter = sFilter + " ) "

                                End If
                            End If

                        Next

                    End If

                Else

                    If strSearchArray.Length = 1 Then
                        sFilter = "AND  ( sName like ('" & strsearch & "%')OR sContact like ('" & strsearch & "%')  OR sPhone like ('" & strsearch & "%') OR sFax like ('" & strsearch & "%') )"
                    Else
                        'For Comma separated value search
                        For i As Integer = 0 To strSearchArray.Length - 1
                            strsearch = strSearchArray(i).Trim()

                            If i = 0 Then
                                sFilter = sFilter + " AND "
                                sFilter = sFilter + " ( " & " sName like ('" & strsearch & "%')OR sContact like ('" & strsearch & "%')  OR sPhone like ('" & strsearch & "%') OR sFax like ('" & strsearch & "%') "
                                sFilter = sFilter + " ) "
                            Else
                                If sFilter <> "" Then

                                    sFilter = sFilter + " AND "

                                    sFilter = sFilter + " (" & "  sName like ('" & strsearch & "%')OR sContact like ('" & strsearch & "%')  OR sPhone like ('" & strsearch & "%') OR sFax like ('" & strsearch & "%') "
                                    sFilter = sFilter + " ) "
                                End If

                            End If

                        Next

                    End If

                End If

            End If

            ContactDBLayer.FetchContacts(strcontacttype, sFilter, PatientID)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

        End Try

    End Sub

    Private Sub OkCoverPageRevised()

        multipleRecipients = False

        If (IsNothing(FAXContactsCollection) = False) Then
            FAXContactsCollection.Clear()
        Else
            FAXContactsCollection = New Collection
        End If

        If optNormalPriority.Checked Then
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        Else
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
        End If

        Dim sFaxName As String = ""
        Dim sFaxNo As String = ""

        If (_IsInternetFax) Then
            If (mskFaxNo.IsValidated = False) Then
                Exit Sub
            End If
        End If

        '' If grid is selected & changed the number manually
        '' Then send the changed number
        If (C1Contacts.RowSel > 0) Then
            If Convert.ToString(C1Contacts.GetData(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("FAX"))) = mskFaxNo.Text Then
                sFaxNo = Convert.ToString(C1Contacts.GetData(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("FAX")))
                ''For "Pharmacy" it will go to else condition and for "Other" fax type it will go to if condition
                If SelectedNodekey = 1 Or SelectedNodekey = 2 Or SelectedNodekey = 3 Or SelectedNodekey = 4 Or SelectedNodekey = 5 Or SelectedNodekey = 40 Or SelectedNodekey = 41 Or SelectedNodekey = 42 Then
                    sFaxName = C1Contacts.Item(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("Name"))
                Else
                    sFaxName = C1Contacts.Item(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("FirstName")) & " " & C1Contacts.Item(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("LastName"))

                End If
            Else
                sFaxNo = mskFaxNo.Text
                '' For Resolving Bug Id : 7654 Application is sending Fax to different  Referrals If we sort the Contact List
                'If lblContactPerson.Text.Trim() <> "" Then
                '    sFaxName = lblContactPerson.Text.Trim()
                'Else
                sFaxName = ""
                lblContactPerson.Text = ""
                'End If
            End If
        Else
            sFaxNo = mskFaxNo.Text
            sFaxName = ""
            lblContactPerson.Text = ""
        End If

        If (trvFaxTo.Nodes(0).Nodes.Count = 0) Then
            If (C1Contacts.RowSel = 0) Then
                If (mskFaxNo.Text <> "") Then
                    '' Manual Fax Number Provided
                    Dim node As New myTreeNode
                    node.FaxTo = mskFaxNo.Text
                    node.FaxName = ""

                    GenerateFaxCoverPage(node.FaxTo, node.FaxName, gstrFAXType)
                    node.FaxCoverPage = strFAXCoverPage
                    FAXContactsCollection.Add(node)

                    '24-Apr-13 Aniket: (Bug 49733) Do not dispose the following node as it causes the FAX Contact and Number to be sent blank
                    'node.Dispose()
                    node = Nothing  'Change made to solve memory Leak and word crash issue
                Else
                    If (_IsInternetFax) Then
                        If (mskFaxNo.IsValidated = False) Then
                            Exit Sub
                        Else
                            If (mskFaxNo.Text = "") Then
                                MessageBox.Show("Please enter the FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                mskFaxNo.Focus()
                                Exit Sub
                            End If
                        End If
                    Else
                        MessageBox.Show("Please enter the FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        mskFaxNo.Focus()
                        Exit Sub
                    End If

                End If
            Else
                If (_IsInternetFax) Then
                    If (mskFaxNo.IsValidated = False) Then
                        Exit Sub
                    Else
                        If (mskFaxNo.Text = "") Then
                            MessageBox.Show("Please enter the FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            mskFaxNo.Focus()
                            Exit Sub
                        End If
                    End If
                Else
                    If (mskFaxNo.Text = "") Then
                        MessageBox.Show("Please enter the FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        mskFaxNo.Focus()
                        Exit Sub
                    End If
                End If

                Dim node As New myTreeNode
                node.FaxTo = sFaxNo
                node.FaxName = sFaxName
                GenerateFaxCoverPage(node.FaxTo, node.FaxName, gstrFAXType)
                node.FaxCoverPage = strFAXCoverPage
                FAXContactsCollection.Add(node)

                '24-Apr-13 Aniket: (Bug 49733) Do not dispose the following node as it causes the FAX Contact and Number to be sent blank
                'node.Dispose()
                node = Nothing 'Change made to solve memory Leak and word crash issue
            End If
        Else
            '' Set the variables 
            multipleRecipients = False
            If (trvFaxTo.Nodes(0).Nodes.Count > 0) Then
                multipleRecipients = True
            End If

            '22-Apr-13 Aniket: Resolving Memory Leak Issues
            Dim node As myTreeNode

            '' Contact Selected in treeview 
            For i As Integer = 0 To trvFaxTo.Nodes(0).Nodes.Count - 1

                node = trvFaxTo.Nodes(0).Nodes(i)
                node.FaxTo = node.Tag
                node.FaxName = node.Text

                GenerateFaxCoverPage(node.FaxTo, node.FaxName, gstrFAXType)
                node.FaxCoverPage = strFAXCoverPage
                FAXContactsCollection.Add(node)

                '24-Apr-13 Aniket: (Bug 49733) Do not dispose the following node as it causes the FAX Contact and Number to be sent blank
                'node.Dispose()
                node = Nothing 'Change made to solve memory Leak and word crash issue

            Next
        End If

        Me.Opacity = 0
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnUpdateFAXNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFAXNo1.Click, btnUpdateFAXNo.Click
        Try
            If C1Contacts.RowSel <= 0 Then
                MessageBox.Show("Please select the Contact", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If Trim(mskFaxNo.IsValidated) = False Then
                Exit Sub
            End If
            If ContactDBLayer.UpdateContactFAXNo(C1Contacts.Item(C1Contacts.RowSel, 0), mskFaxNo.Text) = False Then
                MessageBox.Show("Unable to update the Contact's FAX No.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                If ContactDBLayer.ContactType = False Then
                    C1Contacts.Item(C1Contacts.RowSel, 7) = mskFaxNo.Text
                Else
                    If strcontacttype = "Physician" Or strcontacttype = "Patient Contacts" Or strcontacttype = "Referrals" Or strcontacttype = "Primary Care Physician" Or strcontacttype = "Other Care Team" Then
                        C1Contacts.Item(C1Contacts.RowSel, 10) = mskFaxNo.Text '' column no changed because of additional columns for prefix and suffix ''Sandip Darade''20100624
                    Else
                        C1Contacts.Item(C1Contacts.RowSel, 7) = mskFaxNo.Text
                    End If
                End If
            End If
            Dim Id As Long
            Id = CType((C1Contacts.Item(C1Contacts.RowSel, 0)), Long)

            Dim myNode As myTreeNode

            For i As Integer = 0 To trvFaxTo.Nodes(0).Nodes.Count - 1

                '22-Apr-13 Aniket: Resolving Memory Leak Issues
                'myNode = New myTreeNode
                myNode = trvFaxTo.Nodes(0).Nodes(i)

                If myNode.Key = Id Then
                    myNode.Tag = mskFaxNo.Text
                    Exit Sub
                End If

                '  myNode.Dispose()
                myNode = Nothing 'Change made to solve memory Leak and word crash issue
            Next

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Sub LoadFAXCoverPage(Optional ByVal blnFirstReferralLetter As Boolean = True)
        If cmbTemplate.Items.Count <= 0 Then
            FillFaxTemplates()
        End If

        If gblnFAXCoverPage AndAlso blnFirstReferralLetter = False Then
            ''Start'GLO2011-0012282 gloEMR 6.0.2.2 - FAX REFERRAL & COMPLETE GOES TO WRONG CONTACT
            If Not IsNothing(gstrFAXCoverPage) Then
                If Not IsNothing(oCurDoc) Then
                    strFAXCoverPage = gstrFAXCoverPage

                    dsoFAXPreview.Open(strFAXCoverPage)

                    gloWord.LoadAndCloseWord.OpenDSO(dsoFAXPreview, strFAXCoverPage, oCurDoc, oWordApp)

                    oCurDoc = dsoFAXPreview.ActiveDocument
                    oWordApp = oCurDoc.Application
                    oCurDoc.ActiveWindow.SetFocus()
                End If
            End If
            ''End'GLO2011-0012282 gloEMR 6.0.2.2 - FAX REFERRAL & COMPLETE GOES TO WRONG CONTACT
        Else
            loadDSOControl()
        End If
        ' Call ReplaceFieldsByData()
        ''Start'GLO2011-0012282 gloEMR 6.0.2.2 - FAX REFERRAL & COMPLETE GOES TO WRONG CONTACT
        If Not IsNothing(oCurDoc) Then
            oCurDoc.Application.ActiveWindow.View.Type = Wd.WdViewType.wdPrintView
        End If
        ''End'GLO2011-0012282 gloEMR 6.0.2.2 - FAX REFERRAL & COMPLETE GOES TO WRONG CONTACT
    End Sub

    Private Sub ReplaceFieldsByData()
        'Procedure to find form fileds & to replace form fields data 
        'UpdateLog("Start Get Fields")
        'Dim strfieldscol As New Collection 'Collection of fields
        Dim strfieldsvalcol As String 'Collection of values
        Dim strDataCols As Array 'To split collection values with value & Flag
        Dim strData As String 'Split data will be stored in this variable 
        Dim i As Integer 'Conunter variable
        Dim j As Int16 'counter variable for array
        Dim aField As Wd.FormField 'Form field Variable
        'Search for all form fields in the document
        i = 0
        j = 0
        Dim objPrintFAX As New clsPrintFAX 'Create instance of Class
        Try


            objPrintFAX.OpenConnection()
            For Each aField In dsoFAXPreview.ActiveDocument.FormFields
                i += 1
                Select Case aField.Type
                    Case Wd.WdFieldType.wdFieldFormTextInput
                        Select Case aField.StatusText
                            Case Is <> ""
                                j += 1
                                ReDim Preserve strAarryOfFields(j - 1)
                                strAarryOfFields(j - 1) = i
                                strData = objPrintFAX.Get_FAXCoverPageData(PatientID, Replace(aField.StatusText, "+", "+space(2)+"), VisitId, ExamId, ReferralId)
                                strData = strData.Replace(vbCrLf, "")
                                strDataCols = strData.Split("|") 'Split valuees
                                Select Case strDataCols(0)
                                    Case Is <> ""
                                        Select Case Len(strDataCols(0))
                                            Case Is <= 255
                                                Select Case strDataCols(1)
                                                    Case "2"
                                                        aField.Result = "  "
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.Collapse()
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                        If System.IO.File.Exists(strDataCols(0)) Then
                                                            dsoFAXPreview.ActiveDocument.Application.Selection.InsertFile(strDataCols(0))
                                                        End If
                                                        Exit Select
                                                    Case "3"
                                                        aField.Result = "  "
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.Collapse()
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                        If System.IO.File.Exists(strDataCols(0)) Then

                                                            '' SUDHIR 20090619 '' 
                                                            Dim oWord As New gloEMRWord.clsWordDocument
                                                            oWord.CurDocument = dsoFAXPreview.ActiveDocument
                                                            ' oWord.InsertImage(strDataCols(0))
                                                            oWord = Nothing
                                                            'dsoFAXPreview.ActiveDocument.Application.Selection.InlineShapes.AddPicture(FileName:=strDataCols(0), LinkToFile:=False, SaveWithDocument:=True)
                                                            '' END SUDHIR ''

                                                        End If
                                                        Exit Select
                                                    Case Else
                                                        aField.Result = strDataCols(0)
                                                End Select
                                            Case Is > 255
                                                Select Case strDataCols(1)
                                                    Case "2"
                                                        aField.Result = "  "
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.Collapse()
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                        If System.IO.File.Exists(strDataCols(0)) Then
                                                            dsoFAXPreview.ActiveDocument.Application.Selection.InsertFile(strDataCols(0))
                                                        End If
                                                        Exit Select
                                                    Case "3"
                                                        aField.Result = "  "
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.Collapse()
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                        If System.IO.File.Exists(strDataCols(0)) Then

                                                            '' SUDHIR 20090619 '' 
                                                            Dim oWord As New gloEMRWord.clsWordDocument
                                                            oWord.CurDocument = dsoFAXPreview.ActiveDocument
                                                            ' oWord.InsertImage(strDataCols(0))
                                                            oWord = Nothing
                                                            'dsoFAXPreview.ActiveDocument.Application.Selection.InlineShapes.AddPicture(FileName:=strDataCols(0), LinkToFile:=False, SaveWithDocument:=True)
                                                            '' END SUDHIR ''

                                                        End If
                                                        Exit Select
                                                    Case Else
                                                        aField.Result = "  "
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.Collapse()
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                        dsoFAXPreview.ActiveDocument.Application.Selection.TypeText(strDataCols(0))
                                                End Select
                                        End Select
                                End Select
                        End Select
                End Select
            Next aField
            objPrintFAX.CloseConnection()
            aField = Nothing ' set nothing

            'strfieldscol = Nothing 'Set nothing 
            strfieldsvalcol = Nothing 'Set nothing

            objPrintFAX = Nothing 'Set nothing
            dsoFAXPreview.ActiveDocument.FormFields.Shaded = False 'Make shading false
            dsoFAXPreview.ActiveDocument.SpellingChecked = True  'Spell check off
            dsoFAXPreview.ActiveDocument.ShowGrammaticalErrors = False 'Grammer erros off
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
        ' UpdateLog("Stop Get Fields")
    End Sub

    Public Sub btnRefreshCoverPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnRefresh.Click

    End Sub

    Private Sub ShowGridCoverPage(ByVal ShowGrid As Boolean)
        Try
            pnlSelectFAXNo.Visible = ShowGrid
            Splitter1.Visible = ShowGrid

            ''
            If ShowGrid = True Then
                '' Show Grid
                ts_btnShowGrid.Visible = False
                ts_btnHideGrid.Visible = True

                pnlCoverPage.Dock = DockStyle.Bottom
            Else
                '' Hide Grid
                ts_btnShowGrid.Visible = True
                ts_btnHideGrid.Visible = False

                pnlCoverPage.Dock = DockStyle.Fill
                Panel2.Dock = DockStyle.Top
                dsoFAXPreview.Dock = DockStyle.Fill
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub ShowHidePreview()

        If ts_btnHidePreView.Text.StartsWith("Show") Then
            ts_btnHidePreView.Text = "Hide Preview"
            pnlCoverPage.Visible = True
        Else
            ts_btnHidePreView.Text = "Show Preview"
            pnlCoverPage.Visible = False
        End If
        pnlSelectFAXNo.Dock = DockStyle.Fill
        pnlSearch.Dock = DockStyle.Top
        C1Contacts.Dock = DockStyle.Fill
        Splitter2.Dock = DockStyle.Bottom
    End Sub

    Private Sub ClearFormFields()
        Dim i As Integer
        For i = 1 To dsoFAXPreview.ActiveDocument.FormFields.Count
            If dsoFAXPreview.ActiveDocument.FormFields.Item(i).Type = Wd.WdFieldType.wdFieldFormTextInput AndAlso dsoFAXPreview.ActiveDocument.FormFields.Item(i).StatusText = dsoFAXPreview.ActiveDocument.FormFields.Item(i).Result Then
                dsoFAXPreview.ActiveDocument.FormFields.Item(i).Result = ""
            End If
        Next
        'dsoFAXPreview.ActiveDocument.Application.Selection.Find.ClearFormatting()
        'dsoFAXPreview.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        'Try
        '    With dsoFAXPreview.ActiveDocument.Application.Selection.Find
        '        .Text = "[]"
        '        .Replacement.Text = " "
        '        .Forward = True
        '        .Wrap = Wd.WdFindWrap.wdFindContinue
        '        .Format = False
        '        .MatchCase = False
        '        .MatchWholeWord = False
        '        .MatchWildcards = False
        '        .MatchSoundsLike = False
        '        .MatchAllWordForms = False
        '    End With
        '    dsoFAXPreview.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)

        'Catch ex As Exception
        Try

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=dsoFAXPreview.ActiveDocument.Application, FindText:="[]", ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
        Catch ex2 As Exception

        End Try
        'End Try

    End Sub

    Private Sub C1Contacts_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If nRow >= 0 Then
            mnuFaxTo_Click(sender, e)
        Else
            Exit Sub
        End If
    End Sub

    Private Sub loadDSOControl()

        '22-Apr-13 Aniket: Resolving Memory Leak Issues
        Dim objTemplate As DataTable = Nothing
        Dim clsExams As New clsPatientExams

        objTemplate = clsExams.GetTemplateContents(cmbTemplate.SelectedValue)

        '22-Apr-13 Aniket: Resolving Memory Leak Issues
        clsExams.Dispose()
        clsExams = Nothing
        'Check if there are records for selected Node
        Try
            If IsNothing(objTemplate) = False AndAlso objTemplate.Rows.Count > 0 Then
                objWord = New gloEMRWord.clsWordDocument
                strFAXCoverPage = ExamNewDocumentName
                If Not objTemplate.Rows(0)(0) Is Nothing Then
                    strFAXCoverPage = objWord.GenerateFile(objTemplate.Rows(0)(0), strFAXCoverPage)
                    objWord = Nothing

                    If ((dsoFAXPreview.DocumentName = Nothing) OrElse (dsoFAXPreview.DocumentName = "")) OrElse (dsoFAXPreview.Tag <> cmbTemplate.SelectedValue) Then
                        dsoFAXPreview.Tag = cmbTemplate.SelectedValue

                        dsoFAXPreview.Open(strFAXCoverPage)

                        gloWord.LoadAndCloseWord.OpenDSO(dsoFAXPreview, strFAXCoverPage, oCurDoc, oWordApp)

                    End If


                    oCurDoc = dsoFAXPreview.ActiveDocument
                    oWordApp = oCurDoc.Application
                    oCurDoc.ActiveWindow.SetFocus()

                    objWord = New gloEMRWord.clsWordDocument
                    objCriteria = New gloEMRWord.DocCriteria
                    objCriteria.DocCategory = gloEMRWord.enumDocCategory.Referrals
                    objCriteria.PatientID = PatientID
                    objCriteria.VisitID = VisitId
                    objCriteria.PrimaryID = ReferralId
                    objWord.DocumentCriteria = objCriteria
                    objWord.CurDocument = oCurDoc

                    objWord.GetFormFieldData(gloEMRWord.enumDocType.None)
                    oCurDoc = objWord.CurDocument

                    objCriteria = Nothing
                    objWord = Nothing
                End If



            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Finally

            '22-Apr-13 Aniket: Resolving Memory Leak Issues
            If IsNothing(objTemplate) = False Then
                objTemplate.Dispose()
                objTemplate = Nothing
            End If

        End Try
    End Sub

    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        Try
            If cmbTemplate.SelectedValue > 0 Then
                loadDSOControl()
            Else
                dsoFAXPreview.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuFaxTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFaxTo.Click
        Try
            Dim Id As Long
            Id = CType((C1Contacts.Item(C1Contacts.RowSel, 0)), Long)

            Dim cnt As Integer = 0

            If ContactDBLayer.ContactType = False Then
                'if selected node e Pharmacy and it's child nodes
                If C1Contacts.RowSel >= 0 Then

                    lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2)

                    If Not IsDBNull(C1Contacts.Item(C1Contacts.RowSel, 7)) Then
                        mskFaxNo.Text = C1Contacts.Item(C1Contacts.RowSel, 7)
                    End If
                End If
            Else
                'Commented by Mayuri:20090930
                'if selected node other than e Pharmacy and it's child nodes
                If C1Contacts.RowSel >= 0 Then
                    'lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1)
                    'Code Added by Mayuri:20090930
                    'To check whether selected node is Physician or other 
                    If strcontacttype = "Physician" Or strcontacttype = "Patient Contacts" Or strcontacttype = "Referrals" Or strcontacttype = "Primary Care Physician" Or strcontacttype = "Other Care Team" Then
                        'if selected node is Physician
                        'COMMENTED BY SHUBHANGI COZ WE ARE ADDED PREFIX & SUFFIX
                        'lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3)
                        If C1Contacts.Item(C1Contacts.RowSel, 1) <> "" And C1Contacts.Item(C1Contacts.RowSel, 5) <> "" Then
                            lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4) & " " & C1Contacts.Item(C1Contacts.RowSel, 5)
                        ElseIf C1Contacts.Item(C1Contacts.RowSel, 1) <> "" And C1Contacts.Item(C1Contacts.RowSel, 5) = "" Then
                            lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4)
                        ElseIf C1Contacts.Item(C1Contacts.RowSel, 1) = "" And C1Contacts.Item(C1Contacts.RowSel, 5) <> "" Then
                            lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4) & " " & C1Contacts.Item(C1Contacts.RowSel, 5)
                        Else
                            lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4)
                        End If

                        If Not IsDBNull(C1Contacts.Item(C1Contacts.RowSel, 10)) Then
                            mskFaxNo.Text = C1Contacts.Item(C1Contacts.RowSel, 10)
                        End If
                    Else
                        lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2)
                        'if Selected node other than Physician as well as other than e Pharmacy and it's child nodes
                        If Not IsDBNull(C1Contacts.Item(C1Contacts.RowSel, 7)) Then
                            mskFaxNo.Text = C1Contacts.Item(C1Contacts.RowSel, 7)
                        End If
                    End If
                End If
            End If
            If mskFaxNo.IsValidated = False Then
                _IsMsgShown = True
                Exit Sub
            End If
            If mskFaxNo.Text = "" Then
                MessageBox.Show("Please enter the fax number for this contact.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If mskFaxNo.MaskFull = False Then
                Exit Sub
            End If

            Dim myNode As myTreeNode

            For i As Integer = 0 To trvFaxTo.Nodes(0).Nodes.Count - 1

                '22-Apr-13 Aniket: Resolving Memory Leak Issues
                'myNode = New myTreeNode

                myNode = trvFaxTo.Nodes(0).Nodes(i)
                If myNode.Key = Id Then
                    Exit Sub
                End If

                '22-Apr-13 Aniket: Resolving Memory Leak Issues
                'myNode.Dispose()
                myNode = Nothing 'Change made to solve memory Leak and word crash issue
            Next

            myNode = New myTreeNode(lblContactPerson.Text, Id, mskFaxNo.Text)

            If trvContact.SelectedNode.Text = "Physician" Then
                myNode.ImageIndex = 3
                myNode.SelectedImageIndex = 3
            ElseIf trvContact.SelectedNode.Text = "Hospital" Then
                myNode.ImageIndex = 5
                myNode.SelectedImageIndex = 5
            ElseIf trvContact.SelectedNode.Text = "Pharmacy" Then
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
            ElseIf trvContact.SelectedNode.Text = "Others" Then
                myNode.ImageIndex = 9
                myNode.SelectedImageIndex = 9
            ElseIf trvContact.SelectedNode.Text = "Insurance" Then
                myNode.ImageIndex = 0
                myNode.SelectedImageIndex = 0
            ElseIf trvContact.SelectedNode.Text = "Patient Contacts" Then
                myNode.ImageIndex = 4
                myNode.SelectedImageIndex = 4
            ElseIf trvContact.SelectedNode.Text = "Referrals" Then
                myNode.ImageIndex = 4
                myNode.SelectedImageIndex = 4
            ElseIf trvContact.SelectedNode.Text = "Primary Care Physician" Then
                myNode.ImageIndex = 4
                myNode.SelectedImageIndex = 4
            ElseIf trvContact.SelectedNode.Text = "Other Care Team" Then
                myNode.ImageIndex = 4
                myNode.SelectedImageIndex = 4
            End If
            trvFaxTo.Nodes(0).Nodes.Add(myNode)
            myNode = Nothing
            trvFaxTo.ExpandAll()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        'sarika 19th dec 07
        Try
            Dim myNode As myTreeNode
            myNode = trvFaxTo.Nodes(0)

            If Not trvFaxTo.SelectedNode Is myNode Then
                '----
                trvFaxTo.SelectedNode.Remove()
                trvFaxTo.Refresh()
                trvFaxTo.ExpandAll()
            End If

            '22-Apr-13 Aniket: Resolving Memory Leak Issues
            '  myNode.Dispose()
            myNode = Nothing    'Change made to solve memory Leak and word crash issue

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub trvFaxTo_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvFaxTo.NodeMouseClick
        Try
            trvFaxTo.SelectedNode = e.Node
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If Not IsNothing(trvFaxTo.SelectedNode) Then
                    If trvFaxTo.SelectedNode Is trvFaxTo.Nodes(0) Then
                        'Try
                        '    If (IsNothing(trvFaxTo.ContextMenu) = False) Then
                        '        trvFaxTo.ContextMenu.Dispose()
                        '        trvFaxTo.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvFaxTo.ContextMenu = Nothing
                    Else
                        'Try
                        '    If (IsNothing(trvFaxTo.ContextMenu) = False) Then
                        '        trvFaxTo.ContextMenu.Dispose()
                        '        trvFaxTo.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvFaxTo.ContextMenu = cmnuDeleteFaxTo
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try


    End Sub

    Public Function GenerateFaxCoverPage(ByVal FaxNo As String, Optional ByVal FaxTo As String = "", Optional ByVal FaxType As String = "") As String
        loadDSOControl()
        gstrFAXContactPersonFAXNo = FaxNo
        gstrFAXContactPerson = FaxTo

        If optNormalPriority.Checked Then
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        Else
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
        End If

        'Commented by Roopali.30 Nov 2009
        'Same functionality added at C1Contat double click
        'To solve issue with cover page 
        If gblnFAXCoverPage = True Then
            ' Dim sender As Object
            'Dim e As EventArgs
            'btnRefreshCoverPage_Click(sender, e)
            RefreshLiquidLinks(FaxNo, FaxTo, FaxType)
            'objWord = New gloEMRWord.clsWordDocument
            'objWord.CurDocument = oCurDoc
            'objWord.CleanupDoc()
            'oCurDoc = objWord.CurDocument
            'objWord = Nothing
            gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)

            strFAXCoverPage = ExamNewDocumentName
            dsoFAXPreview.Save(strFAXCoverPage, True, "", "")

        End If
        Return Nothing
    End Function

    Private Sub tlsp_SelectContactFAXWithFAXCoverPage_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_SelectContactFAXWithFAXCoverPage.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "ShowGrid"
                    ShowGridCoverPage(True)
                Case "HideGrid"
                    ShowGridCoverPage(False)
                Case "HidePreview"
                    ShowHidePreview()
                Case "OK"
                    'Call change at the time of Change made to solve memory Leak and word crash issue
                    OkCoverPageRevised()
                Case "Cancel"
                    CancelCoverPage()
                Case "BatchRefFax"
                    'Call change at the time of Change made to solve memory Leak and word crash issue
                    BatchRefFaxRevised()
                Case "Refresh"
                    _IsRefresh = True
                    If (_IsRefresh = True) Then
                        txtSearch.Text = ""

                        If txtSearch.Text = "" Then
                            C1Contacts.Select(0, 0, False)
                        End If
                        lblContactPerson.Text = ""
                        mskFaxNo.Text = ""
                    End If
                    Refresh()
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub optNormalPriority_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNormalPriority.CheckedChanged

        '22-Apr-13 Aniket: Resolving Memory Leak Issues
        If optNormalPriority.Checked = True Then
            optNormalPriority.Font = fntNormalPriority
            optHighPriority.Font = fntHighPriority
        Else
            optHighPriority.Font = fntHighPriority
        End If

    End Sub

    Private Sub optHighPriority_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optHighPriority.CheckedChanged
        If optHighPriority.Checked = True Then
            optHighPriority.Font = fntNormalPriority 'New Font("Tahoma", 9, FontStyle.Bold)
            optNormalPriority.Font = fntHighPriority ' New Font("Tahoma", 9, FontStyle.Regular)
        Else
            optHighPriority.Font = fntHighPriority 'New Font("Tahoma", 9, FontStyle.Regular)

        End If
    End Sub

    Private Sub dsoFAXPreview_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles dsoFAXPreview.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                Catch ex As Exception

                End Try
                UpdateVoiceLog("RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp")
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    UpdateVoiceLog(ex.ToString)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Next
                UpdateVoiceLog("Remove from word recent File list")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub dsoFAXPreview_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles dsoFAXPreview.OnDocumentOpened
        oCurDoc = dsoFAXPreview.ActiveDocument
        oWordApp = oCurDoc.Application

        Try
            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent

        Catch ex As Exception

        End Try

        Try
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        oCurDoc.FormFields.Shaded = False
        oCurDoc.ActiveWindow.SetFocus()
    End Sub

    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)

        Try
            strValue = Nothing
            m_elementId = 0
            m_DataType = ""
            m_Required = False
            If IsNothing(Sel) Then
                Return
            End If
            If (Sel.Type <> Microsoft.Office.Interop.Word.WdSelectionType.wdNoSelection) Then
                If Sel.Start = Sel.End Then
                    Dim AllowEditing As Boolean = False
                    Dim rTemp As Wd.Range
                    Dim r As Wd.Range = Nothing
                    Try
                        r = Sel.Range
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r)) Then
                        Exit Sub
                    End If
                    Try
                        r.SetRange(Sel.Start, Sel.End + 1)
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r)) Then
                        Exit Sub
                    End If
                    _Selectedcontentcontrol = r.ParentContentControl

                    If Not IsNothing(r.ParentContentControl) Then
                        strTag = Split(r.ParentContentControl.Tag, "|")
                    End If
                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then
                        ' Dim om As Object = System.Reflection.Missing.Value

                        Dim f As Wd.FormField = Nothing



                        Try
                            Dim o As Object = 1
                            f = r.FormFields.Item(o)
                            o = Nothing
                        Catch

                        End Try
                        rTemp = oCurDoc.Application.Selection.Range
                        If (IsNothing(rTemp) = False) Then


                            If Not rTemp.ContentControls Is Nothing Then
                                If Not IsNothing(rTemp.ParentContentControl) Then
                                    Dim _contentcontrol As Wd.ContentControl = rTemp.ParentContentControl
                                    strValue = _contentcontrol.Tag.Split("|")
                                    m_elementId = 0
                                    m_DataType = ""
                                    m_Required = False
                                    If (strValue.Length > 0) Then
                                        m_elementId = CType(strValue(0), Int64)
                                    End If
                                    If (strValue.Length > 1) Then
                                        m_DataType = strValue(1)
                                    End If
                                    If (strValue.Length > 2) Then
                                        m_Required = strValue(2)
                                    End If
                                    'm_elementId = CType(strValue(0), Int64)
                                    'm_DataType = strValue(1)
                                    'm_Required = strValue(2)
                                End If

                            End If
                        End If
                        If m_DataType = "Boolean" Then
                            If (IsNothing(f) = False) Then
                                If Not rTemp.Tables Is Nothing Then
                                    For Each t1 As Wd.Table In rTemp.Tables
                                        For Each c1 As Wd.Column In t1.Columns
                                            'SLR: is '0' ok?
                                            Dim cellformfields As Wd.FormFields = t1.Cell(0, c1.Index).Range.FormFields
                                            For Each cellformfield As Wd.FormField In cellformfields
                                                'SLR: 6/27/2014: Check for the type?
                                                '16-Jul-14 Aniket: Moving the following If Block as CheckBox type was not checked
                                                If cellformfield.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                                    If cellformfield.CheckBox.Value = True And cellformfield.StatusText <> f.StatusText Then
                                                        cellformfield.CheckBox.Value = False
                                                        If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                                            f.CheckBox.Value = False
                                                        End If

                                                    End If
                                                End If
                                            Next
                                        Next
                                    Next
                                End If
                            End If

                        End If
                        If m_DataType = "Group" Then
                            If Not rTemp.Tables Is Nothing Then

                                Dim HeaderText As String = ""
                                For Each t2 As Wd.Table In rTemp.Tables
                                    If (t2.Rows.Count >= 1) And (t2.Columns.Count >= 1) Then


                                        Dim headerFields As Wd.FormFields = t2.Cell(1, 1).Range.FormFields
                                        For Each Headerfield As Wd.FormField In headerFields
                                            'SLR: 6/27/2014: Check for the type?
                                            '16-Jul-14 Aniket: Moving the following If Block as CheckBox type was not checked
                                            If Headerfield.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                                If Headerfield.CheckBox.Value = True Then
                                                    AllowEditing = True
                                                    'f.Enabled = False
                                                End If
                                            End If

                                            HeaderText = Headerfield.HelpText
                                        Next
                                    End If

                                    If AllowEditing = False And f.HelpText <> "Group" Then
                                        Exit Sub
                                    End If
                                Next
                            End If
                        End If
                        If m_DataType = "Multiple Selection" Then
                            If (IsNothing(f) = False) Then
                                If Not rTemp.Tables Is Nothing Then
                                    Dim HeaderText As String = ""
                                    For Each t2 As Wd.Table In rTemp.Tables
                                        Dim nrow As Integer
                                        For Each frow As Wd.Row In t2.Rows
                                            nrow = frow.Index
                                            For Each orfield As Wd.FormField In frow.Range.FormFields
                                                If orfield.StatusText = f.StatusText Then
                                                    If orfield.Name <> f.Name Then
                                                        'SLR: 6/27/2014: Check for the type?
                                                        '16-Jul-14 Aniket: Moving the following If Block as CheckBox type was not checked
                                                        If (f.Type = Wd.WdFieldType.wdFieldFormCheckBox) And (orfield.Type = Wd.WdFieldType.wdFieldFormCheckBox) Then
                                                            If f.CheckBox.Value <> False Or orfield.CheckBox.Value <> False Then
                                                                If f.CheckBox.Value = False Then
                                                                    orfield.CheckBox.Value = False
                                                                Else
                                                                    orfield.CheckBox.Value = True
                                                                End If
                                                            End If
                                                        End If

                                                    End If
                                                End If
                                            Next
                                        Next
                                    Next
                                End If
                            End If

                        End If
                        If (IsNothing(f) = False) Then
                            If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                f.CheckBox.Value = Not f.CheckBox.Value
                                Dim oUnit As Object = Wd.WdUnits.wdCharacter
                                Dim oCnt As Object = 1
                                Dim oMove As Object = Wd.WdMovementType.wdMove
                                Sel.MoveRight(oUnit, oCnt, oMove)

                                '16-Jul-14 Aniket: Moving the following If Block as CheckBox type was not checked
                                If f.CheckBox.Value = False Then
                                    If f.HelpText = "Group" Then  ''AllowEditing = True And
                                        Dim style As Wd.Style = CreateTableStyleFalse()
                                        For Each T1 As Wd.Table In rTemp.Tables
                                            FormatTables(style, T1)
                                        Next
                                        style = Nothing
                                    End If

                                Else
                                    If f.HelpText = "Group" Then  ''AllowEditing = True And
                                        Dim style As Wd.Style = CreateTableStyleTrue()
                                        For Each T1 As Wd.Table In rTemp.Tables
                                            FormatTables(style, T1)
                                        Next
                                        style = Nothing
                                    End If
                                End If
                            End If


                        End If

                    ElseIf strTag.GetValue(1) = "Group" Then
                        rTemp = oCurDoc.Application.Selection.Range
                        If (IsNothing(rTemp) = False) Then


                            If Not rTemp.Tables Is Nothing Then

                                Dim HeaderText As String = ""
                                For Each t2 As Wd.Table In rTemp.Tables
                                    If (t2.Rows.Count >= 1) And (t2.Columns.Count >= 1) Then


                                        Dim headerFields As Wd.FormFields = t2.Cell(1, 1).Range.FormFields
                                        For Each Headerfield As Wd.FormField In headerFields
                                            '16-Jul-14 Aniket: Moving the following If Block as CheckBox type was not checked
                                            If Headerfield.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                                If Headerfield.CheckBox.Value = True Then
                                                    AllowEditing = True
                                                End If
                                            End If

                                            HeaderText = Headerfield.HelpText
                                        Next
                                    End If

                                    If AllowEditing = False Then  ''And f.HelpText <> "Group"
                                        oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCell, -1)
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If

            End If
        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Public Function CreateTableStyle() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now)
        Dim styl As Wd.Style = oCurDoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = TextureNone
        evenrowbinding.Shading.BackgroundPatternColor = ColorGray10
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        'FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = LineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleDouble
        FirstRow.Font.Size = 14
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        stylTbl.RowStripe = 1
        Return styl
    End Function

    Public Function CreateTableStyleFalse() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now)
        Dim styl As Wd.Style = oCurDoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 0 '1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        'evenrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        evenrowbinding.Font.Color = -603923969 'Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle

        Dim oddrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdOddRowBanding)
        oddrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        'oddrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        oddrowbinding.Font.Color = -603923969 ' Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        'FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray70
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        FirstRow.Font.Size = 12
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        stylTbl.RowStripe = 1
        Return styl
    End Function

    Public Function CreateTableStyleTrue() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now)
        Dim styl As Wd.Style = oCurDoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        evenrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        'FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray70
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Font.Size = 14
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        stylTbl.RowStripe = 1
        Return styl
    End Function

    Public Sub FormatTables(ByVal tstyle As Wd.Style, ByVal tb1 As Wd.Table)

        'For Each t1 As Wd.Table In oCurDoc.Tables
        Dim objtStyl As Object = CType(tstyle, Object)
        tb1.Range.Style = tstyle
        'Next
    End Sub

    ''' <summary>
    ''' Implemented by Ojeswini For Preview panel visible true false
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btnUp_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        'btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.UPHover
        'btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.DownHover
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub

    ''' <summary>
    ''' Implemented by Ojeswini For Preview panel visible true false
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUp_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        'btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
        'btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.Down
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub

    ''' <summary>
    ''' Implemented by Ojeswini For Preview panel visible true false
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDown_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        'btnDown.BackgroundImage = Global.gloEMR.My.Resources.Resources.DownHover
        'btnDown.BackgroundImage = Global.gloEMR.My.Resources.Resources.UPHover
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    ''' <summary>
    ''' Implemented by Ojeswini For Preview panel visible true false
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDown_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        'btnDown.BackgroundImage = Global.gloEMR.My.Resources.Resources.Down
        'btnDown.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    ''' <summary>
    ''' Implemented by Ojeswini For Preview panel visible true false
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        pnlFaxCoverPg.Visible = True
        btnDown.Visible = False
        btnUp.Visible = True
        pnlSelectFAXNo.Dock = DockStyle.Top
        Panel4.Dock = DockStyle.Top
        Panel4.BringToFront()
        pnlFaxCoverPg.BringToFront()
    End Sub

    ''' <summary>
    ''' Implemented by Ojeswini For Preview panel visible true false
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        pnlFaxCoverPg.Visible = False
        btnDown.Visible = True
        btnUp.Visible = False
        Panel4.Dock = DockStyle.Bottom
        pnlSelectFAXNo.Dock = DockStyle.Fill
        If pnlFaxCoverPg.Visible = False Then
            pnlSelectFAXNo.BringToFront()
        End If
    End Sub

    Private Sub btnUp1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp1.Click
        pnlSelectFAXNo.Visible = False
        btnDown1.Visible = True
        btnUp1.Visible = False
    End Sub

    Private Sub btnDown1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown1.Click
        pnlSelectFAXNo.Visible = True
        btnDown1.Visible = False
        btnUp1.Visible = True
        If pnlFaxCoverPg.Visible = False Then
            pnlSelectFAXNo.BringToFront()
        End If
    End Sub

    Private Sub btnDown1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown1.MouseHover
        'btnDown1.BackgroundImage = Global.gloEMR.My.Resources.Resources.DownHover
        btnDown1.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnDown1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown1.MouseLeave
        'btnDown1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Down
        btnDown1.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnUp1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp1.MouseHover
        'btnUp1.BackgroundImage = Global.gloEMR.My.Resources.Resources.UPHover
        btnUp1.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnUp1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp1.MouseLeave
        'btnUp1.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
        btnUp1.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub C1Contacts_DoubleClick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Contacts.DoubleClick
        Try
            '' For Fax Module change 
            If C1Contacts.Rows.Count <= 1 Then
                Return
            End If
            '' For Fax Module change 
            ''Internal issue found : when user double click on grid headers, fax number gets clear
            '' Resolved, changed the fax column number.

            Dim _faxcol As Int16
            ''Integrated from 6040 changed set based
            _faxcol = C1Contacts.Cols.IndexOf("Fax")

            If mskFaxNo.IsValidated = True Then
            Else
                Exit Sub
            End If

            If nRow > 0 Then
                mnuFaxTo_Click(sender, e)
                'Added by Roopali 30 Nov 2009
                'Cover Page Form Filed issue
                Dim newSelectedPatient As String = gloSettings.FolderSettings.AppTempFolderPath & C1Contacts.GetData(C1Contacts.Row, 0).ToString() + ".docx"
                If gblnFAXCoverPage = True Then
                    'btnRefreshCoverPage_Click(sender, e)
                    Refresh()
                    dsoFAXPreview.Save(newSelectedPatient, True, "", "")
                    oCurDoc.Saved = True
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1Contacts_MouseDown_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Contacts.MouseDown
        nRow = -1
        Dim point As New Point(e.X, e.Y)
        Try
            ''For fax Module change 
            If C1Contacts.Rows.Count < 1 Then
                Return
            End If
            ''For fax Module change 

            'point = e.Location
            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1Contacts.HitTest(point) '  DataGrid.HitTestInfo = C1Contacts.HitTest(point)
            UpdateLog(" ")
            UpdateLog("dgPatient MouseDown On Row " & hti.Row)

            If e.Button = MouseButtons.Right Then

                ' '' Mahesh 20071001
                ' '' On Empty Area on the Patient Grid Should not display Context Menu
                If hti.Row > 0 Then
                    'Fill templates in menu
                    ' FillMenus()
                    nRow = hti.Row
                    C1Contacts.Select(nRow, True)
                    'Try
                    '    If (IsNothing(Me.C1Contacts.ContextMenu) = False) Then
                    '        Me.C1Contacts.ContextMenu.Dispose()
                    '        Me.C1Contacts.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    Me.C1Contacts.ContextMenu = cmnuAddFaxTo

                Else
                    'Try
                    '    If (IsNothing(Me.C1Contacts.ContextMenu) = False) Then
                    '        Me.C1Contacts.ContextMenu.Dispose()
                    '        Me.C1Contacts.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1Contacts.ContextMenu = Nothing
                End If
            Else
                If hti.Row >= 0 Then
                    'Fill templates in menu
                    ' FillMenus()
                    nRow = hti.Row
                Else
                    'Messagebox.Show("",gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information)

                    'Try
                    '    If (IsNothing(Me.C1Contacts.ContextMenu) = False) Then
                    '        Me.C1Contacts.ContextMenu.Dispose()
                    '        Me.C1Contacts.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1Contacts.ContextMenu = Nothing
                    Exit Sub
                End If
                'Try
                '    If (IsNothing(Me.C1Contacts.ContextMenu) = False) Then
                '        Me.C1Contacts.ContextMenu.Dispose()
                '        Me.C1Contacts.ContextMenu = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                C1Contacts.ContextMenu = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub C1Contacts_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Contacts.MouseMove
        'Use to set tooltip to the content of C1 Flex grid
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub cmnuDeleteFaxTo_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuDeleteFaxTo.Popup

    End Sub

    Private Sub trvContact_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvContact.MouseClick

    End Sub

    Public Sub FaxRefContents(ByVal sFaxno As String, ByVal strRefName As String, ByVal m_ContactId As Int64)

        Try

            Dim ObjWord As gloEMRWord.clsWordDocument
            Dim objCriteria As gloEMRWord.DocCriteria
            Dim blnFAXPrinterHasToSet As Boolean = True
            Dim blnDSODefaultPrinterHasToSet As Boolean = True

            '22-Apr-13 Aniket: Resolving Memory Leak Issues
            Dim objTable As DataTable

            objTable = dtBatchRefFaxInfo

            Dim strFileName As String
            Dim i As Int16 = 0
            Dim blnFinish As Boolean

            For i = 0 To objTable.Rows.Count - 1

                If objTable.Rows.Count > 0 Then

                    Dim PatientCode As String = objTable.Rows(i)("Patientcode")
                    Dim PatientDOS As String = objTable.Rows(i)("PatientDOS")
                    Dim PatientName As String = objTable.Rows(i)("Patientname")
                    Dim ExamName As String = objTable.Rows(i)("ExamName")

                    '22-Apr-13 Aniket: Resolving Memory Leak Issues
                    'Dim objReferralsDBLayer As New ClsReferralsDBLayer()
                    Dim m_PatientId As Int64 = objTable.Rows(i)("PatientID")
                    Dim m_VisitID As Int64 = objTable.Rows(i)("VisitID")
                    Dim m_ExamId As Int64 = objTable.Rows(i)("ExamID")

                    Dim TemplateId As Int64 = objTable.Rows(i)("TemplateID")
                    blnFinish = objTable.Rows(i)("ExamFinish")
                    Dim strNotesFile As String = ""

                    strNotesFile = SelectNotesFile(m_ExamId)
                    ObjWord = New gloEMRWord.clsWordDocument
                    objCriteria = New gloEMRWord.DocCriteria
                    objCriteria.DocCategory = gloEMRWord.enumDocCategory.Template
                    objCriteria.PrimaryID = TemplateId
                    ObjWord.DocumentCriteria = objCriteria
                    UpdateVoiceLog("Retrieving Referral Letter Contents from Database & Save it to Physical File")
                    strFileName = ObjWord.RetrieveDocumentFile()
                    objCriteria = Nothing
                    ObjWord = Nothing
                    If (IsNothing(strFileName) = False) Then

                        If strFileName <> "" Then
                            ObjWord = New gloEMRWord.clsWordDocument
                            objCriteria = New gloEMRWord.DocCriteria
                            objCriteria.DocCategory = gloEMRWord.enumDocCategory.Referrals
                            objCriteria.PatientID = m_PatientId
                            objCriteria.VisitID = m_VisitID
                            objCriteria.PrimaryID = m_ContactId
                            ObjWord.DocumentCriteria = objCriteria

                            wdTemp = New AxDSOFramer.AxFramerControl

                            If gblnFAXCoverPage = True Then
                                RefreshLiquidLinks(sFaxno, strRefName, gstrFAXType)
                                gstrFAXCoverPage = strFAXCoverPage

                            End If

                            Me.Controls.Add(wdTemp)
                            ''Open Template for processing in Temp user Ctrl

                            wdTemp.Open(strFileName)

                            gloWord.LoadAndCloseWord.OpenDSO(wdTemp, strFileName, oCurDoc, oWordApp)

                            oCurDoc = wdTemp.ActiveDocument
                            ObjWord.CurDocument = oCurDoc
                            ObjWord.GetFormFieldData(gloEMRWord.enumDocType.None)
                            oCurDoc = ObjWord.CurDocument
                            ObjWord = Nothing
                            objCriteria = Nothing
                            If strNotesFile <> "" Then
                                If File.Exists(strNotesFile) Then
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Inserting Exam Notes", gloAuditTrail.ActivityOutCome.Success)
                                    'UpdateVoiceLog("Inserting Exam Notes")
                                    oCurDoc.ActiveWindow.SetFocus()
                                    oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                                    oCurDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                                    oCurDoc.Application.Selection.InsertFile(strNotesFile)
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Exam Notes Inserted", gloAuditTrail.ActivityOutCome.Success)
                                End If
                            End If
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Calling RetrieveFAXDetails method to retrieve FAX Details", gloAuditTrail.ActivityOutCome.Success)
                            Dim strResult As String
                            strResult = BatchReferralsFAXDetails(strRefName, sFaxno, strBatchRefTemplate, m_ContactId, False)

                            If strResult = "Success" Then
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "FAX Details retrieved", gloAuditTrail.ActivityOutCome.Success)
                                'UpdateVoiceLog("FAX Details retrieved")
                                If i >= 1 Then
                                    blnFAXPrinterHasToSet = False
                                End If
                                If i >= objTable.Rows.Count - 1 Then
                                    blnDSODefaultPrinterHasToSet = True
                                Else
                                    blnDSODefaultPrinterHasToSet = False
                                End If

                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Creating object of clsPrintFAX class", gloAuditTrail.ActivityOutCome.Success)
                                Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Calling FAX Document method", gloAuditTrail.ActivityOutCome.Success)

                                ''Fax Module Changes.
                                '' Create a copy of main doc
                                Dim _sFileName As String = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                                Try
                                    oCurDoc.SaveAs(_sFileName)
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                End Try


                                '22-Apr-13 Aniket: Resolving Memory Leak Issues
                                Dim objMainDoc As Wd.Document = Nothing
                                'wdTemp.Open(_sFileName)

                                gloWord.LoadAndCloseWord.OpenDSO(wdTemp, _sFileName, objMainDoc, oWordApp)

                                objMainDoc = wdTemp.ActiveDocument

                                'gstrfaxCollection = FAXContactsCollection

                                '' For Resolving the Bug ID:8081
                                '' if Fax cover page setting is on then only set selected cover page else not.
                                Dim selectTemplateName As String = ""

                                If gblnFAXCoverPage = True Then
                                    If (cmbTemplate.SelectedIndex <> -1) Then
                                        selectTemplateName = Convert.ToString(cmbTemplate.SelectedItem(1))
                                    End If
                                End If

                                If objPrintFAX.FAXDocument(objMainDoc, CStr(m_PatientId), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, selectTemplateName, clsPrintFAX.enmFAXType.ReferralLetter, Not blnFinish, blnFAXPrinterHasToSet, blnDSODefaultPrinterHasToSet, Me) = False Then
                                    ''Fax Module Changes.
                                    'TIFF File has not been created
                                    If Trim(objPrintFAX.ErrorMessage) <> "" Then
                                        '' MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                        '' log the objPrintFAX.ErrorMessage
                                        UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - " & objPrintFAX.ErrorMessage)
                                        AddPrintLogInDB()
                                    End If

                                Else
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Document Faxed", gloAuditTrail.ActivityOutCome.Success)
                                End If

                                '22-Apr-13 Aniket: Resolving Memory Leak Issues
                                'objPrintFAX.Dispose()
                                objPrintFAX = Nothing

                                objMainDoc = Nothing    'Change made to solve memory Leak and word crash issue

                            Else
                                ''log the Error reason from strResult variable
                                UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - " & strResult)
                            End If
                            wdTemp.Close()

                            '22-Apr-13 Aniket: Resolving Memory Leak Issues
                            Me.Controls.Remove(wdTemp)
                            wdTemp.Dispose()
                            oCurDoc = Nothing
                        End If
                    End If

                End If
            Next
            objTable = Nothing  'Change made to solve memory Leak and word crash issue
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        'Next
    End Sub

    Public Sub BatchRefFaxRevised()
        multipleRecipients = False
        If (IsNothing(FAXContactsCollection) = False) Then
            FAXContactsCollection.Clear()
        Else
            FAXContactsCollection = New Collection
        End If

        If optNormalPriority.Checked Then
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        Else
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
        End If

        Dim sFaxName As String = ""
        Dim sFaxNo As String = ""

        ''For validating the Fax no entered or not
        ''For Resolving BugId no : 7657 i.e  Batch Referral Letters >> If we click on FAX button it ask for fax number.
        If trvFaxTo.Nodes(0).Nodes.Count = 0 Then
            If (_IsInternetFax) Then
                If (mskFaxNo.IsValidated = False) Then
                    '22-Apr-13 Aniket: Resolving Memory Leak Issues
                    If (IsNothing(FAXContactsCollection) = False) Then
                        FAXContactsCollection.Clear()

                        FAXContactsCollection = Nothing
                    End If
                    Exit Sub
                Else
                    If (mskFaxNo.Text = "") Then
                        MessageBox.Show("Please enter the FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        mskFaxNo.Focus()
                        '22-Apr-13 Aniket: Resolving Memory Leak Issues
                        If (IsNothing(FAXContactsCollection) = False) Then
                            FAXContactsCollection.Clear()
                            FAXContactsCollection = Nothing
                        End If
                        Exit Sub
                    End If
                End If
            Else
                If (mskFaxNo.Text = "") Then
                    MessageBox.Show("Please enter the FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    mskFaxNo.Focus()
                    '22-Apr-13 Aniket: Resolving Memory Leak Issues
                    If (IsNothing(FAXContactsCollection) = False) Then
                        FAXContactsCollection.Clear()
                        FAXContactsCollection = Nothing
                    End If
                    Exit Sub
                End If
            End If
        End If
        ''For validating the Fax no entered or not


        '' If grid is selected & changed the number manually
        '' Then send the changed number
        If (C1Contacts.RowSel > 0) Then
            If Convert.ToString(C1Contacts.GetData(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("FAX"))) = mskFaxNo.Text Then
                sFaxNo = Convert.ToString(C1Contacts.GetData(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("FAX")))
                ''For "Pharmacy" it will go to else condition and for "Other" fax type it will go to if condition
                If SelectedNodekey = 1 Or SelectedNodekey = 2 Or SelectedNodekey = 3 Or SelectedNodekey = 4 Or SelectedNodekey = 5 Or SelectedNodekey = 40 Or SelectedNodekey = 41 Or SelectedNodekey = 42 Then
                    sFaxName = C1Contacts.Item(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("Name"))
                Else
                    sFaxName = C1Contacts.Item(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("FirstName")) & " " & C1Contacts.Item(C1Contacts.RowSel, C1Contacts.Cols.IndexOf("LastName"))

                End If
            Else
                sFaxNo = mskFaxNo.Text
                sFaxName = ""
                lblContactPerson.Text = ""
            End If
        Else
            sFaxNo = mskFaxNo.Text
            sFaxName = ""
            lblContactPerson.Text = ""
        End If

        Try
            If (trvFaxTo.Nodes(0).Nodes.Count = 0) Then
                If (C1Contacts.RowSel = 0) Then

                    '' Manual Fax Number Provided
                    If (mskFaxNo.Text <> "") Then
                        Dim node As New myTreeNode
                        node.FaxTo = mskFaxNo.Text
                        node.FaxName = ""
                        GenerateFaxCoverPage(node.FaxTo, node.FaxName, gstrFAXType)
                        node.FaxCoverPage = strFAXCoverPage
                        FAXContactsCollection.Add(node)
                        FaxRefContents(node.FaxTo, node.FaxName, 0)

                        '24-Apr-13 Aniket: (Bug 49733) Do not dispose the following node as it causes the FAX Contact and Number to be sent blank
                        'node.Dispose()
                        node = Nothing 'Change made to solve memory Leak and word crash issue
                    Else
                        '' Manual number is required
                        If mskFaxNo.IsValidated = True Then
                            If mskFaxNo.Text = "" Then
                                MessageBox.Show("Please enter the FAX No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                mskFaxNo.Focus()
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    End If
                Else
                    '' Contact Selected from gridview 
                    Dim node As New myTreeNode
                    node.FaxTo = sFaxNo
                    node.FaxName = sFaxName
                    GenerateFaxCoverPage(node.FaxTo, node.FaxName, gstrFAXType)
                    node.FaxCoverPage = strFAXCoverPage
                    FAXContactsCollection.Add(node)
                    FaxRefContents(sFaxNo, sFaxName, 0)

                    '24-Apr-13 Aniket: (Bug 49733) Do not dispose the following node as it causes the FAX Contact and Number to be sent blank
                    'node.Dispose()
                    node = Nothing 'Change made to solve memory Leak and word crash issue
                End If
            Else
                '' Set the variables 
                multipleRecipients = False
                If (trvFaxTo.Nodes(0).Nodes.Count > 0) Then
                    multipleRecipients = True
                End If

                '22-Apr-13 Aniket: Resolving Memory Leak Issues
                Dim node As myTreeNode

                '' Contact Selected in treeview 
                For i As Integer = 0 To trvFaxTo.Nodes(0).Nodes.Count - 1

                    If (IsNothing(FAXContactsCollection) = False) Then
                        FAXContactsCollection.Clear()
                    Else
                        FAXContactsCollection = New Collection
                    End If

                    node = trvFaxTo.Nodes(0).Nodes(i)
                    node.FaxTo = node.Tag
                    node.FaxName = node.Text

                    GenerateFaxCoverPage(node.FaxTo, node.FaxName, gstrFAXType)
                    node.FaxCoverPage = strFAXCoverPage
                    FAXContactsCollection.Add(node)
                    FaxRefContents(node.Tag, node.Text, 0)

                    '24-Apr-13 Aniket: (Bug 49733) Do not dispose the following node as it causes the FAX Contact and Number to be sent blank
                    ' node.Dispose()
                    node = Nothing 'Change made to solve memory Leak and word crash issue
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        'gstrfaxCollection = FAXContactsCollection

        Me.Opacity = 0
        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Function SelectNotesFile(ByVal m_ExamId As Int64) As String

        If (_blnInsertExamNotes = False) Then
            Dim strTempFile As String
            objWord = New gloEMRWord.clsWordDocument
            objCriteria = New gloEMRWord.DocCriteria
            objCriteria.DocCategory = gloEMRWord.enumDocCategory.Exam
            objCriteria.PrimaryID = m_ExamId
            objWord.DocumentCriteria = objCriteria
            strTempFile = objWord.RetrieveDocumentFile()
            objCriteria = Nothing
            objWord = Nothing
            If IsNothing(strTempFile) = False Then


                If strTempFile <> "" Then
                    If File.Exists(strTempFile) Then
                        'Bug #43298: 00000340 : Faxing
                        'When using a template with bookmarks and using the 'selected' notes option in this module, 
                        'it sends out the entire note, not just the bookmarks
                        If _blnInsertSelectedExamNotes Then
                            Return CreateSelectedNotes(strTempFile)
                        End If
                        Return strTempFile
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Private Function CreateSelectedNotes(ByVal strFilename As String) As String

        Dim strBM1 As String = String.Empty
        Dim strBM2 As String = String.Empty
        Dim strFile As String = String.Empty
        Dim blnFlag As Boolean = False

        wdPrint = New AxDSOFramer.AxFramerControl

        Me.Controls.Add(wdPrint)
        wdPrint.CreateNew("Word.Document")
        oTempDoc = wdPrint.ActiveDocument

        wdTemp = New AxDSOFramer.AxFramerControl

        Me.Controls.Add(wdTemp)

        'wdTemp.Open(strFilename)

        gloWord.LoadAndCloseWord.OpenDSO(wdTemp, strFilename, oCurDoc, oWordApp)

        oCurDoc = wdTemp.ActiveDocument

        '' oWordApp = oTempDoc.Application
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
        oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
        Dim setClipBoardSemaphore As Boolean = False
        Dim gotClip As Boolean = False

        For i As Int32 = 1 To oCurDoc.Range.Bookmarks.Count

            strBM2 = oCurDoc.Range.Bookmarks.Item(i).Name
            If InStr(strBM2, "BM") Then
                If Not blnFlag Then
                    blnFlag = True
                    strBM1 = strBM2
                    strBM2 = ""
                ElseIf (strBM1 <> "") Then
                    blnFlag = False
                    If (setClipBoardSemaphore = False) Then
                        Try
                            Dim strEx As String = ""
                            gotClip = Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
                        Catch ex As Exception

                        End Try
                        setClipBoardSemaphore = True
                    End If
                    Call SelectBetweenBookmarks(strBM1, strBM2)
                    oCurDoc.ActiveWindow.SetFocus()
                    strBM1 = ""
                    strBM2 = ""
                End If
            End If
        Next
        If ((setClipBoardSemaphore = True) AndAlso (gotClip = True)) Then
            Try
                Global.gloWord.gloWord.SetClipboardData()
            Catch ex As Exception

            End Try

        End If
        oTempDoc.ActiveWindow.SetFocus()
        oTempDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)

        If oTempDoc.Content.Text.Trim() = "" Then
            strFile = ""
        Else
            strFile = ExamNewDocumentName
            'wdPrint.Save(strFile, True, "", "")
            oTempDoc.SaveAs(strFile, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
        End If

        wdTemp.Close()

        '22-Apr-13 Aniket: Resolving Memory Leak Issues
        Me.Controls.Remove(wdTemp)
        wdTemp.Dispose()
        oCurDoc = Nothing

        wdPrint.Close()
        Me.Controls.Remove(wdPrint)
        wdPrint.Dispose()
        oTempDoc = Nothing

        Return strFile
    End Function

    Private Sub UpdatePrintLog(ByVal strLogMessage As String)
        Try
            Dim objFile As New System.IO.StreamWriter(strPrintLogFile, True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            '22-Apr-13 Aniket: Resolving Memory Leak Issues
            objFile.Dispose()
            objFile = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Print, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub SelectBetweenBookmarks(ByVal strBM1 As String, ByVal strBM2 As String)
        Try
            'SLR: move to calling function
            'Global.gloWord.gloWord.GetClipboardData()

            r = oCurDoc.Range(oCurDoc.Bookmarks(strBM1).End, oCurDoc.Bookmarks(strBM2).Start)
            If (IsNothing(r) = False) Then


                r.Select()

                r.Copy()
                oTempDoc.ActiveWindow.SetFocus()
                oTempDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                oTempDoc.Application.Selection.TypeText(vbNewLine)
                Try
                    oTempDoc.Application.Selection.Range.Paste()
                Catch ex As Exception

                End Try


                r = Nothing
            End If

            ' Clipboard.Clear()
            'SLR: Move to calling function
            'Global.gloWord.gloWord.SetClipboardData()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub AddPrintLogInDB()

        Try

            Dim conString As String
            conString = GetConnectionString()
            Dim Con As New SqlConnection(conString)
            Dim cmd As New SqlCommand("gsp_InsertLog", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            Con.Open()

            sqlParam = cmd.Parameters.Add("@nLogID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID(Now)

            sqlParam = cmd.Parameters.Add("@UserName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gstrLoginName

            sqlParam = cmd.Parameters.Add("@Date", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Format(Now, "MM/dd/yyyy")

            sqlParam = cmd.Parameters.Add("@FileName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Path.GetFileName(strPrintLogFile)


            objWord = New gloEMRWord.clsWordDocument
            sqlParam = cmd.Parameters.Add("@FileContent", SqlDbType.Image)
            sqlParam.Value = objWord.ConvertFiletoBinary(strPrintLogFile & "")
            objWord = Nothing

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cmd = Nothing
            sqlParam = Nothing
            Con.Close()
            Con.Dispose()
            Con = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Print, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub RefreshLiquidLinks(ByVal FaxNo As String, Optional ByVal FaxTo As String = "", Optional ByVal FaxType As String = "")
        Try
            Dim i As Integer
            Dim strText As String

            'If (_IsRefresh = True) Then
            '    txtSearch.Text = ""
            '    lblContactPerson.Text = ""
            '    If txtSearch.Text = "" Then
            '        C1Contacts.Select(0, 0, False)

            '    End If
            'End If
            If IsNothing(oCurDoc) = False Then
                For i = 1 To oCurDoc.FormFields.Count
                    strText = oCurDoc.FormFields(i).StatusText
                    Select Case Trim(strText)
                        Case "FAX.FAXTo"
                            oCurDoc.FormFields.Item(i).Result = FaxTo
                        Case "FAX.FAXNo"
                            'Shweta 20091222
                            'Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                            'oCurDoc.FormFields.Item(i).Result = txtFAXNo.Text
                            oCurDoc.FormFields.Item(i).Result = Formatfax(FaxNo)
                            'End code by Shweta 
                        Case "FAX.FAXType"
                            oCurDoc.FormFields.Item(i).Result = FaxType
                    End Select
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            _IsRefresh = False
        End Try

    End Sub

    Private Sub Refresh()
        Try
            Dim i As Integer
            Dim strText As String

            'If (_IsRefresh = True) Then
            '    txtSearch.Text = ""
            '    lblContactPerson.Text = ""
            '    If txtSearch.Text = "" Then
            '        C1Contacts.Select(0, 0, False)

            '    End If
            'End If
            If IsNothing(oCurDoc) = False Then
                For i = 1 To oCurDoc.FormFields.Count
                    strText = oCurDoc.FormFields(i).StatusText
                    Select Case Trim(strText)
                        Case "FAX.FAXTo"
                            oCurDoc.FormFields.Item(i).Result = lblContactPerson.Text
                        Case "FAX.FAXNo"
                            'Shweta 20091222
                            'Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                            'oCurDoc.FormFields.Item(i).Result = txtFAXNo.Text
                            oCurDoc.FormFields.Item(i).Result = Formatfax(mskFaxNo.Text)
                            'End code by Shweta 
                        Case "FAX.FAXType"
                            oCurDoc.FormFields.Item(i).Result = strFAXType
                    End Select
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            _IsRefresh = False
        End Try

    End Sub

    Private Sub C1Contacts_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1Contacts.RowColChange
        Try

            If ContactDBLayer.ContactType = False Then
                'SHUBHANGI 20090916 
                'Use C1 Flex Grid Instead of Data grid
                If C1Contacts.RowSel > 0 Then

                    lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2)

                    If Not IsDBNull(C1Contacts.Item(C1Contacts.RowSel, 7)) Then
                        'Shweta 20091222
                        'Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                        'txtFAXNo.Text = C1Contacts.Item(C1Contacts.RowSel, 7)
                        mskFaxNo.Text = C1Contacts.Item(C1Contacts.RowSel, 7)
                        lblContactPerson.Tag = C1Contacts.Item(C1Contacts.RowSel, 7)
                        'End code by Shweta 
                    End If
                    If gblnFAXCoverPage = True Then
                        'btnRefreshCoverPage_Click(sender, e)
                        Refresh()
                    End If
                Else
                    lblContactPerson.Text = ""
                    lblContactPerson.Tag = ""
                    'Shweta 20091222
                    'Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                    'txtFAXNo.Text = ""
                    mskFaxNo.Text = ""
                    'End code by Shweta 
                End If
            Else
                If C1Contacts.RowSel >= 0 Then



                    'lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2)
                    ''& " " & C1Contacts.Item(C1Contacts.RowSel, 3)
                    'code Added by Mayuri:20090930
                    'To check whether selected Node is Physician or other than Physician
                    If strcontacttype = "Physician" Or strcontacttype = "Patient Contacts" Or strcontacttype = "Referrals" Or strcontacttype = "Primary Care Physician" Or strcontacttype = "Other Care Team" Then
                        If C1Contacts.Item(C1Contacts.RowSel, 1) <> "" And C1Contacts.Item(C1Contacts.RowSel, 5) <> "" Then
                            lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4) & " " & C1Contacts.Item(C1Contacts.RowSel, 5)
                        ElseIf C1Contacts.Item(C1Contacts.RowSel, 1) <> "" And C1Contacts.Item(C1Contacts.RowSel, 5) = "" Then
                            lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4)
                        ElseIf C1Contacts.Item(C1Contacts.RowSel, 1) = "" And C1Contacts.Item(C1Contacts.RowSel, 5) <> "" Then
                            lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4) & " " & C1Contacts.Item(C1Contacts.RowSel, 5)
                        Else
                            lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4)
                        End If
                        'lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2) & " " & C1Contacts.Item(C1Contacts.RowSel, 3) & " " & C1Contacts.Item(C1Contacts.RowSel, 4) & " " & C1Contacts.Item(C1Contacts.RowSel, 5)
                        ''If Not IsDBNull(C1Contacts.Item(C1Contacts.RowSel, 8)) Then
                        If Not IsDBNull(C1Contacts.Item(C1Contacts.RowSel, 10)) Then '' column no changed because of additional columns for prefix and suffix ''Sandip Darade''20100624
                            'Shweta 20091222
                            'Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                            ' txtFAXNo.Text = C1Contacts.Item(C1Contacts.RowSel, 8)
                            ''mskFaxNo.Text = C1Contacts.Item(C1Contacts.RowSel, 8)
                            mskFaxNo.Text = C1Contacts.Item(C1Contacts.RowSel, 10) '' column no changed because of additional columns for prefix and suffix ''Sandip Darade''20100624
                            lblContactPerson.Tag = C1Contacts.Item(C1Contacts.RowSel, 10)
                            'End code by Shweta 
                        Else
                            'Shweta 20091222
                            'Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                            'txtFAXNo.Text = ""
                            mskFaxNo.Text = ""
                            lblContactPerson.Tag = ""
                            'End code by Shweta 
                        End If
                    Else
                        lblContactPerson.Text = C1Contacts.Item(C1Contacts.RowSel, 1) & " " & C1Contacts.Item(C1Contacts.RowSel, 2)
                        If Not IsDBNull(C1Contacts.Item(C1Contacts.RowSel, 7)) Then
                            'Shweta 20091222
                            'Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                            'txtFAXNo.Text = C1Contacts.Item(C1Contacts.RowSel, 7)
                            mskFaxNo.Text = C1Contacts.Item(C1Contacts.RowSel, 7)
                            lblContactPerson.Tag = C1Contacts.Item(C1Contacts.RowSel, 7)
                            'End code by Shweta 

                        Else
                            'Shweta 20091222
                            'Changed against the bugzilla Id:2867 as maskTextbox have added for updating fax no.
                            'txtFAXNo.Text = ""
                            mskFaxNo.Text = ""
                            lblContactPerson.Tag = ""
                            'End code by Shweta 
                        End If
                    End If
                    'lblFAXNo.Text = "Fax No" & C1Contacts.Item(C1Contacts.RowSel, 8)
                    If gblnFAXCoverPage = True Then
                        Refresh()
                        'btnRefreshCoverPage_Click(sender, e)
                    End If
                End If
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub C1Contacts_AfterSort(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles C1Contacts.AfterSort

        '' Attached the grid handlers after sorting finish
        AddHandler C1Contacts.RowColChange, AddressOf C1Contacts_RowColChange

        '' Reset the selected row index
        If (selContactID <> 0) Then
            Dim RowIndex As Int16 = C1Contacts.FindRow(Convert.ToString(selContactID), 0, 0, False, True, False)
            C1Contacts.Row = RowIndex
        Else
            C1Contacts.Row = -1
        End If
    End Sub

    Private Sub C1Contacts_BeforeSort(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles C1Contacts.BeforeSort
        '' Get the selected row index before sort
        If (C1Contacts.RowSel > 0) Then
            selContactID = C1Contacts.Item(C1Contacts.RowSel, 0)
        Else
            selContactID = 0
        End If

        '' Removed the grid handlers during sorting
        RemoveHandler C1Contacts.RowColChange, AddressOf C1Contacts_RowColChange

    End Sub

    Function Formatfax(ByVal FaxNo As String) As String
        Dim _strSQL As String = ""
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim Conn As SqlConnection
        Dim cmd As SqlCommand
        Dim strFaxNo As String = ""
        Try
            Conn = New SqlConnection(GetConnectionString())
            Conn.Open()
            oDB.Connect(GetConnectionString)
            _strSQL = "select dbo.FormatFax('" + FaxNo + "') as FaxNo"
            cmd = New SqlCommand(_strSQL, Conn)
            strFaxNo = cmd.ExecuteScalar()
            cmd.Dispose()
            cmd = Nothing
            Conn.Close()
            Conn.Dispose()
            Conn = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            oDB.Disconnect()
            'oDB.Dispose()
            oDB = Nothing
        End Try
        Return strFaxNo
    End Function

    Private Sub txtClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClearSearch.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
End Class
