Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO
Imports System.Text

Public Class cls_BAL_MU_Detail_Report
    Dim _databaseConnectionString As String = ""

    Public Function GetDetailReportingDataSet(ByVal _sPName As String, ByVal _providerID As DataTable, ByVal _startDate As String, ByVal _endDate As String, ByVal _SingleProvider As Int64, ByVal TIN As String, ByVal IsReportingYear As Boolean) As DataSet

        Dim oDAL_MU_Detail_Report As New cls_DAL_MU_Detail_Report(_databaseConnectionString)
        Dim Ds As DataSet = Nothing

        If (_sPName = "MU_DIChecks" Or _sPName = "MU_FormularyChecks") Then
            Ds = oDAL_MU_Detail_Report.Getdata(_sPName)
        Else
            Ds = oDAL_MU_Detail_Report.GetdataWithParam(_sPName, _providerID, _startDate, _endDate, _SingleProvider, TIN, IsReportingYear)
        End If

        Return Ds

    End Function

    Public Function GetDetailCQMReportingDataSet(ByVal _sPName As String, ByVal _providerID As Int64, ByVal _startDate As String, ByVal _endDate As String, ByVal _extraParameter As Int16) As DataSet
        Dim oDAL_MU_Detail_Report As New cls_DAL_MU_Detail_Report(_databaseConnectionString)
        Dim Ds As DataSet = Nothing

        If _sPName = "MU_NQF0002" Or _sPName = "MU_NQF0014" Or _sPName = "MU_NQF0028b" Or _sPName = "MU_NQF0033_POP1" Or _sPName = "MU_NQF0033_POP2" Or _sPName = "MU_NQF0033_POP3" Or _sPName = "MU_NQF0061" Or _sPName = "MU_NQF0062" Or _sPName = "MU_NQF0064_Numerator1" Or _sPName = "MU_NQF0064_Numerator2" Or _sPName = "MU_NQF0067" Or _sPName = "MU_NQF0068" Or _sPName = "MU_NQF0070" Or _sPName = "MU_NQF0074" Or _sPName = "MU_NQF0081" Or _sPName = "MU_NQF0083" Or _sPName = "MU_NQF0084" Or _sPName = "MU_NQF0575" Then
            Ds = oDAL_MU_Detail_Report.RetriveDataFromMultipleServer(_sPName, _providerID, _startDate, _endDate, _extraParameter, "")
        ElseIf _sPName = "MU_NQF0038" Or _sPName = "MU_NQF0041" Then
            Ds = oDAL_MU_Detail_Report.RetriveData_0038and41(_sPName, _providerID, _startDate, _endDate, _extraParameter, "")
        Else
            Ds = oDAL_MU_Detail_Report.GetdataWithExtraParam(_sPName, _providerID, _startDate, _endDate, _extraParameter)
        End If

        Return Ds
    End Function

    Public Function GetDenAndNumListByIDS(ByVal _denIDS As String, ByVal _numIDS As String, Optional ByVal DenominatorExclusionsPatientID As String = "", Optional ByVal DenominatorExceptionsPatientID As String = "") As DataSet
        Dim oDAL_MU_Detail_Report As New cls_DAL_MU_Detail_Report(_databaseConnectionString)

        Dim dsDeo As New DataSet
        Dim dsNum As New DataSet

        Dim dsDenominatorExclusions As New DataSet
        Dim dsDenominatorExceptions As New DataSet


        Dim ds As DataSet = Nothing

        Dim _objectArray As String() = New String(999) {}
        Dim xmlSerializer As New XmlSerializer(_objectArray.[GetType]())
        Dim writer As New StringWriter()
        Dim reader As New StringReader("")

        Try

            If (_denIDS = String.Empty) Then
                _denIDS = "0"
            End If

            If (_numIDS = String.Empty) Then
                _numIDS = "0"
            End If

            If (_denIDS = "0" And _numIDS = "0") Then
                Return ds
            End If

            If (DenominatorExclusionsPatientID = "") Then
                DenominatorExclusionsPatientID = "0"
            End If

            If (DenominatorExceptionsPatientID = "") Then
                DenominatorExceptionsPatientID = "0"
            End If

            _objectArray = Nothing
            _objectArray = _denIDS.Split(","c)
            xmlSerializer = New XmlSerializer(_objectArray.[GetType]())
            writer = New StringWriter()
            xmlSerializer.Serialize(writer, _objectArray)
            reader = New StringReader(writer.ToString())
            dsDeo.ReadXml(reader)
            dsDeo.Tables(0).TableName = "TVP_DenIDS"
            dsDeo.Tables("TVP_DenIDS").Columns(0).ColumnName = "nPatientID"


            _objectArray = Nothing
            _objectArray = _numIDS.Split(","c)
            xmlSerializer = New XmlSerializer(_objectArray.[GetType]())
            writer = New StringWriter()
            xmlSerializer.Serialize(writer, _objectArray)
            reader = New StringReader(writer.ToString())
            dsNum.ReadXml(reader)
            dsNum.Tables(0).TableName = "TVP_NumIDS"
            dsNum.Tables("TVP_NumIDS").Columns(0).ColumnName = "nPatientID"


            _objectArray = Nothing
            _objectArray = DenominatorExclusionsPatientID.Split(","c)
            xmlSerializer = New XmlSerializer(_objectArray.[GetType]())
            writer = New StringWriter()
            xmlSerializer.Serialize(writer, _objectArray)
            reader = New StringReader(writer.ToString())
            dsDenominatorExclusions.ReadXml(reader)
            dsDenominatorExclusions.Tables(0).TableName = "TVP_DenominatorExclusionsPatientID"
            dsDenominatorExclusions.Tables("TVP_DenominatorExclusionsPatientID").Columns(0).ColumnName = "nPatientID"

            _objectArray = Nothing
            _objectArray = DenominatorExceptionsPatientID.Split(","c)
            xmlSerializer = New XmlSerializer(_objectArray.[GetType]())
            writer = New StringWriter()
            xmlSerializer.Serialize(writer, _objectArray)
            reader = New StringReader(writer.ToString())
            dsDenominatorExceptions.ReadXml(reader)
            dsDenominatorExceptions.Tables(0).TableName = "TVP_DenominatorExceptionsPatientID"
            dsDenominatorExceptions.Tables("TVP_DenominatorExceptionsPatientID").Columns(0).ColumnName = "nPatientID"

            ds = oDAL_MU_Detail_Report.GetPatientName(dsDeo.Tables("TVP_DenIDS"), dsNum.Tables("TVP_NumIDS"), dsDenominatorExclusions.Tables("TVP_DenominatorExclusionsPatientID"), dsDenominatorExceptions.Tables("TVP_DenominatorExceptionsPatientID"))

        Catch ex As Exception
            'Throw ex
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
                reader = Nothing
            End If
            If Not IsNothing(writer) Then
                writer.Dispose()
                writer = Nothing
            End If
            If Not IsNothing(_objectArray) Then
                _objectArray = Nothing
            End If
            If Not IsNothing(dsDeo) Then
                dsDeo.Dispose()
                dsDeo = Nothing
            End If
            If Not IsNothing(dsNum) Then
                dsNum.Dispose()
                dsNum = Nothing
            End If

            If Not IsNothing(dsDenominatorExclusions) Then
                dsDenominatorExclusions.Dispose()
                dsDenominatorExclusions = Nothing
            End If

            If Not IsNothing(dsDenominatorExceptions) Then
                dsDenominatorExceptions.Dispose()
                dsDenominatorExceptions = Nothing
            End If

        End Try

        Return ds

    End Function



    Public Sub New(ByVal strConnectionString As String)
        _databaseConnectionString = strConnectionString
    End Sub
End Class
