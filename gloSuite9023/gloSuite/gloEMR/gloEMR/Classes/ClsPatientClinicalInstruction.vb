Imports System.Data.SqlClient

Public Class ClsPatientClinicalInstruction
    Implements IDisposable
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                disposed = True
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub


    Public Function GetPatientClinicalInstruction(ByVal nPatientID As Int64, ByVal nInstructionID As Int64, Optional ByVal dtDate As Date = Nothing, Optional ByVal bFromView As Boolean = False) As DataTable
        Dim dtTable As DataTable = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)

            If bFromView = True Then
                oDBPara.Add("@dtDate", Nothing, ParameterDirection.Input, SqlDbType.DateTime)
            Else
                oDBPara.Add("@dtDate", dtDate, ParameterDirection.Input, SqlDbType.DateTime)
            End If

            oDBPara.Add("@nID", nInstructionID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("Get_Patient_ClinicalInstruction", oDBPara, dtTable)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try

        Return dtTable
    End Function

    Public Function DeletePatientClinicalInstruction(ByVal nInstructionID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing


        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDB.Connect(False)
            oDB.Execute_Query("Delete from Patient_ClinicalInstruction where nId=" & nInstructionID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
        Return Nothing

    End Function

    Public Function DeActivatePatientClinicalInstruction(ByVal nInstructionID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing


        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDB.Connect(False)
            oDB.Execute_Query("Update Patient_ClinicalInstruction set bIsActive=0 where nId=" & nInstructionID)
            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
        Return Nothing

    End Function
    Public Function CheckActivePatientClinicalInstruction(ByVal dtdate As Date, ByVal nPatientID As Int64) As Boolean

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oResult As Object = Nothing
        Dim sValue As Boolean = False

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDB.Connect(False)
            oResult = oDB.ExecuteScalar_Query("Select 1 from Patient_ClinicalInstruction where bIsActive=1 and npatientid= " & nPatientID & " and dtDate='" & dtdate & "'")
            oDB.Disconnect()

            If ((oResult IsNot Nothing)) AndAlso Not String.IsNullOrEmpty(Convert.ToString(oResult)) Then
                sValue = Convert.ToBoolean(oResult)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
        Return sValue

    End Function

    Public Function ActivatePatientClinicalInstruction(ByVal nInstructionID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing


        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDB.Connect(False)
            oDB.Execute_Query("Update Patient_ClinicalInstruction set bIsActive=1 where nId=" & nInstructionID)
            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
        Return Nothing

    End Function
    Public Function SavePatientClinicalInstruction(ByVal nID As Int64, ByVal nPatientID As Int64, ByVal dtDate As Date, ByVal sInstruction As String, ByVal sIntructionDtl As String, ByVal bIsActive As Boolean) As Int64


        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters

            oDBPara.Add("@nID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@dtDate", dtDate, ParameterDirection.Input, SqlDbType.DateTime)
            oDBPara.Add("@sClinicalInstruction", sInstruction, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sClinicalInstructionDtl", sIntructionDtl, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@bIsActive", bIsActive, ParameterDirection.Input, SqlDbType.Bit)
            oDB.Connect(False)
            oDB.Execute("INUP_Patient_ClinicalInstruction", oDBPara, nID)
            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If

        End Try
        Return nID
    End Function

End Class
