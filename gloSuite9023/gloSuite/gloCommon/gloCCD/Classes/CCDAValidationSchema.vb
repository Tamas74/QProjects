Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Public Class CcdaValidationResult
    Public Property actualCode() As String
        Get
            Return m_actualCode
        End Get
        Set(value As String)
            m_actualCode = Value
        End Set
    End Property
    Private m_actualCode As String
    Public Property actualCodeSystem() As String
        Get
            Return m_actualCodeSystem
        End Get
        Set(value As String)
            m_actualCodeSystem = Value
        End Set
    End Property
    Private m_actualCodeSystem As String
    Public Property actualCodeSystemName() As String
        Get
            Return m_actualCodeSystemName
        End Get
        Set(value As String)
            m_actualCodeSystemName = Value
        End Set
    End Property
    Private m_actualCodeSystemName As String
    Public Property actualDisplayName() As String
        Get
            Return m_actualDisplayName
        End Get
        Set(value As String)
            m_actualDisplayName = Value
        End Set
    End Property
    Private m_actualDisplayName As String
    Public Property dataTypeSchemaError() As Boolean
        Get
            Return m_dataTypeSchemaError
        End Get
        Set(value As Boolean)
            m_dataTypeSchemaError = Value
        End Set
    End Property
    Private m_dataTypeSchemaError As Boolean
    Public Property description() As String
        Get
            Return m_description
        End Get
        Set(value As String)
            m_description = Value
        End Set
    End Property
    Private m_description As String
    Public Property documentLineNumber() As String
        Get
            Return m_documentLineNumber
        End Get
        Set(value As String)
            m_documentLineNumber = Value
        End Set
    End Property
    Private m_documentLineNumber As String
    Public Property igissue() As Boolean
        Get
            Return m_igissue
        End Get
        Set(value As Boolean)
            m_igissue = Value
        End Set
    End Property
    Private m_igissue As Boolean
    Public Property muissue() As Boolean
        Get
            Return m_muissue
        End Get
        Set(value As Boolean)
            m_muissue = Value
        End Set
    End Property
    Private m_muissue As Boolean
    Public Property schemaError() As Boolean
        Get
            Return m_schemaError
        End Get
        Set(value As Boolean)
            m_schemaError = Value
        End Set
    End Property
    Private m_schemaError As Boolean
    Public Property type() As String
        Get
            Return m_type
        End Get
        Set(value As String)
            m_type = Value
        End Set
    End Property
    Private m_type As String
    Public Property validatorConfiguredXpath() As String
        Get
            Return m_validatorConfiguredXpath
        End Get
        Set(value As String)
            m_validatorConfiguredXpath = Value
        End Set
    End Property
    Private m_validatorConfiguredXpath As String
    Public Property xPath() As String
        Get
            Return m_xPath
        End Get
        Set(value As String)
            m_xPath = Value
        End Set
    End Property
    Private m_xPath As String
End Class

Public Class ResultMetaData
    Public Property count() As Integer
        Get
            Return m_count
        End Get
        Set(value As Integer)
            m_count = Value
        End Set
    End Property
    Private m_count As Integer
    Public Property type() As String
        Get
            Return m_type
        End Get
        Set(value As String)
            m_type = Value
        End Set
    End Property
    Private m_type As String
End Class

Public Class ResultsMetaData
    Public Property ccdaDocumentType() As String
        Get
            Return m_ccdaDocumentType
        End Get
        Set(value As String)
            m_ccdaDocumentType = Value
        End Set
    End Property
    Private m_ccdaDocumentType As String
    Public Property ccdaFileContents() As String
        Get
            Return m_ccdaFileContents
        End Get
        Set(value As String)
            m_ccdaFileContents = Value
        End Set
    End Property
    Private m_ccdaFileContents As String
    Public Property ccdaFileName() As String
        Get
            Return m_ccdaFileName
        End Get
        Set(value As String)
            m_ccdaFileName = Value
        End Set
    End Property
    Private m_ccdaFileName As String
    Public Property resultMetaData() As List(Of ResultMetaData)
        Get
            Return m_resultMetaData
        End Get
        Set(value As List(Of ResultMetaData))
            m_resultMetaData = Value
        End Set
    End Property
    Private m_resultMetaData As List(Of ResultMetaData)
    Public Property serviceError() As Boolean
        Get
            Return m_serviceError
        End Get
        Set(value As Boolean)
            m_serviceError = Value
        End Set
    End Property
    Private m_serviceError As Boolean
    Public Property serviceErrorMessage() As String
        Get
            Return m_serviceErrorMessage
        End Get
        Set(value As String)
            m_serviceErrorMessage = Value
        End Set
    End Property
    Private m_serviceErrorMessage As String
End Class

Public Class RootObject
    Public Property Validation As validation
        Get
            Return m__validation
        End Get
        Set(value As validation)
            m__validation = value
        End Set

    End Property


    Public Property html As String
        Get
            Return m_html
        End Get
        Set(value As String)
            m_html = value
        End Set

    End Property
    Private m__validation As validation
    Private m_html As String
End Class

Public Class validation
    Public Property ccdaValidationResults() As List(Of CcdaValidationResult)
        Get
            Return m_ccdaValidationResults
        End Get
        Set(value As List(Of CcdaValidationResult))
            m_ccdaValidationResults = value
        End Set
    End Property
    Private m_ccdaValidationResults As List(Of CcdaValidationResult)
    Public Property resultsMetaData() As ResultsMetaData
        Get
            Return m_resultsMetaData
        End Get
        Set(value As ResultsMetaData)
            m_resultsMetaData = Value
        End Set
    End Property
    Private m_resultsMetaData As ResultsMetaData
End Class


