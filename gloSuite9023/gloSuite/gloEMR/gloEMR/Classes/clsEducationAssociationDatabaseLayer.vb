Imports System.Data.SqlClient
Imports gloDatabaseLayer

Public Class clsEducationAssociationDatabaseLayer
    Implements IDisposable

    Private strConnectionString As String = Nothing

    Public Enum Codes
        SNOMED = 0
        ICD9 = 1
        NDC = 2
        LOINC = 3
        ICD10 = 4 ''added for ICD10 implementation
    End Enum

    Public Enum Template
        PatientEducationMaterial = 0
        ProviderReferenceMaterial = 1
    End Enum

    Public Sub New()
        strConnectionString = GetConnectionString()        
    End Sub

    Public Function GetTemplates(ByVal eTemplate As Template) As DataTable
        Dim returnedDataTable As DataTable = Nothing
        Dim clsEducationDatabaseLayer As New DBLayer(strConnectionString)
        Dim _strQueryWhere As String = ""

        Try
            ' clsEducationDatabaseLayer = New DBLayer(strConnectionString)

            If eTemplate = Template.PatientEducationMaterial Then
                '_strQueryWhere = "Patient Reference Material"
                _strQueryWhere = "MU Patient Education"                
            ElseIf eTemplate = Template.ProviderReferenceMaterial Then
                '_strQueryWhere = "Provider Reference Material"
                _strQueryWhere = "MU Provider Reference"
            End If
            clsEducationDatabaseLayer.Connect(False)
            clsEducationDatabaseLayer.Retrive_Query("select nTemplateID, sTemplateName from TemplateGallery_MST where sCategoryName = '" + _strQueryWhere + "'", returnedDataTable)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If clsEducationDatabaseLayer IsNot Nothing Then
                clsEducationDatabaseLayer.Disconnect()
                clsEducationDatabaseLayer.Dispose()
                clsEducationDatabaseLayer = Nothing
            End If
            _strQueryWhere = ""
        End Try

        Return returnedDataTable

    End Function

    Public Function GetCodes(ByVal eCodes As Codes, Optional ByVal nSearchType As Integer = 1, Optional ByVal SearchString As String = "") As DataTable

        Dim returnedDataTable As New DataTable
        Dim clsEducationDatabaseLayer As DBLayer = Nothing
        Dim DBParameterCollection As DBParameters = Nothing

        Try
            clsEducationDatabaseLayer = New DBLayer(strConnectionString)
            DBParameterCollection = New DBParameters()
            DBParameterCollection.Add(New DBParameter("@FetchType", nSearchType, ParameterDirection.Input, SqlDbType.Int))
            If eCodes = Codes.ICD9 Then
                DBParameterCollection.Add(New DBParameter("@SearchString", SearchString, ParameterDirection.Input, SqlDbType.VarChar))
                DBParameterCollection.Add(New DBParameter("@nICDRevision", gloGlobal.gloICD.CodeRevision.ICD9, ParameterDirection.Input, SqlDbType.SmallInt))
            End If
            If eCodes = Codes.ICD10 Then ''added for ICD10 implementation
                DBParameterCollection.Add(New DBParameter("@SearchString", SearchString, ParameterDirection.Input, SqlDbType.VarChar))
                DBParameterCollection.Add(New DBParameter("@nICDRevision", gloGlobal.gloICD.CodeRevision.ICD10, ParameterDirection.Input, SqlDbType.SmallInt))
            End If
            clsEducationDatabaseLayer.Connect(False)

            If eCodes = Codes.ICD9 Then
                clsEducationDatabaseLayer.Retrive("Education_FillICD9", DBParameterCollection, returnedDataTable)
            ElseIf eCodes = Codes.ICD10 Then
                clsEducationDatabaseLayer.Retrive("Education_FillICD9", DBParameterCollection, returnedDataTable)
            ElseIf eCodes = Codes.NDC Then
                clsEducationDatabaseLayer.Retrive("Education_GetDrugs", DBParameterCollection, returnedDataTable)
            ElseIf eCodes = Codes.LOINC Then
                clsEducationDatabaseLayer.Retrive("Education_GetLabCodes", DBParameterCollection, returnedDataTable)
            ElseIf eCodes = Codes.SNOMED Then
                clsEducationDatabaseLayer.Retrive_Query("SELECT DISTINCT sConceptID,0 as nAgeMax,0 AS nAgeMin,0 AS nResourceType,0 AS nSnomedMappingID,0 AS nUserID,0 AS sGender,sSnomedDescription,sSnomedID FROM dbo.Education_Snomed", returnedDataTable)
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If clsEducationDatabaseLayer IsNot Nothing Then
                clsEducationDatabaseLayer.Disconnect()
                clsEducationDatabaseLayer.Dispose()
                clsEducationDatabaseLayer = Nothing
            End If

            If DBParameterCollection IsNot Nothing Then
                DBParameterCollection.Dispose()
                DBParameterCollection = Nothing
            End If

        End Try


        Return returnedDataTable
    End Function

    Public Function GetAssociatedCodes(ByVal eCodes As Codes) As DataTable

        Dim returnedDataTable As New DataTable
        Dim clsEducationDatabaseLayer As DBLayer = Nothing
        Dim DBParameterCollection As DBParameters = Nothing

        Try
            clsEducationDatabaseLayer = New DBLayer(strConnectionString)
            DBParameterCollection = New DBParameters()
            DBParameterCollection.Add(New DBParameter("@CodeSystem", eCodes.GetHashCode, ParameterDirection.Input, SqlDbType.Int))
            clsEducationDatabaseLayer.Connect(False)
            clsEducationDatabaseLayer.Retrive("gsp_GetEducationAssociation", DBParameterCollection, returnedDataTable)
            clsEducationDatabaseLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If clsEducationDatabaseLayer IsNot Nothing Then
                clsEducationDatabaseLayer.Disconnect()
                clsEducationDatabaseLayer.Dispose()
                clsEducationDatabaseLayer = Nothing
            End If

            If DBParameterCollection IsNot Nothing Then
                DBParameterCollection.Dispose()
                DBParameterCollection = Nothing
            End If

        End Try


        Return returnedDataTable

    End Function

    Public Function InsertIntoDatabase(ByVal DeleteDataTable As DataTable, ByVal ICD9DataTable As DataTable, ByVal MedicationDataTable As DataTable, ByVal LabsDataTable As DataTable, ByVal SnomedDataTable As DataTable) As String

        Dim clsEducationDatabaseLayer As New DBLayer(strConnectionString)
        Dim DBParameterCollection As New DBParameters()
        Dim _Result As String = ""
        Dim _oResult As Object = Nothing

        Try
            With DBParameterCollection
                .Add(New DBParameter("@tvpDelete", DeleteDataTable, ParameterDirection.Input, SqlDbType.Structured))
                .Add(New DBParameter("@tvpICD9", ICD9DataTable, ParameterDirection.Input, SqlDbType.Structured))
                .Add(New DBParameter("@tvpSnoMed", SnomedDataTable, ParameterDirection.Input, SqlDbType.Structured))
                .Add(New DBParameter("@tvpMedication", MedicationDataTable, ParameterDirection.Input, SqlDbType.Structured))
                .Add(New DBParameter("@tvpLab", LabsDataTable, ParameterDirection.Input, SqlDbType.Structured))
                .Add(New DBParameter("@sTranResult", _Result, ParameterDirection.Output, SqlDbType.VarChar, 1000))

            End With


            clsEducationDatabaseLayer.Connect(False)
            clsEducationDatabaseLayer.Execute("SavePatientEducation_TVP", DBParameterCollection, _oResult)

            If _oResult IsNot Nothing Then
                If Convert.ToString(_oResult) = "Successfully Inserted" Then
                    _Result = "Successfully Saved"
                Else
                    _Result = "Association Not Saved"
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Convert.ToString(_oResult), False)
                End If
            Else
                _Result = "Association Not Saved"
            End If

            Return _Result

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            _Result = "Association Not Saved"
            Return _Result
        Finally
            If clsEducationDatabaseLayer IsNot Nothing Then
                clsEducationDatabaseLayer.Disconnect()
                clsEducationDatabaseLayer.Dispose()
                clsEducationDatabaseLayer = Nothing
            End If

            If DBParameterCollection IsNot Nothing Then
                DBParameterCollection.Dispose()
                DBParameterCollection = Nothing
            End If

            _Result = ""
            _oResult = Nothing

        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                strConnectionString = Nothing
                End If
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.

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
