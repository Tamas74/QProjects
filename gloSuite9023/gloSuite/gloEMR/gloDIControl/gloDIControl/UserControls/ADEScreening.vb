'Imports Medispan
'Imports Medispan.DIB
'Imports Medispan.Collections
'Imports Medispan.Configuration
'Imports Medispan.IREF.Business
'Imports Medispan.IREF.Render
'Imports Medispan.IREF.Container
Imports gloDIControl.DrugInteractionCollection
Imports System.Windows.Forms
Imports gloCentralizedDIB

Public Class ADEScreening
    Inherits System.Windows.Forms.UserControl

    Private m_ADEScreenType As Int16
    Public Event OkClk(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event btnClk(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal inttype As Int16)
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private m_connectionstring As String
    Private _AdEscreening As gloADEScreen
    'Private objADELookUpResults As ADELookupResults
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub
    Public Sub New(ByVal ConnectionString As String, ByVal ADEScreenType As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        m_connectionstring = ConnectionString
        'Add any initialization after the InitializeComponent() call
        'objgloscreening = New gloScreening(Nothing, Nothing, 1)
        _AdEscreening = New gloADEScreen(ADEScreenType)
        If btnDrugs.Dock = DockStyle.Bottom Then
            btnMedication.Dock = DockStyle.Bottom
            btnDrugs.Dock = DockStyle.Top
            Me.btnDrugs.BackgroundImage = Global.gloDIControl.My.Resources.btnover
        End If
    End Sub
    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents pnllft As System.Windows.Forms.Panel
    Friend WithEvents btnMedication As System.Windows.Forms.Button
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents trvList As System.Windows.Forms.TreeView
    Friend WithEvents btnDrugs As System.Windows.Forms.Button
    Friend WithEvents pnlADEDetails As System.Windows.Forms.Panel
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents pnltop As System.Windows.Forms.Panel
    Friend WithEvents pnltitle As System.Windows.Forms.Panel
    Friend WithEvents trvDescList As System.Windows.Forms.TreeView
    Friend WithEvents pnldescription As System.Windows.Forms.Panel
    Friend WithEvents txtdesc As System.Windows.Forms.RichTextBox
    Friend WithEvents lbltitle As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ADEScreening))
        Me.pnllft = New System.Windows.Forms.Panel()
        Me.trvList = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnDrugs = New System.Windows.Forms.Button()
        Me.btnMedication = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlADEDetails = New System.Windows.Forms.Panel()
        Me.pnldescription = New System.Windows.Forms.Panel()
        Me.txtdesc = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.pnltop = New System.Windows.Forms.Panel()
        Me.trvDescList = New System.Windows.Forms.TreeView()
        Me.pnltitle = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lbltitle = New System.Windows.Forms.Label()
        Me.pnllft.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlADEDetails.SuspendLayout()
        Me.pnldescription.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnltop.SuspendLayout()
        Me.pnltitle.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnllft
        '
        Me.pnllft.Controls.Add(Me.trvList)
        Me.pnllft.Controls.Add(Me.btnDrugs)
        Me.pnllft.Controls.Add(Me.btnMedication)
        Me.pnllft.Controls.Add(Me.Panel1)
        Me.pnllft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnllft.Location = New System.Drawing.Point(0, 0)
        Me.pnllft.Name = "pnllft"
        Me.pnllft.Size = New System.Drawing.Size(209, 486)
        Me.pnllft.TabIndex = 0
        '
        'trvList
        '
        Me.trvList.BackColor = System.Drawing.Color.GhostWhite
        Me.trvList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.trvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvList.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvList.HideSelection = False
        Me.trvList.ImageIndex = 0
        Me.trvList.ImageList = Me.ImageList1
        Me.trvList.ItemHeight = 18
        Me.trvList.Location = New System.Drawing.Point(0, 19)
        Me.trvList.Name = "trvList"
        Me.trvList.SelectedImageIndex = 0
        Me.trvList.ShowLines = False
        Me.trvList.Size = New System.Drawing.Size(209, 416)
        Me.trvList.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        '
        'btnDrugs
        '
        Me.btnDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDrugs.BackgroundImage = Global.gloDIControl.My.Resources.Resources.bluebtn
        Me.btnDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugs.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrugs.ForeColor = System.Drawing.Color.Black
        Me.btnDrugs.Location = New System.Drawing.Point(0, 435)
        Me.btnDrugs.Name = "btnDrugs"
        Me.btnDrugs.Size = New System.Drawing.Size(209, 26)
        Me.btnDrugs.TabIndex = 8
        Me.btnDrugs.Text = "Drugs"
        Me.btnDrugs.UseVisualStyleBackColor = False
        '
        'btnMedication
        '
        Me.btnMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnMedication.BackgroundImage = CType(resources.GetObject("btnMedication.BackgroundImage"), System.Drawing.Image)
        Me.btnMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMedication.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMedication.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMedication.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMedication.ForeColor = System.Drawing.Color.Black
        Me.btnMedication.Location = New System.Drawing.Point(0, 461)
        Me.btnMedication.Name = "btnMedication"
        Me.btnMedication.Size = New System.Drawing.Size(209, 25)
        Me.btnMedication.TabIndex = 1
        Me.btnMedication.Text = "Medical Condition"
        Me.btnMedication.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtSearch)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(209, 19)
        Me.Panel1.TabIndex = 7
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(0, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(209, 22)
        Me.txtSearch.TabIndex = 7
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.Navy
        Me.Splitter1.Location = New System.Drawing.Point(209, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 486)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.Navy
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(212, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(370, 1)
        Me.Splitter2.TabIndex = 4
        Me.Splitter2.TabStop = False
        '
        'pnlADEDetails
        '
        Me.pnlADEDetails.Controls.Add(Me.pnldescription)
        Me.pnlADEDetails.Controls.Add(Me.pnltop)
        Me.pnlADEDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlADEDetails.Location = New System.Drawing.Point(212, 1)
        Me.pnlADEDetails.Name = "pnlADEDetails"
        Me.pnlADEDetails.Size = New System.Drawing.Size(370, 485)
        Me.pnlADEDetails.TabIndex = 7
        '
        'pnldescription
        '
        Me.pnldescription.Controls.Add(Me.txtdesc)
        Me.pnldescription.Controls.Add(Me.Panel2)
        Me.pnldescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnldescription.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnldescription.Location = New System.Drawing.Point(0, 232)
        Me.pnldescription.Name = "pnldescription"
        Me.pnldescription.Size = New System.Drawing.Size(370, 253)
        Me.pnldescription.TabIndex = 8
        '
        'txtdesc
        '
        Me.txtdesc.BackColor = System.Drawing.Color.GhostWhite
        Me.txtdesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtdesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtdesc.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.Location = New System.Drawing.Point(0, 22)
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(370, 231)
        Me.txtdesc.TabIndex = 7
        Me.txtdesc.Text = ""
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblDesc)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(370, 22)
        Me.Panel2.TabIndex = 8
        '
        'lblDesc
        '
        Me.lblDesc.BackColor = System.Drawing.Color.Transparent
        Me.lblDesc.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblDesc.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.Location = New System.Drawing.Point(0, 0)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(123, 20)
        Me.lblDesc.TabIndex = 6
        Me.lblDesc.Text = " Description"
        Me.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnltop
        '
        Me.pnltop.Controls.Add(Me.trvDescList)
        Me.pnltop.Controls.Add(Me.pnltitle)
        Me.pnltop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltop.Location = New System.Drawing.Point(0, 0)
        Me.pnltop.Name = "pnltop"
        Me.pnltop.Size = New System.Drawing.Size(370, 232)
        Me.pnltop.TabIndex = 7
        '
        'trvDescList
        '
        Me.trvDescList.BackColor = System.Drawing.Color.GhostWhite
        Me.trvDescList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.trvDescList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvDescList.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvDescList.HideSelection = False
        Me.trvDescList.Location = New System.Drawing.Point(0, 24)
        Me.trvDescList.Name = "trvDescList"
        Me.trvDescList.Size = New System.Drawing.Size(370, 208)
        Me.trvDescList.TabIndex = 1
        '
        'pnltitle
        '
        Me.pnltitle.BackgroundImage = CType(resources.GetObject("pnltitle.BackgroundImage"), System.Drawing.Image)
        Me.pnltitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltitle.Controls.Add(Me.btnOK)
        Me.pnltitle.Controls.Add(Me.lbltitle)
        Me.pnltitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltitle.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltitle.Location = New System.Drawing.Point(0, 0)
        Me.pnltitle.Name = "pnltitle"
        Me.pnltitle.Size = New System.Drawing.Size(370, 24)
        Me.pnltitle.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.BackgroundImage = CType(resources.GetObject("btnOK.BackgroundImage"), System.Drawing.Image)
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(282, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 24)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        '
        'lbltitle
        '
        Me.lbltitle.BackColor = System.Drawing.Color.Transparent
        Me.lbltitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbltitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lbltitle.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltitle.Location = New System.Drawing.Point(0, 0)
        Me.lbltitle.Name = "lbltitle"
        Me.lbltitle.Size = New System.Drawing.Size(109, 24)
        Me.lbltitle.TabIndex = 8
        Me.lbltitle.Text = " Description"
        Me.lbltitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ADEScreening
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.pnlADEDetails)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnllft)
        Me.Name = "ADEScreening"
        Me.Size = New System.Drawing.Size(582, 486)
        Me.pnllft.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlADEDetails.ResumeLayout(False)
        Me.pnldescription.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnltop.ResumeLayout(False)
        Me.pnltitle.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Property search() As String
        Get
            Return txtSearch.Text
        End Get
        Set(ByVal Value As String)
            txtSearch.Text = Value
        End Set
    End Property
    Public Property description() As String
        Get
            Return txtdesc.Text
        End Get
        Set(ByVal Value As String)
            txtdesc.Text = Value

        End Set
    End Property
    Public WriteOnly Property Header() As String
        Set(ByVal Value As String)
            lbltitle.Text = Value
        End Set
    End Property
    Public WriteOnly Property subHeader() As String
        Set(ByVal Value As String)
            lbltitle.Text = Value
        End Set
    End Property
    Private Sub btn_clk(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMedication.Click, btnDrugs.Click
        Try
            trvList.Nodes.Clear()
            trvDescList.Nodes.Clear()
            txtdesc.Text = ""

            Select Case CType(sender, Button).Name
                Case "btnMedication"
                    m_ADEScreenType = 2

                    If btnMedication.Dock = DockStyle.Bottom Then
                        btnMedication.Dock = DockStyle.Top
                        btnDrugs.Dock = DockStyle.Bottom
                    End If
                    Me.btnMedication.BackgroundImage = Global.gloDIControl.My.Resources.btnover
                    Me.btnDrugs.BackgroundImage = Global.gloDIControl.My.Resources.bluebtn
                    'objgloscreening.PopulatePatientProfile()
                    FillMedicationForScreening()
                    RaiseEvent btnClk(sender, e, 2)
                Case "btnDrugs"
                    m_ADEScreenType = 1
                    If btnDrugs.Dock = DockStyle.Bottom Then
                        btnMedication.Dock = DockStyle.Bottom
                        btnDrugs.Dock = DockStyle.Top
                    End If
                    Me.btnMedication.BackgroundImage = Global.gloDIControl.My.Resources.bluebtn
                    Me.btnDrugs.BackgroundImage = Global.gloDIControl.My.Resources.btnover
                    FillDrugsforADEScreening()
                    RaiseEvent btnClk(sender, e, 1)
            End Select
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As DrugInteractionControlException
            Throw ex
        Catch ex As Exception
            Dim objex As New DrugInteractionControlException
            objex.ErrMessage = ex.Message
            Throw objex
        End Try

    End Sub

    Private Sub trvList_DoubleClickService(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MAX_RESULTS As Int16 = 20
        Dim results As GroupedADELookupResult
        Dim i As Int32 = 0
        Try
            If Not IsNothing(trvList.SelectedNode) Then                
                trvDescList.Nodes.Clear()
                txtdesc.Text = ""

                Dim mynode As New TreeNode
                mynode.Tag = trvList.SelectedNode.Tag
                mynode.Text = trvList.SelectedNode.Text
                trvDescList.Nodes.Add(mynode)
                If btnDrugs.Dock = DockStyle.Top Then                    
                    results = _AdEscreening.GetScreeningForDrugsService(trvList.SelectedNode.Tag)
                    If (IsNothing(results) = False) Then
                        If results.Results.Count > 0 Then
                            Dim objnode As TreeNode
                            For Each element As ADELookupResultProperties In results.Results
                                objnode = New TreeNode
                                objnode.Text = element.MedCondNameProf()
                                objnode.Tag = element.MedCondId
                                trvDescList.Nodes.Item(0).Nodes.Add(objnode)
                                objnode = Nothing
                            Next

                            trvDescList.ExpandAll()
                           
                        End If

                    End If

                ElseIf btnMedication.Dock = DockStyle.Top Then
                    results = _AdEscreening.GetAdverseEffectsForMedicalConditionService(trvList.SelectedNode.Tag)
                    If (IsNothing(results) = False) Then
                        If results.Results.Count > 0 Then
                            Dim objnode As TreeNode

                            For Each element As ADELookupResultProperties In results.Results
                                objnode = New TreeNode
                                objnode.Text = element.DispensableDrugDescription()
                                objnode.Tag = element.DispensableDrugId
                                trvDescList.Nodes.Item(0).Nodes.Add(objnode)
                                objnode = Nothing
                            Next
                            trvDescList.ExpandAll()                            
                        End If
                    End If
                End If

            End If
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New DrugInteractionControlException
            objex.ErrMessage = ex.Message
            Throw objex
        Finally

        End Try
    End Sub

    Private Sub trvList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvList.DoubleClick
        'If gloCentralizedDIB.InitialiseServiceVars.gblnUseDIBService = True Then
        '    Me.trvList_DoubleClickService(sender, e)
        'Else
        '    Dim MAX_RESULTS As Int16 = 20
        '    Dim results As ADELookupResults
        '    Dim i As Int32 = 0
        '    Try
        '        If Not IsNothing(trvList.SelectedNode) Then
        '            'If trvDescList.GetNodeCount(False) <= 0 Then
        '            'Clear the present medication/drug item
        '            trvDescList.Nodes.Clear()
        '            txtdesc.Text = ""

        '            Dim mynode As New TreeNode
        '            mynode.Tag = trvList.SelectedNode.Tag
        '            mynode.Text = trvList.SelectedNode.Text
        '            trvDescList.Nodes.Add(mynode)

        '            'Check if screening to be done for Drug
        '            If btnDrugs.Dock = DockStyle.Top Then
        '                'Get the Adverse drug effects for a particular drug
        '                results = _AdEscreening.GetScreeningForDrugs(trvList.SelectedNode.Tag)
        '                If (IsNothing(results) = False) Then


        '                    If results.Count > 0 Then
        '                        Dim objnode As TreeNode
        '                        For i = 0 To MAX_RESULTS - 1 And results.Count - 1
        '                            Dim result As ADELookupResult = results(i)
        '                            ' Get the Type code for the ADELookupResult
        '                            ' object.
        '                            ' Create an output string for the current
        '                            ' result.

        '                            ' Create an output string for the current
        '                            ' result.
        '                            objnode = New TreeNode
        '                            objnode.Text = result.MedCondNameProf()
        '                            objnode.Tag = result.MedCondId
        '                            trvDescList.Nodes.Item(0).Nodes.Add(objnode)
        '                            objnode = Nothing
        '                        Next
        '                        trvDescList.ExpandAll()
        '                        'Get a complete description of a drug effect
        '                        txtdesc.Text = _AdEscreening.GetScreeningDescFordrugs(results)
        '                    End If

        '                End If
        '                ' Dim col As Collection = objgloscreening.GetScreeningforDrugs(trvList.SelectedNode.Tag)
        '                'If Not IsNothing(col) Then
        '                '    If col.Count >= 1 Then
        '                '        Dim objnode As TreeNode
        '                '        Dim i As Int16
        '                '        For i = 1 To col.Count
        '                '            objnode = New TreeNode
        '                '            objnode.Tag = CType(col.Item(i), gloInteraction).ID
        '                '            objnode.Text = CType(col.Item(i), gloInteraction).Name
        '                '            trvDescList.Nodes.Item(0).Nodes.Add(objnode)
        '                '            objnode = Nothing
        '                '        Next
        '                '        trvDescList.ExpandAll()
        '                '        'Get a complete description of a drug effect
        '                '        txtdesc.Text = _AdEscreening.GetScreeningDescFordrugs(objADELookUpResults)
        '                '    End If
        '                'End If
        '                'Check if screening to be dome for medical condition
        '            ElseIf btnMedication.Dock = DockStyle.Top Then
        '                'Get the Adverse drug effects for a particular medical condition
        '                results = _AdEscreening.GetAdverseEffectsForMedicalCondition(trvList.SelectedNode.Tag)
        '                If (IsNothing(results) = False) Then
        '                    If results.Count > 0 Then
        '                        Dim objnode As TreeNode
        '                        For i = 0 To MAX_RESULTS - 1 And results.Count - 1
        '                            Dim result As ADELookupResult = results(i)
        '                            ' Create an output string for the current
        '                            ' result.
        '                            objnode = New TreeNode
        '                            objnode.Text = result.DispensableDrugDescription()
        '                            objnode.Tag = result.DispensableDrugId
        '                            trvDescList.Nodes.Item(0).Nodes.Add(objnode)
        '                            objnode = Nothing
        '                        Next
        '                        trvDescList.ExpandAll()
        '                        txtdesc.Text = _AdEscreening.GetAdverseEffectsDescForMedicalConditions(results)
        '                    End If
        '                End If

        '                'Dim col As Collection = objgloscreening.GetAdverseDrugEffectForMedicalcond(trvList.SelectedNode.Tag)
        '                'If Not IsNothing(col) Then
        '                '    If col.Count >= 1 Then
        '                '        Dim objnode As TreeNode
        '                '        Dim i As Int16
        '                '        For i = 1 To col.Count
        '                '            objnode = New TreeNode
        '                '            objnode.Tag = CType(col.Item(i), gloInteraction).ID
        '                '            objnode.Text = CType(col.Item(i), gloInteraction).Name
        '                '            trvDescList.Nodes.Item(0).Nodes.Add(objnode)
        '                '            objnode = Nothing
        '                '        Next
        '                '        'Get a complete description of a medical condition
        '                '        trvDescList.ExpandAll()
        '                '        txtdesc.Text = _AdEscreening.GetAdverseEffectsDescForMedicalConditions(objADELookUpResults)
        '                '    End If
        '                'End If
        '            End If
        '            'End If
        '        End If
        '    Catch ex As gloScreeningException
        '        Throw ex
        '    Catch ex As Exception
        '        Dim objex As New DrugInteractionControlException
        '        objex.ErrMessage = ex.Message
        '        Throw objex
        '    Finally
        '        '  objADELookUpResults = Nothing
        '    End Try

        'End If



    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            If btnMedication.Dock = DockStyle.Top Then
                If Len(Trim(txtSearch.Text)) > 0 Then
                    If Len(Trim(txtSearch.Text)) <= 1 Then
                        If txtSearch.Tag <> Trim(txtSearch.Text) Then

                            Dim _GloDrugInteractionCol As New gloInteractionCollection
                            _AdEscreening.SearchMedicalConditions(Trim(txtSearch.Text), _GloDrugInteractionCol)
                            Try
                                trvList.Nodes.Clear()

                                If Not IsNothing(_GloDrugInteractionCol) Then
                                    Dim i As Int16
                                    Dim objnode As TreeNode
                                    trvList.BeginUpdate()
                                    For i = 0 To _GloDrugInteractionCol.Count - 1
                                        objnode = New TreeNode
                                        objnode.Tag = _GloDrugInteractionCol.Item(i).ID
                                        objnode.Text = _GloDrugInteractionCol.Item(i).Name
                                        trvList.Nodes.Add(objnode)
                                        objnode = Nothing
                                    Next
                                    trvList.EndUpdate()
                                End If
                            Catch ex As Exception
                                trvList.EndUpdate()
                            Finally
                                _GloDrugInteractionCol.Dispose()
                                _GloDrugInteractionCol = Nothing
                            End Try
                            txtSearch.Tag = Trim(txtSearch.Text)
                        End If
                    End If
                Else
                    txtSearch.Tag = ""
                    FillMedicationForScreening()
                End If

            ElseIf btnDrugs.Dock = DockStyle.Top Then
                If Len(Trim(txtSearch.Text)) <= 1 Then
                    If txtSearch.Tag <> Trim(txtSearch.Text) Then
                        FillDrugsforADEScreening(Trim(txtSearch.Text))
                        txtSearch.Tag = Trim(txtSearch.Text)
                    End If
                End If
            End If
            Dim mychildnode As TreeNode
            'child node collection
            For Each mychildnode In trvList.Nodes
                Dim str As String
                'str = UCase(Splittext(mychildnode.Tag))
                str = UCase(mychildnode.Text)
                If Mid(str, 1, Len(Trim(txtSearch.Text))) = UCase(Trim(txtSearch.Text)) Then
                    trvList.SelectedNode = mychildnode
                    txtSearch.Focus()
                    Exit Sub
                End If
            Next
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New DrugInteractionControlException
            objex.ErrMessage = ex.Message
            Throw objex
        End Try

        'RaiseEvent SearchChanged(sender, e)
    End Sub
    Public Sub FillDrugsforADEScreening(Optional ByVal strsearch As String = "")
        Dim conn As New SqlClient.SqlConnection(m_connectionstring)
        Dim adpt As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim cmd As New SqlClient.SqlCommand
        trvList.Nodes.Clear()
        Try
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_FillDrugs_Mst"
            cmd.Connection = conn

            Dim objParam As SqlClient.SqlParameter

            'If strsearch <> "" Then
            objParam = cmd.Parameters.Add("@drugletter", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strsearch
            'End If

            objParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 5
            adpt.SelectCommand = cmd

            adpt.Fill(dt)
            conn.Close()
            If dt.Rows.Count > 0 Then
                trvList.BeginUpdate()
                Dim i As Int16
                Dim objnode As TreeNode
                For i = 0 To dt.Rows.Count - 1
                    objnode = New TreeNode
                    objnode.Text = dt.Rows(i)(1)
                    objnode.Tag = dt.Rows(i)(5)
                    trvList.Nodes.Add(objnode)
                    objnode = Nothing
                Next
                trvList.EndUpdate()
            End If
        Catch ex As Exception
            If (IsNothing(conn) = False) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            If (IsNothing(adpt) = False) Then
                adpt.Dispose()
                adpt = Nothing
            End If

            If (IsNothing(cmd) = False) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If

            trvList.EndUpdate()
            Dim objex As New DrugInteractionControlException
            objex.ErrMessage = ex.Message
            Throw objex
        Finally
            If (IsNothing(conn) = False) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            If (IsNothing(adpt) = False) Then
                adpt.Dispose()
                adpt = Nothing
            End If

            If (IsNothing(cmd) = False) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If


        End Try
    End Sub
    Private Sub FillMedicationForScreening()
        'Try
        '    trvList.Nodes.Clear()
        '    If Not IsNothing(objgloscreening.AccessgloInteractionCol) Then
        '        Dim i As Int16
        '        Dim objnode As TreeNode
        '        trvList.BeginUpdate()
        '        For i = 1 To objgloscreening.AccessgloInteractionCol.Count
        '            objnode = New TreeNode
        '            objnode.Tag = CType(objgloscreening.AccessgloInteractionCol(i), gloInteraction).ID
        '            objnode.Text = CType(objgloscreening.AccessgloInteractionCol(i), gloInteraction).Name
        '            trvList.Nodes.Add(objnode)
        '        Next
        '        trvList.EndUpdate()
        '    End If
        'Catch ex As Exception
        '    trvList.EndUpdate()
        'End Try
        Dim _GloDrugInteractionCol As New gloInteractionCollection
        Try
            trvList.Nodes.Clear()
            _AdEscreening.FillMedicalConditions(_GloDrugInteractionCol)
            If Not IsNothing(_GloDrugInteractionCol) Then
                Dim i As Int16
                Dim objnode As TreeNode
                trvList.BeginUpdate()
                For i = 0 To _GloDrugInteractionCol.Count - 1
                    objnode = New TreeNode
                    objnode.Tag = _GloDrugInteractionCol.Item(i).ID
                    objnode.Text = _GloDrugInteractionCol.Item(i).Name
                    trvList.Nodes.Add(objnode)
                    objnode = Nothing
                Next
                trvList.EndUpdate()
            End If
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            trvList.EndUpdate()
            Dim objex As New DrugInteractionControlException
            objex.ErrMessage = ex.Message
            Throw objex
        Finally
            _GloDrugInteractionCol.Dispose()
            _GloDrugInteractionCol = Nothing
        End Try
    End Sub
    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvList.Select()
        End If
    End Sub
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RaiseEvent OkClk(sender, e)
    End Sub
End Class
