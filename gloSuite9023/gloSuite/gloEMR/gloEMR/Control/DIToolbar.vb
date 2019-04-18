Imports System.Windows.Forms
Imports gloGlobal.DIB

Public Class DIToolbar
    Inherits System.Windows.Forms.UserControl

    Public Event PerformDrugScreening(ByVal ScreeningType As ScreeningType)

    Public Event DIScreen_Click1(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs, ByVal inttype As Int16, ByVal drugid As Int64)

    'Raised when a particular screening module is selected
    Public Event DIResult(ByVal blnresult As Boolean)
    'Raised when error thrown in class

    'Private objDrugs As gloInteractionCollection
    'Private objAllergies As gloInteractionCollection
    'Private objMedicalConditionCol As gloInteractionCollection

    'Private WithEvents _gloDrugInteraction As gloDrugInteraction
    'Private _gloInteractionCollection As gloInteractionCollection

    'Collection that contains the resultset for each screening module
    'and is used to load user control collection which is then returned 
    'to the form to show the screening result.
    Public DrugInteractionResultSet As Collection

    Private btntype As Int16
    Public m_SeverityLevel As String
    Public m_DocLevel As String


#Region " Windows Form Designer generated code "

    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Friend WithEvents DIMenu1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_ADE As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_PAR As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_DI As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_DT As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_DFA As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_MI As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_EXIT As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_PRC As System.Windows.Forms.ToolStripButton

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        m_SeverityLevel = ""
        m_DocLevel = ""

    End Sub


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

    Public Sub MyDispose()

    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents DImenu_icons As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DIToolbar))
        Me.DImenu_icons = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlToolBar = New System.Windows.Forms.Panel()
        Me.DIMenu1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_ADE = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_PAR = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_DI = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_DT = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_DFA = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_MI = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_PRC = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_EXIT = New System.Windows.Forms.ToolStripButton()
        Me.pnlToolBar.SuspendLayout()
        Me.DIMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DImenu_icons
        '
        Me.DImenu_icons.ImageStream = CType(resources.GetObject("DImenu_icons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.DImenu_icons.TransparentColor = System.Drawing.Color.Transparent
        Me.DImenu_icons.Images.SetKeyName(0, "")
        Me.DImenu_icons.Images.SetKeyName(1, "")
        Me.DImenu_icons.Images.SetKeyName(2, "")
        Me.DImenu_icons.Images.SetKeyName(3, "")
        Me.DImenu_icons.Images.SetKeyName(4, "")
        Me.DImenu_icons.Images.SetKeyName(5, "")
        Me.DImenu_icons.Images.SetKeyName(6, "")
        Me.DImenu_icons.Images.SetKeyName(7, "")
        Me.DImenu_icons.Images.SetKeyName(8, "")
        Me.DImenu_icons.Images.SetKeyName(9, "")
        Me.DImenu_icons.Images.SetKeyName(10, "")
        Me.DImenu_icons.Images.SetKeyName(11, "")
        Me.DImenu_icons.Images.SetKeyName(12, "")
        Me.DImenu_icons.Images.SetKeyName(13, "")
        '
        'pnlToolBar
        '
        Me.pnlToolBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolBar.Controls.Add(Me.DIMenu1)
        Me.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(322, 52)
        Me.pnlToolBar.TabIndex = 2
        '
        'DIMenu1
        '
        Me.DIMenu1.BackColor = System.Drawing.Color.Transparent
        Me.DIMenu1.BackgroundImage = CType(resources.GetObject("DIMenu1.BackgroundImage"), System.Drawing.Image)
        Me.DIMenu1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DIMenu1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DIMenu1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.DIMenu1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.DIMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_ADE, Me.tblbtn_PAR, Me.tblbtn_DI, Me.tblbtn_DT, Me.tblbtn_DFA, Me.tblbtn_MI, Me.tblbtn_PRC, Me.tblbtn_EXIT})
        Me.DIMenu1.Location = New System.Drawing.Point(0, 0)
        Me.DIMenu1.Name = "DIMenu1"
        Me.DIMenu1.Size = New System.Drawing.Size(322, 52)
        Me.DIMenu1.TabIndex = 0
        Me.DIMenu1.Text = "ToolStrip1"
        '
        'tblbtn_ADE
        '
        Me.tblbtn_ADE.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_ADE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_ADE.Image = CType(resources.GetObject("tblbtn_ADE.Image"), System.Drawing.Image)
        Me.tblbtn_ADE.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_ADE.Name = "tblbtn_ADE"
        Me.tblbtn_ADE.Size = New System.Drawing.Size(48, 49)
        Me.tblbtn_ADE.Text = "  &ADE "
        Me.tblbtn_ADE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_ADE.ToolTipText = "Adverse Drug Effects"
        '
        'tblbtn_PAR
        '
        Me.tblbtn_PAR.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_PAR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_PAR.Image = CType(resources.GetObject("tblbtn_PAR.Image"), System.Drawing.Image)
        Me.tblbtn_PAR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_PAR.Name = "tblbtn_PAR"
        Me.tblbtn_PAR.Size = New System.Drawing.Size(49, 49)
        Me.tblbtn_PAR.Text = "  &PAR "
        Me.tblbtn_PAR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_PAR.ToolTipText = "Prior Adverse Reaction "
        '
        'tblbtn_DI
        '
        Me.tblbtn_DI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_DI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_DI.Image = CType(resources.GetObject("tblbtn_DI.Image"), System.Drawing.Image)
        Me.tblbtn_DI.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_DI.Name = "tblbtn_DI"
        Me.tblbtn_DI.Size = New System.Drawing.Size(41, 49)
        Me.tblbtn_DI.Text = "  &DI  "
        Me.tblbtn_DI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_DI.ToolTipText = "Drug Interaction   "
        '
        'tblbtn_DT
        '
        Me.tblbtn_DT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_DT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_DT.Image = CType(resources.GetObject("tblbtn_DT.Image"), System.Drawing.Image)
        Me.tblbtn_DT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_DT.Name = "tblbtn_DT"
        Me.tblbtn_DT.Size = New System.Drawing.Size(39, 49)
        Me.tblbtn_DT.Text = "  &DT "
        Me.tblbtn_DT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_DT.ToolTipText = "Duplicate Therapy"
        '
        'tblbtn_DFA
        '
        Me.tblbtn_DFA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_DFA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_DFA.Image = CType(resources.GetObject("tblbtn_DFA.Image"), System.Drawing.Image)
        Me.tblbtn_DFA.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_DFA.Name = "tblbtn_DFA"
        Me.tblbtn_DFA.Size = New System.Drawing.Size(47, 49)
        Me.tblbtn_DFA.Text = "  &DFA "
        Me.tblbtn_DFA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_DFA.ToolTipText = "Drug To Food Interaction"
        '
        'tblbtn_MI
        '
        Me.tblbtn_MI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_MI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_MI.Image = CType(resources.GetObject("tblbtn_MI.Image"), System.Drawing.Image)
        Me.tblbtn_MI.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_MI.Name = "tblbtn_MI"
        Me.tblbtn_MI.Size = New System.Drawing.Size(43, 49)
        Me.tblbtn_MI.Text = "  &MI  "
        Me.tblbtn_MI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_MI.ToolTipText = "Medical Instructions"
        '
        'tblbtn_PRC
        '
        Me.tblbtn_PRC.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_PRC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_PRC.Image = CType(resources.GetObject("tblbtn_PRC.Image"), System.Drawing.Image)
        Me.tblbtn_PRC.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_PRC.Name = "tblbtn_PRC"
        Me.tblbtn_PRC.Size = New System.Drawing.Size(36, 49)
        Me.tblbtn_PRC.Text = "P&RC"
        Me.tblbtn_PRC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_PRC.ToolTipText = "Drug To Disease"
        '
        'tblbtn_EXIT
        '
        Me.tblbtn_EXIT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_EXIT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_EXIT.Image = CType(resources.GetObject("tblbtn_EXIT.Image"), System.Drawing.Image)
        Me.tblbtn_EXIT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_EXIT.Name = "tblbtn_EXIT"
        Me.tblbtn_EXIT.Size = New System.Drawing.Size(36, 50)
        Me.tblbtn_EXIT.Text = "&Exit"
        Me.tblbtn_EXIT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_EXIT.Visible = False
        '
        'DIToolbar
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnlToolBar)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "DIToolbar"
        Me.Size = New System.Drawing.Size(322, 52)
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.DIMenu1.ResumeLayout(False)
        Me.DIMenu1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub DIMenu1_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles DIMenu1.ItemClicked
        Select Case e.ClickedItem.Name
            Case "tblbtn_ADE"
                RaiseEvent PerformDrugScreening(ScreeningType.ADE) ''DIScreen_Click1(sender, e, 1, 0)
            Case "tblbtn_PAR"
                RaiseEvent PerformDrugScreening(ScreeningType.PAR) ''DIScreen_Click1(sender, e, 2, 0)
            Case "tblbtn_DI"
                RaiseEvent PerformDrugScreening(ScreeningType.DI) ''DIScreen_Click1(sender, e, 3, 0)
            Case "tblbtn_DT"
                RaiseEvent PerformDrugScreening(ScreeningType.DT) ''DIScreen_Click1(sender, e, 4, 0)
            Case "tblbtn_DFA"
                RaiseEvent PerformDrugScreening(ScreeningType.DFA) ''DIScreen_Click1(sender, e, 5, 0)
            Case "tblbtn_MI"
                RaiseEvent PerformDrugScreening(ScreeningType.MI) ''DIScreen_Click1(sender, e, 7, 0)
            Case "tblbtn_PRC"
                RaiseEvent PerformDrugScreening(ScreeningType.PRC) ''DIScreen_Click1(sender, e, 8, 0)
            Case "tblbtn_Exit"
                RaiseEvent PerformDrugScreening(ScreeningType.None) ''DIScreen_Click1(sender, e, 6, 0)
        End Select
    End Sub

    Public Property Screentype() As Int16
        Get
            Return btntype
        End Get
        Set(ByVal Value As Int16)
            btntype = Value
        End Set
    End Property

    Public Sub SetSeverityLevels(ByVal severitylevel As String, Optional ByVal doclevel As String = "")
        m_SeverityLevel = severitylevel
        m_DocLevel = doclevel
    End Sub
    Public Sub RefreshToolBar(ByVal objhash As Hashtable, Optional ByVal m_drugalert As String = "")
        m_SeverityLevel = ""
        m_DocLevel = ""

        If Not IsNothing(objhash.Item("PRC")) Then
            DIMenu1.Items(6).Text = " PRC " & " " & CType(objhash.Item("PRC"), String)
            btntype = 8
        End If

        If Not IsNothing(objhash.Item("DFA")) Then
            DIMenu1.Items(4).Text = " DFA " & " " & CType(objhash.Item("DFA"), String)
            btntype = 5
        End If

        If Not IsNothing(objhash.Item("DT")) Then
            DIMenu1.Items(3).Text = " DT " & " " & CType(objhash.Item("DT"), String)
            btntype = 4
        End If

        If Not IsNothing(objhash.Item("DI")) Then
            DIMenu1.Items(2).Text = " DI " & " " & CType(objhash.Item("DI"), String)
            btntype = 3
        End If

        If Not IsNothing(objhash.Item("PAR")) Then
            DIMenu1.Items(1).Text = " PAR " & " " & CType(objhash.Item("PAR"), String)
            btntype = 2
        End If

        If Not IsNothing(objhash.Item("ADE")) Then
            DIMenu1.Items(0).Text = " ADE " & " " & CType(objhash.Item("ADE"), String)
            btntype = 1
        End If

        If m_drugalert = "ADE" Then
            btntype = 1
        ElseIf m_drugalert = "PAR" Then
            btntype = 2
        ElseIf m_drugalert = "DI" Then
            btntype = 3
        ElseIf m_drugalert = "DT" Then
            btntype = 4
        ElseIf m_drugalert = "DFA" Then
            btntype = 5
        ElseIf m_drugalert = "PRC" Then
            btntype = 8
        End If

    End Sub
    Public Sub RefreshToolBar()
        m_SeverityLevel = ""
        m_DocLevel = ""

        DIMenu1.Items(6).Text = " PRC "
        DIMenu1.Items(4).Text = " DFA "
        DIMenu1.Items(3).Text = " DT "
        DIMenu1.Items(2).Text = " DI "
        DIMenu1.Items(1).Text = " PAR "
        DIMenu1.Items(0).Text = " ADE "
        
    End Sub
  
    ''' <summary>
    ''' this function is called from History DI wrt showing drug interaction only for PAR and MI
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub HideDIButtonsForHistoryDI()
        tblbtn_ADE.Visible = False
        tblbtn_DFA.Visible = False
        tblbtn_DI.Visible = False
        tblbtn_DT.Visible = False
        tblbtn_PRC.Visible = False
        tblbtn_EXIT.Visible = False
    End Sub
End Class

