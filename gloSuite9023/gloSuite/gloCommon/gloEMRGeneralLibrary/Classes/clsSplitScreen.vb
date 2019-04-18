Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class clsSplitScreen

    Implements IDisposable

    Friend WithEvents UiSplitScreenPanelManager As Janus.Windows.UI.Dock.UIPanelManager
    'Public WithEvents uiPanMainPanel As Janus.Windows.UI.Dock.UIPanelGroup
    Friend WithEvents uiPanSplitScreen As Janus.Windows.UI.Dock.UIPanelGroup
    Friend WithEvents uiPanPatientExam As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contExams As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanPatientLetters As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contPatientLetters As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanNurseNotes As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contNurseNotes As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanPatientMessages As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contMessages As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanRxmed As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contRxMed As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanHistory As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contHistory As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanDMS As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contDMS As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanLabs As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contLabs As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanOrders As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contOrders As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents uiPanProblemList As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents contProblemList As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents ImgLarge As System.Windows.Forms.ImageList

    Dim _isPanelClick As Boolean = False
    Dim PatientID As Long
    Dim PatientCode As String
    Dim PatientName As String
    Dim VisitID As Long
    Dim clinicId As Long
    Dim loginId As Long
    Dim _objCriteria As Object
    Dim _objWord As Object
    Dim isSplitControlCallOut As Boolean = False
    '''20121212::Bug No.:41893::New Variable Declared
    Dim showOutlookNavMenu As Boolean = False

    Dim pwnSplitExam1 As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitLetters As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitNursenotes As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitMessages As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitRxMed As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitHistory As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitLabs As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitDMS As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitOrders As gloUC_PastWordNotes_SplitControl
    Dim pwnSplitProblemOrder As gloUC_PastWordNotes_SplitControl

    Dim _clsPatientExams As Object
    Dim _clsPatientLetters As Object
    Dim _clsPatientMessages As Object
    Dim _clsNurseNotes As Object
    Dim _clsRxmed As Object
    Dim _clsHistory As Object
    Dim _clsLabs As Object
    Dim _clsDMS As Object
    Dim _clsOrders As Object
    Dim _clsProblemList As Object
    Dim _clsUCLabControl As Object
    Dim _blnShowSmokingStatusCol As Boolean
    Dim _ParentFrm As Form


    Public Property ParentFrm As Form
        Get
            Return _ParentFrm
        End Get
        Set(ByVal value As Form)
            _ParentFrm = value
        End Set
    End Property

    Public Property clsPatientExams As Object
        Get
            Return _clsPatientExams
        End Get
        Set(ByVal value As Object)
            _clsPatientExams = value
        End Set
    End Property

    Public Property clsPatientLetters As Object
        Get
            Return _clsPatientLetters
        End Get
        Set(ByVal value As Object)
            _clsPatientLetters = value
        End Set
    End Property

    Public Property clsPatientMessages As Object
        Get
            Return _clsPatientMessages
        End Get
        Set(ByVal value As Object)
            _clsPatientMessages = value
        End Set
    End Property

    Public Property clsNurseNotes As Object
        Get
            Return _clsNurseNotes
        End Get
        Set(ByVal value As Object)
            _clsNurseNotes = value
        End Set
    End Property

    Public Property clsRxmed As Object
        Get
            Return _clsRxmed
        End Get
        Set(ByVal value As Object)
            _clsRxmed = value
        End Set
    End Property

    Public Property clsHistory As Object
        Get
            Return _clsHistory
        End Get
        Set(ByVal value As Object)
            _clsHistory = value
        End Set
    End Property

    Public Property clsLabs As Object
        Get
            Return _clsLabs
        End Get
        Set(ByVal value As Object)
            _clsLabs = value
        End Set
    End Property

    Public Property clsDMS As Object
        Get
            Return _clsDMS
        End Get
        Set(ByVal value As Object)
            _clsDMS = value
        End Set
    End Property

    Public Property clsOrders As Object
        Get
            Return _clsOrders
        End Get
        Set(ByVal value As Object)
            _clsOrders = value
        End Set
    End Property

    Public Property clsProblemList As Object
        Get
            Return _clsProblemList
        End Get
        Set(ByVal value As Object)
            _clsProblemList = value
        End Set
    End Property

    Public Property clsUCLabControl As Object
        Get
            Return _clsUCLabControl
        End Get
        Set(ByVal value As Object)
            _clsUCLabControl = value
        End Set
    End Property

    Public Property blnShowSmokingStatusCol As Boolean
        Get
            Return _blnShowSmokingStatusCol
        End Get
        Set(ByVal value As Boolean)
            _blnShowSmokingStatusCol = value
        End Set
    End Property



    '20120910-Yatin
    'Design Split Control At runtime
    Public Function LoadSplitControl(ByVal frm As Form, ByVal m_PatientID As Long, ByVal nVisitId As Long, ByVal fromForm As String, ByVal objCriteria As Object, ByVal objWord As Object, ByVal _clinicId As Long, ByVal _loginId As Long) As Janus.Windows.UI.Dock.UIPanelGroup
        Try
            PatientID = m_PatientID
            VisitID = nVisitId
            clinicId = _clinicId
            loginId = _loginId
            _objCriteria = objCriteria
            _objWord = objWord
            ParentFrm = frm

            If Not IsNothing(UiSplitScreenPanelManager) Then  ''slr free it
                UiSplitScreenPanelManager.Dispose()
                UiSplitScreenPanelManager = Nothing
            End If
            UiSplitScreenPanelManager = New Janus.Windows.UI.Dock.UIPanelManager()

            UiSplitScreenPanelManager.ContainerControl = frm
            UiSplitScreenPanelManager.DefaultPanelSettings.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            UiSplitScreenPanelManager.DefaultPanelSettings.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
            UiSplitScreenPanelManager.LargeImageList = Me.ImgLarge
            UiSplitScreenPanelManager.TabStripFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            UiSplitScreenPanelManager.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007

            GetPatientDetails()

            'uiPanMainPanel = New Janus.Windows.UI.Dock.UIPanelGroup()
            If Not IsNothing(uiPanSplitScreen) Then
                uiPanSplitScreen.Dispose()
                uiPanSplitScreen = Nothing
            End If

            uiPanSplitScreen = New Janus.Windows.UI.Dock.UIPanelGroup()

            If Not IsNothing(uiPanPatientExam) Then
                uiPanPatientExam.Dispose()
                uiPanPatientExam = Nothing
            End If

            uiPanPatientExam = New Janus.Windows.UI.Dock.UIPanel()

            If Not IsNothing(contExams) Then
                contExams.Dispose()
                contExams = Nothing
            End If
            contExams = New Janus.Windows.UI.Dock.UIPanelInnerContainer()

            If Not IsNothing(uiPanPatientLetters) Then
                uiPanPatientLetters.Dispose()
                uiPanPatientLetters = Nothing
            End If

            uiPanPatientLetters = New Janus.Windows.UI.Dock.UIPanel()


            If Not IsNothing(contPatientLetters) Then
                contPatientLetters.Dispose()
                contPatientLetters = Nothing
            End If

            contPatientLetters = New Janus.Windows.UI.Dock.UIPanelInnerContainer()

            If Not IsNothing(uiPanNurseNotes) Then
                uiPanNurseNotes.Dispose()
                uiPanNurseNotes = Nothing
            End If

            uiPanNurseNotes = New Janus.Windows.UI.Dock.UIPanel()
            If Not IsNothing(contNurseNotes) Then
                contNurseNotes.Dispose()
                contNurseNotes = Nothing
            End If

            contNurseNotes = New Janus.Windows.UI.Dock.UIPanelInnerContainer()

            If Not IsNothing(uiPanPatientMessages) Then
                uiPanPatientMessages.Dispose()
                uiPanPatientMessages = Nothing
            End If

            uiPanPatientMessages = New Janus.Windows.UI.Dock.UIPanel()

            If Not IsNothing(contMessages) Then
                contMessages.Dispose()
                contMessages = Nothing
            End If


            contMessages = New Janus.Windows.UI.Dock.UIPanelInnerContainer()

            If Not IsNothing(uiPanRxmed) Then
                uiPanRxmed.Dispose()
                uiPanRxmed = Nothing
            End If

            uiPanRxmed = New Janus.Windows.UI.Dock.UIPanel()

            If Not IsNothing(contRxMed) Then
                contRxMed.Dispose()
                contRxMed = Nothing
            End If
            contRxMed = New Janus.Windows.UI.Dock.UIPanelInnerContainer()


            If Not IsNothing(uiPanHistory) Then
                uiPanHistory.Dispose()
                uiPanHistory = Nothing
            End If

            uiPanHistory = New Janus.Windows.UI.Dock.UIPanel()


            If Not IsNothing(contHistory) Then
                contHistory.Dispose()
                contHistory = Nothing
            End If

            contHistory = New Janus.Windows.UI.Dock.UIPanelInnerContainer()



            If Not IsNothing(uiPanDMS) Then
                uiPanDMS.Dispose()
                uiPanDMS = Nothing
            End If

            uiPanDMS = New Janus.Windows.UI.Dock.UIPanel()

            If Not IsNothing(contDMS) Then
                contDMS.Dispose()
                contDMS = Nothing
            End If
            contDMS = New Janus.Windows.UI.Dock.UIPanelInnerContainer()

            If Not IsNothing(uiPanLabs) Then
                uiPanLabs.Dispose()
                uiPanLabs = Nothing
            End If

            uiPanLabs = New Janus.Windows.UI.Dock.UIPanel()


            If Not IsNothing(contLabs) Then
                contLabs.Dispose()
                contLabs = Nothing
            End If
            contLabs = New Janus.Windows.UI.Dock.UIPanelInnerContainer()

            If Not IsNothing(uiPanOrders) Then
                uiPanOrders.Dispose()
                uiPanOrders = Nothing
            End If
            uiPanOrders = New Janus.Windows.UI.Dock.UIPanel()

            If Not IsNothing(contOrders) Then
                contOrders.Dispose()
                contOrders = Nothing
            End If
            contOrders = New Janus.Windows.UI.Dock.UIPanelInnerContainer()

            If Not IsNothing(uiPanProblemList) Then
                uiPanProblemList.Dispose()
                uiPanProblemList = Nothing
            End If

            uiPanProblemList = New Janus.Windows.UI.Dock.UIPanel()

            If Not IsNothing(contProblemList) Then
                contProblemList.Dispose()
                contProblemList = Nothing
            End If

            contProblemList = New Janus.Windows.UI.Dock.UIPanelInnerContainer()

            uiPanSplitScreen.Id = New System.Guid("cd93dadf-3067-4964-b42a-50d4cf93e3cf")

            LoadControlDisplaySetting()

            If Not loadedfromDatabase Then
                loadDefaultDesign()
                UiSplitScreenPanelManager.Panels.Add(uiPanSplitScreen)
            End If

            loadSplitControlData(m_PatientID, nVisitId, uiPanSplitScreen.SelectedPanel.Name, objCriteria, objWord, clinicId)
            uiPanSplitScreen.BringToFront()
            Return uiPanSplitScreen

        Catch ex As Exception
            Return uiPanSplitScreen
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try
    End Function

    Public Sub loadDefaultDesign()

        'uiPanSplitScreen

        Me.uiPanSplitScreen.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.uiPanSplitScreen.GroupStyle = Janus.Windows.UI.Dock.PanelGroupStyle.OutlookNavigator
        Me.uiPanSplitScreen.Location = New System.Drawing.Point(3, 33)
        Me.uiPanSplitScreen.Name = "uiPanSplitScreen"
        Me.uiPanSplitScreen.SelectedPanel = Me.uiPanRxmed
        Me.uiPanSplitScreen.Size = New System.Drawing.Size(295, 572)
        Me.uiPanSplitScreen.TabIndex = 1
        Me.uiPanSplitScreen.Text = "Split Screen"
        Me.uiPanSplitScreen.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uiPanSplitScreen.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uiPanSplitScreen.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uiPanSplitScreen.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uiPanSplitScreen.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uiPanSplitScreen.StaticGroup = True
        Me.uiPanSplitScreen.AutoHide = True
        'Me.uiPanSplitScreen.Cursor = System.Windows.Forms.Cursors.No

        '
        'contRxMed
        '
        contRxMed.Location = New System.Drawing.Point(1, 24)
        contRxMed.Name = "contRxMed"
        contRxMed.Size = New System.Drawing.Size(289, 254)
        contRxMed.TabIndex = 2
        '
        'uiPanRxmed
        '
        uiPanRxmed.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanRxmed.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanRxmed.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanRxmed.InnerContainer = contRxMed
        uiPanRxmed.Image = gloEMRGeneralLibrary.My.Resources.Resources.Medication16
        uiPanRxmed.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.Medication24
        uiPanRxmed.Location = New System.Drawing.Point(4, 0)
        uiPanRxmed.Name = "uiPanRxmed"
        uiPanRxmed.Size = New System.Drawing.Size(291, 278)
        uiPanRxmed.TabIndex = 3
        uiPanRxmed.Text = "Medication"
        uiPanRxmed.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanRxmed.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanRxmed.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanRxmed.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanRxmed.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanRxmed.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanRxmed)


        '
        'contDMS
        '
        contDMS.Location = New System.Drawing.Point(1, 24)
        contDMS.Name = "contDMS"
        contDMS.Size = New System.Drawing.Size(289, 254)
        contDMS.TabIndex = 4
        '
        'uiPanDMS
        '
        uiPanDMS.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanDMS.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanDMS.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanDMS.InnerContainer = contDMS
        uiPanDMS.Image = gloEMRGeneralLibrary.My.Resources.Resources.ViewDoc16
        uiPanDMS.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.ViewDoc24
        uiPanDMS.Location = New System.Drawing.Point(4, 0)
        uiPanDMS.Name = "uiPanDMS"
        uiPanDMS.Size = New System.Drawing.Size(291, 278)
        uiPanDMS.TabIndex = 5
        uiPanDMS.Text = "View Documents"
        uiPanDMS.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanDMS.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanDMS.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanDMS.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanDMS.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanDMS.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanDMS)


        '
        'contHistory
        '
        contHistory.Location = New System.Drawing.Point(1, 24)
        contHistory.Name = "contHistory"
        contHistory.Size = New System.Drawing.Size(289, 254)
        contHistory.TabIndex = 6
        '
        'uiPanHistory
        '
        ' Me.uiPanHistory.Image = CType(Resources.GetObject("uiPanHistory.Image"), System.Drawing.Image)
        uiPanHistory.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanHistory.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanHistory.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanHistory.InnerContainer = contHistory
        uiPanHistory.Image = gloEMRGeneralLibrary.My.Resources.Resources.History16
        uiPanHistory.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.History24
        uiPanHistory.Location = New System.Drawing.Point(4, 0)
        uiPanHistory.Name = "uiPanHistory"
        uiPanHistory.Size = New System.Drawing.Size(291, 278)
        uiPanHistory.TabIndex = 7
        uiPanHistory.Text = "History"
        uiPanHistory.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanHistory.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanHistory.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanHistory.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanHistory.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanHistory.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanHistory)


        '
        'contExams
        '
        contExams.Location = New System.Drawing.Point(1, 24)
        contExams.Name = "contExams"
        contExams.Size = New System.Drawing.Size(289, 254)
        contExams.TabIndex = 8
        '
        'uiPanPatientExam
        '
        uiPanPatientExam.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientExam.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanPatientExam.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientExam.Image = gloEMRGeneralLibrary.My.Resources.Resources.PatientExam16
        uiPanPatientExam.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.PatientExam24
        uiPanPatientExam.InnerContainer = contExams
        uiPanPatientExam.Location = New System.Drawing.Point(0, 0)
        uiPanPatientExam.Name = "uiPanPatientExam"
        uiPanPatientExam.Size = New System.Drawing.Size(291, 212)
        uiPanPatientExam.TabIndex = 9
        uiPanPatientExam.Text = "Patient Exams"
        uiPanPatientExam.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanPatientExam.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientExam.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientExam.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientExam.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientExam.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanPatientExam)


        '
        'contLabs
        '
        contLabs.Location = New System.Drawing.Point(1, 24)
        contLabs.Name = "contLabs"
        contLabs.Size = New System.Drawing.Size(289, 254)
        contLabs.TabIndex = 10
        '
        'uiPanLabs
        '
        uiPanLabs.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanLabs.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanLabs.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanLabs.InnerContainer = contLabs
        uiPanLabs.Image = gloEMRGeneralLibrary.My.Resources.Resources.Lab16
        uiPanLabs.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.Lab24
        uiPanLabs.Location = New System.Drawing.Point(4, 0)
        uiPanLabs.Name = "uiPanLabs"
        uiPanLabs.Size = New System.Drawing.Size(291, 278)
        uiPanLabs.TabIndex = 11
        uiPanLabs.Text = "Orders & Results"
        uiPanLabs.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanLabs.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanLabs.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanLabs.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanLabs.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanLabs.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanLabs)


        '
        'contMessages
        '
        contMessages.Location = New System.Drawing.Point(1, 24)
        contMessages.Name = "contMessages"
        contMessages.Size = New System.Drawing.Size(289, 254)
        contMessages.TabIndex = 12
        '
        'uiPanPatientMessages
        '
        uiPanPatientMessages.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientMessages.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanPatientMessages.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientMessages.InnerContainer = contMessages
        uiPanPatientMessages.Image = gloEMRGeneralLibrary.My.Resources.Resources.Messages16
        uiPanPatientMessages.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.Messages
        uiPanPatientMessages.Location = New System.Drawing.Point(4, 0)
        uiPanPatientMessages.Name = "uiPanPatientMessages"
        uiPanPatientMessages.Size = New System.Drawing.Size(291, 278)
        uiPanPatientMessages.TabIndex = 13
        uiPanPatientMessages.Text = "Patient Messages"
        uiPanPatientMessages.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanPatientMessages.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientMessages.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientMessages.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientMessages.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientMessages.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanPatientMessages)


        '
        'contNurseNotes
        '
        contNurseNotes.Location = New System.Drawing.Point(1, 24)
        contNurseNotes.Name = "contNurseNotes"
        contNurseNotes.Size = New System.Drawing.Size(289, 254)
        contNurseNotes.TabIndex = 14
        '
        'uiPanNurseNotes
        '
        uiPanNurseNotes.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanNurseNotes.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanNurseNotes.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanNurseNotes.InnerContainer = contNurseNotes
        uiPanNurseNotes.Image = gloEMRGeneralLibrary.My.Resources.Resources.NurseNote16
        uiPanNurseNotes.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.NurseNote24
        uiPanNurseNotes.Location = New System.Drawing.Point(4, 0)
        uiPanNurseNotes.Name = "uiPanNurseNotes"
        uiPanNurseNotes.Size = New System.Drawing.Size(291, 278)
        uiPanNurseNotes.TabIndex = 15
        uiPanNurseNotes.Text = "Nurse Notes"
        uiPanNurseNotes.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanNurseNotes.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanNurseNotes.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanNurseNotes.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanNurseNotes.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanNurseNotes.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanNurseNotes)


        '
        'contPatientLetters
        '
        contPatientLetters.Location = New System.Drawing.Point(1, 24)
        contPatientLetters.Name = "contPatientLetters"
        contPatientLetters.Size = New System.Drawing.Size(289, 254)
        contPatientLetters.TabIndex = 16
        '
        'uiPanPatientLetters
        '
        uiPanPatientLetters.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientLetters.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanPatientLetters.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientLetters.InnerContainer = contPatientLetters
        uiPanPatientLetters.Image = gloEMRGeneralLibrary.My.Resources.Resources.PatientLetters16
        uiPanPatientLetters.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.PatientLetters24
        uiPanPatientLetters.Location = New System.Drawing.Point(4, 0)
        uiPanPatientLetters.Name = "uiPanPatientLetters"
        uiPanPatientLetters.Size = New System.Drawing.Size(291, 278)
        uiPanPatientLetters.TabIndex = 17
        uiPanPatientLetters.Text = "Patient Letters"
        uiPanPatientLetters.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanPatientLetters.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientLetters.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientLetters.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientLetters.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanPatientLetters.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanPatientLetters)


        '
        'contOrders
        '
        contOrders.Location = New System.Drawing.Point(1, 24)
        contOrders.Name = "contOrders"
        contOrders.Size = New System.Drawing.Size(289, 254)
        contOrders.TabIndex = 18
        '
        'uiPanOrders
        '
        uiPanOrders.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanOrders.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanOrders.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanOrders.InnerContainer = contOrders
        uiPanOrders.Image = gloEMRGeneralLibrary.My.Resources.Resources.Orders16
        uiPanOrders.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.Orders24
        uiPanOrders.Location = New System.Drawing.Point(4, 0)
        uiPanOrders.Name = "uiPanOrders"
        uiPanOrders.Size = New System.Drawing.Size(291, 278)
        uiPanOrders.TabIndex = 19
        uiPanOrders.Text = "Order Templates"
        uiPanOrders.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanOrders.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanOrders.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanOrders.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanOrders.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanOrders.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanOrders)


        '
        'contProblemList
        '
        contProblemList.Location = New System.Drawing.Point(1, 24)
        contProblemList.Name = "contProblemList"
        contProblemList.Size = New System.Drawing.Size(289, 254)
        contProblemList.TabIndex = 20
        '
        'uiPanProblemList
        '
        uiPanProblemList.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanProblemList.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark
        uiPanProblemList.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanProblemList.InnerContainer = contProblemList
        uiPanProblemList.Image = gloEMRGeneralLibrary.My.Resources.Resources.ProblemList16
        uiPanProblemList.LargeImage = gloEMRGeneralLibrary.My.Resources.Resources.ProblemList24
        uiPanProblemList.Location = New System.Drawing.Point(4, 0)
        uiPanProblemList.Name = "uiPanProblemList"
        uiPanProblemList.Size = New System.Drawing.Size(291, 278)
        uiPanProblemList.TabIndex = 21
        uiPanProblemList.Text = "Problem List"
        uiPanProblemList.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        uiPanProblemList.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanProblemList.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanProblemList.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanProblemList.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanProblemList.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD ' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        uiPanSplitScreen.Panels.Add(uiPanProblemList)

        'uiPanMainPanel.Panels.Add(uiPanSplitScreen)
    End Sub

    ''20121128::Yatin::Function To Get Patient Details
    Public Sub GetPatientDetails()
        'Dim strSQL As String = "select sPatientCode,sLastName+', '+sFirstName +' '+sMiddleName as Name  from Patient  where nPatientID=" + PatientID.ToString()
        Try
            Dim strSQL As String = "select sPatientCode, ISNULL(Upper(Patient.sLastName)+',','') + SPACE(1) + dbo.FirstLetterCaps(Patient.sFirstName) + SPACE(1) + " +
                                   "CASE WHEN ISNULL(Patient.sMiddleName,'') <> ''  " +
                                   "THEN  ISNULL(Upper(substring(Patient.sMiddleName,1,1)) + '.','')ELSE ''END AS PatientName from Patient  where nPatientID=" + PatientID.ToString()

            Dim con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            Dim cmd As New SqlCommand(strSQL, con)
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            Dim dtPatient As New DataTable
            da.Fill(dtPatient)
            con.Close()

            If Not IsNothing(dtPatient) Then
                If dtPatient.Rows.Count > 0 Then ''added condition for bugid 71304
                    PatientCode = dtPatient.Rows(0)("sPatientCode").ToString()
                    PatientName = dtPatient.Rows(0)("PatientName").ToString()
                End If
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
            da.Dispose()
            da = Nothing

            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            con.Dispose()
            con = Nothing


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try
    End Sub

    Public Sub loadSplitControlData(ByVal m_PatientID As Long, ByVal nVisitId As Long, ByVal fromForm As String, ByVal objCriteria As Object, ByVal objWord As Object, ByVal clinicId As Long)
        PatientID = m_PatientID
        If Not IsNothing(uiPanSplitScreen) Then
            If Not IsNothing(uiPanSplitScreen.SelectedPanel) Then
                If fromForm = "uiPanPatientExam" Then
                    contExams.Controls.Clear()

                    'Dim pwnSplitExam1 As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "PatientExam", objCriteria, objWord, _clsPatientExams, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitExam1) = False) Then
                        pwnSplitExam1.Dispose()
                        pwnSplitExam1 = Nothing
                    End If

                    pwnSplitExam1 = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "PatientExam", objCriteria, objWord, _clsPatientExams, clinicId, blnShowSmokingStatusCol)
                    pwnSplitExam1.Dock = DockStyle.Fill
                    contExams.Controls.Add(pwnSplitExam1)
                ElseIf fromForm = "uiPanPatientLetters" Then
                    contPatientLetters.Controls.Clear()

                    'Dim pwnSplitLetters As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "PatientLetter", objCriteria, objWord, _clsPatientLetters, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitLetters) = False) Then
                        pwnSplitLetters.Dispose()
                        pwnSplitLetters = Nothing
                    End If

                    pwnSplitLetters = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "PatientLetter", objCriteria, objWord, _clsPatientLetters, clinicId, blnShowSmokingStatusCol)
                    pwnSplitLetters.Dock = DockStyle.Fill
                    contPatientLetters.Controls.Add(pwnSplitLetters)
                ElseIf fromForm = "uiPanNurseNotes" Then
                    contNurseNotes.Controls.Clear()

                    'Dim pwnSplitNursenotes As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "NurseNotes", objCriteria, objWord, _clsNurseNotes, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitNursenotes) = False) Then
                        pwnSplitNursenotes.Dispose()
                        pwnSplitNursenotes = Nothing
                    End If

                    pwnSplitNursenotes = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "NurseNotes", objCriteria, objWord, _clsNurseNotes, clinicId, blnShowSmokingStatusCol)
                    pwnSplitNursenotes.Dock = DockStyle.Fill
                    contNurseNotes.Controls.Add(pwnSplitNursenotes)
                ElseIf fromForm = "uiPanPatientMessages" Then
                    contMessages.Controls.Clear()

                    'Dim pwnSplitMessages As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "PatientMessages", objCriteria, objWord, _clsPatientMessages, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitMessages) = False) Then
                        pwnSplitMessages.Dispose()
                        pwnSplitMessages = Nothing
                    End If

                    pwnSplitMessages = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "PatientMessages", objCriteria, objWord, _clsPatientMessages, clinicId, blnShowSmokingStatusCol)
                    pwnSplitMessages.Dock = DockStyle.Fill
                    contMessages.Controls.Add(pwnSplitMessages)
                ElseIf fromForm = "uiPanRxmed" Then
                    contRxMed.Controls.Clear()

                    'Dim pwnSplitRxMed As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "RxMed", Nothing, Nothing, _clsRxmed, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitRxMed) = False) Then
                        pwnSplitRxMed.Dispose()
                        pwnSplitRxMed = Nothing
                    End If

                    pwnSplitRxMed = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "RxMed", Nothing, Nothing, _clsRxmed, clinicId, blnShowSmokingStatusCol)
                    pwnSplitRxMed.Dock = DockStyle.Fill
                    contRxMed.Controls.Add(pwnSplitRxMed)
                ElseIf fromForm = "uiPanHistory" Then
                    contHistory.Controls.Clear()

                    'Dim pwnSplitHistory As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "History", Nothing, Nothing, _clsHistory, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitHistory) = False) Then
                        pwnSplitHistory.Dispose()
                        pwnSplitHistory = Nothing
                    End If

                    pwnSplitHistory = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "History", Nothing, Nothing, _clsHistory, clinicId, blnShowSmokingStatusCol)
                    pwnSplitHistory.Dock = DockStyle.Fill
                    contHistory.Controls.Add(pwnSplitHistory)
                ElseIf fromForm = "uiPanLabs" Then
                    contLabs.Controls.Clear()

                    'Dim pwnSplitLabs As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "Labs", Nothing, Nothing, _clsLabs, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitLabs) = False) Then
                        'pwnSplitLabs.Dispose()
                        pwnSplitLabs = Nothing
                    End If

                    pwnSplitLabs = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "Labs", Nothing, Nothing, _clsLabs, clinicId, blnShowSmokingStatusCol)
                    pwnSplitLabs.clsUCLabControl = _clsUCLabControl
                    pwnSplitLabs.ShowHide_PastExam()
                    pwnSplitLabs.Dock = DockStyle.Fill
                    contLabs.Controls.Add(pwnSplitLabs)
                    ''Added by Mayuri:20121119-Control was loading first time only
                    pwnSplitLabs.BringToFront()
                    ''End Code Added by Mayuri:20121119-Control was loading first time only
                ElseIf fromForm = "uiPanDMS" Then
                    contDMS.Controls.Clear()

                    'Dim pwnSplitDMS As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "DMS", Nothing, Nothing, _clsDMS, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitDMS) = False) Then
                        pwnSplitDMS.Dispose()
                        pwnSplitDMS = Nothing
                    End If

                    pwnSplitDMS = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "DMS", Nothing, Nothing, _clsDMS, clinicId, blnShowSmokingStatusCol)
                    pwnSplitDMS.Dock = DockStyle.Fill
                    contDMS.Controls.Add(pwnSplitDMS)
                ElseIf fromForm = "uiPanOrders" Then
                    contOrders.Controls.Clear()

                    'Dim pwnSplitOrders As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "Orders", objCriteria, objWord, _clsOrders, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitOrders) = False) Then
                        pwnSplitOrders.Dispose()
                        pwnSplitOrders = Nothing
                    End If

                    pwnSplitOrders = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "Orders", objCriteria, objWord, _clsOrders, clinicId, blnShowSmokingStatusCol)
                    pwnSplitOrders.Dock = DockStyle.Fill
                    contOrders.Controls.Add(pwnSplitOrders)
                ElseIf fromForm = "uiPanProblemList" Then
                    contProblemList.Controls.Clear()

                    'Dim pwnSplitProblemOrder As New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "ProblemList", Nothing, Nothing, _clsProblemList, clinicId, blnShowSmokingStatusCol)

                    If (IsNothing(pwnSplitProblemOrder) = False) Then
                        pwnSplitProblemOrder.Dispose()
                        pwnSplitProblemOrder = Nothing
                    End If

                    pwnSplitProblemOrder = New gloUC_PastWordNotes_SplitControl(m_PatientID, nVisitId, "ProblemList", Nothing, Nothing, _clsProblemList, clinicId, blnShowSmokingStatusCol)
                    pwnSplitProblemOrder.Dock = DockStyle.Fill
                    contProblemList.Controls.Add(pwnSplitProblemOrder)
                End If
                GetPatientDetails()
                SetPanelName(uiPanSplitScreen.SelectedPanel.Name)
            End If
        End If
    End Sub

    ''20121128::Yatin::Function To Set Panel captions
    Public Sub SetPanelName(ByVal p As String)
        uiPanRxmed.Text = "Medication"
        uiPanDMS.Text = "View Documents"
        uiPanHistory.Text = "History"
        uiPanPatientExam.Text = "Patient Exams"
        uiPanLabs.Text = "Orders & Results"
        uiPanPatientMessages.Text = "Patient Messages"
        uiPanNurseNotes.Text = "Nurse Notes"
        uiPanPatientLetters.Text = "Patient Letters"
        uiPanOrders.Text = "Order Templates"
        uiPanProblemList.Text = "Problem List"
        Try
            'If Not IsNothing(uiPanSplitScreen.Parent) Then
            'If uiPanSplitScreen.Parent.Name.ToString() = "" Then
            For Each pan As Janus.Windows.UI.Dock.UIPanel In uiPanSplitScreen.Panels
                If pan.Name = p Then
                    pan.Text = pan.Text + " - " + PatientName + " (" + PatientCode + ")"
                    Exit For
                End If
            Next
            'End If
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try
    End Sub


    Private Sub uiPanSplitScreen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles uiPanSplitScreen.Click
        '  _isPanelClick = True
        Try
            Dim panelName As String = ""
            showOutlookNavMenu = False
            If Not IsNothing(uiPanSplitScreen.GetPanelAt(CType(e, System.Windows.Forms.MouseEventArgs).X, CType(e, System.Windows.Forms.MouseEventArgs).Y)) Then
                panelName = Convert.ToString(uiPanSplitScreen.GetPanelAt(CType(e, System.Windows.Forms.MouseEventArgs).X, CType(e, System.Windows.Forms.MouseEventArgs).Y).Name)
                If panelName <> "" Then
                    loadSplitControlData(PatientID, VisitID, uiPanSplitScreen.GetPanelAt(CType(e, System.Windows.Forms.MouseEventArgs).X, CType(e, System.Windows.Forms.MouseEventArgs).Y).Name, _objCriteria, _objWord, clinicId)
                End If
                SetPanelName(panelName)
                ''''20121212::Bug No.:41893::Else Part included
            Else
                showOutlookNavMenu = True
                ''''
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try
    End Sub

    Private Sub uiPanSplitScreen_SelectedPanelChanged(ByVal sender As Object, ByVal e As Janus.Windows.UI.Dock.PanelActionEventArgs) Handles uiPanSplitScreen.SelectedPanelChanged
        If uiPanSplitScreen.AutoHide Then
            If Not IsNothing(e.Panel) Then
                loadSplitControlData(PatientID, VisitID, e.Panel.Name, _objCriteria, _objWord, clinicId)
            End If
            ''''20121212::Bug No.:41893::Else Part included
        Else
            If showOutlookNavMenu Then
                If Not IsNothing(e.Panel) Then
                    loadSplitControlData(PatientID, VisitID, e.Panel.Name, _objCriteria, _objWord, clinicId)
                End If
            End If
            ''''
        End If

    End Sub

    Private Sub uiPanSplitScreen_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uiPanSplitScreen.ParentChanged
        Try
            If Not IsNothing(uiPanSplitScreen.Parent) Then
                If uiPanSplitScreen.Parent.Name.ToString() = "" Then
                    isSplitControlCallOut = True

                End If
            Else

                isSplitControlCallOut = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try
    End Sub

    Private Sub UiSplitScreenPanelManager_CurrentLayoutChanging(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UiSplitScreenPanelManager.CurrentLayoutChanging
        If Not UiSplitScreenPanelManager.CurrentLayout Is Nothing Then
            UiSplitScreenPanelManager.CurrentLayout.Update()
        End If
    End Sub

    Dim loadNewLayout As Boolean = True
    Dim loadedfromDatabase As Boolean = False

    Public Sub LoadControlDisplaySetting()

        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dtSplit As DataTable = Nothing
        Dim oBytesArry1 As New Byte()
        Dim objParam As SqlParameter = Nothing

        Try
            'Get Users Display Settings
            Con = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            cmd = New SqlCommand("gsp_INUP_SplitScreenDisplaySettings", Con)
            cmd.CommandType = CommandType.StoredProcedure


            objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = loginId

            objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = System.Windows.Forms.SystemInformation.ComputerName()

            objParam = cmd.Parameters.Add("@iStyle", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = oBytesArry1

            objParam = cmd.Parameters.Add("@sFlag", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 2
            da = New SqlDataAdapter
            dtSplit = New DataTable
            da.SelectCommand = cmd
            da.Fill(dtSplit)

            da.Dispose()
            da = Nothing

            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Con.Dispose()
            Con = Nothing


            'Load Display Settings
            If IsNothing(dtSplit) = False Then
                If dtSplit.Rows.Count > 0 Then
                    If IsDBNull(dtSplit.Rows(0)("iStyle")) = False Then
                        Dim oBytesArry As Byte() = CType(dtSplit.Rows(0)("iStyle"), Byte())
                        Dim memStream As MemoryStream = New MemoryStream(oBytesArry)

                        Try
                            UiSplitScreenPanelManager.LoadLayoutFile(memStream)
                        Catch ex As Exception

                        End Try

                        memStream.Dispose()
                        memStream = Nothing

                        'loadedfromDatabase = True

                        If IsNothing(UiSplitScreenPanelManager.Panels) = False Then
                            'If uiPanUM.Panels.Count > 0 Then
                            For i As Int16 = 0 To UiSplitScreenPanelManager.Panels.Count - 1
                                Dim uipanPM As Janus.Windows.UI.Dock.UIPanelBase = Nothing
                                Try
                                    uipanPM = UiSplitScreenPanelManager.Panels(i)
                                Catch ex As Exception

                                End Try

                                If Not IsNothing(uipanPM) Then


                                    If uipanPM.Name = "uiPanSplitScreen" Then

                                        uiPanSplitScreen = uipanPM
                                        If Not IsNothing(uiPanSplitScreen) Then

                                            If Not IsNothing(uiPanSplitScreen.Panels) Then
                                                loadedfromDatabase = True
                                                For j As Int16 = 0 To uiPanSplitScreen.Panels.Count - 1
                                                    Dim uipanContainer As Janus.Windows.UI.Dock.UIPanelBase = Nothing
                                                    Try
                                                        uipanContainer = uiPanSplitScreen.Panels(j)
                                                    Catch ex As Exception
                                                        loadedfromDatabase = False
                                                    End Try

                                                    If Not IsNothing(uipanContainer) Then


                                                        If uipanContainer.Name = "uiPanPatientExam" Then
                                                            uiPanPatientExam = uipanContainer
                                                            uiPanPatientExam.InnerContainer = contExams
                                                        ElseIf uipanContainer.Name = "uiPanPatientLetters" Then
                                                            uiPanPatientLetters = uipanContainer
                                                            uiPanPatientLetters.InnerContainer = contPatientLetters
                                                        ElseIf uipanContainer.Name = "uiPanNurseNotes" Then
                                                            uiPanNurseNotes = uipanContainer
                                                            uiPanNurseNotes.InnerContainer = contNurseNotes
                                                        ElseIf uipanContainer.Name = "uiPanPatientMessages" Then
                                                            uiPanPatientMessages = uipanContainer
                                                            uiPanPatientMessages.InnerContainer = contMessages
                                                        ElseIf uipanContainer.Name = "uiPanRxmed" Then
                                                            uiPanRxmed = uipanContainer
                                                            uiPanRxmed.InnerContainer = contRxMed
                                                        ElseIf uipanContainer.Name = "uiPanHistory" Then
                                                            uiPanHistory = uipanContainer
                                                            uiPanHistory.InnerContainer = contHistory
                                                        ElseIf uipanContainer.Name = "uiPanLabs" Then
                                                            uiPanLabs = uipanContainer
                                                            uiPanLabs.InnerContainer = contLabs
                                                        ElseIf uipanContainer.Name = "uiPanDMS" Then
                                                            uiPanDMS = uipanContainer
                                                            uiPanDMS.InnerContainer = contDMS
                                                        ElseIf uipanContainer.Name = "uiPanOrders" Then
                                                            uiPanOrders = uipanContainer
                                                            uiPanOrders.InnerContainer = contOrders
                                                        ElseIf uipanContainer.Name = "uiPanProblemList" Then
                                                            uiPanProblemList = uipanContainer
                                                            uiPanProblemList.InnerContainer = contProblemList
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                        'End If
                    End If

                End If
            End If

        Catch ex As Exception
            loadedfromDatabase = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        Finally
            If Not IsNothing(dtSplit) Then
                dtSplit.Dispose()
                dtSplit = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If

        End Try

    End Sub

    Public Sub SaveControlDisplaySettings()
        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing
        Try
            Dim memStream As MemoryStream
            memStream = New MemoryStream()

            If Not IsNothing(UiSplitScreenPanelManager) Then
                UiSplitScreenPanelManager.SaveLayoutFile(memStream)
            End If


            Con = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)

            'Dim trn As New SqlTransaction

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            'Save New Settings
            cmd = New SqlCommand("gsp_INUP_SplitScreenDisplaySettings", Con)
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Transaction = trn

            objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = loginId

            objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = System.Windows.Forms.SystemInformation.ComputerName()


            objParam = cmd.Parameters.Add("@iStyle", SqlDbType.Image)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = memStream.ToArray()

            objParam = cmd.Parameters.Add("@sFlag", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            memStream.Close()
            memStream.Dispose()
            ' trn.Commit()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally

            If Not IsNothing(Con) Then  ''slr free it
                If Con.State = ConnectionState.Open Then
                    Con.Close()
                End If
                Con.Dispose()
                Con = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
        End Try
    End Sub

    Private Sub uiPanSplitScreen_DockChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles uiPanSplitScreen.DockChanged
        If uiPanSplitScreen.DockStyle = Janus.Windows.UI.Dock.PanelDockStyle.Fill Then

            uiPanSplitScreen.Dispose()
            uiPanSplitScreen = Nothing
            LoadSplitControl(ParentFrm, PatientID, VisitID, "", _objCriteria, _objWord, clinicId, loginId)
            uiPanSplitScreen.AutoHide = False
        End If

    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.

            'RemoveHandler uiPanSplitScreen.Click, AddressOf uiPanSplitScreenClick
            'RemoveHandler uiPanSplitScreen.ParentChanged, AddressOf uiPanSplitScreenParentChanged

            'Added code by Amit to avoid memory leaks
            _isPanelClick = Nothing
            PatientID = Nothing
            VisitID = Nothing
            clinicId = Nothing
            _objCriteria = Nothing
            _objWord = Nothing
            ''slr free it
            If IsNothing(_clsPatientExams) = False Then
                _clsPatientExams.Dispose()
                _clsPatientExams = Nothing
            End If

            ''slr free it
            If IsNothing(_clsPatientLetters) = False Then
                _clsPatientLetters.Dispose()
                _clsPatientLetters = Nothing
            End If

            If IsNothing(_clsPatientMessages) = False Then
                '23-Apr-13 Aniket: Resolving Memory Leaks
                _clsPatientMessages.Dispose()
                _clsPatientMessages = Nothing
            End If

            If IsNothing(_clsNurseNotes) = False Then
                '_clsNurseNotes.Dispose()
                _clsNurseNotes = Nothing
            End If
            ''slr free it
            If IsNothing(_clsRxmed) = False Then
                _clsRxmed.Dispose()
                _clsRxmed = Nothing
            End If

            If IsNothing(_clsHistory) = False Then
                '_clsHistory.Dispose()
                _clsHistory = Nothing
            End If

            If IsNothing(_clsLabs) = False Then
                '_clsLabs.Dispose()
                _clsLabs = Nothing
            End If

            If IsNothing(_clsDMS) = False Then
                _clsDMS.Dispose()
                _clsDMS = Nothing
            End If
            ''slr free it
            If IsNothing(_clsOrders) = False Then
                _clsOrders.Dispose()
                _clsOrders = Nothing
            End If

            ''slr free it
            If IsNothing(_clsProblemList) = False Then
                _clsProblemList.Dispose()
                _clsProblemList = Nothing
            End If

            ''slr free it
            If IsNothing(_clsUCLabControl) = False Then
                _clsUCLabControl.Dispose()
                _clsUCLabControl = Nothing
            End If

            _blnShowSmokingStatusCol = Nothing
            ''slr free it
            If (IsNothing(UiSplitScreenPanelManager) = False) Then
                UiSplitScreenPanelManager.Dispose()
                UiSplitScreenPanelManager = Nothing
            End If
            ''slr free it
            If (IsNothing(uiPanSplitScreen) = False) Then
                uiPanSplitScreen.Dispose()
                uiPanSplitScreen = Nothing
            End If

            If (IsNothing(uiPanPatientExam) = False) Then
                uiPanPatientExam.Dispose()
                uiPanPatientExam = Nothing
            End If
            ''slr free it
            If (IsNothing(contExams) = False) Then
                contExams.Dispose()
                contExams = Nothing
            End If

            If (IsNothing(uiPanPatientLetters) = False) Then
                uiPanPatientLetters.Dispose()
                uiPanPatientLetters = Nothing
            End If

            If (IsNothing(contPatientLetters) = False) Then
                contPatientLetters.Dispose()
                contPatientLetters = Nothing
            End If
            ''slr free it
            If (IsNothing(uiPanNurseNotes) = False) Then
                uiPanNurseNotes.Dispose()
                uiPanNurseNotes = Nothing
            End If

            If (IsNothing(contNurseNotes) = False) Then
                contNurseNotes.Dispose()
                contNurseNotes = Nothing
            End If
            ''slr free it
            If (IsNothing(uiPanPatientMessages) = False) Then
                uiPanPatientMessages.Dispose()
                uiPanPatientMessages = Nothing
            End If
            ''slr free it
            If (IsNothing(contMessages) = False) Then
                contMessages.Dispose()
                contMessages = Nothing
            End If
            ''slr free it
            If (IsNothing(uiPanRxmed) = False) Then
                uiPanRxmed.Dispose()
                uiPanRxmed = Nothing
            End If
            ''slr free it
            If (IsNothing(contRxMed) = False) Then
                contRxMed.Dispose()
                contRxMed = Nothing
            End If

            If (IsNothing(uiPanHistory) = False) Then
                uiPanHistory.Dispose()
                uiPanHistory = Nothing
            End If

            If (IsNothing(contHistory) = False) Then
                contHistory.Dispose()
                contHistory = Nothing
            End If

            If (IsNothing(uiPanDMS) = False) Then
                uiPanDMS.Dispose()
                uiPanDMS = Nothing
            End If

            If (IsNothing(contDMS) = False) Then
                contDMS.Dispose()
                contDMS = Nothing
            End If

            If (IsNothing(uiPanLabs) = False) Then
                uiPanLabs.Dispose()
                uiPanLabs = Nothing
            End If

            If (IsNothing(contLabs) = False) Then
                contLabs.Dispose()
                contLabs = Nothing
            End If

            If (IsNothing(uiPanOrders) = False) Then
                uiPanOrders.Dispose()
                uiPanOrders = Nothing
            End If

            If (IsNothing(contOrders) = False) Then
                contOrders.Dispose()
                contOrders = Nothing
            End If

            If (IsNothing(uiPanProblemList) = False) Then
                uiPanProblemList.Dispose()
                uiPanProblemList = Nothing
            End If

            If (IsNothing(contProblemList) = False) Then
                contProblemList.Dispose()
                contProblemList = Nothing
            End If

            If (IsNothing(pwnSplitExam1) = False) Then
                pwnSplitExam1.Dispose()
                pwnSplitExam1 = Nothing
            End If

            If (IsNothing(pwnSplitLetters) = False) Then
                pwnSplitLetters.Dispose()
                pwnSplitLetters = Nothing
            End If

            If (IsNothing(pwnSplitNursenotes) = False) Then
                pwnSplitNursenotes.Dispose()
                pwnSplitNursenotes = Nothing
            End If

            If (IsNothing(pwnSplitMessages) = False) Then
                pwnSplitMessages.Dispose()
                pwnSplitMessages = Nothing
            End If

            If (IsNothing(pwnSplitRxMed) = False) Then
                pwnSplitRxMed.Dispose()
                pwnSplitRxMed = Nothing
            End If

            If (IsNothing(pwnSplitHistory) = False) Then
                pwnSplitHistory.Dispose()
                pwnSplitHistory = Nothing
            End If

            If (IsNothing(pwnSplitLabs) = False) Then
                pwnSplitLabs.Dispose()
                pwnSplitLabs = Nothing
            End If

            If (IsNothing(pwnSplitDMS) = False) Then
                pwnSplitDMS.Dispose()
                pwnSplitDMS = Nothing
            End If

            If (IsNothing(pwnSplitOrders) = False) Then
                pwnSplitOrders.Dispose()
                pwnSplitOrders = Nothing
            End If

            If (IsNothing(pwnSplitProblemOrder) = False) Then
                pwnSplitProblemOrder.Dispose()
                pwnSplitProblemOrder = Nothing
            End If

        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
