Imports System.Runtime.InteropServices
Module mdlHotkey
    Interface IHotKey
        Sub Navigate(ByVal strstring As String)
    End Interface
End Module

Public Class HotKeyPressedEventArgs
    Inherits EventArgs
    Private m_hotKey As HotKey

    Public ReadOnly Property HotKey()
        Get
            HotKey = m_hotKey
        End Get
    End Property

    Friend Sub New(ByVal hotKey As HotKey)
        m_hotKey = hotKey
    End Sub

End Class

Public Class HotKeyCollection
    Inherits System.Collections.CollectionBase

    Private ownerForm As System.Windows.Forms.Form
    
    <DllImport("kernel32.dll")> _
    Private Shared Function ProcessIdToSessionId(ByVal dwProcessId As UInteger, ByRef pSessionId As UInteger) As Boolean
    End Function
    Protected Overrides Sub OnClear()
        Dim htk As HotKey
        For Each htk In Me.InnerList
            RemoveHotKey(htk)
        Next
        MyBase.OnClear()
    End Sub

    Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal item As Object)
        ' validate item is a hot key:
        Dim htk As HotKey = New HotKey
        If (item.GetType().IsInstanceOfType(htk)) Then
            ' check if the name, keycode and modifiers have been set up:
            htk = item
            ' throws ArgumentException if there is a problem:
            htk.Validate()
            ' throws Unable to add HotKeyException:
            AddHotKey(htk)
            ' ok
            MyBase.OnInsert(index, item)
        Else
            Throw New InvalidCastException("Invalid object.")
        End If

    End Sub
    Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal item As Object)
        ' get the item to be removed:
        Dim htk As HotKey = item
        RemoveHotKey(htk)
        MyBase.OnRemove(index, item)
    End Sub

    Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldItem As Object, ByVal newItem As Object)
        ' remove old hot key:
        Dim htk As HotKey = oldItem
        RemoveHotKey(htk)

        ' add new hotkey:
        htk = newItem
        AddHotKey(htk)

        MyBase.OnSet(index, oldItem, newItem)
    End Sub

    Protected Overrides Sub OnValidate(ByVal item As Object)
        Dim htk As HotKey = item
        htk.Validate()
    End Sub

    Public Sub Add(ByVal hotKey As HotKey)
        ' throws argument exception:
        hotKey.Validate()
        ' throws unable to add hot key exception:
        AddHotKey(hotKey)
        ' assuming all is well:
        Me.InnerList.Add(hotKey)
    End Sub

    Default Public ReadOnly Property Item(ByVal index As Integer) As Integer
        Get
            Item = Me.InnerList.Item(index)
        End Get
    End Property

    Private Sub RemoveHotKey(ByVal hotKey As HotKey)
        '// remove the hot key:
        Dim ret As Boolean = UnmanagedMethods.UnregisterHotKey(ownerForm.Handle, hotKey.AtomId.ToInt32())
        Dim myerror As Integer = Marshal.GetLastWin32Error()
        If ret <> 0 Then
            UnmanagedMethods.GlobalDeleteAtom(hotKey.AtomId)
        End If
        '// unregister the atom:

    End Sub

    Private Function HotKeyFound(ByVal atomName As String, ByRef myfindAtom As IntPtr) As Boolean
        Dim _isHotKeyRegistered As Boolean = False
        myfindAtom = UnmanagedMethods.GlobalFindAtom(atomName)
        If myfindAtom.Equals(IntPtr.Zero) Then
            _isHotKeyRegistered = False
        Else
            _isHotKeyRegistered = True
        End If
        Return _isHotKeyRegistered
    End Function

    Private Sub getProcessIdToSessionId(ByRef myApplicationProcessId As UInteger, ByRef myApplicationSessionId As UInteger, ByRef myApplicationName As String)
        Dim currentProcess As System.Diagnostics.Process = System.Diagnostics.Process.GetCurrentProcess()
        myApplicationProcessId = CUInt(currentProcess.Id)
        myApplicationName = IO.Path.GetFileName(Application.ExecutablePath).Replace(".EXE", "")
        ProcessIdToSessionId(myApplicationProcessId, myApplicationSessionId)
    End Sub

    Private Sub AddHotKey(ByVal hotKey As HotKey)
        
        ''Variable decalaration 
        Dim _isAlreadyExists As Boolean = False
        Dim myApplicationName As String = ""
        Dim myApplicationProcessId As UInteger = 0
        Dim myApplicationSessionId As UInteger = 0
        getProcessIdToSessionId(myApplicationSessionId, myApplicationSessionId, myApplicationName)
        Dim myAtomName As String = myApplicationSessionId.ToString() + "_" + myApplicationProcessId.ToString() + "_" + myApplicationName + "_" + hotKey.Name + "_"
        Dim myKeyCode As Integer = hotKey.KeyCode
        Dim myKeyModifier As Integer = hotKey.Modifiers
        Dim id As IntPtr = 0

        Dim atomName As String = myAtomName + "_" + myKeyCode.ToString() + "_" + myKeyModifier.ToString()
        If (atomName.Length > 255) Then
            atomName = atomName.Substring(0, 255)
        End If
        If (HotKeyFound(atomName, id)) Then
            _isAlreadyExists = True
        Else
            id = UnmanagedMethods.GlobalAddAtom(atomName)
        End If
        If (id.Equals(IntPtr.Zero)) Then
            ' failed
            Throw New HotKeyAddException("Failed to add GlobalAtom for HotKey")
        Else
            ' succeeded:
            Dim ret As Boolean = UnmanagedMethods.RegisterHotKey( _
              ownerForm.Handle, _
              id.ToInt32(), _
              hotKey.Modifiers, _
              hotKey.KeyCode)
            Dim myerror As Integer = Marshal.GetLastWin32Error()
            If Not _isAlreadyExists Then
                If Not (ret) Then
                    ' Remove the atom:
                    UnmanagedMethods.GlobalDeleteAtom(id)
                    ' failed
                    Throw New HotKeyAddException("Failed to register HotKey : " + myerror.ToString())

                Else
                    hotKey.AtomName = atomName
                    hotKey.AtomId = id
                End If
            Else
                hotKey.AtomName = atomName
                hotKey.AtomId = id
            End If

        End If
    End Sub


    Public Sub New(ByVal ownerForm As System.Windows.Forms.Form)
        Me.ownerForm = ownerForm
    End Sub

End Class

Public Class HotKeyAddException
    Inherits System.Exception

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As System.Exception)
        MyBase.New(message, innerException)
    End Sub
End Class

Public Class HotKey
    '[Flags]
    Public Enum HotKeyModifiers As Integer
        MOD_NONE = &H0 ' Added new enaum on 09/18/2006 - Pravin
        MOD_ALT = &H1
        MOD_CONTROL = &H2
        MOD_SHIFT = &H4
        MOD_WIN = &H8
        MOD_ALT_SHIFT = &H1 + &H4           ''For the alt and shift key (Modifier).''dhruv 2010232010
    End Enum
    Private m_name As String
    Private m_atomName As String
    Private m_atomId As IntPtr
    Private m_keyCode As Keys
    Private m_modifiers As HotKeyModifiers

    Friend Property AtomId() As IntPtr
        Get
            AtomId = m_atomId
        End Get
        Set(ByVal Value As IntPtr)
            m_atomId = Value
        End Set
    End Property

    Friend Property AtomName() As String
        Get
            AtomName = m_atomName
        End Get
        Set(ByVal Value As String)
            m_atomName = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Name = m_name
        End Get
        Set(ByVal Value As String)
            m_name = Value
        End Set
    End Property

    Public Property KeyCode() As Keys
        Get
            KeyCode = m_keyCode
        End Get
        Set(ByVal Value As Keys)
            m_keyCode = Value
        End Set
    End Property

    Public Property Modifiers() As HotKeyModifiers
        Get
            Modifiers = m_modifiers
        End Get
        Set(ByVal Value As HotKeyModifiers)
            m_modifiers = Value
        End Set
    End Property

    Public Sub Validate()
        Dim msg As String = ""
        'If (Name Is Null) Then
        'msg = "Name parameter cannot be null"
        'End If
        If (m_name.Trim().Length = 0) Then
            msg = "Name parameter cannot be zero length"
        End If
        If ((KeyCode = Keys.Alt) Or _
         (KeyCode = Keys.Control) Or _
         (KeyCode = Keys.Shift) Or _
         (KeyCode = Keys.ShiftKey) Or _
         (KeyCode = Keys.ControlKey)) Then
            msg = "KeyCode cannot be set to a modifier key"
        End If
        If (msg.Length > 0) Then
            Throw New ArgumentException(msg)
        End If
    End Sub

    Public Sub New()

    End Sub

    Public Sub New( _
        ByVal name As String, _
        ByVal keyCode As Keys, _
        ByVal modifiers As HotKeyModifiers _
        )
        m_name = name
        m_keyCode = keyCode
        m_modifiers = modifiers
    End Sub

End Class
Friend Class UnmanagedMethods

    Friend Const IDHOT_SNAPWINDOW As Integer = -1 '/* SHIFT-PRINTSCRN  */
    Friend Const IDHOT_SNAPDESKTOP As Integer = -2         '/* PRINTSCRN        */
    Friend Const WM_HOTKEY As Integer = &H312
    Friend Const WM_PARENTNOTIFY As Integer = &H210
    Friend Const WM_LBUTTONDOWN As Integer = &H201

    Friend Const WM_ACTIVATEAPP As Integer = &H1C
    
    Public Declare Auto Function GlobalFindAtom Lib "kernel32" _
        (ByVal lpString As String _
        ) As IntPtr

    Public Declare Auto Function RegisterHotKey Lib "user32" _
        (ByVal hWnd As IntPtr, _
        ByVal id As Integer, _
        ByVal fsModifiers As Integer, _
        ByVal vk As Integer _
        ) As Boolean
    Public Declare Auto Function UnregisterHotKey Lib "user32" _
        (ByVal hWnd As IntPtr, _
        ByVal id As Integer _
        ) As Boolean
    Public Declare Auto Function GlobalAddAtom Lib "kernel32" _
        (ByVal lpString As String _
        ) As IntPtr
    Public Declare Auto Function GlobalDeleteAtom Lib "kernel32" _
        (ByVal nAtom As IntPtr _
        ) As IntPtr
    Public Declare Auto Function GetTickCount Lib "kernel32" () As Integer
    Public Declare Auto Function SendMessage Lib "user32" _
        (ByVal hWnd As IntPtr, _
        ByVal wMsg As Integer, _
        ByVal wParam As Integer, _
        ByVal lParam As IntPtr _
        ) As Integer
    Friend Const WM_SYSCOMMAND As Integer = &H112
    Friend Const SC_RESTORE As Integer = &HF120

    Public Declare Auto Function IsIconic Lib "user32" _
        (ByVal hWnd As IntPtr) As Boolean
    Public Declare Auto Function IsWindowVisible Lib "user32" _
        (ByVal hWnd As IntPtr) As Boolean
    Public Declare Auto Function SetForegroundWindow Lib "user32" _
        (ByVal hWnd As IntPtr) As Boolean
    Public Declare Auto Function ShowWindow Lib "user32" _
        (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Integer
    Friend Const SW_SHOW As Integer = 5




End Class




