Imports System.Data.SqlClient

Public Class ClsEjectionFractionDBLayer

    Public Event EjectionFractionAdded(ByVal ID As Int64, ByVal RowIndex As Int32)
    Public Function SaveEjectionFraction(ByRef ArrList As ArrayList) As Boolean
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand
        Dim trEjectionFraction As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trEjectionFraction = Con.BeginTransaction()
        Dim objEjectionFraction As clsEjectionFraction = Nothing
        Try

            For i As Int16 = 0 To ArrList.Count - 1
                ' objEjectionFraction = New clsEjectionFraction
                objEjectionFraction = CType(ArrList(i), clsEjectionFraction)
                'Dim sqlParam As SqlParameter
                Dim EjectionParam As SqlParameter

                'If objEjectionFraction.EjectionFractionID <> 0 Then
                '    '''' if chief Complaints are not inserted then delete that problem list
                '    cmd = New SqlCommand("CV_DeleteEjectionFraction", Con)
                '    cmd.CommandType = CommandType.StoredProcedure
                '    cmd.Transaction = trEjectionFraction

                '    EjectionParam = cmd.Parameters.Add("@nEjectionFractionID", objEjectionFraction.EjectionFractionID)
                '    EjectionParam.Direction = ParameterDirection.Input

                '    If Con.State = ConnectionState.Closed Then
                '        Con.Open()
                '    End If

                '    If cmd.ExecuteNonQuery() > 0 Then
                '        Dim objAudit As New clsAudit
                '        'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & objEjectionFraction.VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
                '        objAudit = Nothing
                '    End If
                'Else
                '' Insert Or Update problem List
                cmd = New SqlCommand("CV_InUpEjectionFraction", Con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trEjectionFraction

                EjectionParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                EjectionParam.Direction = ParameterDirection.Input
                EjectionParam.Value = objEjectionFraction.PatientID

                EjectionParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
                EjectionParam.Direction = ParameterDirection.Input
                EjectionParam.Value = objEjectionFraction.ExamID

                EjectionParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                EjectionParam.Direction = ParameterDirection.Input
                EjectionParam.Value = objEjectionFraction.VisitID

                EjectionParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                EjectionParam.Direction = ParameterDirection.Input
                EjectionParam.Value = objEjectionFraction.ClinicID

                EjectionParam = cmd.Parameters.Add("@dtDateOfTest", SqlDbType.DateTime)
                EjectionParam.Direction = ParameterDirection.Input
                EjectionParam.Value = objEjectionFraction.TestDate

                EjectionParam = cmd.Parameters.Add("@sModalityTest", SqlDbType.VarChar, 100)
                EjectionParam.Direction = ParameterDirection.Input
                EjectionParam.Value = objEjectionFraction.ModalityTest

                EjectionParam = cmd.Parameters.Add("@sQuantityPercent", SqlDbType.VarChar, 50)
                EjectionParam.Direction = ParameterDirection.Input
                If IsNothing(objEjectionFraction.QuantityPercent) Then
                    EjectionParam.Value = DBNull.Value
                Else
                    EjectionParam.Value = objEjectionFraction.QuantityPercent
                End If


                EjectionParam = cmd.Parameters.Add("@sQuantityDesc", SqlDbType.VarChar, 100)
                EjectionParam.Direction = ParameterDirection.Input
                EjectionParam.Value = objEjectionFraction.QuantityDescription

                EjectionParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                EjectionParam.Direction = ParameterDirection.Input
                EjectionParam.Value = GetPrefixTransactionID()


                EjectionParam = cmd.Parameters.AddWithValue("@nEjectionFractionID", objEjectionFraction.EjectionFractionID)
                EjectionParam.Direction = ParameterDirection.InputOutput
                EjectionParam.Value = objEjectionFraction.EjectionFractionID


                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd.ExecuteNonQuery()
                objEjectionFraction.EjectionFractionID = EjectionParam.Value
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                RaiseEvent EjectionFractionAdded(objEjectionFraction.EjectionFractionID, objEjectionFraction.RowIndex)
                If objEjectionFraction.EjectionFractionID = 0 Then
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Ejection Fraction  Added", gstrLoginName, gstrClientMachineName, objEjectionFraction.PatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Ejection Fraction  Added", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Ejection Fraction  Added", objEjectionFraction.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                Else
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Ejection Fraction Modified", gstrLoginName, gstrClientMachineName, objEjectionFraction.PatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Ejection Fraction Modified", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Ejection Fraction  Modified", objEjectionFraction.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                End If

                EjectionParam = Nothing

                'End If
            Next
            trEjectionFraction.Commit()


            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objEjectionFraction.EjectionFractionID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Ejection Fraction could not be Added", gstrLoginName, gstrClientMachineName, objEjectionFraction.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Ejection Fraction could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Ejection Fraction could not be Added", objEjectionFraction.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Ejection Fraction could not be Modified", gstrLoginName, gstrClientMachineName, objEjectionFraction.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Ejection Fraction could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Ejection Fraction could not be Modified", objEjectionFraction.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trEjectionFraction.Rollback()
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objEjectionFraction.EjectionFractionID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Ejection Fraction could not be Added", gstrLoginName, gstrClientMachineName, objEjectionFraction.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Ejection Fraction could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Ejection Fraction could not be Added", objEjectionFraction.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Ejection Fraction could not be Modified", gstrLoginName, gstrClientMachineName, objEjectionFraction.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Ejection Fraction could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Ejection Fraction could not be Modified", objEjectionFraction.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trEjectionFraction.Rollback()
            Return False
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If (Not IsNothing(trEjectionFraction)) Then
                trEjectionFraction.Dispose()
                trEjectionFraction = Nothing
            End If
        End Try
    End Function
    ' '' ''Public Function SaveCardiology(ByRef oClsCardioDeviceCollection As ClsCardioDeviceCollection) As Boolean
    ' '' ''    Dim Con As New SqlConnection(GetConnectionString)
    ' '' ''    Dim cmd As SqlCommand
    ' '' ''    Dim objCardiology As clsCardiologyDevice
    ' '' ''    If Con.State = ConnectionState.Closed Then
    ' '' ''        Con.Open()
    ' '' ''    End If

    ' '' ''    Try

    ' '' ''        For i As Int16 = 0 To oClsCardioDeviceCollection.Count - 1
    ' '' ''            objCardiology = New clsCardiologyDevice
    ' '' ''            objCardiology = oClsCardioDeviceCollection.Item(i)
    ' '' ''            Dim sqlParam As SqlParameter
    ' '' ''            Dim CardiologyParam As SqlParameter

    ' '' ''            '' Insert Or Update problem List
    ' '' ''            cmd = New SqlCommand("CV_InUpCardiologyDevice", Con)
    ' '' ''            cmd.CommandType = CommandType.StoredProcedure

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.PatientID

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.ExamID

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.VisitID

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.ClinicID

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@dtDateOfImplant", SqlDbType.DateTime)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.DateofImplant

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@sDeviceType", SqlDbType.VarChar, 50)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.DeviceType

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@sProductName", SqlDbType.VarChar, 50)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.ProductName

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@sDeviceManufacturer", SqlDbType.VarChar, 100)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.DeviceManufacturer

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@sProductSpecification", SqlDbType.VarChar, 50)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.ProductSpecification

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@sProductSerialNo", SqlDbType.VarChar, 50)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.ProductSerialNo

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@sManufacturerModelNo", SqlDbType.VarChar, 50)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.ManufacturerModelNo

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@sLeadType", SqlDbType.VarChar, 50)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.LeadType

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@dtDateRemoved", SqlDbType.DateTime)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            If objCardiology.DateRemoved = "12:00:00 AM" Then
    ' '' ''                CardiologyParam.Value = Now.Date
    ' '' ''            Else
    ' '' ''                CardiologyParam.Value = objCardiology.DateRemoved
    ' '' ''            End If


    ' '' ''            CardiologyParam = cmd.Parameters.Add("@sPhysicalLocation", SqlDbType.VarChar, 50)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = objCardiology.PhysicalLocation


    ' '' ''            CardiologyParam = cmd.Parameters.Add("@DateFlag", SqlDbType.Int)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            If IsNothing(objCardiology.DateRemoved) Or objCardiology.DateRemoved = "12:00:00 AM" Then
    ' '' ''                CardiologyParam.Value = 0 'Date is not available
    ' '' ''            Else
    ' '' ''                CardiologyParam.Value = 1 'Date  available
    ' '' ''            End If

    ' '' ''            CardiologyParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.Input
    ' '' ''            CardiologyParam.Value = GetPrefixTransactionID()


    ' '' ''            CardiologyParam = cmd.Parameters.Add("@nCardiologyDeviceID", SqlDbType.BigInt)
    ' '' ''            CardiologyParam.Direction = ParameterDirection.InputOutput
    ' '' ''            CardiologyParam.Value = objCardiology.CardiologyDeviceID

    ' '' ''            If Con.State = ConnectionState.Closed Then
    ' '' ''                Con.Open()
    ' '' ''            End If
    ' '' ''            cmd.ExecuteNonQuery()

    ' '' ''            objCardiology.CardiologyDeviceID = CardiologyParam.Value

    ' '' ''            cmd.Parameters.Clear()
    ' '' ''            cmd.Dispose()
    ' '' ''            cmd = Nothing

    ' '' ''            'RaiseEvent EjectionFractionAdded(objCardiology.EjectionFractionID, objCardiology.RowIndex)
    ' '' ''            If objCardiology.CardiologyDeviceID = 0 Then
    ' '' ''                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology  Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Success)
    ' '' ''                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology  Added", gloAuditTrail.ActivityOutCome.Success)
    ' '' ''                ''Added Rahul P on 20101009
    ' '' ''                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology  Added", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    ' '' ''                ''
    ' '' ''            Else
    ' '' ''                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Success)
    ' '' ''                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Modified", gloAuditTrail.ActivityOutCome.Success)
    ' '' ''                ''Added Rahul P on 20101009
    ' '' ''                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology  Modified", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    ' '' ''                ''
    ' '' ''            End If

    ' '' ''            'End If
    ' '' ''            objCardiology.Dispose()
    ' '' ''            objCardiology = Nothing
    ' '' ''        Next

    ' '' ''        Return True
    ' '' ''    Catch ex As SqlException
    ' '' ''        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    ' '' ''        If objCardiology.CardiologyDeviceID = 0 Then
    ' '' ''            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology could not be Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure)
    ' '' ''            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology could not be Added", gloAuditTrail.ActivityOutCome.Failure)
    ' '' ''            ''Added Rahul P on 20101009
    ' '' ''            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology could not be Added", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
    ' '' ''            ''
    ' '' ''        Else
    ' '' ''            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology could not be Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure)
    ' '' ''            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
    ' '' ''            ''Added Rahul P on 20101009
    ' '' ''            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology could not be Modified", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
    ' '' ''            ''
    ' '' ''        End If

    ' '' ''        Throw ex
    ' '' ''    Catch ex As Exception
    ' '' ''        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    ' '' ''        If objCardiology.CardiologyDeviceID = 0 Then
    ' '' ''            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology could not be Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure)
    ' '' ''            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology could not be Added", gloAuditTrail.ActivityOutCome.Failure)
    ' '' ''            ''Added Rahul P on 20101009
    ' '' ''            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology could not be Added", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
    ' '' ''            ''
    ' '' ''        Else
    ' '' ''            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology could not be Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure)
    ' '' ''            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
    ' '' ''            ''Added Rahul P on 20101009
    ' '' ''            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology could not be Modified", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
    ' '' ''            ''
    ' '' ''        End If

    ' '' ''        Throw ex
    ' '' ''    Finally
    ' '' ''        If Con.State = ConnectionState.Open Then
    ' '' ''            Con.Close()
    ' '' ''        End If
    ' '' ''    End Try
    ' '' ''End Function
    Public Sub DeleteCardiology(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtDateofImplant As Date)
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If



        ' Dim sqlParam As SqlParameter
        Dim CardiologyParam As SqlParameter
        Try
            ''''''''''''''''''' Delete existing data
            ''  If ModEntry = True Then

            ''objCardiology = New clsCardiologyDevice
            ''objCardiology = oClsCardioDeviceCollection.Item(0)


            cmd = New SqlCommand("CV_DeleteCardiologyDevice", Con)
            cmd.CommandType = CommandType.StoredProcedure


            CardiologyParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            CardiologyParam.Direction = ParameterDirection.Input
            CardiologyParam.Value = nPatientID





            CardiologyParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            CardiologyParam.Direction = ParameterDirection.Input
            CardiologyParam.Value = nVisitID

            CardiologyParam = cmd.Parameters.Add("@dtDateOfImplant", SqlDbType.DateTime)
            CardiologyParam.Direction = ParameterDirection.Input
            CardiologyParam.Value = Convert.ToDateTime(dtDateofImplant)

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then



            End If



            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "CV Stress Test deleted.", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, "CV Implant Device Test deleted.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 201000915
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, "CV Implant Device Test deleted.", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "CV Stress Test could not be deleted.", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, "CV Implant Device could not be deleted.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 201000915
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, "CV Implant Device could not be deleted.", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
        Finally
            Con.Close()
            Con.Dispose()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            CardiologyParam = Nothing
        End Try

    End Sub

    Public Function SaveCardiology(ByRef oClsCardioDeviceCollection As ClsCardioDeviceCollection, ByVal niPatientID As Long, ByVal niVisitID As Long, ByVal dtiDateofImplant As Date, Optional ByVal sPA_proc As String = "") As Boolean
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim objCardiology As clsCardiologyDevice = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        'Dim sqlParam As SqlParameter
        Dim CardiologyParam As New SqlParameter







        Try
            ''''''''''''''''''' Delete existing data
            ''  If ModEntry = True Then

            ''objCardiology = New clsCardiologyDevice
            ''objCardiology = oClsCardioDeviceCollection.Item(0)

            cmd = New SqlCommand("CV_DeleteCardiologyDevice", Con)
            cmd.CommandType = CommandType.StoredProcedure


            CardiologyParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            CardiologyParam.Direction = ParameterDirection.Input
            CardiologyParam.Value = niPatientID


            CardiologyParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            CardiologyParam.Direction = ParameterDirection.Input
            CardiologyParam.Value = niVisitID

            CardiologyParam = cmd.Parameters.Add("@dtDateOfImplant", SqlDbType.DateTime)
            CardiologyParam.Direction = ParameterDirection.Input
            CardiologyParam.Value = Convert.ToDateTime(dtiDateofImplant)
            ''''''' for proc-device association               
            If sPA_proc <> "" Then
                CardiologyParam = cmd.Parameters.Add("@sProc", SqlDbType.VarChar)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = sPA_proc
            End If
            ''''''' for proc-device association               
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then

            End If

            ''     End If
            '''''''''''''''''''
            For i As Int16 = 0 To oClsCardioDeviceCollection.Count - 1


                '  objCardiology = New clsCardiologyDevice
                objCardiology = oClsCardioDeviceCollection.Item(i)



                cmd = New SqlCommand("CV_InUpCardiologyDevice", Con)
                cmd.CommandType = CommandType.StoredProcedure





                CardiologyParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.PatientID





                CardiologyParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ExamID

                CardiologyParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.VisitID

                CardiologyParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ClinicID

                CardiologyParam = cmd.Parameters.Add("@dtDateOfImplant", SqlDbType.DateTime)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.DateofImplant

                CardiologyParam = cmd.Parameters.Add("@sDeviceType", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.DeviceType

                CardiologyParam = cmd.Parameters.Add("@sProductName", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ProductName

                CardiologyParam = cmd.Parameters.Add("@sDeviceManufacturer", SqlDbType.VarChar, 100)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.DeviceManufacturer

                CardiologyParam = cmd.Parameters.Add("@sProductSpecification", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ProductSpecification

                CardiologyParam = cmd.Parameters.Add("@sProductSerialNo", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ProductSerialNo

                CardiologyParam = cmd.Parameters.Add("@sManufacturerModelNo", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ManufacturerModelNo

                CardiologyParam = cmd.Parameters.Add("@sLeadType", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.LeadType

                CardiologyParam = cmd.Parameters.Add("@dtDateRemoved", SqlDbType.DateTime)
                CardiologyParam.Direction = ParameterDirection.Input
                If objCardiology.DateRemoved = "12:00:00 AM" Then
                    CardiologyParam.Value = Now.Date
                Else
                    CardiologyParam.Value = objCardiology.DateRemoved
                End If


                CardiologyParam = cmd.Parameters.Add("@sPhysicalLocation", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.PhysicalLocation


                CardiologyParam = cmd.Parameters.Add("@DateFlag", SqlDbType.Int)
                CardiologyParam.Direction = ParameterDirection.Input
                If IsNothing(objCardiology.DateRemoved) OrElse objCardiology.DateRemoved = "12:00:00 AM" Then
                    CardiologyParam.Value = 0 'Date is not available
                Else
                    CardiologyParam.Value = 1 'Date  available
                End If

                CardiologyParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = GetPrefixTransactionID()

                CardiologyParam = cmd.Parameters.Add("@nCardiologyDeviceID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.InputOutput
                CardiologyParam.Value = objCardiology.CardiologyDeviceID

                ''''''''''''''''''''''Added by Ujwala Atre as on 05082010

                CardiologyParam = cmd.Parameters.Add("@sLeadLocation", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sLeadLocation

                CardiologyParam = cmd.Parameters.Add("@sThresholdAtrial", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sThresholdAtrial

                CardiologyParam = cmd.Parameters.Add("@sThresholdVentricular", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sThresholdVentricular

                CardiologyParam = cmd.Parameters.Add("@sSensingAtrial", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sSensingAtrial

                CardiologyParam = cmd.Parameters.Add("@sSensingVentricular", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sSensingVentricular

                CardiologyParam = cmd.Parameters.Add("@sImpedenceAtrial", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sImpedenceAtrial

                CardiologyParam = cmd.Parameters.Add("@sImpedenceVentricular", SqlDbType.VarChar, 50)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sImpedenceVentricular

                CardiologyParam = cmd.Parameters.Add("@sCPTCode", SqlDbType.VarChar, 500)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sCPT

                CardiologyParam = cmd.Parameters.Add("@sProcedures", SqlDbType.VarChar, 500)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.sProc

                If sPA_proc <> "" Then
                    CardiologyParam = cmd.Parameters.Add("@bProcDeviceAssociation", SqlDbType.Bit)
                    CardiologyParam.Direction = ParameterDirection.Input
                    CardiologyParam.Value = 1
                End If

                ''''''''''''''''''''''Added by Ujwala Atre as on 05082010

                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd.ExecuteNonQuery()

                objCardiology.CardiologyDeviceID = cmd.Parameters("@nCardiologyDeviceID").Value

                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                'RaiseEvent EjectionFractionAdded(objCardiology.EjectionFractionID, objCardiology.RowIndex)
                If objCardiology.CardiologyDeviceID = 0 Then
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology  Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Success)
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology  Added", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000915
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology  Added", objCardiology.PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                Else
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Success)
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Modified", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000915
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Modified", objCardiology.PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                End If

                'End If
                'objCardiology.Dispose()
                objCardiology = Nothing

                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                CardiologyParam = Nothing
            Next

            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objCardiology.CardiologyDeviceID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology could not be Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 201000915
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology could not be Added", objCardiology.PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology could not be Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 201000915
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology could not be Modified", objCardiology.PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
            End If

            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objCardiology.CardiologyDeviceID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology could not be Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 201000915
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology could not be Added", objCardiology.PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology could not be Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 201000915
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology could not be Modified", objCardiology.PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
            End If

            Throw ex
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            Con.Dispose()
        End Try
    End Function

    Public Function SaveCardiologyAssociation(ByRef oClsCardioDeviceCollection As ClsCardioDeviceCollection, Optional ByVal sProc As String = "") As Boolean
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim objCardiology As clsCardiologyDevice = Nothing
        Try

            For i As Int16 = 0 To oClsCardioDeviceCollection.Count - 1
                ' objCardiology = New clsCardiologyDevice
                objCardiology = oClsCardioDeviceCollection.Item(i)
                'Dim sqlParam As SqlParameter
                Dim CardiologyParam As SqlParameter

                '' Insert Or Update problem List
                cmd = New SqlCommand("CV_InUpProcedureDeviceAssociation", Con)
                cmd.CommandType = CommandType.StoredProcedure

                CardiologyParam = cmd.Parameters.AddWithValue("@nProcDeviceAssociationID", objCardiology.CardiologyDeviceID)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.CardiologyDeviceID

                CardiologyParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.PatientID

                CardiologyParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ExamID

                CardiologyParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.VisitID

                CardiologyParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ClinicID

                CardiologyParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
                CardiologyParam.Direction = ParameterDirection.Input
                CardiologyParam.Value = objCardiology.ProcedureDate

                CardiologyParam = cmd.Parameters.Add("@sProcedures", SqlDbType.VarChar, 100)
                CardiologyParam.Direction = ParameterDirection.Input
                If sProc <> "" Then
                    CardiologyParam.Value = sProc
                Else
                    CardiologyParam.Value = objCardiology.sProc
                End If
                CardiologyParam.Value = objCardiology.sProc

                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd.ExecuteNonQuery()

                If objCardiology.CardiologyDeviceID = 0 Then
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology Association Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology Association Added", gloAuditTrail.ActivityOutCome.Success)

                Else
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology Association Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Association Modified", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Association Modified", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                End If

                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                CardiologyParam = Nothing
                'End If
            Next

            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objCardiology.CardiologyDeviceID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology Association could not be Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology Association could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology Association could not be Added", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology Association could not be Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Association could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Association could not be Modified", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
            Throw ex

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objCardiology.CardiologyDeviceID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Cardiology Association could not be Added", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology Association could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Cardiology Association could not be Added", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Cardiology Association could not be Modified", gstrLoginName, gstrClientMachineName, objCardiology.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Association could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Cardiology Association could not be Modified", objCardiology.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If

            Throw ex
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            Con.Dispose()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Public Sub DeleteStressTest(ByVal mPatientID As Int64, ByVal mVisitId As Int64, ByVal mDateofStudy As Date)
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        'Dim sqlParam As SqlParameter
        Dim StressParam As SqlParameter
        Try
            cmd = New SqlCommand("CV_DeleteStressTest", Con)
            cmd.CommandType = CommandType.StoredProcedure


            StressParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = mPatientID


            StressParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = mVisitId

            StressParam = cmd.Parameters.Add("@DateOfStudy", SqlDbType.DateTime)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = mDateofStudy


            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then
                'Dim objAudit As New clsAudit
                ''objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & objStressTest.VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
                'objAudit = Nothing
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            StressParam = Nothing
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "CV Stress Test deleted.", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, "CV Stress Test deleted.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, "CV Stress Test deleted.", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "CV Stress Test could not be deleted.", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, "CV Stress Test could not be deleted.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, "CV Stress Test could not be deleted.", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Finally
            If Not IsNothing(Con) Then ''open connection closed
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If

        End Try

    End Sub
    Public Function SaveStressTest(ByVal ArrList As ArrayList) As Boolean
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim trStressTest As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trStressTest = Con.BeginTransaction()
        Dim objStressTest As clsstresstest = Nothing
        Try

            For i As Int16 = 0 To ArrList.Count - 1

                'objStressTest = New clsstresstest
                objStressTest = CType(ArrList(i), clsstresstest)
                'Dim sqlParam As SqlParameter
                Dim StressParam As SqlParameter

                If i = 0 Then



                    '''' if chief Complaints are not inserted then delete that problem list
                    cmd = New SqlCommand("CV_DeleteStressTest", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Transaction = trStressTest

                    StressParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
                    StressParam.Direction = ParameterDirection.Input
                    StressParam.Value = objStressTest.PatientID


                    StressParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                    StressParam.Direction = ParameterDirection.Input
                    StressParam.Value = objStressTest.VisitID

                    StressParam = cmd.Parameters.Add("@DateOfStudy", SqlDbType.DateTime)
                    StressParam.Direction = ParameterDirection.Input
                    StressParam.Value = objStressTest.DateofStudy


                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                    End If

                    If cmd.ExecuteNonQuery() > 0 Then
                        'Dim objAudit As New clsAudit
                        ''objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & objStressTest.VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
                        'objAudit = Nothing
                    End If
                    If cmd IsNot Nothing Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If                    
                End If
                '' Insert Or Update problem List
                cmd = New SqlCommand("CV_InUpStressTest", Con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trStressTest

                StressParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = objStressTest.PatientID

                StressParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = objStressTest.Examid

                StressParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = objStressTest.VisitID

                StressParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = objStressTest.ClinicID

                StressParam = cmd.Parameters.Add("@dtDateOfStudy", SqlDbType.DateTime)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = objStressTest.DateofStudy

                StressParam = cmd.Parameters.Add("@sTestType", SqlDbType.VarChar, 100)
                StressParam.Direction = ParameterDirection.Input
                If IsNothing(objStressTest.TestType) Then
                    StressParam.Value = ""
                Else
                    StressParam.Value = objStressTest.TestType
                End If


                StressParam = cmd.Parameters.Add("@sCPT", SqlDbType.VarChar, 100)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = objStressTest.procedure

                StressParam = cmd.Parameters.Add("@sResult", SqlDbType.VarChar, 100)
                StressParam.Direction = ParameterDirection.Input
                If IsNothing(objStressTest.Result) Then
                    StressParam.Value = ""
                Else
                    StressParam.Value = objStressTest.Result
                End If


                StressParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar, 2000)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = objStressTest.UserName

                StressParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = GetPrefixTransactionID()


                StressParam = cmd.Parameters.AddWithValue("@nStressID", objStressTest.StressID)
                StressParam.Direction = ParameterDirection.InputOutput
                StressParam.Value = objStressTest.StressID

                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd.ExecuteNonQuery()
                objStressTest.StressID = StressParam.Value
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                StressParam = Nothing
                If objStressTest.StressID = 0 Then
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Stress Test Added", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Stress Test Added", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Stress Test Added", objStressTest.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                Else
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Stress Test Modified", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Stress Test could not be Modified", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Stress Test could not be Modified", objStressTest.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                End If

            Next
            trStressTest.Commit()



            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objStressTest.StressID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Stress Test could not be Added", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Stress Test could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Stress Test could not be Added", objStressTest.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Stress Test  could not be Modified", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Stress Test could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Stress Test could not be Modified", objStressTest.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trStressTest.Rollback()
            Return False
        Catch ex As Exception
            If objStressTest.StressID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Stress Test could not be Added", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Stress Test could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Stress Test could not be Added", objStressTest.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Stress Test could not be Modified", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Stress Test could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Stress Test could not be Modified", objStressTest.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trStressTest.Rollback()
            Return False
        Finally
            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
            If Not IsNothing(trStressTest) Then
                trStressTest.Dispose()
                trStressTest = Nothing
            End If

        End Try
    End Function

End Class
