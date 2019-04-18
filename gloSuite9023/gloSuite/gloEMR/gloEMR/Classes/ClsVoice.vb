Public Enum VoiceAddendum
    eVoice
    eAddendum
End Enum
Public Class ClsVoice
    Implements gloVoice, IDisposable



    Private mVoiceCommandsCol As New Hashtable
    Private mTemplateCommandsCol As New ArrayList
    Private mVoicecol As DNSTools.DgnStrings
    Private mTemplatecol As DNSTools.DgnStrings
    Private mMyWordToolStrip As WordToolStrip.gloWordToolStrip
    Private mMessageName As String = ""
    Public DelWordToolStripClick As HandleWordToolStripClick
    Public DelTreeViewDoubleClick As HandleTreeviewDoubleClick
    Private oMDIParentVoice As MainMenu
    Private meActivityModule As gloAuditTrail.ActivityModule
    Private enVoiceAddendum As VoiceAddendum
    Private oTreeView As TreeView
    Private trvSearchNode As TreeNode
    Public Property gloTreeView() As TreeView
        Get
            Return oTreeView
        End Get
        Set(ByVal value As TreeView)
            oTreeView = value
        End Set
    End Property
    Public Property eVoiceAddendum() As VoiceAddendum
        Get
            Return enVoiceAddendum
        End Get
        Set(ByVal value As VoiceAddendum)
            enVoiceAddendum = value
        End Set
    End Property
    Public Property eActivityModule() As gloAuditTrail.ActivityModule
        Get
            Return meActivityModule
        End Get
        Set(ByVal value As gloAuditTrail.ActivityModule)
            meActivityModule = value
        End Set
    End Property
    Public Property MyWordToolStrip() As WordToolStrip.gloWordToolStrip
        Get
            Return mMyWordToolStrip
        End Get
        Set(ByVal value As WordToolStrip.gloWordToolStrip)
            mMyWordToolStrip = value
        End Set
    End Property
    Public Property MDIParentVoice() As MainMenu
        Get
            Return oMDIParentVoice
        End Get
        Set(ByVal value As MainMenu)
            oMDIParentVoice = value
        End Set
    End Property
    Public Property MessageName() As String
        Get
            Return mMessageName
        End Get
        Set(ByVal value As String)
            mMessageName = value
        End Set
    End Property
    'Public Property DelWordToolStripClick() As HandleWordToolStripClick
    '    Get
    '        Return oDelWordToolStripClick
    '    End Get
    '    Set(ByVal value As HandleWordToolStripClick)
    '        oDelWordToolStripClick = value
    '    End Set
    'End Property
    Public Property TemplateCommandsCol() As ArrayList
        Get
            Return mTemplateCommandsCol
        End Get
        Set(ByVal value As ArrayList)
            mTemplateCommandsCol = value
        End Set
    End Property
    Public Property VoiceCommandsCol() As Hashtable
        Get

            Return mVoiceCommandsCol
        End Get
        Set(ByVal value As Hashtable)
            mVoiceCommandsCol = value
        End Set
    End Property
    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds
        If VoiceCol.Count > 0 Then
            Dim objSender As Object = Nothing
            SearchNode(oTreeView, VoiceCol.Item(1).ToString)
            If IsNothing(trvSearchNode) = False Then
                oTreeView.SelectedNode = trvSearchNode
                Dim objTrvArgs As New TreeNodeMouseClickEventArgs(oTreeView.SelectedNode, Windows.Forms.MouseButtons.Left, 2, oTreeView.Bounds.X, oTreeView.Bounds.Y)
                DelTreeViewDoubleClick(objSender, objTrvArgs)
                objTrvArgs = Nothing
            End If
        End If
    End Sub
    Private Sub SearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            SearchNode(trvNde, strText)
        Next
    End Sub

    Private Sub SearchNode(ByVal rootNode As TreeNode, ByVal strText As String)
        For Each childNode As TreeNode In rootNode.Nodes
            If LCase(Trim(childNode.Text)) = LCase(Trim(strText)) Then
                trvSearchNode = childNode
                Exit Sub
            End If
            SearchNode(childNode, strText)
        Next
    End Sub
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If VoiceCol.Count > 0 Then
            Dim objSender As Object = Nothing
            Dim obje As EventArgs = Nothing
            'Dim objKeye As KeyEventArgs
            Dim objtblbtn As New ToolStripButton
            Dim ButtonName As String = ""
            ButtonName = mVoiceCommandsCol.Item(VoiceCol.Item(1)).ToString()
            objtblbtn.Name = ButtonName
            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
            DelWordToolStripClick(objSender, objtbl)
            objtbl = Nothing
            objtblbtn.Dispose()
            objtblbtn = Nothing
            'tlsMessages_ToolStripClick(objSender, objtbl)
        End If
    End Sub

    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        If eVoiceAddendum = VoiceAddendum.eAddendum Then
            vVoiceMenu.Remove(1)
            If IsNothing(mTemplatecol) Then
                mTemplatecol = New DNSTools.DgnStrings
            End If
            Call FillTemplateCommands()

            vVoiceMenu.ListSetStrings("BasicVoiceCommands", mTemplatecol)
            vVoiceMenu.Add(1, "Tag <BasicVoiceCommands>", "", "")
        End If
        vVoiceMenu.Remove(2)
        If IsNothing(mVoicecol) Then
            mVoicecol = New DNSTools.DgnStrings

        End If
        Call AddBasicVoiceCommands()
        vVoiceMenu.ListSetStrings(mMessageName, mVoicecol)
        vVoiceMenu.Add(2, "<" & mMessageName & ">", "", "")
    End Sub

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    End Sub
    ''' <summary>
    ''' Add Voice Commands for unfinished word forms
    ''' </summary>
    ''' <param name="oHashtable"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal oHashtable As Hashtable)
        mVoicecol = New DNSTools.DgnStrings
        mVoiceCommandsCol = oHashtable
    End Sub

    ''' <summary>
    ''' Add voice commands and template commands for finished word forms
    ''' </summary>
    ''' <param name="oTemplateHashTable"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal oTemplateHashTable As ArrayList)
        mVoicecol = New DNSTools.DgnStrings
        mTemplatecol = New DNSTools.DgnStrings
        mVoiceCommandsCol = AddVoiceCommandsForAddendum()
        mTemplateCommandsCol = oTemplateHashTable
    End Sub
    ''' <summary>
    ''' 
    ''' Populate Template commands from hashtable into TemplateVoiceCol Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTemplateCommands()
        mTemplatecol.Clear()
        For i As Int32 = 0 To mTemplateCommandsCol.Count - 1
            mTemplatecol.Add(mTemplateCommandsCol.Item(i).ToString)
        Next
    End Sub
    ''' <summary>
    ''' Populate Voice commands from hashtable into Voicecol collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddBasicVoiceCommands()
        mVoicecol.Clear()
        Dim oCollection As ICollection
        Dim Key As Object
        oCollection = mVoiceCommandsCol.Keys
        For Each Key In oCollection
            mVoicecol.Add(Key.ToString)
        Next
    End Sub
    ''' <summary>
    ''' Initialise the voice menus and voice command object
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeVoiceComponents()
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            'And m_IsReadOnly = False Then
            Try
                gloAuditTrail.gloAuditTrail.UpdateLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "Add Voice collection Started at Messages Load", gloAuditTrail.ActivityOutCome.Success)
                'UpdateVoiceLog("-------------------------Add Voice collection Started at Messages Load----------------------")
                mVoicecol = New DNSTools.DgnStrings

                'Fill Voice Collections
                Call AddBasicVoiceCommands()

                'Fill Template Commands
                If enVoiceAddendum = VoiceAddendum.eAddendum Then
                    FillTemplateCommands()
                End If
                gloAuditTrail.gloAuditTrail.UpdateLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "Add Voice collection Completed at Messages Load", gloAuditTrail.ActivityOutCome.Success)
                'UpdateVoiceLog("-------------------------Add Voice collection Completed at Messages Load----------------------")

                'Dim frm As MainMenu
                'frm = CType(Me.MdiParent, MainMenu)
                'Add Voice Commands

                gloAuditTrail.gloAuditTrail.UpdateLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SetRecognition Started at Messages Load", gloAuditTrail.ActivityOutCome.Success)
                'UpdateVoiceLog("------------------------SetRecognition Started at Messages Load----------------------")
                oMDIParentVoice.Vcmd.ExecuteScript("SetRecognitionMode 0", 0)
                gloAuditTrail.gloAuditTrail.UpdateLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SetRecognition Completed at Messages Load", gloAuditTrail.ActivityOutCome.Success)
                'UpdateVoiceLog("------------------------SetRecognition Completed at Messages Load----------------------")

                'oMDIParentVoice = Nothing
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "Error Initializing Voice in Patient Messages load " & ex.Message, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                'UpdateVoiceLog("-------------------------Error Initializing Voice in Patient Messages load " & ex.Message & " ----------------------")
            End Try
        End If

    End Sub
    ''' <summary>
    ''' Uninitialise the voice menus and voice command object
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UnInitializeVoiceComponents()
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            Try
                gloAuditTrail.gloAuditTrail.UpdateLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Close, "Mic Switched On/Off started at " & mMessageName & " Closed Event", gloAuditTrail.ActivityOutCome.Success)
                'UpdateVoiceLog("------------------------Mic Switched On/Off started at " & mMessageName  & " Closed Event----------------------")

                If oMDIParentVoice.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                    oMDIParentVoice.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                End If

                gloAuditTrail.gloAuditTrail.UpdateLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Close, "Mic Switched On/Off Completed at " & mMessageName & " Closed Event", gloAuditTrail.ActivityOutCome.Success)
                'UpdateVoiceLog("------------------------Mic Switched On/Off Completed at " & mMessageName  & " Closed Event----------------------")

                'CType(Me.MdiParent, MainMenu).tlbbtn_Microphone.Visible = False

                If Not IsNothing(vVoiceMenu) Then
                    gloAuditTrail.gloAuditTrail.UpdateLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Close, "VoiceMenu Destruction started at " & mMessageName & " Closed Event", gloAuditTrail.ActivityOutCome.Success)
                    'UpdateLog("------------------------VoiceMenu Destruction started at " & mMessageName  & " Closed Event----------------------")
                    vVoiceMenu.Remove(1)
                    vVoiceMenu.Remove(2)
                    vVoiceMenu.Active = False
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(vVoiceMenu)
                    vVoiceMenu = Nothing
                    gloAuditTrail.gloAuditTrail.UpdateLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Close, "VoiceMenu Destruction Completed at " & mMessageName & " Closed Event", gloAuditTrail.ActivityOutCome.Success)
                    'UpdateVoiceLog("------------------------VoiceMenu Destruction Completed at " & mMessageName  & " Closed Event----------------------")
                End If


            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                gloAuditTrail.gloAuditTrail.ExceptionLog(meActivityModule, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Close, "Error Disabling Voice at " & mMessageName & " Closed Event " & ex.Message, gloAuditTrail.ActivityOutCome.Failure)
                'UpdateVoiceLog("------------------------Error Disabling Voice at " & mMessageName  & " Closed Event " & ex.Message & " ----------------------")
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Show microphone button
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        Try


            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                Try

                    'If Not m_IsReadOnly Then
                    mMyWordToolStrip.MyToolStrip.Items("Mic").Visible = True
                    mMyWordToolStrip.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF

                    mMyWordToolStrip.MyToolStrip.ButtonsToHide.Remove(mMyWordToolStrip.MyToolStrip.Items("Mic").Name)

                    'End If

                Catch ex As Exception

                End Try
            Else
                'If Not m_IsReadOnly Then
                mMyWordToolStrip.MyToolStrip.Items("Mic").Visible = False
                If mMyWordToolStrip.MyToolStrip.ButtonsToHide.Contains(mMyWordToolStrip.MyToolStrip.Items("Mic").Name) = False Then
                    mMyWordToolStrip.MyToolStrip.ButtonsToHide.Add(mMyWordToolStrip.MyToolStrip.Items("Mic").Name)
                End If
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)

        End Try
    End Sub
    ''' <summary>
    ''' Hide microphone button
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        Try
            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                Try
                    If oMDIParentVoice.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        oMDIParentVoice.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        mMyWordToolStrip.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
                    End If

                Catch ex As Exception

                End Try
            Else
                'If Not m_IsReadOnly Then
                mMyWordToolStrip.MyToolStrip.Items("Mic").Visible = False
                If mMyWordToolStrip.MyToolStrip.ButtonsToHide.Contains(mMyWordToolStrip.MyToolStrip.Items("Mic").Name) = False Then
                    mMyWordToolStrip.MyToolStrip.ButtonsToHide.Add(mMyWordToolStrip.MyToolStrip.Items("Mic").Name)
                End If
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try


    End Sub

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                mVoiceCommandsCol = Nothing
                mTemplateCommandsCol = Nothing
                mVoicecol = Nothing
                mTemplatecol = Nothing
                mMyWordToolStrip = Nothing
                mMessageName = Nothing
                DelWordToolStripClick = Nothing
                DelTreeViewDoubleClick = Nothing
                oMDIParentVoice = Nothing
                meActivityModule = Nothing
                enVoiceAddendum = Nothing
                oTreeView = Nothing
                trvSearchNode = Nothing
                ' TODO: free managed resources when explicitly called

            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            'Return Me.MyParent
            Return MyMDIParent
        End Get
    End Property
    Private Function AddVoiceCommandsForAddendum() As Hashtable
        'OrdersVoicecol.Clear()
        'OrdersVoicecol.Add("Save Orders")
        'OrdersVoicecol.Add("Print Orders")
        'OrdersVoicecol.Add("Fax Orders")
        'OrdersVoicecol.Add("Save and Close")
        'OrdersVoicecol.Add("Save and Close Orders")
        'OrdersVoicecol.Add("Insert Signature")
        'OrdersVoicecol.Add("Close Orders")
        'OrdersVoicecol.Add("Finish Orders")
        'OrdersVoicecol.Add("Orders")
        'OrdersVoicecol.Add("Radiology Orders")
        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Addendum", "Save")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Addendum", "Close")
        Return oHashtable

    End Function
End Class
