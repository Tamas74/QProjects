Imports System.Data.SqlClient
Public Class clsVoiceTrainingDocument
    Dim _blnTemplate As Boolean
    Dim _blnPatientExam As Boolean
    Dim _blnPatientHistory As Boolean
    Dim _blnPatientROS As Boolean
    Dim _blnPrescriptionMedication As Boolean
    Dim _blnOrders As Boolean

    Public Property Template() As Boolean
        Get
            Return _blnTemplate
        End Get
        Set(ByVal Value As Boolean)
            _blnTemplate = Value
        End Set
    End Property
    Public Property PatientExam() As Boolean
        Get
            Return _blnPatientExam
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientExam = Value
        End Set
    End Property
    Public Property PatientHistory() As Boolean
        Get
            Return _blnPatientHistory
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientHistory = Value
        End Set
    End Property
    Public Property PatientROS() As Boolean
        Get
            Return _blnPatientROS
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientROS = Value
        End Set
    End Property
    Public Property PrescriptionMedication() As Boolean
        Get
            Return _blnPrescriptionMedication
        End Get
        Set(ByVal Value As Boolean)
            _blnPrescriptionMedication = Value
        End Set
    End Property
    Public Property Orders() As Boolean
        Get
            Return _blnOrders
        End Get
        Set(ByVal Value As Boolean)
            _blnOrders = Value
        End Set
    End Property


    Public Function GenerateDocument() As String
        Dim strCommandText As String = ""
        If _blnTemplate = True Then
            strCommandText = strCommandText & Generate_CommandText("Template")
        End If
        If _blnPatientExam = True Then
            strCommandText = strCommandText & Generate_CommandText("Patient Exam")
        End If
        If _blnPatientHistory = True Then
            strCommandText = strCommandText & Generate_CommandText("History")
        End If
        If _blnPatientROS = True Then
            strCommandText = strCommandText & Generate_CommandText("Patient ROS")
        End If
        If _blnPrescriptionMedication = True Then
            strCommandText = strCommandText & Generate_CommandText("Prescription-Medication")
        End If
        If _blnOrders = True Then
            strCommandText = strCommandText & Generate_CommandText("Orders")
        End If
        Return strCommandText

    End Function
    Private Function Generate_CommandText(ByVal strDocumentName As String) As String
        Dim strCommandText As String = ""
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_VoiceTRainingText"

        Dim objParaCommand As New SqlParameter
        With objParaCommand
            .ParameterName = "@DocumentName"
            .Value = strDocumentName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCommand)


        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                strCommandText = strCommandText & objSQLDataReader.Item(0) & vbCrLf
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return strCommandText
    End Function

End Class
