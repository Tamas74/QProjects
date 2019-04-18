<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab_RequestOrder
    Inherits System.Windows.Forms.Form

#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmLab_RequestOrder

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
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
        frm = Nothing
        Me.blnDisposed = True

    End Sub

    'Public Overloads Sub Dispose()
    '    Dispose(True)
    '    ' Take yourself off of the finalization queue
    '    ' to prevent finalization code for this object
    '    ' from executing a second time.
    '    GC.SuppressFinalize(Me)
    'End Sub

    'Protected Overrides Sub Finalize()
    '    Dispose(False)
    'End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Public Shared Function GetInstance(ByVal blnRecordLock As Boolean) As frmLab_RequestOrder
        '_mu.WaitOne()
        Try
            If frm Is Nothing Then
                frm = New frmLab_RequestOrder(blnRecordLock)
            End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

#End Region

    'Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing AndAlso components IsNot Nothing Then
    '        components.Dispose()
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_RequestOrder))
        Me.Splitter3 = New System.Windows.Forms.Splitter
        Me.pnlTransactionHistory = New System.Windows.Forms.Panel
        Me.GloUC_TransactionHistory = New gloUserControlLibrary.gloUC_TransactionHistory
        Me.spltTransactionHistory = New System.Windows.Forms.Splitter
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.splRight = New System.Windows.Forms.Splitter
        Me.pnlPrevious = New System.Windows.Forms.Panel
        Me.gloUCLab_History = New gloUserControlLibrary.gloUC_LabHistory
        Me.splLeft = New System.Windows.Forms.Splitter
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.pnlList_Detail = New System.Windows.Forms.Panel
        Me.pnltrvList = New System.Windows.Forms.Panel
        Me.GloUC_trvTest = New gloUserControlLibrary.gloUC_TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.trvList = New System.Windows.Forms.TreeView
        Me.pnl_btnTests = New System.Windows.Forms.Panel
        Me.btnTests = New System.Windows.Forms.Button
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.pnl_btnGroups = New System.Windows.Forms.Panel
        Me.btnGroups = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.txtListSearch = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.label9 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.label10 = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        Me.pnlBorder_Right = New System.Windows.Forms.Panel
        Me.pnlBorder_Bottom = New System.Windows.Forms.Panel
        Me.ts_LabMain = New gloToolStrip.gloToolStrip
        Me.tlbbtn_New = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Save = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Finish = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Print = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Fax = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Previous = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_HL7 = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Acknowledgment = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_VWAcknowledgment = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_PrvLabs = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnMicrophone = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep1 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnMasters = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep2 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnNewPatient = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnModifyPatient = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep3 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnHistory = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnPrescription = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnMedication = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnOrders = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep4 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnNewExam = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnPastExam = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnUnFinishedExams = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep5 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnCalendar = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnTasks = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnMessages = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep10 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnScanDocs = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnDocMGMT = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep7 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSettings = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep8 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnFormGallery = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnLockScreen = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep9 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnClose = New System.Windows.Forms.ToolBarButton
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.gloUCLab_Transaction = New gloUserControlLibrary.gloUC_Transaction
        Me.gloUCLab_TestDetail = New gloUserControlLibrary.gloUC_LabTest
        Me.gloUCLab_OrderDetail = New gloUserControlLibrary.gloUC_LabOrderDetail
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.pnlTransactionHistory.SuspendLayout()
        Me.pnlPrevious.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnlList_Detail.SuspendLayout()
        Me.pnltrvList.SuspendLayout()
        Me.pnl_btnTests.SuspendLayout()
        Me.pnl_btnGroups.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ts_LabMain.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter3.Location = New System.Drawing.Point(239, 273)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(543, 3)
        Me.Splitter3.TabIndex = 29
        Me.Splitter3.TabStop = False
        '
        'pnlTransactionHistory
        '
        Me.pnlTransactionHistory.Controls.Add(Me.GloUC_TransactionHistory)
        Me.pnlTransactionHistory.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlTransactionHistory.Location = New System.Drawing.Point(239, 276)
        Me.pnlTransactionHistory.Name = "pnlTransactionHistory"
        Me.pnlTransactionHistory.Size = New System.Drawing.Size(543, 235)
        Me.pnlTransactionHistory.TabIndex = 28
        '
        'GloUC_TransactionHistory
        '
        Me.GloUC_TransactionHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GloUC_TransactionHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TransactionHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GloUC_TransactionHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GloUC_TransactionHistory.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_TransactionHistory.Name = "GloUC_TransactionHistory"
        Me.GloUC_TransactionHistory.Padding = New System.Windows.Forms.Padding(3)
        Me.GloUC_TransactionHistory.Size = New System.Drawing.Size(543, 235)
        Me.GloUC_TransactionHistory.TabIndex = 0
        '
        'spltTransactionHistory
        '
        Me.spltTransactionHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltTransactionHistory.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.spltTransactionHistory.Location = New System.Drawing.Point(239, 511)
        Me.spltTransactionHistory.Name = "spltTransactionHistory"
        Me.spltTransactionHistory.Size = New System.Drawing.Size(543, 3)
        Me.spltTransactionHistory.TabIndex = 27
        Me.spltTransactionHistory.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Enabled = False
        Me.Splitter1.Location = New System.Drawing.Point(239, 127)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(543, 2)
        Me.Splitter1.TabIndex = 18
        Me.Splitter1.TabStop = False
        '
        'splRight
        '
        Me.splRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.splRight.Location = New System.Drawing.Point(782, 0)
        Me.splRight.Name = "splRight"
        Me.splRight.Size = New System.Drawing.Size(4, 598)
        Me.splRight.TabIndex = 7
        Me.splRight.TabStop = False
        '
        'pnlPrevious
        '
        Me.pnlPrevious.Controls.Add(Me.gloUCLab_History)
        Me.pnlPrevious.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlPrevious.Location = New System.Drawing.Point(786, 0)
        Me.pnlPrevious.Name = "pnlPrevious"
        Me.pnlPrevious.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlPrevious.Size = New System.Drawing.Size(242, 598)
        Me.pnlPrevious.TabIndex = 6
        '
        'gloUCLab_History
        '
        Me.gloUCLab_History.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.gloUCLab_History.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloUCLab_History.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gloUCLab_History.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gloUCLab_History.Location = New System.Drawing.Point(0, 3)
        Me.gloUCLab_History.Name = "gloUCLab_History"
        Me.gloUCLab_History.Size = New System.Drawing.Size(242, 595)
        Me.gloUCLab_History.TabIndex = 0
        '
        'splLeft
        '
        Me.splLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splLeft.Location = New System.Drawing.Point(235, 0)
        Me.splLeft.Name = "splLeft"
        Me.splLeft.Size = New System.Drawing.Size(4, 598)
        Me.splLeft.TabIndex = 5
        Me.splLeft.TabStop = False
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLeft.Controls.Add(Me.pnlList_Detail)
        Me.pnlLeft.Controls.Add(Me.pnlSearch)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlLeft.Size = New System.Drawing.Size(235, 598)
        Me.pnlLeft.TabIndex = 4
        '
        'pnlList_Detail
        '
        Me.pnlList_Detail.Controls.Add(Me.pnltrvList)
        Me.pnlList_Detail.Controls.Add(Me.pnl_btnTests)
        Me.pnlList_Detail.Controls.Add(Me.pnl_btnGroups)
        Me.pnlList_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlList_Detail.Location = New System.Drawing.Point(0, 3)
        Me.pnlList_Detail.Name = "pnlList_Detail"
        Me.pnlList_Detail.Size = New System.Drawing.Size(235, 595)
        Me.pnlList_Detail.TabIndex = 8
        '
        'pnltrvList
        '
        Me.pnltrvList.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrvList.Controls.Add(Me.GloUC_trvTest)
        Me.pnltrvList.Controls.Add(Me.trvList)
        Me.pnltrvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvList.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnltrvList.Location = New System.Drawing.Point(0, 30)
        Me.pnltrvList.Name = "pnltrvList"
        Me.pnltrvList.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrvList.Size = New System.Drawing.Size(235, 535)
        Me.pnltrvList.TabIndex = 5
        '
        'GloUC_trvTest
        '
        Me.GloUC_trvTest.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvTest.CheckBoxes = False
        Me.GloUC_trvTest.CodeMember = Nothing
        Me.GloUC_trvTest.DDIDMember = Nothing
        Me.GloUC_trvTest.DescriptionMember = Nothing
        Me.GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        Me.GloUC_trvTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvTest.DrugFlag = CType(16, Short)
        Me.GloUC_trvTest.DrugFormMember = Nothing
        Me.GloUC_trvTest.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvTest.DurationMember = Nothing
        Me.GloUC_trvTest.FrequencyMember = Nothing
        Me.GloUC_trvTest.ImageIndex = 0
        Me.GloUC_trvTest.ImageList = Me.ImageList1
        Me.GloUC_trvTest.ImageObject = Nothing
        Me.GloUC_trvTest.IsDrug = False
        Me.GloUC_trvTest.IsNarcoticsMember = Nothing
        Me.GloUC_trvTest.Location = New System.Drawing.Point(3, 0)
        Me.GloUC_trvTest.MaximumNodes = 1000
        Me.GloUC_trvTest.Name = "GloUC_trvTest"
        Me.GloUC_trvTest.NDCCodeMember = Nothing
        Me.GloUC_trvTest.ParentImageIndex = 0
        Me.GloUC_trvTest.ParentMember = Nothing
        Me.GloUC_trvTest.RouteMember = Nothing
        Me.GloUC_trvTest.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvTest.SearchBox = True
        Me.GloUC_trvTest.SearchText = Nothing
        Me.GloUC_trvTest.SelectedImageIndex = 0
        Me.GloUC_trvTest.SelectedNode = Nothing
        Me.GloUC_trvTest.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvTest.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvTest.SelectedParentImageIndex = 0
        Me.GloUC_trvTest.Size = New System.Drawing.Size(232, 532)
        Me.GloUC_trvTest.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvTest.TabIndex = 42
        Me.GloUC_trvTest.Tag = Nothing
        Me.GloUC_trvTest.UnitMember = Nothing
        Me.GloUC_trvTest.ValueMember = Nothing
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        '
        'trvList
        '
        Me.trvList.BackColor = System.Drawing.Color.White
        Me.trvList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvList.ForeColor = System.Drawing.Color.Black
        Me.trvList.HideSelection = False
        Me.trvList.ImageIndex = 0
        Me.trvList.ImageList = Me.ImageList1
        Me.trvList.Indent = 20
        Me.trvList.ItemHeight = 20
        Me.trvList.Location = New System.Drawing.Point(8, 5)
        Me.trvList.Name = "trvList"
        Me.trvList.SelectedImageIndex = 0
        Me.trvList.ShowNodeToolTips = True
        Me.trvList.ShowPlusMinus = False
        Me.trvList.ShowRootLines = False
        Me.trvList.Size = New System.Drawing.Size(226, 500)
        Me.trvList.TabIndex = 3
        Me.trvList.Visible = False
        '
        'pnl_btnTests
        '
        Me.pnl_btnTests.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnTests.Controls.Add(Me.btnTests)
        Me.pnl_btnTests.Controls.Add(Me.Label15)
        Me.pnl_btnTests.Controls.Add(Me.Label16)
        Me.pnl_btnTests.Controls.Add(Me.Label17)
        Me.pnl_btnTests.Controls.Add(Me.Label18)
        Me.pnl_btnTests.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_btnTests.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnTests.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnTests.Location = New System.Drawing.Point(0, 0)
        Me.pnl_btnTests.Name = "pnl_btnTests"
        Me.pnl_btnTests.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_btnTests.Size = New System.Drawing.Size(235, 30)
        Me.pnl_btnTests.TabIndex = 4
        '
        'btnTests
        '
        Me.btnTests.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnTests.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnTests.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTests.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTests.FlatAppearance.BorderSize = 0
        Me.btnTests.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTests.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTests.Location = New System.Drawing.Point(4, 1)
        Me.btnTests.Name = "btnTests"
        Me.btnTests.Size = New System.Drawing.Size(230, 25)
        Me.btnTests.TabIndex = 0
        Me.btnTests.Text = "Tests"
        Me.btnTests.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(4, 26)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(230, 1)
        Me.Label15.TabIndex = 18
        Me.Label15.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 26)
        Me.Label16.TabIndex = 17
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(234, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 26)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(232, 1)
        Me.Label18.TabIndex = 15
        Me.Label18.Text = "label1"
        '
        'pnl_btnGroups
        '
        Me.pnl_btnGroups.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnGroups.Controls.Add(Me.btnGroups)
        Me.pnl_btnGroups.Controls.Add(Me.Label1)
        Me.pnl_btnGroups.Controls.Add(Me.Label2)
        Me.pnl_btnGroups.Controls.Add(Me.Label3)
        Me.pnl_btnGroups.Controls.Add(Me.Label4)
        Me.pnl_btnGroups.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnGroups.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnGroups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnGroups.Location = New System.Drawing.Point(0, 565)
        Me.pnl_btnGroups.Name = "pnl_btnGroups"
        Me.pnl_btnGroups.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_btnGroups.Size = New System.Drawing.Size(235, 30)
        Me.pnl_btnGroups.TabIndex = 4
        '
        'btnGroups
        '
        Me.btnGroups.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnGroups.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        Me.btnGroups.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGroups.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGroups.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnGroups.FlatAppearance.BorderSize = 0
        Me.btnGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGroups.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGroups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnGroups.Location = New System.Drawing.Point(4, 1)
        Me.btnGroups.Name = "btnGroups"
        Me.btnGroups.Size = New System.Drawing.Size(230, 25)
        Me.btnGroups.TabIndex = 2
        Me.btnGroups.Text = "Groups"
        Me.btnGroups.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(230, 1)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 26)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(234, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 26)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(232, 1)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtListSearch)
        Me.pnlSearch.Controls.Add(Me.Label20)
        Me.pnlSearch.Controls.Add(Me.Label21)
        Me.pnlSearch.Controls.Add(Me.PictureBox1)
        Me.pnlSearch.Controls.Add(Me.label9)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Controls.Add(Me.label10)
        Me.pnlSearch.Controls.Add(Me.label11)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(235, 29)
        Me.pnlSearch.TabIndex = 16
        Me.pnlSearch.Visible = False
        '
        'txtListSearch
        '
        Me.txtListSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtListSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtListSearch.ForeColor = System.Drawing.Color.Black
        Me.txtListSearch.Location = New System.Drawing.Point(32, 8)
        Me.txtListSearch.Name = "txtListSearch"
        Me.txtListSearch.Size = New System.Drawing.Size(202, 15)
        Me.txtListSearch.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(32, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(202, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(32, 23)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(202, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(4, 25)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(230, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(4, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(230, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'label10
        '
        Me.label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.label10.Location = New System.Drawing.Point(3, 3)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(1, 23)
        Me.label10.TabIndex = 39
        Me.label10.Text = "label4"
        '
        'label11
        '
        Me.label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.label11.Location = New System.Drawing.Point(234, 3)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(1, 23)
        Me.label11.TabIndex = 40
        Me.label11.Text = "label4"
        '
        'pnlBorder_Right
        '
        Me.pnlBorder_Right.BackColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.pnlBorder_Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBorder_Right.Location = New System.Drawing.Point(1027, 0)
        Me.pnlBorder_Right.Name = "pnlBorder_Right"
        Me.pnlBorder_Right.Size = New System.Drawing.Size(1, 597)
        Me.pnlBorder_Right.TabIndex = 3
        Me.pnlBorder_Right.Visible = False
        '
        'pnlBorder_Bottom
        '
        Me.pnlBorder_Bottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.pnlBorder_Bottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBorder_Bottom.Location = New System.Drawing.Point(0, 597)
        Me.pnlBorder_Bottom.Name = "pnlBorder_Bottom"
        Me.pnlBorder_Bottom.Size = New System.Drawing.Size(1028, 1)
        Me.pnlBorder_Bottom.TabIndex = 1
        Me.pnlBorder_Bottom.Visible = False
        '
        'ts_LabMain
        '
        Me.ts_LabMain.AddSeparatorsBetweenEachButton = False
        Me.ts_LabMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_LabMain.BackgroundImage = CType(resources.GetObject("ts_LabMain.BackgroundImage"), System.Drawing.Image)
        Me.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_LabMain.ButtonsToHide = CType(resources.GetObject("ts_LabMain.ButtonsToHide"), System.Collections.ArrayList)
        Me.ts_LabMain.ConnectionString = Nothing
        Me.ts_LabMain.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.ts_LabMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_LabMain.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_LabMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_New, Me.tlbbtn_Save, Me.tlbbtn_Finish, Me.tlbbtn_Print, Me.tlbbtn_Fax, Me.tlbbtn_Previous, Me.tlbbtn_HL7, Me.tlbbtn_Acknowledgment, Me.tlbbtn_VWAcknowledgment, Me.tlbbtn_PrvLabs, Me.tlbbtn_Close})
        Me.ts_LabMain.Location = New System.Drawing.Point(0, 0)
        Me.ts_LabMain.ModuleName = Nothing
        Me.ts_LabMain.Name = "ts_LabMain"
        Me.ts_LabMain.Size = New System.Drawing.Size(1028, 53)
        Me.ts_LabMain.TabIndex = 0
        Me.ts_LabMain.UserID = CType(0, Long)
        '
        'tlbbtn_New
        '
        Me.tlbbtn_New.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_New.Image = CType(resources.GetObject("tlbbtn_New.Image"), System.Drawing.Image)
        Me.tlbbtn_New.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_New.Name = "tlbbtn_New"
        Me.tlbbtn_New.Size = New System.Drawing.Size(37, 50)
        Me.tlbbtn_New.Tag = "New"
        Me.tlbbtn_New.Text = "&New"
        Me.tlbbtn_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_New.ToolTipText = "New"
        '
        'tlbbtn_Save
        '
        Me.tlbbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Save.Image = CType(resources.GetObject("tlbbtn_Save.Image"), System.Drawing.Image)
        Me.tlbbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Save.Name = "tlbbtn_Save"
        Me.tlbbtn_Save.Size = New System.Drawing.Size(66, 50)
        Me.tlbbtn_Save.Tag = "Save and Close"
        Me.tlbbtn_Save.Text = "&Save&&Cls"
        Me.tlbbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Save.ToolTipText = "Save and Close"
        '
        'tlbbtn_Finish
        '
        Me.tlbbtn_Finish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Finish.Image = CType(resources.GetObject("tlbbtn_Finish.Image"), System.Drawing.Image)
        Me.tlbbtn_Finish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Finish.Name = "tlbbtn_Finish"
        Me.tlbbtn_Finish.Size = New System.Drawing.Size(45, 50)
        Me.tlbbtn_Finish.Tag = "Finish"
        Me.tlbbtn_Finish.Text = "&Finish"
        Me.tlbbtn_Finish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Finish.ToolTipText = "Finish"
        '
        'tlbbtn_Print
        '
        Me.tlbbtn_Print.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Print.Image = CType(resources.GetObject("tlbbtn_Print.Image"), System.Drawing.Image)
        Me.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Print.Name = "tlbbtn_Print"
        Me.tlbbtn_Print.Size = New System.Drawing.Size(41, 50)
        Me.tlbbtn_Print.Tag = "&Print"
        Me.tlbbtn_Print.Text = "&Print"
        Me.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Print.ToolTipText = "Print"
        '
        'tlbbtn_Fax
        '
        Me.tlbbtn_Fax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Fax.Image = CType(resources.GetObject("tlbbtn_Fax.Image"), System.Drawing.Image)
        Me.tlbbtn_Fax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Fax.Name = "tlbbtn_Fax"
        Me.tlbbtn_Fax.Size = New System.Drawing.Size(36, 50)
        Me.tlbbtn_Fax.Tag = "Fax  "
        Me.tlbbtn_Fax.Text = "F&ax"
        Me.tlbbtn_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Fax.ToolTipText = "Fax  "
        '
        'tlbbtn_Previous
        '
        Me.tlbbtn_Previous.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Previous.Image = CType(resources.GetObject("tlbbtn_Previous.Image"), System.Drawing.Image)
        Me.tlbbtn_Previous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Previous.Name = "tlbbtn_Previous"
        Me.tlbbtn_Previous.Size = New System.Drawing.Size(50, 50)
        Me.tlbbtn_Previous.Tag = "Show "
        Me.tlbbtn_Previous.Text = "&Show "
        Me.tlbbtn_Previous.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Previous.ToolTipText = "Show "
        '
        'tlbbtn_HL7
        '
        Me.tlbbtn_HL7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_HL7.Image = CType(resources.GetObject("tlbbtn_HL7.Image"), System.Drawing.Image)
        Me.tlbbtn_HL7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_HL7.Name = "tlbbtn_HL7"
        Me.tlbbtn_HL7.Size = New System.Drawing.Size(36, 50)
        Me.tlbbtn_HL7.Tag = "HL7"
        Me.tlbbtn_HL7.Text = "&HL7"
        Me.tlbbtn_HL7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_HL7.ToolTipText = "HL7"
        '
        'tlbbtn_Acknowledgment
        '
        Me.tlbbtn_Acknowledgment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Acknowledgment.Image = CType(resources.GetObject("tlbbtn_Acknowledgment.Image"), System.Drawing.Image)
        Me.tlbbtn_Acknowledgment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Acknowledgment.Name = "tlbbtn_Acknowledgment"
        Me.tlbbtn_Acknowledgment.Size = New System.Drawing.Size(44, 50)
        Me.tlbbtn_Acknowledgment.Tag = "Acknowledgment"
        Me.tlbbtn_Acknowledgment.Text = "&Ackw"
        Me.tlbbtn_Acknowledgment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Acknowledgment.ToolTipText = "Acknowledgment"
        '
        'tlbbtn_VWAcknowledgment
        '
        Me.tlbbtn_VWAcknowledgment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_VWAcknowledgment.Image = CType(resources.GetObject("tlbbtn_VWAcknowledgment.Image"), System.Drawing.Image)
        Me.tlbbtn_VWAcknowledgment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_VWAcknowledgment.Name = "tlbbtn_VWAcknowledgment"
        Me.tlbbtn_VWAcknowledgment.Size = New System.Drawing.Size(77, 50)
        Me.tlbbtn_VWAcknowledgment.Tag = "View Acknowledgment"
        Me.tlbbtn_VWAcknowledgment.Text = "&View Ackw"
        Me.tlbbtn_VWAcknowledgment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_VWAcknowledgment.ToolTipText = "View Acknowledgment"
        '
        'tlbbtn_PrvLabs
        '
        Me.tlbbtn_PrvLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_PrvLabs.Image = CType(resources.GetObject("tlbbtn_PrvLabs.Image"), System.Drawing.Image)
        Me.tlbbtn_PrvLabs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_PrvLabs.Name = "tlbbtn_PrvLabs"
        Me.tlbbtn_PrvLabs.Size = New System.Drawing.Size(59, 50)
        Me.tlbbtn_PrvLabs.Tag = "Previous Labs"
        Me.tlbbtn_PrvLabs.Text = "&PrvLabs"
        Me.tlbbtn_PrvLabs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_PrvLabs.ToolTipText = "Previous Labs"
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Close.ToolTipText = "Close"
        '
        'tlbbtnMicrophone
        '
        Me.tlbbtnMicrophone.ImageIndex = 20
        Me.tlbbtnMicrophone.Name = "tlbbtnMicrophone"
        Me.tlbbtnMicrophone.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tlbbtnMicrophone.Tag = "Microphone On/Off"
        Me.tlbbtnMicrophone.ToolTipText = "Microphone On/Off"
        Me.tlbbtnMicrophone.Visible = False
        '
        'tlbbtnSep1
        '
        Me.tlbbtnSep1.Name = "tlbbtnSep1"
        Me.tlbbtnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        Me.tlbbtnSep1.Visible = False
        '
        'tlbbtnMasters
        '
        Me.tlbbtnMasters.ImageIndex = 0
        Me.tlbbtnMasters.Name = "tlbbtnMasters"
        Me.tlbbtnMasters.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton
        Me.tlbbtnMasters.Tag = "Masters"
        Me.tlbbtnMasters.ToolTipText = "Masters"
        '
        'tlbbtnSep2
        '
        Me.tlbbtnSep2.Name = "tlbbtnSep2"
        Me.tlbbtnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnNewPatient
        '
        Me.tlbbtnNewPatient.ImageIndex = 1
        Me.tlbbtnNewPatient.Name = "tlbbtnNewPatient"
        Me.tlbbtnNewPatient.Tag = "NewPatient"
        Me.tlbbtnNewPatient.ToolTipText = "New Patient"
        '
        'tlbbtnModifyPatient
        '
        Me.tlbbtnModifyPatient.ImageIndex = 2
        Me.tlbbtnModifyPatient.Name = "tlbbtnModifyPatient"
        Me.tlbbtnModifyPatient.Tag = "ModifyPatient"
        Me.tlbbtnModifyPatient.ToolTipText = "Modify Patient"
        '
        'tlbbtnSep3
        '
        Me.tlbbtnSep3.Name = "tlbbtnSep3"
        Me.tlbbtnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnHistory
        '
        Me.tlbbtnHistory.ImageIndex = 3
        Me.tlbbtnHistory.Name = "tlbbtnHistory"
        Me.tlbbtnHistory.Tag = "History"
        Me.tlbbtnHistory.ToolTipText = "Patient History"
        '
        'tlbbtnPrescription
        '
        Me.tlbbtnPrescription.ImageIndex = 4
        Me.tlbbtnPrescription.Name = "tlbbtnPrescription"
        Me.tlbbtnPrescription.Tag = "Prescription"
        Me.tlbbtnPrescription.ToolTipText = "Prescription"
        '
        'tlbbtnMedication
        '
        Me.tlbbtnMedication.ImageIndex = 5
        Me.tlbbtnMedication.Name = "tlbbtnMedication"
        Me.tlbbtnMedication.Tag = "Medication"
        Me.tlbbtnMedication.ToolTipText = "Medication"
        '
        'tlbbtnOrders
        '
        Me.tlbbtnOrders.ImageIndex = 6
        Me.tlbbtnOrders.Name = "tlbbtnOrders"
        Me.tlbbtnOrders.Tag = "Orders"
        Me.tlbbtnOrders.ToolTipText = "View Orders"
        '
        'tlbbtnSep4
        '
        Me.tlbbtnSep4.Name = "tlbbtnSep4"
        Me.tlbbtnSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnNewExam
        '
        Me.tlbbtnNewExam.ImageIndex = 8
        Me.tlbbtnNewExam.Name = "tlbbtnNewExam"
        Me.tlbbtnNewExam.Tag = "NewExam"
        Me.tlbbtnNewExam.ToolTipText = "New Exam"
        '
        'tlbbtnPastExam
        '
        Me.tlbbtnPastExam.ImageIndex = 7
        Me.tlbbtnPastExam.Name = "tlbbtnPastExam"
        Me.tlbbtnPastExam.Tag = "PastExams"
        Me.tlbbtnPastExam.ToolTipText = "Past Exams"
        '
        'tlbbtnUnFinishedExams
        '
        Me.tlbbtnUnFinishedExams.ImageIndex = 9
        Me.tlbbtnUnFinishedExams.Name = "tlbbtnUnFinishedExams"
        Me.tlbbtnUnFinishedExams.Tag = "UnFinishedExams"
        Me.tlbbtnUnFinishedExams.ToolTipText = "UnFinished Exams"
        '
        'tlbbtnSep5
        '
        Me.tlbbtnSep5.Name = "tlbbtnSep5"
        Me.tlbbtnSep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnCalendar
        '
        Me.tlbbtnCalendar.ImageIndex = 10
        Me.tlbbtnCalendar.Name = "tlbbtnCalendar"
        Me.tlbbtnCalendar.Tag = "Calendar"
        Me.tlbbtnCalendar.ToolTipText = "Appointments"
        '
        'tlbbtnTasks
        '
        Me.tlbbtnTasks.ImageIndex = 11
        Me.tlbbtnTasks.Name = "tlbbtnTasks"
        Me.tlbbtnTasks.Tag = "Tasks"
        Me.tlbbtnTasks.ToolTipText = "Tasks"
        '
        'tlbbtnMessages
        '
        Me.tlbbtnMessages.ImageIndex = 12
        Me.tlbbtnMessages.Name = "tlbbtnMessages"
        Me.tlbbtnMessages.Tag = "Messages"
        Me.tlbbtnMessages.ToolTipText = "Messages"
        '
        'tlbbtnSep10
        '
        Me.tlbbtnSep10.Name = "tlbbtnSep10"
        Me.tlbbtnSep10.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnScanDocs
        '
        Me.tlbbtnScanDocs.ImageIndex = 14
        Me.tlbbtnScanDocs.Name = "tlbbtnScanDocs"
        Me.tlbbtnScanDocs.Tag = "ScanDocs"
        Me.tlbbtnScanDocs.ToolTipText = "Scan Documents"
        '
        'tlbbtnDocMGMT
        '
        Me.tlbbtnDocMGMT.ImageIndex = 13
        Me.tlbbtnDocMGMT.Name = "tlbbtnDocMGMT"
        Me.tlbbtnDocMGMT.Tag = "DOCMGMT"
        Me.tlbbtnDocMGMT.ToolTipText = "View Documents"
        '
        'tlbbtnSep7
        '
        Me.tlbbtnSep7.Name = "tlbbtnSep7"
        Me.tlbbtnSep7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnSettings
        '
        Me.tlbbtnSettings.ImageIndex = 15
        Me.tlbbtnSettings.Name = "tlbbtnSettings"
        Me.tlbbtnSettings.Tag = "Settings"
        Me.tlbbtnSettings.ToolTipText = "Settings"
        '
        'tlbbtnSep8
        '
        Me.tlbbtnSep8.Name = "tlbbtnSep8"
        Me.tlbbtnSep8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnFormGallery
        '
        Me.tlbbtnFormGallery.ImageIndex = 16
        Me.tlbbtnFormGallery.Name = "tlbbtnFormGallery"
        Me.tlbbtnFormGallery.Tag = "FormGallery"
        Me.tlbbtnFormGallery.ToolTipText = "Form Gallery"
        '
        'tlbbtnLockScreen
        '
        Me.tlbbtnLockScreen.ImageIndex = 18
        Me.tlbbtnLockScreen.Name = "tlbbtnLockScreen"
        Me.tlbbtnLockScreen.Tag = "LockScreen"
        Me.tlbbtnLockScreen.ToolTipText = "Lock Screen"
        Me.tlbbtnLockScreen.Visible = False
        '
        'tlbbtnSep9
        '
        Me.tlbbtnSep9.Name = "tlbbtnSep9"
        Me.tlbbtnSep9.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.ImageIndex = 17
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Tag = "Close"
        Me.tlbbtnClose.ToolTipText = "Close Application"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.gloUCLab_Transaction)
        Me.pnlMain.Controls.Add(Me.Splitter3)
        Me.pnlMain.Controls.Add(Me.pnlTransactionHistory)
        Me.pnlMain.Controls.Add(Me.spltTransactionHistory)
        Me.pnlMain.Controls.Add(Me.gloUCLab_TestDetail)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.gloUCLab_OrderDetail)
        Me.pnlMain.Controls.Add(Me.splRight)
        Me.pnlMain.Controls.Add(Me.pnlPrevious)
        Me.pnlMain.Controls.Add(Me.splLeft)
        Me.pnlMain.Controls.Add(Me.pnlLeft)
        Me.pnlMain.Controls.Add(Me.pnlBorder_Right)
        Me.pnlMain.Controls.Add(Me.pnlBorder_Bottom)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 57)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1028, 598)
        Me.pnlMain.TabIndex = 5
        '
        'gloUCLab_Transaction
        '
        Me.gloUCLab_Transaction.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.gloUCLab_Transaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloUCLab_Transaction.dtSelectedFromDt = New Date(CType(0, Long))
        Me.gloUCLab_Transaction.dtSelectedToDt = New Date(CType(0, Long))
        Me.gloUCLab_Transaction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gloUCLab_Transaction.ForeColor = System.Drawing.Color.Black
        Me.gloUCLab_Transaction.LabResultId = CType(0, Long)
        Me.gloUCLab_Transaction.LabResultName = Nothing
        Me.gloUCLab_Transaction.LabTestId = CType(0, Long)
        Me.gloUCLab_Transaction.LabTestName = Nothing
        Me.gloUCLab_Transaction.Location = New System.Drawing.Point(239, 129)
        Me.gloUCLab_Transaction.Name = "gloUCLab_Transaction"
        Me.gloUCLab_Transaction.PatientID = CType(0, Long)
        Me.gloUCLab_Transaction.Size = New System.Drawing.Size(543, 144)
        Me.gloUCLab_Transaction.TabIndex = 30
        Me.gloUCLab_Transaction.TransactionType = gloUserControlLibrary.gloUC_Transaction.enumTransactionType.None
        '
        'gloUCLab_TestDetail
        '
        Me.gloUCLab_TestDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.gloUCLab_TestDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gloUCLab_TestDetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gloUCLab_TestDetail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gloUCLab_TestDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gloUCLab_TestDetail.Location = New System.Drawing.Point(239, 514)
        Me.gloUCLab_TestDetail.Name = "gloUCLab_TestDetail"
        Me.gloUCLab_TestDetail.Size = New System.Drawing.Size(543, 84)
        Me.gloUCLab_TestDetail.TabIndex = 19
        '
        'gloUCLab_OrderDetail
        '
        Me.gloUCLab_OrderDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.gloUCLab_OrderDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gloUCLab_OrderDetail.ClinicID = CType(0, Long)
        Me.gloUCLab_OrderDetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.gloUCLab_OrderDetail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gloUCLab_OrderDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gloUCLab_OrderDetail.Location = New System.Drawing.Point(239, 0)
        Me.gloUCLab_OrderDetail.Name = "gloUCLab_OrderDetail"
        Me.gloUCLab_OrderDetail.OrderNumberID = CType(0, Short)
        Me.gloUCLab_OrderDetail.OrderNumberPrefix = Nothing
        Me.gloUCLab_OrderDetail.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.gloUCLab_OrderDetail.PreferredLab = Nothing
        Me.gloUCLab_OrderDetail.PreferredLabID = CType(0, Long)
        Me.gloUCLab_OrderDetail.ReferredBy = Nothing
        Me.gloUCLab_OrderDetail.ReferredByFName = ""
        Me.gloUCLab_OrderDetail.ReferredByID = CType(0, Long)
        Me.gloUCLab_OrderDetail.ReferredByLName = ""
        Me.gloUCLab_OrderDetail.ReferredByMName = ""
        Me.gloUCLab_OrderDetail.SampledBy = Nothing
        Me.gloUCLab_OrderDetail.SampledByID = CType(0, Long)
        Me.gloUCLab_OrderDetail.Size = New System.Drawing.Size(543, 127)
        Me.gloUCLab_OrderDetail.TabIndex = 17
        Me.gloUCLab_OrderDetail.TaskDescription = Nothing
        Me.gloUCLab_OrderDetail.TaskDueDate = New Date(CType(0, Long))
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.ts_LabMain)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1028, 57)
        Me.pnlToolStrip.TabIndex = 0
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'frmLab_RequestOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1028, 655)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLab_RequestOrder"
        Me.ShowInTaskbar = False
        Me.Text = "Lab Order"
        Me.pnlTransactionHistory.ResumeLayout(False)
        Me.pnlPrevious.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlList_Detail.ResumeLayout(False)
        Me.pnltrvList.ResumeLayout(False)
        Me.pnl_btnTests.ResumeLayout(False)
        Me.pnl_btnGroups.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ts_LabMain.ResumeLayout(False)
        Me.ts_LabMain.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents ts_LabMain As System.Windows.Forms.ToolStrip
    Friend WithEvents ts_LabMain As gloToolStrip.gloToolStrip
    Friend WithEvents tlbbtnMicrophone As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnMasters As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnNewPatient As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnModifyPatient As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnHistory As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnPrescription As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnMedication As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnOrders As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnNewExam As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnPastExam As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnUnFinishedExams As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnCalendar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnTasks As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnMessages As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep10 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnScanDocs As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnDocMGMT As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep7 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSettings As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep8 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnFormGallery As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnLockScreen As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep9 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtn_New As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Print As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Fax As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Previous As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlBorder_Right As System.Windows.Forms.Panel
    Friend WithEvents pnlBorder_Bottom As System.Windows.Forms.Panel
    Friend WithEvents splLeft As System.Windows.Forms.Splitter
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents splRight As System.Windows.Forms.Splitter
    Friend WithEvents txtListSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlList_Detail As System.Windows.Forms.Panel
    Friend WithEvents btnTests As System.Windows.Forms.Button
    Friend WithEvents btnGroups As System.Windows.Forms.Button
    Friend WithEvents trvList As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents gloUCLab_OrderDetail As gloUserControlLibrary.gloUC_LabOrderDetail
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlPrevious As System.Windows.Forms.Panel
    Friend WithEvents gloUCLab_History As gloUserControlLibrary.gloUC_LabHistory
    Friend WithEvents gloUCLab_TestDetail As gloUserControlLibrary.gloUC_LabTest
    Friend WithEvents tlbbtn_HL7 As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtn_Acknowledgment As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtn_VWAcknowledgment As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Finish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_PrvLabs As System.Windows.Forms.ToolStripButton
    Friend WithEvents spltTransactionHistory As System.Windows.Forms.Splitter
    Friend WithEvents gloUCLab_Transaction As gloUserControlLibrary.gloUC_Transaction
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents pnlTransactionHistory As System.Windows.Forms.Panel
    Friend WithEvents GloUC_TransactionHistory As gloUserControlLibrary.gloUC_TransactionHistory
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents pnl_btnTests As System.Windows.Forms.Panel
    Private WithEvents pnl_btnGroups As System.Windows.Forms.Panel
    Private WithEvents pnltrvList As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GloUC_trvTest As gloUserControlLibrary.gloUC_TreeView
End Class
