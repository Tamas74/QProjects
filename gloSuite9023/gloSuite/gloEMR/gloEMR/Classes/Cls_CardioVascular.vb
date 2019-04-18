Imports System.Data
Imports System.Data.SqlClient

Public Class Cls_CardioVascular
    Implements IDisposable
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO:  free managed resources when explicitly called
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
    Private _dtproceduredate As Date
    Private _nCatheterizationID As Long = 0
    Private _sCPTCode As String = ""
    Private _sPhysicianName As String = ""
    Private _nGroupID As Long = 0
    Private _nPatientID As Int64 = 0
    Private _nVisitID As Int64 = 0
    Private _nClinicID As Int64 = 0
    Private _nexamid As Int64 = 0
    Private _sPeak As String = ""
    Private _sTestType As String = ""
    Private _sProcedures As String = ""
    Private _sInterventionType As String = ""
    Private _SRaPressure As String = ""
    Private _sLaPressure As String = ""
    Private _sWaveMean As String = ""
    Private _sRPulmonary As String = ""
    Private _sLPulmonary As String = ""
    Private _sWedge As String = ""
    Private _sRV As String = ""
    Private _sLV As String = ""
    Private _sEarlyDiastolic As String = ""
    Private _sEndDiastolic As String = ""
    Private _sDiastolic As String = ""
    Private _sMean As String = ""
    Private _sPaPressure As String = ""
    Private _sAoPressure As String = ""
    Private _sIVc As String = ""
    Private _sSvc As String = ""
    Private _sRASaturations As String = ""
    Private _sRVSAturations As String = ""
    Private _sLASaturations As String = ""
    Private _sPASaturations As String = ""
    Private _sLVSaturations As String = ""
    Private _sAortic As String = ""
    Private _sCardiacIndex As String = ""
    Private _sCardiacOutput As String = ""
    Private _sAvo2difference As String = ""
    Private _sshuntFraction As String = ""
    Private _sLVEjectionFraction As String = ""
    Private _sLVDiastolicVol As String = ""
    Private _sLVSystolicVol As String = ""
    Private _sRVEjectionFraction As String = ""
    Private _sRVSystolicVol As String = ""
    Private _sRVDiostolicVol As String = ""
    Private _sNarrativeSummary As String = ""

    Public Property dtproceduredate() As Date
        Get
            Return _dtproceduredate
        End Get
        Set(ByVal value As Date)
            _dtproceduredate = value
        End Set
    End Property

    Public Property nCatheterizationID() As Long
        Get
            Return _nCatheterizationID
        End Get
        Set(ByVal value As Long)
            _nCatheterizationID = value
        End Set
    End Property

    Public Property nGroupID() As Long
        Get
            Return _nGroupID
        End Get
        Set(ByVal value As Long)
            _nGroupID = value
        End Set
    End Property

    Public Property sCPTCode() As String
        Get
            Return _sCPTCode
        End Get
        Set(ByVal value As String)
            _sCPTCode = value
        End Set
    End Property

    Public Property sPhysicianName() As String
        Get
            Return _sPhysicianName
        End Get
        Set(ByVal value As String)
            _sPhysicianName = value
        End Set
    End Property

    Public Property nPatientID() As Int64
        Get
            Return _nPatientID
        End Get
        Set(ByVal value As Int64)
            _nPatientID = value
        End Set
    End Property

    Public Property nexamid() As Int64
        Get
            Return _nexamid
        End Get
        Set(ByVal value As Int64)
            _nexamid = value
        End Set
    End Property

    Public Property nVisitID() As Int64
        Get
            Return _nVisitID
        End Get
        Set(ByVal value As Int64)
            _nVisitID = value
        End Set
    End Property

    Public Property nClinicID() As Int64
        Get
            Return _nClinicID
        End Get
        Set(ByVal value As Int64)
            _nClinicID = value
        End Set
    End Property


    Public Property sPeak() As String
        Get
            Return _sPeak
        End Get
        Set(ByVal value As String)
            _sPeak = value
        End Set
    End Property

    Public Property sTestType() As String
        Get
            Return _sTestType
        End Get
        Set(ByVal value As String)
            _sTestType = value
        End Set
    End Property

    Public Property sProcedures() As String
        Get
            Return _sProcedures
        End Get
        Set(ByVal value As String)
            _sProcedures = value
        End Set
    End Property

    Public Property sInterventionType() As String
        Get
            Return _sInterventionType
        End Get
        Set(ByVal value As String)
            _sInterventionType = value
        End Set
    End Property

    Public Property sRaPressure() As String
        Get
            Return _SRaPressure
        End Get
        Set(ByVal value As String)
            _SRaPressure = value
        End Set
    End Property

    Public Property sLaPressure() As String
        Get
            Return _sLaPressure
        End Get
        Set(ByVal value As String)
            _sLaPressure = value
        End Set
    End Property


    Public Property sWaveMean() As String
        Get
            Return _sWaveMean
        End Get
        Set(ByVal value As String)
            _sWaveMean = value
        End Set
    End Property


    Public Property sRPulmonary() As String
        Get
            Return _sRPulmonary
        End Get
        Set(ByVal value As String)
            _sRPulmonary = value
        End Set
    End Property

    Public Property sLPulmonary() As String
        Get
            Return _sLPulmonary
        End Get
        Set(ByVal value As String)
            _sLPulmonary = value
        End Set
    End Property

    Public Property sWedge() As String
        Get
            Return _sWedge
        End Get
        Set(ByVal value As String)
            _sWedge = value
        End Set
    End Property


    Public Property sRV() As String
        Get
            Return _sRV
        End Get
        Set(ByVal value As String)
            _sRV = value
        End Set
    End Property

    Public Property sLV() As String
        Get
            Return _sLV
        End Get
        Set(ByVal value As String)
            _sLV = value
        End Set
    End Property

    Public Property sEarlyDiastolic() As String
        Get
            Return _sEarlyDiastolic
        End Get
        Set(ByVal value As String)
            _sEarlyDiastolic = value
        End Set
    End Property

    Public Property sEndDiastolic() As String
        Get
            Return _sEndDiastolic
        End Get
        Set(ByVal value As String)
            _sEndDiastolic = value
        End Set
    End Property

    Public Property sDiastolic() As String
        Get
            Return _sDiastolic
        End Get
        Set(ByVal value As String)
            _sDiastolic = value
        End Set
    End Property

    Public Property sMean() As String
        Get
            Return _sMean
        End Get
        Set(ByVal value As String)
            _sMean = value
        End Set
    End Property

    Public Property sPaPressure() As String
        Get
            Return _sPaPressure
        End Get
        Set(ByVal value As String)
            _sPaPressure = value
        End Set
    End Property


    Public Property sAoPressure() As String
        Get
            Return _sAoPressure
        End Get
        Set(ByVal value As String)
            _sAoPressure = value
        End Set
    End Property

    Public Property sIVc() As String
        Get
            Return _sIVc
        End Get
        Set(ByVal value As String)
            _sIVc = value
        End Set
    End Property

    Public Property sSvc() As String
        Get
            Return _sSvc
        End Get
        Set(ByVal value As String)
            _sSvc = value
        End Set
    End Property

    Public Property sRASaturations() As String
        Get
            Return _sRASaturations
        End Get
        Set(ByVal value As String)
            _sRASaturations = value
        End Set
    End Property

    Public Property sRVSAturations() As String
        Get
            Return _sRVSAturations
        End Get
        Set(ByVal value As String)
            _sRVSAturations = value
        End Set
    End Property

    Public Property sLASaturations() As String
        Get
            Return _sLASaturations
        End Get
        Set(ByVal value As String)
            _sLASaturations = value
        End Set
    End Property


    Public Property sPASaturations() As String
        Get
            Return _sPASaturations
        End Get
        Set(ByVal value As String)
            _sPASaturations = value
        End Set
    End Property

    Public Property sLVSaturations() As String
        Get
            Return _sLVSaturations
        End Get
        Set(ByVal value As String)
            _sLVSaturations = value
        End Set
    End Property


    Public Property sAortic() As String
        Get
            Return _sAortic
        End Get
        Set(ByVal value As String)
            _sAortic = value
        End Set
    End Property

    Public Property sCardiacIndex() As String
        Get
            Return _sCardiacIndex
        End Get
        Set(ByVal value As String)
            _sCardiacIndex = value
        End Set
    End Property


    Public Property sCardiacOutput() As String
        Get
            Return _sCardiacOutput
        End Get
        Set(ByVal value As String)
            _sCardiacOutput = value
        End Set
    End Property

    Public Property sAvo2difference() As String
        Get
            Return _sAvo2difference
        End Get
        Set(ByVal value As String)
            _sAvo2difference = value
        End Set
    End Property

    Public Property sshuntFraction() As String
        Get
            Return _sshuntFraction
        End Get
        Set(ByVal value As String)
            _sshuntFraction = value
        End Set
    End Property


    Public Property sLVEjectionFraction() As String
        Get
            Return _sLVEjectionFraction
        End Get
        Set(ByVal value As String)
            _sLVEjectionFraction = value
        End Set
    End Property

    Public Property sLVDiastolicVol() As String
        Get
            Return _sLVDiastolicVol
        End Get
        Set(ByVal value As String)
            _sLVDiastolicVol = value
        End Set
    End Property

    Public Property sLVSystolicVol() As String
        Get
            Return _sLVSystolicVol
        End Get
        Set(ByVal value As String)
            _sLVSystolicVol = value
        End Set
    End Property


    Public Property sRVEjectionFraction() As String
        Get
            Return _sRVEjectionFraction
        End Get
        Set(ByVal value As String)
            _sRVEjectionFraction = value
        End Set
    End Property

    Public Property sRVSystolicVol() As String
        Get
            Return _sRVSystolicVol
        End Get
        Set(ByVal value As String)
            _sRVSystolicVol = value
        End Set
    End Property


    Public Property sRVDiostolicVol() As String
        Get
            Return _sRVDiostolicVol
        End Get
        Set(ByVal value As String)
            _sRVDiostolicVol = value
        End Set
    End Property

    Public Property sNarrativeSummary() As String
        Get
            Return _sNarrativeSummary
        End Get
        Set(ByVal value As String)
            _sNarrativeSummary = value
        End Set
    End Property


End Class

Public Class Cls_CardioVasculars
    Inherits CollectionBase

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    'Remove Item at specified index
    Public Sub Remove(ByVal index As Integer)
        ' Check to see if there is a widget at the supplied index.
        If index > Count - 1 Or index < 0 Then
            ' If no object exists, a messagebox is shown and the operation is 
            ' cancelled.
            'System.Windows.Forms.MessageBox.Show("Index not valid!")
        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Cls_CardioVascular
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Cls_CardioVascular)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Cls_CardioVascular)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                Me.Clear()
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub SetListBoxToolTip(ByVal LstBox As ListBox, ByVal C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal Position As Point)

        Dim MousePositionInClientCoords As Point = LstBox.PointToClient(Position)
        Dim indexUnderTheMouse As Integer = LstBox.IndexFromPoint(MousePositionInClientCoords)
        If indexUnderTheMouse > -1 Then
            Dim s As String = LstBox.Items(indexUnderTheMouse).ToString
            Dim g As Graphics = LstBox.CreateGraphics
            If g.MeasureString(s, LstBox.Font).Width > LstBox.ClientRectangle.Width Then
                C1SuperTooltip1.SetToolTip(LstBox, s)
            Else
                C1SuperTooltip1.SetToolTip(LstBox, "")
            End If
            g.Dispose()
        End If

    End Sub

    Public Sub ExpandAll(ByVal C1Grd As C1.Win.C1FlexGrid.C1FlexGrid)

        Dim i As Integer
        Try
            For i = 0 To C1Grd.Rows.Count - 1
                Dim nd As C1.Win.C1FlexGrid.Node = C1Grd.Rows(i).Node
                If Not (nd Is Nothing) Then
                    If nd.Level = 0 Then
                        nd.Expanded = True
                    End If
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

End Class