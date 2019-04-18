Imports oOffice = Microsoft.Office.Core
Imports Wrd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices
Imports AxDSOFramer

Public Class frmWordPreview
    Inherits System.Windows.Forms.Form

    Private WithEvents oWordApp As Wrd.Application
    Dim objWord As clsWordDocument
    Dim objCriteria As DocCriteria
    Private WithEvents oCurDoc As Wrd.Document

    Dim objDoc As Wrd.Document = Nothing
    Dim glWrd As gloWord.LoadAndCloseWord = Nothing
    Dim sPreviewFileName As String = "" ''''preview filename sent from Preview event

    Dim isFormPrintBtnClicked As Boolean = False ''''some times directly close is clicked without clicking print and still print is going.

    Private blnCopiedbyCCQPrint As Boolean

    Public Property CopiedbyCCQPrint() As Boolean
        Get
            Return blnCopiedbyCCQPrint
        End Get
        Set(ByVal value As Boolean)
            blnCopiedbyCCQPrint = value
        End Set
    End Property

    Public Sub New(ByVal PreviewFileName As String)
        InitializeComponent()
        '\\ Initialise the bits we need to use later
        sPreviewFileName = PreviewFileName

    End Sub
   
    Private Sub frmWordPreview_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
           
            If sPreviewFileName <> "" Then
                ''wdPreview.set_EnableFileCommand(DSOFramer.dsoFileCommandType.dsoFilePropertie, False)
                'Dim ed As Boolean = wdPreview.EditMode
                'pnlWDPreview.Enabled = False
                'Dim rdonly As Boolean = wdPreview.IsReadOnly

                wdPreview.Open(sPreviewFileName)
                oCurDoc = wdPreview.ActiveDocument
                oCurDoc.Protect(Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyReading)
                'LoadWordUserControl(sPreviewFileName)
                'oCurDoc = wdPreview.ActiveDocument

            Else
                ''''invalid file sent for preview
                MessageBox.Show("Unable to show preview of flowsheet", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            If Not IsNothing(oCurDoc) Then
                'oCurDoc.Close()''''gives error
                oCurDoc = Nothing
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

  
    Private Sub tlbtnPrintnCls_Click(sender As System.Object, e As System.EventArgs) Handles tlbtnPrintnCls.Click
        Try
            isFormPrintBtnClicked = True
            oCurDoc = wdPreview.ActiveDocument

            ''CopiedbyCCQPrint = true --means it will NOT Print and stay as it is...
            ''CopiedbyCCQPrint = false --means it will print through RDP print
            blnCopiedbyCCQPrint = gloWord.LoadAndCloseWord.CopyPrintDoc(oCurDoc, 0)
            If Not IsNothing(oCurDoc) Then
                'oCurDoc.Close()''''gives error
                oCurDoc = Nothing
            End If



            If blnCopiedbyCCQPrint Then ''''true means it will NOT print throug CCQ
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Unable or rejected to print flowsheet from flowsheet print preview and clinical queue service", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else ''''it will print through CCQ
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Flow Sheet printed from flowsheet print preview and clinical queue service", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            Me.Close()
        Catch ex As Exception
            If Not IsNothing(oCurDoc) Then
                'oCurDoc.Close()''''gives error
                oCurDoc = Nothing
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        Try

            'objWord = Nothing
            'objWord = New clsWordDocument
         

            oCurDoc = wdPreview.ActiveDocument
            'oWordApp = oCurDoc.Application
            ''  wdSplitControl.Open(strFileName)
            'gloWord.LoadAndCloseWord.OpenDSO(wdPreview, strFileName, oCurDoc, oWordApp)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'Private Sub frmWordPreview_Activated(sender As System.Object, e As System.EventArgs) Handles MyBase.Activated
    '    Try


    '            If Not IsNothing(wdPreview.DocumentName) Then
    '                oCurDoc = wdPreview.ActiveDocument
    '                oWordApp = oCurDoc.Application



    '                RemoveWordHandlers()
    '                AddWordHandlers()
    '                isHandlerRemoved = False

    '                'End If
    '            End If
    '            If (IsNothing(oCurDoc) = False) Then
    '                If (IsNothing(oCurDoc.ActiveWindow) = False) Then
    '                    oCurDoc.ActiveWindow.SetFocus()
    '                End If
    '            End If
    '    Catch ex As Exception
    '        'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "ERROR:At frmPatientLetter_Activated \n" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
    '        ex = Nothing
    '    Finally
    '        If Me.ParentForm IsNot Nothing Then
    '            CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
    '            CType(Me.ParentForm, MainMenu).ActiveDSO = wdPreview
    '        End If
    '    End Try
    'End Sub

    'Private Sub AddWordHandlers()
    '    If (IsNothing(oWordApp) = False) Then
    '        'AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
    '        'AddHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
    '    End If

    'End Sub

    'Private Sub RemoveWordHandlers()
    '    Try
    '        'If (IsNothing(oWordApp) = False) Then
    '        '    RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
    '        '    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
    '        'End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Remove, ex, gloAuditTrail.ActivityOutCome.Failure)
    '        ex = Nothing
    '    End Try
    'End Sub

    Private Sub InitialiseWordObject()
        Try

            objWord = Nothing
            objWord = New clsWordDocument

            objWord.myCallingForm = Me

            objWord.CurDocument = wdPreview.ActiveDocument
        
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try


    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Try
            If isFormPrintBtnClicked = True Then
                blnCopiedbyCCQPrint = blnCopiedbyCCQPrint
            Else
                blnCopiedbyCCQPrint = True ''''this will not call the print event.
            End If

            Me.Close()
        Catch ex As Exception
          If Not IsNothing(oCurDoc) Then
                'oCurDoc.Close()''''gives error
                oCurDoc = Nothing
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(oCurDoc) Then
                'oCurDoc.Close()''''gives error
                oCurDoc = Nothing
            End If
        End Try

    End Sub

  
    
    Private Sub frmWordPreview_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If isFormPrintBtnClicked = True Then
                blnCopiedbyCCQPrint = blnCopiedbyCCQPrint
            Else
                blnCopiedbyCCQPrint = True ''''this will not call the print event.
            End If

            Me.Close()
        Catch ex As Exception
            If Not IsNothing(oCurDoc) Then
                'oCurDoc.Close()''''gives error
                oCurDoc = Nothing
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(oCurDoc) Then
                'oCurDoc.Close()''''gives error
                oCurDoc = Nothing
            End If
        End Try
    End Sub
End Class