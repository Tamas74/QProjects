Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Linq


Namespace gloSmartDx.Core

    Public Class SmartDx
        Implements IDisposable

        Public observableSmartDx As New ObservableCollection(Of SmartDxDisplay)()
        Public Shared dtSmartDx As New DataTable()

        Public Sub New()
            Me.smartICDCodes = New List(Of ICD)
            Me.smartCPTCodes = New List(Of CPT)
            Me.smartDrugs = New List(Of Drugs)
            Me.smartPatientEducation = New List(Of PatientEducation)
            Me.smartTags = New List(Of Tags)
            Me.smartOrders = New List(Of Orders)
            Me.smartReferralLetter = New List(Of ReferralLetter)
            Me.smartFlowsheet = New List(Of Flowsheet)            
            Me.smartCPTModifiers = New List(Of CPTModifiers)

            Me.smartOrderTemplate = New List(Of OrderTemplates)
        End Sub

        Private _smartICDCodes As List(Of ICD) = Nothing
        Public Property smartICDCodes() As List(Of ICD)
            Get
                Return _smartICDCodes
            End Get
            Set(value As List(Of ICD))
                _smartICDCodes = value
            End Set
        End Property

        Private _smartCPTCodes As List(Of CPT) = Nothing
        Public Property smartCPTCodes() As List(Of CPT)
            Get
                Return _smartCPTCodes
            End Get
            Set(value As List(Of CPT))
                _smartCPTCodes = value
            End Set
        End Property

        Private _smartDrugs As List(Of Drugs) = Nothing
        Public Property smartDrugs() As List(Of Drugs)
            Get
                Return _smartDrugs
            End Get
            Set(value As List(Of Drugs))
                _smartDrugs = value
            End Set
        End Property

        Private _smartPatientEducation As List(Of PatientEducation) = Nothing
        Public Property smartPatientEducation() As List(Of PatientEducation)
            Get
                Return _smartPatientEducation
            End Get
            Set(value As List(Of PatientEducation))
                _smartPatientEducation = value
            End Set
        End Property

        Private _smartTags As List(Of Tags) = Nothing
        Public Property smartTags() As List(Of Tags)
            Get
                Return _smartTags
            End Get
            Set(value As List(Of Tags))
                _smartTags = value
            End Set
        End Property

        Private _smartOrders As List(Of Orders) = Nothing
        Public Property smartOrders() As List(Of Orders)
            Get
                Return _smartOrders
            End Get
            Set(value As List(Of Orders))
                _smartOrders = value
            End Set
        End Property

        Private _smartOrderTemplate As List(Of OrderTemplates) = Nothing
        Public Property smartOrderTemplate() As List(Of OrderTemplates)
            Get
                Return _smartOrderTemplate
            End Get
            Set(value As List(Of OrderTemplates))
                _smartOrderTemplate = value
            End Set
        End Property

        Private _smartReferralLetter As List(Of ReferralLetter) = Nothing
        Public Property smartReferralLetter() As List(Of ReferralLetter)
            Get
                Return _smartReferralLetter
            End Get
            Set(value As List(Of ReferralLetter))
                _smartReferralLetter = value
            End Set
        End Property

        Private _smartFlowsheet As List(Of Flowsheet) = Nothing
        Public Property smartFlowsheet() As List(Of Flowsheet)
            Get
                Return _smartFlowsheet
            End Get
            Set(value As List(Of Flowsheet))
                _smartFlowsheet = value
            End Set
        End Property

        Private _smartCPTModifiers As List(Of CPTModifiers) = Nothing
        Public Property smartCPTModifiers() As List(Of CPTModifiers)
            Get
                Return _smartCPTModifiers
            End Get
            Set(value As List(Of CPTModifiers))
                _smartCPTModifiers = value
            End Set
        End Property


        Public Shared Function ToDataTable(Of T)(data As IList(Of T)) ' As DataTable
            Dim props As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
            Try
                dtSmartDx.Columns.Add("nPatientID")
                dtSmartDx.Columns.Add("nExamID")
                dtSmartDx.Columns.Add("nVisitID")
            Catch ex As DuplicateNameException
                'Goto Next Column
            End Try

            Try
                For i As Integer = 0 To props.Count - 1
                    Dim prop As PropertyDescriptor = props(i)
                    Dim myColumn As DataColumn = Nothing
                    'If table.Columns.Count > 1 Then
                    '    myColumn = (From column As DataColumn In table.Columns Where column.ColumnName = Convert.ToString(prop.Name).FirstOrDefault())
                    'End If
                    If IsNothing(myColumn) Then
                        Try
                            If prop.Name <> "CPTModifiers" Then
                                dtSmartDx.Columns.Add(prop.Name, prop.PropertyType)
                            End If
                        Catch ex As DuplicateNameException
                            'Goto Next Column
                        End Try

                    End If
                Next
            Catch ex As Exception

            End Try

            'Dim values As Object() = New Object(props.Count - 1) {}
            'For Each item As T In data
            '    For i As Integer = 0 To values.Length - 1
            '        values(i) = props(i).GetValue(item)
            '    Next
            '    table.Rows.Add(values)
            'Next
            'Return table
            Return Nothing
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then

                    If Me.smartICDCodes IsNot Nothing Then
                        Me.smartICDCodes.Clear()
                        Me.smartICDCodes = Nothing
                    End If

                    If Me.smartCPTCodes IsNot Nothing Then
                        Me.smartCPTCodes.Clear()
                        Me.smartCPTCodes = Nothing
                    End If

                    If Me.smartDrugs IsNot Nothing Then
                        Me.smartDrugs.Clear()
                        Me.smartDrugs = Nothing
                    End If

                    If Me.smartPatientEducation IsNot Nothing Then
                        Me.smartPatientEducation.Clear()
                        Me.smartPatientEducation = Nothing
                    End If

                    If Me.smartTags IsNot Nothing Then
                        Me.smartTags.Clear()
                        Me.smartTags = Nothing
                    End If

                    If Me.smartOrders IsNot Nothing Then
                        Me.smartOrders.Clear()
                        Me.smartOrders = Nothing
                    End If

                    If Me.smartReferralLetter IsNot Nothing Then
                        Me.smartReferralLetter.Clear()
                        Me.smartReferralLetter = Nothing
                    End If

                    If Me.smartFlowsheet IsNot Nothing Then
                        Me.smartFlowsheet.Clear()
                        Me.smartFlowsheet = Nothing
                    End If

                    If Me.smartCPTModifiers IsNot Nothing Then
                        Me.smartCPTModifiers.Clear()
                        Me.smartCPTModifiers = Nothing
                    End If

                    If smartOrderTemplate IsNot Nothing Then
                        smartOrderTemplate.Clear()
                        smartOrderTemplate = Nothing
                    End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class SmartDxDisplay
        Implements IDisposable

        Private _DisplayName As String
        Public Property DisplayName() As String
            Get
                Return _DisplayName
            End Get
            Set(value As String)
                _DisplayName = value
            End Set
        End Property

        Private _Type As String
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(value As String)
                _Type = value
            End Set
        End Property

        Private _Id As Int64
        Public Property Id() As Int64
            Get
                Return _Id
            End Get
            Set(value As Int64)
                _Id = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class ICD
        Implements IDisposable
        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _ICDCode As String
        Public Property ICDCode() As String
            Get
                Return _ICDCode
            End Get
            Set(value As String)
                _ICDCode = value
            End Set
        End Property

        Private _ICDDescription As String
        Public Property ICDDescription() As String
            Get
                Return _ICDDescription
            End Get
            Set(value As String)
                _ICDDescription = value
            End Set
        End Property

        Private _ICDSnomedCode As String
        Public Property ICDSnomedCode() As String
            Get
                Return _ICDSnomedCode
            End Get
            Set(value As String)
                _ICDSnomedCode = value
            End Set
        End Property

        Private _ICDSnomedDescription As String
        Public Property ICDSnomedDescription() As String
            Get
                Return _ICDSnomedDescription
            End Get
            Set(value As String)
                _ICDSnomedDescription = value
            End Set
        End Property

        Private _ICDRevision As Int16
        Public Property ICDRevision() As Int16
            Get
                Return _ICDRevision
            End Get
            Set(value As Int16)
                _ICDRevision = value
            End Set
        End Property

        Private _IsSnomedOneToOne As Boolean
        Public Property IsSnomedOneToOne() As Boolean
            Get
                Return _IsSnomedOneToOne
            End Get
            Set(value As Boolean)
                _IsSnomedOneToOne = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class CPT
        Implements IDisposable

        Public Sub New()
            Me.CPTModifiers = New List(Of CPTModifiers)
        End Sub

        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _CPTId As Int64
        Public Property CPTId() As Int64
            Get
                Return _CPTId
            End Get
            Set(value As Int64)
                _CPTId = value
            End Set
        End Property

        Private _CPTCode As String
        Public Property CPTCode() As String
            Get
                Return _CPTCode
            End Get
            Set(value As String)
                _CPTCode = value
            End Set
        End Property

        Private _CPTDescription As String
        Public Property CPTDescription() As String
            Get
                Return _CPTDescription
            End Get
            Set(value As String)
                _CPTDescription = value
            End Set
        End Property

        Private _CPTUnit As Double
        Public Property CPTUnit() As Double
            Get
                Return _CPTUnit
            End Get
            Set(value As Double)
                _CPTUnit = value
            End Set
        End Property

        Private _CPTLineNo As Int64
        Public Property CPTLineNo() As Int64
            Get
                Return _CPTLineNo
            End Get
            Set(value As Int64)
                _CPTLineNo = value
            End Set
        End Property

        Private _CPTModifiers As List(Of CPTModifiers) = Nothing
        Public Property CPTModifiers() As List(Of CPTModifiers)
            Get
                Return _CPTModifiers
            End Get
            Set(value As List(Of CPTModifiers))
                _CPTModifiers = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If Me.CPTModifiers IsNot Nothing Then
                        Me.CPTModifiers.Clear()
                        Me.CPTModifiers = Nothing
                    End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class CPTModifiers
        Implements IDisposable

        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _CPTId As Int64
        Public Property CPTId() As Int64
            Get
                Return _CPTId
            End Get
            Set(value As Int64)
                _CPTId = value
            End Set
        End Property

        Private _CPTModifierDescription As String
        Public Property CPTModifierDescription() As String
            Get
                Return _CPTModifierDescription
            End Get
            Set(value As String)
                _CPTModifierDescription = value
            End Set
        End Property

        Private _CPTModifierCode As Int64
        Public Property CPTModifierCode() As Int64
            Get
                Return _CPTModifierCode
            End Get
            Set(value As Int64)
                _CPTModifierCode = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class Drugs
        Implements IDisposable
        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _DrugsId As Int64
        Public Property DrugsId() As Int64
            Get
                Return _DrugsId
            End Get
            Set(value As Int64)
                _DrugsId = value
            End Set
        End Property

        Private _DrugsName As String
        Public Property DrugsName() As String
            Get
                Return _DrugsName
            End Get
            Set(ByVal value As String)
                _DrugsName = value
            End Set
        End Property

        Private _Dosage As String
        Public Property Dosage() As String
            Get
                Return _Dosage
            End Get
            Set(ByVal value As String)
                _Dosage = value
            End Set
        End Property

        Private _DrugForm As String
        Public Property DrugForm() As String
            Get
                Return _DrugForm
            End Get
            Set(ByVal value As String)
                _DrugForm = value
            End Set
        End Property

        Private _Route As String
        Public Property Route() As String
            Get
                Return _Route
            End Get
            Set(ByVal value As String)
                _Route = value
            End Set
        End Property

        Private _Frequency As String
        Public Property Frequency() As String
            Get
                Return _Frequency
            End Get
            Set(ByVal value As String)
                _Frequency = value
            End Set
        End Property

        Private _NDCCode As String
        Public Property NDCCode() As String
            Get
                Return _NDCCode
            End Get
            Set(ByVal value As String)
                _NDCCode = value
            End Set
        End Property

        Private _IsNarcotics As Int64
        Public Property IsNarcotics() As Int64
            Get
                Return _IsNarcotics
            End Get
            Set(ByVal value As Int64)
                _IsNarcotics = value
            End Set
        End Property

        Private _Duration As String
        Public Property Duration() As String
            Get
                Return _Duration
            End Get
            Set(ByVal value As String)
                _Duration = value
            End Set
        End Property

        Private _DrugQtyQualifier As String
        Public Property DrugQtyQualifier() As String
            Get
                Return _DrugQtyQualifier
            End Get
            Set(ByVal value As String)
                _DrugQtyQualifier = value
            End Set
        End Property

        Private _Amount As String
        Public Property Amount() As String
            Get
                Return _Amount
            End Get
            Set(ByVal value As String)
                _Amount = value
            End Set
        End Property

        Private _IsFinished As Boolean
        Public Property IsFinished() As Boolean
            Get
                Return _IsFinished
            End Get
            Set(value As Boolean)
                _IsFinished = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class PatientEducation
        Implements IDisposable
        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _EducationTemplateID As Int64
        Public Property EducationTemplateID() As Int64
            Get
                Return _EducationTemplateID
            End Get
            Set(value As Int64)
                _EducationTemplateID = value
            End Set
        End Property

        Private _EducationTemplateName As String
        Public Property EducationTemplateName() As String
            Get
                Return _EducationTemplateName
            End Get
            Set(value As String)
                _EducationTemplateName = value
            End Set
        End Property


        Private _EducationIndex As String
        Public Property EducationIndex() As String
            Get
                Return _EducationIndex
            End Get
            Set(value As String)
                _EducationIndex = value
            End Set
        End Property


        Private _EducationHistoryCategory As String
        Public Property EducationHistoryCategory() As String
            Get
                Return _EducationHistoryCategory
            End Get
            Set(value As String)
                _EducationHistoryCategory = value
            End Set
        End Property


#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class Tags
        Implements IDisposable
        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _TagTemplateID As Int64
        Public Property TagTemplateID() As Int64
            Get
                Return _TagTemplateID
            End Get
            Set(value As Int64)
                _TagTemplateID = value
            End Set
        End Property

        Private _TagTemplateName As String
        Public Property TagTemplateName() As String
            Get
                Return _TagTemplateName
            End Get
            Set(value As String)
                _TagTemplateName = value
            End Set
        End Property

        Private _TagTemplateResult As String
        Public Property TagTemplateResult() As String
            Get
                Return _TagTemplateResult
            End Get
            Set(value As String)
                _TagTemplateResult = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class Orders
        Implements IDisposable
        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _ICDName As String
        Public Property ICDName() As String
            Get
                Return _ICDName
            End Get
            Set(value As String)
                _ICDName = value
            End Set
        End Property

        Private _TestId As Int64
        Public Property TestId() As Int64
            Get
                Return _TestId
            End Get
            Set(value As Int64)
                _TestId = value
            End Set
        End Property

        Private _TestName As String
        Public Property TestName() As String
            Get
                Return _TestName
            End Get
            Set(value As String)
                _TestName = value
            End Set
        End Property

        Private _OrderIsFinished As Boolean
        Public Property OrderIsFinished() As Boolean
            Get
                Return _OrderIsFinished
            End Get
            Set(value As Boolean)
                _OrderIsFinished = value
            End Set
        End Property
#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class OrderTemplates
        Implements IDisposable
        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _OrderTemplateID As Int64
        Public Property OrderTemplateID() As Int64
            Get
                Return _OrderTemplateID
            End Get
            Set(value As Int64)
                _OrderTemplateID = value
            End Set
        End Property

        Private _OrderTemplateName As String
        Public Property OrderTemplateName() As String
            Get
                Return _OrderTemplateName
            End Get
            Set(value As String)
                _OrderTemplateName = value
            End Set
        End Property

        Private _OrderIsFinished As Boolean
        Public Property OrderIsFinished() As Boolean
            Get
                Return _OrderIsFinished
            End Get
            Set(value As Boolean)
                _OrderIsFinished = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class ReferralLetter
        Implements IDisposable
        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _ReferralLetterID As Int64
        Public Property ReferralLetterID() As Int64
            Get
                Return _ReferralLetterID
            End Get
            Set(value As Int64)
                _ReferralLetterID = value
            End Set
        End Property

        Private _ReferralLetterName As String
        Public Property ReferralLetterName() As String
            Get
                Return _ReferralLetterName
            End Get
            Set(value As String)
                _ReferralLetterName = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

    Public Class Flowsheet
        Implements IDisposable
        Private _ICDId As Int64
        Public Property ICDId() As Int64
            Get
                Return _ICDId
            End Get
            Set(value As Int64)
                _ICDId = value
            End Set
        End Property

        Private _FlowsheetID As Int64
        Public Property FlowsheetID() As Int64
            Get
                Return _FlowsheetID
            End Get
            Set(value As Int64)
                _FlowsheetID = value
            End Set
        End Property

        Private _FlowsheetName As String
        Public Property FlowsheetName() As String
            Get
                Return _FlowsheetName
            End Get
            Set(value As String)
                _FlowsheetName = value
            End Set
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
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

End Namespace

