Namespace gloGeneral
    Public Class myList
        ' Inherits CollectionBase
        '  Private oListModifier As myList
        Private itemindex As Int64
        Private itemDescription As String
        Private itemType As Boolean
        Private itemId As System.Int64
        Private itemVisitDate As Date
        Private _IsFinished As Boolean
        Private _HistoryCategory As String
        Private _HistoryItem As String
        Private _SnomedID As String
        Private _SnomedDesc As String
        Private _Code As String

        Private _ParameterName As String

        Private _Value As String

        Private Result As Object

        Private _ICD9No As Integer
        Private _CPTNo As Integer
        Private _ModNo As Integer
        Private _nICDRevision As Int16 = 9

        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
        Private _blnSnoMedOneToOneMapping As Boolean

       

        Sub New(ByVal intindex As Int64, ByVal strDescription As String)
            itemindex = intindex
            itemDescription = strDescription

        End Sub


        Sub New(ByVal UserID As Int64, ByVal UserName As String, ByVal ProviderID As Int64)
            itemindex = UserID
            itemDescription = UserName
            itemId = ProviderID
        End Sub

        Sub New(ByVal intindex As Int64, ByVal strDescription As String, ByVal blnType As Boolean, ByVal intID As System.Int64)
            itemindex = intindex
            itemDescription = strDescription
            itemType = blnType
            itemId = intID
        End Sub
        Sub New()

        End Sub

        Public Property ParameterName() As String
            Get
                Return _ParameterName
            End Get
            Set(ByVal Value As String)
                _ParameterName = Value
            End Set
        End Property

        '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
        Public Property SnoMedOneToOneMapping() As Boolean
            Get
                Return _blnSnoMedOneToOneMapping
            End Get
            Set(ByVal value As Boolean)
                _blnSnoMedOneToOneMapping = value
            End Set
        End Property
        Private _Units As Double = 1
        Public Property Units() As Double
            Get
                Return _Units
            End Get
            Set(ByVal value As Double)
                _Units = value
            End Set
        End Property

        Private _TimedTherapy As String = String.Empty
        Public Property TimedTherapy() As String
            Get
                Return _TimedTherapy
            End Get
            Set(ByVal value As String)
                _TimedTherapy = value
            End Set
        End Property

        Private _UnTimedTherapy As String = String.Empty
        Public Property UnTimedTherapy() As String
            Get
                Return _UnTimedTherapy
            End Get
            Set(ByVal value As String)
                _UnTimedTherapy = value
            End Set
        End Property



        Public Property nICDRevision() As Int16
            Get
                Return _nICDRevision
            End Get
            Set(ByVal value As Int16)
                _nICDRevision = value
            End Set
        End Property

        Public Property Value() As String
            Get
                Return _Value
            End Get
            Set(ByVal Value As String)
                _Value = Value
            End Set
        End Property

        Public Property TemplateResult() As Object
            Get
                Return Result
            End Get
            Set(ByVal Value As Object)
                Result = Value
            End Set
        End Property

        Public Property Index() As Int64
            Get
                Return itemindex
            End Get
            Set(ByVal Value As Int64)
                itemindex = Value
            End Set
        End Property
        Public Property ID() As Int64
            Get
                Return itemId
            End Get
            Set(ByVal Value As Int64)
                itemId = Value
            End Set
        End Property
        Public Property Type() As Boolean
            Get
                Return itemType
            End Get
            Set(ByVal Value As Boolean)
                itemType = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return itemDescription
            End Get
            Set(ByVal Value As String)
                itemDescription = Value
            End Set
        End Property

        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal Value As String)
                _Code = Value
            End Set
        End Property
        'Public Property SnowMadeID() As String
        '    Get
        '        Return _SnowMadeID
        '    End Get
        '    Set(ByVal Value As String)
        '        _SnowMadeID = Value
        '    End Set
        'End Property

        'Public Property SnoDescription() As String
        '    Get
        '        Return _SnoDescription
        '    End Get
        '    Set(ByVal value As String)
        '        _SnoDescription = value
        '    End Set
        'End Property
        Public Property HistoryCategory() As String
            Get
                Return _HistoryCategory
            End Get
            Set(ByVal Value As String)
                _HistoryCategory = Value
            End Set
        End Property
        Private _ModCode As String
        Public Property ModCode() As String
            Get
                Return _ModCode
            End Get
            Set(ByVal value As String)
                _ModCode = value
            End Set
        End Property

        Private _ModDesc As String
        Public Property ModDesc() As String
            Get
                Return _ModDesc
            End Get
            Set(ByVal value As String)
                _ModDesc = value
            End Set
        End Property
        Public Property HistoryItem() As String
            Get
                Return _HistoryItem
            End Get
            Set(ByVal Value As String)
                _HistoryItem = Value
            End Set
        End Property
        Public Property SnomedID() As String
            Get
                Return _SnomedID
            End Get
            Set(ByVal Value As String)
                _SnomedID = Value
            End Set
        End Property
        Public Property SnomedDesc() As String
            Get
                Return _SnomedDesc
            End Get
            Set(ByVal Value As String)
                _SnomedDesc = Value
            End Set
        End Property

        '' Added By Mahesh 
        '' Used in Orders 
        Public Property VisitDate() As Date
            Get
                Return itemVisitDate
            End Get
            Set(ByVal Value As Date)
                itemVisitDate = Value
            End Set
        End Property

        '' Added By Mahesh 
        '' Used in Orders to SetGet Order is Finished /NotFinised 
        Public Property IsFinished() As Boolean
            Get
                Return _IsFinished
            End Get
            Set(ByVal Value As Boolean)
                _IsFinished = Value
            End Set
        End Property

        Public Overrides Function Tostring() As String
            Return itemDescription
        End Function


        Public Property ICD9No() As Integer
            Get
                Return _ICD9No
            End Get
            Set(ByVal value As Integer)
                _ICD9No = value
            End Set
        End Property

        Public Property CPTNo() As Integer
            Get
                Return _CPTNo
            End Get
            Set(ByVal value As Integer)
                _CPTNo = value
            End Set
        End Property

        Public Property ModNo() As Integer
            Get
                Return _ModNo
            End Get
            Set(ByVal value As Integer)
                _ModNo = value
            End Set
        End Property

        Private _ReasonConceptID As String = String.Empty
        Public Property ReasonConceptID() As String
            Get
                Return _ReasonConceptID
            End Get
            Set(ByVal Value As String)
                _ReasonConceptID = Value
            End Set
        End Property
        Private _ReasonConceptDesc As String = String.Empty
        Public Property ReasonConceptDesc() As String
            Get
                Return _ReasonConceptDesc
            End Get
            Set(ByVal value As String)
                _ReasonConceptDesc = value
            End Set
        End Property
    End Class

   
End Namespace
