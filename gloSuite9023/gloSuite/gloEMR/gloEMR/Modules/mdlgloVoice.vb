Public Delegate Sub HandleWordToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
Public Delegate Sub HandleTreeviewDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)

Module mdlgloVoice

    Interface gloVoice
        Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings)
        Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings)
        Sub AddVoiceCommands()
        Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent)
        Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent)
        Sub ShowMicroPhone()
        Sub TurnoffMicrophone()
        ReadOnly Property MyParent() As MainMenu
    End Interface

    Interface IExamChildEvents
        Inherits gloVoice
        Event ActivateExamChild(ByVal frmExamChild As gloVoice)
        Event DeActivateExamChild(ByVal frmExamChild As gloVoice)
        Property Handle() As Int32
    End Interface

    Private mMyMDIParent As MainMenu
    Public Property MyMDIParent() As MainMenu
        Get
            Return mMyMDIParent
        End Get
        Set(ByVal value As MainMenu)
            mMyMDIParent = value
        End Set
    End Property

End Module
