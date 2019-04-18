Imports System

Public Class PrescriptionMedicationEventArgs
    Inherits System.EventArgs
    Public PrescriptionID As Long

    Public Sub New(ByVal prescription_id As Long)
        PrescriptionID = prescription_id
    End Sub
End Class
