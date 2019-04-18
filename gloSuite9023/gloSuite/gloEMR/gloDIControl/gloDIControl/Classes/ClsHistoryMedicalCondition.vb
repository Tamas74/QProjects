Imports gloDIControl
Imports gloDIControl.DrugInteractionCollection


Public Class ClsHistoryMedicalCondition

    Dim _AdEscreening As New gloADEScreen("")

    'this function is called from the frmPatientHistory medical condition button click to fill the 
    'medical conditions in the tree view.this will return a collection object that will contain the medical condition & respective ID's
    Public Function FillMedicationForScreening() As DrugInteractionCollection.gloInteractionCollection

        Dim _GloDrugInteractionCol As New gloInteractionCollection
        Try
            _AdEscreening.FillMedicalConditions(_GloDrugInteractionCol)
            Return _GloDrugInteractionCol
           
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New DrugInteractionControlException
            objex.ErrMessage = ex.Message
            Throw objex
        Finally
            '_GloDrugInteractionCol.Dispose()
            '_GloDrugInteractionCol = Nothing
        End Try
    End Function
    Public Function SearchMedicalCondition(ByVal strsearch As String) As DrugInteractionCollection.gloInteractionCollection
        Try
            Dim _GloDrugInteractionCol As New gloInteractionCollection
            _AdEscreening.SearchMedicalConditions(strsearch, _GloDrugInteractionCol)
            Return _GloDrugInteractionCol
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New DrugInteractionControlException
            objex.ErrMessage = ex.Message
            Throw objex
        Finally
            '_GloDrugInteractionCol.Dispose()
            '_GloDrugInteractionCol = Nothing
        End Try
    End Function
End Class
