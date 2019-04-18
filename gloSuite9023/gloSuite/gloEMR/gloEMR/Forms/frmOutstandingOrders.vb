Imports gloCommon
''Problem No: 00000096 : EMR Settings
''Reason: added to access the appsetting in new construstor.
Imports gloSettings
Imports gloEMR.gloEMRWord

Public Class frmOutstandingOrders
    Inherits System.Windows.Forms.Form
    Implements IPatientContext
    Dim _PatientID As Long
    ''Problem No: 00000096 : EMR Settings
    ''Reason: Used to set login provider.
    Dim _nLoginProviderId As Long = 0
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    ''Problem No: 00000096 : EMR Settings
    ''Reason: New DropDownList and label is added for Provider.
    Friend WithEvents cmbProviders As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboOrderStatus As System.Windows.Forms.ComboBox
    Private strPatientMaritalStatus As String

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ''Problem No: 00000096 : EMR Settings
        ''Reason: code added to set login provider.
        If appSettings("ProviderID") IsNot Nothing Then
            If appSettings("ProviderID") <> "" Then
                _nLoginProviderId = Convert.ToInt64(appSettings("ProviderID"))
            Else
                _nLoginProviderId = 0
            End If
        Else
            _nLoginProviderId = 0
        End If
        _PatientID = PatientID
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try

                If (IsNothing(dgData) = False) Then
                    dgData.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgData)
                    dgData.Dispose()
                    dgData = Nothing
                End If
            Catch ex As Exception

            End Try
            Try

                If (IsNothing(dgLabs) = False) Then
                    dgLabs.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgLabs)
                    dgLabs.Dispose()
                    dgLabs = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(dtTo) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtTo)
                    Catch ex As Exception

                    End Try


                    dtTo.Dispose()
                    dtTo = Nothing
                End If
            Catch
            End Try

            Try
                If (IsNothing(dtFrom) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtFrom)
                    Catch ex As Exception

                    End Try


                    dtFrom.Dispose()
                    dtFrom = Nothing
                End If
            Catch
            End Try

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
                If (IsNothing(dvNext) = False) Then
                    dvNext.Dispose()
                    dvNext = Nothing
                End If
            End If

        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    ' System.Windows.Forms.DataGrid
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftMain As System.Windows.Forms.Panel
    Friend WithEvents dgData As gloEMR.clsDataGrid
    Friend WithEvents pnlLeftTopTop As System.Windows.Forms.Panel
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents trvCriteria As System.Windows.Forms.TreeView
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbSelected As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pnlLabs As System.Windows.Forms.Panel
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents txtLabsrch As System.Windows.Forms.TextBox
    Friend WithEvents btnClearLabs As System.Windows.Forms.Button
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnClearOrders As System.Windows.Forms.Button
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents dgLabs As gloEMR.clsDataGrid
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOutstandingOrders))
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLeftTop = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.trvCriteria = New System.Windows.Forms.TreeView()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dgData = New gloEMR.clsDataGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnClearOrders = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlLeftMain = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgLabs = New gloEMR.clsDataGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.pnlLabs = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboOrderStatus = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.txtLabsrch = New System.Windows.Forms.TextBox()
        Me.btnClearLabs = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.pnlLeftTopTop = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbProviders = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.rbSelected = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlLeftMain.SuspendLayout()
        CType(Me.dgLabs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnlLabs.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlLeftTopTop.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 53)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(216, 439)
        Me.pnlLeft.TabIndex = 0
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_RightBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_TopBrd)
        Me.pnlLeftTop.Controls.Add(Me.trvCriteria)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlLeftTop.Size = New System.Drawing.Size(216, 439)
        Me.pnlLeftTop.TabIndex = 0
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 435)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(211, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 432)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(215, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 432)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(213, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'trvCriteria
        '
        Me.trvCriteria.BackColor = System.Drawing.Color.White
        Me.trvCriteria.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCriteria.ForeColor = System.Drawing.Color.Black
        Me.trvCriteria.HideSelection = False
        Me.trvCriteria.ImageIndex = 0
        Me.trvCriteria.ImageList = Me.imgTreeView
        Me.trvCriteria.Indent = 20
        Me.trvCriteria.ItemHeight = 20
        Me.trvCriteria.Location = New System.Drawing.Point(3, 3)
        Me.trvCriteria.Name = "trvCriteria"
        Me.trvCriteria.SelectedImageIndex = 0
        Me.trvCriteria.Size = New System.Drawing.Size(213, 433)
        Me.trvCriteria.TabIndex = 0
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Small Arrow.ico")
        Me.imgTreeView.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(2, "Current.ico")
        Me.imgTreeView.Images.SetKeyName(3, "Yesterdays.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Last Week.ico")
        Me.imgTreeView.Images.SetKeyName(5, "LastMonth.ico")
        Me.imgTreeView.Images.SetKeyName(6, "Olders.ico")
        Me.imgTreeView.Images.SetKeyName(7, "Outstanding orders.ico")
        Me.imgTreeView.Images.SetKeyName(8, "Add Data.ico")
        Me.imgTreeView.Images.SetKeyName(9, "Unfinished Exam.ico")
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Panel4)
        Me.pnlMain.Controls.Add(Me.Panel3)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.pnlLeftMain)
        Me.pnlMain.Controls.Add(Me.Panel5)
        Me.pnlMain.Controls.Add(Me.Panel7)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(219, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(833, 439)
        Me.pnlMain.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Controls.Add(Me.dgData)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 414)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(833, 25)
        Me.Panel4.TabIndex = 10
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(1, 21)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(828, 1)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 21)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(829, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 21)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(830, 1)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "label1"
        '
        'dgData
        '
        Me.dgData.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgData.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgData.BackgroundColor = System.Drawing.Color.White
        Me.dgData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgData.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgData.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgData.CaptionForeColor = System.Drawing.Color.White
        Me.dgData.CaptionVisible = False
        Me.dgData.DataMember = ""
        Me.dgData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgData.FullRowSelect = True
        Me.dgData.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgData.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgData.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgData.HeaderForeColor = System.Drawing.Color.White
        Me.dgData.LinkColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgData.Location = New System.Drawing.Point(0, 0)
        Me.dgData.Name = "dgData"
        Me.dgData.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgData.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgData.ReadOnly = True
        Me.dgData.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgData.SelectionForeColor = System.Drawing.Color.Black
        Me.dgData.Size = New System.Drawing.Size(830, 22)
        Me.dgData.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 387)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(833, 27)
        Me.Panel3.TabIndex = 9
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.lblSearch)
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Controls.Add(Me.Label20)
        Me.Panel6.Controls.Add(Me.Label22)
        Me.Panel6.Controls.Add(Me.Label23)
        Me.Panel6.Controls.Add(Me.Label24)
        Me.Panel6.Controls.Add(Me.Label25)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(830, 24)
        Me.Panel6.TabIndex = 4
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(568, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(61, 22)
        Me.lblSearch.TabIndex = 14
        Me.lblSearch.Text = "Search : "
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.btnClearOrders)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(629, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 22)
        Me.Panel2.TabIndex = 50
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Enabled = False
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(4, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(170, 15)
        Me.txtSearch.TabIndex = 13
        '
        'btnClearOrders
        '
        Me.btnClearOrders.BackColor = System.Drawing.Color.Transparent
        Me.btnClearOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearOrders.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearOrders.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearOrders.FlatAppearance.BorderSize = 0
        Me.btnClearOrders.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearOrders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearOrders.Image = CType(resources.GetObject("btnClearOrders.Image"), System.Drawing.Image)
        Me.btnClearOrders.Location = New System.Drawing.Point(179, 0)
        Me.btnClearOrders.Name = "btnClearOrders"
        Me.btnClearOrders.Size = New System.Drawing.Size(21, 22)
        Me.btnClearOrders.TabIndex = 49
        Me.btnClearOrders.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(1, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(128, 22)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "  Order Templates"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 23)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(828, 1)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "label2"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 23)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(829, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 23)
        Me.Label24.TabIndex = 7
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(830, 1)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "label1"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 384)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(833, 3)
        Me.Splitter2.TabIndex = 11
        Me.Splitter2.TabStop = False
        '
        'pnlLeftMain
        '
        Me.pnlLeftMain.Controls.Add(Me.Label5)
        Me.pnlLeftMain.Controls.Add(Me.Label6)
        Me.pnlLeftMain.Controls.Add(Me.Label7)
        Me.pnlLeftMain.Controls.Add(Me.Label8)
        Me.pnlLeftMain.Controls.Add(Me.dgLabs)
        Me.pnlLeftMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLeftMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlLeftMain.Name = "pnlLeftMain"
        Me.pnlLeftMain.Padding = New System.Windows.Forms.Padding(0, 3, 3, 0)
        Me.pnlLeftMain.Size = New System.Drawing.Size(833, 330)
        Me.pnlLeftMain.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 329)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(828, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 326)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(829, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 326)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(830, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'dgLabs
        '
        Me.dgLabs.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgLabs.BackgroundColor = System.Drawing.Color.White
        Me.dgLabs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgLabs.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgLabs.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgLabs.CaptionForeColor = System.Drawing.Color.White
        Me.dgLabs.CaptionVisible = False
        Me.dgLabs.DataMember = ""
        Me.dgLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgLabs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgLabs.FullRowSelect = True
        Me.dgLabs.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgLabs.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgLabs.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgLabs.HeaderForeColor = System.Drawing.Color.White
        Me.dgLabs.LinkColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgLabs.Location = New System.Drawing.Point(0, 3)
        Me.dgLabs.Name = "dgLabs"
        Me.dgLabs.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgLabs.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgLabs.ReadOnly = True
        Me.dgLabs.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgLabs.SelectionForeColor = System.Drawing.Color.Black
        Me.dgLabs.Size = New System.Drawing.Size(830, 327)
        Me.dgLabs.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.pnlLabs)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 27)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 3, 3, 0)
        Me.Panel5.Size = New System.Drawing.Size(833, 27)
        Me.Panel5.TabIndex = 8
        '
        'pnlLabs
        '
        Me.pnlLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLabs.Controls.Add(Me.Label9)
        Me.pnlLabs.Controls.Add(Me.cboOrderStatus)
        Me.pnlLabs.Controls.Add(Me.Label21)
        Me.pnlLabs.Controls.Add(Me.Panel8)
        Me.pnlLabs.Controls.Add(Me.Label11)
        Me.pnlLabs.Controls.Add(Me.Label14)
        Me.pnlLabs.Controls.Add(Me.Label15)
        Me.pnlLabs.Controls.Add(Me.Label16)
        Me.pnlLabs.Controls.Add(Me.Label17)
        Me.pnlLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabs.Location = New System.Drawing.Point(0, 3)
        Me.pnlLabs.Name = "pnlLabs"
        Me.pnlLabs.Size = New System.Drawing.Size(830, 24)
        Me.pnlLabs.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(158, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 18)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "  Order Status :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboOrderStatus
        '
        Me.cboOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrderStatus.ForeColor = System.Drawing.Color.Black
        Me.cboOrderStatus.Location = New System.Drawing.Point(268, 1)
        Me.cboOrderStatus.Name = "cboOrderStatus"
        Me.cboOrderStatus.Size = New System.Drawing.Size(199, 22)
        Me.cboOrderStatus.TabIndex = 51
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(568, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(61, 22)
        Me.Label21.TabIndex = 14
        Me.Label21.Text = "Search : "
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.White
        Me.Panel8.Controls.Add(Me.txtLabsrch)
        Me.Panel8.Controls.Add(Me.btnClearLabs)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel8.Location = New System.Drawing.Point(629, 1)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(200, 22)
        Me.Panel8.TabIndex = 50
        '
        'txtLabsrch
        '
        Me.txtLabsrch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabsrch.Enabled = False
        Me.txtLabsrch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabsrch.ForeColor = System.Drawing.Color.Black
        Me.txtLabsrch.Location = New System.Drawing.Point(3, 3)
        Me.txtLabsrch.Name = "txtLabsrch"
        Me.txtLabsrch.Size = New System.Drawing.Size(171, 15)
        Me.txtLabsrch.TabIndex = 13
        '
        'btnClearLabs
        '
        Me.btnClearLabs.BackColor = System.Drawing.Color.Transparent
        Me.btnClearLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearLabs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearLabs.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearLabs.FlatAppearance.BorderSize = 0
        Me.btnClearLabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearLabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearLabs.Image = CType(resources.GetObject("btnClearLabs.Image"), System.Drawing.Image)
        Me.btnClearLabs.Location = New System.Drawing.Point(179, 0)
        Me.btnClearLabs.Name = "btnClearLabs"
        Me.btnClearLabs.Size = New System.Drawing.Size(21, 22)
        Me.btnClearLabs.TabIndex = 50
        Me.btnClearLabs.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(1, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(160, 22)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "   Order"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(828, 1)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 23)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(829, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 23)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(830, 1)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "label1"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.pnlLeftTopTop)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel7.Size = New System.Drawing.Size(833, 27)
        Me.Panel7.TabIndex = 4
        '
        'pnlLeftTopTop
        '
        Me.pnlLeftTopTop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftTopTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftTopTop.Controls.Add(Me.Panel1)
        Me.pnlLeftTopTop.Controls.Add(Me.cmbProviders)
        Me.pnlLeftTopTop.Controls.Add(Me.Label26)
        Me.pnlLeftTopTop.Controls.Add(Me.rbAll)
        Me.pnlLeftTopTop.Controls.Add(Me.rbSelected)
        Me.pnlLeftTopTop.Controls.Add(Me.Label10)
        Me.pnlLeftTopTop.Controls.Add(Me.dtTo)
        Me.pnlLeftTopTop.Controls.Add(Me.lblTo)
        Me.pnlLeftTopTop.Controls.Add(Me.dtFrom)
        Me.pnlLeftTopTop.Controls.Add(Me.lblFrom)
        Me.pnlLeftTopTop.Controls.Add(Me.Label1)
        Me.pnlLeftTopTop.Controls.Add(Me.Label2)
        Me.pnlLeftTopTop.Controls.Add(Me.Label3)
        Me.pnlLeftTopTop.Controls.Add(Me.Label4)
        Me.pnlLeftTopTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTopTop.Location = New System.Drawing.Point(0, 3)
        Me.pnlLeftTopTop.Name = "pnlLeftTopTop"
        Me.pnlLeftTopTop.Size = New System.Drawing.Size(833, 24)
        Me.pnlLeftTopTop.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(471, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(129, 22)
        Me.Panel1.TabIndex = 6
        '
        'cmbProviders
        '
        Me.cmbProviders.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProviders.ForeColor = System.Drawing.Color.Black
        Me.cmbProviders.Location = New System.Drawing.Point(327, 1)
        Me.cmbProviders.Name = "cmbProviders"
        Me.cmbProviders.Size = New System.Drawing.Size(138, 22)
        Me.cmbProviders.TabIndex = 14
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(249, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(78, 22)
        Me.Label26.TabIndex = 15
        Me.Label26.Text = "  Provider :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.BackColor = System.Drawing.Color.Transparent
        Me.rbAll.Checked = True
        Me.rbAll.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAll.Location = New System.Drawing.Point(600, 1)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(95, 22)
        Me.rbAll.TabIndex = 12
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All Patients"
        Me.rbAll.UseVisualStyleBackColor = False
        '
        'rbSelected
        '
        Me.rbSelected.AutoSize = True
        Me.rbSelected.BackColor = System.Drawing.Color.Transparent
        Me.rbSelected.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbSelected.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSelected.Location = New System.Drawing.Point(695, 1)
        Me.rbSelected.Name = "rbSelected"
        Me.rbSelected.Size = New System.Drawing.Size(132, 22)
        Me.rbSelected.TabIndex = 11
        Me.rbSelected.Text = "Selected Patients"
        Me.rbSelected.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Enabled = False
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(827, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(5, 22)
        Me.Label10.TabIndex = 13
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.CustomFormat = "MM/dd/yyyy"
        Me.dtTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtTo.Enabled = False
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(161, 1)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(88, 22)
        Me.dtTo.TabIndex = 1
        '
        'lblTo
        '
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTo.Enabled = False
        Me.lblTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblTo.Location = New System.Drawing.Point(129, 1)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(32, 22)
        Me.lblTo.TabIndex = 2
        Me.lblTo.Text = "To "
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtFrom.Enabled = False
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(42, 1)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(87, 22)
        Me.dtFrom.TabIndex = 0
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFrom.Enabled = False
        Me.lblFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFrom.Location = New System.Drawing.Point(1, 1)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(41, 22)
        Me.lblFrom.TabIndex = 3
        Me.lblFrom.Text = "From"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(831, 1)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(832, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(833, 1)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(216, 53)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 439)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1052, 53)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1052, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnRefresh.Tag = "Open"
        Me.ts_btnRefresh.Text = "&Open"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmOutstandingOrders
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1052, 492)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOutstandingOrders"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Outstanding Orders"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlLeftMain.ResumeLayout(False)
        CType(Me.dgLabs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.pnlLabs.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.pnlLeftTopTop.ResumeLayout(False)
        Me.pnlLeftTopTop.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Enum OrderFor
        Radiology = 1
        Labs = 2
    End Enum
    ''Added Rahul For new Implementation of All Patients & Selected Patients on 20101021.
    Dim _selectedpatientid As Integer
    Dim PatientNm As Int16 = 7
    Dim PatientCat As Int16 = 3
    Dim PatientTest As Int16 = 4
    Dim PatientInstruction As String = ""
    Dim PatientLNm As String = "" '' Added for Labs Searching
    Dim PatientLTest As String = ""
    Dim dvdata As DataView
    Dim dvLabs As DataView
    Dim dtTemp As DataTable
    Dim dvNext As DataView
    Dim _VisiblePatientCount As Integer
    Dim blnLabs As Boolean = False

    'Private selectedOrder As Int64
    Dim trvNode As TreeNode

    '18-Mar-14 Aniket: Resolving Memory Leaks
    Private dtProviderList As DataTable

    Private Sub frmOutstandingOrders_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If (rbSelected.Checked = True) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        If IsNothing(trvCriteria.SelectedNode) Then
            trvCriteria.SelectedNode = trvNode
        End If
        ''Problem No: 00000096 : EMR Settings
        ''Reason: Selected providerId is pass to Fill_RadiologyOrder(),Fill_LabOrder(),Fill_LabOrderChange().
        'If selectedOrder = OrderFor.Radiology Then
        Call Fill_RadiologyOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
        'ElseIf selectedOrder = OrderFor.Labs Then
        'Call Fill_LabOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
        'End If

        Fill_LabOrderChange(Convert.ToInt64(cmbProviders.SelectedValue.ToString()), cboOrderStatus.SelectedValue)
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub frmOutstandingOrders_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed



        '18-Mar-14 Aniket: Resolving Memory Leaks
        If IsNothing(dtProviderList) = False Then
            dtProviderList.Dispose()
            dtProviderList = Nothing
        End If

        RemoveHandler cboOrderStatus.SelectedIndexChanged, AddressOf cboOrderStatus_SelectionChangeCommitted
        RemoveHandler dtFrom.TextChanged, AddressOf dtFrom_TextChanged
        RemoveHandler dtTo.TextChanged, AddressOf dtTo_TextChanged

    End Sub

    Private Sub frmOutstandingOrders_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''Problem No: 00000096 : EMR Settings
            ''Reason: Call to FillProvider() to load provider list in dropdownlist.
            FillProvider()
            FillOrderStatus()

            Me.Cursor = Cursors.WaitCursor
            dtFrom.Visible = True
            dtTo.Visible = True

            txtSearch.Text = ""
            txtLabsrch.Text = ""
            Call Get_PatientDetails()
            rbAll_Click(sender, e)
            rbSelected_Click(sender, e)

            Call Fill_Criterias()

            'Call Fill_RadiologyOrder()
            'selectedOrder = OrderFor.Radiology

            ''
            Me.Cursor = Cursors.Default
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Other, "OutStanding Orders Report Opened", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing
            'Fill_LabOrderChange()
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, "OutStanding Orders Report Opened", gloAuditTrail.ActivityOutCome.Success)

            ''For Tab Index Setting
            Dim scheme As Cls_TabIndexSettings.TabScheme = Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New Cls_TabIndexSettings(Me)
            ' This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme)
            ''End

            AddHandler cboOrderStatus.SelectedIndexChanged, AddressOf cboOrderStatus_SelectionChangeCommitted
            AddHandler dtFrom.TextChanged, AddressOf dtFrom_TextChanged
            AddHandler dtTo.TextChanged, AddressOf dtTo_TextChanged

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''Problem No: 00000096 : EMR Settings
    ''Reason: New fuction is define to get list of all provider and bind to cmbprovider.
    Private Sub FillProvider()


        Dim objRadioOrders As New clsLM_Orders

        Try

            '18-Mar-14 Aniket: Resolving Memory Leaks
            If IsNothing(dtProviderList) = False Then
                dtProviderList.Dispose()
                dtProviderList = Nothing
            End If

            dtProviderList = objRadioOrders.GetAllProvider()

            If Not IsNothing(dtProviderList) Then
                Dim objrow As DataRow
                objrow = dtProviderList.NewRow
                objrow.Item(0) = 0
                objrow.Item(1) = "All"
                dtProviderList.Rows.Add(objrow)

                cmbProviders.DataSource = dtProviderList
                cmbProviders.DisplayMember = dtProviderList.Columns(1).ColumnName 'Provider Name
                cmbProviders.ValueMember = dtProviderList.Columns(0).ColumnName 'Provider ID

                If (_nLoginProviderId > 0) Then

                    cmbProviders.SelectedValue = _nLoginProviderId
                Else
                    cmbProviders.Text = "All"
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillOrderStatus()

        Dim dtOrderStatus As DataTable
        Dim objRadioOrders As New clsLM_Orders

        Try
            dtOrderStatus = objRadioOrders.GetOrderStatus()

            If Not IsNothing(dtOrderStatus) Then

                cboOrderStatus.DataSource = dtOrderStatus
                cboOrderStatus.DisplayMember = dtOrderStatus.Columns("OrderStatus").ColumnName
                cboOrderStatus.ValueMember = dtOrderStatus.Columns("OrderStatusNumber").ColumnName

                cmbProviders.SelectedValue = 0

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''Problem No: 00000096 : EMR Settings
    ''Reason: check whether _providerID and login providerID are different then pass selected providerID to criteria.
    Public Sub Fill_RadiologyOrder(Optional ByVal _ProviderID As Int64 = 0)
        If IsNothing(trvCriteria.SelectedNode) = True Then Exit Sub
        If trvCriteria.SelectedNode Is trvCriteria.Nodes(0) Then Exit Sub

        If _ProviderID <> _nLoginProviderId Then
            _ProviderID = cmbProviders.SelectedValue
        End If

        Dim dtRadioOrders As New DataTable
        Dim objRadioOrders As New clsLM_Orders
        '''''''''''''''''''Code modifications are done by Anil on 20071113
        Dim _DateRange() As DateTime

        Select Case Trim(trvCriteria.SelectedNode.Text)
            Case "Today"
                _DateRange = GetDateRange(DateCategory.Today)
                If _DateRange.Length > 0 Then
                    dtRadioOrders = objRadioOrders.FillOrders(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID)
                    lblFrom.Text = "Date"
                    lblTo.Visible = False
                    dtFrom.Value = _DateRange(0)
                    dtTo.Visible = False
                    dtTo.Visible = False
                End If
                'dtExams = objExams.Fill_Exams(System.DateTime.Now.Date, System.DateTime.Now.Date)
            Case "Yesterday"
                _DateRange = GetDateRange(DateCategory.Yesterday)
                If _DateRange.Length > 0 Then
                    dtRadioOrders = objRadioOrders.FillOrders(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID)
                    lblFrom.Text = "Date"
                    lblTo.Visible = False
                    dtFrom.Value = _DateRange(0)
                    dtTo.Visible = False

                End If
                'dtExams = objExams.Fill_Exams(System.DateTime.Now.Date.AddDays(-1), System.DateTime.Now.Date)
            Case "Last Week"
                _DateRange = GetDateRange(DateCategory.LastWeek)
                If _DateRange.Length > 0 Then
                    dtRadioOrders = objRadioOrders.FillOrders(_DateRange(0).AddDays(1), _DateRange(1).AddDays(1), _ProviderID)
                    lblFrom.Text = "From"
                    lblTo.Text = "To"
                    lblTo.Visible = True
                    dtTo.Visible = True
                    dtFrom.Value = _DateRange(0).AddDays(1)
                    dtTo.Value = _DateRange(1)
                End If
                'dtExams = objExams.Fill_Exams(System.DateTime.Now.Date.AddDays(-7), System.DateTime.Now.Date.AddDays(1))
            Case "Last Month"
                _DateRange = GetDateRange(DateCategory.LastMonth)
                If _DateRange.Length > 0 Then
                    dtRadioOrders = objRadioOrders.FillOrders(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID)
                    lblFrom.Text = "From"
                    lblTo.Text = "To"
                    lblTo.Visible = True
                    dtTo.Visible = True
                    dtFrom.Value = _DateRange(0)
                    dtTo.Value = _DateRange(1)
                End If
                'dtExams = objExams.Fill_Exams(System.DateTime.Now.Date.AddMonths(-1), System.DateTime.Now.Date.AddDays(1))
            Case "Customize"
                dtRadioOrders = objRadioOrders.FillOrders(dtFrom.Value, dtTo.Value.AddDays(1), _ProviderID)
                'pnlLeftTopTop.Enabled = True
                lblFrom.Text = "From"
                lblTo.Text = "To"
                lblTo.Visible = True
                dtTo.Visible = True

        End Select
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        objRadioOrders.Dispose()
        objRadioOrders = Nothing
        dgData.TableStyles.Clear()
        'dgData.Enabled = False
        dgData.DataSource = dtRadioOrders
        'dgData.Enabled = True

        Dim grdTableStyleRadio As New clsDataGridTableStyle(dtRadioOrders.TableName)

        Dim grdColStyleID As New DataGridTextBoxColumn
        ''''Order ID
        With grdColStyleID
            .HeaderText = "ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("lm_Order_ID").ColumnName
            .NullText = ""
            .Width = 0
        End With

        ''''Patient Name
        Dim grdColStylePatient As New DataGridTextBoxColumn
        With grdColStylePatient
            .HeaderText = "Patient Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("PatientName").ColumnName
            .NullText = ""
            .Width = 205 '0.28 * dgData.Width
        End With

        ''''Order Date
        Dim grdColStyleDate As New DataGridTextBoxColumn
        With grdColStyleDate
            .HeaderText = "Order Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("lm_OrderDate").ColumnName
            .NullText = ""
            .Width = 154 '0.2 * dgData.Width - 5
        End With

        ''''Category Name
        Dim grdColStyleCategory As New DataGridTextBoxColumn
        With grdColStyleCategory
            .HeaderText = "Category"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("lm_category_Description").ColumnName
            .NullText = ""
            .Width = 256 ' 0.25 * dgData.Width
        End With

        Dim grdColStyleTest As New DataGridTextBoxColumn
        With grdColStyleTest
            .HeaderText = "Test"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("lm_test_Name").ColumnName
            .NullText = ""
            .Width = 256 '0.25 * dgData.Width
        End With

        Dim grdColStyleVisitID As New DataGridTextBoxColumn
        With grdColStyleVisitID
            .HeaderText = "Visit ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("lm_Visit_ID").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "Patient ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("lm_Patient_ID").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Patient Code"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("sPatientCode").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStyleDOB As New DataGridTextBoxColumn
        With grdColStyleDOB
            .HeaderText = "DOB"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("dtDOB").ColumnName
            .NullText = ""
            .Width = 0
        End With

        ''Added Rahul for Order Status on 20101021.
        Dim grdColStyleStatus As New DataGridTextBoxColumn
        With grdColStyleStatus

            .HeaderText = "Status"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtRadioOrders.Columns("lm_sStatus").ColumnName
            .NullText = ""
            .Width = 195 '0.19 * dgData.Width
        End With
        dvdata = New DataView(dgData.DataSource)
        GetSelectedPatientRecord()
        ''End

        grdTableStyleRadio.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleID, grdColStylePatient, grdColStyleDate, grdColStyleCategory, grdColStyleTest, grdColStyleVisitID, grdColStylePatientID, grdColStylePatientCode, grdColStyleDOB, grdColStyleStatus})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyleRadio)
    End Sub
    ''Problem No: 00000096 : EMR Settings
    ''Reason: check whether _providerID and login providerID are different then pass selected providerID to criteria.
    'Public Sub Fill_LabOrder(Optional ByVal _ProviderID As Int64 = 0)
    '    If IsNothing(trvCriteria.SelectedNode) = True Then Exit Sub
    '    If trvCriteria.SelectedNode Is trvCriteria.Nodes(0) Then Exit Sub

    '    If _ProviderID <> _nLoginProviderId Then
    '        _ProviderID = cmbProviders.SelectedValue
    '    End If

    '    Dim dtLabsOrders As New DataTable
    '    'Dim objRadioOrders As New clsLM_Orders
    '    Dim oLabOrderRequest As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder
    '    '''''''''''''''''''Code modifications are done by Anil on 20071113
    '    Dim _DateRange() As DateTime

    '    Select Case Trim(trvCriteria.SelectedNode.Text)
    '        Case "Today"
    '            _DateRange = GetDateRange(DateCategory.Today)
    '            If _DateRange.Length > 0 Then
    '                '' 20080922
    '                'dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), gnDoctorID)
    '                dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID)
    '                lblFrom.Text = "Date"
    '                lblTo.Visible = False
    '                dtFrom.Value = _DateRange(0)
    '                dtTo.Visible = False
    '                dtTo.Visible = False
    '            End If
    '            'dtExams = objExams.Fill_Exams(System.DateTime.Now.Date, System.DateTime.Now.Date)
    '        Case "Yesterday"
    '            _DateRange = GetDateRange(DateCategory.Yesterday)
    '            If _DateRange.Length > 0 Then
    '                ''20080922
    '                'dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), gnDoctorID)
    '                dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID)
    '                lblFrom.Text = "Date"
    '                lblTo.Visible = False
    '                dtFrom.Value = _DateRange(0)
    '                dtTo.Visible = False

    '            End If
    '            'dtExams = objExams.Fill_Exams(System.DateTime.Now.Date.AddDays(-1), System.DateTime.Now.Date)
    '        Case "Last Week"
    '            _DateRange = GetDateRange(DateCategory.LastWeek)
    '            If _DateRange.Length > 0 Then
    '                ''20080922
    '                'dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0).AddDays(1), _DateRange(1).AddDays(1), gnDoctorID)
    '                dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0).AddDays(1), _DateRange(1).AddDays(1), _ProviderID)
    '                lblFrom.Text = "From"
    '                lblTo.Text = "To"
    '                lblTo.Visible = True
    '                dtTo.Visible = True
    '                dtFrom.Value = _DateRange(0).AddDays(1)
    '                dtTo.Value = _DateRange(1)
    '            End If
    '            'dtExams = objExams.Fill_Exams(System.DateTime.Now.Date.AddDays(-7), System.DateTime.Now.Date.AddDays(1))
    '        Case "Last Month"
    '            _DateRange = GetDateRange(DateCategory.LastMonth)
    '            If _DateRange.Length > 0 Then
    '                ''20080922
    '                'dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), gnDoctorID)
    '                dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID)
    '                lblFrom.Text = "From"
    '                lblTo.Text = "To"
    '                lblTo.Visible = True
    '                dtTo.Visible = True
    '                dtFrom.Value = _DateRange(0)
    '                dtTo.Value = _DateRange(1)
    '            End If
    '            'dtExams = objExams.Fill_Exams(System.DateTime.Now.Date.AddMonths(-1), System.DateTime.Now.Date.AddDays(1))
    '        Case "Customize"
    '            Dim _dtFrom As Date = CType(dtFrom.Text, Date) ' Format(dtFrom.Text, "MM/dd/YYYY")
    '            Dim _dtTo As Date = CType(dtTo.Text, Date) 'Format(dtTo.Text, "MM/dd/YYYY")
    '            ''20080922
    '            'dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_dtFrom, _dtTo.AddDays(1), gnDoctorID) '(dtFrom.Value, dtTo.Value.AddDays(1), gnDoctorID)
    '            dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_dtFrom, _dtTo.AddDays(1), _ProviderID)
    '            'pnlLeftTopTop.Enabled = True
    '            lblFrom.Text = "From"
    '            lblTo.Text = "To"
    '            lblTo.Visible = True
    '            dtTo.Visible = True

    '    End Select
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    oLabOrderRequest.Dispose()
    '    oLabOrderRequest = Nothing
    '    dgData.Enabled = False
    '    dgData.DataSource = dtLabsOrders
    '    dgData.Enabled = True
    '    dgData.TableStyles.Clear()
    '    Dim grdTableStyleLabs As New clsDataGridTableStyle(dtLabsOrders.TableName)

    '    Dim grdColStyleOrderIDPrefix As New DataGridTextBoxColumn
    '    ''''Order ID PreFix
    '    With grdColStyleOrderIDPrefix
    '        .HeaderText = "OrederID PreFix"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labom_OrderNoPrefix").ColumnName
    '        .NullText = ""
    '        .Width = 0
    '    End With



    '    Dim grdColStyleID As New DataGridTextBoxColumn
    '    ''''Order ID
    '    With grdColStyleID
    '        .HeaderText = "ID"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labom_OrderID").ColumnName
    '        .NullText = ""
    '        .Width = 0
    '    End With

    '    ''''Patient Name
    '    Dim grdColStylePatient As New DataGridTextBoxColumn
    '    With grdColStylePatient
    '        .HeaderText = "Patient Name"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("PatientName").ColumnName
    '        .NullText = ""
    '        .Width = 0.35 * dgData.Width
    '    End With

    '    ''''Order Date
    '    Dim grdColStyleDate As New DataGridTextBoxColumn
    '    With grdColStyleDate
    '        .HeaderText = "Order Date"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labom_OrderDate").ColumnName
    '        .NullText = ""
    '        .Width = 0.25 * dgData.Width - 5
    '    End With

    '    ''''Category Name
    '    Dim grdColStyleCategory As New DataGridTextBoxColumn
    '    With grdColStyleCategory
    '        .HeaderText = "Test Name"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labtm_Name").ColumnName
    '        .NullText = ""
    '        .Width = 0.37 * dgData.Width
    '    End With

    '    Dim grdColStyleTestID As New DataGridTextBoxColumn
    '    With grdColStyleTestID
    '        .HeaderText = "Test ID"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labtm_ID").ColumnName
    '        .NullText = ""
    '        .Width = 0
    '    End With

    '    Dim grdColStyleVisitID As New DataGridTextBoxColumn
    '    With grdColStyleVisitID
    '        .HeaderText = "Visit ID"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labom_VisitID").ColumnName
    '        .NullText = ""
    '        .Width = 0
    '    End With

    '    Dim grdColStylePatientID As New DataGridTextBoxColumn
    '    With grdColStylePatientID
    '        .HeaderText = "Patient ID"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labom_PatientID").ColumnName
    '        .NullText = ""
    '        .Width = 0
    '    End With

    '    Dim grdColStylePatientCode As New DataGridTextBoxColumn
    '    With grdColStylePatientCode
    '        .HeaderText = "Patient Code"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("sPatientCode").ColumnName
    '        .NullText = ""
    '        .Width = 0
    '    End With

    '    Dim grdColStyleProviderID As New DataGridTextBoxColumn
    '    With grdColStyleProviderID
    '        .HeaderText = "Provider ID"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labom_ProviderID").ColumnName
    '        .NullText = ""
    '        .Width = 0
    '    End With

    '    Dim grdColStyleOrderNoID As New DataGridTextBoxColumn
    '    ''''Order Number ID 
    '    With grdColStyleOrderNoID
    '        .HeaderText = "Oreder Number ID PreFix"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labom_OrderNoID").ColumnName
    '        .NullText = ""
    '        .Width = 0
    '    End With

    '    Dim grdColStyleInstruction As New DataGridTextBoxColumn
    '    '''' Instructions 
    '    With grdColStyleInstruction
    '        .HeaderText = "Instruction"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("labotd_Instruction").ColumnName
    '        PatientInstruction = dtLabsOrders.Columns("labotd_Instruction").ColumnName
    '        .NullText = ""
    '        .Width = 0.25 * dgData.Width
    '    End With

    '    Dim grdColStyleOrderStatus As New DataGridTextBoxColumn

    '    With grdColStyleOrderStatus
    '        .HeaderText = "Order Status"
    '        .Alignment = HorizontalAlignment.Left
    '        .MappingName = dtLabsOrders.Columns("OrderStatus").ColumnName
    '        .NullText = ""
    '        .Width = 0.25 * dgData.Width
    '    End With


    '    dvdata = New DataView(dgData.DataSource)

    '    GetSelectedPatientRecord()

    '    grdTableStyleLabs.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleOrderIDPrefix, grdColStyleID, grdColStylePatient, grdColStyleDate, grdColStyleCategory, grdColStyleTestID, grdColStyleVisitID, grdColStylePatientID, grdColStylePatientCode, grdColStyleProviderID, grdColStyleOrderNoID, grdColStyleOrderStatus, grdColStyleInstruction})
    '    dgData.TableStyles.Clear()
    '    dgData.TableStyles.Add(grdTableStyleLabs)
    'End Sub

    Private Sub btnShowExams_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ' Call Fill_Exams()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Close, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OpenRadiology()
        If dgData.CurrentRowIndex >= 0 Then
            'dgData.CurrentRowIndex = 0
            'gstrPatientCode = dgData.Item(dgData.CurrentRowIndex, 7) '' Commented not to use globle veriables for patient
            _PatientID = dgData.Item(dgData.CurrentRowIndex, 6) '' Replace gnPatientID
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            Else

                Dim nVisitId As Long = 0
                Dim VisitDate As String = ""

                nVisitId = dgData.Item(dgData.CurrentRowIndex, 5)
                VisitDate = dgData.Item(dgData.CurrentRowIndex, 2)


                '' '' <><><> Record Level Locking <><><><> 
                '' '' Mahesh - 20070724 
                'Dim blnRecordLock As Boolean = False
                'If gblnRecordLocking = True Then
                '    Dim mydt As New mytable
                '    mydt = Scan_n_Lock_Transaction(TrnType.Radiology, gnPatientID, nVisitId, VisitDate)
                '    If mydt.Description <> gstrClientMachineName Then
                '        If MessageBox.Show("This Radiology Order is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            blnRecordLock = True
                '        Else
                '            Exit Sub
                '        End If
                '    End If
                'End If
                ' '' <><><> Record Level Locking <><><><> 

                'Create instance of Class to get message User 
                '' By Mahesh 20070120 - SingleTon Pattern
                Dim frm As frm_LM_Orders
                frm = frm_LM_Orders.GetInstance(nVisitId, VisitDate, _PatientID, 1, False)

                If IsNothing(frm) = True Then
                    Exit Sub
                End If

                With frm
                    'Me.pnlLeft.Visible = False
                    ''Me.pnlRights.Visible = False
                    'Me.Splitter1.Visible = False
                    '''' Hide Tool Bar Mahesh 20070613
                    'Me.pnlMenu.Visible = False

                    '.MdiParent = Me
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    '''' Hide Tool Bar Mahesh 20070613
                    CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False
                    .MdiParent = CType(Me.MdiParent, MainMenu)
                    '.dtOrderTime.Enabled = False
                    .WindowState = FormWindowState.Maximized
                    .Show()
                End With
                txtLabsrch.Text = ""
                txtSearch.Text = ""
                Panel6.BackgroundImage = gloEMR.My.Resources.Resources.Img_Button
            End If
        End If



    End Sub

    Private Sub OpenLabs()
        If dgLabs.CurrentRowIndex >= 0 Then

            'gstrPatientCode = dgData.Item(dgData.CurrentRowIndex, 8)

            '''''''''''''''''''''''   _PatientID = dgData.Item(dgData.CurrentRowIndex, 6) '' 6 Replace gnPatientID

            _PatientID = dgLabs.Item(dgLabs.CurrentRowIndex, 7)

            If MainMenu.IsAccess(False, _PatientID, , True) = False Then
                Exit Sub
            End If

            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            Else

                Dim nVisitId As Long = 0
                Dim VisitDate As String = ""
                Dim OrderId As Long = 0
                nVisitId = dgLabs.Item(dgLabs.CurrentRowIndex, 6) '5 ''''''''dgData.Item(dgData.CurrentRowIndex, 5) '5
                VisitDate = dgLabs.Item(dgLabs.CurrentRowIndex, 3) '''''''''dgData.Item(dgData.CurrentRowIndex, 2)
                OrderId = dgLabs.Item(dgLabs.CurrentRowIndex, 1) '''''''''''''''''''''dgData.Item(dgData.CurrentRowIndex, 0) '0

                '' '' <><><> Record Level Locking <><><><> 
                '' '' Mahesh - 20070724 
                'Dim blnRecordLock As Boolean = False
                'If gblnRecordLocking = True Then
                '    Dim mydt As New mytable
                '    mydt = Scan_n_Lock_Transaction(TrnType.Radiology, gnPatientID, nVisitId, VisitDate)
                '    If mydt.Description <> gstrClientMachineName Then
                '        If MessageBox.Show("This Radiology Order is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            blnRecordLock = True
                '        Else
                '            Exit Sub
                '        End If
                '    End If
                'End If
                ' '' <><><> Record Level Locking <><><><> 
                'Madan 20100406-- added for viewing.. gloLab..
                ' If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel <> "" Then

                Dim frm_viewgloLab As New gloEmdeonInterface.Forms.frmViewgloLab(OrderId, _PatientID)  '' Replace gnPatientID


                'Code added for Already lab order Is open
                If (frm_viewgloLab.CheckInstance() = True) Then
                    MessageBox.Show("A Lab Order screen is already open. Please close that to continue…", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    frm_viewgloLab.Close()
                    If (IsNothing(frm_viewgloLab) = False) Then
                        frm_viewgloLab.Dispose()
                        frm_viewgloLab = Nothing
                    End If
                  
                    Exit Sub
                End If

                With frm_viewgloLab
                    'Developer:Sanjog(Dhamke)
                    'Date: 2 Feb 2012
                    'PRD Name: Lab Usability Changes
                    'Reason: If order is open for modification that time its need to direct on orders tab not on result set tab
                    AddHandler frm_viewgloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
                    AddHandler frm_viewgloLab.EntOpenMessage, AddressOf CType(Me.ParentForm, MainMenu).OpenMessage
                    AddHandler frm_viewgloLab.EvntOpenPatientLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenPatientLetter
                    AddHandler frm_viewgloLab.EvntOpenReferralLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenReferralLetters
                    AddHandler frm_viewgloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart
                    AddHandler frm_viewgloLab.EvntOpenPlanOfTreatment, AddressOf CType(Me.ParentForm, MainMenu).OpenPlanofTreatment
                    'AddHandler ofrmViewgloLab.EvntOpenClinicalChart, AddressOf OpenClinicalChart
                    AddHandler frm_viewgloLab.EvntGenerateCCDHandler, AddressOf CType(Me.ParentForm, MainMenu).openCCD
                    AddHandler frm_viewgloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
                    AddHandler frm_viewgloLab.FormClosed, AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed 'CType(Me.ParentForm, MainMenu).ShowTasks
                    AddHandler frm_viewgloLab.Activated, AddressOf CType(Me.ParentForm, MainMenu).frmViewgloLab_Activated
                    ''Bug No 57350::Patient InfoButton - Applicatipn not able to open Patient spacific & Provider Spacific Document
                    AddHandler frm_viewgloLab.EntOpenEducation, AddressOf CType(Me.ParentForm, MainMenu).OpenEducation

                    frm_viewgloLab.objgloLabPatientExam = New clsPatientExams

                    frm_viewgloLab.objgloLabPatientMessages = New clsMessage
                    frm_viewgloLab.objgloLabPatientLetters = New clsPatientLetters
                    frm_viewgloLab.objgloLabNurseNotes = New clsNurseNotes
                    frm_viewgloLab.objgloLabHistory = New clsPatientHistory
                    frm_viewgloLab.objgloLabLabs = New clsLabs
                    frm_viewgloLab.objgloLabDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                    frm_viewgloLab.objgloLabRxmed = New clsPatientDetails
                    frm_viewgloLab.objgloLabOrders = New clsPatientDetails
                    frm_viewgloLab.objgloLabProblemList = New clsPatientProblemList

                    frm_viewgloLab.objgloLabCriteria = New DocCriteria
                    frm_viewgloLab.objgloLabWord = New clsWordDocument



                    .SelectOrderTab = True
                    'End -Sanjog
                    ''added for split control functionality order & result screen
                    Dim objclsSplit_Laborder As New gloEMRGeneralLibrary.clsSplitScreen()

                    objclsSplit_Laborder.blnShowSmokingStatusCol = gblnShowSmokingColumn

                    ' ofrmViewgloLab.objCriteria = New DocCriteria
                    ' ofrmViewgloLab.objWord = New clsWordDocument
                    frm_viewgloLab.VisitID = nVisitId
                    frm_viewgloLab.clsSplit_Laborder = objclsSplit_Laborder

                    objclsSplit_Laborder.Dispose()
                    objclsSplit_Laborder = Nothing

                    .LabOrderParameter.OrderNumberPrefix = dgLabs.Item(dgLabs.CurrentRowIndex, 0)
                    .LabOrderParameter.OrderID = OrderId
                    .LabOrderParameter.VisitID = nVisitId
                    .LabOrderParameter.PatientID = _PatientID
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False
                    .MdiParent = CType(Me.MdiParent, MainMenu)
                    .ShowInTaskbar = False
                    .WindowState = FormWindowState.Maximized
                    .Show()
                End With
                txtLabsrch.Text = ""
                txtSearch.Text = ""
                pnlLabs.BackgroundImage = gloEMR.My.Resources.Resources.Img_Button
                'Removed as per new lab changes in 5050-- AS we have implemented old labs. by madan 20100619
                'Else
                '    'Create instance of Class to get message User 
                '    '' By Mahesh 20070120 - SingleTon Pattern
                '    Dim frm As New frmLab_RequestOrder()

                '    With frm
                '        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                '        '''' Hide Tool Bar Mahesh 20070613
                '        CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False

                '        .MdiParent = CType(Me.MdiParent, MainMenu)
                '        '.dtOrderTime.Enabled = False
                '        .ShowInTaskbar = False
                '        .OrderPrefix = dgData.Item(dgData.CurrentRowIndex, 0)
                '        .OrderID = dgData.Item(dgData.CurrentRowIndex, 1)
                '        .VisitID = nVisitId

                '        .bIsOpenfrmOutstanding = True
                '        .WindowState = FormWindowState.Maximized
                '        .Show()
                '    End With
                'End If
            End If
        End If



    End Sub

    Private Sub Fill_Criterias()
        With trvCriteria
            .Nodes.Clear()
            Dim RootNode As New TreeNode
            Dim nde As TreeNode

            RootNode.Text = "Outstanding Orders"
            RootNode.ImageIndex = 7
            RootNode.SelectedImageIndex = 7
            .Nodes.Add(RootNode)

            nde = New TreeNode
            nde.Text = "Today"
            nde.ImageIndex = 2 '1
            nde.SelectedImageIndex = 2 '1
            RootNode.Nodes.Add(nde)

            nde = New TreeNode
            nde.Text = "Yesterday"
            nde.ImageIndex = 3 '1
            nde.SelectedImageIndex = 3 '1
            RootNode.Nodes.Add(nde)

            nde = New TreeNode
            nde.Text = "Last Week"
            nde.ImageIndex = 4 '1
            nde.SelectedImageIndex = 4 '1
            RootNode.Nodes.Add(nde)

            nde = New TreeNode
            nde.Text = "Last Month"
            nde.ImageIndex = 5 '1
            nde.SelectedImageIndex = 5 '1
            RootNode.Nodes.Add(nde)
            nde = New TreeNode

            nde.Text = "Customize"
            nde.ImageIndex = 6 '1
            nde.SelectedImageIndex = 6 '1
            RootNode.Nodes.Add(nde)

            '.Nodes(0).Nodes.Add("Today")
            '.Nodes(0).Nodes.Add("Yesterday")
            '.Nodes(0).Nodes.Add("Last Week")
            '.Nodes(0).Nodes.Add("Last Month")
            '.Nodes(0).Nodes.Add("Customize")
            .SelectedNode = .Nodes(0).Nodes(0)
            .ExpandAll()
        End With
    End Sub

    Private Sub trvCriteria_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCriteria.AfterSelect
        ''''Added by Anil on 20071211
        If Trim(trvCriteria.SelectedNode.Text) = "Customize" Then
            dtFrom.Enabled = True
            dtTo.Enabled = True
            lblFrom.Enabled = True
            lblTo.Enabled = True
            dtFrom.Value = Date.Now
            dtTo.Value = Date.Now
        Else
            dtFrom.Enabled = False
            dtTo.Enabled = False
            lblFrom.Enabled = False
            lblTo.Enabled = False
        End If
        ''''''''
    End Sub

    Private Sub trvCriteria_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCriteria.MouseClick

    End Sub
    ''Problem No: 00000096 : EMR Settings
    ''Reason: Selected providerId is pass to Fill_RadiologyOrder(),Fill_LabOrder(),Fill_LabOrderChange().
    Private Sub trvCriteria_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCriteria.MouseDown

        Try

            RemoveHandler dtFrom.TextChanged, AddressOf dtFrom_TextChanged
            RemoveHandler dtTo.TextChanged, AddressOf dtTo_TextChanged

            trvNode = trvCriteria.GetNodeAt(e.X, e.Y)

            If IsNothing(trvNode) = False Then

                trvCriteria.SelectedNode = trvNode

                'If selectedOrder = OrderFor.Radiology Then
                    Call Fill_RadiologyOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
                'ElseIf selectedOrder = OrderFor.Labs Then
                'Call Fill_LabOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
                'End If

                Fill_LabOrderChange(Convert.ToInt64(cmbProviders.SelectedValue.ToString()), cboOrderStatus.SelectedValue)

            End If


            AddHandler dtFrom.TextChanged, AddressOf dtFrom_TextChanged
            AddHandler dtTo.TextChanged, AddressOf dtTo_TextChanged

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to show Labs due to " & objErr.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    ''Problem No: 00000096 : EMR Settings
    ''Reason: Selected providerId is pass to Fill_RadiologyOrder(),Fill_LabOrder(),Fill_LabOrderChange().
    Private Sub dgData_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgData.MouseDoubleClick
        Try
            'If IsNothing(trvCriteria.SelectedNode) Then
            '    Dim mytreenode As New myTreeNode
            '    mytreenode.Text = "Today"
            '    trvCriteria.SelectedNode = mytreenode
            'End If
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgData.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                'If selectedOrder = OrderFor.Radiology Then
                If MainMenu.IsAccess(False, dgData.Item(dgData.CurrentRowIndex, 6), , True) = False Then
                    Exit Sub
                End If
                OpenRadiology()
                Fill_RadiologyOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
                'ElseIf selectedOrder = OrderFor.Labs Then
                '    OpenLabs()
                'End If

            Else
                Exit Sub
            End If


        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to open Labs due to " & objErr.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub dgData_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgData.MouseUp
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgData.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                dgData.Select(htInfo.Row)
                Panel6.BackgroundImage = gloEMR.My.Resources.Resources.Img_Rx_MxGreen
                pnlLabs.BackgroundImage = gloEMR.My.Resources.Resources.Img_Button
                blnLabs = False
            Else
                Exit Sub
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Outstanding Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub frmOutstandingOrders_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                Dim frm As MainMenu
                frm = Me.MdiParent
                If frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Or frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicSleeping Then
                    frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                End If
                frm = Nothing
            End If


            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Close, "Labs Report Closed", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub Labs()
        Try
            ''Added on 20110129-To fix issue:#7983 and 7984-Labs was not getting open on OPEN click and double click
            If blnLabs = False Then
                If dgData.CurrentRowIndex >= 0 Then
                    If dgData.IsSelected(dgData.CurrentRowIndex) Then
                        'If selectedOrder = OrderFor.Radiology Then
                        Call OpenRadiology()
                        'End If

                    End If
                End If
            ElseIf blnLabs = True Then
                If dgLabs.CurrentRowIndex >= 0 Then
                    If dgLabs.IsSelected(dgLabs.CurrentRowIndex) And blnLabs = True Then
                        Call OpenLabs()
                    End If
                End If
            End If


        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Close, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Open"
                Call Labs()
            Case "Close"
                Call FormClose()
        End Select
    End Sub



    ''Problem No: 00000096 : EMR Settings
    ''Reason: Selected providerId is pass to Fill_RadiologyOrder(),Fill_LabOrder(),Fill_LabOrderChange().
    Private Sub dtFrom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If selectedOrder = OrderFor.Radiology Then
        Fill_RadiologyOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
        'ElseIf selectedOrder = OrderFor.Labs Then
        'Fill_LabOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
        'End If
        Fill_LabOrderChange(Convert.ToInt64(cmbProviders.SelectedValue.ToString()), cboOrderStatus.SelectedValue)
    End Sub

    ''Problem No: 00000096 : EMR Settings
    ''Reason: Selected providerId is pass to Fill_RadiologyOrder(),Fill_LabOrder(),Fill_LabOrderChange().
    Private Sub dtTo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If selectedOrder = OrderFor.Radiology Then
        Fill_RadiologyOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
        'ElseIf selectedOrder = OrderFor.Labs Then
        'Fill_LabOrder()
        'End If
        Fill_LabOrderChange(Convert.ToInt64(cmbProviders.SelectedValue.ToString()), cboOrderStatus.SelectedValue)
    End Sub

    'Private Sub rbLabs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbLabs.Click
    '    rbLabs.Checked = True
    '    rbRadiology.Checked = False
    '    selectedOrder = OrderFor.Labs
    '    Fill_LabOrder()
    '    rbAll_Click(sender, e)
    '    rbSelected_Click(sender, e)
    '    'lblHeader.Text = " Outstanding Orders"
    'End Sub

    'Private Sub rbRadiology_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    rbLabs.Checked = False
    '    rbRadiology.Checked = True
    '    selectedOrder = OrderFor.Radiology
    '    Fill_RadiologyOrder()
    '    rbAll_Click(sender, e)
    '    rbSelected_Click(sender, e)
    '    'lblHeader.Text = " Outstanding Orders"
    'End Sub

    'Private Sub rbLabs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rbLabs.Checked = True Then
    '        rbLabs.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rbLabs.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    'Private Sub rbRadiology_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rbRadiology.Checked = True Then
    '        rbRadiology.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rbRadiology.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If


        End Try
    End Sub
#Region "Added Rahul For new Implementation of All Patients & Selected Patients on 20101021."
    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                ''select the searched patient
                If dgData.VisibleRowCount > 0 And txtSearch.Text.Trim() <> "" Then
                    _selectedpatientid = 0
                    'Dim erg As DataGridViewCellEventArgs
                    'erg = New DataGridViewCellEventArgs(0, 0)
                    Search()
                    '' Select The 0th Index i.e 1st row of Patient 
                    '                    dgData.CurrentCell.ColumnNumber = dgData.CurrentCell.ColumnIndex
                    '' dgPatientView_CellClick(null, erg)
                    dgData.Focus()
                End If
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), True)
        Finally
        End Try
    End Sub
    Private Sub Search()
        Me.SuspendLayout()
        Try
            If IsNothing(dgData) = True Then
                Me.Cursor = Cursors.Default
                Return
            End If
            'Dim PatientNm As Byte = 7
            'Dim PatientCat As Byte = 3
            'Dim PatientTest As Byte = 4


            Dim str As String = ""
            'Dim rowid As Integer
            Dim strSearchArray(9999) As String
            Dim i As Integer

            str = txtSearch.Text
            str = str.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%")

            If str.Trim() <> "" Then
                strSearchArray = str.Split(",")
                Dim strSearch As String = ""
                If strSearchArray.Length = 1 Then
                    strSearch = strSearchArray(0)
                    If strSearch.Length > 1 Then
                        Dim _str As String = strSearch.Substring(1).Replace("%", "")
                        strSearch = strSearch.Substring(0, 1) + _str
                    End If
                    If PatientCat.ToString().Trim <> "" Then
                        dvdata.RowFilter = dvdata.Table.Columns(7).ColumnName + " Like '%" + strSearch + "%' OR " & _
                                                  dvdata.Table.Columns(3).ColumnName + " Like '%" + strSearch + "%' OR " & _
                                                  dvdata.Table.Columns(4).ColumnName + " Like '%" + strSearch + "%' "
                    Else

                        dvdata.RowFilter = dvdata.Table.Columns(7).ColumnName + " Like '%" + strSearch + "%' OR " & _
                                            dvdata.Table.Columns(PatientInstruction).ColumnName + " Like '%" + strSearch + "%' OR " & _
                                            dvdata.Table.Columns(4).ColumnName + " Like '%" + strSearch + "%'  "
                        'PatientInstruction + " Like '%" + strSearch + "%' OR " & _
                    End If


                Else
                    For i = 0 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i)
                        If strSearch.Length > 1 Then
                            Dim _str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) + _str
                        End If

                        If strSearch.Trim() <> "" Then

                            'If i = 0 Then
                            '    dtTemp = dvdata.ToTable()
                            '    dvNext = dtTemp.DefaultView
                            'Else
                            '    dtTemp = dvNext.ToTable()
                            '    dvNext = dtTemp.DefaultView
                            'End If
                            If i = 0 Then
                                dtTemp = dvdata.ToTable()
                                dvNext = dtTemp.Copy().DefaultView
                                dtTemp.Dispose()
                                dtTemp = Nothing
                            Else
                                dtTemp = dvNext.ToTable()
                                dvNext = dtTemp.Copy().DefaultView
                                dtTemp.Dispose()
                                dtTemp = Nothing
                            End If

                            Dim strFilter As String
                            strFilter = dvdata.Table.Columns(PatientNm).ColumnName + " Like '%" + strSearch + "%' OR " & _
                                              dvdata.Table.Columns(PatientCat).ColumnName + " Like '%" + strSearch + "%' OR " & _
                                              dvdata.Table.Columns(PatientTest).ColumnName + " Like '%" + strSearch + "%' "
                            dvNext.RowFilter = strFilter
                        End If
                    Next
                End If

                If strSearch <> "" And strSearch.Trim() <> "" Then
                    If strSearchArray.Length = 1 Then
                        dgData.DataSource = dvdata.ToTable()
                        ''dgPatientView.DataSource = dvdata
                        _VisiblePatientCount = dvdata.Count
                    Else
                        dgData.DataSource = dvNext
                        '' dgPatientView.DataSource = dvNext
                        _VisiblePatientCount = dvNext.Count
                    End If
                End If
            Else
                If Not IsNothing(dvdata) Then
                    dvdata.RowFilter = ""
                    dgData.DataSource = dvdata
                    ''dgPatientView.DataSource = dvdata
                    _VisiblePatientCount = dvdata.Count
                End If
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), True)
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub SearchLabs()

        Me.SuspendLayout()
        Try
            If IsNothing(dgLabs) = True Then
                Me.Cursor = Cursors.Default
                Return
            End If


            Dim str As String = ""

            Dim strSearchArray(9999) As String
            Dim i As Integer

            str = txtLabsrch.Text
            str = str.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%")

            If str.Trim() <> "" Then
                strSearchArray = str.Split(",")
                Dim strSearch As String = ""
                If strSearchArray.Length = 1 Then
                    strSearch = strSearchArray(0)
                    If strSearch.Length > 1 Then
                        Dim _str As String = strSearch.Substring(1).Replace("%", "")
                        strSearch = strSearch.Substring(0, 1) + _str
                    End If

                    dvLabs.RowFilter = dvLabs.Table.Columns(PatientLNm).ColumnName + " Like '%" + strSearch + "%' OR " & _
                                           dvLabs.Table.Columns(PatientInstruction).ColumnName + " Like '%" + strSearch + "%' OR " & _
                                           dvLabs.Table.Columns(PatientLTest).ColumnName + " Like '%" + strSearch + "%'  "


                Else
                    For i = 0 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i)
                        If strSearch.Length > 1 Then
                            Dim _str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) + _str
                        End If

                        If strSearch.Trim() <> "" Then

                            If i = 0 Then
                                dtTemp = dvLabs.ToTable()
                                dvNext = dtTemp.DefaultView
                            Else
                                dtTemp = dvNext.ToTable()
                                dvNext = dtTemp.DefaultView
                            End If

                        End If
                    Next
                End If

                If strSearch <> "" And strSearch.Trim() <> "" Then
                    If strSearchArray.Length = 1 Then
                        dgLabs.DataSource = dvLabs.ToTable()
                        ''dgPatientView.DataSource = dvLabs
                        _VisiblePatientCount = dvLabs.Count
                    Else
                        dgLabs.DataSource = dvNext
                        '' dgPatientView.DataSource = dvNext
                        _VisiblePatientCount = dvNext.Count
                    End If
                End If
            Else
                If Not IsNothing(dvLabs) Then
                    dvLabs.RowFilter = ""
                    dgLabs.DataSource = dvLabs
                    ''dgPatientView.DataSource = dvLabs
                    _VisiblePatientCount = dvLabs.Count
                End If
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), True)
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub txtLabsrch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLabsrch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                ''select the searched patient
                If dgLabs.VisibleRowCount > 0 And txtLabsrch.Text.Trim() <> "" Then
                    _selectedpatientid = 0
                    'Dim erg As DataGridViewCellEventArgs
                    'erg = New DataGridViewCellEventArgs(0, 0)
                    SearchLabs()
                    '' Select The 0th Index i.e 1st row of Patient 
                    '                    dgData.CurrentCell.ColumnNumber = dgData.CurrentCell.ColumnIndex
                    '' dgPatientView_CellClick(null, erg)
                    dgLabs.Focus()
                End If
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), True)
        Finally
        End Try
    End Sub


    Private Sub dgLabs_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgLabs.MouseDoubleClick
        Try
            'If IsNothing(trvCriteria.SelectedNode) Then
            '    Dim mytreenode As New myTreeNode
            '    mytreenode.Text = "Today"
            '    trvCriteria.SelectedNode = mytreenode
            'End If

            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgLabs.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then


                OpenLabs()
            Else
                Exit Sub
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to open Labs due to " & objErr.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub dgLabs_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgLabs.MouseUp
        Try
            dgData.Refresh()
            'pnlLabs.BackColor = System.Drawing.Color.Green

            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgLabs.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                dgLabs.Select(htInfo.Row)
                pnlLabs.BackgroundImage = gloEMR.My.Resources.Resources.Img_Rx_MxGreen
                Panel6.BackgroundImage = gloEMR.My.Resources.Resources.Img_Button
                blnLabs = True
            Else
                Exit Sub
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Outstanding Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearOrders.Click
        txtSearch.Text = ""
    End Sub

    Private Sub btnClearLabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearLabs.Click
        txtLabsrch.Text = ""
    End Sub

    Private Sub rbAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAll.CheckedChanged
        If rbAll.Checked = True Then
            txtSearch.Enabled = True
            txtLabsrch.Enabled = True
            txtSearch.Text = ""
            txtLabsrch.Text = ""
            txtSearch.Focus()
            If IsNothing(dvdata) = False Then
                dvdata.RowFilter = ""
                dgData.DataSource = dvdata.ToTable()
            End If
            If IsNothing(dvLabs) = False Then
                dvLabs.RowFilter = ""
                dgLabs.DataSource = dvLabs.ToTable()
            End If

        End If
    End Sub

    Private Sub rbAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbAll.Click
        If rbAll.Checked = True Then
            rbSelected.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rbAll.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            ' lblSearch.Enabled = True
            txtSearch.Enabled = True
            txtSearch.Text = ""
            txtLabsrch.Enabled = True
            txtLabsrch.Text = ""
            txtSearch.Focus()
            If IsNothing(dvdata) = False Then
                dvdata.RowFilter = ""
                dgData.DataSource = dvdata.ToTable()
            End If


            If IsNothing(dvLabs) = False Then
                '' dvdata.RowFilter = dvdata.Table.Columns(10).ColumnName + " = '" + gstrPatientCode + "'"
                dvLabs.RowFilter = ""
                ''Convert.ToString(gnPatientID) 
                dgLabs.DataSource = dvLabs.ToTable()
            End If
        Else
            rbSelected.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbAll.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbSelected_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSelected.CheckedChanged
        If rbSelected.Checked = True Then
            'lblSearch.Enabled = False
            txtSearch.Enabled = False
            txtLabsrch.Enabled = False
            txtSearch.Text = ""
            txtLabsrch.Text = ""
            If IsNothing(dvdata) = False Then
                '' dvdata.RowFilter = dvdata.Table.Columns(10).ColumnName + " = '" + gstrPatientCode + "'"
                dvdata.RowFilter = "sPatientCode = '" + strPatientCode + "'"
                ''Convert.ToString(gnPatientID) 
                dgData.DataSource = dvdata.ToTable()
            End If
            If IsNothing(dvLabs) = False Then
                '' dvdata.RowFilter = dvdata.Table.Columns(10).ColumnName + " = '" + gstrPatientCode + "'"
                dvLabs.RowFilter = "sPatientCode = '" + strPatientCode + "'"
                ''Convert.ToString(gnPatientID) 
                dgLabs.DataSource = dvLabs.ToTable()
            End If

        End If
    End Sub

    Private Sub rbSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSelected.Click
        If rbSelected.Checked = True Then
            rbSelected.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbAll.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            'lblSearch.Enabled = False
            txtSearch.Enabled = False
            txtSearch.Text = ""
            txtLabsrch.Enabled = False
            txtLabsrch.Text = ""
            If IsNothing(dvdata) = False Then
                '' dvdata.RowFilter = dvdata.Table.Columns(10).ColumnName + " = '" + gstrPatientCode + "'"
                dvdata.RowFilter = "sPatientCode = '" + strPatientCode + "'"
                ''Convert.ToString(gnPatientID) 
                dgData.DataSource = dvdata.ToTable()
            End If
            If IsNothing(dvLabs) = False Then
                '' dvdata.RowFilter = dvdata.Table.Columns(10).ColumnName + " = '" + gstrPatientCode + "'"
                dvLabs.RowFilter = "sPatientCode = '" + strPatientCode + "'"
                ''Convert.ToString(gnPatientID) 
                dgLabs.DataSource = dvLabs.ToTable()
            End If

        Else
            rbSelected.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rbAll.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        End If
    End Sub

    ''Problem No: 00000096 : EMR Settings
    ''Reason: check whether _providerID and login providerID are different then pass selected providerID to criteria.
    Public Sub Fill_LabOrderChange(Optional ByVal _ProviderID As Int64 = 0, Optional ByVal OrderStatus As Int64 = 0)

        If IsNothing(trvCriteria.SelectedNode) = True Then Exit Sub
        If trvCriteria.SelectedNode Is trvCriteria.Nodes(0) Then Exit Sub

        If _ProviderID <> _nLoginProviderId Then
            _ProviderID = cmbProviders.SelectedValue
        End If

        Dim dtLabsOrders As New DataTable
        Dim oLabOrderRequest As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder
        Dim _DateRange() As DateTime

        Select Case Trim(trvCriteria.SelectedNode.Text)
            Case "Today"
                _DateRange = GetDateRange(DateCategory.Today)

                If _DateRange.Length > 0 Then
                    dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID, OrderStatus)
                    lblFrom.Text = "Date"
                    lblTo.Visible = False
                    dtFrom.Value = _DateRange(0)
                    dtTo.Visible = False
                    dtTo.Visible = False
                End If

            Case "Yesterday"
                _DateRange = GetDateRange(DateCategory.Yesterday)
                If _DateRange.Length > 0 Then

                    dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID, OrderStatus)
                    lblFrom.Text = "Date"
                    lblTo.Visible = False
                    dtFrom.Value = _DateRange(0)
                    dtTo.Visible = False

                End If

            Case "Last Week"
                _DateRange = GetDateRange(DateCategory.LastWeek)
                If _DateRange.Length > 0 Then
                    dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0).AddDays(1), _DateRange(1).AddDays(1), _ProviderID, OrderStatus)
                    lblFrom.Text = "From"
                    lblTo.Text = "To"
                    lblTo.Visible = True
                    dtTo.Visible = True
                    dtFrom.Value = _DateRange(0).AddDays(1)
                    dtTo.Value = _DateRange(1)
                End If
            Case "Last Month"
                _DateRange = GetDateRange(DateCategory.LastMonth)
                If _DateRange.Length > 0 Then
                    dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_DateRange(0), _DateRange(1).AddDays(1), _ProviderID, OrderStatus)
                    lblFrom.Text = "From"
                    lblTo.Text = "To"
                    lblTo.Visible = True
                    dtTo.Visible = True
                    dtFrom.Value = _DateRange(0)
                    dtTo.Value = _DateRange(1)
                End If
            Case "Customize"
                Dim _dtFrom As Date = CType(dtFrom.Text, Date) ' Format(dtFrom.Text, "MM/dd/YYYY")
                Dim _dtTo As Date = CType(dtTo.Text, Date) 'Format(dtTo.Text, "MM/dd/YYYY")
                dtLabsOrders = oLabOrderRequest.GetOutStandingOrder(_dtFrom, _dtTo.AddDays(1), _ProviderID, OrderStatus)
                lblFrom.Text = "From"
                lblTo.Text = "To"
                lblTo.Visible = True
                dtTo.Visible = True

        End Select

        oLabOrderRequest.Dispose()
        oLabOrderRequest = Nothing
        dgLabs.Enabled = False
        dgLabs.DataSource = dtLabsOrders


        dgLabs.Enabled = True
        dgLabs.TableStyles.Clear()
        Dim grdTableStyleLabs As New clsDataGridTableStyle(dtLabsOrders.TableName)

        Dim grdColStyleOrderIDPrefix As New DataGridTextBoxColumn

        With grdColStyleOrderIDPrefix
            .HeaderText = "OrederID PreFix"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labom_OrderNoPrefix").ColumnName
            .NullText = ""
            .Width = 0
        End With



        Dim grdColStyleID As New DataGridTextBoxColumn

        With grdColStyleID
            .HeaderText = "ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labom_OrderID").ColumnName
            .NullText = ""
            .Width = 0
        End With


        Dim grdColStylePatient As New DataGridTextBoxColumn
        With grdColStylePatient
            .HeaderText = "Patient Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("PatientName").ColumnName
            .NullText = ""
            PatientLNm = dtLabsOrders.Columns("PatientName").ColumnName
            .Width = 205
        End With


        Dim grdColStyleDate As New DataGridTextBoxColumn
        With grdColStyleDate
            .HeaderText = "Order Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labom_OrderDate").ColumnName
            .NullText = ""


            .Width = 154
        End With


        Dim grdColStyleCategory As New DataGridTextBoxColumn
        With grdColStyleCategory
            .HeaderText = "Test Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labtm_Name").ColumnName
            .NullText = ""
            PatientLTest = dtLabsOrders.Columns("labtm_Name").ColumnName
            .Width = 350
        End With

        Dim grdColStyleTestID As New DataGridTextBoxColumn
        With grdColStyleTestID
            .HeaderText = "Test ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labtm_ID").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStyleVisitID As New DataGridTextBoxColumn
        With grdColStyleVisitID
            .HeaderText = "Visit ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labom_VisitID").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "Patient ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labom_PatientID").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Patient Code"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("sPatientCode").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStyleProviderID As New DataGridTextBoxColumn
        With grdColStyleProviderID
            .HeaderText = "Provider ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labom_ProviderID").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStyleOrderNoID As New DataGridTextBoxColumn

        With grdColStyleOrderNoID
            .HeaderText = "Order Number ID PreFix"
            .Alignment = HorizontalAlignment.Left
            .NullText = ""
            .Width = 0
        End With


        Dim grdColStyleInstruction As New DataGridTextBoxColumn

        With grdColStyleInstruction
            .HeaderText = "Instruction"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("labotd_Instruction").ColumnName
            PatientInstruction = dtLabsOrders.Columns("labotd_Instruction").ColumnName
            .NullText = ""
            .Width = 256
        End With


        Dim grdColStyleOrderStatus As New DataGridTextBoxColumn

        With grdColStyleOrderStatus
            .HeaderText = "Order Status"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLabsOrders.Columns("OrderStatus").ColumnName
            .NullText = ""
            .Width = 154
        End With


        dvLabs = New DataView(dgLabs.DataSource)

        GetSelectedPatientRecordLabs()
        grdTableStyleLabs.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleOrderIDPrefix, grdColStyleID, grdColStylePatient, grdColStyleDate, grdColStyleCategory, grdColStyleTestID, grdColStyleVisitID, grdColStylePatientID, grdColStylePatientCode, grdColStyleProviderID, grdColStyleOrderNoID, grdColStyleOrderStatus, grdColStyleInstruction})
        dgLabs.TableStyles.Clear()
        dgLabs.TableStyles.Add(grdTableStyleLabs)

    End Sub

    Public Sub GetSelectedPatientRecord()
        If rbSelected.Checked = True Then
            txtSearch.Enabled = False
            txtLabsrch.Enabled = False
            txtSearch.Text = ""
            txtLabsrch.Text = ""
            If IsNothing(dvdata) = False Then
                dvdata.RowFilter = "sPatientCode = '" + strPatientCode + "'"
                dgData.DataSource = dvdata.ToTable()

            End If
        Else
            If IsNothing(dvdata) = False Then
                dvdata.RowFilter = ""
                dgData.DataSource = dvdata.ToTable()

            End If
        End If
    End Sub

    Public Sub GetSelectedPatientRecordLabs()
        If rbSelected.Checked = True Then
            txtSearch.Enabled = False
            txtLabsrch.Enabled = False
            txtSearch.Text = ""
            txtLabsrch.Text = ""
            If IsNothing(dvLabs) = False Then
                dvLabs.RowFilter = "sPatientCode = '" + strPatientCode + "'"

                dgLabs.DataSource = dvLabs.ToTable()
            End If
        Else
            If IsNothing(dvdata) = False Then
                dvLabs.RowFilter = ""

                dgLabs.DataSource = dvLabs.ToTable()

            End If
        End If
    End Sub

    Private Sub txtLabsrch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLabsrch.TextChanged
        SearchLabs()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Search()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), True)
        Finally
        End Try
    End Sub

#End Region
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    ''Problem No: 00000096 : EMR Settings
    ''Reason: Selected providerId is pass to Fill_RadiologyOrder(),Fill_LabOrder(),Fill_LabOrderChange().
    Private Sub cmbProviders_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProviders.SelectionChangeCommitted
        If cmbProviders.Items.Count > 0 Then
            Fill_RadiologyOrder(Convert.ToInt64(cmbProviders.SelectedValue.ToString()))
            Fill_LabOrderChange(Convert.ToInt64(cmbProviders.SelectedValue.ToString()), cboOrderStatus.SelectedValue)
        End If
    End Sub

    Private Sub cboOrderStatus_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)
        Fill_LabOrderChange(Convert.ToInt64(cmbProviders.SelectedValue.ToString()), cboOrderStatus.SelectedValue)
    End Sub

End Class
