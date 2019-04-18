Imports System.Windows.Forms
Public Class MyTreeNode
    Inherits TreeNode

    Private _mFieldName As String
    Private _mTableName As String
    Private _mDisplayName As String
    Private _mdatatype As String
    Private _mSeparator As String
    Private _mRefFieldPath As String
    Private _mRefFieldValue As String
    Private _mRefFieldNode As String
    Sub New()
        MyBase.New("")
    End Sub
    'for HL7Fields
    Public Sub New(ByVal sFieldName As String, ByVal sDisplayName As String, ByVal sHL7TableName As String)
        'MyBase.New(sDisplayName)
        MyBase.Text = sDisplayName

        _mFieldName = sFieldName
        _mDisplayName = sDisplayName
        _mTableName = sHL7TableName
    End Sub
    'for gloEMRField
    Public Sub New(ByVal sFieldName As String, ByVal sDisplayName As String, ByVal sdatatype As String, ByVal cSeparator As String)
        'MyBase.New(sDisplayName)
        MyBase.Text = sDisplayName

        _mFieldName = sFieldName
        _mDisplayName = sDisplayName
        _mdatatype = sdatatype
        _mSeparator = cSeparator
    End Sub
    '
    Public Sub New(ByVal sDisplayName As String)
        MyBase.Text = sDisplayName
        _mDisplayName = sDisplayName
    End Sub
    ''for gloEMR child node
    'Public Sub New(ByVal sDisplayName As String, ByVal sdatatype As String, ByVal cSeparator As Char)
    '    MyBase.New(sDisplayName)
    '    MyBase.Text = sDisplayName

    '    _mDisplayName = sDisplayName
    '    _mdatatype = sdatatype
    '    _mSeparator = cSeparator
    'End Sub
    Public Property RefFieldPath()
        Get
            Return _mRefFieldPath
        End Get
        Set(ByVal value)
            _mRefFieldPath = value
        End Set
    End Property
    Public Property RefFieldNode()
        Get
            Return _mRefFieldNode
        End Get
        Set(ByVal value)
            _mRefFieldNode = value
        End Set
    End Property
    Public Property RefFieldValue()
        Get
            Return _mRefFieldValue
        End Get
        Set(ByVal value)
            _mRefFieldValue = value
        End Set
    End Property

    Public Property FieldName()
        Get
            Return _mFieldName
        End Get
        Set(ByVal value)
            _mFieldName = value
        End Set
    End Property
    Public Property TableName()
        Get
            Return _mTableName
        End Get
        Set(ByVal value)
            _mTableName = value
        End Set
    End Property
    Public Property DispalyName()
        Get
            Return _mDisplayName
        End Get
        Set(ByVal value)
            _mDisplayName = value
        End Set
    End Property
    Public Property Datatype()
        Get
            Return _mdatatype
        End Get
        Set(ByVal value)
            _mdatatype = value
        End Set
    End Property

    Public Property Separator()
        Get
            Return _mSeparator
        End Get
        Set(ByVal value)
            _mSeparator = value
        End Set
    End Property




End Class

