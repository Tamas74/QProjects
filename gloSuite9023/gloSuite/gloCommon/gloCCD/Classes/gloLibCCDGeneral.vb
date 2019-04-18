Public Class gloLibCCDGeneral
    '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
    Public Shared bIncludePrivacySection As Boolean = False
    Public Shared bIncludePrivacyText As Boolean = False
    Public Shared sConfidentialityCode As String = "N"
    Public Shared sCDAPrivacyText As String = ""
    Public Shared sCDAPrivacyTitle As String = ""
    Public Shared dvCCDAPatientConsent As New DataView
    Public Shared dvCCDAPatientPrivacyText As New DataView
    Public Shared dvCCDAPatientPurposeofUse As New DataView
    Public Shared dvCCDAPatientPrivacyTextSVal As New DataView
    '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End 
    Public Shared CCDFilePath As String = ""
    Public Shared CCDFileGenerationPath As String = ""
    Public Shared gblnCCDAICD10Transition As Boolean = False
    'Public Shared CCRFilePath As String = "" 'Added by kanchan on 20100610 for CCR
    Public Shared ClinicalDocFileType As String = "" 'Added by kanchan on 20100610 for CCR
    Private Shared sgloCCDApplicationPath As String = ""
    Public Shared Connectionstring As String = ""
    Private Shared oCCDMsgLog As CCDMessageLogDetail
    Public Shared sMmwServerName As String = ""
    Public Shared sMmwDatabaseName As String = ""
    Public Shared gRxNServerName As String = ""
    Public Shared gRxNDatabaseName As String = ""
    Public Shared blnRemovePatientDataSetting As Boolean = False
    Public Shared sDIBServiceURL As String = ""
    Public Shared sCDAValidatorUrl As String = ""
    Public Shared bViewCDAErrors As Boolean = False
    Public Shared bImportRestrictedCCD As Boolean = False



    Public Shared Property gloCCDApplicationPath() As String
        Get
            Return sgloCCDApplicationPath
        End Get
        Set(ByVal value As String)
            sgloCCDApplicationPath = value
        End Set
    End Property
    Public Shared Function GetPrefixTransactionID(ByVal PatientDOB As DateTime) As Long

        Dim strID As String
        Dim dtDate As DateTime
        dtDate = System.DateTime.Now
        strID = DateDiff(DateInterval.Day, CDate("1/1/1800"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1800"), PatientDOB.Date)
        Return CLng(strID)
    End Function

    Public Shared Function GetPrefixTransactionID(ByVal PatientID As Int64) As Long
        Dim strID As String = Nothing
        Dim strPatientID As String
        Dim strPatientTempID As String
        'Randomize(strPatientID)
        Try
            strPatientID = CStr(PatientID)
            If strPatientID.Length >= 15 Then
                strPatientTempID = strPatientID.Substring(4, 1) & strPatientID.Substring(9, 1) & strPatientID.Substring(14, 1)
            Else
                Select Case strPatientID.Length
                    Case 1
                        strPatientTempID = "00" & strPatientID
                    Case 2
                        strPatientTempID = "0" & strPatientID
                    Case Else
                        strPatientTempID = Right(strPatientID, 3)
                End Select
            End If
            Dim dtDate As DateTime
            dtDate = System.DateTime.Now

            strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)
            strID = strID & strPatientTempID.Substring(0, 1)
            strID = strID & DateDiff(DateInterval.Second, dtDate.Date, dtDate)
            strID = strID & strPatientTempID.Substring(1, 1)
            strID = strID & dtDate.Millisecond
            strID = strID & strPatientTempID.Substring(2, 1)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            strPatientID = Nothing
            strPatientTempID = Nothing
        End Try
        Return CLng(strID)
    End Function

    Friend Shared Property CCDMsgObject() As CCDMessageLogDetail
        Get
            If IsNothing(oCCDMsgLog) Then
                oCCDMsgLog = New CCDMessageLogDetail
            End If
            Return oCCDMsgLog
        End Get
        Set(ByVal value As CCDMessageLogDetail)
            If IsNothing(oCCDMsgLog) Then
                oCCDMsgLog = New CCDMessageLogDetail
            End If
            oCCDMsgLog = value
        End Set
    End Property
End Class

Class CCDMessageLogDetail
    Implements IDisposable

    Private _dtDate As String = ""
    Private _nMsgID As String = ""
    Private _sDescription As String = ""
    Public Property Datetime() As String
        Get
            Return _dtDate
        End Get
        Set(ByVal value As String)
            _dtDate = value
        End Set
    End Property
    Public Property MsgID() As String
        Get
            Return _nMsgID
        End Get
        Set(ByVal value As String)
            _nMsgID = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _sDescription
        End Get
        Set(ByVal value As String)
            _sDescription = value
        End Set
    End Property
    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
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

End Class

