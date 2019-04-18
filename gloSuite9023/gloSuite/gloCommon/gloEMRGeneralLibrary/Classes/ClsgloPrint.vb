Imports System.Windows.Forms
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary.gloEMRActors
Imports System.IO
Namespace gloprintfax
    Interface IgloPrint
        Function PrintToCustomReport()
        Function PrinttoCrystalReport()
    End Interface
    'This Class will be exposed to the Toolbar control in gloEMR forms.
    Public Class gloPrint

        Implements IgloPrint, IDisposable
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Private _Customprint As CustomPrint
        Public Sub New(ByVal objPrintDialog As PrintDialog, ByVal objpnlPresciptionReport As Panel, ByVal strwhereclause As String)
            MyBase.new()
            _Customprint = New CustomPrint(objPrintDialog, objpnlPresciptionReport)
            _Customprint.WhereClause = strwhereclause
        End Sub
        ' IDisposable
        Public Sub New()
            MyBase.new()
        End Sub
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
        Public Function PrinttoCrystalReport() As Object Implements IgloPrint.PrinttoCrystalReport
            Try
                Return Nothing
            Catch ex As PrintException
                Return Nothing
            Catch ex As Exception
                Dim objex As New PrintException
                objex.ErrMessage = "Error Printing to Crystal Report"
                Return Nothing
            End Try
        End Function

        Public Function PrintToCustomReport() As Object Implements IgloPrint.PrintToCustomReport
            Try
                _Customprint.CallCustomPrint()
                Return Nothing
            Catch ex As PrintException
                Return Nothing
            Catch ex As Exception
                Dim objex As New PrintException
                objex.ErrMessage = "Error Printing to Crystal Report"
                Return Nothing
            End Try
        End Function

    End Class
    Public Class gloFax
        Implements IDisposable
        Private disposedValue As Boolean = False        ' To detect redundant calls
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
        Dim _CustomPrint As CustomPrint
        Public Sub New(ByVal objPrintDialog As PrintDialog, ByVal objpnlPresciptionReport As Panel, ByVal strwhereclause As String)
            MyBase.new()
            _CustomPrint = New CustomPrint(objPrintDialog, objpnlPresciptionReport)
            _CustomPrint.WhereClause = strwhereclause
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub
        'commented by dipak as not in use
        'Public Sub FaxToReport(ByVal VisitID As Int64)
        '    Dim _FaxBusinessLayer As New FaxBusinessLayer()
        '    Try

        '        _CustomPrint.CreateXML()

        '        If File.Exists(_CustomPrint.FileName) Then
        '            FaxSettings.SetFAXPrinterDefaultSettings()
        '            'Retrieve the FAX Cover Page details
        '            'Find FAX Parameters
        '            'Get Pharmacy FAX No
        '            'Dim strFAXTo As String
        '            'Dim strFAXNo As String
        '            Dim _ContactInformation As ContactInformation

        '            _ContactInformation = _FaxBusinessLayer.GetPharmacyFAXNo(globalPatient.gnPatientID)

        '            If Not IsNothing(_ContactInformation) Then
        '                globalFax.gstrFAXContactPersonFAXNo = _ContactInformation.Address.Fax
        '                globalFax.gstrFAXContactPerson = _ContactInformation.Name
        '            End If
        '            If Trim(globalFax.gstrFAXContactPerson) = "" Then
        '                globalFax.gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", clsgeneral.gstrMessageBoxCaption)
        '            End If
        '            If globalFax.gblnFAXCoverPage Then
        '                'code has been commented temporarily,not handling logic for cover page in custom print yet.
        '                'If FaxSettings.RetrieveFAXDetails(globalFax.eFAXType, globalPatient.gnPatientID, globalFax.gstrFAXContactPerson, globalFax.gstrFAXContactPersonFAXNo, "", 0, VisitID, 0) = False Then
        '                '    Exit Function
        '                'End If
        '                'code has been commented temporarily,not handling logic for cover page in custom print yet.
        '            Else
        '                If Trim(globalFax.gstrFAXContactPersonFAXNo) = "" Then
        '                    globalFax.gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", clsgeneral.gstrMessageBoxCaption)
        '                End If
        '            End If
        '            If Trim(globalFax.gstrFAXContactPersonFAXNo) = "" Then
        '                MessageBox.Show("You have not entered the FAX No. So FAX will not be send", clsgeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            End If

        '            'Retrieve FAX Document Name
        '            Dim strFAXDocumentName As String
        '            strFAXDocumentName = FaxSettings.RetrieveFAXDocumentName()
        '            If FaxSettings.SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Sub
        '            Dim objPendingFax As New PendingFax
        '            objPendingFax.PatientID = globalPatient.gnPatientID
        '            objPendingFax.FaxTo = globalFax.gstrFAXContactPerson
        '            objPendingFax.FaxType = globalFax.eFAXType
        '            objPendingFax.FaxNo = globalFax.gstrFAXContactPersonFAXNo
        '            objPendingFax.LoginUser = globalSecurity.gstrLoginName
        '            objPendingFax.FileName = strFAXDocumentName
        '            objPendingFax.FaxDate = Now

        '            _FaxBusinessLayer.AddPendingFAX(objPendingFax, globalFax.CurrentSendingFAXPriority)
        '            _FaxBusinessLayer = Nothing

        '            Dim objDataDictionary As CustomPrintDBLayer
        '            objDataDictionary = New CustomPrintDBLayer
        '            'Print Non Narcotic drugs
        '            _CustomPrint.readXML(True)
        '        Else
        '            MsgBox("Kindly Design the Prescription Report from gloEMR Admin and then Fax Report", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, clsgeneral.gstrMessageBoxCaption & "-Prescription")
        '        End If
        '    Catch ex As FaxException

        '    Catch ex As Exception
        '        Dim objex As New FaxException
        '        objex.ErrMessage = "Error Faxing Report"
        '    Finally
        '        '_CustomPrint.DeleteXML()
        '        _CustomPrint.Dispose()
        '        _CustomPrint = Nothing
        '        _FaxBusinessLayer.Dispose()
        '        _FaxBusinessLayer = Nothing
        '    End Try
        'End Sub

    End Class
End Namespace