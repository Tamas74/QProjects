
Namespace gloReports

    Public Class Report

        Private _ReportName As String
        Private _ReportInfo As ReportInfo ''// Author Information
        Private _DataSource As DataSource ''// Database Settings
        Private _Layout As Layout ''// Report Layout Settings
        ' Private _Font As Font ''// Report Font Settings
        Private _Groups As String
        Private _ReportHeader As Section
        Private _PageHeader As Section
        Private _Details As Section
        Private _PageFooter As Section
        Private _ReportFooter As Section
        'Private _Fields As Fields '' // Individual Field Collection


        Public Property ReportName() As String
            Get
                Return _ReportName
            End Get
            Set(ByVal Value As String)
                _ReportName = Value
            End Set
        End Property

        Public Property ReportInfo() As ReportInfo
            Get
                Return _ReportInfo
            End Get
            Set(ByVal Value As ReportInfo)
                _ReportInfo = Value
            End Set
        End Property

        Public Property DataSource() As DataSource

            Get
                Return _DataSource
            End Get
            Set(ByVal Value As DataSource)
                _DataSource = Value
            End Set
        End Property

        Public Property Layout() As Layout
            Get
                Return _Layout
            End Get
            Set(ByVal Value As Layout)
                _Layout = Value
            End Set
        End Property

        '   Public Property Font() As Font
        '    Get
        '        Return _Font
        '    End Get
        '    Set(ByVal Value As Font)
        '        _Font = Value
        '    End Set
        'End Property

        Public Property Groups() As String
            Get
                Return _Groups
            End Get
            Set(ByVal Value As String)
                _Groups = Value
            End Set
        End Property

        Public Property ReportHeader() As Section
            Get
                Return _ReportHeader
            End Get
            Set(ByVal Value As Section)
                _ReportHeader = Value
            End Set
        End Property

        Public Property PageHeader() As Section
            Get
                Return _PageHeader
            End Get
            Set(ByVal Value As Section)
                _PageHeader = Value
            End Set
        End Property

        Public Property Details() As Section
            Get
                Return _Details
            End Get
            Set(ByVal Value As Section)
                _Details = Value
            End Set
        End Property

        Public Property PageFooter() As Section
            Get
                Return _PageFooter
            End Get
            Set(ByVal Value As Section)
                _PageFooter = Value
            End Set
        End Property

        Public Property ReportFooter() As Section
            Get
                Return _ReportFooter
            End Get
            Set(ByVal Value As Section)
                _ReportFooter = Value
            End Set
        End Property

        Public Sub New()
            MyBase.new()
            _ReportHeader = New Section
            _PageHeader = New Section
            _Details = New Section
            _PageFooter = New Section
            _ReportFooter = New Section
            _ReportInfo = New ReportInfo ''// Author Information
            _DataSource = New DataSource ''// Database Settings
            _Layout = New Layout ''// Report Layout Settings
            '  _Font = New Font '
        End Sub

        Protected Overrides Sub Finalize()
            _ReportHeader = Nothing
            _PageHeader = Nothing
            _Details = Nothing
            _PageFooter = Nothing
            _ReportFooter = Nothing
            _ReportInfo = Nothing
            _DataSource = Nothing
            _Layout = Nothing
            '  _Font = Nothing
            MyBase.Finalize()
        End Sub
    End Class

    ''// Report Author
    Public Class ReportInfo
        Private _AuthorInfo As String
        Public Property AuthorInfo() As String
            Get
                Return _AuthorInfo

            End Get
            Set(ByVal Value As String)
                _AuthorInfo = Value
            End Set
        End Property
    End Class

    ''//Database Connection and Settings
    Public Class DataSource
        Private _ConnectionString As String
        Private _RecordSource As String

        Public Property ConnectionString() As String
            Get
                Return _ConnectionString
            End Get
            Set(ByVal Value As String)
                _ConnectionString = Value
            End Set
        End Property

        Public Property RecordSource() As String
            Get
                Return _RecordSource
            End Get
            Set(ByVal Value As String)
                _RecordSource = Value
            End Set
        End Property
    End Class

    ''//Report Layout
    Public Class Layout

        Private _Width As Integer
        Private _Orientation As Integer
        Private _PaperSize As Integer

        Public Property Width() As Integer
            Get
                Return _Width
            End Get
            Set(ByVal Value As Integer)
                _Width = Value
            End Set
        End Property

        Public Property Orientation() As Integer
            Get
                Return _Orientation
            End Get
            Set(ByVal Value As Integer)
                _Orientation = Value
            End Set
        End Property

        Public Property PaperSize() As Integer
            Get
                Return _PaperSize
            End Get
            Set(ByVal Value As Integer)
                _PaperSize = Value

            End Set
        End Property

    End Class

    ''// Setting Fonts and
    '  Public Class Font
    'Private _FontName As String
    'Private _FontStyle As FontStyle
    'Private _FontSize As Int16
    'Public Property FontName() As String
    '    Get
    '        Return _FontName
    '    End Get
    '    Set(ByVal Value As String)
    '        _FontName = Value
    '    End Set
    'End Property

    'Public Property FontStyle() As FontStyle
    '    Get
    '        Return _FontStyle
    '    End Get
    '    Set(ByVal Value As FontStyle)
    '        _FontStyle = Value
    '    End Set
    'End Property

    ''Public Property FontItalic() As Boolean
    ''    Get
    ''        Return _FontItalic
    ''    End Get
    ''    Set(ByVal Value As Boolean)
    ''        _FontItalic = Value
    ''    End Set
    ''End Property
    ''Public Property FontUnderLine() As Boolean
    ''    Get
    ''        Return _FontUnderLine
    ''    End Get
    ''    Set(ByVal Value As Boolean)
    ''        _FontUnderLine = Value
    ''    End Set
    ''End Property

    'Public Property FontSize() As Int16
    '    Get
    '        Return _FontSize
    '    End Get
    '    Set(ByVal Value As Int16)
    '        _FontSize = Value
    '    End Set
    'End Property
    'End Class

    ''// Report Section Details    
    Public Class Section
        Private _SectionType As String
        Private _SectionHeight As Integer
        Private _SectionWidth As Integer
        Private _SectionVisibility As Boolean
        Private _Fields As Fields

        Public Property SectionType() As String
            Get
                Return _SectionType
            End Get
            Set(ByVal Value As String)
                _SectionType = Value
            End Set
        End Property

        Public Property SectionHeight() As Integer
            Get
                Return _SectionHeight
            End Get
            Set(ByVal Value As Integer)
                _SectionHeight = Value
            End Set
        End Property
        Public Property SectionWidth() As Integer
            Get
                Return _SectionWidth
            End Get
            Set(ByVal Value As Integer)
                _SectionWidth = Value
            End Set
        End Property

        Public Property Fields() As Fields
            Get
                Return _Fields
            End Get
            Set(ByVal Value As Fields)
                _Fields = Value
            End Set
        End Property

        Public Property SectionVisibility() As Boolean
            Get
                Return _SectionVisibility
            End Get
            Set(ByVal Value As Boolean)
                _SectionVisibility = Value
            End Set
        End Property

        'Public Sub New(ByVal FieldSection As enumSection)
        '    MyBase.new()
        '    _SectionType = FieldSection
        'End Sub

        'Protected Overrides Sub Finalize()
        '    MyBase.Finalize()
        'End Sub

        Public Sub New()
            MyBase.new()
            _Fields = New Fields
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

    '' Adding field to the collection
    Public Class Fields
        Implements System.Collections.IEnumerable
        Private mCol As Collection
        Public Function Add(ByRef oField As Field) As Field
            mCol.Add(oField)

            'sarika 25th june 07
            Return oField
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Field
            Get
                Item = mCol.Item(vntIndexKey)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Count = mCol.Count()
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
            'GetEnumerator = mCol.GetEnumerator

            ''sarika 25th june 07
            Return Nothing
        End Function

        Public Sub Remove(ByRef vntIndexKey As Object)
            mCol.Remove(vntIndexKey)
        End Sub

        Public Sub New()
            MyBase.New()
            mCol = New Collection
        End Sub

        Protected Overrides Sub Finalize()
            Clear()
            mCol = Nothing
            MyBase.Finalize()
        End Sub

        Public Sub Clear()
            If mCol Is Nothing Then Exit Sub ' Shouldn't happen, but just in case.
            Dim i As Short
            For i = mCol.Count() To 1 Step -1
                mCol.Remove(i)
            Next i
        End Sub
    End Class

    ''// Categorizing the Reports Section 
    Public Enum enumSection
        ReportHeader = 1
        PageHeader = 2
        Details = 3
        PageFooter = 4
        ReportFooter = 5
    End Enum

    ''// Individual Field
    Public Class Field
        ' Private _Section As Integer
        Private _FieldName As String
        Private _FieldText As String
        Private _FieldLeft As Integer
        Private _FieldTop As Integer
        Private _FieldWidth As Integer
        Private _FieldHeight As Integer
        Private _FieldAlign As HorizontalAlignment
        Private _FieldForeColor As Color
        Private _FieldWordWrap As Boolean
        Private _FieldCanGrow As Integer
        Private _FieldCalculated As Integer
        ' Private _FieldFont As Font
        Private _FontName As String
        Private _FontStyle As FontStyle
        Private _FontSize As Int16
        Private _FieldType As String
        Private _SizeMode As String
        Private _FieldPosition As Integer
        Private _FieldImage As Image

        Public Property FieldImage() As Image
            Get
                Return _FieldImage
            End Get
            Set(ByVal Value As Image)
                _FieldImage = Value
            End Set
        End Property
        Public Property SizeMode() As String
            Get
                Return _SizeMode
            End Get
            Set(ByVal Value As String)
                _SizeMode = Value
            End Set
        End Property

        Public Property FontName() As String
            Get
                Return _FontName
            End Get
            Set(ByVal Value As String)
                _FontName = Value
            End Set
        End Property

        Public Property FontStyle() As FontStyle
            Get
                Return _FontStyle
            End Get
            Set(ByVal Value As FontStyle)
                _FontStyle = Value
            End Set
        End Property
        Public Property FieldPosition() As Int16
            Get
                Return _FieldPosition
            End Get
            Set(ByVal Value As Int16)
                _FieldPosition = Value
            End Set
        End Property

        Public Property FontSize() As Int16
            Get
                Return _FontSize
            End Get
            Set(ByVal Value As Int16)
                _FontSize = Value
            End Set
        End Property

        Public Property FieldName() As String
            Get
                Return _FieldName
                '   Dim a As TextBox

            End Get
            Set(ByVal Value As String)
                _FieldName = Value
            End Set
        End Property

        'Public Property FieldSection() As Integer
        '    Get
        '        Return _Section
        '    End Get
        '    Set(ByVal Value As Integer)
        '        _Section = Value
        '    End Set
        'End Property

        Public Property FieldText() As String
            Get
                Return _FieldText
            End Get
            Set(ByVal Value As String)
                _FieldText = Value
            End Set
        End Property

        Public Property FieldType() As String
            Get
                Return _FieldType
            End Get
            Set(ByVal Value As String)
                _FieldType = Value
            End Set
        End Property

        Public Property FieldLeft() As Integer
            Get
                Return _FieldLeft
            End Get
            Set(ByVal Value As Integer)
                _FieldLeft = Value
            End Set
        End Property

        Public Property FieldTop() As Integer
            Get
                Return _FieldTop
            End Get
            Set(ByVal Value As Integer)
                _FieldTop = Value
            End Set
        End Property

        Public Property FieldWidth() As Integer
            Get
                Return _FieldWidth
            End Get
            Set(ByVal Value As Integer)
                _FieldWidth = Value
            End Set
        End Property

        Public Property FieldHeight() As Integer
            Get
                Return _FieldHeight
            End Get
            Set(ByVal Value As Integer)
                _FieldHeight = Value
            End Set
        End Property

        Public Property FieldAlign() As HorizontalAlignment
            Get
                Return _FieldAlign

            End Get
            Set(ByVal Value As HorizontalAlignment)
                _FieldAlign = Value
            End Set
        End Property

        Public Property FieldForeColor() As Color
            Get
                Return _FieldForeColor
            End Get
            Set(ByVal Value As Color)
                _FieldForeColor = Value
            End Set
        End Property

        Public Property FieldWordWrap() As Boolean
            Get
                Return _FieldWordWrap
            End Get
            Set(ByVal Value As Boolean)
                _FieldWordWrap = Value
            End Set
        End Property

        Public Property FieldCanGrow() As Integer
            Get
                Return _FieldCanGrow
            End Get
            Set(ByVal Value As Integer)
                _FieldCanGrow = Value
            End Set
        End Property

        Public Property FieldCalculated() As Integer
            Get
                Return _FieldCalculated
            End Get
            Set(ByVal Value As Integer)
                _FieldCalculated = Value
            End Set
        End Property

        'Public Property FieldFont() As Font
        '    Get
        '        Return _FieldFont
        '    End Get
        '    Set(ByVal Value As Font)
        '        _FieldFont = Value
        '    End Set
        'End Property


        'Public Sub New(ByVal FieldSection As enumSection)
        '    MyBase.new()
        '    _Section = FieldSection
        'End Sub

        'Protected Overrides Sub Finalize()
        '    MyBase.Finalize()
        'End Sub

        Public Sub New()
            MyBase.new()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class


End Namespace



