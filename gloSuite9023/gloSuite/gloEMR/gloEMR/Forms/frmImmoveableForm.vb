'Added new form for Resolving the case no :GLO2010-0007101
Public Class frmImmoveableForm
    '' Inherits System.Windows.Forms.Form
    Inherits gloAUSLibrary.MasterForm

    Private Declare Function EnableMenuItem Lib "user32.dll" Alias "EnableMenuItem" (ByVal hMenu As IntPtr, ByVal uIDEnableItem As Int32, ByVal uEnable As Int32) As Int32

    Private Const HTCAPTION As Int32 = &H2

    Private Const MF_BYCOMMAND As Int32 = &H0&
    Private Const MF_ENABLED As Int32 = &H0&
    Private Const MF_GRAYED As Int32 = &H1&
    Private Const MF_DISABLED As Int32 = &H2&

    Private Const SC_MOVE As Int32 = &HF010&

    Private Const WM_NCLBUTTONDOWN As Int32 = &HA1
    Private Const WM_SYSCOMMAND As Int32 = &H112
    Private Const WM_INITMENUPOPUP As Int32 = &H117&

    Private bMoveable As Boolean = True

    Public Sub New()
        MyBase.New()
    End Sub

    <System.ComponentModel.Category("Behavior"), System.ComponentModel.Description("Allows the form to be moved")> _
    Public Overridable Property Moveable() As Boolean
        Get
            Return bMoveable
        End Get
        Set(ByVal Value As Boolean)
            If bMoveable <> Value Then
                bMoveable = Value
            End If
        End Set
    End Property

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_INITMENUPOPUP Then
            'handles popup of system menu
            If m.LParam.ToInt32 \ 65536 <> 0 Then 'divide by 65536 to get hiword
                Dim AbleFlags As Int32 = MF_ENABLED
                If Not Moveable Then AbleFlags = MF_DISABLED Or MF_GRAYED
                EnableMenuItem(m.WParam, SC_MOVE, MF_BYCOMMAND Or AbleFlags)
            End If
        End If

        If Not Moveable Then
            If m.Msg = WM_NCLBUTTONDOWN Then
                'cancels any attempt to drag the window by it's caption
                If m.WParam.ToInt32 = HTCAPTION Then
                    Return
                End If
            End If
            If m.Msg = WM_SYSCOMMAND Then
                'redundant but cancels any clicks on the Move system menu item
                If (m.WParam.ToInt32 And &HFFF0) = SC_MOVE Then
                    Return
                End If
            End If
        End If
        'return control to base message handler
        Const WM_NCLBUTTONDBLCLK As Int32 = &HA3
        If m.Msg = WM_NCLBUTTONDBLCLK Then
            Exit Sub
        End If

        MyBase.WndProc(m)
    End Sub

End Class
